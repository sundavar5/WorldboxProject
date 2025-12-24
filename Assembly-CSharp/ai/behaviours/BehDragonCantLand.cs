using System;

namespace ai.behaviours
{
	// Token: 0x02000945 RID: 2373
	public class BehDragonCantLand : BehaviourActionActor
	{
		// Token: 0x0600463C RID: 17980 RVA: 0x001DC7C8 File Offset: 0x001DA9C8
		public BehDragonCantLand(string pNextAction)
		{
			this.task_id = pNextAction;
		}

		// Token: 0x0600463D RID: 17981 RVA: 0x001DC7D7 File Offset: 0x001DA9D7
		public override BehResult execute(Actor pActor)
		{
			if (!Dragon.canLand(pActor, null))
			{
				return base.forceTask(pActor, this.task_id, true, false);
			}
			return BehResult.Continue;
		}

		// Token: 0x040031E0 RID: 12768
		private string task_id;
	}
}
