using System;
using System.Collections.Generic;
using strings;

// Token: 0x02000134 RID: 308
public class LocaleGroupLibrary : AssetLibrary<LocaleGroupAsset>
{
	// Token: 0x06000936 RID: 2358 RVA: 0x00082BCC File Offset: 0x00080DCC
	public override void init()
	{
		base.init();
		this.add(new LocaleGroupAsset
		{
			id = "achievements"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"achievement_groups",
			"achievements"
		});
		this.t.starts_with.Add("achievement");
		this.add(new LocaleGroupAsset
		{
			id = "api_discord"
		});
		this.t.starts_with.Add("discordsocial");
		this.t.starts_with.Add("discord_");
		this.add(new LocaleGroupAsset
		{
			id = "api_steam"
		});
		this.t.starts_with_priority.Add("steam");
		this.t.starts_with.Add("promo_steam");
		this.add(new LocaleGroupAsset
		{
			id = "biomes"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"biome_library"
		});
		this.add(new LocaleGroupAsset
		{
			id = "books"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"book_types_library"
		});
		this.t.starts_with.Add("book");
		this.add(new LocaleGroupAsset
		{
			id = "debug"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"debug_tool_library",
			"tester_jobs",
			"tester_tasks"
		});
		this.t.contains.Add("debug");
		this.t.starts_with.Add("search_by_");
		this.t.starts_with.Add("dt_");
		this.t.starts_with_priority.Add("tab_debug");
		this.add(new LocaleGroupAsset
		{
			id = "genes"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"chromosome_type_library",
			"gene_library"
		});
		this.t.starts_with.Add("neuro");
		this.t.starts_with.Add("dna");
		this.t.starts_with.Add("gene");
		this.t.starts_with.Add("nucleo_");
		this.t.contains.Add("amplif");
		this.t.starts_with.Add("locus");
		this.t.starts_with.Add("chromosomes");
		this.t.starts_with.Add("sequence_synergy");
		this.add(new LocaleGroupAsset
		{
			id = "history"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"world_log_library",
			"history_groups"
		});
		this.t.matches.Add("diplomacy_peace");
		this.t.starts_with.Add("race_dead_");
		this.add(new LocaleGroupAsset
		{
			id = "hotkeys"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"hotkey_library"
		});
		this.t.starts_with.Add("hotkey_");
		this.add(new LocaleGroupAsset
		{
			id = "creatures"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"units"
		});
		this.t.starts_with.Add("creature_statistics");
		this.add(new LocaleGroupAsset
		{
			id = "traits_units"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"traits",
			"trait_groups"
		});
		this.add(new LocaleGroupAsset
		{
			id = "traits_cultures"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"culture_traits",
			"culture_trait_groups"
		});
		this.add(new LocaleGroupAsset
		{
			id = "traits_languages"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"language_traits",
			"language_trait_groups"
		});
		this.add(new LocaleGroupAsset
		{
			id = "traits_clans"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"clan_traits",
			"clan_trait_groups"
		});
		this.add(new LocaleGroupAsset
		{
			id = "traits_subspecies"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"subspecies_traits",
			"subspecies_trait_groups"
		});
		this.add(new LocaleGroupAsset
		{
			id = "traits_religions"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"religion_traits",
			"religion_trait_groups"
		});
		this.add(new LocaleGroupAsset
		{
			id = "traits_kingdoms"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"kingdom_traits",
			"kingdom_trait_group"
		});
		this.add(new LocaleGroupAsset
		{
			id = "meta_traits"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"rarity_library"
		});
		this.t.contains.Add("trait");
		this.add(new LocaleGroupAsset
		{
			id = "meta_alliances"
		});
		this.t.starts_with.Add("alliance");
		this.t.starts_with.Add("unity_");
		this.t.starts_with.Add("whisper_");
		this.add(new LocaleGroupAsset
		{
			id = "meta_clans"
		});
		this.t.starts_with.Add("clan");
		this.add(new LocaleGroupAsset
		{
			id = "meta_religions"
		});
		this.t.starts_with.Add("religion");
		this.add(new LocaleGroupAsset
		{
			id = "meta_families"
		});
		this.t.starts_with.Add("families");
		this.add(new LocaleGroupAsset
		{
			id = "meta_cultures"
		});
		this.t.starts_with.Add("culture");
		this.add(new LocaleGroupAsset
		{
			id = "meta_languages"
		});
		this.t.starts_with_priority.Add("language");
		this.add(new LocaleGroupAsset
		{
			id = "meta_plots"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"plots",
			"plot_group"
		});
		this.t.starts_with.Add("plot");
		this.t.starts_with.Add("can_be_done");
		this.add(new LocaleGroupAsset
		{
			id = "meta_subspecies"
		});
		this.t.starts_with.Add("subspecies");
		this.t.starts_with.Add("race");
		this.t.contains.Add("species");
		this.t.checker = ((string pKey) => typeof(S_SocialStructure).hasField(pKey) || typeof(S_TaxonomyRank).hasField(pKey));
		this.add(new LocaleGroupAsset
		{
			id = "meta_wars"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"war_types_library"
		});
		this.t.matches.Add("war");
		this.t.starts_with.Add("war_");
		this.t.starts_with.Add("wars");
		this.t.starts_with.Add("attacke");
		this.t.starts_with.Add("defende");
		this.add(new LocaleGroupAsset
		{
			id = "metas"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"meta_customization_library",
			"knowledge_library"
		});
		this.add(new LocaleGroupAsset
		{
			id = "happiness"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"happiness_library"
		});
		this.add(new LocaleGroupAsset
		{
			id = "loyalty"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"loyalty_library"
		});
		this.add(new LocaleGroupAsset
		{
			id = "opinion"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"opinion_library"
		});
		this.add(new LocaleGroupAsset
		{
			id = "meta_reports"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"meta_text_report_library"
		});
		this.add(new LocaleGroupAsset
		{
			id = "moods"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"architect_mood_library"
		});
		this.t.starts_with.Add("mood_");
		this.t.starts_with.Add("architect_mood_");
		this.add(new LocaleGroupAsset
		{
			id = "onomastics"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"onomastics_library",
			"onomastics_evolution_library"
		});
		this.t.matches.Add("naming_parts");
		this.t.matches.Add("naming_examples");
		this.t.matches.Add("group_editor");
		this.t.starts_with.Add("onomastic");
		this.add(new LocaleGroupAsset
		{
			id = "possession"
		});
		this.t.starts_with.Add("possession");
		this.t.starts_with.Add("crabzilla");
		this.add(new LocaleGroupAsset
		{
			id = "resources"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"resources"
		});
		this.add(new LocaleGroupAsset
		{
			id = "powers"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"powers"
		});
		this.t.starts_with.Add("Infinity Coin");
		this.add(new LocaleGroupAsset
		{
			id = "ui_options"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"options_library"
		});
		this.t.matches.Add("resolution");
		this.t.matches.Add("additional_sounds");
		this.t.matches.Add("windowed_mode");
		this.t.starts_with.Add("setting");
		this.t.starts_with.Add("option_");
		this.t.starts_with_priority.Add("button_option");
		this.t.starts_with_priority.Add("graphics");
		this.t.starts_with.Add("username");
		this.add(new LocaleGroupAsset
		{
			id = "ui_tabs"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"power_tab_library"
		});
		this.t.starts_with.Add("tab_");
		this.add(new LocaleGroupAsset
		{
			id = "base_stats"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"base_stats_library"
		});
		this.add(new LocaleGroupAsset
		{
			id = "personalities"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"personalities"
		});
		this.add(new LocaleGroupAsset
		{
			id = "statistics"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"graph_time_library",
			"history_data_library",
			"history_meta_data_library",
			"statistics_library"
		});
		this.t.starts_with_priority.Add("chart_");
		this.t.starts_with_priority.Add("graph");
		this.t.starts_with_priority.Add("world_statistics_");
		this.t.starts_with_priority.Add("statistics_");
		this.t.contains.Add("_stat");
		this.add(new LocaleGroupAsset
		{
			id = "information"
		});
		this.t.matches.Add("loyalty_world_law");
		this.t.matches.Add("males_single");
		this.t.matches.Add("population_pyramid");
		this.t.matches.Add("status_waiting_for_passengers");
		this.t.matches.Add("task");
		this.t.matches.Add("total_deaths");
		this.t.matches.Add("total_knowledge");
		this.t.matches.Add("total_knowledge_info");
		this.t.matches.Add("total_population_money");
		this.t.matches.Add("upkeep_buildings");
		this.t.matches.Add("upkeep_homeless");
		this.t.matches.Add("yearly_gain");
		this.t.matches.Add("taxonomy_description_tooltip");
		this.t.matches.Add("vegetation");
		this.t.matches.Add("ownerless_item_info");
		this.t.matches.Add("ownerless_item_tip");
		this.t.matches.Add("capital");
		this.t.matches.Add("alpha");
		this.t.matches.Add("age");
		this.t.matches.Add("ages");
		this.t.matches.Add("amount");
		this.t.matches.Add("amount");
		this.t.matches.Add("ancestor_families");
		this.t.matches.Add("ancestor_family");
		this.t.matches.Add("area");
		this.t.matches.Add("best_friend");
		this.t.matches.Add("biggest_level");
		this.t.matches.Add("birthday");
		this.t.matches.Add("birthplace");
		this.t.matches.Add("carrying");
		this.t.matches.Add("couples");
		this.t.matches.Add("created");
		this.t.matches.Add("creator");
		this.t.matches.Add("creators_clan");
		this.t.matches.Add("creature");
		this.t.matches.Add("deity");
		this.t.matches.Add("deity");
		this.t.matches.Add("durability");
		this.t.matches.Add("fastest");
		this.t.matches.Add("females_single");
		this.t.matches.Add("fertility");
		this.t.matches.Add("food_consumed");
		this.t.matches.Add("founded");
		this.t.matches.Add("founded_in");
		this.t.matches.Add("founder");
		this.t.matches.Add("founder_clan");
		this.t.matches.Add("fullest");
		this.t.matches.Add("grandparents");
		this.t.matches.Add("great_clan_of");
		this.t.matches.Add("happiest");
		this.t.matches.Add("happy_units");
		this.t.matches.Add("happy_units_description");
		this.t.matches.Add("heir");
		this.t.matches.Add("home_village");
		this.t.matches.Add("house");
		this.t.matches.Add("hunger");
		this.t.matches.Add("hungriest");
		this.t.matches.Add("influence");
		this.t.matches.Add("instigator");
		this.t.matches.Add("instigator_from");
		this.t.matches.Add("inventory");
		this.t.matches.Add("level");
		this.t.matches.Add("lifespan_female");
		this.t.matches.Add("lifespan_male");
		this.t.matches.Add("locate");
		this.t.matches.Add("lover");
		this.t.matches.Add("max_age");
		this.t.matches.Add("max_children");
		this.t.matches.Add("members");
		this.t.matches.Add("mobs");
		this.t.matches.Add("nutrition");
		this.t.matches.Add("oldest");
		this.t.matches.Add("origin");
		this.t.matches.Add("origin_families");
		this.t.matches.Add("origin_family");
		this.t.matches.Add("parents");
		this.t.matches.Add("passengers");
		this.t.matches.Add("past_kings");
		this.t.matches.Add("past_leaders");
		this.t.matches.Add("resources");
		this.t.matches.Add("richest");
		this.t.matches.Add("ruler");
		this.t.matches.Add("ruler_money");
		this.t.matches.Add("saddest");
		this.t.matches.Add("sex");
		this.t.matches.Add("siblings");
		this.t.matches.Add("loot");
		this.t.matches.Add("smartest");
		this.t.matches.Add("speakers");
		this.t.matches.Add("started_at");
		this.t.matches.Add("started_by");
		this.t.matches.Add("status");
		this.t.matches.Add("statuses");
		this.t.matches.Add("strongest");
		this.t.matches.Add("tax");
		this.t.matches.Add("tribute");
		this.t.matches.Add("unhappy_units");
		this.t.matches.Add("unhappy_units_description");
		this.t.matches.Add("unit_age");
		this.t.matches.Add("unit_age_description");
		this.t.matches.Add("upkeep_army");
		this.t.matches.Add("year");
		this.t.matches.Add("year_era");
		this.t.matches.Add("years_ago");
		this.t.matches.Add("youngest");
		this.t.matches.Add("zone_range");
		this.t.matches.Add("zones");
		this.t.matches.Add("zones_description");
		this.t.matches.Add("residents");
		this.t.matches.Add("family_heads");
		this.t.matches.Add("family_members");
		this.t.starts_with.Add("most_");
		this.add(new LocaleGroupAsset
		{
			id = "status"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"status"
		});
		this.add(new LocaleGroupAsset
		{
			id = "technologies"
		});
		this.t.starts_with.Add("tech_");
		this.add(new LocaleGroupAsset
		{
			id = "ui_brushes"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"brush_library"
		});
		this.add(new LocaleGroupAsset
		{
			id = "ui_buttons"
		});
		this.t.starts_with.Add("button_");
		this.add(new LocaleGroupAsset
		{
			id = "ui_clicks"
		});
		this.t.starts_with.Add("click_");
		this.add(new LocaleGroupAsset
		{
			id = "ui_loading_screen"
		});
		this.t.starts_with.Add("loading_");
		this.add(new LocaleGroupAsset
		{
			id = "ui_new_world"
		});
		this.t.starts_with.Add("maptype");
		this.t.starts_with.Add("generat");
		this.t.starts_with.Add("custom_world");
		this.t.starts_with.Add("template_config");
		this.add(new LocaleGroupAsset
		{
			id = "ui_premium"
		});
		this.t.starts_with.Add("free_");
		this.t.starts_with.Add("ios_ad");
		this.t.starts_with.Add("ad_");
		this.t.starts_with.Add("waiting_for_ad");
		this.t.starts_with.Add("unlock_");
		this.t.starts_with.Add("prem_");
		this.t.starts_with.Add("premium_");
		this.t.starts_with.Add("restore");
		this.add(new LocaleGroupAsset
		{
			id = "ui_sort"
		});
		this.t.starts_with_priority.Add("sort_by_");
		this.t.starts_with_priority.Add("default_sort");
		this.add(new LocaleGroupAsset
		{
			id = "ui_speed"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"world_time_scales_library"
		});
		this.add(new LocaleGroupAsset
		{
			id = "ui_tips"
		});
		this.t.starts_with.Add("tip_");
		this.add(new LocaleGroupAsset
		{
			id = "ui_tutorial"
		});
		this.t.starts_with.Add("tut_");
		this.add(new LocaleGroupAsset
		{
			id = "ui_worlds"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"map_sizes",
			"map_gen_templates",
			"map_gen_settings"
		});
		this.t.matches.Add("world");
		this.t.matches.Add("worlds");
		this.t.matches.Add("your_world");
		this.t.matches.Add("your_worlds");
		this.t.matches.Add("no_world_found");
		this.t.matches.Add("name_your_world");
		this.t.matches.Add("describe_your_world");
		this.t.starts_with.Add("map_size");
		this.t.starts_with.Add("new_world");
		this.t.starts_with.Add("modded");
		this.t.starts_with.Add("mods_");
		this.t.starts_with.Add("save");
		this.t.starts_with.Add("load_");
		this.t.starts_with.Add("world_");
		this.t.starts_with.Add("create_worlds");
		this.t.starts_with.Add("future_save_version");
		this.add(new LocaleGroupAsset
		{
			id = "ui_windows"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"list_window_library"
		});
		this.t.starts_with.Add("window_");
		this.t.starts_with.Add("title_");
		this.t.starts_with.Add("official_");
		this.t.starts_with.Add("community");
		this.t.starts_with.Add("facebook");
		this.t.starts_with.Add("link_");
		this.t.starts_with.Add("thank");
		this.t.starts_with.Add("ui_class_");
		this.t.starts_with.Add("under_development");
		this.add(new LocaleGroupAsset
		{
			id = "ui_workshop"
		});
		this.t.starts_with.Add("report");
		this.t.starts_with.Add("upload");
		this.t.starts_with.Add("workshop");
		this.t.starts_with.Add("sharing_your_world");
		this.t.starts_with.Add("world_uploaded");
		this.add(new LocaleGroupAsset
		{
			id = "ui_worldnet"
		});
		this.t.matches.Add("pasted_world_from_clipboard");
		this.t.matches.Add("your_account");
		this.t.matches.Add("logout");
		this.t.matches.Add("logging_in");
		this.t.matches.Add("email");
		this.t.matches.Add("missing_email");
		this.t.matches.Add("email_already_in_use");
		this.t.contains.Add("fav_error");
		this.t.contains.Add("authentication_error");
		this.t.contains.Add("worldnet");
		this.t.contains.Add("logout_");
		this.t.contains.Add("login");
		this.t.contains.Add("invalid");
		this.t.contains.Add("find_world");
		this.t.contains.Add("password");
		this.t.contains.Add("register");
		this.t.starts_with.Add("generic");
		this.t.starts_with.Add("getting_your");
		this.t.starts_with_priority.Add("load_a_world");
		this.t.starts_with_priority.Add("load_world_with_id");
		this.t.starts_with_priority.Add("welcome_worldnet");
		this.t.starts_with.Add("share_your_world");
		this.t.starts_with.Add("status_logged_in");
		this.t.starts_with_priority.Add("username_taken");
		this.t.starts_with_priority.Add("user_");
		this.t.starts_with_priority.Add("world_net");
		this.t.starts_with_priority.Add("worlds_max_maps_uploaded");
		this.t.starts_with_priority.Add("worlds_no");
		this.t.starts_with_priority.Add("worlds_error");
		this.t.starts_with_priority.Add("thank_you_for_your_report");
		this.t.starts_with_priority.Add("num_comments");
		this.t.starts_with_priority.Add("num_subscriptions");
		this.t.starts_with_priority.Add("num_upvotes");
		this.add(new LocaleGroupAsset
		{
			id = "ui_welcome"
		});
		this.t.starts_with_priority.Add("tip0");
		this.t.starts_with_priority.Add("update_");
		this.t.starts_with_priority.Add("feature_");
		this.t.starts_with.Add("vote");
		this.t.starts_with.Add("rate");
		this.t.starts_with.Add("welcome");
		this.t.starts_with.Add("your_heroes");
		this.add(new LocaleGroupAsset
		{
			id = "ui_general"
		});
		this.t.matches.Add("king");
		this.t.matches.Add("knowledge");
		this.t.matches.Add("knowledge_description");
		this.t.matches.Add("show_ui_description");
		this.t.matches.Add("translators");
		this.t.matches.Add("try_again");
		this.t.matches.Add("unlocks_goodie");
		this.t.matches.Add("unlocks_goodies");
		this.t.matches.Add("other");
		this.t.matches.Add("epic_items");
		this.t.matches.Add("disasters");
		this.t.matches.Add("description");
		this.t.matches.Add("help");
		this.t.matches.Add("about");
		this.t.matches.Add("browse_worlds");
		this.t.matches.Add("auto_saved_worlds");
		this.t.matches.Add("auto_saves_tip");
		this.t.matches.Add("auto_saves_tip_description");
		this.t.matches.Add("brush");
		this.t.matches.Add("cancel");
		this.t.matches.Add("canceled");
		this.t.matches.Add("changed_brush");
		this.t.matches.Add("changed_worldspeed");
		this.t.matches.Add("close");
		this.t.matches.Add("color");
		this.t.matches.Add("continue");
		this.t.matches.Add("copied_world_to_clipboard");
		this.t.matches.Add("date");
		this.t.matches.Add("delete");
		this.t.matches.Add("delete_confirmation");
		this.t.matches.Add("delete_confirmation_warning");
		this.t.matches.Add("downloads");
		this.t.matches.Add("enjoy_the_game");
		this.t.matches.Add("error_description");
		this.t.matches.Add("error_happened_logs");
		this.t.matches.Add("game_created_by");
		this.t.matches.Add("game_paused");
		this.t.matches.Add("game_unpaused");
		this.t.matches.Add("get_it");
		this.t.matches.Add("getting_worlds");
		this.t.matches.Add("go_back");
		this.t.matches.Add("history");
		this.t.matches.Add("leave_feedback");
		this.t.matches.Add("logs_folder");
		this.t.matches.Add("mouse_wheel");
		this.t.matches.Add("not_found");
		this.t.matches.Add("open_console");
		this.t.matches.Add("open_in_steam");
		this.t.matches.Add("open_workshop");
		this.t.matches.Add("patch_log");
		this.t.matches.Add("pause_ages");
		this.t.matches.Add("pause_ages_description");
		this.t.matches.Add("premium");
		this.t.matches.Add("press_for_next_tip");
		this.t.matches.Add("quit_game_desktop");
		this.t.matches.Add("reset");
		this.t.matches.Add("send_feedback");
		this.t.matches.Add("show_ui");
		this.t.matches.Add("short_on");
		this.t.matches.Add("short_off");
		this.t.matches.Add("sounds");
		this.t.matches.Add("sounds_ambient");
		this.t.matches.Add("load");
		this.t.matches.Add("actor_locked_tooltip_text_achievement");
		this.t.matches.Add("actor_locked_tooltip_text_exploration");
		this.t.matches.Add("news");
		this.t.matches.Add("outdated_version");
		this.t.matches.Add("overview");
		this.t.matches.Add("warning");
		this.t.matches.Add("stats");
		this.t.matches.Add("statistics");
		this.t.matches.Add("special_thanks");
		this.t.matches.Add("slot");
		this.t.matches.Add("plays");
		this.t.matches.Add("empty_dead_list");
		this.t.matches.Add("empty_non_sapient_list");
		this.t.matches.Add("empty_sapient_list");
		this.t.matches.Add("infinity_coin_used");
		this.t.matches.Add("watch_ad");
		this.t.matches.Add("play_now");
		this.t.starts_with.Add("news_");
		this.add(new LocaleGroupAsset
		{
			id = "world_ages"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"month_library",
			"era_library"
		});
		this.t.matches.Add("randomize_ages");
		this.t.matches.Add("randomize_ages_description");
		this.t.starts_with.Add("next_age_");
		this.t.starts_with.Add("age_");
		this.t.starts_with.Add("ages_");
		this.t.starts_with.Add("all_ages");
		this.t.starts_with_priority.Add("world_age");
		this.add(new LocaleGroupAsset
		{
			id = "world_laws"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"world_laws_library",
			"world_law_groups"
		});
		this.t.starts_with_priority.Add("forbidden_knowledg");
		this.t.starts_with_priority.Add("world_curse");
		this.t.starts_with_priority.Add("world_law");
		this.add(new LocaleGroupAsset
		{
			id = "unit_tasks"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"decisions_library",
			"beh_actor",
			"job_actor"
		});
		this.add(new LocaleGroupAsset
		{
			id = "mind"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"neural_layer_library"
		});
		this.t.contains.Add("toggle_all_neurons");
		this.t.contains.Add("toggle_all_neurons_description");
		this.add(new LocaleGroupAsset
		{
			id = "favorites"
		});
		this.t.contains.Add("favorite");
		this.add(new LocaleGroupAsset
		{
			id = "items"
		});
		this.t.libraries = AssetLibrary<LocaleGroupAsset>.a<string>(new string[]
		{
			"items",
			"items_modifiers",
			"item_groups"
		});
		this.t.starts_with.Add("item_");
		this.t.contains.Add("equipment");
		this.add(new LocaleGroupAsset
		{
			id = "kingdoms"
		});
		this.t.contains.Add("_kingdom_");
		this.t.starts_with.Add("kingdom");
		this.t.starts_with.Add("village");
		this.add(new LocaleGroupAsset
		{
			id = "_others"
		});
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x000855E9 File Offset: 0x000837E9
	public override void editorDiagnostic()
	{
	}

	// Token: 0x04000964 RID: 2404
	private static Dictionary<string, LocaleGroupAsset> _already_found = new Dictionary<string, LocaleGroupAsset>();
}
