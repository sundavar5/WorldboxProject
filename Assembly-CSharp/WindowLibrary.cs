using System;
using System.Collections.Generic;
using System.IO;

// Token: 0x02000806 RID: 2054
public class WindowLibrary : AssetLibrary<WindowAsset>
{
	// Token: 0x06004071 RID: 16497 RVA: 0x001B8330 File Offset: 0x001B6530
	public override void init()
	{
		base.init();
		this.add(new WindowAsset
		{
			id = "achievements",
			icon_path = "iconAchievements2",
			preload = true
		});
		this.add(new WindowAsset
		{
			id = "actor_asset",
			icon_path = "iconDebug",
			is_testable = false
		});
		this.add(new WindowAsset
		{
			id = "ad_loading_error",
			icon_path = "iconDeleteWorld"
		});
		this.add(new WindowAsset
		{
			id = "alliance",
			related_parent_window = "list_alliances",
			icon_path = "iconAlliance",
			preload = true
		});
		this.add(new WindowAsset
		{
			id = "alliance_customize",
			related_parent_window = "list_alliances",
			icon_path = "iconColorCustomization"
		});
		this.add(new WindowAsset
		{
			id = "auto_saves_browse",
			icon_path = "actor_traits/iconBlessing"
		});
		this.add(new WindowAsset
		{
			id = "brushes",
			icon_path = "iconColorCirlce2",
			preload = true,
			is_testable = false
		});
		this.t.get_hovering_icons = ((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			"brushes/brush_circ_1",
			"brushes/brush_circ_10",
			"brushes/brush_circ_15",
			"brushes/brush_circ_2",
			"brushes/brush_circ_5",
			"brushes/brush_sqr_1",
			"brushes/brush_sqr_10",
			"brushes/brush_sqr_15",
			"brushes/brush_sqr_2",
			"brushes/brush_sqr_5"
		}));
		this.add(new WindowAsset
		{
			id = "building_asset",
			icon_path = "iconBuildings",
			is_testable = false
		});
		this.add(new WindowAsset
		{
			id = "chart_comparer",
			icon_path = "iconCompareStatistics"
		});
		this.add(new WindowAsset
		{
			id = "city",
			related_parent_window = "list_cities",
			icon_path = "iconCity",
			preload = true
		});
		this.t.get_hovering_icons = ((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			"iconCityInspect",
			SelectedMetas.selected_city.getActorAsset().icon
		}));
		this.add(new WindowAsset
		{
			id = "army",
			related_parent_window = "list_armies",
			icon_path = "iconArmy",
			preload = true
		});
		WindowAsset t = this.t;
		t.get_hovering_icons = (HoveringBGIconsGetter)Delegate.Combine(t.get_hovering_icons, new HoveringBGIconsGetter((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			SelectedMetas.selected_army.getActorAsset().icon
		})));
		this.add(new WindowAsset
		{
			id = "clan",
			related_parent_window = "list_clans",
			icon_path = "iconClan",
			preload = true
		});
		WindowAsset t2 = this.t;
		t2.get_hovering_icons = (HoveringBGIconsGetter)Delegate.Combine(t2.get_hovering_icons, new HoveringBGIconsGetter((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			SelectedMetas.selected_clan.getActorAsset().icon
		})));
		this.add(new WindowAsset
		{
			id = "clan_customize",
			related_parent_window = "list_clans",
			icon_path = "iconColorCustomization"
		});
		this.add(new WindowAsset
		{
			id = "community_links",
			icon_path = "actor_traits/iconCommunity"
		});
		this.add(new WindowAsset
		{
			id = "credits",
			icon_path = "iconCoffee",
			window_toolbar_enabled = false
		});
		WindowAsset t3 = this.t;
		t3.get_hovering_icons = (HoveringBGIconsGetter)Delegate.Combine(t3.get_hovering_icons, new HoveringBGIconsGetter((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			"clan_traits/clan_trait_gods_chosen"
		})));
		this.add(new WindowAsset
		{
			id = "credits_community",
			icon_path = "actor_traits/iconStrong"
		});
		this.add(new WindowAsset
		{
			id = "culture",
			related_parent_window = "list_cultures",
			icon_path = "iconCulture",
			preload = true
		});
		WindowAsset t4 = this.t;
		t4.get_hovering_icons = (HoveringBGIconsGetter)Delegate.Combine(t4.get_hovering_icons, new HoveringBGIconsGetter((WindowAsset _) => WindowLibrary.getHoveringIconsUnits(SelectedMetas.selected_culture.units)));
		this.add(new WindowAsset
		{
			id = "culture_customize",
			related_parent_window = "list_cultures",
			icon_path = "iconColorCustomization"
		});
		this.add(new WindowAsset
		{
			id = "debug",
			icon_path = "iconDebug",
			is_testable = false
		});
		this.add(new WindowAsset
		{
			id = "debug_avatars",
			icon_path = "iconDebug",
			is_testable = false
		});
		this.add(new WindowAsset
		{
			id = "empty",
			icon_path = "iconEmptyLocus"
		});
		this.add(new WindowAsset
		{
			id = "equipment_rain_editor",
			icon_path = "iconCraftAdamantine",
			preload = true
		});
		this.add(new WindowAsset
		{
			id = "error_happened",
			icon_path = "iconDeleteWorld"
		});
		this.add(new WindowAsset
		{
			id = "error_with_reason",
			icon_path = "iconDeleteWorld"
		});
		this.add(new WindowAsset
		{
			id = "family",
			related_parent_window = "list_families",
			icon_path = "iconFamily",
			preload = true
		});
		this.add(new WindowAsset
		{
			id = "family_customize",
			related_parent_window = "list_families",
			icon_path = "iconColorCustomization"
		});
		this.add(new WindowAsset
		{
			id = "item",
			related_parent_window = "list_favorite_items",
			icon_path = "iconFavoriteWeapon",
			preload = true
		});
		this.add(new WindowAsset
		{
			id = "kingdom",
			related_parent_window = "list_kingdoms",
			icon_path = "iconCrown",
			preload = true
		});
		this.add(new WindowAsset
		{
			id = "kingdom_customize",
			related_parent_window = "list_kingdoms",
			icon_path = "iconColorCustomization"
		});
		this.add(new WindowAsset
		{
			id = "language",
			related_parent_window = "list_languages",
			icon_path = "iconLanguage",
			preload = true
		});
		WindowAsset t5 = this.t;
		t5.get_hovering_icons = (HoveringBGIconsGetter)Delegate.Combine(t5.get_hovering_icons, new HoveringBGIconsGetter((WindowAsset _) => WindowLibrary.getHoveringIconsUnits(SelectedMetas.selected_language.units)));
		this.add(new WindowAsset
		{
			id = "language_customize",
			related_parent_window = "list_languages",
			icon_path = "iconColorCustomization"
		});
		this.add(new WindowAsset
		{
			id = "list_alliances",
			icon_path = "iconAllianceList",
			preload = true
		});
		this.t.get_hovering_icons = ((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			"iconAlliance"
		}));
		this.add(new WindowAsset
		{
			id = "list_cities",
			icon_path = "iconCityList",
			preload = true
		});
		this.t.get_hovering_icons = ((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			"iconCityInspect"
		}));
		this.add(new WindowAsset
		{
			id = "list_clans",
			icon_path = "iconClanList",
			preload = true
		});
		this.t.get_hovering_icons = ((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			"iconClan"
		}));
		this.add(new WindowAsset
		{
			id = "list_armies",
			icon_path = "iconArmy",
			preload = true
		});
		this.t.get_hovering_icons = ((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			"iconArmy"
		}));
		this.add(new WindowAsset
		{
			id = "list_cultures",
			icon_path = "iconCultureList",
			preload = true
		});
		this.t.get_hovering_icons = ((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			"iconCulture"
		}));
		this.add(new WindowAsset
		{
			id = "list_families",
			icon_path = "iconFamilyList",
			preload = true
		});
		this.t.get_hovering_icons = ((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			"iconFamily"
		}));
		this.add(new WindowAsset
		{
			id = "list_favorite_items",
			icon_path = "iconFavoriteItemsList",
			preload = true
		});
		this.t.get_hovering_icons = ((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			"iconFavoriteStar"
		}));
		this.add(new WindowAsset
		{
			id = "list_favorite_units",
			icon_path = "iconFavoritesList",
			preload = true
		});
		this.t.get_hovering_icons = ((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			"iconFavoriteStar"
		}));
		this.add(new WindowAsset
		{
			id = "list_kingdoms",
			icon_path = "iconKingdomList",
			preload = true
		});
		this.t.get_hovering_icons = ((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			"iconCrown"
		}));
		this.add(new WindowAsset
		{
			id = "list_languages",
			icon_path = "iconLanguageList",
			preload = true
		});
		this.t.get_hovering_icons = ((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			"iconLanguage"
		}));
		this.add(new WindowAsset
		{
			id = "list_knowledge",
			icon_path = "iconKnowledge",
			preload = true
		});
		this.add(new WindowAsset
		{
			id = "list_plots",
			icon_path = "iconPlotList",
			preload = true
		});
		this.t.get_hovering_icons = ((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			"iconPlot"
		}));
		this.add(new WindowAsset
		{
			id = "list_religions",
			icon_path = "iconReligionList",
			preload = true
		});
		this.t.get_hovering_icons = ((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			"iconReligion"
		}));
		this.add(new WindowAsset
		{
			id = "list_subspecies",
			icon_path = "iconSubspeciesList",
			preload = true
		});
		this.t.get_hovering_icons = ((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			"iconSpecies"
		}));
		this.add(new WindowAsset
		{
			id = "list_wars",
			icon_path = "iconWarList",
			preload = true
		});
		this.t.get_hovering_icons = ((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			"iconWar"
		}));
		this.add(new WindowAsset
		{
			id = "load_world",
			icon_path = "iconSaveLocal",
			window_toolbar_enabled = false,
			preload = true
		});
		WindowAsset t6 = this.t;
		t6.get_hovering_icons = (HoveringBGIconsGetter)Delegate.Combine(t6.get_hovering_icons, new HoveringBGIconsGetter((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			"iconBox"
		})));
		this.add(new WindowAsset
		{
			id = "moonbox_promo",
			icon_path = "iconMoonBox",
			window_toolbar_enabled = false
		});
		this.add(new WindowAsset
		{
			id = "new_world_templates",
			icon_path = "iconBrowse",
			preload = true
		});
		this.add(new WindowAsset
		{
			id = "new_world_templates_2",
			related_parent_window = "new_world_templates",
			icon_path = "iconBrowse"
		});
		this.add(new WindowAsset
		{
			id = "news",
			icon_path = "iconDocument",
			window_toolbar_enabled = false
		});
		this.add(new WindowAsset
		{
			id = "not_found",
			icon_path = "iconDebug",
			window_toolbar_enabled = false
		});
		this.add(new WindowAsset
		{
			id = "other",
			icon_path = "iconOptions"
		});
		this.add(new WindowAsset
		{
			id = "patch_log",
			icon_path = "iconDocument",
			window_toolbar_enabled = false
		});
		this.add(new WindowAsset
		{
			id = "plot",
			related_parent_window = "list_plots",
			icon_path = "iconPlot"
		});
		this.add(new WindowAsset
		{
			id = "premium_menu",
			icon_path = "iconPremium",
			window_toolbar_enabled = false
		});
		this.add(new WindowAsset
		{
			id = "premium_help",
			icon_path = "iconPremium",
			window_toolbar_enabled = false
		});
		this.add(new WindowAsset
		{
			id = "premium_purchase_error",
			icon_path = "iconDeleteWorld",
			window_toolbar_enabled = false
		});
		this.add(new WindowAsset
		{
			id = "premium_unlocked",
			icon_path = "iconPremium",
			window_toolbar_enabled = false
		});
		this.add(new WindowAsset
		{
			id = "quit_game",
			icon_path = "iconClose",
			window_toolbar_enabled = false
		});
		this.add(new WindowAsset
		{
			id = "rate_us",
			icon_path = "iconHealth",
			window_toolbar_enabled = false
		});
		this.add(new WindowAsset
		{
			id = "rate_us_no",
			icon_path = "iconCloudRain",
			window_toolbar_enabled = false
		});
		this.add(new WindowAsset
		{
			id = "rate_us_yes",
			icon_path = "iconHealth",
			window_toolbar_enabled = false
		});
		this.add(new WindowAsset
		{
			id = "religion",
			related_parent_window = "list_religions",
			icon_path = "iconReligion",
			preload = true
		});
		this.add(new WindowAsset
		{
			id = "religion_customize",
			related_parent_window = "list_religions",
			icon_path = "iconColorCustomization"
		});
		this.add(new WindowAsset
		{
			id = "reward_ads",
			icon_path = "iconAdReward",
			window_toolbar_enabled = false
		});
		this.add(new WindowAsset
		{
			id = "reward_ads_power",
			icon_path = "iconAdReward",
			window_toolbar_enabled = false
		});
		this.add(new WindowAsset
		{
			id = "reward_ads_received",
			icon_path = "iconAdReward",
			window_toolbar_enabled = false
		});
		this.add(new WindowAsset
		{
			id = "reward_ads_saveslot",
			icon_path = "iconAdReward",
			window_toolbar_enabled = false
		});
		this.t.get_hovering_icons = ((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			"iconSaveLocal"
		}));
		this.add(new WindowAsset
		{
			id = "save_delete_confirm",
			related_parent_window = "saves_list",
			window_toolbar_enabled = false,
			icon_path = "iconDeleteWorld"
		});
		this.add(new WindowAsset
		{
			id = "save_load_confirm",
			related_parent_window = "saves_list",
			window_toolbar_enabled = false,
			icon_path = "iconBox"
		});
		this.add(new WindowAsset
		{
			id = "save_slot",
			related_parent_window = "saves_list",
			icon_path = "iconSaveLocal"
		});
		WindowAsset t7 = this.t;
		t7.get_hovering_icons = (HoveringBGIconsGetter)Delegate.Combine(t7.get_hovering_icons, new HoveringBGIconsGetter((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			"iconBox"
		})));
		this.add(new WindowAsset
		{
			id = "save_slot_new",
			related_parent_window = "saves_list",
			icon_path = "iconSaveLocal"
		});
		this.add(new WindowAsset
		{
			id = "save_world_confirm",
			related_parent_window = "saves_list",
			window_toolbar_enabled = false,
			icon_path = "iconSaveLocal"
		});
		this.add(new WindowAsset
		{
			id = "saves_list",
			icon_path = "iconBrowse",
			preload = true
		});
		this.add(new WindowAsset
		{
			id = "settings",
			related_parent_window = "other",
			icon_path = "iconOptions"
		});
		this.add(new WindowAsset
		{
			id = "settings_old",
			icon_path = "iconOptions",
			is_testable = false
		});
		this.add(new WindowAsset
		{
			id = "statistics",
			icon_path = "iconStatistics"
		});
		this.add(new WindowAsset
		{
			id = "steam",
			icon_path = "iconSteam",
			window_toolbar_enabled = false
		});
		this.add(new WindowAsset
		{
			id = "steam_workshop_browse",
			related_parent_window = "steam",
			icon_path = "iconSteam",
			window_toolbar_enabled = false
		});
		this.add(new WindowAsset
		{
			id = "steam_workshop_empty",
			related_parent_window = "steam",
			icon_path = "iconSteam",
			window_toolbar_enabled = false
		});
		this.add(new WindowAsset
		{
			id = "steam_workshop_main",
			related_parent_window = "steam",
			icon_path = "iconSteam",
			window_toolbar_enabled = false
		});
		this.add(new WindowAsset
		{
			id = "steam_workshop_play_world",
			related_parent_window = "steam",
			icon_path = "iconSteam",
			window_toolbar_enabled = false
		});
		this.add(new WindowAsset
		{
			id = "steam_workshop_upload_world",
			related_parent_window = "steam",
			icon_path = "iconSteam",
			window_toolbar_enabled = false
		});
		WindowAsset t8 = this.t;
		t8.get_hovering_icons = (HoveringBGIconsGetter)Delegate.Combine(t8.get_hovering_icons, new HoveringBGIconsGetter((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			"iconSaveCloud"
		})));
		this.add(new WindowAsset
		{
			id = "steam_workshop_uploading",
			related_parent_window = "steam",
			icon_path = "iconSteam",
			window_toolbar_enabled = false
		});
		this.add(new WindowAsset
		{
			id = "subspecies",
			related_parent_window = "list_subspecies",
			icon_path = "iconSpecies",
			preload = true
		});
		this.add(new WindowAsset
		{
			id = "subspecies_customize",
			related_parent_window = "list_subspecies",
			icon_path = "iconColorCustomization"
		});
		this.add(new WindowAsset
		{
			id = "thanks_for_testing",
			icon_path = "actor_traits/iconEyePatch",
			window_toolbar_enabled = false
		});
		this.add(new WindowAsset
		{
			id = "trait_rain_editor",
			icon_path = "actor_traits/iconDivineScar"
		});
		this.add(new WindowAsset
		{
			id = "under_development",
			icon_path = "iconDebug"
		});
		this.add(new WindowAsset
		{
			id = "unit",
			related_parent_window = "list_favorite_units",
			icon_path = "iconInspect",
			preload = true
		});
		WindowAsset t9 = this.t;
		t9.get_hovering_icons = (HoveringBGIconsGetter)Delegate.Combine(t9.get_hovering_icons, new HoveringBGIconsGetter((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			SelectedUnit.unit.asset.icon
		})));
		this.add(new WindowAsset
		{
			id = "update_available",
			icon_path = "iconCrit",
			window_toolbar_enabled = false
		});
		this.add(new WindowAsset
		{
			id = "war",
			related_parent_window = "list_wars",
			icon_path = "iconWar",
			preload = true
		});
		this.add(new WindowAsset
		{
			id = "welcome",
			window_toolbar_enabled = false,
			icon_path = "iconAye",
			preload = true
		});
		this.t.get_hovering_icons = ((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			"language_traits/",
			"culture_traits/",
			"clan_traits/",
			"subspecies_traits/",
			"religion_traits/",
			"kingdom_traits/"
		}));
		this.add(new WindowAsset
		{
			id = "world_ages",
			icon_path = "iconAges",
			preload = true
		});
		this.add(new WindowAsset
		{
			id = "world_history",
			icon_path = "iconWorldLog",
			preload = true
		});
		this.add(new WindowAsset
		{
			id = "world_info",
			icon_path = "iconWorldInfo",
			preload = true
		});
		this.add(new WindowAsset
		{
			id = "world_languages",
			related_parent_window = "other",
			icon_path = "iconLanguage"
		});
		this.t.get_hovering_icons = ((WindowAsset _) => WindowLibrary.getHoveringIcons(new string[]
		{
			"iconLanguage"
		}));
		this.add(new WindowAsset
		{
			id = "world_laws",
			icon_path = "iconWorldLaws",
			preload = true
		});
	}

	// Token: 0x06004072 RID: 16498 RVA: 0x001B9934 File Offset: 0x001B7B34
	public override void post_init()
	{
		base.post_init();
		ScrollWindow.addCallbackOpen(delegate(string _)
		{
			Config.debug_window_stats.opens = Config.debug_window_stats.opens + 1;
			HoveringBgIconManager.show();
		});
		ScrollWindow.addCallbackClose(delegate
		{
			Config.debug_window_stats.closes = Config.debug_window_stats.closes + 1;
			HoveringBgIconManager.hide();
		});
		ScrollWindow.addCallbackShowStarted(delegate(string pWindowId)
		{
			Config.debug_window_stats.shows = Config.debug_window_stats.shows + 1;
			Config.debug_window_stats.setCurrent(pWindowId);
		});
		ScrollWindow.addCallbackShow(delegate(string pWindowId)
		{
			HoveringBgIconManager.showWindow(this.get(pWindowId));
		});
		ScrollWindow.addCallbackShowFinished(delegate(string _)
		{
			ScrollWindow.checkElements();
		});
		ScrollWindow.addCallbackHide(delegate(string _)
		{
			Config.debug_window_stats.hides = Config.debug_window_stats.hides + 1;
		});
		foreach (WindowAsset tAsset in this.list)
		{
			if (tAsset.is_testable)
			{
				tAsset.is_testable = this.isTestable(tAsset);
			}
			if (tAsset.is_testable)
			{
				this._testable_windows.Add(tAsset);
			}
		}
	}

	// Token: 0x06004073 RID: 16499 RVA: 0x001B9A74 File Offset: 0x001B7C74
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (MetaTypeAsset tAsset in AssetManager.meta_type_library.list)
		{
			if (!string.IsNullOrEmpty(tAsset.window_name))
			{
				WindowAsset tWindowAsset = this.get(tAsset.window_name);
				if (tWindowAsset == null)
				{
					BaseAssetLibrary.logAssetError("WindowAsset not found for MetaTypeAsset ", tAsset.id);
				}
				else
				{
					tWindowAsset.meta_type_asset = tAsset;
					if (this.has(tAsset.window_name + "_customize"))
					{
						this.get(tAsset.window_name + "_customize").meta_type_asset = tAsset;
					}
					if (tWindowAsset.related_parent_window != null)
					{
						WindowAsset tParentWindow = this.get(tWindowAsset.related_parent_window);
						if (tParentWindow != null)
						{
							tParentWindow.meta_type_asset = tAsset;
						}
					}
				}
			}
		}
	}

	// Token: 0x06004074 RID: 16500 RVA: 0x001B9B5C File Offset: 0x001B7D5C
	internal List<WindowAsset> getTestableWindows()
	{
		return this._testable_windows;
	}

	// Token: 0x06004075 RID: 16501 RVA: 0x001B9B64 File Offset: 0x001B7D64
	private bool isTestable(WindowAsset pWindowAsset)
	{
		string tName = pWindowAsset.id;
		if (tName.Contains("upload"))
		{
			return false;
		}
		if (tName.Contains("_testing_"))
		{
			return false;
		}
		if (tName.StartsWith("worldnet"))
		{
			return false;
		}
		uint num = <PrivateImplementationDetails>.ComputeStringHash(tName);
		if (num <= 2447351236U)
		{
			if (num <= 761819584U)
			{
				if (num != 136160553U)
				{
					if (num != 413646574U)
					{
						if (num != 761819584U)
						{
							return true;
						}
						if (!(tName == "register"))
						{
							return true;
						}
					}
					else if (!(tName == "empty"))
					{
						return true;
					}
				}
				else if (!(tName == "moonbox_promo"))
				{
					return true;
				}
			}
			else if (num != 1282979455U)
			{
				if (num != 1483009432U)
				{
					if (num != 2447351236U)
					{
						return true;
					}
					if (!(tName == "more_games"))
					{
						return true;
					}
				}
				else if (!(tName == "debug"))
				{
					return true;
				}
			}
			else if (!(tName == "lsflw2_promo"))
			{
				return true;
			}
		}
		else if (num <= 3304503427U)
		{
			if (num != 2595186150U)
			{
				if (num != 2687396305U)
				{
					if (num != 3304503427U)
					{
						return true;
					}
					if (!(tName == "brushes"))
					{
						return true;
					}
				}
				else if (!(tName == "not_found"))
				{
					return true;
				}
			}
			else if (!(tName == "settings_old"))
			{
				return true;
			}
		}
		else if (num != 3611614819U)
		{
			if (num != 3718663845U)
			{
				if (num != 4109332074U)
				{
					return true;
				}
				if (!(tName == "create_custom_world"))
				{
					return true;
				}
			}
			else if (!(tName == "kingdom_technology"))
			{
				return true;
			}
		}
		else if (!(tName == "create_predefined_world"))
		{
			return true;
		}
		return false;
	}

	// Token: 0x06004076 RID: 16502 RVA: 0x001B9D14 File Offset: 0x001B7F14
	public override void editorDiagnostic()
	{
		base.editorDiagnostic();
		string[] tWindows = Directory.GetFiles("Assets/Resources/windows", "*.prefab", SearchOption.TopDirectoryOnly);
		using (ListPool<string> tAssetsFound = new ListPool<string>())
		{
			string[] array = tWindows;
			for (int i = 0; i < array.Length; i++)
			{
				string tWindowID = Path.GetFileNameWithoutExtension(array[i]);
				if (this.dict.ContainsKey(tWindowID))
				{
					tAssetsFound.Add(tWindowID);
				}
				else if (!(tWindowID == "list_window"))
				{
					BaseAssetLibrary.logAssetError("No associated WindowAsset found for window ", tWindowID);
				}
			}
			foreach (WindowAsset tAsset in this.list)
			{
				if (!tAssetsFound.Contains(tAsset.id))
				{
					BaseAssetLibrary.logAssetError("Window prefab not found for WindowAsset ", tAsset.id);
				}
			}
		}
	}

	// Token: 0x06004077 RID: 16503 RVA: 0x001B9E04 File Offset: 0x001B8004
	private static IEnumerable<string> getHoveringIconsUnits(List<Actor> pUnits)
	{
		HashSet<string> tIcons = new HashSet<string>();
		foreach (Actor actor in pUnits)
		{
			string tIcon = actor.asset.icon;
			if (tIcons.Add(tIcon))
			{
				yield return tIcon;
			}
		}
		List<Actor>.Enumerator enumerator = default(List<Actor>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x06004078 RID: 16504 RVA: 0x001B9E14 File Offset: 0x001B8014
	private static IEnumerable<string> getHoveringIcons(params string[] pPaths)
	{
		foreach (string tPath in pPaths)
		{
			yield return tPath;
		}
		string[] array = null;
		yield break;
	}

	// Token: 0x04002EAB RID: 11947
	[NonSerialized]
	private List<WindowAsset> _testable_windows = new List<WindowAsset>();
}
