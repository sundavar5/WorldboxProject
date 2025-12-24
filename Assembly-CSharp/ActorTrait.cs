using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

// Token: 0x02000188 RID: 392
[Serializable]
public class ActorTrait : BaseTrait<ActorTrait>
{
	// Token: 0x1700004C RID: 76
	// (get) Token: 0x06000B9F RID: 2975 RVA: 0x000A5F96 File Offset: 0x000A4196
	protected override HashSet<string> progress_elements
	{
		get
		{
			GameProgressData progress_data = base._progress_data;
			if (progress_data == null)
			{
				return null;
			}
			return progress_data.unlocked_traits_actor;
		}
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x000A5FA9 File Offset: 0x000A41A9
	public override string typed_id
	{
		get
		{
			return "trait";
		}
	}

	// Token: 0x06000BA1 RID: 2977 RVA: 0x000A5FB0 File Offset: 0x000A41B0
	public override BaseCategoryAsset getGroup()
	{
		return AssetManager.trait_groups.get(this.group_id);
	}

	// Token: 0x06000BA2 RID: 2978 RVA: 0x000A5FC2 File Offset: 0x000A41C2
	public int getRate(string pGroup)
	{
		if (pGroup == "body")
		{
			return this.rate_birth;
		}
		return this.rate_acquire_grow_up;
	}

	// Token: 0x06000BA3 RID: 2979 RVA: 0x000A5FDE File Offset: 0x000A41DE
	public Kingdom getForcedKingdom()
	{
		if (this.forced_kingdom == string.Empty)
		{
			Debug.LogError("Shouldn't call this from a trait that doesn't have a forced kingdom! " + this.id);
			return null;
		}
		return World.world.kingdoms_wild.get(this.forced_kingdom);
	}

	// Token: 0x06000BA4 RID: 2980 RVA: 0x000A601E File Offset: 0x000A421E
	protected override IEnumerable<ITraitsOwner<ActorTrait>> getRelatedMetaList()
	{
		return World.world.units;
	}

	// Token: 0x06000BA5 RID: 2981 RVA: 0x000A602A File Offset: 0x000A422A
	public override string getCountRows()
	{
		return base.getCountRowsByCategories();
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x000A6032 File Offset: 0x000A4232
	protected override bool isSapient(ITraitsOwner<ActorTrait> pObject)
	{
		return ((Actor)pObject).isSapient();
	}

	// Token: 0x04000B29 RID: 2857
	public int rate_birth;

	// Token: 0x04000B2A RID: 2858
	public int rate_acquire_grow_up;

	// Token: 0x04000B2B RID: 2859
	public bool acquire_grow_up_sapient_only;

	// Token: 0x04000B2C RID: 2860
	public int rate_inherit;

	// Token: 0x04000B2D RID: 2861
	public bool is_mutation_box_allowed = true;

	// Token: 0x04000B2E RID: 2862
	public int same_trait_mod;

	// Token: 0x04000B2F RID: 2863
	public int opposite_trait_mod;

	// Token: 0x04000B30 RID: 2864
	public bool only_active_on_era_flag;

	// Token: 0x04000B31 RID: 2865
	public bool era_active_moon;

	// Token: 0x04000B32 RID: 2866
	public bool era_active_night;

	// Token: 0x04000B33 RID: 2867
	[DefaultValue(TraitType.Other)]
	public TraitType type = TraitType.Other;

	// Token: 0x04000B34 RID: 2868
	public bool remove_for_zombie_actor_asset;

	// Token: 0x04000B35 RID: 2869
	public bool can_be_cured;

	// Token: 0x04000B36 RID: 2870
	public bool affects_mind;

	// Token: 0x04000B37 RID: 2871
	public bool is_kingdom_affected;

	// Token: 0x04000B38 RID: 2872
	[DefaultValue("")]
	public string forced_kingdom = string.Empty;

	// Token: 0x04000B39 RID: 2873
	public bool can_be_removed_by_divine_light;

	// Token: 0x04000B3A RID: 2874
	public bool can_be_removed_by_accelerated_healing;

	// Token: 0x04000B3B RID: 2875
	public float likeability;

	// Token: 0x04000B3C RID: 2876
	public bool in_training_dummy_combat_pot;
}
