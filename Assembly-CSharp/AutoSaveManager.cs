using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityPools;

// Token: 0x02000217 RID: 535
public static class AutoSaveManager
{
	// Token: 0x0600133F RID: 4927 RVA: 0x000D720C File Offset: 0x000D540C
	public static void update()
	{
		if (AutoSaveManager.lowMemory)
		{
			return;
		}
		if (!Config.autosaves)
		{
			return;
		}
		if (AutoSaveManager._time > 0f)
		{
			AutoSaveManager._time -= Time.deltaTime;
			return;
		}
		if (ScrollWindow.isWindowActive() || ControllableUnit.isControllingUnit())
		{
			AutoSaveManager._time += 10f;
			return;
		}
		AutoSaveManager.autoSave(false, true);
	}

	// Token: 0x06001340 RID: 4928 RVA: 0x000D726C File Offset: 0x000D546C
	public static void autoSave(bool pSkipDelete = false, bool pForce = false)
	{
		if (!pForce)
		{
			if (AutoSaveManager._time > 240f)
			{
				return;
			}
			if (Time.realtimeSinceStartup - Config.LAST_LOAD_TIME < 120f)
			{
				return;
			}
		}
		string tNewSavePath = SaveManager.generateAutosavesPath(Math.Truncate(Epoch.Current()).ToString());
		try
		{
			using (AutoSaveManager.getAutoSaves())
			{
				tNewSavePath = SaveManager.generateAutosavesPath(Math.Truncate(Epoch.Current()).ToString());
				SaveManager.saveWorldToDirectory(tNewSavePath, false, true);
			}
		}
		catch (Exception message)
		{
			Debug.Log("error while auto saving");
			Debug.LogError(message);
			SaveManager.deleteSavePath(tNewSavePath);
		}
		try
		{
			if (!pSkipDelete)
			{
				AutoSaveManager.checkClearSaves();
			}
		}
		catch (Exception message2)
		{
			Debug.Log("Error while clearing saves");
			Debug.LogError(message2);
		}
		AutoSaveManager.resetAutoSaveTimer();
	}

	// Token: 0x06001341 RID: 4929 RVA: 0x000D734C File Offset: 0x000D554C
	private static void checkClearSaves()
	{
		using (ListPool<AutoSaveData> tDatas = AutoSaveManager.getAutoSaves())
		{
			Dictionary<string, ListPool<AutoSaveData>> tAutoSavePerMap = AutoSaveManager.getAutoSavesPerMap(tDatas);
			foreach (ListPool<AutoSaveData> tAutoSaves in tAutoSavePerMap.Values)
			{
				while (tAutoSaves.Count > 5)
				{
					SaveManager.deleteSavePath(tAutoSaves.Pop<AutoSaveData>().path);
				}
				tAutoSaves.Dispose();
			}
			UnsafeCollectionPool<Dictionary<string, ListPool<AutoSaveData>>, KeyValuePair<string, ListPool<AutoSaveData>>>.Release(tAutoSavePerMap);
			if (tDatas.Count > 30)
			{
				using (ListPool<AutoSaveData> tDatas2 = AutoSaveManager.getAutoSaves())
				{
					if (tDatas2.Count > 30)
					{
						for (int i = 30; i < tDatas2.Count; i++)
						{
							SaveManager.deleteSavePath(tDatas2[i].path);
						}
					}
				}
			}
		}
	}

	// Token: 0x06001342 RID: 4930 RVA: 0x000D7444 File Offset: 0x000D5644
	public static void resetAutoSaveTimer()
	{
		AutoSaveManager._time = AutoSaveManager._interval;
	}

	// Token: 0x06001343 RID: 4931 RVA: 0x000D7450 File Offset: 0x000D5650
	public static ListPool<AutoSaveData> getAutoSaves()
	{
		string tAutoSavesPath = SaveManager.generateAutosavesPath("");
		if (!Directory.Exists(tAutoSavesPath))
		{
			Directory.CreateDirectory(tAutoSavesPath);
		}
		ListPool<AutoSaveData> result;
		using (ListPool<string> tPaths = Toolbox.getDirectories(tAutoSavesPath))
		{
			ListPool<AutoSaveData> tDatas = new ListPool<AutoSaveData>(tPaths.Count);
			foreach (string ptr in tPaths)
			{
				string tPath = ptr;
				MapMetaData tMeta = SaveManager.getMetaFor(tPath);
				if (tMeta == null)
				{
					SaveManager.deleteSavePath(tPath);
				}
				else
				{
					tDatas.Add(new AutoSaveData
					{
						name = tMeta.mapStats.name,
						path = tPath,
						timestamp = tMeta.timestamp
					});
				}
			}
			tDatas.Sort(new Comparison<AutoSaveData>(AutoSaveManager.sorter));
			result = tDatas;
		}
		return result;
	}

	// Token: 0x06001344 RID: 4932 RVA: 0x000D7540 File Offset: 0x000D5740
	public static Dictionary<string, ListPool<AutoSaveData>> getAutoSavesPerMap(ListPool<AutoSaveData> pDatas)
	{
		Dictionary<string, ListPool<AutoSaveData>> tAutoSavesPerMap = UnsafeCollectionPool<Dictionary<string, ListPool<AutoSaveData>>, KeyValuePair<string, ListPool<AutoSaveData>>>.Get();
		for (int i = 0; i < pDatas.Count; i++)
		{
			AutoSaveData tData = pDatas[i];
			if (!tAutoSavesPerMap.ContainsKey(tData.name))
			{
				tAutoSavesPerMap[tData.name] = new ListPool<AutoSaveData>();
			}
			tAutoSavesPerMap[tData.name].Add(tData);
		}
		return tAutoSavesPerMap;
	}

	// Token: 0x06001345 RID: 4933 RVA: 0x000D759E File Offset: 0x000D579E
	public static int sorter(AutoSaveData o1, AutoSaveData o2)
	{
		return o2.timestamp.CompareTo(o1.timestamp);
	}

	// Token: 0x06001346 RID: 4934 RVA: 0x000D75B4 File Offset: 0x000D57B4
	internal static void OnLowMemory()
	{
		if (!Config.game_loaded || SmoothLoader.isLoading())
		{
			return;
		}
		AutoSaveManager.low_mem_count++;
		if (AutoSaveManager.low_mem_count < 3)
		{
			return;
		}
		AutoSaveManager.resetAutoSaveTimer();
		int tTimeNow = (int)Epoch.Current();
		if (AutoSaveManager.lowMemory && AutoSaveManager.lastClear - tTimeNow < 30)
		{
			return;
		}
		AutoSaveManager.lastClear = tTimeNow;
		if (!AutoSaveManager.lowMemory)
		{
			Debug.Log("Running out of memory!");
			WorldTip.showNow("Low on memory(RAM)! Disabling auto-saves", false, "top", 3f, "#F3961F");
		}
		else
		{
			Debug.Log("Running out of memory!");
			WorldTip.showNow("Your device is low on memory(RAM)", false, "top", 3f, "#F3961F");
		}
		AutoSaveManager.lowMemory = true;
		Config.forceGC("low memory", false);
	}

	// Token: 0x04001167 RID: 4455
	private static float _time = 300f;

	// Token: 0x04001168 RID: 4456
	private static float _interval = 300f;

	// Token: 0x04001169 RID: 4457
	private static bool lowMemory = false;

	// Token: 0x0400116A RID: 4458
	private static int lastClear = 0;

	// Token: 0x0400116B RID: 4459
	private static int low_mem_count = 0;
}
