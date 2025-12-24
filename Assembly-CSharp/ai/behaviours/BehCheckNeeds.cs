using System;

namespace ai.behaviours
{
	// Token: 0x0200089B RID: 2203
	public class BehCheckNeeds : BehCityActor
	{
		// Token: 0x06004481 RID: 17537 RVA: 0x001CE1B7 File Offset: 0x001CC3B7
		public BehCheckNeeds(int pRestarts)
		{
			this._max_restarts = pRestarts;
		}

		// Token: 0x06004482 RID: 17538 RVA: 0x001CE1C6 File Offset: 0x001CC3C6
		public override BehResult execute(Actor pActor)
		{
			if (this._max_restarts > 0 && pActor.ai.restarts >= this._max_restarts)
			{
				return BehResult.Stop;
			}
			if (pActor.isStarving() && pActor.city.hasAnyFood())
			{
				pActor.endJob();
				return BehResult.Stop;
			}
			return BehResult.Continue;
		}

		// Token: 0x0400316B RID: 12651
		private int _max_restarts;
	}
}
