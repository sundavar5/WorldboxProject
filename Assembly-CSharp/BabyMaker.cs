using System;
using UnityEngine;

// Token: 0x020003A8 RID: 936
public class BabyMaker
{
	// Token: 0x060021FC RID: 8700 RVA: 0x0011EBBC File Offset: 0x0011CDBC
	public static void startMiracleBirth(Actor pActor)
	{
		BabyHelper.babyMakingStart(pActor);
		if (pActor.hasSubspeciesTrait("reproduction_strategy_viviparity") && pActor.isSexFemale())
		{
			pActor.addStatusEffect("pregnant", pActor.getMaturationTimeSeconds(), true);
		}
		else
		{
			pActor.birthEvent("miracle_bearer", null);
			BabyMaker.makeBabyFromMiracle(pActor, ActorSex.Male, true);
			BabyMaker.makeBabyFromMiracle(pActor, ActorSex.Female, true);
			if (Randy.randomBool())
			{
				BabyMaker.makeBabyFromMiracle(pActor, ActorSex.None, true);
			}
		}
		pActor.subspecies.counterReproduction();
	}

	// Token: 0x060021FD RID: 8701 RVA: 0x0011EC30 File Offset: 0x0011CE30
	public static void startSoulborneBirth(Actor pActor)
	{
		BabyHelper.babyMakingStart(pActor);
		if (pActor.subspecies.hasTrait("reproduction_strategy_viviparity") && pActor.isSexFemale())
		{
			pActor.addStatusEffect("pregnant", pActor.getMaturationTimeSeconds(), true);
		}
		else
		{
			pActor.birthEvent(null, null);
			BabyMaker.makeBaby(pActor, null, ActorSex.None, false, 0, null, false, true);
		}
		pActor.subspecies.counterReproduction();
	}

	// Token: 0x060021FE RID: 8702 RVA: 0x0011EC94 File Offset: 0x0011CE94
	public static void spawnSporesFor(Actor pActor)
	{
		pActor.birthEvent(null, null);
		BabyHelper.babyMakingStart(pActor);
		int tSporesAmount = Randy.randomInt(3, 10);
		for (int i = 0; i < tSporesAmount; i++)
		{
			Spores tSpores = (Spores)EffectsLibrary.spawn("fx_spores", pActor.current_tile, null, null, 0f, -1f, -1f, null);
			if (tSpores == null)
			{
				return;
			}
			tSpores.prepare();
			tSpores.setActorParent(pActor);
		}
		pActor.subspecies.counterReproduction();
	}

	// Token: 0x060021FF RID: 8703 RVA: 0x0011ED10 File Offset: 0x0011CF10
	public static void spawnBabyFromSpore(Actor pActor, Vector3 pPosition)
	{
		WorldTile tTile = World.world.GetTile((int)pPosition.x, (int)pPosition.y);
		if (tTile == null)
		{
			return;
		}
		BabyMaker.makeBaby(pActor, null, ActorSex.None, false, 0, tTile, false, true);
	}

	// Token: 0x06002200 RID: 8704 RVA: 0x0011ED48 File Offset: 0x0011CF48
	public static void makeBabyFromMiracle(Actor pActor, ActorSex pSex = ActorSex.None, bool pAddToFamily = false)
	{
		BabyMaker.makeBaby(pActor, null, pSex, false, 0, null, pAddToFamily, false).addTrait("miracle_born", false);
	}

	// Token: 0x06002201 RID: 8705 RVA: 0x0011ED64 File Offset: 0x0011CF64
	public static Actor makeBabyViaFission(Actor pActor)
	{
		pActor.birthEvent(null, null);
		BabyHelper.babyMakingStart(pActor);
		Actor actor = BabyMaker.makeBaby(pActor, null, ActorSex.None, false, 0, null, false, true);
		int tParentHealth = pActor.getHealth() / 2;
		int tParentHappiness = pActor.getHappiness() / 2;
		int tParentNutrition = pActor.getNutrition() / 2;
		pActor.setHealth(tParentHealth, true);
		pActor.setStamina(0, true);
		pActor.setHappiness(tParentHappiness, true);
		pActor.setNutrition(tParentNutrition, true);
		actor.setHealth(tParentHealth, true);
		actor.setStamina(0, true);
		actor.setHappiness(tParentHappiness, true);
		actor.setNutrition(tParentNutrition, true);
		pActor.subspecies.counterReproduction();
		return actor;
	}

	// Token: 0x06002202 RID: 8706 RVA: 0x0011EDF2 File Offset: 0x0011CFF2
	public static Actor makeBabyViaBudding(Actor pActor)
	{
		pActor.birthEvent(null, null);
		BabyHelper.babyMakingStart(pActor);
		return BabyMaker.makeBaby(pActor, null, ActorSex.None, false, 0, null, false, true);
	}

	// Token: 0x06002203 RID: 8707 RVA: 0x0011EE10 File Offset: 0x0011D010
	public static Actor makeBabyViaVegetative(Actor pActor)
	{
		pActor.birthEvent(null, null);
		BabyHelper.babyMakingStart(pActor);
		Actor tBaby = BabyMaker.makeBaby(pActor, null, ActorSex.None, false, 0, null, false, true);
		tBaby.addStatusEffect("uprooting", tBaby.getMaturationTimeSeconds(), true);
		return tBaby;
	}

	// Token: 0x06002204 RID: 8708 RVA: 0x0011EE4D File Offset: 0x0011D04D
	public static void makeBabyViaParthenogenesis(Actor pActor)
	{
		pActor.birthEvent(null, null);
		BabyHelper.babyMakingStart(pActor);
		BabyMaker.makeBaby(pActor, null, ActorSex.None, false, 0, null, false, true);
		pActor.subspecies.counterReproduction();
	}

	// Token: 0x06002205 RID: 8709 RVA: 0x0011EE78 File Offset: 0x0011D078
	public static void makeBabiesViaSexual(Actor pMotherTarget, Actor pParentA, Actor pParentB)
	{
		pParentA.birthEvent(null, null);
		pParentB.birthEvent(null, null);
		BabyHelper.babyMakingStart(pParentA);
		BabyHelper.babyMakingStart(pParentB);
		BabyMaker.newImmediateBabySpawn(pParentA, pParentB);
		int tMaxBonusBabies = (int)pMotherTarget.stats["birth_rate"];
		float tChance = 0.5f;
		int i = 0;
		while (i < tMaxBonusBabies && Randy.randomChance(tChance))
		{
			BabyMaker.newImmediateBabySpawn(pParentA, pParentB);
			tChance *= 0.85f;
			i++;
		}
	}

	// Token: 0x06002206 RID: 8710 RVA: 0x0011EEE4 File Offset: 0x0011D0E4
	public static void makeBabyFromPregnancy(Actor pActor)
	{
		pActor.hasLover();
		Actor tLover = pActor.lover;
		pActor.birthEvent(null, null);
		BabyMaker.makeBaby(pActor, tLover, ActorSex.None, false, 0, null, true, false);
		float tChance = 0.5f;
		int tMaxBonusBabies = (int)pActor.stats["birth_rate"];
		int i = 0;
		while (i < tMaxBonusBabies && Randy.randomChance(tChance))
		{
			BabyMaker.makeBaby(pActor, tLover, ActorSex.None, false, 0, null, true, false);
			tChance *= 0.85f;
			i++;
		}
	}

	// Token: 0x06002207 RID: 8711 RVA: 0x0011EF57 File Offset: 0x0011D157
	private static void newImmediateBabySpawn(Actor pParent1, Actor pParent2)
	{
		BabyMaker.makeBaby(pParent1, pParent2, ActorSex.None, false, 0, null, true, false).justBorn();
	}

	// Token: 0x06002208 RID: 8712 RVA: 0x0011EF6C File Offset: 0x0011D16C
	public static Actor makeBaby(Actor pParent1, Actor pParent2, ActorSex pForcedSexType = ActorSex.None, bool pCloneTraits = false, int pMutationRate = 0, WorldTile pTile = null, bool pAddToFamily = false, bool pJoinFamily = false)
	{
		City tCity = pParent1.city ?? ((pParent2 != null) ? pParent2.city : null);
		if (tCity != null)
		{
			tCity.status.housing_free--;
		}
		ActorAsset tActorAsset = pParent1.asset;
		ActorData tNewBabyData = new ActorData();
		tNewBabyData.created_time = World.world.getCurWorldTime();
		tNewBabyData.id = World.world.map_stats.getNextId("unit");
		tNewBabyData.asset_id = tActorAsset.id;
		int tGeneration = pParent1.data.generation;
		if (pParent2 != null && pParent2.data.generation > tGeneration)
		{
			tGeneration = pParent2.data.generation;
		}
		tNewBabyData.generation = tGeneration + 1;
		Actor result;
		using (ListPool<WorldTile> tListPoolForBabySpawn = new ListPool<WorldTile>(4))
		{
			foreach (WorldTile tTile in pParent1.current_tile.neighboursAll)
			{
				if (tTile != pParent1.current_tile && (pParent2 == null || tTile != pParent2.current_tile) && tTile.Type.ground)
				{
					tListPoolForBabySpawn.Add(tTile);
				}
			}
			WorldTile tTargetTile;
			if (pTile != null)
			{
				tTargetTile = pTile;
			}
			else if (tListPoolForBabySpawn.Count == 0)
			{
				tTargetTile = pParent1.current_tile;
			}
			else
			{
				tTargetTile = tListPoolForBabySpawn.GetRandom<WorldTile>();
			}
			Actor tNewActorBaby = World.world.units.createBabyActorFromData(tNewBabyData, tTargetTile, tCity);
			tNewActorBaby.setParent1(pParent1, true);
			if (pParent2 != null)
			{
				tNewActorBaby.setParent2(pParent2, true);
			}
			if (pAddToFamily && !pParent1.hasFamily())
			{
				World.world.families.newFamily(pParent1, pParent1.current_tile, pParent2);
			}
			else if (pJoinFamily)
			{
				Family tFamily;
				if (!pParent1.hasFamily())
				{
					tFamily = World.world.families.newFamily(pParent1, pParent1.current_tile, pParent2);
				}
				else
				{
					tFamily = pParent1.family;
				}
				if (tFamily != null)
				{
					tNewActorBaby.setFamily(tFamily);
				}
			}
			BabyHelper.applyParentsMeta(pParent1, pParent2, tNewActorBaby);
			if (pCloneTraits || pParent1.hasSubspeciesTrait("genetic_mirror"))
			{
				BabyHelper.traitsClone(tNewActorBaby, pParent1);
			}
			else
			{
				foreach (ActorTrait tTrait in tNewActorBaby.subspecies.getActorBirthTraits().getTraits())
				{
					tNewActorBaby.addTrait(tTrait, false);
				}
				BabyHelper.traitsInherit(tNewActorBaby, pParent1, pParent2);
			}
			tNewActorBaby.checkTraitMutationOnBirth();
			tNewActorBaby.setNutrition(SimGlobals.m.nutrition_start_level_baby, true);
			if (pForcedSexType != ActorSex.None)
			{
				tNewActorBaby.data.sex = pForcedSexType;
			}
			else
			{
				ActorSex tForcedSexTypeByMeta = ActorSex.None;
				if (Randy.randomBool())
				{
					if (pParent1.hasCity())
					{
						if (pParent1.city.status.females > pParent1.city.status.males)
						{
							tForcedSexTypeByMeta = ActorSex.Male;
						}
						else
						{
							tForcedSexTypeByMeta = ActorSex.Female;
						}
					}
					else if (pParent1.subspecies.cached_females > pParent1.subspecies.cached_males)
					{
						tForcedSexTypeByMeta = ActorSex.Male;
					}
					else
					{
						tForcedSexTypeByMeta = ActorSex.Female;
					}
				}
				if (tForcedSexTypeByMeta != ActorSex.None)
				{
					tNewActorBaby.data.sex = tForcedSexTypeByMeta;
				}
				else
				{
					tNewActorBaby.generateSex();
				}
			}
			tNewActorBaby.checkShouldBeEgg();
			tNewActorBaby.makeStunned(10f);
			tNewActorBaby.applyRandomForce(1.5f, 2f);
			BabyHelper.countBirth(tNewActorBaby);
			BabyHelper.countMakeChild(pParent1, pParent2);
			tNewActorBaby.setStatsDirty();
			tNewActorBaby.event_full_stats = true;
			result = tNewActorBaby;
		}
		return result;
	}
}
