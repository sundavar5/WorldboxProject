using System;

namespace ai.behaviours
{
	// Token: 0x02000952 RID: 2386
	public class BehDragonSleep : BehDragon
	{
		// Token: 0x06004656 RID: 18006 RVA: 0x001DCF88 File Offset: 0x001DB188
		public override BehResult execute(Actor pActor)
		{
			pActor.setFlip(false);
			pActor.data.set("sleepy", 0);
			if (this.dragon.sleep_time == -1f)
			{
				this.dragon.sleep_time = Randy.randomFloat(10f, 80f);
			}
			this.dragon.sleep_time -= BehaviourActionBase<Actor>.world.elapsed;
			if (!pActor.hasMaxHealth() && Randy.randomChance(0.1f))
			{
				pActor.restoreHealth(1);
			}
			if (this.dragon.sleep_time > 0f)
			{
				return BehResult.RepeatStep;
			}
			this.dragon.sleep_time = -1f;
			return BehResult.Continue;
		}
	}
}
