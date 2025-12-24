using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200063E RID: 1598
public class AugmentationsEditor<TAugmentation, TAugmentationButton, TAugmentationEditorButton, TAugmentationGroupAsset, TAugmentationGroup, TAugmentationWindow, TEditorInterface> : BaseAugmentationsEditor where TAugmentation : BaseAugmentationAsset where TAugmentationButton : AugmentationButton<TAugmentation> where TAugmentationEditorButton : AugmentationEditorButton<TAugmentationButton, TAugmentation> where TAugmentationGroupAsset : BaseCategoryAsset where TAugmentationGroup : AugmentationCategory<TAugmentation, TAugmentationButton, TAugmentationEditorButton> where TAugmentationWindow : IAugmentationsWindow<TEditorInterface> where TEditorInterface : IAugmentationsEditor
{
	// Token: 0x170002D0 RID: 720
	// (get) Token: 0x06003410 RID: 13328 RVA: 0x00184A57 File Offset: 0x00182C57
	protected virtual List<TAugmentationGroupAsset> augmentation_groups_list
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x170002D1 RID: 721
	// (get) Token: 0x06003411 RID: 13329 RVA: 0x00184A5E File Offset: 0x00182C5E
	protected virtual List<TAugmentation> all_augmentations_list
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x170002D2 RID: 722
	// (get) Token: 0x06003412 RID: 13330 RVA: 0x00184A68 File Offset: 0x00182C68
	protected virtual TAugmentation edited_marker_augmentation
	{
		get
		{
			return default(TAugmentation);
		}
	}

	// Token: 0x06003413 RID: 13331 RVA: 0x00184A7E File Offset: 0x00182C7E
	protected override void create()
	{
		base.create();
		this.augmentation_window = base.GetComponentInParent<TAugmentationWindow>();
		if (this.rain_editor)
		{
			this.selected_editor_buttons = new ObjectPoolGenericMono<TAugmentationButton>(this.prefab_augmentation, this.selected_editor_augmentations_grid.transform);
		}
	}

	// Token: 0x06003414 RID: 13332 RVA: 0x00184AB6 File Offset: 0x00182CB6
	protected override void OnEnable()
	{
		if (this.rain_editor)
		{
			this.onEnableRain();
		}
		base.OnEnable();
	}

	// Token: 0x06003415 RID: 13333 RVA: 0x00184ACC File Offset: 0x00182CCC
	protected virtual ListPool<TAugmentation> getOrderedAugmentationsList()
	{
		ListPool<TAugmentation> listPool = new ListPool<TAugmentation>(this.all_augmentations_list);
		listPool.Sort(delegate(TAugmentation pT1, TAugmentation pT2)
		{
			int tResult = pT2.priority.CompareTo(pT1.priority);
			if (tResult == 0)
			{
				tResult = StringComparer.Ordinal.Compare(pT1.id, pT2.id);
			}
			return tResult;
		});
		return listPool;
	}

	// Token: 0x06003416 RID: 13334 RVA: 0x00184B00 File Offset: 0x00182D00
	public override void reloadButtons()
	{
		base.reloadButtons();
		int tCounterSelected = 0;
		int tCounterUnlocked = 0;
		int tTotal = 0;
		foreach (TAugmentationEditorButton tB in this.all_augmentation_buttons)
		{
			bool flag = this.isAugmentationAvailable(tB.augmentation_button);
			TAugmentation tAsset = tB.augmentation_button.getElementAsset();
			tTotal++;
			if (flag)
			{
				tCounterUnlocked++;
			}
			tB.selected_icon.gameObject.SetActive(false);
			if (flag)
			{
				tB.augmentation_button.image.color = Toolbox.color_augmentation_unselected;
			}
			bool can_be_given = tAsset.can_be_given;
			bool tSelected = false;
			if (!can_be_given)
			{
				bool tHas = !this.rain_editor && this.hasAugmentation(tB.augmentation_button);
				tB.selected_icon.gameObject.SetActive(tHas);
				tB.selected_icon.color = Toolbox.color_log_warning;
				if (tHas)
				{
					tCounterSelected++;
					tSelected = true;
				}
			}
			else if (this.rain_editor && this.augmentations_hashset.Contains(tB.augmentation_button.getElementId()))
			{
				Color tColor;
				if (this.rain_editor_state == RainState.Add)
				{
					tColor = ColorStyleLibrary.m.getSelectorColor();
				}
				else
				{
					tColor = ColorStyleLibrary.m.getSelectorRemoveColor();
				}
				tB.selected_icon.gameObject.SetActive(true);
				tB.selected_icon.color = tColor;
				tSelected = true;
			}
			else if (!this.rain_editor && this.hasAugmentation(tB.augmentation_button))
			{
				tB.selected_icon.gameObject.SetActive(true);
				tB.selected_icon.color = ColorStyleLibrary.m.getSelectorColor();
				tSelected = true;
				tCounterSelected++;
			}
			tB.augmentation_button.updateIconColor(tSelected);
		}
		foreach (TAugmentationGroup tElement in this.dict_groups.Values)
		{
			if (tElement.asset.show_counter)
			{
				tElement.updateCounter();
			}
			else
			{
				tElement.hideCounter();
			}
		}
		if (this.rain_editor)
		{
			this.text_counter_augmentations.text = tCounterUnlocked.ToString() + "/" + tTotal.ToString();
		}
		else
		{
			this.text_counter_augmentations.text = tCounterSelected.ToString() + "/" + tTotal.ToString();
		}
		this.startSignal();
	}

	// Token: 0x06003417 RID: 13335 RVA: 0x00184E04 File Offset: 0x00183004
	protected override void groupsBuilder()
	{
		using (ListPool<TAugmentation> tOrderedElementList = this.getOrderedAugmentationsList())
		{
			foreach (TAugmentationGroupAsset tGroupAsset in this.augmentation_groups_list)
			{
				TAugmentationGroup tNewTransform = Object.Instantiate<TAugmentationGroup>(this.prefab_augmentation_group, this.augmentation_groups_parent);
				tNewTransform.asset = tGroupAsset;
				tNewTransform.clearDebug();
				this.dict_groups.Add(tGroupAsset.id, tNewTransform);
				tNewTransform.title.GetComponent<LocalizedText>().setKeyAndUpdate(tGroupAsset.getLocaleID());
				tNewTransform.title.color = tGroupAsset.getColor();
			}
			foreach (TAugmentation ptr in tOrderedElementList)
			{
				TAugmentation tElement = ptr;
				TAugmentationGroup tTransformParent = this.dict_groups[tElement.group_id];
				this.createButton(tElement, tTransformParent);
			}
		}
	}

	// Token: 0x06003418 RID: 13336 RVA: 0x00184F78 File Offset: 0x00183178
	protected override void checkEnabledGroups()
	{
		foreach (TAugmentationGroup taugmentationGroup in this.dict_groups.Values)
		{
			bool tGroupState = taugmentationGroup.countActiveButtons() > 0;
			taugmentationGroup.gameObject.SetActive(tGroupState);
		}
	}

	// Token: 0x06003419 RID: 13337 RVA: 0x00184FE8 File Offset: 0x001831E8
	protected void editorButtonClick(TAugmentationEditorButton pButton)
	{
		if (!InputHelpers.mouseSupported && !Tooltip.isShowingFor(pButton.augmentation_button))
		{
			return;
		}
		if (!Config.hasPremium)
		{
			ScrollWindow.showWindow("premium_menu");
			return;
		}
		if (!pButton.augmentation_button.getElementAsset().can_be_given)
		{
			return;
		}
		if (this.rain_editor)
		{
			this.rainAugmentationClick(pButton);
		}
		else
		{
			this.metaAugmentationClick(pButton);
		}
		this.reloadButtons();
	}

	// Token: 0x0600341A RID: 13338 RVA: 0x00185066 File Offset: 0x00183266
	protected virtual void metaAugmentationClick(TAugmentationEditorButton pButton)
	{
		this.showActiveButtons();
		this.refreshAugmentationWindow();
	}

	// Token: 0x0600341B RID: 13339 RVA: 0x00185074 File Offset: 0x00183274
	protected virtual void rainAugmentationClick(TAugmentationEditorButton pButton)
	{
		this.saveRainValues();
		this.loadEditorSelectedAugmentations();
	}

	// Token: 0x0600341C RID: 13340 RVA: 0x00185082 File Offset: 0x00183282
	protected virtual void validateRainData()
	{
		this.augmentations_list_link.RemoveAll(delegate(string tId)
		{
			TAugmentation tAugmentation2 = this.all_augmentations_list.Find((TAugmentation tAugmentation) => tAugmentation.id == tId);
			return tAugmentation2 == null || !tAugmentation2.isAvailable();
		});
	}

	// Token: 0x0600341D RID: 13341 RVA: 0x0018509C File Offset: 0x0018329C
	protected virtual void refreshAugmentationWindow()
	{
		this.augmentation_window.updateStats();
		this.augmentation_window.reloadBanner();
	}

	// Token: 0x0600341E RID: 13342 RVA: 0x001850C0 File Offset: 0x001832C0
	protected void saveRainValues()
	{
		this.augmentations_list_link.Clear();
		foreach (string tElementID in this.augmentations_hashset)
		{
			this.augmentations_list_link.Add(tElementID);
		}
		PlayerConfig.saveData();
	}

	// Token: 0x0600341F RID: 13343 RVA: 0x00185128 File Offset: 0x00183328
	protected virtual void loadEditorSelectedAugmentations()
	{
		this.selected_editor_buttons.clear(true);
		foreach (string tAugmentationId in this.augmentations_hashset)
		{
			if (this.isAugmentationExists(tAugmentationId))
			{
				TAugmentationButton tButton = this.selected_editor_buttons.getNext();
				this.loadEditorSelectedButton(tButton, tAugmentationId);
			}
		}
	}

	// Token: 0x06003420 RID: 13344 RVA: 0x001851A0 File Offset: 0x001833A0
	public void scrollToGroupStarter(GameObject pButton)
	{
		this.scrollToGroupStarter(pButton, false);
	}

	// Token: 0x06003421 RID: 13345 RVA: 0x001851AC File Offset: 0x001833AC
	public virtual void scrollToGroupStarter(GameObject pButton, bool pIgnoreTooltipCheck)
	{
		if (!pIgnoreTooltipCheck && !InputHelpers.mouseSupported && !Tooltip.isShowingFor(pButton.GetComponent<TAugmentationButton>()))
		{
			return;
		}
		bool tDelay = false;
		if (!base.gameObject.activeInHierarchy)
		{
			if (!(this._editor_tab != null))
			{
				return;
			}
			this._editor_tab.container.showTab(this._editor_tab);
			tDelay = true;
		}
		base.StartCoroutine(this.scrollToGroupStarterRoutine(pButton, tDelay));
	}

	// Token: 0x06003422 RID: 13346 RVA: 0x0018521D File Offset: 0x0018341D
	private IEnumerator scrollToGroupStarterRoutine(GameObject pButton, bool pWithDelay)
	{
		if (pWithDelay)
		{
			yield return new WaitForSeconds(Config.getScrollToGroupDelay());
		}
		this.scrollToGroup(pButton, 0.3f);
		yield break;
	}

	// Token: 0x06003423 RID: 13347 RVA: 0x0018523C File Offset: 0x0018343C
	private void scrollToGroup(GameObject pButton, float pDuration = 0.3f)
	{
		TAugmentationGroup tTraitGroup = default(TAugmentationGroup);
		foreach (TAugmentationGroup tGroup in this.dict_groups.Values)
		{
			TAugmentationButton tTraitButton = pButton.GetComponent<TAugmentationButton>();
			if (tGroup.hasAugmentation(tTraitButton.getElementAsset()))
			{
				tTraitGroup = tGroup;
				break;
			}
		}
		if (tTraitGroup == null)
		{
			return;
		}
		RectTransform rectTransform = pButton.GetComponentInParent<HeaderContainer>().transform as RectTransform;
		RectTransform tContentRect = base.transform.parent.GetComponent<RectTransform>();
		RectTransform component = tContentRect.parent.GetComponent<RectTransform>();
		RectTransform tEditorRect = base.transform as RectTransform;
		RectTransform tCategoryRect = tTraitGroup.GetComponent<RectTransform>();
		float tViewportHeight = component.rect.height;
		float tHeaderHeight = rectTransform.rect.height;
		float tContentHeight = tContentRect.rect.height;
		float tEditorHeight = tEditorRect.rect.height;
		float tCategoryHeight = tCategoryRect.rect.height;
		float tEditorOffset = Mathf.Abs(tEditorRect.anchoredPosition.y) - tEditorHeight * (1f - tEditorRect.pivot.y) - tHeaderHeight;
		float tCategoryUpperY = Mathf.Abs(tCategoryRect.anchoredPosition.y) - tCategoryHeight * (1f - tCategoryRect.pivot.y) + tEditorOffset;
		float tCategoryLowerY = tCategoryUpperY + tCategoryHeight;
		bool tIsAbove = tCategoryUpperY < tContentRect.localPosition.y;
		bool tIsBelow = tCategoryLowerY > tContentRect.localPosition.y + tViewportHeight - tHeaderHeight;
		if (!tIsAbove && !tIsBelow)
		{
			return;
		}
		float tScrollTo;
		if (tIsAbove)
		{
			tScrollTo = tCategoryUpperY;
			tScrollTo -= -5f;
		}
		else
		{
			tScrollTo = tCategoryLowerY - tViewportHeight + tHeaderHeight;
			tScrollTo += 1f;
		}
		tScrollTo = Mathf.Clamp(tScrollTo, 0f, tContentHeight - tViewportHeight);
		tContentRect.DOLocalMoveY(tScrollTo, pDuration, false);
	}

	// Token: 0x06003424 RID: 13348 RVA: 0x0018543C File Offset: 0x0018363C
	protected virtual bool isAugmentationExists(string pId)
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003425 RID: 13349 RVA: 0x00185443 File Offset: 0x00183643
	protected virtual void loadEditorSelectedButton(TAugmentationButton pButton, string pAugmentationId)
	{
		pButton.removeClickAction(new AugmentationButtonClickAction(this.scrollToGroupStarter));
		pButton.addClickAction(new AugmentationButtonClickAction(this.scrollToGroupStarter));
	}

	// Token: 0x06003426 RID: 13350 RVA: 0x00185475 File Offset: 0x00183675
	protected virtual void createButton(TAugmentation pElement, TAugmentationGroup pGroup)
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003427 RID: 13351 RVA: 0x0018547C File Offset: 0x0018367C
	protected virtual bool hasAugmentation(TAugmentationButton pButton)
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003428 RID: 13352 RVA: 0x00185483 File Offset: 0x00183683
	protected virtual bool addAugmentation(TAugmentationButton pButton)
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003429 RID: 13353 RVA: 0x0018548A File Offset: 0x0018368A
	protected virtual bool removeAugmentation(TAugmentationButton pButton)
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600342A RID: 13354 RVA: 0x00185491 File Offset: 0x00183691
	public WindowMetaTab getEditorTab()
	{
		return this._editor_tab;
	}

	// Token: 0x0600342B RID: 13355 RVA: 0x00185499 File Offset: 0x00183699
	protected bool isAugmentationAvailable(TAugmentationButton pButton)
	{
		return pButton.getElementAsset().isAvailable();
	}

	// Token: 0x0400274E RID: 10062
	private const float FOCUS_SCROLL_OFFSET_TOP = -5f;

	// Token: 0x0400274F RID: 10063
	private const float FOCUS_SCROLL_OFFSET_BOTTOM = 1f;

	// Token: 0x04002750 RID: 10064
	public const float FOCUS_SCROLL_DURATION = 0.3f;

	// Token: 0x04002751 RID: 10065
	[SerializeField]
	protected Image art;

	// Token: 0x04002752 RID: 10066
	public TAugmentationButton prefab_augmentation;

	// Token: 0x04002753 RID: 10067
	public TAugmentationEditorButton prefab_editor_augmentation;

	// Token: 0x04002754 RID: 10068
	public TAugmentationGroup prefab_augmentation_group;

	// Token: 0x04002755 RID: 10069
	protected readonly Dictionary<string, TAugmentationGroup> dict_groups = new Dictionary<string, TAugmentationGroup>();

	// Token: 0x04002756 RID: 10070
	protected readonly List<TAugmentationEditorButton> all_augmentation_buttons = new List<TAugmentationEditorButton>();

	// Token: 0x04002757 RID: 10071
	protected TAugmentationWindow augmentation_window;

	// Token: 0x04002758 RID: 10072
	protected ObjectPoolGenericMono<TAugmentationButton> selected_editor_buttons;

	// Token: 0x04002759 RID: 10073
	[SerializeField]
	private WindowMetaTab _editor_tab;
}
