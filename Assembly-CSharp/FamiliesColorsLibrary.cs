using System;

// Token: 0x02000042 RID: 66
public class FamiliesColorsLibrary : ColorLibrary
{
	// Token: 0x060002DC RID: 732 RVA: 0x0001C767 File Offset: 0x0001A967
	public FamiliesColorsLibrary()
	{
		this.file_path = "colors/colors_general";
	}

	// Token: 0x060002DD RID: 733 RVA: 0x0001C77A File Offset: 0x0001A97A
	public override void init()
	{
		base.init();
		base.useSameColorsFrom(AssetManager.kingdom_colors_library);
	}

	// Token: 0x060002DE RID: 734 RVA: 0x0001C790 File Offset: 0x0001A990
	public override bool isColorUsedInWorld(ColorAsset pAsset)
	{
		foreach (Family tObject in World.world.families)
		{
			if (base.checkColor(pAsset, tObject.data.color_id))
			{
				return true;
			}
		}
		return base.isColorUsedInWorld(pAsset);
	}
}
