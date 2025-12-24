using System;
using ai.behaviours;

// Token: 0x0200039F RID: 927
public class BehMakeDecision : BehaviourActionActor
{
	// Token: 0x060021DB RID: 8667 RVA: 0x0011DE99 File Offset: 0x0011C099
	public override BehResult execute(Actor pActor)
	{
		if (pActor.isEgg())
		{
			return BehResult.Stop;
		}
		pActor.batch.c_make_decision.Add(pActor);
		return BehResult.Stop;
	}
}
