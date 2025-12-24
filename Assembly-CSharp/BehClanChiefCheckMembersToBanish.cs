using System;
using ai.behaviours;

// Token: 0x020003B7 RID: 951
public class BehClanChiefCheckMembersToBanish : BehaviourActionActor
{
	// Token: 0x06002228 RID: 8744 RVA: 0x0011F870 File Offset: 0x0011DA70
	public override BehResult execute(Actor pActor)
	{
		Clan tClan = pActor.clan;
		for (int i = 0; i < tClan.units.Count; i++)
		{
			Actor tActorTarget = tClan.units[i];
			if (tActorTarget != pActor && pActor.areFoes(tActorTarget))
			{
				tActorTarget.setClan(null);
			}
		}
		return BehResult.Continue;
	}
}
