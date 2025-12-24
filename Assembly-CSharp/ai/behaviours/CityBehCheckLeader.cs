using System;

namespace ai.behaviours
{
	// Token: 0x0200096E RID: 2414
	public class CityBehCheckLeader : BehaviourActionCity
	{
		// Token: 0x060046C2 RID: 18114 RVA: 0x001E09E0 File Offset: 0x001DEBE0
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.uses_clans = true;
		}

		// Token: 0x060046C3 RID: 18115 RVA: 0x001E09EF File Offset: 0x001DEBEF
		public override BehResult execute(City pCity)
		{
			this.checkLeaderClan(pCity);
			this.checkFindLeader(pCity);
			return BehResult.Continue;
		}

		// Token: 0x060046C4 RID: 18116 RVA: 0x001E0A00 File Offset: 0x001DEC00
		private void checkLeaderClan(City pCity)
		{
			if (!pCity.hasLeader())
			{
				return;
			}
			Actor tLeader = pCity.leader;
			if (tLeader.hasClan())
			{
				return;
			}
			BehaviourActionBase<City>.world.clans.newClan(tLeader, true);
		}

		// Token: 0x060046C5 RID: 18117 RVA: 0x001E0A38 File Offset: 0x001DEC38
		private void checkFindLeader(City pCity)
		{
			if (pCity.hasLeader())
			{
				return;
			}
			if (pCity.isGettingCaptured())
			{
				return;
			}
			Actor tBestActor = null;
			tBestActor = this.tryGetClanLeader(pCity);
			if (tBestActor != null)
			{
				if (tBestActor.city != pCity)
				{
					tBestActor.stopBeingWarrior();
				}
				tBestActor.joinCity(pCity);
				pCity.setLeader(tBestActor, true);
				return;
			}
			int tBestVal = 0;
			foreach (Actor tActor in pCity.units)
			{
				if (!tActor.isKing() && !tActor.isCityLeader())
				{
					int tDiceAmount = 1;
					if (tActor.is_profession_citizen)
					{
						if (tActor.isFavorite())
						{
							tDiceAmount += 2;
						}
						int tCurrent = ActorTool.attributeDice(tActor, tDiceAmount);
						if (tBestActor == null || tCurrent > tBestVal)
						{
							tBestActor = tActor;
							tBestVal = tCurrent;
						}
					}
				}
			}
			if (tBestActor != null)
			{
				pCity.setLeader(tBestActor, true);
			}
		}

		// Token: 0x060046C6 RID: 18118 RVA: 0x001E0B10 File Offset: 0x001DED10
		private Actor tryGetClanLeader(City pCity)
		{
			Kingdom tKingdom = pCity.kingdom;
			Clan tRoyalClan = null;
			if (tKingdom.data.royal_clan_id.hasValue())
			{
				tRoyalClan = BehaviourActionBase<City>.world.clans.get(tKingdom.data.royal_clan_id);
			}
			Actor result;
			using (ListPool<Actor> tPossibleRoyalClanMembers = new ListPool<Actor>())
			{
				using (ListPool<Actor> tPossibleClans = new ListPool<Actor>())
				{
					foreach (City city in tKingdom.getCities())
					{
						foreach (Actor tActor in city.getUnits())
						{
							if (tActor.isUnitFitToRule() && !tActor.isKing() && !tActor.isCityLeader() && tActor.hasClan())
							{
								if (tRoyalClan != null && tActor.clan == tRoyalClan)
								{
									tPossibleRoyalClanMembers.Add(tActor);
								}
								else
								{
									tPossibleClans.Add(tActor);
								}
							}
						}
					}
					Actor tResultActor = null;
					if (tPossibleRoyalClanMembers.Count > 0)
					{
						if (pCity.hasCulture())
						{
							result = ListSorters.getUnitSortedByAgeAndTraits(tPossibleRoyalClanMembers, pCity.culture);
						}
						else
						{
							tPossibleRoyalClanMembers.Sort(new Comparison<Actor>(ListSorters.sortUnitByAgeOldFirst));
							tResultActor = tPossibleRoyalClanMembers[0];
							result = tResultActor;
						}
					}
					else if (tPossibleClans.Count > 0)
					{
						if (pCity.hasCulture())
						{
							result = ListSorters.getUnitSortedByAgeAndTraits(tPossibleClans, pCity.culture);
						}
						else
						{
							tPossibleClans.Sort(new Comparison<Actor>(ListSorters.sortUnitByAgeOldFirst));
							tResultActor = tPossibleClans[0];
							result = tResultActor;
						}
					}
					else
					{
						result = tResultActor;
					}
				}
			}
			return result;
		}
	}
}
