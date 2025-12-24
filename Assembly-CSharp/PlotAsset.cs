using System;
using System.Collections.Generic;
using System.ComponentModel;

// Token: 0x0200014A RID: 330
[Serializable]
public class PlotAsset : BaseAugmentationAsset, IDescription2Asset, IDescriptionAsset, ILocalizedAsset
{
	// Token: 0x1700003A RID: 58
	// (get) Token: 0x060009FB RID: 2555 RVA: 0x00092E6E File Offset: 0x0009106E
	protected override HashSet<string> progress_elements
	{
		get
		{
			GameProgressData progress_data = base._progress_data;
			if (progress_data == null)
			{
				return null;
			}
			return progress_data.unlocked_plots;
		}
	}

	// Token: 0x060009FC RID: 2556 RVA: 0x00092E81 File Offset: 0x00091081
	public override BaseCategoryAsset getGroup()
	{
		return AssetManager.plot_category_library.get(this.group_id);
	}

	// Token: 0x060009FD RID: 2557 RVA: 0x00092E93 File Offset: 0x00091093
	public PlotCategoryAsset getPlotGroup()
	{
		return AssetManager.plot_category_library.get(this.group_id);
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x00092EA8 File Offset: 0x000910A8
	public bool checkIsPossible(Actor pActor, bool pWorldLawsCheck = true)
	{
		return pActor.hasEnoughMoney(this.money_cost) && pActor.level >= this.min_level && pActor.renown >= this.min_renown_actor && pActor.kingdom.getRenown() >= this.min_renown_kingdom && pActor.intelligence >= this.min_intelligence && pActor.diplomacy >= this.min_diplomacy && pActor.warfare >= this.min_warfare && pActor.stewardship >= this.min_stewardship && this.canBeDoneByRole(pActor) && (!pWorldLawsCheck || this.isAllowedByWorldLaws()) && this.check_is_possible(pActor);
	}

	// Token: 0x060009FF RID: 2559 RVA: 0x00092F63 File Offset: 0x00091163
	public bool canBeDoneByRole(Actor pActor)
	{
		return (this.can_be_done_by_king && pActor.isKing()) || (this.can_be_done_by_leader && pActor.isCityLeader()) || (this.can_be_done_by_clan_member && pActor.hasClan());
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x00092F9C File Offset: 0x0009119C
	public bool isAllowedByWorldLaws()
	{
		return (!this.requires_diplomacy || PlotAsset.isDiplomacyON()) && (!this.requires_rebellion || PlotAsset.isRebellionON());
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x00092FC1 File Offset: 0x000911C1
	private static bool isDiplomacyON()
	{
		return WorldLawLibrary.world_law_diplomacy.isEnabled();
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x00092FCD File Offset: 0x000911CD
	private static bool isRebellionON()
	{
		return WorldLawLibrary.world_law_rebellions.isEnabled();
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x00092FD9 File Offset: 0x000911D9
	protected override bool isDebugUnlockedAll()
	{
		return DebugConfig.isOn(DebugOption.UnlockAllPlots);
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x00092FE2 File Offset: 0x000911E2
	public override string getLocaleID()
	{
		return "plot_" + this.id;
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x00092FF4 File Offset: 0x000911F4
	public string getDescriptionID()
	{
		return "plot_" + this.id + "_info";
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x0009300B File Offset: 0x0009120B
	public string getDescriptionID2()
	{
		return "plot_" + this.id + "_info_base";
	}

	// Token: 0x040009D3 RID: 2515
	public int limit_members;

	// Token: 0x040009D4 RID: 2516
	[DefaultValue(1)]
	public int pot_rate = 1;

	// Token: 0x040009D5 RID: 2517
	public int min_level;

	// Token: 0x040009D6 RID: 2518
	public int min_renown_kingdom;

	// Token: 0x040009D7 RID: 2519
	public int min_renown_actor;

	// Token: 0x040009D8 RID: 2520
	[DefaultValue(2)]
	public int min_intelligence = 2;

	// Token: 0x040009D9 RID: 2521
	[DefaultValue(2)]
	public int min_diplomacy = 2;

	// Token: 0x040009DA RID: 2522
	[DefaultValue(2)]
	public int min_warfare = 2;

	// Token: 0x040009DB RID: 2523
	[DefaultValue(2)]
	public int min_stewardship = 2;

	// Token: 0x040009DC RID: 2524
	public bool can_be_done_by_king;

	// Token: 0x040009DD RID: 2525
	public bool can_be_done_by_leader;

	// Token: 0x040009DE RID: 2526
	public bool can_be_done_by_clan_member;

	// Token: 0x040009DF RID: 2527
	public bool requires_diplomacy;

	// Token: 0x040009E0 RID: 2528
	public bool requires_rebellion;

	// Token: 0x040009E1 RID: 2529
	[DefaultValue(60f)]
	public float progress_needed = 60f;

	// Token: 0x040009E2 RID: 2530
	[DefaultValue(5)]
	public int money_cost = 5;

	// Token: 0x040009E3 RID: 2531
	public bool is_basic_plot;

	// Token: 0x040009E4 RID: 2532
	public Rarity rarity;

	// Token: 0x040009E5 RID: 2533
	public PlotDescription get_formatted_description;

	// Token: 0x040009E6 RID: 2534
	public PlotCheckerDelegate check_is_possible;

	// Token: 0x040009E7 RID: 2535
	public PlotCheckerDelegate check_can_be_forced;

	// Token: 0x040009E8 RID: 2536
	public PlotCheckerDelegate check_should_continue;

	// Token: 0x040009E9 RID: 2537
	public PlotActorPlotDelegate check_other_plots;

	// Token: 0x040009EA RID: 2538
	public PlotTryToStart try_to_start_advanced;

	// Token: 0x040009EB RID: 2539
	public PlotStart start;

	// Token: 0x040009EC RID: 2540
	public PlotAction action;

	// Token: 0x040009ED RID: 2541
	public PlotAction post_action;

	// Token: 0x040009EE RID: 2542
	public PlotActorPlotDelegate check_supporters;

	// Token: 0x040009EF RID: 2543
	public bool check_target_actor;

	// Token: 0x040009F0 RID: 2544
	public bool check_target_city;

	// Token: 0x040009F1 RID: 2545
	public bool check_target_kingdom;

	// Token: 0x040009F2 RID: 2546
	public bool check_target_alliance;

	// Token: 0x040009F3 RID: 2547
	public bool check_target_war;
}
