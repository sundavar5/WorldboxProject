using System;

namespace ai.behaviours.conditions
{
	// Token: 0x02000982 RID: 2434
	public class CondDragonNotSleepy : BehaviourActorCondition
	{
		// Token: 0x060046FC RID: 18172 RVA: 0x001E20A4 File Offset: 0x001E02A4
		public override bool check(Actor pActor)
		{
			int tSleepy;
			pActor.data.get("sleepy", out tSleepy, 0);
			return tSleepy < 10;
		}
	}
}
