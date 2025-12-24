using System;
using UnityEngine;

// Token: 0x02000180 RID: 384
public static class HappinessHelper
{
	// Token: 0x06000B85 RID: 2949 RVA: 0x000A5A84 File Offset: 0x000A3C84
	public static Sprite getSpriteBasedOnHappinessValue(int pValue)
	{
		Sprite tSprite;
		if (pValue < -80)
		{
			tSprite = SpriteTextureLoader.getSprite("ui/Icons/iconHappiness_1");
		}
		else if (pValue < -20)
		{
			tSprite = SpriteTextureLoader.getSprite("ui/Icons/iconHappiness_2");
		}
		else if (pValue < 20)
		{
			tSprite = SpriteTextureLoader.getSprite("ui/Icons/iconHappiness_3");
		}
		else if (pValue < 40)
		{
			tSprite = SpriteTextureLoader.getSprite("ui/Icons/iconHappiness_4");
		}
		else if (pValue < 80)
		{
			tSprite = SpriteTextureLoader.getSprite("ui/Icons/iconHappiness_5");
		}
		else
		{
			tSprite = SpriteTextureLoader.getSprite("ui/Icons/iconHappiness_6");
		}
		return tSprite;
	}
}
