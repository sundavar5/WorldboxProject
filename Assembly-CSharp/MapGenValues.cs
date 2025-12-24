using System;

// Token: 0x02000057 RID: 87
[Serializable]
public class MapGenValues
{
	// Token: 0x040002B8 RID: 696
	public int perlin_scale_stage_1 = 5;

	// Token: 0x040002B9 RID: 697
	public int perlin_scale_stage_2 = 5;

	// Token: 0x040002BA RID: 698
	public int perlin_scale_stage_3 = 5;

	// Token: 0x040002BB RID: 699
	public bool main_perlin_noise_stage;

	// Token: 0x040002BC RID: 700
	public bool perlin_noise_stage_2;

	// Token: 0x040002BD RID: 701
	public bool perlin_noise_stage_3;

	// Token: 0x040002BE RID: 702
	public bool square_edges;

	// Token: 0x040002BF RID: 703
	public bool gradient_round_edges;

	// Token: 0x040002C0 RID: 704
	public bool add_center_gradient_land;

	// Token: 0x040002C1 RID: 705
	public bool center_gradient_mountains;

	// Token: 0x040002C2 RID: 706
	public bool add_center_lake;

	// Token: 0x040002C3 RID: 707
	public bool ring_effect;

	// Token: 0x040002C4 RID: 708
	public bool add_vegetation = true;

	// Token: 0x040002C5 RID: 709
	public bool add_resources = true;

	// Token: 0x040002C6 RID: 710
	public bool add_mountain_edges;

	// Token: 0x040002C7 RID: 711
	public bool random_biomes = true;

	// Token: 0x040002C8 RID: 712
	public int random_shapes_amount;

	// Token: 0x040002C9 RID: 713
	public int cubicle_size;

	// Token: 0x040002CA RID: 714
	public bool remove_mountains;

	// Token: 0x040002CB RID: 715
	public bool forbidden_knowledge_start;

	// Token: 0x040002CC RID: 716
	public bool low_ground;

	// Token: 0x040002CD RID: 717
	public bool high_ground;
}
