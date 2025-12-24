using System;

namespace ai.behaviours
{
	// Token: 0x020008F7 RID: 2295
	public class BehRestartTask : BehaviourActionActor
	{
		// Token: 0x06004567 RID: 17767 RVA: 0x001D2214 File Offset: 0x001D0414
		public override BehResult execute(Actor pActor)
		{
			return BehResult.RestartTask;
		}
	}
}
