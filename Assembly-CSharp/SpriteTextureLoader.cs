using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000488 RID: 1160
public static class SpriteTextureLoader
{
	// Token: 0x060027D7 RID: 10199 RVA: 0x001414D4 File Offset: 0x0013F6D4
	public static Sprite getSprite(string pPath)
	{
		Sprite tSprite;
		if (!SpriteTextureLoader._cached_sprites.TryGetValue(pPath, out tSprite))
		{
			tSprite = (Sprite)Resources.Load(pPath, typeof(Sprite));
			SpriteTextureLoader._cached_sprites[pPath] = tSprite;
		}
		return tSprite;
	}

	// Token: 0x060027D8 RID: 10200 RVA: 0x00141514 File Offset: 0x0013F714
	public static Sprite[] getSpriteList(string pPath, bool pSkipIfEmpty = false)
	{
		Sprite[] tSpriteList;
		if (!SpriteTextureLoader._cached_sprite_list.TryGetValue(pPath, out tSpriteList))
		{
			tSpriteList = Resources.LoadAll<Sprite>(pPath);
			if (pSkipIfEmpty && tSpriteList.Length == 0)
			{
				return null;
			}
			SpriteTextureLoader._cached_sprite_list.Add(pPath, tSpriteList);
			SpriteTextureLoader._total_sprite_list_single_sprites += tSpriteList.Length;
		}
		return tSpriteList;
	}

	// Token: 0x060027D9 RID: 10201 RVA: 0x0014155C File Offset: 0x0013F75C
	public static void addSprite(string pPathID, byte[] pBytes)
	{
		Texture2D tTexture = new Texture2D(1, 1);
		tTexture.filterMode = FilterMode.Point;
		if (tTexture.LoadImage(pBytes))
		{
			Rect tRect = new Rect(0f, 0f, (float)tTexture.width, (float)tTexture.height);
			Vector2 tPivot = new Vector2(0.5f, 0.5f);
			Sprite tSprite = Sprite.Create(tTexture, tRect, tPivot, 1f);
			SpriteTextureLoader._cached_sprites.Add(pPathID, tSprite);
		}
	}

	// Token: 0x17000220 RID: 544
	// (get) Token: 0x060027DA RID: 10202 RVA: 0x001415CB File Offset: 0x0013F7CB
	public static int total_sprites
	{
		get
		{
			return SpriteTextureLoader._cached_sprites.Count;
		}
	}

	// Token: 0x17000221 RID: 545
	// (get) Token: 0x060027DB RID: 10203 RVA: 0x001415D7 File Offset: 0x0013F7D7
	public static int total_sprites_list
	{
		get
		{
			return SpriteTextureLoader._cached_sprite_list.Count;
		}
	}

	// Token: 0x17000222 RID: 546
	// (get) Token: 0x060027DC RID: 10204 RVA: 0x001415E3 File Offset: 0x0013F7E3
	public static int total_sprites_list_single_sprites
	{
		get
		{
			return SpriteTextureLoader._total_sprite_list_single_sprites;
		}
	}

	// Token: 0x04001E03 RID: 7683
	private static readonly Dictionary<string, Sprite> _cached_sprites = new Dictionary<string, Sprite>();

	// Token: 0x04001E04 RID: 7684
	private static readonly Dictionary<string, Sprite[]> _cached_sprite_list = new Dictionary<string, Sprite[]>();

	// Token: 0x04001E05 RID: 7685
	private static int _total_sprite_list_single_sprites = 0;
}
