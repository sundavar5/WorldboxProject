using System;
using UnityEngine;

// Token: 0x02000035 RID: 53
[Serializable]
public class CloudAsset : Asset
{
	// Token: 0x040001E5 RID: 485
	public bool normal_cloud;

	// Token: 0x040001E6 RID: 486
	[NonSerialized]
	public Color color;

	// Token: 0x040001E7 RID: 487
	public string color_hex = "#FFFFFF";

	// Token: 0x040001E8 RID: 488
	public float max_alpha = 0.8f;

	// Token: 0x040001E9 RID: 489
	public CloudAction cloud_action_1;

	// Token: 0x040001EA RID: 490
	public CloudAction cloud_action_2;

	// Token: 0x040001EB RID: 491
	public float interval_action_1 = 0.05f;

	// Token: 0x040001EC RID: 492
	public float interval_action_2 = 0.05f;

	// Token: 0x040001ED RID: 493
	public float speed_min = 1f;

	// Token: 0x040001EE RID: 494
	public float speed_max = 6f;

	// Token: 0x040001EF RID: 495
	public string drop_id = string.Empty;

	// Token: 0x040001F0 RID: 496
	public string[] path_sprites;

	// Token: 0x040001F1 RID: 497
	[NonSerialized]
	internal Sprite[] cached_sprites;

	// Token: 0x040001F2 RID: 498
	public bool draw_light_area;

	// Token: 0x040001F3 RID: 499
	public float draw_light_area_offset_x;

	// Token: 0x040001F4 RID: 500
	public float draw_light_area_offset_y;

	// Token: 0x040001F5 RID: 501
	public float draw_light_size = 4f;

	// Token: 0x040001F6 RID: 502
	public bool considered_disaster;
}
