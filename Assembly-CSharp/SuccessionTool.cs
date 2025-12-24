using System;
using System.Collections.Generic;
using ai;

// Token: 0x02000283 RID: 643
public static class SuccessionTool
{
	// Token: 0x060018AF RID: 6319 RVA: 0x000EBC9C File Offset: 0x000E9E9C
	public static Actor findNextHeir(Kingdom pKingdom, Actor pExculdeActor = null)
	{
		return SuccessionTool.getKingFromRoyalClan(pKingdom, pExculdeActor);
	}

	// Token: 0x060018B0 RID: 6320 RVA: 0x000EBCA8 File Offset: 0x000E9EA8
	public static Actor getKingFromRoyalClan(Kingdom pKingdom, Actor pExcludeActor = null)
	{
		if (!pKingdom.data.royal_clan_id.hasValue())
		{
			return null;
		}
		Clan tClan = World.world.clans.get(pKingdom.data.royal_clan_id);
		if (tClan == null)
		{
			return null;
		}
		List<Actor> tMembers = tClan.units;
		Actor result;
		using (ListPool<Actor> tTempList = new ListPool<Actor>())
		{
			for (int i = 0; i < tMembers.Count; i++)
			{
				Actor tActor = tMembers[i];
				if (tActor != pExcludeActor && tClan.fitToRule(tActor, pKingdom))
				{
					tTempList.Add(tActor);
				}
			}
			if (tTempList.Count == 0)
			{
				result = null;
			}
			else if (pKingdom.hasCulture())
			{
				result = ListSorters.getUnitSortedByAgeAndTraits(tTempList, pKingdom.culture);
			}
			else
			{
				tTempList.Sort(new Comparison<Actor>(ListSorters.sortUnitByAgeOldFirst));
				result = tTempList[0];
			}
		}
		return result;
	}

	// Token: 0x060018B1 RID: 6321 RVA: 0x000EBD88 File Offset: 0x000E9F88
	public static Actor getKingFromLeaders(Kingdom pKingdom)
	{
		Actor tResult = null;
		Actor result;
		using (ListPool<Actor> tTempList = new ListPool<Actor>())
		{
			foreach (City tCity in pKingdom.getCities())
			{
				if (tCity.hasLeader())
				{
					tTempList.Add(tCity.leader);
				}
			}
			if (tTempList.Count == 0)
			{
				result = null;
			}
			else
			{
				int tCurrentKingDice = 0;
				foreach (Actor ptr in tTempList)
				{
					Actor tLeader = ptr;
					int tLeaderDice = ActorTool.attributeDice(tLeader, 2);
					if (tResult == null || tLeaderDice > tCurrentKingDice)
					{
						tCurrentKingDice = tLeaderDice;
						tResult = tLeader;
					}
				}
				if (tResult == null)
				{
					result = null;
				}
				else
				{
					result = tResult;
				}
			}
		}
		return result;
	}
}
