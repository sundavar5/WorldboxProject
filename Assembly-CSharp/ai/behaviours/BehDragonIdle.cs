using System;

namespace ai.behaviours
{
	// Token: 0x0200094E RID: 2382
	public class BehDragonIdle : BehDragon
	{
		// Token: 0x0600464E RID: 17998 RVA: 0x001DCD60 File Offset: 0x001DAF60
		public override BehResult execute(Actor pActor)
		{
			if (this.dragon.aggroTargets.Count > 0)
			{
				return BehResult.Continue;
			}
			if (this.dragon.idle_time == -1f)
			{
				this.dragon.idle_time = Randy.randomFloat(1f, 3f);
			}
			this.dragon.idle_time -= BehaviourActionBase<Actor>.world.elapsed;
			if (this.dragon.idle_time > 0f)
			{
				return BehResult.RepeatStep;
			}
			this.dragon.idle_time = -1f;
			return BehResult.Continue;
		}
	}
}
