using System;

// Token: 0x02000243 RID: 579
public class BuildingCreepHUB : BaseBuildingComponent
{
	// Token: 0x060015EF RID: 5615 RVA: 0x000E1014 File Offset: 0x000DF214
	internal override void create(Building pBuilding)
	{
		this._workers = new ListPool<BuildingCreepWorker>();
		base.create(pBuilding);
		for (int i = 0; i < this.building.asset.grow_creep_workers; i++)
		{
			this._workers.Add(new BuildingCreepWorker(this));
		}
		this._interval = this.building.asset.grow_creep_step_interval;
		this._timer = this._interval;
	}

	// Token: 0x060015F0 RID: 5616 RVA: 0x000E1084 File Offset: 0x000DF284
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (this._timer > 0f)
		{
			this._timer -= pElapsed;
			return;
		}
		this._timer = this._interval;
		ListPool<BuildingCreepWorker> tWorkers = this._workers;
		for (int i = 0; i < tWorkers.Count; i++)
		{
			tWorkers[i].update();
		}
	}

	// Token: 0x060015F1 RID: 5617 RVA: 0x000E10E4 File Offset: 0x000DF2E4
	public override void Dispose()
	{
		for (int i = 0; i < this._workers.Count; i++)
		{
			this._workers[i].Dispose();
		}
		this._workers.Dispose();
		this._workers = null;
		base.Dispose();
	}

	// Token: 0x04001264 RID: 4708
	private float _interval = 0.1f;

	// Token: 0x04001265 RID: 4709
	private float _timer;

	// Token: 0x04001266 RID: 4710
	private ListPool<BuildingCreepWorker> _workers;
}
