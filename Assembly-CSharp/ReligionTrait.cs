using System;
using System.Collections.Generic;

// Token: 0x0200019D RID: 413
[Serializable]
public class ReligionTrait : BaseTrait<ReligionTrait>
{
	// Token: 0x1700005A RID: 90
	// (get) Token: 0x06000C1D RID: 3101 RVA: 0x000AEAEE File Offset: 0x000ACCEE
	protected override HashSet<string> progress_elements
	{
		get
		{
			GameProgressData progress_data = base._progress_data;
			if (progress_data == null)
			{
				return null;
			}
			return progress_data.unlocked_traits_religion;
		}
	}

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x06000C1E RID: 3102 RVA: 0x000AEB01 File Offset: 0x000ACD01
	public override string typed_id
	{
		get
		{
			return "religion_trait";
		}
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x000AEB08 File Offset: 0x000ACD08
	protected override IEnumerable<ITraitsOwner<ReligionTrait>> getRelatedMetaList()
	{
		return World.world.religions;
	}

	// Token: 0x06000C20 RID: 3104 RVA: 0x000AEB14 File Offset: 0x000ACD14
	public override BaseCategoryAsset getGroup()
	{
		return AssetManager.religion_trait_groups.get(this.group_id);
	}

	// Token: 0x04000B61 RID: 2913
	public string transformation_biome_id;
}
