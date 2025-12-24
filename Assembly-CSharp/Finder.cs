using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ai.behaviours;

// Token: 0x0200056E RID: 1390
public static class Finder
{
	// Token: 0x06002D52 RID: 11602 RVA: 0x001611C8 File Offset: 0x0015F3C8
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<Building> getBuildingsFromChunk(WorldTile pTile, int pChunkRadius, int pTileRadius = 0, bool pRandom = false)
	{
		int tX = pTile.chunk.x - pChunkRadius;
		int tY = pTile.chunk.y - pChunkRadius;
		int tWidth = pChunkRadius * 2 + 1;
		int tHeight = pChunkRadius * 2 + 1;
		int tCount = tWidth * tHeight;
		MapChunk[] tChunks = Toolbox.checkArraySize<MapChunk>(Finder._chunks, tCount);
		Finder._chunks = tChunks;
		MapChunkManager tChunkManager = World.world.map_chunk_manager;
		int tTileRadius = pTileRadius * pTileRadius;
		int iChunk = 0;
		for (int iX = 0; iX < tWidth; iX++)
		{
			for (int iY = 0; iY < tHeight; iY++)
			{
				MapChunk tChunk = tChunkManager.get(tX + iX, tY + iY);
				if (tChunk == null)
				{
					tCount--;
				}
				else
				{
					tChunks[iChunk++] = tChunk;
				}
			}
		}
		if (pRandom)
		{
			foreach (MapChunk tChunk2 in tChunks.LoopRandom(tCount))
			{
				if (tChunk2 != null)
				{
					List<Building> tBuildings = tChunk2.objects.buildings_all;
					foreach (Building tBuilding in tBuildings.LoopRandom<Building>())
					{
						if (tBuilding.isAlive() && (tTileRadius == 0 || Toolbox.SquaredDistTile(tBuilding.current_tile, pTile) <= tTileRadius))
						{
							yield return tBuilding;
						}
					}
					IEnumerator<Building> enumerator2 = null;
				}
			}
			IEnumerator<MapChunk> enumerator = null;
		}
		else
		{
			foreach (MapChunk tChunk3 in tChunks.LoopRandom(tCount))
			{
				if (tChunk3 != null)
				{
					List<Building> tBuildings2 = tChunk3.objects.buildings_all;
					int i = 0;
					int tLen = tBuildings2.Count;
					while (i < tLen)
					{
						Building tBuilding2 = tBuildings2[i];
						if (tBuilding2.isAlive() && (tTileRadius == 0 || Toolbox.SquaredDistTile(tBuilding2.current_tile, pTile) <= tTileRadius))
						{
							yield return tBuilding2;
						}
						int num = i;
						i = num + 1;
					}
					tBuildings2 = null;
				}
			}
			IEnumerator<MapChunk> enumerator = null;
		}
		yield break;
		yield break;
	}

	// Token: 0x06002D53 RID: 11603 RVA: 0x001611F0 File Offset: 0x0015F3F0
	public static bool isEnemyNearOnSameIsland(Actor pActor, int pChunkRadius = 1)
	{
		foreach (Actor tActor in Finder.getUnitsFromChunk(pActor.current_tile, pChunkRadius, 0f, false))
		{
			if (pActor.isOnSameIsland(tActor) && tActor.kingdom.isEnemy(pActor.kingdom))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002D54 RID: 11604 RVA: 0x00161268 File Offset: 0x0015F468
	public static bool isEnemyNearOnSameIslandAndCarnivore(Actor pActor, int pChunkRadius = 1)
	{
		foreach (Actor tActor in Finder.getUnitsFromChunk(pActor.current_tile, pChunkRadius, 0f, false))
		{
			if (pActor.isOnSameIsland(tActor))
			{
				if (tActor.isCarnivore())
				{
					return true;
				}
				if (tActor.kingdom.isEnemy(pActor.kingdom))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06002D55 RID: 11605 RVA: 0x001612EC File Offset: 0x0015F4EC
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<Actor> getUnitsFromChunk(WorldTile pTile, int pChunkRadius, float pTileRadius = 0f, bool pRandom = false)
	{
		int tX = pTile.chunk.x - pChunkRadius;
		int tY = pTile.chunk.y - pChunkRadius;
		int tWidth = pChunkRadius * 2 + 1;
		int tHeight = pChunkRadius * 2 + 1;
		int tCount = tWidth * tHeight;
		MapChunk[] tChunks = Toolbox.checkArraySize<MapChunk>(Finder._chunks, tCount);
		Finder._chunks = tChunks;
		MapChunkManager tChunkManager = World.world.map_chunk_manager;
		float tTileRadius = pTileRadius * pTileRadius;
		int iChunk = 0;
		for (int iX = 0; iX < tWidth; iX++)
		{
			for (int iY = 0; iY < tHeight; iY++)
			{
				MapChunk tChunk = tChunkManager.get(tX + iX, tY + iY);
				if (tChunk == null)
				{
					tCount--;
				}
				else
				{
					tChunks[iChunk++] = tChunk;
				}
			}
		}
		if (pRandom)
		{
			foreach (MapChunk tChunk2 in tChunks.LoopRandom(tCount))
			{
				if (tChunk2 != null)
				{
					List<Actor> tUnits = tChunk2.objects.units_all;
					foreach (Actor tActor in tUnits.LoopRandom<Actor>())
					{
						if (tActor.isAlive() && (tTileRadius == 0f || (float)Toolbox.SquaredDistTile(tActor.current_tile, pTile) <= tTileRadius))
						{
							yield return tActor;
						}
					}
					IEnumerator<Actor> enumerator2 = null;
				}
			}
			IEnumerator<MapChunk> enumerator = null;
		}
		else
		{
			foreach (MapChunk tChunk3 in tChunks.LoopRandom(tCount))
			{
				if (tChunk3 != null)
				{
					List<Actor> tUnits2 = tChunk3.objects.units_all;
					int i = 0;
					int tLen = tUnits2.Count;
					while (i < tLen)
					{
						Actor tActor2 = tUnits2[i];
						if (tActor2.isAlive() && (tTileRadius == 0f || (float)Toolbox.SquaredDistTile(tActor2.current_tile, pTile) <= tTileRadius))
						{
							yield return tActor2;
						}
						int num = i;
						i = num + 1;
					}
					tUnits2 = null;
				}
			}
			IEnumerator<MapChunk> enumerator = null;
		}
		yield break;
		yield break;
	}

	// Token: 0x06002D56 RID: 11606 RVA: 0x00161314 File Offset: 0x0015F514
	public static List<BaseSimObject> getAllObjectsInChunks(WorldTile pTile, int pTileRadius = 3)
	{
		List<BaseSimObject> tListObjects = Finder._list_objects;
		tListObjects.Clear();
		Finder.fillAllObjectsFromChunk(pTile.chunk, pTile, pTileRadius, tListObjects);
		MapChunk[] tChunks = pTile.chunk.neighbours;
		for (int i = 0; i < tChunks.Length; i++)
		{
			Finder.fillAllObjectsFromChunk(tChunks[i], pTile, pTileRadius, tListObjects);
		}
		return tListObjects;
	}

	// Token: 0x06002D57 RID: 11607 RVA: 0x00161364 File Offset: 0x0015F564
	private static void fillAllObjectsFromChunk(MapChunk pChunk, WorldTile pTile, int pTileRadius, List<BaseSimObject> pListObjects)
	{
		int tTileRadius = pTileRadius * pTileRadius;
		List<long> tKingdoms = pChunk.objects.kingdoms;
		for (int i = 0; i < tKingdoms.Count; i++)
		{
			long tKingdom = tKingdoms[i];
			List<Actor> tUnits = pChunk.objects.getUnits(tKingdom);
			for (int j = 0; j < tUnits.Count; j++)
			{
				BaseSimObject tObject = tUnits[j];
				if (tObject.isAlive() && (pTileRadius == 0 || Toolbox.SquaredDistTile(tObject.current_tile, pTile) <= tTileRadius))
				{
					pListObjects.Add(tObject);
				}
			}
			List<Building> tBuildings = pChunk.objects.getBuildings(tKingdom);
			for (int k = 0; k < tBuildings.Count; k++)
			{
				BaseSimObject tObject2 = tBuildings[k];
				if (tObject2.isAlive() && (pTileRadius == 0 || Toolbox.SquaredDistTile(tObject2.current_tile, pTile) <= tTileRadius))
				{
					pListObjects.Add(tObject2);
				}
			}
		}
	}

	// Token: 0x06002D58 RID: 11608 RVA: 0x00161446 File Offset: 0x0015F646
	internal static IEnumerable<Actor> findSpeciesAroundTileChunk(WorldTile pTile, string pUnitID)
	{
		foreach (Actor tActor in Finder.getUnitsFromChunk(pTile, 1, 0f, false))
		{
			if (!(tActor.a.asset.id != pUnitID))
			{
				yield return tActor;
			}
		}
		IEnumerator<Actor> enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x06002D59 RID: 11609 RVA: 0x0016145D File Offset: 0x0015F65D
	public static Building getClosestBuildingFrom(Actor pActor, IReadOnlyCollection<Building> pBuildingList)
	{
		return Finder.getClosestBuildingFrom(pActor.current_tile, pBuildingList);
	}

	// Token: 0x06002D5A RID: 11610 RVA: 0x0016146C File Offset: 0x0015F66C
	public static Building getClosestBuildingFrom(WorldTile pTile, IReadOnlyCollection<Building> pBuildingList)
	{
		Building tTarget = null;
		float tBestDist = float.MaxValue;
		foreach (Building tB in pBuildingList)
		{
			if (!tB.isRekt() && tB.current_tile.isSameIsland(pTile))
			{
				float tDist = (float)Toolbox.SquaredDistTile(tB.current_tile, pTile);
				if (tDist < tBestDist)
				{
					tTarget = tB;
					tBestDist = tDist;
				}
			}
		}
		return tTarget;
	}

	// Token: 0x06002D5B RID: 11611 RVA: 0x001614E8 File Offset: 0x0015F6E8
	public static void clear()
	{
		Finder._list_objects.Clear();
	}

	// Token: 0x06002D5C RID: 11612 RVA: 0x001614F4 File Offset: 0x0015F6F4
	public static WorldTile findTileInChunk(WorldTile pTile, TileFinderType pTileType)
	{
		ValueTuple<MapChunk[], int> allChunksFromTile = Toolbox.getAllChunksFromTile(pTile);
		MapChunk[] tChunks = allChunksFromTile.Item1;
		int tLength = allChunksFromTile.Item2;
		foreach (MapChunk mapChunk in tChunks.LoopRandom(tLength))
		{
			foreach (MapRegion mapRegion in mapChunk.regions.LoopRandom<MapRegion>())
			{
				foreach (WorldTile tRegionTile in mapRegion.tiles.LoopRandom<WorldTile>())
				{
					switch (pTileType)
					{
					case TileFinderType.FreeTile:
						if (!tRegionTile.isSameIsland(pTile) || tRegionTile.hasBuilding())
						{
							continue;
						}
						if (!tRegionTile.Type.ground)
						{
							continue;
						}
						break;
					case TileFinderType.Sand:
						if (!tRegionTile.Type.sand)
						{
							continue;
						}
						break;
					case TileFinderType.Water:
						if (tRegionTile.isTargeted())
						{
							continue;
						}
						if (!tRegionTile.Type.ocean)
						{
							continue;
						}
						break;
					case TileFinderType.Grass:
						if (!tRegionTile.isSameIsland(pTile) || tRegionTile.isTargeted() || !tRegionTile.Type.grass)
						{
							continue;
						}
						if (tRegionTile.hasBuilding())
						{
							continue;
						}
						break;
					case TileFinderType.Biome:
						if (!tRegionTile.isSameIsland(pTile) || tRegionTile.isTargeted() || !tRegionTile.Type.is_biome)
						{
							continue;
						}
						if (tRegionTile.hasBuilding())
						{
							continue;
						}
						break;
					case TileFinderType.NewRoad:
						goto IL_17D;
					case TileFinderType.Dirt:
						if (!tRegionTile.isSameIsland(pTile) || tRegionTile.isTargeted() || !tRegionTile.Type.can_be_farm)
						{
							continue;
						}
						if (tRegionTile.hasBuilding())
						{
							continue;
						}
						break;
					default:
						goto IL_17D;
					}
					IL_187:
					return tRegionTile;
					IL_17D:
					if (tRegionTile.isSameIsland(pTile))
					{
						goto IL_187;
					}
				}
			}
		}
		return null;
	}

	// Token: 0x040022A4 RID: 8868
	private static readonly List<BaseSimObject> _list_objects = new List<BaseSimObject>(4096);

	// Token: 0x040022A5 RID: 8869
	private static MapChunk[] _chunks;
}
