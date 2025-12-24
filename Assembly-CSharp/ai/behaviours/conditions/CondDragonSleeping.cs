using System;

namespace ai.behaviours.conditions
{
	// Token: 0x02000983 RID: 2435
	public class CondDragonSleeping : BehaviourActorCondition
	{
		// Token: 0x060046FE RID: 18174 RVA: 0x001E20D4 File Offset: 0x001E02D4
		public override bool check(Actor pActor)
		{
			BehaviourTaskActor task = pActor.ai.task;
			string tCurrentTask = (task != null) ? task.id : null;
			return tCurrentTask == "dragon_sleep" || tCurrentTask == "dragon_wakeup";
		}
	}
}
