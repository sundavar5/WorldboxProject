using System;
using UnityEngine;

// Token: 0x0200004F RID: 79
public class LoyaltyLibrary : AssetLibrary<LoyaltyAsset>
{
	// Token: 0x06000310 RID: 784 RVA: 0x0001D1A0 File Offset: 0x0001B3A0
	public override void init()
	{
		base.init();
		LoyaltyAsset loyaltyAsset = new LoyaltyAsset();
		loyaltyAsset.id = "king_diplomacy";
		loyaltyAsset.translation_key = "loyalty_king";
		loyaltyAsset.calc = delegate(City pCity)
		{
			int tResult = 0;
			if (!pCity.kingdom.hasKing())
			{
				return tResult;
			}
			Actor tKing = pCity.kingdom.king;
			if (tKing.isAlive())
			{
				tResult = (int)(tKing.stats["diplomacy"] + tKing.stats["stewardship"] * 2f);
			}
			return tResult;
		};
		this.add(loyaltyAsset);
		LoyaltyAsset loyaltyAsset2 = new LoyaltyAsset();
		loyaltyAsset2.id = "leader_diplomacy";
		loyaltyAsset2.translation_key = "loyalty_leader";
		loyaltyAsset2.calc = delegate(City pCity)
		{
			int tResult = 0;
			if (!pCity.hasLeader())
			{
				return tResult;
			}
			Actor tLeader = pCity.leader;
			if (tLeader.isAlive())
			{
				tResult = -(int)(tLeader.stats["diplomacy"] + tLeader.stats["stewardship"] * 2f);
			}
			return tResult;
		};
		this.add(loyaltyAsset2);
		LoyaltyAsset loyaltyAsset3 = new LoyaltyAsset();
		loyaltyAsset3.id = "leader_loyalty";
		loyaltyAsset3.translation_key = "loyalty_traits";
		loyaltyAsset3.calc = delegate(City pCity)
		{
			int tResult = 0;
			if (pCity.hasLeader())
			{
				tResult = (int)pCity.leader.stats["loyalty_traits"];
			}
			if (pCity.hasLeader() && pCity.kingdom.hasKing())
			{
				int tBonus = AssetManager.traits.checkTraitsMod(pCity.leader, pCity.kingdom.king);
				tResult += tBonus;
			}
			return tResult;
		};
		this.add(loyaltyAsset3);
		LoyaltyAsset loyaltyAsset4 = new LoyaltyAsset();
		loyaltyAsset4.id = "population";
		loyaltyAsset4.translation_key = "loyalty_population";
		loyaltyAsset4.calc = delegate(City pCity)
		{
			int tResult = 0;
			if (pCity.isCapitalCity())
			{
				return tResult;
			}
			if (pCity.kingdom.hasCapital())
			{
				int popDiff = Mathf.Abs(pCity.status.population - pCity.kingdom.capital.status.population) / 3;
				if (popDiff > 30)
				{
					popDiff = 30;
				}
				if (pCity.status.population > pCity.kingdom.capital.status.population)
				{
					tResult = -popDiff;
				}
				else
				{
					tResult = popDiff;
				}
			}
			return tResult;
		};
		this.add(loyaltyAsset4);
		LoyaltyAsset loyaltyAsset5 = new LoyaltyAsset();
		loyaltyAsset5.id = "zones";
		loyaltyAsset5.translation_key = "loyalty_zones";
		loyaltyAsset5.calc = delegate(City pCity)
		{
			int tResult = 0;
			if (pCity.isCapitalCity())
			{
				return tResult;
			}
			if (pCity.kingdom.hasCapital())
			{
				int zoneDiff = Mathf.Abs(pCity.zones.Count - pCity.kingdom.capital.zones.Count) / 20;
				if (zoneDiff > 5)
				{
					zoneDiff = 5;
				}
				if (pCity.zones.Count > pCity.kingdom.capital.zones.Count)
				{
					tResult = -zoneDiff;
				}
				else
				{
					tResult = zoneDiff;
				}
			}
			return tResult;
		};
		this.add(loyaltyAsset5);
		LoyaltyAsset loyaltyAsset6 = new LoyaltyAsset();
		loyaltyAsset6.id = "distance";
		loyaltyAsset6.translation_key = "loyalty_distance";
		loyaltyAsset6.calc = delegate(City pCity)
		{
			int tResult = 0;
			if (pCity.isCapitalCity())
			{
				return tResult;
			}
			if (pCity.kingdom.hasCapital() && pCity.city_center.x != Globals.POINT_IN_VOID_2.x && pCity.kingdom.capital.city_center.x != Globals.POINT_IN_VOID_2.x)
			{
				tResult = -(int)(Toolbox.DistVec2Float(pCity.city_center, pCity.kingdom.capital.city_center) / 10f);
			}
			return tResult;
		};
		this.add(loyaltyAsset6);
		LoyaltyAsset loyaltyAsset7 = new LoyaltyAsset();
		loyaltyAsset7.id = "capital";
		loyaltyAsset7.translation_key = "loyalty_capital";
		loyaltyAsset7.calc = delegate(City pCity)
		{
			if (pCity.isCapitalCity())
			{
				return 1000;
			}
			return 0;
		};
		this.add(loyaltyAsset7);
		LoyaltyAsset loyaltyAsset8 = new LoyaltyAsset();
		loyaltyAsset8.id = "mood";
		loyaltyAsset8.translation_key = "loyalty_leader_mood";
		loyaltyAsset8.calc = delegate(City pCity)
		{
			int tResult = 0;
			if (pCity.hasLeader())
			{
				tResult = (int)pCity.leader.stats["loyalty_mood"];
			}
			return tResult;
		};
		this.add(loyaltyAsset8);
		LoyaltyAsset loyaltyAsset9 = new LoyaltyAsset();
		loyaltyAsset9.id = "new_city";
		loyaltyAsset9.translation_key = "loyalty_new_city";
		loyaltyAsset9.calc = delegate(City pCity)
		{
			int tResult = 0;
			int tAge = pCity.getAge();
			int tMaxAge = 15;
			if (tAge <= tMaxAge)
			{
				tResult = (tMaxAge - tAge) * 5;
			}
			return tResult;
		};
		this.add(loyaltyAsset9);
		LoyaltyAsset loyaltyAsset10 = new LoyaltyAsset();
		loyaltyAsset10.id = "new_kingdom";
		loyaltyAsset10.translation_key = "loyalty_new_kingdom";
		loyaltyAsset10.calc = delegate(City pCity)
		{
			int tResult = 0;
			int tAge = pCity.kingdom.getAge();
			if (tAge <= 5)
			{
				tResult = (5 - tAge) * 5;
			}
			return tResult;
		};
		this.add(loyaltyAsset10);
		LoyaltyAsset loyaltyAsset11 = new LoyaltyAsset();
		loyaltyAsset11.id = "cities";
		loyaltyAsset11.translation_key = "loyalty_number_of_cities";
		loyaltyAsset11.calc = delegate(City pCity)
		{
			int tResult = 0;
			if (pCity.isCapitalCity())
			{
				return 0;
			}
			int tMaxCities = pCity.kingdom.getMaxCities();
			int tCities = pCity.kingdom.countCities();
			if (tCities > tMaxCities)
			{
				tResult = (tMaxCities - tCities) * 25;
			}
			return tResult;
		};
		this.add(loyaltyAsset11);
		LoyaltyAsset loyaltyAsset12 = new LoyaltyAsset();
		loyaltyAsset12.id = "superior_enemies";
		loyaltyAsset12.translation_key = "loyalty_superior_enemies";
		loyaltyAsset12.calc = delegate(City pCity)
		{
			int tResult = 0;
			if (pCity.kingdom.hasEnemies())
			{
				int tPowerDiff = 0;
				using (ListPool<Kingdom> tListEnemies = World.world.wars.getEnemiesOf(pCity.kingdom))
				{
					foreach (Kingdom ptr in tListEnemies)
					{
						Kingdom tKingdom = ptr;
						tPowerDiff += tKingdom.power;
					}
					tResult = (tPowerDiff - pCity.kingdom.power) / 2;
					if (tResult < 0)
					{
						tResult = 0;
					}
					else if (tResult > 50)
					{
						tResult = 50;
					}
				}
			}
			return tResult;
		};
		this.add(loyaltyAsset12);
		LoyaltyAsset loyaltyAsset13 = new LoyaltyAsset();
		loyaltyAsset13.id = "close_to_capital";
		loyaltyAsset13.translation_key = "loyalty_close_to_capital";
		loyaltyAsset13.calc = delegate(City pCity)
		{
			int tResult = 0;
			if (pCity.isCapitalCity())
			{
				return 0;
			}
			if (pCity.kingdom.hasCapital() && City.nearbyBorders(pCity.kingdom.capital, pCity))
			{
				tResult = 20;
			}
			return tResult;
		};
		this.add(loyaltyAsset13);
		LoyaltyAsset loyaltyAsset14 = new LoyaltyAsset();
		loyaltyAsset14.id = "connected_to_capital";
		loyaltyAsset14.translation_key = "loyalty_connected_to_capital";
		loyaltyAsset14.translation_key_negative = "loyalty_not_connected_to_capital";
		loyaltyAsset14.calc = delegate(City pCity)
		{
			if (!pCity.kingdom.hasCapital())
			{
				return 0;
			}
			if (pCity.isCapitalCity())
			{
				return 0;
			}
			if (pCity.kingdom.capital.getSpecies() != pCity.getSpecies())
			{
				return 0;
			}
			if (pCity.isConnectedToCapital())
			{
				return 20;
			}
			return -35;
		};
		this.add(loyaltyAsset14);
		LoyaltyAsset loyaltyAsset15 = new LoyaltyAsset();
		loyaltyAsset15.id = "culture";
		loyaltyAsset15.translation_key = "loyalty_culture";
		loyaltyAsset15.translation_key_negative = "opinion_culture_different";
		loyaltyAsset15.calc = delegate(City pCity)
		{
			int tResult = 0;
			if (pCity.isCapitalCity())
			{
				return 0;
			}
			if (!pCity.hasCulture())
			{
				return 0;
			}
			if (pCity.kingdom.hasCapital())
			{
				if (pCity.kingdom.capital.culture == pCity.culture)
				{
					tResult = 15;
				}
				else
				{
					tResult = -25;
				}
			}
			return tResult;
		};
		this.add(loyaltyAsset15);
		LoyaltyAsset loyaltyAsset16 = new LoyaltyAsset();
		loyaltyAsset16.id = "language";
		loyaltyAsset16.translation_key = "loyalty_language";
		loyaltyAsset16.translation_key_negative = "opinion_language_different";
		loyaltyAsset16.calc = delegate(City pCity)
		{
			int tResult = 0;
			if (pCity.isCapitalCity())
			{
				return 0;
			}
			if (!pCity.hasLanguage())
			{
				return 0;
			}
			if (pCity.kingdom.hasCapital())
			{
				if (pCity.kingdom.capital.language == pCity.language)
				{
					tResult = 15;
				}
				else
				{
					tResult = -20;
				}
			}
			return tResult;
		};
		this.add(loyaltyAsset16);
		LoyaltyAsset loyaltyAsset17 = new LoyaltyAsset();
		loyaltyAsset17.id = "religion";
		loyaltyAsset17.translation_key = "loyalty_religion";
		loyaltyAsset17.translation_key_negative = "opinion_religion_different";
		loyaltyAsset17.calc = delegate(City pCity)
		{
			int tResult = 0;
			if (pCity.isCapitalCity())
			{
				return 0;
			}
			if (!pCity.hasReligion())
			{
				return 0;
			}
			if (pCity.kingdom.hasCapital())
			{
				if (pCity.kingdom.capital.religion == pCity.religion)
				{
					tResult = 15;
				}
				else
				{
					tResult = -30;
				}
			}
			return tResult;
		};
		this.add(loyaltyAsset17);
		LoyaltyAsset loyaltyAsset18 = new LoyaltyAsset();
		loyaltyAsset18.id = "species";
		loyaltyAsset18.translation_key = "loyalty_species";
		loyaltyAsset18.translation_key_negative = "loyalty_species_different";
		loyaltyAsset18.calc = delegate(City pCity)
		{
			int tResult = 0;
			if (pCity.isCapitalCity())
			{
				return 0;
			}
			if (pCity.kingdom.hasCapital())
			{
				if (pCity.kingdom.capital.getSpecies() == pCity.getSpecies())
				{
					tResult = 0;
				}
				else
				{
					if (pCity.hasLeader())
					{
						if (pCity.leader.hasXenophiles())
						{
							tResult = -5;
						}
						else
						{
							tResult = -25;
						}
					}
					else
					{
						tResult = -25;
					}
					if (pCity.hasLeader() && (pCity.leader.hasXenophobic() || (pCity.kingdom.hasKing() && pCity.kingdom.king.hasXenophobic())))
					{
						tResult = -50;
					}
				}
			}
			return tResult;
		};
		this.add(loyaltyAsset18);
		LoyaltyAsset loyaltyAsset19 = new LoyaltyAsset();
		loyaltyAsset19.id = "subspecies";
		loyaltyAsset19.translation_key = "loyalty_subspecies";
		loyaltyAsset19.translation_key_negative = "opinion_subspecies_different";
		loyaltyAsset19.calc = delegate(City pCity)
		{
			if (!pCity.kingdom.hasCapital())
			{
				return 0;
			}
			if (pCity.isCapitalCity())
			{
				return 0;
			}
			City tCapital = pCity.kingdom.capital;
			if (tCapital.getSpecies() != pCity.getSpecies())
			{
				return 0;
			}
			int tResult;
			if (tCapital.getMainSubspecies() == pCity.getMainSubspecies())
			{
				tResult = 15;
			}
			else
			{
				tResult = -15;
			}
			return tResult;
		};
		this.add(loyaltyAsset19);
		LoyaltyAsset loyaltyAsset20 = new LoyaltyAsset();
		loyaltyAsset20.id = "clan";
		loyaltyAsset20.translation_key = "loyalty_same_clan";
		loyaltyAsset20.translation_key_negative = "loyalty_different_clans";
		loyaltyAsset20.calc = delegate(City pCity)
		{
			int tResult = 0;
			if (pCity.isCapitalCity())
			{
				return 0;
			}
			Actor tLeader = pCity.leader;
			Actor tKing = pCity.kingdom.king;
			if (!pCity.hasLeader() || !pCity.kingdom.hasKing())
			{
				return 0;
			}
			if (tKing.subspecies != tLeader.subspecies)
			{
				return tResult;
			}
			if (pCity.leader.clan == pCity.kingdom.king.clan)
			{
				tResult = 30;
			}
			else
			{
				tResult = -20;
			}
			return tResult;
		};
		this.add(loyaltyAsset20);
		LoyaltyAsset loyaltyAsset21 = new LoyaltyAsset();
		loyaltyAsset21.id = "new_conquest";
		loyaltyAsset21.translation_key = "loyalty_new_conquest";
		loyaltyAsset21.calc = delegate(City pCity)
		{
			Kingdom tKingdom = pCity.kingdom;
			if (tKingdom.data.timestamp_new_conquest == -1.0)
			{
				return 0;
			}
			int tYears = Date.getYearsSince(tKingdom.data.timestamp_new_conquest);
			int tMaxYears = 10;
			if (tYears > tMaxYears)
			{
				return 0;
			}
			return (tMaxYears - tYears) * 30;
		};
		this.add(loyaltyAsset21);
		LoyaltyAsset loyaltyAsset22 = new LoyaltyAsset();
		loyaltyAsset22.id = "part_of_kingdom";
		loyaltyAsset22.translation_key = "loyalty_part_of_kingdom";
		loyaltyAsset22.calc = delegate(City pCity)
		{
			int tYears = Date.getYearsSince(pCity.data.timestamp_kingdom);
			int tMaxYears = 10;
			if (tYears > tMaxYears)
			{
				return 0;
			}
			return (tMaxYears - tYears) * 10;
		};
		this.add(loyaltyAsset22);
		LoyaltyAsset loyaltyAsset23 = new LoyaltyAsset();
		loyaltyAsset23.id = "supreme_kingdom";
		loyaltyAsset23.translation_key = "loyalty_supreme_kingdom";
		loyaltyAsset23.calc = delegate(City pCity)
		{
			if (World.world.kingdoms.Count <= 1)
			{
				return 0;
			}
			if (pCity.kingdom.isSupreme())
			{
				return 100;
			}
			return 0;
		};
		this.add(loyaltyAsset23);
		LoyaltyAsset loyaltyAsset24 = new LoyaltyAsset();
		loyaltyAsset24.id = "second_best_kingdom";
		loyaltyAsset24.translation_key = "loyalty_second_best";
		loyaltyAsset24.calc = delegate(City pCity)
		{
			if (World.world.kingdoms.Count <= 2)
			{
				return 0;
			}
			if (pCity.kingdom.isSecondBest())
			{
				return 50;
			}
			return 0;
		};
		this.add(loyaltyAsset24);
		LoyaltyAsset loyaltyAsset25 = new LoyaltyAsset();
		loyaltyAsset25.id = "king_rule";
		loyaltyAsset25.translation_key = "loyalty_king_ruled";
		loyaltyAsset25.calc = delegate(City pCity)
		{
			if (!pCity.kingdom.hasKing())
			{
				return 0;
			}
			int tKingRuleAge = Date.getYearsSince(pCity.kingdom.data.timestamp_king_rule);
			int tMinAge = 5;
			int tMaxAge = 40;
			if (tKingRuleAge < tMinAge)
			{
				return 0;
			}
			if (tKingRuleAge > tMaxAge)
			{
				return tMaxAge;
			}
			return tKingRuleAge;
		};
		this.add(loyaltyAsset25);
		LoyaltyAsset loyaltyAsset26 = new LoyaltyAsset();
		loyaltyAsset26.id = "loyalty_world_era";
		loyaltyAsset26.translation_key = "loyalty_world_era";
		loyaltyAsset26.calc = ((City pCity) => World.world_era.bonus_loyalty);
		this.add(loyaltyAsset26);
		LoyaltyAsset loyaltyAsset27 = new LoyaltyAsset();
		loyaltyAsset27.id = "loyalty_baby_king";
		loyaltyAsset27.translation_key = "loyalty_baby_king";
		loyaltyAsset27.calc = delegate(City pCity)
		{
			if (!pCity.kingdom.hasKing())
			{
				return 0;
			}
			if (pCity.kingdom.king.getAge() < 18)
			{
				return -50;
			}
			return 0;
		};
		this.add(loyaltyAsset27);
		LoyaltyAsset loyaltyAsset28 = new LoyaltyAsset();
		loyaltyAsset28.id = "opinion_patriarchy";
		loyaltyAsset28.translation_key = "opinion_patriarchy";
		loyaltyAsset28.calc = delegate(City pCity)
		{
			Culture tCulture = pCity.culture;
			Kingdom tKingdom = pCity.kingdom;
			if (!pCity.hasCulture())
			{
				return 0;
			}
			if (tCulture != tKingdom.culture)
			{
				return 0;
			}
			if (!tCulture.hasTrait("patriarchy"))
			{
				return 0;
			}
			if (!tKingdom.hasKing())
			{
				return 0;
			}
			if (tKingdom.king.isSexMale())
			{
				return 0;
			}
			return -50;
		};
		this.add(loyaltyAsset28);
		LoyaltyAsset loyaltyAsset29 = new LoyaltyAsset();
		loyaltyAsset29.id = "opinion_matriarchy";
		loyaltyAsset29.translation_key = "opinion_matriarchy";
		loyaltyAsset29.calc = delegate(City pCity)
		{
			Culture tCulture = pCity.culture;
			Kingdom tKingdom = pCity.kingdom;
			if (!pCity.hasCulture())
			{
				return 0;
			}
			if (tCulture != tKingdom.culture)
			{
				return 0;
			}
			if (!tCulture.hasTrait("matriarchy"))
			{
				return 0;
			}
			if (!tKingdom.hasKing())
			{
				return 0;
			}
			if (tKingdom.king.isSexFemale())
			{
				return 0;
			}
			return -50;
		};
		this.add(loyaltyAsset29);
	}

	// Token: 0x06000311 RID: 785 RVA: 0x0001DA0C File Offset: 0x0001BC0C
	public override void editorDiagnosticLocales()
	{
		foreach (LoyaltyAsset tAsset in this.list)
		{
			foreach (string tLocaleID in tAsset.getLocaleIDs())
			{
				this.checkLocale(tAsset, tLocaleID);
			}
		}
		base.editorDiagnosticLocales();
	}
}
