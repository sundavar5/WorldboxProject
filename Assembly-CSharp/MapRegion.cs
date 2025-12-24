using System;
using System.Collections.Generic;

// Token: 0x0200041E RID: 1054
public class MapRegion : IEquatable<MapRegion>
{
	// Token: 0x06002475 RID: 9333 RVA: 0x0012FE10 File Offset: 0x0012E010
	public MapRegion()
	{
		this.created = MapRegion.created_time_last;
	}

	// Token: 0x06002476 RID: 9334 RVA: 0x0012FEA8 File Offset: 0x0012E0A8
	public void reset()
	{
		this._edge_tiles.Clear();
		this.edge_tiles_set.Clear();
		this._outside_edge_regions.Clear();
		this._outside_islands.Clear();
		this.tiles.Clear();
		this.zones.Clear();
		this._links.Clear();
		this.island = null;
		this.is_island_checked = false;
		this.is_checked_path = false;
		this.path_wave_id = -1;
		this.region_path = false;
		this.chunk = null;
		this.center_region = false;
		this.neighbours.Clear();
		this._neighbours_hash.Clear();
		List<WorldTile> list = this.debug_blink_edges_left;
		if (list != null)
		{
			list.Clear();
		}
		List<WorldTile> list2 = this.debug_blink_edges_up;
		if (list2 != null)
		{
			list2.Clear();
		}
		List<WorldTile> list3 = this.debug_blink_edges_down;
		if (list3 != null)
		{
			list3.Clear();
		}
		List<WorldTile> list4 = this.debug_blink_edges_right;
		if (list4 == null)
		{
			return;
		}
		list4.Clear();
	}

	// Token: 0x06002477 RID: 9335 RVA: 0x0012FF8C File Offset: 0x0012E18C
	public void checkZones()
	{
		if (this.chunk.regions.Count == 1)
		{
			this.zones.UnionWith(this.chunk.zones);
			return;
		}
		List<WorldTile> tTiles = this.tiles;
		int tCount = tTiles.Count;
		for (int i = 0; i < tCount; i++)
		{
			this.zones.Add(tTiles[i].zone);
		}
	}

	// Token: 0x06002478 RID: 9336 RVA: 0x0012FFF5 File Offset: 0x0012E1F5
	private void checkDebugLists()
	{
		if (!DebugConfig.isOn(DebugOption.Connections))
		{
			return;
		}
		if (this.debug_blink_edges_left == null)
		{
			this.debug_blink_edges_left = new List<WorldTile>();
			this.debug_blink_edges_up = new List<WorldTile>();
			this.debug_blink_edges_down = new List<WorldTile>();
			this.debug_blink_edges_right = new List<WorldTile>();
		}
	}

	// Token: 0x06002479 RID: 9337 RVA: 0x00130034 File Offset: 0x0012E234
	internal int newConnection(List<WorldTile> pTiles, LinkDirection pDirection, LinkDirection pDirectionID)
	{
		return this.newHash(pTiles, pTiles.Count, pDirection, pDirectionID);
	}

	// Token: 0x0600247A RID: 9338 RVA: 0x00130048 File Offset: 0x0012E248
	private void checkDebugEdges(List<WorldTile> pTiles, LinkDirection pDirection)
	{
		if (!DebugConfig.isOn(DebugOption.Connections))
		{
			return;
		}
		List<WorldTile> tEdges = null;
		this.checkDebugLists();
		switch (pDirection)
		{
		case LinkDirection.Up:
			tEdges = this.debug_blink_edges_up;
			break;
		case LinkDirection.Down:
			tEdges = this.debug_blink_edges_down;
			break;
		case LinkDirection.Left:
			tEdges = this.debug_blink_edges_left;
			break;
		case LinkDirection.Right:
			tEdges = this.debug_blink_edges_right;
			break;
		}
		if (tEdges == null)
		{
			return;
		}
		tEdges.AddRange(pTiles);
	}

	// Token: 0x0600247B RID: 9339 RVA: 0x001300AC File Offset: 0x0012E2AC
	private int newHash(List<WorldTile> pTiles, int pLen, LinkDirection pDirection, LinkDirection pDirectionID)
	{
		WorldTile worldTile = pTiles[0];
		int tX = worldTile.x;
		int tY = worldTile.y;
		int tHashInt = (int)((worldTile.Type.layer_type + 1) * (TileLayerType)100000 + (tX * 11 + tY * 3) * 10000 + pLen * 1000 + tX * 10 + tY);
		if (pDirectionID == LinkDirection.LR)
		{
			tHashInt = -tHashInt;
		}
		return tHashInt;
	}

	// Token: 0x0600247C RID: 9340 RVA: 0x00130108 File Offset: 0x0012E308
	public void clear()
	{
		for (int i = 0; i < this._links.Count; i++)
		{
			RegionLinkHashes.remove(this._links[i], this);
		}
		this._links.Clear();
		this.region_path_id = -1;
	}

	// Token: 0x0600247D RID: 9341 RVA: 0x0013014F File Offset: 0x0012E34F
	public void addLink(RegionLink pLink)
	{
		this._links.Add(pLink);
	}

	// Token: 0x0600247E RID: 9342 RVA: 0x00130160 File Offset: 0x0012E360
	public void calculateNeighbours()
	{
		this.neighbours.Clear();
		this._neighbours_hash.Clear();
		for (int i = 0; i < this._links.Count; i++)
		{
			foreach (MapRegion tLinkedRegion in this._links[i].regions)
			{
				if (tLinkedRegion != this)
				{
					this._neighbours_hash.Add(tLinkedRegion);
				}
			}
		}
		this.neighbours.AddRange(this._neighbours_hash);
	}

	// Token: 0x0600247F RID: 9343 RVA: 0x00130208 File Offset: 0x0012E408
	public bool hasNeighbour(MapRegion pRegion)
	{
		return this._neighbours_hash.Contains(pRegion);
	}

	// Token: 0x06002480 RID: 9344 RVA: 0x00130218 File Offset: 0x0012E418
	public void debugLinks(DebugTool pTool)
	{
		pTool.setText("- links:", this._links.Count, 0f, false, 0L, false, false, "");
		List<RegionLink> tList = new List<RegionLink>(this._links);
		tList.Sort(new Comparison<RegionLink>(this.linkSortByID));
		for (int i = 0; i < tList.Count; i++)
		{
			RegionLink regionLink = tList[i];
			pTool.setText("- hash " + i.ToString() + ":", tList[i].id, 0f, false, 0L, false, false, "");
		}
	}

	// Token: 0x06002481 RID: 9345 RVA: 0x001302C2 File Offset: 0x0012E4C2
	private int linkSortByID(RegionLink o1, RegionLink o2)
	{
		return o2.id.CompareTo(o1.id);
	}

	// Token: 0x06002482 RID: 9346 RVA: 0x001302D8 File Offset: 0x0012E4D8
	public void calculateCenterRegion()
	{
		this.center_region = true;
		if (this.chunk.regions.Count > 1)
		{
			this.center_region = false;
			return;
		}
		MapChunk[] tChunks = this.chunk.neighbours_all;
		for (int i = 0; i < tChunks.Length; i++)
		{
			List<MapRegion> tRegions = tChunks[i].regions;
			if (tRegions.Count > 1)
			{
				this.center_region = false;
				return;
			}
			if (tRegions[0].island != this.island)
			{
				this.center_region = false;
				return;
			}
		}
	}

	// Token: 0x06002483 RID: 9347 RVA: 0x00130358 File Offset: 0x0012E558
	public void calculateTileEdges()
	{
		if (this.center_region)
		{
			return;
		}
		this._edge_tiles.Clear();
		this._outside_edge_regions.Clear();
		this._outside_islands.Clear();
		foreach (WorldTile tTile in this.edge_tiles_set)
		{
			if (tTile.Type.layer_type != this.type)
			{
				this._edge_tiles.Add(tTile);
				this._outside_edge_regions.Add(tTile.region);
			}
		}
	}

	// Token: 0x06002484 RID: 9348 RVA: 0x00130400 File Offset: 0x0012E600
	public WorldTile getRandomTile()
	{
		return this.tiles.GetRandom<WorldTile>();
	}

	// Token: 0x06002485 RID: 9349 RVA: 0x0013040D File Offset: 0x0012E60D
	public bool isTypeGround()
	{
		return this.type == TileLayerType.Ground;
	}

	// Token: 0x06002486 RID: 9350 RVA: 0x00130418 File Offset: 0x0012E618
	public List<WorldTile> getEdgeTiles()
	{
		return this._edge_tiles;
	}

	// Token: 0x06002487 RID: 9351 RVA: 0x00130420 File Offset: 0x0012E620
	public HashSet<MapRegion> getEdgeRegions()
	{
		return this._outside_edge_regions;
	}

	// Token: 0x06002488 RID: 9352 RVA: 0x00130428 File Offset: 0x0012E628
	public override int GetHashCode()
	{
		return this.id;
	}

	// Token: 0x06002489 RID: 9353 RVA: 0x00130430 File Offset: 0x0012E630
	public override bool Equals(object obj)
	{
		return this.Equals(obj as MapRegion);
	}

	// Token: 0x0600248A RID: 9354 RVA: 0x0013043E File Offset: 0x0012E63E
	public bool Equals(MapRegion pObject)
	{
		return this.id == pObject.id;
	}

	// Token: 0x04001A52 RID: 6738
	public int id;

	// Token: 0x04001A53 RID: 6739
	public bool used_by_path_lock;

	// Token: 0x04001A54 RID: 6740
	public int region_path_id = -1;

	// Token: 0x04001A55 RID: 6741
	public TileLayerType type;

	// Token: 0x04001A56 RID: 6742
	public readonly float created;

	// Token: 0x04001A57 RID: 6743
	private readonly List<WorldTile> _edge_tiles = new List<WorldTile>(4);

	// Token: 0x04001A58 RID: 6744
	public readonly HashSet<WorldTile> edge_tiles_set = new HashSet<WorldTile>();

	// Token: 0x04001A59 RID: 6745
	private readonly HashSet<MapRegion> _outside_edge_regions = new HashSet<MapRegion>();

	// Token: 0x04001A5A RID: 6746
	private readonly HashSet<TileIsland> _outside_islands = new HashSet<TileIsland>();

	// Token: 0x04001A5B RID: 6747
	public readonly List<WorldTile> tiles = new List<WorldTile>(256);

	// Token: 0x04001A5C RID: 6748
	private readonly List<RegionLink> _links = new List<RegionLink>(4);

	// Token: 0x04001A5D RID: 6749
	public TileIsland island;

	// Token: 0x04001A5E RID: 6750
	public bool is_island_checked;

	// Token: 0x04001A5F RID: 6751
	public bool is_checked_path;

	// Token: 0x04001A60 RID: 6752
	public int path_wave_id = -1;

	// Token: 0x04001A61 RID: 6753
	public bool region_path;

	// Token: 0x04001A62 RID: 6754
	public MapChunk chunk;

	// Token: 0x04001A63 RID: 6755
	public readonly HashSet<TileZone> zones = new HashSet<TileZone>();

	// Token: 0x04001A64 RID: 6756
	public bool center_region;

	// Token: 0x04001A65 RID: 6757
	public readonly List<MapRegion> neighbours = new List<MapRegion>(4);

	// Token: 0x04001A66 RID: 6758
	private readonly HashSet<MapRegion> _neighbours_hash = new HashSet<MapRegion>();

	// Token: 0x04001A67 RID: 6759
	public static float created_time_last;

	// Token: 0x04001A68 RID: 6760
	public List<WorldTile> debug_blink_edges_left;

	// Token: 0x04001A69 RID: 6761
	public List<WorldTile> debug_blink_edges_up;

	// Token: 0x04001A6A RID: 6762
	public List<WorldTile> debug_blink_edges_down;

	// Token: 0x04001A6B RID: 6763
	public List<WorldTile> debug_blink_edges_right;
}
