using System;

// Token: 0x02000741 RID: 1857
public class ReligionTraitButton : TraitButton<ReligionTrait>
{
	// Token: 0x06003AF1 RID: 15089 RVA: 0x0019F968 File Offset: 0x0019DB68
	internal override void load(string pTraitID)
	{
		ReligionTrait tTrait = AssetManager.religion_traits.get(pTraitID);
		this.load(tTrait);
	}

	// Token: 0x06003AF2 RID: 15090 RVA: 0x0019F988 File Offset: 0x0019DB88
	protected override void startSignal()
	{
		AchievementLibrary.trait_explorer_religion.checkBySignal(null);
	}

	// Token: 0x17000355 RID: 853
	// (get) Token: 0x06003AF3 RID: 15091 RVA: 0x0019F995 File Offset: 0x0019DB95
	protected override string tooltip_type
	{
		get
		{
			return "religion_trait";
		}
	}

	// Token: 0x06003AF4 RID: 15092 RVA: 0x0019F99C File Offset: 0x0019DB9C
	protected override TooltipData tooltipDataBuilder()
	{
		return new TooltipData
		{
			religion_trait = this.augmentation_asset
		};
	}
}
