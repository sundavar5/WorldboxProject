using System;

// Token: 0x020006FA RID: 1786
public class KingdomTraitButton : TraitButton<KingdomTrait>
{
	// Token: 0x06003942 RID: 14658 RVA: 0x00198378 File Offset: 0x00196578
	internal override void load(string pTraitID)
	{
		KingdomTrait tTrait = AssetManager.kingdoms_traits.get(pTraitID);
		this.load(tTrait);
	}

	// Token: 0x1700032C RID: 812
	// (get) Token: 0x06003943 RID: 14659 RVA: 0x00198398 File Offset: 0x00196598
	protected override string tooltip_type
	{
		get
		{
			return "kingdom_trait";
		}
	}

	// Token: 0x06003944 RID: 14660 RVA: 0x0019839F File Offset: 0x0019659F
	protected override TooltipData tooltipDataBuilder()
	{
		return new TooltipData
		{
			kingdom_trait = this.augmentation_asset
		};
	}
}
