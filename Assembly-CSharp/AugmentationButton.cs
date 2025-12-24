using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200063B RID: 1595
public class AugmentationButton<TAugmentation> : MonoBehaviour where TAugmentation : BaseAugmentationAsset
{
	// Token: 0x060033E7 RID: 13287 RVA: 0x0018439E File Offset: 0x0018259E
	public bool isSelected()
	{
		return this._selected;
	}

	// Token: 0x060033E8 RID: 13288 RVA: 0x001843A8 File Offset: 0x001825A8
	protected virtual void Awake()
	{
		this.create();
		DraggableLayoutElement tDraggableLayoutElement;
		if (base.TryGetComponent<DraggableLayoutElement>(out tDraggableLayoutElement))
		{
			DraggableLayoutElement draggableLayoutElement = tDraggableLayoutElement;
			draggableLayoutElement.start_being_dragged = (Action<DraggableLayoutElement>)Delegate.Combine(draggableLayoutElement.start_being_dragged, new Action<DraggableLayoutElement>(this.onStartDrag));
		}
	}

	// Token: 0x060033E9 RID: 13289 RVA: 0x001843E8 File Offset: 0x001825E8
	protected virtual void onStartDrag(DraggableLayoutElement pOriginalElement)
	{
		AugmentationButton<TAugmentation> tOriginalButton = pOriginalElement.GetComponent<AugmentationButton<TAugmentation>>();
		this.load(tOriginalButton.augmentation_asset);
		this.is_editor_button = tOriginalButton.is_editor_button;
	}

	// Token: 0x060033EA RID: 13290 RVA: 0x00184414 File Offset: 0x00182614
	protected virtual void create()
	{
		if (this.created)
		{
			return;
		}
		this.created = true;
		this.button = base.GetComponent<Button>();
		this.image = base.transform.Find("TiltEffect/icon").GetComponent<Image>();
		this.locked_bg = base.transform.Find("TiltEffect/locked_bg").GetComponent<Image>();
		this.locked_bg.gameObject.SetActive(false);
		this.initTooltip();
		Transform transform = base.transform.FindRecursive("outline");
		this._outline = ((transform != null) ? transform.GetComponent<IconOutline>() : null);
		this._shadow = this.image.GetComponent<Shadow>();
		this.button.onClick.AddListener(delegate()
		{
			AugmentationButtonClickAction on_button_clicked = this._on_button_clicked;
			if (on_button_clicked == null)
			{
				return;
			}
			on_button_clicked(base.gameObject);
		});
	}

	// Token: 0x060033EB RID: 13291 RVA: 0x001844D9 File Offset: 0x001826D9
	public virtual void load(TAugmentation pElement)
	{
		throw new NotImplementedException();
	}

	// Token: 0x060033EC RID: 13292 RVA: 0x001844E0 File Offset: 0x001826E0
	protected virtual void initTooltip()
	{
		TipButton tTipButton;
		if (!base.TryGetComponent<TipButton>(out tTipButton))
		{
			return;
		}
		tTipButton.setHoverAction(new TooltipAction(this.showTooltip), true);
	}

	// Token: 0x060033ED RID: 13293 RVA: 0x0018450C File Offset: 0x0018270C
	protected virtual void Update()
	{
		throw new NotImplementedException();
	}

	// Token: 0x060033EE RID: 13294 RVA: 0x00184514 File Offset: 0x00182714
	protected void loadLegendaryOutline()
	{
		this._shadow.enabled = true;
		if (this._outline == null)
		{
			return;
		}
		if (this.getRarity() == Rarity.R3_Legendary)
		{
			this.showOutline(RarityLibrary.legendary.color_container);
			return;
		}
		this._outline.gameObject.SetActive(false);
	}

	// Token: 0x060033EF RID: 13295 RVA: 0x00184567 File Offset: 0x00182767
	private void showOutline(ContainerItemColor pContainer)
	{
		if (this._outline == null)
		{
			return;
		}
		this._outline.show(pContainer);
		this._shadow.enabled = false;
	}

	// Token: 0x060033F0 RID: 13296 RVA: 0x00184590 File Offset: 0x00182790
	public void showTooltip()
	{
		if (!this._tooltip_enabled)
		{
			return;
		}
		if (!this.is_editor_button && !this.augmentation_asset.unlocked_with_achievement && !this.isElementUnlocked() && !WorldLawLibrary.world_law_cursed_world.isEnabled() && this.unlockElement())
		{
			this.startSignal();
			AugmentationUnlockedAction on_augmentation_unlocked = this._on_augmentation_unlocked;
			if (on_augmentation_unlocked != null)
			{
				on_augmentation_unlocked();
			}
		}
		if (!this.is_editor_button || InputHelpers.mouseSupported || !Tooltip.isShowingFor(this))
		{
			this.fillTooltipData(this.augmentation_asset);
		}
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		base.transform.DOKill(false);
		base.transform.DOScale(0.8f, 0.1f).SetEase(Ease.InBack);
	}

	// Token: 0x060033F1 RID: 13297 RVA: 0x0018465F File Offset: 0x0018285F
	public void addElementUnlockedAction(AugmentationUnlockedAction pAction)
	{
		this._on_augmentation_unlocked = (AugmentationUnlockedAction)Delegate.Combine(this._on_augmentation_unlocked, pAction);
	}

	// Token: 0x060033F2 RID: 13298 RVA: 0x00184678 File Offset: 0x00182878
	public void removeElementUnlockedAction(AugmentationUnlockedAction pAction)
	{
		this._on_augmentation_unlocked = (AugmentationUnlockedAction)Delegate.Remove(this._on_augmentation_unlocked, pAction);
	}

	// Token: 0x060033F3 RID: 13299 RVA: 0x00184691 File Offset: 0x00182891
	protected virtual void clearActions()
	{
		this._on_augmentation_unlocked = null;
		this.clearClickActions();
	}

	// Token: 0x060033F4 RID: 13300 RVA: 0x001846A0 File Offset: 0x001828A0
	public virtual void updateIconColor(bool pSelected)
	{
		this._selected = pSelected;
		if (!this.is_editor_button)
		{
			return;
		}
		if (!this.getElementAsset().isAvailable())
		{
			this.image.color = Toolbox.color_black;
			return;
		}
		if (pSelected)
		{
			this.image.color = Toolbox.color_augmentation_selected;
			return;
		}
		this.image.color = Toolbox.color_augmentation_unselected;
	}

	// Token: 0x060033F5 RID: 13301 RVA: 0x00184704 File Offset: 0x00182904
	public TAugmentation getElementAsset()
	{
		return this.augmentation_asset;
	}

	// Token: 0x060033F6 RID: 13302 RVA: 0x0018470C File Offset: 0x0018290C
	protected bool isElementUnlocked()
	{
		return this.augmentation_asset.isAvailable();
	}

	// Token: 0x060033F7 RID: 13303 RVA: 0x0018471E File Offset: 0x0018291E
	protected virtual bool unlockElement()
	{
		throw new NotImplementedException(base.GetType().Name);
	}

	// Token: 0x060033F8 RID: 13304 RVA: 0x00184730 File Offset: 0x00182930
	protected virtual void startSignal()
	{
	}

	// Token: 0x060033F9 RID: 13305 RVA: 0x00184732 File Offset: 0x00182932
	protected virtual void fillTooltipData(TAugmentation pElement)
	{
		throw new NotImplementedException(base.GetType().Name);
	}

	// Token: 0x170002CF RID: 719
	// (get) Token: 0x060033FA RID: 13306 RVA: 0x00184744 File Offset: 0x00182944
	protected virtual string tooltip_type
	{
		get
		{
			throw new NotImplementedException(base.GetType().Name);
		}
	}

	// Token: 0x060033FB RID: 13307 RVA: 0x00184756 File Offset: 0x00182956
	protected virtual TooltipData tooltipDataBuilder()
	{
		throw new NotImplementedException(base.GetType().Name);
	}

	// Token: 0x060033FC RID: 13308 RVA: 0x00184768 File Offset: 0x00182968
	protected virtual string getElementType()
	{
		throw new NotImplementedException(base.GetType().Name);
	}

	// Token: 0x060033FD RID: 13309 RVA: 0x0018477A File Offset: 0x0018297A
	public virtual string getElementId()
	{
		throw new NotImplementedException(base.GetType().Name);
	}

	// Token: 0x060033FE RID: 13310 RVA: 0x0018478C File Offset: 0x0018298C
	protected virtual Rarity getRarity()
	{
		throw new NotImplementedException(base.GetType().Name);
	}

	// Token: 0x060033FF RID: 13311 RVA: 0x0018479E File Offset: 0x0018299E
	protected virtual void disableTooltip()
	{
		this._tooltip_enabled = false;
	}

	// Token: 0x06003400 RID: 13312 RVA: 0x001847A7 File Offset: 0x001829A7
	public void addClickAction(AugmentationButtonClickAction pAction)
	{
		this._on_button_clicked = (AugmentationButtonClickAction)Delegate.Combine(this._on_button_clicked, pAction);
	}

	// Token: 0x06003401 RID: 13313 RVA: 0x001847C0 File Offset: 0x001829C0
	public void removeClickAction(AugmentationButtonClickAction pAction)
	{
		this._on_button_clicked = (AugmentationButtonClickAction)Delegate.Remove(this._on_button_clicked, pAction);
	}

	// Token: 0x06003402 RID: 13314 RVA: 0x001847D9 File Offset: 0x001829D9
	private void clearClickActions()
	{
		this._on_button_clicked = null;
	}

	// Token: 0x06003403 RID: 13315 RVA: 0x001847E2 File Offset: 0x001829E2
	private void OnDestroy()
	{
		base.transform.DOKill(false);
	}

	// Token: 0x0400273A RID: 10042
	[NonSerialized]
	public TAugmentation augmentation_asset;

	// Token: 0x0400273B RID: 10043
	internal Image image;

	// Token: 0x0400273C RID: 10044
	internal Image locked_bg;

	// Token: 0x0400273D RID: 10045
	private IconOutline _outline;

	// Token: 0x0400273E RID: 10046
	private Shadow _shadow;

	// Token: 0x0400273F RID: 10047
	private bool _tooltip_enabled = true;

	// Token: 0x04002740 RID: 10048
	internal Button button;

	// Token: 0x04002741 RID: 10049
	internal bool is_editor_button;

	// Token: 0x04002742 RID: 10050
	private AugmentationUnlockedAction _on_augmentation_unlocked;

	// Token: 0x04002743 RID: 10051
	private AugmentationButtonClickAction _on_button_clicked;

	// Token: 0x04002744 RID: 10052
	protected bool created;

	// Token: 0x04002745 RID: 10053
	private bool _selected;
}
