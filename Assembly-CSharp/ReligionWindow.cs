using System;
using System.Collections.Generic;

// Token: 0x02000746 RID: 1862
public class ReligionWindow : WindowMetaGeneric<Religion, ReligionData>, ITraitWindow<ReligionTrait, ReligionTraitButton>, IAugmentationsWindow<ITraitsEditor<ReligionTrait>>, IBooksWindow
{
	// Token: 0x1700035A RID: 858
	// (get) Token: 0x06003AFF RID: 15103 RVA: 0x0019FA10 File Offset: 0x0019DC10
	public override MetaType meta_type
	{
		get
		{
			return MetaType.Religion;
		}
	}

	// Token: 0x1700035B RID: 859
	// (get) Token: 0x06003B00 RID: 15104 RVA: 0x0019FA13 File Offset: 0x0019DC13
	protected override Religion meta_object
	{
		get
		{
			return SelectedMetas.selected_religion;
		}
	}

	// Token: 0x06003B01 RID: 15105 RVA: 0x0019FA1A File Offset: 0x0019DC1A
	public List<long> getBooks()
	{
		return this.meta_object.books.getList();
	}

	// Token: 0x06003B02 RID: 15106 RVA: 0x0019FA2C File Offset: 0x0019DC2C
	protected override void showTopPartInformation()
	{
		base.showTopPartInformation();
		Religion meta_object = this.meta_object;
		AchievementLibrary.not_just_a_cult.checkBySignal(this.meta_object);
	}

	// Token: 0x06003B03 RID: 15107 RVA: 0x0019FA4C File Offset: 0x0019DC4C
	internal override void showStatsRows()
	{
		Religion tReligion = this.meta_object;
		base.tryShowPastNames();
		base.showStatRow("founded", tReligion.getFoundedDate(), MetaType.None, -1L, "iconAge", null, null);
		base.tryToShowActor("founder", tReligion.data.creator_id, tReligion.data.creator_name, null, "actor_traits/iconStupid");
		base.tryToShowMetaClan("founder_clan", tReligion.data.creator_clan_id, tReligion.data.creator_clan_name, null);
		base.tryToShowMetaKingdom("origin", tReligion.data.creator_kingdom_id, tReligion.data.creator_kingdom_name, null);
		base.tryToShowMetaCity("birthplace", tReligion.data.creator_city_id, tReligion.data.creator_city_name, null, "iconCity");
		base.tryToShowMetaSubspecies("founder_subspecies", tReligion.data.creator_subspecies_id, tReligion.data.creator_subspecies_name, null);
		base.tryToShowMetaSpecies("founder_species", tReligion.data.creator_species_id);
		base.showStatRow("deity", "??", ColorStyleLibrary.m.color_dead_text, MetaType.None, -1L, false, "iconDivineLight", null, null, true);
	}

	// Token: 0x06003B04 RID: 15108 RVA: 0x0019FB74 File Offset: 0x0019DD74
	public void testDebugNewBook()
	{
		if (this.meta_object.units.Count == 0)
		{
			return;
		}
		Actor tActor = this.meta_object.units.GetRandom<Actor>();
		if (tActor.getCity() == null)
		{
			return;
		}
		if (!tActor.city.hasBookSlots())
		{
			return;
		}
		World.world.books.generateNewBook(tActor);
		this.startShowingWindow();
		this.scroll_window.tabs.showTab(this.scroll_window.tabs.getActiveTab());
	}

	// Token: 0x06003B05 RID: 15109 RVA: 0x0019FBF4 File Offset: 0x0019DDF4
	protected override bool onNameChange(string pInput)
	{
		if (!base.onNameChange(pInput))
		{
			return false;
		}
		long tReligionId = this.meta_object.getID();
		string tReligionName = this.meta_object.data.name;
		foreach (Book tBook in World.world.books)
		{
			if (!tBook.isRekt() && tBook.data.religion_id == tReligionId)
			{
				tBook.data.religion_name = tReligionName;
			}
		}
		return true;
	}

	// Token: 0x06003B07 RID: 15111 RVA: 0x0019FC94 File Offset: 0x0019DE94
	T IAugmentationsWindow<ITraitsEditor<ReligionTrait>>.GetComponentInChildren<T>(bool includeInactive)
	{
		return base.GetComponentInChildren<T>(includeInactive);
	}

	// Token: 0x04002B8A RID: 11146
	public StatBar experienceBar;
}
