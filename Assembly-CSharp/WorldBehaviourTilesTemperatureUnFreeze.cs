using System;

// Token: 0x02000313 RID: 787
public class WorldBehaviourTilesTemperatureUnFreeze : WorldBehaviourTilesRunner
{
	// Token: 0x06001D85 RID: 7557 RVA: 0x00107760 File Offset: 0x00105960
	public static void update()
	{
		if (!World.world_era.global_unfreeze_world)
		{
			return;
		}
		if (WorldLawLibrary.world_law_forever_cold.isEnabled())
		{
			return;
		}
		WorldBehaviourTilesTemperatureUnFreeze.updateSingleTiles();
	}

	// Token: 0x06001D86 RID: 7558 RVA: 0x00107784 File Offset: 0x00105984
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
			if (tTile.isTemporaryFrozen() && (!tTile.Type.mountains || WorldBehaviourTilesTemperatureUnFreeze.checkMountainUnfreeze(tTile)))
			{
				tTile.unfreeze(5 + World.world_era.temperature_damage_bonus);
			}
		}
	}

	// Token: 0x06001D87 RID: 7559 RVA: 0x00107820 File Offset: 0x00105A20
	private static bool checkMountainUnfreeze(WorldTile pTile)
	{
		if (World.world_era.global_unfreeze_world_mountains)
		{
			return true;
		}
		if (pTile.Type.summit && !World.world_era.overlay_sun)
		{
			return false;
		}
		if (Randy.randomChance(0.9f))
		{
			return false;
		}
		bool tHasUnfrozenTileNearby = false;
		for (int i = 0; i < pTile.neighboursAll.Length; i++)
		{
			if (!pTile.neighboursAll[i].isFrozen())
			{
				tHasUnfrozenTileNearby = true;
				break;
			}
		}
		return tHasUnfrozenTileNearby;
	}
}
