using System;
using ai.behaviours;

// Token: 0x020003AF RID: 943
public class BehCheckSporeReproduction : BehaviourActionActor
{
	// Token: 0x06002216 RID: 8726 RVA: 0x0011F4AE File Offset: 0x0011D6AE
	public override BehResult execute(Actor pActor)
	{
		BabyMaker.spawnSporesFor(pActor);
		return BehResult.Continue;
	}
}
