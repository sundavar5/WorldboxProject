using System;

// Token: 0x02000407 RID: 1031
public static class LavaHelper
{
	// Token: 0x06002399 RID: 9113 RVA: 0x001284B3 File Offset: 0x001266B3
	public static void heatUpLava(WorldTile pTile)
	{
		if (string.IsNullOrEmpty(pTile.Type.lava_increase))
		{
			return;
		}
		LavaHelper.changeLavaTile(pTile, pTile.Type.lava_increase);
	}

	// Token: 0x0600239A RID: 9114 RVA: 0x001284D9 File Offset: 0x001266D9
	public static void coolDownLava(WorldTile pTile)
	{
		if (!string.IsNullOrEmpty(pTile.Type.lava_decrease))
		{
			LavaHelper.changeLavaTile(pTile, pTile.Type.lava_decrease);
			return;
		}
		LavaHelper.putOut(pTile);
	}

	// Token: 0x0600239B RID: 9115 RVA: 0x00128508 File Offset: 0x00126708
	public static void addLava(WorldTile pTile, string pType = "lava3")
	{
		if (pTile.Type.lava && string.IsNullOrEmpty(pTile.Type.lava_increase))
		{
			return;
		}
		pTile.startFire(false);
		MapAction.terraformMain(pTile, AssetManager.tiles.get(pType), TerraformLibrary.lava_damage, false);
	}

	// Token: 0x0600239C RID: 9116 RVA: 0x00128554 File Offset: 0x00126754
	private static void changeLavaTile(WorldTile pTile, string pType)
	{
		MapAction.terraformMain(pTile, AssetManager.tiles.get(pType), TerraformLibrary.lava_damage, false);
	}

	// Token: 0x0600239D RID: 9117 RVA: 0x0012856D File Offset: 0x0012676D
	public static void putOut(WorldTile pTile)
	{
		if (WorldLawLibrary.world_law_forever_lava.isEnabled() || WorldLawLibrary.world_law_gaias_covenant.isEnabled())
		{
			return;
		}
		MapAction.increaseTile(pTile, false, "flash");
	}

	// Token: 0x0600239E RID: 9118 RVA: 0x00128594 File Offset: 0x00126794
	public static void removeLava(WorldTile pTile)
	{
		MapAction.decreaseTile(pTile, false, "flash");
	}
}
