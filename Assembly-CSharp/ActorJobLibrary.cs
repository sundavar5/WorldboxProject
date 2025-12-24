using System;
using ai.behaviours.conditions;

// Token: 0x020003BB RID: 955
public class ActorJobLibrary : AssetLibrary<ActorJob>
{
	// Token: 0x06002230 RID: 8752 RVA: 0x0011F946 File Offset: 0x0011DB46
	public override void init()
	{
		base.init();
		this.initJobsCivs();
		this.initJobsMobs();
	}

	// Token: 0x06002231 RID: 8753 RVA: 0x0011F95C File Offset: 0x0011DB5C
	private void initJobsCivs()
	{
		this.add(new ActorJob
		{
			id = "unit_citizen"
		});
		this.t.addTask("make_decision");
		this.t.addTask("check_city_destroyed");
		this.add(new ActorJob
		{
			id = "hunter"
		});
		this.t.addTask("random_move");
		this.t.addTask("do_hunting");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "builder"
		});
		this.t.addTask("try_build_building");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "cleaner"
		});
		this.t.addTask("cleaning");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "manure_cleaner"
		});
		this.t.addTask("manure_cleaning");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "road_builder"
		});
		this.t.addTask("build_road");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "woodcutter"
		});
		this.t.addTask("chop_trees");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "miner"
		});
		this.t.addTask("mine");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "miner_deposit"
		});
		this.t.addTask("mine_deposit");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "gatherer_bushes"
		});
		this.t.addTask("collect_fruits");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "gatherer_herbs"
		});
		this.t.addTask("collect_herbs");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "gatherer_honey"
		});
		this.t.addTask("collect_honey");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "farmer"
		});
		this.t.addTask("farmer_make_field");
		this.t.addTask("farmer_plant_crops");
		this.t.addTask("farmer_harvest");
		this.t.addTask("farmer_fertilize_crops");
		this.t.addTask("farmer_random_move");
		this.t.addTask("check_end_job");
		this.t.addTask("check_city_destroyed");
		this.add(new ActorJob
		{
			id = "attacker"
		});
		this.t.addTask("make_decision");
	}

	// Token: 0x06002232 RID: 8754 RVA: 0x0011FCBC File Offset: 0x0011DEBC
	private void initJobsMobs()
	{
		this.add(new ActorJob
		{
			id = "crab"
		});
		this.t.addTask("swim_to_island");
		this.t.addTask("crab_danger_check");
		this.t.addTask("random_move");
		this.t.addTask("crab_danger_check");
		this.t.addTask("crab_eat");
		this.t.addTask("crab_danger_check");
		this.add(new ActorJob
		{
			id = "crab_burrow"
		});
		this.t.addTask("crab_burrow");
		this.add(new ActorJob
		{
			id = "decision"
		});
		this.t.addTask("make_decision");
		this.add(new ActorJob
		{
			id = "egg"
		});
		this.t.addTask("wait10");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "random_move"
		});
		this.t.addTask("random_move");
		this.add(new ActorJob
		{
			id = "random_swim"
		});
		this.t.addTask("random_swim");
		this.add(new ActorJob
		{
			id = "printer_job"
		});
		this.t.addTask("print_start");
		this.t.addTask("print_step");
		this.add(new ActorJob
		{
			id = "godfinger_job",
			random = true
		});
		this.t.addTask("godfinger_move");
		this.t.addTask("godfinger_move");
		this.t.addTask("godfinger_find_target");
		this.t.addTask("godfinger_random_fun_move");
		this.t.addTask("godfinger_random_fun_move");
		this.t.addTask("godfinger_circle_move");
		this.t.addTask("godfinger_circle_move");
		this.t.addTask("godfinger_circle_move_big");
		this.t.addTask("godfinger_circle_move_big");
		this.t.addTask("godfinger_circle_move_small");
		this.t.addTask("godfinger_circle_move_small");
		this.add(new ActorJob
		{
			id = "dragon_job",
			random = true
		});
		this.t.addTask("dragon_slide");
		this.t.addCondition(new CondActorFlying(), true);
		this.t.addCondition(new CondNoPeace(), true);
		this.t.addCondition(new CondDragonHasTargets(), true);
		this.t.addCondition(new CondDragonCanSlide(), true);
		this.t.addTask("dragon_fly");
		this.t.addCondition(new CondActorFlying(), true);
		this.t.addTask("dragon_land");
		this.t.addCondition(new CondActorFlying(), true);
		this.t.addCondition(new CondDragonCanLand(), true);
		this.t.addTask("dragon_sleep");
		this.t.addCondition(new CondActorFlying(), false);
		this.t.addCondition(new CondDragonSleepy(), true);
		this.t.addCondition(new CondDragonHasTargets(), false);
		this.t.addCondition(new CondDragonHasCityTarget(), false);
		this.t.addCondition(new CondCurrentTileNoOtherUnits(), true);
		this.t.addTask("dragon_wakeup");
		this.t.addCondition(new CondActorFlying(), false);
		this.t.addCondition(new CondDragonSleeping(), true);
		this.t.addTask("dragon_land_attack");
		this.t.addCondition(new CondActorFlying(), false);
		this.t.addCondition(new CondNoPeace(), true);
		this.t.addCondition(new CondDragonCanLandAttack(), true);
		this.t.addTask("dragon_idle");
		this.t.addCondition(new CondActorFlying(), false);
		this.t.addCondition(new CondDragonHasTargets(), false);
		this.t.addCondition(new CondDragonHasCityTarget(), false);
		this.t.addTask("dragon_up");
		this.t.addCondition(new CondActorFlying(), false);
		this.t.addCondition(new CondActorNotJustLanded(), true);
		this.t.addCondition(new CondDragonCanLandAttack(), false);
		this.add(new ActorJob
		{
			id = "ufo_job"
		});
		this.t.addTask("ufo_idle");
		this.t.addTask("ufo_fly");
		this.t.addTask("ufo_explore");
		this.add(new ActorJob
		{
			id = "worm_job"
		});
		this.t.addTask("worm_move");
		this.add(new ActorJob
		{
			id = "sandspider_job"
		});
		this.t.addTask("sandspider_move");
		this.add(new ActorJob
		{
			id = "ant_black"
		});
		this.t.addTask("ant_black_island");
		this.t.addTask("ant_black_sand");
		this.add(new ActorJob
		{
			id = "ant_red"
		});
		this.t.addTask("ant_red_move");
		this.add(new ActorJob
		{
			id = "ant_blue"
		});
		this.t.addTask("ant_blue_move");
		this.add(new ActorJob
		{
			id = "ant_green"
		});
		this.t.addTask("ant_green_move");
		this.add(new ActorJob
		{
			id = "skeleton_job"
		});
		this.t.addTask("skeleton_move");
		this.t.addTask("make_decision");
	}
}
