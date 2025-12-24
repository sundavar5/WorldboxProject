using System;

namespace ai.behaviours
{
	// Token: 0x02000963 RID: 2403
	public class BehUFOCheckExplore : BehaviourActionActor
	{
		// Token: 0x06004680 RID: 18048 RVA: 0x001DE734 File Offset: 0x001DC934
		public override BehResult execute(Actor pActor)
		{
			int exploringTicks;
			pActor.data.get("exploringTicks", out exploringTicks, 0);
			if (exploringTicks <= 0)
			{
				return BehResult.Continue;
			}
			exploringTicks--;
			pActor.data.set("exploringTicks", exploringTicks);
			if (pActor.current_tile.zone.city != null)
			{
				pActor.data.set("cityToAttack", pActor.current_tile.zone.city.data.id);
				pActor.data.set("attacksForCity", Randy.randomInt(3, 10));
				return base.forceTask(pActor, "ufo_fly", false, false);
			}
			BehaviourTaskActor task = pActor.ai.task;
			if (((task != null) ? task.id : null) == "ufo_explore")
			{
				return BehResult.RestartTask;
			}
			return base.forceTask(pActor, "ufo_explore", false, false);
		}
	}
}
