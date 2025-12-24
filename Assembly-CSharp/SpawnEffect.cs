using System;

// Token: 0x0200034D RID: 845
public class SpawnEffect : BaseEffect
{
	// Token: 0x06002069 RID: 8297 RVA: 0x00115F83 File Offset: 0x00114183
	internal override void create()
	{
		base.create();
		this._animation = base.GetComponent<SpriteAnimation>();
	}

	// Token: 0x0600206A RID: 8298 RVA: 0x00115F97 File Offset: 0x00114197
	internal override void spawnOnTile(WorldTile pTile)
	{
		this.prepare(pTile, 0.5f);
	}

	// Token: 0x0600206B RID: 8299 RVA: 0x00115FA8 File Offset: 0x001141A8
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (this._eventUsed)
		{
			return;
		}
		if (this._animation.currentFrameIndex == 14)
		{
			this._eventUsed = true;
			if (this._event == "crabzilla")
			{
				GodPower tPower = AssetManager.powers.get("crabzilla");
				World.world.units.createNewUnit(tPower.actor_asset_id, this._tile, false, tPower.actor_spawn_height, null, null, true, false, false, false);
			}
		}
	}

	// Token: 0x0600206C RID: 8300 RVA: 0x00116026 File Offset: 0x00114226
	public void setEvent(string pEvent, WorldTile pTile)
	{
		this._tile = pTile;
		this._eventUsed = false;
		this._event = pEvent;
	}

	// Token: 0x04001795 RID: 6037
	private string _event;

	// Token: 0x04001796 RID: 6038
	private SpriteAnimation _animation;

	// Token: 0x04001797 RID: 6039
	private bool _eventUsed;

	// Token: 0x04001798 RID: 6040
	private WorldTile _tile;
}
