using System;

// Token: 0x0200024D RID: 589
public class IceTower : BaseBuildingComponent
{
	// Token: 0x0600162A RID: 5674 RVA: 0x000E22BB File Offset: 0x000E04BB
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (this._action_timer > 0f)
		{
			this._action_timer -= pElapsed;
			return;
		}
		this._action_timer = this._action_interval;
		this.freezeRandomTile();
	}

	// Token: 0x0600162B RID: 5675 RVA: 0x000E22F4 File Offset: 0x000E04F4
	private void freezeRandomTile()
	{
		WorldTile tCenter = this.building.current_tile;
		MapRegion region = tCenter.region;
		TileIsland tIsland = (region != null) ? region.island : null;
		if (tIsland == null)
		{
			return;
		}
		WorldTile tTile = tIsland.regions.GetRandom().tiles.GetRandom<WorldTile>();
		this.freeze(tCenter, tTile);
	}

	// Token: 0x0600162C RID: 5676 RVA: 0x000E2342 File Offset: 0x000E0542
	private void freeze(WorldTile pCenter, WorldTile pTile)
	{
		if (Toolbox.DistTile(pCenter, pTile) > 50f)
		{
			return;
		}
		pTile.freeze(1);
	}

	// Token: 0x04001284 RID: 4740
	private float _action_interval = 0.3f;

	// Token: 0x04001285 RID: 4741
	private float _action_timer;
}
