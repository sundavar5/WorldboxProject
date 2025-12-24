using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200052B RID: 1323
public class DebugLayer : MapLayer
{
	// Token: 0x06002B3D RID: 11069 RVA: 0x001568AC File Offset: 0x00154AAC
	protected override void UpdateDirty(float pElapsed)
	{
		if (!DebugConfig.instance.debugButton.gameObject.activeSelf)
		{
			this.clear();
			return;
		}
		this.color_active_path = new Color(1f, 1f, 1f, 0.5f);
		this.used = false;
		this.clear();
		if (this._forced_global_path != null && this._forced_global_path.Count > 0)
		{
			this.drawRegionPath(this._forced_global_path);
		}
		if (DebugConfig.isOn(DebugOption.CityZones))
		{
			this.drawZones();
		}
		else if (DebugConfig.isOn(DebugOption.Chunks))
		{
			this.drawChunks();
		}
		if (DebugConfig.isOn(DebugOption.PathRegions))
		{
			this.drawPathRegions();
		}
		if (DebugConfig.isOn(DebugOption.ActivePaths))
		{
			this.drawActivePaths();
		}
		if (DebugConfig.isOn(DebugOption.CityPlaces))
		{
			this.drawCityPlaces();
		}
		if (DebugConfig.isOn(DebugOption.RenderCityDangerZones))
		{
			this.drawCityDangerZones();
		}
		if (DebugConfig.isOn(DebugOption.RenderVisibleZones))
		{
			this.drawVisibleZones();
		}
		if (DebugConfig.isOn(DebugOption.RenderCityCenterZones))
		{
			this.drawCityCenterZones();
		}
		if (DebugConfig.isOn(DebugOption.RenderCityFarmPlaces))
		{
			this.drawCityFarmZones();
		}
		if (DebugConfig.isOn(DebugOption.Buildings))
		{
			this.drawBuildings();
		}
		if (DebugConfig.isOn(DebugOption.FmodZones))
		{
			this.drawFmodZones();
		}
		if (DebugConfig.isOn(DebugOption.ConstructionTiles))
		{
			this.drawConstructionTiles();
		}
		if (DebugConfig.isOn(DebugOption.UnitsInside))
		{
			this.drawUnitsInside();
		}
		if (DebugConfig.isOn(DebugOption.TargetedBy))
		{
			this.drawTargetedBy();
		}
		if (DebugConfig.isOn(DebugOption.UnitKingdoms))
		{
			this.drawUnitKingdoms();
		}
		if (DebugConfig.isOn(DebugOption.DisplayUnitTiles))
		{
			this.drawUnitTiles();
		}
		if (DebugConfig.isOn(DebugOption.ProKing))
		{
			this.drawProfession(UnitProfession.King);
		}
		if (DebugConfig.isOn(DebugOption.ProLeader))
		{
			this.drawProfession(UnitProfession.Leader);
		}
		if (DebugConfig.isOn(DebugOption.ProUnit))
		{
			this.drawProfession(UnitProfession.Unit);
		}
		if (DebugConfig.isOn(DebugOption.ProWarrior))
		{
			this.drawProfession(UnitProfession.Warrior);
		}
		if (this.used)
		{
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			base.updatePixels();
			return;
		}
		if (base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06002B3E RID: 11070 RVA: 0x00156A9C File Offset: 0x00154C9C
	private void drawUnitKingdoms()
	{
		this.used = true;
		foreach (Actor tActor in World.world.units)
		{
			if (tActor.kingdom != null && tActor.kingdom.getColor() != null)
			{
				Color tColor = tActor.kingdom.getColor().getColorMain32();
				this.pixels[tActor.current_tile.data.tile_id] = tColor;
				this._tiles.Add(tActor.current_tile);
			}
		}
	}

	// Token: 0x06002B3F RID: 11071 RVA: 0x00156B4C File Offset: 0x00154D4C
	private void drawUnitTiles()
	{
		this.used = true;
		foreach (WorldTile tTile in World.world.tiles_list)
		{
			if (tTile.hasUnits())
			{
				this.pixels[tTile.data.tile_id] = Color.blue;
				this._tiles.Add(tTile);
			}
		}
	}

	// Token: 0x06002B40 RID: 11072 RVA: 0x00156BB4 File Offset: 0x00154DB4
	private void drawTargetedBy()
	{
		this.used = true;
		foreach (WorldTile tTile in World.world.tiles_list)
		{
			if (tTile.isTargeted())
			{
				this.pixels[tTile.data.tile_id] = Color.blue;
				this._tiles.Add(tTile);
			}
		}
	}

	// Token: 0x06002B41 RID: 11073 RVA: 0x00156C1C File Offset: 0x00154E1C
	private void drawProfession(UnitProfession pPro)
	{
		this.used = true;
		foreach (Actor tActor in World.world.units)
		{
			if (tActor.isProfession(pPro))
			{
				Color tColor = Color.blue;
				this.pixels[tActor.current_tile.data.tile_id] = tColor;
				this._tiles.Add(tActor.current_tile);
			}
		}
	}

	// Token: 0x06002B42 RID: 11074 RVA: 0x00156CB0 File Offset: 0x00154EB0
	private void drawCitizenJobs(string pID)
	{
		this.used = true;
		foreach (Actor tActor in World.world.units)
		{
			if (tActor.ai.job != null && !(pID != tActor.ai.job.id))
			{
				Color tColor = Color.red;
				this.pixels[tActor.current_tile.data.tile_id] = tColor;
				this._tiles.Add(tActor.current_tile);
			}
		}
	}

	// Token: 0x06002B43 RID: 11075 RVA: 0x00156D60 File Offset: 0x00154F60
	private void drawUnitsInside()
	{
		this.used = true;
		foreach (Actor tActor in World.world.units)
		{
			if (tActor.is_inside_building)
			{
				this.pixels[tActor.current_tile.data.tile_id] = Color.green;
				this._tiles.Add(tActor.current_tile);
			}
		}
	}

	// Token: 0x06002B44 RID: 11076 RVA: 0x00156DF0 File Offset: 0x00154FF0
	private void drawConstructionTiles()
	{
		this.used = true;
		foreach (WorldTile tTile in World.world.tiles_list)
		{
			if (tTile.hasBuilding() && tTile.building.asset.docks)
			{
				ValueTuple<TileZone[], int> allZonesFromTile = Toolbox.getAllZonesFromTile(tTile);
				TileZone[] tZones = allZonesFromTile.Item1;
				int tCount = allZonesFromTile.Item2;
				for (int i = 0; i < tCount; i++)
				{
					TileZone tTileZone = tZones[i];
					foreach (WorldTile iTile in tTile.building.checkZoneForDockConstruction(tTileZone))
					{
						this.pixels[iTile.data.tile_id] = Color.red;
						this._tiles.Add(iTile);
					}
				}
			}
		}
	}

	// Token: 0x06002B45 RID: 11077 RVA: 0x00156EE8 File Offset: 0x001550E8
	private void drawFmodZones()
	{
		this.used = true;
		foreach (TileZone tZone in DebugLayer.fmod_zones_to_draw)
		{
			this.fill(tZone.tiles, Color.yellow, false);
		}
	}

	// Token: 0x06002B46 RID: 11078 RVA: 0x00156F4C File Offset: 0x0015514C
	private void drawBuildings()
	{
		this.used = true;
		foreach (WorldTile tTile in World.world.tiles_list)
		{
			if (tTile.hasBuilding())
			{
				if (tTile.building.kingdom != null && tTile.building.isKingdomCiv())
				{
					this.pixels[tTile.data.tile_id] = tTile.building.kingdom.getColor().getColorMain32();
				}
				else
				{
					this.pixels[tTile.data.tile_id] = Color.red;
				}
				this.pixels[tTile.building.current_tile.data.tile_id] = Color.magenta;
				this.pixels[tTile.building.door_tile.data.tile_id] = Color.yellow;
				this._tiles.Add(tTile.building.current_tile);
				this._tiles.Add(tTile.building.door_tile);
				this._tiles.Add(tTile);
			}
		}
	}

	// Token: 0x06002B47 RID: 11079 RVA: 0x00157084 File Offset: 0x00155284
	private void drawCityCenterZones()
	{
		this.used = true;
		foreach (City city in World.world.cities)
		{
			WorldTile tTile = city.getTile(false);
			if (tTile != null)
			{
				this.fill(tTile.zone.tiles, Color.red, false);
			}
		}
	}

	// Token: 0x06002B48 RID: 11080 RVA: 0x001570F8 File Offset: 0x001552F8
	private void drawCityFarmZones()
	{
		this.used = true;
		foreach (City tCity in World.world.cities)
		{
			this.fill(tCity.calculated_place_for_farms.getSimpleList(), Color.blue, false);
			this.fill(tCity.calculated_farm_fields.getSimpleList(), Color.cyan, false);
			this.fill(tCity.calculated_crops.getSimpleList(), Color.green, false);
			this.fill(tCity.calculated_grown_wheat.getSimpleList(), Color.yellow, false);
		}
	}

	// Token: 0x06002B49 RID: 11081 RVA: 0x001571A8 File Offset: 0x001553A8
	private void drawVisibleZones()
	{
		this.used = true;
		List<TileZone> tVisibleZones = World.world.zone_camera.getVisibleZones();
		for (int iZone = 0; iZone < tVisibleZones.Count; iZone++)
		{
			TileZone tZone = tVisibleZones[iZone];
			if (tZone.visible_main_centered)
			{
				this.fill(tZone.tiles, Color.green, false);
			}
			else if (tZone.visible)
			{
				this.fill(tZone.tiles, Color.blue, false);
			}
		}
	}

	// Token: 0x06002B4A RID: 11082 RVA: 0x0015721C File Offset: 0x0015541C
	private void drawCityDangerZones()
	{
		this.used = true;
		foreach (City city in World.world.cities)
		{
			foreach (TileZone tZone in city.danger_zones)
			{
				this.fill(tZone.tiles, Color.red, false);
			}
		}
	}

	// Token: 0x06002B4B RID: 11083 RVA: 0x001572B8 File Offset: 0x001554B8
	private void drawCityPlaces()
	{
		this.used = true;
		foreach (TileZone tZone in World.world.zone_calculator.zones)
		{
			if (tZone.city != null)
			{
				this.fill(tZone.tiles, Color.yellow, false);
			}
			else if (tZone.isGoodForNewCity())
			{
				this.fill(tZone.tiles, Color.blue, false);
			}
		}
	}

	// Token: 0x06002B4C RID: 11084 RVA: 0x0015734C File Offset: 0x0015554C
	private void drawActivePaths()
	{
		this.used = true;
		foreach (Actor tActor in World.world.units)
		{
			if (tActor.current_path_global != null)
			{
				this.drawRegionPath(tActor.current_path_global);
				this.fill(tActor.current_path, Color.blue, false);
			}
		}
	}

	// Token: 0x06002B4D RID: 11085 RVA: 0x001573C4 File Offset: 0x001555C4
	public void drawRegionPath(List<MapRegion> pRegions)
	{
		this.used = true;
		foreach (MapRegion tRegion in pRegions)
		{
			this.fill(tRegion.tiles, this.color_active_path, false);
		}
	}

	// Token: 0x06002B4E RID: 11086 RVA: 0x00157428 File Offset: 0x00155628
	public void forceDrawRegionPath(List<MapRegion> pRegions)
	{
		this._forced_global_path.Clear();
		this._forced_global_path.AddRange(pRegions);
	}

	// Token: 0x06002B4F RID: 11087 RVA: 0x00157444 File Offset: 0x00155644
	private void drawPathRegions()
	{
		this.used = true;
		MapChunk[] chunks = World.world.map_chunk_manager.chunks;
		for (int i = 0; i < chunks.Length; i++)
		{
			foreach (MapRegion tRegion in chunks[i].regions)
			{
				if (tRegion.path_wave_id != -1)
				{
					this.fill(tRegion.tiles, new Color(1f, 1f, 0f, 0.9f), false);
				}
			}
		}
		List<MapRegion> last_globalPath = World.world.region_path_finder.last_globalPath;
		if (last_globalPath != null && last_globalPath.Count > 0)
		{
			RegionPathFinder region_path_finder = World.world.region_path_finder;
			bool flag;
			if (region_path_finder == null)
			{
				flag = (null != null);
			}
			else
			{
				WorldTile tileStart = region_path_finder.tileStart;
				flag = (((tileStart != null) ? tileStart.region : null) != null);
			}
			if (flag)
			{
				RegionPathFinder region_path_finder2 = World.world.region_path_finder;
				bool flag2;
				if (region_path_finder2 == null)
				{
					flag2 = (null != null);
				}
				else
				{
					WorldTile tileTarget = region_path_finder2.tileTarget;
					flag2 = (((tileTarget != null) ? tileTarget.region : null) != null);
				}
				if (flag2)
				{
					foreach (MapRegion tRegion2 in World.world.region_path_finder.last_globalPath)
					{
						this.fill(tRegion2.tiles, Color.blue, false);
					}
					this.fill(World.world.region_path_finder.tileStart.region.tiles, Color.green, false);
					this.fill(World.world.region_path_finder.tileTarget.region.tiles, new Color(1f, 0f, 0f, 0.3f), false);
				}
			}
		}
	}

	// Token: 0x06002B50 RID: 11088 RVA: 0x00157610 File Offset: 0x00155810
	private void fill(List<WorldTile> pTiles, Color pColor, bool pEdge = false)
	{
		base.createTextureNew();
		for (int i = 0; i < pTiles.Count; i++)
		{
			WorldTile tTile = pTiles[i];
			if (!pEdge || tTile.region != null)
			{
				this._tiles.Add(tTile);
				this.pixels[tTile.data.tile_id] = pColor;
			}
		}
	}

	// Token: 0x06002B51 RID: 11089 RVA: 0x00157670 File Offset: 0x00155870
	private void fill(WorldTile[] pTiles, Color pColor, bool pEdge = false)
	{
		base.createTextureNew();
		foreach (WorldTile tTile in pTiles)
		{
			if (!pEdge || tTile.region != null)
			{
				this._tiles.Add(tTile);
				this.pixels[tTile.data.tile_id] = pColor;
			}
		}
	}

	// Token: 0x06002B52 RID: 11090 RVA: 0x001576CC File Offset: 0x001558CC
	private void drawZones()
	{
		this.used = true;
		foreach (TileZone tZone in World.world.zone_calculator.zones)
		{
			if ((tZone.x + tZone.y) % 2 == 0)
			{
				tZone.debug_zone_color = this.color1;
			}
			else
			{
				tZone.debug_zone_color = this.color2;
			}
			this.fill(tZone.tiles, tZone.debug_zone_color, false);
		}
	}

	// Token: 0x06002B53 RID: 11091 RVA: 0x00157768 File Offset: 0x00155968
	private void testCityLayout()
	{
		DebugVariables instance = DebugVariables.instance;
		if (instance != null && !instance.layout_city_test)
		{
			return;
		}
		this.used = true;
		WorldTile tCursorTile = World.world.getMouseTilePos();
		if (tCursorTile == null)
		{
			return;
		}
		TileZone tCursorZone = (tCursorTile != null) ? tCursorTile.zone : null;
		foreach (TileZone tZone in World.world.zone_calculator.zones)
		{
			bool tAllow = true;
			if (!TownPlans.debugVisualizeZone(tZone, tCursorZone))
			{
				tAllow = false;
			}
			if (tAllow)
			{
				tZone.debug_zone_color = this.color1;
			}
			else
			{
				tZone.debug_zone_color = this.color_red;
			}
			this.fill(tZone.tiles, tZone.debug_zone_color, false);
		}
	}

	// Token: 0x06002B54 RID: 11092 RVA: 0x00157838 File Offset: 0x00155A38
	private void drawChunks()
	{
		this.used = true;
		foreach (MapChunk tChunk in World.world.map_chunk_manager.chunks)
		{
			this.fill(tChunk.tiles, tChunk.color, false);
		}
	}

	// Token: 0x06002B55 RID: 11093 RVA: 0x00157884 File Offset: 0x00155A84
	internal override void clear()
	{
		HashSet<WorldTile> tTiles = this._tiles;
		if (tTiles.Count == 0)
		{
			return;
		}
		foreach (WorldTile tTile in tTiles)
		{
			if (tTile.data.tile_id <= this.pixels.Length - 1)
			{
				this.pixels[tTile.data.tile_id] = Color.clear;
			}
		}
		this._tiles.Clear();
		base.createTextureNew();
	}

	// Token: 0x0400206A RID: 8298
	internal static List<TileZone> fmod_zones_to_draw = new List<TileZone>();

	// Token: 0x0400206B RID: 8299
	private HashSet<WorldTile> _tiles = new HashSet<WorldTile>();

	// Token: 0x0400206C RID: 8300
	public Color color1 = Color.gray;

	// Token: 0x0400206D RID: 8301
	public Color color2 = Color.white;

	// Token: 0x0400206E RID: 8302
	public Color color_red = Color.red;

	// Token: 0x0400206F RID: 8303
	public Color color_active_path;

	// Token: 0x04002070 RID: 8304
	private bool used;

	// Token: 0x04002071 RID: 8305
	private List<MapRegion> _forced_global_path = new List<MapRegion>();
}
