using System;
using System.Collections.Generic;

// Token: 0x02000343 RID: 835
public class LavaLayer : MapLayer
{
	// Token: 0x06002037 RID: 8247 RVA: 0x00114B17 File Offset: 0x00112D17
	internal override void create()
	{
		base.create();
	}

	// Token: 0x06002038 RID: 8248 RVA: 0x00114B20 File Offset: 0x00112D20
	protected override void checkAutoDisable()
	{
		bool tEnabled = WorldBehaviourActionLava.hasLava();
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

	// Token: 0x06002039 RID: 8249 RVA: 0x00114B72 File Offset: 0x00112D72
	public override void update(float pElapsed)
	{
		this.checkAutoDisable();
		this.updateLava();
	}

	// Token: 0x0600203A RID: 8250 RVA: 0x00114B80 File Offset: 0x00112D80
	public override void draw(float pElapsed)
	{
	}

	// Token: 0x0600203B RID: 8251 RVA: 0x00114B84 File Offset: 0x00112D84
	private void updateLava()
	{
		bool tUpdate = false;
		if (this._to_clear.Count > 0)
		{
			foreach (WorldTile tTile in this._to_clear)
			{
				this.pixels[tTile.data.tile_id] = Toolbox.clear;
			}
			this._to_clear.Clear();
			tUpdate = true;
		}
		if (WorldBehaviourActionLava.hasLava())
		{
			tUpdate = true;
			this.drawLavaPixel(TileLibrary.lava0);
			this.drawLavaPixel(TileLibrary.lava1);
			this.drawLavaPixel(TileLibrary.lava2);
			this.drawLavaPixel(TileLibrary.lava3);
		}
		if (tUpdate)
		{
			base.updatePixels();
		}
	}

	// Token: 0x0600203C RID: 8252 RVA: 0x00114C48 File Offset: 0x00112E48
	internal override void clear()
	{
		base.clear();
		this._to_clear.Clear();
	}

	// Token: 0x0600203D RID: 8253 RVA: 0x00114C5C File Offset: 0x00112E5C
	private void drawLavaPixel(TileType pType)
	{
		if (pType.hashset.Count == 0)
		{
			return;
		}
		foreach (WorldTile tTile in pType.hashset)
		{
			this.pixels[tTile.data.tile_id] = pType.color;
			this._to_clear.Add(tTile);
		}
	}

	// Token: 0x0400176E RID: 5998
	private List<WorldTile> _to_clear = new List<WorldTile>();
}
