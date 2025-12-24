using System;

namespace ai.behaviours
{
	// Token: 0x02000976 RID: 2422
	public class KingdomBehRandomWait : BehaviourActionKingdom
	{
		// Token: 0x060046E3 RID: 18147 RVA: 0x001E1C8A File Offset: 0x001DFE8A
		public override bool shouldRetry(Kingdom pObject)
		{
			return false;
		}

		// Token: 0x060046E4 RID: 18148 RVA: 0x001E1C8D File Offset: 0x001DFE8D
		public KingdomBehRandomWait(float pMin = 0f, float pMax = 1f)
		{
			this.min = pMin;
			this.max = pMax;
		}

		// Token: 0x060046E5 RID: 18149 RVA: 0x001E1CA3 File Offset: 0x001DFEA3
		public override BehResult execute(Kingdom pKingdom)
		{
			pKingdom.timer_action = Randy.randomFloat(this.min, this.max);
			return BehResult.Continue;
		}

		// Token: 0x040031FC RID: 12796
		private float min;

		// Token: 0x040031FD RID: 12797
		private float max;
	}
}
