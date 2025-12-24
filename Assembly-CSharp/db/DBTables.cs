using System;
using System.Collections.Generic;
using SQLite;
using UnityEngine;

namespace db
{
	// Token: 0x02000859 RID: 2137
	public static class DBTables
	{
		// Token: 0x060042E5 RID: 17125 RVA: 0x001C6092 File Offset: 0x001C4292
		public static void createOrMigrateTables()
		{
			DBTables.createTable<WorldLogMessage>();
			DBTables.createTable<KingdomData>();
		}

		// Token: 0x060042E6 RID: 17126 RVA: 0x001C60A0 File Offset: 0x001C42A0
		public static void createOrMigrateTable(Type pType)
		{
			SQLiteConnectionWithLock tDBConn = DBManager.getSyncConnection();
			using (tDBConn.Lock())
			{
				if (tDBConn.CreateTable(pType, CreateFlags.None) != CreateTableResult.Migrated)
				{
					TableMapping tTable = tDBConn.GetMapping(pType, CreateFlags.None);
					string tTableQuery = "SELECT sql FROM sqlite_master WHERE type='table' AND name=?";
					string tTableCreateCommand = tDBConn.ExecuteScalar<string>(tTableQuery, new object[]
					{
						tTable.TableName
					});
					tDBConn.DropTable(tTable);
					int tLastBracket = tTableCreateCommand.LastIndexOf(')');
					tTableCreateCommand = tTableCreateCommand.Substring(0, tLastBracket);
					tTableCreateCommand = tTableCreateCommand.Replace(" integer ", " INT ").Trim().Replace("  ", " ").Replace(" ,", ",").Replace(", ", ",").Replace("\"", "");
					tTableCreateCommand += ",\nauto INT";
					tTableCreateCommand += ",\nPRIMARY KEY(id, timestamp)";
					tTableCreateCommand += "\n)";
					tDBConn.Execute(tTableCreateCommand, Array.Empty<object>());
				}
			}
		}

		// Token: 0x060042E7 RID: 17127 RVA: 0x001C61BC File Offset: 0x001C43BC
		public static void checkTablesOK(bool pDropTable = false)
		{
			int tTimestamp = Date.getCurrentYear();
			bool tTablesOk = true;
			SQLiteConnectionWithLock tDBConn = DBManager.getSyncConnection();
			using (tDBConn.Lock())
			{
				foreach (HistoryMetaDataAsset tHistoryAsset in AssetManager.history_meta_data_library.list)
				{
					if (!tTablesOk)
					{
						break;
					}
					foreach (Type tType in tHistoryAsset.table_types.Values)
					{
						if (DBTables.checkTableExists(tType) && !DBTables.checkTableOK(tType, tTimestamp))
						{
							tTablesOk = false;
							break;
						}
					}
				}
				if (!tTablesOk)
				{
					if (pDropTable)
					{
						Debug.Log("Statistics have future data, dropping...");
						foreach (HistoryMetaDataAsset historyMetaDataAsset in AssetManager.history_meta_data_library.list)
						{
							foreach (Type tType2 in historyMetaDataAsset.table_types.Values)
							{
								if (DBTables.checkTableExists(tType2))
								{
									TableMapping tTable = tDBConn.GetMapping(tType2, CreateFlags.None);
									tDBConn.DropTable(tTable);
								}
							}
						}
						if (DBTables.checkTableExists<KingdomData>())
						{
							tDBConn.DropTable<KingdomData>();
						}
						if (DBTables.checkTableExists<WorldLogMessage>())
						{
							tDBConn.DropTable<WorldLogMessage>();
						}
					}
					else
					{
						Debug.Log("Statistics have future data, clearing...");
						foreach (HistoryMetaDataAsset historyMetaDataAsset2 in AssetManager.history_meta_data_library.list)
						{
							foreach (Type tType3 in historyMetaDataAsset2.table_types.Values)
							{
								if (DBTables.checkTableExists(tType3))
								{
									TableMapping tTable2 = tDBConn.GetMapping(tType3, CreateFlags.None);
									tDBConn.DeleteAll(tTable2);
								}
							}
						}
						if (DBTables.checkTableExists<KingdomData>())
						{
							tDBConn.DeleteAll<KingdomData>();
						}
						if (DBTables.checkTableExists<WorldLogMessage>())
						{
							tDBConn.DeleteAll<WorldLogMessage>();
						}
					}
				}
			}
		}

		// Token: 0x060042E8 RID: 17128 RVA: 0x001C648C File Offset: 0x001C468C
		public static bool checkTableExists(Type pType)
		{
			SQLiteConnectionWithLock tDBConn = DBManager.getSyncConnection();
			bool result;
			using (tDBConn.Lock())
			{
				TableMapping tTable = tDBConn.GetMapping(pType, CreateFlags.None);
				string tTableCheck = "SELECT count(1) FROM sqlite_master WHERE type='table' AND name=?";
				if (tDBConn.ExecuteScalar<int>(tTableCheck, new object[]
				{
					tTable.TableName
				}) == 0)
				{
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060042E9 RID: 17129 RVA: 0x001C64F8 File Offset: 0x001C46F8
		public static bool checkTableOK(Type pType, int pTimestamp)
		{
			SQLiteConnectionWithLock tDBConn = DBManager.getSyncConnection();
			bool result;
			using (tDBConn.Lock())
			{
				TableMapping tTable = tDBConn.GetMapping(pType, CreateFlags.None);
				string tTableCheck = "SELECT count(1) FROM '" + tTable.TableName + "' WHERE timestamp>?";
				if (tDBConn.ExecuteScalar<int>(tTableCheck, new object[]
				{
					pTimestamp
				}) == 0)
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060042EA RID: 17130 RVA: 0x001C6574 File Offset: 0x001C4774
		public static bool checkTableExists<T>()
		{
			return DBTables.checkTableExists(typeof(T));
		}

		// Token: 0x060042EB RID: 17131 RVA: 0x001C6585 File Offset: 0x001C4785
		public static void createTableIfNotExists<T>()
		{
			if (DBTables.checkTableExists<T>())
			{
				return;
			}
			DBTables.createTable<T>();
		}

		// Token: 0x060042EC RID: 17132 RVA: 0x001C6594 File Offset: 0x001C4794
		public static void createTable<T>()
		{
			SQLiteConnectionWithLock tDBConn = DBManager.getSyncConnection();
			using (tDBConn.Lock())
			{
				tDBConn.CreateTable<T>(CreateFlags.None);
			}
		}

		// Token: 0x060042ED RID: 17133 RVA: 0x001C65D8 File Offset: 0x001C47D8
		public static void createOrMigrateTablesLoader(bool pCreating = true)
		{
			string tAction = pCreating ? "Creating" : "Migrating";
			if (!pCreating)
			{
				SmoothLoader.add(delegate
				{
					SQLiteConnectionWithLock tDBConn = DBManager.getSyncConnection();
					foreach (HistoryMetaDataAsset tHistoryAsset in AssetManager.history_meta_data_library.list)
					{
						DBTriggers.dropTrigger(tDBConn, tHistoryAsset);
					}
				}, "Dropping Triggers", false, 0.001f, false);
			}
			SmoothLoader.add(delegate
			{
				DBTables.createOrMigrateTables();
			}, tAction + " Stats", false, 0.001f, false);
			using (List<HistoryMetaDataAsset>.Enumerator enumerator = AssetManager.history_meta_data_library.list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					HistoryMetaDataAsset tHistoryAsset = enumerator.Current;
					SmoothLoader.add(delegate
					{
						foreach (Type pType in tHistoryAsset.table_types.Values)
						{
							DBTables.createOrMigrateTable(pType);
						}
					}, tAction + " Stats (" + tHistoryAsset.table_type.Name + ")", false, 0.001f, false);
				}
			}
			SmoothLoader.add(delegate
			{
				SQLiteConnectionWithLock tDBConn = DBManager.getSyncConnection();
				foreach (HistoryMetaDataAsset tHistoryAsset in AssetManager.history_meta_data_library.list)
				{
					DBTriggers.createTrigger(tDBConn, tHistoryAsset);
				}
			}, tAction + " Triggers", false, 0.001f, false);
		}
	}
}
