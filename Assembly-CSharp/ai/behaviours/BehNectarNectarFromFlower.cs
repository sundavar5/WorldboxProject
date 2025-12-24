using System;

namespace ai.behaviours
{
	// Token: 0x020008EB RID: 2283
	public class BehNectarNectarFromFlower : BehActorUsableBuildingTarget
	{
		// Token: 0x0600454C RID: 17740 RVA: 0x001D1B8C File Offset: 0x001CFD8C
		public override BehResult execute(Actor pActor)
		{
			if (pActor.beh_building_target.asset.type != "type_flower")
			{
				return BehResult.Stop;
			}
			if (pActor.beh_building_target.isAlive())
			{
				int tNutrition = (int)((float)pActor.beh_building_target.asset.nutrition_restore * 0.5f);
				pActor.addNutritionFromEating(tNutrition, false, true);
				pActor.countConsumed();
			}
			WorldTile tTile = pActor.beh_building_target.current_tile;
			pActor.punchTargetAnimation(tTile.posV3, false, false, 40f);
			return BehResult.Continue;
		}
	}
}
