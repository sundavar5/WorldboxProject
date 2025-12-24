using System;

namespace ai.behaviours
{
	// Token: 0x020008E6 RID: 2278
	public class BehLookAtBuildingTarget : BehActorBuildingTarget
	{
		// Token: 0x0600453F RID: 17727 RVA: 0x001D17BD File Offset: 0x001CF9BD
		public BehLookAtBuildingTarget(float pTimer = 0.3f)
		{
			this._timer = pTimer;
		}

		// Token: 0x06004540 RID: 17728 RVA: 0x001D17CC File Offset: 0x001CF9CC
		public override BehResult execute(Actor pActor)
		{
			pActor.lookTowardsPosition(pActor.beh_building_target.current_position);
			pActor.timer_action = this._timer;
			return BehResult.Continue;
		}

		// Token: 0x0400319E RID: 12702
		private float _timer;
	}
}
