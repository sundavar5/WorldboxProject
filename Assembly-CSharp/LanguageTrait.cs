using System;
using System.Collections.Generic;

// Token: 0x02000198 RID: 408
[Serializable]
public class LanguageTrait : BaseTrait<LanguageTrait>
{
	// Token: 0x17000057 RID: 87
	// (get) Token: 0x06000C0B RID: 3083 RVA: 0x000AE152 File Offset: 0x000AC352
	protected override HashSet<string> progress_elements
	{
		get
		{
			GameProgressData progress_data = base._progress_data;
			if (progress_data == null)
			{
				return null;
			}
			return progress_data.unlocked_traits_language;
		}
	}

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x06000C0C RID: 3084 RVA: 0x000AE165 File Offset: 0x000AC365
	public override string typed_id
	{
		get
		{
			return "language_trait";
		}
	}

	// Token: 0x06000C0D RID: 3085 RVA: 0x000AE16C File Offset: 0x000AC36C
	protected override IEnumerable<ITraitsOwner<LanguageTrait>> getRelatedMetaList()
	{
		return World.world.languages;
	}

	// Token: 0x06000C0E RID: 3086 RVA: 0x000AE178 File Offset: 0x000AC378
	public override BaseCategoryAsset getGroup()
	{
		return AssetManager.language_trait_groups.get(this.group_id);
	}

	// Token: 0x04000B60 RID: 2912
	public BookTraitAction read_book_trait_action;
}
