using System;
using System.Collections.Generic;

// Token: 0x02000031 RID: 49
public class CitizenJobLibrary : AssetLibrary<CitizenJobAsset>
{
	// Token: 0x0600022F RID: 559 RVA: 0x0001477C File Offset: 0x0001297C
	public override void init()
	{
		base.init();
		CitizenJobLibrary.builder = this.add(new CitizenJobAsset
		{
			id = "builder",
			priority = 9,
			debug_option = DebugOption.CitizenJobBuilder,
			path_icon = "ui/Icons/citizen_jobs/iconCitizenJobBuilder"
		});
		CitizenJobLibrary.gatherer_bushes = this.add(new CitizenJobAsset
		{
			id = "gatherer_bushes",
			priority_no_food = 10,
			debug_option = DebugOption.CitizenJobGathererBushes,
			path_icon = "ui/Icons/citizen_jobs/iconCitizenJobGathererBushes"
		});
		CitizenJobLibrary.gatherer_herbs = this.add(new CitizenJobAsset
		{
			id = "gatherer_herbs",
			priority_no_food = 10,
			debug_option = DebugOption.CitizenJobGathererHerbs,
			path_icon = "ui/Icons/citizen_jobs/iconCitizenJobGathererHerbs"
		});
		CitizenJobLibrary.gatherer_honey = this.add(new CitizenJobAsset
		{
			id = "gatherer_honey",
			priority_no_food = 10,
			debug_option = DebugOption.CitizenJobGathererHoney,
			path_icon = "ui/Icons/citizen_jobs/iconCitizenJobGathererHoney"
		});
		CitizenJobLibrary.farmer = this.add(new CitizenJobAsset
		{
			id = "farmer",
			ok_for_king = false,
			ok_for_leader = false,
			debug_option = DebugOption.CitizenJobFarmer,
			path_icon = "ui/Icons/citizen_jobs/iconCitizenJobFarmer"
		});
		CitizenJobLibrary.hunter = this.add(new CitizenJobAsset
		{
			id = "hunter",
			debug_option = DebugOption.CitizenJobHunter,
			path_icon = "ui/Icons/citizen_jobs/iconCitizenJobHunter"
		});
		CitizenJobLibrary.woodcutter = this.add(new CitizenJobAsset
		{
			id = "woodcutter",
			debug_option = DebugOption.CitizenJobWoodcutter,
			path_icon = "ui/Icons/citizen_jobs/iconCitizenJobWoodcutter"
		});
		CitizenJobLibrary.miner = this.add(new CitizenJobAsset
		{
			id = "miner",
			ok_for_king = false,
			ok_for_leader = false,
			debug_option = DebugOption.CitizenJobMiner,
			path_icon = "ui/Icons/citizen_jobs/iconCitizenJobMiner"
		});
		CitizenJobLibrary.miner_deposit = this.add(new CitizenJobAsset
		{
			id = "miner_deposit",
			ok_for_king = false,
			ok_for_leader = false,
			debug_option = DebugOption.CitizenJobMinerDeposit,
			path_icon = "ui/Icons/citizen_jobs/iconCitizenJobMinerDeposit"
		});
		CitizenJobLibrary.road_builder = this.add(new CitizenJobAsset
		{
			id = "road_builder",
			debug_option = DebugOption.CitizenJobRoadBuilder,
			path_icon = "ui/Icons/citizen_jobs/iconCitizenJobRoadBuilder"
		});
		CitizenJobLibrary.cleaner = this.add(new CitizenJobAsset
		{
			id = "cleaner",
			debug_option = DebugOption.CitizenJobCleaner,
			path_icon = "ui/Icons/citizen_jobs/iconCitizenJobCleaner"
		});
		CitizenJobLibrary.manure_cleaner = this.add(new CitizenJobAsset
		{
			id = "manure_cleaner",
			debug_option = DebugOption.CitizenJobManureCleaner,
			path_icon = "ui/Icons/citizen_jobs/iconCitizenJobManureCleaner"
		});
		CitizenJobLibrary.attacker = this.add(new CitizenJobAsset
		{
			id = "attacker",
			debug_option = DebugOption.CitizenJobAttacker,
			path_icon = "ui/Icons/citizen_jobs/iconCitizenJobAttacker",
			common_job = false
		});
	}

	// Token: 0x06000230 RID: 560 RVA: 0x00014A38 File Offset: 0x00012C38
	public override void post_init()
	{
		base.post_init();
		foreach (CitizenJobAsset tAsset in this.list)
		{
			if (tAsset.common_job)
			{
				tAsset.unit_job_default = tAsset.id;
			}
		}
	}

	// Token: 0x06000231 RID: 561 RVA: 0x00014AA0 File Offset: 0x00012CA0
	public override void linkAssets()
	{
		base.linkAssets();
		this.list_priority_normal = new List<CitizenJobAsset>();
		this.list_priority_high = new List<CitizenJobAsset>();
		this.list_priority_high_food = new List<CitizenJobAsset>();
		foreach (CitizenJobAsset tAsset in this.list)
		{
			if (tAsset.common_job)
			{
				if (tAsset.priority_no_food > 0)
				{
					this.list_priority_high_food.Add(tAsset);
				}
				if (tAsset.priority > 0)
				{
					this.list_priority_high.Add(tAsset);
				}
				else
				{
					this.list_priority_normal.Add(tAsset);
				}
			}
		}
	}

	// Token: 0x040001D3 RID: 467
	public List<CitizenJobAsset> list_priority_normal;

	// Token: 0x040001D4 RID: 468
	public List<CitizenJobAsset> list_priority_high;

	// Token: 0x040001D5 RID: 469
	public List<CitizenJobAsset> list_priority_high_food;

	// Token: 0x040001D6 RID: 470
	public static CitizenJobAsset builder;

	// Token: 0x040001D7 RID: 471
	public static CitizenJobAsset gatherer_bushes;

	// Token: 0x040001D8 RID: 472
	public static CitizenJobAsset gatherer_herbs;

	// Token: 0x040001D9 RID: 473
	public static CitizenJobAsset gatherer_honey;

	// Token: 0x040001DA RID: 474
	public static CitizenJobAsset farmer;

	// Token: 0x040001DB RID: 475
	public static CitizenJobAsset hunter;

	// Token: 0x040001DC RID: 476
	public static CitizenJobAsset woodcutter;

	// Token: 0x040001DD RID: 477
	public static CitizenJobAsset miner;

	// Token: 0x040001DE RID: 478
	public static CitizenJobAsset miner_deposit;

	// Token: 0x040001DF RID: 479
	public static CitizenJobAsset road_builder;

	// Token: 0x040001E0 RID: 480
	public static CitizenJobAsset cleaner;

	// Token: 0x040001E1 RID: 481
	public static CitizenJobAsset manure_cleaner;

	// Token: 0x040001E2 RID: 482
	public static CitizenJobAsset attacker;
}
