using System;

// Token: 0x0200018A RID: 394
public class ActorTraitGroupLibrary : BaseCategoryLibrary<ActorTraitGroupAsset>
{
	// Token: 0x06000BA9 RID: 2985 RVA: 0x000A6068 File Offset: 0x000A4268
	public override void init()
	{
		base.init();
		this.add(new ActorTraitGroupAsset
		{
			id = "cognitive",
			name = "trait_group_cognitive",
			color = "#5EFFFF"
		});
		this.add(new ActorTraitGroupAsset
		{
			id = "mind",
			name = "trait_group_mind",
			color = "#BAF0F4"
		});
		this.add(new ActorTraitGroupAsset
		{
			id = "spirit",
			name = "trait_group_spirit",
			color = "#BC42FF"
		});
		this.add(new ActorTraitGroupAsset
		{
			id = "physique",
			name = "trait_group_physique",
			color = "#FF6145"
		});
		this.add(new ActorTraitGroupAsset
		{
			id = "health",
			name = "trait_group_health",
			color = "#89FF56"
		});
		this.add(new ActorTraitGroupAsset
		{
			id = "body",
			name = "trait_group_body",
			color = "#FF6B86"
		});
		this.add(new ActorTraitGroupAsset
		{
			id = "appearance",
			name = "trait_group_appearance",
			color = "#FF6DEB"
		});
		this.add(new ActorTraitGroupAsset
		{
			id = "protection",
			name = "trait_group_protection",
			color = "#FF6B86"
		});
		this.add(new ActorTraitGroupAsset
		{
			id = "skills",
			name = "trait_group_skills",
			color = "#BCBCBC"
		});
		this.add(new ActorTraitGroupAsset
		{
			id = "merits",
			name = "trait_group_merits",
			color = "#FFDA23"
		});
		this.add(new ActorTraitGroupAsset
		{
			id = "acquired",
			name = "trait_group_acquired",
			color = "#A3AFFF"
		});
		this.add(new ActorTraitGroupAsset
		{
			id = "fun",
			name = "trait_group_fun",
			color = "#FFFAA3"
		});
		this.add(new ActorTraitGroupAsset
		{
			id = "fate",
			name = "trait_group_fate",
			color = "#ffd82f"
		});
		this.add(new ActorTraitGroupAsset
		{
			id = "miscellaneous",
			name = "trait_group_miscellaneous",
			color = "#D8D8D8"
		});
		this.add(new ActorTraitGroupAsset
		{
			id = "special",
			name = "trait_group_special",
			color = "#FF8F44"
		});
	}
}
