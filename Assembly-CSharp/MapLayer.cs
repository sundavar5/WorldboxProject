using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200040B RID: 1035
public class MapLayer : BaseMapObject
{
	// Token: 0x060023C8 RID: 9160 RVA: 0x0012A5B0 File Offset: 0x001287B0
	internal override void create()
	{
		base.create();
		this.pixels_to_update = new HashSetWorldTile();
		this.sprRnd = base.gameObject.GetComponent<SpriteRenderer>();
		if (this.rewriteSortingLayer)
		{
			this.sprRnd.sortingLayerName = World.world.GetComponent<SpriteRenderer>().sortingLayerName;
		}
		this.colors = new List<Color32>();
		this.createColors();
	}

	// Token: 0x060023C9 RID: 9161 RVA: 0x0012A614 File Offset: 0x00128814
	protected virtual void checkAutoDisable()
	{
		if (!this.autoDisable)
		{
			return;
		}
		if (this.autoDisableCheckPixels)
		{
			if (this.pixels_to_update.Count > 0)
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
				return;
			}
		}
		else if (this.hashsetTiles.Count > 0)
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

	// Token: 0x060023CA RID: 9162 RVA: 0x0012A6B8 File Offset: 0x001288B8
	internal void createTextureNew()
	{
		if (!(this.texture == null) && MapBox.width == this.textureWidth && MapBox.height == this.texture.height)
		{
			return;
		}
		if (this.sprRnd.sprite != null && this.textureWidth != 0)
		{
			Texture2DStorage.addToStorage(this.sprRnd.sprite, this.textureWidth, this.textureHeight);
		}
		this.textureWidth = MapBox.width;
		this.textureHeight = MapBox.height;
		this.sprRnd.sprite = Texture2DStorage.getSprite(this.textureWidth, this.textureHeight);
		this.texture = this.sprRnd.sprite.texture;
		this.textureID = this.texture.GetHashCode();
		int tSize = this.texture.height * this.texture.width;
		Color32 tClear = Color.clear;
		this.pixels = new Color32[tSize];
		for (int i = 0; i < tSize; i++)
		{
			this.pixels[i] = tClear;
		}
		this.updatePixels();
	}

	// Token: 0x060023CB RID: 9163 RVA: 0x0012A7D1 File Offset: 0x001289D1
	public bool contains(WorldTile pTile)
	{
		return this.pixels_to_update.Contains(pTile);
	}

	// Token: 0x060023CC RID: 9164 RVA: 0x0012A7E0 File Offset: 0x001289E0
	internal virtual void clear()
	{
		if (this.pixels == null)
		{
			return;
		}
		this.pixels_to_update.Clear();
		Color32 tClear = Color.clear;
		for (int i = 0; i < this.pixels.Length; i++)
		{
			this.pixels[i] = tClear;
		}
		this.updatePixels();
	}

	// Token: 0x060023CD RID: 9165 RVA: 0x0012A832 File Offset: 0x00128A32
	public void setRendererEnabled(bool pBool)
	{
		this.sprRnd.enabled = pBool;
	}

	// Token: 0x060023CE RID: 9166 RVA: 0x0012A840 File Offset: 0x00128A40
	protected void createColors()
	{
		for (int i = 0; i < this.colors_amount; i++)
		{
			float tVal;
			if (i > 0)
			{
				tVal = 1f / (float)this.colors_amount * (float)i;
			}
			else
			{
				tVal = 0f;
			}
			this.colors.Add(new Color(this.colorValues.r, this.colorValues.g, this.colorValues.b, tVal * this.colorValues.a));
		}
	}

	// Token: 0x060023CF RID: 9167 RVA: 0x0012A8BE File Offset: 0x00128ABE
	public override void update(float pElapsed)
	{
		this.checkAutoDisable();
	}

	// Token: 0x060023D0 RID: 9168 RVA: 0x0012A8C6 File Offset: 0x00128AC6
	public virtual void draw(float pElapsed)
	{
		if (this.sprRnd.enabled)
		{
			this.UpdateDirty(pElapsed);
		}
	}

	// Token: 0x060023D1 RID: 9169 RVA: 0x0012A8DC File Offset: 0x00128ADC
	internal void updatePixels()
	{
		this.texture.SetPixels32(this.pixels);
		this.texture.Apply();
	}

	// Token: 0x060023D2 RID: 9170 RVA: 0x0012A8FA File Offset: 0x00128AFA
	protected virtual void UpdateDirty(float pElapsed)
	{
	}

	// Token: 0x040019CB RID: 6603
	public bool autoDisable;

	// Token: 0x040019CC RID: 6604
	public bool autoDisableCheckPixels;

	// Token: 0x040019CD RID: 6605
	public int textureID;

	// Token: 0x040019CE RID: 6606
	protected float timer;

	// Token: 0x040019CF RID: 6607
	protected Color colorValues;

	// Token: 0x040019D0 RID: 6608
	protected int colors_amount = 1;

	// Token: 0x040019D1 RID: 6609
	internal SpriteRenderer sprRnd;

	// Token: 0x040019D2 RID: 6610
	internal Texture2D texture;

	// Token: 0x040019D3 RID: 6611
	internal Color32[] pixels;

	// Token: 0x040019D4 RID: 6612
	internal HashSetWorldTile pixels_to_update;

	// Token: 0x040019D5 RID: 6613
	protected List<Color32> colors;

	// Token: 0x040019D6 RID: 6614
	internal HashSetWorldTile hashsetTiles;

	// Token: 0x040019D7 RID: 6615
	private int textureWidth;

	// Token: 0x040019D8 RID: 6616
	private int textureHeight;

	// Token: 0x040019D9 RID: 6617
	public bool rewriteSortingLayer = true;
}
