using System;
using System.Collections.Generic;

// Token: 0x02000302 RID: 770
public static class WorldBehaviourActionBurnedTiles
{
	// Token: 0x06001D2D RID: 7469 RVA: 0x0010552B File Offset: 0x0010372B
	public static void addTile(WorldTile pTile)
	{
		WorldBehaviourActionBurnedTiles._burned_tiles.Add(pTile);
	}

	// Token: 0x06001D2E RID: 7470 RVA: 0x00105539 File Offset: 0x00103739
	public static void clear()
	{
		WorldBehaviourActionBurnedTiles._burned_tiles.Clear();
		WorldBehaviourActionBurnedTiles._tiles_to_remove.Clear();
	}

	// Token: 0x06001D2F RID: 7471 RVA: 0x0010554F File Offset: 0x0010374F
	public static int countBurnedTiles()
	{
		return WorldBehaviourActionBurnedTiles._burned_tiles.Count;
	}

	// Token: 0x06001D30 RID: 7472 RVA: 0x0010555C File Offset: 0x0010375C
	public static void updateBurnedTiles()
	{
		if (WorldBehaviourActionBurnedTiles._burned_tiles.Count == 0)
		{
			return;
		}
		WorldBehaviourActionBurnedTiles._tiles_to_remove.Clear();
		foreach (WorldTile tTile in WorldBehaviourActionBurnedTiles._burned_tiles)
		{
			if (!tTile.isOnFire())
			{
				tTile.burned_stages--;
				World.world.burned_layer.setTileDirty(tTile);
				if (tTile.burned_stages <= 0)
				{
					tTile.burned_stages = 0;
					WorldBehaviourActionBurnedTiles._tiles_to_remove.Add(tTile);
				}
			}
		}
		foreach (WorldTile tTile2 in WorldBehaviourActionBurnedTiles._tiles_to_remove)
		{
			WorldBehaviourActionBurnedTiles._burned_tiles.Remove(tTile2);
		}
	}

	// Token: 0x040015FE RID: 5630
	private static readonly HashSet<WorldTile> _burned_tiles = new HashSet<WorldTile>();

	// Token: 0x040015FF RID: 5631
	private static readonly List<WorldTile> _tiles_to_remove = new List<WorldTile>();
}
