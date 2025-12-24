using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x020000BE RID: 190
[Serializable]
public class BuildingAsset : Asset
{
	// Token: 0x1700001E RID: 30
	// (get) Token: 0x060005EC RID: 1516 RVA: 0x00056B67 File Offset: 0x00054D67
	[JsonIgnore]
	public bool has_sound_spawn
	{
		get
		{
			return this.sound_spawn != null;
		}
	}

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x060005ED RID: 1517 RVA: 0x00056B75 File Offset: 0x00054D75
	[JsonIgnore]
	public bool has_sound_idle
	{
		get
		{
			return this.sound_idle != null;
		}
	}

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x060005EE RID: 1518 RVA: 0x00056B83 File Offset: 0x00054D83
	[JsonIgnore]
	public bool has_sound_hit
	{
		get
		{
			return this.sound_hit != null;
		}
	}

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x060005EF RID: 1519 RVA: 0x00056B91 File Offset: 0x00054D91
	[JsonIgnore]
	public bool has_sound_built
	{
		get
		{
			return this.sound_built != null;
		}
	}

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00056B9F File Offset: 0x00054D9F
	[JsonIgnore]
	public bool has_sound_destroyed
	{
		get
		{
			return this.sound_destroyed != null;
		}
	}

	// Token: 0x060005F1 RID: 1521 RVA: 0x00056BAD File Offset: 0x00054DAD
	public bool setSpread(FloraType pType, int pSpreadSteps = 1, float pSpreadChance = 1f)
	{
		if (pType == FloraType.None)
		{
			this.spread = false;
			return false;
		}
		this.spread = true;
		this.flora_type = pType;
		this.spread_steps = (float)pSpreadSteps;
		this.spread_chance = pSpreadChance;
		return true;
	}

	// Token: 0x060005F2 RID: 1522 RVA: 0x00056BD9 File Offset: 0x00054DD9
	public void setAtlasID(string pAtlasID, string pFallbackID = null)
	{
		if (pFallbackID == null)
		{
			pFallbackID = pAtlasID;
		}
		this.atlas_id = pAtlasID;
		this.atlas_id_fallback_when_not_wobbly = pFallbackID;
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x00056BEF File Offset: 0x00054DEF
	public void setShadow(float pBoundX, float pBoundY, float pDistortion)
	{
		this.shadow = true;
		this.shadow_bound.x = pBoundX;
		this.shadow_bound.y = pBoundY;
		this.shadow_distortion = pDistortion;
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x00056C17 File Offset: 0x00054E17
	public bool isOverlaysBiomeTags(TileTypeBase pTileType)
	{
		return !this.has_biome_tags || pTileType.overlapsBiomeTags(this.biome_tags_growth);
	}

	// Token: 0x060005F5 RID: 1525 RVA: 0x00056C2F File Offset: 0x00054E2F
	public bool isOverlaysBiomeSpreadTags(TileTypeBase pTileType)
	{
		return this.has_biome_tags_spread && pTileType.overlapsBiomeTags(this.biome_tags_spread);
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x00056C48 File Offset: 0x00054E48
	public void checkLimits(Building pBuildingToIgnore = null)
	{
		if (this.limit_global == 0)
		{
			return;
		}
		if (this.buildings.Count < this.limit_global)
		{
			return;
		}
		int tRemove = this.buildings.Count - this.limit_global;
		foreach (Building tBuilding in this.buildings)
		{
			if (tRemove == 0)
			{
				break;
			}
			if ((pBuildingToIgnore == null || pBuildingToIgnore != tBuilding) && tBuilding.isAlive())
			{
				tBuilding.startDestroyBuilding();
				tRemove--;
			}
		}
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x00056CE4 File Offset: 0x00054EE4
	public bool canBeOccupied()
	{
		return this.hasHousingSlots() || this.docks || this.spawn_units;
	}

	// Token: 0x060005F8 RID: 1528 RVA: 0x00056CFE File Offset: 0x00054EFE
	public void addResource(string pID, int pAmount, bool pNewList = false)
	{
		if (this.resources_given == null || pNewList)
		{
			this.resources_given = new List<ResourceContainer>();
		}
		this.resources_given.Add(new ResourceContainer(pID, pAmount));
	}

	// Token: 0x060005F9 RID: 1529 RVA: 0x00056D2C File Offset: 0x00054F2C
	public bool hasResourceGiven(string pID)
	{
		if (this.resources_given == null)
		{
			return false;
		}
		using (List<ResourceContainer>.Enumerator enumerator = this.resources_given.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.id == pID)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x00056D98 File Offset: 0x00054F98
	public ActorAsset getRandomBoatAssetToBuild(City pCity)
	{
		string tBoatType = this.boat_types.GetRandom<string>();
		string tBoatAssetID = this.getBoatAssetIDFromType(tBoatType, pCity);
		if (string.IsNullOrEmpty(tBoatAssetID))
		{
			return null;
		}
		return AssetManager.actor_library.get(tBoatAssetID);
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x00056DCF File Offset: 0x00054FCF
	public void setHousingSlots(int pValue)
	{
		this.can_units_live_here = true;
		this.housing_slots = pValue;
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x00056DDF File Offset: 0x00054FDF
	public bool hasHousingSlots()
	{
		return this.housing_slots > 0;
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x00056DEC File Offset: 0x00054FEC
	private string getBoatAssetIDFromType(string pSpeciesBoat, City pCity)
	{
		if (pCity == null)
		{
			return "boat_fishing";
		}
		ArchitectureAsset tArchitectureAsset = pCity.getActorAsset().architecture_asset;
		if (pSpeciesBoat == "boat_type_fishing")
		{
			return tArchitectureAsset.actor_asset_id_boat_fishing;
		}
		if (pSpeciesBoat == "boat_type_trading")
		{
			return tArchitectureAsset.actor_asset_id_trading;
		}
		if (!(pSpeciesBoat == "boat_type_transport"))
		{
			return tArchitectureAsset.actor_asset_id_boat_fishing;
		}
		return tArchitectureAsset.actor_asset_id_transport;
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x00056E52 File Offset: 0x00055052
	public void checkSpritesAreLoaded()
	{
		if (!this.sprites_are_initiated)
		{
			this.sprites_are_initiated = true;
			this.loadBuildingSprites();
		}
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x00056E6C File Offset: 0x0005506C
	public void loadBuildingSprites()
	{
		Sprite[] tSprites = this.loadBuildingSpriteList();
		PreloadHelpers.total_building_sprites += tSprites.Length;
		PreloadHelpers.all_preloaded_sprites_buildings.AddRange(tSprites);
		BuildingSprites tBuildingSprites = new BuildingSprites();
		this.building_sprites = tBuildingSprites;
		PreloadHelpers.total_building_sprite_containers++;
		foreach (Sprite tSprite in tSprites)
		{
			string[] array = tSprite.name.Split('_', StringSplitOptions.None);
			string tID = array[0];
			int tIndex = int.Parse(array[1]);
			while (tBuildingSprites.animation_data.Count < tIndex + 1)
			{
				tBuildingSprites.animation_data.Add(null);
			}
			if (tBuildingSprites.animation_data[tIndex] == null)
			{
				tBuildingSprites.animation_data[tIndex] = new BuildingAnimationData();
			}
			BuildingAnimationData tData = this.building_sprites.animation_data[tIndex];
			bool tConstructionSprite = false;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(tID);
			if (num <= 975326616U)
			{
				if (num != 119931743U)
				{
					if (num != 871591685U)
					{
						if (num == 975326616U)
						{
							if (tID == "spawn")
							{
								BuildingAnimationData buildingAnimationData = tData;
								if (buildingAnimationData.list_spawn == null)
								{
									buildingAnimationData.list_spawn = new ListPool<Sprite>();
								}
								tData.list_spawn.Add(tSprite);
								if (tData.list_spawn.Count > 1)
								{
									tData.animated = true;
								}
							}
						}
					}
					else if (tID == "disabled")
					{
						BuildingAnimationData buildingAnimationData = tData;
						if (buildingAnimationData.list_main_disabled == null)
						{
							buildingAnimationData.list_main_disabled = new ListPool<Sprite>();
						}
						tData.list_main_disabled.Add(tSprite);
						if (tData.list_main_disabled.Count > 1)
						{
							tData.animated = true;
						}
					}
				}
				else if (tID == "ruin")
				{
					BuildingAnimationData buildingAnimationData = tData;
					if (buildingAnimationData.list_ruins == null)
					{
						buildingAnimationData.list_ruins = new ListPool<Sprite>();
					}
					tData.list_ruins.Add(tSprite);
				}
			}
			else if (num <= 3476073356U)
			{
				if (num != 2324299930U)
				{
					if (num == 3476073356U)
					{
						if (tID == "construction")
						{
							this.building_sprites.construction = tSprite;
							tConstructionSprite = true;
						}
					}
				}
				else if (tID == "mini")
				{
					this.building_sprites.map_icon = new BuildingMapIcon(tSprite);
				}
			}
			else if (num != 3644904752U)
			{
				if (num == 3935363592U)
				{
					if (tID == "main")
					{
						BuildingAnimationData buildingAnimationData = tData;
						if (buildingAnimationData.list_main == null)
						{
							buildingAnimationData.list_main = new ListPool<Sprite>();
						}
						tData.list_main.Add(tSprite);
						if (tData.list_main.Count > 1)
						{
							tData.animated = true;
						}
					}
				}
			}
			else if (tID == "special")
			{
				BuildingAnimationData buildingAnimationData = tData;
				if (buildingAnimationData.list_special == null)
				{
					buildingAnimationData.list_special = new ListPool<Sprite>();
				}
				tData.list_special.Add(tSprite);
			}
			if (this.shadow)
			{
				DynamicSpriteCreator.createBuildingShadow(this, tSprite, tConstructionSprite);
			}
		}
		foreach (BuildingAnimationData buildingAnimationData2 in this.building_sprites.animation_data)
		{
			ListPool<Sprite> list_main = buildingAnimationData2.list_main;
			buildingAnimationData2.main = ((list_main != null) ? list_main.ToArray<Sprite>() : null);
			ListPool<Sprite> list_spawn = buildingAnimationData2.list_spawn;
			buildingAnimationData2.spawn = ((list_spawn != null) ? list_spawn.ToArray<Sprite>() : null);
			ListPool<Sprite> list_main_disabled = buildingAnimationData2.list_main_disabled;
			buildingAnimationData2.main_disabled = ((list_main_disabled != null) ? list_main_disabled.ToArray<Sprite>() : null);
			ListPool<Sprite> list_ruins = buildingAnimationData2.list_ruins;
			buildingAnimationData2.ruins = ((list_ruins != null) ? list_ruins.ToArray<Sprite>() : null);
			ListPool<Sprite> list_special = buildingAnimationData2.list_special;
			buildingAnimationData2.special = ((list_special != null) ? list_special.ToArray<Sprite>() : null);
			ListPool<Sprite> list_main2 = buildingAnimationData2.list_main;
			if (list_main2 != null)
			{
				list_main2.Dispose();
			}
			ListPool<Sprite> list_spawn2 = buildingAnimationData2.list_spawn;
			if (list_spawn2 != null)
			{
				list_spawn2.Dispose();
			}
			ListPool<Sprite> list_main_disabled2 = buildingAnimationData2.list_main_disabled;
			if (list_main_disabled2 != null)
			{
				list_main_disabled2.Dispose();
			}
			ListPool<Sprite> list_ruins2 = buildingAnimationData2.list_ruins;
			if (list_ruins2 != null)
			{
				list_ruins2.Dispose();
			}
			ListPool<Sprite> list_special2 = buildingAnimationData2.list_special;
			if (list_special2 != null)
			{
				list_special2.Dispose();
			}
			buildingAnimationData2.list_main = null;
			buildingAnimationData2.list_spawn = null;
			buildingAnimationData2.list_main_disabled = null;
			buildingAnimationData2.list_ruins = null;
			buildingAnimationData2.list_special = null;
		}
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x000572D8 File Offset: 0x000554D8
	public Sprite[] loadBuildingSpriteList()
	{
		string tPath = this.sprite_path;
		if (string.IsNullOrEmpty(tPath))
		{
			tPath = this.main_path + this.id;
		}
		return SpriteTextureLoader.getSpriteList(tPath, false);
	}

	// Token: 0x040005FD RID: 1533
	[NonSerialized]
	public bool sprites_are_initiated;

	// Token: 0x040005FE RID: 1534
	public Vector3 scale_base = new Vector3(0.25f, 0.25f, 0.25f);

	// Token: 0x040005FF RID: 1535
	[DefaultValue("")]
	public string kingdom = string.Empty;

	// Token: 0x04000600 RID: 1536
	[DefaultValue("")]
	public string civ_kingdom = string.Empty;

	// Token: 0x04000601 RID: 1537
	public BuildingFundament fundament;

	// Token: 0x04000602 RID: 1538
	[DefaultValue("building")]
	public string material = "building";

	// Token: 0x04000603 RID: 1539
	[DefaultValue("buildings")]
	public string atlas_id = "buildings";

	// Token: 0x04000604 RID: 1540
	[DefaultValue("buildings")]
	public string atlas_id_fallback_when_not_wobbly = "buildings";

	// Token: 0x04000605 RID: 1541
	[NonSerialized]
	public DynamicSpritesAsset atlas_asset;

	// Token: 0x04000606 RID: 1542
	public bool prevent_freeze;

	// Token: 0x04000607 RID: 1543
	public float bonus_z;

	// Token: 0x04000608 RID: 1544
	public bool removed_by_sponge;

	// Token: 0x04000609 RID: 1545
	[DefaultValue("")]
	public string sprite_path = string.Empty;

	// Token: 0x0400060A RID: 1546
	[DefaultValue("buildings/")]
	public string main_path = "buildings/";

	// Token: 0x0400060B RID: 1547
	public bool grow_creep;

	// Token: 0x0400060C RID: 1548
	[DefaultValue(CreepWorkerMovementType.RandomNeighbourAll)]
	public CreepWorkerMovementType grow_creep_movement_type;

	// Token: 0x0400060D RID: 1549
	[DefaultValue("")]
	public string grow_creep_type = string.Empty;

	// Token: 0x0400060E RID: 1550
	public bool draw_light_area;

	// Token: 0x0400060F RID: 1551
	public float draw_light_area_offset_x;

	// Token: 0x04000610 RID: 1552
	public float draw_light_area_offset_y;

	// Token: 0x04000611 RID: 1553
	[DefaultValue(0.5f)]
	public float draw_light_size = 0.5f;

	// Token: 0x04000612 RID: 1554
	public int grow_creep_steps_max;

	// Token: 0x04000613 RID: 1555
	public float grow_creep_step_interval;

	// Token: 0x04000614 RID: 1556
	[DefaultValue(1)]
	public int grow_creep_workers = 1;

	// Token: 0x04000615 RID: 1557
	public bool grow_creep_direction_random_position;

	// Token: 0x04000616 RID: 1558
	public bool grow_creep_random_new_direction;

	// Token: 0x04000617 RID: 1559
	public bool grow_creep_flash;

	// Token: 0x04000618 RID: 1560
	public int construction_progress_needed;

	// Token: 0x04000619 RID: 1561
	public bool grow_creep_redraw_tile;

	// Token: 0x0400061A RID: 1562
	[DefaultValue(7)]
	public int grow_creep_steps_before_new_direction = 7;

	// Token: 0x0400061B RID: 1563
	[DefaultValue(true)]
	public bool has_ruins_graphics = true;

	// Token: 0x0400061C RID: 1564
	public bool has_special_animation_state;

	// Token: 0x0400061D RID: 1565
	[DefaultValue(6f)]
	public float animation_speed = 6f;

	// Token: 0x0400061E RID: 1566
	[DefaultValue(BuildingType.Building_None)]
	public BuildingType building_type;

	// Token: 0x0400061F RID: 1567
	public bool sparkle_effect;

	// Token: 0x04000620 RID: 1568
	public List<ResourceContainer> resources_given;

	// Token: 0x04000621 RID: 1569
	public bool can_be_grown;

	// Token: 0x04000622 RID: 1570
	[DefaultValue(0.5f)]
	public float vegetation_random_chance = 0.5f;

	// Token: 0x04000623 RID: 1571
	public bool has_kingdom_color;

	// Token: 0x04000624 RID: 1572
	public bool city_building;

	// Token: 0x04000625 RID: 1573
	public bool can_be_abandoned;

	// Token: 0x04000626 RID: 1574
	public bool mini_civ_auto_load;

	// Token: 0x04000627 RID: 1575
	public bool destroy_on_liquid;

	// Token: 0x04000628 RID: 1576
	public bool can_be_upgraded;

	// Token: 0x04000629 RID: 1577
	[DefaultValue("")]
	public string upgrade_to = string.Empty;

	// Token: 0x0400062A RID: 1578
	[DefaultValue("")]
	public string upgraded_from = string.Empty;

	// Token: 0x0400062B RID: 1579
	public int upgrade_level;

	// Token: 0x0400062C RID: 1580
	[DefaultValue("")]
	public string type = string.Empty;

	// Token: 0x0400062D RID: 1581
	public bool gatherable;

	// Token: 0x0400062E RID: 1582
	public bool wheat;

	// Token: 0x0400062F RID: 1583
	public bool produce_biome_food;

	// Token: 0x04000630 RID: 1584
	public float growth_time;

	// Token: 0x04000631 RID: 1585
	public int loot_generation;

	// Token: 0x04000632 RID: 1586
	public string[] boat_types;

	// Token: 0x04000633 RID: 1587
	public string boat_type_fishing;

	// Token: 0x04000634 RID: 1588
	public string boat_type_trading;

	// Token: 0x04000635 RID: 1589
	public string boat_type_transport;

	// Token: 0x04000636 RID: 1590
	public bool waypoint;

	// Token: 0x04000637 RID: 1591
	public int priority;

	// Token: 0x04000638 RID: 1592
	public BuildingStepAction step_action;

	// Token: 0x04000639 RID: 1593
	[NonSerialized]
	public bool has_step_action;

	// Token: 0x0400063A RID: 1594
	[DefaultValue(true)]
	public bool shadow = true;

	// Token: 0x0400063B RID: 1595
	public Vector2 shadow_bound = new Vector2(0.5f, 0.8f);

	// Token: 0x0400063C RID: 1596
	[DefaultValue(0.2f)]
	public float shadow_distortion = 0.2f;

	// Token: 0x0400063D RID: 1597
	public bool auto_remove_ruin;

	// Token: 0x0400063E RID: 1598
	public bool ice_tower;

	// Token: 0x0400063F RID: 1599
	public bool spawn_units;

	// Token: 0x04000640 RID: 1600
	public bool beehive;

	// Token: 0x04000641 RID: 1601
	[DefaultValue("-")]
	public string spawn_units_asset = "-";

	// Token: 0x04000642 RID: 1602
	public bool tower;

	// Token: 0x04000643 RID: 1603
	[DefaultValue("")]
	public string tower_projectile = string.Empty;

	// Token: 0x04000644 RID: 1604
	public float tower_projectile_offset;

	// Token: 0x04000645 RID: 1605
	[DefaultValue(false)]
	public bool tower_attack_buildings;

	// Token: 0x04000646 RID: 1606
	[DefaultValue(3f)]
	public float tower_projectile_reload = 3f;

	// Token: 0x04000647 RID: 1607
	[DefaultValue(1)]
	public int tower_projectile_amount = 1;

	// Token: 0x04000648 RID: 1608
	public bool ignore_other_buildings_for_upgrade;

	// Token: 0x04000649 RID: 1609
	public bool random_flip;

	// Token: 0x0400064A RID: 1610
	public ConstructionCost cost;

	// Token: 0x0400064B RID: 1611
	public BaseStats base_stats;

	// Token: 0x0400064C RID: 1612
	public bool ignored_by_cities;

	// Token: 0x0400064D RID: 1613
	public bool remove_buildings_when_dropped;

	// Token: 0x0400064E RID: 1614
	public bool remove_civ_buildings;

	// Token: 0x0400064F RID: 1615
	public bool ignore_same_building_id;

	// Token: 0x04000650 RID: 1616
	public bool build_road_to;

	// Token: 0x04000651 RID: 1617
	public bool can_be_damaged_by_tornado;

	// Token: 0x04000652 RID: 1618
	public bool can_be_placed_on_liquid;

	// Token: 0x04000653 RID: 1619
	public bool can_be_placed_on_blocks;

	// Token: 0x04000654 RID: 1620
	public bool damaged_by_rain;

	// Token: 0x04000655 RID: 1621
	public bool only_build_tiles;

	// Token: 0x04000656 RID: 1622
	public bool build_place_borders;

	// Token: 0x04000657 RID: 1623
	public bool build_place_single;

	// Token: 0x04000658 RID: 1624
	public bool build_place_center;

	// Token: 0x04000659 RID: 1625
	public bool needs_farms_ground;

	// Token: 0x0400065A RID: 1626
	public bool build_place_batch;

	// Token: 0x0400065B RID: 1627
	public bool build_prefer_replace_house;

	// Token: 0x0400065C RID: 1628
	public bool check_for_close_building;

	// Token: 0x0400065D RID: 1629
	public bool ignore_buildings;

	// Token: 0x0400065E RID: 1630
	public bool can_be_demolished;

	// Token: 0x0400065F RID: 1631
	public bool burnable;

	// Token: 0x04000660 RID: 1632
	public bool affected_by_lava;

	// Token: 0x04000661 RID: 1633
	public bool affected_by_acid;

	// Token: 0x04000662 RID: 1634
	public bool can_units_live_here;

	// Token: 0x04000663 RID: 1635
	public int housing_slots;

	// Token: 0x04000664 RID: 1636
	public int housing_happiness;

	// Token: 0x04000665 RID: 1637
	public int max_houses;

	// Token: 0x04000666 RID: 1638
	public bool storage;

	// Token: 0x04000667 RID: 1639
	public bool storage_only_food;

	// Token: 0x04000668 RID: 1640
	[DefaultValue(true)]
	public bool can_be_living_house = true;

	// Token: 0x04000669 RID: 1641
	[DefaultValue(true)]
	public bool can_be_living_plant = true;

	// Token: 0x0400066A RID: 1642
	[DefaultValue(true)]
	public bool remove_ruins = true;

	// Token: 0x0400066B RID: 1643
	[DefaultValue(true)]
	public bool has_ruin_state = true;

	// Token: 0x0400066C RID: 1644
	public bool has_resources_to_collect;

	// Token: 0x0400066D RID: 1645
	public bool has_resources_grown_to_collect;

	// Token: 0x0400066E RID: 1646
	public bool has_resources_grown_to_collect_on_spawn;

	// Token: 0x0400066F RID: 1647
	public bool can_be_chopped_down;

	// Token: 0x04000670 RID: 1648
	public int book_slots;

	// Token: 0x04000671 RID: 1649
	public BuildingOverrideMainSprites get_override_sprites_main;

	// Token: 0x04000672 RID: 1650
	public BuildingOverrideMainSprite get_override_sprite_main;

	// Token: 0x04000673 RID: 1651
	public bool is_vegetation;

	// Token: 0x04000674 RID: 1652
	public bool is_stockpile;

	// Token: 0x04000675 RID: 1653
	public Vector2 stockpile_top_left_offset;

	// Token: 0x04000676 RID: 1654
	public Vector2 stockpile_center_offset;

	// Token: 0x04000677 RID: 1655
	public int limit_per_zone;

	// Token: 0x04000678 RID: 1656
	public bool become_alive_when_chopped;

	// Token: 0x04000679 RID: 1657
	public int limit_in_radius;

	// Token: 0x0400067A RID: 1658
	public int limit_global;

	// Token: 0x0400067B RID: 1659
	public bool docks;

	// Token: 0x0400067C RID: 1660
	[NonSerialized]
	public bool has_biome_tags;

	// Token: 0x0400067D RID: 1661
	public HashSet<BiomeTag> biome_tags_growth;

	// Token: 0x0400067E RID: 1662
	[NonSerialized]
	public bool has_biome_tags_spread;

	// Token: 0x0400067F RID: 1663
	public HashSet<BiomeTag> biome_tags_spread;

	// Token: 0x04000680 RID: 1664
	public bool spread_biome;

	// Token: 0x04000681 RID: 1665
	public string spread_biome_id;

	// Token: 0x04000682 RID: 1666
	[DefaultValue("")]
	public string group = string.Empty;

	// Token: 0x04000683 RID: 1667
	public bool affected_by_drought;

	// Token: 0x04000684 RID: 1668
	public bool affected_by_cold_temperature;

	// Token: 0x04000685 RID: 1669
	public bool smoke;

	// Token: 0x04000686 RID: 1670
	[DefaultValue(0.5f)]
	public float smoke_interval = 0.5f;

	// Token: 0x04000687 RID: 1671
	public Vector2Int smoke_offset;

	// Token: 0x04000688 RID: 1672
	public bool spawn_drops;

	// Token: 0x04000689 RID: 1673
	[DefaultValue("")]
	public string spawn_drop_id = "";

	// Token: 0x0400068A RID: 1674
	public float spawn_drop_interval;

	// Token: 0x0400068B RID: 1675
	public float spawn_drop_start_height;

	// Token: 0x0400068C RID: 1676
	public float spawn_drop_min_height;

	// Token: 0x0400068D RID: 1677
	public float spawn_drop_max_height;

	// Token: 0x0400068E RID: 1678
	public float spawn_drop_min_radius;

	// Token: 0x0400068F RID: 1679
	public float spawn_drop_max_radius;

	// Token: 0x04000690 RID: 1680
	public string transform_tiles_to_tile_type;

	// Token: 0x04000691 RID: 1681
	public string transform_tiles_to_top_tiles;

	// Token: 0x04000692 RID: 1682
	public string sound_spawn;

	// Token: 0x04000693 RID: 1683
	public string sound_idle;

	// Token: 0x04000694 RID: 1684
	public string sound_hit;

	// Token: 0x04000695 RID: 1685
	public string sound_built;

	// Token: 0x04000696 RID: 1686
	public string sound_destroyed;

	// Token: 0x04000697 RID: 1687
	public int nutrition_restore;

	// Token: 0x04000698 RID: 1688
	public bool spawn_rats;

	// Token: 0x04000699 RID: 1689
	public bool flora;

	// Token: 0x0400069A RID: 1690
	public FloraSize flora_size;

	// Token: 0x0400069B RID: 1691
	public bool spread;

	// Token: 0x0400069C RID: 1692
	public float spread_chance;

	// Token: 0x0400069D RID: 1693
	public float spread_steps;

	// Token: 0x0400069E RID: 1694
	public FloraType flora_type;

	// Token: 0x0400069F RID: 1695
	public string[] spread_ids;

	// Token: 0x040006A0 RID: 1696
	public bool has_sprites_spawn;

	// Token: 0x040006A1 RID: 1697
	public bool has_sprites_main;

	// Token: 0x040006A2 RID: 1698
	public bool has_sprites_main_disabled;

	// Token: 0x040006A3 RID: 1699
	public bool has_sprites_ruin;

	// Token: 0x040006A4 RID: 1700
	public bool has_sprites_special;

	// Token: 0x040006A5 RID: 1701
	public bool has_sprite_construction;

	// Token: 0x040006A6 RID: 1702
	public bool check_for_adaptation_tags;

	// Token: 0x040006A7 RID: 1703
	public GetColorForMapIcon get_map_icon_color;

	// Token: 0x040006A8 RID: 1704
	public bool has_get_map_icon_color;

	// Token: 0x040006A9 RID: 1705
	[NonSerialized]
	public BuildingSprites building_sprites;

	// Token: 0x040006AA RID: 1706
	[NonSerialized]
	public HashSet<Building> buildings = new HashSet<Building>();
}
