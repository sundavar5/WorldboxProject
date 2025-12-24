using System;
using ai.behaviours;

// Token: 0x020003D2 RID: 978
public class BehChangeKingdomLanguage : BehaviourActionActor
{
	// Token: 0x0600226D RID: 8813 RVA: 0x00120ED3 File Offset: 0x0011F0D3
	public override BehResult execute(Actor pActor)
	{
		if (pActor.hasLanguage())
		{
			pActor.kingdom.setLanguage(pActor.language);
		}
		return BehResult.Continue;
	}
}
