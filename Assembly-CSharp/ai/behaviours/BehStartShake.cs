using System;

namespace ai.behaviours
{
	// Token: 0x02000900 RID: 2304
	public class BehStartShake : BehaviourActionActor
	{
		// Token: 0x0600457B RID: 17787 RVA: 0x001D2628 File Offset: 0x001D0828
		public BehStartShake(float pTimerShake = 1f, float pTimeWaitAction = 0f)
		{
			this._timer_shake = pTimerShake;
			this._wait_action = pTimeWaitAction;
		}

		// Token: 0x0600457C RID: 17788 RVA: 0x001D263E File Offset: 0x001D083E
		public override BehResult execute(Actor pActor)
		{
			pActor.startShake(this._timer_shake, 0.1f, true, true);
			pActor.timer_action = this._wait_action;
			return BehResult.Continue;
		}

		// Token: 0x040031B6 RID: 12726
		private float _timer_shake;

		// Token: 0x040031B7 RID: 12727
		private float _wait_action;
	}
}
