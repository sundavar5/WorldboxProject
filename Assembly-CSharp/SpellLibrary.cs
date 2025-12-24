using System;
using Beebyte.Obfuscator;

// Token: 0x02000170 RID: 368
[ObfuscateLiterals]
public class SpellLibrary : AssetLibrary<SpellAsset>
{
	// Token: 0x06000B1B RID: 2843 RVA: 0x000A18F4 File Offset: 0x0009FAF4
	public override void init()
	{
		base.init();
		this.add(new SpellAsset
		{
			id = "teleport",
			chance = 0.3f,
			cast_target = CastTarget.Himself,
			health_ratio = 0.6f,
			can_be_used_in_combat = true,
			cost_mana = 15
		});
		this.t.addDecision("random_teleport");
		this.t.addDecision("teleport_back_home");
		this.t.action = new AttackAction(ActionLibrary.teleportRandom);
		this.add(new SpellAsset
		{
			id = "summon_lightning",
			chance = 0.1f,
			min_distance = 6f,
			cost_mana = 5,
			can_be_used_in_combat = true
		});
		this.t.action = new AttackAction(ActionLibrary.castLightning);
		this.add(new SpellAsset
		{
			id = "summon_tornado",
			chance = 0.1f,
			min_distance = 6f,
			cost_mana = 10,
			can_be_used_in_combat = true
		});
		this.t.action = new AttackAction(ActionLibrary.castTornado);
		this.add(new SpellAsset
		{
			id = "cast_curse",
			chance = 0.2f,
			min_distance = 4f,
			cast_entity = CastEntity.UnitsOnly,
			cost_mana = 3,
			can_be_used_in_combat = true
		});
		this.t.action = new AttackAction(ActionLibrary.castCurses);
		this.add(new SpellAsset
		{
			id = "cast_fire",
			chance = 0.2f,
			min_distance = 3f,
			cast_entity = CastEntity.Both,
			can_be_used_in_combat = true,
			cost_mana = 3
		});
		this.t.addDecision("burn_tumors");
		this.t.action = new AttackAction(ActionLibrary.castFire);
		this.add(new SpellAsset
		{
			id = "cast_silence",
			chance = 0.2f,
			min_distance = 6f,
			cast_entity = CastEntity.UnitsOnly,
			can_be_used_in_combat = true,
			cost_mana = 5
		});
		this.t.action = new AttackAction(ActionLibrary.castSpellSilence);
		this.add(new SpellAsset
		{
			id = "cast_blood_rain",
			chance = 0.3f,
			min_distance = 0f,
			health_ratio = 0.9f,
			cast_target = CastTarget.Himself,
			cast_entity = CastEntity.UnitsOnly,
			can_be_used_in_combat = true,
			cost_mana = 2
		});
		this.t.addDecision("check_heal");
		this.t.action = new AttackAction(ActionLibrary.castBloodRain);
		this.add(new SpellAsset
		{
			id = "cast_grass_seeds",
			chance = 0.1f,
			min_distance = 0f,
			cast_target = CastTarget.Region,
			cast_entity = CastEntity.Tile,
			cost_mana = 4
		});
		this.t.action = new AttackAction(ActionLibrary.castSpawnGrassSeeds);
		this.add(new SpellAsset
		{
			id = "spawn_vegetation",
			chance = 0.1f,
			min_distance = 0f,
			cast_target = CastTarget.Region,
			cast_entity = CastEntity.Tile,
			cost_mana = 5
		});
		this.t.addDecision("spawn_fertilizer");
		this.t.action = new AttackAction(ActionLibrary.castSpawnFertilizer);
		this.add(new SpellAsset
		{
			id = "spawn_skeleton",
			chance = 0.2f,
			min_distance = 0f,
			cast_target = CastTarget.Himself,
			can_be_used_in_combat = true,
			cost_mana = 10
		});
		this.t.addDecision("make_skeleton");
		this.t.action = new AttackAction(ActionLibrary.castSpawnSkeleton);
		this.add(new SpellAsset
		{
			id = "cast_shield",
			chance = 0.2f,
			cast_target = CastTarget.Himself,
			can_be_used_in_combat = true,
			cost_mana = 5
		});
		this.t.action = new AttackAction(ActionLibrary.castShieldOnHimself);
		this.add(new SpellAsset
		{
			id = "cast_cure",
			chance = 0.3f,
			min_distance = 0f,
			cast_target = CastTarget.Friendly,
			cast_entity = CastEntity.UnitsOnly,
			cost_mana = 3
		});
		this.t.addDecision("check_cure");
		this.t.action = new AttackAction(ActionLibrary.castCure);
	}

	// Token: 0x06000B1C RID: 2844 RVA: 0x000A1DA0 File Offset: 0x0009FFA0
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (SpellAsset tAsset in this.list)
		{
			if (tAsset.decision_ids != null)
			{
				tAsset.decisions_assets = new DecisionAsset[tAsset.decision_ids.Count];
				for (int i = 0; i < tAsset.decision_ids.Count; i++)
				{
					string tDecisionID = tAsset.decision_ids[i];
					DecisionAsset tDecisionAsset = AssetManager.decisions_library.get(tDecisionID);
					tAsset.decisions_assets[i] = tDecisionAsset;
				}
			}
		}
	}
}
