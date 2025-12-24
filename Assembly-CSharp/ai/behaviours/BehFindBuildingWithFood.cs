using System;

namespace ai.behaviours
{
	// Token: 0x020008C0 RID: 2240
	public class BehFindBuildingWithFood : BehCityActor
	{
		// Token: 0x060044E6 RID: 17638 RVA: 0x001CFB5C File Offset: 0x001CDD5C
		public override BehResult execute(Actor pActor)
		{
			Building tBuilding = pActor.city.getStorageWithFoodNear(pActor.current_tile);
			if (tBuilding != null)
			{
				pActor.beh_building_target = tBuilding;
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
