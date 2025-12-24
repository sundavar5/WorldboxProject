using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020001C1 RID: 449
public class BaseSimObject : NanoObject, IEquatable<BaseSimObject>
{
	// Token: 0x17000064 RID: 100
	// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x000BA2F5 File Offset: 0x000B84F5
	public TileIsland current_island
	{
		get
		{
			return this.current_tile.region.island;
		}
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x000BA307 File Offset: 0x000B8507
	public TileZone current_zone
	{
		get
		{
			return this.current_tile.zone;
		}
	}

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x000BA314 File Offset: 0x000B8514
	public MapChunk current_chunk
	{
		get
		{
			return this.current_tile.chunk;
		}
	}

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x000BA321 File Offset: 0x000B8521
	public MapRegion current_region
	{
		get
		{
			return this.current_tile.region;
		}
	}

	// Token: 0x06000CF6 RID: 3318 RVA: 0x000BA32E File Offset: 0x000B852E
	internal virtual void create()
	{
	}

	// Token: 0x06000CF7 RID: 3319 RVA: 0x000BA330 File Offset: 0x000B8530
	public int countStatusEffects()
	{
		return this._active_status_dict.Count;
	}

	// Token: 0x06000CF8 RID: 3320 RVA: 0x000BA33D File Offset: 0x000B853D
	public Dictionary<string, Status>.ValueCollection getStatuses()
	{
		return this._active_status_dict.Values;
	}

	// Token: 0x06000CF9 RID: 3321 RVA: 0x000BA34A File Offset: 0x000B854A
	public Dictionary<string, Status>.KeyCollection getStatusesIds()
	{
		return this._active_status_dict.Keys;
	}

	// Token: 0x06000CFA RID: 3322 RVA: 0x000BA357 File Offset: 0x000B8557
	public IReadOnlyDictionary<string, Status> getStatusesDict()
	{
		return this._active_status_dict;
	}

	// Token: 0x06000CFB RID: 3323 RVA: 0x000BA35F File Offset: 0x000B855F
	protected override void setDefaultValues()
	{
		base.setDefaultValues();
		this._stats_dirty = true;
		this.event_full_stats = false;
		this.current_rotation = default(Vector3);
		this.position_height = 0f;
		this._has_any_status_cached = false;
		this._has_any_status_to_render = false;
	}

	// Token: 0x06000CFC RID: 3324 RVA: 0x000BA39A File Offset: 0x000B859A
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasCity()
	{
		return this.getCity() != null;
	}

	// Token: 0x06000CFD RID: 3325 RVA: 0x000BA3A5 File Offset: 0x000B85A5
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public virtual City getCity()
	{
		return null;
	}

	// Token: 0x06000CFE RID: 3326 RVA: 0x000BA3A8 File Offset: 0x000B85A8
	internal bool addStatusEffect(string pID, float pOverrideTimer = 0f, bool pColorEffect = true)
	{
		StatusAsset tStatusAsset = AssetManager.status.get(pID);
		return tStatusAsset != null && this.addStatusEffect(tStatusAsset, pOverrideTimer, pColorEffect);
	}

	// Token: 0x06000CFF RID: 3327 RVA: 0x000BA3D0 File Offset: 0x000B85D0
	internal virtual bool addStatusEffect(StatusAsset pStatusAsset, float pOverrideTimer = 0f, bool pColorEffect = true)
	{
		if (!base.isAlive())
		{
			return false;
		}
		bool tIsActor = this.isActor();
		if (tIsActor && this.a.asset.allowed_status_tiers < pStatusAsset.tier)
		{
			return false;
		}
		bool tHasAnyStatus = this.hasAnyStatusEffectRaw();
		if (tHasAnyStatus && this.hasStatus(pStatusAsset.id))
		{
			if (!pStatusAsset.allow_timer_reset && pOverrideTimer == 0f)
			{
				return false;
			}
			Status tStatusEffect = this._active_status_dict[pStatusAsset.id];
			float tResetTimer = pStatusAsset.duration;
			if (pOverrideTimer != 0f)
			{
				tResetTimer = pOverrideTimer;
			}
			if (tStatusEffect.getRemainingTime() < (double)tResetTimer)
			{
				tStatusEffect.setDuration(tResetTimer);
			}
			return true;
		}
		else
		{
			if (!this.canAddStatus(pStatusAsset, tIsActor, pColorEffect))
			{
				return false;
			}
			this.addNewStatusEffect(pStatusAsset, pOverrideTimer, pColorEffect, tIsActor, tHasAnyStatus);
			return true;
		}
	}

	// Token: 0x06000D00 RID: 3328 RVA: 0x000BA484 File Offset: 0x000B8684
	private bool canAddStatus(StatusAsset pStatusAsset, bool pIsActor, bool pHasAnyStatus)
	{
		if (pIsActor)
		{
			if (pStatusAsset.opposite_traits != null)
			{
				for (int i = 0; i < pStatusAsset.opposite_traits.Length; i++)
				{
					string tTraitID = pStatusAsset.opposite_traits[i];
					if (this.a.hasTrait(tTraitID))
					{
						return false;
					}
				}
			}
			if (pStatusAsset.opposite_tags != null && this.a.stats.hasTags() && this.a.stats.hasTags(pStatusAsset.opposite_tags))
			{
				return false;
			}
		}
		if (pStatusAsset.opposite_status != null && pHasAnyStatus)
		{
			for (int j = 0; j < pStatusAsset.opposite_status.Length; j++)
			{
				string tStatusID = pStatusAsset.opposite_status[j];
				if (this.hasStatus(tStatusID))
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06000D01 RID: 3329 RVA: 0x000BA534 File Offset: 0x000B8734
	private void addNewStatusEffect(StatusAsset pStatusAsset, float pOverrideTimer, bool pColorEffect, bool pIsActor, bool pHasAnyStatus)
	{
		Status tNewStatus = World.world.statuses.newStatus(this, pStatusAsset, pOverrideTimer);
		this.setStatsDirty();
		this._active_status_dict.Add(pStatusAsset.id, tNewStatus);
		this._has_any_status_cached = true;
		if (pIsActor && pStatusAsset.cancel_actor_job && pColorEffect)
		{
			this.a.cancelAllBeh();
			this.a.startColorEffect(ActorColorEffect.White);
		}
		if (pStatusAsset.remove_status != null && pHasAnyStatus)
		{
			for (int i = 0; i < pStatusAsset.remove_status.Length; i++)
			{
				string tStatusToFinish = pStatusAsset.remove_status[i];
				this.finishStatusEffect(tStatusToFinish);
			}
		}
		if (pIsActor)
		{
			WorldAction action_on_receive = pStatusAsset.action_on_receive;
			if (action_on_receive == null)
			{
				return;
			}
			action_on_receive(this, null);
		}
	}

	// Token: 0x06000D02 RID: 3330 RVA: 0x000BA5E4 File Offset: 0x000B87E4
	internal void finishAllStatusEffects()
	{
		foreach (Status status in this._active_status_dict.Values)
		{
			status.finish();
			this.setStatsDirty();
		}
		this._active_status_dict.Clear();
		this._has_any_status_cached = false;
		this._has_any_status_to_render = false;
	}

	// Token: 0x06000D03 RID: 3331 RVA: 0x000BA658 File Offset: 0x000B8858
	public void finishStatusEffect(string pID)
	{
		if (!this.hasAnyStatusEffect())
		{
			return;
		}
		Status tStatusEffect;
		if (this._active_status_dict.TryGetValue(pID, out tStatusEffect))
		{
			tStatusEffect.finish();
			this.setStatsDirty();
		}
	}

	// Token: 0x06000D04 RID: 3332 RVA: 0x000BA68A File Offset: 0x000B888A
	public virtual void setStatsDirty()
	{
		this._stats_dirty = true;
	}

	// Token: 0x06000D05 RID: 3333 RVA: 0x000BA693 File Offset: 0x000B8893
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal bool isActor()
	{
		return this._object_type == MapObjectType.Actor;
	}

	// Token: 0x06000D06 RID: 3334 RVA: 0x000BA69E File Offset: 0x000B889E
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal bool isBuilding()
	{
		return this._object_type == MapObjectType.Building;
	}

	// Token: 0x06000D07 RID: 3335 RVA: 0x000BA6A9 File Offset: 0x000B88A9
	public void setObjectType(MapObjectType pType)
	{
		this._object_type = pType;
		if (this._object_type == MapObjectType.Actor)
		{
			this.a = (Actor)this;
			return;
		}
		this.b = (Building)this;
	}

	// Token: 0x06000D08 RID: 3336 RVA: 0x000BA6D3 File Offset: 0x000B88D3
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal bool hasStatus(string pID)
	{
		return this._active_status_dict.ContainsKey(pID);
	}

	// Token: 0x06000D09 RID: 3337 RVA: 0x000BA6E1 File Offset: 0x000B88E1
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal bool hasAnyStatusEffect()
	{
		return this._has_any_status_cached;
	}

	// Token: 0x06000D0A RID: 3338 RVA: 0x000BA6E9 File Offset: 0x000B88E9
	internal bool hasAnyStatusEffectRaw()
	{
		return this._active_status_dict.Count > 0;
	}

	// Token: 0x06000D0B RID: 3339 RVA: 0x000BA6F9 File Offset: 0x000B88F9
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal bool hasAnyStatusEffectToRender()
	{
		return this._has_any_status_to_render;
	}

	// Token: 0x06000D0C RID: 3340 RVA: 0x000BA701 File Offset: 0x000B8901
	public void removeFinishedStatusEffect(Status pStatusData)
	{
		this._active_status_dict.Remove(pStatusData.asset.id);
		this._has_any_status_cached = this.hasAnyStatusEffectRaw();
		this.setStatsDirty();
	}

	// Token: 0x06000D0D RID: 3341 RVA: 0x000BA72C File Offset: 0x000B892C
	internal virtual void updateStats()
	{
		this._stats_dirty = false;
		this.stats_dirty_version++;
		this.updateCachedStatusEffects();
	}

	// Token: 0x06000D0E RID: 3342 RVA: 0x000BA749 File Offset: 0x000B8949
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isStatsDirty()
	{
		return this._stats_dirty;
	}

	// Token: 0x06000D0F RID: 3343 RVA: 0x000BA754 File Offset: 0x000B8954
	private void updateCachedStatusEffects()
	{
		this._has_any_status_cached = this.hasAnyStatusEffectRaw();
		this._has_any_status_to_render = false;
		if (this._has_any_status_cached)
		{
			foreach (Status tStatusEffect in this._active_status_dict.Values)
			{
				if (!tStatusEffect.is_finished && tStatusEffect.asset.need_visual_render)
				{
					this._has_any_status_to_render = true;
					break;
				}
			}
		}
	}

	// Token: 0x06000D10 RID: 3344 RVA: 0x000BA7E0 File Offset: 0x000B89E0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal bool isInLiquid()
	{
		return this.current_tile.Type.liquid;
	}

	// Token: 0x06000D11 RID: 3345 RVA: 0x000BA7F2 File Offset: 0x000B89F2
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal bool isInWater()
	{
		return this.current_tile.Type.ocean;
	}

	// Token: 0x06000D12 RID: 3346 RVA: 0x000BA804 File Offset: 0x000B8A04
	public bool isTouchingLiquid()
	{
		return this.isInLiquid() && !this.isInAir();
	}

	// Token: 0x06000D13 RID: 3347 RVA: 0x000BA819 File Offset: 0x000B8A19
	internal virtual bool isInAir()
	{
		return false;
	}

	// Token: 0x06000D14 RID: 3348 RVA: 0x000BA81C File Offset: 0x000B8A1C
	internal virtual bool isFlying()
	{
		return false;
	}

	// Token: 0x06000D15 RID: 3349 RVA: 0x000BA81F File Offset: 0x000B8A1F
	internal virtual float getHeight()
	{
		return 0f;
	}

	// Token: 0x06000D16 RID: 3350 RVA: 0x000BA826 File Offset: 0x000B8A26
	internal virtual void getHit(float pDamage, bool pFlash = true, AttackType pAttackType = AttackType.Other, BaseSimObject pAttacker = null, bool pSkipIfShake = true, bool pMetallicWeapon = false, bool pCheckDamageReduction = true)
	{
	}

	// Token: 0x06000D17 RID: 3351 RVA: 0x000BA828 File Offset: 0x000B8A28
	internal virtual void getHitFullHealth(AttackType pAttackType)
	{
	}

	// Token: 0x06000D18 RID: 3352 RVA: 0x000BA82C File Offset: 0x000B8A2C
	internal BaseSimObject findEnemyObjectTarget(bool pAttackBuildings)
	{
		EnemyFinderData tEnemyData = EnemiesFinder.findEnemiesFrom(this.current_tile, this.kingdom, -1);
		if (tEnemyData.isEmpty())
		{
			return null;
		}
		bool tFindClosest = true;
		if (tEnemyData.list.Count > 50)
		{
			tFindClosest = Randy.randomChance(0.6f);
		}
		IEnumerable<BaseSimObject> pList;
		if (!tFindClosest)
		{
			pList = tEnemyData.list.LoopRandom<BaseSimObject>();
		}
		else
		{
			IEnumerable<BaseSimObject> list = tEnemyData.list;
			pList = list;
		}
		return this.checkObjectList(pList, pAttackBuildings, tFindClosest, false, int.MaxValue);
	}

	// Token: 0x06000D19 RID: 3353 RVA: 0x000BA898 File Offset: 0x000B8A98
	protected BaseSimObject checkObjectList(IEnumerable<BaseSimObject> pList, bool pAttackBuildings, bool pFindClosest, bool pIgnoreStunned, int pMaxDist = 2147483647)
	{
		int tDist = int.MaxValue;
		BaseSimObject tBestObject = null;
		long tBestDist;
		if (pMaxDist != 2147483647)
		{
			tBestDist = (long)(pMaxDist * pMaxDist + 1);
		}
		else
		{
			tBestDist = (long)pMaxDist;
		}
		bool tHasMelee = this.isActor() && this.a.hasMeleeAttack();
		WorldTile tCurrentTile = this.current_tile;
		Vector2Int tCurrentPos = tCurrentTile.pos;
		foreach (BaseSimObject tObject in pList)
		{
			if (tObject.isAlive() && tObject != this)
			{
				WorldTile tObjectTile = tObject.current_tile;
				if (pFindClosest)
				{
					tDist = Toolbox.SquaredDistVec2(tObjectTile.pos, tCurrentPos);
					if ((long)tDist >= tBestDist)
					{
						continue;
					}
				}
				if ((!pIgnoreStunned || !tObject.isActor() || !tObject.a.hasStatusStunned()) && this.canAttackTarget(tObject, true, pAttackBuildings) && (!tHasMelee || tObjectTile.isSameIsland(tCurrentTile) || (!tObjectTile.Type.block && tCurrentTile.region.island.isConnectedWith(tObjectTile.region.island))) && (!tObject.isBuilding() || !this.isKingdomCiv() || !tObject.b.asset.city_building || tObject.b.asset.tower || !(tObject.kingdom.getSpecies() == this.kingdom.getSpecies())) && !this.shouldIgnoreTarget(tObject))
				{
					if (!pFindClosest)
					{
						return tObject;
					}
					if (tDist <= 4)
					{
						return tObject;
					}
					tBestObject = tObject;
					tBestDist = (long)tDist;
				}
			}
		}
		return tBestObject;
	}

	// Token: 0x06000D1A RID: 3354 RVA: 0x000BAA58 File Offset: 0x000B8C58
	internal void ignoreTarget(BaseSimObject pTarget)
	{
		if (this._targets_to_ignore == null)
		{
			this._targets_to_ignore = new HashSet<long>();
		}
		this._targets_to_ignore.Add(pTarget.getID());
	}

	// Token: 0x06000D1B RID: 3355 RVA: 0x000BAA7F File Offset: 0x000B8C7F
	internal bool shouldIgnoreTarget(BaseSimObject pTarget)
	{
		HashSet<long> targets_to_ignore = this._targets_to_ignore;
		return targets_to_ignore != null && targets_to_ignore.Contains(pTarget.getID());
	}

	// Token: 0x06000D1C RID: 3356 RVA: 0x000BAA98 File Offset: 0x000B8C98
	internal void clearIgnoreTargets()
	{
		HashSet<long> targets_to_ignore = this._targets_to_ignore;
		if (targets_to_ignore == null)
		{
			return;
		}
		targets_to_ignore.Clear();
	}

	// Token: 0x06000D1D RID: 3357 RVA: 0x000BAAAA File Offset: 0x000B8CAA
	internal int countTargetsToIgnore()
	{
		HashSet<long> targets_to_ignore = this._targets_to_ignore;
		if (targets_to_ignore == null)
		{
			return 0;
		}
		return targets_to_ignore.Count;
	}

	// Token: 0x06000D1E RID: 3358 RVA: 0x000BAAC0 File Offset: 0x000B8CC0
	internal bool canAttackTarget(BaseSimObject pTarget, bool pCheckForFactions = true, bool pAttackBuildings = true)
	{
		if (!base.isAlive())
		{
			return false;
		}
		if (!pTarget.isAlive())
		{
			return false;
		}
		bool tThisIsActor = this.isActor();
		if (pTarget.isBuilding() && !pAttackBuildings)
		{
			if (!tThisIsActor || !this.a.asset.unit_zombie)
			{
				return false;
			}
			if (!pTarget.kingdom.asset.brain)
			{
				return false;
			}
		}
		string tSpeciesID;
		WeaponType tAttackType;
		if (tThisIsActor)
		{
			if (this.a.asset.skip_fight_logic)
			{
				return false;
			}
			tSpeciesID = this.a.asset.id;
			tAttackType = this.a._attack_asset.attack_type;
		}
		else
		{
			tSpeciesID = this.b.kingdom.getSpecies();
			tAttackType = WeaponType.Range;
		}
		if (pTarget.isActor())
		{
			Actor tActorTarget = pTarget.a;
			if (!tActorTarget.asset.can_be_killed_by_stuff)
			{
				return false;
			}
			if (tActorTarget.isInsideSomething())
			{
				return false;
			}
			if (tActorTarget.isFlying() && tAttackType == WeaponType.Melee)
			{
				return false;
			}
			if (tActorTarget.ai.action != null && tActorTarget.ai.action.special_prevent_can_be_attacked)
			{
				return false;
			}
			if (tActorTarget.isInMagnet())
			{
				return false;
			}
			if (pCheckForFactions && this.areFoes(pTarget) && tActorTarget.isKingdomCiv() && this.isKingdomCiv() && !this.hasStatusTantrum() && !tActorTarget.hasStatusTantrum())
			{
				bool tXenophobicAny = (tThisIsActor && this.a.hasXenophobic()) || tActorTarget.hasXenophobic();
				bool tXenophileAny = (tThisIsActor && this.a.hasXenophiles()) || tActorTarget.hasXenophiles();
				bool tSameCulture = tThisIsActor && this.a.culture == tActorTarget.culture;
				bool tSameSpecies = tSpeciesID == tActorTarget.asset.id;
				bool tIgnoreCivilians = ((tSameSpecies || tXenophileAny) && !tXenophobicAny) || (tSameCulture && tSameSpecies);
				if (!WorldLawLibrary.world_law_angry_civilians.isEnabled())
				{
					if (tActorTarget.profession_asset.is_civilian && tIgnoreCivilians)
					{
						return false;
					}
					if (tThisIsActor && this.a.profession_asset.is_civilian && tIgnoreCivilians)
					{
						return false;
					}
				}
			}
			if (pCheckForFactions && tThisIsActor && this.a.hasCannibalism() && this.a.isSameSpecies(tActorTarget))
			{
				Family tFamilyThis = this.a.family;
				Family tFamilyTarget = tActorTarget.family;
				if (tFamilyTarget == null || tFamilyThis == null)
				{
					return false;
				}
				if (this.a.hasFamily())
				{
					if (tFamilyTarget == tFamilyThis)
					{
						return false;
					}
					if (!tFamilyTarget.areMostUnitsHungry() && !tFamilyThis.areMostUnitsHungry())
					{
						return false;
					}
				}
			}
		}
		else
		{
			Building tBuildingTarget = pTarget.b;
			if (this.isKingdomCiv() && tBuildingTarget.asset.city_building && tBuildingTarget.asset.tower && !tBuildingTarget.isCiv() && tThisIsActor && this.a.profession_asset.is_civilian && !WorldLawLibrary.world_law_angry_civilians.isEnabled() && tBuildingTarget.kingdom.getSpecies() == this.kingdom.getSpecies())
			{
				return false;
			}
		}
		if (tThisIsActor)
		{
			ActorAsset tActorAsset = this.a.asset;
			if (!this.a.isWaterCreature() || !this.a.hasRangeAttack())
			{
				if (this.a.isWaterCreature() && !tActorAsset.force_land_creature)
				{
					if (!pTarget.isInLiquid())
					{
						return false;
					}
					if (!pTarget.current_tile.isSameIsland(this.current_tile))
					{
						return false;
					}
				}
				else if (tAttackType == WeaponType.Melee && pTarget.isInLiquid() && !this.a.isWaterCreature())
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06000D1F RID: 3359 RVA: 0x000BAE28 File Offset: 0x000B9028
	public bool areFoes(BaseSimObject pTarget)
	{
		return this.kingdom.isEnemy(pTarget.kingdom);
	}

	// Token: 0x06000D20 RID: 3360 RVA: 0x000BAE3B File Offset: 0x000B903B
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void setHealth(int pValue, bool pClamp = true)
	{
		BaseObjectData data = this.getData();
		if (pClamp)
		{
			pValue = Mathf.Clamp(pValue, 1, this.getMaxHealth());
		}
		data.health = pValue;
	}

	// Token: 0x06000D21 RID: 3361 RVA: 0x000BAE5B File Offset: 0x000B905B
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void setMaxHealth()
	{
		this.setHealth(this.getMaxHealth(), true);
	}

	// Token: 0x06000D22 RID: 3362 RVA: 0x000BAE6C File Offset: 0x000B906C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void changeHealth(int pValue)
	{
		BaseObjectData data = this.getData();
		int tNewValue = data.health + pValue;
		data.health = Mathf.Clamp(tNewValue, 0, this.getMaxHealth());
	}

	// Token: 0x06000D23 RID: 3363 RVA: 0x000BAE9A File Offset: 0x000B909A
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int getHealth()
	{
		return this.getData().health;
	}

	// Token: 0x06000D24 RID: 3364 RVA: 0x000BAEA8 File Offset: 0x000B90A8
	public int getMaxHealthPercent(float pPercent)
	{
		int tResult = (int)((float)this.getMaxHealth() * pPercent);
		return Mathf.Max(1, tResult);
	}

	// Token: 0x06000D25 RID: 3365 RVA: 0x000BAEC7 File Offset: 0x000B90C7
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasHealth()
	{
		return this.getHealth() > 0;
	}

	// Token: 0x06000D26 RID: 3366 RVA: 0x000BAED2 File Offset: 0x000B90D2
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(BaseSimObject pObject)
	{
		return this._hashcode == pObject.GetHashCode();
	}

	// Token: 0x06000D27 RID: 3367 RVA: 0x000BAEE2 File Offset: 0x000B90E2
	public int getMaxHealth()
	{
		return (int)this.stats["health"];
	}

	// Token: 0x06000D28 RID: 3368 RVA: 0x000BAEF5 File Offset: 0x000B90F5
	public override void Dispose()
	{
		this.current_tile = null;
		this.kingdom = null;
		this.stats.reset();
		this.clearIgnoreTargets();
		this._targets_to_ignore = null;
		this.disposeStatusEffects();
		this.current_tile = null;
		base.Dispose();
	}

	// Token: 0x06000D29 RID: 3369 RVA: 0x000BAF30 File Offset: 0x000B9130
	private void disposeStatusEffects()
	{
		foreach (Status status in this._active_status_dict.Values)
		{
			status.finish();
		}
		this._active_status_dict.Clear();
		this._has_any_status_cached = false;
	}

	// Token: 0x06000D2A RID: 3370 RVA: 0x000BAF98 File Offset: 0x000B9198
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isKingdomCiv()
	{
		return this.kingdom.isCiv();
	}

	// Token: 0x06000D2B RID: 3371 RVA: 0x000BAFA5 File Offset: 0x000B91A5
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isKingdomMob()
	{
		return this.kingdom.isMobs();
	}

	// Token: 0x06000D2C RID: 3372 RVA: 0x000BAFB2 File Offset: 0x000B91B2
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasKingdom()
	{
		return this.kingdom != null;
	}

	// Token: 0x06000D2D RID: 3373 RVA: 0x000BAFBD File Offset: 0x000B91BD
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public virtual BaseObjectData getData()
	{
		return null;
	}

	// Token: 0x06000D2E RID: 3374 RVA: 0x000BAFC0 File Offset: 0x000B91C0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public sealed override long getID()
	{
		return this.getData().id;
	}

	// Token: 0x06000D2F RID: 3375 RVA: 0x000BAFCD File Offset: 0x000B91CD
	public override double getFoundedTimestamp()
	{
		return this.getData().created_time;
	}

	// Token: 0x06000D30 RID: 3376 RVA: 0x000BAFDA File Offset: 0x000B91DA
	public virtual bool hasStatusTantrum()
	{
		return false;
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x000BAFDD File Offset: 0x000B91DD
	public bool isSameIsland(WorldTile pTile)
	{
		return this.current_tile.isSameIsland(pTile);
	}

	// Token: 0x06000D32 RID: 3378 RVA: 0x000BAFEB File Offset: 0x000B91EB
	public bool isSameIslandAs(BaseSimObject pTarget)
	{
		return this.current_tile.isSameIsland(pTarget.current_tile);
	}

	// Token: 0x17000068 RID: 104
	// (get) Token: 0x06000D33 RID: 3379 RVA: 0x000BAFFE File Offset: 0x000B91FE
	public MapChunk chunk
	{
		get
		{
			return this.current_tile.chunk;
		}
	}

	// Token: 0x04000C93 RID: 3219
	public float position_height;

	// Token: 0x04000C94 RID: 3220
	public WorldTile current_tile;

	// Token: 0x04000C95 RID: 3221
	public Vector2 current_position;

	// Token: 0x04000C96 RID: 3222
	public Vector3 current_scale;

	// Token: 0x04000C97 RID: 3223
	internal Vector3 current_rotation;

	// Token: 0x04000C98 RID: 3224
	private HashSet<long> _targets_to_ignore;

	// Token: 0x04000C99 RID: 3225
	[NonSerialized]
	public Kingdom kingdom;

	// Token: 0x04000C9A RID: 3226
	private bool _stats_dirty;

	// Token: 0x04000C9B RID: 3227
	internal bool event_full_stats;

	// Token: 0x04000C9C RID: 3228
	internal readonly BaseStats stats = new BaseStats();

	// Token: 0x04000C9D RID: 3229
	internal Actor a;

	// Token: 0x04000C9E RID: 3230
	internal Building b;

	// Token: 0x04000C9F RID: 3231
	private MapObjectType _object_type;

	// Token: 0x04000CA0 RID: 3232
	private readonly Dictionary<string, Status> _active_status_dict = new Dictionary<string, Status>();

	// Token: 0x04000CA1 RID: 3233
	private bool _has_any_status_cached;

	// Token: 0x04000CA2 RID: 3234
	private bool _has_any_status_to_render;

	// Token: 0x04000CA3 RID: 3235
	internal Vector3 cur_transform_position;
}
