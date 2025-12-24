using System;

// Token: 0x02000415 RID: 1045
public class WorldBehaviour
{
	// Token: 0x06002404 RID: 9220 RVA: 0x0012C7BC File Offset: 0x0012A9BC
	internal void clear()
	{
		this._timer = this._asset.interval;
		if (this._asset.action_world_clear != null)
		{
			this._asset.action_world_clear();
		}
	}

	// Token: 0x06002405 RID: 9221 RVA: 0x0012C7EC File Offset: 0x0012A9EC
	public WorldBehaviour(WorldBehaviourAsset pAsset)
	{
		this._asset = pAsset;
		this._timer = this._asset.interval;
	}

	// Token: 0x06002406 RID: 9222 RVA: 0x0012C80C File Offset: 0x0012AA0C
	public void timerClear()
	{
		this._timer = 0f;
	}

	// Token: 0x06002407 RID: 9223 RVA: 0x0012C81C File Offset: 0x0012AA1C
	public void update(float pElapsed)
	{
		if (MapBox.isRenderMiniMap() && !this._asset.enabled_on_minimap)
		{
			return;
		}
		if (World.world.isPaused() && this._asset.stop_when_world_on_pause)
		{
			return;
		}
		if (this._timer > 0f)
		{
			this._timer -= pElapsed;
			if (this._timer > 0f)
			{
				return;
			}
		}
		float tNewTime = this._asset.interval + Randy.randomFloat(0f, this._asset.interval_random);
		this._timer += tNewTime;
		this._asset.action();
	}

	// Token: 0x040019F1 RID: 6641
	private float _timer;

	// Token: 0x040019F2 RID: 6642
	private WorldBehaviourAsset _asset;
}
