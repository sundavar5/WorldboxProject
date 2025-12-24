using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000651 RID: 1617
public class CityBanner : BannerGeneric<City, CityData>
{
	// Token: 0x170002D3 RID: 723
	// (get) Token: 0x06003478 RID: 13432 RVA: 0x00185D7E File Offset: 0x00183F7E
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.City;
		}
	}

	// Token: 0x170002D4 RID: 724
	// (get) Token: 0x06003479 RID: 13433 RVA: 0x00185D81 File Offset: 0x00183F81
	protected override string tooltip_id
	{
		get
		{
			return "city";
		}
	}

	// Token: 0x0600347A RID: 13434 RVA: 0x00185D88 File Offset: 0x00183F88
	protected override void setupBanner()
	{
		base.setupBanner();
		ColorAsset tColorAsset = this.meta_object.kingdom.getColor();
		this.part_background.sprite = this.meta_object.kingdom.getElementBackground();
		this.part_icon.sprite = this.meta_object.kingdom.getElementIcon();
		Sprite tSpriteToSet = this.meta_object.isCapitalCity() ? this._capital_sprite : this._city_sprite;
		this._part_city_icon.sprite = DynamicSprites.getIconWithColors(tSpriteToSet, null, tColorAsset);
		Color tColorMain2 = tColorAsset.getColorMainSecond();
		Color tColorIcon = tColorAsset.getColorBanner();
		tColorMain2 = Color.Lerp(tColorMain2, Color.black, 0.05f);
		tColorIcon = Color.Lerp(tColorIcon, Color.black, 0.05f);
		this.part_background.color = tColorMain2;
		this.part_icon.color = tColorIcon;
	}

	// Token: 0x0600347B RID: 13435 RVA: 0x00185E59 File Offset: 0x00184059
	protected override void setupParts()
	{
		base.setupParts();
		this._part_city_icon = base.transform.FindRecursive("Foundation").GetComponent<Image>();
	}

	// Token: 0x0600347C RID: 13436 RVA: 0x00185E7C File Offset: 0x0018407C
	protected override TooltipData getTooltipData()
	{
		TooltipData tooltipData = base.getTooltipData();
		tooltipData.city = this.meta_object;
		return tooltipData;
	}

	// Token: 0x0400278B RID: 10123
	[SerializeField]
	private Sprite _city_sprite;

	// Token: 0x0400278C RID: 10124
	[SerializeField]
	private Sprite _capital_sprite;

	// Token: 0x0400278D RID: 10125
	private Image _part_city_icon;
}
