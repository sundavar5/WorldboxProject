using System;
using tools;

namespace ai.behaviours
{
	// Token: 0x02000936 RID: 2358
	public class BehBoatTransportFindTilePickUp : BehBoat
	{
		// Token: 0x06004609 RID: 17929 RVA: 0x001D462C File Offset: 0x001D282C
		public override BehResult execute(Actor pActor)
		{
			if (this.boat.taxi_request == null || !this.boat.taxi_request.isStillLegit())
			{
				this.boat.cancelWork(pActor);
				return BehResult.Stop;
			}
			this.boat.pickup_near_dock = false;
			ActorTool.checkHomeDocks(pActor);
			Building tHomeBuilding = this.boat.actor.getHomeBuilding();
			if (tHomeBuilding != null)
			{
				WorldTile tWaterTileFromDock = tHomeBuilding.component_docks.getOceanTileInSameOcean(pActor.current_tile);
				if (tWaterTileFromDock != null && tWaterTileFromDock.isSameIsland(this.boat.taxi_request.getTileStart()))
				{
					this.boat.pickup_near_dock = true;
					if (this.boat.isNearDock())
					{
						this.boat.passengerWaitCounter = 0;
						pActor.beh_tile_target = pActor.current_tile;
						return BehResult.Continue;
					}
					pActor.beh_tile_target = tWaterTileFromDock;
					return BehResult.Continue;
				}
			}
			WorldTile tTargetTile = OceanHelper.findTileForBoat(pActor.current_tile, this.boat.taxi_request.getTileStart());
			if (tTargetTile == null)
			{
				this.boat.cancelWork(pActor);
				return BehResult.Stop;
			}
			this.boat.passengerWaitCounter = 0;
			pActor.beh_tile_target = tTargetTile;
			return BehResult.Continue;
		}
	}
}
