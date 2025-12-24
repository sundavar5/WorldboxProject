using System;

// Token: 0x02000627 RID: 1575
public class AllianceListComponent : ComponentListBase<AllianceListElement, Alliance, AllianceData, AllianceListComponent>
{
	// Token: 0x170002C5 RID: 709
	// (get) Token: 0x06003376 RID: 13174 RVA: 0x00183301 File Offset: 0x00181501
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Alliance;
		}
	}

	// Token: 0x06003377 RID: 13175 RVA: 0x00183308 File Offset: 0x00181508
	protected override void setupSortingTabs()
	{
		base.genericMetaSortByAge(new Comparison<Alliance>(base.sortByAge));
		base.genericMetaSortByRenown(new Comparison<Alliance>(base.sortByRenown));
		base.genericMetaSortByPopulation(new Comparison<Alliance>(ComponentListBase<AllianceListElement, Alliance, AllianceData, AllianceListComponent>.sortByPopulation));
		this.sorting_tab.tryAddButton("ui/Icons/iconArmy", "sort_by_army", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Alliance>(AllianceListComponent.sortByArmy);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconKingdomList", "sort_by_kingdoms", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Alliance>(AllianceListComponent.sortByKingdoms);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconVillages", "sort_by_villages", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Alliance>(AllianceListComponent.sortByVillages);
		});
	}

	// Token: 0x06003378 RID: 13176 RVA: 0x001833D8 File Offset: 0x001815D8
	public static int sortByArmy(Alliance pAlliance1, Alliance pAlliance2)
	{
		return pAlliance2.countWarriors().CompareTo(pAlliance1.countWarriors());
	}

	// Token: 0x06003379 RID: 13177 RVA: 0x001833FC File Offset: 0x001815FC
	public static int sortByKingdoms(Alliance pAlliance1, Alliance pAlliance2)
	{
		return pAlliance2.countKingdoms().CompareTo(pAlliance1.countKingdoms());
	}

	// Token: 0x0600337A RID: 13178 RVA: 0x00183420 File Offset: 0x00181620
	public static int sortByVillages(Alliance pAlliance1, Alliance pAlliance2)
	{
		return pAlliance2.countCities().CompareTo(pAlliance1.countCities());
	}
}
