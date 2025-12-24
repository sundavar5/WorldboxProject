using System;

namespace ai.behaviours
{
	// Token: 0x02000953 RID: 2387
	public class BehDragonSleepy : BehaviourActionActor
	{
		// Token: 0x06004658 RID: 18008 RVA: 0x001DD040 File Offset: 0x001DB240
		public override BehResult execute(Actor pActor)
		{
			if (pActor.hasTrait("zombie"))
			{
				return BehResult.Continue;
			}
			int sleepy;
			pActor.data.get("sleepy", out sleepy, 0);
			pActor.data.set("sleepy", ++sleepy);
			return BehResult.Continue;
		}
	}
}
