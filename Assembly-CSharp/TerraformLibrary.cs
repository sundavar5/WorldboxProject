using System;

// Token: 0x020001E8 RID: 488
public class TerraformLibrary : AssetLibrary<TerraformOptions>
{
	// Token: 0x06000E15 RID: 3605 RVA: 0x000BFB50 File Offset: 0x000BDD50
	public override void init()
	{
		base.init();
		TerraformLibrary.tumor_attack = this.add(new TerraformOptions
		{
			id = "tumor_attack",
			flash = true,
			destroy_buildings = true,
			remove_frozen = true,
			ignore_kingdoms = AssetLibrary<TerraformOptions>.a<string>(new string[]
			{
				"tumor"
			})
		});
		TerraformLibrary.destroy = this.add(new TerraformOptions
		{
			id = "destroy",
			flash = true,
			destroy_buildings = true
		});
		TerraformLibrary.destroy_life = this.add(new TerraformOptions
		{
			id = "destroy_life",
			destroy_only = AssetLibrary<TerraformOptions>.l<string>(new string[]
			{
				"tumor",
				"super_pumpkin",
				"biomass"
			}),
			destroy_buildings = true,
			flash = true,
			remove_borders = true,
			remove_tornado = true
		});
		TerraformLibrary.earthquake = this.add(new TerraformOptions
		{
			id = "earthquake",
			remove_lava = true,
			make_ruins = true,
			remove_top_tile = true,
			remove_roads = true,
			remove_frozen = true,
			remove_water = true
		});
		TerraformLibrary.earthquake_disaster = this.add(new TerraformOptions
		{
			id = "earthquake_disaster",
			remove_lava = true,
			make_ruins = true,
			remove_top_tile = true,
			remove_roads = true,
			remove_frozen = true,
			remove_water = true
		});
		TerraformLibrary.road = this.add(new TerraformOptions
		{
			id = "road",
			flash = true,
			destroy_buildings = true,
			remove_frozen = true,
			destroy_only = AssetLibrary<TerraformOptions>.l<string>(new string[]
			{
				"nature"
			})
		});
		TerraformLibrary.flash = this.add(new TerraformOptions
		{
			id = "flash",
			flash = true
		});
		TerraformLibrary.grey_goo = this.add(new TerraformOptions
		{
			id = "grey_goo",
			destroy_buildings = true,
			flash = true,
			remove_borders = true,
			remove_frozen = true
		});
		TerraformLibrary.remove = this.add(new TerraformOptions
		{
			id = "remove",
			flash = true,
			destroy_buildings = true,
			remove_frozen = true
		});
		TerraformLibrary.draw = this.add(new TerraformOptions
		{
			id = "draw",
			flash = true,
			remove_frozen = true
		});
		TerraformLibrary.water_fill = this.add(new TerraformOptions
		{
			id = "water_fill",
			flash = true,
			destroy_buildings = true,
			remove_frozen = true,
			remove_fire = true,
			ignore_buildings = AssetLibrary<TerraformOptions>.l<string>(new string[]
			{
				"geyser",
				"volcano",
				"geyser_acid"
			})
		});
		TerraformLibrary.destroy_no_flash = this.add(new TerraformOptions
		{
			id = "destroy_no_flash",
			remove_burned = true,
			destroy_buildings = true
		});
		TerraformLibrary.nothing = this.add(new TerraformOptions
		{
			id = "nothing"
		});
		TerraformLibrary.remove_lava = this.add(new TerraformOptions
		{
			id = "remove_lava",
			remove_lava = true
		});
		TerraformLibrary.lava_damage = this.add(new TerraformOptions
		{
			id = "lava_damage",
			flash = true,
			damage_buildings = true,
			damage = 100,
			remove_frozen = true
		});
		this.add(new TerraformOptions
		{
			id = "grenade",
			flash = true,
			damage_buildings = true,
			damage = 50,
			apply_force = true,
			explode_and_set_random_fire = true,
			explode_tile = true,
			explosion_pixel_effect = true,
			explode_strength = 1,
			shake = true,
			remove_tornado = true,
			remove_frozen = true,
			attack_type = AttackType.Explosion
		});
		this.add(new TerraformOptions
		{
			id = "lightning_power",
			flash = true,
			lightning_effect = true,
			add_burned = true,
			add_heat = 9,
			damage = 77,
			set_fire = true,
			apply_force = true,
			force_power = 2.5f,
			shake = true,
			shake_intensity = 2f,
			remove_frozen = true
		});
		this.add(new TerraformOptions
		{
			id = "lightning_normal",
			flash = true,
			lightning_effect = true,
			add_burned = true,
			damage = 47,
			set_fire = true,
			apply_force = true,
			force_power = 2.5f,
			remove_frozen = true
		});
		this.add(new TerraformOptions
		{
			id = "madness_ball",
			apply_force = true,
			force_power = 1.6f,
			add_trait = "madness",
			flash = true,
			remove_frozen = true
		});
		this.add(new TerraformOptions
		{
			id = "ufo_attack",
			flash = true,
			add_burned = true,
			damage = 50,
			set_fire = true,
			apply_force = true,
			shake = true,
			damage_buildings = true,
			remove_frozen = true
		});
		this.t.ignore_kingdoms = AssetLibrary<TerraformOptions>.a<string>(new string[]
		{
			"aliens"
		});
		this.t.shake_intensity = 0.1f;
		this.add(new TerraformOptions
		{
			id = "dragon_attack",
			flash = true,
			add_burned = true,
			force_power = 0.5f,
			damage = 25,
			set_fire = true,
			apply_force = true,
			shake = true,
			damage_buildings = true,
			remove_frozen = true,
			attack_type = AttackType.Fire
		});
		this.t.ignore_kingdoms = AssetLibrary<TerraformOptions>.a<string>(new string[]
		{
			"dragons"
		});
		this.t.shake_intensity = 0.1f;
		this.add(new TerraformOptions
		{
			id = "zombie_dragon_attack",
			flash = true,
			force_power = 0.5f,
			damage = 25,
			set_fire = false,
			apply_force = true,
			shake = true,
			damage_buildings = true,
			remove_frozen = true,
			attack_type = AttackType.Acid
		});
		this.t.ignore_kingdoms = AssetLibrary<TerraformOptions>.a<string>(new string[]
		{
			"undead"
		});
		this.t.shake_intensity = 0.1f;
		this.clone("ufo_explosion", "grenade");
		this.t.ignore_kingdoms = AssetLibrary<TerraformOptions>.a<string>(new string[]
		{
			"aliens"
		});
		TerraformLibrary.bomb = this.clone("bomb", "grenade");
		this.t.damage = 100;
		this.t.explode_strength = 3;
		this.clone("meteorite", "grenade");
		this.t.damage = 1000;
		this.t.explode_strength = 1;
		this.clone("meteorite_disaster", "grenade");
		this.t.damage = 500;
		this.t.explode_strength = 1;
		this.clone("crab_bomb", "grenade");
		this.t.ignore_kingdoms = AssetLibrary<TerraformOptions>.a<string>(new string[]
		{
			"crabzilla",
			"crab"
		});
		this.t.damage_buildings = true;
		this.t.damage = 30;
		this.t.set_fire = true;
		this.t.shake = false;
		TerraformLibrary.atomic_bomb = this.clone("atomic_bomb", "bomb");
		this.t.damage = 10000;
		this.t.explode_strength = 1;
		this.t.transform_to_wasteland = true;
		this.t.applies_to_high_flyers = true;
		this.t.shake = false;
		this.t.bomb_action = new WorldAction(NukeFlash.atomic_bomb_action);
		TerraformLibrary.crabzilla_bomb = this.clone("crabzilla_bomb", "bomb");
		this.t.damage = 10000;
		this.t.explode_strength = 1;
		this.t.applies_to_high_flyers = true;
		this.t.bomb_action = new WorldAction(NukeFlash.crabzilla_bomb_action);
		TerraformLibrary.czar_bomba = this.clone("czar_bomba", "bomb");
		this.t.damage = 10000;
		this.t.transform_to_wasteland = true;
		this.t.explode_strength = 4;
		this.t.applies_to_high_flyers = true;
		this.t.shake = false;
		this.t.bomb_action = new WorldAction(NukeFlash.czar_bomba_action);
		this.add(new TerraformOptions
		{
			id = "crab_step",
			flash = true,
			damage_buildings = true,
			damage = 10,
			apply_force = true,
			explode_tile = true,
			explosion_pixel_effect = false,
			explode_strength = 1,
			shake = true,
			shake_intensity = 0.1f,
			remove_frozen = true
		});
		this.clone("crab_laser", "bomb");
		this.t.damage = 200;
		this.t.explode_strength = 1;
		this.t.shake_intensity = 1f;
		this.t.attack_type = AttackType.Fire;
		this.clone("santa_bomb", "bomb");
		this.clone("demon_fireball", "grenade");
		this.t.ignore_kingdoms = AssetLibrary<TerraformOptions>.a<string>(new string[]
		{
			"demon"
		});
		this.t.shake = false;
		this.t.damage_buildings = true;
		this.t.damage = 30;
		this.t.set_fire = true;
		this.clone("plasma_ball", "grenade");
		this.t.shake = false;
		this.t.damage_buildings = true;
		this.t.damage = 30;
		this.clone("torch", "grenade");
		this.t.ignore_kingdoms = AssetLibrary<TerraformOptions>.a<string>(new string[]
		{
			"demon"
		});
		this.t.shake = false;
		this.t.damage_buildings = true;
		this.t.damage = 25;
		this.t.set_fire = true;
		this.t.attack_type = AttackType.Fire;
		this.clone("assimilator_bullet", "flash");
		this.t.remove_frozen = true;
		this.t.ignore_kingdoms = AssetLibrary<TerraformOptions>.a<string>(new string[]
		{
			"assimilators"
		});
		this.t.remove_frozen = true;
		this.clone("cannonball", "grenade");
		this.t.shake = false;
		this.t.damage_buildings = true;
		this.t.damage = 0;
		this.t.apply_force = true;
		this.t.force_power = 2f;
		this.clone("acid", "grenade");
		this.t.shake = false;
		this.t.damage_buildings = true;
		this.t.damage = 0;
		this.t.explode_strength = 2;
		this.t.apply_force = true;
		this.t.force_power = 0.5f;
		this.t.set_fire = false;
		this.t.flash = true;
		this.t.remove_frozen = true;
		this.t.explode_and_set_random_fire = false;
		this.t.attack_type = AttackType.Acid;
		this.clone("acid_ball", "grenade");
		this.t.shake = false;
		this.t.damage_buildings = true;
		this.t.damage = 0;
		this.t.apply_force = true;
		this.t.force_power = 0.5f;
		this.t.set_fire = false;
		this.t.flash = true;
		this.t.remove_frozen = true;
		this.t.explode_and_set_random_fire = false;
		this.t.attack_type = AttackType.Acid;
	}

	// Token: 0x04000E9B RID: 3739
	public static TerraformOptions nothing;

	// Token: 0x04000E9C RID: 3740
	public static TerraformOptions remove_lava;

	// Token: 0x04000E9D RID: 3741
	public static TerraformOptions lava_damage;

	// Token: 0x04000E9E RID: 3742
	public static TerraformOptions destroy_no_flash;

	// Token: 0x04000E9F RID: 3743
	public static TerraformOptions remove;

	// Token: 0x04000EA0 RID: 3744
	public static TerraformOptions draw;

	// Token: 0x04000EA1 RID: 3745
	public static TerraformOptions water_fill;

	// Token: 0x04000EA2 RID: 3746
	public static TerraformOptions grey_goo;

	// Token: 0x04000EA3 RID: 3747
	public static TerraformOptions flash;

	// Token: 0x04000EA4 RID: 3748
	public static TerraformOptions road;

	// Token: 0x04000EA5 RID: 3749
	public static TerraformOptions tumor_attack;

	// Token: 0x04000EA6 RID: 3750
	public static TerraformOptions destroy;

	// Token: 0x04000EA7 RID: 3751
	public static TerraformOptions destroy_life;

	// Token: 0x04000EA8 RID: 3752
	public static TerraformOptions earthquake;

	// Token: 0x04000EA9 RID: 3753
	public static TerraformOptions earthquake_disaster;

	// Token: 0x04000EAA RID: 3754
	public static TerraformOptions atomic_bomb;

	// Token: 0x04000EAB RID: 3755
	public static TerraformOptions crabzilla_bomb;

	// Token: 0x04000EAC RID: 3756
	public static TerraformOptions bomb;

	// Token: 0x04000EAD RID: 3757
	public static TerraformOptions czar_bomba;
}
