using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x0200096C RID: 2412
	public class CityBehCheckCitizenTasks : BehaviourActionCity
	{
		// Token: 0x060046B8 RID: 18104 RVA: 0x001E023C File Offset: 0x001DE43C
		public override BehResult execute(City pCity)
		{
			CityTasksData tTasks = pCity.tasks;
			CitizenJobs tJobs = pCity.jobs;
			CityStatus tStatus = pCity.status;
			tJobs.clearJobs();
			this.checkOccupied(pCity);
			this._citizens_left = pCity.status.population_adults;
			tTasks.clear();
			if (!DebugConfig.isOn(DebugOption.SystemCityTasks))
			{
				return BehResult.Continue;
			}
			this.countFires(pCity, tTasks);
			this.countResources(pCity, tTasks);
			this.countRoads(pCity, tTasks);
			int tError = 0;
			bool tWarriorsNeeded = false;
			int tMaxWarriorsPossible = 0;
			bool tHasCityStorage = pCity.hasStorageBuilding();
			int tFoodTotal = pCity.getTotalFood();
			if (WorldLawLibrary.world_law_civ_army.isEnabled() && tStatus.population_adults > 15 && pCity.hasEnoughFoodForArmy())
			{
				tWarriorsNeeded = true;
				tMaxWarriorsPossible = this.getPossibleWarriors(pCity);
			}
			int tMaxHerbCollectors;
			if (tFoodTotal > 100)
			{
				tMaxHerbCollectors = 1;
			}
			else if (tFoodTotal > 60)
			{
				tMaxHerbCollectors = 4;
			}
			else if (tFoodTotal > 40)
			{
				tMaxHerbCollectors = 5;
			}
			else if (tFoodTotal > 20)
			{
				tMaxHerbCollectors = 7;
			}
			else
			{
				tMaxHerbCollectors = 10;
			}
			int tMaxWoodCollectors;
			if (pCity.getResourcesAmount("wood") > 15)
			{
				tMaxWoodCollectors = 1;
			}
			else
			{
				tMaxWoodCollectors = 3;
			}
			bool tHasWindmill = pCity.hasBuildingType("type_windmill", true, null);
			bool tHasMine = pCity.hasBuildingType("type_mine", true, null);
			bool tHasBuildingToBuild = pCity.hasBuildingToBuild();
			int tErrorMaximum = this._citizens_left * 2;
			while (this._citizens_left >= 0)
			{
				if (tHasBuildingToBuild)
				{
					this.addToJob(CitizenJobLibrary.builder, tJobs, 1, 3, 0);
				}
				if (tHasCityStorage)
				{
					this.addToJob(CitizenJobLibrary.gatherer_bushes, tJobs, 1, tTasks.bushes, tMaxHerbCollectors);
					this.addToJob(CitizenJobLibrary.gatherer_herbs, tJobs, 1, tTasks.plants, tMaxHerbCollectors);
					this.addToJob(CitizenJobLibrary.gatherer_honey, tJobs, 1, tTasks.hives, tMaxHerbCollectors);
				}
				if (tHasWindmill)
				{
					this.addToJob(CitizenJobLibrary.farmer, tJobs, 1, pCity.calculated_place_for_farms.Count, 0);
				}
				if (World.world_era.flag_crops_grow && tHasWindmill)
				{
					this.addToJob(CitizenJobLibrary.farmer, tJobs, 1, tTasks.farms_total, 0);
				}
				if (tHasCityStorage)
				{
					this.addToJob(CitizenJobLibrary.miner_deposit, tJobs, 1, tTasks.minerals, 0);
					this.addToJob(CitizenJobLibrary.woodcutter, tJobs, 1, tTasks.trees, tMaxWoodCollectors);
				}
				if (tHasMine)
				{
					this.addToJob(CitizenJobLibrary.miner, tJobs, 1, 5, 0);
				}
				if (tWarriorsNeeded)
				{
					this.addToJob(CitizenJobLibrary.attacker, tJobs, 2, tMaxWarriorsPossible, 0);
				}
				this.addToJob(CitizenJobLibrary.road_builder, tJobs, 1, tTasks.roads, 1);
				this.addToJob(CitizenJobLibrary.cleaner, tJobs, 1, tTasks.ruins, 1);
				if (tHasCityStorage)
				{
					this.addToJob(CitizenJobLibrary.hunter, tJobs, 1, 1, 0);
					this.addToJob(CitizenJobLibrary.manure_cleaner, tJobs, 1, tTasks.poops, 3);
				}
				tError++;
				if (tError > tErrorMaximum)
				{
					break;
				}
			}
			return BehResult.Continue;
		}

		// Token: 0x060046B9 RID: 18105 RVA: 0x001E04BC File Offset: 0x001DE6BC
		private void addToJob(CitizenJobAsset pJobAsset, CitizenJobs pJobsContainer, int pAdd, int pTaskAmountMax, int pJobMax = 0)
		{
			if (pAdd == 0)
			{
				return;
			}
			if (pTaskAmountMax == 0)
			{
				return;
			}
			int tCountCurrentJobs = pJobsContainer.countCurrentJobs(pJobAsset);
			if (tCountCurrentJobs >= pTaskAmountMax)
			{
				return;
			}
			if (pJobMax != 0 && tCountCurrentJobs >= pJobMax)
			{
				return;
			}
			int tAdd = pAdd;
			if (this._citizens_left <= tAdd)
			{
				tAdd = this._citizens_left;
			}
			this._citizens_left -= tAdd;
			pJobsContainer.addToJob(pJobAsset, tAdd);
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x001E0514 File Offset: 0x001DE714
		private int getPossibleWarriors(City pCity)
		{
			float tMultiplier = pCity.getArmyMaxMultiplier();
			return (int)((float)pCity.status.population_adults * tMultiplier);
		}

		// Token: 0x060046BB RID: 18107 RVA: 0x001E0537 File Offset: 0x001DE737
		private void countRoads(City pCity, CityTasksData pTasks)
		{
			if (pCity.road_tiles_to_build.Count > 0)
			{
				pTasks.roads = 1;
			}
		}

		// Token: 0x060046BC RID: 18108 RVA: 0x001E0550 File Offset: 0x001DE750
		private void countResources(City pCity, CityTasksData pTasks)
		{
			bool tHaveSpaceForBerries = false;
			if (pCity.hasSpaceForResourceInStockpile(ResourceLibrary.berries))
			{
				tHaveSpaceForBerries = true;
			}
			bool tHaveSpaceForHerbs = false;
			if (pCity.hasSpaceForResourceInStockpile(ResourceLibrary.herbs))
			{
				tHaveSpaceForHerbs = true;
			}
			bool tHaveSpaceForHoney = false;
			if (pCity.hasSpaceForResourceInStockpile(ResourceLibrary.honey))
			{
				tHaveSpaceForHoney = true;
			}
			bool tHaveSpaceForWood = false;
			if (pCity.hasSpaceForResourceInStockpile(ResourceLibrary.wood))
			{
				tHaveSpaceForWood = true;
			}
			bool tHaveSpaceForFertilizer = false;
			if (pCity.hasSpaceForResourceInStockpile(ResourceLibrary.fertilizer))
			{
				tHaveSpaceForFertilizer = true;
			}
			for (int i = 0; i < pCity.zones.Count; i++)
			{
				TileZone tZone = pCity.zones[i];
				if (tHaveSpaceForWood)
				{
					pTasks.trees += tZone.countBuildingsType(BuildingList.Trees);
				}
				pTasks.minerals += tZone.countBuildingsType(BuildingList.Minerals);
				pTasks.ruins += tZone.countBuildingsType(BuildingList.Ruins);
				if (tHaveSpaceForBerries)
				{
					HashSet<Building> tSet = tZone.getHashset(BuildingList.Food);
					if (tSet != null)
					{
						using (HashSet<Building>.Enumerator enumerator = tSet.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (enumerator.Current.hasResourcesToCollect())
								{
									pTasks.bushes++;
								}
							}
						}
					}
				}
				if (tHaveSpaceForHerbs)
				{
					pTasks.plants += tZone.countBuildingsType(BuildingList.Flora);
				}
				if (tHaveSpaceForFertilizer)
				{
					pTasks.poops += tZone.countBuildingsType(BuildingList.Poops);
				}
				if (tHaveSpaceForHoney)
				{
					HashSet<Building> tSet2 = tZone.getHashset(BuildingList.Food);
					if (tSet2 != null)
					{
						using (HashSet<Building>.Enumerator enumerator = tSet2.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (enumerator.Current.hasResourcesToCollect())
								{
									pTasks.hives++;
								}
							}
						}
					}
				}
				HashSet<WorldTile> tList = tZone.getTilesOfType(TopTileLibrary.field);
				if (tList != null)
				{
					foreach (WorldTile tTile in tList)
					{
						pTasks.farms_total++;
						if (!tTile.hasBuilding())
						{
							pTasks.farm_fields++;
						}
						else if (tTile.building.asset.wheat && tTile.building.component_wheat.isMaxLevel())
						{
							pTasks.wheats++;
						}
					}
				}
			}
		}

		// Token: 0x060046BD RID: 18109 RVA: 0x001E07C0 File Offset: 0x001DE9C0
		private void countFires(City pCity, CityTasksData pTasks)
		{
			foreach (TileZone tZone in pCity.neighbour_zones)
			{
				if (tZone.city == null)
				{
					pTasks.fire += WorldBehaviourActionFire.countFires(tZone);
				}
			}
			for (int i = 0; i < pCity.zones.Count; i++)
			{
				TileZone tZone2 = pCity.zones[i];
				pTasks.fire += WorldBehaviourActionFire.countFires(tZone2);
			}
		}

		// Token: 0x060046BE RID: 18110 RVA: 0x001E0860 File Offset: 0x001DEA60
		private void checkOccupied(City pCity)
		{
			Dictionary<CitizenJobAsset, int> tOccupied = pCity.jobs.occupied;
			tOccupied.Clear();
			for (int i = 0; i < pCity.units.Count; i++)
			{
				Actor tActor = pCity.units[i];
				if (tActor.citizen_job != null)
				{
					int tValue;
					tOccupied.TryGetValue(tActor.citizen_job, out tValue);
					tOccupied[tActor.citizen_job] = tValue + 1;
				}
			}
		}

		// Token: 0x040031F9 RID: 12793
		private int _citizens_left;
	}
}
