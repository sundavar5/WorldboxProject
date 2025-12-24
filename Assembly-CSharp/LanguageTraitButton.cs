using System;

// Token: 0x02000711 RID: 1809
public class LanguageTraitButton : TraitButton<LanguageTrait>
{
	// Token: 0x060039CD RID: 14797 RVA: 0x0019B120 File Offset: 0x00199320
	internal override void load(string pTraitID)
	{
		LanguageTrait tTrait = AssetManager.language_traits.get(pTraitID);
		this.load(tTrait);
	}

	// Token: 0x060039CE RID: 14798 RVA: 0x0019B140 File Offset: 0x00199340
	protected override void startSignal()
	{
		AchievementLibrary.trait_explorer_language.checkBySignal(null);
	}

	// Token: 0x1700033A RID: 826
	// (get) Token: 0x060039CF RID: 14799 RVA: 0x0019B14D File Offset: 0x0019934D
	protected override string tooltip_type
	{
		get
		{
			return "language_trait";
		}
	}

	// Token: 0x060039D0 RID: 14800 RVA: 0x0019B154 File Offset: 0x00199354
	protected override TooltipData tooltipDataBuilder()
	{
		return new TooltipData
		{
			language_trait = this.augmentation_asset
		};
	}
}
