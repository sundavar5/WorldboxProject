using System;
using ai.behaviours;

// Token: 0x020003B1 RID: 945
public class BehSexualReproductionTry : BehaviourActionActor
{
	// Token: 0x0600221A RID: 8730 RVA: 0x0011F4F8 File Offset: 0x0011D6F8
	public override BehResult execute(Actor pActor)
	{
		base.execute(pActor);
		bool tInside = pActor.hasHouseCityInBordersAndSameIsland();
		RateCounter counter_reproduction_sexual_try = pActor.subspecies.counter_reproduction_sexual_try;
		if (counter_reproduction_sexual_try != null)
		{
			counter_reproduction_sexual_try.registerEvent();
		}
		if (tInside)
		{
			return base.forceTask(pActor, "sexual_reproduction_inside", false, true);
		}
		return base.forceTask(pActor, "sexual_reproduction_check_outside", false, true);
	}
}
