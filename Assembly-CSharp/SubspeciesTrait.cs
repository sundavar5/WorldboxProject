using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

// Token: 0x020001A2 RID: 418
[Serializable]
public class SubspeciesTrait : BaseTrait<SubspeciesTrait>, IAnimationFrames
{
	// Token: 0x1700005D RID: 93
	// (get) Token: 0x06000C32 RID: 3122 RVA: 0x000AF8B8 File Offset: 0x000ADAB8
	protected override HashSet<string> progress_elements
	{
		get
		{
			GameProgressData progress_data = base._progress_data;
			if (progress_data == null)
			{
				return null;
			}
			return progress_data.unlocked_traits_subspecies;
		}
	}

	// Token: 0x1700005E RID: 94
	// (get) Token: 0x06000C33 RID: 3123 RVA: 0x000AF8CB File Offset: 0x000ADACB
	public override string typed_id
	{
		get
		{
			return "subspecies_trait";
		}
	}

	// Token: 0x06000C34 RID: 3124 RVA: 0x000AF8D2 File Offset: 0x000ADAD2
	public override BaseCategoryAsset getGroup()
	{
		return AssetManager.subspecies_trait_groups.get(this.group_id);
	}

	// Token: 0x06000C35 RID: 3125 RVA: 0x000AF8E4 File Offset: 0x000ADAE4
	public override Sprite getSprite()
	{
		if (this.cached_sprite == null)
		{
			Sprite tSprite = SpriteTextureLoader.getSprite(this.path_icon);
			if (this.special_icon_logic)
			{
				if (this.phenotype_skin)
				{
					PhenotypeAsset tPhenotype = this.getPhenotypeAsset();
					this.cached_sprite = DynamicSprites.getIconWithColors(tSprite, tPhenotype, null);
				}
			}
			else
			{
				this.cached_sprite = tSprite;
			}
		}
		return this.cached_sprite;
	}

	// Token: 0x06000C36 RID: 3126 RVA: 0x000AF939 File Offset: 0x000ADB39
	public PhenotypeAsset getPhenotypeAsset()
	{
		if (this.phenotype_skin)
		{
			return AssetManager.phenotype_library.get(this.id_phenotype);
		}
		return null;
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x000AF955 File Offset: 0x000ADB55
	protected override IEnumerable<ITraitsOwner<SubspeciesTrait>> getRelatedMetaList()
	{
		return World.world.subspecies;
	}

	// Token: 0x06000C38 RID: 3128 RVA: 0x000AF961 File Offset: 0x000ADB61
	public override string getCountRows()
	{
		return base.getCountRowsByCategories();
	}

	// Token: 0x06000C39 RID: 3129 RVA: 0x000AF969 File Offset: 0x000ADB69
	protected override bool isSapient(ITraitsOwner<SubspeciesTrait> pObject)
	{
		return ((Subspecies)pObject).isSapient();
	}

	// Token: 0x06000C3A RID: 3130 RVA: 0x000AF976 File Offset: 0x000ADB76
	public string[] getWalk()
	{
		return this.animation_walk;
	}

	// Token: 0x06000C3B RID: 3131 RVA: 0x000AF97E File Offset: 0x000ADB7E
	public string[] getIdle()
	{
		return this.animation_idle;
	}

	// Token: 0x06000C3C RID: 3132 RVA: 0x000AF986 File Offset: 0x000ADB86
	public string[] getSwim()
	{
		return this.animation_swim;
	}

	// Token: 0x04000B64 RID: 2916
	public bool in_mutation_pot_add;

	// Token: 0x04000B65 RID: 2917
	public bool in_mutation_pot_remove;

	// Token: 0x04000B66 RID: 2918
	public bool phenotype_skin;

	// Token: 0x04000B67 RID: 2919
	public bool phenotype_egg;

	// Token: 0x04000B68 RID: 2920
	public bool is_diet_related;

	// Token: 0x04000B69 RID: 2921
	[DefaultValue("")]
	public string id_phenotype = string.Empty;

	// Token: 0x04000B6A RID: 2922
	[DefaultValue("")]
	public string id_egg = string.Empty;

	// Token: 0x04000B6B RID: 2923
	public AfterHatchFromEggAction after_hatch_from_egg_action;

	// Token: 0x04000B6C RID: 2924
	public bool has_after_hatch_from_egg_action;

	// Token: 0x04000B6D RID: 2925
	public bool remove_for_zombies;

	// Token: 0x04000B6E RID: 2926
	public bool is_mutation_skin;

	// Token: 0x04000B6F RID: 2927
	public string sprite_path;

	// Token: 0x04000B70 RID: 2928
	public ActorTextureSubAsset texture_asset;

	// Token: 0x04000B71 RID: 2929
	public bool prevent_unconscious_rotation;

	// Token: 0x04000B72 RID: 2930
	public bool render_heads_for_children;

	// Token: 0x04000B73 RID: 2931
	public List<string> skin_citizen_male;

	// Token: 0x04000B74 RID: 2932
	public List<string> skin_citizen_female;

	// Token: 0x04000B75 RID: 2933
	public List<string> skin_warrior;

	// Token: 0x04000B76 RID: 2934
	public string[] animation_walk = ActorAnimationSequences.walk_0;

	// Token: 0x04000B77 RID: 2935
	[DefaultValue(10f)]
	public float animation_walk_speed = 10f;

	// Token: 0x04000B78 RID: 2936
	public string[] animation_swim = ActorAnimationSequences.swim_0;

	// Token: 0x04000B79 RID: 2937
	[DefaultValue(8f)]
	public float animation_swim_speed = 8f;

	// Token: 0x04000B7A RID: 2938
	public string[] animation_idle = ActorAnimationSequences.walk_0;

	// Token: 0x04000B7B RID: 2939
	[DefaultValue(10f)]
	public float animation_idle_speed = 10f;

	// Token: 0x04000B7C RID: 2940
	[DefaultValue(true)]
	public bool shadow = true;

	// Token: 0x04000B7D RID: 2941
	[DefaultValue("unitShadow_5")]
	public string shadow_texture = "unitShadow_5";

	// Token: 0x04000B7E RID: 2942
	[DefaultValue("unitShadow_2")]
	public string shadow_texture_egg = "unitShadow_2";

	// Token: 0x04000B7F RID: 2943
	[DefaultValue("unitShadow_4")]
	public string shadow_texture_baby = "unitShadow_4";
}
