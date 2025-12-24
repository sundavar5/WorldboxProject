using System;

namespace ai.behaviours
{
	// Token: 0x020008D6 RID: 2262
	public class BehFindTileForFarm : BehCityActor
	{
		// Token: 0x06004513 RID: 17683 RVA: 0x001D08EC File Offset: 0x001CEAEC
		public override BehResult execute(Actor pActor)
		{
			Building tBuilding = pActor.city.getBuildingOfType("type_windmill", true, false, false, null);
			if (tBuilding == null)
			{
				return BehResult.Stop;
			}
			int tBestDist = int.MaxValue;
			WorldTile tBestTile = null;
			foreach (WorldTile tTile in pActor.city.calculated_place_for_farms)
			{
				int tDist = Toolbox.SquaredDistTile(tBuilding.current_tile, tTile);
				if (tDist < tBestDist && (!tTile.hasBuilding() || tTile.building.canRemoveForFarms()) && !tTile.isTargeted() && pActor.current_tile.isSameIsland(tTile) && tTile.IsTypeAround(TopTileLibrary.field))
				{
					tBestDist = tDist;
					tBestTile = tTile;
				}
			}
			if (tBestTile == null)
			{
				foreach (WorldTile tTile2 in pActor.city.calculated_place_for_farms)
				{
					int tDist2 = Toolbox.SquaredDistTile(tBuilding.current_tile, tTile2);
					if (tDist2 < tBestDist && (!tTile2.hasBuilding() || tTile2.building.canRemoveForFarms()) && !tTile2.isTargeted() && pActor.current_tile.isSameIsland(tTile2))
					{
						tBestDist = tDist2;
						tBestTile = tTile2;
					}
				}
			}
			if (tBestTile == null)
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = tBestTile;
			return BehResult.Continue;
		}
	}
}
