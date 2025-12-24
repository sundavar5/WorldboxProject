using System;
using System.Collections.Generic;

// Token: 0x02000716 RID: 1814
public class LanguageWindow : WindowMetaGeneric<Language, LanguageData>, ITraitWindow<LanguageTrait, LanguageTraitButton>, IAugmentationsWindow<ITraitsEditor<LanguageTrait>>, IBooksWindow
{
	// Token: 0x1700033F RID: 831
	// (get) Token: 0x060039DB RID: 14811 RVA: 0x0019B1C8 File Offset: 0x001993C8
	public override MetaType meta_type
	{
		get
		{
			return MetaType.Language;
		}
	}

	// Token: 0x17000340 RID: 832
	// (get) Token: 0x060039DC RID: 14812 RVA: 0x0019B1CB File Offset: 0x001993CB
	protected override Language meta_object
	{
		get
		{
			return SelectedMetas.selected_language;
		}
	}

	// Token: 0x060039DD RID: 14813 RVA: 0x0019B1D2 File Offset: 0x001993D2
	protected override void showTopPartInformation()
	{
		base.showTopPartInformation();
		AchievementLibrary.multiply_spoken.checkBySignal(this.meta_object);
	}

	// Token: 0x060039DE RID: 14814 RVA: 0x0019B1EC File Offset: 0x001993EC
	internal override void showStatsRows()
	{
		Language tLanguage = this.meta_object;
		base.tryShowPastNames();
		base.showStatRow("founded", tLanguage.getFoundedDate(), MetaType.None, -1L, "iconAge", null, null);
		base.tryToShowActor("creator", tLanguage.data.creator_id, tLanguage.data.creator_name, null, "actor_traits/iconStupid");
		base.tryToShowMetaClan("creators_clan", tLanguage.data.creator_clan_id, tLanguage.data.creator_clan_name, null);
		base.tryToShowMetaKingdom("origin", tLanguage.data.creator_kingdom_id, tLanguage.data.creator_kingdom_name, null);
		base.tryToShowMetaCity("birthplace", tLanguage.data.creator_city_id, tLanguage.data.creator_city_name, null, "iconCity");
		base.tryToShowMetaSubspecies("creator_subspecies", tLanguage.data.creator_subspecies_id, tLanguage.data.creator_subspecies_name, null);
		base.tryToShowMetaSpecies("creator_species", tLanguage.data.creator_species_id);
	}

	// Token: 0x060039DF RID: 14815 RVA: 0x0019B2EC File Offset: 0x001994EC
	public List<long> getBooks()
	{
		return this.meta_object.books.getList();
	}

	// Token: 0x060039E0 RID: 14816 RVA: 0x0019B300 File Offset: 0x00199500
	protected override bool onNameChange(string pInput)
	{
		if (!base.onNameChange(pInput))
		{
			return false;
		}
		long tLanguageId = this.meta_object.getID();
		string tLanguageName = this.meta_object.data.name;
		foreach (Book tBook in World.world.books)
		{
			if (!tBook.isRekt() && tBook.data.language_id == tLanguageId)
			{
				tBook.data.language_name = tLanguageName;
			}
		}
		return true;
	}

	// Token: 0x060039E2 RID: 14818 RVA: 0x0019B3A0 File Offset: 0x001995A0
	T IAugmentationsWindow<ITraitsEditor<LanguageTrait>>.GetComponentInChildren<T>(bool includeInactive)
	{
		return base.GetComponentInChildren<T>(includeInactive);
	}
}
