using System;
using System.Collections.Generic;
using System.ComponentModel;

// Token: 0x020001E7 RID: 487
[Serializable]
public class TerraformOptions : Asset
{
	// Token: 0x04000E75 RID: 3701
	public bool remove_trees_fully;

	// Token: 0x04000E76 RID: 3702
	public bool destroy_buildings;

	// Token: 0x04000E77 RID: 3703
	public bool make_ruins;

	// Token: 0x04000E78 RID: 3704
	public bool remove_burned;

	// Token: 0x04000E79 RID: 3705
	public bool remove_tornado;

	// Token: 0x04000E7A RID: 3706
	public bool add_burned;

	// Token: 0x04000E7B RID: 3707
	public int add_heat;

	// Token: 0x04000E7C RID: 3708
	public bool flash;

	// Token: 0x04000E7D RID: 3709
	public bool remove_borders;

	// Token: 0x04000E7E RID: 3710
	public bool remove_top_tile;

	// Token: 0x04000E7F RID: 3711
	public bool remove_roads;

	// Token: 0x04000E80 RID: 3712
	public bool remove_frozen;

	// Token: 0x04000E81 RID: 3713
	public bool remove_fire;

	// Token: 0x04000E82 RID: 3714
	public bool remove_water;

	// Token: 0x04000E83 RID: 3715
	public bool remove_ruins;

	// Token: 0x04000E84 RID: 3716
	public bool lightning_effect;

	// Token: 0x04000E85 RID: 3717
	public bool remove_lava;

	// Token: 0x04000E86 RID: 3718
	public bool damage_buildings;

	// Token: 0x04000E87 RID: 3719
	public int damage;

	// Token: 0x04000E88 RID: 3720
	public WorldAction bomb_action;

	// Token: 0x04000E89 RID: 3721
	public bool set_fire;

	// Token: 0x04000E8A RID: 3722
	public bool transform_to_wasteland;

	// Token: 0x04000E8B RID: 3723
	public bool apply_force;

	// Token: 0x04000E8C RID: 3724
	[DefaultValue(1.5f)]
	public float force_power = 1.5f;

	// Token: 0x04000E8D RID: 3725
	public bool explode_tile;

	// Token: 0x04000E8E RID: 3726
	public bool explosion_pixel_effect = true;

	// Token: 0x04000E8F RID: 3727
	public bool explode_and_set_random_fire;

	// Token: 0x04000E90 RID: 3728
	public int explode_strength;

	// Token: 0x04000E91 RID: 3729
	public bool applies_to_high_flyers;

	// Token: 0x04000E92 RID: 3730
	public bool shake;

	// Token: 0x04000E93 RID: 3731
	[DefaultValue(0.3f)]
	public float shake_duration = 0.3f;

	// Token: 0x04000E94 RID: 3732
	[DefaultValue(0.01f)]
	public float shake_interval = 0.01f;

	// Token: 0x04000E95 RID: 3733
	[DefaultValue(2f)]
	public float shake_intensity = 2f;

	// Token: 0x04000E96 RID: 3734
	public string add_trait;

	// Token: 0x04000E97 RID: 3735
	[DefaultValue(AttackType.Other)]
	public AttackType attack_type = AttackType.Other;

	// Token: 0x04000E98 RID: 3736
	public string[] ignore_kingdoms;

	// Token: 0x04000E99 RID: 3737
	public List<string> destroy_only;

	// Token: 0x04000E9A RID: 3738
	public List<string> ignore_buildings;
}
