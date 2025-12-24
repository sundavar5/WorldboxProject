using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using tools;
using UnityEngine;

// Token: 0x02000237 RID: 567
public class BuildingManager : SimSystemManager<Building, BuildingData>
{
	// Token: 0x060015C1 RID: 5569 RVA: 0x000DFB48 File Offset: 0x000DDD48
	public BuildingManager()
	{
		this.type_id = "building";
		this._job_manager = new JobManagerBuildings("buildings");
	}

	// Token: 0x060015C2 RID: 5570 RVA: 0x000DFBCC File Offset: 0x000DDDCC
	public override void clear()
	{
		this._job_manager.clear();
		Array.Clear(this._array_visible_buildings, 0, this._array_visible_buildings.Length);
		this._temp_list_tiles.Clear();
		this.occupied_buildings.Clear();
		base.checkContainer();
		base.scheduleDestroyAllOnWorldClear();
		base.checkObjectsToDestroy();
		base.clear();
	}

	// Token: 0x060015C3 RID: 5571 RVA: 0x000DFC28 File Offset: 0x000DDE28
	protected override void destroyObject(Building pBuilding)
	{
		base.destroyObject(pBuilding);
		if (pBuilding.hasHousingLogic())
		{
			this.event_houses = true;
		}
		pBuilding.setAlive(false);
		pBuilding.asset.buildings.Remove(pBuilding);
		this.occupied_buildings.Remove(pBuilding);
		this.removeObject(pBuilding);
		this._job_manager.removeObject(pBuilding, pBuilding.batch);
	}

	// Token: 0x060015C4 RID: 5572 RVA: 0x000DFC8C File Offset: 0x000DDE8C
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		Bench.bench("buildings", "game_total", false);
		base.checkContainer();
		this._job_manager.updateBase(pElapsed);
		base.checkContainer();
		Bench.benchEnd("buildings", "game_total", false, 0L, false);
	}

	// Token: 0x060015C5 RID: 5573 RVA: 0x000DFCDD File Offset: 0x000DDEDD
	public override void loadFromSave(List<BuildingData> pList)
	{
		base.loadFromSave(pList);
		base.checkContainer();
	}

	// Token: 0x060015C6 RID: 5574 RVA: 0x000DFCEC File Offset: 0x000DDEEC
	internal Building addBuilding(string pID, WorldTile pTile, bool pCheckForBuild = false, bool pSfx = false, BuildPlacingType pType = BuildPlacingType.New)
	{
		BuildingAsset tAsset = AssetManager.buildings.get(pID);
		return this.addBuilding(tAsset, pTile, pCheckForBuild, pSfx, pType);
	}

	// Token: 0x060015C7 RID: 5575 RVA: 0x000DFD14 File Offset: 0x000DDF14
	internal Building addBuilding(BuildingAsset pAsset, WorldTile pTile, bool pCheckForBuild = false, bool pSfx = false, BuildPlacingType pType = BuildPlacingType.New)
	{
		if (pCheckForBuild && !this.canBuildFrom(pTile, pAsset, null, pType, false))
		{
			return null;
		}
		Building building = base.newObject();
		building.create();
		building.setBuilding(pTile, pAsset, null);
		building.checkStartSpawnAnimation();
		if (building.asset.city_building)
		{
			World.world.map_stats.housesBuilt += 1L;
		}
		return building;
	}

	// Token: 0x060015C8 RID: 5576 RVA: 0x000DFD74 File Offset: 0x000DDF74
	protected override void addObject(Building pObject)
	{
		base.addObject(pObject);
		this._job_manager.addNewObject(pObject);
	}

	// Token: 0x060015C9 RID: 5577 RVA: 0x000DFD8C File Offset: 0x000DDF8C
	public override Building loadObject(BuildingData pData)
	{
		if (pData.state == BuildingState.Removed)
		{
			return null;
		}
		BuildingAsset tAsset = AssetManager.buildings.get(pData.asset_id);
		if (tAsset == null)
		{
			return null;
		}
		WorldTile tTile = World.world.GetTileSimple(pData.mainX, pData.mainY);
		if (!this.canBuildFrom(tTile, tAsset, null, BuildPlacingType.Load, false))
		{
			return null;
		}
		Building building = base.loadObject(pData);
		building.create();
		building.setBuilding(tTile, tAsset, pData);
		building.loadBuilding(pData);
		return building;
	}

	// Token: 0x060015CA RID: 5578 RVA: 0x000DFE00 File Offset: 0x000DE000
	internal bool canBuildFrom(WorldTile pTile, BuildingAsset pNewBuildingAsset, City pCity, BuildPlacingType pType = BuildPlacingType.New, bool pFloraGrowth = false)
	{
		Subspecies tSubspecies = (pCity != null) ? pCity.getMainSubspecies() : null;
		bool tCheckForAdaptation = tSubspecies != null && pNewBuildingAsset.city_building && pNewBuildingAsset.check_for_adaptation_tags;
		if (tCheckForAdaptation && pTile.Type.is_biome)
		{
			string tBuildTag = pTile.Type.only_allowed_to_build_with_tag;
			if (tBuildTag != null && !tSubspecies.hasMetaTag(tBuildTag))
			{
				return false;
			}
		}
		BuildingFundament tFundament = pNewBuildingAsset.fundament;
		int tCenterX = pTile.x - tFundament.left;
		int tCenterY = pTile.y - tFundament.bottom;
		int tWidth = tFundament.width;
		int tHeight = tFundament.height;
		bool tGroundFound = false;
		bool tWaterFound = false;
		bool tIsDock = pNewBuildingAsset.docks;
		List<WorldTile> tTempList = this._temp_list_tiles;
		tTempList.Clear();
		bool tCheckForTags = !WorldLawLibrary.world_law_roots_without_borders.isEnabled();
		WorldTile tCityTile = (pCity != null) ? pCity.getTile(false) : null;
		if (pCity != null && tCityTile == null)
		{
			return false;
		}
		bool tTinyOvergrow = pType == BuildPlacingType.New && Randy.randomChance(0.1f);
		for (int tX = 0; tX < tWidth; tX++)
		{
			for (int tY = 0; tY < tHeight; tY++)
			{
				WorldTile tTile = World.world.GetTile(tCenterX + tX, tCenterY + tY);
				if (tTile == null)
				{
					return false;
				}
				if (tCheckForAdaptation)
				{
					string tBuildTag2 = tTile.Type.only_allowed_to_build_with_tag;
					if (tBuildTag2 != null && !tSubspecies.hasMetaTag(tBuildTag2))
					{
						return false;
					}
				}
				tTempList.Add(tTile);
				Building tCurrentBuildingOnTile = tTile.building;
				TileTypeBase tTileType = tTile.Type;
				if (tIsDock)
				{
					if (tTileType.ocean && OceanHelper.goodForNewDock(tTile))
					{
						tWaterFound = true;
					}
					if (tTileType.ground)
					{
						tGroundFound = true;
					}
				}
				if (pCity != null)
				{
					if (!tIsDock && !tTile.isSameIsland(tCityTile))
					{
						return false;
					}
					if (!tTile.isSameCityHere(pCity))
					{
						return false;
					}
					if (pNewBuildingAsset.only_build_tiles && !tTileType.can_build_on)
					{
						return false;
					}
				}
				if ((pType != BuildPlacingType.Load || (!(tTileType.id == "frozen_low") && !(tTileType.id == "frozen_high"))) && tCheckForTags && !pNewBuildingAsset.isOverlaysBiomeTags(tTileType))
				{
					if (!pFloraGrowth)
					{
						return false;
					}
					if (!pNewBuildingAsset.isOverlaysBiomeSpreadTags(tTileType))
					{
						return false;
					}
				}
				if (pNewBuildingAsset.flora && tCurrentBuildingOnTile != null)
				{
					if (!tCurrentBuildingOnTile.asset.flora)
					{
						return false;
					}
					if (pNewBuildingAsset.flora_size <= tCurrentBuildingOnTile.asset.flora_size)
					{
						if (tTinyOvergrow && tCurrentBuildingOnTile.asset.flora_size == FloraSize.Tiny && tCurrentBuildingOnTile.asset.flora_size == pNewBuildingAsset.flora_size)
						{
							if (tCurrentBuildingOnTile.asset == pNewBuildingAsset)
							{
								return false;
							}
						}
						else if (!tCurrentBuildingOnTile.isRuin())
						{
							return false;
						}
					}
					if (!tTile.canGrow())
					{
						return false;
					}
				}
				if (tTileType.liquid && !pNewBuildingAsset.can_be_placed_on_liquid)
				{
					return false;
				}
				if (pNewBuildingAsset.destroy_on_liquid && tTileType.ocean)
				{
					return false;
				}
				if (!tTile.canBuildOn(pNewBuildingAsset))
				{
					return false;
				}
				if (pNewBuildingAsset.check_for_close_building && pType == BuildPlacingType.New)
				{
					if (tX == 0)
					{
						if (this.isBuildingNearby(tTile.tile_left))
						{
							return false;
						}
					}
					else if (tX == tWidth - 1 && this.isBuildingNearby(tTile.tile_right))
					{
						return false;
					}
					if (tY == 0)
					{
						if (this.isBuildingNearby(tTile.tile_down))
						{
							return false;
						}
						if (tTile.has_tile_down && this.isBuildingNearby(tTile.tile_down.tile_down))
						{
							return false;
						}
					}
					else if (tY == tHeight - 1)
					{
						if (this.isBuildingNearby(tTile.tile_up))
						{
							return false;
						}
						if (tTile.has_tile_up && this.isBuildingNearby(tTile.tile_up.tile_up))
						{
							return false;
						}
					}
				}
			}
		}
		if (tIsDock && pType == BuildPlacingType.New)
		{
			if (tWaterFound && !tGroundFound)
			{
				for (int i = 0; i < tTempList.Count; i++)
				{
					WorldTile tDockTile = tTempList[i];
					for (int j = 0; j < tDockTile.neighbours.Length; j++)
					{
						WorldTile tN = tDockTile.neighbours[j];
						if (tN.Type.ground && tN.region.island == ((tCityTile != null) ? tCityTile.region.island : null))
						{
							return true;
						}
					}
				}
			}
			return false;
		}
		return true;
	}

	// Token: 0x060015CB RID: 5579 RVA: 0x000E0200 File Offset: 0x000DE400
	private bool isBuildingNearby(WorldTile pTile)
	{
		if (pTile == null)
		{
			return true;
		}
		Building tBuilding = pTile.building;
		return tBuilding != null && tBuilding.isUsable() && tBuilding.asset.city_building;
	}

	// Token: 0x060015CC RID: 5580 RVA: 0x000E0234 File Offset: 0x000DE434
	public Building getNearbyBuildingToLive(Actor pActor, bool pOnlyBuilt)
	{
		foreach (Building tTarget in this.getBuildingFromZones(pActor.current_tile, 10f))
		{
			if (tTarget.asset.hasHousingSlots() && tTarget.current_tile.isSameIsland(pActor.current_tile) && tTarget.hasResidentSlots())
			{
				if (pOnlyBuilt)
				{
					if (tTarget.isUnderConstruction())
					{
						continue;
					}
				}
				else if (!tTarget.isUnderConstruction())
				{
					continue;
				}
				if (tTarget.kingdom == pActor.kingdom)
				{
					return tTarget;
				}
			}
		}
		return null;
	}

	// Token: 0x060015CD RID: 5581 RVA: 0x000E02D8 File Offset: 0x000DE4D8
	public IEnumerable<Building> getBuildingFromZones(WorldTile pTile, float pRadius)
	{
		foreach (Building tBuilding in this.checkZoneForBuilding(pTile, pTile.zone, pRadius))
		{
			yield return tBuilding;
		}
		IEnumerator<Building> enumerator = null;
		float tRadiusZones = pRadius / 8f;
		int tSize = (int)tRadiusZones + 1;
		int startX = pTile.zone.x - tSize;
		int startY = pTile.zone.y - tSize;
		int num;
		for (int iX = 0; iX < tSize * 2; iX = num + 1)
		{
			for (int iY = 0; iY < tSize * 2; iY = num + 1)
			{
				TileZone tZone = World.world.zone_calculator.getZone(iX + startX, iY + startY);
				if (tZone != null)
				{
					foreach (Building tBuilding2 in this.checkZoneForBuilding(pTile, tZone, pRadius))
					{
						yield return tBuilding2;
					}
					enumerator = null;
				}
				num = iY;
			}
			num = iX;
		}
		yield break;
		yield break;
	}

	// Token: 0x060015CE RID: 5582 RVA: 0x000E02F6 File Offset: 0x000DE4F6
	private IEnumerable<Building> checkZoneForBuilding(WorldTile pTile, TileZone pZone, float pRadius)
	{
		if (!pZone.buildings_all.Any<Building>())
		{
			yield break;
		}
		float tRadius = pRadius * pRadius;
		foreach (Building tBuilding in pZone.buildings_all)
		{
			if ((tRadius == 0f || (float)Toolbox.SquaredDistTile(tBuilding.current_tile, pTile) <= tRadius) && !tBuilding.isRuin() && tBuilding.current_tile.isSameIsland(pTile))
			{
				yield return tBuilding;
			}
		}
		HashSet<Building>.Enumerator enumerator = default(HashSet<Building>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x060015CF RID: 5583 RVA: 0x000E0314 File Offset: 0x000DE514
	public void debugJobManager(DebugTool pTool)
	{
		this._job_manager.debug(pTool);
	}

	// Token: 0x060015D0 RID: 5584 RVA: 0x000E0324 File Offset: 0x000DE524
	private void prepareLists()
	{
		this._array_visible_buildings = Toolbox.checkArraySize<Building>(this._array_visible_buildings, this.Count);
		this.render_data.checkSize(this.Count);
		this.visible_stockpiles.Clear();
		this.sparkles.Clear();
		base.checkContainer();
	}

	// Token: 0x060015D1 RID: 5585 RVA: 0x000E0378 File Offset: 0x000DE578
	internal void calculateVisibleBuildings()
	{
		Bench.bench("buildings_prepare", "game_total", false);
		this.prepareLists();
		this._visible_buildings_count = 0;
		Bench.benchEnd("buildings_prepare", "game_total", false, 0L, false);
		if (!World.world.quality_changer.shouldRenderBuildings())
		{
			Bench.clearBenchmarkEntrySkipMultiple("game_total", new string[]
			{
				"buildings_render_data_parallel_256",
				"buildings_fill_visible",
				"buildings_render_data_normal"
			});
			return;
		}
		Bench.bench("buildings_fill_visible", "game_total", false);
		this.fillVisibleObjects();
		Bench.benchEnd("buildings_fill_visible", "game_total", false, 0L, false);
		Bench.bench("buildings_render_data_parallel_256", "game_total", false);
		this.precalculateRenderDataParallel();
		Bench.benchEnd("buildings_render_data_parallel_256", "game_total", false, 0L, false);
		Bench.bench("buildings_render_data_normal", "game_total", false);
		this.precalculateRenderDataNormal();
		Bench.benchEnd("buildings_render_data_normal", "game_total", false, 0L, false);
	}

	// Token: 0x060015D2 RID: 5586 RVA: 0x000E0474 File Offset: 0x000DE674
	private void fillVisibleObjects()
	{
		Building[] tArrayVisibleBuildings = this._array_visible_buildings;
		List<TileZone> tZonesList = World.world.zone_camera.getVisibleZones();
		int tZonesCount = tZonesList.Count;
		int tTotalVisibleBuildings = 0;
		for (int iZone = 0; iZone < tZonesCount; iZone++)
		{
			List<Building> buildings_render_list = tZonesList[iZone].buildings_render_list;
			int tZoneBuildings = buildings_render_list.Count;
			buildings_render_list.CopyTo(tArrayVisibleBuildings, tTotalVisibleBuildings);
			tTotalVisibleBuildings += tZoneBuildings;
		}
		this._visible_buildings_count = tTotalVisibleBuildings;
	}

	// Token: 0x060015D3 RID: 5587 RVA: 0x000E04DC File Offset: 0x000DE6DC
	private void precalculateRenderDataParallel()
	{
		Building[] tArrayVisibleBuildings = this._array_visible_buildings;
		bool tNeedShadows = World.world.quality_changer.shouldRenderBuildingShadows();
		int tTotalVisibleObjects = this._visible_buildings_count;
		Vector3[] tRenderScales = this.render_data.scales;
		Vector3[] tRenderPositions = this.render_data.positions;
		Vector3[] tRenderRotations = this.render_data.rotations;
		Material[] tRenderMaterials = this.render_data.materials;
		bool[] tRenderFlipXStates = this.render_data.flip_x_states;
		Color[] tRenderColors = this.render_data.colors;
		Sprite[] tRenderMainSprites = this.render_data.main_sprites;
		Sprite[] tRenderColoredSprites = this.render_data.colored_sprites;
		bool[] tRenderShadows = this.render_data.shadows;
		Sprite[] tRenderShadowSprites = this.render_data.shadow_sprites;
		int tDynamicBatchSize = 256;
		int tTotalBatches = ParallelHelper.calcTotalBatches(tTotalVisibleObjects, tDynamicBatchSize);
		bool tNeedNormalCheck = false;
		Parallel.For(0, tTotalBatches, World.world.parallel_options, delegate(int pBatchIndex)
		{
			int num = ParallelHelper.calculateBatchBeg(pBatchIndex, tDynamicBatchSize);
			int tIndexEnd = ParallelHelper.calculateBatchEnd(num, tDynamicBatchSize, tTotalVisibleObjects);
			for (int tIndex = num; tIndex < tIndexEnd; tIndex++)
			{
				Building tBuilding = tArrayVisibleBuildings[tIndex];
				BuildingAsset tAsset = tBuilding.asset;
				tRenderScales[tIndex] = tBuilding.getCurrentScale();
				tRenderPositions[tIndex] = tBuilding.cur_transform_position;
				tRenderRotations[tIndex] = tBuilding.current_rotation;
				tRenderMaterials[tIndex] = tBuilding.material;
				tRenderFlipXStates[tIndex] = tBuilding.flip_x;
				tRenderColors[tIndex] = tBuilding.kingdom.asset.color_building;
				Sprite tMainSprite = tBuilding.calculateMainSprite();
				tRenderMainSprites[tIndex] = tMainSprite;
				if (tBuilding.isColoredSpriteNeedsCheck(tMainSprite))
				{
					tRenderColoredSprites[tIndex] = null;
					tNeedNormalCheck = true;
				}
				else
				{
					tRenderColoredSprites[tIndex] = tBuilding.getLastColoredSprite();
				}
				if (tNeedShadows)
				{
					tRenderShadows[tIndex] = (tAsset.shadow && !tBuilding.chopped);
					tRenderShadowSprites[tIndex] = DynamicSprites.getShadowBuilding(tBuilding.asset, tRenderMainSprites[tIndex]);
				}
				if (tAsset.is_stockpile)
				{
					tNeedNormalCheck = true;
				}
				if (tAsset.sparkle_effect)
				{
					tNeedNormalCheck = true;
				}
			}
		});
		this._need_normal_check = tNeedNormalCheck;
	}

	// Token: 0x060015D4 RID: 5588 RVA: 0x000E0614 File Offset: 0x000DE814
	private void precalculateRenderDataNormal()
	{
		if (!this._need_normal_check)
		{
			return;
		}
		BuildingRenderData buildingRenderData = this.render_data;
		int tTotalVisibleBuildings = this._visible_buildings_count;
		Sprite[] tRenderColoredSprites = buildingRenderData.colored_sprites;
		Sprite[] tRenderMainSprites = buildingRenderData.main_sprites;
		for (int i = 0; i < tTotalVisibleBuildings; i++)
		{
			Building tBuilding = this._array_visible_buildings[i];
			if (tBuilding.asset.is_stockpile)
			{
				this.visible_stockpiles.Add(tBuilding);
			}
			if (tBuilding.asset.sparkle_effect)
			{
				this.sparkles.Add(tBuilding);
			}
			if (tRenderColoredSprites[i] == null)
			{
				tRenderColoredSprites[i] = tBuilding.calculateColoredSprite(tRenderMainSprites[i]);
			}
		}
	}

	// Token: 0x060015D5 RID: 5589 RVA: 0x000E06A2 File Offset: 0x000DE8A2
	public Building[] getVisibleBuildings()
	{
		return this._array_visible_buildings;
	}

	// Token: 0x060015D6 RID: 5590 RVA: 0x000E06AA File Offset: 0x000DE8AA
	public int countVisibleBuildings()
	{
		return this._visible_buildings_count;
	}

	// Token: 0x060015D7 RID: 5591 RVA: 0x000E06B4 File Offset: 0x000DE8B4
	public void checkWobblySetting()
	{
		bool tWobblySettingsOn = PlayerConfig.optionEnabled("tree_wind", OptionType.Bool);
		foreach (DynamicSpritesAsset tAtlasAsset in AssetManager.dynamic_sprites_library.list)
		{
			if (tAtlasAsset.check_wobbly_setting)
			{
				tAtlasAsset.big_atlas = !tWobblySettingsOn;
			}
		}
		foreach (DynamicSpritesAsset tAtlasAsset2 in AssetManager.dynamic_sprites_library.list)
		{
			if (tAtlasAsset2.buildings)
			{
				tAtlasAsset2.resetAtlas();
			}
		}
		AssetManager.buildings.checkAtlasLink(tWobblySettingsOn);
		foreach (Building building in this)
		{
			building.checkMaterial();
			building.clearSprites();
		}
	}

	// Token: 0x060015D8 RID: 5592 RVA: 0x000E07BC File Offset: 0x000DE9BC
	public JobManagerBuildings getJobManager()
	{
		return this._job_manager;
	}

	// Token: 0x04001230 RID: 4656
	private List<WorldTile> _temp_list_tiles = new List<WorldTile>();

	// Token: 0x04001231 RID: 4657
	private JobManagerBuildings _job_manager;

	// Token: 0x04001232 RID: 4658
	private Building[] _array_visible_buildings = new Building[0];

	// Token: 0x04001233 RID: 4659
	private int _visible_buildings_count;

	// Token: 0x04001234 RID: 4660
	public BuildingRenderData render_data = new BuildingRenderData(4096);

	// Token: 0x04001235 RID: 4661
	public HashSet<Building> occupied_buildings = new HashSet<Building>();

	// Token: 0x04001236 RID: 4662
	public List<Building> visible_stockpiles = new List<Building>();

	// Token: 0x04001237 RID: 4663
	public List<Building> sparkles = new List<Building>();

	// Token: 0x04001238 RID: 4664
	public MultiStackPool<BaseBuildingComponent> component_pool = new MultiStackPool<BaseBuildingComponent>();

	// Token: 0x04001239 RID: 4665
	private bool _need_normal_check;
}
