using System;

namespace ai.behaviours
{
	// Token: 0x020008F3 RID: 2291
	public class BehRepeatTaskChance : BehaviourActionActor
	{
		// Token: 0x0600455E RID: 17758 RVA: 0x001D20DE File Offset: 0x001D02DE
		public BehRepeatTaskChance(float pChance = 0.5f)
		{
			this.chance = pChance;
		}

		// Token: 0x0600455F RID: 17759 RVA: 0x001D20ED File Offset: 0x001D02ED
		public override BehResult execute(Actor pActor)
		{
			if (Randy.randomChance(this.chance))
			{
				return BehResult.RestartTask;
			}
			return BehResult.Continue;
		}

		// Token: 0x040031A1 RID: 12705
		private float chance;
	}
}
