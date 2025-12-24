using System;

namespace ai.behaviours
{
	// Token: 0x02000899 RID: 2201
	public class BehCheckIfOnSmallLand : BehaviourActionActor
	{
		// Token: 0x0600447C RID: 17532 RVA: 0x001CE13A File Offset: 0x001CC33A
		public override BehResult execute(Actor pActor)
		{
			if (pActor.current_tile.region.island.isGoodIslandForActor(pActor))
			{
				return BehResult.Stop;
			}
			return BehResult.Continue;
		}
	}
}
