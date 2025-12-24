using System;
using UnityEngine;

// Token: 0x02000685 RID: 1669
public class EquipmentBanner : BannerGeneric<Item, ItemData>
{
	// Token: 0x170002F8 RID: 760
	// (get) Token: 0x06003598 RID: 13720 RVA: 0x00189273 File Offset: 0x00187473
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Item;
		}
	}

	// Token: 0x170002F9 RID: 761
	// (get) Token: 0x06003599 RID: 13721 RVA: 0x00189277 File Offset: 0x00187477
	protected override string tooltip_id
	{
		get
		{
			return "equipment";
		}
	}

	// Token: 0x0600359A RID: 13722 RVA: 0x0018927E File Offset: 0x0018747E
	protected override TooltipData getTooltipData()
	{
		TooltipData tooltipData = base.getTooltipData();
		tooltipData.item = this.meta_object;
		return tooltipData;
	}

	// Token: 0x0600359B RID: 13723 RVA: 0x00189294 File Offset: 0x00187494
	protected override void setupBanner()
	{
		base.setupBanner();
		Item tItem = this.meta_object;
		Rarity tItemQuality = tItem.getQuality();
		this.part_icon.sprite = tItem.getSprite();
		bool tFrameActive = true;
		if (tItemQuality == Rarity.R3_Legendary)
		{
			this.part_frame.sprite = this._frame_sprite_legendary;
		}
		else if (tItemQuality == Rarity.R2_Epic)
		{
			this.part_frame.sprite = this._frame_sprite_epic;
		}
		else
		{
			tFrameActive = false;
		}
		this.part_frame.gameObject.SetActive(tFrameActive);
		if (tItemQuality == Rarity.R3_Legendary)
		{
			this.showOutline();
			return;
		}
		this._outline.gameObject.SetActive(false);
	}

	// Token: 0x0600359C RID: 13724 RVA: 0x00189325 File Offset: 0x00187525
	private void showOutline()
	{
		this._outline.show(RarityLibrary.legendary.color_container);
	}

	// Token: 0x040027F3 RID: 10227
	[SerializeField]
	private IconOutline _outline;

	// Token: 0x040027F4 RID: 10228
	[SerializeField]
	private Sprite _frame_sprite_legendary;

	// Token: 0x040027F5 RID: 10229
	[SerializeField]
	private Sprite _frame_sprite_epic;
}
