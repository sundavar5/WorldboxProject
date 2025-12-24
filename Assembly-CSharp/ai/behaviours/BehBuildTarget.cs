using System;

namespace ai.behaviours
{
	// Token: 0x0200088C RID: 2188
	public class BehBuildTarget : BehActorUsableBuildingTarget
	{
		// Token: 0x0600445D RID: 17501 RVA: 0x001CDC1B File Offset: 0x001CBE1B
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_tile_target = true;
			this.null_check_city = true;
		}

		// Token: 0x0600445E RID: 17502 RVA: 0x001CDC31 File Offset: 0x001CBE31
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.beh_building_target.isUnderConstruction())
			{
				return BehResult.Stop;
			}
			if (pActor.beh_building_target.updateBuild(pActor.getConstructionSpeed()))
			{
				pActor.addLoot(SimGlobals.m.coins_for_building);
			}
			return BehResult.Continue;
		}
	}
}
