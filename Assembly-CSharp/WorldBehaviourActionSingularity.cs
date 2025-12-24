using System;

// Token: 0x0200030A RID: 778
public static class WorldBehaviourActionSingularity
{
	// Token: 0x06001D58 RID: 7512 RVA: 0x001063DC File Offset: 0x001045DC
	public static void updateSingularityTiles()
	{
		int i = 0;
		while (i < 3 && WorldBehaviourActionSingularity.tryActionOn(TopTileLibrary.singularity_low))
		{
			i++;
		}
		int j = 0;
		while (j < 3 && WorldBehaviourActionSingularity.tryActionOn(TopTileLibrary.singularity_high))
		{
			j++;
		}
	}

	// Token: 0x06001D59 RID: 7513 RVA: 0x0010641C File Offset: 0x0010461C
	private static bool tryActionOn(TopTileType pType)
	{
		if (pType.hashset.Count < 10)
		{
			return false;
		}
		WorldTile tTile = pType.getCurrentTiles().GetRandom<WorldTile>();
		World.world.redrawRenderedTile(tTile);
		return true;
	}
}
