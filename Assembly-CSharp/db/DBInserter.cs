using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using db.tables;
using SQLite;
using UnityEngine;

namespace db
{
	// Token: 0x02000857 RID: 2135
	public static class DBInserter
	{
		// Token: 0x060042C8 RID: 17096 RVA: 0x001C53C4 File Offset: 0x001C35C4
		public static void Lock()
		{
			DBInserter._locked = true;
		}

		// Token: 0x060042C9 RID: 17097 RVA: 0x001C53CC File Offset: 0x001C35CC
		public static void Unlock()
		{
			DBInserter._locked = false;
		}

		// Token: 0x060042CA RID: 17098 RVA: 0x001C53D4 File Offset: 0x001C35D4
		public static bool isLocked()
		{
			return DBInserter._locked || Config.disable_db;
		}

		// Token: 0x060042CB RID: 17099 RVA: 0x001C53E4 File Offset: 0x001C35E4
		public static void deleteData(long pID, string pMetaType)
		{
			if (DBInserter.isLocked())
			{
				return;
			}
			foreach (Type tType in AssetManager.history_meta_data_library.get(pMetaType).table_types.Values)
			{
				DBInserter._delete_commands.Add(new ValueTuple<long, Type>(pID, tType));
			}
		}

		// Token: 0x060042CC RID: 17100 RVA: 0x001C5458 File Offset: 0x001C3658
		public static void insertLog(WorldLogMessage pObject)
		{
			if (DBInserter.isLocked())
			{
				return;
			}
			DBInserter._insert_logs.Add(pObject);
			DBInserter._insert_commands_count++;
		}

		// Token: 0x060042CD RID: 17101 RVA: 0x001C547C File Offset: 0x001C367C
		public static void insertData(BaseSystemData pObject, string tMetaType)
		{
			if (DBInserter.isLocked())
			{
				return;
			}
			ListPool<BaseSystemData> tCommands;
			if (!DBInserter._insert_metas.TryGetValue(tMetaType, out tCommands))
			{
				tCommands = new ListPool<BaseSystemData>();
				DBInserter._insert_metas.Add(tMetaType, tCommands);
			}
			tCommands.Add(pObject);
			DBInserter._insert_commands_count++;
			if (ScrollWindow.isWindowActive())
			{
				DBInserter.executeCommands();
			}
		}

		// Token: 0x060042CE RID: 17102 RVA: 0x001C54D4 File Offset: 0x001C36D4
		public static void insertData(HistoryTable pObject, string tMetaType)
		{
			if (DBInserter.isLocked())
			{
				return;
			}
			ListPool<HistoryTable> tCommands;
			if (!DBInserter._insert_commands.TryGetValue(tMetaType, out tCommands))
			{
				tCommands = new ListPool<HistoryTable>();
				DBInserter._insert_commands.Add(tMetaType, tCommands);
			}
			tCommands.Add(pObject);
			DBInserter._insert_commands_count++;
		}

		// Token: 0x060042CF RID: 17103 RVA: 0x001C551D File Offset: 0x001C371D
		public static bool hasCommands()
		{
			return DBInserter._insert_commands_count > 0 || DBInserter._delete_commands.Count > 0;
		}

		// Token: 0x060042D0 RID: 17104 RVA: 0x001C5536 File Offset: 0x001C3736
		public static void clearCommands()
		{
			DBInserter._insert_logs.Clear();
			DBInserter._insert_commands.Clear();
			DBInserter._insert_metas.Clear();
			DBInserter._insert_commands_count = 0;
			DBInserter._delete_commands.Clear();
		}

		// Token: 0x060042D1 RID: 17105 RVA: 0x001C5568 File Offset: 0x001C3768
		public static void executeCommands()
		{
			DBInserter.waitForAsync();
			if (DBInserter.isLocked())
			{
				return;
			}
			if (!DBInserter.hasCommands())
			{
				return;
			}
			SQLiteConnectionWithLock tDBConn = DBManager.getSyncConnection();
			using (tDBConn.Lock())
			{
				if (DBInserter.hasCommands())
				{
					ListPool<ListPool<BaseSystemData>> tMetasList = (DBInserter._insert_metas.Values.Count > 0) ? new ListPool<ListPool<BaseSystemData>>(DBInserter._insert_metas.Values.Count) : null;
					ListPool<ListPool<HistoryTable>> tCommandsList = (DBInserter._insert_commands.Values.Count > 0) ? new ListPool<ListPool<HistoryTable>>(DBInserter._insert_commands.Values.Count) : null;
					ListPool<WorldLogMessage> tInsertLogsList = (DBInserter._insert_logs.Count > 0) ? new ListPool<WorldLogMessage>(DBInserter._insert_logs) : null;
					ListPool<ValueTuple<long, Type>> tDeleteCommandsList = (DBInserter._delete_commands.Count > 0) ? new ListPool<ValueTuple<long, Type>>(DBInserter._delete_commands) : null;
					foreach (ListPool<HistoryTable> tCommands in DBInserter._insert_commands.Values)
					{
						if (tCommands.Count == 0)
						{
							tCommands.Dispose();
						}
						else
						{
							tCommandsList.Add(tCommands);
						}
					}
					foreach (ListPool<BaseSystemData> tCommands2 in DBInserter._insert_metas.Values)
					{
						if (tCommands2.Count == 0)
						{
							tCommands2.Dispose();
						}
						else
						{
							tMetasList.Add(tCommands2);
						}
					}
					DBInserter.clearCommands();
					tDBConn.RunInTransaction(delegate
					{
						DBInserter.sendToDB(tDBConn, tMetasList, tCommandsList, tDeleteCommandsList, tInsertLogsList);
					});
				}
			}
		}

		// Token: 0x060042D2 RID: 17106 RVA: 0x001C576C File Offset: 0x001C396C
		public static void executeCommandsAsync()
		{
			if (DBInserter.isLocked())
			{
				return;
			}
			if (DBInserter._sql_timeout > 0f)
			{
				DBInserter._sql_timeout -= Time.deltaTime;
				return;
			}
			DBInserter._sql_timeout = 10f;
			if (DBInserter._thread != null && !DBInserter._thread.IsCompleted)
			{
				return;
			}
			if (!DBInserter.hasCommands())
			{
				return;
			}
			SQLiteAsyncConnection tDBConn = DBManager.getAsyncConnection();
			if (!DBInserter.hasCommands())
			{
				return;
			}
			ListPool<ListPool<BaseSystemData>> tMetasList = (DBInserter._insert_metas.Values.Count > 0) ? new ListPool<ListPool<BaseSystemData>>(DBInserter._insert_metas.Values.Count) : null;
			ListPool<ListPool<HistoryTable>> tCommandsList = (DBInserter._insert_commands.Values.Count > 0) ? new ListPool<ListPool<HistoryTable>>(DBInserter._insert_commands.Values.Count) : null;
			ListPool<WorldLogMessage> tInsertLogsList = (DBInserter._insert_logs.Count > 0) ? new ListPool<WorldLogMessage>(DBInserter._insert_logs) : null;
			ListPool<ValueTuple<long, Type>> tDeleteCommandsList = (DBInserter._delete_commands.Count > 0) ? new ListPool<ValueTuple<long, Type>>(DBInserter._delete_commands) : null;
			foreach (ListPool<HistoryTable> tCommands in DBInserter._insert_commands.Values)
			{
				if (tCommands.Count == 0)
				{
					tCommands.Dispose();
				}
				else
				{
					tCommandsList.Add(tCommands);
				}
			}
			foreach (ListPool<BaseSystemData> tCommands2 in DBInserter._insert_metas.Values)
			{
				if (tCommands2.Count == 0)
				{
					tCommands2.Dispose();
				}
				else
				{
					tMetasList.Add(tCommands2);
				}
			}
			DBInserter.clearCommands();
			DBInserter._thread = tDBConn.RunInTransactionAsync(delegate(SQLiteConnection pDBConn)
			{
				DBInserter.sendToDB(pDBConn, tMetasList, tCommandsList, tDeleteCommandsList, tInsertLogsList);
			});
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x001C5954 File Offset: 0x001C3B54
		private static void sendToDB(SQLiteConnection pDBConn, ListPool<ListPool<BaseSystemData>> tMetasList = null, ListPool<ListPool<HistoryTable>> tCommandsList = null, [TupleElementNames(new string[]
		{
			"MetaID",
			"MetaType"
		})] ListPool<ValueTuple<long, Type>> tDeleteCommandsList = null, ListPool<WorldLogMessage> tInsertLogsList = null)
		{
			if (tMetasList != null)
			{
				foreach (ListPool<BaseSystemData> ptr in tMetasList)
				{
					ListPool<BaseSystemData> tCommands = ptr;
					try
					{
						pDBConn.InsertAll(tCommands, Orm.GetType(tCommands[0]), false);
						tCommands.Dispose();
					}
					catch (Exception message)
					{
						Debug.LogError(message);
					}
				}
				tMetasList.Clear();
				tMetasList.Dispose();
			}
			if (tCommandsList != null)
			{
				foreach (ListPool<HistoryTable> ptr2 in tCommandsList)
				{
					ListPool<HistoryTable> tCommands2 = ptr2;
					try
					{
						pDBConn.InsertAll(tCommands2, Orm.GetType(tCommands2[0]), false);
						tCommands2.Dispose();
					}
					catch (Exception message2)
					{
						Debug.LogError(message2);
					}
				}
				tCommandsList.Clear();
				tCommandsList.Dispose();
			}
			if (tDeleteCommandsList != null)
			{
				foreach (ValueTuple<long, Type> ptr3 in tDeleteCommandsList)
				{
					ValueTuple<long, Type> valueTuple = ptr3;
					long tMetaID = valueTuple.Item1;
					Type tMetaType = valueTuple.Item2;
					try
					{
						pDBConn.Delete("id", tMetaID, tMetaType);
					}
					catch (Exception message3)
					{
						Debug.LogError(message3);
					}
				}
				tDeleteCommandsList.Clear();
				tDeleteCommandsList.Dispose();
			}
			if (tInsertLogsList != null && tInsertLogsList.Count > 0)
			{
				try
				{
					pDBConn.InsertAll(tInsertLogsList, typeof(WorldLogMessage), false);
				}
				catch (Exception message4)
				{
					Debug.LogError(message4);
				}
				tInsertLogsList.Clear();
				TableMapping tTableMapping = pDBConn.GetMapping<WorldLogMessage>(CreateFlags.None);
				try
				{
					pDBConn.Execute(string.Format("DELETE FROM {0} WHERE ROWID IN ( SELECT ROWID FROM {1} ORDER by timestamp DESC, ROWID DESC LIMIT {2}, 1000 )", tTableMapping.TableName, tTableMapping.TableName, 2000), Array.Empty<object>());
				}
				catch (Exception message5)
				{
					Debug.LogError(message5);
				}
			}
			if (tInsertLogsList != null)
			{
				tInsertLogsList.Dispose();
			}
		}

		// Token: 0x060042D4 RID: 17108 RVA: 0x001C5B6C File Offset: 0x001C3D6C
		public static void quitting()
		{
			DBInserter._sql_timeout = float.MaxValue;
			DBInserter.waitForAsync();
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x001C5B7D File Offset: 0x001C3D7D
		public static void waitForAsync()
		{
			if (DBInserter._thread != null && !DBInserter._thread.IsCompleted)
			{
				Debug.Log("DBInserter thread is still running");
				DBInserter._thread.WaitAndUnwrapException();
				Debug.Log("DBInserter closed");
				DBInserter._thread = null;
			}
		}

		// Token: 0x040030DF RID: 12511
		internal static int _insert_commands_count = 0;

		// Token: 0x040030E0 RID: 12512
		[TupleElementNames(new string[]
		{
			"MetaID",
			"MetaType"
		})]
		internal static readonly List<ValueTuple<long, Type>> _delete_commands = new List<ValueTuple<long, Type>>(4096);

		// Token: 0x040030E1 RID: 12513
		internal static readonly Dictionary<string, ListPool<HistoryTable>> _insert_commands = new Dictionary<string, ListPool<HistoryTable>>(64);

		// Token: 0x040030E2 RID: 12514
		internal static readonly Dictionary<string, ListPool<BaseSystemData>> _insert_metas = new Dictionary<string, ListPool<BaseSystemData>>(64);

		// Token: 0x040030E3 RID: 12515
		internal static readonly List<WorldLogMessage> _insert_logs = new List<WorldLogMessage>(64);

		// Token: 0x040030E4 RID: 12516
		private const float SQL_TIMEOUT_TIME = 10f;

		// Token: 0x040030E5 RID: 12517
		private static float _sql_timeout = 10f;

		// Token: 0x040030E6 RID: 12518
		private static Task _thread;

		// Token: 0x040030E7 RID: 12519
		private static bool _locked = false;
	}
}
