using System;
using System.Collections.Generic;
using UnityEngine.Events;

// Token: 0x02000670 RID: 1648
public class ClanWindow : WindowMetaGeneric<Clan, ClanData>, ITraitWindow<ClanTrait, ClanTraitButton>, IAugmentationsWindow<ITraitsEditor<ClanTrait>>
{
	// Token: 0x170002E7 RID: 743
	// (get) Token: 0x0600352F RID: 13615 RVA: 0x00187F78 File Offset: 0x00186178
	public override MetaType meta_type
	{
		get
		{
			return MetaType.Clan;
		}
	}

	// Token: 0x170002E8 RID: 744
	// (get) Token: 0x06003530 RID: 13616 RVA: 0x00187F7B File Offset: 0x0018617B
	protected override Clan meta_object
	{
		get
		{
			return SelectedMetas.selected_clan;
		}
	}

	// Token: 0x06003531 RID: 13617 RVA: 0x00187F82 File Offset: 0x00186182
	protected override void initNameInput()
	{
		base.initNameInput();
		this.mottoInput.addListener(new UnityAction<string>(this.applyInputMotto));
	}

	// Token: 0x06003532 RID: 13618 RVA: 0x00187FA4 File Offset: 0x001861A4
	protected override bool onNameChange(string pInput)
	{
		if (!base.onNameChange(pInput))
		{
			return false;
		}
		long tClanId = this.meta_object.getID();
		string tClanName = this.meta_object.data.name;
		foreach (Culture tCulture in World.world.cultures)
		{
			if (!tCulture.isRekt() && tCulture.data.creator_clan_id == tClanId)
			{
				tCulture.data.creator_clan_name = tClanName;
			}
		}
		foreach (Religion tReligion in World.world.religions)
		{
			if (!tReligion.isRekt() && tReligion.data.creator_clan_id == tClanId)
			{
				tReligion.data.creator_clan_name = tClanName;
			}
		}
		foreach (Language tLanguage in World.world.languages)
		{
			if (!tLanguage.isRekt() && tLanguage.data.creator_clan_id == tClanId)
			{
				tLanguage.data.creator_clan_name = tClanName;
			}
		}
		foreach (Book tBook in World.world.books)
		{
			if (!tBook.isRekt() && tBook.data.author_clan_id == tClanId)
			{
				tBook.data.author_clan_name = tClanName;
			}
		}
		return true;
	}

	// Token: 0x06003533 RID: 13619 RVA: 0x00188168 File Offset: 0x00186368
	private void applyInputMotto(string pInput)
	{
		if (pInput == null)
		{
			return;
		}
		if (this.meta_object == null)
		{
			return;
		}
		this.meta_object.data.motto = pInput;
	}

	// Token: 0x06003534 RID: 13620 RVA: 0x00188188 File Offset: 0x00186388
	protected override void showTopPartInformation()
	{
		base.showTopPartInformation();
		Clan tClan = this.meta_object;
		if (tClan == null)
		{
			return;
		}
		this.mottoInput.setText(tClan.getMotto());
		this.mottoInput.textField.color = tClan.getColor().getColorText();
	}

	// Token: 0x06003535 RID: 13621 RVA: 0x001881D4 File Offset: 0x001863D4
	internal override void showStatsRows()
	{
		Clan tClan = this.meta_object;
		base.tryShowPastNames();
		base.showStatRow("founded", tClan.getFoundedDate(), MetaType.None, -1L, "iconAge", null, null);
		base.tryToShowActor("clan_founder", tClan.data.founder_actor_id, tClan.data.founder_actor_name, null, "actor_traits/iconStupid");
		base.tryToShowActor("clan_chief_title", -1L, null, tClan.getChief(), "iconClan");
		this.tryShowPastChiefs();
		Actor tNextChief = tClan.getNextChief(null);
		base.tryToShowActor("clan_heir", -1L, null, tNextChief, "iconClanList");
		base.tryToShowMetaCulture("culture", -1L, null, tClan.getClanCulture());
		base.tryToShowMetaKingdom("origin", tClan.data.founder_kingdom_id, tClan.data.founder_kingdom_name, null);
		base.tryToShowMetaCity("birthplace", tClan.data.founder_city_id, tClan.data.founder_city_name, null, "iconCity");
		base.tryToShowMetaSubspecies("original_subspecies", tClan.data.creator_subspecies_id, tClan.data.creator_subspecies_name, null);
		base.tryToShowMetaSpecies("species", tClan.data.creator_species_id);
	}

	// Token: 0x06003536 RID: 13622 RVA: 0x00188304 File Offset: 0x00186504
	private void tryShowPastChiefs()
	{
		List<LeaderEntry> past_chiefs = this.meta_object.data.past_chiefs;
		if (past_chiefs != null && past_chiefs.Count > 1)
		{
			base.showStatRow("past_chiefs", this.meta_object.data.past_chiefs.Count, MetaType.None, -1L, "iconCaptain", "past_rulers", new TooltipDataGetter(this.getTooltipPastChiefs));
		}
	}

	// Token: 0x06003537 RID: 13623 RVA: 0x00188371 File Offset: 0x00186571
	private TooltipData getTooltipPastChiefs()
	{
		return new TooltipData
		{
			tip_name = "past_chiefs",
			meta_type = MetaType.Clan,
			past_rulers = new ListPool<LeaderEntry>(this.meta_object.data.past_chiefs)
		};
	}

	// Token: 0x06003538 RID: 13624 RVA: 0x001883A5 File Offset: 0x001865A5
	protected override void OnDisable()
	{
		base.OnDisable();
		this.mottoInput.inputField.DeactivateInputField();
	}

	// Token: 0x06003539 RID: 13625 RVA: 0x001883BD File Offset: 0x001865BD
	public void debugClearExpLevel()
	{
		this.OnEnable();
	}

	// Token: 0x0600353B RID: 13627 RVA: 0x001883CD File Offset: 0x001865CD
	T IAugmentationsWindow<ITraitsEditor<ClanTrait>>.GetComponentInChildren<T>(bool includeInactive)
	{
		return base.GetComponentInChildren<T>(includeInactive);
	}

	// Token: 0x040027D6 RID: 10198
	public NameInput nameInput;

	// Token: 0x040027D7 RID: 10199
	public NameInput mottoInput;
}
