using System;

// Token: 0x020001B4 RID: 436
public class WorldLawGroupLibrary : BaseCategoryLibrary<WorldLawGroupAsset>
{
	// Token: 0x06000CAD RID: 3245 RVA: 0x000B806C File Offset: 0x000B626C
	public override void init()
	{
		base.init();
		this.add(new WorldLawGroupAsset
		{
			id = "harmony",
			name = "world_laws_tab_harmony",
			color = "#FFFAA3"
		});
		this.add(new WorldLawGroupAsset
		{
			id = "diplomacy",
			name = "world_laws_tab_diplomacy",
			color = "#BAF0F4"
		});
		this.add(new WorldLawGroupAsset
		{
			id = "civilizations",
			name = "world_laws_tab_civilizations",
			color = "#BAF0F4"
		});
		this.add(new WorldLawGroupAsset
		{
			id = "units",
			name = "world_laws_tab_units",
			color = "#FF6B86"
		});
		this.add(new WorldLawGroupAsset
		{
			id = "mobs",
			name = "world_laws_tab_mobs",
			color = "#BAFFC2"
		});
		this.add(new WorldLawGroupAsset
		{
			id = "spawn",
			name = "world_laws_tab_spawn",
			color = "#F482FF"
		});
		this.add(new WorldLawGroupAsset
		{
			id = "nature",
			name = "world_laws_tab_nature",
			color = "#68FF77"
		});
		this.add(new WorldLawGroupAsset
		{
			id = "trees",
			name = "world_laws_tab_trees",
			color = "#DEEA5D"
		});
		this.add(new WorldLawGroupAsset
		{
			id = "plants",
			name = "world_laws_tab_plants",
			color = "#E1E894"
		});
		this.add(new WorldLawGroupAsset
		{
			id = "fungi",
			name = "world_laws_tab_fungi",
			color = "#FF9699"
		});
		this.add(new WorldLawGroupAsset
		{
			id = "biomes",
			name = "world_laws_tab_biomes",
			color = "#FFDD32"
		});
		this.add(new WorldLawGroupAsset
		{
			id = "weather",
			name = "world_laws_tab_weather",
			color = "#59B9FF"
		});
		this.add(new WorldLawGroupAsset
		{
			id = "disasters",
			name = "world_laws_tab_disasters",
			color = "#FF6B86"
		});
		this.add(new WorldLawGroupAsset
		{
			id = "other",
			name = "world_laws_tab_other",
			color = "#D8D8D8"
		});
	}
}
