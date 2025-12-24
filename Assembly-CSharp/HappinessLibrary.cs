using System;

// Token: 0x02000109 RID: 265
public class HappinessLibrary : AssetLibrary<HappinessAsset>
{
	// Token: 0x06000816 RID: 2070 RVA: 0x00070D0C File Offset: 0x0006EF0C
	public override void init()
	{
		base.init();
		this.add(new HappinessAsset
		{
			id = "death_family_member",
			value = -20,
			pot_task_id = "crying",
			path_icon = "ui/Icons/iconDead",
			ignored_by_psychopaths = true,
			pot_amount = 5
		});
		this.add(new HappinessAsset
		{
			id = "death_lover",
			value = -50,
			pot_task_id = "crying",
			path_icon = "ui/Icons/iconCrying",
			ignored_by_psychopaths = true,
			pot_amount = 5
		});
		this.add(new HappinessAsset
		{
			id = "death_child",
			value = -60,
			pot_task_id = "crying",
			path_icon = "ui/Icons/iconCrying",
			ignored_by_psychopaths = true,
			pot_amount = 10
		});
		this.add(new HappinessAsset
		{
			id = "death_best_friend",
			value = -25,
			pot_task_id = "crying",
			path_icon = "ui/Icons/iconDead",
			ignored_by_psychopaths = true,
			pot_amount = 3
		});
		this.add(new HappinessAsset
		{
			id = "got_robbed",
			value = -20,
			pot_task_id = "swearing",
			path_icon = "ui/Icons/actor_traits/iconThief",
			pot_amount = 2
		});
		this.add(new HappinessAsset
		{
			id = "got_poked",
			value = -5,
			dialogs_amount = 10,
			path_icon = "ui/Icons/iconGodFinger"
		});
		this.add(new HappinessAsset
		{
			id = "lost_fight",
			value = -20,
			pot_task_id = "swearing",
			path_icon = "ui/Icons/actor_traits/iconWeak",
			pot_amount = 2
		});
		this.add(new HappinessAsset
		{
			id = "got_caught",
			value = -20,
			pot_task_id = "crying",
			path_icon = "ui/Icons/iconSuspicious",
			ignored_by_psychopaths = true,
			pot_amount = 2
		});
		this.add(new HappinessAsset
		{
			id = "paid_tax",
			value = -5,
			pot_task_id = "swearing",
			path_icon = "ui/Icons/kingdom_traits/kingdom_trait_tax_rate_local_high",
			pot_amount = 1
		});
		this.add(new HappinessAsset
		{
			id = "just_ate",
			path_icon = "ui/Icons/iconHunger",
			value = 10
		});
		this.add(new HappinessAsset
		{
			id = "just_received_gift",
			path_icon = "ui/Icons/iconGift",
			ignored_by_psychopaths = true,
			value = 10
		});
		this.add(new HappinessAsset
		{
			id = "just_gave_gift",
			path_icon = "ui/Icons/iconGift",
			value = 20
		});
		this.add(new HappinessAsset
		{
			id = "just_pooped",
			path_icon = "ui/Icons/iconPoop",
			value = 5
		});
		this.add(new HappinessAsset
		{
			id = "just_slept",
			path_icon = "ui/Icons/iconSleep",
			value = 5
		});
		this.add(new HappinessAsset
		{
			id = "had_bad_dream",
			path_icon = "ui/Icons/iconDreamBad",
			value = -15
		});
		this.add(new HappinessAsset
		{
			id = "had_good_dream",
			path_icon = "ui/Icons/iconDreamGood",
			value = 15
		});
		this.add(new HappinessAsset
		{
			id = "had_nightmare",
			path_icon = "ui/Icons/iconDreamNightmare",
			value = -30
		});
		this.add(new HappinessAsset
		{
			id = "slept_outside",
			path_icon = "ui/Icons/iconSleep",
			value = -30
		});
		this.add(new HappinessAsset
		{
			id = "just_kissed",
			value = 10,
			pot_task_id = "singing",
			path_icon = "ui/Icons/iconLovers",
			ignored_by_psychopaths = true,
			pot_amount = 2
		});
		this.add(new HappinessAsset
		{
			id = "just_killed",
			path_icon = "ui/Icons/iconKills",
			value = 10
		});
		this.add(new HappinessAsset
		{
			id = "become_king",
			value = 50,
			pot_task_id = "happy_laughing",
			path_icon = "ui/Icons/iconKings",
			pot_amount = 2
		});
		this.add(new HappinessAsset
		{
			id = "become_leader",
			value = 40,
			pot_task_id = "happy_laughing",
			path_icon = "ui/Icons/iconLeaders",
			pot_amount = 2
		});
		this.add(new HappinessAsset
		{
			id = "just_won_war",
			path_icon = "ui/Icons/iconWar",
			value = 20,
			pot_task_id = "singing",
			pot_amount = 3
		});
		this.add(new HappinessAsset
		{
			id = "just_made_peace",
			path_icon = "ui/Icons/actor_traits/iconPeaceful",
			value = 5
		});
		this.add(new HappinessAsset
		{
			id = "just_lost_war",
			path_icon = "ui/Icons/iconDeadKingdom",
			value = -20,
			pot_task_id = "swearing",
			ignored_by_psychopaths = true,
			pot_amount = 2
		});
		this.add(new HappinessAsset
		{
			id = "was_conquered",
			path_icon = "ui/Icons/iconDeadKingdom",
			value = -20,
			pot_task_id = "crying",
			pot_amount = 2
		});
		this.add(new HappinessAsset
		{
			id = "kingdom_fell_apart",
			path_icon = "ui/Icons/iconDeadKingdom",
			value = -5,
			pot_task_id = "swearing",
			ignored_by_psychopaths = true,
			pot_amount = 2
		});
		this.add(new HappinessAsset
		{
			id = "just_started_war",
			path_icon = "ui/Icons/iconWar",
			value = 20
		});
		this.add(new HappinessAsset
		{
			id = "just_rebelled",
			value = 30,
			pot_task_id = "singing",
			path_icon = "ui/Icons/iconRebellion",
			pot_amount = 3
		});
		this.add(new HappinessAsset
		{
			id = "fallen_in_love",
			value = 40,
			pot_task_id = "singing",
			path_icon = "ui/Icons/iconLovers",
			ignored_by_psychopaths = true,
			pot_amount = 3
		});
		this.add(new HappinessAsset
		{
			id = "just_had_child",
			value = 30,
			pot_task_id = "singing",
			path_icon = "ui/Icons/iconChildren",
			ignored_by_psychopaths = true,
			pot_amount = 1
		});
		this.add(new HappinessAsset
		{
			id = "just_read_book",
			path_icon = "ui/Icons/iconBooks",
			value = 10
		});
		this.add(new HappinessAsset
		{
			id = "just_played",
			value = 15,
			pot_task_id = "happy_laughing",
			ignored_by_psychopaths = true,
			path_icon = "ui/Icons/iconPlayed",
			pot_amount = 3
		});
		this.add(new HappinessAsset
		{
			id = "just_talked",
			path_icon = "ui/Icons/iconRecoverySocial",
			value = 10
		});
		this.add(new HappinessAsset
		{
			id = "just_laughed",
			path_icon = "ui/Icons/iconLaughing",
			value = 5
		});
		this.add(new HappinessAsset
		{
			id = "just_sang",
			path_icon = "ui/Icons/iconSinging",
			value = 10
		});
		this.add(new HappinessAsset
		{
			id = "just_swore",
			path_icon = "ui/Icons/iconSwearing",
			value = 2
		});
		this.add(new HappinessAsset
		{
			id = "just_cried",
			path_icon = "ui/Icons/iconCrying",
			ignored_by_psychopaths = true,
			value = 5
		});
		this.add(new HappinessAsset
		{
			id = "just_talked_gossip",
			path_icon = "ui/Icons/iconRecoveryPlot",
			value = 14
		});
		this.add(new HappinessAsset
		{
			id = "just_surprised",
			value = -10,
			pot_task_id = "swearing",
			path_icon = "ui/Icons/iconSurprised",
			show_change_happiness_effect = false,
			pot_amount = 2
		});
		this.add(new HappinessAsset
		{
			id = "just_born",
			value = 50,
			pot_task_id = "singing",
			path_icon = "ui/Icons/iconAge",
			pot_amount = 1,
			dialogs_amount = 13,
			show_change_happiness_effect = false
		});
		this.add(new HappinessAsset
		{
			id = "just_magnetised",
			value = -10,
			pot_task_id = "swearing",
			path_icon = "ui/Icons/iconMagnetized",
			dialogs_amount = 13,
			show_change_happiness_effect = false,
			pot_amount = 3
		});
		this.add(new HappinessAsset
		{
			id = "just_forced_power",
			path_icon = "ui/Icons/iconForce",
			show_change_happiness_effect = false,
			value = -20
		});
		this.add(new HappinessAsset
		{
			id = "just_possessed",
			path_icon = "ui/Icons/iconPossessed",
			show_change_happiness_effect = false,
			value = -20
		});
		this.add(new HappinessAsset
		{
			id = "strange_urge",
			path_icon = "ui/Icons/iconStrangeUrge",
			show_change_happiness_effect = false,
			value = -5
		});
		this.add(new HappinessAsset
		{
			id = "just_had_tantrum",
			path_icon = "ui/Icons/iconTantrum",
			value = 25
		});
		this.add(new HappinessAsset
		{
			id = "just_felt_the_divine",
			path_icon = "ui/Icons/iconGodFinger",
			value = 40
		});
		this.add(new HappinessAsset
		{
			id = "just_enchanted",
			path_icon = "ui/Icons/iconEnchanted",
			value = 30
		});
		this.add(new HappinessAsset
		{
			id = "just_inspired",
			path_icon = "ui/Icons/iconInspired",
			value = 25
		});
		this.add(new HappinessAsset
		{
			id = "wrote_book",
			path_icon = "ui/Icons/iconBooksWritten",
			value = 20
		});
		this.add(new HappinessAsset
		{
			id = "just_became_adult",
			path_icon = "ui/Icons/iconAdults",
			value = 30
		});
		this.add(new HappinessAsset
		{
			id = "just_got_out_of_egg",
			path_icon = "ui/Icons/iconEgg",
			show_change_happiness_effect = false,
			value = 20
		});
		this.add(new HappinessAsset
		{
			id = "just_finished_plot",
			path_icon = "ui/Icons/iconPlotList",
			value = 25
		});
		this.add(new HappinessAsset
		{
			id = "just_found_house",
			path_icon = "ui/Icons/iconHoused",
			value = 35
		});
		this.add(new HappinessAsset
		{
			id = "just_lost_house",
			value = -30,
			pot_task_id = "start_tantrum",
			path_icon = "ui/Icons/iconHomeless",
			pot_amount = 1
		});
		this.add(new HappinessAsset
		{
			id = "just_made_friend",
			path_icon = "ui/Icons/iconFriendship",
			value = 40
		});
		this.add(new HappinessAsset
		{
			id = "just_injured",
			value = -20,
			pot_task_id = "swearing",
			path_icon = "ui/Icons/actor_traits/iconCrippled",
			pot_amount = 2
		});
		this.add(new HappinessAsset
		{
			id = "just_cursed",
			path_icon = "ui/Icons/iconCursed",
			value = -20
		});
		this.add(new HappinessAsset
		{
			id = "starving",
			path_icon = "ui/Icons/iconHungry",
			value = -40
		});
		this.add(new HappinessAsset
		{
			id = "conquered_city",
			path_icon = "ui/Icons/iconCitySelect",
			value = 30
		});
		this.add(new HappinessAsset
		{
			id = "destroyed_city",
			path_icon = "ui/Icons/iconRebellion",
			value = 35
		});
		this.add(new HappinessAsset
		{
			id = "lost_crown",
			value = -60,
			pot_task_id = "start_tantrum",
			path_icon = "ui/Icons/iconCrown",
			ignored_by_psychopaths = true,
			pot_amount = 4
		});
		this.add(new HappinessAsset
		{
			id = "razed_capital",
			value = -35,
			pot_task_id = "swearing",
			path_icon = "ui/Icons/iconRebellion"
		});
		this.add(new HappinessAsset
		{
			id = "lost_capital",
			value = -30,
			pot_task_id = "swearing",
			path_icon = "ui/Icons/iconCrown"
		});
		this.add(new HappinessAsset
		{
			id = "razed_city",
			value = -20,
			pot_task_id = "swearing",
			path_icon = "ui/Icons/iconRebellion"
		});
		this.add(new HappinessAsset
		{
			id = "lost_city",
			value = -15,
			pot_task_id = "swearing",
			path_icon = "ui/Icons/iconCitySelect"
		});
		this.add(new HappinessAsset
		{
			id = "become_alpha",
			path_icon = "ui/Icons/iconWolf",
			value = 20
		});
	}

	// Token: 0x06000817 RID: 2071 RVA: 0x00071AB4 File Offset: 0x0006FCB4
	public override void post_init()
	{
		base.post_init();
		for (int i = 0; i < this.list.Count; i++)
		{
			this.list[i].index = i;
		}
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x00071AF0 File Offset: 0x0006FCF0
	public override void editorDiagnosticLocales()
	{
		foreach (HappinessAsset tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
			base.checkSpriteExists("path_icon", tAsset.path_icon, tAsset);
			foreach (string tLocaleID in tAsset.getLocaleIDs())
			{
				this.checkLocale(tAsset, tLocaleID);
			}
		}
		base.editorDiagnosticLocales();
	}
}
