using System;

namespace ai.behaviours
{
	// Token: 0x02000905 RID: 2309
	public class BehTaxiSitInside : BehaviourActionActor
	{
		// Token: 0x06004586 RID: 17798 RVA: 0x001D287F File Offset: 0x001D0A7F
		public override BehResult execute(Actor pActor)
		{
			pActor.timer_action = 1f;
			return BehResult.RestartTask;
		}
	}
}
