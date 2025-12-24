using System;

// Token: 0x0200004C RID: 76
public class LanguagesColorsLibrary : ColorLibrary
{
	// Token: 0x06000306 RID: 774 RVA: 0x0001D0CC File Offset: 0x0001B2CC
	public LanguagesColorsLibrary()
	{
		this.file_path = "colors/colors_general";
	}

	// Token: 0x06000307 RID: 775 RVA: 0x0001D0DF File Offset: 0x0001B2DF
	public override void init()
	{
		base.init();
		base.useSameColorsFrom(AssetManager.kingdom_colors_library);
	}

	// Token: 0x06000308 RID: 776 RVA: 0x0001D0F4 File Offset: 0x0001B2F4
	public override bool isColorUsedInWorld(ColorAsset pAsset)
	{
		foreach (Language tObject in World.world.languages)
		{
			if (base.checkColor(pAsset, tObject.data.color_id))
			{
				return true;
			}
		}
		return base.isColorUsedInWorld(pAsset);
	}
}
