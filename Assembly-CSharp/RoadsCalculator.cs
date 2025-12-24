using System;
using System.Collections.Generic;

// Token: 0x02000410 RID: 1040
public class RoadsCalculator : BaseModule
{
	// Token: 0x060023F2 RID: 9202 RVA: 0x0012C0B3 File Offset: 0x0012A2B3
	internal override void create()
	{
		base.create();
		this.islands = new List<TileIsland>();
		this.hashset = new HashSet<WorldTile>();
	}

	// Token: 0x060023F3 RID: 9203 RVA: 0x0012C0D4 File Offset: 0x0012A2D4
	public void setDirty(WorldTile pTile)
	{
		if (pTile.road_island != null)
		{
			pTile.road_island.dirty = true;
		}
		if (pTile.Type.road)
		{
			this.hashset.Add(pTile);
		}
		else
		{
			this.hashset.Remove(pTile);
			pTile.road_island = null;
		}
		this.dirty = true;
	}

	// Token: 0x060023F4 RID: 9204 RVA: 0x0012C12C File Offset: 0x0012A32C
	internal override void clear()
	{
		base.clear();
		this.hashset.Clear();
		this.islands.Clear();
	}

	// Token: 0x060023F5 RID: 9205 RVA: 0x0012C14A File Offset: 0x0012A34A
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (!this.dirty)
		{
			return;
		}
		this.dirty = false;
		this.RecalculateIslands();
	}

	// Token: 0x060023F6 RID: 9206 RVA: 0x0012C16C File Offset: 0x0012A36C
	private void RecalculateIslands()
	{
		for (int i_islands = 0; i_islands < this.islands.Count; i_islands++)
		{
			TileIsland tIsland = this.islands[i_islands];
			if (tIsland.dirty)
			{
				this.islands.RemoveAt(i_islands);
				using (HashSet<WorldTile>.Enumerator enumerator = tIsland.tiles_roads.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						WorldTile worldTile = enumerator.Current;
						worldTile.road_island = null;
					}
					continue;
				}
			}
		}
		foreach (WorldTile tTile in this.hashset)
		{
			if (tTile.road_island == null)
			{
				int num = this.current_island;
				this.current_island = num + 1;
				TileIsland tIsland = new TileIsland(num);
				this.islands.Add(tIsland);
				this.CalcIsland(tTile, tIsland);
			}
		}
	}

	// Token: 0x060023F7 RID: 9207 RVA: 0x0012C268 File Offset: 0x0012A468
	private void CalcIsland(WorldTile pTile, TileIsland pIsland)
	{
		List<WorldTile> nextWave = new List<WorldTile>
		{
			pTile
		};
		while (nextWave.Count > 0)
		{
			WorldTile tTile = nextWave[0];
			nextWave.RemoveAt(0);
			tTile.road_island = pIsland;
			pIsland.tiles_roads.Add(tTile);
			for (int i = 0; i < tTile.neighboursAll.Length; i++)
			{
				WorldTile tNeighbour = tTile.neighboursAll[i];
				if (tNeighbour.road_island == null && tNeighbour.Type.road)
				{
					tNeighbour.road_island = pIsland;
					nextWave.Add(tNeighbour);
				}
			}
		}
	}

	// Token: 0x040019E6 RID: 6630
	public List<TileIsland> islands;

	// Token: 0x040019E7 RID: 6631
	private int current_island;

	// Token: 0x040019E8 RID: 6632
	private bool dirty = true;
}
