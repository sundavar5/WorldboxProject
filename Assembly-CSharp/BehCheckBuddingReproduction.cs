using System;
using ai.behaviours;

// Token: 0x020003A9 RID: 937
public class BehCheckBuddingReproduction : BehaviourActionActor
{
	// Token: 0x0600220A RID: 8714 RVA: 0x0011F2D8 File Offset: 0x0011D4D8
	public override BehResult execute(Actor pActor)
	{
		if (pActor.hasStatus("budding"))
		{
			return BehResult.Stop;
		}
		pActor.addStatusEffect("budding", pActor.getMaturationTimeSeconds(), true);
		pActor.subspecies.counterReproduction();
		return BehResult.Continue;
	}
}
