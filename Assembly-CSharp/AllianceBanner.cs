using System;
using UnityEngine;

// Token: 0x02000623 RID: 1571
public class AllianceBanner : BannerGeneric<Alliance, AllianceData>
{
	// Token: 0x170002C2 RID: 706
	// (get) Token: 0x06003369 RID: 13161 RVA: 0x001831BF File Offset: 0x001813BF
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Alliance;
		}
	}

	// Token: 0x170002C3 RID: 707
	// (get) Token: 0x0600336A RID: 13162 RVA: 0x001831C3 File Offset: 0x001813C3
	protected override string tooltip_id
	{
		get
		{
			return "alliance";
		}
	}

	// Token: 0x0600336B RID: 13163 RVA: 0x001831CA File Offset: 0x001813CA
	protected override TooltipData getTooltipData()
	{
		TooltipData tooltipData = base.getTooltipData();
		tooltipData.alliance = this.meta_object;
		return tooltipData;
	}

	// Token: 0x0600336C RID: 13164 RVA: 0x001831E0 File Offset: 0x001813E0
	protected override void setupBanner()
	{
		base.setupBanner();
		this.part_background.sprite = this.meta_object.getBackgroundSprite();
		this.part_icon.sprite = this.meta_object.getIconSprite();
		ColorAsset tColorAsset = this.meta_object.getColor();
		this.part_background.color = tColorAsset.getColorMainSecond();
		this.part_icon.color = tColorAsset.getColorBanner();
		if (this.meta_object.isNormalType())
		{
			this.part_frame.sprite = this.frame_normal;
			return;
		}
		this.part_frame.sprite = this.frame_forced;
	}

	// Token: 0x04002701 RID: 9985
	public Sprite frame_normal;

	// Token: 0x04002702 RID: 9986
	public Sprite frame_forced;
}
