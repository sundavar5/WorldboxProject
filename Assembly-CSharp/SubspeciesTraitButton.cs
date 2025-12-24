using System;

// Token: 0x0200076E RID: 1902
public class SubspeciesTraitButton : TraitButton<SubspeciesTrait>
{
	// Token: 0x06003C4F RID: 15439 RVA: 0x001A34B0 File Offset: 0x001A16B0
	internal override void load(string pTraitID)
	{
		SubspeciesTrait tTrait = AssetManager.subspecies_traits.get(pTraitID);
		this.load(tTrait);
	}

	// Token: 0x06003C50 RID: 15440 RVA: 0x001A34D0 File Offset: 0x001A16D0
	protected override void startSignal()
	{
		AchievementLibrary.trait_explorer_subspecies.checkBySignal(null);
	}

	// Token: 0x17000388 RID: 904
	// (get) Token: 0x06003C51 RID: 15441 RVA: 0x001A34DD File Offset: 0x001A16DD
	protected override string tooltip_type
	{
		get
		{
			return "subspecies_trait";
		}
	}

	// Token: 0x06003C52 RID: 15442 RVA: 0x001A34E4 File Offset: 0x001A16E4
	protected override TooltipData tooltipDataBuilder()
	{
		return new TooltipData
		{
			subspecies_trait = this.augmentation_asset
		};
	}
}
