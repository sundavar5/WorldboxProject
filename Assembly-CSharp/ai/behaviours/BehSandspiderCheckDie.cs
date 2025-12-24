using System;

namespace ai.behaviours
{
	// Token: 0x02000918 RID: 2328
	public class BehSandspiderCheckDie : BehaviourActionActor
	{
		// Token: 0x060045C2 RID: 17858 RVA: 0x001D3948 File Offset: 0x001D1B48
		public override BehResult execute(Actor pActor)
		{
			int antSteps;
			pActor.data.get("ant_steps", out antSteps, 0);
			if (pActor.beh_tile_target == null || antSteps > 20)
			{
				pActor.dieSimpleNone();
				return BehResult.Stop;
			}
			return BehResult.Continue;
		}
	}
}
