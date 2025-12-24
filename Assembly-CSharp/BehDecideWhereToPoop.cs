using System;
using ai.behaviours;

// Token: 0x020003CB RID: 971
public class BehDecideWhereToPoop : BehaviourActionActor
{
	// Token: 0x0600225F RID: 8799 RVA: 0x00120DA8 File Offset: 0x0011EFA8
	public override BehResult execute(Actor pActor)
	{
		if (pActor.isAdult() && pActor.hasHouseCityInBordersAndSameIsland())
		{
			return base.forceTask(pActor, "poop_inside", false, true);
		}
		return base.forceTask(pActor, "poop_outside", false, true);
	}
}
