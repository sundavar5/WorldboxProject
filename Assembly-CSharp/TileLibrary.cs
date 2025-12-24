using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200007D RID: 125
public class TileLibrary : TileLibraryMain<TileType>
{
	// Token: 0x0600046C RID: 1132 RVA: 0x0002DF58 File Offset: 0x0002C158
	public override void init()
	{
		base.init();
		TileLibrary.deep_ocean = this.add(new TileType
		{
			id = "deep_ocean",
			color_hex = "#3370CC",
			liquid = true,
			ocean = true,
			height_min = 0,
			decrease_to_id = "pit_deep_ocean",
			increase_to_id = "pit_close_ocean",
			walk_multiplier = 0.1f,
			strength = 0,
			layer_type = TileLayerType.Ocean,
			can_be_frozen = false,
			can_errode_to_sand = false
		});
		this.t.considered_empty_tile = true;
		this.t.used_in_generator = true;
		this.t.setDrawLayer(TileZIndexes.deep_ocean, null);
		this.t.render_z = 0;
		TileLibrary.close_ocean = this.clone("close_ocean", "deep_ocean");
		this.t.considered_empty_tile = false;
		this.t.can_be_frozen = false;
		this.t.used_in_generator = true;
		this.t.setDrawLayer(TileZIndexes.close_ocean, null);
		this.t.drawPixel = true;
		this.t.color_hex = "#4084E2";
		this.t.height_min = 30;
		this.t.decrease_to_id = "pit_close_ocean";
		this.t.increase_to_id = "pit_shallow_waters";
		this.t.strength = 0;
		this.t.layer_type = TileLayerType.Ocean;
		this.t.can_errode_to_sand = false;
		TileLibrary.shallow_waters = this.add(new TileType
		{
			id = "shallow_waters",
			drawPixel = true,
			can_be_frozen = true,
			color_hex = "#55AEF0",
			edge_color_hex = "#3F90EA",
			liquid = true,
			ocean = true,
			height_min = 70,
			freeze_to_id = "ice",
			decrease_to_id = "pit_shallow_waters",
			increase_to_id = "sand",
			walk_multiplier = 0.1f,
			strength = 0,
			layer_type = TileLayerType.Ocean,
			can_errode_to_sand = false,
			fast_freeze = true
		});
		this.t.used_in_generator = true;
		this.t.setDrawLayer(TileZIndexes.shallow_waters, null);
		TileLibrary.pit_deep_ocean = this.clone("pit_deep_ocean", "deep_ocean");
		this.t.can_be_frozen = false;
		this.t.setDrawLayer(TileZIndexes.pit_deep_ocean, null);
		this.t.drawPixel = true;
		this.t.color_hex = "#898989";
		this.t.liquid = false;
		this.t.ocean = false;
		this.t.walk_multiplier = 1f;
		this.t.can_be_filled_with_ocean = true;
		this.t.fill_to_ocean = "deep_ocean";
		this.t.water_fill_sound = "event:/SFX/NATURE/FillWaterTile";
		this.t.ground = true;
		this.t.decrease_to_id = string.Empty;
		this.t.increase_to_id = "pit_close_ocean";
		this.t.can_be_set_on_fire = true;
		this.t.layer_type = TileLayerType.Ground;
		this.t.strength = 2;
		this.t.considered_empty_tile = true;
		TileLibrary.pit_close_ocean = this.clone("pit_close_ocean", "close_ocean");
		this.t.can_be_frozen = false;
		this.t.setDrawLayer(TileZIndexes.pit_close_ocean, null);
		this.t.drawPixel = true;
		this.t.color_hex = "#A0A0A0";
		this.t.liquid = false;
		this.t.ocean = false;
		this.t.walk_multiplier = 1f;
		this.t.can_be_filled_with_ocean = true;
		this.t.fill_to_ocean = "close_ocean";
		this.t.water_fill_sound = "event:/SFX/NATURE/FillWaterTile";
		this.t.decrease_to_id = "pit_deep_ocean";
		this.t.increase_to_id = "pit_shallow_waters";
		this.t.can_be_set_on_fire = true;
		this.t.layer_type = TileLayerType.Ground;
		this.t.strength = 2;
		this.t.ground = true;
		TileLibrary.pit_shallow_waters = this.clone("pit_shallow_waters", "shallow_waters");
		this.t.can_be_frozen = false;
		this.t.setDrawLayer(TileZIndexes.pit_shallow_waters, null);
		this.t.drawPixel = true;
		this.t.color_hex = "#C1C1C1";
		this.t.liquid = false;
		this.t.ocean = false;
		this.t.walk_multiplier = 1f;
		this.t.can_be_filled_with_ocean = true;
		this.t.fill_to_ocean = "shallow_waters";
		this.t.water_fill_sound = "event:/SFX/NATURE/FillWaterTile";
		this.t.decrease_to_id = "pit_close_ocean";
		this.t.increase_to_id = "sand";
		this.t.freeze_to_id = string.Empty;
		this.t.can_be_set_on_fire = true;
		this.t.layer_type = TileLayerType.Ground;
		this.t.ground = true;
		this.t.strength = 2;
		this.add(new TileType
		{
			id = "border_pit",
			layer_type = TileLayerType.Ground,
			can_be_autotested = false
		});
		this.t.setDrawLayer(TileZIndexes.border_pit, null);
		this.add(new TileType
		{
			id = "border_water",
			layer_type = TileLayerType.Ground,
			can_be_autotested = false
		});
		this.t.setDrawLayer(TileZIndexes.border_water, null);
		this.add(new TileType
		{
			id = "border_water_runup",
			layer_type = TileLayerType.Ground,
			can_be_autotested = false
		});
		this.t.setDrawLayer(TileZIndexes.border_water_runup, null);
		TileLibrary.sand = this.add(new TileType
		{
			cost = 116,
			biome_build_check = true,
			id = "sand",
			sand = true,
			drawPixel = true,
			color_hex = "#F7E898",
			edge_color_hex = "#D8C08C",
			height_min = 98,
			decrease_to_id = "pit_shallow_waters",
			increase_to_id = "soil_low",
			ground = true,
			walk_multiplier = 0.5f,
			freeze_to_id = "snow_sand",
			creep_rank_type = TileRank.Low,
			can_be_set_on_fire = true,
			can_build_on = true,
			can_be_farm = true
		});
		this.t.ignore_walk_multiplier_if_tag = "walk_adaptation_sand";
		this.t.setBiome("biome_sand");
		this.t.used_in_generator = true;
		this.t.setDrawLayer(TileZIndexes.sand, null);
		this.t.biome_tags = AssetLibrary<TileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Sand
		});
		TileLibrary.soil_low = this.add(new TileType
		{
			cost = 115,
			drawPixel = true,
			id = "soil_low",
			color_hex = "#E2934B",
			height_min = 108,
			decrease_to_id = "sand",
			increase_to_id = "soil_high",
			ground = true,
			can_be_biome = true,
			soil = true,
			freeze_to_id = "frozen_low",
			rank_type = TileRank.Low,
			creep_rank_type = TileRank.Low,
			can_be_farm = true,
			can_build_on = true,
			can_be_set_on_fire = true,
			used_in_generator = true,
			food_resource = "worms",
			biome_build_check = true
		});
		this.t.setDrawLayer(TileZIndexes.soil_low, null);
		this.t.biome_tags = AssetLibrary<TileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Soil
		});
		TileLibrary.soil_high = this.add(new TileType
		{
			cost = 120,
			drawPixel = true,
			id = "soil_high",
			color_hex = "#B66F3A",
			height_min = 128,
			additional_height = new int[]
			{
				15,
				16,
				17,
				14,
				13,
				12,
				11,
				10
			},
			decrease_to_id = "soil_low",
			increase_to_id = "hills",
			ground = true,
			rank_type = TileRank.High,
			creep_rank_type = TileRank.High,
			can_be_biome = true,
			soil = true,
			freeze_to_id = "frozen_high",
			can_be_farm = true,
			can_build_on = true,
			can_be_set_on_fire = true,
			used_in_generator = true,
			food_resource = "worms",
			biome_build_check = true
		});
		this.t.setDrawLayer(TileZIndexes.soil_high, null);
		this.t.biome_tags = AssetLibrary<TileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Soil
		});
		TileLibrary.lava0 = this.add(new TileType
		{
			cost = 100,
			drawPixel = true,
			id = "lava0",
			color_hex = "#F62D14",
			decrease_to_id = "sand",
			increase_to_id = "hills",
			liquid = true,
			walk_multiplier = 0.2f,
			damage_units = true,
			damage = 150,
			lava = true,
			lava_level = 0,
			strength = 0,
			layer_type = TileLayerType.Lava,
			can_be_frozen = false,
			material = "mat_world_object_lit"
		});
		this.t.lava_increase = "lava1";
		this.t.lava_change_state_after = 30;
		this.t.step_action = new TileStepAction(TileActionLibrary.setUnitOnFire);
		this.t.step_action_chance = 0.9f;
		this.t.setDrawLayer(TileZIndexes.lava0, null);
		TileLibrary.lava1 = this.clone("lava1", "lava0");
		this.t.setDrawLayer(TileZIndexes.lava1, null);
		this.t.color_hex = "#FF6700";
		this.t.step_action = new TileStepAction(TileActionLibrary.setUnitOnFire);
		this.t.step_action_chance = 0.9f;
		this.t.lava_level = 1;
		this.t.lava_decrease = "lava0";
		this.t.lava_increase = "lava2";
		this.t.lava_change_state_after = 10;
		TileLibrary.lava2 = this.clone("lava2", "lava0");
		this.t.setDrawLayer(TileZIndexes.lava2, null);
		this.t.color_hex = "#FFAC00";
		this.t.step_action = new TileStepAction(TileActionLibrary.setUnitOnFire);
		this.t.step_action_chance = 0.9f;
		this.t.lava_level = 2;
		this.t.lava_decrease = "lava1";
		this.t.lava_increase = "lava3";
		this.t.lava_change_state_after = 10;
		TileLibrary.lava3 = this.clone("lava3", "lava0");
		this.t.setDrawLayer(TileZIndexes.lava3, null);
		this.t.color_hex = "#FFDE00";
		this.t.step_action = new TileStepAction(TileActionLibrary.setUnitOnFire);
		this.t.step_action_chance = 0.9f;
		this.t.lava_level = 3;
		this.t.lava_decrease = "lava2";
		this.t.lava_increase = string.Empty;
		this.t.lava_change_state_after = 3;
		TileLibrary.hills = this.add(new TileType
		{
			cost = 140,
			drawPixel = true,
			id = "hills",
			color_hex = "#5B5E5C",
			height_min = 199,
			rocks = true,
			ground = true,
			edge_hills = true,
			additional_height = new int[]
			{
				2,
				-6
			},
			decrease_to_id = "soil_high",
			increase_to_id = "mountains",
			freeze_to_id = "snow_hills",
			can_be_set_on_fire = true
		});
		this.t.setBiome("biome_hill");
		this.t.biome_tags = AssetLibrary<TileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Hills
		});
		this.t.hold_lava = true;
		this.t.used_in_generator = true;
		this.t.setDrawLayer(TileZIndexes.hills, null);
		TileLibrary.mountains = this.add(new TileType
		{
			cost = 160,
			drawPixel = true,
			id = "mountains",
			color_hex = "#414545",
			height_min = 210,
			rocks = true,
			mountains = true,
			edge_mountains = true,
			additional_height = new int[]
			{
				2,
				4
			},
			decrease_to_id = "hills",
			increase_to_id = "summit",
			walk_multiplier = 0.5f,
			freeze_to_id = "snow_block",
			can_be_set_on_fire = true,
			layer_type = TileLayerType.Block,
			block = true,
			block_height = 3f,
			force_edge_variation = true,
			force_edge_variation_frame = 2
		});
		this.t.hold_lava = true;
		this.t.used_in_generator = true;
		this.t.setDrawLayer(TileZIndexes.mountains, null);
		TileLibrary.summit = this.add(new TileType
		{
			cost = 160,
			drawPixel = true,
			id = "summit",
			color_hex = "#333333",
			height_min = 230,
			rocks = true,
			mountains = true,
			edge_mountains = true,
			additional_height = new int[]
			{
				2,
				4
			},
			decrease_to_id = "mountains",
			walk_multiplier = 0.5f,
			freeze_to_id = "snow_summit",
			can_be_set_on_fire = true,
			layer_type = TileLayerType.Block,
			block = true,
			block_height = 5f,
			force_edge_variation = true,
			force_edge_variation_frame = 2
		});
		this.t.summit = true;
		this.t.hold_lava = true;
		this.t.used_in_generator = true;
		this.t.setDrawLayer(TileZIndexes.summit, null);
		TileLibrary.grey_goo = this.add(new TileType
		{
			cost = 10,
			drawPixel = true,
			grey_goo = true,
			id = "grey_goo",
			color_hex = "#5D6191",
			decrease_to_id = "pit_deep_ocean",
			burnable = true,
			ground = false,
			walk_multiplier = 0.1f,
			damage_units = true,
			damage = 200,
			strength = 0,
			life = true,
			can_be_frozen = false,
			layer_type = TileLayerType.Goo
		});
		this.t.setDrawLayer(TileZIndexes.grey_goo, null);
		TileLibrary.lava_types = new List<TileType>
		{
			TileLibrary.lava0,
			TileLibrary.lava1,
			TileLibrary.lava2,
			TileLibrary.lava3
		};
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x0002EE00 File Offset: 0x0002D000
	private void loadTileSprites()
	{
		foreach (TileType tType in this.list)
		{
			this.loadSpritesForTile(tType);
		}
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x0002EE54 File Offset: 0x0002D054
	private void loadSpritesForTile(TileType pType)
	{
		Sprite[] tSpritesArr = SpriteTextureLoader.getSpriteList("tiles/" + pType.id, false);
		if (tSpritesArr == null || tSpritesArr.Length == 0)
		{
			return;
		}
		pType.sprites = new TileSprites();
		foreach (Sprite tSprite in tSpritesArr)
		{
			pType.sprites.addVariation(tSprite, pType.id);
		}
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x0002EEB1 File Offset: 0x0002D0B1
	public TileType getGen(string pID)
	{
		if (!this.dict.ContainsKey(pID))
		{
			return null;
		}
		return this.dict[pID];
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x0002EECF File Offset: 0x0002D0CF
	public override TileType add(TileType pAsset)
	{
		pAsset.index_id = TileTypeBase.last_index_id++;
		TileLibrary.array_tiles[pAsset.index_id] = pAsset;
		return base.add(pAsset);
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x0002EEF8 File Offset: 0x0002D0F8
	public override void linkAssets()
	{
		base.linkAssets();
		using (ListPool<TileType> tListGenerator = new ListPool<TileType>())
		{
			foreach (TileType tType in this.list)
			{
				if (tType.used_in_generator)
				{
					tListGenerator.Add(tType);
				}
			}
			this.setListTo(DepthGeneratorType.Generator);
			for (int i = 0; i < this._depth_list_generator.Length; i++)
			{
				this._depth_list_generator[i] = this.getTypeByDepth(i, tListGenerator);
			}
			this.setListTo(DepthGeneratorType.Gameplay);
			for (int j = 0; j < this._depth_list_gameplay.Length; j++)
			{
				this._depth_list_gameplay[j] = this.getTypeByDepth(j, this.list);
			}
			this.setListTo(DepthGeneratorType.Generator);
			foreach (TileType tType2 in this.list)
			{
				tType2.decrease_to = this.getGen(tType2.decrease_to_id);
				tType2.increase_to = this.getGen(tType2.increase_to_id);
			}
			this.loadTileSprites();
			foreach (TileTypeBase tTileType in this.list)
			{
				if (!string.IsNullOrEmpty(tTileType.biome_id))
				{
					tTileType.biome_asset = AssetManager.biome_library.get(tTileType.biome_id);
				}
			}
		}
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x0002F0D8 File Offset: 0x0002D2D8
	public void setListTo(DepthGeneratorType pVal)
	{
		if (pVal == DepthGeneratorType.Generator)
		{
			this._depth_list = this._depth_list_generator;
			return;
		}
		if (pVal != DepthGeneratorType.Gameplay)
		{
			return;
		}
		this._depth_list = this._depth_list_gameplay;
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x0002F0FC File Offset: 0x0002D2FC
	public TileType getTypeByDepth(int pHeight, IReadOnlyList<TileType> pList)
	{
		TileType tTileType = null;
		for (int i = 0; i < pList.Count; i++)
		{
			TileType tType = pList[i];
			if (tType.height_min != -1 && (tTileType == null || pHeight >= tType.height_min))
			{
				tTileType = tType;
			}
		}
		return tTileType;
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x0002F13C File Offset: 0x0002D33C
	public override TileType clone(string pNew, string pFrom)
	{
		TileType tileType = base.clone(pNew, pFrom);
		tileType.can_be_farm = false;
		tileType.used_in_generator = false;
		return tileType;
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x0002F154 File Offset: 0x0002D354
	public TileType getTypeByDepth(WorldTile pWorldTile)
	{
		return this._depth_list[pWorldTile.Height];
	}

	// Token: 0x04000399 RID: 921
	private TileType[] _depth_list_generator = new TileType[256];

	// Token: 0x0400039A RID: 922
	private TileType[] _depth_list_gameplay = new TileType[256];

	// Token: 0x0400039B RID: 923
	private TileType[] _depth_list;

	// Token: 0x0400039C RID: 924
	public static List<TileType> lava_types = new List<TileType>();

	// Token: 0x0400039D RID: 925
	public static TileType summit;

	// Token: 0x0400039E RID: 926
	public static TileType mountains;

	// Token: 0x0400039F RID: 927
	public static TileType hills;

	// Token: 0x040003A0 RID: 928
	public static TileType grey_goo;

	// Token: 0x040003A1 RID: 929
	public static TileType deep_ocean;

	// Token: 0x040003A2 RID: 930
	public static TileType close_ocean;

	// Token: 0x040003A3 RID: 931
	public static TileType shallow_waters;

	// Token: 0x040003A4 RID: 932
	public static TileType sand;

	// Token: 0x040003A5 RID: 933
	public static TileType soil_low;

	// Token: 0x040003A6 RID: 934
	public static TileType soil_high;

	// Token: 0x040003A7 RID: 935
	public static TileType lava0;

	// Token: 0x040003A8 RID: 936
	public static TileType lava1;

	// Token: 0x040003A9 RID: 937
	public static TileType lava2;

	// Token: 0x040003AA RID: 938
	public static TileType lava3;

	// Token: 0x040003AB RID: 939
	public static TileType pit_deep_ocean;

	// Token: 0x040003AC RID: 940
	public static TileType pit_close_ocean;

	// Token: 0x040003AD RID: 941
	public static TileType pit_shallow_waters;

	// Token: 0x040003AE RID: 942
	public static TileTypeBase[] array_tiles = new TileTypeBase[256];
}
