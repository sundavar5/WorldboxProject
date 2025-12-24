using System;
using System.Collections.Generic;

// Token: 0x02000236 RID: 566
public static class BuildingHelper
{
	// Token: 0x060015BC RID: 5564 RVA: 0x000DF9B4 File Offset: 0x000DDBB4
	public static void tryToBuildNear(WorldTile pTile, string pAssetID)
	{
		BuildingAsset tBuildingAsset = AssetManager.buildings.get(pAssetID);
		if (tBuildingAsset == null)
		{
			return;
		}
		if (World.world.buildings.canBuildFrom(pTile, tBuildingAsset, null, BuildPlacingType.New, false))
		{
			World.world.buildings.addBuilding(tBuildingAsset, pTile, false, false, BuildPlacingType.New);
			return;
		}
		BuildingHelper.tryToBuildNear(pTile, tBuildingAsset);
	}

	// Token: 0x060015BD RID: 5565 RVA: 0x000DFA08 File Offset: 0x000DDC08
	public static bool tryToBuildNear(WorldTile pTile, BuildingAsset pAsset)
	{
		List<WorldTile> tTempList = BuildingHelper._list_tiles;
		BuildingHelper.fillEmptyTilesAroundMine(pTile, tTempList);
		bool result = BuildingHelper.tryToPlaceBuilding(pAsset, tTempList);
		tTempList.Clear();
		return result;
	}

	// Token: 0x060015BE RID: 5566 RVA: 0x000DFA30 File Offset: 0x000DDC30
	private static void fillEmptyTilesAroundMine(WorldTile pTile, List<WorldTile> pList)
	{
		pList.Clear();
		int tSize = 4;
		int startX = pTile.x - tSize;
		int startY = pTile.y - tSize;
		for (int iX = 0; iX < tSize * 2; iX++)
		{
			for (int iY = 0; iY < tSize * 2; iY++)
			{
				WorldTile tTile = World.world.GetTile(iX + startX, iY + startY);
				if (tTile != null && (!tTile.hasBuilding() || !tTile.building.isUsable() || !tTile.building.asset.city_building))
				{
					pList.Add(tTile);
				}
			}
		}
	}

	// Token: 0x060015BF RID: 5567 RVA: 0x000DFAC0 File Offset: 0x000DDCC0
	private static bool tryToPlaceBuilding(BuildingAsset pAsset, List<WorldTile> pList)
	{
		foreach (WorldTile tTile in pList.LoopRandom<WorldTile>())
		{
			if (World.world.buildings.canBuildFrom(tTile, pAsset, null, BuildPlacingType.New, false))
			{
				if (World.world.buildings.addBuilding(pAsset, tTile, false, false, BuildPlacingType.New) != null)
				{
					return true;
				}
				break;
			}
		}
		return false;
	}

	// Token: 0x0400122F RID: 4655
	private static List<WorldTile> _list_tiles = new List<WorldTile>();
}
