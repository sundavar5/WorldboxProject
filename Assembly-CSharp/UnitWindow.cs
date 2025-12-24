using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006E9 RID: 1769
public class UnitWindow : StatsWindow, ITraitWindow<ActorTrait, ActorTraitButton>, IAugmentationsWindow<ITraitsEditor<ActorTrait>>, IEquipmentWindow, IAugmentationsWindow<IEquipmentEditor>, IPlotsWindow, IAugmentationsWindow<IPlotsEditor>
{
	// Token: 0x17000323 RID: 803
	// (get) Token: 0x060038C1 RID: 14529 RVA: 0x00196273 File Offset: 0x00194473
	public override MetaType meta_type
	{
		get
		{
			return MetaType.Unit;
		}
	}

	// Token: 0x17000324 RID: 804
	// (get) Token: 0x060038C2 RID: 14530 RVA: 0x00196277 File Offset: 0x00194477
	internal Actor actor
	{
		get
		{
			return SelectedUnit.unit;
		}
	}

	// Token: 0x060038C3 RID: 14531 RVA: 0x00196280 File Offset: 0x00194480
	protected virtual bool onNameChange(string pInput)
	{
		if (string.IsNullOrWhiteSpace(pInput))
		{
			return false;
		}
		if (this.actor.isRekt())
		{
			return false;
		}
		string tName = pInput.Trim();
		if (this._initial_name == tName)
		{
			return false;
		}
		this.actor.data.custom_name = true;
		this.actor.setName(tName, true);
		this._initial_name = tName;
		this.name_input.SetOutline();
		foreach (City tCity in World.world.cities)
		{
			if (!tCity.isRekt())
			{
				tCity.updateRulers();
				if (tCity.data.founder_id == this.actor.getID())
				{
					tCity.data.founder_name = this.actor.data.name;
				}
			}
		}
		foreach (Army tArmy in World.world.armies)
		{
			if (!tArmy.isRekt())
			{
				tArmy.updateCaptains();
			}
		}
		foreach (Kingdom tKingdom in World.world.kingdoms)
		{
			if (!tKingdom.isRekt())
			{
				tKingdom.updateRulers();
			}
		}
		foreach (War tWar in World.world.wars)
		{
			if (!tWar.isRekt() && tWar.data.started_by_actor_id == this.actor.getID())
			{
				tWar.data.started_by_actor_name = this.actor.data.name;
			}
		}
		foreach (Alliance tAlliance in World.world.alliances)
		{
			if (!tAlliance.isRekt() && tAlliance.data.founder_actor_id == this.actor.getID())
			{
				tAlliance.data.founder_actor_name = this.actor.data.name;
			}
		}
		foreach (Religion tReligion in World.world.religions)
		{
			if (!tReligion.isRekt() && tReligion.data.creator_id == this.actor.getID())
			{
				tReligion.data.creator_name = this.actor.data.name;
			}
		}
		foreach (Culture tCulture in World.world.cultures)
		{
			if (!tCulture.isRekt() && tCulture.data.creator_id == this.actor.getID())
			{
				tCulture.data.creator_name = this.actor.data.name;
			}
		}
		foreach (Clan tClan in World.world.clans)
		{
			if (!tClan.isRekt())
			{
				if (tClan.data.founder_actor_id == this.actor.getID())
				{
					tClan.data.founder_actor_name = this.actor.data.name;
				}
				tClan.updateChiefs();
			}
		}
		foreach (Language tLanguage in World.world.languages)
		{
			if (!tLanguage.isRekt() && tLanguage.data.creator_id == this.actor.getID())
			{
				tLanguage.data.creator_name = this.actor.data.name;
			}
		}
		foreach (Family tFamily in World.world.families)
		{
			if (!tFamily.isRekt())
			{
				if (tFamily.data.main_founder_id_1 == this.actor.getID())
				{
					tFamily.data.founder_actor_name_1 = this.actor.data.name;
				}
				if (tFamily.data.main_founder_id_2 == this.actor.getID())
				{
					tFamily.data.founder_actor_name_2 = this.actor.data.name;
				}
			}
		}
		foreach (Book tBook in World.world.books)
		{
			if (!tBook.isRekt() && tBook.data.author_id == this.actor.getID())
			{
				tBook.data.author_name = this.actor.data.name;
			}
		}
		foreach (Plot tPlot in World.world.plots)
		{
			if (!tPlot.isRekt() && tPlot.data.founder_id == this.actor.getID())
			{
				tPlot.data.founder_name = this.actor.data.name;
			}
		}
		foreach (Item tItem in World.world.items)
		{
			if (!tItem.isRekt() && tItem.data.creator_id == this.actor.getID())
			{
				tItem.data.by = this.actor.data.name;
			}
		}
		return true;
	}

	// Token: 0x060038C4 RID: 14532 RVA: 0x00196928 File Offset: 0x00194B28
	internal override bool checkCancelWindow()
	{
		return this.actor.isRekt() || !this.actor.hasHealth() || base.checkCancelWindow();
	}

	// Token: 0x060038C5 RID: 14533 RVA: 0x00196950 File Offset: 0x00194B50
	private void clear()
	{
		this._button_trait_editor.toggleActive(false);
		this._button_equipment_editor.toggleActive(false);
		this._button_plots_tab.toggleActive(false);
		this._button_mind_tab.toggleActive(false);
		this._button_genealogy_tab.toggleActive(false);
		this._button_possession.toggleActive(false);
		this._icon_favorite.transform.parent.gameObject.SetActive(false);
		this._mood_bg.gameObject.SetActive(false);
		this.name_input.setText("");
	}

	// Token: 0x060038C6 RID: 14534 RVA: 0x001969E4 File Offset: 0x00194BE4
	protected override void OnEnable()
	{
		base.OnEnable();
		this.showInfo();
		if (!this.actor.asset.isAvailable() && !this.actor.asset.unlocked_with_achievement)
		{
			this.actor.asset.unlock(true);
		}
		AchievementLibrary.checkUnitAchievements(this.actor);
	}

	// Token: 0x060038C7 RID: 14535 RVA: 0x00196A3E File Offset: 0x00194C3E
	public void showInfo()
	{
		this.clear();
		this.loadNameInput();
		this.showMainInfo();
		this.checkShowMain();
	}

	// Token: 0x060038C8 RID: 14536 RVA: 0x00196A58 File Offset: 0x00194C58
	private void checkShowMain()
	{
		WindowMetaTab tCurrentTab = base.tabs.getActiveTab();
		if ((!(tCurrentTab == this._button_trait_editor) || this.isTraitsEditorAllowed()) && (!(tCurrentTab == this._button_equipment_editor) || this.isEquipmentEditorAllowed()) && (!(tCurrentTab == this._button_mind_tab) || this.isMindTabAllowed()) && (!(tCurrentTab == this._button_genealogy_tab) || this.isGenealogyTabAllowed()) && (!(tCurrentTab == this._button_plots_tab) || this.isPlotsTabAllowed()))
		{
			return;
		}
		base.showTab(base.tabs.tab_default);
	}

	// Token: 0x060038C9 RID: 14537 RVA: 0x00196AFC File Offset: 0x00194CFC
	internal override void showStatsRows()
	{
		this.tryShowPastNames();
		base.showStatRow("birthday", this.actor.getBirthday(), MetaType.None, -1L, null, null, null);
		base.showStatRow("task", this.actor.getTaskText(), MetaType.None, -1L, null, null, null);
		if (this.actor.asset.inspect_generation)
		{
			base.showStatRow("generation", this.actor.data.generation, MetaType.None, -1L, null, null, null);
		}
		if (this.actor.data.loot > 0)
		{
			base.showStatRow("loot", this.actor.data.loot, "#43FF43", MetaType.None, -1L, false, "iconLoot", null, null, true);
		}
		if (this.actor.data.money > 0)
		{
			base.showStatRow("money", this.actor.data.money, "#43FF43", MetaType.None, -1L, false, "iconMoney", null, null, true);
		}
		if (this.actor.inventory.hasResources())
		{
			base.showStatRow("resources", this.actor.inventory.getRandomResourceID().Localize(), MetaType.None, -1L, null, "carrying_resources", new TooltipDataGetter(this.getTooltipCarryingResources));
		}
		if (this.actor.hasBestFriend())
		{
			Actor tBestFriend = this.actor.getBestFriend();
			if (!tBestFriend.isRekt())
			{
				string tIconPath = "iconMale";
				if (tBestFriend.isSexFemale())
				{
					tIconPath = "iconFemale";
				}
				base.tryToShowActor("best_friend", -1L, null, tBestFriend, tIconPath);
			}
		}
		if (this.actor.hasLover())
		{
			Actor tLover = this.actor.lover;
			if (!this.actor.lover.isRekt())
			{
				string tIconPath2 = "iconMale";
				if (tLover.isSexFemale())
				{
					tIconPath2 = "iconFemale";
				}
				base.tryToShowActor("lover", -1L, null, tLover, tIconPath2);
			}
		}
		if (this.actor.hasFavoriteFood())
		{
			base.showStatRow("creature_statistics_favorite_food", this.actor.favorite_food_asset.getTranslatedName(), MetaType.None, -1L, null, null, null);
		}
		if (this.actor.isSapient() && this.actor.s_personality != null)
		{
			base.showStatRow("creature_statistics_personality", this.actor.s_personality.getTranslatedName(), MetaType.None, -1L, null, null, null);
		}
		if (this.actor.asset.is_boat)
		{
			Boat tBoat = this.actor.getSimpleComponent<Boat>();
			string tPassangersTooltipId;
			TooltipDataGetter tPassengersDataGetter;
			if (!tBoat.hasPassengers())
			{
				tPassangersTooltipId = null;
				tPassengersDataGetter = null;
			}
			else
			{
				tPassangersTooltipId = "passengers";
				tPassengersDataGetter = new TooltipDataGetter(this.getTooltipPassengers);
			}
			base.showStatRow("passengers", tBoat.countPassengers(), MetaType.None, -1L, null, tPassangersTooltipId, tPassengersDataGetter);
		}
		int tMassKG = this.actor.getMassKG();
		string tTooltipId;
		TooltipDataGetter tDataGetter;
		if (this.actor.asset.resources_given == null)
		{
			tTooltipId = null;
			tDataGetter = null;
		}
		else
		{
			tTooltipId = "mass";
			tDataGetter = new TooltipDataGetter(this.getTooltipMass);
		}
		base.showStatRow("mass", string.Format("{0} kg", tMassKG), MetaType.None, -1L, null, tTooltipId, tDataGetter);
		if (this.actor.asset.inspect_show_species)
		{
			base.tryToShowMetaSpecies("species", this.actor.asset.id);
		}
	}

	// Token: 0x060038CA RID: 14538 RVA: 0x00196E48 File Offset: 0x00195048
	public override void showMetaRows()
	{
		if (this.actor.asset.inspect_home)
		{
			this.meta_rows_container.tryToShowMetaCity("creature_statistics_home_village", -1L, null, this.actor.city, "iconCity");
		}
		if (this.actor.hasKingdom() && this.actor.isKingdomCiv())
		{
			this.meta_rows_container.tryToShowMetaKingdom("kingdom", -1L, null, this.actor.kingdom);
			if (this.actor.kingdom.hasAlliance())
			{
				this.meta_rows_container.tryToShowMetaAlliance("alliance", -1L, null, this.actor.kingdom.getAlliance());
			}
		}
		if (this.actor.hasClan())
		{
			this.meta_rows_container.tryToShowMetaClan("clan", -1L, null, this.actor.clan);
		}
		if (this.actor.hasLanguage())
		{
			this.meta_rows_container.tryToShowMetaLanguage("language", -1L, null, this.actor.language);
		}
		if (this.actor.hasReligion())
		{
			this.meta_rows_container.tryToShowMetaReligion("religion", -1L, null, this.actor.religion);
		}
		if (this.actor.hasFamily())
		{
			this.meta_rows_container.tryToShowMetaFamily("family", -1L, null, this.actor.family);
		}
		if (this.actor.hasCulture())
		{
			this.meta_rows_container.tryToShowMetaCulture("culture", -1L, null, this.actor.culture);
		}
		if (this.actor.asset.inspect_show_species && this.actor.hasSubspecies())
		{
			this.meta_rows_container.tryToShowMetaSubspecies("subspecies", -1L, null, this.actor.subspecies);
		}
	}

	// Token: 0x060038CB RID: 14539 RVA: 0x0019700B File Offset: 0x0019520B
	public TooltipData getTooltipMass()
	{
		return new TooltipData
		{
			tip_name = "mass",
			actor = this.actor
		};
	}

	// Token: 0x060038CC RID: 14540 RVA: 0x00197029 File Offset: 0x00195229
	private TooltipData getTooltipPassengers()
	{
		return new TooltipData
		{
			actor = this.actor
		};
	}

	// Token: 0x060038CD RID: 14541 RVA: 0x0019703C File Offset: 0x0019523C
	public TooltipData getTooltipCarryingResources()
	{
		return new TooltipData
		{
			tip_name = "carrying",
			actor = this.actor
		};
	}

	// Token: 0x060038CE RID: 14542 RVA: 0x0019705C File Offset: 0x0019525C
	private void loadNameInput()
	{
		if (this.actor.isRekt())
		{
			return;
		}
		this.name_input.inputField.onEndEdit.AddListener(delegate(string pString)
		{
			this.onNameChange(pString);
		});
		string tName = this.actor.getName().Trim();
		this._initial_name = tName;
		this.name_input.setText(tName);
		if (this.actor.data.custom_name)
		{
			this.name_input.SetOutline();
		}
	}

	// Token: 0x060038CF RID: 14543 RVA: 0x001970DC File Offset: 0x001952DC
	public void showMainInfo()
	{
		if (this.actor.isRekt())
		{
			return;
		}
		this.checkMainTabTitle();
		if (this.isTraitsEditorAllowed())
		{
			this._button_trait_editor.toggleActive(true);
		}
		if (this.isEquipmentEditorAllowed())
		{
			this._button_equipment_editor.toggleActive(true);
		}
		if (this.isPlotsTabAllowed())
		{
			this._button_plots_tab.toggleActive(true);
		}
		if (this.isMindTabAllowed())
		{
			this._button_mind_tab.toggleActive(true);
		}
		if (this.isGenealogyTabAllowed())
		{
			this._button_genealogy_tab.toggleActive(true);
		}
		if (this.actor.canBePossessed())
		{
			this._button_possession.toggleActive(true);
		}
		if (this.actor.isKingdomCiv())
		{
			this.name_input.textField.color = this.actor.kingdom.getColor().getColorText();
		}
		else
		{
			this.name_input.textField.color = Toolbox.color_log_neutral;
		}
		this._avatar_element.show(this.actor);
		if (this.actor.isSapient())
		{
			this._mood_bg.gameObject.SetActive(false);
		}
		if (this.actor.asset.can_be_favorited)
		{
			this._icon_favorite.transform.parent.gameObject.SetActive(true);
		}
		this.setIconValue("i_age", (float)this.actor.getAge(), null, "", false, "", '/');
		this.updateFavoriteIconFor(this.actor);
		this.checkEquipmentTabIcon();
	}

	// Token: 0x060038D0 RID: 14544 RVA: 0x00197260 File Offset: 0x00195460
	private void checkMainTabTitle()
	{
		ActorAsset tAsset = this.actor.asset;
		tAsset.getSpriteIcon();
		if (tAsset.is_boat)
		{
			this._main_tab_title.setKeyAndUpdate("boat");
			return;
		}
		this._main_tab_title.setKeyAndUpdate(tAsset.getLocaleID());
	}

	// Token: 0x060038D1 RID: 14545 RVA: 0x001972AA File Offset: 0x001954AA
	public void reloadBanner()
	{
		this._avatar_element.show(this.actor);
	}

	// Token: 0x060038D2 RID: 14546 RVA: 0x001972BD File Offset: 0x001954BD
	private bool isTraitsEditorAllowed()
	{
		return this.actor.asset.can_edit_traits;
	}

	// Token: 0x060038D3 RID: 14547 RVA: 0x001972CF File Offset: 0x001954CF
	private bool isEquipmentEditorAllowed()
	{
		return this.actor.canEditEquipment();
	}

	// Token: 0x060038D4 RID: 14548 RVA: 0x001972DC File Offset: 0x001954DC
	private bool isMindTabAllowed()
	{
		return this.actor.asset.inspect_mind;
	}

	// Token: 0x060038D5 RID: 14549 RVA: 0x001972EE File Offset: 0x001954EE
	private bool isGenealogyTabAllowed()
	{
		return this.actor.asset.inspect_genealogy;
	}

	// Token: 0x060038D6 RID: 14550 RVA: 0x00197300 File Offset: 0x00195500
	private bool isPlotsTabAllowed()
	{
		return this.actor.isCityLeader() || this.actor.isKing() || this.actor.hasClan();
	}

	// Token: 0x060038D7 RID: 14551 RVA: 0x00197329 File Offset: 0x00195529
	public void locateActorHouse()
	{
		if (this.actor.getHomeBuilding() == null)
		{
			return;
		}
		World.world.locatePosition(this.actor.getHomeBuilding().current_tile.posV3);
	}

	// Token: 0x060038D8 RID: 14552 RVA: 0x00197358 File Offset: 0x00195558
	private void updateFavoriteIconFor(Actor pUnit)
	{
		if (pUnit.isFavorite())
		{
			this._icon_favorite.color = ColorStyleLibrary.m.favorite_selected;
			return;
		}
		this._icon_favorite.color = ColorStyleLibrary.m.favorite_not_selected;
	}

	// Token: 0x060038D9 RID: 14553 RVA: 0x00197390 File Offset: 0x00195590
	public void pressFavorite()
	{
		if (this.actor.isRekt())
		{
			return;
		}
		this.actor.switchFavorite();
		this.updateFavoriteIconFor(this.actor);
		base.refreshMetaList();
		SpriteSwitcher.checkAllStates();
		if (this.actor.data.favorite)
		{
			WorldTip.showNowTop("tip_favorite_icon", true);
		}
	}

	// Token: 0x060038DA RID: 14554 RVA: 0x001973EA File Offset: 0x001955EA
	private void OnDisable()
	{
		this.name_input.inputField.DeactivateInputField();
	}

	// Token: 0x060038DB RID: 14555 RVA: 0x001973FC File Offset: 0x001955FC
	protected void setIconValue(string pName, float pMainVal, float? pMax = null, string pColor = "", bool pFloat = false, string pEnding = "", char pSeparator = '/')
	{
		Transform tTransform = base.transform.FindRecursive(pName);
		if (tTransform == null)
		{
			return;
		}
		StatsIcon component = tTransform.GetComponent<StatsIcon>();
		component.gameObject.SetActive(true);
		component.setValue(pMainVal, pMax, pColor, pFloat, pEnding, pSeparator, false);
	}

	// Token: 0x060038DC RID: 14556 RVA: 0x00197442 File Offset: 0x00195642
	public void clickOpenAssetDebug()
	{
		if (this.actor.isRekt())
		{
			return;
		}
		BaseDebugAssetElement<ActorAsset>.selected_asset = this.actor.asset;
		ScrollWindow.showWindow("actor_asset");
	}

	// Token: 0x060038DD RID: 14557 RVA: 0x0019746C File Offset: 0x0019566C
	public void clickGetLLMPrompt()
	{
		if (this.actor.isRekt())
		{
			return;
		}
		GUIUtility.systemCopyBuffer = GenerateLLMPrompt.getText(this.actor);
	}

	// Token: 0x060038DE RID: 14558 RVA: 0x0019748C File Offset: 0x0019568C
	public void checkEquipmentTabIcon()
	{
		if (!this.actor.hasEquipment())
		{
			return;
		}
		this._equipment_editor_icon.sprite = (this.actor.isWeaponFirearm() ? this._equipment_sprite_firearm : this._equipment_sprite_normal);
	}

	// Token: 0x060038DF RID: 14559 RVA: 0x001974C4 File Offset: 0x001956C4
	public void avatarTouchScream()
	{
		if (!this.actor.isRekt())
		{
			this.actor.pokeFromAvatarUI();
		}
		this._unit_text.spawnAvatarText(null);
		((IShakable)this.scroll_window).shake();
		ScrollWindow.randomDropHoveringIcon(1, 2);
		this.reloadBanner();
		base.tabs.reloadActiveTab();
	}

	// Token: 0x060038E0 RID: 14560 RVA: 0x00197518 File Offset: 0x00195718
	internal void tryShowPastNames()
	{
		List<NameEntry> past_names = this.actor.data.past_names;
		if (past_names != null && past_names.Count > 0)
		{
			string pId = "past_names";
			List<NameEntry> past_names2 = this.actor.data.past_names;
			base.showStatRow(pId, (past_names2 != null) ? past_names2.Count : 1, MetaType.None, -1L, "iconVillages", "past_names", new TooltipDataGetter(this.getTooltipPastNames));
		}
	}

	// Token: 0x060038E1 RID: 14561 RVA: 0x0019758C File Offset: 0x0019578C
	private TooltipData getTooltipPastNames()
	{
		return new TooltipData
		{
			tip_name = "past_names",
			past_names = new ListPool<NameEntry>(this.actor.data.past_names),
			meta_type = MetaType.Unit
		};
	}

	// Token: 0x060038E3 RID: 14563 RVA: 0x001975C9 File Offset: 0x001957C9
	T IAugmentationsWindow<ITraitsEditor<ActorTrait>>.GetComponentInChildren<T>(bool includeInactive)
	{
		return base.GetComponentInChildren<T>(includeInactive);
	}

	// Token: 0x060038E4 RID: 14564 RVA: 0x001975D2 File Offset: 0x001957D2
	T IAugmentationsWindow<IEquipmentEditor>.GetComponentInChildren<T>(bool includeInactive)
	{
		return base.GetComponentInChildren<T>(includeInactive);
	}

	// Token: 0x060038E5 RID: 14565 RVA: 0x001975DB File Offset: 0x001957DB
	T IAugmentationsWindow<IPlotsEditor>.GetComponentInChildren<T>(bool includeInactive)
	{
		return base.GetComponentInChildren<T>(includeInactive);
	}

	// Token: 0x04002A12 RID: 10770
	[SerializeField]
	private Image _mood_sprite;

	// Token: 0x04002A13 RID: 10771
	[SerializeField]
	private Image _mood_bg;

	// Token: 0x04002A14 RID: 10772
	public NameInput name_input;

	// Token: 0x04002A15 RID: 10773
	[SerializeField]
	private UiUnitAvatarElement _avatar_element;

	// Token: 0x04002A16 RID: 10774
	[SerializeField]
	private Image _icon_favorite;

	// Token: 0x04002A17 RID: 10775
	[SerializeField]
	private WindowMetaTab _button_trait_editor;

	// Token: 0x04002A18 RID: 10776
	[SerializeField]
	private WindowMetaTab _button_equipment_editor;

	// Token: 0x04002A19 RID: 10777
	[SerializeField]
	private WindowMetaTab _button_mind_tab;

	// Token: 0x04002A1A RID: 10778
	[SerializeField]
	private WindowMetaTab _button_genealogy_tab;

	// Token: 0x04002A1B RID: 10779
	[SerializeField]
	private WindowMetaTab _button_plots_tab;

	// Token: 0x04002A1C RID: 10780
	[SerializeField]
	private WindowMetaTab _button_possession;

	// Token: 0x04002A1D RID: 10781
	[SerializeField]
	private Image _equipment_editor_icon;

	// Token: 0x04002A1E RID: 10782
	[SerializeField]
	private Sprite _equipment_sprite_normal;

	// Token: 0x04002A1F RID: 10783
	[SerializeField]
	private Sprite _equipment_sprite_firearm;

	// Token: 0x04002A20 RID: 10784
	[SerializeField]
	private LocalizedText _main_tab_title;

	// Token: 0x04002A21 RID: 10785
	[SerializeField]
	private UnitTextManager _unit_text;

	// Token: 0x04002A22 RID: 10786
	private string _initial_name;
}
