using System;
using ai.behaviours;

// Token: 0x020003B0 RID: 944
public class BehCheckVegetativeReproduction : BehaviourActionActor
{
	// Token: 0x06002218 RID: 8728 RVA: 0x0011F4BF File Offset: 0x0011D6BF
	public override BehResult execute(Actor pActor)
	{
		if (pActor.hasStatus("taking_roots"))
		{
			return BehResult.Stop;
		}
		pActor.addStatusEffect("taking_roots", pActor.getMaturationTimeSeconds(), true);
		pActor.subspecies.counterReproduction();
		return BehResult.Continue;
	}
}
