using System;
using System.Collections.Generic;
using life.taxi;
using UnityEngine;

// Token: 0x0200020A RID: 522
public class Boat : ActorSimpleComponent
{
	// Token: 0x0600124D RID: 4685 RVA: 0x000D48D4 File Offset: 0x000D2AD4
	internal override void create(Actor pActor)
	{
		base.create(pActor);
		Actor actor = this.actor;
		actor.callbacks_on_death = (BaseActionActor)Delegate.Combine(actor.callbacks_on_death, new BaseActionActor(this.deathAction));
		Actor actor2 = this.actor;
		actor2.callbacks_on_death = (BaseActionActor)Delegate.Combine(actor2.callbacks_on_death, new BaseActionActor(this.spawnBoatExplosion));
		Actor actor3 = this.actor;
		actor3.callbacks_landed = (BaseActionActor)Delegate.Combine(actor3.callbacks_landed, new BaseActionActor(this.cancelWork));
		Actor actor4 = this.actor;
		actor4.callbacks_cancel_path_movement = (BaseActionActor)Delegate.Combine(actor4.callbacks_cancel_path_movement, new BaseActionActor(this.cancelPathfinderMovement));
	}

	// Token: 0x0600124E RID: 4686 RVA: 0x000D4984 File Offset: 0x000D2B84
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (this.actor.is_moving)
		{
			this.calculateMovementAngle();
		}
	}

	// Token: 0x0600124F RID: 4687 RVA: 0x000D49A0 File Offset: 0x000D2BA0
	public void calculateMovementAngle()
	{
		Vector2 tCurPos = this.actor.current_position;
		Vector2 tNextPos = this.actor.next_step_position;
		if (this._last_step == tNextPos)
		{
			return;
		}
		this._last_step = tNextPos;
		float tAngle = Toolbox.getAngleDegrees(tCurPos.x, tCurPos.y, tNextPos.x, tNextPos.y);
		this.last_movement_angle = (int)tAngle;
	}

	// Token: 0x06001250 RID: 4688 RVA: 0x000D4A04 File Offset: 0x000D2C04
	public bool isNearDock()
	{
		Building tBuilding = this.actor.getHomeBuilding();
		return tBuilding != null && tBuilding.component_docks.tiles_ocean.Contains(this.actor.current_tile);
	}

	// Token: 0x06001251 RID: 4689 RVA: 0x000D4A42 File Offset: 0x000D2C42
	private void cancelPathfinderMovement(Actor pActor)
	{
		this.cancelWork(pActor);
	}

	// Token: 0x06001252 RID: 4690 RVA: 0x000D4A4B File Offset: 0x000D2C4B
	internal void cancelWork(Actor pActor)
	{
		this.actor.cancelAllBeh();
		if (this.taxi_request != null)
		{
			TaxiManager.cancelRequest(this.taxi_request);
			this.taxi_request = null;
			this.taxi_target = null;
		}
	}

	// Token: 0x170000BC RID: 188
	// (get) Token: 0x06001253 RID: 4691 RVA: 0x000D4A79 File Offset: 0x000D2C79
	private string _boat_texture_id
	{
		get
		{
			return this.actor.asset.boat_texture_id;
		}
	}

	// Token: 0x06001254 RID: 4692 RVA: 0x000D4A8B File Offset: 0x000D2C8B
	public AnimationDataBoat getAnimationDataBoat()
	{
		return ActorAnimationLoader.loadAnimationBoat(this._boat_texture_id);
	}

	// Token: 0x06001255 RID: 4693 RVA: 0x000D4A98 File Offset: 0x000D2C98
	public void spawnBoatExplosion(Actor pActor)
	{
		EffectsLibrary.spawnAt("fx_boat_explosion", this.actor.current_position, this.actor.asset.base_stats["scale"]);
	}

	// Token: 0x06001256 RID: 4694 RVA: 0x000D4ACA File Offset: 0x000D2CCA
	private void deathAction(Actor pActor)
	{
		if (this.taxi_request != null)
		{
			TaxiManager.finish(this.taxi_request);
			this.taxi_request = null;
		}
		this.unloadPassengers(this.actor.current_tile, true);
	}

	// Token: 0x06001257 RID: 4695 RVA: 0x000D4AF8 File Offset: 0x000D2CF8
	internal void unloadPassengers(WorldTile pTile, bool pRandomForce = false)
	{
		foreach (Actor tActor in this._passengers)
		{
			if (tActor.isAlive())
			{
				tActor.disembarkTo(this, pTile);
				if (pRandomForce)
				{
					tActor.applyRandomForce(1.5f, 2f);
				}
			}
		}
		this._passengers.Clear();
		this.taxi_target = null;
	}

	// Token: 0x06001258 RID: 4696 RVA: 0x000D4B7C File Offset: 0x000D2D7C
	internal bool hasPassengers()
	{
		return this._passengers.Count > 0;
	}

	// Token: 0x06001259 RID: 4697 RVA: 0x000D4B8C File Offset: 0x000D2D8C
	internal int countPassengers()
	{
		return this._passengers.Count;
	}

	// Token: 0x0600125A RID: 4698 RVA: 0x000D4B99 File Offset: 0x000D2D99
	internal bool hasPassenger(Actor pActor)
	{
		return this._passengers.Contains(pActor);
	}

	// Token: 0x0600125B RID: 4699 RVA: 0x000D4BA7 File Offset: 0x000D2DA7
	public IReadOnlyCollection<Actor> getPassengers()
	{
		return this._passengers;
	}

	// Token: 0x0600125C RID: 4700 RVA: 0x000D4BAF File Offset: 0x000D2DAF
	internal void removePassenger(Actor pActor)
	{
		this._passengers.Remove(pActor);
	}

	// Token: 0x0600125D RID: 4701 RVA: 0x000D4BBE File Offset: 0x000D2DBE
	internal void addPassenger(Actor pActor)
	{
		if (this._passengers.Add(pActor))
		{
			this.passengerWaitCounter = 0;
			if (this.taxi_request != null)
			{
				this.taxi_request.embarkToBoat(pActor);
			}
		}
	}

	// Token: 0x0600125E RID: 4702 RVA: 0x000D4BEC File Offset: 0x000D2DEC
	public bool isHomeDockFull()
	{
		Building tHomeBuilding = this.actor.getHomeBuilding();
		return tHomeBuilding == null || tHomeBuilding.component_docks.isFull(this.actor.asset.boat_type);
	}

	// Token: 0x0600125F RID: 4703 RVA: 0x000D4C2C File Offset: 0x000D2E2C
	public bool isHomeDockOverfilled()
	{
		Building tHomeBuilding = this.actor.getHomeBuilding();
		return tHomeBuilding == null || tHomeBuilding.component_docks.isOverfilled(this.actor.asset.boat_type);
	}

	// Token: 0x06001260 RID: 4704 RVA: 0x000D4C6A File Offset: 0x000D2E6A
	public void destroyBecauseOverfilled()
	{
		if (this.isHomeDockOverfilled())
		{
			this.actor.getHitFullHealth(AttackType.Explosion);
		}
	}

	// Token: 0x06001261 RID: 4705 RVA: 0x000D4C81 File Offset: 0x000D2E81
	public override void Dispose()
	{
		this._passengers.Clear();
		this.taxi_target = null;
		this.taxi_request = null;
		base.Dispose();
	}

	// Token: 0x04001110 RID: 4368
	private readonly HashSet<Actor> _passengers = new HashSet<Actor>();

	// Token: 0x04001111 RID: 4369
	internal TaxiRequest taxi_request;

	// Token: 0x04001112 RID: 4370
	internal int passengerWaitCounter;

	// Token: 0x04001113 RID: 4371
	public WorldTile taxi_target;

	// Token: 0x04001114 RID: 4372
	public bool pickup_near_dock;

	// Token: 0x04001115 RID: 4373
	internal int last_movement_angle;

	// Token: 0x04001116 RID: 4374
	private Vector2 _last_step = Vector2.zero;
}
