using System;
using ai.behaviours;

// Token: 0x020003CD RID: 973
public class BehPoopOutside : BehaviourActionActor
{
	// Token: 0x06002263 RID: 8803 RVA: 0x00120E31 File Offset: 0x0011F031
	public override BehResult execute(Actor pActor)
	{
		pActor.poop(true);
		return BehResult.Continue;
	}
}
