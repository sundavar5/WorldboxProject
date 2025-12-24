using System;
using System.Collections.Generic;
using UnityEngine.Pool;

// Token: 0x0200056A RID: 1386
public class EnemyFinderContainer
{
	// Token: 0x06002D2C RID: 11564 RVA: 0x001608EE File Offset: 0x0015EAEE
	public void setKingdom(Kingdom pKingdom)
	{
		this._kingdom = pKingdom;
	}

	// Token: 0x06002D2D RID: 11565 RVA: 0x001608F8 File Offset: 0x0015EAF8
	public EnemyFinderData getData(MapChunk pChunk, int pRange)
	{
		int t_id = pChunk.id * 10000 + pRange;
		EnemyFinderData tData;
		if (!this.dict_data.TryGetValue(t_id, out tData))
		{
			tData = UnsafeGenericPool<EnemyFinderData>.Get();
			this.dict_data.Add(t_id, tData);
			if (!this._kingdom.asset.force_look_all_chunks)
			{
				if (pRange == 0)
				{
					EnemyFinderContainer.findEnemiesOfKingdomInChunk(tData, pChunk, this._kingdom);
					return tData;
				}
				if (Randy.randomChance(0.8f))
				{
					EnemyFinderContainer.findEnemiesOfKingdomInChunk(tData, pChunk, this._kingdom);
				}
			}
			if (tData.isEmpty())
			{
				for (int i = 0; i <= pRange; i++)
				{
					this.checkRange(tData, pChunk, i, i);
					if (!tData.isEmpty() && !this._kingdom.asset.force_look_all_chunks)
					{
						break;
					}
				}
			}
			return tData;
		}
		EnemiesFinder.counter_reused++;
		return tData;
	}

	// Token: 0x06002D2E RID: 11566 RVA: 0x001609C0 File Offset: 0x0015EBC0
	private void checkRange(EnemyFinderData pData, MapChunk pChunk, int pRange, int pSkipLessThan = -1)
	{
		if (pRange == 0)
		{
			EnemyFinderContainer.findEnemiesOfKingdomInChunk(pData, pChunk, this._kingdom);
			return;
		}
		int tStartX = pChunk.x;
		int tStartY = pChunk.y;
		bool tSkipCheck = pSkipLessThan > 0;
		int tMin = pSkipLessThan * -1;
		for (int iX = -pRange; iX <= pRange; iX++)
		{
			for (int iY = -pRange; iY <= pRange; iY++)
			{
				if (!tSkipCheck || iX <= tMin || iX >= pSkipLessThan || iY <= tMin || iY >= pSkipLessThan)
				{
					int xx = tStartX + iX;
					int yy = tStartY + iY;
					MapChunk tChunk = World.world.map_chunk_manager.get(xx, yy);
					if (tChunk != null)
					{
						EnemyFinderContainer.findEnemiesOfKingdomInChunk(pData, tChunk, this._kingdom);
					}
				}
			}
		}
	}

	// Token: 0x06002D2F RID: 11567 RVA: 0x00160A6C File Offset: 0x0015EC6C
	private static void findEnemiesOfKingdomInChunk(EnemyFinderData pData, MapChunk pChunk, Kingdom pMainKingdom)
	{
		if (pChunk.objects.kingdoms.Count == 0)
		{
			return;
		}
		List<long> tKingdomsIDs = pChunk.objects.kingdoms;
		bool tPeacefulMonsters = WorldLawLibrary.world_law_peaceful_monsters.isEnabled();
		if (pMainKingdom.asset.mobs && tPeacefulMonsters)
		{
			return;
		}
		for (int i = 0; i < tKingdomsIDs.Count; i++)
		{
			long tKingdomID = tKingdomsIDs[i];
			Kingdom iKingdom = World.world.kingdoms.getCivOrWildViaID(tKingdomID);
			if (iKingdom != null && (!tPeacefulMonsters || !iKingdom.asset.mobs) && pMainKingdom.isEnemy(iKingdom))
			{
				pData.addEnemyList(pChunk.objects.getUnits(tKingdomID));
				pData.addEnemyList(pChunk.objects.getBuildings(tKingdomID));
			}
		}
	}

	// Token: 0x06002D30 RID: 11568 RVA: 0x00160B24 File Offset: 0x0015ED24
	public void clear()
	{
		foreach (EnemyFinderData enemyFinderData in this.dict_data.Values)
		{
			enemyFinderData.reset();
			UnsafeGenericPool<EnemyFinderData>.Release(enemyFinderData);
		}
		this.dict_data.Clear();
	}

	// Token: 0x06002D31 RID: 11569 RVA: 0x00160B8C File Offset: 0x0015ED8C
	public void disposeAll()
	{
		foreach (EnemyFinderData enemyFinderData in this.dict_data.Values)
		{
			enemyFinderData.reset();
		}
		this._kingdom = null;
	}

	// Token: 0x040022A0 RID: 8864
	public Dictionary<int, EnemyFinderData> dict_data = new Dictionary<int, EnemyFinderData>((int)Math.Pow(9.0, (double)SimGlobals.m.unit_chunk_sight_range));

	// Token: 0x040022A1 RID: 8865
	private Kingdom _kingdom;
}
