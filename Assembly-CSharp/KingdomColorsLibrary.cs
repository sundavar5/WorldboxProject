using System;

// Token: 0x0200004B RID: 75
public class KingdomColorsLibrary : ColorLibrary
{
	// Token: 0x06000303 RID: 771 RVA: 0x0001CFE2 File Offset: 0x0001B1E2
	public KingdomColorsLibrary()
	{
		this.file_path = "colors/colors_general";
		this.must_be_global = true;
	}

	// Token: 0x06000304 RID: 772 RVA: 0x0001CFFC File Offset: 0x0001B1FC
	public override void init()
	{
		base.init();
		base.loadFromFile<KingdomColorsLibrary>();
	}

	// Token: 0x06000305 RID: 773 RVA: 0x0001D00C File Offset: 0x0001B20C
	public override bool isColorUsedInWorld(ColorAsset pAsset)
	{
		foreach (Kingdom tObject in World.world.kingdoms)
		{
			if (base.checkColor(pAsset, tObject.data.color_id))
			{
				return true;
			}
		}
		foreach (Alliance tObject2 in World.world.alliances)
		{
			if (base.checkColor(pAsset, tObject2.data.color_id))
			{
				return true;
			}
		}
		return base.isColorUsedInWorld(pAsset);
	}
}
