using System;

namespace ai.behaviours
{
	// Token: 0x02000891 RID: 2193
	public class BehCheckCityActorArmyGroup : BehCitizenActionCity
	{
		// Token: 0x0600446B RID: 17515 RVA: 0x001CDEB4 File Offset: 0x001CC0B4
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.isKingdomCiv())
			{
				pActor.stopBeingWarrior();
				return BehResult.Stop;
			}
			if (pActor.city.hasArmy())
			{
				Army tArmy = pActor.city.army;
				if (tArmy.isGroupInCityAndHaveLeader())
				{
					pActor.setArmy(tArmy);
				}
			}
			return BehResult.Continue;
		}
	}
}
