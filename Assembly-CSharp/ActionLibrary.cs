using System;
using System.Collections.Generic;
using ai;
using UnityEngine;

// Token: 0x02000018 RID: 24
public static class ActionLibrary
{
	// Token: 0x06000109 RID: 265 RVA: 0x00009F94 File Offset: 0x00008194
	public static bool unluckyMeteorite(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (!WorldLawLibrary.world_law_disasters_nature.isEnabled())
		{
			return false;
		}
		if (World.world.cities.Count < 5)
		{
			return false;
		}
		if (pTarget.a.getAge() < 30)
		{
			return false;
		}
		if (!Randy.randomChance(5E-05f))
		{
			return false;
		}
		Meteorite.spawnMeteoriteDisaster(pTarget.current_tile, pTarget.a);
		return true;
	}

	// Token: 0x0600010A RID: 266 RVA: 0x00009FF4 File Offset: 0x000081F4
	public static bool unluckyFall(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (Randy.randomChance(0.8f))
		{
			return false;
		}
		pTarget.a.makeStunned(5f);
		return true;
	}

	// Token: 0x0600010B RID: 267 RVA: 0x0000A018 File Offset: 0x00008218
	public static bool flamingWeapon(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (!MapBox.isRenderGameplay())
		{
			return false;
		}
		if (pTarget.isBuilding())
		{
			return false;
		}
		Actor tActor = pTarget.a;
		if (!tActor.a.is_visible)
		{
			return false;
		}
		Sprite tItemSprite = tActor.getRenderedItemSprite();
		if (tItemSprite == null)
		{
			return false;
		}
		AnimationFrameData tFrameData = tActor.getAnimationFrameData();
		if (tFrameData == null)
		{
			return false;
		}
		Vector3 tVec = default(Vector3);
		tVec.x = tActor.cur_transform_position.x + tFrameData.pos_item.x * tActor.current_scale.x;
		tVec.y = tActor.cur_transform_position.y + tFrameData.pos_item.y * tActor.current_scale.y;
		tVec.z = -0.01f;
		float tWeaponHeight = tItemSprite.rect.height * tActor.current_scale.y;
		if (tActor.is_moving)
		{
			tVec.y += tWeaponHeight;
			tVec.x += Randy.randomFloat(-0.1f, 0.1f);
			tVec.y += Randy.randomFloat(-0.1f, 0.2f);
		}
		else
		{
			tVec.x += Randy.randomFloat(-0.05f, 0.05f);
			float yPlus = Randy.randomFloat(0f, tWeaponHeight * 1.5f);
			if ((double)yPlus < (double)tWeaponHeight * 0.5)
			{
				tVec.x += Randy.randomFloat(-0.15f, 0.15f);
			}
			tVec.y += yPlus;
		}
		if (tActor.current_rotation.y != 0f || tActor.current_rotation.z != 0f)
		{
			tVec = Toolbox.RotatePointAroundPivot(ref tVec, ref tActor.cur_transform_position, ref tActor.current_rotation);
		}
		BaseEffect tEffects = EffectsLibrary.spawn("fx_weapon_particle", null, null, null, 0f, -1f, -1f, null);
		if (tEffects != null)
		{
			((StatusParticle)tEffects).spawnParticle(tVec, Toolbox.colors_fire.GetRandom<Color>(), 0.25f);
			return true;
		}
		return false;
	}

	// Token: 0x0600010C RID: 268 RVA: 0x0000A224 File Offset: 0x00008424
	public static bool shiny(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (!pTile.has_tile_up)
		{
			return false;
		}
		if (!MapBox.isRenderGameplay())
		{
			return false;
		}
		Vector3 tVec = pTile.tile_up.posV3;
		tVec.x += Randy.randomFloat(-0.3f, 0.3f);
		tVec.y += Randy.randomFloat(-0.3f, 0.3f);
		EffectsLibrary.spawnAt("fx_building_sparkle", tVec, 0.1f);
		return true;
	}

	// Token: 0x0600010D RID: 269 RVA: 0x0000A298 File Offset: 0x00008498
	public static bool restoreHealthOnHit(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget == null)
		{
			return false;
		}
		if (!pTarget.isActor())
		{
			return false;
		}
		if (!pSelf.isActor())
		{
			return false;
		}
		if (!pSelf.isAlive())
		{
			return false;
		}
		int tHealthToRestore = pTarget.getMaxHealthPercent(0.05f);
		pSelf.a.restoreHealth(tHealthToRestore);
		return true;
	}

	// Token: 0x0600010E RID: 270 RVA: 0x0000A2E4 File Offset: 0x000084E4
	public static void throwTorchAtTile(BaseSimObject pSelf, WorldTile pTile)
	{
		Vector2Int tAttackPosition = pTile.pos;
		Vector3 tSelfPosition = pSelf.current_position;
		float tDist = Vector2.Distance(tSelfPosition, tAttackPosition);
		Vector3 tAttackVector = Toolbox.getNewPoint(tSelfPosition.x, tSelfPosition.y, (float)tAttackPosition.x, (float)tAttackPosition.y, tDist, true);
		Vector3 tStartProjectile = Toolbox.getNewPoint(tSelfPosition.x, tSelfPosition.y, (float)tAttackPosition.x, (float)tAttackPosition.y, pSelf.a.stats["size"], true);
		tStartProjectile.y += 0.5f;
		World.world.projectiles.spawn(pSelf, null, "torch", tStartProjectile, tAttackVector, 0f, 0.25f, null, null);
	}

	// Token: 0x0600010F RID: 271 RVA: 0x0000A3AC File Offset: 0x000085AC
	public static bool canThrowBomb(BaseSimObject pTarget, WorldTile pTile)
	{
		float tDist = Toolbox.Dist(pTarget.a.current_position.x, pTarget.a.current_position.y, (float)pTile.pos.x, (float)pTile.pos.y);
		return tDist > 3f && tDist < 26f;
	}

	// Token: 0x06000110 RID: 272 RVA: 0x0000A410 File Offset: 0x00008610
	public static void throwBombAtTile(BaseSimObject pSelf, WorldTile pTile)
	{
		Vector2Int tAttackPosition = pTile.pos;
		Vector3 tSelfPosition = pSelf.current_position;
		float tDist = Vector2.Distance(tSelfPosition, tAttackPosition);
		Vector3 tAttackVector = Toolbox.getNewPoint(tSelfPosition.x, tSelfPosition.y, (float)tAttackPosition.x, (float)tAttackPosition.y, tDist, true);
		Vector3 tStartProjectile = Toolbox.getNewPoint(tSelfPosition.x, tSelfPosition.y, (float)tAttackPosition.x, (float)tAttackPosition.y, pSelf.a.stats["size"], true);
		tStartProjectile.y += 0.5f;
		World.world.projectiles.spawn(pSelf, null, "firebomb", tStartProjectile, tAttackVector, 0f, 0.25f, null, null);
	}

	// Token: 0x06000111 RID: 273 RVA: 0x0000A4D8 File Offset: 0x000086D8
	public static bool zombieInfectAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (!pTarget.isAlive())
		{
			return false;
		}
		if (!pTarget.isActor())
		{
			return false;
		}
		if (Randy.randomChance(0.25f))
		{
			pTarget.a.startShake(0.2f, 0.05f, true, false);
		}
		pTarget.a.spawnParticle(Toolbox.color_infected);
		if (pTarget.a.asset.can_turn_into_zombie && Randy.randomChance(0.5f))
		{
			pTarget.a.addTrait("infected", false);
		}
		return true;
	}

	// Token: 0x06000112 RID: 274 RVA: 0x0000A55D File Offset: 0x0000875D
	public static bool zombieEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		pTarget.a.spawnParticle(Toolbox.color_infected);
		if (Randy.randomChance(0.25f))
		{
			pTarget.a.startShake(0.2f, 0.05f, true, false);
		}
		return true;
	}

	// Token: 0x06000113 RID: 275 RVA: 0x0000A594 File Offset: 0x00008794
	public static bool infectedEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		int tDamage = pTarget.getHealth() / 10;
		if (tDamage < 10)
		{
			tDamage = 10;
		}
		pTarget.a.getHit((float)tDamage, true, AttackType.Infection, null, false, false, true);
		pTarget.a.spawnParticle(Toolbox.color_infected);
		pTarget.a.startShake(0.4f, 0.2f, true, false);
		return true;
	}

	// Token: 0x06000114 RID: 276 RVA: 0x0000A5F0 File Offset: 0x000087F0
	public static bool mushSporesEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		int tMax = 3;
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 3f, true))
		{
			if (tActor != pTarget.a && !Randy.randomChance(0.7f) && tActor.addTrait("mush_spores", false))
			{
				tActor.spawnParticle(Toolbox.color_mushSpores);
				tMax--;
				if (tMax == 0)
				{
					break;
				}
			}
		}
		return true;
	}

	// Token: 0x06000115 RID: 277 RVA: 0x0000A678 File Offset: 0x00008878
	public static bool tumorEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		pTarget.a.startShake(0.4f, 0.2f, true, false);
		if (Randy.randomChance(0.1f))
		{
			pTarget.getHit((float)pTarget.getMaxHealthPercent(0.1f), false, AttackType.Tumor, null, false, false, true);
		}
		return true;
	}

	// Token: 0x06000116 RID: 278 RVA: 0x0000A6B8 File Offset: 0x000088B8
	public static bool healingAuraEffect(BaseSimObject pSelf, WorldTile pTile = null)
	{
		if (!Randy.randomChance(0.2f))
		{
			return false;
		}
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 4f, true))
		{
			if (tActor != pSelf.a && !tActor.hasMaxHealth() && !pSelf.areFoes(tActor))
			{
				tActor.restoreHealth(10);
				tActor.spawnParticle(Toolbox.color_heal);
				tActor.removeTrait("plague");
			}
		}
		return true;
	}

	// Token: 0x06000117 RID: 279 RVA: 0x0000A74C File Offset: 0x0000894C
	public static bool heliophobiaEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		Actor tActorTarget = pTarget.a;
		BiomeAsset tBiomeAsset = tActorTarget.current_tile.getBiome();
		if (tBiomeAsset != null)
		{
			if (tBiomeAsset.cold_biome)
			{
				return false;
			}
			if (tBiomeAsset.dark_biome)
			{
				return false;
			}
		}
		if (!World.world_era.flag_light_damage)
		{
			return false;
		}
		int tDamage = (int)((float)tActorTarget.getMaxHealth() * 0.1f) + 1;
		tActorTarget.getHit((float)tDamage, true, AttackType.Other, null, true, false, true);
		return true;
	}

	// Token: 0x06000118 RID: 280 RVA: 0x0000A7B4 File Offset: 0x000089B4
	public static bool regenerationEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		Actor tActorTarget = pTarget.a;
		if (tActorTarget.hasTrait("infected"))
		{
			return true;
		}
		if (!tActorTarget.hasMaxHealth() && !tActorTarget.isHungry() && Randy.randomChance(0.2f))
		{
			int tHealthToRegen = tActorTarget.getMaxHealthPercent(0.02f);
			tActorTarget.restoreHealth(tHealthToRegen);
			tActorTarget.spawnParticle(Toolbox.color_heal);
		}
		ActionLibrary.checkRegenerationTraits(tActorTarget);
		return true;
	}

	// Token: 0x06000119 RID: 281 RVA: 0x0000A818 File Offset: 0x00008A18
	private static void checkRegenerationTraits(Actor pActorTarget)
	{
		if (pActorTarget.hasTrait("crippled") && Randy.randomChance(0.05f))
		{
			pActorTarget.removeTrait("crippled");
		}
		if (pActorTarget.hasTrait("skin_burns") && Randy.randomChance(0.05f))
		{
			pActorTarget.removeTrait("skin_burns");
		}
		if (pActorTarget.hasTrait("eyepatch") && Randy.randomChance(0.05f))
		{
			pActorTarget.removeTrait("eyepatch");
		}
	}

	// Token: 0x0600011A RID: 282 RVA: 0x0000A894 File Offset: 0x00008A94
	public static bool regenerationEffectClan(BaseSimObject pTarget, WorldTile pTile = null)
	{
		Actor tActorTarget = pTarget.a;
		if (tActorTarget.hasTrait("infected"))
		{
			return true;
		}
		if (!tActorTarget.hasMaxHealth() && !tActorTarget.isHungry() && Randy.randomChance(0.2f))
		{
			int tHealthToRegen = tActorTarget.getMaxHealthPercent(0.01f);
			tActorTarget.restoreHealth(tHealthToRegen);
			tActorTarget.spawnParticle(Toolbox.color_heal);
		}
		ActionLibrary.checkRegenerationTraits(tActorTarget);
		return true;
	}

	// Token: 0x0600011B RID: 283 RVA: 0x0000A8F8 File Offset: 0x00008AF8
	public static bool suprisedByArchitector(BaseSimObject _, WorldTile pTile)
	{
		if (World.world.isPaused())
		{
			return false;
		}
		foreach (Actor actor in Finder.getUnitsFromChunk(pTile, 1, 8f, false))
		{
			actor.tryToGetSurprised(pTile, false);
		}
		return true;
	}

	// Token: 0x0600011C RID: 284 RVA: 0x0000A95C File Offset: 0x00008B5C
	public static bool coldAuraEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		World.world.loopWithBrush(pTarget.current_tile, Brush.get(4, "circ_"), new PowerActionWithID(PowerLibrary.drawTemperatureMinus), null);
		return true;
	}

	// Token: 0x0600011D RID: 285 RVA: 0x0000A988 File Offset: 0x00008B88
	public static bool megaHeartbeat(BaseSimObject pTarget, WorldTile pTile = null)
	{
		World.world.applyForceOnTile(pTile, 3, 0.3f, true, 0, null, pTarget, null, false);
		EffectsLibrary.spawnExplosionWave(pTile.posV3, 3f, 0.5f);
		return true;
	}

	// Token: 0x0600011E RID: 286 RVA: 0x0000A9C4 File Offset: 0x00008BC4
	public static bool thornsDefense(BaseSimObject pSelf, BaseSimObject pAttackedBy, WorldTile pTile = null)
	{
		if (pSelf.isAlive() && Randy.randomChance(0.5f))
		{
			if (pAttackedBy != null && pAttackedBy.isActor() && pAttackedBy.isAlive())
			{
				Actor tAttacker = pAttackedBy.a;
				if (Toolbox.DistTile(pSelf.a.current_tile, tAttacker.a.current_tile) < 2f)
				{
					float tDamage = tAttacker.stats["damage"] * 0.2f;
					tAttacker.getHit(tDamage, true, AttackType.Weapon, pSelf, true, false, true);
				}
			}
			return true;
		}
		return false;
	}

	// Token: 0x0600011F RID: 287 RVA: 0x0000AA49 File Offset: 0x00008C49
	public static bool bubbleDefense(BaseSimObject pSelf, BaseSimObject pAttackedBy, WorldTile pTile = null)
	{
		if (pSelf.hasHealth() && Randy.randomChance(0.1f))
		{
			pSelf.addStatusEffect("shield", 5f, true);
			return true;
		}
		return false;
	}

	// Token: 0x06000120 RID: 288 RVA: 0x0000AA74 File Offset: 0x00008C74
	public static bool plagueEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		ActionLibrary.tickPlagueInfection(pTarget.a);
		pTarget.a.startShake(0.4f, 0.2f, true, false);
		if (Randy.randomChance(0.1f))
		{
			int tDamage = pTarget.getMaxHealthPercent(0.15f) + 1;
			pTarget.a.getHit((float)tDamage, false, AttackType.Plague, null, false, false, true);
		}
		return true;
	}

	// Token: 0x06000121 RID: 289 RVA: 0x0000AAD1 File Offset: 0x00008CD1
	public static bool energizedLightning(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (!Toolbox.inMapBorder(ref pTarget.current_position))
		{
			EffectsLibrary.spawnAt("fx_lightning_small", pTarget.current_position, 0.25f);
			return true;
		}
		MapBox.spawnLightningSmall(pTarget.current_tile, 0.25f, pTarget.a);
		return true;
	}

	// Token: 0x06000122 RID: 290 RVA: 0x0000AB10 File Offset: 0x00008D10
	public static bool contagiousEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (!WorldLawLibrary.world_law_rat_plague.isEnabled())
		{
			return false;
		}
		if (Randy.randomChance(0.7f) && ActorTool.countContagiousNearby(pTarget.a) > 20 && Randy.randomChance(0.2f))
		{
			ActionLibrary.tickPlagueInfection(pTarget.a);
		}
		return true;
	}

	// Token: 0x06000123 RID: 291 RVA: 0x0000AB5E File Offset: 0x00008D5E
	public static bool deathMark(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (Randy.randomChance(0.2f))
		{
			pTarget.a.getHitFullHealth(AttackType.Divine);
		}
		return true;
	}

	// Token: 0x06000124 RID: 292 RVA: 0x0000AB7C File Offset: 0x00008D7C
	private static void tickPlagueInfection(Actor pActor)
	{
		pActor.spawnParticle(Toolbox.color_plague);
		if (!Randy.randomChance(0.05f))
		{
			return;
		}
		int tMax = 3;
		foreach (Actor tActor in Finder.getUnitsFromChunk(pActor.current_tile, 0, 6f, true))
		{
			if (tActor != pActor)
			{
				if (tActor.addTrait("plague", false))
				{
					break;
				}
				tMax--;
				if (tMax <= 0)
				{
					break;
				}
			}
		}
	}

	// Token: 0x06000125 RID: 293 RVA: 0x0000AC08 File Offset: 0x00008E08
	public static bool burningFeetEffectTileDraw(WorldTile pTile, string pPowerID)
	{
		if (pTile.isTemporaryFrozen() && Randy.randomBool())
		{
			pTile.unfreeze(1);
		}
		return true;
	}

	// Token: 0x06000126 RID: 294 RVA: 0x0000AC24 File Offset: 0x00008E24
	public static bool burningFeetEffect(BaseSimObject pSelf, WorldTile pTile = null)
	{
		WorldTile tCurTile = pSelf.current_tile;
		if (!tCurTile.Type.can_be_set_on_fire_by_burning_feet)
		{
			return false;
		}
		Actor tActor = pSelf.a;
		if (tActor.isInLiquid())
		{
			return false;
		}
		if (!tActor.has_attack_target && !tActor.hasTag("moody"))
		{
			return false;
		}
		World.world.loopWithBrush(tCurTile, Brush.get(4, "circ_"), new PowerActionWithID(ActionLibrary.burningFeetEffectTileDraw), null);
		tCurTile.startFire(true);
		for (int i = 0; i < tCurTile.neighbours.Length; i++)
		{
			WorldTile worldTile = tCurTile.neighbours[i];
			worldTile.startFire(false);
			worldTile.setBurned(-1);
		}
		return true;
	}

	// Token: 0x06000127 RID: 295 RVA: 0x0000ACC8 File Offset: 0x00008EC8
	public static bool flowerPrintsEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (!Randy.randomChance(0.3f))
		{
			return false;
		}
		WorldTile tTile = pTarget.a.current_tile;
		BiomeAsset tBiomeAsset = tTile.Type.biome_asset;
		if (tBiomeAsset == null)
		{
			return false;
		}
		if (!tBiomeAsset.grow_vegetation_auto)
		{
			return false;
		}
		if (tBiomeAsset.grow_type_selector_plants != null)
		{
			BuildingActions.tryGrowVegetationRandom(tTile, VegetationType.Plants, false, false, true);
		}
		return true;
	}

	// Token: 0x06000128 RID: 296 RVA: 0x0000AD20 File Offset: 0x00008F20
	public static bool acidBloodEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		for (int i = 0; i < 5; i++)
		{
			if (Randy.randomBool())
			{
				World.world.drop_manager.spawnParabolicDrop(pTarget.a.current_tile, "acid", 0f, 0.1f, 5f, 0.5f, 4f, 0.15f);
			}
		}
		if (!pTarget.isActor())
		{
			return true;
		}
		if (pTarget.a.asset.actor_size < ActorSize.S17_Dragon)
		{
			return true;
		}
		for (int j = 0; j < 25; j++)
		{
			if (Randy.randomBool())
			{
				World.world.drop_manager.spawnParabolicDrop(pTarget.a.current_tile, "acid", 0f, 0.1f, 10f, 0.5f, 10f, 0.15f);
			}
			for (int k = 0; k < pTarget.a.current_tile.neighboursAll.Length; k++)
			{
				WorldTile tTile = pTarget.a.current_tile.neighboursAll[k];
				if (Randy.randomBool())
				{
					World.world.drop_manager.spawnParabolicDrop(tTile, "acid", 0f, 0.1f, 10f, 0.5f, 7f, 0.15f);
				}
			}
		}
		return true;
	}

	// Token: 0x06000129 RID: 297 RVA: 0x0000AE5F File Offset: 0x0000905F
	public static bool acidTouchEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (!Randy.randomChance(0.3f))
		{
			return false;
		}
		MapAction.checkAcidTerraform(pTarget.a.current_tile);
		return true;
	}

	// Token: 0x0600012A RID: 298 RVA: 0x0000AE80 File Offset: 0x00009080
	public static bool sunblessedEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (!Randy.randomChance(0.5f))
		{
			return false;
		}
		if (World.world.era_manager.getCurrentAge().flag_night)
		{
			return false;
		}
		float tRandomHealth = Randy.randomFloat(0.05f, 0.1f);
		pTarget.a.restoreHealthPercent(tRandomHealth);
		return true;
	}

	// Token: 0x0600012B RID: 299 RVA: 0x0000AED0 File Offset: 0x000090D0
	public static bool castSpawnSkeleton(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget != null)
		{
			pTile = pTarget.current_tile;
		}
		int tCount = 0;
		foreach (Actor actor in Finder.findSpeciesAroundTileChunk(pTile, "skeleton"))
		{
			if (tCount++ > 6)
			{
				return false;
			}
		}
		WorldTile worldTile;
		if (pTile == null)
		{
			worldTile = null;
		}
		else
		{
			MapRegion region = pTile.region;
			worldTile = ((region != null) ? region.getRandomTile() : null);
		}
		WorldTile tTile = worldTile;
		if (tTile == null)
		{
			return false;
		}
		ActionLibrary.spawnSkeleton(pSelf, tTile);
		return true;
	}

	// Token: 0x0600012C RID: 300 RVA: 0x0000AF60 File Offset: 0x00009160
	public static bool spawnSkeleton(BaseSimObject pCaster, WorldTile pTile = null)
	{
		if (pTile == null)
		{
			return false;
		}
		BaseEffect baseEffect = EffectsLibrary.spawnAt("fx_create_skeleton", pTile.posV3, 0.1f);
		Actor tActorCaster = pCaster.a;
		Subspecies tTargetSubspecies = null;
		TileZone current_zone = tActorCaster.current_zone;
		bool tNeedNewSkeletonForm = false;
		Subspecies tSubspeciesTargetForNewSkeleton = null;
		City tCityForSubspecies = current_zone.city;
		if (tCityForSubspecies != null && !tCityForSubspecies.kingdom.isNeutral())
		{
			Subspecies tMainEnemyCitySubspecies = tCityForSubspecies.getMainSubspecies();
			tTargetSubspecies = ((tMainEnemyCitySubspecies != null) ? tMainEnemyCitySubspecies.getSkeletonForm() : null);
			if (tTargetSubspecies == null)
			{
				tNeedNewSkeletonForm = true;
				tSubspeciesTargetForNewSkeleton = tMainEnemyCitySubspecies;
			}
		}
		else if (tActorCaster.hasCity())
		{
			tCityForSubspecies = tActorCaster.city;
			tTargetSubspecies = tCityForSubspecies.getSubspecies("skeleton");
		}
		baseEffect.setCallback(19, delegate
		{
			Actor tSkeleton = World.world.units.createNewUnit("skeleton", pTile, false, 0f, tTargetSubspecies, null, true, true, false, false);
			tSkeleton.makeWait(1f);
			if (tActorCaster.isRekt())
			{
				return;
			}
			if (tSkeleton.subspecies.isJustCreated() && tActorCaster.isKingdomCiv())
			{
				tSkeleton.subspecies.addTrait("prefrontal_cortex", false);
			}
			if (tSkeleton.subspecies.isJustCreated() && tNeedNewSkeletonForm && !tSubspeciesTargetForNewSkeleton.isRekt())
			{
				tSubspeciesTargetForNewSkeleton.setSkeletonForm(tSkeleton.subspecies);
			}
			if (tActorCaster.isKingdomCiv() && tSkeleton.subspecies.hasTrait("prefrontal_cortex"))
			{
				City tCity = tActorCaster.city;
				Kingdom tCasterKingdom = tActorCaster.kingdom;
				if (!tCity.isRekt() && tCity.kingdom == tCasterKingdom)
				{
					tSkeleton.joinCity(tActorCaster.city);
					return;
				}
				tSkeleton.joinKingdom(tCasterKingdom);
			}
		});
		return true;
	}

	// Token: 0x0600012D RID: 301 RVA: 0x0000B050 File Offset: 0x00009250
	public static bool castFire(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget != null)
		{
			pTile = pTarget.current_tile;
		}
		if (pTile == null)
		{
			return false;
		}
		World.world.drop_manager.spawn(pTile, "fire", 15f, -1f, -1L);
		for (int i = 0; i < 3; i++)
		{
			World.world.drop_manager.spawn(pTile.neighboursAll.GetRandom<WorldTile>(), "fire", 15f, -1f, -1L);
		}
		return true;
	}

	// Token: 0x0600012E RID: 302 RVA: 0x0000B0C8 File Offset: 0x000092C8
	public static bool castSpellSilence(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget != null)
		{
			pTile = pTarget.current_tile;
		}
		if (pTile == null)
		{
			return false;
		}
		World.world.drop_manager.spawn(pTile, "spell_silence", 15f, -1f, -1L);
		return true;
	}

	// Token: 0x0600012F RID: 303 RVA: 0x0000B0FD File Offset: 0x000092FD
	public static bool castBloodRain(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget != null)
		{
			pTile = pTarget.current_tile;
		}
		if (pTile == null)
		{
			return false;
		}
		World.world.drop_manager.spawn(pTile, "blood_rain", 15f, -1f, pSelf.id);
		return true;
	}

	// Token: 0x06000130 RID: 304 RVA: 0x0000B138 File Offset: 0x00009338
	public static bool castSpawnGrassSeeds(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTile == null)
		{
			pTile = pTarget.current_tile;
		}
		if (pTile == null)
		{
			return false;
		}
		if (WorldLawLibrary.world_law_gaias_covenant.isEnabled())
		{
			return false;
		}
		World.world.drop_manager.spawn(pTile, "seeds_grass", 15f, -1f, -1L);
		return true;
	}

	// Token: 0x06000131 RID: 305 RVA: 0x0000B186 File Offset: 0x00009386
	public static bool castSpawnFertilizer(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTile == null)
		{
			pTile = pTarget.current_tile;
		}
		if (pTile == null)
		{
			return false;
		}
		World.world.drop_manager.spawn(pTile, "fertilizer_trees", 15f, -1f, -1L);
		return true;
	}

	// Token: 0x06000132 RID: 306 RVA: 0x0000B1BC File Offset: 0x000093BC
	public static bool castCurses(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget != null)
		{
			if (pTarget.a.hasStatus("cursed"))
			{
				return false;
			}
			pTile = pTarget.current_tile;
		}
		if (pTile == null)
		{
			return false;
		}
		World.world.drop_manager.spawn(pTile, "curse", 15f, -1f, -1L);
		return true;
	}

	// Token: 0x06000133 RID: 307 RVA: 0x0000B210 File Offset: 0x00009410
	public static bool castLightning(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget != null)
		{
			pTile = pTarget.current_tile;
		}
		MapBox.spawnLightningMedium(pTile, 0.15f, pSelf.a);
		return true;
	}

	// Token: 0x06000134 RID: 308 RVA: 0x0000B22F File Offset: 0x0000942F
	public static bool castTornado(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget != null)
		{
			pTile = pTarget.current_tile;
		}
		if (pTile == null)
		{
			return false;
		}
		(EffectsLibrary.spawnAtTile("fx_tornado", pTile, 0.083333336f) as TornadoEffect).resizeTornado(0.16666667f);
		return true;
	}

	// Token: 0x06000135 RID: 309 RVA: 0x0000B261 File Offset: 0x00009461
	public static bool castCure(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTile == null)
		{
			pTile = pTarget.current_tile;
		}
		if (pTile == null)
		{
			return false;
		}
		World.world.drop_manager.spawn(pTile, "cure", 15f, -1f, -1L);
		return true;
	}

	// Token: 0x06000136 RID: 310 RVA: 0x0000B296 File Offset: 0x00009496
	public static bool castShieldOnHimself(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		return ActionLibrary.addShieldEffectOnTarget(pSelf, pTarget, null);
	}

	// Token: 0x06000137 RID: 311 RVA: 0x0000B2A0 File Offset: 0x000094A0
	public static bool addShieldEffectOnTarget(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget.hasStatus("shield"))
		{
			return false;
		}
		pTarget.a.addStatusEffect("shield", 30f, true);
		return true;
	}

	// Token: 0x06000138 RID: 312 RVA: 0x0000B2CC File Offset: 0x000094CC
	public static bool addBurningEffectOnTarget(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (!pTarget.isAlive())
		{
			return false;
		}
		if (pTarget.isBuilding() && pTarget.b.isBurnable())
		{
			pTarget.addStatusEffect("burning", 0f, true);
			return true;
		}
		if (pTarget.isActor())
		{
			pTarget.addStatusEffect("burning", 0f, true);
			return true;
		}
		return false;
	}

	// Token: 0x06000139 RID: 313 RVA: 0x0000B329 File Offset: 0x00009529
	public static bool addFrozenEffectOnTarget20(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		return pTarget.isAlive() && !pTarget.isBuilding() && Randy.randomChance(0.2f) && ActionLibrary.addFrozenEffectOnTarget(pSelf, pTarget, pTile);
	}

	// Token: 0x0600013A RID: 314 RVA: 0x0000B355 File Offset: 0x00009555
	public static bool addStunnedEffectOnTarget20(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		return !pTarget.isRekt() && !pTarget.isBuilding() && Randy.randomChance(0.2f) && ActionLibrary.addStunnedEffectOnTarget(pSelf, pTarget, pTile);
	}

	// Token: 0x0600013B RID: 315 RVA: 0x0000B381 File Offset: 0x00009581
	public static bool addStunnedEffectOnTarget(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget.isRekt())
		{
			return false;
		}
		if (pTarget.isBuilding())
		{
			return false;
		}
		pTarget.addStatusEffect("stunned", 0f, true);
		return true;
	}

	// Token: 0x0600013C RID: 316 RVA: 0x0000B3AC File Offset: 0x000095AC
	public static bool addFrozenEffectOnTarget(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget.isBuilding())
		{
			return false;
		}
		if (pTarget.current_tile.Type.lava)
		{
			return false;
		}
		if (pTarget.current_tile.isOnFire())
		{
			return false;
		}
		pTarget.addStatusEffect("frozen", 0f, true);
		return true;
	}

	// Token: 0x0600013D RID: 317 RVA: 0x0000B3F9 File Offset: 0x000095F9
	public static bool addSlowEffectOnTarget20(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		return pTarget.isAlive() && !pTarget.isBuilding() && Randy.randomChance(0.2f) && ActionLibrary.addSlowEffectOnTarget(pSelf, pTarget, pTile);
	}

	// Token: 0x0600013E RID: 318 RVA: 0x0000B425 File Offset: 0x00009625
	public static bool addSlowEffectOnTarget(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget.isBuilding())
		{
			return false;
		}
		pTarget.addStatusEffect("slowness", 0f, true);
		return true;
	}

	// Token: 0x0600013F RID: 319 RVA: 0x0000B444 File Offset: 0x00009644
	public static bool addPoisonedEffectOnTarget(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (!pTarget.isActor())
		{
			return false;
		}
		if (!pTarget.isAlive())
		{
			return false;
		}
		if (pTarget.a.hasTrait("poison_immune"))
		{
			return false;
		}
		if (!pTarget.a.asset.has_skin)
		{
			return false;
		}
		if (pTarget.a.asset.immune_to_injuries)
		{
			return false;
		}
		if (Randy.randomChance(0.3f))
		{
			pTarget.a.addStatusEffect("poisoned", 0f, true);
		}
		return false;
	}

	// Token: 0x06000140 RID: 320 RVA: 0x0000B4C5 File Offset: 0x000096C5
	public static void increaseDroppedBombsCounter(WorldTile pTile = null, string pDropID = null)
	{
		World.world.game_stats.data.bombsDropped += 1L;
		AchievementLibrary.many_bombs.check(null);
	}

	// Token: 0x06000141 RID: 321 RVA: 0x0000B4F0 File Offset: 0x000096F0
	public static bool giveCursed(WorldTile pTile, Actor pActor)
	{
		if (pActor.hasSubspecies() && pActor.subspecies.hasTrait("adaptation_corruption"))
		{
			return false;
		}
		bool flag = pActor.addStatusEffect("cursed", 0f, true);
		if (flag)
		{
			pActor.removeTrait("blessed");
		}
		return flag;
	}

	// Token: 0x06000142 RID: 322 RVA: 0x0000B530 File Offset: 0x00009730
	public static bool singularityTeleportation(WorldTile pTile, Actor pActor)
	{
		BiomeAsset tBiome = AssetManager.biome_library.get("biome_singularity");
		WorldTile tTileTarget = null;
		if (tBiome.getTileHigh().hashset.Count > 0 && Randy.randomBool())
		{
			tTileTarget = tBiome.getTileHigh().hashset.GetRandom<WorldTile>();
		}
		else if (tBiome.getTileLow().hashset.Count > 0)
		{
			tTileTarget = tBiome.getTileLow().hashset.GetRandom<WorldTile>();
		}
		if (tTileTarget == null)
		{
			return false;
		}
		EffectsLibrary.spawnAt("fx_teleport_singularity", tTileTarget.posV3, pActor.stats["scale"] * 1.2f);
		EffectsLibrary.spawnAt("fx_teleport_singularity", pActor.current_position, pActor.stats["scale"] * 1.2f);
		pActor.cancelAllBeh();
		pActor.spawnOn(tTileTarget, 0f);
		pActor.makeStunned(5f);
		return true;
	}

	// Token: 0x06000143 RID: 323 RVA: 0x0000B614 File Offset: 0x00009814
	public static bool timeParadox(WorldTile pTile, Actor pActor)
	{
		if (pActor.isAlive())
		{
			ActorData data = pActor.data;
			int age_overgrowth = data.age_overgrowth;
			data.age_overgrowth = age_overgrowth + 1;
			return true;
		}
		return false;
	}

	// Token: 0x06000144 RID: 324 RVA: 0x0000B641 File Offset: 0x00009841
	public static bool giveEnchanted(WorldTile pTile, Actor pActor)
	{
		pActor.finishStatusEffect("cursed");
		return pActor.addStatusEffect("enchanted", 0f, true);
	}

	// Token: 0x06000145 RID: 325 RVA: 0x0000B660 File Offset: 0x00009860
	public static bool spawnGhost(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (!pTarget.isActor())
		{
			return false;
		}
		if (!pTarget.a.asset.has_soul)
		{
			return false;
		}
		Actor tGhost = World.world.units.createNewUnit("ghost", pTile, false, 0f, null, null, true, false, false, false);
		tGhost.removeTrait("blessed");
		ActorTool.copyUnitToOtherUnit(pTarget.a, tGhost, true);
		return true;
	}

	// Token: 0x06000146 RID: 326 RVA: 0x0000B6C6 File Offset: 0x000098C6
	public static bool tryToGrowBiomeGrass(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (!pTile.Type.can_be_biome)
		{
			return false;
		}
		if (pTile.Type.is_biome)
		{
			return false;
		}
		DropsLibrary.useSeedOn(pTile, TopTileLibrary.grass_low, TopTileLibrary.grass_high);
		return true;
	}

	// Token: 0x06000147 RID: 327 RVA: 0x0000B6F7 File Offset: 0x000098F7
	public static bool tryToGrowTree(BaseSimObject pTarget, WorldTile pTile = null)
	{
		BuildingActions.tryGrowVegetationRandom(pTile, VegetationType.Trees, false, false, true);
		return true;
	}

	// Token: 0x06000148 RID: 328 RVA: 0x0000B704 File Offset: 0x00009904
	public static bool tryToCreatePlants(BaseSimObject pTarget, WorldTile pTile = null)
	{
		BiomeAsset tBiomeAsset = pTarget.current_tile.Type.biome_asset;
		if (tBiomeAsset == null)
		{
			return false;
		}
		if (tBiomeAsset.grow_type_selector_plants != null)
		{
			BuildingActions.tryGrowVegetationRandom(pTarget.current_tile, VegetationType.Plants, false, true, true);
		}
		return true;
	}

	// Token: 0x06000149 RID: 329 RVA: 0x0000B73F File Offset: 0x0000993F
	public static bool startNuke(BaseSimObject pTarget, WorldTile pTile = null)
	{
		pTarget.a.findCurrentTile(true);
		EffectsLibrary.spawn("fx_nuke_flash", pTile, "atomic_bomb", null, 0f, -1f, -1f, null);
		return true;
	}

	// Token: 0x0600014A RID: 330 RVA: 0x0000B770 File Offset: 0x00009970
	public static bool clearCrabzilla(BaseSimObject pTarget, WorldTile pTile = null)
	{
		MusicBox.inst.stopDrawingSound("event:/SFX/UNIQUE/Crabzilla/CrabzillaLazer");
		MusicBox.inst.stopDrawingSound("event:/SFX/UNIQUE/Crabzilla/CrabzillaVoice");
		if (Config.joyControls)
		{
			UltimateJoystick.ResetJoysticks();
		}
		return true;
	}

	// Token: 0x0600014B RID: 331 RVA: 0x0000B79D File Offset: 0x0000999D
	public static bool startCrabzillaNuke(BaseSimObject pTarget, WorldTile pTile = null)
	{
		pTarget.a.findCurrentTile(true);
		EffectsLibrary.spawn("fx_nuke_flash", pTile, "crabzilla_bomb", null, 0f, -1f, -1f, null);
		return true;
	}

	// Token: 0x0600014C RID: 332 RVA: 0x0000B7CE File Offset: 0x000099CE
	public static bool deathNuke(BaseSimObject pTarget, WorldTile pTile = null)
	{
		pTarget.a.findCurrentTile(true);
		DropsLibrary.action_atomic_bomb(pTarget.current_tile, null);
		return true;
	}

	// Token: 0x0600014D RID: 333 RVA: 0x0000B7E9 File Offset: 0x000099E9
	public static bool deathBomb(BaseSimObject pTarget, WorldTile pTile = null)
	{
		pTarget.a.findCurrentTile(true);
		DropsLibrary.action_bomb(pTarget.current_tile, null);
		return true;
	}

	// Token: 0x0600014E RID: 334 RVA: 0x0000B804 File Offset: 0x00009A04
	public static bool spawnAliens(BaseSimObject pTarget, WorldTile pTile = null)
	{
		Actor a = pTarget.a;
		a.findCurrentTile(true);
		if (!a.inMapBorder())
		{
			return false;
		}
		int tAmount = 1;
		if (Randy.randomChance(0.5f))
		{
			tAmount++;
		}
		if (Randy.randomChance(0.1f))
		{
			tAmount++;
		}
		for (int i = 0; i < tAmount; i++)
		{
			World.world.units.createNewUnit("alien", pTarget.a.current_tile, false, pTarget.a.position_height, null, null, true, true, false, false);
		}
		return true;
	}

	// Token: 0x0600014F RID: 335 RVA: 0x0000B888 File Offset: 0x00009A88
	public static bool fireDropsSpawn(BaseSimObject pTarget, WorldTile pTile = null)
	{
		for (int i = 0; i < 5; i++)
		{
			if (Randy.randomBool())
			{
				World.world.drop_manager.spawnParabolicDrop(pTarget.a.current_tile, "fire", 0f, 0.1f, 5f, 0.5f, 4f, 0.15f);
			}
		}
		if (!pTarget.isActor())
		{
			return true;
		}
		if (pTarget.a.asset.actor_size < ActorSize.S17_Dragon)
		{
			return true;
		}
		for (int j = 0; j < 25; j++)
		{
			if (Randy.randomBool())
			{
				World.world.drop_manager.spawnParabolicDrop(pTarget.a.current_tile, "fire", 0f, 0.1f, 10f, 0.5f, 10f, 0.15f);
			}
			for (int k = 0; k < pTarget.a.current_tile.neighboursAll.Length; k++)
			{
				WorldTile tTile = pTarget.a.current_tile.neighboursAll[k];
				if (Randy.randomBool())
				{
					World.world.drop_manager.spawnParabolicDrop(tTile, "fire", 0f, 0.1f, 10f, 0.5f, 7f, 0.15f);
				}
			}
		}
		return true;
	}

	// Token: 0x06000150 RID: 336 RVA: 0x0000B9C8 File Offset: 0x00009BC8
	public static bool snowDropsSpawn(BaseSimObject pTarget, WorldTile pTile = null)
	{
		for (int i = 0; i < 20; i++)
		{
			if (Randy.randomBool())
			{
				World.world.drop_manager.spawnParabolicDrop(pTarget.a.current_tile, "snow", 0f, 0.1f, 5f, 0.5f, 4f, 0.15f);
			}
		}
		return true;
	}

	// Token: 0x06000151 RID: 337 RVA: 0x0000BA28 File Offset: 0x00009C28
	public static bool teleportRandom(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		TileIsland randomIslandGround = World.world.islands_calculator.getRandomIslandGround(true);
		WorldTile worldTile;
		if (randomIslandGround == null)
		{
			worldTile = null;
		}
		else
		{
			MapRegion random = randomIslandGround.regions.GetRandom();
			worldTile = ((random != null) ? random.tiles.GetRandom<WorldTile>() : null);
		}
		WorldTile tTile = worldTile;
		if (tTile == null)
		{
			return false;
		}
		if (tTile.Type.block)
		{
			return false;
		}
		if (!tTile.Type.ground)
		{
			return false;
		}
		ActionLibrary.teleportEffect(pTarget.a, tTile);
		pTarget.a.cancelAllBeh();
		pTarget.a.spawnOn(tTile, 0f);
		return true;
	}

	// Token: 0x06000152 RID: 338 RVA: 0x0000BAB4 File Offset: 0x00009CB4
	public static void teleportEffect(Actor pActor, WorldTile pTile)
	{
		string tTeleportEffect = pActor.asset.effect_teleport;
		if (string.IsNullOrEmpty(tTeleportEffect))
		{
			tTeleportEffect = "fx_teleport_blue";
		}
		EffectsLibrary.spawnAt(tTeleportEffect, pActor.current_position, pActor.stats["scale"]);
		BaseEffect tEffect = EffectsLibrary.spawnAt(tTeleportEffect, pTile.posV3, pActor.stats["scale"]);
		if (tEffect != null)
		{
			tEffect.sprite_animation.setFrameIndex(9);
		}
	}

	// Token: 0x06000153 RID: 339 RVA: 0x0000BB2C File Offset: 0x00009D2C
	public static bool metamorphInto(Actor pTarget, string pAsset, bool pRemoveAcquiredTraits = false, bool pUseCurrentSubspecies = false)
	{
		if (pTarget == null)
		{
			return false;
		}
		if (!pTarget.inMapBorder())
		{
			return false;
		}
		if (pTarget.isAlreadyTransformed())
		{
			return false;
		}
		pTarget.finishStatusEffect("cursed");
		pTarget.removeTrait("infected");
		pTarget.removeTrait("mush_spores");
		pTarget.removeTrait("tumor_infection");
		if (pRemoveAcquiredTraits)
		{
			IReadOnlyCollection<ActorTrait> tTraits = pTarget.getTraits();
			using (ListPool<ActorTrait> tToRemove = new ListPool<ActorTrait>(tTraits.Count))
			{
				foreach (ActorTrait tTrait in tTraits)
				{
					if (tTrait.group_id == "acquired")
					{
						tToRemove.Add(tTrait);
					}
				}
				pTarget.removeTraits(tToRemove);
			}
		}
		Subspecies tPreviousSubspecies = null;
		if (pUseCurrentSubspecies)
		{
			tPreviousSubspecies = pTarget.subspecies;
		}
		Actor tNewUnit = World.world.units.createNewUnit(pAsset, pTarget.current_tile, false, 0f, tPreviousSubspecies, null, false, false, false, false);
		ActorTool.copyUnitToOtherUnit(pTarget, tNewUnit, false);
		EffectsLibrary.spawn("fx_spawn", tNewUnit.current_tile, null, null, 0f, -1f, -1f, null);
		ActionLibrary.removeUnit(pTarget);
		pTarget.setTransformed();
		tNewUnit.addTrait("metamorphed", false);
		return true;
	}

	// Token: 0x06000154 RID: 340 RVA: 0x0000BC7C File Offset: 0x00009E7C
	public static bool turnIntoMush(BaseSimObject pTarget, WorldTile pTile = null)
	{
		Actor pActor = pTarget.a;
		if (pActor == null)
		{
			return false;
		}
		if (!pActor.hasTrait("mush_spores"))
		{
			return false;
		}
		if (!pActor.inMapBorder())
		{
			return false;
		}
		if (!pActor.asset.can_turn_into_mush)
		{
			return false;
		}
		if (pActor.isAlreadyTransformed())
		{
			return false;
		}
		pActor.finishStatusEffect("cursed");
		pActor.removeTrait("infected");
		pActor.removeTrait("mush_spores");
		pActor.removeTrait("tumor_infection");
		pActor.removeTrait("peaceful");
		Actor tMush = World.world.units.createNewUnit(pActor.asset.mush_id, pActor.current_tile, false, 0f, null, null, false, false, false, false);
		ActorTool.copyUnitToOtherUnit(pActor, tMush, true);
		if (MapBox.isRenderGameplay())
		{
			EffectsLibrary.spawn("fx_spawn", tMush.current_tile, null, null, 0f, -1f, -1f, null);
		}
		ActionLibrary.removeUnit(pTarget.a);
		pActor.setTransformed();
		return true;
	}

	// Token: 0x06000155 RID: 341 RVA: 0x0000BD70 File Offset: 0x00009F70
	public static Actor turnIntoMetamorph(BaseSimObject pTarget, string pAssetID)
	{
		Actor pActor = pTarget.a;
		if (pActor == null)
		{
			return null;
		}
		if (!pActor.inMapBorder())
		{
			return null;
		}
		if (pActor.isAlreadyTransformed())
		{
			return null;
		}
		pActor.finishStatusEffect("cursed");
		pActor.removeTrait("infected");
		pActor.removeTrait("mush_spores");
		pActor.removeTrait("tumor_infection");
		pActor.removeTrait("peaceful");
		Actor tNewMetamorphedUnit = World.world.units.createNewUnit(pAssetID, pActor.current_tile, false, 0f, null, null, false, false, false, false);
		ActorTool.copyUnitToOtherUnit(pActor, tNewMetamorphedUnit, true);
		EffectsLibrary.spawn("fx_spawn", tNewMetamorphedUnit.current_tile, null, null, 0f, -1f, -1f, null);
		ActionLibrary.removeUnit(pTarget.a);
		pActor.setTransformed();
		return tNewMetamorphedUnit;
	}

	// Token: 0x06000156 RID: 342 RVA: 0x0000BE34 File Offset: 0x0000A034
	public static Actor turnIntoIceOne(BaseSimObject pTarget, WorldTile pTile = null)
	{
		return ActionLibrary.turnIntoMetamorph(pTarget, "cold_one");
	}

	// Token: 0x06000157 RID: 343 RVA: 0x0000BE44 File Offset: 0x0000A044
	public static bool turnIntoDemon(BaseSimObject pTarget, WorldTile pTile = null)
	{
		Actor pActor = pTarget.a;
		if (pActor == null)
		{
			return false;
		}
		if (!pActor.inMapBorder())
		{
			return false;
		}
		if (pActor.isAlreadyTransformed())
		{
			return false;
		}
		pActor.finishStatusEffect("cursed");
		pActor.removeTrait("infected");
		pActor.removeTrait("mush_spores");
		pActor.removeTrait("tumor_infection");
		pActor.removeTrait("peaceful");
		Actor tDemon = World.world.units.createNewUnit("demon", pActor.current_tile, false, 0f, null, null, false, false, false, false);
		tDemon.addTrait("metamorphed", false);
		ActorTool.copyUnitToOtherUnit(pActor, tDemon, true);
		EffectsLibrary.spawn("fx_spawn", tDemon.current_tile, null, null, 0f, -1f, -1f, null);
		ActionLibrary.removeUnit(pTarget.a);
		pActor.setTransformed();
		return true;
	}

	// Token: 0x06000158 RID: 344 RVA: 0x0000BF1C File Offset: 0x0000A11C
	public static bool turnIntoTumorMonster(BaseSimObject pTarget, WorldTile pTile = null)
	{
		Actor pActor = pTarget.a;
		if (pActor == null)
		{
			return false;
		}
		if (!pActor.hasTrait("tumor_infection"))
		{
			return false;
		}
		if (!pActor.inMapBorder())
		{
			return false;
		}
		if (!pActor.asset.can_turn_into_tumor)
		{
			return false;
		}
		if (pActor.isAlreadyTransformed())
		{
			return false;
		}
		pActor.finishStatusEffect("cursed");
		pActor.removeTrait("infected");
		pActor.removeTrait("mush_spores");
		pActor.removeTrait("tumor_infection");
		pActor.removeTrait("peaceful");
		Actor tTumorMonster = World.world.units.createNewUnit(pActor.asset.tumor_id, pActor.current_tile, false, 0f, null, null, false, false, false, false);
		ActorTool.copyUnitToOtherUnit(pActor, tTumorMonster, true);
		EffectsLibrary.spawn("fx_spawn", tTumorMonster.current_tile, null, null, 0f, -1f, -1f, null);
		ActionLibrary.removeUnit(pTarget.a);
		pActor.setTransformed();
		return true;
	}

	// Token: 0x06000159 RID: 345 RVA: 0x0000C008 File Offset: 0x0000A208
	public static bool turnIntoZombie(BaseSimObject pTarget, WorldTile pTile = null)
	{
		Actor pActor = pTarget.a;
		if (pActor == null)
		{
			return false;
		}
		if (!pActor.hasTrait("infected"))
		{
			return false;
		}
		if (!pActor.inMapBorder())
		{
			return false;
		}
		if (!pActor.asset.can_turn_into_zombie)
		{
			return false;
		}
		if (pActor.isAlreadyTransformed())
		{
			return false;
		}
		pActor.finishStatusEffect("cursed");
		pActor.removeTrait("infected");
		pActor.removeTrait("mush_spores");
		pActor.removeTrait("tumor_infection");
		string tStatsID = pActor.asset.getZombieID();
		if (pActor.asset.id == "dragon")
		{
			pActor.removeTrait("fire_blood");
			pActor.removeTrait("fire_proof");
		}
		Actor tZombie = World.world.units.createNewUnit(tStatsID, pActor.current_tile, false, 0f, null, pActor.subspecies, false, false, false, false);
		ActorTool.copyUnitToOtherUnit(pActor, tZombie, true);
		tZombie.removeTrait("fast");
		tZombie.removeTrait("agile");
		tZombie.removeTrait("genius");
		tZombie.removeTrait("peaceful");
		if (!pActor.getName().StartsWith("Un"))
		{
			tZombie.setName("Un" + Toolbox.LowerCaseFirst(pActor.getName()), true);
		}
		EffectsLibrary.spawn("fx_spawn", tZombie.current_tile, null, null, 0f, -1f, -1f, null);
		ActionLibrary.removeUnit(pTarget.a);
		pActor.setTransformed();
		return true;
	}

	// Token: 0x0600015A RID: 346 RVA: 0x0000C178 File Offset: 0x0000A378
	public static bool turnIntoSkeleton(BaseSimObject pTarget, WorldTile pTile = null)
	{
		Actor pActor = pTarget.a;
		if (string.IsNullOrEmpty(pActor.asset.skeleton_id))
		{
			return false;
		}
		if (pActor == null)
		{
			return false;
		}
		if (!pActor.hasStatus("cursed"))
		{
			return false;
		}
		if (!pActor.inMapBorder())
		{
			return false;
		}
		if (pActor.isAlreadyTransformed())
		{
			return false;
		}
		string tStatsID = pActor.asset.skeleton_id;
		pActor.finishStatusEffect("cursed");
		pActor.removeTrait("infected");
		pActor.removeTrait("mush_spores");
		pActor.removeTrait("tumor_infection");
		Subspecies tTargetSubspecies = null;
		if (pActor.hasSubspecies())
		{
			tTargetSubspecies = pActor.subspecies.getSkeletonForm();
		}
		Actor tSkeletonActor = World.world.units.createNewUnit(tStatsID, pActor.current_tile, false, 0f, tTargetSubspecies, null, false, false, false, false);
		Subspecies tSkeletonSubspecies = tSkeletonActor.subspecies;
		if (tSkeletonSubspecies.isJustCreated() && tTargetSubspecies != null)
		{
			tTargetSubspecies.setSkeletonForm(tSkeletonSubspecies);
		}
		ActorTool.copyUnitToOtherUnit(pActor, tSkeletonActor, true);
		if (!pActor.getName().StartsWith("Un"))
		{
			tSkeletonActor.setName("Un" + Toolbox.LowerCaseFirst(pActor.getName()), true);
		}
		EffectsLibrary.spawn("fx_spawn", tSkeletonActor.current_tile, null, null, 0f, -1f, -1f, null);
		ActionLibrary.removeUnit(pTarget.a);
		pActor.setTransformed();
		return true;
	}

	// Token: 0x0600015B RID: 347 RVA: 0x0000C2C0 File Offset: 0x0000A4C0
	public static Actor getActorNearPos(Vector2 pPos)
	{
		Actor tResult = null;
		float tDistBest = float.MaxValue;
		Actor[] tArr = World.world.units.visible_units.array;
		int tLen = World.world.units.visible_units.count;
		for (int i = 0; i < tLen; i++)
		{
			Actor tActor = tArr[i];
			if (tActor.isAlive() && tActor.asset.can_be_inspected && !tActor.isInsideSomething())
			{
				float tDist = Toolbox.DistVec2Float(tActor.current_position, pPos);
				if (tDist <= 3f && tDist < tDistBest)
				{
					tResult = tActor;
					tDistBest = tDist;
				}
			}
		}
		return tResult;
	}

	// Token: 0x0600015C RID: 348 RVA: 0x0000C35C File Offset: 0x0000A55C
	public static Actor getActorFromTile(WorldTile pTile = null)
	{
		if (pTile == null)
		{
			return null;
		}
		Actor tResult = null;
		float tDistBest = float.MaxValue;
		List<Actor> tActorList = World.world.units.getSimpleList();
		for (int i = 0; i < tActorList.Count; i++)
		{
			Actor tActor = tActorList[i];
			if (tActor.isAlive())
			{
				float tDist = (float)Toolbox.SquaredDistTile(tActor.current_tile, pTile);
				if (tDist <= tDistBest && tDist <= 9f && tActor.asset.can_be_inspected && !tActor.isInsideSomething())
				{
					tResult = tActor;
					tDistBest = tDist;
				}
			}
		}
		return tResult;
	}

	// Token: 0x0600015D RID: 349 RVA: 0x0000C3E5 File Offset: 0x0000A5E5
	public static void openUnitWindow(Actor pActor)
	{
		if (!pActor.isRekt())
		{
			SelectedUnit.clear();
			SelectedUnit.select(pActor, true);
		}
		else if (!SelectedUnit.isSet())
		{
			return;
		}
		ScrollWindow.showWindow("unit");
	}

	// Token: 0x0600015E RID: 350 RVA: 0x0000C410 File Offset: 0x0000A610
	public static bool inspectUnit(WorldTile pTile = null, string pPower = null)
	{
		Actor tResult;
		if (pTile == null)
		{
			tResult = World.world.getActorNearCursor();
		}
		else
		{
			tResult = ActionLibrary.getActorFromTile(pTile);
		}
		if (tResult == null)
		{
			return false;
		}
		ActionLibrary.openUnitWindow(tResult);
		return true;
	}

	// Token: 0x0600015F RID: 351 RVA: 0x0000C444 File Offset: 0x0000A644
	public static bool inspectUnitSelectedMeta(WorldTile pTile = null, string pPower = null)
	{
		Actor tActor;
		if (pTile == null)
		{
			tActor = World.world.getActorNearCursor();
		}
		else
		{
			tActor = ActionLibrary.getActorFromTile(pTile);
		}
		if (tActor == null)
		{
			return false;
		}
		MetaTypeAsset tMetaAsset = Zones.getCurrentMapBorderMode(false).getAsset();
		if (tMetaAsset == null)
		{
			return false;
		}
		if (tMetaAsset.check_unit_has_meta(tActor))
		{
			tMetaAsset.set_unit_set_meta_for_meta_for_window(tActor);
			ScrollWindow.showWindow(tMetaAsset.window_name);
			return true;
		}
		ActionLibrary.openUnitWindow(tActor);
		return true;
	}

	// Token: 0x06000160 RID: 352 RVA: 0x0000C4B0 File Offset: 0x0000A6B0
	public static bool inspectCity(WorldTile pTile = null, string pPower = null)
	{
		if (pTile == null)
		{
			return false;
		}
		NanoObject tMetaObject = ActionLibrary.getNanoObjectFromTile(pTile, MetaTypeLibrary.city);
		if (tMetaObject == null)
		{
			return false;
		}
		MetaType.City.getAsset().selectAndInspect(tMetaObject, false, true, false);
		return true;
	}

	// Token: 0x06000161 RID: 353 RVA: 0x0000C4E4 File Offset: 0x0000A6E4
	public static bool inspectKingdom(WorldTile pTile = null, string pPower = null)
	{
		if (pTile == null)
		{
			return false;
		}
		NanoObject tMetaObject = ActionLibrary.getNanoObjectFromTile(pTile, MetaTypeLibrary.kingdom);
		if (tMetaObject == null)
		{
			return false;
		}
		if (((Kingdom)tMetaObject).isNeutral())
		{
			return false;
		}
		MetaType.Kingdom.getAsset().selectAndInspect(tMetaObject, false, true, false);
		return true;
	}

	// Token: 0x06000162 RID: 354 RVA: 0x0000C528 File Offset: 0x0000A728
	public static bool inspectAlliance(WorldTile pTile = null, string pPower = null)
	{
		if (pTile == null)
		{
			return false;
		}
		City tCity = pTile.zone.city;
		if (tCity.isRekt())
		{
			return false;
		}
		Kingdom tKingdom = tCity.kingdom;
		if (tKingdom.isRekt())
		{
			return false;
		}
		if (tKingdom.isNeutral())
		{
			return false;
		}
		if (tKingdom.hasAlliance())
		{
			MetaType.Alliance.getAsset().selectAndInspect(tKingdom.getAlliance(), false, true, false);
		}
		else
		{
			ActionLibrary.inspectKingdom(pTile, pPower);
		}
		return true;
	}

	// Token: 0x06000163 RID: 355 RVA: 0x0000C594 File Offset: 0x0000A794
	public static bool inspectCulture(WorldTile pTile, string pPower = null)
	{
		if (pTile == null)
		{
			return false;
		}
		NanoObject tMetaObject = ActionLibrary.getNanoObjectFromTile(pTile, MetaTypeLibrary.culture);
		if (tMetaObject == null)
		{
			return false;
		}
		MetaType.Culture.getAsset().selectAndInspect(tMetaObject, false, true, false);
		return true;
	}

	// Token: 0x06000164 RID: 356 RVA: 0x0000C5C8 File Offset: 0x0000A7C8
	public static bool inspectReligion(WorldTile pTile, string pPower = null)
	{
		if (pTile == null)
		{
			return false;
		}
		NanoObject tMetaObject = ActionLibrary.getNanoObjectFromTile(pTile, MetaTypeLibrary.religion);
		if (tMetaObject == null)
		{
			return false;
		}
		MetaType.Religion.getAsset().selectAndInspect(tMetaObject, false, true, false);
		return true;
	}

	// Token: 0x06000165 RID: 357 RVA: 0x0000C5FC File Offset: 0x0000A7FC
	public static bool inspectSubspecies(WorldTile pTile, string pPower = null)
	{
		if (pTile == null)
		{
			return false;
		}
		NanoObject tMetaObject = ActionLibrary.getNanoObjectFromTile(pTile, MetaTypeLibrary.subspecies);
		if (tMetaObject == null)
		{
			return false;
		}
		MetaType.Subspecies.getAsset().selectAndInspect(tMetaObject, false, true, false);
		return true;
	}

	// Token: 0x06000166 RID: 358 RVA: 0x0000C630 File Offset: 0x0000A830
	public static bool inspectFamily(WorldTile pTile, string pPower = null)
	{
		if (pTile == null)
		{
			return false;
		}
		NanoObject tMetaObject = ActionLibrary.getNanoObjectFromTile(pTile, MetaTypeLibrary.family);
		if (tMetaObject == null)
		{
			return false;
		}
		MetaType.Family.getAsset().selectAndInspect(tMetaObject, false, true, false);
		return true;
	}

	// Token: 0x06000167 RID: 359 RVA: 0x0000C664 File Offset: 0x0000A864
	public static bool inspectArmy(WorldTile pTile, string pPower = null)
	{
		if (pTile == null)
		{
			return false;
		}
		NanoObject tMetaObject = ActionLibrary.getNanoObjectFromTile(pTile, MetaTypeLibrary.army);
		if (tMetaObject == null)
		{
			return false;
		}
		MetaType.Army.getAsset().selectAndInspect(tMetaObject, false, true, false);
		return true;
	}

	// Token: 0x06000168 RID: 360 RVA: 0x0000C698 File Offset: 0x0000A898
	public static bool inspectLanguage(WorldTile pTile, string pPower = null)
	{
		if (pTile == null)
		{
			return false;
		}
		NanoObject tMetaObject = ActionLibrary.getNanoObjectFromTile(pTile, MetaTypeLibrary.language);
		if (tMetaObject == null)
		{
			return false;
		}
		MetaType.Language.getAsset().selectAndInspect(tMetaObject, false, true, false);
		return true;
	}

	// Token: 0x06000169 RID: 361 RVA: 0x0000C6CC File Offset: 0x0000A8CC
	public static bool inspectClan(WorldTile pTile, string pPower = null)
	{
		if (pTile == null)
		{
			return false;
		}
		NanoObject tMetaObject = ActionLibrary.getNanoObjectFromTile(pTile, MetaTypeLibrary.clan);
		if (tMetaObject == null)
		{
			return false;
		}
		MetaType.Clan.getAsset().selectAndInspect(tMetaObject, false, true, false);
		return true;
	}

	// Token: 0x0600016A RID: 362 RVA: 0x0000C6FF File Offset: 0x0000A8FF
	public static bool burnTile(BaseSimObject pSelf, BaseSimObject pTarget = null, WorldTile pTile = null)
	{
		if (!World.world.flash_effects.contains(pTile) && Randy.randomChance(0.2f))
		{
			World.world.particles_fire.spawn(pTile.posV3);
		}
		pTile.startFire(true);
		return true;
	}

	// Token: 0x0600016B RID: 363 RVA: 0x0000C740 File Offset: 0x0000A940
	public static bool tryToEvolveUnitViaMonolith(Actor pActor)
	{
		pActor.startShake(0.3f, 0.1f, true, true);
		pActor.startColorEffect(ActorColorEffect.White);
		if (!pActor.hasSubspecies())
		{
			return false;
		}
		if (pActor.hasSubspeciesTrait("pure"))
		{
			return false;
		}
		float tChance = 1f;
		if (pActor.asset.can_evolve_into_new_species)
		{
			tChance = 1f;
		}
		else if (pActor.hasSubspeciesTrait("uplifted") && pActor.subspecies.isSapient())
		{
			tChance = 0.1f;
		}
		if (!Randy.randomChance(tChance))
		{
			return false;
		}
		World.world.units.evolutionEvent(pActor, true, false);
		return true;
	}

	// Token: 0x0600016C RID: 364 RVA: 0x0000C7D8 File Offset: 0x0000A9D8
	public static bool tryToEvolveUnitViaAscension(Actor pActor, out Actor pEvolvedActorForm)
	{
		pEvolvedActorForm = null;
		pActor.startShake(0.3f, 0.1f, true, true);
		pActor.startColorEffect(ActorColorEffect.White);
		if (!pActor.hasSubspecies())
		{
			return false;
		}
		if (pActor.hasSubspeciesTrait("pure"))
		{
			return false;
		}
		Actor tNewActor = World.world.units.evolutionEvent(pActor, true, true);
		pEvolvedActorForm = tNewActor;
		return true;
	}

	// Token: 0x0600016D RID: 365 RVA: 0x0000C834 File Offset: 0x0000AA34
	public static void startBurningObjects(BaseSimObject pSelf, BaseSimObject pTarget = null, WorldTile pTile = null)
	{
		List<BaseSimObject> tList = Finder.getAllObjectsInChunks(pTile, 3);
		for (int i = 0; i < tList.Count; i++)
		{
			BaseSimObject tSimObject = tList[i];
			if (tSimObject.isAlive() && !tSimObject.current_tile.Type.ocean)
			{
				ActionLibrary.addBurningEffectOnTarget(pSelf, tSimObject, null);
			}
		}
	}

	// Token: 0x0600016E RID: 366 RVA: 0x0000C885 File Offset: 0x0000AA85
	public static void action_growTornadoes(WorldTile pTile = null, string pDropID = null)
	{
		TornadoEffect.growTornados(pTile);
	}

	// Token: 0x0600016F RID: 367 RVA: 0x0000C88D File Offset: 0x0000AA8D
	public static void action_shrinkTornadoes(WorldTile pTile = null, string pDropID = null)
	{
		TornadoEffect.shrinkTornados(pTile);
	}

	// Token: 0x06000170 RID: 368 RVA: 0x0000C898 File Offset: 0x0000AA98
	public static bool dragonSlayer(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget == null)
		{
			return false;
		}
		if (!pTarget.isActor())
		{
			return false;
		}
		BaseSimObject tAttacker = pTarget.a.attackedBy;
		if (tAttacker != null && tAttacker.isActor() && tAttacker.isAlive())
		{
			tAttacker.a.addTrait("dragonslayer", false);
			return true;
		}
		return false;
	}

	// Token: 0x06000171 RID: 369 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
	public static bool mageSlayerCheck(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget == null)
		{
			return false;
		}
		if (!pTarget.isActor())
		{
			return false;
		}
		if (!pTarget.a.hasSpells())
		{
			return false;
		}
		BaseSimObject tAttacker = pTarget.a.attackedBy;
		if (tAttacker != null && tAttacker.isActor() && tAttacker.isAlive())
		{
			tAttacker.a.addTrait("mageslayer", false);
			return true;
		}
		return false;
	}

	// Token: 0x06000172 RID: 370 RVA: 0x0000C947 File Offset: 0x0000AB47
	public static bool checkPiranhaAchievement(BaseSimObject pTarget, WorldTile pTile = null)
	{
		AchievementLibrary.piranha_land.check(pTarget.a);
		return true;
	}

	// Token: 0x06000173 RID: 371 RVA: 0x0000C95C File Offset: 0x0000AB5C
	public static bool clickRelations(WorldTile pTile, string pPowerID)
	{
		City tCity = pTile.zone.city;
		if (tCity.isRekt())
		{
			return false;
		}
		Kingdom tKingdom = tCity.kingdom;
		if (tKingdom.isRekt())
		{
			return false;
		}
		if (tKingdom.isNeutral())
		{
			return false;
		}
		if (SelectedMetas.selected_kingdom != tKingdom)
		{
			SelectedMetas.selected_kingdom = tKingdom;
		}
		else
		{
			ScrollWindow.showWindow("kingdom");
		}
		return true;
	}

	// Token: 0x06000174 RID: 372 RVA: 0x0000C9B8 File Offset: 0x0000ABB8
	public static bool clickWhisperOfWar(WorldTile pTile, string pPowerID)
	{
		City tCity = pTile.zone.city;
		if (tCity.isRekt())
		{
			return false;
		}
		Kingdom tKingdom = tCity.kingdom;
		if (tKingdom.isRekt())
		{
			return false;
		}
		if (tKingdom.isNeutral())
		{
			return false;
		}
		if (Config.whisper_A == null)
		{
			Config.whisper_A = tKingdom;
			ActionLibrary.showWhisperTip("whisper_selected_first");
			return false;
		}
		if (Config.whisper_B == null && Config.whisper_A == tKingdom)
		{
			ActionLibrary.showWhisperTip("whisper_cancelled");
			Config.whisper_A = null;
			Config.whisper_B = null;
			return false;
		}
		if (Config.whisper_B == null)
		{
			Config.whisper_B = tKingdom;
		}
		if (Config.whisper_B != Config.whisper_A)
		{
			if (Config.whisper_A.isEnemy(Config.whisper_B))
			{
				ActionLibrary.showWhisperTip("whisper_already_in_war");
				Config.whisper_B = null;
				return false;
			}
			if (Config.whisper_A.isInWarOnSameSide(Config.whisper_B))
			{
				using (ListPool<War> tWars = new ListPool<War>(Config.whisper_A.getWars(false)))
				{
					foreach (War ptr in tWars)
					{
						War tWar = ptr;
						if (!tWar.isTotalWar() && tWar.onTheSameSide(Config.whisper_A, Config.whisper_B))
						{
							tWar.leaveWar(Config.whisper_B);
						}
					}
				}
			}
			bool tHaveCommonEnemy = World.world.wars.haveCommonEnemy(Config.whisper_A, Config.whisper_B);
			Alliance tAllianceA = Config.whisper_A.getAlliance();
			if (tAllianceA != null && Alliance.isSame(tAllianceA, Config.whisper_B.getAlliance()))
			{
				tAllianceA.leave(Config.whisper_A, true);
			}
			War tOngoingWar = World.world.wars.getRandomWarFor(Config.whisper_B);
			if (tOngoingWar != null && !tOngoingWar.isTotalWar() && !tHaveCommonEnemy)
			{
				if (tOngoingWar.isAttacker(Config.whisper_B))
				{
					tOngoingWar.joinDefenders(Config.whisper_A);
				}
				else
				{
					tOngoingWar.joinAttackers(Config.whisper_A);
				}
				ActionLibrary.showWhisperTip("whisper_joined_war");
			}
			else
			{
				World.world.diplomacy.startWar(Config.whisper_A, Config.whisper_B, WarTypeLibrary.whisper_of_war, true);
				ActionLibrary.showWhisperTip("whisper_new_war");
			}
			Config.whisper_A.affectKingByPowers();
			Config.whisper_A = null;
			Config.whisper_B = null;
		}
		return true;
	}

	// Token: 0x06000175 RID: 373 RVA: 0x0000CBF8 File Offset: 0x0000ADF8
	public static bool clickUnity(WorldTile pTile, string pPowerID)
	{
		City tCity = pTile.zone.city;
		if (tCity.isRekt())
		{
			return false;
		}
		Kingdom tKingdom = tCity.kingdom;
		if (tKingdom.isRekt())
		{
			return false;
		}
		if (tKingdom.isNeutral())
		{
			return false;
		}
		if (Config.unity_A == null)
		{
			Config.unity_A = tKingdom;
			ActionLibrary.showWhisperTip("unity_selected_first");
			return false;
		}
		if (Config.whisper_B == null && Config.unity_A == tKingdom)
		{
			ActionLibrary.showWhisperTip("unity_cancelled");
			Config.unity_A = null;
			Config.unity_B = null;
			return false;
		}
		if (Config.unity_A.hasAlliance() && tKingdom.hasAlliance() && Config.unity_A.getAlliance() == tKingdom.getAlliance())
		{
			ActionLibrary.showWhisperTip("unity_cancelled");
			Config.unity_A = null;
			Config.unity_B = null;
			return false;
		}
		if (Config.unity_B == null)
		{
			Config.unity_B = tKingdom;
		}
		if (Config.unity_B == Config.unity_A)
		{
			return false;
		}
		if (Config.unity_A.isEnemy(Config.unity_B))
		{
			ActionLibrary.showWhisperTip("unity_in_war");
			Config.unity_B = null;
			return false;
		}
		if (Config.unity_A.hasAlliance())
		{
			if (Config.unity_A.getAlliance() == Config.unity_B.getAlliance())
			{
				ActionLibrary.showWhisperTip("unity_cancelled");
				Config.unity_B = null;
				return false;
			}
			if (Config.unity_B.hasAlliance())
			{
				Config.unity_A.getAlliance().leave(Config.unity_A, true);
			}
		}
		if (World.world.alliances.forceAlliance(Config.unity_A, Config.unity_B))
		{
			ActionLibrary.showWhisperTip("unity_new_alliance");
		}
		else
		{
			ActionLibrary.showWhisperTip("unity_joined_alliance");
		}
		Config.unity_A.affectKingByPowers();
		Config.unity_A = null;
		Config.unity_B = null;
		World.world.zone_calculator.dirtyAndClear();
		return true;
	}

	// Token: 0x06000176 RID: 374 RVA: 0x0000CDA0 File Offset: 0x0000AFA0
	private static void showWhisperTip(string pText)
	{
		string tLocalizedText = LocalizedTextManager.getText(pText, null, false);
		if (Config.whisper_A != null)
		{
			tLocalizedText = tLocalizedText.Replace("$kingdom_A$", Config.whisper_A.name);
		}
		if (Config.whisper_B != null)
		{
			tLocalizedText = tLocalizedText.Replace("$kingdom_B$", Config.whisper_B.name);
		}
		WorldTip.showNow(tLocalizedText, false, "top", 6f, "#F3961F");
	}

	// Token: 0x06000177 RID: 375 RVA: 0x0000CE06 File Offset: 0x0000B006
	public static bool selectWhisperOfWar(string pPowerID)
	{
		WorldTip.showNow("whisper_selected", true, "top", 3f, "#F3961F");
		Config.whisper_A = null;
		Config.whisper_B = null;
		return false;
	}

	// Token: 0x06000178 RID: 376 RVA: 0x0000CE2F File Offset: 0x0000B02F
	public static bool selectUnity(string pPowerID)
	{
		WorldTip.showNow("unity_selected", true, "top", 3f, "#F3961F");
		Config.unity_A = null;
		Config.unity_B = null;
		return false;
	}

	// Token: 0x06000179 RID: 377 RVA: 0x0000CE58 File Offset: 0x0000B058
	public static bool selectRelations(string pPowerID)
	{
		SelectedMetas.selected_kingdom = World.world.kingdoms.getRandom();
		return false;
	}

	// Token: 0x0600017A RID: 378 RVA: 0x0000CE70 File Offset: 0x0000B070
	public static bool whirlwind(BaseSimObject pSelf, WorldTile pTile)
	{
		World.world.applyForceOnTile(pTile, 10, 3f, false, 0, null, pSelf, null, false);
		return true;
	}

	// Token: 0x0600017B RID: 379 RVA: 0x0000CE96 File Offset: 0x0000B096
	public static void removeUnit(Actor pActor)
	{
		pActor.removeByMetamorphosis();
	}

	// Token: 0x0600017C RID: 380 RVA: 0x0000CE9E File Offset: 0x0000B09E
	public static bool breakBones(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
	{
		if (!pTarget.isAlive())
		{
			return false;
		}
		if (pTarget.isActor())
		{
			pTarget.a.addInjuryTrait("crippled");
		}
		return true;
	}

	// Token: 0x0600017D RID: 381 RVA: 0x0000CEC4 File Offset: 0x0000B0C4
	public static bool restoreMana(WorldTile pTile, Actor pSelf)
	{
		if (pSelf.isManaFull())
		{
			return false;
		}
		int tAdd = (int)((float)pSelf.getMaxMana() * 0.01f);
		pSelf.addMana(tAdd);
		return true;
	}

	// Token: 0x0600017E RID: 382 RVA: 0x0000CEF4 File Offset: 0x0000B0F4
	public static bool restoreStamina(WorldTile pTile, Actor pSelf)
	{
		if (pSelf.isStaminaFull())
		{
			return false;
		}
		int tAdd = (int)((float)pSelf.getMaxStamina() * 0.01f);
		pSelf.addStamina(tAdd);
		return true;
	}

	// Token: 0x0600017F RID: 383 RVA: 0x0000CF22 File Offset: 0x0000B122
	public static bool restoreFullStats(NanoObject pTarget, BaseAugmentationAsset pTrait)
	{
		if (pTarget.isRekt())
		{
			return false;
		}
		((Actor)pTarget).event_full_stats = true;
		return true;
	}

	// Token: 0x06000180 RID: 384 RVA: 0x0000CF3C File Offset: 0x0000B13C
	public static bool forcedKingdomAdd(NanoObject pTarget, BaseAugmentationAsset pTrait)
	{
		if (!pTarget.isAlive())
		{
			return false;
		}
		ActorTrait tTrait = (ActorTrait)pTrait;
		Actor tActor = (Actor)pTarget;
		if (tActor.asset.is_boat)
		{
			tActor.getHitFullHealth(AttackType.Explosion);
			return false;
		}
		tActor.applyForcedKingdomTrait();
		tActor.setForcedKingdom(tTrait.getForcedKingdom());
		return true;
	}

	// Token: 0x06000181 RID: 385 RVA: 0x0000CF8B File Offset: 0x0000B18B
	public static bool forcedKingdomEffectRemove(NanoObject pTarget, BaseAugmentationAsset pTrait)
	{
		if (pTarget.isRekt())
		{
			return false;
		}
		((Actor)pTarget).setDefaultKingdom();
		return true;
	}

	// Token: 0x06000182 RID: 386 RVA: 0x0000CFA3 File Offset: 0x0000B1A3
	public static bool madnessEffectLoad(NanoObject pTarget, BaseAugmentationAsset pTrait)
	{
		if (pTarget.isRekt())
		{
			return false;
		}
		((Actor)pTarget).setForcedKingdom(((ActorTrait)pTrait).getForcedKingdom());
		return true;
	}

	// Token: 0x06000183 RID: 387 RVA: 0x0000CFC8 File Offset: 0x0000B1C8
	public static bool tryToMakeBuildingAlive(Building pBuilding)
	{
		if (!pBuilding.isAlive())
		{
			return false;
		}
		if (pBuilding.isRuin())
		{
			return false;
		}
		if (pBuilding.isUnderConstruction())
		{
			return false;
		}
		if (!pBuilding.asset.can_be_living_house)
		{
			return false;
		}
		Actor actor = World.world.units.createNewUnit("living_house", pBuilding.current_tile, false, 0f, null, null, true, false, false, false);
		actor.data.set("special_sprite_id", pBuilding.asset.id);
		actor.data.set("special_sprite_index", pBuilding.animData_index);
		actor.data.created_time = pBuilding.data.created_time;
		pBuilding.removeBuildingFinal();
		actor.startColorEffect(ActorColorEffect.White);
		return true;
	}

	// Token: 0x06000184 RID: 388 RVA: 0x0000D080 File Offset: 0x0000B280
	public static bool tryToMakeFloraAlive(Building pBuilding, bool pFullyGrownOnly = true)
	{
		if (!pBuilding.isAlive())
		{
			return false;
		}
		if (pBuilding.isRuin())
		{
			return false;
		}
		if (!pBuilding.asset.can_be_living_plant)
		{
			return false;
		}
		if (pBuilding.chopped)
		{
			return false;
		}
		if (pBuilding.isUnderConstruction())
		{
			return false;
		}
		if (pFullyGrownOnly && !pBuilding.isFullyGrown())
		{
			return false;
		}
		Actor actor = World.world.units.createNewUnit("living_plants", pBuilding.current_tile, false, 0f, null, null, false, false, false, false);
		actor.data.set("special_sprite_id", pBuilding.asset.id);
		actor.data.set("special_sprite_index", pBuilding.animData_index);
		actor.data.created_time = pBuilding.data.created_time;
		pBuilding.removeBuildingFinal();
		actor.startColorEffect(ActorColorEffect.White);
		return true;
	}

	// Token: 0x06000185 RID: 389 RVA: 0x0000D150 File Offset: 0x0000B350
	public static void growRandomVegetation(WorldTile pTile, BiomeAsset pBiomeAsset)
	{
		switch (Randy.randomInt(0, 3))
		{
		case 0:
			if (pBiomeAsset.grow_type_selector_trees != null)
			{
				BuildingActions.tryGrowVegetationRandom(pTile, VegetationType.Trees, false, true, true);
				return;
			}
			break;
		case 1:
			if (pBiomeAsset.grow_type_selector_plants != null)
			{
				BuildingActions.tryGrowVegetationRandom(pTile, VegetationType.Plants, false, true, true);
				return;
			}
			break;
		case 2:
			if (pBiomeAsset.grow_type_selector_bushes != null)
			{
				BuildingActions.tryGrowVegetationRandom(pTile, VegetationType.Bushes, false, true, true);
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x06000186 RID: 390 RVA: 0x0000D1B0 File Offset: 0x0000B3B0
	private static NanoObject getNanoObjectFromTile(WorldTile pTile, MetaTypeAsset pMetaTypeAsset)
	{
		if (pTile == null)
		{
			return null;
		}
		NanoObject tMetaObject = pMetaTypeAsset.tile_get_metaobject(pTile.zone, pMetaTypeAsset.getZoneOptionState()) as NanoObject;
		if (tMetaObject.isRekt())
		{
			return null;
		}
		return tMetaObject;
	}
}
