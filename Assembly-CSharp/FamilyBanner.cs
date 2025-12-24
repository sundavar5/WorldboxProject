using System;

// Token: 0x0200068E RID: 1678
public class FamilyBanner : BannerGeneric<Family, FamilyData>
{
	// Token: 0x170002FD RID: 765
	// (get) Token: 0x060035C0 RID: 13760 RVA: 0x001898E7 File Offset: 0x00187AE7
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Family;
		}
	}

	// Token: 0x170002FE RID: 766
	// (get) Token: 0x060035C1 RID: 13761 RVA: 0x001898EA File Offset: 0x00187AEA
	protected override string tooltip_id
	{
		get
		{
			return "family";
		}
	}

	// Token: 0x060035C2 RID: 13762 RVA: 0x001898F1 File Offset: 0x00187AF1
	protected override TooltipData getTooltipData()
	{
		TooltipData tooltipData = base.getTooltipData();
		tooltipData.family = this.meta_object;
		return tooltipData;
	}

	// Token: 0x060035C3 RID: 13763 RVA: 0x00189908 File Offset: 0x00187B08
	protected override void setupBanner()
	{
		base.setupBanner();
		this.part_background.sprite = this.meta_object.getSpriteBackground();
		this.part_icon.sprite = this.meta_object.getSpriteIcon();
		this.part_frame.sprite = this.meta_object.getSpriteFrame();
		ColorAsset tColorAsset = this.meta_object.getColor();
		this.part_background.color = tColorAsset.getColorMainSecond();
	}
}
