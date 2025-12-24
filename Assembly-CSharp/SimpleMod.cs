using System;
using ai.behaviours;

// Token: 0x02000426 RID: 1062
public class SimpleMod
{
	// Token: 0x06002531 RID: 9521 RVA: 0x001334DC File Offset: 0x001316DC
	public SimpleMod()
	{
		ProjectileAsset projectileAsset = AssetManager.projectiles.clone("skeleton_arrow", "arrow");
		projectileAsset.trail_effect_enabled = true;
		projectileAsset.texture = "fireball";
		projectileAsset.scale_target = 0.5f;
		EquipmentAsset equipmentAsset = AssetManager.items.clone("skeleton_bow", "_range");
		equipmentAsset.base_stats["range"] = 22f;
		equipmentAsset.base_stats["critical_chance"] = 0.1f;
		equipmentAsset.base_stats["critical_damage_multiplier"] = 0.5f;
		equipmentAsset.projectile = "skeleton_arrow";
		ActorAsset actorAsset = AssetManager.actor_library.clone("super_skeleton", "skeleton");
		actorAsset.default_attack = "skeleton_bow";
		actorAsset.default_weapons = null;
		actorAsset.base_stats["health"] = 100000f;
		actorAsset.base_stats["damage"] = 500f;
		actorAsset.base_stats["speed"] = 500f;
		actorAsset.job = Toolbox.a<string>(new string[]
		{
			"super_skeleton_job"
		});
		AssetManager.actor_library.addTrait("regeneration");
		AssetManager.actor_library.addTrait("immortal");
		ActorJob actorJob = AssetManager.job_actor.add(new ActorJob
		{
			id = "super_skeleton_job"
		});
		actorJob.addTask("mod_destroy_trees");
		actorJob.addTask("random_move");
		actorJob.addTask("wait");
		actorJob.addTask("attack_golden_brain");
		BehaviourTaskActor behaviourTaskActor = AssetManager.tasks_actor.add(new BehaviourTaskActor
		{
			id = "mod_destroy_trees"
		});
		behaviourTaskActor.addBeh(new BehFindBuilding("type_tree", true, true));
		behaviourTaskActor.addBeh(new BehGoToBuildingTarget(false));
		behaviourTaskActor.addBeh(new BehResourceGatheringAnimation(1f, "", true));
		behaviourTaskActor.addBeh(new BehResourceGatheringAnimation(1f, "", true));
		behaviourTaskActor.addBeh(new BehResourceGatheringAnimation(1f, "", true));
		behaviourTaskActor.addBeh(new BehResourceGatheringAnimation(1f, "", true));
		behaviourTaskActor.addBeh(new BehExtractResourcesFromBuilding());
		behaviourTaskActor.addBeh(new BehRandomWait(1f, 2f, false));
	}
}
