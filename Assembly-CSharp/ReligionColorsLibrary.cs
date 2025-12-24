using System;

// Token: 0x020002DA RID: 730
[Serializable]
public class ReligionColorsLibrary : ColorLibrary
{
	// Token: 0x06001B1D RID: 6941 RVA: 0x000FB8AB File Offset: 0x000F9AAB
	public ReligionColorsLibrary()
	{
		this.file_path = "colors/colors_general";
	}

	// Token: 0x06001B1E RID: 6942 RVA: 0x000FB8BE File Offset: 0x000F9ABE
	public override void init()
	{
		base.init();
		base.useSameColorsFrom(AssetManager.kingdom_colors_library);
	}

	// Token: 0x06001B1F RID: 6943 RVA: 0x000FB8D4 File Offset: 0x000F9AD4
	public override bool isColorUsedInWorld(ColorAsset pAsset)
	{
		foreach (Religion tObject in World.world.religions)
		{
			if (base.checkColor(pAsset, tObject.data.color_id))
			{
				return true;
			}
		}
		return base.isColorUsedInWorld(pAsset);
	}
}
