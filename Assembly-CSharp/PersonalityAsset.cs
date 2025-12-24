using System;
using UnityEngine;

// Token: 0x0200006C RID: 108
[Serializable]
public class PersonalityAsset : Asset, ILocalizedAsset
{
	// Token: 0x060003C7 RID: 967 RVA: 0x00022580 File Offset: 0x00020780
	public string getTranslatedName()
	{
		return this.getLocaleID().Localize();
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x0002258D File Offset: 0x0002078D
	public string getLocaleID()
	{
		return "personality_" + this.id;
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x0002259F File Offset: 0x0002079F
	public Sprite getSprite()
	{
		if (this.sprite == null)
		{
			this.sprite = SpriteTextureLoader.getSprite("ui/Icons/" + this.icon);
		}
		return this.sprite;
	}

	// Token: 0x04000330 RID: 816
	public BaseStats base_stats = new BaseStats();

	// Token: 0x04000331 RID: 817
	public string icon;

	// Token: 0x04000332 RID: 818
	public Sprite sprite;
}
