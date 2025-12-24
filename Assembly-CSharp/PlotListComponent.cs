using System;

// Token: 0x02000728 RID: 1832
public class PlotListComponent : ComponentListBase<PlotListElement, Plot, PlotData, PlotListComponent>
{
	// Token: 0x17000348 RID: 840
	// (get) Token: 0x06003A62 RID: 14946 RVA: 0x0019D959 File Offset: 0x0019BB59
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Plot;
		}
	}

	// Token: 0x06003A63 RID: 14947 RVA: 0x0019D960 File Offset: 0x0019BB60
	protected override void setupSortingTabs()
	{
		this.sorting_tab.tryAddButton("ui/Icons/iconAge", "sort_by_age", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Plot>(PlotListComponent.sortByAge);
		});
		this.sorting_tab.tryAddButton("ui/Icons/iconPopulation", "sort_by_members", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Plot>(PlotListComponent.sortBySupporters);
		});
	}

	// Token: 0x06003A64 RID: 14948 RVA: 0x0019D9CC File Offset: 0x0019BBCC
	public static int sortByAge(Plot pPlot1, Plot pPlot2)
	{
		return -pPlot2.data.created_time.CompareTo(pPlot1.data.created_time);
	}

	// Token: 0x06003A65 RID: 14949 RVA: 0x0019D9F8 File Offset: 0x0019BBF8
	public static int sortBySupporters(Plot pPlot1, Plot pPlot2)
	{
		return pPlot2.units.Count.CompareTo(pPlot1.units.Count);
	}
}
