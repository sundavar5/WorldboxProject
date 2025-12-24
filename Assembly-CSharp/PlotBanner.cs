using System;

// Token: 0x02000723 RID: 1827
public class PlotBanner : BannerGeneric<Plot, PlotData>
{
	// Token: 0x17000344 RID: 836
	// (get) Token: 0x06003A4C RID: 14924 RVA: 0x0019D6D9 File Offset: 0x0019B8D9
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Plot;
		}
	}

	// Token: 0x17000345 RID: 837
	// (get) Token: 0x06003A4D RID: 14925 RVA: 0x0019D6DD File Offset: 0x0019B8DD
	protected override string tooltip_id
	{
		get
		{
			return "plot";
		}
	}

	// Token: 0x06003A4E RID: 14926 RVA: 0x0019D6E4 File Offset: 0x0019B8E4
	protected override TooltipData getTooltipData()
	{
		TooltipData tooltipData = base.getTooltipData();
		tooltipData.plot = this.meta_object;
		return tooltipData;
	}

	// Token: 0x06003A4F RID: 14927 RVA: 0x0019D6F8 File Offset: 0x0019B8F8
	protected override void setupBanner()
	{
		base.setupBanner();
		BaseUnlockableAsset asset = this.meta_object.getAsset();
		string tPathBackground = "plots/backgrounds/plot_background_0";
		string tPathIcon = asset.path_icon;
		this.part_background.sprite = SpriteTextureLoader.getSprite(tPathBackground);
		this.part_icon.sprite = SpriteTextureLoader.getSprite(tPathIcon);
	}
}
