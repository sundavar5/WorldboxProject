using System;

// Token: 0x02000632 RID: 1586
public class ArmyListComponent : ComponentListBase<ArmyListElement, Army, ArmyData, ArmyListComponent>
{
	// Token: 0x170002CB RID: 715
	// (get) Token: 0x060033B2 RID: 13234 RVA: 0x00183BE5 File Offset: 0x00181DE5
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Army;
		}
	}

	// Token: 0x060033B3 RID: 13235 RVA: 0x00183BEC File Offset: 0x00181DEC
	protected override void setupSortingTabs()
	{
		base.genericMetaSortByAge(new Comparison<Army>(base.sortByAge));
		base.genericMetaSortByRenown(new Comparison<Army>(base.sortByRenown));
		base.genericMetaSortByPopulation(new Comparison<Army>(ComponentListBase<ArmyListElement, Army, ArmyData, ArmyListComponent>.sortByPopulation));
		base.genericMetaSortByKills(new Comparison<Army>(ComponentListBase<ArmyListElement, Army, ArmyData, ArmyListComponent>.sortByKills));
		base.genericMetaSortByDeath(new Comparison<Army>(ComponentListBase<ArmyListElement, Army, ArmyData, ArmyListComponent>.sortByDeaths));
		this.sorting_tab.tryAddButton("ui/Icons/iconKingdom", "sort_by_kingdom", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Army>(ArmyListComponent.sortByKingdom);
		});
	}

	// Token: 0x060033B4 RID: 13236 RVA: 0x00183C82 File Offset: 0x00181E82
	private static int sortByKingdom(Army p1, Army p2)
	{
		return p2.getKingdom().CompareTo(p1.getKingdom());
	}
}
