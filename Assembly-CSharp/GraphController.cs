using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ChartAndGraph;
using db;
using db.tables;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020006BB RID: 1723
public class GraphController : MonoBehaviour
{
	// Token: 0x06003730 RID: 14128 RVA: 0x0018E9DC File Offset: 0x0018CBDC
	private void Awake()
	{
		this._container_time_scale = base.transform.GetComponentInChildren<GraphTimeScaleContainer>();
		this._vertical_axis = base.transform.GetComponentInChildren<VerticalAxis>();
		this._horizontal_axis = base.transform.GetComponentInChildren<HorizontalAxis>();
		this._container_graph_categories = base.transform.GetComponentInChildren<GraphCategoriesContainer>();
	}

	// Token: 0x06003731 RID: 14129 RVA: 0x0018EA2D File Offset: 0x0018CC2D
	internal List<NanoObject> getObjects()
	{
		return this._current_objects;
	}

	// Token: 0x06003732 RID: 14130 RVA: 0x0018EA35 File Offset: 0x0018CC35
	internal List<HistoryDataAsset> getCategories()
	{
		return this._list_categories;
	}

	// Token: 0x06003733 RID: 14131 RVA: 0x0018EA3D File Offset: 0x0018CC3D
	internal bool hasCategory(HistoryDataAsset pCategory)
	{
		return this._list_categories.Contains(pCategory);
	}

	// Token: 0x06003734 RID: 14132 RVA: 0x0018EA4C File Offset: 0x0018CC4C
	internal bool hasCategory(string pCategory)
	{
		HistoryDataAsset tCategory = AssetManager.history_data_library.get(pCategory);
		return this.hasCategory(tCategory);
	}

	// Token: 0x06003735 RID: 14133 RVA: 0x0018EA6C File Offset: 0x0018CC6C
	private static string getCategoryName(string pCategory)
	{
		return GraphHelpers.getCategoryName(pCategory);
	}

	// Token: 0x06003736 RID: 14134 RVA: 0x0018EA74 File Offset: 0x0018CC74
	private NanoObject extractObject(string pCategory)
	{
		if (!pCategory.Contains('|'))
		{
			return null;
		}
		string text = pCategory.Split('|', StringSplitOptions.None)[0];
		string tType = pCategory.Split('|', StringSplitOptions.None)[1];
		string tTypeID = pCategory.Split('|', StringSplitOptions.None)[2];
		foreach (NanoObject tObject in this._current_objects)
		{
			if (tObject.getType() == tType && tObject.getTypeID() == tTypeID)
			{
				return tObject;
			}
		}
		return null;
	}

	// Token: 0x06003737 RID: 14135 RVA: 0x0018EB18 File Offset: 0x0018CD18
	internal bool isCategoryEnabled(string pCategory)
	{
		string tCategoryName = GraphController.getCategoryName(pCategory);
		return this._category_enabled[tCategoryName];
	}

	// Token: 0x06003738 RID: 14136 RVA: 0x0018EB38 File Offset: 0x0018CD38
	internal string getActiveCategory()
	{
		foreach (string tCategory in this._category_enabled.Keys)
		{
			if (this._category_enabled[tCategory])
			{
				return tCategory;
			}
		}
		return null;
	}

	// Token: 0x06003739 RID: 14137 RVA: 0x0018EBA0 File Offset: 0x0018CDA0
	private void loadCategories()
	{
		if (this._categories_loaded)
		{
			return;
		}
		this._categories_loaded = true;
		this._list_categories.Clear();
		HashSet<HistoryDataAsset> tCommonCategories = new HashSet<HistoryDataAsset>();
		foreach (MetaType tType in this._current_types)
		{
			HistoryMetaDataAsset[] assets = AssetManager.history_meta_data_library.getAssets(tType);
			HashSet<HistoryDataAsset> tMetaCategories = new HashSet<HistoryDataAsset>();
			foreach (HistoryMetaDataAsset tHistoryAsset in assets)
			{
				tMetaCategories.UnionWith(tHistoryAsset.categories);
			}
			if (tCommonCategories.Count == 0)
			{
				tCommonCategories.UnionWith(tMetaCategories);
			}
			else
			{
				tCommonCategories.IntersectWith(tMetaCategories);
			}
		}
		foreach (NanoObject tCurrentObject in this._current_objects)
		{
			foreach (HistoryDataAsset tDataAsset in tCommonCategories)
			{
				if (!this.hasCategory(tDataAsset))
				{
					this.addCategory(tDataAsset, tDataAsset.enabled_default);
				}
				this.colorCategory(tDataAsset, tCurrentObject, this.multi_chart);
			}
		}
	}

	// Token: 0x0600373A RID: 14138 RVA: 0x0018ED00 File Offset: 0x0018CF00
	internal void addCategory(HistoryDataAsset pAsset, bool pEnabled = false)
	{
		this._list_categories.Add(pAsset);
		this._category_enabled[pAsset.id] = pEnabled;
	}

	// Token: 0x0600373B RID: 14139 RVA: 0x0018ED20 File Offset: 0x0018CF20
	internal void disableAllCategories(string pExcept = null)
	{
		foreach (HistoryDataAsset tCategory in this.getCategories())
		{
			if (!(tCategory.id == pExcept))
			{
				this.setCategoryEnabled(tCategory.id, false, false);
			}
		}
	}

	// Token: 0x0600373C RID: 14140 RVA: 0x0018ED88 File Offset: 0x0018CF88
	internal void pickRandomCategory()
	{
		using (ListPool<string> tBestCategories = GraphHelpers.bestCategories(this._min_max_categories))
		{
			if (tBestCategories.Count != 0)
			{
				string tCategory = tBestCategories.GetRandom<string>();
				this.tryEnableCategory(tCategory);
			}
		}
	}

	// Token: 0x0600373D RID: 14141 RVA: 0x0018EDD8 File Offset: 0x0018CFD8
	internal void tryEnableCategory(string pCategory)
	{
		if (string.IsNullOrEmpty(pCategory))
		{
			return;
		}
		if (!this.hasCategory(pCategory))
		{
			return;
		}
		this._container_graph_categories.setCategoryEnabled(pCategory, true);
	}

	// Token: 0x0600373E RID: 14142 RVA: 0x0018EDFC File Offset: 0x0018CFFC
	internal void setCategoryEnabled(string pCategory, bool pIsOn, bool pUpdateGraph = true)
	{
		this._category_enabled[pCategory] = pIsOn;
		foreach (string tCategory in this.chart.DataSource.CategoryNames)
		{
			if (tCategory.StartsWith(pCategory + "|"))
			{
				this.chart.DataSource.SetCategoryEnabled(tCategory, pIsOn);
			}
		}
		if (pUpdateGraph)
		{
			this.updateGraph();
		}
	}

	// Token: 0x0600373F RID: 14143 RVA: 0x0018EE88 File Offset: 0x0018D088
	private void hookEvents()
	{
		if (this._events_hooked)
		{
			return;
		}
		this._events_hooked = true;
		this.chart.PointHovered.AddListener(delegate(GraphChartBase.GraphEventArgs _)
		{
			Tooltip.cancelHiding();
		});
		if (this.multi_chart)
		{
			this.chart.PointHovered.AddListener(new UnityAction<GraphChartBase.GraphEventArgs>(this.multiChartHover));
		}
		else
		{
			this.chart.PointHovered.AddListener(new UnityAction<GraphChartBase.GraphEventArgs>(this.singleChartHover));
		}
		this.chart.NonHovered.AddListener(delegate()
		{
			Tooltip.scheduledHide(0.15f, true);
		});
	}

	// Token: 0x06003740 RID: 14144 RVA: 0x0018EF48 File Offset: 0x0018D148
	private void multiChartHover(GraphChartBase.GraphEventArgs pArgs)
	{
		long tYear = (long)pArgs.Value.x;
		string tCategoryName = GraphController.getCategoryName(pArgs.Category);
		if (Tooltip.anyActive())
		{
			Tooltip tTooltip = Tooltip.findActive((Tooltip pTooltip) => !(pTooltip.asset.id != "graph_multi_resource") && !(pTooltip.data.tip_name != tCategoryName) && pTooltip.data.custom_data_long["year"] == tYear);
			if (tTooltip != null)
			{
				tTooltip.reposition();
				return;
			}
		}
		CustomDataContainer<string> tColorData = new CustomDataContainer<string>();
		CustomDataContainer<long> tValueData = new CustomDataContainer<long>();
		tValueData["year"] = tYear;
		foreach (string tCategory in this.chart.DataSource.CategoryNames)
		{
			if (this.isCategoryEnabled(tCategory))
			{
				NanoObject tObject = this.extractObject(tCategory);
				string tObjectName = tObject.name;
				ValueTuple<long, long> categoryValueAtTime = this.getCategoryValueAtTime(tCategory, (long)pArgs.Value.x);
				long tValue = categoryValueAtTime.Item1;
				long tPrevious = categoryValueAtTime.Item2;
				tValueData[tObjectName] = tValue;
				tValueData[tObjectName + "_previous"] = tPrevious;
				tColorData[tObjectName] = Toolbox.colorToHex(tObject.getColor().getColorText(), true);
			}
		}
		Tooltip.show(pArgs.Position, "graph_multi_resource", new TooltipData
		{
			tip_name = tCategoryName,
			custom_data_long = tValueData,
			custom_data_string = tColorData
		});
	}

	// Token: 0x06003741 RID: 14145 RVA: 0x0018F0C4 File Offset: 0x0018D2C4
	private void singleChartHover(GraphChartBase.GraphEventArgs pArgs)
	{
		Tooltip.hideTooltip();
		CustomDataContainer<long> tCustomData = new CustomDataContainer<long>();
		tCustomData["year"] = (long)pArgs.Value.x;
		foreach (string tCategory in this.chart.DataSource.CategoryNames)
		{
			if (this.isCategoryEnabled(tCategory))
			{
				string tCategoryName = GraphController.getCategoryName(tCategory);
				ValueTuple<long, long> categoryValueAtTime = this.getCategoryValueAtTime(tCategory, (long)pArgs.Value.x);
				long tValue = categoryValueAtTime.Item1;
				long tPrevious = categoryValueAtTime.Item2;
				tCustomData[tCategoryName] = tValue;
				tCustomData[tCategoryName + "_previous"] = tPrevious;
			}
		}
		NanoObject tObject = this.extractObject(pArgs.Category);
		Tooltip.show(pArgs.Position, "graph_resource", new TooltipData
		{
			custom_data_long = tCustomData,
			nano_object = tObject
		});
	}

	// Token: 0x06003742 RID: 14146 RVA: 0x0018F1BC File Offset: 0x0018D3BC
	public void resetAndUpdateGraph()
	{
		this._loaded = false;
		this._categories_loaded = false;
		this._current_interval = HistoryInterval.None;
		this._container_time_scale.resetTimeScale();
		this.updateGraph();
		this._container_time_scale.calcBounds();
	}

	// Token: 0x06003743 RID: 14147 RVA: 0x0018F1F0 File Offset: 0x0018D3F0
	public bool randomTimeScale()
	{
		if (this._container_time_scale.randomizeTimeScale())
		{
			this.updateGraph();
			return true;
		}
		return false;
	}

	// Token: 0x06003744 RID: 14148 RVA: 0x0018F208 File Offset: 0x0018D408
	public void forceUpdateGraph()
	{
		this.updateGraph();
	}

	// Token: 0x06003745 RID: 14149 RVA: 0x0018F210 File Offset: 0x0018D410
	private void updateGraph()
	{
		if (Config.disable_db)
		{
			return;
		}
		if (!Config.graphs)
		{
			return;
		}
		this.chart.DataSource.StartBatch();
		this.loadGraph();
		if (this._container_time_scale.resetTimeScale())
		{
			this.clearChartData();
		}
		this.loadSample();
		this.loadCategoryAndCharts();
		this.adjustCharts();
		this.chart.DataSource.EndBatch();
	}

	// Token: 0x06003746 RID: 14150 RVA: 0x0018F278 File Offset: 0x0018D478
	private void loadGraph()
	{
		if (this._loaded)
		{
			return;
		}
		this._loaded = true;
		this.chart.GetComponent<HorizontalAxis>().CustomNumberFormatWorldbox = new Func<double, int, string>(GraphHelpers.horizontalFormatYears);
		this.chart.CustomNumberFormat = new Func<double, int, string>(GraphHelpers.verticalFormat);
		this._vertical_axis.enabled = true;
		this._horizontal_axis.enabled = true;
		if (this.multi_chart)
		{
			this.loadMultiChart();
		}
		else
		{
			this.loadSingleChart();
		}
		this.hookEvents();
	}

	// Token: 0x06003747 RID: 14151 RVA: 0x0018F2FC File Offset: 0x0018D4FC
	private void loadSingleChart()
	{
		NanoObject tSelectedObject = AssetManager.meta_type_library.getAsset(this._meta_type).get_selected();
		this.selectContainer(tSelectedObject);
	}

	// Token: 0x06003748 RID: 14152 RVA: 0x0018F32C File Offset: 0x0018D52C
	private void loadMultiChart()
	{
		this._current_types.Clear();
		this._current_objects.Clear();
		this._last_data.Clear();
		foreach (NanoObject tObject in Config.selected_objects_graph)
		{
			if (tObject != null && tObject.isAlive())
			{
				this.addContainer(tObject);
			}
		}
		this.clearChartData();
		this._category_enabled.Clear();
	}

	// Token: 0x06003749 RID: 14153 RVA: 0x0018F3B8 File Offset: 0x0018D5B8
	private void showCategory(string pCategory, NanoObject pObject)
	{
		string tType = pObject.getType();
		string tTypeID = pObject.getTypeID();
		CategoryData tData = this._current_datas[tTypeID];
		string tCategoryName = string.Concat(new string[]
		{
			pCategory,
			"|",
			tType,
			"|",
			tTypeID
		});
		for (LinkedListNode<Dictionary<string, long>> tNode = tData.Last; tNode != null; tNode = tNode.Previous)
		{
			if (tNode.Value.ContainsKey(pCategory))
			{
				long tValue = tNode.Value[pCategory];
				long tTimestamp = tNode.Value["timestamp"];
				bool tHide = false;
				LinkedListNode<Dictionary<string, long>> previous = tNode.Previous;
				long tPrevValue = (previous != null) ? previous.Value[pCategory] : 0L;
				LinkedListNode<Dictionary<string, long>> next = tNode.Next;
				long tNextValue = (next != null) ? next.Value[pCategory] : 0L;
				if (tValue == tPrevValue && tValue == tNextValue)
				{
					tHide = true;
				}
				this.chart.DataSource.AddPointToCategory(tCategoryName, (double)tTimestamp, (double)tValue, (double)(tHide ? 0f : -1f));
			}
		}
	}

	// Token: 0x0600374A RID: 14154 RVA: 0x0018F4CC File Offset: 0x0018D6CC
	[return: TupleElementNames(new string[]
	{
		"tValue",
		"tPrevious"
	})]
	private ValueTuple<long, long> getCategoryValueAtTime(string pCategory, long pTime)
	{
		string tCategory = GraphController.getCategoryName(pCategory);
		string tTypeID = pCategory.Split('|', StringSplitOptions.None).Last<string>();
		CategoryData categoryData = this._current_datas[tTypeID];
		long tValue = 0L;
		long tPrevious = 0L;
		bool tFound = false;
		for (LinkedListNode<Dictionary<string, long>> tNode = categoryData.Last; tNode != null; tNode = tNode.Previous)
		{
			if (tNode.Value.ContainsKey(tCategory))
			{
				if (tFound)
				{
					tPrevious = tNode.Value[tCategory];
					break;
				}
				long tTimestamp = tNode.Value["timestamp"];
				if (tTimestamp <= pTime)
				{
					if (tTimestamp <= pTime)
					{
						tValue = tNode.Value[tCategory];
					}
					tFound = true;
				}
			}
		}
		return new ValueTuple<long, long>(tValue, tPrevious);
	}

	// Token: 0x0600374B RID: 14155 RVA: 0x0018F574 File Offset: 0x0018D774
	private void colorCategory(HistoryDataAsset pHistoryDataAsset, NanoObject pObject, bool pColorFromObject = false)
	{
		string tType = pObject.getType();
		string tTypeID = pObject.getTypeID();
		string tCategory = pHistoryDataAsset.id;
		string tCategoryName = string.Concat(new string[]
		{
			tCategory,
			"|",
			tType,
			"|",
			tTypeID
		});
		float tLineThickness = 2f;
		MaterialTiling tTiling = default(MaterialTiling);
		tTiling.EnableTiling = false;
		bool tStretchFill = true;
		Material tChartLineMaterial;
		Material tChartInnerFillMaterial;
		if (pColorFromObject)
		{
			Color colorText = pObject.getColor().getColorText();
			tChartLineMaterial = HistoryDataAsset.getChartLineMaterial(colorText);
			tChartInnerFillMaterial = HistoryDataAsset.getChartInnerFillMaterial(colorText);
		}
		else
		{
			tChartLineMaterial = pHistoryDataAsset.getChartLineMaterial();
			tChartInnerFillMaterial = pHistoryDataAsset.getChartInnerFillMaterial();
		}
		this.chart.DataSource.AddCategory(tCategoryName, tChartLineMaterial, (double)tLineThickness, tTiling, tChartInnerFillMaterial, tStretchFill, null, 0.0, false);
		this.chart.DataSource.SetCategoryEnabled(tCategoryName, this.isCategoryEnabled(tCategory));
		this.chart.DataSource.Set2DCategoryPrefabs(tCategoryName, null, pHistoryDataAsset.getHoverPointMaterial());
		int tPointSize = 10;
		this.chart.DataSource.SetCategoryPoint(tCategoryName, pHistoryDataAsset.getChartPointMaterial(), (double)tPointSize);
	}

	// Token: 0x0600374C RID: 14156 RVA: 0x0018F67C File Offset: 0x0018D87C
	private MinMax getMinMax(string pCategoryName)
	{
		long tMin = long.MaxValue;
		long tMax = long.MinValue;
		bool tFound = false;
		string tCategory = pCategoryName.Split('|', StringSplitOptions.None)[0];
		string text = pCategoryName.Split('|', StringSplitOptions.None)[1];
		string tTypeID = pCategoryName.Split('|', StringSplitOptions.None)[2];
		if (this._current_datas.Count == 0 || !this._current_datas.ContainsKey(tTypeID))
		{
			return new MinMax(0L, 0L);
		}
		for (LinkedListNode<Dictionary<string, long>> tNode = this._current_datas[tTypeID].Last; tNode != null; tNode = tNode.Previous)
		{
			Dictionary<string, long> tData = tNode.Value;
			if (tData.ContainsKey(tCategory))
			{
				long tValue = tData[tCategory];
				long tTimestamp = tData["timestamp"];
				if (tFound && tTimestamp < this._min_timestamp)
				{
					break;
				}
				if (tValue < tMin)
				{
					tMin = tValue;
				}
				if (tValue > tMax)
				{
					tMax = tValue;
				}
				tFound = true;
			}
		}
		if (!tFound)
		{
			return new MinMax(0L, 0L);
		}
		return new MinMax(tMin, tMax);
	}

	// Token: 0x0600374D RID: 14157 RVA: 0x0018F76C File Offset: 0x0018D96C
	internal void adjustCharts()
	{
		long tMinValue = long.MaxValue;
		long tMaxValue = 0L;
		this._min_max_categories.Clear();
		foreach (string tCategory in this.chart.DataSource.CategoryNames)
		{
			MinMax tMinMax = this.getMinMax(tCategory);
			this._min_max_categories.Add(tCategory, tMinMax);
			if (this.isCategoryEnabled(tCategory))
			{
				if (tMinMax.max > tMaxValue)
				{
					tMaxValue = tMinMax.max;
				}
				if (tMinMax.min < tMinValue)
				{
					tMinValue = tMinMax.min;
				}
			}
		}
		tMaxValue = GraphHelpers.calculateNiceMaxAxisSize((double)tMaxValue * 1.05);
		int tVerticalDivisions = GraphHelpers.findVerticalDivision(tMaxValue);
		if (tMinValue >= 0L)
		{
			tMinValue = 0L;
		}
		else
		{
			tMinValue = GraphHelpers.calculateNiceMaxAxisSize((double)(-(double)tMinValue) * 1.05);
			if (tMinValue < tMaxValue)
			{
				long tMultiplier = tMaxValue / (long)tVerticalDivisions;
				int tMinValueMultiplier = Mathf.CeilToInt((float)tMinValue / (float)tMultiplier);
				tMinValue = (long)tMinValueMultiplier * tMultiplier;
				tVerticalDivisions += tMinValueMultiplier;
			}
			else
			{
				tVerticalDivisions = GraphHelpers.findVerticalDivision(tMinValue);
				long tMultiplier2 = tMinValue / (long)tVerticalDivisions;
				int tMaxValueMultiplier = Mathf.CeilToInt((float)tMaxValue / (float)tMultiplier2);
				tMaxValue = (long)tMaxValueMultiplier * tMultiplier2;
				tVerticalDivisions += tMaxValueMultiplier;
			}
		}
		this.chart.DataSource.VerticalViewOrigin = (double)(-(double)tMinValue);
		this.chart.DataSource.VerticalViewSize = (double)(tMaxValue + tMinValue);
		this.chart.DataSource.HorizontalViewOrigin = (double)GraphTimeLibrary.getMinTime(this._current_sample);
		this.chart.DataSource.HorizontalViewSize = (double)(GraphTimeLibrary.getMaxTime(this._current_sample) - GraphTimeLibrary.getMinTime(this._current_sample));
		this._horizontal_axis.MainDivisions.Total = 5;
		this._horizontal_axis.MainDivisions.FractionDigits = 2;
		this._vertical_axis.MainDivisions.Total = tVerticalDivisions;
		GraphController.min_max = new MinMax(-tMinValue, tMaxValue);
	}

	// Token: 0x0600374E RID: 14158 RVA: 0x0018F948 File Offset: 0x0018DB48
	private void loadSample()
	{
		GraphTimeScale tScale = this._container_time_scale.getCurrentScale();
		this._current_sample = AssetManager.graph_time_library.get(tScale.ToString());
		bool tClear = false;
		this._current_interval = this._current_sample.interval;
		foreach (NanoObject tCurrentObject in this._current_objects)
		{
			string tTypeID = tCurrentObject.getTypeID();
			CategoryData tData;
			if (!this._current_datas.TryGetValue(tTypeID, out tData))
			{
				tData = new CategoryData();
				this._current_datas[tTypeID] = tData;
			}
			if (DBGetter.getData(tData, tCurrentObject, this._current_interval, this._last_data[tCurrentObject]))
			{
				tClear = true;
			}
		}
		if (tClear)
		{
			this.clearChartData();
		}
		this._min_timestamp = GraphTimeLibrary.getMinTime(this._current_sample);
		this._max_timestamp = GraphTimeLibrary.getMaxTime(this._current_sample);
	}

	// Token: 0x0600374F RID: 14159 RVA: 0x0018FA48 File Offset: 0x0018DC48
	private void clearChartData()
	{
		this._categories_loaded = false;
		this.chart.DataSource.Clear();
	}

	// Token: 0x06003750 RID: 14160 RVA: 0x0018FA64 File Offset: 0x0018DC64
	private void loadCategoryAndCharts()
	{
		this.loadCategories();
		foreach (NanoObject tCurrentObject in this._current_objects)
		{
			foreach (HistoryDataAsset historyDataAsset in this._list_categories)
			{
				string tCategory = historyDataAsset.id;
				this.showCategory(tCategory, tCurrentObject);
			}
		}
		this._container_graph_categories.apply();
	}

	// Token: 0x06003751 RID: 14161 RVA: 0x0018FB0C File Offset: 0x0018DD0C
	private void selectContainer(NanoObject pMetaObject)
	{
		MetaType tMetaType = pMetaObject.getMetaType();
		if (!this._current_types.Contains(tMetaType))
		{
			this._category_enabled.Clear();
			this.clearChartData();
		}
		else if (!this._current_objects.Contains(pMetaObject))
		{
			this.clearChartData();
		}
		this._current_types.Clear();
		this._current_objects.Clear();
		this._last_data.Clear();
		this._current_types.Add(tMetaType);
		this._current_objects.Add(pMetaObject);
		foreach (HistoryMetaDataAsset tHistoryAsset in AssetManager.history_meta_data_library.getAssets(tMetaType))
		{
			this._last_data[pMetaObject] = tHistoryAsset.collector(pMetaObject);
			this._last_data[pMetaObject].timestamp = (long)Date.getCurrentYear();
		}
	}

	// Token: 0x06003752 RID: 14162 RVA: 0x0018FBDC File Offset: 0x0018DDDC
	private void addContainer(NanoObject pMetaObject)
	{
		MetaType tMetaType = pMetaObject.getMetaType();
		this._current_types.Add(tMetaType);
		this._current_objects.Add(pMetaObject);
		foreach (HistoryMetaDataAsset tHistoryAsset in AssetManager.history_meta_data_library.getAssets(tMetaType))
		{
			this._last_data[pMetaObject] = tHistoryAsset.collector(pMetaObject);
			this._last_data[pMetaObject].timestamp = (long)Date.getCurrentYear();
		}
	}

	// Token: 0x06003753 RID: 14163 RVA: 0x0018FC56 File Offset: 0x0018DE56
	private void clearGraph()
	{
		this.clearChartData();
		this._category_enabled.Clear();
		this._list_categories.Clear();
		this._loaded = false;
		this._container_graph_categories.apply();
	}

	// Token: 0x06003754 RID: 14164 RVA: 0x0018FC88 File Offset: 0x0018DE88
	internal void load()
	{
		this._loaded = false;
		if (this.multi_chart)
		{
			this._current_interval = HistoryInterval.None;
			this._container_time_scale.resetTimeScale();
		}
		if (this.clear_on_enable)
		{
			this.clearGraph();
		}
		if (this._last_timestamp != (long)Date.getMonthsSince(0.0))
		{
			this._last_timestamp = (long)Date.getMonthsSince(0.0);
			foreach (CategoryData categoryData in this._current_datas.Values)
			{
				categoryData.Dispose();
			}
			this._current_datas.Clear();
			this.clearChartData();
		}
		this.updateGraph();
		this._container_time_scale.calcBounds();
	}

	// Token: 0x06003755 RID: 14165 RVA: 0x0018FD5C File Offset: 0x0018DF5C
	private void clear()
	{
		this._current_types.Clear();
		this._current_objects.Clear();
		this._last_data.Clear();
	}

	// Token: 0x06003756 RID: 14166 RVA: 0x0018FD7F File Offset: 0x0018DF7F
	private void OnEnable()
	{
		this.load();
	}

	// Token: 0x06003757 RID: 14167 RVA: 0x0018FD87 File Offset: 0x0018DF87
	private void OnDisable()
	{
		this.clear();
	}

	// Token: 0x040028E5 RID: 10469
	public static MinMax min_max;

	// Token: 0x040028E6 RID: 10470
	public GraphChart chart;

	// Token: 0x040028E7 RID: 10471
	[SerializeField]
	private MetaType _meta_type = MetaType.City;

	// Token: 0x040028E8 RID: 10472
	private GraphCategoriesContainer _container_graph_categories;

	// Token: 0x040028E9 RID: 10473
	private GraphTimeScaleContainer _container_time_scale;

	// Token: 0x040028EA RID: 10474
	public bool clear_on_enable;

	// Token: 0x040028EB RID: 10475
	public bool multi_chart;

	// Token: 0x040028EC RID: 10476
	private VerticalAxis _vertical_axis;

	// Token: 0x040028ED RID: 10477
	private HorizontalAxis _horizontal_axis;

	// Token: 0x040028EE RID: 10478
	private List<HistoryDataAsset> _list_categories = new List<HistoryDataAsset>();

	// Token: 0x040028EF RID: 10479
	private Dictionary<string, bool> _category_enabled = new Dictionary<string, bool>();

	// Token: 0x040028F0 RID: 10480
	private Dictionary<string, MinMax> _min_max_categories = new Dictionary<string, MinMax>();

	// Token: 0x040028F1 RID: 10481
	private long _min_timestamp = long.MinValue;

	// Token: 0x040028F2 RID: 10482
	private long _max_timestamp = long.MaxValue;

	// Token: 0x040028F3 RID: 10483
	private HashSet<MetaType> _current_types = new HashSet<MetaType>();

	// Token: 0x040028F4 RID: 10484
	private List<NanoObject> _current_objects = new List<NanoObject>();

	// Token: 0x040028F5 RID: 10485
	private Dictionary<NanoObject, HistoryTable> _last_data = new Dictionary<NanoObject, HistoryTable>();

	// Token: 0x040028F6 RID: 10486
	private GraphTimeAsset _current_sample;

	// Token: 0x040028F7 RID: 10487
	private HistoryInterval _current_interval;

	// Token: 0x040028F8 RID: 10488
	private Dictionary<string, CategoryData> _current_datas = new Dictionary<string, CategoryData>();

	// Token: 0x040028F9 RID: 10489
	private bool _events_hooked;

	// Token: 0x040028FA RID: 10490
	private bool _loaded;

	// Token: 0x040028FB RID: 10491
	private bool _categories_loaded;

	// Token: 0x040028FC RID: 10492
	private long _last_timestamp = -1L;
}
