using System;

// Token: 0x02000098 RID: 152
public class WorldBehaviourLibrary : AssetLibrary<WorldBehaviourAsset>
{
	// Token: 0x060004C9 RID: 1225 RVA: 0x00032814 File Offset: 0x00030A14
	public override void init()
	{
		base.init();
		this.add(new WorldBehaviourAsset
		{
			id = "grin_reaper",
			interval = 0.01f,
			interval_random = 0.01f,
			stop_when_world_on_pause = false,
			action = new WorldBehaviourAction(WorldBehaviourActions.updateGrinReaper),
			action_world_clear = new WorldBehaviourAction(WorldBehaviourActions.clearGrinReaper)
		});
		this.add(new WorldBehaviourAsset
		{
			id = "zones_meta_data_visualizer",
			interval = 0.4f,
			interval_random = 0f,
			stop_when_world_on_pause = false,
			action = new WorldBehaviourAction(ZoneMetaDataVisualizer.updateMetaZones)
		});
		this.add(new WorldBehaviourAsset
		{
			id = "debug_highlight",
			interval = 0.1f,
			interval_random = 0f,
			stop_when_world_on_pause = false,
			action = new WorldBehaviourAction(DebugHighlight.updateDebugHighlights)
		});
		this.add(new WorldBehaviourAsset
		{
			id = "disasters",
			interval = 80f,
			interval_random = 0f,
			action = new WorldBehaviourAction(WorldBehaviourActions.updateDisasters)
		});
		this.add(new WorldBehaviourAsset
		{
			id = "migrants",
			interval = 80f,
			interval_random = 0f,
			action = new WorldBehaviourAction(WorldBehaviourActions.updateMigrants)
		});
		this.add(new WorldBehaviourAsset
		{
			id = "decay_roads",
			interval = 1f,
			interval_random = 1f,
			action = new WorldBehaviourAction(WorldBehaviourActions.updateRoadDecay)
		});
		this.add(new WorldBehaviourAsset
		{
			id = "decay_farms",
			interval = 1f,
			interval_random = 1f,
			action = new WorldBehaviourAction(WorldBehaviourActions.updateFarmDecay),
			action_world_clear = new WorldBehaviourAction(WorldBehaviourActions.clearFarmDecay)
		});
		this.add(new WorldBehaviourAsset
		{
			id = "spawn_random_vegetation",
			interval = 10f,
			interval_random = 10f,
			action = new WorldBehaviourAction(WorldBehaviourActions.spawnRandomVegetation)
		});
		this.add(new WorldBehaviourAsset
		{
			id = "spawn_minerals",
			interval = 2f,
			interval_random = 3f,
			action = new WorldBehaviourAction(WorldBehaviourActions.growMinerals)
		});
		this.add(new WorldBehaviourAsset
		{
			id = "building_sparks",
			interval = 0.1f,
			interval_random = 1f,
			enabled_on_minimap = false,
			action = new WorldBehaviourAction(WorldBehaviourActions.buildingSparks)
		});
		this.add(new WorldBehaviourAsset
		{
			id = "unit_spawn",
			interval = 24.5f,
			interval_random = 30f,
			action = new WorldBehaviourAction(WorldBehaviourActions.updateUnitSpawn)
		});
		this.add(new WorldBehaviourAsset
		{
			id = "ruins_rat_spawn",
			interval = 30.5f,
			interval_random = 3f,
			action = new WorldBehaviourAction(WorldBehaviourActions.updateRuinRatSpawn)
		});
		this.add(new WorldBehaviourAsset
		{
			id = "creep_decay",
			interval = 1f,
			interval_random = 0.3f,
			action = new WorldBehaviourAction(WorldBehaviourActionCreepDecay.checkCreep),
			action_world_clear = new WorldBehaviourAction(WorldBehaviourActionCreepDecay.clear)
		});
		this.add(new WorldBehaviourAsset
		{
			id = "ocean",
			interval = 0.2f,
			interval_random = 0f,
			action = new WorldBehaviourAction(WorldBehaviourOcean.updateOcean),
			action_world_clear = new WorldBehaviourAction(WorldBehaviourOcean.clear)
		});
		this.add(new WorldBehaviourAsset
		{
			id = "tiles_freeze",
			interval = 0.1f,
			interval_random = 0f,
			action = new WorldBehaviourAction(WorldBehaviourTilesTemperatureFreeze.update),
			action_world_clear = new WorldBehaviourAction(WorldBehaviourTilesRunner.clearTilesToCheck)
		});
		this.add(new WorldBehaviourAsset
		{
			id = "tiles_unfreeze",
			interval = 0.01f,
			interval_random = 0f,
			action = new WorldBehaviourAction(WorldBehaviourTilesTemperatureUnFreeze.update),
			action_world_clear = new WorldBehaviourAction(WorldBehaviourTilesRunner.clearTilesToCheck)
		});
		this.add(new WorldBehaviourAsset
		{
			id = "biomes",
			interval = 0.05f,
			interval_random = 0.05f,
			action = new WorldBehaviourAction(WorldBehaviourActionBiomes.update),
			action_world_clear = new WorldBehaviourAction(WorldBehaviourActionBiomes.clear)
		});
		this.add(new WorldBehaviourAsset
		{
			id = "lava",
			interval = 0.05f,
			interval_random = 0.05f,
			action = new WorldBehaviourAction(WorldBehaviourActionLava.update),
			action_world_clear = new WorldBehaviourAction(WorldBehaviourTilesRunner.clearTilesToCheck)
		});
		this.add(new WorldBehaviourAsset
		{
			id = "fire",
			interval = 1f,
			interval_random = 0f,
			action = new WorldBehaviourAction(WorldBehaviourActionFire.updateFire),
			action_world_clear = new WorldBehaviourAction(WorldBehaviourActionFire.clear)
		});
		this.add(new WorldBehaviourAsset
		{
			id = "burned_tiles",
			interval = 3f,
			interval_random = 5f,
			action = new WorldBehaviourAction(WorldBehaviourActionBurnedTiles.updateBurnedTiles),
			action_world_clear = new WorldBehaviourAction(WorldBehaviourActionBurnedTiles.clear)
		});
		this.add(new WorldBehaviourAsset
		{
			id = "erosion",
			interval = 3f,
			interval_random = 1f,
			action = new WorldBehaviourAction(WorldBehaviourActionErosion.updateErosion),
			action_world_clear = new WorldBehaviourAction(WorldBehaviourActionErosion.clear)
		});
		this.add(new WorldBehaviourAsset
		{
			id = "biome_inferno_fires",
			interval = 0.5f,
			interval_random = 1f,
			action = new WorldBehaviourAction(WorldBehaviourActionInferno.updateInfernoFireAction),
			enabled_on_minimap = false
		});
		this.add(new WorldBehaviourAsset
		{
			id = "biome_inferno_tile_low_animations",
			interval = 0.5f,
			interval_random = 0.5f,
			action = new WorldBehaviourAction(WorldBehaviourActionInferno.updateInfernalLowAnimations),
			enabled_on_minimap = false
		});
		this.add(new WorldBehaviourAsset
		{
			id = "biome_singularity_tile_animations",
			interval = 0.5f,
			interval_random = 0.5f,
			action = new WorldBehaviourAction(WorldBehaviourActionSingularity.updateSingularityTiles),
			enabled_on_minimap = false
		});
		this.add(new WorldBehaviourAsset
		{
			id = "creep_biomass",
			interval = 0.1f,
			interval_random = 0.1f,
			action = new WorldBehaviourAction(WorldBehaviourActionCreepBiomass.updateBiomassTiles),
			enabled_on_minimap = false
		});
		this.add(new WorldBehaviourAsset
		{
			id = "biome_low_swamp_animations",
			interval = 0.1f,
			interval_random = 0.1f,
			action = new WorldBehaviourAction(WorldBehaviourActionSwampAnimation.updateSwampTiles),
			enabled_on_minimap = false
		});
		this.add(new WorldBehaviourAsset
		{
			id = "unit_temperatures",
			interval = 0.1f,
			interval_random = 0.1f,
			action = new WorldBehaviourAction(WorldBehaviourUnitTemperatures.updateUnitTemperatures),
			action_world_clear = new WorldBehaviourAction(WorldBehaviourUnitTemperatures.clear)
		});
		this.add(new WorldBehaviourAsset
		{
			id = "waves",
			interval = 0.1f,
			interval_random = 0.1f,
			enabled_on_minimap = false,
			action = new WorldBehaviourAction(WorldBehaviourTileEffects.tryToStartTileEffects)
		});
		this.add(new WorldBehaviourAsset
		{
			id = "clouds",
			interval = 1f,
			interval_random = 1f,
			action = new WorldBehaviourAction(WorldBehaviourClouds.spawnRandomCloud)
		});
		WorldBehaviourAsset worldBehaviourAsset = new WorldBehaviourAsset();
		worldBehaviourAsset.id = "achievements_checks";
		worldBehaviourAsset.interval = 5f;
		worldBehaviourAsset.action = delegate()
		{
			AchievementLibrary.great_plague.check(null);
			AchievementLibrary.eternal_chaos.checkBySignal(null);
			AchievementLibrary.minefield.checkBySignal(null);
			AchievementLibrary.rain_tornado.checkBySignal(null);
			AchievementLibrary.ancient_war_of_geometry_and_evil.checkBySignal(null);
		};
		this.add(worldBehaviourAsset);
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x000330B0 File Offset: 0x000312B0
	public void createManagers()
	{
		foreach (WorldBehaviourAsset worldBehaviourAsset in this.list)
		{
			worldBehaviourAsset.manager = new WorldBehaviour(worldBehaviourAsset);
		}
	}
}
