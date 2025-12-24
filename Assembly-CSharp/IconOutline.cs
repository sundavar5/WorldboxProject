using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005E2 RID: 1506
public class IconOutline : MonoBehaviour
{
	// Token: 0x06003178 RID: 12664 RVA: 0x0017A52F File Offset: 0x0017872F
	private void Awake()
	{
		this.checkInit();
	}

	// Token: 0x06003179 RID: 12665 RVA: 0x0017A537 File Offset: 0x00178737
	private void checkInit()
	{
		if (this._image != null)
		{
			return;
		}
		this._image = base.GetComponent<Image>();
		base.gameObject.AddComponent<FadeInOutAnimation>();
	}

	// Token: 0x0600317A RID: 12666 RVA: 0x0017A560 File Offset: 0x00178760
	public void show(ContainerItemColor pContainer)
	{
		this.checkInit();
		base.gameObject.SetActive(true);
		Color tColor = pContainer.color;
		tColor.a = 1f;
		this._image.color = tColor;
		string tID = this.parent_image.sprite.texture.GetHashCode().ToString() + "_" + tColor.GetHashCode().ToString();
		Sprite tSprite;
		if (IconOutline._cached_textures.ContainsKey(tID))
		{
			tSprite = IconOutline._cached_textures[tID];
		}
		else
		{
			tSprite = this.generateSprite();
			IconOutline._cached_textures.Add(tID, tSprite);
		}
		this._image.sprite = tSprite;
	}

	// Token: 0x0600317B RID: 12667 RVA: 0x0017A618 File Offset: 0x00178818
	private Sprite generateSprite()
	{
		int width = this.parent_image.sprite.texture.width;
		int tHeight = this.parent_image.sprite.texture.height;
		Texture2D tTexture = new Texture2D(width, tHeight);
		Color tColor = new Color(1f, 1f, 1f, 0f);
		for (int xx = 0; xx < tTexture.width; xx++)
		{
			for (int yy = 0; yy < tTexture.height; yy++)
			{
				tTexture.SetPixel(xx, yy, tColor);
			}
		}
		this.makePixels(-1, -1, tTexture);
		this.makePixels(1, 1, tTexture);
		this.makePixels(1, -1, tTexture);
		this.makePixels(-1, 1, tTexture);
		this.makePixels(1, 0, tTexture);
		this.makePixels(-1, 0, tTexture);
		this.makePixels(0, 1, tTexture);
		this.makePixels(0, -1, tTexture);
		tTexture.Apply();
		tTexture.filterMode = FilterMode.Point;
		tTexture.name = "IconOutline";
		Rect tRect = new Rect(0f, 0f, (float)tTexture.width, (float)tTexture.height);
		Vector2 tPivot = new Vector2(0.5f, 0.5f);
		return Sprite.Create(tTexture, tRect, tPivot, 1f);
	}

	// Token: 0x0600317C RID: 12668 RVA: 0x0017A748 File Offset: 0x00178948
	private void makePixels(int pOffsetX, int pOffsetY, Texture2D pTexture)
	{
		for (int xx = 0; xx < pTexture.width; xx++)
		{
			for (int yy = 0; yy < pTexture.height; yy++)
			{
				if (this.parent_image.sprite.texture.GetPixel(xx, yy).a != 0f)
				{
					int tNewX = xx + pOffsetX;
					int tNewY = yy + pOffsetY;
					if (tNewX >= 0 && tNewX <= pTexture.width && tNewY >= 0 && tNewY <= pTexture.height)
					{
						Color tColorNew = pTexture.GetPixel(tNewX, tNewY);
						tColorNew.a += 0.3f;
						pTexture.SetPixel(tNewX, tNewY, tColorNew);
					}
				}
			}
		}
	}

	// Token: 0x04002550 RID: 9552
	private static Dictionary<string, Sprite> _cached_textures = new Dictionary<string, Sprite>();

	// Token: 0x04002551 RID: 9553
	private Image _image;

	// Token: 0x04002552 RID: 9554
	public Image parent_image;
}
