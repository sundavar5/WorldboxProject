using System;
using System.Collections.Generic;
using UnityEngine;
using UnityPools;

// Token: 0x02000040 RID: 64
public class DropsLibrary : AssetLibrary<DropAsset>
{
	// Token: 0x06000287 RID: 647 RVA: 0x0001714C File Offset: 0x0001534C
	public override void init()
	{
		base.init();
		this.add(new DropAsset
		{
			id = "paint",
			path_texture = "drops/drop_paint",
			animated = true,
			animation_speed = 0.03f,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_paint),
			sound_drop = "event:/SFX/DROPS/DropBlessing",
			type = DropType.DropMagic
		});
		this.add(new DropAsset
		{
			id = "dust_black",
			path_texture = "drops/drop_dust_black",
			animated = true,
			animation_speed = 0.03f,
			default_scale = 0.1f,
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropBlessing",
			type = DropType.DropMagic
		});
		DropAsset t = this.t;
		t.action_landed = (DropsAction)Delegate.Combine(t.action_landed, new DropsAction(DropsLibrary.action_dust_white));
		DropAsset t2 = this.t;
		t2.action_landed = (DropsAction)Delegate.Combine(t2.action_landed, new DropsAction(DropsLibrary.action_dust_red));
		DropAsset t3 = this.t;
		t3.action_landed = (DropsAction)Delegate.Combine(t3.action_landed, new DropsAction(DropsLibrary.action_dust_blue));
		DropAsset t4 = this.t;
		t4.action_landed = (DropsAction)Delegate.Combine(t4.action_landed, new DropsAction(DropsLibrary.action_dust_gold));
		DropAsset t5 = this.t;
		t5.action_landed = (DropsAction)Delegate.Combine(t5.action_landed, new DropsAction(DropsLibrary.action_dust_purple));
		DropAsset t6 = this.t;
		t6.action_landed = (DropsAction)Delegate.Combine(t6.action_landed, new DropsAction(DropsLibrary.action_dust_black));
		this.add(new DropAsset
		{
			id = "dust_white",
			path_texture = "drops/drop_dust_white",
			animated = true,
			animation_speed = 0.03f,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_dust_white),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropBlessing",
			type = DropType.DropMagic
		});
		this.add(new DropAsset
		{
			id = "dust_red",
			path_texture = "drops/drop_dust_red",
			animated = true,
			animation_speed = 0.03f,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_dust_red),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropBlessing",
			type = DropType.DropMagic
		});
		this.add(new DropAsset
		{
			id = "dust_blue",
			path_texture = "drops/drop_dust_blue",
			animated = true,
			animation_speed = 0.03f,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_dust_blue),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropBlessing",
			type = DropType.DropMagic
		});
		this.add(new DropAsset
		{
			id = "dust_gold",
			path_texture = "drops/drop_dust_gold",
			animated = true,
			animation_speed = 0.03f,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_dust_gold),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropBlessing",
			type = DropType.DropMagic
		});
		this.add(new DropAsset
		{
			id = "dust_purple",
			path_texture = "drops/drop_dust_purple",
			animated = true,
			animation_speed = 0.03f,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_dust_purple),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropBlessing",
			type = DropType.DropMagic
		});
		this.add(new DropAsset
		{
			id = "gamma_rain",
			path_texture = "drops/drop_gamma_rain",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_gamma_rain),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropRainGamma",
			type = DropType.DropTraitRain
		});
		this.add(new DropAsset
		{
			id = "delta_rain",
			path_texture = "drops/drop_delta_rain",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_delta_rain),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropRainDelta",
			type = DropType.DropTraitRain
		});
		this.add(new DropAsset
		{
			id = "omega_rain",
			path_texture = "drops/drop_omega_rain",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_omega_rain),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropRainOmeaga",
			type = DropType.DropTraitRain
		});
		this.add(new DropAsset
		{
			id = "loot_rain",
			path_texture = "drops/drop_loot_rain",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_equipment_rain),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropRainGamma",
			type = DropType.DropEquipmentRain
		});
		this.add(new DropAsset
		{
			id = "tnt",
			animated = true,
			path_texture = "drops/drop_tnt",
			animation_speed = 0.03f,
			default_scale = 0.2f,
			action_landed = new DropsAction(DropsLibrary.action_tnt),
			sound_drop = "event:/SFX/DROPS/DropTnt",
			type = DropType.DropTile
		});
		this.add(new DropAsset
		{
			id = "tnt_timed",
			path_texture = "drops/drop_tnttimed",
			default_scale = 0.2f,
			action_landed = new DropsAction(DropsLibrary.action_tnt_timed),
			sound_drop = "event:/SFX/DROPS/DropTnt",
			type = DropType.DropTile
		});
		this.add(new DropAsset
		{
			id = "water_bomb",
			path_texture = "drops/drop_waterbomb",
			default_scale = 0.2f,
			action_landed = new DropsAction(DropsLibrary.action_water_bomb),
			sound_drop = "event:/SFX/DROPS/DropWaterBomb",
			type = DropType.DropTile
		});
		this.add(new DropAsset
		{
			id = "landmine",
			path_texture = "drops/drop_landmine",
			default_scale = 0.2f,
			action_landed = new DropsAction(DropsLibrary.action_landmine),
			sound_drop = "event:/SFX/DROPS/DropLandmine",
			type = DropType.DropTile
		});
		this.add(new DropAsset
		{
			id = "fireworks",
			path_texture = "drops/drop_fireworks",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_fireworks),
			sound_drop = "event:/SFX/DROPS/DropFireworks",
			type = DropType.DropTile
		});
		this.add(new DropAsset
		{
			id = "inspiration",
			path_texture = "drops/drop_inspiration",
			default_scale = 0.2f,
			action_landed = new DropsAction(DropsLibrary.action_inspiration),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropInspiration",
			type = DropType.DropMagic
		});
		this.add(new DropAsset
		{
			id = "discord",
			path_texture = "drops/drop_discord",
			default_scale = 0.2f,
			action_landed = new DropsAction(DropsLibrary.action_discord),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropInspiration",
			type = DropType.DropMagic
		});
		this.add(new DropAsset
		{
			id = "friendship",
			path_texture = "drops/drop_friendship",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_friendship),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropFriendship",
			type = DropType.DropMagic
		});
		this.add(new DropAsset
		{
			id = "spite",
			path_texture = "drops/drop_spite",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_spite),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropSpite",
			type = DropType.DropMagic
		});
		this.add(new DropAsset
		{
			id = "madness",
			path_texture = "drops/drop_madness",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_madness),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropMadness",
			type = DropType.DropTrait
		});
		this.add(new DropAsset
		{
			id = "blessing",
			path_texture = "drops/drop_blessing",
			animated = true,
			animation_speed = 0.03f,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_blessing),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropBlessing",
			type = DropType.DropMagic
		});
		DropAsset t7 = this.t;
		t7.action_landed = (DropsAction)Delegate.Combine(t7.action_landed, new DropsAction(ActionLibrary.action_shrinkTornadoes));
		this.add(new DropAsset
		{
			id = "shield",
			path_texture = "drops/drop_shield",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_shield),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropShield",
			type = DropType.DropStatus
		});
		this.add(new DropAsset
		{
			id = "coffee",
			path_texture = "drops/drop_coffee",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_coffee),
			sound_drop = "event:/SFX/DROPS/DropCoffee",
			type = DropType.DropStatus
		});
		this.add(new DropAsset
		{
			id = "powerup",
			path_texture = "drops/drop_mushroom_powerup",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_powerup),
			sound_drop = "event:/SFX/DROPS/DropPowerup",
			type = DropType.DropMagic
		});
		this.add(new DropAsset
		{
			id = "curse",
			path_texture = "drops/drop_curse",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_curse),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropCurse",
			type = DropType.DropTrait
		});
		DropAsset t8 = this.t;
		t8.action_landed = (DropsAction)Delegate.Combine(t8.action_landed, new DropsAction(ActionLibrary.action_growTornadoes));
		this.add(new DropAsset
		{
			id = "spell_silence",
			path_texture = "drops/drop_spell_silence",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_spell_silence),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropCurse",
			type = DropType.DropTrait
		});
		this.add(new DropAsset
		{
			id = "zombie_infection",
			path_texture = "drops/drop_zombieinfection",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_zombie_infection),
			sound_drop = "event:/SFX/DROPS/DropZombieInfection",
			type = DropType.DropTrait
		});
		this.add(new DropAsset
		{
			id = "mush_spores",
			path_texture = "drops/drop_mushSpores",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_mush_spore),
			sound_drop = "event:/SFX/DROPS/DropMushSpores",
			type = DropType.DropTrait
		});
		this.add(new DropAsset
		{
			id = "plague",
			path_texture = "drops/drop_plague",
			random_frame = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_plague),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropPlague",
			type = DropType.DropTrait
		});
		this.add(new DropAsset
		{
			id = "living_plants",
			path_texture = "drops/drop_blessing",
			animated = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_living_plants),
			sound_drop = "event:/SFX/DROPS/DropLivingPlants",
			type = DropType.DropMagic
		});
		this.add(new DropAsset
		{
			id = "living_house",
			path_texture = "drops/drop_blessing",
			animated = true,
			default_scale = 0.1f,
			action_landed = new DropsAction(DropsLibrary.action_living_house),
			sound_drop = "event:/SFX/DROPS/DropLivingHouse",
			type = DropType.DropMagic
		});
		this.add(new DropAsset
		{
			id = "bomb",
			path_texture = "drops/drop_bomb",
			default_scale = 0.2f,
			falling_height = new Vector2(60f, 70f),
			sound_launch = "event:/SFX/DROPS/DropLaunchBombSmall",
			action_landed = new DropsAction(DropsLibrary.action_bomb),
			type = DropType.DropBomb
		});
		DropAsset t9 = this.t;
		t9.action_launch = (DropsAction)Delegate.Combine(t9.action_launch, new DropsAction(ActionLibrary.increaseDroppedBombsCounter));
		this.add(new DropAsset
		{
			id = "grenade",
			path_texture = "drops/drop_grenade",
			animated = true,
			default_scale = 0.2f,
			animation_speed = 0.03f,
			falling_height = new Vector2(60f, 70f),
			action_landed = new DropsAction(DropsLibrary.action_grenade),
			random_flip = true,
			sound_launch = "event:/SFX/DROPS/DropLaunchGrenade",
			type = DropType.DropBomb
		});
		this.add(new DropAsset
		{
			id = "crab_bomb",
			path_texture = "drops/drop_crab_bomb_parachute",
			animated = true,
			default_scale = 0.1f,
			animation_speed = 0.05f,
			falling_height = new Vector2(60f, 70f),
			action_landed = new DropsAction(DropsLibrary.action_crab_bomb_impact),
			random_flip = true,
			sound_launch = "event:/SFX/DROPS/DropLaunchCrabBomb",
			type = DropType.DropBomb
		});
		this.add(new DropAsset
		{
			id = "crab_bomb_shrapnel",
			path_texture = "drops/drop_crab_bomb_shrapnel",
			animated = true,
			animation_rotation = true,
			animation_rotation_speed_min = 50f,
			animation_rotation_speed_max = 200f,
			default_scale = 0.175f,
			animation_speed = 0.05f,
			falling_height = new Vector2(60f, 70f),
			action_landed = new DropsAction(DropsLibrary.action_crab_bomb_shrapnel),
			random_flip = true,
			sound_launch = "event:/SFX/DROPS/DropLaunchCrabBomb",
			type = DropType.DropBomb,
			surprises_units = true
		});
		this.add(new DropAsset
		{
			id = "napalm_bomb",
			path_texture = "drops/drop_napalmbomb",
			default_scale = 0.2f,
			falling_height = new Vector2(60f, 70f),
			action_landed = new DropsAction(DropsLibrary.action_napalm_bomb),
			random_flip = true,
			type = DropType.DropBomb
		});
		DropAsset t10 = this.t;
		t10.action_launch = (DropsAction)Delegate.Combine(t10.action_launch, new DropsAction(ActionLibrary.increaseDroppedBombsCounter));
		this.add(new DropAsset
		{
			id = "atomic_bomb",
			path_texture = "drops/drop_atomicbomb",
			default_scale = 0.2f,
			falling_height = new Vector2(60f, 70f),
			action_landed = new DropsAction(DropsLibrary.action_atomic_bomb),
			random_flip = true,
			sound_launch = "event:/SFX/DROPS/DropLaunchGrenadeHuge",
			type = DropType.DropBomb
		});
		DropAsset t11 = this.t;
		t11.action_launch = (DropsAction)Delegate.Combine(t11.action_launch, new DropsAction(ActionLibrary.increaseDroppedBombsCounter));
		this.add(new DropAsset
		{
			id = "antimatter_bomb",
			path_texture = "drops/drop_antimatterbomb",
			default_scale = 0.2f,
			falling_height = new Vector2(60f, 70f),
			action_landed = new DropsAction(DropsLibrary.action_antimatter_bomb),
			sound_launch = "event:/SFX/DROPS/DropLaunchGrenadeHuge",
			type = DropType.DropBomb
		});
		DropAsset t12 = this.t;
		t12.action_launch = (DropsAction)Delegate.Combine(t12.action_launch, new DropsAction(ActionLibrary.increaseDroppedBombsCounter));
		this.add(new DropAsset
		{
			id = "czar_bomba",
			path_texture = "drops/drop_czarbomba",
			default_scale = 0.2f,
			falling_height = new Vector2(60f, 70f),
			action_landed = new DropsAction(DropsLibrary.action_czar_bomba),
			sound_launch = "event:/SFX/DROPS/DropLaunchGrenadeHuge",
			type = DropType.DropBomb
		});
		DropAsset t13 = this.t;
		t13.action_launch = (DropsAction)Delegate.Combine(t13.action_launch, new DropsAction(ActionLibrary.increaseDroppedBombsCounter));
		this.add(new DropAsset
		{
			id = "rain",
			path_texture = "drops/drop_rain",
			random_frame = true,
			default_scale = 0.2f,
			falling_height = new Vector2(30f, 45f),
			action_landed = new DropsAction(DropsLibrary.action_rain),
			sound_drop = "event:/SFX/DROPS/DropRain",
			type = DropType.DropGeneric,
			surprises_units = false
		});
		this.add(new DropAsset
		{
			id = "blood_rain",
			path_texture = "drops/drop_blood",
			random_frame = true,
			default_scale = 0.1f,
			falling_height = new Vector2(30f, 45f),
			action_landed_drop = new DropsLandedAction(DropsLibrary.action_blood_rain),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropBloodRain",
			type = DropType.DropMagic
		});
		this.add(new DropAsset
		{
			id = "clone_rain",
			path_texture = "drops/drop_clone",
			random_frame = true,
			default_scale = 0.1f,
			falling_height = new Vector2(30f, 45f),
			action_landed = new DropsAction(DropsLibrary.action_clone_rain),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropBloodRain",
			type = DropType.DropMagic
		});
		this.add(new DropAsset
		{
			id = "jazz",
			path_texture = "drops/drop_jazz",
			random_frame = true,
			default_scale = 0.1f,
			falling_height = new Vector2(30f, 45f),
			action_landed = new DropsAction(this.action_jazz_rain),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropBloodRain",
			type = DropType.DropMagic
		});
		this.add(new DropAsset
		{
			id = "dispel",
			path_texture = "drops/drop_dispel",
			random_frame = true,
			default_scale = 0.1f,
			falling_height = new Vector2(30f, 45f),
			action_landed = new DropsAction(this.action_dispel_rain),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropBloodRain",
			type = DropType.DropMagic
		});
		this.add(new DropAsset
		{
			id = "sleep",
			path_texture = "drops/drop_sleep",
			random_frame = true,
			default_scale = 0.1f,
			falling_height = new Vector2(30f, 45f),
			action_landed = new DropsAction(this.action_sleep_rain),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropBloodRain",
			type = DropType.DropMagic
		});
		this.add(new DropAsset
		{
			id = "cure",
			path_texture = "drops/drop_cure",
			random_frame = true,
			default_scale = 0.1f,
			falling_height = new Vector2(30f, 45f),
			action_landed = new DropsAction(DropsLibrary.action_cure),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropCure",
			type = DropType.DropMagic
		});
		this.add(new DropAsset
		{
			id = "fire",
			path_texture = "drops/drop_fire",
			animated = true,
			animation_speed = 0.03f,
			default_scale = 0.2f,
			falling_height = new Vector2(30f, 45f),
			falling_random_x_move = true,
			particle_interval = 0.3f,
			action_landed = new DropsAction(DropsLibrary.action_fire),
			animation_speed_random = 0.08f,
			random_frame = true,
			random_flip = true,
			sound_drop = "event:/SFX/DROPS/DropFire",
			material = "mat_world_object_lit",
			type = DropType.DropGeneric
		});
		this.add(new DropAsset
		{
			id = "snow",
			path_texture = "drops/drop_snow",
			random_frame = true,
			default_scale = 0.2f,
			falling_speed = 0.3f,
			falling_height = new Vector2(30f, 45f),
			falling_random_x_move = true,
			particle_interval = 0.15f,
			sound_drop = "event:/SFX/DROPS/DropSnow",
			action_landed = new DropsAction(DropsLibrary.action_snow),
			type = DropType.DropGeneric
		});
		this.add(new DropAsset
		{
			id = "life_seed",
			path_texture = "drops/drop_life_seed",
			random_frame = true,
			default_scale = 0.2f,
			falling_speed = 0.3f,
			falling_height = new Vector2(30f, 45f),
			falling_random_x_move = true,
			particle_interval = 0.15f,
			sound_drop = "event:/SFX/DROPS/DropSeedGrass",
			action_landed = new DropsAction(DropsLibrary.action_life_seed),
			type = DropType.DropGeneric
		});
		this.add(new DropAsset
		{
			id = "ash",
			path_texture = "drops/drop_ash",
			random_frame = true,
			default_scale = 0.2f,
			falling_speed = 0.3f,
			falling_height = new Vector2(30f, 45f),
			falling_random_x_move = true,
			particle_interval = 0.15f,
			sound_drop = "event:/SFX/DROPS/DropAsh",
			action_landed = new DropsAction(DropsLibrary.action_ash),
			type = DropType.DropMagic
		});
		this.add(new DropAsset
		{
			id = "magic_rain",
			path_texture = "drops/drop_magic_rain",
			random_frame = true,
			default_scale = 0.2f,
			falling_speed = 0.3f,
			falling_height = new Vector2(30f, 45f),
			falling_random_x_move = true,
			particle_interval = 0.15f,
			sound_drop = "event:/SFX/DROPS/DropMagicRain",
			action_landed = new DropsAction(DropsLibrary.action_magic_rain),
			type = DropType.DropMagic
		});
		this.add(new DropAsset
		{
			id = "rage",
			path_texture = "drops/drop_rage",
			random_frame = true,
			default_scale = 0.2f,
			falling_speed = 0.3f,
			falling_height = new Vector2(30f, 45f),
			falling_random_x_move = true,
			particle_interval = 0.15f,
			sound_drop = "event:/SFX/DROPS/DropRage",
			action_landed = new DropsAction(DropsLibrary.action_rage),
			type = DropType.DropStatus
		});
		this.add(new DropAsset
		{
			id = "acid",
			path_texture = "drops/drop_acid",
			random_frame = true,
			default_scale = 0.2f,
			falling_height = new Vector2(30f, 45f),
			action_landed = new DropsAction(DropsLibrary.action_acid),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropAcid",
			type = DropType.DropMagic
		});
		this.add(new DropAsset
		{
			id = "lava",
			path_texture = "drops/drop_lava",
			animated = true,
			animation_speed = 0.03f,
			default_scale = 0.2f,
			falling_height = new Vector2(30f, 45f),
			action_landed = new DropsAction(DropsLibrary.action_lava),
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropLava",
			type = DropType.DropGeneric
		});
		this.add(new DropAsset
		{
			id = "santa_bomb",
			path_texture = "drops/drop_santabomb",
			random_frame = true,
			default_scale = 0.2f,
			sound_launch = "event:/SFX/DROPS/DropLaunchSantaBomb",
			action_landed = new DropsAction(DropsLibrary.action_santa_bomb),
			type = DropType.DropBomb,
			surprises_units = true
		});
		this.add(new DropAsset
		{
			id = "$spawn_building$",
			path_texture = "drops/drop_stone",
			random_frame = true,
			default_scale = 0.2f,
			falling_height = new Vector2(10f, 15f),
			falling_speed = 5f,
			type = DropType.DropBuilding
		});
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.clone("$biome_seeds$", "$spawn_building$");
		this.t.type = DropType.DropSeed;
		this.t.action_landed = null;
		this.clone("seeds_grass", "$biome_seeds$");
		this.t.path_texture = "drops/drop_seed_grass";
		this.t.falling_speed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_seeds);
		this.t.drop_type_low = "grass_low";
		this.t.drop_type_high = "grass_high";
		this.t.sound_drop = "event:/SFX/DROPS/DropSeedGrass";
		this.clone("seeds_enchanted", "$biome_seeds$");
		this.t.path_texture = "drops/drop_seed_enchanted";
		this.t.falling_speed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_seeds);
		this.t.drop_type_low = "enchanted_low";
		this.t.drop_type_high = "enchanted_high";
		this.t.sound_drop = "event:/SFX/DROPS/DropSeedEnchanted";
		this.clone("seeds_savanna", "$biome_seeds$");
		this.t.path_texture = "drops/drop_seed_savanna";
		this.t.falling_speed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_seeds);
		this.t.drop_type_low = "savanna_low";
		this.t.drop_type_high = "savanna_high";
		this.t.sound_drop = "event:/SFX/DROPS/DropSeedSavanna";
		this.clone("seeds_corrupted", "$biome_seeds$");
		this.t.path_texture = "drops/drop_seed_corrupted";
		this.t.falling_speed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_seeds);
		this.t.drop_type_low = "corrupted_low";
		this.t.drop_type_high = "corrupted_high";
		this.t.sound_drop = "event:/SFX/DROPS/DropSeedCorrupted";
		this.clone("seeds_mushroom", "$biome_seeds$");
		this.t.path_texture = "drops/drop_seed_mushroom";
		this.t.falling_speed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_seeds);
		this.t.drop_type_low = "mushroom_low";
		this.t.drop_type_high = "mushroom_high";
		this.t.sound_drop = "event:/SFX/DROPS/DropSeedMushroom";
		this.clone("seeds_jungle", "$biome_seeds$");
		this.t.path_texture = "drops/drop_seed_jungle";
		this.t.falling_speed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_seeds);
		this.t.drop_type_low = "jungle_low";
		this.t.drop_type_high = "jungle_high";
		this.t.sound_drop = "event:/SFX/DROPS/DropSeedJungle";
		this.clone("seeds_desert", "$biome_seeds$");
		this.t.path_texture = "drops/drop_seed_desert";
		this.t.falling_speed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_seeds);
		this.t.drop_type_low = "desert_low";
		this.t.drop_type_high = "desert_high";
		this.t.sound_drop = "event:/SFX/DROPS/DropSeedDesert";
		this.clone("seeds_lemon", "$biome_seeds$");
		this.t.path_texture = "drops/drop_seed_lemon";
		this.t.falling_speed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_seeds);
		this.t.drop_type_low = "lemon_low";
		this.t.drop_type_high = "lemon_high";
		this.t.sound_drop = "event:/SFX/DROPS/DropSeedLemon";
		this.clone("seeds_permafrost", "$biome_seeds$");
		this.t.path_texture = "drops/drop_seed_permafrost";
		this.t.falling_speed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_seeds);
		this.t.drop_type_low = "permafrost_low";
		this.t.drop_type_high = "permafrost_high";
		this.t.sound_drop = "event:/SFX/DROPS/DropSeedPermafrost";
		this.clone("seeds_candy", "$biome_seeds$");
		this.t.path_texture = "drops/drop_seed_candy";
		this.t.falling_speed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_seeds);
		this.t.drop_type_low = "candy_low";
		this.t.drop_type_high = "candy_high";
		this.t.sound_drop = "event:/SFX/DROPS/DropSeedCandy";
		this.clone("seeds_crystal", "$biome_seeds$");
		this.t.path_texture = "drops/drop_seed_crystal";
		this.t.falling_speed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_seeds);
		this.t.drop_type_low = "crystal_low";
		this.t.drop_type_high = "crystal_high";
		this.t.sound_drop = "event:/SFX/DROPS/DropSeedCrystal";
		this.clone("seeds_swamp", "$biome_seeds$");
		this.t.path_texture = "drops/drop_seed_swamp";
		this.t.falling_speed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_seeds);
		this.t.drop_type_low = "swamp_low";
		this.t.drop_type_high = "swamp_high";
		this.t.sound_drop = "event:/SFX/DROPS/DropSeedSwamp";
		this.clone("seeds_infernal", "$biome_seeds$");
		this.t.path_texture = "drops/drop_seed_infernal";
		this.t.falling_speed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_seeds);
		this.t.drop_type_low = "infernal_low";
		this.t.drop_type_high = "infernal_high";
		this.t.sound_drop = "event:/SFX/DROPS/DropSeedInfernal";
		this.clone("seeds_birch", "$biome_seeds$");
		this.t.path_texture = "drops/drop_seed_birch";
		this.t.falling_speed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_seeds);
		this.t.drop_type_low = "birch_low";
		this.t.drop_type_high = "birch_high";
		this.t.sound_drop = "event:/SFX/DROPS/DropSeedGrass";
		this.clone("seeds_maple", "$biome_seeds$");
		this.t.path_texture = "drops/drop_seed_maple";
		this.t.falling_speed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_seeds);
		this.t.drop_type_low = "maple_low";
		this.t.drop_type_high = "maple_high";
		this.t.sound_drop = "event:/SFX/DROPS/DropSeedGrass";
		this.clone("seeds_rocklands", "$biome_seeds$");
		this.t.path_texture = "drops/drop_seed_rocklands";
		this.t.falling_speed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_seeds);
		this.t.drop_type_low = "rocklands_low";
		this.t.drop_type_high = "rocklands_high";
		this.t.sound_drop = "event:/SFX/DROPS/DropSeedGrass";
		this.clone("seeds_garlic", "$biome_seeds$");
		this.t.path_texture = "drops/drop_seed_garlic";
		this.t.falling_speed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_seeds);
		this.t.drop_type_low = "garlic_low";
		this.t.drop_type_high = "garlic_high";
		this.t.sound_drop = "event:/SFX/DROPS/DropSeedGrass";
		this.clone("seeds_flower", "$biome_seeds$");
		this.t.path_texture = "drops/drop_seed_flower";
		this.t.falling_speed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_seeds);
		this.t.drop_type_low = "flower_low";
		this.t.drop_type_high = "flower_high";
		this.t.sound_drop = "event:/SFX/DROPS/DropSeedGrass";
		this.clone("seeds_celestial", "$biome_seeds$");
		this.t.path_texture = "drops/drop_seed_celestial";
		this.t.falling_speed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_seeds);
		this.t.drop_type_low = "celestial_low";
		this.t.drop_type_high = "celestial_high";
		this.t.sound_drop = "event:/SFX/DROPS/DropSeedGrass";
		this.clone("seeds_singularity", "$biome_seeds$");
		this.t.path_texture = "drops/drop_seed_singularity";
		this.t.falling_speed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_seeds);
		this.t.drop_type_low = "singularity_low";
		this.t.drop_type_high = "singularity_high";
		this.t.sound_drop = "event:/SFX/DROPS/DropSeedGrass";
		this.clone("seeds_clover", "$biome_seeds$");
		this.t.path_texture = "drops/drop_seed_clover";
		this.t.falling_speed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_seeds);
		this.t.drop_type_low = "clover_low";
		this.t.drop_type_high = "clover_high";
		this.t.sound_drop = "event:/SFX/DROPS/DropSeedGrass";
		this.clone("seeds_paradox", "$biome_seeds$");
		this.t.path_texture = "drops/drop_seed_paradox";
		this.t.falling_speed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_seeds);
		this.t.drop_type_low = "paradox_low";
		this.t.drop_type_high = "paradox_high";
		this.t.sound_drop = "event:/SFX/DROPS/DropSeedGrass";
		this.clone("fruit_bush", "$spawn_building$");
		this.t.path_texture = "drops/drop_seed";
		this.t.falling_speed = 3f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_fruit_bush);
		this.t.sound_drop = "event:/SFX/DROPS/DropBush";
		this.clone("fertilizer_plants", "$biome_seeds$");
		this.t.surprises_units = false;
		this.t.path_texture = "drops/drop_fertilizer";
		this.t.falling_speed = 5f;
		DropAsset t14 = this.t;
		t14.action_landed = (DropsAction)Delegate.Combine(t14.action_landed, new DropsAction(DropsLibrary.action_fertilizer_plants));
		DropAsset t15 = this.t;
		t15.action_landed = (DropsAction)Delegate.Combine(t15.action_landed, new DropsAction(DropsLibrary.tryToGrowWheat));
		DropAsset t16 = this.t;
		t16.action_landed = (DropsAction)Delegate.Combine(t16.action_landed, new DropsAction(DropsLibrary.flash));
		this.t.sound_drop = "event:/SFX/DROPS/DropFertilizerPlants";
		this.clone("fertilizer_trees", "$biome_seeds$");
		this.t.path_texture = "drops/drop_fertilizer";
		this.t.falling_speed = 5f;
		this.t.action_landed = new DropsAction(DropsLibrary.action_fertilizer_trees);
		DropAsset t17 = this.t;
		t17.action_landed = (DropsAction)Delegate.Combine(t17.action_landed, new DropsAction(DropsLibrary.flash));
		this.t.sound_drop = "event:/SFX/DROPS/DropFertilizerPlants";
		this.clone("$spawn_mineral$", "$spawn_building$");
		this.t.falling_speed = 6f;
		this.t.type = DropType.DropMineral;
		this.clone("stone", "$spawn_mineral$");
		this.t.path_texture = "drops/drop_stone";
		this.t.default_scale = 0.2f;
		this.t.building_asset = "mineral_stone";
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.sound_drop = "event:/SFX/DROPS/DropStone";
		this.clone("metals", "$spawn_mineral$");
		this.t.path_texture = "drops/drop_metal";
		this.t.default_scale = 0.2f;
		this.t.building_asset = "mineral_metals";
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.sound_drop = "event:/SFX/DROPS/DropMineral";
		this.clone("gold", "$spawn_mineral$");
		this.t.path_texture = "drops/drop_gold";
		this.t.default_scale = 0.2f;
		this.t.building_asset = "mineral_gold";
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.sound_drop = "event:/SFX/DROPS/DropGold";
		this.clone("silver", "$spawn_mineral$");
		this.t.path_texture = "drops/drop_stone";
		this.t.default_scale = 0.2f;
		this.t.building_asset = "mineral_silver";
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.sound_drop = "event:/SFX/DROPS/DropMineral";
		this.clone("mythril", "$spawn_mineral$");
		this.t.path_texture = "drops/drop_stone";
		this.t.default_scale = 0.2f;
		this.t.building_asset = "mineral_mythril";
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.sound_drop = "event:/SFX/DROPS/DropMineral";
		this.clone("adamantine", "$spawn_mineral$");
		this.t.path_texture = "drops/drop_stone";
		this.t.default_scale = 0.2f;
		this.t.building_asset = "mineral_adamantine";
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.sound_drop = "event:/SFX/DROPS/DropMineral";
		this.clone("$spawn_creep$", "$spawn_building$");
		this.t.type = DropType.DropCreep;
		this.clone("tumor", "$spawn_creep$");
		this.t.building_asset = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.sound_drop = "event:/SFX/DROPS/DropTumor";
		this.clone("biomass", "$spawn_creep$");
		this.t.building_asset = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.sound_drop = "event:/SFX/DROPS/DropBiomass";
		this.clone("cybercore", "$spawn_creep$");
		this.t.building_asset = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.sound_drop = "event:/SFX/DROPS/DropCybercore";
		this.clone("super_pumpkin", "$spawn_creep$");
		this.t.building_asset = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.sound_drop = "event:/SFX/DROPS/DropSuperPumpkin";
		this.clone("geyser", "$spawn_building$");
		this.t.building_asset = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.sound_drop = "event:/SFX/DROPS/DropGeyser";
		this.clone("geyser_acid", "$spawn_building$");
		this.t.building_asset = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.sound_drop = "event:/SFX/DROPS/DropGeyser";
		this.clone("volcano", "$spawn_building$");
		this.t.building_asset = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.sound_drop = "event:/SFX/DROPS/DropVolcano";
		this.clone("golden_brain", "$spawn_building$");
		this.t.building_asset = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.sound_drop = "event:/SFX/DROPS/DropGoldenBrain";
		this.clone("monolith", "$spawn_building$");
		this.t.building_asset = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		DropAsset t18 = this.t;
		t18.action_landed = (DropsAction)Delegate.Combine(t18.action_landed, new DropsAction(delegate(WorldTile _, string _)
		{
			AchievementLibrary.cant_be_too_much.checkBySignal(null);
		}));
		this.t.sound_drop = "event:/SFX/DROPS/DropMonolith";
		this.clone("corrupted_brain", "$spawn_building$");
		this.t.building_asset = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.sound_drop = "event:/SFX/DROPS/DropCorruptedBrain";
		this.clone("ice_tower", "$spawn_building$");
		this.t.building_asset = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.sound_drop = "event:/SFX/DROPS/DropIceTower";
		this.clone("angle_tower", "$spawn_building$");
		this.t.building_asset = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.sound_drop = "event:/SFX/DROPS/DropIceTower";
		this.clone("beehive", "$spawn_building$");
		this.t.building_asset = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.sound_drop = "event:/SFX/DROPS/DropBeehive";
		this.clone("flame_tower", "$spawn_building$");
		this.t.building_asset = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.sound_drop = "event:/SFX/DROPS/DropFlameTower";
		this.addWaypointDrops();
	}

	// Token: 0x06000288 RID: 648 RVA: 0x00019F4C File Offset: 0x0001814C
	public override void linkAssets()
	{
		foreach (DropAsset tAsset in this.list)
		{
			if (!string.IsNullOrEmpty(tAsset.drop_type_high))
			{
				tAsset.cached_drop_type_high = AssetManager.top_tiles.get(tAsset.drop_type_high);
			}
			if (!string.IsNullOrEmpty(tAsset.drop_type_low))
			{
				tAsset.cached_drop_type_low = AssetManager.top_tiles.get(tAsset.drop_type_low);
			}
		}
		base.linkAssets();
	}

	// Token: 0x06000289 RID: 649 RVA: 0x00019FE4 File Offset: 0x000181E4
	private void addWaypointDrops()
	{
		this.add(new DropAsset
		{
			id = "desire_alien_mold",
			path_texture = "drops/drop_alien_mold",
			animated = false,
			default_scale = 0.1f,
			material = "mat_world_object_lit",
			sound_drop = "event:/SFX/DROPS/DropBlessing",
			type = DropType.DropMagic
		});
		this.t.action_landed = new DropsAction(DropsLibrary.action_alien_mold);
		this.t.surprises_units = true;
		this.clone("desire_computer", "desire_alien_mold");
		this.t.path_texture = "drops/drop_computer";
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_computer);
		this.clone("desire_golden_egg", "desire_alien_mold");
		this.t.path_texture = "drops/drop_golden_egg";
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_golden_egg);
		this.clone("desire_harp", "desire_alien_mold");
		this.t.path_texture = "drops/drop_harp";
		this.t.action_landed = new DropsAction(DropsLibrary.action_drop_harp);
		this.clone("waypoint_alien_mold", "$spawn_building$");
		this.t.building_asset = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.sound_drop = "event:/SFX/DROPS/DropCorruptedBrain";
		this.clone("waypoint_computer", "$spawn_building$");
		this.t.building_asset = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.sound_drop = "event:/SFX/DROPS/DropCorruptedBrain";
		this.clone("waypoint_golden_egg", "$spawn_building$");
		this.t.building_asset = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.sound_drop = "event:/SFX/DROPS/DropCorruptedBrain";
		this.clone("waypoint_harp", "$spawn_building$");
		this.t.building_asset = this.t.id;
		this.t.action_landed = new DropsAction(DropsLibrary.action_spawn_building);
		this.t.sound_drop = "event:/SFX/DROPS/DropCorruptedBrain";
	}

	// Token: 0x0600028A RID: 650 RVA: 0x0001A248 File Offset: 0x00018448
	public static void action_drop_seeds(WorldTile pTile = null, string pDropID = null)
	{
		DropAsset tDropAsset = AssetManager.drops.get(pDropID);
		DropsLibrary.useDropSeedOn(pTile, tDropAsset.cached_drop_type_low, tDropAsset.cached_drop_type_high);
	}

	// Token: 0x0600028B RID: 651 RVA: 0x0001A274 File Offset: 0x00018474
	public static void useDropSeedOn(WorldTile pTile, TopTileType pTypeLow, TopTileType pHigh)
	{
		DropsLibrary.useSeedOn(pTile, pTypeLow, pHigh);
		for (int i = 0; i < pTile.neighbours.Length; i++)
		{
			DropsLibrary.useSeedOn(pTile.neighbours[i], pTypeLow, pHigh);
		}
	}

	// Token: 0x0600028C RID: 652 RVA: 0x0001A2AB File Offset: 0x000184AB
	public static void tryToGrowWheat(WorldTile pTile = null, string pDropID = null)
	{
		if (pTile.Type.farm_field && !pTile.hasBuilding())
		{
			World.world.buildings.addBuilding("wheat", pTile, false, false, BuildPlacingType.New);
		}
	}

	// Token: 0x0600028D RID: 653 RVA: 0x0001A2DC File Offset: 0x000184DC
	public static void useSeedOn(WorldTile pTile, TopTileType pTypeLow, TopTileType pHigh)
	{
		pTile.unfreeze(1);
		if (!pTile.Type.can_be_biome)
		{
			return;
		}
		if (pTile.isTileRank(TileRank.Low))
		{
			MapAction.growGreens(pTile, pTypeLow);
		}
		else if (pTile.isTileRank(TileRank.High))
		{
			MapAction.growGreens(pTile, pHigh);
		}
		BiomeAsset tBiome = pTile.getBiome();
		if (tBiome == null)
		{
			return;
		}
		pTile.doUnits(delegate(Actor tActor)
		{
			if (!tActor.hasSubspecies())
			{
				return;
			}
			if (tBiome.spawn_trait_subspecies_always == null)
			{
				return;
			}
			foreach (string tID in tBiome.spawn_trait_subspecies_always)
			{
				tActor.subspecies.addTrait(tID, false);
			}
		});
	}

	// Token: 0x0600028E RID: 654 RVA: 0x0001A350 File Offset: 0x00018550
	public static void action_rain(WorldTile pTile = null, string pDropID = null)
	{
		DropsLibrary.useRainOn(pTile);
		for (int i = 0; i < pTile.neighbours.Length; i++)
		{
			DropsLibrary.useRainOn(pTile.neighbours[i]);
		}
		for (int j = 0; j < pTile.neighbours.Length; j++)
		{
			WorldTile tNeighbour = pTile.neighbours[j];
			if (tNeighbour.isOnFire())
			{
				tNeighbour.stopFire();
			}
		}
	}

	// Token: 0x0600028F RID: 655 RVA: 0x0001A3B0 File Offset: 0x000185B0
	private static void useRainOn(WorldTile pTile)
	{
		pTile.stopFire();
		pTile.doUnits(delegate(Actor tActor)
		{
			tActor.finishStatusEffect("burning");
			tActor.finishAngryStatus();
			if (tActor.isDamagedByRain())
			{
				tActor.getHit((float)tActor.getWaterDamage(), true, AttackType.Water, null, true, false, true);
				return;
			}
			tActor.addStamina((int)((float)tActor.getMaxStamina() * 0.1f));
		});
		if (pTile.hasBuilding())
		{
			pTile.building.stopFire();
			if (pTile.building.asset.wheat)
			{
				pTile.building.component_wheat.grow();
			}
		}
		if (pTile.hasBuilding() && pTile.building.asset.damaged_by_rain)
		{
			pTile.building.getHit(20f, true, AttackType.Other, null, true, false, true);
		}
		pTile.removeBurn();
		if (pTile.Type.can_be_filled_with_ocean)
		{
			MapAction.setOcean(pTile);
		}
		if (pTile.Type.lava)
		{
			LavaHelper.putOut(pTile);
		}
		if (pTile.Type.explodable_by_ocean)
		{
			World.world.explosion_layer.explodeBomb(pTile, false);
		}
		BiomeAsset biome = pTile.getBiome();
		if (biome != null && biome.spread_by_drops_water)
		{
			WorldBehaviourActionBiomes.trySpreadBiomeAround(pTile, pTile, false, false, false);
		}
	}

	// Token: 0x06000290 RID: 656 RVA: 0x0001A4B8 File Offset: 0x000186B8
	public static void action_gamma_rain(WorldTile pTile = null, string pDropID = null)
	{
		List<string> tList = PlayerConfig.instance.data.trait_editor_gamma;
		DropsLibrary.useTraitRain(pTile, tList, PlayerConfig.instance.data.trait_editor_gamma_state);
	}

	// Token: 0x06000291 RID: 657 RVA: 0x0001A4EC File Offset: 0x000186EC
	public static void action_delta_rain(WorldTile pTile = null, string pDropID = null)
	{
		List<string> tList = PlayerConfig.instance.data.trait_editor_delta;
		DropsLibrary.useTraitRain(pTile, tList, PlayerConfig.instance.data.trait_editor_delta_state);
	}

	// Token: 0x06000292 RID: 658 RVA: 0x0001A520 File Offset: 0x00018720
	public static void action_omega_rain(WorldTile pTile = null, string pDropID = null)
	{
		List<string> tList = PlayerConfig.instance.data.trait_editor_omega;
		DropsLibrary.useTraitRain(pTile, tList, PlayerConfig.instance.data.trait_editor_omega_state);
	}

	// Token: 0x06000293 RID: 659 RVA: 0x0001A554 File Offset: 0x00018754
	private static void useTraitRain(WorldTile pTile, List<string> pList, RainState pRainState)
	{
		if (pList.Count == 0)
		{
			return;
		}
		using (ListPool<ActorTrait> tList = new ListPool<ActorTrait>(pList.Count))
		{
			foreach (string tTraitID in pList)
			{
				ActorTrait tTrait = AssetManager.traits.get(tTraitID);
				if (tTrait != null && tTrait.isAvailable() && (pRainState != RainState.Add || tTrait.can_be_given) && (pRainState != RainState.Remove || tTrait.can_be_removed))
				{
					tList.Add(tTrait);
				}
			}
			foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
			{
				if (tActor.asset.can_edit_traits)
				{
					if (pRainState == RainState.Remove)
					{
						tActor.removeTraits(tList);
					}
					else
					{
						foreach (ActorTrait ptr in tList)
						{
							ActorTrait tTrait2 = ptr;
							tActor.addTrait(tTrait2, true);
						}
					}
					tActor.addTrait("scar_of_divinity", false);
					tActor.startShake(0.3f, 0.1f, true, true);
					tActor.makeConfused(-1f, true);
				}
			}
		}
	}

	// Token: 0x06000294 RID: 660 RVA: 0x0001A704 File Offset: 0x00018904
	public static void action_equipment_rain(WorldTile pTile = null, string pDropID = null)
	{
		List<string> tItems = PlayerConfig.instance.data.equipment_editor;
		DropsLibrary.useEquipmentRain(pTile, tItems, PlayerConfig.instance.data.equipment_editor_state);
	}

	// Token: 0x06000295 RID: 661 RVA: 0x0001A738 File Offset: 0x00018938
	private static void useEquipmentRain(WorldTile pTile, List<string> pItems, RainState pRainState)
	{
		if (pItems.Count == 0)
		{
			return;
		}
		pItems.Shuffle<string>();
		using (ListPool<EquipmentAsset> tListItems = new ListPool<EquipmentAsset>(pItems.Count))
		{
			HashSet<EquipmentType> tTempSetTypes = UnsafeCollectionPool<HashSet<EquipmentType>, EquipmentType>.Get();
			for (int i = 0; i < pItems.Count; i++)
			{
				string tItemId = pItems[i];
				EquipmentAsset tItemAsset = AssetManager.items.get(tItemId);
				if (tItemAsset != null && (pRainState != RainState.Add || !tTempSetTypes.Contains(tItemAsset.equipment_type)) && tItemAsset.isAvailable() && (pRainState != RainState.Add || tItemAsset.can_be_given) && (pRainState != RainState.Remove || tItemAsset.can_be_removed))
				{
					tTempSetTypes.Add(tItemAsset.equipment_type);
					tListItems.Add(tItemAsset);
				}
			}
			UnsafeCollectionPool<HashSet<EquipmentType>, EquipmentType>.Release(tTempSetTypes);
			foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
			{
				if (tActor.canEditEquipment())
				{
					for (int j = 0; j < tListItems.Count; j++)
					{
						EquipmentAsset tItemAsset2 = tListItems[j];
						if (tActor.asset.canEditItem(tItemAsset2))
						{
							ActorEquipmentSlot tSlot = tActor.equipment.getSlot(tItemAsset2.equipment_type);
							Item tSlotItem = tSlot.getItem();
							if (pRainState == RainState.Remove)
							{
								if (tSlot.isEmpty())
								{
									goto IL_1E5;
								}
								if (tSlotItem.asset.id != tItemAsset2.id)
								{
									goto IL_1E5;
								}
							}
							else if (!tSlot.isEmpty() && (tSlotItem.asset.id == tItemAsset2.id || tSlotItem.isFavorite() || tSlotItem.isCursed()))
							{
								goto IL_1E5;
							}
							if (pRainState == RainState.Remove)
							{
								tSlotItem.data.favorite = false;
								tSlotItem.removeMod("eternal");
								tSlot.takeAwayItem();
							}
							else
							{
								Item tItem = World.world.items.generateItem(tItemAsset2, tActor.kingdom, World.world.map_stats.player_name, 1, tActor, 0, true);
								tItem.addMod("divine_rune");
								tActor.equipment.setItem(tItem, tActor);
							}
						}
						IL_1E5:;
					}
					tActor.startShake(0.3f, 0.1f, true, true);
					tActor.makeConfused(-1f, true);
				}
			}
		}
	}

	// Token: 0x06000296 RID: 662 RVA: 0x0001A9B8 File Offset: 0x00018BB8
	public static void action_acid(WorldTile pTile = null, string pDropID = null)
	{
		MapAction.checkAcidTerraform(pTile);
		if (Randy.randomChance(0.2f))
		{
			World.world.particles_smoke.spawn(pTile.posV3);
		}
		if (pTile.hasBuilding() && pTile.building.asset.affected_by_acid && pTile.building.isAlive())
		{
			pTile.building.getHit(20f, true, AttackType.Other, null, true, false, true);
		}
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			if (!Randy.randomChance(0.6f) && !tActor.hasTrait("acid_proof") && !tActor.hasTrait("acid_blood"))
			{
				tActor.getHit(20f, true, AttackType.Acid, null, true, false, true);
			}
		}
		World.world.conway_layer.checkKillRange(pTile.pos, 2);
		BiomeAsset biome = pTile.getBiome();
		if (biome != null && biome.spread_by_drops_acid)
		{
			WorldBehaviourActionBiomes.trySpreadBiomeAround(pTile, pTile, false, false, true);
		}
	}

	// Token: 0x06000297 RID: 663 RVA: 0x0001AAD4 File Offset: 0x00018CD4
	public static void action_fire(WorldTile pTile = null, string pDropID = null)
	{
		ActionLibrary.burnTile(null, null, pTile);
		ActionLibrary.startBurningObjects(null, null, pTile);
		BiomeAsset biome = pTile.getBiome();
		if (biome != null && biome.spread_by_drops_fire)
		{
			WorldBehaviourActionBiomes.trySpreadBiomeAround(pTile, pTile, false, false, true);
		}
	}

	// Token: 0x06000298 RID: 664 RVA: 0x0001AB05 File Offset: 0x00018D05
	public static void action_fireworks(WorldTile pTile = null, string pDropID = null)
	{
		MapAction.terraformTop(pTile, TopTileLibrary.fireworks, TerraformLibrary.remove, false);
	}

	// Token: 0x06000299 RID: 665 RVA: 0x0001AB18 File Offset: 0x00018D18
	public static void action_tnt(WorldTile pTile = null, string pDropID = null)
	{
		if (pTile.Type.lava || pTile.isOnFire())
		{
			MapAction.terraformTop(pTile, TopTileLibrary.tnt, TerraformLibrary.remove, false);
			World.world.explosion_layer.explodeBomb(pTile, false);
			return;
		}
		MapAction.terraformTop(pTile, TopTileLibrary.tnt, TerraformLibrary.remove, false);
	}

	// Token: 0x0600029A RID: 666 RVA: 0x0001AB70 File Offset: 0x00018D70
	public static void action_tnt_timed(WorldTile pTile = null, string pDropID = null)
	{
		if (pTile.Type.lava || pTile.isOnFire())
		{
			MapAction.terraformTop(pTile, TopTileLibrary.tnt_timed, TerraformLibrary.remove, false);
			World.world.explosion_layer.explodeBomb(pTile, false);
			return;
		}
		MapAction.terraformTop(pTile, TopTileLibrary.tnt_timed, TerraformLibrary.remove, false);
	}

	// Token: 0x0600029B RID: 667 RVA: 0x0001ABC6 File Offset: 0x00018DC6
	public static void action_czar_bomba(WorldTile pTile = null, string pDropID = null)
	{
		EffectsLibrary.spawn("fx_nuke_flash", pTile, "czar_bomba", null, 0f, -1f, -1f, null);
		World.world.startShake(0.3f, 0.01f, 2.5f, true, true);
	}

	// Token: 0x0600029C RID: 668 RVA: 0x0001AC05 File Offset: 0x00018E05
	public static void action_atomic_bomb(WorldTile pTile = null, string pDropID = null)
	{
		World.world.startShake(0.3f, 0.01f, 2f, true, true);
		EffectsLibrary.spawn("fx_nuke_flash", pTile, "atomic_bomb", null, 0f, -1f, -1f, null);
	}

	// Token: 0x0600029D RID: 669 RVA: 0x0001AC44 File Offset: 0x00018E44
	public static void action_antimatter_bomb(WorldTile pTile = null, string pDropID = null)
	{
		World.world.startShake(0.3f, 0.01f, 0.03f, false, true);
		EffectsLibrary.spawn("fx_antimatter_effect", pTile, null, null, 0f, -1f, -1f, null);
	}

	// Token: 0x0600029E RID: 670 RVA: 0x0001AC80 File Offset: 0x00018E80
	public static void action_napalm_bomb(WorldTile pTile = null, string pDropID = null)
	{
		World.world.startShake(0.3f, 0.01f, 0.5f, true, true);
		EffectsLibrary.spawn("fx_napalm_flash", pTile, null, null, 0f, -1f, -1f, null);
		EffectsLibrary.spawnAtTileRandomScale("fx_explosion_tiny", pTile, 0.15f, 0.3f);
	}

	// Token: 0x0600029F RID: 671 RVA: 0x0001ACDC File Offset: 0x00018EDC
	public static void action_crab_bomb_impact(WorldTile pTile = null, string pDropID = null)
	{
		MusicBox.playSound("event:/SFX/DESTRUCTION/CrabBombImpact", pTile, false, false);
		int tAmount = Randy.randomInt(1, 4);
		for (int i = 0; i < tAmount; i++)
		{
			World.world.drop_manager.spawnParabolicDrop(pTile, "crab_bomb_shrapnel", 1f, 15f, 40f, 4f, 16f, -1f);
		}
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x0001AD40 File Offset: 0x00018F40
	public static void action_crab_bomb_shrapnel(WorldTile pTile = null, string pDropID = null)
	{
		EffectsLibrary.spawnAt("fx_explosion_crab_bomb", pTile.posV, 0.25f);
		World.world.startShake(0.3f, 0.01f, 0.5f, true, true);
		MapAction.damageWorld(pTile, 2, AssetManager.terraform.get("crab_bomb"), null);
		if (Randy.randomChance(0.05f))
		{
			DropsLibrary.action_crab_bomb_impact(pTile, "crab_bomb_shrapnel");
		}
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x0001ADAC File Offset: 0x00018FAC
	public static void action_grenade(WorldTile pTile = null, string pDropID = null)
	{
		MapAction.damageWorld(pTile, 5, AssetManager.terraform.get("grenade"), null);
		EffectsLibrary.spawnAtTileRandomScale("fx_explosion_small", pTile, 0.1f, 0.15f);
	}

	// Token: 0x060002A2 RID: 674 RVA: 0x0001ADDC File Offset: 0x00018FDC
	public static void action_bomb(WorldTile pTile = null, string pDropID = null)
	{
		EffectsLibrary.spawnAtTileRandomScale("fx_explosion_middle", pTile, 0.45f, 0.6f);
		if (World.world.explosion_checker.checkNearby(pTile, 10))
		{
			return;
		}
		MapAction.damageWorld(pTile, 10, AssetManager.terraform.get("bomb"), null);
	}

	// Token: 0x060002A3 RID: 675 RVA: 0x0001AE2C File Offset: 0x0001902C
	public static void action_santa_bomb(WorldTile pTile = null, string pDropID = null)
	{
		MapAction.damageWorld(pTile, 10, AssetManager.terraform.get("santa_bomb"), null);
		EffectsLibrary.spawnAtTileRandomScale("fx_explosion_small", pTile, 0.45f, 0.6f);
	}

	// Token: 0x060002A4 RID: 676 RVA: 0x0001AE5C File Offset: 0x0001905C
	public static void action_water_bomb(WorldTile pTile = null, string pDropID = null)
	{
		if (pTile.Type.liquid || pTile.Type.lava || pTile.isOnFire())
		{
			MapAction.terraformTop(pTile, TopTileLibrary.water_bomb, TerraformLibrary.remove, false);
			World.world.explosion_layer.explodeBomb(pTile, false);
			return;
		}
		MapAction.terraformTop(pTile, TopTileLibrary.water_bomb, TerraformLibrary.remove, false);
	}

	// Token: 0x060002A5 RID: 677 RVA: 0x0001AEBF File Offset: 0x000190BF
	public static void action_lava(WorldTile pTile = null, string pDropID = null)
	{
		LavaHelper.addLava(pTile, "lava3");
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x0001AECC File Offset: 0x000190CC
	public static void action_rage(WorldTile pTile = null, string pDropID = null)
	{
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			if (Randy.randomChance(0.2f))
			{
				tActor.addStatusEffect("rage", 0f, true);
			}
		}
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x0001AF38 File Offset: 0x00019138
	public static void action_magic_rain(WorldTile pTile = null, string pDropID = null)
	{
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			if (Randy.randomChance(0.2f))
			{
				tActor.addStatusEffect("powerup", 0f, true);
			}
			if (Randy.randomChance(0.2f))
			{
				tActor.addStatusEffect("spell_boost", 0f, true);
			}
			if (Randy.randomChance(0.2f))
			{
				tActor.addStatusEffect("shield", 0f, true);
			}
			if (Randy.randomChance(0.2f))
			{
				tActor.addStatusEffect("caffeinated", 0f, true);
			}
			tActor.addMana((int)((float)tActor.getMaxMana() * 0.1f));
		}
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x0001B018 File Offset: 0x00019218
	public static void action_ash(WorldTile pTile = null, string pDropID = null)
	{
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			if (Randy.randomChance(0.3f))
			{
				tActor.addStatusEffect("cough", 0f, true);
			}
			if (Randy.randomChance(0.1f))
			{
				tActor.addStatusEffect("ash_fever", 0f, true);
			}
		}
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x0001B0A4 File Offset: 0x000192A4
	public static void action_life_seed(WorldTile pTile = null, string pDropID = null)
	{
		if (WorldLawLibrary.world_law_animals_spawn.isEnabled())
		{
			DropsLibrary.trySpawnUnit(pTile);
		}
		if (WorldLawLibrary.world_law_vegetation_random_seeds.isEnabled())
		{
			DropsLibrary.trySpawnVegetation(pTile);
		}
	}

	// Token: 0x060002AA RID: 682 RVA: 0x0001B0CC File Offset: 0x000192CC
	private void action_jazz_rain(WorldTile pTile, string pDropID)
	{
		Actor tRandomParent = null;
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 3f, true))
		{
			if (tActor.hasSubspecies() && tActor.isBreedingAge())
			{
				tRandomParent = tActor;
				break;
			}
		}
		if (tRandomParent == null)
		{
			return;
		}
		BabyMaker.makeBabyFromMiracle(tRandomParent, ActorSex.None, true);
	}

	// Token: 0x060002AB RID: 683 RVA: 0x0001B13C File Offset: 0x0001933C
	private static void trySpawnUnit(WorldTile pTile)
	{
		BiomeAsset tBiomeAsset = pTile.Type.biome_asset;
		if (tBiomeAsset == null)
		{
			return;
		}
		if (!tBiomeAsset.pot_spawn_units_auto)
		{
			return;
		}
		string tUnitID = tBiomeAsset.pot_units_spawn.GetRandom<string>();
		bool tSapient = false;
		if (WorldLawLibrary.world_law_drop_of_thoughts.isEnabled() && Randy.randomBool() && tBiomeAsset.pot_sapient_units_spawn != null)
		{
			foreach (string tID in tBiomeAsset.pot_sapient_units_spawn.LoopRandom<string>())
			{
				ActorAsset tAsset = AssetManager.actor_library.get(tID);
				if (tAsset.isAvailable())
				{
					GodPower tPower = tAsset.getGodPower();
					if (tPower == null || tPower.isAvailable())
					{
						tUnitID = tID;
						tSapient = true;
						break;
					}
				}
			}
		}
		ActorAsset tActorAsset = AssetManager.actor_library.get(tUnitID);
		if (tActorAsset == null)
		{
			return;
		}
		if (tActorAsset.units.Count > tActorAsset.max_random_amount)
		{
			return;
		}
		int tCountActors = 0;
		foreach (Actor actor in Finder.getUnitsFromChunk(pTile, 1, 0f, false))
		{
			if (tCountActors++ > 3)
			{
				return;
			}
		}
		Actor tActor = World.world.units.spawnNewUnit(tActorAsset.id, pTile, false, false, 6f, null, false, false);
		if (tSapient && tActor != null && tActor.subspecies.isJustCreated())
		{
			tActor.subspecies.makeSapient();
		}
	}

	// Token: 0x060002AC RID: 684 RVA: 0x0001B2BC File Offset: 0x000194BC
	private static void trySpawnVegetation(WorldTile pTile)
	{
		BiomeAsset tBiomeAsset = pTile.Type.biome_asset;
		if (tBiomeAsset == null)
		{
			return;
		}
		if (!tBiomeAsset.grow_vegetation_auto)
		{
			return;
		}
		ActionLibrary.growRandomVegetation(pTile, tBiomeAsset);
	}

	// Token: 0x060002AD RID: 685 RVA: 0x0001B2EC File Offset: 0x000194EC
	public static void action_snow(WorldTile pTile = null, string pDropID = null)
	{
		if (pTile.canBeFrozen())
		{
			pTile.freeze(1);
		}
		for (int i = 0; i < 10; i++)
		{
			WorldTile tRandTile = pTile.chunk.tiles.GetRandom<WorldTile>();
			if (tRandTile.canBeFrozen())
			{
				if (Toolbox.DistTile(pTile, tRandTile) < 11f)
				{
					break;
				}
				tRandTile.freeze(1);
			}
		}
		if (pTile.Type.lava)
		{
			return;
		}
		if (Randy.randomBool())
		{
			foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
			{
				ActionLibrary.addFrozenEffectOnTarget(tActor, tActor, null);
			}
		}
		DropsLibrary.checkColdOneBabies(pTile);
	}

	// Token: 0x060002AE RID: 686 RVA: 0x0001B3AC File Offset: 0x000195AC
	public static void checkColdOneBabies(WorldTile pTile)
	{
		if (!WorldLawLibrary.world_law_disasters_other.isEnabled())
		{
			return;
		}
		if (World.world_era.era_disaster_snow_turns_babies_into_ice_ones)
		{
			foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
			{
				if (tActor.canTurnIntoColdOne())
				{
					ActionLibrary.turnIntoIceOne(tActor, null);
				}
			}
		}
	}

	// Token: 0x060002AF RID: 687 RVA: 0x0001B424 File Offset: 0x00019624
	private static void action_cure(WorldTile pTile = null, string pDropID = null)
	{
		foreach (Actor actor in Finder.getUnitsFromChunk(pTile, 1, 4f, false))
		{
			actor.removeTrait("plague");
			actor.removeTrait("tumor_infection");
			actor.removeTrait("mush_spores");
			actor.removeTrait("infected");
			actor.finishStatusEffect("ash_fever");
			actor.finishStatusEffect("cursed");
			actor.startShake(0.3f, 0.1f, true, true);
			actor.startColorEffect(ActorColorEffect.White);
		}
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x0001B4CC File Offset: 0x000196CC
	private static void action_clone_rain(WorldTile pTile, string pDropID = null)
	{
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 1f, true))
		{
			WorldTile tTileTarget = null;
			foreach (WorldTile tTile in tActor.current_tile.neighboursAll.LoopRandom<WorldTile>())
			{
				if (!tTile.hasUnits())
				{
					tTileTarget = tTile;
					break;
				}
			}
			if (tTileTarget != null && World.world.units.cloneUnit(tActor, tTileTarget))
			{
				break;
			}
		}
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x0001B584 File Offset: 0x00019784
	private void action_sleep_rain(WorldTile pTile, string pDropID)
	{
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			if (tActor.makeSleep(60f) && !tActor.isLying())
			{
				tActor.applyRandomForce(1.5f, 2f);
			}
		}
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x0001B5F8 File Offset: 0x000197F8
	private void action_dispel_rain(WorldTile pTile, string pDropID)
	{
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			tActor.finishStatusEffect("powerup");
			tActor.finishStatusEffect("enchanted");
			tActor.finishStatusEffect("slowness");
			tActor.finishStatusEffect("shield");
			tActor.finishStatusEffect("invincible");
			tActor.finishStatusEffect("spell_boost");
			if (tActor.asset.die_from_dispel)
			{
				tActor.getHit((float)tActor.getMaxHealthPercent(0.5f), true, AttackType.Other, null, true, false, true);
			}
		}
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x0001B6AC File Offset: 0x000198AC
	public static void action_blood_rain(Drop pDrop, WorldTile pTile = null, string pDropID = null)
	{
		long tCasterId = pDrop.getCasterId();
		Actor tCaster = World.world.units.get(tCasterId);
		bool tIsCasterOk = !tCaster.isRekt();
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			if (!tIsCasterOk || tActor.id == tCasterId || !tCaster.kingdom.isEnemy(tActor.kingdom))
			{
				tActor.finishStatusEffect("burning");
				tActor.restoreHealth(tActor.getMaxHealthPercent(0.2f));
				tActor.startShake(0.3f, 0.1f, true, true);
				tActor.startColorEffect(ActorColorEffect.White);
			}
		}
	}

	// Token: 0x060002B4 RID: 692 RVA: 0x0001B778 File Offset: 0x00019978
	public static void action_plague(WorldTile pTile = null, string pDropID = null)
	{
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 4f, false))
		{
			if (tActor.hasTrait("plague"))
			{
				tActor.startShake(0.3f, 0.1f, true, true);
				tActor.startColorEffect(ActorColorEffect.White);
			}
			else
			{
				tActor.addTrait("plague", false);
			}
		}
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x0001B7FC File Offset: 0x000199FC
	public static void action_zombie_infection(WorldTile pTile = null, string pDropID = null)
	{
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			if (tActor.asset.can_turn_into_zombie && !tActor.hasTrait("zombie"))
			{
				tActor.addTrait("infected", false);
				tActor.startShake(0.3f, 0.1f, true, true);
				tActor.startColorEffect(ActorColorEffect.White);
			}
		}
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x0001B88C File Offset: 0x00019A8C
	public static void action_mush_spore(WorldTile pTile = null, string pDropID = null)
	{
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			if (tActor.asset.can_turn_into_mush && !tActor.hasTrait("mush_spores"))
			{
				tActor.addTrait("mush_spores", false);
				tActor.startShake(0.3f, 0.1f, true, true);
				tActor.startColorEffect(ActorColorEffect.White);
			}
		}
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x0001B91C File Offset: 0x00019B1C
	private static void action_curse(WorldTile pTile = null, string pDropID = null)
	{
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			if (tActor.addStatusEffect("cursed", 0f, true))
			{
				tActor.setStatsDirty();
				tActor.removeTrait("blessed");
				tActor.startShake(0.3f, 0.1f, true, true);
				tActor.startColorEffect(ActorColorEffect.White);
			}
		}
		BiomeAsset biome = pTile.getBiome();
		if (biome != null && biome.spread_by_drops_curse)
		{
			WorldBehaviourActionBiomes.trySpreadBiomeAround(pTile, pTile, false, false, true);
		}
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x0001B9C4 File Offset: 0x00019BC4
	private static void action_spell_silence(WorldTile pTile = null, string pDropID = null)
	{
		foreach (Actor actor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			actor.addStatusEffect("spell_silence", 0f, true);
		}
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x0001BA24 File Offset: 0x00019C24
	private static void action_shield(WorldTile pTile = null, string pDropID = null)
	{
		foreach (Actor actor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			actor.addStatusEffect("shield", 0f, true);
		}
	}

	// Token: 0x060002BA RID: 698 RVA: 0x0001BA84 File Offset: 0x00019C84
	private static void action_powerup(WorldTile pTile = null, string pDropID = null)
	{
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			tActor.addStatusEffect("powerup", 0f, true);
			if (tActor.isSameSpecies("mush_unit") || tActor.isSameSpecies("mush_animal"))
			{
				AchievementLibrary.super_mushroom.check(null);
			}
		}
		BiomeAsset biome = pTile.getBiome();
		if (biome != null && biome.spread_by_drops_powerup)
		{
			WorldBehaviourActionBiomes.trySpreadBiomeAround(pTile, pTile, false, false, true);
		}
	}

	// Token: 0x060002BB RID: 699 RVA: 0x0001BB28 File Offset: 0x00019D28
	private static void action_paint(WorldTile pTile = null, string pDropID = null)
	{
		TileZone tMainZone = pTile.zone;
		if (!tMainZone.hasCity())
		{
			return;
		}
		City tMainCity = tMainZone.city;
		World.world.city_zone_helper.city_growth.getZoneToClaim(null, tMainCity, true, DropsLibrary._paint_zones_hashset, 1);
		using (ListPool<TileZone> tZones = new ListPool<TileZone>())
		{
			foreach (TileZone tZone in DropsLibrary._paint_zones_hashset)
			{
				if (!tZone.hasCity())
				{
					TileZone[] tNeighbours = tZone.neighbours;
					for (int i = 0; i < tNeighbours.Length; i++)
					{
						if (tNeighbours[i].city == tMainCity)
						{
							tZones.Add(tZone);
						}
					}
				}
			}
			if (tZones.Count > 0)
			{
				TileZone tRandomZone = tZones.GetRandom<TileZone>();
				tMainCity.addZone(tRandomZone);
				tMainCity.setAbandonedZonesDirty();
			}
			DropsLibrary._paint_zones_hashset.Clear();
		}
	}

	// Token: 0x060002BC RID: 700 RVA: 0x0001BC2C File Offset: 0x00019E2C
	public static void action_dust_black(WorldTile pTile = null, string pDropID = null)
	{
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			if (tActor.asset.affected_by_dust)
			{
				tActor.makeConfused(-1f, true);
			}
		}
	}

	// Token: 0x060002BD RID: 701 RVA: 0x0001BC94 File Offset: 0x00019E94
	public static void action_dust_white(WorldTile pTile = null, string pDropID = null)
	{
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			if (tActor.asset.affected_by_dust)
			{
				tActor.forgetLanguage();
			}
		}
	}

	// Token: 0x060002BE RID: 702 RVA: 0x0001BCF4 File Offset: 0x00019EF4
	public static void action_dust_red(WorldTile pTile = null, string pDropID = null)
	{
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			if (tActor.asset.affected_by_dust)
			{
				tActor.makeConfused(-1f, true);
				if (tActor.hasFamily())
				{
					tActor.setFamily(null);
				}
				if (tActor.hasClan())
				{
					tActor.forgetClan();
				}
				if (tActor.hasLover())
				{
					Actor lover = tActor.lover;
					tActor.setLover(null);
					lover.setLover(null);
				}
			}
		}
	}

	// Token: 0x060002BF RID: 703 RVA: 0x0001BD94 File Offset: 0x00019F94
	public static void action_dust_blue(WorldTile pTile = null, string pDropID = null)
	{
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			if (tActor.asset.affected_by_dust)
			{
				tActor.forgetCulture();
			}
		}
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x0001BDF4 File Offset: 0x00019FF4
	public static void action_dust_gold(WorldTile pTile = null, string pDropID = null)
	{
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			if (tActor.asset.affected_by_dust)
			{
				tActor.forgetKingdomAndCity();
			}
		}
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x0001BE54 File Offset: 0x0001A054
	public static void action_dust_purple(WorldTile pTile = null, string pDropID = null)
	{
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			if (tActor.asset.affected_by_dust)
			{
				tActor.forgetReligion();
			}
		}
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x0001BEB4 File Offset: 0x0001A0B4
	public static void action_coffee(WorldTile pTile = null, string pDropID = null)
	{
		foreach (Actor actor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			actor.addStatusEffect("caffeinated", 0f, true);
		}
		BiomeAsset biome = pTile.getBiome();
		if (biome != null && biome.spread_by_drops_coffee)
		{
			WorldBehaviourActionBiomes.trySpreadBiomeAround(pTile, pTile, false, false, true);
		}
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x0001BF30 File Offset: 0x0001A130
	public static void action_blessing(WorldTile pTile = null, string pDropID = null)
	{
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			if (tActor.addTrait("blessed", false))
			{
				tActor.setStatsDirty();
				tActor.event_full_stats = true;
			}
			tActor.finishStatusEffect("cursed");
			tActor.startShake(0.3f, 0.1f, true, true);
			if (tActor.isSameSpecies("frog"))
			{
				AchievementLibrary.the_princess.check(null);
			}
			tActor.startColorEffect(ActorColorEffect.White);
		}
		BiomeAsset biome = pTile.getBiome();
		if (biome != null && biome.spread_by_drops_blessing)
		{
			WorldBehaviourActionBiomes.trySpreadBiomeAround(pTile, pTile, false, false, true);
		}
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x0001BFF4 File Offset: 0x0001A1F4
	public static void action_alien_mold(WorldTile pTile = null, string pDropID = null)
	{
		foreach (Actor actor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			actor.addTrait("desire_alien_mold", false);
		}
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x0001C04C File Offset: 0x0001A24C
	public static void action_drop_computer(WorldTile pTile = null, string pDropID = null)
	{
		foreach (Actor actor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			actor.addTrait("desire_computer", false);
		}
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x0001C0A4 File Offset: 0x0001A2A4
	public static void action_drop_golden_egg(WorldTile pTile = null, string pDropID = null)
	{
		foreach (Actor actor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			actor.addTrait("desire_golden_egg", false);
		}
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x0001C0FC File Offset: 0x0001A2FC
	public static void action_drop_harp(WorldTile pTile = null, string pDropID = null)
	{
		foreach (Actor actor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			actor.addTrait("desire_harp", false);
		}
	}

	// Token: 0x060002C8 RID: 712 RVA: 0x0001C154 File Offset: 0x0001A354
	public static void action_madness(WorldTile pTile = null, string pDropID = null)
	{
		foreach (Actor actor in Finder.getUnitsFromChunk(pTile, 1, 3f, false))
		{
			actor.addTrait("madness", false);
		}
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x0001C1AC File Offset: 0x0001A3AC
	public static void action_inspiration(WorldTile pTile, string pDropID = null)
	{
		if (!pTile.zone.hasCity())
		{
			return;
		}
		if (World.world.cities.isLocked())
		{
			return;
		}
		City tCity = pTile.zone_city;
		if (tCity.isNeutral())
		{
			return;
		}
		if (tCity.kingdom.countCities() == 1)
		{
			return;
		}
		if (tCity.isCapitalCity())
		{
			return;
		}
		if (!tCity.hasLeader())
		{
			return;
		}
		tCity.leader.addStatusEffect("voices_in_my_head", 0f, true);
		tCity.useInspire(tCity.leader);
	}

	// Token: 0x060002CA RID: 714 RVA: 0x0001C230 File Offset: 0x0001A430
	public static void action_discord(WorldTile pTile, string pDropID = null)
	{
		if (!pTile.zone.hasCity())
		{
			return;
		}
		City tCity = pTile.zone_city;
		if (tCity == null)
		{
			return;
		}
		if (tCity.isNeutral())
		{
			return;
		}
		Alliance tAlliance = tCity.kingdom.getAlliance();
		if (tAlliance == null)
		{
			return;
		}
		World.world.alliances.useDiscordPower(tAlliance, tCity);
	}

	// Token: 0x060002CB RID: 715 RVA: 0x0001C280 File Offset: 0x0001A480
	public static void action_spite(WorldTile pTile, string pDropID = null)
	{
		if (!pTile.zone.hasCity())
		{
			return;
		}
		Kingdom tKingdom = pTile.zone.city.kingdom;
		if (tKingdom.isNeutral())
		{
			return;
		}
		World.world.diplomacy.eventSpite(tKingdom);
	}

	// Token: 0x060002CC RID: 716 RVA: 0x0001C2C8 File Offset: 0x0001A4C8
	public static void action_friendship(WorldTile pTile, string pDropID = null)
	{
		if (!pTile.zone.hasCity())
		{
			return;
		}
		Kingdom tKingdom = pTile.zone.city.kingdom;
		if (tKingdom.isNeutral())
		{
			return;
		}
		World.world.diplomacy.eventFriendship(tKingdom);
	}

	// Token: 0x060002CD RID: 717 RVA: 0x0001C310 File Offset: 0x0001A510
	public static void action_spawn_building(WorldTile pTile = null, string pDropID = null)
	{
		string tBuildingAssetID = AssetManager.drops.get(pDropID).getRandomBuildingAsset();
		BuildingAsset tBuildingAsset = AssetManager.buildings.get(tBuildingAssetID);
		Building tNewBuilding = World.world.buildings.addBuilding(tBuildingAssetID, pTile, true, false, BuildPlacingType.New);
		if (tNewBuilding == null)
		{
			EffectsLibrary.spawnAtTile("fx_bad_place", pTile, 0.25f);
			return;
		}
		tBuildingAsset.checkLimits(tNewBuilding);
	}

	// Token: 0x060002CE RID: 718 RVA: 0x0001C36B File Offset: 0x0001A56B
	public static void flash(WorldTile pTile, string pDropID)
	{
		World.world.flash_effects.flashPixel(pTile, 20, ColorType.White);
	}

	// Token: 0x060002CF RID: 719 RVA: 0x0001C380 File Offset: 0x0001A580
	public static void action_fertilizer_plants(WorldTile pTile = null, string pDropID = null)
	{
		BuildingActions.tryGrowVegetationRandom(pTile, VegetationType.Plants, false, false, false);
		if (pTile.Type.biome_asset == null)
		{
			return;
		}
		if (pTile.Type.biome_asset.grow_type_selector_plants == null)
		{
			EffectsLibrary.spawnAtTile("fx_bad_place", pTile, 0.25f);
		}
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x0001C3C0 File Offset: 0x0001A5C0
	public static void action_fertilizer_trees(WorldTile pTile = null, string pDropID = null)
	{
		BiomeAsset tBiomeAsset = pTile.Type.biome_asset;
		BuildingActions.tryGrowVegetationRandom(pTile, VegetationType.Trees, false, false, false);
		if (tBiomeAsset == null)
		{
			return;
		}
		if (tBiomeAsset.grow_type_selector_trees == null)
		{
			EffectsLibrary.spawnAtTile("fx_bad_place", pTile, 0.25f);
		}
	}

	// Token: 0x060002D1 RID: 721 RVA: 0x0001C400 File Offset: 0x0001A600
	public static void action_fruit_bush(WorldTile pTile = null, string pDropID = null)
	{
		BuildingAsset tBuildingAsset = AssetManager.buildings.get("fruit_bush");
		BuildingActions.tryGrowVegetation(pTile, tBuildingAsset.id, true, false);
		if (!tBuildingAsset.isOverlaysBiomeTags(pTile.Type))
		{
			EffectsLibrary.spawnAtTile("fx_bad_place", pTile, 0.25f);
		}
	}

	// Token: 0x060002D2 RID: 722 RVA: 0x0001C44B File Offset: 0x0001A64B
	public static void action_landmine(WorldTile pTile = null, string pDropID = null)
	{
		if (pTile.Type.lava)
		{
			World.world.explosion_layer.explodeBomb(pTile, false);
			return;
		}
		MapAction.terraformTop(pTile, TopTileLibrary.landmine, TerraformLibrary.remove, false);
	}

	// Token: 0x060002D3 RID: 723 RVA: 0x0001C480 File Offset: 0x0001A680
	public static void action_living_house(WorldTile pTile = null, string pDropID = null)
	{
		TileZone tZone = pTile.zone;
		if (!tZone.hasAnyBuildings())
		{
			return;
		}
		using (ListPool<Building> tTempPool = new ListPool<Building>())
		{
			if (tZone.hasAnyBuildingsInSet(BuildingList.Civs))
			{
				tTempPool.AddRange(tZone.getHashset(BuildingList.Civs));
			}
			if (tZone.hasAnyBuildingsInSet(BuildingList.Ruins))
			{
				tTempPool.AddRange(tZone.getHashset(BuildingList.Ruins));
			}
			if (tZone.hasAnyBuildingsInSet(BuildingList.Abandoned))
			{
				tTempPool.AddRange(tZone.getHashset(BuildingList.Abandoned));
			}
			for (int i = 0; i < tTempPool.Count; i++)
			{
				ActionLibrary.tryToMakeBuildingAlive(tTempPool[i]);
			}
		}
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x0001C520 File Offset: 0x0001A720
	public static void action_living_plants(WorldTile pTile = null, string pDropID = null)
	{
		TileZone tZone = pTile.zone;
		if (!tZone.hasAnyBuildings())
		{
			return;
		}
		using (ListPool<Building> tTempPool = new ListPool<Building>())
		{
			if (tZone.hasAnyBuildingsInSet(BuildingList.Food))
			{
				tTempPool.AddRange(tZone.getHashset(BuildingList.Food));
			}
			if (tZone.hasAnyBuildingsInSet(BuildingList.Trees))
			{
				tTempPool.AddRange(tZone.getHashset(BuildingList.Trees));
			}
			if (tZone.hasAnyBuildingsInSet(BuildingList.Wheat))
			{
				tTempPool.AddRange(tZone.getHashset(BuildingList.Wheat));
			}
			for (int i = 0; i < tTempPool.Count; i++)
			{
				ActionLibrary.tryToMakeFloraAlive(tTempPool[i], true);
			}
		}
	}

	// Token: 0x0400025C RID: 604
	private const string TEMPLATE_BIOME_SEEDS = "$biome_seeds$";

	// Token: 0x0400025D RID: 605
	private const string TEMPLATE_SPAWN_BUILDING = "$spawn_building$";

	// Token: 0x0400025E RID: 606
	private const string TEMPLATE_SPAWN_MINERAL = "$spawn_mineral$";

	// Token: 0x0400025F RID: 607
	private const string TEMPLATE_SPAWN_CREEP = "$spawn_creep$";

	// Token: 0x04000260 RID: 608
	private static HashSet<TileZone> _paint_zones_hashset = new HashSet<TileZone>();
}
