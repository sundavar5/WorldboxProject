using System;
using System.Collections.Generic;
using ai.behaviours;
using UnityEngine;

// Token: 0x0200036F RID: 879
public class UFO : BaseActorComponent
{
	// Token: 0x06002136 RID: 8502 RVA: 0x0011B600 File Offset: 0x00119800
	internal override void create(Actor pActor)
	{
		base.create(pActor);
		this.beamRnd = base.transform.Find("Beam").GetComponent<SpriteRenderer>();
		this.beamAnim = base.transform.Find("Beam").GetComponent<SpriteAnimation>();
		this.actor.position_height = this.actor.asset.default_height;
		this.actor.getSpriteAnimation().forceUpdateFrame();
		this.hideBeam();
	}

	// Token: 0x06002137 RID: 8503 RVA: 0x0011B67C File Offset: 0x0011987C
	public static bool click(BaseSimObject pTarget, WorldTile pTile = null)
	{
		Actor actor = pTarget.a;
		BehaviourTaskActor task = actor.ai.task;
		if (((task != null) ? task.id : null) == "ufo_attack")
		{
			return false;
		}
		actor.cancelAllBeh();
		actor.setTask("ufo_attack", true, false, false);
		return true;
	}

	// Token: 0x06002138 RID: 8504 RVA: 0x0011B6CC File Offset: 0x001198CC
	internal void startBeam()
	{
		this.beamAnim.stopAt(0, true);
		this.beamAnim.isOn = true;
		this.beamRnd.enabled = true;
		MusicBox.playSound(this.actor.asset.sound_attack, this.actor.current_tile, false, false);
	}

	// Token: 0x06002139 RID: 8505 RVA: 0x0011B720 File Offset: 0x00119920
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		this.beamAnim.update(pElapsed);
		if (this.beamAnim.isOn)
		{
			World.world.stack_effects.light_blobs.Add(new LightBlobData
			{
				position = new Vector2(this.actor.current_position.x, this.actor.current_position.y),
				radius = 1f
			});
		}
		if (this.actor.stats["speed"] < 50f)
		{
			BehaviourTaskActor task = this.actor.ai.task;
			if (((task != null) ? task.id : null) == "ufo_fly")
			{
				BaseStats stats = this.actor.stats;
				stats["speed"] = stats["speed"] + pElapsed * 10f;
			}
		}
		if (!World.world.isPaused() && this.actor.isAlive() && this.actor.position_height < this.actor.asset.default_height)
		{
			this.actor.position_height += this.actor.stats["speed"] * pElapsed * 0.1f;
		}
	}

	// Token: 0x0600213A RID: 8506 RVA: 0x0011B875 File Offset: 0x00119A75
	internal void hideBeam()
	{
		this.beamAnim.isOn = false;
		this.beamRnd.enabled = false;
	}

	// Token: 0x0600213B RID: 8507 RVA: 0x0011B890 File Offset: 0x00119A90
	internal static bool getHit(BaseSimObject pSelf, BaseSimObject pAttackedBy = null, WorldTile pTile = null)
	{
		Actor tActor = pSelf.a;
		UFO tUFO = tActor.getActorComponent<UFO>();
		tUFO.aggroTargets.RemoveWhere((Actor tAttacker) => tAttacker == null || !tAttacker.isAlive());
		if (pAttackedBy != null && pAttackedBy.isActor())
		{
			tUFO.aggroTargets.Add((pAttackedBy != null) ? pAttackedBy.a : null);
		}
		BehaviourTaskActor task = tActor.ai.task;
		string actorTask = (task != null) ? task.id : null;
		if (actorTask == "ufo_fly" || actorTask == "ufo_explore")
		{
			tActor.cancelAllBeh();
			if (pAttackedBy == null)
			{
				tActor.setTask("ufo_flee", true, false, false);
			}
			else
			{
				tActor.setTask("ufo_hit", true, false, false);
			}
		}
		return true;
	}

	// Token: 0x0600213C RID: 8508 RVA: 0x0011B954 File Offset: 0x00119B54
	public static bool ufoFall(BaseSimObject pTarget, WorldTile pTile, float pElapsed)
	{
		pTarget.a.updateFall();
		if (pTarget.a.position_height == 0f)
		{
			WorldTile tTile = World.world.GetTile((int)pTarget.a.current_position.x, (int)pTarget.a.current_position.y);
			if (tTile != null)
			{
				MapAction.damageWorld(tTile, 5, AssetManager.terraform.get("ufo_explosion"), pTarget);
				EffectsLibrary.spawnAtTileRandomScale("fx_explosion_ufo", tTile, 0.45f, 0.6f);
			}
			pTarget.a.dieAndDestroy(AttackType.Other);
		}
		return true;
	}

	// Token: 0x040018A1 RID: 6305
	private SpriteRenderer beamRnd;

	// Token: 0x040018A2 RID: 6306
	internal SpriteAnimation beamAnim;

	// Token: 0x040018A3 RID: 6307
	internal HashSet<Actor> aggroTargets = new HashSet<Actor>();
}
