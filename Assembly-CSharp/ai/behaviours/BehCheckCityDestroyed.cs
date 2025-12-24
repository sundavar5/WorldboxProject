using System;

namespace ai.behaviours
{
	// Token: 0x02000893 RID: 2195
	public class BehCheckCityDestroyed : BehaviourActionActor
	{
		// Token: 0x0600446F RID: 17519 RVA: 0x001CDF33 File Offset: 0x001CC133
		public override BehResult execute(Actor pActor)
		{
			if (pActor.city == null)
			{
				if (pActor.profession_asset.cancel_when_no_city)
				{
					pActor.stopBeingWarrior();
				}
				pActor.endJob();
				return BehResult.Stop;
			}
			return BehResult.Continue;
		}
	}
}
