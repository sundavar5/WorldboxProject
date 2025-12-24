using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200002B RID: 43
[Serializable]
public class BrushData : Asset, ILocalizedAsset
{
	// Token: 0x06000212 RID: 530 RVA: 0x00012C68 File Offset: 0x00010E68
	public void setupImage(Image pSprite)
	{
		pSprite.sprite = this.getSprite();
		Vector2 tUiScale = this.ui_scale;
		Vector2 tUiSize = this.ui_size;
		if (this.height < 28)
		{
			tUiSize = new Vector2((float)this.width, (float)this.height);
		}
		pSprite.rectTransform.sizeDelta = new Vector2(tUiSize.x, tUiSize.y);
		pSprite.transform.localScale = new Vector3(tUiScale.x, tUiScale.y, 1f);
	}

	// Token: 0x06000213 RID: 531 RVA: 0x00012CEC File Offset: 0x00010EEC
	public Sprite getSprite()
	{
		if (this._sprite != null)
		{
			return this._sprite;
		}
		Texture2D tTexture = new Texture2D(this.width, this.height, TextureFormat.RGBA32, false)
		{
			filterMode = FilterMode.Point,
			wrapMode = TextureWrapMode.Clamp
		};
		Color[] tTransparentPixels = new Color[this.width * this.height];
		for (int i = 0; i < tTransparentPixels.Length; i++)
		{
			tTransparentPixels[i] = Color.clear;
		}
		tTexture.SetPixels(tTransparentPixels);
		Color tColor = Color.white;
		int tMinX = 0;
		int tMinY = 0;
		foreach (BrushPixelData tPixel in this.pos)
		{
			if (tPixel.x < tMinX)
			{
				tMinX = tPixel.x;
			}
			if (tPixel.y < tMinY)
			{
				tMinY = tPixel.y;
			}
		}
		foreach (BrushPixelData tPixel2 in this.pos)
		{
			tTexture.SetPixel(tPixel2.x - tMinX, tPixel2.y - tMinY, tColor);
		}
		tTexture.Apply(false, true);
		Rect tRect = new Rect(0f, 0f, (float)tTexture.width, (float)tTexture.height);
		Vector2 tPivot = new Vector2(0f, 0f);
		this._sprite = Sprite.Create(tTexture, tRect, tPivot, 1f);
		this._sprite.name = this.id;
		return this._sprite;
	}

	// Token: 0x06000214 RID: 532 RVA: 0x00012E64 File Offset: 0x00011064
	public string getLocaleID()
	{
		return this.localized_key;
	}

	// Token: 0x040001AE RID: 430
	[DefaultValue(1)]
	public int size = 1;

	// Token: 0x040001AF RID: 431
	[DefaultValue(1)]
	public int drops = 1;

	// Token: 0x040001B0 RID: 432
	public BrushGroup group;

	// Token: 0x040001B1 RID: 433
	public bool show_in_brush_window;

	// Token: 0x040001B2 RID: 434
	public int width;

	// Token: 0x040001B3 RID: 435
	public int height;

	// Token: 0x040001B4 RID: 436
	public int sqr_size;

	// Token: 0x040001B5 RID: 437
	public bool auto_size;

	// Token: 0x040001B6 RID: 438
	public bool continuous;

	// Token: 0x040001B7 RID: 439
	public bool fast_spawn;

	// Token: 0x040001B8 RID: 440
	public string localized_key;

	// Token: 0x040001B9 RID: 441
	public BrushPixelData[] pos;

	// Token: 0x040001BA RID: 442
	public BrushGenerateAction generate_action;

	// Token: 0x040001BB RID: 443
	public Vector2 ui_scale = new Vector2(1f, 1f);

	// Token: 0x040001BC RID: 444
	public Vector2 ui_size = new Vector2(28f, 28f);

	// Token: 0x040001BD RID: 445
	[NonSerialized]
	private Sprite _sprite;
}
