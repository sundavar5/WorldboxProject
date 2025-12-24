using System;

// Token: 0x02000233 RID: 563
public static class BuildingActions
{
	// Token: 0x060015B3 RID: 5555 RVA: 0x000DF620 File Offset: 0x000DD820
	public static void tryGrowVegetationRandom(WorldTile pTile, VegetationType pType, bool pOnStart = false, bool pCheckLimit = true, bool pCheckRandom = true)
	{
		BiomeAsset tBiome = pTile.Type.biome_asset;
		if (tBiome == null)
		{
			return;
		}
		if (!tBiome.grow_vegetation_auto)
		{
			return;
		}
		BuildingAsset tAsset = null;
		if (pType == VegetationType.Plants)
		{
			if (tBiome.grow_type_selector_plants != null)
			{
				tAsset = tBiome.grow_type_selector_plants(pTile);
			}
		}
		else if (pType == VegetationType.Trees)
		{
			if (tBiome.grow_type_selector_trees != null)
			{
				tAsset = tBiome.grow_type_selector_trees(pTile);
			}
		}
		else if (pType == VegetationType.Bushes && tBiome.grow_type_selector_bushes != null)
		{
			tAsset = tBiome.grow_type_selector_bushes(pTile);
		}
		if (tAsset == null)
		{
			return;
		}
		if (tAsset.limit_in_radius > 0)
		{
			pCheckLimit = true;
		}
		if (pCheckLimit && pTile.zone.hasReachedBuildingLimit(pTile, tAsset))
		{
			return;
		}
		if (pCheckRandom && tAsset.vegetation_random_chance < Randy.random())
		{
			return;
		}
		if (!World.world.buildings.canBuildFrom(pTile, tAsset, null, BuildPlacingType.New, false))
		{
			return;
		}
		World.world.buildings.addBuilding(tAsset, pTile, false, false, BuildPlacingType.New);
		if (tAsset.flora_type == FloraType.Tree)
		{
			World.world.game_stats.data.treesGrown += 1L;
		}
		else if (tAsset.flora_type == FloraType.Plant || tAsset.flora_type == FloraType.Fungi)
		{
			World.world.game_stats.data.floraGrown += 1L;
		}
		if (tAsset.has_sound_spawn)
		{
			MusicBox.playSound(tAsset.sound_spawn, pTile, true, true);
		}
	}

	// Token: 0x060015B4 RID: 5556 RVA: 0x000DF764 File Offset: 0x000DD964
	public static void tryGrowMineralRandom(WorldTile pTile, bool pOnStart = false, bool pCheckLimit = true)
	{
		BiomeAsset tBiome = pTile.getBiome();
		if (tBiome == null)
		{
			return;
		}
		if (!tBiome.grow_minerals_auto)
		{
			return;
		}
		if (pTile.hasBuilding() && pTile.building.isUsable())
		{
			return;
		}
		BuildingAsset tTempAsset = tBiome.grow_type_selector_minerals(pTile);
		if (tTempAsset == null)
		{
			return;
		}
		if (pCheckLimit && pTile.zone.hasReachedBuildingLimit(pTile, tTempAsset))
		{
			return;
		}
		if (!World.world.buildings.canBuildFrom(pTile, tTempAsset, null, BuildPlacingType.New, false))
		{
			return;
		}
		World.world.buildings.addBuilding(tTempAsset, pTile, false, false, BuildPlacingType.New);
	}

	// Token: 0x060015B5 RID: 5557 RVA: 0x000DF7EC File Offset: 0x000DD9EC
	public static Building tryGrowVegetation(WorldTile pTile, string pTemplateID, bool pSfx = false, bool pCheckLimit = true)
	{
		BuildingAsset tTempPlant = AssetManager.buildings.get(pTemplateID);
		if (pTile.hasBuilding() && pTile.building.isUsable())
		{
			return null;
		}
		if (tTempPlant == null)
		{
			return null;
		}
		if (pCheckLimit && pTile.zone.hasReachedBuildingLimit(pTile, tTempPlant))
		{
			return null;
		}
		if (!World.world.buildings.canBuildFrom(pTile, tTempPlant, null, BuildPlacingType.New, false))
		{
			return null;
		}
		Building result = World.world.buildings.addBuilding(tTempPlant, pTile, false, pSfx, BuildPlacingType.New);
		World.world.game_stats.data.floraGrown += 1L;
		return result;
	}

	// Token: 0x060015B6 RID: 5558 RVA: 0x000DF880 File Offset: 0x000DDA80
	public static void spawnBeehives(int pAmount)
	{
		for (int i = 0; i < pAmount; i++)
		{
			WorldTile tTile = World.world.tiles_list.GetRandom<WorldTile>();
			if (tTile.Type.grass)
			{
				World.world.buildings.addBuilding("beehive", tTile, true, false, BuildPlacingType.New);
			}
		}
	}

	// Token: 0x060015B7 RID: 5559 RVA: 0x000DF8D0 File Offset: 0x000DDAD0
	public static void spawnResource(int pAmount, string pType, bool pRandomSize = true)
	{
		for (int i = 0; i < pAmount; i++)
		{
			WorldTile tTile = World.world.tiles_list.GetRandom<WorldTile>();
			if (tTile.Type.ground)
			{
				World.world.buildings.addBuilding(pType, tTile, true, false, BuildPlacingType.New);
			}
		}
	}
}
