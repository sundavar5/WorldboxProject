using System;

// Token: 0x02000303 RID: 771
public class WorldBehaviourActionCreepBiomass
{
	// Token: 0x06001D32 RID: 7474 RVA: 0x00105660 File Offset: 0x00103860
	public static void updateBiomassTiles()
	{
		int i = 0;
		while (i < 3 && WorldBehaviourActionCreepBiomass.tryActionOn(TopTileLibrary.biomass_low))
		{
			i++;
		}
		int j = 0;
		while (j < 3 && WorldBehaviourActionCreepBiomass.tryActionOn(TopTileLibrary.biomass_high))
		{
			j++;
		}
	}

	// Token: 0x06001D33 RID: 7475 RVA: 0x001056A0 File Offset: 0x001038A0
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
