using System;

namespace ai.behaviours
{
	// Token: 0x0200088D RID: 2189
	public class BehBuildingTargetHome : BehCityActor
	{
		// Token: 0x06004460 RID: 17504 RVA: 0x001CDC70 File Offset: 0x001CBE70
		public override BehResult execute(Actor pActor)
		{
			Building tHomeBuilding = pActor.getHomeBuilding();
			if (tHomeBuilding == null)
			{
				return BehResult.Stop;
			}
			pActor.beh_building_target = tHomeBuilding;
			return BehResult.Continue;
		}
	}
}
