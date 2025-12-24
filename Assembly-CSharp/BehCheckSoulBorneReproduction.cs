using System;
using ai.behaviours;

// Token: 0x020003AE RID: 942
public class BehCheckSoulBorneReproduction : BehaviourActionActor
{
	// Token: 0x06002214 RID: 8724 RVA: 0x0011F46A File Offset: 0x0011D66A
	public override BehResult execute(Actor pActor)
	{
		if (!pActor.hasStatus("soul_harvested"))
		{
			return BehResult.Stop;
		}
		if (pActor.hasStatus("pregnant"))
		{
			return BehResult.Stop;
		}
		if (BabyHelper.isMetaLimitsReached(pActor))
		{
			return BehResult.Stop;
		}
		pActor.finishStatusEffect("soul_harvested");
		BabyMaker.startSoulborneBirth(pActor);
		return BehResult.Continue;
	}
}
