using System;
using System.Collections.Generic;
using UnityEngine;

namespace ai
{
	// Token: 0x0200087A RID: 2170
	public static class ActorTool
	{
		// Token: 0x06004428 RID: 17448 RVA: 0x001CC698 File Offset: 0x001CA898
		public static int countContagiousNearby(Actor pActor)
		{
			int tVal = 0;
			using (IEnumerator<Actor> enumerator = Finder.getUnitsFromChunk(pActor.current_tile, 1, 10f, false).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.hasTrait("contagious"))
					{
						tVal++;
					}
				}
			}
			return tVal;
		}

		// Token: 0x06004429 RID: 17449 RVA: 0x001CC6FC File Offset: 0x001CA8FC
		public static Building findNewBuildingTarget(Actor pActor, string pType, bool pOnlyFreeTile = true)
		{
			Building result;
			using (ListPool<Building> tPossibleBuildings = new ListPool<Building>(64))
			{
				if (!(pType == "new_building"))
				{
					if (!(pType == "random_house_building"))
					{
						if (!(pType == "ruins"))
						{
							if (!(pType == "type_mine") && !(pType == "type_bonfire") && !(pType == "type_training_dummies"))
							{
								return ActorTool.findNewTargetInZones(pActor, pType, tPossibleBuildings);
							}
							goto IL_18C;
						}
					}
					else
					{
						using (IEnumerator<Building> enumerator = pActor.city.buildings.LoopRandom<Building>().GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								Building tBuilding = enumerator.Current;
								if (tBuilding.isSameIslandAs(pActor) && !tBuilding.isUnderConstruction() && tBuilding.isUsable() && tBuilding.asset.hasHousingSlots())
								{
									return tBuilding;
								}
							}
							goto IL_1B6;
						}
					}
					using (IEnumerator<TileZone> enumerator2 = pActor.city.zones.LoopRandom<TileZone>().GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							TileZone tileZone = enumerator2.Current;
							HashSet<Building> tSetRuins = tileZone.getHashset(BuildingList.Ruins);
							if (tSetRuins != null)
							{
								tPossibleBuildings.AddRange(tSetRuins);
							}
							HashSet<Building> tSetAbandoned = tileZone.getHashset(BuildingList.Abandoned);
							if (tSetAbandoned != null)
							{
								tPossibleBuildings.AddRange(tSetAbandoned);
							}
							if (tPossibleBuildings.Count > 0)
							{
								break;
							}
						}
						goto IL_1B6;
					}
					IL_18C:
					Building tBuildingOfType = pActor.city.getBuildingOfType(pType, true, false, pOnlyFreeTile, pActor.current_island);
					if (tBuildingOfType != null)
					{
						return tBuildingOfType;
					}
				}
				else
				{
					Building tBuildingToBuild = pActor.city.getBuildingToBuild();
					if (tBuildingToBuild != null && tBuildingToBuild.tiles.Count != 0)
					{
						WorldTile tTile = tBuildingToBuild.getConstructionTile();
						if (tTile != null && tTile.isSameIsland(pActor.current_tile))
						{
							return tBuildingToBuild;
						}
					}
				}
				IL_1B6:
				if (tPossibleBuildings.Count == 0)
				{
					result = null;
				}
				else
				{
					result = tPossibleBuildings.GetRandom<Building>();
				}
			}
			return result;
		}

		// Token: 0x0600442A RID: 17450 RVA: 0x001CC930 File Offset: 0x001CAB30
		public static Building findNewTargetInZones(Actor pActor, string pType, ListPool<Building> pPossibleBuildings)
		{
			foreach (TileZone tZone in pActor.city.zones)
			{
				HashSet<Building> tHashset = null;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(pType);
				if (num <= 1141948218U)
				{
					if (num != 278895133U)
					{
						if (num != 1031750018U)
						{
							if (num == 1141948218U)
							{
								if (pType == "type_tree")
								{
									tHashset = tZone.getHashset(BuildingList.Trees);
								}
							}
						}
						else if (pType == "type_vegetation")
						{
							goto IL_F3;
						}
					}
					else if (pType == "type_fruits")
					{
						tHashset = tZone.getHashset(BuildingList.Food);
					}
				}
				else if (num <= 2386089880U)
				{
					if (num != 1505661272U)
					{
						if (num == 2386089880U)
						{
							if (pType == "type_hive")
							{
								tHashset = tZone.getHashset(BuildingList.Hives);
							}
						}
					}
					else if (pType == "type_poop")
					{
						tHashset = tZone.getHashset(BuildingList.Poops);
					}
				}
				else if (num != 2908999029U)
				{
					if (num == 3964518768U)
					{
						if (pType == "type_mineral")
						{
							tHashset = tZone.getHashset(BuildingList.Minerals);
						}
					}
				}
				else if (pType == "type_flower")
				{
					goto IL_F3;
				}
				IL_12E:
				if (tHashset == null || tHashset.Count == 0)
				{
					continue;
				}
				foreach (Building tBuilding in tHashset)
				{
					BuildingAsset tAsset = tBuilding.asset;
					if (!tBuilding.current_tile.isTargeted() && tBuilding.isSameIslandAs(pActor) && ((tAsset.building_type != BuildingType.Building_Fruits && tAsset.building_type != BuildingType.Building_Tree) || tBuilding.hasResourcesToCollect()) && (tAsset.building_type != BuildingType.Building_Tree || tAsset.can_be_chopped_down))
					{
						pPossibleBuildings.Add(tBuilding);
					}
				}
				if (pPossibleBuildings.Count > 0)
				{
					break;
				}
				continue;
				IL_F3:
				tHashset = tZone.getHashset(BuildingList.Flora);
				goto IL_12E;
			}
			if (pPossibleBuildings.Count == 0)
			{
				return null;
			}
			return pPossibleBuildings.GetRandom<Building>();
		}

		// Token: 0x0600442B RID: 17451 RVA: 0x001CCB6C File Offset: 0x001CAD6C
		public static WorldTile getTileNearby(ActorTileTarget pTarget, MapChunk pChunk)
		{
			WorldTile result;
			using (ListPool<WorldTile> tPossibleMoves = new ListPool<WorldTile>(64))
			{
				ValueTuple<MapChunk[], int> allChunksFromChunk = Toolbox.getAllChunksFromChunk(pChunk);
				MapChunk[] tTempChunks = allChunksFromChunk.Item1;
				int tLength = allChunksFromChunk.Item2;
				for (int i = 0; i < tLength; i++)
				{
					MapChunk tChunk = tTempChunks[i];
					if (tPossibleMoves.Count <= 20)
					{
						WorldTile[] tTiles = tChunk.tiles;
						int tCount = tTiles.Length;
						for (int j = 0; j < tCount; j++)
						{
							WorldTile tTile = tTiles[j];
							if (tPossibleMoves.Count <= 20)
							{
								switch (pTarget)
								{
								case ActorTileTarget.RandomTNT:
									if (tTile.Type.explodable)
									{
										tPossibleMoves.Add(tTile);
									}
									break;
								case ActorTileTarget.RandomBurnableTile:
									if (tTile.Type.burnable)
									{
										tPossibleMoves.Add(tTile);
									}
									break;
								case ActorTileTarget.RandomTileWithUnits:
									tTile.doUnits(delegate(Actor _)
									{
										tPossibleMoves.Add(tTile);
									});
									break;
								case ActorTileTarget.RandomTileWithCivStructures:
									if (tTile.hasBuilding() && tTile.building.hasCity())
									{
										tPossibleMoves.Add(tTile);
									}
									if (tTile.Type.burnable && tTile.zone.city != null)
									{
										tPossibleMoves.Add(tTile);
									}
									break;
								}
							}
						}
					}
				}
				if (tPossibleMoves.Count == 0)
				{
					result = null;
				}
				else
				{
					result = tPossibleMoves.GetRandom<WorldTile>();
				}
			}
			return result;
		}

		// Token: 0x0600442C RID: 17452 RVA: 0x001CCD88 File Offset: 0x001CAF88
		public static Docks getDockTradeTarget(Actor pActor)
		{
			return ActorTool.getDockTradeTarget(pActor.current_tile, pActor);
		}

		// Token: 0x0600442D RID: 17453 RVA: 0x001CCD96 File Offset: 0x001CAF96
		private static Docks getDockTradeTarget(WorldTile pTile, Actor pActor)
		{
			return ActorTool.getDockTradeTarget(pTile.region, pActor);
		}

		// Token: 0x0600442E RID: 17454 RVA: 0x001CCDA4 File Offset: 0x001CAFA4
		private static Docks getDockTradeTarget(MapRegion pRegion, Actor pActor)
		{
			return ActorTool.getDockTradeTarget(pRegion.island, pActor);
		}

		// Token: 0x0600442F RID: 17455 RVA: 0x001CCDB2 File Offset: 0x001CAFB2
		private static Docks getDockTradeTarget(TileIsland pIsland, Actor pActor)
		{
			return ActorTool.getDockTradeTarget(pIsland.docks, pActor);
		}

		// Token: 0x06004430 RID: 17456 RVA: 0x001CCDC0 File Offset: 0x001CAFC0
		private static Docks getDockTradeTarget(ListPool<Docks> pList, Actor pActor)
		{
			if (pList == null || pList.Count == 0)
			{
				return null;
			}
			foreach (Docks tDock in pList.LoopRandom<Docks>())
			{
				if (tDock.building.hasCity() && pActor.getHomeBuilding() != tDock.building && tDock.building.isUsable() && !tDock.building.isAbandoned() && !tDock.building.city.kingdom.isEnemy(pActor.kingdom) && (tDock.isDockGood() || tDock.hasOceanTiles()))
				{
					return tDock;
				}
			}
			return null;
		}

		// Token: 0x06004431 RID: 17457 RVA: 0x001CCE7C File Offset: 0x001CB07C
		public static WorldTile getRandomTileForBoat(Actor pActor)
		{
			MapRegion tRegion = pActor.current_tile.region;
			if (tRegion.neighbours.Count > 0 && Randy.randomBool())
			{
				tRegion = tRegion.neighbours.GetRandom<MapRegion>();
			}
			if (tRegion.tiles.Count > 0)
			{
				return tRegion.tiles.GetRandom<WorldTile>();
			}
			return null;
		}

		// Token: 0x06004432 RID: 17458 RVA: 0x001CCED4 File Offset: 0x001CB0D4
		public static int attributeDice(Actor pActor, int pAmount = 2)
		{
			if (pActor == null)
			{
				return int.MinValue;
			}
			int tResult = 0;
			int tMax = (int)(pActor.stats["diplomacy"] + pActor.stats["warfare"] + pActor.stats["stewardship"]);
			if (pActor.hasCulture())
			{
				Culture culture = pActor.culture;
				bool tPatriarchy = culture.hasTrait("patriarchy");
				bool flag = culture.hasTrait("matriarchy");
				if (tPatriarchy && pActor.isSexMale())
				{
					tMax += 999;
				}
				if (flag && pActor.isSexFemale())
				{
					tMax += 999;
				}
			}
			for (int i = 0; i < pAmount; i++)
			{
				tResult += Randy.randomInt(0, tMax);
			}
			return tResult;
		}

		// Token: 0x06004433 RID: 17459 RVA: 0x001CCF80 File Offset: 0x001CB180
		public static void checkHomeDocks(Actor pActor)
		{
			Building tHomeBuilding = pActor.getHomeBuilding();
			if (tHomeBuilding == null)
			{
				ListPool<Docks> tList = pActor.current_tile.region.island.docks;
				if (tList != null && tList.Count > 0)
				{
					for (int i = 0; i < tList.Count; i++)
					{
						Docks tDock = tList[i];
						if (tDock.building.isUsable() && !tDock.building.isAbandoned() && !tDock.building.isUnderConstruction() && tDock.building.hasCity() && tDock.building.city.kingdom == pActor.kingdom && !tDock.building.city.kingdom.isEnemy(pActor.kingdom) && !tDock.isFull(pActor.asset.boat_type))
						{
							tDock.addBoatToDock(pActor);
							return;
						}
					}
				}
			}
			if (tHomeBuilding != null)
			{
				if (!tHomeBuilding.isSameIslandAs(pActor))
				{
					pActor.clearHomeBuilding();
					return;
				}
				Docks tDocks = tHomeBuilding.component_docks;
				if (!tDocks.isDockGood() && !tDocks.hasOceanTiles())
				{
					pActor.clearHomeBuilding();
				}
			}
		}

		// Token: 0x06004434 RID: 17460 RVA: 0x001CD098 File Offset: 0x001CB298
		public static void copyImportantData(ActorData pFrom, ActorData pCloneTo, bool pCopyAge)
		{
			pCloneTo.name = pFrom.name;
			pCloneTo.custom_name = pFrom.custom_name;
			if (pCopyAge)
			{
				pCloneTo.created_time = pFrom.created_time;
				pCloneTo.age_overgrowth = pFrom.age_overgrowth;
			}
			pCloneTo.asset_id = pFrom.asset_id;
			pCloneTo.kills = pFrom.kills;
			pCloneTo.births = pFrom.births;
			pCloneTo.favorite = pFrom.favorite;
			pCloneTo.food_consumed = pFrom.food_consumed;
			pCloneTo.favorite_food = pFrom.favorite_food;
			pCloneTo.head = pFrom.head;
			pCloneTo.generation = pFrom.generation;
			pCloneTo.parent_id_1 = pFrom.parent_id_1;
			pCloneTo.parent_id_2 = pFrom.parent_id_2;
			pCloneTo.ancestor_family = pFrom.ancestor_family;
			pCloneTo.best_friend_id = pFrom.best_friend_id;
			pCloneTo.lover = pFrom.lover;
			pCloneTo.experience = pFrom.experience;
			pCloneTo.renown = pFrom.renown;
			pCloneTo.loot = pFrom.loot;
			pCloneTo.money = pFrom.money;
			pCloneTo.level = pFrom.level;
			pCloneTo.sex = pFrom.sex;
			pCloneTo.phenotype_shade = pFrom.phenotype_shade;
			pCloneTo.phenotype_index = pFrom.phenotype_index;
			pCloneTo["diplomacy"] = pFrom["diplomacy"];
			pCloneTo["intelligence"] = pFrom["intelligence"];
			pCloneTo["stewardship"] = pFrom["stewardship"];
			pCloneTo["warfare"] = pFrom["warfare"];
		}

		// Token: 0x06004435 RID: 17461 RVA: 0x001CD22C File Offset: 0x001CB42C
		public static void copyUnitToOtherUnit(Actor pParent, Actor pCloneTarget, bool pCopyAge = true)
		{
			pCloneTarget.current_position = pParent.current_position;
			pCloneTarget.current_rotation = pParent.current_rotation;
			ActorTool.copyImportantData(pParent.data, pCloneTarget.data, pCopyAge);
			pCloneTarget.takeItems(pParent, 1f, 0);
			foreach (ActorTrait tTrait in pParent.getTraits())
			{
				pCloneTarget.addTrait(tTrait, false);
			}
			pCloneTarget.setStatsDirty();
			if (MoveCamera.inSpectatorMode() && pParent.isCameraFollowingUnit())
			{
				MoveCamera.setFocusUnit(pCloneTarget);
			}
		}

		// Token: 0x06004436 RID: 17462 RVA: 0x001CD2D0 File Offset: 0x001CB4D0
		public static bool canBeCuredFromTraitsOrStatus(Actor pActor)
		{
			using (IEnumerator<ActorTrait> enumerator = pActor.getTraits().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.can_be_cured)
					{
						return true;
					}
				}
			}
			if (pActor.hasAnyStatusEffect())
			{
				using (Dictionary<string, Status>.ValueCollection.Enumerator enumerator2 = pActor.getStatuses().GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.asset.can_be_cured)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06004437 RID: 17463 RVA: 0x001CD378 File Offset: 0x001CB578
		public static void applyForceToUnit(AttackData pData, BaseSimObject pTargetToCheck, float pMod = 1f, bool pCheckCancelJobOnLand = false)
		{
			float tForce = pData.knockback * pMod;
			if (tForce > 0f && pTargetToCheck.isActor())
			{
				Vector2 tPosStart = pTargetToCheck.cur_transform_position;
				Vector2 tAttackVec = pData.hit_position;
				pTargetToCheck.a.calculateForce(tPosStart.x, tPosStart.y, tAttackVec.x, tAttackVec.y, tForce, 0f, pCheckCancelJobOnLand);
			}
		}

		// Token: 0x06004438 RID: 17464 RVA: 0x001CD3E0 File Offset: 0x001CB5E0
		public static int countUnitsFrom(string pActorID)
		{
			return AssetManager.actor_library.get(pActorID).units.Count;
		}

		// Token: 0x06004439 RID: 17465 RVA: 0x001CD3F7 File Offset: 0x001CB5F7
		public static void checkFallInLove(Actor pActor, Actor pTarget)
		{
			if (!pActor.canFallInLoveWith(pTarget))
			{
				return;
			}
			if (!pTarget.canFallInLoveWith(pActor))
			{
				return;
			}
			pActor.becomeLoversWith(pTarget);
		}

		// Token: 0x0600443A RID: 17466 RVA: 0x001CD414 File Offset: 0x001CB614
		public static void checkBecomingBestFriends(Actor pActor, Actor pTarget)
		{
			if (pActor.hasBestFriend() || pTarget.hasBestFriend())
			{
				return;
			}
			if (pActor.isBaby() && pTarget.isAdult())
			{
				return;
			}
			if (pActor.isAdult() && pTarget.isBaby())
			{
				return;
			}
			float tChanceToBecomeFriends = 0f;
			if (pActor.hasEmotions() && pTarget.hasEmotions())
			{
				float tHappinessDifference = 1f - Mathf.Abs(pActor.getHappinessRatio() - pTarget.getHappinessRatio());
				tChanceToBecomeFriends += tHappinessDifference * 0.25f;
			}
			if (pActor.family == pTarget.family)
			{
				tChanceToBecomeFriends += 0.1f;
			}
			tChanceToBecomeFriends += ActorTool.calcLikeability(pActor, pTarget);
			if (tChanceToBecomeFriends <= 0f)
			{
				return;
			}
			if (!Randy.randomChance(tChanceToBecomeFriends))
			{
				return;
			}
			pActor.setBestFriend(pTarget, true);
			pTarget.setBestFriend(pActor, true);
		}

		// Token: 0x0600443B RID: 17467 RVA: 0x001CD4D0 File Offset: 0x001CB6D0
		private static float calcLikeability(Actor pActor, Actor pTarget)
		{
			float tRelations = 0f;
			foreach (ActorTrait tTrait in pActor.getTraits())
			{
				float tMultiplier = 1f;
				if (tTrait.same_trait_mod != 0 && pTarget.hasTrait(tTrait))
				{
					tMultiplier += (float)tTrait.same_trait_mod / 100f;
				}
				if (tTrait.opposite_trait_mod != 0)
				{
					foreach (ActorTrait tOppositeTrait in tTrait.opposite_traits)
					{
						if (pTarget.hasTrait(tOppositeTrait))
						{
							tMultiplier += (float)tTrait.opposite_trait_mod / 100f;
						}
					}
				}
				if (tTrait.likeability == 0f)
				{
					tRelations += tMultiplier * 0.1f;
				}
				else
				{
					tRelations += tTrait.likeability * tMultiplier;
				}
			}
			foreach (ActorTrait tTrait2 in pTarget.getTraits())
			{
				if (tTrait2.same_trait_mod == 0 && tTrait2.opposite_trait_mod == 0)
				{
					tRelations += tTrait2.likeability;
				}
			}
			tRelations *= 0.5f;
			if (pActor.areFoes(pTarget))
			{
				tRelations -= 0.5f;
			}
			if (pActor.religion == pTarget.religion)
			{
				tRelations += 0.1f;
			}
			else
			{
				tRelations -= 0.25f;
			}
			if (pActor.clan == pTarget.clan)
			{
				tRelations += 0.1f;
			}
			if (pActor.culture == pTarget.culture)
			{
				tRelations += 0.1f;
			}
			return tRelations;
		}
	}
}
