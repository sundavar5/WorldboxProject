using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000F3 RID: 243
public static class PixelBagManager
{
	// Token: 0x0600072B RID: 1835 RVA: 0x0006A4C8 File Offset: 0x000686C8
	public static PixelBag getPixelBag(Sprite pSpriteSource, bool pCheckPhenotypes = false, bool pCheckLights = false)
	{
		PixelBag tPixelBag;
		PixelBagManager._dict.TryGetValue(pSpriteSource, out tPixelBag);
		if (tPixelBag == null)
		{
			PixelBagManager.createPixelBag(pSpriteSource, pCheckPhenotypes, pCheckLights);
		}
		tPixelBag = PixelBagManager._dict[pSpriteSource];
		return tPixelBag;
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x0006A4FC File Offset: 0x000686FC
	private static void createPixelBag(Sprite pSpriteSource, bool pCheckPhenotypes, bool pCheckLights)
	{
		PixelBag tPixelBag = new PixelBag(pSpriteSource, pCheckPhenotypes, pCheckLights);
		PixelBagManager._dict.Add(pSpriteSource, tPixelBag);
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x0006A51E File Offset: 0x0006871E
	public static void preloadPixelBagUnit(Sprite pSpriteSource)
	{
		PixelBagManager.getPixelBag(pSpriteSource, true, false);
	}

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x0600072E RID: 1838 RVA: 0x0006A529 File Offset: 0x00068729
	public static int total
	{
		get
		{
			return PixelBagManager._dict.Count;
		}
	}

	// Token: 0x040007D0 RID: 2000
	private static readonly Dictionary<Sprite, PixelBag> _dict = new Dictionary<Sprite, PixelBag>();
}
