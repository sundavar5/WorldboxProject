using System;

namespace ai.behaviours
{
	// Token: 0x02000933 RID: 2355
	public class BehBoatSetHomeDockTarget : BehBoat
	{
		// Token: 0x06004603 RID: 17923 RVA: 0x001D43CC File Offset: 0x001D25CC
		public override BehResult execute(Actor pActor)
		{
			base.checkHomeDocks(pActor);
			Building tHomeBuilding = this.boat.actor.getHomeBuilding();
			if (tHomeBuilding == null)
			{
				return BehResult.Stop;
			}
			pActor.beh_building_target = tHomeBuilding;
			return BehResult.Continue;
		}
	}
}
