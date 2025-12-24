using System;

// Token: 0x0200073A RID: 1850
public class ReligionBanner : BannerGeneric<Religion, ReligionData>
{
	// Token: 0x1700034E RID: 846
	// (get) Token: 0x06003AD4 RID: 15060 RVA: 0x0019F659 File Offset: 0x0019D859
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Religion;
		}
	}

	// Token: 0x1700034F RID: 847
	// (get) Token: 0x06003AD5 RID: 15061 RVA: 0x0019F65C File Offset: 0x0019D85C
	protected override string tooltip_id
	{
		get
		{
			return "religion";
		}
	}

	// Token: 0x06003AD6 RID: 15062 RVA: 0x0019F663 File Offset: 0x0019D863
	protected override TooltipData getTooltipData()
	{
		TooltipData tooltipData = base.getTooltipData();
		tooltipData.religion = this.meta_object;
		return tooltipData;
	}

	// Token: 0x06003AD7 RID: 15063 RVA: 0x0019F678 File Offset: 0x0019D878
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
