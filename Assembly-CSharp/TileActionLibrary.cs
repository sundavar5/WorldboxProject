using System;
using System.Collections.Generic;

// Token: 0x0200007B RID: 123
public static class TileActionLibrary
{
	// Token: 0x06000460 RID: 1120 RVA: 0x0002DD14 File Offset: 0x0002BF14
	public static bool damage(WorldTile pTile, Actor pActor)
	{
		throw new NotImplementedException("damage logic per tile not implemented yet");
	}

	// Token: 0x06000461 RID: 1121 RVA: 0x0002DD20 File Offset: 0x0002BF20
	public static bool landmine(WorldTile pTile, Actor pActor)
	{
		if (pActor.isFlying())
		{
			return false;
		}
		if (pActor.isInAir() || pActor.isHovering())
		{
			return false;
		}
		if (pActor.hasTrait("weightless"))
		{
			return false;
		}
		World.world.explosion_layer.explodeBomb(pTile, false);
		return true;
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x0002DD5F File Offset: 0x0002BF5F
	public static bool setUnitOnFire(WorldTile pTile, Actor pActor)
	{
		if (pActor.hasTrait("burning_feet"))
		{
			return false;
		}
		pActor.addStatusEffect("burning", 0f, true);
		pTile.startFire(true);
		return true;
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x0002DD8B File Offset: 0x0002BF8B
	public static bool giveTumorTrait(WorldTile pTile, Actor pActor)
	{
		if (pActor.asset.immune_to_tumor)
		{
			return false;
		}
		if (!pActor.asset.can_turn_into_tumor)
		{
			return false;
		}
		pActor.addTrait("tumor_infection", false);
		return true;
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x0002DDB9 File Offset: 0x0002BFB9
	public static bool giveSlownessStatus(WorldTile pTile, Actor pActor)
	{
		if (pActor.asset.immune_to_slowness)
		{
			return false;
		}
		pActor.addStatusEffect("slowness", 0f, true);
		return true;
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x0002DDDD File Offset: 0x0002BFDD
	public static bool giveMadnessTrait(WorldTile pTile, Actor pActor)
	{
		if (pActor.asset.immune_to_tumor)
		{
			return false;
		}
		if (!pActor.asset.can_turn_into_tumor)
		{
			return false;
		}
		pActor.addTrait("madness", false);
		return true;
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x0002DE0C File Offset: 0x0002C00C
	public static BuildingAsset getGrowTypeRandomMineral(WorldTile pTile)
	{
		BiomeAsset tBiomeAsset = pTile.Type.biome_asset;
		if (tBiomeAsset == null)
		{
			return null;
		}
		if (tBiomeAsset.pot_minerals_spawn == null)
		{
			return null;
		}
		string tID = tBiomeAsset.pot_minerals_spawn.GetRandom<string>();
		return AssetManager.buildings.get(tID);
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x0002DE4C File Offset: 0x0002C04C
	public static BuildingAsset getGrowTypeRandomTrees(WorldTile pTile)
	{
		BiomeAsset tBiomeAsset = pTile.Type.biome_asset;
		if (tBiomeAsset == null)
		{
			return null;
		}
		return TileActionLibrary.getRandomAssetFromPot(tBiomeAsset.pot_trees_spawn);
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x0002DE78 File Offset: 0x0002C078
	public static BuildingAsset getGrowTypeRandomPlants(WorldTile pTile)
	{
		BiomeAsset tBiomeAsset = pTile.Type.biome_asset;
		if (tBiomeAsset == null)
		{
			return null;
		}
		return TileActionLibrary.getRandomAssetFromPot(tBiomeAsset.pot_plants_spawn);
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x0002DEA4 File Offset: 0x0002C0A4
	public static BuildingAsset getGrowTypeRandomBushes(WorldTile pTile)
	{
		BiomeAsset tBiomeAsset = pTile.Type.biome_asset;
		if (tBiomeAsset == null)
		{
			return null;
		}
		return TileActionLibrary.getRandomAssetFromPot(tBiomeAsset.pot_bushes_spawn);
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x0002DED0 File Offset: 0x0002C0D0
	private static BuildingAsset getRandomAssetFromPot(List<string> pPotList)
	{
		if (pPotList == null)
		{
			return null;
		}
		string tID = pPotList.GetRandom<string>();
		return AssetManager.buildings.get(tID);
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x0002DEF4 File Offset: 0x0002C0F4
	public static BuildingAsset getGrowTypeSand(WorldTile pTile)
	{
		bool tHasWater = pTile.zone.hasLiquid();
		if (!tHasWater)
		{
			TileZone[] tNeighbours = pTile.zone.neighbours_all;
			for (int i = 0; i < tNeighbours.Length; i++)
			{
				tHasWater = tNeighbours[i].hasLiquid();
				if (tHasWater)
				{
					break;
				}
			}
		}
		if (tHasWater)
		{
			return AssetManager.buildings.get("palm_tree");
		}
		return AssetManager.buildings.get("cacti_tree");
	}
}
