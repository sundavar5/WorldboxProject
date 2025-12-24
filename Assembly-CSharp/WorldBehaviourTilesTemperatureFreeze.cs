using System;
using System.Collections.Generic;

// Token: 0x02000311 RID: 785
public class WorldBehaviourTilesTemperatureFreeze : WorldBehaviourTilesRunner
{
	// Token: 0x06001D7C RID: 7548 RVA: 0x001073DB File Offset: 0x001055DB
	public static void update()
	{
		WorldBehaviourTilesTemperatureFreeze.freezeSummits();
		if (!World.world_era.global_freeze_world)
		{
			return;
		}
		WorldBehaviourTilesTemperatureFreeze.updateSingleTiles();
	}

	// Token: 0x06001D7D RID: 7549 RVA: 0x001073F4 File Offset: 0x001055F4
	private static void freezeSummits()
	{
		if (World.world_era.overlay_sun)
		{
			return;
		}
		if (WorldBehaviourTilesTemperatureFreeze.timer_freeze_summits > 0f)
		{
			WorldBehaviourTilesTemperatureFreeze.timer_freeze_summits -= World.world.elapsed;
			return;
		}
		WorldBehaviourTilesTemperatureFreeze.timer_freeze_summits = Randy.randomFloat(10f, 60f);
		if (TileLibrary.summit.hashset.Count == 0)
		{
			return;
		}
		WorldBehaviourTilesTemperatureFreeze._summit_tiles.AddRange(TileLibrary.summit.hashset);
		foreach (WorldTile tTile in WorldBehaviourTilesTemperatureFreeze._summit_tiles)
		{
			if (!Randy.randomChance(0.8f) && tTile.canBeFrozen())
			{
				tTile.freeze(5);
			}
		}
		WorldBehaviourTilesTemperatureFreeze._summit_tiles.Clear();
	}

	// Token: 0x06001D7E RID: 7550 RVA: 0x001074D0 File Offset: 0x001056D0
	public static void updateSingleTiles()
	{
		WorldBehaviourTilesRunner.checkTiles();
		WorldTile[] tTilesToCheck = WorldBehaviourTilesRunner._tiles_to_check;
		int tMax = World.world.map_chunk_manager.amount_x * 10;
		if (WorldBehaviourTilesRunner._tile_next_check + tMax >= tTilesToCheck.Length)
		{
			tMax = tTilesToCheck.Length - WorldBehaviourTilesRunner._tile_next_check;
		}
		while (tMax-- > 0)
		{
			WorldBehaviourTilesRunner._tiles_to_check.ShuffleOne(WorldBehaviourTilesRunner._tile_next_check);
			WorldTile tTile = tTilesToCheck[WorldBehaviourTilesRunner._tile_next_check++];
			if (tTile.canBeFrozen())
			{
				tTile.freeze(5);
			}
		}
	}

	// Token: 0x04001614 RID: 5652
	private static float timer_freeze_summits = 0f;

	// Token: 0x04001615 RID: 5653
	private static List<WorldTile> _summit_tiles = new List<WorldTile>();
}
