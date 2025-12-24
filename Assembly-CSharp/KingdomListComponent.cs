using System;

// Token: 0x020006F2 RID: 1778
public class KingdomListComponent : ComponentListBase<KingdomListElement, Kingdom, KingdomData, KingdomListComponent>
{
	// Token: 0x1700032A RID: 810
	// (get) Token: 0x0600390B RID: 14603 RVA: 0x00197962 File Offset: 0x00195B62
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Kingdom;
		}
	}

	// Token: 0x0600390C RID: 14604 RVA: 0x00197968 File Offset: 0x00195B68
	protected override void setupSortingTabs()
	{
		base.genericMetaSortByAge(new Comparison<Kingdom>(base.sortByAge));
		base.genericMetaSortByRenown(new Comparison<Kingdom>(base.sortByRenown));
		base.genericMetaSortByPopulation(new Comparison<Kingdom>(ComponentListBase<KingdomListElement, Kingdom, KingdomData, KingdomListComponent>.sortByPopulation));
		this.sorting_tab.tryAddButton("ui/Icons/iconArmy", "sort_by_army", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Kingdom>(KingdomListComponent.sortByArmy);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconChildren", "sort_by_children", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Kingdom>(KingdomListComponent.sortByChildren);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconVillages", "sort_by_villages", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Kingdom>(KingdomListComponent.sortByCities);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconZones", "sort_by_area", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Kingdom>(KingdomListComponent.sortByArea);
		});
	}

	// Token: 0x0600390D RID: 14605 RVA: 0x00197A68 File Offset: 0x00195C68
	private static int sortByArea(Kingdom p1, Kingdom p2)
	{
		return p2.countZones().CompareTo(p1.countZones());
	}

	// Token: 0x0600390E RID: 14606 RVA: 0x00197A8C File Offset: 0x00195C8C
	public static int sortByArmy(Kingdom p1, Kingdom p2)
	{
		return p2.countTotalWarriors().CompareTo(p1.countTotalWarriors());
	}

	// Token: 0x0600390F RID: 14607 RVA: 0x00197AB0 File Offset: 0x00195CB0
	private static int sortByChildren(Kingdom p1, Kingdom p2)
	{
		return p2.countChildren().CompareTo(p1.countChildren());
	}

	// Token: 0x06003910 RID: 14608 RVA: 0x00197AD4 File Offset: 0x00195CD4
	private static int sortByCities(Kingdom p1, Kingdom p2)
	{
		return p2.countCities().CompareTo(p1.countCities());
	}
}
