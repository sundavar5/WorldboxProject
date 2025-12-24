using System;

namespace ai.behaviours
{
	// Token: 0x020008F2 RID: 2290
	public class BehRemoveRuins : BehActorBuildingTarget
	{
		// Token: 0x0600455C RID: 17756 RVA: 0x001D2040 File Offset: 0x001D0240
		public override BehResult execute(Actor pActor)
		{
			BuildingAsset tAsset = pActor.beh_building_target.asset;
			BuildingType building_type = tAsset.building_type;
			if (building_type != BuildingType.Building_Tree)
			{
				if (building_type == BuildingType.Building_Civ)
				{
					if (tAsset.cost.wood > 0)
					{
						pActor.addToInventory("wood", 1);
					}
					if (tAsset.cost.stone > 0)
					{
						pActor.addToInventory("stone", 1);
					}
				}
			}
			else if (tAsset.hasResourceGiven("wood"))
			{
				pActor.addToInventory("wood", 1);
			}
			pActor.beh_building_target.startDestroyBuilding();
			pActor.addLoot(SimGlobals.m.coins_for_cleaning);
			return BehResult.Continue;
		}
	}
}
