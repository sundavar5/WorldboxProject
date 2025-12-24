using System;

// Token: 0x0200027E RID: 638
public static class DiplomacyHelpersRebellion
{
	// Token: 0x060017DB RID: 6107 RVA: 0x000E8DC4 File Offset: 0x000E6FC4
	public static void startRebellion(Actor pActor, Plot pPlot, bool pCheckForHappiness)
	{
		City tMainCity = pActor.city;
		Kingdom tOldKingdom = tMainCity.kingdom;
		if (pActor.isCityLeader())
		{
			pActor.city.removeLeader();
		}
		Kingdom tNewKingdom = tMainCity.makeOwnKingdom(pActor, true, false);
		using (ListPool<City> tCitiesInRebellion = new ListPool<City>())
		{
			tCitiesInRebellion.Add(tMainCity);
			pActor.joinCity(tMainCity);
			War tRebellionWar = null;
			foreach (War tWar in tOldKingdom.getWars(false))
			{
				if (tWar.isMainAttacker(tOldKingdom) && tWar.getAsset() == WarTypeLibrary.rebellion)
				{
					tRebellionWar = tWar;
					tRebellionWar.joinDefenders(tNewKingdom);
					break;
				}
			}
			if (tRebellionWar == null)
			{
				tRebellionWar = World.world.diplomacy.startWar(tOldKingdom, tNewKingdom, WarTypeLibrary.rebellion, true);
				if (tOldKingdom.hasAlliance())
				{
					foreach (Kingdom tKingdom in tOldKingdom.getAlliance().kingdoms_hashset)
					{
						if (tKingdom != tOldKingdom && tKingdom.isOpinionTowardsKingdomGood(tOldKingdom))
						{
							tRebellionWar.joinAttackers(tKingdom);
						}
					}
				}
			}
			foreach (Actor actor in pPlot.units)
			{
				City tCity = actor.city;
				if (tCity != null && tCity.kingdom != tNewKingdom && tCity.kingdom == tOldKingdom)
				{
					tCity.joinAnotherKingdom(tNewKingdom, false, true);
				}
			}
			int tCurCities = tOldKingdom.countCities();
			int tMaxCitiesNew = tNewKingdom.getMaxCities();
			tMaxCitiesNew -= tCitiesInRebellion.Count;
			if (tMaxCitiesNew < 0)
			{
				tMaxCitiesNew = 0;
			}
			if (tMaxCitiesNew > tCurCities / 3)
			{
				tMaxCitiesNew = (int)((float)tCurCities / 3f);
			}
			for (int i = 0; i < tMaxCitiesNew; i++)
			{
				if (!DiplomacyHelpersRebellion.checkMoreAlignedCities(tNewKingdom, tOldKingdom, tCitiesInRebellion, pCheckForHappiness))
				{
					break;
				}
			}
		}
	}

	// Token: 0x060017DC RID: 6108 RVA: 0x000E8FFC File Offset: 0x000E71FC
	public static bool checkMoreAlignedCities(Kingdom pNewKingdom, Kingdom pOldKingdom, ListPool<City> pNewCities, bool pCheckForHappiness)
	{
		bool result;
		using (ListPool<City> tTempCities = new ListPool<City>(World.world.cities.Count))
		{
			DiplomacyHelpersRebellion.addNeighbourCities(tTempCities, pNewCities);
			if (tTempCities.Count == 0)
			{
				tTempCities.AddRange(pOldKingdom.getCities());
			}
			if (tTempCities.Count == 0)
			{
				result = false;
			}
			else
			{
				foreach (City tCity in tTempCities.LoopRandom<City>())
				{
					if (tCity.kingdom == pOldKingdom && !tCity.isCapitalCity() && (!pCheckForHappiness || !tCity.isHappy()) && !Randy.randomBool())
					{
						tCity.joinAnotherKingdom(pNewKingdom, false, true);
						return true;
					}
				}
				result = true;
			}
		}
		return result;
	}

	// Token: 0x060017DD RID: 6109 RVA: 0x000E90CC File Offset: 0x000E72CC
	private static void addNeighbourCities(ListPool<City> pTempCitiesToCheck, ListPool<City> pRebelledCities)
	{
		foreach (City ptr in pRebelledCities)
		{
			ptr.recalculateNeighbourCities();
		}
		foreach (City ptr2 in pRebelledCities)
		{
			City tCity = ptr2;
			pTempCitiesToCheck.AddRange(tCity.neighbours_cities);
		}
	}
}
