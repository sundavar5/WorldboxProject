using System;
using SleekRender;
using UnityEngine;

// Token: 0x0200006B RID: 107
public class OptionsLibrary : AssetLibrary<OptionAsset>
{
	// Token: 0x060003A0 RID: 928 RVA: 0x00020B00 File Offset: 0x0001ED00
	public override void init()
	{
		base.init();
		this.initGameplayOptions();
		this.initLayerOptions();
		this.initAppOptions();
		this.initQualityOptions();
		this.initOtherOptions();
		this.initHotkeyOptions();
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x00020B2C File Offset: 0x0001ED2C
	private void initLayerOptions()
	{
		this.add(new OptionAsset
		{
			id = "map_layers",
			default_bool = true,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "map_species_families",
			default_bool = true,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "map_kingdom_layer",
			default_int = 0,
			max_value = 1,
			multi_toggle = true,
			type = OptionType.Bool,
			locale_options_ids = AssetLibrary<OptionAsset>.a<string>(new string[]
			{
				"ui_zone_mode_borders",
				"ui_zone_mode_units"
			})
		});
		this.add(new OptionAsset
		{
			id = "map_city_layer",
			default_int = 1,
			max_value = 1,
			multi_toggle = true,
			type = OptionType.Bool,
			locale_options_ids = AssetLibrary<OptionAsset>.a<string>(new string[]
			{
				"ui_zone_mode_borders",
				"ui_zone_mode_units"
			})
		});
		this.add(new OptionAsset
		{
			id = "map_clan_layer",
			default_int = 0,
			max_value = 2,
			multi_toggle = true,
			type = OptionType.Bool,
			locale_options_ids = AssetLibrary<OptionAsset>.a<string>(new string[]
			{
				"ui_zone_mode_kingdoms",
				"ui_zone_mode_cities",
				"ui_zone_mode_units"
			})
		});
		this.add(new OptionAsset
		{
			id = "map_religion_layer",
			default_int = 2,
			max_value = 2,
			multi_toggle = true,
			type = OptionType.Bool,
			locale_options_ids = AssetLibrary<OptionAsset>.a<string>(new string[]
			{
				"ui_zone_mode_kingdoms",
				"ui_zone_mode_cities",
				"ui_zone_mode_units"
			})
		});
		this.add(new OptionAsset
		{
			id = "map_culture_layer",
			default_int = 2,
			max_value = 2,
			multi_toggle = true,
			type = OptionType.Bool,
			locale_options_ids = AssetLibrary<OptionAsset>.a<string>(new string[]
			{
				"ui_zone_mode_kingdoms",
				"ui_zone_mode_cities",
				"ui_zone_mode_units"
			})
		});
		this.add(new OptionAsset
		{
			id = "map_subspecies_layer",
			default_int = 2,
			max_value = 2,
			multi_toggle = true,
			type = OptionType.Bool,
			locale_options_ids = AssetLibrary<OptionAsset>.a<string>(new string[]
			{
				"ui_zone_mode_kingdoms",
				"ui_zone_mode_cities",
				"ui_zone_mode_units"
			})
		});
		this.add(new OptionAsset
		{
			id = "map_family_layer",
			default_int = 2,
			max_value = 2,
			multi_toggle = true,
			type = OptionType.Bool,
			locale_options_ids = AssetLibrary<OptionAsset>.a<string>(new string[]
			{
				"ui_zone_mode_kingdoms",
				"ui_zone_mode_cities",
				"ui_zone_mode_units"
			})
		});
		this.add(new OptionAsset
		{
			id = "map_language_layer",
			default_int = 2,
			max_value = 2,
			multi_toggle = true,
			type = OptionType.Bool,
			locale_options_ids = AssetLibrary<OptionAsset>.a<string>(new string[]
			{
				"ui_zone_mode_kingdoms",
				"ui_zone_mode_cities",
				"ui_zone_mode_units"
			})
		});
		this.add(new OptionAsset
		{
			id = "map_alliance_layer",
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "map_army_layer",
			default_bool = false,
			type = OptionType.Bool
		});
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x00020EA8 File Offset: 0x0001F0A8
	private void initGameplayOptions()
	{
		this.add(new OptionAsset
		{
			id = "map_names",
			default_bool = true,
			default_int = 0,
			max_value = 1,
			multi_toggle = true,
			type = OptionType.Bool,
			locale_options_ids = AssetLibrary<OptionAsset>.a<string>(new string[]
			{
				"ui_map_names_normal",
				"ui_map_names_banners_only"
			})
		});
		this.add(new OptionAsset
		{
			id = "map_kings_leaders",
			default_bool = true,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "marks_favorites",
			default_bool = true,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "marks_favorite_items",
			default_bool = true,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "marks_plots",
			default_bool = true,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "marks_wars",
			default_bool = false,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "marks_armies",
			default_bool = true,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "only_favorited_meta",
			default_bool = false,
			reset_to_default_on_launch = true,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "money_flow",
			default_bool = false,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "icons_happiness",
			default_bool = false,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "icons_tasks",
			default_bool = false,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "talk_bubbles",
			default_bool = true,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "meta_conversions",
			default_bool = true,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "unit_metas",
			default_bool = false,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "marks_battles",
			default_bool = true,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "history_log",
			default_bool = true,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "marks_boats",
			default_bool = true,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "army_targets",
			default_bool = true,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "tooltip_zones",
			default_bool = true,
			default_bool_mobile = false,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "tooltip_units",
			default_bool = true,
			default_bool_mobile = false,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "cursor_arrow_destination",
			default_bool = false,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "cursor_arrow_house",
			default_bool = false,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "cursor_arrow_lover",
			default_bool = false,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "cursor_arrow_family",
			default_bool = false,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "cursor_arrow_parents",
			default_bool = false,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "cursor_arrow_kids",
			default_bool = false,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "cursor_arrow_attack_target",
			default_bool = false,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "highlight_kingdom_enemies",
			default_bool = true,
			type = OptionType.Bool,
			override_bool_mobile = true,
			default_bool_mobile = false
		});
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x0002131C File Offset: 0x0001F51C
	private void initAppOptions()
	{
		this.add(new OptionAsset
		{
			id = "autorotation",
			translation_key = "autorotation",
			translation_key_description = "option_description_autorotation",
			update_all_elements_after_click = true,
			default_bool = false,
			type = OptionType.Bool,
			action = delegate(OptionAsset pAsset)
			{
				Config.setAutorotation(this.getSavedBool(pAsset.id));
			}
		});
		this.add(new OptionAsset
		{
			id = "portrait",
			translation_key = "portrait_mode",
			translation_key_description = "option_description_portrait",
			default_bool = true,
			type = OptionType.Bool,
			action = delegate(OptionAsset pAsset)
			{
				Config.setPortrait(this.getSavedBool(pAsset.id));
			}
		});
		this.add(new OptionAsset
		{
			id = "fps_lock_30",
			translation_key_description = "option_description_fps_lock_30",
			default_bool = false,
			type = OptionType.Bool,
			update_all_elements_after_click = true,
			action = delegate(OptionAsset pAsset)
			{
				bool flag = Config.fps_lock_30 = this.getSavedBool(pAsset.id);
				if (Config.fps_lock_30)
				{
					Application.targetFrameRate = 30;
				}
				else
				{
					Application.targetFrameRate = 60;
				}
				if (flag)
				{
					this.forceDisableOption("vsync");
				}
			}
		});
		this.add(new OptionAsset
		{
			id = "sound",
			translation_key = "sound_effects",
			translation_key_description = "option_description_sound_effects",
			default_bool = true,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "volume_master_sound",
			translation_key_description = "option_description_volume_master_sound",
			default_int = 100,
			type = OptionType.Int,
			max_value = 100,
			min_value = 0,
			counter_percent = true
		});
		this.add(new OptionAsset
		{
			id = "volume_music",
			translation_key_description = "option_description_volume_music",
			default_int = 100,
			type = OptionType.Int,
			max_value = 100,
			min_value = 0,
			counter_percent = true
		});
		this.add(new OptionAsset
		{
			id = "volume_sound_effects",
			translation_key_description = "option_description_volume_sound_effects",
			default_int = 100,
			type = OptionType.Int,
			max_value = 100,
			min_value = 0,
			counter_percent = true
		});
		this.add(new OptionAsset
		{
			id = "volume_ui",
			translation_key_description = "option_description_volume_ui",
			default_int = 100,
			type = OptionType.Int,
			max_value = 100,
			min_value = 0,
			counter_percent = true
		});
		this.add(new OptionAsset
		{
			id = "music",
			translation_key_description = "option_description_music",
			translation_key = "music",
			default_bool = true,
			type = OptionType.Bool
		});
		this.add(new OptionAsset
		{
			id = "vsync",
			translation_key = "vsync",
			translation_key_description = "option_description_vsync",
			default_bool = false,
			type = OptionType.Bool,
			update_all_elements_after_click = true,
			computer_only = true,
			action = delegate(OptionAsset pAsset)
			{
				bool savedBool = this.getSavedBool(pAsset.id);
				PlayerConfig.setVsync(savedBool);
				if (savedBool)
				{
					this.forceDisableOption("fps_lock_30");
				}
			}
		});
		this.add(new OptionAsset
		{
			id = "fullscreen",
			default_bool = true,
			translation_key_description = "option_description_fullscreen",
			type = OptionType.Bool,
			computer_only = true,
			action = delegate(OptionAsset pAsset)
			{
				Config.full_screen = this.getSavedBool(pAsset.id);
				if (Config.full_screen)
				{
					int width = Screen.currentResolution.width;
					int tHeight = Screen.currentResolution.height;
					RefreshRate tRefreshRate = Screen.currentResolution.refreshRateRatio;
					Screen.SetResolution(width, tHeight, FullScreenMode.FullScreenWindow, tRefreshRate);
					return;
				}
				Screen.fullScreen = false;
			}
		});
		this.add(new OptionAsset
		{
			id = "language",
			type = OptionType.String,
			default_string = "en"
		});
		this.add(new OptionAsset
		{
			id = "username",
			type = OptionType.String,
			default_string = "",
			has_locales = false
		});
		this.add(new OptionAsset
		{
			id = "wbb_confirmed",
			type = OptionType.Bool,
			default_bool = false,
			has_locales = false,
			action = delegate(OptionAsset pAsset)
			{
				Config.wbb_confirmed = this.getSavedBool(pAsset.id);
			}
		});
		OptionAsset optionAsset = new OptionAsset();
		optionAsset.id = "ui_size_main";
		optionAsset.translation_key = "power_bar_size";
		optionAsset.translation_key_description = "option_description_power_bar_size";
		optionAsset.type = OptionType.Int;
		optionAsset.default_int = 100;
		optionAsset.max_value = 150;
		optionAsset.min_value = 50;
		optionAsset.action = delegate(OptionAsset _)
		{
			CanvasMain.instance.resizeMainUI();
			PowerButtonSelector.instance.showBarTemporary();
		};
		optionAsset.counter_format = new ActionFormatCounterOptionAsset(this.getCounterFormat100Percent);
		this.add(optionAsset);
		OptionAsset optionAsset2 = new OptionAsset();
		optionAsset2.id = "ui_size_windows";
		optionAsset2.translation_key_description = "option_description_ui_windows_size";
		optionAsset2.type = OptionType.Int;
		optionAsset2.default_int = 100;
		optionAsset2.max_value = 115;
		optionAsset2.min_value = 30;
		optionAsset2.action = delegate(OptionAsset _)
		{
			CanvasMain.instance.resizeWindowsUI();
			MetaSwitchManager.checkAndRefresh();
			HoveringBgIconManager.dropAll();
		};
		optionAsset2.counter_format = new ActionFormatCounterOptionAsset(this.getCounterFormat100Percent);
		this.add(optionAsset2);
		OptionAsset optionAsset3 = new OptionAsset();
		optionAsset3.id = "ui_size_tooltips";
		optionAsset3.translation_key_description = "option_description_ui_tooltips_size";
		optionAsset3.type = OptionType.Int;
		optionAsset3.default_int = 100;
		optionAsset3.max_value = 150;
		optionAsset3.min_value = 30;
		optionAsset3.action = delegate(OptionAsset _)
		{
			CanvasMain.instance.resizeTooltipUI();
		};
		optionAsset3.counter_format = new ActionFormatCounterOptionAsset(this.getCounterFormat100Percent);
		this.add(optionAsset3);
		OptionAsset optionAsset4 = new OptionAsset();
		optionAsset4.id = "ui_size_map_names";
		optionAsset4.translation_key_description = "option_description_ui_map_names_size";
		optionAsset4.type = OptionType.Int;
		optionAsset4.default_int = 70;
		optionAsset4.max_value = 160;
		optionAsset4.min_value = 30;
		optionAsset4.action = delegate(OptionAsset _)
		{
			CanvasMain.instance.resizeMapNames();
			World.world.nameplate_manager.clearCaches();
		};
		optionAsset4.counter_format = new ActionFormatCounterOptionAsset(this.getCounterFormat100Percent);
		this.add(optionAsset4);
		this.add(new OptionAsset
		{
			id = "ui_map_border_opacity",
			translation_key_description = "option_description_map_border_opacity",
			type = OptionType.Int,
			default_int = 88,
			max_value = 100,
			min_value = 30,
			action = delegate(OptionAsset pAsset)
			{
				float tVal = (float)this.getSavedInt(pAsset.id) / 100f;
				World.world.zone_calculator.minimap_opacity = tVal;
			},
			counter_format = new ActionFormatCounterOptionAsset(this.getCounterFormat100Percent)
		});
		this.add(new OptionAsset
		{
			id = "ui_map_border_brightness",
			translation_key_description = "option_description_map_border_brightness",
			type = OptionType.Int,
			default_int = 100,
			max_value = 100,
			min_value = 60,
			action = delegate(OptionAsset pAsset)
			{
				float tVal = (float)this.getSavedInt(pAsset.id) / 100f;
				World.world.zone_calculator.border_brightness = tVal;
			},
			counter_format = new ActionFormatCounterOptionAsset(this.getCounterFormat100Percent)
		});
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x000219A0 File Offset: 0x0001FBA0
	private void initQualityOptions()
	{
		this.add(new OptionAsset
		{
			id = "vignette",
			translation_key_description = "option_description_vignette",
			default_bool = true,
			type = OptionType.Bool,
			action = delegate(OptionAsset pAsset)
			{
				Camera.main.GetComponent<SleekRenderPostProcess>().settings.vignetteEnabled = this.getSavedBool(pAsset.id);
			}
		});
		this.add(new OptionAsset
		{
			id = "bloom",
			translation_key_description = "option_description_bloom",
			default_bool = true,
			type = OptionType.Bool,
			action = delegate(OptionAsset pAsset)
			{
				Camera.main.GetComponent<SleekRenderPostProcess>().settings.bloomEnabled = this.getSavedBool(pAsset.id);
			}
		});
		this.add(new OptionAsset
		{
			id = "fire",
			translation_key_description = "option_description_fire",
			default_bool = true,
			type = OptionType.Bool,
			action = delegate(OptionAsset pAsset)
			{
				World.world.particles_fire.enabled = this.getSavedBool(pAsset.id);
			}
		});
		this.add(new OptionAsset
		{
			id = "smoke",
			translation_key_description = "option_description_smoke",
			default_bool = true,
			type = OptionType.Bool,
			action = delegate(OptionAsset pAsset)
			{
				World.world.particles_smoke.enabled = this.getSavedBool(pAsset.id);
			}
		});
		this.add(new OptionAsset
		{
			id = "shadows",
			translation_key_description = "option_description_shadows",
			type = OptionType.Bool,
			default_bool = true,
			action = delegate(OptionAsset pAsset)
			{
				Config.shadows_active = this.getSavedBool(pAsset.id);
			}
		});
		this.add(new OptionAsset
		{
			id = "tree_wind",
			translation_key_description = "option_description_tree_wind",
			type = OptionType.Bool,
			default_bool = true,
			action = delegate(OptionAsset pAsset)
			{
				BuildingRendererSettings.wobbly_material_enabled = this.getSavedBool(pAsset.id);
				World.world.buildings.checkWobblySetting();
			}
		});
		this.add(new OptionAsset
		{
			id = "minimap_transition_animation",
			translation_key_description = "option_description_minimap_transition_animation",
			type = OptionType.Bool,
			default_bool = true
		});
		this.add(new OptionAsset
		{
			id = "night_lights",
			translation_key_description = "option_description_night_lights",
			type = OptionType.Bool,
			default_bool = true
		});
		this.add(new OptionAsset
		{
			id = "cursor_lights",
			translation_key_description = "option_description_cursor_lights",
			type = OptionType.Bool,
			default_bool = true,
			default_bool_mobile = false
		});
		this.add(new OptionAsset
		{
			id = "age_particles",
			translation_key_description = "option_description_age_particles",
			type = OptionType.Bool,
			default_bool = true,
			action = delegate(OptionAsset pAsset)
			{
				WorldAgesParticles.effects_enabled = this.getSavedBool(pAsset.id);
			}
		});
		this.add(new OptionAsset
		{
			id = "age_overlay_effect",
			translation_key_description = "option_description_age_overlay_effect",
			type = OptionType.Int,
			default_int = 100,
			max_value = 100,
			min_value = 0,
			counter_format = new ActionFormatCounterOptionAsset(this.getCounterFormat100Percent)
		});
		this.add(new OptionAsset
		{
			id = "age_night_effect",
			translation_key_description = "option_description_age_night_effect",
			type = OptionType.Int,
			default_int = 100,
			max_value = 100,
			min_value = 50,
			counter_format = new ActionFormatCounterOptionAsset(this.getCounterFormat100Percent)
		});
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x00021CB8 File Offset: 0x0001FEB8
	private string getCounterFormat100Percent(OptionAsset pAsset)
	{
		return PlayerConfig.getIntValue(pAsset.id).ToString() + "%";
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x00021CE4 File Offset: 0x0001FEE4
	private void initOtherOptions()
	{
		this.add(new OptionAsset
		{
			id = "preload_windows",
			translation_key_description = "option_description_preload_windows",
			default_bool = true,
			type = OptionType.Bool,
			override_bool_mobile = true,
			default_bool_mobile = false,
			action = delegate(OptionAsset pAsset)
			{
				Config.preload_windows = this.getSavedBool(pAsset.id);
			}
		});
		this.add(new OptionAsset
		{
			id = "preload_quantum_sprites",
			translation_key_description = "option_description_preload_quantum_sprites",
			default_bool = true,
			type = OptionType.Bool,
			override_bool_mobile = true,
			default_bool_mobile = false,
			action = delegate(OptionAsset pAsset)
			{
				Config.preload_quantum_sprites = this.getSavedBool(pAsset.id);
			}
		});
		this.add(new OptionAsset
		{
			id = "preload_buildings",
			translation_key_description = "option_description_preload_buildings",
			default_bool = true,
			type = OptionType.Bool,
			override_bool_mobile = true,
			default_bool_mobile = false,
			action = delegate(OptionAsset pAsset)
			{
				Config.preload_buildings = this.getSavedBool(pAsset.id);
			}
		});
		this.add(new OptionAsset
		{
			id = "preload_units",
			translation_key_description = "option_description_preload_units",
			default_bool = true,
			type = OptionType.Bool,
			override_bool_mobile = true,
			default_bool_mobile = false,
			action = delegate(OptionAsset pAsset)
			{
				Config.preload_units = this.getSavedBool(pAsset.id);
			}
		});
		this.add(new OptionAsset
		{
			id = "autosaves",
			translation_key_description = "option_description_autosaves",
			default_bool = true,
			type = OptionType.Bool,
			action = delegate(OptionAsset pAsset)
			{
				Config.autosaves = this.getSavedBool(pAsset.id);
			}
		});
		this.add(new OptionAsset
		{
			id = "graphs",
			translation_key_description = "option_description_graphs",
			default_bool = true,
			type = OptionType.Bool,
			action = delegate(OptionAsset pAsset)
			{
				Config.graphs = this.getSavedBool(pAsset.id);
			}
		});
		this.add(new OptionAsset
		{
			id = "tooltips",
			translation_key_description = "option_description_tooltips",
			default_bool = true,
			type = OptionType.Bool,
			override_bool_mobile = true,
			default_bool_mobile = false,
			action = delegate(OptionAsset pAsset)
			{
				Config.tooltips_active = this.getSavedBool(pAsset.id);
			}
		});
		this.add(new OptionAsset
		{
			id = "experimental",
			translation_key_description = "option_description_experimental",
			translation_key_description_2 = "option_description_experimental_warning",
			default_bool = false,
			type = OptionType.Bool,
			action = delegate(OptionAsset pAsset)
			{
				bool tResult = this.getSavedBool(pAsset.id);
				if (tResult)
				{
					string tLastUsedVersion = this.getSavedString("last_used_version");
					if (tLastUsedVersion != Application.version)
					{
						tResult = false;
						Debug.LogWarning("Last version and current version differ, disabling experimental mode " + tLastUsedVersion + " " + Application.version);
						this.setSavedBool(pAsset.id, tResult);
					}
					else
					{
						Debug.Log("Experimental mode is enabled");
						WorldTip.instance.showToolbarText("Experimental mode is enabled");
					}
				}
				Config.experimental_mode = tResult;
			}
		});
		OptionAsset optionAsset = new OptionAsset();
		optionAsset.id = "last_used_version";
		optionAsset.default_string = "";
		optionAsset.type = OptionType.String;
		optionAsset.has_locales = false;
		optionAsset.action = delegate(OptionAsset pAsset)
		{
			PlayerConfig.setOptionString(pAsset.id, Application.version);
		};
		this.add(optionAsset);
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x00021FA8 File Offset: 0x000201A8
	private void initHotkeyOptions()
	{
		this.add(new OptionAsset
		{
			id = "hotkey_1",
			default_string = string.Empty,
			type = OptionType.String,
			has_locales = false
		});
		this.clone("hotkey_2", "hotkey_1");
		this.clone("hotkey_3", "hotkey_1");
		this.clone("hotkey_4", "hotkey_1");
		this.clone("hotkey_5", "hotkey_1");
		this.clone("hotkey_6", "hotkey_1");
		this.clone("hotkey_7", "hotkey_1");
		this.clone("hotkey_8", "hotkey_1");
		this.clone("hotkey_9", "hotkey_1");
		this.clone("hotkey_0", "hotkey_1");
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x00022080 File Offset: 0x00020280
	public void forceDisableOption(string pID)
	{
		OptionAsset tAsset = this.get(pID);
		if (tAsset.computer_only && Config.isMobile)
		{
			return;
		}
		PlayerConfig.setOptionBool(tAsset.id, false);
		ActionOptionAsset action = tAsset.action;
		if (action == null)
		{
			return;
		}
		action(tAsset);
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x000220C2 File Offset: 0x000202C2
	private bool getSavedBool(string pAssetID)
	{
		return PlayerConfig.optionBoolEnabled(pAssetID);
	}

	// Token: 0x060003AA RID: 938 RVA: 0x000220CA File Offset: 0x000202CA
	private void setSavedBool(string pAssetID, bool pValue)
	{
		PlayerConfig.setOptionBool(pAssetID, pValue);
	}

	// Token: 0x060003AB RID: 939 RVA: 0x000220D3 File Offset: 0x000202D3
	private int getSavedInt(string pAssetID)
	{
		return PlayerConfig.getOptionInt(pAssetID);
	}

	// Token: 0x060003AC RID: 940 RVA: 0x000220DB File Offset: 0x000202DB
	private string getSavedString(string pAssetID)
	{
		return PlayerConfig.getOptionString(pAssetID);
	}

	// Token: 0x060003AD RID: 941 RVA: 0x000220E4 File Offset: 0x000202E4
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (OptionAsset tAsset in this.list)
		{
			if (tAsset.has_locales && tAsset.type == OptionType.Bool && string.IsNullOrEmpty(tAsset.translation_key))
			{
				foreach (GodPower tPower in AssetManager.powers.list)
				{
					if (!(tPower.toggle_name != tAsset.id))
					{
						tAsset.translation_key = tPower.getLocaleID();
						if (string.IsNullOrEmpty(tAsset.translation_key_description))
						{
							tAsset.translation_key_description = tPower.getDescriptionID();
						}
					}
				}
			}
		}
	}

	// Token: 0x060003AE RID: 942 RVA: 0x000221D4 File Offset: 0x000203D4
	public override void editorDiagnosticLocales()
	{
		foreach (OptionAsset tAsset in this.list)
		{
			if (tAsset.has_locales)
			{
				this.checkLocale(tAsset, tAsset.getLocaleID());
				this.checkLocale(tAsset, tAsset.getDescriptionID());
				this.checkLocale(tAsset, tAsset.getDescriptionID2());
			}
		}
		base.editorDiagnosticLocales();
	}
}
