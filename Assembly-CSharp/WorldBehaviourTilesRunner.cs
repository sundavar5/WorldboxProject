using System;

// Token: 0x02000310 RID: 784
public class WorldBehaviourTilesRunner
{
	// Token: 0x06001D79 RID: 7545 RVA: 0x0010735E File Offset: 0x0010555E
	public static void clearTilesToCheck()
	{
		WorldBehaviourTilesRunner._tiles_to_check = null;
		WorldBehaviourTilesRunner._tile_next_check = 0;
	}

	// Token: 0x06001D7A RID: 7546 RVA: 0x0010736C File Offset: 0x0010556C
	public static void checkTiles()
	{
		if (WorldBehaviourTilesRunner._tiles_to_check == null || WorldBehaviourTilesRunner._tile_next_check >= WorldBehaviourTilesRunner._tiles_to_check.Length - 1)
		{
			if (WorldBehaviourTilesRunner._tiles_to_check == null)
			{
				WorldBehaviourTilesRunner._tiles_to_check = new WorldTile[World.world.tiles_list.Length];
				Array.Copy(World.world.tiles_list, WorldBehaviourTilesRunner._tiles_to_check, World.world.tiles_list.Length);
			}
			WorldBehaviourTilesRunner._tile_next_check = 0;
		}
	}

	// Token: 0x04001612 RID: 5650
	internal static WorldTile[] _tiles_to_check;

	// Token: 0x04001613 RID: 5651
	internal static int _tile_next_check;
}
