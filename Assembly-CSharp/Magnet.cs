using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001D6 RID: 470
public class Magnet
{
	// Token: 0x06000DC6 RID: 3526 RVA: 0x000BE100 File Offset: 0x000BC300
	internal void magnetAction(bool pFromUpdate, WorldTile pTile = null)
	{
		if (ScrollWindow.isWindowActive())
		{
			this.dropPickedUnits();
			return;
		}
		if (pFromUpdate && this._magnet_state != 1 && this._magnet_state != 3)
		{
			return;
		}
		if (pTile != null)
		{
			this._magnet_last_pos = pTile;
		}
		this._magnet_throw.trackMouseMovement(this._magnet_state);
		this.updatePickedUnits();
		if (pTile != null)
		{
			World.world.flash_effects.flashPixel(pTile, 10, ColorType.White);
		}
		switch (this._magnet_state)
		{
		case 0:
			if (Input.GetMouseButton(0))
			{
				this._magnet_state = 1;
				this._magnet_throw.initializeMouseTracking();
				return;
			}
			break;
		case 1:
			if (!pFromUpdate)
			{
				this.pickupUnits(pTile);
			}
			if (Input.GetMouseButtonUp(0))
			{
				this._magnet_state = 2;
				this.dropPickedUnits();
				return;
			}
			break;
		case 2:
			if (!pFromUpdate && Input.GetMouseButton(0))
			{
				this.dropPickedUnits();
				this._magnet_state = 0;
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x06000DC7 RID: 3527 RVA: 0x000BE1D8 File Offset: 0x000BC3D8
	public void dropPickedUnits()
	{
		if (this.magnet_units.Count == 0)
		{
			return;
		}
		Vector2 tForce = this._magnet_throw.calculateThrowForce();
		for (int i = 0; i < this.magnet_units.Count; i++)
		{
			Actor tActor = this.magnet_units[i];
			if (tActor != null && tActor.isAlive())
			{
				Actor actor = tActor;
				actor.current_position.y = actor.current_position.y - tActor.position_height;
				tActor.is_in_magnet = false;
				tActor.dirty_current_tile = true;
				tActor.findCurrentTile(true);
				tActor.spawnOn(tActor.current_tile, tActor.getActorAsset().default_height);
				tActor.makeStunned(1f);
				tActor.addStatusEffect("magnetized", 0f, true);
				tActor.target_angle.z = 0f;
				if (tForce.magnitude > 0.1f)
				{
					Vector2 tRandomUnitForce = tForce;
					tRandomUnitForce.x += Random.Range(-0.3f, 0.3f);
					tRandomUnitForce.y += Random.Range(-0.3f, 0.3f);
					tActor.addForce(tRandomUnitForce.x, tRandomUnitForce.y, tRandomUnitForce.magnitude * 0.3f, true, true);
				}
				else
				{
					tActor.addForce(0f, 0f, 0.1f, true, false);
				}
				tActor.addActionWaitAfterLand(0.5f);
			}
		}
		this.magnet_units.Clear();
		this._magnet_units.Clear();
		this._has_units = false;
		this._magnet_throw.clear();
	}

	// Token: 0x06000DC8 RID: 3528 RVA: 0x000BE358 File Offset: 0x000BC558
	private void updatePickedUnits()
	{
		if (this._magnet_last_pos == null)
		{
			return;
		}
		if (this.magnet_units.Count == 0)
		{
			return;
		}
		this.updateMovingForce();
		if (this._picked_up_multiplier > 0.1f)
		{
			this._picked_up_multiplier -= World.world.delta_time * 0.3f;
			if (this._picked_up_multiplier < 0.1f)
			{
				this._picked_up_multiplier = 0.1f;
			}
		}
		float tCount = (float)this.magnet_units.Count;
		float tSeconds = 6f;
		if (tCount > 100f)
		{
			tSeconds = 4f;
		}
		else if (tCount > 50f)
		{
			tSeconds = 4.5f;
		}
		else if (tCount > 5f)
		{
			tSeconds = 5f;
		}
		float tSpeed = 6.2831855f / tSeconds;
		bool flag = false;
		int tBrushSize = Config.current_brush_data.width + 1;
		float tRadius;
		if (flag)
		{
			tRadius = Mathf.Lerp(0f, (float)tBrushSize, this._picked_up_multiplier) / 2f;
		}
		else
		{
			tRadius = (float)tBrushSize / 2f;
		}
		float tMultiplier = 1f / tCount * tRadius;
		this._angle += tSpeed * Time.deltaTime;
		Vector2 tCursorPos = World.world.getMousePos();
		int i = 0;
		while ((float)i < tCount)
		{
			Actor tActor = this.magnet_units[i];
			if (tActor != null && tActor.isAlive())
			{
				tActor.findCurrentTile(true);
				Vector3 tPos = tCursorPos;
				tPos.x += Mathf.Cos(this._angle + (float)i) * (tMultiplier * (float)i);
				tPos.y += Mathf.Sin(this._angle + (float)i) * (tMultiplier * (float)i);
				tActor.current_position = new Vector2(tPos.x, tPos.y - tActor.position_height);
				BaseActionActor callbacks_magnet_update = tActor.callbacks_magnet_update;
				if (callbacks_magnet_update != null)
				{
					callbacks_magnet_update(tActor);
				}
			}
			i++;
		}
	}

	// Token: 0x06000DC9 RID: 3529 RVA: 0x000BE534 File Offset: 0x000BC734
	private void updateMovingForce()
	{
		Vector2 tForce = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * 3f;
		if (tForce.magnitude > 0.1f)
		{
			this._target_angle = Mathf.Atan2(tForce.y, tForce.x) * 57.29578f;
			this._target_angle -= 90f;
		}
		else
		{
			this._target_angle = 0f;
		}
		this._current_angle = Mathf.SmoothDampAngle(this._current_angle, this._target_angle, ref this._rotation_velocity, 0.2f);
		this.moving_angle = this._current_angle;
	}

	// Token: 0x06000DCA RID: 3530 RVA: 0x000BE5E0 File Offset: 0x000BC7E0
	private void pickupUnits(WorldTile pTile)
	{
		BrushPixelData[] tBrushPixels = Config.current_brush_data.pos;
		for (int i = 0; i < tBrushPixels.Length; i++)
		{
			WorldTile tTile = World.world.GetTile(tBrushPixels[i].x + pTile.x, tBrushPixels[i].y + pTile.y);
			if (tTile != null && tTile.hasUnits())
			{
				tTile.doUnits(delegate(Actor tActor)
				{
					if (!tActor.asset.can_be_moved_by_powers)
					{
						return;
					}
					if (tActor.isInsideSomething())
					{
						return;
					}
					if (this._magnet_units.Add(tActor))
					{
						tActor.cancelAllBeh();
						this.magnet_units.Add(tActor);
						tActor.is_in_magnet = true;
						this._picked_up_multiplier = 2f;
					}
				});
			}
		}
		this._has_units = (this._magnet_units.Count > 0);
	}

	// Token: 0x06000DCB RID: 3531 RVA: 0x000BE668 File Offset: 0x000BC868
	public int countUnits()
	{
		return this._magnet_units.Count;
	}

	// Token: 0x06000DCC RID: 3532 RVA: 0x000BE675 File Offset: 0x000BC875
	public bool hasUnits()
	{
		return this._has_units;
	}

	// Token: 0x04000E1D RID: 3613
	private const float ANIMATED_SHRINK_SPEED = 0.3f;

	// Token: 0x04000E1E RID: 3614
	private const float PICKED_UP_SPEED_MULTIPLIER = 0.1f;

	// Token: 0x04000E1F RID: 3615
	private int _magnet_state;

	// Token: 0x04000E20 RID: 3616
	private WorldTile _magnet_last_pos;

	// Token: 0x04000E21 RID: 3617
	private bool _has_units;

	// Token: 0x04000E22 RID: 3618
	internal List<Actor> magnet_units = new List<Actor>();

	// Token: 0x04000E23 RID: 3619
	private HashSet<Actor> _magnet_units = new HashSet<Actor>();

	// Token: 0x04000E24 RID: 3620
	private float _picked_up_multiplier = 1f;

	// Token: 0x04000E25 RID: 3621
	private float _angle;

	// Token: 0x04000E26 RID: 3622
	public float moving_angle;

	// Token: 0x04000E27 RID: 3623
	private MagnetThrow _magnet_throw = new MagnetThrow();

	// Token: 0x04000E28 RID: 3624
	private float _target_angle;

	// Token: 0x04000E29 RID: 3625
	private float _current_angle;

	// Token: 0x04000E2A RID: 3626
	private float _rotation_velocity;
}
