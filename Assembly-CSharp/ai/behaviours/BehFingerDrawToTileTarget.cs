using System;
using System.Collections.Generic;
using UnityPools;

namespace ai.behaviours
{
	// Token: 0x02000959 RID: 2393
	public class BehFingerDrawToTileTarget : BehFingerDrawAction
	{
		// Token: 0x06004665 RID: 18021 RVA: 0x001DD3DE File Offset: 0x001DB5DE
		public BehFingerDrawToTileTarget()
		{
			this.drawing_action = true;
		}

		// Token: 0x06004666 RID: 18022 RVA: 0x001DD3F0 File Offset: 0x001DB5F0
		public override BehResult execute(Actor pActor)
		{
			BehFingerDrawToTileTarget.pickBrush(this.finger);
			BehFingerDrawToTileTarget.pickPower(this.finger);
			BehResult result;
			using (ListPool<WorldTile> tTiles = new ListPool<WorldTile>(this.finger.target_tiles))
			{
				ExecuteEvent tResult;
				if (pActor.current_tile == pActor.beh_tile_target || Toolbox.DistTile(pActor.current_tile, pActor.beh_tile_target) < 6f)
				{
					tResult = ActorMove.goToCurved(pActor, new WorldTile[]
					{
						pActor.current_tile,
						pActor.current_tile.neighboursAll.GetRandom<WorldTile>(),
						pActor.current_tile.neighboursAll.GetRandom<WorldTile>(),
						pActor.beh_tile_target.neighboursAll.GetRandom<WorldTile>(),
						pActor.beh_tile_target.neighboursAll.GetRandom<WorldTile>(),
						pActor.beh_tile_target
					});
				}
				else if (this.finger.target_tiles.Count > 10)
				{
					WorldTile tWayPoint = Toolbox.getRandomTileWithinDistance(pActor.current_tile, 25, tTiles);
					WorldTile tWayPoint2 = Toolbox.getRandomTileWithinDistance(tWayPoint, 25, tTiles);
					WorldTile tWayPoint3 = Toolbox.getRandomTileWithinDistance(pActor.beh_tile_target, 25, tTiles);
					WorldTile tWayPoint4 = Toolbox.getRandomTileWithinDistance(tWayPoint3, 25, tTiles);
					tResult = ActorMove.goToCurved(pActor, new WorldTile[]
					{
						pActor.current_tile,
						tWayPoint,
						tWayPoint2,
						tWayPoint4,
						tWayPoint3,
						pActor.beh_tile_target
					});
				}
				else
				{
					tResult = ActorMove.goToCurved(pActor, new WorldTile[]
					{
						pActor.current_tile,
						pActor.beh_tile_target
					});
				}
				pActor.timer_action = 0.5f;
				if (tResult == ExecuteEvent.False)
				{
					result = BehResult.Stop;
				}
				else
				{
					result = BehResult.Continue;
				}
			}
			return result;
		}

		// Token: 0x06004667 RID: 18023 RVA: 0x001DD598 File Offset: 0x001DB798
		private static void pickBrush(GodFinger pFinger)
		{
			if (pFinger.target_tiles.Count > 0)
			{
				int tMinSize = pFinger.target_tiles.Count / 10;
				int tMaxSize = pFinger.target_tiles.Count / 3;
				pFinger.brush = Brush.getRandom(tMinSize, tMaxSize, new Predicate<BrushData>(BehFingerDrawToTileTarget.brushFilter));
			}
		}

		// Token: 0x06004668 RID: 18024 RVA: 0x001DD5E9 File Offset: 0x001DB7E9
		private static bool brushFilter(BrushData pBrush)
		{
			return pBrush.id.StartsWith("circ_") || pBrush.id.StartsWith("special_");
		}

		// Token: 0x06004669 RID: 18025 RVA: 0x001DD610 File Offset: 0x001DB810
		private static void pickPower(GodFinger pFinger)
		{
			bool tDrawingOverGround = pFinger.drawing_over_ground;
			bool tDrawingOverWater = pFinger.drawing_over_water;
			HashSet<WorldTile> tTargetTiles = pFinger.target_tiles;
			if (pFinger.god_power != null)
			{
				if (tDrawingOverWater && GodFinger.power_over_water.Contains(pFinger.god_power.id))
				{
					return;
				}
				if (tDrawingOverGround && GodFinger.power_over_ground.Contains(pFinger.god_power.id))
				{
					return;
				}
			}
			Dictionary<string, int> tNeighbourBiomes = UnsafeCollectionPool<Dictionary<string, int>, KeyValuePair<string, int>>.Get();
			Dictionary<TileTypeBase, int> tNeighbourTypes = UnsafeCollectionPool<Dictionary<TileTypeBase, int>, KeyValuePair<TileTypeBase, int>>.Get();
			HashSet<WorldTile> tNeighbourTiles = UnsafeCollectionPool<HashSet<WorldTile>, WorldTile>.Get();
			int tNeighbourTypesCount = 0;
			foreach (WorldTile worldTile in tTargetTiles)
			{
				foreach (WorldTile tNTile in worldTile.neighboursAll)
				{
					if (!tTargetTiles.Contains(tNTile) && tNeighbourTiles.Add(tNTile))
					{
						tNeighbourTypesCount++;
						int tCurrentCount;
						tNeighbourTypes.TryGetValue(tNTile.Type, out tCurrentCount);
						tCurrentCount = (tNeighbourTypes[tNTile.Type] = tCurrentCount + 1);
						if (tDrawingOverGround)
						{
							BiomeAsset tBiome = tNTile.Type.biome_asset;
							if (tBiome != null && !tBiome.special_biome)
							{
								int tCurrentCountHigh;
								tNeighbourBiomes.TryGetValue(tBiome.tile_high, out tCurrentCountHigh);
								tCurrentCountHigh = (tNeighbourBiomes[tBiome.tile_high] = tCurrentCountHigh + 1);
								int tCurrentCountLow;
								tNeighbourBiomes.TryGetValue(tBiome.tile_low, out tCurrentCountLow);
								tCurrentCountLow = (tNeighbourBiomes[tBiome.tile_low] = tCurrentCountLow + 1);
							}
						}
					}
				}
			}
			if (tDrawingOverWater)
			{
				using (ListPool<string> tList = new ListPool<string>(tNeighbourTypesCount))
				{
					foreach (string tPowerID in GodFinger.power_over_water)
					{
						GodPower godPower = AssetManager.powers.get(tPowerID);
						bool tDropTypeFound = false;
						TileType tTileAsset = godPower.cached_tile_type_asset;
						if (tTileAsset != null)
						{
							tDropTypeFound = true;
							int tCount;
							if (tNeighbourTypes.TryGetValue(tTileAsset, out tCount))
							{
								tList.AddTimes(tCount, tPowerID);
							}
						}
						TopTileType tTopTileAsset = godPower.cached_top_tile_type_asset;
						if (tTopTileAsset != null)
						{
							tDropTypeFound = true;
							int tCount2;
							if (tNeighbourTypes.TryGetValue(tTopTileAsset, out tCount2))
							{
								tList.AddTimes(tCount2, tPowerID);
							}
						}
						if (!tDropTypeFound)
						{
							tList.Add(tPowerID);
						}
					}
					string _random_power = Randy.getRandom<string>(tList) ?? Randy.getRandom<string>(GodFinger.power_over_water);
					pFinger.god_power = AssetManager.powers.get(_random_power);
					goto IL_384;
				}
			}
			if (tDrawingOverGround)
			{
				using (ListPool<string> tList2 = new ListPool<string>(tNeighbourTypesCount))
				{
					foreach (string tPowerID2 in GodFinger.power_over_ground)
					{
						GodPower godPower2 = AssetManager.powers.get(tPowerID2);
						bool tDropTypeFound2 = false;
						DropAsset tDropAsset = godPower2.cached_drop_asset;
						if (tDropAsset != null)
						{
							if (!string.IsNullOrEmpty(tDropAsset.drop_type_high))
							{
								tDropTypeFound2 = true;
								int tCountHigh;
								if (tNeighbourBiomes.TryGetValue(tDropAsset.drop_type_high, out tCountHigh))
								{
									tList2.AddTimes(tCountHigh, tPowerID2);
								}
							}
							if (!string.IsNullOrEmpty(tDropAsset.drop_type_low))
							{
								tDropTypeFound2 = true;
								int tCountLow;
								if (tNeighbourBiomes.TryGetValue(tDropAsset.drop_type_low, out tCountLow))
								{
									tList2.AddTimes(tCountLow, tPowerID2);
								}
							}
						}
						TileType tTileAsset2 = godPower2.cached_tile_type_asset;
						if (tTileAsset2 != null)
						{
							tDropTypeFound2 = true;
							int tCount3;
							if (tNeighbourTypes.TryGetValue(tTileAsset2, out tCount3))
							{
								tList2.AddTimes(tCount3, tPowerID2);
							}
						}
						TopTileType tTopTileAsset2 = godPower2.cached_top_tile_type_asset;
						if (tTopTileAsset2 != null)
						{
							tDropTypeFound2 = true;
							int tCount4;
							if (tNeighbourTypes.TryGetValue(tTopTileAsset2, out tCount4))
							{
								tList2.AddTimes(tCount4, tPowerID2);
							}
						}
						if (!tDropTypeFound2)
						{
							tList2.Add(tPowerID2);
						}
					}
					string _random_power2 = Randy.getRandom<string>(tList2) ?? Randy.getRandom<string>(GodFinger.power_over_ground);
					pFinger.god_power = AssetManager.powers.get(_random_power2);
					goto IL_384;
				}
			}
			pFinger.god_power = null;
			IL_384:
			UnsafeCollectionPool<Dictionary<string, int>, KeyValuePair<string, int>>.Release(tNeighbourBiomes);
			UnsafeCollectionPool<Dictionary<TileTypeBase, int>, KeyValuePair<TileTypeBase, int>>.Release(tNeighbourTypes);
			UnsafeCollectionPool<HashSet<WorldTile>, WorldTile>.Release(tNeighbourTiles);
		}
	}
}
