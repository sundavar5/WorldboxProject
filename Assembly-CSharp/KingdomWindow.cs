using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000700 RID: 1792
public class KingdomWindow : WindowMetaGeneric<Kingdom, KingdomData>, ITraitWindow<KingdomTrait, KingdomTraitButton>, IAugmentationsWindow<ITraitsEditor<KingdomTrait>>
{
	// Token: 0x17000330 RID: 816
	// (get) Token: 0x0600394F RID: 14671 RVA: 0x0019840C File Offset: 0x0019660C
	public override MetaType meta_type
	{
		get
		{
			return MetaType.Kingdom;
		}
	}

	// Token: 0x17000331 RID: 817
	// (get) Token: 0x06003950 RID: 14672 RVA: 0x0019840F File Offset: 0x0019660F
	protected override Kingdom meta_object
	{
		get
		{
			return SelectedMetas.selected_kingdom;
		}
	}

	// Token: 0x06003951 RID: 14673 RVA: 0x00198416 File Offset: 0x00196616
	protected override void initNameInput()
	{
		base.initNameInput();
		this.mottoInput.addListener(new UnityAction<string>(this.applyInputMotto));
	}

	// Token: 0x06003952 RID: 14674 RVA: 0x00198438 File Offset: 0x00196638
	protected override bool onNameChange(string pInput)
	{
		if (!base.onNameChange(pInput))
		{
			return false;
		}
		long tKingdomID = this.meta_object.getID();
		string tKingdomName = this.meta_object.data.name;
		foreach (War tWar in World.world.wars)
		{
			if (!tWar.isRekt() && tWar.data.started_by_kingdom_id == tKingdomID)
			{
				tWar.data.started_by_kingdom_name = tKingdomName;
			}
		}
		foreach (Alliance tAlliance in World.world.alliances)
		{
			if (!tAlliance.isRekt() && tAlliance.data.founder_kingdom_id == tKingdomID)
			{
				tAlliance.data.founder_kingdom_name = tKingdomName;
			}
		}
		foreach (Religion tReligion in World.world.religions)
		{
			if (!tReligion.isRekt() && tReligion.data.creator_kingdom_id == tKingdomID)
			{
				tReligion.data.creator_kingdom_name = tKingdomName;
			}
		}
		foreach (Culture tCulture in World.world.cultures)
		{
			if (!tCulture.isRekt() && tCulture.data.creator_kingdom_id == tKingdomID)
			{
				tCulture.data.creator_kingdom_name = tKingdomName;
			}
		}
		foreach (Clan tClan in World.world.clans)
		{
			if (!tClan.isRekt() && tClan.data.founder_kingdom_id == tKingdomID)
			{
				tClan.data.founder_kingdom_name = tKingdomName;
			}
		}
		foreach (Language tLanguage in World.world.languages)
		{
			if (!tLanguage.isRekt() && tLanguage.data.creator_kingdom_id == tKingdomID)
			{
				tLanguage.data.creator_kingdom_name = tKingdomName;
			}
		}
		foreach (Family tFamily in World.world.families)
		{
			if (!tFamily.isRekt() && tFamily.data.founder_kingdom_id == tKingdomID)
			{
				tFamily.data.founder_kingdom_name = tKingdomName;
			}
		}
		foreach (Book tBook in World.world.books)
		{
			if (!tBook.isRekt() && tBook.data.author_kingdom_id == tKingdomID)
			{
				tBook.data.author_kingdom_name = tKingdomName;
			}
		}
		foreach (Item tItem in World.world.items)
		{
			if (!tItem.isRekt() && tItem.data.creator_kingdom_id == tKingdomID)
			{
				tItem.data.from = tKingdomName;
			}
		}
		foreach (Army tArmy in World.world.armies)
		{
			if (!tArmy.isRekt() && tArmy.getKingdom() == this.meta_object)
			{
				tArmy.onKingdomNameChange();
			}
		}
		return true;
	}

	// Token: 0x06003953 RID: 14675 RVA: 0x0019884C File Offset: 0x00196A4C
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

	// Token: 0x06003954 RID: 14676 RVA: 0x0019886C File Offset: 0x00196A6C
	protected override void showTopPartInformation()
	{
		base.showTopPartInformation();
		Kingdom tKingdom = this.meta_object;
		if (tKingdom == null)
		{
			return;
		}
		this.raceTopIcon1.sprite = tKingdom.getSpriteIcon();
		this.raceTopIcon2.sprite = tKingdom.getSpriteIcon();
		this.mottoInput.setText(tKingdom.getMotto());
		this.mottoInput.textField.color = tKingdom.getColor().getColorText();
	}

	// Token: 0x06003955 RID: 14677 RVA: 0x001988D8 File Offset: 0x00196AD8
	private void tryShowPastRulers()
	{
		List<LeaderEntry> past_rulers = this.meta_object.data.past_rulers;
		if (past_rulers != null && past_rulers.Count > 1)
		{
			base.showStatRow("past_kings", this.meta_object.data.past_rulers.Count, MetaType.None, -1L, "iconKingdomList", "past_rulers", new TooltipDataGetter(this.getTooltipPastRulers));
		}
	}

	// Token: 0x06003956 RID: 14678 RVA: 0x00198945 File Offset: 0x00196B45
	private TooltipData getTooltipPastRulers()
	{
		return new TooltipData
		{
			tip_name = "past_kings",
			meta_type = MetaType.Kingdom,
			past_rulers = new ListPool<LeaderEntry>(this.meta_object.data.past_rulers)
		};
	}

	// Token: 0x06003957 RID: 14679 RVA: 0x0019897C File Offset: 0x00196B7C
	internal override void showStatsRows()
	{
		Kingdom tKingdom = this.meta_object;
		base.tryShowPastNames();
		base.showStatRow("founded", tKingdom.getFoundedDate(), MetaType.None, -1L, "iconAge", null, null);
		this.tryShowPastRulers();
		base.tryToShowActor("king", -1L, null, tKingdom.king, "iconKings");
		Actor tHeir = SuccessionTool.findNextHeir(tKingdom, tKingdom.king);
		base.tryToShowActor("heir", -1L, null, tHeir, "iconChildren");
		if (tKingdom.hasKing())
		{
			if (tKingdom.king.s_personality != null)
			{
				base.showStatRow("creature_statistics_personality", tKingdom.king.s_personality.getTranslatedName(), MetaType.None, -1L, "actor_traits/iconStupid", null, null);
			}
			int tKingRuleAge = Date.getYearsSince(tKingdom.data.timestamp_king_rule);
			base.showStatRow("kingdom_statistics_king_ruled", tKingRuleAge, MetaType.None, -1L, "iconClock", null, null);
			base.showStatRow("ruler_money", tKingdom.king.money, "#43FF43", MetaType.None, -1L, false, "iconMoney", null, null, true);
		}
		string tTributeText = tKingdom.getTaxRateTribute().ToString("0%");
		base.showStatRow("tribute", tTributeText, "#43FF43", MetaType.None, -1L, false, "kingdom_traits/kingdom_trait_tax_rate_tribute_high", null, null, true);
		base.tryToShowMetaSpecies("founder_species", tKingdom.getFounderSpecies().id);
	}

	// Token: 0x06003958 RID: 14680 RVA: 0x00198AD4 File Offset: 0x00196CD4
	public override void showMetaRows()
	{
		Kingdom tKingdom = this.meta_object;
		this.meta_rows_container.tryToShowMetaAlliance("alliance", -1L, null, tKingdom.getAlliance());
		this.meta_rows_container.tryToShowMetaCity("kingdom_statistics_capital", -1L, null, tKingdom.capital, "iconKingdom");
		StatsMetaRowsContainer meta_rows_container = this.meta_rows_container;
		string pTitle = "clan";
		long pID = -1L;
		string pName = null;
		Actor king = tKingdom.king;
		meta_rows_container.tryToShowMetaClan(pTitle, pID, pName, (king != null) ? king.clan : null);
		this.meta_rows_container.tryToShowMetaCulture("culture", -1L, null, tKingdom.culture);
		this.meta_rows_container.tryToShowMetaLanguage("language", -1L, null, tKingdom.language);
		this.meta_rows_container.tryToShowMetaReligion("religion", -1L, null, tKingdom.religion);
		this.meta_rows_container.tryToShowMetaSubspecies("main_subspecies", -1L, null, tKingdom.getMainSubspecies());
	}

	// Token: 0x06003959 RID: 14681 RVA: 0x00198BA8 File Offset: 0x00196DA8
	protected override void OnDisable()
	{
		base.OnDisable();
		this.mottoInput.inputField.DeactivateInputField();
	}

	// Token: 0x0600395A RID: 14682 RVA: 0x00198BC0 File Offset: 0x00196DC0
	public void clickCapital()
	{
		SelectedMetas.selected_city = this.meta_object.capital;
		ScrollWindow.showWindow("city");
	}

	// Token: 0x0600395C RID: 14684 RVA: 0x00198BE4 File Offset: 0x00196DE4
	T IAugmentationsWindow<ITraitsEditor<KingdomTrait>>.GetComponentInChildren<T>(bool includeInactive)
	{
		return base.GetComponentInChildren<T>(includeInactive);
	}

	// Token: 0x04002A49 RID: 10825
	public Image raceTopIcon1;

	// Token: 0x04002A4A RID: 10826
	public Image raceTopIcon2;

	// Token: 0x04002A4B RID: 10827
	public NameInput mottoInput;
}
