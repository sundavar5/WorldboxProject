using System;

namespace ai.behaviours
{
	// Token: 0x02000943 RID: 2371
	public class BehaviourTaskActorLibrary : AssetLibrary<BehaviourTaskActor>
	{
		// Token: 0x06004623 RID: 17955 RVA: 0x001D4AE4 File Offset: 0x001D2CE4
		public override void init()
		{
			base.init();
			this.initTasksMobs();
			this.initTasksSocializing();
			this.initTasksSubspeciesTraits();
			this.initTasksReproductionSexual();
			this.initTasksReproductionAsexual();
			this.initTasksChildren();
			this.initTasksStatusRelated();
			this.initTasksKings();
			this.initTasksLeaders();
			this.initTasksWarriors();
			this.initTasksSleep();
			this.initTasksPoop();
			this.initTasksThinkingReflectionHappiness();
			this.initTasksClanLeader();
			this.initTasksBoats();
			this.initTasksDragons();
			this.initTasksFingers();
			this.initTasksUFOs();
			BehaviourTaskActor behaviourTaskActor = new BehaviourTaskActor();
			behaviourTaskActor.id = "nothing";
			behaviourTaskActor.cancellable_by_socialize = true;
			behaviourTaskActor.cancellable_by_reproduction = true;
			behaviourTaskActor.locale_key = "task_unit_nothing";
			BehaviourTaskActor pAsset = behaviourTaskActor;
			this.t = behaviourTaskActor;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconClock");
			BehaviourTaskActor behaviourTaskActor2 = new BehaviourTaskActor();
			behaviourTaskActor2.id = "fighting";
			behaviourTaskActor2.in_combat = true;
			behaviourTaskActor2.locale_key = "task_unit_fight";
			pAsset = behaviourTaskActor2;
			this.t = behaviourTaskActor2;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconWar");
			this.t.addBeh(new BehFightCheckEnemyIsOk());
			this.t.addBeh(new BehGoToActorTarget(GoToActorTargetType.SameTile, true, true, true, 2f, true, true));
			this.t.addBeh(new BehRestartTask());
			BehaviourTaskActor behaviourTaskActor3 = new BehaviourTaskActor();
			behaviourTaskActor3.id = "move_from_block";
			behaviourTaskActor3.move_from_block = true;
			behaviourTaskActor3.ignore_fight_check = true;
			behaviourTaskActor3.locale_key = "task_unit_move";
			pAsset = behaviourTaskActor3;
			this.t = behaviourTaskActor3;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconArrowDestination");
			this.t.addBeh(new BehMoveAwayFromBlock());
			this.t.addBeh(new BehGoToTileTarget
			{
				walk_on_water = true,
				walk_on_blocks = true
			});
			BehaviourTaskActor behaviourTaskActor4 = new BehaviourTaskActor();
			behaviourTaskActor4.id = "swim_to_island";
			behaviourTaskActor4.ignore_fight_check = true;
			behaviourTaskActor4.locale_key = "task_unit_move";
			pAsset = behaviourTaskActor4;
			this.t = behaviourTaskActor4;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconTileShallowWater");
			this.t.addBeh(new BehGoToStablePlace());
			this.t.addBeh(new BehGoOrSwimToTileTarget());
			BehaviourTaskActor behaviourTaskActor5 = new BehaviourTaskActor();
			behaviourTaskActor5.id = "move_to_water";
			behaviourTaskActor5.ignore_fight_check = true;
			behaviourTaskActor5.locale_key = "task_unit_move";
			pAsset = behaviourTaskActor5;
			this.t = behaviourTaskActor5;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconTileShallowWater");
			this.t.addBeh(new BehGoToStablePlace());
			this.t.addBeh(new BehGoOrSwimToTileTarget());
			BehaviourTaskActor behaviourTaskActor6 = new BehaviourTaskActor();
			behaviourTaskActor6.id = "check_if_stuck_on_small_land";
			behaviourTaskActor6.ignore_fight_check = true;
			behaviourTaskActor6.locale_key = "task_unit_move";
			pAsset = behaviourTaskActor6;
			this.t = behaviourTaskActor6;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconTileSoil");
			this.t.addBeh(new BehCheckIfOnSmallLand());
			this.t.addBeh(new BehWalkIntoWaterCorner());
			this.t.addBeh(new BehGoOrSwimToTileTarget());
			BehaviourTaskActor behaviourTaskActor7 = new BehaviourTaskActor();
			behaviourTaskActor7.id = "pollinate";
			behaviourTaskActor7.locale_key = "task_unit_pollinate";
			pAsset = behaviourTaskActor7;
			this.t = behaviourTaskActor7;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconBee");
			this.t.addBeh(new BehBeeCheckHome());
			this.t.addBeh(new BehFindBuilding("type_flower", true, false));
			this.t.addBeh(new BehGetTargetBuildingMainTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(1f, 3f, true));
			this.t.addBeh(new BehPollinate());
			this.t.addBeh(new BehBeeCheckReturnHome());
			this.t.addBeh(new BehGetTargetBuildingMainTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehBeeReturnHome());
			BehaviourTaskActor behaviourTaskActor8 = new BehaviourTaskActor();
			behaviourTaskActor8.id = "bee_find_hive";
			behaviourTaskActor8.locale_key = "task_unit_bee_find_hive";
			pAsset = behaviourTaskActor8;
			this.t = behaviourTaskActor8;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconBeehive");
			this.t.addBeh(new BehBeeCheckNoHome());
			this.t.addBeh(new BehFindBuilding("type_hive", false, false));
			this.t.addBeh(new BehGetTargetBuildingMainTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehBeeJoinHive());
			this.t.addBeh(new BehBeeReturnHome());
			BehaviourTaskActor behaviourTaskActor9 = new BehaviourTaskActor();
			behaviourTaskActor9.id = "bee_create_hive";
			behaviourTaskActor9.locale_key = "task_unit_bee_create_hive";
			pAsset = behaviourTaskActor9;
			this.t = behaviourTaskActor9;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconBeehive");
			this.t.addBeh(new BehBeeCheckNoHome());
			this.t.addBeh(new BehFindTile(TileFinderType.Biome));
			this.t.addBeh(new BehGoToTileTarget());
			for (int i = 0; i < 5; i++)
			{
				this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Tile, null, 0.5f, 40f, true, true));
			}
			this.t.addBeh(new BehBeeCreateHive());
			this.t.addBeh(new BehBeeJoinHive());
			this.t.addBeh(new BehBeeReturnHome());
			BehaviourTaskActor behaviourTaskActor10 = new BehaviourTaskActor();
			behaviourTaskActor10.id = "random_move";
			behaviourTaskActor10.locale_key = "task_unit_move";
			pAsset = behaviourTaskActor10;
			this.t = behaviourTaskActor10;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconArrowDestination");
			this.t.addBeh(new BehFindRandomTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(0f, 1f, false));
			BehaviourTaskActor behaviourTaskActor11 = new BehaviourTaskActor();
			behaviourTaskActor11.id = "random_fun_move";
			behaviourTaskActor11.locale_key = "task_unit_move";
			pAsset = behaviourTaskActor11;
			this.t = behaviourTaskActor11;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconArrowDestination");
			for (int j = 0; j < 6; j++)
			{
				this.t.addBeh(new BehFindRandomNeighbourTile());
				this.t.addBeh(new BehGoToTileTarget());
				this.t.addBeh(new BehRandomWait(0f, 0.01f, false));
			}
			BehaviourTaskActor behaviourTaskActor12 = new BehaviourTaskActor();
			behaviourTaskActor12.id = "run_away";
			behaviourTaskActor12.speed_multiplier = 2f;
			behaviourTaskActor12.ignore_fight_check = true;
			behaviourTaskActor12.locale_key = "task_unit_flee";
			pAsset = behaviourTaskActor12;
			this.t = behaviourTaskActor12;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/actor_traits/iconAgile");
			this.t.addBeh(new BehFindRandomFarTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(0f, 1f, false));
			BehaviourTaskActor behaviourTaskActor13 = new BehaviourTaskActor();
			behaviourTaskActor13.id = "run_away_from_carnivore";
			behaviourTaskActor13.speed_multiplier = 2f;
			behaviourTaskActor13.ignore_fight_check = true;
			behaviourTaskActor13.locale_key = "task_unit_flee";
			pAsset = behaviourTaskActor13;
			this.t = behaviourTaskActor13;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/actor_traits/iconAgile");
			this.t.addBeh(new BehFindRandomFarTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(0f, 1f, false));
			BehaviourTaskActor behaviourTaskActor14 = new BehaviourTaskActor();
			behaviourTaskActor14.id = "print_start";
			pAsset = behaviourTaskActor14;
			this.t = behaviourTaskActor14;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconPrinterStar");
			this.t.addBeh(new BehPrinterSetup());
			BehaviourTaskActor behaviourTaskActor15 = new BehaviourTaskActor();
			behaviourTaskActor15.id = "print_step";
			pAsset = behaviourTaskActor15;
			this.t = behaviourTaskActor15;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconPrinterStar");
			this.t.addBeh(new BehPrinterStep());
			BehaviourTaskActor behaviourTaskActor16 = new BehaviourTaskActor();
			behaviourTaskActor16.id = "worm_move";
			pAsset = behaviourTaskActor16;
			this.t = behaviourTaskActor16;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconWorm");
			this.t.addBeh(new BehFindRandomTile8Directions());
			this.t.addBeh(new BehWormDive());
			this.t.addBeh(new BehGoToTileTarget
			{
				walk_on_water = true,
				walk_on_blocks = true
			});
			this.t.addBeh(new BehWormDig());
			BehaviourTaskActor behaviourTaskActor17 = new BehaviourTaskActor();
			behaviourTaskActor17.id = "sandspider_move";
			pAsset = behaviourTaskActor17;
			this.t = behaviourTaskActor17;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconSandSpider");
			this.t.addBeh(new BehFindRandomTile4Directions());
			this.t.addBeh(new BehSandspiderCheckSand());
			this.t.addBeh(new BehSandspiderCheckDie());
			this.t.addBeh(new BehGoToTileTarget
			{
				walk_on_water = true,
				walk_on_blocks = true
			});
			this.t.addBeh(new BehSandspiderBuildSand());
			BehaviourTaskActor behaviourTaskActor18 = new BehaviourTaskActor();
			behaviourTaskActor18.id = "ant_black_island";
			pAsset = behaviourTaskActor18;
			this.t = behaviourTaskActor18;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconAntBlack");
			this.t.addBeh(new BehFindRandomTile4Directions());
			this.t.addBeh(new BehGoToTileTarget
			{
				walk_on_water = true,
				walk_on_blocks = true
			});
			this.t.addBeh(new BehBlackAntBuildIsland());
			BehaviourTaskActor behaviourTaskActor19 = new BehaviourTaskActor();
			behaviourTaskActor19.id = "ant_black_sand";
			pAsset = behaviourTaskActor19;
			this.t = behaviourTaskActor19;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconAntBlack");
			this.t.addBeh(new BehFindRandomTile4Directions());
			this.t.addBeh(new BehGoToTileTarget
			{
				walk_on_water = true,
				walk_on_blocks = true
			});
			this.t.addBeh(new BehBlackAntBuildSand());
			BehaviourTaskActor behaviourTaskActor20 = new BehaviourTaskActor();
			behaviourTaskActor20.id = "ant_red_move";
			pAsset = behaviourTaskActor20;
			this.t = behaviourTaskActor20;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconAntRed");
			this.t.addBeh(new BehAntSetup());
			this.t.addBeh(new BehFindRandomTile4Directions());
			this.t.addBeh(new BehGoToTileTarget
			{
				walk_on_water = true,
				walk_on_blocks = true
			});
			this.t.addBeh(new BehAntSwitchGround());
			BehaviourTaskActor behaviourTaskActor21 = new BehaviourTaskActor();
			behaviourTaskActor21.id = "ant_blue_move";
			pAsset = behaviourTaskActor21;
			this.t = behaviourTaskActor21;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconAntBlue");
			this.t.addBeh(new BehFindRandomTile4Directions());
			this.t.addBeh(new BehGoToTileTarget
			{
				walk_on_water = true,
				walk_on_blocks = true
			});
			this.t.addBeh(new BehBlueAntSwitchGround());
			BehaviourTaskActor behaviourTaskActor22 = new BehaviourTaskActor();
			behaviourTaskActor22.id = "ant_green_move";
			pAsset = behaviourTaskActor22;
			this.t = behaviourTaskActor22;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconAntGreen");
			this.t.addBeh(new BehFindRandomTile4Directions());
			this.t.addBeh(new BehGoToTileTarget
			{
				walk_on_water = true,
				walk_on_blocks = true
			});
			this.t.addBeh(new BehGreenAntSwitchGround());
			BehaviourTaskActor behaviourTaskActor23 = new BehaviourTaskActor();
			behaviourTaskActor23.id = "random_wait_short_1";
			behaviourTaskActor23.cancellable_by_socialize = true;
			behaviourTaskActor23.cancellable_by_reproduction = true;
			behaviourTaskActor23.locale_key = "task_unit_wait";
			pAsset = behaviourTaskActor23;
			this.t = behaviourTaskActor23;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconClock");
			this.t.addBeh(new BehRandomWait(0.1f, 1f, false));
			BehaviourTaskActor behaviourTaskActor24 = new BehaviourTaskActor();
			behaviourTaskActor24.id = "investigate_curiosity";
			behaviourTaskActor24.cancellable_by_socialize = true;
			behaviourTaskActor24.cancellable_by_reproduction = true;
			behaviourTaskActor24.locale_key = "task_unit_move";
			pAsset = behaviourTaskActor24;
			this.t = behaviourTaskActor24;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/subspecies_traits/subspecies_trait_inquisitive_nature");
			this.t.addBeh(new BehCheckCuriosityTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(1f, 3f, false));
			this.t.addBeh(new BehActorReverseFlip());
			this.t.addBeh(new BehRandomWait(1f, 3f, false));
			this.t.addBeh(new BehFindRandomNeighbourTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(1f, 3f, false));
			this.t.addBeh(new BehActorReverseFlip());
			this.t.addBeh(new BehRandomWait(1f, 2f, false));
			BehaviourTaskActor behaviourTaskActor25 = new BehaviourTaskActor();
			behaviourTaskActor25.id = "random_animal_move";
			behaviourTaskActor25.cancellable_by_socialize = true;
			behaviourTaskActor25.cancellable_by_reproduction = true;
			behaviourTaskActor25.locale_key = "task_unit_move";
			pAsset = behaviourTaskActor25;
			this.t = behaviourTaskActor25;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconArrowDestination");
			this.t.addBeh(new BehAnimalFindTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(3f, 6f, false));
			BehaviourTaskActor behaviourTaskActor26 = new BehaviourTaskActor();
			behaviourTaskActor26.id = "random_move_towards_civ_building";
			behaviourTaskActor26.cancellable_by_socialize = true;
			behaviourTaskActor26.cancellable_by_reproduction = true;
			behaviourTaskActor26.locale_key = "task_unit_move";
			pAsset = behaviourTaskActor26;
			this.t = behaviourTaskActor26;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconArrowDestination");
			this.t.addBeh(new BehFindRandomCivBuildingTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(1f, 4f, false));
			BehaviourTaskActor behaviourTaskActor27 = new BehaviourTaskActor();
			behaviourTaskActor27.id = "stay_in_random_house";
			behaviourTaskActor27.cancellable_by_socialize = true;
			behaviourTaskActor27.cancellable_by_reproduction = true;
			behaviourTaskActor27.locale_key = "task_unit_nothing";
			pAsset = behaviourTaskActor27;
			this.t = behaviourTaskActor27;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconBuildings");
			this.t.addBeh(new BehCityActorFindBuilding("random_house_building", true));
			this.t.addBeh(new BehGetTargetBuildingMainTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehStayInBuildingTarget(10f, 60f));
			this.t.addBeh(new BehExitBuilding());
			BehaviourTaskActor behaviourTaskActor28 = new BehaviourTaskActor();
			behaviourTaskActor28.id = "stay_in_own_home";
			behaviourTaskActor28.cancellable_by_socialize = true;
			behaviourTaskActor28.cancellable_by_reproduction = true;
			behaviourTaskActor28.locale_key = "task_unit_nothing";
			pAsset = behaviourTaskActor28;
			this.t = behaviourTaskActor28;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconHoused");
			this.t.addBeh(new BehBuildingTargetHome());
			this.t.addBeh(new BehGetTargetBuildingMainTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehStayInBuildingTarget(10f, 60f));
			this.t.addBeh(new BehRestoreStats(0.1f, 0.2f));
			this.t.addBeh(new BehExitBuilding());
			BehaviourTaskActor behaviourTaskActor29 = new BehaviourTaskActor();
			behaviourTaskActor29.id = "generate_loot";
			behaviourTaskActor29.cancellable_by_socialize = true;
			behaviourTaskActor29.cancellable_by_reproduction = true;
			behaviourTaskActor29.locale_key = "task_unit_nothing";
			pAsset = behaviourTaskActor29;
			this.t = behaviourTaskActor29;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconMoney");
			this.t.addBeh(new BehBuildingTargetHome());
			this.t.addBeh(new BehGetTargetBuildingMainTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehStayInBuildingTarget(2f, 6f));
			this.t.addBeh(new BehGenerateLootFromHouse());
			this.t.addBeh(new BehExitBuilding());
			BehaviourTaskActor behaviourTaskActor30 = new BehaviourTaskActor();
			behaviourTaskActor30.id = "random_swim";
			behaviourTaskActor30.locale_key = "task_unit_move";
			pAsset = behaviourTaskActor30;
			this.t = behaviourTaskActor30;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconTileShallowWater");
			this.t.addBeh(new BehRandomSwim());
			this.t.addBeh(new BehRandomWait(0f, 2f, false));
			BehaviourTaskActor behaviourTaskActor31 = new BehaviourTaskActor();
			behaviourTaskActor31.id = "make_decision";
			pAsset = behaviourTaskActor31;
			this.t = behaviourTaskActor31;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/actor_traits/iconStupid");
			this.t.addBeh(new BehMakeDecision());
			BehaviourTaskActor behaviourTaskActor32 = new BehaviourTaskActor();
			behaviourTaskActor32.id = "try_new_plot";
			behaviourTaskActor32.locale_key = "task_unit_plot";
			pAsset = behaviourTaskActor32;
			this.t = behaviourTaskActor32;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconPlot");
			this.t.addBeh(new BehTryNewPlot());
			BehaviourTaskActor behaviourTaskActor33 = new BehaviourTaskActor();
			behaviourTaskActor33.id = "check_plot";
			behaviourTaskActor33.locale_key = "task_unit_plot";
			pAsset = behaviourTaskActor33;
			this.t = behaviourTaskActor33;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconPlot");
			this.t.addBeh(new BehCheckPlot());
			BehaviourTaskActor behaviourTaskActor34 = new BehaviourTaskActor();
			behaviourTaskActor34.id = "progress_plot";
			behaviourTaskActor34.force_hand_tool = "coffee_cup";
			behaviourTaskActor34.locale_key = "task_unit_plot";
			pAsset = behaviourTaskActor34;
			this.t = behaviourTaskActor34;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconPlot");
			this.t.addBeh(new BehCheckPlotIsOk());
			this.t.addBeh(new BehActorReverseFlip());
			this.t.addBeh(new BehWait(2f));
			this.t.addBeh(new BehCheckPlotProgress());
			this.t.addBeh(new BehSpawnPlotProgressEffect(1));
			this.t.addBeh(new BehCheckNeeds(0));
			this.t.addBeh(new BehRestartTask());
			BehaviourTaskActor behaviourTaskActor35 = new BehaviourTaskActor();
			behaviourTaskActor35.id = "try_to_eat_city_food";
			behaviourTaskActor35.locale_key = "task_unit_eat";
			behaviourTaskActor35.diet = true;
			pAsset = behaviourTaskActor35;
			this.t = behaviourTaskActor35;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconHunger");
			this.t.addBeh(new BehCheckHasMoneyForCityFood());
			this.t.addBeh(new BehFindRandomTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(0.1f, 1f, false));
			for (int k = 0; k < 3; k++)
			{
				this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Tile, string.Empty, 0.2f, 40f, true, false));
			}
			this.t.addBeh(new BehTryToEatCityFood());
			for (int l = 0; l < 3; l++)
			{
				this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Tile, string.Empty, 0.2f, 40f, true, false));
			}
			this.t.addBeh(new BehRandomWait(1f, 5f, false));
			BehaviourTaskActor behaviourTaskActor36 = new BehaviourTaskActor();
			behaviourTaskActor36.id = "find_house";
			pAsset = behaviourTaskActor36;
			this.t = behaviourTaskActor36;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconBuildings");
			this.t.addBeh(new BehFindHouse());
			BehaviourTaskActor behaviourTaskActor37 = new BehaviourTaskActor();
			behaviourTaskActor37.id = "random_move_near_house";
			behaviourTaskActor37.cancellable_by_socialize = true;
			behaviourTaskActor37.cancellable_by_reproduction = true;
			behaviourTaskActor37.locale_key = "task_unit_move";
			pAsset = behaviourTaskActor37;
			this.t = behaviourTaskActor37;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconLivingHouse");
			this.t.addBeh(new BehFindRandomFrontTileNearHouse());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(1f, 6f, false));
			BehaviourTaskActor behaviourTaskActor38 = new BehaviourTaskActor();
			behaviourTaskActor38.id = "end_job";
			pAsset = behaviourTaskActor38;
			this.t = behaviourTaskActor38;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconClose");
			this.t.addBeh(new BehEndJob());
			BehaviourTaskActor behaviourTaskActor39 = new BehaviourTaskActor();
			behaviourTaskActor39.id = "check_end_job";
			pAsset = behaviourTaskActor39;
			this.t = behaviourTaskActor39;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconClose");
			this.t.addBeh(new BehCheckEndCityActorJob());
			BehaviourTaskActor behaviourTaskActor40 = new BehaviourTaskActor();
			behaviourTaskActor40.id = "check_city_destroyed";
			pAsset = behaviourTaskActor40;
			this.t = behaviourTaskActor40;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconWar");
			this.t.addBeh(new BehCheckCityDestroyed());
			BehaviourTaskActor behaviourTaskActor41 = new BehaviourTaskActor();
			behaviourTaskActor41.id = "build_civ_city_here";
			pAsset = behaviourTaskActor41;
			this.t = behaviourTaskActor41;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconCity");
			this.t.addBeh(new BehCheckBuildCity());
			this.t.addBeh(new BehEndJob());
			BehaviourTaskActor behaviourTaskActor42 = new BehaviourTaskActor();
			behaviourTaskActor42.id = "try_to_start_new_civilization";
			pAsset = behaviourTaskActor42;
			this.t = behaviourTaskActor42;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconKingdom");
			this.t.addBeh(new BehCheckEnemyNotNear());
			this.t.addBeh(new BehCheckStartCivilization());
			BehaviourTaskActor behaviourTaskActor43 = new BehaviourTaskActor();
			behaviourTaskActor43.id = "check_join_city";
			pAsset = behaviourTaskActor43;
			this.t = behaviourTaskActor43;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconCity");
			this.t.addBeh(new BehJoinCity());
			this.t.addBeh(new BehEndJob());
			BehaviourTaskActor behaviourTaskActor44 = new BehaviourTaskActor();
			behaviourTaskActor44.id = "check_join_empty_nearby_city";
			pAsset = behaviourTaskActor44;
			this.t = behaviourTaskActor44;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconCity");
			this.t.addBeh(new BehFindNearbyPotentialCivCityToJoin());
			BehaviourTaskActor behaviourTaskActor45 = new BehaviourTaskActor();
			behaviourTaskActor45.id = "try_to_read";
			behaviourTaskActor45.force_hand_tool = "book";
			behaviourTaskActor45.cancellable_by_reproduction = true;
			behaviourTaskActor45.cancellable_by_socialize = true;
			behaviourTaskActor45.locale_key = "task_unit_read";
			pAsset = behaviourTaskActor45;
			this.t = behaviourTaskActor45;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconBooks");
			this.t.addBeh(new BehTryToRead());
			for (int m = 0; m < 7; m++)
			{
				this.t.addBeh(new BehFindRandomNeighbourTile());
				this.t.addBeh(new BehGoToTileTarget());
				this.t.addBeh(new BehSpawnHmmEffect(1));
				for (int n = 0; n < 5; n++)
				{
					this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Nothing, null, 1f, 6f, true, false));
				}
				this.t.addBeh(new BehActorReverseFlip());
				for (int k2 = 0; k2 < 5; k2++)
				{
					this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Nothing, null, 1f, 6f, true, false));
				}
			}
			this.t.addBeh(new BehFinishReading());
			BehaviourTaskActor behaviourTaskActor46 = new BehaviourTaskActor();
			behaviourTaskActor46.id = "citizen";
			pAsset = behaviourTaskActor46;
			this.t = behaviourTaskActor46;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconCity");
			this.t.addBeh(new BehRandomWait(1f, 5f, false));
			this.t.addBeh(new BehFindRandomTile());
			this.t.addBeh(new BehGoToTileTarget());
			BehaviourTaskActor behaviourTaskActor47 = new BehaviourTaskActor();
			behaviourTaskActor47.id = "try_to_take_city_item";
			pAsset = behaviourTaskActor47;
			this.t = behaviourTaskActor47;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/items/icon_sword_wood");
			this.t.addBeh(new BehActorTryToTakeItemFromCity());
			BehaviourTaskActor behaviourTaskActor48 = new BehaviourTaskActor();
			behaviourTaskActor48.id = "city_idle_walking";
			behaviourTaskActor48.cancellable_by_socialize = true;
			behaviourTaskActor48.cancellable_by_reproduction = true;
			behaviourTaskActor48.locale_key = "task_unit_walk";
			pAsset = behaviourTaskActor48;
			this.t = behaviourTaskActor48;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconCity");
			this.t.addBeh(new BehCityActorGetRandomIdleTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(3f, 6f, false));
			BehaviourTaskActor behaviourTaskActor49 = new BehaviourTaskActor();
			behaviourTaskActor49.id = "city_walking_to_danger_zone";
			behaviourTaskActor49.locale_key = "task_unit_investigate";
			pAsset = behaviourTaskActor49;
			this.t = behaviourTaskActor49;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconWar");
			this.t.addBeh(new BehCityActorGetRandomDangerZone());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(3f, 6f, false));
			BehaviourTaskActor behaviourTaskActor50 = new BehaviourTaskActor();
			behaviourTaskActor50.id = "find_city_job";
			pAsset = behaviourTaskActor50;
			this.t = behaviourTaskActor50;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconCity");
			this.t.addBeh(new BehCityActorFindNewJob());
			BehaviourTaskActor behaviourTaskActor51 = new BehaviourTaskActor();
			behaviourTaskActor51.id = "give_tax";
			pAsset = behaviourTaskActor51;
			this.t = behaviourTaskActor51;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconTax");
			this.t.addBeh(new BehActorGiveTax());
			BehaviourTaskActor behaviourTaskActor52 = new BehaviourTaskActor();
			behaviourTaskActor52.id = "do_hunting";
			behaviourTaskActor52.cancellable_by_reproduction = true;
			behaviourTaskActor52.in_combat = true;
			behaviourTaskActor52.ignore_fight_check = true;
			behaviourTaskActor52.locale_key = "task_unit_hunt";
			pAsset = behaviourTaskActor52;
			this.t = behaviourTaskActor52;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/citizen_jobs/iconCitizenJobHunter");
			this.t.addBeh(new BehFindTargetForHunter());
			this.t.addBeh(new BehGoToActorTarget(GoToActorTargetType.SameTile, false, true, true, 2f, true, true));
			this.t.addBeh(new BehAttackActorHuntingTarget());
			this.addActionsForDeliverResources(this.t, false);
			BehaviourTaskActor behaviourTaskActor53 = new BehaviourTaskActor();
			behaviourTaskActor53.id = "make_items";
			pAsset = behaviourTaskActor53;
			this.t = behaviourTaskActor53;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/citizen_jobs/iconCitizenJobBlacksmith");
			this.t.addBeh(new BehBuildingTargetHome());
			this.t.addBeh(new BehGetTargetBuildingMainTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehStayInBuildingTarget(10f, 15f));
			this.t.addBeh(new BehMakeItem());
			this.t.addBeh(new BehActorTryToTakeItemFromCity());
			this.t.addBeh(new BehExitBuilding());
			BehaviourTaskActor behaviourTaskActor54 = new BehaviourTaskActor();
			behaviourTaskActor54.id = "cleaning";
			behaviourTaskActor54.force_hand_tool = "hammer";
			behaviourTaskActor54.cancellable_by_socialize = true;
			behaviourTaskActor54.cancellable_by_reproduction = true;
			behaviourTaskActor54.locale_key = "task_unit_cleaning_ruins";
			pAsset = behaviourTaskActor54;
			this.t = behaviourTaskActor54;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/citizen_jobs/iconCitizenJobCleaner");
			this.t.addBeh(new BehCityActorFindBuilding("ruins", true));
			this.t.addBeh(new BehGetTargetBuildingMainTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(0.3f, 0.3f, false));
			this.t.addBeh(new BehLookAtBuildingTarget(0.3f));
			for (int i2 = 0; i2 < 3; i2++)
			{
				this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Ruin, "event:/SFX/CIVILIZATIONS/CleanRuins", 1f, 40f, true, true));
			}
			this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Ruin, "event:/SFX/CIVILIZATIONS/CleanRuins", 0f, 40f, true, true));
			this.t.addBeh(new BehRemoveRuins());
			this.addActionsForDeliverResources(this.t, false);
			BehaviourTaskActor behaviourTaskActor55 = new BehaviourTaskActor();
			behaviourTaskActor55.id = "manure_cleaning";
			behaviourTaskActor55.force_hand_tool = "basket";
			behaviourTaskActor55.cancellable_by_reproduction = true;
			behaviourTaskActor55.locale_key = "task_unit_cleaning_poop";
			pAsset = behaviourTaskActor55;
			this.t = behaviourTaskActor55;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/citizen_jobs/iconCitizenJobCleaner");
			this.t.addBeh(new BehCityActorFindBuilding("type_poop", true));
			this.t.addBeh(new BehGetTargetBuildingMainTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehLookAtBuildingTarget(0.3f));
			for (int i3 = 0; i3 < 5; i3++)
			{
				this.t.addBeh(new BehResourceGatheringAnimation(1f, "event:/SFX/CIVILIZATIONS/CollectHerbs", true));
			}
			this.t.addBeh(new BehExtractResourcesFromBuilding());
			this.addActionsForDeliverResources(this.t, false);
			this.t.addBeh(new BehCheckNeeds(6));
			this.t.addBeh(new BehRestartTask());
			BehaviourTaskActor behaviourTaskActor56 = new BehaviourTaskActor();
			behaviourTaskActor56.id = "put_out_fire";
			behaviourTaskActor56.force_hand_tool = "bucket";
			behaviourTaskActor56.locale_key = "task_unit_extinguish";
			behaviourTaskActor56.is_fireman = true;
			pAsset = behaviourTaskActor56;
			this.t = behaviourTaskActor56;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/citizen_jobs/iconCitizenJobFireman");
			this.t.addBeh(new BehCityActorFindFireZone());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehCityActorFindClosestFire());
			this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Nothing, null, 0f, -20f, false, false));
			this.t.addBeh(new BehRandomWait(0.1f, 0.5f, false));
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Tile, null, 0f, 40f, true, false));
			this.t.addBeh(new BehCityActorRemoveFire());
			this.t.addBeh(new BehRandomWait(0.4f, 1.2f, false));
			this.t.addBeh(new BehRestartTask());
			BehaviourTaskActor behaviourTaskActor57 = new BehaviourTaskActor();
			behaviourTaskActor57.id = "try_build_building";
			behaviourTaskActor57.force_hand_tool = "hammer";
			behaviourTaskActor57.locale_key = "task_unit_build";
			pAsset = behaviourTaskActor57;
			this.t = behaviourTaskActor57;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/citizen_jobs/iconCitizenJobBuilder");
			this.t.addBeh(new BehCityActorFindBuilding("new_building", true));
			this.t.addBeh(new BehSetNextTask("build_building", false, true));
			BehaviourTaskActor behaviourTaskActor58 = new BehaviourTaskActor();
			behaviourTaskActor58.id = "build_building";
			behaviourTaskActor58.force_hand_tool = "hammer";
			behaviourTaskActor58.locale_key = "task_unit_build";
			pAsset = behaviourTaskActor58;
			this.t = behaviourTaskActor58;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/citizen_jobs/iconCitizenJobBuilder");
			this.t.addBeh(new BehCheckStillUnderConstruction());
			this.t.addBeh(new BehFindConstructionTile());
			this.t.addBeh(new BehGoToTileTarget());
			for (int i4 = 0; i4 < 5; i4++)
			{
				this.t.addBeh(new BehCheckStillUnderConstruction());
				this.t.addBeh(new BehLookAtBuildingTarget(0.3f));
				this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Building, "event:/SFX/BUILDINGS/СonstructionBuildingGeneric", 0f, 40f, true, true));
				this.t.addBeh(new BehBuildTarget());
			}
			this.t.addBeh(new BehRestartTask());
			BehaviourTaskActor behaviourTaskActor59 = new BehaviourTaskActor();
			behaviourTaskActor59.id = "build_road";
			behaviourTaskActor59.force_hand_tool = "hammer";
			behaviourTaskActor59.locale_key = "task_unit_build";
			pAsset = behaviourTaskActor59;
			this.t = behaviourTaskActor59;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/citizen_jobs/iconCitizenJobRoadBuilder");
			this.t.addBeh(new BehFindTile(TileFinderType.NewRoad)
			{
				null_check_city = true
			});
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(0.2f, 1f, false));
			for (int i5 = 0; i5 < 3; i5++)
			{
				this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Tile, "event:/SFX/CIVILIZATIONS/BuildRoad", 1f, 40f, true, true));
			}
			this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Tile, "event:/SFX/CIVILIZATIONS/BuildRoad", 0f, 40f, true, true));
			this.t.addBeh(new BehCityActorCreateRoad());
			this.t.addBeh(new BehRandomWait(1f, 2f, false));
			this.t.addBeh(new BehCheckNeeds(10));
			this.t.addBeh(new BehRestartTask());
			BehaviourTaskActor behaviourTaskActor60 = new BehaviourTaskActor();
			behaviourTaskActor60.id = "collect_fruits";
			behaviourTaskActor60.force_hand_tool = "basket";
			behaviourTaskActor60.cancellable_by_reproduction = true;
			behaviourTaskActor60.locale_key = "task_unit_collect_food";
			pAsset = behaviourTaskActor60;
			this.t = behaviourTaskActor60;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/citizen_jobs/iconCitizenJobGathererBushes");
			this.t.addBeh(new BehCityActorFindBuilding("type_fruits", true));
			this.t.addBeh(new BehGetTargetBuildingMainTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehLookAtBuildingTarget(0.3f));
			for (int i6 = 0; i6 < 4; i6++)
			{
				this.t.addBeh(new BehResourceGatheringAnimation(1f, "event:/SFX/CIVILIZATIONS/CollectFruits", true));
			}
			this.t.addBeh(new BehResourceGatheringAnimation(0f, "event:/SFX/CIVILIZATIONS/CollectFruits", true));
			this.t.addBeh(new BehExtractResourcesFromBuilding());
			this.addActionsForDeliverResources(this.t, false);
			this.t.addBeh(new BehCheckNeeds(5));
			this.t.addBeh(new BehRestartTask());
			BehaviourTaskActor behaviourTaskActor61 = new BehaviourTaskActor();
			behaviourTaskActor61.id = "collect_honey";
			behaviourTaskActor61.force_hand_tool = "basket";
			behaviourTaskActor61.cancellable_by_reproduction = true;
			behaviourTaskActor61.locale_key = "task_unit_collect_honey";
			pAsset = behaviourTaskActor61;
			this.t = behaviourTaskActor61;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/citizen_jobs/iconCitizenJobGathererHoney");
			this.t.addBeh(new BehCityActorFindBuilding("type_hive", true));
			this.t.addBeh(new BehGetTargetBuildingMainTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehLookAtBuildingTarget(0.3f));
			for (int i7 = 0; i7 < 4; i7++)
			{
				this.t.addBeh(new BehResourceGatheringAnimation(1f, null, true));
			}
			this.t.addBeh(new BehResourceGatheringAnimation(0f, null, true));
			this.t.addBeh(new BehExtractResourcesFromBuilding());
			this.addActionsForDeliverResources(this.t, false);
			this.t.addBeh(new BehCheckNeeds(3));
			this.t.addBeh(new BehRestartTask());
			BehaviourTaskActor behaviourTaskActor62 = new BehaviourTaskActor();
			behaviourTaskActor62.id = "claim_land";
			behaviourTaskActor62.force_hand_tool = "flag";
			behaviourTaskActor62.cancellable_by_reproduction = true;
			behaviourTaskActor62.locale_key = "task_unit_claim_land";
			pAsset = behaviourTaskActor62;
			this.t = behaviourTaskActor62;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/citizen_jobs/iconCitizenLandClaimer");
			this.t.addBeh(new BehActorCheckZoneTarget());
			this.t.addBeh(new BehGoToTileTarget());
			for (int i8 = 0; i8 < 21; i8++)
			{
				this.t.addBeh(new BehActorReverseFlip());
				this.t.addBeh(new BehJumpingAnimation(0.5f, 0.5f));
				this.t.addBeh(new BehSpawnCityBorderEffect(1));
			}
			this.t.addBeh(new BehClaimZoneForCityActorBorder());
			this.t.addBeh(new BehSpawnCityBorderEffect(5));
			this.t.addBeh(new BehEndJob());
			BehaviourTaskActor behaviourTaskActor63 = new BehaviourTaskActor();
			behaviourTaskActor63.id = "collect_herbs";
			behaviourTaskActor63.force_hand_tool = "basket";
			behaviourTaskActor63.cancellable_by_reproduction = true;
			behaviourTaskActor63.locale_key = "task_unit_collect_herbs";
			pAsset = behaviourTaskActor63;
			this.t = behaviourTaskActor63;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/citizen_jobs/iconCitizenJobGathererHerbs");
			this.t.addBeh(new BehCityActorFindBuilding("type_vegetation", true));
			this.t.addBeh(new BehGetTargetBuildingMainTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehLookAtBuildingTarget(0.3f));
			for (int i9 = 0; i9 < 4; i9++)
			{
				this.t.addBeh(new BehResourceGatheringAnimation(1f, "event:/SFX/CIVILIZATIONS/CollectHerbs", true));
			}
			this.t.addBeh(new BehResourceGatheringAnimation(0f, "event:/SFX/CIVILIZATIONS/CollectHerbs", true));
			this.t.addBeh(new BehExtractResourcesFromBuilding());
			this.addActionsForDeliverResources(this.t, false);
			this.t.addBeh(new BehCheckNeeds(4));
			this.t.addBeh(new BehRestartTask());
			BehaviourTaskActor behaviourTaskActor64 = new BehaviourTaskActor();
			behaviourTaskActor64.id = "chop_trees";
			behaviourTaskActor64.force_hand_tool = "axe";
			behaviourTaskActor64.cancellable_by_reproduction = true;
			behaviourTaskActor64.locale_key = "task_unit_chop";
			pAsset = behaviourTaskActor64;
			this.t = behaviourTaskActor64;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/citizen_jobs/iconCitizenJobWoodcutter");
			this.t.addBeh(new BehCityActorFindBuilding("type_tree", true));
			this.t.addBeh(new BehGetTargetBuildingMainTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehLookAtBuildingTarget(0.3f));
			for (int i10 = 0; i10 < 7; i10++)
			{
				this.t.addBeh(new BehResourceGatheringAnimation(1f, "event:/SFX/CIVILIZATIONS/ChopTree", true));
			}
			this.t.addBeh(new BehResourceGatheringAnimation(0f, "event:/SFX/CIVILIZATIONS/ChopTree", true));
			this.t.addBeh(new BehExtractResourcesFromBuilding());
			this.addActionsForDeliverResources(this.t, false);
			BehaviourTaskActor behaviourTaskActor65 = new BehaviourTaskActor();
			behaviourTaskActor65.id = "mine_deposit";
			behaviourTaskActor65.force_hand_tool = "pickaxe";
			behaviourTaskActor65.cancellable_by_reproduction = true;
			behaviourTaskActor65.locale_key = "task_unit_mine";
			pAsset = behaviourTaskActor65;
			this.t = behaviourTaskActor65;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/citizen_jobs/iconCitizenJobMinerDeposit");
			this.t.addBeh(new BehCityActorFindBuilding("type_mineral", true));
			this.t.addBeh(new BehGetTargetBuildingMainTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehLookAtBuildingTarget(0.3f));
			for (int i11 = 0; i11 < 6; i11++)
			{
				this.t.addBeh(new BehResourceGatheringAnimation(1f, "event:/SFX/CIVILIZATIONS/MiningMineral", true));
			}
			this.t.addBeh(new BehResourceGatheringAnimation(0f, "event:/SFX/CIVILIZATIONS/MiningMineral", true));
			this.t.addBeh(new BehExtractResourcesFromBuilding());
			this.addActionsForDeliverResources(this.t, false);
			this.t.addBeh(new BehCheckNeeds(3));
			this.t.addBeh(new BehRestartTask());
			BehaviourTaskActor behaviourTaskActor66 = new BehaviourTaskActor();
			behaviourTaskActor66.id = "farmer_make_field";
			behaviourTaskActor66.force_hand_tool = "hoe";
			behaviourTaskActor66.cancellable_by_reproduction = true;
			behaviourTaskActor66.locale_key = "task_unit_farm";
			pAsset = behaviourTaskActor66;
			this.t = behaviourTaskActor66;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/citizen_jobs/iconCitizenJobFarmer");
			this.t.addBeh(new BehFindTileForFarm());
			this.t.addBeh(new BehGoToTileTarget());
			for (int i12 = 0; i12 < 6; i12++)
			{
				this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Tile, "event:/SFX/CIVILIZATIONS/MakeFarmField", 1f, 40f, true, true));
			}
			this.t.addBeh(new BehMakeFarm());
			this.t.addBeh(new BehRandomWait(1f, 2f, false));
			BehaviourTaskActor behaviourTaskActor67 = new BehaviourTaskActor();
			behaviourTaskActor67.id = "farmer_plant_crops";
			behaviourTaskActor67.force_hand_tool = "hoe";
			behaviourTaskActor67.cancellable_by_reproduction = true;
			behaviourTaskActor67.locale_key = "task_unit_farm";
			pAsset = behaviourTaskActor67;
			this.t = behaviourTaskActor67;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/citizen_jobs/iconCitizenJobFarmer");
			this.t.addBeh(new BehFindFarmField());
			this.t.addBeh(new BehGoToTileTarget());
			for (int i13 = 0; i13 < 4; i13++)
			{
				this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Tile, "event:/SFX/CIVILIZATIONS/PlantCrops", 1f, 40f, true, true));
			}
			this.t.addBeh(new BehPlantCrops());
			this.t.addBeh(new BehRandomWait(1f, 2f, false));
			this.t.addBeh(new BehCheckNeeds(6));
			this.t.addBeh(new BehRestartTask());
			BehaviourTaskActor behaviourTaskActor68 = new BehaviourTaskActor();
			behaviourTaskActor68.id = "farmer_harvest";
			behaviourTaskActor68.force_hand_tool = "hoe";
			behaviourTaskActor68.locale_key = "task_unit_farm";
			pAsset = behaviourTaskActor68;
			this.t = behaviourTaskActor68;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/citizen_jobs/iconCitizenJobFarmer");
			this.t.addBeh(new BehFindWheat());
			this.t.addBeh(new BehGoToBuildingTarget(false));
			for (int i14 = 0; i14 < 3; i14++)
			{
				this.t.addBeh(new BehResourceGatheringAnimation(1f, "event:/SFX/CIVILIZATIONS/HarvestCrops", true));
			}
			this.t.addBeh(new BehResourceGatheringAnimation(0f, "event:/SFX/CIVILIZATIONS/HarvestCrops", true));
			this.t.addBeh(new BehExtractResourcesFromBuilding());
			this.addActionsForDeliverResources(this.t, true);
			this.t.addBeh(new BehCheckNeeds(6));
			this.t.addBeh(new BehRestartTask());
			BehaviourTaskActor behaviourTaskActor69 = new BehaviourTaskActor();
			behaviourTaskActor69.id = "farmer_fertilize_crops";
			behaviourTaskActor69.force_hand_tool = "bucket";
			behaviourTaskActor69.cancellable_by_reproduction = true;
			behaviourTaskActor69.locale_key = "task_unit_farm";
			pAsset = behaviourTaskActor69;
			this.t = behaviourTaskActor69;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/citizen_jobs/iconCitizenJobFarmer");
			this.t.addBeh(new BehCityActorFindStorage());
			this.t.addBeh(new BehGoToBuildingTarget(false));
			this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Building, null, 1f, 40f, true, true));
			this.t.addBeh(new BehCityActorGetResourceFromStorage("fertilizer", 5));
			for (int i15 = 0; i15 < 5; i15++)
			{
				this.t.addBeh(new BehCityActorFindUngrownCrop());
				this.t.addBeh(new BehGoToBuildingTarget(false));
				this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Building, null, 0f, 40f, true, true));
				this.t.addBeh(new BehThrowResourceAnimation("fertilizer"));
				this.t.addBeh(new BehWait(1f));
				this.t.addBeh(new BehCityActorFertilizeCrop());
			}
			this.addActionsForDeliverResources(this.t, false);
			this.t.addBeh(new BehRandomWait(1f, 2f, false));
			this.t.addBeh(new BehCheckNeeds(6));
			this.t.addBeh(new BehRestartTask());
			BehaviourTaskActor behaviourTaskActor70 = new BehaviourTaskActor();
			behaviourTaskActor70.id = "farmer_random_move";
			behaviourTaskActor70.force_hand_tool = "hoe";
			behaviourTaskActor70.locale_key = "task_unit_farm";
			pAsset = behaviourTaskActor70;
			this.t = behaviourTaskActor70;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/citizen_jobs/iconCitizenJobFarmer");
			this.t.addBeh(new BehFindRandomFarmTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(1f, 3f, false));
			BehaviourTaskActor behaviourTaskActor71 = new BehaviourTaskActor();
			behaviourTaskActor71.id = "store_resources";
			behaviourTaskActor71.locale_key = "task_unit_store_resources";
			pAsset = behaviourTaskActor71;
			this.t = behaviourTaskActor71;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconCityInventory");
			this.addActionsForDeliverResources(this.t, false);
			BehaviourTaskActor behaviourTaskActor72 = new BehaviourTaskActor();
			behaviourTaskActor72.id = "mine";
			behaviourTaskActor72.force_hand_tool = "pickaxe";
			behaviourTaskActor72.locale_key = "task_unit_mine";
			pAsset = behaviourTaskActor72;
			this.t = behaviourTaskActor72;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/citizen_jobs/iconCitizenJobMiner");
			this.t.addBeh(new BehCityActorFindBuilding("type_mine", true));
			this.t.addBeh(new BehGoToBuildingTarget(false));
			this.t.addBeh(new BehStayInBuildingTarget(10f, 15f));
			this.t.addBeh(new BehGetResourcesFromMine());
			this.t.addBeh(new BehExitBuilding());
			this.t.addBeh(new BehFindRandomTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(1f, 2f, false));
			this.t.addBeh(new BehCheckNeeds(3));
			this.t.addBeh(new BehRestartTask());
			BehaviourTaskActor behaviourTaskActor73 = new BehaviourTaskActor();
			behaviourTaskActor73.id = "try_to_return_to_home_city";
			behaviourTaskActor73.flag_boat_related = true;
			pAsset = behaviourTaskActor73;
			this.t = behaviourTaskActor73;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconHoused");
			this.t.addBeh(new BehTaxiCheck());
			this.t.addBeh(new BehRandomWait(1f, 1f, false));
			this.t.addBeh(new BehTaxiFindShipTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehTaxiEmbark());
			BehaviourTaskActor behaviourTaskActor74 = new BehaviourTaskActor();
			behaviourTaskActor74.id = "force_into_a_boat";
			behaviourTaskActor74.flag_boat_related = true;
			pAsset = behaviourTaskActor74;
			this.t = behaviourTaskActor74;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconBoat");
			this.t.addBeh(new BehTaxiFindShipTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehTaxiEmbark());
			BehaviourTaskActor behaviourTaskActor75 = new BehaviourTaskActor();
			behaviourTaskActor75.id = "embark_into_boat";
			behaviourTaskActor75.flag_boat_related = true;
			pAsset = behaviourTaskActor75;
			this.t = behaviourTaskActor75;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconBoat");
			this.t.addBeh(new BehTaxiEmbark());
			BehaviourTaskActor behaviourTaskActor76 = new BehaviourTaskActor();
			behaviourTaskActor76.id = "sit_inside_boat";
			behaviourTaskActor76.flag_boat_related = true;
			pAsset = behaviourTaskActor76;
			this.t = behaviourTaskActor76;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconBoat");
			this.t.addBeh(new BehTaxiSitInside());
			this.t.addBeh(new BehTaxiSitInside());
			this.t.addBeh(new BehTaxiSitInside());
			BehaviourTaskActor behaviourTaskActor77 = new BehaviourTaskActor();
			behaviourTaskActor77.id = "wait";
			behaviourTaskActor77.cancellable_by_socialize = true;
			behaviourTaskActor77.cancellable_by_reproduction = true;
			behaviourTaskActor77.locale_key = "task_unit_wait";
			pAsset = behaviourTaskActor77;
			this.t = behaviourTaskActor77;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconClock");
			this.t.addBeh(new BehRandomWait(0.5f, 1.3f, false));
			BehaviourTaskActor behaviourTaskActor78 = new BehaviourTaskActor();
			behaviourTaskActor78.id = "wait5";
			behaviourTaskActor78.cancellable_by_socialize = true;
			behaviourTaskActor78.cancellable_by_reproduction = true;
			behaviourTaskActor78.locale_key = "task_unit_wait";
			pAsset = behaviourTaskActor78;
			this.t = behaviourTaskActor78;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconClock");
			this.t.addBeh(new BehRandomWait(1f, 5f, false));
			BehaviourTaskActor behaviourTaskActor79 = new BehaviourTaskActor();
			behaviourTaskActor79.id = "wait10";
			behaviourTaskActor79.cancellable_by_socialize = true;
			behaviourTaskActor79.cancellable_by_reproduction = true;
			behaviourTaskActor79.locale_key = "task_unit_wait";
			pAsset = behaviourTaskActor79;
			this.t = behaviourTaskActor79;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconClock");
			this.t.addBeh(new BehRandomWait(1f, 10f, false));
			BehaviourTaskActor behaviourTaskActor80 = new BehaviourTaskActor();
			behaviourTaskActor80.id = "replenish_energy";
			behaviourTaskActor80.cancellable_by_socialize = false;
			behaviourTaskActor80.cancellable_by_reproduction = true;
			behaviourTaskActor80.locale_key = "task_unit_replenish_energy";
			pAsset = behaviourTaskActor80;
			this.t = behaviourTaskActor80;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconStamina");
			this.t.addBeh(new BehFindBuilding("type_well", false, false));
			this.t.addBeh(new BehGoToBuildingTarget(false));
			for (int i16 = 0; i16 < 5; i16++)
			{
				this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Building, null, 1f, 20f, true, true));
			}
			this.t.addBeh(new BehReplenishEnergy());
			BehaviourTaskActor behaviourTaskActor81 = new BehaviourTaskActor();
			behaviourTaskActor81.id = "repair_equipment";
			behaviourTaskActor81.cancellable_by_socialize = false;
			behaviourTaskActor81.cancellable_by_reproduction = true;
			behaviourTaskActor81.locale_key = "task_unit_repair_equipment";
			pAsset = behaviourTaskActor81;
			this.t = behaviourTaskActor81;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconReforge");
			this.t.addBeh(new BehCheckCanRepairEquipment());
			this.t.addBeh(new BehFindBuilding("type_barracks", false, false));
			this.t.addBeh(new BehGoToBuildingTarget(false));
			for (int i17 = 0; i17 < 5; i17++)
			{
				this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Building, null, 1f, 20f, true, false));
			}
			this.t.addBeh(new BehRepairEquipment());
		}

		// Token: 0x06004624 RID: 17956 RVA: 0x001D7B28 File Offset: 0x001D5D28
		private void initTasksThinkingReflectionHappiness()
		{
			BehaviourTaskActor behaviourTaskActor = new BehaviourTaskActor();
			behaviourTaskActor.id = "try_to_steal_money";
			BehaviourTaskActor pAsset = behaviourTaskActor;
			this.t = behaviourTaskActor;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/actor_traits/iconThief");
			this.t.addBeh(new BehFindTargetToStealFrom());
			this.t.addBeh(new BehGoToActorTarget(GoToActorTargetType.NearbyTileClosest, false, false, true, 2f, true, true));
			for (int i = 0; i < 2; i++)
			{
				this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Actor, null, 0.3f, 30f, true, true));
			}
			this.t.addBeh(new BehStealFromTarget());
			this.t.addBeh(new BehSetNextTask("run_away", true, false));
			BehaviourTaskActor behaviourTaskActor2 = new BehaviourTaskActor();
			behaviourTaskActor2.id = "reflection";
			pAsset = behaviourTaskActor2;
			this.t = behaviourTaskActor2;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconBre");
			this.t.addBeh(new BehReflection());
			BehaviourTaskActor behaviourTaskActor3 = new BehaviourTaskActor();
			behaviourTaskActor3.id = "madness_random_emotion";
			pAsset = behaviourTaskActor3;
			this.t = behaviourTaskActor3;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/actor_traits/iconMadness");
			this.t.addBeh(new BehMadnessRandomEmotion());
			BehaviourTaskActor behaviourTaskActor4 = new BehaviourTaskActor();
			behaviourTaskActor4.id = "happy_laughing";
			pAsset = behaviourTaskActor4;
			this.t = behaviourTaskActor4;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconLaughing");
			this.t.addBeh(new BehTryFindTargetWithStatusNearby(new string[]
			{
				"laughing",
				"crying",
				"swearing",
				"singing"
			}));
			this.t.addBeh(new BehGoToTileTarget());
			for (int j = 0; j < 4; j++)
			{
				this.t.addBeh(new BehActorAddStatus("laughing", 2f, false, false));
				for (int k = 0; k < 3; k++)
				{
					this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Nothing, null, 0.2f, -20f, false, false));
				}
				this.t.addBeh(new BehActorReverseFlip());
				this.t.addBeh(new BehWait(0.3f));
			}
			this.t.addBeh(new BehAddHappiness("just_laughed"));
			BehaviourTaskActor behaviourTaskActor5 = new BehaviourTaskActor();
			behaviourTaskActor5.id = "singing";
			pAsset = behaviourTaskActor5;
			this.t = behaviourTaskActor5;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconSinging");
			this.t.addBeh(new BehTryFindTargetWithStatusNearby(new string[]
			{
				"laughing",
				"crying",
				"swearing",
				"singing"
			}));
			this.t.addBeh(new BehGoToTileTarget());
			for (int l = 0; l < 5; l++)
			{
				this.t.addBeh(new BehJumpingAnimation(0f, 5f));
				this.t.addBeh(new BehActorAddStatus("singing", 3f, false, false));
				for (int m = 0; m < 3; m++)
				{
					this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Nothing, null, 0.4f, -30f, false, false));
					this.t.addBeh(new BehActorReverseFlip());
					this.t.addBeh(new BehWait(0.1f));
				}
			}
			this.t.addBeh(new BehAddHappiness("just_sang"));
			this.t.addBeh(new BehFinishSinging());
			BehaviourTaskActor behaviourTaskActor6 = new BehaviourTaskActor();
			behaviourTaskActor6.id = "swearing";
			pAsset = behaviourTaskActor6;
			this.t = behaviourTaskActor6;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconSwearing");
			this.t.addBeh(new BehTryFindTargetWithStatusNearby(new string[]
			{
				"laughing",
				"crying",
				"swearing",
				"singing"
			}));
			this.t.addBeh(new BehGoToTileTarget());
			for (int n = 0; n < 4; n++)
			{
				this.t.addBeh(new BehActorAddStatus("swearing", 2f, false, false));
				for (int k2 = 0; k2 < 3; k2++)
				{
					this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Nothing, null, 0.2f, -20f, false, false));
				}
				this.t.addBeh(new BehActorReverseFlip());
				this.t.addBeh(new BehWait(0.3f));
			}
			this.t.addBeh(new BehAddHappiness("just_swore"));
			BehaviourTaskActor behaviourTaskActor7 = new BehaviourTaskActor();
			behaviourTaskActor7.id = "crying";
			pAsset = behaviourTaskActor7;
			this.t = behaviourTaskActor7;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconCrying");
			this.t.addBeh(new BehTryFindTargetWithStatusNearby(new string[]
			{
				"laughing",
				"crying",
				"swearing",
				"singing"
			}));
			this.t.addBeh(new BehGoToTileTarget());
			for (int i2 = 0; i2 < 4; i2++)
			{
				this.t.addBeh(new BehActorAddStatus("crying", 2f, false, false));
				for (int k3 = 0; k3 < 3; k3++)
				{
					this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Nothing, null, 0.2f, -20f, false, false));
				}
				this.t.addBeh(new BehActorReverseFlip());
				this.t.addBeh(new BehWait(0.3f));
			}
			this.t.addBeh(new BehAddHappiness("just_cried"));
			BehaviourTaskActor behaviourTaskActor8 = new BehaviourTaskActor();
			behaviourTaskActor8.id = "possessed_following";
			pAsset = behaviourTaskActor8;
			this.t = behaviourTaskActor8;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconPossessed");
			this.t.addBeh(new BehTryFindTargetWithStatusNearby(new string[]
			{
				"possessed"
			}));
			this.t.addBeh(new BehCopyAggro());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehFindRandomNeighbourTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(0.5f, 2f, false));
			this.t.addBeh(new BehRandomSocializeTopic(1.5f, 3f, 0.1f));
			BehaviourTaskActor behaviourTaskActor9 = new BehaviourTaskActor();
			behaviourTaskActor9.id = "start_tantrum";
			pAsset = behaviourTaskActor9;
			this.t = behaviourTaskActor9;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconTantrum");
			for (int i3 = 0; i3 < 6; i3++)
			{
				this.t.addBeh(new BehActorReverseFlip());
				this.t.addBeh(new BehJumpingAnimation(0.1f, 0.1f));
			}
			this.t.addBeh(new BehActorAddStatus("tantrum", 60f, true, false));
			BehaviourTaskActor behaviourTaskActor10 = new BehaviourTaskActor();
			behaviourTaskActor10.id = "do_tantrum";
			pAsset = behaviourTaskActor10;
			this.t = behaviourTaskActor10;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconTantrum");
			this.t.addBeh(new BehFindTantrumTarget());
			this.t.addBeh(new BehGoToActorTarget(GoToActorTargetType.RaycastWithAttackRange, false, true, true, 2f, true, true));
			this.t.addBeh(new BehAddAggroForBehTarget());
			BehaviourTaskActor behaviourTaskActor11 = new BehaviourTaskActor();
			behaviourTaskActor11.id = "punch_a_tree";
			pAsset = behaviourTaskActor11;
			this.t = behaviourTaskActor11;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconRage");
			this.t.addBeh(new BehFindBuilding("type_tree", true, false));
			this.t.addBeh(new BehGoToBuildingTarget(false));
			for (int i4 = 0; i4 < 2; i4++)
			{
				this.t.addBeh(new BehResourceGatheringAnimation(1f, "event:/SFX/HIT/HitWood", true));
			}
			this.t.addBeh(new BehGetDamaged(1, AttackType.Gravity));
			this.t.addBeh(new BehActorAddStatus("crying", 5f, false, true));
			this.t.addBeh(new BehRandomWait(1f, 2f, false));
			BehaviourTaskActor behaviourTaskActor12 = new BehaviourTaskActor();
			behaviourTaskActor12.id = "punch_a_building";
			pAsset = behaviourTaskActor12;
			this.t = behaviourTaskActor12;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconRage");
			this.t.addBeh(new BehFindBuilding("type_house", false, false));
			this.t.addBeh(new BehGoToBuildingTarget(false));
			for (int i5 = 0; i5 < 2; i5++)
			{
				this.t.addBeh(new BehResourceGatheringAnimation(1f, "event:/SFX/HIT/HitGeneric", true));
			}
			this.t.addBeh(new BehGetDamaged(1, AttackType.Gravity));
			this.t.addBeh(new BehActorAddStatus("crying", 5f, false, true));
			this.t.addBeh(new BehRandomWait(1f, 2f, false));
			BehaviourTaskActor behaviourTaskActor13 = new BehaviourTaskActor();
			behaviourTaskActor13.id = "start_fire";
			pAsset = behaviourTaskActor13;
			this.t = behaviourTaskActor13;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconFire");
			this.t.addBeh(new BehWait(1f));
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x001D844C File Offset: 0x001D664C
		private void initTasksClanLeader()
		{
			BehaviourTaskActor behaviourTaskActor = new BehaviourTaskActor();
			behaviourTaskActor.id = "kill_unruly_clan_members";
			BehaviourTaskActor pAsset = behaviourTaskActor;
			this.t = behaviourTaskActor;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/clan_traits/clan_trait_deathbound");
			this.t.addBeh(new BehClanChiefCheckMembersToKill());
			BehaviourTaskActor behaviourTaskActor2 = new BehaviourTaskActor();
			behaviourTaskActor2.id = "banish_unruly_clan_members";
			pAsset = behaviourTaskActor2;
			this.t = behaviourTaskActor2;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/clan_traits/clan_trait_blood_pact");
			this.t.addBeh(new BehClanChiefCheckMembersToBanish());
		}

		// Token: 0x06004626 RID: 17958 RVA: 0x001D84DC File Offset: 0x001D66DC
		private void initTasksPoop()
		{
			BehaviourTaskActor behaviourTaskActor = new BehaviourTaskActor();
			behaviourTaskActor.id = "try_to_poop";
			BehaviourTaskActor pAsset = behaviourTaskActor;
			this.t = behaviourTaskActor;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconPoop");
			this.t.addBeh(new BehDecideWhereToPoop());
			BehaviourTaskActor behaviourTaskActor2 = new BehaviourTaskActor();
			behaviourTaskActor2.id = "try_to_launch_fireworks";
			pAsset = behaviourTaskActor2;
			this.t = behaviourTaskActor2;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconFireworks");
			this.t.addBeh(new BehTryFindTargetWithStatusNearby(new string[]
			{
				"laughing",
				"crying",
				"swearing",
				"singing"
			}));
			this.t.addBeh(new BehGoToTileTarget());
			for (int i = 0; i < 1; i++)
			{
				this.t.addBeh(new BehActorAddStatus("laughing", 2f, false, false));
				for (int j = 0; j < 3; j++)
				{
					this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Nothing, null, 0.2f, -20f, false, true));
				}
				this.t.addBeh(new BehActorReverseFlip());
				this.t.addBeh(new BehWait(0.3f));
			}
			this.t.addBeh(new BehLaunchFireworks());
			this.t.addBeh(new BehAddHappiness("just_laughed"));
			this.t.addBeh(new BehFindRandomTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(0f, 1f, false));
			BehaviourTaskActor behaviourTaskActor3 = new BehaviourTaskActor();
			behaviourTaskActor3.id = "poop_inside";
			pAsset = behaviourTaskActor3;
			this.t = behaviourTaskActor3;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconPoop");
			this.t.addBeh(new BehBuildingTargetHome());
			this.t.addBeh(new BehGetTargetBuildingMainTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehStayInBuildingTarget(10f, 60f));
			this.t.addBeh(new BehPoopInside());
			this.t.addBeh(new BehExitBuilding());
			BehaviourTaskActor behaviourTaskActor4 = new BehaviourTaskActor();
			behaviourTaskActor4.id = "poop_outside";
			pAsset = behaviourTaskActor4;
			this.t = behaviourTaskActor4;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconPoop");
			this.t.addBeh(new BehFindTile(TileFinderType.FreeTile));
			this.t.addBeh(new BehGoToTileTarget());
			for (int k = 0; k < 4; k++)
			{
				this.t.addBeh(new BehActorReverseFlip());
				this.t.addBeh(new BehJumpingAnimation(0.5f, 0.5f));
			}
			this.t.addBeh(new BehPoopOutside());
		}

		// Token: 0x06004627 RID: 17959 RVA: 0x001D87A8 File Offset: 0x001D69A8
		private void initTasksSleep()
		{
			BehaviourTaskActor behaviourTaskActor = new BehaviourTaskActor();
			behaviourTaskActor.id = "decide_where_to_sleep";
			BehaviourTaskActor pAsset = behaviourTaskActor;
			this.t = behaviourTaskActor;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconSleep");
			this.t.addBeh(new BehDecideWhereToSleep());
			BehaviourTaskActor behaviourTaskActor2 = new BehaviourTaskActor();
			behaviourTaskActor2.id = "sleep_inside";
			pAsset = behaviourTaskActor2;
			this.t = behaviourTaskActor2;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconHoused");
			this.t.addBeh(new BehBuildingTargetHome());
			this.t.addBeh(new BehGetTargetBuildingMainTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehStayInBuildingTarget(0f, 1f));
			this.t.addBeh(new BehTrySleep(false));
			BehaviourTaskActor behaviourTaskActor3 = new BehaviourTaskActor();
			behaviourTaskActor3.id = "sleep_outside";
			pAsset = behaviourTaskActor3;
			this.t = behaviourTaskActor3;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconHomeless");
			this.t.addBeh(new BehFindTile(TileFinderType.FreeTile));
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(0f, 2f, false));
			this.t.addBeh(new BehActorReverseFlip());
			this.t.addBeh(new BehRandomWait(0f, 2f, false));
			this.t.addBeh(new BehFindRandomNeighbourTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(0f, 2f, false));
			this.t.addBeh(new BehActorReverseFlip());
			this.t.addBeh(new BehRandomWait(0f, 2f, false));
			this.t.addBeh(new BehTrySleep(true));
		}

		// Token: 0x06004628 RID: 17960 RVA: 0x001D8990 File Offset: 0x001D6B90
		private void initTasksKings()
		{
			BehaviourTaskActor behaviourTaskActor = new BehaviourTaskActor();
			behaviourTaskActor.id = "king_check_new_city_foundation";
			BehaviourTaskActor pAsset = behaviourTaskActor;
			this.t = behaviourTaskActor;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconCity");
			this.t.addBeh(new BehKingCheckNewCityFoundation());
			BehaviourTaskActor behaviourTaskActor2 = new BehaviourTaskActor();
			behaviourTaskActor2.id = "king_change_kingdom_language";
			pAsset = behaviourTaskActor2;
			this.t = behaviourTaskActor2;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconLanguage");
			this.t.addBeh(new BehChangeKingdomLanguage());
			BehaviourTaskActor behaviourTaskActor3 = new BehaviourTaskActor();
			behaviourTaskActor3.id = "king_change_kingdom_culture";
			pAsset = behaviourTaskActor3;
			this.t = behaviourTaskActor3;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconCulture");
			this.t.addBeh(new BehChangeKingdomCulture());
			BehaviourTaskActor behaviourTaskActor4 = new BehaviourTaskActor();
			behaviourTaskActor4.id = "king_change_kingdom_religion";
			pAsset = behaviourTaskActor4;
			this.t = behaviourTaskActor4;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconReligion");
			this.t.addBeh(new BehChangeKingdomReligion());
		}

		// Token: 0x06004629 RID: 17961 RVA: 0x001D8AA0 File Offset: 0x001D6CA0
		private void initTasksWarriors()
		{
			BehaviourTaskActor behaviourTaskActor = new BehaviourTaskActor();
			behaviourTaskActor.id = "warrior_random_move";
			behaviourTaskActor.locale_key = "task_unit_move";
			BehaviourTaskActor pAsset = behaviourTaskActor;
			this.t = behaviourTaskActor;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconArrowDestination");
			this.t.addBeh(new BehFindRandomTile());
			this.t.addBeh(new BehGoToTileTarget());
			BehaviourTaskActor behaviourTaskActor2 = new BehaviourTaskActor();
			behaviourTaskActor2.id = "warrior_army_captain_idle_walking_city";
			behaviourTaskActor2.cancellable_by_socialize = true;
			behaviourTaskActor2.cancellable_by_reproduction = true;
			behaviourTaskActor2.locale_key = "task_unit_walk";
			pAsset = behaviourTaskActor2;
			this.t = behaviourTaskActor2;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconCity");
			this.t.addBeh(new BehCityActorGetRandomBorderTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(3f, 6f, false));
			BehaviourTaskActor behaviourTaskActor3 = new BehaviourTaskActor();
			behaviourTaskActor3.id = "warrior_army_captain_waiting";
			behaviourTaskActor3.cancellable_by_socialize = true;
			behaviourTaskActor3.cancellable_by_reproduction = true;
			behaviourTaskActor3.locale_key = "task_unit_walk";
			pAsset = behaviourTaskActor3;
			this.t = behaviourTaskActor3;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconCity");
			this.t.addBeh(new BehCityActorGetRandomBorderTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(10f, 20f, false));
			BehaviourTaskActor behaviourTaskActor4 = new BehaviourTaskActor();
			behaviourTaskActor4.id = "check_warrior_limit";
			pAsset = behaviourTaskActor4;
			this.t = behaviourTaskActor4;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconWar");
			this.t.addBeh(new BehCheckCityActorWarriorLimit());
			BehaviourTaskActor behaviourTaskActor5 = new BehaviourTaskActor();
			behaviourTaskActor5.id = "warrior_try_join_army_group";
			pAsset = behaviourTaskActor5;
			this.t = behaviourTaskActor5;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconWar");
			this.t.addBeh(new BehCheckCityActorArmyGroup());
			this.t.addBeh(new BehRandomWait(1f, 2f, false));
			BehaviourTaskActor behaviourTaskActor6 = new BehaviourTaskActor();
			behaviourTaskActor6.id = "warrior_army_leader_move_random";
			behaviourTaskActor6.speed_multiplier = 0.8f;
			pAsset = behaviourTaskActor6;
			this.t = behaviourTaskActor6;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconArrowDestination");
			this.t.addBeh(new BehFindRandomTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(10f, 20f, false));
			BehaviourTaskActor behaviourTaskActor7 = new BehaviourTaskActor();
			behaviourTaskActor7.id = "warrior_army_leader_move_to_attack_target";
			behaviourTaskActor7.speed_multiplier = 0.8f;
			behaviourTaskActor7.cancellable_by_socialize = false;
			behaviourTaskActor7.cancellable_by_reproduction = false;
			pAsset = behaviourTaskActor7;
			this.t = behaviourTaskActor7;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconArrowAttackTarget");
			this.t.addTaskVerifier(new BehVerifierAttackZone());
			this.t.addBeh(new BehCityActorCheckAttack());
			this.t.addBeh(new BehGoToTileTarget
			{
				limit_pathfinding_regions = 6
			});
			this.t.addBeh(new BehWarriorCaptainWait());
			this.t.addBeh(new BehRestartTask());
			BehaviourTaskActor behaviourTaskActor8 = new BehaviourTaskActor();
			behaviourTaskActor8.id = "warrior_army_follow_leader";
			behaviourTaskActor8.speed_multiplier = 1.3f;
			behaviourTaskActor8.cancellable_by_socialize = false;
			behaviourTaskActor8.cancellable_by_reproduction = false;
			pAsset = behaviourTaskActor8;
			this.t = behaviourTaskActor8;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconLoyalty");
			this.t.addBeh(new BehFindTileNearbyGroupLeader());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(1f, 2f, false));
			BehaviourTaskActor behaviourTaskActor9 = new BehaviourTaskActor();
			behaviourTaskActor9.id = "check_warrior_transport";
			behaviourTaskActor9.flag_boat_related = true;
			behaviourTaskActor9.cancellable_by_socialize = false;
			behaviourTaskActor9.cancellable_by_reproduction = false;
			pAsset = behaviourTaskActor9;
			this.t = behaviourTaskActor9;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconArrowDestination");
			this.t.addBeh(new BehCityActorWarriorTaxiCheck());
			this.t.addBeh(new BehRandomWait(1f, 5f, false));
			this.t.addBeh(new BehTaxiFindShipTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehTaxiEmbark());
			BehaviourTaskActor behaviourTaskActor10 = new BehaviourTaskActor();
			behaviourTaskActor10.id = "warrior_train_with_dummy";
			pAsset = behaviourTaskActor10;
			this.t = behaviourTaskActor10;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconWarfare");
			this.t.addBeh(new BehCityActorFindBuilding("type_training_dummies", true));
			this.t.addBeh(new BehGetTargetBuildingMainTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehLookAtBuildingTarget(0.3f));
			for (int i = 0; i < 10; i++)
			{
				this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Building, "event:/SFX/BUILDINGS/DestroyBuildingWood", 0f, 40f, true, true));
				this.t.addBeh(new BehActorAddExperience(0, 2));
				this.t.addBeh(new BehRandomWait(0f, 2f, false));
				this.t.addBeh(new BehDealDamageToTargetBuilding(0.05f, 0.15f));
			}
			this.t.addBeh(new BehActorTryToAddRandomCombatSkill());
			this.t.addBeh(new BehRandomWait(3f, 6f, false));
		}

		// Token: 0x0600462A RID: 17962 RVA: 0x001D9004 File Offset: 0x001D7204
		private void initTasksLeaders()
		{
			BehaviourTaskActor behaviourTaskActor = new BehaviourTaskActor();
			behaviourTaskActor.id = "leader_change_city_language";
			BehaviourTaskActor pAsset = behaviourTaskActor;
			this.t = behaviourTaskActor;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconLanguage");
			this.t.addBeh(new BehChangeCityActorLanguage());
			BehaviourTaskActor behaviourTaskActor2 = new BehaviourTaskActor();
			behaviourTaskActor2.id = "leader_change_city_culture";
			pAsset = behaviourTaskActor2;
			this.t = behaviourTaskActor2;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconCulture");
			this.t.addBeh(new BehChangeCityActorCulture());
			BehaviourTaskActor behaviourTaskActor3 = new BehaviourTaskActor();
			behaviourTaskActor3.id = "leader_change_city_religion";
			pAsset = behaviourTaskActor3;
			this.t = behaviourTaskActor3;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconReligion");
			this.t.addBeh(new BehChangeCityActorReligion());
		}

		// Token: 0x0600462B RID: 17963 RVA: 0x001D90D4 File Offset: 0x001D72D4
		private void initTasksStatusRelated()
		{
			BehaviourTaskActor behaviourTaskActor = new BehaviourTaskActor();
			behaviourTaskActor.id = "strange_urge_finish";
			behaviourTaskActor.locale_key = "task_strange_urge_finish";
			BehaviourTaskActor pAsset = behaviourTaskActor;
			this.t = behaviourTaskActor;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconStrangeUrge");
			for (int i = 0; i < 6; i++)
			{
				this.t.addBeh(new BehFindRandomNeighbourTile());
				this.t.addBeh(new BehGoToTileTarget());
				this.t.addBeh(new BehRandomWait(0.5f, 5f, false));
			}
			BehaviourTaskActor behaviourTaskActor2 = new BehaviourTaskActor();
			behaviourTaskActor2.id = "status_confused";
			behaviourTaskActor2.locale_key = "task_unit_confused";
			pAsset = behaviourTaskActor2;
			this.t = behaviourTaskActor2;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconConfused");
			for (int j = 0; j < 6; j++)
			{
				this.t.addBeh(new BehFindRandomNeighbourTile());
				this.t.addBeh(new BehGoToTileTarget());
				this.t.addBeh(new BehRandomWait(0f, 0.01f, false));
			}
			BehaviourTaskActor behaviourTaskActor3 = new BehaviourTaskActor();
			behaviourTaskActor3.id = "status_soul_harvested";
			behaviourTaskActor3.locale_key = "status_title_soul_harvested";
			pAsset = behaviourTaskActor3;
			this.t = behaviourTaskActor3;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconSoulHarvested");
			this.t.addBeh(new BehStartShake(1f, 0f));
			for (int k = 0; k < 12; k++)
			{
				this.t.addBeh(new BehActorReverseFlip());
				this.t.addBeh(new BehJumpingAnimation(0.1f, 0.1f));
			}
			this.t.addBeh(new BehCheckSoulBorneReproduction());
		}

		// Token: 0x0600462C RID: 17964 RVA: 0x001D9284 File Offset: 0x001D7484
		private void initTasksMobs()
		{
			BehaviourTaskActor behaviourTaskActor = new BehaviourTaskActor();
			behaviourTaskActor.id = "diet_tiles";
			behaviourTaskActor.cancellable_by_socialize = true;
			behaviourTaskActor.cancellable_by_reproduction = true;
			behaviourTaskActor.locale_key = "task_unit_eat";
			behaviourTaskActor.diet = true;
			BehaviourTaskActor pAsset = behaviourTaskActor;
			this.t = behaviourTaskActor;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/subspecies_traits/subspecies_trait_diet_geophagy");
			this.t.addBeh(new BehFindTileForEating());
			this.t.addBeh(new BehGoToTileTarget
			{
				walk_on_water = true,
				walk_on_blocks = true
			});
			for (int i = 0; i < 10; i++)
			{
				this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Tile, string.Empty, 1f, 20f, true, true));
			}
			this.t.addBeh(new BehConsumeTargetTile());
			this.t.addBeh(new BehRandomWait(1f, 2f, false));
			BehaviourTaskActor behaviourTaskActor2 = new BehaviourTaskActor();
			behaviourTaskActor2.id = "diet_wood";
			behaviourTaskActor2.cancellable_by_socialize = true;
			behaviourTaskActor2.cancellable_by_reproduction = true;
			behaviourTaskActor2.locale_key = "task_unit_eat";
			behaviourTaskActor2.diet = true;
			pAsset = behaviourTaskActor2;
			this.t = behaviourTaskActor2;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/subspecies_traits/subspecies_trait_diet_xylophagy");
			this.t.addBeh(new BehFindBuilding("type_tree", false, true));
			this.t.addBeh(new BehGoToBuildingTarget(false));
			for (int j = 0; j < 4; j++)
			{
				this.t.addBeh(new BehResourceGatheringAnimation(1f, "event:/SFX/NATURE/AnimalEatPlant", true));
			}
			this.t.addBeh(new BehConsumeTargetBuilding());
			this.t.addBeh(new BehRandomWait(1f, 2f, false));
			BehaviourTaskActor behaviourTaskActor3 = new BehaviourTaskActor();
			behaviourTaskActor3.id = "diet_minerals";
			behaviourTaskActor3.cancellable_by_socialize = true;
			behaviourTaskActor3.cancellable_by_reproduction = true;
			behaviourTaskActor3.locale_key = "task_unit_eat";
			behaviourTaskActor3.diet = true;
			pAsset = behaviourTaskActor3;
			this.t = behaviourTaskActor3;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/subspecies_traits/subspecies_trait_diet_lithotroph");
			this.t.addBeh(new BehFindBuilding("type_mineral", false, true));
			this.t.addBeh(new BehGoToBuildingTarget(false));
			for (int k = 0; k < 4; k++)
			{
				this.t.addBeh(new BehResourceGatheringAnimation(1f, "event:/SFX/NATURE/AnimalEatPlant", true));
			}
			this.t.addBeh(new BehConsumeTargetBuilding());
			this.t.addBeh(new BehRandomWait(1f, 2f, false));
			BehaviourTaskActor behaviourTaskActor4 = new BehaviourTaskActor();
			behaviourTaskActor4.id = "diet_fruits";
			behaviourTaskActor4.cancellable_by_socialize = true;
			behaviourTaskActor4.cancellable_by_reproduction = true;
			behaviourTaskActor4.locale_key = "task_unit_eat";
			behaviourTaskActor4.diet = true;
			pAsset = behaviourTaskActor4;
			this.t = behaviourTaskActor4;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/subspecies_traits/subspecies_trait_diet_frugivore");
			this.t.addBeh(new BehFindBuilding("type_fruits", false, true));
			this.t.addBeh(new BehGoToBuildingTarget(false));
			for (int l = 0; l < 4; l++)
			{
				this.t.addBeh(new BehResourceGatheringAnimation(1f, "event:/SFX/NATURE/AnimalEatPlant", true));
			}
			this.t.addBeh(new BehConsumeTargetBuilding());
			this.t.addBeh(new BehRandomWait(1f, 2f, false));
			BehaviourTaskActor behaviourTaskActor5 = new BehaviourTaskActor();
			behaviourTaskActor5.id = "diet_vegetation";
			behaviourTaskActor5.cancellable_by_socialize = true;
			behaviourTaskActor5.cancellable_by_reproduction = true;
			behaviourTaskActor5.locale_key = "task_unit_eat";
			behaviourTaskActor5.diet = true;
			pAsset = behaviourTaskActor5;
			this.t = behaviourTaskActor5;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/subspecies_traits/subspecies_trait_diet_folivore");
			this.t.addBeh(new BehFindBuilding("type_vegetation", false, true));
			this.t.addBeh(new BehGoToBuildingTarget(false));
			for (int m = 0; m < 4; m++)
			{
				this.t.addBeh(new BehResourceGatheringAnimation(1f, "event:/SFX/NATURE/AnimalEatPlant", true));
			}
			this.t.addBeh(new BehConsumeTargetBuilding());
			this.t.addBeh(new BehRandomWait(1f, 2f, false));
			BehaviourTaskActor behaviourTaskActor6 = new BehaviourTaskActor();
			behaviourTaskActor6.id = "diet_flowers";
			behaviourTaskActor6.cancellable_by_socialize = true;
			behaviourTaskActor6.cancellable_by_reproduction = true;
			behaviourTaskActor6.locale_key = "task_unit_eat";
			behaviourTaskActor6.diet = true;
			pAsset = behaviourTaskActor6;
			this.t = behaviourTaskActor6;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/subspecies_traits/subspecies_trait_diet_florivore");
			this.t.addBeh(new BehFindBuilding("type_flower", false, true));
			this.t.addBeh(new BehGoToBuildingTarget(false));
			for (int n = 0; n < 4; n++)
			{
				this.t.addBeh(new BehResourceGatheringAnimation(1f, "event:/SFX/NATURE/AnimalEatPlant", true));
			}
			this.t.addBeh(new BehConsumeTargetBuilding());
			this.t.addBeh(new BehRandomWait(1f, 2f, false));
			BehaviourTaskActor behaviourTaskActor7 = new BehaviourTaskActor();
			behaviourTaskActor7.id = "diet_nectar";
			behaviourTaskActor7.cancellable_by_socialize = true;
			behaviourTaskActor7.cancellable_by_reproduction = true;
			behaviourTaskActor7.locale_key = "task_unit_eat";
			behaviourTaskActor7.diet = true;
			pAsset = behaviourTaskActor7;
			this.t = behaviourTaskActor7;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/subspecies_traits/subspecies_trait_diet_florivore");
			this.t.addBeh(new BehFindBuilding("type_flower", false, true));
			this.t.addBeh(new BehGoToBuildingTarget(false));
			for (int i2 = 0; i2 < 4; i2++)
			{
				this.t.addBeh(new BehResourceGatheringAnimation(1f, "event:/SFX/NATURE/AnimalEatPlant", true));
			}
			this.t.addBeh(new BehNectarNectarFromFlower());
			this.t.addBeh(new BehRandomWait(1f, 2f, false));
			BehaviourTaskActor behaviourTaskActor8 = new BehaviourTaskActor();
			behaviourTaskActor8.id = "diet_crops";
			behaviourTaskActor8.cancellable_by_socialize = true;
			behaviourTaskActor8.cancellable_by_reproduction = true;
			behaviourTaskActor8.locale_key = "task_unit_eat";
			behaviourTaskActor8.diet = true;
			pAsset = behaviourTaskActor8;
			this.t = behaviourTaskActor8;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/subspecies_traits/subspecies_trait_diet_granivore");
			this.t.addBeh(new BehFindBuilding("type_crops", false, true));
			this.t.addBeh(new BehGoToBuildingTarget(false));
			for (int i3 = 0; i3 < 4; i3++)
			{
				this.t.addBeh(new BehResourceGatheringAnimation(1f, "event:/SFX/NATURE/AnimalEatPlant", true));
			}
			this.t.addBeh(new BehConsumeTargetBuilding());
			this.t.addBeh(new BehRandomWait(1f, 2f, false));
			BehaviourTaskActor behaviourTaskActor9 = new BehaviourTaskActor();
			behaviourTaskActor9.id = "diet_grass";
			behaviourTaskActor9.cancellable_by_socialize = true;
			behaviourTaskActor9.cancellable_by_reproduction = true;
			behaviourTaskActor9.locale_key = "task_unit_eat";
			behaviourTaskActor9.diet = true;
			pAsset = behaviourTaskActor9;
			this.t = behaviourTaskActor9;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/subspecies_traits/subspecies_trait_diet_graminivore");
			this.t.addBeh(new BehFindTile(TileFinderType.Grass));
			this.t.addBeh(new BehGoToTileTarget());
			for (int i4 = 0; i4 < 4; i4++)
			{
				this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Tile, string.Empty, 1f, 20f, true, true));
			}
			this.t.addBeh(new BehConsumeGrass());
			this.t.addBeh(new BehRandomWait(1f, 2f, false));
			BehaviourTaskActor behaviourTaskActor10 = new BehaviourTaskActor();
			behaviourTaskActor10.id = "diet_meat";
			behaviourTaskActor10.cancellable_by_reproduction = true;
			behaviourTaskActor10.ignore_fight_check = true;
			behaviourTaskActor10.locale_key = "task_unit_eat";
			behaviourTaskActor10.diet = true;
			pAsset = behaviourTaskActor10;
			this.t = behaviourTaskActor10;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/subspecies_traits/subspecies_trait_diet_carnivore");
			this.t.addBeh(new BehFindMeatSource(MeatTargetType.Meat, true));
			this.t.addBeh(new BehGoToActorTarget(GoToActorTargetType.SameTile, false, true, true, 2f, true, true));
			this.t.addBeh(new BehConsumeActorTarget());
			BehaviourTaskActor behaviourTaskActor11 = new BehaviourTaskActor();
			behaviourTaskActor11.id = "diet_blood";
			behaviourTaskActor11.cancellable_by_reproduction = true;
			behaviourTaskActor11.ignore_fight_check = true;
			behaviourTaskActor11.locale_key = "task_unit_eat";
			behaviourTaskActor11.diet = true;
			pAsset = behaviourTaskActor11;
			this.t = behaviourTaskActor11;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/subspecies_traits/subspecies_trait_diet_hematophagy");
			this.t.addBeh(new BehFindMeatSource(MeatTargetType.Meat, false));
			this.t.addBeh(new BehGoToActorTarget(GoToActorTargetType.SameTile, false, true, true, 2f, true, true));
			this.t.addBeh(new BehConsumeActorsBloodTarget());
			BehaviourTaskActor behaviourTaskActor12 = new BehaviourTaskActor();
			behaviourTaskActor12.id = "diet_meat_insect";
			behaviourTaskActor12.locale_key = "task_unit_eat";
			behaviourTaskActor12.diet = true;
			pAsset = behaviourTaskActor12;
			this.t = behaviourTaskActor12;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/subspecies_traits/subspecies_trait_diet_insectivore");
			this.t.addBeh(new BehFindMeatInsectSource(true));
			this.t.addBeh(new BehGoToActorTarget(GoToActorTargetType.SameTile, false, true, true, 2f, true, true));
			this.t.addBeh(new BehConsumeActorTarget());
			BehaviourTaskActor behaviourTaskActor13 = new BehaviourTaskActor();
			behaviourTaskActor13.id = "diet_same_species";
			behaviourTaskActor13.locale_key = "task_unit_eat";
			behaviourTaskActor13.diet = true;
			pAsset = behaviourTaskActor13;
			this.t = behaviourTaskActor13;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/subspecies_traits/subspecies_trait_diet_cannibalism");
			this.t.addBeh(new BehFindMeatSameSpeciesSource(false));
			this.t.addBeh(new BehGoToActorTarget(GoToActorTargetType.SameTile, false, true, true, 2f, true, true));
			this.t.addBeh(new BehConsumeActorTarget());
			BehaviourTaskActor behaviourTaskActor14 = new BehaviourTaskActor();
			behaviourTaskActor14.id = "diet_algae";
			behaviourTaskActor14.locale_key = "task_unit_eat";
			behaviourTaskActor14.diet = true;
			pAsset = behaviourTaskActor14;
			this.t = behaviourTaskActor14;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/subspecies_traits/subspecies_trait_diet_algivore");
			this.t.addBeh(new BehFindTile(TileFinderType.Water));
			this.t.addBeh(new BehGoOrSwimToTileTarget());
			for (int k2 = 0; k2 < 4; k2++)
			{
				this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Tile, string.Empty, 1f, 20f, true, true));
			}
			this.t.addBeh(new BehReplenishNutrition());
			BehaviourTaskActor behaviourTaskActor15 = new BehaviourTaskActor();
			behaviourTaskActor15.id = "diet_fish";
			behaviourTaskActor15.locale_key = "task_unit_eat";
			behaviourTaskActor15.diet = true;
			pAsset = behaviourTaskActor15;
			this.t = behaviourTaskActor15;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/subspecies_traits/subspecies_trait_diet_piscivore");
			this.t.addBeh(new BehFindTile(TileFinderType.Water));
			this.t.addBeh(new BehGoOrSwimToTileTarget());
			for (int k3 = 0; k3 < 4; k3++)
			{
				this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Tile, string.Empty, 1f, 20f, true, true));
			}
			this.t.addBeh(new BehReplenishNutrition());
			BehaviourTaskActor behaviourTaskActor16 = new BehaviourTaskActor();
			behaviourTaskActor16.id = "family_alpha_move";
			behaviourTaskActor16.cancellable_by_socialize = true;
			behaviourTaskActor16.cancellable_by_reproduction = true;
			behaviourTaskActor16.locale_key = "task_unit_move";
			pAsset = behaviourTaskActor16;
			this.t = behaviourTaskActor16;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/actor_traits/iconStrong");
			this.t.addBeh(new BehFamilyAlphaMove());
			this.t.addBeh(new BehGoToTileTarget());
			BehaviourTaskActor behaviourTaskActor17 = new BehaviourTaskActor();
			behaviourTaskActor17.id = "family_group_follow";
			behaviourTaskActor17.cancellable_by_socialize = true;
			behaviourTaskActor17.cancellable_by_reproduction = true;
			behaviourTaskActor17.locale_key = "task_unit_follow_family";
			pAsset = behaviourTaskActor17;
			this.t = behaviourTaskActor17;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconFamiliesZones");
			this.t.addBeh(new BehFamilyFollowAlpha());
			this.t.addBeh(new BehGoToActorTarget(GoToActorTargetType.SameRegion, false, false, false, 2f, true, true));
			BehaviourTaskActor behaviourTaskActor18 = new BehaviourTaskActor();
			behaviourTaskActor18.id = "family_check_existence";
			pAsset = behaviourTaskActor18;
			this.t = behaviourTaskActor18;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconFamiliesZones");
			this.t.addBeh(new BehFamilyCheckMembers());
			BehaviourTaskActor behaviourTaskActor19 = new BehaviourTaskActor();
			behaviourTaskActor19.id = "family_group_leave";
			pAsset = behaviourTaskActor19;
			this.t = behaviourTaskActor19;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconFamiliesZones");
			this.t.addBeh(new BehFamilyGroupLeave());
			BehaviourTaskActor behaviourTaskActor20 = new BehaviourTaskActor();
			behaviourTaskActor20.id = "family_group_join_or_new_herd";
			pAsset = behaviourTaskActor20;
			this.t = behaviourTaskActor20;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconFamiliesZones");
			this.t.addBeh(new BehFamilyGroupJoin());
			this.t.addBeh(new BehFamilyGroupNew());
			BehaviourTaskActor behaviourTaskActor21 = new BehaviourTaskActor();
			behaviourTaskActor21.id = "attack_golden_brain";
			pAsset = behaviourTaskActor21;
			this.t = behaviourTaskActor21;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconGoldBrain");
			this.t.addBeh(new BehFindGoldenBrain());
			this.t.addBeh(new BehGoToBuildingTarget(false));
			BehaviourTaskActor behaviourTaskActor22 = new BehaviourTaskActor();
			behaviourTaskActor22.id = "follow_desire_target";
			behaviourTaskActor22.locale_key = "task_unit_weird_desire";
			pAsset = behaviourTaskActor22;
			this.t = behaviourTaskActor22;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconGoldBrain");
			this.t.addBeh(new BehFindDesireWaypoint());
			this.t.addBeh(new BehGoToBuildingTarget(false));
			BehaviourTaskActor behaviourTaskActor23 = new BehaviourTaskActor();
			behaviourTaskActor23.id = "crab_eat";
			behaviourTaskActor23.diet = true;
			pAsset = behaviourTaskActor23;
			this.t = behaviourTaskActor23;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconCrab");
			this.t.addBeh(new BehAnimalCheckHungry());
			this.t.addBeh(new BehRandomWait(2f, 5f, false));
			this.t.addBeh(new BehFindTileBeach());
			this.t.addBeh(new BehGoOrSwimToTileTarget());
			for (int i5 = 0; i5 < 4; i5++)
			{
				this.t.addBeh(new BehAngleAnimation(AngleAnimationTarget.Tile, string.Empty, 1f, 20f, true, true));
			}
			this.t.addBeh(new BehReplenishNutrition());
			BehaviourTaskActor behaviourTaskActor24 = new BehaviourTaskActor();
			behaviourTaskActor24.id = "crab_danger_check";
			pAsset = behaviourTaskActor24;
			this.t = behaviourTaskActor24;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconBloodRain");
			this.t.addBeh(new BehCheckIfOnGround());
			this.t.addBeh(new BehActiveCrabDangerCheck());
			BehaviourTaskActor behaviourTaskActor25 = new BehaviourTaskActor();
			behaviourTaskActor25.id = "crab_burrow";
			pAsset = behaviourTaskActor25;
			this.t = behaviourTaskActor25;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconCrab");
			this.t.addBeh(new BehCheckIfOnGround());
			this.t.addBeh(new BehFindRandomTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehCrabBurrow());
			BehaviourTaskActor behaviourTaskActor26 = new BehaviourTaskActor();
			behaviourTaskActor26.id = "make_skeleton";
			pAsset = behaviourTaskActor26;
			this.t = behaviourTaskActor26;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconSkeleton");
			this.t.addBeh(new BehMagicMakeSkeleton());
			this.t.addBeh(new BehRandomWait(3f, 3f, false));
			BehaviourTaskActor behaviourTaskActor27 = new BehaviourTaskActor();
			behaviourTaskActor27.id = "skeleton_move";
			behaviourTaskActor27.locale_key = "task_unit_move";
			pAsset = behaviourTaskActor27;
			this.t = behaviourTaskActor27;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconArrowDestination");
			this.t.addBeh(new BehSkeletonFindTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(3f, 6f, false));
			BehaviourTaskActor behaviourTaskActor28 = new BehaviourTaskActor();
			behaviourTaskActor28.id = "spawn_fertilizer";
			pAsset = behaviourTaskActor28;
			this.t = behaviourTaskActor28;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconFertilizerPlants");
			this.t.addBeh(new BehSpawnTreeFertilizer());
			this.t.addBeh(new BehRandomWait(1f, 3f, false));
			BehaviourTaskActor behaviourTaskActor29 = new BehaviourTaskActor();
			behaviourTaskActor29.id = "check_cure";
			pAsset = behaviourTaskActor29;
			this.t = behaviourTaskActor29;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconHealth");
			this.t.addBeh(new BehCheckCure());
			this.t.addBeh(new BehRandomWait(1f, 3f, false));
			BehaviourTaskActor behaviourTaskActor30 = new BehaviourTaskActor();
			behaviourTaskActor30.id = "burn_tumors";
			pAsset = behaviourTaskActor30;
			this.t = behaviourTaskActor30;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconFire");
			this.t.addBeh(new BehBurnTumorTiles());
			this.t.addBeh(new BehRandomWait(1f, 3f, false));
			BehaviourTaskActor behaviourTaskActor31 = new BehaviourTaskActor();
			behaviourTaskActor31.id = "check_heal";
			pAsset = behaviourTaskActor31;
			this.t = behaviourTaskActor31;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconHealth");
			this.t.addBeh(new BehHeal());
			this.t.addBeh(new BehRandomWait(1f, 3f, false));
			BehaviourTaskActor behaviourTaskActor32 = new BehaviourTaskActor();
			behaviourTaskActor32.id = "random_teleport";
			pAsset = behaviourTaskActor32;
			this.t = behaviourTaskActor32;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/actor_traits/iconMageSlayer");
			this.t.addBeh(new BehRandomTeleport());
			this.t.addBeh(new BehRandomWait(0.5f, 1f, false));
			BehaviourTaskActor behaviourTaskActor33 = new BehaviourTaskActor();
			behaviourTaskActor33.id = "teleport_back_home";
			pAsset = behaviourTaskActor33;
			this.t = behaviourTaskActor33;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/actor_traits/iconMageSlayer");
			this.t.addBeh(new BehTeleportHome());
			this.t.addBeh(new BehRandomWait(0.5f, 1f, false));
			BehaviourTaskActor behaviourTaskActor34 = new BehaviourTaskActor();
			behaviourTaskActor34.id = "run_to_water_when_on_fire";
			behaviourTaskActor34.locale_key = "task_unit_run_to_water";
			pAsset = behaviourTaskActor34;
			this.t = behaviourTaskActor34;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconFire");
			this.t.addBeh(new BehShortRandomMove());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehShortRandomMove());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehShortRandomMove());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehFindTileWhenOnFire());
			this.t.addBeh(new BehGoOrSwimToTileTarget());
			BehaviourTaskActor behaviourTaskActor35 = new BehaviourTaskActor();
			behaviourTaskActor35.id = "short_move";
			behaviourTaskActor35.cancellable_by_socialize = true;
			behaviourTaskActor35.locale_key = "task_unit_move";
			pAsset = behaviourTaskActor35;
			this.t = behaviourTaskActor35;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconArrowDestination");
			this.t.addBeh(new BehShortRandomMove());
			this.t.addBeh(new BehGoToTileTarget());
		}

		// Token: 0x0600462D RID: 17965 RVA: 0x001DA520 File Offset: 0x001D8720
		private void initTasksFingers()
		{
			BehaviourTaskActor behaviourTaskActor = new BehaviourTaskActor();
			behaviourTaskActor.id = "godfinger_find_target";
			BehaviourTaskActor pAsset = behaviourTaskActor;
			this.t = behaviourTaskActor;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconGodFinger");
			this.t.addBeh(new BehFingerSetFlying(true, -1f));
			this.t.addBeh(new BehFingerFindTarget());
			this.t.addBeh(new BehFingerGoTowardsTileTarget(5));
			this.t.addBeh(new BehFingerSetFlying(true, 2f));
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehSetNextTask("godfinger_draw", true, false));
			BehaviourTaskActor behaviourTaskActor2 = new BehaviourTaskActor();
			behaviourTaskActor2.id = "godfinger_draw";
			pAsset = behaviourTaskActor2;
			this.t = behaviourTaskActor2;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconGodFinger");
			this.t.addBeh(new BehFingerCheckCanDraw());
			this.t.addBeh(new BehFingerSetFlying(false, -1f));
			this.t.addBeh(new BehFingerFindCloseTile());
			this.t.addBeh(new BehFingerWaitForFlying());
			this.t.addBeh(new BehFingerDrawToTileTarget());
			this.t.addBeh(new BehSetNextTask("godfinger_find_target", true, false));
			BehaviourTaskActor behaviourTaskActor3 = new BehaviourTaskActor();
			behaviourTaskActor3.id = "godfinger_move";
			pAsset = behaviourTaskActor3;
			this.t = behaviourTaskActor3;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconGodFinger");
			this.t.addBeh(new BehFingerSetFlying(true, -1f));
			this.t.addBeh(new BehRandomWait(0.1f, 0.4f, false));
			this.t.addBeh(new BehFingerFindRandomTile(75));
			this.t.addBeh(new BehFingerGoTowardsTileTarget(25));
			BehaviourTaskActor behaviourTaskActor4 = new BehaviourTaskActor();
			behaviourTaskActor4.id = "godfinger_random_fun_move";
			pAsset = behaviourTaskActor4;
			this.t = behaviourTaskActor4;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconGodFinger");
			this.t.addBeh(new BehFingerSetFlying(true, -1f));
			for (int i = 0; i < 6; i++)
			{
				this.t.addBeh(new BehFingerFindRandomTile(5));
				this.t.addBeh(new BehFingerGoTowardsTileTarget(5));
				this.t.addBeh(new BehRandomWait(0f, 0.01f, false));
			}
			BehaviourTaskActor behaviourTaskActor5 = new BehaviourTaskActor();
			behaviourTaskActor5.id = "godfinger_circle_move";
			pAsset = behaviourTaskActor5;
			this.t = behaviourTaskActor5;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconGodFinger");
			this.t.addBeh(new BehFingerSetFlying(true, -1f));
			this.t.addBeh(new BehRandomWait(0.1f, 0.4f, false));
			this.t.addBeh(new BehFingerGoToCircleTarget(25, 75));
			BehaviourTaskActor behaviourTaskActor6 = new BehaviourTaskActor();
			behaviourTaskActor6.id = "godfinger_circle_move_small";
			pAsset = behaviourTaskActor6;
			this.t = behaviourTaskActor6;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconGodFinger");
			this.t.addBeh(new BehFingerSetFlying(true, -1f));
			this.t.addBeh(new BehRandomWait(0.1f, 0.4f, false));
			this.t.addBeh(new BehFingerGoToCircleTarget(5, 15));
			BehaviourTaskActor behaviourTaskActor7 = new BehaviourTaskActor();
			behaviourTaskActor7.id = "godfinger_circle_move_big";
			pAsset = behaviourTaskActor7;
			this.t = behaviourTaskActor7;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconGodFinger");
			this.t.addBeh(new BehFingerSetFlying(true, -1f));
			this.t.addBeh(new BehRandomWait(0.1f, 0.4f, false));
			this.t.addBeh(new BehFingerGoToCircleTarget(75, 150));
		}

		// Token: 0x0600462E RID: 17966 RVA: 0x001DA8E4 File Offset: 0x001D8AE4
		private void initTasksDragons()
		{
			BehaviourTaskActor behaviourTaskActor = new BehaviourTaskActor();
			behaviourTaskActor.id = "dragon_fly";
			behaviourTaskActor.locale_key = "task_unit_dragon_fly";
			BehaviourTaskActor pAsset = behaviourTaskActor;
			this.t = behaviourTaskActor;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconDragon");
			this.t.addBeh(new BehDragonSetAnimation(DragonState.Fly, true, false));
			this.t.addBeh(new BehActorSetFlying(true));
			this.t.addBeh(new BehDragonSleepy());
			this.t.addBeh(new BehDragonCheckAttackTargetAlive());
			this.t.addBeh(new BehDragonZombieFindGoldenBrain());
			this.t.addBeh(new BehDragonCheckAttackTile());
			this.t.addBeh(new BehDragonCheckAttackCity());
			this.t.addBeh(new BehDragonFindRandomTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRepeatTaskChance(0.7f));
			BehaviourTaskActor behaviourTaskActor2 = new BehaviourTaskActor();
			behaviourTaskActor2.id = "dragon_sleep";
			behaviourTaskActor2.locale_key = "task_unit_dragon_sleep";
			pAsset = behaviourTaskActor2;
			this.t = behaviourTaskActor2;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconSleep");
			this.t.addBeh(new BehDragonSetAnimation(DragonState.SleepStart, false, true));
			this.t.addBeh(new BehActorSetFlip(false));
			this.t.addBeh(new BehActorSetFlying(false));
			this.t.addBeh(new BehDragonFinishAnimation());
			this.t.addBeh(new BehDragonSetAnimation(DragonState.SleepLoop, true, true));
			this.t.addBeh(new BehDragonSleep());
			this.t.addBeh(new BehDragonFinishAnimation());
			this.t.addBeh(new BehSetNextTask("dragon_wakeup", true, false));
			BehaviourTaskActor behaviourTaskActor3 = new BehaviourTaskActor();
			behaviourTaskActor3.id = "dragon_wakeup";
			behaviourTaskActor3.locale_key = "task_unit_dragon_sleep";
			pAsset = behaviourTaskActor3;
			this.t = behaviourTaskActor3;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconDragon");
			this.t.addBeh(new BehDragonSetAnimation(DragonState.SleepUp, false, true));
			this.t.addBeh(new BehActorSetFlip(false));
			this.t.addBeh(new BehDragonFinishAnimation());
			BehaviourTaskActor behaviourTaskActor4 = new BehaviourTaskActor();
			behaviourTaskActor4.id = "dragon_slide";
			behaviourTaskActor4.locale_key = "task_unit_dragon_attack";
			pAsset = behaviourTaskActor4;
			this.t = behaviourTaskActor4;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconFire");
			this.t.addBeh(new BehDragonSetAnimation(DragonState.Slide, false, true));
			this.t.addBeh(new BehActorSetFlying(true));
			this.t.addBeh(new BehDragonSleepy());
			this.t.addBeh(new BehActorAddStatus("invincible", 2f, true, false));
			this.t.addBeh(new BehActorSetBool("justSlid", true));
			this.t.addBeh(new BehDragonSlide());
			BehaviourTaskActor behaviourTaskActor5 = new BehaviourTaskActor();
			behaviourTaskActor5.id = "dragon_land";
			behaviourTaskActor5.locale_key = "task_unit_dragon_land";
			pAsset = behaviourTaskActor5;
			this.t = behaviourTaskActor5;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconDragon");
			this.t.addBeh(new BehDragonSetAnimation(DragonState.Landing, false, true));
			this.t.addBeh(new BehDragonSleepy());
			this.t.addBeh(new BehDragonCheckAttackTargetAlive());
			this.t.addBeh(new BehDragonCheckOverTargetCity());
			this.t.addBeh(new BehDragonFinishAnimation());
			this.t.addBeh(new BehActorSetFlying(false));
			this.t.addBeh(new BehDragonCantLand("dragon_up"));
			this.t.addBeh(new BehDragonLanded());
			this.t.addBeh(new BehDragonCheckOverTargetActor());
			this.t.addBeh(new BehDragonCheckOverTargetCity());
			this.t.addBeh(new BehActorSetInt("justLanded", 2));
			BehaviourTaskActor behaviourTaskActor6 = new BehaviourTaskActor();
			behaviourTaskActor6.id = "dragon_land_attack";
			behaviourTaskActor6.locale_key = "task_unit_dragon_attack";
			pAsset = behaviourTaskActor6;
			this.t = behaviourTaskActor6;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconFire");
			this.t.addBeh(new BehDragonSetAnimation(DragonState.LandAttack, false, true));
			this.t.addBeh(new BehDragonSleepy());
			this.t.addBeh(new BehActorSetFlying(false));
			this.t.addBeh(new BehDragonLandAttack());
			BehaviourTaskActor behaviourTaskActor7 = new BehaviourTaskActor();
			behaviourTaskActor7.id = "dragon_up";
			behaviourTaskActor7.locale_key = "task_unit_dragon_fly";
			pAsset = behaviourTaskActor7;
			this.t = behaviourTaskActor7;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconDragon");
			this.t.addBeh(new BehDragonSetAnimation(DragonState.Up, false, true));
			this.t.addBeh(new BehDragonFlyUp());
			this.t.addBeh(new BehDragonSleepy());
			this.t.addBeh(new BehActorSetFlying(true));
			this.t.addBeh(new BehDragonFinishAnimation());
			this.t.addBeh(new BehActorSetBool("justUp", true));
			this.t.addBeh(new BehActorCheckBool("justGotHit", "dragon_fly"));
			BehaviourTaskActor behaviourTaskActor8 = new BehaviourTaskActor();
			behaviourTaskActor8.id = "dragon_idle";
			behaviourTaskActor8.locale_key = "task_unit_dragon_normal";
			pAsset = behaviourTaskActor8;
			this.t = behaviourTaskActor8;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconDragon");
			this.t.addBeh(new BehDragonSetAnimation(DragonState.Idle, true, true));
			this.t.addBeh(new BehActorSetFlying(false));
			this.t.addBeh(new BehDragonIdle());
			this.t.addBeh(new BehDragonFinishAnimation());
		}

		// Token: 0x0600462F RID: 17967 RVA: 0x001DAE78 File Offset: 0x001D9078
		private void initTasksUFOs()
		{
			BehaviourTaskActor behaviourTaskActor = new BehaviourTaskActor();
			behaviourTaskActor.id = "ufo_idle";
			BehaviourTaskActor pAsset = behaviourTaskActor;
			this.t = behaviourTaskActor;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconUFO");
			this.t.addBeh(new BehSetActorSpeed(20f));
			this.t.addBeh(new BehUFOBeam(false));
			this.t.addBeh(new BehUFOCheckExplore());
			BehaviourTaskActor behaviourTaskActor2 = new BehaviourTaskActor();
			behaviourTaskActor2.id = "ufo_hit";
			pAsset = behaviourTaskActor2;
			this.t = behaviourTaskActor2;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconUFO");
			this.t.addBeh(new BehUFOSelectTarget());
			BehaviourTaskActor behaviourTaskActor3 = new BehaviourTaskActor();
			behaviourTaskActor3.id = "ufo_flee";
			pAsset = behaviourTaskActor3;
			this.t = behaviourTaskActor3;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/actor_traits/iconAgile");
			this.t.addBeh(new BehSetActorSpeed(100f));
			this.t.addBeh(new BehUFOBeam(false));
			this.t.addBeh(new BehGetRandomZoneTile());
			this.t.addBeh(new BehGoToTileTarget());
			BehaviourTaskActor behaviourTaskActor4 = new BehaviourTaskActor();
			behaviourTaskActor4.id = "ufo_fly";
			pAsset = behaviourTaskActor4;
			this.t = behaviourTaskActor4;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconUFO");
			this.t.addBeh(new BehSetActorSpeed(20f));
			this.t.addBeh(new BehUFOBeam(false));
			this.t.addBeh(new BehUFOFindTarget());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehUFOCheckAttackCity());
			this.t.addBeh(new BehUFOExplore());
			BehaviourTaskActor behaviourTaskActor5 = new BehaviourTaskActor();
			behaviourTaskActor5.id = "ufo_explore";
			pAsset = behaviourTaskActor5;
			this.t = behaviourTaskActor5;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconInspectZoneToggle");
			this.t.addBeh(new BehSetActorSpeed(10f));
			this.t.addBeh(new BehUFOBeam(false));
			this.t.addBeh(new BehGetRandomZoneTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehUFOCheckExplore());
			BehaviourTaskActor behaviourTaskActor6 = new BehaviourTaskActor();
			behaviourTaskActor6.id = "ufo_chase";
			pAsset = behaviourTaskActor6;
			this.t = behaviourTaskActor6;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconUFO");
			this.t.addBeh(new BehSetActorSpeed(50f));
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehSetNextTask("ufo_attack", true, false));
			BehaviourTaskActor behaviourTaskActor7 = new BehaviourTaskActor();
			behaviourTaskActor7.id = "ufo_attack";
			pAsset = behaviourTaskActor7;
			this.t = behaviourTaskActor7;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconArrowAttackTarget");
			this.t.addBeh(new BehUFOBeam(true));
		}

		// Token: 0x06004630 RID: 17968 RVA: 0x001DB16C File Offset: 0x001D936C
		private void initTasksBoats()
		{
			BehaviourTaskActor behaviourTaskActor = new BehaviourTaskActor();
			behaviourTaskActor.id = "boat_check_existence";
			BehaviourTaskActor pAsset = behaviourTaskActor;
			this.t = behaviourTaskActor;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconBoat");
			this.t.addBeh(new BehBoatCheckExistence());
			BehaviourTaskActor behaviourTaskActor2 = new BehaviourTaskActor();
			behaviourTaskActor2.id = "boat_check_limits";
			behaviourTaskActor2.locale_key = "task_unit_move";
			pAsset = behaviourTaskActor2;
			this.t = behaviourTaskActor2;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconBoat");
			this.t.addBeh(new BehBoatCheckLimit());
			this.t.addBeh(new BehBoatCheckHomeDocks());
			this.t.addBeh(new BehBoatSetHomeDockTarget());
			this.t.addBeh(new BehBoatFindTileInDock());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(0f, 3f, false));
			this.t.addBeh(new BehStayInBuildingTarget(2f, 4f));
			this.t.addBeh(new BehBoatRemoveIfLimit());
			this.t.addBeh(new BehExitBuilding());
			BehaviourTaskActor behaviourTaskActor3 = new BehaviourTaskActor();
			behaviourTaskActor3.id = "boat_idle";
			behaviourTaskActor3.locale_key = "task_unit_nothing";
			pAsset = behaviourTaskActor3;
			this.t = behaviourTaskActor3;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconClock");
			this.t.addBeh(new BehBoatDamageCheck());
			this.t.addBeh(new BehBoatFindOceanNeutralTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(3f, 10f, false));
			BehaviourTaskActor behaviourTaskActor4 = new BehaviourTaskActor();
			behaviourTaskActor4.id = "boat_danger_check";
			behaviourTaskActor4.locale_key = "task_unit_flee";
			pAsset = behaviourTaskActor4;
			this.t = behaviourTaskActor4;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconBoat");
			this.t.addBeh(new BehCheckIfInLiquid());
			this.t.addBeh(new BehBoatDangerCheck());
			this.t.addBeh(new BehBoatFindWaterTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(3f, 10f, false));
			BehaviourTaskActor behaviourTaskActor5 = new BehaviourTaskActor();
			behaviourTaskActor5.id = "boat_transport_check";
			pAsset = behaviourTaskActor5;
			this.t = behaviourTaskActor5;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconBoat");
			this.t.addBeh(new BehRandomWait(0f, 1f, false));
			this.t.addBeh(new BehBoatTransportCheck());
			BehaviourTaskActor behaviourTaskActor6 = new BehaviourTaskActor();
			behaviourTaskActor6.id = "boat_transport_check_taxi";
			pAsset = behaviourTaskActor6;
			this.t = behaviourTaskActor6;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconBoat");
			this.t.addBeh(new BehBoatFindRequest());
			BehaviourTaskActor behaviourTaskActor7 = new BehaviourTaskActor();
			behaviourTaskActor7.id = "boat_transport_go_load";
			behaviourTaskActor7.locale_key = "task_unit_boat_load_units";
			pAsset = behaviourTaskActor7;
			this.t = behaviourTaskActor7;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconCityInventory");
			this.t.addBeh(new BehBoatTransportFindTilePickUp());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(0f, 1f, false));
			this.t.addBeh(new BehBoatTransportDoLoading());
			this.t.addBeh(new BehSetNextTask("boat_transport_go_unload", false, false));
			BehaviourTaskActor behaviourTaskActor8 = new BehaviourTaskActor();
			behaviourTaskActor8.id = "boat_transport_go_unload";
			behaviourTaskActor8.locale_key = "task_unit_boat_unload_units";
			pAsset = behaviourTaskActor8;
			this.t = behaviourTaskActor8;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconCityInventory");
			this.t.addBeh(new BehRandomWait(0f, 1f, false));
			this.t.addBeh(new BehBoatTransportFindTileUnload());
			this.t.addBeh(new BehGoToTileTarget
			{
				walk_on_water = true
			});
			this.t.addBeh(new BehRandomWait(0f, 1f, false));
			this.t.addBeh(new BehBoatTransportUnloadUnits());
			this.t.addBeh(new BehRandomWait(3f, 10f, false));
			this.t.addBeh(new BehEndJob());
			BehaviourTaskActor behaviourTaskActor9 = new BehaviourTaskActor();
			behaviourTaskActor9.id = "boat_trading";
			behaviourTaskActor9.locale_key = "task_unit_boat_trade";
			pAsset = behaviourTaskActor9;
			this.t = behaviourTaskActor9;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconMoney");
			this.t.addBeh(new BehBoatCheckHomeDocks());
			this.t.addBeh(new BehBoatFindTargetForTrade());
			this.t.addBeh(new BehBoatFindTileInDock());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehStayInBuildingTarget(2f, 5f));
			this.t.addBeh(new BehBoatMakeTrade());
			this.t.addBeh(new BehExitBuilding());
			this.t.addBeh(new BehSetNextTask("boat_return_to_dock", true, false));
			BehaviourTaskActor behaviourTaskActor10 = new BehaviourTaskActor();
			behaviourTaskActor10.id = "boat_fishing";
			behaviourTaskActor10.locale_key = "task_unit_boat_catch_fish";
			pAsset = behaviourTaskActor10;
			this.t = behaviourTaskActor10;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconResFish");
			this.t.addBeh(new BehBoatCheckHomeDocks());
			this.t.addBeh(new BehBoatFindWaterTile());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(0f, 1f, false));
			this.t.addBeh(new BehBoatFishing());
			this.t.addBeh(new BehRandomWait(5f, 10f, false));
			this.t.addBeh(new BehBoatCollectFish());
			this.t.addBeh(new BehRandomWait(1f, 2f, false));
			this.t.addBeh(new BehBoatCheckFishingRepeat());
			BehaviourTaskActor behaviourTaskActor11 = new BehaviourTaskActor();
			behaviourTaskActor11.id = "boat_return_to_dock";
			behaviourTaskActor11.locale_key = "task_unit_return_to_dock";
			pAsset = behaviourTaskActor11;
			this.t = behaviourTaskActor11;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconArrowDestination");
			this.t.addBeh(new BehBoatCheckHomeDocks());
			this.t.addBeh(new BehBoatSetHomeDockTarget());
			this.t.addBeh(new BehBoatFindTileInDock());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehRandomWait(0f, 3f, false));
			this.t.addBeh(new BehStayInBuildingTarget(2f, 4f));
			this.t.addBeh(new BehUnloadResources());
			this.t.addBeh(new BehRepairInDock());
			this.t.addBeh(new BehExitBuilding());
			this.t.addBeh(new BehEndJob());
		}

		// Token: 0x06004631 RID: 17969 RVA: 0x001DB868 File Offset: 0x001D9A68
		private void initTasksReproductionAsexual()
		{
			BehaviourTaskActor behaviourTaskActor = new BehaviourTaskActor();
			behaviourTaskActor.id = "asexual_reproduction_budding";
			behaviourTaskActor.locale_key = "task_unit_reproduce";
			BehaviourTaskActor pAsset = behaviourTaskActor;
			this.t = behaviourTaskActor;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/subspecies_traits/subspecies_trait_reproduction_budding");
			for (int i = 0; i < 4; i++)
			{
				this.t.addBeh(new BehStartShake(0.1f, 0.1f));
				this.t.addBeh(new BehActorReverseFlip());
			}
			this.t.addBeh(new BehCheckReproductionBasics());
			this.t.addBeh(new BehCheckBuddingReproduction());
			BehaviourTaskActor behaviourTaskActor2 = new BehaviourTaskActor();
			behaviourTaskActor2.id = "asexual_reproduction_divine";
			behaviourTaskActor2.locale_key = "task_unit_reproduce";
			pAsset = behaviourTaskActor2;
			this.t = behaviourTaskActor2;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/subspecies_traits/subspecies_trait_reproduction_divine");
			for (int j = 0; j < 4; j++)
			{
				this.t.addBeh(new BehStartShake(0.1f, 0.1f));
				this.t.addBeh(new BehActorReverseFlip());
			}
			this.t.addBeh(new BehCheckReproductionBasics());
			this.t.addBeh(new BehCheckDivineReproduction());
			BehaviourTaskActor behaviourTaskActor3 = new BehaviourTaskActor();
			behaviourTaskActor3.id = "asexual_reproduction_spores";
			behaviourTaskActor3.locale_key = "task_unit_reproduce";
			pAsset = behaviourTaskActor3;
			this.t = behaviourTaskActor3;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/subspecies_traits/subspecies_trait_reproduction_spores");
			this.t.addBeh(new BehStartShake(1f, 0f));
			for (int k = 0; k < 4; k++)
			{
				this.t.addBeh(new BehActorReverseFlip());
				this.t.addBeh(new BehJumpingAnimation(0.1f, 0.1f));
			}
			this.t.addBeh(new BehCheckReproductionBasics());
			this.t.addBeh(new BehCheckSporeReproduction());
			BehaviourTaskActor behaviourTaskActor4 = new BehaviourTaskActor();
			behaviourTaskActor4.id = "asexual_reproduction_fission";
			behaviourTaskActor4.locale_key = "task_unit_reproduce";
			pAsset = behaviourTaskActor4;
			this.t = behaviourTaskActor4;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/subspecies_traits/subspecies_trait_reproduction_fission");
			for (int l = 0; l < 4; l++)
			{
				this.t.addBeh(new BehStartShake(0.1f, 0.1f));
				this.t.addBeh(new BehActorReverseFlip());
			}
			this.t.addBeh(new BehCheckReproductionBasics());
			this.t.addBeh(new BehCheckFissionReproduction());
			BehaviourTaskActor behaviourTaskActor5 = new BehaviourTaskActor();
			behaviourTaskActor5.id = "asexual_reproduction_vegetative";
			behaviourTaskActor5.locale_key = "task_unit_reproduce";
			pAsset = behaviourTaskActor5;
			this.t = behaviourTaskActor5;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/subspecies_traits/subspecies_trait_reproduction_vegetative");
			for (int m = 0; m < 4; m++)
			{
				this.t.addBeh(new BehStartShake(0.1f, 0.1f));
				this.t.addBeh(new BehActorReverseFlip());
			}
			this.t.addBeh(new BehCheckReproductionBasics());
			this.t.addBeh(new BehFindTile(TileFinderType.Dirt));
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehCheckVegetativeReproduction());
			BehaviourTaskActor behaviourTaskActor6 = new BehaviourTaskActor();
			behaviourTaskActor6.id = "asexual_reproduction_parthenogenesis";
			behaviourTaskActor6.locale_key = "task_unit_reproduce";
			pAsset = behaviourTaskActor6;
			this.t = behaviourTaskActor6;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/subspecies_traits/subspecies_trait_reproduction_parthenogenesis");
			for (int n = 0; n < 4; n++)
			{
				this.t.addBeh(new BehStartShake(0.1f, 0.1f));
				this.t.addBeh(new BehActorReverseFlip());
			}
			this.t.addBeh(new BehCheckReproductionBasics());
			this.t.addBeh(new BehCheckParthenogenesisReproduction());
		}

		// Token: 0x06004632 RID: 17970 RVA: 0x001DBC24 File Offset: 0x001D9E24
		private void initTasksReproductionSexual()
		{
			BehaviourTaskActor behaviourTaskActor = new BehaviourTaskActor();
			behaviourTaskActor.id = "check_lover_city";
			BehaviourTaskActor pAsset = behaviourTaskActor;
			this.t = behaviourTaskActor;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconCity");
			this.t.addBeh(new BehCheckSameCityActorLover());
			BehaviourTaskActor behaviourTaskActor2 = new BehaviourTaskActor();
			behaviourTaskActor2.id = "find_lover";
			pAsset = behaviourTaskActor2;
			this.t = behaviourTaskActor2;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconArrowLover");
			this.t.addBeh(new BehFindLover());
			BehaviourTaskActor behaviourTaskActor3 = new BehaviourTaskActor();
			behaviourTaskActor3.id = "sexual_reproduction_try";
			behaviourTaskActor3.locale_key = "task_unit_reproduce";
			pAsset = behaviourTaskActor3;
			this.t = behaviourTaskActor3;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconLovers");
			this.t.addBeh(new BehCheckReproductionBasics());
			this.t.addBeh(new BehSexualReproductionTry());
			BehaviourTaskActor behaviourTaskActor4 = new BehaviourTaskActor();
			behaviourTaskActor4.id = "sexual_reproduction_check_outside";
			behaviourTaskActor4.locale_key = "task_unit_reproduce";
			pAsset = behaviourTaskActor4;
			this.t = behaviourTaskActor4;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconLovers");
			this.t.addBeh(new BehCheckSexualReproductionOutside());
			BehaviourTaskActor behaviourTaskActor5 = new BehaviourTaskActor();
			behaviourTaskActor5.id = "sexual_reproduction_inside";
			behaviourTaskActor5.locale_key = "task_unit_reproduce";
			pAsset = behaviourTaskActor5;
			this.t = behaviourTaskActor5;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconLovers");
			this.t.addBeh(new BehCheckSexualReproductionCiv());
			BehaviourTaskActor behaviourTaskActor6 = new BehaviourTaskActor();
			behaviourTaskActor6.id = "sexual_reproduction_outside";
			behaviourTaskActor6.locale_key = "task_unit_reproduce";
			pAsset = behaviourTaskActor6;
			this.t = behaviourTaskActor6;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconLovers");
			this.t.addBeh(new BehGoToActorTarget(GoToActorTargetType.SameTile, false, false, false, 2f, true, true));
			this.t.addBeh(new BehRandomWait(0.5f, 1f, false));
			this.t.addBeh(new BehAnimalBreedingTime());
			this.t.addBeh(new BehRandomWait(0.5f, 1f, false));
			this.t.addBeh(new BehAnimalBreedingTime());
			this.t.addBeh(new BehRandomWait(0.5f, 1f, false));
			this.t.addBeh(new BehAnimalBreedingTime());
			this.t.addBeh(new BehRandomWait(0.5f, 1f, false));
			this.t.addBeh(new BehCheckForBabiesFromSexualReproduction());
			this.t.addBeh(new BehRandomWait(1f, 3f, false));
			BehaviourTaskActor behaviourTaskActor7 = new BehaviourTaskActor();
			behaviourTaskActor7.id = "sexual_reproduction_civ_go";
			behaviourTaskActor7.locale_key = "task_unit_reproduce";
			pAsset = behaviourTaskActor7;
			this.t = behaviourTaskActor7;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconLovers");
			this.t.addBeh(new BehBuildingTargetLoverHome());
			this.t.addBeh(new BehGetTargetBuildingMainTile());
			this.t.addBeh(new BehGoToTileTarget());
			for (int i = 0; i < 6; i++)
			{
				this.t.addBeh(new BehRandomWait(1f, 2f, false));
				this.t.addBeh(new BehCheckForLover());
				this.t.addBeh(new BehRandomWait(1f, 2f, false));
			}
			BehaviourTaskActor behaviourTaskActor8 = new BehaviourTaskActor();
			behaviourTaskActor8.id = "sexual_reproduction_civ_action";
			behaviourTaskActor8.locale_key = "task_unit_reproduce";
			pAsset = behaviourTaskActor8;
			this.t = behaviourTaskActor8;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconLovers");
			for (int j = 0; j < 5; j++)
			{
				this.t.addBeh(new BehStayInBuildingTarget(1f, 2f));
				this.t.addBeh(new BehShakeBuilding(0.7f, 0.04f, 0.04f));
				this.t.addBeh(new BehSpawnHeartsFromBuilding(1f));
			}
			this.t.addBeh(new BehCheckForBabiesFromSexualReproduction());
			this.t.addBeh(new BehExitBuilding());
			BehaviourTaskActor behaviourTaskActor9 = new BehaviourTaskActor();
			behaviourTaskActor9.id = "sexual_reproduction_civ_wait";
			behaviourTaskActor9.locale_key = "task_unit_reproduce";
			pAsset = behaviourTaskActor9;
			this.t = behaviourTaskActor9;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconLovers");
			for (int k = 0; k < 5; k++)
			{
				this.t.addBeh(new BehStayInBuildingTarget(1f, 2f));
			}
			this.t.addBeh(new BehExitBuilding());
		}

		// Token: 0x06004633 RID: 17971 RVA: 0x001DC0A4 File Offset: 0x001DA2A4
		private void initTasksChildren()
		{
			BehaviourTaskActor behaviourTaskActor = new BehaviourTaskActor();
			behaviourTaskActor.id = "child_random_flips";
			behaviourTaskActor.cancellable_by_socialize = true;
			behaviourTaskActor.locale_key = "task_unit_play";
			BehaviourTaskActor pAsset = behaviourTaskActor;
			this.t = behaviourTaskActor;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconChildren");
			for (int i = 0; i < 4; i++)
			{
				this.t.addBeh(new BehActorReverseFlip());
				this.t.addBeh(new BehJumpingAnimation(0.2f, 0.2f));
			}
			this.t.addBeh(new BehActorChangeHappiness("just_played"));
			BehaviourTaskActor behaviourTaskActor2 = new BehaviourTaskActor();
			behaviourTaskActor2.id = "child_play_at_one_spot";
			behaviourTaskActor2.cancellable_by_socialize = true;
			behaviourTaskActor2.locale_key = "task_unit_play";
			pAsset = behaviourTaskActor2;
			this.t = behaviourTaskActor2;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconChildren");
			this.t.addBeh(new BehActorReverseFlip());
			this.t.addBeh(new BehJumpingAnimation(1.5f, 1.5f));
			this.t.addBeh(new BehActorReverseFlip());
			this.t.addBeh(new BehActorChangeHappiness("just_played"));
			BehaviourTaskActor behaviourTaskActor3 = new BehaviourTaskActor();
			behaviourTaskActor3.id = "child_random_jump";
			behaviourTaskActor3.cancellable_by_socialize = true;
			behaviourTaskActor3.locale_key = "task_unit_play";
			pAsset = behaviourTaskActor3;
			this.t = behaviourTaskActor3;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconChildren");
			for (int j = 0; j < 2; j++)
			{
				this.t.addBeh(new BehActorReverseFlip());
				this.t.addBeh(new BehJumpingAnimation(1.5f, 1.5f));
			}
			this.t.addBeh(new BehActorRandomJump());
			this.t.addBeh(new BehRandomWait(1f, 3f, false));
			this.t.addBeh(new BehActorChangeHappiness("just_played"));
			BehaviourTaskActor behaviourTaskActor4 = new BehaviourTaskActor();
			behaviourTaskActor4.id = "child_follow_parent";
			behaviourTaskActor4.cancellable_by_socialize = true;
			behaviourTaskActor4.locale_key = "task_unit_move";
			pAsset = behaviourTaskActor4;
			this.t = behaviourTaskActor4;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/iconAdults");
			this.t.addBeh(new BehChildFindRandomFamilyParent());
			this.t.addBeh(new BehGoToActorTarget(GoToActorTargetType.SameRegion, false, false, false, 2f, true, true));
			this.t.addBeh(new BehRepeatTaskChance(0.1f));
		}

		// Token: 0x06004634 RID: 17972 RVA: 0x001DC30C File Offset: 0x001DA50C
		private void initTasksSubspeciesTraits()
		{
			BehaviourTaskActor behaviourTaskActor = new BehaviourTaskActor();
			behaviourTaskActor.id = "try_affect_dreams";
			BehaviourTaskActor pAsset = behaviourTaskActor;
			this.t = behaviourTaskActor;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/subspecies_traits/subspecies_trait_dreamweavers");
			this.t.addBeh(new BehFindTile(TileFinderType.FreeTile));
			this.t.addBeh(new BehGoToTileTarget());
			for (int i = 0; i < 4; i++)
			{
				this.t.addBeh(new BehActorReverseFlip());
				this.t.addBeh(new BehJumpingAnimation(0.5f, 0.5f));
			}
			this.t.addBeh(new BehAffectDreams());
		}

		// Token: 0x06004635 RID: 17973 RVA: 0x001DC3B0 File Offset: 0x001DA5B0
		private void initTasksSocializing()
		{
			BehaviourTaskActor behaviourTaskActor = new BehaviourTaskActor();
			behaviourTaskActor.id = "socialize_initial_check";
			behaviourTaskActor.locale_key = "task_unit_socialize";
			BehaviourTaskActor pAsset = behaviourTaskActor;
			this.t = behaviourTaskActor;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/culture_traits/culture_trait_gossip_lovers");
			this.t.addBeh(new BehSocializeStartCheck());
			BehaviourTaskActor behaviourTaskActor2 = new BehaviourTaskActor();
			behaviourTaskActor2.id = "socialize_try_to_start_near_bonfire";
			behaviourTaskActor2.locale_key = "task_unit_socialize";
			pAsset = behaviourTaskActor2;
			this.t = behaviourTaskActor2;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/culture_traits/culture_trait_gossip_lovers");
			this.t.addBeh(new BehCityActorFindBuilding("type_bonfire", true));
			this.t.addBeh(new BehFindRandomTileNearBuildingTarget());
			this.t.addBeh(new BehGoToTileTarget());
			this.t.addBeh(new BehTryToSocialize());
			BehaviourTaskActor behaviourTaskActor3 = new BehaviourTaskActor();
			behaviourTaskActor3.id = "socialize_try_to_start_immediate";
			behaviourTaskActor3.locale_key = "task_unit_socialize";
			pAsset = behaviourTaskActor3;
			this.t = behaviourTaskActor3;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/culture_traits/culture_trait_gossip_lovers");
			this.t.addBeh(new BehTryToSocialize());
			BehaviourTaskActor behaviourTaskActor4 = new BehaviourTaskActor();
			behaviourTaskActor4.id = "socialize_go_to_target";
			behaviourTaskActor4.locale_key = "task_unit_socialize";
			pAsset = behaviourTaskActor4;
			this.t = behaviourTaskActor4;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/culture_traits/culture_trait_gossip_lovers");
			this.t.addBeh(new BehGoToActorTarget(GoToActorTargetType.NearbyTileClosest, false, false, true, 2f, true, true));
			this.t.addBeh(new BehCheckNearActorTarget());
			this.t.addBeh(new BehSetNextTask("socialize_do_talk", false, false));
			BehaviourTaskActor behaviourTaskActor5 = new BehaviourTaskActor();
			behaviourTaskActor5.id = "socialize_do_talk";
			behaviourTaskActor5.locale_key = "task_unit_socialize";
			pAsset = behaviourTaskActor5;
			this.t = behaviourTaskActor5;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/culture_traits/culture_trait_gossip_lovers");
			this.t.addBeh(new BehDoTalk());
			this.t.addBeh(new BehFinishTalk());
			this.t.addBeh(new BehRandomWait(1f, 2f, false));
			BehaviourTaskActor behaviourTaskActor6 = new BehaviourTaskActor();
			behaviourTaskActor6.id = "socialize_receiving";
			behaviourTaskActor6.locale_key = "task_unit_socialize";
			pAsset = behaviourTaskActor6;
			this.t = behaviourTaskActor6;
			this.add(pAsset);
			this.t.setIcon("ui/Icons/culture_traits/culture_trait_gossip_lovers");
			this.t.addBeh(new BehSocializeTalk());
		}

		// Token: 0x06004636 RID: 17974 RVA: 0x001DC614 File Offset: 0x001DA814
		private void addActionsForDeliverResources(BehaviourTaskActor pTask, bool pWheatStorage = false)
		{
			pTask.addBeh(new BehCheckHasResources());
			pTask.addBeh(new BehRandomWait(0.7f, 1.2f, false));
			if (pWheatStorage)
			{
				pTask.addBeh(new BehCityActorFindStorageWheat());
			}
			else
			{
				pTask.addBeh(new BehCityActorFindStorage());
			}
			pTask.addBeh(new BehFindRaycastTileForBuildingTarget());
			pTask.addBeh(new BehGoToTileTarget());
			pTask.addBeh(new BehLookAtBuildingTarget(0.3f));
			pTask.addBeh(new BehAngleAnimation(AngleAnimationTarget.Building, string.Empty, 0.1f, 20f, true, false));
			pTask.addBeh(new BehThrowResources());
			pTask.addBeh(new BehRandomWait(0f, 0.2f, false));
		}

		// Token: 0x06004637 RID: 17975 RVA: 0x001DC6C4 File Offset: 0x001DA8C4
		public override void linkAssets()
		{
			base.linkAssets();
			foreach (BehaviourTaskActor tAsset in this.list)
			{
				string tToolId = tAsset.force_hand_tool;
				if (!string.IsNullOrEmpty(tToolId))
				{
					tAsset.cached_hand_tool_asset = AssetManager.unit_hand_tools.get(tToolId);
				}
			}
		}

		// Token: 0x06004638 RID: 17976 RVA: 0x001DC738 File Offset: 0x001DA938
		public override void editorDiagnosticLocales()
		{
			base.editorDiagnosticLocales();
			foreach (BehaviourTaskActor tAsset in this.list)
			{
				this.checkLocale(tAsset, tAsset.getLocaleID());
			}
		}
	}
}
