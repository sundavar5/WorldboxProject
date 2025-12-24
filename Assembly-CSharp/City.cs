using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using db;
using JetBrains.Annotations;
using UnityEngine;

// Token: 0x02000319 RID: 793
public class City : MetaObject<CityData>
{
	// Token: 0x170001CB RID: 459
	// (get) Token: 0x06001DAC RID: 7596 RVA: 0x00108212 File Offset: 0x00106412
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.City;
		}
	}

	// Token: 0x170001CC RID: 460
	// (get) Token: 0x06001DAD RID: 7597 RVA: 0x00108215 File Offset: 0x00106415
	public override BaseSystemManager manager
	{
		get
		{
			return World.world.cities;
		}
	}

	// Token: 0x170001CD RID: 461
	// (get) Token: 0x06001DAE RID: 7598 RVA: 0x00108221 File Offset: 0x00106421
	protected override bool track_death_types
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06001DAF RID: 7599 RVA: 0x00108224 File Offset: 0x00106424
	public int getStorageVersion()
	{
		return this._storage_version;
	}

	// Token: 0x06001DB0 RID: 7600 RVA: 0x0010822C File Offset: 0x0010642C
	public override void increaseBirths()
	{
		base.increaseBirths();
		base.addRenown(1);
	}

	// Token: 0x06001DB1 RID: 7601 RVA: 0x0010823C File Offset: 0x0010643C
	public void increaseLeft()
	{
		if (!base.isAlive())
		{
			return;
		}
		CityData data = this.data;
		long left = data.left;
		data.left = left + 1L;
	}

	// Token: 0x06001DB2 RID: 7602 RVA: 0x00108268 File Offset: 0x00106468
	public void increaseJoined()
	{
		if (!base.isAlive())
		{
			return;
		}
		CityData data = this.data;
		long joined = data.joined;
		data.joined = joined + 1L;
		base.addRenown(1);
	}

	// Token: 0x06001DB3 RID: 7603 RVA: 0x0010829C File Offset: 0x0010649C
	public void increaseMoved()
	{
		if (!base.isAlive())
		{
			return;
		}
		CityData data = this.data;
		long moved = data.moved;
		data.moved = moved + 1L;
		base.addRenown(2);
	}

	// Token: 0x06001DB4 RID: 7604 RVA: 0x001082D0 File Offset: 0x001064D0
	public void increaseMigrants()
	{
		if (!base.isAlive())
		{
			return;
		}
		CityData data = this.data;
		long migrated = data.migrated;
		data.migrated = migrated + 1L;
	}

	// Token: 0x06001DB5 RID: 7605 RVA: 0x001082FC File Offset: 0x001064FC
	public long getTotalLeft()
	{
		return this.data.left;
	}

	// Token: 0x06001DB6 RID: 7606 RVA: 0x00108309 File Offset: 0x00106509
	public long getTotalJoined()
	{
		return this.data.joined;
	}

	// Token: 0x06001DB7 RID: 7607 RVA: 0x00108316 File Offset: 0x00106516
	public long getTotalMoved()
	{
		return this.data.moved;
	}

	// Token: 0x06001DB8 RID: 7608 RVA: 0x00108323 File Offset: 0x00106523
	public long getTotalMigrated()
	{
		return this.data.migrated;
	}

	// Token: 0x06001DB9 RID: 7609 RVA: 0x00108330 File Offset: 0x00106530
	public bool isZoneToClaimStillGood(Actor pActor, TileZone pZone, WorldTile pCityTile)
	{
		if (!pZone.canBeClaimedByCity(this))
		{
			return false;
		}
		if (!pZone.checkCanSettleInThisBiomes(pActor.subspecies))
		{
			return false;
		}
		foreach (TileZone tZone in pZone.neighbours)
		{
			if (tZone.hasCity() && tZone.city == this)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001DBA RID: 7610 RVA: 0x00108385 File Offset: 0x00106585
	internal override void clearListUnits()
	{
		base.clearListUnits();
		this._boats.Clear();
		this._species.Clear();
	}

	// Token: 0x06001DBB RID: 7611 RVA: 0x001083A3 File Offset: 0x001065A3
	public override ActorAsset getActorAsset()
	{
		if (this.hasLeader())
		{
			return this.leader.getActorAsset();
		}
		return this.getFounderSpecies();
	}

	// Token: 0x06001DBC RID: 7612 RVA: 0x001083BF File Offset: 0x001065BF
	public ActorAsset getFounderSpecies()
	{
		return AssetManager.actor_library.get(this.data.original_actor_asset);
	}

	// Token: 0x06001DBD RID: 7613 RVA: 0x001083D8 File Offset: 0x001065D8
	public CityLayoutTilePlacement getTilePlacementFromZone()
	{
		if (this.hasCulture())
		{
			if (this.culture.hasTrait("city_layout_the_grand_arrangement"))
			{
				return CityLayoutTilePlacement.CenterTile;
			}
			if (this.culture.hasTrait("city_layout_tile_wobbly_pattern"))
			{
				return CityLayoutTilePlacement.CenterTileDrunk;
			}
			if (this.culture.hasTrait("city_layout_tile_moonsteps"))
			{
				return CityLayoutTilePlacement.Moonsteps;
			}
		}
		return CityLayoutTilePlacement.Random;
	}

	// Token: 0x06001DBE RID: 7614 RVA: 0x0010842A File Offset: 0x0010662A
	public string getSpecies()
	{
		return this.getActorAsset().id;
	}

	// Token: 0x06001DBF RID: 7615 RVA: 0x00108437 File Offset: 0x00106637
	public override bool isReadyForRemoval()
	{
		return this.zones.Count == 0;
	}

	// Token: 0x06001DC0 RID: 7616 RVA: 0x0010844C File Offset: 0x0010664C
	public void clearBuildingList()
	{
		this.buildings.Clear();
		foreach (List<Building> list in this.buildings_dict_type.Values)
		{
			list.Clear();
		}
		foreach (List<Building> list2 in this.buildings_dict_id.Values)
		{
			list2.Clear();
		}
		this.stockpiles.Clear();
		this.storages.Clear();
		this._cached_book_ids.Clear();
		this._cached_buildings_with_book_slots.Clear();
	}

	// Token: 0x06001DC1 RID: 7617 RVA: 0x0010851C File Offset: 0x0010671C
	public override void listUnit(Actor pActor)
	{
		if (pActor.asset.is_boat)
		{
			this._boats.Add(pActor);
			return;
		}
		base.units.Add(pActor);
		if (pActor.hasSubspecies())
		{
			this._species[pActor.asset.id] = pActor.subspecies.id;
		}
	}

	// Token: 0x06001DC2 RID: 7618 RVA: 0x00108578 File Offset: 0x00106778
	public Subspecies getSubspecies(string pSpeciesId)
	{
		long tSubspeciesId = this.getSubspeciesId(pSpeciesId);
		return World.world.subspecies.get(tSubspeciesId);
	}

	// Token: 0x06001DC3 RID: 7619 RVA: 0x001085A0 File Offset: 0x001067A0
	public long getSubspeciesId(string pSpeciesId)
	{
		long tSubspeciesId;
		if (this._species.TryGetValue(pSpeciesId, out tSubspeciesId))
		{
			return tSubspeciesId;
		}
		return -1L;
	}

	// Token: 0x06001DC4 RID: 7620 RVA: 0x001085C1 File Offset: 0x001067C1
	public bool hasFreeHouseSlots()
	{
		return this.status.housing_free != 0;
	}

	// Token: 0x06001DC5 RID: 7621 RVA: 0x001085D3 File Offset: 0x001067D3
	public bool hasReachedWorldLawLimit()
	{
		return WorldLawLibrary.world_law_civ_limit_population_100.isEnabled() && this.getPopulationPeople() >= 100;
	}

	// Token: 0x06001DC6 RID: 7622 RVA: 0x001085F0 File Offset: 0x001067F0
	public void listBuilding(Building pBuilding)
	{
		this.buildings.Add(pBuilding);
		BuildingAsset asset = pBuilding.asset;
		if (asset.type == "type_stockpile")
		{
			this.stockpiles.Add(pBuilding);
		}
		if (asset.storage)
		{
			this.storages.Add(pBuilding);
		}
		if (asset.book_slots > 0)
		{
			this._cached_buildings_with_book_slots.Add(pBuilding);
			if (pBuilding.data.books != null)
			{
				this._cached_book_ids.AddRange(pBuilding.data.books.list_books);
			}
		}
		this.setBuildingDictType(pBuilding);
		this.setBuildingDictID(pBuilding);
	}

	// Token: 0x06001DC7 RID: 7623 RVA: 0x0010868B File Offset: 0x0010688B
	[CanBeNull]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public WorldTile getTile(bool pForceRecalc = false)
	{
		if (this._city_tile == null || pForceRecalc)
		{
			this.recalculateCityTile();
		}
		return this._city_tile;
	}

	// Token: 0x06001DC8 RID: 7624 RVA: 0x001086A8 File Offset: 0x001068A8
	internal void recalculateCityTile()
	{
		this._city_tile = null;
		Building tMainBuilding = this.getBuildingOfType("type_bonfire", true, false, false, null);
		if (tMainBuilding != null)
		{
			this._city_tile = tMainBuilding.current_tile;
			return;
		}
		foreach (Building tBuilding in this.buildings.LoopRandom<Building>())
		{
			if (!tBuilding.asset.docks && !tBuilding.current_tile.Type.ocean)
			{
				if (tMainBuilding == null)
				{
					tMainBuilding = tBuilding;
				}
				else if (tBuilding.asset.priority > tMainBuilding.asset.priority)
				{
					tMainBuilding = tBuilding;
				}
			}
		}
		if (tMainBuilding != null)
		{
			this._city_tile = tMainBuilding.current_tile;
			return;
		}
		List<TileZone> tZones = this.zones;
		if (tZones.Count != 0)
		{
			for (int i = 0; i < tZones.Count; i++)
			{
				TileZone tZone = tZones[i];
				if (!tZone.centerTile.Type.ocean)
				{
					this._city_tile = tZone.centerTile;
					return;
				}
			}
		}
	}

	// Token: 0x06001DC9 RID: 7625 RVA: 0x001087BC File Offset: 0x001069BC
	internal int countInHouses()
	{
		int ii = 0;
		List<Actor> tUnits = base.units;
		for (int i = 0; i < tUnits.Count; i++)
		{
			if (tUnits[i].is_inside_building)
			{
				ii++;
			}
		}
		return ii;
	}

	// Token: 0x06001DCA RID: 7626 RVA: 0x001087F8 File Offset: 0x001069F8
	public int countBookSlots()
	{
		int tSlotsTotal = 0;
		for (int i = 0; i < this._cached_buildings_with_book_slots.Count; i++)
		{
			Building tBuilding = this._cached_buildings_with_book_slots[i];
			tSlotsTotal += tBuilding.asset.book_slots;
		}
		return tSlotsTotal;
	}

	// Token: 0x06001DCB RID: 7627 RVA: 0x0010883C File Offset: 0x00106A3C
	public bool hasBookSlots()
	{
		int tSlots = this.countBookSlots();
		return this.countBooks() < tSlots;
	}

	// Token: 0x06001DCC RID: 7628 RVA: 0x0010885C File Offset: 0x00106A5C
	public Building getBuildingWithBookSlot()
	{
		foreach (Building tBuilding in this._cached_buildings_with_book_slots)
		{
			if (tBuilding.hasFreeBookSlot())
			{
				return tBuilding;
			}
		}
		return null;
	}

	// Token: 0x06001DCD RID: 7629 RVA: 0x001088B8 File Offset: 0x00106AB8
	public int countBooks()
	{
		return this._cached_book_ids.Count;
	}

	// Token: 0x06001DCE RID: 7630 RVA: 0x001088C5 File Offset: 0x00106AC5
	private void setKingdomTimestamp()
	{
		this.data.timestamp_kingdom = World.world.getCurWorldTime();
	}

	// Token: 0x06001DCF RID: 7631 RVA: 0x001088DC File Offset: 0x00106ADC
	public override ColorAsset getColor()
	{
		return this.kingdom.getColor();
	}

	// Token: 0x06001DD0 RID: 7632 RVA: 0x001088EC File Offset: 0x00106AEC
	internal void setKingdom(Kingdom pKingdom, bool pFromLoad = false)
	{
		World.world.kingdoms.setDirtyCities();
		if (this.isCapitalCity())
		{
			this.kingdom.clearCapital();
		}
		this.kingdom = pKingdom;
		if (this.kingdom != null && this.kingdom != WildKingdomsManager.neutral)
		{
			this.data.last_kingdom_id = this.kingdom.id;
		}
		if (!pFromLoad)
		{
			this.checkArmyExistence();
			if (this.hasArmy())
			{
				this.army.checkCity();
			}
		}
	}

	// Token: 0x06001DD1 RID: 7633 RVA: 0x00108969 File Offset: 0x00106B69
	internal void newForceKingdomEvent(List<Actor> pUnits, List<Actor> pBoats, Kingdom pKingdom, string pHappinessEvent)
	{
		this.setKingdomTimestamp();
		this.forceUnitsIntoThisKingdom(pUnits, pKingdom, false, pHappinessEvent);
		this.forceUnitsIntoThisKingdom(pBoats, pKingdom, true, null);
	}

	// Token: 0x06001DD2 RID: 7634 RVA: 0x00108988 File Offset: 0x00106B88
	internal void forceBuildingsToKingdom(List<Building> pBuildings, Kingdom pKingdom)
	{
		for (int i = 0; i < pBuildings.Count; i++)
		{
			pBuildings[i].setKingdom(pKingdom);
		}
	}

	// Token: 0x06001DD3 RID: 7635 RVA: 0x001089B4 File Offset: 0x00106BB4
	internal void forceUnitsIntoThisKingdom(List<Actor> pActors, Kingdom pKingdom, bool pBoats, string pHappinessEvent = null)
	{
		if (pBoats)
		{
			for (int i = 0; i < pActors.Count; i++)
			{
				Actor tActor = pActors[i];
				if (!tActor.isRekt())
				{
					tActor.joinKingdom(pKingdom);
				}
			}
			return;
		}
		for (int j = 0; j < pActors.Count; j++)
		{
			Actor tActor2 = pActors[j];
			if (!tActor2.isRekt())
			{
				if (tActor2.isKing())
				{
					if (tActor2.city != this || tActor2.kingdom == pKingdom)
					{
						goto IL_7A;
					}
					tActor2.kingdom.kingLeftEvent();
				}
				tActor2.joinKingdom(pKingdom);
				if (pHappinessEvent != null)
				{
					tActor2.changeHappiness(pHappinessEvent, 0);
				}
			}
			IL_7A:;
		}
	}

	// Token: 0x06001DD4 RID: 7636 RVA: 0x00108A48 File Offset: 0x00106C48
	internal Building getStorageNear(WorldTile pTile, bool pOnlyFood = false)
	{
		Building tBest = null;
		int tDistBest = int.MaxValue;
		List<Building> tBuildings = this.storages;
		for (int i = 0; i < tBuildings.Count; i++)
		{
			Building tBuilding = tBuildings[i];
			if (tBuilding.isUsable() && tBuilding.current_tile.isSameIsland(pTile))
			{
				if (pOnlyFood && tBuilding.asset.storage_only_food)
				{
					tBest = tBuilding;
				}
				else
				{
					int tDist = Toolbox.SquaredDistVec2(tBuilding.current_tile.pos, pTile.pos);
					if (tDist < tDistBest)
					{
						tDistBest = tDist;
						tBest = tBuilding;
					}
				}
			}
		}
		return tBest;
	}

	// Token: 0x06001DD5 RID: 7637 RVA: 0x00108AD4 File Offset: 0x00106CD4
	internal Building getStorageWithFoodNear(WorldTile pTile)
	{
		Building tBest = null;
		int tDistBest = int.MaxValue;
		List<Building> tBuildings = this.storages;
		for (int i = 0; i < tBuildings.Count; i++)
		{
			Building tBuilding = tBuildings[i];
			if (tBuilding.isUsable() && tBuilding.current_tile.isSameIsland(pTile) && tBuilding.countFood() != 0)
			{
				int tDist = Toolbox.SquaredDistVec2(tBuilding.current_tile.pos, pTile.pos);
				if (tDist < tDistBest)
				{
					tDistBest = tDist;
					tBest = tBuilding;
				}
			}
		}
		return tBest;
	}

	// Token: 0x06001DD6 RID: 7638 RVA: 0x00108B50 File Offset: 0x00106D50
	internal bool hasStorageBuilding()
	{
		List<Building> tBuildings = this.storages;
		for (int i = 0; i < tBuildings.Count; i++)
		{
			if (!tBuildings[i].isUnderConstruction())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001DD7 RID: 7639 RVA: 0x00108B88 File Offset: 0x00106D88
	public WorldTile getRoadTileToBuild(Actor pBuilder)
	{
		this.tiles_to_remove.Clear();
		for (int i = 0; i < this.road_tiles_to_build.Count; i++)
		{
			WorldTile tTile = this.road_tiles_to_build[i];
			if (tTile.Type.road)
			{
				this.tiles_to_remove.Add(tTile);
			}
		}
		for (int j = 0; j < this.tiles_to_remove.Count; j++)
		{
			WorldTile tTile2 = this.tiles_to_remove[j];
			this.road_tiles_to_build.Remove(tTile2);
		}
		this.tiles_to_remove.Clear();
		if (this.road_tiles_to_build.Count > 0)
		{
			return this.road_tiles_to_build[0];
		}
		return null;
	}

	// Token: 0x06001DD8 RID: 7640 RVA: 0x00108C33 File Offset: 0x00106E33
	internal void init()
	{
		this.createAI();
		this.setStatusDirty();
	}

	// Token: 0x06001DD9 RID: 7641 RVA: 0x00108C44 File Offset: 0x00106E44
	private void createAI()
	{
		if (!Globals.AI_TEST_ACTIVE)
		{
			return;
		}
		if (this.ai == null)
		{
			this.ai = new AiSystemCity(this);
		}
		this.ai.next_job_delegate = new GetNextJobID(this.getNextJob);
		this.ai.jobs_library = AssetManager.job_city;
		this.ai.task_library = AssetManager.tasks_city;
		this.ai.addSingleTask("build");
		this.ai.addSingleTask("check_loyalty");
		this.ai.addSingleTask("check_destruction");
	}

	// Token: 0x06001DDA RID: 7642 RVA: 0x00108CD4 File Offset: 0x00106ED4
	protected sealed override void setDefaultValues()
	{
		base.setDefaultValues();
		this.mark_scale_effect = 1f;
		this.timer_build_boat = 10f;
		this.timer_build = 0f;
		this.timer_action = 0f;
		this._timer_capture = 0f;
		this._timer_warrior = 0f;
		this._capture_ticks = 0f;
		this.last_visual_capture_ticks = 0;
		this._dirty_citizens = true;
		this._dirty_city_status = false;
		this._dirty_abandoned_zones = false;
		this._current_total_food = 0;
		this._last_checked_job_id = 0;
		this._loyalty_last_time = -1.0;
		this._loyalty_cached = -1;
	}

	// Token: 0x06001DDB RID: 7643 RVA: 0x00108D74 File Offset: 0x00106F74
	private string getNextJob()
	{
		return "city";
	}

	// Token: 0x06001DDC RID: 7644 RVA: 0x00108D7B File Offset: 0x00106F7B
	public bool isValidTargetForWar()
	{
		return this.hasZones();
	}

	// Token: 0x06001DDD RID: 7645 RVA: 0x00108D88 File Offset: 0x00106F88
	public bool hasZones()
	{
		return this.zones.Count > 0;
	}

	// Token: 0x06001DDE RID: 7646 RVA: 0x00108D98 File Offset: 0x00106F98
	public bool needSettlers()
	{
		int tCurrentPop = this.getPopulationPeople();
		return this.getAge() < 5 || (tCurrentPop < 22 && (tCurrentPop >= 22 || this.status.housing_free != 0 || this.getAge() <= 10 || this.getHouseCurrent() <= 2));
	}

	// Token: 0x06001DDF RID: 7647 RVA: 0x00108DE8 File Offset: 0x00106FE8
	internal void generateName(Actor pActor)
	{
		string tName = pActor.generateName(MetaType.City, this.getID(), ActorSex.None);
		base.setName(tName, true);
		BaseSystemData data = this.data;
		Culture culture = this.culture;
		data.name_culture_id = ((culture != null) ? culture.id : -1L);
	}

	// Token: 0x06001DE0 RID: 7648 RVA: 0x00108E2C File Offset: 0x0010702C
	public void loadLeader()
	{
		if (this.data.leaderID.hasValue())
		{
			Actor tActor = World.world.units.get(this.data.leaderID);
			this.setLeader(tActor, false);
		}
	}

	// Token: 0x06001DE1 RID: 7649 RVA: 0x00108E6E File Offset: 0x0010706E
	public void newCityEvent(Actor pActor)
	{
		this.recalculateCityTile();
		this.generateName(pActor);
	}

	// Token: 0x06001DE2 RID: 7650 RVA: 0x00108E80 File Offset: 0x00107080
	private void loadCityZones(List<ZoneData> pZoneData)
	{
		if (pZoneData == null)
		{
			return;
		}
		for (int i = 0; i < pZoneData.Count; i++)
		{
			ZoneData tZoneData = pZoneData[i];
			TileZone tZone = World.world.zone_calculator.getZone(tZoneData.x, tZoneData.y);
			if (tZone != null)
			{
				this.addZone(tZone);
			}
		}
	}

	// Token: 0x06001DE3 RID: 7651 RVA: 0x00108ED0 File Offset: 0x001070D0
	public void loadCity(CityData pData)
	{
		this.loadCityZones(pData.zones);
		this.setData(pData);
		if (this.data.id_culture.hasValue())
		{
			this.setCulture(World.world.cultures.get(this.data.id_culture));
		}
		if (this.data.id_language.hasValue())
		{
			this.setLanguage(World.world.languages.get(this.data.id_language));
		}
		if (this.data.id_religion.hasValue())
		{
			this.setReligion(World.world.religions.get(this.data.id_religion));
		}
		if (this.data.equipment == null)
		{
			this.data.equipment = new CityEquipment();
		}
		else
		{
			this.data.equipment.loadFromSave(this);
		}
		Kingdom tKingdom;
		if (pData.kingdomID.hasValue() && pData.kingdomID != 0L)
		{
			tKingdom = World.world.kingdoms.get(pData.kingdomID);
		}
		else
		{
			tKingdom = WildKingdomsManager.neutral;
		}
		this.setKingdom(tKingdom, true);
	}

	// Token: 0x06001DE4 RID: 7652 RVA: 0x00108FF1 File Offset: 0x001071F1
	public void forceDoChecks()
	{
		this.updateTotalFood();
		this.updateCitizens();
		this.updateCityStatus();
	}

	// Token: 0x06001DE5 RID: 7653 RVA: 0x00109005 File Offset: 0x00107205
	public void executeAllActionsForCity()
	{
		AssetManager.tasks_city.get("do_initial_load_check").executeAllActionsForCity(this);
	}

	// Token: 0x06001DE6 RID: 7654 RVA: 0x0010901C File Offset: 0x0010721C
	public void eventUnitAdded(Actor pActor)
	{
		if (!pActor.asset.is_boat)
		{
			this.setCitizensDirty();
		}
		this.setStatusDirty();
	}

	// Token: 0x06001DE7 RID: 7655 RVA: 0x00109037 File Offset: 0x00107237
	public void eventUnitRemoved(Actor pActor)
	{
		this.setStatusDirty();
		this.setCitizensDirty();
		if (pActor.isCityLeader())
		{
			this.removeLeader();
		}
	}

	// Token: 0x06001DE8 RID: 7656 RVA: 0x00109053 File Offset: 0x00107253
	public void setAbandonedZonesDirty()
	{
		this._dirty_abandoned_zones = true;
	}

	// Token: 0x06001DE9 RID: 7657 RVA: 0x0010905C File Offset: 0x0010725C
	public void setCitizensDirty()
	{
		this._dirty_citizens = true;
	}

	// Token: 0x06001DEA RID: 7658 RVA: 0x00109065 File Offset: 0x00107265
	public void setStatusDirty()
	{
		this._dirty_city_status = true;
	}

	// Token: 0x06001DEB RID: 7659 RVA: 0x00109070 File Offset: 0x00107270
	private void sortZonesByDistanceToCenter()
	{
		WorldTile tCenter = this.getTile(false);
		if (tCenter == null)
		{
			return;
		}
		Vector2Int tCenterPos = tCenter.pos;
		this.zones.Sort(delegate(TileZone a, TileZone b)
		{
			int tDistA = Toolbox.SquaredDistVec2(a.centerTile.pos, tCenterPos);
			int tDistB = Toolbox.SquaredDistVec2(b.centerTile.pos, tCenterPos);
			return tDistA.CompareTo(tDistB);
		});
	}

	// Token: 0x06001DEC RID: 7660 RVA: 0x001090B4 File Offset: 0x001072B4
	private void updateCityStatus()
	{
		this._dirty_city_status = false;
		this.status.clear();
		this.recalculateCityTile();
		this.sortZonesByDistanceToCenter();
		this.recalculateNeighbourZones();
		this.recalculateNeighbourCities();
		List<Building> tBuildings = this.buildings;
		int tPopBabies = this.countPopulationChildren();
		this.status.population = this.getPopulationPeople();
		this.status.population_adults = this.status.population - tPopBabies;
		this.status.population_children = tPopBabies;
		MetaObject<CityData>._family_counter.Clear();
		List<Actor> tUnits = base.units;
		for (int i = 0; i < tUnits.Count; i++)
		{
			Actor tActor = tUnits[i];
			if (tActor.isHungry())
			{
				this.status.hungry++;
			}
			if (tActor.isSexMale())
			{
				this.status.males++;
			}
			else
			{
				this.status.females++;
			}
			if (tActor.hasFamily())
			{
				MetaObject<CityData>._family_counter.Add(tActor.family);
			}
			if (tActor.isSick())
			{
				this.status.sick++;
			}
			if (tActor.hasHouse())
			{
				this.status.housed++;
			}
			else
			{
				this.status.homeless++;
			}
		}
		this.status.families = MetaObject<CityData>._family_counter.Count;
		MetaObject<CityData>._family_counter.Clear();
		for (int j = 0; j < tBuildings.Count; j++)
		{
			Building tBuilding = tBuildings[j];
			if (!tBuilding.isUnderConstruction() && tBuilding.asset.hasHousingSlots())
			{
				this.status.housing_total += tBuilding.asset.housing_slots;
			}
		}
		if (this.status.population > this.status.housing_total)
		{
			this.status.housing_occupied = this.status.housing_total;
		}
		else
		{
			this.status.housing_occupied = this.status.population;
		}
		this.status.housing_free = this.status.housing_total - this.status.housing_occupied;
		this.status.maximum_items = 15;
		this.recalculateMaxHouses();
		this.status.warrior_slots = this.jobs.countCurrentJobs(CitizenJobLibrary.attacker);
		this.status.warriors_current = this.countProfession(UnitProfession.Warrior);
		CityBehCheckFarms.check(this);
	}

	// Token: 0x06001DED RID: 7661 RVA: 0x00109338 File Offset: 0x00107538
	private void recalculateMaxHouses()
	{
		if (DebugConfig.isOn(DebugOption.CityUnlimitedHouses))
		{
			this.status.houses_max = 9999;
			return;
		}
		float tHouseMax = (float)this.zones.Count;
		if (this.hasCulture())
		{
			if (this.culture.hasTrait("dense_dwellings"))
			{
				tHouseMax = (float)(this.zones.Count * 2);
			}
			if (this.culture.hasTrait("solitude_seekers"))
			{
				tHouseMax = (float)this.zones.Count / 3f;
			}
			if (this.culture.hasTrait("hive_society"))
			{
				tHouseMax = (float)this.zones.Count * 3f;
			}
		}
		foreach (Building tBuilding in this.buildings)
		{
			tHouseMax += (float)tBuilding.asset.max_houses;
		}
		this.status.houses_max = (int)tHouseMax;
	}

	// Token: 0x06001DEE RID: 7662 RVA: 0x00109440 File Offset: 0x00107640
	public bool hasBooksToRead(Actor pActor)
	{
		if (pActor.hasTag("can_read_any_book"))
		{
			return this.countBooks() > 0;
		}
		return pActor.hasLanguage() && this.hasBooksOfLanguage(pActor.language);
	}

	// Token: 0x06001DEF RID: 7663 RVA: 0x00109474 File Offset: 0x00107674
	public bool hasBooksOfLanguage(Language pLanguage)
	{
		int i = 0;
		int tLength = this.countBooks();
		while (i < tLength)
		{
			long tBookID = this._cached_book_ids[i];
			Book tBook = World.world.books.get(tBookID);
			if (!tBook.isRekt() && tBook.isReadyToBeRead())
			{
				Language tBookLanguage = tBook.getLanguage();
				if (tBookLanguage.id == pLanguage.id || tBookLanguage.hasTrait("magic_words"))
				{
					return true;
				}
			}
			i++;
		}
		return false;
	}

	// Token: 0x06001DF0 RID: 7664 RVA: 0x001094EC File Offset: 0x001076EC
	public Book getRandomBookOfLanguage(Language pLanguage)
	{
		Book result;
		using (ListPool<Book> tBooks = new ListPool<Book>())
		{
			int i = 0;
			int tLength = this.countBooks();
			while (i < tLength)
			{
				long tBookID = this._cached_book_ids[i];
				Book tBook = World.world.books.get(tBookID);
				if (!tBook.isRekt() && tBook.isReadyToBeRead())
				{
					Language tBookLanguage = tBook.getLanguage();
					if (tBookLanguage.id == pLanguage.id || tBookLanguage.hasTrait("magic_words"))
					{
						tBooks.Add(tBook);
					}
				}
				i++;
			}
			if (tBooks.Count == 0)
			{
				result = null;
			}
			else
			{
				result = tBooks.GetRandom<Book>();
			}
		}
		return result;
	}

	// Token: 0x06001DF1 RID: 7665 RVA: 0x001095A4 File Offset: 0x001077A4
	public Book getRandomBook()
	{
		Book result;
		using (ListPool<Book> tBooks = new ListPool<Book>())
		{
			int i = 0;
			int tLength = this.countBooks();
			while (i < tLength)
			{
				long tBookID = this._cached_book_ids[i];
				Book tBook = World.world.books.get(tBookID);
				if (!tBook.isRekt() && tBook.isReadyToBeRead())
				{
					tBooks.Add(tBook);
				}
				i++;
			}
			if (tBooks.Count == 0)
			{
				result = null;
			}
			else
			{
				result = tBooks.GetRandom<Book>();
			}
		}
		return result;
	}

	// Token: 0x06001DF2 RID: 7666 RVA: 0x00109638 File Offset: 0x00107838
	public List<long> getBooks()
	{
		return this._cached_book_ids;
	}

	// Token: 0x06001DF3 RID: 7667 RVA: 0x00109640 File Offset: 0x00107840
	public int getHouseCurrent()
	{
		return this.countBuildingsType("type_house", false);
	}

	// Token: 0x06001DF4 RID: 7668 RVA: 0x0010964E File Offset: 0x0010784E
	public int getHouseLimit()
	{
		return this.status.houses_max;
	}

	// Token: 0x06001DF5 RID: 7669 RVA: 0x0010965C File Offset: 0x0010785C
	public bool isConnectedToCapital()
	{
		if (!this.kingdom.hasCapital())
		{
			return false;
		}
		this.recalculateNeighbourCities();
		if (this.neighbours_cities_kingdom.Contains(this))
		{
			return true;
		}
		this.kingdom.calculateNeighbourCities();
		City._connected_checked.Clear();
		City._connected_next_wave.Clear();
		City._connected_current_wave.Clear();
		City._connected_next_wave.UnionWith(this.kingdom.capital.neighbours_cities_kingdom);
		int iii = 0;
		while (City._connected_next_wave.Count > 0)
		{
			City._connected_current_wave.UnionWith(City._connected_next_wave);
			City._connected_next_wave.Clear();
			iii++;
			foreach (City tCity in City._connected_current_wave)
			{
				if (tCity == this)
				{
					return true;
				}
				City._connected_checked.Add(tCity);
				foreach (City iCity in tCity.neighbours_cities_kingdom)
				{
					if (!City._connected_checked.Contains(iCity))
					{
						City._connected_next_wave.Add(iCity);
					}
				}
			}
			if (iii <= 30)
			{
				continue;
			}
			break;
		}
		return false;
	}

	// Token: 0x06001DF6 RID: 7670 RVA: 0x001097BC File Offset: 0x001079BC
	public void recalculateNeighbourCities()
	{
		this.neighbours_cities.Clear();
		this.neighbours_cities_kingdom.Clear();
		this.neighbours_kingdoms.Clear();
		foreach (TileZone tileZone in this.neighbour_zones)
		{
			City tZoneCity = tileZone.city;
			if (tZoneCity != this && tZoneCity != null)
			{
				this.neighbours_cities.Add(tZoneCity);
				if (tZoneCity.kingdom == this.kingdom)
				{
					this.neighbours_cities_kingdom.Add(tZoneCity);
				}
				else
				{
					this.neighbours_kingdoms.Add(tZoneCity.kingdom);
				}
			}
		}
	}

	// Token: 0x06001DF7 RID: 7671 RVA: 0x00109874 File Offset: 0x00107A74
	public void recalculateNeighbourZones()
	{
		this.border_zones.Clear();
		this.neighbour_zones.Clear();
		List<TileZone> tZones = this.zones;
		for (int i = 0; i < tZones.Count; i++)
		{
			TileZone tParentZone = tZones[i];
			foreach (TileZone tNeighbourZone in tParentZone.neighbours_all)
			{
				if (tNeighbourZone.city != this)
				{
					this.border_zones.Add(tParentZone);
					this.neighbour_zones.Add(tNeighbourZone);
				}
			}
		}
	}

	// Token: 0x06001DF8 RID: 7672 RVA: 0x001098FB File Offset: 0x00107AFB
	internal void setCulture(Culture pCulture)
	{
		if (this.culture == pCulture)
		{
			return;
		}
		this.culture = pCulture;
		World.world.cultures.setDirtyCities();
	}

	// Token: 0x06001DF9 RID: 7673 RVA: 0x0010991D File Offset: 0x00107B1D
	public Culture getCulture()
	{
		return this.culture;
	}

	// Token: 0x06001DFA RID: 7674 RVA: 0x00109925 File Offset: 0x00107B25
	public Language getLanguage()
	{
		return this.language;
	}

	// Token: 0x06001DFB RID: 7675 RVA: 0x0010992D File Offset: 0x00107B2D
	public Religion getReligion()
	{
		return this.religion;
	}

	// Token: 0x06001DFC RID: 7676 RVA: 0x00109935 File Offset: 0x00107B35
	public void checkAbandon()
	{
		if (!this._dirty_abandoned_zones)
		{
			return;
		}
		this._dirty_abandoned_zones = false;
		World.world.city_zone_helper.city_abandon.check(this, false, null);
	}

	// Token: 0x06001DFD RID: 7677 RVA: 0x00109960 File Offset: 0x00107B60
	public void update(float pElapsed)
	{
		if (this.timer_build > 0f)
		{
			this.timer_build -= pElapsed;
		}
		this.updateTotalFood();
		if (this.data.timer_supply > 0f)
		{
			this.data.timer_supply -= pElapsed;
		}
		if (this.data.timer_trade > 0f)
		{
			this.data.timer_trade -= pElapsed;
		}
		if (this._timer_warrior > 0f)
		{
			this._timer_warrior -= pElapsed;
		}
		if (base.isDirtyUnits())
		{
			return;
		}
		if (!this.kingdom.wild && !this.hasUnits())
		{
			this.turnCityToNeutral();
			return;
		}
		if (this._dirty_city_status)
		{
			this.updateCityStatus();
		}
		if (this._dirty_citizens)
		{
			this.updateCitizens();
		}
		if (World.world.isPaused())
		{
			return;
		}
		if (this.timer_build_boat > 0f)
		{
			this.timer_build_boat -= pElapsed;
		}
		if (this.ai != null)
		{
			if (this.timer_action > 0f)
			{
				this.timer_action -= pElapsed;
			}
			else
			{
				this.ai.update();
			}
			this.ai.updateSingleTasks(pElapsed);
		}
		this.updateCapture(pElapsed);
	}

	// Token: 0x06001DFE RID: 7678 RVA: 0x00109A9F File Offset: 0x00107C9F
	private void turnCityToNeutral()
	{
		this.makeBoatsAbandonCity();
		this.setKingdom(WildKingdomsManager.neutral, false);
		this.forceBuildingsToKingdom(this.buildings, WildKingdomsManager.neutral);
	}

	// Token: 0x06001DFF RID: 7679 RVA: 0x00109AC4 File Offset: 0x00107CC4
	private void makeBoatsAbandonCity()
	{
		if (this.countBoats() == 0)
		{
			return;
		}
		foreach (Actor tBoat in this._boats)
		{
			if (!tBoat.isRekt())
			{
				tBoat.setCity(null);
			}
		}
	}

	// Token: 0x06001E00 RID: 7680 RVA: 0x00109B28 File Offset: 0x00107D28
	private void updateTotalFood()
	{
		this._current_total_food = this.countFoodTotal();
	}

	// Token: 0x06001E01 RID: 7681 RVA: 0x00109B38 File Offset: 0x00107D38
	private void updateCapture(float pElapsed)
	{
		if (this.last_visual_capture_ticks == 0 && !this.isGettingCaptured())
		{
			return;
		}
		if ((int)this._capture_ticks != this.last_visual_capture_ticks)
		{
			if ((int)this._capture_ticks > this.last_visual_capture_ticks)
			{
				this.last_visual_capture_ticks++;
			}
			else
			{
				this.last_visual_capture_ticks--;
			}
		}
		this.last_visual_capture_ticks = Mathf.Clamp(this.last_visual_capture_ticks, 0, 100);
		if (this._timer_capture > 0f)
		{
			this._timer_capture -= pElapsed;
			return;
		}
		this._timer_capture = 0.1f;
		int tTowers = this.countBuildingsType("type_watch_tower", true);
		if (tTowers > 0)
		{
			this.addCapturePoints(this.kingdom, 10 * tTowers);
		}
		Kingdom tDominating = null;
		foreach (Kingdom iKingdom in this._capturing_units.Keys)
		{
			if (tDominating == null)
			{
				tDominating = iKingdom;
			}
			else if (this._capturing_units[iKingdom] > this._capturing_units[tDominating])
			{
				tDominating = iKingdom;
			}
		}
		if (tDominating == null)
		{
			this._capture_ticks -= 0.5f;
			if (this._capture_ticks <= 0f)
			{
				this.clearCapture();
			}
			return;
		}
		bool haveDefenders = false;
		if (this._capturing_units.ContainsKey(this.kingdom) && this._capturing_units[this.kingdom] > 0 && this.countWarriors() > 0)
		{
			haveDefenders = true;
		}
		if (this.being_captured_by != null && !this.being_captured_by.isAlive())
		{
			this.being_captured_by = null;
		}
		bool tCaptureGoDown = false;
		if (this.kingdom == tDominating)
		{
			tCaptureGoDown = true;
		}
		if (haveDefenders && this._capturing_units.Count == 1)
		{
			tCaptureGoDown = true;
		}
		if (tCaptureGoDown)
		{
			this._capture_ticks -= 1f;
			if (this._capture_ticks <= 0f)
			{
				this.clearCapture();
			}
			return;
		}
		if (tDominating.isEnemy(this.kingdom) && (!haveDefenders || this._capture_ticks < 5f))
		{
			if (this.being_captured_by == null || this.being_captured_by == tDominating)
			{
				this._capture_ticks += 1f + 1f * pElapsed;
				this.being_captured_by = tDominating;
				if (this._capture_ticks >= 100f)
				{
					this.finishCapture(tDominating);
					return;
				}
			}
			else if (tDominating.isEnemy(this.being_captured_by))
			{
				this._capture_ticks -= 0.5f;
				if (this._capture_ticks <= 0f)
				{
					this.clearCapture();
					return;
				}
			}
			else
			{
				this._capture_ticks += 1f + 1f * pElapsed;
				if (this._capture_ticks >= 100f)
				{
					this.finishCapture(this.being_captured_by);
				}
			}
		}
	}

	// Token: 0x06001E02 RID: 7682 RVA: 0x00109DF0 File Offset: 0x00107FF0
	public bool isGettingCaptured()
	{
		return this._capturing_units.Count != 0 && (this._capturing_units.Count != 1 || !this._capturing_units.ContainsKey(this.kingdom));
	}

	// Token: 0x06001E03 RID: 7683 RVA: 0x00109E28 File Offset: 0x00108028
	public bool isGettingCapturedBy(Kingdom pKingdom)
	{
		int tCount;
		return this._capturing_units.TryGetValue(pKingdom, out tCount) && tCount > 0;
	}

	// Token: 0x06001E04 RID: 7684 RVA: 0x00109E4C File Offset: 0x0010804C
	public Kingdom getCapturingKingdom()
	{
		return this.being_captured_by;
	}

	// Token: 0x06001E05 RID: 7685 RVA: 0x00109E54 File Offset: 0x00108054
	private void clearCapture()
	{
		this._capture_ticks = 0f;
		this.being_captured_by = null;
	}

	// Token: 0x06001E06 RID: 7686 RVA: 0x00109E68 File Offset: 0x00108068
	public float getCaptureTicks()
	{
		return this._capture_ticks;
	}

	// Token: 0x06001E07 RID: 7687 RVA: 0x00109E70 File Offset: 0x00108070
	private void prepareProfessionDicts()
	{
		if (this._professions_dict.Count == 0)
		{
			for (int i = 0; i < ProfessionLibrary.list_enum_profession_ids.Length; i++)
			{
				UnitProfession tPro = ProfessionLibrary.list_enum_profession_ids[i];
				this._professions_dict.Add(tPro, new List<Actor>());
			}
		}
	}

	// Token: 0x06001E08 RID: 7688 RVA: 0x00109EB8 File Offset: 0x001080B8
	private void updateCitizens()
	{
		this._dirty_citizens = false;
		this.prepareProfessionDicts();
		foreach (List<Actor> list in this._professions_dict.Values)
		{
			list.Clear();
		}
		List<Actor> tUnits = base.units;
		for (int i = 0; i < tUnits.Count; i++)
		{
			Actor tActor = tUnits[i];
			if (tActor != null && tActor.isAlive())
			{
				this._professions_dict[tActor.getProfession()].Add(tActor);
			}
		}
	}

	// Token: 0x06001E09 RID: 7689 RVA: 0x00109F5C File Offset: 0x0010815C
	public bool canGrowZones()
	{
		return DebugConfig.isOn(DebugOption.SystemZoneGrowth) && !this._dirty_abandoned_zones && this.getPopulationPeople() != 0;
	}

	// Token: 0x06001E0A RID: 7690 RVA: 0x00109F80 File Offset: 0x00108180
	internal int countProfession(UnitProfession pType)
	{
		List<Actor> tList;
		if (this._professions_dict.TryGetValue(pType, out tList))
		{
			return tList.Count;
		}
		return 0;
	}

	// Token: 0x06001E0B RID: 7691 RVA: 0x00109FA8 File Offset: 0x001081A8
	public void destroyCity()
	{
		this.removeLeader();
		this.disbandArmy();
		foreach (TileZone tileZone in this.zones)
		{
			tileZone.setCity(null);
		}
		foreach (Actor tActor in World.world.units)
		{
			if (tActor.city == this)
			{
				tActor.setCity(null);
			}
		}
		this.data.equipment.clearItems();
		base.units.Clear();
		this._boats.Clear();
		this.zones.Clear();
		if (this.hasKingdom())
		{
			this.removeFromCurrentKingdom();
		}
	}

	// Token: 0x06001E0C RID: 7692 RVA: 0x0010A090 File Offset: 0x00108290
	public override void Dispose()
	{
		DBInserter.deleteData(this.getID(), "city");
		City._connected_checked.Clear();
		City._connected_next_wave.Clear();
		City._connected_current_wave.Clear();
		this.stockpiles.Clear();
		this.storages.Clear();
		this._cached_book_ids.Clear();
		this._cached_buildings_with_book_slots.Clear();
		base.units.Clear();
		this._boats.Clear();
		this.buildings.Clear();
		this.buildings_dict_id.Clear();
		this.buildings_dict_type.Clear();
		this.zones.Clear();
		this.road_tiles_to_build.Clear();
		this.calculated_place_for_farms.Clear();
		this.calculated_farm_fields.Clear();
		this.calculated_crops.Clear();
		this.calculated_grown_wheat.Clear();
		this._professions_dict.Clear();
		this.neighbour_zones.Clear();
		this.border_zones.Clear();
		this.neighbours_cities.Clear();
		this.neighbours_cities_kingdom.Clear();
		this.neighbours_kingdoms.Clear();
		this.tiles_to_remove.Clear();
		this.danger_zones.Clear();
		this._capturing_units.Clear();
		this._city_tile = null;
		this.target_attack_zone = null;
		this.target_attack_city = null;
		this.army = null;
		this.tasks.clear();
		this.jobs.clear();
		this.status.clear();
		this.under_construction_building = null;
		this.culture = null;
		this.language = null;
		this.religion = null;
		this.kingdom = null;
		this.leader = null;
		this.being_captured_by = null;
		this._debug_last_possible_build_orders = null;
		this._debug_last_possible_build_orders_no_resources = null;
		this._debug_last_build_order_try = null;
		this.timestamp_shrink = 0.0;
		this.ai.reset();
		base.Dispose();
	}

	// Token: 0x06001E0D RID: 7693 RVA: 0x0010A276 File Offset: 0x00108476
	public bool hasAttackZoneOrder()
	{
		return this.target_attack_zone != null;
	}

	// Token: 0x06001E0E RID: 7694 RVA: 0x0010A284 File Offset: 0x00108484
	internal void spendResourcesForBuildingAsset(ConstructionCost pCost)
	{
		this.takeResource("wood", pCost.wood);
		this.takeResource("gold", pCost.gold);
		this.takeResource("stone", pCost.stone);
		this.takeResource("common_metals", pCost.common_metals);
	}

	// Token: 0x06001E0F RID: 7695 RVA: 0x0010A2D8 File Offset: 0x001084D8
	internal bool hasEnoughResourcesFor(ConstructionCost pCost)
	{
		return DebugConfig.isOn(DebugOption.CityInfiniteResources) || (this.amount_wood >= pCost.wood && this.amount_common_metals >= pCost.common_metals && this.amount_stone >= pCost.stone && this.amount_gold >= pCost.gold);
	}

	// Token: 0x170001CE RID: 462
	// (get) Token: 0x06001E10 RID: 7696 RVA: 0x0010A334 File Offset: 0x00108534
	public int amount_wood
	{
		get
		{
			return this.getResourcesAmount("wood");
		}
	}

	// Token: 0x170001CF RID: 463
	// (get) Token: 0x06001E11 RID: 7697 RVA: 0x0010A341 File Offset: 0x00108541
	public int amount_gold
	{
		get
		{
			return this.getResourcesAmount("gold");
		}
	}

	// Token: 0x170001D0 RID: 464
	// (get) Token: 0x06001E12 RID: 7698 RVA: 0x0010A34E File Offset: 0x0010854E
	public int amount_stone
	{
		get
		{
			return this.getResourcesAmount("stone");
		}
	}

	// Token: 0x170001D1 RID: 465
	// (get) Token: 0x06001E13 RID: 7699 RVA: 0x0010A35B File Offset: 0x0010855B
	public int amount_common_metals
	{
		get
		{
			return this.getResourcesAmount("common_metals");
		}
	}

	// Token: 0x06001E14 RID: 7700 RVA: 0x0010A368 File Offset: 0x00108568
	internal Building getBuildingToBuild()
	{
		if (this.under_construction_building != null && (!this.under_construction_building.isAlive() || !this.under_construction_building.isUnderConstruction()))
		{
			this.under_construction_building = null;
		}
		return this.under_construction_building;
	}

	// Token: 0x06001E15 RID: 7701 RVA: 0x0010A399 File Offset: 0x00108599
	internal bool hasBuildingToBuild()
	{
		if (this.under_construction_building == null)
		{
			return false;
		}
		if (!this.under_construction_building.isAlive() || !this.under_construction_building.isUnderConstruction())
		{
			this.under_construction_building = null;
			return false;
		}
		return true;
	}

	// Token: 0x06001E16 RID: 7702 RVA: 0x0010A3CC File Offset: 0x001085CC
	internal void setBuildingDictType(Building pBuilding)
	{
		List<Building> tList = this.getBuildingListOfType(pBuilding.asset.type);
		if (tList == null)
		{
			tList = new List<Building>();
			this.buildings_dict_type.Add(pBuilding.asset.type, tList);
		}
		tList.Add(pBuilding);
	}

	// Token: 0x06001E17 RID: 7703 RVA: 0x0010A414 File Offset: 0x00108614
	internal List<Building> getBuildingListOfID(string pBuildingID)
	{
		List<Building> tList;
		this.buildings_dict_id.TryGetValue(pBuildingID, out tList);
		return tList;
	}

	// Token: 0x06001E18 RID: 7704 RVA: 0x0010A431 File Offset: 0x00108631
	public int countZones()
	{
		return this.zones.Count;
	}

	// Token: 0x06001E19 RID: 7705 RVA: 0x0010A43E File Offset: 0x0010863E
	public int countBuildings()
	{
		return this.buildings.Count;
	}

	// Token: 0x06001E1A RID: 7706 RVA: 0x0010A44C File Offset: 0x0010864C
	public int countBuildingsOfID(string pBuildingID)
	{
		List<Building> tList = this.getBuildingListOfID(pBuildingID);
		if (tList == null)
		{
			return 0;
		}
		return tList.Count;
	}

	// Token: 0x06001E1B RID: 7707 RVA: 0x0010A46C File Offset: 0x0010866C
	internal void setBuildingDictID(Building pBuilding)
	{
		List<Building> tList;
		if (!this.buildings_dict_id.TryGetValue(pBuilding.asset.id, out tList))
		{
			this.buildings_dict_id.Add(pBuilding.asset.id, tList = new List<Building>());
		}
		tList.Add(pBuilding);
	}

	// Token: 0x06001E1C RID: 7708 RVA: 0x0010A4B8 File Offset: 0x001086B8
	public int countBuildingsType(string pBuildingTypeID, bool pCountOnlyFinished = true)
	{
		List<Building> tList = this.getBuildingListOfType(pBuildingTypeID);
		if (tList == null)
		{
			return 0;
		}
		if (pCountOnlyFinished)
		{
			int tCount = 0;
			using (List<Building>.Enumerator enumerator = tList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.isUnderConstruction())
					{
						tCount++;
					}
				}
			}
			return tCount;
		}
		return tList.Count;
	}

	// Token: 0x06001E1D RID: 7709 RVA: 0x0010A524 File Offset: 0x00108724
	internal bool hasBuildingType(string pBuildingTypeID, bool pCountOnlyFinished = true, TileIsland pLimitIsland = null)
	{
		List<Building> tList = this.getBuildingListOfType(pBuildingTypeID);
		if (tList == null)
		{
			return false;
		}
		if (tList.Count == 0)
		{
			return false;
		}
		bool tLimitIsland = pLimitIsland != null;
		foreach (Building tBuilding in tList)
		{
			if ((!pCountOnlyFinished || (!tBuilding.isUnderConstruction() && tBuilding.isUsable())) && (!tLimitIsland || tBuilding.current_island == pLimitIsland))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001E1E RID: 7710 RVA: 0x0010A5B0 File Offset: 0x001087B0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal List<Building> getBuildingListOfType(string pType)
	{
		List<Building> tList;
		this.buildings_dict_type.TryGetValue(pType, out tList);
		return tList;
	}

	// Token: 0x06001E1F RID: 7711 RVA: 0x0010A5D0 File Offset: 0x001087D0
	internal Building getBuildingOfType(string pBuildingTypeID, bool pCountOnlyFinished = true, bool pRandom = false, bool pOnlyFreeTile = false, TileIsland pLimitIsland = null)
	{
		List<Building> tList = this.getBuildingListOfType(pBuildingTypeID);
		if (tList == null)
		{
			return null;
		}
		if (tList.Count == 0)
		{
			return null;
		}
		bool tLimitIsland = pLimitIsland != null;
		IEnumerable<Building> enumerable2;
		if (!pRandom)
		{
			IEnumerable<Building> enumerable = tList;
			enumerable2 = enumerable;
		}
		else
		{
			enumerable2 = tList.LoopRandom<Building>();
		}
		foreach (Building tBuilding in enumerable2)
		{
			if ((!pCountOnlyFinished || (!tBuilding.isUnderConstruction() && tBuilding.isUsable())) && (!pOnlyFreeTile || !tBuilding.current_tile.isTargeted()) && (!tLimitIsland || tBuilding.current_island == pLimitIsland))
			{
				return tBuilding;
			}
		}
		return null;
	}

	// Token: 0x06001E20 RID: 7712 RVA: 0x0010A67C File Offset: 0x0010887C
	public void addRoads(List<WorldTile> pTiles)
	{
		for (int i = 0; i < pTiles.Count; i++)
		{
			WorldTile tTile = pTiles[i];
			if (!tTile.Type.road && !this.road_tiles_to_build.Contains(tTile))
			{
				this.road_tiles_to_build.Add(tTile);
			}
		}
	}

	// Token: 0x06001E21 RID: 7713 RVA: 0x0010A6C9 File Offset: 0x001088C9
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private bool isArmyFull()
	{
		return this.status.warriors_current >= this.status.warrior_slots;
	}

	// Token: 0x06001E22 RID: 7714 RVA: 0x0010A6E6 File Offset: 0x001088E6
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private bool isArmyOverLimit()
	{
		return this.status.warriors_current > this.status.warrior_slots;
	}

	// Token: 0x06001E23 RID: 7715 RVA: 0x0010A704 File Offset: 0x00108904
	private bool tryToMakeWarrior(Actor pActor)
	{
		if (!this.checkCanMakeWarrior(pActor))
		{
			return false;
		}
		this.makeWarrior(pActor);
		this._timer_warrior = 15f;
		if (this.hasLeader())
		{
			float tDecrease = this.leader.stats["warfare"] / 2f;
			this._timer_warrior -= tDecrease;
			if (this._timer_warrior < 1f)
			{
				this._timer_warrior = 1f;
			}
		}
		if (this.hasBuildingType("type_barracks", true, null))
		{
			this._timer_warrior /= 2f;
		}
		return true;
	}

	// Token: 0x06001E24 RID: 7716 RVA: 0x0010A79C File Offset: 0x0010899C
	public bool checkCanMakeWarrior(Actor pActor)
	{
		if (this.isArmyFull())
		{
			return false;
		}
		if (pActor.isBaby())
		{
			return false;
		}
		if (this.hasCulture())
		{
			if (pActor.isSexFemale() && this.culture.hasTrait("conscription_male_only"))
			{
				return false;
			}
			if (pActor.isSexMale() && this.culture.hasTrait("conscription_female_only"))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06001E25 RID: 7717 RVA: 0x0010A7FE File Offset: 0x001089FE
	public void makeWarrior(Actor pActor)
	{
		pActor.setProfession(UnitProfession.Warrior, true);
		if (pActor.equipment.weapon.isEmpty())
		{
			City.giveItem(pActor, this.getEquipmentList(EquipmentType.Weapon), this);
		}
		this.status.warriors_current++;
	}

	// Token: 0x06001E26 RID: 7718 RVA: 0x0010A83C File Offset: 0x00108A3C
	public bool checkIfWarriorStillOk(Actor pActor)
	{
		bool tIsOk = true;
		if (this.isArmyOverLimit())
		{
			tIsOk = false;
		}
		else if (!this.hasEnoughFoodForArmy())
		{
			tIsOk = false;
		}
		if (!tIsOk)
		{
			pActor.stopBeingWarrior();
			this._timer_warrior = 30f;
		}
		return tIsOk;
	}

	// Token: 0x06001E27 RID: 7719 RVA: 0x0010A878 File Offset: 0x00108A78
	public void setCitizenJob(Actor pActor)
	{
		if (!this.isGettingCaptured() && this._timer_warrior <= 0f && pActor.isProfession(UnitProfession.Unit) && this.getResourcesAmount("gold") > 10 && this.hasEnoughFoodForArmy() && this.tryToMakeWarrior(pActor))
		{
			return;
		}
		if (this.checkCitizenJobList(AssetManager.citizen_job_library.list_priority_high, pActor))
		{
			return;
		}
		if (!this.hasAnyFood() && this.checkCitizenJobList(AssetManager.citizen_job_library.list_priority_high_food, pActor))
		{
			return;
		}
		List<CitizenJobAsset> tJobList = AssetManager.citizen_job_library.list_priority_normal;
		for (int i = 0; i < tJobList.Count; i++)
		{
			this._last_checked_job_id++;
			if (this._last_checked_job_id > tJobList.Count - 1)
			{
				this._last_checked_job_id = 0;
			}
			CitizenJobAsset tCitizenJobAsset = tJobList[this._last_checked_job_id];
			if ((tCitizenJobAsset.ok_for_king || !pActor.isKing()) && (tCitizenJobAsset.ok_for_leader || !pActor.isCityLeader()) && this.checkCitizenJob(tCitizenJobAsset, this, pActor))
			{
				return;
			}
		}
	}

	// Token: 0x06001E28 RID: 7720 RVA: 0x0010A970 File Offset: 0x00108B70
	private bool checkCitizenJobList(List<CitizenJobAsset> pList, Actor pActor)
	{
		for (int i = 0; i < pList.Count; i++)
		{
			CitizenJobAsset tAsset = pList[i];
			if (this.checkCitizenJob(tAsset, this, pActor))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001E29 RID: 7721 RVA: 0x0010A9A4 File Offset: 0x00108BA4
	private bool checkCitizenJob(CitizenJobAsset pJobAsset, City pCity, Actor pActor)
	{
		if (pJobAsset.only_leaders && !pActor.isKing() && !pActor.isCityLeader())
		{
			return false;
		}
		if (pJobAsset.should_be_assigned != null && !pJobAsset.should_be_assigned(pActor))
		{
			return false;
		}
		if (this.jobs.hasJob(pJobAsset))
		{
			this.jobs.takeJob(pJobAsset);
			pActor.setCitizenJob(pJobAsset);
			return true;
		}
		return false;
	}

	// Token: 0x06001E2A RID: 7722 RVA: 0x0010AA08 File Offset: 0x00108C08
	public bool hasSuitableFood(Subspecies pSubspecies)
	{
		HashSet<string> tAllowedFood = pSubspecies.getAllowedFoodByDiet();
		foreach (Building tStorage in this.storages)
		{
			if (tStorage.isUsable())
			{
				foreach (string tAllowedFoodID in tAllowedFood)
				{
					if (tStorage.getResourcesAmount(tAllowedFoodID) != 0)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06001E2B RID: 7723 RVA: 0x0010AAB0 File Offset: 0x00108CB0
	internal ResourceAsset getFoodItem(Subspecies pSubspecies, string pFavoriteFood = null)
	{
		if (!string.IsNullOrEmpty(pFavoriteFood) && this.getResourcesAmount(pFavoriteFood) > 0)
		{
			return AssetManager.resources.get(pFavoriteFood);
		}
		return this.getRandomSuitableFood(pSubspecies);
	}

	// Token: 0x06001E2C RID: 7724 RVA: 0x0010AAD7 File Offset: 0x00108CD7
	internal void eatFoodItem(string pItem)
	{
		if (pItem == null)
		{
			return;
		}
		this.takeResource(pItem, 1);
		this.data.total_food_consumed++;
	}

	// Token: 0x06001E2D RID: 7725 RVA: 0x0010AAF8 File Offset: 0x00108CF8
	internal void removeZone(TileZone pZone)
	{
		this.setAbandonedZonesDirty();
		if (this.zones.Remove(pZone))
		{
			pZone.setCity(null);
			World.world.city_zone_helper.city_place_finder.setDirty();
		}
		this.updateCityCenter();
		this.setStatusDirty();
	}

	// Token: 0x06001E2E RID: 7726 RVA: 0x0010AB38 File Offset: 0x00108D38
	internal void addZone(TileZone pZone)
	{
		if (this.zones.Contains(pZone))
		{
			return;
		}
		if (pZone.city != null)
		{
			pZone.city.removeZone(pZone);
		}
		this.zones.Add(pZone);
		pZone.setCity(this);
		this.updateCityCenter();
		if (World.world.city_zone_helper.city_place_finder.hasPossibleZones())
		{
			World.world.city_zone_helper.city_place_finder.setDirty();
		}
		this.setStatusDirty();
	}

	// Token: 0x06001E2F RID: 7727 RVA: 0x0010ABB4 File Offset: 0x00108DB4
	public int getLoyalty(bool pForceRecalc = false)
	{
		if (this.kingdom.isNeutral())
		{
			this._loyalty_cached = 0;
		}
		else if (World.world.getWorldTimeElapsedSince(this._loyalty_last_time) > 3f || pForceRecalc)
		{
			this._loyalty_cached = LoyaltyCalculator.calculate(this);
			this._loyalty_last_time = World.world.getCurWorldTime();
		}
		return this._loyalty_cached;
	}

	// Token: 0x06001E30 RID: 7728 RVA: 0x0010AC14 File Offset: 0x00108E14
	public int getCachedLoyalty()
	{
		return this._loyalty_cached;
	}

	// Token: 0x06001E31 RID: 7729 RVA: 0x0010AC1C File Offset: 0x00108E1C
	public bool isCapitalCity()
	{
		return this.kingdom != null && this == this.kingdom.capital;
	}

	// Token: 0x06001E32 RID: 7730 RVA: 0x0010AC36 File Offset: 0x00108E36
	internal void updateAge()
	{
		if (this.hasLeader() && this.leader.hasClan())
		{
			this.leader.addRenown(1);
		}
	}

	// Token: 0x06001E33 RID: 7731 RVA: 0x0010AC5C File Offset: 0x00108E5C
	private void updateCityCenter()
	{
		if (!this.hasZones())
		{
			this.city_center = Globals.POINT_IN_VOID_2;
			return;
		}
		float x = 0f;
		float y = 0f;
		float tBestDistance = float.MaxValue;
		TileZone tBestCenterZone = null;
		for (int i = 0; i < this.zones.Count; i++)
		{
			TileZone tZone = this.zones[i];
			x += tZone.centerTile.posV3.x;
			y += tZone.centerTile.posV3.y;
		}
		this.city_center.x = x / (float)this.zones.Count;
		this.city_center.y = y / (float)this.zones.Count;
		for (int j = 0; j < this.zones.Count; j++)
		{
			TileZone tZone2 = this.zones[j];
			float tDistance = Toolbox.SquaredDist((float)tZone2.centerTile.x, (float)tZone2.centerTile.y, this.city_center.x, this.city_center.y);
			if (tDistance < tBestDistance)
			{
				tBestCenterZone = tZone2;
				tBestDistance = tDistance;
			}
		}
		this.city_center.x = tBestCenterZone.centerTile.posV3.x;
		this.city_center.y = tBestCenterZone.centerTile.posV3.y + 2f;
		this.last_city_center = this.city_center;
	}

	// Token: 0x06001E34 RID: 7732 RVA: 0x0010ADC7 File Offset: 0x00108FC7
	internal void removeFromCurrentKingdom()
	{
		this.kingdom.checkClearCapital(this);
	}

	// Token: 0x06001E35 RID: 7733 RVA: 0x0010ADD8 File Offset: 0x00108FD8
	internal void switchedKingdom()
	{
		List<Building> tBuildings = this.buildings;
		for (int i = 0; i < tBuildings.Count; i++)
		{
			Building tBuilding = tBuildings[i];
			if (!tBuilding.isRemoved())
			{
				tBuilding.setKingdomCiv(this.kingdom);
			}
		}
		World.world.zone_calculator.setDrawnZonesDirty();
	}

	// Token: 0x06001E36 RID: 7734 RVA: 0x0010AE28 File Offset: 0x00109028
	internal void useInspire(Actor pActor)
	{
		Kingdom tOldKingdom = this.kingdom;
		this.makeOwnKingdom(pActor, true, false);
		World.world.diplomacy.startWar(tOldKingdom, this.kingdom, WarTypeLibrary.inspire, false);
	}

	// Token: 0x06001E37 RID: 7735 RVA: 0x0010AE63 File Offset: 0x00109063
	internal void clearCurrentCaptureAmounts()
	{
		this._capturing_units.Clear();
	}

	// Token: 0x06001E38 RID: 7736 RVA: 0x0010AE70 File Offset: 0x00109070
	internal void clearDangerZones()
	{
		this.danger_zones.Clear();
	}

	// Token: 0x06001E39 RID: 7737 RVA: 0x0010AE7D File Offset: 0x0010907D
	public bool isInDanger()
	{
		return this.danger_zones.Count > 0;
	}

	// Token: 0x06001E3A RID: 7738 RVA: 0x0010AE90 File Offset: 0x00109090
	internal void updateConquest(Actor pActor)
	{
		if (!pActor.isKingdomCiv())
		{
			return;
		}
		if (pActor.kingdom != this.kingdom && !pActor.kingdom.isEnemy(this.kingdom))
		{
			return;
		}
		this.addCapturePoints(pActor, 1);
	}

	// Token: 0x06001E3B RID: 7739 RVA: 0x0010AEC5 File Offset: 0x001090C5
	public void addCapturePoints(BaseSimObject pObject, int pValue)
	{
		this.addCapturePoints(pObject.kingdom, pValue);
	}

	// Token: 0x06001E3C RID: 7740 RVA: 0x0010AED4 File Offset: 0x001090D4
	public void addCapturePoints(Kingdom pKingdom, int pValue)
	{
		int tCurrentCount;
		this._capturing_units.TryGetValue(pKingdom, out tCurrentCount);
		this._capturing_units[pKingdom] = tCurrentCount + pValue;
	}

	// Token: 0x06001E3D RID: 7741 RVA: 0x0010AF00 File Offset: 0x00109100
	public void debugCaptureUnits(DebugTool pTool)
	{
		pTool.setText("capture units:", this._capturing_units.Count, 0f, false, 0L, false, false, "");
		pTool.setText("isGettingCaptured()", this.isGettingCaptured(), 0f, false, 0L, false, false, "");
		foreach (Kingdom iKingdom in this._capturing_units.Keys)
		{
			pTool.setText("-" + iKingdom.name, this._capturing_units[iKingdom], 0f, false, 0L, false, false, "");
		}
	}

	// Token: 0x06001E3E RID: 7742 RVA: 0x0010AFD8 File Offset: 0x001091D8
	internal void finishCapture(Kingdom pNewKingdom)
	{
		if (this.kingdom.hasKing() && this.kingdom.king.city == this)
		{
			this.kingdom.kingFledCity();
		}
		if (World.world.cities.isLocked())
		{
			return;
		}
		this.clearCapture();
		this.recalculateNeighbourCities();
		pNewKingdom.increaseHappinessFromNewCityCapture();
		this.kingdom.decreaseHappinessFromLostCityCapture(this);
		using (ListPool<War> tListWars = new ListPool<War>(pNewKingdom.getWars(false)))
		{
			Kingdom tKingdomToJoin = this.findKingdomToJoinAfterCapture(pNewKingdom, tListWars);
			if (!this.checkRebelWar(tKingdomToJoin, tListWars))
			{
				tKingdomToJoin.data.timestamp_new_conquest = World.world.getCurWorldTime();
			}
			this.removeSoldiers();
			this.joinAnotherKingdom(tKingdomToJoin, true, false);
		}
	}

	// Token: 0x06001E3F RID: 7743 RVA: 0x0010B0A0 File Offset: 0x001092A0
	private Kingdom findKingdomToJoinAfterCapture(Kingdom pKingdom, ListPool<War> pWars)
	{
		Kingdom tResultKingdom = null;
		for (int i = 0; i < pWars.Count; i++)
		{
			War tWar = pWars[i];
			if (!tWar.isTotalWar() && tWar.hasKingdom(this.kingdom) && tWar.isInWarWith(pKingdom, this.kingdom))
			{
				if (tWar.isMainAttacker(pKingdom) || tWar.isMainDefender(pKingdom))
				{
					break;
				}
				if (tWar.isAttacker(this.kingdom))
				{
					Kingdom tMainDefender = tWar.main_defender;
					if (!tMainDefender.isRekt())
					{
						if (this.neighbours_kingdoms.Contains(tMainDefender))
						{
							tResultKingdom = tMainDefender;
							break;
						}
						if (this.neighbours_kingdoms.Contains(pKingdom))
						{
							tResultKingdom = pKingdom;
							break;
						}
						tResultKingdom = tMainDefender;
						break;
					}
				}
				if (tWar.isDefender(this.kingdom))
				{
					Kingdom tMainAttacker = tWar.main_attacker;
					if (!tMainAttacker.isRekt())
					{
						if (this.neighbours_kingdoms.Contains(tMainAttacker))
						{
							tResultKingdom = tMainAttacker;
							break;
						}
						if (this.neighbours_kingdoms.Contains(pKingdom))
						{
							tResultKingdom = pKingdom;
							break;
						}
						tResultKingdom = tMainAttacker;
						break;
					}
				}
			}
		}
		if (tResultKingdom == null)
		{
			tResultKingdom = pKingdom;
		}
		else if (tResultKingdom.getSpecies() != this.kingdom.getSpecies())
		{
			tResultKingdom = pKingdom;
		}
		return tResultKingdom;
	}

	// Token: 0x06001E40 RID: 7744 RVA: 0x0010B1C8 File Offset: 0x001093C8
	private bool checkRebelWar(Kingdom pKingdomToJoin, ListPool<War> pWars)
	{
		foreach (War ptr in pWars)
		{
			War tWar = ptr;
			if (tWar.getAsset().rebellion && tWar.isMainAttacker(pKingdomToJoin) && tWar.isInWarWith(pKingdomToJoin, this.kingdom))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001E41 RID: 7745 RVA: 0x0010B23C File Offset: 0x0010943C
	private void removeSoldiers()
	{
		foreach (Actor actor in this._professions_dict[UnitProfession.Warrior])
		{
			actor.setProfession(UnitProfession.Unit, true);
		}
		this.disbandArmy();
	}

	// Token: 0x06001E42 RID: 7746 RVA: 0x0010B29C File Offset: 0x0010949C
	public void disbandArmy()
	{
		this.checkArmyExistence();
		if (!this.hasArmy())
		{
			return;
		}
		this.army.disband();
		this.checkArmyExistence();
	}

	// Token: 0x06001E43 RID: 7747 RVA: 0x0010B2BE File Offset: 0x001094BE
	public void checkArmyExistence()
	{
		if (!this.hasArmy())
		{
			return;
		}
		if (this.army.isAlive() && this.army.countUnits() > 0)
		{
			return;
		}
		this.setArmy(null);
	}

	// Token: 0x06001E44 RID: 7748 RVA: 0x0010B2EC File Offset: 0x001094EC
	public bool hasArmy()
	{
		return this.army != null;
	}

	// Token: 0x06001E45 RID: 7749 RVA: 0x0010B2F7 File Offset: 0x001094F7
	public Army getArmy()
	{
		return this.army;
	}

	// Token: 0x06001E46 RID: 7750 RVA: 0x0010B2FF File Offset: 0x001094FF
	public void setArmy(Army pArmy)
	{
		if (this.army != null && this.army != pArmy)
		{
			this.army.clearCity();
		}
		this.army = pArmy;
	}

	// Token: 0x06001E47 RID: 7751 RVA: 0x0010B324 File Offset: 0x00109524
	public Actor getRandomWarrior()
	{
		return this._professions_dict[UnitProfession.Warrior].GetRandom<Actor>();
	}

	// Token: 0x06001E48 RID: 7752 RVA: 0x0010B338 File Offset: 0x00109538
	internal Kingdom makeOwnKingdom(Actor pActor, bool pRebellion = false, bool pFellApart = false)
	{
		string tHappinessEvent = null;
		if (pRebellion)
		{
			World.world.game_stats.data.citiesRebelled += 1L;
			World.world.map_stats.citiesRebelled += 1L;
			tHappinessEvent = "just_rebelled";
		}
		if (pFellApart)
		{
			tHappinessEvent = "kingdom_fell_apart";
		}
		Kingdom tPrevKingdom = this.kingdom;
		this.removeFromCurrentKingdom();
		this.removeLeader();
		Kingdom tNewKingdom = World.world.kingdoms.makeNewCivKingdom(pActor, null, true);
		this.setKingdom(tNewKingdom, false);
		this.newForceKingdomEvent(base.units, this._boats, tNewKingdom, tHappinessEvent);
		this.switchedKingdom();
		tNewKingdom.copyMetasFromOtherKingdom(tPrevKingdom);
		tNewKingdom.setCityMetas(this);
		return tNewKingdom;
	}

	// Token: 0x06001E49 RID: 7753 RVA: 0x0010B3E5 File Offset: 0x001095E5
	public override int getPopulationPeople()
	{
		return this.countUnits();
	}

	// Token: 0x06001E4A RID: 7754 RVA: 0x0010B3ED File Offset: 0x001095ED
	public int getPopulationMaximum()
	{
		if (!WorldLawLibrary.world_law_civ_limit_population_100.isEnabled())
		{
			return this.status.housing_total;
		}
		if (this.status.housing_total >= 100)
		{
			return 100;
		}
		return this.status.housing_total;
	}

	// Token: 0x06001E4B RID: 7755 RVA: 0x0010B424 File Offset: 0x00109624
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int getUnitsTotal()
	{
		return this.countUnits() + this.countBoats();
	}

	// Token: 0x06001E4C RID: 7756 RVA: 0x0010B434 File Offset: 0x00109634
	public int countPopulationChildren()
	{
		int tCount = 0;
		foreach (Actor tActor in base.units)
		{
			if (tActor.isAlive() && tActor.isBaby())
			{
				tCount++;
			}
		}
		return tCount;
	}

	// Token: 0x06001E4D RID: 7757 RVA: 0x0010B498 File Offset: 0x00109698
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int countBoats()
	{
		return this._boats.Count;
	}

	// Token: 0x06001E4E RID: 7758 RVA: 0x0010B4A8 File Offset: 0x001096A8
	public void joinAnotherKingdom(Kingdom pNewSetKingdom, bool pCaptured = false, bool pRebellion = false)
	{
		string tHappinessEvent = null;
		if (pCaptured)
		{
			World.world.game_stats.data.citiesConquered += 1L;
			World.world.map_stats.citiesConquered += 1L;
			tHappinessEvent = "was_conquered";
		}
		if (pRebellion)
		{
			World.world.game_stats.data.citiesRebelled += 1L;
			World.world.map_stats.citiesRebelled += 1L;
			tHappinessEvent = "just_rebelled";
		}
		Kingdom tOldKingdom = this.kingdom;
		this.removeFromCurrentKingdom();
		this.setKingdom(pNewSetKingdom, false);
		this.newForceKingdomEvent(base.units, this._boats, pNewSetKingdom, tHappinessEvent);
		this.switchedKingdom();
		pNewSetKingdom.capturedFrom(tOldKingdom);
	}

	// Token: 0x06001E4F RID: 7759 RVA: 0x0010B569 File Offset: 0x00109769
	public int countWeapons()
	{
		return this.getEquipmentList(EquipmentType.Weapon).Count;
	}

	// Token: 0x06001E50 RID: 7760 RVA: 0x0010B577 File Offset: 0x00109777
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int countFoodTotal()
	{
		return this.countFood();
	}

	// Token: 0x06001E51 RID: 7761 RVA: 0x0010B57F File Offset: 0x0010977F
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasEnoughFoodForArmy()
	{
		return true;
	}

	// Token: 0x06001E52 RID: 7762 RVA: 0x0010B582 File Offset: 0x00109782
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int getTotalFood()
	{
		return this._current_total_food;
	}

	// Token: 0x06001E53 RID: 7763 RVA: 0x0010B58A File Offset: 0x0010978A
	public bool hasAnyFood()
	{
		return this._current_total_food > 0;
	}

	// Token: 0x06001E54 RID: 7764 RVA: 0x0010B595 File Offset: 0x00109795
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int countWarriors()
	{
		return this.countProfession(UnitProfession.Warrior);
	}

	// Token: 0x06001E55 RID: 7765 RVA: 0x0010B59E File Offset: 0x0010979E
	public bool hasAnyWarriors()
	{
		return this.countWarriors() > 0;
	}

	// Token: 0x06001E56 RID: 7766 RVA: 0x0010B5A9 File Offset: 0x001097A9
	public bool isHappy()
	{
		return this.getCachedLoyalty() >= 0;
	}

	// Token: 0x06001E57 RID: 7767 RVA: 0x0010B5B8 File Offset: 0x001097B8
	public float getArmyMaxMultiplier()
	{
		float num = 0f + this.getActorAsset().civ_base_army_multiplier;
		float tFromleader = this.getArmyMaxLeaderMultiplier();
		return num + tFromleader;
	}

	// Token: 0x06001E58 RID: 7768 RVA: 0x0010B5E0 File Offset: 0x001097E0
	public float getArmyMaxLeaderMultiplier()
	{
		float tMultiplier = 0f;
		if (this.hasLeader())
		{
			tMultiplier += this.leader.stats["army"];
			float tWarfareBonus = this.leader.stats["warfare"] * 2f / 100f;
			tMultiplier += tWarfareBonus;
		}
		return tMultiplier;
	}

	// Token: 0x06001E59 RID: 7769 RVA: 0x0010B63A File Offset: 0x0010983A
	public int getMaxWarriors()
	{
		return this.status.warrior_slots;
	}

	// Token: 0x06001E5A RID: 7770 RVA: 0x0010B647 File Offset: 0x00109847
	public void removeLeader()
	{
		this.leader = null;
		this.data.leaderID = -1L;
		this.rulerLeft();
	}

	// Token: 0x06001E5B RID: 7771 RVA: 0x0010B664 File Offset: 0x00109864
	public void setLeader(Actor pActor, bool pNew)
	{
		if (pActor == null)
		{
			return;
		}
		if (this.kingdom.king == pActor)
		{
			return;
		}
		this.leader = pActor;
		this.leader.setProfession(UnitProfession.Leader, true);
		this.data.leaderID = (this.data.last_leader_id = pActor.data.id);
		if (pNew)
		{
			this.data.total_leaders++;
			this.leader.changeHappiness("become_leader", 0);
			this.addRuler(pActor);
		}
	}

	// Token: 0x06001E5C RID: 7772 RVA: 0x0010B6EC File Offset: 0x001098EC
	public void updateRulers()
	{
		if (this.data.past_rulers == null)
		{
			return;
		}
		if (this.data.past_rulers.Count == 0)
		{
			return;
		}
		foreach (LeaderEntry tEntry in this.data.past_rulers)
		{
			Actor tRuler = World.world.units.get(tEntry.id);
			if (!tRuler.isRekt())
			{
				tEntry.name = tRuler.name;
			}
		}
	}

	// Token: 0x06001E5D RID: 7773 RVA: 0x0010B788 File Offset: 0x00109988
	public void addRuler(Actor pActor)
	{
		CityData data = this.data;
		if (data.past_rulers == null)
		{
			data.past_rulers = new List<LeaderEntry>();
		}
		this.rulerLeft();
		List<LeaderEntry> past_rulers = this.data.past_rulers;
		LeaderEntry leaderEntry = new LeaderEntry();
		leaderEntry.id = pActor.getID();
		leaderEntry.name = pActor.name;
		Kingdom kingdom = pActor.kingdom;
		leaderEntry.color_id = ((kingdom != null) ? kingdom.data.color_id : -1);
		leaderEntry.timestamp_ago = World.world.getCurWorldTime();
		past_rulers.Add(leaderEntry);
		if (this.data.past_rulers.Count > 30)
		{
			this.data.past_rulers.Shift<LeaderEntry>();
		}
	}

	// Token: 0x06001E5E RID: 7774 RVA: 0x0010B834 File Offset: 0x00109A34
	public void rulerLeft()
	{
		if (this.data.past_rulers == null)
		{
			return;
		}
		if (this.data.past_rulers.Count == 0)
		{
			return;
		}
		LeaderEntry tLast = this.data.past_rulers.Last<LeaderEntry>();
		if (tLast.timestamp_end >= tLast.timestamp_ago)
		{
			return;
		}
		tLast.timestamp_end = World.world.getCurWorldTime();
		this.updateRulers();
	}

	// Token: 0x06001E5F RID: 7775 RVA: 0x0010B898 File Offset: 0x00109A98
	public static bool nearbyBorders(City pA, City pB)
	{
		City tSmallest;
		City tCheck;
		if (pA.zones.Count > pB.zones.Count)
		{
			tSmallest = pB;
			tCheck = pA;
		}
		else
		{
			tSmallest = pA;
			tCheck = pB;
		}
		for (int i = 0; i < tSmallest.zones.Count; i++)
		{
			TileZone[] tNeighbours = tSmallest.zones[i].neighbours_all;
			for (int j = 0; j < tNeighbours.Length; j++)
			{
				if (tNeighbours[j].city == tCheck)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001E60 RID: 7776 RVA: 0x0010B910 File Offset: 0x00109B10
	public static bool giveItem(Actor pActor, List<long> pItems, City pCity)
	{
		if (pItems.Count == 0)
		{
			return false;
		}
		if (!pActor.understandsHowToUseItems())
		{
			return false;
		}
		long tItemID = pItems.GetRandom<long>();
		Item tNewItem = World.world.items.get(tItemID);
		EquipmentAsset tAsset = tNewItem.getAsset();
		ActorEquipmentSlot tActorSlot = pActor.equipment.getSlot(tAsset.equipment_type);
		if (!tActorSlot.isEmpty())
		{
			int tCurItemValue = tActorSlot.getItem().getValue();
			if (tNewItem.getValue() <= tCurItemValue)
			{
				return false;
			}
		}
		Item tPrevItem = null;
		if (!tActorSlot.isEmpty())
		{
			tPrevItem = tActorSlot.getItem();
			tActorSlot.takeAwayItem();
		}
		pItems.Remove(tItemID);
		tActorSlot.setItem(tNewItem, pActor);
		pActor.setStatsDirty();
		if (tPrevItem != null)
		{
			pCity.data.equipment.addItem(pCity, tPrevItem, pItems);
		}
		pCity._storage_version++;
		return true;
	}

	// Token: 0x06001E61 RID: 7777 RVA: 0x0010B9DC File Offset: 0x00109BDC
	public int getLimitOfBuildingsType(BuildOrder pElement)
	{
		int tResult = pElement.limit_type;
		if (this.hasCulture())
		{
			string type = pElement.getBuildingAsset(this, null).type;
			if (!(type == "type_statue"))
			{
				if (type == "type_watch_tower")
				{
					if (this.culture.hasTrait("tower_lovers"))
					{
						tResult += CultureTraitLibrary.getValue("tower_lovers");
					}
					if (this.hasLeader())
					{
						tResult += (int)this.leader.stats["bonus_towers"];
					}
				}
			}
			else if (this.culture.hasTrait("statue_lovers"))
			{
				tResult += CultureTraitLibrary.getValue("statue_lovers");
			}
		}
		return tResult;
	}

	// Token: 0x06001E62 RID: 7778 RVA: 0x0010BA87 File Offset: 0x00109C87
	public Alliance getAlliance()
	{
		return this.kingdom.getAlliance();
	}

	// Token: 0x06001E63 RID: 7779 RVA: 0x0010BA94 File Offset: 0x00109C94
	public Clan getRoyalClan()
	{
		Clan tClan = null;
		if (tClan == null && this.hasLeader())
		{
			tClan = this.leader.clan;
		}
		if (tClan == null && this.kingdom.hasKing())
		{
			tClan = this.kingdom.king.clan;
		}
		return tClan;
	}

	// Token: 0x06001E64 RID: 7780 RVA: 0x0010BADC File Offset: 0x00109CDC
	public bool isOkToSendArmy()
	{
		if (!this.hasArmy())
		{
			return false;
		}
		float tMaxArmy = (float)this.getMaxWarriors();
		return (float)this.army.countUnits() / tMaxArmy >= 0.7f;
	}

	// Token: 0x06001E65 RID: 7781 RVA: 0x0010BB14 File Offset: 0x00109D14
	public void tryToPutItem(Item pItem)
	{
		List<long> tCityItemList = this.data.equipment.getEquipmentList(pItem.getAsset().equipment_type);
		if (tCityItemList.Count >= this.status.maximum_items)
		{
			this.tryToPutItemInStorage(pItem);
			return;
		}
		this.data.equipment.addItem(this, pItem, tCityItemList);
		this._storage_version++;
	}

	// Token: 0x06001E66 RID: 7782 RVA: 0x0010BB7C File Offset: 0x00109D7C
	public void tryToPutItems(IEnumerable<Item> pItems)
	{
		foreach (Item tItem in pItems)
		{
			this.tryToPutItem(tItem);
		}
	}

	// Token: 0x06001E67 RID: 7783 RVA: 0x0010BBC4 File Offset: 0x00109DC4
	private void tryToPutItemInStorage(Item pNewItem)
	{
		float tNewItemValue = (float)pNewItem.getValue();
		EquipmentType tListType = pNewItem.getAsset().equipment_type;
		List<long> tCityItemList = this.data.equipment.getEquipmentList(tListType);
		for (int i = 0; i < tCityItemList.Count; i++)
		{
			long tID = tCityItemList[i];
			Item tOldCityItem = World.world.items.get(tID);
			float tCurValue = (float)tOldCityItem.getValue();
			if (tNewItemValue > tCurValue)
			{
				tOldCityItem.clearCity();
				tCityItemList[i] = pNewItem.id;
				pNewItem.setInCityStorage(this);
				this._storage_version++;
				return;
			}
		}
	}

	// Token: 0x06001E68 RID: 7784 RVA: 0x0010BC5D File Offset: 0x00109E5D
	public int getZoneRange(bool pAllowCheat = true)
	{
		if (pAllowCheat && DebugConfig.isOn(DebugOption.CityUnlimitedZoneRange))
		{
			return 999;
		}
		return 13;
	}

	// Token: 0x06001E69 RID: 7785 RVA: 0x0010BC78 File Offset: 0x00109E78
	public bool reachableFrom(City pCity)
	{
		WorldTile tTile = this.getTile(false);
		if (tTile == null)
		{
			return false;
		}
		WorldTile tTile2 = pCity.getTile(false);
		return tTile2 != null && tTile.reachableFrom(tTile2);
	}

	// Token: 0x06001E6A RID: 7786 RVA: 0x0010BCA6 File Offset: 0x00109EA6
	public bool hasLeader()
	{
		if (this.leader == null)
		{
			return false;
		}
		if (!this.leader.isAlive())
		{
			this.removeLeader();
			return false;
		}
		return true;
	}

	// Token: 0x06001E6B RID: 7787 RVA: 0x0010BCC8 File Offset: 0x00109EC8
	public override void convertSameSpeciesAroundUnit(Actor pActorMain, bool pOverride = false)
	{
		foreach (Actor tActor in Finder.getUnitsFromChunk(pActorMain.current_tile, 2, 0f, false))
		{
			if (!tActor.hasCity() && !tActor.isKingdomCiv() && tActor.isSameSpecies(pActorMain) && tActor.isSapient())
			{
				tActor.joinCity(this);
			}
		}
	}

	// Token: 0x06001E6C RID: 7788 RVA: 0x0010BD44 File Offset: 0x00109F44
	public override void forceConvertSameSpeciesAroundUnit(Actor pActorMain)
	{
		this.convertSameSpeciesAroundUnit(pActorMain, true);
	}

	// Token: 0x06001E6D RID: 7789 RVA: 0x0010BD4E File Offset: 0x00109F4E
	public void setUnitMetas(Actor pActor)
	{
		if (pActor.hasCulture())
		{
			this.setCulture(pActor.culture);
		}
		if (pActor.hasLanguage())
		{
			this.setLanguage(pActor.language);
		}
		if (pActor.hasReligion())
		{
			this.setReligion(pActor.religion);
		}
	}

	// Token: 0x06001E6E RID: 7790 RVA: 0x0010BD8C File Offset: 0x00109F8C
	public override void save()
	{
		base.save();
		if (this.hasCulture())
		{
			this.data.id_culture = this.culture.id;
		}
		if (this.hasReligion())
		{
			this.data.id_religion = this.religion.id;
		}
		if (this.hasLanguage())
		{
			this.data.id_language = this.language.id;
		}
		if (this.kingdom == null)
		{
			this.data.kingdomID = -1L;
		}
		else
		{
			this.data.kingdomID = this.kingdom.id;
		}
		this.data.zones.Clear();
		foreach (TileZone tZone in this.zones)
		{
			ZoneData tZoneData = new ZoneData
			{
				x = tZone.x,
				y = tZone.y
			};
			this.data.zones.Add(tZoneData);
		}
	}

	// Token: 0x06001E6F RID: 7791 RVA: 0x0010BEA4 File Offset: 0x0010A0A4
	public bool hasCulture()
	{
		if (this.culture != null && !this.culture.isAlive())
		{
			this.setCulture(null);
		}
		return this.culture != null;
	}

	// Token: 0x06001E70 RID: 7792 RVA: 0x0010BECB File Offset: 0x0010A0CB
	public bool hasLanguage()
	{
		if (this.language != null && !this.language.isAlive())
		{
			this.setLanguage(null);
		}
		return this.language != null;
	}

	// Token: 0x06001E71 RID: 7793 RVA: 0x0010BEF2 File Offset: 0x0010A0F2
	internal void setLanguage(Language pLanguage)
	{
		if (this.language == pLanguage)
		{
			return;
		}
		this.language = pLanguage;
		World.world.languages.setDirtyCities();
	}

	// Token: 0x06001E72 RID: 7794 RVA: 0x0010BF14 File Offset: 0x0010A114
	internal void setReligion(Religion pReligion)
	{
		if (this.religion == pReligion)
		{
			return;
		}
		this.religion = pReligion;
		World.world.religions.setDirtyCities();
	}

	// Token: 0x06001E73 RID: 7795 RVA: 0x0010BF36 File Offset: 0x0010A136
	public Subspecies getMainSubspecies()
	{
		if (this.hasLeader())
		{
			return this.leader.subspecies;
		}
		if (this.getPopulationPeople() == 0)
		{
			return null;
		}
		return base.units[0].subspecies;
	}

	// Token: 0x06001E74 RID: 7796 RVA: 0x0010BF67 File Offset: 0x0010A167
	public bool hasReligion()
	{
		if (this.religion != null && !this.religion.isAlive())
		{
			this.setReligion(null);
		}
		return this.religion != null;
	}

	// Token: 0x06001E75 RID: 7797 RVA: 0x0010BF8E File Offset: 0x0010A18E
	public bool hasStockpiles()
	{
		return this.stockpiles.Count > 0;
	}

	// Token: 0x06001E76 RID: 7798 RVA: 0x0010BF9E File Offset: 0x0010A19E
	public bool hasStorages()
	{
		return this.storages.Count > 0;
	}

	// Token: 0x06001E77 RID: 7799 RVA: 0x0010BFB0 File Offset: 0x0010A1B0
	public Building getRandomStockpile()
	{
		if (!this.hasStockpiles())
		{
			return null;
		}
		foreach (Building tStockpile in this.stockpiles.LoopRandom<Building>())
		{
			if (tStockpile.isUsable())
			{
				return tStockpile;
			}
		}
		return null;
	}

	// Token: 0x06001E78 RID: 7800 RVA: 0x0010C014 File Offset: 0x0010A214
	public void takeResource(string pResourceID, int pAmount)
	{
		if (!this.hasStorages())
		{
			return;
		}
		int tLeftToTake = pAmount;
		foreach (Building tStorage in this.storages)
		{
			if (tStorage.isUsable())
			{
				int tAmountCanTake;
				if (tStorage.getResourcesAmount(pResourceID) >= tLeftToTake)
				{
					tAmountCanTake = tLeftToTake;
				}
				else
				{
					tAmountCanTake = tStorage.getResourcesAmount(pResourceID);
				}
				tStorage.takeResource(pResourceID, tAmountCanTake);
				tLeftToTake -= tAmountCanTake;
				if (tLeftToTake == 0)
				{
					break;
				}
			}
		}
		this._storage_version++;
	}

	// Token: 0x06001E79 RID: 7801 RVA: 0x0010C0AC File Offset: 0x0010A2AC
	public int getResourcesAmount(string pResourceID)
	{
		if (!this.hasStorages())
		{
			return 0;
		}
		int tResult = 0;
		foreach (Building tStorage in this.storages)
		{
			if (tStorage.isUsable())
			{
				tResult += tStorage.getResourcesAmount(pResourceID);
			}
		}
		return tResult;
	}

	// Token: 0x06001E7A RID: 7802 RVA: 0x0010C118 File Offset: 0x0010A318
	public int addResourcesToRandomStockpile(string pResourceID, int pAmount = 1)
	{
		Building tStockpile = this.getRandomStockpile();
		if (tStockpile == null)
		{
			return 0;
		}
		this._storage_version++;
		return tStockpile.addResources(pResourceID, pAmount);
	}

	// Token: 0x06001E7B RID: 7803 RVA: 0x0010C148 File Offset: 0x0010A348
	public bool hasSpaceForResourceInStockpile(ResourceAsset pResourceAsset)
	{
		if (!this.hasStockpiles())
		{
			return false;
		}
		foreach (Building tStockpile in this.stockpiles)
		{
			if (tStockpile.isUsable() && tStockpile.hasSpaceForResource(pResourceAsset))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001E7C RID: 7804 RVA: 0x0010C1B8 File Offset: 0x0010A3B8
	public bool hasResourcesForNewItems()
	{
		if (!this.hasStorages())
		{
			return false;
		}
		foreach (Building tStorage in this.storages)
		{
			if (tStorage.isUsable() && tStorage.hasResourcesForNewItems())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001E7D RID: 7805 RVA: 0x0010C228 File Offset: 0x0010A428
	public ResourceAsset getRandomSuitableFood(Subspecies pSubspecies)
	{
		if (!this.hasStorages())
		{
			return null;
		}
		foreach (Building tStorage in this.storages)
		{
			if (tStorage.isUsable())
			{
				ResourceAsset tAsset = tStorage.getRandomSuitableFood(pSubspecies, null);
				if (tAsset != null)
				{
					return tAsset;
				}
			}
		}
		return null;
	}

	// Token: 0x06001E7E RID: 7806 RVA: 0x0010C29C File Offset: 0x0010A49C
	public int countFood()
	{
		if (!this.hasStorages())
		{
			return 0;
		}
		int tResult = 0;
		foreach (Building tStorage in this.storages)
		{
			if (tStorage.isUsable())
			{
				tResult += tStorage.countFood();
			}
		}
		return tResult;
	}

	// Token: 0x06001E7F RID: 7807 RVA: 0x0010C308 File Offset: 0x0010A508
	public ListPool<CityStorageSlot> getTotalResourceSlots(ResType[] pResTypes)
	{
		foreach (CityStorageSlot tSlot in this._total_resource_slots.Values)
		{
			ResourceAsset tAsset = tSlot.asset;
			if (pResTypes.IndexOf(tAsset.type) != -1)
			{
				tSlot.amount = 0;
			}
		}
		foreach (Building tBuilding in this.storages)
		{
			if (tBuilding.isUsable())
			{
				foreach (CityStorageSlot tSlot2 in tBuilding.resources.getSlots())
				{
					CityStorageSlot tTotalSlot;
					this._total_resource_slots.TryGetValue(tSlot2.id, out tTotalSlot);
					if (tTotalSlot == null)
					{
						tTotalSlot = new CityStorageSlot(tSlot2.id);
						this._total_resource_slots[tSlot2.id] = tTotalSlot;
					}
					tTotalSlot.amount += tSlot2.amount;
				}
			}
		}
		ListPool<CityStorageSlot> tResult = new ListPool<CityStorageSlot>(this._total_resource_slots.Count);
		foreach (CityStorageSlot tSlot3 in this._total_resource_slots.Values)
		{
			ResourceAsset tAsset2 = tSlot3.asset;
			if (pResTypes.IndexOf(tAsset2.type) != -1 && tSlot3.amount != 0)
			{
				tResult.Add(tSlot3);
			}
		}
		tResult.Sort((CityStorageSlot a, CityStorageSlot b) => a.asset.order.CompareTo(b.asset.order));
		return tResult;
	}

	// Token: 0x06001E80 RID: 7808 RVA: 0x0010C4F8 File Offset: 0x0010A6F8
	public bool hasKingdom()
	{
		return this.kingdom != null;
	}

	// Token: 0x06001E81 RID: 7809 RVA: 0x0010C503 File Offset: 0x0010A703
	public float getTimerForNewWarrior()
	{
		return this._timer_warrior;
	}

	// Token: 0x06001E82 RID: 7810 RVA: 0x0010C50B File Offset: 0x0010A70B
	public List<long> getEquipmentList(EquipmentType pType)
	{
		return this.data.equipment.getEquipmentList(pType);
	}

	// Token: 0x06001E83 RID: 7811 RVA: 0x0010C51E File Offset: 0x0010A71E
	public bool planAllowsToPlaceBuildingInZone(TileZone pZone, TileZone pCenterZone)
	{
		return (this.status.housing_total < 10 && this.zones.Count < 20) || this.culture.planAllowsToPlaceBuildingInZone(pZone, pCenterZone);
	}

	// Token: 0x06001E84 RID: 7812 RVA: 0x0010C54D File Offset: 0x0010A74D
	public bool hasSpecialTownPlans()
	{
		return this.hasCulture() && this.culture.hasSpecialTownPlans();
	}

	// Token: 0x06001E85 RID: 7813 RVA: 0x0010C564 File Offset: 0x0010A764
	public bool isNeutral()
	{
		return this.kingdom.isNeutral();
	}

	// Token: 0x06001E86 RID: 7814 RVA: 0x0010C574 File Offset: 0x0010A774
	public bool isWelcomedToJoin(Actor pActor)
	{
		if (pActor.kingdom == this.kingdom)
		{
			return true;
		}
		if (pActor.isSameSubspecies(this.getMainSubspecies()))
		{
			return true;
		}
		if (!this.hasCulture())
		{
			return false;
		}
		if (this.culture.hasTrait("xenophobic"))
		{
			return false;
		}
		if (pActor.hasCultureTrait("xenophobic"))
		{
			return false;
		}
		if (this.culture.hasTrait("xenophiles"))
		{
			if (!pActor.hasCulture())
			{
				return true;
			}
			if (pActor.hasCultureTrait("xenophiles"))
			{
				return true;
			}
		}
		return this.isSameSpeciesAsActor(pActor);
	}

	// Token: 0x06001E87 RID: 7815 RVA: 0x0010C605 File Offset: 0x0010A805
	public bool isSameSpeciesAsActor(Actor pActor)
	{
		return pActor.isSameSpecies(this.getCurrentSpecies());
	}

	// Token: 0x06001E88 RID: 7816 RVA: 0x0010C618 File Offset: 0x0010A818
	public string getCurrentSpecies()
	{
		Subspecies tMainSubspecies = this.getMainSubspecies();
		if (tMainSubspecies != null)
		{
			return tMainSubspecies.getActorAsset().id;
		}
		return this.getActorAsset().id;
	}

	// Token: 0x06001E89 RID: 7817 RVA: 0x0010C648 File Offset: 0x0010A848
	public Sprite getCurrentSpeciesIcon()
	{
		Subspecies tMainSubspecies = this.getMainSubspecies();
		if (tMainSubspecies != null)
		{
			return tMainSubspecies.getSpriteIcon();
		}
		return this.getActorAsset().getSpriteIcon();
	}

	// Token: 0x06001E8A RID: 7818 RVA: 0x0010C674 File Offset: 0x0010A874
	public bool hasTransportBoats()
	{
		using (List<Actor>.Enumerator enumerator = this._boats.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.asset.is_boat_transport)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001E8B RID: 7819 RVA: 0x0010C6D4 File Offset: 0x0010A8D4
	public bool isCityUnderDangerFire()
	{
		return this.tasks.fire > 0;
	}

	// Token: 0x06001E8C RID: 7820 RVA: 0x0010C6E4 File Offset: 0x0010A8E4
	public bool isPossibleToJoin(Actor pActor)
	{
		if (this == pActor.city)
		{
			return false;
		}
		if (this.isNeutral())
		{
			return true;
		}
		if (!this.isWelcomedToJoin(pActor))
		{
			return false;
		}
		if (pActor.city != null)
		{
			if (pActor.isKing())
			{
				return false;
			}
			if (pActor.isCityLeader())
			{
				return false;
			}
			if (pActor.city.getPopulationPeople() < this.getPopulationPeople())
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06001E8D RID: 7821 RVA: 0x0010C744 File Offset: 0x0010A944
	public override string ToString()
	{
		if (this.data == null)
		{
			return "[City is null]";
		}
		string result;
		using (StringBuilderPool tBuilder = new StringBuilderPool())
		{
			tBuilder.Append(string.Format("[City:{0} ", base.id));
			if (!base.isAlive())
			{
				tBuilder.Append("[DEAD] ");
			}
			tBuilder.Append("\"" + this.name + "\" ");
			StringBuilderPool stringBuilderPool = tBuilder;
			string format = "Kingdom:{0} ";
			Kingdom kingdom = this.kingdom;
			stringBuilderPool.Append(string.Format(format, (kingdom != null) ? kingdom.id : -1L));
			if (this.hasArmy())
			{
				tBuilder.Append(string.Format("Army:{0} ", this.army.id));
			}
			tBuilder.Append(string.Format("Units:{0} ", base.units.Count));
			if (base.isDirtyUnits())
			{
				tBuilder.Append("[Dirty] ");
			}
			if (!this.leader.isRekt())
			{
				tBuilder.Append(string.Format("Leader:{0} ", this.leader.id));
			}
			Kingdom kingdom2 = this.kingdom;
			City city;
			if (kingdom2 == null)
			{
				city = null;
			}
			else
			{
				Actor king = kingdom2.king;
				city = ((king != null) ? king.city : null);
			}
			if (city == this)
			{
				tBuilder.Append(string.Format("King:{0} ", this.kingdom.king.id));
			}
			result = tBuilder.ToString().Trim() + "]";
		}
		return result;
	}

	// Token: 0x0400162B RID: 5675
	private static readonly HashSet<City> _connected_checked = new HashSet<City>();

	// Token: 0x0400162C RID: 5676
	private static readonly HashSet<City> _connected_next_wave = new HashSet<City>();

	// Token: 0x0400162D RID: 5677
	private static readonly HashSet<City> _connected_current_wave = new HashSet<City>();

	// Token: 0x0400162E RID: 5678
	private readonly Dictionary<string, CityStorageSlot> _total_resource_slots = new Dictionary<string, CityStorageSlot>();

	// Token: 0x0400162F RID: 5679
	private readonly Dictionary<UnitProfession, List<Actor>> _professions_dict = new Dictionary<UnitProfession, List<Actor>>();

	// Token: 0x04001630 RID: 5680
	private readonly List<Actor> _boats = new List<Actor>();

	// Token: 0x04001631 RID: 5681
	private readonly Dictionary<string, long> _species = new Dictionary<string, long>();

	// Token: 0x04001632 RID: 5682
	public readonly List<Building> buildings = new List<Building>();

	// Token: 0x04001633 RID: 5683
	public readonly Dictionary<string, List<Building>> buildings_dict_type = new Dictionary<string, List<Building>>();

	// Token: 0x04001634 RID: 5684
	public readonly Dictionary<string, List<Building>> buildings_dict_id = new Dictionary<string, List<Building>>();

	// Token: 0x04001635 RID: 5685
	public readonly CityTasksData tasks = new CityTasksData();

	// Token: 0x04001636 RID: 5686
	public readonly CitizenJobs jobs = new CitizenJobs();

	// Token: 0x04001637 RID: 5687
	public readonly CityStatus status = new CityStatus();

	// Token: 0x04001638 RID: 5688
	public float mark_scale_effect;

	// Token: 0x04001639 RID: 5689
	[NonSerialized]
	internal Kingdom kingdom;

	// Token: 0x0400163A RID: 5690
	public Culture culture;

	// Token: 0x0400163B RID: 5691
	public Language language;

	// Token: 0x0400163C RID: 5692
	public Religion religion;

	// Token: 0x0400163D RID: 5693
	public Actor leader;

	// Token: 0x0400163E RID: 5694
	public Army army;

	// Token: 0x0400163F RID: 5695
	internal readonly List<TileZone> zones = new List<TileZone>();

	// Token: 0x04001640 RID: 5696
	internal readonly HashSet<TileZone> neighbour_zones = new HashSet<TileZone>();

	// Token: 0x04001641 RID: 5697
	internal readonly HashSet<TileZone> border_zones = new HashSet<TileZone>();

	// Token: 0x04001642 RID: 5698
	internal readonly HashSet<City> neighbours_cities = new HashSet<City>();

	// Token: 0x04001643 RID: 5699
	internal readonly HashSet<City> neighbours_cities_kingdom = new HashSet<City>();

	// Token: 0x04001644 RID: 5700
	internal readonly HashSet<Kingdom> neighbours_kingdoms = new HashSet<Kingdom>();

	// Token: 0x04001645 RID: 5701
	internal Building under_construction_building;

	// Token: 0x04001646 RID: 5702
	internal readonly List<Building> stockpiles = new List<Building>();

	// Token: 0x04001647 RID: 5703
	internal readonly List<Building> storages = new List<Building>();

	// Token: 0x04001648 RID: 5704
	internal float timer_build_boat;

	// Token: 0x04001649 RID: 5705
	internal float timer_build;

	// Token: 0x0400164A RID: 5706
	public float timer_action;

	// Token: 0x0400164B RID: 5707
	private float _timer_capture;

	// Token: 0x0400164C RID: 5708
	private float _timer_warrior;

	// Token: 0x0400164D RID: 5709
	internal readonly List<WorldTile> road_tiles_to_build = new List<WorldTile>();

	// Token: 0x0400164E RID: 5710
	private readonly List<WorldTile> tiles_to_remove = new List<WorldTile>();

	// Token: 0x0400164F RID: 5711
	internal TileZone target_attack_zone;

	// Token: 0x04001650 RID: 5712
	internal City target_attack_city;

	// Token: 0x04001651 RID: 5713
	internal WorldTile _city_tile;

	// Token: 0x04001652 RID: 5714
	internal string _debug_last_possible_build_orders;

	// Token: 0x04001653 RID: 5715
	internal string _debug_last_possible_build_orders_no_resources;

	// Token: 0x04001654 RID: 5716
	internal string _debug_last_build_order_try;

	// Token: 0x04001655 RID: 5717
	internal Kingdom being_captured_by;

	// Token: 0x04001656 RID: 5718
	private float _capture_ticks;

	// Token: 0x04001657 RID: 5719
	public int last_visual_capture_ticks;

	// Token: 0x04001658 RID: 5720
	private bool _dirty_citizens;

	// Token: 0x04001659 RID: 5721
	private bool _dirty_city_status;

	// Token: 0x0400165A RID: 5722
	private bool _dirty_abandoned_zones;

	// Token: 0x0400165B RID: 5723
	internal Vector2 city_center;

	// Token: 0x0400165C RID: 5724
	internal Vector2 last_city_center;

	// Token: 0x0400165D RID: 5725
	public readonly WorldTileContainer calculated_place_for_farms = new WorldTileContainer();

	// Token: 0x0400165E RID: 5726
	public readonly WorldTileContainer calculated_farm_fields = new WorldTileContainer();

	// Token: 0x0400165F RID: 5727
	public readonly WorldTileContainer calculated_crops = new WorldTileContainer();

	// Token: 0x04001660 RID: 5728
	public readonly WorldTileContainer calculated_grown_wheat = new WorldTileContainer();

	// Token: 0x04001661 RID: 5729
	private readonly Dictionary<Kingdom, int> _capturing_units = new Dictionary<Kingdom, int>();

	// Token: 0x04001662 RID: 5730
	internal readonly HashSet<TileZone> danger_zones = new HashSet<TileZone>();

	// Token: 0x04001663 RID: 5731
	public AiSystemCity ai;

	// Token: 0x04001664 RID: 5732
	private int _current_total_food;

	// Token: 0x04001665 RID: 5733
	private int _last_checked_job_id;

	// Token: 0x04001666 RID: 5734
	private double _loyalty_last_time;

	// Token: 0x04001667 RID: 5735
	private int _loyalty_cached;

	// Token: 0x04001668 RID: 5736
	private readonly List<long> _cached_book_ids = new List<long>();

	// Token: 0x04001669 RID: 5737
	private readonly List<Building> _cached_buildings_with_book_slots = new List<Building>();

	// Token: 0x0400166A RID: 5738
	public double timestamp_shrink;

	// Token: 0x0400166B RID: 5739
	private int _storage_version;
}
