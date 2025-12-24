using System;

// Token: 0x02000724 RID: 1828
public class PlotButton : AugmentationButton<PlotAsset>
{
	// Token: 0x06003A51 RID: 14929 RVA: 0x0019D74C File Offset: 0x0019B94C
	public override void load(PlotAsset pElement)
	{
		this.create();
		this.augmentation_asset = pElement;
		this.image.sprite = this.augmentation_asset.getSprite();
		base.gameObject.name = this.getElementType() + "_" + this.augmentation_asset.id;
		base.loadLegendaryOutline();
	}

	// Token: 0x06003A52 RID: 14930 RVA: 0x0019D7A8 File Offset: 0x0019B9A8
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

	// Token: 0x06003A53 RID: 14931 RVA: 0x0019D800 File Offset: 0x0019BA00
	public override void updateIconColor(bool pSelected)
	{
		if (!this.is_editor_button)
		{
			return;
		}
		if (!base.getElementAsset().isAvailable())
		{
			this.image.color = Toolbox.color_black;
			return;
		}
		if (pSelected)
		{
			this.image.color = Toolbox.color_augmentation_selected;
			return;
		}
		Actor tSelected = SelectedUnit.unit;
		if (!this.augmentation_asset.canBeDoneByRole(tSelected))
		{
			this.image.color = Toolbox.color_gray;
			return;
		}
		if (this.augmentation_asset.check_can_be_forced != null && !this.augmentation_asset.check_can_be_forced(SelectedUnit.unit))
		{
			this.image.color = Toolbox.color_gray;
			return;
		}
		this.image.color = Toolbox.color_white;
	}

	// Token: 0x06003A54 RID: 14932 RVA: 0x0019D8B5 File Offset: 0x0019BAB5
	protected override bool unlockElement()
	{
		return this.augmentation_asset.unlock(true);
	}

	// Token: 0x06003A55 RID: 14933 RVA: 0x0019D8C3 File Offset: 0x0019BAC3
	protected override void startSignal()
	{
		AchievementLibrary.plots_explorer.checkBySignal(null);
	}

	// Token: 0x06003A56 RID: 14934 RVA: 0x0019D8D0 File Offset: 0x0019BAD0
	protected override void fillTooltipData(PlotAsset pElement)
	{
		Tooltip.show(this, this.tooltip_type, this.tooltipDataBuilder());
	}

	// Token: 0x17000346 RID: 838
	// (get) Token: 0x06003A57 RID: 14935 RVA: 0x0019D8E4 File Offset: 0x0019BAE4
	protected override string tooltip_type
	{
		get
		{
			return "plot_in_editor";
		}
	}

	// Token: 0x06003A58 RID: 14936 RVA: 0x0019D8EB File Offset: 0x0019BAEB
	protected override TooltipData tooltipDataBuilder()
	{
		return new TooltipData
		{
			plot_asset = this.augmentation_asset
		};
	}

	// Token: 0x06003A59 RID: 14937 RVA: 0x0019D8FE File Offset: 0x0019BAFE
	protected override string getElementType()
	{
		return "plot";
	}

	// Token: 0x06003A5A RID: 14938 RVA: 0x0019D905 File Offset: 0x0019BB05
	public override string getElementId()
	{
		return this.augmentation_asset.id;
	}

	// Token: 0x06003A5B RID: 14939 RVA: 0x0019D912 File Offset: 0x0019BB12
	protected override Rarity getRarity()
	{
		return this.augmentation_asset.rarity;
	}
}
