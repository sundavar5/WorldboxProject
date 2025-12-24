using System;

namespace ai.behaviours
{
	// Token: 0x0200091F RID: 2335
	public class BehBeeCheckReturnHome : BehaviourActionActor
	{
		// Token: 0x060045D5 RID: 17877 RVA: 0x001D3D28 File Offset: 0x001D1F28
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
