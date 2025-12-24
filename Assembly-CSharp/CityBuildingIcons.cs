using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000652 RID: 1618
public class CityBuildingIcons : CityElement
{
	// Token: 0x0600347E RID: 13438 RVA: 0x00185E98 File Offset: 0x00184098
	protected override void Awake()
	{
		this._list_house_images = new Image[]
		{
			this.top,
			this.topRight,
			this.right,
			this.bottomRight,
			this.bottom,
			this.bottomLeft,
			this.left,
			this.topLeft
		};
		base.Awake();
	}

	// Token: 0x0600347F RID: 13439 RVA: 0x00185F00 File Offset: 0x00184100
	private void Update()
	{
		foreach (SpriteAnimation spriteAnimation in this._sprite_animations)
		{
			spriteAnimation.update(Time.deltaTime);
		}
	}

	// Token: 0x06003480 RID: 13440 RVA: 0x00185F58 File Offset: 0x00184158
	protected override void clear()
	{
		Image[] list_house_images = this._list_house_images;
		for (int i = 0; i < list_house_images.Length; i++)
		{
			list_house_images[i].gameObject.SetActive(false);
		}
		this._sprite_animations.Clear();
	}

	// Token: 0x06003481 RID: 13441 RVA: 0x00185F93 File Offset: 0x00184193
	protected override IEnumerator showContent()
	{
		CityBuildingIcons.<showContent>d__14 <showContent>d__ = new CityBuildingIcons.<showContent>d__14(0);
		<showContent>d__.<>4__this = this;
		return <showContent>d__;
	}

	// Token: 0x06003482 RID: 13442 RVA: 0x00185FA4 File Offset: 0x001841A4
	private void showSprite(Image pImage, string pAssetID)
	{
		BuildingAsset tAsset = AssetManager.buildings.get(pAssetID);
		Sprite random = tAsset.building_sprites.animation_data.GetRandom<BuildingAnimationData>().main.GetRandom<Sprite>();
		Kingdom kingdom = base.city.kingdom;
		Sprite tColoredSprite = DynamicSprites.getRecoloredBuilding(random, (kingdom != null) ? kingdom.getColor() : null, tAsset.atlas_asset);
		pImage.sprite = tColoredSprite;
		pImage.SetNativeSize();
		pImage.gameObject.SetActive(true);
	}

	// Token: 0x06003483 RID: 13443 RVA: 0x00186014 File Offset: 0x00184214
	private ListPool<string> getCityBuildingTypes()
	{
		ListPool<string> pList = new ListPool<string>(base.city.buildings.Count);
		foreach (Building tBuilding in base.city.buildings)
		{
			if (tBuilding.asset.hasHousingSlots() && tBuilding.asset.id.Contains("house"))
			{
				pList.Add(tBuilding.asset.id);
			}
		}
		return pList;
	}

	// Token: 0x0400278E RID: 10126
	public Image top;

	// Token: 0x0400278F RID: 10127
	public Image topLeft;

	// Token: 0x04002790 RID: 10128
	public Image topRight;

	// Token: 0x04002791 RID: 10129
	public Image left;

	// Token: 0x04002792 RID: 10130
	public Image right;

	// Token: 0x04002793 RID: 10131
	public Image bottom;

	// Token: 0x04002794 RID: 10132
	public Image bottomLeft;

	// Token: 0x04002795 RID: 10133
	public Image bottomRight;

	// Token: 0x04002796 RID: 10134
	public Image centerBonfire;

	// Token: 0x04002797 RID: 10135
	private Image[] _list_house_images;

	// Token: 0x04002798 RID: 10136
	private List<SpriteAnimation> _sprite_animations = new List<SpriteAnimation>();
}
