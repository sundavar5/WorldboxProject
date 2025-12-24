using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Token: 0x020001EA RID: 490
public class TilemapExtended : MonoBehaviour
{
	// Token: 0x06000E1C RID: 3612 RVA: 0x000C0804 File Offset: 0x000BEA04
	public void create(TileTypeBase pTileBase)
	{
		this.z = pTileBase.render_z;
		base.gameObject.name = pTileBase.draw_layer_name;
		TilemapRenderer component = base.GetComponent<TilemapRenderer>();
		component.sortingOrder = pTileBase.render_z;
		component.sharedMaterial = LibraryMaterials.instance.dict[pTileBase.material];
		if (pTileBase.id == "deep_ocean")
		{
			base.gameObject.SetActive(false);
		}
		this._tilemap = base.GetComponent<Tilemap>();
	}

	// Token: 0x06000E1D RID: 3613 RVA: 0x000C0884 File Offset: 0x000BEA84
	internal void prepareDraw()
	{
		this._vec.Clear();
		this._tiles.Clear();
	}

	// Token: 0x06000E1E RID: 3614 RVA: 0x000C089C File Offset: 0x000BEA9C
	internal void addToQueueToRedraw(WorldTile pWorldTile, Vector3Int pPosition, TileBase pTileGraphics, bool pSkipCheck = false)
	{
		pPosition.z = 0;
		if (!pSkipCheck)
		{
			if (pWorldTile.current_rendered_tile_graphics == pTileGraphics && pTileGraphics != null)
			{
				return;
			}
			pWorldTile.current_rendered_tile_graphics = pTileGraphics;
		}
		this._vec.Add(pPosition);
		this._tiles.Add(pTileGraphics);
	}

	// Token: 0x06000E1F RID: 3615 RVA: 0x000C08DB File Offset: 0x000BEADB
	internal void clear()
	{
		this._tilemap.ClearAllTiles();
	}

	// Token: 0x06000E20 RID: 3616 RVA: 0x000C08E8 File Offset: 0x000BEAE8
	internal void redraw()
	{
		if (this._vec.Count == 0)
		{
			return;
		}
		this._tilemap.SetTiles(this._vec.ToArray(), this._tiles.GetRawBuffer());
	}

	// Token: 0x04000EAF RID: 3759
	public int z;

	// Token: 0x04000EB0 RID: 3760
	private Tilemap _tilemap;

	// Token: 0x04000EB1 RID: 3761
	private readonly List<Vector3Int> _vec = new List<Vector3Int>();

	// Token: 0x04000EB2 RID: 3762
	private readonly ListPool<TileBase> _tiles = new ListPool<TileBase>();
}
