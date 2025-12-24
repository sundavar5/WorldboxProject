using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020002D6 RID: 726
public class Projectile : CoreSystemObject<ProjectileData>
{
	// Token: 0x17000191 RID: 401
	// (get) Token: 0x06001ACB RID: 6859 RVA: 0x000FA298 File Offset: 0x000F8498
	public override BaseSystemManager manager
	{
		get
		{
			return World.world.projectiles;
		}
	}

	// Token: 0x06001ACC RID: 6860 RVA: 0x000FA2A4 File Offset: 0x000F84A4
	protected sealed override void setDefaultValues()
	{
		base.setDefaultValues();
		this._current_scale = 0f;
		this._target_scale = 0f;
		this._is_target_reached = false;
		this._angle_horizontal = 0f;
		this._angle_vertical = 0f;
		this._timer_smoke = 0f;
		this._dead_alpha = 1f;
		this._state = ProjectileState.Active;
		this._collision_timeout = 0f;
		this._speed = 0f;
	}

	// Token: 0x06001ACD RID: 6861 RVA: 0x000FA31D File Offset: 0x000F851D
	public Vector2 getStartVector()
	{
		return this._vector_start;
	}

	// Token: 0x06001ACE RID: 6862 RVA: 0x000FA325 File Offset: 0x000F8525
	public Vector2 getTargetVector()
	{
		return this._vector_target;
	}

	// Token: 0x06001ACF RID: 6863 RVA: 0x000FA330 File Offset: 0x000F8530
	public void start(BaseSimObject pInitiator, BaseSimObject pTargetObject, Vector3 pLaunchPosition, Vector3 pTargetPosition, string pAssetID, float pTargetPosZ = 0f, float pStartZ = 0.25f, float pForce = 0f, Action pKillAction = null, Kingdom pForcedKingdom = null)
	{
		this._kill_action = pKillAction;
		this.by_who = pInitiator;
		this._main_target = pTargetObject;
		if (this._main_target != null)
		{
			this._main_target_id = this._main_target.id;
		}
		if (this.by_who != null)
		{
			this.setStats(pInitiator.stats);
			this.kingdom = this.by_who.kingdom;
		}
		if (pForcedKingdom != null)
		{
			this.kingdom = pForcedKingdom;
		}
		this.asset = AssetManager.projectiles.get(pAssetID);
		this._speed = this.asset.speed + Randy.randomFloat(0f, this.asset.speed_random);
		if (pForce != 0f)
		{
			this._speed = pForce;
		}
		Vector3 tPos = new Vector3(pLaunchPosition.x, pLaunchPosition.y, 0f)
		{
			z = pStartZ
		};
		this._current_position_3d = tPos;
		this.calculateAngles(pLaunchPosition, pTargetPosition, tPos.z, pTargetPosZ);
		this.calculateVelocities();
		if (this.asset.frames == null || this.asset.frames.Length == 0)
		{
			this.asset.frames = SpriteTextureLoader.getSpriteList("effects/projectiles/" + this.asset.texture, false);
		}
		if (this.asset.sound_launch != string.Empty)
		{
			MusicBox.playSound(this.asset.sound_launch, pLaunchPosition.x, pLaunchPosition.y, true, false);
		}
		this._current_scale = this.asset.scale_start;
		this._target_scale = this.asset.scale_target;
		this._is_target_reached = false;
		this._dead_alpha = 1f;
		this.setState(ProjectileState.Active);
	}

	// Token: 0x06001AD0 RID: 6864 RVA: 0x000FA4D7 File Offset: 0x000F86D7
	private void calculateAngles(Vector3 pStart, Vector3 pTarget, float pStartZ, float pTargetZ)
	{
		this._vector_start = pStart;
		this._vector_target = pTarget;
		this._angle_horizontal = this.getHorizontalLaunchAngle(pStart, pTarget);
		this._angle_vertical = this.getVerticalLaunchAngle(pStart, pTarget, this._speed, pStartZ, pTargetZ);
	}

	// Token: 0x06001AD1 RID: 6865 RVA: 0x000FA518 File Offset: 0x000F8718
	private void calculateVelocities()
	{
		float initialSpeed = this._speed / this.asset.mass;
		this._velocity.x = initialSpeed * Mathf.Cos(this._angle_vertical) * Mathf.Cos(this._angle_horizontal);
		this._velocity.y = initialSpeed * Mathf.Cos(this._angle_vertical) * Mathf.Sin(this._angle_horizontal);
		this._velocity.z = initialSpeed * Mathf.Sin(this._angle_vertical);
	}

	// Token: 0x06001AD2 RID: 6866 RVA: 0x000FA598 File Offset: 0x000F8798
	private float getVerticalLaunchAngle(Vector3 pStart, Vector3 pTarget, float pForce, float pStartHeight, float pTargetHeight)
	{
		float tGravity = SimGlobals.m.gravity;
		float tDistance = Toolbox.DistVec3(pStart, pTarget);
		float tHeightDiff = pTargetHeight - pStartHeight;
		float tInitialSpeed = pForce / this.asset.mass;
		float tSpeedSquared = tInitialSpeed * tInitialSpeed;
		float tOperandA = Mathf.Pow(tInitialSpeed, 4f);
		float tOperandB = tGravity * (tGravity * tDistance * tDistance + 2f * tHeightDiff * tSpeedSquared);
		if (tOperandB > tOperandA)
		{
			return 0.7853982f;
		}
		float tRoot = Mathf.Sqrt(tOperandA - tOperandB);
		float tAngle = Mathf.Atan((tSpeedSquared + tRoot) / (SimGlobals.m.gravity * tDistance));
		float tAngle2 = Mathf.Atan((tSpeedSquared - tRoot) / (SimGlobals.m.gravity * tDistance));
		float tTimeOfFlight = 2f * tInitialSpeed * Mathf.Sin(tAngle) / tGravity;
		float tTimeOfFlight2 = 2f * tInitialSpeed * Mathf.Sin(tAngle2) / tGravity;
		float tResultAngle;
		if (this.asset.use_min_angle_height)
		{
			tResultAngle = ((tTimeOfFlight < tTimeOfFlight2) ? tAngle : tAngle2);
		}
		else if (tAngle > tAngle2)
		{
			tResultAngle = tAngle;
		}
		else
		{
			tResultAngle = tAngle2;
		}
		return tResultAngle;
	}

	// Token: 0x06001AD3 RID: 6867 RVA: 0x000FA691 File Offset: 0x000F8891
	private float getHorizontalLaunchAngle(Vector3 pStart, Vector3 pTarget)
	{
		return Mathf.Atan2(pTarget.y - pStart.y, pTarget.x - pStart.x);
	}

	// Token: 0x06001AD4 RID: 6868 RVA: 0x000FA6B4 File Offset: 0x000F88B4
	private void updateVelocity(float pElapsed)
	{
		this._velocity.z = this._velocity.z - SimGlobals.m.gravity * pElapsed;
		Vector3 tNewPos = this._current_position_3d + this._velocity * pElapsed;
		this._current_position_3d = tNewPos;
		float tDirectionAngle = Mathf.Atan2(this._velocity.y + this._velocity.z, this._velocity.x) * 57.29578f;
		this.rotation = Quaternion.AngleAxis(tDirectionAngle, Vector3.forward);
		if (this._current_position_3d.z <= 0f)
		{
			this._velocity.Set(0f, 0f, 0f);
			this._current_position_3d.z = 0f;
		}
		if (this._collision_timeout == 0f || this._current_position_3d.z == 0f)
		{
			AttackDataResult tAttackResult = this.checkHitOnNearbyUnits();
			switch (tAttackResult.state)
			{
			case ApplyAttackState.Hit:
			{
				this.setState(ProjectileState.ToRemove);
				Action kill_action = this._kill_action;
				if (kill_action != null)
				{
					kill_action();
				}
				this.targetReached();
				return;
			}
			case ApplyAttackState.Block:
				this.getCollided(this._vector_target);
				return;
			case ApplyAttackState.Deflect:
				this.getDeflected(this._vector_start, tAttackResult);
				return;
			case ApplyAttackState.Continue:
				return;
			}
			EffectsLibrary.spawnAt("fx_miss", this._current_position_3d, 0.1f);
			if (this.asset.can_be_left_on_ground)
			{
				this.setState(ProjectileState.AlphaAnimation);
			}
			else
			{
				this.setState(ProjectileState.ToRemove);
			}
			this.targetReached();
		}
	}

	// Token: 0x06001AD5 RID: 6869 RVA: 0x000FA83F File Offset: 0x000F8A3F
	private bool isOnGround()
	{
		return this._current_position_3d.z <= 0f;
	}

	// Token: 0x06001AD6 RID: 6870 RVA: 0x000FA858 File Offset: 0x000F8A58
	private AttackDataResult checkHitOnNearbyUnits()
	{
		if (this._is_target_reached)
		{
			return AttackDataResult.Continue;
		}
		WorldTile tTile = World.world.GetTile((int)this._current_position_3d.x, (int)this._current_position_3d.y);
		if (tTile == null)
		{
			if (this.isOnGround())
			{
				return AttackDataResult.Miss;
			}
			return AttackDataResult.Continue;
		}
		else
		{
			if (this.by_who.isRekt())
			{
				return AttackDataResult.Miss;
			}
			EnemyFinderData tEnemyData = EnemiesFinder.findEnemiesFrom(tTile, this.kingdom, -1);
			bool flag = this.isMainTargetStillValid();
			if (!flag)
			{
				this._main_target = null;
				this._main_target_id = -1L;
			}
			if (flag && !tEnemyData.list.Contains(this._main_target))
			{
				tEnemyData.list.Add(this._main_target);
			}
			if (tEnemyData.isEmpty())
			{
				if (this.isOnGround())
				{
					return AttackDataResult.Miss;
				}
				return AttackDataResult.Continue;
			}
			else
			{
				Vector3 tPos = this.by_who.current_position;
				BaseSimObject pInitiator = this.by_who;
				Kingdom pKingdom = this.kingdom;
				Vector3 pInitiatorPosition = tPos;
				AttackData tAttackData = new AttackData(pInitiator, tTile, this._current_position_3d, pInitiatorPosition, null, pKingdom, AttackType.Weapon, false, false, true, this.asset.id, null, 0f);
				if (this.isOnGround())
				{
					AttackDataResult tResult = MapBox.newAttack(tAttackData);
					if (tResult.state == ApplyAttackState.Continue)
					{
						tResult.state = ApplyAttackState.Miss;
					}
					return tResult;
				}
				foreach (BaseSimObject tSimObject in tEnemyData.list.LoopRandom<BaseSimObject>())
				{
					AttackDataResult tResult2 = this.checkHitForUnit(tSimObject, tAttackData);
					ApplyAttackState state = tResult2.state;
					if (state == ApplyAttackState.Hit || state - ApplyAttackState.Block <= 1)
					{
						return tResult2;
					}
				}
				return AttackDataResult.Continue;
			}
		}
	}

	// Token: 0x06001AD7 RID: 6871 RVA: 0x000FAA08 File Offset: 0x000F8C08
	private bool isMainTargetStillValid()
	{
		return !this._main_target.isRekt() && this._main_target.id == this._main_target_id;
	}

	// Token: 0x06001AD8 RID: 6872 RVA: 0x000FAA30 File Offset: 0x000F8C30
	private AttackDataResult checkHitForUnit(BaseSimObject pObject, AttackData pData)
	{
		if (pObject == null)
		{
			return AttackDataResult.Continue;
		}
		if (!pObject.isAlive())
		{
			return AttackDataResult.Continue;
		}
		float z = this._current_position_3d.z;
		float tObjectHeight = pObject.getHeight();
		if (Mathf.Abs(z - tObjectHeight) > 3f)
		{
			return AttackDataResult.Continue;
		}
		Vector3 tObjectCurrentPos = pObject.current_position;
		Vector3 tCurrentArrayPos3d = this._current_position_3d;
		float num = Toolbox.Dist(tCurrentArrayPos3d.x, tCurrentArrayPos3d.y + tCurrentArrayPos3d.z, tObjectCurrentPos.x, tObjectCurrentPos.y + pObject.getHeight());
		float tRad = this.asset.size + pObject.stats["size"];
		if (num > tRad)
		{
			return AttackDataResult.Continue;
		}
		return MapBox.checkAttackFor(pData, pObject);
	}

	// Token: 0x06001AD9 RID: 6873 RVA: 0x000FAAE5 File Offset: 0x000F8CE5
	public void setStats(BaseStats pStats)
	{
		this.stats.clear();
		this.stats.mergeStats(pStats, 1f);
	}

	// Token: 0x06001ADA RID: 6874 RVA: 0x000FAB03 File Offset: 0x000F8D03
	public float getLaunchAngle()
	{
		return Mathf.Atan2(this._velocity.y + this._velocity.z, this._velocity.x) * 57.29578f;
	}

	// Token: 0x06001ADB RID: 6875 RVA: 0x000FAB32 File Offset: 0x000F8D32
	private void updateScaleEffect(float pElapsed)
	{
		if (this._current_scale < this._target_scale)
		{
			this._current_scale += pElapsed * 0.2f;
			if (this._current_scale > this._target_scale)
			{
				this._current_scale = this._target_scale;
			}
		}
	}

	// Token: 0x06001ADC RID: 6876 RVA: 0x000FAB70 File Offset: 0x000F8D70
	private void updateTrailEffect(float pElapsed)
	{
		if (!this.asset.trail_effect_enabled)
		{
			return;
		}
		if (this._timer_smoke > 0f)
		{
			this._timer_smoke -= pElapsed;
			return;
		}
		Vector3 tPos = new Vector3(this._current_position_3d.x, this._current_position_3d.y + this._current_position_3d.z, 0f);
		BaseEffect tEffect = EffectsLibrary.spawnAt(this.asset.trail_effect_id, tPos, this.asset.trail_effect_scale);
		if (this.asset.look_at_target && tEffect != null)
		{
			tEffect.transform.rotation = this.rotation;
		}
		this._timer_smoke = this.asset.trail_effect_timer;
	}

	// Token: 0x06001ADD RID: 6877 RVA: 0x000FAC2C File Offset: 0x000F8E2C
	public void update(float pElapsed)
	{
		if (this._collision_timeout > 0f)
		{
			this._collision_timeout -= pElapsed;
			if (this._collision_timeout < 0f)
			{
				this._collision_timeout = 0f;
			}
		}
		switch (this._state)
		{
		case ProjectileState.Active:
			this.updateVelocity(pElapsed);
			break;
		case ProjectileState.AlphaAnimation:
			this.updateDeadAnimation(pElapsed);
			break;
		}
		this.updateScaleEffect(pElapsed);
		this.updateTrailEffect(pElapsed);
		this.updateLightEffect(pElapsed);
	}

	// Token: 0x06001ADE RID: 6878 RVA: 0x000FACAC File Offset: 0x000F8EAC
	private void updateLightEffect(float pElapsed)
	{
		if (this.asset.draw_light_area)
		{
			Vector2 tPos = new Vector2(this._current_position_3d.x, this._current_position_3d.y + this._current_position_3d.z);
			tPos.x += this.asset.draw_light_area_offset_x;
			tPos.y += this.asset.draw_light_area_offset_y;
			World.world.stack_effects.light_blobs.Add(new LightBlobData
			{
				position = tPos,
				radius = this.asset.draw_light_size
			});
		}
	}

	// Token: 0x06001ADF RID: 6879 RVA: 0x000FAD55 File Offset: 0x000F8F55
	private void updateDeadAnimation(float pElapsed)
	{
		this._dead_alpha -= pElapsed * 0.5f;
		if (this._dead_alpha < 0f)
		{
			this.setState(ProjectileState.ToRemove);
		}
	}

	// Token: 0x06001AE0 RID: 6880 RVA: 0x000FAD80 File Offset: 0x000F8F80
	private void targetReached()
	{
		this._is_target_reached = true;
		WorldTile tTile = this.getCurrentTilePosition();
		if (tTile == null)
		{
			tTile = World.world.GetTile((int)this._vector_target.x, (int)this._vector_target.y);
		}
		if (this.asset.impact_actions != null && tTile != null && !this.asset.impact_actions(this.by_who, null, tTile))
		{
			this.reset();
			return;
		}
		Vector2 tEndPos = this.getTransformedPositionWithHeight();
		if (!string.IsNullOrEmpty(this.asset.end_effect))
		{
			EffectsLibrary.spawnAt(this.asset.end_effect, tEndPos, this.asset.end_effect_scale);
		}
		if (this.asset.sound_impact != string.Empty)
		{
			MusicBox.playSound(this.asset.sound_impact, tEndPos.x, tEndPos.y, true, false);
		}
		if (tTile != null)
		{
			if (this.asset.world_actions != null)
			{
				this.asset.world_actions(this.by_who, null, tTile);
			}
			if (this.asset.hit_freeze)
			{
				tTile.freeze(1);
				for (int i = 0; i < tTile.neighbours.Length; i++)
				{
					WorldTile tNTile = tTile.neighbours[i];
					if (Randy.randomBool())
					{
						tNTile.freeze(1);
					}
				}
			}
			if (this.asset.hit_shake && tTile.zone.visible && MapBox.isRenderGameplay())
			{
				World.world.startShake(this.asset.shake_duration, this.asset.shake_interval, this.asset.shake_intensity, this.asset.shake_x, this.asset.shake_y);
			}
			if (this.asset.terraform_option != string.Empty)
			{
				MapAction.damageWorld(tTile, this.asset.terraform_range, AssetManager.terraform.get(this.asset.terraform_option), this.by_who);
			}
		}
		this.reset();
	}

	// Token: 0x06001AE1 RID: 6881 RVA: 0x000FAF74 File Offset: 0x000F9174
	public void getDeflected(Vector3 pPos, AttackDataResult pDataResult)
	{
		BaseSimObject tOldTarget = World.world.units.get(pDataResult.deflected_by_who_id);
		Vector3 tCurPos = this.getCurrentPosition();
		Vector3 tTargetPos = this._vector_start;
		this.start(tOldTarget, null, tCurPos, tTargetPos, this.asset.id, this.getCurrentHeight(), this.getCurrentHeight(), this._speed, null, null);
	}

	// Token: 0x06001AE2 RID: 6882 RVA: 0x000FAFD8 File Offset: 0x000F91D8
	public void getCollided(Vector3 pPos)
	{
		if (this.asset.trigger_on_collision)
		{
			this.targetReached();
			this.setState(ProjectileState.ToRemove);
			return;
		}
		this._collision_timeout = 1f;
		Vector3 tCurPos = this.getCurrentPosition();
		Vector3 tNewDirection = Toolbox.getNewPoint(tCurPos.x, tCurPos.y, pPos.x, pPos.y, 6f, true);
		this.start(this.by_who, null, tCurPos, tNewDirection, this.asset.id, this.getCurrentHeight() + 5f, this.getCurrentHeight(), this._speed * 0.2f, null, null);
	}

	// Token: 0x06001AE3 RID: 6883 RVA: 0x000FB075 File Offset: 0x000F9275
	private void setState(ProjectileState pState)
	{
		this._state = pState;
	}

	// Token: 0x06001AE4 RID: 6884 RVA: 0x000FB07E File Offset: 0x000F927E
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isTargetReached()
	{
		return this._is_target_reached;
	}

	// Token: 0x06001AE5 RID: 6885 RVA: 0x000FB086 File Offset: 0x000F9286
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public float getCurrentHeight()
	{
		return this._current_position_3d.z;
	}

	// Token: 0x06001AE6 RID: 6886 RVA: 0x000FB093 File Offset: 0x000F9293
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2 getCurrentPosition()
	{
		return this._current_position_3d;
	}

	// Token: 0x06001AE7 RID: 6887 RVA: 0x000FB0A0 File Offset: 0x000F92A0
	private WorldTile getCurrentTilePosition()
	{
		return World.world.GetTile((int)this._current_position_3d.x, (int)this._current_position_3d.y);
	}

	// Token: 0x06001AE8 RID: 6888 RVA: 0x000FB0C4 File Offset: 0x000F92C4
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2 getTransformedPositionWithHeight()
	{
		Vector2 tResult = this._current_position_3d;
		tResult.y += this.getCurrentHeight();
		return tResult;
	}

	// Token: 0x06001AE9 RID: 6889 RVA: 0x000FB0EF File Offset: 0x000F92EF
	public float getCurrentScale()
	{
		return this._current_scale;
	}

	// Token: 0x06001AEA RID: 6890 RVA: 0x000FB0F7 File Offset: 0x000F92F7
	public float getAngleForShadow()
	{
		return Toolbox.getAngleDegrees(this._current_position_3d.x, this._current_position_3d.y, this._vector_target.x, this._vector_target.y);
	}

	// Token: 0x06001AEB RID: 6891 RVA: 0x000FB12A File Offset: 0x000F932A
	public override void setAlive(bool pValue)
	{
		this._alive = pValue;
	}

	// Token: 0x06001AEC RID: 6892 RVA: 0x000FB133 File Offset: 0x000F9333
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isFinished()
	{
		return this._state == ProjectileState.ToRemove;
	}

	// Token: 0x06001AED RID: 6893 RVA: 0x000FB13E File Offset: 0x000F933E
	public bool isDeadAnimation()
	{
		return this._state == ProjectileState.AlphaAnimation;
	}

	// Token: 0x06001AEE RID: 6894 RVA: 0x000FB149 File Offset: 0x000F9349
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool canBeCollided()
	{
		return this.asset.can_be_collided && !this.isFinished() && !this.isTargetReached() && this._collision_timeout <= 0f;
	}

	// Token: 0x06001AEF RID: 6895 RVA: 0x000FB17E File Offset: 0x000F937E
	public float getAlpha()
	{
		return this._dead_alpha;
	}

	// Token: 0x06001AF0 RID: 6896 RVA: 0x000FB186 File Offset: 0x000F9386
	private void reset()
	{
		this._kill_action = null;
		this._collision_timeout = 0f;
		this.by_who = null;
		this._main_target = null;
		this._main_target_id = -1L;
		this.kingdom = null;
		this.stats.clear();
	}

	// Token: 0x06001AF1 RID: 6897 RVA: 0x000FB1C2 File Offset: 0x000F93C2
	public override void Dispose()
	{
		this.reset();
		this.asset = null;
		this.stats.reset();
		base.Dispose();
	}

	// Token: 0x040014D5 RID: 5333
	internal readonly BaseStats stats = new BaseStats();

	// Token: 0x040014D6 RID: 5334
	private BaseSimObject by_who;

	// Token: 0x040014D7 RID: 5335
	private BaseSimObject _main_target;

	// Token: 0x040014D8 RID: 5336
	private long _main_target_id;

	// Token: 0x040014D9 RID: 5337
	internal Kingdom kingdom;

	// Token: 0x040014DA RID: 5338
	private Action _kill_action;

	// Token: 0x040014DB RID: 5339
	public ProjectileAsset asset;

	// Token: 0x040014DC RID: 5340
	private Vector2 _vector_start;

	// Token: 0x040014DD RID: 5341
	private Vector2 _vector_target;

	// Token: 0x040014DE RID: 5342
	private float _current_scale;

	// Token: 0x040014DF RID: 5343
	private float _target_scale;

	// Token: 0x040014E0 RID: 5344
	private bool _is_target_reached;

	// Token: 0x040014E1 RID: 5345
	private Vector3 _current_position_3d;

	// Token: 0x040014E2 RID: 5346
	private float _angle_horizontal;

	// Token: 0x040014E3 RID: 5347
	private float _angle_vertical;

	// Token: 0x040014E4 RID: 5348
	private float _timer_smoke;

	// Token: 0x040014E5 RID: 5349
	private float _dead_alpha;

	// Token: 0x040014E6 RID: 5350
	private ProjectileState _state;

	// Token: 0x040014E7 RID: 5351
	private float _collision_timeout;

	// Token: 0x040014E8 RID: 5352
	private Vector3 _velocity;

	// Token: 0x040014E9 RID: 5353
	public Quaternion rotation;

	// Token: 0x040014EA RID: 5354
	private float _speed;
}
