using System;
using System.Collections.Generic;
using UnityEngine;
using UnityPools;

// Token: 0x020006BC RID: 1724
public static class GraphHelpers
{
	// Token: 0x06003759 RID: 14169 RVA: 0x0018FE1D File Offset: 0x0018E01D
	public static string getCategoryName(string pCategory)
	{
		if (!pCategory.Contains('|'))
		{
			return pCategory;
		}
		return pCategory.Split('|', StringSplitOptions.None)[0];
	}

	// Token: 0x0600375A RID: 14170 RVA: 0x0018FE38 File Offset: 0x0018E038
	public static ListPool<string> bestCategories(Dictionary<string, MinMax> pCategoryStats)
	{
		Dictionary<string, AvgStats> tCategoryStats = UnsafeCollectionPool<Dictionary<string, AvgStats>, KeyValuePair<string, AvgStats>>.Get();
		foreach (KeyValuePair<string, MinMax> tEntry in pCategoryStats)
		{
			string tCategoryName = GraphHelpers.getCategoryName(tEntry.Key);
			MinMax tMinMax = tEntry.Value;
			AvgStats tStats;
			if (!tCategoryStats.TryGetValue(tCategoryName, out tStats))
			{
				tStats = new AvgStats(0.0, 0, tCategoryName);
			}
			tCategoryStats[tCategoryName] = tStats.add((double)tMinMax.max);
		}
		ListPool<string> result;
		using (ListPool<AvgStats> tSortedCategories = new ListPool<AvgStats>(tCategoryStats.Values))
		{
			UnsafeCollectionPool<Dictionary<string, AvgStats>, KeyValuePair<string, AvgStats>>.Release(tCategoryStats);
			tSortedCategories.Sort(delegate(AvgStats a, AvgStats b)
			{
				int tCountComparison = b.count.CompareTo(a.count);
				if (tCountComparison == 0)
				{
					return b.avg.CompareTo(a.avg);
				}
				return tCountComparison;
			});
			int tLimit = Math.Min(3, tSortedCategories.Count);
			ListPool<string> tTopThreeCategories = new ListPool<string>(tLimit);
			for (int i = 0; i < tLimit; i++)
			{
				if (i <= 0 || (tSortedCategories[i].avg > 3.0 && tSortedCategories[i].count >= tSortedCategories[0].count))
				{
					tTopThreeCategories.Add(tSortedCategories[i].name);
				}
			}
			result = tTopThreeCategories;
		}
		return result;
	}

	// Token: 0x0600375B RID: 14171 RVA: 0x0018FF98 File Offset: 0x0018E198
	public static string horizontalFormatYears(double pValue, int pDigits)
	{
		return Toolbox.formatNumber((long)(pValue - (double)Date.getCurrentYear()) * -1L) + "\n" + pValue.ToText();
	}

	// Token: 0x0600375C RID: 14172 RVA: 0x0018FFBC File Offset: 0x0018E1BC
	public static string verticalFormat(double pValue, int pDigits)
	{
		MinMax tMinMax = GraphController.min_max;
		string tResult;
		if (Math.Abs(pValue) < 1000.0)
		{
			tResult = pValue.ToString("N" + pDigits.ToString());
		}
		else
		{
			tResult = Toolbox.formatNumber((long)pValue);
		}
		if (pValue == 0.0)
		{
			return Toolbox.coloredText(tResult, "#FFBC66", false);
		}
		if (pValue < 0.0)
		{
			string tColor = Toolbox.colorBetween(pValue, (double)tMinMax.min, 0.0, "#FF637D", "#FFBC66");
			return Toolbox.coloredText(tResult, tColor, false);
		}
		string tColor2 = Toolbox.colorBetween(pValue, 0.0, (double)tMinMax.max, "#FFBC66", "#F3961F");
		return Toolbox.coloredText(tResult, tColor2, false);
	}

	// Token: 0x0600375D RID: 14173 RVA: 0x00190080 File Offset: 0x0018E280
	public static long calculateNiceMaxAxisSize(double pLargestValue)
	{
		if (pLargestValue < 5.0)
		{
			return 5L;
		}
		if (pLargestValue < 8.0)
		{
			return 8L;
		}
		if (pLargestValue < 10.0)
		{
			return 10L;
		}
		if (pLargestValue < 20.0)
		{
			return 20L;
		}
		if (pLargestValue < 30.0)
		{
			return 30L;
		}
		if (pLargestValue < 40.0)
		{
			return 40L;
		}
		if (pLargestValue < 50.0)
		{
			return 50L;
		}
		if (pLargestValue < 60.0)
		{
			return 60L;
		}
		if (pLargestValue < 80.0)
		{
			return 80L;
		}
		if (pLargestValue < 100.0)
		{
			return 100L;
		}
		if (pLargestValue < 120.0)
		{
			return 120L;
		}
		if (pLargestValue < 140.0)
		{
			return 140L;
		}
		if (pLargestValue < 160.0)
		{
			return 160L;
		}
		if (pLargestValue < 180.0)
		{
			return 180L;
		}
		if (pLargestValue < 200.0)
		{
			return 200L;
		}
		if (pLargestValue < 240.0)
		{
			return 240L;
		}
		if (pLargestValue < 280.0)
		{
			return 280L;
		}
		if (pLargestValue < 300.0)
		{
			return 300L;
		}
		if (pLargestValue < 340.0)
		{
			return 340L;
		}
		if (pLargestValue < 380.0)
		{
			return 380L;
		}
		if (pLargestValue < 400.0)
		{
			return 400L;
		}
		if (pLargestValue < 500.0)
		{
			return 500L;
		}
		if (pLargestValue < 600.0)
		{
			return 600L;
		}
		if (pLargestValue < 700.0)
		{
			return 700L;
		}
		if (pLargestValue < 800.0)
		{
			return 800L;
		}
		if (pLargestValue < 900.0)
		{
			return 900L;
		}
		if (pLargestValue < 1000.0)
		{
			return 1000L;
		}
		double tOrderOfMagnitude = (double)Mathf.Pow(10f, Mathf.Floor(Mathf.Log10((float)pLargestValue)));
		double tFractionOfMagnitude = pLargestValue / tOrderOfMagnitude;
		double tNiceFraction;
		if (tFractionOfMagnitude <= 1.5)
		{
			tNiceFraction = 1.5;
		}
		else if (tFractionOfMagnitude <= 2.0)
		{
			tNiceFraction = 2.0;
		}
		else if (tFractionOfMagnitude <= 3.0)
		{
			tNiceFraction = 3.0;
		}
		else if (tFractionOfMagnitude <= 5.0)
		{
			tNiceFraction = 5.0;
		}
		else
		{
			tNiceFraction = 10.0;
		}
		return (long)(tNiceFraction * tOrderOfMagnitude);
	}

	// Token: 0x0600375E RID: 14174 RVA: 0x001902F5 File Offset: 0x0018E4F5
	public static int findVerticalDivision(long pValue)
	{
		if (GraphHelpers.canDivideIntoWholeNumbers(pValue, 4))
		{
			return 4;
		}
		if (GraphHelpers.canDivideIntoWholeNumbers(pValue, 5))
		{
			return 5;
		}
		if (GraphHelpers.canDivideIntoWholeNumbers(pValue, 3))
		{
			return 3;
		}
		if (GraphHelpers.canDivideIntoWholeNumbers(pValue, 6))
		{
			return 6;
		}
		if (GraphHelpers.canDivideIntoWholeNumbers(pValue, 2))
		{
			return 2;
		}
		return 4;
	}

	// Token: 0x0600375F RID: 14175 RVA: 0x00190330 File Offset: 0x0018E530
	private static bool canDivideIntoWholeNumbers(long pTotalValue, int pSegments)
	{
		for (int tStep = 1; tStep <= pSegments; tStep++)
		{
			if ((double)pTotalValue / (double)pSegments * (double)tStep % 1.0 > 0.0)
			{
				return false;
			}
		}
		return true;
	}
}
