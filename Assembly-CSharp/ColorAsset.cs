using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000037 RID: 55
[Serializable]
public class ColorAsset : Asset
{
	// Token: 0x06000245 RID: 581 RVA: 0x0001543E File Offset: 0x0001363E
	public static List<ColorAsset> getAllColorsList()
	{
		return ColorAsset._all_colors_list;
	}

	// Token: 0x06000246 RID: 582 RVA: 0x00015445 File Offset: 0x00013645
	public ColorAsset()
	{
	}

	// Token: 0x06000247 RID: 583 RVA: 0x0001544D File Offset: 0x0001364D
	public static bool isColorAssetExists(string pColorMain)
	{
		return ColorAsset._all_colors_dict.ContainsKey(pColorMain);
	}

	// Token: 0x06000248 RID: 584 RVA: 0x0001545C File Offset: 0x0001365C
	public static ColorAsset getExistingColorAsset(string pColorMain)
	{
		ColorAsset tAsset;
		ColorAsset._all_colors_dict.TryGetValue(pColorMain, out tAsset);
		return tAsset;
	}

	// Token: 0x06000249 RID: 585 RVA: 0x00015478 File Offset: 0x00013678
	public static ColorAsset tryMakeNewColorAsset(string pColorMain)
	{
		ColorAsset tAsset;
		ColorAsset._all_colors_dict.TryGetValue(pColorMain, out tAsset);
		if (tAsset == null)
		{
			tAsset = new ColorAsset(pColorMain);
		}
		return tAsset;
	}

	// Token: 0x0600024A RID: 586 RVA: 0x0001549E File Offset: 0x0001369E
	private ColorAsset(string pColorMain)
	{
		this.setMainHexColors(pColorMain, pColorMain, pColorMain);
		this.index_id = ColorAsset._create_last_index_id++;
		ColorAsset.saveToGlobalList(this, false);
	}

	// Token: 0x0600024B RID: 587 RVA: 0x000154CC File Offset: 0x000136CC
	public static void saveToGlobalList(ColorAsset pAsset, bool pMustBeGlobal = false)
	{
		if (ColorAsset.isColorAssetExists(pAsset.color_main))
		{
			if (pMustBeGlobal)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"ColorAsset with same <b>color_main</b> already exists in global list: ",
					pAsset.id,
					" ",
					pAsset.index_id.ToString(),
					" ",
					pAsset.color_main
				}));
			}
			return;
		}
		ColorAsset._all_colors_list.Add(pAsset);
		ColorAsset._all_colors_dict.Add(pAsset.color_main, pAsset);
	}

	// Token: 0x0600024C RID: 588 RVA: 0x0001554E File Offset: 0x0001374E
	private void setMainHexColors(string pColorMain, string pColorMain2, string pColorBanner)
	{
		this.color_main = pColorMain;
		this.color_main_2 = pColorMain2;
		this.color_banner = pColorBanner;
		this.color_text = pColorMain;
	}

	// Token: 0x0600024D RID: 589 RVA: 0x0001556C File Offset: 0x0001376C
	public void setEditorColors(Color pMain, Color pMain2, Color pBanner, Color pText)
	{
		this.initColor();
	}

	// Token: 0x0600024E RID: 590 RVA: 0x00015574 File Offset: 0x00013774
	public void initColor()
	{
		if (this._initialized)
		{
			return;
		}
		this._initialized = true;
		this._color_main = Toolbox.makeColor(this.color_main);
		this._color_main_32 = Toolbox.makeColor(this.color_main);
		this._color_main_second = Toolbox.makeColor(this.color_main_2);
		this._color_main_second_32 = Toolbox.makeColor(this.color_main_2);
		this._color_text = Toolbox.makeColor(this.color_text);
		this._color_banner = Toolbox.makeColor(this.color_banner);
		Color tColor = this._color_main_32;
		this._color_border_inside_alpha_32 = new Color(tColor.r, tColor.g, tColor.b);
		this._color_border_inside_alpha_32.a = 170;
		Color32 tColorDarkVersion = new Color32(30, 30, 30, byte.MaxValue);
		this._color_border_out_capture = new Color(this._color_main_second.r, this._color_main_second.g, this._color_main_second.b, 0.8f);
		this._color_unit_32 = Color.Lerp(this._color_main_second, Color.white, 0.3f);
		this._color_unit_32.a = byte.MaxValue;
		this.k_color_0 = this._color_text;
		this.k_color_0 = this.checkIfColorTooDark(this.k_color_0);
		this._color_minimap_element = Color32.Lerp(this.k_color_0, Color.white, 0.2f);
		this.k_color_1 = Color32.Lerp(this.k_color_0, tColorDarkVersion, 0.13f);
		this.k_color_2 = Color32.Lerp(this.k_color_0, tColorDarkVersion, 0.35000002f);
		this.k_color_3 = Color32.Lerp(this.k_color_0, tColorDarkVersion, 0.51f);
		this.k_color_4 = Color32.Lerp(this.k_color_0, tColorDarkVersion, 0.65999997f);
		this.k2_color_0 = this._color_main_32;
		this.k2_color_0 = this.checkIfColorTooDark(this.k2_color_0);
		this.k2_color_1 = Color32.Lerp(this.k2_color_0, tColorDarkVersion, 0.13f);
		this.k2_color_2 = Color32.Lerp(this.k2_color_0, tColorDarkVersion, 0.35000002f);
		this.k2_color_3 = Color32.Lerp(this.k2_color_0, tColorDarkVersion, 0.51f);
		this.k2_color_4 = Color32.Lerp(this.k2_color_0, tColorDarkVersion, 0.65999997f);
	}

	// Token: 0x0600024F RID: 591 RVA: 0x000157D0 File Offset: 0x000139D0
	public Material getChartLineMaterial()
	{
		if (this._material_line == null)
		{
			this._material_line = this.cloneMaterial("materials/graph/graph_base_line");
			Color tColor = this.getColorText();
			this._material_line.SetColor("_Color", tColor);
		}
		return this._material_line;
	}

	// Token: 0x06000250 RID: 592 RVA: 0x0001581C File Offset: 0x00013A1C
	public Material getChartInnerFillMaterial()
	{
		if (this._material_gradient == null)
		{
			this._material_gradient = this.cloneMaterial("materials/graph/graph_base_gradient");
			Color tColor = this.getColorText();
			tColor.a = 0.4f;
			Color tColor2 = this.getColorText();
			tColor2.a = 0.1f;
			this._material_gradient.SetColor("_ColorFrom", tColor);
			this._material_gradient.SetColor("_ColorTo", tColor2);
		}
		return this._material_gradient;
	}

	// Token: 0x06000251 RID: 593 RVA: 0x00015896 File Offset: 0x00013A96
	private Material cloneMaterial(string pPath)
	{
		return Object.Instantiate<Material>(Resources.Load<Material>(pPath));
	}

	// Token: 0x06000252 RID: 594 RVA: 0x000158A4 File Offset: 0x00013AA4
	private Color32 checkIfColorTooDark(Color32 pColor)
	{
		if (pColor.r < 128 && pColor.g < 128 && pColor.b < 128)
		{
			pColor.r += 50;
			pColor.g += 50;
			pColor.b += 50;
		}
		return pColor;
	}

	// Token: 0x06000253 RID: 595 RVA: 0x00015903 File Offset: 0x00013B03
	private Color32 getDarkerColor(Color32 pColor, byte pValue)
	{
		pColor.r += pValue;
		pColor.g += pValue;
		pColor.b += pValue;
		return pColor;
	}

	// Token: 0x06000254 RID: 596 RVA: 0x0001592D File Offset: 0x00013B2D
	public Color32 getColorUnit32()
	{
		return this._color_unit_32;
	}

	// Token: 0x06000255 RID: 597 RVA: 0x00015935 File Offset: 0x00013B35
	public Color32 getColorMain32()
	{
		return this._color_main_32;
	}

	// Token: 0x06000256 RID: 598 RVA: 0x0001593D File Offset: 0x00013B3D
	public Color32 getColorBorderInsideAlpha32()
	{
		return this._color_border_inside_alpha_32;
	}

	// Token: 0x06000257 RID: 599 RVA: 0x00015945 File Offset: 0x00013B45
	public Color32 getColorMainSecond32()
	{
		return this._color_main_second_32;
	}

	// Token: 0x06000258 RID: 600 RVA: 0x0001594D File Offset: 0x00013B4D
	public Color getColorMainSecond()
	{
		return this._color_main_second;
	}

	// Token: 0x06000259 RID: 601 RVA: 0x00015955 File Offset: 0x00013B55
	public Color getColorMain()
	{
		return this._color_main;
	}

	// Token: 0x0600025A RID: 602 RVA: 0x0001595D File Offset: 0x00013B5D
	public Color getColorText()
	{
		return this._color_text;
	}

	// Token: 0x0600025B RID: 603 RVA: 0x00015965 File Offset: 0x00013B65
	public ref Color getColorTextRef()
	{
		return ref this._color_text;
	}

	// Token: 0x0600025C RID: 604 RVA: 0x0001596D File Offset: 0x00013B6D
	public Color getColorMinimapElements()
	{
		return this._color_minimap_element;
	}

	// Token: 0x0600025D RID: 605 RVA: 0x00015975 File Offset: 0x00013B75
	public Color getColorBorderOut_capture()
	{
		return this._color_border_out_capture;
	}

	// Token: 0x0600025E RID: 606 RVA: 0x0001597D File Offset: 0x00013B7D
	public Color getColorBanner()
	{
		return this._color_banner;
	}

	// Token: 0x040001FA RID: 506
	private static int _create_last_index_id = 1000;

	// Token: 0x040001FB RID: 507
	public int index_id;

	// Token: 0x040001FC RID: 508
	public string color_main;

	// Token: 0x040001FD RID: 509
	public string color_main_2;

	// Token: 0x040001FE RID: 510
	public string color_banner;

	// Token: 0x040001FF RID: 511
	public string color_text;

	// Token: 0x04000200 RID: 512
	public bool favorite;

	// Token: 0x04000201 RID: 513
	[NonSerialized]
	public Color32 k_color_0;

	// Token: 0x04000202 RID: 514
	[NonSerialized]
	public Color32 k_color_1;

	// Token: 0x04000203 RID: 515
	[NonSerialized]
	public Color32 k_color_2;

	// Token: 0x04000204 RID: 516
	[NonSerialized]
	public Color32 k_color_3;

	// Token: 0x04000205 RID: 517
	[NonSerialized]
	public Color32 k_color_4;

	// Token: 0x04000206 RID: 518
	[NonSerialized]
	public Color32 k2_color_0;

	// Token: 0x04000207 RID: 519
	[NonSerialized]
	public Color32 k2_color_1;

	// Token: 0x04000208 RID: 520
	[NonSerialized]
	public Color32 k2_color_2;

	// Token: 0x04000209 RID: 521
	[NonSerialized]
	public Color32 k2_color_3;

	// Token: 0x0400020A RID: 522
	[NonSerialized]
	public Color32 k2_color_4;

	// Token: 0x0400020B RID: 523
	private Color32 _color_main_32;

	// Token: 0x0400020C RID: 524
	private Color32 _color_main_second_32;

	// Token: 0x0400020D RID: 525
	private Color32 _color_unit_32;

	// Token: 0x0400020E RID: 526
	private Color32 _color_border_inside_alpha_32;

	// Token: 0x0400020F RID: 527
	private Color _color_main;

	// Token: 0x04000210 RID: 528
	private Color _color_main_second;

	// Token: 0x04000211 RID: 529
	private Color _color_text;

	// Token: 0x04000212 RID: 530
	private Color _color_minimap_element;

	// Token: 0x04000213 RID: 531
	private Color _color_border_out_capture;

	// Token: 0x04000214 RID: 532
	private Color _color_banner;

	// Token: 0x04000215 RID: 533
	private static readonly List<ColorAsset> _all_colors_list = new List<ColorAsset>();

	// Token: 0x04000216 RID: 534
	private static readonly Dictionary<string, ColorAsset> _all_colors_dict = new Dictionary<string, ColorAsset>();

	// Token: 0x04000217 RID: 535
	public const byte ALPHA_BORDER_INSIDE_BYTE = 170;

	// Token: 0x04000218 RID: 536
	private bool _initialized;

	// Token: 0x04000219 RID: 537
	private Material _material_line;

	// Token: 0x0400021A RID: 538
	private Material _material_gradient;
}
