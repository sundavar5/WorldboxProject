using System;
using System.Collections;
using System.Collections.Generic;
using ai.behaviours;
using UnityEngine;

// Token: 0x020003F9 RID: 1017
public class AutoCivilization
{
	// Token: 0x0600231F RID: 8991 RVA: 0x001251B8 File Offset: 0x001233B8
	public AutoCivilization()
	{
		this._tasks_building.Add(AssetManager.tasks_actor.get("try_build_building"));
		this._tasks_building.Add(AssetManager.tasks_actor.get("build_building"));
		this._tasks_building.Add(AssetManager.tasks_actor.get("build_road"));
		this._tasks_building.Add(AssetManager.tasks_actor.get("cleaning"));
		this._tasks_gathering.Add(AssetManager.tasks_actor.get("chop_trees"));
		this._tasks_gathering.Add(AssetManager.tasks_actor.get("mine_deposit"));
		this._tasks_gathering.Add(AssetManager.tasks_actor.get("mine"));
		this._tasks_gathering.Add(AssetManager.tasks_actor.get("collect_fruits"));
		this._tasks_gathering.Add(AssetManager.tasks_actor.get("collect_herbs"));
		this._tasks_gathering.Add(AssetManager.tasks_actor.get("collect_honey"));
		this._tasks_gathering.Add(AssetManager.tasks_actor.get("manure_cleaning"));
		this._tasks_gathering.Add(AssetManager.tasks_actor.get("store_resources"));
		this._tasks_farming.Add(AssetManager.tasks_actor.get("farmer_make_field"));
		this._tasks_farming.Add(AssetManager.tasks_actor.get("farmer_plant_crops"));
		this._tasks_farming.Add(AssetManager.tasks_actor.get("farmer_harvest"));
		this._tasks_farming.Add(AssetManager.tasks_actor.get("farmer_fertilize_crops"));
		this._task_take_items = AssetManager.tasks_actor.get("try_to_take_city_item");
	}

	// Token: 0x06002320 RID: 8992 RVA: 0x001253B0 File Offset: 0x001235B0
	public void makeCivilization(Actor pUnit)
	{
		if (!pUnit.isSapient())
		{
			return;
		}
		this._unit = pUnit;
		this.clear();
		if (!this._unit.buildCityAndStartCivilization())
		{
			return;
		}
		Religion tReligion = World.world.religions.newReligion(this._unit, true);
		this._unit.setReligion(tReligion);
		this._city = this._unit.current_tile.zone.city;
		this._unit.setCity(this._city);
		this._unit.createNewWeapon("flame_sword");
		this._unit.kingdom.setCapital(this._city);
		this._units_list.Add(this._unit);
		Actor tLeader = this.newUnit(this._unit.culture, this._unit.language);
		this._city.setLeader(tLeader, true);
		this.spawnUnits(this._unit.culture, this._unit.language);
		for (int i = 0; i < 50; i++)
		{
			this.makeBooks(this._unit);
		}
		this._routine = World.world.StartCoroutine(this.civilizationMakingRoutine());
	}

	// Token: 0x06002321 RID: 8993 RVA: 0x001254DD File Offset: 0x001236DD
	private void makeBooks(Actor pActor)
	{
		if (!pActor.hasLanguage())
		{
			return;
		}
		if (!pActor.hasCulture())
		{
			return;
		}
		World.world.books.generateNewBook(pActor);
	}

	// Token: 0x06002322 RID: 8994 RVA: 0x00125504 File Offset: 0x00123704
	private Actor newUnit(Culture pCulture, Language pLanguage)
	{
		Actor tNewUnit = World.world.units.spawnNewUnit(this._unit.asset.id, this._city.zones.GetRandom<TileZone>().centerTile, true, true, 3f, null, false, false);
		tNewUnit.setCity(this._city);
		tNewUnit.setSubspecies(this._unit.subspecies);
		tNewUnit.setCulture(pCulture);
		tNewUnit.joinLanguage(pLanguage);
		this._units_list.Add(tNewUnit);
		return tNewUnit;
	}

	// Token: 0x06002323 RID: 8995 RVA: 0x00125588 File Offset: 0x00123788
	private void spawnUnits(Culture pCulture, Language pLanguage)
	{
		for (int i = 0; i < 99; i++)
		{
			this.newUnit(pCulture, pLanguage).data.age_overgrowth = Randy.randomInt(0, 100);
		}
	}

	// Token: 0x06002324 RID: 8996 RVA: 0x001255BC File Offset: 0x001237BC
	private IEnumerator civilizationMakingRoutine()
	{
		int tNoBuiltTicks = 0;
		int num;
		for (int i = 0; i < 5000; i = num + 1)
		{
			Actor tRandomUnit = this._units_list.GetRandom<Actor>();
			this.claimZone(tRandomUnit);
			this.gatherResources(tRandomUnit);
			bool tIsBuildingBuilt = this.buildBuilding(tRandomUnit);
			this.doFarming(tRandomUnit);
			this.makeFood(tRandomUnit);
			this.refertilizeTiles(tRandomUnit.current_zone);
			if (!tIsBuildingBuilt)
			{
				this.randomTeleport(tRandomUnit);
				num = tNoBuiltTicks;
				tNoBuiltTicks = num + 1;
			}
			else
			{
				tNoBuiltTicks = 0;
			}
			if (tNoBuiltTicks > 100)
			{
				break;
			}
			if (i % 4 == 0)
			{
				yield return new WaitForEndOfFrame();
			}
			num = i;
		}
		using (List<Actor>.Enumerator enumerator = this._units_list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Actor tUnit = enumerator.Current;
				this.craftAndTakeItems(tUnit);
				tUnit.setTask("citizen", true, false, false);
				tUnit.setStatsDirty();
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x06002325 RID: 8997 RVA: 0x001255CC File Offset: 0x001237CC
	private void claimZone(Actor pUnit)
	{
		TileZone tZone = MapBox.instance.city_zone_helper.city_growth.getZoneToClaim(null, this._city, false, null, 0);
		if (tZone == null)
		{
			return;
		}
		pUnit.setCurrentTilePosition(tZone.centerTile);
		BehClaimZoneForCityActorBorder.tryClaimZone(pUnit);
	}

	// Token: 0x06002326 RID: 8998 RVA: 0x00125610 File Offset: 0x00123810
	private void gatherResources(Actor pUnit)
	{
		foreach (BehaviourTaskActor tTask in this._tasks_gathering)
		{
			this.doTask(tTask, pUnit);
		}
		this._city.addResourcesToRandomStockpile("wood", 8);
		this._city.addResourcesToRandomStockpile("stone", 8);
		this._city.addResourcesToRandomStockpile("common_metals", 4);
		this._city.addResourcesToRandomStockpile("gold", 2);
		this._city.addResourcesToRandomStockpile("fertilizer", 1);
	}

	// Token: 0x06002327 RID: 8999 RVA: 0x001256C0 File Offset: 0x001238C0
	private bool buildBuilding(Actor pActor)
	{
		bool tResult = CityBehBuild.buildTick(this._city);
		foreach (BehaviourTaskActor tTask in this._tasks_building)
		{
			this.doTask(tTask, pActor);
		}
		World.world.cities.setDirtyBuildings(this._city);
		World.world.cities.beginChecksBuildings();
		BuildingZonesSystem.setDirty();
		BuildingZonesSystem.update();
		return tResult;
	}

	// Token: 0x06002328 RID: 9000 RVA: 0x00125750 File Offset: 0x00123950
	private void doFarming(Actor pUnit)
	{
		foreach (BehaviourTaskActor tTask in this._tasks_farming)
		{
			this.doTask(tTask, pUnit);
		}
		foreach (WorldTile worldTile in this._city.calculated_farm_fields)
		{
			Building tBuilding = worldTile.building;
			if (tBuilding != null && tBuilding.asset.wheat)
			{
				tBuilding.component_wheat.update(1.5f);
			}
		}
	}

	// Token: 0x06002329 RID: 9001 RVA: 0x00125804 File Offset: 0x00123A04
	private void makeFood(Actor pUnit)
	{
		if (this._food_producer_bonfire == null)
		{
			Building buildingOfType = this._city.getBuildingOfType("type_bonfire", true, false, false, null);
			this._food_producer_bonfire = ((buildingOfType != null) ? buildingOfType.component_food_producer : null);
		}
		if (this._food_producer_hall == null)
		{
			Building buildingOfType2 = this._city.getBuildingOfType("type_hall", true, false, false, null);
			this._food_producer_hall = ((buildingOfType2 != null) ? buildingOfType2.component_food_producer : null);
		}
		BuildingBiomeFoodProducer food_producer_bonfire = this._food_producer_bonfire;
		if (food_producer_bonfire != null)
		{
			food_producer_bonfire.update(1.5f);
		}
		BuildingBiomeFoodProducer food_producer_hall = this._food_producer_hall;
		if (food_producer_hall == null)
		{
			return;
		}
		food_producer_hall.update(1.5f);
	}

	// Token: 0x0600232A RID: 9002 RVA: 0x00125898 File Offset: 0x00123A98
	private void refertilizeTiles(TileZone pTileZone)
	{
		foreach (WorldTile tTile in pTileZone.tiles)
		{
			if (Randy.randomChance(0.05f))
			{
				DropsLibrary.action_fertilizer_trees(tTile, null);
			}
			if (Randy.randomChance(0.05f))
			{
				DropsLibrary.action_fertilizer_plants(tTile, null);
			}
		}
	}

	// Token: 0x0600232B RID: 9003 RVA: 0x001258E4 File Offset: 0x00123AE4
	private void randomTeleport(Actor pUnit)
	{
		pUnit.setCurrentTilePosition(this._city.zones.GetRandom<TileZone>().centerTile);
	}

	// Token: 0x0600232C RID: 9004 RVA: 0x00125904 File Offset: 0x00123B04
	private void craftAndTakeItems(Actor pUnit)
	{
		if (!Randy.randomChance(0.6f))
		{
			return;
		}
		for (int i = 0; i < 5; i++)
		{
			ItemCrafting.tryToCraftRandomArmor(pUnit, this._city);
			ItemCrafting.tryToCraftRandomWeapon(pUnit, this._city);
		}
		this.doTask(this._task_take_items, pUnit);
	}

	// Token: 0x0600232D RID: 9005 RVA: 0x00125954 File Offset: 0x00123B54
	private void doTask(BehaviourTaskActor pTask, Actor pActor)
	{
		this._action_index = 0;
		for (int i = 0; i < 500; i++)
		{
			BehResult tResult = pTask.list[this._action_index].startExecute(pActor);
			if (!this.updateTaskIndex(tResult) || this._action_index > pTask.list.Count - 1)
			{
				break;
			}
		}
	}

	// Token: 0x0600232E RID: 9006 RVA: 0x001259B0 File Offset: 0x00123BB0
	private bool updateTaskIndex(BehResult pResult)
	{
		switch (pResult)
		{
		case BehResult.Continue:
			this._action_index++;
			break;
		case BehResult.Stop:
		case BehResult.Skip:
		case BehResult.RestartTask:
		case BehResult.ActiveTaskReturn:
		case BehResult.ImmediateRun:
			return false;
		case BehResult.StepBack:
			this._action_index--;
			if (this._action_index < 0)
			{
				this._action_index = 0;
			}
			break;
		}
		return true;
	}

	// Token: 0x0600232F RID: 9007 RVA: 0x00125A18 File Offset: 0x00123C18
	private void clear()
	{
		if (this._routine != null)
		{
			World.world.StopCoroutine(this._routine);
		}
		this._action_index = 0;
		this._food_producer_bonfire = null;
		this._food_producer_hall = null;
		this._units_list.Clear();
	}

	// Token: 0x04001963 RID: 6499
	private const int KIDS_AGE = 5;

	// Token: 0x04001964 RID: 6500
	private const float KIDS_PERCENT = 0.5f;

	// Token: 0x04001965 RID: 6501
	private const int EGGS_AGE = 0;

	// Token: 0x04001966 RID: 6502
	private const float EGGS_PERCENT_OF_KIDS = 0.5f;

	// Token: 0x04001967 RID: 6503
	private const float WARRIORS_PERCENT_OF_ADULTS = 0.1f;

	// Token: 0x04001968 RID: 6504
	private const float ITEMS_HOLDER_PERCENT = 0.6f;

	// Token: 0x04001969 RID: 6505
	private const int TICKS_WITHOUT_BUILDING_TO_STOP = 100;

	// Token: 0x0400196A RID: 6506
	private const int MAXIMUM_TICKS = 5000;

	// Token: 0x0400196B RID: 6507
	private const int UNITS_AMOUNT = 100;

	// Token: 0x0400196C RID: 6508
	private const float ELAPSED_PER_TICK = 1.5f;

	// Token: 0x0400196D RID: 6509
	private const int ITEM_PRODUCTION_PER_UNIT = 5;

	// Token: 0x0400196E RID: 6510
	private const int MAXIMUM_TASK_ACTIONS = 500;

	// Token: 0x0400196F RID: 6511
	private const int FRAMES_PER_ROUTINE_UPDATE = 4;

	// Token: 0x04001970 RID: 6512
	private List<BehaviourTaskActor> _tasks_building = new List<BehaviourTaskActor>();

	// Token: 0x04001971 RID: 6513
	private List<BehaviourTaskActor> _tasks_gathering = new List<BehaviourTaskActor>();

	// Token: 0x04001972 RID: 6514
	private List<BehaviourTaskActor> _tasks_farming = new List<BehaviourTaskActor>();

	// Token: 0x04001973 RID: 6515
	private BehaviourTaskActor _task_take_items;

	// Token: 0x04001974 RID: 6516
	private City _city;

	// Token: 0x04001975 RID: 6517
	private Actor _unit;

	// Token: 0x04001976 RID: 6518
	private List<Actor> _units_list = new List<Actor>(100);

	// Token: 0x04001977 RID: 6519
	private BuildingBiomeFoodProducer _food_producer_bonfire;

	// Token: 0x04001978 RID: 6520
	private BuildingBiomeFoodProducer _food_producer_hall;

	// Token: 0x04001979 RID: 6521
	private Coroutine _routine;

	// Token: 0x0400197A RID: 6522
	private int _action_index;
}
