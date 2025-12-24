using System;

// Token: 0x02000039 RID: 57
[Serializable]
public class CultureColorsLibrary : ColorLibrary
{
	// Token: 0x06000269 RID: 617 RVA: 0x00015BB6 File Offset: 0x00013DB6
	public CultureColorsLibrary()
	{
		this.file_path = "colors/colors_general";
	}

	// Token: 0x0600026A RID: 618 RVA: 0x00015BC9 File Offset: 0x00013DC9
	public override void init()
	{
		base.init();
		base.useSameColorsFrom(AssetManager.kingdom_colors_library);
	}

	// Token: 0x0600026B RID: 619 RVA: 0x00015BDC File Offset: 0x00013DDC
	public override bool isColorUsedInWorld(ColorAsset pAsset)
	{
		foreach (Culture tObject in World.world.cultures)
		{
			if (base.checkColor(pAsset, tObject.data.color_id))
			{
				return true;
			}
		}
		return base.isColorUsedInWorld(pAsset);
	}
}
