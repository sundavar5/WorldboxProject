using System;

namespace ai.behaviours
{
	// Token: 0x02000925 RID: 2341
	public class BehBoatCheckFishingRepeat : BehaviourActionActor
	{
		// Token: 0x060045E5 RID: 17893 RVA: 0x001D3FD1 File Offset: 0x001D21D1
		public override BehResult execute(Actor pActor)
		{
			if (pActor.inventory.getResource("fish") <= 10)
			{
				return BehResult.RestartTask;
			}
			return base.forceTask(pActor, "boat_return_to_dock", true, false);
		}
	}
}
