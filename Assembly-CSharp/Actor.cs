using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ai;
using ai.behaviours;
using UnityEngine;

// Token: 0x020001F7 RID: 503
public class Actor : BaseSimObject, ILoadable<ActorData>, ITraitsOwner<ActorTrait>, IEquatable<Actor>, IComparable<Actor>, IFavoriteable
{
	// Token: 0x17000090 RID: 144
	// (get) Token: 0x06000F08 RID: 3848 RVA: 0x000C4683 File Offset: 0x000C2883
	public Queue<HappinessHistory> happiness_change_history
	{
		get
		{
			return this._last_happiness_history;
		}
	}

	// Token: 0x06000F09 RID: 3849 RVA: 0x000C468C File Offset: 0x000C288C
	protected sealed override void setDefaultValues()
	{
		base.setDefaultValues();
		this._last_decision_id = string.Empty;
		this.is_inside_building = false;
		this.inside_building = null;
		this._state_adult = false;
		this._state_baby = false;
		this._state_egg = false;
		this.next_step_position = Globals.emptyVector;
		this.shake_offset = new Vector2(0f, 0f);
		this.move_jump_offset = new Vector2(0f, 0f);
		this._shake_horizontal = true;
		this._shake_vertical = true;
		this._shake_timer = 0f;
		this._shake_active = false;
		this._shake_volume = 0.1f;
		this._is_moving = false;
		this.is_visible = false;
		this.last_sprite_renderer_enabled = false;
		this.dirty_current_tile = true;
		this.split_path = SplitPathStatus.Normal;
		this.current_path_index = 0;
		this.current_path_global = null;
		this.actor_scale = 0f;
		this.target_scale = 0f;
		this.timer_action = 0f;
		this.timer_jump_animation = 0f;
		this.hitbox_bonus_height = 0f;
		this.velocity = default(Vector3);
		this.velocity_speed = 0f;
		this.under_forces = false;
		this._flying = false;
		this.is_in_magnet = false;
		this.attack_timer = 0f;
		this._attack_asset = ItemLibrary.base_attack;
		this.dirty_sprite_main = true;
		this.cached_sprite_head = null;
		this.dirty_sprite_head = true;
		this._cached_sprite_item = null;
		this._cached_hand_renderer_asset = null;
		this.s_personality = null;
		this._action_wait_after_land = false;
		this.rotation_cooldown = 0f;
		this.target_angle = default(Vector3);
		this._timeout_targets = 0f;
		this._precalc_movement_speed_skips = 0;
		this.flip_angle = 0f;
		this.lastX = -10f;
		this.lastY = -10f;
		this._jump_time = 0f;
		this._death_timer_color_stage_1 = 1f;
		this._death_timer_alpha_stage_2 = 1f;
		this.color = Color.white;
		this.ate_last_item_id = string.Empty;
		this.timestamp_session_ate_food = 0.0;
		this.timestamp_tween_session_social = 0.0;
		this.timestamp_profession_set = 0.0;
		this._timestamp_augmentation_effects = 0.0;
		this.show_shadow = false;
		this._decision_cooldowns = Toolbox.checkArraySize<double>(this._decision_cooldowns, AssetManager.decisions_library.list.Count);
		this._decision_disabled = Toolbox.checkArraySize<bool>(this._decision_disabled, AssetManager.decisions_library.list.Count);
		this.decisions = Toolbox.checkArraySize<DecisionAsset>(this.decisions, AssetManager.decisions_library.list.Count);
	}

	// Token: 0x06000F0A RID: 3850 RVA: 0x000C4931 File Offset: 0x000C2B31
	public void setShowShadow(bool pShadow)
	{
		this.show_shadow = pShadow;
	}

	// Token: 0x17000091 RID: 145
	// (get) Token: 0x06000F0B RID: 3851 RVA: 0x000C493C File Offset: 0x000C2B3C
	public string coloredName
	{
		get
		{
			if (this.data == null)
			{
				return "";
			}
			Kingdom kingdom = this.kingdom;
			if (((kingdom != null) ? kingdom.getColor() : null) != null)
			{
				return string.Concat(new string[]
				{
					"<color=",
					this.kingdom.getColor().color_text,
					">",
					this.getName(),
					"</color>"
				});
			}
			return this.getName();
		}
	}

	// Token: 0x06000F0C RID: 3852 RVA: 0x000C49B4 File Offset: 0x000C2BB4
	private void updateChildrenList(List<BaseActorComponent> pList, float pElapsed)
	{
		if (pList == null)
		{
			return;
		}
		for (int i = 0; i < pList.Count; i++)
		{
			pList[i].update(pElapsed);
		}
	}

	// Token: 0x06000F0D RID: 3853 RVA: 0x000C49E4 File Offset: 0x000C2BE4
	private void updateChildrenListSimple(List<ActorSimpleComponent> pList, float pElapsed)
	{
		if (pList == null)
		{
			return;
		}
		for (int i = 0; i < pList.Count; i++)
		{
			pList[i].update(pElapsed);
		}
	}

	// Token: 0x06000F0E RID: 3854 RVA: 0x000C4A14 File Offset: 0x000C2C14
	public void setAsset(ActorAsset pAsset)
	{
		if (this.asset != null)
		{
			this.asset.units.Remove(this);
		}
		this.asset = pAsset;
		this.asset.units.Add(this);
		this.setStatsDirty();
		if (this.canUseItems() && !this.hasEquipment())
		{
			this.equipment = new ActorEquipment();
		}
	}

	// Token: 0x06000F0F RID: 3855 RVA: 0x000C4A78 File Offset: 0x000C2C78
	internal override void create()
	{
		base.create();
		if (this.ai == null)
		{
			this.ai = new AiSystemActor(this);
		}
		this.ai.jobs_library = AssetManager.job_actor;
		this.ai.task_library = AssetManager.tasks_actor;
		this.ai.next_job_delegate = new GetNextJobID(this.getNextJob);
		this.ai.clear_action_delegate = new JobAction(this.clearBeh);
		this.ai.subscribeToTaskSwitch(new TaskSwitchAction(this.setItemSpriteRenderDirty));
		if (this.targets_to_ignore_timer == null)
		{
			this.targets_to_ignore_timer = new WorldTimer(3f, new Action(base.clearIgnoreTargets));
		}
		this._flying = this.asset.flying;
		this.setActorScale(this.asset.base_stats["scale"] * 0.6f);
		if (this.asset.finish_scale_on_creation)
		{
			this.target_scale = this.asset.base_stats["scale"];
			this.finishScale();
		}
		base.setObjectType(MapObjectType.Actor);
		this.setShowShadow(this.asset.shadow);
		if (this.asset.has_sound_idle_loop)
		{
			this.idle_loop_sound = new ActorIdleLoopSound(this.asset, this);
		}
		if (this.isHovering())
		{
			this.move_jump_offset.y = this.asset.hovering_min;
		}
		this.addChildren();
		if (this.asset.kingdom_id_wild.Contains("ants"))
		{
			AchievementLibrary.ant_world.check(null);
		}
		if (this.asset.kingdom_id_wild.Contains("monkey"))
		{
			AchievementLibrary.planet_of_apes.check(null);
		}
		if (this.asset.cancel_beh_on_land)
		{
			this.callbacks_landed = (BaseActionActor)Delegate.Combine(this.callbacks_landed, new BaseActionActor(this.checkLand));
		}
		this.callbacks_landed = (BaseActionActor)Delegate.Combine(this.callbacks_landed, new BaseActionActor(this.checkDeathOutsideMap));
		this.callbacks_on_death = (BaseActionActor)Delegate.Combine(this.callbacks_on_death, new BaseActionActor(this.playDeathSound));
		this.callbacks_magnet_update = (BaseActionActor)Delegate.Combine(this.callbacks_magnet_update, new BaseActionActor(this.actionMagnetAnimation));
	}

	// Token: 0x06000F10 RID: 3856 RVA: 0x000C4CC0 File Offset: 0x000C2EC0
	public bool canSeeTileBasedOnDirection(WorldTile pTile)
	{
		bool tHappenedOnLeft = this.isTileOnTheLeft(pTile);
		return this.is_looking_left == tHappenedOnLeft;
	}

	// Token: 0x06000F11 RID: 3857 RVA: 0x000C4CE4 File Offset: 0x000C2EE4
	public void setParent1(Actor pParentActor, bool pIncreaseChildren = true)
	{
		this.data.parent_id_1 = pParentActor.data.id;
		if (pIncreaseChildren)
		{
			pParentActor.increaseChildren();
		}
	}

	// Token: 0x06000F12 RID: 3858 RVA: 0x000C4D05 File Offset: 0x000C2F05
	public void setParent2(Actor pActor, bool pIncreaseChildren = true)
	{
		this.data.parent_id_2 = pActor.data.id;
		if (pIncreaseChildren)
		{
			pActor.increaseChildren();
		}
	}

	// Token: 0x06000F13 RID: 3859 RVA: 0x000C4D28 File Offset: 0x000C2F28
	internal void setProfession(UnitProfession pType, bool pCancelBeh = true)
	{
		this._profession = pType;
		this.profession_asset = AssetManager.professions.get(pType);
		this.setStatsDirty();
		if (base.hasCity())
		{
			this.city.setCitizensDirty();
		}
		if (pCancelBeh)
		{
			this.cancelAllBeh();
		}
		this.timestamp_profession_set = World.world.getCurWorldTime();
		this.clearGraphicsFully();
	}

	// Token: 0x06000F14 RID: 3860 RVA: 0x000C4D88 File Offset: 0x000C2F88
	private void addChildren()
	{
		if (this.asset.avatar_prefab != string.Empty)
		{
			GameObject tPrefab = Resources.Load<GameObject>("actors/" + this.asset.avatar_prefab);
			this.avatar = Object.Instantiate<GameObject>(tPrefab, World.world.transform_units);
			if (this.avatar.HasComponent<SpriteAnimation>())
			{
				this.sprite_animation = this.avatar.GetComponent<SpriteAnimation>();
				this.batch.c_sprite_animations.Add(this);
			}
			if (this.avatar.HasComponent<Crabzilla>())
			{
				this.addChild(this.avatar.GetComponent<Crabzilla>());
			}
			if (this.avatar.HasComponent<GodFinger>())
			{
				this.addChild(this.avatar.GetComponent<GodFinger>());
			}
			if (this.avatar.HasComponent<Dragon>())
			{
				this.addChild(this.avatar.GetComponent<Dragon>());
			}
			if (this.avatar.HasComponent<UFO>())
			{
				this.addChild(this.avatar.GetComponent<UFO>());
			}
		}
		if (this.asset.is_boat)
		{
			this.addChildSimple(new Boat());
		}
		if (this.children_pre_behaviour != null || this.children_special != null)
		{
			this.batch.c_update_children.Add(this);
		}
	}

	// Token: 0x06000F15 RID: 3861 RVA: 0x000C4EC0 File Offset: 0x000C30C0
	private void addChild(BaseActorComponent pObject)
	{
		if (this.children_special == null)
		{
			this.children_special = new List<BaseActorComponent>();
			this._dict_special = new Dictionary<Type, BaseActorComponent>();
		}
		Type tType = pObject.GetType();
		this.children_special.Add(pObject);
		this._dict_special.Add(tType, pObject);
		pObject.create(this);
	}

	// Token: 0x06000F16 RID: 3862 RVA: 0x000C4F14 File Offset: 0x000C3114
	private void addChildSimple(ActorSimpleComponent pObject)
	{
		if (this.children_pre_behaviour == null)
		{
			this.children_pre_behaviour = new List<ActorSimpleComponent>();
			this.dict_pre_behaviour = new Dictionary<Type, ActorSimpleComponent>();
		}
		Type tType = pObject.GetType();
		this.children_pre_behaviour.Add(pObject);
		this.dict_pre_behaviour.Add(tType, pObject);
		pObject.create(this);
	}

	// Token: 0x06000F17 RID: 3863 RVA: 0x000C4F68 File Offset: 0x000C3168
	public T getActorComponent<T>() where T : BaseActorComponent
	{
		if (this._dict_special == null)
		{
			return default(T);
		}
		BaseActorComponent tResult;
		if (this._dict_special.TryGetValue(typeof(T), out tResult))
		{
			return tResult as T;
		}
		return default(T);
	}

	// Token: 0x06000F18 RID: 3864 RVA: 0x000C4FB8 File Offset: 0x000C31B8
	public T getSimpleComponent<T>() where T : ActorSimpleComponent
	{
		ActorSimpleComponent tResult;
		if (this.dict_pre_behaviour.TryGetValue(typeof(T), out tResult))
		{
			return tResult as T;
		}
		return default(T);
	}

	// Token: 0x06000F19 RID: 3865 RVA: 0x000C4FF3 File Offset: 0x000C31F3
	private void playDeathSound(Actor pActor)
	{
		if (!this.asset.has_sound_death)
		{
			return;
		}
		MusicBox.playSound(this.asset.sound_death, this.current_tile, true, true);
	}

	// Token: 0x06000F1A RID: 3866 RVA: 0x000C501B File Offset: 0x000C321B
	public void playIdleSound()
	{
		if (!this.asset.has_sound_idle)
		{
			return;
		}
		MusicBox.playIdleSoundVisibleOnly(this.asset.sound_idle, this.current_tile);
	}

	// Token: 0x06000F1B RID: 3867 RVA: 0x000C5044 File Offset: 0x000C3244
	public void startShake(float pTimer = 0.3f, float pVol = 0.1f, bool pHorizontal = true, bool pVertical = true)
	{
		this._shake_horizontal = pHorizontal;
		this._shake_vertical = pVertical;
		this._shake_timer = Mathf.Min(pTimer, this.asset.max_shake_timer);
		this._shake_volume = pVol;
		this._shake_active = true;
		this.batch.c_shake.Add(this);
	}

	// Token: 0x06000F1C RID: 3868 RVA: 0x000C5098 File Offset: 0x000C3298
	public Vector3 getThrowStartPosition()
	{
		Vector3 tCurrentActorPos = this.cur_transform_position;
		Vector3 tActorScale = this.current_scale;
		Vector3 current_rotation = this.current_rotation;
		AnimationFrameData tFrameData = this.getAnimationFrameData();
		float tFrameDataPosX = 0f;
		float tFrameDataPosY = 0f;
		if (tFrameData != null)
		{
			tFrameDataPosX = tFrameData.pos_item.x;
			tFrameDataPosY = tFrameData.pos_item.y;
		}
		float tX = tCurrentActorPos.x + tFrameDataPosX * tActorScale.x;
		float tY = tCurrentActorPos.y + tFrameDataPosY * tActorScale.y;
		Vector3 tItemPosition = new Vector3(tX, tY, -0.01f);
		Vector3 tAngle = current_rotation;
		if (tAngle.y != 0f || tAngle.z != 0f)
		{
			Vector3 t_pivot = new Vector3(tCurrentActorPos.x, tCurrentActorPos.y, 0f);
			tItemPosition = Toolbox.RotatePointAroundPivot(ref tItemPosition, ref t_pivot, ref tAngle);
			tItemPosition.z = -0.01f;
		}
		return tItemPosition;
	}

	// Token: 0x06000F1D RID: 3869 RVA: 0x000C516D File Offset: 0x000C336D
	public void checkDefaultProfession()
	{
		this.setProfession(UnitProfession.Unit, false);
	}

	// Token: 0x06000F1E RID: 3870 RVA: 0x000C5178 File Offset: 0x000C3378
	public void addAfterglowStatus()
	{
		float tAdjustedTimeout = (float)this.asset.months_breeding_timeout * 5f;
		base.addStatusEffect("afterglow", tAdjustedTimeout, true);
	}

	// Token: 0x06000F1F RID: 3871 RVA: 0x000C51A8 File Offset: 0x000C33A8
	public void updateHover(float pElapsed)
	{
		if (!base.isAlive())
		{
			this.changeMoveJumpOffset(-pElapsed * 10f);
			return;
		}
		if (this.isOnGround())
		{
			this.changeMoveJumpOffset(-pElapsed * 3f);
		}
		else if (this.move_jump_offset.y < this.asset.hovering_min)
		{
			this.changeMoveJumpOffset(pElapsed * 3f);
			return;
		}
		if (this._hover_timer > 0f)
		{
			this._hover_timer -= pElapsed;
			return;
		}
		this._hover_timer = 1f + Randy.randomFloat(0f, 4f);
		if (World.world.isPaused())
		{
			return;
		}
		switch (this._hover_state)
		{
		case HoverState.Hover:
			if (Randy.randomBool())
			{
				this._hover_state = HoverState.Down;
				return;
			}
			this._hover_state = HoverState.Up;
			return;
		case HoverState.Up:
			this._hover_state = HoverState.Hover;
			if (this.move_jump_offset.y < this.asset.hovering_max)
			{
				this.changeMoveJumpOffset(pElapsed * 3f);
				return;
			}
			break;
		case HoverState.Down:
			this._hover_state = HoverState.Hover;
			if (this.move_jump_offset.y > this.asset.hovering_min)
			{
				this.changeMoveJumpOffset(-pElapsed * 3f);
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x06000F20 RID: 3872 RVA: 0x000C52DC File Offset: 0x000C34DC
	public void updatePollinate(float pElapsed)
	{
		if (!base.isAlive())
		{
			return;
		}
		if (!this.is_moving)
		{
			BehaviourTaskActor task = this.ai.task;
			if (((task != null) ? task.id : null) == "pollinate")
			{
				this.setHoverState(HoverState.Down);
				this.changeMoveJumpOffset(-pElapsed * 3f);
				return;
			}
		}
		this.setHoverState(HoverState.Up);
		if (this.move_jump_offset.y < this.asset.hovering_max)
		{
			this.changeMoveJumpOffset(pElapsed * 3f);
		}
	}

	// Token: 0x06000F21 RID: 3873 RVA: 0x000C5360 File Offset: 0x000C3560
	private void checkCalibrateTargetPosition()
	{
		if (this.hasRangeAttack())
		{
			return;
		}
		if (this.beh_actor_target == null)
		{
			return;
		}
		BaseSimObject tTarget = this.beh_actor_target;
		if (this.hasTask() && this.ai.action != null && this.ai.action.calibrate_target_position && tTarget != null && tTarget.isActor())
		{
			Actor tActorTarget = this.beh_actor_target.a;
			float num = (float)Toolbox.SquaredDist(tActorTarget.current_tile.x, tActorTarget.current_tile.y, this.tile_target.x, this.tile_target.y);
			float tPositionDistance = this.ai.action.check_actor_target_position_distance * this.ai.action.check_actor_target_position_distance;
			if (num > tPositionDistance)
			{
				this.clearPathForCalibration();
				this.ai.action.startExecute(this);
			}
		}
	}

	// Token: 0x06000F22 RID: 3874 RVA: 0x000C543F File Offset: 0x000C363F
	internal override bool addStatusEffect(StatusAsset pStatusAsset, float pOverrideTimer = 0f, bool pColorEffect = true)
	{
		if (pStatusAsset.affects_mind && this.hasTag("strong_mind"))
		{
			return false;
		}
		bool flag = base.addStatusEffect(pStatusAsset, pOverrideTimer, pColorEffect);
		if (flag && pColorEffect)
		{
			this.startColorEffect(ActorColorEffect.White);
		}
		return flag;
	}

	// Token: 0x06000F23 RID: 3875 RVA: 0x000C546D File Offset: 0x000C366D
	public void setTargetAngleZ(float pValue)
	{
		this.target_angle.z = pValue;
	}

	// Token: 0x06000F24 RID: 3876 RVA: 0x000C547B File Offset: 0x000C367B
	public void lookTowardsPosition(Vector2 pDirection)
	{
		if (!this.asset.can_flip)
		{
			return;
		}
		if (this.current_position.x < pDirection.x)
		{
			this.setFlip(true);
			return;
		}
		this.setFlip(false);
	}

	// Token: 0x06000F25 RID: 3877 RVA: 0x000C54AD File Offset: 0x000C36AD
	public override void setStatsDirty()
	{
		if (base.isAlive())
		{
			this.batch.c_stats_dirty.Add(this);
		}
		base.setStatsDirty();
		this.setItemSpriteRenderDirty();
	}

	// Token: 0x06000F26 RID: 3878 RVA: 0x000C54D4 File Offset: 0x000C36D4
	private void checkRageDemon()
	{
		if (!WorldLawLibrary.world_law_disasters_other.isEnabled())
		{
			return;
		}
		if (this.canTurnIntoDemon() && World.world_era.era_disaster_rage_brings_demons)
		{
			if (this.hasTrait("blessed"))
			{
				return;
			}
			if (this.isFavorite())
			{
				return;
			}
			if (base.hasStatus("rage") && Randy.randomChance(0.1f))
			{
				ActionLibrary.turnIntoDemon(this, null);
			}
		}
	}

	// Token: 0x06000F27 RID: 3879 RVA: 0x000C553C File Offset: 0x000C373C
	internal void updateChangeScale(float pElapsed)
	{
		if (this.actor_scale == this.target_scale)
		{
			return;
		}
		if (this.actor_scale > this.target_scale)
		{
			this.setActorScale(this.actor_scale - 0.2f * pElapsed);
			if (this.actor_scale < this.target_scale)
			{
				this.setActorScale(this.target_scale);
				return;
			}
		}
		else
		{
			this.setActorScale(this.actor_scale + 0.2f * pElapsed);
			if (this.actor_scale > this.target_scale)
			{
				this.setActorScale(this.target_scale);
			}
		}
	}

	// Token: 0x06000F28 RID: 3880 RVA: 0x000C55C4 File Offset: 0x000C37C4
	internal void newCreature()
	{
		this.changeHappiness("just_born", 0);
		World.world.game_stats.data.creaturesCreated += 1L;
		World.world.map_stats.creaturesCreated += 1L;
		AchievementLibrary.ten_thousands_creatures.check(null);
		this.generatePersonality();
		this.checkShouldBeEgg();
		this.event_full_stats = true;
		this.updateStats();
		this.event_full_stats = true;
		if (this.needsFood())
		{
			this.setNutrition(this.getMaxNutrition(), true);
		}
	}

	// Token: 0x06000F29 RID: 3881 RVA: 0x000C5654 File Offset: 0x000C3854
	public void clearTraits()
	{
		this.clearTraitCache();
		this.traits.Clear();
	}

	// Token: 0x06000F2A RID: 3882 RVA: 0x000C5668 File Offset: 0x000C3868
	public override void Dispose()
	{
		WorldBehaviourUnitTemperatures.removeUnit(this);
		this.clearTraits();
		this.idle_loop_sound = null;
		this.checkSimpleComponentListDispose(this.children_pre_behaviour);
		this.checkComponentListDispose(this.children_special);
		this._profession = UnitProfession.Nothing;
		this.sprite_animation = null;
		this.lover = null;
		this.idle_loop_sound = null;
		this.scheduled_tile_target = null;
		this._last_main_sprite = null;
		this._last_colored_sprite = null;
		this._last_color_asset = null;
		this._last_topic_sprite = null;
		this.children_special = null;
		this._dict_special = null;
		this.children_pre_behaviour = null;
		this.dict_pre_behaviour = null;
		this.avatar = null;
		this.clearDecisions();
		if (this.hasSubspecies())
		{
			World.world.subspecies.unitDied(this.subspecies);
			this.subspecies = null;
		}
		if (this.hasCulture())
		{
			World.world.cultures.unitDied(this.culture);
			this.culture = null;
		}
		this.ai.reset();
		this._last_happiness_history.Clear();
		this.citizen_job = null;
		if (base.hasCity())
		{
			World.world.cities.unitDied(this.city);
			this.city = null;
		}
		if (base.hasKingdom())
		{
			if (this.isKing())
			{
				this.kingdom.removeKing();
			}
			World.world.kingdoms.unitDied(this.kingdom);
			this.kingdom = null;
		}
		this.callbacks_on_death = null;
		this.callbacks_landed = null;
		this.callbacks_cancel_path_movement = null;
		this.callbacks_magnet_update = null;
		this.s_personality = null;
		this._s_special_effect_augmentations.Clear();
		this._s_special_effect_augmentations_timers.Clear();
		this.s_action_attack_target = null;
		this.targets_to_ignore_timer = null;
		this.clearOldPath();
		this.data = null;
		this.attackedBy = null;
		this.attack_target = null;
		this.has_attack_target = false;
		this.army = null;
		this.clan = null;
		this.culture = null;
		this.family = null;
		this.language = null;
		this.plot = null;
		this.religion = null;
		this.subspecies = null;
		this.beh_tile_target = null;
		this.beh_building_target = null;
		this.beh_actor_target = null;
		this.beh_book_target = null;
		this.exitBuilding();
		this.inside_boat = null;
		this.is_inside_boat = false;
		this.army = null;
		this.batch = null;
		this.equipment = null;
		this.tile_target = null;
		this.profession_asset = null;
		this._next_step_tile = null;
		this.asset = null;
		this.frame_data = null;
		this.animation_container = null;
		this._home_building = null;
		this.cached_sprite_head = null;
		this._cached_sprite_item = null;
		this._cached_hand_renderer_asset = null;
		this._aggression_targets.Clear();
		this._current_children = 0;
		this.is_forced_socialize_icon = false;
		this.is_forced_socialize_timestamp = 0.0;
		base.Dispose();
	}

	// Token: 0x06000F2B RID: 3883 RVA: 0x000C5924 File Offset: 0x000C3B24
	private void checkComponentListDispose(List<BaseActorComponent> pList)
	{
		if (pList != null)
		{
			for (int i = 0; i < pList.Count; i++)
			{
				pList[i].Dispose();
			}
			pList.Clear();
		}
	}

	// Token: 0x06000F2C RID: 3884 RVA: 0x000C5958 File Offset: 0x000C3B58
	private void checkSimpleComponentListDispose(List<ActorSimpleComponent> pList)
	{
		if (pList != null)
		{
			for (int i = 0; i < pList.Count; i++)
			{
				pList[i].Dispose();
			}
			pList.Clear();
		}
	}

	// Token: 0x06000F2D RID: 3885 RVA: 0x000C598C File Offset: 0x000C3B8C
	public void showTooltip(object pUiObject)
	{
		string tTooltipID;
		if (this.isKing())
		{
			tTooltipID = "actor_king";
		}
		else if (this.isCityLeader())
		{
			tTooltipID = "actor_leader";
		}
		else
		{
			tTooltipID = "actor";
		}
		Tooltip.show(pUiObject, tTooltipID, new TooltipData
		{
			actor = this
		});
	}

	// Token: 0x06000F2E RID: 3886 RVA: 0x000C59D2 File Offset: 0x000C3BD2
	public override ColorAsset getColor()
	{
		return this.kingdom.getColor();
	}

	// Token: 0x06000F2F RID: 3887 RVA: 0x000C59DF File Offset: 0x000C3BDF
	public void setHoverState(HoverState pState)
	{
		this._hover_state = pState;
	}

	// Token: 0x06000F30 RID: 3888 RVA: 0x000C59E8 File Offset: 0x000C3BE8
	public override string ToString()
	{
		if (this.data == null)
		{
			return "[Actor is null]";
		}
		string result;
		using (StringBuilderPool tBuilder = new StringBuilderPool())
		{
			tBuilder.Append(string.Format("[Actor:{0} ", base.id));
			if (!base.isAlive())
			{
				tBuilder.Append("[DEAD] ");
			}
			if (!string.IsNullOrEmpty(this.data.name))
			{
				tBuilder.Append(this.data.name + " ");
			}
			if (base.hasCity())
			{
				tBuilder.Append(string.Format("City:{0} ", this.city.getID()));
				if (this.city.kingdom != this.kingdom)
				{
					StringBuilderPool stringBuilderPool = tBuilder;
					string format = "CityKingdom:{0} ";
					Kingdom kingdom = this.city.kingdom;
					stringBuilderPool.Append(string.Format(format, (kingdom != null) ? kingdom.getID() : -1L));
				}
				if (this.city.hasArmy())
				{
					tBuilder.Append(string.Format("CityArmy:{0} ", this.city.army.id));
				}
			}
			if (base.hasKingdom())
			{
				tBuilder.Append(string.Format("Kingdom:{0} ", this.kingdom.getID()));
			}
			if (this.isKing())
			{
				tBuilder.Append("isKing ");
			}
			if (this.isCityLeader())
			{
				tBuilder.Append("isCityLeader ");
			}
			if (this.hasArmy())
			{
				tBuilder.Append(string.Format("Army:{0} ", this.army.id));
				if (this.isArmyGroupLeader())
				{
					tBuilder.Append("isArmyGroupLeader ");
				}
				if (this.isArmyGroupWarrior())
				{
					tBuilder.Append("isArmyGroupWarrior ");
				}
			}
			result = tBuilder.ToString().Trim() + "]";
		}
		return result;
	}

	// Token: 0x06000F31 RID: 3889 RVA: 0x000C5BE8 File Offset: 0x000C3DE8
	private int getMaxPossibleLevel()
	{
		return 9999;
	}

	// Token: 0x06000F32 RID: 3890 RVA: 0x000C5BF0 File Offset: 0x000C3DF0
	internal void addExperience(int pValue)
	{
		if (pValue == 0)
		{
			return;
		}
		if (!this.asset.can_level_up)
		{
			return;
		}
		if (!base.isAlive())
		{
			return;
		}
		if (this.hasCulture() && this.culture.hasTrait("fast_learners"))
		{
			pValue *= CultureTraitLibrary.getValue("fast_learners");
		}
		int tMaxLevel = this.getMaxPossibleLevel();
		if (this.data.level >= tMaxLevel)
		{
			return;
		}
		this.data.experience += pValue;
		if (this.data.experience >= this.getExpToLevelup())
		{
			this.levelUp();
		}
		if (this.data.level >= tMaxLevel)
		{
			this.data.experience = this.getExpToLevelup();
		}
	}

	// Token: 0x06000F33 RID: 3891 RVA: 0x000C5CA1 File Offset: 0x000C3EA1
	public void addRenown(int pValue)
	{
		this.data.renown += pValue;
	}

	// Token: 0x06000F34 RID: 3892 RVA: 0x000C5CB8 File Offset: 0x000C3EB8
	public void addRenown(int pAmount, float pPercent)
	{
		int tRenown = (int)((float)pAmount * pPercent);
		this.addRenown(tRenown);
	}

	// Token: 0x06000F35 RID: 3893 RVA: 0x000C5CD4 File Offset: 0x000C3ED4
	internal void updateAge()
	{
		this.checkGrowthEvent();
		float tAge = (float)this.getAge();
		if (this.hasSubspecies())
		{
			WorldAction all_actions_actor_growth = this.subspecies.all_actions_actor_growth;
			if (all_actions_actor_growth != null)
			{
				all_actions_actor_growth(this, this.current_tile);
			}
			this.updateAttributes();
		}
		if (base.hasCity())
		{
			if (this.isKing())
			{
				this.addExperience(20);
			}
			if (this.isCityLeader())
			{
				this.addExperience(10);
			}
		}
		if (this.isSapient() && tAge > 300f && this.hasTrait("immortal") && Randy.randomBool())
		{
			this.addTrait("evil", false);
		}
		if (tAge > 40f && Randy.randomChance(0.3f))
		{
			this.addTrait("wise", false);
		}
	}

	// Token: 0x06000F36 RID: 3894 RVA: 0x000C5D98 File Offset: 0x000C3F98
	private void updateAttributes()
	{
		if (!Randy.randomChance(0.3f))
		{
			return;
		}
		string tAttribute = this.subspecies.getPossibleAttribute();
		if (string.IsNullOrEmpty(tAttribute))
		{
			return;
		}
		ActorData actorData = this.data;
		string pKey = tAttribute;
		float num = actorData[pKey];
		actorData[pKey] = num + 1f;
	}

	// Token: 0x06000F37 RID: 3895 RVA: 0x000C5DE4 File Offset: 0x000C3FE4
	public void setMaxHappiness()
	{
		this.setHappiness(this.getMaxHappiness(), true);
	}

	// Token: 0x06000F38 RID: 3896 RVA: 0x000C5DF3 File Offset: 0x000C3FF3
	public void setHappiness(int pValue, bool pClamp = true)
	{
		if (pClamp)
		{
			pValue = Math.Clamp(pValue, this.getMinHappiness(), this.getMaxHappiness());
		}
		this.data.happiness = pValue;
	}

	// Token: 0x06000F39 RID: 3897 RVA: 0x000C5E18 File Offset: 0x000C4018
	public void restoreHealthPercent(float pVal)
	{
		if (pVal > 0f && !this.hasMaxHealth())
		{
			int tHealthToRestore = base.getMaxHealthPercent(pVal);
			this.restoreHealth(tHealthToRestore);
		}
	}

	// Token: 0x06000F3A RID: 3898 RVA: 0x000C5E44 File Offset: 0x000C4044
	public void restoreHealth(int pVal)
	{
		if (this.hasMaxHealth())
		{
			return;
		}
		base.changeHealth(pVal);
	}

	// Token: 0x06000F3B RID: 3899 RVA: 0x000C5E58 File Offset: 0x000C4058
	public bool changeHappiness(string pID, int pValue = 0)
	{
		if (!this.hasEmotions())
		{
			return false;
		}
		if (this.isEgg())
		{
			return false;
		}
		HappinessAsset tHappinessAsset = AssetManager.happiness_library.get(pID);
		if (tHappinessAsset.ignored_by_psychopaths && this.hasTrait("psychopath"))
		{
			return false;
		}
		int tValueToAdd = pValue + tHappinessAsset.value;
		int tFinalHappiness = this.getHappiness() + tValueToAdd;
		tFinalHappiness = Mathf.Clamp(tFinalHappiness, this.getMinHappiness(), this.getMaxHappiness());
		this.setHappiness(tFinalHappiness, true);
		if (tHappinessAsset.show_change_happiness_effect)
		{
			if (tValueToAdd > 0)
			{
				EffectsLibrary.showMetaEventEffect("fx_change_happiness_positive", this);
			}
			else if (tValueToAdd < 0)
			{
				EffectsLibrary.showMetaEventEffect("fx_change_happiness_negative", this);
			}
		}
		this._last_happiness_history.Enqueue(new HappinessHistory
		{
			index = tHappinessAsset.index,
			timestamp = World.world.getCurWorldTime(),
			bonus = pValue
		});
		if (this._last_happiness_history.Count > 20)
		{
			this._last_happiness_history.Dequeue();
		}
		return true;
	}

	// Token: 0x06000F3C RID: 3900 RVA: 0x000C5F47 File Offset: 0x000C4147
	public void spendNutritionOnBirth()
	{
		this.decreaseNutrition(SimGlobals.m.nutrition_cost_new_baby);
	}

	// Token: 0x06000F3D RID: 3901 RVA: 0x000C5F5C File Offset: 0x000C415C
	public void addNutritionFromEating(int pVal = 100, bool pSetMaxNutrition = false, bool pSetJustAte = false)
	{
		if (pSetMaxNutrition)
		{
			this.setNutrition(this.getMaxNutrition(), true);
		}
		else
		{
			int tNewValue = Math.Min(this.getMaxNutrition(), this.data.nutrition + pVal);
			this.setNutrition(tNewValue, true);
		}
		if (pSetJustAte)
		{
			this.justAte();
		}
	}

	// Token: 0x06000F3E RID: 3902 RVA: 0x000C5FA8 File Offset: 0x000C41A8
	public void updateNutritionDecay(bool pDoStarvationDamage = true)
	{
		int tNutritionSpent = this.subspecies.getMetabolicRate();
		this.decreaseNutrition(tNutritionSpent);
		if (this.isStarving())
		{
			this.setNutrition(0, true);
			if (pDoStarvationDamage)
			{
				int tDamage = base.getMaxHealthPercent(SimGlobals.m.starvation_damage_multiplier);
				this.getHit((float)tDamage, true, AttackType.Starvation, null, true, false, true);
				if (base.isAlive())
				{
					base.addStatusEffect("starving", 0f, false);
					return;
				}
			}
		}
		else
		{
			this.updateStamina();
			this.updateMana();
		}
	}

	// Token: 0x06000F3F RID: 3903 RVA: 0x000C6022 File Offset: 0x000C4222
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void decreaseNutrition(int pValue = -1)
	{
		this.setNutrition(this.getNutrition() - pValue, true);
	}

	// Token: 0x06000F40 RID: 3904 RVA: 0x000C6033 File Offset: 0x000C4233
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void setNutrition(int pVal, bool pClamp = true)
	{
		if (pClamp)
		{
			pVal = Math.Clamp(pVal, 0, this.getMaxNutrition());
		}
		this.data.nutrition = pVal;
	}

	// Token: 0x06000F41 RID: 3905 RVA: 0x000C6053 File Offset: 0x000C4253
	public void updateMana()
	{
		if (this.isManaFull())
		{
			return;
		}
		this.addMana(SimGlobals.m.mana_change);
	}

	// Token: 0x06000F42 RID: 3906 RVA: 0x000C6070 File Offset: 0x000C4270
	public void addMana(int pValue)
	{
		int tValueMax = this.getMaxMana();
		int tValueCurrent = this.getMana();
		if (tValueCurrent < tValueMax)
		{
			tValueCurrent += pValue;
		}
		tValueCurrent = Math.Clamp(tValueCurrent, 0, tValueMax);
		this.setMana(tValueCurrent, true);
	}

	// Token: 0x06000F43 RID: 3907 RVA: 0x000C60A4 File Offset: 0x000C42A4
	public int getMaxManaPercent(float pPercent)
	{
		int tResult = (int)((float)this.getMaxMana() * pPercent);
		return Mathf.Max(1, tResult);
	}

	// Token: 0x06000F44 RID: 3908 RVA: 0x000C60C4 File Offset: 0x000C42C4
	public void restoreManaPercent(float pVal)
	{
		if (pVal > 0f && !this.hasMaxMana())
		{
			int tManaToRestore = this.getMaxManaPercent(pVal);
			this.restoreMana(tManaToRestore);
		}
	}

	// Token: 0x06000F45 RID: 3909 RVA: 0x000C60F0 File Offset: 0x000C42F0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void changeMana(int pValue)
	{
		int tNewValue = this.data.mana + pValue;
		this.data.mana = Mathf.Clamp(tNewValue, 0, this.getMaxMana());
	}

	// Token: 0x06000F46 RID: 3910 RVA: 0x000C6123 File Offset: 0x000C4323
	public void restoreMana(int pVal)
	{
		if (this.hasMaxMana())
		{
			return;
		}
		this.changeMana(pVal);
	}

	// Token: 0x06000F47 RID: 3911 RVA: 0x000C6135 File Offset: 0x000C4335
	public void setMana(int pValue, bool pClamp = true)
	{
		if (pClamp)
		{
			pValue = Math.Clamp(pValue, 0, this.getMaxMana());
		}
		this.data.mana = pValue;
	}

	// Token: 0x06000F48 RID: 3912 RVA: 0x000C6155 File Offset: 0x000C4355
	public void spendMana(int pValueSpend)
	{
		if (pValueSpend == 0)
		{
			return;
		}
		this.setMana(this.getMana() - pValueSpend, true);
	}

	// Token: 0x06000F49 RID: 3913 RVA: 0x000C616C File Offset: 0x000C436C
	public int getMaxStaminaPercent(float pPercent)
	{
		int tResult = (int)((float)this.getMaxStamina() * pPercent);
		return Mathf.Max(1, tResult);
	}

	// Token: 0x06000F4A RID: 3914 RVA: 0x000C618C File Offset: 0x000C438C
	public void restoreStaminaPercent(float pVal)
	{
		if (pVal > 0f && !this.isStaminaFull())
		{
			int tStaminaToRestore = this.getMaxStaminaPercent(pVal);
			this.restoreStamina(tStaminaToRestore);
		}
	}

	// Token: 0x06000F4B RID: 3915 RVA: 0x000C61B8 File Offset: 0x000C43B8
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void changeStamina(int pValue)
	{
		int tNewValue = this.data.stamina + pValue;
		this.data.stamina = Mathf.Clamp(tNewValue, 0, this.getMaxStamina());
	}

	// Token: 0x06000F4C RID: 3916 RVA: 0x000C61EB File Offset: 0x000C43EB
	public void restoreStamina(int pVal)
	{
		if (this.isStaminaFull())
		{
			return;
		}
		this.changeStamina(pVal);
	}

	// Token: 0x06000F4D RID: 3917 RVA: 0x000C61FD File Offset: 0x000C43FD
	public void updateStamina()
	{
		if (this.isStaminaFull())
		{
			return;
		}
		this.addStamina(SimGlobals.m.stamina_change);
	}

	// Token: 0x06000F4E RID: 3918 RVA: 0x000C6218 File Offset: 0x000C4418
	public void addStamina(int pValue)
	{
		int tValueMax = this.getMaxStamina();
		int tValueCurrent = this.getStamina();
		if (tValueCurrent < tValueMax)
		{
			tValueCurrent += pValue;
		}
		tValueCurrent = Math.Clamp(tValueCurrent, 0, tValueMax);
		this.setStamina(tValueCurrent, true);
	}

	// Token: 0x06000F4F RID: 3919 RVA: 0x000C624C File Offset: 0x000C444C
	public void setStamina(int pValue, bool pClamp = true)
	{
		if (pClamp)
		{
			pValue = Math.Clamp(pValue, 0, this.getMaxStamina());
		}
		this.data.stamina = pValue;
	}

	// Token: 0x06000F50 RID: 3920 RVA: 0x000C626C File Offset: 0x000C446C
	public void spendStamina(int pValueSpend)
	{
		if (pValueSpend == 0)
		{
			return;
		}
		this.setStamina(this.getStamina() - pValueSpend, true);
	}

	// Token: 0x06000F51 RID: 3921 RVA: 0x000C6281 File Offset: 0x000C4481
	public void spendStaminaWithCooldown(int pValueSpend)
	{
		if (pValueSpend == 0)
		{
			return;
		}
		if (this.isUnderStaminaCooldown())
		{
			return;
		}
		this._last_stamina_reduce_timestamp = World.world.getCurSessionTime();
		this.setStamina(this.getStamina() - pValueSpend, true);
	}

	// Token: 0x06000F52 RID: 3922 RVA: 0x000C62B0 File Offset: 0x000C44B0
	public bool hasHappinessEntry(string pID, float pTime = 0f)
	{
		if (!this.hasHappinessHistory())
		{
			return false;
		}
		foreach (HappinessHistory tHappinessEntry in this.happiness_change_history)
		{
			if (!(tHappinessEntry.asset.id != pID))
			{
				if (pTime == 0f)
				{
					return true;
				}
				if (tHappinessEntry.elapsedSince() < (double)pTime)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x17000092 RID: 146
	// (get) Token: 0x06000F53 RID: 3923 RVA: 0x000C6338 File Offset: 0x000C4538
	public bool is_invincible
	{
		get
		{
			return this._has_status_invincible;
		}
	}

	// Token: 0x06000F54 RID: 3924 RVA: 0x000C6340 File Offset: 0x000C4540
	public void finishScale()
	{
		this.setActorScale(this.target_scale);
	}

	// Token: 0x06000F55 RID: 3925 RVA: 0x000C634E File Offset: 0x000C454E
	public void setActorScale(float pVal)
	{
		this.actor_scale = pVal;
		this.current_scale.Set(this.actor_scale, this.actor_scale, 1f);
	}

	// Token: 0x06000F56 RID: 3926 RVA: 0x000C6373 File Offset: 0x000C4573
	public void setData(ActorData pData)
	{
		this.data = pData;
	}

	// Token: 0x06000F57 RID: 3927 RVA: 0x000C637C File Offset: 0x000C457C
	public void loadData(ActorData pData)
	{
		this.setData(pData);
		pData.load();
	}

	// Token: 0x06000F58 RID: 3928 RVA: 0x000C638B File Offset: 0x000C458B
	public void generateSex()
	{
		if (Randy.randomBool())
		{
			this.data.sex = ActorSex.Male;
			return;
		}
		this.data.sex = ActorSex.Female;
	}

	// Token: 0x06000F59 RID: 3929 RVA: 0x000C63B0 File Offset: 0x000C45B0
	protected void generatePersonality()
	{
		if (this.hasSubspecies())
		{
			foreach (ActorTrait tTrait in this.subspecies.getActorBirthTraits().getTraits())
			{
				this.addTrait(tTrait, false);
			}
			if (this.subspecies.hasPhenotype())
			{
				this.generatePhenotypeAndShade();
			}
		}
		else
		{
			this.generateRandomSpawnTraits(this.asset);
		}
		if (this.isAdult())
		{
			this.checkTraitMutationGrowUp();
		}
		this.checkTraitMutationOnBirth();
		this.generateSex();
		this.setStatsDirty();
	}

	// Token: 0x06000F5A RID: 3930 RVA: 0x000C6454 File Offset: 0x000C4654
	public void calcIsEgg()
	{
		if (!this.hasSubspecies() || !this.subspecies.has_egg_form)
		{
			return;
		}
		this._state_egg = base.hasStatus("egg");
	}

	// Token: 0x06000F5B RID: 3931 RVA: 0x000C647D File Offset: 0x000C467D
	public void calcIsBaby()
	{
		if (!this.hasSubspecies())
		{
			return;
		}
		if (!this.asset.has_baby_form)
		{
			return;
		}
		if ((float)this.getAge() >= this.subspecies.age_adult)
		{
			return;
		}
		this._state_baby = true;
		this.clearSprites();
	}

	// Token: 0x06000F5C RID: 3932 RVA: 0x000C64B8 File Offset: 0x000C46B8
	public void setCheckLanding()
	{
		this.should_check_land_cancel = true;
	}

	// Token: 0x06000F5D RID: 3933 RVA: 0x000C64C4 File Offset: 0x000C46C4
	public void addForce(float pX, float pY, float pHeight, bool pCheckLandCancelAllActions = false, bool pIgnorePosHeight = false)
	{
		if (!this.asset.can_be_moved_by_powers)
		{
			return;
		}
		if (pCheckLandCancelAllActions)
		{
			this.setCheckLanding();
		}
		if (!pIgnorePosHeight && this.position_height > 0f)
		{
			return;
		}
		this.velocity.x = pX;
		this.velocity.y = pY;
		this.velocity.z = pHeight;
		this.velocity_speed = pHeight;
		this.under_forces = true;
	}

	// Token: 0x06000F5E RID: 3934 RVA: 0x000C652D File Offset: 0x000C472D
	public void setFlying(bool pVal)
	{
		this._flying = pVal;
		if (pVal)
		{
			this.hitbox_bonus_height = 8f;
			return;
		}
		this.hitbox_bonus_height = 2f;
	}

	// Token: 0x06000F5F RID: 3935 RVA: 0x000C6550 File Offset: 0x000C4750
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal void checkIsInLiquid()
	{
		bool tInLiquid = this.current_tile.is_liquid && this.move_jump_offset.y == 0f && this.position_height <= 0f && base.isAlive();
		this._is_in_liquid = tInLiquid;
	}

	// Token: 0x06000F60 RID: 3936 RVA: 0x000C659A File Offset: 0x000C479A
	private void addDefaultItemAttackActions(ItemAsset pItemAsset)
	{
		this.addItemActions(pItemAsset);
		if (pItemAsset.action_attack_target != null)
		{
			this.s_action_attack_target = (AttackAction)Delegate.Combine(this.s_action_attack_target, pItemAsset.action_attack_target);
		}
	}

	// Token: 0x06000F61 RID: 3937 RVA: 0x000C65C7 File Offset: 0x000C47C7
	private void addItemActions(ItemAsset pItemAsset)
	{
		if (pItemAsset.action_special_effect != null)
		{
			this._s_special_effect_augmentations.Add(pItemAsset);
		}
	}

	// Token: 0x06000F62 RID: 3938 RVA: 0x000C65DD File Offset: 0x000C47DD
	internal void attackTargetActions(BaseSimObject pTarget, WorldTile pTile)
	{
		AttackAction attackAction = this.s_action_attack_target;
		if (attackAction == null)
		{
			return;
		}
		attackAction(this, pTarget, pTile);
	}

	// Token: 0x06000F63 RID: 3939 RVA: 0x000C65F4 File Offset: 0x000C47F4
	protected void calcAgeStates()
	{
		this._state_egg = false;
		this._state_baby = false;
		this._state_adult = false;
		this.calcIsEgg();
		if (!this.isEgg())
		{
			this.calcIsBaby();
			if (!this.isBaby())
			{
				this._state_adult = true;
				this.clearSprites();
				return;
			}
		}
		else
		{
			this._state_baby = true;
			this.clearSprites();
		}
	}

	// Token: 0x06000F64 RID: 3940 RVA: 0x000C6650 File Offset: 0x000C4850
	internal override void updateStats()
	{
		if (!base.isStatsDirty())
		{
			return;
		}
		base.updateStats();
		this.checkGrowthEvent();
		this.decisions_counter = 0;
		this.batch.c_stats_dirty.Remove(this.a);
		if (!base.isAlive())
		{
			return;
		}
		this.s_action_attack_target = null;
		this.s_get_hit_action = null;
		this._s_special_effect_augmentations.Clear();
		this._s_special_effect_augmentations_timers.Clear();
		this.stats.clear();
		this.clearCombatActions();
		this.clearSpells();
		if (this.hasSubspecies())
		{
			this.stats.mergeStats(this.subspecies.base_stats, 1f);
			if (this.isSexMale())
			{
				this.stats.mergeStats(this.subspecies.base_stats_male, 1f);
			}
			else
			{
				this.stats.mergeStats(this.subspecies.base_stats_female, 1f);
			}
		}
		else
		{
			this.stats.mergeStats(this.asset.base_stats, 1f);
		}
		if (this.hasClan())
		{
			this.stats.mergeStats(this.clan.base_stats, 1f);
			if (this.isSexMale())
			{
				this.stats.mergeStats(this.clan.base_stats_male, 1f);
			}
			else
			{
				this.stats.mergeStats(this.clan.base_stats_female, 1f);
			}
		}
		if (this.hasLanguage())
		{
			this.stats.mergeStats(this.language.base_stats, 1f);
		}
		if (this.hasCulture())
		{
			this.stats.mergeStats(this.culture.base_stats, 1f);
		}
		BaseStats stats = this.stats;
		stats["diplomacy"] = stats["diplomacy"] + this.data["diplomacy"];
		stats = this.stats;
		stats["stewardship"] = stats["stewardship"] + this.data["stewardship"];
		stats = this.stats;
		stats["intelligence"] = stats["intelligence"] + this.data["intelligence"];
		stats = this.stats;
		stats["warfare"] = stats["warfare"] + this.data["warfare"];
		this._cache_check_has_status_removed_on_damage = false;
		if (base.hasAnyStatusEffect())
		{
			foreach (Status tStatus in base.getStatuses())
			{
				this.stats.mergeStats(tStatus.asset.base_stats, 1f);
				if (tStatus.asset.removed_on_damage)
				{
					this._cache_check_has_status_removed_on_damage = true;
				}
				if (!string.IsNullOrEmpty(tStatus.asset.decision_id))
				{
					DecisionAsset[] array = this.decisions;
					int num = this.decisions_counter;
					this.decisions_counter = num + 1;
					array[num] = tStatus.asset.getDecisionAsset();
				}
			}
		}
		if (!this.hasWeapon())
		{
			EquipmentAsset tDefaultWeapon = AssetManager.items.get(this.asset.default_attack);
			if (tDefaultWeapon != null)
			{
				this.stats.mergeStats(tDefaultWeapon.base_stats, 1f);
			}
		}
		this.checkAttackTypes();
		foreach (ActorTrait tTrait in this.traits)
		{
			if (!tTrait.only_active_on_era_flag || ((!tTrait.era_active_moon || World.world_era.flag_moon) && (!tTrait.era_active_night || World.world_era.overlay_darkness)))
			{
				if (tTrait.action_get_hit != null)
				{
					this.s_get_hit_action = (GetHitAction)Delegate.Combine(this.s_get_hit_action, tTrait.action_get_hit);
				}
				this.stats.mergeStats(tTrait.base_stats, 1f);
			}
		}
		this.is_forced_socialize_icon = (base.hasStatus("possessed") && this.hasTag("strong_mind"));
		if (base.hasStatus("budding"))
		{
			stats = this.stats;
			stats["diplomacy"] = stats["diplomacy"] * 2f;
			stats = this.stats;
			stats["stewardship"] = stats["stewardship"] * 2f;
			stats = this.stats;
			stats["intelligence"] = stats["intelligence"] * 2f;
			stats = this.stats;
			stats["warfare"] = stats["warfare"] * 2f;
		}
		if (this.isSapient())
		{
			this.s_personality = null;
			if (this.isKing() || this.isCityLeader())
			{
				string tPersonality = "balanced";
				float tHighStat = this.stats["diplomacy"];
				if (this.stats["diplomacy"] > this.stats["stewardship"])
				{
					tPersonality = "diplomat";
					tHighStat = this.stats["diplomacy"];
				}
				else if (this.stats["diplomacy"] < this.stats["stewardship"])
				{
					tPersonality = "administrator";
					tHighStat = this.stats["stewardship"];
				}
				if (this.stats["warfare"] > tHighStat)
				{
					tPersonality = "militarist";
				}
				this.s_personality = AssetManager.personalities.get(tPersonality);
				this.stats.mergeStats(this.s_personality.base_stats, 1f);
			}
		}
		float tBonusFromLevelHealth = (float)this.data.level * SimGlobals.m.level_mod_bonus_health * this.stats["health"];
		float tBonusFromLevelMana = (float)this.data.level * SimGlobals.m.level_mod_bonus_mana * this.stats["mana"];
		float tBonusFromLevelStamina = (float)this.data.level * SimGlobals.m.level_mod_bonus_stamina * this.stats["stamina"];
		stats = this.stats;
		stats["health"] = stats["health"] + tBonusFromLevelHealth;
		stats = this.stats;
		stats["mana"] = stats["mana"] + tBonusFromLevelMana;
		stats = this.stats;
		stats["stamina"] = stats["stamina"] + tBonusFromLevelStamina;
		stats = this.stats;
		stats["skill_combat"] = stats["skill_combat"] + (float)((int)(this.stats["warfare"] / 5f)) * 0.01f;
		stats = this.stats;
		stats["skill_spell"] = stats["skill_spell"] + (float)((int)(this.stats["intelligence"] / 5f)) * 0.01f;
		if (this.data.level > 5)
		{
			stats = this.stats;
			stats["skill_combat"] = stats["skill_combat"] + 0.1f;
			stats = this.stats;
			stats["skill_spell"] = stats["skill_spell"] + 0.1f;
		}
		this.addSpecialEffectAugmentations(this.traits);
		this.checkActionsFromAllMetas();
		this.recalcCombatActions();
		this.recalcSpells();
		this.registerDecisions();
		bool tHadStatusUnconscious = this._has_tag_unconscious;
		this.has_tag_generate_light = this.hasTag("generate_light");
		this._has_tag_unconscious = this.hasTag("unconscious");
		this.has_tag_immunity_cold = this.hasTag("immunity_cold");
		if (this._has_tag_unconscious)
		{
			if (!tHadStatusUnconscious)
			{
				if (this.batch.rnd.NextBool())
				{
					this._rotation_direction = RotationDirection.Left;
				}
				else
				{
					this._rotation_direction = RotationDirection.Right;
				}
			}
			this.timer_jump_animation = 0f;
		}
		this._has_trait_weightless = this.hasTrait("weightless");
		this._has_status_sleeping = base.hasStatus("sleeping");
		this._has_status_strange_urge = base.hasStatus("strange_urge");
		this._has_status_possessed = base.hasStatus("possessed");
		this._has_status_tantrum = base.hasStatus("tantrum");
		this._has_status_drowning = base.hasStatus("drowning");
		this._has_status_invincible = base.hasStatus("invincible");
		this.is_immovable = this.isImmovable();
		this.is_ai_frozen = this.isAiFrozen();
		this._has_stop_idle_animation = this.hasStopIdleAnimation();
		this._ignore_fights = this.isIgnoreFights();
		if (this.hasSubspecies())
		{
			this._has_emotions = this.subspecies.can_process_emotions;
		}
		else
		{
			this._has_emotions = false;
		}
		if (!this.hasWeapon())
		{
			EquipmentAsset tDefaultItemAttackAsset = AssetManager.items.get(this.asset.default_attack);
			this.addDefaultItemAttackActions(tDefaultItemAttackAsset);
			if (tDefaultItemAttackAsset.item_modifiers != null)
			{
				for (int i = 0; i < tDefaultItemAttackAsset.item_modifiers.Length; i++)
				{
					ItemModAsset tModData = tDefaultItemAttackAsset.item_modifiers[i];
					if (tModData != null)
					{
						this.addDefaultItemAttackActions(tModData);
					}
				}
			}
		}
		if (this.canUseItems())
		{
			foreach (ActorEquipmentSlot tSlot in this.equipment)
			{
				if (!tSlot.isEmpty())
				{
					Item tItem = tSlot.getItem();
					this.addItemActions(tItem.getAsset());
					if (tItem.action_attack_target != null)
					{
						this.s_action_attack_target = (AttackAction)Delegate.Combine(this.s_action_attack_target, tItem.action_attack_target);
					}
					foreach (string ptr in tItem.data.modifiers)
					{
						string tModID = ptr;
						ItemModAsset tModData2 = AssetManager.items_modifiers.get(tModID);
						this.addItemActions(tModData2);
					}
				}
			}
		}
		if (this._s_special_effect_augmentations.Count == 0)
		{
			this.batch.c_augmentation_effects.Remove(this.a);
		}
		else
		{
			this.batch.c_augmentation_effects.Add(this.a);
		}
		this._has_any_sick_trait = this.calculateIsSick();
		this._has_trait_peaceful = this.hasTrait("peaceful");
		this._has_trait_clone = this.hasTrait("clone");
		if (this.canUseItems())
		{
			foreach (ActorEquipmentSlot tSlot2 in this.equipment)
			{
				if (!tSlot2.isEmpty())
				{
					Item tItem2 = tSlot2.getItem();
					float tStatsMultiplier = 1f;
					if (tItem2.isBroken())
					{
						tStatsMultiplier = 0.5f;
					}
					ItemTools.mergeStatsWithItem(this.stats, tItem2, false, tStatsMultiplier);
				}
			}
		}
		if (this.asset.only_melee_attack)
		{
			this.stats["range"] = this.asset.base_stats["range"];
		}
		this.stats.normalize();
		stats = this.stats;
		stats["cities"] = stats["cities"] + (float)((int)this.stats["stewardship"] / 6 + 1);
		stats = this.stats;
		stats["bonus_towers"] = stats["bonus_towers"] + (float)((int)(this.stats["warfare"] / 10f));
		stats = this.stats;
		stats["mana"] = stats["mana"] + (float)((int)(this.stats["intelligence"] * SimGlobals.m.MANA_PER_INTELLIGENCE));
		this.stats.checkMultipliers();
		if (this.isSapient())
		{
			this.calculateOffspringBasedOnAge();
		}
		if (this.hasRangeAttack())
		{
			stats = this.stats;
			stats["range"] = stats["range"] + this.stats["range"] * World.world_era.range_weapons_multiplier;
		}
		stats = this.stats;
		stats["damage"] = stats["damage"] + this.stats["warfare"] / 5f;
		if (this.isBaby())
		{
			this.stats["damage"] = this.stats["damage"] * 0.5f;
			this.stats["health"] = this.stats["health"] * 0.5f;
		}
		this.stats.normalize();
		if (base.getHealth() > base.getMaxHealth())
		{
			base.setMaxHealth();
		}
		if (this.getHappiness() > this.getMaxHappiness())
		{
			this.setMaxHappiness();
		}
		if (this.getStamina() > this.getMaxStamina())
		{
			this.setMaxStamina();
		}
		if (this.getMana() > this.getMaxMana())
		{
			this.setMaxMana();
		}
		if (this.event_full_stats)
		{
			this.event_full_stats = false;
			base.setMaxHealth();
			this.setMaxStamina();
			this.setMaxMana();
		}
		if (this.isHovering())
		{
			this.batch.c_hovering.Add(this.a);
		}
		else
		{
			this.move_jump_offset.y = 0f;
			this.batch.c_hovering.Remove(this.a);
		}
		if (this.isPollinator())
		{
			this.batch.c_pollinating.Add(this.a);
		}
		else
		{
			this.batch.c_pollinating.Remove(this.a);
		}
		this.target_scale = this.stats["scale"];
		if (this.attack_timer > this.getAttackCooldown())
		{
			this.attack_timer = this.getAttackCooldown();
		}
	}

	// Token: 0x06000F65 RID: 3941 RVA: 0x000C747C File Offset: 0x000C567C
	public void resetAttackTimeout()
	{
		this.attack_timer = 0f;
	}

	// Token: 0x06000F66 RID: 3942 RVA: 0x000C7489 File Offset: 0x000C5689
	public void setActionTimeout(float pTimeout)
	{
		this.attack_timer = pTimeout;
	}

	// Token: 0x06000F67 RID: 3943 RVA: 0x000C7494 File Offset: 0x000C5694
	private void addSpecialEffectAugmentations(IEnumerable<BaseAugmentationAsset> pAssets)
	{
		foreach (BaseAugmentationAsset tAugmentation in pAssets)
		{
			if (tAugmentation.action_special_effect != null)
			{
				this._s_special_effect_augmentations.Add(tAugmentation);
			}
			if (tAugmentation.action_attack_target != null)
			{
				this.s_action_attack_target = (AttackAction)Delegate.Combine(this.s_action_attack_target, tAugmentation.action_attack_target);
			}
		}
	}

	// Token: 0x06000F68 RID: 3944 RVA: 0x000C7510 File Offset: 0x000C5710
	private void addSpecialEffectsFromMetas(List<BaseAugmentationAsset> pAugmentations)
	{
		if (pAugmentations == null || pAugmentations.Count == 0)
		{
			return;
		}
		this._s_special_effect_augmentations.AddRange(pAugmentations);
	}

	// Token: 0x06000F69 RID: 3945 RVA: 0x000C752C File Offset: 0x000C572C
	private void calculateOffspringBasedOnAge()
	{
		if (!this.hasTrait("immortal"))
		{
			int tBaseOffspring = (int)this.stats["offspring"];
			float tMaxAgeRatio = this.getAgeRatio();
			float tAgeFertilityMod = 1f;
			if (tMaxAgeRatio > 0.9f)
			{
				tAgeFertilityMod = 0.1f;
			}
			else if (tMaxAgeRatio > 0.7f)
			{
				tAgeFertilityMod = 0.2f;
			}
			else if (tMaxAgeRatio > 0.5f)
			{
				tAgeFertilityMod = 0.5f;
			}
			else if (tMaxAgeRatio > 0.3f)
			{
				tAgeFertilityMod = 0.8f;
			}
			this.stats["offspring"] = (float)((int)Math.Ceiling((double)((float)tBaseOffspring * tAgeFertilityMod)));
		}
	}

	// Token: 0x06000F6A RID: 3946 RVA: 0x000C75C0 File Offset: 0x000C57C0
	internal virtual void updateFall()
	{
		if (this.position_height < 0f)
		{
			return;
		}
		float tElapsed = World.world.elapsed;
		float tF = SimGlobals.m.gravity * this.stats.get("mass");
		this.position_height -= tF * tElapsed;
		if (this.position_height <= 0f)
		{
			this.position_height = 0f;
			if (!this.under_forces)
			{
				this.stopForce();
			}
		}
	}

	// Token: 0x06000F6B RID: 3947 RVA: 0x000C7638 File Offset: 0x000C5838
	private void stopForce()
	{
		this.position_height = 0f;
		this.velocity = Vector3.zero;
		this.under_forces = false;
		this.batch.c_action_landed.Add(this);
	}

	// Token: 0x06000F6C RID: 3948 RVA: 0x000C7668 File Offset: 0x000C5868
	internal virtual void actionLanded()
	{
		this.batch.c_action_landed.Remove(this);
		this.dirty_current_tile = true;
		BaseActionActor baseActionActor = this.callbacks_landed;
		if (baseActionActor != null)
		{
			baseActionActor(this);
		}
		if (this._action_wait_after_land)
		{
			this._action_wait_after_land = false;
			this.makeWait(this._action_wait_after_land_timer);
		}
		this.checkStepActionForTile(this.current_tile);
	}

	// Token: 0x06000F6D RID: 3949 RVA: 0x000C76C8 File Offset: 0x000C58C8
	public void updateShake(float pElapsed)
	{
		if (this._shake_active)
		{
			this._shake_timer -= pElapsed;
			if (this._shake_timer <= 0f)
			{
				this.shake_offset.Set(0f, 0f);
				this._shake_active = false;
				this.batch.c_shake.Remove(this);
				return;
			}
			if (this._shake_vertical)
			{
				this.shake_offset.y = this.batch.rnd.NextFloat(-this._shake_volume, this._shake_volume);
			}
			if (this._shake_horizontal)
			{
				this.shake_offset.x = this.batch.rnd.NextFloat(-this._shake_volume, this._shake_volume);
			}
		}
	}

	// Token: 0x06000F6E RID: 3950 RVA: 0x000C778C File Offset: 0x000C598C
	internal void updateFlipRotation(float pElapsed)
	{
		if (!this.asset.can_flip)
		{
			return;
		}
		if (this.flip)
		{
			this.flip_angle += pElapsed * 600f;
			if (this.flip_angle > 180f)
			{
				this.flip_angle = 180f;
			}
		}
		else
		{
			this.flip_angle -= pElapsed * 600f;
			if (this.flip_angle < 0f)
			{
				this.flip_angle = 0f;
			}
		}
		this.target_angle.y = this.flip_angle;
	}

	// Token: 0x06000F6F RID: 3951 RVA: 0x000C781A File Offset: 0x000C5A1A
	internal bool flipAnimationActive()
	{
		if (!this.asset.can_flip)
		{
			return false;
		}
		if (this.flip)
		{
			return this.flip_angle != 180f;
		}
		return this.flip_angle != 0f;
	}

	// Token: 0x06000F70 RID: 3952 RVA: 0x000C7854 File Offset: 0x000C5A54
	private void updateRotations(float pElapsed)
	{
		if (this.rotation_cooldown > 0f)
		{
			this.rotation_cooldown -= pElapsed;
			return;
		}
		if (this.is_unconscious)
		{
			this.updateRotationFall(pElapsed);
			return;
		}
		this.updateRotationBack(pElapsed);
	}

	// Token: 0x06000F71 RID: 3953 RVA: 0x000C788C File Offset: 0x000C5A8C
	private void updateRotationFall(float pElapsed)
	{
		if (this.getTextureAsset().prevent_unconscious_rotation)
		{
			return;
		}
		if (this.current_tile.is_liquid && this._is_in_liquid)
		{
			this.target_angle.z = 0f;
			return;
		}
		if (this._rotation_direction == RotationDirection.Left && this.target_angle.z != -90f)
		{
			this.target_angle.z = this.target_angle.z - 230f * pElapsed;
			if (this.target_angle.z < -90f)
			{
				this.target_angle.z = -90f;
			}
		}
		if (this._rotation_direction == RotationDirection.Right && this.target_angle.z != 90f)
		{
			this.target_angle.z = this.target_angle.z + 300f * pElapsed;
			if (this.target_angle.z > 90f)
			{
				this.target_angle.z = 90f;
			}
		}
	}

	// Token: 0x06000F72 RID: 3954 RVA: 0x000C7974 File Offset: 0x000C5B74
	private void updateRotationBack(float pElapsed)
	{
		if (this.target_angle.z == 0f)
		{
			return;
		}
		if (this.target_angle.z < 0f)
		{
			this.target_angle.z = this.target_angle.z + 300f * pElapsed;
			if (this.target_angle.z > 0f)
			{
				this.target_angle.z = 0f;
			}
		}
		if (this.target_angle.z > 0f)
		{
			this.target_angle.z = this.target_angle.z - 300f * pElapsed;
			if (this.target_angle.z < 0f)
			{
				this.target_angle.z = 0f;
			}
		}
	}

	// Token: 0x06000F73 RID: 3955 RVA: 0x000C7A28 File Offset: 0x000C5C28
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3 updateRotation()
	{
		if (this.current_rotation.y == this.target_angle.y && this.current_rotation.z == this.target_angle.z)
		{
			return this.current_rotation;
		}
		this.current_rotation.Set(this.target_angle.x, this.target_angle.y, this.target_angle.z);
		return this.current_rotation;
	}

	// Token: 0x06000F74 RID: 3956 RVA: 0x000C7AA0 File Offset: 0x000C5CA0
	internal void updateDeadBlackAnimation(float pElapsed)
	{
		if (this._death_timer_color_stage_1 > 0f)
		{
			this._death_timer_color_stage_1 -= pElapsed;
			if (this._death_timer_color_stage_1 <= 0f)
			{
				this._death_timer_color_stage_1 = 0f;
			}
		}
		if (this._death_timer_color_stage_1 > 0f)
		{
			Color tColor = new Color(this._death_timer_color_stage_1, this._death_timer_color_stage_1, this._death_timer_color_stage_1, 1f);
			this.color = tColor;
			return;
		}
		if (this._death_timer_alpha_stage_2 > 0f)
		{
			this._death_timer_alpha_stage_2 -= 1f * pElapsed;
			if (this._death_timer_alpha_stage_2 <= 0f)
			{
				this.die(true, AttackType.None, false, true);
				return;
			}
			Color tColor2 = new Color(this._death_timer_color_stage_1, this._death_timer_color_stage_1, this._death_timer_color_stage_1, this._death_timer_alpha_stage_2);
			this.color = tColor2;
		}
	}

	// Token: 0x06000F75 RID: 3957 RVA: 0x000C7B73 File Offset: 0x000C5D73
	internal virtual void spawnOn(WorldTile pTile, float pZHeight = 0f)
	{
		this.setCurrentTilePosition(pTile);
		this.position_height = pZHeight;
		this.hitbox_bonus_height = this.asset.default_height;
	}

	// Token: 0x06000F76 RID: 3958 RVA: 0x000C7B94 File Offset: 0x000C5D94
	public string getName()
	{
		if (string.IsNullOrEmpty(this.data.name))
		{
			this.generateNewName();
			AchievementLibrary.child_named_toto.checkBySignal(this.data.name);
		}
		return this.data.name;
	}

	// Token: 0x06000F77 RID: 3959 RVA: 0x000C7BCE File Offset: 0x000C5DCE
	public string generateName(MetaType pType, long pSeed, ActorSex pSex = ActorSex.None)
	{
		return NameGenerator.generateName(this, pType, pSeed + World.world.map_stats.life_dna, pSex);
	}

	// Token: 0x17000093 RID: 147
	// (get) Token: 0x06000F78 RID: 3960 RVA: 0x000C7BE9 File Offset: 0x000C5DE9
	// (set) Token: 0x06000F79 RID: 3961 RVA: 0x000C7BF1 File Offset: 0x000C5DF1
	public override string name
	{
		get
		{
			return this.getName();
		}
		protected set
		{
			this.data.name = value;
		}
	}

	// Token: 0x06000F7A RID: 3962 RVA: 0x000C7C00 File Offset: 0x000C5E00
	private void generateNewName()
	{
		ActorSex tSex = this.isSapient() ? this.data.sex : ActorSex.None;
		long tUnitID = this.getID();
		long tSeed = World.world.map_stats.life_dna + tUnitID * 543L;
		string tNewName = NameGenerator.generateName(this, MetaType.Unit, tSeed, tSex);
		base.setName(tNewName, true);
		BaseSystemData baseSystemData = this.data;
		Culture culture = this.culture;
		baseSystemData.name_culture_id = ((culture != null) ? culture.id : -1L);
	}

	// Token: 0x06000F7B RID: 3963 RVA: 0x000C7C78 File Offset: 0x000C5E78
	public override void trackName(bool pPostChange = false)
	{
		if (string.IsNullOrEmpty(this.data.name))
		{
			return;
		}
		if (pPostChange && (this.data.past_names == null || this.data.past_names.Count == 0))
		{
			return;
		}
		ActorData actorData = this.data;
		if (actorData.past_names == null)
		{
			actorData.past_names = new List<NameEntry>();
		}
		if (this.data.past_names.Count == 0)
		{
			NameEntry tNewEntry = new NameEntry(this.data.name, false, this.data.created_time);
			this.data.past_names.Add(tNewEntry);
			return;
		}
		if (this.data.past_names.Last<NameEntry>().name == this.data.name)
		{
			return;
		}
		NameEntry tNewEntry2 = new NameEntry(this.data.name, this.data.custom_name);
		this.data.past_names.Add(tNewEntry2);
	}

	// Token: 0x06000F7C RID: 3964 RVA: 0x000C7D70 File Offset: 0x000C5F70
	public void setHomeBuilding(Building pBuilding)
	{
		if (this._home_building != null)
		{
			this.clearHomeBuilding();
		}
		this._home_building = pBuilding;
		this._home_building.residents.Add(this.data.id);
		World.world.buildings.event_houses = true;
	}

	// Token: 0x06000F7D RID: 3965 RVA: 0x000C7DBE File Offset: 0x000C5FBE
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasHomeBuilding()
	{
		return this.getHomeBuilding() != null;
	}

	// Token: 0x17000094 RID: 148
	// (get) Token: 0x06000F7E RID: 3966 RVA: 0x000C7DC9 File Offset: 0x000C5FC9
	public Building home_building
	{
		get
		{
			return this._home_building;
		}
	}

	// Token: 0x06000F7F RID: 3967 RVA: 0x000C7DD1 File Offset: 0x000C5FD1
	public Building getHomeBuilding()
	{
		this.checkHomeBuilding();
		return this._home_building;
	}

	// Token: 0x06000F80 RID: 3968 RVA: 0x000C7DE0 File Offset: 0x000C5FE0
	public void checkHomeBuilding()
	{
		if (this._home_building == null)
		{
			return;
		}
		if (!this._home_building.isUsable() || this._home_building.isAbandoned())
		{
			this.clearHomeBuilding();
			this.changeHappiness("just_lost_house", 0);
			return;
		}
		if (this._home_building.asset.city_building && this._home_building.city != this.city)
		{
			this.clearHomeBuilding();
			this.changeHappiness("just_lost_house", 0);
		}
	}

	// Token: 0x06000F81 RID: 3969 RVA: 0x000C7E5C File Offset: 0x000C605C
	public void cloneTopicSprite(Sprite pSprite)
	{
		this._last_topic_sprite = pSprite;
	}

	// Token: 0x06000F82 RID: 3970 RVA: 0x000C7E65 File Offset: 0x000C6065
	public void clearLastTopicSprite()
	{
		this._last_topic_sprite = null;
	}

	// Token: 0x06000F83 RID: 3971 RVA: 0x000C7E6E File Offset: 0x000C606E
	public Sprite getTopicSpriteTrait()
	{
		if (this.traits.Count == 0)
		{
			return null;
		}
		return this.traits.GetRandom<ActorTrait>().getSprite();
	}

	// Token: 0x06000F84 RID: 3972 RVA: 0x000C7E8F File Offset: 0x000C608F
	public Sprite getSocializeTopic()
	{
		if (this._last_topic_sprite == null)
		{
			this._last_topic_sprite = AssetManager.communication_topic_library.getTopicSprite(this);
		}
		return this._last_topic_sprite;
	}

	// Token: 0x06000F85 RID: 3973 RVA: 0x000C7EB6 File Offset: 0x000C60B6
	public void forceSocializeTopic(string pPath)
	{
		this._last_topic_sprite = SpriteTextureLoader.getSprite(pPath);
		this.is_forced_socialize_timestamp = World.world.getCurWorldTime();
	}

	// Token: 0x06000F86 RID: 3974 RVA: 0x000C7ED4 File Offset: 0x000C60D4
	public void clearHomeBuilding()
	{
		this._home_building = null;
		World.world.buildings.event_houses = true;
	}

	// Token: 0x06000F87 RID: 3975 RVA: 0x000C7EF0 File Offset: 0x000C60F0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override void setAlive(bool pValue)
	{
		this._alive = pValue;
		if (!pValue && this.data.died_time == 0.0)
		{
			this.data.died_time = World.world.getCurWorldTime();
		}
		if (!pValue)
		{
			World.world.units.somethingChanged();
		}
	}

	// Token: 0x06000F88 RID: 3976 RVA: 0x000C7F44 File Offset: 0x000C6144
	internal bool isProfession(UnitProfession pType)
	{
		return this._profession == pType;
	}

	// Token: 0x06000F89 RID: 3977 RVA: 0x000C7F4F File Offset: 0x000C614F
	public bool isAnimal()
	{
		return !this.isSapient() && !this.asset.unit_other && this.asset.default_animal;
	}

	// Token: 0x06000F8A RID: 3978 RVA: 0x000C7F75 File Offset: 0x000C6175
	public bool isNomad()
	{
		return !base.isKingdomCiv();
	}

	// Token: 0x06000F8B RID: 3979 RVA: 0x000C7F80 File Offset: 0x000C6180
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isSapient()
	{
		return this.hasSubspecies() && this.subspecies.isSapient();
	}

	// Token: 0x06000F8C RID: 3980 RVA: 0x000C7F98 File Offset: 0x000C6198
	public bool isPrettyOld()
	{
		int tAge = this.getAge();
		return tAge > 1 && (float)tAge >= this.subspecies.age_adult && this.getAgeRatio() > 0.7f;
	}

	// Token: 0x06000F8D RID: 3981 RVA: 0x000C7FD0 File Offset: 0x000C61D0
	public bool isBaby()
	{
		return this._state_baby;
	}

	// Token: 0x06000F8E RID: 3982 RVA: 0x000C7FD8 File Offset: 0x000C61D8
	public bool isAdult()
	{
		return this._state_adult;
	}

	// Token: 0x06000F8F RID: 3983 RVA: 0x000C7FE0 File Offset: 0x000C61E0
	public bool isBreedingAge()
	{
		return this.hasSubspecies() && (float)this.getAge() >= this.subspecies.age_breeding;
	}

	// Token: 0x06000F90 RID: 3984 RVA: 0x000C8003 File Offset: 0x000C6203
	public bool isEgg()
	{
		return this._state_egg;
	}

	// Token: 0x06000F91 RID: 3985 RVA: 0x000C800B File Offset: 0x000C620B
	public int getAge()
	{
		return this.data.getAge();
	}

	// Token: 0x17000095 RID: 149
	// (get) Token: 0x06000F92 RID: 3986 RVA: 0x000C8018 File Offset: 0x000C6218
	public int age
	{
		get
		{
			return this.getAge();
		}
	}

	// Token: 0x06000F93 RID: 3987 RVA: 0x000C8020 File Offset: 0x000C6220
	public string getBirthday()
	{
		return Date.getDate(this.data.created_time);
	}

	// Token: 0x06000F94 RID: 3988 RVA: 0x000C8032 File Offset: 0x000C6232
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isKing()
	{
		return base.hasKingdom() && this.kingdom.king == this;
	}

	// Token: 0x06000F95 RID: 3989 RVA: 0x000C804C File Offset: 0x000C624C
	public float getMaturationTimeSeconds()
	{
		return this.getMaturationTimeMonths() * 5f;
	}

	// Token: 0x06000F96 RID: 3990 RVA: 0x000C805C File Offset: 0x000C625C
	public float getMaturationTimeMonths()
	{
		float tResult = 0f;
		if (this.hasSubspecies())
		{
			tResult += this.subspecies.getMaturationTimeMonths();
		}
		return tResult;
	}

	// Token: 0x17000096 RID: 150
	// (get) Token: 0x06000F97 RID: 3991 RVA: 0x000C8086 File Offset: 0x000C6286
	public bool is_army_captain
	{
		get
		{
			return this.hasArmy() && this.army.getCaptain() == this;
		}
	}

	// Token: 0x06000F98 RID: 3992 RVA: 0x000C80A0 File Offset: 0x000C62A0
	public bool isFavorite()
	{
		return this.data.favorite;
	}

	// Token: 0x06000F99 RID: 3993 RVA: 0x000C80AD File Offset: 0x000C62AD
	public void switchFavorite()
	{
		this.data.favorite = !this.data.favorite;
	}

	// Token: 0x06000F9A RID: 3994 RVA: 0x000C80C8 File Offset: 0x000C62C8
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override City getCity()
	{
		return this.city;
	}

	// Token: 0x06000F9B RID: 3995 RVA: 0x000C80D0 File Offset: 0x000C62D0
	public bool canBuildNewCity()
	{
		return !base.current_zone.hasCity() && !base.hasCity() && base.current_zone.isGoodForNewCity(this);
	}

	// Token: 0x06000F9C RID: 3996 RVA: 0x000C80FC File Offset: 0x000C62FC
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isCityLeader()
	{
		return base.hasCity() && this.city.leader == this;
	}

	// Token: 0x06000F9D RID: 3997 RVA: 0x000C8116 File Offset: 0x000C6316
	public override bool hasDied()
	{
		return this.data.died_time > 0.0;
	}

	// Token: 0x06000F9E RID: 3998 RVA: 0x000C812E File Offset: 0x000C632E
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal bool isPollinator()
	{
		Subspecies subspecies = this.subspecies;
		return subspecies != null && subspecies.has_trait_pollinating;
	}

	// Token: 0x06000F9F RID: 3999 RVA: 0x000C8141 File Offset: 0x000C6341
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal bool isAffectedByLiquid()
	{
		return !this.isInAir() && this._is_in_liquid;
	}

	// Token: 0x06000FA0 RID: 4000 RVA: 0x000C8153 File Offset: 0x000C6353
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal override bool isInAir()
	{
		return this._flying || this.isHovering() || this.isInMagnet();
	}

	// Token: 0x06000FA1 RID: 4001 RVA: 0x000C816D File Offset: 0x000C636D
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal override bool isFlying()
	{
		return this._flying;
	}

	// Token: 0x06000FA2 RID: 4002 RVA: 0x000C8175 File Offset: 0x000C6375
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal bool ignoresBlocks()
	{
		return this.asset.ignore_blocks || this.isFlying() || this.isInMagnet();
	}

	// Token: 0x06000FA3 RID: 4003 RVA: 0x000C8194 File Offset: 0x000C6394
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal bool isInMagnet()
	{
		return this.is_in_magnet;
	}

	// Token: 0x06000FA4 RID: 4004 RVA: 0x000C819C File Offset: 0x000C639C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal bool isHovering()
	{
		Subspecies subspecies = this.subspecies;
		return subspecies != null && subspecies.has_trait_hovering;
	}

	// Token: 0x06000FA5 RID: 4005 RVA: 0x000C81AF File Offset: 0x000C63AF
	public ActorAsset getActorAsset()
	{
		return this.asset;
	}

	// Token: 0x06000FA6 RID: 4006 RVA: 0x000C81B7 File Offset: 0x000C63B7
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public IReadOnlyCollection<ActorTrait> getTraits()
	{
		return this.traits;
	}

	// Token: 0x06000FA7 RID: 4007 RVA: 0x000C81BF File Offset: 0x000C63BF
	public bool isWaterCreature()
	{
		if (!this.asset.force_ocean_creature)
		{
			Subspecies subspecies = this.subspecies;
			return subspecies != null && subspecies.has_trait_water_creature;
		}
		return true;
	}

	// Token: 0x06000FA8 RID: 4008 RVA: 0x000C81E1 File Offset: 0x000C63E1
	public bool mustAvoidGround()
	{
		return this.isWaterCreature() && !this.asset.force_land_creature;
	}

	// Token: 0x06000FA9 RID: 4009 RVA: 0x000C81FC File Offset: 0x000C63FC
	public bool isInStablePlace()
	{
		if (this.mustAvoidGround())
		{
			if (this.current_tile.Type.ground)
			{
				return false;
			}
		}
		else
		{
			if (this.current_tile.Type.ocean && !this.isWaterCreature())
			{
				return false;
			}
			if (this.current_tile.Type.lava && this.asset.die_in_lava)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000FAA RID: 4010 RVA: 0x000C8263 File Offset: 0x000C6463
	internal bool hasWeapon()
	{
		return this.canUseItems() && !this.equipment.weapon.isEmpty();
	}

	// Token: 0x06000FAB RID: 4011 RVA: 0x000C8282 File Offset: 0x000C6482
	internal Item getWeapon()
	{
		if (this.hasWeapon())
		{
			return this.equipment.weapon.getItem();
		}
		return null;
	}

	// Token: 0x06000FAC RID: 4012 RVA: 0x000C829E File Offset: 0x000C649E
	internal EquipmentAsset getWeaponAsset()
	{
		if (this.hasWeapon())
		{
			return this.equipment.weapon.getItem().getAsset();
		}
		return AssetManager.items.get(this.asset.default_attack);
	}

	// Token: 0x06000FAD RID: 4013 RVA: 0x000C82D3 File Offset: 0x000C64D3
	public bool isWeaponFirearm()
	{
		Item weapon = this.getWeapon();
		return ((weapon != null) ? weapon.getAsset().group_id : null) == "firearm";
	}

	// Token: 0x06000FAE RID: 4014 RVA: 0x000C82F6 File Offset: 0x000C64F6
	public bool isArmyGroupLeader()
	{
		return this.hasArmy() && this.army.getCaptain() == this;
	}

	// Token: 0x06000FAF RID: 4015 RVA: 0x000C8310 File Offset: 0x000C6510
	public bool isArmyGroupWarrior()
	{
		return this.hasArmy() && this.army.getCaptain() != this;
	}

	// Token: 0x06000FB0 RID: 4016 RVA: 0x000C832D File Offset: 0x000C652D
	public bool hasTraits()
	{
		return this.traits.Count > 0;
	}

	// Token: 0x17000097 RID: 151
	// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x000C833D File Offset: 0x000C653D
	public bool is_profession_nothing
	{
		get
		{
			return this._profession == UnitProfession.Nothing;
		}
	}

	// Token: 0x17000098 RID: 152
	// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x000C8348 File Offset: 0x000C6548
	public bool is_profession_king
	{
		get
		{
			return this._profession == UnitProfession.King;
		}
	}

	// Token: 0x17000099 RID: 153
	// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x000C8353 File Offset: 0x000C6553
	public bool is_profession_leader
	{
		get
		{
			return this._profession == UnitProfession.Leader;
		}
	}

	// Token: 0x1700009A RID: 154
	// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x000C835E File Offset: 0x000C655E
	public bool is_profession_warrior
	{
		get
		{
			return this._profession == UnitProfession.Warrior;
		}
	}

	// Token: 0x1700009B RID: 155
	// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x000C8369 File Offset: 0x000C6569
	public bool is_profession_citizen
	{
		get
		{
			return this._profession == UnitProfession.Unit;
		}
	}

	// Token: 0x06000FB6 RID: 4022 RVA: 0x000C8374 File Offset: 0x000C6574
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isSexMale()
	{
		return this.data.sex == ActorSex.Male;
	}

	// Token: 0x06000FB7 RID: 4023 RVA: 0x000C8384 File Offset: 0x000C6584
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isSexFemale()
	{
		return this.data.sex == ActorSex.Female;
	}

	// Token: 0x06000FB8 RID: 4024 RVA: 0x000C8394 File Offset: 0x000C6594
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasEquipment()
	{
		return this.equipment != null;
	}

	// Token: 0x06000FB9 RID: 4025 RVA: 0x000C839F File Offset: 0x000C659F
	public bool hasHouse()
	{
		return this.getHomeBuilding() != null;
	}

	// Token: 0x06000FBA RID: 4026 RVA: 0x000C83AA File Offset: 0x000C65AA
	public bool hasLover()
	{
		return this.lover != null;
	}

	// Token: 0x06000FBB RID: 4027 RVA: 0x000C83B5 File Offset: 0x000C65B5
	public bool hasBestFriend()
	{
		return this.getBestFriend() != null;
	}

	// Token: 0x06000FBC RID: 4028 RVA: 0x000C83C0 File Offset: 0x000C65C0
	public Actor getBestFriend()
	{
		if (this.data.best_friend_id.hasValue())
		{
			return World.world.units.get(this.data.best_friend_id);
		}
		return null;
	}

	// Token: 0x06000FBD RID: 4029 RVA: 0x000C83F0 File Offset: 0x000C65F0
	public bool isChildOf(Actor pActor)
	{
		return this.isChildOf(pActor.data.id);
	}

	// Token: 0x06000FBE RID: 4030 RVA: 0x000C8403 File Offset: 0x000C6603
	public bool isChildOf(long pID)
	{
		return this.data.parent_id_1 == pID || this.data.parent_id_2 == pID;
	}

	// Token: 0x06000FBF RID: 4031 RVA: 0x000C8426 File Offset: 0x000C6626
	public bool isParentOf(long pID, Actor pActor)
	{
		return pID == pActor.data.parent_id_1 || pID == pActor.data.parent_id_2;
	}

	// Token: 0x06000FC0 RID: 4032 RVA: 0x000C8449 File Offset: 0x000C6649
	public bool isParentOf(Actor pActor)
	{
		return this.isParentOf(this.data.id, pActor);
	}

	// Token: 0x06000FC1 RID: 4033 RVA: 0x000C845D File Offset: 0x000C665D
	public IEnumerable<Actor> getParents()
	{
		Actor tParent = World.world.units.get(this.data.parent_id_1);
		if (tParent != null && tParent.isAlive())
		{
			yield return tParent;
		}
		Actor tParent2 = World.world.units.get(this.data.parent_id_2);
		if (tParent2 != null && tParent2.isAlive())
		{
			yield return tParent2;
		}
		yield break;
	}

	// Token: 0x06000FC2 RID: 4034 RVA: 0x000C846D File Offset: 0x000C666D
	public IEnumerable<Actor> getChildren(bool pOnlyCurrentFamily = true)
	{
		if (pOnlyCurrentFamily)
		{
			if (!this.hasFamily())
			{
				yield break;
			}
			Family tFamily = this.family;
			foreach (Actor tChild in tFamily.units)
			{
				if (tChild != this && tChild.isChildOf(this))
				{
					yield return tChild;
				}
			}
			List<Actor>.Enumerator enumerator = default(List<Actor>.Enumerator);
		}
		else
		{
			int tCurrentLivingChildren = this.current_children_count;
			if (tCurrentLivingChildren == 0)
			{
				yield break;
			}
			long tParentID = this.data.id;
			if (this.hasSubspecies() && !this.subspecies.isRekt())
			{
				foreach (Actor tChild2 in this.subspecies.units)
				{
					if (!tChild2.isRekt() && tChild2 != this && tChild2.isChildOf(tParentID))
					{
						int num = tCurrentLivingChildren;
						tCurrentLivingChildren = num - 1;
						yield return tChild2;
						if (tCurrentLivingChildren == 0)
						{
							break;
						}
					}
				}
				List<Actor>.Enumerator enumerator = default(List<Actor>.Enumerator);
			}
		}
		yield break;
		yield break;
	}

	// Token: 0x06000FC3 RID: 4035 RVA: 0x000C8484 File Offset: 0x000C6684
	public bool hasSuitableBookTraits()
	{
		using (IEnumerator<ActorTrait> enumerator = this.getTraits().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.group_id == "mind")
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000FC4 RID: 4036 RVA: 0x000C84E4 File Offset: 0x000C66E4
	public bool canBeSurprised(WorldTile pFromTile = null)
	{
		return this._has_emotions && this.asset.can_be_surprised && !this.isFighting() && !this.isInsideSomething() && !this.is_unconscious && !this.isEgg() && (!this.hasTask() || !this.ai.task.ignore_fight_check);
	}

	// Token: 0x1700009C RID: 156
	// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x000C854F File Offset: 0x000C674F
	public bool is_looking_left
	{
		get
		{
			return !this.flip;
		}
	}

	// Token: 0x06000FC6 RID: 4038 RVA: 0x000C855A File Offset: 0x000C675A
	public bool isTileOnTheLeft(WorldTile pTile)
	{
		return this.current_tile.x > pTile.x;
	}

	// Token: 0x06000FC7 RID: 4039 RVA: 0x000C856F File Offset: 0x000C676F
	public bool isFighting()
	{
		return this.has_attack_target;
	}

	// Token: 0x06000FC8 RID: 4040 RVA: 0x000C857C File Offset: 0x000C677C
	public UnitProfession getProfession()
	{
		return this._profession;
	}

	// Token: 0x06000FC9 RID: 4041 RVA: 0x000C8584 File Offset: 0x000C6784
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int getNutrition()
	{
		return this.data.nutrition;
	}

	// Token: 0x06000FCA RID: 4042 RVA: 0x000C8591 File Offset: 0x000C6791
	public bool isHungry()
	{
		return this.needsFood() && this.getNutritionRatio() <= SimGlobals.m.nutrition_level_hungry;
	}

	// Token: 0x06000FCB RID: 4043 RVA: 0x000C85B4 File Offset: 0x000C67B4
	public float getNutritionRatio()
	{
		float num = (float)this.getNutrition();
		float tMaxHunger = (float)this.getMaxNutrition();
		return num / tMaxHunger;
	}

	// Token: 0x06000FCC RID: 4044 RVA: 0x000C85D4 File Offset: 0x000C67D4
	public float getHealthRatio()
	{
		float num = (float)base.getHealth();
		float tMaxHealth = (float)base.getMaxHealth();
		return num / tMaxHealth;
	}

	// Token: 0x06000FCD RID: 4045 RVA: 0x000C85F2 File Offset: 0x000C67F2
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasMaxHealth()
	{
		return base.getHealth() >= base.getMaxHealth();
	}

	// Token: 0x06000FCE RID: 4046 RVA: 0x000C8605 File Offset: 0x000C6805
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasMaxMana()
	{
		return this.getMana() >= this.getMaxMana();
	}

	// Token: 0x06000FCF RID: 4047 RVA: 0x000C8618 File Offset: 0x000C6818
	public bool isStarving()
	{
		return this.getNutrition() == 0;
	}

	// Token: 0x06000FD0 RID: 4048 RVA: 0x000C8623 File Offset: 0x000C6823
	public bool hasFavoriteFood()
	{
		return !string.IsNullOrEmpty(this.data.favorite_food);
	}

	// Token: 0x1700009D RID: 157
	// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x000C8638 File Offset: 0x000C6838
	public ResourceAsset favorite_food_asset
	{
		get
		{
			return AssetManager.resources.get(this.data.favorite_food);
		}
	}

	// Token: 0x06000FD2 RID: 4050 RVA: 0x000C864F File Offset: 0x000C684F
	public bool hasEmotions()
	{
		return this._has_emotions;
	}

	// Token: 0x06000FD3 RID: 4051 RVA: 0x000C8657 File Offset: 0x000C6857
	public bool canHavePrejudice()
	{
		return this.hasEmotions();
	}

	// Token: 0x06000FD4 RID: 4052 RVA: 0x000C865F File Offset: 0x000C685F
	public bool hasHappinessHistory()
	{
		return this._last_happiness_history.Count > 0;
	}

	// Token: 0x06000FD5 RID: 4053 RVA: 0x000C866F File Offset: 0x000C686F
	public bool isUnhappy()
	{
		return this.hasEmotions() && this.getHappinessRatio() < 0.3f;
	}

	// Token: 0x06000FD6 RID: 4054 RVA: 0x000C8688 File Offset: 0x000C6888
	public int getHappiness()
	{
		return this.data.happiness;
	}

	// Token: 0x06000FD7 RID: 4055 RVA: 0x000C8695 File Offset: 0x000C6895
	public bool isHappy()
	{
		return !this.hasEmotions() || this.getHappinessRatio() >= 0.6f;
	}

	// Token: 0x06000FD8 RID: 4056 RVA: 0x000C86B1 File Offset: 0x000C68B1
	public int getMinHappiness()
	{
		return -100;
	}

	// Token: 0x06000FD9 RID: 4057 RVA: 0x000C86B5 File Offset: 0x000C68B5
	public int getMaxHappiness()
	{
		return 100;
	}

	// Token: 0x06000FDA RID: 4058 RVA: 0x000C86B9 File Offset: 0x000C68B9
	public float getHappinessRatio()
	{
		return ((float)this.getHappiness() + 100f) / 200f;
	}

	// Token: 0x06000FDB RID: 4059 RVA: 0x000C86CE File Offset: 0x000C68CE
	internal bool isSameSpecies(string pID)
	{
		return this.asset.id == pID;
	}

	// Token: 0x06000FDC RID: 4060 RVA: 0x000C86E1 File Offset: 0x000C68E1
	internal bool isSameSpecies(Actor pActor)
	{
		return this.isSameSpecies(pActor.asset.id);
	}

	// Token: 0x06000FDD RID: 4061 RVA: 0x000C86F4 File Offset: 0x000C68F4
	internal bool isSameSubspecies(Subspecies pSubspecies)
	{
		return this.subspecies == pSubspecies;
	}

	// Token: 0x06000FDE RID: 4062 RVA: 0x000C8700 File Offset: 0x000C6900
	public bool isAllowedToLookForEnemies()
	{
		return !this.shouldSkipFightCheck() && (!this.hasTask() || !this.ai.task.ignore_fight_check) && !this._has_trait_peaceful && !this.isInsideSomething() && (this.kingdom.asset.units_always_looking_for_enemies || !this.isBaby());
	}

	// Token: 0x06000FDF RID: 4063 RVA: 0x000C8766 File Offset: 0x000C6966
	private bool shouldSkipFightCheck()
	{
		return this.asset.skip_fight_logic || this._ignore_fights || (this.asset.is_boat && this.getSimpleComponent<Boat>().hasPassengers());
	}

	// Token: 0x06000FE0 RID: 4064 RVA: 0x000C879E File Offset: 0x000C699E
	public bool isInWaterAndCantAttack()
	{
		return !this.isWaterCreature() && this.isAffectedByLiquid();
	}

	// Token: 0x06000FE1 RID: 4065 RVA: 0x000C87B0 File Offset: 0x000C69B0
	public bool hasReachedOffspringLimit()
	{
		int tMaxOffspring = this.getMaxOffspring();
		return this.current_children_count >= tMaxOffspring;
	}

	// Token: 0x06000FE2 RID: 4066 RVA: 0x000C87D0 File Offset: 0x000C69D0
	public int getMaxOffspring()
	{
		return (int)Math.Ceiling((double)this.stats["offspring"]);
	}

	// Token: 0x06000FE3 RID: 4067 RVA: 0x000C87E9 File Offset: 0x000C69E9
	public bool haveNutritionForNewBaby()
	{
		return !this.needsFood() || this.getNutrition() >= SimGlobals.m.nutrition_cost_new_baby;
	}

	// Token: 0x06000FE4 RID: 4068 RVA: 0x000C8808 File Offset: 0x000C6A08
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isInsideSomething()
	{
		return this.is_inside_building || this.is_inside_boat;
	}

	// Token: 0x06000FE5 RID: 4069 RVA: 0x000C881D File Offset: 0x000C6A1D
	public bool isOnSameIsland(Actor pActor)
	{
		return this.current_tile.isSameIsland(pActor.current_tile);
	}

	// Token: 0x06000FE6 RID: 4070 RVA: 0x000C8830 File Offset: 0x000C6A30
	public bool hasSameCity(Actor pActorTarget)
	{
		return base.hasCity() && this.city == pActorTarget.city;
	}

	// Token: 0x06000FE7 RID: 4071 RVA: 0x000C884A File Offset: 0x000C6A4A
	public bool canBreed()
	{
		return base.isAlive() && this.isBreedingAge() && this.haveNutritionForNewBaby() && !base.hasStatus("pregnant") && !base.hasStatus("afterglow");
	}

	// Token: 0x06000FE8 RID: 4072 RVA: 0x000C8889 File Offset: 0x000C6A89
	public bool canProduceBabies()
	{
		return !this.hasTrait("infertile");
	}

	// Token: 0x06000FE9 RID: 4073 RVA: 0x000C889C File Offset: 0x000C6A9C
	public bool isPlacePrivateForBreeding()
	{
		int tUnitsInChunk = Toolbox.countUnitsInChunk(this.current_tile);
		if (base.hasCity())
		{
			int tEdge = this.city.getPopulationMaximum() * 2 + 10;
			return this.city.countUnits() < tEdge;
		}
		return this.asset.animal_breeding_close_units_limit > tUnitsInChunk;
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x000C88EB File Offset: 0x000C6AEB
	public bool isOnGround()
	{
		if (this.is_immovable || this.is_unconscious)
		{
			return true;
		}
		if (this.hasTask())
		{
			BehaviourActionActor action = this.ai.action;
			return action != null && action.land_if_hovering;
		}
		return false;
	}

	// Token: 0x06000FEB RID: 4075 RVA: 0x000C8920 File Offset: 0x000C6B20
	internal bool isInAttackRange(BaseSimObject pObject)
	{
		float tRange = this.getAttackRange() + pObject.stats["size"];
		tRange *= tRange;
		return Toolbox.SquaredDistVec2Float(this.current_position, pObject.current_position) < tRange;
	}

	// Token: 0x06000FEC RID: 4076 RVA: 0x000C895D File Offset: 0x000C6B5D
	internal bool isAttackReady()
	{
		return this.attack_timer <= 0f;
	}

	// Token: 0x06000FED RID: 4077 RVA: 0x000C8970 File Offset: 0x000C6B70
	public float getAttackCooldownRatio()
	{
		float tCooldown = this.getAttackCooldown();
		if (tCooldown == 0f)
		{
			return 1f;
		}
		return this.attack_timer / tCooldown;
	}

	// Token: 0x06000FEE RID: 4078 RVA: 0x000C899A File Offset: 0x000C6B9A
	internal bool isAttackPossible()
	{
		return this.isAttackReady() && this.current_rotation.z == 0f;
	}

	// Token: 0x06000FEF RID: 4079 RVA: 0x000C89BB File Offset: 0x000C6BBB
	public bool canUseSpells()
	{
		return !base.hasStatus("spell_silence") && !this.hasSpellCastCooldownStatus();
	}

	// Token: 0x06000FF0 RID: 4080 RVA: 0x000C89D8 File Offset: 0x000C6BD8
	public bool hasSpells()
	{
		return this._spells.hasAny() || (this.hasSubspecies() && this.subspecies.spells.hasAny()) || (this.canUseReligionSpells() && this.religion.spells.hasAny()) || this.asset.hasDefaultSpells();
	}

	// Token: 0x06000FF1 RID: 4081 RVA: 0x000C8A38 File Offset: 0x000C6C38
	public bool canUseReligionSpells()
	{
		if (!this.hasReligion())
		{
			return false;
		}
		if (!this.religion.spells.hasAny())
		{
			return false;
		}
		if (this.hasTrait("mute"))
		{
			return false;
		}
		if (this.hasClan())
		{
			return !this.clan.hasTrait("void_ban");
		}
		return !this.religion.is_magic_only_clan_members;
	}

	// Token: 0x06000FF2 RID: 4082 RVA: 0x000C8AA0 File Offset: 0x000C6CA0
	public SpellAsset getRandomSpell()
	{
		SpellAsset result;
		using (ListPool<SpellAsset> tSpellPool = new ListPool<SpellAsset>())
		{
			if (this._spells.hasAny())
			{
				tSpellPool.Add(this._spells.getRandomSpell());
			}
			if (this.hasSubspecies() && this.subspecies.spells.hasAny())
			{
				tSpellPool.Add(this.subspecies.spells.getRandomSpell());
			}
			if (this.canUseReligionSpells())
			{
				tSpellPool.Add(this.religion.spells.getRandomSpell());
			}
			if (this.asset.hasDefaultSpells())
			{
				tSpellPool.Add(this.asset.spells.getRandomSpell());
			}
			if (tSpellPool.Count == 0)
			{
				result = null;
			}
			else
			{
				result = tSpellPool.GetRandom<SpellAsset>();
			}
		}
		return result;
	}

	// Token: 0x06000FF3 RID: 4083 RVA: 0x000C8B74 File Offset: 0x000C6D74
	internal override float getHeight()
	{
		return this.position_height + this.hitbox_bonus_height;
	}

	// Token: 0x06000FF4 RID: 4084 RVA: 0x000C8B83 File Offset: 0x000C6D83
	public float getScaleMod()
	{
		return this.actor_scale / 0.1f;
	}

	// Token: 0x06000FF5 RID: 4085 RVA: 0x000C8B91 File Offset: 0x000C6D91
	public bool isCameraFollowingUnit()
	{
		return MoveCamera.isCameraFollowingUnit(this);
	}

	// Token: 0x06000FF6 RID: 4086 RVA: 0x000C8B99 File Offset: 0x000C6D99
	internal bool isTargetOkToAttack(Actor pTarget)
	{
		return pTarget != this && base.canAttackTarget(pTarget, true, true) && base.isSameIslandAs(pTarget);
	}

	// Token: 0x06000FF7 RID: 4087 RVA: 0x000C8BBA File Offset: 0x000C6DBA
	private float getLastColorEffectTime()
	{
		return World.world.getRealTimeElapsedSince(this._last_color_effect_timestamp);
	}

	// Token: 0x06000FF8 RID: 4088 RVA: 0x000C8BCC File Offset: 0x000C6DCC
	private float getLastStaminaReduceTime()
	{
		return World.world.getRealTimeElapsedSince(this._last_stamina_reduce_timestamp);
	}

	// Token: 0x06000FF9 RID: 4089 RVA: 0x000C8BDE File Offset: 0x000C6DDE
	public bool isUnderDamageCooldown()
	{
		return this.getLastColorEffectTime() < 0.3f;
	}

	// Token: 0x06000FFA RID: 4090 RVA: 0x000C8BED File Offset: 0x000C6DED
	private bool isUnderStaminaCooldown()
	{
		return this.getLastStaminaReduceTime() < 0.3f;
	}

	// Token: 0x06000FFB RID: 4091 RVA: 0x000C8BFC File Offset: 0x000C6DFC
	private bool haveMetallicArmor()
	{
		return false;
	}

	// Token: 0x06000FFC RID: 4092 RVA: 0x000C8BFF File Offset: 0x000C6DFF
	private bool haveMetallicWeapon()
	{
		return this.hasEquipment() && !this.equipment.getSlot(EquipmentType.Weapon).isEmpty() && this.equipment.getSlot(EquipmentType.Weapon).getItem().getAsset().metallic;
	}

	// Token: 0x06000FFD RID: 4093 RVA: 0x000C8C3B File Offset: 0x000C6E3B
	public bool isSameKingdomAndAlmostDead(Actor pActor, float pDamage)
	{
		return this.isSameKingdom(pActor) && (float)base.getHealth() - pDamage <= 0f;
	}

	// Token: 0x06000FFE RID: 4094 RVA: 0x000C8C59 File Offset: 0x000C6E59
	public bool isSameKingdom(BaseSimObject pSimObject)
	{
		return this.kingdom == pSimObject.kingdom;
	}

	// Token: 0x06000FFF RID: 4095 RVA: 0x000C8C6C File Offset: 0x000C6E6C
	public bool isInCityIsland()
	{
		if (this.city.isRekt())
		{
			return false;
		}
		WorldTile tCityTile = this.city.getTile(false);
		return tCityTile != null && this.current_tile.isSameIsland(tCityTile);
	}

	// Token: 0x06001000 RID: 4096 RVA: 0x000C8CAB File Offset: 0x000C6EAB
	public bool isClone()
	{
		return this._has_trait_clone;
	}

	// Token: 0x06001001 RID: 4097 RVA: 0x000C8CB3 File Offset: 0x000C6EB3
	public bool isClonedFrom(Actor pActor)
	{
		return this.isClone() && this.data.parent_id_1 == pActor.data.id;
	}

	// Token: 0x06001002 RID: 4098 RVA: 0x000C8CDA File Offset: 0x000C6EDA
	public bool isSameClones(Actor pActor)
	{
		return this.isClone() && pActor.isClone() && this.data.parent_id_1 == pActor.data.parent_id_1;
	}

	// Token: 0x06001003 RID: 4099 RVA: 0x000C8D0B File Offset: 0x000C6F0B
	public bool isUnitFitToRule()
	{
		return base.isAlive() && base.isKingdomCiv();
	}

	// Token: 0x06001004 RID: 4100 RVA: 0x000C8D22 File Offset: 0x000C6F22
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(Actor pObject)
	{
		return this.GetHashCode() == pObject.GetHashCode();
	}

	// Token: 0x06001005 RID: 4101 RVA: 0x000C8D34 File Offset: 0x000C6F34
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int CompareTo(Actor pTarget)
	{
		return this.GetHashCode().CompareTo(pTarget.GetHashCode());
	}

	// Token: 0x06001006 RID: 4102 RVA: 0x000C8D58 File Offset: 0x000C6F58
	public bool canTalkWith(Actor pTarget)
	{
		return this != pTarget && pTarget.isReadyToTalk() && base.isSameIslandAs(pTarget) && !base.areFoes(pTarget) && !this.isInsideSomething() && !pTarget.asset.special;
	}

	// Token: 0x06001007 RID: 4103 RVA: 0x000C8DA8 File Offset: 0x000C6FA8
	public bool canFallInLoveWith(Actor pTarget)
	{
		return !this.hasLover() && this.isAdult() && this.isBreedingAge() && this.subspecies.needs_mate && pTarget.subspecies.needs_mate && this.isSameSpecies(pTarget) && this.subspecies.isPartnerSuitableForReproduction(this, pTarget) && !pTarget.hasLover() && pTarget.isAdult() && pTarget.isBreedingAge() && !this.isRelatedTo(pTarget);
	}

	// Token: 0x06001008 RID: 4104 RVA: 0x000C8E37 File Offset: 0x000C7037
	public bool hasHouseCityInBordersAndSameIsland()
	{
		return base.hasCity() && this.hasHouse() && this.inOwnCityBorders() && this.inOwnHouseIsland();
	}

	// Token: 0x06001009 RID: 4105 RVA: 0x000C8E5C File Offset: 0x000C705C
	public bool inOwnHouseIsland()
	{
		Building tHouse = this.getHomeBuilding();
		return !tHouse.isRekt() && this.current_tile.isSameIsland(tHouse.current_tile);
	}

	// Token: 0x0600100A RID: 4106 RVA: 0x000C8E8B File Offset: 0x000C708B
	public bool inOwnCityBorders()
	{
		return base.hasCity() && base.current_zone.isSameCityHere(this.city);
	}

	// Token: 0x0600100B RID: 4107 RVA: 0x000C8EA8 File Offset: 0x000C70A8
	public bool inOwnCityIsland()
	{
		if (!base.hasCity())
		{
			return false;
		}
		WorldTile tCityTile = this.city.getTile(false);
		return tCityTile != null && this.current_tile.isSameIsland(tCityTile);
	}

	// Token: 0x0600100C RID: 4108 RVA: 0x000C8EDD File Offset: 0x000C70DD
	public bool isReadyToTalk()
	{
		return base.isAlive() && this.canSocialize() && (!this.hasTask() || this.ai.task.cancellable_by_socialize);
	}

	// Token: 0x0600100D RID: 4109 RVA: 0x000C8F10 File Offset: 0x000C7110
	public bool canSocialize()
	{
		return !this.asset.unit_zombie && !this.isEgg() && !this.isFighting() && !base.hasStatus("recovery_social") && this.hasSubspecies();
	}

	// Token: 0x0600100E RID: 4110 RVA: 0x000C8F50 File Offset: 0x000C7150
	public int getConstructionSpeed()
	{
		int tResult = 2;
		if (this.hasSubspecies())
		{
			tResult += (int)this.subspecies.base_stats_meta["construction_speed"];
		}
		return tResult;
	}

	// Token: 0x0600100F RID: 4111 RVA: 0x000C8F81 File Offset: 0x000C7181
	private bool combatActionOnTimeout()
	{
		return base.hasStatus("recovery_combat_action");
	}

	// Token: 0x06001010 RID: 4112 RVA: 0x000C8F8E File Offset: 0x000C718E
	private bool hasSpellCastCooldownStatus()
	{
		return base.hasStatus("recovery_spell");
	}

	// Token: 0x06001011 RID: 4113 RVA: 0x000C8F9B File Offset: 0x000C719B
	public bool hasEnoughMana(int pCostMana)
	{
		return pCostMana == 0 || this.getMana() >= pCostMana;
	}

	// Token: 0x06001012 RID: 4114 RVA: 0x000C8FAE File Offset: 0x000C71AE
	public int getMana()
	{
		return this.data.mana;
	}

	// Token: 0x06001013 RID: 4115 RVA: 0x000C8FBB File Offset: 0x000C71BB
	public int getMaxMana()
	{
		return (int)this.stats["mana"];
	}

	// Token: 0x06001014 RID: 4116 RVA: 0x000C8FCE File Offset: 0x000C71CE
	public void setMaxMana()
	{
		this.setMana(this.getMaxMana(), true);
	}

	// Token: 0x06001015 RID: 4117 RVA: 0x000C8FDD File Offset: 0x000C71DD
	public bool isManaFull()
	{
		return this.getMana() == this.getMaxMana();
	}

	// Token: 0x06001016 RID: 4118 RVA: 0x000C8FED File Offset: 0x000C71ED
	public bool hasEnoughStamina(int pCostStamina)
	{
		return pCostStamina == 0 || this.getStamina() >= pCostStamina;
	}

	// Token: 0x06001017 RID: 4119 RVA: 0x000C9000 File Offset: 0x000C7200
	public int getStamina()
	{
		return this.data.stamina;
	}

	// Token: 0x06001018 RID: 4120 RVA: 0x000C900D File Offset: 0x000C720D
	public int getMaxStamina()
	{
		return (int)this.stats["stamina"];
	}

	// Token: 0x06001019 RID: 4121 RVA: 0x000C9020 File Offset: 0x000C7220
	public void setMaxStamina()
	{
		this.setStamina(this.getMaxStamina(), true);
	}

	// Token: 0x0600101A RID: 4122 RVA: 0x000C902F File Offset: 0x000C722F
	public bool isStaminaFull()
	{
		return this.getStamina() == this.getMaxStamina();
	}

	// Token: 0x1700009E RID: 158
	// (get) Token: 0x0600101B RID: 4123 RVA: 0x000C903F File Offset: 0x000C723F
	public int current_children_count
	{
		get
		{
			return this._current_children;
		}
	}

	// Token: 0x0600101C RID: 4124 RVA: 0x000C9047 File Offset: 0x000C7247
	public bool isWarrior()
	{
		return this.profession_asset.profession_id == UnitProfession.Warrior;
	}

	// Token: 0x0600101D RID: 4125 RVA: 0x000C9057 File Offset: 0x000C7257
	public bool isCarnivore()
	{
		return this.hasSubspecies() && this.subspecies.diet_meat;
	}

	// Token: 0x0600101E RID: 4126 RVA: 0x000C9071 File Offset: 0x000C7271
	public bool isHerbivore()
	{
		return this.hasSubspecies() && this.subspecies.diet_vegetation;
	}

	// Token: 0x0600101F RID: 4127 RVA: 0x000C908B File Offset: 0x000C728B
	public bool hasStatusStunned()
	{
		return base.hasStatus("stunned");
	}

	// Token: 0x1700009F RID: 159
	// (get) Token: 0x06001020 RID: 4128 RVA: 0x000C9098 File Offset: 0x000C7298
	public bool is_unconscious
	{
		get
		{
			return this._has_tag_unconscious;
		}
	}

	// Token: 0x06001021 RID: 4129 RVA: 0x000C90A0 File Offset: 0x000C72A0
	public bool isLying()
	{
		return this.is_unconscious || this._has_status_sleeping;
	}

	// Token: 0x06001022 RID: 4130 RVA: 0x000C90B2 File Offset: 0x000C72B2
	public override bool hasStatusTantrum()
	{
		return this._has_status_tantrum;
	}

	// Token: 0x06001023 RID: 4131 RVA: 0x000C90BA File Offset: 0x000C72BA
	public bool hasAnyCash()
	{
		return this.money > 0 || this.loot > 0;
	}

	// Token: 0x170000A0 RID: 160
	// (get) Token: 0x06001024 RID: 4132 RVA: 0x000C90D0 File Offset: 0x000C72D0
	public int loot
	{
		get
		{
			return this.data.loot;
		}
	}

	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x06001025 RID: 4133 RVA: 0x000C90DD File Offset: 0x000C72DD
	public int money
	{
		get
		{
			return this.data.money;
		}
	}

	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x06001026 RID: 4134 RVA: 0x000C90EA File Offset: 0x000C72EA
	public int renown
	{
		get
		{
			return this.data.renown;
		}
	}

	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x06001027 RID: 4135 RVA: 0x000C90F7 File Offset: 0x000C72F7
	public int level
	{
		get
		{
			return this.data.level;
		}
	}

	// Token: 0x06001028 RID: 4136 RVA: 0x000C9104 File Offset: 0x000C7304
	public bool hasEnoughMoney(int pCost)
	{
		return this.money >= pCost;
	}

	// Token: 0x06001029 RID: 4137 RVA: 0x000C9114 File Offset: 0x000C7314
	public int getHappinessPercent()
	{
		int tMax = this.getMaxHappiness();
		int tMin = this.getMinHappiness();
		return Mathf.Clamp(Mathf.Clamp(this.getHappiness() - tMin, 0, tMax - tMin) * 100 / (tMax - tMin), 0, 100);
	}

	// Token: 0x0600102A RID: 4138 RVA: 0x000C914F File Offset: 0x000C734F
	public float distanceToObjectTarget(BaseSimObject pBaseSimObject)
	{
		return Toolbox.DistVec2Float(this.current_position, pBaseSimObject.current_position);
	}

	// Token: 0x0600102B RID: 4139 RVA: 0x000C9162 File Offset: 0x000C7362
	public float distanceToActorTile(Actor pActor)
	{
		return this.distanceToActorTile(pActor.current_tile);
	}

	// Token: 0x0600102C RID: 4140 RVA: 0x000C9170 File Offset: 0x000C7370
	public float distanceToActorTile(WorldTile pTile)
	{
		return this.current_tile.distanceTo(pTile);
	}

	// Token: 0x0600102D RID: 4141 RVA: 0x000C9180 File Offset: 0x000C7380
	public bool isRelatedTo(Actor pTarget)
	{
		return (this.hasFamily() && pTarget.hasFamily() && this.isSapient() && this.family == pTarget.family) || this.isChildOf(pTarget) || this.isParentOf(pTarget);
	}

	// Token: 0x0600102E RID: 4142 RVA: 0x000C91CC File Offset: 0x000C73CC
	public bool isImportantTo(Actor pTarget)
	{
		return (this.hasLover() && this.lover == pTarget) || (this.hasBestFriend() && this.getBestFriend() == pTarget);
	}

	// Token: 0x0600102F RID: 4143 RVA: 0x000C91F8 File Offset: 0x000C73F8
	public bool canWork()
	{
		if (this.isAdult())
		{
			return true;
		}
		if (this.hasCulture())
		{
			Culture tCulture = this.culture;
			if (tCulture.hasTrait("tiny_legends"))
			{
				return true;
			}
			if (tCulture.hasTrait("youth_reverence"))
			{
				return false;
			}
		}
		return this.getAge() >= SimGlobals.m.child_work_age;
	}

	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x06001030 RID: 4144 RVA: 0x000C9251 File Offset: 0x000C7451
	public int intelligence
	{
		get
		{
			return (int)this.stats["intelligence"];
		}
	}

	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x06001031 RID: 4145 RVA: 0x000C9264 File Offset: 0x000C7464
	public int diplomacy
	{
		get
		{
			return (int)this.stats["diplomacy"];
		}
	}

	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x06001032 RID: 4146 RVA: 0x000C9277 File Offset: 0x000C7477
	public int warfare
	{
		get
		{
			return (int)this.stats["warfare"];
		}
	}

	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x06001033 RID: 4147 RVA: 0x000C928A File Offset: 0x000C748A
	public int stewardship
	{
		get
		{
			return (int)this.stats["stewardship"];
		}
	}

	// Token: 0x06001034 RID: 4148 RVA: 0x000C929D File Offset: 0x000C749D
	public bool hasCultureTrait(string pTraitID)
	{
		return this.hasCulture() && this.culture.hasTrait(pTraitID);
	}

	// Token: 0x06001035 RID: 4149 RVA: 0x000C92B5 File Offset: 0x000C74B5
	public bool canBePossessed()
	{
		return this.asset.allow_possession;
	}

	// Token: 0x06001036 RID: 4150 RVA: 0x000C92C2 File Offset: 0x000C74C2
	public float getAttackRange()
	{
		return this.stats["range"];
	}

	// Token: 0x06001037 RID: 4151 RVA: 0x000C92D4 File Offset: 0x000C74D4
	public float getAttackRangeSquared()
	{
		return this.stats["range"] * this.stats["range"];
	}

	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x06001038 RID: 4152 RVA: 0x000C92F7 File Offset: 0x000C74F7
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Unit;
		}
	}

	// Token: 0x06001039 RID: 4153 RVA: 0x000C92FC File Offset: 0x000C74FC
	public float getStaminaRatio()
	{
		float tMax = (float)this.getMaxStamina();
		if (tMax == 0f)
		{
			return 0f;
		}
		return (float)this.getStamina() / tMax;
	}

	// Token: 0x0600103A RID: 4154 RVA: 0x000C9328 File Offset: 0x000C7528
	public float getManaRatio()
	{
		float tMax = (float)this.getMaxMana();
		if (tMax == 0f)
		{
			return 0f;
		}
		return (float)this.getMana() / tMax;
	}

	// Token: 0x0600103B RID: 4155 RVA: 0x000C9354 File Offset: 0x000C7554
	public bool canGetFoodFromCity()
	{
		return this.isFoodFreeForThisPerson() || this.money > SimGlobals.m.min_coins_before_city_food;
	}

	// Token: 0x0600103C RID: 4156 RVA: 0x000C9375 File Offset: 0x000C7575
	public bool isFoodFreeForThisPerson()
	{
		return this.isKing() || this.isCityLeader() || this.isBaby() || this.isStarving();
	}

	// Token: 0x0600103D RID: 4157 RVA: 0x000C93A0 File Offset: 0x000C75A0
	public int getMaxNutrition()
	{
		float tVal = (float)this.asset.nutrition_max;
		if (this.hasSubspecies())
		{
			tVal += this.subspecies.base_stats_meta["max_nutrition"];
		}
		return (int)tVal;
	}

	// Token: 0x0600103E RID: 4158 RVA: 0x000C93DC File Offset: 0x000C75DC
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int getExpToLevelup()
	{
		return 100 + (this.data.level - 1) * 20;
	}

	// Token: 0x0600103F RID: 4159 RVA: 0x000C93F4 File Offset: 0x000C75F4
	private bool calculateIsSick()
	{
		return this.hasTrait("infected") || this.hasTrait("plague") || (this.hasTrait("mush_spores") && this.asset.can_turn_into_mush) || (this.hasTrait("tumor_infection") && this.asset.can_turn_into_tumor);
	}

	// Token: 0x06001040 RID: 4160 RVA: 0x000C9458 File Offset: 0x000C7658
	public bool isSick()
	{
		return this._has_any_sick_trait;
	}

	// Token: 0x06001041 RID: 4161 RVA: 0x000C9460 File Offset: 0x000C7660
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool canTakeItems()
	{
		return this.asset.take_items;
	}

	// Token: 0x06001042 RID: 4162 RVA: 0x000C946D File Offset: 0x000C766D
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool understandsHowToUseItems()
	{
		return this.canUseItems() && this.isSapient();
	}

	// Token: 0x06001043 RID: 4163 RVA: 0x000C9484 File Offset: 0x000C7684
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool canUseItems()
	{
		return this.asset.use_items;
	}

	// Token: 0x06001044 RID: 4164 RVA: 0x000C9491 File Offset: 0x000C7691
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool canEditEquipment()
	{
		return this.asset.use_items;
	}

	// Token: 0x06001045 RID: 4165 RVA: 0x000C949E File Offset: 0x000C769E
	public bool canTurnIntoColdOne()
	{
		return !this.isAdult() && this.asset.can_turn_into_ice_one && this.asset.has_soul;
	}

	// Token: 0x06001046 RID: 4166 RVA: 0x000C94C9 File Offset: 0x000C76C9
	public bool canTurnIntoDemon()
	{
		return !this.isBaby() && this.asset.can_turn_into_demon_in_age_of_chaos;
	}

	// Token: 0x06001047 RID: 4167 RVA: 0x000C94E0 File Offset: 0x000C76E0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override BaseObjectData getData()
	{
		return this.data;
	}

	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x06001048 RID: 4168 RVA: 0x000C94E8 File Offset: 0x000C76E8
	public bool is_moving
	{
		get
		{
			return this._is_moving || this._possessed_movement;
		}
	}

	// Token: 0x06001049 RID: 4169 RVA: 0x000C94FA File Offset: 0x000C76FA
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isCarryingResources()
	{
		return this.inventory.hasResources();
	}

	// Token: 0x0600104A RID: 4170 RVA: 0x000C9507 File Offset: 0x000C7707
	public bool needsFood()
	{
		return this.hasSubspecies() && this.subspecies.needs_food;
	}

	// Token: 0x0600104B RID: 4171 RVA: 0x000C951E File Offset: 0x000C771E
	public bool isDamagedByRain()
	{
		return this.hasSubspecies() && this.subspecies.is_damaged_by_water;
	}

	// Token: 0x0600104C RID: 4172 RVA: 0x000C9535 File Offset: 0x000C7735
	public bool isDamagedByOcean()
	{
		if (this.hasSubspecies())
		{
			return this.subspecies.is_damaged_by_water;
		}
		return this.asset.damaged_by_ocean;
	}

	// Token: 0x0600104D RID: 4173 RVA: 0x000C9558 File Offset: 0x000C7758
	public int getWaterDamage()
	{
		int tResult = (int)((float)base.getMaxHealth() * SimGlobals.m.water_damage_multiplier);
		if (tResult < 1)
		{
			tResult = 1;
		}
		return tResult;
	}

	// Token: 0x0600104E RID: 4174 RVA: 0x000C9580 File Offset: 0x000C7780
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasSubspeciesTrait(string pTraitID)
	{
		return this.hasSubspecies() && this.subspecies.hasTrait(pTraitID);
	}

	// Token: 0x0600104F RID: 4175 RVA: 0x000C9598 File Offset: 0x000C7798
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasSubspeciesMetaTag(string pTagID)
	{
		return this.hasSubspecies() && this.subspecies.hasMetaTag(pTagID);
	}

	// Token: 0x06001050 RID: 4176 RVA: 0x000C95B0 File Offset: 0x000C77B0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasTag(string pTag)
	{
		return this.stats.hasTag(pTag);
	}

	// Token: 0x06001051 RID: 4177 RVA: 0x000C95BE File Offset: 0x000C77BE
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isImmuneToFire()
	{
		return this.hasTag("immunity_fire");
	}

	// Token: 0x06001052 RID: 4178 RVA: 0x000C95CB File Offset: 0x000C77CB
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isImmuneToCold()
	{
		return this.hasTag("immunity_cold");
	}

	// Token: 0x06001053 RID: 4179 RVA: 0x000C95D8 File Offset: 0x000C77D8
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isImmovable()
	{
		return this.hasTag("immovable");
	}

	// Token: 0x06001054 RID: 4180 RVA: 0x000C95E5 File Offset: 0x000C77E5
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isAiFrozen()
	{
		return this.hasTag("frozen_ai");
	}

	// Token: 0x06001055 RID: 4181 RVA: 0x000C95F2 File Offset: 0x000C77F2
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isIgnoreFights()
	{
		return this.hasTag("ignore_fights");
	}

	// Token: 0x06001056 RID: 4182 RVA: 0x000C95FF File Offset: 0x000C77FF
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasStopIdleAnimation()
	{
		return (!this.hasSubspecies() || !this.subspecies.hasMetaTag("always_idle_animation")) && this.hasTag("stop_idle_animation");
	}

	// Token: 0x06001057 RID: 4183 RVA: 0x000C9628 File Offset: 0x000C7828
	public bool hasDivineScar()
	{
		return this.hasTrait("scar_of_divinity");
	}

	// Token: 0x06001058 RID: 4184 RVA: 0x000C9635 File Offset: 0x000C7835
	public bool hasTelepathicLink()
	{
		return this.hasSubspecies() && this.subspecies.hasTrait("telepathic_link");
	}

	// Token: 0x06001059 RID: 4185 RVA: 0x000C9651 File Offset: 0x000C7851
	public float getResourceThrowDistance()
	{
		return this.asset.base_throwing_range + this.stats["throwing_range"];
	}

	// Token: 0x0600105A RID: 4186 RVA: 0x000C966F File Offset: 0x000C786F
	internal bool isFalling()
	{
		return this.position_height != 0f || this.move_jump_offset.y != 0f;
	}

	// Token: 0x0600105B RID: 4187 RVA: 0x000C9698 File Offset: 0x000C7898
	public float getAgeRatio()
	{
		float tLifespan = this.stats["lifespan"];
		return (float)this.getAge() / tLifespan;
	}

	// Token: 0x0600105C RID: 4188 RVA: 0x000C96C0 File Offset: 0x000C78C0
	public int getMassKG()
	{
		float tSizeDiff = this.target_scale / 0.1f;
		int tMassKG = (int)(this.stats["mass_2"] * tSizeDiff);
		if (this.isBaby())
		{
			tMassKG = (int)((float)tMassKG * SimGlobals.m.baby_mass_multiplier);
		}
		return tMassKG;
	}

	// Token: 0x0600105D RID: 4189 RVA: 0x000C9706 File Offset: 0x000C7906
	public IEnumerable<ResourceContainer> getResourcesFromActor()
	{
		if (this.asset.resources_given == null)
		{
			yield break;
		}
		int tMass = this.getMassKG();
		foreach (ResourceContainer tContainer in this.asset.resources_given)
		{
			ResourceAsset tResourceAsset = tContainer.asset;
			int tMultiplier = tMass / tResourceAsset.drop_per_mass + 1;
			int tAmount = tContainer.amount * tMultiplier;
			tAmount = Mathf.Clamp(tAmount, 1, tResourceAsset.drop_max);
			if (tAmount > 0)
			{
				yield return new ResourceContainer(tContainer.id, tAmount);
			}
		}
		List<ResourceContainer>.Enumerator enumerator = default(List<ResourceContainer>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x0600105E RID: 4190 RVA: 0x000C9716 File Offset: 0x000C7916
	public bool hasXenophobic()
	{
		return this.hasCulture() && this.culture.hasTrait("xenophobic");
	}

	// Token: 0x0600105F RID: 4191 RVA: 0x000C9732 File Offset: 0x000C7932
	public bool hasXenophiles()
	{
		return this.hasCulture() && this.culture.hasTrait("xenophiles");
	}

	// Token: 0x06001060 RID: 4192 RVA: 0x000C974E File Offset: 0x000C794E
	public bool hasCannibalism()
	{
		return this.hasSubspecies() && this.subspecies.hasCannibalism();
	}

	// Token: 0x06001061 RID: 4193 RVA: 0x000C9765 File Offset: 0x000C7965
	public bool isOneCityKingdom()
	{
		return base.hasCity() && this.city.kingdom.countCities() == 1;
	}

	// Token: 0x06001062 RID: 4194 RVA: 0x000C9785 File Offset: 0x000C7985
	public bool isImportantPerson()
	{
		return this.isKing() || this.isCityLeader() || this.isArmyGroupLeader() || this.isFavorite();
	}

	// Token: 0x06001063 RID: 4195 RVA: 0x000C97B0 File Offset: 0x000C79B0
	public bool canCurrentTaskBeCancelledByReproduction()
	{
		return !this.hasTask() || this.ai.task.cancellable_by_reproduction;
	}

	// Token: 0x06001064 RID: 4196 RVA: 0x000C97CC File Offset: 0x000C79CC
	public bool isAbleToSkipPriorityLevels()
	{
		return !this.isWarrior() || !base.hasCity() || !this.city.hasAttackZoneOrder();
	}

	// Token: 0x06001065 RID: 4197 RVA: 0x000C97EE File Offset: 0x000C79EE
	public void makeSpawnSound(bool pFromUI)
	{
		if (this.asset.has_sound_spawn)
		{
			if (pFromUI)
			{
				MusicBox.playSoundUI(this.asset.sound_spawn);
				return;
			}
			MusicBox.playSound(this.asset.sound_spawn, this.current_tile, false, false);
		}
	}

	// Token: 0x06001066 RID: 4198 RVA: 0x000C9829 File Offset: 0x000C7A29
	public void makeSoundAttack()
	{
		if (this.asset.has_sound_attack)
		{
			MusicBox.playSound(this.asset.sound_attack, this.current_tile, true, true);
		}
	}

	// Token: 0x06001067 RID: 4199 RVA: 0x000C9850 File Offset: 0x000C7A50
	public string getTaskText()
	{
		if (!this.hasTask())
		{
			return "???";
		}
		string localizedText = this.ai.task.getLocalizedText();
		string tTime = this.ai.getTaskTime();
		return localizedText + " " + tTime.ColorHex(ColorStyleLibrary.m.color_text_grey_dark, false);
	}

	// Token: 0x06001068 RID: 4200 RVA: 0x000C98A2 File Offset: 0x000C7AA2
	public void afterEvolutionEvents()
	{
		this.clearGraphicsFully();
		this.makeConfused(-1f, false);
		this.applyRandomForce(1.5f, 2f);
		this.increaseEvolutions();
	}

	// Token: 0x06001069 RID: 4201 RVA: 0x000C98CC File Offset: 0x000C7ACC
	public void generatePhenotypeAndShade()
	{
		this.data.phenotype_index = this.subspecies.getRandomPhenotypeIndex();
		this.data.phenotype_shade = Actor.getRandomPhenotypeShade();
	}

	// Token: 0x0600106A RID: 4202 RVA: 0x000C98F4 File Offset: 0x000C7AF4
	public static int getRandomPhenotypeShade()
	{
		return Randy.randomInt(0, 4);
	}

	// Token: 0x0600106B RID: 4203 RVA: 0x000C98FD File Offset: 0x000C7AFD
	public bool isRendered()
	{
		return this.current_tile.zone.visible;
	}

	// Token: 0x0600106C RID: 4204 RVA: 0x000C9910 File Offset: 0x000C7B10
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool checkHasRenderedItem()
	{
		return this.canUseItems() && !this._is_in_liquid && !this.isEgg() && (!this.equipment.weapon.isEmpty() || (this.hasTask() && this.ai.task.force_hand_tool != string.Empty) || this.isCarryingResources());
	}

	// Token: 0x0600106D RID: 4205 RVA: 0x000C997E File Offset: 0x000C7B7E
	internal Sprite getSpriteToRender()
	{
		return this.checkSpriteToRender();
	}

	// Token: 0x0600106E RID: 4206 RVA: 0x000C9986 File Offset: 0x000C7B86
	public bool hasColoredSprite()
	{
		return this.asset.need_colored_sprite;
	}

	// Token: 0x0600106F RID: 4207 RVA: 0x000C9993 File Offset: 0x000C7B93
	public bool isColoredSpriteNeedsCheck(Sprite pMainSprite)
	{
		return this._last_main_sprite != pMainSprite || this._last_color_asset != this.kingdom.getColor();
	}

	// Token: 0x06001070 RID: 4208 RVA: 0x000C99BC File Offset: 0x000C7BBC
	public Sprite calculateColoredSprite(Sprite pMainSprite, bool pUpdateFrameData = true)
	{
		if (this.isColoredSpriteNeedsCheck(pMainSprite))
		{
			if (this.animation_container != null && pUpdateFrameData)
			{
				this.animation_container.dict_frame_data.TryGetValue(pMainSprite.name, out this.frame_data);
			}
			this.checkSpriteHead();
			int tPhenotypeID = this.data.phenotype_index;
			int tPhenotypeShadeIndex = this.data.phenotype_shade;
			this._last_colored_sprite = DynamicSpriteCreator.getSpriteUnit(this.frame_data, pMainSprite, this, this.kingdom.getColor(), tPhenotypeID, tPhenotypeShadeIndex, this.asset.texture_atlas);
			this._last_main_sprite = pMainSprite;
			this._last_color_asset = this.kingdom.getColor();
		}
		return this._last_colored_sprite;
	}

	// Token: 0x06001071 RID: 4209 RVA: 0x000C9A66 File Offset: 0x000C7C66
	public Sprite getLastColoredSprite()
	{
		return this._last_colored_sprite;
	}

	// Token: 0x06001072 RID: 4210 RVA: 0x000C9A6E File Offset: 0x000C7C6E
	public bool canParallelSetColoredSprite()
	{
		return this.asset.has_avatar_prefab || !this.dirty_sprite_main;
	}

	// Token: 0x06001073 RID: 4211 RVA: 0x000C9A8C File Offset: 0x000C7C8C
	public Sprite calculateMainSprite()
	{
		if (this.asset.has_override_sprite)
		{
			return this.asset.get_override_sprite(this);
		}
		this.checkAnimationContainer();
		if (this.ai.action != null && this.ai.action.force_animation)
		{
			return this.animation_container.sprites[this.ai.action.force_animation_id];
		}
		Sprite tMainSprite;
		if (!base.isAlive() || this._has_stop_idle_animation)
		{
			if (this.animation_container.has_swimming && this._has_status_drowning)
			{
				tMainSprite = this.animation_container.swimming.frames[0];
			}
			else
			{
				tMainSprite = this.animation_container.idle.frames[0];
			}
			return tMainSprite;
		}
		float tAnimSpeed = this.asset.animation_walk_speed;
		bool tSpeedAffectsAnimSpeed = false;
		ActorAnimation tAnimation;
		if (this.is_moving || this.timer_jump_animation > 0f || this.move_jump_offset.y > 0f || this.is_in_magnet)
		{
			if (this.animation_container.has_swimming && this.isAffectedByLiquid())
			{
				tAnimation = this.animation_container.swimming;
				tAnimSpeed = this.asset.animation_swim_speed;
			}
			else
			{
				tAnimation = this.animation_container.walking;
			}
			tSpeedAffectsAnimSpeed = true;
		}
		else if (this.position_height > 0f)
		{
			tAnimation = this.animation_container.idle;
		}
		else if (this.animation_container.has_swimming && this.isAffectedByLiquid())
		{
			tAnimation = this.animation_container.swimming;
			tAnimSpeed = this.asset.animation_swim_speed;
			tSpeedAffectsAnimSpeed = true;
		}
		else
		{
			tAnimation = this.animation_container.idle;
			tAnimSpeed = this.asset.animation_idle_speed;
		}
		if (this.asset.animation_speed_based_on_walk_speed && tSpeedAffectsAnimSpeed)
		{
			tAnimSpeed *= this.stats["speed"] / 10f;
			tAnimSpeed = Mathf.Clamp(tAnimSpeed, 4f, tAnimSpeed);
		}
		if (tAnimation.frames.Length > 1)
		{
			tMainSprite = AnimationHelper.getSpriteFromList(this.GetHashCode(), tAnimation.frames, tAnimSpeed);
		}
		else
		{
			tMainSprite = tAnimation.frames[0];
		}
		return tMainSprite;
	}

	// Token: 0x06001074 RID: 4212 RVA: 0x000C9C90 File Offset: 0x000C7E90
	internal Sprite checkSpriteToRender()
	{
		Sprite tMainSprite = this.calculateMainSprite();
		if (!this.asset.need_colored_sprite)
		{
			return tMainSprite;
		}
		return this.calculateColoredSprite(tMainSprite, true);
	}

	// Token: 0x06001075 RID: 4213 RVA: 0x000C9CBB File Offset: 0x000C7EBB
	protected void setItemSpriteRenderDirty()
	{
		this._dirty_sprite_item = true;
	}

	// Token: 0x06001076 RID: 4214 RVA: 0x000C9CC4 File Offset: 0x000C7EC4
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Sprite getRenderedItemSprite()
	{
		if (this._dirty_sprite_item || this._has_animated_item)
		{
			this._cached_hand_renderer_asset = this.getHandRendererAsset();
			this._cached_sprite_item = ItemRendering.getItemMainSpriteFrame(this._cached_hand_renderer_asset);
			this._dirty_sprite_item = false;
		}
		return this._cached_sprite_item;
	}

	// Token: 0x06001077 RID: 4215 RVA: 0x000C9D00 File Offset: 0x000C7F00
	public IHandRenderer getCachedHandRendererAsset()
	{
		return this._cached_hand_renderer_asset;
	}

	// Token: 0x06001078 RID: 4216 RVA: 0x000C9D08 File Offset: 0x000C7F08
	public IHandRenderer getHandRendererAsset()
	{
		IHandRenderer tToolItem = this.getRenderedToolOrItem();
		if (tToolItem != null)
		{
			return tToolItem;
		}
		if (this.hasWeapon())
		{
			return this.getWeaponTextureId();
		}
		return null;
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x000C9D34 File Offset: 0x000C7F34
	private IHandRenderer getRenderedToolOrItem()
	{
		if (!this.asset.use_tool_items)
		{
			return null;
		}
		this._has_animated_item = false;
		if (this.has_attack_target && this.hasWeapon())
		{
			return null;
		}
		if (this.isCarryingResources())
		{
			return AssetManager.resources.get(this.inventory.getItemIDToRender());
		}
		if (this.hasTask())
		{
			UnitHandToolAsset tTaskTool = this.ai.task.cached_hand_tool_asset;
			if (tTaskTool != null)
			{
				this._has_animated_item = tTaskTool.animated;
				return tTaskTool;
			}
		}
		return null;
	}

	// Token: 0x0600107A RID: 4218 RVA: 0x000C9DB4 File Offset: 0x000C7FB4
	public bool isItemInHandAnimated()
	{
		if (this.isCarryingResources())
		{
			return false;
		}
		if (this.hasTask())
		{
			UnitHandToolAsset tTaskTool = this.ai.task.cached_hand_tool_asset;
			if (tTaskTool != null)
			{
				return tTaskTool.animated;
			}
		}
		return this.hasWeapon() && this.getWeapon().getAsset().animated;
	}

	// Token: 0x0600107B RID: 4219 RVA: 0x000C9E08 File Offset: 0x000C8008
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void clearSprites()
	{
		this.dirty_sprite_head = true;
		this.dirty_sprite_main = true;
	}

	// Token: 0x0600107C RID: 4220 RVA: 0x000C9E18 File Offset: 0x000C8018
	public void clearGraphicsFully()
	{
		this.clearSprites();
		this.clearLastColorCache();
		this.animation_container = null;
		this.frame_data = null;
		this.animation_container = null;
		this._last_main_sprite = null;
	}

	// Token: 0x0600107D RID: 4221 RVA: 0x000C9E42 File Offset: 0x000C8042
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public AnimationFrameData getAnimationFrameData()
	{
		return this.frame_data;
	}

	// Token: 0x0600107E RID: 4222 RVA: 0x000C9E4C File Offset: 0x000C804C
	public Vector3 getHeadOffsetPositionForFunRendering()
	{
		Vector3 tPos = new Vector3(this.cur_transform_position.x, this.cur_transform_position.y, 0f);
		AnimationFrameData tFrameData = this.getAnimationFrameData();
		if (tFrameData != null)
		{
			tPos.x += tFrameData.pos_head.x * this.current_scale.x;
			tPos.y += tFrameData.pos_head.y * this.current_scale.y;
		}
		return tPos;
	}

	// Token: 0x0600107F RID: 4223 RVA: 0x000C9ECC File Offset: 0x000C80CC
	public IHandRenderer getWeaponTextureId()
	{
		Item tWeapon = this.getWeapon();
		this._has_animated_item = tWeapon.getAsset().animated;
		return tWeapon.asset;
	}

	// Token: 0x06001080 RID: 4224 RVA: 0x000C9EF8 File Offset: 0x000C80F8
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private ActorTextureSubAsset getTextureAsset()
	{
		ActorTextureSubAsset tTextureAsset;
		if (this.hasSubspecies() && this.subspecies.has_mutation_reskin)
		{
			tTextureAsset = this.subspecies.mutation_skin_asset.texture_asset;
		}
		else
		{
			tTextureAsset = this.asset.texture_asset;
		}
		return tTextureAsset;
	}

	// Token: 0x06001081 RID: 4225 RVA: 0x000C9F3A File Offset: 0x000C813A
	public string getUnitTexturePath()
	{
		return this.getTextureAsset().getUnitTexturePath(this);
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x000C9F48 File Offset: 0x000C8148
	internal void checkAnimationContainer()
	{
		if (!this.dirty_sprite_main)
		{
			return;
		}
		this.dirty_sprite_main = false;
		string unitTexturePath = this.getUnitTexturePath();
		ActorAsset pAsset = this.asset;
		Subspecies subspecies = this.subspecies;
		SubspeciesTrait pEggAsset = (subspecies != null) ? subspecies.egg_asset : null;
		Subspecies subspecies2 = this.subspecies;
		AnimationContainerUnit tContainer = ActorAnimationLoader.getAnimationContainer(unitTexturePath, pAsset, pEggAsset, (subspecies2 != null) ? subspecies2.mutation_skin_asset : null);
		this.animation_container = tContainer;
	}

	// Token: 0x06001083 RID: 4227 RVA: 0x000C9FA2 File Offset: 0x000C81A2
	public SpriteAnimation getSpriteAnimation()
	{
		return this.sprite_animation;
	}

	// Token: 0x06001084 RID: 4228 RVA: 0x000C9FAC File Offset: 0x000C81AC
	public Vector2 getRenderedItemPosition()
	{
		AnimationFrameData tFrameData = this.getAnimationFrameData();
		if (tFrameData == null)
		{
			return Vector2.one;
		}
		return tFrameData.pos_item;
	}

	// Token: 0x06001085 RID: 4229 RVA: 0x000C9FCF File Offset: 0x000C81CF
	public void clearLastColorCache()
	{
		this._last_colored_sprite = null;
		this._last_color_asset = null;
		this.cached_sprite_head = null;
	}

	// Token: 0x06001086 RID: 4230 RVA: 0x000C9FE8 File Offset: 0x000C81E8
	public void startColorEffect(ActorColorEffect pColorType = ActorColorEffect.White)
	{
		if (!this.asset.effect_damage)
		{
			return;
		}
		if (!this.is_visible)
		{
			return;
		}
		if (this.isUnderDamageCooldown())
		{
			return;
		}
		this._last_color_effect_timestamp = World.world.getCurSessionTime();
		if (World.world.stack_effects.actor_effect_hit.Count > 1000)
		{
			return;
		}
		if (pColorType == ActorColorEffect.Red)
		{
			World.world.stack_effects.actor_effect_hit.Add(new ActorDamageEffectData
			{
				actor = this,
				timestamp = this._last_color_effect_timestamp
			});
			return;
		}
		World.world.stack_effects.actor_effect_highlight.Add(new ActorHighlightEffectData
		{
			actor = this,
			timestamp = this._last_color_effect_timestamp
		});
	}

	// Token: 0x06001087 RID: 4231 RVA: 0x000CA0AC File Offset: 0x000C82AC
	protected void checkSpriteHead()
	{
		if (!this.dirty_sprite_head)
		{
			return;
		}
		this.dirty_sprite_head = false;
		if (this.frame_data == null)
		{
			return;
		}
		if (!this.frame_data.show_head)
		{
			return;
		}
		if (this.animation_container.heads.Length == 0)
		{
			return;
		}
		if (this.isEgg())
		{
			return;
		}
		if (this.isBaby() && !this.animation_container.render_heads_for_children)
		{
			return;
		}
		ActorTextureSubAsset tTextureAsset = this.getTextureAsset();
		if (!tTextureAsset.has_advanced_textures)
		{
			this.checkHeadID(this.animation_container.heads, true);
			this.setHeadSprite(this.animation_container.heads[this.data.head]);
			return;
		}
		bool tSpecial = false;
		string tHeadPath;
		Sprite[] tListHeadsSprites;
		if (this.isSexMale())
		{
			tHeadPath = tTextureAsset.texture_heads_male;
			tListHeadsSprites = this.animation_container.heads_male;
		}
		else
		{
			tHeadPath = tTextureAsset.texture_heads_female;
			tListHeadsSprites = this.animation_container.heads_female;
		}
		if (this.isSapient())
		{
			if (this.is_profession_warrior && !this.equipment.helmet.isEmpty())
			{
				tHeadPath = tTextureAsset.texture_head_warrior;
				tSpecial = true;
			}
			else if (this.is_profession_king)
			{
				tHeadPath = tTextureAsset.texture_head_king;
				tSpecial = true;
			}
			else if (tTextureAsset.has_old_heads && this.hasTrait("wise"))
			{
				if (this.isSexMale())
				{
					tHeadPath = tTextureAsset.texture_heads_old_male;
				}
				else
				{
					tHeadPath = tTextureAsset.texture_heads_old_female;
				}
				tSpecial = true;
			}
			else if (this.isSexMale())
			{
				tHeadPath = tTextureAsset.texture_heads_male;
				tListHeadsSprites = this.animation_container.heads_male;
			}
			else
			{
				tHeadPath = tTextureAsset.texture_heads_female;
				tListHeadsSprites = this.animation_container.heads_female;
			}
		}
		if (tSpecial)
		{
			this.setHeadSprite(ActorAnimationLoader.getHeadSpecial(tHeadPath));
			return;
		}
		this.checkHeadID(tListHeadsSprites, true);
		this.setHeadSprite(ActorAnimationLoader.getHead(tHeadPath, this.data.head));
	}

	// Token: 0x06001088 RID: 4232 RVA: 0x000CA254 File Offset: 0x000C8454
	internal void checkHeadID(Sprite[] pListHeads, bool pCheckSavedHead = true)
	{
		if (pCheckSavedHead && this.data.head > pListHeads.Length - 1)
		{
			this.data.head = 0;
		}
		if (this.data.head == -1)
		{
			int tIndex = AnimationHelper.getSpriteIndex(this.data.id, pListHeads.Length);
			this.data.head = tIndex;
		}
	}

	// Token: 0x06001089 RID: 4233 RVA: 0x000CA2B0 File Offset: 0x000C84B0
	private void setHeadSprite(Sprite pSprite)
	{
		this.cached_sprite_head = pSprite;
	}

	// Token: 0x0600108A RID: 4234 RVA: 0x000CA2BC File Offset: 0x000C84BC
	protected void updateDeadAnimation(float pElapsed)
	{
		if (this.asset.special_dead_animation && this.asset.action_dead_animation(this, this.current_tile, pElapsed))
		{
			return;
		}
		if (World.world.quality_changer.isFullLowRes())
		{
			this.die(true, AttackType.None, false, true);
			return;
		}
		if (this.asset.death_animation_angle && !this._has_status_drowning && this.target_angle.z < 90f)
		{
			this.target_angle.z = Mathf.Lerp(this.target_angle.z, 90f, pElapsed * 4f);
			if (this.target_angle.z > 90f)
			{
				this.target_angle.z = 90f;
			}
			if (this.is_visible && Mathf.Abs(this.current_rotation.z) < 45f)
			{
				return;
			}
		}
		this.changeMoveJumpOffset(-0.05f);
		if (this.isFalling())
		{
			return;
		}
		this.updateDeadBlackAnimation(pElapsed);
	}

	// Token: 0x170000AA RID: 170
	// (get) Token: 0x0600108B RID: 4235 RVA: 0x000CA3BC File Offset: 0x000C85BC
	public bool has_rendered_sprite_head
	{
		get
		{
			return this.cached_sprite_head != null;
		}
	}

	// Token: 0x0600108C RID: 4236 RVA: 0x000CA3CA File Offset: 0x000C85CA
	public double[] getDecisionsCooldowns()
	{
		return this._decision_cooldowns;
	}

	// Token: 0x0600108D RID: 4237 RVA: 0x000CA3D2 File Offset: 0x000C85D2
	public bool[] getDecisionsDisabled()
	{
		return this._decision_disabled;
	}

	// Token: 0x0600108E RID: 4238 RVA: 0x000CA3DC File Offset: 0x000C85DC
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isDecisionOnCooldown(int pIndex, double pCooldown)
	{
		double tTimestamp = this._decision_cooldowns[pIndex];
		if (tTimestamp == 0.0)
		{
			return false;
		}
		if ((double)World.world.getWorldTimeElapsedSince(tTimestamp) > pCooldown)
		{
			this._decision_cooldowns[pIndex] = 0.0;
			return false;
		}
		return true;
	}

	// Token: 0x0600108F RID: 4239 RVA: 0x000CA424 File Offset: 0x000C8624
	public void setupRandomDecisionCooldowns()
	{
		double tWorldTime = World.world.getCurWorldTime();
		for (int i = 0; i < this.decisions_counter; i++)
		{
			DecisionAsset tDecisionAsset = this.decisions[i];
			if (tDecisionAsset.cooldown != 0)
			{
				double tFakeTimestamp = tWorldTime - (double)Randy.randomFloat(0f, (float)tDecisionAsset.cooldown * 0.5f);
				this._decision_cooldowns[tDecisionAsset.decision_index] = tFakeTimestamp;
			}
		}
		this.timer_action = Randy.randomFloat(1f, 5f);
	}

	// Token: 0x06001090 RID: 4240 RVA: 0x000CA49C File Offset: 0x000C869C
	public void setDecisionCooldown(DecisionAsset pAsset)
	{
		if (pAsset.cooldown == 0)
		{
			return;
		}
		this._decision_cooldowns[pAsset.decision_index] = World.world.getCurWorldTime();
	}

	// Token: 0x06001091 RID: 4241 RVA: 0x000CA4BE File Offset: 0x000C86BE
	public bool isDecisionEnabled(int pIndex)
	{
		return !this._decision_disabled[pIndex];
	}

	// Token: 0x06001092 RID: 4242 RVA: 0x000CA4CB File Offset: 0x000C86CB
	public bool switchDecisionState(int pIndex)
	{
		this._decision_disabled[pIndex] = !this._decision_disabled[pIndex];
		return this._decision_disabled[pIndex];
	}

	// Token: 0x06001093 RID: 4243 RVA: 0x000CA4E8 File Offset: 0x000C86E8
	public void setDecisionState(int pIndex, bool pState)
	{
		this._decision_disabled[pIndex] = !pState;
	}

	// Token: 0x06001094 RID: 4244 RVA: 0x000CA4F6 File Offset: 0x000C86F6
	public void setTask(string pTaskId, bool pClean = true, bool pCleanJob = false, bool pForceAction = false)
	{
		this.ai.setTask(pTaskId, pClean, pCleanJob, pForceAction);
	}

	// Token: 0x06001095 RID: 4245 RVA: 0x000CA508 File Offset: 0x000C8708
	public void cancelAllBeh()
	{
		this.ai.clearBeh();
		this.ai.setTaskBehFinished();
		this.endJob();
		this.clearTasks();
	}

	// Token: 0x06001096 RID: 4246 RVA: 0x000CA52C File Offset: 0x000C872C
	public void endJob()
	{
		this.ai.clearJob();
		this.citizen_job = null;
	}

	// Token: 0x06001097 RID: 4247 RVA: 0x000CA540 File Offset: 0x000C8740
	protected virtual void clearTasks()
	{
		this.exitBuilding();
		this.clearAttackTarget();
		this.timer_action = 0f;
		this.clearTileTarget();
		this.stopMovement();
	}

	// Token: 0x06001098 RID: 4248 RVA: 0x000CA565 File Offset: 0x000C8765
	public void setCitizenJob(CitizenJobAsset pJobAsset)
	{
		this.citizen_job = pJobAsset;
		this.ai.setJob(pJobAsset.unit_job_default);
	}

	// Token: 0x06001099 RID: 4249 RVA: 0x000CA57F File Offset: 0x000C877F
	internal void clearBeh()
	{
		this.clearTasks();
		this.beh_tile_target = null;
		this.beh_building_target = null;
		this.beh_actor_target = null;
		this.beh_book_target = null;
	}

	// Token: 0x0600109A RID: 4250 RVA: 0x000CA5A3 File Offset: 0x000C87A3
	public string getNextJob()
	{
		return Actor.nextJobActor(this.a);
	}

	// Token: 0x0600109B RID: 4251 RVA: 0x000CA5B0 File Offset: 0x000C87B0
	public static string nextJobActor(Actor pActor)
	{
		if (pActor.isEgg())
		{
			return "egg";
		}
		string tNewJobID = null;
		if (pActor.isSapient())
		{
			if (pActor.isBaby())
			{
				tNewJobID = pActor.asset.job_baby.GetRandom<string>();
			}
			else if (pActor.hasCity())
			{
				if (pActor.isProfession(UnitProfession.Warrior))
				{
					tNewJobID = pActor.asset.job_attacker.GetRandom<string>();
				}
				else
				{
					tNewJobID = pActor.asset.job_citizen.GetRandom<string>();
				}
			}
			else if (pActor.isKingdomCiv())
			{
				tNewJobID = pActor.asset.job_kingdom.GetRandom<string>();
			}
			else if (pActor.asset.job.Length != 0)
			{
				tNewJobID = pActor.asset.job.GetRandom<string>();
			}
		}
		else if (pActor.asset.job.Length != 0)
		{
			tNewJobID = pActor.asset.job.GetRandom<string>();
		}
		return tNewJobID;
	}

	// Token: 0x0600109C RID: 4252 RVA: 0x000CA689 File Offset: 0x000C8889
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isTask(string pID)
	{
		BehaviourTaskActor task = this.ai.task;
		return ((task != null) ? task.id : null) == pID;
	}

	// Token: 0x0600109D RID: 4253 RVA: 0x000CA6A8 File Offset: 0x000C88A8
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasTask()
	{
		return this.ai.hasTask();
	}

	// Token: 0x0600109E RID: 4254 RVA: 0x000CA6B5 File Offset: 0x000C88B5
	public void clearDecisions()
	{
		this._decision_cooldowns.Clear<double>();
		this._decision_disabled.Clear<bool>();
		this.decisions.Clear<DecisionAsset>();
		this.decisions_counter = 0;
		this._last_decision_id = string.Empty;
	}

	// Token: 0x0600109F RID: 4255 RVA: 0x000CA6EA File Offset: 0x000C88EA
	public void scheduleTask(string pTask, WorldTile pTile)
	{
		this.ai.scheduleTask(pTask);
		this.scheduled_tile_target = pTile;
	}

	// Token: 0x060010A0 RID: 4256 RVA: 0x000CA700 File Offset: 0x000C8900
	private void registerDecisions()
	{
		foreach (ActorTrait actorTrait in this.traits)
		{
			DecisionAsset[] tTraitDecisionAssets = actorTrait.decisions_assets;
			if (tTraitDecisionAssets != null)
			{
				for (int i = 0; i < tTraitDecisionAssets.Length; i++)
				{
					DecisionAsset[] array = this.decisions;
					int num = this.decisions_counter;
					this.decisions_counter = num + 1;
					array[num] = tTraitDecisionAssets[i];
				}
			}
		}
		Clan clan = this.clan;
		if (clan != null && clan.decisions_assets.Count > 0)
		{
			List<DecisionAsset> tDecisionsAssets = this.clan.decisions_assets;
			for (int j = 0; j < tDecisionsAssets.Count; j++)
			{
				DecisionAsset[] array2 = this.decisions;
				int num = this.decisions_counter;
				this.decisions_counter = num + 1;
				array2[num] = tDecisionsAssets[j];
			}
		}
		Culture culture = this.culture;
		if (culture != null && culture.decisions_assets.Count > 0)
		{
			List<DecisionAsset> tDecisionsAssets2 = this.culture.decisions_assets;
			for (int k = 0; k < tDecisionsAssets2.Count; k++)
			{
				DecisionAsset[] array3 = this.decisions;
				int num = this.decisions_counter;
				this.decisions_counter = num + 1;
				array3[num] = tDecisionsAssets2[k];
			}
		}
		Language language = this.language;
		if (language != null && language.decisions_assets.Count > 0)
		{
			List<DecisionAsset> tDecisionsAssets3 = this.language.decisions_assets;
			for (int l = 0; l < tDecisionsAssets3.Count; l++)
			{
				DecisionAsset[] array4 = this.decisions;
				int num = this.decisions_counter;
				this.decisions_counter = num + 1;
				array4[num] = tDecisionsAssets3[l];
			}
		}
		Religion religion = this.religion;
		if (religion != null && religion.decisions_assets.Count > 0 && this.canUseReligionSpells())
		{
			List<DecisionAsset> tDecisionsAssets4 = this.religion.decisions_assets;
			for (int m = 0; m < tDecisionsAssets4.Count; m++)
			{
				DecisionAsset[] array5 = this.decisions;
				int num = this.decisions_counter;
				this.decisions_counter = num + 1;
				array5[num] = tDecisionsAssets4[m];
			}
		}
		Subspecies subspecies = this.subspecies;
		if (subspecies != null && subspecies.decisions_assets.Count > 0)
		{
			List<DecisionAsset> tDecisionsAssets5 = this.subspecies.decisions_assets;
			for (int n = 0; n < tDecisionsAssets5.Count; n++)
			{
				DecisionAsset[] array6 = this.decisions;
				int num = this.decisions_counter;
				this.decisions_counter = num + 1;
				array6[num] = tDecisionsAssets5[n];
			}
		}
		if (this.profession_asset != null && this.profession_asset.hasDecisions())
		{
			DecisionAsset[] tDecisionsAssets6 = this.profession_asset.decisions_assets;
			for (int i2 = 0; i2 < tDecisionsAssets6.Length; i2++)
			{
				DecisionAsset[] array7 = this.decisions;
				int num = this.decisions_counter;
				this.decisions_counter = num + 1;
				array7[num] = tDecisionsAssets6[i2];
			}
		}
		if (this._spells.hasAny())
		{
			foreach (SpellAsset tSpellAsset in this._spells.spells)
			{
				if (tSpellAsset.hasDecisions())
				{
					DecisionAsset[] tSpellDecisions = tSpellAsset.decisions_assets;
					for (int i3 = 0; i3 < tSpellDecisions.Length; i3++)
					{
						DecisionAsset[] array8 = this.decisions;
						int num = this.decisions_counter;
						this.decisions_counter = num + 1;
						array8[num] = tSpellDecisions[i3];
					}
				}
			}
		}
		if (this.hasWeapon() && this.getWeapon().getAsset().hasDecisions())
		{
			DecisionAsset[] tWeaponDecisions = this.getWeapon().getAsset().decisions_assets;
			for (int i4 = 0; i4 < tWeaponDecisions.Length; i4++)
			{
				DecisionAsset[] array9 = this.decisions;
				int num = this.decisions_counter;
				this.decisions_counter = num + 1;
				array9[num] = tWeaponDecisions[i4];
			}
		}
		if (this.hasFamily())
		{
			DecisionAsset[] tDecisionsAssets7 = MetaTypeLibrary.family.decisions_assets;
			for (int i5 = 0; i5 < tDecisionsAssets7.Length; i5++)
			{
				DecisionAsset[] array10 = this.decisions;
				int num = this.decisions_counter;
				this.decisions_counter = num + 1;
				array10[num] = tDecisionsAssets7[i5];
			}
		}
		if (base.hasCity() && !this.asset.is_boat)
		{
			DecisionAsset[] tDecisionsAssets8 = MetaTypeLibrary.city.decisions_assets;
			for (int i6 = 0; i6 < tDecisionsAssets8.Length; i6++)
			{
				DecisionAsset[] array11 = this.decisions;
				int num = this.decisions_counter;
				this.decisions_counter = num + 1;
				array11[num] = tDecisionsAssets8[i6];
			}
		}
		if (this.hasPlot())
		{
			DecisionAsset[] tDecisionsAssets9 = MetaTypeLibrary.plot.decisions_assets;
			for (int i7 = 0; i7 < tDecisionsAssets9.Length; i7++)
			{
				DecisionAsset[] array12 = this.decisions;
				int num = this.decisions_counter;
				this.decisions_counter = num + 1;
				array12[num] = tDecisionsAssets9[i7];
			}
		}
		if (this.hasClan())
		{
			DecisionAsset[] tDecisionsAssets10 = MetaTypeLibrary.clan.decisions_assets;
			for (int i8 = 0; i8 < tDecisionsAssets10.Length; i8++)
			{
				DecisionAsset[] array13 = this.decisions;
				int num = this.decisions_counter;
				this.decisions_counter = num + 1;
				array13[num] = tDecisionsAssets10[i8];
			}
		}
	}

	// Token: 0x060010A1 RID: 4257 RVA: 0x000CABC0 File Offset: 0x000C8DC0
	public void debugFav()
	{
	}

	// Token: 0x170000AB RID: 171
	// (get) Token: 0x060010A2 RID: 4258 RVA: 0x000CABC2 File Offset: 0x000C8DC2
	public WorldTile debug_next_step_tile
	{
		get
		{
			return this._next_step_tile;
		}
	}

	// Token: 0x060010A3 RID: 4259 RVA: 0x000CABCA File Offset: 0x000C8DCA
	public void clearWait()
	{
		this.timer_action = 0f;
	}

	// Token: 0x060010A4 RID: 4260 RVA: 0x000CABD7 File Offset: 0x000C8DD7
	public void makeWait(float pValue = 10f)
	{
		this.stopMovement();
		this.timer_action = pValue;
	}

	// Token: 0x060010A5 RID: 4261 RVA: 0x000CABE6 File Offset: 0x000C8DE6
	public void stopSleeping()
	{
		base.finishStatusEffect("sleeping");
	}

	// Token: 0x060010A6 RID: 4262 RVA: 0x000CABF4 File Offset: 0x000C8DF4
	private void checkStepActionForTile(WorldTile pTile)
	{
		if (pTile.Type.step_action != null && Randy.randomChance(pTile.Type.step_action_chance))
		{
			pTile.Type.step_action(pTile, this.a);
		}
		Building tBuilding = pTile.building;
		if (tBuilding != null && tBuilding.asset.flora)
		{
			BuildingAsset tBuildingAsset = tBuilding.asset;
			FloraType flora_type = tBuildingAsset.flora_type;
			if (flora_type != FloraType.Fungi)
			{
				if (flora_type != FloraType.Plant)
				{
					return;
				}
				if (tBuildingAsset.type == "type_flower" && WorldLawLibrary.world_law_nectar_nap.isEnabled() && Randy.randomChance(0.1f))
				{
					this.makeSleep(10f);
					return;
				}
				if (WorldLawLibrary.world_law_plants_tickles.isEnabled() && Randy.randomChance(0.3f))
				{
					this.tryToGetSurprised(pTile, false);
				}
				if (WorldLawLibrary.world_law_root_pranks.isEnabled() && Randy.randomChance(0.2f))
				{
					this.makeStunned(5f);
				}
			}
			else if (WorldLawLibrary.world_law_exploding_mushrooms.isEnabled())
			{
				MapAction.damageWorld(pTile, 5, AssetManager.terraform.get("grenade"), null);
				EffectsLibrary.spawnAtTileRandomScale("fx_explosion_small", pTile, 0.1f, 0.15f);
				return;
			}
		}
	}

	// Token: 0x060010A7 RID: 4263 RVA: 0x000CAD23 File Offset: 0x000C8F23
	public void setLover(Actor pActor)
	{
		this.lover = pActor;
	}

	// Token: 0x060010A8 RID: 4264 RVA: 0x000CAD2C File Offset: 0x000C8F2C
	public void setBestFriend(Actor pActor, bool pNew)
	{
		this.data.best_friend_id = pActor.data.id;
		if (pNew)
		{
			this.changeHappiness("just_made_friend", 0);
		}
	}

	// Token: 0x060010A9 RID: 4265 RVA: 0x000CAD54 File Offset: 0x000C8F54
	public void becomeLoversWith(Actor pTarget)
	{
		this.setLover(pTarget);
		pTarget.setLover(this);
		base.addStatusEffect("fell_in_love", 0f, false);
		pTarget.addStatusEffect("fell_in_love", 0f, false);
	}

	// Token: 0x060010AA RID: 4266 RVA: 0x000CAD88 File Offset: 0x000C8F88
	public void resetSocialize()
	{
		this.data.removeInt("socialize");
		this.timestamp_tween_session_social = 0.0;
	}

	// Token: 0x060010AB RID: 4267 RVA: 0x000CADA9 File Offset: 0x000C8FA9
	public void addActionWaitAfterLand(float pTimer)
	{
		this._action_wait_after_land = true;
		this._action_wait_after_land_timer = pTimer;
	}

	// Token: 0x060010AC RID: 4268 RVA: 0x000CADB9 File Offset: 0x000C8FB9
	private void actionMagnetAnimation(Actor pActor)
	{
		this.position_height = 0f;
	}

	// Token: 0x060010AD RID: 4269 RVA: 0x000CADC8 File Offset: 0x000C8FC8
	private bool isSurprisedJump(WorldTile pTile)
	{
		bool flag = this.canSeeTileBasedOnDirection(pTile);
		bool tMakeJump = false;
		if (!flag && this.hasSubspecies() && this.subspecies.can_process_emotions && (this.subspecies.has_trait_timid || !base.hasStatus("on_guard")))
		{
			tMakeJump = true;
		}
		return tMakeJump;
	}

	// Token: 0x060010AE RID: 4270 RVA: 0x000CAE14 File Offset: 0x000C9014
	private void checkLand(Actor pActor)
	{
		if (!this.should_check_land_cancel)
		{
			return;
		}
		this.should_check_land_cancel = false;
		if (this.has_attack_target && this.isEnemyTargetAlive() && this._has_emotions && !this.hasStatusTantrum())
		{
			if (this.getHealthRatio() < 0.15f)
			{
				this.cancelAllBeh();
				this.setTask("run_away", true, false, true);
				return;
			}
			if (Toolbox.DistVec2Float(this.current_position, this.attack_target.current_position) < 10f)
			{
				return;
			}
		}
		this.cancelAllBeh();
	}

	// Token: 0x060010AF RID: 4271 RVA: 0x000CAE97 File Offset: 0x000C9097
	private void checkDeathOutsideMap(Actor pActor)
	{
		if (!this.inMapBorder())
		{
			this.getHitFullHealth(AttackType.Gravity);
		}
	}

	// Token: 0x060010B0 RID: 4272 RVA: 0x000CAEA9 File Offset: 0x000C90A9
	public void tryToGetSurprised(WorldTile pTile, bool pForceJump = false)
	{
		if (this.canBeSurprised(pTile))
		{
			this.getSurprised(pTile, pForceJump);
		}
	}

	// Token: 0x060010B1 RID: 4273 RVA: 0x000CAEBC File Offset: 0x000C90BC
	public void getSurprised(WorldTile pTile, bool pForceJump = false)
	{
		if (!this._has_emotions)
		{
			return;
		}
		float tTimer = 1f + Randy.randomFloat(0f, 2f);
		bool flag = !base.hasStatus("surprised");
		bool tDoSurprisedJump = pForceJump || this.isSurprisedJump(pTile);
		if (flag)
		{
			base.addStatusEffect("surprised", tTimer, false);
			if (base.hasStatus("just_ate"))
			{
				this.poop(false);
				tDoSurprisedJump = true;
			}
		}
		else
		{
			tTimer = 0.1f;
		}
		if (tDoSurprisedJump)
		{
			this.addActionWaitAfterLand(tTimer);
			this.applyRandomForce(1.5f, 2f);
		}
		base.addStatusEffect("on_guard", 0f, false);
		if (tDoSurprisedJump || !this.isTask("investigate_curiosity") || !this.is_moving)
		{
			this.lookTowardsPosition(pTile.posV3);
			this.stopMovement();
			this.cancelAllBeh();
			if (!tDoSurprisedJump)
			{
				this.makeWait(tTimer);
			}
			this.scheduleTask("investigate_curiosity", pTile);
		}
		float tRunAwayChance = 0.3f;
		if (this.hasSubspecies() && this.subspecies.has_trait_timid)
		{
			tRunAwayChance += 0.3f;
		}
		if (Randy.randomChance(tRunAwayChance))
		{
			this.cancelAllBeh();
			this.scheduleTask("run_away", null);
		}
	}

	// Token: 0x060010B2 RID: 4274 RVA: 0x000CAFE6 File Offset: 0x000C91E6
	public bool makeSleep(float pTime)
	{
		bool flag = base.addStatusEffect("sleeping", pTime, true);
		if (flag)
		{
			this.makeWait(pTime);
		}
		return flag;
	}

	// Token: 0x060010B3 RID: 4275 RVA: 0x000CAFFF File Offset: 0x000C91FF
	public void makeStunned(float pTime = 5f)
	{
		pTime += Randy.randomFloat(0f, pTime * 0.1f);
		this.cancelAllBeh();
		this.makeWait(pTime);
		if (base.addStatusEffect("stunned", pTime, true))
		{
			this.finishAngryStatus();
		}
	}

	// Token: 0x060010B4 RID: 4276 RVA: 0x000CB038 File Offset: 0x000C9238
	public void makeStunnedFromUI()
	{
		this.makeStunned(5f);
		this.updateStats();
	}

	// Token: 0x060010B5 RID: 4277 RVA: 0x000CB04B File Offset: 0x000C924B
	public void justAte()
	{
		base.addStatusEffect("just_ate", 0f, true);
	}

	// Token: 0x060010B6 RID: 4278 RVA: 0x000CB060 File Offset: 0x000C9260
	public void poop(bool pApplyForce)
	{
		this.donePooping();
		float tRandomChance = 1f;
		string tBuildingID;
		if (this.hasSubspecies())
		{
			tBuildingID = this.subspecies.getRandomBioProduct();
			tRandomChance = 0.2f;
		}
		else
		{
			tBuildingID = "poop";
		}
		if (tRandomChance >= 1f || Randy.randomChance(tRandomChance))
		{
			BuildingHelper.tryToBuildNear(this.current_tile, tBuildingID);
		}
		if (pApplyForce)
		{
			this.applyRandomForce(1.5f, 2f);
		}
	}

	// Token: 0x060010B7 RID: 4279 RVA: 0x000CB0CA File Offset: 0x000C92CA
	public void donePooping()
	{
		base.finishStatusEffect("just_ate");
		this.changeHappiness("just_pooped", 0);
	}

	// Token: 0x060010B8 RID: 4280 RVA: 0x000CB0E4 File Offset: 0x000C92E4
	public void birthEvent(string pAddSpecialTrait = null, string pAddSpecialStatus = null)
	{
		this.changeHappiness("just_had_child", 0);
		this.makeStunned(4f);
		this.spendNutritionOnBirth();
		if (!string.IsNullOrEmpty(pAddSpecialTrait))
		{
			this.addTrait(pAddSpecialTrait, false);
		}
		if (!string.IsNullOrEmpty(pAddSpecialStatus))
		{
			base.addStatusEffect(pAddSpecialStatus, 0f, true);
		}
	}

	// Token: 0x060010B9 RID: 4281 RVA: 0x000CB138 File Offset: 0x000C9338
	public void consumeTopTile(WorldTile pTile)
	{
		if (Randy.randomChance(0.3f))
		{
			World.world.units.addRandomTraitFromBiomeToActor(this, pTile);
		}
		this.addNutritionFromEating(pTile.Type.nutrition, false, true);
		this.countConsumed();
		pTile.topTileEaten(5);
		pTile.setBurned(-1);
	}

	// Token: 0x060010BA RID: 4282 RVA: 0x000CB18C File Offset: 0x000C938C
	public void countConsumed()
	{
		ActorData actorData = this.data;
		int food_consumed = actorData.food_consumed;
		actorData.food_consumed = food_consumed + 1;
	}

	// Token: 0x060010BB RID: 4283 RVA: 0x000CB1B0 File Offset: 0x000C93B0
	public void consumeFoodResource(ResourceAsset pAsset)
	{
		this.ate_last_item_id = pAsset.id;
		this.timestamp_session_ate_food = World.world.getCurSessionTime();
		if (pAsset.give_experience != 0)
		{
			this.addExperience(pAsset.give_experience);
		}
		if (pAsset.restore_happiness != 0)
		{
			this.changeHappiness("just_ate", pAsset.restore_happiness);
		}
		int tRestoreHungerValue = pAsset.restore_nutrition;
		float tRestoreHealthPercent = pAsset.restore_health;
		if (this.hasFavoriteFood())
		{
			if (pAsset.id != this.data.favorite_food)
			{
				ResourceAsset tFavoriteFood = this.favorite_food_asset;
				if (pAsset.tastiness > tFavoriteFood.tastiness && Randy.randomChance(pAsset.favorite_food_chance))
				{
					this.data.favorite_food = pAsset.id;
				}
			}
		}
		else if (Randy.randomChance(pAsset.favorite_food_chance))
		{
			this.data.favorite_food = pAsset.id;
		}
		if (pAsset.id == this.data.favorite_food)
		{
			tRestoreHungerValue *= 2;
			tRestoreHealthPercent *= 2f;
		}
		this.addNutritionFromEating(tRestoreHungerValue, false, true);
		this.restoreHealthPercent(tRestoreHealthPercent);
		this.countConsumed();
		if (Randy.randomChance(pAsset.give_chance))
		{
			ActorTrait[] give_trait = pAsset.give_trait;
			if (give_trait != null && give_trait.Length != 0 && Randy.randomBool())
			{
				ActorTrait tTrait = pAsset.give_trait.GetRandom<ActorTrait>();
				if (tTrait != null)
				{
					this.addTrait(tTrait, false);
				}
			}
			StatusAsset[] give_status = pAsset.give_status;
			if (give_status != null && give_status.Length != 0 && Randy.randomBool())
			{
				StatusAsset tStatus = pAsset.give_status.GetRandom<StatusAsset>();
				if (tStatus != null)
				{
					this.addStatusEffect(tStatus, 0f, true);
				}
			}
			if (pAsset.give_action != null && Randy.randomBool())
			{
				pAsset.give_action(pAsset);
			}
		}
	}

	// Token: 0x060010BC RID: 4284 RVA: 0x000CB359 File Offset: 0x000C9559
	internal void justBorn()
	{
		this.setActorScale(0.02f);
	}

	// Token: 0x060010BD RID: 4285 RVA: 0x000CB366 File Offset: 0x000C9566
	public void stopBeingWarrior()
	{
		if (this.isProfession(UnitProfession.Warrior))
		{
			this.setProfession(UnitProfession.Unit, true);
			if (base.hasCity())
			{
				this.city.status.warriors_current--;
			}
		}
		this.removeFromArmy();
	}

	// Token: 0x060010BE RID: 4286 RVA: 0x000CB3A0 File Offset: 0x000C95A0
	public void pokeFromAvatarUI()
	{
		if (base.getHealth() > 1)
		{
			this.getHit(1f, true, AttackType.Divine, null, true, false, true);
		}
		if (Randy.randomChance(0.15f))
		{
			this.makeStunnedFromUI();
			this.changeHappiness("got_poked", 0);
		}
		base.addStatusEffect("motivated", 0f, true);
		this.applyRandomForce(1.5f, 2f);
		this.makeSoundAttack();
	}

	// Token: 0x060010BF RID: 4287 RVA: 0x000CB40E File Offset: 0x000C960E
	public void finishPossessionStatus()
	{
		base.finishStatusEffect("possessed");
		this._has_status_possessed = false;
	}

	// Token: 0x060010C0 RID: 4288 RVA: 0x000CB424 File Offset: 0x000C9624
	public void madePeace(War pWar)
	{
		this.changeHappiness("just_made_peace", 0);
		if (this.isKing())
		{
			this.addRenown(pWar.getRenown(), 0.2f);
		}
		if (this.isCityLeader())
		{
			this.addRenown(pWar.getRenown(), 0.05f);
		}
		if (this.is_army_captain)
		{
			this.army.addRenown(pWar.getRenown(), 0.05f);
		}
		if (this.hasTag("love_peace"))
		{
			base.addStatusEffect("festive_spirit", 0f, true);
		}
	}

	// Token: 0x060010C1 RID: 4289 RVA: 0x000CB4B0 File Offset: 0x000C96B0
	public void warWon(War pWar)
	{
		if (!this.hasHappinessEntry("was_conquered", 300f))
		{
			if (this.isKing())
			{
				this.addRenown(pWar.getRenown());
			}
			if (this.isCityLeader())
			{
				this.addRenown(pWar.getRenown(), 0.2f);
			}
			if (this.isWarrior())
			{
				this.addRenown(pWar.getRenown(), 0.05f);
			}
			if (this.is_army_captain)
			{
				this.army.addRenown(pWar.getRenown(), 0.05f);
			}
			this.changeHappiness("just_won_war", 0);
		}
		if (this.hasTag("love_peace"))
		{
			base.addStatusEffect("festive_spirit", 0f, true);
		}
	}

	// Token: 0x060010C2 RID: 4290 RVA: 0x000CB560 File Offset: 0x000C9760
	public void warLost(War pWar)
	{
		this.changeHappiness("just_lost_war", 0);
		if (this.isKing())
		{
			this.addRenown(pWar.getRenown(), 0.05f);
		}
		if (this.is_army_captain)
		{
			this.army.addRenown(pWar.getRenown(), 0.01f);
		}
	}

	// Token: 0x060010C3 RID: 4291 RVA: 0x000CB5B1 File Offset: 0x000C97B1
	public void setTransformed()
	{
		this.data.set("transformation_done", true);
	}

	// Token: 0x060010C4 RID: 4292 RVA: 0x000CB5C8 File Offset: 0x000C97C8
	public bool isAlreadyTransformed()
	{
		bool tValue;
		this.data.get("transformation_done", out tValue, false);
		return tValue;
	}

	// Token: 0x060010C5 RID: 4293 RVA: 0x000CB5E9 File Offset: 0x000C97E9
	public void makeConfused(float pConfusedTimer = -1f, bool pColorEffect = false)
	{
		this.cancelAllBeh();
		if (pColorEffect)
		{
			this.startColorEffect(ActorColorEffect.White);
		}
		base.addStatusEffect("confused", pConfusedTimer, pColorEffect);
		this.makeStunned(3f);
	}

	// Token: 0x060010C6 RID: 4294 RVA: 0x000CB614 File Offset: 0x000C9814
	public void checkShouldBeEgg()
	{
		if (this.hasSubspecies() && this.subspecies.has_egg_form && (float)this.age < this.subspecies.age_adult)
		{
			float tMaturationTime = this.getMaturationTimeSeconds();
			base.addStatusEffect("egg", tMaturationTime, true);
		}
	}

	// Token: 0x060010C7 RID: 4295 RVA: 0x000CB65F File Offset: 0x000C985F
	public void leavePlot()
	{
		this.setPlot(null);
	}

	// Token: 0x060010C8 RID: 4296 RVA: 0x000CB668 File Offset: 0x000C9868
	private void levelUp()
	{
		int tExpToLevelUp = this.getExpToLevelup();
		int tMaxLevel = this.getMaxPossibleLevel();
		this.data.experience = 0;
		ActorData actorData = this.data;
		int level = actorData.level;
		actorData.level = level + 1;
		if (this.hasCulture() && this.culture.hasTrait("training_potential"))
		{
			ActorData actorData2 = this.data;
			level = actorData2.level;
			actorData2.level = level + 1;
		}
		if (this.data.level == tMaxLevel)
		{
			this.data.experience = tExpToLevelUp;
		}
		this.setStatsDirty();
		EffectsLibrary.showMetaEventEffect("fx_experience_gain", this);
	}

	// Token: 0x060010C9 RID: 4297 RVA: 0x000CB700 File Offset: 0x000C9900
	private void checkGrowthEvent()
	{
		bool tWasBaby = this.isBaby();
		bool flag = this.isEgg();
		this.calcAgeStates();
		if (this.animation_container != null && this.animation_container.child != this.isBaby())
		{
			this.clearSprites();
		}
		if (flag && !this.isEgg())
		{
			this.batch.c_events_hatched.Add(this);
			return;
		}
		if (tWasBaby && !this.isBaby())
		{
			this.batch.c_events_become_adult.Add(this);
		}
	}

	// Token: 0x060010CA RID: 4298 RVA: 0x000CB779 File Offset: 0x000C9979
	internal void eventHatchFromEgg()
	{
		this.growthStateEvent();
		this.triggerHatchFromEggAction();
		this.applyRandomForce(1.5f, 2f);
		this.changeHappiness("just_got_out_of_egg", 0);
		this.batch.c_events_hatched.Remove(this);
	}

	// Token: 0x060010CB RID: 4299 RVA: 0x000CB7B8 File Offset: 0x000C99B8
	internal void eventBecomeAdult()
	{
		this.growthStateEvent();
		this.changeHappiness("just_became_adult", 0);
		this.checkTraitMutationGrowUp();
		this.batch.c_events_become_adult.Remove(this);
		RateCounter counter_new_adults = this.subspecies.counter_new_adults;
		if (counter_new_adults == null)
		{
			return;
		}
		counter_new_adults.registerEvent();
	}

	// Token: 0x060010CC RID: 4300 RVA: 0x000CB804 File Offset: 0x000C9A04
	private void growthStateEvent()
	{
		this.setStatsDirty();
		this.event_full_stats = true;
		if (base.hasCity())
		{
			this.city.setCitizensDirty();
			this.city.setStatusDirty();
		}
	}

	// Token: 0x060010CD RID: 4301 RVA: 0x000CB834 File Offset: 0x000C9A34
	private void triggerHatchFromEggAction()
	{
		SubspeciesTrait tEgg = this.subspecies.egg_asset;
		if (tEgg == null)
		{
			return;
		}
		if (!tEgg.has_after_hatch_from_egg_action)
		{
			return;
		}
		tEgg.after_hatch_from_egg_action(this);
	}

	// Token: 0x060010CE RID: 4302 RVA: 0x000CB868 File Offset: 0x000C9A68
	public bool checkNaturalDeath()
	{
		if (!WorldLawLibrary.world_law_old_age.isEnabled())
		{
			return false;
		}
		if (this.hasTrait("immortal"))
		{
			return false;
		}
		float tAge = (float)this.getAge();
		float tLifespan = this.stats["lifespan"];
		if (tLifespan == 0f)
		{
			return false;
		}
		if (tAge <= tLifespan)
		{
			return false;
		}
		float tOverAge = tAge - tLifespan;
		float tSeverity = 5f;
		if (Randy.randomChance(Mathf.Clamp(1f / (1f + Mathf.Exp(-tSeverity * (tOverAge / tLifespan - 0.5f))), 0f, 0.9f)))
		{
			this.getHitFullHealth(AttackType.Age);
			return true;
		}
		return false;
	}

	// Token: 0x060010CF RID: 4303 RVA: 0x000CB904 File Offset: 0x000C9B04
	public void spawnParticle(Color pColor)
	{
		if (Randy.randomBool())
		{
			return;
		}
		if (!MapBox.isRenderGameplay())
		{
			return;
		}
		Vector3 tVec = this.current_position;
		tVec.y += 0.5f * this.current_scale.y / 2f;
		tVec.x += Randy.randomFloat(-0.2f, 0.2f);
		tVec.y += Randy.randomFloat(-0.2f, 0.2f);
		BaseEffect tEffects = EffectsLibrary.spawn("fx_status_particle", null, null, null, 0f, -1f, -1f, null);
		if (tEffects != null)
		{
			((StatusParticle)tEffects).spawnParticle(tVec, pColor, 0.25f);
		}
	}

	// Token: 0x060010D0 RID: 4304 RVA: 0x000CB9BC File Offset: 0x000C9BBC
	private void checkActionsFromAllMetas()
	{
		if (this.hasSubspecies())
		{
			this.addSpecialEffectsFromMetas(this.subspecies.all_actions_actor_special_effect);
			this.s_action_attack_target = (AttackAction)Delegate.Combine(this.s_action_attack_target, this.subspecies.all_actions_actor_attack_target);
			this.s_get_hit_action = (GetHitAction)Delegate.Combine(this.s_get_hit_action, this.subspecies.all_actions_actor_get_hit);
		}
		if (this.hasClan())
		{
			this.addSpecialEffectsFromMetas(this.clan.all_actions_actor_special_effect);
			this.s_action_attack_target = (AttackAction)Delegate.Combine(this.s_action_attack_target, this.clan.all_actions_actor_attack_target);
			this.s_get_hit_action = (GetHitAction)Delegate.Combine(this.s_get_hit_action, this.clan.all_actions_actor_get_hit);
		}
		if (this.hasLanguage())
		{
			this.addSpecialEffectsFromMetas(this.language.all_actions_actor_special_effect);
			this.s_action_attack_target = (AttackAction)Delegate.Combine(this.s_action_attack_target, this.language.all_actions_actor_attack_target);
			this.s_get_hit_action = (GetHitAction)Delegate.Combine(this.s_get_hit_action, this.language.all_actions_actor_get_hit);
		}
		if (this.hasCulture())
		{
			this.addSpecialEffectsFromMetas(this.culture.all_actions_actor_special_effect);
			this.s_action_attack_target = (AttackAction)Delegate.Combine(this.s_action_attack_target, this.culture.all_actions_actor_attack_target);
			this.s_get_hit_action = (GetHitAction)Delegate.Combine(this.s_get_hit_action, this.culture.all_actions_actor_get_hit);
		}
		if (this.hasReligion())
		{
			this.addSpecialEffectsFromMetas(this.religion.all_actions_actor_special_effect);
			this.s_action_attack_target = (AttackAction)Delegate.Combine(this.s_action_attack_target, this.religion.all_actions_actor_attack_target);
			this.s_get_hit_action = (GetHitAction)Delegate.Combine(this.s_get_hit_action, this.religion.all_actions_actor_get_hit);
		}
	}

	// Token: 0x060010D1 RID: 4305 RVA: 0x000CBB90 File Offset: 0x000C9D90
	private void recalcCombatActions()
	{
		foreach (ActorTrait tTrait in this.traits)
		{
			if (tTrait.hasCombatActions())
			{
				this._combat_actions.mergeWith(tTrait.combat_actions);
			}
		}
		Subspecies subspecies = this.subspecies;
		this.checkCombatActions((subspecies != null) ? subspecies.combat_actions : null);
		Clan clan = this.clan;
		this.checkCombatActions((clan != null) ? clan.combat_actions : null);
		Religion religion = this.religion;
		this.checkCombatActions((religion != null) ? religion.combat_actions : null);
	}

	// Token: 0x060010D2 RID: 4306 RVA: 0x000CBC40 File Offset: 0x000C9E40
	private void recalcSpells()
	{
		foreach (ActorTrait tTrait in this.traits)
		{
			if (tTrait.hasSpells())
			{
				this._spells.mergeWith(tTrait.spells);
			}
		}
		if (this.hasEquipment())
		{
			foreach (ActorEquipmentSlot tSlot in this.equipment)
			{
				if (!tSlot.isEmpty())
				{
					Item tItem = tSlot.getItem();
					if (tItem.asset.hasSpells())
					{
						this._spells.mergeWith(tItem.asset.spells);
					}
				}
			}
		}
	}

	// Token: 0x060010D3 RID: 4307 RVA: 0x000CBD18 File Offset: 0x000C9F18
	private void checkSpells(SpellHolder pSpellsHolder)
	{
		if (pSpellsHolder == null || !pSpellsHolder.hasAny())
		{
			return;
		}
		this._spells.mergeWith(pSpellsHolder);
	}

	// Token: 0x060010D4 RID: 4308 RVA: 0x000CBD32 File Offset: 0x000C9F32
	private void checkCombatActions(CombatActionHolder pHolder)
	{
		if (pHolder == null)
		{
			return;
		}
		if (pHolder.isEmpty())
		{
			return;
		}
		this._combat_actions.mergeWith(pHolder);
	}

	// Token: 0x060010D5 RID: 4309 RVA: 0x000CBD4D File Offset: 0x000C9F4D
	public List<CombatActionAsset> getCombatActionPool(CombatActionPool pPool)
	{
		if (!this._combat_actions.hasAny())
		{
			return null;
		}
		return this._combat_actions.getPool(pPool);
	}

	// Token: 0x060010D6 RID: 4310 RVA: 0x000CBD6A File Offset: 0x000C9F6A
	private void clearCombatActions()
	{
		this._combat_actions.reset();
	}

	// Token: 0x060010D7 RID: 4311 RVA: 0x000CBD77 File Offset: 0x000C9F77
	private void clearSpells()
	{
		this._spells.reset();
	}

	// Token: 0x060010D8 RID: 4312 RVA: 0x000CBD84 File Offset: 0x000C9F84
	private bool checkCurrentEnemyTarget()
	{
		if (this.shouldSkipFightCheck())
		{
			return false;
		}
		if (!this.has_attack_target)
		{
			return false;
		}
		if (!this.isEnemyTargetAlive())
		{
			return false;
		}
		BaseSimObject tAttackTarget = this.attack_target;
		Actor tAttackTargetActor = this.attack_target.a;
		if (base.isKingdomCiv() && tAttackTarget.isKingdomCiv() && !this.shouldContinueToAttackTarget())
		{
			this.clearAttackTarget();
			return false;
		}
		if (tAttackTarget.isActor() && !this.hasStatusTantrum() && !tAttackTarget.areFoes(this) && tAttackTarget.a.is_unconscious)
		{
			this.clearAttackTarget();
			return false;
		}
		if (base.canAttackTarget(tAttackTarget, true, this.asset.can_attack_buildings))
		{
			bool tAttackPossible = this.isAttackPossible();
			bool tInAttackInRange = this.isInAttackRange(tAttackTarget);
			if (!tInAttackInRange)
			{
				float tDist = this.distanceToObjectTarget(tAttackTarget);
				if (tDist > 20f && tAttackTargetActor != null && tAttackTargetActor.isTask("run_away"))
				{
					this.clearAttackTarget();
					return false;
				}
				if (tDist > 50f)
				{
					this.clearAttackTarget();
					return false;
				}
				CombatActionAsset tCombatAsset;
				if (tDist > 3f && this.tryToUseAdvancedCombatAction(this.getCombatActionPool(CombatActionPool.BEFORE_ATTACK_MELEE), tAttackTarget, out tCombatAsset))
				{
					tCombatAsset.action_actor_target_position(this, tAttackTarget.current_position, tAttackTarget.current_tile);
					return false;
				}
			}
			if (this.attack_timer > 0f || (!tAttackPossible && tInAttackInRange))
			{
				this.stopMovement();
				CombatActionAsset tCombatAsset2;
				if (this.hasRangeAttack() && this.tryToUseAdvancedCombatAction(this.getCombatActionPool(CombatActionPool.BEFORE_ATTACK_RANGE), tAttackTarget, out tCombatAsset2))
				{
					tCombatAsset2.action_actor_target_position(this, tAttackTarget.current_position, tAttackTarget.current_tile);
				}
				return true;
			}
			if (tInAttackInRange && this.tryToAttack(tAttackTarget, false, null, default(Vector3), null, null, 0f))
			{
				this.stopMovement();
				return true;
			}
		}
		return false;
	}

	// Token: 0x060010D9 RID: 4313 RVA: 0x000CBF28 File Offset: 0x000CA128
	private bool checkEnemyTargets()
	{
		if (!this.isAllowedToLookForEnemies())
		{
			return false;
		}
		if (this.isInWaterAndCantAttack())
		{
			return false;
		}
		if (this._has_status_strange_urge)
		{
			return false;
		}
		if (this.has_attack_target)
		{
			if (!this.hasTask() || !this.ai.task.in_combat)
			{
				this.setTask("fighting", true, true, false);
			}
			return false;
		}
		if (this._timeout_targets > 0f)
		{
			return false;
		}
		this._timeout_targets = 0.1f + Randy.randomFloat(0f, 1f);
		BaseSimObject tNewTarget = base.findEnemyObjectTarget(this.asset.can_attack_buildings);
		if (tNewTarget == null && this._aggression_targets.Count > 0)
		{
			using (ListPool<Actor> tList = new ListPool<Actor>(this._aggression_targets.Count))
			{
				foreach (long tID in this._aggression_targets)
				{
					Actor tActor = World.world.units.get(tID);
					if (!tActor.isRekt())
					{
						tList.Add(tActor);
					}
				}
				if (tList.Count > 0)
				{
					tNewTarget = base.checkObjectList(tList, this.asset.can_attack_buildings, true, true, 30);
				}
				else
				{
					this._aggression_targets.Clear();
				}
			}
		}
		if (tNewTarget == null)
		{
			return false;
		}
		this.startFightingWith(tNewTarget);
		return true;
	}

	// Token: 0x060010DA RID: 4314 RVA: 0x000CC0A0 File Offset: 0x000CA2A0
	public void startFightingWith(BaseSimObject pSimObject)
	{
		this.setAttackTarget(pSimObject);
		this.setTask("fighting", false, true, false);
		this.beh_actor_target = pSimObject;
	}

	// Token: 0x060010DB RID: 4315 RVA: 0x000CC0BE File Offset: 0x000CA2BE
	internal void startAttackCooldown()
	{
		this.attack_timer = this.getAttackCooldown();
		this.last_attack_timestamp = World.world.getCurWorldTime();
	}

	// Token: 0x060010DC RID: 4316 RVA: 0x000CC0DC File Offset: 0x000CA2DC
	internal bool isJustAttacked()
	{
		return World.world.getWorldTimeElapsedSince(this.last_attack_timestamp) < 0.25f;
	}

	// Token: 0x060010DD RID: 4317 RVA: 0x000CC0F8 File Offset: 0x000CA2F8
	internal bool tryToAttack(BaseSimObject pTarget, bool pDoChecks = true, Action pKillAction = null, Vector3 pAttackPosition = default(Vector3), Kingdom pForceKingdom = null, WorldTile pTileTarget = null, float pBonusAreOfEffect = 0f)
	{
		if (pDoChecks)
		{
			if (this.hasMeleeAttack() && pTarget != null && pTarget.position_height > 0f)
			{
				return false;
			}
			if (this.isInWaterAndCantAttack())
			{
				return false;
			}
			if (!this.isAttackPossible())
			{
				return false;
			}
			if (pTarget != null && !this.isInAttackRange(pTarget))
			{
				return false;
			}
		}
		float tZ = 0f;
		float tTargetSize = 0f;
		Vector3 tTargetPos;
		if (pTarget != null)
		{
			tTargetPos = pTarget.current_position;
			tZ = pTarget.getHeight();
			tTargetSize = pTarget.stats["size"];
		}
		else
		{
			tTargetPos = pAttackPosition;
		}
		bool tPossessed = this._has_status_possessed;
		this.startAttackCooldown();
		this.punchTargetAnimation(tTargetPos, true, this.hasRangeAttack(), 40f);
		Vector3 tAttackPosition = new Vector3(tTargetPos.x, tTargetPos.y);
		if (pTarget != null && pTarget.isActor() && pTarget.a.is_moving && pTarget.isFlying())
		{
			tAttackPosition = Vector3.MoveTowards(tAttackPosition, pTarget.a.next_step_position, tTargetSize * 3f);
		}
		Vector3 tCurrentPos = this.current_position;
		float tDist = Vector2.Distance(tCurrentPos, tTargetPos) + tZ;
		Vector3 tAttackVector = Toolbox.getNewPoint(tCurrentPos.x, tCurrentPos.y, tAttackPosition.x, tAttackPosition.y, tDist - tTargetSize, true);
		string tProjectileID = this.getWeaponAsset().projectile;
		bool tRangeAttack = this.hasRangeAttack();
		Kingdom tKingdomForData = pForceKingdom ?? this.kingdom;
		WorldTile tHitTileTarget = pTileTarget ?? ((pTarget != null) ? pTarget.current_tile : null);
		Kingdom pKingdom = tKingdomForData;
		Vector3 pInitiatorPosition = tCurrentPos;
		AttackData tAttackData = new AttackData(this, tHitTileTarget, tAttackVector, pInitiatorPosition, pTarget, pKingdom, AttackType.Weapon, this.haveMetallicWeapon(), true, tRangeAttack, tProjectileID, pKillAction, pBonusAreOfEffect);
		bool result;
		using (ListPool<CombatActionAsset> tRandomPool = new ListPool<CombatActionAsset>())
		{
			if (this.hasSpells() && this.canUseSpells() && !tPossessed)
			{
				this.addToAttackPool(CombatActionLibrary.combat_cast_spell, tRandomPool);
			}
			CombatActionAsset tCombatAsset;
			bool tCombatActionDone;
			if (tRandomPool.Count > 0)
			{
				if (this.hasMeleeAttack())
				{
					this.addToAttackPool(CombatActionLibrary.combat_attack_melee, tRandomPool);
				}
				else
				{
					this.addToAttackPool(CombatActionLibrary.combat_attack_range, tRandomPool);
				}
				tCombatAsset = tRandomPool.GetRandom<CombatActionAsset>();
				tCombatActionDone = tCombatAsset.action(tAttackData);
				if (!tCombatActionDone && !tCombatAsset.basic)
				{
					if (this.hasMeleeAttack())
					{
						tCombatActionDone = CombatActionLibrary.combat_attack_melee.action(tAttackData);
					}
					else
					{
						tCombatActionDone = CombatActionLibrary.combat_attack_range.action(tAttackData);
					}
				}
			}
			else
			{
				if (this.hasMeleeAttack())
				{
					tCombatAsset = CombatActionLibrary.combat_attack_melee;
				}
				else
				{
					tCombatAsset = CombatActionLibrary.combat_attack_range;
				}
				tCombatActionDone = tCombatAsset.action(tAttackData);
			}
			if (tCombatActionDone)
			{
				this.spendStamina(tCombatAsset.cost_stamina);
				this.spendMana(tCombatAsset.cost_mana);
			}
			if (tCombatAsset.play_unit_attack_sounds)
			{
				this.makeSoundAttack();
			}
			if (this.needsFood() && Randy.randomBool())
			{
				this.decreaseNutrition(-1);
			}
			float tRecoil = this.stats.get("recoil");
			if (tRecoil > 0f)
			{
				this.calculateForce(this.current_position.x, this.current_position.y, tAttackPosition.x, tAttackPosition.y, tRecoil, 0f, false);
			}
			result = true;
		}
		return result;
	}

	// Token: 0x060010DE RID: 4318 RVA: 0x000CC434 File Offset: 0x000CA634
	internal override void getHitFullHealth(AttackType pAttackType)
	{
		this.getHit((float)base.getHealth(), false, pAttackType, null, false, false, false);
	}

	// Token: 0x060010DF RID: 4319 RVA: 0x000CC44C File Offset: 0x000CA64C
	internal override void getHit(float pDamage, bool pFlash, AttackType pAttackType, BaseSimObject pAttacker = null, bool pSkipIfShake = true, bool pMetallicWeapon = false, bool pCheckDamageReduction = true)
	{
		this._last_attack_type = pAttackType;
		if (this._cache_check_has_status_removed_on_damage)
		{
			foreach (Status tStatusData in base.getStatuses())
			{
				if (!tStatusData.is_finished && tStatusData.asset.removed_on_damage)
				{
					base.finishStatusEffect(tStatusData.asset.id);
				}
			}
		}
		if (DebugConfig.isOn(DebugOption.IgnoreDamage))
		{
			return;
		}
		if (pSkipIfShake && this._shake_active)
		{
			return;
		}
		this.attackedBy = null;
		if (pAttacker.isRekt())
		{
			pAttacker = null;
		}
		if (pAttacker != this)
		{
			this.attackedBy = pAttacker;
		}
		if (!base.hasHealth())
		{
			return;
		}
		if (this.is_invincible)
		{
			return;
		}
		Actor tAttackerUnit = (pAttacker != null) ? pAttacker.a : null;
		if (pAttackType == AttackType.Weapon)
		{
			bool tClank = false;
			if (pMetallicWeapon && this.haveMetallicWeapon())
			{
				tClank = true;
			}
			if (tClank)
			{
				MusicBox.playSound("event:/SFX/HIT/HitSwordSword", this.current_tile, false, true);
			}
			else if (this.asset.has_sound_hit)
			{
				MusicBox.playSound(this.asset.sound_hit, this.current_tile, false, true);
			}
			if (tAttackerUnit != null && !base.hasStatus("shield"))
			{
				this.damageEquipmentOnGetHit(tAttackerUnit);
			}
		}
		if (pCheckDamageReduction)
		{
			if (pAttackType == AttackType.Other || pAttackType == AttackType.Weapon)
			{
				float tArmorPercent = 1f - this.stats["armor"] / 100f;
				pDamage *= tArmorPercent;
			}
			if (pDamage < 1f)
			{
				pDamage = 1f;
			}
			if (tAttackerUnit != null)
			{
				float tFinalDamage;
				this.checkSpecialAttackLogic(tAttackerUnit, pAttackType, pDamage, out tFinalDamage);
				pDamage = tFinalDamage;
				AchievementLibrary.clone_wars.checkBySignal(new ValueTuple<Actor, Actor>(this, tAttackerUnit));
			}
		}
		base.changeHealth((int)(-(int)pDamage));
		this.timer_action = 0.002f;
		GetHitAction getHitAction = this.s_get_hit_action;
		if (getHitAction != null)
		{
			getHitAction(this, pAttacker, this.current_tile);
		}
		if (pFlash)
		{
			this.startColorEffect(ActorColorEffect.Red);
		}
		if (!base.hasHealth())
		{
			this.batch.c_check_deaths.Add(this);
		}
		if (pAttackType == AttackType.Weapon && !this.asset.immune_to_injuries && !base.hasStatus("shield"))
		{
			if (Randy.randomChance(0.02f))
			{
				this.addInjuryTrait("crippled");
			}
			if (Randy.randomChance(0.02f))
			{
				this.addInjuryTrait("eyepatch");
			}
		}
		this.startShake(0.3f, 0.1f, true, true);
		if (!this.has_attack_target)
		{
			if (this.attackedBy != null && !base.shouldIgnoreTarget(this.attackedBy) && base.canAttackTarget(this.attackedBy, false, true))
			{
				this.setAttackTarget(this.attackedBy);
			}
		}
		else if (this.hasMeleeAttack() && this.attackedBy != null && base.canAttackTarget(this.attackedBy, false, true))
		{
			float tDistToCurrentTarget = Toolbox.SquaredDistVec2Float(this.current_position, this.attack_target.current_position);
			float tDistToAttacker = Toolbox.SquaredDistVec2Float(this.current_position, pAttacker.current_position);
			if (tDistToCurrentTarget > this.getAttackRangeSquared() && tDistToAttacker < tDistToCurrentTarget)
			{
				this.setAttackTarget(pAttacker);
			}
		}
		if (base.hasAnyStatusEffect())
		{
			foreach (Status status in base.getStatuses())
			{
				GetHitAction action_get_hit = status.asset.action_get_hit;
				if (action_get_hit != null)
				{
					action_get_hit(this, pAttacker, this.current_tile);
				}
			}
		}
		GetHitAction action_get_hit2 = this.asset.action_get_hit;
		if (action_get_hit2 != null)
		{
			action_get_hit2(this, pAttacker, this.current_tile);
		}
		if (!base.hasHealth())
		{
			this.checkCallbacksOnDeath();
		}
	}

	// Token: 0x060010E0 RID: 4320 RVA: 0x000CC7E0 File Offset: 0x000CA9E0
	private void pickupResourcesFromKill(Actor pAttacker)
	{
		if (!pAttacker.hasCity())
		{
			return;
		}
		foreach (ResourceContainer tResource in this.getResourcesFromActor())
		{
			if (!this.isSameSpecies(pAttacker) || pAttacker.hasTrait("savage"))
			{
				pAttacker.addToInventory(tResource);
			}
		}
	}

	// Token: 0x060010E1 RID: 4321 RVA: 0x000CC84C File Offset: 0x000CAA4C
	private void checkSpecialAttackLogic(Actor pAttacker, AttackType pAttackType, float pInitialDamage, out float pDamageFinal)
	{
		pDamageFinal = pInitialDamage;
		bool tIsSameKingdom = this.isSameKingdom(pAttacker);
		bool tLucky = false;
		bool tAttackerFriendHasTantrum = pAttacker.hasStatus("tantrum") && !tIsSameKingdom;
		bool tAttackHasPossessed = pAttacker.hasStatus("possessed");
		bool flag = this.kingdom.isEnemy(pAttacker.kingdom);
		float tStunChance = 0.1f;
		if (this._has_status_possessed || tAttackHasPossessed)
		{
			tStunChance = 0.7f;
		}
		else if (tIsSameKingdom)
		{
			tStunChance = 0.5f;
		}
		if (flag)
		{
			tStunChance = 0f;
		}
		bool tShouldStun = Randy.randomChance(tStunChance);
		if (tAttackerFriendHasTantrum)
		{
			tShouldStun = true;
		}
		if (this.getHealthRatio() < 0.5f && tShouldStun)
		{
			pDamageFinal = 1f;
			this.makeStunned(5f);
			this.changeHappiness("lost_fight", 0);
			this.finishAngryStatus();
			tLucky = true;
			if (tAttackerFriendHasTantrum)
			{
				pAttacker.finishStatusEffect("tantrum");
			}
			if (Randy.randomChance(0.4f))
			{
				pAttacker.finishAngryStatus();
			}
		}
		bool tIsAggroChecked = false;
		if (tIsSameKingdom && pAttackType != AttackType.Eaten)
		{
			if (Randy.randomChance(0.3f) || tAttackHasPossessed || pAttacker.hasStatus("angry"))
			{
				this.checkAggroAgainst(pAttacker, tAttackHasPossessed);
				tIsAggroChecked = true;
			}
			if (tLucky)
			{
				pDamageFinal = 0f;
				pAttacker.clearAttackTarget();
				pAttacker.makeWait(0.3f);
				if (pAttacker.hasStatus("angry"))
				{
					pAttacker.finishAngryStatus();
				}
			}
		}
		if (!tIsAggroChecked && tAttackHasPossessed)
		{
			this.checkAggroAgainst(pAttacker, false);
		}
	}

	// Token: 0x060010E2 RID: 4322 RVA: 0x000CC9A4 File Offset: 0x000CABA4
	private void damageEquipmentOnGetHit(Actor pAttacker)
	{
		if (!pAttacker.hasWeapon())
		{
			return;
		}
		if (!this.hasEquipment())
		{
			return;
		}
		int tDamageMultiplier = 4;
		float tBowDamageChance = 0.35f;
		Item tAttackerWeapon = pAttacker.getWeapon();
		EquipmentAsset tAttackerWeaponAsset = tAttackerWeapon.getAsset();
		int tAttackerRigidityRating = tAttackerWeaponAsset.rigidity_rating;
		int tSumRigidityOfAttacked = 0;
		bool tSetStatsDirty = false;
		foreach (ActorEquipmentSlot tSlot in this.equipment)
		{
			if (!Randy.randomBool())
			{
				Item tItem = tSlot.getItem();
				if (!tItem.isBroken())
				{
					EquipmentAsset equipmentAsset = tItem.getAsset();
					int tRigidityRating = equipmentAsset.rigidity_rating;
					if (!equipmentAsset.is_pool_weapon)
					{
						tSumRigidityOfAttacked += tRigidityRating;
					}
					int tDamage = tAttackerRigidityRating / tRigidityRating * tDamageMultiplier;
					tItem.getDamaged(tDamage);
					if (tItem.isBroken())
					{
						tSetStatsDirty = true;
					}
				}
			}
		}
		if (tSetStatsDirty)
		{
			this.setStatsDirty();
		}
		if (tAttackerWeapon.isBroken())
		{
			return;
		}
		if (Randy.randomBool())
		{
			return;
		}
		if (tAttackerWeaponAsset.attack_type == WeaponType.Melee)
		{
			int tDamageToAttackerWeapon = tSumRigidityOfAttacked / 5 / tAttackerRigidityRating * tDamageMultiplier;
			tAttackerWeapon.getDamaged(tDamageToAttackerWeapon);
		}
		else if (tAttackerWeaponAsset.attack_type == WeaponType.Range && Randy.randomChance(tBowDamageChance))
		{
			tAttackerWeapon.getDamaged(1);
		}
		if (tAttackerWeapon.isBroken())
		{
			pAttacker.setStatsDirty();
		}
	}

	// Token: 0x060010E3 RID: 4323 RVA: 0x000CCAD8 File Offset: 0x000CACD8
	public void addInjuryTrait(string pTraitID)
	{
		if (this.addTrait(pTraitID, false))
		{
			this.changeHappiness("just_injured", 0);
		}
	}

	// Token: 0x060010E4 RID: 4324 RVA: 0x000CCAF4 File Offset: 0x000CACF4
	private void checkAggroAgainst(Actor pAttackedBy, bool pCheckAllLists = false)
	{
		this.addAggro(pAttackedBy);
		if (pCheckAllLists)
		{
			if (this.hasFamily())
			{
				this.family.allAngryAt(pAttackedBy, 10f);
			}
			if (this.hasClan())
			{
				this.clan.allAngryAt(pAttackedBy, 10f);
			}
			if (base.hasCity() && this.isBaby())
			{
				this.city.allAngryAt(pAttackedBy, 10f);
			}
			if (this.hasLover())
			{
				this.lover.addAggro(pAttackedBy);
			}
			if (this.hasBestFriend())
			{
				this.getBestFriend().addAggro(pAttackedBy);
			}
			if ((this.isKing() || this.isWarrior() || this.isCityLeader() || this.isBaby()) && !pAttackedBy.isKing())
			{
				foreach (City tCity in this.kingdom.getCities())
				{
					if (tCity.hasArmy())
					{
						tCity.army.allAngryAt(pAttackedBy, 10f);
					}
				}
			}
		}
	}

	// Token: 0x060010E5 RID: 4325 RVA: 0x000CCC08 File Offset: 0x000CAE08
	internal void newKillAction(Actor pDeadUnit, Kingdom pPrevKingdom, AttackType pAttackType)
	{
		this.increaseKills();
		if (this.hasWeapon())
		{
			this.getWeapon().countKill();
		}
		if (base.isKingdomCiv() && pPrevKingdom.isCiv())
		{
			War tWar = World.world.wars.getWar(this.kingdom, pPrevKingdom, false);
			if (tWar != null)
			{
				if (tWar.isAttacker(this.kingdom))
				{
					tWar.increaseDeathsDefenders(pAttackType);
				}
				else
				{
					tWar.increaseDeathsAttackers(pAttackType);
				}
			}
		}
		if (!base.isAlive())
		{
			return;
		}
		if (this.timer_action <= 0f)
		{
			this.makeWait(Randy.randomFloat(0.1f, 1f));
		}
		if (this.hasTrait("bloodlust"))
		{
			this.changeHappiness("just_killed", 0);
		}
		int tMoneys = pDeadUnit.giveAllLootAndMoney();
		this.addLoot(tMoneys);
		this.takeAllResources(pDeadUnit);
		if (this.data.kills > 10)
		{
			this.addTrait("veteran", false);
		}
		if (pDeadUnit.isKing())
		{
			this.addTrait("kingslayer", false);
		}
		this.addExperience(pDeadUnit.asset.experience_given);
		this.addRenown(pDeadUnit.asset.experience_given);
		if (this.hasTrait("madness"))
		{
			this.restoreHealth(base.getMaxHealthPercent(0.05f));
		}
		if (this.understandsHowToUseItems() && !pDeadUnit.hasTrait("infected") && this.canTakeItems())
		{
			this.takeItems(pDeadUnit, 1f, 0);
		}
		this.checkRageDemon();
	}

	// Token: 0x060010E6 RID: 4326 RVA: 0x000CCD74 File Offset: 0x000CAF74
	internal void applyRandomForce(float pMinHeight = 1.5f, float pMaxHeight = 2f)
	{
		float tForceDirection = Randy.randomFloat(1.5f, 2f);
		float tForceHeight = Randy.randomFloat(pMinHeight, pMaxHeight);
		WorldTile tRandomTile = this.current_tile.neighboursAll.GetRandom<WorldTile>();
		this.calculateForce(this.current_tile.posV3.x, this.current_tile.posV3.y, tRandomTile.posV3.x, tRandomTile.posV3.y, tForceDirection, tForceHeight, true);
	}

	// Token: 0x060010E7 RID: 4327 RVA: 0x000CCDEC File Offset: 0x000CAFEC
	internal void calculateForce(float pStartX, float pStartY, float pTargetX, float pTargetY, float pForceAmountDirection, float pForceHeight = 0f, bool pCheckCancelJobOnLand = false)
	{
		if (pForceHeight == 0f)
		{
			pForceHeight = pForceAmountDirection;
		}
		pForceAmountDirection *= SimGlobals.m.unit_force_multiplier;
		pForceHeight *= SimGlobals.m.unit_force_multiplier;
		if (pForceAmountDirection <= 0f)
		{
			return;
		}
		float angle = Toolbox.getAngle(pStartX, pStartY, pTargetX, pTargetY);
		float tForceDirectionX = -Mathf.Cos(angle) * pForceAmountDirection;
		float tForceDirectionY = -Mathf.Sin(angle) * pForceAmountDirection;
		if (pStartX == pTargetX && pStartY == pTargetY)
		{
			tForceDirectionX = 0f;
			tForceDirectionY = 0f;
		}
		this.addForce(tForceDirectionX, tForceDirectionY, pForceHeight, pCheckCancelJobOnLand, false);
	}

	// Token: 0x060010E8 RID: 4328 RVA: 0x000CCE70 File Offset: 0x000CB070
	public bool tryToUseAdvancedCombatAction(List<CombatActionAsset> pCombatActionAssetsCategory, BaseSimObject pAttackTarget, out CombatActionAsset pResultCombatAsset)
	{
		pResultCombatAsset = null;
		if (pCombatActionAssetsCategory == null)
		{
			return false;
		}
		if (pCombatActionAssetsCategory.Count == 0)
		{
			return false;
		}
		if (this.hasTrait("slow"))
		{
			return false;
		}
		if (this.combatActionOnTimeout())
		{
			return false;
		}
		bool result;
		using (ListPool<CombatActionAsset> tPossibleList = new ListPool<CombatActionAsset>(pCombatActionAssetsCategory.Count))
		{
			foreach (CombatActionAsset tCombatActionAsset in pCombatActionAssetsCategory)
			{
				if (this.hasEnoughStamina(tCombatActionAsset.cost_stamina) && this.hasEnoughMana(tCombatActionAsset.cost_mana))
				{
					if (pAttackTarget != null)
					{
						CombatActionCheckStart can_do_action = tCombatActionAsset.can_do_action;
						if (can_do_action != null && !can_do_action(this, pAttackTarget))
						{
							continue;
						}
					}
					tPossibleList.Add(tCombatActionAsset);
				}
			}
			if (tPossibleList.Count == 0)
			{
				result = false;
			}
			else
			{
				CombatActionAsset tAsset = tPossibleList.GetRandom<CombatActionAsset>();
				if (!Randy.randomChance(tAsset.chance + tAsset.chance * this.stats["skill_combat"]))
				{
					result = false;
				}
				else
				{
					this.spendStamina(tAsset.cost_stamina);
					this.spendMana(tAsset.cost_mana);
					pResultCombatAsset = tAsset;
					base.addStatusEffect("recovery_combat_action", pResultCombatAsset.cooldown, false);
					result = true;
				}
			}
		}
		return result;
	}

	// Token: 0x060010E9 RID: 4329 RVA: 0x000CCFB8 File Offset: 0x000CB1B8
	public void addAggro(long pActorID)
	{
		Actor tActor = World.world.units.get(pActorID);
		if (tActor.isRekt())
		{
			return;
		}
		this.addAggro(tActor);
	}

	// Token: 0x060010EA RID: 4330 RVA: 0x000CCFE6 File Offset: 0x000CB1E6
	public void addAggro(Actor pActor)
	{
		if (pActor.isRekt())
		{
			return;
		}
		if (pActor == this)
		{
			return;
		}
		base.addStatusEffect("angry", 0f, false);
		this._aggression_targets.Add(pActor.getID());
	}

	// Token: 0x060010EB RID: 4331 RVA: 0x000CD01A File Offset: 0x000CB21A
	public void finishAngryStatus()
	{
		this._aggression_targets.Clear();
		base.finishStatusEffect("angry");
	}

	// Token: 0x060010EC RID: 4332 RVA: 0x000CD034 File Offset: 0x000CB234
	public void spawnSlashPunch(Vector2 pTowardsPosition)
	{
		this.spawnSlash(pTowardsPosition, "effects/slashes/slash_punch", 2f, 0f, 0f, null);
	}

	// Token: 0x060010ED RID: 4333 RVA: 0x000CD068 File Offset: 0x000CB268
	public void spawnSlashSteal(Vector2 pTowardsPosition)
	{
		this.spawnSlash(pTowardsPosition, "effects/slashes/slash_steal", 2f, 0f, 0f, null);
	}

	// Token: 0x060010EE RID: 4334 RVA: 0x000CD09C File Offset: 0x000CB29C
	public void spawnSlashYell(Vector2 pTowardsPosition)
	{
		this.spawnSlash(pTowardsPosition, "effects/slashes/slash_swear", 2f, 0f, 0f, null);
	}

	// Token: 0x060010EF RID: 4335 RVA: 0x000CD0D0 File Offset: 0x000CB2D0
	public void spawnSlashTalk(Vector2 pTowardsPosition)
	{
		this.spawnSlash(pTowardsPosition, "effects/slashes/slash_talk", 2f, 0f, 0f, null);
	}

	// Token: 0x060010F0 RID: 4336 RVA: 0x000CD104 File Offset: 0x000CB304
	public void spawnSlashKick(Vector2 pTowardsPosition)
	{
		this.spawnSlash(pTowardsPosition, "effects/slashes/slash_kick", 2f, 0f, -this.actor_scale * 8f, null);
	}

	// Token: 0x060010F1 RID: 4337 RVA: 0x000CD140 File Offset: 0x000CB340
	public void spawnSlash(Vector2 pTowardsPosition, string pSlashType = null, float pDistMod = 2f, float pTargetZ = 0f, float pStartZ = 0f, float? pAngle = null)
	{
		if (!this.is_visible)
		{
			return;
		}
		if (!EffectsLibrary.canShowSlashEffect())
		{
			return;
		}
		if (string.IsNullOrEmpty(pSlashType))
		{
			pSlashType = this._attack_asset.path_slash_animation;
		}
		Vector2 tSlashPosition = this.getSlashPosition(this, pTowardsPosition, pDistMod, pTargetZ, pStartZ);
		float tAngle = (pAngle != null) ? pAngle.Value : this.getSlashAngle(tSlashPosition, pTowardsPosition);
		EffectsLibrary.spawnSlash(tSlashPosition, pSlashType, tAngle, this.actor_scale);
	}

	// Token: 0x060010F2 RID: 4338 RVA: 0x000CD1AA File Offset: 0x000CB3AA
	public float getSlashAngle(Vector2 pSlashVector, Vector2 pAttackPosition)
	{
		return Toolbox.getAngleDegrees(pSlashVector.x, pSlashVector.y, pAttackPosition.x, pAttackPosition.y);
	}

	// Token: 0x060010F3 RID: 4339 RVA: 0x000CD1CC File Offset: 0x000CB3CC
	public Vector2 getSlashPosition(Actor pActor, Vector2 pAttackPosition, float pDistMod, float pTargetZ = 0f, float pStartZ = 0f)
	{
		float tScaleMod = pActor.getScaleMod();
		float num = pActor.stats["size"];
		Vector2 tSlashStartPosition = new Vector2(pActor.current_position.x, pActor.current_position.y);
		tSlashStartPosition.y += pActor.getHeight();
		tSlashStartPosition.y += 0.5f * tScaleMod;
		tSlashStartPosition.y += pStartZ;
		float tDist = num * tScaleMod * pDistMod;
		return Toolbox.getNewPointVec2(tSlashStartPosition.x, tSlashStartPosition.y, pAttackPosition.x, pAttackPosition.y + pTargetZ, tDist, true);
	}

	// Token: 0x060010F4 RID: 4340 RVA: 0x000CD264 File Offset: 0x000CB464
	public void doCastAnimation()
	{
		if (!this.is_visible)
		{
			return;
		}
		Vector2 tHeadPos = this.getRenderedItemPosition();
		Vector3 tPos = this.cur_transform_position;
		EffectsLibrary.spawnAt(this.asset.effect_cast_ground, tPos, this.stats["scale"]);
		tPos.y += tHeadPos.y * 6f * this.current_scale.y;
		EffectsLibrary.spawnAt(this.asset.effect_cast_top, tPos, this.stats["scale"]);
	}

	// Token: 0x060010F5 RID: 4341 RVA: 0x000CD2F0 File Offset: 0x000CB4F0
	internal void punchTargetAnimation(Vector3 pDirection, bool pFlip = true, bool pReverse = false, float pAngle = 40f)
	{
		if (this.asset.can_flip)
		{
			if (pFlip && this.checkFlip())
			{
				if (this.current_position.x < pDirection.x)
				{
					this.setFlip(true);
				}
				else
				{
					this.setFlip(false);
				}
			}
			if (pReverse)
			{
				this.target_angle.z = -pAngle;
				return;
			}
			this.target_angle.z = pAngle;
		}
	}

	// Token: 0x060010F6 RID: 4342 RVA: 0x000CD358 File Offset: 0x000CB558
	private void addToAttackPool(CombatActionAsset pAsset, ListPool<CombatActionAsset> pPool)
	{
		for (int i = 0; i < pAsset.rate; i++)
		{
			pPool.Add(pAsset);
		}
	}

	// Token: 0x060010F7 RID: 4343 RVA: 0x000CD380 File Offset: 0x000CB580
	private void checkHappinessChangeFromDeathEvent()
	{
		foreach (Actor actor in this.getParents())
		{
			actor.changeHappiness("death_child", 0);
		}
		Actor tBestFriend = this.getBestFriend();
		if (tBestFriend != null)
		{
			tBestFriend.changeHappiness("death_best_friend", 0);
		}
		if (this.hasLover())
		{
			this.lover.changeHappiness("death_lover", 0);
			this.lover.finishStatusEffect("fell_in_love");
		}
		if (this.hasFamily())
		{
			foreach (Actor tFamilyMember in this.family.units)
			{
				if (tFamilyMember != this && !tFamilyMember.isParentOf(this))
				{
					tFamilyMember.changeHappiness("death_family_member", 0);
				}
			}
		}
	}

	// Token: 0x060010F8 RID: 4344 RVA: 0x000CD474 File Offset: 0x000CB674
	private void checkCallbacksOnDeath()
	{
		WorldAction unit_death_action = this.current_tile.Type.unit_death_action;
		if (unit_death_action != null)
		{
			unit_death_action(this, this.current_tile);
		}
		WorldAction action_death = this.asset.action_death;
		if (action_death != null)
		{
			action_death(this, this.current_tile);
		}
		using (ListPool<ActorTrait> tListCurrentTraits = new ListPool<ActorTrait>(this.getTraits()))
		{
			for (int i = 0; i < tListCurrentTraits.Count; i++)
			{
				WorldAction action_death2 = tListCurrentTraits[i].action_death;
				if (action_death2 != null)
				{
					action_death2(this, this.current_tile);
				}
			}
			if (base.hasAnyStatusEffect())
			{
				foreach (Status tStatusEffectData in base.getStatuses())
				{
					if (tStatusEffectData.asset.action_death != null)
					{
						tStatusEffectData.asset.action_death(this, this.current_tile);
					}
				}
			}
			if (this.hasClan())
			{
				WorldAction all_actions_actor_death = this.clan.all_actions_actor_death;
				if (all_actions_actor_death != null)
				{
					all_actions_actor_death(this, this.current_tile);
				}
			}
			if (this.hasSubspecies())
			{
				WorldAction all_actions_actor_death2 = this.subspecies.all_actions_actor_death;
				if (all_actions_actor_death2 != null)
				{
					all_actions_actor_death2(this, this.current_tile);
				}
			}
			if (this.hasReligion())
			{
				WorldAction all_actions_actor_death3 = this.religion.all_actions_actor_death;
				if (all_actions_actor_death3 != null)
				{
					all_actions_actor_death3(this, this.current_tile);
				}
			}
			BaseActionActor baseActionActor = this.callbacks_on_death;
			if (baseActionActor != null)
			{
				baseActionActor(this);
			}
		}
	}

	// Token: 0x060010F9 RID: 4345 RVA: 0x000CD620 File Offset: 0x000CB820
	public void checkDeath()
	{
		if (base.hasHealth())
		{
			return;
		}
		if (!base.isAlive())
		{
			return;
		}
		Kingdom tPrevKingdom = this.kingdom;
		Actor tAttackerUnit = null;
		if (!this.attackedBy.isRekt() && this.attackedBy.isActor() && this.attackedBy != this)
		{
			tAttackerUnit = this.attackedBy.a;
		}
		if (this._last_attack_type == AttackType.Weapon && (base.isKingdomCiv() || (!tAttackerUnit.isRekt() && tAttackerUnit.isKingdomCiv())))
		{
			BattleKeeperManager.addUnitKilled(this);
		}
		if (tAttackerUnit != null)
		{
			tAttackerUnit.newKillAction(this, tPrevKingdom, this._last_attack_type);
			this.pickupResourcesFromKill(tAttackerUnit);
		}
		this.die(false, this._last_attack_type, true, true);
	}

	// Token: 0x060010FA RID: 4346 RVA: 0x000CD6C6 File Offset: 0x000CB8C6
	public void dieSimpleNone()
	{
		this.die(false, AttackType.None, false, true);
	}

	// Token: 0x060010FB RID: 4347 RVA: 0x000CD6D3 File Offset: 0x000CB8D3
	public void dieAndDestroy(AttackType pType)
	{
		this.die(true, pType, false, true);
	}

	// Token: 0x060010FC RID: 4348 RVA: 0x000CD6DF File Offset: 0x000CB8DF
	public void removeByMetamorphosis()
	{
		this.die(true, AttackType.Metamorphosis, false, false);
	}

	// Token: 0x060010FD RID: 4349 RVA: 0x000CD6EC File Offset: 0x000CB8EC
	private void die(bool pDestroy = false, AttackType pType = AttackType.Other, bool pCountDeath = true, bool pLogFavorite = true)
	{
		if (!base.isAlive() && !pDestroy)
		{
			return;
		}
		switch (pType)
		{
		case AttackType.Plague:
		case AttackType.Infection:
		case AttackType.Tumor:
		case AttackType.Divine:
		case AttackType.AshFever:
		case AttackType.Metamorphosis:
		case AttackType.Starvation:
		case AttackType.Age:
		case AttackType.None:
		case AttackType.Poison:
		case AttackType.Gravity:
		case AttackType.Drowning:
			this.attackedBy = null;
			break;
		}
		SelectedUnit.removeSelected(this);
		if (ControllableUnit.isControllingUnit(this))
		{
			ControllableUnit.remove(this);
			if (this.asset.id == "crabzilla")
			{
				pDestroy = true;
			}
		}
		if (base.isAlive())
		{
			this.setAlive(false);
			this.skipUpdates();
			if (this.is_inside_boat)
			{
				this.inside_boat.removePassenger(this);
				this.exitBoat();
			}
			if (pCountDeath)
			{
				this.countDeath(pType);
				this.checkHappinessChangeFromDeathEvent();
			}
			if (this.isFavorite() && pLogFavorite)
			{
				if (!this.attackedBy.isRekt() && this.attackedBy.isActor())
				{
					WorldLog.logFavMurder(this, this.attackedBy.a);
				}
				else
				{
					WorldLog.logFavDead(this);
				}
			}
			this.clearTasks();
		}
		this.exitBuilding();
		this.clearHomeBuilding();
		using (ListPool<Item> tItems = new ListPool<Item>())
		{
			if (this.hasEquipment())
			{
				tItems.AddRange(this.equipment.getItems());
				this.takeAwayItems();
			}
			if (this.current_tile.zone.hasCity())
			{
				this.current_tile.zone.city.tryToPutItems(tItems);
				tItems.Clear();
			}
			if (pDestroy)
			{
				World.world.units.scheduleDestroyOnPlay(this);
			}
			if (this.isKing())
			{
				this.kingdom.removeKing();
				this.kingdom.logKingDead(this);
			}
			if (base.hasCity())
			{
				this.stopBeingWarrior();
				if (pType == AttackType.Age)
				{
					this.city.tryToPutItems(tItems);
					tItems.Clear();
				}
				this.setCity(null);
			}
			if (this.isKing())
			{
				this.kingdom.removeKing();
			}
			this.clearManagers();
			if (this.hasEquipment())
			{
				this.equipment.destroyAllEquipment();
			}
			this.clearAttackTarget();
			this.clearTileTarget();
		}
	}

	// Token: 0x060010FE RID: 4350 RVA: 0x000CD914 File Offset: 0x000CBB14
	public void checkDieOnGroundBoat()
	{
		if (!this.asset.is_boat)
		{
			return;
		}
		if (!this.current_tile.Type.liquid && base.isAlive() && !this.isInMagnet())
		{
			this.getHitFullHealth(AttackType.Gravity);
			this.skipBehaviour();
			if (base.hasStatus("magnetized"))
			{
				World.world.game_stats.data.boatsDestroyedByMagnet += 1L;
				AchievementLibrary.boats_disposal.checkBySignal(null);
			}
		}
	}

	// Token: 0x060010FF RID: 4351 RVA: 0x000CD994 File Offset: 0x000CBB94
	public void copyAggroFrom(Actor pTarget)
	{
		if (pTarget.hasStatus("angry"))
		{
			foreach (long tAggroTarget in pTarget._aggression_targets)
			{
				this.addAggro(tAggroTarget);
			}
			if (!pTarget.attackedBy.isRekt() && pTarget.attackedBy.isActor())
			{
				this.addAggro(pTarget.attackedBy.a);
			}
			if (!pTarget.attack_target.isRekt() && pTarget.attack_target.isActor())
			{
				this.addAggro(pTarget.attack_target.a);
			}
		}
	}

	// Token: 0x06001100 RID: 4352 RVA: 0x000CDA50 File Offset: 0x000CBC50
	public bool isInAggroList(Actor pActor)
	{
		return this._aggression_targets.Contains(pActor.getID());
	}

	// Token: 0x06001101 RID: 4353 RVA: 0x000CDA64 File Offset: 0x000CBC64
	public bool shouldContinueToAttackTarget()
	{
		BaseSimObject tAttackTarget = this.attack_target;
		return base.areFoes(tAttackTarget) || (tAttackTarget.isActor() && (tAttackTarget.a.hasStatusTantrum() || this.isInAggroList(tAttackTarget.a)));
	}

	// Token: 0x06001102 RID: 4354 RVA: 0x000CDAAD File Offset: 0x000CBCAD
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void clearAttackTarget()
	{
		if (!this.has_attack_target)
		{
			return;
		}
		this.attack_target = null;
		this.has_attack_target = false;
		this.batch.c_check_attack_target.Remove(this.a);
	}

	// Token: 0x06001103 RID: 4355 RVA: 0x000CDADC File Offset: 0x000CBCDC
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isEnemyTargetAlive()
	{
		if (this.has_attack_target)
		{
			if (this.attack_target.isRekt())
			{
				this.clearAttackTarget();
				return false;
			}
			if (this.attack_target.isBuilding() && !this.attack_target.b.isUsable())
			{
				this.clearAttackTarget();
				return false;
			}
		}
		return true;
	}

	// Token: 0x06001104 RID: 4356 RVA: 0x000CDB2E File Offset: 0x000CBD2E
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void setAttackTarget(BaseSimObject pAttackTarget)
	{
		this.attack_target = pAttackTarget;
		if (this.has_attack_target)
		{
			return;
		}
		this.has_attack_target = true;
		this.batch.c_check_attack_target.Add(this.a);
	}

	// Token: 0x06001105 RID: 4357 RVA: 0x000CDB5D File Offset: 0x000CBD5D
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasRangeAttack()
	{
		return this._attack_asset.attack_type == WeaponType.Range;
	}

	// Token: 0x06001106 RID: 4358 RVA: 0x000CDB6D File Offset: 0x000CBD6D
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasMeleeAttack()
	{
		return this._attack_asset.attack_type == WeaponType.Melee;
	}

	// Token: 0x06001107 RID: 4359 RVA: 0x000CDB80 File Offset: 0x000CBD80
	private void checkAttackTypes()
	{
		EquipmentAsset tWeaponAsset = this.getWeaponAsset();
		this._attack_asset = tWeaponAsset;
	}

	// Token: 0x06001108 RID: 4360 RVA: 0x000CDB9C File Offset: 0x000CBD9C
	private bool isEquipmentMeleeAttack()
	{
		EquipmentAsset tWeaponAsset = this.getWeaponAsset();
		return this.asset.only_melee_attack || tWeaponAsset.attack_type == WeaponType.Melee;
	}

	// Token: 0x06001109 RID: 4361 RVA: 0x000CDBC8 File Offset: 0x000CBDC8
	public float getAttackCooldown()
	{
		return 1f / this.stats["attack_speed"];
	}

	// Token: 0x0600110A RID: 4362 RVA: 0x000CDBE0 File Offset: 0x000CBDE0
	private void takeAwayItems()
	{
		if (!this.hasEquipment())
		{
			return;
		}
		foreach (ActorEquipmentSlot tDeadActorSlot in this.equipment)
		{
			if (!tDeadActorSlot.isEmpty())
			{
				tDeadActorSlot.takeAwayItem();
			}
		}
	}

	// Token: 0x0600110B RID: 4363 RVA: 0x000CDC40 File Offset: 0x000CBE40
	public bool isInDangerZone()
	{
		return base.hasCity() && this.city.danger_zones.Contains(base.current_zone);
	}

	// Token: 0x0600110C RID: 4364 RVA: 0x000CDC62 File Offset: 0x000CBE62
	public void setPossessionAttackHappened()
	{
		this._possession_attack_happened_frame = World.world.getCurWorldTime();
	}

	// Token: 0x0600110D RID: 4365 RVA: 0x000CDC74 File Offset: 0x000CBE74
	public bool isPossessionAttackJustHappened()
	{
		return World.world.getCurWorldTime() - this._possession_attack_happened_frame <= 0.5;
	}

	// Token: 0x170000AC RID: 172
	// (get) Token: 0x0600110E RID: 4366 RVA: 0x000CDC95 File Offset: 0x000CBE95
	// (set) Token: 0x0600110F RID: 4367 RVA: 0x000CDCA2 File Offset: 0x000CBEA2
	public ActorBag inventory
	{
		get
		{
			return this.data.inventory;
		}
		set
		{
			this.data.inventory = value;
		}
	}

	// Token: 0x06001110 RID: 4368 RVA: 0x000CDCB0 File Offset: 0x000CBEB0
	public void addLoot(int pLootValue)
	{
		if (pLootValue == 0)
		{
			return;
		}
		this.data.loot += pLootValue;
		this.data.loot = Mathf.Clamp(this.data.loot, 0, 99999);
		EffectsLibrary.showMoneyEffect("fx_money_got_loot", this.current_position, base.current_zone, this.actor_scale);
	}

	// Token: 0x06001111 RID: 4369 RVA: 0x000CDD14 File Offset: 0x000CBF14
	public void addMoney(int pValue)
	{
		if (pValue == 0)
		{
			return;
		}
		this.data.money += pValue;
		this.data.money = Mathf.Clamp(this.data.money, 0, 99999);
		EffectsLibrary.showMoneyEffect("fx_money_got_money", this.current_position, base.current_zone, this.actor_scale);
	}

	// Token: 0x06001112 RID: 4370 RVA: 0x000CDD75 File Offset: 0x000CBF75
	public int giveAllLoot()
	{
		int loot = this.data.loot;
		this.lootEmpty();
		return loot;
	}

	// Token: 0x06001113 RID: 4371 RVA: 0x000CDD88 File Offset: 0x000CBF88
	public int giveAllMoney()
	{
		int money = this.data.money;
		this.data.money = 0;
		return money;
	}

	// Token: 0x06001114 RID: 4372 RVA: 0x000CDDA1 File Offset: 0x000CBFA1
	public void spendMoney(int pCost)
	{
		if (pCost == 0)
		{
			return;
		}
		this.data.money -= pCost;
		EffectsLibrary.showMoneyEffect("fx_money_spend_money", this.current_position, base.current_zone, this.actor_scale);
	}

	// Token: 0x06001115 RID: 4373 RVA: 0x000CDDD8 File Offset: 0x000CBFD8
	public int getMoneyForGift()
	{
		if (this.money < 10)
		{
			return 0;
		}
		float tRandomMoneyMod = Randy.randomFloat(0.05f, 0.1f);
		int tRandomMoney = Mathf.RoundToInt((float)this.money * tRandomMoneyMod);
		if (tRandomMoney == 0)
		{
			return 0;
		}
		EffectsLibrary.showMoneyEffect("fx_money_spend_money", this.current_position, base.current_zone, this.actor_scale);
		return tRandomMoney;
	}

	// Token: 0x06001116 RID: 4374 RVA: 0x000CDE32 File Offset: 0x000CC032
	public void takeAllOwnLoot()
	{
		this.addMoney(this.giveAllLoot());
	}

	// Token: 0x06001117 RID: 4375 RVA: 0x000CDE40 File Offset: 0x000CC040
	public int giveAllLootAndMoney()
	{
		return this.giveAllLoot() + this.giveAllMoney();
	}

	// Token: 0x06001118 RID: 4376 RVA: 0x000CDE50 File Offset: 0x000CC050
	public void paidTax(float pTaxRate, string pEffect)
	{
		this.lootEmpty();
		EffectsLibrary.showMoneyEffect(pEffect, this.current_position, base.current_zone, this.actor_scale);
		int tBonusHappinessChange = -5;
		if ((double)pTaxRate > 0.7)
		{
			tBonusHappinessChange = -10;
		}
		this.changeHappiness("paid_tax", tBonusHappinessChange);
	}

	// Token: 0x06001119 RID: 4377 RVA: 0x000CDE9B File Offset: 0x000CC09B
	public void lootEmpty()
	{
		this.data.loot = 0;
	}

	// Token: 0x0600111A RID: 4378 RVA: 0x000CDEAC File Offset: 0x000CC0AC
	public void giveInventoryResourcesToCity()
	{
		if (this.isCarryingResources() && base.hasCity() && this.city.isAlive())
		{
			foreach (ResourceContainer tContainer in this.inventory.getResources().Values)
			{
				this.city.addResourcesToRandomStockpile(tContainer.id, tContainer.amount);
			}
		}
		this.inventory.empty();
		this.setItemSpriteRenderDirty();
	}

	// Token: 0x0600111B RID: 4379 RVA: 0x000CDF48 File Offset: 0x000CC148
	public void generateDefaultSpawnWeapons(bool pUseOwnerless)
	{
		if (pUseOwnerless && this.canUseItems())
		{
			foreach (Item tItem in World.world.items)
			{
				if (!tItem.isDestroyable() && !tItem.hasCity() && !tItem.hasActor())
				{
					this.equipment.setItem(tItem, this);
					return;
				}
			}
		}
		string[] default_weapons = this.asset.default_weapons;
		if (default_weapons != null && default_weapons.Length != 0)
		{
			string tItemId = this.asset.default_weapons.GetRandom<string>();
			this.createNewWeapon(tItemId);
		}
	}

	// Token: 0x0600111C RID: 4380 RVA: 0x000CDFF8 File Offset: 0x000CC1F8
	public bool createNewWeapon(string pItemId)
	{
		EquipmentAsset tItemAsset = AssetManager.items.get(pItemId);
		Item tNewItem = World.world.items.generateItem(tItemAsset, null, null, 1, this.a, 10, false);
		this.equipment.weapon.setItem(tNewItem, this.a);
		return true;
	}

	// Token: 0x0600111D RID: 4381 RVA: 0x000CE046 File Offset: 0x000CC246
	internal void reloadInventory()
	{
		this.setStatsDirty();
	}

	// Token: 0x0600111E RID: 4382 RVA: 0x000CE050 File Offset: 0x000CC250
	public void stealActionFrom(Actor pTarget, float pTargetStunnedTimer = 5f, float pWaitTimerForThief = 1f, bool pAddAggro = true, bool pPossessedSteal = false)
	{
		bool tAnythingStolen = false;
		int tEverything = pTarget.giveAllLootAndMoney();
		if (tEverything > 0)
		{
			tAnythingStolen = true;
		}
		this.addLoot(tEverything);
		pTarget.cancelAllBeh();
		pTarget.makeStunned(pTargetStunnedTimer);
		this.makeWait(pWaitTimerForThief);
		base.addStatusEffect("being_suspicious", 0f, true);
		if (pAddAggro)
		{
			pTarget.addAggro(this);
		}
		this.punchTargetAnimation(this.current_position, false, false, -40f);
		if ((this.hasSubspeciesMetaTag("steal_items") || this.hasTag("steal_items") || pPossessedSteal) && this.tryToStealItems(pTarget, pPossessedSteal))
		{
			tAnythingStolen = true;
		}
		if (tAnythingStolen)
		{
			pTarget.changeHappiness("got_robbed", 0);
		}
	}

	// Token: 0x0600111F RID: 4383 RVA: 0x000CE0FC File Offset: 0x000CC2FC
	public bool tryToStealItems(Actor pActorTarget, bool pPossessedSteal = false)
	{
		if (!this.understandsHowToUseItems())
		{
			return false;
		}
		if (!this.hasMeleeAttack())
		{
			return false;
		}
		float tChance = 0.5f;
		if (pPossessedSteal)
		{
			tChance = 1f;
		}
		if (this.takeItems(pActorTarget, tChance, 1))
		{
			pActorTarget.makeStunned(1f);
			this.checkAttackTypes();
			pActorTarget.checkAttackTypes();
			return true;
		}
		return false;
	}

	// Token: 0x06001120 RID: 4384 RVA: 0x000CE151 File Offset: 0x000CC351
	public bool tryToAcceptGift(Actor pActorTarget)
	{
		if (!this.understandsHowToUseItems())
		{
			return false;
		}
		if (this.takeItems(pActorTarget, 0.5f, 1))
		{
			this.checkAttackTypes();
			pActorTarget.checkAttackTypes();
			return true;
		}
		return false;
	}

	// Token: 0x06001121 RID: 4385 RVA: 0x000CE17C File Offset: 0x000CC37C
	public void takeAllResources(Actor pActorTarget)
	{
		if (pActorTarget.isCarryingResources())
		{
			foreach (KeyValuePair<string, ResourceContainer> tContainer in pActorTarget.inventory.getResources())
			{
				this.inventory.add(tContainer.Value);
			}
			pActorTarget.inventory.empty();
		}
	}

	// Token: 0x06001122 RID: 4386 RVA: 0x000CE1F4 File Offset: 0x000CC3F4
	public bool takeItems(Actor pActorTarget, float pChance = 1f, int pMaxItems = 0)
	{
		if (!this.understandsHowToUseItems())
		{
			return false;
		}
		if (!pActorTarget.hasEquipment())
		{
			return false;
		}
		bool result;
		using (ListPool<ActorEquipmentSlot> tList = new ListPool<ActorEquipmentSlot>(pActorTarget.equipment))
		{
			bool tAnyItemTaken = false;
			if (pMaxItems == 0)
			{
				pMaxItems = tList.Count;
			}
			foreach (ActorEquipmentSlot tTargetSlot in tList.LoopRandom(pMaxItems))
			{
				if (!tTargetSlot.isEmpty())
				{
					ActorEquipmentSlot tOurSlot = this.equipment.getSlot(tTargetSlot.type);
					Item tOurItem = tOurSlot.getItem();
					Item tTargetItem = tTargetSlot.getItem();
					if (!tTargetItem.isCursed() && (tOurSlot.isEmpty() || (!tOurItem.isCursed() && tTargetItem.getValue() > tOurItem.getValue())))
					{
						tAnyItemTaken = true;
						tTargetSlot.takeAwayItem();
						tOurSlot.setItem(tTargetItem, this);
						this.setStatsDirty();
						pActorTarget.setStatsDirty();
					}
				}
			}
			result = tAnyItemTaken;
		}
		return result;
	}

	// Token: 0x06001123 RID: 4387 RVA: 0x000CE300 File Offset: 0x000CC500
	public void addToInventory(string pResourceID, int pAmount)
	{
		this.inventory = this.inventory.add(pResourceID, pAmount);
		this.setItemSpriteRenderDirty();
	}

	// Token: 0x06001124 RID: 4388 RVA: 0x000CE31B File Offset: 0x000CC51B
	public void addToInventory(ResourceContainer pResourceContainer)
	{
		this.inventory = this.inventory.add(pResourceContainer);
		this.setItemSpriteRenderDirty();
	}

	// Token: 0x06001125 RID: 4389 RVA: 0x000CE335 File Offset: 0x000CC535
	public void takeFromInventory(string pID, int pAmount)
	{
		this.inventory = this.inventory.remove(pID, pAmount);
		this.setItemSpriteRenderDirty();
	}

	// Token: 0x06001126 RID: 4390 RVA: 0x000CE350 File Offset: 0x000CC550
	public void setSubspecies(Subspecies pObject)
	{
		World.world.subspecies.setDirtyUnits(this.subspecies);
		this.subspecies = pObject;
		World.world.subspecies.unitAdded(pObject);
		this.setStatsDirty();
	}

	// Token: 0x06001127 RID: 4391 RVA: 0x000CE384 File Offset: 0x000CC584
	public void joinLanguage(Language pLanguage)
	{
		if (this.language != pLanguage)
		{
			bool tHadLanguage = false;
			if (this.hasLanguage())
			{
				this.language.increaseSpeakersLost();
				tHadLanguage = true;
			}
			if (pLanguage != null)
			{
				if (!tHadLanguage)
				{
					pLanguage.countNewSpeaker();
				}
				else
				{
					pLanguage.countConversion();
				}
			}
		}
		this.setLanguage(pLanguage);
	}

	// Token: 0x06001128 RID: 4392 RVA: 0x000CE3CC File Offset: 0x000CC5CC
	public void setLanguage(Language pObject)
	{
		World.world.languages.setDirtyUnits(this.language);
		this.language = pObject;
		World.world.languages.unitAdded(pObject);
		this.setStatsDirty();
	}

	// Token: 0x06001129 RID: 4393 RVA: 0x000CE400 File Offset: 0x000CC600
	public void setPlot(Plot pObject)
	{
		World.world.plots.setDirtyUnits(this.plot);
		this.plot = pObject;
		World.world.plots.unitAdded(pObject);
		this.setStatsDirty();
	}

	// Token: 0x0600112A RID: 4394 RVA: 0x000CE434 File Offset: 0x000CC634
	public void setReligion(Religion pObject)
	{
		World.world.religions.setDirtyUnits(this.religion);
		this.religion = pObject;
		World.world.religions.unitAdded(pObject);
		this.setStatsDirty();
	}

	// Token: 0x0600112B RID: 4395 RVA: 0x000CE468 File Offset: 0x000CC668
	public void setFamily(Family pObject)
	{
		World.world.families.setDirtyUnits(this.family);
		this.family = pObject;
		World.world.families.unitAdded(pObject);
		this.setStatsDirty();
	}

	// Token: 0x0600112C RID: 4396 RVA: 0x000CE49C File Offset: 0x000CC69C
	public void setClan(Clan pObject)
	{
		World.world.clans.setDirtyUnits(this.clan);
		this.clan = pObject;
		World.world.clans.unitAdded(pObject);
		this.setStatsDirty();
	}

	// Token: 0x0600112D RID: 4397 RVA: 0x000CE4D0 File Offset: 0x000CC6D0
	public void setCulture(Culture pCulture)
	{
		World.world.cultures.setDirtyUnits(this.culture);
		this.culture = pCulture;
		World.world.cultures.unitAdded(pCulture);
		this.setStatsDirty();
	}

	// Token: 0x0600112E RID: 4398 RVA: 0x000CE504 File Offset: 0x000CC704
	internal void removeFromArmy()
	{
		if (this.hasArmy())
		{
			Army army = this.army;
			this.setArmy(null);
			army.checkCaptainRemoval(this);
		}
	}

	// Token: 0x0600112F RID: 4399 RVA: 0x000CE521 File Offset: 0x000CC721
	public void setArmy(Army pObject)
	{
		World.world.armies.setDirtyUnits(this.army);
		this.army = pObject;
		World.world.armies.unitAdded(pObject);
		this.setStatsDirty();
	}

	// Token: 0x06001130 RID: 4400 RVA: 0x000CE558 File Offset: 0x000CC758
	internal void setCity(City pCity)
	{
		if (this.city == pCity)
		{
			return;
		}
		if (this.city != null)
		{
			this.city.eventUnitRemoved(this.a);
		}
		World.world.cities.setDirtyUnits(this.city);
		this.city = pCity;
		if (this.city != null)
		{
			this.city.eventUnitAdded(this.a);
			this.setKingdom(this.city.kingdom);
		}
		World.world.cities.unitAdded(this.city);
		this.setStatsDirty();
	}

	// Token: 0x06001131 RID: 4401 RVA: 0x000CE5EC File Offset: 0x000CC7EC
	public void setMetasFromCity(City pCity)
	{
		if (pCity.hasCulture() && !this.hasCulture())
		{
			this.setCulture(pCity.culture);
		}
		if (pCity.hasLanguage() && !this.hasLanguage())
		{
			this.joinLanguage(pCity.language);
		}
		if (pCity.hasReligion() && !this.hasReligion())
		{
			this.setReligion(pCity.religion);
		}
	}

	// Token: 0x06001132 RID: 4402 RVA: 0x000CE64D File Offset: 0x000CC84D
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasClan()
	{
		return this.clan != null;
	}

	// Token: 0x06001133 RID: 4403 RVA: 0x000CE658 File Offset: 0x000CC858
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasSubspecies()
	{
		return this.subspecies != null;
	}

	// Token: 0x06001134 RID: 4404 RVA: 0x000CE663 File Offset: 0x000CC863
	public bool hasArmy()
	{
		return this.army != null;
	}

	// Token: 0x06001135 RID: 4405 RVA: 0x000CE66E File Offset: 0x000CC86E
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasFamily()
	{
		return this.family != null;
	}

	// Token: 0x06001136 RID: 4406 RVA: 0x000CE679 File Offset: 0x000CC879
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasLanguage()
	{
		return this.language != null;
	}

	// Token: 0x06001137 RID: 4407 RVA: 0x000CE684 File Offset: 0x000CC884
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasPlot()
	{
		return this.plot != null;
	}

	// Token: 0x06001138 RID: 4408 RVA: 0x000CE68F File Offset: 0x000CC88F
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasReligion()
	{
		return this.religion != null;
	}

	// Token: 0x06001139 RID: 4409 RVA: 0x000CE69C File Offset: 0x000CC89C
	public bool tryToConvertToReligion(Religion pReligion)
	{
		if (!this.hasSubspecies() || !this.subspecies.has_advanced_memory)
		{
			return false;
		}
		if (this.hasReligion() && this.religion == pReligion)
		{
			return false;
		}
		if (this.hasCulture() && !this.culture.isPossibleToConvertToOtherMeta())
		{
			return false;
		}
		this.setReligion(pReligion);
		pReligion.countConversion();
		EffectsLibrary.showMetaEventEffectConversion("fx_conversion_religion", this);
		return true;
	}

	// Token: 0x0600113A RID: 4410 RVA: 0x000CE704 File Offset: 0x000CC904
	public bool tryToConvertToCulture(Culture pCulture)
	{
		if (!this.hasSubspecies() || !this.subspecies.has_advanced_memory)
		{
			return false;
		}
		if (this.hasCulture() && this.culture == pCulture)
		{
			return false;
		}
		if (this.hasCulture() && !this.culture.isPossibleToConvertToOtherMeta())
		{
			return false;
		}
		bool flag = this.hasCulture();
		Culture culture = this.culture;
		this.setCulture(pCulture);
		if (flag)
		{
			pCulture.countConversion();
		}
		EffectsLibrary.showMetaEventEffectConversion("fx_conversion_culture", this);
		return true;
	}

	// Token: 0x0600113B RID: 4411 RVA: 0x000CE77C File Offset: 0x000CC97C
	public bool tryToConvertToLanguage(Language pLanguage)
	{
		if (!this.hasSubspecies() || !this.subspecies.has_advanced_communication)
		{
			return false;
		}
		if (this.hasLanguage() && this.language == pLanguage)
		{
			return false;
		}
		if (this.hasCulture() && !this.culture.isPossibleToConvertToOtherMeta())
		{
			return false;
		}
		this.joinLanguage(pLanguage);
		EffectsLibrary.showMetaEventEffectConversion("fx_conversion_language", this);
		return true;
	}

	// Token: 0x0600113C RID: 4412 RVA: 0x000CE7DD File Offset: 0x000CC9DD
	public void saveOriginFamily(long pID)
	{
		this.data.ancestor_family = pID;
	}

	// Token: 0x0600113D RID: 4413 RVA: 0x000CE7EC File Offset: 0x000CC9EC
	private void clearManagers()
	{
		if (this.hasClan())
		{
			World.world.clans.unitDied(this.clan);
			this.clan = null;
		}
		if (this.hasArmy())
		{
			World.world.armies.unitDied(this.army);
			this.army = null;
		}
		if (this.hasCulture())
		{
			World.world.cultures.unitDied(this.culture);
			this.culture = null;
		}
		if (this.hasFamily())
		{
			World.world.families.unitDied(this.family);
			this.family = null;
		}
		if (this.hasLanguage())
		{
			World.world.languages.unitDied(this.language);
			this.language = null;
		}
		if (this.hasPlot())
		{
			World.world.plots.unitDied(this.plot);
			this.plot = null;
		}
		if (this.hasReligion())
		{
			World.world.religions.unitDied(this.religion);
			this.religion = null;
		}
	}

	// Token: 0x0600113E RID: 4414 RVA: 0x000CE8F5 File Offset: 0x000CCAF5
	internal bool isCitizenJob(string pJob)
	{
		return this.citizen_job != null && this.citizen_job.id == pJob;
	}

	// Token: 0x0600113F RID: 4415 RVA: 0x000CE912 File Offset: 0x000CCB12
	public void forgetCulture()
	{
		this.makeConfused(-1f, false);
		if (this.hasCulture())
		{
			this.setCulture(null);
		}
	}

	// Token: 0x06001140 RID: 4416 RVA: 0x000CE92F File Offset: 0x000CCB2F
	public void forgetReligion()
	{
		this.makeConfused(-1f, false);
		if (this.hasReligion())
		{
			this.setReligion(null);
		}
	}

	// Token: 0x06001141 RID: 4417 RVA: 0x000CE94C File Offset: 0x000CCB4C
	public void forgetLanguage()
	{
		this.makeConfused(10f, false);
		if (this.hasLanguage())
		{
			this.joinLanguage(null);
		}
	}

	// Token: 0x06001142 RID: 4418 RVA: 0x000CE969 File Offset: 0x000CCB69
	public void forgetClan()
	{
		this.makeConfused(-1f, false);
		if (this.hasClan())
		{
			this.clan.tryForgetChief(this);
			this.setClan(null);
		}
	}

	// Token: 0x06001143 RID: 4419 RVA: 0x000CE992 File Offset: 0x000CCB92
	public void forgetKingdomAndCity()
	{
		this.makeConfused(-1f, false);
		this.removeFromPreviousFaction();
		if (base.isKingdomCiv())
		{
			this.setDefaultKingdom();
		}
	}

	// Token: 0x06001144 RID: 4420 RVA: 0x000CE9B4 File Offset: 0x000CCBB4
	public void tryToConvertActorToMetaFromActor(Actor pActor, bool pStunOnSuccess = true)
	{
		int tCounterSuccess = 0;
		if (pActor.hasCulture() && Randy.randomBool() && this.tryToConvertToCulture(pActor.culture))
		{
			tCounterSuccess++;
		}
		if (pActor.hasLanguage() && Randy.randomBool() && this.tryToConvertToLanguage(pActor.language))
		{
			tCounterSuccess++;
		}
		if (pActor.hasReligion() && Randy.randomBool() && this.tryToConvertToReligion(pActor.religion))
		{
			tCounterSuccess++;
		}
		if (!pStunOnSuccess)
		{
			return;
		}
		if (tCounterSuccess > 0)
		{
			this.makeStunned(5f);
			this.applyRandomForce(1.5f, 2f);
			base.addStatusEffect("voices_in_my_head", 0f, true);
			return;
		}
		if (Randy.randomChance(0.1f))
		{
			this.makeConfused(Randy.randomFloat(0.8f, 5f), false);
		}
	}

	// Token: 0x06001145 RID: 4421 RVA: 0x000CEA80 File Offset: 0x000CCC80
	public void joinCity(City pCity)
	{
		bool tCount = !this.asset.is_boat;
		if (this.city != pCity)
		{
			bool tHadCity = base.hasCity();
			if (tHadCity && tCount)
			{
				this.city.increaseLeft();
			}
			if (pCity != null)
			{
				if (pCity.kingdom != this.kingdom)
				{
					this.joinKingdom(pCity.kingdom);
				}
				if (tCount)
				{
					if (tHadCity)
					{
						pCity.increaseMoved();
					}
					else
					{
						pCity.increaseJoined();
					}
				}
			}
		}
		this.setCity(pCity);
	}

	// Token: 0x06001146 RID: 4422 RVA: 0x000CEAF8 File Offset: 0x000CCCF8
	public void joinKingdom(Kingdom pKingdom)
	{
		if (!this.asset.is_boat && this.kingdom != pKingdom)
		{
			bool tHadKingdom = base.hasKingdom();
			if (tHadKingdom && this.kingdom.isCiv())
			{
				this.kingdom.increaseLeft();
			}
			if (pKingdom != null && pKingdom.isCiv())
			{
				if (tHadKingdom)
				{
					pKingdom.increaseMoved();
				}
				else
				{
					pKingdom.increaseJoined();
				}
			}
		}
		this.setKingdom(pKingdom);
	}

	// Token: 0x06001147 RID: 4423 RVA: 0x000CEB63 File Offset: 0x000CCD63
	internal void setKingdom(Kingdom pKingdomToSet)
	{
		if (this.kingdom == pKingdomToSet)
		{
			return;
		}
		this.checkKingdom();
		this.kingdom = pKingdomToSet;
		this.checkKingdom();
		this.setStatsDirty();
	}

	// Token: 0x06001148 RID: 4424 RVA: 0x000CEB88 File Offset: 0x000CCD88
	private void checkKingdom()
	{
		if (base.hasKingdom())
		{
			if (this.kingdom.wild)
			{
				World.world.kingdoms_wild.setDirtyUnits(this.kingdom);
				return;
			}
			World.world.kingdoms.setDirtyUnits(this.kingdom);
		}
	}

	// Token: 0x06001149 RID: 4425 RVA: 0x000CEBD5 File Offset: 0x000CCDD5
	public void setForcedKingdom(Kingdom pForcedKingdom)
	{
		if (this.kingdom.asset.id == pForcedKingdom.asset.id)
		{
			return;
		}
		this.joinKingdom(pForcedKingdom);
	}

	// Token: 0x0600114A RID: 4426 RVA: 0x000CEC01 File Offset: 0x000CCE01
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasCulture()
	{
		return this.culture != null;
	}

	// Token: 0x0600114B RID: 4427 RVA: 0x000CEC0C File Offset: 0x000CCE0C
	public bool buildCityAndStartCivilization()
	{
		if (!World.world.cities.canStartNewCityCivilizationHere(this))
		{
			return false;
		}
		Kingdom kingdom = World.world.kingdoms.makeNewCivKingdom(this, null, true);
		City tNewCity = World.world.cities.buildFirstCivilizationCity(this);
		this.createDefaultCultureAndLanguageAndClan(tNewCity.name);
		kingdom.setUnitMetas(this);
		tNewCity.setUnitMetas(this);
		return true;
	}

	// Token: 0x0600114C RID: 4428 RVA: 0x000CEC6C File Offset: 0x000CCE6C
	public void createDefaultCultureAndLanguageAndClan(string pCultureName = null)
	{
		if (!this.hasClan())
		{
			World.world.clans.newClan(this, true);
		}
		if (!this.hasLanguage() && this.subspecies.has_advanced_communication)
		{
			Language tLanguage = World.world.languages.newLanguage(this, true);
			this.joinLanguage(tLanguage);
			tLanguage.convertSameSpeciesAroundUnit(this, false);
		}
		if (!this.hasCulture() && this.subspecies.has_advanced_memory)
		{
			Culture tCulture = World.world.cultures.newCulture(this, true);
			if (pCultureName != null)
			{
				tCulture.setName(pCultureName, false);
			}
			this.setCulture(tCulture);
			tCulture.convertSameSpeciesAroundUnit(this, false);
		}
	}

	// Token: 0x0600114D RID: 4429 RVA: 0x000CED0A File Offset: 0x000CCF0A
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void checkDefaultKingdom()
	{
		if (base.hasKingdom())
		{
			return;
		}
		this.setDefaultKingdom();
	}

	// Token: 0x0600114E RID: 4430 RVA: 0x000CED1B File Offset: 0x000CCF1B
	public void setDefaultKingdom()
	{
		this.setKingdom(World.world.kingdoms_wild.get(this.asset.kingdom_id_wild));
	}

	// Token: 0x0600114F RID: 4431 RVA: 0x000CED3D File Offset: 0x000CCF3D
	public void removeFromPreviousFaction()
	{
		this.stopBeingWarrior();
		if (this.isKing())
		{
			this.kingdom.kingLeftEvent();
		}
		this.joinCity(null);
	}

	// Token: 0x06001150 RID: 4432 RVA: 0x000CED60 File Offset: 0x000CCF60
	public bool wantsToSplitMeta()
	{
		return (!base.hasKingdom() || !base.isKingdomCiv() || !this.hasSubspecies() || this.kingdom.getMainSubspecies() != this.subspecies) && (this.hasTrait("ambitious") || base.hasStatus("inspired"));
	}

	// Token: 0x06001151 RID: 4433 RVA: 0x000CEDBC File Offset: 0x000CCFBC
	public NanoObject getMetaObjectOfType(MetaType pType)
	{
		switch (pType)
		{
		case MetaType.Subspecies:
			return this.subspecies;
		case MetaType.Family:
			return this.family;
		case MetaType.Language:
			return this.language;
		case MetaType.Culture:
			return this.culture;
		case MetaType.Religion:
			return this.religion;
		case MetaType.Clan:
			return this.clan;
		case MetaType.City:
			return this.city;
		case MetaType.Kingdom:
			return this.kingdom;
		case MetaType.Alliance:
			return this.kingdom.getAlliance();
		case MetaType.Army:
			return this.army;
		}
		return null;
	}

	// Token: 0x06001152 RID: 4434 RVA: 0x000CEE63 File Offset: 0x000CD063
	internal void setFlip(bool pFlip)
	{
		this.flip = pFlip;
	}

	// Token: 0x06001153 RID: 4435 RVA: 0x000CEE6C File Offset: 0x000CD06C
	public void precalcMovementSpeed(bool pForce = false)
	{
		if (!pForce)
		{
			if (!this.is_moving)
			{
				return;
			}
			if (this._precalc_movement_speed_skips > 0)
			{
				this._precalc_movement_speed_skips--;
				return;
			}
			this._precalc_movement_speed_skips = 5;
		}
		bool tIsInAir = this.isInAir();
		bool tIsWaterCreature = this.isWaterCreature();
		float tWalkMultiplier = 1f;
		if (this.asset.ignore_tile_speed_multiplier || tIsInAir || tIsWaterCreature)
		{
			tWalkMultiplier = 1f;
		}
		else if (this.current_tile.is_liquid)
		{
			if (this.getStamina() <= 0 && !this.hasTag("fast_swimming"))
			{
				tWalkMultiplier *= 0.4f;
			}
		}
		else if (!string.IsNullOrEmpty(this.current_tile.Type.ignore_walk_multiplier_if_tag) && !this.stats.hasTag(this.current_tile.Type.ignore_walk_multiplier_if_tag))
		{
			tWalkMultiplier = this.current_tile.Type.walk_multiplier;
		}
		if (!this.asset.ignore_tile_speed_multiplier && this._is_in_liquid && this.hasTag("fast_swimming"))
		{
			tWalkMultiplier *= 5f;
		}
		if (this.hasTask() && this.ai.task.speed_multiplier != 1f)
		{
			tWalkMultiplier *= this.ai.task.speed_multiplier;
		}
		float tSpeed = this.stats["speed"] * tWalkMultiplier;
		if (!tIsInAir && WorldLawLibrary.world_law_entanglewood.isEnabled())
		{
			Building building = this.current_tile.building;
			if (building != null && building.asset.flora_type == FloraType.Tree)
			{
				tSpeed *= 0.8f;
			}
		}
		if (tSpeed < 1f)
		{
			tSpeed = 1f;
		}
		if (DebugConfig.isOn(DebugOption.UnitsAlwaysFast))
		{
			tSpeed *= 20f;
		}
		tSpeed *= 0.4f;
		this._current_combined_movement_speed = tSpeed * SimGlobals.m.unit_speed_multiplier;
		if (this.tile_target != null)
		{
			float tDistFinal = Toolbox.DistVec2Float(this.current_position, this.tile_target.posV3);
			if (tDistFinal < 1f && this._current_combined_movement_speed > 3f)
			{
				float tSlodownFactor = Mathf.Lerp(1f, 0.3f, 1f - tDistFinal);
				this._current_combined_movement_speed *= tSlodownFactor;
			}
		}
	}

	// Token: 0x06001154 RID: 4436 RVA: 0x000CF089 File Offset: 0x000CD289
	internal bool checkFlip()
	{
		return this.asset.check_flip(this, null);
	}

	// Token: 0x06001155 RID: 4437 RVA: 0x000CF0A0 File Offset: 0x000CD2A0
	protected void updateMovement(float pElapsed, float pWalkedDistance = 0f)
	{
		float tSqrRemainingDistance = Toolbox.DistVec2Float(this.current_position, this.next_step_position);
		if (this.asset.can_flip && this.checkFlip())
		{
			if (this.current_position.x < this.next_step_position.x)
			{
				this.setFlip(true);
			}
			else
			{
				this.setFlip(false);
			}
		}
		float tDelta = this.getMovementDelta(pElapsed, pWalkedDistance);
		if (tSqrRemainingDistance < tDelta)
		{
			tDelta = tSqrRemainingDistance;
			this.current_position = this.next_step_position;
			if (this.isUsingPath())
			{
				this.updatePathMovement();
			}
			else
			{
				this.stopMovement();
			}
			if (this.is_moving)
			{
				this.updateMovement(pElapsed, pWalkedDistance + tDelta);
				return;
			}
		}
		else
		{
			this.current_position = Vector2.MoveTowards(this.current_position, this.next_step_position, tDelta);
		}
	}

	// Token: 0x06001156 RID: 4438 RVA: 0x000CF158 File Offset: 0x000CD358
	private float getMovementDelta(float pElapsed, float pWalkedDistance = 0f)
	{
		float tDelta = this._current_combined_movement_speed;
		tDelta *= pElapsed;
		tDelta -= pWalkedDistance;
		if (tDelta < 0f)
		{
			tDelta = 0f;
		}
		return tDelta;
	}

	// Token: 0x06001157 RID: 4439 RVA: 0x000CF184 File Offset: 0x000CD384
	internal void updateMovementPossessedFlip()
	{
		if (InputHelpers.mouseSupported)
		{
			this.checkFlipAgainstTargetPosition(World.world.getMousePos());
			return;
		}
		if (ControllableUnit.isMovementActionActive() && !this.isPossessionAttackJustHappened())
		{
			Vector2 tPos = ControllableUnit.getMovementVector() + this.current_position;
			this.checkFlipAgainstTargetPosition(tPos);
		}
	}

	// Token: 0x06001158 RID: 4440 RVA: 0x000CF1D0 File Offset: 0x000CD3D0
	public void checkFlipAgainstTargetPosition(Vector2 pPosition)
	{
		if (!this.asset.can_flip)
		{
			return;
		}
		if (this.current_position.x < pPosition.x)
		{
			this.setFlip(true);
			return;
		}
		this.setFlip(false);
	}

	// Token: 0x06001159 RID: 4441 RVA: 0x000CF204 File Offset: 0x000CD404
	internal float updatePossessedMovementTowards(float pElapsed, Vector2 pMovementPoint)
	{
		this.precalcMovementSpeed(true);
		if (this.asset.can_flip && this.checkFlip())
		{
			float tMismatchFactor = this.getMismatchFactorForSideMovement(pMovementPoint);
			if (tMismatchFactor > 0.2f)
			{
				pElapsed *= Mathf.Lerp(1f, 0.8f, tMismatchFactor);
			}
		}
		float tDelta = this.getMovementDelta(pElapsed, 0f);
		Vector2 tNewPos = Vector2.MoveTowards(this.current_position, pMovementPoint, tDelta);
		tNewPos = this.checkVelocityAgainstBlock(tNewPos);
		if (!Toolbox.inMapBorder(ref tNewPos))
		{
			return 0f;
		}
		this.current_position = tNewPos;
		return tDelta;
	}

	// Token: 0x0600115A RID: 4442 RVA: 0x000CF28B File Offset: 0x000CD48B
	public Vector2 getPossessionControlTargetPosition()
	{
		return ControllableUnit.getClickVector();
	}

	// Token: 0x0600115B RID: 4443 RVA: 0x000CF292 File Offset: 0x000CD492
	public Vector2 getPossessionControlTargetPositionMovementVector()
	{
		if (InputHelpers.mouseSupported)
		{
			return ControllableUnit.getClickVector();
		}
		return ControllableUnit.getMovementVector() + this.current_position;
	}

	// Token: 0x0600115C RID: 4444 RVA: 0x000CF2B4 File Offset: 0x000CD4B4
	private float getMismatchFactorForSideMovement(Vector2 pMovementPoint)
	{
		Vector2 tCursorPos = World.world.getMousePos();
		bool tSideLook = this.current_position.x < tCursorPos.x;
		bool tSideMove = this.current_position.x < pMovementPoint.x;
		bool flag = this.current_position.y < tCursorPos.y;
		bool tUpMove = this.current_position.y < pMovementPoint.y;
		float tDeltaX = Mathf.Abs(pMovementPoint.x - this.current_position.x);
		float tDeltaY = Mathf.Abs(pMovementPoint.y - this.current_position.y);
		float tMismatchFactor = 0f;
		if (tSideLook != tSideMove)
		{
			tMismatchFactor += tDeltaX;
		}
		if (flag != tUpMove)
		{
			tMismatchFactor += tDeltaY;
		}
		return Mathf.Clamp01(tMismatchFactor / (tDeltaX + tDeltaY + 0.001f));
	}

	// Token: 0x0600115D RID: 4445 RVA: 0x000CF380 File Offset: 0x000CD580
	internal void findCurrentTile(bool pCheckNeighbours = true)
	{
		Vector3 tCurrentPosition = this.current_position;
		if (!this.dirty_current_tile && tCurrentPosition.x == this.lastX && tCurrentPosition.y == this.lastY)
		{
			return;
		}
		this.dirty_current_tile = false;
		this.lastX = this.current_position.x;
		this.lastY = this.current_position.y;
		if (this.current_tile != null && Toolbox.SquaredDist(this.current_tile.posV3.x, this.current_tile.posV3.y, tCurrentPosition.x, tCurrentPosition.y) < 0.16000001f)
		{
			return;
		}
		WorldTile tNewTile = Toolbox.getTileAt(tCurrentPosition.x, tCurrentPosition.y);
		this.setCurrentTile(tNewTile);
		if (Toolbox.SquaredDist(tNewTile.posV3.x, tNewTile.posV3.y, tCurrentPosition.x, tCurrentPosition.y) < 0.09f)
		{
			return;
		}
		if (!pCheckNeighbours)
		{
			return;
		}
		if (this.isFlying())
		{
			return;
		}
		bool tMustAvoidGround = this.mustAvoidGround();
		if (tNewTile.Type.lava && this.asset.die_in_lava)
		{
			foreach (WorldTile tTile in tNewTile.neighboursAll)
			{
				if (tTile.Type.ground)
				{
					this.setCurrentTile(tTile);
					break;
				}
			}
		}
		if (tNewTile.Type.ocean && this.isDamagedByOcean())
		{
			foreach (WorldTile tTile2 in tNewTile.neighboursAll)
			{
				if (!tTile2.is_liquid)
				{
					this.setCurrentTile(tTile2);
					break;
				}
			}
		}
		if (tNewTile.Type.block && !this.isFlying() && !tMustAvoidGround)
		{
			foreach (WorldTile tTile3 in tNewTile.neighboursAll)
			{
				if (tTile3.Type.ground)
				{
					this.setCurrentTile(tTile3);
					break;
				}
			}
		}
		if (!tNewTile.is_liquid && tMustAvoidGround)
		{
			foreach (WorldTile tTile4 in tNewTile.neighboursAll)
			{
				if (tTile4.is_liquid)
				{
					this.setCurrentTile(tTile4);
					break;
				}
			}
		}
		if (tNewTile.isOnFire() && !this.isImmuneToFire())
		{
			foreach (WorldTile tTile5 in tNewTile.neighboursAll)
			{
				if (!tTile5.isOnFire())
				{
					this.setCurrentTile(tTile5);
					return;
				}
			}
		}
	}

	// Token: 0x0600115E RID: 4446 RVA: 0x000CF5F7 File Offset: 0x000CD7F7
	internal void checkFindCurrentTile()
	{
		if (this.dirty_current_tile || (this._next_step_tile != null && (float)Toolbox.SquaredDistTile(this.current_tile, this._next_step_tile) > 4f))
		{
			this.findCurrentTile(true);
		}
	}

	// Token: 0x0600115F RID: 4447 RVA: 0x000CF629 File Offset: 0x000CD829
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal void setTileTarget(WorldTile pTile)
	{
		this.clearTileTarget();
		this.tile_target = pTile;
		this.tile_target.setTargetedBy(this.a);
	}

	// Token: 0x06001160 RID: 4448 RVA: 0x000CF649 File Offset: 0x000CD849
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal void clearTileTarget()
	{
		if (this.tile_target == null)
		{
			return;
		}
		if (this.tile_target.isTargetedBy(this))
		{
			this.tile_target.cleanTargetedBy();
		}
		this.tile_target = null;
		this.scheduled_tile_target = null;
	}

	// Token: 0x06001161 RID: 4449 RVA: 0x000CF67B File Offset: 0x000CD87B
	internal void clearOldPath()
	{
		this.current_path.Clear();
		this.current_path_global = null;
		this.current_path_index = 0;
	}

	// Token: 0x06001162 RID: 4450 RVA: 0x000CF698 File Offset: 0x000CD898
	public virtual void updatePathMovement()
	{
		if (!this.isFollowingLocalPath())
		{
			this.setNotMoving();
			if (this.split_path != SplitPathStatus.Split)
			{
				this.split_path = SplitPathStatus.Split;
				this.timer_action = Randy.randomFloat(0f, this.asset.path_movement_timeout);
				return;
			}
			this.split_path = SplitPathStatus.Normal;
			if (this.tile_target != null)
			{
				this.goTo(this.tile_target, false, false, false, 0);
			}
			return;
		}
		else
		{
			WorldTile tMoveTile = this.current_path[this.current_path_index];
			TileTypeBase tMoveTileType = tMoveTile.Type;
			this.current_path_index++;
			if (tMoveTileType.damaged_when_walked)
			{
				this.current_tile.tryToBreak();
			}
			bool tCheckFailure = true;
			if (this._has_status_strange_urge)
			{
				tCheckFailure = false;
			}
			if (tCheckFailure)
			{
				if (this.asset.is_boat && !tMoveTile.isGoodForBoat())
				{
					BaseActionActor baseActionActor = this.callbacks_cancel_path_movement;
					if (baseActionActor != null)
					{
						baseActionActor(this);
					}
					this.cancelAllBeh();
					return;
				}
				if (tMoveTileType.block && !this.ignoresBlocks())
				{
					if (!this.hasTask() || !this.ai.task.move_from_block)
					{
						this.cancelAllBeh();
						return;
					}
				}
				else
				{
					if (this.asset.die_in_lava && tMoveTileType.lava)
					{
						this.cancelAllBeh();
						return;
					}
					if (this.isDamagedByOcean() && tMoveTileType.ocean && !this._is_in_liquid)
					{
						this.cancelAllBeh();
						return;
					}
				}
			}
			if (!tMoveTile.isOnFire() || this.isImmuneToFire() || base.hasStatus("burning") || this.current_tile.isOnFire())
			{
				this.moveTo(tMoveTile);
				return;
			}
			if (this.hasTask() && this.ai.task.is_fireman)
			{
				this.stopMovement();
				return;
			}
			this.cancelAllBeh();
			this.makeWait(0.3f);
			return;
		}
	}

	// Token: 0x06001163 RID: 4451 RVA: 0x000CF84A File Offset: 0x000CDA4A
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal bool isFollowingLocalPath()
	{
		return this.current_path.Count > 0 && this.current_path_index < this.current_path.Count;
	}

	// Token: 0x06001164 RID: 4452 RVA: 0x000CF870 File Offset: 0x000CDA70
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal bool isUsingPath()
	{
		return this.isFollowingLocalPath() || this.current_path_global != null;
	}

	// Token: 0x06001165 RID: 4453 RVA: 0x000CF885 File Offset: 0x000CDA85
	public ExecuteEvent goTo(WorldTile pTile, bool pPathOnWater = false, bool pWalkOnBlocks = false, bool pWalkOnLava = false, int pLimitPathfindingRegions = 0)
	{
		this.setTileTarget(pTile);
		return ActorMove.goTo(this, pTile, pPathOnWater, pWalkOnBlocks, pWalkOnLava, pLimitPathfindingRegions);
	}

	// Token: 0x06001166 RID: 4454 RVA: 0x000CF89B File Offset: 0x000CDA9B
	public void clearPathForCalibration()
	{
		this.clearOldPath();
		this.next_step_position = this.current_position;
	}

	// Token: 0x06001167 RID: 4455 RVA: 0x000CF8AF File Offset: 0x000CDAAF
	private void finishStrangeUrgeMovement()
	{
		this._has_status_strange_urge = false;
		base.finishStatusEffect("strange_urge");
		this.setTask("strange_urge_finish", true, false, false);
	}

	// Token: 0x06001168 RID: 4456 RVA: 0x000CF8D4 File Offset: 0x000CDAD4
	public void stopMovement()
	{
		this.split_path = SplitPathStatus.Normal;
		this._next_step_tile = null;
		this.clearOldPath();
		this.clearTileTarget();
		this.setNotMoving();
		this.next_step_position = Globals.emptyVector;
		this.dirty_current_tile = true;
		if (this._has_status_strange_urge)
		{
			this.finishStrangeUrgeMovement();
		}
	}

	// Token: 0x06001169 RID: 4457 RVA: 0x000CF926 File Offset: 0x000CDB26
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void setIsMoving()
	{
		if (this.is_moving)
		{
			return;
		}
		this._is_moving = true;
		this.batch.c_update_movement.Add(this.a);
	}

	// Token: 0x0600116A RID: 4458 RVA: 0x000CF94E File Offset: 0x000CDB4E
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void setNotMoving()
	{
		if (!this.is_moving)
		{
			return;
		}
		this._is_moving = false;
		this.batch.c_update_movement.Remove(this.a);
	}

	// Token: 0x0600116B RID: 4459 RVA: 0x000CF976 File Offset: 0x000CDB76
	public void setPossessedMovement(bool pValue)
	{
		this._possessed_movement = pValue;
	}

	// Token: 0x0600116C RID: 4460 RVA: 0x000CF980 File Offset: 0x000CDB80
	public void moveTo(WorldTile pTileTarget)
	{
		this.setIsMoving();
		if (!this.has_attack_target && this.current_tile != null && pTileTarget.isOnFire() && !this.current_tile.isOnFire() && !this.isImmuneToFire())
		{
			this.cancelAllBeh();
			return;
		}
		this._next_step_tile = pTileTarget;
		if ((float)Toolbox.SquaredDistTile(this.current_tile, pTileTarget) > 4f)
		{
			this.dirty_current_tile = true;
		}
		else
		{
			this.setCurrentTile(this._next_step_tile);
		}
		this.checkStepActionForTile(this.current_tile);
		Vector3 tEnd = new Vector3(pTileTarget.posV3.x, pTileTarget.posV3.y);
		this.next_step_position = tEnd;
	}

	// Token: 0x0600116D RID: 4461 RVA: 0x000CFA2C File Offset: 0x000CDC2C
	public Vector3 updatePos()
	{
		Vector3 tCurPosition = this.current_position;
		Vector2 tShakePos = this.shake_offset;
		Vector2 tMoveJumpOffset = this.move_jump_offset;
		float xx = tCurPosition.x + tMoveJumpOffset.x + tShakePos.x;
		float yy = tCurPosition.y + tMoveJumpOffset.y + tShakePos.y;
		yy += this.position_height;
		this.current_shadow_position.Set(tCurPosition.x + tShakePos.x, tCurPosition.y + tShakePos.y);
		float zz = this.position_height;
		this.cur_transform_position.Set(xx, yy, zz);
		return this.cur_transform_position;
	}

	// Token: 0x0600116E RID: 4462 RVA: 0x000CFACC File Offset: 0x000CDCCC
	public void stayInBuilding(Building pBuilding)
	{
		this.is_inside_building = true;
		this.inside_building = pBuilding;
	}

	// Token: 0x0600116F RID: 4463 RVA: 0x000CFADC File Offset: 0x000CDCDC
	internal void exitBuilding()
	{
		if (!this.is_inside_building)
		{
			return;
		}
		this.timer_action = 0f;
		this.is_inside_building = false;
		this.inside_building = null;
	}

	// Token: 0x06001170 RID: 4464 RVA: 0x000CFB00 File Offset: 0x000CDD00
	internal void embarkInto(Boat pBoat)
	{
		this.stopMovement();
		this.data.transportID = pBoat.actor.data.id;
		this.is_inside_boat = true;
		this.inside_boat = pBoat;
		this.inside_boat.addPassenger(this);
		this.setTask("sit_inside_boat", true, false, false);
		this.ai.update();
	}

	// Token: 0x06001171 RID: 4465 RVA: 0x000CFB61 File Offset: 0x000CDD61
	internal void disembarkTo(Boat pBoat, WorldTile pTile)
	{
		this.spawnOn(pTile, 0f);
		this.data.transportID = -1L;
		this.exitBoat();
		this.setTask("short_move", true, false, false);
	}

	// Token: 0x06001172 RID: 4466 RVA: 0x000CFB90 File Offset: 0x000CDD90
	internal void exitBoat()
	{
		this.inside_boat = null;
		this.is_inside_boat = false;
		this.dirty_current_tile = true;
	}

	// Token: 0x06001173 RID: 4467 RVA: 0x000CFBA7 File Offset: 0x000CDDA7
	internal void changeMoveJumpOffset(float pValue)
	{
		this.move_jump_offset.y = this.move_jump_offset.y + pValue;
		if (this.move_jump_offset.y < 0f)
		{
			this.move_jump_offset.y = 0f;
		}
	}

	// Token: 0x06001174 RID: 4468 RVA: 0x000CFBDB File Offset: 0x000CDDDB
	internal void setCurrentTile(WorldTile pTile)
	{
		this.current_tile = pTile;
	}

	// Token: 0x06001175 RID: 4469 RVA: 0x000CFBE4 File Offset: 0x000CDDE4
	internal void setCurrentTilePosition(WorldTile pTile)
	{
		this.setCurrentTile(pTile);
		this.current_position.Set(pTile.posV3.x, pTile.posV3.y);
	}

	// Token: 0x06001176 RID: 4470 RVA: 0x000CFC10 File Offset: 0x000CDE10
	protected void updateWalkJump(float pElapsed)
	{
		if (!this.is_visible && this.move_jump_offset.y == 0f)
		{
			return;
		}
		if (this.position_height > 0f)
		{
			return;
		}
		if (this.asset.disable_jump_animation)
		{
			return;
		}
		if (!this.is_moving)
		{
			if (this.move_jump_offset.y == 0f && (this._jump_time == 0f || this.isAffectedByLiquid()))
			{
				return;
			}
		}
		else if ((!this.is_moving && this._jump_time == 0f) || this.isAffectedByLiquid())
		{
			return;
		}
		this._jump_time += World.world.elapsed * 6f;
		if (this._jump_time >= 1f)
		{
			this.changeMoveJumpOffset(-3f * pElapsed);
		}
		else
		{
			this.changeMoveJumpOffset(3f * pElapsed);
		}
		if (this._jump_time >= 2f)
		{
			this._jump_time = 0f;
			this.changeMoveJumpOffset(0f);
		}
		if (this.asset.rotating_animation)
		{
			this.target_angle.z = this.target_angle.z + -this.move_jump_offset.y * 200f * World.world.elapsed;
		}
	}

	// Token: 0x06001177 RID: 4471 RVA: 0x000CFD44 File Offset: 0x000CDF44
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool inMapBorder()
	{
		return Toolbox.inMapBorder(ref this.current_position);
	}

	// Token: 0x06001178 RID: 4472 RVA: 0x000CFD54 File Offset: 0x000CDF54
	protected virtual void updateVelocity()
	{
		if (!this.under_forces)
		{
			return;
		}
		this.dirty_current_tile = true;
		float tElapsed = World.world.fixed_delta_time;
		float tDirectionSpeed = tElapsed * this.velocity_speed;
		float tDampMod = this.stats["mass"] * SimGlobals.m.gravity;
		tDampMod = Mathf.Min(tDampMod, 20f);
		float tVelocity = this.velocity.z * tElapsed * tDampMod;
		this.position_height += tVelocity;
		this.velocity.z = this.velocity.z - tElapsed * tDampMod * 0.3f;
		Vector3 tCurrentPosition = this.current_position;
		Vector2 tNewPos = new Vector2(tCurrentPosition.x + this.velocity.x * tDirectionSpeed, tCurrentPosition.y + this.velocity.y * tDirectionSpeed);
		tNewPos = this.checkVelocityAgainstBlock(tNewPos);
		this.current_position.Set(tNewPos.x, tNewPos.y);
		if (this.position_height < 0f)
		{
			this.position_height = 0f;
			this.velocity.z = 0f;
		}
		if (this.position_height <= 0f)
		{
			this.stopForce();
		}
	}

	// Token: 0x06001179 RID: 4473 RVA: 0x000CFE80 File Offset: 0x000CE080
	private Vector2 checkVelocityAgainstBlock(Vector2 pNewPos)
	{
		WorldTile tTileAtCoords = Toolbox.getTileAt(pNewPos.x, pNewPos.y);
		if (this.current_tile.Type.block && (!this.current_tile.Type.mountains || tTileAtCoords.Type.mountains))
		{
			return pNewPos;
		}
		if (tTileAtCoords == this.current_tile)
		{
			return pNewPos;
		}
		if (this.asset.is_boat)
		{
			if (tTileAtCoords.Type.liquid)
			{
				return pNewPos;
			}
		}
		else
		{
			if (!tTileAtCoords.Type.block)
			{
				return pNewPos;
			}
			if (this.getHeight() > tTileAtCoords.Type.block_height)
			{
				return pNewPos;
			}
		}
		Vector2 tWallNormal = this.getWallNormal(pNewPos, tTileAtCoords.posV3);
		float tElasticity = 0.8f;
		float tDotProduct = this.velocity.x * tWallNormal.x + this.velocity.y * tWallNormal.y;
		float tReflectionX = this.velocity.x - 2f * tDotProduct * tWallNormal.x;
		float tReflectionY = this.velocity.y - 2f * tDotProduct * tWallNormal.y;
		this.velocity.x = tReflectionX * tElasticity;
		this.velocity.y = tReflectionY * tElasticity;
		pNewPos.x = this.current_position.x;
		pNewPos.y = this.current_position.y;
		return pNewPos;
	}

	// Token: 0x0600117A RID: 4474 RVA: 0x000CFFD8 File Offset: 0x000CE1D8
	private Vector2 getWallNormal(Vector2 pPosition, Vector2 pBlockPosition)
	{
		Vector2 dirToObject = (pPosition - pBlockPosition).normalized;
		if (Mathf.Abs(dirToObject.x) > Mathf.Abs(dirToObject.y))
		{
			return new Vector2(Mathf.Sign(dirToObject.x), 0f);
		}
		return new Vector2(0f, Mathf.Sign(dirToObject.y));
	}

	// Token: 0x0600117B RID: 4475 RVA: 0x000D0038 File Offset: 0x000CE238
	public void prepareForSave()
	{
		this.saveCoordinates();
		this.saveAssetID();
		this.saveProfession();
		this.saveHomeBuilding();
		this.saveEquipment();
		this.saveLover();
		this.saveCity();
		this.saveKingdomCiv();
		this.saveCulture();
		this.saveClan();
		this.saveSubspecies();
		this.saveFamily();
		this.saveArmy();
		this.saveLanguage();
		this.savePlot();
		this.saveReligion();
		this.saveTraits();
		this.finishSaving();
	}

	// Token: 0x0600117C RID: 4476 RVA: 0x000D00B4 File Offset: 0x000CE2B4
	private void saveCoordinates()
	{
		this.data.x = this.current_tile.pos.x;
		this.data.y = this.current_tile.pos.y;
	}

	// Token: 0x0600117D RID: 4477 RVA: 0x000D00FD File Offset: 0x000CE2FD
	private void saveAssetID()
	{
		this.data.asset_id = this.asset.id;
	}

	// Token: 0x0600117E RID: 4478 RVA: 0x000D0115 File Offset: 0x000CE315
	private void saveProfession()
	{
		this.data.profession = this._profession;
	}

	// Token: 0x0600117F RID: 4479 RVA: 0x000D0128 File Offset: 0x000CE328
	private void saveHomeBuilding()
	{
		if (this._home_building != null && this._home_building.isUsable() && !this._home_building.isAbandoned())
		{
			this.data.homeBuildingID = this._home_building.data.id;
			return;
		}
		this.data.homeBuildingID = -1L;
	}

	// Token: 0x06001180 RID: 4480 RVA: 0x000D0180 File Offset: 0x000CE380
	private void saveEquipment()
	{
		if (this.hasEquipment())
		{
			List<long> tItems = this.equipment.getDataForSave();
			this.data.saved_items = tItems;
		}
	}

	// Token: 0x06001181 RID: 4481 RVA: 0x000D01AD File Offset: 0x000CE3AD
	private void saveLover()
	{
		if (this.hasLover())
		{
			this.data.lover = this.lover.data.id;
			return;
		}
		this.data.lover = -1L;
	}

	// Token: 0x06001182 RID: 4482 RVA: 0x000D01E0 File Offset: 0x000CE3E0
	private void saveCity()
	{
		if (base.hasCity() && this.city.isAlive())
		{
			this.data.cityID = this.city.id;
			return;
		}
		this.data.cityID = -1L;
	}

	// Token: 0x06001183 RID: 4483 RVA: 0x000D021B File Offset: 0x000CE41B
	private void saveKingdomCiv()
	{
		if (base.isKingdomCiv())
		{
			this.data.civ_kingdom_id = this.kingdom.id;
			return;
		}
		this.data.civ_kingdom_id = -1L;
	}

	// Token: 0x06001184 RID: 4484 RVA: 0x000D0249 File Offset: 0x000CE449
	private void saveCulture()
	{
		if (this.hasCulture())
		{
			this.data.culture = this.culture.id;
			return;
		}
		this.data.culture = -1L;
	}

	// Token: 0x06001185 RID: 4485 RVA: 0x000D0277 File Offset: 0x000CE477
	private void saveClan()
	{
		if (this.hasClan())
		{
			this.data.clan = this.clan.id;
			return;
		}
		this.data.clan = -1L;
	}

	// Token: 0x06001186 RID: 4486 RVA: 0x000D02A5 File Offset: 0x000CE4A5
	private void saveSubspecies()
	{
		if (this.hasSubspecies())
		{
			this.data.subspecies = this.subspecies.id;
			return;
		}
		this.data.subspecies = -1L;
	}

	// Token: 0x06001187 RID: 4487 RVA: 0x000D02D3 File Offset: 0x000CE4D3
	private void saveFamily()
	{
		if (this.hasFamily())
		{
			this.data.family = this.family.id;
			return;
		}
		this.data.family = -1L;
	}

	// Token: 0x06001188 RID: 4488 RVA: 0x000D0301 File Offset: 0x000CE501
	private void saveArmy()
	{
		if (this.hasArmy())
		{
			this.data.army = this.army.id;
			return;
		}
		this.data.army = -1L;
	}

	// Token: 0x06001189 RID: 4489 RVA: 0x000D032F File Offset: 0x000CE52F
	private void saveLanguage()
	{
		if (this.hasLanguage())
		{
			this.data.language = this.language.id;
			return;
		}
		this.data.language = -1L;
	}

	// Token: 0x0600118A RID: 4490 RVA: 0x000D035D File Offset: 0x000CE55D
	private void savePlot()
	{
		if (this.hasPlot())
		{
			this.data.plot = this.plot.id;
			return;
		}
		this.data.plot = -1L;
	}

	// Token: 0x0600118B RID: 4491 RVA: 0x000D038B File Offset: 0x000CE58B
	private void saveReligion()
	{
		if (this.hasReligion())
		{
			this.data.religion = this.religion.id;
			return;
		}
		this.data.religion = -1L;
	}

	// Token: 0x0600118C RID: 4492 RVA: 0x000D03B9 File Offset: 0x000CE5B9
	private void saveTraits()
	{
		this.data.saved_traits = Toolbox.getListForSave<ActorTrait>(this.getTraits());
	}

	// Token: 0x0600118D RID: 4493 RVA: 0x000D03D1 File Offset: 0x000CE5D1
	private void finishSaving()
	{
		this.data.save();
	}

	// Token: 0x0600118E RID: 4494 RVA: 0x000D03E0 File Offset: 0x000CE5E0
	public void loadFromSave()
	{
		this.setStatsDirty();
		TraitTools.loadTraits(this, this.data.saved_traits);
		foreach (ActorTrait tTrait in this.traits)
		{
			WorldActionTrait action_on_augmentation_load = tTrait.action_on_augmentation_load;
			if (action_on_augmentation_load != null)
			{
				action_on_augmentation_load(this, tTrait);
			}
		}
		if (this.isSapient() && this.is_profession_nothing)
		{
			this.data.profession = UnitProfession.Unit;
		}
		this.setProfession(this.data.profession, false);
		City tCity = World.world.cities.get(this.data.cityID);
		Kingdom tKingdom = World.world.kingdoms.get(this.data.civ_kingdom_id);
		if (tCity != null && !tCity.isNeutral())
		{
			this.setCity(tCity);
		}
		if (tKingdom != null)
		{
			this.setKingdom(tKingdom);
		}
		if (this.hasEquipment())
		{
			foreach (ActorEquipmentSlot tSlot in this.equipment)
			{
				if (!tSlot.isEmpty())
				{
					Item tItem = tSlot.getItem();
					int i = 0;
					while (i < tItem.data.modifiers.Count)
					{
						if (AssetManager.items_modifiers.get(tItem.data.modifiers[i]) == null)
						{
							tItem.data.modifiers.RemoveAt(i);
						}
						else
						{
							i++;
						}
					}
				}
			}
		}
		if (this.data.inventory.isEmpty())
		{
			this.data.inventory.empty();
		}
		foreach (Actor actor in this.getParents())
		{
			actor.increaseChildren();
		}
		BaseActionActor action_on_load = this.asset.action_on_load;
		if (action_on_load == null)
		{
			return;
		}
		action_on_load(this);
	}

	// Token: 0x0600118F RID: 4495 RVA: 0x000D05FC File Offset: 0x000CE7FC
	private void countDeath(AttackType pType)
	{
		World.world.game_stats.data.creaturesDied += 1L;
		MapStats map_stats = World.world.map_stats;
		long num = map_stats.deaths;
		map_stats.deaths = num + 1L;
		switch (pType)
		{
		case AttackType.Acid:
		{
			MapStats map_stats2 = World.world.map_stats;
			num = map_stats2.deaths_acid;
			map_stats2.deaths_acid = num + 1L;
			break;
		}
		case AttackType.Fire:
		{
			MapStats map_stats3 = World.world.map_stats;
			num = map_stats3.deaths_fire;
			map_stats3.deaths_fire = num + 1L;
			break;
		}
		case AttackType.Plague:
		{
			MapStats map_stats4 = World.world.map_stats;
			num = map_stats4.deaths_plague;
			map_stats4.deaths_plague = num + 1L;
			break;
		}
		case AttackType.Infection:
		{
			MapStats map_stats5 = World.world.map_stats;
			num = map_stats5.deaths_infection;
			map_stats5.deaths_infection = num + 1L;
			break;
		}
		case AttackType.Tumor:
		{
			MapStats map_stats6 = World.world.map_stats;
			num = map_stats6.deaths_tumor;
			map_stats6.deaths_tumor = num + 1L;
			break;
		}
		case AttackType.Other:
		case AttackType.AshFever:
		case AttackType.None:
			break;
		case AttackType.Divine:
		{
			MapStats map_stats7 = World.world.map_stats;
			num = map_stats7.deaths_divine;
			map_stats7.deaths_divine = num + 1L;
			break;
		}
		case AttackType.Metamorphosis:
		{
			MapStats map_stats8 = World.world.map_stats;
			num = map_stats8.metamorphosis;
			map_stats8.metamorphosis = num + 1L;
			break;
		}
		case AttackType.Starvation:
		{
			MapStats map_stats9 = World.world.map_stats;
			num = map_stats9.deaths_hunger;
			map_stats9.deaths_hunger = num + 1L;
			break;
		}
		case AttackType.Eaten:
		{
			MapStats map_stats10 = World.world.map_stats;
			num = map_stats10.deaths_eaten;
			map_stats10.deaths_eaten = num + 1L;
			break;
		}
		case AttackType.Age:
		{
			MapStats map_stats11 = World.world.map_stats;
			num = map_stats11.deaths_age;
			map_stats11.deaths_age = num + 1L;
			break;
		}
		case AttackType.Weapon:
		{
			MapStats map_stats12 = World.world.map_stats;
			num = map_stats12.deaths_weapon;
			map_stats12.deaths_weapon = num + 1L;
			break;
		}
		case AttackType.Poison:
		{
			MapStats map_stats13 = World.world.map_stats;
			num = map_stats13.deaths_poison;
			map_stats13.deaths_poison = num + 1L;
			break;
		}
		case AttackType.Gravity:
		{
			MapStats map_stats14 = World.world.map_stats;
			num = map_stats14.deaths_gravity;
			map_stats14.deaths_gravity = num + 1L;
			break;
		}
		case AttackType.Drowning:
		{
			MapStats map_stats15 = World.world.map_stats;
			num = map_stats15.deaths_drowning;
			map_stats15.deaths_drowning = num + 1L;
			break;
		}
		case AttackType.Water:
		{
			MapStats map_stats16 = World.world.map_stats;
			num = map_stats16.deaths_water;
			map_stats16.deaths_water = num + 1L;
			break;
		}
		case AttackType.Explosion:
		{
			MapStats map_stats17 = World.world.map_stats;
			num = map_stats17.deaths_explosion;
			map_stats17.deaths_explosion = num + 1L;
			break;
		}
		case AttackType.Smile:
		{
			MapStats map_stats18 = World.world.map_stats;
			num = map_stats18.deaths_smile;
			map_stats18.deaths_smile = num + 1L;
			break;
		}
		default:
			throw new ArgumentOutOfRangeException(string.Format("Unknown attack type: {0}", pType));
		}
		if (this.hasArmy())
		{
			this.army.increaseDeaths(pType);
		}
		if (base.hasCity())
		{
			this.city.increaseDeaths(pType);
		}
		if (this.hasClan())
		{
			this.clan.increaseDeaths(pType);
		}
		if (this.hasCulture())
		{
			this.culture.increaseDeaths(pType);
		}
		if (this.hasFamily())
		{
			this.family.increaseDeaths(pType);
		}
		if (this.hasLanguage())
		{
			this.language.increaseDeaths(pType);
		}
		if (this.hasReligion())
		{
			this.religion.increaseDeaths(pType);
		}
		if (this.hasSubspecies())
		{
			this.subspecies.increaseDeaths(pType);
		}
		if (base.isKingdomCiv())
		{
			this.kingdom.increaseDeaths(pType);
		}
		foreach (Actor actor in this.getParents())
		{
			actor.decreaseChildren();
		}
	}

	// Token: 0x06001190 RID: 4496 RVA: 0x000D09A8 File Offset: 0x000CEBA8
	public void increaseEvolutions()
	{
		MapStats map_stats = World.world.map_stats;
		long evolutions = map_stats.evolutions;
		map_stats.evolutions = evolutions + 1L;
		if (base.hasCity())
		{
			this.city.increaseEvolutions();
		}
		if (this.hasClan())
		{
			this.clan.increaseEvolutions();
		}
		if (this.hasReligion())
		{
			this.religion.increaseEvolutions();
		}
		if (this.hasSubspecies())
		{
			this.subspecies.increaseEvolutions();
		}
		if (base.isKingdomCiv())
		{
			this.kingdom.increaseEvolutions();
		}
	}

	// Token: 0x06001191 RID: 4497 RVA: 0x000D0A30 File Offset: 0x000CEC30
	private void increaseKills()
	{
		ActorData actorData = this.data;
		int kills = actorData.kills;
		actorData.kills = kills + 1;
		if (this.hasArmy())
		{
			this.army.increaseKills();
		}
		if (base.hasCity())
		{
			this.city.increaseKills();
		}
		if (this.hasClan())
		{
			this.clan.increaseKills();
		}
		if (this.hasCulture())
		{
			this.culture.increaseKills();
		}
		if (this.hasFamily())
		{
			this.family.increaseKills();
		}
		if (this.hasLanguage())
		{
			this.language.increaseKills();
		}
		if (this.hasReligion())
		{
			this.religion.increaseKills();
		}
		if (this.hasSubspecies())
		{
			this.subspecies.increaseKills();
		}
		if (base.isKingdomCiv())
		{
			this.kingdom.increaseKills();
		}
	}

	// Token: 0x06001192 RID: 4498 RVA: 0x000D0AFD File Offset: 0x000CECFD
	public void increaseChildren()
	{
		this._current_children++;
	}

	// Token: 0x06001193 RID: 4499 RVA: 0x000D0B0D File Offset: 0x000CED0D
	public void decreaseChildren()
	{
		this._current_children--;
	}

	// Token: 0x06001194 RID: 4500 RVA: 0x000D0B20 File Offset: 0x000CED20
	public void increaseBirths()
	{
		ActorData actorData = this.data;
		int births = actorData.births;
		actorData.births = births + 1;
	}

	// Token: 0x06001195 RID: 4501 RVA: 0x000D0B42 File Offset: 0x000CED42
	public void applyForcedKingdomTrait()
	{
		this.removeFromPreviousFaction();
		this.removeTrait("peaceful");
		this.startShake(0.3f, 0.2f, true, true);
		this.startColorEffect(ActorColorEffect.White);
		this.cancelAllBeh();
	}

	// Token: 0x06001196 RID: 4502 RVA: 0x000D0B74 File Offset: 0x000CED74
	public string getTraitsAsLocalizedString()
	{
		string tResult = "";
		foreach (ActorTrait tBaseTrait in this.traits)
		{
			tResult = tResult + tBaseTrait.getTranslatedName() + ", ";
		}
		return tResult;
	}

	// Token: 0x06001197 RID: 4503 RVA: 0x000D0BDC File Offset: 0x000CEDDC
	public void sortTraits(IReadOnlyCollection<ActorTrait> pTraits)
	{
		if (!this.traits.SetEquals(pTraits))
		{
			return;
		}
		this.traits.Clear();
		foreach (ActorTrait tTrait in pTraits)
		{
			this.traits.Add(tTrait);
		}
	}

	// Token: 0x06001198 RID: 4504 RVA: 0x000D0C44 File Offset: 0x000CEE44
	public void traitModifiedEvent()
	{
	}

	// Token: 0x06001199 RID: 4505 RVA: 0x000D0C48 File Offset: 0x000CEE48
	public void removeTrait(string pTraitID)
	{
		ActorTrait tTrait = AssetManager.traits.get(pTraitID);
		this.removeTrait(tTrait);
	}

	// Token: 0x0600119A RID: 4506 RVA: 0x000D0C69 File Offset: 0x000CEE69
	public bool removeTrait(ActorTrait pTrait)
	{
		bool flag = this.traits.Remove(pTrait);
		if (flag)
		{
			WorldActionTrait action_on_augmentation_remove = pTrait.action_on_augmentation_remove;
			if (action_on_augmentation_remove != null)
			{
				action_on_augmentation_remove(this, pTrait);
			}
			this.setStatsDirty();
			this.clearTraitCache();
		}
		return flag;
	}

	// Token: 0x0600119B RID: 4507 RVA: 0x000D0C9C File Offset: 0x000CEE9C
	public void removeTraits(ICollection<ActorTrait> pTraits)
	{
		bool tAnyRemoved = false;
		foreach (ActorTrait tTrait in pTraits)
		{
			if (this.traits.Remove(tTrait))
			{
				WorldActionTrait action_on_augmentation_remove = tTrait.action_on_augmentation_remove;
				if (action_on_augmentation_remove != null)
				{
					action_on_augmentation_remove(this, tTrait);
				}
				tAnyRemoved = true;
			}
		}
		if (tAnyRemoved)
		{
			this.setStatsDirty();
			this.clearTraitCache();
		}
	}

	// Token: 0x0600119C RID: 4508 RVA: 0x000D0D14 File Offset: 0x000CEF14
	public void clearTraitCache()
	{
		this._traits_cache.Clear();
	}

	// Token: 0x0600119D RID: 4509 RVA: 0x000D0D21 File Offset: 0x000CEF21
	private void removeOppositeTraits(ActorTrait pTrait)
	{
		if (!pTrait.hasOppositeTraits<ActorTrait>())
		{
			return;
		}
		this.removeTraits(pTrait.opposite_traits);
	}

	// Token: 0x0600119E RID: 4510 RVA: 0x000D0D38 File Offset: 0x000CEF38
	public bool addTrait(string pTraitID, bool pRemoveOpposites = false)
	{
		ActorTrait tTrait = AssetManager.traits.get(pTraitID);
		return tTrait != null && this.addTrait(tTrait, pRemoveOpposites);
	}

	// Token: 0x0600119F RID: 4511 RVA: 0x000D0D60 File Offset: 0x000CEF60
	public bool addTrait(ActorTrait pTrait, bool pRemoveOpposites = false)
	{
		if (this.hasTrait(pTrait))
		{
			return false;
		}
		if (pTrait.affects_mind && this.hasTag("strong_mind"))
		{
			return false;
		}
		if (pTrait.traits_to_remove != null)
		{
			this.removeTraits(pTrait.traits_to_remove);
		}
		if (pRemoveOpposites)
		{
			this.removeOppositeTraits(pTrait);
		}
		else if (this.hasOppositeTrait(pTrait))
		{
			return false;
		}
		this.traits.Add(pTrait);
		WorldActionTrait action_on_augmentation_add = pTrait.action_on_augmentation_add;
		if (action_on_augmentation_add != null)
		{
			action_on_augmentation_add(this, pTrait);
		}
		this.setStatsDirty();
		this.clearTraitCache();
		return true;
	}

	// Token: 0x060011A0 RID: 4512 RVA: 0x000D0DE8 File Offset: 0x000CEFE8
	internal bool hasOppositeTrait(string pTraitID)
	{
		return TraitTools.hasOppositeTrait(pTraitID, this.traits);
	}

	// Token: 0x060011A1 RID: 4513 RVA: 0x000D0DF6 File Offset: 0x000CEFF6
	internal bool hasOppositeTrait(ActorTrait pTrait)
	{
		return pTrait.hasOppositeTrait(this.traits);
	}

	// Token: 0x060011A2 RID: 4514 RVA: 0x000D0E04 File Offset: 0x000CF004
	public void generateRandomSpawnTraits(ActorAsset pAsset)
	{
		if (pAsset.traits == null)
		{
			return;
		}
		for (int i = 0; i < pAsset.traits.Count; i++)
		{
			string tTrait = pAsset.traits[i];
			this.addTrait(tTrait, false);
		}
	}

	// Token: 0x060011A3 RID: 4515 RVA: 0x000D0E48 File Offset: 0x000CF048
	public void checkTraitMutationOnBirth()
	{
		if (!this.hasSubspecies())
		{
			return;
		}
		int tCurrentRandomMutationsMax = this.subspecies.getAmountOfRandomMutationsActorTraits();
		if (tCurrentRandomMutationsMax == 0)
		{
			return;
		}
		for (int i = 0; i < tCurrentRandomMutationsMax; i++)
		{
			ActorTrait tTrait = AssetManager.traits.pot_traits_birth.GetRandom<ActorTrait>();
			if (this.asset.traits_ignore == null || !this.asset.traits_ignore.Contains(tTrait.id))
			{
				this.addTrait(tTrait, false);
			}
		}
	}

	// Token: 0x060011A4 RID: 4516 RVA: 0x000D0EB8 File Offset: 0x000CF0B8
	public void checkTraitMutationGrowUp()
	{
		if (!this.hasSubspecies())
		{
			return;
		}
		int tRandomTraits = Randy.randomInt(0, 3);
		for (int i = 0; i < tRandomTraits; i++)
		{
			ActorTrait tTrait = AssetManager.traits.pot_traits_growup.GetRandom<ActorTrait>();
			if ((this.asset.traits_ignore == null || !this.asset.traits_ignore.Contains(tTrait.id)) && (!tTrait.acquire_grow_up_sapient_only || this.isSapient()))
			{
				this.addTrait(tTrait, false);
			}
		}
	}

	// Token: 0x060011A5 RID: 4517 RVA: 0x000D0F30 File Offset: 0x000CF130
	public int countTraits()
	{
		return this.traits.Count;
	}

	// Token: 0x060011A6 RID: 4518 RVA: 0x000D0F40 File Offset: 0x000CF140
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasTrait(string pTraitID)
	{
		bool tResult;
		if (!this._traits_cache.TryGetValue(pTraitID, out tResult))
		{
			ActorTrait tTrait = AssetManager.traits.get(pTraitID);
			tResult = this.hasTrait(tTrait);
			this._traits_cache[pTraitID] = tResult;
		}
		return tResult;
	}

	// Token: 0x060011A7 RID: 4519 RVA: 0x000D0F7F File Offset: 0x000CF17F
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasTrait(ActorTrait pTrait)
	{
		return this.traits.Contains(pTrait);
	}

	// Token: 0x060011A8 RID: 4520 RVA: 0x000D0F90 File Offset: 0x000CF190
	public void updateParallelChecks(float pElapsed)
	{
		this._update_done = false;
		this._beh_skip = false;
		if (this.timer_jump_animation > 0f)
		{
			this.timer_jump_animation -= pElapsed;
		}
		this.checkFindCurrentTile();
		this.checkIsInLiquid();
		if (this.asset.update_z && this.position_height != 0f)
		{
			this.updateFall();
		}
		if (this.attackedBy != null && !this.attackedBy.isAlive())
		{
			this.attackedBy = null;
		}
		if (this.is_inside_boat)
		{
			return;
		}
		this.updateFlipRotation(pElapsed);
		if (this.under_forces)
		{
			int i = 0;
			while ((float)i < Config.time_scale_asset.multiplier)
			{
				this.updateVelocity();
				i++;
			}
		}
		if (World.world.isPaused())
		{
			return;
		}
		if (!base.isAlive())
		{
			return;
		}
		this.updateRotations(pElapsed);
		if (this.attack_timer >= 0f)
		{
			this.attack_timer -= pElapsed;
		}
		this.updateWalkJump(World.world.delta_time);
		if (this._timeout_targets >= 0f)
		{
			this._timeout_targets -= World.world.delta_time;
		}
		if (this.timer_action >= 0f)
		{
			this.timer_action -= pElapsed;
		}
		if (this.isAllowedToLookForEnemies())
		{
			this.targets_to_ignore_timer.update(pElapsed);
		}
		this.updateChangeScale(pElapsed);
		if (this.is_immovable)
		{
			return;
		}
		this.precalcMovementSpeed(false);
	}

	// Token: 0x060011A9 RID: 4521 RVA: 0x000D10F4 File Offset: 0x000CF2F4
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void skipUpdates()
	{
		this._update_done = true;
	}

	// Token: 0x060011AA RID: 4522 RVA: 0x000D10FD File Offset: 0x000CF2FD
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void skipBehaviour()
	{
		this._beh_skip = true;
	}

	// Token: 0x060011AB RID: 4523 RVA: 0x000D1106 File Offset: 0x000CF306
	public void u1_checkInside(float pElapsed)
	{
		if (!this.isInsideSomething())
		{
			return;
		}
		if (this.is_inside_boat)
		{
			this.setCurrentTilePosition(this.inside_boat.actor.current_tile);
			this.skipUpdates();
		}
	}

	// Token: 0x060011AC RID: 4524 RVA: 0x000D1135 File Offset: 0x000CF335
	public void u2_updateChildren(float pElapsed)
	{
		if (this._update_done)
		{
			return;
		}
		this.updateChildrenList(this.children_special, pElapsed);
		this.updateChildrenListSimple(this.children_pre_behaviour, pElapsed);
	}

	// Token: 0x060011AD RID: 4525 RVA: 0x000D115A File Offset: 0x000CF35A
	public void u3_spriteAnimation(float pElapsed)
	{
		if (this._update_done)
		{
			return;
		}
		if (!this.is_visible)
		{
			return;
		}
		this.sprite_animation.update(pElapsed);
	}

	// Token: 0x060011AE RID: 4526 RVA: 0x000D117A File Offset: 0x000CF37A
	public void u4_deadCheck(float pElapsed)
	{
		if (this._update_done)
		{
			return;
		}
		if (!base.isAlive())
		{
			this.updateDeadAnimation(pElapsed);
			this.skipUpdates();
			return;
		}
		if (this.isInMagnet() || this.under_forces)
		{
			this.skipUpdates();
		}
	}

	// Token: 0x060011AF RID: 4527 RVA: 0x000D11B4 File Offset: 0x000CF3B4
	public void u5_curTileAction()
	{
		if (this._update_done)
		{
			return;
		}
		if (this.position_height > 0f)
		{
			return;
		}
		WorldTile tTile = this.current_tile;
		TileTypeBase tType = tTile.Type;
		if (this.isFlying())
		{
			return;
		}
		if (tType.block && !this.ignoresBlocks())
		{
			if (this.asset.move_from_block && !this.is_moving && (!this.hasTask() || !this.ai.task.move_from_block))
			{
				this.setTask("move_from_block", true, true, false);
			}
			if (this.asset.die_on_blocks && !this.isUnderDamageCooldown() && !this._shake_active && base.getHealth() > 1)
			{
				this.getHit(1f, true, AttackType.Gravity, null, true, false, true);
			}
			if (!this.isInAir() || this.isHovering())
			{
				this.applyRandomForce(1.5f, 3f);
				if (Randy.randomChance(0.02f))
				{
					this.makeStunned(5f);
				}
			}
			if (tType.mountains || tType.wall)
			{
				this.checkDieOnGroundBoat();
			}
			return;
		}
		if (tType.ground)
		{
			if (tTile.isOnFire() && !this.isImmuneToFire())
			{
				ActionLibrary.addBurningEffectOnTarget(null, this, null);
				if (!base.isAlive())
				{
					if (!this._update_done)
					{
						Debug.LogError("If you ever see me, remove this line");
					}
					this.skipUpdates();
					return;
				}
			}
			if (this.isWaterCreature() && !this.asset.force_land_creature)
			{
				this.spendStaminaWithCooldown(Randy.randomInt(1, 6));
				if (!this.isUnderDamageCooldown() && !this._shake_active)
				{
					this.getHit(1f, true, AttackType.Other, null, true, false, true);
				}
			}
			this.checkDieOnGroundBoat();
		}
		else if (tType.liquid)
		{
			if (tType.damaged_when_walked)
			{
				tTile.tryToBreak();
			}
			if (!tType.lava)
			{
				base.finishStatusEffect("burning");
			}
			if (this.isDamagedByOcean() && tTile.Type.ocean && !this.isUnderDamageCooldown() && !this._shake_active)
			{
				this.getHit((float)this.getWaterDamage(), true, AttackType.Water, null, true, false, true);
			}
			if (!this.hasTag("fast_swimming") && !this.isWaterCreature() && !this.isInAir())
			{
				this.spendStaminaWithCooldown(Randy.randomInt(1, 6));
				if (this.getStamina() <= 0 && !this.isUnderDamageCooldown())
				{
					base.addStatusEffect("drowning", 0f, false);
				}
			}
		}
		if (tType.damage_units && !this.isUnderDamageCooldown() && (!tType.lava || (this.asset.die_in_lava && !this.isImmuneToFire())))
		{
			this.getHit((float)tType.damage, true, AttackType.Fire, null, true, false, true);
			if (!base.hasHealth())
			{
				if (tType.lava)
				{
					CursedSacrifice.checkGoodForSacrifice(this);
				}
				this.skipUpdates();
			}
		}
		if (tTile.hasBuilding() && tTile.building.asset.has_step_action)
		{
			tTile.building.asset.step_action(this, tTile.building);
			if (!base.hasHealth())
			{
				this.skipUpdates();
			}
		}
	}

	// Token: 0x060011B0 RID: 4528 RVA: 0x000D14AD File Offset: 0x000CF6AD
	public void u6_checkFrozen(float pElapsed)
	{
		if (this._update_done)
		{
			return;
		}
		if (this.is_ai_frozen || this.is_unconscious)
		{
			this.skipUpdates();
		}
	}

	// Token: 0x060011B1 RID: 4529 RVA: 0x000D14CE File Offset: 0x000CF6CE
	public void u8_checkUpdateTimers(float pElapsed)
	{
		if (this._update_done)
		{
			return;
		}
		if (this.timer_action >= 0f)
		{
			this.skipUpdates();
			return;
		}
		if (!base.isAlive())
		{
			if (!this._update_done)
			{
				Debug.LogError("If you ever see me, remove this line");
			}
			this.skipUpdates();
		}
	}

	// Token: 0x060011B2 RID: 4530 RVA: 0x000D1510 File Offset: 0x000CF710
	public void u7_checkAugmentationEffects()
	{
		if (this._update_done)
		{
			return;
		}
		if (World.world.getWorldTimeElapsedSince(this._timestamp_augmentation_effects) < 1f)
		{
			return;
		}
		List<BaseAugmentationAsset> tTempAugmentationList = Actor._tempAugmentationList;
		Dictionary<BaseAugmentationAsset, double> tDictTimers = this._s_special_effect_augmentations_timers;
		double tWorldTime = World.world.getCurWorldTime();
		this._timestamp_augmentation_effects = tWorldTime;
		int i = 0;
		int tLen = this._s_special_effect_augmentations.Count;
		while (i < tLen)
		{
			BaseAugmentationAsset tAugmentation = this._s_special_effect_augmentations[i];
			double tLastUseTimestamp;
			if (!tDictTimers.TryGetValue(tAugmentation, out tLastUseTimestamp))
			{
				goto IL_88;
			}
			if (World.world.getWorldTimeElapsedSince(tLastUseTimestamp) >= tAugmentation.special_effect_interval)
			{
				tTempAugmentationList.Add(tAugmentation);
				goto IL_88;
			}
			IL_91:
			i++;
			continue;
			IL_88:
			tDictTimers[tAugmentation] = tWorldTime;
			goto IL_91;
		}
		if (tTempAugmentationList.Count == 0)
		{
			return;
		}
		int j = 0;
		int tLen2 = tTempAugmentationList.Count;
		while (j < tLen2)
		{
			BaseAugmentationAsset tAugmentation2 = tTempAugmentationList[j];
			WorldAction tSpecialEffect = tAugmentation2.action_special_effect;
			if (Bench.bench_enabled)
			{
				double tTimeStart = Time.realtimeSinceStartupAsDouble;
				tSpecialEffect(this, this.current_tile);
				double tTimeEnd = Time.realtimeSinceStartupAsDouble - tTimeStart;
				Bench.benchSaveSplit(tAugmentation2.id, tTimeEnd, 1, "effects_traits");
			}
			else
			{
				tSpecialEffect(this, this.current_tile);
			}
			j++;
		}
		Actor._tempAugmentationList.Clear();
	}

	// Token: 0x060011B3 RID: 4531 RVA: 0x000D1643 File Offset: 0x000CF843
	public void b1_checkUnderForce(float pElapsed)
	{
		if (this._update_done)
		{
			return;
		}
		if (this.under_forces)
		{
			this.skipBehaviour();
			return;
		}
		if (this.asset.update_z && this.position_height != 0f)
		{
			this.skipBehaviour();
		}
	}

	// Token: 0x060011B4 RID: 4532 RVA: 0x000D167D File Offset: 0x000CF87D
	public void b2_checkCurrentEnemyTarget(float pElapsed)
	{
		if (this._update_done)
		{
			return;
		}
		if (this._beh_skip)
		{
			return;
		}
		if (this.checkCurrentEnemyTarget())
		{
			this.skipBehaviour();
		}
	}

	// Token: 0x060011B5 RID: 4533 RVA: 0x000D169F File Offset: 0x000CF89F
	public void b3_findEnemyTarget(float pElapsed)
	{
		if (this._update_done)
		{
			return;
		}
		if (this._beh_skip)
		{
			return;
		}
		if (this.checkEnemyTargets())
		{
			this.stopMovement();
			this.skipBehaviour();
		}
	}

	// Token: 0x060011B6 RID: 4534 RVA: 0x000D16C8 File Offset: 0x000CF8C8
	public void b4_checkTaskVerifier(float pElapsed)
	{
		if (this._update_done)
		{
			return;
		}
		if (this._beh_skip)
		{
			return;
		}
		if (this.hasTask() && this.ai.task.has_verifier && this.ai.task.task_verifier.execute(this) == BehResult.Stop)
		{
			this.cancelAllBeh();
			this.skipBehaviour();
			return;
		}
		if (this.is_moving)
		{
			this.skipBehaviour();
		}
	}

	// Token: 0x060011B7 RID: 4535 RVA: 0x000D1735 File Offset: 0x000CF935
	public void b5_checkPathMovement(float pElapsed)
	{
		if (this._update_done)
		{
			return;
		}
		if (this._beh_skip)
		{
			return;
		}
		if (this.isUsingPath())
		{
			this.updatePathMovement();
			this.skipBehaviour();
		}
	}

	// Token: 0x060011B8 RID: 4536 RVA: 0x000D1760 File Offset: 0x000CF960
	public void b6_0_updateDecision(float pElapsed)
	{
		if (this._update_done)
		{
			return;
		}
		if (this._beh_skip)
		{
			return;
		}
		if (this.is_unconscious)
		{
			return;
		}
		if (this._has_status_possessed)
		{
			return;
		}
		if (!this.asset.has_ai_system)
		{
			return;
		}
		DecisionHelper.makeDecisionFor(this, out this._last_decision_id);
	}

	// Token: 0x060011B9 RID: 4537 RVA: 0x000D17AC File Offset: 0x000CF9AC
	public string getLastDecisionForMindOverview()
	{
		return this._last_decision_id;
	}

	// Token: 0x060011BA RID: 4538 RVA: 0x000D17B4 File Offset: 0x000CF9B4
	public void b6_updateAI(float pElapsed)
	{
		if (this._update_done)
		{
			return;
		}
		if (this._beh_skip)
		{
			return;
		}
		if (this.is_unconscious)
		{
			return;
		}
		if (this._has_status_possessed)
		{
			return;
		}
		if (!this.asset.has_ai_system)
		{
			return;
		}
		this.ai.update();
	}

	// Token: 0x060011BB RID: 4539 RVA: 0x000D17F4 File Offset: 0x000CF9F4
	public void b55_updateNaturalDeaths(float pElapsed)
	{
		if (this._update_done)
		{
			return;
		}
		if (this._beh_skip)
		{
			return;
		}
		if (this.is_unconscious)
		{
			return;
		}
		if (this._has_status_possessed)
		{
			return;
		}
		if (!this.asset.has_ai_system)
		{
			return;
		}
		if (this.ai.action_index == 0 && this.checkNaturalDeath())
		{
			this.skipBehaviour();
			this.skipUpdates();
		}
	}

	// Token: 0x060011BC RID: 4540 RVA: 0x000D1854 File Offset: 0x000CFA54
	public void u10_checkSmoothMovement(float pElapsed)
	{
		if (this._update_done)
		{
			return;
		}
		if (this.is_immovable)
		{
			return;
		}
		if (!Config.time_scale_asset.sonic)
		{
			this.checkCalibrateTargetPosition();
		}
		this.updateMovement(pElapsed, 0f);
	}

	// Token: 0x04000F1A RID: 3866
	internal ActorIdleLoopSound idle_loop_sound;

	// Token: 0x04000F1B RID: 3867
	internal bool is_forced_socialize_icon;

	// Token: 0x04000F1C RID: 3868
	internal double is_forced_socialize_timestamp;

	// Token: 0x04000F1D RID: 3869
	internal string ate_last_item_id;

	// Token: 0x04000F1E RID: 3870
	internal double timestamp_session_ate_food;

	// Token: 0x04000F1F RID: 3871
	internal double timestamp_tween_session_social;

	// Token: 0x04000F20 RID: 3872
	private double _last_color_effect_timestamp;

	// Token: 0x04000F21 RID: 3873
	private double _last_stamina_reduce_timestamp;

	// Token: 0x04000F22 RID: 3874
	internal double timestamp_profession_set;

	// Token: 0x04000F23 RID: 3875
	internal List<BaseActorComponent> children_special;

	// Token: 0x04000F24 RID: 3876
	private Dictionary<Type, BaseActorComponent> _dict_special;

	// Token: 0x04000F25 RID: 3877
	private List<ActorSimpleComponent> children_pre_behaviour;

	// Token: 0x04000F26 RID: 3878
	private Dictionary<Type, ActorSimpleComponent> dict_pre_behaviour;

	// Token: 0x04000F27 RID: 3879
	private UnitProfession _profession;

	// Token: 0x04000F28 RID: 3880
	public GameObject avatar;

	// Token: 0x04000F29 RID: 3881
	private double _timestamp_augmentation_effects;

	// Token: 0x04000F2A RID: 3882
	internal bool show_shadow;

	// Token: 0x04000F2B RID: 3883
	internal Vector2 current_shadow_position = Globals.POINT_IN_VOID;

	// Token: 0x04000F2C RID: 3884
	private double[] _decision_cooldowns;

	// Token: 0x04000F2D RID: 3885
	private bool[] _decision_disabled;

	// Token: 0x04000F2E RID: 3886
	public DecisionAsset[] decisions = new DecisionAsset[64];

	// Token: 0x04000F2F RID: 3887
	public int decisions_counter;

	// Token: 0x04000F30 RID: 3888
	private int _current_children;

	// Token: 0x04000F31 RID: 3889
	private readonly Queue<HappinessHistory> _last_happiness_history = new Queue<HappinessHistory>();

	// Token: 0x04000F32 RID: 3890
	private HashSet<long> _aggression_targets = new HashSet<long>();

	// Token: 0x04000F33 RID: 3891
	private HoverState _hover_state;

	// Token: 0x04000F34 RID: 3892
	private float _hover_timer;

	// Token: 0x04000F35 RID: 3893
	public BatchActors batch;

	// Token: 0x04000F36 RID: 3894
	internal WorldTile beh_tile_target;

	// Token: 0x04000F37 RID: 3895
	internal Building beh_building_target;

	// Token: 0x04000F38 RID: 3896
	internal BaseSimObject beh_actor_target;

	// Token: 0x04000F39 RID: 3897
	internal Book beh_book_target;

	// Token: 0x04000F3A RID: 3898
	internal Building inside_building;

	// Token: 0x04000F3B RID: 3899
	internal bool is_inside_building;

	// Token: 0x04000F3C RID: 3900
	internal Boat inside_boat;

	// Token: 0x04000F3D RID: 3901
	internal bool is_inside_boat;

	// Token: 0x04000F3E RID: 3902
	internal BaseSimObject attackedBy;

	// Token: 0x04000F3F RID: 3903
	public Actor lover;

	// Token: 0x04000F40 RID: 3904
	public readonly HashSet<ActorTrait> traits = new HashSet<ActorTrait>();

	// Token: 0x04000F41 RID: 3905
	private readonly CombatActionHolder _combat_actions = new CombatActionHolder();

	// Token: 0x04000F42 RID: 3906
	private readonly SpellHolder _spells = new SpellHolder();

	// Token: 0x04000F43 RID: 3907
	private readonly Dictionary<string, bool> _traits_cache = new Dictionary<string, bool>();

	// Token: 0x04000F44 RID: 3908
	internal ActorData data;

	// Token: 0x04000F45 RID: 3909
	internal ProfessionAsset profession_asset;

	// Token: 0x04000F46 RID: 3910
	private bool _state_adult;

	// Token: 0x04000F47 RID: 3911
	private bool _state_baby;

	// Token: 0x04000F48 RID: 3912
	private bool _state_egg;

	// Token: 0x04000F49 RID: 3913
	public ActorAsset asset;

	// Token: 0x04000F4A RID: 3914
	public Vector2 next_step_position;

	// Token: 0x04000F4B RID: 3915
	public Vector2 next_step_position_possession;

	// Token: 0x04000F4C RID: 3916
	internal Vector2 shake_offset;

	// Token: 0x04000F4D RID: 3917
	public static readonly Vector2 sprite_offset = new Vector2(0.5f, 0.5f);

	// Token: 0x04000F4E RID: 3918
	public Vector2 move_jump_offset;

	// Token: 0x04000F4F RID: 3919
	private bool _shake_horizontal;

	// Token: 0x04000F50 RID: 3920
	private bool _shake_vertical;

	// Token: 0x04000F51 RID: 3921
	private float _shake_timer;

	// Token: 0x04000F52 RID: 3922
	private bool _shake_active;

	// Token: 0x04000F53 RID: 3923
	private float _shake_volume;

	// Token: 0x04000F54 RID: 3924
	private bool _is_moving;

	// Token: 0x04000F55 RID: 3925
	private bool _possessed_movement;

	// Token: 0x04000F56 RID: 3926
	private bool _is_in_liquid;

	// Token: 0x04000F57 RID: 3927
	internal bool is_visible;

	// Token: 0x04000F58 RID: 3928
	internal bool last_sprite_renderer_enabled;

	// Token: 0x04000F59 RID: 3929
	internal AnimationFrameData frame_data;

	// Token: 0x04000F5A RID: 3930
	internal bool dirty_current_tile;

	// Token: 0x04000F5B RID: 3931
	internal WorldTile tile_target;

	// Token: 0x04000F5C RID: 3932
	private WorldTile _next_step_tile;

	// Token: 0x04000F5D RID: 3933
	public SplitPathStatus split_path;

	// Token: 0x04000F5E RID: 3934
	public int current_path_index;

	// Token: 0x04000F5F RID: 3935
	public readonly List<WorldTile> current_path = new List<WorldTile>();

	// Token: 0x04000F60 RID: 3936
	public List<MapRegion> current_path_global;

	// Token: 0x04000F61 RID: 3937
	public BaseActionActor callbacks_on_death;

	// Token: 0x04000F62 RID: 3938
	public BaseActionActor callbacks_landed;

	// Token: 0x04000F63 RID: 3939
	public BaseActionActor callbacks_cancel_path_movement;

	// Token: 0x04000F64 RID: 3940
	public BaseActionActor callbacks_magnet_update;

	// Token: 0x04000F65 RID: 3941
	internal float actor_scale;

	// Token: 0x04000F66 RID: 3942
	internal float target_scale;

	// Token: 0x04000F67 RID: 3943
	internal BaseSimObject attack_target;

	// Token: 0x04000F68 RID: 3944
	internal bool has_attack_target;

	// Token: 0x04000F69 RID: 3945
	internal float timer_action;

	// Token: 0x04000F6A RID: 3946
	internal float timer_jump_animation;

	// Token: 0x04000F6B RID: 3947
	internal float hitbox_bonus_height;

	// Token: 0x04000F6C RID: 3948
	internal Vector3 velocity;

	// Token: 0x04000F6D RID: 3949
	internal float velocity_speed;

	// Token: 0x04000F6E RID: 3950
	internal bool under_forces;

	// Token: 0x04000F6F RID: 3951
	protected WorldTimer targets_to_ignore_timer;

	// Token: 0x04000F70 RID: 3952
	private bool _flying;

	// Token: 0x04000F71 RID: 3953
	internal bool is_in_magnet;

	// Token: 0x04000F72 RID: 3954
	internal float attack_timer;

	// Token: 0x04000F73 RID: 3955
	internal double last_attack_timestamp;

	// Token: 0x04000F74 RID: 3956
	internal EquipmentAsset _attack_asset;

	// Token: 0x04000F75 RID: 3957
	internal PersonalityAsset s_personality;

	// Token: 0x04000F76 RID: 3958
	private readonly List<BaseAugmentationAsset> _s_special_effect_augmentations = new List<BaseAugmentationAsset>();

	// Token: 0x04000F77 RID: 3959
	private readonly Dictionary<BaseAugmentationAsset, double> _s_special_effect_augmentations_timers = new Dictionary<BaseAugmentationAsset, double>();

	// Token: 0x04000F78 RID: 3960
	internal AttackAction s_action_attack_target;

	// Token: 0x04000F79 RID: 3961
	internal GetHitAction s_get_hit_action;

	// Token: 0x04000F7A RID: 3962
	protected static readonly List<BaseAugmentationAsset> _tempAugmentationList = new List<BaseAugmentationAsset>();

	// Token: 0x04000F7B RID: 3963
	private bool _has_emotions;

	// Token: 0x04000F7C RID: 3964
	private bool _has_tag_unconscious;

	// Token: 0x04000F7D RID: 3965
	public bool has_tag_immunity_cold;

	// Token: 0x04000F7E RID: 3966
	private bool _has_status_strange_urge;

	// Token: 0x04000F7F RID: 3967
	private bool _has_status_possessed;

	// Token: 0x04000F80 RID: 3968
	private bool _has_status_sleeping;

	// Token: 0x04000F81 RID: 3969
	private bool _has_status_tantrum;

	// Token: 0x04000F82 RID: 3970
	private bool _has_status_drowning;

	// Token: 0x04000F83 RID: 3971
	private bool _has_status_invincible;

	// Token: 0x04000F84 RID: 3972
	private bool _cache_check_has_status_removed_on_damage;

	// Token: 0x04000F85 RID: 3973
	private bool _has_trait_weightless;

	// Token: 0x04000F86 RID: 3974
	private bool _has_trait_peaceful;

	// Token: 0x04000F87 RID: 3975
	private bool _has_trait_clone;

	// Token: 0x04000F88 RID: 3976
	internal bool has_tag_generate_light;

	// Token: 0x04000F89 RID: 3977
	private bool _has_any_sick_trait;

	// Token: 0x04000F8A RID: 3978
	internal bool is_immovable;

	// Token: 0x04000F8B RID: 3979
	internal bool is_ai_frozen;

	// Token: 0x04000F8C RID: 3980
	private bool _has_stop_idle_animation;

	// Token: 0x04000F8D RID: 3981
	private bool _ignore_fights;

	// Token: 0x04000F8E RID: 3982
	protected bool should_check_land_cancel;

	// Token: 0x04000F8F RID: 3983
	internal WorldTile scheduled_tile_target;

	// Token: 0x04000F90 RID: 3984
	internal bool _action_wait_after_land;

	// Token: 0x04000F91 RID: 3985
	internal float _action_wait_after_land_timer;

	// Token: 0x04000F92 RID: 3986
	internal AiSystemActor ai;

	// Token: 0x04000F93 RID: 3987
	public CitizenJobAsset citizen_job;

	// Token: 0x04000F94 RID: 3988
	protected Building _home_building;

	// Token: 0x04000F95 RID: 3989
	private float _death_timer_color_stage_1;

	// Token: 0x04000F96 RID: 3990
	private float _death_timer_alpha_stage_2;

	// Token: 0x04000F97 RID: 3991
	private float _jump_time;

	// Token: 0x04000F98 RID: 3992
	private float lastX;

	// Token: 0x04000F99 RID: 3993
	private float lastY;

	// Token: 0x04000F9A RID: 3994
	public float flip_angle;

	// Token: 0x04000F9B RID: 3995
	internal bool flip;

	// Token: 0x04000F9C RID: 3996
	private int _precalc_movement_speed_skips;

	// Token: 0x04000F9D RID: 3997
	private float _current_combined_movement_speed;

	// Token: 0x04000F9E RID: 3998
	internal float _timeout_targets;

	// Token: 0x04000F9F RID: 3999
	internal Vector3 target_angle;

	// Token: 0x04000FA0 RID: 4000
	internal float rotation_cooldown;

	// Token: 0x04000FA1 RID: 4001
	private RotationDirection _rotation_direction;

	// Token: 0x04000FA2 RID: 4002
	private Sprite _last_topic_sprite;

	// Token: 0x04000FA3 RID: 4003
	public Color color;

	// Token: 0x04000FA4 RID: 4004
	internal bool dirty_sprite_main;

	// Token: 0x04000FA5 RID: 4005
	private Sprite _cached_sprite_item;

	// Token: 0x04000FA6 RID: 4006
	private IHandRenderer _cached_hand_renderer_asset;

	// Token: 0x04000FA7 RID: 4007
	internal Sprite cached_sprite_head;

	// Token: 0x04000FA8 RID: 4008
	internal bool dirty_sprite_head;

	// Token: 0x04000FA9 RID: 4009
	internal AnimationContainerUnit animation_container;

	// Token: 0x04000FAA RID: 4010
	private Sprite _last_main_sprite;

	// Token: 0x04000FAB RID: 4011
	private Sprite _last_colored_sprite;

	// Token: 0x04000FAC RID: 4012
	private ColorAsset _last_color_asset;

	// Token: 0x04000FAD RID: 4013
	private bool _dirty_sprite_item;

	// Token: 0x04000FAE RID: 4014
	private bool _has_animated_item;

	// Token: 0x04000FAF RID: 4015
	public SpriteAnimation sprite_animation;

	// Token: 0x04000FB0 RID: 4016
	private const float POSSESSION_ATTACK_SECONDS = 0.5f;

	// Token: 0x04000FB1 RID: 4017
	private double _possession_attack_happened_frame;

	// Token: 0x04000FB2 RID: 4018
	private AttackType _last_attack_type;

	// Token: 0x04000FB3 RID: 4019
	public ActorEquipment equipment;

	// Token: 0x04000FB4 RID: 4020
	public Army army;

	// Token: 0x04000FB5 RID: 4021
	public City city;

	// Token: 0x04000FB6 RID: 4022
	public Clan clan;

	// Token: 0x04000FB7 RID: 4023
	public Culture culture;

	// Token: 0x04000FB8 RID: 4024
	public Family family;

	// Token: 0x04000FB9 RID: 4025
	public Language language;

	// Token: 0x04000FBA RID: 4026
	public Plot plot;

	// Token: 0x04000FBB RID: 4027
	public Religion religion;

	// Token: 0x04000FBC RID: 4028
	public Subspecies subspecies;

	// Token: 0x04000FBD RID: 4029
	private const float FIND_TILE_SQ_DIST = 4f;

	// Token: 0x04000FBE RID: 4030
	private const float CUR_SQ_DIST = 0.16000001f;

	// Token: 0x04000FBF RID: 4031
	private const float NEW_SQ_DIST = 0.09f;

	// Token: 0x04000FC0 RID: 4032
	private bool _beh_skip;

	// Token: 0x04000FC1 RID: 4033
	private bool _update_done;

	// Token: 0x04000FC2 RID: 4034
	private string _last_decision_id;
}
