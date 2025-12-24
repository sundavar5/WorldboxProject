using System;

// Token: 0x0200014D RID: 333
public class PlotCategoryLibrary : BaseCategoryLibrary<PlotCategoryAsset>
{
	// Token: 0x06000A0D RID: 2573 RVA: 0x00093068 File Offset: 0x00091268
	public override void init()
	{
		base.init();
		this.add(new PlotCategoryAsset
		{
			id = "diplomacy",
			name = "plot_group_diplomacy",
			color = "#5EFFFF",
			show_counter = false,
			plot_retry_action = new PlotRetryAction(PlotCategoryLibrary.diplomacyRetryAction)
		});
		this.add(new PlotCategoryAsset
		{
			id = "rites_wrathful",
			name = "plot_group_rites_wrathful",
			color = "#FF6145",
			show_counter = false,
			plot_retry_action = new PlotRetryAction(PlotCategoryLibrary.ritesRetryAction)
		});
		this.add(new PlotCategoryAsset
		{
			id = "rites_summoning",
			name = "plot_group_rites_summoning",
			color = "#BC42FF",
			show_counter = false,
			plot_retry_action = new PlotRetryAction(PlotCategoryLibrary.ritesRetryAction)
		});
		this.add(new PlotCategoryAsset
		{
			id = "rites_merciful",
			name = "plot_group_rites_merciful",
			color = "#89FF56",
			show_counter = false,
			plot_retry_action = new PlotRetryAction(PlotCategoryLibrary.ritesRetryAction)
		});
		this.add(new PlotCategoryAsset
		{
			id = "culture",
			name = "plot_group_culture",
			color = "#FFDA23",
			show_counter = false,
			plot_retry_action = new PlotRetryAction(PlotCategoryLibrary.culturePlotsRetryAction)
		});
		this.add(new PlotCategoryAsset
		{
			id = "language",
			name = "plot_group_language",
			color = "#A3AFFF",
			show_counter = false,
			plot_retry_action = new PlotRetryAction(PlotCategoryLibrary.culturePlotsRetryAction)
		});
		this.add(new PlotCategoryAsset
		{
			id = "religion",
			name = "plot_group_religion",
			color = "#BAF0F4",
			show_counter = false,
			plot_retry_action = new PlotRetryAction(PlotCategoryLibrary.culturePlotsRetryAction)
		});
		this.add(new PlotCategoryAsset
		{
			id = "rites_various",
			name = "plot_group_rites_various",
			color = "#d86569",
			show_counter = false,
			plot_retry_action = new PlotRetryAction(PlotCategoryLibrary.ritesRetryAction)
		});
		this.add(new PlotCategoryAsset
		{
			id = "plots_others",
			name = "plot_group_others",
			color = "#D8D8D8",
			show_counter = false,
			plot_retry_action = new PlotRetryAction(PlotCategoryLibrary.ritesRetryAction)
		});
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x000932F4 File Offset: 0x000914F4
	public static bool diplomacyRetryAction()
	{
		return World.world.cities.isLocked() || World.world.kingdoms.isLocked() || World.world.alliances.isLocked() || World.world.wars.isLocked() || World.world.clans.isLocked();
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x00093364 File Offset: 0x00091564
	public static bool ritesRetryAction()
	{
		return World.world.cities.isLocked() || World.world.kingdoms.isLocked() || World.world.clans.isLocked() || World.world.religions.isLocked() || World.world.subspecies.isLocked();
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x000933D1 File Offset: 0x000915D1
	public static bool culturePlotsRetryAction()
	{
		return World.world.cultures.isLocked() || World.world.religions.isLocked() || World.world.languages.isLocked();
	}
}
