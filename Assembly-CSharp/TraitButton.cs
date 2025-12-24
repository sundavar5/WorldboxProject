using System;

// Token: 0x02000793 RID: 1939
public class TraitButton<TTrait> : AugmentationButton<TTrait> where TTrait : BaseTrait<TTrait>
{
	// Token: 0x06003D7E RID: 15742 RVA: 0x001AE3D4 File Offset: 0x001AC5D4
	public override void load(TTrait pElement)
	{
		this.create();
		this.augmentation_asset = pElement;
		this.image.sprite = this.augmentation_asset.getSprite();
		base.gameObject.name = this.getElementType() + "_" + this.augmentation_asset.id;
		base.loadLegendaryOutline();
	}

	// Token: 0x06003D7F RID: 15743 RVA: 0x001AE43A File Offset: 0x001AC63A
	internal virtual void load(string pElementID)
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003D80 RID: 15744 RVA: 0x001AE444 File Offset: 0x001AC644
	protected override void Update()
	{
		if (this.is_editor_button)
		{
			return;
		}
		if (this.augmentation_asset.unlocked_with_achievement)
		{
			this.locked_bg.gameObject.SetActive(false);
			return;
		}
		bool tShowLocked = !this.augmentation_asset.isAvailable();
		this.locked_bg.gameObject.SetActive(tShowLocked);
	}

	// Token: 0x06003D81 RID: 15745 RVA: 0x001AE4A4 File Offset: 0x001AC6A4
	protected override void fillTooltipData(TTrait pTrait)
	{
		TooltipData tData = this.tooltipDataBuilder();
		tData.is_editor_augmentation_button = this.is_editor_button;
		Tooltip.show(this, this.tooltip_type, tData);
	}

	// Token: 0x06003D82 RID: 15746 RVA: 0x001AE4D1 File Offset: 0x001AC6D1
	protected override bool unlockElement()
	{
		return this.augmentation_asset.unlock(true);
	}

	// Token: 0x06003D83 RID: 15747 RVA: 0x001AE4E4 File Offset: 0x001AC6E4
	protected override string getElementType()
	{
		return "trait";
	}

	// Token: 0x06003D84 RID: 15748 RVA: 0x001AE4EB File Offset: 0x001AC6EB
	protected override void startSignal()
	{
		AchievementLibrary.traits_explorer_40.checkBySignal(null);
		AchievementLibrary.traits_explorer_60.checkBySignal(null);
		AchievementLibrary.traits_explorer_90.checkBySignal(null);
	}

	// Token: 0x06003D85 RID: 15749 RVA: 0x001AE50E File Offset: 0x001AC70E
	public override string getElementId()
	{
		return this.augmentation_asset.id;
	}

	// Token: 0x06003D86 RID: 15750 RVA: 0x001AE520 File Offset: 0x001AC720
	protected override Rarity getRarity()
	{
		return this.augmentation_asset.rarity;
	}
}
