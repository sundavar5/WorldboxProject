using System;

namespace ai.behaviours
{
	// Token: 0x020008FA RID: 2298
	public class BehSetNextTask : BehaviourActionActor
	{
		// Token: 0x0600456B RID: 17771 RVA: 0x001D2247 File Offset: 0x001D0447
		public BehSetNextTask(string taskID, bool pClean = true, bool pForce = false)
		{
			this._task_id = taskID;
			this._clean = pClean;
			this._force = pForce;
		}

		// Token: 0x0600456C RID: 17772 RVA: 0x001D2264 File Offset: 0x001D0464
		public override BehResult execute(Actor pActor)
		{
			return base.forceTask(pActor, this._task_id, this._clean, this._force);
		}

		// Token: 0x040031AE RID: 12718
		private bool _clean;

		// Token: 0x040031AF RID: 12719
		private bool _force;

		// Token: 0x040031B0 RID: 12720
		private string _task_id;
	}
}
