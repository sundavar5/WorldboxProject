using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200062F RID: 1583
public class ArmyBanner : BannerGeneric<Army, ArmyData>
{
	// Token: 0x170002C8 RID: 712
	// (get) Token: 0x060033A7 RID: 13223 RVA: 0x00183AA3 File Offset: 0x00181CA3
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Army;
		}
	}

	// Token: 0x170002C9 RID: 713
	// (get) Token: 0x060033A8 RID: 13224 RVA: 0x00183AA7 File Offset: 0x00181CA7
	protected override string tooltip_id
	{
		get
		{
			return "army";
		}
	}

	// Token: 0x060033A9 RID: 13225 RVA: 0x00183AB0 File Offset: 0x00181CB0
	protected override void setupBanner()
	{
		base.setupBanner();
		Kingdom tKingdom = this.meta_object.getKingdom();
		this.part_background.sprite = tKingdom.getElementBackground();
		this.part_icon.sprite = tKingdom.getElementIcon();
		ColorAsset color = tKingdom.getColor();
		Color tColorMain2 = color.getColorMainSecond();
		Color tColorIcon = color.getColorBanner();
		tColorMain2 = Color.Lerp(tColorMain2, Color.black, 0.05f);
		tColorIcon = Color.Lerp(tColorIcon, Color.black, 0.05f);
		this.part_background.color = tColorMain2;
		this.part_icon.color = tColorIcon;
		this._species_icon.gameObject.SetActive(false);
	}

	// Token: 0x060033AA RID: 13226 RVA: 0x00183B4F File Offset: 0x00181D4F
	protected override TooltipData getTooltipData()
	{
		TooltipData tooltipData = base.getTooltipData();
		tooltipData.army = this.meta_object;
		return tooltipData;
	}

	// Token: 0x0400271C RID: 10012
	[SerializeField]
	private Image _species_icon;
}
