using System;
using Beebyte.Obfuscator;
using UnityEngine;

// Token: 0x02000063 RID: 99
[ObfuscateLiterals]
[Serializable]
public class MoodAsset : Asset
{
	// Token: 0x0600037E RID: 894 RVA: 0x0001FF9C File Offset: 0x0001E19C
	public Sprite getSprite()
	{
		if (this.sprite == null)
		{
			this.sprite = SpriteTextureLoader.getSprite("ui/Icons/" + this.icon);
		}
		return this.sprite;
	}

	// Token: 0x04000315 RID: 789
	public BaseStats base_stats = new BaseStats();

	// Token: 0x04000316 RID: 790
	public string icon;

	// Token: 0x04000317 RID: 791
	[NonSerialized]
	public Sprite sprite;
}
