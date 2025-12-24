using System;

namespace ai.behaviours
{
	// Token: 0x02000971 RID: 2417
	public class CityBehRandomWait : BehaviourActionCity
	{
		// Token: 0x060046CE RID: 18126 RVA: 0x001E0ECB File Offset: 0x001DF0CB
		public override bool shouldRetry(City pObject)
		{
			return false;
		}

		// Token: 0x060046CF RID: 18127 RVA: 0x001E0ECE File Offset: 0x001DF0CE
		public CityBehRandomWait(float pMin = 0f, float pMax = 1f)
		{
			this.min = pMin;
			this.max = pMax;
		}

		// Token: 0x060046D0 RID: 18128 RVA: 0x001E0EE4 File Offset: 0x001DF0E4
		public override BehResult execute(City pCity)
		{
			pCity.timer_action = Randy.randomFloat(this.min, this.max);
			return BehResult.Continue;
		}

		// Token: 0x040031FA RID: 12794
		private float min;

		// Token: 0x040031FB RID: 12795
		private float max;
	}
}
