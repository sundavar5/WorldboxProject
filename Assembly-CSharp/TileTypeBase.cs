using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000083 RID: 131
[Serializable]
public class TileTypeBase : Asset
{
	// Token: 0x0600047F RID: 1151 RVA: 0x0002F268 File Offset: 0x0002D468
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool IsType(string v)
	{
		return this.id == v;
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x0002F276 File Offset: 0x0002D476
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool IsType(TileTypeBase pType)
	{
		return pType.index_id == this.index_id;
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x0002F286 File Offset: 0x0002D486
	public void setBiome(string pType)
	{
		if (pType == null)
		{
			this.biome_id = string.Empty;
			this.is_biome = false;
			return;
		}
		this.is_biome = true;
		this.biome_id = pType;
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x0002F2AC File Offset: 0x0002D4AC
	public void setDrawLayer(TileZIndexes pForceZ, string pForceOtherName = null)
	{
		if (pForceZ == TileZIndexes.nothing)
		{
			this.render_z = TileTypeBase.last_z;
			TileTypeBase.last_z++;
		}
		else
		{
			this.render_z = (int)pForceZ;
		}
		if (!string.IsNullOrEmpty(pForceOtherName))
		{
			this.draw_layer_name = pForceOtherName;
			return;
		}
		this.draw_layer_name = this.id;
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x0002F2F8 File Offset: 0x0002D4F8
	public void hashsetAdd(WorldTile pTile)
	{
		this._hashset_dirty = true;
		this.hashset.Add(pTile);
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x0002F30E File Offset: 0x0002D50E
	public void hashsetRemove(WorldTile pTile)
	{
		this._hashset_dirty = true;
		this.hashset.Remove(pTile);
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x0002F324 File Offset: 0x0002D524
	public List<WorldTile> getCurrentTiles()
	{
		if (this._hashset_dirty)
		{
			this._hashset_dirty = false;
			this._current_tiles.Clear();
			this._current_tiles.AddRange(this.hashset);
		}
		return this._current_tiles;
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x0002F357 File Offset: 0x0002D557
	public void hashsetClear()
	{
		this._current_tiles.Clear();
		this.hashset.Clear();
		this._hashset_dirty = false;
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x0002F376 File Offset: 0x0002D576
	public bool canBeEatenByGeophag()
	{
		return !this.liquid && !this.can_be_filled_with_ocean;
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x0002F38D File Offset: 0x0002D58D
	public bool overlapsBiomeTags(HashSet<BiomeTag> pBiomeTagsGrowth)
	{
		return this.has_biome_tags && this.biome_tags.Overlaps(pBiomeTagsGrowth);
	}

	// Token: 0x040003BA RID: 954
	public const int BURNED_STAGES = 15;

	// Token: 0x040003BB RID: 955
	public const int EXPLOSION_STAGES = 60;

	// Token: 0x040003BC RID: 956
	public const int MAX_HEIGHT = 255;

	// Token: 0x040003BD RID: 957
	[NonSerialized]
	public HashSetWorldTile hashset = new HashSetWorldTile();

	// Token: 0x040003BE RID: 958
	[NonSerialized]
	public List<MusicAsset> music_assets;

	// Token: 0x040003BF RID: 959
	[NonSerialized]
	private bool _hashset_dirty;

	// Token: 0x040003C0 RID: 960
	[NonSerialized]
	private List<WorldTile> _current_tiles = new List<WorldTile>();

	// Token: 0x040003C1 RID: 961
	public static Color32 edge_color_ocean = Toolbox.makeColor("#2D61AF");

	// Token: 0x040003C2 RID: 962
	public static Color32 edge_color_no_ocean = Toolbox.makeColor("#494949");

	// Token: 0x040003C3 RID: 963
	public static Color32 edge_color_hills = Toolbox.makeColor("#313333");

	// Token: 0x040003C4 RID: 964
	public static Color32 edge_color_mountain = Toolbox.makeColor("#2C3032");

	// Token: 0x040003C5 RID: 965
	[NonSerialized]
	public TileType increase_to;

	// Token: 0x040003C6 RID: 966
	[NonSerialized]
	public TileType decrease_to;

	// Token: 0x040003C7 RID: 967
	public WorldAction unit_death_action;

	// Token: 0x040003C8 RID: 968
	public TileStepAction step_action;

	// Token: 0x040003C9 RID: 969
	[DefaultValue(0.05f)]
	public float step_action_chance = 0.05f;

	// Token: 0x040003CA RID: 970
	public bool force_edge_variation;

	// Token: 0x040003CB RID: 971
	public int force_edge_variation_frame;

	// Token: 0x040003CC RID: 972
	[DefaultValue("")]
	public string increase_to_id = string.Empty;

	// Token: 0x040003CD RID: 973
	[DefaultValue("")]
	public string decrease_to_id = string.Empty;

	// Token: 0x040003CE RID: 974
	[DefaultValue("")]
	public string freeze_to_id = string.Empty;

	// Token: 0x040003CF RID: 975
	public bool creep;

	// Token: 0x040003D0 RID: 976
	public bool wasteland;

	// Token: 0x040003D1 RID: 977
	public int index_id;

	// Token: 0x040003D2 RID: 978
	public static int last_index_id = 0;

	// Token: 0x040003D3 RID: 979
	[DefaultValue(1f)]
	public float walk_multiplier = 1f;

	// Token: 0x040003D4 RID: 980
	[DefaultValue("")]
	public string ignore_walk_multiplier_if_tag = string.Empty;

	// Token: 0x040003D5 RID: 981
	[DefaultValue(true)]
	public bool allowed_to_be_finger_copied = true;

	// Token: 0x040003D6 RID: 982
	[DefaultValue(TileRank.Nothing)]
	public TileRank rank_type;

	// Token: 0x040003D7 RID: 983
	[DefaultValue(TileRank.Nothing)]
	public TileRank creep_rank_type;

	// Token: 0x040003D8 RID: 984
	[DefaultValue("")]
	public string biome_id = string.Empty;

	// Token: 0x040003D9 RID: 985
	[NonSerialized]
	public BiomeAsset biome_asset;

	// Token: 0x040003DA RID: 986
	public bool can_be_removed_with_spade;

	// Token: 0x040003DB RID: 987
	public bool can_be_removed_with_bucket;

	// Token: 0x040003DC RID: 988
	public bool can_be_removed_with_demolish;

	// Token: 0x040003DD RID: 989
	public bool can_be_removed_with_pickaxe;

	// Token: 0x040003DE RID: 990
	public bool can_be_removed_with_axe;

	// Token: 0x040003DF RID: 991
	public bool can_be_removed_with_sickle;

	// Token: 0x040003E0 RID: 992
	public bool is_biome;

	// Token: 0x040003E1 RID: 993
	public bool block;

	// Token: 0x040003E2 RID: 994
	public float block_height;

	// Token: 0x040003E3 RID: 995
	public bool animated_wall;

	// Token: 0x040003E4 RID: 996
	[NonSerialized]
	public TileSprites sprites;

	// Token: 0x040003E5 RID: 997
	[DefaultValue(TileLayerType.Ground)]
	public TileLayerType layer_type = TileLayerType.Ground;

	// Token: 0x040003E6 RID: 998
	public int render_z;

	// Token: 0x040003E7 RID: 999
	public string draw_layer_name;

	// Token: 0x040003E8 RID: 1000
	public static int last_z;

	// Token: 0x040003E9 RID: 1001
	public bool can_be_set_on_fire;

	// Token: 0x040003EA RID: 1002
	public bool burnable;

	// Token: 0x040003EB RID: 1003
	public bool can_be_set_on_fire_by_burning_feet = true;

	// Token: 0x040003EC RID: 1004
	[DefaultValue(10)]
	public int burn_rate = 10;

	// Token: 0x040003ED RID: 1005
	public bool hold_lava;

	// Token: 0x040003EE RID: 1006
	public bool can_be_filled_with_ocean;

	// Token: 0x040003EF RID: 1007
	public string fill_to_ocean;

	// Token: 0x040003F0 RID: 1008
	public string water_fill_sound;

	// Token: 0x040003F1 RID: 1009
	public bool liquid;

	// Token: 0x040003F2 RID: 1010
	public bool ocean;

	// Token: 0x040003F3 RID: 1011
	public bool ground;

	// Token: 0x040003F4 RID: 1012
	public bool forever_frozen;

	// Token: 0x040003F5 RID: 1013
	public bool road;

	// Token: 0x040003F6 RID: 1014
	public bool life;

	// Token: 0x040003F7 RID: 1015
	public bool damaged_when_walked;

	// Token: 0x040003F8 RID: 1016
	public bool remove_on_heat;

	// Token: 0x040003F9 RID: 1017
	public bool remove_on_freeze;

	// Token: 0x040003FA RID: 1018
	public bool chunk_dirty_when_temperature;

	// Token: 0x040003FB RID: 1019
	public bool fast_freeze;

	// Token: 0x040003FC RID: 1020
	[DefaultValue(true)]
	public bool can_be_frozen = true;

	// Token: 0x040003FD RID: 1021
	public bool can_be_unfrozen;

	// Token: 0x040003FE RID: 1022
	[DefaultValue(true)]
	public bool can_be_autotested = true;

	// Token: 0x040003FF RID: 1023
	public bool grey_goo;

	// Token: 0x04000400 RID: 1024
	public bool grass;

	// Token: 0x04000401 RID: 1025
	public bool sand;

	// Token: 0x04000402 RID: 1026
	public bool rocks;

	// Token: 0x04000403 RID: 1027
	public bool mountains;

	// Token: 0x04000404 RID: 1028
	public bool summit;

	// Token: 0x04000405 RID: 1029
	public bool soil;

	// Token: 0x04000406 RID: 1030
	public bool terraform_after_fire;

	// Token: 0x04000407 RID: 1031
	public bool explodable;

	// Token: 0x04000408 RID: 1032
	public bool explodable_delayed;

	// Token: 0x04000409 RID: 1033
	public bool explodable_timed;

	// Token: 0x0400040A RID: 1034
	public bool explodable_by_ocean;

	// Token: 0x0400040B RID: 1035
	public bool ignore_ocean_edge_rendering;

	// Token: 0x0400040C RID: 1036
	public int explode_range;

	// Token: 0x0400040D RID: 1037
	public bool damage_units;

	// Token: 0x0400040E RID: 1038
	[DefaultValue(1)]
	public int damage = 1;

	// Token: 0x0400040F RID: 1039
	public bool lava;

	// Token: 0x04000410 RID: 1040
	[DefaultValue(-1)]
	public int lava_level = -1;

	// Token: 0x04000411 RID: 1041
	public int lava_change_state_after;

	// Token: 0x04000412 RID: 1042
	[DefaultValue("")]
	public string lava_decrease = string.Empty;

	// Token: 0x04000413 RID: 1043
	[DefaultValue("")]
	public string lava_increase = string.Empty;

	// Token: 0x04000414 RID: 1044
	public bool edge_hills;

	// Token: 0x04000415 RID: 1045
	public bool edge_mountains;

	// Token: 0x04000416 RID: 1046
	public bool wall;

	// Token: 0x04000417 RID: 1047
	[DefaultValue(true)]
	public bool check_edge = true;

	// Token: 0x04000418 RID: 1048
	public bool can_be_biome;

	// Token: 0x04000419 RID: 1049
	[DefaultValue(true)]
	public bool can_errode_to_sand = true;

	// Token: 0x0400041A RID: 1050
	public int explodeTimer;

	// Token: 0x0400041B RID: 1051
	[DefaultValue(1)]
	public int cost = 1;

	// Token: 0x0400041C RID: 1052
	public bool can_be_farm;

	// Token: 0x0400041D RID: 1053
	public bool farm_field;

	// Token: 0x0400041E RID: 1054
	public bool can_build_on;

	// Token: 0x0400041F RID: 1055
	public bool biome_build_check;

	// Token: 0x04000420 RID: 1056
	[DefaultValue(1f)]
	public float fire_chance = 1f;

	// Token: 0x04000421 RID: 1057
	[DefaultValue("")]
	public string food_resource = string.Empty;

	// Token: 0x04000422 RID: 1058
	public int nutrition;

	// Token: 0x04000423 RID: 1059
	public HashSet<BiomeTag> biome_tags;

	// Token: 0x04000424 RID: 1060
	[NonSerialized]
	public bool has_biome_tags;

	// Token: 0x04000425 RID: 1061
	public string color_hex;

	// Token: 0x04000426 RID: 1062
	[NonSerialized]
	public Color32 color;

	// Token: 0x04000427 RID: 1063
	public string edge_color_hex;

	// Token: 0x04000428 RID: 1064
	[NonSerialized]
	public Color32 edge_color;

	// Token: 0x04000429 RID: 1065
	public bool considered_empty_tile;

	// Token: 0x0400042A RID: 1066
	public bool drawPixel;

	// Token: 0x0400042B RID: 1067
	[DefaultValue(3)]
	public int strength = 3;

	// Token: 0x0400042C RID: 1068
	[DefaultValue("mat_world_object")]
	public string material = "mat_world_object";

	// Token: 0x0400042D RID: 1069
	[DefaultValue(-1)]
	public int height_min = -1;

	// Token: 0x0400042E RID: 1070
	public int[] additional_height;

	// Token: 0x0400042F RID: 1071
	public string only_allowed_to_build_with_tag;

	// Token: 0x04000430 RID: 1072
	public bool used_in_generator;
}
