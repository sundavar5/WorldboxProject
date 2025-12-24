using System;
using System.Collections.Generic;

// Token: 0x020001ED RID: 493
public class WorldAgeManager
{
	// Token: 0x17000073 RID: 115
	// (get) Token: 0x06000E30 RID: 3632 RVA: 0x000C10E8 File Offset: 0x000BF2E8
	private static MapStats _map_stats
	{
		get
		{
			return World.world.map_stats;
		}
	}

	// Token: 0x06000E32 RID: 3634 RVA: 0x000C1112 File Offset: 0x000BF312
	public WorldAgeAsset getCurrentAge()
	{
		return this._current_age;
	}

	// Token: 0x06000E33 RID: 3635 RVA: 0x000C111A File Offset: 0x000BF31A
	public WorldAgeAsset getNextAge()
	{
		return this.getAgeFromSlot(this.getNextSlotIndex());
	}

	// Token: 0x06000E34 RID: 3636 RVA: 0x000C1128 File Offset: 0x000BF328
	public int getCurrentSlotIndex()
	{
		return WorldAgeManager._map_stats.world_age_slot_index;
	}

	// Token: 0x06000E35 RID: 3637 RVA: 0x000C1134 File Offset: 0x000BF334
	public int getNextSlotIndex()
	{
		return Toolbox.loopIndex(WorldAgeManager._map_stats.world_age_slot_index + 1, WorldAgeManager._map_stats.world_ages_slots.Length);
	}

	// Token: 0x06000E36 RID: 3638 RVA: 0x000C1154 File Offset: 0x000BF354
	public WorldAgeAsset getAgeFromSlot(int pIndex)
	{
		string tAssetId = WorldAgeManager._map_stats.world_ages_slots[pIndex];
		if (string.IsNullOrEmpty(tAssetId))
		{
			tAssetId = "age_unknown";
		}
		return AssetManager.era_library.get(tAssetId);
	}

	// Token: 0x06000E37 RID: 3639 RVA: 0x000C1188 File Offset: 0x000BF388
	public void setAgeToSlot(WorldAgeAsset pAsset, int pSlotIndex)
	{
		if (WorldAgeManager._map_stats.world_ages_slots[pSlotIndex] == pAsset.id)
		{
			return;
		}
		WorldAgeManager._map_stats.world_ages_slots[pSlotIndex] = pAsset.id;
		if (pSlotIndex != WorldAgeManager._map_stats.world_age_slot_index)
		{
			return;
		}
		this.setCurrentAge(pAsset, false);
	}

	// Token: 0x06000E38 RID: 3640 RVA: 0x000C11D7 File Offset: 0x000BF3D7
	public float getNightMod()
	{
		if (this._effects == null)
		{
			return 0f;
		}
		if (this._effects.override_night)
		{
			return this._effects.night_value_mat;
		}
		return this._night_multiplier;
	}

	// Token: 0x06000E39 RID: 3641 RVA: 0x000C120C File Offset: 0x000BF40C
	public bool shouldShowLights()
	{
		return this.getNightMod() != 0f;
	}

	// Token: 0x06000E3A RID: 3642 RVA: 0x000C1220 File Offset: 0x000BF420
	internal void loadAge()
	{
		bool tOverrideTime = false;
		if (string.IsNullOrEmpty(WorldAgeManager._map_stats.world_age_id) || WorldAgeManager._map_stats.world_age_id == "age_unknown")
		{
			WorldAgeManager._map_stats.world_age_id = "age_hope";
			tOverrideTime = true;
		}
		WorldAgeAsset tAsset = AssetManager.era_library.get(WorldAgeManager._map_stats.world_age_id);
		if (tAsset == null)
		{
			tAsset = WorldAgeLibrary.hope;
			tOverrideTime = true;
		}
		this.setCurrentAge(tAsset, tOverrideTime);
	}

	// Token: 0x06000E3B RID: 3643 RVA: 0x000C1290 File Offset: 0x000BF490
	public void update(float pElapsed)
	{
		if (!World.world.isPaused() && !this.isPaused())
		{
			WorldAgeManager._map_stats.current_age_progress += pElapsed / (WorldAgeManager._map_stats.current_world_ages_duration / WorldAgeManager._map_stats.world_ages_speed_multiplier);
			if (WorldAgeManager._map_stats.current_age_progress >= 1f)
			{
				this.startNextAge(0f);
			}
		}
		this.updateEffects(pElapsed);
	}

	// Token: 0x06000E3C RID: 3644 RVA: 0x000C12FC File Offset: 0x000BF4FC
	public float getTimeTillNextAge()
	{
		return WorldAgeManager._map_stats.current_world_ages_duration * (1f - WorldAgeManager._map_stats.current_age_progress);
	}

	// Token: 0x06000E3D RID: 3645 RVA: 0x000C1319 File Offset: 0x000BF519
	public bool isPaused()
	{
		return WorldAgeManager._map_stats.is_world_ages_paused;
	}

	// Token: 0x06000E3E RID: 3646 RVA: 0x000C1325 File Offset: 0x000BF525
	public void togglePlay(bool pState)
	{
		WorldAgeManager._map_stats.is_world_ages_paused = !pState;
	}

	// Token: 0x06000E3F RID: 3647 RVA: 0x000C1335 File Offset: 0x000BF535
	public void setAgesSpeedMultiplier(float pMultiplier)
	{
		WorldAgeManager._map_stats.world_ages_speed_multiplier = pMultiplier;
	}

	// Token: 0x06000E40 RID: 3648 RVA: 0x000C1342 File Offset: 0x000BF542
	public void debugEndAge()
	{
		WorldAgeManager._map_stats.current_world_ages_duration = 5f;
	}

	// Token: 0x06000E41 RID: 3649 RVA: 0x000C1354 File Offset: 0x000BF554
	public void startNextAge(float pStartProgress = 0f)
	{
		int tNewSlotIndex = this.getNextSlotIndex();
		this.setCurrentSlotIndex(tNewSlotIndex, pStartProgress);
	}

	// Token: 0x06000E42 RID: 3650 RVA: 0x000C1370 File Offset: 0x000BF570
	public void setCurrentSlotIndex(int pIndex, float pStartProgress = 1f)
	{
		WorldAgeManager._map_stats.world_age_slot_index = pIndex;
		WorldAgeManager._map_stats.current_age_progress = pStartProgress;
		WorldAgeAsset tNewAgeAsset = this.getAgeFromSlot(pIndex);
		tNewAgeAsset = this.checkAge(tNewAgeAsset);
		this.setCurrentAge(tNewAgeAsset, true);
	}

	// Token: 0x06000E43 RID: 3651 RVA: 0x000C13AC File Offset: 0x000BF5AC
	private void updateEffects(float pElapsed)
	{
		if (this._effects == null)
		{
			this._effects = WorldAgeEffects.instance;
			return;
		}
		this._effects.update(World.world.delta_time);
		if (World.world.isPaused())
		{
			return;
		}
		if (this._current_age == null)
		{
			return;
		}
		if (this._current_age.overlay_darkness)
		{
			float tNightMax = this.NIGHT_MAX * ((float)PlayerConfig.getIntValue("age_night_effect") / 100f);
			this._night_multiplier += pElapsed * this.NIGHT_SPEED;
			if (this._night_multiplier > tNightMax)
			{
				this._night_multiplier = tNightMax;
			}
		}
		else
		{
			this._night_multiplier -= pElapsed * this.NIGHT_SPEED;
			if (this._night_multiplier < 0f)
			{
				this._night_multiplier = 0f;
			}
		}
		if (this._current_age.special_effect_action != null)
		{
			this._timer_special_action -= pElapsed;
			if (this._timer_special_action <= 0f)
			{
				this._current_age.special_effect_action();
				this._timer_special_action = this._current_age.special_effect_interval;
			}
		}
	}

	// Token: 0x06000E44 RID: 3652 RVA: 0x000C14C4 File Offset: 0x000BF6C4
	private void calcNextEraTime(WorldAgeAsset pAsset, bool pForceMax = false)
	{
		int tTime = (int)((float)Randy.randomInt(pAsset.years_min, pAsset.years_max) * 12f * 5f);
		if (pForceMax)
		{
			tTime = (int)((float)pAsset.years_max * 12f * 5f);
		}
		WorldAgeManager._map_stats.current_world_ages_duration = (float)tTime;
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x000C1518 File Offset: 0x000BF718
	private void setCurrentAge(WorldAgeAsset pAsset, bool pOverrideTime = true)
	{
		pAsset = this.checkAge(pAsset);
		WorldAgeManager._map_stats.world_age_id = pAsset.id;
		WorldAgeManager._map_stats.world_age_started_at = World.world.getCurWorldTime();
		if (this._current_age != pAsset)
		{
			WorldAgeManager._map_stats.same_world_age_started_at = World.world.getCurWorldTime();
		}
		if (pOverrideTime)
		{
			this.calcNextEraTime(pAsset, false);
		}
		this._current_age = pAsset;
		WorldBehaviourClouds.setEra(pAsset);
		List<Actor> tActorList = World.world.units.getSimpleList();
		for (int i = 0; i < tActorList.Count; i++)
		{
			tActorList[i].setStatsDirty();
		}
		this._timer_special_action = pAsset.special_effect_interval;
	}

	// Token: 0x06000E46 RID: 3654 RVA: 0x000C15C0 File Offset: 0x000BF7C0
	public void clear()
	{
		this._current_age = null;
	}

	// Token: 0x06000E47 RID: 3655 RVA: 0x000C15C9 File Offset: 0x000BF7C9
	public void prepare()
	{
		if (this._current_age == null)
		{
			this.setCurrentAge(WorldAgeLibrary.hope, true);
		}
	}

	// Token: 0x06000E48 RID: 3656 RVA: 0x000C15DF File Offset: 0x000BF7DF
	public bool isWinter()
	{
		return this._current_age != null && this._current_age.flag_winter;
	}

	// Token: 0x06000E49 RID: 3657 RVA: 0x000C15F6 File Offset: 0x000BF7F6
	public bool isNight()
	{
		return this._current_age != null && this._current_age.flag_night;
	}

	// Token: 0x06000E4A RID: 3658 RVA: 0x000C160D File Offset: 0x000BF80D
	public bool isChaosAge()
	{
		return this._current_age != null && this._current_age.flag_chaos;
	}

	// Token: 0x06000E4B RID: 3659 RVA: 0x000C1624 File Offset: 0x000BF824
	public bool isLightAge()
	{
		return this._current_age != null && this._current_age.flag_light_age;
	}

	// Token: 0x06000E4C RID: 3660 RVA: 0x000C163B File Offset: 0x000BF83B
	public bool isCurrentAge(WorldAgeAsset pAgeAsset)
	{
		return this._current_age == pAgeAsset;
	}

	// Token: 0x06000E4D RID: 3661 RVA: 0x000C1646 File Offset: 0x000BF846
	private WorldAgeAsset checkAge(WorldAgeAsset pAsset)
	{
		if (pAsset.id == "age_unknown")
		{
			pAsset = AssetManager.era_library.list_only_normal.GetRandom<WorldAgeAsset>();
		}
		return pAsset;
	}

	// Token: 0x06000E4E RID: 3662 RVA: 0x000C166C File Offset: 0x000BF86C
	public int calculateMoonsLeft()
	{
		return (int)((WorldAgeManager._map_stats.current_world_ages_duration * (1f - WorldAgeManager._map_stats.current_age_progress) / 5f + 1f) / WorldAgeManager._map_stats.world_ages_speed_multiplier);
	}

	// Token: 0x06000E4F RID: 3663 RVA: 0x000C16A4 File Offset: 0x000BF8A4
	public void setDefaultAges()
	{
		for (int i = 1; i < 9; i++)
		{
			using (ListPool<WorldAgeAsset> tPool = new ListPool<WorldAgeAsset>(AssetManager.era_library.pool_by_slots[i]))
			{
				WorldAgeAsset tAsset = tPool.GetRandom<WorldAgeAsset>();
				if (tAsset.link_default_slots)
				{
					bool tAlreadyUsed = false;
					foreach (int tSlotNumber in tAsset.default_slots)
					{
						if (tSlotNumber != i && this.getAgeFromSlot(tSlotNumber - 1) == tAsset)
						{
							tAlreadyUsed = true;
							break;
						}
					}
					if (tAlreadyUsed)
					{
						tPool.Remove(tAsset);
						tAsset = tPool.GetRandom<WorldAgeAsset>();
					}
				}
				this.setAgeToSlot(tAsset, i - 1);
			}
		}
		this.setCurrentSlotIndex(0, 0f);
	}

	// Token: 0x04000EB8 RID: 3768
	private WorldAgeEffects _effects;

	// Token: 0x04000EB9 RID: 3769
	private float _night_multiplier;

	// Token: 0x04000EBA RID: 3770
	private float _timer_special_action;

	// Token: 0x04000EBB RID: 3771
	private WorldAgeAsset _current_age;

	// Token: 0x04000EBC RID: 3772
	private float NIGHT_MAX = 0.6f;

	// Token: 0x04000EBD RID: 3773
	private float NIGHT_SPEED = 0.1f;
}
