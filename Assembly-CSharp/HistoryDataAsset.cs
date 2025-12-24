using System;
using ChartAndGraph;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200056F RID: 1391
[Serializable]
public class HistoryDataAsset : Asset, IDescriptionAsset, ILocalizedAsset
{
	// Token: 0x06002D5E RID: 11614 RVA: 0x0016173C File Offset: 0x0015F93C
	public Material getChartPointMaterial()
	{
		if (this._material_point == null)
		{
			this._material_point = HistoryDataAsset.cloneMaterial("materials/graph/graph_base_point");
			this._material_point.SetTexture("_MainTex", Resources.Load<Texture2D>(this.path_icon));
		}
		return this._material_point;
	}

	// Token: 0x06002D5F RID: 11615 RVA: 0x00161788 File Offset: 0x0015F988
	public ChartItemEffect getHoverPointMaterial()
	{
		if (this._hover_prefab == null)
		{
			this._hover_prefab = HistoryDataAsset.clonePrefab("Prefabs/graph/PointHover", GameObject.Find("Charts").transform);
			this._hover_prefab.gameObject.name = "Hover " + this.id;
			this._hover_prefab.GetComponent<Image>().sprite = SpriteTextureLoader.getSprite(this.path_icon);
			this._hover_prefab.gameObject.SetActive(false);
		}
		return this._hover_prefab;
	}

	// Token: 0x06002D60 RID: 11616 RVA: 0x00161814 File Offset: 0x0015FA14
	public Material getChartLineMaterial()
	{
		if (this._material_line == null)
		{
			this._material_line = HistoryDataAsset.getChartLineMaterial(this.getColorMain());
		}
		return this._material_line;
	}

	// Token: 0x06002D61 RID: 11617 RVA: 0x0016183B File Offset: 0x0015FA3B
	public static Material getChartLineMaterial(Color pColor)
	{
		Material material = HistoryDataAsset.cloneMaterial("materials/graph/graph_base_line");
		material.SetColor("_Color", pColor);
		return material;
	}

	// Token: 0x06002D62 RID: 11618 RVA: 0x00161853 File Offset: 0x0015FA53
	public Material getChartInnerFillMaterial()
	{
		if (this._material_gradient == null)
		{
			this._material_gradient = HistoryDataAsset.getChartInnerFillMaterial(this.getColorMain());
		}
		return this._material_gradient;
	}

	// Token: 0x06002D63 RID: 11619 RVA: 0x0016187C File Offset: 0x0015FA7C
	public static Material getChartInnerFillMaterial(Color pColor)
	{
		Material material = HistoryDataAsset.cloneMaterial("materials/graph/graph_base_gradient");
		Color tColor = pColor;
		tColor.a = 0.4f;
		Color tColor2 = pColor;
		tColor2.a = 0.1f;
		material.SetColor("_ColorFrom", tColor);
		material.SetColor("_ColorTo", tColor2);
		return material;
	}

	// Token: 0x06002D64 RID: 11620 RVA: 0x001618C7 File Offset: 0x0015FAC7
	public string getLocaleID()
	{
		return this.localized_key ?? this.id;
	}

	// Token: 0x06002D65 RID: 11621 RVA: 0x001618DC File Offset: 0x0015FADC
	public string getDescriptionID()
	{
		string tDescriptionID = this.getLocaleID() + "_description";
		if (!string.IsNullOrEmpty(this.localized_key_description))
		{
			tDescriptionID = this.localized_key_description;
		}
		if (LocalizedTextManager.stringExists(tDescriptionID))
		{
			return tDescriptionID;
		}
		return this.getLocaleID() + "_description";
	}

	// Token: 0x06002D66 RID: 11622 RVA: 0x00161928 File Offset: 0x0015FB28
	public Color getColorMain()
	{
		return Toolbox.makeColor(this.color_hex);
	}

	// Token: 0x06002D67 RID: 11623 RVA: 0x00161935 File Offset: 0x0015FB35
	private static Material cloneMaterial(string pPath)
	{
		return Object.Instantiate<Material>(Resources.Load<Material>(pPath));
	}

	// Token: 0x06002D68 RID: 11624 RVA: 0x00161942 File Offset: 0x0015FB42
	private static ChartItemEffect clonePrefab(string pPath, Transform pParentTransform)
	{
		return Object.Instantiate<ChartItemEffect>(Resources.Load<ChartItemEffect>(pPath), pParentTransform);
	}

	// Token: 0x040022A6 RID: 8870
	public string localized_key;

	// Token: 0x040022A7 RID: 8871
	public string localized_key_description;

	// Token: 0x040022A8 RID: 8872
	public string statistics_asset;

	// Token: 0x040022A9 RID: 8873
	public string color_hex;

	// Token: 0x040022AA RID: 8874
	public string tooltip_color_hex;

	// Token: 0x040022AB RID: 8875
	public string path_icon;

	// Token: 0x040022AC RID: 8876
	public bool enabled_default;

	// Token: 0x040022AD RID: 8877
	private Material _material_point;

	// Token: 0x040022AE RID: 8878
	private Material _material_line;

	// Token: 0x040022AF RID: 8879
	private Material _material_gradient;

	// Token: 0x040022B0 RID: 8880
	private ChartItemEffect _hover_prefab;

	// Token: 0x040022B1 RID: 8881
	public bool average;

	// Token: 0x040022B2 RID: 8882
	public bool max;

	// Token: 0x040022B3 RID: 8883
	public bool sum;

	// Token: 0x040022B4 RID: 8884
	public GraphCategoryGroup category_group = GraphCategoryGroup.General;
}
