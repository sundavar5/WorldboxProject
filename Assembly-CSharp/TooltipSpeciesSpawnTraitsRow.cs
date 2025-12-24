using System;
using System.Collections.Generic;

// Token: 0x0200078D RID: 1933
public class TooltipSpeciesSpawnTraitsRow : TooltipTraitsRow<SubspeciesTrait>
{
	// Token: 0x1700039B RID: 923
	// (get) Token: 0x06003D6B RID: 15723 RVA: 0x001AE260 File Offset: 0x001AC460
	protected override IReadOnlyCollection<SubspeciesTrait> traits_hashset
	{
		get
		{
			return this.loadTraitsFromPowerAsset();
		}
	}

	// Token: 0x06003D6C RID: 15724 RVA: 0x001AE268 File Offset: 0x001AC468
	private HashSet<SubspeciesTrait> loadTraitsFromPowerAsset()
	{
		string tActorAssetID;
		if (this.tooltip_data.power != null)
		{
			tActorAssetID = this.tooltip_data.power.getActorAssetID();
		}
		else
		{
			tActorAssetID = this.tooltip_data.tip_name;
		}
		ActorAsset tAsset = AssetManager.actor_library.get(tActorAssetID);
		if (tAsset == null)
		{
			return null;
		}
		return tAsset.getDefaultSubspeciesTraits();
	}
}
