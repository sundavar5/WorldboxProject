using System;
using System.Collections.Generic;

// Token: 0x020001EF RID: 495
public class WorldLayerEdges : MapLayer
{
	// Token: 0x06000E53 RID: 3667 RVA: 0x000C1797 File Offset: 0x000BF997
	public override void update(float pElapsed)
	{
	}

	// Token: 0x06000E54 RID: 3668 RVA: 0x000C1799 File Offset: 0x000BF999
	public override void draw(float pElapsed)
	{
		this.UpdateDirty(pElapsed);
	}

	// Token: 0x06000E55 RID: 3669 RVA: 0x000C17A2 File Offset: 0x000BF9A2
	public void addDirtyChunk(MapChunk pChunk)
	{
		this._dirty_chunks.Add(pChunk);
	}

	// Token: 0x06000E56 RID: 3670 RVA: 0x000C17B1 File Offset: 0x000BF9B1
	internal override void clear()
	{
		this._dirty_chunks.Clear();
		base.clear();
	}

	// Token: 0x06000E57 RID: 3671 RVA: 0x000C17C4 File Offset: 0x000BF9C4
	public void redraw()
	{
		this._chunks_to_redraw.UnionWith(this._dirty_chunks);
		foreach (MapChunk tChunk in this._dirty_chunks)
		{
			this._chunks_to_redraw.UnionWith(tChunk.neighbours_all);
		}
		this._dirty_chunks.Clear();
		foreach (MapChunk mapChunk in this._chunks_to_redraw)
		{
			WorldTile[] tTiles = mapChunk.tiles;
			int tCount = tTiles.Length;
			for (int i = 0; i < tCount; i++)
			{
				WorldTile tTile = tTiles[i];
				this.pixels[tTile.data.tile_id] = Toolbox.clear;
				this.redrawTile(tTile);
			}
		}
		this._chunks_to_redraw.Clear();
		base.updatePixels();
	}

	// Token: 0x06000E58 RID: 3672 RVA: 0x000C18CC File Offset: 0x000BFACC
	private void redrawTile(WorldTile pTile)
	{
		WorldTile tTileDown = pTile.tile_down;
		WorldTile tTileUp = pTile.tile_up;
		if (!pTile.Type.check_edge)
		{
			return;
		}
		if (pTile.Type.wall)
		{
			return;
		}
		if (tTileDown != null && !tTileDown.Type.wall)
		{
			if (pTile.Type.edge_hills && !tTileDown.Type.rocks)
			{
				this.pixels[pTile.data.tile_id] = TileTypeBase.edge_color_hills;
				return;
			}
			if (pTile.Type.edge_mountains && tTileDown.Type.edge_hills)
			{
				this.pixels[pTile.data.tile_id] = TileTypeBase.edge_color_mountain;
				return;
			}
			if (pTile.Type.rocks && !tTileDown.Type.rocks)
			{
				this.pixels[pTile.data.tile_id] = TileTypeBase.edge_color_mountain;
				return;
			}
			if (!tTileDown.Type.ocean && !pTile.Type.ocean && tTileDown.Type != pTile.Type && tTileDown.Type.height_min < pTile.Type.height_min)
			{
				this.pixels[pTile.data.tile_id] = Toolbox.edge_alpha;
				return;
			}
		}
		if (tTileUp != null)
		{
			if (tTileUp.Type.wall)
			{
				this.pixels[pTile.data.tile_id] = tTileUp.Type.edge_color;
				return;
			}
			if (pTile.Type.ocean && tTileUp.Type.ocean && tTileUp.Type.height_min > pTile.Type.height_min)
			{
				this.pixels[pTile.data.tile_id] = Toolbox.edge_alpha;
				return;
			}
			if (tTileUp.Type.ground && !tTileUp.Type.can_be_filled_with_ocean)
			{
				if (pTile.Type.layer_type == TileLayerType.Ocean)
				{
					this.pixels[pTile.data.tile_id] = TileTypeBase.edge_color_ocean;
					return;
				}
				if (pTile.Type.can_be_filled_with_ocean && !pTile.Type.explodable)
				{
					this.pixels[pTile.data.tile_id] = TileTypeBase.edge_color_no_ocean;
					return;
				}
			}
		}
	}

	// Token: 0x04000EBE RID: 3774
	private HashSet<MapChunk> _dirty_chunks = new HashSet<MapChunk>();

	// Token: 0x04000EBF RID: 3775
	private HashSet<MapChunk> _chunks_to_redraw = new HashSet<MapChunk>();
}
