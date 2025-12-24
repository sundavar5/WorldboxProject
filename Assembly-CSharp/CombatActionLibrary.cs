using System;
using ai;
using UnityEngine;

// Token: 0x020000D2 RID: 210
public class CombatActionLibrary : AssetLibrary<CombatActionAsset>
{
	// Token: 0x06000641 RID: 1601 RVA: 0x0005EC0C File Offset: 0x0005CE0C
	public override void init()
	{
		base.init();
		CombatActionLibrary.combat_attack_melee = this.add(new CombatActionAsset
		{
			id = "combat_attack_melee",
			play_unit_attack_sounds = true,
			rate = 6,
			action = new CombatAction(this.attackMeleeAction),
			basic = true
		});
		CombatActionLibrary.combat_attack_range = this.add(new CombatActionAsset
		{
			id = "combat_attack_range",
			play_unit_attack_sounds = true,
			rate = 6,
			action = new CombatAction(this.attackRangeAction),
			basic = true
		});
		CombatActionLibrary.combat_cast_spell = this.add(new CombatActionAsset
		{
			id = "combat_cast_spell",
			play_unit_attack_sounds = true,
			cost_stamina = 5,
			is_spell_use = true,
			rate = 3,
			action = new CombatAction(CombatActionLibrary.tryToCastSpell)
		});
		CombatActionLibrary.combat_action_deflect = this.add(new CombatActionAsset
		{
			id = "combat_deflect_projectile",
			cost_stamina = 5,
			chance = 0.2f,
			pools = new CombatActionPool[1],
			action_actor = new CombatActionActor(this.doDeflect)
		});
		this.add(new CombatActionAsset
		{
			id = "combat_dodge",
			chance = 0.2f,
			cost_stamina = 5,
			action_actor = new CombatActionActor(this.doDodgeAction),
			pools = AssetLibrary<CombatActionAsset>.a<CombatActionPool>(new CombatActionPool[]
			{
				CombatActionPool.BEFORE_HIT
			})
		});
		this.add(new CombatActionAsset
		{
			id = "combat_block",
			chance = 0.2f,
			cost_stamina = 5,
			cooldown = 0.5f,
			action_actor = new CombatActionActor(this.doBlockAction),
			pools = AssetLibrary<CombatActionAsset>.a<CombatActionPool>(new CombatActionPool[]
			{
				CombatActionPool.BEFORE_HIT_BLOCK
			})
		});
		this.add(new CombatActionAsset
		{
			id = "combat_random_jump",
			cost_stamina = 5,
			cooldown = 2f
		});
		CombatActionLibrary.combat_action_dash = this.add(new CombatActionAsset
		{
			id = "combat_dash",
			cost_stamina = 10,
			chance = 0.2f,
			cooldown = 2f,
			action_actor_target_position = new CombatActionActorTargetPosition(this.doDashAction),
			pools = AssetLibrary<CombatActionAsset>.a<CombatActionPool>(new CombatActionPool[]
			{
				CombatActionPool.BEFORE_ATTACK_MELEE
			})
		});
		CombatActionAsset combatActionAsset = new CombatActionAsset();
		combatActionAsset.id = "combat_backstep";
		combatActionAsset.cost_stamina = 10;
		combatActionAsset.chance = 0.2f;
		combatActionAsset.cooldown = 1f;
		combatActionAsset.can_do_action = delegate(Actor pSelf, BaseSimObject pAttackTarget)
		{
			if (pSelf.current_tile.Type.block)
			{
				return false;
			}
			float num = Toolbox.SquaredDistVec2Float(pSelf.current_position, pAttackTarget.current_position);
			float tMinRange = pSelf.getAttackRangeSquared() * 0.5f;
			return num < tMinRange;
		};
		combatActionAsset.action_actor_target_position = new CombatActionActorTargetPosition(this.doBackstepAction);
		combatActionAsset.pools = AssetLibrary<CombatActionAsset>.a<CombatActionPool>(new CombatActionPool[]
		{
			CombatActionPool.BEFORE_ATTACK_RANGE
		});
		CombatActionLibrary.combat_action_backstep = this.add(combatActionAsset);
		combatActionAsset = new CombatActionAsset();
		combatActionAsset.id = "combat_throw_bomb";
		combatActionAsset.cost_stamina = 5;
		combatActionAsset.chance = 0.2f;
		combatActionAsset.cooldown = 8f;
		combatActionAsset.action_actor_target_position = new CombatActionActorTargetPosition(this.doThrowBombAction);
		combatActionAsset.can_do_action = delegate(Actor pSelf, BaseSimObject pAttackTarget)
		{
			float tDist = Toolbox.SquaredDistVec2Float(pSelf.current_position, pAttackTarget.current_position);
			return tDist > 36f && tDist < 2500f;
		};
		combatActionAsset.pools = AssetLibrary<CombatActionAsset>.a<CombatActionPool>(new CombatActionPool[]
		{
			CombatActionPool.BEFORE_ATTACK_MELEE,
			CombatActionPool.BEFORE_ATTACK_RANGE
		});
		this.add(combatActionAsset);
		combatActionAsset = new CombatActionAsset();
		combatActionAsset.id = "combat_throw_torch";
		combatActionAsset.cost_stamina = 30;
		combatActionAsset.chance = 0.2f;
		combatActionAsset.cooldown = 8f;
		combatActionAsset.action_actor_target_position = new CombatActionActorTargetPosition(this.doThrowTorchAction);
		combatActionAsset.can_do_action = delegate(Actor pSelf, BaseSimObject pAttackTarget)
		{
			float tDist = Toolbox.SquaredDistVec2Float(pSelf.current_position, pAttackTarget.current_position);
			return tDist > 36f && tDist < 2500f;
		};
		combatActionAsset.pools = AssetLibrary<CombatActionAsset>.a<CombatActionPool>(new CombatActionPool[]
		{
			CombatActionPool.BEFORE_ATTACK_MELEE,
			CombatActionPool.BEFORE_ATTACK_RANGE
		});
		this.add(combatActionAsset);
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x0005F007 File Offset: 0x0005D207
	private bool doThrowBombAction(Actor pSelf, Vector2 pTarget, WorldTile pTile = null)
	{
		ActionLibrary.throwBombAtTile(pSelf, pTile);
		pSelf.punchTargetAnimation(pTarget, true, false, 45f);
		return true;
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x0005F024 File Offset: 0x0005D224
	private bool doThrowTorchAction(Actor pSelf, Vector2 pTarget, WorldTile pTile = null)
	{
		ActionLibrary.throwTorchAtTile(pSelf, pTile);
		pSelf.punchTargetAnimation(pTarget, true, false, 45f);
		return true;
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x0005F044 File Offset: 0x0005D244
	private bool doBackstepAction(Actor pActor, Vector2 pTarget, WorldTile pTile = null)
	{
		float tDodgePowerSide = 5f;
		float tDodgePowerHeight = 1.2f;
		Vector2 tStart = pActor.current_position;
		pActor.punchTargetAnimation(pTarget, false, false, -20f);
		pActor.calculateForce(tStart.x, tStart.y, pTarget.x, pTarget.y, tDodgePowerSide, tDodgePowerHeight, false);
		Vector2 tEffectPos = pActor.current_position;
		tEffectPos.y += pActor.getHeight();
		BaseEffect tEffect = EffectsLibrary.spawnAt("fx_dodge", tEffectPos, pActor.actor_scale);
		if (tEffect != null)
		{
			tEffect.transform.rotation = Toolbox.getEulerAngle(tStart, pTarget);
		}
		return true;
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x0005F0E4 File Offset: 0x0005D2E4
	private bool doDashAction(Actor pActor, Vector2 pTarget, WorldTile pTile = null)
	{
		float tDodgePowerSide = 5f;
		float tDodgePowerHeight = 1.2f;
		Vector2 tStart = pActor.current_position;
		pActor.punchTargetAnimation(pTarget, true, false, 50f);
		pActor.addStatusEffect("dash", 0f, false);
		pActor.calculateForce(pTarget.x, pTarget.y, tStart.x, tStart.y, tDodgePowerSide, tDodgePowerHeight, false);
		Vector2 tEffectPos = pActor.current_position;
		tEffectPos.y += pActor.getHeight();
		BaseEffect tEffect = EffectsLibrary.spawnAt("fx_dodge", tEffectPos, pActor.actor_scale);
		if (tEffect != null)
		{
			tEffect.transform.rotation = Toolbox.getEulerAngle(tStart, pTarget);
		}
		return true;
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x0005F194 File Offset: 0x0005D394
	private bool doBlockAction(Actor pActor, AttackData pData, float pTargetX = 0f, float pTargetY = 0f)
	{
		ActorTool.applyForceToUnit(pData, pActor, 0.1f, false);
		if (!pActor.is_visible)
		{
			return true;
		}
		Vector2 tCurPosition = pActor.current_position;
		Vector2 tHitPosition = pData.hit_position;
		pActor.punchTargetAnimation(tHitPosition, false, false, -40f);
		BaseEffect tEffect = EffectsLibrary.spawnAt("fx_block", tHitPosition, pActor.a.actor_scale);
		if (tEffect == null)
		{
			return true;
		}
		tEffect.transform.rotation = Toolbox.getEulerAngle(tCurPosition.x, tCurPosition.y, tHitPosition.x, tHitPosition.y);
		return true;
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x0005F22C File Offset: 0x0005D42C
	private bool doDeflect(Actor pActor, AttackData pData, float pTargetX = 0f, float pTargetY = 0f)
	{
		Vector2 tOldStartPos = pData.initiator_position;
		pActor.spawnSlashPunch(tOldStartPos);
		pActor.stopMovement();
		pActor.punchTargetAnimation(tOldStartPos, true, pActor.hasRangeAttack(), 40f);
		pActor.startAttackCooldown();
		return true;
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x0005F274 File Offset: 0x0005D474
	private bool doDodgeAction(Actor pActor, AttackData pData, float pTargetX = 0f, float pTargetY = 0f)
	{
		float tDodgePowerSide = 3f;
		float tDodgePowerHeight = 1.5f;
		Vector2 tStart = pActor.cur_transform_position;
		Vector2 tFrom = pData.initiator_position;
		Vector2 tAB = tStart - tFrom;
		Vector2 tTarget;
		if (Randy.randomBool())
		{
			tTarget = tStart + Toolbox.rotateVector(tAB, 90f) * tDodgePowerSide;
		}
		else
		{
			tTarget = tStart + Toolbox.rotateVector(tAB, -90f) * tDodgePowerSide;
		}
		pActor.calculateForce(tStart.x, tStart.y, tTarget.x, tTarget.y, tDodgePowerSide, tDodgePowerHeight, false);
		pActor.addStatusEffect("dodge", 0f, false);
		pActor.punchTargetAnimation(tStart, false, false, -60f);
		Vector2 tEffectPos = pActor.current_position;
		tEffectPos.y += pActor.getHeight();
		BaseEffect tEffect = EffectsLibrary.spawnAt("fx_dodge", tEffectPos, pActor.actor_scale);
		if (tEffect != null)
		{
			tEffect.transform.rotation = Toolbox.getEulerAngle(tStart, tTarget);
		}
		return true;
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x0005F380 File Offset: 0x0005D580
	public bool attackRangeAction(AttackData pData)
	{
		Actor tSelf = pData.initiator.a;
		BaseSimObject tAttackTarget = pData.target;
		string tProjectileID = pData.projectile_id;
		float actor_scale = tSelf.actor_scale;
		float tScaleMod = tSelf.getScaleMod();
		float tSizeThis = tSelf.stats["size"];
		int tProjectiles = (int)tSelf.stats["projectiles"];
		Vector2 tAttackPosition;
		if (tAttackTarget == null)
		{
			tAttackPosition = pData.hit_position;
		}
		else
		{
			tAttackPosition = this.getAttackTargetPosition(pData);
			tAttackPosition.y += 0.2f * tScaleMod;
		}
		float tAccuracy = tSelf.stats["accuracy"];
		float tDistanceAccuracyMod = Toolbox.DistVec2Float(tSelf.current_position, tAttackPosition) / tAccuracy * 0.25f;
		tDistanceAccuracyMod = Randy.randomFloat(0f, tDistanceAccuracyMod);
		tDistanceAccuracyMod = Mathf.Clamp(tDistanceAccuracyMod, 0f, 2f);
		float tStartHeight = 0.6f * tScaleMod;
		float tTargetHeight = 0f;
		float tAngle = 0f;
		for (int i = 0; i < tProjectiles; i++)
		{
			Vector2 tProjectileAttackVector = new Vector2(tAttackPosition.x, tAttackPosition.y);
			if (tAccuracy < 10f)
			{
				Vector2 tInnacuracyVector = this.getInnacuracyVector(tAccuracy);
				tInnacuracyVector *= tDistanceAccuracyMod;
				tProjectileAttackVector += tInnacuracyVector;
			}
			Vector3 tStartProjectile = Toolbox.getNewPoint(tSelf.current_position.x, tSelf.current_position.y, tProjectileAttackVector.x, tProjectileAttackVector.y, tSizeThis * tScaleMod, true);
			tStartProjectile.y += tSelf.getHeight();
			if (tAttackTarget != null && tAttackTarget.isInAir())
			{
				tTargetHeight = tAttackTarget.getHeight();
			}
			tAngle = World.world.projectiles.spawn(tSelf, tAttackTarget, tProjectileID, tStartProjectile, tProjectileAttackVector, tTargetHeight, tStartHeight, pData.kill_action, pData.kingdom).getLaunchAngle();
		}
		tSelf.spawnSlash(tAttackPosition, null, 2f, tTargetHeight, 0f, new float?(tAngle));
		return true;
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x0005F564 File Offset: 0x0005D764
	public Vector2 getInnacuracyVector(float pAccuracyStat)
	{
		float tInaccuracy = 1f * (10f - pAccuracyStat) / 10f;
		float tInaccuracyAngle = (float)((double)(Randy.random() * 2f) * 3.141592653589793);
		return new Vector2(tInaccuracy * (float)Math.Cos((double)tInaccuracyAngle), tInaccuracy * (float)Math.Sin((double)tInaccuracyAngle));
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x0005F5B8 File Offset: 0x0005D7B8
	public static bool tryToCastSpell(AttackData pData)
	{
		Actor tHimself = pData.initiator.a;
		BaseSimObject tTarget = pData.target;
		SpellAsset tSpellAsset = tHimself.getRandomSpell();
		if (!tHimself.hasEnoughMana(tSpellAsset.cost_mana))
		{
			return false;
		}
		if (!Randy.randomChance(tSpellAsset.chance + tSpellAsset.chance * tHimself.stats["skill_spell"]))
		{
			return false;
		}
		if (tSpellAsset.cast_target == CastTarget.Himself)
		{
			tTarget = tHimself;
		}
		if (tSpellAsset.cast_entity == CastEntity.BuildingsOnly)
		{
			if (tTarget.isActor())
			{
				return false;
			}
		}
		else if (tSpellAsset.cast_entity == CastEntity.UnitsOnly && tTarget.isBuilding())
		{
			return false;
		}
		if (tSpellAsset.health_ratio > 0f)
		{
			float tCurrentHealth = tHimself.getHealthRatio();
			if (tSpellAsset.health_ratio <= tCurrentHealth)
			{
				return false;
			}
		}
		if (tSpellAsset.min_distance > 0f && (float)Toolbox.SquaredDistTile(tHimself.current_tile, tTarget.current_tile) < tSpellAsset.min_distance * tSpellAsset.min_distance)
		{
			return false;
		}
		bool tWasCast = false;
		if (tSpellAsset.action != null)
		{
			tWasCast = tSpellAsset.action.RunAnyTrue(tHimself, tTarget, tTarget.current_tile);
		}
		if (tWasCast)
		{
			tHimself.doCastAnimation();
			tHimself.addStatusEffect("recovery_spell", 0f, true);
		}
		return tWasCast;
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x0005F6D4 File Offset: 0x0005D8D4
	public bool attackMeleeAction(AttackData pData)
	{
		ref AttackDataResult ptr = MapBox.newAttack(pData);
		if (pData.initiator.a.is_visible && EffectsLibrary.canShowSlashEffect())
		{
			this.showMeleeSlashAttack(pData);
		}
		Action kill_action = pData.kill_action;
		if (kill_action != null)
		{
			kill_action();
		}
		return ptr.state == ApplyAttackState.Hit;
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x0005F720 File Offset: 0x0005D920
	public void showMeleeSlashAttack(AttackData pData)
	{
		pData.initiator.a.spawnSlash(pData.hit_position, null, 2f, 0f, 0f, null);
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x0005F764 File Offset: 0x0005D964
	public Vector2 getAttackTargetPosition(AttackData pData)
	{
		BaseSimObject tTarget = pData.target;
		Vector2 tResultVector = new Vector2(pData.hit_position.x, pData.hit_position.y);
		if (tTarget == null)
		{
			return tResultVector;
		}
		float tTargetSize = tTarget.stats["size"];
		if (tTarget.isActor() && tTarget.a.is_moving && tTarget.isFlying())
		{
			tResultVector = Vector2.MoveTowards(tResultVector, tTarget.a.next_step_position, tTargetSize * 3f);
		}
		return tResultVector;
	}

	// Token: 0x04000719 RID: 1817
	public static CombatActionAsset combat_attack_melee;

	// Token: 0x0400071A RID: 1818
	public static CombatActionAsset combat_attack_range;

	// Token: 0x0400071B RID: 1819
	public static CombatActionAsset combat_cast_spell;

	// Token: 0x0400071C RID: 1820
	public static CombatActionAsset combat_action_deflect;

	// Token: 0x0400071D RID: 1821
	public static CombatActionAsset combat_action_dash;

	// Token: 0x0400071E RID: 1822
	public static CombatActionAsset combat_action_backstep;
}
