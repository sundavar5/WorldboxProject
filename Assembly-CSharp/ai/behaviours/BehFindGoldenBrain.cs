using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x020008C4 RID: 2244
	public class BehFindGoldenBrain : BehaviourActionActor
	{
		// Token: 0x060044EE RID: 17646 RVA: 0x001CFD1C File Offset: 0x001CDF1C
		public override BehResult execute(Actor pActor)
		{
			if (WorldLawLibrary.world_law_peaceful_monsters.isEnabled())
			{
				return BehResult.Stop;
			}
			List<Building> tBuildingList = BehaviourActionBase<Actor>.world.kingdoms_wild.get("golden_brain").buildings;
			if (tBuildingList.Count == 0)
			{
				return BehResult.Stop;
			}
			Building tTarget = Finder.getClosestBuildingFrom(pActor, tBuildingList);
			if (tTarget == null)
			{
				return BehResult.Stop;
			}
			pActor.beh_building_target = tTarget;
			return BehResult.Continue;
		}
	}
}
