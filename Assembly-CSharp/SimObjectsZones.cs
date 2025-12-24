using System;
using System.Collections.Generic;

// Token: 0x020001E5 RID: 485
public class SimObjectsZones
{
	// Token: 0x06000E06 RID: 3590 RVA: 0x000BF514 File Offset: 0x000BD714
	public void setBuildingsDirty(MapChunk pChunk)
	{
		this._buildings_dirty = true;
		pChunk.setBuildingsDirty();
		this._dirty_building_chunks.Add(pChunk);
	}

	// Token: 0x06000E07 RID: 3591 RVA: 0x000BF530 File Offset: 0x000BD730
	internal void update()
	{
		Bench.bench("sim_zones", "game_total", false);
		if (this._timer > 0f)
		{
			this._timer -= World.world.delta_time;
		}
		else
		{
			this._timer = 0.1f;
			this.recalc();
		}
		Bench.benchEnd("sim_zones", "game_total", false, 0L, false);
	}

	// Token: 0x06000E08 RID: 3592 RVA: 0x000BF59C File Offset: 0x000BD79C
	private void recalc()
	{
		this.reset(false);
		Bench.bench("islands.recalcActors", "sim_zones", false);
		World.world.islands_calculator.recalcActors();
		Bench.benchEnd("islands.recalcActors", "sim_zones", false, 0L, false);
		Bench.bench("checkUnits", "sim_zones", false);
		this.checkUnits();
		Bench.benchEnd("checkUnits", "sim_zones", false, 0L, false);
		Bench.bench("checkBuildings", "sim_zones", false);
		if (this._buildings_dirty)
		{
			this.checkBuildings();
			this._buildings_dirty = false;
			foreach (MapChunk mapChunk in this._dirty_building_chunks)
			{
				mapChunk.finishBuildingsCheck();
			}
			this._dirty_building_chunks.Clear();
		}
		Bench.benchEnd("checkBuildings", "sim_zones", false, 0L, false);
	}

	// Token: 0x06000E09 RID: 3593 RVA: 0x000BF698 File Offset: 0x000BD898
	private void checkUnits()
	{
		List<Actor> tActorList = World.world.units.getSimpleList();
		int i = 0;
		int tLen = tActorList.Count;
		while (i < tLen)
		{
			Actor tUnit = tActorList[i];
			if (tUnit.isAlive())
			{
				WorldTile tTile = tUnit.current_tile;
				this.addUnit(tUnit, tTile);
				tTile.chunk.objects.addActor(tUnit);
			}
			i++;
		}
	}

	// Token: 0x06000E0A RID: 3594 RVA: 0x000BF6FC File Offset: 0x000BD8FC
	private void checkBuildings()
	{
		List<Building> tBuildingList = World.world.buildings.getSimpleList();
		int i = 0;
		int tLen = tBuildingList.Count;
		while (i < tLen)
		{
			Building tBuilding = tBuildingList[i];
			if (tBuilding.isUsable())
			{
				MapChunk tChunk = tBuilding.chunk;
				if (tChunk.buildings_dirty)
				{
					if (tBuilding.isCiv() && tBuilding.asset.docks && tBuilding.component_docks.hasOceanTiles())
					{
						tBuilding.component_docks.tiles_ocean[0].region.island.addDock(tBuilding);
					}
					tChunk.objects.addBuilding(tBuilding);
				}
			}
			i++;
		}
	}

	// Token: 0x06000E0B RID: 3595 RVA: 0x000BF7A0 File Offset: 0x000BD9A0
	private void addUnit(Actor pActor, WorldTile pTile)
	{
		if (!pTile.hasUnits())
		{
			this._to_clear_tiles.Add(pTile);
		}
		pTile.addUnit(pActor);
		TileZone tZone = pTile.zone;
		City tCity = pTile.zone_city;
		if (tCity == null)
		{
			return;
		}
		if (pActor.isInsideSomething())
		{
			return;
		}
		Kingdom tActorKingdom = pActor.kingdom;
		if (pActor.profession_asset.can_capture)
		{
			tCity.updateConquest(pActor);
		}
		else if (tActorKingdom.isCiv())
		{
			return;
		}
		if (tCity.danger_zones.Contains(tZone))
		{
			return;
		}
		if (tActorKingdom.isMobs() && WorldLawLibrary.world_law_peaceful_monsters.isEnabled())
		{
			return;
		}
		if (tActorKingdom == tCity.kingdom)
		{
			return;
		}
		if (!tActorKingdom.asset.count_as_danger)
		{
			return;
		}
		if (!tActorKingdom.isEnemy(tCity.kingdom))
		{
			return;
		}
		tCity.danger_zones.Add(tZone);
	}

	// Token: 0x06000E0C RID: 3596 RVA: 0x000BF864 File Offset: 0x000BDA64
	private void clearTileUnits()
	{
		List<WorldTile> tToClearTiles = this._to_clear_tiles;
		int i = 0;
		int tLen = tToClearTiles.Count;
		while (i < tLen)
		{
			tToClearTiles[i].clearUnits();
			i++;
		}
		tToClearTiles.Clear();
	}

	// Token: 0x06000E0D RID: 3597 RVA: 0x000BF8A0 File Offset: 0x000BDAA0
	private void clearChunkObjects(bool pForceClearBuildings)
	{
		MapChunk[] tChunks = World.world.map_chunk_manager.chunks;
		int i = 0;
		int tLen = tChunks.Length;
		while (i < tLen)
		{
			MapChunk tChunk = tChunks[i];
			if (!tChunk.objects.isEmpty())
			{
				tChunk.clearObjects(pForceClearBuildings);
			}
			i++;
		}
	}

	// Token: 0x06000E0E RID: 3598 RVA: 0x000BF8E8 File Offset: 0x000BDAE8
	private void clearIslandsDocks()
	{
		if (!this._buildings_dirty)
		{
			return;
		}
		ListPool<TileIsland> tIslands = World.world.islands_calculator.islands;
		int i = 0;
		int tLen = tIslands.Count;
		while (i < tLen)
		{
			TileIsland tileIsland = tIslands[i];
			ListPool<Docks> docks = tileIsland.docks;
			if (docks != null)
			{
				docks.Dispose();
			}
			tileIsland.docks = null;
			i++;
		}
	}

	// Token: 0x06000E0F RID: 3599 RVA: 0x000BF940 File Offset: 0x000BDB40
	private void clearCaptureAndDangerZones()
	{
		foreach (City city in World.world.cities)
		{
			city.clearCurrentCaptureAmounts();
			city.clearDangerZones();
		}
	}

	// Token: 0x06000E10 RID: 3600 RVA: 0x000BF994 File Offset: 0x000BDB94
	private void clearAllDisposed()
	{
		foreach (BaseSystemManager baseSystemManager in World.world.list_all_sim_managers)
		{
			baseSystemManager.ClearAllDisposed();
		}
	}

	// Token: 0x06000E11 RID: 3601 RVA: 0x000BF9E8 File Offset: 0x000BDBE8
	private void reset(bool pForceClearBuildings = false)
	{
		if (pForceClearBuildings)
		{
			this._buildings_dirty = true;
		}
		Bench.bench("clear_tiles", "sim_zones", false);
		this.clearTileUnits();
		Bench.benchEnd("clear_tiles", "sim_zones", false, 0L, false);
		Bench.bench("clear_chunks", "sim_zones", false);
		this.clearChunkObjects(pForceClearBuildings);
		Bench.benchEnd("clear_chunks", "sim_zones", false, 0L, false);
		Bench.bench("clear_islands_docks", "sim_zones", false);
		this.clearIslandsDocks();
		Bench.benchEnd("clear_islands_docks", "sim_zones", false, 0L, false);
		Bench.bench("clear_capture_and_danger_zones", "sim_zones", false);
		this.clearCaptureAndDangerZones();
		Bench.benchEnd("clear_capture_and_danger_zones", "sim_zones", false, 0L, false);
		Bench.bench("clear_all_disposed", "sim_zones", false);
		this.clearAllDisposed();
		Bench.benchEnd("clear_all_disposed", "sim_zones", false, 0L, false);
	}

	// Token: 0x06000E12 RID: 3602 RVA: 0x000BFAD7 File Offset: 0x000BDCD7
	public void fullClear()
	{
		this.reset(true);
	}

	// Token: 0x04000E67 RID: 3687
	private float _timer;

	// Token: 0x04000E68 RID: 3688
	private const float INTERVAL = 0.1f;

	// Token: 0x04000E69 RID: 3689
	private readonly List<WorldTile> _to_clear_tiles = new List<WorldTile>();

	// Token: 0x04000E6A RID: 3690
	private readonly HashSet<MapChunk> _dirty_building_chunks = new HashSet<MapChunk>();

	// Token: 0x04000E6B RID: 3691
	private bool _buildings_dirty;
}
