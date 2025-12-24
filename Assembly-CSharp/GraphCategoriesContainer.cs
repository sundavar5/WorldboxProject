using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020006B6 RID: 1718
public class GraphCategoriesContainer : MonoBehaviour
{
	// Token: 0x060036E3 RID: 14051 RVA: 0x0018D3B4 File Offset: 0x0018B5B4
	private void init()
	{
		if (this._is_initialized)
		{
			return;
		}
		this._is_initialized = true;
		ButtonGraphCategory[] componentsInChildren = base.GetComponentsInChildren<ButtonGraphCategory>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Object.Destroy(componentsInChildren[i].gameObject);
		}
		this._prefab_button = Resources.Load<ButtonGraphCategory>("ui/graphs/GraphCategoryButton");
	}

	// Token: 0x060036E4 RID: 14052 RVA: 0x0018D404 File Offset: 0x0018B604
	public void apply()
	{
		this.init();
		List<HistoryDataAsset> tCategoriesList = this.graph_controller.getCategories();
		if (this._last_category_group == this.category_group && this._current_list.Count > 0 && this._current_list.Count == tCategoriesList.Count && this._current_list.All(new Func<HistoryDataAsset, bool>(tCategoriesList.Contains)) && tCategoriesList.All(new Func<HistoryDataAsset, bool>(this._current_list.Contains)))
		{
			foreach (ButtonGraphCategory tButton in this._category_buttons.Values)
			{
				this.graph_controller.setCategoryEnabled(tButton.gameObject.name, tButton.is_on, false);
			}
			return;
		}
		foreach (ButtonGraphCategory buttonGraphCategory in this._category_buttons.Values)
		{
			buttonGraphCategory.gameObject.SetActive(false);
		}
		GraphCategoryGroup tCategories = GraphCategoryGroup.None;
		this._current_list = new List<HistoryDataAsset>(tCategoriesList);
		foreach (HistoryDataAsset tCategory in this._current_list)
		{
			tCategories |= tCategory.category_group;
			if (tCategory.category_group.HasFlag(this.category_group))
			{
				ButtonGraphCategory tCategoryButton;
				if (!this._category_buttons.TryGetValue(tCategory.id, out tCategoryButton))
				{
					tCategoryButton = Object.Instantiate<ButtonGraphCategory>(this._prefab_button, base.transform);
					tCategoryButton.gameObject.name = tCategory.id;
					tCategoryButton.transform.SetParent(base.transform);
					tCategoryButton.init();
					tCategoryButton.setAsset(tCategory);
					tCategoryButton.is_on = this.graph_controller.isCategoryEnabled(tCategory.id);
					this._category_buttons.Add(tCategory.id, tCategoryButton);
				}
				tCategoryButton.gameObject.SetActive(true);
			}
		}
		this._last_category_group = this.category_group;
		this.showCategoryGroups(tCategories);
	}

	// Token: 0x060036E5 RID: 14053 RVA: 0x0018D65C File Offset: 0x0018B85C
	private void showCategoryGroups(GraphCategoryGroup pGroups)
	{
		this._category_groups.gameObject.SetActive(pGroups.Count<GraphCategoryGroup>() > 1);
		if (this._last_category_groups != pGroups)
		{
			this._category_groups.clearButtons();
			if (pGroups.HasFlag(GraphCategoryGroup.General))
			{
				this._category_groups.tryAddButton("ui/Icons/iconRenown", "tab_general_stats", new TabToggleAction(this.apply), delegate
				{
					this.category_group = GraphCategoryGroup.General;
				});
			}
			if (pGroups.HasFlag(GraphCategoryGroup.Noosphere))
			{
				this._category_groups.tryAddButton("ui/Icons/iconKnowledge", "tab_noosphere", new TabToggleAction(this.apply), delegate
				{
					this.category_group = GraphCategoryGroup.Noosphere;
				});
			}
			if (pGroups.HasFlag(GraphCategoryGroup.Deaths))
			{
				this._category_groups.tryAddButton("civ/map_mark_death", "tab_deaths", new TabToggleAction(this.apply), delegate
				{
					this.category_group = GraphCategoryGroup.Deaths;
				});
			}
			if (pGroups.HasFlag(GraphCategoryGroup.Biomes))
			{
				this._category_groups.tryAddButton("ui/Icons/iconSeedClover", "tab_biomes", new TabToggleAction(this.apply), delegate
				{
					this.category_group = GraphCategoryGroup.Biomes;
				});
			}
			if (pGroups.HasFlag(GraphCategoryGroup.Tiles))
			{
				this._category_groups.tryAddButton("ui/Icons/iconZones", "tab_tiles", new TabToggleAction(this.apply), delegate
				{
					this.category_group = GraphCategoryGroup.Tiles;
				});
			}
			this._last_category_groups = pGroups;
			this._category_groups.enableFirst();
		}
	}

	// Token: 0x060036E6 RID: 14054 RVA: 0x0018D7EC File Offset: 0x0018B9EC
	public void setCategoryEnabled(string pId, bool pEnabled)
	{
		if (this.graph_controller.multi_chart)
		{
			foreach (ButtonGraphCategory buttonGraphCategory in this._category_buttons.Values)
			{
				buttonGraphCategory.is_on = (buttonGraphCategory.gameObject.name == pId);
			}
			this.graph_controller.disableAllCategories(pId);
		}
		this.graph_controller.setCategoryEnabled(pId, pEnabled, false);
		this.graph_controller.adjustCharts();
	}

	// Token: 0x040028AA RID: 10410
	public GraphController graph_controller;

	// Token: 0x040028AB RID: 10411
	public GraphCategoryGroup category_group = GraphCategoryGroup.General;

	// Token: 0x040028AC RID: 10412
	private GraphCategoryGroup _last_category_group;

	// Token: 0x040028AD RID: 10413
	private GraphCategoryGroup _last_category_groups;

	// Token: 0x040028AE RID: 10414
	private List<HistoryDataAsset> _current_list = new List<HistoryDataAsset>();

	// Token: 0x040028AF RID: 10415
	private Dictionary<string, ButtonGraphCategory> _category_buttons = new Dictionary<string, ButtonGraphCategory>();

	// Token: 0x040028B0 RID: 10416
	private ButtonGraphCategory _prefab_button;

	// Token: 0x040028B1 RID: 10417
	private bool _is_initialized;

	// Token: 0x040028B2 RID: 10418
	[SerializeField]
	private TabTogglesGroup _category_groups;
}
