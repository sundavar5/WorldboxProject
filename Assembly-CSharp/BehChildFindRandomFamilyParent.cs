using System;
using ai.behaviours;

// Token: 0x02000394 RID: 916
public class BehChildFindRandomFamilyParent : BehaviourActionActor
{
	// Token: 0x060021B1 RID: 8625 RVA: 0x0011D260 File Offset: 0x0011B460
	public override BehResult execute(Actor pBabyActor)
	{
		if (!pBabyActor.family.hasFounders())
		{
			return BehResult.Stop;
		}
		Actor tParent = pBabyActor.family.getRandomFounder();
		if (pBabyActor.inOwnCityBorders() && !tParent.inOwnCityBorders())
		{
			return BehResult.Stop;
		}
		pBabyActor.beh_actor_target = tParent;
		return BehResult.Continue;
	}
}
