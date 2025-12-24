using System;
using UnityEngine;

// Token: 0x020000F9 RID: 249
public class EffectsLibrary : AssetLibrary<EffectAsset>
{
	// Token: 0x06000744 RID: 1860 RVA: 0x0006A87C File Offset: 0x00068A7C
	public override void init()
	{
		base.init();
		this.add(new EffectAsset
		{
			id = "fx_spores",
			prefab_id = "effects/prefabs/PrefabSpores",
			show_on_mini_map = true,
			limit = 200
		});
		this.add(new EffectAsset
		{
			id = "fx_fireball_explosion",
			sprite_path = "effects/fx_fireball_explosion",
			sorting_layer_id = "EffectsTop",
			use_basic_prefab = true,
			draw_light_area = true,
			sound_launch = "event:/SFX/EXPLOSIONS/ExplosionSmall"
		});
		this.add(new EffectAsset
		{
			id = "fx_firebomb_explosion",
			sprite_path = "effects/fx_firebomb_explosion",
			sorting_layer_id = "EffectsTop",
			use_basic_prefab = true,
			draw_light_area = true,
			sound_launch = "event:/SFX/EXPLOSIONS/ExplosionSmall"
		});
		this.add(new EffectAsset
		{
			id = "fx_plasma_ball_explosion",
			sprite_path = "effects/fx_plasma_ball_explosion",
			sorting_layer_id = "EffectsTop",
			use_basic_prefab = true,
			draw_light_area = true
		});
		this.add(new EffectAsset
		{
			id = "fx_cast_ground_blue",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsBack",
			sprite_path = "effects/fx_cast_ground_blue_t",
			draw_light_area = true,
			draw_light_size = 0.2f,
			limit = 60
		});
		this.add(new EffectAsset
		{
			id = "fx_cast_top_blue",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsTop",
			sprite_path = "effects/fx_cast_top_blue_t",
			draw_light_area = true,
			draw_light_size = 0.2f,
			limit = 60
		});
		this.add(new EffectAsset
		{
			id = "fx_cast_ground_red",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsBack",
			sprite_path = "effects/fx_cast_ground_red_t",
			draw_light_area = true,
			draw_light_size = 0.2f,
			limit = 60
		});
		this.add(new EffectAsset
		{
			id = "fx_cast_top_red",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsTop",
			sprite_path = "effects/fx_cast_top_red_t",
			draw_light_area = true,
			draw_light_size = 0.2f,
			limit = 60
		});
		this.add(new EffectAsset
		{
			id = "fx_cast_ground_green",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsBack",
			sprite_path = "effects/fx_cast_ground_green_t",
			draw_light_area = true,
			draw_light_size = 0.2f,
			limit = 60
		});
		this.add(new EffectAsset
		{
			id = "fx_cast_ground_purple",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsBack",
			sprite_path = "effects/fx_cast_ground_purple_t",
			draw_light_area = true,
			draw_light_size = 0.2f,
			limit = 60
		});
		this.add(new EffectAsset
		{
			id = "fx_cast_top_green",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsTop",
			sprite_path = "effects/fx_cast_top_green_t",
			draw_light_area = true,
			draw_light_size = 0.2f,
			limit = 60
		});
		this.add(new EffectAsset
		{
			id = "fx_create_skeleton",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsTop",
			sprite_path = "effects/fx_create_skeleton_t",
			draw_light_area = true,
			show_on_mini_map = true,
			limit = 0
		});
		this.add(new EffectAsset
		{
			id = "fx_teleport_blue",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsTop",
			sprite_path = "effects/fx_teleport_blue_t",
			draw_light_area = true,
			limit = 100
		});
		this.add(new EffectAsset
		{
			id = "fx_teleport_red",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsTop",
			sprite_path = "effects/fx_teleport_red_t",
			draw_light_area = true,
			limit = 100
		});
		this.add(new EffectAsset
		{
			id = "fx_shield_hit",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsTop",
			sprite_path = "effects/fx_shield_hit_t",
			draw_light_area = true,
			limit = 200
		});
		this.add(new EffectAsset
		{
			id = "fx_dodge",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsBack",
			sprite_path = "effects/combat_actions/fx_action_dodge_t",
			limit = 100
		});
		this.add(new EffectAsset
		{
			id = "fx_block",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsTop",
			sprite_path = "effects/combat_actions/fx_action_block_t",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_drowning",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsTop",
			sprite_path = "effects/fx_status_drowning_t",
			limit = 50
		});
		this.add(new EffectAsset
		{
			id = "fx_water_splash",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsTop",
			sprite_path = "effects/fx_status_drowning_t",
			limit_unload = true,
			limit = 50
		});
		this.add(new EffectAsset
		{
			id = "fx_grin_reaper",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsTop",
			sprite_path = "effects/fx_grin_reaper_animation",
			show_on_mini_map = false,
			time_between_frames = 0.001f,
			limit = 20
		});
		this.add(new EffectAsset
		{
			id = "fx_monolith_launch",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsTop",
			sprite_path = "effects/fx_monolith_launch",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_monolith_launch_bottom",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsBack",
			sprite_path = "effects/fx_monolith_launch_bottom",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_monolith_glow_1",
			use_basic_prefab = true,
			sorting_layer_id = "Objects",
			sprite_path = "effects/fx_monolith_glow_1",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_monolith_glow_2",
			use_basic_prefab = true,
			sorting_layer_id = "Objects",
			sprite_path = "effects/fx_monolith_glow_2",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_waypoint_alien_mold_launch_bottom",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsBack",
			sprite_path = "effects/fx_waypoint_alien_mold_launch_bottom",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_waypoint_computer_launch_bottom",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsTop",
			sprite_path = "effects/fx_waypoint_computer_launch_bottom",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_waypoint_golden_egg_launch_bottom",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsTop",
			sprite_path = "effects/fx_waypoint_golden_egg_launch_bottom",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_waypoint_harp_launch_bottom",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsBack",
			sprite_path = "effects/fx_waypoint_harp_launch_bottom",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_bad_place",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsBack",
			sprite_path = "effects/fx_bad_place_t",
			draw_light_area = true,
			limit = 10,
			show_on_mini_map = true
		});
		this.add(new EffectAsset
		{
			id = "fx_debug_tile",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsTop",
			sprite_path = "effects/fx_debug_tile",
			draw_light_area = true,
			time_between_frames = 3f,
			show_on_mini_map = true
		});
		this.add(new EffectAsset
		{
			id = "fx_move",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsBack",
			sprite_path = "effects/fx_move_t",
			draw_light_area = true,
			limit = 30,
			show_on_mini_map = true
		});
		this.add(new EffectAsset
		{
			id = "fx_plasma_trail",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsTop",
			sprite_path = "effects/fx_plasma_trail_t",
			draw_light_area = true,
			show_on_mini_map = true,
			limit = 15
		});
		this.add(new EffectAsset
		{
			id = "fx_building_sparkle",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsTop",
			sprite_path = "effects/fx_building_sparkle_t",
			limit = 15
		});
		this.add(new EffectAsset
		{
			id = "fx_fire_smoke",
			prefab_id = "Prefabs/PrefabFireSmoke",
			show_on_mini_map = true
		});
		this.add(new EffectAsset
		{
			id = "fx_boulder_charge",
			prefab_id = "Prefabs/PrefabBoulderCharge",
			show_on_mini_map = true
		});
		this.add(new EffectAsset
		{
			id = "fx_spark",
			prefab_id = "Prefabs/PrefabSpark",
			show_on_mini_map = true
		});
		this.add(new EffectAsset
		{
			id = "fx_lightning_big",
			prefab_id = "effects/prefabs/PrefabLightning",
			show_on_mini_map = true,
			limit = 100,
			draw_light_area = true,
			draw_light_size = 2f,
			draw_light_area_offset_y = 5f,
			sound_launch = "event:/SFX/EXPLOSIONS/ExplosionLightningStrike"
		});
		this.add(new EffectAsset
		{
			id = "fx_lightning_medium",
			prefab_id = "effects/prefabs/PrefabLightningMedium",
			show_on_mini_map = true,
			limit = 100,
			draw_light_area = true,
			draw_light_size = 2f,
			draw_light_area_offset_y = 5f,
			sound_launch = "event:/SFX/EXPLOSIONS/ExplosionLightningStrike"
		});
		this.add(new EffectAsset
		{
			id = "fx_lightning_small",
			prefab_id = "effects/prefabs/PrefabLightningSmall",
			show_on_mini_map = true,
			limit = 100,
			draw_light_area = true,
			draw_light_size = 2f,
			draw_light_area_offset_y = 5f,
			sound_launch = "event:/SFX/EXPLOSIONS/ExplosionLightningStrike"
		});
		this.add(new EffectAsset
		{
			id = "fx_spawn",
			prefab_id = "effects/prefabs/PrefabSpawnSmall",
			show_on_mini_map = false,
			draw_light_area = true,
			spawn_action = new EffectAction(this.showSpawnEffect),
			limit = 100
		});
		this.add(new EffectAsset
		{
			id = "fx_teleport_singularity",
			use_basic_prefab = true,
			sorting_layer_id = "EffectsTop",
			sprite_path = "effects/fx_teleport_singularity",
			draw_light_area = true,
			limit = 0
		});
		this.add(new EffectAsset
		{
			id = "fx_spawn_big",
			prefab_id = "effects/prefabs/PrefabSpawnBig",
			show_on_mini_map = true,
			spawn_action = new EffectAction(this.spawnSimpleTile),
			draw_light_area = true,
			draw_light_size = 2f,
			sound_launch = "event:/SFX/UNIQUE/Crabzilla/CrabzillaSpawn"
		});
		this.add(new EffectAsset
		{
			id = "fx_land_explosion_old",
			prefab_id = "effects/prefabs/PrefabFireballExplosion",
			show_on_mini_map = true,
			draw_light_area = true,
			sound_launch = "event:/SFX/EXPLOSIONS/ExplosionSmall"
		});
		this.add(new EffectAsset
		{
			id = "fx_explosion_crab_bomb",
			prefab_id = "effects/prefabs/PrefabFireballExplosion",
			show_on_mini_map = true,
			draw_light_area = true,
			sound_launch = "event:/SFX/EXPLOSIONS/ExplosionCrabBomb"
		});
		this.add(new EffectAsset
		{
			id = "fx_explosion_tiny",
			prefab_id = "effects/prefabs/PrefabExplosionSmall",
			show_on_mini_map = true,
			draw_light_area = true,
			sound_launch = "event:/SFX/EXPLOSIONS/ExplosionTiny"
		});
		this.add(new EffectAsset
		{
			id = "fx_explosion_small",
			prefab_id = "effects/prefabs/PrefabExplosionSmall",
			show_on_mini_map = true,
			draw_light_area = true,
			draw_light_size = 1f,
			sound_launch = "event:/SFX/EXPLOSIONS/ExplosionSmall"
		});
		this.add(new EffectAsset
		{
			id = "fx_explosion_ufo",
			prefab_id = "effects/prefabs/PrefabExplosionSmall",
			show_on_mini_map = true,
			draw_light_area = true,
			draw_light_size = 1f,
			sound_launch = "event:/SFX/EXPLOSIONS/ExplosionUFO"
		});
		this.add(new EffectAsset
		{
			id = "fx_explosion_meteorite",
			prefab_id = "effects/prefabs/PrefabExplosionSmall",
			show_on_mini_map = true,
			draw_light_area = true,
			draw_light_size = 2f,
			sound_launch = "event:/SFX/EXPLOSIONS/ExplosionMeteorite"
		});
		this.add(new EffectAsset
		{
			id = "fx_explosion_middle",
			prefab_id = "effects/prefabs/PrefabExplosionSmall",
			show_on_mini_map = true,
			draw_light_area = true,
			draw_light_size = 2f,
			sound_launch = "event:/SFX/EXPLOSIONS/ExplosionSmall"
		});
		this.add(new EffectAsset
		{
			id = "fx_explosion_nuke_atomic",
			show_on_mini_map = true,
			prefab_id = "effects/prefabs/PrefabExplosionBig",
			draw_light_area = true,
			draw_light_size = 5f,
			sound_launch = "event:/SFX/EXPLOSIONS/ExplosionBig"
		});
		this.add(new EffectAsset
		{
			id = "fx_explosion_huge",
			show_on_mini_map = true,
			prefab_id = "effects/prefabs/PrefabExplosionBig",
			draw_light_area = true,
			draw_light_size = 5f,
			sound_launch = "event:/SFX/EXPLOSIONS/ExplosionHuge"
		});
		this.add(new EffectAsset
		{
			id = "fx_explosion_wave",
			prefab_id = "effects/prefabs/PrefabExplosionWave",
			show_on_mini_map = true
		});
		this.add(new EffectAsset
		{
			id = "fx_fireworks",
			prefab_id = "effects/prefabs/PrefabFireworks",
			show_on_mini_map = true,
			spawn_action = new EffectAction(this.spawnFireworks),
			cooldown_interval = 0.20000000298023224,
			draw_light_area = true,
			draw_light_size = 4f,
			draw_light_area_offset_y = 40f,
			sound_launch = "event:/SFX/EXPLOSIONS/ExplosionFireworks"
		});
		this.add(new EffectAsset
		{
			id = "fx_hearts",
			prefab_id = "effects/prefabs/PrefabHearts",
			sorting_layer_id = "EffectsTop",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_new_border",
			prefab_id = "effects/prefabs/PrefabNewBorder",
			sorting_layer_id = "EffectsTop",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_money_got_loot",
			prefab_id = "effects/prefabs/PrefabMoneyGotLoot",
			sorting_layer_id = "EffectsTop",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_money_got_money",
			prefab_id = "effects/prefabs/PrefabMoneyGotMoney",
			sorting_layer_id = "EffectsTop",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_money_paid_tax",
			prefab_id = "effects/prefabs/PrefabMoneyPaidTax",
			sorting_layer_id = "EffectsTop",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_money_paid_tribute",
			prefab_id = "effects/prefabs/PrefabMoneyPaidTribute",
			sorting_layer_id = "EffectsTop",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_money_spend_money",
			prefab_id = "effects/prefabs/PrefabMoneySpendMoney",
			sorting_layer_id = "EffectsTop",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_conversion_religion",
			load_texture = true,
			prefab_id = "effects/prefabs/PrefabMetaEvent",
			sprite_path = "effects/fx_conversion_religion",
			sorting_layer_id = "EffectsTop",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_conversion_culture",
			load_texture = true,
			prefab_id = "effects/prefabs/PrefabMetaEvent",
			sprite_path = "effects/fx_conversion_culture",
			sorting_layer_id = "EffectsTop",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_conversion_language",
			load_texture = true,
			prefab_id = "effects/prefabs/PrefabMetaEvent",
			sprite_path = "effects/fx_conversion_language",
			sorting_layer_id = "EffectsTop",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_experience_gain",
			load_texture = true,
			prefab_id = "effects/prefabs/PrefabMetaEvent",
			sprite_path = "effects/fx_experience_gain",
			sorting_layer_id = "EffectsTop",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_change_happiness_positive",
			load_texture = true,
			prefab_id = "effects/prefabs/PrefabMetaEvent",
			sprite_path = "effects/fx_change_happiness_positive",
			sorting_layer_id = "EffectsTop",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_change_happiness_negative",
			load_texture = true,
			prefab_id = "effects/prefabs/PrefabMetaEvent",
			sprite_path = "effects/fx_change_happiness_negative",
			sorting_layer_id = "EffectsTop",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_hmm",
			prefab_id = "effects/prefabs/PrefabHmm",
			sorting_layer_id = "EffectsTop",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_plot_progress",
			prefab_id = "effects/prefabs/PrefabPlotProgress",
			sorting_layer_id = "EffectsTop",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_nuke_flash",
			prefab_id = "effects/prefabs/PrefabNukeFlash",
			show_on_mini_map = true,
			draw_light_area = true,
			draw_light_size = 3f,
			spawn_action = new EffectAction(this.spawnNukeFlash)
		});
		this.add(new EffectAsset
		{
			id = "fx_napalm_flash",
			show_on_mini_map = true,
			sound_launch = "event:/SFX/EXPLOSIONS/ExplosionMiddle",
			prefab_id = "effects/prefabs/PrefabNapalmFlash",
			draw_light_area = true,
			draw_light_size = 2f,
			spawn_action = new EffectAction(this.spawnNapalmFlash)
		});
		this.add(new EffectAsset
		{
			id = "fx_thunder_flash",
			prefab_id = "effects/prefabs/PrefabThunderFlash",
			limit = 3,
			show_on_mini_map = true,
			sound_launch = "event:/SFX/EXPLOSIONS/ExplosionLightningStrike",
			spawn_action = new EffectAction(this.spawnThunderFlash)
		});
		this.add(new EffectAsset
		{
			id = "fx_boulder_impact",
			prefab_id = "effects/prefabs/PrefabBoulderImpact",
			show_on_mini_map = true,
			sound_launch = "event:/SFX/DESTRUCTION/DropSimpleImpact"
		});
		this.add(new EffectAsset
		{
			id = "fx_boulder_impact_water",
			prefab_id = "effects/prefabs/PrefabBoulderImpactWater",
			show_on_mini_map = true,
			sound_launch = "event:/SFX/DESTRUCTION/DropSimpleImpact"
		});
		this.add(new EffectAsset
		{
			id = "fx_antimatter_effect",
			prefab_id = "effects/prefabs/PrefabAntimatterEffect",
			show_on_mini_map = true,
			sound_launch = "event:/SFX/EXPLOSIONS/ExplosionAntimatterBomb",
			spawn_action = new EffectAction(this.spawnSimpleTile)
		});
		this.add(new EffectAsset
		{
			id = "fx_infinity_coin",
			show_on_mini_map = true,
			prefab_id = "effects/prefabs/PrefabInfinityCoin",
			spawn_action = new EffectAction(this.spawnSimpleTile),
			draw_light_area = true,
			draw_light_size = 1f,
			sound_launch = "event:/SFX/DESTRUCTION/InfinityCoin"
		});
		this.add(new EffectAsset
		{
			id = "fx_status_particle",
			prefab_id = "effects/prefabs/PrefabStatusParticle",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_weapon_particle",
			prefab_id = "effects/prefabs/PrefabStatusParticle",
			limit = 50
		});
		this.add(new EffectAsset
		{
			id = "fx_slash",
			prefab_id = "effects/prefabs/PrefabSlash",
			limit = 40
		});
		this.add(new EffectAsset
		{
			id = "fx_hit",
			prefab_id = "effects/prefabs/PrefabHit",
			limit = 20
		});
		this.add(new EffectAsset
		{
			id = "fx_miss",
			prefab_id = "effects/prefabs/PrefabMiss",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_jump",
			sorting_layer_id = "EffectsBack",
			load_texture = true,
			sprite_path = "effects/jump",
			use_basic_prefab = true,
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_walk",
			sorting_layer_id = "EffectsBack",
			load_texture = true,
			sprite_path = "effects/walk",
			limit = 15,
			use_basic_prefab = true,
			cooldown_interval = 0.15000000596046448
		});
		this.add(new EffectAsset
		{
			id = "fx_hit_critical",
			prefab_id = "effects/prefabs/PrefabHitCritical",
			limit = 10
		});
		this.add(new EffectAsset
		{
			id = "fx_boat_explosion",
			prefab_id = "effects/prefabs/PrefabBoatExplosion",
			draw_light_area = true,
			limit = 20
		});
		this.add(new EffectAsset
		{
			id = "fx_fishnet",
			prefab_id = "effects/prefabs/PrefabFishnet",
			limit = 20,
			sound_launch = "event:/SFX/CIVILIZATIONS/SpawnFishnet"
		});
		this.add(new EffectAsset
		{
			id = "fx_tile_effect",
			prefab_id = "effects/prefabs/PrefabTileEffect",
			limit = 20,
			show_on_mini_map = false,
			spawn_action = new EffectAction(this.spawnSimpleTile)
		});
		this.add(new EffectAsset
		{
			id = "fx_cloud",
			prefab_id = "effects/prefabs/PrefabCloud",
			show_on_mini_map = true,
			limit = 200,
			limit_unload = true,
			spawn_action = new EffectAction(this.spawnCloud)
		});
		this.add(new EffectAsset
		{
			id = "fx_meteorite",
			prefab_id = "effects/prefabs/PrefabMeteorite",
			show_on_mini_map = true,
			spawn_action = new EffectAction(this.spawnMeteorite),
			sound_launch = "event:/SFX/DESTRUCTION/FallMeteorite"
		});
		this.add(new EffectAsset
		{
			id = "fx_boulder",
			prefab_id = "effects/prefabs/PrefabBoulder",
			show_on_mini_map = true,
			spawn_action = new EffectAction(this.spawnBoulder)
		});
		this.add(new EffectAsset
		{
			id = "fx_santa",
			prefab_id = "effects/prefabs/PrefabSanta",
			show_on_mini_map = true,
			spawn_action = new EffectAction(this.spawnSanta),
			sound_loop_idle = "event:/SFX/OTHER/RoboSanta/RoboSantaIdleLoop",
			limit = 100
		});
		this.add(new EffectAsset
		{
			id = "fx_zone_highlight",
			prefab_id = "effects/prefabs/PrefabZoneFlash",
			show_on_mini_map = true,
			spawn_action = new EffectAction(this.spawnZoneFlash)
		});
		this.add(new EffectAsset
		{
			id = "fx_tornado",
			prefab_id = "effects/prefabs/PrefabTornado",
			show_on_mini_map = true,
			sound_loop_idle = "event:/SFX/NATURE/TornadoIdleLoop"
		});
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x0006BFEC File Offset: 0x0006A1EC
	public override void editorDiagnostic()
	{
		base.editorDiagnostic();
		foreach (EffectAsset tAsset in this.list)
		{
			if (tAsset.use_basic_prefab || tAsset.load_texture)
			{
				if (tAsset.sorting_layer_id == null)
				{
					BaseAssetLibrary.logAssetError("EffectsLibrary: sorting_layer_id is missing", tAsset.id);
				}
				if (tAsset.sprite_path == null)
				{
					BaseAssetLibrary.logAssetError("EffectsLibrary: sprite_path is missing", tAsset.id);
				}
			}
			if (!tAsset.use_basic_prefab && tAsset.prefab_id == null)
			{
				BaseAssetLibrary.logAssetError("EffectsLibrary: prefab_id is missing", tAsset.id);
			}
		}
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x0006C0A0 File Offset: 0x0006A2A0
	private static BaseEffect check(string pID)
	{
		EffectAsset tAsset = AssetManager.effects_library.get(pID);
		if (tAsset == null)
		{
			return null;
		}
		if (tAsset.cooldown_interval > 0.0 && tAsset.checkIsUnderCooldown())
		{
			return null;
		}
		if (!tAsset.show_on_mini_map && MapBox.isRenderMiniMap())
		{
			return null;
		}
		return World.world.stack_effects.get(pID).spawnNew();
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x0006C100 File Offset: 0x0006A300
	public static BaseEffect spawnAtTileRandomScale(string pID, WorldTile pTile, float pScaleMin, float pScaleMax)
	{
		float tScale = Randy.randomFloat(pScaleMin, pScaleMax);
		return EffectsLibrary.spawnAtTile(pID, pTile, tScale);
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x0006C120 File Offset: 0x0006A320
	public static void spawnDebugTile(WorldTile pTile, Color pColor)
	{
		if (pTile == null)
		{
			return;
		}
		BaseEffect tEffect = EffectsLibrary.spawnAtTile("fx_debug_tile", pTile, 0.75f);
		if (tEffect == null)
		{
			return;
		}
		pColor.a = 0.7f;
		tEffect.sprite_renderer.color = pColor;
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x0006C164 File Offset: 0x0006A364
	public static BaseEffect spawnAtTile(string pID, WorldTile pTile, float pScale)
	{
		BaseEffect tEffect = EffectsLibrary.spawn(pID, pTile, null, null, 0f, -1f, -1f, null);
		if (tEffect == null)
		{
			return null;
		}
		tEffect.prepare(pTile, pScale);
		return tEffect;
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x0006C1A0 File Offset: 0x0006A3A0
	public static BaseEffect spawnAt(string pID, Vector2 pPos, float pScale)
	{
		BaseEffect tEffect = EffectsLibrary.spawn(pID, null, null, null, 0f, pPos.x, pPos.y, null);
		if (tEffect == null)
		{
			return null;
		}
		tEffect.prepare(pPos, pScale);
		return tEffect;
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x0006C1E0 File Offset: 0x0006A3E0
	public static BaseEffect spawnAt(string pID, Vector3 pPos, float pScale)
	{
		BaseEffect tEffect = EffectsLibrary.spawn(pID, null, null, null, 0f, pPos.x, pPos.y, null);
		if (tEffect == null)
		{
			return null;
		}
		tEffect.prepare(pPos, pScale);
		return tEffect;
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x0006C224 File Offset: 0x0006A424
	public static BaseEffect spawn(string pID, WorldTile pTile = null, string pParam1 = null, string pParam2 = null, float pFloatParam1 = 0f, float pX = -1f, float pY = -1f, Actor pActor = null)
	{
		BaseEffect tEffect = EffectsLibrary.check(pID);
		if (tEffect == null)
		{
			return null;
		}
		EffectAsset tAsset = AssetManager.effects_library.get(pID);
		if (tAsset.spawn_action != null)
		{
			tAsset.spawn_action(tEffect, pTile, pParam1, pParam2, pFloatParam1, pActor);
		}
		if (tAsset.has_sound_launch)
		{
			float tX = pX;
			float tY = pY;
			if (pTile != null && tX == -1f && tY == -1f)
			{
				tX = (float)pTile.x;
				tY = (float)pTile.y;
			}
			MusicBox.playSound(tAsset.sound_launch, tX, tY, false, false);
		}
		if (pX != -1f && pY != -1f)
		{
			tEffect.transform.position = new Vector3(pX, pY, 0f);
		}
		if (tAsset.has_sound_loop_idle)
		{
			tEffect.fmod_instance = MusicBox.attachToObject(tAsset.sound_loop_idle, tEffect.gameObject, tEffect);
		}
		return tEffect;
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x0006C2FC File Offset: 0x0006A4FC
	public static void spawnExplosionWave(Vector3 pVec, float pRadius, float pSpeed = 1f)
	{
		BaseEffect tEffects = EffectsLibrary.spawn("fx_explosion_wave", null, null, null, 0f, -1f, -1f, null);
		if (tEffects == null)
		{
			return;
		}
		((ExplosionFlash)tEffects).start(pVec, pRadius, pSpeed);
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x0006C33F File Offset: 0x0006A53F
	public static bool canShowSlashEffect()
	{
		return !World.world.stack_effects.controller_slash_effects.isLimitReached();
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x0006C358 File Offset: 0x0006A558
	public static void spawnSlash(Vector2 pVec, string pPathSprites, float pAngle, float pScaleMod = 0.1f)
	{
		BaseEffect tObject = EffectsLibrary.spawn("fx_slash", null, null, null, 0f, -1f, -1f, null);
		if (tObject == null)
		{
			return;
		}
		tObject.prepare(pVec, pScaleMod);
		SpriteAnimation component = tObject.GetComponent<SpriteAnimation>();
		Sprite[] sprites = SpriteTextureLoader.getSpriteList(pPathSprites, false);
		component.setFrames(sprites);
		tObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, pAngle));
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x0006C3C9 File Offset: 0x0006A5C9
	public BaseEffect spawnMeteorite(BaseEffect pEffect, WorldTile pTile, string pParam1, string pParam2 = null, float pFloatParam1 = 0f, Actor pActor = null)
	{
		((Meteorite)pEffect).spawnOn(pTile, pParam1, pActor);
		return pEffect;
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x0006C3DB File Offset: 0x0006A5DB
	public BaseEffect spawnSanta(BaseEffect pEffect, WorldTile pTile, string pParam1, string pParam2 = null, float pFloatParam1 = 0f, Actor pActor = null)
	{
		((Santa)pEffect).spawnOn(pTile);
		return pEffect;
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x0006C3EC File Offset: 0x0006A5EC
	public BaseEffect spawnBoulder(BaseEffect pEffect, WorldTile pTile, string pParam1, string pParam2 = null, float pFloatParam1 = 0f, Actor pActor = null)
	{
		Boulder boulder = (Boulder)pEffect;
		Vector2 tPosition = World.world.getMousePos();
		boulder.spawnOn(tPosition);
		return pEffect;
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x0006C411 File Offset: 0x0006A611
	public BaseEffect spawnNapalmFlash(BaseEffect pEffect, WorldTile pTile, string pParam1, string pParam2 = null, float pFloatParam1 = 0f, Actor pActor = null)
	{
		((NapalmFlash)pEffect).spawnFlash(pTile);
		return pEffect;
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x0006C420 File Offset: 0x0006A620
	public BaseEffect spawnNukeFlash(BaseEffect pEffect, WorldTile pTile, string pParam1, string pParam2 = null, float pFloatParam1 = 0f, Actor pActor = null)
	{
		((NukeFlash)pEffect).spawnFlash(pTile, pParam1);
		return pEffect;
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x0006C430 File Offset: 0x0006A630
	public BaseEffect spawnThunderFlash(BaseEffect pEffect, WorldTile pTile, string pParam1, string pParam2 = null, float pFloatParam1 = 0f, Actor pActor = null)
	{
		((ThunderFlash)pEffect).spawnFlash();
		return pEffect;
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x0006C43E File Offset: 0x0006A63E
	public BaseEffect spawnSimpleTile(BaseEffect pEffect, WorldTile pTile, string pParam1 = null, string pParam2 = null, float pFloatParam1 = 0f, Actor pActor = null)
	{
		pEffect.spawnOnTile(pTile);
		return pEffect;
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x0006C448 File Offset: 0x0006A648
	public BaseEffect spawnZoneFlash(BaseEffect pEffect, WorldTile pTile, string pParam1 = null, string pParam2 = null, float pFloatParam1 = 0f, Actor pActor = null)
	{
		((ZoneFlash)pEffect).spawnOnTile(pTile);
		return pEffect;
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x0006C457 File Offset: 0x0006A657
	public BaseEffect spawnCloud(BaseEffect pEffect, WorldTile pTile, string pParam1, string pParam2 = null, float pFloatParam1 = 0f, Actor pActor = null)
	{
		((Cloud)pEffect).spawn(pTile, pParam1);
		return pEffect;
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x0006C467 File Offset: 0x0006A667
	public BaseEffect spawnFireworks(BaseEffect pEffect, WorldTile pTile, string pParam1, string pParam2 = null, float pFloatParam1 = 0f, Actor pActor = null)
	{
		((Fireworks)pEffect).spawnOnTile(pTile);
		return pEffect;
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x0006C476 File Offset: 0x0006A676
	public BaseEffect showSpawnEffect(BaseEffect pEffect, WorldTile pTile, string pParam1, string pParam2 = null, float pFloatParam1 = 0f, Actor pActor = null)
	{
		pEffect.prepare(pTile, 0.2f);
		return pEffect;
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x0006C485 File Offset: 0x0006A685
	public BaseEffect spawnStatusParticle(BaseEffect pEffect, Vector3 pPos)
	{
		return pEffect;
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x0006C488 File Offset: 0x0006A688
	public static void highlightKingdomZones(Kingdom pKingdom, Color pColor, float pAlpha = 0.3f)
	{
		foreach (City city in pKingdom.getCities())
		{
			foreach (TileZone tZone in city.zones)
			{
				((ZoneFlash)EffectsLibrary.spawn("fx_zone_highlight", tZone.centerTile, null, null, pAlpha, -1f, -1f, null)).start(pColor, pAlpha);
			}
		}
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x0006C534 File Offset: 0x0006A734
	public static void showMoneyEffect(string pID, Vector2 pPosition, TileZone pZone, float pScale)
	{
		if (!pZone.visible_main_centered)
		{
			return;
		}
		if (!PlayerConfig.optionBoolEnabled("money_flow"))
		{
			return;
		}
		float tRandomXPos = pPosition.x + Randy.randomFloat(-0.3f, 0.3f);
		pPosition.x = tRandomXPos;
		EffectsLibrary.spawnAt(pID, pPosition, pScale);
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x0006C57F File Offset: 0x0006A77F
	public static void showMetaEventEffectConversion(string pID, Actor pActor)
	{
		if (!PlayerConfig.optionBoolEnabled("meta_conversions"))
		{
			return;
		}
		EffectsLibrary.showMetaEventEffect(pID, pActor.current_position, pActor.current_zone, pActor.actor_scale);
	}

	// Token: 0x0600075F RID: 1887 RVA: 0x0006C5A6 File Offset: 0x0006A7A6
	public static void showMetaEventEffect(string pID, Actor pActor)
	{
		EffectsLibrary.showMetaEventEffect(pID, pActor.current_position, pActor.current_zone, pActor.actor_scale);
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x0006C5C0 File Offset: 0x0006A7C0
	public static void showMetaEventEffect(string pID, Vector2 pPosition, TileZone pZone, float pScale)
	{
		if (!pZone.visible_main_centered)
		{
			return;
		}
		float tRandomXPos = pPosition.x + Randy.randomFloat(-0.3f, 0.3f);
		pPosition.x = tRandomXPos;
		EffectsLibrary.spawnAt(pID, pPosition, pScale);
	}
}
