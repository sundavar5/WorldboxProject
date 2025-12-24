using System;

// Token: 0x020000E5 RID: 229
public class DecisionsLibrary : AssetLibrary<DecisionAsset>
{
	// Token: 0x060006BA RID: 1722 RVA: 0x00063FF4 File Offset: 0x000621F4
	public override void init()
	{
		base.init();
		this.initDecisionsGeneral();
		this.initDecisionsTraits();
		this.initDecisionsChildren();
		this.initDecisionsAnimals();
		this.initDecisionDiets();
		this.initDecisionSleep();
		this.initDecisionsHerd();
		this.initDecisionsCivs();
		this.initDecisionsKings();
		this.initDecisionsWarriors();
		this.initDecisionsLeaders();
		this.initDecisionsBoats();
		this.initDecisionsBees();
		this.initDecisionsOther();
		this.initDecisionsUnique();
		this.initDecisionsSocialize();
		this.initDecisionsReproduction();
		this.initDecisionsClans();
		this.initDecisionsNomads();
		this.initDecisionsStatusRelated();
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x00064080 File Offset: 0x00062280
	private void initDecisionsWarriors()
	{
		DecisionAsset decisionAsset = new DecisionAsset();
		decisionAsset.id = "warrior_try_join_army_group";
		decisionAsset.priority = NeuroLayer.Layer_3_High;
		decisionAsset.path_icon = "ui/Icons/iconSoldier";
		decisionAsset.cooldown = 5;
		decisionAsset.unique = true;
		decisionAsset.action_check_launch = ((Actor pActor) => pActor.hasCity() && !pActor.hasArmy());
		decisionAsset.weight = 3f;
		this.add(decisionAsset);
		DecisionAsset decisionAsset2 = new DecisionAsset();
		decisionAsset2.id = "check_warrior_limit";
		decisionAsset2.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset2.path_icon = "ui/Icons/iconArmyList";
		decisionAsset2.cooldown = 60;
		decisionAsset2.unique = true;
		decisionAsset2.action_check_launch = ((Actor pActor) => pActor.hasCity() && pActor.inOwnCityBorders());
		decisionAsset2.weight = 0.7f;
		this.add(decisionAsset2);
		DecisionAsset decisionAsset3 = new DecisionAsset();
		decisionAsset3.id = "city_walking_to_danger_zone";
		decisionAsset3.priority = NeuroLayer.Layer_3_High;
		decisionAsset3.path_icon = "ui/Icons/iconArrowAttackTarget";
		decisionAsset3.cooldown = 5;
		decisionAsset3.unique = true;
		decisionAsset3.action_check_launch = ((Actor pActor) => pActor.hasCity() && pActor.city.isInDanger() && pActor.inOwnCityBorders());
		decisionAsset3.weight = 2.7f;
		this.add(decisionAsset3);
		DecisionAsset decisionAsset4 = new DecisionAsset();
		decisionAsset4.id = "warrior_army_captain_idle_walking_city";
		decisionAsset4.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset4.path_icon = "ui/Icons/iconArmyList";
		decisionAsset4.cooldown = 20;
		decisionAsset4.unique = true;
		decisionAsset4.action_check_launch = ((Actor pActor) => pActor.hasCity() && pActor.isArmyGroupLeader() && !pActor.city.hasAttackZoneOrder());
		decisionAsset4.weight = 1.3f;
		this.add(decisionAsset4);
		DecisionAsset decisionAsset5 = new DecisionAsset();
		decisionAsset5.id = "warrior_army_captain_waiting";
		decisionAsset5.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset5.path_icon = "ui/Icons/iconClock";
		decisionAsset5.cooldown = 20;
		decisionAsset5.unique = true;
		decisionAsset5.action_check_launch = ((Actor pActor) => pActor.hasCity() && pActor.isArmyGroupLeader() && !pActor.city.hasAttackZoneOrder());
		decisionAsset5.weight = 1.5f;
		this.add(decisionAsset5);
		DecisionAsset decisionAsset6 = new DecisionAsset();
		decisionAsset6.id = "warrior_army_leader_move_random";
		decisionAsset6.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset6.path_icon = "ui/Icons/iconArrowDestination";
		decisionAsset6.cooldown = 1;
		decisionAsset6.unique = true;
		decisionAsset6.action_check_launch = ((Actor pActor) => pActor.hasCity() && pActor.isArmyGroupLeader());
		decisionAsset6.weight = 1.5f;
		this.add(decisionAsset6);
		DecisionAsset decisionAsset7 = new DecisionAsset();
		decisionAsset7.id = "warrior_army_leader_move_to_attack_target";
		decisionAsset7.priority = NeuroLayer.Layer_3_High;
		decisionAsset7.path_icon = "ui/Icons/iconArrowAttackTarget";
		decisionAsset7.cooldown = 1;
		decisionAsset7.unique = true;
		decisionAsset7.action_check_launch = ((Actor pActor) => pActor.hasCity() && pActor.isArmyGroupLeader() && pActor.city.hasAttackZoneOrder());
		decisionAsset7.weight = 2f;
		this.add(decisionAsset7);
		DecisionAsset decisionAsset8 = new DecisionAsset();
		decisionAsset8.id = "warrior_army_follow_leader";
		decisionAsset8.priority = NeuroLayer.Layer_3_High;
		decisionAsset8.path_icon = "ui/Icons/iconLoyalty";
		decisionAsset8.cooldown = 1;
		decisionAsset8.unique = true;
		decisionAsset8.action_check_launch = delegate(Actor pActor)
		{
			if (!pActor.hasCity())
			{
				return false;
			}
			if (!pActor.isArmyGroupWarrior())
			{
				return false;
			}
			if (!pActor.army.hasCaptain())
			{
				return false;
			}
			WorldTile tArmyCaptainTile = pActor.army.getCaptain().current_tile;
			return pActor.current_tile.isSameIsland(tArmyCaptainTile) && pActor.city.hasAttackZoneOrder();
		};
		decisionAsset8.weight = 5f;
		this.add(decisionAsset8);
		DecisionAsset decisionAsset9 = new DecisionAsset();
		decisionAsset9.id = "warrior_random_move";
		decisionAsset9.priority = NeuroLayer.Layer_1_Low;
		decisionAsset9.path_icon = "ui/Icons/iconArrowDestination";
		decisionAsset9.cooldown = 4;
		decisionAsset9.unique = true;
		decisionAsset9.action_check_launch = delegate(Actor pActor)
		{
			if (!pActor.hasCity())
			{
				return false;
			}
			if (!pActor.isArmyGroupWarrior())
			{
				return false;
			}
			if (!pActor.army.hasCaptain())
			{
				return true;
			}
			WorldTile tArmyCaptainTile = pActor.army.getCaptain().current_tile;
			return !pActor.current_tile.isSameIsland(tArmyCaptainTile);
		};
		decisionAsset9.weight = 1.6f;
		this.add(decisionAsset9);
		this.add(new DecisionAsset
		{
			id = "check_warrior_transport",
			priority = NeuroLayer.Layer_2_Moderate,
			path_icon = "ui/Icons/iconBoat",
			cooldown = 6,
			unique = true,
			weight = 2f
		});
		DecisionAsset decisionAsset10 = new DecisionAsset();
		decisionAsset10.id = "warrior_train_with_dummy";
		decisionAsset10.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset10.path_icon = "ui/Icons/iconWarfare";
		decisionAsset10.cooldown = 100;
		decisionAsset10.unique = true;
		decisionAsset10.city_must_be_safe = true;
		decisionAsset10.action_check_launch = ((Actor pActor) => pActor.hasCity() && pActor.inOwnCityBorders() && pActor.city.hasBuildingType("type_training_dummies", true, pActor.current_island));
		decisionAsset10.weight = 1.1f;
		this.add(decisionAsset10);
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x000644E0 File Offset: 0x000626E0
	private void initDecisionsStatusRelated()
	{
		DecisionAsset decisionAsset = new DecisionAsset();
		decisionAsset.id = "check_swearing";
		decisionAsset.priority = NeuroLayer.Layer_3_High;
		decisionAsset.task_id = "swearing";
		decisionAsset.path_icon = "ui/Icons/iconSwearing";
		decisionAsset.cooldown_on_launch_failure = true;
		decisionAsset.cooldown = 60;
		decisionAsset.unique = true;
		decisionAsset.action_check_launch = ((Actor _) => Randy.randomChance(0.1f));
		decisionAsset.weight = 0.1f;
		this.add(decisionAsset);
		this.add(new DecisionAsset
		{
			id = "do_tantrum",
			priority = NeuroLayer.Layer_4_Critical,
			path_icon = "ui/Icons/iconTantrum",
			cooldown = 1,
			unique = true,
			weight = 3f
		});
		this.add(new DecisionAsset
		{
			id = "possessed_following",
			priority = NeuroLayer.Layer_4_Critical,
			path_icon = "ui/Icons/iconPossessed",
			cooldown = 1,
			unique = true,
			weight = 3f
		});
		this.add(new DecisionAsset
		{
			id = "status_confused",
			priority = NeuroLayer.Layer_4_Critical,
			path_icon = "ui/Icons/iconConfused",
			cooldown = 1,
			unique = true,
			weight = 3.5f
		});
		DecisionAsset decisionAsset2 = new DecisionAsset();
		decisionAsset2.id = "run_to_water_when_on_fire";
		decisionAsset2.priority = NeuroLayer.Layer_4_Critical;
		decisionAsset2.path_icon = "ui/Icons/iconFire";
		decisionAsset2.cooldown = 1;
		decisionAsset2.unique = true;
		decisionAsset2.action_check_launch = ((Actor pActor) => pActor.asset.run_to_water_when_on_fire);
		decisionAsset2.weight = 5f;
		this.add(decisionAsset2);
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x00064694 File Offset: 0x00062894
	private void initDecisionsNomads()
	{
		DecisionAsset decisionAsset = new DecisionAsset();
		decisionAsset.id = "try_to_start_new_civilization";
		decisionAsset.priority = NeuroLayer.Layer_3_High;
		decisionAsset.path_icon = "ui/Icons/iconKingdom";
		decisionAsset.cooldown_on_launch_failure = true;
		decisionAsset.cooldown = 30;
		decisionAsset.unique = true;
		decisionAsset.only_sapient = true;
		decisionAsset.only_safe = true;
		decisionAsset.only_adult = true;
		decisionAsset.action_check_launch = delegate(Actor pActor)
		{
			if (pActor.isKing())
			{
				return false;
			}
			if (pActor.hasCity())
			{
				return false;
			}
			if (pActor.current_zone.hasCity())
			{
				if (!pActor.current_zone.city.isNeutral())
				{
					return false;
				}
			}
			else if (!pActor.canBuildNewCity())
			{
				return false;
			}
			return true;
		};
		decisionAsset.weight = 1f;
		this.add(decisionAsset);
		DecisionAsset decisionAsset2 = new DecisionAsset();
		decisionAsset2.id = "check_join_city";
		decisionAsset2.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset2.path_icon = "ui/Icons/iconCity";
		decisionAsset2.cooldown = 10;
		decisionAsset2.unique = true;
		decisionAsset2.action_check_launch = ((Actor pActor) => !pActor.hasCity() && !pActor.kingdom.asset.is_forced_by_trait && pActor.current_zone.hasCity());
		decisionAsset2.weight = 1f;
		this.add(decisionAsset2);
		DecisionAsset decisionAsset3 = new DecisionAsset();
		decisionAsset3.id = "check_join_empty_nearby_city";
		decisionAsset3.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset3.path_icon = "ui/Icons/iconCity";
		decisionAsset3.cooldown = 10;
		decisionAsset3.unique = true;
		decisionAsset3.action_check_launch = ((Actor pActor) => !pActor.hasCity() && !pActor.kingdom.asset.is_forced_by_trait);
		decisionAsset3.weight = 1f;
		this.add(decisionAsset3);
		DecisionAsset decisionAsset4 = new DecisionAsset();
		decisionAsset4.id = "build_civ_city_here";
		decisionAsset4.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset4.path_icon = "ui/Icons/iconCity";
		decisionAsset4.cooldown = 60;
		decisionAsset4.only_adult = true;
		decisionAsset4.unique = true;
		decisionAsset4.action_check_launch = ((Actor pActor) => pActor.isKingdomCiv() && !pActor.hasCity() && !Finder.isEnemyNearOnSameIsland(pActor, 1));
		decisionAsset4.weight = 1f;
		this.add(decisionAsset4);
	}

	// Token: 0x060006BE RID: 1726 RVA: 0x00064864 File Offset: 0x00062A64
	private void initDecisionsHerd()
	{
		DecisionAsset decisionAsset = new DecisionAsset();
		decisionAsset.id = "family_check_existence";
		decisionAsset.priority = NeuroLayer.Layer_3_High;
		decisionAsset.path_icon = "ui/Icons/iconFamily";
		decisionAsset.cooldown = 60;
		decisionAsset.unique = true;
		decisionAsset.action_check_launch = ((Actor pActor) => pActor.family.countUnits() <= 1);
		decisionAsset.weight = 2f;
		this.add(decisionAsset);
		DecisionAsset decisionAsset2 = new DecisionAsset();
		decisionAsset2.id = "family_alpha_move";
		decisionAsset2.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset2.path_icon = "ui/Icons/iconFamily";
		decisionAsset2.cooldown = 200;
		decisionAsset2.unique = true;
		decisionAsset2.only_herd = true;
		decisionAsset2.action_check_launch = ((Actor pActor) => !pActor.isSapient() && pActor.family.isAlpha(pActor));
		decisionAsset2.weight = 0.8f;
		this.add(decisionAsset2);
		DecisionAsset decisionAsset3 = new DecisionAsset();
		decisionAsset3.id = "family_group_follow";
		decisionAsset3.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset3.path_icon = "ui/Icons/iconFamily";
		decisionAsset3.cooldown = 5;
		decisionAsset3.unique = true;
		decisionAsset3.only_herd = true;
		decisionAsset3.action_check_launch = ((Actor pActor) => !pActor.isSapient() && !pActor.family.isAlpha(pActor));
		decisionAsset3.weight = 0.7f;
		this.add(decisionAsset3);
		DecisionAsset decisionAsset4 = new DecisionAsset();
		decisionAsset4.id = "family_group_leave";
		decisionAsset4.priority = NeuroLayer.Layer_1_Low;
		decisionAsset4.path_icon = "ui/Icons/iconFamily";
		decisionAsset4.cooldown = 20;
		decisionAsset4.unique = true;
		decisionAsset4.only_herd = true;
		decisionAsset4.only_adult = true;
		decisionAsset4.only_safe = true;
		decisionAsset4.action_check_launch = ((Actor pActor) => !pActor.isSapient() && pActor.family.isFull() && !pActor.family.isAlpha(pActor));
		decisionAsset4.weight = 0.5f;
		this.add(decisionAsset4);
		DecisionAsset decisionAsset5 = new DecisionAsset();
		decisionAsset5.id = "family_group_join_or_new_herd";
		decisionAsset5.priority = NeuroLayer.Layer_3_High;
		decisionAsset5.path_icon = "ui/Icons/iconFamily";
		decisionAsset5.cooldown = 60;
		decisionAsset5.only_herd = true;
		decisionAsset5.action_check_launch = ((Actor pActor) => !pActor.isSapient() && !pActor.hasFamily());
		decisionAsset5.weight = 0.3f;
		this.add(decisionAsset5);
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x00064AA0 File Offset: 0x00062CA0
	private void initDecisionSleep()
	{
		this.add(new DecisionAsset
		{
			id = "bored_sleep",
			priority = NeuroLayer.Layer_0_Minimal,
			task_id = "decide_where_to_sleep",
			path_icon = "ui/Icons/iconSleep",
			cooldown = 90,
			only_safe = true,
			city_must_be_safe = true,
			weight = 0.05f
		});
		this.add(new DecisionAsset
		{
			id = "polyphasic_sleep",
			priority = NeuroLayer.Layer_1_Low,
			task_id = "decide_where_to_sleep",
			path_icon = "ui/Icons/iconSleep",
			unique = true,
			cooldown = 90,
			only_safe = true,
			city_must_be_safe = true,
			weight = 0.8f
		});
		this.add(new DecisionAsset
		{
			id = "monophasic_sleep",
			priority = NeuroLayer.Layer_1_Low,
			task_id = "decide_where_to_sleep",
			path_icon = "ui/Icons/iconSleep",
			unique = true,
			cooldown = 200,
			only_safe = true,
			city_must_be_safe = true,
			weight = 0.75f
		});
		DecisionAsset decisionAsset = new DecisionAsset();
		decisionAsset.id = "sleep_at_winter_age";
		decisionAsset.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset.task_id = "decide_where_to_sleep";
		decisionAsset.path_icon = "ui/Icons/iconSleep";
		decisionAsset.unique = true;
		decisionAsset.cooldown = 30;
		decisionAsset.only_safe = true;
		decisionAsset.city_must_be_safe = true;
		decisionAsset.action_check_launch = ((Actor _) => World.world.era_manager.isWinter());
		decisionAsset.weight = 1f;
		this.add(decisionAsset);
		DecisionAsset decisionAsset2 = new DecisionAsset();
		decisionAsset2.id = "sleep_at_dark_age";
		decisionAsset2.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset2.task_id = "decide_where_to_sleep";
		decisionAsset2.path_icon = "ui/Icons/iconSleep";
		decisionAsset2.unique = true;
		decisionAsset2.cooldown = 30;
		decisionAsset2.only_safe = true;
		decisionAsset2.city_must_be_safe = true;
		decisionAsset2.action_check_launch = ((Actor _) => World.world.era_manager.isNight());
		decisionAsset2.weight = 1f;
		this.add(decisionAsset2);
		DecisionAsset decisionAsset3 = new DecisionAsset();
		decisionAsset3.id = "sleep_when_not_chaos_age";
		decisionAsset3.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset3.task_id = "decide_where_to_sleep";
		decisionAsset3.path_icon = "ui/Icons/iconSleep";
		decisionAsset3.unique = true;
		decisionAsset3.cooldown = 30;
		decisionAsset3.only_safe = true;
		decisionAsset3.city_must_be_safe = true;
		decisionAsset3.action_check_launch = ((Actor _) => !World.world.era_manager.isChaosAge());
		decisionAsset3.weight = 1f;
		this.add(decisionAsset3);
		DecisionAsset decisionAsset4 = new DecisionAsset();
		decisionAsset4.id = "sleep_at_light_age";
		decisionAsset4.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset4.task_id = "decide_where_to_sleep";
		decisionAsset4.path_icon = "ui/Icons/iconSleep";
		decisionAsset4.unique = true;
		decisionAsset4.cooldown = 30;
		decisionAsset4.only_safe = true;
		decisionAsset4.city_must_be_safe = true;
		decisionAsset4.action_check_launch = ((Actor _) => World.world.era_manager.isLightAge());
		decisionAsset4.weight = 1f;
		this.add(decisionAsset4);
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x00064DC4 File Offset: 0x00062FC4
	private void initDecisionDiets()
	{
		this.add(new DecisionAsset
		{
			id = "diet_wood",
			priority = NeuroLayer.Layer_3_High,
			path_icon = "ui/Icons/subspecies_traits/subspecies_trait_diet_xylophagy",
			unique = true,
			cooldown = 30,
			only_safe = true,
			only_hungry = true,
			weight = 1f
		});
		this.add(new DecisionAsset
		{
			id = "diet_tiles",
			priority = NeuroLayer.Layer_3_High,
			path_icon = "ui/Icons/subspecies_traits/subspecies_trait_diet_geophagy",
			unique = true,
			cooldown = 10,
			only_safe = true,
			only_hungry = true,
			weight = 1f
		});
		this.add(new DecisionAsset
		{
			id = "diet_minerals",
			priority = NeuroLayer.Layer_3_High,
			path_icon = "ui/Icons/subspecies_traits/subspecies_trait_diet_lithotroph",
			unique = true,
			cooldown = 10,
			only_safe = true,
			only_hungry = true,
			weight = 1f
		});
		this.add(new DecisionAsset
		{
			id = "diet_algae",
			priority = NeuroLayer.Layer_3_High,
			path_icon = "ui/Icons/subspecies_traits/subspecies_trait_diet_algivore",
			unique = true,
			cooldown = 10,
			only_safe = true,
			only_hungry = true,
			weight = 1f
		});
		this.add(new DecisionAsset
		{
			id = "diet_fish",
			priority = NeuroLayer.Layer_3_High,
			path_icon = "ui/Icons/subspecies_traits/subspecies_trait_diet_piscivore",
			unique = true,
			cooldown = 10,
			only_safe = true,
			only_hungry = true,
			weight = 1f
		});
		this.add(new DecisionAsset
		{
			id = "diet_fruits",
			priority = NeuroLayer.Layer_3_High,
			path_icon = "ui/Icons/subspecies_traits/subspecies_trait_diet_frugivore",
			unique = true,
			cooldown = 10,
			only_safe = true,
			only_hungry = true,
			weight = 1f
		});
		this.add(new DecisionAsset
		{
			id = "diet_flowers",
			priority = NeuroLayer.Layer_3_High,
			path_icon = "ui/Icons/subspecies_traits/subspecies_trait_diet_florivore",
			unique = true,
			cooldown = 10,
			only_safe = true,
			only_hungry = true,
			weight = 1f
		});
		this.add(new DecisionAsset
		{
			id = "diet_nectar",
			priority = NeuroLayer.Layer_3_High,
			path_icon = "ui/Icons/subspecies_traits/subspecies_trait_diet_nectarivore",
			unique = true,
			cooldown = 10,
			only_safe = true,
			only_hungry = true,
			weight = 1f
		});
		this.add(new DecisionAsset
		{
			id = "diet_crops",
			priority = NeuroLayer.Layer_3_High,
			path_icon = "ui/Icons/subspecies_traits/subspecies_trait_diet_granivore",
			unique = true,
			cooldown = 10,
			only_safe = true,
			only_hungry = true,
			weight = 1f
		});
		this.add(new DecisionAsset
		{
			id = "diet_vegetation",
			priority = NeuroLayer.Layer_3_High,
			path_icon = "ui/Icons/subspecies_traits/subspecies_trait_diet_folivore",
			unique = true,
			cooldown = 10,
			only_safe = true,
			only_hungry = true,
			weight = 1f
		});
		this.add(new DecisionAsset
		{
			id = "diet_grass",
			priority = NeuroLayer.Layer_3_High,
			path_icon = "ui/Icons/subspecies_traits/subspecies_trait_diet_graminivore",
			unique = true,
			cooldown = 20,
			only_safe = true,
			only_hungry = true,
			weight = 1f
		});
		this.add(new DecisionAsset
		{
			id = "diet_meat",
			priority = NeuroLayer.Layer_3_High,
			path_icon = "ui/Icons/subspecies_traits/subspecies_trait_diet_carnivore",
			unique = true,
			cooldown = 55,
			only_safe = true,
			only_hungry = true,
			cooldown_on_launch_failure = true,
			weight = 0.96f
		});
		this.add(new DecisionAsset
		{
			id = "diet_blood",
			priority = NeuroLayer.Layer_3_High,
			path_icon = "ui/Icons/subspecies_traits/subspecies_trait_diet_hematophagy",
			unique = true,
			cooldown = 55,
			only_safe = true,
			only_hungry = true,
			cooldown_on_launch_failure = true,
			weight = 0.96f
		});
		this.add(new DecisionAsset
		{
			id = "diet_meat_insect",
			priority = NeuroLayer.Layer_3_High,
			path_icon = "ui/Icons/subspecies_traits/subspecies_trait_diet_insectivore",
			unique = true,
			cooldown = 55,
			only_safe = true,
			only_hungry = true,
			cooldown_on_launch_failure = true,
			weight = 0.96f
		});
		DecisionAsset decisionAsset = new DecisionAsset();
		decisionAsset.id = "diet_same_species";
		decisionAsset.priority = NeuroLayer.Layer_3_High;
		decisionAsset.path_icon = "ui/Icons/subspecies_traits/subspecies_trait_diet_cannibalism";
		decisionAsset.unique = true;
		decisionAsset.cooldown = 60;
		decisionAsset.only_safe = true;
		decisionAsset.only_hungry = true;
		decisionAsset.cooldown_on_launch_failure = true;
		decisionAsset.action_check_launch = ((Actor pActor) => pActor.getNutritionRatio() < 0.1f && pActor.hasStatus("starving"));
		decisionAsset.weight = 0.3f;
		this.add(decisionAsset);
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x000652D4 File Offset: 0x000634D4
	private void initDecisionsAnimals()
	{
		DecisionAsset decisionAsset = new DecisionAsset();
		decisionAsset.id = "run_away_from_carnivore";
		decisionAsset.priority = NeuroLayer.Layer_3_High;
		decisionAsset.path_icon = "ui/Icons/actor_traits/iconAgile";
		decisionAsset.cooldown = 10;
		decisionAsset.unique = true;
		decisionAsset.action_check_launch = delegate(Actor pActor)
		{
			if (!pActor.isAnimal())
			{
				return false;
			}
			bool tEnemyNear = false;
			if (!pActor.isCarnivore())
			{
				tEnemyNear = Finder.isEnemyNearOnSameIslandAndCarnivore(pActor, 1);
			}
			return tEnemyNear;
		};
		decisionAsset.weight = 2f;
		this.add(decisionAsset);
		DecisionAsset decisionAsset2 = new DecisionAsset();
		decisionAsset2.id = "check_if_stuck_on_small_land";
		decisionAsset2.priority = NeuroLayer.Layer_3_High;
		decisionAsset2.path_icon = "ui/Icons/iconTileSoil";
		decisionAsset2.cooldown = 90;
		decisionAsset2.action_check_launch = ((Actor pActor) => !pActor.current_tile.region.island.isGoodIslandForActor(pActor));
		decisionAsset2.weight = 3f;
		this.add(decisionAsset2);
		DecisionAsset decisionAsset3 = new DecisionAsset();
		decisionAsset3.id = "run_away";
		decisionAsset3.priority = NeuroLayer.Layer_3_High;
		decisionAsset3.path_icon = "ui/Icons/actor_traits/iconAgile";
		decisionAsset3.cooldown = 10;
		decisionAsset3.unique = true;
		decisionAsset3.action_check_launch = ((Actor pActor) => pActor.isFighting() && pActor.getHealthRatio() > 0.2f);
		decisionAsset3.weight = 3.1f;
		this.add(decisionAsset3);
		DecisionAsset decisionAsset4 = new DecisionAsset();
		decisionAsset4.id = "run_away_being_sus";
		decisionAsset4.priority = NeuroLayer.Layer_3_High;
		decisionAsset4.task_id = "run_away";
		decisionAsset4.path_icon = "ui/Icons/actor_traits/iconAgile";
		decisionAsset4.cooldown = 10;
		decisionAsset4.unique = true;
		decisionAsset4.action_check_launch = ((Actor pActor) => true);
		decisionAsset4.weight = 5f;
		this.add(decisionAsset4);
	}

	// Token: 0x060006C2 RID: 1730 RVA: 0x00065488 File Offset: 0x00063688
	private void initDecisionsSocialize()
	{
		DecisionAsset decisionAsset = new DecisionAsset();
		decisionAsset.id = "socialize_initial_check";
		decisionAsset.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset.path_icon = "ui/Icons/culture_traits/culture_trait_gossip_lovers";
		decisionAsset.cooldown = 30;
		decisionAsset.unique = true;
		decisionAsset.cooldown_on_launch_failure = true;
		decisionAsset.action_check_launch = ((Actor pActor) => pActor.canSocialize());
		decisionAsset.weight = 0.5f;
		this.add(decisionAsset);
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x00065504 File Offset: 0x00063704
	private void initDecisionsReproduction()
	{
		DecisionAsset decisionAsset = new DecisionAsset();
		decisionAsset.id = "sexual_reproduction_try";
		decisionAsset.priority = NeuroLayer.Layer_3_High;
		decisionAsset.path_icon = "ui/Icons/subspecies_traits/subspecies_trait_reproduction_sexual";
		decisionAsset.unique = true;
		decisionAsset.cooldown_on_launch_failure = true;
		decisionAsset.cooldown = 20;
		decisionAsset.only_adult = true;
		decisionAsset.only_safe = true;
		decisionAsset.city_must_be_safe = true;
		decisionAsset.action_check_launch = delegate(Actor pActor)
		{
			if (!pActor.canBreed())
			{
				return false;
			}
			if (!pActor.hasLover())
			{
				return false;
			}
			if (pActor.isHungry())
			{
				return false;
			}
			pActor.subspecies.countReproductionNeuron();
			return true;
		};
		decisionAsset.weight_calculate_custom = delegate(Actor pActor)
		{
			if (!pActor.hasReachedOffspringLimit())
			{
				return 2f;
			}
			return 0.1f;
		};
		this.add(decisionAsset);
		DecisionAsset decisionAsset2 = new DecisionAsset();
		decisionAsset2.id = "asexual_reproduction_divine";
		decisionAsset2.priority = NeuroLayer.Layer_3_High;
		decisionAsset2.path_icon = "ui/Icons/subspecies_traits/subspecies_trait_reproduction_divine";
		decisionAsset2.unique = true;
		decisionAsset2.cooldown_on_launch_failure = true;
		decisionAsset2.cooldown = 60;
		decisionAsset2.only_adult = true;
		decisionAsset2.action_check_launch = delegate(Actor pActor)
		{
			if (!pActor.canBreed())
			{
				return false;
			}
			if (pActor.hasLover())
			{
				return false;
			}
			if (pActor.hasTrait("miracle_bearer"))
			{
				return false;
			}
			if (pActor.getAgeRatio() < 0.6f)
			{
				return false;
			}
			pActor.subspecies.countReproductionNeuron();
			return true;
		};
		decisionAsset2.weight_calculate_custom = delegate(Actor pActor)
		{
			if (!pActor.hasReachedOffspringLimit())
			{
				return 2f;
			}
			return 0.1f;
		};
		this.add(decisionAsset2);
		DecisionAsset decisionAsset3 = new DecisionAsset();
		decisionAsset3.id = "status_soul_harvested";
		decisionAsset3.priority = NeuroLayer.Layer_3_High;
		decisionAsset3.path_icon = "ui/Icons/iconSoulHarvested";
		decisionAsset3.unique = true;
		decisionAsset3.cooldown_on_launch_failure = true;
		decisionAsset3.cooldown = 40;
		decisionAsset3.only_safe = true;
		decisionAsset3.action_check_launch = delegate(Actor pActor)
		{
			pActor.subspecies.countReproductionNeuron();
			return true;
		};
		decisionAsset3.weight_calculate_custom = delegate(Actor pActor)
		{
			if (!pActor.hasReachedOffspringLimit())
			{
				return 2f;
			}
			return 0.1f;
		};
		this.add(decisionAsset3);
		DecisionAsset decisionAsset4 = new DecisionAsset();
		decisionAsset4.id = "asexual_reproduction_fission";
		decisionAsset4.priority = NeuroLayer.Layer_3_High;
		decisionAsset4.path_icon = "ui/Icons/subspecies_traits/subspecies_trait_reproduction_fission";
		decisionAsset4.unique = true;
		decisionAsset4.cooldown_on_launch_failure = true;
		decisionAsset4.cooldown = 20;
		decisionAsset4.only_adult = true;
		decisionAsset4.action_check_launch = delegate(Actor pActor)
		{
			if (!pActor.canBreed())
			{
				return false;
			}
			if (pActor.getHealthRatio() < 0.9f)
			{
				return false;
			}
			pActor.subspecies.countReproductionNeuron();
			return true;
		};
		decisionAsset4.weight_calculate_custom = delegate(Actor pActor)
		{
			if (!pActor.hasReachedOffspringLimit())
			{
				return 2f;
			}
			return 0.1f;
		};
		this.add(decisionAsset4);
		DecisionAsset decisionAsset5 = new DecisionAsset();
		decisionAsset5.id = "asexual_reproduction_budding";
		decisionAsset5.priority = NeuroLayer.Layer_3_High;
		decisionAsset5.path_icon = "ui/Icons/subspecies_traits/subspecies_trait_reproduction_budding";
		decisionAsset5.unique = true;
		decisionAsset5.cooldown_on_launch_failure = true;
		decisionAsset5.cooldown = 50;
		decisionAsset5.only_adult = true;
		decisionAsset5.action_check_launch = delegate(Actor pActor)
		{
			if (!pActor.canBreed())
			{
				return false;
			}
			if (pActor.getHealthRatio() < 0.9f)
			{
				return false;
			}
			pActor.subspecies.countReproductionNeuron();
			return true;
		};
		decisionAsset5.weight_calculate_custom = delegate(Actor pActor)
		{
			if (!pActor.hasReachedOffspringLimit())
			{
				return 2f;
			}
			return 0.1f;
		};
		this.add(decisionAsset5);
		DecisionAsset decisionAsset6 = new DecisionAsset();
		decisionAsset6.id = "asexual_reproduction_parthenogenesis";
		decisionAsset6.priority = NeuroLayer.Layer_3_High;
		decisionAsset6.path_icon = "ui/Icons/subspecies_traits/subspecies_trait_reproduction_parthenogenesis";
		decisionAsset6.unique = true;
		decisionAsset6.cooldown_on_launch_failure = true;
		decisionAsset6.cooldown = 40;
		decisionAsset6.only_adult = true;
		decisionAsset6.action_check_launch = delegate(Actor pActor)
		{
			if (!pActor.isSexFemale())
			{
				return false;
			}
			if (!pActor.canBreed())
			{
				return false;
			}
			pActor.subspecies.countReproductionNeuron();
			return true;
		};
		decisionAsset6.weight_calculate_custom = delegate(Actor pActor)
		{
			if (!pActor.hasReachedOffspringLimit())
			{
				return 2f;
			}
			return 0.1f;
		};
		this.add(decisionAsset6);
		DecisionAsset decisionAsset7 = new DecisionAsset();
		decisionAsset7.id = "asexual_reproduction_spores";
		decisionAsset7.priority = NeuroLayer.Layer_3_High;
		decisionAsset7.path_icon = "ui/Icons/subspecies_traits/subspecies_trait_reproduction_spores";
		decisionAsset7.unique = true;
		decisionAsset7.cooldown_on_launch_failure = true;
		decisionAsset7.cooldown = 60;
		decisionAsset7.only_adult = true;
		decisionAsset7.action_check_launch = delegate(Actor pActor)
		{
			if (!pActor.canBreed())
			{
				return false;
			}
			if (!pActor.hasStatus("just_ate"))
			{
				return false;
			}
			pActor.subspecies.countReproductionNeuron();
			return true;
		};
		decisionAsset7.weight_calculate_custom = delegate(Actor pActor)
		{
			if (!pActor.hasReachedOffspringLimit())
			{
				return 2f;
			}
			return 0.1f;
		};
		this.add(decisionAsset7);
		DecisionAsset decisionAsset8 = new DecisionAsset();
		decisionAsset8.id = "asexual_reproduction_vegetative";
		decisionAsset8.priority = NeuroLayer.Layer_3_High;
		decisionAsset8.path_icon = "ui/Icons/subspecies_traits/subspecies_trait_reproduction_vegetative";
		decisionAsset8.unique = true;
		decisionAsset8.cooldown_on_launch_failure = true;
		decisionAsset8.cooldown = 30;
		decisionAsset8.only_adult = true;
		decisionAsset8.action_check_launch = delegate(Actor pActor)
		{
			if (!pActor.canBreed())
			{
				return false;
			}
			if (pActor.hasStatus("taking_roots"))
			{
				return false;
			}
			pActor.subspecies.countReproductionNeuron();
			return true;
		};
		decisionAsset8.weight = 2.5f;
		this.add(decisionAsset8);
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x00065988 File Offset: 0x00063B88
	private void initDecisionsClans()
	{
		DecisionAsset decisionAsset = new DecisionAsset();
		decisionAsset.id = "try_new_plot";
		decisionAsset.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset.path_icon = "ui/Icons/iconPlot";
		decisionAsset.cooldown = 40;
		decisionAsset.list_civ = true;
		decisionAsset.only_adult = true;
		decisionAsset.only_safe = true;
		decisionAsset.action_check_launch = ((Actor pActor) => !pActor.hasPlot() && pActor.hasClan());
		decisionAsset.weight = 1.5f;
		this.add(decisionAsset);
		DecisionAsset decisionAsset2 = new DecisionAsset();
		decisionAsset2.id = "check_plot";
		decisionAsset2.priority = NeuroLayer.Layer_3_High;
		decisionAsset2.path_icon = "ui/Icons/iconPlot";
		decisionAsset2.cooldown = 3;
		decisionAsset2.only_safe = true;
		decisionAsset2.unique = true;
		decisionAsset2.action_check_launch = ((Actor pActor) => pActor.hasPlot());
		decisionAsset2.weight = 5f;
		this.add(decisionAsset2);
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x00065A7C File Offset: 0x00063C7C
	private void initDecisionsKings()
	{
		DecisionAsset decisionAsset = new DecisionAsset();
		decisionAsset.id = "king_check_new_city_foundation";
		decisionAsset.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset.path_icon = "ui/Icons/iconCity";
		decisionAsset.cooldown = 60;
		decisionAsset.unique = true;
		decisionAsset.only_adult = true;
		decisionAsset.action_check_launch = ((Actor pActor) => WorldLawLibrary.world_law_kingdom_expansion.isEnabled() && !pActor.kingdom.hasEnemies());
		decisionAsset.weight = 3f;
		this.add(decisionAsset);
		DecisionAsset decisionAsset2 = new DecisionAsset();
		decisionAsset2.id = "king_change_kingdom_language";
		decisionAsset2.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset2.path_icon = "ui/Icons/iconLanguage";
		decisionAsset2.cooldown = 10;
		decisionAsset2.unique = true;
		decisionAsset2.action_check_launch = ((Actor pActor) => pActor.hasLanguage() && pActor.kingdom.getLanguage() != pActor.language);
		decisionAsset2.weight = 1f;
		this.add(decisionAsset2);
		DecisionAsset decisionAsset3 = new DecisionAsset();
		decisionAsset3.id = "king_change_kingdom_culture";
		decisionAsset3.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset3.path_icon = "ui/Icons/iconCulture";
		decisionAsset3.cooldown = 10;
		decisionAsset3.unique = true;
		decisionAsset3.action_check_launch = ((Actor pActor) => pActor.hasCulture() && pActor.kingdom.getCulture() != pActor.culture);
		decisionAsset3.weight = 1f;
		this.add(decisionAsset3);
		DecisionAsset decisionAsset4 = new DecisionAsset();
		decisionAsset4.id = "king_change_kingdom_religion";
		decisionAsset4.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset4.path_icon = "ui/Icons/iconReligion";
		decisionAsset4.cooldown = 10;
		decisionAsset4.unique = true;
		decisionAsset4.action_check_launch = ((Actor pActor) => pActor.hasReligion() && pActor.kingdom.getReligion() != pActor.religion);
		decisionAsset4.weight = 1f;
		this.add(decisionAsset4);
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x00065C30 File Offset: 0x00063E30
	private void initDecisionsLeaders()
	{
		DecisionAsset decisionAsset = new DecisionAsset();
		decisionAsset.id = "leader_change_city_language";
		decisionAsset.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset.path_icon = "ui/Icons/iconLanguage";
		decisionAsset.cooldown = 10;
		decisionAsset.unique = true;
		decisionAsset.action_check_launch = ((Actor pActor) => pActor.hasCity() && pActor.hasLanguage() && pActor.city.getLanguage() != pActor.language);
		decisionAsset.weight = 1f;
		this.add(decisionAsset);
		DecisionAsset decisionAsset2 = new DecisionAsset();
		decisionAsset2.id = "leader_change_city_culture";
		decisionAsset2.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset2.path_icon = "ui/Icons/iconCulture";
		decisionAsset2.cooldown = 10;
		decisionAsset2.unique = true;
		decisionAsset2.action_check_launch = ((Actor pActor) => pActor.hasCity() && pActor.hasCulture() && pActor.city.getCulture() != pActor.culture);
		decisionAsset2.weight = 1f;
		this.add(decisionAsset2);
		DecisionAsset decisionAsset3 = new DecisionAsset();
		decisionAsset3.id = "leader_change_city_religion";
		decisionAsset3.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset3.path_icon = "ui/Icons/iconReligion";
		decisionAsset3.cooldown = 10;
		decisionAsset3.unique = true;
		decisionAsset3.action_check_launch = ((Actor pActor) => pActor.hasCity() && pActor.hasReligion() && pActor.city.getReligion() != pActor.religion);
		decisionAsset3.weight = 1f;
		this.add(decisionAsset3);
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x00065D78 File Offset: 0x00063F78
	private void initDecisionsCivs()
	{
		DecisionAsset decisionAsset = new DecisionAsset();
		decisionAsset.id = "try_to_return_to_home_city";
		decisionAsset.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset.path_icon = "ui/Icons/iconHoused";
		decisionAsset.cooldown = 15;
		decisionAsset.unique = true;
		decisionAsset.only_adult = true;
		decisionAsset.action_check_launch = ((Actor pActor) => pActor.hasCity() && !pActor.inOwnCityBorders() && !pActor.inOwnCityIsland());
		decisionAsset.weight = 3f;
		this.add(decisionAsset);
		DecisionAsset decisionAsset2 = new DecisionAsset();
		decisionAsset2.id = "random_move_near_house";
		decisionAsset2.priority = NeuroLayer.Layer_0_Minimal;
		decisionAsset2.path_icon = "ui/Icons/iconLivingHouse";
		decisionAsset2.cooldown = 30;
		decisionAsset2.action_check_launch = ((Actor pActor) => pActor.hasHouse());
		decisionAsset2.weight = 0.3f;
		this.add(decisionAsset2);
		DecisionAsset decisionAsset3 = new DecisionAsset();
		decisionAsset3.id = "try_to_read";
		decisionAsset3.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset3.path_icon = "ui/Icons/iconBooks";
		decisionAsset3.cooldown = 120;
		decisionAsset3.only_adult = true;
		decisionAsset3.unique = true;
		decisionAsset3.action_check_launch = ((Actor pActor) => pActor.hasCity() && pActor.stats["intelligence"] > 5f && pActor.city.hasBooksToRead(pActor));
		decisionAsset3.weight = 1f;
		this.add(decisionAsset3);
		this.add(new DecisionAsset
		{
			id = "try_affect_dreams",
			priority = NeuroLayer.Layer_2_Moderate,
			path_icon = "ui/Icons/subspecies_traits/subspecies_trait_dreamweavers",
			cooldown = 120,
			only_adult = true,
			unique = true,
			weight = 1.5f
		});
		DecisionAsset decisionAsset4 = new DecisionAsset();
		decisionAsset4.id = "try_to_take_city_item";
		decisionAsset4.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset4.path_icon = "ui/Icons/iconEquipmentEditor2";
		decisionAsset4.cooldown = 180;
		decisionAsset4.unique = true;
		decisionAsset4.only_adult = true;
		decisionAsset4.action_check_launch = ((Actor pActor) => pActor.inOwnCityBorders() && pActor.city.data.equipment.hasAnyItem());
		decisionAsset4.weight = 1.5f;
		this.add(decisionAsset4);
		DecisionAsset decisionAsset5 = new DecisionAsset();
		decisionAsset5.id = "make_items";
		decisionAsset5.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset5.path_icon = "ui/Icons/iconReforge";
		decisionAsset5.cooldown = 90;
		decisionAsset5.unique = true;
		decisionAsset5.only_adult = true;
		decisionAsset5.action_check_launch = ((Actor pActor) => pActor.hasHouse() && pActor.inOwnCityBorders() && pActor.city.hasResourcesForNewItems());
		decisionAsset5.weight = 0.4f;
		this.add(decisionAsset5);
		DecisionAsset decisionAsset6 = new DecisionAsset();
		decisionAsset6.id = "city_idle_walking";
		decisionAsset6.priority = NeuroLayer.Layer_1_Low;
		decisionAsset6.path_icon = "ui/Icons/iconCity";
		decisionAsset6.cooldown = 5;
		decisionAsset6.unique = true;
		decisionAsset6.action_check_launch = ((Actor pActor) => pActor.city.hasZones());
		decisionAsset6.weight = 0.5f;
		this.add(decisionAsset6);
		DecisionAsset decisionAsset7 = new DecisionAsset();
		decisionAsset7.id = "store_resources";
		decisionAsset7.priority = NeuroLayer.Layer_3_High;
		decisionAsset7.path_icon = "ui/Icons/iconCityInventory";
		decisionAsset7.cooldown = 5;
		decisionAsset7.unique = true;
		decisionAsset7.action_check_launch = ((Actor pActor) => pActor.isCarryingResources() && pActor.city.hasStorageBuilding() && (!pActor.isWarrior() || pActor.inOwnCityBorders()));
		decisionAsset7.weight = 3.1f;
		this.add(decisionAsset7);
		DecisionAsset decisionAsset8 = new DecisionAsset();
		decisionAsset8.id = "stay_in_own_home";
		decisionAsset8.priority = NeuroLayer.Layer_1_Low;
		decisionAsset8.path_icon = "ui/Icons/iconHoused";
		decisionAsset8.cooldown = 15;
		decisionAsset8.list_civ = true;
		decisionAsset8.action_check_launch = ((Actor pActor) => pActor.hasHouse() && pActor.getHappinessRatio() <= 0.5f && Randy.randomChance(0.2f));
		decisionAsset8.weight = 0.5f;
		this.add(decisionAsset8);
		DecisionAsset decisionAsset9 = new DecisionAsset();
		decisionAsset9.id = "generate_loot";
		decisionAsset9.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset9.path_icon = "ui/Icons/iconLoot";
		decisionAsset9.cooldown = 60;
		decisionAsset9.list_civ = true;
		decisionAsset9.action_check_launch = ((Actor pActor) => pActor.hasHouse());
		decisionAsset9.weight = 1.5f;
		this.add(decisionAsset9);
		DecisionAsset decisionAsset10 = new DecisionAsset();
		decisionAsset10.id = "try_to_eat_city_food";
		decisionAsset10.priority = NeuroLayer.Layer_3_High;
		decisionAsset10.path_icon = "ui/Icons/iconHunger";
		decisionAsset10.cooldown = 10;
		decisionAsset10.unique = true;
		decisionAsset10.only_hungry = true;
		decisionAsset10.action_check_launch = ((Actor pActor) => pActor.hasCity() && pActor.city.hasSuitableFood(pActor.subspecies));
		decisionAsset10.weight = 2.6f;
		this.add(decisionAsset10);
		DecisionAsset decisionAsset11 = new DecisionAsset();
		decisionAsset11.id = "find_city_job";
		decisionAsset11.priority = NeuroLayer.Layer_3_High;
		decisionAsset11.path_icon = "ui/Icons/iconShowTasks";
		decisionAsset11.cooldown = 50;
		decisionAsset11.unique = true;
		decisionAsset11.action_check_launch = ((Actor pActor) => pActor.hasCity() && pActor.isInCityIsland() && pActor.city.jobs.hasAnyTask() && pActor.canWork() && (pActor.city.getPopulationPeople() <= 30 || !pActor.isWarrior()));
		decisionAsset11.weight_calculate_custom = delegate(Actor pActor)
		{
			float tResult = 2f;
			if (pActor.isStarving())
			{
				tResult = 0.3f;
			}
			else if (pActor.isHungry())
			{
				tResult = 1f;
			}
			return tResult;
		};
		this.add(decisionAsset11);
		DecisionAsset decisionAsset12 = new DecisionAsset();
		decisionAsset12.id = "claim_land";
		decisionAsset12.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset12.path_icon = "ui/Icons/citizen_jobs/iconCitizenLandClaimer";
		decisionAsset12.cooldown = 60;
		decisionAsset12.unique = true;
		decisionAsset12.cooldown_on_launch_failure = true;
		decisionAsset12.action_check_launch = ((Actor pActor) => pActor.hasCity() && pActor.city.canGrowZones());
		decisionAsset12.weight_calculate_custom = ((Actor pActor) => 2f + pActor.stats["stewardship"] / 5f * 0.1f);
		this.add(decisionAsset12);
		DecisionAsset decisionAsset13 = new DecisionAsset();
		decisionAsset13.id = "put_out_fire";
		decisionAsset13.priority = NeuroLayer.Layer_3_High;
		decisionAsset13.path_icon = "ui/Icons/iconMoney";
		decisionAsset13.cooldown = 1;
		decisionAsset13.cooldown_on_launch_failure = true;
		decisionAsset13.unique = true;
		decisionAsset13.action_check_launch = ((Actor pActor) => pActor.inOwnCityBorders() && pActor.city.isCityUnderDangerFire() && !pActor.hasStatus("burning"));
		decisionAsset13.weight = 4f;
		this.add(decisionAsset13);
		DecisionAsset decisionAsset14 = new DecisionAsset();
		decisionAsset14.id = "give_tax";
		decisionAsset14.priority = NeuroLayer.Layer_3_High;
		decisionAsset14.path_icon = "ui/Icons/iconMoney";
		decisionAsset14.cooldown = 90;
		decisionAsset14.cooldown_on_launch_failure = true;
		decisionAsset14.unique = true;
		decisionAsset14.action_check_launch = ((Actor pActor) => pActor.inOwnCityBorders() && pActor.data.loot >= pActor.kingdom.getLootMin());
		decisionAsset14.weight = 2.55f;
		this.add(decisionAsset14);
		DecisionAsset decisionAsset15 = new DecisionAsset();
		decisionAsset15.id = "check_city_destroyed";
		decisionAsset15.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset15.path_icon = "ui/Icons/iconWar";
		decisionAsset15.cooldown = 10;
		decisionAsset15.list_civ = true;
		decisionAsset15.action_check_launch = ((Actor pActor) => !pActor.hasCity() && pActor.profession_asset != null && pActor.profession_asset.cancel_when_no_city);
		decisionAsset15.weight = 0.5f;
		this.add(decisionAsset15);
		DecisionAsset decisionAsset16 = new DecisionAsset();
		decisionAsset16.id = "check_lover_city";
		decisionAsset16.priority = NeuroLayer.Layer_3_High;
		decisionAsset16.path_icon = "ui/Icons/iconCity";
		decisionAsset16.cooldown = 30;
		decisionAsset16.only_adult = true;
		decisionAsset16.unique = true;
		decisionAsset16.action_check_launch = ((Actor pActor) => !pActor.isKing() && !pActor.isCityLeader() && !pActor.isSexMale() && pActor.hasLover() && pActor.lover.hasCity() && pActor.lover.hasHouse() && !pActor.hasSameCity(pActor.lover));
		decisionAsset16.weight = 1.5f;
		this.add(decisionAsset16);
		DecisionAsset decisionAsset17 = new DecisionAsset();
		decisionAsset17.id = "find_lover";
		decisionAsset17.priority = NeuroLayer.Layer_3_High;
		decisionAsset17.path_icon = "ui/Icons/iconArrowLover";
		decisionAsset17.cooldown = 50;
		decisionAsset17.only_adult = true;
		decisionAsset17.unique = true;
		decisionAsset17.action_check_launch = ((Actor pActor) => !pActor.hasLover() && pActor.isBreedingAge());
		decisionAsset17.weight = 1.5f;
		this.add(decisionAsset17);
		DecisionAsset decisionAsset18 = new DecisionAsset();
		decisionAsset18.id = "find_house";
		decisionAsset18.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset18.path_icon = "ui/Icons/iconBuildings";
		decisionAsset18.cooldown = 10;
		decisionAsset18.unique = true;
		decisionAsset18.action_check_launch = ((Actor pActor) => !pActor.hasHouse());
		decisionAsset18.weight = 0.5f;
		this.add(decisionAsset18);
		DecisionAsset decisionAsset19 = new DecisionAsset();
		decisionAsset19.id = "replenish_energy";
		decisionAsset19.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset19.path_icon = "ui/Icons/iconStamina";
		decisionAsset19.cooldown = 30;
		decisionAsset19.unique = true;
		decisionAsset19.action_check_launch = ((Actor pActor) => (!pActor.isStaminaFull() || !pActor.isManaFull()) && pActor.getCity().hasBuildingType("type_well", true, pActor.current_island));
		decisionAsset19.weight = 0.3f;
		this.add(decisionAsset19);
		DecisionAsset decisionAsset20 = new DecisionAsset();
		decisionAsset20.id = "repair_equipment";
		decisionAsset20.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset20.path_icon = "ui/Icons/iconReforge";
		decisionAsset20.cooldown = 300;
		decisionAsset20.unique = true;
		decisionAsset20.action_check_launch = ((Actor pActor) => pActor.hasEquipment() && pActor.getCity().hasBuildingType("type_barracks", true, pActor.current_island));
		decisionAsset20.weight = 0.3f;
		this.add(decisionAsset20);
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x00066668 File Offset: 0x00064868
	private void initDecisionsBoats()
	{
		this.add(new DecisionAsset
		{
			id = "boat_check_existence",
			priority = NeuroLayer.Layer_1_Low,
			path_icon = "ui/Icons/iconBoat",
			cooldown = 5,
			unique = true,
			weight = 1f
		});
		this.add(new DecisionAsset
		{
			id = "boat_danger_check",
			priority = NeuroLayer.Layer_1_Low,
			path_icon = "ui/Icons/iconBoat",
			cooldown = 5,
			unique = true,
			weight = 1.25f
		});
		this.add(new DecisionAsset
		{
			id = "boat_idle",
			priority = NeuroLayer.Layer_1_Low,
			path_icon = "ui/Icons/iconBoat",
			cooldown = 5,
			unique = true,
			weight = 0.75f
		});
		this.add(new DecisionAsset
		{
			id = "boat_check_limits",
			priority = NeuroLayer.Layer_1_Low,
			path_icon = "ui/Icons/iconBoat",
			cooldown = 15,
			unique = true,
			weight = 1f
		});
		this.add(new DecisionAsset
		{
			id = "boat_fishing",
			priority = NeuroLayer.Layer_1_Low,
			path_icon = "ui/Icons/iconResFish",
			cooldown = 1,
			unique = true,
			weight = 1.3f
		});
		this.add(new DecisionAsset
		{
			id = "boat_trading",
			priority = NeuroLayer.Layer_1_Low,
			path_icon = "ui/Icons/iconCityInventory",
			cooldown = 1,
			unique = true,
			weight = 1.3f
		});
		this.add(new DecisionAsset
		{
			id = "boat_transport_check",
			priority = NeuroLayer.Layer_2_Moderate,
			path_icon = "ui/Icons/iconBoat",
			cooldown = 1,
			unique = true,
			weight = 1.3f
		});
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x00066844 File Offset: 0x00064A44
	private void initDecisionsBees()
	{
		DecisionAsset decisionAsset = new DecisionAsset();
		decisionAsset.id = "pollinate";
		decisionAsset.priority = NeuroLayer.Layer_1_Low;
		decisionAsset.path_icon = "ui/Icons/iconBee";
		decisionAsset.cooldown = 10;
		decisionAsset.unique = true;
		decisionAsset.weight = 1.3f;
		decisionAsset.action_check_launch = ((Actor pActor) => !(pActor.asset.id == "bee") || pActor.hasHomeBuilding());
		this.add(decisionAsset);
		DecisionAsset decisionAsset2 = new DecisionAsset();
		decisionAsset2.id = "bee_find_hive";
		decisionAsset2.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset2.path_icon = "ui/Icons/iconBeehive";
		decisionAsset2.cooldown = 20;
		decisionAsset2.only_mob = true;
		decisionAsset2.unique = true;
		decisionAsset2.weight = 1.5f;
		decisionAsset2.action_check_launch = ((Actor pActor) => !pActor.isKingdomCiv() && !pActor.hasHomeBuilding());
		this.add(decisionAsset2);
		DecisionAsset decisionAsset3 = new DecisionAsset();
		decisionAsset3.id = "bee_create_hive";
		decisionAsset3.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset3.path_icon = "ui/Icons/iconBeehive";
		decisionAsset3.cooldown = 200;
		decisionAsset3.unique = true;
		decisionAsset3.only_mob = true;
		decisionAsset3.cooldown_on_launch_failure = true;
		decisionAsset3.weight = 1.5f;
		decisionAsset3.action_check_launch = ((Actor pActor) => !pActor.hasHomeBuilding() && pActor.isSexFemale());
		this.add(decisionAsset3);
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x000669A4 File Offset: 0x00064BA4
	private void initDecisionsGeneral()
	{
		DecisionAsset decisionAsset = new DecisionAsset();
		decisionAsset.id = "swim_to_island";
		decisionAsset.priority = NeuroLayer.Layer_4_Critical;
		decisionAsset.path_icon = "ui/Icons/iconTileShallowWater";
		decisionAsset.cooldown = 1;
		decisionAsset.action_check_launch = ((Actor pActor) => !pActor.isInStablePlace());
		decisionAsset.weight = 4f;
		this.add(decisionAsset);
		DecisionAsset decisionAsset2 = new DecisionAsset();
		decisionAsset2.id = "move_to_water";
		decisionAsset2.priority = NeuroLayer.Layer_4_Critical;
		decisionAsset2.path_icon = "ui/Icons/iconTileShallowWater";
		decisionAsset2.cooldown = 1;
		decisionAsset2.action_check_launch = ((Actor pActor) => !pActor.isInStablePlace());
		decisionAsset2.weight = 4f;
		this.add(decisionAsset2);
		this.add(new DecisionAsset
		{
			id = "random_move",
			priority = NeuroLayer.Layer_0_Minimal,
			path_icon = "ui/Icons/iconArrowDestination",
			cooldown = 1,
			weight = 0.2f
		});
		this.add(new DecisionAsset
		{
			id = "random_fun_move",
			priority = NeuroLayer.Layer_1_Low,
			path_icon = "ui/Icons/iconArrowDestination",
			cooldown = 10,
			list_animal = true,
			weight = 0.2f
		});
		this.add(new DecisionAsset
		{
			id = "wait5",
			priority = NeuroLayer.Layer_0_Minimal,
			path_icon = "ui/Icons/iconClock",
			cooldown = 10,
			weight = 0.1f
		});
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x00066B2C File Offset: 0x00064D2C
	private void initDecisionsTraits()
	{
		this.add(new DecisionAsset
		{
			id = "follow_desire_target",
			priority = NeuroLayer.Layer_3_High,
			path_icon = "ui/Icons/iconGoldBrain",
			cooldown = 5,
			unique = true,
			weight = 4f
		});
		DecisionAsset decisionAsset = new DecisionAsset();
		decisionAsset.id = "try_to_poop";
		decisionAsset.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset.path_icon = "ui/Icons/iconPoop";
		decisionAsset.cooldown = 60;
		decisionAsset.unique = true;
		decisionAsset.action_check_launch = ((Actor pActor) => pActor.asset.actor_size != ActorSize.S0_Bug && !pActor.hasSubspeciesTrait("reproduction_spores"));
		decisionAsset.weight = 1.5f;
		this.add(decisionAsset);
		DecisionAsset decisionAsset2 = new DecisionAsset();
		decisionAsset2.id = "try_to_launch_fireworks";
		decisionAsset2.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset2.path_icon = "ui/Icons/iconFireworks";
		decisionAsset2.cooldown = 60;
		decisionAsset2.unique = true;
		decisionAsset2.action_check_launch = ((Actor pActor) => pActor.hasEnoughMoney(SimGlobals.m.festive_fireworks_cost));
		decisionAsset2.weight = 1.5f;
		this.add(decisionAsset2);
		DecisionAsset decisionAsset3 = new DecisionAsset();
		decisionAsset3.id = "reflection";
		decisionAsset3.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset3.path_icon = "ui/Icons/iconBre";
		decisionAsset3.cooldown = 100;
		decisionAsset3.unique = true;
		decisionAsset3.action_check_launch = ((Actor pActor) => pActor.subspecies.can_process_emotions);
		decisionAsset3.weight = 2.5f;
		this.add(decisionAsset3);
		DecisionAsset decisionAsset4 = new DecisionAsset();
		decisionAsset4.id = "madness_random_emotion";
		decisionAsset4.priority = NeuroLayer.Layer_3_High;
		decisionAsset4.path_icon = "ui/Icons/actor_traits/iconMadness";
		decisionAsset4.cooldown = 60;
		decisionAsset4.unique = true;
		decisionAsset4.action_check_launch = ((Actor pActor) => pActor.hasSubspecies() && pActor.subspecies.can_process_emotions);
		decisionAsset4.weight = 2.5f;
		this.add(decisionAsset4);
		DecisionAsset decisionAsset5 = new DecisionAsset();
		decisionAsset5.id = "try_to_steal_money";
		decisionAsset5.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset5.path_icon = "ui/Icons/actor_traits/iconThief";
		decisionAsset5.cooldown = 60;
		decisionAsset5.unique = true;
		decisionAsset5.action_check_launch = ((Actor pActor) => pActor.isHungry() && pActor.canGetFoodFromCity());
		decisionAsset5.weight = 1f;
		this.add(decisionAsset5);
		DecisionAsset decisionAsset6 = new DecisionAsset();
		decisionAsset6.id = "kill_unruly_clan_members";
		decisionAsset6.priority = NeuroLayer.Layer_3_High;
		decisionAsset6.path_icon = "ui/Icons/clan_traits/clan_trait_deathbound";
		decisionAsset6.cooldown = 200;
		decisionAsset6.unique = true;
		decisionAsset6.action_check_launch = ((Actor pActor) => pActor.clan.getChief() == pActor && pActor.kingdom.hasEnemies());
		decisionAsset6.weight = 3f;
		this.add(decisionAsset6);
		DecisionAsset decisionAsset7 = new DecisionAsset();
		decisionAsset7.id = "banish_unruly_clan_members";
		decisionAsset7.priority = NeuroLayer.Layer_3_High;
		decisionAsset7.path_icon = "ui/Icons/clan_traits/clan_trait_blood_pact";
		decisionAsset7.cooldown = 200;
		decisionAsset7.unique = true;
		decisionAsset7.action_check_launch = ((Actor pActor) => pActor.clan.getChief() == pActor && pActor.kingdom.hasEnemies());
		decisionAsset7.weight = 3f;
		this.add(decisionAsset7);
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x00066E5C File Offset: 0x0006505C
	private void initDecisionsUnique()
	{
		this.add(new DecisionAsset
		{
			id = "check_heal",
			priority = NeuroLayer.Layer_2_Moderate,
			path_icon = "ui/Icons/iconHealth",
			cooldown = 5,
			unique = true,
			weight = 0.5f
		});
		this.add(new DecisionAsset
		{
			id = "make_skeleton",
			priority = NeuroLayer.Layer_2_Moderate,
			path_icon = "ui/Icons/iconSkeleton",
			cooldown = 10,
			unique = true,
			weight = 0.5f
		});
		this.add(new DecisionAsset
		{
			id = "spawn_fertilizer",
			priority = NeuroLayer.Layer_2_Moderate,
			path_icon = "ui/Icons/iconFertilizerPlants",
			cooldown = 10,
			unique = true,
			weight = 0.5f
		});
		this.add(new DecisionAsset
		{
			id = "random_teleport",
			priority = NeuroLayer.Layer_1_Low,
			path_icon = "ui/Icons/iconArrowDestination",
			cooldown = 20,
			unique = true,
			weight = 0.5f
		});
		DecisionAsset decisionAsset = new DecisionAsset();
		decisionAsset.id = "teleport_back_home";
		decisionAsset.priority = NeuroLayer.Layer_1_Low;
		decisionAsset.path_icon = "ui/Icons/iconArrowDestination";
		decisionAsset.cooldown = 60;
		decisionAsset.action_check_launch = delegate(Actor pActor)
		{
			if (!pActor.hasCity())
			{
				return false;
			}
			if (!Randy.randomChance(0.3f))
			{
				return false;
			}
			WorldTile tCityTile = pActor.city.getTile(false);
			return tCityTile != null && !tCityTile.isSameIsland(pActor.current_tile);
		};
		decisionAsset.unique = true;
		decisionAsset.weight = 1f;
		this.add(decisionAsset);
		DecisionAsset decisionAsset2 = new DecisionAsset();
		decisionAsset2.id = "check_cure";
		decisionAsset2.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset2.path_icon = "ui/Icons/iconHealth";
		decisionAsset2.cooldown = 15;
		decisionAsset2.unique = true;
		decisionAsset2.action_check_launch = ((Actor _) => Randy.randomChance(0.3f));
		decisionAsset2.weight = 0.5f;
		this.add(decisionAsset2);
		DecisionAsset decisionAsset3 = new DecisionAsset();
		decisionAsset3.id = "burn_tumors";
		decisionAsset3.priority = NeuroLayer.Layer_3_High;
		decisionAsset3.path_icon = "ui/Icons/iconFire";
		decisionAsset3.cooldown = 10;
		decisionAsset3.unique = true;
		decisionAsset3.action_check_launch = ((Actor _) => Randy.randomChance(0.5f));
		decisionAsset3.weight = 0.5f;
		this.add(decisionAsset3);
		this.add(new DecisionAsset
		{
			id = "random_move_towards_civ_building",
			priority = NeuroLayer.Layer_1_Low,
			path_icon = "ui/Icons/iconArrowDestination",
			cooldown = 10,
			unique = true,
			weight = 0.5f
		});
		this.add(new DecisionAsset
		{
			id = "random_swim",
			priority = NeuroLayer.Layer_1_Low,
			path_icon = "ui/Icons/iconTileShallowWater",
			cooldown = 10,
			unique = true,
			weight = 2f
		});
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x00067134 File Offset: 0x00065334
	private void initDecisionsOther()
	{
		this.add(new DecisionAsset
		{
			id = "attack_golden_brain",
			priority = NeuroLayer.Layer_3_High,
			path_icon = "ui/Icons/iconGoldBrain",
			cooldown = 60,
			only_mob = true,
			unique = true,
			weight = 1f
		});
	}

	// Token: 0x060006CE RID: 1742 RVA: 0x0006718C File Offset: 0x0006538C
	private void initDecisionsChildren()
	{
		this.add(new DecisionAsset
		{
			id = "child_random_flips",
			priority = NeuroLayer.Layer_1_Low,
			path_icon = "ui/Icons/iconChildren",
			cooldown = 5,
			list_baby = true,
			weight = 0.1f
		});
		this.add(new DecisionAsset
		{
			id = "child_play_at_one_spot",
			priority = NeuroLayer.Layer_1_Low,
			path_icon = "ui/Icons/iconChildren",
			cooldown = 5,
			list_baby = true,
			weight = 0.1f
		});
		this.add(new DecisionAsset
		{
			id = "child_random_jump",
			priority = NeuroLayer.Layer_1_Low,
			path_icon = "ui/Icons/iconChildren",
			cooldown = 5,
			list_baby = true,
			weight = 0.1f
		});
		DecisionAsset decisionAsset = new DecisionAsset();
		decisionAsset.id = "child_follow_parent";
		decisionAsset.priority = NeuroLayer.Layer_2_Moderate;
		decisionAsset.path_icon = "ui/Icons/iconAdults";
		decisionAsset.cooldown = 10;
		decisionAsset.unique = true;
		decisionAsset.action_check_launch = ((Actor pActor) => pActor.family.hasFounders() && pActor.isBaby());
		decisionAsset.weight = 0.2f;
		this.add(decisionAsset);
	}

	// Token: 0x060006CF RID: 1743 RVA: 0x000672C8 File Offset: 0x000654C8
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (DecisionAsset tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
		}
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x00067328 File Offset: 0x00065528
	public override void linkAssets()
	{
		using (ListPool<DecisionAsset> tOnlyCiv = new ListPool<DecisionAsset>())
		{
			using (ListPool<DecisionAsset> tOnlyCivChildren = new ListPool<DecisionAsset>())
			{
				using (ListPool<DecisionAsset> tOnlyCity = new ListPool<DecisionAsset>())
				{
					using (ListPool<DecisionAsset> tOnlyAnimal = new ListPool<DecisionAsset>())
					{
						using (ListPool<DecisionAsset> tOthers = new ListPool<DecisionAsset>())
						{
							int tCurIndex = 0;
							foreach (DecisionAsset tAsset in this.list)
							{
								tAsset.decision_index = tCurIndex++;
								tAsset.priority_int_cached = (int)tAsset.priority;
								tAsset.has_weight_custom = (tAsset.weight_calculate_custom != null);
								if (!tAsset.unique)
								{
									if (tAsset.list_baby)
									{
										tOnlyCivChildren.Add(tAsset);
									}
									else if (tAsset.list_animal)
									{
										tOnlyAnimal.Add(tAsset);
									}
									else if (tAsset.list_civ)
									{
										tOnlyCiv.Add(tAsset);
									}
									else
									{
										tOthers.Add(tAsset);
									}
								}
							}
							this.list_only_civ = tOnlyCiv.ToArray<DecisionAsset>();
							this.list_only_children = tOnlyCivChildren.ToArray<DecisionAsset>();
							this.list_only_city = tOnlyCity.ToArray<DecisionAsset>();
							this.list_only_animal = tOnlyAnimal.ToArray<DecisionAsset>();
							this.list_others = tOthers.ToArray<DecisionAsset>();
							base.linkAssets();
						}
					}
				}
			}
		}
	}

	// Token: 0x060006D1 RID: 1745 RVA: 0x00067514 File Offset: 0x00065714
	public override void editorDiagnostic()
	{
		foreach (DecisionAsset tAsset in this.list)
		{
			base.checkSpriteExists("path_icon", tAsset.path_icon, tAsset);
		}
		foreach (DecisionAsset tDecisionAsset in this.list)
		{
			int tCount = 0;
			if (tDecisionAsset.unique && (tDecisionAsset.list_civ || tDecisionAsset.list_baby || tDecisionAsset.list_animal))
			{
				BaseAssetLibrary.logAssetError("<e>" + tDecisionAsset.id + "</e>: Unique but also has list setting?", null);
			}
			if (this.list_only_civ.Contains(tDecisionAsset))
			{
				tCount++;
			}
			if (this.list_only_children.Contains(tDecisionAsset))
			{
				tCount++;
			}
			if (this.list_only_city.Contains(tDecisionAsset))
			{
				tCount++;
			}
			if (this.list_only_animal.Contains(tDecisionAsset))
			{
				tCount++;
			}
			if (this.list_others.Contains(tDecisionAsset))
			{
				tCount++;
			}
			if (tDecisionAsset.unique && tCount > 0)
			{
				BaseAssetLibrary.logAssetError("<e>" + tDecisionAsset.id + "</e>: Unique but also in a list?", null);
			}
			if (!tDecisionAsset.unique && tCount == 0)
			{
				BaseAssetLibrary.logAssetError("<e>" + tDecisionAsset.id + "</e>: Not unique but not in any list?", null);
			}
			if (tCount > 1)
			{
				BaseAssetLibrary.logAssetError("<e>" + tDecisionAsset.id + "</e>: In multiple lists?", null);
			}
		}
		base.editorDiagnostic();
	}

	// Token: 0x04000772 RID: 1906
	public DecisionAsset[] list_only_civ;

	// Token: 0x04000773 RID: 1907
	public DecisionAsset[] list_only_children;

	// Token: 0x04000774 RID: 1908
	public DecisionAsset[] list_only_city;

	// Token: 0x04000775 RID: 1909
	public DecisionAsset[] list_only_animal;

	// Token: 0x04000776 RID: 1910
	public DecisionAsset[] list_others;
}
