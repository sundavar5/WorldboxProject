using System;

namespace ai.behaviours
{
	// Token: 0x0200096A RID: 2410
	public class CityBehCheckArmy : BehaviourActionCity
	{
		// Token: 0x060046B3 RID: 18099 RVA: 0x001E0028 File Offset: 0x001DE228
		public override BehResult execute(City pCity)
		{
			pCity.checkArmyExistence();
			if (pCity.hasArmy())
			{
				return BehResult.Continue;
			}
			if (!pCity.hasAnyWarriors())
			{
				return BehResult.Continue;
			}
			Actor tActor = pCity.getRandomWarrior();
			if (tActor == null)
			{
				return BehResult.Continue;
			}
			BehaviourActionBase<City>.world.armies.newArmy(tActor, pCity);
			return BehResult.Continue;
		}
	}
}
