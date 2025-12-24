using System;
using System.Collections.Generic;
using System.Linq;
using db.tables;
using SQLite;
using UnityPools;

namespace db
{
	// Token: 0x02000856 RID: 2134
	public static class DBGetter
	{
		// Token: 0x060042C2 RID: 17090 RVA: 0x001C4E4C File Offset: 0x001C304C
		public static ListPool<GraphTimeScale> getTimeScales(NanoObject pObject)
		{
			if (Config.disable_db)
			{
				return new ListPool<GraphTimeScale>();
			}
			return DBGetter.getTimeScales(pObject.getID(), pObject.getType());
		}

		// Token: 0x060042C3 RID: 17091 RVA: 0x001C4E6C File Offset: 0x001C306C
		public static ListPool<GraphTimeScale> getTimeScales(long pID, string pMetaType)
		{
			SQLiteConnectionWithLock tDBConn = DBManager.getSyncConnection();
			ListPool<GraphTimeScale> result;
			using (tDBConn.Lock())
			{
				HistoryMetaDataAsset tHistoryMetaDataAsset = AssetManager.history_meta_data_library.get(pMetaType);
				using (ListPool<string> tQueries = new ListPool<string>(AssetManager.graph_time_library.list.Count))
				{
					foreach (GraphTimeAsset tAsset in AssetManager.graph_time_library.list)
					{
						HistoryInterval tInterval = tAsset.interval;
						TableMapping tTableMapping = tDBConn.GetMapping(tHistoryMetaDataAsset.getTableType(tInterval), CreateFlags.None);
						tQueries.Add(string.Format("select \"{0}\" as Scale, count() as Count from {1} where id = {2} GROUP BY id HAVING Count > 0", tAsset.id, tTableMapping.TableName, pID));
					}
					string tQuery = string.Join(" UNION ", tQueries);
					using (ListPool<ValueTuple<string, int>> tDBList = tDBConn.QueryPool(tQuery, Array.Empty<object>()))
					{
						if (tDBList.Count == 0)
						{
							result = new ListPool<GraphTimeScale>();
						}
						else
						{
							using (ListPool<GraphTimeScale> tDBTimeScales = new ListPool<GraphTimeScale>(tDBList.Count))
							{
								foreach (ValueTuple<string, int> ptr in tDBList)
								{
									ValueTuple<string, int> tDBItem = ptr;
									tDBTimeScales.Add(AssetManager.graph_time_library.get(tDBItem.Item1).scale_id);
								}
								ListPool<GraphTimeScale> tTimeScales = new ListPool<GraphTimeScale>(tDBTimeScales.Count);
								GraphTimeScale tMaxTimeScale = tDBTimeScales.Max<GraphTimeScale>();
								for (GraphTimeScale tScale = GraphTimeScale.year_10; tScale <= tMaxTimeScale; tScale++)
								{
									tTimeScales.Add(tScale);
								}
								result = tTimeScales;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060042C4 RID: 17092 RVA: 0x001C50A8 File Offset: 0x001C32A8
		public static ListPool<WorldLogMessage> getWorldLogMessages()
		{
			DBInserter.executeCommands();
			SQLiteConnectionWithLock tDBConn = DBManager.getSyncConnection();
			ListPool<WorldLogMessage> result;
			using (tDBConn.Lock())
			{
				TableMapping tTableMapping = tDBConn.GetMapping<WorldLogMessage>(CreateFlags.None);
				result = tDBConn.QueryPool(string.Format("select * from {0} order by timestamp DESC, ROWID DESC LIMIT {1}", tTableMapping.TableName, 2000), Array.Empty<object>());
			}
			return result;
		}

		// Token: 0x060042C5 RID: 17093 RVA: 0x001C5118 File Offset: 0x001C3318
		public static bool getData(CategoryData pData, NanoObject pObject, HistoryInterval pInterval, HistoryTable pExtraData)
		{
			return DBGetter.getData(pData, pObject.getID(), pObject.getType(), pInterval, pExtraData);
		}

		// Token: 0x060042C6 RID: 17094 RVA: 0x001C5130 File Offset: 0x001C3330
		public static bool getData(CategoryData pData, long pID, string pMetaType, HistoryInterval pInterval, HistoryTable pExtraData)
		{
			SQLiteConnectionWithLock tDBConn = DBManager.getSyncConnection();
			bool result;
			using (tDBConn.Lock())
			{
				HistoryMetaDataAsset tHistoryMetaDataAsset = AssetManager.history_meta_data_library.get(pMetaType);
				TableMapping tTableMapping = tDBConn.GetMapping(tHistoryMetaDataAsset.getTableType(pInterval), CreateFlags.None);
				ListPool<object> tDBList = tDBConn.QueryPool(tTableMapping, "select * from " + tTableMapping.TableName + " where id = ? order by timestamp ASC", new object[]
				{
					pID
				});
				if (tDBList.Count > 0)
				{
					if (((HistoryTable)tDBList.Last<object>()).timestamp < pExtraData.timestamp)
					{
						tDBList.Add(pExtraData);
					}
				}
				else
				{
					tDBList.Add(pExtraData);
				}
				if (pData.db_list != null)
				{
					if (pData.db_list.ValuesEqual(tDBList))
					{
						tDBList.Dispose();
						return false;
					}
					pData.Clear();
				}
				foreach (object ptr in tDBList)
				{
					Dictionary<string, long?> tDBObject = DBGetter.parseValues(ptr, tTableMapping);
					Dictionary<string, long> tNewValue = UnsafeCollectionPool<Dictionary<string, long>, KeyValuePair<string, long>>.Get();
					foreach (string tDictKey in tDBObject.Keys)
					{
						long? tValue = tDBObject[tDictKey];
						if (tValue == null)
						{
							LinkedListNode<Dictionary<string, long>> last = pData.Last;
							tValue = ((last != null) ? new long?(last.Value[tDictKey]) : null);
							if (tValue == null)
							{
								tValue = new long?(0L);
							}
						}
						tNewValue.Add(tDictKey, tValue.Value);
					}
					pData.AddLast(tNewValue);
					UnsafeCollectionPool<Dictionary<string, long?>, KeyValuePair<string, long?>>.Release(tDBObject);
				}
				pData.db_list = tDBList;
				result = true;
			}
			return result;
		}

		// Token: 0x060042C7 RID: 17095 RVA: 0x001C5344 File Offset: 0x001C3544
		public static Dictionary<string, long?> parseValues(object pItem, TableMapping pTableMapping)
		{
			TableMapping.Column[] columns = pTableMapping.Columns;
			Dictionary<string, long?> tDict = UnsafeCollectionPool<Dictionary<string, long?>, KeyValuePair<string, long?>>.Get();
			foreach (TableMapping.Column tCol in columns)
			{
				if (!(tCol.Name == "id"))
				{
					object tValue = tCol.GetValue(pItem);
					if (tValue == null)
					{
						tDict.Add(tCol.Name, null);
					}
					else
					{
						long tValueCast = (long)tValue;
						tDict.Add(tCol.Name, new long?(tValueCast));
					}
				}
			}
			return tDict;
		}
	}
}
