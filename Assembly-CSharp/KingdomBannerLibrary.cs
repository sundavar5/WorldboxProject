using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AC RID: 172
public class KingdomBannerLibrary : GenericBannerLibrary
{
	// Token: 0x06000597 RID: 1431 RVA: 0x0005391C File Offset: 0x00051B1C
	public override void init()
	{
		base.init();
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x00053924 File Offset: 0x00051B24
	public override BannerAsset get(string pID)
	{
		if (this.dict.ContainsKey(pID))
		{
			return base.get(pID);
		}
		this.loadNewAssetRuntime(pID);
		return base.get(pID);
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x0005394B File Offset: 0x00051B4B
	public static string getFullPathBackground(string pID)
	{
		return "banners_kingdoms/" + pID + "/background";
	}

	// Token: 0x0600059A RID: 1434 RVA: 0x0005395D File Offset: 0x00051B5D
	public static string getFullPathIcon(string pID)
	{
		return "banners_kingdoms/" + pID + "/icon";
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x00053970 File Offset: 0x00051B70
	private BannerAsset loadNewAssetRuntime(string pID)
	{
		string tPathBackgrounds = KingdomBannerLibrary.getFullPathBackground(pID);
		string tPathIcons = KingdomBannerLibrary.getFullPathIcon(pID);
		Sprite[] spriteList = SpriteTextureLoader.getSpriteList(tPathBackgrounds, false);
		Sprite[] tSpriteIcons = SpriteTextureLoader.getSpriteList(tPathIcons, false);
		List<string> tBackgrounds = new List<string>();
		List<string> tIcons = new List<string>();
		foreach (Sprite tSprite in spriteList)
		{
			string tPath = tPathBackgrounds + "/" + tSprite.name;
			tBackgrounds.Add(tPath);
		}
		foreach (Sprite tSprite2 in tSpriteIcons)
		{
			string tPath2 = tPathIcons + "/" + tSprite2.name;
			tIcons.Add(tPath2);
		}
		BannerAsset tAsset = new BannerAsset
		{
			id = pID,
			backgrounds = tBackgrounds,
			icons = tIcons
		};
		this.add(tAsset);
		return tAsset;
	}

	// Token: 0x040005D0 RID: 1488
	public const string PATH_BANNER_KINGDOMS = "banners_kingdoms/";

	// Token: 0x040005D1 RID: 1489
	public const string PATH_BACKGROUND = "/background";

	// Token: 0x040005D2 RID: 1490
	public const string PATH_ICON = "/icon";
}
