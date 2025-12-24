using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020001EB RID: 491
public static class TraitTools
{
	// Token: 0x06000E22 RID: 3618 RVA: 0x000C0937 File Offset: 0x000BEB37
	public static bool hasOppositeTraits<TTrait>(this TTrait pTraitToCheck) where TTrait : BaseTrait<TTrait>
	{
		return pTraitToCheck.opposite_traits != null && pTraitToCheck.opposite_traits.Count > 0;
	}

	// Token: 0x06000E23 RID: 3619 RVA: 0x000C095B File Offset: 0x000BEB5B
	public static bool hasOppositeTrait<TTrait>(this TTrait pTraitToCheck, HashSet<TTrait> pTraits) where TTrait : BaseTrait<TTrait>
	{
		return pTraitToCheck.hasOppositeTraits<TTrait>() && pTraits.Overlaps(pTraitToCheck.opposite_traits);
	}

	// Token: 0x06000E24 RID: 3620 RVA: 0x000C097D File Offset: 0x000BEB7D
	public static bool hasOppositeTrait(this ActorTrait pTraitToCheck, HashSet<ActorTrait> pTraits)
	{
		return pTraitToCheck.hasOppositeTraits<ActorTrait>() && pTraits.Overlaps(pTraitToCheck.opposite_traits);
	}

	// Token: 0x06000E25 RID: 3621 RVA: 0x000C099A File Offset: 0x000BEB9A
	public static bool hasOppositeTrait(string pTraitID, HashSet<ActorTrait> pTraits)
	{
		return AssetManager.traits.get(pTraitID).hasOppositeTrait(pTraits);
	}

	// Token: 0x06000E26 RID: 3622 RVA: 0x000C09B0 File Offset: 0x000BEBB0
	public static void loadTraits(Actor pActor, List<string> pListTraitIDs)
	{
		if (pListTraitIDs == null)
		{
			return;
		}
		if (pListTraitIDs.Count == 0)
		{
			return;
		}
		pActor.clearTraitCache();
		pActor.traits.Clear();
		foreach (string tID in pListTraitIDs)
		{
			ActorTrait tTrait = AssetManager.traits.get(tID);
			if (tTrait != null)
			{
				pActor.traits.Add(tTrait);
			}
		}
	}

	// Token: 0x06000E27 RID: 3623 RVA: 0x000C0A34 File Offset: 0x000BEC34
	public static ActorTrait getNewRandomTrait(string pGroup, HashSet<ActorTrait> pTraits)
	{
		ActorTrait result;
		using (ListPool<ActorTrait> tListTraits = new ListPool<ActorTrait>())
		{
			foreach (ActorTrait tTrait in AssetManager.traits.list)
			{
				int tRate = tTrait.getRate(pGroup);
				if (tRate != 0 && !(tTrait.group_id != pGroup) && !pTraits.Contains(tTrait) && !tTrait.hasOppositeTrait(pTraits))
				{
					for (int i = 0; i < tRate; i++)
					{
						tListTraits.Add(tTrait);
					}
				}
			}
			if (tListTraits.Count == 0)
			{
				Debug.LogError("No Trait Found? How possible? 2");
				result = null;
			}
			else
			{
				result = tListTraits.GetRandom<ActorTrait>();
			}
		}
		return result;
	}

	// Token: 0x06000E28 RID: 3624 RVA: 0x000C0B08 File Offset: 0x000BED08
	public static ActorTrait getMostUsedTraitFromPopulation(List<Actor> pUnits, HashSet<ActorTrait> pTraits, string pGroup)
	{
		Dictionary<ActorTrait, int> tDictCounter = new Dictionary<ActorTrait, int>();
		ActorTrait actorTrait;
		foreach (Actor tActor in pUnits)
		{
			if (tActor.isAlive())
			{
				foreach (ActorTrait tTrait in tActor.getTraits())
				{
					if (!(tTrait.group_id != pGroup) && !pTraits.Contains(tTrait) && !tTrait.hasOppositeTrait(pTraits))
					{
						if (tDictCounter.ContainsKey(tTrait))
						{
							Dictionary<ActorTrait, int> dictionary = tDictCounter;
							actorTrait = tTrait;
							int num = dictionary[actorTrait];
							dictionary[actorTrait] = num + 1;
						}
						else
						{
							tDictCounter[tTrait] = 1;
						}
					}
				}
			}
		}
		if (tDictCounter.Count == 0)
		{
			return null;
		}
		using (ListPool<ActorTrait> tTop3Traits = new ListPool<ActorTrait>(3))
		{
			foreach (KeyValuePair<ActorTrait, int> kv2 in from kv in tDictCounter
			orderby kv.Value descending
			select kv)
			{
				tTop3Traits.Add(kv2.Key);
				if (tTop3Traits.Count >= 3)
				{
					break;
				}
			}
			using (ListPool<ActorTrait> tRandomTraits = new ListPool<ActorTrait>())
			{
				ActorTrait tTrait2 = tTop3Traits[0];
				for (int i = 0; i < tTrait2.getRate(pGroup) * 2; i++)
				{
					tRandomTraits.Add(tTrait2);
				}
				if (tTop3Traits.Count > 1)
				{
					ActorTrait tTrait3 = tTop3Traits[1];
					for (int j = 0; j < tTrait3.getRate(pGroup); j++)
					{
						tRandomTraits.Add(tTrait3);
					}
				}
				if (tTop3Traits.Count > 2)
				{
					ActorTrait tTrait4 = tTop3Traits[2];
					for (int k = 0; k < tTrait4.getRate(pGroup); k++)
					{
						tRandomTraits.Add(tTrait4);
					}
				}
				if (tRandomTraits.Count == 0)
				{
					actorTrait = null;
				}
				else
				{
					actorTrait = tRandomTraits.GetRandom<ActorTrait>();
				}
			}
		}
		return actorTrait;
	}

	// Token: 0x06000E29 RID: 3625 RVA: 0x000C0D8C File Offset: 0x000BEF8C
	public static void recalculateTraitBonuses(BaseStats pBaseStats, HashSet<ActorTrait> pTraits, bool pClear = true)
	{
		if (pClear)
		{
			pBaseStats.clear();
		}
		foreach (ActorTrait tTrait in pTraits)
		{
			pBaseStats.mergeStats(tTrait.base_stats, 1f);
		}
	}
}
