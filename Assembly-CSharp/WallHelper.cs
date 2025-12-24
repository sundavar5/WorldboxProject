using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001A8 RID: 424
public static class WallHelper
{
	// Token: 0x06000C61 RID: 3169 RVA: 0x000B4514 File Offset: 0x000B2714
	public static Sprite getSprite(WorldTile pTile, TopTileType pTileAsset)
	{
		WallFrameContainer tContainer;
		if (!WallHelper._dictionary.TryGetValue(pTileAsset.index_id, out tContainer))
		{
			tContainer = new WallFrameContainer();
			Sprite[] tSprites = SpriteTextureLoader.getSpriteList("walls/" + pTileAsset.id + "/wall_sheet", false);
			tContainer.sprites = tSprites;
			WallHelper._dictionary.Add(pTileAsset.index_id, tContainer);
		}
		int tFrameIndex;
		if (pTile.Type.animated_wall)
		{
			tFrameIndex = (int)(AnimationHelper.getAnimationGlobalTime(4f) + (float)pTile.random_animation_seed) % tContainer.sprites.Length;
		}
		else
		{
			tFrameIndex = pTile.random_animation_seed % tContainer.sprites.Length;
		}
		return tContainer.sprites[tFrameIndex];
	}

	// Token: 0x04000B8C RID: 2956
	private static Dictionary<int, WallFrameContainer> _dictionary = new Dictionary<int, WallFrameContainer>();
}
