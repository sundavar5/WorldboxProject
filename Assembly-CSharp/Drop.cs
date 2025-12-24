using System;
using UnityEngine;

// Token: 0x020001C9 RID: 457
public class Drop : BaseMapObject
{
	// Token: 0x06000D85 RID: 3461 RVA: 0x000BC808 File Offset: 0x000BAA08
	private void Awake()
	{
		this._sprite_renderer = base.gameObject.GetComponent<SpriteRenderer>();
		this._sprite_animation = base.gameObject.GetComponent<SpriteAnimation>();
	}

	// Token: 0x06000D86 RID: 3462 RVA: 0x000BC82C File Offset: 0x000BAA2C
	public void setForceSurprise()
	{
		this._force_surprise = true;
	}

	// Token: 0x06000D87 RID: 3463 RVA: 0x000BC838 File Offset: 0x000BAA38
	internal void prepare()
	{
		if (!this.created)
		{
			this.create();
		}
		base.gameObject.SetActive(true);
		this.m_transform.localScale = Vector3.one;
		this.active = true;
		this._force_surprise = false;
		this._timeInAir = 0f;
		this._timeToTarget = 0f;
		this._landed = false;
		this._parabolic = false;
		this.soundOn = false;
		this._currentHeightZ = 0f;
		this._caster_id = -1L;
		base.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
		if (this.DEBUG_COLOR)
		{
			this._sprite_renderer.color = Randy.getRandomColor();
		}
	}

	// Token: 0x06000D88 RID: 3464 RVA: 0x000BC8F8 File Offset: 0x000BAAF8
	internal void launchStraight(WorldTile pTile, DropAsset pAsset, float zDropHeight = -1f)
	{
		this._asset = pAsset;
		if (this._asset.animation_rotation)
		{
			this._rotation_speed = Randy.randomFloat(this._asset.animation_rotation_speed_min, this._asset.animation_rotation_speed_max);
			if (Randy.randomBool())
			{
				this._rotation_speed *= -1f;
			}
		}
		if (!string.IsNullOrEmpty(this._asset.sound_launch))
		{
			MusicBox.playSound(this._asset.sound_launch, pTile, false, false);
		}
		if (this._asset.action_launch != null)
		{
			this._asset.action_launch(null, null);
		}
		this._falling_speed = this._asset.falling_speed + Randy.randomFloat(0f, this._asset.falling_speed_random);
		if (this._asset.cached_sprites == null || this._asset.cached_sprites.Length == 0)
		{
			this._asset.cached_sprites = SpriteTextureLoader.getSpriteList(this._asset.path_texture, false);
		}
		this._sprite_renderer.sharedMaterial = LibraryMaterials.instance.dict[this._asset.material];
		this._sprite_animation.setFrames(this._asset.cached_sprites);
		if (this._asset.random_flip)
		{
			this._sprite_renderer.flipX = Randy.randomBool();
		}
		if (this._asset.animated)
		{
			this._sprite_animation.isOn = true;
			this._sprite_animation.timeBetweenFrames = this._asset.animation_speed + Randy.randomFloat(0f, this._asset.animation_speed_random);
		}
		else
		{
			this._sprite_animation.isOn = false;
		}
		if (this._asset.random_frame)
		{
			this._sprite_animation.setRandomFrame();
		}
		this._sprite_animation.forceUpdateFrame();
		this.current_tile = pTile;
		if (zDropHeight != -1f)
		{
			this._currentHeightZ = zDropHeight;
		}
		else
		{
			this._currentHeightZ = (float)((int)Randy.randomFloat(pAsset.falling_height.x, pAsset.falling_height.y));
		}
		this.current_position = new Vector2(pTile.posV3.x, pTile.posV3.y);
		this._startPosition = this.current_position;
		this.updatePosition();
	}

	// Token: 0x06000D89 RID: 3465 RVA: 0x000BCB38 File Offset: 0x000BAD38
	public void launchParabolic(float pStartHeight, float pMinHeight, float pMaxHeight, float pMinRadius, float pMaxRadius)
	{
		Vector2 tRandomVec = Randy.randomPointOnCircle(pMinRadius, pMaxRadius);
		this._targetPosition = this._startPosition + tRandomVec;
		this._targetHeight = Randy.randomFloat(pMinHeight, pMaxHeight);
		this._startPosition.y = this._startPosition.y + pStartHeight;
		this._currentHeightZ = this._startPosition.y;
		this._timeInAir = 0f;
		if (this._scale < 1f)
		{
			this._falling_speed /= this._scale * 2f;
		}
		float tDist = Toolbox.DistVec2Float(this._startPosition, this._targetPosition);
		this._timeToTarget = (tDist + this._targetHeight * 3f) * 0.25f / this._falling_speed;
		if (this._timeToTarget < 1f)
		{
			this._timeToTarget += 0.5f;
		}
		this._parabolic = true;
		this.updatePosition();
	}

	// Token: 0x06000D8A RID: 3466 RVA: 0x000BCC20 File Offset: 0x000BAE20
	private void updateStraightFall(float pElapsed)
	{
		float tChange = 15f * pElapsed;
		if (this._scale < 1f)
		{
			tChange *= this._falling_speed / (this._scale * 2f);
		}
		else
		{
			tChange *= this._falling_speed;
		}
		if (this._currentHeightZ < 0f)
		{
			tChange = 0f;
		}
		this._currentHeightZ -= tChange * this._scale;
		this.applyRandomXMove(tChange);
		if (this._currentHeightZ <= 0f)
		{
			this._currentHeightZ = 0f;
			this.updatePosition();
			this.current_tile = World.world.GetTile((int)this.current_position.x, (int)this.current_position.y);
			this.land();
			return;
		}
		this.updatePosition();
	}

	// Token: 0x06000D8B RID: 3467 RVA: 0x000BCCE8 File Offset: 0x000BAEE8
	private void applyRandomXMove(float pChangeX)
	{
		if (!this._asset.falling_random_x_move)
		{
			return;
		}
		if (pChangeX <= 0f)
		{
			return;
		}
		if (Randy.randomBool())
		{
			if (Randy.randomBool())
			{
				this.current_position.x = this.current_position.x - 1f * this._scale;
				return;
			}
			this.current_position.x = this.current_position.x + 1f * this._scale;
		}
	}

	// Token: 0x06000D8C RID: 3468 RVA: 0x000BCD54 File Offset: 0x000BAF54
	private void land()
	{
		if (this.current_tile != null)
		{
			if (this._asset.action_landed != null)
			{
				this._asset.action_landed(this.current_tile, this._asset.id);
			}
			if (this._asset.action_landed_drop != null)
			{
				this._asset.action_landed_drop(this, this.current_tile, this._asset.id);
			}
			if (this.current_tile.zone.visible && this._asset.sound_drop != string.Empty)
			{
				MusicBox.playSound(this._asset.sound_drop, this.current_tile, false, false);
			}
			if (this._force_surprise || this._asset.surprises_units)
			{
				ActionLibrary.suprisedByArchitector(null, this.current_tile);
			}
		}
		World.world.drop_manager.landDrop(this);
		this._landed = true;
	}

	// Token: 0x06000D8D RID: 3469 RVA: 0x000BCE44 File Offset: 0x000BB044
	public override void update(float pElapsed)
	{
		if (this._landed)
		{
			return;
		}
		this._sprite_animation.update(pElapsed);
		if (this._parabolic)
		{
			this.updateParabolicFall(pElapsed);
		}
		else
		{
			this.updateStraightFall(pElapsed);
		}
		if (this._landed)
		{
			return;
		}
		if (this._asset.animation_rotation)
		{
			this.updateRotation(pElapsed);
		}
	}

	// Token: 0x06000D8E RID: 3470 RVA: 0x000BCE9C File Offset: 0x000BB09C
	private void updateRotation(float pElapsed)
	{
		float tAngleZ = base.transform.rotation.eulerAngles.z + this._rotation_speed * pElapsed;
		base.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, tAngleZ));
	}

	// Token: 0x06000D8F RID: 3471 RVA: 0x000BCEEC File Offset: 0x000BB0EC
	private void updateParabolicFall(float pElapsed)
	{
		if (this._timeInAir > this._timeToTarget)
		{
			return;
		}
		this._timeInAir += pElapsed;
		if (this._timeInAir > this._timeToTarget)
		{
			this._timeInAir = this._timeToTarget;
		}
		float tTime = this._timeInAir / this._timeToTarget;
		Vector2 tParabolicPos = Toolbox.ParabolaDrag(this._startPosition, this._targetPosition, this._targetHeight, tTime);
		Vector2 tStraightLine = Vector2.Lerp(this._startPosition, this._targetPosition, tTime);
		this._currentHeightZ = tParabolicPos.y - tStraightLine.y;
		float tX = tParabolicPos.x;
		float tY = tParabolicPos.y - this._currentHeightZ;
		this.current_position.Set(tX, tY);
		if (this.current_position == this._targetPosition)
		{
			this.current_tile = World.world.GetTile((int)this._targetPosition.x, (int)this._targetPosition.y);
			this.land();
			return;
		}
		if (this._timeInAir >= this._timeToTarget)
		{
			this.current_tile = World.world.GetTile((int)this._targetPosition.x, (int)this._targetPosition.y);
			this.land();
			return;
		}
		this.updatePosition();
	}

	// Token: 0x06000D90 RID: 3472 RVA: 0x000BD028 File Offset: 0x000BB228
	private void updatePosition()
	{
		Vector3 tVec = new Vector3(this.current_position.x, this.current_position.y + this._currentHeightZ, this._currentHeightZ);
		this.m_transform.position = tVec;
	}

	// Token: 0x06000D91 RID: 3473 RVA: 0x000BD06B File Offset: 0x000BB26B
	public void setScale(Vector3 pVec)
	{
		this.m_transform.localScale = pVec;
		this._scale = pVec.x;
	}

	// Token: 0x06000D92 RID: 3474 RVA: 0x000BD085 File Offset: 0x000BB285
	public void setCasterId(long pCasterId)
	{
		this._caster_id = pCasterId;
	}

	// Token: 0x06000D93 RID: 3475 RVA: 0x000BD08E File Offset: 0x000BB28E
	public long getCasterId()
	{
		return this._caster_id;
	}

	// Token: 0x06000D94 RID: 3476 RVA: 0x000BD096 File Offset: 0x000BB296
	public void makeInactive()
	{
		this.reset();
		this.active = false;
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000D95 RID: 3477 RVA: 0x000BD0B1 File Offset: 0x000BB2B1
	public void reset()
	{
		this._asset = null;
		this.current_tile = null;
	}

	// Token: 0x06000D96 RID: 3478 RVA: 0x000BD0C4 File Offset: 0x000BB2C4
	private void OnDrawGizmos()
	{
		if (!this._parabolic)
		{
			return;
		}
		if (this._landed)
		{
			return;
		}
		if (this._timeToTarget == 0f)
		{
			return;
		}
		if (this._timeInAir > this._timeToTarget)
		{
			return;
		}
		if (this._gizmoColor.Equals(Vector4.zero))
		{
			this._gizmoColor = Randy.ColorHSV();
		}
		if (this._gizmoColor2.Equals(Vector4.zero))
		{
			this._gizmoColor2 = Randy.ColorHSV();
			this._gizmoColor2.a = 0.5f;
		}
		Gizmos.color = this._gizmoColor;
		Vector2 previousDrawPoint = this._startPosition;
		Vector2 previousDrawPoint2 = this._startPosition;
		int resolution = 60;
		for (int i = 1; i <= resolution; i++)
		{
			float simulationTime = (float)i / (float)resolution * this._timeToTarget;
			Vector2 currentPosition = Toolbox.ParabolaDrag(this._startPosition, this._targetPosition, this._targetHeight, simulationTime);
			Vector2 currentPosition2 = Toolbox.Parabola(this._startPosition, this._targetPosition, this._targetHeight, simulationTime);
			Gizmos.color = this._gizmoColor;
			Gizmos.DrawLine(previousDrawPoint, currentPosition);
			Gizmos.color = this._gizmoColor2;
			Gizmos.DrawLine(previousDrawPoint, currentPosition2);
			Gizmos.DrawLine(currentPosition, currentPosition2);
			Gizmos.DrawLine(previousDrawPoint2, currentPosition2);
			previousDrawPoint = currentPosition;
			previousDrawPoint2 = currentPosition2;
		}
	}

	// Token: 0x04000D34 RID: 3380
	private readonly bool DEBUG_COLOR;

	// Token: 0x04000D35 RID: 3381
	public int drop_index;

	// Token: 0x04000D36 RID: 3382
	internal bool active;

	// Token: 0x04000D37 RID: 3383
	private SpriteRenderer _sprite_renderer;

	// Token: 0x04000D38 RID: 3384
	private SpriteAnimation _sprite_animation;

	// Token: 0x04000D39 RID: 3385
	private float _currentHeightZ;

	// Token: 0x04000D3A RID: 3386
	private bool _landed;

	// Token: 0x04000D3B RID: 3387
	private DropAsset _asset;

	// Token: 0x04000D3C RID: 3388
	internal bool soundOn;

	// Token: 0x04000D3D RID: 3389
	private bool _parabolic;

	// Token: 0x04000D3E RID: 3390
	private float _falling_speed;

	// Token: 0x04000D3F RID: 3391
	private float _scale = 1f;

	// Token: 0x04000D40 RID: 3392
	private bool _force_surprise;

	// Token: 0x04000D41 RID: 3393
	private long _caster_id = -1L;

	// Token: 0x04000D42 RID: 3394
	private Vector2 _targetPosition;

	// Token: 0x04000D43 RID: 3395
	private Vector2 _startPosition;

	// Token: 0x04000D44 RID: 3396
	private float _targetHeight;

	// Token: 0x04000D45 RID: 3397
	private float _timeToTarget;

	// Token: 0x04000D46 RID: 3398
	private float _timeInAir;

	// Token: 0x04000D47 RID: 3399
	private Color _gizmoColor = Vector4.zero;

	// Token: 0x04000D48 RID: 3400
	private Color _gizmoColor2 = Vector4.zero;

	// Token: 0x04000D49 RID: 3401
	private float _rotation_speed;
}
