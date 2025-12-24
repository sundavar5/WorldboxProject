using System;
using System.ComponentModel;
using UnityEngine;

// Token: 0x02000073 RID: 115
[Serializable]
public class ProjectileAsset : Asset
{
	// Token: 0x0400035B RID: 859
	public string texture;

	// Token: 0x0400035C RID: 860
	public bool animated = true;

	// Token: 0x0400035D RID: 861
	public float animation_speed = 6f;

	// Token: 0x0400035E RID: 862
	public float speed = 20f;

	// Token: 0x0400035F RID: 863
	public float speed_random;

	// Token: 0x04000360 RID: 864
	public string terraform_option = string.Empty;

	// Token: 0x04000361 RID: 865
	public int terraform_range;

	// Token: 0x04000362 RID: 866
	public string end_effect;

	// Token: 0x04000363 RID: 867
	public float end_effect_scale = 0.25f;

	// Token: 0x04000364 RID: 868
	public string sound_launch = string.Empty;

	// Token: 0x04000365 RID: 869
	public string sound_impact = string.Empty;

	// Token: 0x04000366 RID: 870
	public bool look_at_target;

	// Token: 0x04000367 RID: 871
	public bool trail_effect_enabled;

	// Token: 0x04000368 RID: 872
	public string trail_effect_id = "fx_fire_smoke";

	// Token: 0x04000369 RID: 873
	public float trail_effect_scale = 0.25f;

	// Token: 0x0400036A RID: 874
	public float trail_effect_timer = 0.2f;

	// Token: 0x0400036B RID: 875
	public bool hit_freeze;

	// Token: 0x0400036C RID: 876
	public bool hit_shake;

	// Token: 0x0400036D RID: 877
	[DefaultValue(0.3f)]
	public float shake_duration = 0.3f;

	// Token: 0x0400036E RID: 878
	[DefaultValue(0.01f)]
	public float shake_interval = 0.01f;

	// Token: 0x0400036F RID: 879
	[DefaultValue(2f)]
	public float shake_intensity = 2f;

	// Token: 0x04000370 RID: 880
	[DefaultValue(false)]
	public bool shake_x;

	// Token: 0x04000371 RID: 881
	[DefaultValue(true)]
	public bool shake_y = true;

	// Token: 0x04000372 RID: 882
	[NonSerialized]
	public Sprite[] frames;

	// Token: 0x04000373 RID: 883
	[DefaultValue(0.1f)]
	public float scale_start = 0.1f;

	// Token: 0x04000374 RID: 884
	[DefaultValue(0.1f)]
	public float scale_target = 0.1f;

	// Token: 0x04000375 RID: 885
	public string texture_shadow = string.Empty;

	// Token: 0x04000376 RID: 886
	public AttackAction world_actions;

	// Token: 0x04000377 RID: 887
	public AttackAction impact_actions;

	// Token: 0x04000378 RID: 888
	public bool draw_light_area;

	// Token: 0x04000379 RID: 889
	public float draw_light_area_offset_x;

	// Token: 0x0400037A RID: 890
	public float draw_light_area_offset_y;

	// Token: 0x0400037B RID: 891
	public float draw_light_size = 0.05f;

	// Token: 0x0400037C RID: 892
	public bool trigger_on_collision;

	// Token: 0x0400037D RID: 893
	public bool can_be_collided = true;

	// Token: 0x0400037E RID: 894
	public bool can_be_left_on_ground;

	// Token: 0x0400037F RID: 895
	public bool can_be_blocked;

	// Token: 0x04000380 RID: 896
	public bool use_min_angle_height = true;

	// Token: 0x04000381 RID: 897
	public float mass = 1f;

	// Token: 0x04000382 RID: 898
	public float size = 0.5f;
}
