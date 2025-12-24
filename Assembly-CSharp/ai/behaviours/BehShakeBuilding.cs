using System;

namespace ai.behaviours
{
	// Token: 0x020008FB RID: 2299
	public class BehShakeBuilding : BehaviourActionActor
	{
		// Token: 0x0600456D RID: 17773 RVA: 0x001D227F File Offset: 0x001D047F
		public BehShakeBuilding(float pShakeParam = 0.7f, float pIntensityX = 0.04f, float pIntensityY = 0.04f)
		{
			this._shake_time = pShakeParam;
			this._shake_intensity_x = pIntensityX;
			this._shake_intensity_y = pIntensityY;
		}

		// Token: 0x0600456E RID: 17774 RVA: 0x001D229C File Offset: 0x001D049C
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.check_building_target_non_usable = true;
			this.null_check_building_target = true;
		}

		// Token: 0x0600456F RID: 17775 RVA: 0x001D22B2 File Offset: 0x001D04B2
		public override BehResult execute(Actor pActor)
		{
			pActor.beh_building_target.startShake(this._shake_time, this._shake_intensity_x, this._shake_intensity_y);
			return BehResult.Continue;
		}

		// Token: 0x040031B1 RID: 12721
		private float _shake_time;

		// Token: 0x040031B2 RID: 12722
		private float _shake_intensity_x;

		// Token: 0x040031B3 RID: 12723
		private float _shake_intensity_y;
	}
}
