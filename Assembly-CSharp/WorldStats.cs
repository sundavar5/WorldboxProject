using System;
using UnityEngine;

// Token: 0x02000618 RID: 1560
public class WorldStats : StatisticsRows
{
	// Token: 0x06003334 RID: 13108 RVA: 0x00182258 File Offset: 0x00180458
	protected override void init()
	{
		bool tFilterTabs = this._tab_type > WorldStatsTabs.Everything;
		foreach (StatisticsAsset tAsset in AssetManager.statistics_library.list)
		{
			if (tAsset.is_world_statistics && (!tFilterTabs || tAsset.world_stats_tabs.HasFlag(this._tab_type)))
			{
				base.addStatRow(tAsset);
			}
		}
	}

	// Token: 0x040026CE RID: 9934
	[SerializeField]
	private WorldStatsTabs _tab_type;
}
