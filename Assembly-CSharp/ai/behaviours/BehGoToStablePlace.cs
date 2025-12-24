using System;
using System.Collections.Generic;
using UnityEngine;

namespace ai.behaviours
{
	// Token: 0x020008E1 RID: 2273
	public class BehGoToStablePlace : BehaviourActionActor
	{
		// Token: 0x06004530 RID: 17712 RVA: 0x001D112C File Offset: 0x001CF32C
		public override BehResult execute(Actor pActor)
		{
			TileIsland tCurIsland = pActor.current_tile.region.island;
			if (tCurIsland.isGoodIslandForActor(pActor))
			{
				return BehResult.Stop;
			}
			BehGoToStablePlace.best_region = null;
			BehGoToStablePlace.best_fast_dist = int.MaxValue;
			BehGoToStablePlace.best_tile = null;
			BehGoToStablePlace.best_region = BehGoToStablePlace.findIslandNearby(pActor);
			if (BehGoToStablePlace.best_region != null)
			{
				pActor.beh_tile_target = BehGoToStablePlace.best_region.tiles.GetRandom<WorldTile>();
				BehGoToStablePlace.best_tile = pActor.beh_tile_target;
				return BehResult.Continue;
			}
			BehGoToStablePlace.bestRegions.Clear();
			Vector2Int tActorPos = pActor.current_tile.pos;
			for (int i = 0; i < BehaviourActionBase<Actor>.world.islands_calculator.islands.Count; i++)
			{
				TileIsland tIsland = BehaviourActionBase<Actor>.world.islands_calculator.islands[i];
				if (BehGoToStablePlace.checkIsland(tCurIsland, tIsland, pActor))
				{
					HashSet<MapRegion> tLessBorderRegions;
					HashSet<MapRegion> tMoreBorderRegions;
					BehGoToStablePlace.selectBorderRegionsForComparison(tIsland, tCurIsland, out tLessBorderRegions, out tMoreBorderRegions);
					MapRegion tBest = null;
					int tBestFastDist = int.MaxValue;
					foreach (MapRegion tRegion in tLessBorderRegions)
					{
						if (tMoreBorderRegions.Contains(tRegion))
						{
							int tFastDist = Toolbox.SquaredDist(tActorPos.x, tActorPos.y, tRegion.tiles[0].pos.x, tRegion.tiles[0].pos.y);
							if (tFastDist < tBestFastDist)
							{
								tBestFastDist = tFastDist;
								tBest = tRegion;
							}
						}
					}
					if (tBest != null)
					{
						MapRegion tCornerRegion = tBest;
						List<WorldTile> tListEdges = tCornerRegion.getEdgeTiles();
						if (tListEdges.Count != 0)
						{
							float tDist = Toolbox.DistTile(pActor.current_tile, tListEdges.GetRandom<WorldTile>());
							if (BehGoToStablePlace.bestRegions.Count <= 0 || (float)(BehGoToStablePlace.bestRegions[0].Key + 15) >= tDist)
							{
								if (BehGoToStablePlace.bestRegions.Count < 4)
								{
									BehGoToStablePlace.bestRegions.Add(new KeyValuePair<int, MapRegion>((int)tDist, tCornerRegion));
								}
								else
								{
									BehGoToStablePlace.bestRegions.Sort((KeyValuePair<int, MapRegion> x, KeyValuePair<int, MapRegion> y) => x.Key.CompareTo(y.Key));
									if ((float)BehGoToStablePlace.bestRegions[3].Key > tDist)
									{
										BehGoToStablePlace.bestRegions[3] = new KeyValuePair<int, MapRegion>((int)tDist, tCornerRegion);
									}
								}
							}
						}
					}
				}
			}
			BehGoToStablePlace.bestRegions.RemoveAll((KeyValuePair<int, MapRegion> x) => x.Key - 15 > BehGoToStablePlace.bestRegions[0].Key);
			if (Randy.randomChance(0.8f) && BehGoToStablePlace.bestRegions.Count > 0)
			{
				pActor.beh_tile_target = BehGoToStablePlace.bestRegions.GetRandom<KeyValuePair<int, MapRegion>>().Value.tiles.GetRandom<WorldTile>();
			}
			else
			{
				MapRegion tRegionN;
				if (Randy.randomChance(0.5f))
				{
					tRegionN = pActor.current_tile.region;
				}
				else
				{
					tRegionN = Randy.getRandom<MapRegion>(pActor.current_tile.region.neighbours);
				}
				if (tRegionN != null)
				{
					pActor.beh_tile_target = Randy.getRandom<WorldTile>(tRegionN.tiles);
				}
			}
			if (!DebugConfig.isOn(DebugOption.ShowSwimToIslandLogic))
			{
				BehGoToStablePlace.bestRegions.Clear();
			}
			else
			{
				BehGoToStablePlace.best_tile = pActor.beh_tile_target;
			}
			return BehResult.Continue;
		}

		// Token: 0x06004531 RID: 17713 RVA: 0x001D145C File Offset: 0x001CF65C
		private static MapRegion findIslandNearby(Actor pActor)
		{
			ValueTuple<MapChunk[], int> allChunksFromTile = Toolbox.getAllChunksFromTile(pActor.current_tile);
			MapChunk[] tChunks = allChunksFromTile.Item1;
			int tLength = allChunksFromTile.Item2;
			TileIsland tCurIsland = pActor.current_tile.region.island;
			for (int i = 0; i < tLength; i++)
			{
				MapChunk tChunk = tChunks[i];
				for (int j = 0; j < tChunk.regions.Count; j++)
				{
					MapRegion tReg = tChunk.regions[j];
					if (BehGoToStablePlace.checkIsland(tCurIsland, tReg.island, pActor))
					{
						List<WorldTile> tListCorners = tReg.getEdgeTiles();
						if (tListCorners.Count != 0)
						{
							WorldTile tClosestCornerTile = Toolbox.getClosestTile(tListCorners, pActor.current_tile);
							int tFastDist = Toolbox.SquaredDistTile(pActor.current_tile, tClosestCornerTile);
							if (tFastDist < BehGoToStablePlace.best_fast_dist)
							{
								BehGoToStablePlace.best_region = tReg;
								BehGoToStablePlace.best_fast_dist = tFastDist;
							}
						}
					}
				}
			}
			return BehGoToStablePlace.best_region;
		}

		// Token: 0x06004532 RID: 17714 RVA: 0x001D1530 File Offset: 0x001CF730
		private static bool checkIsland(TileIsland pCurrentIsland, TileIsland pIsland, Actor pActor)
		{
			if (pCurrentIsland == pIsland)
			{
				return false;
			}
			if (!pIsland.isGoodIslandForActor(pActor))
			{
				return false;
			}
			bool tConnected;
			if (pCurrentIsland.getTileCount() > pIsland.getTileCount())
			{
				tConnected = pIsland.isConnectedWith(pCurrentIsland);
			}
			else
			{
				tConnected = pCurrentIsland.isConnectedWith(pIsland);
			}
			return tConnected && pIsland.insideRegionEdges.Count != 0;
		}

		// Token: 0x06004533 RID: 17715 RVA: 0x001D1584 File Offset: 0x001CF784
		private static void selectBorderRegionsForComparison(TileIsland pIsland1, TileIsland pIsland2, out HashSet<MapRegion> pOut1, out HashSet<MapRegion> pOut2)
		{
			if (pIsland1.outsideRegionEdges.Count + pIsland2.insideRegionEdges.Count < pIsland1.insideRegionEdges.Count + pIsland2.outsideRegionEdges.Count)
			{
				if (pIsland1.outsideRegionEdges.Count > pIsland2.insideRegionEdges.Count)
				{
					pOut1 = pIsland2.insideRegionEdges;
					pOut2 = pIsland1.outsideRegionEdges;
					return;
				}
				pOut2 = pIsland2.insideRegionEdges;
				pOut1 = pIsland1.outsideRegionEdges;
				return;
			}
			else
			{
				if (pIsland1.insideRegionEdges.Count > pIsland2.outsideRegionEdges.Count)
				{
					pOut1 = pIsland2.outsideRegionEdges;
					pOut2 = pIsland1.insideRegionEdges;
					return;
				}
				pOut2 = pIsland2.outsideRegionEdges;
				pOut1 = pIsland1.insideRegionEdges;
				return;
			}
		}

		// Token: 0x04003194 RID: 12692
		private static MapRegion best_region;

		// Token: 0x04003195 RID: 12693
		private static int best_fast_dist = int.MaxValue;

		// Token: 0x04003196 RID: 12694
		internal static List<KeyValuePair<int, MapRegion>> bestRegions = new List<KeyValuePair<int, MapRegion>>(4);

		// Token: 0x04003197 RID: 12695
		internal static WorldTile best_tile = null;

		// Token: 0x04003198 RID: 12696
		private const int MAX_DISTANCE = 15;
	}
}
