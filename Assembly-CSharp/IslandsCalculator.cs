using System;
using System.Collections.Generic;

// Token: 0x02000419 RID: 1049
public class IslandsCalculator
{
	// Token: 0x0600241D RID: 9245 RVA: 0x0012CF09 File Offset: 0x0012B109
	public void prepareCalc()
	{
		this._dirty_islands.Clear();
	}

	// Token: 0x0600241E RID: 9246 RVA: 0x0012CF16 File Offset: 0x0012B116
	public void makeDirty(TileIsland pIsland)
	{
		this._dirty_islands.Add(pIsland);
	}

	// Token: 0x0600241F RID: 9247 RVA: 0x0012CF28 File Offset: 0x0012B128
	public void clearDirty()
	{
		using (ListPool<TileIsland> tCurIslands = this.islands)
		{
			this.islands = new ListPool<TileIsland>(tCurIslands.Count);
			foreach (TileIsland tileIsland in this._dirty_islands)
			{
				tileIsland.clearRegionsFromIsland();
				tileIsland.insideRegionEdges.Clear();
				foreach (TileIsland tileIsland2 in tileIsland.getConnectedIslands())
				{
					tileIsland2.setDirtyIslandNeighbours();
					MapChunkManager.m_dirtyIslands++;
				}
			}
			for (int i = 0; i < tCurIslands.Count; i++)
			{
				TileIsland tIsland = tCurIslands[i];
				if (tIsland.removed)
				{
					tIsland.reset();
					this._island_pool.Push(tIsland);
				}
				else
				{
					this.islands.Add(tIsland);
				}
			}
		}
	}

	// Token: 0x06002420 RID: 9248 RVA: 0x0012D044 File Offset: 0x0012B244
	public void clear()
	{
		this._last_island_id = 0;
		ListPool<TileIsland> tIslands = this.islands;
		for (int i = 0; i < tIslands.Count; i++)
		{
			TileIsland tIsland = tIslands[i];
			tIsland.reset();
			this._island_pool.Push(tIsland);
		}
		this._dirty_islands.Clear();
		this.islands.Clear();
		this.islands_ground.Clear();
		this._wave.Clear();
		this._temp_regions.Clear();
		this._temp_regions_cur_wave.Clear();
		this._temp_regions_next_wave.Clear();
	}

	// Token: 0x06002421 RID: 9249 RVA: 0x0012D0D8 File Offset: 0x0012B2D8
	public WorldTile tryGetRandomGround()
	{
		WorldTile tTile = null;
		if (this.islands.Count > 0)
		{
			TileIsland tIsland = this.getRandomIslandGround(true);
			if (tIsland != null && tIsland.regions.Count > 0)
			{
				tTile = tIsland.getRandomTile();
			}
		}
		if (tTile == null)
		{
			tTile = World.world.tiles_list.GetRandom<WorldTile>();
		}
		return tTile;
	}

	// Token: 0x06002422 RID: 9250 RVA: 0x0012D129 File Offset: 0x0012B329
	internal bool hasGround()
	{
		return this.islands_ground.Count > 0;
	}

	// Token: 0x06002423 RID: 9251 RVA: 0x0012D139 File Offset: 0x0012B339
	internal bool hasNonGround()
	{
		return this.islands.Count > this.islands_ground.Count;
	}

	// Token: 0x06002424 RID: 9252 RVA: 0x0012D153 File Offset: 0x0012B353
	internal float groundIslandRatio()
	{
		if (this.islands.Count == 0)
		{
			return 0f;
		}
		return (float)this.islands_ground.Count / (float)this.islands.Count;
	}

	// Token: 0x06002425 RID: 9253 RVA: 0x0012D184 File Offset: 0x0012B384
	internal float realGroundRatio()
	{
		if (!this.hasNonGround())
		{
			return 1f;
		}
		if (!this.hasGround())
		{
			return 0f;
		}
		int tGroundTiles = 0;
		int tNonGroundTiles = 0;
		foreach (TileIsland ptr in this.islands)
		{
			TileIsland tIsland = ptr;
			if (tIsland.type == TileLayerType.Ground)
			{
				tGroundTiles += tIsland.getTileCount();
			}
			else
			{
				tNonGroundTiles += tIsland.getTileCount();
			}
		}
		if (tGroundTiles == 0)
		{
			return 0f;
		}
		if (tNonGroundTiles == 0)
		{
			return 1f;
		}
		return (float)tGroundTiles / (float)(tGroundTiles + tNonGroundTiles);
	}

	// Token: 0x06002426 RID: 9254 RVA: 0x0012D228 File Offset: 0x0012B428
	internal TileIsland getRandomIslandGroundWeighted(bool pMinRegions = true)
	{
		if (this.islands_ground.Count == 0)
		{
			return null;
		}
		int tRegions = 0;
		for (int i = 0; i < this.islands_ground.Count; i++)
		{
			TileIsland tIsland = this.islands_ground[i];
			if (!pMinRegions || tIsland.regions.Count >= 4)
			{
				tRegions += tIsland.regions.Count;
			}
		}
		if (tRegions == 0)
		{
			return null;
		}
		TileIsland random;
		using (ListPool<TileIsland> tTileIslands = new ListPool<TileIsland>(tRegions))
		{
			for (int j = 0; j < this.islands_ground.Count; j++)
			{
				TileIsland tIsland2 = this.islands_ground[j];
				if (!pMinRegions || tIsland2.regions.Count >= 4)
				{
					tTileIslands.AddTimes(tIsland2.regions.Count, tIsland2);
				}
			}
			random = tTileIslands.GetRandom<TileIsland>();
		}
		return random;
	}

	// Token: 0x06002427 RID: 9255 RVA: 0x0012D30C File Offset: 0x0012B50C
	internal TileIsland getRandomIslandGround(bool pMinRegions = true)
	{
		if (this.islands_ground.Count == 0)
		{
			return null;
		}
		if (!pMinRegions)
		{
			return this.islands_ground.GetRandom<TileIsland>();
		}
		foreach (TileIsland tIsland in this.islands_ground.LoopRandom<TileIsland>())
		{
			if (tIsland.regions.Count >= 4)
			{
				return tIsland;
			}
		}
		return null;
	}

	// Token: 0x06002428 RID: 9256 RVA: 0x0012D38C File Offset: 0x0012B58C
	internal TileIsland getRandomIslandNonGroundWeighted(bool pMinRegions = true)
	{
		if (this.islands.Count == 0)
		{
			return null;
		}
		if (this.islands_ground.Count == this.islands.Count)
		{
			return null;
		}
		int tRegions = 0;
		for (int i = 0; i < this.islands.Count; i++)
		{
			TileIsland tIsland = this.islands[i];
			if (tIsland.type != TileLayerType.Ground && (!pMinRegions || tIsland.regions.Count >= 4))
			{
				tRegions += tIsland.regions.Count;
			}
		}
		if (tRegions == 0)
		{
			return null;
		}
		TileIsland random;
		using (ListPool<TileIsland> tTileIslands = new ListPool<TileIsland>(tRegions))
		{
			for (int j = 0; j < this.islands.Count; j++)
			{
				TileIsland tIsland2 = this.islands[j];
				if (tIsland2.type != TileLayerType.Ground && (!pMinRegions || tIsland2.regions.Count >= 4))
				{
					tTileIslands.AddTimes(tIsland2.regions.Count, tIsland2);
				}
			}
			random = tTileIslands.GetRandom<TileIsland>();
		}
		return random;
	}

	// Token: 0x06002429 RID: 9257 RVA: 0x0012D49C File Offset: 0x0012B69C
	internal TileIsland getRandomIslandNonGround(bool pMinRegions = true)
	{
		if (this.islands.Count == 0)
		{
			return null;
		}
		if (this.islands_ground.Count == this.islands.Count)
		{
			return null;
		}
		foreach (TileIsland tIsland in this.islands.LoopRandom<TileIsland>())
		{
			if (tIsland.type != TileLayerType.Ground && (!pMinRegions || tIsland.regions.Count >= 4))
			{
				return tIsland;
			}
		}
		return null;
	}

	// Token: 0x0600242A RID: 9258 RVA: 0x0012D534 File Offset: 0x0012B734
	public int countLandIslands()
	{
		int tResult = 0;
		for (int i = 0; i < this.islands.Count; i++)
		{
			TileIsland tIsland = this.islands[i];
			if (tIsland.type == TileLayerType.Ground && tIsland.regions.Count >= 4)
			{
				tResult++;
			}
		}
		return tResult;
	}

	// Token: 0x0600242B RID: 9259 RVA: 0x0012D584 File Offset: 0x0012B784
	internal void recalcActors()
	{
		ListPool<TileIsland> tIslands = this.islands;
		for (int i = 0; i < tIslands.Count; i++)
		{
			tIslands[i].actors.Clear();
		}
		List<Actor> tList = World.world.units.getSimpleList();
		for (int j = 0; j < tList.Count; j++)
		{
			Actor tActor = tList[j];
			if (tActor.isAlive())
			{
				tActor.current_tile.region.island.actors.Add(tActor);
			}
		}
	}

	// Token: 0x0600242C RID: 9260 RVA: 0x0012D60C File Offset: 0x0012B80C
	private void clearCaches()
	{
		for (int i = 0; i < this.islands.Count; i++)
		{
			this.islands[i].clearCache();
		}
	}

	// Token: 0x0600242D RID: 9261 RVA: 0x0012D640 File Offset: 0x0012B840
	public void findIslands(ListPool<TileIsland> pNewIslands)
	{
		Bench.bench("find_islands_prepare", "chunks", false);
		this._temp_regions.Clear();
		this.islands_ground.Clear();
		this.clearCaches();
		Bench.benchEnd("find_islands_prepare", "chunks", false, 0L, false);
		Bench.bench("find_islands_temp_regions", "chunks", false);
		MapChunk[] tChunks = World.world.map_chunk_manager.chunks;
		int tLen = tChunks.Length;
		for (int i = 0; i < tLen; i++)
		{
			MapChunk tChunk = tChunks[i];
			for (int j = 0; j < tChunk.regions.Count; j++)
			{
				MapRegion tRegion = tChunk.regions[j];
				if (tRegion.island == null)
				{
					tRegion.is_island_checked = false;
					this._temp_regions.Add(tRegion);
				}
			}
		}
		Bench.benchEnd("find_islands_temp_regions", "chunks", true, (long)this._temp_regions.Count, false);
		Bench.bench("find_islands_new_islands", "chunks", false);
		for (int k = 0; k < this._temp_regions.Count; k++)
		{
			MapRegion tRegion2 = this._temp_regions[k];
			if (tRegion2.island == null)
			{
				TileIsland tIsland = this.newIslandFrom(tRegion2);
				pNewIslands.Add(tIsland);
				MapChunkManager.m_newIslands++;
			}
		}
		Bench.benchEnd("find_islands_new_islands", "chunks", true, (long)MapChunkManager.m_newIslands, false);
		Bench.bench("find_islands_fin", "chunks", false);
		for (int l = 0; l < this.islands.Count; l++)
		{
			TileIsland tIsland2 = this.islands[l];
			tIsland2.countTiles();
			if (tIsland2.type == TileLayerType.Ground)
			{
				this.islands_ground.Add(tIsland2);
			}
		}
		Bench.benchEnd("find_islands_fin", "chunks", false, 0L, false);
	}

	// Token: 0x0600242E RID: 9262 RVA: 0x0012D810 File Offset: 0x0012BA10
	private TileIsland newIslandFrom(MapRegion pRegion)
	{
		this._temp_regions_cur_wave.Clear();
		this._temp_regions_next_wave.Clear();
		TileIsland tIsland;
		if (this._island_pool.Count > 0)
		{
			tIsland = this._island_pool.Pop();
		}
		else
		{
			tIsland = new TileIsland(this._last_island_id);
			this._last_island_id++;
		}
		tIsland.reset();
		tIsland.type = pRegion.type;
		this.islands.Add(tIsland);
		this._temp_regions_next_wave.Add(pRegion);
		this._wave.Clear();
		this._wave.Enqueue(pRegion);
		this.startFill(tIsland);
		return tIsland;
	}

	// Token: 0x0600242F RID: 9263 RVA: 0x0012D8B4 File Offset: 0x0012BAB4
	private void startFill(TileIsland pIsland)
	{
		while (this._wave.Count > 0)
		{
			MapRegion tCurRegion = this._wave.Dequeue();
			if (!tCurRegion.is_island_checked)
			{
				tCurRegion.island = pIsland;
				pIsland.addRegion(tCurRegion);
			}
			tCurRegion.is_island_checked = true;
			for (int i = 0; i < tCurRegion.neighbours.Count; i++)
			{
				MapRegion tNeighbour = tCurRegion.neighbours[i];
				if (!tNeighbour.is_island_checked)
				{
					tNeighbour.is_island_checked = true;
					tNeighbour.island = pIsland;
					pIsland.addRegion(tNeighbour);
					this._wave.Enqueue(tNeighbour);
				}
			}
		}
		pIsland.regions.checkAddRemove();
	}

	// Token: 0x04001A02 RID: 6658
	private float _timer_update_actors;

	// Token: 0x04001A03 RID: 6659
	public ListPool<TileIsland> islands = new ListPool<TileIsland>();

	// Token: 0x04001A04 RID: 6660
	public readonly List<TileIsland> islands_ground = new List<TileIsland>();

	// Token: 0x04001A05 RID: 6661
	private readonly List<MapRegion> _temp_regions = new List<MapRegion>();

	// Token: 0x04001A06 RID: 6662
	private readonly List<MapRegion> _temp_regions_cur_wave = new List<MapRegion>();

	// Token: 0x04001A07 RID: 6663
	private readonly List<MapRegion> _temp_regions_next_wave = new List<MapRegion>();

	// Token: 0x04001A08 RID: 6664
	private int _last_island_id;

	// Token: 0x04001A09 RID: 6665
	private readonly HashSet<TileIsland> _dirty_islands = new HashSet<TileIsland>();

	// Token: 0x04001A0A RID: 6666
	private readonly Queue<MapRegion> _wave = new Queue<MapRegion>();

	// Token: 0x04001A0B RID: 6667
	private readonly Stack<TileIsland> _island_pool = new Stack<TileIsland>();
}
