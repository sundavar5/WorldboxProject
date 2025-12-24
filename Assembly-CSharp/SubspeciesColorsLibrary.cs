using System;

// Token: 0x02000079 RID: 121
public class SubspeciesColorsLibrary : ColorLibrary
{
	// Token: 0x06000459 RID: 1113 RVA: 0x0002DC7F File Offset: 0x0002BE7F
	public SubspeciesColorsLibrary()
	{
		this.file_path = "colors/colors_general";
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x0002DC92 File Offset: 0x0002BE92
	public override void init()
	{
		base.init();
		base.useSameColorsFrom(AssetManager.kingdom_colors_library);
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x0002DCA8 File Offset: 0x0002BEA8
	public override bool isColorUsedInWorld(ColorAsset pAsset)
	{
		foreach (Subspecies tObject in World.world.subspecies)
		{
			if (base.checkColor(pAsset, tObject.data.color_id))
			{
				return true;
			}
		}
		return base.isColorUsedInWorld(pAsset);
	}
}
