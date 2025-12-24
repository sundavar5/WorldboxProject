using System;
using System.Collections.Generic;

// Token: 0x02000556 RID: 1366
public class BuildingDebugAssetsComponent : BaseDebugAssetsComponent<BuildingAsset, BuildingDebugAssetElement, BuildingAssetElementPlace>
{
	// Token: 0x06002C8B RID: 11403 RVA: 0x0015DCB5 File Offset: 0x0015BEB5
	protected override List<BuildingAsset> getAssetsList()
	{
		return AssetManager.buildings.list;
	}

	// Token: 0x06002C8C RID: 11404 RVA: 0x0015DCC4 File Offset: 0x0015BEC4
	protected override void init()
	{
		this.sorting_tab.addButton("ui/Icons/iconHealth", "sort_by_health", new SortButtonAction(base.setDataResorted), delegate
		{
			this.list_assets_sorted = this.list_assets_sorting;
			this.list_assets_sorted.Sort(new Comparison<BuildingAsset>(this.sortByHealth));
			base.checkReverseSort();
		});
		this.sorting_tab.addButton("ui/Icons/iconDamage", "sort_by_damage", new SortButtonAction(base.setDataResorted), delegate
		{
			this.list_assets_sorted = this.list_assets_sorting;
			this.list_assets_sorted.Sort(new Comparison<BuildingAsset>(this.sortByDamage));
			base.checkReverseSort();
		});
		this.sorting_tab.addButton("ui/Icons/iconPopulationAttackers", "sort_by_targets", new SortButtonAction(base.setDataResorted), delegate
		{
			this.list_assets_sorted = this.list_assets_sorting;
			this.list_assets_sorted.Sort(new Comparison<BuildingAsset>(this.sortByTargets));
			base.checkReverseSort();
		});
		this.sorting_tab.addButton("effects/circle132", "sort_by_area_of_effect", new SortButtonAction(base.setDataResorted), delegate
		{
			this.list_assets_sorted = this.list_assets_sorting;
			this.list_assets_sorted.Sort(new Comparison<BuildingAsset>(this.sortByAreaOfEffect));
			base.checkReverseSort();
		});
		base.init();
	}

	// Token: 0x06002C8D RID: 11405 RVA: 0x0015DD90 File Offset: 0x0015BF90
	private int sortByHealth(BuildingAsset pObject1, BuildingAsset pObject2)
	{
		return -pObject1.base_stats["health"].CompareTo(pObject2.base_stats["health"]);
	}

	// Token: 0x06002C8E RID: 11406 RVA: 0x0015DDC8 File Offset: 0x0015BFC8
	private int sortByDamage(BuildingAsset pObject1, BuildingAsset pObject2)
	{
		return -pObject1.base_stats["damage"].CompareTo(pObject2.base_stats["damage"]);
	}

	// Token: 0x06002C8F RID: 11407 RVA: 0x0015DE00 File Offset: 0x0015C000
	private int sortByTargets(BuildingAsset pObject1, BuildingAsset pObject2)
	{
		return -pObject1.base_stats["targets"].CompareTo(pObject2.base_stats["targets"]);
	}

	// Token: 0x06002C90 RID: 11408 RVA: 0x0015DE38 File Offset: 0x0015C038
	private int sortByAreaOfEffect(BuildingAsset pObject1, BuildingAsset pObject2)
	{
		return -pObject1.base_stats["area_of_effect"].CompareTo(pObject2.base_stats["area_of_effect"]);
	}

	// Token: 0x06002C91 RID: 11409 RVA: 0x0015DE70 File Offset: 0x0015C070
	protected override List<BuildingAsset> getListCivsSort()
	{
		bool tShow = this.sorting_tab.getCurrentButton().getState() == SortButtonState.Up;
		List<BuildingAsset> tResult = new List<BuildingAsset>();
		foreach (BuildingAsset tAsset in this.getAssetsList())
		{
			bool tStringEmpty = string.IsNullOrEmpty(tAsset.civ_kingdom);
			if ((!tStringEmpty || !tShow) && (tStringEmpty || tShow))
			{
				tResult.Add(tAsset);
			}
		}
		return tResult;
	}
}
