using System;
using System.Collections.Generic;

// Token: 0x02000413 RID: 1043
public static class VortexAction
{
	// Token: 0x060023FD RID: 9213 RVA: 0x0012C680 File Offset: 0x0012A880
	internal static void moveTiles(WorldTile pCenter, BrushData pBrush)
	{
		VortexAction.clear();
		foreach (BrushPixelData tPixelData in pBrush.pos)
		{
			WorldTile tTile = World.world.GetTile(pCenter.x + tPixelData.x, pCenter.y + tPixelData.y);
			if (tTile != null)
			{
				World.world.flash_effects.flashPixel(tTile, 10, ColorType.White);
				if (!Randy.randomChance(0.8f))
				{
					WorldTile tTarget = Randy.getRandom<WorldTile>(tTile.neighbours);
					if (tTarget != null)
					{
						VortexAction.newTiles.Add(new VortexSwitchHelper
						{
							tile = tTarget,
							newType = tTile.main_type,
							newTopType = tTile.top_type
						});
					}
				}
			}
		}
		foreach (VortexSwitchHelper tSwitch in VortexAction.newTiles)
		{
			MapAction.terraformTile(tSwitch.tile, tSwitch.newType, tSwitch.newTopType, TerraformLibrary.flash, false);
		}
	}

	// Token: 0x060023FE RID: 9214 RVA: 0x0012C7A4 File Offset: 0x0012A9A4
	private static void clear()
	{
		VortexAction.newTiles.Clear();
	}

	// Token: 0x040019F0 RID: 6640
	private static List<VortexSwitchHelper> newTiles = new List<VortexSwitchHelper>();
}
