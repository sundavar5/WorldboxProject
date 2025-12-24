using System;

namespace ai.behaviours.conditions
{
	// Token: 0x02000984 RID: 2436
	public class CondDragonSleepy : BehaviourActorCondition
	{
		// Token: 0x06004700 RID: 18176 RVA: 0x001E211C File Offset: 0x001E031C
		public override bool check(Actor pActor)
		{
			int tSleepy;
			pActor.data.get("sleepy", out tSleepy, 0);
			return tSleepy > 10;
		}
	}
}
