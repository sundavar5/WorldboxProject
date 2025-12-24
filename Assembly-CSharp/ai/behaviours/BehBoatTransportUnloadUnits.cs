using System;
using life.taxi;

namespace ai.behaviours
{
	// Token: 0x02000938 RID: 2360
	public class BehBoatTransportUnloadUnits : BehBoat
	{
		// Token: 0x0600460D RID: 17933 RVA: 0x001D4794 File Offset: 0x001D2994
		public override BehResult execute(Actor pActor)
		{
			if (this.boat.taxi_target == null)
			{
				return BehResult.Stop;
			}
			WorldTile tLandingTile = PathfinderTools.raycastTileForUnitLandingFromOcean(pActor.current_tile, this.boat.taxi_target);
			if (tLandingTile.Type.ocean)
			{
				foreach (WorldTile tNeighbourTile in tLandingTile.neighboursAll)
				{
					if (tNeighbourTile.Type.ground)
					{
						tLandingTile = tNeighbourTile;
						break;
					}
				}
			}
			this.boat.unloadPassengers(tLandingTile, false);
			if (this.boat.taxi_request != null)
			{
				TaxiManager.finish(this.boat.taxi_request);
				this.boat.taxi_request = null;
				this.boat.cancelWork(pActor);
			}
			return BehResult.Continue;
		}
	}
}
