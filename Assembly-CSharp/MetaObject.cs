using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000223 RID: 547
public abstract class MetaObject<TData> : CoreSystemObject<TData>, IMetaObject, ICoreObject where TData : MetaObjectData
{
	// Token: 0x17000104 RID: 260
	// (get) Token: 0x060013E7 RID: 5095 RVA: 0x000D996D File Offset: 0x000D7B6D
	protected virtual bool track_death_types
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000105 RID: 261
	// (get) Token: 0x060013E8 RID: 5096 RVA: 0x000D9970 File Offset: 0x000D7B70
	public List<Actor> units { get; } = new List<Actor>();

	// Token: 0x060013E9 RID: 5097 RVA: 0x000D9978 File Offset: 0x000D7B78
	public void preserveAlive()
	{
		this._force_preserve_alive = true;
	}

	// Token: 0x060013EA RID: 5098 RVA: 0x000D9981 File Offset: 0x000D7B81
	protected override void setDefaultValues()
	{
		base.setDefaultValues();
		this._units_dirty = true;
		this._force_preserve_alive = true;
	}

	// Token: 0x060013EB RID: 5099 RVA: 0x000D9997 File Offset: 0x000D7B97
	public virtual bool isReadyForRemoval()
	{
		return !this._force_preserve_alive && this.units.Count <= 0;
	}

	// Token: 0x060013EC RID: 5100 RVA: 0x000D99B4 File Offset: 0x000D7BB4
	internal virtual void clearListUnits()
	{
		this._force_preserve_alive = false;
		this.units.Clear();
	}

	// Token: 0x060013ED RID: 5101 RVA: 0x000D99C8 File Offset: 0x000D7BC8
	public virtual void listUnit(Actor pActor)
	{
		this.units.Add(pActor);
	}

	// Token: 0x060013EE RID: 5102 RVA: 0x000D99D6 File Offset: 0x000D7BD6
	public bool isLocked()
	{
		return this.isDirtyUnits();
	}

	// Token: 0x060013EF RID: 5103 RVA: 0x000D99DE File Offset: 0x000D7BDE
	public bool isDirtyUnits()
	{
		return this._units_dirty;
	}

	// Token: 0x060013F0 RID: 5104 RVA: 0x000D99E6 File Offset: 0x000D7BE6
	public void unDirty()
	{
		this.stats_dirty_version++;
		this._units_dirty = false;
	}

	// Token: 0x060013F1 RID: 5105 RVA: 0x000D99FD File Offset: 0x000D7BFD
	public void setDirty()
	{
		this._units_dirty = true;
	}

	// Token: 0x060013F2 RID: 5106 RVA: 0x000D9A06 File Offset: 0x000D7C06
	public virtual void updateDirty()
	{
	}

	// Token: 0x060013F3 RID: 5107 RVA: 0x000D9A08 File Offset: 0x000D7C08
	public override void Dispose()
	{
		if (!Config.disable_dispose_logs)
		{
			Debug.Log("MetaObject::Dispose " + this.data.id.ToString() + " " + this.data.name);
		}
		this.clearListUnits();
		this._cached_color = null;
		this.clearCachedVisibleUnit();
		base.Dispose();
	}

	// Token: 0x060013F4 RID: 5108 RVA: 0x000D9A71 File Offset: 0x000D7C71
	protected virtual ColorLibrary getColorLibrary()
	{
		throw new NotImplementedException(base.GetType().Name);
	}

	// Token: 0x060013F5 RID: 5109 RVA: 0x000D9A83 File Offset: 0x000D7C83
	public override bool updateColor(ColorAsset pColor)
	{
		if (this.getColor() == pColor)
		{
			return false;
		}
		this.data.setColorID(this.getColorLibrary().list.IndexOf(pColor));
		this._cached_color = null;
		return true;
	}

	// Token: 0x060013F6 RID: 5110 RVA: 0x000D9AB9 File Offset: 0x000D7CB9
	public bool isCursorOver()
	{
		return this._cursor_over > 0;
	}

	// Token: 0x060013F7 RID: 5111 RVA: 0x000D9AC4 File Offset: 0x000D7CC4
	public void setCursorOver()
	{
		this._cursor_over = 3;
	}

	// Token: 0x060013F8 RID: 5112 RVA: 0x000D9ACD File Offset: 0x000D7CCD
	public void clearCursorOver()
	{
		if (this._cursor_over > 0)
		{
			this._cursor_over--;
		}
	}

	// Token: 0x060013F9 RID: 5113 RVA: 0x000D9AE6 File Offset: 0x000D7CE6
	public override ColorAsset getColor()
	{
		if (this._cached_color == null)
		{
			this._cached_color = this.getColorLibrary().list[this.data.color_id];
		}
		return this._cached_color;
	}

	// Token: 0x060013FA RID: 5114 RVA: 0x000D9B1C File Offset: 0x000D7D1C
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
		BaseSystemData baseSystemData = this.data;
		if (baseSystemData.past_names == null)
		{
			baseSystemData.past_names = new List<NameEntry>();
		}
		if (this.data.past_names.Count == 0)
		{
			NameEntry tNewEntry = new NameEntry(this.data.name, false, this.data.original_color_id, this.data.created_time);
			this.data.past_names.Add(tNewEntry);
			return;
		}
		if (this.data.past_names.Last<NameEntry>().name == this.data.name)
		{
			return;
		}
		NameEntry tNewEntry2 = new NameEntry(this.data.name, this.data.custom_name, this.data.color_id);
		this.data.past_names.Add(tNewEntry2);
	}

	// Token: 0x060013FB RID: 5115 RVA: 0x000D9C72 File Offset: 0x000D7E72
	protected virtual void generateNewMetaObject(bool pAddDefaultTraits)
	{
		this.generateNewMetaObject();
	}

	// Token: 0x060013FC RID: 5116 RVA: 0x000D9C7A File Offset: 0x000D7E7A
	protected virtual void generateNewMetaObject()
	{
		this.generateColor();
		this.generateBanner();
	}

	// Token: 0x060013FD RID: 5117 RVA: 0x000D9C88 File Offset: 0x000D7E88
	public virtual void generateBanner()
	{
		throw new NotImplementedException(base.GetType().Name);
	}

	// Token: 0x060013FE RID: 5118 RVA: 0x000D9C9C File Offset: 0x000D7E9C
	protected virtual void generateColor()
	{
		ActorAsset tActorAsset = this.getActorAsset();
		int tNewColorIndex = this.getColorLibrary().getNextColorIndex(tActorAsset);
		this.data.setColorID(tNewColorIndex);
	}

	// Token: 0x060013FF RID: 5119 RVA: 0x000D9CCE File Offset: 0x000D7ECE
	public bool isSelected()
	{
		return SelectedObjects.isNanoObjectSelected(this);
	}

	// Token: 0x06001400 RID: 5120 RVA: 0x000D9CD6 File Offset: 0x000D7ED6
	public virtual int countUnits()
	{
		return this.units.Count;
	}

	// Token: 0x06001401 RID: 5121 RVA: 0x000D9CE3 File Offset: 0x000D7EE3
	public virtual IEnumerable<Actor> getUnits()
	{
		return this.units;
	}

	// Token: 0x06001402 RID: 5122 RVA: 0x000D9CEB File Offset: 0x000D7EEB
	public virtual Actor getRandomUnit()
	{
		return Randy.getRandom<Actor>(this.units);
	}

	// Token: 0x06001403 RID: 5123 RVA: 0x000D9CF8 File Offset: 0x000D7EF8
	public Actor getRandomActorForReaper()
	{
		foreach (Actor tActor in this.units.LoopRandom<Actor>())
		{
			if (tActor.isAlive())
			{
				return tActor;
			}
		}
		return null;
	}

	// Token: 0x06001404 RID: 5124 RVA: 0x000D9D54 File Offset: 0x000D7F54
	public virtual int countHappyUnits()
	{
		int tResult = 0;
		foreach (Actor tActor in this.getUnits())
		{
			if (!tActor.asset.is_boat && tActor.isHappy())
			{
				tResult++;
			}
		}
		return tResult;
	}

	// Token: 0x06001405 RID: 5125 RVA: 0x000D9DB8 File Offset: 0x000D7FB8
	public virtual int countUnhappyUnits()
	{
		int tResult = 0;
		foreach (Actor tActor in this.getUnits())
		{
			if (!tActor.asset.is_boat && tActor.isUnhappy())
			{
				tResult++;
			}
		}
		return tResult;
	}

	// Token: 0x06001406 RID: 5126 RVA: 0x000D9E1C File Offset: 0x000D801C
	public virtual int countSingleMales()
	{
		int tResult = 0;
		foreach (Actor tActor in this.getUnits())
		{
			if (tActor.isBreedingAge() && tActor.isSexMale() && !tActor.hasLover())
			{
				tResult++;
			}
		}
		return tResult;
	}

	// Token: 0x06001407 RID: 5127 RVA: 0x000D9E84 File Offset: 0x000D8084
	public virtual int countCouples()
	{
		int tResult = 0;
		using (IEnumerator<Actor> enumerator = this.getUnits().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.hasLover())
				{
					tResult++;
				}
			}
		}
		return tResult / 2;
	}

	// Token: 0x06001408 RID: 5128 RVA: 0x000D9EDC File Offset: 0x000D80DC
	public virtual int countSingleFemales()
	{
		int tResult = 0;
		foreach (Actor tActor in this.getUnits())
		{
			if (tActor.isBreedingAge() && tActor.isSexFemale() && !tActor.hasLover())
			{
				tResult++;
			}
		}
		return tResult;
	}

	// Token: 0x06001409 RID: 5129 RVA: 0x000D9F44 File Offset: 0x000D8144
	public virtual int countHoused()
	{
		int tResult = 0;
		foreach (Actor tActor in this.getUnits())
		{
			if (!tActor.asset.is_boat && tActor.hasHouse())
			{
				tResult++;
			}
		}
		return tResult;
	}

	// Token: 0x0600140A RID: 5130 RVA: 0x000D9FA8 File Offset: 0x000D81A8
	public virtual int countHomeless()
	{
		int tResult = 0;
		foreach (Actor tActor in this.getUnits())
		{
			if (!tActor.asset.is_boat && !tActor.hasHouse())
			{
				tResult++;
			}
		}
		return tResult;
	}

	// Token: 0x0600140B RID: 5131 RVA: 0x000DA00C File Offset: 0x000D820C
	public virtual int countStarving()
	{
		int tResult = 0;
		using (IEnumerator<Actor> enumerator = this.getUnits().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isStarving())
				{
					tResult++;
				}
			}
		}
		return tResult;
	}

	// Token: 0x0600140C RID: 5132 RVA: 0x000DA060 File Offset: 0x000D8260
	public virtual int countHungry()
	{
		int tResult = 0;
		using (IEnumerator<Actor> enumerator = this.getUnits().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isHungry())
				{
					tResult++;
				}
			}
		}
		return tResult;
	}

	// Token: 0x0600140D RID: 5133 RVA: 0x000DA0B4 File Offset: 0x000D82B4
	public virtual int countSick()
	{
		int tResult = 0;
		using (IEnumerator<Actor> enumerator = this.getUnits().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isSick())
				{
					tResult++;
				}
			}
		}
		return tResult;
	}

	// Token: 0x0600140E RID: 5134 RVA: 0x000DA108 File Offset: 0x000D8308
	public virtual int countAdults()
	{
		int tCount = 0;
		foreach (Actor tActor in this.getUnits())
		{
			if (tActor.isAlive() && !tActor.asset.is_boat && tActor.isAdult())
			{
				tCount++;
			}
		}
		return tCount;
	}

	// Token: 0x0600140F RID: 5135 RVA: 0x000DA174 File Offset: 0x000D8374
	public virtual int countTotalMoney()
	{
		int tCount = 0;
		foreach (Actor tActor in this.getUnits())
		{
			if (tActor.isAlive())
			{
				tCount += tActor.money;
			}
		}
		return tCount;
	}

	// Token: 0x06001410 RID: 5136 RVA: 0x000DA1D0 File Offset: 0x000D83D0
	public int countPotentialParents(ActorSex pSex)
	{
		int tCount = 0;
		foreach (Actor tActor in this.getUnits())
		{
			if (tActor.isAlive() && !tActor.asset.is_boat && tActor.data.sex == pSex && tActor.canBreed() && !tActor.hasReachedOffspringLimit())
			{
				tCount++;
			}
		}
		return tCount;
	}

	// Token: 0x06001411 RID: 5137 RVA: 0x000DA250 File Offset: 0x000D8450
	public int countUnitsWithStatus(string pStatusID)
	{
		int tCount = 0;
		foreach (Actor tActor in this.getUnits())
		{
			if (tActor.isAlive() && tActor.hasStatus(pStatusID))
			{
				tCount++;
			}
		}
		return tCount;
	}

	// Token: 0x06001412 RID: 5138 RVA: 0x000DA2B0 File Offset: 0x000D84B0
	public virtual int countChildren()
	{
		int tCount = 0;
		foreach (Actor tActor in this.getUnits())
		{
			if (!tActor.asset.is_boat && tActor.isAlive() && tActor.isBaby())
			{
				tCount++;
			}
		}
		return tCount;
	}

	// Token: 0x06001413 RID: 5139 RVA: 0x000DA31C File Offset: 0x000D851C
	public virtual IEnumerable<Family> getFamilies()
	{
		MetaObject<TData>._family_counter.Clear();
		try
		{
			foreach (Actor tActor in this.getUnits())
			{
				if (tActor.hasFamily() && MetaObject<TData>._family_counter.Add(tActor.family))
				{
					yield return tActor.family;
				}
			}
			IEnumerator<Actor> enumerator = null;
		}
		finally
		{
			MetaObject<TData>._family_counter.Clear();
		}
		yield break;
		yield break;
	}

	// Token: 0x06001414 RID: 5140 RVA: 0x000DA32C File Offset: 0x000D852C
	public virtual bool hasFamilies()
	{
		using (IEnumerator<Actor> enumerator = this.getUnits().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.hasFamily())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001415 RID: 5141 RVA: 0x000DA380 File Offset: 0x000D8580
	public virtual int countFamilies()
	{
		int tCount = 0;
		foreach (Family family in this.getFamilies())
		{
			tCount++;
		}
		return tCount;
	}

	// Token: 0x06001416 RID: 5142 RVA: 0x000DA3D0 File Offset: 0x000D85D0
	public int countKings()
	{
		int tResult = 0;
		using (IEnumerator<Actor> enumerator = this.getUnits().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isKing())
				{
					tResult++;
				}
			}
		}
		return tResult;
	}

	// Token: 0x06001417 RID: 5143 RVA: 0x000DA424 File Offset: 0x000D8624
	public int countLeaders()
	{
		int tResult = 0;
		using (IEnumerator<Actor> enumerator = this.getUnits().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isCityLeader())
				{
					tResult++;
				}
			}
		}
		return tResult;
	}

	// Token: 0x06001418 RID: 5144 RVA: 0x000DA478 File Offset: 0x000D8678
	public virtual int countMales()
	{
		int tResult = 0;
		foreach (Actor tActor in this.getUnits())
		{
			if (tActor.isAlive() && tActor.isSexMale())
			{
				tResult++;
			}
		}
		return tResult;
	}

	// Token: 0x06001419 RID: 5145 RVA: 0x000DA4D8 File Offset: 0x000D86D8
	public virtual int countFemales()
	{
		int tResult = 0;
		foreach (Actor tActor in this.getUnits())
		{
			if (tActor.isAlive() && tActor.isSexFemale())
			{
				tResult++;
			}
		}
		return tResult;
	}

	// Token: 0x0600141A RID: 5146 RVA: 0x000DA538 File Offset: 0x000D8738
	public virtual int countPopulationPercentage()
	{
		float num = (float)this.countUnits();
		int tTotalPopulation = World.world.units.Count;
		return (int)(num / (float)tTotalPopulation * 100f);
	}

	// Token: 0x0600141B RID: 5147 RVA: 0x000DA568 File Offset: 0x000D8768
	public virtual void increaseDeaths(AttackType pType)
	{
		if (!base.isAlive())
		{
			return;
		}
		long num = this.data.total_deaths;
		this.data.total_deaths = num + 1L;
		if (!this.track_death_types)
		{
			return;
		}
		switch (pType)
		{
		case AttackType.Acid:
			num = this.data.deaths_acid;
			this.data.deaths_acid = num + 1L;
			return;
		case AttackType.Fire:
			num = this.data.deaths_fire;
			this.data.deaths_fire = num + 1L;
			return;
		case AttackType.Plague:
			num = this.data.deaths_plague;
			this.data.deaths_plague = num + 1L;
			return;
		case AttackType.Infection:
			num = this.data.deaths_infection;
			this.data.deaths_infection = num + 1L;
			return;
		case AttackType.Tumor:
			num = this.data.deaths_tumor;
			this.data.deaths_tumor = num + 1L;
			return;
		case AttackType.Divine:
			num = this.data.deaths_divine;
			this.data.deaths_divine = num + 1L;
			return;
		case AttackType.Metamorphosis:
			num = this.data.metamorphosis;
			this.data.metamorphosis = num + 1L;
			return;
		case AttackType.Starvation:
			num = this.data.deaths_hunger;
			this.data.deaths_hunger = num + 1L;
			return;
		case AttackType.Eaten:
			num = this.data.deaths_eaten;
			this.data.deaths_eaten = num + 1L;
			return;
		case AttackType.Age:
			num = this.data.deaths_natural;
			this.data.deaths_natural = num + 1L;
			return;
		case AttackType.Weapon:
			num = this.data.deaths_weapon;
			this.data.deaths_weapon = num + 1L;
			return;
		case AttackType.Poison:
			num = this.data.deaths_poison;
			this.data.deaths_poison = num + 1L;
			return;
		case AttackType.Gravity:
			num = this.data.deaths_gravity;
			this.data.deaths_gravity = num + 1L;
			return;
		case AttackType.Drowning:
			num = this.data.deaths_drowning;
			this.data.deaths_drowning = num + 1L;
			return;
		case AttackType.Water:
			num = this.data.deaths_water;
			this.data.deaths_water = num + 1L;
			return;
		case AttackType.Explosion:
			num = this.data.deaths_explosion;
			this.data.deaths_explosion = num + 1L;
			return;
		}
		num = this.data.deaths_other;
		this.data.deaths_other = num + 1L;
	}

	// Token: 0x0600141C RID: 5148 RVA: 0x000DA854 File Offset: 0x000D8A54
	public virtual void increaseBirths()
	{
		if (!base.isAlive())
		{
			return;
		}
		long total_births = this.data.total_births;
		this.data.total_births = total_births + 1L;
	}

	// Token: 0x0600141D RID: 5149 RVA: 0x000DA88C File Offset: 0x000D8A8C
	public virtual void increaseKills()
	{
		if (!base.isAlive())
		{
			return;
		}
		long total_kills = this.data.total_kills;
		this.data.total_kills = total_kills + 1L;
	}

	// Token: 0x0600141E RID: 5150 RVA: 0x000DA8C4 File Offset: 0x000D8AC4
	private void clearCachedVisibleUnit()
	{
		this._cached_visible_unit = null;
		this._cached_visible_unit_id = -1L;
		this._timestamp_last_visible = -1.0;
	}

	// Token: 0x0600141F RID: 5151 RVA: 0x000DA8E4 File Offset: 0x000D8AE4
	public Actor getOldestVisibleUnitForNameplatesCached()
	{
		if (World.world.getWorldTimeElapsedSince(this._timestamp_last_visible) > 5f)
		{
			this._cached_visible_unit = null;
		}
		if (!this._cached_visible_unit.isRekt() && (!this._cached_visible_unit.current_zone.visible_main_centered || this._cached_visible_unit.id != this._cached_visible_unit_id))
		{
			this.clearCachedVisibleUnit();
		}
		if (this._cached_visible_unit != null)
		{
			return this._cached_visible_unit;
		}
		this._timestamp_last_visible = World.world.getCurWorldTime();
		this._cached_visible_unit = this.getOldestVisibleUnit();
		if (this._cached_visible_unit != null)
		{
			this._cached_visible_unit_id = this._cached_visible_unit.data.id;
		}
		else
		{
			this.clearCachedVisibleUnit();
		}
		return this._cached_visible_unit;
	}

	// Token: 0x06001420 RID: 5152 RVA: 0x000DA9A0 File Offset: 0x000D8BA0
	public Actor getOldestVisibleUnit()
	{
		Actor tResult = null;
		foreach (Actor tActor in this.units)
		{
			if (!tActor.asset.is_boat && tActor.isAlive() && tActor.current_zone.visible_main_centered && (tResult == null || tActor.data.created_time < tResult.data.created_time))
			{
				tResult = tActor;
			}
		}
		return tResult;
	}

	// Token: 0x06001421 RID: 5153 RVA: 0x000DAA30 File Offset: 0x000D8C30
	public virtual Sprite getTopicSprite()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06001422 RID: 5154 RVA: 0x000DAA37 File Offset: 0x000D8C37
	public long getTotalDeaths()
	{
		return this.data.total_deaths;
	}

	// Token: 0x06001423 RID: 5155 RVA: 0x000DAA49 File Offset: 0x000D8C49
	public long getTotalBirths()
	{
		return this.data.total_births;
	}

	// Token: 0x06001424 RID: 5156 RVA: 0x000DAA5B File Offset: 0x000D8C5B
	public long getTotalKills()
	{
		return this.data.total_kills;
	}

	// Token: 0x06001425 RID: 5157 RVA: 0x000DAA6D File Offset: 0x000D8C6D
	public long getEvolutions()
	{
		return this.data.evolutions;
	}

	// Token: 0x06001426 RID: 5158 RVA: 0x000DAA80 File Offset: 0x000D8C80
	public void increaseEvolutions()
	{
		long evolutions = this.data.evolutions;
		this.data.evolutions = evolutions + 1L;
	}

	// Token: 0x06001427 RID: 5159 RVA: 0x000DAAB0 File Offset: 0x000D8CB0
	public long getDeaths(AttackType pType)
	{
		switch (pType)
		{
		case AttackType.Acid:
			return this.data.deaths_acid;
		case AttackType.Fire:
			return this.data.deaths_fire;
		case AttackType.Plague:
			return this.data.deaths_plague;
		case AttackType.Infection:
			return this.data.deaths_infection;
		case AttackType.Tumor:
			return this.data.deaths_tumor;
		case AttackType.Other:
		case AttackType.AshFever:
		case AttackType.None:
			return this.data.deaths_other;
		case AttackType.Divine:
			return this.data.deaths_divine;
		case AttackType.Metamorphosis:
			return this.data.metamorphosis;
		case AttackType.Starvation:
			return this.data.deaths_hunger;
		case AttackType.Eaten:
			return this.data.deaths_eaten;
		case AttackType.Age:
			return this.data.deaths_natural;
		case AttackType.Weapon:
			return this.data.deaths_weapon;
		case AttackType.Poison:
			return this.data.deaths_poison;
		case AttackType.Gravity:
			return this.data.deaths_gravity;
		case AttackType.Drowning:
			return this.data.deaths_drowning;
		case AttackType.Water:
			return this.data.deaths_water;
		case AttackType.Explosion:
			return this.data.deaths_explosion;
		default:
			throw new ArgumentOutOfRangeException(string.Format("Unknown attack type: {0}", pType));
		}
	}

	// Token: 0x06001428 RID: 5160 RVA: 0x000DAC4C File Offset: 0x000D8E4C
	public void addRenown(int pAmount)
	{
		ref TData ptr = ref this.data;
		ptr.renown += pAmount;
	}

	// Token: 0x06001429 RID: 5161 RVA: 0x000DAC7C File Offset: 0x000D8E7C
	public void addRenown(int pAmount, float pPercent)
	{
		int tRenown = (int)((float)pAmount * pPercent);
		this.addRenown(tRenown);
	}

	// Token: 0x17000106 RID: 262
	// (get) Token: 0x0600142A RID: 5162 RVA: 0x000DAC96 File Offset: 0x000D8E96
	public MetaTypeAsset meta_type_asset
	{
		get
		{
			return AssetManager.meta_type_library.getAsset(this.meta_type);
		}
	}

	// Token: 0x0600142B RID: 5163 RVA: 0x000DACA8 File Offset: 0x000D8EA8
	public virtual void clearLastYearStats()
	{
	}

	// Token: 0x0600142C RID: 5164 RVA: 0x000DACAA File Offset: 0x000D8EAA
	public virtual void convertSameSpeciesAroundUnit(Actor pActorMain, bool pOverrideExisting = false)
	{
		throw new NotImplementedException(base.GetType().Name);
	}

	// Token: 0x0600142D RID: 5165 RVA: 0x000DACBC File Offset: 0x000D8EBC
	public virtual void forceConvertSameSpeciesAroundUnit(Actor pActorMain)
	{
		throw new NotImplementedException(base.GetType().Name);
	}

	// Token: 0x0600142E RID: 5166 RVA: 0x000DACCE File Offset: 0x000D8ECE
	public virtual ActorAsset getActorAsset()
	{
		return null;
	}

	// Token: 0x0600142F RID: 5167 RVA: 0x000DACD1 File Offset: 0x000D8ED1
	public IEnumerable<Actor> getUnitFromChunkForConversion(Actor pActorMain)
	{
		foreach (Actor tActor in Finder.getUnitsFromChunk(pActorMain.current_tile, 1, 0f, false))
		{
			if (tActor.isSameSpecies(pActorMain) && (!tActor.hasCity() || tActor.hasSameCity(pActorMain)))
			{
				yield return tActor;
			}
		}
		IEnumerator<Actor> enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x06001430 RID: 5168 RVA: 0x000DACE1 File Offset: 0x000D8EE1
	public Sprite getSpriteIcon()
	{
		return this.getActorAsset().getSpriteIcon();
	}

	// Token: 0x06001431 RID: 5169 RVA: 0x000DACF0 File Offset: 0x000D8EF0
	public void allAngryAt(Actor pActorTarget, float pDistance)
	{
		float tDistance = pDistance * pDistance;
		WorldTile tTile = pActorTarget.current_tile;
		bool tHasPossessed = pActorTarget.hasStatus("possessed");
		foreach (Actor tActor in this.getUnits())
		{
			if (tActor != pActorTarget && !tActor.isRekt() && (float)Toolbox.SquaredDistTile(tActor.current_tile, tTile) <= tDistance && (!tHasPossessed || !tActor.hasStatus("possessed_follower")))
			{
				tActor.addAggro(pActorTarget);
			}
		}
	}

	// Token: 0x06001432 RID: 5170 RVA: 0x000DAD88 File Offset: 0x000D8F88
	public virtual bool hasUnits()
	{
		foreach (Actor tActor in this.getUnits())
		{
			if (!tActor.isRekt() && !tActor.asset.is_boat)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001433 RID: 5171 RVA: 0x000DADEC File Offset: 0x000D8FEC
	public virtual void triggerOnRemoveObject()
	{
	}

	// Token: 0x06001434 RID: 5172 RVA: 0x000DADEE File Offset: 0x000D8FEE
	public MetaObjectData getMetaData()
	{
		return this.data;
	}

	// Token: 0x06001435 RID: 5173 RVA: 0x000DADFB File Offset: 0x000D8FFB
	public int getRenown()
	{
		return this.data.renown;
	}

	// Token: 0x06001436 RID: 5174 RVA: 0x000DAE0D File Offset: 0x000D900D
	public virtual int getPopulationPeople()
	{
		return this.units.Count;
	}

	// Token: 0x06001437 RID: 5175 RVA: 0x000DAE1A File Offset: 0x000D901A
	public virtual bool hasCities()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06001438 RID: 5176 RVA: 0x000DAE21 File Offset: 0x000D9021
	public virtual IEnumerable<City> getCities()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06001439 RID: 5177 RVA: 0x000DAE28 File Offset: 0x000D9028
	public virtual bool hasKingdoms()
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600143A RID: 5178 RVA: 0x000DAE2F File Offset: 0x000D902F
	public virtual IEnumerable<Kingdom> getKingdoms()
	{
		throw new NotImplementedException();
	}

	// Token: 0x04001195 RID: 4501
	private bool _units_dirty;

	// Token: 0x04001197 RID: 4503
	protected static readonly HashSet<Family> _family_counter = new HashSet<Family>();

	// Token: 0x04001198 RID: 4504
	private ColorAsset _cached_color;

	// Token: 0x04001199 RID: 4505
	private bool _force_preserve_alive;

	// Token: 0x0400119A RID: 4506
	private int _cursor_over;

	// Token: 0x0400119B RID: 4507
	private double _timestamp_last_visible = -1.0;

	// Token: 0x0400119C RID: 4508
	private Actor _cached_visible_unit;

	// Token: 0x0400119D RID: 4509
	private long _cached_visible_unit_id;
}
