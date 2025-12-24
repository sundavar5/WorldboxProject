using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000360 RID: 864
public class AnimationContainerUnit
{
	// Token: 0x060020D2 RID: 8402 RVA: 0x00118CFC File Offset: 0x00116EFC
	public AnimationContainerUnit(string pTexturePath)
	{
		this.id = pTexturePath;
		foreach (Sprite tSprite in SpriteTextureLoader.getSpriteList(pTexturePath, false))
		{
			this.sprites.Add(tSprite.name, tSprite);
		}
	}

	// Token: 0x04001827 RID: 6183
	public bool child;

	// Token: 0x04001828 RID: 6184
	internal readonly string id;

	// Token: 0x04001829 RID: 6185
	internal readonly Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

	// Token: 0x0400182A RID: 6186
	internal readonly Dictionary<string, AnimationFrameData> dict_frame_data = new Dictionary<string, AnimationFrameData>();

	// Token: 0x0400182B RID: 6187
	internal ActorAnimation idle;

	// Token: 0x0400182C RID: 6188
	internal ActorAnimation walking;

	// Token: 0x0400182D RID: 6189
	internal ActorAnimation swimming;

	// Token: 0x0400182E RID: 6190
	public bool has_swimming;

	// Token: 0x0400182F RID: 6191
	public bool has_idle;

	// Token: 0x04001830 RID: 6192
	public bool has_walking;

	// Token: 0x04001831 RID: 6193
	public bool render_heads_for_children;

	// Token: 0x04001832 RID: 6194
	internal Sprite[] heads;

	// Token: 0x04001833 RID: 6195
	internal Sprite[] heads_male;

	// Token: 0x04001834 RID: 6196
	internal Sprite[] heads_female;
}
