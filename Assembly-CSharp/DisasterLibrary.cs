using System;
using Beebyte.Obfuscator;

// Token: 0x0200003D RID: 61
[ObfuscateLiterals]
public class DisasterLibrary : AssetLibrary<DisasterAsset>
{
	// Token: 0x06000271 RID: 625 RVA: 0x00015CB8 File Offset: 0x00013EB8
	public override void init()
	{
		base.init();
		this.add(new DisasterAsset
		{
			id = "tornado",
			rate = 3,
			chance = 0.5f,
			min_world_cities = 3,
			world_log = "disaster_tornado",
			min_world_population = 100,
			type = DisasterType.Nature
		});
		this.t.ages_allow.Add("age_tears");
		this.t.ages_allow.Add("age_ash");
		this.t.ages_allow.Add("age_chaos");
		this.t.ages_allow.Add("age_wonders");
		this.t.ages_allow.Add("age_moon");
		this.t.action = new DisasterAction(this.spawnTornado);
		this.add(new DisasterAsset
		{
			id = "heatwave",
			rate = 4,
			chance = 0.5f,
			world_log = "disaster_heatwave",
			premium_only = false,
			type = DisasterType.Nature
		});
		this.t.ages_allow.Add("age_sun");
		this.t.action = new DisasterAction(this.spawnHeatwave);
		this.add(new DisasterAsset
		{
			id = "small_meteorite",
			rate = 5,
			chance = 0.5f,
			world_log = "disaster_meteorite",
			min_world_population = 400,
			min_world_cities = 3,
			premium_only = true,
			type = DisasterType.Nature
		});
		this.t.ages_forbid.Add("age_hope");
		this.t.ages_forbid.Add("age_sun");
		this.t.action = new DisasterAction(this.spawnMeteorite);
		this.add(new DisasterAsset
		{
			id = "small_earthquake",
			rate = 3,
			chance = 0.4f,
			world_log = "disaster_earthquake",
			min_world_population = 400,
			min_world_cities = 5,
			type = DisasterType.Nature
		});
		this.t.ages_forbid.Add("age_hope");
		this.t.ages_forbid.Add("age_sun");
		this.t.action = new DisasterAction(this.spawnSmallEarthquake);
		this.add(new DisasterAsset
		{
			id = "hellspawn",
			rate = 2,
			chance = 0.9f,
			min_world_cities = 5,
			world_log = "disaster_hellspawn",
			min_world_population = 300,
			premium_only = true,
			spawn_asset_unit = "demon",
			max_existing_units = 5,
			units_min = 2,
			units_max = 5
		});
		this.t.ages_allow.Add("age_chaos");
		this.t.action = new DisasterAction(this.simpleUnitAssetSpawnUsingIslands);
		this.add(new DisasterAsset
		{
			id = "ice_ones_awoken",
			rate = 2,
			chance = 0.9f,
			min_world_cities = 5,
			world_log = "disaster_ice_ones",
			min_world_population = 300,
			premium_only = true,
			spawn_asset_unit = "cold_one",
			max_existing_units = 5,
			units_min = 10,
			units_max = 20
		});
		this.t.ages_allow.Add("age_despair");
		this.t.ages_allow.Add("age_ice");
		this.t.action = new DisasterAction(this.simpleUnitAssetSpawnUsingIslands);
		this.add(new DisasterAsset
		{
			id = "sudden_snowman",
			rate = 3,
			chance = 0.9f,
			min_world_cities = 5,
			world_log = "disaster_sudden_snowman",
			min_world_population = 100,
			spawn_asset_unit = "snowman",
			max_existing_units = 5,
			units_min = 20,
			units_max = 40
		});
		this.t.ages_allow.Add("age_ice");
		this.t.action = new DisasterAction(this.simpleUnitAssetSpawnUsingIslands);
		this.add(new DisasterAsset
		{
			id = "garden_surprise",
			rate = 1,
			chance = 0.9f,
			min_world_cities = 5,
			world_log = "disaster_garden_surprise",
			min_world_population = 800,
			spawn_asset_building = "super_pumpkin",
			spawn_asset_unit = "lil_pumpkin",
			max_existing_units = 5,
			units_min = 50,
			units_max = 100
		});
		this.t.ages_allow.Add("age_sun");
		this.t.ages_allow.Add("age_wonders");
		this.t.action = new DisasterAction(this.gardenSurprise);
		this.add(new DisasterAsset
		{
			id = "dragon_from_farlands",
			rate = 1,
			chance = 0.9f,
			min_world_cities = 10,
			world_log = "disaster_dragon_from_farlands",
			min_world_population = 3000,
			spawn_asset_unit = "dragon",
			max_existing_units = 1,
			units_min = 1,
			units_max = 1
		});
		this.t.ages_allow.Add("age_chaos");
		this.t.ages_allow.Add("age_dark");
		this.t.ages_allow.Add("age_despair");
		this.t.action = new DisasterAction(this.spawnDragon);
		this.add(new DisasterAsset
		{
			id = "ash_bandits",
			rate = 1,
			chance = 0.9f,
			min_world_cities = 10,
			world_log = "disaster_bandits",
			min_world_population = 700,
			spawn_asset_unit = "bandit",
			max_existing_units = 10,
			units_min = 15,
			units_max = 30
		});
		this.t.ages_allow.Add("age_ash");
		this.t.action = new DisasterAction(this.simpleUnitAssetSpawnUsingIslands);
		this.add(new DisasterAsset
		{
			id = "alien_invasion",
			rate = 1,
			chance = 0.9f,
			min_world_cities = 10,
			world_log = "disaster_alien_invasion",
			min_world_population = 1500,
			spawn_asset_unit = "UFO",
			max_existing_units = 1,
			units_min = 5,
			units_max = 10
		});
		this.t.ages_allow.Add("age_moon");
		this.t.action = new DisasterAction(this.startAlienInvasion);
		this.add(new DisasterAsset
		{
			id = "biomass",
			rate = 1,
			chance = 0.9f,
			min_world_cities = 10,
			world_log = "disaster_biomass",
			min_world_population = 700,
			spawn_asset_building = "biomass",
			spawn_asset_unit = "bioblob",
			max_existing_units = 10,
			units_min = 20,
			units_max = 30
		});
		this.t.ages_allow.Add("age_ash");
		this.t.action = new DisasterAction(this.spawnBiomass);
		this.add(new DisasterAsset
		{
			id = "tumor",
			rate = 1,
			chance = 0.9f,
			min_world_cities = 10,
			world_log = "disaster_tumor",
			min_world_population = 700,
			spawn_asset_building = "tumor",
			spawn_asset_unit = "tumor_monster_unit",
			max_existing_units = 10,
			units_min = 20,
			units_max = 30
		});
		this.t.ages_allow.Add("age_moon");
		this.t.action = new DisasterAction(this.spawnTumor);
		this.add(new DisasterAsset
		{
			id = "wild_mage",
			rate = 1,
			chance = 0.8f,
			world_log = "disaster_evil_mage",
			min_world_population = 400,
			min_world_cities = 5,
			premium_only = true,
			max_existing_units = 1,
			spawn_asset_unit = "evil_mage",
			units_min = 1,
			units_max = 1
		});
		this.t.ages_forbid.Add("age_hope");
		this.t.action = new DisasterAction(this.spawnEvilMage);
		this.add(new DisasterAsset
		{
			id = "underground_necromancer",
			rate = 2,
			chance = 0.9f,
			world_log = "disaster_underground_necromancer",
			min_world_population = 200,
			min_world_cities = 4,
			premium_only = true,
			spawn_asset_unit = "necromancer",
			max_existing_units = 1,
			units_min = 1,
			units_max = 1
		});
		this.t.ages_allow.Add("age_dark");
		this.t.ages_allow.Add("age_despair");
		this.t.action = new DisasterAction(this.spawnNecromancer);
		this.add(new DisasterAsset
		{
			id = "mad_thoughts",
			rate = 2,
			chance = 0.7f,
			world_log = "disaster_mad_thoughts",
			min_world_cities = 5,
			min_world_population = 150
		});
		this.t.ages_forbid.Add("age_hope");
		this.t.ages_forbid.Add("age_wonders");
		this.t.action = new DisasterAction(this.spawnMadThought);
		this.add(new DisasterAsset
		{
			id = "greg_abominations",
			rate = 1,
			chance = 0.5f,
			world_log = "disaster_greg_abominations",
			min_world_population = 1000,
			min_world_cities = 3,
			spawn_asset_unit = "greg",
			max_existing_units = 1,
			units_min = 30,
			units_max = 55
		});
		this.t.ages_allow.Add("age_despair");
		this.t.action = new DisasterAction(this.spawnGreg);
	}

	// Token: 0x06000272 RID: 626 RVA: 0x0001675C File Offset: 0x0001495C
	public DisasterAsset getRandomAssetFromPool()
	{
		DisasterAsset result;
		using (ListPool<DisasterAsset> tRandomPool = new ListPool<DisasterAsset>())
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				DisasterAsset tAsset = this.list[i];
				if ((tAsset.ages_allow.Count <= 0 || tAsset.ages_allow.Contains(World.world_era.id)) && (tAsset.ages_forbid.Count <= 0 || !tAsset.ages_forbid.Contains(World.world_era.id)))
				{
					for (int j = 0; j < tAsset.rate; j++)
					{
						tRandomPool.Add(tAsset);
					}
				}
			}
			if (tRandomPool.Count == 0)
			{
				result = null;
			}
			else
			{
				DisasterAsset tResult = tRandomPool.GetRandom<DisasterAsset>();
				if (tResult.type == DisasterType.Nature)
				{
					if (!WorldLawLibrary.world_law_disasters_nature.isEnabled())
					{
						return null;
					}
				}
				else if (tResult.type == DisasterType.Other && !WorldLawLibrary.world_law_disasters_other.isEnabled())
				{
					return null;
				}
				if (tResult.min_world_cities > World.world.cities.Count)
				{
					result = null;
				}
				else if (tResult.min_world_population > World.world.units.Count)
				{
					result = null;
				}
				else
				{
					result = tResult;
				}
			}
		}
		return result;
	}

	// Token: 0x06000273 RID: 627 RVA: 0x000168AC File Offset: 0x00014AAC
	public void spawnMadThought(DisasterAsset pAsset)
	{
		City tCity = World.world.cities.getRandom();
		if (tCity == null)
		{
			return;
		}
		if (tCity.getPopulationPeople() < 50)
		{
			return;
		}
		if (tCity.getTile(false) == null)
		{
			return;
		}
		using (ListPool<Actor> tMadUnits = new ListPool<Actor>(tCity.countUnits()))
		{
			foreach (Actor tUnit in tCity.units.LoopRandom<Actor>())
			{
				if (tUnit.city == tCity && Randy.randomChance(0.2f))
				{
					tMadUnits.Add(tUnit);
				}
			}
			for (int i = 0; i < tMadUnits.Count; i++)
			{
				tMadUnits[i].addTrait("madness", false);
			}
			WorldLog.logDisaster(pAsset, tCity.getTile(false), null, tCity, null);
		}
	}

	// Token: 0x06000274 RID: 628 RVA: 0x0001699C File Offset: 0x00014B9C
	public void spawnGreg(DisasterAsset pAsset)
	{
		if (!DebugConfig.isOn(DebugOption.Greg))
		{
			return;
		}
		if (!this.checkUnitSpawnLimits(pAsset))
		{
			return;
		}
		City tCity = World.world.cities.getRandom();
		if (tCity == null)
		{
			return;
		}
		Building tMine = tCity.getBuildingOfType("type_mine", true, false, false, null);
		if (tMine == null)
		{
			return;
		}
		WorldTile tSpawnTile = tMine.current_tile;
		Actor tUnit = this.spawnDisasterUnit(pAsset, tSpawnTile);
		WorldLog.logDisaster(pAsset, tSpawnTile, tUnit.getName(), tCity, tUnit);
		this.spawnDisasterUnit(pAsset, tSpawnTile.region.tiles.GetRandom<WorldTile>());
		AchievementLibrary.greg.check(null);
	}

	// Token: 0x06000275 RID: 629 RVA: 0x00016A28 File Offset: 0x00014C28
	public void spawnNecromancer(DisasterAsset pAsset)
	{
		if (!this.checkUnitSpawnLimits(pAsset))
		{
			return;
		}
		City tCity = World.world.cities.getRandom();
		if (tCity == null)
		{
			return;
		}
		Building tMine = tCity.getBuildingOfType("type_mine", true, false, false, null);
		if (tMine == null)
		{
			return;
		}
		WorldTile tSpawnTile = tMine.current_tile;
		Actor tNecromancer = this.spawnDisasterUnit(pAsset, tSpawnTile);
		WorldLog.logDisaster(pAsset, tSpawnTile, tNecromancer.getName(), tCity, tNecromancer);
		int numSkeletons = Randy.randomInt(5, 25);
		for (int i = 0; i < numSkeletons; i++)
		{
			World.world.units.createNewUnit("skeleton", tSpawnTile.region.tiles.GetRandom<WorldTile>(), false, 0f, null, null, true, true, false, false);
		}
	}

	// Token: 0x06000276 RID: 630 RVA: 0x00016AD4 File Offset: 0x00014CD4
	public void spawnEvilMage(DisasterAsset pAsset)
	{
		if (!this.checkUnitSpawnLimits(pAsset))
		{
			return;
		}
		TileIsland tIsland = World.world.islands_calculator.getRandomIslandGround(true);
		if (tIsland == null)
		{
			return;
		}
		WorldTile tRandomTile = tIsland.getRandomTile();
		Actor tActor = this.spawnDisasterUnit(pAsset, tRandomTile);
		WorldLog.logDisaster(pAsset, tRandomTile, tActor.getName(), null, tActor);
	}

	// Token: 0x06000277 RID: 631 RVA: 0x00016B20 File Offset: 0x00014D20
	public void spawnHeatwave(DisasterAsset pAsset)
	{
		if (!WorldLawLibrary.world_law_disasters_nature.isEnabled())
		{
			return;
		}
		int tFires = Randy.randomInt(1, 3);
		WorldTile tRandomIslandTile = null;
		bool tHeatwave = false;
		for (int i = 0; i < tFires; i++)
		{
			TileIsland tIsland = World.world.islands_calculator.getRandomIslandGround(true);
			if (tIsland != null)
			{
				tRandomIslandTile = tIsland.getRandomTile();
				tHeatwave = true;
				WorldTile[] neighboursAll = tRandomIslandTile.neighboursAll;
				for (int j = 0; j < neighboursAll.Length; j++)
				{
					neighboursAll[j].startFire(false);
				}
			}
		}
		if (!tHeatwave)
		{
			return;
		}
		WorldLog.logDisaster(pAsset, tRandomIslandTile, null, null, null);
	}

	// Token: 0x06000278 RID: 632 RVA: 0x00016BA8 File Offset: 0x00014DA8
	public void spawnSmallEarthquake(DisasterAsset pAsset)
	{
		WorldTile tRandomTile = World.world.tiles_list.GetRandom<WorldTile>();
		Earthquake.startQuake(tRandomTile, EarthquakeType.SmallDisaster);
		WorldLog.logDisaster(pAsset, tRandomTile, null, null, null);
	}

	// Token: 0x06000279 RID: 633 RVA: 0x00016BD8 File Offset: 0x00014DD8
	public void spawnDragon(DisasterAsset pAsset)
	{
		if (!this.checkUnitSpawnLimits(pAsset))
		{
			return;
		}
		TileZone tZone;
		if (Randy.randomBool())
		{
			tZone = World.world.zone_calculator.getZone(0, Randy.randomInt(0, World.world.zone_calculator.zones_total_y));
		}
		else
		{
			tZone = World.world.zone_calculator.getZone(World.world.zone_calculator.zones_total_x - 1, Randy.randomInt(0, World.world.zone_calculator.zones_total_y));
		}
		WorldTile tTile = tZone.centerTile;
		this.spawnDisasterUnit(pAsset, tTile);
		WorldLog.logDisaster(pAsset, tTile, null, null, null);
	}

	// Token: 0x0600027A RID: 634 RVA: 0x00016C70 File Offset: 0x00014E70
	public void startAlienInvasion(DisasterAsset pAsset)
	{
		if (!this.checkUnitSpawnLimits(pAsset))
		{
			return;
		}
		TileZone tZone;
		if (Randy.randomBool())
		{
			tZone = World.world.zone_calculator.getZone(0, Randy.randomInt(0, World.world.zone_calculator.zones_total_y));
		}
		else
		{
			tZone = World.world.zone_calculator.getZone(World.world.zone_calculator.zones_total_x - 1, Randy.randomInt(0, World.world.zone_calculator.zones_total_y));
		}
		WorldTile tTile = tZone.centerTile;
		this.spawnDisasterUnit(pAsset, tTile);
		WorldLog.logDisaster(pAsset, tTile, null, null, null);
	}

	// Token: 0x0600027B RID: 635 RVA: 0x00016D08 File Offset: 0x00014F08
	public void spawnBiomass(DisasterAsset pAsset)
	{
		if (!this.checkUnitSpawnLimits(pAsset))
		{
			return;
		}
		Building tBuildingTarget = null;
		foreach (City city in World.world.cities.list.LoopRandom<City>())
		{
			Building tRandomBuilding = Randy.getRandom<Building>(city.buildings);
			if (tRandomBuilding != null && tRandomBuilding.isUsable())
			{
				tBuildingTarget = tRandomBuilding;
				break;
			}
		}
		if (tBuildingTarget == null)
		{
			return;
		}
		WorldTile tTile = tBuildingTarget.tiles.GetRandom<WorldTile>();
		if (this.spawnDisasterBuilding(pAsset, tTile) == null)
		{
			return;
		}
		this.spawnDisasterUnit(pAsset, tTile);
		WorldLog.logDisaster(pAsset, tTile, null, null, null);
	}

	// Token: 0x0600027C RID: 636 RVA: 0x00016DB0 File Offset: 0x00014FB0
	public void spawnTumor(DisasterAsset pAsset)
	{
		if (!this.checkUnitSpawnLimits(pAsset))
		{
			return;
		}
		Building tBuildingTarget = null;
		foreach (City city in World.world.cities.list.LoopRandom<City>())
		{
			Building tRandomBuilding = Randy.getRandom<Building>(city.buildings);
			if (tRandomBuilding != null && tRandomBuilding.isUsable())
			{
				tBuildingTarget = tRandomBuilding;
				break;
			}
		}
		if (tBuildingTarget == null)
		{
			return;
		}
		WorldTile tTile = tBuildingTarget.tiles.GetRandom<WorldTile>();
		if (this.spawnDisasterBuilding(pAsset, tTile) == null)
		{
			return;
		}
		this.spawnDisasterUnit(pAsset, tTile);
		WorldLog.logDisaster(pAsset, tTile, null, null, null);
	}

	// Token: 0x0600027D RID: 637 RVA: 0x00016E58 File Offset: 0x00015058
	public void gardenSurprise(DisasterAsset pAsset)
	{
		if (!this.checkUnitSpawnLimits(pAsset))
		{
			return;
		}
		Building tBuildingTarget = null;
		foreach (City city in World.world.cities.list.LoopRandom<City>())
		{
			Building tWindmill = city.getBuildingOfType("type_windmill", true, false, false, null);
			if (tWindmill != null && tWindmill.isAlive())
			{
				tBuildingTarget = tWindmill;
				break;
			}
		}
		if (tBuildingTarget == null)
		{
			return;
		}
		WorldTile tTile = tBuildingTarget.tiles.GetRandom<WorldTile>();
		if (this.spawnDisasterBuilding(pAsset, tTile) == null)
		{
			return;
		}
		this.spawnDisasterUnit(pAsset, tTile);
		WorldLog.logDisaster(pAsset, tTile, null, null, null);
	}

	// Token: 0x0600027E RID: 638 RVA: 0x00016F04 File Offset: 0x00015104
	public void spawnTornado(DisasterAsset pAsset)
	{
		WorldTile tRandomTile = World.world.tiles_list.GetRandom<WorldTile>();
		EffectsLibrary.spawnAtTile("fx_tornado", tRandomTile, 0.5f);
		WorldLog.logDisaster(pAsset, tRandomTile, null, null, null);
	}

	// Token: 0x0600027F RID: 639 RVA: 0x00016F3C File Offset: 0x0001513C
	public void spawnMeteorite(DisasterAsset pAsset)
	{
		WorldTile tRandomTile = World.world.tiles_list.GetRandom<WorldTile>();
		Meteorite.spawnMeteoriteDisaster(tRandomTile, null);
		WorldLog.logDisaster(pAsset, tRandomTile, null, null, null);
	}

	// Token: 0x06000280 RID: 640 RVA: 0x00016F6C File Offset: 0x0001516C
	public void simpleUnitAssetSpawnUsingIslands(DisasterAsset pAsset)
	{
		if (!this.checkUnitSpawnLimits(pAsset))
		{
			return;
		}
		TileIsland tIsland = World.world.islands_calculator.getRandomIslandGround(true);
		if (tIsland == null)
		{
			return;
		}
		WorldTile tRandomIslandTile = tIsland.getRandomTile();
		this.spawnDisasterUnit(pAsset, tRandomIslandTile);
		WorldLog.logDisaster(pAsset, tRandomIslandTile, null, null, null);
	}

	// Token: 0x06000281 RID: 641 RVA: 0x00016FB2 File Offset: 0x000151B2
	private bool checkUnitSpawnLimits(DisasterAsset pAsset)
	{
		return !string.IsNullOrEmpty(pAsset.spawn_asset_unit) && AssetManager.actor_library.get(pAsset.spawn_asset_unit).units.Count < pAsset.max_existing_units;
	}

	// Token: 0x06000282 RID: 642 RVA: 0x00016FE8 File Offset: 0x000151E8
	private Actor spawnDisasterUnit(DisasterAsset pAsset, WorldTile pTile)
	{
		EffectsLibrary.spawn("fx_spawn", pTile, null, null, 0f, -1f, -1f, null);
		Actor tActor = null;
		int tAmount = Randy.randomInt(pAsset.units_min, pAsset.units_max);
		for (int i = 0; i < tAmount; i++)
		{
			tActor = World.world.units.createNewUnit(pAsset.spawn_asset_unit, pTile, false, 0f, null, null, true, true, true, false);
		}
		return tActor;
	}

	// Token: 0x06000283 RID: 643 RVA: 0x00017057 File Offset: 0x00015257
	private Building spawnDisasterBuilding(DisasterAsset pAsset, WorldTile pTile)
	{
		if (string.IsNullOrEmpty(pAsset.spawn_asset_building))
		{
			return null;
		}
		return World.world.buildings.addBuilding(pAsset.spawn_asset_building, pTile, true, false, BuildPlacingType.New);
	}
}
