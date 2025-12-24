using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200034A RID: 842
public class PixelFlashEffects : MapLayer
{
	// Token: 0x0600205F RID: 8287 RVA: 0x00115B40 File Offset: 0x00113D40
	internal override void create()
	{
		this.colors_amount = 30;
		this.colorValues = new Color(1f, 1f, 1f);
		this.colorWhite = new ColorArray(1f, 1f, 1f, 1f, (float)this.colors_amount, 0.5f);
		this.colorPurple = new ColorArray(ConwayLife.colorEater, this.colors_amount);
		this.colorBlue = new ColorArray(Toolbox.makeColor("#3BCC55"), this.colors_amount);
		base.create();
		base.enabled = true;
	}

	// Token: 0x06002060 RID: 8288 RVA: 0x00115BE0 File Offset: 0x00113DE0
	public void flashPixel(WorldTile pTile, int pVal = -1, ColorType pColorType = ColorType.White)
	{
		if (SmoothLoader.isLoading())
		{
			return;
		}
		if (pVal == -1 || pVal >= this.colors_amount)
		{
			pVal = this.colors_amount - 1;
		}
		if (!base.enabled)
		{
			return;
		}
		switch (pColorType)
		{
		case ColorType.White:
			pTile.color_array = this.colorWhite;
			break;
		case ColorType.Purple:
			pTile.color_array = this.colorPurple;
			break;
		case ColorType.Blue:
			pTile.color_array = this.colorBlue;
			break;
		}
		if (pTile.flash_state <= 0)
		{
			this.pixels_to_update.Add(pTile);
		}
		if (pTile.flash_state < pVal)
		{
			pTile.flash_state = pVal;
		}
	}

	// Token: 0x06002061 RID: 8289 RVA: 0x00115C77 File Offset: 0x00113E77
	internal override void clear()
	{
		base.clear();
		this.pixels_to_update.Clear();
	}

	// Token: 0x06002062 RID: 8290 RVA: 0x00115C8C File Offset: 0x00113E8C
	protected override void UpdateDirty(float pElapsed)
	{
		if (this._timer > 0f)
		{
			this._timer -= World.world.delta_time;
			return;
		}
		this._timer = 0.01f;
		if (this.pixels_to_update.Count > 0)
		{
			this.toRemove.Clear();
			foreach (WorldTile tTile in this.pixels_to_update)
			{
				if (tTile.flash_state < 0)
				{
					this.toRemove.Add(tTile);
				}
				else
				{
					this.pixels[tTile.data.tile_id] = tTile.color_array.colors[tTile.flash_state];
					tTile.flash_state--;
				}
			}
			for (int i = 0; i < this.toRemove.Count; i++)
			{
				WorldTile tTile2 = this.toRemove[i];
				this.pixels_to_update.Remove(tTile2);
			}
			base.updatePixels();
		}
	}

	// Token: 0x0400178A RID: 6026
	private List<WorldTile> toRemove = new List<WorldTile>();

	// Token: 0x0400178B RID: 6027
	private ColorArray colorWhite;

	// Token: 0x0400178C RID: 6028
	private ColorArray colorPurple;

	// Token: 0x0400178D RID: 6029
	private ColorArray colorBlue;

	// Token: 0x0400178E RID: 6030
	private float _timer;
}
