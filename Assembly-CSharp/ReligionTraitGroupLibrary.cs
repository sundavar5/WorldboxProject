using System;

// Token: 0x0200019F RID: 415
public class ReligionTraitGroupLibrary : BaseCategoryLibrary<ReligionTraitGroupAsset>
{
	// Token: 0x06000C23 RID: 3107 RVA: 0x000AEB38 File Offset: 0x000ACD38
	public override void init()
	{
		base.init();
		this.add(new ReligionTraitGroupAsset
		{
			id = "harmony",
			name = "trait_group_harmony",
			color = "#FFE8AA"
		});
		this.add(new ReligionTraitGroupAsset
		{
			id = "creation",
			name = "trait_group_creation",
			color = "#A5FF7F"
		});
		this.add(new ReligionTraitGroupAsset
		{
			id = "destruction",
			name = "trait_group_destruction",
			color = "#FF4949"
		});
		this.add(new ReligionTraitGroupAsset
		{
			id = "restoration",
			name = "trait_group_restoration",
			color = "#FFE97C"
		});
		this.add(new ReligionTraitGroupAsset
		{
			id = "necromancy",
			name = "trait_group_necromancy",
			color = "#ACB6C1"
		});
		this.add(new ReligionTraitGroupAsset
		{
			id = "protection",
			name = "trait_group_protection",
			color = "#56FFFF"
		});
		this.add(new ReligionTraitGroupAsset
		{
			id = "the_void",
			name = "trait_group_the_void",
			color = "#FF00D5"
		});
		this.add(new ReligionTraitGroupAsset
		{
			id = "transformation",
			name = "trait_group_transformation",
			color = "#C2C1FF"
		});
		this.add(new ReligionTraitGroupAsset
		{
			id = "fate",
			name = "trait_group_fate",
			color = "#ffd82f"
		});
		this.add(new ReligionTraitGroupAsset
		{
			id = "special",
			name = "trait_group_special",
			color = "#FF8F44"
		});
	}
}
