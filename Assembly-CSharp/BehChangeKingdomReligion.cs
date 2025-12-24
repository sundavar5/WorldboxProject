using System;
using ai.behaviours;

// Token: 0x020003D3 RID: 979
public class BehChangeKingdomReligion : BehaviourActionActor
{
	// Token: 0x0600226F RID: 8815 RVA: 0x00120EF7 File Offset: 0x0011F0F7
	public override BehResult execute(Actor pActor)
	{
		if (pActor.hasReligion())
		{
			pActor.kingdom.setReligion(pActor.religion);
		}
		return BehResult.Continue;
	}
}
