using System;

// Token: 0x02000196 RID: 406
public class CultureTraitGroupLibrary : BaseCategoryLibrary<CultureTraitGroupAsset>
{
	// Token: 0x06000BFF RID: 3071 RVA: 0x000ACA84 File Offset: 0x000AAC84
	public override void init()
	{
		base.init();
		this.add(new CultureTraitGroupAsset
		{
			id = "harmony",
			name = "trait_group_harmony",
			color = "#FFE877"
		});
		this.add(new CultureTraitGroupAsset
		{
			id = "architecture",
			name = "trait_group_architecture",
			color = "#BAF0F4"
		});
		this.add(new CultureTraitGroupAsset
		{
			id = "town_plan",
			name = "trait_group_town_plan",
			color = "#BAF0F4"
		});
		this.add(new CultureTraitGroupAsset
		{
			id = "kingdom",
			name = "trait_group_kingdom",
			color = "#FF6B86"
		});
		this.add(new CultureTraitGroupAsset
		{
			id = "buildings",
			name = "trait_group_buildings",
			color = "#ADADAD"
		});
		this.add(new CultureTraitGroupAsset
		{
			id = "succession",
			name = "trait_group_succession",
			color = "#CA59FF"
		});
		this.add(new CultureTraitGroupAsset
		{
			id = "knowledge",
			name = "trait_group_knowledge",
			color = "#BAFFC2"
		});
		this.add(new CultureTraitGroupAsset
		{
			id = "warfare",
			name = "trait_group_warfare",
			color = "#FF6B86"
		});
		this.add(new CultureTraitGroupAsset
		{
			id = "weapons",
			name = "trait_group_weapons",
			color = "#FF6B86"
		});
		this.add(new CultureTraitGroupAsset
		{
			id = "craft",
			name = "trait_group_craft",
			color = "#FF6B11"
		});
		this.add(new CultureTraitGroupAsset
		{
			id = "happiness",
			name = "trait_group_happiness",
			color = "#FFFAA3"
		});
		this.add(new CultureTraitGroupAsset
		{
			id = "worldview",
			name = "trait_group_worldview",
			color = "#B0FF8E"
		});
		this.add(new CultureTraitGroupAsset
		{
			id = "miscellaneous",
			name = "trait_group_miscellaneous",
			color = "#D8D8D8"
		});
		this.add(new CultureTraitGroupAsset
		{
			id = "fate",
			name = "trait_group_fate",
			color = "#ffd82f"
		});
		this.add(new CultureTraitGroupAsset
		{
			id = "special",
			name = "trait_group_special",
			color = "#FF8F44"
		});
	}
}
