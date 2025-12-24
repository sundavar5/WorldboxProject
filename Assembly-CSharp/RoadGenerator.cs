using System;
using System.Collections.Generic;

// Token: 0x0200040F RID: 1039
public class RoadGenerator
{
	// Token: 0x060023EC RID: 9196 RVA: 0x0012BD78 File Offset: 0x00129F78
	internal static void generateRoadFor(Building pBuilding)
	{
		City tCity = pBuilding.city;
		if (tCity == null || tCity.buildings.Count <= 2)
		{
			return;
		}
		if (!pBuilding.door_tile.Type.ground)
		{
			return;
		}
		RoadGenerator.calcFlow(pBuilding.door_tile, pBuilding, 12);
	}

	// Token: 0x060023ED RID: 9197 RVA: 0x0012BDC0 File Offset: 0x00129FC0
	private static void calcFlow(WorldTile pStartTile, Building pBuilding, int pMaxWave)
	{
		RoadGenerator.targetBuildings.Clear();
		RoadGenerator.checkedTiles.Clear();
		RoadGenerator.curWave.Clear();
		RoadGenerator.nextWave.Clear();
		RoadGenerator.nextWave.Add(pStartTile);
		RoadGenerator.checkedTiles.Add(pStartTile);
		int iWaves = 0;
		while (RoadGenerator.nextWave.Count > 0 || RoadGenerator.curWave.Count > 0)
		{
			if (RoadGenerator.curWave.Count == 0)
			{
				if (iWaves > pMaxWave)
				{
					break;
				}
				RoadGenerator.curWave.AddRange(RoadGenerator.nextWave);
				RoadGenerator.nextWave.Clear();
				iWaves++;
			}
			WorldTile tTile = RoadGenerator.curWave[RoadGenerator.curWave.Count - 1];
			RoadGenerator.curWave.RemoveAt(RoadGenerator.curWave.Count - 1);
			tTile.is_checked_tile = true;
			tTile.score = iWaves;
			World.world.flash_effects.flashPixel(tTile, -1, ColorType.White);
			if (tTile.hasBuilding() && tTile.building.city == pBuilding.city && tTile.building != pBuilding && tTile.building.current_tile == tTile)
			{
				RoadGenerator.targetBuildings.Add(tTile.building);
			}
			for (int i = 0; i < tTile.neighboursAll.Length; i++)
			{
				WorldTile tNeighbour = tTile.neighboursAll[i];
				if (!tNeighbour.is_checked_tile)
				{
					tNeighbour.is_checked_tile = true;
					RoadGenerator.checkedTiles.Add(tNeighbour);
					if (!tNeighbour.Type.liquid && tNeighbour.Type.layer_type != TileLayerType.Block)
					{
						RoadGenerator.nextWave.Add(tNeighbour);
					}
				}
			}
		}
		for (int j = 0; j < RoadGenerator.targetBuildings.Count; j++)
		{
			Building tBuilding = RoadGenerator.targetBuildings[j];
			RoadGenerator.path.Clear();
			if (tBuilding.asset.build_road_to)
			{
				RoadGenerator.findPath(tBuilding.door_tile);
				RoadGenerator.fillPath();
			}
		}
		for (int k = 0; k < RoadGenerator.checkedTiles.Count; k++)
		{
			WorldTile worldTile = RoadGenerator.checkedTiles[k];
			worldTile.is_checked_tile = false;
			worldTile.score = -1;
		}
	}

	// Token: 0x060023EE RID: 9198 RVA: 0x0012BFD0 File Offset: 0x0012A1D0
	private static void fillPath()
	{
		for (int i = 0; i < RoadGenerator.path.Count; i++)
		{
			MapAction.createRoadTile(RoadGenerator.path[i]);
		}
	}

	// Token: 0x060023EF RID: 9199 RVA: 0x0012C004 File Offset: 0x0012A204
	private static void findPath(WorldTile pTile)
	{
		RoadGenerator.path.Add(pTile);
		if (pTile.score > 1)
		{
			WorldTile tBest = null;
			for (int i = 0; i < pTile.neighboursAll.Length; i++)
			{
				WorldTile tNextTile = pTile.neighboursAll[i];
				if (tNextTile.score == pTile.score - 1)
				{
					if (tBest == null)
					{
						tBest = tNextTile;
					}
					else if (tNextTile.Type.road)
					{
						tBest = tNextTile;
						RoadGenerator.findPath(tBest);
						return;
					}
				}
			}
			RoadGenerator.findPath(tBest);
			return;
		}
	}

	// Token: 0x040019E1 RID: 6625
	internal static List<WorldTile> checkedTiles = new List<WorldTile>();

	// Token: 0x040019E2 RID: 6626
	private static List<WorldTile> nextWave = new List<WorldTile>();

	// Token: 0x040019E3 RID: 6627
	private static List<WorldTile> curWave = new List<WorldTile>();

	// Token: 0x040019E4 RID: 6628
	private static List<Building> targetBuildings = new List<Building>();

	// Token: 0x040019E5 RID: 6629
	private static List<WorldTile> path = new List<WorldTile>();
}
