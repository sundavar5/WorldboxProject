using System;

// Token: 0x02000247 RID: 583
public class BuildingFruitGrowth : BaseBuildingComponent
{
	// Token: 0x060015FF RID: 5631 RVA: 0x000E160C File Offset: 0x000DF80C
	public override void update(float pElapsed)
	{
		if (!this.building.isNormal())
		{
			return;
		}
		if (!this.building.hasResourcesToCollect())
		{
			if (this._resource_reset_time > 0f)
			{
				this._resource_reset_time -= pElapsed;
				return;
			}
			this.building.setHaveResourcesToCollect(true);
			this.building.setScaleTween(0.75f, 0.2f, 1f, null, null, 0);
		}
	}

	// Token: 0x06001600 RID: 5632 RVA: 0x000E1679 File Offset: 0x000DF879
	public void reset()
	{
		this._resource_reset_time = 90f;
	}

	// Token: 0x04001273 RID: 4723
	private float _resource_reset_time;
}
