using System;

// Token: 0x02000033 RID: 51
public class ClanColorsLibrary : ColorLibrary
{
	// Token: 0x06000237 RID: 567 RVA: 0x00014C6F File Offset: 0x00012E6F
	public ClanColorsLibrary()
	{
		this.file_path = "colors/colors_general";
	}

	// Token: 0x06000238 RID: 568 RVA: 0x00014C82 File Offset: 0x00012E82
	public override void init()
	{
		base.init();
		base.useSameColorsFrom(AssetManager.kingdom_colors_library);
	}

	// Token: 0x06000239 RID: 569 RVA: 0x00014C98 File Offset: 0x00012E98
	public override bool isColorUsedInWorld(ColorAsset pAsset)
	{
		foreach (Clan tObject in World.world.clans)
		{
			if (base.checkColor(pAsset, tObject.data.color_id))
			{
				return true;
			}
		}
		return base.isColorUsedInWorld(pAsset);
	}
}
