using System;

namespace ai.behaviours
{
	// Token: 0x0200092D RID: 2349
	public class BehBoatFindTargetForTrade : BehBoat
	{
		// Token: 0x060045F5 RID: 17909 RVA: 0x001D4230 File Offset: 0x001D2430
		public override BehResult execute(Actor pActor)
		{
			Docks tDockTarget = ActorTool.getDockTradeTarget(pActor);
			if (tDockTarget != null)
			{
				pActor.beh_building_target = tDockTarget.building;
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
