using System;
using System.Collections.Generic;

// Token: 0x0200005A RID: 90
[Serializable]
public class MapGenTemplate : Asset, IDescriptionAsset, ILocalizedAsset
{
	// Token: 0x06000355 RID: 853 RVA: 0x0001E5EF File Offset: 0x0001C7EF
	public string getLocaleID()
	{
		return "template_" + this.id;
	}

	// Token: 0x06000356 RID: 854 RVA: 0x0001E601 File Offset: 0x0001C801
	public string getDescriptionID()
	{
		return "template_" + this.id + "_info";
	}

	// Token: 0x040002D3 RID: 723
	public MapGenValues values = new MapGenValues();

	// Token: 0x040002D4 RID: 724
	public int force_height_to;

	// Token: 0x040002D5 RID: 725
	public bool freeze_mountains;

	// Token: 0x040002D6 RID: 726
	public bool special_anthill;

	// Token: 0x040002D7 RID: 727
	public bool special_checkerboard;

	// Token: 0x040002D8 RID: 728
	public bool special_cubicles;

	// Token: 0x040002D9 RID: 729
	public List<PerlinReplaceContainer> perlin_replace = new List<PerlinReplaceContainer>();

	// Token: 0x040002DA RID: 730
	public string path_icon = "ui/template_icon_1";

	// Token: 0x040002DB RID: 731
	public bool allow_edit_size = true;

	// Token: 0x040002DC RID: 732
	public bool allow_edit_random_shapes = true;

	// Token: 0x040002DD RID: 733
	public bool allow_edit_random_biomes = true;

	// Token: 0x040002DE RID: 734
	public bool allow_edit_perlin_scale_stage_1 = true;

	// Token: 0x040002DF RID: 735
	public bool allow_edit_perlin_scale_stage_2 = true;

	// Token: 0x040002E0 RID: 736
	public bool allow_edit_perlin_scale_stage_3 = true;

	// Token: 0x040002E1 RID: 737
	public bool allow_edit_mountain_edges = true;

	// Token: 0x040002E2 RID: 738
	public bool allow_edit_random_vegetation = true;

	// Token: 0x040002E3 RID: 739
	public bool allow_edit_random_resources = true;

	// Token: 0x040002E4 RID: 740
	public bool allow_edit_center_lake = true;

	// Token: 0x040002E5 RID: 741
	public bool allow_edit_round_edges = true;

	// Token: 0x040002E6 RID: 742
	public bool allow_edit_square_edges = true;

	// Token: 0x040002E7 RID: 743
	public bool allow_edit_ring_effect = true;

	// Token: 0x040002E8 RID: 744
	public bool allow_edit_center_land = true;

	// Token: 0x040002E9 RID: 745
	public bool allow_edit_low_ground = true;

	// Token: 0x040002EA RID: 746
	public bool allow_edit_high_ground = true;

	// Token: 0x040002EB RID: 747
	public bool allow_edit_remove_mountains = true;

	// Token: 0x040002EC RID: 748
	public bool allow_edit_cubicles;

	// Token: 0x040002ED RID: 749
	public bool show_reset_button = true;
}
