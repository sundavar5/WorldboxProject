using System;
using life.taxi;

namespace ai.behaviours
{
	// Token: 0x02000904 RID: 2308
	public class BehTaxiFindShipTile : BehCityActor
	{
		// Token: 0x06004584 RID: 17796 RVA: 0x001D27E8 File Offset: 0x001D09E8
		public override BehResult execute(Actor pActor)
		{
			TaxiRequest tRequest = TaxiManager.getRequestForActor(pActor);
			if (tRequest == null || !tRequest.hasAssignedBoat() || tRequest.state != TaxiRequestState.Loading)
			{
				return BehResult.Stop;
			}
			Boat tBoat = tRequest.getBoat();
			WorldTile tTargetTile = null;
			if (tBoat.pickup_near_dock)
			{
				Building tHomeBuilding = tBoat.actor.getHomeBuilding();
				if (tHomeBuilding != null)
				{
					WorldTile tTile = tHomeBuilding.getConstructionTile();
					if (tTile != null)
					{
						tTargetTile = tTile.region.tiles.GetRandom<WorldTile>();
					}
				}
			}
			if (tTargetTile == null)
			{
				tTargetTile = PathfinderTools.raycastTileForUnitToEmbark(pActor.current_tile, tBoat.actor.current_tile);
			}
			if (tTargetTile == null)
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = tTargetTile;
			return BehResult.Continue;
		}
	}
}
