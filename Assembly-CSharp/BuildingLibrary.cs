using System;
using System.Collections.Generic;
using System.Reflection;
using Beebyte.Obfuscator;
using strings;
using UnityEngine;

// Token: 0x020000C0 RID: 192
[ObfuscateLiterals]
public class BuildingLibrary : AssetLibrary<BuildingAsset>
{
	// Token: 0x06000602 RID: 1538 RVA: 0x0005736C File Offset: 0x0005556C
	public override void init()
	{
		base.init();
		this.addTrees();
		this.addVegetation();
		this.addMinerals();
		this.addPoop();
		this.addGrownResources();
		this.addGeneralCityBuildings();
		this.addNatureBuildings();
		this.addMobBuildings();
		this.addCreeps();
		this.addHumans();
		this.addOrcs();
		this.addElves();
		this.addDwarves();
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x000573CD File Offset: 0x000555CD
	public override void post_init()
	{
		base.post_init();
		this.initBuildingsFromArchitectures();
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x000573DC File Offset: 0x000555DC
	public override void linkAssets()
	{
		base.linkAssets();
		this.checkAtlasLink(true);
		foreach (BuildingAsset tAsset in this.list)
		{
			if (tAsset.step_action != null)
			{
				tAsset.has_step_action = true;
			}
			if (tAsset.get_map_icon_color != null)
			{
				tAsset.has_get_map_icon_color = true;
			}
			BuildingAsset buildingAsset = tAsset;
			HashSet<BiomeTag> biome_tags_growth = tAsset.biome_tags_growth;
			buildingAsset.has_biome_tags = (biome_tags_growth != null && biome_tags_growth.Count > 0);
			BuildingAsset buildingAsset2 = tAsset;
			HashSet<BiomeTag> biome_tags_spread = tAsset.biome_tags_spread;
			buildingAsset2.has_biome_tags_spread = (biome_tags_spread != null && biome_tags_spread.Count > 0);
		}
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x0005748C File Offset: 0x0005568C
	public void checkAtlasLink(bool pWobbleTreesSettingIsActive)
	{
		foreach (BuildingAsset tAsset in this.list)
		{
			if (!pWobbleTreesSettingIsActive)
			{
				tAsset.atlas_asset = AssetManager.dynamic_sprites_library.get(tAsset.atlas_id_fallback_when_not_wobbly);
			}
			else
			{
				tAsset.atlas_asset = AssetManager.dynamic_sprites_library.get(tAsset.atlas_id);
			}
		}
	}

	// Token: 0x06000606 RID: 1542 RVA: 0x0005750C File Offset: 0x0005570C
	private void initBuildingsFromArchitectures()
	{
		foreach (ArchitectureAsset tArchitectureAsset in AssetManager.architecture_library.list)
		{
			if (!tArchitectureAsset.isTemplateAsset() && tArchitectureAsset.generate_buildings)
			{
				string tArchitectureID = tArchitectureAsset.id;
				foreach (string tStyledBuildingOrderID in tArchitectureAsset.styled_building_orders)
				{
					string tNewBuildingID = tArchitectureAsset.building_ids_for_construction[tStyledBuildingOrderID];
					string tTargetArchitectureID = tArchitectureAsset.generation_target;
					BuildingAsset tOriginalBuildingAsset = AssetManager.architecture_library.get(tTargetArchitectureID).getBuilding(tStyledBuildingOrderID);
					BuildingAsset tNewBuildingAsset = this.clone(tNewBuildingID, tOriginalBuildingAsset.id);
					tNewBuildingAsset.group = "civ_building";
					tNewBuildingAsset.mini_civ_auto_load = true;
					tNewBuildingAsset.civ_kingdom = tArchitectureID;
					tNewBuildingAsset.main_path = "buildings/civ_main/" + tArchitectureID + "/";
					tNewBuildingAsset.can_be_upgraded = false;
					tNewBuildingAsset.has_sprite_construction = true;
					if (tArchitectureAsset.spread_biome)
					{
						tNewBuildingAsset.spread_biome = true;
						tNewBuildingAsset.spread_biome_id = tArchitectureAsset.spread_biome_id;
					}
					tNewBuildingAsset.material = tArchitectureAsset.material;
					if (tNewBuildingAsset.material == "jelly")
					{
						tNewBuildingAsset.setAtlasID("buildings_wobbly", "buildings");
					}
					tNewBuildingAsset.shadow = tArchitectureAsset.has_shadows;
					tNewBuildingAsset.burnable = tArchitectureAsset.burnable_buildings;
					tNewBuildingAsset.affected_by_acid = tArchitectureAsset.acid_affected_buildings;
					uint num = <PrivateImplementationDetails>.ComputeStringHash(tStyledBuildingOrderID);
					if (num <= 2808412044U)
					{
						if (num <= 735917779U)
						{
							if (num != 279793225U)
							{
								if (num == 735917779U)
								{
									if (tStyledBuildingOrderID == "order_library")
									{
										tNewBuildingAsset.fundament = new BuildingFundament(2, 2, 2, 0);
									}
								}
							}
							else if (tStyledBuildingOrderID == "order_temple")
							{
								tNewBuildingAsset.fundament = new BuildingFundament(2, 2, 3, 0);
							}
						}
						else if (num != 2323241755U)
						{
							if (num != 2612399145U)
							{
								if (num == 2808412044U)
								{
									if (tStyledBuildingOrderID == "order_hall_0")
									{
										tNewBuildingAsset.fundament = new BuildingFundament(3, 3, 4, 0);
									}
								}
							}
							else if (tStyledBuildingOrderID == "order_tent")
							{
								tNewBuildingAsset.fundament = new BuildingFundament(2, 2, 2, 0);
							}
						}
						else if (!(tStyledBuildingOrderID == "order_house_0"))
						{
						}
					}
					else if (num <= 3352047755U)
					{
						if (num != 2867511799U)
						{
							if (num == 3352047755U)
							{
								if (!(tStyledBuildingOrderID == "order_barracks"))
								{
								}
							}
						}
						else if (tStyledBuildingOrderID == "order_windmill_0")
						{
							tNewBuildingAsset.fundament = new BuildingFundament(2, 2, 2, 0);
							if (tNewBuildingAsset.shadow)
							{
								tNewBuildingAsset.setShadow(0.4f, 0.38f, 0.47f);
							}
						}
					}
					else if (num != 3863055912U)
					{
						if (num != 3879833531U)
						{
							if (num == 4148002217U)
							{
								if (tStyledBuildingOrderID == "order_watch_tower")
								{
									tNewBuildingAsset.fundament = new BuildingFundament(1, 1, 1, 0);
									if (!string.IsNullOrEmpty(tArchitectureAsset.projectile_id))
									{
										tNewBuildingAsset.tower_projectile = tArchitectureAsset.projectile_id;
									}
								}
							}
						}
						else if (tStyledBuildingOrderID == "order_docks_0")
						{
							string tID_docks = "docks_" + tArchitectureID;
							tNewBuildingAsset.upgrade_to = tID_docks;
							tNewBuildingAsset.can_be_upgraded = true;
						}
					}
					else if (tStyledBuildingOrderID == "order_docks_1")
					{
						string tID_fishing_docks_upgraded_from = "fishing_docks_" + tArchitectureID;
						tNewBuildingAsset.upgraded_from = tID_fishing_docks_upgraded_from;
						tNewBuildingAsset.has_sprites_main_disabled = false;
					}
				}
			}
		}
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x00057918 File Offset: 0x00055B18
	private void addTrees()
	{
		this.add(new BuildingAsset
		{
			id = "tree_green_1",
			fundament = new BuildingFundament(1, 1, 1, 0),
			building_type = BuildingType.Building_Tree,
			type = "type_tree",
			destroy_on_liquid = true,
			random_flip = true,
			ignored_by_cities = true,
			burnable = true,
			affected_by_acid = true,
			affected_by_lava = true,
			flora = true,
			flora_size = FloraSize.Big,
			can_be_damaged_by_tornado = true,
			group = "nature",
			kingdom = "nature",
			check_for_close_building = false,
			biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[1]),
			material = "tree",
			affected_by_drought = true,
			affected_by_cold_temperature = true,
			main_path = "buildings/trees/",
			can_be_chopped_down = true,
			has_resources_to_collect = true,
			is_vegetation = true
		});
		this.t.setAtlasID("buildings_trees", "buildings");
		this.t.nutrition_restore = 40;
		this.t.sound_spawn = "event:/SFX/NATURE/BaseFloraSpawn";
		this.t.remove_ruins = false;
		this.t.setSpread(FloraType.Tree, 10, 0.5f);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"tree_green_1"
		});
		this.t.setShadow(0.5f, 0.14f, 0.08f);
		this.t.limit_per_zone = 3;
		this.t.can_be_living_plant = true;
		this.t.base_stats["health"] = 10f;
		this.t.addResource("wood", 5, false);
		this.t.has_sprites_main = true;
		this.t.has_sprites_ruin = true;
		this.clone("tree_green_2", "tree_green_1");
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"tree_green_2"
		});
		this.clone("tree_green_3", "tree_green_1");
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"tree_green_3"
		});
		this.clone("corrupted_tree", "tree_green_1");
		this.t.become_alive_when_chopped = true;
		this.t.limit_per_zone = 4;
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Corrupted
		});
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"corrupted_tree",
			"corrupted_tree_big"
		});
		this.t.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleCorruptedTree";
		this.t.affected_by_cold_temperature = false;
		this.clone("corrupted_tree_big", "corrupted_tree");
		this.t.become_alive_when_chopped = true;
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Corrupted
		});
		this.t.fundament = new BuildingFundament(2, 2, 1, 0);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"corrupted_tree",
			"corrupted_tree_big"
		});
		this.clone("enchanted_tree", "tree_green_1");
		this.t.limit_per_zone = 4;
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.2f;
		this.t.draw_light_area_offset_y = 2f;
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Enchanted
		});
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Green,
			BiomeTag.Clover,
			BiomeTag.Flower,
			BiomeTag.Garlic,
			BiomeTag.Maple,
			BiomeTag.Birch,
			BiomeTag.Enchanted
		});
		this.t.setShadow(0.5f, 0.03f, 0.12f);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"enchanted_tree"
		});
		this.clone("swamp_tree", "tree_green_1");
		this.t.fundament = new BuildingFundament(1, 1, 1, 0);
		this.t.limit_per_zone = 2;
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Swamp
		});
		this.t.can_be_placed_on_liquid = true;
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"swamp_tree"
		});
		this.clone("savanna_tree_1", "tree_green_1");
		this.t.limit_per_zone = 3;
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Savanna
		});
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"savanna_tree_1",
			"savanna_tree_big_1"
		});
		this.clone("savanna_tree_2", "savanna_tree_1");
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"savanna_tree_2",
			"savanna_tree_big_2"
		});
		this.clone("savanna_tree_big_1", "savanna_tree_1");
		this.t.fundament = new BuildingFundament(2, 2, 1, 0);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"savanna_tree_1",
			"savanna_tree_big_1"
		});
		this.clone("savanna_tree_big_2", "savanna_tree_big_1");
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"savanna_tree_2",
			"savanna_tree_big_2"
		});
		this.clone("mushroom_tree", "tree_green_1");
		this.t.limit_per_zone = 2;
		this.t.setSpread(FloraType.Fungi, 10, 0.45f);
		BuildingAsset t = this.t;
		BiomeTag[] array = new BiomeTag[2];
		array[0] = BiomeTag.Mushroom;
		t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(array);
		this.t.addResource("mushrooms", 1, false);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"mushroom_tree"
		});
		this.clone("jungle_tree", "tree_green_1");
		this.t.limit_per_zone = 8;
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Jungle
		});
		this.t.addResource("bananas", 1, false);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"jungle_tree"
		});
		this.clone("infernal_tree", "tree_green_1");
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.05f;
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Infernal
		});
		this.t.burnable = false;
		this.t.affected_by_drought = false;
		this.t.setShadow(0.1f, 0.31f, 0.33f);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"infernal_tree",
			"infernal_tree_small",
			"infernal_tree_big"
		});
		this.t.affected_by_cold_temperature = false;
		this.clone("infernal_tree_small", "infernal_tree");
		this.t.fundament = new BuildingFundament(0, 0, 1, 0);
		this.t.setShadow(0.5f, 0.31f, 0.33f);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"infernal_tree",
			"infernal_tree_small",
			"infernal_tree_big"
		});
		this.clone("infernal_tree_big", "infernal_tree");
		this.t.fundament = new BuildingFundament(2, 2, 1, 0);
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.1f;
		this.t.setShadow(0.37f, 0.16f, 0.2f);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"infernal_tree",
			"infernal_tree_small",
			"infernal_tree_big"
		});
		this.clone("cacti_tree", "tree_green_1");
		this.t.affected_by_drought = false;
		this.t.vegetation_random_chance = 0.2f;
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Sand
		});
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"cacti_tree"
		});
		this.clone("palm_tree", "tree_green_1");
		this.t.vegetation_random_chance = 0.1f;
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Sand
		});
		this.t.setShadow(0.37f, 0.16f, 0f);
		this.t.addResource("coconut", 1, false);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"palm_tree"
		});
		this.clone("desert_tree", "tree_green_1");
		this.t.affected_by_drought = false;
		this.t.limit_per_zone = 1;
		this.t.vegetation_random_chance = 0.1f;
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Desert
		});
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"desert_tree"
		});
		this.clone("crystal_tree", "tree_green_1");
		this.t.affected_by_drought = false;
		this.t.burnable = false;
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.1f;
		this.t.limit_per_zone = 1;
		this.t.vegetation_random_chance = 0.1f;
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Crystal,
			BiomeTag.Rocklands
		});
		this.t.material = "building";
		this.t.setAtlasID("buildings", null);
		this.t.sparkle_effect = true;
		this.t.addResource("wood", 5, true);
		this.t.addResource("stone", 1, false);
		this.t.addResource("gems", 1, false);
		this.t.addResource("common_metals", 1, false);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"crystal_tree"
		});
		this.t.affected_by_cold_temperature = false;
		this.clone("wasteland_tree", "tree_green_1");
		this.t.vegetation_random_chance = 0.5f;
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Soil,
			BiomeTag.Wasteland
		});
		this.t.affected_by_acid = false;
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"wasteland_tree"
		});
		this.clone("candy_tree", "tree_green_1");
		this.t.limit_per_zone = 1;
		this.t.vegetation_random_chance = 0.1f;
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Candy
		});
		this.t.addResource("candy", 3, false);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"candy_tree"
		});
		this.t.affected_by_cold_temperature = false;
		this.clone("lemon_tree", "tree_green_1");
		this.t.limit_per_zone = 1;
		this.t.vegetation_random_chance = 0.1f;
		BuildingAsset t2 = this.t;
		BiomeTag[] array2 = new BiomeTag[2];
		array2[0] = BiomeTag.Lemon;
		t2.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(array2);
		this.t.addResource("lemons", 3, false);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"lemon_tree"
		});
		this.t.affected_by_cold_temperature = false;
		this.clone("pine_tree", "tree_green_1");
		this.t.vegetation_random_chance = 0.5f;
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Soil,
			BiomeTag.Green,
			BiomeTag.Permafrost
		});
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"pine_tree"
		});
		this.t.addResource("pine_cones", 3, false);
		this.t.affected_by_cold_temperature = false;
		this.clone("birch_tree", "tree_green_1");
		this.t.vegetation_random_chance = 0.5f;
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Green,
			BiomeTag.Birch
		});
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"birch_tree"
		});
		this.clone("maple_tree", "tree_green_1");
		this.t.vegetation_random_chance = 0.5f;
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Green,
			BiomeTag.Maple
		});
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"maple_tree"
		});
		this.clone("garlic_tree", "tree_green_1");
		this.t.vegetation_random_chance = 0.5f;
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Garlic
		});
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"garlic_tree"
		});
		this.clone("flower_tree_1", "tree_green_1");
		this.t.setSpread(FloraType.Plant, 10, 0.3f);
		this.t.vegetation_random_chance = 0.5f;
		this.t.flora_size = FloraSize.Big;
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Flower
		});
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"flower_tree_1"
		});
		this.clone("flower_tree_2", "flower_tree_1");
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"flower_tree_2"
		});
		this.clone("flower_tree_3", "flower_tree_1");
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"flower_tree_3"
		});
		this.clone("rocklands_tree", "tree_green_1");
		this.t.vegetation_random_chance = 0.5f;
		this.t.material = "building";
		this.t.setAtlasID("buildings", null);
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Rocklands
		});
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"rocklands_tree"
		});
		this.t.limit_in_radius = 6;
		this.t.affected_by_cold_temperature = false;
		this.clone("celestial_tree", "tree_green_1");
		this.t.can_be_living_plant = false;
		this.t.ignored_by_cities = false;
		this.t.can_be_chopped_down = false;
		this.t.material = "tree_celestial";
		this.t.setAtlasID("buildings_trees_big", null);
		this.t.vegetation_random_chance = 0.5f;
		this.t.setShadow(0.5f, 0.03f, 0.05f);
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Celestial
		});
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"celestial_tree"
		});
		this.t.fundament = new BuildingFundament(2, 2, 2, 0);
		this.t.limit_per_zone = 1;
		this.t.limit_in_radius = 30;
		this.t.affected_by_cold_temperature = false;
		this.t.draw_light_area = true;
		this.t.draw_light_size = 1f;
		this.t.addResource("celestial_avocado", 3, false);
		this.t.addResource("wood", 100, false);
		this.clone("celestial_tree_small", "tree_green_1");
		this.t.vegetation_random_chance = 0.5f;
		this.t.material = "building";
		this.t.setAtlasID("buildings_trees", "buildings");
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Celestial
		});
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"celestial_tree_small"
		});
		this.t.limit_in_radius = 6;
		this.t.limit_per_zone = 15;
		this.t.affected_by_cold_temperature = false;
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.2f;
		this.t.addResource("celestial_avocado", 1, false);
		this.clone("singularity_tree", "tree_green_1");
		this.t.vegetation_random_chance = 0.5f;
		this.t.limit_per_zone = 1;
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Singularity
		});
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"singularity_tree"
		});
		this.t.affected_by_cold_temperature = false;
		this.clone("clover_tree", "tree_green_1");
		this.t.vegetation_random_chance = 0.5f;
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Clover
		});
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"clover_tree"
		});
		this.clone("paradox_tree", "tree_green_1");
		this.t.vegetation_random_chance = 0.5f;
		this.t.limit_per_zone = 1;
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Paradox
		});
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"paradox_tree"
		});
		this.t.affected_by_cold_temperature = false;
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x00058B3C File Offset: 0x00056D3C
	private void addVegetation()
	{
		this.add(new BuildingAsset
		{
			id = "$flora_small$",
			fundament = new BuildingFundament(0, 0, 0, 0),
			has_ruins_graphics = false,
			destroy_on_liquid = true,
			random_flip = true,
			ignored_by_cities = true,
			burnable = true,
			affected_by_acid = true,
			affected_by_lava = true,
			flora = true,
			flora_size = FloraSize.Tiny,
			affected_by_cold_temperature = true,
			group = "nature",
			kingdom = "nature",
			building_type = BuildingType.Building_Plant,
			material = "tree",
			main_path = "buildings/vegetation/",
			is_vegetation = true
		});
		this.t.setAtlasID("buildings_trees", "buildings");
		this.t.has_ruin_state = false;
		this.t.remove_ruins = false;
		this.t.setSpread(FloraType.Plant, 5, 0.3f);
		this.t.type = "type_vegetation";
		this.t.nutrition_restore = 10;
		this.t.limit_per_zone = 5;
		this.t.priority = -1;
		this.t.can_be_placed_on_blocks = false;
		this.t.base_stats["health"] = 10f;
		this.t.sound_spawn = "event:/SFX/NATURE/BaseFloraSpawn";
		this.t.shadow = false;
		this.t.addResource("herbs", 1, false);
		this.t.has_sprites_main = true;
		this.clone("desert_plant", "$flora_small$");
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Desert
		});
		this.t.limit_per_zone = 3;
		this.t.addResource("desert_berries", 1, true);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"desert_plant"
		});
		this.clone("crystal_plant", "$flora_small$");
		this.t.limit_per_zone = 2;
		this.t.setShadow(0.19f, 0.03f, 0.09f);
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Crystal
		});
		this.t.material = "building";
		this.t.setAtlasID("buildings", null);
		this.t.burnable = false;
		this.t.sparkle_effect = true;
		this.t.addResource("gems", 1, true);
		this.t.addResource("crystal_salt", 1, false);
		this.t.addResource("common_metals", 2, false);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"crystal_plant"
		});
		this.t.affected_by_cold_temperature = false;
		this.clone("candy_plant", "$flora_small$");
		this.t.setShadow(0.19f, 0.03f, 0.09f);
		this.t.fundament = new BuildingFundament(1, 1, 1, 0);
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Candy
		});
		this.t.addResource("candy", 1, true);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"candy_plant"
		});
		this.t.affected_by_cold_temperature = false;
		this.clone("snow_plant", "$flora_small$");
		this.t.limit_per_zone = 4;
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Permafrost
		});
		this.t.addResource("snow_cucumbers", 1, false);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"snow_plant"
		});
		this.t.affected_by_cold_temperature = false;
		this.clone("green_herb", "$flora_small$");
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Green,
			BiomeTag.Lemon,
			BiomeTag.Jungle
		});
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"green_herb"
		});
		this.clone("corrupted_plant", "$flora_small$");
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Corrupted
		});
		this.t.addResource("evil_beets", 1, true);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"corrupted_plant"
		});
		this.t.affected_by_cold_temperature = false;
		this.clone("jungle_plant", "$flora_small$");
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Jungle
		});
		this.t.limit_per_zone = 6;
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"jungle_plant"
		});
		this.clone("savanna_plant", "$flora_small$");
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Savanna
		});
		this.t.addResource("wheat", 1, false);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"savanna_plant"
		});
		this.clone("mushroom_red", "$flora_small$");
		this.t.fundament = new BuildingFundament(1, 1, 1, 0);
		this.t.limit_per_zone = 9;
		this.t.setSpread(FloraType.Fungi, 4, 0.5f);
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Lemon,
			BiomeTag.Green,
			BiomeTag.Mushroom
		});
		this.t.biome_tags_spread = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Hills
		});
		this.t.addResource("mushrooms", 1, true);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"mushroom_red"
		});
		this.clone("mushroom_green", "mushroom_red");
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"mushroom_green"
		});
		this.clone("mushroom_teal", "mushroom_red");
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"mushroom_teal"
		});
		this.clone("mushroom_white", "mushroom_red");
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"mushroom_white"
		});
		this.clone("mushroom_yellow", "mushroom_red");
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"mushroom_yellow"
		});
		this.clone("flower", "$flora_small$");
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Lemon,
			BiomeTag.Green,
			BiomeTag.Mushroom,
			BiomeTag.Enchanted
		});
		this.t.type = "type_flower";
		this.t.nutrition_restore = 15;
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"flower"
		});
		this.clone("flame_flower", "flower");
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Infernal
		});
		this.t.burnable = false;
		this.t.addResource("peppers", 1, true);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"flame_flower"
		});
		this.t.affected_by_cold_temperature = false;
		this.clone("jungle_flower", "flower");
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Jungle
		});
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"jungle_flower"
		});
		this.clone("wasteland_flower", "flower");
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Wasteland
		});
		this.t.affected_by_acid = false;
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"wasteland_flower"
		});
		this.clone("swamp_plant", "$flora_small$");
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Swamp
		});
		this.t.fundament = new BuildingFundament(0, 0, 0, 0);
		this.t.can_be_placed_on_liquid = true;
		this.t.limit_per_zone = 4;
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"swamp_plant",
			"swamp_plant_big"
		});
		this.clone("swamp_plant_big", "swamp_plant");
		this.t.limit_per_zone = 4;
		this.t.fundament = new BuildingFundament(1, 1, 1, 0);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"swamp_plant",
			"swamp_plant_big"
		});
		this.clone("birch_plant", "flower");
		BuildingAsset t = this.t;
		BiomeTag[] array = new BiomeTag[2];
		array[0] = BiomeTag.Birch;
		t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(array);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"birch_plant"
		});
		this.clone("maple_plant", "flower");
		BuildingAsset t2 = this.t;
		BiomeTag[] array2 = new BiomeTag[2];
		array2[0] = BiomeTag.Maple;
		t2.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(array2);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"maple_plant"
		});
		this.clone("flower_plant", "flower");
		BuildingAsset t3 = this.t;
		BiomeTag[] array3 = new BiomeTag[2];
		array3[0] = BiomeTag.Flower;
		t3.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(array3);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"flower_plant"
		});
		this.clone("garlic_plant", "flower");
		BuildingAsset t4 = this.t;
		BiomeTag[] array4 = new BiomeTag[2];
		array4[0] = BiomeTag.Garlic;
		t4.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(array4);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"garlic_plant"
		});
		this.clone("rocklands_plant", "flower");
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Rocklands
		});
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"rocklands_plant"
		});
		this.t.affected_by_cold_temperature = false;
		this.clone("celestial_plant", "flower");
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Celestial
		});
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"celestial_plant"
		});
		this.t.affected_by_cold_temperature = false;
		this.clone("singularity_plant", "flower");
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Singularity
		});
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"singularity_plant"
		});
		this.t.affected_by_cold_temperature = false;
		this.clone("clover_plant", "flower");
		BuildingAsset t5 = this.t;
		BiomeTag[] array5 = new BiomeTag[2];
		array5[0] = BiomeTag.Clover;
		t5.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(array5);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"clover_plant"
		});
		this.clone("paradox_plant", "flower");
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Paradox
		});
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"paradox_plant"
		});
		this.t.affected_by_cold_temperature = false;
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x00059710 File Offset: 0x00057910
	private void addMinerals()
	{
		this.add(new BuildingAsset
		{
			id = "$resource$",
			fundament = new BuildingFundament(1, 1, 1, 0),
			has_resources_to_collect = true,
			has_ruins_graphics = false,
			destroy_on_liquid = true,
			random_flip = true,
			ignored_by_cities = false,
			burnable = false,
			affected_by_acid = true,
			affected_by_lava = true,
			group = "nature",
			kingdom = "nature",
			main_path = "buildings/minerals/"
		});
		this.t.setAtlasID("buildings", null);
		this.t.remove_ruins = false;
		this.t.can_be_placed_on_blocks = false;
		this.t.base_stats["health"] = 10f;
		this.clone("$mineral$", "$resource$");
		this.t.type = "type_mineral";
		this.t.has_ruin_state = false;
		this.t.remove_ruins = true;
		this.t.ignore_buildings = false;
		this.t.ignored_by_cities = true;
		this.t.ignore_same_building_id = true;
		this.t.building_type = BuildingType.Building_Mineral;
		this.t.vegetation_random_chance = 0.1f;
		this.t.limit_per_zone = 1;
		this.t.setShadow(0.19f, 0.03f, 0.09f);
		this.t.has_sprites_main = true;
		this.t.nutrition_restore = 30;
		this.clone("mineral_bones", "$mineral$");
		this.t.addResource("bones", 3, false);
		this.t.addResource("stone", 1, false);
		this.clone("mineral_adamantine", "$mineral$");
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.15f;
		this.t.sparkle_effect = true;
		this.t.nutrition_restore = 60;
		this.t.addResource("adamantine", 1, false);
		this.t.addResource("stone", 1, false);
		this.clone("mineral_mythril", "$mineral$");
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.1f;
		this.t.sparkle_effect = true;
		this.t.nutrition_restore = 40;
		this.t.addResource("mythril", 1, false);
		this.t.addResource("stone", 1, false);
		this.clone("mineral_gems", "$mineral$");
		this.t.sparkle_effect = true;
		this.t.nutrition_restore = 70;
		this.t.addResource("gems", 1, false);
		this.t.addResource("stone", 1, false);
		this.clone("mineral_stone", "$mineral$");
		this.t.addResource("stone", 3, false);
		this.clone("mineral_metals", "$mineral$");
		this.t.sparkle_effect = true;
		this.t.addResource("common_metals", 2, false);
		this.t.addResource("stone", 1, false);
		this.clone("mineral_gold", "$mineral$");
		this.t.sparkle_effect = true;
		this.t.addResource("gold", 20, false);
		this.t.addResource("stone", 20, false);
		this.clone("mineral_silver", "$mineral$");
		this.t.sparkle_effect = true;
		this.t.addResource("silver", 1, false);
		this.t.addResource("stone", 1, false);
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x00059AD8 File Offset: 0x00057CD8
	private void addPoop()
	{
		this.add(new BuildingAsset
		{
			id = "poop",
			building_type = BuildingType.Building_Poops,
			fundament = new BuildingFundament(0, 0, 0, 0),
			has_ruins_graphics = false,
			has_ruin_state = false,
			destroy_on_liquid = true,
			random_flip = true,
			ignored_by_cities = true,
			burnable = true,
			affected_by_acid = true,
			affected_by_lava = true,
			flora = true,
			flora_size = FloraSize.Tiny,
			group = "nature",
			kingdom = "nature",
			main_path = "buildings/nature/",
			removed_by_sponge = true
		});
		this.t.scale_base = new Vector3(0.1f, 0.1f, 0.1f);
		this.t.type = "type_poop";
		this.t.remove_ruins = true;
		this.t.addResource("fertilizer", 1, false);
		this.t.base_stats["health"] = 10f;
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x00059BE8 File Offset: 0x00057DE8
	private void addGrownResources()
	{
		this.clone("fruit_bush", "$resource$");
		this.t.main_path = "buildings/nature/";
		this.t.has_ruin_state = false;
		this.t.can_be_living_plant = true;
		this.t.building_type = BuildingType.Building_Fruits;
		this.t.is_vegetation = true;
		this.t.has_special_animation_state = true;
		this.t.addResource("berries", 3, false);
		this.t.nutrition_restore = 30;
		this.t.type = "type_fruits";
		this.t.burnable = true;
		this.t.flora = true;
		this.t.can_be_damaged_by_tornado = true;
		this.t.ignored_by_cities = true;
		this.t.vegetation_random_chance = 0.2f;
		this.t.limit_per_zone = 1;
		this.t.biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Lemon,
			BiomeTag.Green,
			BiomeTag.Mushroom,
			BiomeTag.Enchanted,
			BiomeTag.Jungle,
			BiomeTag.Savanna,
			BiomeTag.Maple,
			BiomeTag.Birch,
			BiomeTag.Flower,
			BiomeTag.Garlic,
			BiomeTag.Clover
		});
		this.t.setSpread(FloraType.Plant, 10, 1f);
		this.t.spread_ids = AssetLibrary<BuildingAsset>.a<string>(new string[]
		{
			"fruit_bush"
		});
		this.t.material = "tree";
		this.t.setAtlasID("buildings_trees", "buildings");
		this.t.setShadow(0.19f, 0.03f, 0.09f);
		this.t.has_sprites_main = true;
		this.t.has_sprites_special = true;
		this.t.gatherable = true;
		this.t.has_resources_grown_to_collect = true;
		this.t.has_resources_grown_to_collect_on_spawn = true;
		this.add(new BuildingAsset
		{
			id = "wheat",
			fundament = new BuildingFundament(0, 0, 0, 0),
			type = "type_crops",
			building_type = BuildingType.Building_Wheat,
			destroy_on_liquid = true,
			random_flip = true,
			ignored_by_cities = true,
			burnable = true,
			affected_by_acid = true,
			affected_by_lava = true,
			flora = true,
			can_be_damaged_by_tornado = true,
			group = "nature",
			kingdom = "nature",
			shadow = false,
			biome_tags_growth = AssetLibrary<BuildingAsset>.h<BiomeTag>(new BiomeTag[]
			{
				BiomeTag.Field
			}),
			has_ruins_graphics = false,
			material = "tree",
			wheat = true,
			growth_time = 50f,
			main_path = "buildings/nature/",
			can_be_living_plant = true,
			can_be_grown = true
		});
		this.t.setAtlasID("buildings_trees", "buildings");
		this.t.nutrition_restore = 20;
		this.t.has_ruin_state = false;
		this.t.addResource("wheat", 1, false);
		this.t.base_stats["health"] = 10f;
		this.t.has_sprites_main = true;
		this.t.get_map_icon_color = delegate(Building pBuilding)
		{
			int tLevel = pBuilding.animData_index;
			return Toolbox.colors_wheat[tLevel];
		};
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x00059F10 File Offset: 0x00058110
	private void addGeneralCityBuildings()
	{
		this.add(new BuildingAsset
		{
			id = "$building$",
			fundament = new BuildingFundament(3, 3, 2, 0),
			burnable = true,
			destroy_on_liquid = true,
			build_road_to = true,
			affected_by_acid = true,
			affected_by_lava = true,
			can_be_damaged_by_tornado = true,
			only_build_tiles = true,
			check_for_close_building = true,
			sound_hit = "event:/SFX/HIT/HitGeneric",
			main_path = "buildings/nature/",
			can_be_demolished = true
		});
		this.t.base_stats["health"] = 1500f;
		this.t.setShadow(0.5f, 0.35f, 0.53f);
		this.clone("$city_building$", "$building$");
		this.t.building_type = BuildingType.Building_Civ;
		this.t.has_sprite_construction = true;
		this.t.main_path = "buildings/civ_general/";
		this.t.construction_progress_needed = 50;
		this.t.city_building = true;
		this.t.can_be_abandoned = true;
		this.t.build_place_batch = true;
		this.t.setShadow(0.5f, 0.37f, 0.28f);
		this.t.has_sprites_main = true;
		this.t.has_sprites_ruin = true;
		this.t.check_for_adaptation_tags = true;
		this.clone("$city_colored_building$", "$city_building$");
		this.t.has_kingdom_color = true;
		this.clone("bonfire", "$city_building$");
		this.t.burnable = false;
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.8f;
		this.t.can_be_abandoned = false;
		this.t.priority = 120;
		this.t.type = "type_bonfire";
		this.t.fundament = new BuildingFundament(2, 2, 2, 0);
		this.t.construction_progress_needed = 30;
		this.t.cost = new ConstructionCost(0, 0, 0, 0);
		this.t.smoke = true;
		this.t.smoke_interval = 2.5f;
		this.t.smoke_offset = new Vector2Int(2, 3);
		this.t.can_be_living_house = false;
		this.t.build_place_batch = false;
		this.t.build_prefer_replace_house = true;
		this.t.check_for_close_building = false;
		this.t.max_houses = 3;
		this.t.produce_biome_food = true;
		this.t.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleBonfire";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingWood";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingGeneric";
		this.t.setShadow(0.19f, 0.5f, 0.27f);
		this.t.has_sprites_main = true;
		this.t.has_sprites_ruin = true;
		this.t.check_for_adaptation_tags = false;
		this.clone("well", "$city_building$");
		this.t.priority = 21;
		this.t.type = "type_well";
		this.t.fundament = new BuildingFundament(2, 2, 1, 0);
		this.t.cost = new ConstructionCost(0, 20, 1, 5);
		this.t.construction_progress_needed = 200;
		this.t.burnable = false;
		this.t.max_houses = 3;
		this.t.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleWell";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingStone";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingStone";
		this.t.has_sprites_main = true;
		this.t.has_sprites_ruin = true;
		this.clone("training_dummy", "$city_building$");
		this.t.priority = 23;
		this.t.type = "type_training_dummies";
		this.t.fundament = new BuildingFundament(0, 0, 0, 0);
		this.t.cost = new ConstructionCost(5, 0, 0, 5);
		this.t.construction_progress_needed = 100;
		this.t.burnable = true;
		this.t.max_houses = 3;
		this.t.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleBarracks";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingWood";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingWood";
		this.t.has_sprite_construction = true;
		this.t.has_sprites_main = true;
		this.t.has_sprites_ruin = true;
		this.t.setShadow(0.6f, 0.27f, 0.23f);
		this.clone("stockpile", "$city_building$");
		this.t.priority = 100;
		this.t.is_stockpile = true;
		this.t.shadow = false;
		this.t.stockpile_top_left_offset = new Vector2(-2f, 3.5f);
		this.t.stockpile_center_offset = new Vector2(0f, 1.5f);
		this.t.storage = true;
		this.t.type = "type_stockpile";
		this.t.fundament = new BuildingFundament(3, 3, 5, 0);
		this.t.cost = new ConstructionCost(0, 0, 0, 0);
		this.t.bonus_z = -5f;
		this.t.construction_progress_needed = 10;
		this.t.burnable = true;
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingStone";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingStone";
		this.t.has_sprites_main = true;
		this.t.has_sprites_ruin = true;
		this.clone("stockpile_fireproof", "stockpile");
		this.t.burnable = false;
		this.clone("stockpile_acidproof", "stockpile");
		this.t.affected_by_acid = false;
		this.clone("statue", "$city_building$");
		this.t.priority = 27;
		this.t.type = "type_statue";
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.cost = new ConstructionCost(0, 5, 0, 25);
		this.t.burnable = false;
		this.t.max_houses = 3;
		this.t.setShadow(0.5f, 0.17f, 0.26f);
		this.t.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleStatue";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingStone";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingStone";
		this.t.has_sprites_main = true;
		this.t.has_sprites_ruin = true;
		this.clone("mine", "$city_building$");
		this.t.priority = 50;
		this.t.type = "type_mine";
		this.t.fundament = new BuildingFundament(2, 2, 2, 0);
		this.t.cost = new ConstructionCost(0, 0, 0, 15);
		this.t.construction_progress_needed = 300;
		this.t.burnable = false;
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.3f;
		this.t.build_place_single = true;
		this.t.build_place_batch = false;
		this.t.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleMine";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingStone";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingStone";
		this.t.has_sprites_main = true;
		this.t.has_sprites_ruin = true;
		this.clone("$windmill_base$", "$city_colored_building$");
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.3f;
		this.t.priority = 23;
		this.t.burnable = false;
		this.t.storage = true;
		this.t.storage_only_food = true;
		this.t.type = "type_windmill";
		this.t.needs_farms_ground = true;
		this.t.build_place_center = true;
		this.t.build_place_single = true;
		this.t.build_place_batch = false;
		this.t.setShadow(0.5f, 0.23f, 0.27f);
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleWindmill";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingWood";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingWood";
		this.clone("$windmill_0$", "$windmill_base$");
		this.t.cost = new ConstructionCost(5, 0, 0, 5);
		this.t.can_be_upgraded = true;
		this.t.sound_hit = "event:/SFX/HIT/HitWood";
		this.clone("$windmill_1$", "$windmill_base$");
		this.t.cost = new ConstructionCost(0, 5, 5, 30);
		this.t.can_be_upgraded = false;
		this.t.has_sprite_construction = false;
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x0005A844 File Offset: 0x00058A44
	private void addNatureBuildings()
	{
		this.clone("golden_brain", "$building$");
		this.t.building_type = BuildingType.Building_Nature;
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.3f;
		this.t.base_stats["health"] = 10000f;
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.group = "golden_brain";
		this.t.kingdom = "golden_brain";
		this.t.can_be_placed_on_liquid = false;
		this.t.ignore_buildings = true;
		this.t.check_for_close_building = false;
		this.t.can_be_living_house = true;
		this.t.burnable = false;
		this.t.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleGoldenBrain";
		this.t.setShadow(0.56f, 0.23f, 0.28f);
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingStone";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingStone";
		this.t.has_sprites_spawn = true;
		this.t.has_sprites_main = true;
		this.t.has_sprites_ruin = true;
		this.clone("$waypoint$", "$building$");
		this.t.waypoint = true;
		this.t.building_type = BuildingType.Building_Nature;
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.3f;
		this.t.base_stats["health"] = 10000f;
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.group = "nature";
		this.t.can_be_placed_on_liquid = false;
		this.t.ignore_buildings = true;
		this.t.check_for_close_building = false;
		this.t.can_be_living_house = true;
		this.t.burnable = false;
		this.t.limit_global = 1;
		this.t.setShadow(0.56f, 0.23f, 0.28f);
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingStone";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingStone";
		this.t.has_sprites_spawn = true;
		this.t.has_sprites_main = true;
		this.t.has_sprites_ruin = true;
		this.clone("waypoint_alien_mold", "$waypoint$");
		this.t.kingdom = "alien_mold";
		this.clone("waypoint_computer", "$waypoint$");
		this.t.kingdom = "computer";
		this.clone("waypoint_golden_egg", "$waypoint$");
		this.t.kingdom = "golden_egg";
		this.clone("waypoint_harp", "$waypoint$");
		this.t.kingdom = "harp";
		this.clone("corrupted_brain", "$building$");
		this.t.building_type = BuildingType.Building_Nature;
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.5f;
		this.t.base_stats["health"] = 10000f;
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.group = "corrupted_brain";
		this.t.kingdom = "corrupted_brain";
		this.t.can_be_placed_on_liquid = false;
		this.t.ignore_buildings = true;
		this.t.check_for_close_building = false;
		this.t.can_be_living_house = true;
		this.t.burnable = false;
		this.t.tower = true;
		this.t.tower_attack_buildings = false;
		this.t.tower_projectile = "madness_ball";
		this.t.tower_projectile_offset = 6f;
		this.t.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleCorruptedBrain";
		this.t.sound_hit = "event:/SFX/HIT/HitFlesh";
		this.t.setShadow(0.44f, 0.38f, 0.37f);
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingFlesh";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingFlesh";
		this.t.has_sprites_spawn = true;
		this.t.has_sprites_main = true;
		this.t.has_sprites_ruin = true;
		this.clone("monolith", "$building$");
		this.t.building_type = BuildingType.Building_Nature;
		this.t.draw_light_area = true;
		this.t.ignored_by_cities = false;
		this.t.draw_light_size = 1f;
		this.t.base_stats["health"] = 50000f;
		this.t.fundament = new BuildingFundament(2, 2, 3, 0);
		this.t.group = "nature";
		this.t.kingdom = "nature";
		this.t.can_be_placed_on_liquid = false;
		this.t.ignore_buildings = true;
		this.t.check_for_close_building = false;
		this.t.can_be_living_house = true;
		this.t.burnable = false;
		this.t.setShadow(0.56f, 0.23f, 0.28f);
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingStone";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingStone";
		this.t.has_sprites_spawn = true;
		this.t.has_sprites_main = true;
		this.t.has_sprites_ruin = true;
		this.t.has_sprites_special = true;
		this.clone("beehive", "$building$");
		this.t.building_type = BuildingType.Building_Hives;
		this.t.base_stats["health"] = 100f;
		this.t.fundament = new BuildingFundament(1, 0, 1, 0);
		this.t.group = "nature";
		this.t.kingdom = "neutral_animals";
		this.t.can_be_placed_on_liquid = false;
		this.t.ignore_buildings = true;
		this.t.check_for_close_building = false;
		this.t.can_be_living_house = true;
		this.t.burnable = true;
		this.t.housing_slots = 5;
		this.t.beehive = true;
		this.t.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleBeehive";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingGeneric";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingGeneric";
		this.t.has_sprites_main = true;
		this.t.has_sprites_ruin = true;
		this.t.has_special_animation_state = true;
		this.t.addResource("honey", 1, false);
		this.t.type = "type_hive";
		this.t.gatherable = true;
		this.t.has_resources_grown_to_collect = true;
		this.t.has_resources_grown_to_collect_on_spawn = false;
		this.clone("$drop_spreader$", "$building$");
		this.t.building_type = BuildingType.Building_Nature;
		this.t.group = "nature";
		this.t.kingdom = "nature";
		this.t.burnable = false;
		this.t.fundament = new BuildingFundament(2, 2, 2, 0);
		this.t.can_be_placed_on_blocks = true;
		this.t.destroy_on_liquid = false;
		this.t.ignored_by_cities = false;
		this.t.affected_by_lava = false;
		this.t.can_be_placed_on_liquid = true;
		this.t.has_sprites_spawn = true;
		this.t.has_sprites_main = true;
		this.t.has_sprites_ruin = true;
		this.t.has_sprites_main_disabled = true;
		this.t.can_be_damaged_by_tornado = false;
		this.t.ignore_buildings = true;
		this.t.check_for_close_building = false;
		this.t.can_be_living_house = false;
		this.t.spawn_drops = true;
		this.clone("volcano", "$drop_spreader$");
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.8f;
		this.t.transform_tiles_to_tile_type = "lava3";
		this.t.smoke = true;
		this.t.smoke_interval = 1.5f;
		this.t.smoke_offset = new Vector2Int(2, 2);
		this.t.spawn_drop_id = "lava";
		this.t.spawn_drop_start_height = 1.8f;
		this.t.spawn_drop_min_height = 5f;
		this.t.spawn_drop_max_height = 30f;
		this.t.spawn_drop_interval = 0.1f;
		this.t.spawn_drop_min_radius = 2f;
		this.t.spawn_drop_max_radius = 8f;
		this.t.step_action = delegate(Actor pActor, Building pBuilding)
		{
			if (!pActor.asset.die_in_lava)
			{
				return;
			}
			if (pActor.isUnderDamageCooldown())
			{
				return;
			}
			if (pBuilding.isRuin())
			{
				return;
			}
			pActor.getHit(200f, true, AttackType.Fire, null, true, false, true);
			if (!pActor.isAlive())
			{
				CursedSacrifice.checkGoodForSacrifice(pActor);
				pActor.skipUpdates();
			}
		};
		this.t.setShadow(0.4f, 0f, 0.7f);
		this.t.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleVolcano";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingStone";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingStone";
		this.clone("geyser", "$drop_spreader$");
		this.t.smoke = true;
		this.t.smoke_interval = 2.5f;
		this.t.spawn_drop_id = "rain";
		this.t.spawn_drop_start_height = 2.5f;
		this.t.spawn_drop_min_height = 10f;
		this.t.spawn_drop_max_height = 40f;
		this.t.spawn_drop_min_radius = 2f;
		this.t.spawn_drop_max_radius = 17f;
		this.t.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleGeyser";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingStone";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingStone";
		this.clone("geyser_acid", "$drop_spreader$");
		this.t.smoke = true;
		this.t.smoke_interval = 3.5f;
		this.t.spawn_drop_id = "acid";
		this.t.affected_by_acid = false;
		this.t.spawn_drop_start_height = 2f;
		this.t.spawn_drop_min_height = 5f;
		this.t.spawn_drop_max_height = 36f;
		this.t.spawn_drop_min_radius = 2f;
		this.t.spawn_drop_max_radius = 15f;
		this.t.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleAcidGeyser";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingStone";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingStone";
	}

	// Token: 0x0600060E RID: 1550 RVA: 0x0005B314 File Offset: 0x00059514
	private void addMobBuildings()
	{
		this.clone("flame_tower", "$building$");
		this.t.building_type = BuildingType.Building_Mob;
		this.t.main_path = "buildings/mobs/";
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.5f;
		this.t.draw_light_area_offset_y = 8f;
		this.t.base_stats["health"] = 1000f;
		this.t.fundament = new BuildingFundament(2, 2, 3, 0);
		this.t.group = "demon";
		this.t.kingdom = "demon";
		this.t.can_be_placed_on_liquid = false;
		this.t.ignore_buildings = true;
		this.t.check_for_close_building = false;
		this.t.can_be_living_house = true;
		this.t.burnable = false;
		this.t.spawn_units = true;
		this.t.spawn_units_asset = "demon";
		this.t.housing_slots = 5;
		this.t.tower = true;
		this.t.tower_attack_buildings = true;
		this.t.tower_projectile = "fireball";
		this.t.tower_projectile_offset = 10f;
		this.t.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleFlameTower";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingStone";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingStone";
		this.t.has_sprites_spawn = true;
		this.t.has_sprites_main = true;
		this.t.has_sprites_ruin = true;
		this.clone("ice_tower", "$building$");
		this.t.building_type = BuildingType.Building_Mob;
		this.t.main_path = "buildings/mobs/";
		this.t.base_stats["health"] = 1000f;
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.group = "cold_one";
		this.t.kingdom = "cold_one";
		this.t.can_be_placed_on_liquid = false;
		this.t.ignore_buildings = true;
		this.t.check_for_close_building = false;
		this.t.can_be_living_house = true;
		this.t.burnable = false;
		this.t.ice_tower = true;
		this.t.spawn_units = true;
		this.t.spawn_units_asset = "cold_one";
		this.t.housing_slots = 5;
		this.t.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleIceTower";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingStone";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingStone";
		this.t.has_sprites_spawn = true;
		this.t.has_sprites_main = true;
		this.t.has_sprites_ruin = true;
		this.clone("angle_tower", "$building$");
		this.t.building_type = BuildingType.Building_Mob;
		this.t.main_path = "buildings/mobs/";
		this.t.base_stats["health"] = 1000f;
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.group = "angle";
		this.t.kingdom = "angle";
		this.t.housing_slots = 5;
		this.t.can_be_placed_on_liquid = false;
		this.t.ignore_buildings = true;
		this.t.check_for_close_building = false;
		this.t.can_be_living_house = true;
		this.t.burnable = false;
		this.t.spawn_units = true;
		this.t.spawn_units_asset = "angle";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingStone";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingStone";
		this.t.has_sprites_spawn = true;
		this.t.has_sprites_main = true;
		this.t.has_sprites_ruin = true;
	}

	// Token: 0x0600060F RID: 1551 RVA: 0x0005B720 File Offset: 0x00059920
	private void addCreeps()
	{
		this.clone("$building_creep$", "$building$");
		this.t.main_path = "buildings/creeps/";
		this.t.building_type = BuildingType.Building_Creep;
		this.t.has_sprites_spawn = true;
		this.t.has_sprites_main = true;
		this.t.has_sprites_ruin = true;
		this.clone("tumor", "$building_creep$");
		this.t.material = "jelly";
		this.t.setAtlasID("buildings_wobbly", "buildings");
		this.t.transform_tiles_to_top_tiles = "tumor_low";
		this.t.fundament = new BuildingFundament(1, 1, 1, 0);
		this.t.group = "tumor";
		this.t.kingdom = "tumor";
		this.t.can_be_placed_on_blocks = false;
		this.t.can_be_placed_on_liquid = false;
		this.t.ignore_buildings = true;
		this.t.check_for_close_building = false;
		this.t.can_be_living_house = false;
		this.t.spawn_units = true;
		this.t.spawn_units_asset = "tumor_monster_animal";
		this.t.housing_slots = 5;
		this.setGrowBiomeAround("biome_tumor", 5, 2, 0.1f, CreepWorkerMovementType.Direction);
		this.t.grow_creep_direction_random_position = true;
		this.t.grow_creep_flash = true;
		this.t.grow_creep_redraw_tile = true;
		this.t.setShadow(0.2f, 0.08f, 0.66f);
		this.t.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleTumor";
		this.t.sound_hit = "event:/SFX/HIT/HitFlesh";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingFlesh";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingFlesh";
		this.clone("biomass", "tumor");
		this.t.group = "biomass";
		this.t.kingdom = "biomass";
		this.t.spawn_units_asset = "bioblob";
		this.t.housing_slots = 5;
		this.t.transform_tiles_to_top_tiles = "biomass_low";
		this.setGrowBiomeAround("biome_biomass", 10, 4, 0.7f, CreepWorkerMovementType.RandomNeighbourAll);
		this.t.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleBiomass";
		this.t.sound_hit = "event:/SFX/HIT/HitFlesh";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingFlesh";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingFlesh";
		this.clone("super_pumpkin", "tumor");
		this.t.group = "super_pumpkin";
		this.t.kingdom = "super_pumpkin";
		this.t.spawn_units_asset = "lil_pumpkin";
		this.t.housing_slots = 5;
		this.t.transform_tiles_to_top_tiles = "pumpkin_low";
		this.setGrowBiomeAround("biome_pumpkin", 10, 3, 0.2f, CreepWorkerMovementType.Direction);
		this.t.grow_creep_direction_random_position = true;
		this.t.grow_creep_random_new_direction = true;
		this.t.grow_creep_steps_before_new_direction = 20;
		this.t.grow_creep_flash = true;
		this.t.grow_creep_redraw_tile = true;
		this.t.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleSuperPumpkin";
		this.t.sound_hit = "event:/SFX/HIT/HitFlesh";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingFlesh";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingFlesh";
		this.clone("cybercore", "tumor");
		this.t.group = "assimilators";
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.2f;
		this.t.draw_light_area_offset_y = 2f;
		this.t.kingdom = "assimilators";
		this.t.spawn_units_asset = "assimilator";
		this.t.housing_slots = 5;
		this.t.transform_tiles_to_top_tiles = "cybertile_low";
		this.setGrowBiomeAround("biome_cybertile", 20, 6, 2f, CreepWorkerMovementType.Direction);
		this.t.grow_creep_steps_before_new_direction = 7;
		this.t.grow_creep_direction_random_position = false;
		this.t.grow_creep_random_new_direction = true;
		this.t.damaged_by_rain = true;
		this.t.burnable = false;
		this.t.material = "building";
		this.t.setAtlasID("buildings", null);
		this.t.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleCybercore";
		this.t.sound_hit = "event:/SFX/HIT/HitMetal";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingRobotic";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingRobotic";
	}

	// Token: 0x06000610 RID: 1552 RVA: 0x0005BBC8 File Offset: 0x00059DC8
	private void addHumans()
	{
		this.clone("$building_civ_human$", "$city_colored_building$");
		this.t.main_path = "buildings/civ_main/human/";
		this.t.group = "human";
		this.t.civ_kingdom = "human";
		this.clone("fishing_docks_human", "$building_civ_human$");
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.2f;
		this.t.draw_light_area_offset_y = 2f;
		this.t.sprite_path = "buildings/civ_general/fishing_dock";
		this.t.priority = 20;
		this.t.type = "type_docks";
		this.t.fundament = new BuildingFundament(2, 2, 4, 0);
		this.t.cost = new ConstructionCost(10, 0, 0, 0);
		this.t.burnable = false;
		this.t.docks = true;
		this.t.can_be_placed_on_liquid = true;
		this.t.destroy_on_liquid = false;
		this.t.build_road_to = false;
		this.t.only_build_tiles = false;
		this.t.auto_remove_ruin = true;
		this.t.max_houses = 1;
		this.t.can_be_upgraded = true;
		this.t.upgrade_level = 1;
		this.t.upgrade_to = "docks_human";
		this.t.boat_types = new string[]
		{
			"boat_type_fishing"
		};
		this.t.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleFishingDocks";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingWood";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingWood";
		this.t.setShadow(0.5f, 0.55f, 0.63f);
		this.clone("watch_tower_human", "$building_civ_human$");
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.5f;
		this.t.base_stats["health"] = 3000f;
		this.t.base_stats["targets"] = 1f;
		this.t.base_stats["area_of_effect"] = 1f;
		this.t.base_stats["damage"] = 50f;
		this.t.base_stats["knockback"] = 1f;
		this.t.priority = 22;
		this.t.type = "type_watch_tower";
		this.t.fundament = new BuildingFundament(1, 1, 1, 0);
		this.t.cost = new ConstructionCost(0, 20, 1, 5);
		this.t.burnable = false;
		this.t.tower = true;
		this.t.tower_attack_buildings = true;
		this.t.tower_projectile = "arrow";
		this.t.tower_projectile_offset = 4f;
		this.t.tower_projectile_amount = 6;
		this.t.build_place_borders = true;
		this.t.build_place_batch = false;
		this.t.build_place_single = true;
		this.t.setShadow(0.5f, 0.23f, 0.27f);
		this.t.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleWatchTower";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingStone";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingStone";
		this.clone("docks_human", "fishing_docks_human");
		this.t.sprite_path = string.Empty;
		this.t.cost = new ConstructionCost(10, 6, 0, 0);
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.5f;
		this.t.draw_light_area_offset_y = 8f;
		this.t.can_be_upgraded = false;
		this.t.upgraded_from = "fishing_docks_human";
		this.t.boat_types = new string[]
		{
			"boat_type_fishing",
			"boat_type_trading",
			"boat_type_transport"
		};
		this.t.setShadow(0.5f, 0.55f, 0.63f);
		this.t.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleDocks";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingStone";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingStone";
		this.t.has_sprites_main_disabled = true;
		this.clone("barracks_human", "$building_civ_human$");
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.5f;
		this.t.priority = 22;
		this.t.burnable = false;
		this.t.type = "type_barracks";
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.cost = new ConstructionCost(0, 5, 2, 15);
		this.t.setShadow(0.56f, 0.41f, 0.43f);
		this.t.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleBarracks";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingStone";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingStone";
		this.clone("temple_human", "$building_civ_human$");
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.3f;
		this.t.draw_light_area_offset_y = 3f;
		this.t.priority = 26;
		this.t.type = "type_temple";
		this.t.fundament = new BuildingFundament(2, 2, 3, 0);
		this.t.cost = new ConstructionCost(0, 10, 2, 30);
		this.t.burnable = false;
		this.t.group = "human";
		this.t.max_houses = 2;
		this.t.setShadow(0.56f, 0.41f, 0.43f);
		this.t.sound_idle = "event:/SFX/BUILDINGS_IDLE/IdleTemple";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingStone";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingStone";
		this.clone("library_human", "$building_civ_human$");
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.3f;
		this.t.draw_light_area_offset_y = 3f;
		this.t.priority = 26;
		this.t.type = "type_library";
		this.t.fundament = new BuildingFundament(2, 2, 3, 0);
		this.t.cost = new ConstructionCost(0, 10, 2, 30);
		this.t.burnable = false;
		this.t.group = "human";
		this.t.book_slots = 5;
		this.t.setShadow(0.56f, 0.41f, 0.43f);
		this.clone("market_human", "$building_civ_human$");
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.3f;
		this.t.draw_light_area_offset_y = 3f;
		this.t.priority = 26;
		this.t.type = "type_market";
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.cost = new ConstructionCost(10, 5, 2, 100);
		this.t.burnable = true;
		this.t.group = "human";
		this.t.setShadow(0.56f, 0.41f, 0.43f);
		this.clone("windmill_human_0", "$windmill_0$");
		this.t.group = "human";
		this.t.main_path = "buildings/civ_main/human/";
		this.t.upgrade_to = "windmill_human_1";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingStone";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingStone";
		this.t.civ_kingdom = "human";
		this.clone("windmill_human_1", "$windmill_1$");
		this.t.group = "human";
		this.t.main_path = "buildings/civ_main/human/";
		this.t.upgraded_from = "windmill_human_0";
		this.t.civ_kingdom = "human";
		this.clone("tent_human", "$building_civ_human$");
		this.t.type = "type_house";
		this.t.cost = new ConstructionCost(1, 0, 0, 0);
		this.t.fundament = new BuildingFundament(2, 2, 2, 0);
		this.t.can_be_upgraded = true;
		this.t.setHousingSlots(3);
		this.t.loot_generation = 1;
		this.t.housing_happiness = 5;
		this.t.burnable = true;
		this.t.upgrade_to = "house_human_0";
		this.t.base_stats["health"] = 50f;
		this.t.build_place_batch = true;
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingGeneric";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingGeneric";
		this.clone("house_human_0", "$building_civ_human$");
		this.t.draw_light_area = true;
		this.t.draw_light_size = 0.2f;
		this.t.type = "type_house";
		this.t.cost = new ConstructionCost(5, 0, 0, 0);
		this.t.setHousingSlots(3);
		this.t.loot_generation = 1;
		this.t.housing_happiness = 6;
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.can_be_upgraded = true;
		this.t.burnable = true;
		this.t.upgrade_to = "house_human_1";
		this.t.upgraded_from = "tent_human";
		this.t.base_stats["health"] = 100f;
		this.t.has_sprite_construction = false;
		this.t.group = "human";
		this.t.sound_hit = "event:/SFX/HIT/HitWood";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingWood";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingWood";
		this.clone("house_human_1", "house_human_0");
		this.t.cost = new ConstructionCost(4, 0, 0, 0);
		this.t.setHousingSlots(4);
		this.t.loot_generation = 2;
		this.t.housing_happiness = 7;
		this.t.upgrade_level = 1;
		this.t.upgrade_to = "house_human_2";
		this.t.upgraded_from = "house_human_0";
		this.t.base_stats["health"] = 150f;
		this.t.group = "human";
		this.t.sound_hit = "event:/SFX/HIT/HitWood";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingWood";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingWood";
		this.clone("house_human_2", "house_human_1");
		this.t.cost = new ConstructionCost(0, 5, 0, 0);
		this.t.upgrade_level = 2;
		this.t.loot_generation = 3;
		this.t.burnable = false;
		this.t.upgrade_to = "house_human_3";
		this.t.upgraded_from = "house_human_1";
		this.t.base_stats["health"] = 200f;
		this.t.group = "human";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingStone";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingStone";
		this.clone("house_human_3", "house_human_2");
		this.t.fundament = new BuildingFundament(2, 2, 2, 0);
		this.t.cost = new ConstructionCost(0, 10, 0, 0);
		this.t.setHousingSlots(5);
		this.t.loot_generation = 4;
		this.t.housing_happiness = 9;
		this.t.upgrade_level = 3;
		this.t.upgrade_to = "house_human_4";
		this.t.upgraded_from = "house_human_2";
		this.t.base_stats["health"] = 250f;
		this.t.group = "human";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingStone";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingStone";
		this.clone("house_human_4", "house_human_3");
		this.t.fundament = new BuildingFundament(3, 3, 2, 0);
		this.t.cost = new ConstructionCost(0, 15, 0, 0);
		this.t.setHousingSlots(6);
		this.t.loot_generation = 5;
		this.t.housing_happiness = 10;
		this.t.upgrade_level = 4;
		this.t.upgrade_to = "house_human_5";
		this.t.upgraded_from = "house_human_3";
		this.t.base_stats["health"] = 350f;
		this.t.group = "human";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingStone";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingStone";
		this.clone("house_human_5", "house_human_4");
		this.t.cost = new ConstructionCost(0, 20, 2, 10);
		this.t.setHousingSlots(7);
		this.t.loot_generation = 6;
		this.t.housing_happiness = 11;
		this.t.upgrade_level = 5;
		this.t.can_be_upgraded = false;
		this.t.upgrade_to = string.Empty;
		this.t.upgraded_from = "house_human_4";
		this.t.base_stats["health"] = 400f;
		this.t.group = "human";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingStone";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingStone";
		this.clone("hall_human_0", "house_human_0");
		this.t.sound_hit = "event:/SFX/HIT/HitWood";
		this.t.priority = 100;
		this.t.storage = true;
		this.t.type = "type_hall";
		this.t.cost = new ConstructionCost(10, 5, 0, 0);
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.can_be_upgraded = true;
		this.t.base_stats["health"] = 200f;
		this.t.burnable = true;
		this.t.setHousingSlots(5);
		this.t.housing_happiness = 10;
		this.t.loot_generation = 3;
		this.t.upgrade_to = "hall_human_1";
		this.t.ignore_other_buildings_for_upgrade = true;
		this.t.group = "human";
		this.t.build_place_batch = true;
		this.t.max_houses = 2;
		this.t.produce_biome_food = true;
		this.t.setShadow(0.56f, 0.41f, 0.43f);
		this.t.draw_light_size = 0.3f;
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingWood";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingWood";
		this.t.book_slots = 3;
		this.t.has_sprite_construction = true;
		this.clone("hall_human_1", "hall_human_0");
		this.t.cost = new ConstructionCost(0, 10, 1, 20);
		this.t.setHousingSlots(8);
		this.t.loot_generation = 5;
		this.t.housing_happiness = 15;
		this.t.upgrade_level = 1;
		this.t.burnable = false;
		this.t.upgrade_to = "hall_human_2";
		this.t.upgraded_from = "hall_human_0";
		this.t.base_stats["health"] = 400f;
		this.t.group = "human";
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingStone";
		this.t.draw_light_size = 0.4f;
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingStone";
		this.t.has_sprite_construction = false;
		this.clone("hall_human_2", "hall_human_1");
		this.t.cost = new ConstructionCost(0, 15, 1, 100);
		this.t.setHousingSlots(12);
		this.t.loot_generation = 10;
		this.t.housing_happiness = 20;
		this.t.upgrade_level = 2;
		this.t.can_be_upgraded = false;
		this.t.upgraded_from = "hall_human_1";
		this.t.upgrade_to = string.Empty;
		this.t.base_stats["health"] = 600f;
		this.t.group = "human";
		this.t.draw_light_size = 0.5f;
		this.t.sound_built = "event:/SFX/BUILDINGS/SpawnBuildingStone";
		this.t.sound_destroyed = "event:/SFX/BUILDINGS/DestroyBuildingStone";
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x0005CD88 File Offset: 0x0005AF88
	private void addOrcs()
	{
		this.clone("$building_civ_orc$", "$city_colored_building$");
		this.t.main_path = "buildings/civ_main/orc/";
		this.t.group = "orc";
		this.t.civ_kingdom = "orc";
		this.clone("watch_tower_orc", "watch_tower_human");
		this.t.group = "orc";
		this.t.main_path = "buildings/civ_main/orc/";
		this.t.civ_kingdom = "orc";
		this.clone("fishing_docks_orc", "fishing_docks_human");
		this.t.group = "orc";
		this.t.main_path = "buildings/civ_main/orc/";
		this.t.upgrade_to = "docks_orc";
		this.t.civ_kingdom = "orc";
		this.clone("docks_orc", "docks_human");
		this.t.main_path = "buildings/civ_main/orc/";
		this.t.group = "orc";
		this.t.draw_light_area_offset_y = 8f;
		this.t.draw_light_area_offset_x = -1f;
		this.t.upgraded_from = "fishing_docks_orc";
		this.t.civ_kingdom = "orc";
		this.clone("barracks_orc", "barracks_human");
		this.t.group = "orc";
		this.t.main_path = "buildings/civ_main/orc/";
		this.t.civ_kingdom = "orc";
		this.clone("temple_orc", "temple_human");
		this.t.group = "orc";
		this.t.main_path = "buildings/civ_main/orc/";
		this.t.civ_kingdom = "orc";
		this.clone("library_orc", "library_human");
		this.t.group = "orc";
		this.t.main_path = "buildings/civ_main/orc/";
		this.t.civ_kingdom = "orc";
		this.clone("market_orc", "market_human");
		this.t.group = "orc";
		this.t.main_path = "buildings/civ_main/orc/";
		this.t.civ_kingdom = "orc";
		this.clone("windmill_orc_0", "$windmill_0$");
		this.t.group = "orc";
		this.t.main_path = "buildings/civ_main/orc/";
		this.t.upgrade_to = "windmill_orc_1";
		this.t.civ_kingdom = "orc";
		this.clone("windmill_orc_1", "$windmill_1$");
		this.t.group = "orc";
		this.t.main_path = "buildings/civ_main/orc/";
		this.t.upgraded_from = "windmill_orc_0";
		this.t.civ_kingdom = "orc";
		this.clone("tent_orc", "tent_human");
		this.t.fundament = new BuildingFundament(2, 2, 2, 0);
		this.t.upgrade_to = "house_orc_0";
		this.t.group = "orc";
		this.t.main_path = "buildings/civ_main/orc/";
		this.t.civ_kingdom = "orc";
		this.clone("house_orc_0", "house_human_0");
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.upgrade_to = "house_orc_1";
		this.t.upgraded_from = "tent_orc";
		this.t.group = "orc";
		this.t.main_path = "buildings/civ_main/orc/";
		this.t.civ_kingdom = "orc";
		this.clone("house_orc_1", "house_human_1");
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.upgrade_to = "house_orc_2";
		this.t.upgraded_from = "house_orc_0";
		this.t.group = "orc";
		this.t.main_path = "buildings/civ_main/orc/";
		this.t.civ_kingdom = "orc";
		this.clone("house_orc_2", "house_human_2");
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.upgrade_to = "house_orc_3";
		this.t.upgraded_from = "house_orc_1";
		this.t.group = "orc";
		this.t.main_path = "buildings/civ_main/orc/";
		this.t.civ_kingdom = "orc";
		this.clone("house_orc_3", "house_human_3");
		this.t.fundament = new BuildingFundament(2, 2, 2, 0);
		this.t.upgrade_to = "house_orc_4";
		this.t.upgraded_from = "house_orc_2";
		this.t.group = "orc";
		this.t.main_path = "buildings/civ_main/orc/";
		this.t.civ_kingdom = "orc";
		this.clone("house_orc_4", "house_human_4");
		this.t.fundament = new BuildingFundament(3, 3, 2, 0);
		this.t.upgrade_to = "house_orc_5";
		this.t.upgraded_from = "house_orc_3";
		this.t.group = "orc";
		this.t.main_path = "buildings/civ_main/orc/";
		this.t.civ_kingdom = "orc";
		this.clone("house_orc_5", "house_human_5");
		this.t.fundament = new BuildingFundament(3, 3, 2, 0);
		this.t.group = "orc";
		this.t.main_path = "buildings/civ_main/orc/";
		this.t.upgraded_from = "house_orc_4";
		this.t.civ_kingdom = "orc";
		this.clone("hall_orc_0", "hall_human_0");
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.upgrade_to = "hall_orc_1";
		this.t.group = "orc";
		this.t.main_path = "buildings/civ_main/orc/";
		this.t.civ_kingdom = "orc";
		this.clone("hall_orc_1", "hall_human_1");
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.upgrade_to = "hall_orc_2";
		this.t.upgraded_from = "hall_orc_0";
		this.t.group = "orc";
		this.t.main_path = "buildings/civ_main/orc/";
		this.t.civ_kingdom = "orc";
		this.clone("hall_orc_2", "hall_human_2");
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.group = "orc";
		this.t.main_path = "buildings/civ_main/orc/";
		this.t.upgraded_from = "hall_orc_1";
		this.t.civ_kingdom = "orc";
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x0005D4D4 File Offset: 0x0005B6D4
	private void addElves()
	{
		this.clone("$building_civ_elf$", "$city_colored_building$");
		this.t.main_path = "buildings/civ_main/elf/";
		this.t.group = "elf";
		this.t.civ_kingdom = "elf";
		this.clone("watch_tower_elf", "watch_tower_human");
		this.t.group = "elf";
		this.t.main_path = "buildings/civ_main/elf/";
		this.t.civ_kingdom = "elf";
		this.clone("fishing_docks_elf", "fishing_docks_human");
		this.t.group = "elf";
		this.t.main_path = "buildings/civ_main/elf/";
		this.t.upgrade_to = "docks_elf";
		this.t.civ_kingdom = "elf";
		this.clone("docks_elf", "docks_human");
		this.t.group = "elf";
		this.t.main_path = "buildings/civ_main/elf/";
		this.t.draw_light_area_offset_y = 6f;
		this.t.draw_light_area_offset_x = -2f;
		this.t.upgraded_from = "fishing_docks_elf";
		this.t.civ_kingdom = "elf";
		this.clone("barracks_elf", "barracks_human");
		this.t.group = "elf";
		this.t.main_path = "buildings/civ_main/elf/";
		this.t.civ_kingdom = "elf";
		this.clone("temple_elf", "temple_human");
		this.t.group = "elf";
		this.t.main_path = "buildings/civ_main/elf/";
		this.t.civ_kingdom = "elf";
		this.clone("library_elf", "library_human");
		this.t.group = "elf";
		this.t.main_path = "buildings/civ_main/elf/";
		this.t.civ_kingdom = "elf";
		this.clone("market_elf", "market_human");
		this.t.group = "elf";
		this.t.main_path = "buildings/civ_main/elf/";
		this.t.civ_kingdom = "elf";
		this.clone("windmill_elf_0", "$windmill_0$");
		this.t.group = "elf";
		this.t.main_path = "buildings/civ_main/elf/";
		this.t.upgrade_to = "windmill_elf_1";
		this.t.civ_kingdom = "elf";
		this.clone("windmill_elf_1", "$windmill_1$");
		this.t.group = "elf";
		this.t.main_path = "buildings/civ_main/elf/";
		this.t.upgraded_from = "windmill_elf_0";
		this.t.civ_kingdom = "elf";
		this.clone("tent_elf", "tent_human");
		this.t.fundament = new BuildingFundament(2, 2, 2, 0);
		this.t.upgrade_to = "house_elf_0";
		this.t.group = "elf";
		this.t.main_path = "buildings/civ_main/elf/";
		this.t.civ_kingdom = "elf";
		this.clone("house_elf_0", "house_human_0");
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.upgrade_to = "house_elf_1";
		this.t.upgraded_from = "tent_human";
		this.t.group = "elf";
		this.t.main_path = "buildings/civ_main/elf/";
		this.t.civ_kingdom = "elf";
		this.clone("house_elf_1", "house_human_1");
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.upgrade_to = "house_elf_2";
		this.t.upgraded_from = "house_elf_0";
		this.t.group = "elf";
		this.t.main_path = "buildings/civ_main/elf/";
		this.t.civ_kingdom = "elf";
		this.clone("house_elf_2", "house_human_2");
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.upgrade_to = "house_elf_3";
		this.t.upgraded_from = "house_elf_1";
		this.t.group = "elf";
		this.t.main_path = "buildings/civ_main/elf/";
		this.t.civ_kingdom = "elf";
		this.clone("house_elf_3", "house_human_3");
		this.t.fundament = new BuildingFundament(2, 2, 2, 0);
		this.t.upgrade_to = "house_elf_4";
		this.t.upgraded_from = "house_elf_2";
		this.t.group = "elf";
		this.t.main_path = "buildings/civ_main/elf/";
		this.t.civ_kingdom = "elf";
		this.clone("house_elf_4", "house_human_4");
		this.t.fundament = new BuildingFundament(3, 3, 2, 0);
		this.t.upgrade_to = "house_elf_5";
		this.t.upgraded_from = "house_elf_3";
		this.t.group = "elf";
		this.t.main_path = "buildings/civ_main/elf/";
		this.t.civ_kingdom = "elf";
		this.clone("house_elf_5", "house_human_5");
		this.t.fundament = new BuildingFundament(3, 3, 2, 0);
		this.t.upgraded_from = "house_elf_4";
		this.t.group = "elf";
		this.t.main_path = "buildings/civ_main/elf/";
		this.t.civ_kingdom = "elf";
		this.clone("hall_elf_0", "hall_human_0");
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.upgrade_to = "hall_elf_1";
		this.t.group = "elf";
		this.t.main_path = "buildings/civ_main/elf/";
		this.t.civ_kingdom = "elf";
		this.clone("hall_elf_1", "hall_human_1");
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.upgrade_to = "hall_elf_2";
		this.t.upgraded_from = "hall_elf_0";
		this.t.group = "elf";
		this.t.main_path = "buildings/civ_main/elf/";
		this.t.civ_kingdom = "elf";
		this.clone("hall_elf_2", "hall_human_2");
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.group = "elf";
		this.t.main_path = "buildings/civ_main/elf/";
		this.t.upgraded_from = "hall_elf_1";
		this.t.civ_kingdom = "elf";
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x0005DC20 File Offset: 0x0005BE20
	private void addDwarves()
	{
		this.clone("$building_civ_dwarf$", "$city_colored_building$");
		this.t.main_path = "buildings/civ_main/dwarf/";
		this.t.group = "dwarf";
		this.t.civ_kingdom = "dwarf";
		this.clone("watch_tower_dwarf", "watch_tower_human");
		this.t.group = "dwarf";
		this.t.main_path = "buildings/civ_main/dwarf/";
		this.t.civ_kingdom = "dwarf";
		this.clone("fishing_docks_dwarf", "fishing_docks_human");
		this.t.group = "dwarf";
		this.t.main_path = "buildings/civ_main/dwarf/";
		this.t.upgrade_to = "docks_dwarf";
		this.t.civ_kingdom = "dwarf";
		this.clone("docks_dwarf", "docks_human");
		this.t.group = "dwarf";
		this.t.main_path = "buildings/civ_main/dwarf/";
		this.t.draw_light_area_offset_y = 10f;
		this.t.upgraded_from = "fishing_docks_dwarf";
		this.t.civ_kingdom = "dwarf";
		this.clone("barracks_dwarf", "barracks_human");
		this.t.group = "dwarf";
		this.t.main_path = "buildings/civ_main/dwarf/";
		this.t.civ_kingdom = "dwarf";
		this.clone("temple_dwarf", "temple_human");
		this.t.group = "dwarf";
		this.t.main_path = "buildings/civ_main/dwarf/";
		this.t.civ_kingdom = "dwarf";
		this.clone("library_dwarf", "library_human");
		this.t.group = "dwarf";
		this.t.main_path = "buildings/civ_main/dwarf/";
		this.t.civ_kingdom = "dwarf";
		this.clone("market_dwarf", "market_human");
		this.t.group = "dwarf";
		this.t.main_path = "buildings/civ_main/dwarf/";
		this.t.civ_kingdom = "dwarf";
		this.clone("windmill_dwarf_0", "$windmill_0$");
		this.t.group = "dwarf";
		this.t.main_path = "buildings/civ_main/dwarf/";
		this.t.upgrade_to = "windmill_dwarf_1";
		this.t.civ_kingdom = "dwarf";
		this.clone("windmill_dwarf_1", "$windmill_1$");
		this.t.group = "dwarf";
		this.t.main_path = "buildings/civ_main/dwarf/";
		this.t.upgraded_from = "windmill_dwarf_0";
		this.t.civ_kingdom = "dwarf";
		this.clone("tent_dwarf", "tent_human");
		this.t.fundament = new BuildingFundament(2, 2, 2, 0);
		this.t.upgrade_to = "house_dwarf_0";
		this.t.group = "dwarf";
		this.t.main_path = "buildings/civ_main/dwarf/";
		this.t.civ_kingdom = "dwarf";
		this.clone("house_dwarf_0", "house_human_0");
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.upgrade_to = "house_dwarf_1";
		this.t.upgraded_from = "tent_dwarf";
		this.t.group = "dwarf";
		this.t.main_path = "buildings/civ_main/dwarf/";
		this.t.civ_kingdom = "dwarf";
		this.clone("house_dwarf_1", "house_human_1");
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.upgrade_to = "house_dwarf_2";
		this.t.upgraded_from = "house_dwarf_0";
		this.t.group = "dwarf";
		this.t.main_path = "buildings/civ_main/dwarf/";
		this.t.civ_kingdom = "dwarf";
		this.clone("house_dwarf_2", "house_human_2");
		this.t.fundament = new BuildingFundament(1, 1, 2, 0);
		this.t.upgrade_to = "house_dwarf_3";
		this.t.upgraded_from = "house_dwarf_1";
		this.t.group = "dwarf";
		this.t.main_path = "buildings/civ_main/dwarf/";
		this.t.civ_kingdom = "dwarf";
		this.clone("house_dwarf_3", "house_human_3");
		this.t.fundament = new BuildingFundament(2, 2, 2, 0);
		this.t.upgrade_to = "house_dwarf_4";
		this.t.upgraded_from = "house_dwarf_2";
		this.t.group = "dwarf";
		this.t.main_path = "buildings/civ_main/dwarf/";
		this.t.setHousingSlots(6);
		this.t.civ_kingdom = "dwarf";
		this.clone("house_dwarf_4", "house_human_4");
		this.t.fundament = new BuildingFundament(3, 3, 2, 0);
		this.t.upgrade_to = "house_dwarf_5";
		this.t.upgraded_from = "house_dwarf_3";
		this.t.group = "dwarf";
		this.t.main_path = "buildings/civ_main/dwarf/";
		this.t.setHousingSlots(8);
		this.t.civ_kingdom = "dwarf";
		this.clone("house_dwarf_5", "house_human_5");
		this.t.fundament = new BuildingFundament(3, 3, 2, 0);
		this.t.group = "dwarf";
		this.t.main_path = "buildings/civ_main/dwarf/";
		this.t.upgraded_from = "house_dwarf_4";
		this.t.setHousingSlots(10);
		this.t.civ_kingdom = "dwarf";
		this.clone("hall_dwarf_0", "hall_human_0");
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.upgrade_to = "hall_dwarf_1";
		this.t.group = "dwarf";
		this.t.main_path = "buildings/civ_main/dwarf/";
		this.t.civ_kingdom = "dwarf";
		this.clone("hall_dwarf_1", "hall_human_1");
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.upgrade_to = "hall_dwarf_2";
		this.t.upgraded_from = "hall_dwarf_0";
		this.t.group = "dwarf";
		this.t.main_path = "buildings/civ_main/dwarf/";
		this.t.civ_kingdom = "dwarf";
		this.clone("hall_dwarf_2", "hall_human_2");
		this.t.fundament = new BuildingFundament(3, 3, 4, 0);
		this.t.group = "dwarf";
		this.t.main_path = "buildings/civ_main/dwarf/";
		this.t.upgraded_from = "1hall_dwarf";
		this.t.civ_kingdom = "dwarf";
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x0005E380 File Offset: 0x0005C580
	public void setGrowBiomeAround(string pID, int pMaxSteps, int pWorkers, float pStepInterval, CreepWorkerMovementType pMovementType)
	{
		this.t.grow_creep = true;
		this.t.grow_creep_type = pID;
		this.t.grow_creep_steps_max = pMaxSteps;
		this.t.grow_creep_workers = pWorkers;
		this.t.grow_creep_step_interval = pStepInterval;
		this.t.grow_creep_movement_type = pMovementType;
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x0005E3D8 File Offset: 0x0005C5D8
	public override void editorDiagnostic()
	{
		foreach (BuildingAsset tAsset in this.list)
		{
			if (!tAsset.mini_civ_auto_load && typeof(SB).GetField(tAsset.id, BindingFlags.Static | BindingFlags.Public) == null)
			{
				BaseAssetLibrary.logAssetError("BuildingLibrary: SB class does not have property", tAsset.id);
			}
			if (!(tAsset.type == "") && typeof(S_BuildingType).GetField(tAsset.type, BindingFlags.Static | BindingFlags.Public) == null)
			{
				BaseAssetLibrary.logAssetError("BuildingLibrary: SB class does not have type property", tAsset.type);
			}
		}
		base.editorDiagnostic();
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x0005E4A8 File Offset: 0x0005C6A8
	public void clear()
	{
		for (int i = 0; i < this.list.Count; i++)
		{
			this.list[i].buildings.Clear();
		}
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x0005E4E4 File Offset: 0x0005C6E4
	public override BuildingAsset add(BuildingAsset pAsset)
	{
		BuildingAsset tNewAsset = base.add(pAsset);
		if (tNewAsset.base_stats == null)
		{
			tNewAsset.base_stats = new BaseStats();
			tNewAsset.base_stats["health"] = 100f;
			tNewAsset.base_stats["size"] = 2f;
		}
		return tNewAsset;
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x0005E538 File Offset: 0x0005C738
	public string addToGameplayReport()
	{
		string tResult = "##### Buildings: \n\n";
		tResult += "\nAsset ID                           | type                             | building_type                    | health                           | size                             | city_building                    | can_be_upgraded                  | upgrade_from                     | upgrade_to\n";
		int tFirstColumn = 35;
		int tOffset = 35;
		foreach (BuildingAsset tAsset in this.list)
		{
			int tRow = 0;
			string text = "> " + tAsset.id;
			string tType = tAsset.type;
			string tBuildingType = tAsset.building_type.ToString();
			string tHealth = tAsset.base_stats["health"].ToString();
			string tSize = tAsset.base_stats["size"].ToString();
			string tCityBuilding = tAsset.city_building.ToString();
			string tCanBeUpgraded = tAsset.can_be_upgraded.ToString();
			string tUpgradeFrom = tAsset.upgraded_from;
			string tUpgradeTo = tAsset.upgrade_to;
			string tLineInfo = text;
			this.addLine(ref tLineInfo, tType, tFirstColumn + tOffset * tRow++);
			this.addLine(ref tLineInfo, tBuildingType, tFirstColumn + tOffset * tRow++);
			this.addLine(ref tLineInfo, tHealth, tFirstColumn + tOffset * tRow++);
			this.addLine(ref tLineInfo, tSize, tFirstColumn + tOffset * tRow++);
			this.addLine(ref tLineInfo, tCityBuilding, tFirstColumn + tOffset * tRow++);
			this.addLine(ref tLineInfo, tCanBeUpgraded, tFirstColumn + tOffset * tRow++);
			this.addLine(ref tLineInfo, tUpgradeFrom, tFirstColumn + tOffset * tRow++);
			this.addLine(ref tLineInfo, tUpgradeTo, tFirstColumn + tOffset * tRow++);
			tLineInfo += "\n";
			tResult += tLineInfo;
		}
		tResult += "\n## END OF BUILDINGS REPORT\n";
		tResult = tResult + Toolbox.getRepeatedString('=', 100) + "\n\n";
		tResult += "\n\n";
		return tResult;
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x0005E738 File Offset: 0x0005C938
	private void addLine(ref string pLineInfo, string pText, int pSize)
	{
		pLineInfo = Toolbox.fillRight(pLineInfo, pSize, ' ');
		pLineInfo = pLineInfo + "| " + pText;
	}

	// Token: 0x040006B1 RID: 1713
	public static readonly Vector2 shadow_under_construction_bound = new Vector2(0f, 0.61f);

	// Token: 0x040006B2 RID: 1714
	public static readonly float shadow_under_construction_distortion = 0.19f;

	// Token: 0x040006B3 RID: 1715
	private const string TEMPLATE_CREEP = "$building_creep$";

	// Token: 0x040006B4 RID: 1716
	private const string TEMPLATE_RESOURCE = "$resource$";

	// Token: 0x040006B5 RID: 1717
	private const string TEMPLATE_MINERAL = "$mineral$";

	// Token: 0x040006B6 RID: 1718
	private const string TEMPLATE_FLORA_SMALL = "$flora_small$";

	// Token: 0x040006B7 RID: 1719
	private const string TEMPLATE_BUILDING = "$building$";

	// Token: 0x040006B8 RID: 1720
	private const string TEMPLATE_WAYPOINT = "$waypoint$";

	// Token: 0x040006B9 RID: 1721
	private const string TEMPLATE_DROP_SPREADER = "$drop_spreader$";

	// Token: 0x040006BA RID: 1722
	private const string TEMPLATE_CITY_BUILDING = "$city_building$";

	// Token: 0x040006BB RID: 1723
	private const string TEMPLATE_CITY_COLORED_BUILDING = "$city_colored_building$";

	// Token: 0x040006BC RID: 1724
	private const string TEMPLATE_WINDMILL_BASE = "$windmill_base$";

	// Token: 0x040006BD RID: 1725
	private const string TEMPLATE_WINDMILL_0 = "$windmill_0$";

	// Token: 0x040006BE RID: 1726
	private const string TEMPLATE_WINDMILL_1 = "$windmill_1$";

	// Token: 0x040006BF RID: 1727
	private const string TEMPLATE_BUILDING_CIV_HUMAN = "$building_civ_human$";

	// Token: 0x040006C0 RID: 1728
	private const string TEMPLATE_BUILDING_CIV_ORC = "$building_civ_orc$";

	// Token: 0x040006C1 RID: 1729
	private const string TEMPLATE_BUILDING_CIV_DWARF = "$building_civ_dwarf$";

	// Token: 0x040006C2 RID: 1730
	private const string TEMPLATE_BUILDING_CIV_ELF = "$building_civ_elf$";
}
