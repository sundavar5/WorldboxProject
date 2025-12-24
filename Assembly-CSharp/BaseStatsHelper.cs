using System;
using System.Collections.Generic;
using UnityEngine.UI;

// Token: 0x020001C2 RID: 450
public static class BaseStatsHelper
{
	// Token: 0x06000D35 RID: 3381 RVA: 0x000BB029 File Offset: 0x000B9229
	public static BaseStats getTotalStatsFrom(BaseStats pBaseStats, BaseStats pBaseStatsMeta)
	{
		BaseStatsHelper._base_stats_tooltip_helper.clear();
		BaseStatsHelper._base_stats_tooltip_helper.mergeStats(pBaseStats, 1f);
		BaseStatsHelper._base_stats_tooltip_helper.mergeStats(pBaseStatsMeta, 1f);
		return BaseStatsHelper._base_stats_tooltip_helper;
	}

	// Token: 0x06000D36 RID: 3382 RVA: 0x000BB05C File Offset: 0x000B925C
	public static void showItemMods(Text pTextFieldDescription, Text pTextFieldValues, Item pItem)
	{
		using (ListPool<TooltipModContainerInfo> tListMods = BaseStatsHelper.getItemModsBase(pItem))
		{
			foreach (TooltipModContainerInfo ptr in tListMods)
			{
				TooltipModContainerInfo tContainer = ptr;
				BaseStatsHelper.addStatValues(pTextFieldDescription, pTextFieldValues, Toolbox.coloredText("+" + LocalizedTextManager.getText(tContainer.asset.getLocaleID(), null, false), "#45FFFE", false), Toolbox.coloredText(tContainer.string_pluses, "#45FFFE", false));
				BaseStatsHelper.addLineBreak(pTextFieldDescription, pTextFieldValues);
			}
		}
	}

	// Token: 0x06000D37 RID: 3383 RVA: 0x000BB10C File Offset: 0x000B930C
	public static void showItemModsRows(BaseStatsHelper.KeyValueFieldGetter pFieldsFabric, Item pItem)
	{
		using (ListPool<TooltipModContainerInfo> tListMods = BaseStatsHelper.getItemModsBase(pItem))
		{
			foreach (TooltipModContainerInfo ptr in tListMods)
			{
				TooltipModContainerInfo tContainer = ptr;
				string tStats = Toolbox.coloredText("+" + LocalizedTextManager.getText(tContainer.asset.getLocaleID(), null, false), "#45FFFE", false);
				string tValues = Toolbox.coloredText(tContainer.string_pluses, "#45FFFE", false);
				KeyValueField keyValueField = pFieldsFabric(tContainer.asset.getLocaleID());
				keyValueField.name_text.text = tStats;
				keyValueField.value.text = tValues;
			}
		}
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x000BB1DC File Offset: 0x000B93DC
	private static ListPool<TooltipModContainerInfo> getItemModsBase(Item pItem)
	{
		ListPool<TooltipModContainerInfo> tListMods = new ListPool<TooltipModContainerInfo>(pItem.data.modifiers.Count);
		foreach (string ptr in pItem.data.modifiers)
		{
			string tModID = ptr;
			ItemModAsset tModAsset = AssetManager.items_modifiers.get(tModID);
			string tPluses = "";
			for (int i = 0; i < tModAsset.mod_rank; i++)
			{
				tPluses += "+";
			}
			tListMods.Add(new TooltipModContainerInfo(tModAsset, tModAsset.mod_rank, tPluses));
		}
		tListMods.Sort(new Comparison<TooltipModContainerInfo>(BaseStatsHelper.sortByPluses));
		return tListMods;
	}

	// Token: 0x06000D39 RID: 3385 RVA: 0x000BB2A4 File Offset: 0x000B94A4
	private static void addStatValues(Text pStatsField, Text pValuesField, string pStats, string pValues)
	{
		pStatsField.text += pStats;
		pValuesField.text += pValues;
	}

	// Token: 0x06000D3A RID: 3386 RVA: 0x000BB2CA File Offset: 0x000B94CA
	private static void addLineBreak(Text pStatsField, Text pValuesField)
	{
		pStatsField.text += "\n";
		pValuesField.text += "\n";
	}

	// Token: 0x06000D3B RID: 3387 RVA: 0x000BB2F8 File Offset: 0x000B94F8
	private static int sortByPluses(TooltipModContainerInfo pContainer1, TooltipModContainerInfo pContainer2)
	{
		return pContainer2.pluses.CompareTo(pContainer1.pluses);
	}

	// Token: 0x06000D3C RID: 3388 RVA: 0x000BB30C File Offset: 0x000B950C
	public static void showBaseStats(Text pStatsField, Text pValuesField, BaseStats pBaseStats, bool pAddPlus = true)
	{
		BaseStatsHelper.calcBaseStatsBase(pBaseStats);
		foreach (BaseStatsContainer tBaseStats in BaseStatsHelper._stats_container_positive)
		{
			BaseStatsHelper.showBaseStatLine(pStatsField, pValuesField, tBaseStats, true, pAddPlus, "#43FF43", false);
		}
		foreach (BaseStatsContainer tBaseStats2 in BaseStatsHelper._stats_container_negative)
		{
			BaseStatsHelper.showBaseStatLine(pStatsField, pValuesField, tBaseStats2, true, pAddPlus, "#43FF43", false);
		}
	}

	// Token: 0x06000D3D RID: 3389 RVA: 0x000BB3B8 File Offset: 0x000B95B8
	public static void showBaseStatsRows(BaseStatsHelper.KeyValueFieldGetter pFieldsFabric, BaseStats pBaseStats, bool pAddPlus = true)
	{
		BaseStatsHelper.calcBaseStatsBase(pBaseStats);
		foreach (BaseStatsContainer tBaseStats in BaseStatsHelper._stats_container_positive)
		{
			BaseStatsHelper.showBaseStatRow(pFieldsFabric, tBaseStats, true, pAddPlus, "#43FF43", false);
		}
		foreach (BaseStatsContainer tBaseStats2 in BaseStatsHelper._stats_container_negative)
		{
			BaseStatsHelper.showBaseStatRow(pFieldsFabric, tBaseStats2, true, pAddPlus, "#43FF43", false);
		}
	}

	// Token: 0x06000D3E RID: 3390 RVA: 0x000BB464 File Offset: 0x000B9664
	private static void calcBaseStatsBase(BaseStats pBaseStats)
	{
		BaseStatsHelper._stats_container_positive.Clear();
		BaseStatsHelper._stats_container_negative.Clear();
		foreach (BaseStatsContainer tContainer in pBaseStats.getList())
		{
			if (!tContainer.asset.hidden || DebugConfig.isOn(DebugOption.ShowHiddenStats))
			{
				BaseStatsHelper.queueStatContainer(tContainer);
			}
		}
		BaseStatsHelper._stats_container_positive.Sort(new Comparison<BaseStatsContainer>(BaseStatsHelper.sortByRank));
	}

	// Token: 0x06000D3F RID: 3391 RVA: 0x000BB4FC File Offset: 0x000B96FC
	private static int sortByRank(BaseStatsContainer pContainerA, BaseStatsContainer pContainerB)
	{
		BaseStatAsset tAssetA = pContainerA.asset;
		return pContainerB.asset.sort_rank.CompareTo(tAssetA.sort_rank);
	}

	// Token: 0x06000D40 RID: 3392 RVA: 0x000BB526 File Offset: 0x000B9726
	private static void queueStatContainer(BaseStatsContainer pContainer)
	{
		if (pContainer.value > 0f)
		{
			BaseStatsHelper._stats_container_positive.Add(pContainer);
		}
		if (pContainer.value < 0f)
		{
			BaseStatsHelper._stats_container_negative.Add(pContainer);
		}
	}

	// Token: 0x06000D41 RID: 3393 RVA: 0x000BB558 File Offset: 0x000B9758
	private static void showBaseStatLine(Text pStatsField, Text pValuesField, BaseStatsContainer pContainer, bool pAddColor = true, bool pAddPlus = true, string pMainColor = "#43FF43", bool pForceZero = false)
	{
		string tId;
		float tValue;
		BaseStatAsset tAsset;
		BaseStatsHelper.calcBaseStatLineBase(pContainer, out tId, out tValue, out tAsset);
		if (!tAsset.hidden)
		{
			BaseStatsHelper.addItemText(pStatsField, pValuesField, tId, tValue, tAsset.show_as_percents, pAddColor, pAddPlus, pMainColor, pForceZero);
			return;
		}
		if (pStatsField.text.Length > 0)
		{
			BaseStatsHelper.addLineBreak(pStatsField, pValuesField);
		}
		string tFinalTextLeft = tId;
		string tFinalTextRight = tValue.ToText();
		if (tAsset.show_as_percents)
		{
			tFinalTextRight += " %";
		}
		pValuesField.text += Toolbox.coloredText(tFinalTextRight, ColorStyleLibrary.m.color_text_grey, false);
		pStatsField.text += Toolbox.coloredText(tFinalTextLeft, ColorStyleLibrary.m.color_text_grey, false);
	}

	// Token: 0x06000D42 RID: 3394 RVA: 0x000BB60C File Offset: 0x000B980C
	private static void showBaseStatRow(BaseStatsHelper.KeyValueFieldGetter pFieldsFabric, BaseStatsContainer pContainer, bool pAddColor = true, bool pAddPlus = true, string pMainColor = "#43FF43", bool pForceZero = false)
	{
		string tId;
		float tValue;
		BaseStatAsset tAsset;
		BaseStatsHelper.calcBaseStatLineBase(pContainer, out tId, out tValue, out tAsset);
		BaseStatsHelper.addItemTextRow(pFieldsFabric(tId), tId, tValue, tAsset.show_as_percents, pAddColor, pAddPlus, pMainColor, pForceZero);
	}

	// Token: 0x06000D43 RID: 3395 RVA: 0x000BB640 File Offset: 0x000B9840
	private static void calcBaseStatLineBase(BaseStatsContainer pContainer, out string tId, out float tValue, out BaseStatAsset tAsset)
	{
		tAsset = pContainer.asset;
		tId = tAsset.getLocaleID();
		tValue = pContainer.value;
		if (tAsset.tooltip_multiply_for_visual_number != 1f)
		{
			tValue *= tAsset.tooltip_multiply_for_visual_number;
		}
		if (tAsset.hidden && DebugConfig.isOn(DebugOption.ShowHiddenStats))
		{
			tId = "[HIDDEN] " + tId;
		}
	}

	// Token: 0x06000D44 RID: 3396 RVA: 0x000BB6A4 File Offset: 0x000B98A4
	private static void addItemText(Text pStatsField, Text pValuesField, string pID, float pValue, bool pPercent = false, bool pAddColor = true, bool pAddPlus = true, string pMainColor = "#43FF43", bool pForceZero = false)
	{
		string tValString;
		BaseStatsHelper.addItemTextBase(pValue, out tValString, pPercent, pForceZero);
		if (!pAddColor)
		{
			BaseStatsHelper.addLineText(pStatsField, pValuesField, pID, tValString, null, pPercent);
			return;
		}
		if (pValue > 0f)
		{
			if (pAddPlus)
			{
				tValString = "+" + tValString;
			}
			BaseStatsHelper.addLineText(pStatsField, pValuesField, pID, tValString, pMainColor, pPercent);
			return;
		}
		BaseStatsHelper.addLineText(pStatsField, pValuesField, pID, tValString, "#FB2C21", pPercent);
	}

	// Token: 0x06000D45 RID: 3397 RVA: 0x000BB704 File Offset: 0x000B9904
	private static void addItemTextRow(KeyValueField pField, string pID, float pValue, bool pPercent = false, bool pAddColor = true, bool pAddPlus = true, string pMainColor = "#43FF43", bool pForceZero = false)
	{
		string tValString;
		BaseStatsHelper.addItemTextBase(pValue, out tValString, pPercent, pForceZero);
		if (!pAddColor)
		{
			BaseStatsHelper.addRowText(pField, pID, tValString, null, pPercent);
			return;
		}
		if (pValue > 0f)
		{
			if (pAddPlus)
			{
				tValString = "+" + tValString;
			}
			BaseStatsHelper.addRowText(pField, pID, tValString, pMainColor, pPercent);
			return;
		}
		BaseStatsHelper.addRowText(pField, pID, tValString, "#FB2C21", pPercent);
	}

	// Token: 0x06000D46 RID: 3398 RVA: 0x000BB75D File Offset: 0x000B995D
	private static void addItemTextBase(float pValue, out string pValString, bool pPercent = false, bool pForceZero = false)
	{
		pValString = pValue.ToText();
		if (pValue == 0f && !pForceZero)
		{
			return;
		}
		if (pPercent)
		{
			pValString += "%";
		}
	}

	// Token: 0x06000D47 RID: 3399 RVA: 0x000BB784 File Offset: 0x000B9984
	private static void addLineIntText(Text pStatsField, Text pValuesField, string pID, int pValue, string pColor = null)
	{
		BaseStatsHelper.addLineText(pStatsField, pValuesField, pID, pValue.ToText(), pColor, false);
	}

	// Token: 0x06000D48 RID: 3400 RVA: 0x000BB798 File Offset: 0x000B9998
	private static void addLineText(Text pStatsField, Text pValuesField, string pID, string pValue, string pColor = null, bool pPercent = false)
	{
		if (pStatsField.text.Length > 0)
		{
			BaseStatsHelper.addLineBreak(pStatsField, pValuesField);
		}
		if (pValue.Length > 21)
		{
			pValue = pValue.Substring(0, 20) + "...";
		}
		string tFinalText = LocalizedTextManager.getText(pID, null, false);
		if (pPercent)
		{
			tFinalText += " %";
		}
		if (!string.IsNullOrEmpty(pColor))
		{
			pStatsField.text += tFinalText;
			pValuesField.text += Toolbox.coloredText(pValue, pColor, false);
			return;
		}
		pStatsField.text += tFinalText;
		pValuesField.text += pValue;
	}

	// Token: 0x06000D49 RID: 3401 RVA: 0x000BB84C File Offset: 0x000B9A4C
	private static void addRowText(KeyValueField pField, string pID, string pValue, string pColor = null, bool pPercent = false)
	{
		if (pValue.Length > 21)
		{
			pValue = pValue.Substring(0, 20) + "...";
		}
		string tLocalizedText;
		if (pID.Contains("[HIDDEN]"))
		{
			tLocalizedText = pID;
			pColor = ColorStyleLibrary.m.color_text_grey;
		}
		else
		{
			tLocalizedText = LocalizedTextManager.getText(pID, null, false);
		}
		if (pPercent)
		{
			tLocalizedText += " %";
		}
		if (!string.IsNullOrEmpty(pColor))
		{
			pField.name_text.text = Toolbox.coloredText(tLocalizedText, pColor, false);
			pField.value.text = Toolbox.coloredText(pValue, pColor, false);
			return;
		}
		pField.name_text.text = tLocalizedText;
		pField.value.text = pValue;
	}

	// Token: 0x04000CA4 RID: 3236
	public static BaseStats _base_stats_tooltip_helper = new BaseStats();

	// Token: 0x04000CA5 RID: 3237
	private static List<BaseStatsContainer> _stats_container_positive = new List<BaseStatsContainer>();

	// Token: 0x04000CA6 RID: 3238
	private static List<BaseStatsContainer> _stats_container_negative = new List<BaseStatsContainer>();

	// Token: 0x020009D6 RID: 2518
	// (Invoke) Token: 0x06004B04 RID: 19204
	public delegate KeyValueField KeyValueFieldGetter(string pID);
}
