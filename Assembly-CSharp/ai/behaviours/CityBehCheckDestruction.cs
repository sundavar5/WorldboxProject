using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x0200096D RID: 2413
	public class CityBehCheckDestruction : BehaviourActionCity
	{
		// Token: 0x060046C0 RID: 18112 RVA: 0x001E08D0 File Offset: 0x001DEAD0
		public override BehResult execute(City pCity)
		{
			if (!pCity.isGettingCaptured())
			{
				return BehResult.Continue;
			}
			Kingdom tAttacker = pCity.getCapturingKingdom();
			if (tAttacker.isRekt())
			{
				return BehResult.Continue;
			}
			Actor king = tAttacker.king;
			if (king == null || !king.hasXenophobic())
			{
				return BehResult.Continue;
			}
			if (tAttacker.getSpecies() == pCity.kingdom.getSpecies())
			{
				return BehResult.Continue;
			}
			using (IEnumerator<Actor> enumerator = pCity.getUnits().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.current_zone.city == pCity)
					{
						return BehResult.Continue;
					}
				}
			}
			pCity.kingdom.decreaseHappinessFromRazedCity(pCity);
			tAttacker.increaseHappinessFromDestroyingCity();
			foreach (Actor actor in pCity.getUnits())
			{
				actor.stopBeingWarrior();
				actor.joinCity(null);
			}
			if (pCity.hasLeader())
			{
				pCity.removeLeader();
			}
			return BehResult.Continue;
		}
	}
}
