using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001C6 RID: 454
public static class BattleKeeperManager
{
	// Token: 0x06000D57 RID: 3415 RVA: 0x000BB9DA File Offset: 0x000B9BDA
	public static void clear()
	{
		if (BattleKeeperManager._hashset == null)
		{
			BattleKeeperManager._hashset = new HashSet<BattleContainer>();
		}
		BattleKeeperManager._hashset.Clear();
		BattleKeeperManager._to_remove.Clear();
	}

	// Token: 0x06000D58 RID: 3416 RVA: 0x000BBA01 File Offset: 0x000B9C01
	public static HashSet<BattleContainer> get()
	{
		return BattleKeeperManager._hashset;
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x000BBA08 File Offset: 0x000B9C08
	public static void update(float pElapsed)
	{
		if (BattleKeeperManager._hashset.Count == 0)
		{
			return;
		}
		foreach (BattleContainer tCont in BattleKeeperManager._hashset)
		{
			if (tCont.timer > 1f)
			{
				tCont.timer -= pElapsed;
				tCont.timer = Mathf.Clamp(tCont.timer, 1f, tCont.timer);
			}
			if (tCont.isRendered())
			{
				if (tCont.timer_animation > 0f)
				{
					tCont.timer_animation -= pElapsed;
				}
				else
				{
					tCont.timer_animation = 0.04f;
					tCont.frame++;
					if (tCont.frame >= 8)
					{
						tCont.frame = 7;
					}
				}
			}
			if (tCont.timeout > 0f)
			{
				tCont.timeout -= pElapsed;
			}
			else
			{
				tCont.timer -= pElapsed;
				if (tCont.timer <= 0f)
				{
					BattleKeeperManager._to_remove.Add(tCont);
				}
			}
		}
		if (BattleKeeperManager._to_remove.Count > 0)
		{
			foreach (BattleContainer tCont2 in BattleKeeperManager._to_remove)
			{
				BattleKeeperManager._hashset.Remove(tCont2);
			}
			BattleKeeperManager._to_remove.Clear();
		}
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x000BBB8C File Offset: 0x000B9D8C
	public static void addUnitKilled(Actor pActor)
	{
		BattleContainer tCont = null;
		foreach (BattleContainer iCont in BattleKeeperManager._hashset)
		{
			if ((float)Toolbox.SquaredDistTile(iCont.tile, pActor.current_tile) < 1600f)
			{
				tCont = iCont;
				break;
			}
		}
		if (tCont == null && !pActor.isSapient())
		{
			return;
		}
		if (tCont == null)
		{
			tCont = new BattleContainer();
			tCont.tile = pActor.current_tile;
			BattleKeeperManager._hashset.Add(tCont);
		}
		tCont.increaseDeaths(pActor);
		if (tCont.tile != pActor.current_tile && ((float)Toolbox.SquaredDistTile(tCont.tile, pActor.current_tile) < 25f || tCont.getDeathsTotal() < 3))
		{
			tCont.tile = pActor.current_tile;
		}
		tCont.timer = 1.2f;
		tCont.timeout = 1f;
		if (tCont.frame >= 7)
		{
			tCont.frame = 0;
		}
	}

	// Token: 0x04000CB1 RID: 3249
	private const int MAX_FRAMES = 8;

	// Token: 0x04000CB2 RID: 3250
	private static HashSet<BattleContainer> _hashset;

	// Token: 0x04000CB3 RID: 3251
	private static readonly List<BattleContainer> _to_remove = new List<BattleContainer>();
}
