using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006CB RID: 1739
public class EquipmentButton : AugmentationButton<EquipmentAsset>, IBanner, IBaseMono, IRefreshElement
{
	// Token: 0x17000316 RID: 790
	// (get) Token: 0x060037B0 RID: 14256 RVA: 0x0019184E File Offset: 0x0018FA4E
	public MetaCustomizationAsset meta_asset
	{
		get
		{
			return AssetManager.meta_customization_library.getAsset(MetaType.Item);
		}
	}

	// Token: 0x17000317 RID: 791
	// (get) Token: 0x060037B1 RID: 14257 RVA: 0x0019185C File Offset: 0x0018FA5C
	public MetaTypeAsset meta_type_asset
	{
		get
		{
			return AssetManager.meta_type_library.getAsset(MetaType.Item);
		}
	}

	// Token: 0x060037B2 RID: 14258 RVA: 0x0019186C File Offset: 0x0018FA6C
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
		bool tShowLocked = !this.augmentation_asset.isAvailable() && this._object_button;
		this.locked_bg.gameObject.SetActive(tShowLocked);
	}

	// Token: 0x060037B3 RID: 14259 RVA: 0x001918CC File Offset: 0x0018FACC
	protected override void onStartDrag(DraggableLayoutElement pOriginalElement)
	{
		EquipmentEditorButton tOriginalButton = pOriginalElement.GetComponent<EquipmentEditorButton>();
		if (tOriginalButton != null)
		{
			this.load(tOriginalButton.augmentation_button.augmentation_asset);
			this.is_editor_button = tOriginalButton.augmentation_button.is_editor_button;
			return;
		}
		EquipmentButton tOriginalButtonSec = pOriginalElement.GetComponent<EquipmentButton>();
		if (tOriginalButtonSec._object_button)
		{
			this.load(tOriginalButtonSec._item);
			this.is_editor_button = tOriginalButtonSec.is_editor_button;
			return;
		}
		this.load(tOriginalButtonSec.augmentation_asset);
		this.is_editor_button = tOriginalButtonSec.is_editor_button;
	}

	// Token: 0x060037B4 RID: 14260 RVA: 0x0019194C File Offset: 0x0018FB4C
	public void load(NanoObject pObject)
	{
		this.load((Item)pObject);
	}

	// Token: 0x060037B5 RID: 14261 RVA: 0x0019195C File Offset: 0x0018FB5C
	internal void load(Item pItem)
	{
		this._object_button = true;
		this.create();
		this._item = pItem;
		this.augmentation_asset = this._item.getAsset();
		if (this.augmentation_asset == null)
		{
			return;
		}
		this.image.sprite = this._item.getSprite();
		base.loadLegendaryOutline();
		base.gameObject.name = this.getElementType() + "_" + this._item.data.asset_id;
		bool tIsFavorite = this._item.isFavorite();
		this._favorited_icon.gameObject.SetActive(tIsFavorite);
	}

	// Token: 0x060037B6 RID: 14262 RVA: 0x001919FC File Offset: 0x0018FBFC
	public override void load(EquipmentAsset pItem)
	{
		this._object_button = false;
		this.create();
		this.augmentation_asset = pItem;
		if (this.augmentation_asset == null)
		{
			return;
		}
		this.image.sprite = this.augmentation_asset.getSprite();
		base.gameObject.name = this.getElementType() + "_" + this.augmentation_asset.id;
		this._favorited_icon.gameObject.SetActive(false);
	}

	// Token: 0x060037B7 RID: 14263 RVA: 0x00191A74 File Offset: 0x0018FC74
	protected override void initTooltip()
	{
		base.initTooltip();
		TipButton tTipButton;
		if (!base.TryGetComponent<TipButton>(out tTipButton))
		{
			return;
		}
		TipButton tipButton = tTipButton;
		tipButton.clickAction = (TooltipAction)Delegate.Combine(tipButton.clickAction, new TooltipAction(delegate()
		{
			if (InputHelpers.mouseSupported)
			{
				this.openItemWindow();
				return;
			}
			if (Tooltip.isShowingFor(this) && !this.is_editor_button)
			{
				this.openItemWindow();
				return;
			}
			base.showTooltip();
		}));
	}

	// Token: 0x060037B8 RID: 14264 RVA: 0x00191AB4 File Offset: 0x0018FCB4
	private void openItemWindow()
	{
		SelectedMetas.selected_item = this._item;
		if (SelectedMetas.selected_item == null)
		{
			return;
		}
		ScrollWindow.showWindow("item");
	}

	// Token: 0x060037B9 RID: 14265 RVA: 0x00191AD4 File Offset: 0x0018FCD4
	protected override void fillTooltipData(EquipmentAsset pElement)
	{
		string tType = (this.is_editor_button || !this._object_button) ? "equipment_in_editor" : "equipment";
		Tooltip.show(this, tType, this.tooltipDataBuilder());
	}

	// Token: 0x060037BA RID: 14266 RVA: 0x00191B0B File Offset: 0x0018FD0B
	protected override bool unlockElement()
	{
		return this.augmentation_asset.unlock(true);
	}

	// Token: 0x060037BB RID: 14267 RVA: 0x00191B19 File Offset: 0x0018FD19
	protected override TooltipData tooltipDataBuilder()
	{
		if (!this.is_editor_button && this._object_button)
		{
			return new TooltipData
			{
				item = this._item
			};
		}
		return new TooltipData
		{
			item_asset = this.augmentation_asset
		};
	}

	// Token: 0x17000318 RID: 792
	// (get) Token: 0x060037BC RID: 14268 RVA: 0x00191B4E File Offset: 0x0018FD4E
	protected override string tooltip_type
	{
		get
		{
			return "equipment";
		}
	}

	// Token: 0x060037BD RID: 14269 RVA: 0x00191B55 File Offset: 0x0018FD55
	protected override string getElementType()
	{
		return "equip";
	}

	// Token: 0x060037BE RID: 14270 RVA: 0x00191B5C File Offset: 0x0018FD5C
	protected override void startSignal()
	{
		AchievementLibrary.equipment_explorer.checkBySignal(null);
	}

	// Token: 0x060037BF RID: 14271 RVA: 0x00191B69 File Offset: 0x0018FD69
	public override string getElementId()
	{
		return base.getElementAsset().id;
	}

	// Token: 0x060037C0 RID: 14272 RVA: 0x00191B76 File Offset: 0x0018FD76
	private bool hasDivineRune()
	{
		return this._item != null && this._item.isAlive() && this._item.hasMod("divine_rune");
	}

	// Token: 0x060037C1 RID: 14273 RVA: 0x00191BA1 File Offset: 0x0018FDA1
	protected override Rarity getRarity()
	{
		return this._item.getQuality();
	}

	// Token: 0x060037C2 RID: 14274 RVA: 0x00191BAE File Offset: 0x0018FDAE
	public string getName()
	{
		return this._item.getName(true);
	}

	// Token: 0x060037C3 RID: 14275 RVA: 0x00191BBC File Offset: 0x0018FDBC
	public NanoObject GetNanoObject()
	{
		return this._item;
	}

	// Token: 0x060037C5 RID: 14277 RVA: 0x00191BCC File Offset: 0x0018FDCC
	Transform IBaseMono.get_transform()
	{
		return base.transform;
	}

	// Token: 0x060037C6 RID: 14278 RVA: 0x00191BD4 File Offset: 0x0018FDD4
	GameObject IBaseMono.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x060037C7 RID: 14279 RVA: 0x00191BDC File Offset: 0x0018FDDC
	T IBaseMono.GetComponent<T>()
	{
		return base.GetComponent<T>();
	}

	// Token: 0x04002958 RID: 10584
	[SerializeField]
	private Image _favorited_icon;

	// Token: 0x04002959 RID: 10585
	private Item _item;

	// Token: 0x0400295A RID: 10586
	private bool _object_button;
}
