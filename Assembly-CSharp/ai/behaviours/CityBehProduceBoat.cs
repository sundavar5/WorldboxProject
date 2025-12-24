using System;

namespace ai.behaviours
{
	// Token: 0x0200096F RID: 2415
	public class CityBehProduceBoat : BehaviourActionCity
	{
		// Token: 0x060046C8 RID: 18120 RVA: 0x001E0D10 File Offset: 0x001DEF10
		public override BehResult execute(City pCity)
		{
			if (pCity.timer_build_boat > 0f)
			{
				return BehResult.Stop;
			}
			Building tBuilding = pCity.getBuildingOfType("type_docks", true, true, false, null);
			if (tBuilding == null)
			{
				return BehResult.Stop;
			}
			Actor tBoat = tBuilding.component_docks.buildBoatFromHere(pCity);
			if (tBoat == null)
			{
				return BehResult.Stop;
			}
			tBoat.joinKingdom(pCity.kingdom);
			tBoat.joinCity(pCity);
			pCity.timer_build_boat = 10f;
			return BehResult.Continue;
		}
	}
}
