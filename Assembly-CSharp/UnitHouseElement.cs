using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006E2 RID: 1762
public class UnitHouseElement : UnitElement
{
	// Token: 0x0600387E RID: 14462 RVA: 0x001955CB File Offset: 0x001937CB
	protected override IEnumerator showContent()
	{
		if (!this.actor.hasHomeBuilding())
		{
			yield break;
		}
		Building tHomeBuilding = this.actor.getHomeBuilding();
		this.track_objects.Add(tHomeBuilding);
		this._title.SetActive(true);
		this._house_container.gameObject.SetActive(true);
		this.showSprite(this.actor.kingdom, this._house_image, tHomeBuilding);
		this.setIconValue("i_house_health", (float)tHomeBuilding.getHealth(), new float?((float)tHomeBuilding.getMaxHealth()), "", false, "", '/');
		this.setIconValue("i_house_people", (float)tHomeBuilding.countResidents(), new float?((float)tHomeBuilding.asset.housing_slots), "", false, "", '/');
		yield break;
	}

	// Token: 0x0600387F RID: 14463 RVA: 0x001955DC File Offset: 0x001937DC
	private void setIconValue(string pName, float pMainVal, float? pMax = null, string pColor = "", bool pFloat = false, string pEnding = "", char pSeparator = '/')
	{
		Transform tTransform = base.transform.FindRecursive(pName);
		if (tTransform == null)
		{
			return;
		}
		StatsIcon component = tTransform.GetComponent<StatsIcon>();
		component.gameObject.SetActive(true);
		component.setValue(pMainVal, pMax, pColor, pFloat, pEnding, pSeparator, false);
	}

	// Token: 0x06003880 RID: 14464 RVA: 0x00195624 File Offset: 0x00193824
	private void showSprite(Kingdom pKingdom, Image pImage, Building pBuilding)
	{
		BuildingAsset tAsset = pBuilding.asset;
		Sprite tColoredSprite = DynamicSprites.getRecoloredBuilding(tAsset.building_sprites.animation_data[pBuilding.animData_index].main.GetRandom<Sprite>(), pKingdom.getColor(), tAsset.atlas_asset);
		pImage.sprite = tColoredSprite;
		pImage.SetNativeSize();
		float a = 28f / pImage.rectTransform.sizeDelta.x;
		float tRatioY = 28f / pImage.rectTransform.sizeDelta.y;
		float tRatio = Mathf.Min(a, tRatioY);
		pImage.rectTransform.sizeDelta = new Vector2(pImage.rectTransform.sizeDelta.x * tRatio, pImage.rectTransform.sizeDelta.y * tRatio);
	}

	// Token: 0x06003881 RID: 14465 RVA: 0x001956DF File Offset: 0x001938DF
	protected override void clear()
	{
		this._title.SetActive(false);
		this._house_container.SetActive(false);
		base.clear();
	}

	// Token: 0x06003882 RID: 14466 RVA: 0x001956FF File Offset: 0x001938FF
	public override bool checkRefreshWindow()
	{
		return (this._house_container.activeSelf && !this.actor.hasHomeBuilding()) || base.checkRefreshWindow();
	}

	// Token: 0x040029F3 RID: 10739
	[SerializeField]
	private GameObject _title;

	// Token: 0x040029F4 RID: 10740
	[SerializeField]
	private GameObject _house_container;

	// Token: 0x040029F5 RID: 10741
	[SerializeField]
	private Image _house_image;
}
