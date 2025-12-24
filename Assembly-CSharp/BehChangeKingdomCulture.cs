using System;
using ai.behaviours;

// Token: 0x020003D1 RID: 977
public class BehChangeKingdomCulture : BehaviourActionActor
{
	// Token: 0x0600226B RID: 8811 RVA: 0x00120EAF File Offset: 0x0011F0AF
	public override BehResult execute(Actor pActor)
	{
		if (pActor.hasCulture())
		{
			pActor.kingdom.setCulture(pActor.culture);
		}
		return BehResult.Continue;
	}
}
