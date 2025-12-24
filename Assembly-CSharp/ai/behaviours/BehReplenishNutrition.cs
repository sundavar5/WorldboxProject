using System;

namespace ai.behaviours
{
	// Token: 0x020008F5 RID: 2293
	public class BehReplenishNutrition : BehaviourActionActor
	{
		// Token: 0x06004562 RID: 17762 RVA: 0x001D2116 File Offset: 0x001D0316
		public override BehResult execute(Actor pActor)
		{
			pActor.addNutritionFromEating(pActor.getMaxNutrition(), false, true);
			pActor.countConsumed();
			return BehResult.Continue;
		}
	}
}
