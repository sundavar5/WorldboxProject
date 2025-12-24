using System;
using UnityEngine;

// Token: 0x02000333 RID: 819
public class BurnedTilesLayer : MapLayer
{
	// Token: 0x06001FD6 RID: 8150 RVA: 0x00112478 File Offset: 0x00110678
	internal override void create()
	{
		this.colorValues = new Color(this.color.r, this.color.g, this.color.b, 0.5f);
		this.colors_amount = 15;
		this.autoDisable = false;
		base.create();
		base.enabled = true;
	}

	// Token: 0x06001FD7 RID: 8151 RVA: 0x001124D2 File Offset: 0x001106D2
	public void setTileDirty(WorldTile pTile)
	{
		if (!this.pixels_to_update.Contains(pTile))
		{
			this.pixels_to_update.Add(pTile);
		}
	}

	// Token: 0x06001FD8 RID: 8152 RVA: 0x001124F0 File Offset: 0x001106F0
	protected override void UpdateDirty(float pElapsed)
	{
		if (this.pixels_to_update.Count > 0)
		{
			foreach (WorldTile tTile in this.pixels_to_update)
			{
				if (tTile.burned_stages > 0)
				{
					this.pixels[tTile.data.tile_id] = this.colors[tTile.burned_stages - 1];
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

	// Token: 0x0400172E RID: 5934
	public Color color;

	// Token: 0x0400172F RID: 5935
	private WorldBehaviour worldBehaviour;
}
