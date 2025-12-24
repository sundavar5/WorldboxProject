using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000F4 RID: 244
public class UnitSpriteConstructorAtlas
{
	// Token: 0x06000730 RID: 1840 RVA: 0x0006A541 File Offset: 0x00068741
	public UnitSpriteConstructorAtlas(UnitTextureAtlasID pID, bool pBigAtlas)
	{
		this.id = pID;
		this._big_atlas = pBigAtlas;
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x0006A562 File Offset: 0x00068762
	public void setBigAtlas(bool pBigAtlas)
	{
		this._big_atlas = pBigAtlas;
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x0006A56B File Offset: 0x0006876B
	public bool isBigSpriteSheetAtlas()
	{
		return this._big_atlas;
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x0006A574 File Offset: 0x00068774
	public void newTexture(int pWidth, int pHeight, string tName)
	{
		if (!this._big_atlas)
		{
			pWidth += 2;
			pHeight += 10;
		}
		this.texture = new Texture2D(pWidth, pHeight);
		this.textures.Add(this.texture);
		this.texture.filterMode = FilterMode.Point;
		this.texture.wrapMode = TextureWrapMode.Clamp;
		this.texture.name = tName;
		this.pixels = this.texture.GetPixels32();
		Color32 tColor = Color.clear;
		for (int i = 0; i < this.pixels.Length; i++)
		{
			this.pixels[i] = tColor;
		}
		this.dirty = true;
		this.last_x = 0;
		this.last_y = 0;
		this._biggest_height = 0;
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x0006A62E File Offset: 0x0006882E
	public void checkDirty()
	{
		if (!this.dirty)
		{
			return;
		}
		this.dirty = false;
		this.texture.SetPixels32(this.pixels);
		this.texture.Apply();
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x0006A65C File Offset: 0x0006885C
	public string debug()
	{
		return this.textures.Count.ToString() + " | " + this.last_y.ToString();
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x0006A694 File Offset: 0x00068894
	public void checkBounds(int pWidth, int pHeight)
	{
		if (!this._big_atlas)
		{
			this.newTexture(pWidth, pHeight, this.id.ToString() + "_small_atlas");
			this.last_x = 1;
			this.last_y = 1;
			return;
		}
		bool tNew = false;
		if (this.textures.Count == 0)
		{
			tNew = true;
		}
		if (pHeight > this._biggest_height)
		{
			this._biggest_height = pHeight;
		}
		int tMaxTextureSize = DynamicSpritesConfig.texture_size;
		if (this.last_x + pWidth + 1 > tMaxTextureSize)
		{
			this.last_x = 0;
			this.last_y += this._biggest_height + 1;
			if (this.last_y + this._biggest_height >= tMaxTextureSize || this.last_y >= tMaxTextureSize)
			{
				tNew = true;
			}
			else
			{
				this._biggest_height = pHeight;
			}
		}
		else if (this.last_y + pHeight >= tMaxTextureSize)
		{
			tNew = true;
		}
		if (tNew)
		{
			this.checkDirty();
			this.newTexture(tMaxTextureSize, tMaxTextureSize, this.id.ToString() + "_big_atlas");
			this._biggest_height = pHeight;
		}
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x0006A794 File Offset: 0x00068994
	public void clear()
	{
		foreach (Texture2D tTexture2D in this.textures)
		{
			if (tTexture2D != null)
			{
				Object.Destroy(tTexture2D);
			}
		}
		this.textures.Clear();
		this._biggest_height = 0;
		this.last_x = 0;
		this.last_y = 0;
	}

	// Token: 0x040007D1 RID: 2001
	public UnitTextureAtlasID id;

	// Token: 0x040007D2 RID: 2002
	private bool _big_atlas;

	// Token: 0x040007D3 RID: 2003
	public Texture2D texture;

	// Token: 0x040007D4 RID: 2004
	public Color32[] pixels;

	// Token: 0x040007D5 RID: 2005
	public List<Texture2D> textures = new List<Texture2D>();

	// Token: 0x040007D6 RID: 2006
	public int last_x;

	// Token: 0x040007D7 RID: 2007
	public int last_y;

	// Token: 0x040007D8 RID: 2008
	private int _biggest_height;

	// Token: 0x040007D9 RID: 2009
	public bool dirty;
}
