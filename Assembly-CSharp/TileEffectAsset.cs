using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

// Token: 0x020000FA RID: 250
[Serializable]
public class TileEffectAsset : Asset
{
	// Token: 0x06000762 RID: 1890 RVA: 0x0006C606 File Offset: 0x0006A806
	public void addTileType(string pType)
	{
		if (this.tile_types == null)
		{
			this.tile_types = new HashSet<string>();
		}
		this.tile_types.Add(pType);
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x0006C628 File Offset: 0x0006A828
	public void addTileTypes(params string[] pTypes)
	{
		if (this.tile_types == null)
		{
			this.tile_types = new HashSet<string>(pTypes);
			return;
		}
		this.tile_types.UnionWith(pTypes);
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x0006C64B File Offset: 0x0006A84B
	public Sprite[] getSprites()
	{
		if (this._cached_sprites == null)
		{
			this._cached_sprites = SpriteTextureLoader.getSpriteList(this.path_sprite, false);
		}
		return this._cached_sprites;
	}

	// Token: 0x040007F9 RID: 2041
	[DefaultValue(1)]
	public int rate = 1;

	// Token: 0x040007FA RID: 2042
	[DefaultValue(1f)]
	public float chance = 1f;

	// Token: 0x040007FB RID: 2043
	public string path_sprite;

	// Token: 0x040007FC RID: 2044
	[DefaultValue(0.1f)]
	public float time_between_frames = 0.1f;

	// Token: 0x040007FD RID: 2045
	private Sprite[] _cached_sprites;

	// Token: 0x040007FE RID: 2046
	public HashSet<string> tile_types;
}
