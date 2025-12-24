using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200041B RID: 1051
public class MapChunk : IDisposable
{
	// Token: 0x06002431 RID: 9265 RVA: 0x0012D9BF File Offset: 0x0012BBBF
	internal void addTile(WorldTile pTile, int pX, int pY)
	{
		this.tiles[this.tiles.FreeIndex<WorldTile>()] = pTile;
	}

	// Token: 0x06002432 RID: 9266 RVA: 0x0012D9D4 File Offset: 0x0012BBD4
	internal void addNeighbour(MapChunk pNeighbour, TileDirection pDirection, IList<MapChunk> pNeighbours, IList<MapChunk> pNeighboursAll, bool pDiagonal = false)
	{
		if (pNeighbour == null)
		{
			this.world_edge = true;
			return;
		}
		if (!pDiagonal)
		{
			pNeighbours.Add(pNeighbour);
		}
		pNeighboursAll.Add(pNeighbour);
		switch (pDirection)
		{
		case TileDirection.Left:
			this.chunk_left = pNeighbour;
			return;
		case TileDirection.Right:
			this.chunk_right = pNeighbour;
			return;
		case TileDirection.Up:
			this.chunk_up = pNeighbour;
			return;
		case TileDirection.Down:
			this.chunk_down = pNeighbour;
			return;
		default:
			return;
		}
	}

	// Token: 0x06002433 RID: 9267 RVA: 0x0012DA38 File Offset: 0x0012BC38
	public void calculateRegions()
	{
		this.dirty_regions = false;
		WorldTile[] tTiles = this.tiles;
		List<MapRegion> tRegions = this.regions;
		StackPool<MapRegion> tRegionPool = this._region_pool;
		this.clearTiles(true);
		int tCount = tTiles.Length;
		for (int i = 0; i < tCount; i++)
		{
			WorldTile tTile = tTiles[i];
			if (tTile.region == null)
			{
				MapRegion tRegion = tRegionPool.get();
				tRegion.reset();
				tRegion.type = tTile.Type.layer_type;
				tRegion.chunk = this;
				this.fillRegion(tTile, tRegion);
				tRegion.id = tTile.zone.id * 1000 + tRegions.Count;
				tRegions.Add(tRegion);
				if (tRegion.tiles.Count == tTiles.Length)
				{
					break;
				}
			}
		}
		this.clearTiles(false);
		tCount = tRegions.Count;
		for (int j = 0; j < tCount; j++)
		{
			tRegions[j].checkZones();
		}
	}

	// Token: 0x06002434 RID: 9268 RVA: 0x0012DB2C File Offset: 0x0012BD2C
	private void clearTiles(bool pClearRegion = true)
	{
		WorldTile[] tTiles = this.tiles;
		if (pClearRegion)
		{
			int tCount = tTiles.Length;
			for (int i = 0; i < tCount; i++)
			{
				WorldTile worldTile = tTiles[i];
				worldTile.is_checked_tile = false;
				worldTile.region = null;
			}
			return;
		}
		int tCount2 = tTiles.Length;
		for (int j = 0; j < tCount2; j++)
		{
			tTiles[j].is_checked_tile = false;
		}
	}

	// Token: 0x06002435 RID: 9269 RVA: 0x0012DB84 File Offset: 0x0012BD84
	internal void shuffleRegionTiles()
	{
		for (int i = 0; i < this.regions.Count; i++)
		{
			MapRegion tRegion = this.regions[i];
			if (this.regions.Count > 1)
			{
				tRegion.center_region = false;
			}
			else
			{
				tRegion.center_region = true;
			}
			tRegion.tiles.Shuffle<WorldTile>();
		}
	}

	// Token: 0x06002436 RID: 9270 RVA: 0x0012DBE0 File Offset: 0x0012BDE0
	private void fillRegion(WorldTile pTile, MapRegion pTargetRegion)
	{
		Queue<WorldTile> tWave = this._wave;
		tWave.Enqueue(pTile);
		while (tWave.Count > 0)
		{
			WorldTile tTile = tWave.Dequeue();
			tTile.is_checked_tile = true;
			tTile.region = pTargetRegion;
			pTargetRegion.tiles.Add(tTile);
			this.processTileNeighbours(tTile, pTargetRegion, tWave);
		}
		tWave.Clear();
	}

	// Token: 0x06002437 RID: 9271 RVA: 0x0012DC38 File Offset: 0x0012BE38
	private void processTileNeighbours(WorldTile pTileMain, MapRegion pTargetRegion, Queue<WorldTile> pWave)
	{
		foreach (WorldTile tTileNeighbour in pTileMain.neighboursAll)
		{
			TileTypeBase tTypeNeighbour = tTileNeighbour.Type;
			if (!tTileNeighbour.is_checked_tile)
			{
				if (tTypeNeighbour.layer_type != pTileMain.region.type || tTileNeighbour.chunk != this)
				{
					pTargetRegion.edge_tiles_set.Add(tTileNeighbour);
				}
				else if (!this.isDiagonalBlockedByCorners(pTileMain, tTileNeighbour))
				{
					tTileNeighbour.is_checked_tile = true;
					tTileNeighbour.region = pTargetRegion;
					pWave.Enqueue(tTileNeighbour);
				}
			}
		}
	}

	// Token: 0x06002438 RID: 9272 RVA: 0x0012DCB8 File Offset: 0x0012BEB8
	private bool isDiagonalBlockedByCorners(WorldTile pTileFrom, WorldTile pTileTo)
	{
		int tDx = pTileTo.x - pTileFrom.x;
		int tDy = pTileTo.y - pTileFrom.y;
		if (Math.Abs(tDx) != 1 || Math.Abs(tDy) != 1)
		{
			return false;
		}
		WorldTile tTileX = World.world.GetTile(pTileFrom.x + tDx, pTileFrom.y);
		WorldTile tTileY = World.world.GetTile(pTileFrom.x, pTileFrom.y + tDy);
		bool flag = tTileX == null || tTileX.Type.block;
		bool yBlocked = tTileY == null || tTileY.Type.block;
		return flag || yBlocked;
	}

	// Token: 0x06002439 RID: 9273 RVA: 0x0012DD4E File Offset: 0x0012BF4E
	public void clearObjects(bool pForceClearBuildings = false)
	{
		if (pForceClearBuildings)
		{
			this.setBuildingsDirty();
		}
		this.objects.reset(this.buildings_dirty);
		this._temp_tiles.Clear();
		this._new_hashes.Clear();
	}

	// Token: 0x0600243A RID: 9274 RVA: 0x0012DD80 File Offset: 0x0012BF80
	public void clearRegions()
	{
		this.clearIsland();
		for (int i = 0; i < this.regions.Count; i++)
		{
			MapRegion tRegion = this.regions[i];
			tRegion.reset();
			this._region_pool.release(tRegion);
		}
		this.regions.Clear();
	}

	// Token: 0x0600243B RID: 9275 RVA: 0x0012DDD4 File Offset: 0x0012BFD4
	public void Dispose()
	{
		this.clearRegions();
		this.clearObjects(true);
		this.setBuildingsDirty();
		this.objects.Dispose();
		this.neighbours.Clear<MapChunk>();
		this.neighbours_all.Clear<MapChunk>();
		this.neighbours = null;
		this.neighbours_all = null;
		this.tiles.Clear<WorldTile>();
		this._wave.Clear();
		List<WorldTile> list = this.edges_all;
		if (list != null)
		{
			list.Clear();
		}
		List<WorldTile> list2 = this.chunk_bounds;
		if (list2 != null)
		{
			list2.Clear();
		}
		this.bounds_left.Clear();
		this.bounds_up.Clear();
		this.bounds_down.Clear();
		this.bounds_right.Clear();
		this.chunk_up = null;
		this.chunk_down = null;
		this.chunk_left = null;
		this.chunk_right = null;
		this.edge_down_left = null;
		this.edge_down_right = null;
		this.edge_up_left = null;
		this.edge_up_right = null;
		this._edge_down_left_connection = null;
		this._edge_down_right_connection = null;
		this._edge_up_left_connection = null;
		this._edge_up_right_connection = null;
		this._region_pool.clear();
		this.zones.Clear();
	}

	// Token: 0x0600243C RID: 9276 RVA: 0x0012DEF4 File Offset: 0x0012C0F4
	public void clearIsland()
	{
		for (int i = 0; i < this.regions.Count; i++)
		{
			MapRegion tRegion = this.regions[i];
			tRegion.clear();
			if (tRegion.island != null)
			{
				World.world.islands_calculator.makeDirty(tRegion.island);
			}
		}
	}

	// Token: 0x0600243D RID: 9277 RVA: 0x0012DF48 File Offset: 0x0012C148
	internal void combineEdges()
	{
		HashSet<WorldTile> tSet = new HashSet<WorldTile>();
		this.edges_all = new List<WorldTile>(tSet);
		tSet.Clear();
		tSet.UnionWith(this.bounds_down);
		tSet.UnionWith(this.bounds_left);
		tSet.UnionWith(this.bounds_right);
		tSet.UnionWith(this.bounds_up);
		this.chunk_bounds = new List<WorldTile>(tSet);
	}

	// Token: 0x0600243E RID: 9278 RVA: 0x0012DFAC File Offset: 0x0012C1AC
	public void generateEdgeConnections()
	{
		MapChunk mapChunk = this.chunk_left;
		WorldTile edge_up_left_connection;
		if (mapChunk == null)
		{
			edge_up_left_connection = null;
		}
		else
		{
			MapChunk mapChunk2 = mapChunk.chunk_up;
			edge_up_left_connection = ((mapChunk2 != null) ? mapChunk2.edge_down_right : null);
		}
		this._edge_up_left_connection = edge_up_left_connection;
		MapChunk mapChunk3 = this.chunk_right;
		WorldTile edge_up_right_connection;
		if (mapChunk3 == null)
		{
			edge_up_right_connection = null;
		}
		else
		{
			MapChunk mapChunk4 = mapChunk3.chunk_up;
			edge_up_right_connection = ((mapChunk4 != null) ? mapChunk4.edge_down_left : null);
		}
		this._edge_up_right_connection = edge_up_right_connection;
		MapChunk mapChunk5 = this.chunk_left;
		WorldTile edge_down_left_connection;
		if (mapChunk5 == null)
		{
			edge_down_left_connection = null;
		}
		else
		{
			MapChunk mapChunk6 = mapChunk5.chunk_down;
			edge_down_left_connection = ((mapChunk6 != null) ? mapChunk6.edge_up_right : null);
		}
		this._edge_down_left_connection = edge_down_left_connection;
		MapChunk mapChunk7 = this.chunk_right;
		WorldTile edge_down_right_connection;
		if (mapChunk7 == null)
		{
			edge_down_right_connection = null;
		}
		else
		{
			MapChunk mapChunk8 = mapChunk7.chunk_down;
			edge_down_right_connection = ((mapChunk8 != null) ? mapChunk8.edge_up_left : null);
		}
		this._edge_down_right_connection = edge_down_right_connection;
	}

	// Token: 0x0600243F RID: 9279 RVA: 0x0012E04C File Offset: 0x0012C24C
	public void checkLinksResults()
	{
		for (int i = 0; i < this._new_hashes.Count; i++)
		{
			TempLinkStruct tStruct = this._new_hashes[i];
			RegionLinkHashes.addHash(tStruct.hash, tStruct.region);
		}
		MapChunkManager.m_newLinks += this._new_hashes.Count;
		this._new_hashes.Clear();
	}

	// Token: 0x06002440 RID: 9280 RVA: 0x0012E0B0 File Offset: 0x0012C2B0
	internal void calculateLinks()
	{
		this.dirty_links = false;
		List<WorldTile> pOurBounds = this.bounds_right;
		MapChunk mapChunk = this.chunk_right;
		this.calculateLink(pOurBounds, (mapChunk != null) ? mapChunk.bounds_left : null, LinkDirection.Right, LinkDirection.LR, true);
		List<WorldTile> pOurBounds2 = this.bounds_left;
		MapChunk mapChunk2 = this.chunk_left;
		this.calculateLink(pOurBounds2, (mapChunk2 != null) ? mapChunk2.bounds_right : null, LinkDirection.Left, LinkDirection.LR, false);
		List<WorldTile> pOurBounds3 = this.bounds_up;
		MapChunk mapChunk3 = this.chunk_up;
		this.calculateLink(pOurBounds3, (mapChunk3 != null) ? mapChunk3.bounds_down : null, LinkDirection.Up, LinkDirection.UD, true);
		List<WorldTile> pOurBounds4 = this.bounds_down;
		MapChunk mapChunk4 = this.chunk_down;
		this.calculateLink(pOurBounds4, (mapChunk4 != null) ? mapChunk4.bounds_up : null, LinkDirection.Down, LinkDirection.UD, false);
		this.checkSpecialDiagonalConnection(this.edge_up_left, this._edge_up_left_connection, LinkDirection.Up, LinkDirection.UD, false);
		this.checkSpecialDiagonalConnection(this.edge_up_right, this._edge_up_right_connection, LinkDirection.Up, LinkDirection.UD, true);
		this.checkSpecialDiagonalConnection(this.edge_down_left, this._edge_down_left_connection, LinkDirection.Down, LinkDirection.UD, false);
		this.checkSpecialDiagonalConnection(this.edge_down_right, this._edge_down_right_connection, LinkDirection.Down, LinkDirection.UD, true);
	}

	// Token: 0x06002441 RID: 9281 RVA: 0x0012E19C File Offset: 0x0012C39C
	private void checkSpecialDiagonalConnection(WorldTile pTileMain, WorldTile pTileTarget, LinkDirection pDirection, LinkDirection pGroupID, bool pUseTargetList = false)
	{
		if (pTileTarget == null)
		{
			return;
		}
		if (!WorldTile.isSameLayer(pTileMain, pTileTarget))
		{
			return;
		}
		if (this.isDiagonalBlockedByCorners(pTileMain, pTileTarget))
		{
			return;
		}
		if (pUseTargetList)
		{
			this._temp_tiles.Add(pTileTarget);
		}
		else
		{
			this._temp_tiles.Add(pTileMain);
		}
		this.makeLink(this._temp_tiles, pDirection, pGroupID, pTileMain.region);
	}

	// Token: 0x06002442 RID: 9282 RVA: 0x0012E1F8 File Offset: 0x0012C3F8
	private void calculateLink(List<WorldTile> pOurBounds, List<WorldTile> pTargetEdgeTiles, LinkDirection pDirection, LinkDirection pGroupID, bool pUseTargetList = false)
	{
		if (pTargetEdgeTiles == null)
		{
			return;
		}
		int tCount = pOurBounds.Count;
		List<WorldTile> tNewConnectionTiles = this._temp_tiles;
		tNewConnectionTiles.Clear();
		bool tStarted = false;
		MapRegion tRegion = null;
		for (int i = 0; i < tCount; i++)
		{
			bool tIsLastElement = i == tCount - 1;
			WorldTile tOurTile = pOurBounds[i];
			WorldTile tTargetTile = pTargetEdgeTiles[i];
			bool tGoodToContinue = WorldTile.isSameLayer(tOurTile, tTargetTile);
			if (tGoodToContinue && !tStarted)
			{
				tStarted = true;
				tRegion = tOurTile.region;
			}
			if (!tIsLastElement)
			{
				if (tGoodToContinue)
				{
					WorldTile tNextOurTile = pOurBounds[i + 1];
					tGoodToContinue = WorldTile.isSameLayer(tOurTile, tNextOurTile);
				}
				if (tGoodToContinue)
				{
					WorldTile tNextTargetTile = pTargetEdgeTiles[i + 1];
					tGoodToContinue = WorldTile.isSameLayer(tOurTile, tNextTargetTile);
				}
			}
			if (!tGoodToContinue && !tStarted && this.tryDiagonal(tOurTile, tTargetTile, pDirection, pGroupID, pUseTargetList, tNewConnectionTiles))
			{
				tStarted = false;
				tRegion = null;
			}
			else
			{
				if (tIsLastElement)
				{
					tGoodToContinue = false;
				}
				if (tStarted || tGoodToContinue)
				{
					if (!tStarted && tGoodToContinue)
					{
						tStarted = true;
						tRegion = tOurTile.region;
						this.saveToConnection(tNewConnectionTiles, tOurTile, tTargetTile, pUseTargetList);
					}
					else if (tStarted && !tGoodToContinue)
					{
						this.saveToConnection(tNewConnectionTiles, tOurTile, tTargetTile, pUseTargetList);
						this.makeLink(tNewConnectionTiles, pDirection, pGroupID, tRegion);
						tStarted = false;
						tRegion = null;
					}
					else
					{
						this.saveToConnection(tNewConnectionTiles, tOurTile, tTargetTile, pUseTargetList);
					}
				}
			}
		}
	}

	// Token: 0x06002443 RID: 9283 RVA: 0x0012E32C File Offset: 0x0012C52C
	private bool tryDiagonal(WorldTile pMainTile, WorldTile pTargetTile, LinkDirection pDirection, LinkDirection pGroupID, bool pUseTargetList, List<WorldTile> pListConnections)
	{
		bool tAnyDiagonalFound = false;
		WorldTile tDiagonalTile = this.getDiagonalConnection(pMainTile, pTargetTile, pDirection, true);
		if (tDiagonalTile != null)
		{
			this.saveToConnection(pListConnections, pMainTile, tDiagonalTile, pUseTargetList);
			this.makeLink(pListConnections, pDirection, pGroupID, pMainTile.region);
			tAnyDiagonalFound = true;
		}
		WorldTile tDiagonalTile2 = this.getDiagonalConnection(pMainTile, pTargetTile, pDirection, false);
		if (tDiagonalTile2 != null)
		{
			this.saveToConnection(pListConnections, pMainTile, tDiagonalTile2, pUseTargetList);
			this.makeLink(pListConnections, pDirection, pGroupID, pMainTile.region);
			tAnyDiagonalFound = true;
		}
		return tAnyDiagonalFound;
	}

	// Token: 0x06002444 RID: 9284 RVA: 0x0012E396 File Offset: 0x0012C596
	private void saveToConnection(List<WorldTile> pList, WorldTile pOurTile, WorldTile pTargetTile, bool pUseTargetList)
	{
		if (pUseTargetList)
		{
			pList.Add(pTargetTile);
			return;
		}
		pList.Add(pOurTile);
	}

	// Token: 0x06002445 RID: 9285 RVA: 0x0012E3AC File Offset: 0x0012C5AC
	private WorldTile getDiagonalConnection(WorldTile pOurTile, WorldTile pTargetTile, LinkDirection pDirection, bool pFirst)
	{
		TileLayerType tMainType = pOurTile.Type.layer_type;
		WorldTile tResultTile = null;
		if (pDirection > LinkDirection.Down)
		{
			if (pDirection - LinkDirection.Left <= 1)
			{
				tResultTile = (pFirst ? pTargetTile.tile_up : pTargetTile.tile_down);
			}
		}
		else
		{
			tResultTile = (pFirst ? pTargetTile.tile_right : pTargetTile.tile_left);
		}
		if (tResultTile == null)
		{
			return null;
		}
		if (this.isDiagonalBlockedByCorners(pOurTile, tResultTile))
		{
			return null;
		}
		if (tResultTile.Type.layer_type != tMainType)
		{
			return null;
		}
		return tResultTile;
	}

	// Token: 0x06002446 RID: 9286 RVA: 0x0012E420 File Offset: 0x0012C620
	private void makeLink(List<WorldTile> pConnectionList, LinkDirection pDirection, LinkDirection pGroupID, MapRegion pRegionMain)
	{
		int tLinkHashId = pRegionMain.newConnection(pConnectionList, pDirection, pGroupID);
		this._new_hashes.Add(new TempLinkStruct
		{
			region = pRegionMain,
			hash = tLinkHashId
		});
		pConnectionList.Clear();
	}

	// Token: 0x06002447 RID: 9287 RVA: 0x0012E463 File Offset: 0x0012C663
	public void setBuildingsDirty()
	{
		this._buildings_dirty = true;
	}

	// Token: 0x170001F9 RID: 505
	// (get) Token: 0x06002448 RID: 9288 RVA: 0x0012E46C File Offset: 0x0012C66C
	public bool buildings_dirty
	{
		get
		{
			return this._buildings_dirty;
		}
	}

	// Token: 0x06002449 RID: 9289 RVA: 0x0012E474 File Offset: 0x0012C674
	public void finishBuildingsCheck()
	{
		this._buildings_dirty = false;
	}

	// Token: 0x0600244A RID: 9290 RVA: 0x0012E47D File Offset: 0x0012C67D
	public List<MusicBoxTileData> getSimpleData()
	{
		if (this._tile_types_dirty)
		{
			this.musicBoxCheckCount();
		}
		return this._simple_data;
	}

	// Token: 0x0600244B RID: 9291 RVA: 0x0012E494 File Offset: 0x0012C694
	private void musicBoxCheckCount()
	{
		this._tile_types_dirty = false;
		this._tile_types_count.Clear();
		this._simple_data.Clear();
		int i = 0;
		int tLen = this.zones.Count;
		while (i < tLen)
		{
			foreach (KeyValuePair<TileTypeBase, HashSet<WorldTile>> keyValuePair in this.zones[i].getTileTypes())
			{
				TileTypeBase tileTypeBase;
				HashSet<WorldTile> hashSet;
				keyValuePair.Deconstruct(out tileTypeBase, out hashSet);
				TileTypeBase tKey = tileTypeBase;
				HashSet<WorldTile> tSet = hashSet;
				if (tKey.music_assets != null)
				{
					int tValue = tSet.Count;
					if (!this._tile_types_count.TryAdd(tKey, tValue))
					{
						Dictionary<TileTypeBase, int> tile_types_count = this._tile_types_count;
						tileTypeBase = tKey;
						tile_types_count[tileTypeBase] += tValue;
					}
				}
			}
			i++;
		}
		foreach (KeyValuePair<TileTypeBase, int> keyValuePair2 in this._tile_types_count)
		{
			TileTypeBase tileTypeBase;
			int num;
			keyValuePair2.Deconstruct(out tileTypeBase, out num);
			TileTypeBase tKey2 = tileTypeBase;
			int tValue2 = num;
			this._simple_data.Add(new MusicBoxTileData
			{
				tile_type_id = tKey2.index_id,
				amount = tValue2
			});
		}
	}

	// Token: 0x0600244C RID: 9292 RVA: 0x0012E5F8 File Offset: 0x0012C7F8
	public Dictionary<TileTypeBase, int> getTileTypesCount()
	{
		if (this._tile_types_dirty)
		{
			this.musicBoxCheckCount();
		}
		return this._tile_types_count;
	}

	// Token: 0x0600244D RID: 9293 RVA: 0x0012E610 File Offset: 0x0012C810
	public int countTilesOfType(TileTypeBase pType)
	{
		if (this._tile_types_dirty)
		{
			this.musicBoxCheckCount();
		}
		int tResult;
		this._tile_types_count.TryGetValue(pType, out tResult);
		return tResult;
	}

	// Token: 0x0600244E RID: 9294 RVA: 0x0012E63B File Offset: 0x0012C83B
	public void setTileTypesDirty()
	{
		this._tile_types_dirty = true;
	}

	// Token: 0x04001A13 RID: 6675
	private readonly Queue<WorldTile> _wave = new Queue<WorldTile>(256);

	// Token: 0x04001A14 RID: 6676
	public readonly ChunkObjectContainer objects = new ChunkObjectContainer();

	// Token: 0x04001A15 RID: 6677
	public MapChunk[] neighbours;

	// Token: 0x04001A16 RID: 6678
	public MapChunk[] neighbours_all;

	// Token: 0x04001A17 RID: 6679
	public readonly WorldTile[] tiles = new WorldTile[256];

	// Token: 0x04001A18 RID: 6680
	public readonly List<MapRegion> regions = new List<MapRegion>(4);

	// Token: 0x04001A19 RID: 6681
	public List<WorldTile> edges_all;

	// Token: 0x04001A1A RID: 6682
	public List<WorldTile> chunk_bounds;

	// Token: 0x04001A1B RID: 6683
	public readonly List<WorldTile> bounds_left = new List<WorldTile>(16);

	// Token: 0x04001A1C RID: 6684
	public readonly List<WorldTile> bounds_up = new List<WorldTile>(16);

	// Token: 0x04001A1D RID: 6685
	public readonly List<WorldTile> bounds_down = new List<WorldTile>(16);

	// Token: 0x04001A1E RID: 6686
	public readonly List<WorldTile> bounds_right = new List<WorldTile>(16);

	// Token: 0x04001A1F RID: 6687
	public float world_center_x;

	// Token: 0x04001A20 RID: 6688
	public float world_center_y;

	// Token: 0x04001A21 RID: 6689
	public WorldTile edge_up_left;

	// Token: 0x04001A22 RID: 6690
	public WorldTile edge_up_right;

	// Token: 0x04001A23 RID: 6691
	public WorldTile edge_down_left;

	// Token: 0x04001A24 RID: 6692
	public WorldTile edge_down_right;

	// Token: 0x04001A25 RID: 6693
	private WorldTile _edge_up_left_connection;

	// Token: 0x04001A26 RID: 6694
	private WorldTile _edge_up_right_connection;

	// Token: 0x04001A27 RID: 6695
	private WorldTile _edge_down_left_connection;

	// Token: 0x04001A28 RID: 6696
	private WorldTile _edge_down_right_connection;

	// Token: 0x04001A29 RID: 6697
	public bool world_edge;

	// Token: 0x04001A2A RID: 6698
	public int x;

	// Token: 0x04001A2B RID: 6699
	public int y;

	// Token: 0x04001A2C RID: 6700
	public int id;

	// Token: 0x04001A2D RID: 6701
	public Color color;

	// Token: 0x04001A2E RID: 6702
	public bool dirty_regions;

	// Token: 0x04001A2F RID: 6703
	public bool dirty_links;

	// Token: 0x04001A30 RID: 6704
	private readonly List<WorldTile> _temp_tiles = new List<WorldTile>(16);

	// Token: 0x04001A31 RID: 6705
	private readonly List<TempLinkStruct> _new_hashes = new List<TempLinkStruct>();

	// Token: 0x04001A32 RID: 6706
	internal MapChunk chunk_up;

	// Token: 0x04001A33 RID: 6707
	internal MapChunk chunk_down;

	// Token: 0x04001A34 RID: 6708
	internal MapChunk chunk_left;

	// Token: 0x04001A35 RID: 6709
	internal MapChunk chunk_right;

	// Token: 0x04001A36 RID: 6710
	private readonly StackPool<MapRegion> _region_pool = new StackPool<MapRegion>();

	// Token: 0x04001A37 RID: 6711
	public readonly List<TileZone> zones = new List<TileZone>();

	// Token: 0x04001A38 RID: 6712
	private bool _buildings_dirty;

	// Token: 0x04001A39 RID: 6713
	private bool _tile_types_dirty;

	// Token: 0x04001A3A RID: 6714
	private const int MAX_TILE_TYPES = 256;

	// Token: 0x04001A3B RID: 6715
	private readonly Dictionary<TileTypeBase, int> _tile_types_count = new Dictionary<TileTypeBase, int>(256);

	// Token: 0x04001A3C RID: 6716
	private readonly List<MusicBoxTileData> _simple_data = new List<MusicBoxTileData>();
}
