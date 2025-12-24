using System;

namespace ai.behaviours
{
	// Token: 0x020008E5 RID: 2277
	public class BehJumpingAnimation : BehaviourActionActor
	{
		// Token: 0x0600453D RID: 17725 RVA: 0x001D178C File Offset: 0x001CF98C
		public BehJumpingAnimation(float pTimerAction, float pTimerJumpAnimation)
		{
			this._timer_action = pTimerAction;
			this._timer_jumping = pTimerJumpAnimation;
		}

		// Token: 0x0600453E RID: 17726 RVA: 0x001D17A2 File Offset: 0x001CF9A2
		public override BehResult execute(Actor pActor)
		{
			pActor.timer_jump_animation = this._timer_jumping;
			pActor.timer_action = this._timer_action;
			return BehResult.Continue;
		}

		// Token: 0x0400319C RID: 12700
		private float _timer_action;

		// Token: 0x0400319D RID: 12701
		private float _timer_jumping;
	}
}
