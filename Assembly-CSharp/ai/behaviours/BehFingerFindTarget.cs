using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x0200095C RID: 2396
	public class BehFingerFindTarget : BehFinger
	{
		// Token: 0x0600466E RID: 18030 RVA: 0x001DDAC4 File Offset: 0x001DBCC4
		public override BehResult execute(Actor pActor)
		{
			pActor.findCurrentTile(false);
			BehFingerFindTarget.clearTargets(this.finger);
			if (this.finger.target_tiles.Count == 0)
			{
				this.finger.finger_target = this.fillRandomTiles(pActor.current_tile, this.finger.target_tiles);
			}
			pActor.beh_tile_target = this.finger.target_tiles.GetRandom<WorldTile>();
			return BehResult.Continue;
		}

		// Token: 0x0600466F RID: 18031 RVA: 0x001DDB30 File Offset: 0x001DBD30
		private FingerTarget fillRandomTiles(WorldTile pTile, HashSet<WorldTile> pTargetTiles)
		{
			float tRatio = BehaviourActionBase<Actor>.world.islands_calculator.groundIslandRatio() * 4f;
			int tPitCount = TileLibrary.pit_deep_ocean.hashset.Count + TileLibrary.pit_close_ocean.hashset.Count + TileLibrary.pit_shallow_waters.hashset.Count;
			if (tPitCount > 20 && Randy.randomChance(0.95f))
			{
				using (ListPool<WorldTile> tSet = new ListPool<WorldTile>(tPitCount))
				{
					tSet.AddRange(TileLibrary.pit_deep_ocean.hashset);
					tSet.AddRange(TileLibrary.pit_close_ocean.hashset);
					tSet.AddRange(TileLibrary.pit_shallow_waters.hashset);
					Toolbox.sortTilesByDistance(pTile, tSet);
					tSet.Clear(10);
					WorldTile tTargetTile = tSet.GetRandom<WorldTile>();
					ValueTuple<MapChunk[], int> allChunksFromTile = Toolbox.getAllChunksFromTile(tTargetTile);
					MapChunk[] tChunks = allChunksFromTile.Item1;
					int tLength = allChunksFromTile.Item2;
					bool tFull = false;
					for (int i = 0; i < tLength; i++)
					{
						foreach (WorldTile tTile in tChunks[i].tiles)
						{
							if (tTile.Type.IsType(tTargetTile.Type) && Randy.randomChance(0.65f))
							{
								pTargetTiles.Add(tTile);
								if (pTargetTiles.Count >= 1200)
								{
									tFull = true;
									break;
								}
							}
						}
						if (tFull)
						{
							break;
						}
					}
					return FingerTarget.Water;
				}
			}
			if (BehaviourActionBase<Actor>.world.islands_calculator.hasNonGround() && Randy.randomChance(0.95f * tRatio))
			{
				TileIsland tIsland;
				if (pTile.region.island.type == TileLayerType.Ocean && Randy.randomChance(0.6f))
				{
					tIsland = pTile.region.island;
				}
				else
				{
					tIsland = BehaviourActionBase<Actor>.world.islands_calculator.getRandomIslandNonGroundWeighted(true);
					if (tIsland == null)
					{
						tIsland = BehaviourActionBase<Actor>.world.islands_calculator.getRandomIslandNonGround(false);
					}
					pTile = tIsland.getRandomTile();
				}
				foreach (MapRegion tRegion in tIsland.regions.getSimpleList().LoopRandom<MapRegion>())
				{
					if (pTile.region == tRegion || pTile.region.hasNeighbour(tRegion))
					{
						foreach (WorldTile tTile2 in tRegion.tiles.LoopRandom<WorldTile>())
						{
							if (Randy.randomChance(0.65f))
							{
								pTargetTiles.Add(tTile2);
							}
						}
						if (pTargetTiles.Count >= 1200)
						{
							break;
						}
					}
				}
				return FingerTarget.Water;
			}
			if (BehaviourActionBase<Actor>.world.islands_calculator.hasGround() && Randy.randomChance(0.95f))
			{
				TileIsland tIsland;
				if (pTile.region.island.type == TileLayerType.Ground && Randy.randomChance(0.6f))
				{
					tIsland = pTile.region.island;
				}
				else
				{
					tIsland = BehaviourActionBase<Actor>.world.islands_calculator.getRandomIslandGroundWeighted(true);
					if (tIsland == null)
					{
						tIsland = BehaviourActionBase<Actor>.world.islands_calculator.getRandomIslandGround(false);
					}
					pTile = tIsland.getRandomTile();
				}
				foreach (MapRegion tRegion2 in tIsland.regions.getSimpleList().LoopRandom<MapRegion>())
				{
					if (pTile.region == tRegion2 || pTile.region.hasNeighbour(tRegion2))
					{
						foreach (WorldTile tTile3 in tRegion2.tiles.LoopRandom<WorldTile>())
						{
							if (Randy.randomChance(0.65f))
							{
								pTargetTiles.Add(tTile3);
							}
						}
						if (pTargetTiles.Count >= 1200)
						{
							break;
						}
					}
				}
				return FingerTarget.Ground;
			}
			WorldTile tTargetTile2 = Toolbox.getRandomTileWithinDistance(pTile, 75);
			foreach (MapRegion tRegion3 in tTargetTile2.region.island.regions.getSimpleList().LoopRandom<MapRegion>())
			{
				if (tTargetTile2.region == tRegion3 || tTargetTile2.region.hasNeighbour(tRegion3))
				{
					foreach (WorldTile tTile4 in tRegion3.tiles.LoopRandom<WorldTile>())
					{
						if (Randy.randomChance(0.65f))
						{
							pTargetTiles.Add(tTile4);
						}
					}
					if (pTargetTiles.Count >= 1200)
					{
						break;
					}
				}
			}
			return BehFingerFindTarget.getFingerTarget(tTargetTile2);
		}

		// Token: 0x06004670 RID: 18032 RVA: 0x001DE010 File Offset: 0x001DC210
		private static FingerTarget getFingerTarget(WorldTile pTile)
		{
			if (pTile.Type.layer_type == TileLayerType.Ocean || pTile.Type.can_be_filled_with_ocean)
			{
				return FingerTarget.Water;
			}
			return FingerTarget.Ground;
		}

		// Token: 0x06004671 RID: 18033 RVA: 0x001DE030 File Offset: 0x001DC230
		private static void clearTargets(GodFinger pFinger)
		{
			if (pFinger.finger_target == FingerTarget.None)
			{
				return;
			}
			if (pFinger.drawing_over_water)
			{
				pFinger.target_tiles.RemoveWhere((WorldTile x) => x.Type.layer_type != TileLayerType.Ocean && !x.Type.can_be_filled_with_ocean);
			}
			if (pFinger.drawing_over_ground)
			{
				pFinger.target_tiles.RemoveWhere((WorldTile x) => x.Type.layer_type != TileLayerType.Ground);
			}
		}

		// Token: 0x040031EA RID: 12778
		private const float RANDOM_CHANCE_ADD_TILE = 0.65f;

		// Token: 0x040031EB RID: 12779
		private const float RANDOM_CHANCE_FIND_PITS = 0.95f;

		// Token: 0x040031EC RID: 12780
		private const float RANDOM_CHANCE_FIND_WATER = 0.95f;

		// Token: 0x040031ED RID: 12781
		private const float RANDOM_CHANCE_FIND_GROUND = 0.95f;

		// Token: 0x040031EE RID: 12782
		private const float RANDOM_CHANCE_USE_CURRENT_ISLAND = 0.6f;
	}
}
