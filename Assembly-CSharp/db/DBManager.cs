using System;
using System.IO;
using SQLite;
using UnityEngine;

namespace db
{
	// Token: 0x02000858 RID: 2136
	public class DBManager : MonoBehaviour
	{
		// Token: 0x060042D7 RID: 17111 RVA: 0x001C5C0E File Offset: 0x001C3E0E
		private static void resetDataPath()
		{
			DBManager._dbpath = Application.persistentDataPath + "/stats.s3db";
		}

		// Token: 0x060042D8 RID: 17112 RVA: 0x001C5C24 File Offset: 0x001C3E24
		public static bool loadDBFrom(string pPath)
		{
			bool result;
			try
			{
				DBManager.closeDB();
				if (!File.Exists(pPath))
				{
					result = false;
				}
				else
				{
					DBManager.resetDataPath();
					if (File.Exists(DBManager._dbpath))
					{
						File.Delete(DBManager._dbpath);
					}
					File.Copy(pPath, DBManager._dbpath);
					DBManager.openDB();
					result = true;
				}
			}
			catch (Exception message)
			{
				Debug.Log("[SQLITE] error loading db");
				Debug.LogError(message);
				result = false;
			}
			return result;
		}

		// Token: 0x060042D9 RID: 17113 RVA: 0x001C5C98 File Offset: 0x001C3E98
		public static void createDB()
		{
			try
			{
				DBManager.closeDB();
				DBManager.resetDataPath();
				if (File.Exists(DBManager._dbpath))
				{
					File.Delete(DBManager._dbpath);
				}
				Debug.Log("[SQLITE] new db " + DBManager._dbpath);
				DBManager.openDB();
			}
			catch (Exception message)
			{
				Debug.Log("[SQLITE] error creating db");
				Debug.Log(message);
			}
		}

		// Token: 0x060042DA RID: 17114 RVA: 0x001C5D04 File Offset: 0x001C3F04
		public static void openDB()
		{
			if (Config.disable_db)
			{
				return;
			}
			if (DBManager._dbconn == null)
			{
				DBManager._dbconn = new SQLiteAsyncConnection(DBManager._dbpath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.NoMutex, true);
				Debug.Log("[SQLITE] opening db " + DBManager._dbconn.LibVersionNumber.ToString());
				DBManager._dbconn.Trace = false;
				SQLiteConnectionWithLock tDBConn = DBManager._dbconn.GetConnection();
				using (tDBConn.Lock())
				{
					tDBConn.ExecuteScalar<string>("PRAGMA temp_store=MEMORY;", Array.Empty<object>());
					tDBConn.ExecuteScalar<string>("PRAGMA synchronous=OFF;", Array.Empty<object>());
					tDBConn.ExecuteScalar<string>("PRAGMA cache_size=4000;", Array.Empty<object>());
					tDBConn.ExecuteScalar<string>("PRAGMA journal_mode=MEMORY;", Array.Empty<object>());
				}
			}
		}

		// Token: 0x060042DB RID: 17115 RVA: 0x001C5DDC File Offset: 0x001C3FDC
		public static SQLiteAsyncConnection getAsyncConnection()
		{
			DBManager.openDB();
			return DBManager._dbconn;
		}

		// Token: 0x060042DC RID: 17116 RVA: 0x001C5DE8 File Offset: 0x001C3FE8
		public static SQLiteConnectionWithLock getSyncConnection()
		{
			DBManager.openDB();
			return DBManager._dbconn.GetConnection();
		}

		// Token: 0x060042DD RID: 17117 RVA: 0x001C5DF9 File Offset: 0x001C3FF9
		public static void clearAndClose()
		{
			DBInserter.waitForAsync();
			DBInserter.clearCommands();
			DBManager.closeDB();
		}

		// Token: 0x060042DE RID: 17118 RVA: 0x001C5E0C File Offset: 0x001C400C
		public static void closeDB()
		{
			if (Config.disable_db)
			{
				return;
			}
			if (DBManager._dbconn != null)
			{
				Debug.Log("[SQLITE] closing db");
				try
				{
					DBManager._dbconn.CloseAsync().WaitAndUnwrapException();
				}
				catch (Exception message)
				{
					Debug.LogError("[SQLITE] error closing db");
					Debug.LogError(message);
				}
				DBManager._dbconn = null;
				Debug.Log("[SQLITE] db closed");
			}
		}

		// Token: 0x060042DF RID: 17119 RVA: 0x001C5E74 File Offset: 0x001C4074
		private static void vacuum()
		{
			DBManager.openDB();
			SQLiteConnectionWithLock tDBConn = DBManager._dbconn.GetConnection();
			using (tDBConn.Lock())
			{
				tDBConn.Execute("vacuum", Array.Empty<object>());
			}
		}

		// Token: 0x060042E0 RID: 17120 RVA: 0x001C5ECC File Offset: 0x001C40CC
		private static void backupTo(string pPath)
		{
			DBManager.openDB();
			SQLiteConnectionWithLock tDBConn = DBManager._dbconn.GetConnection();
			using (tDBConn.Lock())
			{
				tDBConn.Backup(pPath, "main");
			}
		}

		// Token: 0x060042E1 RID: 17121 RVA: 0x001C5F1C File Offset: 0x001C411C
		public static void saveToPath(string pPath)
		{
			if (File.Exists(pPath))
			{
				File.Delete(pPath);
			}
			if (Config.disable_db)
			{
				return;
			}
			string tWhat = "Stats DB";
			string tTempPath = pPath + ".bak";
			bool hasError = false;
			try
			{
				DBInserter.executeCommands();
				DBManager.vacuum();
				DBManager.backupTo(tTempPath);
			}
			catch (IOException e)
			{
				if (Toolbox.IsDiskFull(e))
				{
					WorldTip.showNow("Error saving " + tWhat + " : Disk full!", false, "top", 3f, "#F3961F");
				}
				else
				{
					Debug.Log("Could not save " + tWhat + " due to hard drive / IO Error : ");
					Debug.Log(e);
					WorldTip.showNow("Error saving " + tWhat + " due to IOError! Check console for details", false, "top", 3f, "#F3961F");
				}
				hasError = true;
			}
			catch (Exception message)
			{
				Debug.Log("Could not save " + tWhat + " due to error : ");
				Debug.Log(message);
				WorldTip.showNow("Error saving " + tWhat + "! Check console for errors", false, "top", 3f, "#F3961F");
				hasError = true;
			}
			if (hasError)
			{
				if (File.Exists(tTempPath))
				{
					File.Delete(tTempPath);
					return;
				}
			}
			else
			{
				Toolbox.MoveSafely(tTempPath, pPath);
			}
		}

		// Token: 0x060042E2 RID: 17122 RVA: 0x001C6058 File Offset: 0x001C4258
		private void Awake()
		{
			ScrollWindow.addCallbackShowStarted(delegate(string _)
			{
				DBInserter.executeCommands();
			});
		}

		// Token: 0x060042E3 RID: 17123 RVA: 0x001C607E File Offset: 0x001C427E
		private void OnApplicationQuit()
		{
			DBInserter.quitting();
			DBManager.closeDB();
		}

		// Token: 0x040030E8 RID: 12520
		private static SQLiteAsyncConnection _dbconn;

		// Token: 0x040030E9 RID: 12521
		private static string _dbpath;
	}
}
