using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000490 RID: 1168
public static class Texture2DStorage
{
	// Token: 0x060027EF RID: 10223 RVA: 0x00141C08 File Offset: 0x0013FE08
	internal static Sprite getSprite(int pW, int pH)
	{
		string tKey = pW.ToString() + "_" + pH.ToString();
		if (Texture2DStorage.pools.ContainsKey(tKey))
		{
			SpritePool tPool = Texture2DStorage.pools[tKey];
			if (tPool.list.Count > 0)
			{
				Sprite result = tPool.list[tPool.list.Count - 1];
				tPool.list.RemoveAt(tPool.list.Count - 1);
				return result;
			}
		}
		if (!Texture2DStorage.prefabs.ContainsKey(tKey))
		{
			Texture2D tTexture = new Texture2D(pW, pH, TextureFormat.RGBA32, false)
			{
				filterMode = FilterMode.Point
			};
			tTexture.name = "Texture2DStorage_" + tKey;
			Texture2DStorage.prefabs.Add(tKey, tTexture);
		}
		return Sprite.Create(Object.Instantiate<Texture2D>(Texture2DStorage.prefabs[tKey]), new Rect(0f, 0f, (float)pW, (float)pH), new Vector2(0f, 0f), 1f);
	}

	// Token: 0x060027F0 RID: 10224 RVA: 0x00141D00 File Offset: 0x0013FF00
	internal static void addToStorage(Sprite pSprite, int pW, int pH)
	{
		string tKey = pW.ToString() + "_" + pH.ToString();
		SpritePool tPool;
		if (Texture2DStorage.pools.ContainsKey(tKey))
		{
			tPool = Texture2DStorage.pools[tKey];
		}
		else
		{
			tPool = new SpritePool();
			Texture2DStorage.pools.Add(tKey, tPool);
		}
		tPool.list.Add(pSprite);
	}

	// Token: 0x04001E16 RID: 7702
	private static Dictionary<string, SpritePool> pools = new Dictionary<string, SpritePool>();

	// Token: 0x04001E17 RID: 7703
	private static Dictionary<string, Texture2D> prefabs = new Dictionary<string, Texture2D>();
}
