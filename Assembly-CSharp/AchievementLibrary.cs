using System;
using System.Collections.Generic;
using Beebyte.Obfuscator;
using UnityEngine;

// Token: 0x0200009E RID: 158
[ObfuscateLiterals]
[Serializable]
public class AchievementLibrary : AssetLibrary<Achievement>
{
	// Token: 0x060004E3 RID: 1251 RVA: 0x00033670 File Offset: 0x00031870
	public override void init()
	{
		base.init();
		Debug.Log("Init Achievements");
		AchievementLibrary.the_accomplished = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "1_20",
			id = "achievementTheAccomplished",
			action = new AchievementCheck(AchievementLibrary.checkTheAccomplished),
			icon = "ui/Icons/achievements/achievements_theAccomplished",
			group = "creatures"
		});
		AchievementLibrary.the_king = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "1_17",
			id = "achievementTheKing",
			action = new AchievementCheck(AchievementLibrary.checkTheKing),
			icon = "ui/Icons/achievements/achievements_theKing",
			group = "creatures"
		});
		AchievementLibrary.the_demon = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "1_21",
			id = "achievementTheDemon",
			action = new AchievementCheck(AchievementLibrary.checkTheDemon),
			icon = "ui/Icons/achievements/achievements_theDemon",
			group = "creatures"
		});
		AchievementLibrary.the_broken = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "1_22",
			id = "achievementTheBroken",
			action = new AchievementCheck(AchievementLibrary.checkTheBroken),
			icon = "ui/Icons/achievements/achievements_theBroken",
			group = "creatures"
		});
		AchievementLibrary.touch_the_grass = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "2_10",
			id = "achievementTouchTheGrass",
			icon = "ui/Icons/achievements/achievements_touchTheGrass",
			group = "nature",
			hidden = true
		});
		AchievementLibrary.gen_5_worlds = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "1_0",
			id = "achievementGen5Worlds",
			action = new AchievementCheck(AchievementLibrary.checkMapCreations5),
			icon = "ui/Icons/achievements/achievements_generate5",
			group = "worlds"
		});
		AchievementLibrary.gen_50_worlds = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "1_1",
			id = "achievementGen50Worlds",
			action = new AchievementCheck(AchievementLibrary.checkMapCreations50),
			icon = "ui/Icons/achievements/achievements_generate50",
			group = "worlds"
		});
		AchievementLibrary.gen_100_worlds = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "1_2",
			id = "achievementGen100Worlds",
			action = new AchievementCheck(AchievementLibrary.checkMapCreations100),
			icon = "ui/Icons/achievements/achievements_generate100",
			group = "worlds"
		});
		AchievementLibrary.life_is_a_sim = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "1_3",
			id = "achievementLifeIsASim",
			hidden = true,
			action = new AchievementCheck(AchievementLibrary.checkLifeIsASim),
			icon = "ui/Icons/achievements/achievements_lifeissimulation",
			group = "miscellaneous"
		});
		AchievementLibrary.the_corrupted_trees = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "1_31",
			id = "achievementTheCorruptedTrees",
			icon = "ui/Icons/achievements/achievements_corruptedbiome",
			group = "exploration"
		});
		AchievementLibrary.the_hell = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "2_0",
			id = "achievementTheHell",
			hidden = true,
			action = new AchievementCheck(AchievementLibrary.checkTheHell),
			icon = "ui/Icons/achievements/achievements_infernalbiome",
			group = "destruction"
		});
		AchievementLibrary.lets_not = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "2_1",
			id = "achievementLetsNot",
			hidden = true,
			action = new AchievementCheck(AchievementLibrary.checkLetsNot),
			icon = "ui/Icons/achievements/achievements_wastelandbiome",
			group = "destruction"
		});
		AchievementLibrary.world_war = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "2_2",
			id = "achievementWorldWar",
			hidden = true,
			action = new AchievementCheck(AchievementLibrary.checkWorldWar),
			icon = "ui/Icons/achievements/achievements_worldwar",
			group = "civilizations"
		});
		AchievementLibrary.planet_of_apes = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "1_23",
			id = "achievementPlanetOfApes",
			hidden = true,
			action = new AchievementCheck(AchievementLibrary.checkPlanetOfTheApes),
			icon = "ui/Icons/achievements/achievements_planetofapes",
			group = "creatures"
		});
		AchievementLibrary.super_mushroom = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "1_24",
			id = "achievementSuperMushroom",
			hidden = true,
			icon = "ui/Icons/achievements/achievements_supermush",
			group = "creatures"
		});
		AchievementLibrary.the_princess = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "1_25",
			id = "achievementThePrincess",
			hidden = true,
			icon = "ui/Icons/achievements/achievements_princess",
			group = "creatures"
		});
		AchievementLibrary.tornado = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "2_11",
			id = "achievementTORNADO",
			locale_key = "achievement_tornado",
			hidden = true,
			icon = "ui/Icons/achievements/achievements_cursedtornado",
			group = "nature"
		});
		AchievementLibrary.god_mode = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "2_13",
			id = "achievementGodMode",
			hidden = true,
			icon = "ui/Icons/achievements/achievements_godmode",
			group = "miscellaneous"
		});
		AchievementLibrary.greg = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "1_26",
			id = "achievementGreg",
			hidden = true,
			icon = "ui/Icons/achievements/achievements_greg",
			group = "forbidden"
		});
		Achievement achievement = new Achievement();
		achievement.play_store_id = "CgkIia6M98wfEAIQAg";
		achievement.steam_id = "1_27";
		achievement.id = "achievementNinjaTurtle";
		achievement.icon = "ui/Icons/achievements/achievements_ninjaturtle";
		achievement.action = delegate(object pActor)
		{
			Actor tActor = pActor as Actor;
			return tActor.asset.flag_turtle && tActor.level >= 10;
		};
		achievement.group = "creatures";
		AchievementLibrary.ninja_turtle = this.add(achievement);
		AchievementLibrary.great_plague = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "2_3",
			id = "achievementGreatPlague",
			hidden = true,
			action = new AchievementCheck(AchievementLibrary.checkGreatPlague),
			icon = "ui/Icons/achievements/achievements_plagueworld",
			group = "experiments"
		});
		AchievementLibrary.lava_strike = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "2_4",
			id = "achievementLavaStrike",
			hidden = true,
			icon = "ui/Icons/actor_traits/iconLightning",
			group = "destruction"
		});
		AchievementLibrary.baby_tornado = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAw",
			steam_id = "2_12",
			id = "achievementBabyTornado",
			hidden = true,
			icon = "ui/Icons/achievements/achievements_babytornado",
			group = "nature"
		});
		AchievementLibrary.rain_tornado = this.add(new Achievement
		{
			id = "achievementRainTornado",
			play_store_id = "CgkIia6M98wfEAIQAw",
			steam_id = "2_21",
			icon = "ui/Icons/achievements/achievements_raintornado",
			group = "nature",
			hidden = true,
			action = new AchievementCheck(AchievementLibrary.checkRainTornado)
		});
		AchievementLibrary.ten_thousands_creatures = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQBA",
			steam_id = "1_4",
			id = "achievement10000Creatures",
			action = new AchievementCheck(AchievementLibrary.check10000Creatures),
			icon = "ui/Icons/achievements/achievements_1000Creatures",
			group = "creation"
		});
		AchievementLibrary.many_bombs = this.add(new Achievement
		{
			play_store_id = "",
			steam_id = "2_5",
			id = "achievementManyBombs",
			action = new AchievementCheck(AchievementLibrary.checkManyBombs),
			icon = "ui/Icons/iconBomb",
			group = "destruction"
		});
		AchievementLibrary.megapolis = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQBg",
			steam_id = "1_18",
			hidden = true,
			id = "achievementMegapolis",
			action = new AchievementCheck(AchievementLibrary.checkMegapolis),
			icon = "ui/Icons/achievements/achievements_megapolis",
			group = "civilizations"
		});
		AchievementLibrary.wilhelm_scream = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQBw",
			steam_id = "2_14",
			id = "achievementMakeWilhelmScream",
			hidden = true,
			icon = "ui/Icons/iconHumans",
			group = "exploration"
		});
		AchievementLibrary.burger = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "2_15",
			id = "achievementBurger",
			hidden = true,
			icon = "ui/Icons/iconBurger",
			group = "exploration"
		});
		AchievementLibrary.burger = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "2_16",
			id = "achievementPie",
			hidden = true,
			icon = "ui/Icons/iconResPie",
			group = "exploration"
		});
		AchievementLibrary.mayday = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "2_6",
			id = "achievementMayday",
			hidden = true,
			icon = "ui/Icons/iconSanta",
			group = "destruction"
		});
		AchievementLibrary.destroy_worldbox = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "2_17",
			id = "achievementDestroyWorldBox",
			hidden = true,
			icon = "ui/Icons/iconBrowse2",
			group = "exploration"
		});
		AchievementLibrary.custom_world = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "1_5",
			id = "achievementCustomWorld",
			hidden = true,
			icon = "ui/Icons/iconTileSoil",
			group = "creation"
		});
		AchievementLibrary.four_race_cities = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "1_19",
			id = "achievement4RaceCities",
			hidden = true,
			icon = "ui/Icons/achievements/achievements_4Races",
			action = new AchievementCheck(AchievementLibrary.check4RaceCities),
			group = "civilizations"
		});
		AchievementLibrary.piranha_land = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "1_28",
			id = "achievementPiranhaLand",
			hidden = true,
			icon = "ui/Icons/iconPiranha",
			action = new AchievementCheck(AchievementLibrary.checkPiranhaLand),
			group = "experiments"
		});
		AchievementLibrary.print_heart = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "1_6",
			id = "achievementPrintHeart",
			hidden = true,
			action = new AchievementCheck(AchievementLibrary.checkPrintHeart),
			icon = "ui/Icons/achievements/achievements_printHeart",
			group = "creation"
		});
		AchievementLibrary.sacrifice = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "1_29",
			id = "achievementSacrifice",
			hidden = true,
			icon = "ui/Icons/iconSheep",
			group = "experiments"
		});
		AchievementLibrary.ant_world = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "1_30",
			id = "achievementAntWorld",
			hidden = true,
			icon = "ui/Icons/iconAntBlack",
			action = new AchievementCheck(AchievementLibrary.checkAntWorld),
			group = "creatures"
		});
		AchievementLibrary.final_resolution = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "2_7",
			id = "achievementFinalResolution",
			hidden = true,
			icon = "ui/Icons/iconGreygoo",
			group = "destruction"
		});
		AchievementLibrary.tnt_and_heat = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "2_8",
			id = "achievementTntAndHeat",
			hidden = true,
			icon = "ui/Icons/iconTnt",
			group = "destruction"
		});
		AchievementLibrary.god_finger_lightning = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "2_9",
			id = "achievementGodFingerLightning",
			hidden = true,
			icon = "ui/Icons/iconGodFinger",
			group = "destruction"
		});
		Achievement achievement2 = new Achievement();
		achievement2.play_store_id = "?";
		achievement2.steam_id = "2_18";
		achievement2.id = "achievementTraitsExplorer40";
		achievement2.hidden = false;
		achievement2.icon = "ui/Icons/achievements/achievements_traitexplorer1";
		achievement2.group = "collection";
		achievement2.action = ((object _) => AchievementLibrary.checkTraitsExplorer(40));
		AchievementLibrary.traits_explorer_40 = this.add(achievement2);
		Achievement achievement3 = new Achievement();
		achievement3.play_store_id = "?";
		achievement3.steam_id = "2_19";
		achievement3.id = "achievementTraitsExplorer60";
		achievement3.hidden = false;
		achievement3.icon = "ui/Icons/achievements/achievements_traitexplorer2";
		achievement3.group = "collection";
		achievement3.action = ((object _) => AchievementLibrary.checkTraitsExplorer(60));
		AchievementLibrary.traits_explorer_60 = this.add(achievement3);
		Achievement achievement4 = new Achievement();
		achievement4.play_store_id = "?";
		achievement4.steam_id = "2_20";
		achievement4.id = "achievementTraitsExplorer90";
		achievement4.hidden = false;
		achievement4.icon = "ui/Icons/achievements/achievements_traitexplorer3";
		achievement4.group = "collection";
		achievement4.action = ((object _) => AchievementLibrary.checkTraitsExplorer(90));
		AchievementLibrary.traits_explorer_90 = this.add(achievement4);
		Achievement achievement5 = new Achievement();
		achievement5.play_store_id = "?";
		achievement5.steam_id = "?";
		achievement5.id = "achievementTraitExplorerSubspecies";
		achievement5.hidden = false;
		achievement5.icon = "ui/Icons/achievements/achievements_traits_explorer_subspecies";
		achievement5.group = "collection";
		achievement5.action = ((object _) => AchievementLibrary.checkUnlockAugmentations(190, AssetManager.subspecies_traits));
		AchievementLibrary.trait_explorer_subspecies = this.add(achievement5);
		Achievement achievement6 = new Achievement();
		achievement6.play_store_id = "?";
		achievement6.steam_id = "?";
		achievement6.id = "achievementTraitExplorerCulture";
		achievement6.hidden = false;
		achievement6.icon = "ui/Icons/achievements/achievements_traits_explorer_culture";
		achievement6.group = "collection";
		achievement6.action = ((object _) => AchievementLibrary.checkUnlockAugmentations(70, AssetManager.culture_traits));
		AchievementLibrary.trait_explorer_culture = this.add(achievement6);
		Achievement achievement7 = new Achievement();
		achievement7.play_store_id = "?";
		achievement7.steam_id = "?";
		achievement7.id = "achievementTraitExplorerLanguage";
		achievement7.hidden = false;
		achievement7.icon = "ui/Icons/achievements/achievements_traits_explorer_language";
		achievement7.group = "collection";
		achievement7.action = ((object _) => AchievementLibrary.checkUnlockAugmentations(20, AssetManager.language_traits));
		AchievementLibrary.trait_explorer_language = this.add(achievement7);
		Achievement achievement8 = new Achievement();
		achievement8.play_store_id = "?";
		achievement8.steam_id = "?";
		achievement8.id = "achievementTraitExplorerClan";
		achievement8.hidden = false;
		achievement8.icon = "ui/Icons/achievements/achievements_traits_explorer_clan";
		achievement8.group = "collection";
		achievement8.action = ((object _) => AchievementLibrary.checkUnlockAugmentations(25, AssetManager.clan_traits));
		AchievementLibrary.trait_explorer_clan = this.add(achievement8);
		Achievement achievement9 = new Achievement();
		achievement9.play_store_id = "?";
		achievement9.steam_id = "?";
		achievement9.id = "achievementTraitExplorerReligion";
		achievement9.hidden = false;
		achievement9.icon = "ui/Icons/achievements/achievements_traits_explorer_religion";
		achievement9.group = "collection";
		achievement9.action = ((object _) => AchievementLibrary.checkUnlockAugmentations(33, AssetManager.religion_traits));
		AchievementLibrary.trait_explorer_religion = this.add(achievement9);
		Achievement achievement10 = new Achievement();
		achievement10.play_store_id = "?";
		achievement10.steam_id = "?";
		achievement10.id = "achievementEquipmentExplorer";
		achievement10.hidden = false;
		achievement10.icon = "ui/Icons/achievements/achievements_equipment_explorer";
		achievement10.group = "collection";
		achievement10.action = ((object _) => AchievementLibrary.checkUnlockAugmentations(80, AssetManager.items));
		AchievementLibrary.equipment_explorer = this.add(achievement10);
		Achievement achievement11 = new Achievement();
		achievement11.play_store_id = "?";
		achievement11.steam_id = "?";
		achievement11.id = "achievementGenesExplorer";
		achievement11.hidden = false;
		achievement11.icon = "ui/Icons/achievements/achievements_genes_explorer";
		achievement11.group = "collection";
		achievement11.action = ((object _) => AchievementLibrary.checkUnlockAugmentations(35, AssetManager.gene_library));
		AchievementLibrary.genes_explorer = this.add(achievement11);
		AchievementLibrary.creatures_explorer = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementCreaturesExplorer",
			hidden = true,
			icon = "ui/Icons/achievements/achievements_creatures_explorer",
			group = "collection",
			action = new AchievementCheck(AchievementLibrary.checkCreaturesExplorer)
		});
		Achievement achievement12 = new Achievement();
		achievement12.play_store_id = "?";
		achievement12.steam_id = "?";
		achievement12.id = "achievementPlotsExplorer";
		achievement12.hidden = false;
		achievement12.icon = "ui/Icons/achievements/achievements_plots_explorer";
		achievement12.group = "collection";
		achievement12.action = ((object _) => AchievementLibrary.checkUnlockAugmentations(20, AssetManager.plots_library));
		AchievementLibrary.plots_explorer = this.add(achievement12);
		AchievementLibrary.cursed_world = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementCursedWorld",
			hidden = true,
			icon = "ui/Icons/achievements/achievement_cursed_world",
			action = new AchievementCheck(AchievementLibrary.checkCursedWorld),
			group = "forbidden"
		});
		AchievementLibrary.boats_disposal = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementBoatsDisposal",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_boats_disposal",
			group = "destruction",
			action = new AchievementCheck(AchievementLibrary.checkBoatDisposal)
		});
		AchievementLibrary.engineered_evolution = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementEngineeredEvolution",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_engineered_evolution",
			group = "experiments"
		});
		AchievementLibrary.simple_stupid_genetics = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementSimpleStupidGenetics",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_simple_stupid_genetics",
			group = "experiments",
			action = new AchievementCheck(AchievementLibrary.checkSimpleStupidGenetics)
		});
		AchievementLibrary.fast_living = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementFastLiving",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_fast_living",
			group = "experiments",
			action = new AchievementCheck(AchievementLibrary.checkFastLiving)
		});
		AchievementLibrary.long_living = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementLongLiving",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_long_living",
			group = "experiments",
			action = new AchievementCheck(AchievementLibrary.checkLongLiving)
		});
		AchievementLibrary.ancient_war_of_geometry_and_evil = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementAncientWarOfGeometryAndEvil",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_ancient_war_of_geometry_and_evil",
			group = "civilizations",
			action = new AchievementCheck(AchievementLibrary.checkAncientWarOfGeometryAndEvil)
		});
		AchievementLibrary.cant_be_too_much = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementCantBeTooMuch",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_cant_be_too_much",
			group = "creation",
			action = new AchievementCheck(AchievementLibrary.checkCantBeTooMuch)
		});
		AchievementLibrary.zoo = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementZoo",
			hidden = true,
			icon = "ui/Icons/achievements/achievement_zoo",
			group = "civilizations",
			action = new AchievementCheck(AchievementLibrary.checkZoo)
		});
		AchievementLibrary.mindless_husk = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementMindlessHusk",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_mindless_husk",
			group = "experiments",
			action = new AchievementCheck(AchievementLibrary.checkMindlessHusk)
		});
		AchievementLibrary.master_weaver = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementMasterWeaver",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_master_weaver",
			group = "experiments",
			action = new AchievementCheck(AchievementLibrary.checkMasterWeaver)
		});
		AchievementLibrary.not_just_a_cult = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementNotJustACult",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_not_just_a_cult",
			group = "civilizations",
			action = new AchievementCheck(AchievementLibrary.checkNotJustACult)
		});
		AchievementLibrary.succession = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementSuccession",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_succession",
			group = "experiments"
		});
		AchievementLibrary.multiply_spoken = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementMultiplySpoken",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_multiply_spoken",
			group = "civilizations",
			action = new AchievementCheck(AchievementLibrary.checkMultiplySpoken)
		});
		AchievementLibrary.child_named_toto = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementChildNamedToto",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_child_named_toto",
			group = "experiments",
			action = new AchievementCheck(AchievementLibrary.checkChildNamedMakoMako)
		});
		AchievementLibrary.flick_it = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementFlickIt",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_flick_it",
			group = "experiments"
		});
		AchievementLibrary.segregator = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementSegregator",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_segregator",
			group = "creation",
			action = new AchievementCheck(AchievementLibrary.checkSegregator)
		});
		AchievementLibrary.eternal_chaos = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementEternalChaos",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_eternal_chaos",
			group = "nature",
			action = new AchievementCheck(AchievementLibrary.checkEternalChaos)
		});
		AchievementLibrary.minefield = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementMinefield",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_minefield",
			group = "nature",
			action = new AchievementCheck(AchievementLibrary.checkMinefield)
		});
		AchievementLibrary.godly_smithing = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementGodlySmithing",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_godly_smithing",
			group = "experiments"
		});
		AchievementLibrary.master_of_combat = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementMasterOfCombat",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_master_of_combat",
			group = "creatures",
			action = new AchievementCheck(AchievementLibrary.checkMasterOfCombat)
		});
		AchievementLibrary.clannibals = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementClannibals",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_clannibals",
			group = "creatures",
			action = new AchievementCheck(AchievementLibrary.checkClannibals)
		});
		AchievementLibrary.social_network = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementSocialNetwork",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_social_network",
			group = "miscellaneous"
		});
		AchievementLibrary.watch_your_mouth = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementWatchYourMouth",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_watch_your_mouth",
			group = "creatures",
			action = new AchievementCheck(AchievementLibrary.checkWatchYourMouth)
		});
		AchievementLibrary.clone_wars = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementCloneWars",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_clone_wars",
			group = "experiments",
			action = new AchievementCheck(AchievementLibrary.checkCloneWars)
		});
		AchievementLibrary.smelly_city = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementSmellyCity",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_smelly_city",
			group = "civilizations",
			action = new AchievementCheck(AchievementLibrary.checkSmellyCity)
		});
		AchievementLibrary.tldr = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementTLDR",
			locale_key = "achievement_tldr",
			hidden = true,
			icon = "ui/Icons/achievements/achievement_tldr",
			group = "exploration"
		});
		AchievementLibrary.not_on_my_watch = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementNotOnMyWatch",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_not_on_my_watch",
			group = "civilizations",
			action = new AchievementCheck(AchievementLibrary.checkNotOnMyWatch)
		});
		AchievementLibrary.may_i_interrupt = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementMayIInterrupt",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_may_i_interrupt",
			group = "civilizations",
			action = new AchievementCheck(AchievementLibrary.checkMayIInterrupt)
		});
		AchievementLibrary.ball_to_ball = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementBallToBall",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_ball_to_ball",
			group = "destruction",
			action = new AchievementCheck(AchievementLibrary.checkBallToBall)
		});
		AchievementLibrary.sword_with_shotgun = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementSwordWithShotgun",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_sword_with_shotgun",
			group = "creatures",
			action = new AchievementCheck(AchievementLibrary.checkSwordWithShotgun)
		});
		AchievementLibrary.back_to_beta_testing = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementBackToBetaTesting",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_back_to_beta_testing",
			group = "creatures",
			action = new AchievementCheck(AchievementLibrary.checkBackToBetaTesting)
		});
		AchievementLibrary.swarm = this.add(new Achievement
		{
			play_store_id = "?",
			steam_id = "?",
			id = "achievementSwarm",
			hidden = false,
			icon = "ui/Icons/achievements/achievement_swarm",
			group = "creatures",
			action = new AchievementCheck(AchievementLibrary.checkSwarm)
		});
		this.standaloneAchievements();
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x000355C0 File Offset: 0x000337C0
	private void standaloneAchievements()
	{
		AchievementLibrary.the_builder = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "1_7",
			id = "achievementTheBuilder",
			icon = "ui/Icons/achievements/achievements_thebuilder",
			group = "worlds"
		});
		AchievementLibrary.the_dwarf = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "1_8",
			id = "achievementTheDwarf",
			icon = "ui/Icons/achievements/achievements_thedwarf",
			group = "worlds"
		});
		AchievementLibrary.the_creator = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "1_9",
			id = "achievementTheCreator",
			icon = "ui/Icons/achievements/achievements_thecreator",
			group = "worlds"
		});
		AchievementLibrary.the_light = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "1_10",
			id = "achievementTheLight",
			icon = "ui/Icons/achievements/achievements_thelight",
			group = "worlds"
		});
		AchievementLibrary.the_sky = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "1_11",
			id = "achievementTheSky",
			icon = "ui/Icons/achievements/achievements_thesky",
			group = "worlds"
		});
		AchievementLibrary.the_land = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "1_12",
			id = "achievementTheLand",
			icon = "ui/Icons/achievements/achievements_theland",
			group = "worlds"
		});
		AchievementLibrary.the_sun = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "1_13",
			id = "achievementTheSun",
			icon = "ui/Icons/achievements/achievements_thesun",
			group = "worlds"
		});
		AchievementLibrary.the_moon = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "1_14",
			id = "achievementTheMoon",
			icon = "ui/Icons/achievements/achievements_themoon",
			group = "worlds"
		});
		AchievementLibrary.the_living = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "1_15",
			id = "achievementTheLiving",
			icon = "ui/Icons/achievements/achievements_theliving",
			group = "worlds"
		});
		AchievementLibrary.the_rest_day = this.add(new Achievement
		{
			play_store_id = "CgkIia6M98wfEAIQAg",
			steam_id = "1_16",
			id = "achievementTheRestDay",
			icon = "ui/Icons/achievements/achievements_restday",
			group = "worlds"
		});
		WorkshopAchievements.checkAchievements();
	}

	// Token: 0x060004E5 RID: 1253 RVA: 0x00035898 File Offset: 0x00033A98
	public override void post_init()
	{
		base.post_init();
		foreach (Achievement tAchievement in this.list)
		{
			Achievement achievement = tAchievement;
			if (achievement.locale_key == null)
			{
				achievement.locale_key = tAchievement.id.Underscore();
			}
			if (tAchievement.getSignal() != null)
			{
				tAchievement.has_signal = true;
			}
		}
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x00035914 File Offset: 0x00033B14
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (Achievement tAchievement in this.list)
		{
			this.addAsUnlockAssets(tAchievement, AssetManager.traits);
			this.addAsUnlockAssets(tAchievement, AssetManager.subspecies_traits);
			this.addAsUnlockAssets(tAchievement, AssetManager.culture_traits);
			this.addAsUnlockAssets(tAchievement, AssetManager.language_traits);
			this.addAsUnlockAssets(tAchievement, AssetManager.clan_traits);
			this.addAsUnlockAssets(tAchievement, AssetManager.religion_traits);
			this.addAsUnlockAssets(tAchievement, AssetManager.kingdoms_traits);
			this.addAsUnlockAssets(tAchievement, AssetManager.gene_library);
			this.addAsUnlockAssets(tAchievement, AssetManager.items);
			this.addAsUnlockAssets(tAchievement, AssetManager.actor_library);
			this.addAsUnlockAssets(tAchievement, AssetManager.plots_library);
		}
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x000359F0 File Offset: 0x00033BF0
	private void addAsUnlockAssets(Achievement pAchievement, ILibraryWithUnlockables pLibrary)
	{
		foreach (BaseUnlockableAsset tAsset in pLibrary.elements_list)
		{
			if (tAsset.unlocked_with_achievement && !(tAsset.achievement_id != pAchievement.id))
			{
				if (pAchievement.unlock_assets == null)
				{
					pAchievement.unlock_assets = new List<BaseUnlockableAsset>();
					pAchievement.unlocks_something = true;
				}
				pAchievement.unlock_assets.Add(tAsset);
			}
		}
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x00035A78 File Offset: 0x00033C78
	public static void unlock(string pID)
	{
		Achievement tAchievement = AssetManager.achievements.get(pID);
		if (tAchievement == null)
		{
			return;
		}
		AchievementLibrary.unlock(tAchievement);
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x00035A9C File Offset: 0x00033C9C
	public static void unlock(Achievement pAchievement)
	{
		if (WorldLawLibrary.world_law_cursed_world.isEnabled())
		{
			return;
		}
		SteamAchievements.TriggerAchievement(pAchievement.id);
		if (AchievementLibrary.isUnlocked(pAchievement))
		{
			return;
		}
		if (GameProgress.unlockAchievement(pAchievement.id))
		{
			AchievementPopup.show(pAchievement);
		}
		Analytics.LogEvent("Achievement", "id", pAchievement.id);
		MapBox.aye();
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x00035AF7 File Offset: 0x00033CF7
	public static bool isUnlocked(Achievement pAchievement)
	{
		return GameProgress.isAchievementUnlocked(pAchievement.id);
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x00035B04 File Offset: 0x00033D04
	public static bool isUnlocked(string pID)
	{
		return GameProgress.isAchievementUnlocked(pID);
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x00035B0C File Offset: 0x00033D0C
	private static bool checkTraitsExplorer(int pAmount)
	{
		int tUnlocked = 0;
		using (List<ActorTrait>.Enumerator enumerator = AssetManager.traits.list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isAvailable())
				{
					tUnlocked++;
				}
			}
		}
		return tUnlocked >= pAmount;
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x00035B70 File Offset: 0x00033D70
	private static bool checkUnlockAugmentations(int pAmount, ILibraryWithUnlockables pLibrary)
	{
		int tCounterUnlocked = 0;
		using (IEnumerator<BaseUnlockableAsset> enumerator = pLibrary.elements_list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isAvailable())
				{
					tCounterUnlocked++;
				}
			}
		}
		return tCounterUnlocked >= pAmount;
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x00035BCC File Offset: 0x00033DCC
	private static bool checkAntWorld(object pCheckData = null)
	{
		List<Actor> tUnits = World.world.kingdoms_wild.get("ants").units;
		if (tUnits.Count < 40)
		{
			return false;
		}
		int tBlack = 0;
		int tGreen = 0;
		int tRed = 0;
		int tBlue = 0;
		for (int i = 0; i < tUnits.Count; i++)
		{
			string id = tUnits[i].asset.id;
			if (!(id == "ant_black"))
			{
				if (!(id == "ant_blue"))
				{
					if (!(id == "ant_red"))
					{
						if (id == "ant_green")
						{
							tGreen++;
						}
					}
					else
					{
						tRed++;
					}
				}
				else
				{
					tBlue++;
				}
			}
			else
			{
				tBlack++;
			}
		}
		return tBlue >= 10 && tGreen >= 10 && tRed >= 10 && tBlack >= 10;
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x00035C9B File Offset: 0x00033E9B
	private static bool checkCursedWorld(object pCheckData = null)
	{
		return WorldLawLibrary.world_law_cursed_world.isEnabledRaw();
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x00035CA7 File Offset: 0x00033EA7
	private static bool checkBoatDisposal(object pCheckData = null)
	{
		return StatsHelper.getStat("statistics_boats_destroyed_by_magnet") >= 10L;
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x00035CBB File Offset: 0x00033EBB
	private static bool check10000Creatures(object pCheckData = null)
	{
		return World.world.game_stats.data.creaturesCreated >= 10000L;
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x00035CDC File Offset: 0x00033EDC
	private static bool checkManyBombs(object pCheckData = null)
	{
		return World.world.game_stats.data.bombsDropped >= 1000L;
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x00035D00 File Offset: 0x00033F00
	private static bool checkMegapolis(object pCheckData = null)
	{
		foreach (City tCity in World.world.cities)
		{
			if (!(tCity.getSpecies() != "human") && tCity.getPopulationPeople() >= 200)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x00035D70 File Offset: 0x00033F70
	private static bool check4RaceCities(object pCheckData = null)
	{
		if (World.world.cities.Count < 4)
		{
			return false;
		}
		bool tHumans = false;
		bool tOrcs = false;
		bool tElves = false;
		bool tDwarves = false;
		foreach (City city in World.world.cities)
		{
			string species = city.getSpecies();
			if (!(species == "human"))
			{
				if (!(species == "orc"))
				{
					if (!(species == "elf"))
					{
						if (species == "dwarf")
						{
							tDwarves = true;
						}
					}
					else
					{
						tElves = true;
					}
				}
				else
				{
					tOrcs = true;
				}
			}
			else
			{
				tHumans = true;
			}
		}
		return tHumans && tOrcs && tElves && tDwarves;
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x00035E38 File Offset: 0x00034038
	private static bool checkPiranhaLand(object pCheckData = null)
	{
		Actor tActor = (Actor)pCheckData;
		return !(tActor.asset.id != "piranha") && tActor.mustAvoidGround() && !tActor.current_tile.Type.liquid;
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x00035E84 File Offset: 0x00034084
	private static bool checkPrintHeart(object pCheckData = null)
	{
		return ((GodPower)pCheckData).printers_print == "heart";
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x00035EA0 File Offset: 0x000340A0
	internal static void checkSteamMapUploads()
	{
		long workshopUploads = World.world.game_stats.data.workshopUploads;
		if (workshopUploads >= 1L)
		{
			AchievementLibrary.the_builder.check(null);
		}
		if (workshopUploads >= 3L)
		{
			AchievementLibrary.the_dwarf.check(null);
		}
		if (workshopUploads >= 5L)
		{
			AchievementLibrary.the_creator.check(null);
		}
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x00035EF4 File Offset: 0x000340F4
	internal static void checkSteamMapDownloads(int pDownloads)
	{
		if (pDownloads >= 1)
		{
			AchievementLibrary.the_light.check(null);
		}
		if (pDownloads >= 2)
		{
			AchievementLibrary.the_sky.check(null);
		}
		if (pDownloads >= 3)
		{
			AchievementLibrary.the_land.check(null);
		}
		if (pDownloads >= 4)
		{
			AchievementLibrary.the_sun.check(null);
		}
		if (pDownloads >= 5)
		{
			AchievementLibrary.the_moon.check(null);
		}
		if (pDownloads >= 6)
		{
			AchievementLibrary.the_living.check(null);
		}
		if (pDownloads >= 7)
		{
			AchievementLibrary.the_rest_day.check(null);
		}
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x00035F71 File Offset: 0x00034171
	private static bool checkLifeIsASim(object pCheckData = null)
	{
		return Mathf.Ceil(Time.realtimeSinceStartup) / 3600f > 24f;
	}

	// Token: 0x060004FA RID: 1274 RVA: 0x00035F90 File Offset: 0x00034190
	private static bool checkTheDemon(object pCheckData = null)
	{
		return SelectedUnit.isSet() && !SelectedUnit.unit.hasDivineScar() && !(SelectedUnit.unit.asset.id != "demon") && SelectedUnit.unit.countTraits() >= 10;
	}

	// Token: 0x060004FB RID: 1275 RVA: 0x00035FE2 File Offset: 0x000341E2
	private static bool checkTheKing(object pCheckData = null)
	{
		return SelectedUnit.isSet() && !SelectedUnit.unit.hasDivineScar() && SelectedUnit.unit.isKing() && SelectedUnit.unit.countTraits() >= 20;
	}

	// Token: 0x060004FC RID: 1276 RVA: 0x0003601C File Offset: 0x0003421C
	private static bool checkTheAccomplished(object pCheckData = null)
	{
		return SelectedUnit.isSet() && !SelectedUnit.unit.hasDivineScar() && SelectedUnit.unit.hasTrait("veteran") && SelectedUnit.unit.hasTrait("mageslayer") && SelectedUnit.unit.hasTrait("dragonslayer") && SelectedUnit.unit.hasTrait("kingslayer");
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x00036090 File Offset: 0x00034290
	private static bool checkTheBroken(object pCheckData = null)
	{
		return SelectedUnit.isSet() && !SelectedUnit.unit.hasDivineScar() && SelectedUnit.unit.hasTrait("crippled") && SelectedUnit.unit.hasTrait("eyepatch") && SelectedUnit.unit.hasTrait("skin_burns");
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x000360EE File Offset: 0x000342EE
	private static bool checkMapCreations100(object pCheckData = null)
	{
		return World.world.game_stats.data.mapsCreated >= 100L;
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x0003610C File Offset: 0x0003430C
	private static bool checkMapCreations50(object pCheckData = null)
	{
		return World.world.game_stats.data.mapsCreated >= 50L;
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x0003612A File Offset: 0x0003432A
	private static bool checkMapCreations5(object pCheckData = null)
	{
		return World.world.game_stats.data.mapsCreated >= 5L;
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x00036148 File Offset: 0x00034348
	private static bool checkTheHell(object pCheckData = null)
	{
		float tCount = (float)(TopTileLibrary.infernal_high.hashset.Count + TopTileLibrary.infernal_low.hashset.Count);
		if (tCount == 0f)
		{
			return false;
		}
		float tTotalLand = (float)World.world.tiles_list.Length;
		return tCount / tTotalLand >= 0.666f && World.world.kingdoms_wild.get("demon").units.Count >= 66;
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x000361C0 File Offset: 0x000343C0
	private static bool checkLetsNot(object pCheckData = null)
	{
		float num = (float)(TopTileLibrary.wasteland_high.hashset.Count + TopTileLibrary.wasteland_low.hashset.Count);
		float tTotalLand = (float)World.world.tiles_list.Length;
		return num / tTotalLand >= 0.9f;
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x00036208 File Offset: 0x00034408
	private static bool checkWorldWar(object pCheckData = null)
	{
		int tKingdomsAtWarCount = 0;
		using (IEnumerator<Kingdom> enumerator = World.world.kingdoms.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.hasEnemies())
				{
					tKingdomsAtWarCount++;
					if (tKingdomsAtWarCount >= 10)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x0003626C File Offset: 0x0003446C
	private static bool checkPlanetOfTheApes(object pCheckData = null)
	{
		float num = (float)(TopTileLibrary.wasteland_high.hashset.Count + TopTileLibrary.wasteland_low.hashset.Count);
		float tTotalJungles = (float)(TopTileLibrary.jungle_high.hashset.Count + TopTileLibrary.jungle_low.hashset.Count);
		float tTotalLand = (float)World.world.tiles_list.Length;
		float num2 = num / tTotalLand;
		float tPercentJungles = tTotalJungles / tTotalLand;
		float tTotalMonkeyWorld = num2 + tPercentJungles;
		return World.world.kingdoms_wild.get("monkey").units.Count >= 100 && tTotalMonkeyWorld > 0.8f;
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x000362FE File Offset: 0x000344FE
	private static bool checkGreatPlague(object pCheckData = null)
	{
		return World.world.map_stats.current_infected_plague >= 1000L;
	}

	// Token: 0x06000506 RID: 1286 RVA: 0x0003631C File Offset: 0x0003451C
	private static bool checkRainTornado(object pCheckData = null)
	{
		return !(World.world.map_stats.world_age_id != "age_tears") && World.world.stack_effects.get("fx_tornado").getList().Count >= 100;
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x0003636C File Offset: 0x0003456C
	private static bool checkSimpleStupidGenetics(object pCheckData = null)
	{
		Subspecies tSubspecies = SelectedMetas.selected_subspecies;
		if (tSubspecies == null)
		{
			return false;
		}
		List<Chromosome> chromosomes = tSubspecies.nucleus.chromosomes;
		GeneAsset tGeneToCompare = null;
		foreach (Chromosome chromosome in chromosomes)
		{
			foreach (GeneAsset tGene in chromosome.genes)
			{
				if (!tGene.is_empty)
				{
					if (tGeneToCompare == null)
					{
						tGeneToCompare = tGene;
					}
					if (tGeneToCompare != tGene)
					{
						return false;
					}
				}
			}
		}
		return true;
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x00036424 File Offset: 0x00034624
	private static bool checkFastLiving(object pCheckData = null)
	{
		Subspecies tSubspecies = SelectedMetas.selected_subspecies;
		if (tSubspecies == null)
		{
			return false;
		}
		int tFastLivingLifespan = 2;
		float tLifespanBase = tSubspecies.base_stats["lifespan"];
		float tLifespanMale = tSubspecies.base_stats["lifespan"] + tLifespanBase;
		float tLifespanFemale = tSubspecies.base_stats_female["lifespan"] + tLifespanBase;
		return tLifespanMale <= (float)tFastLivingLifespan && tLifespanFemale <= (float)tFastLivingLifespan;
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x00036484 File Offset: 0x00034684
	private static bool checkLongLiving(object pCheckData = null)
	{
		Subspecies tSubspecies = SelectedMetas.selected_subspecies;
		if (tSubspecies == null)
		{
			return false;
		}
		int tLongLivingLifespan = 3000;
		float tLifespanBase = tSubspecies.base_stats["lifespan"];
		float tLifespanMale = tSubspecies.base_stats_male["lifespan"] + tLifespanBase;
		float tLifespanFemale = tSubspecies.base_stats_female["lifespan"] + tLifespanBase;
		return tLifespanMale >= (float)tLongLivingLifespan && tLifespanFemale >= (float)tLongLivingLifespan;
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x000364E8 File Offset: 0x000346E8
	private static bool checkAncientWarOfGeometryAndEvil(object pCheckData = null)
	{
		foreach (War tWar in World.world.wars)
		{
			if (!tWar.hasEnded())
			{
				bool tHasAngleKingdom = false;
				bool tHasDemonKingdom = false;
				foreach (Kingdom kingdom in tWar.getAttackers())
				{
					if (kingdom.getSpecies() == "angle")
					{
						tHasAngleKingdom = true;
					}
					if (kingdom.getSpecies() == "demon")
					{
						tHasDemonKingdom = true;
					}
				}
				foreach (Kingdom kingdom2 in tWar.getDefenders())
				{
					if (kingdom2.getSpecies() == "angle")
					{
						tHasAngleKingdom = true;
					}
					if (kingdom2.getSpecies() == "demon")
					{
						tHasDemonKingdom = true;
					}
				}
				if (tHasAngleKingdom && tHasDemonKingdom)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x00036618 File Offset: 0x00034818
	private static bool checkCantBeTooMuch(object pCheckData = null)
	{
		int tNeedAmount = 10;
		int tAmount = 0;
		using (IEnumerator<Building> enumerator = World.world.buildings.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!(enumerator.Current.asset.id != "monolith"))
				{
					tAmount++;
				}
			}
		}
		return tAmount >= tNeedAmount;
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x00036688 File Offset: 0x00034888
	private static bool checkZoo(object pCheckData = null)
	{
		bool result;
		using (ListPool<string> tList = new ListPool<string>())
		{
			using (IEnumerator<City> enumerator = World.world.cities.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					MetaObject<CityData> metaObject = enumerator.Current;
					tList.Clear();
					foreach (Actor actor in metaObject.units)
					{
						string tId = actor.asset.id;
						if (!tList.Contains(tId))
						{
							tList.Add(tId);
						}
					}
					if (tList.Count < 33)
					{
						return false;
					}
					return true;
				}
			}
			result = false;
		}
		return result;
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x00036764 File Offset: 0x00034964
	private static bool checkMindlessHusk(object pCheckData = null)
	{
		Actor tActor = (Actor)pCheckData;
		UtilityBasedDecisionSystem decision_system = DecisionHelper.decision_system;
		int tAmount = decision_system.getCounter();
		DecisionAsset[] tAssets = decision_system.getActions();
		for (int i = 0; i < tAmount; i++)
		{
			DecisionAsset tAsset = tAssets[i];
			if (tActor.isDecisionEnabled(tAsset.decision_index))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x000367B0 File Offset: 0x000349B0
	private static bool checkMasterWeaver(object pCheckData = null)
	{
		Subspecies tSubspecies = SelectedMetas.selected_subspecies;
		if (tSubspecies == null)
		{
			return false;
		}
		if (tSubspecies.getActorAsset().id != "butterfly")
		{
			return false;
		}
		using (List<Chromosome>.Enumerator enumerator = tSubspecies.nucleus.chromosomes.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.isAllLociSynergy())
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x00036834 File Offset: 0x00034A34
	private static bool checkNotJustACult(object pCheckData = null)
	{
		return ((Religion)pCheckData).countUnits() >= 7777;
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x0003684B File Offset: 0x00034A4B
	private static bool checkMultiplySpoken(object pCheckData = null)
	{
		return ((Language)pCheckData).countUnits() >= 5555;
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x00036862 File Offset: 0x00034A62
	private static bool checkChildNamedMakoMako(object pCheckData = null)
	{
		return !((string)pCheckData != "Mako Mako");
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x00036879 File Offset: 0x00034A79
	private static bool checkSegregator(object pCheckData = null)
	{
		return World.world.game_stats.data.wallsPlaced >= 10000L;
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x0003689A File Offset: 0x00034A9A
	private static bool checkEternalChaos(object pCheckData = null)
	{
		return !(World.world_era.id != "age_chaos") && (float)Date.getYearsSince(World.world.map_stats.same_world_age_started_at) >= 1000f;
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x000368D3 File Offset: 0x00034AD3
	private static bool checkMinefield(object pCheckData = null)
	{
		return WorldLawLibrary.world_law_exploding_mushrooms.isEnabled() && (float)Date.getYearsSince(World.world.map_stats.exploding_mushrooms_enabled_at) >= 1000f;
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x00036904 File Offset: 0x00034B04
	private static bool checkWatchYourMouth(object pCheckData = null)
	{
		Actor tActor = (Actor)pCheckData;
		return tActor.isBaby() && tActor.hasStatus("swearing");
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x00036934 File Offset: 0x00034B34
	private static bool checkCloneWars(object pCheckData = null)
	{
		ValueTuple<Actor, Actor> valueTuple = (ValueTuple<Actor, Actor>)pCheckData;
		Actor tMain = valueTuple.Item1;
		Actor tTarget = valueTuple.Item2;
		return tMain.isAlive() && tTarget.isAlive() && (tMain.isSameClones(tTarget) || tMain.isClonedFrom(tTarget) || tTarget.isClonedFrom(tMain));
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x00036988 File Offset: 0x00034B88
	private static bool checkCreaturesExplorer(object pCheckData = null)
	{
		int tCountTotal = 0;
		int tCountExplored = 0;
		foreach (ActorAsset tAsset in AssetManager.actor_library.list)
		{
			if (tAsset.needs_to_be_explored && !tAsset.isTemplateAsset())
			{
				tCountTotal++;
				if (tAsset.isAvailable())
				{
					tCountExplored++;
				}
			}
		}
		return tCountExplored >= 52;
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x00036A04 File Offset: 0x00034C04
	private static bool checkMasterOfCombat(object pCheckData = null)
	{
		Actor tActor = (Actor)pCheckData;
		if (tActor == null)
		{
			return false;
		}
		if (!tActor.isAlive())
		{
			return false;
		}
		if (tActor.hasDivineScar())
		{
			return false;
		}
		int tCombatTraitsAmount = 0;
		using (IEnumerator<ActorTrait> enumerator = tActor.getTraits().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.in_training_dummy_combat_pot)
				{
					tCombatTraitsAmount++;
				}
			}
		}
		return tCombatTraitsAmount >= 5;
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x00036A80 File Offset: 0x00034C80
	private static bool checkClannibals(object pCheckData = null)
	{
		ValueTuple<Actor, Clan> valueTuple = (ValueTuple<Actor, Clan>)pCheckData;
		Actor tMain = valueTuple.Item1;
		Clan tTargetClan = valueTuple.Item2;
		return tMain.hasClan() && tTargetClan != null && tMain.clan.id == tTargetClan.id;
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x00036AC3 File Offset: 0x00034CC3
	private static bool checkSmellyCity(object pCheckData = null)
	{
		return ((City)pCheckData).getResourcesAmount("fertilizer") >= 999;
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x00036ADF File Offset: 0x00034CDF
	private static bool checkNotOnMyWatch(object pCheckData = null)
	{
		return ((Actor)pCheckData).hasStatus("being_suspicious");
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x00036AF6 File Offset: 0x00034CF6
	private static bool checkMayIInterrupt(object pCheckData = null)
	{
		return !((string)pCheckData != "socialize_do_talk");
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x00036B0D File Offset: 0x00034D0D
	private static bool checkBallToBall(object pCheckData = null)
	{
		return !(((Actor)pCheckData).asset.id != "armadillo");
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x00036B30 File Offset: 0x00034D30
	private static bool checkSwordWithShotgun(object pCheckData = null)
	{
		Actor tActor = (Actor)pCheckData;
		if (tActor.asset.id != "crystal_sword")
		{
			return false;
		}
		if (!tActor.hasWeapon())
		{
			return false;
		}
		Item tWeapon = tActor.getWeapon();
		return !(tWeapon.getAsset().id != "shotgun") && !tWeapon.hasMod("divine_rune");
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x00036B98 File Offset: 0x00034D98
	private static bool checkBackToBetaTesting(object pCheckData = null)
	{
		Actor tActor = (Actor)pCheckData;
		return !(tActor.asset.id != "beetle") && !(tActor.current_tile.Type.biome_id != "biome_singularity");
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x00036BE4 File Offset: 0x00034DE4
	private static bool checkSwarm(object pCheckData = null)
	{
		Subspecies tSubspecies = (Subspecies)pCheckData;
		return !tSubspecies.hasPopulationLimit() && tSubspecies.hasTrait("high_fecundity") && tSubspecies.hasTrait("reproduction_spores") && tSubspecies.hasTrait("gestation_short") && tSubspecies.hasTrait("rapid_aging");
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x00036C3F File Offset: 0x00034E3F
	public static void checkCityAchievements(City pCity)
	{
		AchievementLibrary.zoo.checkBySignal(null);
		AchievementLibrary.four_race_cities.check(pCity);
		AchievementLibrary.smelly_city.check(pCity);
		AchievementLibrary.megapolis.checkBySignal(null);
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x00036C70 File Offset: 0x00034E70
	public static void checkUnitAchievements(Actor pActor)
	{
		AchievementLibrary.creatures_explorer.check(null);
		AchievementLibrary.the_broken.check(null);
		AchievementLibrary.the_demon.check(null);
		AchievementLibrary.the_king.check(null);
		AchievementLibrary.the_accomplished.check(null);
		AchievementLibrary.watch_your_mouth.check(pActor);
		AchievementLibrary.master_of_combat.check(pActor);
		AchievementLibrary.sword_with_shotgun.check(pActor);
		AchievementLibrary.ninja_turtle.check(pActor);
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x00036CE9 File Offset: 0x00034EE9
	public static void checkSubspeciesAchievements(Subspecies pSubspecies)
	{
		AchievementLibrary.creatures_explorer.check(null);
		AchievementLibrary.swarm.checkBySignal(pSubspecies);
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x00036D02 File Offset: 0x00034F02
	public static void login()
	{
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x00036D04 File Offset: 0x00034F04
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (Achievement tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
			this.checkLocale(tAsset, tAsset.getDescriptionID());
		}
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x00036D70 File Offset: 0x00034F70
	public static int countUnlocked()
	{
		int tUnlocked = 0;
		using (List<Achievement>.Enumerator enumerator = AssetManager.achievements.list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (AchievementLibrary.isUnlocked(enumerator.Current.id))
				{
					tUnlocked++;
				}
			}
		}
		return tUnlocked;
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x00036DD4 File Offset: 0x00034FD4
	public static bool isAllUnlocked()
	{
		int num = AchievementLibrary.countUnlocked();
		int tMax = AssetManager.achievements.list.Count;
		return num >= tMax;
	}

	// Token: 0x0400051C RID: 1308
	private const int ZOO_SPECIES_NEED = 33;

	// Token: 0x0400051D RID: 1309
	private const int SPECIES_EXPLORER_ASSETS = 52;

	// Token: 0x0400051E RID: 1310
	private const int TRAITS_EXPLORER_AMOUNT_FIRST = 40;

	// Token: 0x0400051F RID: 1311
	private const int TRAITS_EXPLORER_AMOUNT_SECOND = 60;

	// Token: 0x04000520 RID: 1312
	private const int TRAITS_EXPLORER_AMOUNT_THIRD = 90;

	// Token: 0x04000521 RID: 1313
	private const int SUBSPECIES_TRAITS_EXPLORER_AMOUNT = 190;

	// Token: 0x04000522 RID: 1314
	private const int CULTURE_TRAITS_EXPLORER_AMOUNT = 70;

	// Token: 0x04000523 RID: 1315
	private const int LANGUAGE_TRAITS_EXPLORER_AMOUNT = 20;

	// Token: 0x04000524 RID: 1316
	private const int CLAN_TRAITS_EXPLORER_AMOUNT = 25;

	// Token: 0x04000525 RID: 1317
	private const int RELIGION_TRAITS_EXPLORER_AMOUNT = 33;

	// Token: 0x04000526 RID: 1318
	private const int EQUIPMENT_EXPLORER_AMOUNT = 80;

	// Token: 0x04000527 RID: 1319
	private const int GENES_EXPLORER_AMOUNT = 35;

	// Token: 0x04000528 RID: 1320
	private const int PLOTS_EXPLORER_AMOUNT = 20;

	// Token: 0x04000529 RID: 1321
	private const string ONOMASTICS_NAME_FOR_ACHIEVEMENT = "Mako Mako";

	// Token: 0x0400052A RID: 1322
	private const int NOT_JUST_A_CULT_UNITS = 7777;

	// Token: 0x0400052B RID: 1323
	private const int MULTIPLY_SPOKEN_UNITS = 5555;

	// Token: 0x0400052C RID: 1324
	public static Achievement lava_strike;

	// Token: 0x0400052D RID: 1325
	public static Achievement baby_tornado;

	// Token: 0x0400052E RID: 1326
	public static Achievement rain_tornado;

	// Token: 0x0400052F RID: 1327
	public static Achievement many_bombs;

	// Token: 0x04000530 RID: 1328
	public static Achievement megapolis;

	// Token: 0x04000531 RID: 1329
	public static Achievement wilhelm_scream;

	// Token: 0x04000532 RID: 1330
	public static Achievement burger;

	// Token: 0x04000533 RID: 1331
	public static Achievement mayday;

	// Token: 0x04000534 RID: 1332
	public static Achievement destroy_worldbox;

	// Token: 0x04000535 RID: 1333
	public static Achievement custom_world;

	// Token: 0x04000536 RID: 1334
	public static Achievement four_race_cities;

	// Token: 0x04000537 RID: 1335
	public static Achievement piranha_land;

	// Token: 0x04000538 RID: 1336
	public static Achievement print_heart;

	// Token: 0x04000539 RID: 1337
	public static Achievement sacrifice;

	// Token: 0x0400053A RID: 1338
	public static Achievement final_resolution;

	// Token: 0x0400053B RID: 1339
	public static Achievement tnt_and_heat;

	// Token: 0x0400053C RID: 1340
	public static Achievement god_finger_lightning;

	// Token: 0x0400053D RID: 1341
	public static Achievement ten_thousands_creatures;

	// Token: 0x0400053E RID: 1342
	public static Achievement ant_world;

	// Token: 0x0400053F RID: 1343
	public static Achievement traits_explorer_40;

	// Token: 0x04000540 RID: 1344
	public static Achievement traits_explorer_60;

	// Token: 0x04000541 RID: 1345
	public static Achievement traits_explorer_90;

	// Token: 0x04000542 RID: 1346
	public static Achievement trait_explorer_subspecies;

	// Token: 0x04000543 RID: 1347
	public static Achievement trait_explorer_culture;

	// Token: 0x04000544 RID: 1348
	public static Achievement trait_explorer_language;

	// Token: 0x04000545 RID: 1349
	public static Achievement trait_explorer_clan;

	// Token: 0x04000546 RID: 1350
	public static Achievement trait_explorer_religion;

	// Token: 0x04000547 RID: 1351
	public static Achievement equipment_explorer;

	// Token: 0x04000548 RID: 1352
	public static Achievement genes_explorer;

	// Token: 0x04000549 RID: 1353
	public static Achievement creatures_explorer;

	// Token: 0x0400054A RID: 1354
	public static Achievement plots_explorer;

	// Token: 0x0400054B RID: 1355
	public static Achievement the_builder;

	// Token: 0x0400054C RID: 1356
	public static Achievement the_dwarf;

	// Token: 0x0400054D RID: 1357
	public static Achievement the_creator;

	// Token: 0x0400054E RID: 1358
	public static Achievement the_light;

	// Token: 0x0400054F RID: 1359
	public static Achievement the_sky;

	// Token: 0x04000550 RID: 1360
	public static Achievement the_land;

	// Token: 0x04000551 RID: 1361
	public static Achievement the_sun;

	// Token: 0x04000552 RID: 1362
	public static Achievement the_moon;

	// Token: 0x04000553 RID: 1363
	public static Achievement the_living;

	// Token: 0x04000554 RID: 1364
	public static Achievement the_rest_day;

	// Token: 0x04000555 RID: 1365
	public static Achievement life_is_a_sim;

	// Token: 0x04000556 RID: 1366
	public static Achievement gen_5_worlds;

	// Token: 0x04000557 RID: 1367
	public static Achievement gen_50_worlds;

	// Token: 0x04000558 RID: 1368
	public static Achievement gen_100_worlds;

	// Token: 0x04000559 RID: 1369
	public static Achievement the_corrupted_trees;

	// Token: 0x0400055A RID: 1370
	public static Achievement the_hell;

	// Token: 0x0400055B RID: 1371
	public static Achievement lets_not;

	// Token: 0x0400055C RID: 1372
	public static Achievement world_war;

	// Token: 0x0400055D RID: 1373
	public static Achievement planet_of_apes;

	// Token: 0x0400055E RID: 1374
	public static Achievement super_mushroom;

	// Token: 0x0400055F RID: 1375
	public static Achievement the_princess;

	// Token: 0x04000560 RID: 1376
	public static Achievement oh_my_crab;

	// Token: 0x04000561 RID: 1377
	public static Achievement tornado;

	// Token: 0x04000562 RID: 1378
	public static Achievement god_mode;

	// Token: 0x04000563 RID: 1379
	public static Achievement greg;

	// Token: 0x04000564 RID: 1380
	public static Achievement ninja_turtle;

	// Token: 0x04000565 RID: 1381
	public static Achievement great_plague;

	// Token: 0x04000566 RID: 1382
	public static Achievement no_hope_only_mush;

	// Token: 0x04000567 RID: 1383
	public static Achievement touch_the_grass;

	// Token: 0x04000568 RID: 1384
	public static Achievement the_broken;

	// Token: 0x04000569 RID: 1385
	public static Achievement the_king;

	// Token: 0x0400056A RID: 1386
	public static Achievement the_demon;

	// Token: 0x0400056B RID: 1387
	public static Achievement the_accomplished;

	// Token: 0x0400056C RID: 1388
	public static Achievement cursed_world;

	// Token: 0x0400056D RID: 1389
	public static Achievement boats_disposal;

	// Token: 0x0400056E RID: 1390
	public static Achievement engineered_evolution;

	// Token: 0x0400056F RID: 1391
	public static Achievement simple_stupid_genetics;

	// Token: 0x04000570 RID: 1392
	public static Achievement fast_living;

	// Token: 0x04000571 RID: 1393
	public static Achievement long_living;

	// Token: 0x04000572 RID: 1394
	public static Achievement ancient_war_of_geometry_and_evil;

	// Token: 0x04000573 RID: 1395
	public static Achievement cant_be_too_much;

	// Token: 0x04000574 RID: 1396
	public static Achievement zoo;

	// Token: 0x04000575 RID: 1397
	public static Achievement mindless_husk;

	// Token: 0x04000576 RID: 1398
	public static Achievement master_weaver;

	// Token: 0x04000577 RID: 1399
	public static Achievement not_just_a_cult;

	// Token: 0x04000578 RID: 1400
	public static Achievement succession;

	// Token: 0x04000579 RID: 1401
	public static Achievement multiply_spoken;

	// Token: 0x0400057A RID: 1402
	public static Achievement child_named_toto;

	// Token: 0x0400057B RID: 1403
	public static Achievement flick_it;

	// Token: 0x0400057C RID: 1404
	public static Achievement segregator;

	// Token: 0x0400057D RID: 1405
	public static Achievement swarm;

	// Token: 0x0400057E RID: 1406
	public static Achievement eternal_chaos;

	// Token: 0x0400057F RID: 1407
	public static Achievement minefield;

	// Token: 0x04000580 RID: 1408
	public static Achievement godly_smithing;

	// Token: 0x04000581 RID: 1409
	public static Achievement master_of_combat;

	// Token: 0x04000582 RID: 1410
	public static Achievement clannibals;

	// Token: 0x04000583 RID: 1411
	public static Achievement social_network;

	// Token: 0x04000584 RID: 1412
	public static Achievement not_on_my_watch;

	// Token: 0x04000585 RID: 1413
	public static Achievement may_i_interrupt;

	// Token: 0x04000586 RID: 1414
	public static Achievement watch_your_mouth;

	// Token: 0x04000587 RID: 1415
	public static Achievement smelly_city;

	// Token: 0x04000588 RID: 1416
	public static Achievement ball_to_ball;

	// Token: 0x04000589 RID: 1417
	public static Achievement back_to_beta_testing;

	// Token: 0x0400058A RID: 1418
	public static Achievement clone_wars;

	// Token: 0x0400058B RID: 1419
	public static Achievement sword_with_shotgun;

	// Token: 0x0400058C RID: 1420
	public static Achievement tldr;

	// Token: 0x0400058D RID: 1421
	public const float LIFE_IS_SIM_HOURS = 24f;
}
