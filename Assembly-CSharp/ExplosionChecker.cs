using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000402 RID: 1026
public class ExplosionChecker
{
	// Token: 0x0600236F RID: 9071 RVA: 0x00127188 File Offset: 0x00125388
	public bool checkNearby(WorldTile pTile, int pRange)
	{
		int tID = pRange * 10000000 + pTile.x * 1000 + pTile.y;
		if (this.data.ContainsKey(tID) || this.isNearbyOthers(pTile, (float)(pRange / 3)))
		{
			return true;
		}
		this.add(tID, pTile, pRange);
		this.updateNearbyTimers(pTile, (float)pRange);
		return false;
	}

	// Token: 0x06002370 RID: 9072 RVA: 0x001271E4 File Offset: 0x001253E4
	private void updateNearbyTimers(WorldTile pTile, float pRange)
	{
		float tTimer = 1f;
		tTimer += (float)(this.data.Count / 10);
		float tRange = pRange + (float)(this.data.Count / 5);
		tTimer = Mathf.Clamp(tTimer, 1f, 5f);
		tRange = Mathf.Clamp(tRange, pRange, pRange * 5f);
		foreach (int tKey in this.data.Keys)
		{
			ExplosionMemoryData tData = this.data[tKey];
			if (Toolbox.Dist(pTile.x, pTile.y, tData.x, tData.y) < tRange)
			{
				tData.timer = tTimer;
			}
		}
	}

	// Token: 0x06002371 RID: 9073 RVA: 0x001272B8 File Offset: 0x001254B8
	private bool isNearbyOthers(WorldTile pTile, float pRange)
	{
		foreach (ExplosionMemoryData tData in this.data.Values)
		{
			if (Toolbox.Dist(pTile.x, pTile.y, tData.x, tData.y) < pRange)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002372 RID: 9074 RVA: 0x00127330 File Offset: 0x00125530
	private void add(int pID, WorldTile pTile, int pRange)
	{
		ExplosionMemoryData tData = new ExplosionMemoryData();
		tData.range = pRange;
		tData.x = pTile.x;
		tData.y = pTile.y;
		float tTimer = 1f;
		tTimer += (float)(this.data.Count / 10);
		tTimer = Mathf.Clamp(tTimer, 1f, 5f);
		tData.timer = tTimer;
		this.data.Add(pID, tData);
	}

	// Token: 0x06002373 RID: 9075 RVA: 0x001273A0 File Offset: 0x001255A0
	public void update(float pElapsed)
	{
		Bench.bench("explosion_checker", "game_total", false);
		foreach (int tId in this.data.Keys)
		{
			ExplosionMemoryData explosionMemoryData = this.data[tId];
			explosionMemoryData.timer -= pElapsed;
			if (explosionMemoryData.timer <= 0f)
			{
				this._to_remove.Add(tId);
			}
		}
		if (this._to_remove.Count > 0)
		{
			for (int i = 0; i < this._to_remove.Count; i++)
			{
				this.data.Remove(this._to_remove[i]);
			}
			this._to_remove.Clear();
		}
		Bench.benchEnd("explosion_checker", "game_total", false, 0L, false);
	}

	// Token: 0x06002374 RID: 9076 RVA: 0x00127490 File Offset: 0x00125690
	public void clear()
	{
		this.data.Clear();
	}

	// Token: 0x06002375 RID: 9077 RVA: 0x001274A0 File Offset: 0x001256A0
	public static void debug(DebugTool pTool)
	{
		pTool.setText("explosion_checker", MapBox.instance.explosion_checker.data.Count, 0f, false, 0L, false, false, "");
	}

	// Token: 0x040019A5 RID: 6565
	private const float TIMER = 1f;

	// Token: 0x040019A6 RID: 6566
	private Dictionary<int, ExplosionMemoryData> data = new Dictionary<int, ExplosionMemoryData>();

	// Token: 0x040019A7 RID: 6567
	private List<int> _to_remove = new List<int>(16);
}
