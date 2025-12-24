using System;

namespace ai.behaviours
{
	// Token: 0x020008A6 RID: 2214
	public class BehCityActorFindStorage : BehCityActor
	{
		// Token: 0x0600449F RID: 17567 RVA: 0x001CE788 File Offset: 0x001CC988
		public override BehResult execute(Actor pActor)
		{
			Building tBuilding = pActor.city.getStorageNear(pActor.current_tile, false);
			if (tBuilding != null)
			{
				pActor.beh_building_target = tBuilding;
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
