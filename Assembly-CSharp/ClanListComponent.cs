using System;

// Token: 0x02000666 RID: 1638
public class ClanListComponent : ComponentListBase<ClanListElement, Clan, ClanData, ClanListComponent>
{
	// Token: 0x170002E0 RID: 736
	// (get) Token: 0x0600350A RID: 13578 RVA: 0x00187BDA File Offset: 0x00185DDA
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Clan;
		}
	}

	// Token: 0x0600350B RID: 13579 RVA: 0x00187BE0 File Offset: 0x00185DE0
	protected override void setupSortingTabs()
	{
		base.genericMetaSortByAge(new Comparison<Clan>(base.sortByAge));
		base.genericMetaSortByRenown(new Comparison<Clan>(base.sortByRenown));
		base.genericMetaSortByPopulation(new Comparison<Clan>(ComponentListBase<ClanListElement, Clan, ClanData, ClanListComponent>.sortByPopulation));
		base.genericMetaSortByKills(new Comparison<Clan>(ComponentListBase<ClanListElement, Clan, ClanData, ClanListComponent>.sortByKills));
		base.genericMetaSortByDeath(new Comparison<Clan>(ComponentListBase<ClanListElement, Clan, ClanData, ClanListComponent>.sortByDeaths));
		this.sorting_tab.tryAddButton("ui/Icons/iconKingdom", "sort_by_kingdom", new SortButtonAction(this.show), delegate
		{
			this.current_sort = new Comparison<Clan>(ClanListComponent.sortByKingdom);
		});
	}

	// Token: 0x0600350C RID: 13580 RVA: 0x00187C78 File Offset: 0x00185E78
	private static int sortByKingdom(Clan p1, Clan p2)
	{
		Actor tChief = p1.getChief();
		return p2.getChief().kingdom.CompareTo(tChief.kingdom);
	}
}
