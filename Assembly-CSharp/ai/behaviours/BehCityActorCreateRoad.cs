using System;

namespace ai.behaviours
{
	// Token: 0x020008A0 RID: 2208
	public class BehCityActorCreateRoad : BehCityActor
	{
		// Token: 0x0600448D RID: 17549 RVA: 0x001CE2D3 File Offset: 0x001CC4D3
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_tile_target = true;
		}

		// Token: 0x0600448E RID: 17550 RVA: 0x001CE2E2 File Offset: 0x001CC4E2
		public override BehResult execute(Actor pActor)
		{
			MapAction.createRoadTile(pActor.beh_tile_target);
			pActor.addLoot(SimGlobals.m.coins_for_road);
			return BehResult.Continue;
		}
	}
}
