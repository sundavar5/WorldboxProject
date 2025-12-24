using System;

// Token: 0x020007C8 RID: 1992
public class WarListComponent : ComponentListBase<WarListElement, War, WarData, WarListComponent>
{
	// Token: 0x170003A7 RID: 935
	// (get) Token: 0x06003ED5 RID: 16085 RVA: 0x001B3872 File Offset: 0x001B1A72
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.War;
		}
	}

	// Token: 0x06003ED6 RID: 16086 RVA: 0x001B3878 File Offset: 0x001B1A78
	protected override void setupSortingTabs()
	{
		SortButton tAgeButton = this.sorting_tab.tryAddButton("ui/Icons/iconAge", "sort_by_age", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<War>(WarListComponent.sortByAge);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconClock", "sort_by_duration", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<War>(WarListComponent.sortByDuration);
		});
		if (base.getCurrentFilter() != ListItemsFilter.OnlyAlive)
		{
			this.sorting_tab.tryAddButton("ui/Icons/iconDeadKingdom", "sort_by_ended", new SortButtonAction(this.show), delegate
			{
				this.current_sort = new Comparison<War>(WarListComponent.sortByEndedTime);
			});
		}
		this.sorting_tab.tryAddButton("ui/Icons/iconRenown", "sort_by_renown", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<War>(WarListComponent.sortByRenown);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconArmy", "sort_by_army", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<War>(WarListComponent.sortByArmy);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconKills", "sort_by_dead", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<War>(WarListComponent.sortByDead);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconPopulation", "sort_by_population", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<War>(WarListComponent.sortByPopulation);
		});
		if (tAgeButton != null)
		{
			tAgeButton.click();
			tAgeButton.click();
		}
	}

	// Token: 0x06003ED7 RID: 16087 RVA: 0x001B39EC File Offset: 0x001B1BEC
	public static int sortByRenown(War pWar1, War pWar2)
	{
		if (WarListComponent.sortByEnded(pWar1, pWar2) != 0)
		{
			return WarListComponent.sortByEnded(pWar1, pWar2);
		}
		return pWar2.getRenown().CompareTo(pWar1.getRenown());
	}

	// Token: 0x06003ED8 RID: 16088 RVA: 0x001B3A20 File Offset: 0x001B1C20
	public static int sortByDuration(War pWar1, War pWar2)
	{
		if (WarListComponent.sortByEnded(pWar1, pWar2) != 0)
		{
			return WarListComponent.sortByEnded(pWar1, pWar2);
		}
		return -pWar2.getDuration().CompareTo(pWar1.getDuration());
	}

	// Token: 0x06003ED9 RID: 16089 RVA: 0x001B3A54 File Offset: 0x001B1C54
	public static int sortByAge(War pWar1, War pWar2)
	{
		if (WarListComponent.sortByEnded(pWar1, pWar2) != 0)
		{
			return WarListComponent.sortByEnded(pWar1, pWar2);
		}
		return -pWar2.data.created_time.CompareTo(pWar1.data.created_time);
	}

	// Token: 0x06003EDA RID: 16090 RVA: 0x001B3A94 File Offset: 0x001B1C94
	public static int sortByArmy(War pWar1, War pWar2)
	{
		if (WarListComponent.sortByEnded(pWar1, pWar2) != 0)
		{
			return WarListComponent.sortByEnded(pWar1, pWar2);
		}
		return pWar1.countTotalArmy().CompareTo(pWar2.countTotalArmy());
	}

	// Token: 0x06003EDB RID: 16091 RVA: 0x001B3AC8 File Offset: 0x001B1CC8
	public static int sortByPopulation(War pWar1, War pWar2)
	{
		if (WarListComponent.sortByEnded(pWar1, pWar2) != 0)
		{
			return WarListComponent.sortByEnded(pWar1, pWar2);
		}
		return pWar1.countTotalPopulation().CompareTo(pWar2.countTotalPopulation());
	}

	// Token: 0x06003EDC RID: 16092 RVA: 0x001B3AFC File Offset: 0x001B1CFC
	public static int sortByEndedTime(War pWar1, War pWar2)
	{
		if (WarListComponent.sortByEnded(pWar1, pWar2) == 0)
		{
			return WarListComponent.sortByAge(pWar1, pWar2);
		}
		return pWar2.data.died_time.CompareTo(pWar1.data.died_time);
	}

	// Token: 0x06003EDD RID: 16093 RVA: 0x001B3B38 File Offset: 0x001B1D38
	public static int sortByDead(War pWar1, War pWar2)
	{
		if (WarListComponent.sortByEnded(pWar1, pWar2) != 0)
		{
			return WarListComponent.sortByEnded(pWar1, pWar2);
		}
		return pWar2.data.total_deaths.CompareTo(pWar1.getTotalDeaths());
	}

	// Token: 0x06003EDE RID: 16094 RVA: 0x001B3B70 File Offset: 0x001B1D70
	private static int sortByEnded(War pWar1, War pWar2)
	{
		return pWar1.hasEnded().CompareTo(pWar2.hasEnded());
	}
}
