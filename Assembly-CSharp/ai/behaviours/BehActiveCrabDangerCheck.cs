using System;

namespace ai.behaviours
{
	// Token: 0x0200087D RID: 2173
	public class BehActiveCrabDangerCheck : BehActive
	{
		// Token: 0x0600443D RID: 17469 RVA: 0x001CD68C File Offset: 0x001CB88C
		public override BehResult execute(Actor pActor)
		{
			if (pActor.isHungry())
			{
				return BehResult.Continue;
			}
			if (Toolbox.hasDifferentSpeciesInChunkAround(pActor.current_tile, pActor.asset.id))
			{
				pActor.cancelAllBeh();
				pActor.ai.setJob("crab_burrow");
			}
			return BehResult.Continue;
		}
	}
}
