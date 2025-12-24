using System;
using UnityEngine;

// Token: 0x020000D6 RID: 214
[Serializable]
public class CommunicationAsset : Asset
{
	// Token: 0x06000658 RID: 1624 RVA: 0x0005F7EA File Offset: 0x0005D9EA
	public Sprite getSpriteBubble()
	{
		if (this._sprite_cache == null)
		{
			this._sprite_cache = SpriteTextureLoader.getSprite(this.icon_path);
		}
		return this._sprite_cache;
	}

	// Token: 0x04000725 RID: 1829
	public string icon_path;

	// Token: 0x04000726 RID: 1830
	public bool show_topic;

	// Token: 0x04000727 RID: 1831
	public float rate;

	// Token: 0x04000728 RID: 1832
	public TopicCheck check;

	// Token: 0x04000729 RID: 1833
	public TopicPotFill pot_fill;

	// Token: 0x0400072A RID: 1834
	[NonSerialized]
	private Sprite _sprite_cache;
}
