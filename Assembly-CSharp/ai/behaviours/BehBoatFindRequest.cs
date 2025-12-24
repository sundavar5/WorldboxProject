using System;
using life.taxi;

namespace ai.behaviours
{
	// Token: 0x0200092C RID: 2348
	public class BehBoatFindRequest : BehBoat
	{
		// Token: 0x060045F3 RID: 17907 RVA: 0x001D4198 File Offset: 0x001D2398
		public override BehResult execute(Actor pActor)
		{
			if (this.boat.taxi_request != null && !this.boat.taxi_request.isAlreadyUsedByBoat(pActor))
			{
				this.boat.taxi_request.cancel();
				this.boat.taxi_request = null;
			}
			this.boat.taxi_request = TaxiManager.getNewRequestForBoat(pActor);
			if (this.boat.taxi_request == null)
			{
				return BehResult.Stop;
			}
			this.boat.taxi_request.assign(this.boat);
			return base.forceTask(pActor, "boat_transport_go_load", true, false);
		}
	}
}
