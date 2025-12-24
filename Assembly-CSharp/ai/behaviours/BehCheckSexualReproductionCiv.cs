using System;

namespace ai.behaviours
{
	// Token: 0x0200091B RID: 2331
	public class BehCheckSexualReproductionCiv : BehCityActor
	{
		// Token: 0x060045CC RID: 17868 RVA: 0x001D3BFC File Offset: 0x001D1DFC
		public override BehResult execute(Actor pActor)
		{
			if (pActor.isKingdomCiv())
			{
				if (!pActor.hasHouse())
				{
					return BehResult.Stop;
				}
				if (!pActor.hasLover())
				{
					return BehResult.Stop;
				}
			}
			Actor tLover = pActor.lover;
			if (!tLover.canCurrentTaskBeCancelledByReproduction())
			{
				return BehResult.Stop;
			}
			tLover.setTask("sexual_reproduction_civ_go", true, true, false);
			tLover.timer_action = 0f;
			return base.forceTask(pActor, "sexual_reproduction_civ_go", true, false);
		}
	}
}
