using System;
using System.Collections.Generic;
using UnityEngine.UI;

// Token: 0x0200065E RID: 1630
public class CityWindow : WindowMetaGeneric<City, CityData>, IBooksWindow
{
	// Token: 0x170002D7 RID: 727
	// (get) Token: 0x060034CE RID: 13518 RVA: 0x00186B29 File Offset: 0x00184D29
	public override MetaType meta_type
	{
		get
		{
			return MetaType.City;
		}
	}

	// Token: 0x170002D8 RID: 728
	// (get) Token: 0x060034CF RID: 13519 RVA: 0x00186B2C File Offset: 0x00184D2C
	protected override City meta_object
	{
		get
		{
			return SelectedMetas.selected_city;
		}
	}

	// Token: 0x060034D0 RID: 13520 RVA: 0x00186B33 File Offset: 0x00184D33
	public List<long> getBooks()
	{
		return this.meta_object.getBooks();
	}

	// Token: 0x060034D1 RID: 13521 RVA: 0x00186B40 File Offset: 0x00184D40
	protected override void showTopPartInformation()
	{
		base.showTopPartInformation();
		City tCity = this.meta_object;
		if (tCity == null)
		{
			return;
		}
		this.raceTopIcon1.sprite = tCity.getSpriteIcon();
		this.raceTopIcon2.sprite = tCity.getSpriteIcon();
	}

	// Token: 0x060034D2 RID: 13522 RVA: 0x00186B80 File Offset: 0x00184D80
	public override void startShowingWindow()
	{
		base.startShowingWindow();
		AchievementLibrary.checkCityAchievements(this.meta_object);
	}

	// Token: 0x060034D3 RID: 13523 RVA: 0x00186B94 File Offset: 0x00184D94
	private void tryShowPastRulers()
	{
		List<LeaderEntry> past_rulers = this.meta_object.data.past_rulers;
		if (past_rulers != null && past_rulers.Count > 1)
		{
			base.showStatRow("past_leaders", this.meta_object.data.past_rulers.Count, MetaType.None, -1L, "iconVillages", "past_rulers", new TooltipDataGetter(this.getTooltipPastRulers));
		}
	}

	// Token: 0x060034D4 RID: 13524 RVA: 0x00186C01 File Offset: 0x00184E01
	private TooltipData getTooltipPastRulers()
	{
		return new TooltipData
		{
			tip_name = "past_leaders",
			meta_type = MetaType.City,
			past_rulers = new ListPool<LeaderEntry>(this.meta_object.data.past_rulers)
		};
	}

	// Token: 0x060034D5 RID: 13525 RVA: 0x00186C38 File Offset: 0x00184E38
	protected override bool onNameChange(string pInput)
	{
		if (!base.onNameChange(pInput))
		{
			return false;
		}
		foreach (Religion tReligion in World.world.religions)
		{
			if (!tReligion.isRekt() && tReligion.data.creator_city_id == this.meta_object.getID())
			{
				tReligion.data.creator_city_name = this.meta_object.data.name;
			}
		}
		foreach (Culture tCulture in World.world.cultures)
		{
			if (!tCulture.isRekt() && tCulture.data.creator_city_id == this.meta_object.getID())
			{
				tCulture.data.creator_city_name = this.meta_object.data.name;
			}
		}
		foreach (Clan tClan in World.world.clans)
		{
			if (!tClan.isRekt() && tClan.data.founder_city_id == this.meta_object.getID())
			{
				tClan.data.founder_city_name = this.meta_object.data.name;
			}
		}
		foreach (Language tLanguage in World.world.languages)
		{
			if (!tLanguage.isRekt() && tLanguage.data.creator_city_id == this.meta_object.getID())
			{
				tLanguage.data.creator_city_name = this.meta_object.data.name;
			}
		}
		foreach (Family tFamily in World.world.families)
		{
			if (!tFamily.isRekt() && tFamily.data.founder_city_id == this.meta_object.getID())
			{
				tFamily.data.founder_city_name = this.meta_object.data.name;
			}
		}
		foreach (Book tBook in World.world.books)
		{
			if (!tBook.isRekt() && tBook.data.author_city_id == this.meta_object.getID())
			{
				tBook.data.author_city_name = this.meta_object.data.name;
			}
		}
		return true;
	}

	// Token: 0x060034D6 RID: 13526 RVA: 0x00186F34 File Offset: 0x00185134
	internal override void showStatsRows()
	{
		City tCity = this.meta_object;
		if (tCity == null)
		{
			return;
		}
		if (tCity.kingdom.isNeutral())
		{
			this.village_title.setKeyAndUpdate("village_dying");
		}
		else
		{
			this.village_title.setKeyAndUpdate("village");
		}
		base.tryShowPastNames();
		base.showStatRow("founded", tCity.getFoundedDate(), MetaType.None, -1L, "iconAge", null, null);
		base.tryToShowActor("founder", tCity.data.founder_id, tCity.data.founder_name, null, "actor_traits/iconStupid");
		this.tryShowPastRulers();
		base.tryToShowActor("village_statistics_leader", -1L, null, tCity.leader, "iconLeaders");
		if (tCity.hasLeader())
		{
			base.showStatRow("ruler_money", tCity.leader.money, "#43FF43", MetaType.None, -1L, false, "iconMoney", null, null, true);
		}
		string tTaxText = tCity.kingdom.getTaxRateLocal().ToString("0%");
		base.showStatRow("tax", tTaxText, "#43FF43", MetaType.None, -1L, false, "kingdom_traits/kingdom_trait_tax_rate_local_low", null, null, true);
		string tTributeText = tCity.kingdom.getTaxRateTribute().ToString("0%");
		base.showStatRow("tribute", tTributeText, "#43FF43", MetaType.None, -1L, false, "kingdom_traits/kingdom_trait_tax_rate_tribute_high", null, null, true);
		base.tryToShowActor("king", -1L, null, tCity.kingdom.king, "iconKings");
		string pTitle = "founder_species";
		ActorAsset founderSpecies = tCity.getFounderSpecies();
		base.tryToShowMetaSpecies(pTitle, (founderSpecies != null) ? founderSpecies.id : null);
	}

	// Token: 0x060034D7 RID: 13527 RVA: 0x001870C4 File Offset: 0x001852C4
	public override void showMetaRows()
	{
		City tCity = this.meta_object;
		if (tCity == null)
		{
			return;
		}
		if (!tCity.kingdom.isNeutral())
		{
			StatsMetaRowsContainer meta_rows_container = this.meta_rows_container;
			string pTitle = "clan";
			long pID = -1L;
			string pName = null;
			Actor leader = tCity.leader;
			meta_rows_container.tryToShowMetaClan(pTitle, pID, pName, (leader != null) ? leader.clan : null);
			this.meta_rows_container.tryToShowMetaKingdom("kingdom", -1L, null, tCity.kingdom);
			this.meta_rows_container.tryToShowMetaAlliance("alliance", -1L, null, tCity.kingdom.getAlliance());
			this.meta_rows_container.tryToShowMetaCulture("culture", -1L, null, tCity.culture);
			this.meta_rows_container.tryToShowMetaLanguage("language", -1L, null, tCity.language);
			this.meta_rows_container.tryToShowMetaReligion("religion", -1L, null, tCity.religion);
			this.meta_rows_container.tryToShowMetaSubspecies("main_subspecies", -1L, null, tCity.getMainSubspecies());
			this.meta_rows_container.tryToShowMetaArmy("army", -1L, null, tCity.army);
		}
	}

	// Token: 0x060034D8 RID: 13528 RVA: 0x001871C5 File Offset: 0x001853C5
	public void clickTestItemProduction()
	{
		ItemCrafting.tryToCraftRandomWeapon(this.meta_object.units.GetRandom<Actor>(), this.meta_object);
		this.scroll_window.tabs.showTab(this.scroll_window.tabs.getActiveTab());
	}

	// Token: 0x060034D9 RID: 13529 RVA: 0x00187203 File Offset: 0x00185403
	public void clickTestClearItems()
	{
		this.meta_object.data.equipment.clearItems();
		this.scroll_window.tabs.showTab(this.scroll_window.tabs.getActiveTab());
	}

	// Token: 0x060034DA RID: 13530 RVA: 0x0018723C File Offset: 0x0018543C
	public void clickTestNewBook()
	{
		if (!this.meta_object.hasLeader())
		{
			return;
		}
		if (!this.meta_object.leader.hasCulture())
		{
			return;
		}
		if (!this.meta_object.leader.hasLanguage())
		{
			return;
		}
		World.world.books.generateNewBook(this.meta_object.leader);
		this.meta_object.forceDoChecks();
		this.scroll_window.tabs.showTab(this.scroll_window.tabs.getActiveTab());
	}

	// Token: 0x040027B9 RID: 10169
	public Image raceTopIcon1;

	// Token: 0x040027BA RID: 10170
	public Image raceTopIcon2;

	// Token: 0x040027BB RID: 10171
	public LocalizedText village_title;
}
