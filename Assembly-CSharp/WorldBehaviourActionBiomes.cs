using System;
using System.Collections.Generic;

// Token: 0x02000301 RID: 769
public class WorldBehaviourActionBiomes : WorldBehaviourTilesRunner
{
	// Token: 0x06001D25 RID: 7461 RVA: 0x0010522A File Offset: 0x0010342A
	public static void clear()
	{
		WorldBehaviourTilesRunner.clearTilesToCheck();
		WorldBehaviourActionBiomes._bonus_tiles.Clear();
	}

	// Token: 0x06001D26 RID: 7462 RVA: 0x0010523B File Offset: 0x0010343B
	public static void update()
	{
		AchievementLibrary.the_hell.check(null);
		if (!WorldLawLibrary.world_law_grow_grass.isEnabled())
		{
			return;
		}
		if (World.world.era_manager.isWinter())
		{
			return;
		}
		WorldBehaviourActionBiomes.updateSingleTiles();
	}

	// Token: 0x06001D27 RID: 7463 RVA: 0x00105270 File Offset: 0x00103470
	public static void updateSingleTiles()
	{
		WorldBehaviourTilesRunner.checkTiles();
		int tMax = World.world.map_chunk_manager.amount_x * (SimGlobals.m.biomes_growth_speed + World.world_era.bonus_biomes_growth);
		if (tMax <= 0)
		{
			return;
		}
		WorldTile[] tTilesToCheck = WorldBehaviourTilesRunner._tiles_to_check;
		if (WorldBehaviourTilesRunner._tile_next_check + tMax >= tTilesToCheck.Length)
		{
			tMax = tTilesToCheck.Length - WorldBehaviourTilesRunner._tile_next_check;
		}
		while (tMax-- > 0)
		{
			WorldBehaviourTilesRunner._tiles_to_check.ShuffleOne(WorldBehaviourTilesRunner._tile_next_check);
			WorldTile tSpreaderTile = tTilesToCheck[WorldBehaviourTilesRunner._tile_next_check++];
			if (tSpreaderTile.Type.is_biome && tSpreaderTile.burned_stages <= 0)
			{
				WorldBehaviourActionBiomes.trySpreadBiomeAround(tSpreaderTile, tSpreaderTile, true, true, false);
			}
		}
		WorldBehaviourActionBiomes.checkBonusTilesGrowth();
	}

	// Token: 0x06001D28 RID: 7464 RVA: 0x00105318 File Offset: 0x00103518
	private static void checkBonusTilesGrowth()
	{
		if (WorldBehaviourActionBiomes._bonus_tiles.Count == 0)
		{
			return;
		}
		foreach (WorldTile worldTile in WorldBehaviourActionBiomes._bonus_tiles)
		{
			WorldBehaviourActionBiomes.trySpreadBiomeAround(worldTile, worldTile, false, false, false);
		}
		WorldBehaviourActionBiomes._bonus_tiles.Clear();
	}

	// Token: 0x06001D29 RID: 7465 RVA: 0x00105384 File Offset: 0x00103584
	public static void trySpreadBiomeAround(WorldTile pAroundTile, WorldTile pSpreader, bool pCheckRoad = false, bool pCheckBonuses = false, bool pForce = false)
	{
		WorldBehaviourActionBiomes.trySpreadBiomeAround(pAroundTile, pSpreader.Type, pCheckRoad, pCheckBonuses, pForce, false);
	}

	// Token: 0x06001D2A RID: 7466 RVA: 0x00105398 File Offset: 0x00103598
	public static void trySpreadBiomeAround(WorldTile pAroundTile, TileTypeBase pSpreadType, bool pCheckRoad = false, bool pCheckBonuses = false, bool pForce = false, bool pSkipEraCheck = false)
	{
		if (!pSpreadType.is_biome)
		{
			return;
		}
		BiomeAsset tSpreaderBiome = pSpreadType.biome_asset;
		bool tIgnoreBurnedStages = tSpreaderBiome.spread_ignore_burned_stages;
		if (!tSpreaderBiome.spread_biome)
		{
			return;
		}
		if (!pSkipEraCheck && !string.IsNullOrEmpty(tSpreaderBiome.spread_only_in_era) && World.world_era.id != tSpreaderBiome.spread_only_in_era)
		{
			return;
		}
		for (int i = 0; i < pAroundTile.neighbours.Length; i++)
		{
			WorldTile tNeighbour = pAroundTile.neighbours[i];
			if (tIgnoreBurnedStages || tNeighbour.burned_stages <= 0)
			{
				if (tNeighbour.Type.road && pCheckRoad)
				{
					WorldBehaviourActionBiomes.trySpreadBiomeAround(tNeighbour, pSpreadType, false, false, false, false);
				}
				if (tNeighbour.Type.can_be_biome)
				{
					BiomeAsset tNeighbourBiome = tNeighbour.Type.biome_asset;
					if (tNeighbourBiome != tSpreaderBiome)
					{
						bool tAdd = false;
						if (tNeighbour.Type.soil || pForce)
						{
							tAdd = true;
						}
						else if (WorldLawLibrary.world_law_biome_overgrowth.isEnabled())
						{
							if (tNeighbourBiome.grow_strength == tSpreaderBiome.grow_strength)
							{
								if (Randy.randomChance(0.05f))
								{
									tAdd = true;
								}
							}
							else
							{
								int num = Randy.randomInt(0, tSpreaderBiome.grow_strength);
								int tOldRoll = Randy.randomInt(0, tNeighbourBiome.grow_strength);
								if (num > tOldRoll)
								{
									tAdd = true;
								}
							}
						}
						if (tAdd)
						{
							MusicBox.playSound("event:/SFX/NATURE/GrassSpawn", tNeighbour, true, true);
							TopTileType tBiomeTileType = tSpreaderBiome.getTile(tNeighbour);
							MapAction.growGreens(tNeighbour, tBiomeTileType);
							if (pCheckBonuses)
							{
								HashSet<string> biomes = World.world_era.biomes;
								if (biomes != null && biomes.Contains(tNeighbour.Type.biome_id))
								{
									WorldBehaviourActionBiomes._bonus_tiles.Add(tNeighbour);
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x040015FD RID: 5629
	private static List<WorldTile> _bonus_tiles = new List<WorldTile>();
}
