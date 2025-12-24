using System;
using System.Collections.Generic;
using Beebyte.Obfuscator;

// Token: 0x02000585 RID: 1413
[ObfuscateLiterals]
[Serializable]
public class StatisticsLibrary : AssetLibrary<StatisticsAsset>
{
	// Token: 0x06002EC9 RID: 11977 RVA: 0x0016763A File Offset: 0x0016583A
	public override void init()
	{
		base.init();
		this.addStatsGeneralMain();
		this.addStats();
		this.addStatsNoos();
		this.addStatsDeaths();
		this.addStatsTiles();
		this.addStatsBiomes();
	}

	// Token: 0x06002ECA RID: 11978 RVA: 0x00167668 File Offset: 0x00165868
	private void addStats()
	{
		StatisticsAsset statisticsAsset = new StatisticsAsset();
		statisticsAsset.id = "world_name";
		statisticsAsset.rarity = 1;
		statisticsAsset.string_action = ((StatisticsAsset _) => World.world.map_stats.name ?? "");
		this.add(statisticsAsset);
		StatisticsAsset statisticsAsset2 = new StatisticsAsset();
		statisticsAsset2.id = "world_statistics_infected";
		statisticsAsset2.list_window_meta_type = MetaType.Unit;
		statisticsAsset2.localized_key = "world_statistics_infected";
		statisticsAsset2.steam_activity = "#Status_stat_value";
		statisticsAsset2.rarity = 1;
		statisticsAsset2.path_icon = "ui/Icons/actor_traits/iconInfected";
		statisticsAsset2.long_action = delegate(StatisticsAsset _)
		{
			long tVal = 0L;
			List<Actor> tActorList = World.world.units.getSimpleList();
			for (int i = 0; i < tActorList.Count; i++)
			{
				if (tActorList[i].isSick())
				{
					tVal += 1L;
				}
			}
			return tVal;
		};
		statisticsAsset2.is_world_statistics = true;
		statisticsAsset2.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset2);
		StatisticsAsset statisticsAsset3 = new StatisticsAsset();
		statisticsAsset3.id = "world_statistics_houses";
		statisticsAsset3.localized_key_description = "houses".Description();
		statisticsAsset3.list_window_meta_type = MetaType.City;
		statisticsAsset3.path_icon = "ui/Icons/iconBuildings";
		statisticsAsset3.long_action = delegate(StatisticsAsset _)
		{
			long tVal = 0L;
			List<Building> tBuildingList = World.world.buildings.getSimpleList();
			for (int i = 0; i < tBuildingList.Count; i++)
			{
				if (tBuildingList[i].asset.city_building)
				{
					tVal += 1L;
				}
			}
			return tVal;
		};
		statisticsAsset3.is_world_statistics = true;
		statisticsAsset3.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset3);
		StatisticsAsset statisticsAsset4 = new StatisticsAsset();
		statisticsAsset4.id = "world_statistics_houses_built";
		statisticsAsset4.list_window_meta_type = MetaType.World;
		statisticsAsset4.path_icon = "ui/Icons/citizen_jobs/iconCitizenJobBuilder";
		statisticsAsset4.long_action = ((StatisticsAsset _) => World.world.map_stats.housesBuilt);
		this.add(statisticsAsset4);
		this.add(new StatisticsAsset
		{
			id = "houses",
			rarity = 4,
			path_icon = "ui/Icons/iconBuildings",
			string_action = delegate(StatisticsAsset _)
			{
				string tResult = "";
				long tHouses = this.get("world_statistics_houses").long_action(null);
				long tDestroyedHouses = this.get("world_statistics_houses_destroyed").long_action(null);
				if (tHouses < 1L && tDestroyedHouses < 1L)
				{
					return "";
				}
				tResult += LocalizedTextManager.getText("world_statistics_houses_all", null, false);
				tResult = tResult.Replace("$houses$", tHouses.ToText());
				return tResult.Replace("$destroyed$", tDestroyedHouses.ToText());
			}
		});
		StatisticsAsset statisticsAsset5 = new StatisticsAsset();
		statisticsAsset5.id = "alliances";
		statisticsAsset5.localized_key = "statistics_alliances";
		statisticsAsset5.list_window_meta_type = MetaType.Alliance;
		statisticsAsset5.path_icon = "ui/Icons/iconAllianceList";
		statisticsAsset5.long_action = ((StatisticsAsset _) => (long)World.world.alliances.Count);
		this.add(statisticsAsset5);
		StatisticsAsset statisticsAsset6 = new StatisticsAsset();
		statisticsAsset6.id = "books";
		statisticsAsset6.localized_key = "books";
		statisticsAsset6.list_window_meta_type = MetaType.Language;
		statisticsAsset6.path_icon = "ui/Icons/iconBooks";
		statisticsAsset6.long_action = ((StatisticsAsset _) => (long)World.world.books.Count);
		this.add(statisticsAsset6);
		StatisticsAsset statisticsAsset7 = new StatisticsAsset();
		statisticsAsset7.id = "clans";
		statisticsAsset7.localized_key = "statistics_clans";
		statisticsAsset7.list_window_meta_type = MetaType.Clan;
		statisticsAsset7.path_icon = "ui/Icons/iconClanList";
		statisticsAsset7.long_action = ((StatisticsAsset _) => (long)World.world.clans.Count);
		this.add(statisticsAsset7);
		StatisticsAsset statisticsAsset8 = new StatisticsAsset();
		statisticsAsset8.id = "cultures";
		statisticsAsset8.localized_key = "statistics_cultures";
		statisticsAsset8.list_window_meta_type = MetaType.Culture;
		statisticsAsset8.path_icon = "ui/Icons/iconCultureList";
		statisticsAsset8.long_action = ((StatisticsAsset _) => (long)World.world.cultures.Count);
		this.add(statisticsAsset8);
		StatisticsAsset statisticsAsset9 = new StatisticsAsset();
		statisticsAsset9.id = "families";
		statisticsAsset9.localized_key = "statistics_families";
		statisticsAsset9.list_window_meta_type = MetaType.Family;
		statisticsAsset9.path_icon = "ui/Icons/iconFamilyList";
		statisticsAsset9.long_action = ((StatisticsAsset _) => (long)World.world.families.Count);
		this.add(statisticsAsset9);
		StatisticsAsset statisticsAsset10 = new StatisticsAsset();
		statisticsAsset10.id = "plots";
		statisticsAsset10.localized_key = "statistics_plots";
		statisticsAsset10.list_window_meta_type = MetaType.Plot;
		statisticsAsset10.path_icon = "ui/Icons/iconPlotList";
		statisticsAsset10.long_action = ((StatisticsAsset _) => (long)World.world.plots.Count);
		this.add(statisticsAsset10);
		StatisticsAsset statisticsAsset11 = new StatisticsAsset();
		statisticsAsset11.id = "languages";
		statisticsAsset11.localized_key = "statistics_languages";
		statisticsAsset11.list_window_meta_type = MetaType.Language;
		statisticsAsset11.path_icon = "ui/Icons/iconLanguageList";
		statisticsAsset11.long_action = ((StatisticsAsset _) => (long)World.world.languages.Count);
		this.add(statisticsAsset11);
		StatisticsAsset statisticsAsset12 = new StatisticsAsset();
		statisticsAsset12.id = "religions";
		statisticsAsset12.localized_key = "statistics_religions";
		statisticsAsset12.list_window_meta_type = MetaType.Religion;
		statisticsAsset12.path_icon = "ui/Icons/iconReligionList";
		statisticsAsset12.long_action = ((StatisticsAsset _) => (long)World.world.religions.Count);
		this.add(statisticsAsset12);
		StatisticsAsset statisticsAsset13 = new StatisticsAsset();
		statisticsAsset13.id = "subspecies";
		statisticsAsset13.localized_key = "statistics_subspecies";
		statisticsAsset13.list_window_meta_type = MetaType.Subspecies;
		statisticsAsset13.path_icon = "ui/Icons/iconSpecies";
		statisticsAsset13.long_action = ((StatisticsAsset _) => (long)World.world.subspecies.Count);
		this.add(statisticsAsset13);
		StatisticsAsset statisticsAsset14 = new StatisticsAsset();
		statisticsAsset14.id = "wars";
		statisticsAsset14.localized_key = "statistics_wars";
		statisticsAsset14.list_window_meta_type = MetaType.War;
		statisticsAsset14.path_icon = "ui/Icons/iconWar";
		statisticsAsset14.long_action = ((StatisticsAsset _) => World.world.wars.countActiveWars());
		this.add(statisticsAsset14);
		StatisticsAsset statisticsAsset15 = new StatisticsAsset();
		statisticsAsset15.id = "kingdoms";
		statisticsAsset15.localized_key = "statistics_kingdoms";
		statisticsAsset15.list_window_meta_type = MetaType.Kingdom;
		statisticsAsset15.path_icon = "ui/Icons/iconKingdomList";
		statisticsAsset15.long_action = ((StatisticsAsset _) => (long)World.world.kingdoms.Count);
		this.add(statisticsAsset15);
		StatisticsAsset statisticsAsset16 = new StatisticsAsset();
		statisticsAsset16.id = "villages";
		statisticsAsset16.localized_key = "statistics_villages";
		statisticsAsset16.list_window_meta_type = MetaType.City;
		statisticsAsset16.path_icon = "ui/Icons/iconCitySelect";
		statisticsAsset16.long_action = ((StatisticsAsset _) => (long)World.world.cities.Count);
		this.add(statisticsAsset16);
		StatisticsAsset statisticsAsset17 = new StatisticsAsset();
		statisticsAsset17.id = "world_statistics_population_total";
		statisticsAsset17.localized_key = "world_statistics_population_total";
		statisticsAsset17.steam_activity = "#Status_stat_value";
		statisticsAsset17.rarity = 2;
		statisticsAsset17.list_window_meta_type = MetaType.Unit;
		statisticsAsset17.path_icon = "ui/Icons/iconPopulation";
		statisticsAsset17.long_action = ((StatisticsAsset _) => (long)World.world.units.Count);
		this.add(statisticsAsset17);
		StatisticsAsset statisticsAsset18 = new StatisticsAsset();
		statisticsAsset18.id = "world_statistics_beasts";
		statisticsAsset18.list_window_meta_type = MetaType.Unit;
		statisticsAsset18.path_icon = "ui/Icons/worldrules/icon_animalspawn";
		statisticsAsset18.long_action = delegate(StatisticsAsset _)
		{
			long tCount = 0L;
			List<Actor> tActorList = World.world.units.getSimpleList();
			for (int i = 0; i < tActorList.Count; i++)
			{
				if (!tActorList[i].isSapient())
				{
					tCount += 1L;
				}
			}
			return tCount;
		};
		statisticsAsset18.is_world_statistics = true;
		statisticsAsset18.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset18);
		StatisticsAsset statisticsAsset19 = new StatisticsAsset();
		statisticsAsset19.id = "world_statistics_trees";
		statisticsAsset19.path_icon = "ui/Icons/iconFertilizerTrees";
		statisticsAsset19.long_action = delegate(StatisticsAsset _)
		{
			long tCount = 0L;
			List<Building> tBuildingList = World.world.buildings.getSimpleList();
			for (int i = 0; i < tBuildingList.Count; i++)
			{
				if (tBuildingList[i].asset.building_type == BuildingType.Building_Tree)
				{
					tCount += 1L;
				}
			}
			return tCount;
		};
		statisticsAsset19.is_world_statistics = true;
		statisticsAsset19.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset19);
		StatisticsAsset statisticsAsset20 = new StatisticsAsset();
		statisticsAsset20.id = "world_statistics_vegetation";
		statisticsAsset20.path_icon = "ui/Icons/iconFertilizerPlants";
		statisticsAsset20.long_action = delegate(StatisticsAsset _)
		{
			long tCount = 0L;
			List<Building> tBuildingList = World.world.buildings.getSimpleList();
			for (int i = 0; i < tBuildingList.Count; i++)
			{
				Building tBuilding = tBuildingList[i];
				if (tBuilding.asset.building_type == BuildingType.Building_Tree || tBuilding.asset.building_type == BuildingType.Building_Plant)
				{
					tCount += 1L;
				}
			}
			return tCount;
		};
		this.add(statisticsAsset20);
		StatisticsAsset statisticsAsset21 = new StatisticsAsset();
		statisticsAsset21.id = "world_statistics_islands";
		statisticsAsset21.path_icon = "ui/Icons/iconZones";
		statisticsAsset21.long_action = ((StatisticsAsset _) => (long)World.world.islands_calculator.countLandIslands());
		statisticsAsset21.is_world_statistics = true;
		statisticsAsset21.world_stats_tabs = WorldStatsTabs.Everything;
		this.add(statisticsAsset21);
		StatisticsAsset statisticsAsset22 = new StatisticsAsset();
		statisticsAsset22.id = "world_statistics_creatures_born";
		statisticsAsset22.list_window_meta_type = MetaType.Unit;
		statisticsAsset22.path_icon = "ui/Icons/iconBirths";
		statisticsAsset22.long_action = ((StatisticsAsset _) => World.world.map_stats.creaturesBorn);
		statisticsAsset22.is_world_statistics = true;
		statisticsAsset22.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset22);
		StatisticsAsset statisticsAsset23 = new StatisticsAsset();
		statisticsAsset23.id = "world_statistics_creatures_created";
		statisticsAsset23.list_window_meta_type = MetaType.Unit;
		statisticsAsset23.path_icon = "ui/Icons/actor_traits/iconMiracleBorn";
		statisticsAsset23.long_action = ((StatisticsAsset _) => World.world.map_stats.creaturesCreated);
		statisticsAsset23.is_world_statistics = true;
		statisticsAsset23.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset23);
		StatisticsAsset statisticsAsset24 = new StatisticsAsset();
		statisticsAsset24.id = "world_statistics_subspecies_created";
		statisticsAsset24.localized_key_description = "statistics_subspecies".Description();
		statisticsAsset24.list_window_meta_type = MetaType.Subspecies;
		statisticsAsset24.path_icon = "ui/Icons/iconSpecies";
		statisticsAsset24.long_action = ((StatisticsAsset _) => World.world.map_stats.subspeciesCreated);
		statisticsAsset24.is_world_statistics = true;
		statisticsAsset24.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset24);
		StatisticsAsset statisticsAsset25 = new StatisticsAsset();
		statisticsAsset25.id = "statistics_total_playtime";
		statisticsAsset25.is_game_statistics = true;
		statisticsAsset25.path_icon = "ui/Icons/iconClock";
		statisticsAsset25.string_action = ((StatisticsAsset _) => Toolbox.formatTime((float)World.world.game_stats.data.gameTime));
		this.add(statisticsAsset25);
		StatisticsAsset statisticsAsset26 = new StatisticsAsset();
		statisticsAsset26.id = "statistics_trees_grown";
		statisticsAsset26.path_icon = "ui/Icons/worldrules/icon_grow_trees";
		statisticsAsset26.is_game_statistics = true;
		statisticsAsset26.long_action = ((StatisticsAsset _) => World.world.game_stats.data.treesGrown);
		this.add(statisticsAsset26);
		StatisticsAsset statisticsAsset27 = new StatisticsAsset();
		statisticsAsset27.id = "statistics_flora_grown";
		statisticsAsset27.path_icon = "ui/Icons/worldrules/icon_flora_density_high";
		statisticsAsset27.is_game_statistics = true;
		statisticsAsset27.long_action = ((StatisticsAsset _) => World.world.game_stats.data.floraGrown);
		this.add(statisticsAsset27);
		StatisticsAsset statisticsAsset28 = new StatisticsAsset();
		statisticsAsset28.id = "statistics_meteorites_launched";
		statisticsAsset28.is_game_statistics = true;
		statisticsAsset28.path_icon = "ui/Icons/iconMeteorite";
		statisticsAsset28.long_action = ((StatisticsAsset _) => World.world.game_stats.data.meteoritesLaunched);
		this.add(statisticsAsset28);
		StatisticsAsset statisticsAsset29 = new StatisticsAsset();
		statisticsAsset29.id = "statistics_pixels_exploded";
		statisticsAsset29.is_game_statistics = true;
		statisticsAsset29.path_icon = "ui/Icons/worldrules/icon_exploding_mushrooms";
		statisticsAsset29.long_action = ((StatisticsAsset _) => World.world.game_stats.data.pixelsExploded);
		this.add(statisticsAsset29);
		StatisticsAsset statisticsAsset30 = new StatisticsAsset();
		statisticsAsset30.id = "statistics_creatures_created";
		statisticsAsset30.is_game_statistics = true;
		statisticsAsset30.path_icon = "ui/Icons/actor_traits/iconMiracleBorn";
		statisticsAsset30.long_action = ((StatisticsAsset _) => World.world.game_stats.data.creaturesCreated);
		this.add(statisticsAsset30);
		StatisticsAsset statisticsAsset31 = new StatisticsAsset();
		statisticsAsset31.id = "statistics_creatures_born";
		statisticsAsset31.is_game_statistics = true;
		statisticsAsset31.path_icon = "ui/Icons/iconBirths";
		statisticsAsset31.long_action = ((StatisticsAsset _) => World.world.game_stats.data.creaturesBorn);
		this.add(statisticsAsset31);
		StatisticsAsset statisticsAsset32 = new StatisticsAsset();
		statisticsAsset32.id = "statistics_creatures_died";
		statisticsAsset32.is_game_statistics = true;
		statisticsAsset32.path_icon = "ui/Icons/iconDead";
		statisticsAsset32.long_action = ((StatisticsAsset _) => World.world.game_stats.data.creaturesDied);
		this.add(statisticsAsset32);
		StatisticsAsset statisticsAsset33 = new StatisticsAsset();
		statisticsAsset33.id = "statistics_bombs_dropped";
		statisticsAsset33.is_game_statistics = true;
		statisticsAsset33.path_icon = "ui/Icons/iconBomb";
		statisticsAsset33.long_action = ((StatisticsAsset _) => World.world.game_stats.data.bombsDropped);
		this.add(statisticsAsset33);
		StatisticsAsset statisticsAsset34 = new StatisticsAsset();
		statisticsAsset34.id = "statistics_subspecies_created";
		statisticsAsset34.localized_key_description = "statistics_subspecies".Description();
		statisticsAsset34.is_game_statistics = true;
		statisticsAsset34.path_icon = "ui/Icons/iconSpecies";
		statisticsAsset34.long_action = ((StatisticsAsset _) => World.world.game_stats.data.subspeciesCreated);
		this.add(statisticsAsset34);
		StatisticsAsset statisticsAsset35 = new StatisticsAsset();
		statisticsAsset35.id = "statistics_subspecies_extinct";
		statisticsAsset35.is_game_statistics = true;
		statisticsAsset35.path_icon = "ui/Icons/iconSpeciesExtinct";
		statisticsAsset35.long_action = ((StatisticsAsset _) => World.world.game_stats.data.subspeciesExtinct);
		this.add(statisticsAsset35);
		StatisticsAsset statisticsAsset36 = new StatisticsAsset();
		statisticsAsset36.id = "statistics_languages_created";
		statisticsAsset36.localized_key_description = "statistics_languages".Description();
		statisticsAsset36.is_game_statistics = true;
		statisticsAsset36.path_icon = "ui/Icons/iconLanguage";
		statisticsAsset36.long_action = ((StatisticsAsset _) => World.world.game_stats.data.languagesCreated);
		this.add(statisticsAsset36);
		StatisticsAsset statisticsAsset37 = new StatisticsAsset();
		statisticsAsset37.id = "statistics_languages_forgotten";
		statisticsAsset37.is_game_statistics = true;
		statisticsAsset37.path_icon = "ui/Icons/iconLanguageForgotten";
		statisticsAsset37.long_action = ((StatisticsAsset _) => World.world.game_stats.data.languagesForgotten);
		this.add(statisticsAsset37);
		StatisticsAsset statisticsAsset38 = new StatisticsAsset();
		statisticsAsset38.id = "statistics_cultures_created";
		statisticsAsset38.localized_key_description = "statistics_cultures".Description();
		statisticsAsset38.is_game_statistics = true;
		statisticsAsset38.path_icon = "ui/Icons/iconCulture";
		statisticsAsset38.long_action = ((StatisticsAsset _) => World.world.game_stats.data.culturesCreated);
		this.add(statisticsAsset38);
		StatisticsAsset statisticsAsset39 = new StatisticsAsset();
		statisticsAsset39.id = "statistics_cultures_forgotten";
		statisticsAsset39.is_game_statistics = true;
		statisticsAsset39.path_icon = "ui/Icons/iconCultureForgotten";
		statisticsAsset39.long_action = ((StatisticsAsset _) => World.world.game_stats.data.culturesForgotten);
		this.add(statisticsAsset39);
		StatisticsAsset statisticsAsset40 = new StatisticsAsset();
		statisticsAsset40.id = "statistics_families_created";
		statisticsAsset40.localized_key_description = "statistics_families".Description();
		statisticsAsset40.is_game_statistics = true;
		statisticsAsset40.path_icon = "ui/Icons/iconNewFamily";
		statisticsAsset40.long_action = ((StatisticsAsset _) => World.world.game_stats.data.familiesCreated);
		this.add(statisticsAsset40);
		StatisticsAsset statisticsAsset41 = new StatisticsAsset();
		statisticsAsset41.id = "statistics_families_destroyed";
		statisticsAsset41.is_game_statistics = true;
		statisticsAsset41.path_icon = "ui/Icons/iconFamilyDestroyed";
		statisticsAsset41.long_action = ((StatisticsAsset _) => World.world.game_stats.data.familiesDestroyed);
		this.add(statisticsAsset41);
		StatisticsAsset statisticsAsset42 = new StatisticsAsset();
		statisticsAsset42.id = "statistics_clans_created";
		statisticsAsset42.localized_key_description = "statistics_clans".Description();
		statisticsAsset42.is_game_statistics = true;
		statisticsAsset42.path_icon = "ui/Icons/iconClan";
		statisticsAsset42.long_action = ((StatisticsAsset _) => World.world.game_stats.data.clansCreated);
		this.add(statisticsAsset42);
		StatisticsAsset statisticsAsset43 = new StatisticsAsset();
		statisticsAsset43.id = "statistics_clans_destroyed";
		statisticsAsset43.is_game_statistics = true;
		statisticsAsset43.path_icon = "ui/Icons/iconClanDestroyed";
		statisticsAsset43.long_action = ((StatisticsAsset _) => World.world.game_stats.data.clansDestroyed);
		this.add(statisticsAsset43);
		StatisticsAsset statisticsAsset44 = new StatisticsAsset();
		statisticsAsset44.id = "statistics_books_written";
		statisticsAsset44.localized_key_description = "books".Description();
		statisticsAsset44.is_game_statistics = true;
		statisticsAsset44.path_icon = "ui/Icons/iconBooksWritten";
		statisticsAsset44.long_action = ((StatisticsAsset _) => World.world.game_stats.data.booksWritten);
		this.add(statisticsAsset44);
		StatisticsAsset statisticsAsset45 = new StatisticsAsset();
		statisticsAsset45.id = "statistics_books_read";
		statisticsAsset45.is_game_statistics = true;
		statisticsAsset45.path_icon = "ui/Icons/iconBooksRead";
		statisticsAsset45.long_action = ((StatisticsAsset _) => World.world.game_stats.data.booksRead);
		this.add(statisticsAsset45);
		StatisticsAsset statisticsAsset46 = new StatisticsAsset();
		statisticsAsset46.id = "statistics_books_burnt";
		statisticsAsset46.is_game_statistics = true;
		statisticsAsset46.path_icon = "ui/Icons/iconBooksDestroyed";
		statisticsAsset46.long_action = ((StatisticsAsset _) => World.world.game_stats.data.booksBurnt);
		this.add(statisticsAsset46);
		StatisticsAsset statisticsAsset47 = new StatisticsAsset();
		statisticsAsset47.id = "statistics_religions_created";
		statisticsAsset47.localized_key_description = "statistics_religions".Description();
		statisticsAsset47.is_game_statistics = true;
		statisticsAsset47.path_icon = "ui/Icons/iconReligion";
		statisticsAsset47.long_action = ((StatisticsAsset _) => World.world.game_stats.data.religionsCreated);
		this.add(statisticsAsset47);
		StatisticsAsset statisticsAsset48 = new StatisticsAsset();
		statisticsAsset48.id = "statistics_religions_forgotten";
		statisticsAsset48.is_game_statistics = true;
		statisticsAsset48.path_icon = "ui/Icons/iconReligionForgotten";
		statisticsAsset48.long_action = ((StatisticsAsset _) => World.world.game_stats.data.religionsForgotten);
		this.add(statisticsAsset48);
		StatisticsAsset statisticsAsset49 = new StatisticsAsset();
		statisticsAsset49.id = "statistics_kingdoms_created";
		statisticsAsset49.localized_key_description = "statistics_kingdoms".Description();
		statisticsAsset49.is_game_statistics = true;
		statisticsAsset49.path_icon = "ui/Icons/iconKingdom";
		statisticsAsset49.long_action = ((StatisticsAsset _) => World.world.game_stats.data.kingdomsCreated);
		this.add(statisticsAsset49);
		StatisticsAsset statisticsAsset50 = new StatisticsAsset();
		statisticsAsset50.id = "statistics_kingdoms_destroyed";
		statisticsAsset50.is_game_statistics = true;
		statisticsAsset50.path_icon = "ui/Icons/iconKingdomDestroyed";
		statisticsAsset50.long_action = ((StatisticsAsset _) => World.world.game_stats.data.kingdomsDestroyed);
		this.add(statisticsAsset50);
		StatisticsAsset statisticsAsset51 = new StatisticsAsset();
		statisticsAsset51.id = "statistics_cities_created";
		statisticsAsset51.localized_key_description = "statistics_villages".Description();
		statisticsAsset51.is_game_statistics = true;
		statisticsAsset51.path_icon = "ui/Icons/iconCity";
		statisticsAsset51.long_action = ((StatisticsAsset _) => World.world.game_stats.data.citiesCreated);
		this.add(statisticsAsset51);
		StatisticsAsset statisticsAsset52 = new StatisticsAsset();
		statisticsAsset52.id = "statistics_cities_destroyed";
		statisticsAsset52.is_game_statistics = true;
		statisticsAsset52.path_icon = "ui/Icons/iconCityDestroyed";
		statisticsAsset52.long_action = ((StatisticsAsset _) => World.world.game_stats.data.citiesDestroyed);
		this.add(statisticsAsset52);
		StatisticsAsset statisticsAsset53 = new StatisticsAsset();
		statisticsAsset53.id = "statistics_wars_started";
		statisticsAsset53.localized_key_description = "statistics_wars".Description();
		statisticsAsset53.is_game_statistics = true;
		statisticsAsset53.path_icon = "ui/Icons/iconWhisperOfWar";
		statisticsAsset53.long_action = ((StatisticsAsset _) => World.world.game_stats.data.warsStarted);
		this.add(statisticsAsset53);
		StatisticsAsset statisticsAsset54 = new StatisticsAsset();
		statisticsAsset54.id = "statistics_peaces_made";
		statisticsAsset54.is_game_statistics = true;
		statisticsAsset54.path_icon = "ui/Icons/actor_traits/iconPacifist";
		statisticsAsset54.long_action = ((StatisticsAsset _) => World.world.game_stats.data.peacesMade);
		this.add(statisticsAsset54);
		StatisticsAsset statisticsAsset55 = new StatisticsAsset();
		statisticsAsset55.id = "statistics_plots_started";
		statisticsAsset55.localized_key_description = "statistics_plots".Description();
		statisticsAsset55.is_game_statistics = true;
		statisticsAsset55.path_icon = "ui/Icons/iconPlot";
		statisticsAsset55.long_action = ((StatisticsAsset _) => World.world.game_stats.data.plotsStarted);
		this.add(statisticsAsset55);
		StatisticsAsset statisticsAsset56 = new StatisticsAsset();
		statisticsAsset56.id = "statistics_plots_succeeded";
		statisticsAsset56.is_game_statistics = true;
		statisticsAsset56.path_icon = "ui/Icons/iconPlotSucceeded";
		statisticsAsset56.long_action = ((StatisticsAsset _) => World.world.game_stats.data.plotsSucceeded);
		this.add(statisticsAsset56);
		StatisticsAsset statisticsAsset57 = new StatisticsAsset();
		statisticsAsset57.id = "statistics_plots_forgotten";
		statisticsAsset57.is_game_statistics = true;
		statisticsAsset57.path_icon = "ui/Icons/iconPlotForgotten";
		statisticsAsset57.long_action = ((StatisticsAsset _) => World.world.game_stats.data.plotsForgotten);
		this.add(statisticsAsset57);
		StatisticsAsset statisticsAsset58 = new StatisticsAsset();
		statisticsAsset58.id = "statistics_creatures_sacrificed";
		statisticsAsset58.is_game_statistics = true;
		statisticsAsset58.path_icon = "ui/Icons/iconVolcano";
		statisticsAsset58.long_action = ((StatisticsAsset _) => World.world.game_stats.data.creaturesSacrificed);
		this.add(statisticsAsset58);
		StatisticsAsset statisticsAsset59 = new StatisticsAsset();
		statisticsAsset59.id = "statistics_elves_sacrificed";
		statisticsAsset59.is_game_statistics = true;
		statisticsAsset59.path_icon = "ui/Icons/iconHateElf";
		statisticsAsset59.long_action = ((StatisticsAsset _) => World.world.game_stats.data.elvesSacrificed);
		this.add(statisticsAsset59);
		StatisticsAsset statisticsAsset60 = new StatisticsAsset();
		statisticsAsset60.id = "statistics_boats_destroyed_by_magnet";
		statisticsAsset60.is_game_statistics = true;
		statisticsAsset60.path_icon = "ui/Icons/iconBoat";
		statisticsAsset60.long_action = ((StatisticsAsset _) => World.world.game_stats.data.boatsDestroyedByMagnet);
		this.add(statisticsAsset60);
	}

	// Token: 0x06002ECB RID: 11979 RVA: 0x00168AE0 File Offset: 0x00166CE0
	private void addStatsGeneralMain()
	{
		StatisticsAsset statisticsAsset = new StatisticsAsset();
		statisticsAsset.id = "world_statistics_time";
		statisticsAsset.localized_key = "world_statistics_time";
		statisticsAsset.steam_activity = "#Status_stat_value";
		statisticsAsset.rarity = 2;
		statisticsAsset.path_icon = "ui/Icons/iconClock";
		statisticsAsset.string_action = ((StatisticsAsset _) => Date.getUIStringYearMonthShort());
		statisticsAsset.is_world_statistics = true;
		statisticsAsset.world_stats_tabs = WorldStatsTabs.Everything;
		this.add(statisticsAsset);
		StatisticsAsset statisticsAsset2 = new StatisticsAsset();
		statisticsAsset2.id = "world_statistics_population";
		statisticsAsset2.localized_key = "world_statistics_population";
		statisticsAsset2.steam_activity = "#Status_stat_value";
		statisticsAsset2.rarity = 2;
		statisticsAsset2.list_window_meta_type = MetaType.Unit;
		statisticsAsset2.path_icon = "ui/Icons/iconPopulationCiv";
		statisticsAsset2.long_action = ((StatisticsAsset _) => (long)World.world.getCivWorldPopulation());
		statisticsAsset2.is_world_statistics = true;
		statisticsAsset2.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset2);
	}

	// Token: 0x06002ECC RID: 11980 RVA: 0x00168BDC File Offset: 0x00166DDC
	public override void post_init()
	{
		base.post_init();
		foreach (StatisticsAsset tAsset in this.list)
		{
			if (tAsset.locale_getter != null)
			{
				tAsset.localized_key = tAsset.locale_getter();
			}
		}
	}

	// Token: 0x06002ECD RID: 11981 RVA: 0x00168C48 File Offset: 0x00166E48
	public override void editorDiagnosticLocales()
	{
		foreach (StatisticsAsset tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
			this.checkLocale(tAsset, tAsset.getDescriptionID());
		}
		foreach (StatisticsAsset tAsset2 in this.list)
		{
			if (tAsset2.is_world_statistics)
			{
				string tGameStatsId = tAsset2.id.Replace("world_", "");
				if (this.has(tGameStatsId))
				{
					StatisticsAsset tGameStats = this.get(tGameStatsId);
					if (tGameStats.path_icon != tAsset2.path_icon)
					{
						BaseAssetLibrary.logAssetError(string.Concat(new string[]
						{
							"<e>StatisticsLibrary</e>: World Stat <b>",
							tAsset2.path_icon,
							"</b> has different icon than Game Stat <b>",
							tGameStats.path_icon,
							"</b>"
						}), tAsset2.id);
					}
				}
			}
		}
		foreach (StatisticsAsset tAsset3 in this.list)
		{
			if (tAsset3.is_game_statistics)
			{
				string tWorldStatsId = "world_" + tAsset3.id;
				if (this.has(tWorldStatsId))
				{
					StatisticsAsset tWorldStats = this.get(tWorldStatsId);
					if (tWorldStats.path_icon != tAsset3.path_icon)
					{
						BaseAssetLibrary.logAssetError(string.Concat(new string[]
						{
							"<e>StatisticsLibrary</e>: Game Stat <b>",
							tAsset3.path_icon,
							"</b> has different icon than World Stat <b>",
							tWorldStats.path_icon,
							"</b>"
						}), tAsset3.id);
					}
				}
			}
		}
		base.editorDiagnosticLocales();
	}

	// Token: 0x06002ECE RID: 11982 RVA: 0x00168E44 File Offset: 0x00167044
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (StatisticsAsset tAsset in this.list)
		{
			for (int i = 0; i < tAsset.rarity; i++)
			{
				StatisticsLibrary.power_tracker_pool.Add(tAsset);
			}
		}
	}

	// Token: 0x06002ECF RID: 11983 RVA: 0x00168EB4 File Offset: 0x001670B4
	private string getDominatingMetaRow(MetaType pType)
	{
		MetaTypeAsset tMetaAsset = AssetManager.meta_type_library.getAsset(pType);
		long tId = this.getDominatingMetaId(tMetaAsset);
		IMetaObject tMostPopulated = tMetaAsset.get(tId) as IMetaObject;
		if (tMostPopulated == null || tMostPopulated.countUnits() == 0)
		{
			return StatisticsLibrary._unknown_text;
		}
		string tColor = tMostPopulated.getColor().color_text;
		return Toolbox.coloredText(tMostPopulated.name + Toolbox.coloredGreyPart(tMostPopulated.countUnits(), tColor, false), tColor, false);
	}

	// Token: 0x06002ED0 RID: 11984 RVA: 0x00168F28 File Offset: 0x00167128
	private long getDominatingMetaId(MetaType pType)
	{
		MetaTypeAsset tMetaAsset = AssetManager.meta_type_library.getAsset(pType);
		return this.getDominatingMetaId(tMetaAsset);
	}

	// Token: 0x06002ED1 RID: 11985 RVA: 0x00168F48 File Offset: 0x00167148
	private long getDominatingMetaId(MetaTypeAsset pMetaAsset)
	{
		IMetaObject tMostPopulated = null;
		foreach (NanoObject nanoObject in pMetaAsset.get_list())
		{
			IMetaObject tMeta = (IMetaObject)nanoObject;
			if (tMostPopulated == null || tMeta.countUnits() > tMostPopulated.countUnits())
			{
				tMostPopulated = tMeta;
			}
		}
		if (tMostPopulated == null)
		{
			return -1L;
		}
		return tMostPopulated.getID();
	}

	// Token: 0x06002ED2 RID: 11986 RVA: 0x00168FBC File Offset: 0x001671BC
	private string getOldestMetaRow(MetaType pType)
	{
		MetaTypeAsset tMetaAsset = AssetManager.meta_type_library.getAsset(pType);
		long tId = this.getOldestMetaId(tMetaAsset);
		IMetaObject tOldest = tMetaAsset.get(tId) as IMetaObject;
		if (tOldest == null)
		{
			return StatisticsLibrary._unknown_text;
		}
		string tColor = tOldest.getColor().color_text;
		return Toolbox.coloredText(tOldest.name + Toolbox.coloredGreyPart(tOldest.getAge(), tColor, false), tColor, false);
	}

	// Token: 0x06002ED3 RID: 11987 RVA: 0x00169028 File Offset: 0x00167228
	private long getOldestMetaId(MetaType pType)
	{
		MetaTypeAsset tMetaAsset = AssetManager.meta_type_library.getAsset(pType);
		return this.getOldestMetaId(tMetaAsset);
	}

	// Token: 0x06002ED4 RID: 11988 RVA: 0x00169048 File Offset: 0x00167248
	private long getOldestMetaId(MetaTypeAsset pMetaAsset)
	{
		IMetaObject tOldest = null;
		foreach (NanoObject nanoObject in pMetaAsset.get_list())
		{
			IMetaObject tMeta = (IMetaObject)nanoObject;
			if (tOldest == null || tMeta.getAge() > tOldest.getAge())
			{
				tOldest = tMeta;
			}
		}
		if (tOldest == null)
		{
			return -1L;
		}
		return tOldest.getID();
	}

	// Token: 0x06002ED5 RID: 11989 RVA: 0x001690BC File Offset: 0x001672BC
	public string addToGameplayReport(string pWhatFor)
	{
		string tResult = string.Empty;
		tResult = tResult + pWhatFor + "\n";
		foreach (StatisticsAsset statisticsAsset in this.list)
		{
			string tName = statisticsAsset.getLocaleID().Localize();
			string tDescription = statisticsAsset.getDescriptionID().Localize();
			string tLineInfo = "\n" + tName;
			tLineInfo += "\n";
			if (!string.IsNullOrEmpty(tDescription))
			{
				tLineInfo = tLineInfo + "1: " + tDescription;
			}
			tResult += tLineInfo;
		}
		tResult += "\n\n";
		return tResult;
	}

	// Token: 0x06002ED6 RID: 11990 RVA: 0x0016917C File Offset: 0x0016737C
	public void addStatsBiomes()
	{
		this.addNormalBiomes();
		this.addCreepBiomes();
		this.addSpecialBiomes();
	}

	// Token: 0x06002ED7 RID: 11991 RVA: 0x00169190 File Offset: 0x00167390
	public void addNormalBiomes()
	{
		StatisticsAsset statisticsAsset = new StatisticsAsset();
		statisticsAsset.id = "world_statistics_grass";
		statisticsAsset.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_grass"));
		statisticsAsset.path_icon = "ui/Icons/iconSeedGrass";
		statisticsAsset.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.grass_high.hashset.Count + TopTileLibrary.grass_low.hashset.Count));
		statisticsAsset.is_world_statistics = true;
		statisticsAsset.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset);
		StatisticsAsset statisticsAsset2 = new StatisticsAsset();
		statisticsAsset2.id = "world_statistics_savanna";
		statisticsAsset2.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_savanna"));
		statisticsAsset2.path_icon = "ui/Icons/iconSeedSavanna";
		statisticsAsset2.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.savanna_high.hashset.Count + TopTileLibrary.savanna_low.hashset.Count));
		statisticsAsset2.is_world_statistics = true;
		statisticsAsset2.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset2);
		StatisticsAsset statisticsAsset3 = new StatisticsAsset();
		statisticsAsset3.id = "world_statistics_jungle";
		statisticsAsset3.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_jungle"));
		statisticsAsset3.path_icon = "ui/Icons/iconSeedJungle";
		statisticsAsset3.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.jungle_high.hashset.Count + TopTileLibrary.jungle_low.hashset.Count));
		statisticsAsset3.is_world_statistics = true;
		statisticsAsset3.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset3);
		StatisticsAsset statisticsAsset4 = new StatisticsAsset();
		statisticsAsset4.id = "world_statistics_desert";
		statisticsAsset4.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_desert"));
		statisticsAsset4.path_icon = "ui/Icons/iconSeedDesert";
		statisticsAsset4.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.desert_high.hashset.Count + TopTileLibrary.desert_low.hashset.Count));
		statisticsAsset4.is_world_statistics = true;
		statisticsAsset4.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset4);
		StatisticsAsset statisticsAsset5 = new StatisticsAsset();
		statisticsAsset5.id = "world_statistics_lemon";
		statisticsAsset5.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_lemon"));
		statisticsAsset5.path_icon = "ui/Icons/iconSeedLemon";
		statisticsAsset5.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.lemon_high.hashset.Count + TopTileLibrary.lemon_low.hashset.Count));
		statisticsAsset5.is_world_statistics = true;
		statisticsAsset5.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset5);
		StatisticsAsset statisticsAsset6 = new StatisticsAsset();
		statisticsAsset6.id = "world_statistics_permafrost";
		statisticsAsset6.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_permafrost"));
		statisticsAsset6.path_icon = "ui/Icons/iconSeedPermafrost";
		statisticsAsset6.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.permafrost_high.hashset.Count + TopTileLibrary.permafrost_low.hashset.Count));
		statisticsAsset6.is_world_statistics = true;
		statisticsAsset6.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset6);
		StatisticsAsset statisticsAsset7 = new StatisticsAsset();
		statisticsAsset7.id = "world_statistics_swamp";
		statisticsAsset7.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_swamp"));
		statisticsAsset7.path_icon = "ui/Icons/iconSeedSwamp";
		statisticsAsset7.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.swamp_high.hashset.Count + TopTileLibrary.swamp_low.hashset.Count));
		statisticsAsset7.is_world_statistics = true;
		statisticsAsset7.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset7);
		StatisticsAsset statisticsAsset8 = new StatisticsAsset();
		statisticsAsset8.id = "world_statistics_crystal";
		statisticsAsset8.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_crystal"));
		statisticsAsset8.path_icon = "ui/Icons/iconSeedCrystal";
		statisticsAsset8.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.crystal_high.hashset.Count + TopTileLibrary.crystal_low.hashset.Count));
		statisticsAsset8.is_world_statistics = true;
		statisticsAsset8.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset8);
		StatisticsAsset statisticsAsset9 = new StatisticsAsset();
		statisticsAsset9.id = "world_statistics_enchanted";
		statisticsAsset9.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_enchanted"));
		statisticsAsset9.path_icon = "ui/Icons/iconSeedEnchanted";
		statisticsAsset9.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.enchanted_high.hashset.Count + TopTileLibrary.enchanted_low.hashset.Count));
		statisticsAsset9.is_world_statistics = true;
		statisticsAsset9.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset9);
		StatisticsAsset statisticsAsset10 = new StatisticsAsset();
		statisticsAsset10.id = "world_statistics_corruption";
		statisticsAsset10.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_corrupted"));
		statisticsAsset10.path_icon = "ui/Icons/iconSeedCorrupted";
		statisticsAsset10.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.corruption_high.hashset.Count + TopTileLibrary.corruption_low.hashset.Count));
		statisticsAsset10.is_world_statistics = true;
		statisticsAsset10.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset10);
		StatisticsAsset statisticsAsset11 = new StatisticsAsset();
		statisticsAsset11.id = "world_statistics_infernal";
		statisticsAsset11.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_infernal"));
		statisticsAsset11.path_icon = "ui/Icons/iconSeedInfernal";
		statisticsAsset11.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.infernal_high.hashset.Count + TopTileLibrary.infernal_low.hashset.Count));
		statisticsAsset11.is_world_statistics = true;
		statisticsAsset11.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset11);
		StatisticsAsset statisticsAsset12 = new StatisticsAsset();
		statisticsAsset12.id = "world_statistics_candy";
		statisticsAsset12.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_candy"));
		statisticsAsset12.path_icon = "ui/Icons/iconSeedCandy";
		statisticsAsset12.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.candy_high.hashset.Count + TopTileLibrary.candy_low.hashset.Count));
		statisticsAsset12.is_world_statistics = true;
		statisticsAsset12.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset12);
		StatisticsAsset statisticsAsset13 = new StatisticsAsset();
		statisticsAsset13.id = "world_statistics_mushroom";
		statisticsAsset13.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_mushroom"));
		statisticsAsset13.path_icon = "ui/Icons/iconSeedMushroom";
		statisticsAsset13.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.mushroom_high.hashset.Count + TopTileLibrary.mushroom_low.hashset.Count));
		statisticsAsset13.is_world_statistics = true;
		statisticsAsset13.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset13);
		StatisticsAsset statisticsAsset14 = new StatisticsAsset();
		statisticsAsset14.id = "world_statistics_wasteland";
		statisticsAsset14.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_wasteland"));
		statisticsAsset14.path_icon = "ui/Icons/achievements/achievements_wastelandbiome";
		statisticsAsset14.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.wasteland_high.hashset.Count + TopTileLibrary.wasteland_low.hashset.Count));
		statisticsAsset14.is_world_statistics = true;
		statisticsAsset14.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset14);
		StatisticsAsset statisticsAsset15 = new StatisticsAsset();
		statisticsAsset15.id = "world_statistics_birch";
		statisticsAsset15.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_birch"));
		statisticsAsset15.path_icon = "ui/Icons/iconSeedBirch";
		statisticsAsset15.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.birch_high.hashset.Count + TopTileLibrary.birch_low.hashset.Count));
		statisticsAsset15.is_world_statistics = true;
		statisticsAsset15.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset15);
		StatisticsAsset statisticsAsset16 = new StatisticsAsset();
		statisticsAsset16.id = "world_statistics_maple";
		statisticsAsset16.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_maple"));
		statisticsAsset16.path_icon = "ui/Icons/iconSeedMaple";
		statisticsAsset16.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.maple_high.hashset.Count + TopTileLibrary.maple_low.hashset.Count));
		statisticsAsset16.is_world_statistics = true;
		statisticsAsset16.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset16);
		StatisticsAsset statisticsAsset17 = new StatisticsAsset();
		statisticsAsset17.id = "world_statistics_rocklands";
		statisticsAsset17.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_rocklands"));
		statisticsAsset17.path_icon = "ui/Icons/iconSeedRocklands";
		statisticsAsset17.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.rocklands_high.hashset.Count + TopTileLibrary.rocklands_low.hashset.Count));
		statisticsAsset17.is_world_statistics = true;
		statisticsAsset17.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset17);
		StatisticsAsset statisticsAsset18 = new StatisticsAsset();
		statisticsAsset18.id = "world_statistics_garlic";
		statisticsAsset18.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_garlic"));
		statisticsAsset18.path_icon = "ui/Icons/iconSeedGarlic";
		statisticsAsset18.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.garlic_high.hashset.Count + TopTileLibrary.garlic_low.hashset.Count));
		statisticsAsset18.is_world_statistics = true;
		statisticsAsset18.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset18);
		StatisticsAsset statisticsAsset19 = new StatisticsAsset();
		statisticsAsset19.id = "world_statistics_flower";
		statisticsAsset19.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_flower"));
		statisticsAsset19.path_icon = "ui/Icons/iconSeedFlower";
		statisticsAsset19.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.flower_high.hashset.Count + TopTileLibrary.flower_low.hashset.Count));
		statisticsAsset19.is_world_statistics = true;
		statisticsAsset19.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset19);
		StatisticsAsset statisticsAsset20 = new StatisticsAsset();
		statisticsAsset20.id = "world_statistics_celestial";
		statisticsAsset20.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_celestial"));
		statisticsAsset20.path_icon = "ui/Icons/iconSeedCelestial";
		statisticsAsset20.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.celestial_high.hashset.Count + TopTileLibrary.celestial_low.hashset.Count));
		statisticsAsset20.is_world_statistics = true;
		statisticsAsset20.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset20);
		StatisticsAsset statisticsAsset21 = new StatisticsAsset();
		statisticsAsset21.id = "world_statistics_clover";
		statisticsAsset21.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_clover"));
		statisticsAsset21.path_icon = "ui/Icons/iconSeedClover";
		statisticsAsset21.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.clover_high.hashset.Count + TopTileLibrary.clover_low.hashset.Count));
		statisticsAsset21.is_world_statistics = true;
		statisticsAsset21.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset21);
		StatisticsAsset statisticsAsset22 = new StatisticsAsset();
		statisticsAsset22.id = "world_statistics_singularity";
		statisticsAsset22.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_singularity"));
		statisticsAsset22.path_icon = "ui/Icons/iconSeedSingularity";
		statisticsAsset22.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.singularity_high.hashset.Count + TopTileLibrary.singularity_low.hashset.Count));
		statisticsAsset22.is_world_statistics = true;
		statisticsAsset22.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset22);
		StatisticsAsset statisticsAsset23 = new StatisticsAsset();
		statisticsAsset23.id = "world_statistics_paradox";
		statisticsAsset23.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_paradox"));
		statisticsAsset23.path_icon = "ui/Icons/iconSeedParadox";
		statisticsAsset23.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.paradox_high.hashset.Count + TopTileLibrary.paradox_low.hashset.Count));
		statisticsAsset23.is_world_statistics = true;
		statisticsAsset23.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset23);
	}

	// Token: 0x06002ED8 RID: 11992 RVA: 0x00169C94 File Offset: 0x00167E94
	public void addSpecialBiomes()
	{
		StatisticsAsset statisticsAsset = new StatisticsAsset();
		statisticsAsset.id = "world_statistics_sand";
		statisticsAsset.locale_getter = (() => StatisticsLibrary.getPowerLocale("tile_sand"));
		statisticsAsset.path_icon = "ui/Icons/iconTileSand";
		statisticsAsset.long_action = ((StatisticsAsset _) => (long)TileLibrary.sand.hashset.Count);
		statisticsAsset.is_world_statistics = true;
		statisticsAsset.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset);
	}

	// Token: 0x06002ED9 RID: 11993 RVA: 0x00169D1C File Offset: 0x00167F1C
	public void addCreepBiomes()
	{
		StatisticsAsset statisticsAsset = new StatisticsAsset();
		statisticsAsset.id = "world_statistics_biomass";
		statisticsAsset.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_biomass"));
		statisticsAsset.path_icon = "ui/Icons/iconBiomass";
		statisticsAsset.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.biomass_high.hashset.Count + TopTileLibrary.biomass_low.hashset.Count));
		statisticsAsset.is_world_statistics = true;
		statisticsAsset.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset);
		StatisticsAsset statisticsAsset2 = new StatisticsAsset();
		statisticsAsset2.id = "world_statistics_cybertile";
		statisticsAsset2.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_cybertile"));
		statisticsAsset2.path_icon = "ui/Icons/iconCybercore";
		statisticsAsset2.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.cybertile_high.hashset.Count + TopTileLibrary.cybertile_low.hashset.Count));
		statisticsAsset2.is_world_statistics = true;
		statisticsAsset2.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset2);
		StatisticsAsset statisticsAsset3 = new StatisticsAsset();
		statisticsAsset3.id = "world_statistics_pumpkin";
		statisticsAsset3.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_pumpkin"));
		statisticsAsset3.path_icon = "ui/Icons/iconSuperPumpkin";
		statisticsAsset3.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.pumpkin_high.hashset.Count + TopTileLibrary.pumpkin_low.hashset.Count));
		statisticsAsset3.is_world_statistics = true;
		statisticsAsset3.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset3);
		StatisticsAsset statisticsAsset4 = new StatisticsAsset();
		statisticsAsset4.id = "world_statistics_tumor";
		statisticsAsset4.locale_getter = (() => StatisticsLibrary.getBiomeLocale("biome_tumor"));
		statisticsAsset4.path_icon = "ui/Icons/iconTumor";
		statisticsAsset4.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.tumor_high.hashset.Count + TopTileLibrary.tumor_low.hashset.Count));
		statisticsAsset4.is_world_statistics = true;
		statisticsAsset4.world_stats_tabs = WorldStatsTabs.Biomes;
		this.add(statisticsAsset4);
	}

	// Token: 0x06002EDA RID: 11994 RVA: 0x00169F11 File Offset: 0x00168111
	public static string getBiomeLocale(string pBiomeID)
	{
		return AssetManager.biome_library.get(pBiomeID).getLocaleID();
	}

	// Token: 0x06002EDB RID: 11995 RVA: 0x00169F23 File Offset: 0x00168123
	public static string getPowerLocale(string pPowerID)
	{
		return AssetManager.powers.get(pPowerID).getLocaleID();
	}

	// Token: 0x06002EDC RID: 11996 RVA: 0x00169F38 File Offset: 0x00168138
	public void addStatsDeaths()
	{
		StatisticsAsset statisticsAsset = new StatisticsAsset();
		statisticsAsset.id = "world_statistics_deaths_total";
		statisticsAsset.localized_key = "world_statistics_deaths_total";
		statisticsAsset.steam_activity = "#Status_stat_value";
		statisticsAsset.rarity = 3;
		statisticsAsset.list_window_meta_type = MetaType.Unit;
		statisticsAsset.path_icon = "ui/Icons/iconDead";
		statisticsAsset.long_action = ((StatisticsAsset _) => World.world.map_stats.deaths);
		statisticsAsset.is_world_statistics = true;
		statisticsAsset.world_stats_tabs = WorldStatsTabs.Deaths;
		this.add(statisticsAsset);
		StatisticsAsset statisticsAsset2 = new StatisticsAsset();
		statisticsAsset2.id = "world_statistics_deaths_natural";
		statisticsAsset2.list_window_meta_type = MetaType.Unit;
		statisticsAsset2.path_icon = "ui/Icons/iconClock";
		statisticsAsset2.long_action = ((StatisticsAsset _) => World.world.map_stats.deaths_age);
		statisticsAsset2.is_world_statistics = true;
		statisticsAsset2.world_stats_tabs = WorldStatsTabs.Deaths;
		this.add(statisticsAsset2);
		this.clone("world_statistics_deaths_hunger", "world_statistics_deaths_natural");
		this.t.long_action = ((StatisticsAsset _) => World.world.map_stats.deaths_hunger);
		this.t.path_icon = "ui/Icons/iconDeathsHunger";
		this.clone("world_statistics_deaths_eaten", "world_statistics_deaths_natural");
		this.t.long_action = ((StatisticsAsset _) => World.world.map_stats.deaths_eaten);
		this.t.path_icon = "ui/Icons/iconDeathsEaten";
		this.clone("world_statistics_deaths_plague", "world_statistics_deaths_natural");
		this.t.long_action = ((StatisticsAsset _) => World.world.map_stats.deaths_plague);
		this.t.path_icon = "ui/Icons/actor_traits/iconPlague";
		this.clone("world_statistics_deaths_poison", "world_statistics_deaths_natural");
		this.t.long_action = ((StatisticsAsset _) => World.world.map_stats.deaths_poison);
		this.t.path_icon = "ui/Icons/iconPoisoned";
		this.clone("world_statistics_deaths_infection", "world_statistics_deaths_natural");
		this.t.long_action = ((StatisticsAsset _) => World.world.map_stats.deaths_infection);
		this.t.path_icon = "ui/Icons/actor_traits/iconInfected";
		this.clone("world_statistics_deaths_tumor", "world_statistics_deaths_natural");
		this.t.long_action = ((StatisticsAsset _) => World.world.map_stats.deaths_tumor);
		this.t.path_icon = "ui/Icons/iconTumor";
		this.clone("world_statistics_deaths_acid", "world_statistics_deaths_natural");
		this.t.long_action = ((StatisticsAsset _) => World.world.map_stats.deaths_acid);
		this.t.path_icon = "ui/Icons/iconAcid";
		this.clone("world_statistics_deaths_fire", "world_statistics_deaths_natural");
		this.t.long_action = ((StatisticsAsset _) => World.world.map_stats.deaths_fire);
		this.t.path_icon = "ui/Icons/iconFire";
		this.clone("world_statistics_deaths_divine", "world_statistics_deaths_natural");
		this.t.long_action = ((StatisticsAsset _) => World.world.map_stats.deaths_divine);
		this.t.path_icon = "ui/Icons/iconDivineLight";
		this.clone("world_statistics_deaths_weapon", "world_statistics_deaths_natural");
		this.t.long_action = ((StatisticsAsset _) => World.world.map_stats.deaths_weapon);
		this.t.path_icon = "ui/Icons/actor_traits/iconBloodlust";
		this.clone("world_statistics_deaths_gravity", "world_statistics_deaths_natural");
		this.t.long_action = ((StatisticsAsset _) => World.world.map_stats.deaths_gravity);
		this.t.path_icon = "ui/Icons/worldrules/icon_grow_trees";
		this.clone("world_statistics_deaths_drowning", "world_statistics_deaths_natural");
		this.t.long_action = ((StatisticsAsset _) => World.world.map_stats.deaths_drowning);
		this.t.path_icon = "ui/Icons/iconTileDeepOcean";
		this.clone("world_statistics_deaths_water", "world_statistics_deaths_natural");
		this.t.long_action = ((StatisticsAsset _) => World.world.map_stats.deaths_water);
		this.t.path_icon = "ui/Icons/iconRain";
		this.clone("world_statistics_deaths_explosion", "world_statistics_deaths_natural");
		this.t.long_action = ((StatisticsAsset _) => World.world.map_stats.deaths_explosion);
		this.t.path_icon = "ui/Icons/worldrules/icon_exploding_mushrooms";
		this.clone("world_statistics_deaths_other", "world_statistics_deaths_natural");
		this.t.long_action = ((StatisticsAsset _) => World.world.map_stats.deaths_other);
		this.t.path_icon = "ui/Icons/iconDead";
		StatisticsAsset statisticsAsset3 = new StatisticsAsset();
		statisticsAsset3.id = "world_statistics_metamorphosis";
		statisticsAsset3.list_window_meta_type = MetaType.Unit;
		statisticsAsset3.path_icon = "ui/Icons/subspecies_traits/subspecies_trait_reproduction_metamorph";
		statisticsAsset3.long_action = ((StatisticsAsset _) => World.world.map_stats.metamorphosis);
		statisticsAsset3.is_world_statistics = true;
		statisticsAsset3.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset3);
		StatisticsAsset statisticsAsset4 = new StatisticsAsset();
		statisticsAsset4.id = "world_statistics_evolutions";
		statisticsAsset4.list_window_meta_type = MetaType.Unit;
		statisticsAsset4.path_icon = "ui/Icons/iconMonolith";
		statisticsAsset4.long_action = ((StatisticsAsset _) => World.world.map_stats.evolutions);
		statisticsAsset4.is_world_statistics = true;
		statisticsAsset4.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset4);
		StatisticsAsset statisticsAsset5 = new StatisticsAsset();
		statisticsAsset5.id = "world_statistics_houses_destroyed";
		statisticsAsset5.list_window_meta_type = MetaType.City;
		statisticsAsset5.path_icon = "ui/Icons/actor_traits/iconPyromaniac";
		statisticsAsset5.long_action = ((StatisticsAsset _) => World.world.map_stats.housesDestroyed);
		statisticsAsset5.is_world_statistics = true;
		statisticsAsset5.world_stats_tabs = (WorldStatsTabs.General | WorldStatsTabs.Deaths);
		this.add(statisticsAsset5);
		StatisticsAsset statisticsAsset6 = new StatisticsAsset();
		statisticsAsset6.id = "world_statistics_subspecies_extinct";
		statisticsAsset6.list_window_meta_type = MetaType.Subspecies;
		statisticsAsset6.path_icon = "ui/Icons/iconSpeciesExtinct";
		statisticsAsset6.long_action = ((StatisticsAsset _) => World.world.map_stats.subspeciesExtinct);
		statisticsAsset6.is_world_statistics = true;
		statisticsAsset6.world_stats_tabs = (WorldStatsTabs.Noosphere | WorldStatsTabs.Deaths);
		this.add(statisticsAsset6);
		StatisticsAsset statisticsAsset7 = new StatisticsAsset();
		statisticsAsset7.id = "world_statistics_kingdoms_destroyed";
		statisticsAsset7.list_window_meta_type = MetaType.Kingdom;
		statisticsAsset7.path_icon = "ui/Icons/iconKingdomDestroyed";
		statisticsAsset7.long_action = ((StatisticsAsset _) => World.world.map_stats.kingdomsDestroyed);
		statisticsAsset7.is_world_statistics = true;
		statisticsAsset7.world_stats_tabs = (WorldStatsTabs.Noosphere | WorldStatsTabs.Deaths);
		this.add(statisticsAsset7);
		StatisticsAsset statisticsAsset8 = new StatisticsAsset();
		statisticsAsset8.id = "world_statistics_armies_destroyed";
		statisticsAsset8.list_window_meta_type = MetaType.Army;
		statisticsAsset8.path_icon = "ui/Icons/iconArmiesDestroyed";
		statisticsAsset8.long_action = ((StatisticsAsset _) => World.world.map_stats.armiesDestroyed);
		statisticsAsset8.is_world_statistics = true;
		statisticsAsset8.world_stats_tabs = (WorldStatsTabs.Noosphere | WorldStatsTabs.Deaths);
		this.add(statisticsAsset8);
		StatisticsAsset statisticsAsset9 = new StatisticsAsset();
		statisticsAsset9.id = "world_statistics_cities_destroyed";
		statisticsAsset9.list_window_meta_type = MetaType.City;
		statisticsAsset9.path_icon = "ui/Icons/iconCityDestroyed";
		statisticsAsset9.long_action = ((StatisticsAsset _) => World.world.map_stats.citiesDestroyed);
		statisticsAsset9.is_world_statistics = true;
		statisticsAsset9.world_stats_tabs = (WorldStatsTabs.Noosphere | WorldStatsTabs.Deaths);
		this.add(statisticsAsset9);
	}

	// Token: 0x06002EDD RID: 11997 RVA: 0x0016A708 File Offset: 0x00168908
	public void addStatsNoos()
	{
		StatisticsAsset statisticsAsset = new StatisticsAsset();
		statisticsAsset.id = "world_statistics_languages_created";
		statisticsAsset.localized_key_description = "statistics_languages".Description();
		statisticsAsset.list_window_meta_type = MetaType.Language;
		statisticsAsset.path_icon = "ui/Icons/iconLanguage";
		statisticsAsset.long_action = ((StatisticsAsset _) => World.world.map_stats.languagesCreated);
		statisticsAsset.is_world_statistics = true;
		statisticsAsset.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset);
		StatisticsAsset statisticsAsset2 = new StatisticsAsset();
		statisticsAsset2.id = "world_statistics_languages_forgotten";
		statisticsAsset2.list_window_meta_type = MetaType.Language;
		statisticsAsset2.path_icon = "ui/Icons/iconLanguageForgotten";
		statisticsAsset2.long_action = ((StatisticsAsset _) => World.world.map_stats.languagesForgotten);
		statisticsAsset2.is_world_statistics = true;
		statisticsAsset2.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset2);
		StatisticsAsset statisticsAsset3 = new StatisticsAsset();
		statisticsAsset3.id = "world_statistics_families_created";
		statisticsAsset3.list_window_meta_type = MetaType.Family;
		statisticsAsset3.path_icon = "ui/Icons/iconNewFamily";
		statisticsAsset3.long_action = ((StatisticsAsset _) => World.world.map_stats.familiesCreated);
		statisticsAsset3.is_world_statistics = true;
		statisticsAsset3.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset3);
		StatisticsAsset statisticsAsset4 = new StatisticsAsset();
		statisticsAsset4.id = "world_statistics_families_destroyed";
		statisticsAsset4.list_window_meta_type = MetaType.Family;
		statisticsAsset4.path_icon = "ui/Icons/iconFamilyDestroyed";
		statisticsAsset4.long_action = ((StatisticsAsset _) => World.world.map_stats.familiesDestroyed);
		statisticsAsset4.is_world_statistics = true;
		statisticsAsset4.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset4);
		StatisticsAsset statisticsAsset5 = new StatisticsAsset();
		statisticsAsset5.id = "world_statistics_clans_created";
		statisticsAsset5.localized_key_description = "statistics_clans".Description();
		statisticsAsset5.list_window_meta_type = MetaType.Clan;
		statisticsAsset5.path_icon = "ui/Icons/iconClan";
		statisticsAsset5.long_action = ((StatisticsAsset _) => World.world.map_stats.clansCreated);
		statisticsAsset5.is_world_statistics = true;
		statisticsAsset5.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset5);
		StatisticsAsset statisticsAsset6 = new StatisticsAsset();
		statisticsAsset6.id = "world_statistics_clans_destroyed";
		statisticsAsset6.list_window_meta_type = MetaType.Clan;
		statisticsAsset6.path_icon = "ui/Icons/iconClanDestroyed";
		statisticsAsset6.long_action = ((StatisticsAsset _) => World.world.map_stats.clansDestroyed);
		statisticsAsset6.is_world_statistics = true;
		statisticsAsset6.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset6);
		StatisticsAsset statisticsAsset7 = new StatisticsAsset();
		statisticsAsset7.id = "world_statistics_cultures_created";
		statisticsAsset7.localized_key_description = "statistics_cultures".Description();
		statisticsAsset7.list_window_meta_type = MetaType.Culture;
		statisticsAsset7.path_icon = "ui/Icons/iconCulture";
		statisticsAsset7.long_action = ((StatisticsAsset _) => World.world.map_stats.culturesCreated);
		statisticsAsset7.is_world_statistics = true;
		statisticsAsset7.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset7);
		StatisticsAsset statisticsAsset8 = new StatisticsAsset();
		statisticsAsset8.id = "world_statistics_cultures_forgotten";
		statisticsAsset8.list_window_meta_type = MetaType.Culture;
		statisticsAsset8.path_icon = "ui/Icons/iconCultureForgotten";
		statisticsAsset8.long_action = ((StatisticsAsset _) => World.world.map_stats.culturesForgotten);
		statisticsAsset8.is_world_statistics = true;
		statisticsAsset8.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset8);
		StatisticsAsset statisticsAsset9 = new StatisticsAsset();
		statisticsAsset9.id = "world_statistics_books_written";
		statisticsAsset9.localized_key_description = "books".Description();
		statisticsAsset9.list_window_meta_type = MetaType.Language;
		statisticsAsset9.path_icon = "ui/Icons/iconBooksWritten";
		statisticsAsset9.long_action = ((StatisticsAsset _) => World.world.map_stats.booksWritten);
		statisticsAsset9.is_world_statistics = true;
		statisticsAsset9.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset9);
		StatisticsAsset statisticsAsset10 = new StatisticsAsset();
		statisticsAsset10.id = "world_statistics_books_read";
		statisticsAsset10.list_window_meta_type = MetaType.Language;
		statisticsAsset10.path_icon = "ui/Icons/iconBooksRead";
		statisticsAsset10.long_action = ((StatisticsAsset _) => World.world.map_stats.booksRead);
		statisticsAsset10.is_world_statistics = true;
		statisticsAsset10.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset10);
		StatisticsAsset statisticsAsset11 = new StatisticsAsset();
		statisticsAsset11.id = "world_statistics_books_burnt";
		statisticsAsset11.list_window_meta_type = MetaType.Language;
		statisticsAsset11.path_icon = "ui/Icons/iconBooksDestroyed";
		statisticsAsset11.long_action = ((StatisticsAsset _) => World.world.map_stats.booksBurnt);
		statisticsAsset11.is_world_statistics = true;
		statisticsAsset11.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset11);
		StatisticsAsset statisticsAsset12 = new StatisticsAsset();
		statisticsAsset12.id = "world_statistics_religions_created";
		statisticsAsset12.localized_key_description = "statistics_religions".Description();
		statisticsAsset12.list_window_meta_type = MetaType.Religion;
		statisticsAsset12.path_icon = "ui/Icons/iconReligion";
		statisticsAsset12.long_action = ((StatisticsAsset _) => World.world.map_stats.religionsCreated);
		statisticsAsset12.is_world_statistics = true;
		statisticsAsset12.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset12);
		StatisticsAsset statisticsAsset13 = new StatisticsAsset();
		statisticsAsset13.id = "world_statistics_religions_forgotten";
		statisticsAsset13.list_window_meta_type = MetaType.Religion;
		statisticsAsset13.path_icon = "ui/Icons/iconReligionForgotten";
		statisticsAsset13.long_action = ((StatisticsAsset _) => World.world.map_stats.religionsForgotten);
		statisticsAsset13.is_world_statistics = true;
		statisticsAsset13.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset13);
		StatisticsAsset statisticsAsset14 = new StatisticsAsset();
		statisticsAsset14.id = "world_statistics_kingdoms_created";
		statisticsAsset14.list_window_meta_type = MetaType.Kingdom;
		statisticsAsset14.path_icon = "ui/Icons/iconKingdom";
		statisticsAsset14.long_action = ((StatisticsAsset _) => World.world.map_stats.kingdomsCreated);
		statisticsAsset14.is_world_statistics = true;
		statisticsAsset14.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset14);
		StatisticsAsset statisticsAsset15 = new StatisticsAsset();
		statisticsAsset15.id = "world_statistics_cities_created";
		statisticsAsset15.localized_key_description = "statistics_villages".Description();
		statisticsAsset15.list_window_meta_type = MetaType.City;
		statisticsAsset15.path_icon = "ui/Icons/iconCity";
		statisticsAsset15.long_action = ((StatisticsAsset _) => World.world.map_stats.citiesCreated);
		statisticsAsset15.is_world_statistics = true;
		statisticsAsset15.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset15);
		StatisticsAsset statisticsAsset16 = new StatisticsAsset();
		statisticsAsset16.id = "world_statistics_cities_conquered";
		statisticsAsset16.list_window_meta_type = MetaType.City;
		statisticsAsset16.path_icon = "ui/Icons/iconCityConquered";
		statisticsAsset16.long_action = ((StatisticsAsset _) => World.world.map_stats.citiesConquered);
		statisticsAsset16.is_world_statistics = true;
		statisticsAsset16.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset16);
		StatisticsAsset statisticsAsset17 = new StatisticsAsset();
		statisticsAsset17.id = "statistics_cities_conquered";
		statisticsAsset17.path_icon = "ui/Icons/iconCityConquered";
		statisticsAsset17.long_action = ((StatisticsAsset _) => World.world.game_stats.data.citiesConquered);
		statisticsAsset17.is_game_statistics = true;
		statisticsAsset17.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset17);
		StatisticsAsset statisticsAsset18 = new StatisticsAsset();
		statisticsAsset18.id = "world_statistics_cities_rebelled";
		statisticsAsset18.list_window_meta_type = MetaType.City;
		statisticsAsset18.path_icon = "ui/Icons/worldrules/icon_rebellion";
		statisticsAsset18.long_action = ((StatisticsAsset _) => World.world.map_stats.citiesRebelled);
		statisticsAsset18.is_world_statistics = true;
		statisticsAsset18.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset18);
		StatisticsAsset statisticsAsset19 = new StatisticsAsset();
		statisticsAsset19.id = "statistics_cities_rebelled";
		statisticsAsset19.path_icon = "ui/Icons/worldrules/icon_rebellion";
		statisticsAsset19.long_action = ((StatisticsAsset _) => World.world.game_stats.data.citiesRebelled);
		statisticsAsset19.is_game_statistics = true;
		statisticsAsset19.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset19);
		StatisticsAsset statisticsAsset20 = new StatisticsAsset();
		statisticsAsset20.id = "world_statistics_alliances_made";
		statisticsAsset20.localized_key_description = "statistics_alliances".Description();
		statisticsAsset20.list_window_meta_type = MetaType.Alliance;
		statisticsAsset20.path_icon = "ui/Icons/iconAlliance";
		statisticsAsset20.long_action = ((StatisticsAsset _) => World.world.map_stats.alliancesMade);
		statisticsAsset20.is_world_statistics = true;
		statisticsAsset20.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset20);
		StatisticsAsset statisticsAsset21 = new StatisticsAsset();
		statisticsAsset21.id = "world_statistics_alliances_dissolved";
		statisticsAsset21.list_window_meta_type = MetaType.Alliance;
		statisticsAsset21.path_icon = "ui/Icons/iconAllianceDissolved";
		statisticsAsset21.long_action = ((StatisticsAsset _) => World.world.map_stats.alliancesDissolved);
		statisticsAsset21.is_world_statistics = true;
		statisticsAsset21.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset21);
		StatisticsAsset statisticsAsset22 = new StatisticsAsset();
		statisticsAsset22.id = "statistics_alliances_made";
		statisticsAsset22.path_icon = "ui/Icons/iconAlliance";
		statisticsAsset22.long_action = ((StatisticsAsset _) => World.world.game_stats.data.alliancesMade);
		statisticsAsset22.is_game_statistics = true;
		statisticsAsset22.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset22);
		StatisticsAsset statisticsAsset23 = new StatisticsAsset();
		statisticsAsset23.id = "statistics_alliances_dissolved";
		statisticsAsset23.path_icon = "ui/Icons/iconAllianceDissolved";
		statisticsAsset23.long_action = ((StatisticsAsset _) => World.world.game_stats.data.alliancesDissolved);
		statisticsAsset23.is_game_statistics = true;
		statisticsAsset23.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset23);
		StatisticsAsset statisticsAsset24 = new StatisticsAsset();
		statisticsAsset24.id = "world_statistics_wars_started";
		statisticsAsset24.localized_key_description = "statistics_wars".Description();
		statisticsAsset24.list_window_meta_type = MetaType.War;
		statisticsAsset24.path_icon = "ui/Icons/iconWhisperOfWar";
		statisticsAsset24.long_action = ((StatisticsAsset _) => World.world.map_stats.warsStarted);
		statisticsAsset24.is_world_statistics = true;
		statisticsAsset24.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset24);
		StatisticsAsset statisticsAsset25 = new StatisticsAsset();
		statisticsAsset25.id = "world_statistics_peaces_made";
		statisticsAsset25.list_window_meta_type = MetaType.War;
		statisticsAsset25.path_icon = "ui/Icons/actor_traits/iconPacifist";
		statisticsAsset25.long_action = ((StatisticsAsset _) => World.world.map_stats.peacesMade);
		statisticsAsset25.is_world_statistics = true;
		statisticsAsset25.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset25);
		StatisticsAsset statisticsAsset26 = new StatisticsAsset();
		statisticsAsset26.id = "world_statistics_plots_started";
		statisticsAsset26.localized_key_description = "statistics_plots".Description();
		statisticsAsset26.list_window_meta_type = MetaType.Plot;
		statisticsAsset26.path_icon = "ui/Icons/iconPlot";
		statisticsAsset26.long_action = ((StatisticsAsset _) => World.world.map_stats.plotsStarted);
		statisticsAsset26.is_world_statistics = true;
		statisticsAsset26.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset26);
		StatisticsAsset statisticsAsset27 = new StatisticsAsset();
		statisticsAsset27.id = "world_statistics_plots_succeeded";
		statisticsAsset27.list_window_meta_type = MetaType.Plot;
		statisticsAsset27.path_icon = "ui/Icons/iconPlotSucceeded";
		statisticsAsset27.long_action = ((StatisticsAsset _) => World.world.map_stats.plotsSucceeded);
		statisticsAsset27.is_world_statistics = true;
		statisticsAsset27.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset27);
		StatisticsAsset statisticsAsset28 = new StatisticsAsset();
		statisticsAsset28.id = "world_statistics_plots_forgotten";
		statisticsAsset28.list_window_meta_type = MetaType.Plot;
		statisticsAsset28.path_icon = "ui/Icons/iconPlotForgotten";
		statisticsAsset28.long_action = ((StatisticsAsset _) => World.world.map_stats.plotsForgotten);
		statisticsAsset28.is_world_statistics = true;
		statisticsAsset28.world_stats_tabs = WorldStatsTabs.Noosphere;
		this.add(statisticsAsset28);
		this.add(new StatisticsAsset
		{
			id = "world_statistics_most_populated_village",
			path_icon = "ui/Icons/iconCity",
			is_world_statistics = true,
			world_stats_tabs = WorldStatsTabs.Noosphere,
			world_stats_meta_type = MetaType.City,
			list_window_meta_type = MetaType.City,
			string_action = ((StatisticsAsset _) => this.getDominatingMetaRow(MetaType.City)),
			get_meta_id = ((StatisticsAsset _) => this.getDominatingMetaId(MetaType.City))
		});
		StatisticsAsset statisticsAsset29 = new StatisticsAsset();
		statisticsAsset29.id = "world_statistics_biggest_village";
		statisticsAsset29.path_icon = "ui/Icons/iconCity";
		statisticsAsset29.is_world_statistics = true;
		statisticsAsset29.world_stats_tabs = WorldStatsTabs.Noosphere;
		statisticsAsset29.world_stats_meta_type = MetaType.City;
		statisticsAsset29.list_window_meta_type = MetaType.City;
		statisticsAsset29.string_action = delegate(StatisticsAsset pAsset)
		{
			long tId = pAsset.get_meta_id(pAsset);
			City tBiggest = World.world.cities.get(tId);
			if (tBiggest == null)
			{
				return StatisticsLibrary._unknown_text;
			}
			string tColor = tBiggest.getColor().color_text;
			return Toolbox.coloredText(tBiggest.name + Toolbox.coloredGreyPart(tBiggest.zones.Count, tColor, false), tColor, false);
		};
		statisticsAsset29.get_meta_id = delegate(StatisticsAsset _)
		{
			City tBiggest = null;
			foreach (City tCity in World.world.cities)
			{
				if (tBiggest == null || tCity.zones.Count > tBiggest.zones.Count)
				{
					tBiggest = tCity;
				}
			}
			if (tBiggest == null)
			{
				return -1L;
			}
			return tBiggest.id;
		};
		this.add(statisticsAsset29);
		this.add(new StatisticsAsset
		{
			id = "world_statistics_most_populated_kingdom",
			path_icon = "ui/Icons/iconKingdom",
			is_world_statistics = true,
			world_stats_tabs = WorldStatsTabs.Noosphere,
			world_stats_meta_type = MetaType.Kingdom,
			list_window_meta_type = MetaType.Kingdom,
			string_action = ((StatisticsAsset _) => this.getDominatingMetaRow(MetaType.Kingdom)),
			get_meta_id = ((StatisticsAsset _) => this.getDominatingMetaId(MetaType.Kingdom))
		});
		this.add(new StatisticsAsset
		{
			id = "world_statistics_dominating_culture",
			path_icon = "ui/Icons/iconCulture",
			is_world_statistics = true,
			world_stats_tabs = WorldStatsTabs.Noosphere,
			world_stats_meta_type = MetaType.Culture,
			list_window_meta_type = MetaType.Culture,
			string_action = ((StatisticsAsset _) => this.getDominatingMetaRow(MetaType.Culture)),
			get_meta_id = ((StatisticsAsset _) => this.getDominatingMetaId(MetaType.Culture))
		});
		this.add(new StatisticsAsset
		{
			id = "world_statistics_dominating_language",
			path_icon = "ui/Icons/iconLanguage",
			is_world_statistics = true,
			world_stats_tabs = WorldStatsTabs.Noosphere,
			world_stats_meta_type = MetaType.Language,
			list_window_meta_type = MetaType.Language,
			string_action = ((StatisticsAsset _) => this.getDominatingMetaRow(MetaType.Language)),
			get_meta_id = ((StatisticsAsset _) => this.getDominatingMetaId(MetaType.Language))
		});
		this.add(new StatisticsAsset
		{
			id = "world_statistics_dominating_religion",
			path_icon = "ui/Icons/iconReligion",
			is_world_statistics = true,
			world_stats_tabs = WorldStatsTabs.Noosphere,
			world_stats_meta_type = MetaType.Religion,
			list_window_meta_type = MetaType.Religion,
			string_action = ((StatisticsAsset _) => this.getDominatingMetaRow(MetaType.Religion)),
			get_meta_id = ((StatisticsAsset _) => this.getDominatingMetaId(MetaType.Religion))
		});
		this.add(new StatisticsAsset
		{
			id = "world_statistics_dominating_subspecies",
			path_icon = "ui/Icons/iconSpecies",
			is_world_statistics = true,
			world_stats_tabs = WorldStatsTabs.Noosphere,
			world_stats_meta_type = MetaType.Subspecies,
			list_window_meta_type = MetaType.Subspecies,
			string_action = ((StatisticsAsset _) => this.getDominatingMetaRow(MetaType.Subspecies)),
			get_meta_id = ((StatisticsAsset _) => this.getDominatingMetaId(MetaType.Subspecies))
		});
		this.add(new StatisticsAsset
		{
			id = "world_statistics_oldest_clan",
			path_icon = "ui/Icons/iconClan",
			is_world_statistics = true,
			world_stats_tabs = WorldStatsTabs.Noosphere,
			world_stats_meta_type = MetaType.Clan,
			list_window_meta_type = MetaType.Clan,
			string_action = ((StatisticsAsset _) => this.getOldestMetaRow(MetaType.Clan)),
			get_meta_id = ((StatisticsAsset _) => this.getOldestMetaId(MetaType.Clan))
		});
	}

	// Token: 0x06002EDE RID: 11998 RVA: 0x0016B4D8 File Offset: 0x001696D8
	public void addStatsTiles()
	{
		StatisticsAsset statisticsAsset = new StatisticsAsset();
		statisticsAsset.id = "world_statistics_water";
		statisticsAsset.localized_key = "Water";
		statisticsAsset.path_icon = "ui/Icons/iconTileDeepOcean";
		statisticsAsset.long_action = ((StatisticsAsset _) => (long)(TileLibrary.deep_ocean.hashset.Count + TileLibrary.close_ocean.hashset.Count + TileLibrary.shallow_waters.hashset.Count));
		statisticsAsset.is_world_statistics = true;
		statisticsAsset.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset);
		StatisticsAsset statisticsAsset2 = new StatisticsAsset();
		statisticsAsset2.id = "world_statistics_soil";
		statisticsAsset2.locale_getter = (() => StatisticsLibrary.getPowerLocale("tile_soil"));
		statisticsAsset2.path_icon = "ui/Icons/iconTileSoil";
		statisticsAsset2.long_action = ((StatisticsAsset _) => (long)(TileLibrary.soil_low.hashset.Count + TileLibrary.soil_high.hashset.Count));
		statisticsAsset2.is_world_statistics = true;
		statisticsAsset2.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset2);
		StatisticsAsset statisticsAsset3 = new StatisticsAsset();
		statisticsAsset3.id = "world_statistics_summit";
		statisticsAsset3.locale_getter = (() => StatisticsLibrary.getPowerLocale("tile_summit"));
		statisticsAsset3.path_icon = "ui/Icons/iconTileSummit";
		statisticsAsset3.long_action = ((StatisticsAsset _) => (long)TileLibrary.summit.hashset.Count);
		statisticsAsset3.is_world_statistics = true;
		statisticsAsset3.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset3);
		StatisticsAsset statisticsAsset4 = new StatisticsAsset();
		statisticsAsset4.id = "world_statistics_mountains";
		statisticsAsset4.locale_getter = (() => StatisticsLibrary.getPowerLocale("tile_mountains"));
		statisticsAsset4.path_icon = "ui/Icons/iconTileMountains";
		statisticsAsset4.long_action = ((StatisticsAsset _) => (long)TileLibrary.mountains.hashset.Count);
		statisticsAsset4.is_world_statistics = true;
		statisticsAsset4.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset4);
		StatisticsAsset statisticsAsset5 = new StatisticsAsset();
		statisticsAsset5.id = "world_statistics_hills";
		statisticsAsset5.locale_getter = (() => StatisticsLibrary.getPowerLocale("tile_hills"));
		statisticsAsset5.path_icon = "ui/Icons/iconTileHills";
		statisticsAsset5.long_action = ((StatisticsAsset _) => (long)TileLibrary.hills.hashset.Count);
		statisticsAsset5.is_world_statistics = true;
		statisticsAsset5.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset5);
		StatisticsAsset statisticsAsset6 = new StatisticsAsset();
		statisticsAsset6.id = "world_statistics_lava";
		statisticsAsset6.locale_getter = (() => StatisticsLibrary.getPowerLocale("lava"));
		statisticsAsset6.path_icon = "ui/Icons/iconLava";
		statisticsAsset6.long_action = ((StatisticsAsset _) => (long)(TileLibrary.lava0.hashset.Count + TileLibrary.lava1.hashset.Count + TileLibrary.lava2.hashset.Count + TileLibrary.lava3.hashset.Count));
		statisticsAsset6.is_world_statistics = true;
		statisticsAsset6.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset6);
		StatisticsAsset statisticsAsset7 = new StatisticsAsset();
		statisticsAsset7.id = "world_statistics_pit";
		statisticsAsset7.localized_key = "Pit";
		statisticsAsset7.path_icon = "ui/Icons/iconTileShallowWater";
		statisticsAsset7.long_action = ((StatisticsAsset _) => (long)(TileLibrary.pit_deep_ocean.hashset.Count + TileLibrary.pit_close_ocean.hashset.Count + TileLibrary.pit_shallow_waters.hashset.Count));
		statisticsAsset7.is_world_statistics = true;
		statisticsAsset7.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset7);
		StatisticsAsset statisticsAsset8 = new StatisticsAsset();
		statisticsAsset8.id = "world_statistics_field";
		statisticsAsset8.localized_key = "fields";
		statisticsAsset8.path_icon = "ui/Icons/citizen_jobs/iconCitizenJobFarmer";
		statisticsAsset8.long_action = ((StatisticsAsset _) => (long)TopTileLibrary.field.hashset.Count);
		statisticsAsset8.is_world_statistics = true;
		statisticsAsset8.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset8);
		StatisticsAsset statisticsAsset9 = new StatisticsAsset();
		statisticsAsset9.id = "world_statistics_fireworks";
		statisticsAsset9.locale_getter = (() => StatisticsLibrary.getPowerLocale("fireworks"));
		statisticsAsset9.path_icon = "ui/Icons/iconFireworks";
		statisticsAsset9.long_action = ((StatisticsAsset _) => (long)TopTileLibrary.fireworks.hashset.Count);
		statisticsAsset9.is_world_statistics = true;
		statisticsAsset9.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset9);
		StatisticsAsset statisticsAsset10 = new StatisticsAsset();
		statisticsAsset10.id = "world_statistics_frozen";
		statisticsAsset10.localized_key = "Frozen";
		statisticsAsset10.path_icon = "ui/Icons/iconFrozen";
		statisticsAsset10.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.frozen_high.hashset.Count + TopTileLibrary.frozen_low.hashset.Count));
		statisticsAsset10.is_world_statistics = true;
		statisticsAsset10.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset10);
		StatisticsAsset statisticsAsset11 = new StatisticsAsset();
		statisticsAsset11.id = "world_statistics_fuse";
		statisticsAsset11.locale_getter = (() => StatisticsLibrary.getPowerLocale("fuse"));
		statisticsAsset11.path_icon = "ui/Icons/iconFuse";
		statisticsAsset11.long_action = ((StatisticsAsset _) => (long)TopTileLibrary.fuse.hashset.Count);
		statisticsAsset11.is_world_statistics = true;
		statisticsAsset11.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset11);
		StatisticsAsset statisticsAsset12 = new StatisticsAsset();
		statisticsAsset12.id = "world_statistics_ice";
		statisticsAsset12.localized_key = "Ice";
		statisticsAsset12.path_icon = "ui/Icons/iconIceberg";
		statisticsAsset12.long_action = ((StatisticsAsset _) => (long)TopTileLibrary.ice.hashset.Count);
		statisticsAsset12.is_world_statistics = true;
		statisticsAsset12.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset12);
		StatisticsAsset statisticsAsset13 = new StatisticsAsset();
		statisticsAsset13.id = "world_statistics_landmine";
		statisticsAsset13.locale_getter = (() => StatisticsLibrary.getPowerLocale("landmine"));
		statisticsAsset13.path_icon = "ui/Icons/iconLandmine";
		statisticsAsset13.long_action = ((StatisticsAsset _) => (long)TopTileLibrary.landmine.hashset.Count);
		statisticsAsset13.is_world_statistics = true;
		statisticsAsset13.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset13);
		StatisticsAsset statisticsAsset14 = new StatisticsAsset();
		statisticsAsset14.id = "world_statistics_road";
		statisticsAsset14.localized_key = "Roads";
		statisticsAsset14.path_icon = "ui/Icons/citizen_jobs/iconCitizenJobRoadBuilder";
		statisticsAsset14.long_action = ((StatisticsAsset _) => (long)TopTileLibrary.road.hashset.Count);
		statisticsAsset14.is_world_statistics = true;
		statisticsAsset14.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset14);
		StatisticsAsset statisticsAsset15 = new StatisticsAsset();
		statisticsAsset15.id = "world_statistics_snow";
		statisticsAsset15.localized_key = "Snow";
		statisticsAsset15.path_icon = "ui/Icons/iconSnow";
		statisticsAsset15.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.snow_hills.hashset.Count + TopTileLibrary.snow_block.hashset.Count + TopTileLibrary.snow_summit.hashset.Count + TopTileLibrary.snow_sand.hashset.Count));
		statisticsAsset15.is_world_statistics = true;
		statisticsAsset15.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset15);
		StatisticsAsset statisticsAsset16 = new StatisticsAsset();
		statisticsAsset16.id = "world_statistics_tnt";
		statisticsAsset16.locale_getter = (() => StatisticsLibrary.getPowerLocale("tnt"));
		statisticsAsset16.path_icon = "ui/Icons/iconTnt";
		statisticsAsset16.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.tnt.hashset.Count + TopTileLibrary.tnt_timed.hashset.Count));
		statisticsAsset16.is_world_statistics = true;
		statisticsAsset16.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset16);
		StatisticsAsset statisticsAsset17 = new StatisticsAsset();
		statisticsAsset17.id = "world_statistics_wall";
		statisticsAsset17.localized_key = "Walls";
		statisticsAsset17.path_icon = "ui/Icons/iconWallIron";
		statisticsAsset17.long_action = ((StatisticsAsset _) => (long)(TopTileLibrary.wall_evil.hashset.Count + TopTileLibrary.wall_order.hashset.Count + TopTileLibrary.wall_ancient.hashset.Count + TopTileLibrary.wall_wild.hashset.Count + TopTileLibrary.wall_green.hashset.Count + TopTileLibrary.wall_iron.hashset.Count + TopTileLibrary.wall_light.hashset.Count));
		statisticsAsset17.is_world_statistics = true;
		statisticsAsset17.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset17);
		StatisticsAsset statisticsAsset18 = new StatisticsAsset();
		statisticsAsset18.id = "world_statistics_water_bomb";
		statisticsAsset18.locale_getter = (() => StatisticsLibrary.getPowerLocale("water_bomb"));
		statisticsAsset18.path_icon = "ui/Icons/iconWaterBomb";
		statisticsAsset18.long_action = ((StatisticsAsset _) => (long)TopTileLibrary.water_bomb.hashset.Count);
		statisticsAsset18.is_world_statistics = true;
		statisticsAsset18.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset18);
		StatisticsAsset statisticsAsset19 = new StatisticsAsset();
		statisticsAsset19.id = "world_statistics_grey_goo";
		statisticsAsset19.locale_getter = (() => StatisticsLibrary.getPowerLocale("grey_goo"));
		statisticsAsset19.path_icon = "ui/Icons/iconGreygoo";
		statisticsAsset19.long_action = ((StatisticsAsset _) => (long)TileLibrary.grey_goo.hashset.Count);
		statisticsAsset19.is_world_statistics = true;
		statisticsAsset19.world_stats_tabs = WorldStatsTabs.General;
		this.add(statisticsAsset19);
	}

	// Token: 0x040022FA RID: 8954
	internal static readonly List<StatisticsAsset> power_tracker_pool = new List<StatisticsAsset>();

	// Token: 0x040022FB RID: 8955
	private static readonly string _unknown_text = Toolbox.coloredString("???", ColorStyleLibrary.m.color_dead_text);
}
