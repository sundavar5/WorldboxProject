using System;
using System.Collections.Generic;
using EpPathFinding.cs;
using UnityEngine;

// Token: 0x02000439 RID: 1081
public class PathFindingVisualiser : MapLayer
{
	// Token: 0x060025A0 RID: 9632 RVA: 0x0013660D File Offset: 0x0013480D
	internal override void create()
	{
		this.colorValues = new Color(1f, 0.46f, 0.19f, 1f);
		this.colorValues = this.default_color;
		base.create();
	}

	// Token: 0x060025A1 RID: 9633 RVA: 0x00136640 File Offset: 0x00134840
	protected override void UpdateDirty(float pElapsed)
	{
		if (DebugConfig.isOn(DebugOption.LastPath))
		{
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
				return;
			}
		}
		else if (base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060025A2 RID: 9634 RVA: 0x00136680 File Offset: 0x00134880
	internal override void clear()
	{
		if (this.tiles.Count == 0)
		{
			return;
		}
		this.tiles.Clear();
		for (int i = 0; i < this.pixels.Length; i++)
		{
			this.pixels[i] = Color.clear;
		}
		base.createTextureNew();
	}

	// Token: 0x060025A3 RID: 9635 RVA: 0x001366D8 File Offset: 0x001348D8
	internal void showPath(StaticGrid pGrid, List<WorldTile> pTilePath)
	{
		if (!DebugConfig.isOn(DebugOption.LastPath))
		{
			return;
		}
		this.clear();
		if (pGrid != null)
		{
			foreach (WorldTile tTile in World.world.tiles_list)
			{
				this.tiles.Add(tTile);
				Node tNode = pGrid.GetNodeAt(tTile.pos.x, tTile.pos.y);
				if (tNode.isClosed)
				{
					this.pixels[tTile.data.tile_id] = Color.red;
				}
				else if (tNode.isOpened)
				{
					this.pixels[tTile.data.tile_id] = Color.green;
				}
				else
				{
					this.pixels[tTile.data.tile_id] = Color.clear;
				}
			}
		}
		foreach (WorldTile tTile2 in pTilePath)
		{
			this.pixels[tTile2.data.tile_id] = Color.blue;
			this.tiles.Add(tTile2);
		}
		base.updatePixels();
	}

	// Token: 0x04001C90 RID: 7312
	public Color default_color;

	// Token: 0x04001C91 RID: 7313
	private List<WorldTile> tiles = new List<WorldTile>();
}
