using System;

// Token: 0x02000662 RID: 1634
public class ClanBanner : BannerGeneric<Clan, ClanData>
{
	// Token: 0x170002DB RID: 731
	// (get) Token: 0x060034FB RID: 13563 RVA: 0x00187A91 File Offset: 0x00185C91
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Clan;
		}
	}

	// Token: 0x170002DC RID: 732
	// (get) Token: 0x060034FC RID: 13564 RVA: 0x00187A94 File Offset: 0x00185C94
	protected override string tooltip_id
	{
		get
		{
			return "clan";
		}
	}

	// Token: 0x060034FD RID: 13565 RVA: 0x00187A9B File Offset: 0x00185C9B
	protected override TooltipData getTooltipData()
	{
		TooltipData tooltipData = base.getTooltipData();
		tooltipData.clan = this.meta_object;
		return tooltipData;
	}

	// Token: 0x060034FE RID: 13566 RVA: 0x00187AB0 File Offset: 0x00185CB0
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
