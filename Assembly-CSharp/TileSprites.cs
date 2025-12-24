using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Token: 0x020001E9 RID: 489
public class TileSprites
{
	// Token: 0x06000E17 RID: 3607 RVA: 0x000C0798 File Offset: 0x000BE998
	public void addVariation(Sprite pSprite, string pID)
	{
		Tile tTile = ScriptableObject.CreateInstance<Tile>();
		tTile.name = pID;
		tTile.sprite = pSprite;
		this._tiles.Add(tTile);
	}

	// Token: 0x06000E18 RID: 3608 RVA: 0x000C07C5 File Offset: 0x000BE9C5
	public Tile getRandom()
	{
		return this._tiles.GetRandom<Tile>();
	}

	// Token: 0x06000E19 RID: 3609 RVA: 0x000C07D2 File Offset: 0x000BE9D2
	public Tile getVariation(int pID)
	{
		return this._tiles[pID];
	}

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x06000E1A RID: 3610 RVA: 0x000C07E0 File Offset: 0x000BE9E0
	public Tile main
	{
		get
		{
			return this._tiles[0];
		}
	}

	// Token: 0x04000EAE RID: 3758
	private List<Tile> _tiles = new List<Tile>();
}
