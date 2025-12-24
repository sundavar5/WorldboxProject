using System;

namespace ai.behaviours
{
	// Token: 0x02000898 RID: 2200
	public class BehCheckIfOnGround : BehaviourActionActor
	{
		// Token: 0x0600447A RID: 17530 RVA: 0x001CE125 File Offset: 0x001CC325
		public override BehResult execute(Actor pActor)
		{
			if (pActor.isInLiquid())
			{
				return BehResult.Stop;
			}
			return BehResult.Continue;
		}
	}
}
