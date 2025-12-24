using System;

// Token: 0x02000691 RID: 1681
public class FamilyListComponent : ComponentListSapient<FamilyListElement, Family, FamilyData, FamilyListComponent>
{
	// Token: 0x17000302 RID: 770
	// (get) Token: 0x060035CB RID: 13771 RVA: 0x001899D2 File Offset: 0x00187BD2
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Family;
		}
	}

	// Token: 0x17000303 RID: 771
	// (get) Token: 0x060035CC RID: 13772 RVA: 0x001899D5 File Offset: 0x00187BD5
	protected override bool change_asset_sort_order
	{
		get
		{
			return this._families_window == null;
		}
	}

	// Token: 0x060035CD RID: 13773 RVA: 0x001899E0 File Offset: 0x00187BE0
	protected override void create()
	{
		base.create();
		this._families_window = base.GetComponentInParent<IMetaWithFamiliesWindow>();
		if (this._families_window != null)
		{
			this.get_objects_delegate = ((FamilyListComponent _) => this._families_window.getFamilies());
		}
	}

	// Token: 0x060035CE RID: 13774 RVA: 0x00189A10 File Offset: 0x00187C10
	protected override void setupSortingTabs()
	{
		base.genericMetaSortByAge(new Comparison<Family>(base.sortByAge));
		base.genericMetaSortByRenown(new Comparison<Family>(base.sortByRenown));
		base.genericMetaSortByPopulation(new Comparison<Family>(ComponentListBase<FamilyListElement, Family, FamilyData, FamilyListComponent>.sortByPopulation));
		base.genericMetaSortByKills(new Comparison<Family>(ComponentListBase<FamilyListElement, Family, FamilyData, FamilyListComponent>.sortByKills));
		base.genericMetaSortByDeath(new Comparison<Family>(ComponentListBase<FamilyListElement, Family, FamilyData, FamilyListComponent>.sortByDeaths));
		this.sorting_tab.tryAddButton("ui/Icons/iconAdults", "sort_by_adults", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Family>(FamilyListComponent.sortByAdults);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconChildren", "sort_by_children", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Family>(FamilyListComponent.sortByChildren);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconHelixDNA", "sort_by_species", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Family>(FamilyListComponent.sortBySpecies);
		});
	}

	// Token: 0x060035CF RID: 13775 RVA: 0x00189B04 File Offset: 0x00187D04
	public override bool isEmpty()
	{
		if (this._families_window != null)
		{
			return !this._families_window.hasFamilies();
		}
		return base.isEmpty();
	}

	// Token: 0x060035D0 RID: 13776 RVA: 0x00189B24 File Offset: 0x00187D24
	public static int sortByAdults(Family pObject1, Family pObject2)
	{
		return pObject2.countAdults().CompareTo(pObject1.countAdults());
	}

	// Token: 0x060035D1 RID: 13777 RVA: 0x00189B48 File Offset: 0x00187D48
	public static int sortByChildren(Family pObject1, Family pObject2)
	{
		return pObject2.countChildren().CompareTo(pObject1.countChildren());
	}

	// Token: 0x060035D2 RID: 13778 RVA: 0x00189B6C File Offset: 0x00187D6C
	public static int sortBySpecies(Family pObject1, Family pObject2)
	{
		return pObject2.getActorAsset().GetHashCode().CompareTo(pObject1.getActorAsset().GetHashCode());
	}

	// Token: 0x040027FA RID: 10234
	private IMetaWithFamiliesWindow _families_window;
}
