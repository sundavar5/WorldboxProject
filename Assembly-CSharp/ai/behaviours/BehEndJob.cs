using System;

namespace ai.behaviours
{
	// Token: 0x020008B6 RID: 2230
	public class BehEndJob : BehaviourActionActor
	{
		// Token: 0x060044CA RID: 17610 RVA: 0x001CF3CE File Offset: 0x001CD5CE
		public override BehResult execute(Actor pActor)
		{
			pActor.endJob();
			return BehResult.Continue;
		}
	}
}
