using System;
using UnityEngine;

namespace ai.behaviours
{
	// Token: 0x02000910 RID: 2320
	public class BehWormDive : BehaviourActionActor
	{
		// Token: 0x060045A8 RID: 17832 RVA: 0x001D31F8 File Offset: 0x001D13F8
		public override BehResult execute(Actor pActor)
		{
			int diveSteps;
			pActor.data.get("dive_steps", out diveSteps, 0);
			if (--diveSteps < 1)
			{
				diveSteps = Randy.randomInt(Randy.randomInt(1, 6), Randy.randomInt(10, 60));
				int size;
				pActor.data.get("size", out size, 0);
				size = Mathf.Clamp(Randy.randomBool() ? (++size) : (--size), 0, 2);
				pActor.data.set("size", size);
			}
			pActor.data.set("dive_steps", diveSteps);
			return BehResult.Continue;
		}
	}
}
