using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AA RID: 170
public abstract class GenericBannerLibrary : AssetLibrary<BannerAsset>
{
	// Token: 0x06000584 RID: 1412 RVA: 0x000537B5 File Offset: 0x000519B5
	public int getNewIndexBackground()
	{
		return Randy.randomInt(0, this.getCurrentAsset().backgrounds.Count);
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x000537CD File Offset: 0x000519CD
	public int getNewIndexIcon()
	{
		return Randy.randomInt(0, this.getCurrentAsset().icons.Count);
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x000537E5 File Offset: 0x000519E5
	public int getNewIndexFrame()
	{
		return Randy.randomInt(0, this.getCurrentAsset().frames.Count);
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x00053800 File Offset: 0x00051A00
	public Sprite getSpriteBackground(int pIndex)
	{
		BannerAsset tAsset = this.getCurrentAsset();
		return this.loadSpriteFromAsset(tAsset.backgrounds, pIndex);
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x00053824 File Offset: 0x00051A24
	public Sprite getSpriteBackground(int pIndex, string pAssetID)
	{
		BannerAsset tAsset = this.get(pAssetID);
		return this.loadSpriteFromAsset(tAsset.backgrounds, pIndex);
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x00053848 File Offset: 0x00051A48
	public Sprite getSpriteIcon(int pIndex)
	{
		BannerAsset tAsset = this.getCurrentAsset();
		return this.loadSpriteFromAsset(tAsset.icons, pIndex);
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x0005386C File Offset: 0x00051A6C
	public Sprite getSpriteIcon(int pIndex, string pAssetID)
	{
		BannerAsset tAsset = this.get(pAssetID);
		return this.loadSpriteFromAsset(tAsset.icons, pIndex);
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x00053890 File Offset: 0x00051A90
	public Sprite getSpriteFrame(int pID)
	{
		BannerAsset tAsset = this.getCurrentAsset();
		if (pID >= tAsset.frames.Count)
		{
			pID = 0;
		}
		return SpriteTextureLoader.getSprite(tAsset.frames[pID]);
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x000538C6 File Offset: 0x00051AC6
	private Sprite loadSpriteFromAsset(List<string> pSpriteList, int pIndex)
	{
		if (pIndex >= pSpriteList.Count)
		{
			pIndex = 0;
		}
		return SpriteTextureLoader.getSprite(pSpriteList[pIndex]);
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x000538E0 File Offset: 0x00051AE0
	public BannerAsset getCurrentAsset()
	{
		return this.main;
	}

	// Token: 0x040005CF RID: 1487
	public BannerAsset main;
}
