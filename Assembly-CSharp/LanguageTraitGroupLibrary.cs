using System;

// Token: 0x0200019A RID: 410
public class LanguageTraitGroupLibrary : BaseCategoryLibrary<LanguageTraitGroupAsset>
{
	// Token: 0x06000C11 RID: 3089 RVA: 0x000AE19C File Offset: 0x000AC39C
	public override void init()
	{
		base.init();
		this.add(new LanguageTraitGroupAsset
		{
			id = "knowledge",
			name = "trait_group_knowledge",
			color = "#BAF0F4"
		});
		this.add(new LanguageTraitGroupAsset
		{
			id = "spirit",
			name = "trait_group_spirit",
			color = "#BAFFC2"
		});
		this.add(new LanguageTraitGroupAsset
		{
			id = "harmony",
			name = "trait_group_harmony",
			color = "#FFFAA3"
		});
		this.add(new LanguageTraitGroupAsset
		{
			id = "chaos",
			name = "trait_group_chaos",
			color = "#FF6B86"
		});
		this.add(new LanguageTraitGroupAsset
		{
			id = "miscellaneous",
			name = "trait_group_miscellaneous",
			color = "#D8D8D8"
		});
		this.add(new LanguageTraitGroupAsset
		{
			id = "fate",
			name = "trait_group_fate",
			color = "#ffd82f"
		});
		this.add(new LanguageTraitGroupAsset
		{
			id = "special",
			name = "trait_group_special",
			color = "#FF8F44"
		});
	}
}
