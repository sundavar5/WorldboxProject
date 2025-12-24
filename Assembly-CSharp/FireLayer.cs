using System;
using UnityEngine;

// Token: 0x0200033E RID: 830
public class FireLayer : MapLayer
{
	// Token: 0x0600200C RID: 8204 RVA: 0x00113CD7 File Offset: 0x00111ED7
	internal override void create()
	{
		base.create();
	}

	// Token: 0x0600200D RID: 8205 RVA: 0x00113CDF File Offset: 0x00111EDF
	public void setTileDirty(WorldTile pTile)
	{
		if (!this.pixels_to_update.Contains(pTile))
		{
			this.pixels_to_update.Add(pTile);
		}
	}

	// Token: 0x0600200E RID: 8206 RVA: 0x00113CFC File Offset: 0x00111EFC
	protected override void checkAutoDisable()
	{
		bool tEnabled = WorldBehaviourActionFire.hasFires();
		if (!MapBox.isRenderMiniMap())
		{
			tEnabled = false;
		}
		if (tEnabled)
		{
			if (!this.sprRnd.enabled)
			{
				this.sprRnd.enabled = true;
				return;
			}
		}
		else if (this.sprRnd.enabled)
		{
			this.sprRnd.enabled = false;
		}
	}

	// Token: 0x0600200F RID: 8207 RVA: 0x00113D50 File Offset: 0x00111F50
	protected override void UpdateDirty(float pElapsed)
	{
		if (this.pixels_to_update.Count > 0)
		{
			Color tColor = Toolbox.color_fire;
			foreach (WorldTile tTile in this.pixels_to_update)
			{
				if (tTile.isOnFire())
				{
					float tFireTime = World.world.getWorldTimeElapsedSince(tTile.data.fire_timestamp);
					tColor.a = 0.5f + (1f - tFireTime / SimGlobals.m.fire_stop_time);
					this.pixels[tTile.data.tile_id] = tColor;
				}
				else
				{
					this.pixels[tTile.data.tile_id] = Toolbox.clear;
				}
			}
			this.pixels_to_update.Clear();
			base.updatePixels();
		}
	}
}
