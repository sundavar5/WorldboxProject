using System;
using System.Collections.Generic;

// Token: 0x0200027D RID: 637
public static class DiplomacyHelpers
{
	// Token: 0x060017CD RID: 6093 RVA: 0x000E86FC File Offset: 0x000E68FC
	public static bool isWarNeeded(Kingdom pKingdom)
	{
		if (!pKingdom.hasCities())
		{
			return false;
		}
		if (!pKingdom.hasCapital())
		{
			return false;
		}
		if (pKingdom.data.timestamp_last_war != -1.0 && Date.getYearsSince(pKingdom.data.timestamp_last_war) <= SimGlobals.m.diplomacy_years_war_timeout)
		{
			return false;
		}
		if (DiplomacyHelpers.wars.hasWars(pKingdom))
		{
			return false;
		}
		if (pKingdom.countTotalWarriors() <= SimGlobals.m.diplomacy_years_war_min_warriors)
		{
			return false;
		}
		float tCurPopulation = (float)pKingdom.getPopulationPeople();
		float tPopulationMax = (float)pKingdom.getPopulationTotalPossible();
		return pKingdom.countCities() >= 4 || tCurPopulation >= tPopulationMax * 0.6f;
	}

	// Token: 0x060017CE RID: 6094 RVA: 0x000E879C File Offset: 0x000E699C
	public static Kingdom getWarTarget(Kingdom pInitiatorKingdom)
	{
		Kingdom tBestTarget = null;
		float tBestFastDist = float.MaxValue;
		int tCurrentArmy = pInitiatorKingdom.countTotalWarriors();
		if (pInitiatorKingdom.hasAlliance())
		{
			tCurrentArmy = pInitiatorKingdom.getAlliance().countWarriors();
		}
		Kingdom result;
		using (ListPool<Kingdom> tPossibleKingdomsList = DiplomacyHelpers.wars.getNeutralKingdoms(pInitiatorKingdom, false, false))
		{
			foreach (Kingdom ptr in tPossibleKingdomsList)
			{
				Kingdom tTargetKingdom = ptr;
				if (tTargetKingdom.hasCities() && tTargetKingdom.hasCapital() && tTargetKingdom.getAge() >= SimGlobals.m.minimum_kingdom_age_for_attack)
				{
					int tTargetArmy;
					if (tTargetKingdom.hasAlliance())
					{
						tTargetArmy = tTargetKingdom.getAlliance().countWarriors();
					}
					else
					{
						tTargetArmy = tTargetKingdom.countTotalWarriors();
					}
					if (tCurrentArmy >= tTargetArmy && pInitiatorKingdom.capital.reachableFrom(tTargetKingdom.capital) && (float)Date.getYearsSince(DiplomacyHelpers.diplomacy.getRelation(pInitiatorKingdom, tTargetKingdom).data.timestamp_last_war_ended) >= (float)SimGlobals.m.minimum_years_between_wars && !pInitiatorKingdom.isOpinionTowardsKingdomGood(tTargetKingdom))
					{
						float tFastDist = Kingdom.distanceBetweenKingdom(pInitiatorKingdom, tTargetKingdom);
						if (tFastDist < tBestFastDist)
						{
							tBestFastDist = tFastDist;
							tBestTarget = tTargetKingdom;
						}
					}
				}
			}
			result = tBestTarget;
		}
		return result;
	}

	// Token: 0x060017CF RID: 6095 RVA: 0x000E88F0 File Offset: 0x000E6AF0
	public static Kingdom getAllianceTarget(Kingdom pKingdomStarter)
	{
		if (pKingdomStarter.isSupreme())
		{
			return null;
		}
		Kingdom result;
		using (ListPool<Kingdom> tKingdoms = World.world.wars.getNeutralKingdoms(pKingdomStarter, true, true))
		{
			if (tKingdoms.Count == 0)
			{
				result = null;
			}
			else
			{
				foreach (Kingdom tKingdom in tKingdoms.LoopRandom<Kingdom>())
				{
					if (tKingdom.hasKing() && !tKingdom.isSupreme() && !tKingdom.king.hasPlot() && pKingdomStarter.isOpinionTowardsKingdomGood(tKingdom) && tKingdom.getRenown() >= PlotsLibrary.alliance_create.min_renown_kingdom)
					{
						bool tGoodKingdomTarget = false;
						if (pKingdomStarter.countCities() <= 2 && tKingdom.countCities() <= 2 && !pKingdomStarter.hasNearbyKingdoms() && !tKingdom.hasNearbyKingdoms())
						{
							tGoodKingdomTarget = true;
						}
						if (!tGoodKingdomTarget && DiplomacyHelpers.areKingdomsClose(tKingdom, pKingdomStarter))
						{
							tGoodKingdomTarget = true;
						}
						if (tGoodKingdomTarget)
						{
							return tKingdom;
						}
					}
				}
				result = null;
			}
		}
		return result;
	}

	// Token: 0x060017D0 RID: 6096 RVA: 0x000E89F8 File Offset: 0x000E6BF8
	public static bool areKingdomsClose(Kingdom pMain, Kingdom pTarget)
	{
		foreach (City tCityMain in pMain.getCities())
		{
			foreach (City tCityTarget in pTarget.getCities())
			{
				if (City.nearbyBorders(tCityMain, tCityTarget))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060017D1 RID: 6097 RVA: 0x000E8A88 File Offset: 0x000E6C88
	public static bool isThereActiveCityConquest(Kingdom pKingdom, Kingdom pTargetKingdom)
	{
		using (IEnumerator<City> enumerator = pKingdom.getCities().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isGettingCapturedBy(pTargetKingdom))
				{
					return true;
				}
			}
		}
		using (IEnumerator<City> enumerator = pTargetKingdom.getCities().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isGettingCapturedBy(pKingdom))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060017D2 RID: 6098 RVA: 0x000E8B1C File Offset: 0x000E6D1C
	public static bool isThereFightBetween(Kingdom pKingdom1, Kingdom pKingdom2)
	{
		return DiplomacyHelpers.isThereActiveCityFight(pKingdom1, pKingdom2) || DiplomacyHelpers.isThereActiveCityFight(pKingdom2, pKingdom1);
	}

	// Token: 0x060017D3 RID: 6099 RVA: 0x000E8B38 File Offset: 0x000E6D38
	private static bool isThereActiveCityFight(Kingdom pDefenderKingdom, Kingdom pAttackerKingdom)
	{
		foreach (City tCity in pDefenderKingdom.getCities())
		{
			if (tCity.hasArmy())
			{
				Army tArmy = tCity.army;
				if (tArmy.hasCaptain())
				{
					Actor tArmyLeader = tArmy.getCaptain();
					if (tArmyLeader.current_tile.hasCity() && tArmyLeader.current_tile.zone_city.kingdom == pAttackerKingdom)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060017D4 RID: 6100 RVA: 0x000E8BC8 File Offset: 0x000E6DC8
	public static bool areDefendersGettingCaptured(this War pWar)
	{
		using (IEnumerator<Kingdom> enumerator = pWar.getDefenders().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isGettingCaptured())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060017D5 RID: 6101 RVA: 0x000E8C1C File Offset: 0x000E6E1C
	public static bool areAttackersGettingCaptured(this War pWar)
	{
		using (IEnumerator<Kingdom> enumerator = pWar.getAttackers().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isGettingCaptured())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060017D6 RID: 6102 RVA: 0x000E8C70 File Offset: 0x000E6E70
	public static bool areAttackersAttackingAnotherCity(this War pWar)
	{
		using (IEnumerator<Kingdom> enumerator = pWar.getAttackers().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isAttackingAnotherCity())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060017D7 RID: 6103 RVA: 0x000E8CC4 File Offset: 0x000E6EC4
	public static bool areDefendersAttackingAnotherCity(this War pWar)
	{
		using (IEnumerator<Kingdom> enumerator = pWar.getDefenders().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isAttackingAnotherCity())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060017D8 RID: 6104 RVA: 0x000E8D18 File Offset: 0x000E6F18
	public static bool isAttackingAnotherCity(this Kingdom pAttackerKingdom)
	{
		foreach (City tCity in pAttackerKingdom.getCities())
		{
			if (tCity.hasArmy())
			{
				Army tArmy = tCity.army;
				if (tArmy.hasCaptain())
				{
					Actor tArmyLeader = tArmy.getCaptain();
					if (tArmyLeader.current_tile.hasCity() && tArmyLeader.current_tile.zone_city.kingdom.isEnemy(pAttackerKingdom))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x17000166 RID: 358
	// (get) Token: 0x060017D9 RID: 6105 RVA: 0x000E8DAC File Offset: 0x000E6FAC
	public static WarManager wars
	{
		get
		{
			return World.world.wars;
		}
	}

	// Token: 0x17000167 RID: 359
	// (get) Token: 0x060017DA RID: 6106 RVA: 0x000E8DB8 File Offset: 0x000E6FB8
	public static DiplomacyManager diplomacy
	{
		get
		{
			return World.world.diplomacy;
		}
	}
}
