using System;
using System.Collections.Generic;

// Token: 0x0200031C RID: 796
public class CityManager : MetaSystemManager<City, CityData>
{
	// Token: 0x06001EBC RID: 7868 RVA: 0x0010D027 File Offset: 0x0010B227
	public CityManager()
	{
		this.type_id = "city";
	}

	// Token: 0x06001EBD RID: 7869 RVA: 0x0010D03C File Offset: 0x0010B23C
	public City newCity(Kingdom pKingdom, TileZone pZone, Actor pOriginalActor)
	{
		World.world.game_stats.data.citiesCreated += 1L;
		World.world.map_stats.citiesCreated += 1L;
		City tCity = base.newObject();
		tCity.data.founder_id = pOriginalActor.getID();
		tCity.data.founder_name = pOriginalActor.name;
		tCity.data.original_actor_asset = pOriginalActor.asset.id;
		tCity.data.equipment = new CityEquipment();
		tCity.setKingdom(pKingdom, false);
		tCity.addZone(pZone);
		foreach (TileZone tZone in pZone.neighbours_all)
		{
			if (tZone.city == null)
			{
				tCity.addZone(tZone);
			}
		}
		World.world.city_zone_helper.city_place_finder.setDirty();
		return tCity;
	}

	// Token: 0x06001EBE RID: 7870 RVA: 0x0010D11C File Offset: 0x0010B31C
	public City buildNewCity(Actor pActor, TileZone pZone)
	{
		Kingdom tKingdom = pActor.kingdom;
		City city = World.world.cities.newCity(tKingdom, pZone, pActor);
		city.setUnitMetas(pActor);
		city.newCityEvent(pActor);
		WorldLog.logNewCity(city);
		return city;
	}

	// Token: 0x06001EBF RID: 7871 RVA: 0x0010D156 File Offset: 0x0010B356
	public bool tryToCreateCity(Actor pActor, ListPool<Building> pBuildingList)
	{
		return !pActor.current_tile.zone.hasCity();
	}

	// Token: 0x06001EC0 RID: 7872 RVA: 0x0010D170 File Offset: 0x0010B370
	public bool canStartNewCityCivilizationHere(Actor pActor)
	{
		if (pActor.kingdom.asset.is_forced_by_trait)
		{
			return false;
		}
		if (!pActor.canBuildNewCity())
		{
			return false;
		}
		KingdomAsset tPossibleKingdomAsset = AssetManager.kingdoms.get(pActor.asset.kingdom_id_civilization);
		if (tPossibleKingdomAsset == null || !tPossibleKingdomAsset.civ)
		{
			return false;
		}
		WorldTile tActorTile = pActor.current_tile;
		TileZone tActorZone = tActorTile.zone;
		foreach (TileZone tNeighbourZone in tActorZone.neighbours)
		{
			if (tNeighbourZone.hasCity())
			{
				WorldTile tTile = tNeighbourZone.city.getTile(false);
				if (tTile != null && tTile.isSameIsland(tActorTile))
				{
					tNeighbourZone.city.addZone(tActorZone);
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06001EC1 RID: 7873 RVA: 0x0010D224 File Offset: 0x0010B424
	public City buildFirstCivilizationCity(Actor pActor)
	{
		City tNewCity = this.buildNewCity(pActor, pActor.current_zone);
		pActor.joinCity(tNewCity);
		tNewCity.setUnitMetas(pActor);
		tNewCity.convertSameSpeciesAroundUnit(pActor, false);
		return tNewCity;
	}

	// Token: 0x06001EC2 RID: 7874 RVA: 0x0010D258 File Offset: 0x0010B458
	protected override void updateDirtyUnits()
	{
		List<Actor> tActorList = World.world.units.units_only_alive;
		for (int i = 0; i < tActorList.Count; i++)
		{
			Actor tUnit = tActorList[i];
			City tCity = tUnit.city;
			if (tCity != null && tCity.isDirtyUnits())
			{
				tCity.listUnit(tUnit);
			}
		}
	}

	// Token: 0x06001EC3 RID: 7875 RVA: 0x0010D2A7 File Offset: 0x0010B4A7
	public void beginChecksBuildings()
	{
		if (this._dirty_buildings)
		{
			this.updateDirtyBuildings();
		}
		this._dirty_buildings = false;
	}

	// Token: 0x06001EC4 RID: 7876 RVA: 0x0010D2C0 File Offset: 0x0010B4C0
	private void updateDirtyBuildings()
	{
		this.clearAllBuildingLists();
		foreach (City tCity in this)
		{
			Kingdom tKingdomCiv = tCity.kingdom;
			for (int i = 0; i < tCity.zones.Count; i++)
			{
				foreach (Building tBuilding in tCity.zones[i].buildings_all)
				{
					if (tBuilding.asset.city_building && tBuilding.isUsable())
					{
						tBuilding.setKingdomCiv(tKingdomCiv);
						tCity.listBuilding(tBuilding);
					}
				}
			}
		}
	}

	// Token: 0x06001EC5 RID: 7877 RVA: 0x0010D39C File Offset: 0x0010B59C
	public void setDirtyBuildings(City pCity)
	{
		this._dirty_buildings = true;
		World.world.kingdoms.setDirtyBuildings();
	}

	// Token: 0x06001EC6 RID: 7878 RVA: 0x0010D3B4 File Offset: 0x0010B5B4
	private void clearAllBuildingLists()
	{
		foreach (City city in this)
		{
			city.clearBuildingList();
		}
	}

	// Token: 0x06001EC7 RID: 7879 RVA: 0x0010D3FC File Offset: 0x0010B5FC
	protected override void addObject(City pObject)
	{
		pObject.init();
		base.addObject(pObject);
	}

	// Token: 0x06001EC8 RID: 7880 RVA: 0x0010D40B File Offset: 0x0010B60B
	public override City loadObject(CityData pData)
	{
		City city = base.loadObject(pData);
		city.loadCity(pData);
		return city;
	}

	// Token: 0x06001EC9 RID: 7881 RVA: 0x0010D41C File Offset: 0x0010B61C
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		foreach (City city in this)
		{
			city.update(pElapsed);
			city.clearCursorOver();
		}
	}

	// Token: 0x06001ECA RID: 7882 RVA: 0x0010D470 File Offset: 0x0010B670
	public void updateAge()
	{
		foreach (City city in this)
		{
			city.updateAge();
		}
	}

	// Token: 0x06001ECB RID: 7883 RVA: 0x0010D4B8 File Offset: 0x0010B6B8
	public override List<CityData> save(List<City> pList = null)
	{
		List<CityData> tSavingList = new List<CityData>();
		foreach (City tCity in this)
		{
			tCity.save();
			tSavingList.Add(tCity.data);
		}
		return tSavingList;
	}

	// Token: 0x06001ECC RID: 7884 RVA: 0x0010D514 File Offset: 0x0010B714
	private void checkForCityErrors(SavedMap pSaveData)
	{
		List<CityData> tCityData = new List<CityData>();
		for (int i = 0; i < pSaveData.cities.Count; i++)
		{
			CityData tData = pSaveData.cities[i];
			if (tData.zones.Count != 0)
			{
				TileZone tFirstZone = World.world.zone_calculator.getZone(tData.zones[0].x, tData.zones[0].y);
				if (pSaveData.saveVersion < 7)
				{
					tFirstZone = this.findZoneViaBuilding(tData.id, pSaveData.buildings);
				}
				if (tFirstZone != null)
				{
					tCityData.Add(tData);
				}
			}
		}
		pSaveData.cities = tCityData;
	}

	// Token: 0x06001ECD RID: 7885 RVA: 0x0010D5BC File Offset: 0x0010B7BC
	public void loadCities(SavedMap pSaveData)
	{
		this.checkForCityErrors(pSaveData);
		for (int i = 0; i < pSaveData.cities.Count; i++)
		{
			CityData tData = pSaveData.cities[i];
			City tCity = this.loadObject(tData);
			if (tCity != null && pSaveData.saveVersion >= 7)
			{
				for (int j = 0; j < tData.zones.Count; j++)
				{
					ZoneData tZoneData = tData.zones[j];
					TileZone tZone = World.world.zone_calculator.getZone(tZoneData.x, tZoneData.y);
					if (tZone != null)
					{
						tCity.addZone(tZone);
					}
				}
			}
		}
	}

	// Token: 0x06001ECE RID: 7886 RVA: 0x0010D65C File Offset: 0x0010B85C
	public override void removeObject(City pObject)
	{
		World.world.game_stats.data.citiesDestroyed += 1L;
		World.world.map_stats.citiesDestroyed += 1L;
		WorldLog.logCityDestroyed(pObject);
		pObject.destroyCity();
		base.removeObject(pObject);
		World.world.city_zone_helper.city_place_finder.setDirty();
		World.world.cultures.setDirtyCities();
		World.world.kingdoms.setDirtyCities();
		World.world.languages.setDirtyCities();
		World.world.religions.setDirtyCities();
	}

	// Token: 0x06001ECF RID: 7887 RVA: 0x0010D704 File Offset: 0x0010B904
	private TileZone findZoneViaBuilding(long pID, List<BuildingData> pList)
	{
		for (int i = 0; i < pList.Count; i++)
		{
			BuildingData tData = pList[i];
			if (tData.cityID == pID)
			{
				return World.world.GetTileSimple(tData.mainX, tData.mainY).zone;
			}
		}
		return null;
	}

	// Token: 0x06001ED0 RID: 7888 RVA: 0x0010D750 File Offset: 0x0010B950
	public override bool isLocked()
	{
		return this.isUnitsDirty() || World.world.kingdoms.hasDirtyCities();
	}

	// Token: 0x04001689 RID: 5769
	private bool _dirty_buildings;
}
