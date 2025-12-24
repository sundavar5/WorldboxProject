using System;

namespace ai.behaviours
{
	// Token: 0x020008D9 RID: 2265
	public class BehFindWheat : BehCityActor
	{
		// Token: 0x0600451A RID: 17690 RVA: 0x001D0BCC File Offset: 0x001CEDCC
		public override BehResult execute(Actor pActor)
		{
			int tBestDist = int.MaxValue;
			WorldTile tBestTile = null;
			Building tBestBuilding = null;
			foreach (WorldTile tTile in pActor.city.calculated_grown_wheat)
			{
				if (tTile.building != null && tTile.building.asset.wheat)
				{
					int tDist = Toolbox.SquaredDistTile(pActor.current_tile, tTile);
					if (tDist < tBestDist && tTile.building.isUsable() && tTile.building.component_wheat.isMaxLevel() && tTile.isSameIsland(pActor.current_tile) && !tTile.isTargeted())
					{
						tBestDist = tDist;
						tBestTile = tTile;
						tBestBuilding = tTile.building;
					}
				}
			}
			if (tBestTile == null)
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = tBestTile;
			pActor.beh_building_target = tBestBuilding;
			return BehResult.Continue;
		}
	}
}
