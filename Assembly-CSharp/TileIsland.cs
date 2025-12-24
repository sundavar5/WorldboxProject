using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000422 RID: 1058
public sealed class TileIsland
{
	// Token: 0x06002497 RID: 9367 RVA: 0x00130618 File Offset: 0x0012E818
	public TileIsland(int pID)
	{
		this.id = pID;
		this.debug_hash_code = this.GetHashCode();
		this.created = Time.time;
	}

	// Token: 0x06002498 RID: 9368 RVA: 0x001306A4 File Offset: 0x0012E8A4
	public void reset()
	{
		this.removed = false;
		this._tile_count = 0;
		this.regions.Clear();
		this.insideRegionEdges.Clear();
		this.outsideRegionEdges.Clear();
		this.tiles_roads.Clear();
		this.actors.Clear();
		ListPool<Docks> listPool = this.docks;
		if (listPool != null)
		{
			listPool.Dispose();
		}
		this.docks = null;
		this._cached_reachable_islands_check.Clear();
		this._connected_islands.Clear();
		this.dirty = false;
		this._dirty_neighbours = true;
	}

	// Token: 0x06002499 RID: 9369 RVA: 0x00130732 File Offset: 0x0012E932
	public void addDock(Building pBuilding)
	{
		if (this.docks == null)
		{
			this.docks = new ListPool<Docks>();
		}
		this.docks.Add(pBuilding.component_docks);
	}

	// Token: 0x0600249A RID: 9370 RVA: 0x00130758 File Offset: 0x0012E958
	public void clearCache()
	{
		this._cached_reachable_islands_check.Clear();
	}

	// Token: 0x0600249B RID: 9371 RVA: 0x00130765 File Offset: 0x0012E965
	public void addRegion(MapRegion pRegion)
	{
		this.regions.Add(pRegion);
	}

	// Token: 0x0600249C RID: 9372 RVA: 0x00130773 File Offset: 0x0012E973
	public void removeRegion(MapRegion pRegion)
	{
		this.regions.Remove(pRegion);
		pRegion.island = null;
		this.dirty = true;
	}

	// Token: 0x0600249D RID: 9373 RVA: 0x00130790 File Offset: 0x0012E990
	public void clearRegionsFromIsland()
	{
		if (this.removed)
		{
			return;
		}
		this.removed = true;
		List<MapRegion> tList = this.regions.getSimpleList();
		for (int i = 0; i < tList.Count; i++)
		{
			tList[i].island = null;
		}
		this.regions.Clear();
	}

	// Token: 0x0600249E RID: 9374 RVA: 0x001307E2 File Offset: 0x0012E9E2
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int getTileCount()
	{
		return this._tile_count;
	}

	// Token: 0x0600249F RID: 9375 RVA: 0x001307EC File Offset: 0x0012E9EC
	internal void countTiles()
	{
		this._tile_count = 0;
		List<MapRegion> tList = this.regions.getSimpleList();
		for (int i = 0; i < tList.Count; i++)
		{
			MapRegion tRegion = tList[i];
			this._tile_count += tRegion.tiles.Count;
		}
	}

	// Token: 0x060024A0 RID: 9376 RVA: 0x0013083D File Offset: 0x0012EA3D
	internal WorldTile getRandomTile()
	{
		return this.regions.GetRandom().tiles.GetRandom<WorldTile>();
	}

	// Token: 0x060024A1 RID: 9377 RVA: 0x00130854 File Offset: 0x0012EA54
	internal HashSet<TileIsland> getConnectedIslands()
	{
		return this._connected_islands;
	}

	// Token: 0x060024A2 RID: 9378 RVA: 0x0013085C File Offset: 0x0012EA5C
	public void setDirtyIslandNeighbours()
	{
		this._dirty_neighbours = true;
	}

	// Token: 0x060024A3 RID: 9379 RVA: 0x00130865 File Offset: 0x0012EA65
	public bool isDirtyNeighbours()
	{
		return this._dirty_neighbours;
	}

	// Token: 0x060024A4 RID: 9380 RVA: 0x00130870 File Offset: 0x0012EA70
	public void calcNeighbourIslands()
	{
		if (!this._dirty_neighbours)
		{
			return;
		}
		this._dirty_neighbours = false;
		HashSet<TileIsland> tConnectedIslands = this._connected_islands;
		HashSet<MapRegion> tOutsideRegionEdges = this.outsideRegionEdges;
		HashSet<MapRegion> tInsideRegionEdges = this.insideRegionEdges;
		tConnectedIslands.Clear();
		tOutsideRegionEdges.Clear();
		foreach (MapRegion mapRegion in tInsideRegionEdges)
		{
			HashSet<MapRegion> tSetOutsideRegions = mapRegion.getEdgeRegions();
			if (tSetOutsideRegions.Count != 0)
			{
				foreach (MapRegion tRegion in tSetOutsideRegions)
				{
					if (tOutsideRegionEdges.Add(tRegion))
					{
						tConnectedIslands.Add(tRegion.island);
					}
				}
			}
		}
	}

	// Token: 0x060024A5 RID: 9381 RVA: 0x00130948 File Offset: 0x0012EB48
	public bool goodForDocks()
	{
		return this.getTileCount() >= 2500;
	}

	// Token: 0x060024A6 RID: 9382 RVA: 0x0013095C File Offset: 0x0012EB5C
	public bool reachableByCityFrom(TileIsland pIsland)
	{
		if (this._cached_reachable_islands_check.ContainsKey(pIsland))
		{
			return this._cached_reachable_islands_check[pIsland];
		}
		if (pIsland == this)
		{
			return false;
		}
		HashSet<TileIsland> connectedIslands = this.getConnectedIslands();
		HashSet<TileIsland> tOther = pIsland.getConnectedIslands();
		foreach (TileIsland tN in connectedIslands)
		{
			foreach (TileIsland tNCheck in tOther)
			{
				if (tN == tNCheck && tN.type == TileLayerType.Ocean && tN.goodForDocks())
				{
					this._cached_reachable_islands_check[pIsland] = true;
					return true;
				}
			}
		}
		this._cached_reachable_islands_check[pIsland] = false;
		return false;
	}

	// Token: 0x060024A7 RID: 9383 RVA: 0x00130A40 File Offset: 0x0012EC40
	public bool isConnectedWith(TileIsland pIsland)
	{
		return this._connected_islands.Contains(pIsland);
	}

	// Token: 0x060024A8 RID: 9384 RVA: 0x00130A54 File Offset: 0x0012EC54
	internal bool isGoodIslandForActor(Actor pActor)
	{
		return this.type != TileLayerType.Block && (this.type != TileLayerType.Ocean || pActor.isWaterCreature()) && (this.type != TileLayerType.Ground || !pActor.isWaterCreature()) && (this.type != TileLayerType.Lava || !pActor.asset.die_in_lava) && this._tile_count > 5 && (this._tile_count >= 100 || this._tile_count >= this.actors.Count);
	}

	// Token: 0x04001A72 RID: 6770
	public readonly RegionsContainer regions = new RegionsContainer();

	// Token: 0x04001A73 RID: 6771
	public readonly HashSet<MapRegion> insideRegionEdges = new HashSet<MapRegion>();

	// Token: 0x04001A74 RID: 6772
	public readonly HashSet<MapRegion> outsideRegionEdges = new HashSet<MapRegion>();

	// Token: 0x04001A75 RID: 6773
	public readonly float created;

	// Token: 0x04001A76 RID: 6774
	public TileLayerType type = TileLayerType.Ocean;

	// Token: 0x04001A77 RID: 6775
	public bool dirty;

	// Token: 0x04001A78 RID: 6776
	private bool _dirty_neighbours = true;

	// Token: 0x04001A79 RID: 6777
	public readonly int id;

	// Token: 0x04001A7A RID: 6778
	public readonly int debug_hash_code;

	// Token: 0x04001A7B RID: 6779
	public readonly HashSetWorldTile tiles_roads = new HashSetWorldTile();

	// Token: 0x04001A7C RID: 6780
	internal bool removed;

	// Token: 0x04001A7D RID: 6781
	public readonly List<Actor> actors = new List<Actor>();

	// Token: 0x04001A7E RID: 6782
	public ListPool<Docks> docks;

	// Token: 0x04001A7F RID: 6783
	private int _tile_count;

	// Token: 0x04001A80 RID: 6784
	private readonly Dictionary<TileIsland, bool> _cached_reachable_islands_check = new Dictionary<TileIsland, bool>();

	// Token: 0x04001A81 RID: 6785
	private readonly HashSet<TileIsland> _connected_islands = new HashSet<TileIsland>();
}
