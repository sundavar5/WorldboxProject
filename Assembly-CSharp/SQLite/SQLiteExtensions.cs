using System;
using System.Collections.Generic;

namespace SQLite
{
	// Token: 0x02000855 RID: 2133
	public static class SQLiteExtensions
	{
		// Token: 0x060042BA RID: 17082 RVA: 0x001C4D62 File Offset: 0x001C2F62
		public static ListPool<Dictionary<string, long>> Query(this SQLiteConnection conn, string query, params object[] args)
		{
			return new ListPool<Dictionary<string, long>>(conn.CreateCommand(query, args).ExecuteDeferredQuery());
		}

		// Token: 0x060042BB RID: 17083 RVA: 0x001C4D76 File Offset: 0x001C2F76
		public static IEnumerable<Dictionary<string, long>> ExecuteDeferredQuery(this SQLiteConnection conn, string query, params object[] args)
		{
			return conn.CreateCommand(query, args).ExecuteDeferredQuery();
		}

		// Token: 0x060042BC RID: 17084 RVA: 0x001C4D85 File Offset: 0x001C2F85
		public static int Delete(this SQLiteConnection conn, string columnName, object obj, Type objType)
		{
			return conn.Delete(columnName, obj, conn.GetMapping(objType, CreateFlags.None));
		}

		// Token: 0x060042BD RID: 17085 RVA: 0x001C4D98 File Offset: 0x001C2F98
		public static int Delete(this SQLiteConnection conn, string columnName, object columnValue, TableMapping map)
		{
			TableMapping.Column col = map.FindColumn(columnName);
			if (col == null)
			{
				throw new NotSupportedException("Cannot delete " + map.TableName + ": it has no column named " + columnName);
			}
			string q = string.Format("delete from \"{0}\" where \"{1}\" = ?", map.TableName, col.Name);
			int num = conn.Execute(q, new object[]
			{
				columnValue
			});
			if (num > 0)
			{
				conn.OnTableChanged(map, NotifyTableChangedAction.Delete);
			}
			return num;
		}

		// Token: 0x060042BE RID: 17086 RVA: 0x001C4E00 File Offset: 0x001C3000
		public static ListPool<T> ExecuteQueryPool<T>(this SQLiteCommand cmd, TableMapping map)
		{
			return new ListPool<T>(cmd.ExecuteDeferredQuery<T>(map));
		}

		// Token: 0x060042BF RID: 17087 RVA: 0x001C4E0E File Offset: 0x001C300E
		public static ListPool<T> ExecuteQueryPool<T>(this SQLiteCommand cmd, SQLiteConnection conn)
		{
			return new ListPool<T>(cmd.ExecuteDeferredQuery<T>(conn.GetMapping(typeof(T), CreateFlags.None)));
		}

		// Token: 0x060042C0 RID: 17088 RVA: 0x001C4E2C File Offset: 0x001C302C
		public static ListPool<object> QueryPool(this SQLiteConnection conn, TableMapping map, string query, params object[] args)
		{
			return conn.CreateCommand(query, args).ExecuteQueryPool(map);
		}

		// Token: 0x060042C1 RID: 17089 RVA: 0x001C4E3C File Offset: 0x001C303C
		public static ListPool<T> QueryPool<T>(this SQLiteConnection conn, string query, params object[] args) where T : new()
		{
			return conn.CreateCommand(query, args).ExecuteQueryPool(conn);
		}
	}
}
