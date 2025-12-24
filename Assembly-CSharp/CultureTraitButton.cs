using System;

// Token: 0x02000679 RID: 1657
public class CultureTraitButton : TraitButton<CultureTrait>
{
	// Token: 0x06003561 RID: 13665 RVA: 0x00188834 File Offset: 0x00186A34
	internal override void load(string pTraitID)
	{
		CultureTrait tTrait = AssetManager.culture_traits.get(pTraitID);
		this.load(tTrait);
	}

	// Token: 0x06003562 RID: 13666 RVA: 0x00188854 File Offset: 0x00186A54
	protected override void startSignal()
	{
		AchievementLibrary.trait_explorer_culture.checkBySignal(null);
	}

	// Token: 0x170002F1 RID: 753
	// (get) Token: 0x06003563 RID: 13667 RVA: 0x00188861 File Offset: 0x00186A61
	protected override string tooltip_type
	{
		get
		{
			return "culture_trait";
		}
	}

	// Token: 0x06003564 RID: 13668 RVA: 0x00188868 File Offset: 0x00186A68
	protected override TooltipData tooltipDataBuilder()
	{
		return new TooltipData
		{
			culture_trait = this.augmentation_asset
		};
	}
}
