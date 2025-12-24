using System;

namespace ai.behaviours
{
	// Token: 0x02000929 RID: 2345
	public class BehBoatDamageCheck : BehBoat
	{
		// Token: 0x060045ED RID: 17901 RVA: 0x001D4056 File Offset: 0x001D2256
		public override BehResult execute(Actor pActor)
		{
			if (pActor.getHealthRatio() < 0.8f)
			{
				base.checkHomeDocks(pActor);
				if (this.boat.actor.getHomeBuilding() != null)
				{
					pActor.cancelAllBeh();
					return base.forceTask(pActor, "boat_return_to_dock", true, false);
				}
			}
			return BehResult.Continue;
		}
	}
}
