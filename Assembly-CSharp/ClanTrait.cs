using System;
using System.Collections.Generic;

// Token: 0x02000190 RID: 400
[Serializable]
public class ClanTrait : BaseTrait<ClanTrait>
{
	// Token: 0x17000051 RID: 81
	// (get) Token: 0x06000BEA RID: 3050 RVA: 0x000ABDF9 File Offset: 0x000A9FF9
	protected override HashSet<string> progress_elements
	{
		get
		{
			GameProgressData progress_data = base._progress_data;
			if (progress_data == null)
			{
				return null;
			}
			return progress_data.unlocked_traits_clan;
		}
	}

	// Token: 0x17000052 RID: 82
	// (get) Token: 0x06000BEB RID: 3051 RVA: 0x000ABE0C File Offset: 0x000AA00C
	public override string typed_id
	{
		get
		{
			return "clan_trait";
		}
	}

	// Token: 0x06000BEC RID: 3052 RVA: 0x000ABE13 File Offset: 0x000AA013
	protected override IEnumerable<ITraitsOwner<ClanTrait>> getRelatedMetaList()
	{
		return World.world.clans;
	}

	// Token: 0x06000BED RID: 3053 RVA: 0x000ABE1F File Offset: 0x000AA01F
	public override BaseCategoryAsset getGroup()
	{
		return AssetManager.clan_trait_groups.get(this.group_id);
	}

	// Token: 0x04000B59 RID: 2905
	public BaseStats base_stats_male = new BaseStats();

	// Token: 0x04000B5A RID: 2906
	public BaseStats base_stats_female = new BaseStats();
}
