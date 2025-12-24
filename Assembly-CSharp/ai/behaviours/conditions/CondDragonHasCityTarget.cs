using System;

namespace ai.behaviours.conditions
{
	// Token: 0x02000980 RID: 2432
	public class CondDragonHasCityTarget : BehaviourActorCondition
	{
		// Token: 0x060046F8 RID: 18168 RVA: 0x001E2040 File Offset: 0x001E0240
		public override bool check(Actor pActor)
		{
			int attacksForCity;
			pActor.data.get("attacksForCity", out attacksForCity, 0);
			if (attacksForCity == 0)
			{
				return false;
			}
			long cityToAttack;
			pActor.data.get("cityToAttack", out cityToAttack, -1L);
			return cityToAttack.hasValue();
		}
	}
}
