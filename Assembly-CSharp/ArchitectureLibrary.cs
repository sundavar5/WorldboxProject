using System;

// Token: 0x020000B8 RID: 184
public class ArchitectureLibrary : AssetLibrary<ArchitectureAsset>
{
	// Token: 0x060005D1 RID: 1489 RVA: 0x00055568 File Offset: 0x00053768
	public override void init()
	{
		base.init();
		this.addTemplates();
		this.addClassic();
		this.addUnique();
		this.addAnimal();
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x00055588 File Offset: 0x00053788
	private void addTemplates()
	{
		this.add(new ArchitectureAsset
		{
			id = "$template_with_generated_buildings$",
			generation_target = "human",
			generate_buildings = true
		});
		this.t.styled_building_orders = new string[]
		{
			"order_docks_0",
			"order_docks_1",
			"order_house_0",
			"order_hall_0",
			"order_windmill_0",
			"order_watch_tower",
			"order_temple",
			"order_library",
			"order_barracks"
		};
		this.t.shared_building_orders = new ValueTuple<string, string>[]
		{
			new ValueTuple<string, string>("order_bonfire", "bonfire"),
			new ValueTuple<string, string>("order_statue", "statue"),
			new ValueTuple<string, string>("order_well", "well"),
			new ValueTuple<string, string>("order_stockpile", "stockpile"),
			new ValueTuple<string, string>("order_mine", "mine"),
			new ValueTuple<string, string>("order_training_dummy", "training_dummy")
		};
		this.t.actor_asset_id_trading = "boat_trading_human";
		this.t.actor_asset_id_transport = "boat_transport_human";
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x000556D0 File Offset: 0x000538D0
	private void addClassic()
	{
		this.add(new ArchitectureAsset
		{
			id = "human"
		});
		this.t.actor_asset_id_trading = "boat_trading_human";
		this.t.actor_asset_id_transport = "boat_transport_human";
		this.t.styled_building_orders = new string[]
		{
			"order_docks_0",
			"order_tent",
			"order_house_0",
			"order_house_1",
			"order_house_2",
			"order_house_3",
			"order_house_4",
			"order_house_5",
			"order_hall_0",
			"order_hall_1",
			"order_hall_2",
			"order_windmill_0",
			"order_windmill_1",
			"order_docks_1",
			"order_watch_tower",
			"order_temple",
			"order_library",
			"order_market",
			"order_barracks"
		};
		this.t.shared_building_orders = new ValueTuple<string, string>[]
		{
			new ValueTuple<string, string>("order_bonfire", "bonfire"),
			new ValueTuple<string, string>("order_statue", "statue"),
			new ValueTuple<string, string>("order_well", "well"),
			new ValueTuple<string, string>("order_stockpile", "stockpile"),
			new ValueTuple<string, string>("order_mine", "mine"),
			new ValueTuple<string, string>("order_training_dummy", "training_dummy")
		};
		this.clone("orc", "human");
		this.t.actor_asset_id_trading = "boat_trading_orc";
		this.t.actor_asset_id_transport = "boat_transport_orc";
		this.clone("elf", "human");
		this.t.actor_asset_id_trading = "boat_trading_elf";
		this.t.actor_asset_id_transport = "boat_transport_elf";
		this.clone("dwarf", "human");
		this.t.actor_asset_id_trading = "boat_trading_dwarf";
		this.t.actor_asset_id_transport = "boat_transport_dwarf";
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x000558F0 File Offset: 0x00053AF0
	private void addUnique()
	{
		this.clone("civ_necromancer", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_necromancer";
		this.t.actor_asset_id_transport = "boat_transport_necromancer";
		this.t.spread_biome_id = "biome_corrupted";
		this.clone("civ_alien", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_alien";
		this.t.actor_asset_id_transport = "boat_transport_alien";
		this.t.projectile_id = "plasma_ball";
		this.clone("civ_druid", "$template_with_generated_buildings$");
		this.t.spread_biome_id = "biome_jungle";
		this.t.actor_asset_id_trading = "boat_trading_druid";
		this.t.actor_asset_id_transport = "boat_transport_druid";
		this.clone("civ_bee", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_bee";
		this.t.actor_asset_id_transport = "boat_transport_bee";
		this.clone("civ_beetle", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_beetle";
		this.t.actor_asset_id_transport = "boat_transport_beetle";
		this.clone("civ_seal", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_seal";
		this.t.actor_asset_id_transport = "boat_transport_seal";
		this.clone("civ_unicorn", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_unicorn";
		this.t.actor_asset_id_transport = "boat_transport_unicorn";
		this.clone("civ_ghost", "$template_with_generated_buildings$");
		this.t.has_shadows = false;
		this.t.material = "jelly";
		this.t.actor_asset_id_trading = "boat_trading_ghost";
		this.t.actor_asset_id_transport = "boat_transport_ghost";
		this.clone("civ_fairy", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_fairy";
		this.t.actor_asset_id_transport = "boat_transport_fairy";
		this.clone("civ_evil_mage", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_evil_mage";
		this.t.actor_asset_id_transport = "boat_transport_evil_mage";
		this.t.replaceSharedID("order_stockpile", "stockpile_fireproof");
		this.t.burnable_buildings = false;
		this.clone("civ_white_mage", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_white_mage";
		this.t.actor_asset_id_transport = "boat_transport_white_mage";
		this.clone("civ_bandit", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_bandit";
		this.t.actor_asset_id_transport = "boat_transport_bandit";
		this.clone("civ_demon", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_demon";
		this.t.actor_asset_id_transport = "boat_transport_demon";
		this.t.burnable_buildings = false;
		this.t.spread_biome_id = "biome_infernal";
		this.t.replaceSharedID("order_stockpile", "stockpile_fireproof");
		this.clone("civ_cold_one", "$template_with_generated_buildings$");
		this.t.spread_biome_id = "biome_permafrost";
		this.t.actor_asset_id_trading = "boat_trading_cold_one";
		this.t.actor_asset_id_transport = "boat_transport_cold_one";
		this.clone("civ_angle", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_angle";
		this.t.actor_asset_id_transport = "boat_transport_angle";
		this.clone("civ_snowman", "$template_with_generated_buildings$");
		this.t.spread_biome_id = "biome_permafrost";
		this.t.actor_asset_id_trading = "boat_trading_snowman";
		this.t.actor_asset_id_transport = "boat_transport_snowman";
		this.clone("civ_garlic_man", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_garlic_man";
		this.t.actor_asset_id_transport = "boat_transport_garlic_man";
		this.t.spread_biome_id = "biome_garlic";
		this.clone("civ_lemon_man", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_lemon_man";
		this.t.actor_asset_id_transport = "boat_transport_lemon_man";
		this.t.spread_biome_id = "biome_lemon";
		this.clone("civ_acid_gentleman", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_acid_gentleman";
		this.t.actor_asset_id_transport = "boat_transport_acid_gentleman";
		this.t.spread_biome_id = "biome_wasteland";
		this.t.acid_affected_buildings = false;
		this.t.replaceSharedID("order_stockpile", "stockpile_acidproof");
		this.clone("civ_crystal_golem", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_crystal_golem";
		this.t.actor_asset_id_transport = "boat_transport_crystal_golem";
		this.t.spread_biome_id = "biome_crystal";
		this.t.burnable_buildings = false;
		this.clone("civ_candy_man", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_candy_man";
		this.t.actor_asset_id_transport = "boat_transport_candy_man";
		this.t.spread_biome_id = "biome_candy";
		this.clone("civ_liliar", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_liliar";
		this.t.actor_asset_id_transport = "boat_transport_liliar";
		this.t.spread_biome_id = "biome_flower";
		this.clone("civ_greg", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_greg";
		this.t.actor_asset_id_transport = "boat_transport_greg";
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x00055EB0 File Offset: 0x000540B0
	private void addAnimal()
	{
		this.clone("civ_cat", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_cat";
		this.t.actor_asset_id_transport = "boat_transport_cat";
		this.clone("civ_dog", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_dog";
		this.t.actor_asset_id_transport = "boat_transport_dog";
		this.clone("civ_chicken", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_chicken";
		this.t.actor_asset_id_transport = "boat_transport_chicken";
		this.clone("civ_rabbit", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_rabbit";
		this.t.actor_asset_id_transport = "boat_transport_rabbit";
		this.clone("civ_monkey", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_monkey";
		this.t.actor_asset_id_transport = "boat_transport_monkey";
		this.clone("civ_fox", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_fox";
		this.t.actor_asset_id_transport = "boat_transport_fox";
		this.clone("civ_sheep", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_sheep";
		this.t.actor_asset_id_transport = "boat_transport_sheep";
		this.clone("civ_cow", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_cow";
		this.t.actor_asset_id_transport = "boat_transport_cow";
		this.clone("civ_armadillo", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_armadillo";
		this.t.actor_asset_id_transport = "boat_transport_armadillo";
		this.clone("civ_wolf", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_wolf";
		this.t.actor_asset_id_transport = "boat_transport_wolf";
		this.clone("civ_bear", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_bear";
		this.t.actor_asset_id_transport = "boat_transport_bear";
		this.clone("civ_rhino", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_rhino";
		this.t.actor_asset_id_transport = "boat_transport_rhino";
		this.clone("civ_buffalo", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_buffalo";
		this.t.actor_asset_id_transport = "boat_transport_buffalo";
		this.clone("civ_hyena", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_hyena";
		this.t.actor_asset_id_transport = "boat_transport_hyena";
		this.clone("civ_rat", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_rat";
		this.t.actor_asset_id_transport = "boat_transport_rat";
		this.clone("civ_alpaca", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_alpaca";
		this.t.actor_asset_id_transport = "boat_transport_alpaca";
		this.clone("civ_capybara", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_capybara";
		this.t.actor_asset_id_transport = "boat_transport_capybara";
		this.clone("civ_goat", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_goat";
		this.t.actor_asset_id_transport = "boat_transport_goat";
		this.clone("civ_scorpion", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_scorpion";
		this.t.actor_asset_id_transport = "boat_transport_scorpion";
		this.clone("civ_crab", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_crab";
		this.t.actor_asset_id_transport = "boat_transport_crab";
		this.clone("civ_penguin", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_penguin";
		this.t.actor_asset_id_transport = "boat_transport_penguin";
		this.t.spread_biome_id = "biome_permafrost";
		this.clone("civ_turtle", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_turtle";
		this.t.actor_asset_id_transport = "boat_transport_turtle";
		this.clone("civ_crocodile", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_crocodile";
		this.t.actor_asset_id_transport = "boat_transport_crocodile";
		this.clone("civ_snake", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_snake";
		this.t.actor_asset_id_transport = "boat_transport_snake";
		this.clone("civ_frog", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_frog";
		this.t.actor_asset_id_transport = "boat_transport_frog";
		this.clone("civ_piranha", "$template_with_generated_buildings$");
		this.t.actor_asset_id_trading = "boat_trading_piranha";
		this.t.actor_asset_id_transport = "boat_transport_piranha";
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x000563C8 File Offset: 0x000545C8
	public override void post_init()
	{
		base.post_init();
		foreach (ArchitectureAsset tAsset in this.list)
		{
			if (!string.IsNullOrEmpty(tAsset.spread_biome_id))
			{
				tAsset.spread_biome = true;
			}
		}
		this.initBuildingKeys();
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x00056434 File Offset: 0x00054634
	private void initBuildingKeys()
	{
		foreach (ArchitectureAsset tAsset in this.list)
		{
			if (!tAsset.isTemplateAsset())
			{
				this.loadAutoBuildingsForAsset(tAsset);
				foreach (ValueTuple<string, string> tSharedBuilding in tAsset.shared_building_orders)
				{
					tAsset.addBuildingOrderKey(tSharedBuilding.Item1, tSharedBuilding.Item2);
				}
			}
		}
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x000564C4 File Offset: 0x000546C4
	private void loadAutoBuildingsForAsset(ArchitectureAsset pAsset)
	{
		string tArchitectureID = pAsset.id;
		foreach (string tOrderID in pAsset.styled_building_orders)
		{
			string tBuildCivID = null;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(tOrderID);
			if (num <= 2390352231U)
			{
				if (num <= 1798277731U)
				{
					if (num <= 735917779U)
					{
						if (num != 279793225U)
						{
							if (num != 673856294U)
							{
								if (num == 735917779U)
								{
									if (tOrderID == "order_library")
									{
										tBuildCivID = "library_" + tArchitectureID;
									}
								}
							}
							else if (tOrderID == "order_stockpile")
							{
								tBuildCivID = "stockpile";
							}
						}
						else if (tOrderID == "order_temple")
						{
							tBuildCivID = "temple_" + tArchitectureID;
						}
					}
					else if (num != 1339425632U)
					{
						if (num != 1354987549U)
						{
							if (num == 1798277731U)
							{
								if (tOrderID == "order_mine")
								{
									tBuildCivID = "mine";
								}
							}
						}
						else if (tOrderID == "order_training_dummy")
						{
							tBuildCivID = "training_dummy";
						}
					}
					else if (tOrderID == "order_market")
					{
						tBuildCivID = "market_" + tArchitectureID;
					}
				}
				else
				{
					if (num <= 2340019374U)
					{
						if (num != 2306464136U)
						{
							if (num != 2323241755U)
							{
								if (num != 2340019374U)
								{
									goto IL_4F6;
								}
								if (!(tOrderID == "order_house_3"))
								{
									goto IL_4F6;
								}
							}
							else if (!(tOrderID == "order_house_0"))
							{
								goto IL_4F6;
							}
						}
						else if (!(tOrderID == "order_house_1"))
						{
							goto IL_4F6;
						}
					}
					else if (num != 2356796993U)
					{
						if (num != 2373574612U)
						{
							if (num != 2390352231U)
							{
								goto IL_4F6;
							}
							if (!(tOrderID == "order_house_4"))
							{
								goto IL_4F6;
							}
						}
						else if (!(tOrderID == "order_house_5"))
						{
							goto IL_4F6;
						}
					}
					else if (!(tOrderID == "order_house_2"))
					{
						goto IL_4F6;
					}
					tBuildCivID = "house_" + tArchitectureID + "_" + tOrderID.Substring(tOrderID.Length - 1);
				}
			}
			else
			{
				if (num <= 2850734180U)
				{
					if (num <= 2825189663U)
					{
						if (num != 2612399145U)
						{
							if (num != 2808412044U)
							{
								if (num != 2825189663U)
								{
									goto IL_4F6;
								}
								if (!(tOrderID == "order_hall_1"))
								{
									goto IL_4F6;
								}
							}
							else if (!(tOrderID == "order_hall_0"))
							{
								goto IL_4F6;
							}
						}
						else
						{
							if (!(tOrderID == "order_tent"))
							{
								goto IL_4F6;
							}
							tBuildCivID = "tent_" + tArchitectureID;
							goto IL_4F6;
						}
					}
					else if (num != 2839302848U)
					{
						if (num != 2841967282U)
						{
							if (num != 2850734180U)
							{
								goto IL_4F6;
							}
							if (!(tOrderID == "order_windmill_1"))
							{
								goto IL_4F6;
							}
							goto IL_431;
						}
						else if (!(tOrderID == "order_hall_2"))
						{
							goto IL_4F6;
						}
					}
					else
					{
						if (!(tOrderID == "order_well"))
						{
							goto IL_4F6;
						}
						tBuildCivID = "well";
						goto IL_4F6;
					}
					tBuildCivID = "hall_" + tArchitectureID + "_" + tOrderID.Substring(tOrderID.Length - 1);
					goto IL_4F6;
				}
				if (num <= 3352047755U)
				{
					if (num != 2867511799U)
					{
						if (num != 3349403640U)
						{
							if (num != 3352047755U)
							{
								goto IL_4F6;
							}
							if (!(tOrderID == "order_barracks"))
							{
								goto IL_4F6;
							}
							tBuildCivID = "barracks_" + tArchitectureID;
							goto IL_4F6;
						}
						else
						{
							if (!(tOrderID == "order_statue"))
							{
								goto IL_4F6;
							}
							tBuildCivID = "statue";
							goto IL_4F6;
						}
					}
					else if (!(tOrderID == "order_windmill_0"))
					{
						goto IL_4F6;
					}
				}
				else if (num <= 3879833531U)
				{
					if (num != 3863055912U)
					{
						if (num != 3879833531U)
						{
							goto IL_4F6;
						}
						if (!(tOrderID == "order_docks_0"))
						{
							goto IL_4F6;
						}
						tBuildCivID = "fishing_docks_" + tArchitectureID;
						goto IL_4F6;
					}
					else
					{
						if (!(tOrderID == "order_docks_1"))
						{
							goto IL_4F6;
						}
						tBuildCivID = "docks_" + tArchitectureID;
						goto IL_4F6;
					}
				}
				else if (num != 3995760707U)
				{
					if (num != 4148002217U)
					{
						goto IL_4F6;
					}
					if (!(tOrderID == "order_watch_tower"))
					{
						goto IL_4F6;
					}
					tBuildCivID = "watch_tower_" + tArchitectureID;
					goto IL_4F6;
				}
				else
				{
					if (!(tOrderID == "order_bonfire"))
					{
						goto IL_4F6;
					}
					tBuildCivID = "bonfire";
					goto IL_4F6;
				}
				IL_431:
				tBuildCivID = "windmill_" + tArchitectureID + "_" + tOrderID.Substring(tOrderID.Length - 1);
			}
			IL_4F6:
			if (tBuildCivID != null)
			{
				pAsset.addBuildingOrderKey(tOrderID, tBuildCivID);
			}
		}
	}

	// Token: 0x040005F1 RID: 1521
	private const string TEMPLATE_WITH_GENERATED_BUILDINGS = "$template_with_generated_buildings$";
}
