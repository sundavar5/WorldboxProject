using System;
using UnityEngine;

// Token: 0x0200006E RID: 110
[Serializable]
public class PhenotypeAsset : BaseAugmentationAsset, ISkipLocaleAsset
{
	// Token: 0x060003CE RID: 974 RVA: 0x00022824 File Offset: 0x00020A24
	public override BaseCategoryAsset getGroup()
	{
		return AssetManager.subspecies_trait_groups.get(this.group_id);
	}

	// Token: 0x060003CF RID: 975 RVA: 0x00022836 File Offset: 0x00020A36
	public PhenotypeAsset()
	{
		this.has_locales = false;
	}

	// Token: 0x04000333 RID: 819
	public string shades_from;

	// Token: 0x04000334 RID: 820
	public string shades_to;

	// Token: 0x04000335 RID: 821
	public string color_eyes;

	// Token: 0x04000336 RID: 822
	public string color_details_1;

	// Token: 0x04000337 RID: 823
	public string color_details_2;

	// Token: 0x04000338 RID: 824
	public int phenotype_index;

	// Token: 0x04000339 RID: 825
	[NonSerialized]
	public Color32[] colors = new Color32[4];

	// Token: 0x0400033A RID: 826
	public string subspecies_trait_id;
}
