using System;
using UnityEngine;

// Token: 0x020001B6 RID: 438
[Serializable]
public class HistoryGroupAsset : Asset, ILocalizedAsset
{
	// Token: 0x06000CBD RID: 3261 RVA: 0x000B90D4 File Offset: 0x000B72D4
	public string getLocaleID()
	{
		return "history_group_" + this.id;
	}

	// Token: 0x06000CBE RID: 3262 RVA: 0x000B90E6 File Offset: 0x000B72E6
	public Sprite getSprite()
	{
		if (this._icon_cache == null)
		{
			this._icon_cache = SpriteTextureLoader.getSprite(this.icon_path);
		}
		return this._icon_cache;
	}

	// Token: 0x04000C59 RID: 3161
	public string icon_path;

	// Token: 0x04000C5A RID: 3162
	private Sprite _icon_cache;
}
