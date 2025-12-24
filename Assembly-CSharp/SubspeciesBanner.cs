using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000763 RID: 1891
public class SubspeciesBanner : BannerGeneric<Subspecies, SubspeciesData>
{
	// Token: 0x1700037C RID: 892
	// (get) Token: 0x06003C13 RID: 15379 RVA: 0x001A2DAC File Offset: 0x001A0FAC
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Subspecies;
		}
	}

	// Token: 0x1700037D RID: 893
	// (get) Token: 0x06003C14 RID: 15380 RVA: 0x001A2DAF File Offset: 0x001A0FAF
	protected override string tooltip_id
	{
		get
		{
			return "subspecies";
		}
	}

	// Token: 0x06003C15 RID: 15381 RVA: 0x001A2DB6 File Offset: 0x001A0FB6
	protected override TooltipData getTooltipData()
	{
		TooltipData tooltipData = base.getTooltipData();
		tooltipData.subspecies = this.meta_object;
		return tooltipData;
	}

	// Token: 0x06003C16 RID: 15382 RVA: 0x001A2DCC File Offset: 0x001A0FCC
	protected override void setupParts()
	{
		base.setupParts();
		Transform transform = base.transform.FindRecursive("Bookmark 1");
		this._part_bookmark_1 = ((transform != null) ? transform.GetComponent<Image>() : null);
		Transform transform2 = base.transform.FindRecursive("Bookmark 2");
		this._part_bookmark_2 = ((transform2 != null) ? transform2.GetComponent<Image>() : null);
	}

	// Token: 0x06003C17 RID: 15383 RVA: 0x001A2E24 File Offset: 0x001A1024
	protected override void setupBanner()
	{
		base.setupBanner();
		this.part_background.sprite = this.meta_object.getSpriteBackground();
		this.part_icon.sprite = this.meta_object.getSpriteIcon();
		ColorAsset tColorAsset = this.meta_object.getColor();
		this._part_bookmark_1.color = tColorAsset.getColorMainSecond();
		this._part_bookmark_2.color = tColorAsset.getColorMain();
		Sprite tUnitSprite = this.meta_object.getUnitSpriteForBanner();
		this.unit_sprite.sprite = tUnitSprite;
	}

	// Token: 0x04002BD4 RID: 11220
	private Image _part_bookmark_1;

	// Token: 0x04002BD5 RID: 11221
	private Image _part_bookmark_2;

	// Token: 0x04002BD6 RID: 11222
	public Image unit_sprite;
}
