using System;

namespace ai.behaviours
{
	// Token: 0x020008B7 RID: 2231
	public class BehExitBuilding : BehCityActor
	{
		// Token: 0x060044CC RID: 17612 RVA: 0x001CF3DF File Offset: 0x001CD5DF
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_building_target = true;
			this.check_building_target_non_usable = true;
		}

		// Token: 0x060044CD RID: 17613 RVA: 0x001CF3F5 File Offset: 0x001CD5F5
		public override BehResult execute(Actor pActor)
		{
			pActor.exitBuilding();
			pActor.beh_building_target.startShake(0.01f, 0.1f, 0.1f);
			return BehResult.Continue;
		}
	}
}
