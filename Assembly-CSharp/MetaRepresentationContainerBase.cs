using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityPools;

// Token: 0x020002AA RID: 682
public class MetaRepresentationContainerBase : StatsRowsContainer
{
	// Token: 0x06001982 RID: 6530 RVA: 0x000F08EC File Offset: 0x000EEAEC
	protected override void init()
	{
		base.init();
		this.asset = AssetManager.meta_representation_library.getAsset(this._meta_type);
		this._prefab_bar.gameObject.SetActive(false);
		this._title.setKeyAndUpdate(this.asset.getLocaleID());
	}

	// Token: 0x06001983 RID: 6531 RVA: 0x000F093C File Offset: 0x000EEB3C
	protected override void showStats()
	{
		int tTotal = 0;
		bool tAny = false;
		Dictionary<IMetaObject, int> tDict = UnsafeCollectionPool<Dictionary<IMetaObject, int>, KeyValuePair<IMetaObject, int>>.Get();
		this.fillDict(ref tTotal, ref tAny, tDict);
		int tNone = tTotal;
		foreach (KeyValuePair<IMetaObject, int> tPair in from p in tDict
		orderby p.Value descending
		select p)
		{
			IMetaObject tMeta = tPair.Key;
			int tAmount = tPair.Value;
			tNone -= tAmount;
			string tPopString = this.amountWithPercent(tAmount, tTotal);
			string tIconPath = this.asset.icon_getter(tMeta);
			string tSecondaryIconPath = this.asset.show_species_icon ? tMeta.getActorAsset().icon : null;
			string tNameString = tMeta.name;
			tNameString += Toolbox.coloredGreyPart(tAmount, tMeta.getColor().color_text, false);
			KeyValueField tField = this.showStatRowTwoIcons(tNameString, tPopString, tMeta.getColor().color_text, this.asset.meta_type, tMeta.getID(), true, tIconPath, tSecondaryIconPath, null, null, false);
			this.showBar(tField, tAmount, tTotal, tMeta.getColor().color_text);
		}
		this.checkShowNone(tAny, tNone, tTotal);
		UnsafeCollectionPool<Dictionary<IMetaObject, int>, KeyValuePair<IMetaObject, int>>.Release(tDict);
		this._layout_element.ignoreLayout = !tAny;
		this._background.enabled = tAny;
		this._title.gameObject.SetActive(tAny);
	}

	// Token: 0x06001984 RID: 6532 RVA: 0x000F0AC8 File Offset: 0x000EECC8
	protected virtual void fillDict(ref int pTotal, ref bool pAny, Dictionary<IMetaObject, int> pDict)
	{
		throw new NotImplementedException();
	}

	// Token: 0x06001985 RID: 6533 RVA: 0x000F0ACF File Offset: 0x000EECCF
	protected virtual void checkShowNone(bool pAny, int pNone, int pTotal)
	{
		throw new NotImplementedException();
	}

	// Token: 0x06001986 RID: 6534 RVA: 0x000F0AD8 File Offset: 0x000EECD8
	protected void showBar(KeyValueField pField, int pAmount, int pTotal, string pColorHex)
	{
		float tFill = (pTotal > 0) ? ((float)pAmount / (float)pTotal) : 0f;
		Transform transform = pField.transform.Find("gen_percent_bar");
		Image tBarImage = (transform != null) ? transform.GetComponent<Image>() : null;
		if (tBarImage == null)
		{
			tBarImage = Object.Instantiate<GameObject>(this._prefab_bar.gameObject, pField.transform).GetComponent<Image>();
			tBarImage.gameObject.SetActive(true);
			tBarImage.name = "gen_percent_bar";
		}
		float tWidth = 100f * tFill * 0.5f;
		Vector2 tSizeDelta = new Vector2(tWidth, 8.5f);
		tBarImage.GetComponent<RectTransform>().sizeDelta = tSizeDelta;
		tBarImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-2f, 0f);
		tBarImage.transform.SetAsFirstSibling();
		Color tColor = Toolbox.makeColor(pColorHex);
		tColor.a = 0.4f;
		tBarImage.color = tColor;
	}

	// Token: 0x06001987 RID: 6535 RVA: 0x000F0BB8 File Offset: 0x000EEDB8
	protected string amountWithPercent(int pAmount, int pTotal)
	{
		float tPercent = (pTotal > 0) ? ((float)pAmount / (float)pTotal * 100f) : 0f;
		if (pTotal == pAmount)
		{
			tPercent = 100f;
		}
		return tPercent.ToText() + "%";
	}

	// Token: 0x06001988 RID: 6536 RVA: 0x000F0BF8 File Offset: 0x000EEDF8
	internal KeyValueField showStatRowTwoIcons(string pId, object pValue, string pColor, MetaType pMetaType = MetaType.None, long pMetaId = -1L, bool pColorText = false, string pIconPath = null, string pIconSecondaryPath = null, string pTooltipId = null, TooltipDataGetter pTooltipData = null, bool pLocalize = true)
	{
		KeyValueField tNewRow = base.showStatRow(pId, pValue, pColor, pMetaType, pMetaId, pColorText, pIconPath, pTooltipId, pTooltipData, pLocalize);
		bool tShowIcon = !string.IsNullOrEmpty(pIconSecondaryPath);
		if (tShowIcon)
		{
			Sprite tIcon = SpriteTextureLoader.getSprite("ui/Icons/" + pIconSecondaryPath);
			tNewRow.icon_secondary.sprite = tIcon;
		}
		tNewRow.icon_secondary.gameObject.SetActive(tShowIcon);
		return tNewRow;
	}

	// Token: 0x06001989 RID: 6537 RVA: 0x000F0C5B File Offset: 0x000EEE5B
	public void setMetaType(MetaType pType)
	{
		this._meta_type = pType;
	}

	// Token: 0x04001401 RID: 5121
	[SerializeField]
	protected MetaType _meta_type;

	// Token: 0x04001402 RID: 5122
	[SerializeField]
	private LocalizedText _title;

	// Token: 0x04001403 RID: 5123
	[SerializeField]
	private Image _background;

	// Token: 0x04001404 RID: 5124
	[SerializeField]
	private Image _prefab_bar;

	// Token: 0x04001405 RID: 5125
	[SerializeField]
	private LayoutElement _layout_element;

	// Token: 0x04001406 RID: 5126
	protected MetaRepresentationAsset asset;
}
