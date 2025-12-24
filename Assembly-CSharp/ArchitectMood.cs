using System;
using UnityEngine;

// Token: 0x020000A0 RID: 160
[Serializable]
public class ArchitectMood : Asset, ILocalizedAsset
{
	// Token: 0x06000552 RID: 1362 RVA: 0x00052A98 File Offset: 0x00050C98
	public Sprite getSprite()
	{
		if (this._cached_sprite == null)
		{
			this._cached_sprite = SpriteTextureLoader.getSprite(this.path_icon);
		}
		return this._cached_sprite;
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x00052ABF File Offset: 0x00050CBF
	public string getLocaleID()
	{
		return "architect_mood_" + this.id;
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x00052AD1 File Offset: 0x00050CD1
	public Color getColor()
	{
		if (this._cached_color == Color.clear)
		{
			this._cached_color = Toolbox.makeColor(this.color_main);
		}
		return this._cached_color;
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x00052AFC File Offset: 0x00050CFC
	public Color getColorText()
	{
		if (this._cached_color_text == Color.clear)
		{
			this._cached_color_text = Toolbox.makeColor(this.color_text);
		}
		return this._cached_color_text;
	}

	// Token: 0x040005A5 RID: 1445
	public string color_main;

	// Token: 0x040005A6 RID: 1446
	public string color_text;

	// Token: 0x040005A7 RID: 1447
	public string path_icon;

	// Token: 0x040005A8 RID: 1448
	private Color _cached_color = Color.clear;

	// Token: 0x040005A9 RID: 1449
	private Color _cached_color_text = Color.clear;

	// Token: 0x040005AA RID: 1450
	private Sprite _cached_sprite;
}
