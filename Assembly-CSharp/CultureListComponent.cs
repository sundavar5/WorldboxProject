using System;

// Token: 0x02000674 RID: 1652
public class CultureListComponent : ComponentListBase<CultureListElement, Culture, CultureData, CultureListComponent>
{
	// Token: 0x170002EE RID: 750
	// (get) Token: 0x06003548 RID: 13640 RVA: 0x001884EA File Offset: 0x001866EA
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Culture;
		}
	}

	// Token: 0x06003549 RID: 13641 RVA: 0x001884F0 File Offset: 0x001866F0
	protected override void setupSortingTabs()
	{
		base.genericMetaSortByAge(new Comparison<Culture>(base.sortByAge));
		base.genericMetaSortByRenown(new Comparison<Culture>(base.sortByRenown));
		base.genericMetaSortByPopulation(new Comparison<Culture>(ComponentListBase<CultureListElement, Culture, CultureData, CultureListComponent>.sortByPopulation));
		base.genericMetaSortByKills(new Comparison<Culture>(ComponentListBase<CultureListElement, Culture, CultureData, CultureListComponent>.sortByKills));
		base.genericMetaSortByDeath(new Comparison<Culture>(ComponentListBase<CultureListElement, Culture, CultureData, CultureListComponent>.sortByDeaths));
		this.sorting_tab.tryAddButton("ui/Icons/iconVillages", "sort_by_villages", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Culture>(CultureListComponent.sortByVillages);
		});
	}

	// Token: 0x0600354A RID: 13642 RVA: 0x00188588 File Offset: 0x00186788
	public static int sortByVillages(Culture pCulture1, Culture pCulture2)
	{
		return pCulture2.countCities().CompareTo(pCulture1.countCities());
	}
}
