using System;
using UnityEngine;

// Token: 0x02000168 RID: 360
public static class ItemRendering
{
	// Token: 0x06000B00 RID: 2816 RVA: 0x000A0D1C File Offset: 0x0009EF1C
	public static Sprite getItemMainSpriteFrame(IHandRenderer pHandRendererAsset)
	{
		if (pHandRendererAsset == null)
		{
			return null;
		}
		Sprite[] tSpriteList = pHandRendererAsset.getSprites();
		if (tSpriteList.Length > 1)
		{
			return AnimationHelper.getSpriteFromList(0, tSpriteList, 5f);
		}
		return tSpriteList[0];
	}
}
