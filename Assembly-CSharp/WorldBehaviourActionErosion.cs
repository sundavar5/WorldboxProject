using System;
using System.Collections.Generic;

// Token: 0x02000306 RID: 774
public static class WorldBehaviourActionErosion
{
	// Token: 0x06001D3D RID: 7485 RVA: 0x00105A1C File Offset: 0x00103C1C
	public static void updateErosion()
	{
		if (!WorldLawLibrary.world_law_erosion.isEnabled())
		{
			return;
		}
		WorldBehaviourActionErosion.list.Clear();
		foreach (TileIsland tIsland in World.world.islands_calculator.islands.LoopRandom<TileIsland>())
		{
			if (tIsland.type == TileLayerType.Ground)
			{
				for (int i = 0; i < 5; i++)
				{
					WorldTile tTile = tIsland.getRandomTile();
					if (tTile != null && tTile.Type.can_errode_to_sand && (tTile.Type.can_be_biome || tTile.Type.grass) && tTile.IsOceanAround() && !WorldBehaviourActionErosion.list.Contains(tTile))
					{
						WorldBehaviourActionErosion.list.Add(tTile);
						if (WorldBehaviourActionErosion.list.Count >= 5)
						{
							break;
						}
					}
				}
				if (WorldBehaviourActionErosion.list.Count >= 5)
				{
					break;
				}
			}
		}
		if (WorldBehaviourActionErosion.list.Count == 0)
		{
			return;
		}
		for (int j = 0; j < WorldBehaviourActionErosion.list.Count; j++)
		{
			MapAction.terraformMain(WorldBehaviourActionErosion.list[j], TileLibrary.sand, AssetManager.terraform.get("flash"), false);
		}
	}

	// Token: 0x06001D3E RID: 7486 RVA: 0x00105B5C File Offset: 0x00103D5C
	public static void clear()
	{
		WorldBehaviourActionErosion.list.Clear();
	}

	// Token: 0x04001606 RID: 5638
	private const int MAX_TILES_IN_LIST = 5;

	// Token: 0x04001607 RID: 5639
	private static List<WorldTile> list = new List<WorldTile>();
}
