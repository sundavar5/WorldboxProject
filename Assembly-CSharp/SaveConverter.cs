using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000592 RID: 1426
public static class SaveConverter
{
	// Token: 0x06002F5E RID: 12126 RVA: 0x0016DAE4 File Offset: 0x0016BCE4
	public static void convert(SavedMap pData)
	{
		if (pData.saveVersion == 15)
		{
			throw new Exception("saveVersion 15 is not supported");
		}
		if (pData.saveVersion < 12)
		{
			SaveConverter.convertOldAges(pData);
		}
		if (pData.saveVersion < 15)
		{
			SaveConverter.checkOldBuildingID(pData);
		}
		if (pData.saveVersion <= 15)
		{
			SaveConverter.convertTo15(pData);
		}
		if (pData.saveVersion <= 16)
		{
			SaveConverter.convertTo16(pData);
		}
		if (pData.saveVersion <= 17)
		{
			SaveConverter.convertTo17(pData);
		}
	}

	// Token: 0x06002F5F RID: 12127 RVA: 0x0016DB58 File Offset: 0x0016BD58
	public static long kingdomIDFixer(SavedMap pData, long pKingdomID)
	{
		if (pKingdomID != 0L)
		{
			return pKingdomID;
		}
		if (SaveConverter._kingdom_0 == 0L)
		{
			MapStats mapStats = pData.mapStats;
			long id_kingdom = mapStats.id_kingdom;
			mapStats.id_kingdom = id_kingdom + 1L;
			SaveConverter._kingdom_0 = id_kingdom;
		}
		Debug.LogWarning("found kingdom with id 0, changing to " + SaveConverter._kingdom_0.ToString());
		return SaveConverter._kingdom_0;
	}

	// Token: 0x06002F60 RID: 12128 RVA: 0x0016DBAC File Offset: 0x0016BDAC
	public static string assetIDFixer(string pAssetID)
	{
		if (pAssetID.StartsWith("unit_"))
		{
			pAssetID = pAssetID.Replace("unit_", "");
		}
		if (pAssetID.StartsWith("baby_"))
		{
			pAssetID = pAssetID.Replace("baby_", "");
		}
		if (pAssetID == "chick")
		{
			pAssetID = "chicken";
		}
		if (pAssetID == "skeleton_cursed")
		{
			pAssetID = "skeleton";
		}
		if (pAssetID == "whiteMage")
		{
			pAssetID = "white_mage";
		}
		if (pAssetID == "evilMage")
		{
			pAssetID = "evil_mage";
		}
		if (pAssetID == "godFinger")
		{
			pAssetID = "god_finger";
		}
		if (pAssetID == "livingPlants")
		{
			pAssetID = "living_plants";
		}
		if (pAssetID == "livingHouse")
		{
			pAssetID = "living_house";
		}
		if (pAssetID == "walker")
		{
			pAssetID = "cold_one";
		}
		if (pAssetID == "lemon_man")
		{
			pAssetID = "lemon_snail";
		}
		if (pAssetID == "lemon_boi")
		{
			pAssetID = "lemon_snail";
		}
		if (pAssetID == "enchanted_fairy")
		{
			pAssetID = "fairy";
		}
		if (pAssetID == "crystal_golem")
		{
			pAssetID = "crystal_sword";
		}
		return pAssetID;
	}

	// Token: 0x06002F61 RID: 12129 RVA: 0x0016DCE8 File Offset: 0x0016BEE8
	public static void checkMaxValues(SavedMap pData)
	{
		if (pData.mapStats != null)
		{
			if (pData.mapStats.id_unit <= 1L && pData.actors_data != null)
			{
				bool tIncreased = false;
				foreach (ActorData tData in pData.actors_data)
				{
					if (tData.id >= pData.mapStats.id_unit)
					{
						pData.mapStats.id_unit = tData.id + 1L;
						tIncreased = true;
					}
				}
				if (tIncreased)
				{
					Debug.LogWarning("increased id_unit to " + pData.mapStats.id_unit.ToString());
				}
			}
			if (pData.mapStats.id_building <= 1L && pData.buildings != null)
			{
				bool tIncreased2 = false;
				foreach (BuildingData tData2 in pData.buildings)
				{
					if (tData2.id >= pData.mapStats.id_building)
					{
						pData.mapStats.id_building = tData2.id + 1L;
						tIncreased2 = true;
					}
				}
				if (tIncreased2)
				{
					Debug.LogWarning("increased id_building to " + pData.mapStats.id_building.ToString());
				}
			}
			if (pData.mapStats.id_kingdom <= 1L && pData.kingdoms != null)
			{
				bool tIncreased3 = false;
				foreach (KingdomData tData3 in pData.kingdoms)
				{
					if (tData3.id >= pData.mapStats.id_kingdom)
					{
						pData.mapStats.id_kingdom = tData3.id + 1L;
						tIncreased3 = true;
					}
				}
				if (tIncreased3)
				{
					Debug.LogWarning("increased id_kingdom to " + pData.mapStats.id_kingdom.ToString());
				}
			}
			if (pData.mapStats.id_city <= 1L && pData.cities != null)
			{
				bool tIncreased4 = false;
				foreach (CityData tData4 in pData.cities)
				{
					if (tData4.id >= pData.mapStats.id_city)
					{
						pData.mapStats.id_city = tData4.id + 1L;
						tIncreased4 = true;
					}
				}
				if (tIncreased4)
				{
					Debug.LogWarning("increased id_city to " + pData.mapStats.id_city.ToString());
				}
			}
			if (pData.mapStats.id_culture <= 1L && pData.cultures != null)
			{
				bool tIncreased5 = false;
				foreach (CultureData tData5 in pData.cultures)
				{
					if (tData5.id >= pData.mapStats.id_culture)
					{
						pData.mapStats.id_culture = tData5.id + 1L;
						tIncreased5 = true;
					}
				}
				if (tIncreased5)
				{
					Debug.LogWarning("increased id_culture to " + pData.mapStats.id_culture.ToString());
				}
			}
			if (pData.mapStats.id_clan <= 1L && pData.clans != null)
			{
				bool tIncreased6 = false;
				foreach (ClanData tData6 in pData.clans)
				{
					if (tData6.id >= pData.mapStats.id_clan)
					{
						pData.mapStats.id_clan = tData6.id + 1L;
						tIncreased6 = true;
					}
				}
				if (tIncreased6)
				{
					Debug.LogWarning("increased id_clan to " + pData.mapStats.id_clan.ToString());
				}
			}
			if (pData.mapStats.id_alliance <= 1L && pData.alliances != null)
			{
				bool tIncreased7 = false;
				foreach (AllianceData tData7 in pData.alliances)
				{
					if (tData7.id >= pData.mapStats.id_alliance)
					{
						pData.mapStats.id_alliance = tData7.id + 1L;
						tIncreased7 = true;
					}
				}
				if (tIncreased7)
				{
					Debug.LogWarning("increased id_alliance to " + pData.mapStats.id_alliance.ToString());
				}
			}
			if (pData.mapStats.id_war <= 1L && pData.wars != null)
			{
				bool tIncreased8 = false;
				foreach (WarData tData8 in pData.wars)
				{
					if (tData8.id >= pData.mapStats.id_war)
					{
						pData.mapStats.id_war = tData8.id + 1L;
						tIncreased8 = true;
					}
				}
				if (tIncreased8)
				{
					Debug.LogWarning("increased id_war to " + pData.mapStats.id_war.ToString());
				}
			}
			if (pData.mapStats.id_plot <= 1L && pData.plots != null)
			{
				bool tIncreased9 = false;
				foreach (PlotData tData9 in pData.plots)
				{
					if (tData9.id >= pData.mapStats.id_plot)
					{
						pData.mapStats.id_plot = tData9.id + 1L;
						tIncreased9 = true;
					}
				}
				if (tIncreased9)
				{
					Debug.LogWarning("increased id_plot to " + pData.mapStats.id_plot.ToString());
				}
			}
			if (pData.mapStats.id_book <= 1L && pData.books != null)
			{
				bool tIncreased10 = false;
				foreach (BookData tData10 in pData.books)
				{
					if (tData10.id >= pData.mapStats.id_book)
					{
						pData.mapStats.id_book = tData10.id + 1L;
						tIncreased10 = true;
					}
				}
				if (tIncreased10)
				{
					Debug.LogWarning("increased id_book to " + pData.mapStats.id_book.ToString());
				}
			}
			if (pData.mapStats.id_subspecies <= 1L && pData.subspecies != null)
			{
				bool tIncreased11 = false;
				foreach (SubspeciesData tData11 in pData.subspecies)
				{
					if (tData11.id >= pData.mapStats.id_subspecies)
					{
						pData.mapStats.id_subspecies = tData11.id + 1L;
						tIncreased11 = true;
					}
				}
				if (tIncreased11)
				{
					Debug.LogWarning("increased id_subspecies to " + pData.mapStats.id_subspecies.ToString());
				}
			}
			if (pData.mapStats.id_family <= 1L && pData.families != null)
			{
				bool tIncreased12 = false;
				foreach (FamilyData tData12 in pData.families)
				{
					if (tData12.id >= pData.mapStats.id_family)
					{
						pData.mapStats.id_family = tData12.id + 1L;
						tIncreased12 = true;
					}
				}
				if (tIncreased12)
				{
					Debug.LogWarning("increased id_family to " + pData.mapStats.id_family.ToString());
				}
			}
			if (pData.mapStats.id_army <= 1L && pData.armies != null)
			{
				bool tIncreased13 = false;
				foreach (ArmyData tData13 in pData.armies)
				{
					if (tData13.id >= pData.mapStats.id_army)
					{
						pData.mapStats.id_army = tData13.id + 1L;
						tIncreased13 = true;
					}
				}
				if (tIncreased13)
				{
					Debug.LogWarning("increased id_army to " + pData.mapStats.id_army.ToString());
				}
			}
			if (pData.mapStats.id_language <= 1L && pData.languages != null)
			{
				bool tIncreased14 = false;
				foreach (LanguageData tData14 in pData.languages)
				{
					if (tData14.id >= pData.mapStats.id_language)
					{
						pData.mapStats.id_language = tData14.id + 1L;
						tIncreased14 = true;
					}
				}
				if (tIncreased14)
				{
					Debug.LogWarning("increased id_language to " + pData.mapStats.id_language.ToString());
				}
			}
			if (pData.mapStats.id_religion <= 1L && pData.religions != null)
			{
				bool tIncreased15 = false;
				foreach (ReligionData tData15 in pData.religions)
				{
					if (tData15.id >= pData.mapStats.id_religion)
					{
						pData.mapStats.id_religion = tData15.id + 1L;
						tIncreased15 = true;
					}
				}
				if (tIncreased15)
				{
					Debug.LogWarning("increased id_religion to " + pData.mapStats.id_religion.ToString());
				}
			}
			if (pData.mapStats.id_item <= 1L && pData.items != null)
			{
				bool tIncreased16 = false;
				foreach (ItemData tData16 in pData.items)
				{
					if (tData16.id >= pData.mapStats.id_item)
					{
						pData.mapStats.id_item = tData16.id + 1L;
						tIncreased16 = true;
					}
				}
				if (tIncreased16)
				{
					Debug.LogWarning("increased id_item to " + pData.mapStats.id_item.ToString());
				}
			}
			if (pData.mapStats.id_diplomacy <= 1L && pData.relations != null)
			{
				long tMax = pData.mapStats.id_diplomacy;
				foreach (DiplomacyRelationData tData17 in pData.relations)
				{
					if (tData17.id < 100000000L && tData17.id >= tMax)
					{
						tMax = tData17.id + 1L;
					}
				}
				foreach (DiplomacyRelationData tData18 in pData.relations)
				{
					if (tData18.id >= 100000000L)
					{
						BaseSystemData baseSystemData = tData18;
						long num = tMax;
						tMax = num + 1L;
						baseSystemData.id = num;
					}
				}
				pData.mapStats.id_diplomacy = tMax;
			}
		}
	}

	// Token: 0x06002F62 RID: 12130 RVA: 0x0016E8AC File Offset: 0x0016CAAC
	public static void convertTo17(SavedMap pData)
	{
		if (pData.subspecies != null)
		{
			foreach (SubspeciesData tData in pData.subspecies)
			{
				for (int i = 0; i < tData.saved_traits.Count; i++)
				{
					if (tData.saved_traits[i] == "water_creature")
					{
						tData.saved_traits[i] = "aquatic";
					}
					if (tData.saved_traits[i] == "aquatic_adaptation")
					{
						tData.saved_traits[i] = "fins";
					}
				}
			}
		}
	}

	// Token: 0x06002F63 RID: 12131 RVA: 0x0016E96C File Offset: 0x0016CB6C
	public static void convertTo16(SavedMap pData)
	{
		if (pData.buildings != null)
		{
			foreach (BuildingData buildingData in pData.buildings)
			{
				buildingData.asset_id = buildingData.asset_id.Replace("mapple_plant", "maple_plant");
				buildingData.asset_id = buildingData.asset_id.Replace("mapple_tree", "maple_tree");
			}
		}
		if (pData.tileMap != null)
		{
			for (int i = 0; i < pData.tileMap.Count; i++)
			{
				if (pData.tileMap[i].Contains("mapple_"))
				{
					pData.tileMap[i] = pData.tileMap[i].Replace("mapple_", "maple_");
				}
			}
		}
		if (pData.subspecies != null)
		{
			foreach (SubspeciesData tData in pData.subspecies)
			{
				if (tData.biome_variant.Contains("biome_mapple"))
				{
					tData.biome_variant = tData.biome_variant.Replace("biome_mapple", "biome_maple");
				}
			}
		}
	}

	// Token: 0x06002F64 RID: 12132 RVA: 0x0016EAC4 File Offset: 0x0016CCC4
	public static void convertTo15(SavedMap pData)
	{
		SaveConverter._kingdom_0 = 0L;
		SaveConverter.checkMaxValues(pData);
		if (pData.kingdoms != null)
		{
			foreach (KingdomData tData in pData.kingdoms)
			{
				tData.id = SaveConverter.kingdomIDFixer(pData, tData.id);
				tData.original_actor_asset = SaveConverter.assetIDFixer(tData.original_actor_asset);
			}
		}
		if (pData.actors_data != null)
		{
			foreach (ActorData tData2 in pData.actors_data)
			{
				tData2.asset_id = SaveConverter.assetIDFixer(tData2.asset_id);
				tData2.civ_kingdom_id = SaveConverter.kingdomIDFixer(pData, tData2.civ_kingdom_id);
				if (tData2.profession == UnitProfession.Baby)
				{
					tData2.profession = UnitProfession.Nothing;
				}
				if (tData2.saved_traits != null)
				{
					for (int i = 0; i < tData2.saved_traits.Count; i++)
					{
						if (tData2.saved_traits[i] == "mushSpores")
						{
							tData2.saved_traits[i] = "mush_spores";
						}
						if (tData2.saved_traits[i] == "tumorInfection")
						{
							tData2.saved_traits[i] = "tumor_infection";
						}
					}
				}
			}
		}
		if (pData.cities != null)
		{
			foreach (CityData tData3 in pData.cities)
			{
				tData3.kingdomID = SaveConverter.kingdomIDFixer(pData, tData3.kingdomID);
				tData3.original_actor_asset = SaveConverter.assetIDFixer(tData3.original_actor_asset);
			}
		}
		if (pData.books != null)
		{
			foreach (BookData tData4 in pData.books)
			{
				tData4.author_kingdom_id = SaveConverter.kingdomIDFixer(pData, tData4.author_kingdom_id);
			}
		}
		if (pData.religions != null)
		{
			foreach (ReligionData tData5 in pData.religions)
			{
				tData5.creator_kingdom_id = SaveConverter.kingdomIDFixer(pData, tData5.creator_kingdom_id);
			}
		}
		if (pData.alliances != null)
		{
			foreach (AllianceData tData6 in pData.alliances)
			{
				tData6.founder_kingdom_id = SaveConverter.kingdomIDFixer(pData, tData6.founder_kingdom_id);
				List<long> kingdoms = tData6.kingdoms;
				if (kingdoms != null && kingdoms.Contains(0L))
				{
					tData6.kingdoms[tData6.kingdoms.IndexOf(0L)] = SaveConverter.kingdomIDFixer(pData, 0L);
				}
			}
		}
		if (pData.wars != null)
		{
			foreach (WarData tData7 in pData.wars)
			{
				tData7.started_by_kingdom_id = SaveConverter.kingdomIDFixer(pData, tData7.started_by_kingdom_id);
				tData7.main_attacker = SaveConverter.kingdomIDFixer(pData, tData7.main_attacker);
				tData7.main_defender = SaveConverter.kingdomIDFixer(pData, tData7.main_defender);
				List<long> list_attackers = tData7.list_attackers;
				if (list_attackers != null && list_attackers.Contains(0L))
				{
					tData7.list_attackers[tData7.list_attackers.IndexOf(0L)] = SaveConverter.kingdomIDFixer(pData, 0L);
				}
				List<long> list_defenders = tData7.list_defenders;
				if (list_defenders != null && list_defenders.Contains(0L))
				{
					tData7.list_defenders[tData7.list_defenders.IndexOf(0L)] = SaveConverter.kingdomIDFixer(pData, 0L);
				}
			}
		}
		if (pData.clans != null)
		{
			foreach (ClanData tData8 in pData.clans)
			{
				tData8.founder_kingdom_id = SaveConverter.kingdomIDFixer(pData, tData8.founder_kingdom_id);
				tData8.original_actor_asset = SaveConverter.assetIDFixer(tData8.original_actor_asset);
			}
		}
		if (pData.cultures != null)
		{
			foreach (CultureData tData9 in pData.cultures)
			{
				tData9.creator_kingdom_id = SaveConverter.kingdomIDFixer(pData, tData9.creator_kingdom_id);
				tData9.original_actor_asset = SaveConverter.assetIDFixer(tData9.original_actor_asset);
			}
		}
		if (pData.families != null)
		{
			foreach (FamilyData tData10 in pData.families)
			{
				tData10.species_id = SaveConverter.assetIDFixer(tData10.species_id);
				tData10.founder_kingdom_id = SaveConverter.kingdomIDFixer(pData, tData10.founder_kingdom_id);
			}
		}
		if (pData.subspecies != null)
		{
			foreach (SubspeciesData subspeciesData in pData.subspecies)
			{
				subspeciesData.species_id = SaveConverter.assetIDFixer(subspeciesData.species_id);
			}
		}
		if (pData.plots != null)
		{
			foreach (PlotData tData11 in pData.plots)
			{
				if (tData11.plot_type_id == "stop_war")
				{
					tData11.plot_type_id = "attacker_stop_war";
				}
			}
		}
		if (pData.relations != null)
		{
			foreach (DiplomacyRelationData tData12 in pData.relations)
			{
				tData12.kingdom1_id = SaveConverter.kingdomIDFixer(pData, tData12.kingdom1_id);
				tData12.kingdom2_id = SaveConverter.kingdomIDFixer(pData, tData12.kingdom2_id);
			}
		}
	}

	// Token: 0x06002F65 RID: 12133 RVA: 0x0016F154 File Offset: 0x0016D354
	public static void convertOldAges(SavedMap data)
	{
		if (data.actors != null)
		{
			foreach (ActorDataObsolete pOldData in data.actors)
			{
				ActorData tData = SaveConverter.getNewActorData(pOldData);
				if (tData != null)
				{
					data.actors_data.Add(tData);
				}
			}
			foreach (ActorData tData2 in data.actors_data)
			{
				if (data.saveVersion < 11 && tData2.created_time < 0.0)
				{
					tData2.created_time = data.mapStats.world_time + tData2.created_time + (double)Randy.randomFloat(0f, 360f);
				}
			}
			data.actors = null;
		}
		if (data.kingdoms != null)
		{
			foreach (KingdomData tKingdom in data.kingdoms)
			{
				if (tKingdom.created_time < 0.0)
				{
					tKingdom.created_time = data.mapStats.world_time + tKingdom.created_time + (double)Randy.randomFloat(0f, 360f);
				}
			}
		}
		if (data.cities != null)
		{
			foreach (CityData tCity in data.cities)
			{
				if (tCity.created_time < 0.0)
				{
					tCity.created_time = data.mapStats.world_time + tCity.created_time + (double)Randy.randomFloat(0f, 360f);
				}
			}
		}
		if (data.cultures != null)
		{
			foreach (CultureData tCulture in data.cultures)
			{
				if (tCulture.created_time == 0.0 && tCulture.year_obsolete > 0)
				{
					tCulture.created_time = data.mapStats.world_time - (double)((float)tCulture.year_obsolete * 60f) + (double)Randy.randomFloat(0f, 360f);
				}
			}
		}
	}

	// Token: 0x06002F66 RID: 12134 RVA: 0x0016F3E0 File Offset: 0x0016D5E0
	public static void checkOldCityZones(SavedMap pData)
	{
		if (pData.saveVersion < 7)
		{
			for (int i = 0; i < pData.buildings.Count; i++)
			{
				BuildingData tData = pData.buildings[i];
				City tCity = World.world.cities.get(tData.cityID);
				if (tCity != null)
				{
					WorldTile tTile = World.world.GetTile(tData.mainX, tData.mainY);
					tCity.addZone(tTile.zone);
				}
			}
		}
	}

	// Token: 0x06002F67 RID: 12135 RVA: 0x0016F458 File Offset: 0x0016D658
	public static void checkOldBuildingID(SavedMap pData)
	{
		if (SaveConverter._buildings_old_id_dictionary == null)
		{
			SaveConverter._buildings_old_id_dictionary = new Dictionary<string, string[]>();
			SaveConverter._buildings_old_id_dictionary.Add("geyserAcid", new string[]
			{
				"geyser_acid"
			});
			SaveConverter._buildings_old_id_dictionary.Add("tree", new string[]
			{
				"tree_green_1",
				"tree_green_2",
				"tree_green_3"
			});
			SaveConverter._buildings_old_id_dictionary.Add("mushroom", new string[]
			{
				"mushroom_red"
			});
			SaveConverter._buildings_old_id_dictionary.Add("savanna_tree", new string[]
			{
				"savanna_tree_1",
				"savanna_tree_2"
			});
			SaveConverter._buildings_old_id_dictionary.Add("savanna_tree_big", new string[]
			{
				"savanna_tree_big_1",
				"savanna_tree_big_2"
			});
			SaveConverter._buildings_old_id_dictionary.Add("cacti", new string[]
			{
				"cacti_tree"
			});
			SaveConverter._buildings_old_id_dictionary.Add("iron", new string[]
			{
				"mineral_metals"
			});
			SaveConverter._buildings_old_id_dictionary.Add("iron_m", new string[]
			{
				"mineral_metals"
			});
			SaveConverter._buildings_old_id_dictionary.Add("iron_s", new string[]
			{
				"mineral_metals"
			});
			SaveConverter._buildings_old_id_dictionary.Add("gold", new string[]
			{
				"mineral_gold"
			});
			SaveConverter._buildings_old_id_dictionary.Add("gold_m", new string[]
			{
				"mineral_gold"
			});
			SaveConverter._buildings_old_id_dictionary.Add("gold_s", new string[]
			{
				"mineral_gold"
			});
			SaveConverter._buildings_old_id_dictionary.Add("ore_deposit", new string[]
			{
				"mineral_metals"
			});
			SaveConverter._buildings_old_id_dictionary.Add("ore_deposit_m", new string[]
			{
				"mineral_metals"
			});
			SaveConverter._buildings_old_id_dictionary.Add("ore_deposit_s", new string[]
			{
				"mineral_metals"
			});
			SaveConverter._buildings_old_id_dictionary.Add("palm", new string[]
			{
				"palm_tree"
			});
			SaveConverter._buildings_old_id_dictionary.Add("pine", new string[]
			{
				"pine_tree"
			});
			SaveConverter._buildings_old_id_dictionary.Add("stone", new string[]
			{
				"mineral_stone"
			});
			SaveConverter._buildings_old_id_dictionary.Add("stone_m", new string[]
			{
				"mineral_stone"
			});
			SaveConverter._buildings_old_id_dictionary.Add("stone_s", new string[]
			{
				"mineral_stone"
			});
			SaveConverter._buildings_old_id_dictionary.Add("ruins_small", new string[]
			{
				"poop"
			});
			SaveConverter._buildings_old_id_dictionary.Add("ruins_medium", new string[]
			{
				"poop"
			});
			SaveConverter._buildings_old_id_dictionary.Add("house_human", new string[]
			{
				"house_human_0"
			});
			SaveConverter._buildings_old_id_dictionary.Add("1house_human", new string[]
			{
				"house_human_1"
			});
			SaveConverter._buildings_old_id_dictionary.Add("2house_human", new string[]
			{
				"house_human_2"
			});
			SaveConverter._buildings_old_id_dictionary.Add("3house_human", new string[]
			{
				"house_human_3"
			});
			SaveConverter._buildings_old_id_dictionary.Add("4house_human", new string[]
			{
				"house_human_4"
			});
			SaveConverter._buildings_old_id_dictionary.Add("5house_human", new string[]
			{
				"house_human_5"
			});
			SaveConverter._buildings_old_id_dictionary.Add("hall_human", new string[]
			{
				"hall_human_0"
			});
			SaveConverter._buildings_old_id_dictionary.Add("1hall_human", new string[]
			{
				"hall_human_1"
			});
			SaveConverter._buildings_old_id_dictionary.Add("2hall_human", new string[]
			{
				"hall_human_2"
			});
			SaveConverter._buildings_old_id_dictionary.Add("windmill_human", new string[]
			{
				"windmill_human_0"
			});
			SaveConverter._buildings_old_id_dictionary.Add("1windmill_human", new string[]
			{
				"windmill_human_1"
			});
			SaveConverter._buildings_old_id_dictionary.Add("house_elf", new string[]
			{
				"house_elf_0"
			});
			SaveConverter._buildings_old_id_dictionary.Add("1house_elf", new string[]
			{
				"house_elf_1"
			});
			SaveConverter._buildings_old_id_dictionary.Add("2house_elf", new string[]
			{
				"house_elf_2"
			});
			SaveConverter._buildings_old_id_dictionary.Add("3house_elf", new string[]
			{
				"house_elf_3"
			});
			SaveConverter._buildings_old_id_dictionary.Add("4house_elf", new string[]
			{
				"house_elf_4"
			});
			SaveConverter._buildings_old_id_dictionary.Add("5house_elf", new string[]
			{
				"house_elf_5"
			});
			SaveConverter._buildings_old_id_dictionary.Add("hall_elf", new string[]
			{
				"hall_elf_0"
			});
			SaveConverter._buildings_old_id_dictionary.Add("1hall_elf", new string[]
			{
				"hall_elf_1"
			});
			SaveConverter._buildings_old_id_dictionary.Add("2hall_elf", new string[]
			{
				"hall_elf_2"
			});
			SaveConverter._buildings_old_id_dictionary.Add("windmill_elf", new string[]
			{
				"windmill_elf_0"
			});
			SaveConverter._buildings_old_id_dictionary.Add("1windmill_elf", new string[]
			{
				"windmill_elf_1"
			});
			SaveConverter._buildings_old_id_dictionary.Add("house_orc", new string[]
			{
				"house_orc_0"
			});
			SaveConverter._buildings_old_id_dictionary.Add("1house_orc", new string[]
			{
				"house_orc_1"
			});
			SaveConverter._buildings_old_id_dictionary.Add("2house_orc", new string[]
			{
				"house_orc_2"
			});
			SaveConverter._buildings_old_id_dictionary.Add("3house_orc", new string[]
			{
				"house_orc_3"
			});
			SaveConverter._buildings_old_id_dictionary.Add("4house_orc", new string[]
			{
				"house_orc_4"
			});
			SaveConverter._buildings_old_id_dictionary.Add("5house_orc", new string[]
			{
				"house_orc_5"
			});
			SaveConverter._buildings_old_id_dictionary.Add("hall_orc", new string[]
			{
				"hall_orc_0"
			});
			SaveConverter._buildings_old_id_dictionary.Add("1hall_orc", new string[]
			{
				"hall_orc_1"
			});
			SaveConverter._buildings_old_id_dictionary.Add("2hall_orc", new string[]
			{
				"hall_orc_2"
			});
			SaveConverter._buildings_old_id_dictionary.Add("windmill_orc", new string[]
			{
				"windmill_orc_0"
			});
			SaveConverter._buildings_old_id_dictionary.Add("1windmill_orc", new string[]
			{
				"windmill_orc_1"
			});
			SaveConverter._buildings_old_id_dictionary.Add("house_dwarf", new string[]
			{
				"house_dwarf_0"
			});
			SaveConverter._buildings_old_id_dictionary.Add("1house_dwarf", new string[]
			{
				"house_dwarf_1"
			});
			SaveConverter._buildings_old_id_dictionary.Add("2house_dwarf", new string[]
			{
				"house_dwarf_2"
			});
			SaveConverter._buildings_old_id_dictionary.Add("3house_dwarf", new string[]
			{
				"house_dwarf_3"
			});
			SaveConverter._buildings_old_id_dictionary.Add("4house_dwarf", new string[]
			{
				"house_dwarf_4"
			});
			SaveConverter._buildings_old_id_dictionary.Add("5house_dwarf", new string[]
			{
				"house_dwarf_5"
			});
			SaveConverter._buildings_old_id_dictionary.Add("hall_dwarf", new string[]
			{
				"hall_dwarf_0"
			});
			SaveConverter._buildings_old_id_dictionary.Add("1hall_dwarf", new string[]
			{
				"hall_dwarf_1"
			});
			SaveConverter._buildings_old_id_dictionary.Add("2hall_dwarf", new string[]
			{
				"hall_dwarf_2"
			});
			SaveConverter._buildings_old_id_dictionary.Add("windmill_dwarf", new string[]
			{
				"windmill_dwarf_0"
			});
			SaveConverter._buildings_old_id_dictionary.Add("1windmill_dwarf", new string[]
			{
				"windmill_dwarf_1"
			});
			SaveConverter._buildings_old_id_dictionary.Add("0wheat", new string[]
			{
				"wheat"
			});
			SaveConverter._buildings_old_id_dictionary.Add("1wheat", new string[]
			{
				"wheat"
			});
			SaveConverter._buildings_old_id_dictionary.Add("2wheat", new string[]
			{
				"wheat"
			});
			SaveConverter._buildings_old_id_dictionary.Add("3wheat", new string[]
			{
				"wheat"
			});
			SaveConverter._buildings_old_id_dictionary.Add("4wheat", new string[]
			{
				"wheat"
			});
			SaveConverter._buildings_old_id_dictionary.Add("wheat_0", new string[]
			{
				"wheat"
			});
			SaveConverter._buildings_old_id_dictionary.Add("wheat_1", new string[]
			{
				"wheat"
			});
			SaveConverter._buildings_old_id_dictionary.Add("wheat_2", new string[]
			{
				"wheat"
			});
			SaveConverter._buildings_old_id_dictionary.Add("wheat_3", new string[]
			{
				"wheat"
			});
			SaveConverter._buildings_old_id_dictionary.Add("wheat_4", new string[]
			{
				"wheat"
			});
			SaveConverter._buildings_old_id_dictionary.Add("goldenBrain", new string[]
			{
				"golden_brain"
			});
			SaveConverter._buildings_old_id_dictionary.Add("corruptedBrain", new string[]
			{
				"corrupted_brain"
			});
			SaveConverter._buildings_old_id_dictionary.Add("flameTower", new string[]
			{
				"flame_tower"
			});
			SaveConverter._buildings_old_id_dictionary.Add("iceTower", new string[]
			{
				"ice_tower"
			});
			SaveConverter._buildings_old_id_dictionary.Add("superPumpkin", new string[]
			{
				"super_pumpkin"
			});
		}
		if (pData.buildings == null)
		{
			return;
		}
		foreach (BuildingData tData in pData.buildings)
		{
			if (SaveConverter._buildings_old_id_dictionary.ContainsKey(tData.asset_id))
			{
				tData.asset_id = SaveConverter._buildings_old_id_dictionary[tData.asset_id].GetRandom<string>();
			}
			if (tData.state == BuildingState.None)
			{
				tData.state = BuildingState.Normal;
			}
			if (tData.state == BuildingState.CivKingdom)
			{
				tData.state = BuildingState.Normal;
			}
			if (tData.state == BuildingState.CivAbandoned)
			{
				tData.state = BuildingState.Normal;
			}
		}
	}

	// Token: 0x06002F68 RID: 12136 RVA: 0x0016FE6C File Offset: 0x0016E06C
	private static ActorData getNewActorData(ActorDataObsolete pOldData)
	{
		ActorData tData = pOldData.status;
		if (string.IsNullOrEmpty(tData.asset_id))
		{
			Debug.Log("skipping unit because it's missing an asset_id");
			return null;
		}
		tData.x = pOldData.x;
		tData.y = pOldData.y;
		tData.cityID = pOldData.cityID;
		List<long> saved_items = pOldData.saved_items;
		if (saved_items != null && saved_items.Count > 0)
		{
			tData.saved_items = pOldData.saved_items;
		}
		tData.inventory = pOldData.inventory;
		if (tData.inventory.isEmpty())
		{
			tData.inventory.empty();
		}
		return tData;
	}

	// Token: 0x04002398 RID: 9112
	private static Dictionary<string, string[]> _buildings_old_id_dictionary;

	// Token: 0x04002399 RID: 9113
	private static long _kingdom_0;
}
