using System;
using System.Collections.Generic;

// Token: 0x0200043C RID: 1084
public class RegionPathFinder
{
	// Token: 0x060025AD RID: 9645 RVA: 0x00136B74 File Offset: 0x00134D74
	public PathFinderResult getGlobalPath(WorldTile pFrom, WorldTile pTarget, bool pBoat = false)
	{
		this.last_globalPath = null;
		if (pFrom == pTarget)
		{
			return PathFinderResult.SamePlace;
		}
		if (pFrom.region == pTarget.region && pFrom.region.tiles.Count == 256)
		{
			return PathFinderResult.SamePlace;
		}
		if (pFrom.region == pTarget.region)
		{
			return PathFinderResult.PathFound;
		}
		if (pFrom.region.island != pTarget.region.island)
		{
			return PathFinderResult.DifferentIslands;
		}
		this.tileStart = pFrom;
		this.tileTarget = pTarget;
		string tID = pFrom.region.id.ToString() + "_" + pTarget.region.id.ToString();
		if (DebugConfig.isOn(DebugOption.UseCacheForRegionPath) && this.cachedPaths.TryGetValue(tID, out this.last_globalPath))
		{
			return PathFinderResult.PathFound;
		}
		this.last_globalPath = new List<MapRegion>();
		this.startWave(this.tileTarget.region);
		if (DebugConfig.isOn(DebugOption.UseCacheForRegionPath))
		{
			this.addToCache(tID, this.last_globalPath);
		}
		return PathFinderResult.PathFound;
	}

	// Token: 0x060025AE RID: 9646 RVA: 0x00136C70 File Offset: 0x00134E70
	public void addToCache(string pID, List<MapRegion> pPath)
	{
		if (this.cachedPaths.Count > 1000)
		{
			this.cachedPaths.Clear();
		}
		this.cachedPaths.Add(pID, pPath);
	}

	// Token: 0x060025AF RID: 9647 RVA: 0x00136C9C File Offset: 0x00134E9C
	public void clearCache()
	{
		this.cachedPaths.Clear();
	}

	// Token: 0x060025B0 RID: 9648 RVA: 0x00136CA9 File Offset: 0x00134EA9
	public void clear()
	{
		this.last_globalPath = null;
		this._temp_regions_cur_wave.Clear();
		this._temp_regions_next_wave.Clear();
		this.clearRegions();
		this.clearCache();
		this.currentWave = 0;
		this.tileStart = null;
		this.tileTarget = null;
	}

	// Token: 0x060025B1 RID: 9649 RVA: 0x00136CE9 File Offset: 0x00134EE9
	private void startWave(MapRegion pRegion)
	{
		this.simplePath = true;
		this.clearRegions();
		this.currentWave = 0;
		this._temp_regions_cur_wave.Clear();
		this._temp_regions_next_wave.Clear();
		this.addToNext(pRegion);
		this.newWave();
	}

	// Token: 0x060025B2 RID: 9650 RVA: 0x00136D24 File Offset: 0x00134F24
	private void addToNext(MapRegion pRegion)
	{
		if (pRegion.tiles.Count != 256)
		{
			this.simplePath = false;
		}
		pRegion.is_checked_path = true;
		this._temp_regions_next_wave.Add(pRegion);
		this._temp_regions_checked.Add(pRegion);
		pRegion.path_wave_id = this.currentWave;
	}

	// Token: 0x060025B3 RID: 9651 RVA: 0x00136D78 File Offset: 0x00134F78
	private void newWave()
	{
		this.currentWave++;
		this._temp_regions_cur_wave.Clear();
		this._temp_regions_cur_wave.AddRange(this._temp_regions_next_wave);
		this._temp_regions_next_wave.Clear();
		for (int i = 0; i < this._temp_regions_cur_wave.Count; i++)
		{
			MapRegion tRegion = this._temp_regions_cur_wave[i];
			for (int i2 = 0; i2 < tRegion.neighbours.Count; i2++)
			{
				MapRegion tLinkedRegion = tRegion.neighbours[i2];
				if (!tLinkedRegion.is_checked_path)
				{
					this.addToNext(tLinkedRegion);
					if (tLinkedRegion == this.tileStart.region)
					{
						this.finalPath(this.tileStart.region);
						return;
					}
				}
			}
		}
		if (this._temp_regions_next_wave.Count > 0)
		{
			this.newWave();
		}
	}

	// Token: 0x060025B4 RID: 9652 RVA: 0x00136E44 File Offset: 0x00135044
	private void finalPath(MapRegion pMainRegion)
	{
		this.last_globalPath.Add(pMainRegion);
		pMainRegion.region_path = true;
		if (pMainRegion == this.tileTarget.region)
		{
			return;
		}
		MapRegion tBest = null;
		for (int i = 0; i < pMainRegion.neighbours.Count; i++)
		{
			MapRegion tLinkedRegion = pMainRegion.neighbours[i];
			if (tLinkedRegion.path_wave_id != -1)
			{
				if (tBest == null)
				{
					tBest = tLinkedRegion;
				}
				else if (tLinkedRegion.path_wave_id < tBest.path_wave_id)
				{
					tBest = tLinkedRegion;
				}
				else if (tLinkedRegion.path_wave_id == tBest.path_wave_id)
				{
					MapChunk tChunkTarget = this.tileTarget.chunk;
					MapChunk tBestChunk = tBest.chunk;
					MapChunk tLinkedChunk = tLinkedRegion.chunk;
					int tDistBest = Toolbox.SquaredDist(tBestChunk.x, tBestChunk.y, tChunkTarget.x, tChunkTarget.y);
					if (Toolbox.SquaredDist(tLinkedChunk.x, tLinkedChunk.y, tChunkTarget.x, tChunkTarget.y) < tDistBest)
					{
						tBest = tLinkedRegion;
					}
				}
			}
		}
		this.finalPath(tBest);
	}

	// Token: 0x060025B5 RID: 9653 RVA: 0x00136F40 File Offset: 0x00135140
	private void clearRegions()
	{
		for (int i = 0; i < this._temp_regions_checked.Count; i++)
		{
			MapRegion mapRegion = this._temp_regions_checked[i];
			mapRegion.is_checked_path = false;
			mapRegion.path_wave_id = -1;
			mapRegion.region_path = false;
		}
		this._temp_regions_checked.Clear();
	}

	// Token: 0x060025B6 RID: 9654 RVA: 0x00136F90 File Offset: 0x00135190
	public string debug()
	{
		return this.cachedPaths.Count.ToString() ?? "";
	}

	// Token: 0x04001C98 RID: 7320
	private Dictionary<string, List<MapRegion>> cachedPaths = new Dictionary<string, List<MapRegion>>();

	// Token: 0x04001C99 RID: 7321
	private List<MapRegion> _temp_regions_cur_wave = new List<MapRegion>();

	// Token: 0x04001C9A RID: 7322
	private List<MapRegion> _temp_regions_next_wave = new List<MapRegion>();

	// Token: 0x04001C9B RID: 7323
	internal List<MapRegion> _temp_regions_checked = new List<MapRegion>();

	// Token: 0x04001C9C RID: 7324
	internal List<MapRegion> last_globalPath;

	// Token: 0x04001C9D RID: 7325
	private int currentWave;

	// Token: 0x04001C9E RID: 7326
	public WorldTile tileStart;

	// Token: 0x04001C9F RID: 7327
	public WorldTile tileTarget;

	// Token: 0x04001CA0 RID: 7328
	public bool simplePath;
}
