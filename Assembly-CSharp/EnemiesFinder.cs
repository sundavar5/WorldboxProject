using System;
using System.Collections.Generic;
using UnityEngine.Pool;

// Token: 0x02000569 RID: 1385
public static class EnemiesFinder
{
	// Token: 0x06002D27 RID: 11559 RVA: 0x001607A8 File Offset: 0x0015E9A8
	private static EnemyFinderContainer getCacheContainer(Kingdom pKingdom)
	{
		EnemyFinderContainer tContainer;
		if (!EnemiesFinder._cache_data.TryGetValue(pKingdom, out tContainer))
		{
			tContainer = UnsafeGenericPool<EnemyFinderContainer>.Get();
			tContainer.setKingdom(pKingdom);
			EnemiesFinder._cache_data.Add(pKingdom, tContainer);
		}
		return tContainer;
	}

	// Token: 0x06002D28 RID: 11560 RVA: 0x001607E0 File Offset: 0x0015E9E0
	internal static EnemyFinderData findEnemiesFrom(WorldTile pTile, Kingdom pKingdom, int pChunkRange = -1)
	{
		if (pChunkRange == -1)
		{
			pChunkRange = SimGlobals.m.unit_chunk_sight_range;
		}
		EnemyFinderContainer cacheContainer = EnemiesFinder.getCacheContainer(pKingdom);
		MapChunk tMainChunk = pTile.chunk;
		return cacheContainer.getData(tMainChunk, pChunkRange);
	}

	// Token: 0x06002D29 RID: 11561 RVA: 0x00160814 File Offset: 0x0015EA14
	public static void clear()
	{
		EnemiesFinder.counter_reused = 0;
		foreach (EnemyFinderContainer enemyFinderContainer in EnemiesFinder._cache_data.Values)
		{
			enemyFinderContainer.clear();
			UnsafeGenericPool<EnemyFinderContainer>.Release(enemyFinderContainer);
		}
		EnemiesFinder._cache_data.Clear();
	}

	// Token: 0x06002D2A RID: 11562 RVA: 0x00160880 File Offset: 0x0015EA80
	public static void disposeAll()
	{
		foreach (EnemyFinderContainer enemyFinderContainer in EnemiesFinder._cache_data.Values)
		{
			enemyFinderContainer.disposeAll();
		}
		EnemiesFinder.clear();
	}

	// Token: 0x0400229E RID: 8862
	private static Dictionary<Kingdom, EnemyFinderContainer> _cache_data = new Dictionary<Kingdom, EnemyFinderContainer>();

	// Token: 0x0400229F RID: 8863
	public static int counter_reused = 0;
}
