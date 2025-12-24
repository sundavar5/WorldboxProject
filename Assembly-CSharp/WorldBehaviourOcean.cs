using System;
using System.Collections.Generic;

// Token: 0x0200030C RID: 780
public static class WorldBehaviourOcean
{
	// Token: 0x06001D5B RID: 7515 RVA: 0x00106490 File Offset: 0x00104690
	public static void clear()
	{
		WorldBehaviourOcean.tiles_to_update.Clear();
		WorldBehaviourOcean.tiles.Clear();
	}

	// Token: 0x06001D5C RID: 7516 RVA: 0x001064A8 File Offset: 0x001046A8
	public static void updateOcean()
	{
		if (WorldBehaviourOcean.tiles.Count == 0)
		{
			return;
		}
		WorldBehaviourOcean.tiles_to_update.Clear();
		foreach (WorldTile tTile in WorldBehaviourOcean.tiles)
		{
			if (tTile.world_edge)
			{
				if ((float)Randy.randomInt(0, 100) >= 30f)
				{
					WorldBehaviourOcean.tiles_to_update.Add(tTile);
				}
			}
			else if (tTile.IsOceanAround() && (float)Randy.randomInt(0, 100) >= 30f)
			{
				WorldBehaviourOcean.tiles_to_update.Add(tTile);
			}
		}
		for (int i = 0; i < WorldBehaviourOcean.tiles_to_update.Count; i++)
		{
			WorldTile tTile2 = WorldBehaviourOcean.tiles_to_update[i];
			if (tTile2.Type.can_be_filled_with_ocean)
			{
				if (tTile2.Type.explodable_by_ocean)
				{
					World.world.explosion_layer.explodeBomb(tTile2, false);
				}
				else
				{
					MapAction.setOcean(tTile2);
				}
			}
		}
	}

	// Token: 0x0400160C RID: 5644
	private static List<WorldTile> tiles_to_update = new List<WorldTile>();

	// Token: 0x0400160D RID: 5645
	public static HashSetWorldTile tiles = new HashSetWorldTile();
}
