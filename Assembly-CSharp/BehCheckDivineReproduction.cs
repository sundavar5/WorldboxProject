using System;
using ai.behaviours;

// Token: 0x020003AA RID: 938
public class BehCheckDivineReproduction : BehaviourActionActor
{
	// Token: 0x0600220C RID: 8716 RVA: 0x0011F310 File Offset: 0x0011D510
	public override BehResult execute(Actor pActor)
	{
		if (pActor.hasTrait("miracle_bearer"))
		{
			return BehResult.Stop;
		}
		BabyMaker.startMiracleBirth(pActor);
		return BehResult.Continue;
	}
}
