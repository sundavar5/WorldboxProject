using System;

namespace ai.behaviours
{
	// Token: 0x020008A5 RID: 2213
	public class BehCityActorFindNewJob : BehCityActor
	{
		// Token: 0x0600449D RID: 17565 RVA: 0x001CE770 File Offset: 0x001CC970
		public override BehResult execute(Actor pActor)
		{
			pActor.city.setCitizenJob(pActor);
			return BehResult.Continue;
		}
	}
}
