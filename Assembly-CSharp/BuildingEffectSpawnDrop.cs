using System;
using UnityEngine;

// Token: 0x02000246 RID: 582
public class BuildingEffectSpawnDrop : BaseBuildingComponent
{
	// Token: 0x060015FD RID: 5629 RVA: 0x000E157C File Offset: 0x000DF77C
	public override void update(float pElapsed)
	{
		if (this.building.data.hasFlag("stop_spawn_drops"))
		{
			return;
		}
		if (this._timer >= 0f)
		{
			this._timer -= pElapsed;
			return;
		}
		int tAmount = Mathf.CeilToInt(-(this._timer / this.building.asset.spawn_drop_interval));
		if (tAmount < 1)
		{
			tAmount = 1;
		}
		this._timer = this.building.asset.spawn_drop_interval;
		this.building.spawnBurstSpecial(tAmount);
	}

	// Token: 0x04001272 RID: 4722
	private float _timer;
}
