using System;
using UnityEngine;

// Token: 0x020007CC RID: 1996
public class WarWindow : WindowMetaGeneric<War, WarData>
{
	// Token: 0x170003A8 RID: 936
	// (get) Token: 0x06003EF6 RID: 16118 RVA: 0x001B41A6 File Offset: 0x001B23A6
	public override MetaType meta_type
	{
		get
		{
			return MetaType.War;
		}
	}

	// Token: 0x170003A9 RID: 937
	// (get) Token: 0x06003EF7 RID: 16119 RVA: 0x001B41AA File Offset: 0x001B23AA
	protected override War meta_object
	{
		get
		{
			return SelectedMetas.selected_war;
		}
	}

	// Token: 0x06003EF8 RID: 16120 RVA: 0x001B41B4 File Offset: 0x001B23B4
	internal override void showStatsRows()
	{
		War tWar = this.meta_object;
		base.tryShowPastNames();
		base.showStatRow("war_type", LocalizedTextManager.getText(tWar.getAsset().localized_war_name, null, false), MetaType.None, -1L, "iconWar", null, null);
		base.showStatRow("started_at", tWar.getFoundedDate(), MetaType.None, -1L, "iconAge", null, null);
		if (tWar.hasEnded())
		{
			base.showStatRow("war_ended_at", tWar.getYearEnded().ToString() ?? "", MetaType.None, -1L, "iconClose", null, null);
		}
		base.showStatRow("war_duration", tWar.getDuration().ToString() ?? "", MetaType.None, -1L, "iconClock", null, null);
		string tWinnerLocale = tWar.data.winner.getLocaleID().Localize();
		switch (tWar.data.winner)
		{
		case WarWinner.Attackers:
			base.showStatRow("war_winner", tWinnerLocale, tWar.getAttackersColorTextString(), MetaType.None, -1L, true, "iconAttackRate", null, null, true);
			break;
		case WarWinner.Defenders:
			base.showStatRow("war_winner", tWinnerLocale, tWar.getDefendersColorTextString(), MetaType.None, -1L, true, "iconAttackRate", null, null, true);
			break;
		case WarWinner.Peace:
			base.showStatRow("war_outcome", tWinnerLocale, MetaType.None, -1L, "actor_traits/iconPeaceful", null, null);
			break;
		case WarWinner.Merged:
			base.showStatRow("war_outcome", tWinnerLocale, MetaType.None, -1L, "iconBre", null, null);
			break;
		}
		base.tryToShowActor("instigator", tWar.data.started_by_actor_id, tWar.data.started_by_actor_name, null, "worldrules/icon_angryvillagers");
		base.tryToShowMetaKingdom("instigator_from", tWar.data.started_by_kingdom_id, tWar.data.started_by_kingdom_name, null);
		base.showStatRow("kingdoms", tWar.countKingdoms().ToString(), MetaType.None, -1L, "iconKingdomList", null, null);
		base.showStatRow("villages", tWar.countCities().ToString(), MetaType.None, -1L, "iconVillages", null, null);
		base.showStatRow("deaths", tWar.getTotalDeaths().ToString() ?? "", MetaType.None, -1L, "iconDead", null, null);
		base.showStatRow("attackers_army", tWar.countAttackersWarriors(), MetaType.None, -1L, "iconArmyAttackers", null, null);
		base.showStatRow("attackers_population", tWar.countAttackersPopulation(), MetaType.None, -1L, "iconPopulationAttackers", null, null);
		base.showStatRow("attackers_deaths", tWar.getDeadAttackers(), MetaType.None, -1L, "iconDeathAttackers", null, null);
		base.showStatRow("attackers_cities", tWar.countAttackersCities(), MetaType.None, -1L, "iconVillages", null, null);
		base.showStatRow("defenders_army", tWar.countDefendersWarriors(), MetaType.None, -1L, "iconArmyDefenders", null, null);
		base.showStatRow("defenders_population", tWar.countDefendersPopulation(), MetaType.None, -1L, "iconPopulationDefenders", null, null);
		base.showStatRow("defenders_deaths", tWar.getDeadDefenders(), MetaType.None, -1L, "iconDeathDefenders", null, null);
		base.showStatRow("defenders_cities", tWar.countDefendersCities(), MetaType.None, -1L, "iconVillages", null, null);
		AchievementLibrary.ancient_war_of_geometry_and_evil.checkBySignal(null);
	}

	// Token: 0x06003EF9 RID: 16121 RVA: 0x001B44F4 File Offset: 0x001B26F4
	public override void startShowingWindow()
	{
		base.startShowingWindow();
		if (!this.meta_object.hasEnded())
		{
			this._button_interesting_persons_tab.toggleActive(true);
		}
		else
		{
			this._button_interesting_persons_tab.toggleActive(false);
		}
		if (base.tabs.getActiveTab() == this._button_interesting_persons_tab && this.meta_object.hasEnded())
		{
			base.showTab(base.tabs.tab_default);
		}
	}

	// Token: 0x04002DD8 RID: 11736
	[SerializeField]
	private WindowMetaTab _button_interesting_persons_tab;
}
