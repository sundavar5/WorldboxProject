using System;
using UnityEngine;

// Token: 0x02000570 RID: 1392
public class HistoryDataLibrary : AssetLibrary<HistoryDataAsset>
{
	// Token: 0x06002D6A RID: 11626 RVA: 0x0016195F File Offset: 0x0015FB5F
	public override void init()
	{
		base.init();
		this.addStats();
		this.addBiomes();
		this.addTiles();
	}

	// Token: 0x06002D6B RID: 11627 RVA: 0x0016197C File Offset: 0x0015FB7C
	public void addStats()
	{
		this.add(new HistoryDataAsset
		{
			id = "population",
			color_hex = "#6295B3",
			path_icon = "ui/Icons/iconPopulation",
			enabled_default = true
		});
		this.add(new HistoryDataAsset
		{
			id = "population_civ",
			statistics_asset = "world_statistics_population",
			color_hex = "#65A23F",
			path_icon = "ui/Icons/iconPopulationCiv"
		});
		this.add(new HistoryDataAsset
		{
			id = "population_beasts",
			statistics_asset = "world_statistics_beasts",
			color_hex = "#664C28",
			path_icon = "ui/Icons/worldrules/icon_animalspawn"
		});
		this.add(new HistoryDataAsset
		{
			id = "creatures_born",
			statistics_asset = "world_statistics_creatures_born",
			color_hex = "#DA3A57",
			path_icon = "ui/Icons/iconBirths",
			max = true
		});
		this.t.category_group |= GraphCategoryGroup.Deaths;
		this.add(new HistoryDataAsset
		{
			id = "creatures_created",
			statistics_asset = "world_statistics_creatures_created",
			color_hex = "#9670B5",
			path_icon = "ui/Icons/actor_traits/iconMiracleBorn",
			max = true
		});
		this.t.category_group |= GraphCategoryGroup.Deaths;
		this.add(new HistoryDataAsset
		{
			id = "food",
			color_hex = "#C67D49",
			path_icon = "ui/Icons/iconResBread",
			enabled_default = true
		});
		this.add(new HistoryDataAsset
		{
			id = "hungry",
			color_hex = "#7FA9BC",
			path_icon = "ui/Icons/iconHungry"
		});
		this.add(new HistoryDataAsset
		{
			id = "starving",
			color_hex = "#BAC6B2",
			path_icon = "ui/Icons/iconStarving",
			enabled_default = true
		});
		this.add(new HistoryDataAsset
		{
			id = "items",
			color_hex = "#FFB22D",
			path_icon = "ui/Icons/iconReforge"
		});
		this.add(new HistoryDataAsset
		{
			id = "homeless",
			color_hex = "#AD3640",
			path_icon = "ui/Icons/iconHomeless"
		});
		this.add(new HistoryDataAsset
		{
			id = "housed",
			color_hex = "#358A23",
			path_icon = "ui/Icons/iconHoused"
		});
		this.add(new HistoryDataAsset
		{
			id = "houses",
			statistics_asset = "world_statistics_houses",
			color_hex = "#B59780",
			path_icon = "ui/Icons/iconBuildings",
			max = true
		});
		this.add(new HistoryDataAsset
		{
			id = "houses_built",
			statistics_asset = "world_statistics_houses_built",
			color_hex = "#8D9995",
			path_icon = "ui/Icons/citizen_jobs/iconCitizenJobBuilder",
			max = true
		});
		this.add(new HistoryDataAsset
		{
			id = "houses_destroyed",
			statistics_asset = "world_statistics_houses_destroyed",
			color_hex = "#DA3722",
			path_icon = "ui/Icons/actor_traits/iconPyromaniac",
			max = true,
			category_group = GraphCategoryGroup.Deaths
		});
		this.add(new HistoryDataAsset
		{
			id = "cities",
			statistics_asset = "villages",
			color_hex = "#9B785E",
			path_icon = "ui/Icons/iconCitySelect",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "cities_destroyed",
			statistics_asset = "world_statistics_cities_destroyed",
			color_hex = "#AD363F",
			path_icon = "ui/Icons/iconCityDestroyed",
			max = true,
			category_group = GraphCategoryGroup.Deaths
		});
		this.add(new HistoryDataAsset
		{
			id = "cities_created",
			statistics_asset = "world_statistics_cities_created",
			color_hex = "#739297",
			path_icon = "ui/Icons/iconCity",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "cities_conquered",
			statistics_asset = "world_statistics_cities_conquered",
			color_hex = "#A1B1A2",
			path_icon = "ui/Icons/iconCityConquered",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "cities_rebelled",
			statistics_asset = "world_statistics_cities_rebelled",
			color_hex = "#FE7332",
			path_icon = "ui/Icons/worldrules/icon_rebellion",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "kingdoms",
			statistics_asset = "kingdoms",
			color_hex = "#FF8500",
			path_icon = "ui/Icons/iconKingdomList",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "kingdoms_created",
			statistics_asset = "world_statistics_kingdoms_created",
			color_hex = "#FFE563",
			path_icon = "ui/Icons/iconKingdom",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "kingdoms_destroyed",
			statistics_asset = "world_statistics_kingdoms_destroyed",
			color_hex = "#AD363F",
			path_icon = "ui/Icons/iconKingdomDestroyed",
			max = true,
			category_group = GraphCategoryGroup.Deaths
		});
		this.add(new HistoryDataAsset
		{
			id = "armies_destroyed",
			statistics_asset = "world_statistics_armies_destroyed",
			color_hex = "#AD363F",
			path_icon = "ui/Icons/iconArmiesDestroyed",
			max = true,
			category_group = GraphCategoryGroup.Deaths
		});
		this.add(new HistoryDataAsset
		{
			id = "kings",
			color_hex = "#B74007",
			path_icon = "ui/Icons/iconKings",
			max = true
		});
		this.add(new HistoryDataAsset
		{
			id = "leaders",
			color_hex = "#9670B5",
			path_icon = "ui/Icons/iconLeaders",
			max = true
		});
		this.add(new HistoryDataAsset
		{
			id = "gold",
			color_hex = "#FED760",
			path_icon = "ui/Icons/iconResGold"
		});
		this.add(new HistoryDataAsset
		{
			id = "money",
			color_hex = "#F5C308",
			path_icon = "ui/Icons/iconMoney",
			max = true
		});
		this.add(new HistoryDataAsset
		{
			id = "families",
			statistics_asset = "families",
			color_hex = "#FF006E",
			path_icon = "ui/Icons/iconFamilyList",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "families_destroyed",
			statistics_asset = "world_statistics_families_destroyed",
			color_hex = "#FF006E",
			path_icon = "ui/Icons/iconFamilyDestroyed",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "families_created",
			statistics_asset = "world_statistics_families_created",
			color_hex = "#FF006E",
			path_icon = "ui/Icons/iconNewFamily",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "wood",
			color_hex = "#7C431C",
			path_icon = "ui/Icons/iconResWood"
		});
		this.add(new HistoryDataAsset
		{
			id = "stone",
			color_hex = "#9E5858",
			path_icon = "ui/Icons/iconResStone"
		});
		this.add(new HistoryDataAsset
		{
			id = "common_metals",
			color_hex = "#AAC7CF",
			path_icon = "ui/Icons/iconResCommonMetals"
		});
		this.add(new HistoryDataAsset
		{
			id = "territory",
			color_hex = "#D08B4B",
			path_icon = "ui/Icons/iconZones",
			max = true
		});
		this.add(new HistoryDataAsset
		{
			id = "islands",
			statistics_asset = "world_statistics_islands",
			color_hex = "#805C49",
			path_icon = "ui/Icons/iconZones",
			max = true
		});
		this.add(new HistoryDataAsset
		{
			id = "army",
			color_hex = "#2DBF4D",
			path_icon = "ui/Icons/iconArmy"
		});
		this.add(new HistoryDataAsset
		{
			id = "boats",
			color_hex = "#A0653B",
			path_icon = "ui/Icons/iconBoat"
		});
		this.add(new HistoryDataAsset
		{
			id = "children",
			color_hex = "#EB29B3",
			path_icon = "ui/Icons/iconChildren",
			enabled_default = true
		});
		this.add(new HistoryDataAsset
		{
			id = "buildings",
			color_hex = "#BD7743",
			path_icon = "ui/Icons/iconBuildings",
			max = true
		});
		this.add(new HistoryDataAsset
		{
			id = "sick",
			color_hex = "#A129EB",
			path_icon = "ui/Icons/iconSick",
			sum = true
		});
		this.add(new HistoryDataAsset
		{
			id = "infected",
			statistics_asset = "world_statistics_infected",
			color_hex = "#31A347",
			path_icon = "ui/Icons/actor_traits/iconInfected",
			max = true
		});
		this.add(new HistoryDataAsset
		{
			id = "loyalty",
			color_hex = "#EB29B3",
			path_icon = "ui/Icons/iconLoyalty"
		});
		this.add(new HistoryDataAsset
		{
			id = "happy",
			color_hex = "#FFEC3F",
			path_icon = "ui/Icons/iconHappiness_6"
		});
		this.add(new HistoryDataAsset
		{
			id = "adults",
			color_hex = "#2B86D1",
			path_icon = "ui/Icons/iconAdults",
			enabled_default = true
		});
		this.add(new HistoryDataAsset
		{
			id = "deaths",
			color_hex = "#282828",
			path_icon = "ui/Icons/iconDead",
			max = true
		});
		this.t.category_group |= GraphCategoryGroup.Deaths;
		this.add(new HistoryDataAsset
		{
			id = "deaths_attackers",
			localized_key = "attackers_deaths",
			color_hex = "#AD9878",
			path_icon = "ui/Icons/iconDeathAttackers",
			sum = true
		});
		this.t.category_group |= GraphCategoryGroup.Deaths;
		this.add(new HistoryDataAsset
		{
			id = "deaths_defenders",
			localized_key = "defenders_deaths",
			color_hex = "#979894",
			path_icon = "ui/Icons/iconDeathDefenders",
			sum = true
		});
		this.t.category_group |= GraphCategoryGroup.Deaths;
		this.add(new HistoryDataAsset
		{
			id = "money_attackers",
			localized_key = "attackers_money",
			color_hex = "#C3800E",
			path_icon = "ui/Icons/iconMoney",
			max = true
		});
		this.add(new HistoryDataAsset
		{
			id = "money_defenders",
			localized_key = "defenders_money",
			color_hex = "#FFFF49",
			path_icon = "ui/Icons/iconMoney",
			max = true
		});
		this.add(new HistoryDataAsset
		{
			id = "births",
			color_hex = "#EB5D4D",
			path_icon = "ui/Icons/iconBirths",
			max = true,
			enabled_default = true
		});
		this.t.category_group |= GraphCategoryGroup.Deaths;
		this.add(new HistoryDataAsset
		{
			id = "renown",
			color_hex = "#FFFF49",
			path_icon = "ui/Icons/iconRenown",
			max = true
		});
		this.add(new HistoryDataAsset
		{
			id = "joined",
			color_hex = "#DBC93F",
			path_icon = "ui/Icons/iconStatisticsJoined",
			localized_key = "statistics_joined",
			localized_key_description = "statistics_joined".Description(),
			max = true
		});
		this.add(new HistoryDataAsset
		{
			id = "left",
			color_hex = "#282828",
			path_icon = "ui/Icons/iconStatisticsLeft",
			localized_key = "statistics_left",
			localized_key_description = "statistics_left".Description(),
			max = true
		});
		this.add(new HistoryDataAsset
		{
			id = "moved",
			color_hex = "#282828",
			path_icon = "ui/Icons/iconStatisticsMoved",
			localized_key = "statistics_moved",
			localized_key_description = "statistics_moved".Description(),
			max = true
		});
		this.add(new HistoryDataAsset
		{
			id = "migrated",
			color_hex = "#282828",
			path_icon = "ui/Icons/iconStatisticsGoodLookingMigrants",
			localized_key = "statistics_migrated",
			localized_key_description = "statistics_migrated".Description(),
			max = true
		});
		this.add(new HistoryDataAsset
		{
			id = "males",
			color_hex = "#2B86D1",
			path_icon = "ui/Icons/iconMale"
		});
		this.add(new HistoryDataAsset
		{
			id = "females",
			color_hex = "#AD3640",
			path_icon = "ui/Icons/iconFemale"
		});
		this.add(new HistoryDataAsset
		{
			id = "speakers_converted",
			color_hex = "#a42cec",
			path_icon = "ui/Icons/iconSpeakersTotal",
			max = true
		});
		this.add(new HistoryDataAsset
		{
			id = "speakers_new",
			color_hex = "#A129EB",
			path_icon = "ui/Icons/iconSpeakersNew",
			max = true
		});
		this.add(new HistoryDataAsset
		{
			id = "speakers_lost",
			color_hex = "#B92935",
			path_icon = "ui/Icons/iconSpeakersLost",
			max = true
		});
		this.add(new HistoryDataAsset
		{
			id = "books_written",
			statistics_asset = "world_statistics_books_written",
			color_hex = "#ac9c7c",
			path_icon = "ui/Icons/iconBooksWritten",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "books",
			statistics_asset = "books",
			color_hex = "#358A23",
			path_icon = "ui/Icons/iconBooks",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "books_burnt",
			statistics_asset = "world_statistics_books_burnt",
			color_hex = "#AD3442",
			path_icon = "ui/Icons/iconBooksDestroyed",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "books_read",
			statistics_asset = "world_statistics_books_read",
			color_hex = "#7C29B9",
			path_icon = "ui/Icons/iconBooksRead",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "army_attackers",
			localized_key = "attackers_army",
			color_hex = "#996753",
			path_icon = "ui/Icons/iconWarriors"
		});
		this.add(new HistoryDataAsset
		{
			id = "army_defenders",
			localized_key = "defenders_army",
			color_hex = "#698B99",
			path_icon = "ui/Icons/iconShield"
		});
		this.add(new HistoryDataAsset
		{
			id = "population_attackers",
			localized_key = "attackers_population",
			color_hex = "#325870",
			path_icon = "ui/Icons/iconPopulationAttackers"
		});
		this.add(new HistoryDataAsset
		{
			id = "population_defenders",
			localized_key = "defenders_population",
			color_hex = "#414349",
			path_icon = "ui/Icons/iconPopulationDefenders"
		});
		this.add(new HistoryDataAsset
		{
			id = "kills",
			color_hex = "#D7C493",
			path_icon = "ui/Icons/iconKills",
			max = true
		});
		this.add(new HistoryDataAsset
		{
			id = "alliances",
			statistics_asset = "alliances",
			color_hex = "#A7B7A8",
			path_icon = "ui/Icons/iconAllianceList",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "alliances_dissolved",
			statistics_asset = "world_statistics_alliances_dissolved",
			color_hex = "#CE2C29",
			path_icon = "ui/Icons/iconAllianceDissolved",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "alliances_made",
			statistics_asset = "world_statistics_alliances_made",
			color_hex = "#1F4792",
			path_icon = "ui/Icons/iconAlliance",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "clans",
			statistics_asset = "clans",
			color_hex = "#65A13F",
			path_icon = "ui/Icons/iconClanList",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "clans_destroyed",
			statistics_asset = "world_statistics_clans_destroyed",
			color_hex = "#AD363F",
			path_icon = "ui/Icons/iconClanDestroyed",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "clans_created",
			statistics_asset = "world_statistics_clans_created",
			color_hex = "#783057",
			path_icon = "ui/Icons/iconClan",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "cultures",
			statistics_asset = "cultures",
			color_hex = "#AD9878",
			path_icon = "ui/Icons/iconCultureList",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "cultures_forgotten",
			statistics_asset = "world_statistics_cultures_forgotten",
			color_hex = "#AD9A73",
			path_icon = "ui/Icons/iconCultureForgotten",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "cultures_created",
			statistics_asset = "world_statistics_cultures_created",
			color_hex = "#D7C493",
			path_icon = "ui/Icons/iconCulture",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "languages",
			statistics_asset = "languages",
			color_hex = "#A7B7A8",
			path_icon = "ui/Icons/iconLanguageList",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "languages_forgotten",
			statistics_asset = "world_statistics_languages_forgotten",
			color_hex = "#737874",
			path_icon = "ui/Icons/iconLanguageForgotten",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "languages_created",
			statistics_asset = "world_statistics_languages_created",
			color_hex = "#FFFFFF",
			path_icon = "ui/Icons/iconLanguage",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "subspecies",
			statistics_asset = "subspecies",
			color_hex = "#FFFFFF",
			path_icon = "ui/Icons/iconSpecies",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "subspecies_extinct",
			statistics_asset = "world_statistics_subspecies_extinct",
			color_hex = "#AD9A7B",
			path_icon = "ui/Icons/iconSpeciesExtinct",
			max = true,
			category_group = GraphCategoryGroup.Deaths
		});
		this.add(new HistoryDataAsset
		{
			id = "subspecies_created",
			statistics_asset = "world_statistics_subspecies_created",
			color_hex = "#FFFFFF",
			path_icon = "ui/Icons/iconSpecies",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "plots_started",
			statistics_asset = "world_statistics_plots_started",
			color_hex = "#ac9c7c",
			path_icon = "ui/Icons/iconPlot",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "plots",
			statistics_asset = "plots",
			color_hex = "#3a3bbb",
			path_icon = "ui/Icons/iconPlotList",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "plots_forgotten",
			statistics_asset = "world_statistics_plots_forgotten",
			color_hex = "#AD9A7B",
			path_icon = "ui/Icons/iconPlotForgotten",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "plots_succeeded",
			statistics_asset = "world_statistics_plots_succeeded",
			color_hex = "#3a3bbb",
			path_icon = "ui/Icons/iconPlotSucceeded",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "religions",
			statistics_asset = "religions",
			color_hex = "#8EFFD9",
			path_icon = "ui/Icons/iconReligionList",
			max = true
		});
		this.add(new HistoryDataAsset
		{
			id = "religions_forgotten",
			statistics_asset = "world_statistics_religions_forgotten",
			color_hex = "#CEDBBD",
			path_icon = "ui/Icons/iconReligionForgotten",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "religions_created",
			statistics_asset = "world_statistics_religions_created",
			color_hex = "#46DCE3",
			path_icon = "ui/Icons/iconReligion",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "wars",
			statistics_asset = "wars",
			color_hex = "#D3D9C5",
			path_icon = "ui/Icons/iconWar",
			max = true
		});
		this.add(new HistoryDataAsset
		{
			id = "peaces_made",
			statistics_asset = "world_statistics_peaces_made",
			color_hex = "#51B349",
			path_icon = "ui/Icons/actor_traits/iconPacifist",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "wars_started",
			statistics_asset = "world_statistics_wars_started",
			color_hex = "#3a3bbb",
			path_icon = "ui/Icons/iconWhisperOfWar",
			max = true,
			category_group = GraphCategoryGroup.Noosphere
		});
		this.add(new HistoryDataAsset
		{
			id = "deaths_total",
			statistics_asset = "world_statistics_deaths_total",
			color_hex = "#A7B7A8",
			path_icon = "ui/Icons/iconDead",
			max = true,
			category_group = GraphCategoryGroup.Deaths
		});
		this.clone("deaths_natural", "deaths_total");
		this.t.statistics_asset = "world_statistics_deaths_natural";
		this.t.path_icon = "ui/Icons/iconClock";
		this.t.enabled_default = true;
		this.t.color_hex = "#AF6E19";
		this.clone("deaths_hunger", "deaths_total");
		this.t.statistics_asset = "world_statistics_deaths_hunger";
		this.t.path_icon = "ui/Icons/iconDeathsHunger";
		this.t.enabled_default = true;
		this.t.color_hex = "#BDD7DE";
		this.clone("deaths_eaten", "deaths_total");
		this.t.statistics_asset = "world_statistics_deaths_eaten";
		this.t.path_icon = "ui/Icons/iconDeathsEaten";
		this.t.color_hex = "#EFAE52";
		this.clone("deaths_plague", "deaths_total");
		this.t.statistics_asset = "world_statistics_deaths_plague";
		this.t.path_icon = "ui/Icons/actor_traits/iconPlague";
		this.t.color_hex = "#DA18D8";
		this.clone("deaths_poison", "deaths_total");
		this.t.statistics_asset = "world_statistics_deaths_poison";
		this.t.path_icon = "ui/Icons/iconPoisoned";
		this.t.color_hex = "#A31BC9";
		this.clone("deaths_infection", "deaths_total");
		this.t.statistics_asset = "world_statistics_deaths_infection";
		this.t.path_icon = "ui/Icons/actor_traits/iconInfected";
		this.t.color_hex = "#31A347";
		this.clone("deaths_tumor", "deaths_total");
		this.t.statistics_asset = "world_statistics_deaths_tumor";
		this.t.path_icon = "ui/Icons/iconTumor";
		this.t.color_hex = "#EF3F58";
		this.clone("deaths_acid", "deaths_total");
		this.t.statistics_asset = "world_statistics_deaths_acid";
		this.t.path_icon = "ui/Icons/iconAcid";
		this.t.color_hex = "#ADCF3A";
		this.clone("deaths_fire", "deaths_total");
		this.t.statistics_asset = "world_statistics_deaths_fire";
		this.t.path_icon = "ui/Icons/iconFire";
		this.t.color_hex = "#D92D14";
		this.clone("deaths_divine", "deaths_total");
		this.t.statistics_asset = "world_statistics_deaths_divine";
		this.t.path_icon = "ui/Icons/iconDivineLight";
		this.t.color_hex = "#FFE490";
		this.clone("deaths_weapon", "deaths_total");
		this.t.statistics_asset = "world_statistics_deaths_weapon";
		this.t.path_icon = "ui/Icons/actor_traits/iconBloodlust";
		this.t.enabled_default = true;
		this.t.color_hex = "#7F1212";
		this.clone("deaths_gravity", "deaths_total");
		this.t.statistics_asset = "world_statistics_deaths_gravity";
		this.t.path_icon = "ui/Icons/worldrules/icon_grow_trees";
		this.t.color_hex = "#65A13F";
		this.clone("deaths_drowning", "deaths_total");
		this.t.statistics_asset = "world_statistics_deaths_drowning";
		this.t.path_icon = "ui/Icons/iconTileDeepOcean";
		this.t.color_hex = "#00B9D1";
		this.clone("deaths_water", "deaths_total");
		this.t.statistics_asset = "world_statistics_deaths_water";
		this.t.path_icon = "ui/Icons/iconRain";
		this.t.color_hex = "#CBF4FF";
		this.clone("deaths_explosion", "deaths_total");
		this.t.statistics_asset = "world_statistics_deaths_explosion";
		this.t.path_icon = "ui/Icons/worldrules/icon_exploding_mushrooms";
		this.t.color_hex = "#FFB22D";
		this.clone("deaths_other", "deaths_total");
		this.t.statistics_asset = "world_statistics_deaths_other";
		this.t.path_icon = "ui/Icons/iconDead";
		this.t.color_hex = "#D7C493";
		this.add(new HistoryDataAsset
		{
			id = "metamorphosis",
			statistics_asset = "world_statistics_metamorphosis",
			color_hex = "#8EFFD9",
			path_icon = "ui/Icons/subspecies_traits/subspecies_trait_reproduction_metamorph",
			max = true
		});
		this.add(new HistoryDataAsset
		{
			id = "evolutions",
			statistics_asset = "world_statistics_evolutions",
			color_hex = "#a3836c",
			path_icon = "ui/Icons/iconMonolith",
			max = true
		});
		this.add(new HistoryDataAsset
		{
			id = "trees",
			statistics_asset = "world_statistics_trees",
			color_hex = "#86BC4E",
			path_icon = "ui/Icons/iconFertilizerTrees"
		});
		this.add(new HistoryDataAsset
		{
			id = "vegetation",
			statistics_asset = "world_statistics_vegetation",
			color_hex = "#31A347",
			path_icon = "ui/Icons/iconFertilizerPlants"
		});
	}

	// Token: 0x06002D6C RID: 11628 RVA: 0x001635F8 File Offset: 0x001617F8
	public void addBiomes()
	{
		this.add(new HistoryDataAsset
		{
			id = "grass",
			statistics_asset = "world_statistics_grass",
			color_hex = "#5EE54E",
			path_icon = "ui/Icons/iconSeedGrass",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "savanna",
			statistics_asset = "world_statistics_savanna",
			color_hex = "#D4854C",
			path_icon = "ui/Icons/iconSeedSavanna",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "jungle",
			statistics_asset = "world_statistics_jungle",
			color_hex = "#45C131",
			path_icon = "ui/Icons/iconSeedJungle",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "desert",
			statistics_asset = "world_statistics_desert",
			color_hex = "#E9C057",
			path_icon = "ui/Icons/iconSeedDesert",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "lemon",
			statistics_asset = "world_statistics_lemon",
			color_hex = "#FDD435",
			path_icon = "ui/Icons/iconSeedLemon",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "permafrost",
			statistics_asset = "world_statistics_permafrost",
			color_hex = "#9AF8FF",
			path_icon = "ui/Icons/iconSeedPermafrost",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "swamp",
			statistics_asset = "world_statistics_swamp",
			color_hex = "#447528",
			path_icon = "ui/Icons/iconSeedSwamp",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "crystal",
			statistics_asset = "world_statistics_crystal",
			color_hex = "#7D6887",
			path_icon = "ui/Icons/iconSeedCrystal",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "enchanted",
			statistics_asset = "world_statistics_enchanted",
			color_hex = "#F6FFC2",
			path_icon = "ui/Icons/iconSeedEnchanted",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "corruption",
			statistics_asset = "world_statistics_corruption",
			color_hex = "#3D2F40",
			path_icon = "ui/Icons/iconSeedCorrupted",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "infernal",
			statistics_asset = "world_statistics_infernal",
			color_hex = "#D0371D",
			path_icon = "ui/Icons/iconSeedInfernal",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "candy",
			statistics_asset = "world_statistics_candy",
			color_hex = "#C73736",
			path_icon = "ui/Icons/iconSeedCandy",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "mushroom",
			statistics_asset = "world_statistics_mushroom",
			color_hex = "#D65C4A",
			path_icon = "ui/Icons/iconSeedMushroom",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "wasteland",
			statistics_asset = "world_statistics_wasteland",
			color_hex = "#65A03F",
			path_icon = "ui/Icons/achievements/achievements_wastelandbiome",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "birch",
			statistics_asset = "world_statistics_birch",
			color_hex = "#757A81",
			path_icon = "ui/Icons/iconSeedBirch",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "maple",
			statistics_asset = "world_statistics_maple",
			color_hex = "#B92935",
			path_icon = "ui/Icons/iconSeedMaple",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "rocklands",
			statistics_asset = "world_statistics_rocklands",
			color_hex = "#3C6732",
			path_icon = "ui/Icons/iconSeedRocklands",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "garlic",
			statistics_asset = "world_statistics_garlic",
			color_hex = "#D7C493",
			path_icon = "ui/Icons/iconSeedGarlic",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "flower",
			statistics_asset = "world_statistics_flower",
			color_hex = "#9E3A4C",
			path_icon = "ui/Icons/iconSeedFlower",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "celestial",
			statistics_asset = "world_statistics_celestial",
			color_hex = "#F6F248",
			path_icon = "ui/Icons/iconSeedCelestial",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "clover",
			statistics_asset = "world_statistics_clover",
			color_hex = "#35B929",
			path_icon = "ui/Icons/iconSeedClover",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "singularity",
			statistics_asset = "world_statistics_singularity",
			color_hex = "#A9CAFF",
			path_icon = "ui/Icons/iconSeedSingularity",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "paradox",
			statistics_asset = "world_statistics_paradox",
			color_hex = "#C3800E",
			path_icon = "ui/Icons/iconSeedParadox",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "sand",
			statistics_asset = "world_statistics_sand",
			color_hex = "#F6BD67",
			path_icon = "ui/Icons/iconTileSand",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "biomass",
			statistics_asset = "world_statistics_biomass",
			color_hex = "#29B326",
			path_icon = "ui/Icons/iconBiomass",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "cybertile",
			statistics_asset = "world_statistics_cybertile",
			color_hex = "#BD8137",
			path_icon = "ui/Icons/iconCybercore",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "pumpkin",
			statistics_asset = "world_statistics_pumpkin",
			color_hex = "#FF8D1A",
			path_icon = "ui/Icons/iconSuperPumpkin",
			category_group = GraphCategoryGroup.Biomes
		});
		this.add(new HistoryDataAsset
		{
			id = "tumor",
			statistics_asset = "world_statistics_tumor",
			color_hex = "#D1184F",
			path_icon = "ui/Icons/iconTumor",
			category_group = GraphCategoryGroup.Biomes
		});
	}

	// Token: 0x06002D6D RID: 11629 RVA: 0x00163CEC File Offset: 0x00161EEC
	public void addTiles()
	{
		this.add(new HistoryDataAsset
		{
			id = "water",
			statistics_asset = "world_statistics_water",
			color_hex = "#00B9D1",
			path_icon = "ui/Icons/iconTileDeepOcean",
			category_group = GraphCategoryGroup.Tiles
		});
		this.add(new HistoryDataAsset
		{
			id = "soil",
			statistics_asset = "world_statistics_soil",
			color_hex = "#D08B4B",
			path_icon = "ui/Icons/iconTileSoil",
			category_group = GraphCategoryGroup.Tiles
		});
		this.add(new HistoryDataAsset
		{
			id = "summit",
			statistics_asset = "world_statistics_summit",
			color_hex = "#BBEFFF",
			path_icon = "ui/Icons/iconTileSummit",
			category_group = GraphCategoryGroup.Tiles
		});
		this.add(new HistoryDataAsset
		{
			id = "mountains",
			statistics_asset = "world_statistics_mountains",
			color_hex = "#97ADB7",
			path_icon = "ui/Icons/iconTileMountains",
			category_group = GraphCategoryGroup.Tiles
		});
		this.add(new HistoryDataAsset
		{
			id = "hills",
			statistics_asset = "world_statistics_hills",
			color_hex = "#657688",
			path_icon = "ui/Icons/iconTileHills",
			category_group = GraphCategoryGroup.Tiles
		});
		this.add(new HistoryDataAsset
		{
			id = "lava",
			statistics_asset = "world_statistics_lava",
			color_hex = "#BB311C",
			path_icon = "ui/Icons/iconLava",
			category_group = GraphCategoryGroup.Tiles
		});
		this.add(new HistoryDataAsset
		{
			id = "pit",
			statistics_asset = "world_statistics_pit",
			color_hex = "#53EDE3",
			path_icon = "ui/Icons/iconTileShallowWater",
			category_group = GraphCategoryGroup.Tiles
		});
		this.add(new HistoryDataAsset
		{
			id = "field",
			statistics_asset = "world_statistics_field",
			color_hex = "#C3800E",
			path_icon = "ui/Icons/citizen_jobs/iconCitizenJobFarmer",
			category_group = GraphCategoryGroup.Tiles
		});
		this.add(new HistoryDataAsset
		{
			id = "fireworks",
			statistics_asset = "world_statistics_fireworks",
			color_hex = "#FF0000",
			path_icon = "ui/Icons/iconFireworks",
			category_group = GraphCategoryGroup.Tiles
		});
		this.add(new HistoryDataAsset
		{
			id = "frozen",
			statistics_asset = "world_statistics_frozen",
			color_hex = "#60C1C8",
			path_icon = "ui/Icons/iconFrozen",
			category_group = GraphCategoryGroup.Tiles
		});
		this.add(new HistoryDataAsset
		{
			id = "fuse",
			statistics_asset = "world_statistics_fuse",
			color_hex = "#B55400",
			path_icon = "ui/Icons/iconFuse",
			category_group = GraphCategoryGroup.Tiles
		});
		this.add(new HistoryDataAsset
		{
			id = "ice",
			statistics_asset = "world_statistics_ice",
			color_hex = "#9AF8FF",
			path_icon = "ui/Icons/iconIceberg",
			category_group = GraphCategoryGroup.Tiles
		});
		this.add(new HistoryDataAsset
		{
			id = "landmine",
			statistics_asset = "world_statistics_landmine",
			color_hex = "#FF2E00",
			path_icon = "ui/Icons/iconLandmine",
			category_group = GraphCategoryGroup.Tiles
		});
		this.add(new HistoryDataAsset
		{
			id = "road",
			statistics_asset = "world_statistics_road",
			color_hex = "#80D03F",
			path_icon = "ui/Icons/citizen_jobs/iconCitizenJobRoadBuilder",
			category_group = GraphCategoryGroup.Tiles
		});
		this.add(new HistoryDataAsset
		{
			id = "snow",
			statistics_asset = "world_statistics_snow",
			color_hex = "#F2F3ED",
			path_icon = "ui/Icons/iconSnow",
			category_group = GraphCategoryGroup.Tiles
		});
		this.add(new HistoryDataAsset
		{
			id = "tnt",
			statistics_asset = "world_statistics_tnt",
			color_hex = "#F67A39",
			path_icon = "ui/Icons/iconTnt",
			category_group = GraphCategoryGroup.Tiles
		});
		this.add(new HistoryDataAsset
		{
			id = "wall",
			statistics_asset = "world_statistics_wall",
			color_hex = "#425B78",
			path_icon = "ui/Icons/iconWallIron",
			category_group = GraphCategoryGroup.Tiles
		});
		this.add(new HistoryDataAsset
		{
			id = "water_bomb",
			statistics_asset = "world_statistics_water_bomb",
			color_hex = "#00A2DE",
			path_icon = "ui/Icons/iconWaterBomb",
			category_group = GraphCategoryGroup.Tiles
		});
		this.add(new HistoryDataAsset
		{
			id = "grey_goo",
			statistics_asset = "world_statistics_grey_goo",
			color_hex = "#4D666D",
			path_icon = "ui/Icons/iconGreygoo",
			category_group = GraphCategoryGroup.Tiles
		});
	}

	// Token: 0x06002D6E RID: 11630 RVA: 0x001641B9 File Offset: 0x001623B9
	public override HistoryDataAsset add(HistoryDataAsset pAsset)
	{
		pAsset.average = (!pAsset.max && !pAsset.sum);
		return base.add(pAsset);
	}

	// Token: 0x06002D6F RID: 11631 RVA: 0x001641DC File Offset: 0x001623DC
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (HistoryDataAsset tAsset in this.list)
		{
			if (!string.IsNullOrEmpty(tAsset.statistics_asset))
			{
				StatisticsAsset tStatsAsset = AssetManager.statistics_library.get(tAsset.statistics_asset);
				if (tStatsAsset.path_icon != tAsset.path_icon)
				{
					BaseAssetLibrary.logAssetError(string.Concat(new string[]
					{
						"HistoryDataAsset: StatisticsAsset icon_path (",
						tStatsAsset.path_icon,
						") does not match HistoryDataAsset path_icon (",
						tAsset.path_icon,
						")"
					}), tAsset.id);
				}
				if (string.IsNullOrEmpty(tAsset.localized_key))
				{
					tAsset.localized_key = tStatsAsset.getLocaleID();
				}
				if (string.IsNullOrEmpty(tAsset.localized_key_description))
				{
					tAsset.localized_key_description = tStatsAsset.localized_key_description;
				}
			}
		}
	}

	// Token: 0x06002D70 RID: 11632 RVA: 0x001642DC File Offset: 0x001624DC
	public override void post_init()
	{
		base.post_init();
		foreach (HistoryDataAsset tAsset in this.list)
		{
			Color tColor = Toolbox.makeColor(tAsset.color_hex);
			if (tColor.grayscale < 0.28f)
			{
				Color tBrighter = Color.Lerp(tColor, Color.white, 0.33f);
				string tBrighterHex = "#" + ColorUtility.ToHtmlStringRGB(tBrighter);
				tAsset.tooltip_color_hex = tBrighterHex;
			}
			else
			{
				tAsset.tooltip_color_hex = tAsset.color_hex;
			}
		}
	}

	// Token: 0x06002D71 RID: 11633 RVA: 0x00164384 File Offset: 0x00162584
	public override void editorDiagnosticLocales()
	{
		foreach (HistoryDataAsset tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
			this.checkLocale(tAsset, tAsset.getDescriptionID());
		}
		base.editorDiagnosticLocales();
	}

	// Token: 0x06002D72 RID: 11634 RVA: 0x001643F0 File Offset: 0x001625F0
	public string addToGameplayReport(string pWhatFor)
	{
		string tResult = string.Empty;
		tResult = tResult + pWhatFor + "\n";
		foreach (HistoryDataAsset historyDataAsset in this.list)
		{
			string tName = historyDataAsset.getLocaleID().Localize();
			string tDescription = historyDataAsset.getDescriptionID().Localize();
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
}
