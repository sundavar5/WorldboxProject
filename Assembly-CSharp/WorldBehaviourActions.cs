using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200030D RID: 781
public static class WorldBehaviourActions
{
	// Token: 0x06001D5E RID: 7518 RVA: 0x001065C0 File Offset: 0x001047C0
	public static void buildingSparks()
	{
		if (World.world.buildings.sparkles.Count == 0)
		{
			return;
		}
		if (!MapBox.isRenderGameplay())
		{
			return;
		}
		for (int i = 0; i < 1; i++)
		{
			Building tBuilding = World.world.buildings.sparkles.GetRandom<Building>();
			if (tBuilding.isUsable() && tBuilding.animation_state == BuildingAnimationState.Normal)
			{
				WorldTile tTile = tBuilding.tiles.GetRandom<WorldTile>();
				if (tTile.has_tile_up)
				{
					EffectsLibrary.spawnAtTile("fx_building_sparkle", tTile.tile_up, 0.25f);
				}
			}
		}
	}

	// Token: 0x06001D5F RID: 7519 RVA: 0x00106648 File Offset: 0x00104848
	public static void updateDisasters()
	{
		DisasterAsset tAsset = AssetManager.disasters.getRandomAssetFromPool();
		if (tAsset == null)
		{
			return;
		}
		if (!Randy.randomChance(tAsset.chance))
		{
			return;
		}
		if (tAsset == null)
		{
			return;
		}
		tAsset.action(tAsset);
	}

	// Token: 0x06001D60 RID: 7520 RVA: 0x00106684 File Offset: 0x00104884
	public static void updateMigrants()
	{
		if (!WorldLawLibrary.world_law_civ_migrants.isEnabled())
		{
			return;
		}
		if (World.world.cities.list.Count == 0)
		{
			return;
		}
		int tRandomAmountCities = Randy.randomInt(1, World.world.cities.list.Count);
		foreach (City tCity in World.world.cities.list.LoopRandom(tRandomAmountCities))
		{
			if (!tCity.isNeutral() && tCity.getPopulationPeople() <= 100 && tCity.countFoodTotal() >= 10)
			{
				Building tBuildingHall = tCity.getBuildingOfType("type_bonfire", true, false, false, null);
				if (tBuildingHall != null)
				{
					Subspecies tSubspecies = tCity.getMainSubspecies();
					if (tSubspecies != null && !tSubspecies.hasReachedPopulationLimit())
					{
						Kingdom tKingdom = tCity.kingdom;
						ActorAsset tAsset = tSubspecies.getActorAsset();
						int tRandomAmount = Randy.randomInt(1, 4);
						for (int i = 0; i < tRandomAmount; i++)
						{
							Actor actor = World.world.units.spawnNewUnit(tAsset.id, tBuildingHall.current_tile, false, false, 0f, tSubspecies, true, false);
							actor.data.age_overgrowth = Randy.randomInt(tAsset.age_spawn, tAsset.age_spawn * 2);
							actor.addTrait("attractive", false);
							actor.addTrait("soft_skin", false);
							actor.setMetasFromCity(tCity);
							tKingdom.increaseMigrants();
							actor.setKingdom(tKingdom);
							tCity.increaseMigrants();
							actor.setCity(tCity);
							actor.addStatusEffect("handsome_migrant", 0f, true);
						}
					}
				}
			}
		}
	}

	// Token: 0x06001D61 RID: 7521 RVA: 0x00106848 File Offset: 0x00104A48
	public static void updateRoadDecay()
	{
		TileZone tZone = World.world.zone_calculator.zones.GetRandom<TileZone>();
		if (tZone.hasCity() && tZone.city.hasCulture() && tZone.city.culture.canUseRoads())
		{
			return;
		}
		if (Randy.randomBool())
		{
			return;
		}
		foreach (WorldTile tTile in tZone.tiles.LoopRandom<WorldTile>())
		{
			if (!Randy.randomBool() && tTile.Type.road)
			{
				MapAction.decreaseTile(tTile, false, "flash");
			}
		}
	}

	// Token: 0x06001D62 RID: 7522 RVA: 0x001068F8 File Offset: 0x00104AF8
	public static void updateFarmDecay()
	{
		if (MapBox.current_world_seed_id != WorldBehaviourActions._last_used_world_id)
		{
			WorldBehaviourActions._last_used_world_id = MapBox.current_world_seed_id;
			WorldBehaviourActions._temp_wheat_list.Clear();
		}
		if (WorldBehaviourActions._temp_wheat_list.Count == 0)
		{
			foreach (WorldTile tTile in TopTileLibrary.field.hashset)
			{
				WorldBehaviourActions._temp_wheat_list.Add(tTile);
			}
			WorldBehaviourActions._temp_wheat_list.Shuffle<WorldTile>();
		}
		int i = 0;
		while (i < 10 && WorldBehaviourActions._temp_wheat_list.Count != 0)
		{
			WorldTile tTile2 = WorldBehaviourActions._temp_wheat_list.Pop<WorldTile>();
			if (tTile2.Type.farm_field)
			{
				City tCity = tTile2.zone.city;
				if (tCity == null)
				{
					MapAction.decreaseTile(tTile2, false, "flash");
				}
				else if (!tCity.calculated_farm_fields.Contains(tTile2, true) && !tCity.calculated_place_for_farms.Contains(tTile2, true))
				{
					if (tTile2.hasBuilding() && tTile2.building.asset.wheat)
					{
						tTile2.building.startDestroyBuilding();
					}
					MapAction.decreaseTile(tTile2, false, "flash");
				}
			}
			i++;
		}
	}

	// Token: 0x06001D63 RID: 7523 RVA: 0x00106A34 File Offset: 0x00104C34
	public static void clearFarmDecay()
	{
		WorldBehaviourActions._temp_wheat_list.Clear();
	}

	// Token: 0x06001D64 RID: 7524 RVA: 0x00106A40 File Offset: 0x00104C40
	public static void updateRuinRatSpawn()
	{
		if (!WorldLawLibrary.world_law_animals_spawn.isEnabled())
		{
			return;
		}
		if (World.world.map_chunk_manager.chunks.Length == 0)
		{
			return;
		}
		ActorAsset tActorAsset = AssetManager.actor_library.get("rat");
		if (tActorAsset == null)
		{
			return;
		}
		if (tActorAsset.units.Count > tActorAsset.max_random_amount * 3)
		{
			return;
		}
		Kingdom tKingdom = World.world.kingdoms_wild.get("ruins");
		if (tKingdom.buildings.Count == 0)
		{
			return;
		}
		Building tRandomBuilding = null;
		foreach (Building tBuilding in tKingdom.buildings.LoopRandom<Building>())
		{
			if (tBuilding.isAlive() && tBuilding.asset.city_building && tBuilding.asset.hasHousingSlots())
			{
				tRandomBuilding = tBuilding;
				break;
			}
		}
		if (tRandomBuilding == null)
		{
			return;
		}
		WorldTile tTile = tRandomBuilding.current_tile;
		int tRandomUnits = Randy.randomInt(2, 5);
		for (int i = 0; i < tRandomUnits; i++)
		{
			World.world.units.spawnNewUnit(tActorAsset.id, tTile, false, false, 6f, null, false, false);
		}
	}

	// Token: 0x06001D65 RID: 7525 RVA: 0x00106B70 File Offset: 0x00104D70
	public static void updateUnitSpawn()
	{
		if (!WorldLawLibrary.world_law_animals_spawn.isEnabled())
		{
			return;
		}
		if (World.world.map_chunk_manager.chunks.Length == 0)
		{
			return;
		}
		TileIsland tIsland = null;
		if (World.world.islands_calculator.islands_ground.Count > 0)
		{
			tIsland = World.world.islands_calculator.getRandomIslandGround(true);
		}
		if (tIsland == null)
		{
			return;
		}
		WorldTile tTile = tIsland.getRandomTile();
		BiomeAsset tBiomeAsset = tTile.Type.biome_asset;
		if (tBiomeAsset == null)
		{
			return;
		}
		if (!tBiomeAsset.pot_spawn_units_auto)
		{
			return;
		}
		string tUnitID = tBiomeAsset.pot_units_spawn.GetRandom<string>();
		ActorAsset tActorAsset = AssetManager.actor_library.get(tUnitID);
		if (tActorAsset == null)
		{
			return;
		}
		if (tActorAsset.units.Count > tActorAsset.max_random_amount)
		{
			return;
		}
		int tCountActors = 0;
		foreach (Actor actor in Finder.getUnitsFromChunk(tTile, 1, 0f, false))
		{
			if (tCountActors++ > 3)
			{
				return;
			}
		}
		int tRandomUnits = Randy.randomInt(2, 5);
		for (int i = 0; i < tRandomUnits; i++)
		{
			World.world.units.spawnNewUnit(tActorAsset.id, tTile, false, false, 6f, null, true, false);
		}
	}

	// Token: 0x06001D66 RID: 7526 RVA: 0x00106CB0 File Offset: 0x00104EB0
	public static void growMinerals()
	{
		if (!WorldLawLibrary.world_law_grow_minerals.isEnabled())
		{
			return;
		}
		for (int i = 0; i < World.world.islands_calculator.islands_ground.Count; i++)
		{
			TileIsland tIsland = World.world.islands_calculator.islands_ground[i];
			if (tIsland.getTileCount() > 20)
			{
				MapRegion tRegion = tIsland.regions.GetRandom();
				if (!WorldBehaviourActions.mineralsAroundZone(tRegion.getRandomTile().zone))
				{
					int tAmount = tIsland.regions.Count / 20 + 1;
					tAmount = Mathf.Min(tAmount, 3);
					for (int j = 0; j < tAmount; j++)
					{
						foreach (WorldTile tTile in tRegion.tiles.LoopRandom<WorldTile>())
						{
							BiomeAsset tBioAsset = tTile.getBiome();
							if (tBioAsset != null && tBioAsset.grow_minerals_auto && !tTile.isOnFire() && !Randy.randomBool())
							{
								BuildingActions.tryGrowMineralRandom(tTile, false, true);
								break;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06001D67 RID: 7527 RVA: 0x00106DD4 File Offset: 0x00104FD4
	private static bool mineralsAroundZone(TileZone pZone)
	{
		if (pZone.hasAnyBuildingsInSet(BuildingList.Minerals))
		{
			return true;
		}
		TileZone[] tNeighbours = pZone.neighbours_all;
		for (int i = 0; i < tNeighbours.Length; i++)
		{
			if (tNeighbours[i].hasAnyBuildingsInSet(BuildingList.Minerals))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001D68 RID: 7528 RVA: 0x00106E10 File Offset: 0x00105010
	public static bool tryGrowVegetation(Building pBuilding, bool pCheckLimit = true)
	{
		if (!pBuilding.isUsable())
		{
			return false;
		}
		BuildingAsset tTempPlant = pBuilding.asset;
		WorldTile tTargetTile = pBuilding.tiles.GetRandom<WorldTile>();
		int tRandomDeep = 6;
		for (int i = 0; i < tRandomDeep; i++)
		{
			tTargetTile = tTargetTile.neighboursAll.GetRandom<WorldTile>();
		}
		if (WorldBehaviourActions.tryToGrowOnTile(tTargetTile, tTempPlant, pCheckLimit))
		{
			return true;
		}
		WorldBehaviourActions._spread_fauna_list.Clear();
		WorldBehaviourActions._spread_fauna_list.AddRange(pBuilding.current_tile.region.neighbours);
		WorldBehaviourActions._spread_fauna_list.Add(pBuilding.current_tile.region);
		return WorldBehaviourActions.tryToGrowOnTile(WorldBehaviourActions._spread_fauna_list.GetRandom<MapRegion>().getRandomTile(), tTempPlant, pCheckLimit);
	}

	// Token: 0x06001D69 RID: 7529 RVA: 0x00106EB0 File Offset: 0x001050B0
	private static bool tryToGrowOnTile(WorldTile pTile, BuildingAsset pAsset, bool pCheckLimit = true)
	{
		if (pCheckLimit && pTile.zone.hasReachedBuildingLimit(pTile, pAsset))
		{
			return false;
		}
		if (!World.world.buildings.canBuildFrom(pTile, pAsset, null, BuildPlacingType.New, false))
		{
			return false;
		}
		World.world.buildings.addBuilding(pAsset, pTile, false, false, BuildPlacingType.New);
		World.world.game_stats.data.treesGrown += 1L;
		return true;
	}

	// Token: 0x06001D6A RID: 7530 RVA: 0x00106F1C File Offset: 0x0010511C
	public static void spawnRandomVegetation()
	{
		if (!WorldLawLibrary.world_law_vegetation_random_seeds.isEnabled())
		{
			return;
		}
		if (!World.world_era.grow_vegetation)
		{
			return;
		}
		for (int i = 0; i < 5; i++)
		{
			MapRegion tRegion = World.world.map_chunk_manager.chunks.GetRandom<MapChunk>().regions.GetRandom<MapRegion>();
			if (tRegion.island.type == TileLayerType.Ground)
			{
				WorldBehaviourActions.growVegetationAt(tRegion, 5);
			}
		}
	}

	// Token: 0x06001D6B RID: 7531 RVA: 0x00106F84 File Offset: 0x00105184
	public static void growVegetationAt(MapRegion pRegion, int pAmount)
	{
		for (int i = 0; i < pAmount; i++)
		{
			WorldTile tTile = pRegion.tiles.GetRandom<WorldTile>();
			if (!tTile.isOnFire())
			{
				BiomeAsset tBiomeAsset = tTile.Type.biome_asset;
				if (tBiomeAsset != null && tBiomeAsset.grow_vegetation_auto && !Randy.randomBool())
				{
					ActionLibrary.growRandomVegetation(tTile, tBiomeAsset);
				}
			}
		}
	}

	// Token: 0x06001D6C RID: 7532 RVA: 0x00106FD6 File Offset: 0x001051D6
	public static bool addForGrinReaper(NanoObject pObject, BaseAugmentationAsset pAugmentationAsset)
	{
		return !pObject.isRekt() && WorldBehaviourActions._used_for_grin_reaper.Add((IMetaObject)pObject);
	}

	// Token: 0x06001D6D RID: 7533 RVA: 0x00106FF2 File Offset: 0x001051F2
	public static bool removeUsedForGrinReaper(NanoObject pObject, BaseAugmentationAsset pAugmentationAsset)
	{
		return pObject != null && WorldBehaviourActions._used_for_grin_reaper.Remove((IMetaObject)pObject);
	}

	// Token: 0x06001D6E RID: 7534 RVA: 0x00107009 File Offset: 0x00105209
	public static void clearGrinReaper()
	{
		WorldBehaviourActions._used_for_grin_reaper.Clear();
	}

	// Token: 0x06001D6F RID: 7535 RVA: 0x00107018 File Offset: 0x00105218
	public static void updateGrinReaper()
	{
		if (WorldBehaviourActions._used_for_grin_reaper.Count == 0)
		{
			return;
		}
		if (World.world.isWindowOnScreen())
		{
			return;
		}
		Actor tActor = WorldBehaviourActions.getRandomActorForReaper();
		if (tActor.isRekt())
		{
			return;
		}
		if (tActor.isInMagnet())
		{
			return;
		}
		tActor.getHitFullHealth(AttackType.Smile);
		EffectsLibrary.spawnAt("fx_grin_reaper", tActor.current_position, tActor.actor_scale);
	}

	// Token: 0x06001D70 RID: 7536 RVA: 0x00107078 File Offset: 0x00105278
	private static Actor getRandomActorForReaper()
	{
		WorldBehaviourActions._used_for_grin_reaper.RemoveWhere((IMetaObject pMeta) => pMeta == null || !pMeta.isAlive());
		IMetaObject tRandomMeta = WorldBehaviourActions._used_for_grin_reaper.GetRandom<IMetaObject>();
		if (tRandomMeta == null)
		{
			return null;
		}
		if (!tRandomMeta.isAlive())
		{
			return null;
		}
		return tRandomMeta.getRandomActorForReaper();
	}

	// Token: 0x0400160E RID: 5646
	private static List<WorldTile> _temp_wheat_list = new List<WorldTile>();

	// Token: 0x0400160F RID: 5647
	private static List<MapRegion> _spread_fauna_list = new List<MapRegion>();

	// Token: 0x04001610 RID: 5648
	private static int _last_used_world_id = 0;

	// Token: 0x04001611 RID: 5649
	private static readonly HashSet<IMetaObject> _used_for_grin_reaper = new HashSet<IMetaObject>();
}
