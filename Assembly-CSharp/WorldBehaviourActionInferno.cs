using System;

// Token: 0x02000308 RID: 776
public static class WorldBehaviourActionInferno
{
	// Token: 0x06001D4C RID: 7500 RVA: 0x00105F34 File Offset: 0x00104134
	public static void updateInfernalLowAnimations()
	{
		if (TopTileLibrary.infernal_low.hashset.Count < 10)
		{
			return;
		}
		if (!Randy.randomChance(0.4f))
		{
			return;
		}
		WorldTile tTile = TopTileLibrary.infernal_low.getCurrentTiles().GetRandom<WorldTile>();
		World.world.redrawRenderedTile(tTile);
	}

	// Token: 0x06001D4D RID: 7501 RVA: 0x00105F7D File Offset: 0x0010417D
	public static void updateInfernoFireAction()
	{
		WorldBehaviourActionInferno.tryFireAction(TopTileLibrary.infernal_high);
		WorldBehaviourActionInferno.tryFireAction(TopTileLibrary.infernal_low);
	}

	// Token: 0x06001D4E RID: 7502 RVA: 0x00105F94 File Offset: 0x00104194
	private static void tryFireAction(TopTileType pType)
	{
		if (pType.hashset.Count < 10)
		{
			return;
		}
		if (!Randy.randomChance(0.1f))
		{
			return;
		}
		WorldTile tTile = pType.getCurrentTiles().GetRandom<WorldTile>();
		tTile.startFire(true);
		World.world.particles_fire.spawn(tTile.posV3);
	}
}
