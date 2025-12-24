using System;

// Token: 0x0200070A RID: 1802
public class LanguageBanner : BannerGeneric<Language, LanguageData>
{
	// Token: 0x17000333 RID: 819
	// (get) Token: 0x060039B0 RID: 14768 RVA: 0x0019AE0C File Offset: 0x0019900C
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Language;
		}
	}

	// Token: 0x17000334 RID: 820
	// (get) Token: 0x060039B1 RID: 14769 RVA: 0x0019AE0F File Offset: 0x0019900F
	protected override string tooltip_id
	{
		get
		{
			return "language";
		}
	}

	// Token: 0x060039B2 RID: 14770 RVA: 0x0019AE16 File Offset: 0x00199016
	protected override TooltipData getTooltipData()
	{
		TooltipData tooltipData = base.getTooltipData();
		tooltipData.language = this.meta_object;
		return tooltipData;
	}

	// Token: 0x060039B3 RID: 14771 RVA: 0x0019AE2C File Offset: 0x0019902C
	protected override void setupBanner()
	{
		base.setupBanner();
		this.part_background.sprite = this.meta_object.getBackgroundSprite();
		this.part_icon.sprite = this.meta_object.getIconSprite();
		ColorAsset tColorAsset = this.meta_object.getColor();
		this.part_background.color = tColorAsset.getColorMainSecond();
		this.part_icon.color = tColorAsset.getColorBanner();
	}
}
