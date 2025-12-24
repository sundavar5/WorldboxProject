using System;

// Token: 0x02000192 RID: 402
public class ClanTraitGroupLibrary : BaseCategoryLibrary<ClanTraitGroupAsset>
{
	// Token: 0x06000BF0 RID: 3056 RVA: 0x000ABE58 File Offset: 0x000AA058
	public override void init()
	{
		base.init();
		this.add(new ClanTraitGroupAsset
		{
			id = "spirit",
			name = "trait_group_spirit",
			color = "#BAFFC2"
		});
		this.add(new ClanTraitGroupAsset
		{
			id = "mind",
			name = "trait_group_mind",
			color = "#BAF0F4"
		});
		this.add(new ClanTraitGroupAsset
		{
			id = "body",
			name = "trait_group_body",
			color = "#FF6B86"
		});
		this.add(new ClanTraitGroupAsset
		{
			id = "chaos",
			name = "trait_group_chaos",
			color = "#FF6A00"
		});
		this.add(new ClanTraitGroupAsset
		{
			id = "harmony",
			name = "trait_group_harmony",
			color = "#DD96FF"
		});
		this.add(new ClanTraitGroupAsset
		{
			id = "fate",
			name = "trait_group_fate",
			color = "#ffd82f"
		});
		this.add(new ClanTraitGroupAsset
		{
			id = "special",
			name = "trait_group_special",
			color = "#FF8F44"
		});
	}
}
