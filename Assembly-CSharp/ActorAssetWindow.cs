using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000547 RID: 1351
public class ActorAssetWindow : BaseDebugAssetWindow<ActorAsset, ActorDebugAssetElement>
{
	// Token: 0x06002C0F RID: 11279 RVA: 0x0015B41A File Offset: 0x0015961A
	public void clickRandomKingdomColor()
	{
		AssetsDebugManager.setRandomKingdomColor(this.asset.kingdom_id_wild);
		this.asset_debug_element.setData(this.asset);
	}

	// Token: 0x06002C10 RID: 11280 RVA: 0x0015B43D File Offset: 0x0015963D
	public void clickRandomSkinColor()
	{
		AssetsDebugManager.setRandomSkinColor(this.asset);
		this.asset_debug_element.setData(this.asset);
	}

	// Token: 0x06002C11 RID: 11281 RVA: 0x0015B45B File Offset: 0x0015965B
	public void clickChangeSex()
	{
		AssetsDebugManager.changeSex();
		this.asset_debug_element.setData(this.asset);
	}

	// Token: 0x06002C12 RID: 11282 RVA: 0x0015B474 File Offset: 0x00159674
	protected override void initSprites()
	{
		base.initSprites();
		string tPath = this.asset.texture_asset.texture_path_base;
		if (new List<string>
		{
			"dragon",
			"zombie_dragon",
			"worm"
		}.Contains(this.asset.id))
		{
			tPath = "actors_special/t_" + this.asset.id;
		}
		if (this.asset.is_boat)
		{
			tPath = "actors/boats/" + this.asset.id;
		}
		string id = this.asset.id;
		if (!(id == "UFO"))
		{
			if (!(id == "crabzilla"))
			{
				if (id == "god_finger")
				{
					tPath = "actors/species/other/god_finger";
				}
			}
			else
			{
				tPath = "actors/special/crab";
			}
		}
		else
		{
			tPath = "actors/special/t_ufo";
		}
		foreach (Sprite tSprite in SpriteTextureLoader.getSpriteList(tPath, false))
		{
			SpriteElement spriteElement = Object.Instantiate<SpriteElement>(this.sprite_element_prefab, this.sprite_elements_parent);
			spriteElement.image.sprite = tSprite;
			spriteElement.text_name.text = tSprite.name;
		}
	}
}
