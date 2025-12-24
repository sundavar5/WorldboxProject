using System;

namespace ai.behaviours
{
	// Token: 0x02000892 RID: 2194
	public class BehCheckCityActorWarriorLimit : BehCityActor
	{
		// Token: 0x0600446D RID: 17517 RVA: 0x001CDF04 File Offset: 0x001CC104
		public override BehResult execute(Actor pActor)
		{
			City tCity = pActor.city;
			if (!pActor.inOwnCityBorders())
			{
				return BehResult.Stop;
			}
			tCity.checkIfWarriorStillOk(pActor);
			return BehResult.Continue;
		}
	}
}
