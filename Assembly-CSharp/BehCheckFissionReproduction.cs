using System;
using ai.behaviours;

// Token: 0x020003AB RID: 939
public class BehCheckFissionReproduction : BehaviourActionActor
{
	// Token: 0x0600220E RID: 8718 RVA: 0x0011F330 File Offset: 0x0011D530
	public override BehResult execute(Actor pActor)
	{
		BabyMaker.makeBabyViaFission(pActor);
		return BehResult.Continue;
	}
}
