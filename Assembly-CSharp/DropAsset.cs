using System;
using System.ComponentModel;
using UnityEngine;

// Token: 0x0200003F RID: 63
[Serializable]
public class DropAsset : Asset
{
	// Token: 0x06000285 RID: 645 RVA: 0x00017089 File Offset: 0x00015289
	public string getRandomBuildingAsset()
	{
		if (this._building_asset_split == null)
		{
			this._building_asset_split = this.building_asset.Split(',', StringSplitOptions.None);
		}
		return this._building_asset_split.GetRandom<string>();
	}

	// Token: 0x0400023E RID: 574
	[DefaultValue(DropType.DropGeneric)]
	public DropType type = DropType.DropGeneric;

	// Token: 0x0400023F RID: 575
	public bool random_frame;

	// Token: 0x04000240 RID: 576
	public bool random_flip;

	// Token: 0x04000241 RID: 577
	public bool animated;

	// Token: 0x04000242 RID: 578
	[DefaultValue(0.1f)]
	public float animation_speed = 0.1f;

	// Token: 0x04000243 RID: 579
	[DefaultValue(0.1f)]
	public float animation_speed_random = 0.1f;

	// Token: 0x04000244 RID: 580
	public bool animation_rotation;

	// Token: 0x04000245 RID: 581
	[DefaultValue(1f)]
	public float animation_rotation_speed_min = 1f;

	// Token: 0x04000246 RID: 582
	[DefaultValue(1f)]
	public float animation_rotation_speed_max = 1f;

	// Token: 0x04000247 RID: 583
	public string sound_drop;

	// Token: 0x04000248 RID: 584
	public string sound_launch;

	// Token: 0x04000249 RID: 585
	public DropsAction action_launch;

	// Token: 0x0400024A RID: 586
	public DropsAction action_landed;

	// Token: 0x0400024B RID: 587
	public DropsLandedAction action_landed_drop;

	// Token: 0x0400024C RID: 588
	public string building_asset;

	// Token: 0x0400024D RID: 589
	[DefaultValue(3.2f)]
	public float falling_speed = 3.2f;

	// Token: 0x0400024E RID: 590
	[DefaultValue(0.5f)]
	public float falling_speed_random = 0.5f;

	// Token: 0x0400024F RID: 591
	public Vector3 falling_height = new Vector2(15f, 20f);

	// Token: 0x04000250 RID: 592
	public bool falling_random_x_move;

	// Token: 0x04000251 RID: 593
	public float particle_interval;

	// Token: 0x04000252 RID: 594
	[DefaultValue("mat_world_object")]
	public string material = "mat_world_object";

	// Token: 0x04000253 RID: 595
	[DefaultValue("drops/drop_pixel")]
	public string path_texture = "drops/drop_pixel";

	// Token: 0x04000254 RID: 596
	[DefaultValue(1f)]
	public float default_scale = 1f;

	// Token: 0x04000255 RID: 597
	public bool surprises_units;

	// Token: 0x04000256 RID: 598
	public string drop_type_low;

	// Token: 0x04000257 RID: 599
	public string drop_type_high;

	// Token: 0x04000258 RID: 600
	[NonSerialized]
	public TopTileType cached_drop_type_low;

	// Token: 0x04000259 RID: 601
	[NonSerialized]
	public TopTileType cached_drop_type_high;

	// Token: 0x0400025A RID: 602
	[NonSerialized]
	public Sprite[] cached_sprites;

	// Token: 0x0400025B RID: 603
	private string[] _building_asset_split;
}
