using System;
using UnityEngine;

// Token: 0x020003A7 RID: 935
public static class BabyHelper
{
	// Token: 0x060021EF RID: 8687 RVA: 0x0011E354 File Offset: 0x0011C554
	public static Actor debugTryToMakeUnit(Actor pActor)
	{
		WorldTile current_tile = pActor.current_tile;
		Actor tTarget = null;
		foreach (Actor tActor in Finder.getUnitsFromChunk(current_tile, 1, 10f, false))
		{
			if (tActor != pActor && tActor.subspecies == pActor.subspecies)
			{
				tTarget = tActor;
				break;
			}
		}
		if (tTarget == null)
		{
			return null;
		}
		return BabyMaker.makeBaby(pActor, tTarget, ActorSex.None, false, 0, null, false, false);
	}

	// Token: 0x060021F0 RID: 8688 RVA: 0x0011E3D0 File Offset: 0x0011C5D0
	public static void countBirth(Actor pBaby)
	{
		World.world.game_stats.data.creaturesBorn += 1L;
		World.world.map_stats.creaturesBorn += 1L;
		if (pBaby.hasCity())
		{
			pBaby.city.increaseBirths();
		}
		if (pBaby.hasClan())
		{
			pBaby.clan.increaseBirths();
		}
		if (pBaby.hasFamily())
		{
			pBaby.family.increaseBirths();
		}
		if (pBaby.hasSubspecies())
		{
			pBaby.subspecies.increaseBirths();
		}
		if (pBaby.isKingdomCiv())
		{
			pBaby.kingdom.increaseBirths();
		}
	}

	// Token: 0x060021F1 RID: 8689 RVA: 0x0011E474 File Offset: 0x0011C674
	public static void applyParentsMeta(Actor pParent1, Actor pParent2, Actor pBaby)
	{
		Subspecies tBabySubspecies = BabyHelper.getBabySubspecies(pParent1, pParent2);
		pBaby.setSubspecies(tBabySubspecies);
		Family tFamily = pParent1.family;
		Clan tClan = BabyHelper.checkGreatClan(pParent1, pParent2);
		if (tClan != null && !tClan.isFull())
		{
			pBaby.setClan(tClan);
		}
		if (tBabySubspecies.isSapient())
		{
			if (pParent1.hasCity())
			{
				pBaby.setCity(pParent1.city);
			}
			else if (pParent2 != null && pParent2.hasCity())
			{
				pBaby.setCity(pParent2.city);
			}
		}
		if (tFamily != null)
		{
			pBaby.setFamily(tFamily);
			pBaby.saveOriginFamily(tFamily.data.id);
		}
		using (ListPool<Culture> tPotCultures = new ListPool<Culture>(2))
		{
			using (ListPool<Religion> tPotReligions = new ListPool<Religion>(2))
			{
				using (ListPool<Language> tPotLanguages = new ListPool<Language>(2))
				{
					using (ListPool<int> tPotPhenotypes = new ListPool<int>(2))
					{
						tPotPhenotypes.Add(pParent1.data.phenotype_index);
						if (pParent1.hasCulture())
						{
							tPotCultures.Add(pParent1.culture);
						}
						if (pParent1.hasReligion())
						{
							tPotReligions.Add(pParent1.religion);
						}
						if (pParent1.hasLanguage())
						{
							tPotLanguages.Add(pParent1.language);
						}
						if (pParent2 != null)
						{
							if (pParent2.hasCulture())
							{
								tPotCultures.Add(pParent2.culture);
							}
							if (pParent2.hasReligion())
							{
								tPotReligions.Add(pParent2.religion);
							}
							if (pParent2.hasLanguage())
							{
								tPotLanguages.Add(pParent2.language);
							}
							if (pParent2.subspecies == pBaby.subspecies)
							{
								tPotPhenotypes.Add(pParent2.data.phenotype_index);
							}
						}
						if (tPotCultures.Count > 0 && tBabySubspecies.has_advanced_memory)
						{
							pBaby.setCulture(tPotCultures.GetRandom<Culture>());
						}
						if (tPotReligions.Count > 0 && tBabySubspecies.has_advanced_memory)
						{
							pBaby.setReligion(tPotReligions.GetRandom<Religion>());
						}
						if (tPotLanguages.Count > 0 && tBabySubspecies.has_advanced_communication)
						{
							pBaby.joinLanguage(tPotLanguages.GetRandom<Language>());
						}
						if (pParent1 != null && pParent1.hasCultureTrait("ancestors_knowledge"))
						{
							string tBestAttribute = BabyHelper.getBestAtribute(pParent1);
							if (tBestAttribute != null)
							{
								pBaby.data[tBestAttribute] = (float)((int)pParent1.data[tBestAttribute]) * 0.5f + 1f;
							}
						}
						if (pParent2 != null && pParent2.hasCultureTrait("ancestors_knowledge"))
						{
							string tBestAttribute2 = BabyHelper.getBestAtribute(pParent2);
							if (tBestAttribute2 != null)
							{
								pBaby.data[tBestAttribute2] = (float)((int)pParent2.data[tBestAttribute2]) * 0.5f + 1f;
							}
						}
						pBaby.data.phenotype_index = tPotPhenotypes.GetRandom<int>();
						pBaby.data.phenotype_shade = Actor.getRandomPhenotypeShade();
						if (tBabySubspecies.hasTrait("parental_care"))
						{
							pBaby.addStatusEffect("invincible", 90f, true);
						}
					}
				}
			}
		}
	}

	// Token: 0x060021F2 RID: 8690 RVA: 0x0011E78C File Offset: 0x0011C98C
	private static string getBestAtribute(Actor pParent1)
	{
		string tBestAttribute = null;
		int tBestValue = 0;
		if (pParent1.data["intelligence"] > (float)tBestValue)
		{
			tBestValue = (int)pParent1.data["intelligence"];
			tBestAttribute = "intelligence";
		}
		if (pParent1.data["warfare"] > (float)tBestValue)
		{
			tBestValue = (int)pParent1.data["warfare"];
			tBestAttribute = "warfare";
		}
		if (pParent1.data["diplomacy"] > (float)tBestValue)
		{
			tBestValue = (int)pParent1.data["diplomacy"];
			tBestAttribute = "diplomacy";
		}
		if (pParent1.data["stewardship"] > (float)tBestValue)
		{
			tBestValue = (int)pParent1.data["stewardship"];
			tBestAttribute = "stewardship";
		}
		return tBestAttribute;
	}

	// Token: 0x060021F3 RID: 8691 RVA: 0x0011E850 File Offset: 0x0011CA50
	private static Clan checkGreatClan(Actor pParent1, Actor pParent2)
	{
		Clan tClan = null;
		if (pParent1.isKing())
		{
			tClan = pParent1.clan;
		}
		else if (pParent2 != null && pParent2.isKing())
		{
			tClan = pParent2.clan;
		}
		if (tClan == null)
		{
			if (pParent1.isCityLeader() && pParent2 != null && pParent2.isCityLeader())
			{
				if (Randy.randomBool())
				{
					tClan = pParent1.clan;
				}
				else
				{
					tClan = pParent2.clan;
				}
			}
			else if (pParent1 != null && pParent1.isCityLeader())
			{
				tClan = pParent1.clan;
			}
			else if (pParent2 != null && pParent2.isCityLeader())
			{
				tClan = pParent2.clan;
			}
		}
		return tClan;
	}

	// Token: 0x060021F4 RID: 8692 RVA: 0x0011E8D8 File Offset: 0x0011CAD8
	private static Subspecies getBabySubspecies(Actor pParent1, Actor pParent2)
	{
		Subspecies tSubspecies = pParent1.subspecies;
		Subspecies tSubspecies2 = ((pParent2 != null) ? pParent2.subspecies : null) ?? tSubspecies;
		if (tSubspecies.isSapient() && tSubspecies.isSapient() != tSubspecies2.isSapient())
		{
			if (tSubspecies.isSapient())
			{
				return tSubspecies;
			}
			return tSubspecies2;
		}
		else if (tSubspecies != tSubspecies2 && tSubspecies.getGeneration() != tSubspecies2.getGeneration())
		{
			if (tSubspecies.getGeneration() > tSubspecies2.getGeneration())
			{
				return tSubspecies;
			}
			return tSubspecies2;
		}
		else
		{
			if (Randy.randomBool())
			{
				return tSubspecies;
			}
			return tSubspecies2;
		}
	}

	// Token: 0x060021F5 RID: 8693 RVA: 0x0011E94E File Offset: 0x0011CB4E
	public static bool canMakeBabies(Actor pActor)
	{
		return pActor.isAdult() && pActor.canProduceBabies() && !pActor.hasReachedOffspringLimit() && pActor.haveNutritionForNewBaby();
	}

	// Token: 0x060021F6 RID: 8694 RVA: 0x0011E97C File Offset: 0x0011CB7C
	public static bool isMetaLimitsReached(Actor pActor)
	{
		if (pActor.subspecies.hasReachedPopulationLimit())
		{
			return true;
		}
		if (pActor.hasCity())
		{
			if (pActor.city.hasReachedWorldLawLimit())
			{
				return true;
			}
			Actor tLover = pActor.lover;
			bool flag = pActor.isImportantPerson() && !pActor.hasReachedOffspringLimit();
			bool tLoverImportant = tLover != null && tLover.isImportantPerson() && !tLover.hasReachedOffspringLimit();
			if (flag || tLoverImportant)
			{
				return false;
			}
			if (pActor.subspecies.isReproductionSexual() && pActor.current_children_count == 0)
			{
				return false;
			}
			if (!pActor.city.hasFreeHouseSlots())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060021F7 RID: 8695 RVA: 0x0011EA0F File Offset: 0x0011CC0F
	public static void countMakeChild(Actor pParent1, Actor pParent2)
	{
		if (!pParent1.isRekt())
		{
			pParent1.increaseBirths();
		}
		if (!pParent2.isRekt())
		{
			pParent2.increaseBirths();
		}
	}

	// Token: 0x060021F8 RID: 8696 RVA: 0x0011EA2D File Offset: 0x0011CC2D
	public static void babyMakingStart(Actor pActor)
	{
		WorldAction all_actions_actor_birth = pActor.subspecies.all_actions_actor_birth;
		if (all_actions_actor_birth == null)
		{
			return;
		}
		all_actions_actor_birth(pActor, pActor.current_tile);
	}

	// Token: 0x060021F9 RID: 8697 RVA: 0x0011EA4C File Offset: 0x0011CC4C
	public static void traitsClone(Actor pActorTarget, Actor pParent1)
	{
		foreach (ActorTrait tParentTrait in pParent1.getTraits())
		{
			if (tParentTrait.rate_birth != 0 || tParentTrait.rate_inherit != 0)
			{
				pActorTarget.addTrait(tParentTrait, false);
			}
		}
	}

	// Token: 0x060021FA RID: 8698 RVA: 0x0011EAAC File Offset: 0x0011CCAC
	public static void traitsInherit(Actor pActorTarget, Actor pParent1, Actor pParent2)
	{
		using (ListPool<ActorTrait> tPossibleTraits = new ListPool<ActorTrait>(128))
		{
			int tTotalParentTraits = 0;
			int tTotalParentTraits2 = 0;
			BabyHelper.addTraitsFromParentToList(pParent1, tPossibleTraits, out tTotalParentTraits);
			if (pParent2 != null)
			{
				BabyHelper.addTraitsFromParentToList(pParent2, tPossibleTraits, out tTotalParentTraits2);
			}
			if (tPossibleTraits.Count != 0)
			{
				int tTotalParentTraits3 = (int)((float)(tTotalParentTraits + tTotalParentTraits2) * 0.25f);
				tTotalParentTraits3 = Mathf.Max(1, tTotalParentTraits3);
				for (int i = 0; i < tTotalParentTraits3; i++)
				{
					ActorTrait tTrait = tPossibleTraits.GetRandom<ActorTrait>();
					pActorTarget.addTrait(tTrait.id, false);
				}
			}
		}
	}

	// Token: 0x060021FB RID: 8699 RVA: 0x0011EB40 File Offset: 0x0011CD40
	private static void addTraitsFromParentToList(Actor pActor, ListPool<ActorTrait> pList, out int pCounter)
	{
		int tResultCounter = 0;
		foreach (ActorTrait tTrait in pActor.getTraits())
		{
			if (tTrait.rate_inherit != 0 || tTrait.rate_birth != 0)
			{
				tResultCounter++;
				pList.AddTimes(tTrait.rate_birth, tTrait);
				pList.AddTimes(tTrait.rate_inherit, tTrait);
			}
		}
		pCounter = tResultCounter;
	}
}
