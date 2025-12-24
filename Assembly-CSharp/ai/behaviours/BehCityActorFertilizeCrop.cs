using System;

namespace ai.behaviours
{
	// Token: 0x020008A1 RID: 2209
	public class BehCityActorFertilizeCrop : BehActorUsableBuildingTarget
	{
		// Token: 0x06004490 RID: 17552 RVA: 0x001CE308 File Offset: 0x001CC508
		public override BehResult execute(Actor pActor)
		{
			Building tCrop = pActor.beh_building_target;
			if (tCrop.component_wheat.isMaxLevel())
			{
				return BehResult.Stop;
			}
			if (pActor.inventory.getResource("fertilizer") == 0)
			{
				return BehResult.Stop;
			}
			pActor.takeFromInventory("fertilizer", 1);
			tCrop.component_wheat.growFull();
			pActor.addLoot(SimGlobals.m.coins_for_fertilize);
			return BehResult.Continue;
		}
	}
}
