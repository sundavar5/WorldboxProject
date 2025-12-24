using System;

// Token: 0x0200030B RID: 779
public static class WorldBehaviourActionSwampAnimation
{
	// Token: 0x06001D5A RID: 7514 RVA: 0x00106454 File Offset: 0x00104654
	public static void updateSwampTiles()
	{
		if (TopTileLibrary.swamp_low.hashset.Count < 10)
		{
			return;
		}
		WorldTile tTile = TopTileLibrary.swamp_low.getCurrentTiles().GetRandom<WorldTile>();
		World.world.redrawRenderedTile(tTile);
	}
}
