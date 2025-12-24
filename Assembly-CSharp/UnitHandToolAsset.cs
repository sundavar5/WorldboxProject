using System;
using UnityEngine;

// Token: 0x02000181 RID: 385
[Serializable]
public class UnitHandToolAsset : Asset, IHandRenderer
{
	// Token: 0x06000B86 RID: 2950 RVA: 0x000A5AF7 File Offset: 0x000A3CF7
	public Sprite[] getSprites()
	{
		return this.gameplay_sprites;
	}

	// Token: 0x1700004A RID: 74
	// (get) Token: 0x06000B87 RID: 2951 RVA: 0x000A5AFF File Offset: 0x000A3CFF
	public bool is_colored
	{
		get
		{
			return this.colored;
		}
	}

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x06000B88 RID: 2952 RVA: 0x000A5B07 File Offset: 0x000A3D07
	public bool is_animated
	{
		get
		{
			return this.animated;
		}
	}

	// Token: 0x04000B1E RID: 2846
	public bool animated;

	// Token: 0x04000B1F RID: 2847
	public string path_gameplay_sprite;

	// Token: 0x04000B20 RID: 2848
	public bool colored;

	// Token: 0x04000B21 RID: 2849
	[NonSerialized]
	public Sprite[] gameplay_sprites;
}
