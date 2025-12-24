using System;

// Token: 0x02000137 RID: 311
public static class NameGeneratorChecks
{
	// Token: 0x06000947 RID: 2375 RVA: 0x000858C5 File Offset: 0x00083AC5
	public static bool hasLatinKing(Actor pActor)
	{
		return NameGeneratorChecks.hasCivKingdom(pActor) && pActor.kingdom.hasKing() && Toolbox.isFirstLatin(pActor.kingdom.king.getName());
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x000858FC File Offset: 0x00083AFC
	public static bool hasEnemyLatinKing(Actor pActor)
	{
		if (!NameGeneratorChecks.hasCivKingdom(pActor))
		{
			return false;
		}
		if (!pActor.kingdom.hasEnemies())
		{
			return false;
		}
		bool result;
		using (ListPool<Kingdom> tEnemyKingdoms = pActor.kingdom.getEnemiesKingdoms())
		{
			foreach (Kingdom ptr in tEnemyKingdoms)
			{
				Kingdom tKingdom = ptr;
				if (tKingdom.hasKing() && Toolbox.isFirstLatin(tKingdom.king.getName()))
				{
					return true;
				}
			}
			result = false;
		}
		return result;
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x000859A4 File Offset: 0x00083BA4
	public static bool hasCivKingdom(Actor pActor)
	{
		return pActor != null && pActor.kingdom != null && pActor.isKingdomCiv();
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x000859C0 File Offset: 0x00083BC0
	public static bool hasLatinKingdom(Actor pActor)
	{
		return NameGeneratorChecks.hasCivKingdom(pActor) && Toolbox.isFirstLatin(pActor.kingdom.name);
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x000859E4 File Offset: 0x00083BE4
	public unsafe static bool hasEnemyLatinKingdom(Actor pActor)
	{
		if (!NameGeneratorChecks.hasCivKingdom(pActor))
		{
			return false;
		}
		if (!pActor.kingdom.hasEnemies())
		{
			return false;
		}
		bool result;
		using (ListPool<Kingdom> tEnemyKingdoms = pActor.kingdom.getEnemiesKingdoms())
		{
			using (ListPool<Kingdom>.Enumerator enumerator = tEnemyKingdoms.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (Toolbox.isFirstLatin(enumerator.Current->name))
					{
						return true;
					}
				}
			}
			result = false;
		}
		return result;
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x00085A7C File Offset: 0x00083C7C
	public static bool hasLatinCity(Actor pActor)
	{
		return pActor != null && pActor.hasCity() && Toolbox.isFirstLatin(pActor.city.name);
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x00085AA2 File Offset: 0x00083CA2
	public static bool hasLatinCulture(Actor pActor)
	{
		return pActor != null && pActor.hasCulture() && Toolbox.isFirstLatin(pActor.culture.name);
	}
}
