using System;

// Token: 0x0200070D RID: 1805
public class LanguageListComponent : ComponentListBase<LanguageListElement, Language, LanguageData, LanguageListComponent>
{
	// Token: 0x17000338 RID: 824
	// (get) Token: 0x060039BB RID: 14779 RVA: 0x0019AEF1 File Offset: 0x001990F1
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Language;
		}
	}

	// Token: 0x060039BC RID: 14780 RVA: 0x0019AEF4 File Offset: 0x001990F4
	protected override void setupSortingTabs()
	{
		base.genericMetaSortByAge(new Comparison<Language>(base.sortByAge));
		base.genericMetaSortByRenown(new Comparison<Language>(base.sortByRenown));
		base.genericMetaSortByPopulation(new Comparison<Language>(ComponentListBase<LanguageListElement, Language, LanguageData, LanguageListComponent>.sortByPopulation));
		base.genericMetaSortByKills(new Comparison<Language>(ComponentListBase<LanguageListElement, Language, LanguageData, LanguageListComponent>.sortByKills));
		base.genericMetaSortByDeath(new Comparison<Language>(ComponentListBase<LanguageListElement, Language, LanguageData, LanguageListComponent>.sortByDeaths));
		this.sorting_tab.tryAddButton("ui/Icons/iconVillages", "sort_by_villages", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Language>(LanguageListComponent.sortByVillages);
		});
	}

	// Token: 0x060039BD RID: 14781 RVA: 0x0019AF8C File Offset: 0x0019918C
	public static int sortByVillages(Language pObject1, Language pObject2)
	{
		return pObject2.cities.Count.CompareTo(pObject1.cities.Count);
	}
}
