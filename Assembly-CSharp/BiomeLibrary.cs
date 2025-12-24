using System;
using System.Collections.Generic;

// Token: 0x02000027 RID: 39
public class BiomeLibrary : AssetLibrary<BiomeAsset>
{
	// Token: 0x06000205 RID: 517 RVA: 0x0000F831 File Offset: 0x0000DA31
	public override void init()
	{
		base.init();
		this.addNormalBiomes();
		this.addCreepBiomes();
		this.addSpecialBiomes();
	}

	// Token: 0x06000206 RID: 518 RVA: 0x0000F84C File Offset: 0x0000DA4C
	public void addNormalBiomes()
	{
		this.add(new BiomeAsset
		{
			id = "biome_grass",
			tile_low = "grass_low",
			tile_high = "grass_high",
			localized_key = "Grass",
			grow_strength = 5,
			spread_biome = true,
			spread_by_drops_water = true,
			generator_pot_amount = 8,
			grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral),
			grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees),
			grow_type_selector_plants = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants),
			grow_type_selector_bushes = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomBushes),
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"graminae",
				"viridis",
				"pratensis"
			})
		});
		this.t.addSubspeciesTrait("fast_builders");
		this.t.addCultureTrait("pep_talks");
		this.t.addCultureTrait("youth_reverence");
		this.t.addCultureTrait("gossip_lovers");
		this.t.addLanguageTrait("strict_spelling");
		this.t.addUnit("wolf", 1);
		this.t.addUnit("fox", 2);
		this.t.addUnit("raccoon", 1);
		this.t.addUnit("sheep", 3);
		this.t.addUnit("chicken", 3);
		this.t.addUnit("rabbit", 3);
		this.t.addUnit("fly", 4);
		this.t.addUnit("beetle", 4);
		this.t.addUnit("grasshopper", 4);
		this.t.addSapientUnit("human", 1);
		this.t.addSapientUnit("dwarf", 1);
		this.t.addSapientUnit("orc", 1);
		this.t.addSapientUnit("elf", 1);
		this.t.addSapientUnit("civ_wolf", 1);
		this.t.addSapientUnit("civ_fox", 1);
		this.t.addSapientUnit("bandit", 1);
		this.t.addSapientUnit("civ_sheep", 1);
		this.t.addSapientUnit("civ_chicken", 1);
		this.t.addSapientUnit("civ_rabbit", 1);
		this.t.addMineral("mineral_stone", 4);
		this.t.addMineral("mineral_metals", 3);
		this.t.addTree("tree_green_1", 1);
		this.t.addTree("tree_green_2", 1);
		this.t.addTree("tree_green_3", 1);
		this.t.addPlant("mushroom_red", 2);
		this.t.addPlant("mushroom_green", 2);
		this.t.addPlant("mushroom_teal", 2);
		this.t.addPlant("mushroom_white", 2);
		this.t.addPlant("mushroom_yellow", 2);
		this.t.addPlant("green_herb", 4);
		this.t.addPlant("flower", 4);
		this.t.addBush("fruit_bush", 1);
		this.add(new BiomeAsset
		{
			id = "biome_savanna",
			tile_low = "savanna_low",
			tile_high = "savanna_high",
			localized_key = "Savanna",
			spread_biome = true,
			spread_by_drops_water = true,
			generator_pot_amount = 5,
			grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral),
			grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees),
			grow_type_selector_plants = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants),
			grow_type_selector_bushes = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomBushes),
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"savannae",
				"aridus",
				"praeruptus"
			})
		});
		this.t.addActorTrait("fast");
		this.t.addCultureTrait("sword_lovers");
		this.t.addLanguageTrait("elegant_words");
		this.t.addUnit("rhino", 1);
		this.t.addUnit("hyena", 1);
		this.t.addUnit("buffalo", 1);
		this.t.addUnit("cat", 3);
		this.t.addUnit("armadillo", 1);
		this.t.addUnit("ostrich", 2);
		this.t.addUnit("fly", 4);
		this.t.addUnit("beetle", 4);
		this.t.addSapientUnit("human", 1);
		this.t.addSapientUnit("dwarf", 1);
		this.t.addSapientUnit("orc", 1);
		this.t.addSapientUnit("elf", 1);
		this.t.addSapientUnit("civ_cat", 1);
		this.t.addSapientUnit("civ_rhino", 1);
		this.t.addSapientUnit("civ_hyena", 1);
		this.t.addSapientUnit("civ_buffalo", 1);
		this.t.addSapientUnit("civ_armadillo", 1);
		this.t.addMineral("mineral_stone", 5);
		this.t.addMineral("mineral_metals", 3);
		this.t.addTree("savanna_tree_1", 5);
		this.t.addTree("savanna_tree_2", 5);
		this.t.addTree("savanna_tree_big_1", 1);
		this.t.addTree("savanna_tree_big_2", 1);
		this.t.addPlant("savanna_plant", 1);
		this.t.addBush("fruit_bush", 1);
		this.add(new BiomeAsset
		{
			id = "biome_jungle",
			tile_low = "jungle_low",
			tile_high = "jungle_high",
			localized_key = "Jungle",
			spread_biome = true,
			spread_by_drops_water = true,
			generator_pot_amount = 6,
			grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral),
			grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees),
			grow_type_selector_plants = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants),
			grow_type_selector_bushes = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomBushes),
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"junglis",
				"viriditas",
				"tropicus"
			})
		});
		this.t.addActorTrait("tiny");
		this.t.addActorTrait("poison_immune");
		this.t.addCultureTrait("spear_lovers");
		this.t.addCultureTrait("city_layout_raindrops");
		this.t.addLanguageTrait("beautiful_calligraphy");
		this.t.addUnit("snake", 1);
		this.t.addUnit("cat", 2);
		this.t.addUnit("monkey", 1);
		this.t.addUnit("frog", 3);
		this.t.addUnit("scorpion", 1);
		this.t.addSapientUnit("human", 1);
		this.t.addSapientUnit("dwarf", 1);
		this.t.addSapientUnit("orc", 1);
		this.t.addSapientUnit("elf", 1);
		this.t.addSapientUnit("civ_snake", 1);
		this.t.addSapientUnit("civ_cat", 1);
		this.t.addSapientUnit("civ_monkey", 1);
		this.t.addSapientUnit("civ_frog", 1);
		this.t.addSapientUnit("civ_scorpion", 1);
		this.t.addMineral("mineral_silver", 5);
		this.t.addMineral("mineral_metals", 1);
		this.t.addMineral("mineral_gems", 1);
		this.t.addTree("jungle_tree", 1);
		this.t.addPlant("jungle_plant", 1);
		this.t.addPlant("jungle_flower", 1);
		this.t.addBush("fruit_bush", 1);
		this.add(new BiomeAsset
		{
			id = "biome_desert",
			tile_low = "desert_low",
			tile_high = "desert_high",
			localized_key = "Desert",
			spread_biome = true,
			spread_by_drops_water = true,
			generator_pot_amount = 4,
			grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral),
			grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees),
			grow_type_selector_plants = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants),
			grow_type_selector_bushes = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomBushes),
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"solantheria",
				"ergensis",
				"deserticus"
			})
		});
		this.t.addActorTrait("eagle_eyed");
		this.t.addClanTrait("silver_tongues");
		this.t.addCultureTrait("solitude_seekers");
		this.t.addCultureTrait("gossip_lovers");
		this.t.addLanguageTrait("stylish_writing");
		this.t.addSubspeciesTraitAlways("adaptation_desert");
		this.t.addUnit("cat", 1);
		this.t.addUnit("snake", 1);
		this.t.addSapientUnit("human", 1);
		this.t.addSapientUnit("dwarf", 1);
		this.t.addSapientUnit("orc", 1);
		this.t.addSapientUnit("elf", 1);
		this.t.addSapientUnit("civ_cat", 1);
		this.t.addSapientUnit("civ_snake", 1);
		this.t.addMineral("mineral_stone", 4);
		this.t.addMineral("mineral_metals", 3);
		this.t.addMineral("mineral_gems", 1);
		this.t.addMineral("mineral_silver", 1);
		this.t.addTree("desert_tree", 1);
		this.t.addPlant("desert_plant", 1);
		this.t.addUnit("scorpion", 1);
		this.add(new BiomeAsset
		{
			id = "biome_lemon",
			tile_low = "lemon_low",
			tile_high = "lemon_high",
			localized_key = "Lemon",
			spread_biome = true,
			spread_by_drops_water = true,
			generator_pot_amount = 3,
			grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral),
			grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees),
			grow_type_selector_plants = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants),
			grow_type_selector_bushes = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomBushes),
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"aurantium",
				"acidus",
				"limonum"
			})
		});
		this.t.addActorTrait("genius");
		this.t.addCultureTrait("fast_learners");
		this.t.addLanguageTrait("melodic");
		this.t.addLanguageTrait("elegant_words");
		this.t.addUnit("lemon_snail", 1);
		this.t.addSapientUnit("civ_lemon_man", 1);
		this.t.addMineral("mineral_stone", 5);
		this.t.addMineral("mineral_metals", 3);
		this.t.addTree("lemon_tree", 1);
		this.t.addPlant("mushroom_red", 1);
		this.t.addPlant("mushroom_green", 1);
		this.t.addPlant("mushroom_teal", 1);
		this.t.addPlant("mushroom_white", 1);
		this.t.addPlant("mushroom_yellow", 1);
		this.t.addPlant("green_herb", 5);
		this.t.addPlant("flower", 5);
		this.t.addBush("fruit_bush", 1);
		this.add(new BiomeAsset
		{
			id = "biome_permafrost",
			tile_low = "permafrost_low",
			tile_high = "permafrost_high",
			localized_key = "Permafrost",
			cold_biome = true,
			spread_biome = true,
			generator_pot_amount = 3,
			grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral),
			grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees),
			grow_type_selector_plants = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants),
			grow_type_selector_bushes = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomBushes),
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"albus",
				"sibericus",
				"frostis"
			})
		});
		this.t.addActorTrait("freeze_proof");
		this.t.addSubspeciesTrait("cold_resistance");
		this.t.addSubspeciesTraitAlways("adaptation_permafrost");
		this.t.addCultureTrait("attentive_readers");
		this.t.addCultureTrait("ancestors_knowledge");
		this.t.addUnit("penguin", 4);
		this.t.addUnit("bear", 1);
		this.t.addUnit("wolf", 2);
		this.t.addUnit("snowman", 1);
		this.t.addMineral("mineral_stone", 4);
		this.t.addMineral("mineral_metals", 3);
		this.t.addSapientUnit("human", 1);
		this.t.addSapientUnit("cold_one", 1);
		this.t.addSapientUnit("civ_penguin", 1);
		this.t.addSapientUnit("civ_bear", 1);
		this.t.addSapientUnit("civ_wolf", 1);
		this.t.addSapientUnit("snowman", 1);
		this.t.addTree("pine_tree", 1);
		this.t.addPlant("snow_plant", 1);
		this.add(new BiomeAsset
		{
			id = "biome_swamp",
			tile_low = "swamp_low",
			tile_high = "swamp_high",
			localized_key = "Swamp",
			spread_biome = true,
			spread_by_drops_water = true,
			generator_pot_amount = 3,
			generator_max_size = 300,
			grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral),
			grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees),
			grow_type_selector_plants = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants),
			grow_type_selector_bushes = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomBushes),
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"paludis",
				"limosa",
				"paluster"
			})
		});
		this.t.addActorTrait("regeneration");
		this.t.addCultureTrait("bow_lovers");
		this.t.addLanguageTrait("scribble");
		this.t.addSubspeciesTraitAlways("adaptation_swamp");
		this.t.addSubspeciesTraitEvolution("mutation_skin_tentacle_horror");
		this.t.addUnit("crocodile", 1);
		this.t.addUnit("snake", 4);
		this.t.addUnit("frog", 6);
		this.t.addUnit("fly", 5);
		this.t.addUnit("turtle", 3);
		this.t.addUnit("capybara", 1);
		this.t.addSapientUnit("orc", 1);
		this.t.addSapientUnit("elf", 1);
		this.t.addSapientUnit("bandit", 1);
		this.t.addSapientUnit("civ_crocodile", 1);
		this.t.addSapientUnit("civ_snake", 1);
		this.t.addSapientUnit("civ_frog", 1);
		this.t.addSapientUnit("civ_capybara", 1);
		this.t.addSapientUnit("civ_piranha", 1);
		this.t.addMineral("mineral_stone", 4);
		this.t.addMineral("mineral_metals", 3);
		this.t.addTree("swamp_tree", 1);
		this.t.addPlant("swamp_plant", 1);
		this.t.addPlant("swamp_plant_big", 1);
		this.add(new BiomeAsset
		{
			id = "biome_crystal",
			tile_low = "crystal_low",
			tile_high = "crystal_high",
			localized_key = "Crystal",
			loot_generation = 2,
			spread_biome = true,
			generator_pot_amount = 2,
			grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral),
			grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees),
			grow_type_selector_plants = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants),
			grow_type_selector_bushes = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomBushes),
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"crystallus",
				"lucidus",
				"prismatus"
			})
		});
		this.t.addActorTrait("strong");
		this.t.addSubspeciesTrait("bioproduct_gems");
		this.t.addCultureTrait("tiny_legends");
		this.t.addCultureTrait("armorsmith_mastery");
		this.t.addCultureTrait("weaponsmith_mastery");
		this.t.addLanguageTrait("powerful_words");
		this.t.addUnit("crystal_sword", 2);
		this.t.addSapientUnit("dwarf", 1);
		this.t.addSapientUnit("civ_crystal_golem", 1);
		this.t.addTree("crystal_tree", 1);
		this.t.addPlant("crystal_plant", 1);
		this.add(new BiomeAsset
		{
			id = "biome_enchanted",
			tile_low = "enchanted_low",
			tile_high = "enchanted_high",
			localized_key = "Enchanted",
			spread_biome = true,
			spread_by_drops_blessing = true,
			loot_generation = 1,
			generator_pot_amount = 3,
			grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral),
			grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees),
			grow_type_selector_plants = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants),
			grow_type_selector_bushes = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomBushes),
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"incantatum",
				"enchanta",
				"herbaeonis"
			})
		});
		this.t.addActorTrait("attractive");
		this.t.addSubspeciesTrait("gaia_roots");
		this.t.addSubspeciesTraitEvolution("mutation_skin_energy");
		this.t.addCultureTrait("training_potential");
		this.t.addCultureTrait("diplomatic_ascension");
		this.t.addCultureTrait("legacy_keepers");
		this.t.addLanguageTrait("ancient_runes");
		this.t.addLanguageTrait("enlightening_script");
		this.t.addUnit("butterfly", 5);
		this.t.addUnit("fairy", 1);
		this.t.addSapientUnit("elf", 1);
		this.t.addSapientUnit("white_mage", 1);
		this.t.addSapientUnit("fairy", 1);
		this.t.addMineral("mineral_silver", 5);
		this.t.addMineral("mineral_metals", 2);
		this.t.addMineral("mineral_gems", 1);
		this.t.addTree("enchanted_tree", 1);
		this.t.addPlant("flower", 5);
		this.t.addBush("fruit_bush", 1);
		this.add(new BiomeAsset
		{
			id = "biome_corrupted",
			tile_low = "corrupted_low",
			tile_high = "corrupted_high",
			localized_key = "Corrupted",
			loot_generation = -1,
			dark_biome = true,
			spread_biome = true,
			spread_by_drops_curse = true,
			generator_pot_amount = 1,
			generator_max_size = 20,
			grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral),
			grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees),
			grow_type_selector_plants = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants),
			grow_type_selector_bushes = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomBushes),
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"corruptus",
				"morbida",
				"tenebris"
			})
		});
		this.t.addActorTrait("weak");
		this.t.addSubspeciesTrait("death_grow_mythril");
		this.t.addSubspeciesTraitAlways("adaptation_corruption");
		this.t.addCultureTrait("happiness_from_war");
		this.t.addCultureTrait("xenophobic");
		this.t.addCultureTrait("ethnocentric_guard");
		this.t.addLanguageTrait("cursed_font");
		this.t.addReligionTrait("rite_of_restless_dead");
		this.t.addReligionTrait("cast_curse");
		this.t.addReligionTrait("spawn_skeleton");
		this.t.addUnit("jumpy_skull", 1);
		this.t.addSapientUnit("necromancer", 1);
		this.t.addSapientUnit("jumpy_skull", 1);
		this.t.addSapientUnit("plague_doctor", 1);
		this.t.addMineral("mineral_bones", 7);
		this.t.addTree("corrupted_tree", 1);
		this.t.addTree("corrupted_tree_big", 1);
		this.t.addPlant("corrupted_plant", 1);
		this.add(new BiomeAsset
		{
			id = "biome_infernal",
			tile_low = "infernal_low",
			tile_high = "infernal_high",
			localized_key = "Infernal",
			spread_biome = true,
			spread_by_drops_fire = true,
			spread_ignore_burned_stages = true,
			generator_pot_amount = 1,
			generator_max_size = 20,
			grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral),
			grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees),
			grow_type_selector_plants = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants),
			grow_type_selector_bushes = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomBushes),
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"ignifer",
				"brasarius",
				"ardorus"
			})
		});
		this.t.addActorTrait("fire_blood");
		this.t.addActorTrait("fire_proof");
		this.t.addSubspeciesTrait("heat_resistance");
		this.t.addSubspeciesTrait("hydrophobia");
		this.t.addSubspeciesTraitAlways("adaptation_infernal");
		this.t.addCultureTrait("happiness_from_war");
		this.t.addCultureTrait("xenophobic");
		this.t.addClanTrait("cursed_blood");
		this.t.addLanguageTrait("raging_paragraphs");
		this.t.addReligionTrait("rite_of_the_abyss");
		this.t.addUnit("fire_skull", 1);
		this.t.addSapientUnit("evil_mage", 1);
		this.t.addSapientUnit("demon", 1);
		this.t.addSapientUnit("fire_skull", 1);
		this.t.addMineral("mineral_metals", 3);
		this.t.addMineral("mineral_adamantine", 3);
		this.t.addTree("infernal_tree", 1);
		this.t.addTree("infernal_tree_big", 1);
		this.t.addTree("infernal_tree_small", 1);
		this.t.addPlant("flame_flower", 1);
		this.add(new BiomeAsset
		{
			id = "biome_candy",
			tile_low = "candy_low",
			tile_high = "candy_high",
			localized_key = "Candy",
			spread_biome = true,
			spread_by_drops_water = true,
			generator_pot_amount = 1,
			generator_max_size = 30,
			grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral),
			grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees),
			grow_type_selector_plants = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants),
			grow_type_selector_bushes = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomBushes),
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"dulcis",
				"glucosus",
				"crispus"
			})
		});
		this.t.addActorTrait("fat");
		this.t.addActorTrait("gluttonous");
		this.t.addSubspeciesTrait("annoying_fireworks");
		this.t.addSubspeciesTrait("super_positivity");
		this.t.addCultureTrait("tiny_legends");
		this.t.addLanguageTrait("foolish_glyphs");
		this.t.addUnit("smore", 1);
		this.t.addSapientUnit("civ_candy_man", 1);
		this.t.addMineral("mineral_stone", 4);
		this.t.addMineral("mineral_metals", 3);
		this.t.addTree("candy_tree", 1);
		this.t.addPlant("candy_plant", 1);
		this.add(new BiomeAsset
		{
			id = "biome_mushroom",
			tile_low = "mushroom_low",
			tile_high = "mushroom_high",
			localized_key = "Mushroom",
			spread_biome = true,
			spread_by_drops_water = true,
			spread_by_drops_powerup = true,
			generator_pot_amount = 1,
			generator_max_size = 300,
			grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral),
			grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees),
			grow_type_selector_plants = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants),
			grow_type_selector_bushes = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomBushes),
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"sporae",
				"mycota",
				"mycetis"
			})
		});
		this.t.addActorTrait("regeneration");
		this.t.addSubspeciesTrait("bioproduct_mushrooms");
		this.t.addSubspeciesTrait("super_positivity");
		this.t.addCultureTrait("animal_whisperers");
		this.t.addCultureTrait("pep_talks");
		this.t.addLanguageTrait("powerful_words");
		this.t.addUnit("frog", 3);
		this.t.addUnit("sheep", 1);
		this.t.addSapientUnit("civ_sheep", 1);
		this.t.addSapientUnit("civ_frog", 1);
		this.t.addMineral("mineral_silver", 3);
		this.t.addMineral("mineral_stone", 5);
		this.t.addMineral("mineral_metals", 3);
		this.t.addMineral("mineral_gems", 1);
		this.t.addTree("mushroom_tree", 1);
		this.t.addPlant("flower", 2);
		this.t.addPlant("mushroom_red", 2);
		this.t.addPlant("mushroom_green", 2);
		this.t.addPlant("mushroom_teal", 2);
		this.t.addPlant("mushroom_white", 2);
		this.t.addPlant("mushroom_yellow", 2);
		this.t.addBush("fruit_bush", 1);
		this.add(new BiomeAsset
		{
			id = "biome_wasteland",
			tile_low = "wasteland_low",
			tile_high = "wasteland_high",
			localized_key = "Wasteland",
			loot_generation = -2,
			dark_biome = true,
			spread_biome = true,
			spread_by_drops_acid = true,
			grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral),
			grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees),
			grow_type_selector_plants = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants),
			grow_type_selector_bushes = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomBushes),
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"toxicus",
				"wastus"
			})
		});
		this.t.addActorTrait("ugly");
		this.t.addSubspeciesTrait("bad_genes");
		this.t.addSubspeciesTrait("bioluminescence");
		this.t.addSubspeciesTraitAlways("adaptation_wasteland");
		this.t.addSubspeciesTraitEvolution("mutation_skin_abomination");
		this.t.addCultureTrait("xenophobic");
		this.t.addCultureTrait("solitude_seekers");
		this.t.addCultureTrait("legacy_keepers");
		this.t.addLanguageTrait("foolish_glyphs");
		this.t.addUnit("rat", 4);
		this.t.addUnit("acid_blob", 1);
		this.t.addSapientUnit("civ_acid_gentleman", 1);
		this.t.addSapientUnit("civ_rat", 1);
		this.t.addMineral("mineral_stone", 4);
		this.t.addMineral("mineral_metals", 3);
		this.t.addTree("wasteland_tree", 1);
		this.t.addPlant("wasteland_flower", 1);
		this.add(new BiomeAsset
		{
			id = "biome_birch",
			tile_low = "birch_low",
			tile_high = "birch_high",
			localized_key = "Birch",
			grow_strength = 5,
			spread_biome = true,
			spread_by_drops_water = true,
			generator_pot_amount = 4,
			grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral),
			grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees),
			grow_type_selector_plants = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants),
			grow_type_selector_bushes = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomBushes),
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"silvanus",
				"virentis",
				"sylvestris"
			})
		});
		this.t.addSubspeciesTrait("enhanced_strength");
		this.t.addCultureTrait("axe_lovers");
		this.t.addCultureTrait("diplomatic_ascension");
		this.t.addUnit("wolf", 1);
		this.t.addUnit("sheep", 1);
		this.t.addUnit("rabbit", 1);
		this.t.addUnit("fox", 1);
		this.t.addSapientUnit("civ_wolf", 1);
		this.t.addSapientUnit("civ_sheep", 1);
		this.t.addSapientUnit("civ_rabbit", 1);
		this.t.addSapientUnit("civ_fox", 1);
		this.t.addMineral("mineral_stone", 4);
		this.t.addMineral("mineral_metals", 3);
		this.t.addTree("birch_tree", 1);
		this.t.addPlant("birch_plant", 1);
		this.t.addPlant("mushroom_red", 1);
		this.t.addPlant("mushroom_green", 1);
		this.t.addPlant("mushroom_teal", 1);
		this.t.addPlant("mushroom_white", 1);
		this.t.addPlant("mushroom_yellow", 1);
		this.t.addPlant("green_herb", 4);
		this.t.addPlant("flower", 4);
		this.t.addBush("fruit_bush", 1);
		this.add(new BiomeAsset
		{
			id = "biome_maple",
			tile_low = "maple_low",
			tile_high = "maple_high",
			localized_key = "Maple",
			grow_strength = 5,
			spread_biome = true,
			spread_by_drops_water = true,
			generator_pot_amount = 5,
			grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral),
			grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees),
			grow_type_selector_plants = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants),
			grow_type_selector_bushes = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomBushes),
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"mapleleafus",
				"canadus",
				"malifolia"
			})
		});
		this.t.addSubspeciesTrait("death_grow_tree");
		this.t.addCultureTrait("xenophiles");
		this.t.addLanguageTrait("nicely_structured_grammar");
		this.t.addLanguageTrait("melodic");
		this.t.addUnit("wolf", 1);
		this.t.addUnit("dog", 1);
		this.t.addSapientUnit("civ_wolf", 1);
		this.t.addSapientUnit("civ_dog", 1);
		this.t.addMineral("mineral_stone", 4);
		this.t.addMineral("mineral_metals", 3);
		this.t.addTree("maple_tree", 1);
		this.t.addPlant("maple_plant", 1);
		this.t.addPlant("mushroom_red", 1);
		this.t.addPlant("mushroom_green", 1);
		this.t.addPlant("mushroom_teal", 1);
		this.t.addPlant("mushroom_white", 1);
		this.t.addPlant("mushroom_yellow", 1);
		this.t.addPlant("green_herb", 4);
		this.t.addPlant("flower", 4);
		this.t.addBush("fruit_bush", 1);
		this.add(new BiomeAsset
		{
			id = "biome_rocklands",
			tile_low = "rocklands_low",
			tile_high = "rocklands_high",
			localized_key = "Rocklands",
			grow_strength = 5,
			spread_biome = true,
			generator_pot_amount = 2,
			grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral),
			grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees),
			grow_type_selector_plants = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants),
			grow_type_selector_bushes = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomBushes),
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"lithos",
				"petrae",
				"rupes"
			})
		});
		this.t.addActorTrait("giant");
		this.t.addSubspeciesTrait("bioproduct_stone");
		this.t.addSubspeciesTrait("parental_care");
		this.t.addSubspeciesTraitEvolution("mutation_skin_living_rock");
		this.t.addCultureTrait("statue_lovers");
		this.t.addCultureTrait("training_potential");
		this.t.addCultureTrait("city_layout_stone_garden");
		this.t.addClanTrait("blood_of_giants");
		this.t.addLanguageTrait("powerful_words");
		this.t.addUnit("goat", 1);
		this.t.addUnit("alpaca", 1);
		this.t.addUnit("armadillo", 1);
		this.t.addSapientUnit("dwarf", 1);
		this.t.addSapientUnit("civ_goat", 1);
		this.t.addSapientUnit("civ_alpaca", 1);
		this.t.addSapientUnit("civ_armadillo", 1);
		this.t.addMineral("mineral_stone", 4);
		this.t.addMineral("mineral_metals", 3);
		this.t.addMineral("mineral_gems", 1);
		this.t.addTree("rocklands_tree", 1);
		this.t.addPlant("rocklands_plant", 1);
		this.add(new BiomeAsset
		{
			id = "biome_garlic",
			tile_low = "garlic_low",
			tile_high = "garlic_high",
			localized_key = "Garlic",
			grow_strength = 5,
			spread_biome = true,
			spread_by_drops_water = true,
			generator_pot_amount = 2,
			grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral),
			grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees),
			grow_type_selector_plants = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants),
			grow_type_selector_bushes = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomBushes),
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"aromatica",
				"vampiris",
				"odoratus"
			})
		});
		this.t.addActorTrait("immune");
		this.t.addSubspeciesTrait("accelerated_healing");
		this.t.addCultureTrait("attentive_readers");
		this.t.addClanTrait("masters_of_propaganda");
		this.t.addLanguageTrait("enlightening_script");
		this.t.addUnit("garl", 1);
		this.t.addSapientUnit("civ_garlic_man", 1);
		this.t.addMineral("mineral_stone", 4);
		this.t.addMineral("mineral_metals", 3);
		this.t.addTree("garlic_tree", 1);
		this.t.addPlant("garlic_plant", 1);
		this.t.addBush("fruit_bush", 1);
		this.add(new BiomeAsset
		{
			id = "biome_flower",
			tile_low = "flower_low",
			tile_high = "flower_high",
			localized_key = "Flower",
			grow_strength = 5,
			spread_biome = true,
			spread_by_drops_water = true,
			generator_pot_amount = 3,
			grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral),
			grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees),
			grow_type_selector_plants = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants),
			grow_type_selector_bushes = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomBushes),
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"botanicus",
				"petala",
				"fragrans"
			})
		});
		this.t.addActorTrait("attractive");
		this.t.addSubspeciesTrait("death_grow_plant");
		this.t.addCultureTrait("matriarchy");
		this.t.addCultureTrait("gossip_lovers");
		this.t.addLanguageTrait("elegant_words");
		this.t.addUnit("flower_bud", 1);
		this.t.addUnit("butterfly", 1);
		this.t.addUnit("beetle", 1);
		this.t.addUnit("bee", 1);
		this.t.addUnit("grasshopper", 1);
		this.t.addUnit("fly", 1);
		this.t.addSapientUnit("civ_liliar", 1);
		this.t.addSapientUnit("druid", 1);
		this.t.addMineral("mineral_stone", 4);
		this.t.addMineral("mineral_metals", 3);
		this.t.addTree("flower_tree_1", 1);
		this.t.addTree("flower_tree_2", 1);
		this.t.addTree("flower_tree_3", 1);
		this.t.addPlant("flower_plant", 3);
		this.t.addPlant("mushroom_red", 1);
		this.t.addPlant("mushroom_green", 1);
		this.t.addPlant("mushroom_teal", 1);
		this.t.addPlant("mushroom_white", 1);
		this.t.addPlant("mushroom_yellow", 1);
		this.t.addPlant("green_herb", 5);
		this.t.addPlant("flower", 20);
		this.t.addBush("fruit_bush", 1);
		this.add(new BiomeAsset
		{
			id = "biome_celestial",
			tile_low = "celestial_low",
			tile_high = "celestial_high",
			localized_key = "Celestial",
			grow_strength = 5,
			spread_biome = true,
			generator_pot_amount = 1,
			grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral),
			grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees),
			grow_type_selector_plants = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants),
			grow_type_selector_bushes = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomBushes),
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"caelestis",
				"stellaris",
				"fragrans"
			})
		});
		this.t.addActorTrait("giant");
		this.t.addActorTrait("genius");
		this.t.addSubspeciesTrait("hyper_intelligence");
		this.t.addSubspeciesTraitEvolution("mutation_skin_light_orb");
		this.t.addClanTrait("gods_chosen");
		this.t.addCultureTrait("serenity_now");
		this.t.addCultureTrait("ancestors_knowledge");
		this.t.addLanguageTrait("font_of_gods");
		this.t.addLanguageTrait("magic_words");
		this.t.addReligionTrait("rite_of_falling_stars");
		this.t.addMineral("mineral_stone", 4);
		this.t.addMineral("mineral_metals", 3);
		this.t.addMineral("mineral_mythril", 1);
		this.t.addTree("celestial_tree", 10);
		this.t.addTree("celestial_tree_small", 1);
		this.t.addUnit("unicorn", 1);
		this.t.addSapientUnit("civ_unicorn", 1);
		this.t.addPlant("celestial_plant", 1);
		this.add(new BiomeAsset
		{
			id = "biome_clover",
			tile_low = "clover_low",
			tile_high = "clover_high",
			localized_key = "Clover",
			loot_generation = 3,
			grow_strength = 5,
			spread_biome = true,
			spread_by_drops_water = true,
			generator_pot_amount = 2,
			grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral),
			grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees),
			grow_type_selector_plants = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants),
			grow_type_selector_bushes = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomBushes),
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"luckii",
				"trifolii",
				"prosperus"
			})
		});
		this.t.addActorTrait("lucky");
		this.t.addSubspeciesTrait("death_grow_plant");
		this.t.addCultureTrait("elder_reverence");
		this.t.addLanguageTrait("stylish_writing");
		this.t.addMineral("mineral_stone", 4);
		this.t.addMineral("mineral_metals", 3);
		this.t.addMineral("mineral_gems", 1);
		this.t.addUnit("rabbit", 1);
		this.t.addUnit("cow", 1);
		this.t.addUnit("butterfly", 5);
		this.t.addUnit("bee", 3);
		this.t.addUnit("fairy", 1);
		this.t.addSapientUnit("civ_rabbit", 1);
		this.t.addSapientUnit("civ_cow", 1);
		this.t.addTree("clover_tree", 1);
		this.t.addPlant("clover_plant", 1);
		this.t.addBush("fruit_bush", 1);
		this.add(new BiomeAsset
		{
			id = "biome_singularity",
			tile_low = "singularity_low",
			tile_high = "singularity_high",
			localized_key = "Singularity Swamp",
			loot_generation = 3,
			dark_biome = true,
			grow_strength = 5,
			spread_biome = true,
			spread_by_drops_coffee = true,
			generator_pot_amount = 0,
			grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral),
			grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees),
			grow_type_selector_plants = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants),
			grow_type_selector_bushes = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomBushes),
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"singularis",
				"infinitus",
				"quantumus"
			})
		});
		this.t.addActorTrait("tiny");
		this.t.addActorTrait("long_liver");
		this.t.addSubspeciesTrait("big_stomach");
		this.t.addSubspeciesTraitEvolution("mutation_skin_void");
		this.t.addCultureTrait("ancestors_knowledge");
		this.t.addCultureTrait("gossip_lovers");
		this.t.addLanguageTrait("repeated_sentences");
		this.t.addReligionTrait("teleport");
		this.t.addMineral("mineral_stone", 4);
		this.t.addMineral("mineral_metals", 3);
		this.t.addUnit("angle", 1);
		this.t.addSapientUnit("angle", 1);
		this.t.addTree("singularity_tree", 1);
		this.t.addPlant("singularity_plant", 1);
		this.add(new BiomeAsset
		{
			id = "biome_paradox",
			tile_low = "paradox_low",
			tile_high = "paradox_high",
			localized_key = "Paradox",
			grow_strength = 5,
			spread_biome = true,
			generator_pot_amount = 0,
			grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral),
			grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomTrees),
			grow_type_selector_plants = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomPlants),
			grow_type_selector_bushes = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomBushes),
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"temporis",
				"tempus",
				"chronos"
			})
		});
		this.t.addActorTrait("long_liver");
		this.t.addSubspeciesTrait("rapid_aging");
		this.t.addSubspeciesTrait("long_lifespan");
		this.t.addSubspeciesTraitEvolution("mutation_skin_fractal");
		this.t.addClanTrait("blood_of_eons");
		this.t.addCultureTrait("reading_lovers");
		this.t.addCultureTrait("youth_reverence");
		this.t.addCultureTrait("ancestors_knowledge");
		this.t.addCultureTrait("elder_reverence");
		this.t.addCultureTrait("expansionists");
		this.t.addLanguageTrait("eternal_text");
		this.t.addLanguageTrait("confusing_semantics");
		this.t.addMineral("mineral_stone", 4);
		this.t.addMineral("mineral_metals", 3);
		this.t.addMineral("mineral_mythril", 1);
		this.t.addTree("paradox_tree", 1);
		this.t.addUnit("scorpion", 1);
		this.t.addSapientUnit("alien", 1);
		this.t.addSapientUnit("civ_scorpion", 1);
		this.t.addPlant("paradox_plant", 1);
	}

	// Token: 0x06000207 RID: 519 RVA: 0x00012860 File Offset: 0x00010A60
	private void addSpecialBiomes()
	{
		this.add(new BiomeAsset
		{
			id = "biome_sand",
			localized_key = "Sand",
			grow_type_selector_trees = new GrowTypeSelector(TileActionLibrary.getGrowTypeSand),
			grow_vegetation_auto = true,
			special_biome = true,
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"granitius",
				"arenarius",
				"sabulum"
			})
		});
		this.t.addSubspeciesTrait("metamorphosis_crab");
		this.t.addUnit("crab", 1);
		this.t.addSapientUnit("civ_crab", 1);
		this.add(new BiomeAsset
		{
			id = "biome_hill",
			localized_key = "Hills",
			grow_type_selector_minerals = new GrowTypeSelector(TileActionLibrary.getGrowTypeRandomMineral),
			grow_vegetation_auto = true,
			special_biome = true,
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"hillus",
				"stonus",
				"absolutus"
			})
		});
		this.t.addMineral("mineral_stone", 4);
		this.t.addMineral("mineral_metals", 1);
	}

	// Token: 0x06000208 RID: 520 RVA: 0x0001299C File Offset: 0x00010B9C
	private void addCreepBiomes()
	{
		this.add(new BiomeAsset
		{
			id = "biome_biomass",
			tile_low = "biomass_low",
			tile_high = "biomass_high",
			localized_key = "Biomass",
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"organicus",
				"vivum"
			})
		});
		this.t.addSubspeciesTraitEvolution("mutation_skin_abomination");
		this.add(new BiomeAsset
		{
			id = "biome_cybertile",
			tile_low = "cybertile_low",
			tile_high = "cybertile_high",
			localized_key = "Cybercore",
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"machinus",
				"cyberneticus",
				"technus"
			})
		});
		this.t.addSubspeciesTraitEvolution("mutation_skin_metalic_orb");
		this.add(new BiomeAsset
		{
			id = "biome_pumpkin",
			tile_low = "pumpkin_low",
			tile_high = "pumpkin_high",
			localized_key = "Super Pumpkin",
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"maximus",
				"pepo",
				"cucurbitae"
			})
		});
		this.add(new BiomeAsset
		{
			id = "biome_tumor",
			tile_low = "tumor_low",
			tile_high = "tumor_high",
			localized_key = "Tumor",
			subspecies_name_suffix = AssetLibrary<BiomeAsset>.a<string>(new string[]
			{
				"neoplasmis",
				"cancrosus",
				"tumoralis"
			})
		});
		this.t.addSubspeciesTraitEvolution("mutation_skin_blood_vortex");
	}

	// Token: 0x06000209 RID: 521 RVA: 0x00012B60 File Offset: 0x00010D60
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (BiomeAsset tAsset in this.list)
		{
			this.addBiomeToPool(tAsset);
		}
	}

	// Token: 0x0600020A RID: 522 RVA: 0x00012BBC File Offset: 0x00010DBC
	private void addBiomeToPool(BiomeAsset pAsset)
	{
		for (int i = 0; i < pAsset.generator_pot_amount; i++)
		{
			BiomeLibrary.pool_biomes.Add(pAsset);
		}
	}

	// Token: 0x0600020B RID: 523 RVA: 0x00012BE8 File Offset: 0x00010DE8
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (BiomeAsset tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
			this.checkLocale(tAsset, tAsset.getDescriptionID());
		}
	}

	// Token: 0x0400018A RID: 394
	public static List<BiomeAsset> pool_biomes = new List<BiomeAsset>();
}
