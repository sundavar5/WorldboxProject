using System;
using UnityEngine;

// Token: 0x0200005C RID: 92
[Serializable]
public class MapSizeAsset : Asset, ILocalizedAsset
{
	// Token: 0x06000360 RID: 864 RVA: 0x0001F2E3 File Offset: 0x0001D4E3
	public string getLocaleID()
	{
		return "map_size_" + this.id;
	}

	// Token: 0x06000361 RID: 865 RVA: 0x0001F2F5 File Offset: 0x0001D4F5
	public Sprite getIconSprite()
	{
		if (this.cached_sprite == null)
		{
			this.cached_sprite = SpriteTextureLoader.getSprite("ui/Icons/" + this.path_icon);
		}
		return this.cached_sprite;
	}

	// Token: 0x040002EF RID: 751
	public Sprite cached_sprite;

	// Token: 0x040002F0 RID: 752
	public string path_icon;

	// Token: 0x040002F1 RID: 753
	public bool show_warning;

	// Token: 0x040002F2 RID: 754
	public int size = 1;
}
