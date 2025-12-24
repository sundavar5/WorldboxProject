using System;
using tools;

namespace ai.behaviours
{
	// Token: 0x02000937 RID: 2359
	public class BehBoatTransportFindTileUnload : BehBoat
	{
		// Token: 0x0600460B RID: 17931 RVA: 0x001D4740 File Offset: 0x001D2940
		public override BehResult execute(Actor pActor)
		{
			if (this.boat.taxi_target == null)
			{
				return BehResult.Stop;
			}
			WorldTile tTargetTile = OceanHelper.findTileForBoat(pActor.current_tile, this.boat.taxi_target);
			if (tTargetTile == null)
			{
				this.boat.cancelWork(pActor);
				return BehResult.Stop;
			}
			pActor.beh_tile_target = tTargetTile;
			return BehResult.Continue;
		}
	}
}
