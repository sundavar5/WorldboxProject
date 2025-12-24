using System;

// Token: 0x02000489 RID: 1161
internal static class StatsHelper
{
	// Token: 0x060027DE RID: 10206 RVA: 0x00141608 File Offset: 0x0013F808
	public static string getStatistic(string statName)
	{
		StatisticsAsset tAsset = AssetManager.statistics_library.get(statName);
		if (tAsset != null && tAsset.string_action != null)
		{
			return tAsset.string_action(tAsset);
		}
		return StatsHelper.getStat(statName).ToString() ?? "";
	}

	// Token: 0x060027DF RID: 10207 RVA: 0x00141650 File Offset: 0x0013F850
	public static long getStat(string statName)
	{
		StatisticsAsset tAsset = AssetManager.statistics_library.get(statName);
		if (tAsset != null && tAsset.long_action != null)
		{
			return tAsset.long_action(tAsset);
		}
		return 0L;
	}
}
