using System;
using System.Collections.Generic;
using tools;
using UnityEngine;
using UnityPools;

namespace ai.behaviours
{
	// Token: 0x02000969 RID: 2409
	public class CityBehBuild : BehaviourActionCity
	{
		// Token: 0x06004690 RID: 18064 RVA: 0x001DEE16 File Offset: 0x001DD016
		public override bool shouldRetry(City pCity)
		{
			return false;
		}

		// Token: 0x06004691 RID: 18065 RVA: 0x001DEE1C File Offset: 0x001DD01C
		public override BehResult execute(City pCity)
		{
			if (pCity.timer_build > 0f)
			{
				return BehResult.Continue;
			}
			if (!DebugConfig.isOn(DebugOption.SystemBuildTick))
			{
				return BehResult.Continue;
			}
			if (pCity.isGettingCaptured())
			{
				return BehResult.Continue;
			}
			if (pCity.isInDanger())
			{
				return BehResult.Continue;
			}
			pCity.timer_build = 5f;
			CityBehBuild.buildTick(pCity);
			if (DebugConfig.isOn(DebugOption.CityFastUpgrades))
			{
				pCity.timer_build = 0.2f;
			}
			return BehResult.Continue;
		}

		// Token: 0x06004692 RID: 18066 RVA: 0x001DEE84 File Offset: 0x001DD084
		public static bool buildTick(City pCity)
		{
			if (pCity.buildings.Count > 2 && pCity.hasCulture() && pCity.culture.canUseRoads())
			{
				Building tBuilding = pCity.buildings.GetRandom<Building>();
				if (tBuilding != null)
				{
					CityBehBuild.makeRoadsBuildings(pCity, tBuilding);
				}
			}
			if (pCity.isCityUnderDangerFire() && pCity.hasLeader() && !pCity.leader.isImmuneToFire())
			{
				return false;
			}
			if (pCity.under_construction_building == null)
			{
				foreach (Building tBuilding2 in pCity.buildings)
				{
					if (tBuilding2.isUnderConstruction())
					{
						pCity.under_construction_building = tBuilding2;
						break;
					}
				}
			}
			if (pCity.under_construction_building != null)
			{
				return false;
			}
			CityBehBuild.calcPossibleBuildings(pCity);
			if (DebugConfig.isOn(DebugOption.OverlayCity))
			{
				pCity._debug_last_possible_build_orders = string.Empty;
				pCity._debug_last_possible_build_orders_no_resources = string.Empty;
				pCity._debug_last_build_order_try = string.Empty;
			}
			if (CityBehBuild._possible_buildings.Count == 0)
			{
				return false;
			}
			BuildOrder tNextBuildOrder = CityBehBuild._possible_buildings.GetRandom<BuildOrder>();
			if (DebugConfig.isOn(DebugOption.OverlayCity))
			{
				foreach (BuildOrder iOrder in CityBehBuild._possible_buildings)
				{
					pCity._debug_last_possible_build_orders = pCity._debug_last_possible_build_orders + (iOrder.upgrade ? "U-" : "") + iOrder.id + "; ";
				}
				foreach (BuildOrder iOrder2 in CityBehBuild._possible_buildings_no_resources)
				{
					pCity._debug_last_possible_build_orders_no_resources = pCity._debug_last_possible_build_orders_no_resources + (iOrder2.upgrade ? "U-" : "") + iOrder2.id + "; ";
				}
				pCity._debug_last_build_order_try = (tNextBuildOrder.upgrade ? "U-" : "") + tNextBuildOrder.id;
			}
			CityBehBuild._possible_buildings_no_resources.Clear();
			CityBehBuild._possible_buildings.Clear();
			if (tNextBuildOrder.upgrade)
			{
				List<Building> tList = pCity.getBuildingListOfID(tNextBuildOrder.getBuildingAsset(pCity, null).id);
				if (tList == null)
				{
					return false;
				}
				Building tBuildToUpgrade = tList.GetRandom<Building>();
				return tBuildToUpgrade != null && CityBehBuild.upgradeBuilding(tBuildToUpgrade, pCity);
			}
			else
			{
				Building tNewBuild = CityBehBuild.tryToBuild(pCity, tNextBuildOrder.getBuildingAsset(pCity, null));
				if (tNewBuild == null)
				{
					return false;
				}
				if (DebugConfig.isOn(DebugOption.CityFastConstruction))
				{
					if (tNewBuild != null)
					{
						tNewBuild.updateBuild(1000);
					}
					pCity.under_construction_building = null;
				}
				if (pCity.hasCulture())
				{
					pCity.culture.canUseRoads();
				}
				return true;
			}
		}

		// Token: 0x06004693 RID: 18067 RVA: 0x001DF144 File Offset: 0x001DD344
		private void upgradeRandomBuilding(City pCity)
		{
			if (pCity.buildings.Count == 0)
			{
				return;
			}
			foreach (Building tBuilding in pCity.buildings.LoopRandom<Building>())
			{
				if (tBuilding.canBeUpgraded())
				{
					CityBehBuild.upgradeBuilding(tBuilding, pCity);
					break;
				}
			}
		}

		// Token: 0x06004694 RID: 18068 RVA: 0x001DF1B0 File Offset: 0x001DD3B0
		public static bool upgradeBuilding(Building pBuilding, City pCity)
		{
			string tTempID = pBuilding.asset.upgrade_to;
			BuildingAsset tTemp = AssetManager.buildings.get(tTempID);
			if (!pCity.hasEnoughResourcesFor(tTemp.cost))
			{
				return false;
			}
			bool flag = pBuilding.upgradeBuilding();
			if (flag)
			{
				pCity.spendResourcesForBuildingAsset(tTemp.cost);
			}
			return flag;
		}

		// Token: 0x06004695 RID: 18069 RVA: 0x001DF1FC File Offset: 0x001DD3FC
		public static void calcPossibleBuildings(City pCity)
		{
			ActorAsset tActorAsset = pCity.getActorAsset();
			CityBuildOrderAsset cityBuildOrderAsset = AssetManager.city_build_orders.get(tActorAsset.build_order_template_id);
			bool tTrackNoResources = DebugConfig.isOn(DebugOption.OverlayCity);
			foreach (BuildOrder iOrder in cityBuildOrderAsset.list)
			{
				if (CityBehBuild.canUseBuildAsset(iOrder, pCity))
				{
					if (!CityBehBuild.hasResourcesForBuildAsset(iOrder, pCity))
					{
						if (tTrackNoResources)
						{
							CityBehBuild._possible_buildings_no_resources.Add(iOrder);
						}
					}
					else
					{
						CityBehBuild._possible_buildings.Add(iOrder);
					}
				}
			}
		}

		// Token: 0x06004696 RID: 18070 RVA: 0x001DF298 File Offset: 0x001DD498
		public static bool hasResourcesForBuildAsset(BuildOrder pBuildAsset, City pCity)
		{
			BuildingAsset tTemp = pBuildAsset.getBuildingAsset(pCity, null);
			return pCity.hasEnoughResourcesFor(tTemp.cost);
		}

		// Token: 0x06004697 RID: 18071 RVA: 0x001DF2C0 File Offset: 0x001DD4C0
		public static bool canUseBuildAsset(BuildOrder pBuildAsset, City pCity)
		{
			BuildingAsset tTemp = pBuildAsset.getBuildingAsset(pCity, null);
			if (pBuildAsset.min_zones != 0 && pCity.zones.Count < pBuildAsset.min_zones)
			{
				return false;
			}
			int tCurrentCountType = pCity.countBuildingsType(tTemp.type, false);
			if (pBuildAsset.check_house_limit)
			{
				if (pCity.status.housing_free > 10)
				{
					return false;
				}
				int tLimitPerZone = pCity.getHouseLimit();
				if (tCurrentCountType >= tLimitPerZone)
				{
					return false;
				}
			}
			int tLimitType = pCity.getLimitOfBuildingsType(pBuildAsset);
			if (tLimitType != 0 && tCurrentCountType >= tLimitType)
			{
				return false;
			}
			if (pBuildAsset.check_full_village && pCity.status.housing_free != 0)
			{
				return false;
			}
			if (pCity.status.population < pBuildAsset.required_pop)
			{
				return false;
			}
			if (pCity.buildings.Count < pBuildAsset.required_buildings)
			{
				return false;
			}
			if (!CityBehBuild.haveRequiredBuildings(pBuildAsset, pCity))
			{
				return false;
			}
			if (!CityBehBuild.haveRequiredBuildingTypes(pBuildAsset.requirements_types, pCity))
			{
				return false;
			}
			if (pBuildAsset.upgrade)
			{
				List<Building> tList = pCity.getBuildingListOfID(tTemp.id);
				if (tList == null || tList.Count == 0)
				{
					return false;
				}
			}
			else if (tTemp.docks && CityBehBuild.getDockTile(pCity) == null)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06004698 RID: 18072 RVA: 0x001DF3CC File Offset: 0x001DD5CC
		private static bool haveRequiredBuildings(BuildOrder pOrder, City pCity)
		{
			if (pOrder.requirements_orders == null)
			{
				return true;
			}
			for (int i = 0; i < pOrder.requirements_orders.Length; i++)
			{
				string tOrderID = pOrder.requirements_orders[i];
				BuildingAsset tAsset = pOrder.getBuildingAsset(pCity, tOrderID);
				if (tAsset.id == tAsset.upgrade_to)
				{
					Debug.LogError("(!) Building is set to be upgraded to self: " + tAsset.id);
				}
				else
				{
					while (pCity.countBuildingsOfID(tAsset.id) == 0)
					{
						if (!tAsset.can_be_upgraded || string.IsNullOrEmpty(tAsset.upgrade_to))
						{
							return false;
						}
						tAsset = AssetManager.buildings.get(tAsset.upgrade_to);
					}
				}
			}
			return true;
		}

		// Token: 0x06004699 RID: 18073 RVA: 0x001DF470 File Offset: 0x001DD670
		private static bool haveRequiredBuildingTypes(string[] pRequiredBuildingTypes, City pCity)
		{
			if (pRequiredBuildingTypes == null)
			{
				return true;
			}
			foreach (string tID in pRequiredBuildingTypes)
			{
				if (!pCity.hasBuildingType(tID, true, null))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600469A RID: 18074 RVA: 0x001DF4A4 File Offset: 0x001DD6A4
		public static Building tryToBuild(City pCity, BuildingAsset pBuildingAsset)
		{
			if (!pCity.hasEnoughResourcesFor(pBuildingAsset.cost))
			{
				return null;
			}
			WorldTile tBuildTile = null;
			List<TileZone> tPossibleZones = CityBehBuild._possible_zones;
			if (pBuildingAsset.type == "type_training_dummies")
			{
				tBuildTile = CityBehBuild.getTileTrainingDummy(pBuildingAsset, pCity);
			}
			else if (pBuildingAsset.docks)
			{
				tBuildTile = CityBehBuild.getDockTile(pCity);
			}
			else
			{
				if (pBuildingAsset.build_prefer_replace_house)
				{
					tBuildTile = CityBehBuild.getOnHouseTile(pCity, pBuildingAsset);
				}
				if (tBuildTile == null)
				{
					CityBehBuild.fillPossibleZones(pBuildingAsset, pCity, tPossibleZones);
				}
			}
			if (tBuildTile == null && tPossibleZones.Count > 0)
			{
				if (pBuildingAsset.build_place_center)
				{
					tBuildTile = CityBehBuild.tryToBuildInZones(tPossibleZones, pBuildingAsset, pCity, true);
				}
				if (tBuildTile == null)
				{
					tBuildTile = CityBehBuild.tryToBuildInZones(tPossibleZones, pBuildingAsset, pCity, false);
				}
				if (tBuildTile != null && pBuildingAsset.needs_farms_ground && !CityBehBuild.checkFarmGround(tBuildTile, pBuildingAsset, pCity))
				{
					tBuildTile = null;
				}
			}
			tPossibleZones.Clear();
			if (tBuildTile == null)
			{
				return null;
			}
			Building tNewBuild = BehaviourActionBase<City>.world.buildings.addBuilding(pBuildingAsset, tBuildTile, false, false, BuildPlacingType.New);
			pCity.under_construction_building = tNewBuild;
			tNewBuild.setUnderConstruction();
			pCity.spendResourcesForBuildingAsset(pBuildingAsset.cost);
			return tNewBuild;
		}

		// Token: 0x0600469B RID: 18075 RVA: 0x001DF590 File Offset: 0x001DD790
		private static void fillPossibleZones(BuildingAsset pBuildingAsset, City pCity, List<TileZone> pPossibleZones)
		{
			for (int i = 0; i < pCity.zones.Count; i++)
			{
				TileZone tZone = pCity.zones[i];
				if ((!pBuildingAsset.build_place_single || CityBehBuild.isZonesClear(tZone, pBuildingAsset, pCity)) && (!pBuildingAsset.build_place_batch || !CityBehBuild.isNearbySingleBuilding(tZone, pBuildingAsset, pCity)) && (!pBuildingAsset.build_place_borders || CityBehBuild.isZoneNearbyBorder(tZone, pBuildingAsset, pCity)))
				{
					pPossibleZones.Add(tZone);
				}
			}
		}

		// Token: 0x0600469C RID: 18076 RVA: 0x001DF600 File Offset: 0x001DD800
		public static WorldTile getOnHouseTile(City pCity, BuildingAsset pAsset)
		{
			foreach (Building tBuilding in pCity.buildings.LoopRandom<Building>())
			{
				if (tBuilding.asset.priority <= pAsset.priority && tBuilding.asset.hasHousingSlots() && CityBehBuild.isGoodTileForBuilding(tBuilding.current_tile, pAsset, pCity))
				{
					return tBuilding.current_tile;
				}
			}
			return null;
		}

		// Token: 0x0600469D RID: 18077 RVA: 0x001DF688 File Offset: 0x001DD888
		public static WorldTile tryToBuildInZones(List<TileZone> pList, BuildingAsset pBuildingAsset, City pCity, bool pForceCenterZone = false)
		{
			CityLayoutTilePlacement tCityLayoutTilePlacement = pCity.getTilePlacementFromZone();
			bool flag = pCity.hasCulture() && pCity.culture.hasTrait("buildings_spread");
			bool tHasTownPlans = pCity.hasSpecialTownPlans();
			WorldTile tCityCenter = pCity.getTile(false);
			TileZone tCityZone = (tCityCenter != null) ? tCityCenter.zone : null;
			WorldTile tTileBest = null;
			int tBestDist = int.MaxValue;
			bool tCheckDist = !flag && tCityCenter != null;
			foreach (TileZone tZone in pList.LoopRandom<TileZone>())
			{
				WorldTile tTileResult = null;
				if (pForceCenterZone)
				{
					if (CityBehBuild.isGoodTileForBuilding(tZone.centerTile, pBuildingAsset, pCity))
					{
						tTileResult = tZone.centerTile;
					}
				}
				else if (pBuildingAsset.docks)
				{
					tTileResult = CityBehBuild.tryToBuildInZoneRandomly(tZone, pBuildingAsset, pCity);
				}
				else
				{
					if (tHasTownPlans && !pCity.planAllowsToPlaceBuildingInZone(tZone, tCityZone))
					{
						continue;
					}
					tTileResult = CityBehBuild.getTileBasedOnLayout(tCityLayoutTilePlacement, tZone, pBuildingAsset, pCity);
				}
				if (tTileResult != null)
				{
					if (!tCheckDist)
					{
						tTileBest = tTileResult;
						break;
					}
					int tDist = Toolbox.SquaredDistTile(tCityCenter, tTileResult);
					if (tDist < tBestDist)
					{
						tTileBest = tTileResult;
						tBestDist = tDist;
					}
				}
			}
			return tTileBest;
		}

		// Token: 0x0600469E RID: 18078 RVA: 0x001DF7A8 File Offset: 0x001DD9A8
		private static WorldTile getTileBasedOnLayout(CityLayoutTilePlacement pCityLayoutTilePlacement, TileZone pTileZone, BuildingAsset pBuildingAsset, City pCity)
		{
			switch (pCityLayoutTilePlacement)
			{
			case CityLayoutTilePlacement.Random:
			{
				WorldTile tResult = CityBehBuild.tryToBuildInZoneRandomly(pTileZone, pBuildingAsset, pCity);
				if (tResult != null)
				{
					return tResult;
				}
				break;
			}
			case CityLayoutTilePlacement.CenterTile:
			{
				WorldTile tResult = CityBehBuild.tryToBuildInZoneCenter(pTileZone, pBuildingAsset, pCity);
				if (tResult != null)
				{
					return tResult;
				}
				break;
			}
			case CityLayoutTilePlacement.CenterTileDrunk:
			{
				WorldTile tResult = CityBehBuild.tryToBuildInZoneDrunk(pTileZone, pBuildingAsset, pCity);
				if (tResult != null)
				{
					return tResult;
				}
				break;
			}
			case CityLayoutTilePlacement.Moonsteps:
			{
				WorldTile tResult = CityBehBuild.tryToBuildInZoneMoonsteps(pTileZone, pBuildingAsset, pCity);
				if (tResult != null)
				{
					return tResult;
				}
				break;
			}
			}
			return null;
		}

		// Token: 0x0600469F RID: 18079 RVA: 0x001DF808 File Offset: 0x001DDA08
		private static WorldTile tryToBuildInZoneMoonsteps(TileZone pZone, BuildingAsset pBuildingAsset, City pCity)
		{
			WorldTile tTile;
			if ((pZone.x + pZone.y) % 2 != 0)
			{
				tTile = pZone.centerTile.tile_down.tile_down.tile_down;
			}
			else
			{
				tTile = pZone.centerTile.tile_up.tile_up.tile_up;
			}
			if (CityBehBuild.isGoodTileForBuilding(tTile, pBuildingAsset, pCity))
			{
				return tTile;
			}
			return null;
		}

		// Token: 0x060046A0 RID: 18080 RVA: 0x001DF864 File Offset: 0x001DDA64
		private static WorldTile tryToBuildInZoneDrunk(TileZone pZone, BuildingAsset pBuildingAsset, City pCity)
		{
			WorldTile tResult = CityBehBuild.tryToBuildInZoneCenter(pZone, pBuildingAsset, pCity);
			if (Randy.randomChance(0.6f) && tResult != null)
			{
				WorldTile tTileChaos = tResult.neighboursAll.GetRandom<WorldTile>();
				if (CityBehBuild.isGoodTileForBuilding(tTileChaos, pBuildingAsset, pCity))
				{
					tResult = tTileChaos;
				}
			}
			return tResult;
		}

		// Token: 0x060046A1 RID: 18081 RVA: 0x001DF8A4 File Offset: 0x001DDAA4
		private static WorldTile tryToBuildInZoneRandomly(TileZone pZone, BuildingAsset pBuildingAsset, City pCity)
		{
			foreach (WorldTile tZoneTile in pZone.tiles.LoopRandom<WorldTile>())
			{
				if (CityBehBuild.isGoodTileForBuilding(tZoneTile, pBuildingAsset, pCity))
				{
					return tZoneTile;
				}
			}
			return null;
		}

		// Token: 0x060046A2 RID: 18082 RVA: 0x001DF900 File Offset: 0x001DDB00
		private static WorldTile tryToBuildInZoneCenter(TileZone pZone, BuildingAsset pBuildingAsset, City pCity)
		{
			if (!CityBehBuild.isGoodTileForBuilding(pZone.centerTile, pBuildingAsset, pCity))
			{
				return null;
			}
			return pZone.centerTile;
		}

		// Token: 0x060046A3 RID: 18083 RVA: 0x001DF91C File Offset: 0x001DDB1C
		internal static bool isZoneNearbyBorder(TileZone pParentZone, BuildingAsset pAsset, City pCity)
		{
			TileZone[] tNeighbours = pParentZone.neighbours_all;
			for (int i = 0; i < tNeighbours.Length; i++)
			{
				if (tNeighbours[i].city != pCity)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060046A4 RID: 18084 RVA: 0x001DF94C File Offset: 0x001DDB4C
		internal static bool isNearbySingleBuilding(TileZone pParentZone, BuildingAsset pAsset, City pCity)
		{
			if (CityBehBuild.checkZoneNearbySignleBuilding(pParentZone, pAsset, pCity))
			{
				return true;
			}
			TileZone[] tNeighbours = pParentZone.neighbours_all;
			for (int i = 0; i < tNeighbours.Length; i++)
			{
				if (CityBehBuild.checkZoneNearbySignleBuilding(tNeighbours[i], pAsset, pCity))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060046A5 RID: 18085 RVA: 0x001DF98C File Offset: 0x001DDB8C
		internal static bool checkZoneNearbySignleBuilding(TileZone pZone, BuildingAsset pAsset, City pCity)
		{
			if (!pZone.hasBuildingOf(pCity))
			{
				return false;
			}
			HashSet<Building> tSet = pZone.getHashset(BuildingList.Civs);
			if (tSet != null)
			{
				using (HashSet<Building>.Enumerator enumerator = tSet.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.asset.build_place_single)
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060046A6 RID: 18086 RVA: 0x001DF9FC File Offset: 0x001DDBFC
		internal static bool isZonesNearbyBuilding(TileZone pParentZone, BuildingAsset pAsset, City pCity)
		{
			if (CityBehBuild.checkZoneNearbyBuilding(pParentZone, pAsset, pCity))
			{
				return true;
			}
			TileZone[] tNeighbours = pParentZone.neighbours_all;
			for (int i = 0; i < tNeighbours.Length; i++)
			{
				if (CityBehBuild.checkZoneNearbyBuilding(tNeighbours[i], pAsset, pCity))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060046A7 RID: 18087 RVA: 0x001DFA39 File Offset: 0x001DDC39
		internal static bool checkZoneNearbyBuilding(TileZone pZone, BuildingAsset pAsset, City pCity)
		{
			return pZone.isSameCityHere(pCity) && pZone.hasBuildingOf(pCity);
		}

		// Token: 0x060046A8 RID: 18088 RVA: 0x001DFA54 File Offset: 0x001DDC54
		internal static bool isZonesClear(TileZone pParentZone, BuildingAsset pAsset, City pCity)
		{
			if (!CityBehBuild.checkZoneClear(pParentZone, pAsset, pCity))
			{
				return false;
			}
			TileZone[] tNeighbours = pParentZone.neighbours_all;
			for (int i = 0; i < tNeighbours.Length; i++)
			{
				if (!CityBehBuild.checkZoneClear(tNeighbours[i], pAsset, pCity))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060046A9 RID: 18089 RVA: 0x001DFA94 File Offset: 0x001DDC94
		internal static bool checkFarmGround(WorldTile pTile, BuildingAsset pAsset, City pCity)
		{
			int tCountSoil = 0;
			tCountSoil += CityBehBuild.countGoodForFarms(pTile.region, pCity);
			for (int i = 0; i < pTile.region.neighbours.Count; i++)
			{
				MapRegion tReg = pTile.region.neighbours[i];
				List<TileZone> tChunkZones = tReg.chunk.zones;
				for (int j = 0; j < tChunkZones.Count; j++)
				{
					if (tChunkZones[j].city == pCity)
					{
						tCountSoil += CityBehBuild.countGoodForFarms(tReg, pCity);
					}
				}
			}
			return tCountSoil > 30;
		}

		// Token: 0x060046AA RID: 18090 RVA: 0x001DFB20 File Offset: 0x001DDD20
		internal static int countGoodForFarms(MapRegion pRegion, City pCity)
		{
			int tRes = 0;
			List<WorldTile> tTiles = pRegion.tiles;
			for (int i = 0; i < tTiles.Count; i++)
			{
				if (tTiles[i].Type.can_be_farm)
				{
					tRes++;
				}
			}
			return tRes;
		}

		// Token: 0x060046AB RID: 18091 RVA: 0x001DFB5F File Offset: 0x001DDD5F
		internal static bool checkZoneClear(TileZone pZone, BuildingAsset pAsset, City pCity)
		{
			return !pZone.hasAnyBuildingsInSet(BuildingList.Civs);
		}

		// Token: 0x060046AC RID: 18092 RVA: 0x001DFB6D File Offset: 0x001DDD6D
		public static bool isGoodTileForBuilding(WorldTile pTile, BuildingAsset pAsset, City pCity)
		{
			return pTile.canBuildOn(pAsset) && BehaviourActionBase<City>.world.buildings.canBuildFrom(pTile, pAsset, pCity, BuildPlacingType.New, false);
		}

		// Token: 0x060046AD RID: 18093 RVA: 0x001DFB93 File Offset: 0x001DDD93
		public static void debugRoards(City pCity, Building pBuilding)
		{
			CityBehBuild.makeRoadsBuildings(pCity, pBuilding);
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x001DFB9C File Offset: 0x001DDD9C
		public static void makeRoadsBuildings(City pCity, Building pBuilding)
		{
			if (pCity.road_tiles_to_build.Count > 0)
			{
				return;
			}
			if (!pBuilding.asset.build_road_to)
			{
				return;
			}
			WorldTile tFromTile = pBuilding.current_tile;
			if (tFromTile.Type.liquid)
			{
				return;
			}
			using (ListPool<WorldTile> tTemp = new ListPool<WorldTile>(pCity.buildings.Count))
			{
				foreach (Building tBuild in pCity.buildings)
				{
					if (tBuild != pBuilding && tBuild.asset.build_road_to && !tBuild.current_tile.Type.liquid && tBuild.current_tile.isSameIsland(pBuilding.current_tile))
					{
						tTemp.Add(tBuild.current_tile);
					}
				}
				if (tTemp.Count != 0)
				{
					bool tFinished = false;
					if (DebugConfig.isOn(DebugOption.CityFastConstruction))
					{
						tFinished = true;
					}
					WorldTile tClosestTile = Toolbox.getClosestTile(tTemp, tFromTile);
					if (tClosestTile != null)
					{
						tTemp.Remove(tClosestTile);
						MapAction.makeRoadBetween(tClosestTile, tFromTile, pCity, tFinished);
					}
					tClosestTile = Toolbox.getClosestTile(tTemp, tFromTile);
					if (tClosestTile != null)
					{
						MapAction.makeRoadBetween(tClosestTile, tFromTile, pCity, tFinished);
					}
				}
			}
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x001DFCD8 File Offset: 0x001DDED8
		public static WorldTile getTileTrainingDummy(BuildingAsset pBuildingAsset, City pCity)
		{
			Building tBarracks = pCity.getBuildingOfType("type_barracks", true, true, false, null);
			if (tBarracks == null)
			{
				return null;
			}
			HashSet<WorldTile> tTempCheckedTiles = UnsafeCollectionPool<HashSet<WorldTile>, WorldTile>.Get();
			WorldTile result;
			using (ListPool<WorldTile> tPossibleTilesWave = new ListPool<WorldTile>())
			{
				using (ListPool<WorldTile> tPossibleTilesWave2 = new ListPool<WorldTile>())
				{
					foreach (WorldTile worldTile in tBarracks.tiles)
					{
						foreach (WorldTile tTile in worldTile.neighbours)
						{
							if (!tTempCheckedTiles.Contains(tTile))
							{
								tTempCheckedTiles.Add(tTile);
								if (!tTile.hasBuilding() && CityBehBuild.isGoodTileForBuilding(tTile, pBuildingAsset, pCity))
								{
									tPossibleTilesWave.Add(tTile);
								}
							}
						}
					}
					foreach (WorldTile ptr in tPossibleTilesWave)
					{
						foreach (WorldTile tTile2 in ptr.neighbours)
						{
							if (!tTempCheckedTiles.Contains(tTile2))
							{
								tTempCheckedTiles.Add(tTile2);
								if (!tTile2.hasBuilding() && CityBehBuild.isGoodTileForBuilding(tTile2, pBuildingAsset, pCity))
								{
									tPossibleTilesWave2.Add(tTile2);
								}
							}
						}
					}
					tTempCheckedTiles.Clear();
					UnsafeCollectionPool<HashSet<WorldTile>, WorldTile>.Release(tTempCheckedTiles);
					if (tPossibleTilesWave2.Count == 0)
					{
						result = null;
					}
					else
					{
						result = tPossibleTilesWave2.GetRandom<WorldTile>();
					}
				}
			}
			return result;
		}

		// Token: 0x060046B0 RID: 18096 RVA: 0x001DFEA4 File Offset: 0x001DE0A4
		public static WorldTile getDockTile(City pCity)
		{
			BuildingAsset tDocksAsset = pCity.getActorAsset().getBuildingDockAsset();
			if (tDocksAsset == null)
			{
				return null;
			}
			OceanHelper.clearOceanPools();
			OceanHelper.saveOceanPoolsWithDocks(pCity);
			if (pCity.getTile(false) == null)
			{
				return null;
			}
			foreach (TileZone tZone in pCity.zones.LoopRandom<TileZone>())
			{
				if (tZone.tiles_with_liquid != 0)
				{
					MapChunk tChunk = tZone.chunk;
					if (tChunk.regions.Count > 1)
					{
						bool tHaveWater = false;
						for (int i = 0; i < tChunk.regions.Count; i++)
						{
							MapRegion tMapRegion = tChunk.regions[i];
							if (tMapRegion.type == TileLayerType.Ocean && OceanHelper.goodForNewDock(tMapRegion.tiles[0]))
							{
								tHaveWater = true;
							}
						}
						if (tHaveWater)
						{
							foreach (WorldTile tTile in tZone.tiles.LoopRandom<WorldTile>())
							{
								if (tTile.Type.ocean && BehaviourActionBase<City>.world.buildings.canBuildFrom(tTile, tDocksAsset, pCity, BuildPlacingType.New, false))
								{
									return tTile;
								}
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x040031F6 RID: 12790
		private static readonly List<BuildOrder> _possible_buildings = new List<BuildOrder>();

		// Token: 0x040031F7 RID: 12791
		private static readonly List<BuildOrder> _possible_buildings_no_resources = new List<BuildOrder>();

		// Token: 0x040031F8 RID: 12792
		private static readonly List<TileZone> _possible_zones = new List<TileZone>();
	}
}
