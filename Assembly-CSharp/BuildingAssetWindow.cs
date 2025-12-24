using System;
using UnityEngine;

// Token: 0x02000552 RID: 1362
public class BuildingAssetWindow : BaseDebugAssetWindow<BuildingAsset, BuildingDebugAssetElement>
{
	// Token: 0x06002C6E RID: 11374 RVA: 0x0015CFDF File Offset: 0x0015B1DF
	public void clickRandomKingdomColor()
	{
		AssetsDebugManager.setRandomKingdomColor(this.asset.civ_kingdom);
		this.asset_debug_element.setData(this.asset);
	}

	// Token: 0x06002C6F RID: 11375 RVA: 0x0015D004 File Offset: 0x0015B204
	protected override void initSprites()
	{
		base.initSprites();
		string tPath = this.asset.sprite_path;
		if (string.IsNullOrEmpty(tPath))
		{
			tPath = this.asset.main_path + this.asset.id;
		}
		foreach (Sprite tSprite in SpriteTextureLoader.getSpriteList(tPath, false))
		{
			SpriteElement spriteElement = Object.Instantiate<SpriteElement>(this.sprite_element_prefab, this.sprite_elements_parent);
			spriteElement.image.sprite = tSprite;
			spriteElement.text_name.text = tSprite.name;
		}
	}

	// Token: 0x06002C70 RID: 11376 RVA: 0x0015D08E File Offset: 0x0015B28E
	public static void reloadSprites()
	{
		BaseDebugAssetWindow<BuildingAsset, BuildingDebugAssetElement>.current_element.setData(BaseDebugAssetWindow<BuildingAsset, BuildingDebugAssetElement>.current_element.asset);
	}
}
