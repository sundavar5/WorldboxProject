using System;

namespace ai.behaviours
{
	// Token: 0x020008B2 RID: 2226
	public class BehConsumeTargetBuilding : BehActorUsableBuildingTarget
	{
		// Token: 0x060044C0 RID: 17600 RVA: 0x001CF18C File Offset: 0x001CD38C
		public override BehResult execute(Actor pActor)
		{
			if (pActor.beh_building_target.asset.type == "type_fruits")
			{
				if (pActor.beh_building_target.hasResourcesToCollect())
				{
					pActor.beh_building_target.extractResources(pActor);
					pActor.addNutritionFromEating(pActor.beh_building_target.asset.nutrition_restore, false, true);
					pActor.countConsumed();
				}
			}
			else if (pActor.beh_building_target.isAlive())
			{
				pActor.beh_building_target.startDestroyBuilding();
				pActor.addNutritionFromEating(pActor.beh_building_target.asset.nutrition_restore, false, true);
				pActor.countConsumed();
			}
			WorldTile tTile = pActor.beh_building_target.current_tile;
			pActor.punchTargetAnimation(tTile.posV3, false, false, 40f);
			return BehResult.Continue;
		}
	}
}
