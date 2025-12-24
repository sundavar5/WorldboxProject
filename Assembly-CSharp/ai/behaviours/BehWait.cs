using System;

namespace ai.behaviours
{
	// Token: 0x0200090C RID: 2316
	public class BehWait : BehaviourActionActor
	{
		// Token: 0x06004596 RID: 17814 RVA: 0x001D2C7F File Offset: 0x001D0E7F
		public BehWait(float pWaitInterval = 1f)
		{
			this._wait_interval = pWaitInterval;
		}

		// Token: 0x06004597 RID: 17815 RVA: 0x001D2C8E File Offset: 0x001D0E8E
		public override BehResult execute(Actor pActor)
		{
			pActor.timer_action = this._wait_interval;
			return BehResult.Continue;
		}

		// Token: 0x040031BB RID: 12731
		private float _wait_interval;
	}
}
