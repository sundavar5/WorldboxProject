using System;

namespace ai.behaviours
{
	// Token: 0x020008C5 RID: 2245
	public class BehFindMeatInsectSource : BehFindMeatSource
	{
		// Token: 0x060044F0 RID: 17648 RVA: 0x001CFD78 File Offset: 0x001CDF78
		public BehFindMeatInsectSource(bool pCheckForFactions = true) : base(MeatTargetType.Insect, pCheckForFactions)
		{
		}
	}
}
