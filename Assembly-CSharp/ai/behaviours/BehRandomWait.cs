using System;

namespace ai.behaviours
{
	// Token: 0x020008F1 RID: 2289
	public class BehRandomWait : BehaviourActionActor
	{
		// Token: 0x0600455A RID: 17754 RVA: 0x001D2007 File Offset: 0x001D0207
		public BehRandomWait(float pMin = 0f, float pMax = 1f, bool pLand = false)
		{
			this.min = pMin;
			this.max = pMax;
			this.land_if_hovering = pLand;
		}

		// Token: 0x0600455B RID: 17755 RVA: 0x001D2024 File Offset: 0x001D0224
		public override BehResult execute(Actor pActor)
		{
			pActor.timer_action = Randy.randomFloat(this.min, this.max);
			return BehResult.Continue;
		}

		// Token: 0x0400319F RID: 12703
		private float min;

		// Token: 0x040031A0 RID: 12704
		private float max;
	}
}
