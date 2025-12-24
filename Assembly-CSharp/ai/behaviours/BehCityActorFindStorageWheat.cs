using System;

namespace ai.behaviours
{
	// Token: 0x020008A7 RID: 2215
	public class BehCityActorFindStorageWheat : BehCityActor
	{
		// Token: 0x060044A1 RID: 17569 RVA: 0x001CE7C0 File Offset: 0x001CC9C0
		public override BehResult execute(Actor pActor)
		{
			Building tWindmill = pActor.city.getBuildingOfType("type_windmill", true, false, false, pActor.current_island);
			if (tWindmill != null)
			{
				pActor.beh_building_target = tWindmill;
				return BehResult.Continue;
			}
			Building tBuilding = pActor.city.getStorageNear(pActor.current_tile, true);
			if (tBuilding != null)
			{
				pActor.beh_building_target = tBuilding;
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
