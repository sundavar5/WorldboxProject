using System;
using ai.behaviours;

// Token: 0x020003AC RID: 940
public class BehCheckParthenogenesisReproduction : BehaviourActionActor
{
	// Token: 0x06002210 RID: 8720 RVA: 0x0011F344 File Offset: 0x0011D544
	public override BehResult execute(Actor pActor)
	{
		switch (pActor.subspecies.getReproductionStrategy())
		{
		case ReproductiveStrategy.Egg:
		case ReproductiveStrategy.SpawnUnitImmediate:
			BabyMaker.makeBabyViaParthenogenesis(pActor);
			break;
		case ReproductiveStrategy.Pregnancy:
		{
			BabyHelper.babyMakingStart(pActor);
			float tMaturationTime = pActor.getMaturationTimeSeconds();
			pActor.addStatusEffect("pregnant_parthenogenesis", tMaturationTime, true);
			pActor.subspecies.counterReproduction();
			break;
		}
		}
		return BehResult.Continue;
	}
}
