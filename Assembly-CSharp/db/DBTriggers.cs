using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace db
{
	// Token: 0x0200085A RID: 2138
	public static class DBTriggers
	{
		// Token: 0x060042EE RID: 17134 RVA: 0x001C671C File Offset: 0x001C491C
		public static void createTrigger(SQLiteConnectionWithLock pDBConn, HistoryMetaDataAsset tHistoryAsset)
		{
			List<string> tStatsColumns = new List<string>();
			List<HistoryDataAsset> tColumnAssets = new List<HistoryDataAsset>();
			foreach (HistoryDataAsset tCategory in tHistoryAsset.categories)
			{
				tColumnAssets.Add(tCategory);
				tStatsColumns.Add(tCategory.id);
			}
			foreach (HistoryInterval tInterval in tHistoryAsset.table_types.Keys)
			{
				Type tType = tHistoryAsset.table_types[tInterval];
				ValueTuple<int, HistoryInterval> valueTuple = tInterval.fillFrom();
				int tYearDiviver = valueTuple.Item1;
				HistoryInterval tFromInterval = valueTuple.Item2;
				if (tFromInterval != HistoryInterval.None)
				{
					Type tFromType = tHistoryAsset.table_types[tFromInterval];
					DBTriggers.createInsertionTrigger(pDBConn, tType.Name, tColumnAssets, tFromType.Name, tYearDiviver);
				}
				DBTriggers.createNullDuplicateValuesTrigger(pDBConn, tType.Name, tStatsColumns);
				int tMaxYearsToKeep = tInterval.getMaxTimeFrame();
				DBTriggers.createTrimTableTrigger(pDBConn, tType.Name, tStatsColumns, tMaxYearsToKeep);
			}
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x001C6844 File Offset: 0x001C4A44
		public static void dropTrigger(SQLiteConnectionWithLock pDBConn, HistoryMetaDataAsset tHistoryAsset)
		{
			foreach (HistoryInterval tInterval in tHistoryAsset.table_types.Keys)
			{
				Type tType = tHistoryAsset.table_types[tInterval];
				ValueTuple<int, HistoryInterval> valueTuple = tInterval.fillFrom();
				int tYearDiviver = valueTuple.Item1;
				if (valueTuple.Item2 != HistoryInterval.None)
				{
					DBTriggers.dropInsertionTrigger(pDBConn, tType.Name, tYearDiviver);
				}
				DBTriggers.dropNullDuplicateValuesTrigger(pDBConn, tType.Name);
				DBTriggers.dropTrimTableTrigger(pDBConn, tType.Name);
			}
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x001C68DC File Offset: 0x001C4ADC
		public static void dropTrimTableTrigger(SQLiteConnectionWithLock pDBConn, string pTableName)
		{
			pDBConn.ExecuteScalar<string>("DROP TRIGGER IF EXISTS DELETE_OLD_" + pTableName, Array.Empty<object>());
		}

		// Token: 0x060042F1 RID: 17137 RVA: 0x001C68F8 File Offset: 0x001C4AF8
		public static void createTrimTableTrigger(SQLiteConnectionWithLock pDBConn, string pTableName, List<string> pColumns, int pMaxYears)
		{
			string sqlQuery = string.Format("CREATE TRIGGER IF NOT EXISTS DELETE_OLD_{0}\n\t\t\tAFTER INSERT ON {1}\n\t\t\tWHEN\n\t\t\t\tNEW.timestamp % {2} = 0\n\t\t\tAND\n\t\t\t\tEXISTS (\n    \t\t\t\tSELECT 1 FROM {3}\n    \t\t\t\tWHERE\n\t\t\t\t\t\tid = NEW.id AND\n\t\t\t\t\t\ttimestamp < (NEW.timestamp - {4})\n\t\t\t\t\tLIMIT 1\n\t\t\t\t)\n\t\t\tBEGIN\n\t\t\t\tINSERT OR REPLACE INTO {5}\n\t\t\t\t(\n\t\t\t\t\tid,\n\t\t\t\t\ttimestamp,\n\t\t\t\t\t{6},\n\t\t\t\t\tauto\n\t\t\t\t) VALUES (\n\t\t\t\t\tNEW.id,\n\t\t\t\t\tNEW.timestamp - {7},\n\t\t\t\t\t{8},\n\t\t\t\t\t1\n\t\t\t\t);\n\n-- \t\t\t\tUPDATE {9}\n-- \t\t\t\tSET\n-- \t\t\t\t\t{10}\n-- \t\t\t\tWHERE\n-- \t\t\t\t\tid = NEW.id AND\n-- \t\t\t\t\ttimestamp = (NEW.timestamp - {11})\n-- \t\t\t\t;\n\n\t\t\t\tDELETE FROM {12}\n\t\t\t\tWHERE\n\t\t\t\t\tid = NEW.id AND\n\t\t\t\t\ttimestamp < (NEW.timestamp - {13})\n\t\t\t\t;\n\t\t\tEND;", new object[]
			{
				pTableName,
				pTableName,
				pMaxYears,
				pTableName,
				pMaxYears,
				pTableName,
				string.Join(", ", pColumns),
				pMaxYears,
				string.Join(", ", from x in pColumns
				select string.Format("(SELECT {0} FROM {1} WHERE id = NEW.id AND timestamp <= (NEW.timestamp - {2}) AND {3} IS NOT NULL ORDER BY timestamp DESC LIMIT 1)", new object[]
				{
					x,
					pTableName,
					pMaxYears,
					x
				})),
				pTableName,
				string.Join(", ", from x in pColumns
				select string.Format("{0} = CASE WHEN {1} IS NULL THEN (SELECT {2} FROM {3} WHERE id = NEW.id AND timestamp <= (NEW.timestamp - {4}) AND {5} IS NOT NULL ORDER BY timestamp DESC LIMIT 1) ELSE {6} END", new object[]
				{
					x,
					x,
					x,
					pTableName,
					pMaxYears,
					x,
					x
				})),
				pMaxYears,
				pTableName,
				pMaxYears
			});
			pDBConn.ExecuteScalar<string>(sqlQuery, Array.Empty<object>());
		}

		// Token: 0x060042F2 RID: 17138 RVA: 0x001C6A05 File Offset: 0x001C4C05
		public static void dropNullDuplicateValuesTrigger(SQLiteConnectionWithLock pDBConn, string pTableName)
		{
			pDBConn.ExecuteScalar<string>("DROP TRIGGER IF EXISTS NULL_DUPLICATES_" + pTableName, Array.Empty<object>());
		}

		// Token: 0x060042F3 RID: 17139 RVA: 0x001C6A20 File Offset: 0x001C4C20
		public static void createNullDuplicateValuesTrigger(SQLiteConnectionWithLock pDBConn, string pTableName, List<string> pColumns)
		{
			string[] array = new string[15];
			array[0] = "CREATE TRIGGER IF NOT EXISTS NULL_DUPLICATES_";
			array[1] = pTableName;
			array[2] = "\n\t\t\tAFTER INSERT ON ";
			array[3] = pTableName;
			array[4] = "\n\t\t\t\tWHEN NOT EXISTS (\n\t\t\t\t\tSELECT 1 FROM ";
			array[5] = pTableName;
			array[6] = " WHERE id = NEW.id AND timestamp > NEW.timestamp LIMIT 1\n\t\t\t\t)\n\t\t\tBEGIN\n\t\t\t\tUPDATE ";
			array[7] = pTableName;
			array[8] = "\n\t\t\t\tSET ";
			array[9] = string.Join(", ", from x in pColumns
			select string.Concat(new string[]
			{
				x,
				" = CASE WHEN (SELECT ",
				x,
				" FROM ",
				pTableName,
				" WHERE id = NEW.id AND ",
				x,
				" IS NOT NULL ORDER BY timestamp DESC LIMIT 1,1) = NEW.",
				x,
				" THEN NULL ELSE NEW.",
				x,
				" END"
			}));
			array[10] = "\n\t\t\t\tWHERE rowid = NEW.rowid;\n\n\t\t\t\tDELETE FROM ";
			array[11] = pTableName;
			array[12] = " WHERE rowid = NEW.rowid AND ";
			array[13] = string.Join(" AND ", from x in pColumns
			select x + " IS NULL");
			array[14] = ";\n\t\t\tEND;";
			string sqlQuery = string.Concat(array);
			pDBConn.ExecuteScalar<string>(sqlQuery, Array.Empty<object>());
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x001C6B18 File Offset: 0x001C4D18
		public static void dropInsertionTrigger(SQLiteConnectionWithLock pDBConn, string pTargetTable, int pYearDiviver)
		{
			pDBConn.ExecuteScalar<string>(string.Format("DROP TRIGGER IF EXISTS FILL_{0}_{1}", pTargetTable, pYearDiviver), Array.Empty<object>());
		}

		// Token: 0x060042F5 RID: 17141 RVA: 0x001C6B38 File Offset: 0x001C4D38
		public static void createInsertionTrigger(SQLiteConnectionWithLock pDBConn, string pTargetTable, List<HistoryDataAsset> pColumns, string pSourceTable, int pYearDiviver)
		{
			string format = "CREATE TRIGGER IF NOT EXISTS FILL_{0}_{1}\n\t\t\tAFTER INSERT ON {2}\n\t\t\t\tWHEN NEW.timestamp % {3} = 0 AND NEW.auto IS NOT 1\n\t\t\tBEGIN\n\t\t\tINSERT INTO\n\t\t\t\t{4}(\n\t\t\t\t\t{5},\n\t\t\t\t\tid,\n\t\t\t\t\ttimestamp\n\t\t\t\t)\n\t\t\tSELECT\n\t\t\t\t{6},\n\t\t\t\tNEW.id,\n\t\t\t\tNEW.timestamp\n\t\t\tFROM\n\t\t\t\t(\n\t\t\t\t\tSELECT\n\t\t\t\t\t\t*\n\t\t\t\t\tFROM\n\t\t\t\t\t\t{7}\n\t\t\t\t\tWHERE\n\t\t\t\t\t\tid = NEW.id\n\t\t\t\t\tAND\n\t\t\t\t\t\ttimestamp >= NEW.timestamp - {8}\n\t\t\t\t\tAND\n\t\t\t\t\t\ttimestamp < NEW.timestamp\n\t\t\t\t\tORDER BY\n\t\t\t\t\t\ttimestamp DESC\n\t\t\t\t);\n\n\t\t\tEND;";
			object[] array = new object[9];
			array[0] = pTargetTable;
			array[1] = pYearDiviver;
			array[2] = pSourceTable;
			array[3] = pYearDiviver;
			array[4] = pTargetTable;
			array[5] = string.Join(", ", from x in pColumns
			select x.id);
			array[6] = string.Join(", ", pColumns.Select(delegate(HistoryDataAsset x)
			{
				if (x.max)
				{
					return "MAX(" + x.id + ")";
				}
				if (x.sum)
				{
					return "SUM(" + x.id + ")";
				}
				if (x.average)
				{
					return "CAST(AVG(" + x.id + ")+1-1e-1 AS INT)";
				}
				return "ROUND(AVG(" + x.id + "))";
			}));
			array[7] = pSourceTable;
			array[8] = pYearDiviver;
			string sqlQuery = string.Format(format, array);
			pDBConn.ExecuteScalar<string>(sqlQuery, Array.Empty<object>());
		}
	}
}
