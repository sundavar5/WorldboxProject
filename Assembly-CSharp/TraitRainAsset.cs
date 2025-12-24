using System;
using UnityEngine;

// Token: 0x02000186 RID: 390
[Serializable]
public class TraitRainAsset : Asset
{
	// Token: 0x06000B9A RID: 2970 RVA: 0x000A5D04 File Offset: 0x000A3F04
	public Sprite getSpriteArt()
	{
		if (this._sprite_art == null)
		{
			this._sprite_art = SpriteTextureLoader.getSprite(this.path_art);
		}
		return this._sprite_art;
	}

	// Token: 0x06000B9B RID: 2971 RVA: 0x000A5D2B File Offset: 0x000A3F2B
	public Sprite getSpriteArtVoid()
	{
		if (this._sprite_art_void == null)
		{
			this._sprite_art_void = SpriteTextureLoader.getSprite(this.path_art_void);
		}
		return this._sprite_art_void;
	}

	// Token: 0x04000B22 RID: 2850
	public RainListGetter get_list;

	// Token: 0x04000B23 RID: 2851
	public RainStateGetter get_state;

	// Token: 0x04000B24 RID: 2852
	public RainStateSetter set_state;

	// Token: 0x04000B25 RID: 2853
	public string path_art;

	// Token: 0x04000B26 RID: 2854
	private Sprite _sprite_art;

	// Token: 0x04000B27 RID: 2855
	public string path_art_void;

	// Token: 0x04000B28 RID: 2856
	private Sprite _sprite_art_void;
}
