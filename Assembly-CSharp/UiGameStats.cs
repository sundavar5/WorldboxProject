using System;

// Token: 0x0200060A RID: 1546
public class UiGameStats : StatisticsRows
{
	// Token: 0x060032BA RID: 12986 RVA: 0x0018046C File Offset: 0x0017E66C
	protected override void init()
	{
		foreach (StatisticsAsset tAsset in AssetManager.statistics_library.list)
		{
			if (tAsset.is_game_statistics)
			{
				base.addStatRow(tAsset);
			}
		}
	}
}
