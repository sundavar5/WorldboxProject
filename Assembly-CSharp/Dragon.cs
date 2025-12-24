using System;
using System.Collections.Generic;
using ai.behaviours;

// Token: 0x02000366 RID: 870
public class Dragon : BaseActorComponent
{
	// Token: 0x060020FD RID: 8445 RVA: 0x00119998 File Offset: 0x00117B98
	internal override void create(Actor pActor)
	{
		base.create(pActor);
		this.spriteAnimation = base.GetComponent<SpriteAnimation>();
		if (this.actor.asset.id == "zombie_dragon")
		{
			this.dragonAsset = PrefabLibrary.instance.zombieDragonAsset;
		}
		else
		{
			this.dragonAsset = PrefabLibrary.instance.dragonAsset;
		}
		this.actor.setFlying(true);
		this.setFrames(DragonState.Fly, true);
	}

	// Token: 0x060020FE RID: 8446 RVA: 0x00119A0C File Offset: 0x00117C0C
	private void playSound(DragonState pState)
	{
		DragonState dragonState = this.state;
		if (dragonState == DragonState.LandAttack)
		{
			MusicBox.playSound("event:/SFX/UNITS/dragon/fire_breath", base.transform.localPosition.x, base.transform.localPosition.y, false, false);
			return;
		}
		if (dragonState != DragonState.Slide)
		{
			return;
		}
		MusicBox.playSound("event:/SFX/UNITS/dragon/swoop", base.transform.localPosition.x, base.transform.localPosition.y, false, false);
	}

	// Token: 0x060020FF RID: 8447 RVA: 0x00119A82 File Offset: 0x00117C82
	internal static bool shouldFly(Actor pActor, WorldTile pTile = null)
	{
		if (pTile == null)
		{
			pTile = pActor.current_tile;
		}
		return !Dragon.canLand(pActor, pTile);
	}

	// Token: 0x06002100 RID: 8448 RVA: 0x00119A99 File Offset: 0x00117C99
	internal static bool canLand(Actor pActor, WorldTile pTile = null)
	{
		if (pTile == null)
		{
			pTile = pActor.current_tile;
		}
		return pTile.Type.ground || (pTile.Type.lava && !pActor.asset.die_in_lava);
	}

	// Token: 0x06002101 RID: 8449 RVA: 0x00119AD4 File Offset: 0x00117CD4
	internal void attackTile(WorldTile pTile)
	{
		if (pTile == null)
		{
			return;
		}
		bool tIsZombie = this.actor.hasTrait("zombie");
		if (tIsZombie)
		{
			DropsLibrary.action_acid(pTile, null);
			if (pTile.hasUnits() || Randy.randomBool())
			{
				World.world.drop_manager.spawnParabolicDrop(pTile, "acid", 0f, 0.1f, 3.5f, 0.5f, 4f, Randy.randomFloat(0.025f, 0.2f));
			}
		}
		else
		{
			pTile.startFire(true);
			if (pTile.hasBuilding())
			{
				pTile.building.getHit(10f, true, AttackType.Other, null, true, false, true);
			}
			if (pTile.hasUnits() || Randy.randomBool())
			{
				World.world.drop_manager.spawnParabolicDrop(pTile, "fire", 0f, 0.1f, 3.5f, 0.5f, 4f, Randy.randomFloat(0.025f, 0.2f));
			}
		}
		if (!pTile.hasUnits())
		{
			return;
		}
		MapAction.damageWorld(pTile, 2, AssetManager.terraform.get(tIsZombie ? "zombie_dragon_attack" : "dragon_attack"), this.actor);
	}

	// Token: 0x06002102 RID: 8450 RVA: 0x00119BF4 File Offset: 0x00117DF4
	internal bool hasTargetsForSlide()
	{
		if (WorldLawLibrary.world_law_peaceful_monsters.isEnabled())
		{
			return false;
		}
		this.attackRange(this.actor.flip);
		using (HashSet<WorldTile>.Enumerator enumerator = (this.actor.flip ? this._slideAttackTilesFlip : this._slideAttackTilesNoFlip).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (Dragon.hasTarget(enumerator.Current, this.actor))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06002103 RID: 8451 RVA: 0x00119C88 File Offset: 0x00117E88
	internal bool targetWithinSlide(WorldTile pTargetTile)
	{
		if (WorldLawLibrary.world_law_peaceful_monsters.isEnabled())
		{
			return false;
		}
		this.attackRange(true);
		if (this._slideAttackTilesFlip.Contains(pTargetTile))
		{
			this.actor.setFlip(true);
			return true;
		}
		this.attackRange(false);
		if (this._slideAttackTilesNoFlip.Contains(pTargetTile))
		{
			this.actor.setFlip(false);
			return true;
		}
		return false;
	}

	// Token: 0x06002104 RID: 8452 RVA: 0x00119CEC File Offset: 0x00117EEC
	internal static Kingdom getIgnoredKingdom(Actor pActor)
	{
		if (pActor.hasTrait("zombie"))
		{
			return World.world.kingdoms_wild.get("undead");
		}
		return World.world.kingdoms_wild.get("dragons");
	}

	// Token: 0x06002105 RID: 8453 RVA: 0x00119D24 File Offset: 0x00117F24
	internal bool targetsWithinLandAttackRange()
	{
		foreach (Actor tAttackTarget in this.aggroTargets)
		{
			if (!tAttackTarget.isRekt() && this.landAttackRange(tAttackTarget.current_tile))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002106 RID: 8454 RVA: 0x00119D90 File Offset: 0x00117F90
	internal bool landAttackRange(WorldTile pTargetTile)
	{
		if (Toolbox.Dist(this.actor.current_tile.pos.x, this.actor.current_tile.pos.y, pTargetTile.pos.x, pTargetTile.pos.y) > 9f)
		{
			return false;
		}
		this.landAttackTiles(this.actor.current_tile);
		return this._landAttackTiles.Contains(pTargetTile);
	}

	// Token: 0x06002107 RID: 8455 RVA: 0x00119E18 File Offset: 0x00118018
	internal HashSet<WorldTile> landAttackTiles(WorldTile pTile)
	{
		if (this._landAttackPosCheck == pTile)
		{
			this._landAttackCache++;
			return this._landAttackTiles;
		}
		this._landAttackCache = 0;
		this._landAttackTiles.Clear();
		this._landAttackPosCheck = pTile;
		for (int yy = 0; yy < 12; yy++)
		{
			for (int xx = 0; xx < 20; xx++)
			{
				WorldTile tTile = World.world.GetTile(pTile.pos.x + xx - 10, pTile.pos.y - yy + 1);
				if (tTile != null && Toolbox.Dist(pTile.pos.x, pTile.pos.y, tTile.pos.x, tTile.pos.y) <= 9f)
				{
					this._landAttackTiles.Add(tTile);
				}
			}
		}
		return this._landAttackTiles;
	}

	// Token: 0x06002108 RID: 8456 RVA: 0x00119F0C File Offset: 0x0011810C
	internal WorldTile randomTileWithinLandAttackRange(WorldTile pTile)
	{
		Toolbox.temp_list_tiles.Clear();
		for (int yy = 9; yy > 1; yy--)
		{
			WorldTile tTile = World.world.GetTile(pTile.pos.x, pTile.pos.y + yy);
			if (tTile != null)
			{
				pTile = tTile;
				break;
			}
		}
		for (int yy2 = 0; yy2 < 12; yy2++)
		{
			for (int xx = 0; xx < 20; xx++)
			{
				WorldTile tTile2 = World.world.GetTile(pTile.pos.x + xx - 10, pTile.pos.y - yy2 + 1);
				if (tTile2 != null && Toolbox.Dist(pTile.pos.x, pTile.pos.y, tTile2.pos.x, tTile2.pos.y) <= 9f && Dragon.canLand(this.actor, tTile2))
				{
					Toolbox.temp_list_tiles.Add(tTile2);
				}
			}
		}
		if (Toolbox.temp_list_tiles.Count == 0)
		{
			return pTile;
		}
		return Toolbox.temp_list_tiles.GetRandom<WorldTile>();
	}

	// Token: 0x06002109 RID: 8457 RVA: 0x0011A03C File Offset: 0x0011823C
	internal HashSet<WorldTile> attackRange(bool flip)
	{
		if (flip)
		{
			if (this._slideAttackPosCheckFlip == this.actor.current_tile)
			{
				this._slideAttackTilesFlipCache++;
				return this._slideAttackTilesFlip;
			}
			this._slideAttackTilesFlipCache = 0;
			this._slideAttackTilesFlip.Clear();
			this._slideAttackPosCheckFlip = this.actor.current_tile;
		}
		else
		{
			if (this._slideAttackPosCheckNoFlip == this.actor.current_tile)
			{
				this._slideAttackTilesNoFlipCache++;
				return this._slideAttackTilesNoFlip;
			}
			this._slideAttackTilesNoFlipCache = 0;
			this._slideAttackTilesNoFlip.Clear();
			this._slideAttackPosCheckNoFlip = this.actor.current_tile;
		}
		int tXOffset;
		if (flip)
		{
			tXOffset = -25;
		}
		else
		{
			tXOffset = 20;
		}
		for (int yy = 0; yy < 4; yy++)
		{
			for (int xx = 0; xx < 35; xx++)
			{
				WorldTile tTile = World.world.GetTile(this.actor.current_tile.x + xx - 15 + tXOffset, this.actor.current_tile.y - yy + 2);
				if (tTile != null)
				{
					if (flip)
					{
						this._slideAttackTilesFlip.Add(tTile);
					}
					if (!flip)
					{
						this._slideAttackTilesNoFlip.Add(tTile);
					}
				}
			}
		}
		if (flip)
		{
			return this._slideAttackTilesFlip;
		}
		return this._slideAttackTilesNoFlip;
	}

	// Token: 0x0600210A RID: 8458 RVA: 0x0011A174 File Offset: 0x00118374
	private static bool hasTarget(WorldTile tTile, Actor pActor)
	{
		if (tTile.hasBuilding() && tTile.building.isUsable())
		{
			return true;
		}
		if (!tTile.hasUnits())
		{
			return false;
		}
		Kingdom tIgnoredKingdom = Dragon.getIgnoredKingdom(pActor2);
		bool tTargetFound = false;
		tTile.doUnits(delegate(Actor pActor)
		{
			if (pActor.position_height > 0f)
			{
				return true;
			}
			if (pActor.kingdom == tIgnoredKingdom)
			{
				return true;
			}
			tTargetFound = true;
			return false;
		});
		return tTargetFound;
	}

	// Token: 0x0600210B RID: 8459 RVA: 0x0011A1D4 File Offset: 0x001183D4
	public void setFrames(DragonState pDragonState, bool pForce = false)
	{
		if (this.state == pDragonState && !pForce)
		{
			return;
		}
		this.actor.setShowShadow(pDragonState == DragonState.Fly);
		this.state = pDragonState;
		this.playSound(this.state);
		DragonAssetContainer tContainer = this.dragonAsset.getAsset(pDragonState);
		this.spriteAnimation.setFrames(tContainer.frames);
		this.spriteAnimation.timeBetweenFrames = tContainer.speed;
		this.spriteAnimation.resetAnim(0);
		this.spriteAnimation.looped = true;
	}

	// Token: 0x0600210C RID: 8460 RVA: 0x0011A257 File Offset: 0x00118457
	internal static bool clickToWakeup(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget.a.isTask("dragon_sleep"))
		{
			pTarget.a.cancelAllBeh();
			pTarget.a.setTask("dragon_wakeup", true, false, false);
			return true;
		}
		return false;
	}

	// Token: 0x0600210D RID: 8461 RVA: 0x0011A28C File Offset: 0x0011848C
	internal static bool canFlip(BaseSimObject pTarget = null, WorldTile pTile = null)
	{
		switch (pTarget.a.getActorComponent<Dragon>().state)
		{
		case DragonState.Fly:
		case DragonState.Idle:
			return true;
		case DragonState.LandAttack:
		case DragonState.Death:
		case DragonState.SleepStart:
		case DragonState.SleepLoop:
		case DragonState.SleepUp:
		case DragonState.Landing:
		case DragonState.Slide:
		case DragonState.Up:
			return false;
		default:
			return true;
		}
	}

	// Token: 0x0600210E RID: 8462 RVA: 0x0011A2E0 File Offset: 0x001184E0
	internal static bool getHit(BaseSimObject pSelf, BaseSimObject pAttackedBy = null, WorldTile pTile = null)
	{
		Actor tActor = pSelf.a;
		Dragon tDragon = tActor.getActorComponent<Dragon>();
		if (WorldLawLibrary.world_law_peaceful_monsters.isEnabled())
		{
			return true;
		}
		bool tNewAttacker = false;
		tDragon.aggroTargets.RemoveWhere((Actor tAttacker) => tAttacker.isRekt());
		if (pAttackedBy != null)
		{
			if (pAttackedBy.isActor() && tDragon.aggroTargets.Add(pAttackedBy.a))
			{
				tNewAttacker = (tDragon.aggroTargets.Count == 1);
			}
			if (pAttackedBy.hasCity())
			{
				tActor.data.set("cityToAttack", pAttackedBy.getCity().data.id);
				tActor.data.set("attacksForCity", Randy.randomInt(4, 12));
			}
		}
		BehaviourTaskActor task = tActor.ai.task;
		string a = (task != null) ? task.id : null;
		if (!(a == "dragon_sleep"))
		{
			if (!(a == "dragon_idle"))
			{
				if (!(a == "dragon_fly"))
				{
					if (a == "dragon_wakeup" || a == "dragon_up")
					{
						tActor.data.set("justGotHit", true);
					}
				}
				else if (tNewAttacker)
				{
					tActor.cancelAllBeh();
					if (!pAttackedBy.isFlying() && tDragon.landAttackRange(pAttackedBy.current_tile) && Dragon.canLand(tActor, null) && tDragon.lastLanded != tActor.current_tile)
					{
						tActor.setTask("dragon_land", true, false, false);
					}
					else if (tDragon.targetWithinSlide(pAttackedBy.current_tile))
					{
						tActor.setTask("dragon_slide", true, false, false);
					}
					else
					{
						tActor.setTask("dragon_fly", true, false, false);
					}
				}
			}
			else
			{
				int landAttacks;
				tActor.data.get("landAttacks", out landAttacks, 0);
				if (landAttacks > 2 || Dragon.shouldFly(tActor, null) || pAttackedBy == null)
				{
					tActor.data.set("justGotHit", true);
					tActor.cancelAllBeh();
					tActor.setTask("dragon_up", true, false, false);
				}
				else if (!pAttackedBy.isFlying() && tDragon.landAttackRange(pAttackedBy.current_tile) && Dragon.canLand(tActor, null))
				{
					tActor.cancelAllBeh();
					tActor.setTask("dragon_land_attack", true, false, false);
				}
			}
		}
		else
		{
			tActor.data.set("justGotHit", true);
			tActor.cancelAllBeh();
			tActor.setTask("dragon_wakeup", true, false, false);
		}
		return true;
	}

	// Token: 0x0600210F RID: 8463 RVA: 0x0011A558 File Offset: 0x00118758
	internal static bool dragonFall(BaseSimObject pTarget, WorldTile pTile, float pElapsed)
	{
		Dragon tDragon = pTarget.a.getActorComponent<Dragon>();
		SpriteAnimation tSpriteAnimation = tDragon.spriteAnimation;
		tSpriteAnimation.looped = false;
		tSpriteAnimation.ignorePause = true;
		if (!pTarget.isFlying())
		{
			tDragon.setFrames(DragonState.Death, false);
			if (tSpriteAnimation.currentFrameIndex == tSpriteAnimation.frames.Length - 1)
			{
				pTarget.a.updateDeadBlackAnimation(World.world.elapsed);
			}
			return true;
		}
		tDragon.setFrames(DragonState.Landing, false);
		if (tSpriteAnimation.currentFrameIndex < tSpriteAnimation.frames.Length - 1)
		{
			return true;
		}
		pTarget.a.setFlying(false);
		return true;
	}

	// Token: 0x06002110 RID: 8464 RVA: 0x0011A5E8 File Offset: 0x001187E8
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (this.actor.isRekt())
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		this.checkLiquid();
	}

	// Token: 0x06002111 RID: 8465 RVA: 0x0011A614 File Offset: 0x00118814
	internal void checkLiquid()
	{
		if (this.actor.isFlying())
		{
			return;
		}
		if (this.actor.is_moving)
		{
			return;
		}
		if (this.actor.isEgg())
		{
			return;
		}
		if (!this.actor.current_tile.Type.liquid)
		{
			return;
		}
		if (this.actor.hasTask())
		{
			if (this.actor.isTask("dragon_up"))
			{
				return;
			}
			if (this.actor.isTask("dragon_wakeup"))
			{
				return;
			}
			if (this.actor.isTask("dragon_sleep"))
			{
				this.actor.cancelAllBeh();
				this.actor.setTask("dragon_wakeup", true, false, false);
				return;
			}
		}
		this.actor.cancelAllBeh();
		this.actor.setTask("dragon_up", true, false, false);
	}

	// Token: 0x06002112 RID: 8466 RVA: 0x0011A6E5 File Offset: 0x001188E5
	public HashSet<WorldTile> getLandAttackTiles()
	{
		return this._landAttackTiles;
	}

	// Token: 0x04001867 RID: 6247
	private DragonAsset dragonAsset;

	// Token: 0x04001868 RID: 6248
	private DragonState state;

	// Token: 0x04001869 RID: 6249
	internal float idle_time = -1f;

	// Token: 0x0400186A RID: 6250
	internal float sleep_time = -1f;

	// Token: 0x0400186B RID: 6251
	internal SpriteAnimation spriteAnimation;

	// Token: 0x0400186C RID: 6252
	internal HashSet<Actor> aggroTargets = new HashSet<Actor>();

	// Token: 0x0400186D RID: 6253
	internal WorldTile lastLanded;

	// Token: 0x0400186E RID: 6254
	private HashSet<WorldTile> _landAttackTiles = new HashSet<WorldTile>();

	// Token: 0x0400186F RID: 6255
	private WorldTile _landAttackPosCheck;

	// Token: 0x04001870 RID: 6256
	internal int _landAttackCache;

	// Token: 0x04001871 RID: 6257
	internal HashSet<WorldTile> _slideAttackTilesFlip = new HashSet<WorldTile>();

	// Token: 0x04001872 RID: 6258
	internal HashSet<WorldTile> _slideAttackTilesNoFlip = new HashSet<WorldTile>();

	// Token: 0x04001873 RID: 6259
	private WorldTile _slideAttackPosCheckFlip;

	// Token: 0x04001874 RID: 6260
	private WorldTile _slideAttackPosCheckNoFlip;

	// Token: 0x04001875 RID: 6261
	internal int _slideAttackTilesFlipCache;

	// Token: 0x04001876 RID: 6262
	internal int _slideAttackTilesNoFlipCache;
}
