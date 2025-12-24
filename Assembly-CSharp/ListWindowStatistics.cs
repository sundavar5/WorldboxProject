using System;

// Token: 0x020007D9 RID: 2009
public class ListWindowStatistics : StatisticsRows
{
	// Token: 0x06003F5D RID: 16221 RVA: 0x001B535C File Offset: 0x001B355C
	protected override void init()
	{
		foreach (StatisticsAsset tAsset in AssetManager.statistics_library.list)
		{
			if (!tAsset.list_window_meta_type.isNone() && this.meta_type == tAsset.list_window_meta_type)
			{
				base.addStatRow(tAsset);
			}
		}
	}

	// Token: 0x04002E0A RID: 11786
	public MetaType meta_type;
}
