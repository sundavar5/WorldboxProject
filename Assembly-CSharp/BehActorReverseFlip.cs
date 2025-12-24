using System;
using ai.behaviours;

// Token: 0x0200038B RID: 907
public class BehActorReverseFlip : BehaviourActionActor
{
	// Token: 0x0600219E RID: 8606 RVA: 0x0011D092 File Offset: 0x0011B292
	public override BehResult execute(Actor pActor)
	{
		pActor.setFlip(!pActor.flip);
		return BehResult.Continue;
	}
}
