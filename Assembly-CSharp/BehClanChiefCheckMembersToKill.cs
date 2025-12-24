using System;
using ai.behaviours;

// Token: 0x020003B8 RID: 952
public class BehClanChiefCheckMembersToKill : BehaviourActionActor
{
	// Token: 0x0600222A RID: 8746 RVA: 0x0011F8C4 File Offset: 0x0011DAC4
	public override BehResult execute(Actor pActor)
	{
		Clan tClan = pActor.clan;
		for (int i = 0; i < tClan.units.Count; i++)
		{
			Actor tActorTarget = tClan.units[i];
			if (tActorTarget != pActor && pActor.areFoes(tActorTarget))
			{
				tActorTarget.getHitFullHealth(AttackType.Divine);
			}
		}
		return BehResult.Continue;
	}
}
