using System;

// Token: 0x02000025 RID: 37
public static class BaseStatsExtension
{
	// Token: 0x060001EE RID: 494 RVA: 0x0000F431 File Offset: 0x0000D631
	public static bool isEmpty(this BaseStats pBaseStats)
	{
		return pBaseStats == null || (!pBaseStats.hasTags() && !pBaseStats.hasStats());
	}
}
