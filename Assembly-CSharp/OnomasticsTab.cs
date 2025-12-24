using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000720 RID: 1824
public class OnomasticsTab : OnomasticsNameGenerator
{
	// Token: 0x06003A1E RID: 14878 RVA: 0x0019C099 File Offset: 0x0019A299
	private void OnEnable()
	{
		this.create();
		this.showCategoryGroups();
	}

	// Token: 0x06003A1F RID: 14879 RVA: 0x0019C0A8 File Offset: 0x0019A2A8
	private void showCategoryGroups()
	{
		this._tab_groups.gameObject.SetActive(true);
		this._tab_groups.clearButtons();
		this._tab_groups.tryAddButton("ui/Icons/actor_traits/iconAttractive", "tab_onomastics_unit", new TabToggleAction(this.loadNameSet), delegate
		{
			this._name_set_type = MetaType.Unit;
		});
		this._tab_groups.tryAddButton("ui/Icons/iconFamilyList", "tab_onomastics_family", new TabToggleAction(this.loadNameSet), delegate
		{
			this._name_set_type = MetaType.Family;
		});
		this._tab_groups.tryAddButton("ui/Icons/iconClanList", "tab_onomastics_clan", new TabToggleAction(this.loadNameSet), delegate
		{
			this._name_set_type = MetaType.Clan;
		});
		this._tab_groups.tryAddButton("ui/Icons/iconCityList", "tab_onomastics_city", new TabToggleAction(this.loadNameSet), delegate
		{
			this._name_set_type = MetaType.City;
		});
		this._tab_groups.tryAddButton("ui/Icons/iconKingdomList", "tab_onomastics_kingdom", new TabToggleAction(this.loadNameSet), delegate
		{
			this._name_set_type = MetaType.Kingdom;
		});
		this._tab_groups.enableFirst();
	}

	// Token: 0x06003A20 RID: 14880 RVA: 0x0019C1C0 File Offset: 0x0019A3C0
	private void openFirstGroup()
	{
		using (ListPool<OnomasticsAssetButton> tCurButtons = this.getActiveButtons(this.parent_asset_groups))
		{
			for (int i = 0; i < tCurButtons.Count; i++)
			{
				OnomasticsAssetButton tButton = tCurButtons[i];
				if (tButton.onomastics_asset.id == "group_1")
				{
					this.openGroup(tButton);
					break;
				}
			}
		}
	}

	// Token: 0x06003A21 RID: 14881 RVA: 0x0019C230 File Offset: 0x0019A430
	private void Update()
	{
		if (this._selected_editor_group != null)
		{
			this.selected_icon_effect.transform.position = this._selected_editor_group_button.transform.position;
			Vector3 tNewPos = this._selected_editor_group_button.transform.position;
			tNewPos.y += 30f;
			this.selected_icon_effect_2.transform.position = tNewPos;
		}
		base.updateNameGeneration(this._onomastics_data);
		this.text_counter.text = string.Format("{0}/{1}", this._pool_buttons.countActive().ToString(), 30);
	}

	// Token: 0x06003A22 RID: 14882 RVA: 0x0019C2D2 File Offset: 0x0019A4D2
	private void LateUpdate()
	{
		this.checkButtonsAndEffects();
	}

	// Token: 0x06003A23 RID: 14883 RVA: 0x0019C2DC File Offset: 0x0019A4DC
	private void checkButtonsAndEffects()
	{
		this._pool_boxed_effects.clear(true);
		this._pool_word_effects.clear(true);
		OnomasticsAssetButton tWordStart = null;
		using (ListPool<OnomasticsAssetButton> tCurButtons = this.getActiveButtons(this.parent_name_variation_1))
		{
			for (int i = 0; i < tCurButtons.Count; i++)
			{
				OnomasticsAssetButton tThisButton = tCurButtons[i];
				OnomasticsAssetButton tNextButton = null;
				if (tThisButton.onomastics_asset.is_word_divider)
				{
					tWordStart = null;
				}
				else if (tWordStart == null)
				{
					tWordStart = tThisButton;
				}
				if (i + 1 < tCurButtons.Count)
				{
					tNextButton = tCurButtons[i + 1];
				}
				if (tThisButton.onomastics_asset.affects_left_word)
				{
					this.showWordBox(tWordStart, tThisButton);
				}
				if (!tThisButton.onomastics_asset.is_immune && tNextButton != null && tNextButton.onomastics_asset.affects_left && (!tNextButton.onomastics_asset.affects_left_group_only || tThisButton.onomastics_asset.isGroupType()))
				{
					this.showEffectBox(tThisButton, tNextButton);
				}
			}
		}
	}

	// Token: 0x06003A24 RID: 14884 RVA: 0x0019C3E0 File Offset: 0x0019A5E0
	private ListPool<OnomasticsAssetButton> getActiveButtons(Transform pTransform)
	{
		ListPool<OnomasticsAssetButton> tResult = new ListPool<OnomasticsAssetButton>(pTransform.childCount);
		for (int i = 0; i < pTransform.childCount; i++)
		{
			OnomasticsAssetButton tButton = pTransform.GetChild(i).GetComponent<OnomasticsAssetButton>();
			if (!(tButton == null) && tButton.gameObject.activeSelf)
			{
				tResult.Add(tButton);
			}
		}
		return tResult;
	}

	// Token: 0x06003A25 RID: 14885 RVA: 0x0019C438 File Offset: 0x0019A638
	private void showEffectBox(OnomasticsAssetButton pButton1, OnomasticsAssetButton pButton2)
	{
		Image next = this._pool_boxed_effects.getNext();
		RectTransform tEffectRect = next.GetComponent<RectTransform>();
		Vector3[] tWorldCorners = new Vector3[4];
		Vector3[] tWorldCorners2 = new Vector3[4];
		pButton1.getRect().GetWorldCorners(tWorldCorners);
		pButton2.getRect().GetWorldCorners(tWorldCorners2);
		float tMinX = Mathf.Min(tWorldCorners[0].x, tWorldCorners2[0].x);
		float tMaxX = Mathf.Max(tWorldCorners[2].x, tWorldCorners2[2].x);
		float tMinY = Mathf.Min(tWorldCorners[0].y, tWorldCorners2[0].y);
		float tMaxY = Mathf.Max(tWorldCorners[2].y, tWorldCorners2[2].y);
		Vector3 tMinPoint = next.transform.parent.InverseTransformPoint(new Vector3(tMinX, tMinY, 0f));
		Vector3 tMaxPoint = next.transform.parent.InverseTransformPoint(new Vector3(tMaxX, tMaxY, 0f));
		float tBonusSizeX = 10f;
		float tBonusSizeY = 3f;
		tEffectRect.anchoredPosition = new Vector2((tMinPoint.x + tMaxPoint.x) / 2f, (tMinPoint.y + tMaxPoint.y) / 2f);
		tEffectRect.sizeDelta = new Vector2(tMaxPoint.x - tMinPoint.x + tBonusSizeX, tMaxPoint.y - tMinPoint.y + tBonusSizeY);
		tEffectRect.localScale = new Vector3(0.8f, 0.8f, 0.8f);
	}

	// Token: 0x06003A26 RID: 14886 RVA: 0x0019C5C4 File Offset: 0x0019A7C4
	private void showWordBox(OnomasticsAssetButton pButton1, OnomasticsAssetButton pButton2)
	{
		Image next = this._pool_word_effects.getNext();
		RectTransform tEffectRect = next.GetComponent<RectTransform>();
		Vector3[] tWorldCorners = new Vector3[4];
		Vector3[] tWorldCorners2 = new Vector3[4];
		pButton1.getRect().GetWorldCorners(tWorldCorners);
		pButton2.getRect().GetWorldCorners(tWorldCorners2);
		float tMinX = Mathf.Min(tWorldCorners[0].x, tWorldCorners2[0].x);
		float tMaxX = Mathf.Max(tWorldCorners[2].x, tWorldCorners2[2].x);
		float tMinY = Mathf.Min(tWorldCorners[0].y, tWorldCorners2[0].y);
		float tMaxY = Mathf.Max(tWorldCorners[2].y, tWorldCorners2[2].y);
		Vector3 tMinPoint = next.transform.parent.InverseTransformPoint(new Vector3(tMinX, tMinY, 0f));
		Vector3 tMaxPoint = next.transform.parent.InverseTransformPoint(new Vector3(tMaxX, tMaxY, 0f));
		float tBonusSizeX = 2f;
		float tBonusSizeY = 2f;
		tEffectRect.anchoredPosition = new Vector2((tMinPoint.x + tMaxPoint.x) / 2f, (tMinPoint.y + tMaxPoint.y) / 2f);
		tEffectRect.sizeDelta = new Vector2(tMaxPoint.x - tMinPoint.x + tBonusSizeX, tMaxPoint.y - tMinPoint.y + tBonusSizeY);
		tEffectRect.localScale = new Vector3(1f, 1f, 1f);
	}

	// Token: 0x06003A27 RID: 14887 RVA: 0x0019C74E File Offset: 0x0019A94E
	private void loadOnomasicsData()
	{
		this._onomastics_data = SelectedMetas.selected_culture.getOnomasticData(this._name_set_type, false);
		this.loadInitialButtons();
	}

	// Token: 0x06003A28 RID: 14888 RVA: 0x0019C770 File Offset: 0x0019A970
	private void loadInitialButtons()
	{
		this._pool_buttons.clear(true);
		List<string> tListFull = this._onomastics_data.getFullTemplateData();
		for (int i = 0; i < tListFull.Count; i++)
		{
			string tID = tListFull[i];
			this.loadTemplateButton(tID, false);
		}
	}

	// Token: 0x06003A29 RID: 14889 RVA: 0x0019C7B6 File Offset: 0x0019A9B6
	public OnomasticsData getOnomasticsData()
	{
		return this._onomastics_data;
	}

	// Token: 0x06003A2A RID: 14890 RVA: 0x0019C7BE File Offset: 0x0019A9BE
	protected void OnDisable()
	{
		if (this._editor_input == null)
		{
			return;
		}
		this._editor_input.inputField.DeactivateInputField();
	}

	// Token: 0x06003A2B RID: 14891 RVA: 0x0019C7DF File Offset: 0x0019A9DF
	protected virtual void initNameInput()
	{
		if (this._editor_input == null)
		{
			return;
		}
		this._editor_input.addListener(new UnityAction<string>(this.applyInputName));
	}

	// Token: 0x06003A2C RID: 14892 RVA: 0x0019C808 File Offset: 0x0019AA08
	private void applyInputName(string pString)
	{
		pString = pString.Replace("\n", " ");
		pString = pString.Replace("\r", " ");
		while (pString.Contains("  "))
		{
			pString = pString.Replace("  ", " ");
		}
		pString = pString.Trim();
		if (this._onomastics_data.setGroup(this._selected_editor_group.id, pString))
		{
			this.resetNameGenerationTextBox();
		}
	}

	// Token: 0x06003A2D RID: 14893 RVA: 0x0019C884 File Offset: 0x0019AA84
	private void resetNameGenerationTextBox()
	{
		base.clickRegenerate();
		using (ListPool<OnomasticsAssetButton> tCurButtons = this.getActiveButtons(this.parent_name_variation_1))
		{
			using (ListPool<string> tList = new ListPool<string>(tCurButtons.Count))
			{
				for (int i = 0; i < tCurButtons.Count; i++)
				{
					OnomasticsAssetButton tButton = tCurButtons[i];
					tList.Add(tButton.onomastics_asset.id);
				}
				this._onomastics_data.setTemplateData(tList);
				if (this.name_variation_1_drag_container.rect_transform != null)
				{
					LayoutRebuilder.ForceRebuildLayoutImmediate(this.name_variation_1_drag_container.rect_transform);
				}
			}
		}
	}

	// Token: 0x06003A2E RID: 14894 RVA: 0x0019C93C File Offset: 0x0019AB3C
	private void create()
	{
		if (this._created)
		{
			return;
		}
		DragOrderContainer dragOrderContainer = this.name_variation_1_drag_container;
		dragOrderContainer.on_order_changed = (Action)Delegate.Combine(dragOrderContainer.on_order_changed, new Action(this.resetNameGenerationTextBox));
		this._pool_boxed_effects = new ObjectPoolGenericMono<Image>(this.boxed_effect_prefab, this.boxed_effects_transform);
		this._pool_word_effects = new ObjectPoolGenericMono<Image>(this.word_effect_prefab, this.word_effects_transform);
		Transform transform = base.transform.FindRecursive("Group Editor Element");
		this._editor_input = ((transform != null) ? transform.GetComponent<NameInput>() : null);
		this.initNameInput();
		this._created = true;
		this._pool_buttons = new ObjectPoolGenericMono<OnomasticsAssetButton>(this.prefab_button_template, this.parent_name_variation_1);
		foreach (OnomasticsAsset tAsset in AssetManager.onomastics_library.list)
		{
			Transform tTransformTarget;
			if (tAsset.isGroupType())
			{
				tTransformTarget = this.parent_asset_groups;
			}
			else
			{
				tTransformTarget = this.parent_asset_specials;
			}
			OnomasticsAssetButton tB = Object.Instantiate<OnomasticsAssetButton>(this.prefab_button, tTransformTarget);
			this.setupButton(tB, tAsset);
			if (tAsset.isGroupType())
			{
				tB.GetComponent<TipButton>().showOnClick = false;
				tB.GetComponent<DraggableLayoutElement>().enabled = false;
			}
			tB.button.onClick.AddListener(delegate()
			{
				this.clickAssetButton(tB);
			});
		}
	}

	// Token: 0x06003A2F RID: 14895 RVA: 0x0019CAC8 File Offset: 0x0019ACC8
	private void clickAssetButton(OnomasticsAssetButton pButton)
	{
		if (!InputHelpers.mouseSupported)
		{
			if (!Tooltip.isShowingFor(pButton))
			{
				pButton.showTooltip();
				return;
			}
			Tooltip.hideTooltip();
		}
		if (this._selected_editor_group != pButton.onomastics_asset && pButton.isGroupType())
		{
			this.openGroup(pButton);
			return;
		}
		if (pButton.isGroupType() && this._onomastics_data.isGroupEmpty(pButton.onomastics_asset.id))
		{
			this.punch(this.parent_asset_editor_group.transform, 1f, 0.1f, 0.3f);
			return;
		}
		if (this._pool_buttons.countActive() >= 30)
		{
			this.punch(this.parent_name_variation_1.parent, 1f, 0.1f, 0.3f);
			return;
		}
		this.punch(pButton.transform, 1f, 0.1f, 0.3f);
		this.loadTemplateButton(pButton.onomastics_asset.id, true);
		this.resetNameGenerationTextBox();
	}

	// Token: 0x06003A30 RID: 14896 RVA: 0x0019CBB2 File Offset: 0x0019ADB2
	private void punch(Transform pTransformTarget, float pDefaultScale = 1f, float pPower = 0.1f, float pDuration = 0.3f)
	{
		pTransformTarget.DOKill(true);
		pTransformTarget.localScale = new Vector3(pDefaultScale, pDefaultScale, pDefaultScale);
		pTransformTarget.DOPunchScale(new Vector3(pPower, pPower, pPower), pDuration, 10, 1f);
	}

	// Token: 0x06003A31 RID: 14897 RVA: 0x0019CBE2 File Offset: 0x0019ADE2
	private void setupButton(OnomasticsAssetButton pButton, OnomasticsAsset pAsset)
	{
		pButton.setupButton(pAsset, new GetCurrentOnomasticsData(this.getOnomasticsData));
	}

	// Token: 0x06003A32 RID: 14898 RVA: 0x0019CBF8 File Offset: 0x0019ADF8
	private void setupButton(OnomasticsAssetButton pButton, string pAssetID)
	{
		OnomasticsAsset tAsset = AssetManager.onomastics_library.get(pAssetID);
		this.setupButton(pButton, tAsset);
	}

	// Token: 0x06003A33 RID: 14899 RVA: 0x0019CC1C File Offset: 0x0019AE1C
	private void loadTemplateButton(string pID, bool pPunch = false)
	{
		OnomasticsAssetButton tNewButton = this._pool_buttons.getNext();
		this.setupButton(tNewButton, pID);
		tNewButton.transform.SetAsLastSibling();
		tNewButton.button.onClick.AddListener(delegate()
		{
			this.clickToRemoveButton(tNewButton);
		});
		this.punch(tNewButton.transform, 1f, 0.1f, 0.3f);
	}

	// Token: 0x06003A34 RID: 14900 RVA: 0x0019CCA5 File Offset: 0x0019AEA5
	private void clickToRemoveButton(OnomasticsAssetButton pButton)
	{
		this._pool_buttons.release(pButton, true);
		this.resetNameGenerationTextBox();
		Tooltip.blockTooltips(0.01f);
	}

	// Token: 0x06003A35 RID: 14901 RVA: 0x0019CCC4 File Offset: 0x0019AEC4
	public void openGroup(OnomasticsAssetButton pButton)
	{
		this._selected_editor_group = pButton.onomastics_asset;
		this._selected_editor_group_button = pButton;
		this.parent_asset_editor_group.gameObject.SetActive(true);
		this._editor_input.setText(this._onomastics_data.getGroupString(this._selected_editor_group.id));
		this.icon_last_selected_group.sprite = pButton.onomastics_asset.getSprite();
	}

	// Token: 0x06003A36 RID: 14902 RVA: 0x0019CD2C File Offset: 0x0019AF2C
	public void loadFromTemplate(bool pReset = false)
	{
		this._onomastics_data = SelectedMetas.selected_culture.getOnomasticData(this._name_set_type, pReset);
		this.loadInitialButtons();
		this.openFirstGroup();
		this.resetNameGenerationTextBox();
	}

	// Token: 0x06003A37 RID: 14903 RVA: 0x0019CD57 File Offset: 0x0019AF57
	public void loadNameSet()
	{
		this.loadFromTemplate(false);
	}

	// Token: 0x06003A38 RID: 14904 RVA: 0x0019CD60 File Offset: 0x0019AF60
	public void resetTemplate()
	{
		this.loadFromTemplate(true);
	}

	// Token: 0x06003A39 RID: 14905 RVA: 0x0019CD69 File Offset: 0x0019AF69
	public void clickRegenerateNames()
	{
		this.resetNameGenerationTextBox();
	}

	// Token: 0x06003A3A RID: 14906 RVA: 0x0019CD74 File Offset: 0x0019AF74
	public void randomEverything()
	{
		this._onomastics_data.clearTemplateData();
		int tRandomGroups = Randy.randomInt(3, 5);
		bool tVowels = Randy.randomBool();
		for (int i = 1; i <= tRandomGroups; i++)
		{
			string tGeneratedParts;
			if (tVowels)
			{
				tGeneratedParts = this.getRandomVowels();
			}
			else
			{
				tGeneratedParts = this.getRandomConsonants();
			}
			tVowels = !tVowels;
			this._onomastics_data.setGroup("group_" + i.ToString(), tGeneratedParts);
		}
		this.fillRandomCards();
		this.loadInitialButtons();
		this.openFirstGroup();
		this.resetNameGenerationTextBox();
	}

	// Token: 0x06003A3B RID: 14907 RVA: 0x0019CDF4 File Offset: 0x0019AFF4
	public void randomCards()
	{
		this.fillRandomCards();
		this.loadInitialButtons();
		this.openFirstGroup();
		this.resetNameGenerationTextBox();
	}

	// Token: 0x06003A3C RID: 14908 RVA: 0x0019CE10 File Offset: 0x0019B010
	public void fillRandomCards()
	{
		this._onomastics_data.clearTemplateData();
		using (ListPool<string> tRandomGroups = new ListPool<string>())
		{
			using (new ListPool<string>())
			{
				foreach (KeyValuePair<string, OnomasticsDataGroup> tPair in this._onomastics_data.groups)
				{
					if (!tPair.Value.isEmpty())
					{
						tRandomGroups.Add(tPair.Key);
					}
				}
				int tRandomAmount = Randy.randomInt(2, 6);
				for (int i = 0; i < tRandomAmount; i++)
				{
					string tRandomID = tRandomGroups.GetRandom<string>();
					this._onomastics_data.addToTemplateData(tRandomID);
				}
				for (int j = 0; j < tRandomAmount / 2; j++)
				{
					OnomasticsAsset tAsset = AssetManager.onomastics_library.list_special.GetRandom<OnomasticsAsset>();
					this._onomastics_data.addToTemplateData(tAsset.id);
				}
				this._onomastics_data.shuffleAllCards();
			}
		}
	}

	// Token: 0x06003A3D RID: 14909 RVA: 0x0019CF30 File Offset: 0x0019B130
	private string getRandomVowels()
	{
		string tResult = string.Empty;
		int tAmount = Randy.randomInt(2, 4);
		for (int i = 0; i < tAmount; i++)
		{
			tResult = tResult + OnomasticsTab.vowel_combinations[Randy.randomInt(0, OnomasticsTab.vowel_combinations.Length)] + " ";
		}
		return tResult;
	}

	// Token: 0x06003A3E RID: 14910 RVA: 0x0019CF78 File Offset: 0x0019B178
	private string getRandomConsonants()
	{
		string tResult = string.Empty;
		int tAmount = Randy.randomInt(2, 4);
		for (int i = 0; i < tAmount; i++)
		{
			tResult = tResult + OnomasticsTab.consonant_combinations[Randy.randomInt(0, OnomasticsTab.consonant_combinations.Length)] + " ";
		}
		return tResult;
	}

	// Token: 0x06003A3F RID: 14911 RVA: 0x0019CFC0 File Offset: 0x0019B1C0
	public void saveToLibrary()
	{
		string tCurrentTemplate = OnomasticsDropdown.current_template;
		int tCurrentTemplateIndex = OnomasticsDropdown.current_template_index;
		Debug.Log("Saving to library: " + tCurrentTemplate + " " + tCurrentTemplateIndex.ToString());
		string tShortTemplate = this._onomastics_data.getShortTemplate();
		NameGeneratorAsset tTemplate = AssetManager.name_generator.get(tCurrentTemplate);
		if (tCurrentTemplateIndex >= tTemplate.onomastics_templates.Count)
		{
			tTemplate.onomastics_templates.Add(tShortTemplate);
		}
		else
		{
			tTemplate.onomastics_templates[tCurrentTemplateIndex] = tShortTemplate;
		}
		AssetManager.name_generator.exportAssets();
	}

	// Token: 0x06003A40 RID: 14912 RVA: 0x0019D040 File Offset: 0x0019B240
	public void saveToClipboard()
	{
		string tShortTemplate = this._onomastics_data.getShortTemplate();
		GUIUtility.systemCopyBuffer = "`" + tShortTemplate + "`";
		WorldTip.showNow("onomastics_exported", true, "top", 3f, "#F3961F");
	}

	// Token: 0x06003A41 RID: 14913 RVA: 0x0019D088 File Offset: 0x0019B288
	public void saveNamesToClipboard()
	{
		string tShortTemplate = "";
		tShortTemplate += "## Template: \n\n";
		tShortTemplate += this._onomastics_data.getShortTemplate();
		tShortTemplate += "\n\n";
		if (this._onomastics_data.isGendered())
		{
			string tMaleNames = "";
			string tFemaleNames = "";
			for (int i = 0; i < 25; i++)
			{
				string tNewName = this._onomastics_data.generateName(ActorSex.Male, 0, null);
				tMaleNames = tMaleNames + "- " + tNewName + "\n";
				tNewName = this._onomastics_data.generateName(ActorSex.Female, 0, null);
				tFemaleNames = tFemaleNames + "- " + tNewName + "\n";
			}
			tShortTemplate = tShortTemplate + "## Male names: \n\n" + tMaleNames;
			tShortTemplate += "\n";
			tShortTemplate = tShortTemplate + "## Female names: \n\n" + tFemaleNames;
		}
		else
		{
			tShortTemplate += "## Generated names: \n\n";
			for (int j = 0; j < 50; j++)
			{
				string tNewName2 = this._onomastics_data.generateName(ActorSex.None, 0, null);
				tShortTemplate = tShortTemplate + "- " + tNewName2 + "\n";
			}
		}
		GUIUtility.systemCopyBuffer = tShortTemplate;
	}

	// Token: 0x06003A42 RID: 14914 RVA: 0x0019D1C0 File Offset: 0x0019B3C0
	public void loadFromClipboard()
	{
		string tShortTemplate = GUIUtility.systemCopyBuffer;
		this.loadTemplate(tShortTemplate);
	}

	// Token: 0x06003A43 RID: 14915 RVA: 0x0019D1DC File Offset: 0x0019B3DC
	public void loadTemplate(string pTemplate = null)
	{
		string tOld = this._onomastics_data.getShortTemplate();
		pTemplate = (((pTemplate != null) ? pTemplate.Trim(new char[]
		{
			'\n',
			'\r',
			' ',
			'"',
			'`'
		}) : null) ?? "");
		try
		{
			if (!this._onomastics_data.templateIsValid(pTemplate))
			{
				throw new ArgumentException("Invalid template format: (OT) " + pTemplate);
			}
			this._onomastics_data.loadFromShortTemplate(pTemplate);
		}
		catch (ArgumentException ex)
		{
			WorldTip.showNow("onomastics_import_error_invalid", true, "top", 3f, "#FF637D");
			Debug.LogWarning(ex.Message);
			return;
		}
		catch (Exception ex2)
		{
			WorldTip.showNow("onomastics_import_error_logs", true, "top", 3f, "#FF637D");
			Debug.LogWarning(ex2.Message);
			return;
		}
		Debug.Log("old: " + tOld.Trim(new char[]
		{
			'\n',
			'\r',
			' ',
			'"',
			'`'
		}));
		Debug.Log("new: " + pTemplate);
		WorldTip.showNow(pTemplate, false, "top", 3f, "#F3961F");
		this.loadInitialButtons();
		this.openFirstGroup();
		this.resetNameGenerationTextBox();
	}

	// Token: 0x06003A44 RID: 14916 RVA: 0x0019D314 File Offset: 0x0019B514
	public static string debugTemplateReport(string pTemplateName)
	{
		OnomasticsData tOnomasticsData = OnomasticsCache.getOriginalData(AssetManager.name_generator.get(pTemplateName).onomastics_templates.GetRandom<string>());
		string tShortTemplate = "";
		tShortTemplate += "## Template: \n\n";
		tShortTemplate += tOnomasticsData.getShortTemplate();
		tShortTemplate += "\n\n";
		if (tOnomasticsData.isGendered())
		{
			string tMaleNames = "";
			string tFemaleNames = "";
			for (int i = 0; i < 25; i++)
			{
				string tNewName = tOnomasticsData.generateName(ActorSex.Male, 0, null);
				if (i > 0)
				{
					tMaleNames += ", ";
				}
				tMaleNames += tNewName;
				tNewName = tOnomasticsData.generateName(ActorSex.Female, 0, null);
				if (i > 0)
				{
					tFemaleNames += ", ";
				}
				tFemaleNames += tNewName;
			}
			tShortTemplate = tShortTemplate + "## Male names: \n\n" + tMaleNames;
			tShortTemplate += "\n";
			tShortTemplate = tShortTemplate + "## Female names: \n\n" + tFemaleNames;
		}
		else
		{
			tShortTemplate += "## Generated names: \n\n";
			for (int j = 0; j < 50; j++)
			{
				string tNewName2 = tOnomasticsData.generateName(ActorSex.None, 0, null);
				if (j > 0)
				{
					tShortTemplate += ", ";
				}
				tShortTemplate += tNewName2;
			}
		}
		return tShortTemplate + "\n\n";
	}

	// Token: 0x04002AF9 RID: 11001
	private const int MAX_CARDS = 30;

	// Token: 0x04002AFA RID: 11002
	private const float BUTTON_SCALE = 1f;

	// Token: 0x04002AFB RID: 11003
	private const float WORD_SCALE = 1f;

	// Token: 0x04002AFC RID: 11004
	private const float EFFECT_SCALE = 0.8f;

	// Token: 0x04002AFD RID: 11005
	private const float WORD_BOX_SIZE_X = 2f;

	// Token: 0x04002AFE RID: 11006
	private const float WORD_BOX_SIZE_Y = 2f;

	// Token: 0x04002AFF RID: 11007
	private const float EFFECT_BOX_SIZE_X = 10f;

	// Token: 0x04002B00 RID: 11008
	private const float EFFECT_BOX_SIZE_Y = 3f;

	// Token: 0x04002B01 RID: 11009
	public Transform parent_name_variation_1;

	// Token: 0x04002B02 RID: 11010
	public DragOrderContainer name_variation_1_drag_container;

	// Token: 0x04002B03 RID: 11011
	public Transform parent_asset_groups;

	// Token: 0x04002B04 RID: 11012
	public Transform parent_asset_specials;

	// Token: 0x04002B05 RID: 11013
	public Transform parent_asset_editor_group;

	// Token: 0x04002B06 RID: 11014
	public Image icon_last_selected_group;

	// Token: 0x04002B07 RID: 11015
	public Text text_counter;

	// Token: 0x04002B08 RID: 11016
	public OnomasticsAssetButton prefab_button;

	// Token: 0x04002B09 RID: 11017
	public OnomasticsAssetButton prefab_button_template;

	// Token: 0x04002B0A RID: 11018
	private ObjectPoolGenericMono<OnomasticsAssetButton> _pool_buttons;

	// Token: 0x04002B0B RID: 11019
	protected NameInput _editor_input;

	// Token: 0x04002B0C RID: 11020
	private bool _created;

	// Token: 0x04002B0D RID: 11021
	private OnomasticsData _onomastics_data;

	// Token: 0x04002B0E RID: 11022
	private MetaType _name_set_type = MetaType.Unit;

	// Token: 0x04002B0F RID: 11023
	private OnomasticsAsset _selected_editor_group;

	// Token: 0x04002B10 RID: 11024
	private OnomasticsAssetButton _selected_editor_group_button;

	// Token: 0x04002B11 RID: 11025
	public Image selected_icon_effect;

	// Token: 0x04002B12 RID: 11026
	public Image selected_icon_effect_2;

	// Token: 0x04002B13 RID: 11027
	private ObjectPoolGenericMono<Image> _pool_boxed_effects;

	// Token: 0x04002B14 RID: 11028
	public Image boxed_effect_prefab;

	// Token: 0x04002B15 RID: 11029
	public Transform boxed_effects_transform;

	// Token: 0x04002B16 RID: 11030
	private ObjectPoolGenericMono<Image> _pool_word_effects;

	// Token: 0x04002B17 RID: 11031
	public Image word_effect_prefab;

	// Token: 0x04002B18 RID: 11032
	public Transform word_effects_transform;

	// Token: 0x04002B19 RID: 11033
	[SerializeField]
	private TabTogglesGroup _tab_groups;

	// Token: 0x04002B1A RID: 11034
	private static readonly string[] consonant_combinations = new string[]
	{
		"b",
		"c",
		"d",
		"f",
		"g",
		"h",
		"j",
		"k",
		"l",
		"m",
		"n",
		"p",
		"q",
		"r",
		"s",
		"t",
		"v",
		"w",
		"x",
		"y",
		"z",
		"st",
		"bl",
		"tr",
		"pr",
		"cl",
		"kr",
		"fr",
		"gr",
		"pl"
	};

	// Token: 0x04002B1B RID: 11035
	private static readonly string[] vowel_combinations = new string[]
	{
		"a",
		"e",
		"i",
		"o",
		"u",
		"ai",
		"ei",
		"oi",
		"au",
		"ou",
		"ie",
		"ee",
		"oa",
		"ea",
		"io",
		"ia",
		"ui",
		"ue",
		"oo",
		"ae",
		"ya",
		"yo",
		"ye",
		"wa",
		"we",
		"wi",
		"wo",
		"ua",
		"eu",
		"iu"
	};
}
