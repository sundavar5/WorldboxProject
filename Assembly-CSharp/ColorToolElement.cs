using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000527 RID: 1319
public class ColorToolElement : MonoBehaviour
{
	// Token: 0x06002B33 RID: 11059 RVA: 0x001565AD File Offset: 0x001547AD
	public void createKingdom(ColorAsset pColor)
	{
		this.color_asset = pColor;
	}

	// Token: 0x06002B34 RID: 11060 RVA: 0x001565B6 File Offset: 0x001547B6
	public void createCulture(ColorAsset pColor)
	{
		this.color_asset = pColor;
		this.setColorsForObjects(pColor);
		this.saveColors(pColor);
	}

	// Token: 0x06002B35 RID: 11061 RVA: 0x001565D0 File Offset: 0x001547D0
	public void createClans(ColorAsset pColor)
	{
		this.color_asset = pColor;
		string tPathBackground = AssetManager.clan_banners_library.main.backgrounds.GetRandom<string>();
		string tPathIcon = AssetManager.clan_banners_library.main.icons.GetRandom<string>();
		this.background.sprite = SpriteTextureLoader.getSprite(tPathBackground);
		this.icon.sprite = SpriteTextureLoader.getSprite(tPathIcon);
		this.setColorsForObjects(pColor);
		this.saveColors(pColor);
	}

	// Token: 0x06002B36 RID: 11062 RVA: 0x00156640 File Offset: 0x00154840
	private void setColorsForObjects(ColorAsset pColorAsset)
	{
		this.borderInside.color = pColorAsset.getColorBorderInsideAlpha32();
		this.borderOutside.color = pColorAsset.getColorMainSecond();
		this.background.color = pColorAsset.getColorMainSecond();
		this.icon.color = pColorAsset.getColorBanner();
		this.text.color = pColorAsset.getColorText();
		this.favorite = pColorAsset.favorite;
		this.id = pColorAsset.id;
		this.text.text = pColorAsset.id + " |  " + pColorAsset.index_id.ToString();
		this.debug_index = pColorAsset.index_id;
		if (this.test_house != null && this.house_default_sprite != null)
		{
			this.test_house.sprite = DynamicSpriteCreator.createNewSpriteForDebug(this.house_default_sprite, pColorAsset);
		}
		if (this.test_face != null && this.face_default_sprite != null)
		{
			this.test_face.sprite = DynamicSpriteCreator.createNewSpriteForDebug(this.face_default_sprite, pColorAsset);
		}
		if (this.sprite_favorite != null)
		{
			this.sprite_favorite.gameObject.SetActive(this.favorite);
		}
	}

	// Token: 0x06002B37 RID: 11063 RVA: 0x0015677C File Offset: 0x0015497C
	private void OnValidate()
	{
		if (this.color_asset == null)
		{
			return;
		}
		this.color_asset.color_main = Toolbox.colorToHex(this.colorMain, false);
		this.color_asset.color_main_2 = Toolbox.colorToHex(this.colorMain2, false);
		this.color_asset.color_banner = Toolbox.colorToHex(this.colorBanner, false);
		this.color_asset.color_text = Toolbox.colorToHex(this.colorText, false);
		this.color_asset.id = this.id;
		this.color_asset.favorite = this.favorite;
		this.color_asset.setEditorColors(this.colorMain, this.colorMain2, this.colorBanner, this.colorText);
		this.setColorsForObjects(this.color_asset);
	}

	// Token: 0x06002B38 RID: 11064 RVA: 0x00156853 File Offset: 0x00154A53
	private void saveColors(ColorAsset pColor)
	{
		this.colorMain = pColor.getColorMain();
		this.colorMain2 = pColor.getColorMainSecond();
		this.colorBanner = pColor.getColorBanner();
		this.colorText = pColor.getColorText();
	}

	// Token: 0x04002053 RID: 8275
	[Header("Edit Colors")]
	public Color colorMain;

	// Token: 0x04002054 RID: 8276
	public Color colorMain2;

	// Token: 0x04002055 RID: 8277
	public Color colorBanner;

	// Token: 0x04002056 RID: 8278
	public Color colorText;

	// Token: 0x04002057 RID: 8279
	[Header("Edit Asset Name / Id")]
	public string id;

	// Token: 0x04002058 RID: 8280
	public bool favorite;

	// Token: 0x04002059 RID: 8281
	[Header("Other Stuff")]
	[Space(30f)]
	public Image background;

	// Token: 0x0400205A RID: 8282
	public Image icon;

	// Token: 0x0400205B RID: 8283
	public Text text;

	// Token: 0x0400205C RID: 8284
	public Image sprite_favorite;

	// Token: 0x0400205D RID: 8285
	public Image borderInside;

	// Token: 0x0400205E RID: 8286
	public Image borderOutside;

	// Token: 0x0400205F RID: 8287
	[HideInInspector]
	public ColorAsset color_asset;

	// Token: 0x04002060 RID: 8288
	public Image test_house;

	// Token: 0x04002061 RID: 8289
	public Image test_face;

	// Token: 0x04002062 RID: 8290
	public Sprite house_default_sprite;

	// Token: 0x04002063 RID: 8291
	public Sprite face_default_sprite;

	// Token: 0x04002064 RID: 8292
	public int debug_index;
}
