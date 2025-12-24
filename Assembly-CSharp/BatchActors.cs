using System;
using System.Collections.Generic;
using Unity.Mathematics;

// Token: 0x020002F0 RID: 752
public class BatchActors : Batch<Actor>
{
	// Token: 0x06001C38 RID: 7224 RVA: 0x00100C6C File Offset: 0x000FEE6C
	protected override void createJobs()
	{
		base.addJob(null, new JobUpdater(this.prepare), JobType.Parallel, "prepare", 0);
		base.createJob(out this.c_main, new JobUpdater(this.updateParallelChecks), JobType.Parallel, "update_timers", 0);
		base.addJob(this.c_main, new JobUpdater(this.updateVisibility), JobType.Parallel, "update_visibility", 0);
		base.createJob(out this.c_stats_dirty, new JobUpdater(this.updateStats), JobType.Parallel, "update_stats", 0);
		base.createJob(out this.c_events_become_adult, new JobUpdater(this.updateEventsBecomeAdult), JobType.Post, "update_events_become_adult", 0);
		base.createJob(out this.c_events_hatched, new JobUpdater(this.updateEventsEggHatched), JobType.Post, "update_events_hatched", 0);
		base.createJob(out this.c_action_landed, new JobUpdater(this.updateActionLanded), JobType.Post, "update_action_landed", 0);
		base.addJob(this.c_main, new JobUpdater(this.updateNutritionDecay), JobType.Post, "update_hunger", 0);
		base.addJob(this.c_main, new JobUpdater(this.u1_checkInside), JobType.Post, "u1_checkInside", 0);
		base.createJob(out this.c_update_children, new JobUpdater(this.u2_updateChildren), JobType.Post, "u2_updateChildren", 0);
		base.createJob(out this.c_sprite_animations, new JobUpdater(this.u3_spriteAnimation), JobType.Post, "u3_spriteAnimation", 0);
		base.addJob(this.c_main, new JobUpdater(this.u4_deadCheck), JobType.Post, "u4_deadCheck", 0);
		base.createJob(out this.c_main_tile_action, new JobUpdater(this.u5_curTileAction), JobType.Post, "u5_curTileAction", 0);
		base.addJob(this.c_main, new JobUpdater(this.u6_checkFrozen), JobType.Post, "u6_checkFrozen", 0);
		base.createJob(out this.c_augmentation_effects, new JobUpdater(this.u7_checkAugmentationEffects), JobType.Post, "u7_checkAugmentationEffects", 20);
		base.addJob(this.c_main, new JobUpdater(this.u8_checkUpdateTimers), JobType.Post, "u8_checkUpdateTimers", 0);
		base.addJob(this.c_main, new JobUpdater(this.b1_checkUnderForce), JobType.Post, "b1_checkUnderForce", 0);
		base.createJob(out this.c_check_attack_target, new JobUpdater(this.b2_checkCurrentEnemyTarget), JobType.Post, "b2_checkCurrentEnemyTarget", 0);
		base.addJob(this.c_main, new JobUpdater(this.b3_findEnemyTarget), JobType.Post, "b3_findEnemyTarget", 5);
		base.addJob(this.c_main, new JobUpdater(this.b4_checkTaskVerifier), JobType.Post, "b4_checkTaskVerifier", 0);
		base.addJob(this.c_main, new JobUpdater(this.b5_checkPathMovement), JobType.Post, "b5_checkPathMovement", 0);
		base.createJob(out this.c_make_decision, new JobUpdater(this.b6_0_updateDecision), JobType.Post, "b6_0_update_decision", 0);
		base.addJob(this.c_main, new JobUpdater(this.b55_updateNaturalDeaths), JobType.Post, "b55_update_natural_death", 20);
		base.addJob(this.c_main, new JobUpdater(this.b6_updateAI), JobType.Post, "b6_update_ai", 0);
		base.createJob(out this.c_update_movement, new JobUpdater(this.u10_checkSmoothMovement), JobType.Post, "u10_checkSmoothMovement", 0);
		base.createJob(out this.c_shake, new JobUpdater(this.updateShake), JobType.Post, "update_shake", 0);
		base.createJob(out this.c_hovering, new JobUpdater(this.updateHovering), JobType.Post, "update_hovering", 0);
		base.createJob(out this.c_pollinating, new JobUpdater(this.updatePollinating), JobType.Post, "update_pollinating", 0);
		base.createJob(out this.c_check_deaths, new JobUpdater(this.updateDeathCheck), JobType.Post, "update_death", 0);
		this.main = this.c_main;
		this.clearParallelResults = (JobUpdater)Delegate.Combine(this.clearParallelResults, new JobUpdater(this.clearParallelSprites));
	}

	// Token: 0x06001C39 RID: 7225 RVA: 0x00101028 File Offset: 0x000FF228
	private void updateParallelChecks()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].updateParallelChecks(this._elapsed);
		}
	}

	// Token: 0x06001C3A RID: 7226 RVA: 0x0010106C File Offset: 0x000FF26C
	private void updateVisibility()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		bool tRenderActors = MapBox.isRenderGameplay();
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			Actor tActor = tArray[i];
			if (tActor.asset.has_sprite_renderer)
			{
				if (tActor.isInMagnet() || tActor.isInsideSomething())
				{
					tActor.is_visible = false;
				}
				else if (tRenderActors)
				{
					if (tActor.current_tile.zone.visible)
					{
						tActor.is_visible = true;
					}
					else
					{
						tActor.is_visible = false;
					}
				}
				else if (tActor.asset.visible_on_minimap)
				{
					tActor.is_visible = true;
				}
				else
				{
					tActor.is_visible = false;
				}
			}
		}
	}

	// Token: 0x06001C3B RID: 7227 RVA: 0x00101124 File Offset: 0x000FF324
	private void updateNutritionDecay()
	{
		if (World.world.timer_nutrition_decay > 0f)
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		if (!base.check(this._cur_container))
		{
			return;
		}
		bool tStarvationDamage = false;
		if (WorldLawLibrary.world_law_hunger.isEnabled())
		{
			tStarvationDamage = true;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			Actor tActor = tArray[i];
			if (tActor.needsFood() && !tActor.isEgg() && (!tActor.hasSubspecies() || !tActor.subspecies.has_trait_energy_preserver || !tActor.hasStatus("sleeping")))
			{
				tActor.updateNutritionDecay(tStarvationDamage);
			}
		}
	}

	// Token: 0x06001C3C RID: 7228 RVA: 0x001011D0 File Offset: 0x000FF3D0
	private void updateEventsBecomeAdult()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].eventBecomeAdult();
		}
	}

	// Token: 0x06001C3D RID: 7229 RVA: 0x00101210 File Offset: 0x000FF410
	private void updateEventsEggHatched()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].eventHatchFromEgg();
		}
	}

	// Token: 0x06001C3E RID: 7230 RVA: 0x00101250 File Offset: 0x000FF450
	private void updateActionLanded()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].actionLanded();
		}
	}

	// Token: 0x06001C3F RID: 7231 RVA: 0x00101290 File Offset: 0x000FF490
	private void updateStats()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].updateStats();
		}
	}

	// Token: 0x06001C40 RID: 7232 RVA: 0x001012D0 File Offset: 0x000FF4D0
	private void u1_checkInside()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].u1_checkInside(this._elapsed);
		}
	}

	// Token: 0x06001C41 RID: 7233 RVA: 0x00101314 File Offset: 0x000FF514
	private void u2_updateChildren()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].u2_updateChildren(this._elapsed);
		}
	}

	// Token: 0x06001C42 RID: 7234 RVA: 0x00101358 File Offset: 0x000FF558
	private void u3_spriteAnimation()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].u3_spriteAnimation(this._elapsed);
		}
	}

	// Token: 0x06001C43 RID: 7235 RVA: 0x0010139C File Offset: 0x000FF59C
	private void u4_deadCheck()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].u4_deadCheck(this._elapsed);
		}
	}

	// Token: 0x06001C44 RID: 7236 RVA: 0x001013E0 File Offset: 0x000FF5E0
	private void u5_curTileAction()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].u5_curTileAction();
		}
	}

	// Token: 0x06001C45 RID: 7237 RVA: 0x0010142C File Offset: 0x000FF62C
	private void u5_checkTileDeath()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].checkDieOnGroundBoat();
		}
	}

	// Token: 0x06001C46 RID: 7238 RVA: 0x0010146C File Offset: 0x000FF66C
	private void u6_checkFrozen()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].u6_checkFrozen(this._elapsed);
		}
	}

	// Token: 0x06001C47 RID: 7239 RVA: 0x001014C0 File Offset: 0x000FF6C0
	private void u7_checkAugmentationEffects()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].u7_checkAugmentationEffects();
		}
	}

	// Token: 0x06001C48 RID: 7240 RVA: 0x0010150C File Offset: 0x000FF70C
	private void u8_checkUpdateTimers()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].u8_checkUpdateTimers(this._elapsed);
		}
	}

	// Token: 0x06001C49 RID: 7241 RVA: 0x00101560 File Offset: 0x000FF760
	private void b1_checkUnderForce()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].b1_checkUnderForce(this._elapsed);
		}
	}

	// Token: 0x06001C4A RID: 7242 RVA: 0x001015B4 File Offset: 0x000FF7B4
	private void b2_checkCurrentEnemyTarget()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].b2_checkCurrentEnemyTarget(this._elapsed);
		}
	}

	// Token: 0x06001C4B RID: 7243 RVA: 0x00101608 File Offset: 0x000FF808
	private void b3_findEnemyTarget()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].b3_findEnemyTarget(this._elapsed);
		}
	}

	// Token: 0x06001C4C RID: 7244 RVA: 0x0010165C File Offset: 0x000FF85C
	private void b4_checkTaskVerifier()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].b4_checkTaskVerifier(this._elapsed);
		}
	}

	// Token: 0x06001C4D RID: 7245 RVA: 0x001016B0 File Offset: 0x000FF8B0
	private void b5_checkPathMovement()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].b5_checkPathMovement(this._elapsed);
		}
	}

	// Token: 0x06001C4E RID: 7246 RVA: 0x00101704 File Offset: 0x000FF904
	private void b6_0_updateDecision()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].b6_0_updateDecision(this._elapsed);
		}
		this._cur_container.Clear();
	}

	// Token: 0x06001C4F RID: 7247 RVA: 0x00101760 File Offset: 0x000FF960
	private void b55_updateNaturalDeaths()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].b55_updateNaturalDeaths(this._elapsed);
		}
	}

	// Token: 0x06001C50 RID: 7248 RVA: 0x001017B4 File Offset: 0x000FF9B4
	private void b6_updateAI()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].b6_updateAI(this._elapsed);
		}
	}

	// Token: 0x06001C51 RID: 7249 RVA: 0x00101808 File Offset: 0x000FFA08
	private void u10_checkSmoothMovement()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].u10_checkSmoothMovement(this._elapsed);
		}
	}

	// Token: 0x06001C52 RID: 7250 RVA: 0x0010185C File Offset: 0x000FFA5C
	private void updateShake()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].updateShake(this._elapsed);
		}
		this._cur_container.checkAddRemove();
	}

	// Token: 0x06001C53 RID: 7251 RVA: 0x001018AC File Offset: 0x000FFAAC
	private void updateHovering()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].updateHover(this._elapsed);
		}
	}

	// Token: 0x06001C54 RID: 7252 RVA: 0x00101900 File Offset: 0x000FFB00
	private void updatePollinating()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].updatePollinate(this._elapsed);
		}
	}

	// Token: 0x06001C55 RID: 7253 RVA: 0x00101954 File Offset: 0x000FFB54
	private void updateDeathCheck()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		if (World.world.isWindowOnScreen())
		{
			return;
		}
		Actor[] tArray = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArray[i].checkDeath();
		}
		this._cur_container.Clear();
	}

	// Token: 0x06001C56 RID: 7254 RVA: 0x001019AA File Offset: 0x000FFBAA
	internal override void clear()
	{
		base.clear();
		JobUpdater clearParallelResults = this.clearParallelResults;
		if (clearParallelResults == null)
		{
			return;
		}
		clearParallelResults();
	}

	// Token: 0x06001C57 RID: 7255 RVA: 0x001019C2 File Offset: 0x000FFBC2
	private void clearParallelSprites()
	{
		this.l_parallel_update_sprites.Clear();
	}

	// Token: 0x06001C58 RID: 7256 RVA: 0x001019CF File Offset: 0x000FFBCF
	internal override void add(Actor pActor)
	{
		base.add(pActor);
		pActor.batch = this;
	}

	// Token: 0x06001C59 RID: 7257 RVA: 0x001019DF File Offset: 0x000FFBDF
	internal override void remove(Actor pObject)
	{
		base.remove(pObject);
		pObject.batch = null;
	}

	// Token: 0x04001591 RID: 5521
	public ObjectContainer<Actor> c_main;

	// Token: 0x04001592 RID: 5522
	public ObjectContainer<Actor> c_check_attack_target;

	// Token: 0x04001593 RID: 5523
	public ObjectContainer<Actor> c_update_movement;

	// Token: 0x04001594 RID: 5524
	public ObjectContainer<Actor> c_main_tile_action;

	// Token: 0x04001595 RID: 5525
	public ObjectContainer<Actor> c_shake;

	// Token: 0x04001596 RID: 5526
	public ObjectContainer<Actor> c_stats_dirty;

	// Token: 0x04001597 RID: 5527
	public ObjectContainer<Actor> c_action_landed;

	// Token: 0x04001598 RID: 5528
	public ObjectContainer<Actor> c_make_decision;

	// Token: 0x04001599 RID: 5529
	public ObjectContainer<Actor> c_sprite_animations;

	// Token: 0x0400159A RID: 5530
	public ObjectContainer<Actor> c_update_children;

	// Token: 0x0400159B RID: 5531
	public ObjectContainer<Actor> c_check_enemy_target;

	// Token: 0x0400159C RID: 5532
	public ObjectContainer<Actor> c_augmentation_effects;

	// Token: 0x0400159D RID: 5533
	public ObjectContainer<Actor> c_events_become_adult;

	// Token: 0x0400159E RID: 5534
	public ObjectContainer<Actor> c_events_hatched;

	// Token: 0x0400159F RID: 5535
	public ObjectContainer<Actor> c_hovering;

	// Token: 0x040015A0 RID: 5536
	public ObjectContainer<Actor> c_pollinating;

	// Token: 0x040015A1 RID: 5537
	public ObjectContainer<Actor> c_check_deaths;

	// Token: 0x040015A2 RID: 5538
	internal List<Actor> l_parallel_update_sprites = new List<Actor>();

	// Token: 0x040015A3 RID: 5539
	public Random rnd = new Random(10U);
}
