using System;

namespace ai.behaviours
{
	// Token: 0x02000974 RID: 2420
	public class KingdomBehCheckCapital : BehaviourActionKingdom
	{
		// Token: 0x060046D8 RID: 18136 RVA: 0x001E14E8 File Offset: 0x001DF6E8
		public override BehResult execute(Kingdom pKingdom)
		{
			if (!pKingdom.hasCities())
			{
				return BehResult.Continue;
			}
			if (pKingdom.hasCapital() && pKingdom.capital.isAlive())
			{
				return BehResult.Continue;
			}
			City tBest = null;
			foreach (City tCity in pKingdom.cities)
			{
				if (tBest == null || tCity.buildings.Count > tBest.buildings.Count)
				{
					tBest = tCity;
				}
			}
			pKingdom.setCapital(tBest);
			return BehResult.Continue;
		}
	}
}
