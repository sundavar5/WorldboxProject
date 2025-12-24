using System;

// Token: 0x0200011F RID: 287
public class KingdomTraitGroupLibrary : BaseCategoryLibrary<KingdomTraitGroupAsset>
{
	// Token: 0x060008DA RID: 2266 RVA: 0x00081154 File Offset: 0x0007F354
	public override void init()
	{
		base.init();
		this.add(new KingdomTraitGroupAsset
		{
			id = "tribute",
			name = "trait_group_tribute",
			color = "#BAFFC2"
		});
		this.add(new KingdomTraitGroupAsset
		{
			id = "local_tax",
			name = "trait_group_local_tax",
			color = "#BAFFC2"
		});
		this.add(new KingdomTraitGroupAsset
		{
			id = "miscellaneous",
			name = "trait_group_miscellaneous",
			color = "#D8D8D8"
		});
		this.add(new KingdomTraitGroupAsset
		{
			id = "fate",
			name = "trait_group_fate",
			color = "#ffd82f"
		});
	}
}
