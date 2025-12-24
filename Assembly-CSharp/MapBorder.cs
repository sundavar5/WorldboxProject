using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000345 RID: 837
public class MapBorder : BaseEffect
{
	// Token: 0x06002043 RID: 8259 RVA: 0x00114DB9 File Offset: 0x00112FB9
	internal override void create()
	{
		base.create();
		this.updateTimer = new WorldTimer(0.12f, new Action(this.updateEffect));
		this.alphaTimer = new WorldTimer(0.02f, new Action(this.updateAlpha));
	}

	// Token: 0x06002044 RID: 8260 RVA: 0x00114DFC File Offset: 0x00112FFC
	internal void generateTexture()
	{
		if (this.curWidth == MapBox.width && this.curHeight == MapBox.height)
		{
			return;
		}
		this.curWidth = MapBox.width;
		this.curHeight = MapBox.height;
		SpriteRenderer sprRnd = base.gameObject.GetComponent<SpriteRenderer>();
		Texture2D texture = new Texture2D(this.curWidth, this.curHeight, TextureFormat.RGBA32, false);
		texture.filterMode = FilterMode.Point;
		texture.name = "MapBorder_" + this.curWidth.ToString() + "x" + this.curHeight.ToString();
		int tSize = texture.height * texture.width;
		Color32[] pixels = new Color32[tSize];
		List<int> borderPixels = new List<int>();
		List<int> newPixels = new List<int>();
		newPixels.Clear();
		int tX = 0;
		int tY = 0;
		for (int i = 0; i < tSize; i++)
		{
			if (tY == 0 && !borderPixels.Contains(i))
			{
				newPixels.Add(i);
			}
			tX++;
			if (tX >= this.curWidth)
			{
				tX = 0;
				tY++;
			}
		}
		borderPixels.AddRange(newPixels);
		newPixels.Clear();
		tX = 0;
		tY = 0;
		for (int j = 0; j < tSize; j++)
		{
			if (tX == this.curWidth - 1 && !borderPixels.Contains(j))
			{
				newPixels.Add(j);
			}
			tX++;
			if (tX >= this.curWidth)
			{
				tX = 0;
				tY++;
			}
		}
		borderPixels.AddRange(newPixels);
		newPixels.Clear();
		tX = 0;
		tY = 0;
		for (int k = 0; k < tSize; k++)
		{
			if (tY == this.curHeight - 1 && !borderPixels.Contains(k))
			{
				newPixels.Add(k);
			}
			tX++;
			if (tX >= this.curWidth)
			{
				tX = 0;
				tY++;
			}
		}
		borderPixels.AddRange(newPixels);
		newPixels.Clear();
		tX = 0;
		tY = 0;
		for (int l = 0; l < tSize; l++)
		{
			if (tX == 0 && !borderPixels.Contains(l))
			{
				newPixels.Add(l);
			}
			tX++;
			if (tX >= this.curWidth)
			{
				tX = 0;
				tY++;
			}
		}
		newPixels.Reverse();
		borderPixels.AddRange(newPixels);
		int tStroke = 0;
		for (int m = 0; m < borderPixels.Count; m++)
		{
			int ii = borderPixels[m];
			if (tStroke == 0 || tStroke == 1 || tStroke == 2)
			{
				pixels[ii] = Color.white;
				tStroke++;
			}
			else
			{
				tStroke = 0;
			}
		}
		sprRnd.sprite = Sprite.Create(texture, new Rect(0f, 0f, (float)texture.width, (float)texture.height), new Vector2(0.5f, 0.5f), 1f);
		texture.SetPixels32(pixels);
		texture.Apply();
		base.gameObject.transform.localPosition = new Vector3((float)(this.curWidth / 2), (float)(this.curHeight / 2));
	}

	// Token: 0x06002045 RID: 8261 RVA: 0x001150E8 File Offset: 0x001132E8
	private void Update()
	{
		this.updateTimer.update(-1f);
		this.alphaTimer.update(-1f);
	}

	// Token: 0x06002046 RID: 8262 RVA: 0x0011510C File Offset: 0x0011330C
	private void updateAlpha()
	{
		if (World.world.selected_buttons.selectedButton == null)
		{
			this.alpha -= 0.02f;
			if (this.alpha < 0f)
			{
				this.alpha = 0f;
			}
		}
		else
		{
			this.alpha += 0.02f;
			if (this.alpha > 0.42f)
			{
				this.alpha = 0.42f;
			}
		}
		if (this.sprite_renderer.color.a == this.alpha)
		{
			return;
		}
		base.setAlpha(this.alpha);
	}

	// Token: 0x06002047 RID: 8263 RVA: 0x001151AC File Offset: 0x001133AC
	private void updateEffect()
	{
		if (this.alpha == 0f)
		{
			return;
		}
		this.currentState++;
		if (this.currentState > 3)
		{
			this.currentState = 0;
		}
		switch (this.currentState)
		{
		case 0:
			this.sprite_renderer.flipX = false;
			this.sprite_renderer.flipY = false;
			return;
		case 1:
			this.sprite_renderer.flipX = true;
			this.sprite_renderer.flipY = false;
			return;
		case 2:
			this.sprite_renderer.flipX = true;
			this.sprite_renderer.flipY = true;
			return;
		case 3:
			this.sprite_renderer.flipX = false;
			this.sprite_renderer.flipY = true;
			return;
		default:
			return;
		}
	}

	// Token: 0x04001772 RID: 6002
	private int currentState;

	// Token: 0x04001773 RID: 6003
	private WorldTimer updateTimer;

	// Token: 0x04001774 RID: 6004
	private WorldTimer alphaTimer;

	// Token: 0x04001775 RID: 6005
	private int curWidth;

	// Token: 0x04001776 RID: 6006
	private int curHeight;
}
