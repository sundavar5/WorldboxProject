using System;

namespace ai.behaviours
{
	// Token: 0x02000895 RID: 2197
	public class BehCheckEndCityActorJob : BehCityActor
	{
		// Token: 0x06004473 RID: 17523 RVA: 0x001CE014 File Offset: 0x001CC214
		public override BehResult execute(Actor pActor)
		{
			CitizenJobAsset tJob = pActor.a.citizen_job;
			int num = pActor.city.jobs.countOccupied(tJob);
			int tMax = pActor.city.jobs.countCurrentJobs(tJob);
			if (num > tMax)
			{
				pActor.endJob();
				return BehResult.Stop;
			}
			return BehResult.Continue;
		}
	}
}
