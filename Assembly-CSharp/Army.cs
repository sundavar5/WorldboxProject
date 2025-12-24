using System;
using System.Collections.Generic;
using JetBrains.Annotations;

// Token: 0x02000213 RID: 531
public class Army : MetaObject<ArmyData>
{
	// Token: 0x170000F2 RID: 242
	// (get) Token: 0x0600130F RID: 4879 RVA: 0x000D6340 File Offset: 0x000D4540
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Army;
		}
	}

	// Token: 0x170000F3 RID: 243
	// (get) Token: 0x06001310 RID: 4880 RVA: 0x000D6344 File Offset: 0x000D4544
	public override BaseSystemManager manager
	{
		get
		{
			return World.world.armies;
		}
	}

	// Token: 0x06001311 RID: 4881 RVA: 0x000D6350 File Offset: 0x000D4550
	public override ActorAsset getActorAsset()
	{
		return this.getKingdom().getActorAsset();
	}

	// Token: 0x06001312 RID: 4882 RVA: 0x000D635D File Offset: 0x000D455D
	public void createArmy(Actor pActor, City pCity)
	{
		this._city = pCity;
		this._kingdom = this._city.kingdom;
		this.setCaptain(pActor, false);
		this.generateNewMetaObject();
		this.generateName(null);
	}

	// Token: 0x06001313 RID: 4883 RVA: 0x000D638C File Offset: 0x000D458C
	public void checkCity()
	{
		if (this._city.kingdom != this._kingdom)
		{
			this._kingdom = this._city.kingdom;
			Kingdom kingdom = this._kingdom;
			this.updateColor((kingdom != null) ? kingdom.getColor() : null);
			this.generateName(this._kingdom);
		}
	}

	// Token: 0x06001314 RID: 4884 RVA: 0x000D63E2 File Offset: 0x000D45E2
	public void onKingdomNameChange()
	{
		this.generateName(null);
	}

	// Token: 0x06001315 RID: 4885 RVA: 0x000D63EC File Offset: 0x000D45EC
	protected override void generateColor()
	{
		if (!base.isAlive())
		{
			return;
		}
		Kingdom tKingdom = this.getKingdom();
		if (tKingdom.isRekt())
		{
			return;
		}
		this.data.setColorID(tKingdom.data.color_id);
	}

	// Token: 0x06001316 RID: 4886 RVA: 0x000D6428 File Offset: 0x000D4628
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
		ArmyData data = this.data;
		if (data.past_names == null)
		{
			data.past_names = new List<NameEntry>();
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

	// Token: 0x06001317 RID: 4887 RVA: 0x000D6534 File Offset: 0x000D4734
	private void generateName(Kingdom pKingdom = null)
	{
		if (this.data.custom_name && !string.IsNullOrEmpty(this.data.name))
		{
			return;
		}
		Kingdom tKingdom;
		if (pKingdom != null)
		{
			tKingdom = pKingdom;
		}
		else
		{
			tKingdom = this.getKingdom();
		}
		if (tKingdom == null)
		{
			base.setName("Forgotten Army", true);
			return;
		}
		string tKingdomName = tKingdom.name ?? "";
		string name = this.data.name;
		if (name != null && name.StartsWith(tKingdomName + " "))
		{
			return;
		}
		using (ListPool<string> tPoolArmies = new ListPool<string>(World.world.armies.Count))
		{
			foreach (Army tArmy in World.world.armies)
			{
				if (tArmy != this && tArmy.getKingdom() == tKingdom)
				{
					tPoolArmies.Add(tArmy.name);
				}
			}
			int tCountID = 1;
			string tNewArmyName;
			for (;;)
			{
				string tRoman = tCountID.ToRoman();
				tNewArmyName = tKingdomName + " " + tRoman;
				if (!tPoolArmies.Contains(tNewArmyName))
				{
					break;
				}
				tCountID++;
			}
			base.setName(tNewArmyName, true);
		}
	}

	// Token: 0x06001318 RID: 4888 RVA: 0x000D6674 File Offset: 0x000D4874
	public Actor getCaptain()
	{
		return this._captain;
	}

	// Token: 0x06001319 RID: 4889 RVA: 0x000D667C File Offset: 0x000D487C
	public override void save()
	{
		base.save();
		ArmyData data = this.data;
		City city = this._city;
		data.id_city = ((city != null) ? city.id : -1L);
		ArmyData data2 = this.data;
		City city2 = this._city;
		long? num;
		if (city2 == null)
		{
			num = null;
		}
		else
		{
			Kingdom kingdom = city2.kingdom;
			num = ((kingdom != null) ? new long?(kingdom.id) : null);
		}
		long? num2 = num;
		long id_kingdom;
		if (num2 == null)
		{
			Kingdom kingdom2 = this._kingdom;
			id_kingdom = ((kingdom2 != null) ? kingdom2.id : -1L);
		}
		else
		{
			id_kingdom = num2.GetValueOrDefault();
		}
		data2.id_kingdom = id_kingdom;
		this.data.id_captain = (this.hasCaptain() ? this._captain.data.id : -1L);
	}

	// Token: 0x0600131A RID: 4890 RVA: 0x000D6738 File Offset: 0x000D4938
	public override void loadData(ArmyData pData)
	{
		base.loadData(pData);
		this._city = World.world.cities.get(pData.id_city);
		if (this._city != null)
		{
			this._city.setArmy(this);
		}
		this._kingdom = World.world.kingdoms.get(pData.id_kingdom);
		if (string.IsNullOrEmpty(this.name))
		{
			this.generateName(null);
		}
	}

	// Token: 0x0600131B RID: 4891 RVA: 0x000D67AC File Offset: 0x000D49AC
	public void loadDataCaptains()
	{
		Actor tActor = World.world.units.get(this.data.id_captain);
		if (tActor != null && tActor.army == this)
		{
			this.setCaptain(tActor, true);
		}
		this.updateColor(this.getColor());
	}

	// Token: 0x0600131C RID: 4892 RVA: 0x000D67F5 File Offset: 0x000D49F5
	public override void generateBanner()
	{
	}

	// Token: 0x0600131D RID: 4893 RVA: 0x000D67F7 File Offset: 0x000D49F7
	protected override ColorLibrary getColorLibrary()
	{
		return AssetManager.kingdom_colors_library;
	}

	// Token: 0x0600131E RID: 4894 RVA: 0x000D67FE File Offset: 0x000D49FE
	public override ColorAsset getColor()
	{
		return this.getKingdom().getColor();
	}

	// Token: 0x0600131F RID: 4895 RVA: 0x000D680B File Offset: 0x000D4A0B
	public void clearCity()
	{
		this._city = null;
		this.data.id_city = -1L;
	}

	// Token: 0x06001320 RID: 4896 RVA: 0x000D6824 File Offset: 0x000D4A24
	public void disband()
	{
		for (int i = 0; i < base.units.Count; i++)
		{
			base.units[i].stopBeingWarrior();
		}
		this.setCaptain(null, false);
	}

	// Token: 0x06001321 RID: 4897 RVA: 0x000D6860 File Offset: 0x000D4A60
	public void updateCaptains()
	{
		if (this.data.past_captains == null)
		{
			return;
		}
		if (this.data.past_captains.Count == 0)
		{
			return;
		}
		foreach (LeaderEntry tEntry in this.data.past_captains)
		{
			Actor tRuler = World.world.units.get(tEntry.id);
			if (!tRuler.isRekt())
			{
				tEntry.name = tRuler.name;
			}
		}
	}

	// Token: 0x06001322 RID: 4898 RVA: 0x000D68FC File Offset: 0x000D4AFC
	public void addCaptain(Actor pActor)
	{
		ArmyData data = this.data;
		if (data.past_captains == null)
		{
			data.past_captains = new List<LeaderEntry>();
		}
		this.captainLeft();
		List<LeaderEntry> past_captains = this.data.past_captains;
		LeaderEntry leaderEntry = new LeaderEntry();
		leaderEntry.id = pActor.getID();
		leaderEntry.name = pActor.name;
		Kingdom kingdom = this.getKingdom();
		leaderEntry.color_id = ((kingdom != null) ? kingdom.data.color_id : this.data.color_id);
		leaderEntry.timestamp_ago = World.world.getCurWorldTime();
		past_captains.Add(leaderEntry);
		if (this.data.past_captains.Count > 30)
		{
			this.data.past_captains.Shift<LeaderEntry>();
		}
	}

	// Token: 0x06001323 RID: 4899 RVA: 0x000D69B4 File Offset: 0x000D4BB4
	public void captainLeft()
	{
		if (this.data.past_captains == null)
		{
			return;
		}
		if (this.data.past_captains.Count == 0)
		{
			return;
		}
		LeaderEntry tLast = this.data.past_captains.Last<LeaderEntry>();
		if (tLast.timestamp_end >= tLast.timestamp_ago)
		{
			return;
		}
		tLast.timestamp_end = World.world.getCurWorldTime();
		this.updateCaptains();
	}

	// Token: 0x06001324 RID: 4900 RVA: 0x000D6A18 File Offset: 0x000D4C18
	public void setCaptain(Actor pActor, bool pFromLoad = false)
	{
		this._captain = pActor;
		if (this.data == null)
		{
			return;
		}
		if (pActor.isRekt())
		{
			this.data.id_captain = -1L;
			if (!pFromLoad)
			{
				this.captainLeft();
				return;
			}
		}
		else
		{
			this.data.id_captain = pActor.getID();
			if (!pFromLoad)
			{
				this.addCaptain(pActor);
			}
		}
	}

	// Token: 0x06001325 RID: 4901 RVA: 0x000D6A70 File Offset: 0x000D4C70
	public void checkCaptainExistence()
	{
		Actor tCaptain = this.getCaptain();
		if (!tCaptain.isRekt() && tCaptain.current_tile != null)
		{
			this._prev_captain_position = tCaptain.current_tile;
		}
		if (tCaptain.isRekt())
		{
			this.setCaptain(null, false);
		}
		this.findCaptain();
	}

	// Token: 0x06001326 RID: 4902 RVA: 0x000D6AB6 File Offset: 0x000D4CB6
	public void checkCaptainRemoval(Actor pActor)
	{
		if (this._captain == pActor)
		{
			this.setCaptain(null, false);
		}
	}

	// Token: 0x06001327 RID: 4903 RVA: 0x000D6ACC File Offset: 0x000D4CCC
	public int countMelee()
	{
		int tResult = 0;
		for (int i = 0; i < base.units.Count; i++)
		{
			Actor tActor = base.units[i];
			if (tActor.isAlive())
			{
				if (!tActor.hasWeapon())
				{
					tResult++;
				}
				else if (tActor.getWeaponAsset().attack_type == WeaponType.Melee)
				{
					tResult++;
				}
			}
		}
		return tResult;
	}

	// Token: 0x06001328 RID: 4904 RVA: 0x000D6B28 File Offset: 0x000D4D28
	public int countRange()
	{
		int tResult = 0;
		for (int i = 0; i < base.units.Count; i++)
		{
			Actor tActor = base.units[i];
			if (tActor.isAlive() && tActor.hasWeapon() && tActor.getWeaponAsset().attack_type == WeaponType.Range)
			{
				tResult++;
			}
		}
		return tResult;
	}

	// Token: 0x06001329 RID: 4905 RVA: 0x000D6B80 File Offset: 0x000D4D80
	public bool isGroupInCityAndHaveLeader()
	{
		if (!base.isAlive())
		{
			return false;
		}
		if (base.units.Count == 0)
		{
			return true;
		}
		if (!this.hasCaptain())
		{
			return false;
		}
		Actor tCaptain = this.getCaptain();
		return !tCaptain.isInsideSomething() && tCaptain.current_zone.isSameCityHere(this._city);
	}

	// Token: 0x0600132A RID: 4906 RVA: 0x000D6BD8 File Offset: 0x000D4DD8
	private void findCaptain()
	{
		if (base.isLocked())
		{
			return;
		}
		if (this.hasCaptain())
		{
			if (this.getCaptain().isKingdomCiv())
			{
				return;
			}
			this.setCaptain(null, false);
		}
		if (base.units.Count == 0)
		{
			return;
		}
		Actor tBest;
		if (this._prev_captain_position == null)
		{
			tBest = this.getRandomActorForCaptain();
		}
		else
		{
			tBest = this.getNearbyUnitForCaptain(this._prev_captain_position);
		}
		if (tBest != null)
		{
			this.setCaptain(tBest, false);
		}
	}

	// Token: 0x0600132B RID: 4907 RVA: 0x000D6C48 File Offset: 0x000D4E48
	private Actor getRandomActorForCaptain()
	{
		foreach (Actor tActor in base.units.LoopRandom<Actor>())
		{
			if (!tActor.isRekt() && tActor.army == this)
			{
				return tActor;
			}
		}
		return null;
	}

	// Token: 0x0600132C RID: 4908 RVA: 0x000D6CAC File Offset: 0x000D4EAC
	private Actor getNearbyUnitForCaptain(WorldTile pLastPosition)
	{
		Actor tBest = null;
		int tBestFastDist = int.MaxValue;
		List<Actor> list = base.units;
		for (int i = 0; i < list.Count; i++)
		{
			Actor tActor = list[i];
			if (tActor.army == this && !tActor.isRekt())
			{
				int tFastDist = Toolbox.SquaredDistTile(tActor.current_tile, pLastPosition);
				if (tFastDist < tBestFastDist)
				{
					tBest = tActor;
					tBestFastDist = tFastDist;
				}
			}
		}
		return tBest;
	}

	// Token: 0x0600132D RID: 4909 RVA: 0x000D6D10 File Offset: 0x000D4F10
	public string getDebug()
	{
		string tResult = base.units.Count.ToString() ?? "";
		if (this._captain != null)
		{
			tResult = string.Concat(new string[]
			{
				tResult,
				" ",
				this._captain.getName(),
				"(",
				this._captain.getAge().ToString(),
				")"
			});
		}
		return tResult;
	}

	// Token: 0x0600132E RID: 4910 RVA: 0x000D6D90 File Offset: 0x000D4F90
	[CanBeNull]
	public Kingdom getKingdom()
	{
		Kingdom tKingdom = null;
		if (this.hasCaptain())
		{
			tKingdom = this.getCaptain().kingdom;
		}
		if (tKingdom == null)
		{
			if (!this._city.isRekt())
			{
				tKingdom = this._city.kingdom;
			}
			else
			{
				tKingdom = this._kingdom;
			}
		}
		return tKingdom;
	}

	// Token: 0x0600132F RID: 4911 RVA: 0x000D6DD9 File Offset: 0x000D4FD9
	public bool hasKingdom()
	{
		return !this._kingdom.isRekt();
	}

	// Token: 0x06001330 RID: 4912 RVA: 0x000D6DE9 File Offset: 0x000D4FE9
	public bool hasCaptain()
	{
		return !this._captain.isRekt();
	}

	// Token: 0x06001331 RID: 4913 RVA: 0x000D6DF9 File Offset: 0x000D4FF9
	public City getCity()
	{
		return this._city;
	}

	// Token: 0x06001332 RID: 4914 RVA: 0x000D6E01 File Offset: 0x000D5001
	public bool hasCity()
	{
		return !this._city.isRekt();
	}

	// Token: 0x06001333 RID: 4915 RVA: 0x000D6E11 File Offset: 0x000D5011
	public override bool isReadyForRemoval()
	{
		return base.units.Count <= 0 && !this.hasCaptain() && !this.hasCity() && base.isReadyForRemoval();
	}

	// Token: 0x06001334 RID: 4916 RVA: 0x000D6E42 File Offset: 0x000D5042
	public override void Dispose()
	{
		base.Dispose();
		base.units.Clear();
		this._captain = null;
		this._prev_captain_position = null;
		this._city = null;
		this._kingdom = null;
	}

	// Token: 0x06001335 RID: 4917 RVA: 0x000D6E74 File Offset: 0x000D5074
	public override string ToString()
	{
		if (this.data == null)
		{
			return "[Army is null]";
		}
		string result;
		using (StringBuilderPool tBuilder = new StringBuilderPool())
		{
			tBuilder.Append(string.Format("[Army:{0} ", base.id));
			if (!base.isAlive())
			{
				tBuilder.Append("[DEAD] ");
			}
			tBuilder.Append("\"" + this.name + "\" ");
			Kingdom tKingdom = this.getKingdom();
			tBuilder.Append(string.Format("Kingdom:{0} ", (tKingdom != null) ? tKingdom.id : -1L));
			if (this.hasCity())
			{
				tBuilder.Append(string.Format("{0} ", this._city));
			}
			if (tKingdom != this._kingdom)
			{
				tBuilder.Append(string.Format("_kingdom:{0} ", this._kingdom));
			}
			if (this.hasCaptain())
			{
				StringBuilderPool stringBuilderPool = tBuilder;
				string format = "Captain:{0} ";
				Actor captain = this._captain;
				stringBuilderPool.Append(string.Format(format, (captain != null) ? captain.id : -1L));
				Actor captain2 = this._captain;
				if (((captain2 != null) ? captain2.kingdom : null) != tKingdom)
				{
					StringBuilderPool stringBuilderPool2 = tBuilder;
					string format2 = "CaptainKingdom:{0} ";
					Actor captain3 = this._captain;
					long? num;
					if (captain3 == null)
					{
						num = null;
					}
					else
					{
						Kingdom kingdom = captain3.kingdom;
						num = ((kingdom != null) ? new long?(kingdom.id) : null);
					}
					stringBuilderPool2.Append(string.Format(format2, num ?? -1L));
				}
			}
			tBuilder.Append(string.Format("Units:{0} ", base.units.Count));
			if (this.manager.isUnitsDirty())
			{
				tBuilder.Append("[Dirty] ");
			}
			result = tBuilder.ToString().Trim() + "]";
		}
		return result;
	}

	// Token: 0x0400115C RID: 4444
	private Actor _captain;

	// Token: 0x0400115D RID: 4445
	private WorldTile _prev_captain_position;

	// Token: 0x0400115E RID: 4446
	private City _city;

	// Token: 0x0400115F RID: 4447
	private Kingdom _kingdom;
}
