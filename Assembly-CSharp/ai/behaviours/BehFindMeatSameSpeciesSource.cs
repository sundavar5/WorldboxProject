using System;

namespace ai.behaviours
{
	// Token: 0x020008C6 RID: 2246
	public class BehFindMeatSameSpeciesSource : BehFindMeatSource
	{
		// Token: 0x060044F1 RID: 17649 RVA: 0x001CFD82 File Offset: 0x001CDF82
		public BehFindMeatSameSpeciesSource(bool pCheckForFactions) : base(MeatTargetType.MeatSameSpecies, pCheckForFactions)
		{
		}
	}
}
