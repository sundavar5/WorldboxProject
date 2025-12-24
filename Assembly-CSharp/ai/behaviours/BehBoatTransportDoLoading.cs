using System;
using life.taxi;

namespace ai.behaviours
{
	// Token: 0x02000935 RID: 2357
	public class BehBoatTransportDoLoading : BehBoat
	{
		// Token: 0x06004607 RID: 17927 RVA: 0x001D44E0 File Offset: 0x001D26E0
		public override BehResult execute(Actor pActor)
		{
			TaxiRequest tRequest = this.boat.taxi_request;
			if (tRequest == null)
			{
				this.boat.cancelWork(pActor);
				return BehResult.Stop;
			}
			bool tContinueLoading = true;
			if (this.boat.passengerWaitCounter > 4 || this.boat.countPassengers() >= 100)
			{
				tContinueLoading = false;
			}
			else if (tRequest.everyoneEmbarked())
			{
				tContinueLoading = false;
			}
			if (tContinueLoading)
			{
				foreach (Actor tActor in tRequest.getActors())
				{
					if (!tActor.is_inside_boat && !tActor.isFighting() && (!tActor.hasTask() || !tActor.ai.task.flag_boat_related))
					{
						tActor.stopSleeping();
						tActor.cancelAllBeh();
						tActor.setTask("force_into_a_boat", true, false, false);
					}
				}
				tRequest.setState(TaxiRequestState.Loading);
				pActor.timer_action = 12f;
				this.boat.passengerWaitCounter++;
				return BehResult.RepeatStep;
			}
			if (!this.boat.hasPassengers())
			{
				this.boat.cancelWork(pActor);
				return BehResult.Stop;
			}
			tRequest.setState(TaxiRequestState.Transporting);
			tRequest.cancelForLatePassengers();
			this.boat.taxi_target = tRequest.getTileTarget();
			return BehResult.Continue;
		}
	}
}
