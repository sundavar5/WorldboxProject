using System;

// Token: 0x02000769 RID: 1897
public class SubspeciesListComponent : ComponentListSapient<SubspeciesListElement, Subspecies, SubspeciesData, SubspeciesListComponent>
{
	// Token: 0x17000385 RID: 901
	// (get) Token: 0x06003C33 RID: 15411 RVA: 0x001A306C File Offset: 0x001A126C
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Subspecies;
		}
	}

	// Token: 0x06003C34 RID: 15412 RVA: 0x001A3070 File Offset: 0x001A1270
	protected override void setupSortingTabs()
	{
		base.genericMetaSortByAge(new Comparison<Subspecies>(base.sortByAge));
		base.genericMetaSortByRenown(new Comparison<Subspecies>(base.sortByRenown));
		base.genericMetaSortByPopulation(new Comparison<Subspecies>(ComponentListBase<SubspeciesListElement, Subspecies, SubspeciesData, SubspeciesListComponent>.sortByPopulation));
		base.genericMetaSortByKills(new Comparison<Subspecies>(ComponentListBase<SubspeciesListElement, Subspecies, SubspeciesData, SubspeciesListComponent>.sortByKills));
		base.genericMetaSortByDeath(new Comparison<Subspecies>(ComponentListBase<SubspeciesListElement, Subspecies, SubspeciesData, SubspeciesListComponent>.sortByDeaths));
		this.sorting_tab.tryAddButton("ui/Icons/iconChildren", "sort_by_children", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Subspecies>(SubspeciesListComponent.sortByChildren);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconHelixDNA", "sort_by_species", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Subspecies>(SubspeciesListComponent.sortBySpecies);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconFamily", "sort_by_families", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Subspecies>(SubspeciesListComponent.sortByFamilies);
		});
	}

	// Token: 0x06003C35 RID: 15413 RVA: 0x001A3164 File Offset: 0x001A1364
	public static int sortByChildren(Subspecies pObject1, Subspecies pObject2)
	{
		return pObject2.countChildren().CompareTo(pObject1.countChildren());
	}

	// Token: 0x06003C36 RID: 15414 RVA: 0x001A3188 File Offset: 0x001A1388
	public static int sortBySpecies(Subspecies pObject1, Subspecies pObject2)
	{
		return pObject2.getActorAsset().GetHashCode().CompareTo(pObject1.getActorAsset().GetHashCode());
	}

	// Token: 0x06003C37 RID: 15415 RVA: 0x001A31B4 File Offset: 0x001A13B4
	public static int sortByDead(Subspecies pObject1, Subspecies pObject2)
	{
		return pObject2.data.total_deaths.CompareTo(pObject1.data.total_deaths);
	}

	// Token: 0x06003C38 RID: 15416 RVA: 0x001A31E0 File Offset: 0x001A13E0
	public static int sortByFamilies(Subspecies pObject1, Subspecies pObject2)
	{
		return pObject2.countCurrentFamilies().CompareTo(pObject1.countCurrentFamilies());
	}
}
