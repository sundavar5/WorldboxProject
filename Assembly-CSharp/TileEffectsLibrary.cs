using System;
using System.Collections.Generic;

// Token: 0x020000FB RID: 251
public class TileEffectsLibrary : AssetLibrary<TileEffectAsset>
{
	// Token: 0x06000766 RID: 1894 RVA: 0x0006C694 File Offset: 0x0006A894
	public override void init()
	{
		base.init();
		this.add(new TileEffectAsset
		{
			id = "wave_normal",
			rate = 100,
			chance = 1f,
			path_sprite = "effects/tile_effects/waves/wave_normal"
		});
		this.t.addTileTypes(new string[]
		{
			"close_ocean",
			"deep_ocean",
			"shallow_waters"
		});
		this.add(new TileEffectAsset
		{
			id = "wave_mermaid",
			chance = 0.05f,
			path_sprite = "effects/tile_effects/waves/wave_mermaid"
		});
		this.t.addTileTypes(new string[]
		{
			"close_ocean",
			"deep_ocean"
		});
		this.add(new TileEffectAsset
		{
			id = "wave_dolphin",
			chance = 0.05f,
			path_sprite = "effects/tile_effects/waves/wave_dolphin"
		});
		this.t.addTileTypes(new string[]
		{
			"close_ocean",
			"deep_ocean"
		});
		this.add(new TileEffectAsset
		{
			id = "wave_tentacle",
			chance = 0.05f,
			path_sprite = "effects/tile_effects/waves/wave_tentacle"
		});
		this.t.addTileTypes(new string[]
		{
			"close_ocean",
			"deep_ocean"
		});
		this.add(new TileEffectAsset
		{
			id = "wave_shark_fin",
			chance = 0.05f,
			path_sprite = "effects/tile_effects/waves/wave_shark_fin"
		});
		this.t.addTileTypes(new string[]
		{
			"close_ocean",
			"deep_ocean"
		});
		this.add(new TileEffectAsset
		{
			id = "wave_bubbles",
			chance = 0.05f,
			path_sprite = "effects/tile_effects/waves/wave_bubbles"
		});
		this.t.addTileTypes(new string[]
		{
			"close_ocean",
			"deep_ocean",
			"shallow_waters"
		});
		this.add(new TileEffectAsset
		{
			id = "enchanted_sparkle",
			chance = 0.3f,
			path_sprite = "effects/tile_effects/enchanted_sparkle"
		});
		this.t.addTileTypes(new string[]
		{
			"enchanted_high",
			"enchanted_low"
		});
		this.add(new TileEffectAsset
		{
			id = "paradox_effect",
			chance = 0.3f,
			path_sprite = "effects/tile_effects/paradox_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"paradox_high",
			"paradox_low"
		});
		this.add(new TileEffectAsset
		{
			id = "desert_effect",
			chance = 0.3f,
			path_sprite = "effects/tile_effects/desert_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"desert_high",
			"desert_low"
		});
		this.add(new TileEffectAsset
		{
			id = "celestial_effect",
			chance = 0.3f,
			path_sprite = "effects/tile_effects/celestial_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"celestial_low",
			"celestial_high"
		});
		this.add(new TileEffectAsset
		{
			id = "singularity_effect",
			chance = 0.3f,
			path_sprite = "effects/tile_effects/singularity_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"singularity_high",
			"singularity_low"
		});
		this.add(new TileEffectAsset
		{
			id = "corrupted_effect",
			chance = 0.3f,
			path_sprite = "effects/tile_effects/corrupted_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"corrupted_low",
			"corrupted_high"
		});
		this.add(new TileEffectAsset
		{
			id = "infernal_effect",
			chance = 0.3f,
			path_sprite = "effects/tile_effects/infernal_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"infernal_low",
			"infernal_high"
		});
		this.add(new TileEffectAsset
		{
			id = "wind_effect",
			chance = 0.3f,
			path_sprite = "effects/tile_effects/wind_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"savanna_high",
			"savanna_low"
		});
		this.add(new TileEffectAsset
		{
			id = "birch_effect",
			chance = 0.3f,
			path_sprite = "effects/tile_effects/birch_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"birch_high",
			"birch_low"
		});
		this.add(new TileEffectAsset
		{
			id = "maple_effect",
			chance = 0.3f,
			path_sprite = "effects/tile_effects/maple_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"maple_high",
			"maple_low"
		});
		this.add(new TileEffectAsset
		{
			id = "lava_effect",
			chance = 0.8f,
			path_sprite = "effects/tile_effects/lava_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"lava3",
			"lava2"
		});
		this.add(new TileEffectAsset
		{
			id = "permafrost_effect",
			chance = 0.3f,
			path_sprite = "effects/tile_effects/permafrost_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"permafrost_high",
			"permafrost_low"
		});
		this.add(new TileEffectAsset
		{
			id = "swamp_effect",
			chance = 0.3f,
			path_sprite = "effects/tile_effects/swamp_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"swamp_high",
			"swamp_low"
		});
		this.add(new TileEffectAsset
		{
			id = "sand_effect",
			chance = 0.3f,
			path_sprite = "effects/tile_effects/desert_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"sand"
		});
		this.add(new TileEffectAsset
		{
			id = "mountain_effect",
			chance = 0.3f,
			path_sprite = "effects/tile_effects/wind_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"hills",
			"mountains"
		});
		this.add(new TileEffectAsset
		{
			id = "soil_effect",
			chance = 0.3f,
			path_sprite = "effects/tile_effects/wind_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"soil_high",
			"soil_low"
		});
		this.add(new TileEffectAsset
		{
			id = "clover_effect",
			chance = 0.3f,
			path_sprite = "effects/tile_effects/clover_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"clover_high",
			"clover_low"
		});
		this.add(new TileEffectAsset
		{
			id = "garlic_effect",
			chance = 0.3f,
			path_sprite = "effects/tile_effects/infernal_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"garlic_high",
			"garlic_low"
		});
		this.add(new TileEffectAsset
		{
			id = "flower_effect",
			chance = 0.1f,
			path_sprite = "effects/tile_effects/flower_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"flower_high",
			"flower_low"
		});
		this.add(new TileEffectAsset
		{
			id = "crystal_effect",
			chance = 0.3f,
			path_sprite = "effects/tile_effects/crystal_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"crystal_high",
			"crystal_low"
		});
		this.add(new TileEffectAsset
		{
			id = "jungle_effect",
			chance = 0.3f,
			path_sprite = "effects/tile_effects/jungle_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"jungle_high",
			"jungle_low"
		});
		this.add(new TileEffectAsset
		{
			id = "mushroom_effect",
			chance = 0.3f,
			path_sprite = "effects/tile_effects/mushroom_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"mushroom_high",
			"mushroom_low"
		});
		this.add(new TileEffectAsset
		{
			id = "lemon_effect",
			chance = 0.3f,
			path_sprite = "effects/tile_effects/lemon_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"lemon_high",
			"lemon_low"
		});
		this.add(new TileEffectAsset
		{
			id = "candy_effect",
			chance = 0.1f,
			path_sprite = "effects/tile_effects/candy_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"candy_high",
			"candy_low"
		});
		this.add(new TileEffectAsset
		{
			id = "grass_effect",
			chance = 0.3f,
			path_sprite = "effects/tile_effects/birch_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"grass_high",
			"grass_low"
		});
		this.add(new TileEffectAsset
		{
			id = "wasteland_effect",
			chance = 0.3f,
			path_sprite = "effects/tile_effects/wasteland_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"wasteland_high",
			"wasteland_low"
		});
		this.add(new TileEffectAsset
		{
			id = "rocklands_effect",
			chance = 0.3f,
			path_sprite = "effects/tile_effects/rocklands_effect"
		});
		this.t.addTileTypes(new string[]
		{
			"rocklands_high",
			"rocklands_low"
		});
	}

	// Token: 0x06000767 RID: 1895 RVA: 0x0006D0C5 File Offset: 0x0006B2C5
	public override void linkAssets()
	{
		base.linkAssets();
		this.fillPool();
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x0006D0D4 File Offset: 0x0006B2D4
	private void fillPool()
	{
		foreach (TileEffectAsset tAsset in this.list)
		{
			foreach (string tTileTypeID in tAsset.tile_types)
			{
				this.addToDict(tTileTypeID, tAsset);
			}
		}
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x0006D164 File Offset: 0x0006B364
	private void addToDict(string pTileTypeID, TileEffectAsset pAsset)
	{
		List<TileEffectAsset> tList;
		if (!TileEffectsLibrary._dict_effects.TryGetValue(pTileTypeID, out tList))
		{
			tList = new List<TileEffectAsset>();
			TileEffectsLibrary._dict_effects.Add(pTileTypeID, tList);
		}
		tList.AddTimes(pAsset.rate, pAsset);
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x0006D1A0 File Offset: 0x0006B3A0
	public static TileEffectAsset getRandomEffect(WorldTile pTile)
	{
		List<TileEffectAsset> tList;
		if (!TileEffectsLibrary._dict_effects.TryGetValue(pTile.Type.id, out tList))
		{
			return null;
		}
		return tList.GetRandom<TileEffectAsset>();
	}

	// Token: 0x040007FF RID: 2047
	private static Dictionary<string, List<TileEffectAsset>> _dict_effects = new Dictionary<string, List<TileEffectAsset>>();
}
