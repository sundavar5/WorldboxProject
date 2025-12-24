using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200052C RID: 1324
public class DebugLayerCursor : MapLayer
{
	// Token: 0x06002B58 RID: 11096 RVA: 0x00157970 File Offset: 0x00155B70
	internal override void create()
	{
		base.create();
		this.color_highlight_white = Toolbox.makeColor("#FFFFFF77");
		this.color_main = new Color(0f, 1f, 0f, 0.1f);
		this.color_neighbour = new Color(1f, 0f, 1f, 0.8f);
		this.color_neighbour_2 = new Color(1f, 0f, 1f, 0.3f);
		this.color_edges = new Color(1f, 0f, 0f, 0.5f);
		this.color_chunk_bounds = new Color(0f, 1f, 1f, 0.5f);
		this.color_edges_blink = new Color(0.1f, 0.1f, 1f, 1f);
		this.color_region = new Color(0f, 0f, 1f, 0.8f);
	}

	// Token: 0x06002B59 RID: 11097 RVA: 0x00157A6C File Offset: 0x00155C6C
	protected override void UpdateDirty(float pElapsed)
	{
		if (ScrollWindow.isWindowActive())
		{
			return;
		}
		if (!Config.isEditor && !DebugConfig.instance.debugButton.gameObject.activeSelf)
		{
			this.clear();
			return;
		}
		if (this.timerBlink > 0f)
		{
			this.timerBlink -= Time.deltaTime;
		}
		else
		{
			this.timerBlink = 0.2f;
			this.blink = !this.blink;
		}
		if (this.timerRecalc <= 0f)
		{
			this.timerRecalc = 0.1f;
			return;
		}
		this.timerRecalc -= pElapsed;
		this.clear();
		WorldTile tCursorTile = World.world.getMouseTilePos();
		if (tCursorTile == null)
		{
			return;
		}
		this.lastChunk = tCursorTile.chunk;
		MapChunk chunk = tCursorTile.chunk;
		MapChunk mapChunk = this.lastChunk;
		if (DebugConfig.isOn(DebugOption.RenderIslands))
		{
			bool flag;
			if (tCursorTile == null)
			{
				flag = (null != null);
			}
			else
			{
				MapRegion region = tCursorTile.region;
				flag = (((region != null) ? region.island : null) != null);
			}
			if (flag)
			{
				this.drawIsland(tCursorTile.region.island);
			}
		}
		if (DebugConfig.isOn(DebugOption.CursorChunk))
		{
			this.fill(this.lastChunk.tiles, this.color_highlight_white, false);
		}
		if (DebugConfig.isOn(DebugOption.RenderConnectedIslands))
		{
			bool flag2;
			if (tCursorTile == null)
			{
				flag2 = (null != null);
			}
			else
			{
				MapRegion region2 = tCursorTile.region;
				flag2 = (((region2 != null) ? region2.island : null) != null);
			}
			if (flag2)
			{
				foreach (TileIsland tileIsland in tCursorTile.region.island.getConnectedIslands())
				{
					foreach (MapRegion tReg in tileIsland.regions)
					{
						this.fill(tReg.tiles, Color.blue, false);
					}
				}
			}
		}
		if (DebugConfig.isOn(DebugOption.PossibleCityReach))
		{
			this.renderPossibleCityReach();
		}
		if (DebugConfig.isOn(DebugOption.RenderIslandsInsideRegionCorners))
		{
			bool flag3;
			if (tCursorTile == null)
			{
				flag3 = (null != null);
			}
			else
			{
				MapRegion region3 = tCursorTile.region;
				flag3 = (((region3 != null) ? region3.island : null) != null);
			}
			if (flag3)
			{
				foreach (MapRegion tRegion in tCursorTile.region.island.insideRegionEdges)
				{
					this.fill(tRegion.tiles, Color.magenta, false);
				}
			}
		}
		if (DebugConfig.isOn(DebugOption.RenderIslandsTileCorners))
		{
			bool flag4;
			if (tCursorTile == null)
			{
				flag4 = (null != null);
			}
			else
			{
				MapRegion region4 = tCursorTile.region;
				flag4 = (((region4 != null) ? region4.island : null) != null);
			}
			if (flag4)
			{
				foreach (MapRegion tRegion2 in tCursorTile.region.island.insideRegionEdges)
				{
					this.fill(tRegion2.getEdgeTiles(), Color.red, false);
				}
			}
		}
		if (DebugConfig.isOn(DebugOption.RenderIslandCenterRegions))
		{
			bool flag5;
			if (tCursorTile == null)
			{
				flag5 = (null != null);
			}
			else
			{
				MapRegion region5 = tCursorTile.region;
				flag5 = (((region5 != null) ? region5.island : null) != null);
			}
			if (flag5)
			{
				foreach (MapRegion tRegion3 in tCursorTile.region.island.regions)
				{
					if (!tRegion3.center_region)
					{
						this.fill(tRegion3.tiles, Color.red, false);
					}
				}
			}
		}
		if (DebugConfig.isOn(DebugOption.RenderRegionOutsideRegionCorners) && ((tCursorTile != null) ? tCursorTile.region : null) != null)
		{
			foreach (MapRegion tRegion4 in tCursorTile.region.getEdgeRegions())
			{
				this.fill(tRegion4.tiles, Color.yellow, false);
			}
		}
		if (DebugConfig.isOn(DebugOption.RenderMapRegionEdges) && tCursorTile.region != null)
		{
			this.fill(tCursorTile.region.getEdgeTiles(), Color.red, false);
		}
		if (DebugConfig.isOn(DebugOption.RegionNeighbours) && tCursorTile.region != null)
		{
			HashSet<MapRegion> tWave = new HashSet<MapRegion>();
			HashSet<MapRegion> tWave2 = new HashSet<MapRegion>();
			tWave.Add(tCursorTile.region);
			foreach (MapRegion tRegion5 in tCursorTile.region.neighbours)
			{
				tWave.Add(tRegion5);
			}
			foreach (MapRegion mapRegion in tWave)
			{
				foreach (MapRegion tRegionNeighbour in mapRegion.neighbours)
				{
					if (!tWave.Contains(tRegionNeighbour))
					{
						tWave2.Add(tRegionNeighbour);
					}
				}
			}
			foreach (MapRegion tReg2 in tWave)
			{
				this.fill(tReg2.tiles, this.color_neighbour, false);
			}
			foreach (MapRegion tReg3 in tWave2)
			{
				this.fill(tReg3.tiles, this.color_neighbour_2, false);
			}
		}
		if (DebugConfig.isOn(DebugOption.Region) && tCursorTile.region != null)
		{
			this.fill(tCursorTile.region.tiles, this.color_region, false);
		}
		if (DebugConfig.isOn(DebugOption.ConnectedZones) && tCursorTile.zone != null)
		{
			TileZone tMainZone = tCursorTile.zone;
			MapRegion tMainRegion = tCursorTile.region;
			this.fill(tMainZone.tiles, this.color_region, false);
			using (ListPool<MapRegion> tPool = new ListPool<MapRegion>())
			{
				foreach (TileZone tNZone in tMainZone.neighbours)
				{
					tPool.Clear();
					if (TileZone.hasZonesConnectedViaRegions(tMainZone, tNZone, tMainRegion, tPool))
					{
						this.fill(tNZone.tiles, this.color_neighbour, false);
					}
				}
			}
		}
		if (DebugConfig.isOn(DebugOption.ChunkEdges) && tCursorTile.chunk != null)
		{
			this.fill(tCursorTile.chunk.edges_all, this.color_edges, false);
		}
		if (DebugConfig.isOn(DebugOption.ChunkBounds) && tCursorTile.chunk != null)
		{
			this.fill(tCursorTile.chunk.chunk_bounds, this.color_chunk_bounds, false);
		}
		if (DebugConfig.isOn(DebugOption.Connections) && tCursorTile.region != null)
		{
			this.drawConnections(tCursorTile);
		}
		base.updatePixels();
	}

	// Token: 0x06002B5A RID: 11098 RVA: 0x0015813C File Offset: 0x0015633C
	private void renderPossibleCityReach()
	{
		WorldTile tCursorTile = World.world.getMouseTilePos();
		if (tCursorTile.zone.city == null)
		{
			return;
		}
		TileIsland tCityIsland = tCursorTile.region.island;
		foreach (TileIsland ptr in World.world.islands_calculator.islands)
		{
			TileIsland tIsland = ptr;
			if (tCityIsland != tIsland && tCityIsland.reachableByCityFrom(tIsland))
			{
				foreach (MapRegion tReg in tIsland.regions)
				{
					this.fill(tReg.tiles, Color.blue, false);
				}
			}
		}
	}

	// Token: 0x06002B5B RID: 11099 RVA: 0x00158214 File Offset: 0x00156414
	private void drawIsland(TileIsland pIsland)
	{
		Color32 tRed = Color.red;
		foreach (MapRegion tRegion in pIsland.regions)
		{
			this._tiles.AddRange(tRegion.tiles);
			foreach (WorldTile tTile in tRegion.tiles)
			{
				this.pixels[tTile.data.tile_id] = tRed;
			}
		}
	}

	// Token: 0x06002B5C RID: 11100 RVA: 0x001582CC File Offset: 0x001564CC
	private void drawConnections(WorldTile pTile)
	{
		if (this.blink && pTile.region.debug_blink_edges_up != null)
		{
			this.fill(pTile.region.debug_blink_edges_up, this.color_edges_blink, true);
			this.fill(pTile.region.debug_blink_edges_down, this.color_edges_blink, true);
			this.fill(pTile.region.debug_blink_edges_left, this.color_edges_blink, true);
			this.fill(pTile.region.debug_blink_edges_right, this.color_edges_blink, true);
		}
	}

	// Token: 0x06002B5D RID: 11101 RVA: 0x00158350 File Offset: 0x00156550
	private void fill(List<WorldTile> pTiles, Color pColor, bool pEdge = false)
	{
		base.createTextureNew();
		for (int i = 0; i < pTiles.Count; i++)
		{
			WorldTile tTile = pTiles[i];
			if (!pEdge || tTile.region != null)
			{
				this._tiles.Add(tTile);
				this.pixels[tTile.data.tile_id] = pColor;
			}
		}
	}

	// Token: 0x06002B5E RID: 11102 RVA: 0x001583B0 File Offset: 0x001565B0
	private void fill(WorldTile[] pTiles, Color pColor, bool pEdge = false)
	{
		base.createTextureNew();
		foreach (WorldTile tTile in pTiles)
		{
			if (!pEdge || tTile.region != null)
			{
				this._tiles.Add(tTile);
				this.pixels[tTile.data.tile_id] = pColor;
			}
		}
	}

	// Token: 0x06002B5F RID: 11103 RVA: 0x00158408 File Offset: 0x00156608
	internal override void clear()
	{
		if (this._tiles.Count == 0)
		{
			return;
		}
		this._tiles.Clear();
		for (int i = 0; i < this.pixels.Length; i++)
		{
			this.pixels[i] = Color.clear;
		}
		base.createTextureNew();
	}

	// Token: 0x04002072 RID: 8306
	private Color color_highlight_white;

	// Token: 0x04002073 RID: 8307
	private Color color_main;

	// Token: 0x04002074 RID: 8308
	private Color color_neighbour;

	// Token: 0x04002075 RID: 8309
	private Color color_neighbour_2;

	// Token: 0x04002076 RID: 8310
	private Color color_region;

	// Token: 0x04002077 RID: 8311
	private Color color_edges;

	// Token: 0x04002078 RID: 8312
	private Color color_chunk_bounds;

	// Token: 0x04002079 RID: 8313
	private Color color_edges_blink;

	// Token: 0x0400207A RID: 8314
	private List<WorldTile> _tiles = new List<WorldTile>();

	// Token: 0x0400207B RID: 8315
	private bool blink = true;

	// Token: 0x0400207C RID: 8316
	private float timerBlink = 0.2f;

	// Token: 0x0400207D RID: 8317
	private float timerRecalc = 0.1f;

	// Token: 0x0400207E RID: 8318
	private MapChunk lastChunk;
}
