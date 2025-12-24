using System;

namespace ai.behaviours
{
	// Token: 0x0200092A RID: 2346
	public class BehBoatDangerCheck : BehBoat
	{
		// Token: 0x060045EF RID: 17903 RVA: 0x001D409C File Offset: 0x001D229C
		public override BehResult execute(Actor pActor)
		{
			if (pActor.attackedBy != null)
			{
				if (pActor.getHealthRatio() < 0.25f)
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
			return BehResult.Stop;
		}
	}
}
