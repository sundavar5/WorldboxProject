using System;
using System.Collections.Generic;
using Unity.Mathematics;

// Token: 0x020002F1 RID: 753
public class BatchBuildings : Batch<Building>
{
	// Token: 0x06001C5B RID: 7259 RVA: 0x00101A10 File Offset: 0x000FFC10
	protected override void createJobs()
	{
		base.addJob(null, new JobUpdater(this.prepare), JobType.Parallel, "prepare", 0);
		base.createJob(out this.c_scale, new JobUpdater(this.updateScale), JobType.Parallel, "update_scale", 0);
		base.createJob(out this.c_angle, new JobUpdater(this.updateAngle), JobType.Parallel, "update_angle", 0);
		base.createJob(out this.c_resource_shaker, new JobUpdater(this.updateResourceShaker), JobType.Parallel, "update_resource_shaker", 0);
		base.createJob(out this.c_stats_dirty, new JobUpdater(this.updateStatsDirty), JobType.Parallel, "update_dirty_stats", 0);
		base.createJob(out this.c_shake, new JobUpdater(this.updateShake), JobType.Parallel, "update_shake", 0);
		base.createJob(out this.c_main, new JobUpdater(this.updateVisibility), JobType.Parallel, "update_visibility", 0);
		base.createJob(out this.c_tiles_dirty, new JobUpdater(this.updateTilesDirty), JobType.Post, "update_dirty_tiles", 0);
		base.createJob(out this.c_auto_remove, new JobUpdater(this.updateAutoRemove), JobType.Post, "update_auto_remove", 0);
		base.createJob(out this.c_components, new JobUpdater(this.updateComponents), JobType.Post, "update_components", 0);
		base.createJob(out this.c_spread_trees, new JobUpdater(this.updateSpreadTrees), JobType.Post, "update_spread_trees", 0);
		base.createJob(out this.c_spread_plants, new JobUpdater(this.updateSpreadPlants), JobType.Post, "update_spread_plants", 0);
		base.createJob(out this.c_spread_fungi, new JobUpdater(this.updateSpreadFungi), JobType.Post, "update_spread_fungi", 0);
		base.createJob(out this.c_poop, new JobUpdater(this.updatePoopTurningIntoFlora), JobType.Post, "update_poop_turning_into_flora", 0);
		base.createJob(out this.c_position_dirty, new JobUpdater(this.updatePositionsDirty), JobType.Post, "update_dirty_positions", 0);
		this.main = this.c_main;
		this.applyParallelResults = (JobUpdater)Delegate.Combine(this.applyParallelResults, new JobUpdater(this.applyTweenActions));
	}

	// Token: 0x06001C5C RID: 7260 RVA: 0x00101C18 File Offset: 0x000FFE18
	public void applyTweenActions()
	{
		if (this.actions_to_run.Count == 0)
		{
			return;
		}
		for (int i = 0; i < this.actions_to_run.Count; i++)
		{
			this.actions_to_run[i]();
		}
		this.actions_to_run.Clear();
	}

	// Token: 0x06001C5D RID: 7261 RVA: 0x00101C65 File Offset: 0x000FFE65
	internal override void clear()
	{
		base.clear();
		JobUpdater clearParallelResults = this.clearParallelResults;
		if (clearParallelResults != null)
		{
			clearParallelResults();
		}
		this.actions_to_run.Clear();
	}

	// Token: 0x06001C5E RID: 7262 RVA: 0x00101C8C File Offset: 0x000FFE8C
	private void updateScale()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		Building[] tArr = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArr[i].updateScale();
		}
	}

	// Token: 0x06001C5F RID: 7263 RVA: 0x00101CCC File Offset: 0x000FFECC
	private void updateAngle()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		Building[] tArr = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArr[i].updateAngle(this._elapsed);
		}
	}

	// Token: 0x06001C60 RID: 7264 RVA: 0x00101D10 File Offset: 0x000FFF10
	private void updateVisibility()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		bool tRenderEnabled = MapBox.isRenderGameplay();
		bool tRenderBuildings = World.world.quality_changer.shouldRenderBuildings();
		if (!DebugConfig.isOn(DebugOption.ScaleEffectEnabled) && tRenderBuildings && !tRenderEnabled)
		{
			tRenderBuildings = false;
		}
		Building[] tArr = this._array;
		int tCount = this._count;
		if (tRenderEnabled)
		{
			for (int i = 0; i < tCount; i++)
			{
				Building building = tArr[i];
				building.is_visible = building.current_tile.zone.visible;
			}
			return;
		}
		for (int j = 0; j < tCount; j++)
		{
			tArr[j].is_visible = tRenderBuildings;
		}
	}

	// Token: 0x06001C61 RID: 7265 RVA: 0x00101DAC File Offset: 0x000FFFAC
	private void updateTilesDirty()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		Building[] tArr = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArr[i].checkDirtyTiles();
		}
	}

	// Token: 0x06001C62 RID: 7266 RVA: 0x00101DEC File Offset: 0x000FFFEC
	private void updateAutoRemove()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		Building[] tArr = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArr[i].updateAutoRemove(this._elapsed);
		}
	}

	// Token: 0x06001C63 RID: 7267 RVA: 0x00101E30 File Offset: 0x00100030
	private void updateStatsDirty()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		Building[] tArr = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArr[i].updateStats();
		}
	}

	// Token: 0x06001C64 RID: 7268 RVA: 0x00101E70 File Offset: 0x00100070
	private void updateComponents()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		Building[] tArr = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			Building tBuilding = tArr[i];
			if (tBuilding.isUsable())
			{
				tBuilding.updateComponents(this._elapsed);
			}
		}
	}

	// Token: 0x06001C65 RID: 7269 RVA: 0x00101ECC File Offset: 0x001000CC
	private void updateSpreadTrees()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		if (!WorldLawLibrary.world_law_spread_trees.isEnabled())
		{
			return;
		}
		if (this._timer_spread_trees >= 0f)
		{
			this._timer_spread_trees -= this._elapsed;
			if (this._timer_spread_trees > 0f)
			{
				return;
			}
			this._timer_spread_trees = WorldLawLibrary.getIntervalSpreadTrees();
		}
		Building[] tArr = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			Building tBuilding = tArr[i];
			if (tBuilding.isUsable())
			{
				tBuilding.checkVegetationSpread(this._elapsed);
			}
		}
	}

	// Token: 0x06001C66 RID: 7270 RVA: 0x00101F70 File Offset: 0x00100170
	private void updateSpreadPlants()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		if (!WorldLawLibrary.world_law_spread_plants.isEnabled())
		{
			return;
		}
		if (this._timer_spread_plants >= 0f)
		{
			this._timer_spread_plants -= this._elapsed;
			if (this._timer_spread_plants > 0f)
			{
				return;
			}
			this._timer_spread_plants = WorldLawLibrary.getIntervalSpreadPlants();
		}
		Building[] tArr = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			Building tBuilding = tArr[i];
			if (tBuilding.isUsable())
			{
				tBuilding.checkVegetationSpread(this._elapsed);
			}
		}
	}

	// Token: 0x06001C67 RID: 7271 RVA: 0x00102014 File Offset: 0x00100214
	private void updatePoopTurningIntoFlora()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		if (this._timer_poop_flora >= 0f)
		{
			this._timer_poop_flora -= this._elapsed;
			if (this._timer_poop_flora > 0f)
			{
				return;
			}
			this._timer_poop_flora = 5f;
		}
		Building[] tArr = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			Building tBuilding = tArr[i];
			if (tBuilding.isUsable() && tBuilding.getExistenceMonths() >= (float)SimGlobals.m.months_till_pool_turns_into_flora && !Randy.randomChance(0.7f))
			{
				WorldTile tTile = tBuilding.current_tile;
				BiomeAsset tBiomeAsset = tTile.Type.biome_asset;
				if (tBiomeAsset != null && tBiomeAsset.grow_type_selector_plants != null)
				{
					tBuilding.startDestroyBuilding();
					BuildingActions.tryGrowVegetationRandom(tTile, VegetationType.Plants, false, false, false);
				}
			}
		}
	}

	// Token: 0x06001C68 RID: 7272 RVA: 0x001020F0 File Offset: 0x001002F0
	private void updateSpreadFungi()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		if (!WorldLawLibrary.world_law_spread_fungi.isEnabled())
		{
			return;
		}
		if (this._timer_spread_fungi >= 0f)
		{
			this._timer_spread_fungi -= this._elapsed;
			if (this._timer_spread_fungi > 0f)
			{
				return;
			}
			this._timer_spread_fungi = WorldLawLibrary.getIntervalSpreadFungi();
		}
		Building[] tArr = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			Building tBuilding = tArr[i];
			if (tBuilding.isUsable())
			{
				tBuilding.checkVegetationSpread(this._elapsed);
			}
		}
	}

	// Token: 0x06001C69 RID: 7273 RVA: 0x00102194 File Offset: 0x00100394
	private void updateResourceShaker()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		if (World.world.isPaused())
		{
			return;
		}
		Building[] tArr = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			Building tBuilding = tArr[i];
			if (tBuilding.isUsable())
			{
				tBuilding.updateTimerShakeResources(this._elapsed);
			}
		}
	}

	// Token: 0x06001C6A RID: 7274 RVA: 0x001021F0 File Offset: 0x001003F0
	private void updateShake()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		Building[] tArr = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArr[i].updateShake(this._elapsed);
		}
	}

	// Token: 0x06001C6B RID: 7275 RVA: 0x00102234 File Offset: 0x00100434
	private void updatePositionsDirty()
	{
		if (!base.check(this._cur_container))
		{
			return;
		}
		Building[] tArr = this._array;
		int tCount = this._count;
		for (int i = 0; i < tCount; i++)
		{
			tArr[i].updatePosition();
		}
	}

	// Token: 0x06001C6C RID: 7276 RVA: 0x00102272 File Offset: 0x00100472
	internal override void add(Building pBuilding)
	{
		base.add(pBuilding);
		pBuilding.batch = this;
	}

	// Token: 0x06001C6D RID: 7277 RVA: 0x00102282 File Offset: 0x00100482
	internal override void remove(Building pObject)
	{
		base.remove(pObject);
		pObject.batch = null;
	}

	// Token: 0x040015A4 RID: 5540
	public ObjectContainer<Building> c_main;

	// Token: 0x040015A5 RID: 5541
	public ObjectContainer<Building> c_scale;

	// Token: 0x040015A6 RID: 5542
	public ObjectContainer<Building> c_angle;

	// Token: 0x040015A7 RID: 5543
	public ObjectContainer<Building> c_components;

	// Token: 0x040015A8 RID: 5544
	public ObjectContainer<Building> c_spread_trees;

	// Token: 0x040015A9 RID: 5545
	public ObjectContainer<Building> c_spread_plants;

	// Token: 0x040015AA RID: 5546
	public ObjectContainer<Building> c_spread_fungi;

	// Token: 0x040015AB RID: 5547
	public ObjectContainer<Building> c_poop;

	// Token: 0x040015AC RID: 5548
	public ObjectContainer<Building> c_resource_shaker;

	// Token: 0x040015AD RID: 5549
	public ObjectContainer<Building> c_shake;

	// Token: 0x040015AE RID: 5550
	public ObjectContainer<Building> c_position_dirty;

	// Token: 0x040015AF RID: 5551
	public ObjectContainer<Building> c_tiles_dirty;

	// Token: 0x040015B0 RID: 5552
	public ObjectContainer<Building> c_stats_dirty;

	// Token: 0x040015B1 RID: 5553
	public ObjectContainer<Building> c_auto_remove;

	// Token: 0x040015B2 RID: 5554
	public Random rnd = new Random(10U);

	// Token: 0x040015B3 RID: 5555
	private float _timer_spread_trees;

	// Token: 0x040015B4 RID: 5556
	private float _timer_spread_plants;

	// Token: 0x040015B5 RID: 5557
	private float _timer_poop_flora;

	// Token: 0x040015B6 RID: 5558
	private float _timer_spread_fungi;

	// Token: 0x040015B7 RID: 5559
	public List<Action> actions_to_run = new List<Action>();
}
