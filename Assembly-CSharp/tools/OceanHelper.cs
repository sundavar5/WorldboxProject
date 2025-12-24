using System;
using System.Collections.Generic;

namespace tools
{
	// Token: 0x02000861 RID: 2145
	public class OceanHelper
	{
		// Token: 0x06004330 RID: 17200 RVA: 0x001C8610 File Offset: 0x001C6810
		public static bool goodForNewDock(WorldTile pTile)
		{
			if (!pTile.Type.ocean)
			{
				return false;
			}
			MapRegion region = pTile.region;
			return ((region != null) ? region.island : null) != null && pTile.region.island.goodForDocks() && !OceanHelper._ocean_pools_with_docks.Contains(pTile.region.island);
		}

		// Token: 0x06004331 RID: 17201 RVA: 0x001C8670 File Offset: 0x001C6870
		public static void saveOceanPoolsWithDocks(City pCity)
		{
			List<Building> tList = pCity.getBuildingListOfType("type_docks");
			if (tList != null && tList.Count != 0)
			{
				for (int i = 0; i < tList.Count; i++)
				{
					Building tDock = tList[i];
					for (int j = 0; j < tDock.tiles.Count; j++)
					{
						OceanHelper.addOceanPool(tDock.tiles[j]);
					}
				}
			}
		}

		// Token: 0x06004332 RID: 17202 RVA: 0x001C86D4 File Offset: 0x001C68D4
		public static void clearOceanPools()
		{
			OceanHelper._ocean_pools_with_docks.Clear();
		}

		// Token: 0x06004333 RID: 17203 RVA: 0x001C86E0 File Offset: 0x001C68E0
		private static void addOceanPool(WorldTile pTile)
		{
			if (pTile.region.island == null)
			{
				return;
			}
			if (pTile.region.type != TileLayerType.Ocean)
			{
				return;
			}
			OceanHelper._ocean_pools_with_docks.Add(pTile.region.island);
		}

		// Token: 0x06004334 RID: 17204 RVA: 0x001C8714 File Offset: 0x001C6914
		public static WorldTile findTileForBoat(WorldTile pTileBoat, WorldTile pTileTarget)
		{
			WorldTile tResultTile = OceanHelper.findWaterTileInRegion(pTileTarget.region, pTileBoat);
			if (tResultTile == null)
			{
				tResultTile = OceanHelper.findTileInWholeIsland(pTileTarget.region.island, pTileBoat);
			}
			if (tResultTile == null)
			{
				return null;
			}
			for (int i = 0; i < tResultTile.neighboursAll.Length; i++)
			{
				WorldTile tTile = tResultTile.neighboursAll[i];
				if (tTile.isSameIsland(pTileBoat) && tTile.isGoodForBoat())
				{
					tResultTile = tTile;
					break;
				}
			}
			World.world.flash_effects.flashPixel(tResultTile, -1, ColorType.White);
			return tResultTile;
		}

		// Token: 0x06004335 RID: 17205 RVA: 0x001C878C File Offset: 0x001C698C
		private static WorldTile findTileInWholeIsland(TileIsland pIslandTarget, WorldTile pTileBoat)
		{
			WorldTile tResultTile = null;
			int tTriesLeft = 10;
			float tBestDistance = float.MaxValue;
			foreach (MapRegion pRegion in pIslandTarget.regions.getSimpleList().LoopRandom<MapRegion>())
			{
				WorldTile tTempTile = OceanHelper.findWaterTileInRegion(pRegion, pTileBoat);
				if (tTempTile != null)
				{
					float tDistance = Toolbox.DistTile(pTileBoat, tTempTile);
					if (tDistance < tBestDistance)
					{
						tResultTile = tTempTile;
						tBestDistance = tDistance;
						tTriesLeft--;
						if (tTriesLeft <= 0)
						{
							break;
						}
					}
				}
			}
			return tResultTile;
		}

		// Token: 0x06004336 RID: 17206 RVA: 0x001C8814 File Offset: 0x001C6A14
		private static WorldTile findWaterTileInRegion(MapRegion pRegion, WorldTile pBoatTile)
		{
			List<WorldTile> tListCorners = pRegion.getEdgeTiles();
			if (tListCorners.Count == 0)
			{
				return null;
			}
			foreach (WorldTile tCornerTile in tListCorners.LoopRandom<WorldTile>())
			{
				if (tCornerTile.isSameIsland(pBoatTile))
				{
					return tCornerTile;
				}
			}
			return null;
		}

		// Token: 0x040030FC RID: 12540
		private static List<TileIsland> _ocean_pools_with_docks = new List<TileIsland>();
	}
}
