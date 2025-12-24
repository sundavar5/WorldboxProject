using System;
using System.Collections.Generic;

// Token: 0x0200067E RID: 1662
public class CultureWindow : WindowMetaGeneric<Culture, CultureData>, ITraitWindow<CultureTrait, CultureTraitButton>, IAugmentationsWindow<ITraitsEditor<CultureTrait>>, IBooksWindow
{
	// Token: 0x170002F6 RID: 758
	// (get) Token: 0x06003570 RID: 13680 RVA: 0x0018890E File Offset: 0x00186B0E
	public override MetaType meta_type
	{
		get
		{
			return MetaType.Culture;
		}
	}

	// Token: 0x170002F7 RID: 759
	// (get) Token: 0x06003571 RID: 13681 RVA: 0x00188911 File Offset: 0x00186B11
	protected override Culture meta_object
	{
		get
		{
			return SelectedMetas.selected_culture;
		}
	}

	// Token: 0x06003572 RID: 13682 RVA: 0x00188918 File Offset: 0x00186B18
	public void testDebugNewBook()
	{
		this.meta_object.testDebugNewBook();
		this.startShowingWindow();
		this.scroll_window.tabs.showTab(this.scroll_window.tabs.getActiveTab());
	}

	// Token: 0x06003573 RID: 13683 RVA: 0x0018894B File Offset: 0x00186B4B
	public List<long> getBooks()
	{
		return this.meta_object.books.getList();
	}

	// Token: 0x06003574 RID: 13684 RVA: 0x0018895D File Offset: 0x00186B5D
	protected override void showTopPartInformation()
	{
		base.showTopPartInformation();
		Culture meta_object = this.meta_object;
	}

	// Token: 0x06003575 RID: 13685 RVA: 0x0018896C File Offset: 0x00186B6C
	internal override void showStatsRows()
	{
		Culture tCulture = this.meta_object;
		base.tryShowPastNames();
		base.showStatRow("founded", tCulture.getFoundedDate(), MetaType.None, -1L, null, null, null);
		base.tryToShowActor("founder", tCulture.data.creator_id, tCulture.data.creator_name, null, "actor_traits/iconStupid");
		base.tryToShowMetaClan("founder_clan", tCulture.data.creator_clan_id, tCulture.data.creator_clan_name, null);
		base.tryToShowMetaKingdom("origin", tCulture.data.creator_kingdom_id, tCulture.data.creator_kingdom_name, null);
		base.tryToShowMetaCity("birthplace", tCulture.data.creator_city_id, tCulture.data.creator_city_name, null, "iconCity");
		base.tryToShowMetaSubspecies("founder_subspecies", tCulture.data.creator_subspecies_id, tCulture.data.creator_subspecies_name, null);
		base.tryToShowMetaSpecies("founder_species", tCulture.data.creator_species_id);
	}

	// Token: 0x06003576 RID: 13686 RVA: 0x00188A68 File Offset: 0x00186C68
	protected override bool onNameChange(string pInput)
	{
		if (!base.onNameChange(pInput))
		{
			return false;
		}
		long tCultureId = this.meta_object.getID();
		string tCultureName = this.meta_object.data.name;
		foreach (Book tBook in World.world.books)
		{
			if (!tBook.isRekt() && tBook.data.culture_id == tCultureId)
			{
				tBook.data.culture_name = tCultureName;
			}
		}
		return true;
	}

	// Token: 0x06003578 RID: 13688 RVA: 0x00188B08 File Offset: 0x00186D08
	T IAugmentationsWindow<ITraitsEditor<CultureTrait>>.GetComponentInChildren<T>(bool includeInactive)
	{
		return base.GetComponentInChildren<T>(includeInactive);
	}

	// Token: 0x040027E3 RID: 10211
	public StatBar experienceBar;
}
