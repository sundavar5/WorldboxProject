using System;
using System.Collections.Generic;

// Token: 0x02000120 RID: 288
public class KingdomTraitLibrary : BaseTraitLibrary<KingdomTrait>
{
	// Token: 0x060008DC RID: 2268 RVA: 0x00081223 File Offset: 0x0007F423
	protected override List<string> getDefaultTraitsForMeta(ActorAsset pAsset)
	{
		return pAsset.default_kingdom_traits;
	}

	// Token: 0x060008DD RID: 2269 RVA: 0x0008122C File Offset: 0x0007F42C
	public override void init()
	{
		base.init();
		this.add(new KingdomTrait
		{
			id = "tax_rate_local_high",
			group_id = "local_tax",
			is_local_tax_trait = true,
			tax_rate = 0.7f
		});
		this.t.addOpposite("tax_rate_local_low");
		this.add(new KingdomTrait
		{
			id = "tax_rate_local_low",
			group_id = "local_tax",
			is_local_tax_trait = true,
			tax_rate = 0.2f
		});
		this.t.addOpposite("tax_rate_local_high");
		this.add(new KingdomTrait
		{
			id = "tax_rate_tribute_high",
			group_id = "tribute",
			is_tribute_tax_trait = true,
			tax_rate = 0.7f
		});
		this.t.addOpposite("tax_rate_tribute_low");
		this.add(new KingdomTrait
		{
			id = "tax_rate_tribute_low",
			group_id = "tribute",
			is_tribute_tax_trait = true,
			tax_rate = 0.2f
		});
		this.t.addOpposite("tax_rate_tribute_high");
		this.add(new KingdomTrait
		{
			id = "grin_mark",
			group_id = "fate",
			spawn_random_trait_allowed = false,
			priority = -100
		});
		this.t.setTraitInfoToGrinMark();
		this.t.setUnlockedWithAchievement("achievementCreaturesExplorer");
	}

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x060008DE RID: 2270 RVA: 0x0008139B File Offset: 0x0007F59B
	protected override string icon_path
	{
		get
		{
			return "ui/Icons/kingdom_traits/";
		}
	}
}
