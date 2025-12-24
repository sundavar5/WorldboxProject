using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004FE RID: 1278
public class Bench
{
	// Token: 0x06002A5B RID: 10843 RVA: 0x0014E394 File Offset: 0x0014C594
	public static void update()
	{
		if (!Bench.bench_enabled)
		{
			return;
		}
		Bench.finishSplitBenchmarkGroupAI();
		Bench.finishSplitBenchmarkGroup("effects_traits");
		Bench.finishSplitBenchmarkGroup("effects_items");
		if (Bench._timer_flatten > 0f)
		{
			Bench._timer_flatten -= Time.deltaTime;
			return;
		}
		Bench._timer_flatten = 0.05f;
		Bench.flatten("effects_traits");
		Bench.flatten("effects_items");
	}

	// Token: 0x06002A5C RID: 10844 RVA: 0x0014E400 File Offset: 0x0014C600
	private static void flatten(string pID)
	{
		BenchmarkGroup tGroup;
		if (Bench.dict.TryGetValue(pID, out tGroup))
		{
			tGroup.flatten();
		}
	}

	// Token: 0x06002A5D RID: 10845 RVA: 0x0014E422 File Offset: 0x0014C622
	private static void finishSplitBenchmarkGroupAI()
	{
		DebugConfig.isOn(DebugOption.BenchAiEnabled);
	}

	// Token: 0x06002A5E RID: 10846 RVA: 0x0014E430 File Offset: 0x0014C630
	private static void finishSplitBenchmarkGroup(string pID)
	{
		BenchmarkGroup tGroup;
		if (!Bench.dict.TryGetValue(pID, out tGroup))
		{
			return;
		}
		double tTotal = 0.0;
		foreach (ToolBenchmarkData tData in tGroup.dict_data.Values)
		{
			tTotal += tData.latest_result;
			tData.saveAverageCounter();
		}
		Bench.benchSaveSplit(pID, tTotal, 1, "game_total");
	}

	// Token: 0x06002A5F RID: 10847 RVA: 0x0014E4B8 File Offset: 0x0014C6B8
	public static void saveAverageCounter(string pID, string pGroup)
	{
		Bench.get(pID, pGroup, true).saveAverageCounter();
	}

	// Token: 0x06002A60 RID: 10848 RVA: 0x0014E4C8 File Offset: 0x0014C6C8
	public static BenchmarkGroup getGroup(string pID)
	{
		if (Bench.dict.ContainsKey(pID))
		{
			return Bench.dict[pID];
		}
		BenchmarkGroup tGroup = new BenchmarkGroup();
		tGroup.id = pID;
		Bench.dict.Add(pID, tGroup);
		return tGroup;
	}

	// Token: 0x06002A61 RID: 10849 RVA: 0x0014E508 File Offset: 0x0014C708
	private static ToolBenchmarkData get(string pID, string pGroupID = "main", bool pNew = true)
	{
		BenchmarkGroup tGroup;
		if (!Bench.dict.TryGetValue(pGroupID, out tGroup))
		{
			tGroup = new BenchmarkGroup();
			tGroup.id = pGroupID;
			Bench.dict.Add(pGroupID, tGroup);
		}
		ToolBenchmarkData tData;
		if (!tGroup.dict_data.TryGetValue(pID, out tData) && pNew)
		{
			tData = new ToolBenchmarkData();
			tData.id = pID;
			tGroup.dict_data.Add(pID, tData);
		}
		return tData;
	}

	// Token: 0x06002A62 RID: 10850 RVA: 0x0014E570 File Offset: 0x0014C770
	public static void clearBenchmarkEntrySkipMultiple(string pGroupID = "main", params string[] pEntries)
	{
		foreach (string pID in pEntries)
		{
			Bench.bench(pID, pGroupID, false);
			Bench.benchEnd(pID, pGroupID, false, 0L, false);
		}
	}

	// Token: 0x06002A63 RID: 10851 RVA: 0x0014E5A4 File Offset: 0x0014C7A4
	public static void clearBenchmarkEntrySkip(string pID, string pGroupID = "main")
	{
		Bench.bench(pID, pGroupID, false);
		Bench.benchEnd(pID, pGroupID, false, 0L, false);
	}

	// Token: 0x06002A64 RID: 10852 RVA: 0x0014E5BC File Offset: 0x0014C7BC
	public static double bench(string pID, string pGroupID = "main", bool pForce = false)
	{
		if (!Bench.bench_enabled && !pForce)
		{
			return 0.0;
		}
		ToolBenchmarkData toolBenchmarkData = Bench.get(pID, pGroupID, true);
		double tTime = Time.realtimeSinceStartupAsDouble;
		toolBenchmarkData.start(tTime);
		return tTime;
	}

	// Token: 0x06002A65 RID: 10853 RVA: 0x0014E5F4 File Offset: 0x0014C7F4
	public static double benchEnd(string pID, string pGroupID = "main", bool pSaveCounter = false, long pCounter = 0L, bool pForce = false)
	{
		if (!Bench.bench_enabled && !pForce)
		{
			return 0.0;
		}
		ToolBenchmarkData tData = Bench.get(pID, pGroupID, true);
		double tTime = Time.realtimeSinceStartupAsDouble - tData.latest_time;
		tData.end(tTime);
		if (pSaveCounter)
		{
			tData.newCount(pCounter);
			tData.saveAverageCounter();
		}
		return tTime;
	}

	// Token: 0x06002A66 RID: 10854 RVA: 0x0014E643 File Offset: 0x0014C843
	public static void benchSet(string pID, double pVal, int pCounter, string pGroupID = "main")
	{
		if (!Bench.bench_enabled)
		{
			return;
		}
		Bench.benchSave(pID, pVal, pCounter, pGroupID);
		Bench.saveAverageCounter(pID, pGroupID);
	}

	// Token: 0x06002A67 RID: 10855 RVA: 0x0014E65E File Offset: 0x0014C85E
	public static void benchSetValue(string pID, int pValue, string pGroupID = "main")
	{
		if (!Bench.bench_enabled)
		{
			return;
		}
		Bench.get(pID, pGroupID, true).newValue(pValue);
	}

	// Token: 0x06002A68 RID: 10856 RVA: 0x0014E676 File Offset: 0x0014C876
	public static int getBenchValue(string pID, string pGroupID = "main")
	{
		if (!Bench.bench_enabled)
		{
			return 0;
		}
		return (int)Bench.get(pID, pGroupID, true).debug_value;
	}

	// Token: 0x06002A69 RID: 10857 RVA: 0x0014E68F File Offset: 0x0014C88F
	public static double benchSave(string pID, double pValue, int pCounter, string pGroupID = "main")
	{
		if (!Bench.bench_enabled)
		{
			return 0.0;
		}
		ToolBenchmarkData toolBenchmarkData = Bench.get(pID, pGroupID, true);
		toolBenchmarkData.end(pValue);
		toolBenchmarkData.newCount((long)pCounter);
		return pValue;
	}

	// Token: 0x06002A6A RID: 10858 RVA: 0x0014E6B9 File Offset: 0x0014C8B9
	public static double benchSaveSplit(string pID, double pValue, int pCounter, string pGroupID = "main")
	{
		if (!Bench.bench_enabled)
		{
			return 0.0;
		}
		ToolBenchmarkData toolBenchmarkData = Bench.get(pID, pGroupID, true);
		toolBenchmarkData.end(pValue);
		toolBenchmarkData.newCount((long)pCounter);
		return pValue;
	}

	// Token: 0x06002A6B RID: 10859 RVA: 0x0014E6E4 File Offset: 0x0014C8E4
	public static string getBenchResult(string pID, string pGroupID = "main", bool pAverage = true)
	{
		return Bench.getBenchResultAsDouble(pID, pGroupID, pAverage).ToString("##,0.#######");
	}

	// Token: 0x06002A6C RID: 10860 RVA: 0x0014E708 File Offset: 0x0014C908
	public static double getBenchResultAsDouble(string pID, string pGroupID = "main", bool pAverage = true)
	{
		ToolBenchmarkData tData = Bench.get(pID, pGroupID, false);
		if (tData == null)
		{
			return -1.0;
		}
		if (pAverage)
		{
			return tData.getAverage();
		}
		return tData.latest_result;
	}

	// Token: 0x06002A6D RID: 10861 RVA: 0x0014E73C File Offset: 0x0014C93C
	public static string printableBenchResults(string pGroupID = "main", bool pAverage = false, params string[] pID)
	{
		double[] tResults = new double[pID.Length];
		double tMax = 0.0;
		double tMin = double.MaxValue;
		for (int i = 0; i < pID.Length; i++)
		{
			tResults[i] = Bench.getBenchResultAsDouble(pID[i], pGroupID, pAverage);
			if (tResults[i] > tMax)
			{
				tMax = tResults[i];
			}
			if (tResults[i] < tMin)
			{
				tMin = tResults[i];
			}
		}
		Array.Sort<double, string>(tResults, pID);
		string result;
		using (ListPool<string[]> tRow = new ListPool<string[]>())
		{
			tRow.Add(new string[]
			{
				"ID",
				"TIME",
				"PERCENT",
				"WINNER",
				"BAR"
			});
			tRow.Add(new string[0]);
			for (int j = 0; j < pID.Length; j++)
			{
				double tPercent = tResults[j] / tMax;
				bool tWinner = tResults[j].Equals(tMin);
				bool tLoser = tResults[j].Equals(tMax);
				string tPrefix = "";
				string tSuffix = "";
				string tBar = "";
				int tBars = (int)(tPercent * 10.0);
				for (int k = 0; k < tBars; k++)
				{
					tBar += "■";
				}
				tBar = Toolbox.fillRight(tBar, 10, ' ');
				if (tWinner || tLoser)
				{
					if (tWinner)
					{
						tPrefix = "<color=green>";
					}
					if (tLoser)
					{
						tPrefix = "<color=red>";
					}
					tSuffix = "</color>";
				}
				string tID = tPrefix + pID[j] + tSuffix;
				string tResult = tPrefix + tResults[j].ToString("F7") + tSuffix;
				string tPercentStr = tPrefix + tPercent.ToString("P0") + tSuffix;
				string tWinnerStr = tPrefix + (tWinner ? "WINNER" : (tLoser ? "SLOWEST" : "")) + tSuffix;
				string tBarStr = tPrefix + tBar + tSuffix;
				tRow.Add(new string[]
				{
					tID,
					tResult,
					tPercentStr,
					tWinnerStr,
					tBarStr
				});
			}
			result = Toolbox.printRows(tRow, "right", false);
		}
		return result;
	}

	// Token: 0x06002A6E RID: 10862 RVA: 0x0014E974 File Offset: 0x0014CB74
	public static void printBenchResult(string pID, string pGroupID = "main", bool pAverage = false)
	{
		double tResultFloat = Bench.getBenchResultAsDouble(pID, pGroupID, pAverage);
		string tResult = tResultFloat.ToString("##,0.##########");
		if (tResultFloat > 0.3)
		{
			tResult = "<color=red>" + tResult + "</color>";
		}
		else if (tResultFloat > 0.1)
		{
			tResult = "<color=yellow>" + tResult + "</color>";
		}
		Debug.Log("#benchmark: <color=white>" + pID + "</color>: " + tResult);
	}

	// Token: 0x04001F84 RID: 8068
	public static bool bench_enabled = false;

	// Token: 0x04001F85 RID: 8069
	public static bool bench_ai_enabled = false;

	// Token: 0x04001F86 RID: 8070
	private static Dictionary<string, BenchmarkGroup> dict = new Dictionary<string, BenchmarkGroup>();

	// Token: 0x04001F87 RID: 8071
	private static float _timer_flatten = 0f;
}
