using System;
using System.Collections.Generic;
using db;
using JetBrains.Annotations;
using SQLite;

// Token: 0x02000282 RID: 642
public class KingdomManager : MetaSystemManager<Kingdom, KingdomData>
{
	// Token: 0x0600189A RID: 6298 RVA: 0x000EB605 File Offset: 0x000E9805
	public KingdomManager()
	{
		this.type_id = "kingdom";
	}

	// Token: 0x0600189B RID: 6299 RVA: 0x000EB634 File Offset: 0x000E9834
	public Kingdom makeNewCivKingdom(Actor pActor, string pID = null, bool pLog = true)
	{
		World.world.game_stats.data.kingdomsCreated += 1L;
		World.world.map_stats.kingdomsCreated += 1L;
		Kingdom tKingdom = base.newObject();
		tKingdom.newCivKingdom(pActor);
		pActor.stopBeingWarrior();
		pActor.joinKingdom(tKingdom);
		tKingdom.setKing(pActor, false);
		tKingdom.location = pActor.current_position;
		if (pLog)
		{
			WorldLog.logNewKingdom(tKingdom);
		}
		return tKingdom;
	}

	// Token: 0x0600189C RID: 6300 RVA: 0x000EB6B4 File Offset: 0x000E98B4
	protected override void addObject(Kingdom pObject)
	{
		base.addObject(pObject);
		ZoneCalculator zone_calculator = World.world.zone_calculator;
		if (zone_calculator != null)
		{
			zone_calculator.setDrawnZonesDirty();
		}
		pObject.createAI();
	}

	// Token: 0x0600189D RID: 6301 RVA: 0x000EB6D8 File Offset: 0x000E98D8
	public override void removeObject(Kingdom pKingdom)
	{
		World.world.game_stats.data.kingdomsDestroyed += 1L;
		World.world.map_stats.kingdomsDestroyed += 1L;
		WorldLog.logKingdomDestroyed(pKingdom);
		World.world.diplomacy.removeRelationsFor(pKingdom);
		if (World.world.isSelectedPower("relations") && SelectedMetas.selected_kingdom == pKingdom)
		{
			World.world.selected_buttons.unselectAll();
		}
		if (Config.whisper_A == pKingdom)
		{
			Config.whisper_A = null;
		}
		if (Config.unity_A == pKingdom)
		{
			Config.unity_A = null;
		}
		pKingdom.makeSurvivorsToNomads();
		World.world.zone_calculator.setDrawnZonesDirty();
		using (ListPool<War> tWarList = new ListPool<War>(pKingdom.getWars(false)))
		{
			foreach (War ptr in tWarList)
			{
				ptr.removeFromWar(pKingdom, false);
			}
			Alliance tAlliance = pKingdom.getAlliance();
			if (tAlliance != null)
			{
				tAlliance.leave(pKingdom, true);
			}
			World.world.cultures.setDirtyKingdoms();
			World.world.languages.setDirtyKingdoms();
			World.world.religions.setDirtyKingdoms();
			base.removeObject(pKingdom);
			DBInserter.insertData(pKingdom.data, this.type_id);
		}
	}

	// Token: 0x0600189E RID: 6302 RVA: 0x000EB848 File Offset: 0x000E9A48
	public Kingdom getCivOrWildViaID(long pID)
	{
		if (pID < 0L)
		{
			return World.world.kingdoms_wild.get(pID);
		}
		return World.world.kingdoms.get(pID);
	}

	// Token: 0x0600189F RID: 6303 RVA: 0x000EB870 File Offset: 0x000E9A70
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		foreach (Kingdom kingdom in this)
		{
			kingdom.clearCursorOver();
		}
		if (World.world.isPaused())
		{
			return;
		}
		this.updateCivKingdoms(pElapsed);
	}

	// Token: 0x060018A0 RID: 6304 RVA: 0x000EB8D0 File Offset: 0x000E9AD0
	private void updateCivKingdoms(float pElapsed)
	{
		int i = 0;
		int tLength = this.list.Count;
		while (i < tLength)
		{
			this.list[i].updateCiv(pElapsed);
			i++;
		}
	}

	// Token: 0x060018A1 RID: 6305 RVA: 0x000EB908 File Offset: 0x000E9B08
	public void updateAge()
	{
		foreach (Kingdom kingdom in this)
		{
			kingdom.updateAge();
		}
	}

	// Token: 0x060018A2 RID: 6306 RVA: 0x000EB950 File Offset: 0x000E9B50
	[CanBeNull]
	public DeadKingdom db_get(long pID)
	{
		if (Config.disable_db)
		{
			return null;
		}
		DeadKingdom tDeadKingdom;
		if (this._dead_kingdoms.TryGetValue(pID, out tDeadKingdom))
		{
			return tDeadKingdom;
		}
		SQLiteConnectionWithLock tDBConn = DBManager.getSyncConnection();
		DeadKingdom result;
		using (tDBConn.Lock())
		{
			KingdomData tData = tDBConn.Find<KingdomData>(pID);
			if (tData == null)
			{
				result = null;
			}
			else
			{
				tData.from_db = true;
				tDeadKingdom = new DeadKingdom();
				tDeadKingdom.loadData(tData);
				this._dead_kingdoms[pID] = tDeadKingdom;
				result = tDeadKingdom;
			}
		}
		return result;
	}

	// Token: 0x060018A3 RID: 6307 RVA: 0x000EB9E0 File Offset: 0x000E9BE0
	public override void clear()
	{
		foreach (DeadKingdom deadKingdom in this._dead_kingdoms.Values)
		{
			deadKingdom.Dispose();
		}
		this._dead_kingdoms.Clear();
		base.clear();
	}

	// Token: 0x060018A4 RID: 6308 RVA: 0x000EBA48 File Offset: 0x000E9C48
	public override bool isLocked()
	{
		return this.isUnitsDirty() || this._dirty_cities;
	}

	// Token: 0x060018A5 RID: 6309 RVA: 0x000EBA60 File Offset: 0x000E9C60
	protected override void updateDirtyUnits()
	{
		for (int i = 0; i < World.world.units.units_only_dying.Count; i++)
		{
			World.world.units.units_only_dying[i].kingdom.preserveAlive();
		}
		List<Actor> tActorList = World.world.units.units_only_civ;
		for (int j = 0; j < tActorList.Count; j++)
		{
			Actor tUnit = tActorList[j];
			if (tUnit.kingdom.isDirtyUnits())
			{
				tUnit.kingdom.listUnit(tUnit);
			}
		}
	}

	// Token: 0x060018A6 RID: 6310 RVA: 0x000EBAED File Offset: 0x000E9CED
	public void beginChecksCities()
	{
		if (this._dirty_cities)
		{
			this.updateDirtyCities();
		}
		this._dirty_cities = false;
	}

	// Token: 0x060018A7 RID: 6311 RVA: 0x000EBB04 File Offset: 0x000E9D04
	public void updateDirtyCities()
	{
		this.clearAllCitiesLists();
		foreach (City tCity in World.world.cities)
		{
			tCity.kingdom.listCity(tCity);
		}
	}

	// Token: 0x060018A8 RID: 6312 RVA: 0x000EBB60 File Offset: 0x000E9D60
	public void clearAllCitiesLists()
	{
		foreach (Kingdom kingdom in this)
		{
			kingdom.clearListCities();
		}
		WildKingdomsManager.neutral.clearListCities();
	}

	// Token: 0x060018A9 RID: 6313 RVA: 0x000EBBB0 File Offset: 0x000E9DB0
	public bool hasDirtyCities()
	{
		return this._dirty_cities;
	}

	// Token: 0x060018AA RID: 6314 RVA: 0x000EBBB8 File Offset: 0x000E9DB8
	public void setDirtyCities()
	{
		this._dirty_cities = true;
	}

	// Token: 0x060018AB RID: 6315 RVA: 0x000EBBC1 File Offset: 0x000E9DC1
	public void beginChecksBuildings()
	{
		if (this._dirty_buildings)
		{
			this.updateDirtyBuildings();
		}
		this._dirty_buildings = false;
	}

	// Token: 0x060018AC RID: 6316 RVA: 0x000EBBD8 File Offset: 0x000E9DD8
	private void updateDirtyBuildings()
	{
		this.clearAllBuildingLists();
		foreach (City tCity in World.world.cities)
		{
			if (!tCity.kingdom.wild)
			{
				tCity.kingdom.addBuildings(tCity.buildings);
			}
		}
	}

	// Token: 0x060018AD RID: 6317 RVA: 0x000EBC48 File Offset: 0x000E9E48
	public void setDirtyBuildings()
	{
		this._dirty_buildings = true;
	}

	// Token: 0x060018AE RID: 6318 RVA: 0x000EBC54 File Offset: 0x000E9E54
	private void clearAllBuildingLists()
	{
		foreach (Kingdom kingdom in this)
		{
			kingdom.clearBuildingList();
		}
	}

	// Token: 0x0400136D RID: 4973
	private bool _dirty_cities = true;

	// Token: 0x0400136E RID: 4974
	private bool _dirty_buildings = true;

	// Token: 0x0400136F RID: 4975
	protected readonly Dictionary<long, DeadKingdom> _dead_kingdoms = new Dictionary<long, DeadKingdom>();
}
