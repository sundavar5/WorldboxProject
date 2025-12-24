using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityPools;

// Token: 0x02000423 RID: 1059
public class TileZone : IDisposable
{
	// Token: 0x060024A9 RID: 9385 RVA: 0x00130AD8 File Offset: 0x0012ECD8
	public bool checkShouldReRender(int pHashCode, int pID, int pColorAssetID, bool pLastAnimated)
	{
		if (this.last_drawn_id == pID && this.last_drawn_hashcode == pHashCode)
		{
			bool? last_animated = this._last_animated;
			if ((last_animated.GetValueOrDefault() == pLastAnimated & last_animated != null) && this.last_color_asset_id == pColorAssetID)
			{
				return false;
			}
		}
		this.last_drawn_id = pID;
		this.last_drawn_hashcode = pHashCode;
		this._last_animated = new bool?(pLastAnimated);
		this.last_color_asset_id = pColorAssetID;
		return true;
	}

	// Token: 0x060024AA RID: 9386 RVA: 0x00130B43 File Offset: 0x0012ED43
	public void resetRenderHelpers()
	{
		this.last_color_asset_id = 0;
		this.last_drawn_hashcode = 0;
		this.last_drawn_id = 0;
		this._last_animated = null;
	}

	// Token: 0x060024AB RID: 9387 RVA: 0x00130B66 File Offset: 0x0012ED66
	public void clearDebug()
	{
		this.debug_args = null;
		this.debug_show = false;
	}

	// Token: 0x060024AC RID: 9388 RVA: 0x00130B76 File Offset: 0x0012ED76
	public void showDebug([TupleElementNames(new string[]
	{
		"key",
		"value"
	})] params ValueTuple<string, int>[] pArgs)
	{
		if (!DebugConfig.isOn(DebugOption.DebugZones))
		{
			return;
		}
		this.debug_show = true;
		this.debug_args = pArgs;
	}

	// Token: 0x170001FD RID: 509
	// (get) Token: 0x060024AD RID: 9389 RVA: 0x00130B90 File Offset: 0x0012ED90
	public WorldTile top_left_corner_tile
	{
		get
		{
			return this.tiles.Last<WorldTile>();
		}
	}

	// Token: 0x170001FE RID: 510
	// (get) Token: 0x060024AE RID: 9390 RVA: 0x00130B9D File Offset: 0x0012ED9D
	private CityPlaceFinder _city_place_finder
	{
		get
		{
			return World.world.city_zone_helper.city_place_finder;
		}
	}

	// Token: 0x060024AF RID: 9391 RVA: 0x00130BB0 File Offset: 0x0012EDB0
	public bool checkCanSettleInThisBiomes(Subspecies pSubspecies)
	{
		int tAdapted = 0;
		int tNotAdapted = 0;
		int tSoil = 0;
		foreach (KeyValuePair<TileTypeBase, HashSet<WorldTile>> tPair in this._tile_types)
		{
			TileTypeBase tType = tPair.Key;
			if (tType.biome_build_check)
			{
				if (tType.soil)
				{
					tSoil += tPair.Value.Count;
				}
				else if (!tType.is_biome)
				{
					tAdapted += tPair.Value.Count;
				}
				else
				{
					string tAddaptabtedTag = tType.only_allowed_to_build_with_tag;
					if (string.IsNullOrEmpty(tAddaptabtedTag))
					{
						tAdapted += tPair.Value.Count;
					}
					else if (!pSubspecies.hasMetaTag(tAddaptabtedTag))
					{
						tNotAdapted += tPair.Value.Count;
					}
					else
					{
						tAdapted += tPair.Value.Count;
					}
				}
			}
		}
		tSoil -= tAdapted;
		tSoil -= tNotAdapted;
		bool tResult = tSoil > tNotAdapted || tNotAdapted <= tAdapted;
		TileZone.debug_adapted = tAdapted;
		TileZone.debug_not_adapted = tNotAdapted;
		TileZone.debug_soil = tSoil;
		TileZone.debug_can_settle = tResult;
		return tResult;
	}

	// Token: 0x060024B0 RID: 9392 RVA: 0x00130CD8 File Offset: 0x0012EED8
	public bool hasAnyBuildings()
	{
		return this.buildings_all.Count > 0;
	}

	// Token: 0x060024B1 RID: 9393 RVA: 0x00130CE8 File Offset: 0x0012EEE8
	public HashSet<Building> getHashset(BuildingList pType)
	{
		return this._building_hashset_types_array[(int)pType];
	}

	// Token: 0x060024B2 RID: 9394 RVA: 0x00130CF4 File Offset: 0x0012EEF4
	private HashSet<Building> getHashsetOrCreate(BuildingList pType)
	{
		HashSet<Building> tList = this._building_hashset_types_array[(int)pType];
		if (tList == null)
		{
			tList = UnsafeCollectionPool<HashSet<Building>, Building>.Get();
			this._building_hashset_types_array[(int)pType] = tList;
		}
		return tList;
	}

	// Token: 0x060024B3 RID: 9395 RVA: 0x00130D1F File Offset: 0x0012EF1F
	public bool hasAnyBuildingsInSet(BuildingList pType)
	{
		HashSet<Building> hashset = this.getHashset(pType);
		return hashset != null && hashset.Count > 0;
	}

	// Token: 0x060024B4 RID: 9396 RVA: 0x00130D38 File Offset: 0x0012EF38
	public int countBuildingsType(BuildingList pType)
	{
		HashSet<Building> tSet = this.getHashset(pType);
		if (tSet == null)
		{
			return 0;
		}
		return tSet.Count;
	}

	// Token: 0x060024B5 RID: 9397 RVA: 0x00130D58 File Offset: 0x0012EF58
	public bool isGoodForNewCity(Actor pActor)
	{
		if (!this.checkCanSettleInThisBiomes(pActor.subspecies))
		{
			return false;
		}
		if (this._city_place_finder.isDirty())
		{
			if (this.hasCity())
			{
				return false;
			}
			if (pActor.hasCity() && pActor.city.neighbour_zones.Contains(this))
			{
				return false;
			}
		}
		return this.isGoodForNewCity();
	}

	// Token: 0x060024B6 RID: 9398 RVA: 0x00130DB0 File Offset: 0x0012EFB0
	public bool isGoodForNewCity()
	{
		if (this.hasCity())
		{
			return false;
		}
		if (this._city_place_finder.isDirty())
		{
			TileZone[] tNeighbours = this.neighbours_all;
			int tCount = tNeighbours.Length;
			for (int i = 0; i < tCount; i++)
			{
				if (tNeighbours[i].hasCity())
				{
					return false;
				}
			}
			this._city_place_finder.recalc();
		}
		return this._good_for_new_city;
	}

	// Token: 0x060024B7 RID: 9399 RVA: 0x00130E08 File Offset: 0x0012F008
	public void setGoodForNewCity(bool pValue)
	{
		this._good_for_new_city = pValue;
	}

	// Token: 0x060024B8 RID: 9400 RVA: 0x00130E11 File Offset: 0x0012F011
	public bool hasCity()
	{
		return this.city != null;
	}

	// Token: 0x060024B9 RID: 9401 RVA: 0x00130E1C File Offset: 0x0012F01C
	public bool isSameCityHere(City pCity)
	{
		return this.city == pCity;
	}

	// Token: 0x060024BA RID: 9402 RVA: 0x00130E28 File Offset: 0x0012F028
	public static bool hasZonesConnectedViaRegions(TileZone pZone1, TileZone pZone2, MapRegion pMainRegion1, ListPool<MapRegion> pListToFill)
	{
		if (pZone2.tiles_with_ground == 0)
		{
			return false;
		}
		if (!pMainRegion1.isTypeGround())
		{
			return false;
		}
		MapChunk tChunk = pZone1.chunk;
		MapChunk tChunk2 = pZone2.chunk;
		if (tChunk == tChunk2)
		{
			if (tChunk.regions.Count == 1 && pMainRegion1.isTypeGround())
			{
				pListToFill.Add(pMainRegion1);
				return true;
			}
		}
		else if (tChunk.regions.Count == 1 && tChunk2.regions.Count == 1)
		{
			MapRegion tReg2 = tChunk2.regions[0];
			if (pMainRegion1.isTypeGround() && tReg2.isTypeGround())
			{
				pListToFill.Add(tReg2);
				return true;
			}
		}
		List<MapRegion> tChunk2Regions = tChunk2.regions;
		for (int i = 0; i < tChunk2Regions.Count; i++)
		{
			MapRegion tRegion2 = tChunk2Regions[i];
			if (tRegion2.isTypeGround() && tRegion2.zones.Contains(pZone2))
			{
				if (pMainRegion1 == tRegion2)
				{
					pListToFill.Add(pMainRegion1);
					return true;
				}
				if (pMainRegion1.island == tRegion2.island && pMainRegion1.hasNeighbour(tRegion2))
				{
					pListToFill.Add(tRegion2);
				}
			}
		}
		return pListToFill.Count > 0;
	}

	// Token: 0x060024BB RID: 9403 RVA: 0x00130F32 File Offset: 0x0012F132
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasLiquid()
	{
		return this.tiles_with_liquid > 0;
	}

	// Token: 0x060024BC RID: 9404 RVA: 0x00130F40 File Offset: 0x0012F140
	public bool hasBuildingOf(City pCity)
	{
		foreach (Building tBuilding in this.getHashsetOrCreate(BuildingList.Civs))
		{
			if (tBuilding.isUsable() && tBuilding.city == pCity)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060024BD RID: 9405 RVA: 0x00130FA8 File Offset: 0x0012F1A8
	public void addTileType(TileTypeBase pType, WorldTile pTile)
	{
		HashSet<WorldTile> tList;
		if (!this._tile_types.TryGetValue(pType, out tList))
		{
			tList = UnsafeCollectionPool<HashSet<WorldTile>, WorldTile>.Get();
			this._tile_types.Add(pType, tList);
		}
		tList.Add(pTile);
		this.chunk.setTileTypesDirty();
	}

	// Token: 0x060024BE RID: 9406 RVA: 0x00130FEC File Offset: 0x0012F1EC
	public void removeTileType(TileTypeBase pType, WorldTile pTile)
	{
		HashSet<WorldTile> tList;
		if (!this._tile_types.TryGetValue(pType, out tList))
		{
			return;
		}
		tList.Remove(pTile);
		this.chunk.setTileTypesDirty();
	}

	// Token: 0x060024BF RID: 9407 RVA: 0x00131020 File Offset: 0x0012F220
	public HashSet<WorldTile> getTilesOfType(TileTypeBase pType)
	{
		HashSet<WorldTile> tList;
		if (this._tile_types.TryGetValue(pType, out tList))
		{
			return tList;
		}
		return null;
	}

	// Token: 0x060024C0 RID: 9408 RVA: 0x00131040 File Offset: 0x0012F240
	public bool hasTilesOfType(TileTypeBase pType)
	{
		HashSet<WorldTile> tList;
		return this._tile_types.TryGetValue(pType, out tList) && tList.Count > 0;
	}

	// Token: 0x060024C1 RID: 9409 RVA: 0x00131068 File Offset: 0x0012F268
	internal void addTile(WorldTile pTile, int pX, int pY)
	{
		this.tiles[this.tiles.FreeIndex<WorldTile>()] = pTile;
		if (pX == 0)
		{
			pTile.world_tile_zone_border.border_left = true;
			pTile.world_tile_zone_border.border = true;
		}
		else if (pX == 7)
		{
			pTile.world_tile_zone_border.border_right = true;
			pTile.world_tile_zone_border.border = true;
		}
		if (pY == 0)
		{
			pTile.world_tile_zone_border.border_down = true;
			pTile.world_tile_zone_border.border = true;
			return;
		}
		if (pY == 7)
		{
			pTile.world_tile_zone_border.border = true;
			pTile.world_tile_zone_border.border_up = true;
		}
	}

	// Token: 0x060024C2 RID: 9410 RVA: 0x001310F9 File Offset: 0x0012F2F9
	internal void setCity(City pCity)
	{
		this.setDirty(true);
		if (this.hasCity() && this.city != pCity)
		{
			World.world.cities.setDirtyBuildings(this.city);
		}
		this.city = pCity;
	}

	// Token: 0x060024C3 RID: 9411 RVA: 0x0013112F File Offset: 0x0012F32F
	public bool isDirty()
	{
		return this._dirty;
	}

	// Token: 0x060024C4 RID: 9412 RVA: 0x00131137 File Offset: 0x0012F337
	public void setDirty(bool pValue)
	{
		this._dirty = pValue;
		if (pValue)
		{
			BuildingZonesSystem.setDirty();
		}
		if (this.hasCity())
		{
			World.world.cities.setDirtyBuildings(this.city);
			this.city.setStatusDirty();
		}
	}

	// Token: 0x060024C5 RID: 9413 RVA: 0x00131170 File Offset: 0x0012F370
	public void addBuildingMain(Building pBuilding)
	{
		this.buildings_all.Add(pBuilding);
		this.setDirty(true);
	}

	// Token: 0x060024C6 RID: 9414 RVA: 0x00131186 File Offset: 0x0012F386
	public void removeBuildingMain(Building pBuilding)
	{
		this.buildings_all.Remove(pBuilding);
		this.setDirty(true);
	}

	// Token: 0x060024C7 RID: 9415 RVA: 0x0013119C File Offset: 0x0012F39C
	internal void addBuildingToSet(Building pBuilding)
	{
		HashSet<Building> tSet;
		if (pBuilding.isRuin())
		{
			tSet = this.getHashsetOrCreate(BuildingList.Ruins);
		}
		else if (pBuilding.asset.city_building)
		{
			if (pBuilding.isAbandoned())
			{
				tSet = this.getHashsetOrCreate(BuildingList.Abandoned);
			}
			else
			{
				tSet = this.getHashsetOrCreate(BuildingList.Civs);
			}
		}
		else
		{
			switch (pBuilding.asset.building_type)
			{
			case BuildingType.Building_Tree:
				tSet = this.getHashsetOrCreate(BuildingList.Trees);
				break;
			case BuildingType.Building_Fruits:
				tSet = this.getHashsetOrCreate(BuildingList.Food);
				break;
			case BuildingType.Building_Hives:
				tSet = this.getHashsetOrCreate(BuildingList.Hives);
				break;
			case BuildingType.Building_Poops:
				tSet = this.getHashsetOrCreate(BuildingList.Poops);
				break;
			case BuildingType.Building_Wheat:
				tSet = this.getHashsetOrCreate(BuildingList.Wheat);
				break;
			case BuildingType.Building_Plant:
				tSet = this.getHashsetOrCreate(BuildingList.Flora);
				break;
			case BuildingType.Building_Mineral:
				tSet = this.getHashsetOrCreate(BuildingList.Minerals);
				break;
			default:
				tSet = this.getHashsetOrCreate(BuildingList.Civs);
				break;
			}
		}
		tSet.Add(pBuilding);
	}

	// Token: 0x060024C8 RID: 9416 RVA: 0x00131278 File Offset: 0x0012F478
	internal void addNeighbour(TileZone pNeighbour, TileDirection pDirection, IList<TileZone> pNeighbours, IList<TileZone> pNeighboursAll, bool pDiagonal = false)
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
			this.zone_left = pNeighbour;
			return;
		case TileDirection.Right:
			this.zone_right = pNeighbour;
			return;
		case TileDirection.Up:
			this.zone_up = pNeighbour;
			return;
		case TileDirection.Down:
			this.zone_down = pNeighbour;
			return;
		default:
			return;
		}
	}

	// Token: 0x060024C9 RID: 9417 RVA: 0x001312D9 File Offset: 0x0012F4D9
	public bool canStartCityHere()
	{
		return !this.hasCity() && this.tiles_with_ground >= 64 && !this.hasTilesOfType(TileLibrary.hills);
	}

	// Token: 0x060024CA RID: 9418 RVA: 0x00131304 File Offset: 0x0012F504
	public bool hasLava()
	{
		foreach (TileType tType in TileLibrary.lava_types)
		{
			if (this.hasTilesOfType(tType))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060024CB RID: 9419 RVA: 0x00131360 File Offset: 0x0012F560
	public int countLava()
	{
		int tResult = 0;
		foreach (TileType tType in TileLibrary.lava_types)
		{
			HashSet<WorldTile> tTiles = this.getTilesOfType(tType);
			if (tTiles != null)
			{
				tResult += tTiles.Count;
			}
		}
		return tResult;
	}

	// Token: 0x060024CC RID: 9420 RVA: 0x001313C4 File Offset: 0x0012F5C4
	public IEnumerable<WorldTile> loopLava()
	{
		foreach (TileType tType in TileLibrary.lava_types.LoopRandom<TileType>())
		{
			HashSet<WorldTile> tTiles = this.getTilesOfType(tType);
			if (tTiles != null)
			{
				foreach (WorldTile tTile in tTiles)
				{
					yield return tTile;
				}
				HashSet<WorldTile>.Enumerator enumerator2 = default(HashSet<WorldTile>.Enumerator);
			}
		}
		IEnumerator<TileType> enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x060024CD RID: 9421 RVA: 0x001313D4 File Offset: 0x0012F5D4
	public bool goodForExpansion()
	{
		return !this.hasLava();
	}

	// Token: 0x060024CE RID: 9422 RVA: 0x001313E4 File Offset: 0x0012F5E4
	public bool hasReachedBuildingLimit(WorldTile pTile, BuildingAsset pAsset)
	{
		int tLimitPerZone = pAsset.limit_per_zone;
		if (pTile.isTileRank(TileRank.Low) && pAsset.building_type == BuildingType.Building_Tree)
		{
			tLimitPerZone = (int)((float)tLimitPerZone * 0.5f);
			if (tLimitPerZone == 0)
			{
				tLimitPerZone = 1;
			}
		}
		if (WorldLawLibrary.world_law_spread_density_high.isEnabled())
		{
			tLimitPerZone = (int)((float)tLimitPerZone * 1.5f);
		}
		if (pAsset.limit_in_radius > 0)
		{
			using (IEnumerator<Building> enumerator = World.world.buildings.getBuildingFromZones(pTile, (float)pAsset.limit_in_radius).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.asset == pAsset)
					{
						return true;
					}
				}
			}
		}
		BuildingList? tBuildingList = null;
		switch (pAsset.building_type)
		{
		case BuildingType.Building_Tree:
			tBuildingList = new BuildingList?(BuildingList.Trees);
			break;
		case BuildingType.Building_Fruits:
			tBuildingList = new BuildingList?(BuildingList.Food);
			break;
		case BuildingType.Building_Hives:
			tBuildingList = new BuildingList?(BuildingList.Hives);
			break;
		case BuildingType.Building_Poops:
			tBuildingList = new BuildingList?(BuildingList.Poops);
			break;
		case BuildingType.Building_Plant:
			tBuildingList = new BuildingList?(BuildingList.Flora);
			break;
		case BuildingType.Building_Mineral:
			tBuildingList = new BuildingList?(BuildingList.Minerals);
			break;
		}
		return tBuildingList != null && this.countBuildingsType(tBuildingList.Value) >= tLimitPerZone;
	}

	// Token: 0x060024CF RID: 9423 RVA: 0x00131520 File Offset: 0x0012F720
	public void clearBuildingLists()
	{
		this.buildings_render_list.Clear();
		for (int i = 0; i < this._building_hashset_types_array.Length; i++)
		{
			HashSet<Building> tCollection = this._building_hashset_types_array[i];
			if (tCollection != null)
			{
				tCollection.Clear();
				UnsafeCollectionPool<HashSet<Building>, Building>.Release(tCollection);
				this._building_hashset_types_array[i] = null;
			}
		}
	}

	// Token: 0x060024D0 RID: 9424 RVA: 0x0013156C File Offset: 0x0012F76C
	public Dictionary<TileTypeBase, HashSet<WorldTile>> getTileTypes()
	{
		return this._tile_types;
	}

	// Token: 0x060024D1 RID: 9425 RVA: 0x00131574 File Offset: 0x0012F774
	public void clear()
	{
		foreach (HashSet<WorldTile> hashSet in this._tile_types.Values)
		{
			hashSet.Clear();
		}
		this.clearBuildingLists();
		this.buildings_all.Clear();
		this.city = null;
		this._good_for_new_city = false;
		this.resetRenderHelpers();
	}

	// Token: 0x060024D2 RID: 9426 RVA: 0x001315F0 File Offset: 0x0012F7F0
	public void Dispose()
	{
		this.clear();
		this.tiles.Clear<WorldTile>();
		this.neighbours.Clear<TileZone>();
		this.neighbours_all.Clear<TileZone>();
		this.neighbours = null;
		this.neighbours_all = null;
		this.zone_up = null;
		this.zone_down = null;
		this.zone_left = null;
		this.zone_right = null;
		this.centerTile = null;
		foreach (HashSet<WorldTile> hashSet in this._tile_types.Values)
		{
			hashSet.Clear();
			UnsafeCollectionPool<HashSet<WorldTile>, WorldTile>.Release(hashSet);
		}
		this._tile_types.Clear();
	}

	// Token: 0x060024D3 RID: 9427 RVA: 0x001316B0 File Offset: 0x0012F8B0
	public bool canBeClaimedByCity(City pCity)
	{
		return (!pCity.hasLeader() || this.checkCanSettleInThisBiomes(pCity.leader.subspecies)) && (!this.hasCity() || (!this.isSameCityHere(pCity) && WorldLawLibrary.world_law_border_stealing.isEnabled() && (!pCity.hasKingdom() || !this.city.hasKingdom() || pCity.kingdom.isEnemy(this.city.kingdom)) && pCity.kingdom != this.city.kingdom));
	}

	// Token: 0x060024D4 RID: 9428 RVA: 0x00131742 File Offset: 0x0012F942
	public bool isZoneOnFire()
	{
		return WorldBehaviourActionFire.countFires(this) > 0;
	}

	// Token: 0x060024D5 RID: 9429 RVA: 0x0013174D File Offset: 0x0012F94D
	public WorldTile getRandomTile()
	{
		return this.tiles.GetRandom<WorldTile>();
	}

	// Token: 0x170001FF RID: 511
	// (get) Token: 0x060024D6 RID: 9430 RVA: 0x0013175A File Offset: 0x0012F95A
	public MapChunk chunk
	{
		get
		{
			return this.centerTile.chunk;
		}
	}

	// Token: 0x060024D7 RID: 9431 RVA: 0x00131768 File Offset: 0x0012F968
	public int countNotNullTypes()
	{
		int tResult = 0;
		for (int i = 0; i < this._building_hashset_types_array.Length; i++)
		{
			if (this._building_hashset_types_array[i] != null)
			{
				tResult++;
			}
		}
		return tResult;
	}

	// Token: 0x060024D8 RID: 9432 RVA: 0x0013179C File Offset: 0x0012F99C
	public IMetaObject getClanOnZone(int pZoneOption)
	{
		IMetaObject tMeta;
		if (pZoneOption != 0)
		{
			if (pZoneOption != 1)
			{
				tMeta = this.getDefaultMetaOnZone();
			}
			else
			{
				City tCity = this.city;
				if (tCity.isRekt())
				{
					return null;
				}
				tMeta = tCity.getRoyalClan();
			}
		}
		else
		{
			City tCity2 = this.city;
			if (tCity2.isRekt())
			{
				return null;
			}
			tMeta = tCity2.kingdom.getKingClan();
		}
		return tMeta;
	}

	// Token: 0x060024D9 RID: 9433 RVA: 0x001317F4 File Offset: 0x0012F9F4
	public IMetaObject getFamilyOnZone(int pZoneOption)
	{
		IMetaObject tMeta;
		if (pZoneOption != 0)
		{
			if (pZoneOption != 1)
			{
				tMeta = this.getDefaultMetaOnZone();
			}
			else
			{
				City tCity = this.city;
				if (tCity.isRekt())
				{
					return null;
				}
				Actor leader = tCity.leader;
				tMeta = ((leader != null) ? leader.family : null);
			}
		}
		else
		{
			City tCity2 = this.city;
			if (tCity2.isRekt())
			{
				return null;
			}
			Actor king = tCity2.kingdom.king;
			tMeta = ((king != null) ? king.family : null);
		}
		return tMeta;
	}

	// Token: 0x060024DA RID: 9434 RVA: 0x00131863 File Offset: 0x0012FA63
	public IMetaObject getArmyOnZone(int pZoneOption)
	{
		return this.getDefaultMetaOnZone();
	}

	// Token: 0x060024DB RID: 9435 RVA: 0x0013186C File Offset: 0x0012FA6C
	public IMetaObject getCityOnZone(int pZoneOption)
	{
		IMetaObject tMeta;
		if (pZoneOption == 0)
		{
			City tCity = this.city;
			if (tCity.isRekt())
			{
				return null;
			}
			tMeta = tCity;
		}
		else
		{
			tMeta = this.getDefaultMetaOnZone();
		}
		return tMeta;
	}

	// Token: 0x060024DC RID: 9436 RVA: 0x0013189C File Offset: 0x0012FA9C
	public IMetaObject getKingdomOnZone(int pZoneOption)
	{
		IMetaObject tMeta;
		if (pZoneOption == 0)
		{
			City tCity = this.city;
			if (tCity.isRekt())
			{
				return null;
			}
			tMeta = tCity.kingdom;
		}
		else
		{
			tMeta = this.getDefaultMetaOnZone();
		}
		return tMeta;
	}

	// Token: 0x060024DD RID: 9437 RVA: 0x001318D0 File Offset: 0x0012FAD0
	public IMetaObject getLanguageOnZone(int pZoneOption)
	{
		IMetaObject tMeta;
		if (pZoneOption != 0)
		{
			if (pZoneOption != 1)
			{
				tMeta = this.getDefaultMetaOnZone();
			}
			else
			{
				City tCity = this.city;
				if (tCity.isRekt())
				{
					return null;
				}
				tMeta = tCity.getLanguage();
			}
		}
		else
		{
			City tCity2 = this.city;
			if (tCity2.isRekt())
			{
				return null;
			}
			tMeta = tCity2.kingdom.getLanguage();
		}
		return tMeta;
	}

	// Token: 0x060024DE RID: 9438 RVA: 0x00131928 File Offset: 0x0012FB28
	public IMetaObject getReligionOnZone(int pZoneOption)
	{
		IMetaObject tMeta;
		if (pZoneOption != 0)
		{
			if (pZoneOption != 1)
			{
				tMeta = this.getDefaultMetaOnZone();
			}
			else
			{
				City tCity = this.city;
				if (tCity.isRekt())
				{
					return null;
				}
				tMeta = tCity.getReligion();
			}
		}
		else
		{
			City tCity2 = this.city;
			if (tCity2.isRekt())
			{
				return null;
			}
			tMeta = tCity2.kingdom.getReligion();
		}
		return tMeta;
	}

	// Token: 0x060024DF RID: 9439 RVA: 0x00131980 File Offset: 0x0012FB80
	public IMetaObject getSubspeciesOnZone(int pZoneOption)
	{
		IMetaObject tMeta;
		if (pZoneOption != 0)
		{
			if (pZoneOption != 1)
			{
				tMeta = this.getDefaultMetaOnZone();
			}
			else
			{
				City tCity = this.city;
				if (tCity.isRekt())
				{
					return null;
				}
				tMeta = tCity.getMainSubspecies();
			}
		}
		else
		{
			City tCity2 = this.city;
			if (tCity2.isRekt())
			{
				return null;
			}
			tMeta = tCity2.kingdom.getMainSubspecies();
		}
		return tMeta;
	}

	// Token: 0x060024E0 RID: 9440 RVA: 0x001319D8 File Offset: 0x0012FBD8
	public IMetaObject getCultureOnZone(int pZoneOption)
	{
		IMetaObject tMeta;
		if (pZoneOption != 0)
		{
			if (pZoneOption != 1)
			{
				tMeta = this.getDefaultMetaOnZone();
			}
			else
			{
				City tCity = this.city;
				if (tCity.isRekt())
				{
					return null;
				}
				tMeta = tCity.getCulture();
			}
		}
		else
		{
			City tCity2 = this.city;
			if (tCity2.isRekt())
			{
				return null;
			}
			tMeta = tCity2.kingdom.getCulture();
		}
		return tMeta;
	}

	// Token: 0x060024E1 RID: 9441 RVA: 0x00131A30 File Offset: 0x0012FC30
	public Alliance getAllianceOnZone(int pZoneOption)
	{
		City tCity = this.city;
		if (tCity == null)
		{
			return null;
		}
		return tCity.getAlliance();
	}

	// Token: 0x060024E2 RID: 9442 RVA: 0x00131A50 File Offset: 0x0012FC50
	private IMetaObject getDefaultMetaOnZone()
	{
		ZoneMetaData tData = ZoneMetaDataVisualizer.getZoneMetaData(this);
		if (tData.meta_object == null)
		{
			return null;
		}
		if (!tData.meta_object.isAlive())
		{
			return null;
		}
		return tData.meta_object;
	}

	// Token: 0x04001A82 RID: 6786
	private const int MAX_BUILDING_COLLECTIONS = 16;

	// Token: 0x04001A83 RID: 6787
	public int last_drawn_id;

	// Token: 0x04001A84 RID: 6788
	public int last_drawn_hashcode;

	// Token: 0x04001A85 RID: 6789
	public int last_color_asset_id;

	// Token: 0x04001A86 RID: 6790
	private bool? _last_animated;

	// Token: 0x04001A87 RID: 6791
	public ValueTuple<string, int>[] debug_args;

	// Token: 0x04001A88 RID: 6792
	public bool debug_show;

	// Token: 0x04001A89 RID: 6793
	private bool _dirty;

	// Token: 0x04001A8A RID: 6794
	public bool visible;

	// Token: 0x04001A8B RID: 6795
	public bool visible_main_centered;

	// Token: 0x04001A8C RID: 6796
	public int x;

	// Token: 0x04001A8D RID: 6797
	public int y;

	// Token: 0x04001A8E RID: 6798
	public int id;

	// Token: 0x04001A8F RID: 6799
	public readonly WorldTile[] tiles = new WorldTile[64];

	// Token: 0x04001A90 RID: 6800
	public Color debug_zone_color;

	// Token: 0x04001A91 RID: 6801
	[CanBeNull]
	public City city;

	// Token: 0x04001A92 RID: 6802
	public TileZone[] neighbours;

	// Token: 0x04001A93 RID: 6803
	public TileZone[] neighbours_all;

	// Token: 0x04001A94 RID: 6804
	public bool world_edge;

	// Token: 0x04001A95 RID: 6805
	public WorldTile centerTile;

	// Token: 0x04001A96 RID: 6806
	public int tiles_with_liquid;

	// Token: 0x04001A97 RID: 6807
	public int tiles_with_ground;

	// Token: 0x04001A98 RID: 6808
	public readonly List<Building> buildings_render_list = new List<Building>();

	// Token: 0x04001A99 RID: 6809
	public readonly HashSet<Building> buildings_all = new HashSet<Building>();

	// Token: 0x04001A9A RID: 6810
	private readonly HashSet<Building>[] _building_hashset_types_array = new HashSet<Building>[16];

	// Token: 0x04001A9B RID: 6811
	internal TileZone zone_up;

	// Token: 0x04001A9C RID: 6812
	internal TileZone zone_down;

	// Token: 0x04001A9D RID: 6813
	internal TileZone zone_left;

	// Token: 0x04001A9E RID: 6814
	internal TileZone zone_right;

	// Token: 0x04001A9F RID: 6815
	private readonly Dictionary<TileTypeBase, HashSet<WorldTile>> _tile_types = new Dictionary<TileTypeBase, HashSet<WorldTile>>();

	// Token: 0x04001AA0 RID: 6816
	private bool _good_for_new_city;

	// Token: 0x04001AA1 RID: 6817
	public static int debug_adapted;

	// Token: 0x04001AA2 RID: 6818
	public static int debug_not_adapted;

	// Token: 0x04001AA3 RID: 6819
	public static int debug_soil;

	// Token: 0x04001AA4 RID: 6820
	public static bool debug_can_settle;
}
