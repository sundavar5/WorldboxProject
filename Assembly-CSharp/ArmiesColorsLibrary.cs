using System;

// Token: 0x02000212 RID: 530
public class ArmiesColorsLibrary : ColorLibrary
{
	// Token: 0x0600130C RID: 4876 RVA: 0x000D62B3 File Offset: 0x000D44B3
	public ArmiesColorsLibrary()
	{
		this.file_path = "colors/colors_general";
	}

	// Token: 0x0600130D RID: 4877 RVA: 0x000D62C6 File Offset: 0x000D44C6
	public override void init()
	{
		base.init();
		base.loadFromFile<ArmiesColorsLibrary>();
	}

	// Token: 0x0600130E RID: 4878 RVA: 0x000D62D4 File Offset: 0x000D44D4
	public override bool isColorUsedInWorld(ColorAsset pAsset)
	{
		foreach (Army tObject in World.world.armies)
		{
			if (base.checkColor(pAsset, tObject.data.color_id))
			{
				return true;
			}
		}
		return base.isColorUsedInWorld(pAsset);
	}
}
