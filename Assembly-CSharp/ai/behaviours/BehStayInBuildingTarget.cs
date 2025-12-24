using System;

namespace ai.behaviours
{
	// Token: 0x02000901 RID: 2305
	public class BehStayInBuildingTarget : BehCityActor
	{
		// Token: 0x0600457D RID: 17789 RVA: 0x001D2660 File Offset: 0x001D0860
		public BehStayInBuildingTarget(float pMin = 0f, float pMax = 1f)
		{
			this.min = pMin;
			this.max = pMax;
		}

		// Token: 0x0600457E RID: 17790 RVA: 0x001D2676 File Offset: 0x001D0876
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.check_building_target_non_usable = true;
			this.null_check_building_target = true;
		}

		// Token: 0x0600457F RID: 17791 RVA: 0x001D268C File Offset: 0x001D088C
		public override BehResult execute(Actor pActor)
		{
			pActor.timer_action = Randy.randomFloat(this.min, this.max);
			pActor.stayInBuilding(pActor.beh_building_target);
			pActor.beh_tile_target = null;
			pActor.beh_building_target.startShake(0.5f, 0.1f, 0.1f);
			return BehResult.Continue;
		}

		// Token: 0x040031B8 RID: 12728
		private float min;

		// Token: 0x040031B9 RID: 12729
		private float max;
	}
}
