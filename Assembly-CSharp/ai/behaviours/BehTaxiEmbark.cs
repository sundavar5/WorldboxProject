using System;
using life.taxi;

namespace ai.behaviours
{
	// Token: 0x02000903 RID: 2307
	public class BehTaxiEmbark : BehCityActor
	{
		// Token: 0x06004582 RID: 17794 RVA: 0x001D2770 File Offset: 0x001D0970
		public override BehResult execute(Actor pActor)
		{
			TaxiRequest tRequest = TaxiManager.getRequestForActor(pActor);
			if (tRequest == null || !tRequest.hasAssignedBoat() || tRequest.state != TaxiRequestState.Loading)
			{
				return BehResult.Stop;
			}
			ActorSimpleComponent boat = tRequest.getBoat();
			bool tBoatNearDock = tRequest.isBoatNearDock();
			if ((float)Toolbox.SquaredDistTile(boat.actor.current_tile, pActor.current_tile) < 25f || tBoatNearDock)
			{
				pActor.beh_tile_target = null;
				pActor.embarkInto(tRequest.getBoat());
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
