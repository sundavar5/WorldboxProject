using System;

// Token: 0x0200073D RID: 1853
public class ReligionListComponent : ComponentListBase<ReligionListElement, Religion, ReligionData, ReligionListComponent>
{
	// Token: 0x17000353 RID: 851
	// (get) Token: 0x06003ADF RID: 15071 RVA: 0x0019F73D File Offset: 0x0019D93D
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Religion;
		}
	}

	// Token: 0x06003AE0 RID: 15072 RVA: 0x0019F740 File Offset: 0x0019D940
	protected override void setupSortingTabs()
	{
		base.genericMetaSortByAge(new Comparison<Religion>(base.sortByAge));
		base.genericMetaSortByRenown(new Comparison<Religion>(base.sortByRenown));
		base.genericMetaSortByPopulation(new Comparison<Religion>(ComponentListBase<ReligionListElement, Religion, ReligionData, ReligionListComponent>.sortByPopulation));
		base.genericMetaSortByKills(new Comparison<Religion>(ComponentListBase<ReligionListElement, Religion, ReligionData, ReligionListComponent>.sortByKills));
		base.genericMetaSortByDeath(new Comparison<Religion>(ComponentListBase<ReligionListElement, Religion, ReligionData, ReligionListComponent>.sortByDeaths));
		this.sorting_tab.tryAddButton("ui/Icons/iconVillages", "sort_by_villages", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Religion>(ReligionListComponent.sortByVillages);
		});
	}

	// Token: 0x06003AE1 RID: 15073 RVA: 0x0019F7D8 File Offset: 0x0019D9D8
	public static int sortByVillages(Religion p1, Religion p2)
	{
		return p2.cities.Count.CompareTo(p1.cities.Count);
	}
}
