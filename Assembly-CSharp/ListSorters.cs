using System;

// Token: 0x0200046F RID: 1135
public class ListSorters
{
	// Token: 0x060026C2 RID: 9922 RVA: 0x0013AD88 File Offset: 0x00138F88
	public static int sortUnitByAgeOldFirst(Actor pActor1, Actor pActor2)
	{
		return -pActor2.data.created_time.CompareTo(pActor1.data.created_time);
	}

	// Token: 0x060026C3 RID: 9923 RVA: 0x0013ADB4 File Offset: 0x00138FB4
	public static int sortUnitByAgeYoungFirst(Actor pActor1, Actor pActor2)
	{
		return pActor2.data.created_time.CompareTo(pActor1.data.created_time);
	}

	// Token: 0x060026C4 RID: 9924 RVA: 0x0013ADE0 File Offset: 0x00138FE0
	public static int sortUnitByKills(Actor pActor1, Actor pActor2)
	{
		return -pActor1.data.kills.CompareTo(pActor2.data.kills);
	}

	// Token: 0x060026C5 RID: 9925 RVA: 0x0013AE0C File Offset: 0x0013900C
	public static int sortUnitByRenown(Actor pActor1, Actor pActor2)
	{
		return -pActor1.data.renown.CompareTo(pActor2.data.renown);
	}

	// Token: 0x060026C6 RID: 9926 RVA: 0x0013AE38 File Offset: 0x00139038
	public static int sortUnitByGoldCoins(Actor pActor1, Actor pActor2)
	{
		return -pActor1.data.money.CompareTo(pActor2.data.money);
	}

	// Token: 0x060026C7 RID: 9927 RVA: 0x0013AE64 File Offset: 0x00139064
	public static int sortUnitByGender(Actor pActor1, Actor pActor2, ActorSex pTopGender)
	{
		if (pActor1.data.sex == pActor2.data.sex)
		{
			return 0;
		}
		if (pActor1.data.sex == pTopGender)
		{
			return -1;
		}
		return 1;
	}

	// Token: 0x060026C8 RID: 9928 RVA: 0x0013AE94 File Offset: 0x00139094
	public static int sortUnitByStats(Actor pActor1, Actor pActor2, string pStatId)
	{
		float tValue = pActor1.stats.get(pStatId);
		float tValue2 = pActor2.stats.get(pStatId);
		return -tValue.CompareTo(tValue2);
	}

	// Token: 0x060026C9 RID: 9929 RVA: 0x0013AEC4 File Offset: 0x001390C4
	public static Actor getUnitSortedByAgeAndTraits(ListPool<Actor> pUnits, Culture pCulture)
	{
		ListSorters.sortUnitsSortedByAgeAndTraits(pUnits, pCulture);
		return pUnits[0];
	}

	// Token: 0x060026CA RID: 9930 RVA: 0x0013AED4 File Offset: 0x001390D4
	public static void sortUnitsSortedByAgeAndTraits(ListPool<Actor> pUnits, Culture pCulture)
	{
		if (pCulture == null)
		{
			pUnits.Sort(new Comparison<Actor>(ListSorters.sortUnitByAgeOldFirst));
			return;
		}
		if (pCulture.hasTrait("ultimogeniture"))
		{
			pUnits.Sort(new Comparison<Actor>(ListSorters.sortUnitByAgeYoungFirst));
		}
		else
		{
			pUnits.Sort(new Comparison<Actor>(ListSorters.sortUnitByAgeOldFirst));
		}
		bool flag = pCulture.hasTrait("diplomatic_ascension");
		bool tWarriorAscension = pCulture.hasTrait("warriors_ascension");
		bool tGoldenRule = pCulture.hasTrait("golden_rule");
		bool tFamesCrown = pCulture.hasTrait("fames_crown");
		if (flag)
		{
			pUnits.Sort((Actor a1, Actor a2) => ListSorters.sortUnitByStats(a1, a2, "diplomacy"));
		}
		else if (tWarriorAscension)
		{
			pUnits.Sort((Actor a1, Actor a2) => ListSorters.sortUnitByStats(a1, a2, "warfare"));
		}
		else if (tFamesCrown)
		{
			pUnits.Sort((Actor a1, Actor a2) => ListSorters.sortUnitByRenown(a1, a2));
		}
		else if (tGoldenRule)
		{
			pUnits.Sort((Actor a1, Actor a2) => ListSorters.sortUnitByGoldCoins(a1, a2));
		}
		bool tPatriarchy = pCulture.hasTrait("patriarchy");
		bool tMatriarchy = pCulture.hasTrait("matriarchy");
		if (tPatriarchy || tMatriarchy)
		{
			ActorSex tSex = tPatriarchy ? ActorSex.Male : ActorSex.Female;
			pUnits.Sort((Actor a1, Actor a2) => ListSorters.sortUnitByGender(a1, a2, tSex));
		}
	}
}
