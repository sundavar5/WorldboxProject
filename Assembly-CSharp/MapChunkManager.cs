using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

// Token: 0x0200041C RID: 1052
public class MapChunkManager
{
	// Token: 0x170001FA RID: 506
	// (get) Token: 0x06002450 RID: 9296 RVA: 0x0012E70B File Offset: 0x0012C90B
	public int amount_x
	{
		get
		{
			return this._get_amount_x;
		}
	}

	// Token: 0x06002451 RID: 9297 RVA: 0x0012E713 File Offset: 0x0012C913
	public void checkDiagnosticRegions()
	{
		this.diagnosticRegions();
	}

	// Token: 0x06002452 RID: 9298 RVA: 0x0012E71B File Offset: 0x0012C91B
	public void update(float pElapsed, bool pForce = false)
	{
		if (this._timer > 0f && !pForce)
		{
			this._timer -= pElapsed;
			return;
		}
		this.updateDirty();
	}

	// Token: 0x06002453 RID: 9299 RVA: 0x0012E744 File Offset: 0x0012C944
	private void diagnosticRegions()
	{
		HashSet<MapRegion> tRegionMain = new HashSet<MapRegion>();
		HashSet<MapRegion> tRegionNeighbour = new HashSet<MapRegion>();
		MapChunk[] tChunks = this.chunks;
		int tLen = tChunks.Length;
		for (int i = 0; i < tLen; i++)
		{
			foreach (MapRegion tReg in tChunks[i].regions)
			{
				if (tReg.tiles.Count == 0 || tReg.chunk == null)
				{
					tRegionMain.Add(tReg);
				}
				foreach (MapRegion tRegNeighbour in tReg.neighbours)
				{
					if (tRegNeighbour.tiles.Count == 0 || tRegNeighbour.chunk == null)
					{
						tRegionNeighbour.Add(tReg);
					}
				}
			}
		}
		if (tRegionMain.Count > 0 || tRegionNeighbour.Count > 0)
		{
			Debug.LogError("Something is wrong with regions");
			Debug.LogError("tRegionMain: " + tRegionMain.Count.ToString());
			Debug.LogError("tRegionNeighbour: " + tRegionNeighbour.Count.ToString());
		}
	}

	// Token: 0x06002454 RID: 9300 RVA: 0x0012E89C File Offset: 0x0012CA9C
	public void prepare()
	{
		int tMod = 4;
		this._get_amount_x = Config.ZONE_AMOUNT_X * tMod;
		this._amount_y = Config.ZONE_AMOUNT_Y * tMod;
		this._map = new MapChunk[this._get_amount_x, this._amount_y];
		int tLen = this._get_amount_x * this._amount_y;
		if (tLen != this.chunks.Length)
		{
			this.chunks = new MapChunk[tLen];
		}
		else
		{
			Array.Clear(this.chunks, 0, this.chunks.Length);
		}
		int tId = 0;
		int iZone = 0;
		for (int yy = 0; yy < this._amount_y; yy++)
		{
			for (int xx = 0; xx < this._get_amount_x; xx++)
			{
				MapChunk tChunk = new MapChunk();
				tChunk.id = tId++;
				tChunk.x = xx;
				tChunk.y = yy;
				this._map[xx, yy] = tChunk;
				if ((xx + yy) % 2 == 0)
				{
					tChunk.color = this._color_1_gray;
				}
				else
				{
					tChunk.color = this._color_2_white;
				}
				this.chunks[iZone] = tChunk;
				this.fill(tChunk);
				iZone++;
			}
		}
		this.fillAndLinkTileZones();
		this.generateNeighbours();
		this.generateEdgeConnections();
	}

	// Token: 0x06002455 RID: 9301 RVA: 0x0012E9D4 File Offset: 0x0012CBD4
	private void generateEdgeConnections()
	{
		MapChunk[] tChunks = this.chunks;
		int tLen = tChunks.Length;
		for (int i = 0; i < tLen; i++)
		{
			tChunks[i].generateEdgeConnections();
		}
	}

	// Token: 0x06002456 RID: 9302 RVA: 0x0012EA00 File Offset: 0x0012CC00
	private void fillAndLinkTileZones()
	{
		for (int i = 0; i < World.world.zone_calculator.zones.Count; i++)
		{
			TileZone tZone = World.world.zone_calculator.zones[i];
			tZone.chunk.zones.Add(tZone);
		}
	}

	// Token: 0x06002457 RID: 9303 RVA: 0x0012EA54 File Offset: 0x0012CC54
	private void fill(MapChunk pChunk)
	{
		int tChunkActual = 16;
		int tStartX = pChunk.x * tChunkActual;
		int tStartY = pChunk.y * tChunkActual;
		for (int xx = 0; xx < tChunkActual; xx++)
		{
			for (int yy = 0; yy < tChunkActual; yy++)
			{
				WorldTile tTile = World.world.GetTileSimple(xx + tStartX, yy + tStartY);
				tTile.chunk = pChunk;
				pChunk.addTile(tTile, xx, yy);
			}
		}
		pChunk.world_center_x = (float)(tStartX + 8);
		pChunk.world_center_y = (float)(tStartY + 8);
		for (int xx2 = 0; xx2 < 16; xx2++)
		{
			WorldTile tTile = World.world.GetTileSimple(xx2 + tStartX, tStartY);
			pChunk.bounds_down.Add(tTile);
		}
		for (int yy2 = 0; yy2 < 16; yy2++)
		{
			WorldTile tTile = World.world.GetTileSimple(tStartX, tStartY + yy2);
			pChunk.bounds_left.Add(tTile);
		}
		for (int yy3 = 0; yy3 < 16; yy3++)
		{
			WorldTile tTile = World.world.GetTileSimple(tStartX + 16 - 1, tStartY + yy3);
			pChunk.bounds_right.Add(tTile);
		}
		for (int xx3 = 0; xx3 < 16; xx3++)
		{
			WorldTile tTile = World.world.GetTileSimple(xx3 + tStartX, tStartY + 16 - 1);
			pChunk.bounds_up.Add(tTile);
		}
		pChunk.edge_up_left = pChunk.bounds_up[0];
		pChunk.edge_up_right = pChunk.bounds_up[15];
		pChunk.edge_down_left = pChunk.bounds_down[0];
		pChunk.edge_down_right = pChunk.bounds_down[15];
		pChunk.combineEdges();
	}

	// Token: 0x06002458 RID: 9304 RVA: 0x0012EBE0 File Offset: 0x0012CDE0
	private void generateNeighbours()
	{
		MapChunk[] tChunks = this.chunks;
		int tLen = tChunks.Length;
		using (ListPool<MapChunk> tNeighbours = new ListPool<MapChunk>(4))
		{
			using (ListPool<MapChunk> tNeighboursAll = new ListPool<MapChunk>(8))
			{
				for (int i = 0; i < tLen; i++)
				{
					MapChunk tObj = tChunks[i];
					MapChunk tNeighbour = this.get(tObj.x - 1, tObj.y);
					tObj.addNeighbour(tNeighbour, TileDirection.Left, tNeighbours, tNeighboursAll, false);
					tNeighbour = this.get(tObj.x + 1, tObj.y);
					tObj.addNeighbour(tNeighbour, TileDirection.Right, tNeighbours, tNeighboursAll, false);
					tNeighbour = this.get(tObj.x, tObj.y - 1);
					tObj.addNeighbour(tNeighbour, TileDirection.Down, tNeighbours, tNeighboursAll, false);
					tNeighbour = this.get(tObj.x, tObj.y + 1);
					tObj.addNeighbour(tNeighbour, TileDirection.Up, tNeighbours, tNeighboursAll, false);
					tNeighbour = this.get(tObj.x - 1, tObj.y - 1);
					tObj.addNeighbour(tNeighbour, TileDirection.Null, tNeighbours, tNeighboursAll, true);
					tNeighbour = this.get(tObj.x - 1, tObj.y + 1);
					tObj.addNeighbour(tNeighbour, TileDirection.Null, tNeighbours, tNeighboursAll, true);
					tNeighbour = this.get(tObj.x + 1, tObj.y - 1);
					tObj.addNeighbour(tNeighbour, TileDirection.Null, tNeighbours, tNeighboursAll, true);
					tNeighbour = this.get(tObj.x + 1, tObj.y + 1);
					tObj.addNeighbour(tNeighbour, TileDirection.Null, tNeighbours, tNeighboursAll, true);
					tObj.neighbours = tNeighbours.ToArray<MapChunk>();
					tObj.neighbours_all = tNeighboursAll.ToArray<MapChunk>();
					tNeighbours.Clear();
					tNeighboursAll.Clear();
				}
			}
		}
	}

	// Token: 0x06002459 RID: 9305 RVA: 0x0012EDC4 File Offset: 0x0012CFC4
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public MapChunk get(int pX, int pY)
	{
		if (pX < 0 || pX >= this._get_amount_x || pY < 0 || pY >= this._amount_y)
		{
			return null;
		}
		return this._map[pX, pY];
	}

	// Token: 0x0600245A RID: 9306 RVA: 0x0012EDF0 File Offset: 0x0012CFF0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public MapChunk get(ref Vector2Int pPos)
	{
		if (pPos.x < 0 || pPos.x >= this._get_amount_x || pPos.y < 0 || pPos.y >= this._amount_y)
		{
			return null;
		}
		return this._map[pPos.x, pPos.y];
	}

	// Token: 0x0600245B RID: 9307 RVA: 0x0012EE44 File Offset: 0x0012D044
	public MapChunk getByID(int pID)
	{
		return this.chunks[pID];
	}

	// Token: 0x0600245C RID: 9308 RVA: 0x0012EE50 File Offset: 0x0012D050
	public void clearAll()
	{
		World.world.islands_calculator.clear();
		this._dirty_chunks_links.Clear();
		this._dirty_chunks_regions.Clear();
		int tLen = this.chunks.Length;
		for (int i = 0; i < tLen; i++)
		{
			this.chunks[i].clearRegions();
		}
	}

	// Token: 0x0600245D RID: 9309 RVA: 0x0012EEA4 File Offset: 0x0012D0A4
	public void clean()
	{
		this.clearAll();
		MapChunk[] tChunks = this.chunks;
		int tLen = tChunks.Length;
		for (int i = 0; i < tLen; i++)
		{
			tChunks[i].Dispose();
		}
		Array.Clear(this.chunks, 0, this.chunks.Length);
	}

	// Token: 0x0600245E RID: 9310 RVA: 0x0012EEEC File Offset: 0x0012D0EC
	public void setAllLinksDirty()
	{
		foreach (MapChunk tChunk in this.chunks)
		{
			this.setDirty(tChunk, false, true);
		}
	}

	// Token: 0x0600245F RID: 9311 RVA: 0x0012EF1B File Offset: 0x0012D11B
	public void setDirty(MapChunk pChunk, bool pRegions = true, bool pLinks = true)
	{
		if (pRegions && !pChunk.dirty_regions)
		{
			pChunk.dirty_regions = true;
			this._dirty_chunks_regions.Add(pChunk);
		}
		if (pLinks && !pChunk.dirty_links)
		{
			pChunk.dirty_links = true;
			this._dirty_chunks_links.Add(pChunk);
		}
	}

	// Token: 0x06002460 RID: 9312 RVA: 0x0012EF5C File Offset: 0x0012D15C
	public void allDirty()
	{
		this._dirty_chunks_links.Clear();
		this._dirty_chunks_regions.Clear();
		MapChunk[] tChunks = this.chunks;
		int tLen = tChunks.Length;
		for (int i = 0; i < tLen; i++)
		{
			MapChunk mapChunk = tChunks[i];
			mapChunk.dirty_links = true;
			mapChunk.dirty_regions = true;
		}
		this._dirty_chunks_links.AddRange(this.chunks);
		this._dirty_chunks_regions.AddRange(this.chunks);
	}

	// Token: 0x06002461 RID: 9313 RVA: 0x0012EFC8 File Offset: 0x0012D1C8
	private bool isAllChunksDirty()
	{
		return this.chunks.Length == this._dirty_chunks_regions.Count;
	}

	// Token: 0x06002462 RID: 9314 RVA: 0x0012EFE0 File Offset: 0x0012D1E0
	private void updateDirty()
	{
		if (!DebugConfig.isOn(DebugOption.SystemUpdateDirtyChunks))
		{
			return;
		}
		if (!this.isAllChunksDirty() && World.world.isActionHappening())
		{
			return;
		}
		if (this._dirty_chunks_links.Count == 0 && this._dirty_chunks_regions.Count == 0)
		{
			return;
		}
		Bench.bench("chunks", "chunks_total", false);
		this._timer = 0.4f;
		this.m_dirtyChunks = this._dirty_chunks_regions.Count;
		MapChunkManager.m_newRegions = 0;
		MapChunkManager.m_newLinks = 0;
		MapChunkManager.m_newIslands = 0;
		MapChunkManager.m_dirtyIslands = 0;
		MapChunkManager.m_dirtyCorners = 0;
		MapRegion.created_time_last = Time.time;
		World.world.islands_calculator.prepareCalc();
		Bench.bench("clear_regions", "chunks", false);
		this.calc_clearRegions();
		Bench.benchEnd("clear_regions", "chunks", true, (long)this._dirty_chunks_links.Count, false);
		Bench.bench("clear_dirty_islands", "chunks", false);
		World.world.islands_calculator.clearDirty();
		Bench.benchEnd("clear_dirty_islands", "chunks", true, (long)this._dirty_chunks_links.Count, false);
		Bench.bench("P calc_regions", "chunks", false);
		this.calc_regions();
		Bench.benchEnd("P calc_regions", "chunks", true, (long)this._dirty_chunks_regions.Count, false);
		Bench.bench("shuffle_region_tiles", "chunks", false);
		this.calc_shuffleRegionTiles();
		Bench.benchEnd("shuffle_region_tiles", "chunks", true, (long)MapChunkManager.m_newRegions, false);
		Bench.bench("P calc_links", "chunks", false);
		this.calc_links();
		Bench.benchEnd("P calc_links", "chunks", true, (long)this._dirty_chunks_links.Count, false);
		Bench.bench("create_links", "chunks", false);
		this.calc_checkLinkResults();
		Bench.benchEnd("create_links", "chunks", true, (long)this._dirty_chunks_links.Count, false);
		Bench.bench("P calc_linked_regions", "chunks", false);
		this.calc_linkedRegions();
		Bench.benchEnd("P calc_linked_regions", "chunks", true, (long)this._dirty_chunks_links.Count, false);
		using (ListPool<TileIsland> tNewIslands = new ListPool<TileIsland>())
		{
			World.world.islands_calculator.findIslands(tNewIslands);
			Bench.bench("tile_corners_prepare", "chunks", false);
			this.calc_tileCornersPrepare(this._region_set);
			MapChunkManager.m_dirtyCorners = this._region_set.Count;
			Bench.benchEnd("tile_corners_prepare", "chunks", true, (long)this._dirty_chunks_links.Count, false);
			Bench.bench("center_regions", "chunks", false);
			this.calc_centerRegions();
			Bench.benchEnd("center_regions", "chunks", true, (long)this._region_set.Count, false);
			Bench.bench("island_region_edges", "chunks", false);
			int tCountIslandCorners = this.calc_islandRegionEdges(tNewIslands);
			Bench.benchEnd("island_region_edges", "chunks", true, (long)tCountIslandCorners, false);
			Bench.bench("PH tile_edges", "chunks", false);
			this.calc_tileEdges();
			Bench.benchEnd("PH tile_edges", "chunks", true, (long)this._region_set.Count, false);
			Bench.bench("prepare_d_neighbour_islands", "chunks", false);
			this.prepareDirtyNeighbourIslands();
			Bench.benchEnd("prepare_d_neighbour_islands", "chunks", true, (long)this._temp_dirty_neighbours_islands.Count, false);
			Bench.bench("P neighbour_islands", "chunks", false);
			this.calc_neighbourIslands();
			Bench.benchEnd("P neighbour_islands", "chunks", true, (long)this._temp_dirty_neighbours_islands.Count, false);
			Bench.bench("clear_end", "chunks", false);
			this._region_set.Clear();
			this._dirty_chunks_links.Clear();
			this._dirty_chunks_regions.Clear();
			World.world.city_zone_helper.city_place_finder.setDirty();
			World.world.region_path_finder.clearCache();
			Bench.benchEnd("clear_end", "chunks", false, 0L, false);
			Bench.benchSetValue("m_dirtyChunks", this.m_dirtyChunks, "chunks");
			Bench.benchSetValue("m_newRegions", MapChunkManager.m_newRegions, "chunks");
			Bench.benchSetValue("m_newLinks", MapChunkManager.m_newLinks, "chunks");
			Bench.benchSetValue("m_newIslands", MapChunkManager.m_newIslands, "chunks");
			Bench.benchSetValue("m_dirtyIslands", MapChunkManager.m_dirtyIslands, "chunks");
			Bench.benchSetValue("m_dirtyCorners", MapChunkManager.m_dirtyCorners, "chunks");
			Bench.benchSetValue("m_dirtyIslandNeighb", this._temp_dirty_neighbours_islands.Count, "chunks");
			Bench.benchEnd("chunks", "chunks_total", false, 0L, false);
		}
	}

	// Token: 0x06002463 RID: 9315 RVA: 0x0012F49C File Offset: 0x0012D69C
	private void calc_clearRegions()
	{
		List<MapChunk> tDirtyRegionsList = this._dirty_chunks_regions;
		int tCount = tDirtyRegionsList.Count;
		for (int i = 0; i < tCount; i++)
		{
			tDirtyRegionsList[i].clearRegions();
		}
		List<MapChunk> tDirtyLinksList = this._dirty_chunks_links;
		int tCount2 = tDirtyLinksList.Count;
		for (int j = 0; j < tCount2; j++)
		{
			tDirtyLinksList[j].clearIsland();
		}
	}

	// Token: 0x06002464 RID: 9316 RVA: 0x0012F500 File Offset: 0x0012D700
	private void calc_regions()
	{
		if (!this._is_parallel_enabled)
		{
			List<MapChunk> tDirtyChunks = this._dirty_chunks_regions;
			int tCount = tDirtyChunks.Count;
			for (int i = 0; i < tCount; i++)
			{
				tDirtyChunks[i].calculateRegions();
			}
			return;
		}
		MapChunkManager.<>c__DisplayClass39_0 CS$<>8__locals1 = new MapChunkManager.<>c__DisplayClass39_0();
		CS$<>8__locals1.tDirtyChunks = this._dirty_chunks_regions;
		CS$<>8__locals1.tCount = CS$<>8__locals1.tDirtyChunks.Count;
		if (!this._batches_enabled)
		{
			Parallel.For(0, CS$<>8__locals1.tCount, World.world.parallel_options, delegate(int pIndex)
			{
				CS$<>8__locals1.tDirtyChunks[pIndex].calculateRegions();
			});
			return;
		}
		int tDynamicBatchSize = ParallelHelper.getDynamicBatchSize(CS$<>8__locals1.tCount);
		int tTotalBatches = ParallelHelper.calcTotalBatches(CS$<>8__locals1.tCount, tDynamicBatchSize);
		Parallel.For(0, tTotalBatches, World.world.parallel_options, delegate(int pBatchIndex)
		{
			int num = ParallelHelper.calculateBatchBeg(pBatchIndex, tDynamicBatchSize);
			int tIndexEnd = ParallelHelper.calculateBatchEnd(num, tDynamicBatchSize, CS$<>8__locals1.tCount);
			for (int j = num; j < tIndexEnd; j++)
			{
				CS$<>8__locals1.tDirtyChunks[j].calculateRegions();
			}
		});
	}

	// Token: 0x06002465 RID: 9317 RVA: 0x0012F5F0 File Offset: 0x0012D7F0
	private void calc_shuffleRegionTiles()
	{
		List<MapChunk> tDirtyChunks = this._dirty_chunks_regions;
		int tCount = tDirtyChunks.Count;
		for (int i = 0; i < tCount; i++)
		{
			MapChunk tChunk = tDirtyChunks[i];
			MapChunkManager.m_newRegions += tChunk.regions.Count;
			tChunk.shuffleRegionTiles();
		}
	}

	// Token: 0x06002466 RID: 9318 RVA: 0x0012F63C File Offset: 0x0012D83C
	private void calc_links()
	{
		if (!this._is_parallel_enabled)
		{
			List<MapChunk> tDirtyChunks = this._dirty_chunks_links;
			int tCount = tDirtyChunks.Count;
			for (int i = 0; i < tCount; i++)
			{
				tDirtyChunks[i].calculateLinks();
			}
			return;
		}
		MapChunkManager.<>c__DisplayClass41_0 CS$<>8__locals1 = new MapChunkManager.<>c__DisplayClass41_0();
		CS$<>8__locals1.tDirtyChunks = this._dirty_chunks_links;
		CS$<>8__locals1.tCount = CS$<>8__locals1.tDirtyChunks.Count;
		if (!this._batches_enabled)
		{
			Parallel.For(0, CS$<>8__locals1.tCount, World.world.parallel_options, delegate(int pIndex)
			{
				CS$<>8__locals1.tDirtyChunks[pIndex].calculateLinks();
			});
			return;
		}
		int tDynamicBatchSize = ParallelHelper.getDynamicBatchSize(CS$<>8__locals1.tCount);
		int tTotalBatches = ParallelHelper.calcTotalBatches(CS$<>8__locals1.tCount, tDynamicBatchSize);
		Parallel.For(0, tTotalBatches, World.world.parallel_options, delegate(int pBatchIndex)
		{
			int num = ParallelHelper.calculateBatchBeg(pBatchIndex, tDynamicBatchSize);
			int tIndexEnd = ParallelHelper.calculateBatchEnd(num, tDynamicBatchSize, CS$<>8__locals1.tCount);
			for (int j = num; j < tIndexEnd; j++)
			{
				CS$<>8__locals1.tDirtyChunks[j].calculateLinks();
			}
		});
	}

	// Token: 0x06002467 RID: 9319 RVA: 0x0012F72C File Offset: 0x0012D92C
	private void calc_checkLinkResults()
	{
		List<MapChunk> tDirtyChunks = this._dirty_chunks_links;
		int tCount = tDirtyChunks.Count;
		for (int i = 0; i < tCount; i++)
		{
			tDirtyChunks[i].checkLinksResults();
		}
	}

	// Token: 0x06002468 RID: 9320 RVA: 0x0012F760 File Offset: 0x0012D960
	private void calc_linkedRegions()
	{
		MapChunkManager.<>c__DisplayClass43_0 CS$<>8__locals1 = new MapChunkManager.<>c__DisplayClass43_0();
		CS$<>8__locals1.<>4__this = this;
		if (!this._is_parallel_enabled)
		{
			List<MapChunk> tDirtyChunks = this._dirty_chunks_links;
			int tCount = tDirtyChunks.Count;
			for (int i = 0; i < tCount; i++)
			{
				MapChunk tChunk = tDirtyChunks[i];
				this.calculateRegionNeighbours(tChunk);
			}
			return;
		}
		CS$<>8__locals1.tDirtyChunks = this._dirty_chunks_links;
		CS$<>8__locals1.tCount = CS$<>8__locals1.tDirtyChunks.Count;
		if (!this._batches_enabled)
		{
			Parallel.For(0, CS$<>8__locals1.tCount, World.world.parallel_options, delegate(int pIndex)
			{
				MapChunk tChunk2 = CS$<>8__locals1.tDirtyChunks[pIndex];
				CS$<>8__locals1.<>4__this.calculateRegionNeighbours(tChunk2);
			});
			return;
		}
		int tDynamicBatchSize = ParallelHelper.getDynamicBatchSize(CS$<>8__locals1.tCount);
		int tTotalBatches = ParallelHelper.calcTotalBatches(CS$<>8__locals1.tCount, tDynamicBatchSize);
		Parallel.For(0, tTotalBatches, World.world.parallel_options, delegate(int pBatchIndex)
		{
			int num = ParallelHelper.calculateBatchBeg(pBatchIndex, tDynamicBatchSize);
			int tIndexEnd = ParallelHelper.calculateBatchEnd(num, tDynamicBatchSize, CS$<>8__locals1.tCount);
			for (int j = num; j < tIndexEnd; j++)
			{
				MapChunk tChunk2 = CS$<>8__locals1.tDirtyChunks[j];
				CS$<>8__locals1.<>4__this.calculateRegionNeighbours(tChunk2);
			}
		});
	}

	// Token: 0x06002469 RID: 9321 RVA: 0x0012F85C File Offset: 0x0012DA5C
	private void calc_tileCornersPrepare(HashSet<MapRegion> pSetRegionsResult)
	{
		List<MapChunk> tDirtyChunks = this._dirty_chunks_links;
		int tCount = tDirtyChunks.Count;
		for (int tChunk = 0; tChunk < tCount; tChunk++)
		{
			List<MapRegion> tRegions = tDirtyChunks[tChunk].regions;
			int tCountRegions = tRegions.Count;
			for (int iRegion = 0; iRegion < tCountRegions; iRegion++)
			{
				MapRegion tReg = tRegions[iRegion];
				pSetRegionsResult.Add(tReg);
			}
		}
	}

	// Token: 0x0600246A RID: 9322 RVA: 0x0012F8BC File Offset: 0x0012DABC
	private void calc_centerRegions()
	{
		foreach (MapRegion mapRegion in this._region_set)
		{
			mapRegion.calculateCenterRegion();
		}
	}

	// Token: 0x0600246B RID: 9323 RVA: 0x0012F90C File Offset: 0x0012DB0C
	private int calc_islandRegionEdges(ListPool<TileIsland> pNewIslands)
	{
		int tCountIslandCorners = 0;
		for (int iIsland = 0; iIsland < pNewIslands.Count; iIsland++)
		{
			TileIsland tIsland = pNewIslands[iIsland];
			List<MapRegion> tList = tIsland.regions.getSimpleList();
			for (int i = 0; i < tList.Count; i++)
			{
				MapRegion tRegion = tList[i];
				tCountIslandCorners++;
				if (!tRegion.center_region)
				{
					tIsland.insideRegionEdges.Add(tRegion);
				}
			}
		}
		return tCountIslandCorners;
	}

	// Token: 0x0600246C RID: 9324 RVA: 0x0012F97C File Offset: 0x0012DB7C
	private void calc_tileEdges()
	{
		HashSet<MapRegion> tRegionSet = this._region_set;
		if (this._is_parallel_enabled)
		{
			Parallel.ForEach<MapRegion>(tRegionSet, World.world.parallel_options, delegate(MapRegion tReg)
			{
				tReg.calculateTileEdges();
			});
			return;
		}
		foreach (MapRegion mapRegion in tRegionSet)
		{
			mapRegion.calculateTileEdges();
		}
	}

	// Token: 0x0600246D RID: 9325 RVA: 0x0012FA08 File Offset: 0x0012DC08
	private void prepareDirtyNeighbourIslands()
	{
		ListPool<TileIsland> islands = World.world.islands_calculator.islands;
		List<TileIsland> tIslandsWithDirtyNeighbours = this._temp_dirty_neighbours_islands;
		tIslandsWithDirtyNeighbours.Clear();
		foreach (TileIsland ptr in islands)
		{
			TileIsland tIsland = ptr;
			if (tIsland.isDirtyNeighbours())
			{
				tIslandsWithDirtyNeighbours.Add(tIsland);
			}
		}
	}

	// Token: 0x0600246E RID: 9326 RVA: 0x0012FA7C File Offset: 0x0012DC7C
	private void calc_neighbourIslands()
	{
		MapChunkManager.<>c__DisplayClass50_0 CS$<>8__locals1 = new MapChunkManager.<>c__DisplayClass50_0();
		CS$<>8__locals1.tIslandsWithDirtyNeighbours = this._temp_dirty_neighbours_islands;
		if (!this._is_parallel_enabled)
		{
			for (int i = 0; i < CS$<>8__locals1.tIslandsWithDirtyNeighbours.Count; i++)
			{
				CS$<>8__locals1.tIslandsWithDirtyNeighbours[i].calcNeighbourIslands();
			}
			return;
		}
		int tCount = CS$<>8__locals1.tIslandsWithDirtyNeighbours.Count;
		if (!this._batches_enabled)
		{
			Parallel.For(0, tCount, World.world.parallel_options, delegate(int pIndex)
			{
				CS$<>8__locals1.tIslandsWithDirtyNeighbours[pIndex].calcNeighbourIslands();
			});
			return;
		}
		int tDynamicBatchSize = ParallelHelper.DEBUG_BATCH_SIZE;
		int tTotalBatches = ParallelHelper.calcTotalBatches(tCount, tDynamicBatchSize);
		Parallel.For(0, tTotalBatches, World.world.parallel_options, delegate(int pBatchIndex)
		{
			int num = ParallelHelper.calculateBatchBeg(pBatchIndex, tDynamicBatchSize);
			int tIndexEnd = ParallelHelper.calculateBatchEnd(num, tDynamicBatchSize, tCount);
			for (int j = num; j < tIndexEnd; j++)
			{
				CS$<>8__locals1.tIslandsWithDirtyNeighbours[j].calcNeighbourIslands();
			}
		});
	}

	// Token: 0x0600246F RID: 9327 RVA: 0x0012FB60 File Offset: 0x0012DD60
	private void checkWrongIslands()
	{
		MapChunk[] tChunks = this.chunks;
		int tLen = tChunks.Length;
		for (int i = 0; i < tLen; i++)
		{
			MapChunk tChunk = tChunks[i];
			foreach (MapRegion tRegion in tChunk.regions)
			{
				foreach (WorldTile tTile in tRegion.tiles)
				{
					if (tTile.Type.layer_type != tRegion.island.type)
					{
						bool tWasDirtyChunkLastTime = this._last_dirty_chunks.Contains(tChunk);
						bool tWasDirtyLinkLastTime = this._last_dirty_links.Contains(tChunk);
						Debug.LogError(string.Concat(new string[]
						{
							"Wrong island type: ",
							tTile.Type.layer_type.ToString(),
							" != ",
							tRegion.island.type.ToString(),
							" ",
							tTile.chunk.id.ToString(),
							" - was dirty: ",
							tWasDirtyChunkLastTime.ToString(),
							" | ",
							tWasDirtyLinkLastTime.ToString()
						}));
						break;
					}
				}
			}
		}
	}

	// Token: 0x06002470 RID: 9328 RVA: 0x0012FD04 File Offset: 0x0012DF04
	private void calculateRegionNeighbours(MapChunk pChunk)
	{
		for (int i = 0; i < pChunk.regions.Count; i++)
		{
			pChunk.regions[i].calculateNeighbours();
		}
	}

	// Token: 0x06002471 RID: 9329 RVA: 0x0012FD38 File Offset: 0x0012DF38
	public int countRegions()
	{
		int tResult = 0;
		MapChunk[] tChunks = this.chunks;
		int tLen = tChunks.Length;
		for (int i = 0; i < tLen; i++)
		{
			MapChunk tChunk = tChunks[i];
			tResult += tChunk.regions.Count;
		}
		return tResult;
	}

	// Token: 0x170001FB RID: 507
	// (get) Token: 0x06002472 RID: 9330 RVA: 0x0012FD73 File Offset: 0x0012DF73
	private bool _is_parallel_enabled
	{
		get
		{
			return DebugConfig.isOn(DebugOption.ParallelChunks);
		}
	}

	// Token: 0x170001FC RID: 508
	// (get) Token: 0x06002473 RID: 9331 RVA: 0x0012FD7F File Offset: 0x0012DF7F
	private bool _batches_enabled
	{
		get
		{
			return DebugConfig.isOn(DebugOption.ChunkBatches);
		}
	}

	// Token: 0x04001A3D RID: 6717
	private readonly Color _color_1_gray = Color.gray;

	// Token: 0x04001A3E RID: 6718
	private readonly Color _color_2_white = Color.white;

	// Token: 0x04001A3F RID: 6719
	private MapChunk[,] _map;

	// Token: 0x04001A40 RID: 6720
	public MapChunk[] chunks = new MapChunk[0];

	// Token: 0x04001A41 RID: 6721
	private readonly List<MapChunk> _dirty_chunks_regions = new List<MapChunk>();

	// Token: 0x04001A42 RID: 6722
	private readonly List<MapChunk> _dirty_chunks_links = new List<MapChunk>();

	// Token: 0x04001A43 RID: 6723
	private int m_dirtyChunks;

	// Token: 0x04001A44 RID: 6724
	private static int m_dirtyCorners;

	// Token: 0x04001A45 RID: 6725
	private static int m_newRegions;

	// Token: 0x04001A46 RID: 6726
	public static int m_newLinks;

	// Token: 0x04001A47 RID: 6727
	internal static int m_newIslands;

	// Token: 0x04001A48 RID: 6728
	internal static int m_dirtyIslands;

	// Token: 0x04001A49 RID: 6729
	private float _timer = 0.4f;

	// Token: 0x04001A4A RID: 6730
	private int _get_amount_x;

	// Token: 0x04001A4B RID: 6731
	private int _amount_y;

	// Token: 0x04001A4C RID: 6732
	private readonly List<MapChunk> _last_dirty_chunks = new List<MapChunk>();

	// Token: 0x04001A4D RID: 6733
	private readonly List<MapChunk> _last_dirty_links = new List<MapChunk>();

	// Token: 0x04001A4E RID: 6734
	private readonly HashSet<MapRegion> _region_set = new HashSet<MapRegion>();

	// Token: 0x04001A4F RID: 6735
	private readonly List<TileIsland> _temp_dirty_neighbours_islands = new List<TileIsland>();
}
