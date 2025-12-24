using System;

// Token: 0x02000656 RID: 1622
public class CityListComponent : ComponentListBase<CityListElement, City, CityData, CityListComponent>
{
	// Token: 0x170002D6 RID: 726
	// (get) Token: 0x0600348E RID: 13454 RVA: 0x0018618A File Offset: 0x0018438A
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.City;
		}
	}

	// Token: 0x0600348F RID: 13455 RVA: 0x00186190 File Offset: 0x00184390
	protected override void setupSortingTabs()
	{
		base.genericMetaSortByAge(new Comparison<City>(base.sortByAge));
		base.genericMetaSortByRenown(new Comparison<City>(base.sortByRenown));
		base.genericMetaSortByPopulation(new Comparison<City>(ComponentListBase<CityListElement, City, CityData, CityListComponent>.sortByPopulation));
		this.sorting_tab.tryAddButton("ui/Icons/iconLoyalty", "sort_by_loyalty", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<City>(CityListComponent.sortByLoyalty);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconZones", "sort_by_area", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<City>(CityListComponent.sortByArea);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconArmy", "sort_by_army", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<City>(CityListComponent.sortByArmy);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconKingdom", "sort_by_kingdom", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<City>(CityListComponent.sortByKingdom);
		});
	}

	// Token: 0x06003490 RID: 13456 RVA: 0x0018628F File Offset: 0x0018448F
	private static int sortByKingdom(City p1, City p2)
	{
		return p2.kingdom.CompareTo(p1.kingdom);
	}

	// Token: 0x06003491 RID: 13457 RVA: 0x001862A4 File Offset: 0x001844A4
	private static int sortByArmy(City p1, City p2)
	{
		return p2.countWarriors().CompareTo(p1.countWarriors());
	}

	// Token: 0x06003492 RID: 13458 RVA: 0x001862C8 File Offset: 0x001844C8
	private static int sortByLoyalty(City p1, City p2)
	{
		return p2.getCachedLoyalty().CompareTo(p1.getCachedLoyalty());
	}

	// Token: 0x06003493 RID: 13459 RVA: 0x001862EC File Offset: 0x001844EC
	private static int sortByArea(City p1, City p2)
	{
		return p2.zones.Count.CompareTo(p1.zones.Count);
	}
}
