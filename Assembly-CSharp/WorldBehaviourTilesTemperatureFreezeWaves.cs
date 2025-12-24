using System;
using System.Collections.Generic;

// Token: 0x02000312 RID: 786
public static class WorldBehaviourTilesTemperatureFreezeWaves
{
	// Token: 0x06001D81 RID: 7553 RVA: 0x00107568 File Offset: 0x00105768
	public static void clear()
	{
		WorldBehaviourTilesTemperatureFreezeWaves._nextFreezeWave.Clear();
		WorldBehaviourTilesTemperatureFreezeWaves._currentWave.Clear();
		WorldBehaviourTilesTemperatureFreezeWaves._waveNumber = 0;
	}

	// Token: 0x06001D82 RID: 7554 RVA: 0x00107584 File Offset: 0x00105784
	public static void update()
	{
		if (!World.world_era.global_freeze_world)
		{
			return;
		}
		WorldBehaviourTilesTemperatureFreezeWaves.updateTileFreezeWaves();
	}

	// Token: 0x06001D83 RID: 7555 RVA: 0x00107598 File Offset: 0x00105798
	public static void updateTileFreezeWaves()
	{
		if (WorldBehaviourTilesTemperatureFreezeWaves._waveNumber == 60)
		{
			WorldBehaviourTilesTemperatureFreezeWaves._nextFreezeWave.Clear();
			for (int i = 0; i < WorldBehaviourTilesTemperatureFreezeWaves._currentWave.Count; i++)
			{
				WorldTile tTile = WorldBehaviourTilesTemperatureFreezeWaves._currentWave[i];
				if (tTile.canBeFrozen() && tTile.heat <= 0)
				{
					WorldBehaviourTilesTemperatureFreezeWaves._nextFreezeWave.Add(tTile);
					if (WorldBehaviourTilesTemperatureFreezeWaves._nextFreezeWave.Count > 20)
					{
						break;
					}
				}
			}
			WorldBehaviourTilesTemperatureFreezeWaves._nextFreezeWave.Shuffle<WorldTile>();
			WorldBehaviourTilesTemperatureFreezeWaves._waveNumber = 0;
		}
		WorldBehaviourTilesTemperatureFreezeWaves._currentWave.Clear();
		if (WorldBehaviourTilesTemperatureFreezeWaves._nextFreezeWave.Count == 0)
		{
			int tMaxChunks = 3 + Randy.randomInt(0, 3);
			while (tMaxChunks-- > 0)
			{
				MapChunk random = World.world.map_chunk_manager.chunks.GetRandom<MapChunk>();
				int tAdded = 0;
				foreach (WorldTile tTile2 in random.tiles.LoopRandom<WorldTile>())
				{
					if (tTile2.canBeFrozen() && tTile2.heat <= 0)
					{
						WorldBehaviourTilesTemperatureFreezeWaves._currentWave.Add(tTile2);
						tAdded++;
						if (tAdded > 5)
						{
							break;
						}
					}
				}
			}
		}
		else
		{
			WorldBehaviourTilesTemperatureFreezeWaves._currentWave = WorldBehaviourTilesTemperatureFreezeWaves._nextFreezeWave;
			WorldBehaviourTilesTemperatureFreezeWaves._nextFreezeWave = new List<WorldTile>();
		}
		for (int j = 0; j < WorldBehaviourTilesTemperatureFreezeWaves._currentWave.Count; j++)
		{
			WorldTile tTile3 = WorldBehaviourTilesTemperatureFreezeWaves._currentWave[j];
			if (tTile3.canBeFrozen() && (WorldBehaviourTilesTemperatureFreezeWaves._waveNumber <= 3 || !Randy.randomChance(0.7f)) && tTile3.freeze(5))
			{
				WorldBehaviourTilesTemperatureFreezeWaves._nextFreezeWave.AddRange(tTile3.neighboursAll);
			}
		}
		WorldBehaviourTilesTemperatureFreezeWaves._waveNumber++;
	}

	// Token: 0x04001616 RID: 5654
	private static List<WorldTile> _nextFreezeWave = new List<WorldTile>();

	// Token: 0x04001617 RID: 5655
	private static List<WorldTile> _currentWave = new List<WorldTile>();

	// Token: 0x04001618 RID: 5656
	private static int _waveNumber = 0;

	// Token: 0x04001619 RID: 5657
	private const int MAX_WAVES = 60;

	// Token: 0x0400161A RID: 5658
	private const int MAX_TILES_PER_WAVE = 20;
}
