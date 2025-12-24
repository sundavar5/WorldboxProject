using System;
using UnityEngine;

// Token: 0x0200013C RID: 316
public static class NameGeneratorReplacers
{
	// Token: 0x06000979 RID: 2425 RVA: 0x0008AC74 File Offset: 0x00088E74
	public static void replaceKingdom(ref string pName, Kingdom pKingdom)
	{
		if (!pName.Contains("$kingdom$"))
		{
			return;
		}
		if (pKingdom == null)
		{
			pName = "";
			return;
		}
		pName = pName.Replace("$kingdom$", pKingdom.name);
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x0008ACA4 File Offset: 0x00088EA4
	public static void replaceEnemyKing(ref string pName, Actor pActor)
	{
		using (ListPool<Kingdom> tEnemyKingdoms = pActor.kingdom.getEnemiesKingdoms())
		{
			foreach (Kingdom tKingdom in tEnemyKingdoms.LoopRandom<Kingdom>())
			{
				if (tKingdom.hasKing() && Toolbox.isFirstLatin(tKingdom.king.getName()))
				{
					pName = pName.Replace("$king$", "King " + tKingdom.king.getName());
					return;
				}
			}
			pName = "";
		}
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x0008AD54 File Offset: 0x00088F54
	public static void replaceOwnKingdom(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$kingdom$"))
		{
			return;
		}
		if (!pActor.hasKingdom())
		{
			pName = "";
			return;
		}
		Kingdom tKingdom = pActor.kingdom;
		pName = pName.Replace("$kingdom$", tKingdom.name);
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x0008AD9C File Offset: 0x00088F9C
	public static void replaceEnemyKingdom(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$kingdom$"))
		{
			return;
		}
		using (ListPool<Kingdom> tEnemyKingdoms = pActor.kingdom.getEnemiesKingdoms())
		{
			foreach (Kingdom tKingdom in tEnemyKingdoms.LoopRandom<Kingdom>())
			{
				if (Toolbox.isFirstLatin(tKingdom.name))
				{
					pName = pName.Replace("$kingdom$", tKingdom.name);
					return;
				}
			}
			pName = "";
		}
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x0008AE40 File Offset: 0x00089040
	public static void replaceFavoriteFood(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$food$"))
		{
			return;
		}
		Kingdom kingdom = pActor.kingdom;
		bool flag;
		if (kingdom == null)
		{
			flag = false;
		}
		else
		{
			Actor king = kingdom.king;
			bool? flag2 = (king != null) ? new bool?(king.hasFavoriteFood()) : null;
			bool flag3 = true;
			flag = (flag2.GetValueOrDefault() == flag3 & flag2 != null);
		}
		string tFood;
		if (flag)
		{
			tFood = pActor.kingdom.king.favorite_food_asset.getTranslatedName();
		}
		else
		{
			City city = pActor.city;
			bool flag4;
			if (city == null)
			{
				flag4 = false;
			}
			else
			{
				Actor leader = city.leader;
				bool? flag2 = (leader != null) ? new bool?(leader.hasFavoriteFood()) : null;
				bool flag3 = true;
				flag4 = (flag2.GetValueOrDefault() == flag3 & flag2 != null);
			}
			if (flag4)
			{
				tFood = pActor.city.leader.favorite_food_asset.getTranslatedName();
			}
			else if (pActor.hasFavoriteFood())
			{
				tFood = pActor.favorite_food_asset.getTranslatedName();
			}
			else
			{
				tFood = AssetManager.resources.list.GetRandom<ResourceAsset>().getTranslatedName();
			}
		}
		pName = pName.Replace("$food$", tFood);
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x0008AF4D File Offset: 0x0008914D
	public static void replaceOwnName(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$unit$"))
		{
			return;
		}
		pName = pName.Replace("$unit$", pActor.getName());
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x0008AF74 File Offset: 0x00089174
	public static void replaceOwnCity(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$city$"))
		{
			return;
		}
		if (!pActor.hasCity())
		{
			pName = "";
			return;
		}
		City tCity = pActor.city;
		pName = pName.Replace("$city$", tCity.name);
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x0008AFBC File Offset: 0x000891BC
	public static void replaceOwnSubspecies(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$subspecies$"))
		{
			return;
		}
		if (!pActor.hasSubspecies())
		{
			pName = "";
			return;
		}
		Subspecies tSubspecies = pActor.subspecies;
		pName = pName.Replace("$subspecies$", tSubspecies.name);
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x0008B004 File Offset: 0x00089204
	public static void replaceOwnAlliance(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$alliance$"))
		{
			return;
		}
		if (!pActor.hasKingdom())
		{
			pName = "";
			return;
		}
		Kingdom tKingdom = pActor.kingdom;
		if (!tKingdom.hasAlliance())
		{
			pName = "";
			return;
		}
		Alliance tAlliance = tKingdom.getAlliance();
		pName = pName.Replace("$alliance$", tAlliance.name);
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x0008B064 File Offset: 0x00089264
	public static void replaceOwnKingClan(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$clan$"))
		{
			return;
		}
		Kingdom tKingdom = pActor.kingdom;
		if (!tKingdom.hasKing())
		{
			pName = "";
			return;
		}
		Actor tKing = tKingdom.king;
		if (!tKing.hasClan())
		{
			pName = "";
			return;
		}
		pName = pName.Replace("$clan$", tKing.clan.name);
	}

	// Token: 0x06000983 RID: 2435 RVA: 0x0008B0C8 File Offset: 0x000892C8
	public static void replaceOwnLeader(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$leader$"))
		{
			return;
		}
		if (!pActor.hasCity())
		{
			pName = "";
			return;
		}
		City tCity = pActor.city;
		if (!tCity.hasLeader())
		{
			pName = "";
			return;
		}
		Actor tLeader = tCity.leader;
		pName = pName.Replace("$leader$", tLeader.getName());
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x0008B126 File Offset: 0x00089326
	public static void replaceFigure(ref string pName, Actor pActor)
	{
		NameGeneratorReplacers.replaceOwnLeader(ref pName, pActor);
		NameGeneratorReplacers.replaceOwnKing(ref pName, pActor);
		NameGeneratorReplacers.replaceOwnKingClan(ref pName, pActor);
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x0008B140 File Offset: 0x00089340
	public static void replaceAnyCity(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$city_random$"))
		{
			return;
		}
		if (!World.world.cities.hasAny())
		{
			pName = "";
			return;
		}
		City tCity = World.world.cities.getRandom();
		pName = pName.Replace("$city_random$", tCity.name);
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x0008B19C File Offset: 0x0008939C
	public static void replaceAnyKingdom(ref string pName, Actor _)
	{
		if (!pName.Contains("$kingdom_random$"))
		{
			return;
		}
		if (!World.world.kingdoms.hasAny())
		{
			pName = "";
			return;
		}
		Kingdom tKingdom = World.world.kingdoms.getRandom();
		pName = pName.Replace("$kingdom_random$", tKingdom.name);
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x0008B1F8 File Offset: 0x000893F8
	public static void replaceAnyCulture(ref string pName, Actor _)
	{
		if (!pName.Contains("$culture_random$"))
		{
			return;
		}
		if (!World.world.cultures.hasAny())
		{
			pName = "";
			return;
		}
		Culture tCulture = World.world.cultures.getRandom();
		pName = pName.Replace("$culture_random$", tCulture.name);
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x0008B254 File Offset: 0x00089454
	public static void replaceAnyFamily(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$family_random$"))
		{
			return;
		}
		if (!World.world.families.hasAny())
		{
			pName = "";
			return;
		}
		int tTries = World.world.families.Count;
		Family tRandomFamily;
		for (;;)
		{
			tRandomFamily = World.world.families.getRandom();
			if (tRandomFamily.isSameSpecies(pActor.asset.id))
			{
				break;
			}
			if (tTries-- <= 0)
			{
				goto Block_4;
			}
		}
		Family tFamily = tRandomFamily;
		pName = pName.Replace("$family_random$", tFamily.name);
		return;
		Block_4:
		pName = "";
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x0008B2E4 File Offset: 0x000894E4
	public static void replaceAnySubspecies(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$random_subspecies$"))
		{
			return;
		}
		if (!World.world.subspecies.hasAny())
		{
			pName = "";
			return;
		}
		Subspecies tSubspecies = World.world.subspecies.getRandom();
		pName = pName.Replace("$random_subspecies$", tSubspecies.name);
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x0008B340 File Offset: 0x00089540
	public static void replaceAnyClan(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$clan_random$"))
		{
			return;
		}
		if (!World.world.clans.hasAny())
		{
			pName = "";
			return;
		}
		Clan tClan = World.world.clans.getRandom();
		pName = pName.Replace("$clan_random$", tClan.name);
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x0008B39C File Offset: 0x0008959C
	public static void replaceAnyKing(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$king_random$"))
		{
			return;
		}
		if (!World.world.kingdoms.hasAny())
		{
			pName = "";
			return;
		}
		int i = 0;
		Kingdom tKingdom = null;
		while (tKingdom == null || !tKingdom.hasKing())
		{
			if (i++ > 10)
			{
				pName = "";
				return;
			}
			tKingdom = World.world.kingdoms.getRandom();
		}
		Actor tKing = tKingdom.king;
		pName = pName.Replace("$king_random$", tKing.getName());
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x0008B420 File Offset: 0x00089620
	public static void replaceAnyLeader(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$leader_random$"))
		{
			return;
		}
		if (!World.world.cities.hasAny())
		{
			pName = "";
			return;
		}
		int i = 0;
		City tCity = null;
		while (tCity == null || !tCity.hasLeader())
		{
			if (i++ > 10)
			{
				pName = "";
				return;
			}
			tCity = World.world.cities.getRandom();
		}
		Actor tLeader = tCity.leader;
		pName = pName.Replace("$leader_random$", tLeader.getName());
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x0008B4A4 File Offset: 0x000896A4
	public static void replaceOwnKing(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$king$"))
		{
			return;
		}
		if (!pActor.hasKingdom())
		{
			pName = "";
			return;
		}
		Kingdom tKingdom = pActor.kingdom;
		if (!tKingdom.hasKing())
		{
			pName = "";
			return;
		}
		Actor tKing = tKingdom.king;
		pName = pName.Replace("$king$", tKing.getName());
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x0008B504 File Offset: 0x00089704
	public static void replaceOwnKingLover(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$king_lover$"))
		{
			return;
		}
		if (!pActor.hasKingdom())
		{
			pName = "";
			return;
		}
		Kingdom tKingdom = pActor.kingdom;
		if (!tKingdom.hasKing())
		{
			pName = "";
			return;
		}
		Actor tKing = tKingdom.king;
		if (!tKing.hasLover())
		{
			pName = "";
			return;
		}
		Actor tLover = tKing.lover;
		pName = pName.Replace("$king$", tKing.getName());
		pName = pName.Replace("$king_lover$", tLover.getName());
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x0008B590 File Offset: 0x00089790
	public static void replaceOwnCulture(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$culture$"))
		{
			return;
		}
		if (!pActor.hasCulture())
		{
			pName = "";
			return;
		}
		Culture tCulture = pActor.culture;
		pName = pName.Replace("$culture$", tCulture.name);
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x0008B5D8 File Offset: 0x000897D8
	public static void replaceOwnLanguage(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$language$"))
		{
			return;
		}
		if (!pActor.hasLanguage())
		{
			pName = "";
			return;
		}
		Language tLanguage = pActor.language;
		pName = pName.Replace("$language$", tLanguage.name);
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x0008B620 File Offset: 0x00089820
	public static void replaceOwnReligion(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$religion$"))
		{
			return;
		}
		if (!pActor.hasReligion())
		{
			pName = "";
			return;
		}
		Religion tReligion = pActor.religion;
		pName = pName.Replace("$religion$", tReligion.name);
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x0008B668 File Offset: 0x00089868
	public static void replaceOwnFamily(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$family$"))
		{
			return;
		}
		if (!pActor.hasFamily())
		{
			pName = "";
			return;
		}
		Family tFamily = pActor.family;
		pName = pName.Replace("$family$", tFamily.name);
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x0008B6B0 File Offset: 0x000898B0
	public static void replaceAnyFamilyFounders(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$family_founder_1$") && !pName.Contains("$family_founder_2$"))
		{
			return;
		}
		if (!World.world.families.hasAny())
		{
			pName = "";
			return;
		}
		int tTries = World.world.families.list.Count;
		Family tRandomFamily;
		for (;;)
		{
			tRandomFamily = World.world.families.getRandom();
			if (tRandomFamily.isSameSpecies(pActor.asset.id) && tRandomFamily.hasFounders())
			{
				break;
			}
			if (tTries-- <= 0)
			{
				goto Block_6;
			}
		}
		Family tFamily = tRandomFamily;
		NameGeneratorReplacers.replaceFamilyFounder1(ref pName, tFamily.units[0]);
		NameGeneratorReplacers.replaceFamilyFounder2(ref pName, tFamily.units[0]);
		return;
		Block_6:
		pName = "";
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x0008B76B File Offset: 0x0008996B
	public static void replaceOwnFamilyFounders(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$family_founder_1$") && !pName.Contains("$family_founder_2$"))
		{
			return;
		}
		if (!pActor.hasFamily())
		{
			pName = "";
			return;
		}
		NameGeneratorReplacers.replaceFamilyFounder1(ref pName, pActor);
		NameGeneratorReplacers.replaceFamilyFounder2(ref pName, pActor);
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x0008B7A8 File Offset: 0x000899A8
	public static void replaceFamilyFounder1(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$family_founder_1$"))
		{
			return;
		}
		if (!pActor.hasFamily())
		{
			pName = "";
			return;
		}
		string tFamilyFounder = pActor.family.data.founder_actor_name_1;
		if (string.IsNullOrEmpty(tFamilyFounder))
		{
			pName = "";
			return;
		}
		pName = pName.Replace("$family_founder_1$", tFamilyFounder);
	}

	// Token: 0x06000996 RID: 2454 RVA: 0x0008B804 File Offset: 0x00089A04
	public static void replaceFamilyFounder2(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$family_founder_2$"))
		{
			return;
		}
		if (!pActor.hasFamily())
		{
			pName = "";
			return;
		}
		string tFamilyFounder2 = pActor.family.data.founder_actor_name_2;
		if (string.IsNullOrEmpty(tFamilyFounder2))
		{
			pName = "";
			return;
		}
		pName = pName.Replace("$family_founder_2$", tFamilyFounder2);
	}

	// Token: 0x06000997 RID: 2455 RVA: 0x0008B860 File Offset: 0x00089A60
	public static void replaceWorldName(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$world_name$"))
		{
			return;
		}
		pName = pName.Replace("$world_name$", World.world.map_stats.name);
	}

	// Token: 0x06000998 RID: 2456 RVA: 0x0008B88E File Offset: 0x00089A8E
	public static void replaceArchitectName(ref string pName, Actor pActor)
	{
		if (!pName.Contains("$architect_name$"))
		{
			return;
		}
		pName = pName.Replace("$architect_name$", World.world.map_stats.player_name);
	}

	// Token: 0x06000999 RID: 2457 RVA: 0x0008B8BC File Offset: 0x00089ABC
	public static void replacer_debug(ref string pName)
	{
		pName = pName.Replace("$alliance$", "Pact of Gregs");
		pName = pName.Replace("$food$", "Tea");
		pName = pName.Replace("$family$", "Gregovich");
		pName = pName.Replace("$family_random$", "Urg Zurg");
		pName = pName.Replace("$family_founder_1$", "Greg");
		pName = pName.Replace("$family_founder_2$", "Gregia");
		pName = pName.Replace("$king$", "Gregor");
		pName = pName.Replace("$king_lover$", "Gregoria");
		pName = pName.Replace("$king_random$", "Zurg Gurg");
		pName = pName.Replace("$kingdom$", "Kingdom of Greg");
		pName = pName.Replace("$kingdom_random$", "Brothers of Wargh");
		pName = pName.Replace("$clan$", "Greg Clan");
		pName = pName.Replace("$clan_random$", "Deze Zaz");
		pName = pName.Replace("$leader$", "Gregoryl");
		pName = pName.Replace("$leader_random$", "Orcaryl");
		pName = pName.Replace("$culture$", "Gragian Culture");
		pName = pName.Replace("$culture_random$", "Orkian Kult");
		pName = pName.Replace("$city$", "Gregopolis");
		pName = pName.Replace("$city_random$", "Orcville");
		pName = pName.Replace("$unit$", "Greg the Great");
		pName = pName.Replace("$warrior$", "Greg the Warrior");
		pName = pName.Replace("$language$", "Gregian Language");
		pName = pName.Replace("$religion$", "Gregianity");
		pName = pName.Replace("$subspecies$", "Gregian Sapient");
		pName = pName.Replace("$random_subspecies$", "Weird Dudes");
		pName = pName.Replace("$world_name$", "The Bad Place");
		pName = pName.Replace("$architect_name$", "Your Mom");
		pName = pName.Replace("$item$", "Legendary Greg Axe");
		if (pName.Contains('$'))
		{
			Debug.LogWarning("replacer_debug missing variable " + pName);
		}
	}
}
