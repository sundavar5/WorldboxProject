using System;
using System.Collections.Generic;
using UnityPools;

namespace ai.behaviours
{
	// Token: 0x02000975 RID: 2421
	public class KingdomBehCheckKing : BehaviourActionKingdom
	{
		// Token: 0x060046DA RID: 18138 RVA: 0x001E1584 File Offset: 0x001DF784
		public override BehResult execute(Kingdom pKingdom)
		{
			if (pKingdom.data.timer_new_king > 0f)
			{
				return BehResult.Continue;
			}
			if (pKingdom.hasKing())
			{
				Actor tKing = pKingdom.king;
				if (tKing.isAlive())
				{
					this.tryToGiveGoldenTooth(tKing);
					this.checkClanCreation(tKing);
					return BehResult.Continue;
				}
			}
			pKingdom.clearKingData();
			if (pKingdom.data.royal_clan_id != -1L)
			{
				Clan tPreviousRoyalClan = BehaviourActionBase<Kingdom>.world.clans.get(pKingdom.data.royal_clan_id);
				bool tRoyalClanExists = !tPreviousRoyalClan.isRekt();
				Actor tNewKing = this.findKingFromRoyalClan(pKingdom);
				if (tNewKing == null)
				{
					if (pKingdom.countCities() == 1)
					{
						if (pKingdom.capital != null && pKingdom.capital.hasLeader())
						{
							Actor tCapitalLeader = pKingdom.capital.leader;
							pKingdom.capital.removeLeader();
							tCapitalLeader.stopBeingWarrior();
							pKingdom.setKing(tCapitalLeader, false);
						}
					}
					else
					{
						this.checkKingdomChaos(pKingdom);
					}
				}
				else if (pKingdom.hasCulture() && pKingdom.culture.hasTrait("shattered_crown") && tRoyalClanExists)
				{
					this.checkShatteredCrownEvent(pKingdom, tNewKing, tPreviousRoyalClan);
				}
				if (!tRoyalClanExists)
				{
					pKingdom.data.royal_clan_id = -1L;
				}
			}
			else
			{
				Actor tNewKing2 = SuccessionTool.getKingFromLeaders(pKingdom);
				if (tNewKing2 != null)
				{
					this.makeKingAndMoveToCapital(pKingdom, tNewKing2);
				}
				else
				{
					this.checkKingdomChaos(pKingdom);
				}
			}
			return BehResult.Continue;
		}

		// Token: 0x060046DB RID: 18139 RVA: 0x001E16C0 File Offset: 0x001DF8C0
		private void checkKingdomChaos(Kingdom pMainKingdom)
		{
			bool tKingdomFractured = false;
			using (ListPool<City> tTempCities = new ListPool<City>(pMainKingdom.countCities()))
			{
				foreach (City tCity in pMainKingdom.getCities())
				{
					if (tCity != pMainKingdom.capital && tCity.hasLeader())
					{
						tTempCities.Add(tCity);
					}
				}
				if (tTempCities.Count != 0)
				{
					foreach (City tCity2 in tTempCities.LoopRandom<City>())
					{
						Actor tActor = tCity2.leader;
						if (tActor != null && tActor.isAlive())
						{
							tCity2.makeOwnKingdom(tActor, false, true);
							tKingdomFractured = true;
						}
					}
					if (tKingdomFractured)
					{
						if (pMainKingdom.hasAlliance())
						{
							pMainKingdom.getAlliance().leave(pMainKingdom, true);
						}
						WorldLog.logFracturedKingdom(pMainKingdom);
					}
				}
			}
		}

		// Token: 0x060046DC RID: 18140 RVA: 0x001E17C8 File Offset: 0x001DF9C8
		private void checkShatteredCrownEvent(Kingdom pMainKingdom, Actor pMainKing, Clan pRoyalClan)
		{
			if (!this.isRebellionsEnabled())
			{
				return;
			}
			if (pRoyalClan == null)
			{
				return;
			}
			using (ListPool<Actor> tTempActors = new ListPool<Actor>(pRoyalClan.units.Count))
			{
				using (ListPool<City> tTempCities = new ListPool<City>(pMainKingdom.countCities()))
				{
					foreach (Actor tActor in pRoyalClan.units)
					{
						if (!tActor.isRekt() && tActor != pMainKing && !tActor.isKing())
						{
							tTempActors.Add(tActor);
						}
					}
					foreach (City tCity in pMainKingdom.getCities())
					{
						if (tCity != pMainKingdom.capital)
						{
							tTempCities.Add(tCity);
						}
					}
					if (tTempActors.Count != 0)
					{
						if (tTempCities.Count != 0)
						{
							Dictionary<long, int> tKingdomCityCounter = UnsafeCollectionPool<Dictionary<long, int>, KeyValuePair<long, int>>.Get();
							using (ListPool<Kingdom> tNewKingdoms = new ListPool<Kingdom>())
							{
								tKingdomCityCounter[pMainKingdom.id] = pMainKingdom.countCities();
								tTempCities.Shuffle<City>();
								tTempActors.Shuffle<Actor>();
								tTempCities.Sort(delegate(City a, City b)
								{
									int tCountA = tTempActors.CountAll((Actor t) => t.city == a);
									int tCountB = tTempActors.CountAll((Actor t) => t.city == b);
									return tCountA.CompareTo(tCountB);
								});
								bool tKingdomShattered = false;
								while (tTempCities.Count > 0)
								{
									if (tTempActors.Count <= 0)
									{
										break;
									}
									City tCity2 = tTempCities.Pop<City>();
									Actor tActor2 = null;
									if (tCity2.hasLeader() && tTempActors.Contains(tCity2.leader))
									{
										tActor2 = tCity2.leader;
									}
									else
									{
										foreach (Actor ptr in tTempActors)
										{
											Actor tTempActor = ptr;
											if (tTempActor.city == tCity2)
											{
												tActor2 = tTempActor;
												break;
											}
										}
									}
									if (tActor2 == null)
									{
										tActor2 = tTempActors.Last<Actor>();
									}
									tTempActors.Remove(tActor2);
									if (tActor2.isCityLeader())
									{
										tActor2.city.removeLeader();
									}
									Kingdom tNewKingdom = tCity2.makeOwnKingdom(tActor2, false, true);
									tNewKingdoms.Add(tNewKingdom);
									tKingdomCityCounter[tNewKingdom.id] = 1;
									Dictionary<long, int> dictionary = tKingdomCityCounter;
									long id = pMainKingdom.id;
									int num = dictionary[id];
									dictionary[id] = num - 1;
									tActor2.stopBeingWarrior();
									tActor2.joinCity(tCity2);
									tKingdomShattered = true;
								}
								while (tTempCities.Count > 0)
								{
									City tCity3 = tTempCities.Pop<City>();
									Kingdom tKingdom = tNewKingdoms.GetRandom<Kingdom>();
									if (tKingdomCityCounter[tKingdom.id] < tKingdomCityCounter[pMainKingdom.id])
									{
										Dictionary<long, int> dictionary2 = tKingdomCityCounter;
										long id = tKingdom.id;
										int num = dictionary2[id];
										dictionary2[id] = num + 1;
										Dictionary<long, int> dictionary3 = tKingdomCityCounter;
										id = pMainKingdom.id;
										num = dictionary3[id];
										dictionary3[id] = num - 1;
										tCity3.joinAnotherKingdom(tKingdom, false, true);
									}
								}
								UnsafeCollectionPool<Dictionary<long, int>, KeyValuePair<long, int>>.Release(tKingdomCityCounter);
								if (tKingdomShattered)
								{
									if (pMainKingdom.hasAlliance())
									{
										pMainKingdom.getAlliance().leave(pMainKingdom, true);
									}
									WorldLog.logShatteredCrown(pMainKingdom);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060046DD RID: 18141 RVA: 0x001E1B84 File Offset: 0x001DFD84
		private void checkClanCreation(Actor pActor)
		{
			if (pActor.hasClan())
			{
				return;
			}
			BehaviourActionBase<Kingdom>.world.clans.newClan(pActor, true);
		}

		// Token: 0x060046DE RID: 18142 RVA: 0x001E1BA1 File Offset: 0x001DFDA1
		private void tryToGiveGoldenTooth(Actor pActor)
		{
			if (pActor.getAge() > 45 && Randy.randomChance(0.05f))
			{
				pActor.addTrait("golden_tooth", false);
			}
		}

		// Token: 0x060046DF RID: 18143 RVA: 0x001E1BC6 File Offset: 0x001DFDC6
		private bool isRebellionsEnabled()
		{
			return WorldLawLibrary.world_law_rebellions.isEnabled();
		}

		// Token: 0x060046E0 RID: 18144 RVA: 0x001E1BD4 File Offset: 0x001DFDD4
		private Actor findKingFromRoyalClan(Kingdom pKingdom)
		{
			Actor tNewKing = SuccessionTool.getKingFromRoyalClan(pKingdom, null);
			if (tNewKing == null && pKingdom.hasCulture() && (pKingdom.culture.hasTrait("unbroken_chain") || !this.isRebellionsEnabled()))
			{
				tNewKing = SuccessionTool.getKingFromLeaders(pKingdom);
			}
			if (tNewKing == null)
			{
				return null;
			}
			this.makeKingAndMoveToCapital(pKingdom, tNewKing);
			return tNewKing;
		}

		// Token: 0x060046E1 RID: 18145 RVA: 0x001E1C24 File Offset: 0x001DFE24
		private void makeKingAndMoveToCapital(Kingdom pKingdom, Actor pNewKing)
		{
			if (pNewKing.hasCity())
			{
				pNewKing.stopBeingWarrior();
				if (pNewKing.isCityLeader())
				{
					pNewKing.city.removeLeader();
				}
			}
			if (pKingdom.hasCapital() && pNewKing.city != pKingdom.capital)
			{
				pNewKing.joinCity(pKingdom.capital);
			}
			pKingdom.setKing(pNewKing, false);
			WorldLog.logNewKing(pKingdom);
		}
	}
}
