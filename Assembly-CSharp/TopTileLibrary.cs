using System;
using UnityEngine;

// Token: 0x02000085 RID: 133
public class TopTileLibrary : TileLibraryMain<TopTileType>
{
	// Token: 0x0600048B RID: 1163 RVA: 0x0002F518 File Offset: 0x0002D718
	public override void init()
	{
		base.init();
		TopTileLibrary.grass_low = this.add(new TopTileType
		{
			drawPixel = true,
			biome_build_check = true,
			id = "grass_low",
			color_hex = "#7EAF46",
			height_min = 108,
			grass = true,
			nutrition = 30,
			ground = true,
			can_be_farm = true,
			can_build_on = true,
			can_be_set_on_fire = true,
			burnable = true,
			burn_rate = 5,
			strength = 0,
			fire_chance = 0.05f,
			remove_on_freeze = true,
			remove_on_heat = true,
			can_be_biome = true,
			food_resource = "herbs"
		});
		this.t.can_be_removed_with_spade = true;
		this.t.setBiome("biome_grass");
		this.t.setDrawLayer(TileZIndexes.grass_low, null);
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[1]);
		TopTileLibrary.grass_high = this.add(new TopTileType
		{
			cost = 120,
			biome_build_check = true,
			drawPixel = true,
			id = "grass_high",
			color_hex = "#5F833C",
			height_min = 128,
			grass = true,
			nutrition = 30,
			can_be_set_on_fire = true,
			burnable = true,
			burn_rate = 5,
			additional_height = new int[]
			{
				15,
				16,
				17,
				14,
				13,
				12,
				11,
				10
			},
			ground = true,
			can_build_on = true,
			can_be_farm = true,
			fire_chance = 0.06f,
			strength = 0,
			remove_on_freeze = true,
			remove_on_heat = true,
			can_be_biome = true,
			food_resource = "herbs"
		});
		this.t.can_be_removed_with_spade = true;
		this.t.setBiome("biome_grass");
		this.t.used_in_generator = true;
		this.t.setDrawLayer(TileZIndexes.grass_high, null);
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[1]);
		TopTileLibrary.savanna_low = this.clone("savanna_low", "grass_low");
		this.t.color_hex = "#F0B121";
		this.t.setBiome("biome_savanna");
		this.t.setDrawLayer(TileZIndexes.savanna_low, null);
		this.t.fire_chance = 0.06f;
		this.t.burn_rate = 5;
		this.t.food_resource = "wheat";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Savanna
		});
		TopTileLibrary.savanna_high = this.clone("savanna_high", "grass_high");
		this.t.color_hex = "#CF931B";
		this.t.setBiome("biome_savanna");
		this.t.setDrawLayer(TileZIndexes.savanna_high, null);
		this.t.fire_chance = 0.06f;
		this.t.burn_rate = 5;
		this.t.food_resource = "wheat";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Savanna
		});
		TopTileLibrary.enchanted_low = this.clone("enchanted_low", "grass_low");
		this.t.color_hex = "#8CDC6A";
		this.t.setBiome("biome_enchanted");
		this.t.setDrawLayer(TileZIndexes.enchanted_low, null);
		this.t.step_action = new TileStepAction(ActionLibrary.giveEnchanted);
		this.t.fire_chance = 0.03f;
		this.t.burn_rate = 4;
		this.t.food_resource = "herbs";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Enchanted
		});
		TopTileLibrary.enchanted_high = this.clone("enchanted_high", "grass_low");
		this.t.color_hex = "#76B153";
		this.t.setBiome("biome_enchanted");
		this.t.setDrawLayer(TileZIndexes.enchanted_high, null);
		this.t.step_action = new TileStepAction(ActionLibrary.giveEnchanted);
		this.t.fire_chance = 0.04f;
		this.t.burn_rate = 4;
		this.t.food_resource = "herbs";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Enchanted
		});
		TopTileLibrary.mushroom_low = this.clone("mushroom_low", "grass_low");
		this.t.color_hex = "#677642";
		this.t.setBiome("biome_mushroom");
		this.t.setDrawLayer(TileZIndexes.mushroom_low, null);
		this.t.fire_chance = 0.03f;
		this.t.burn_rate = 4;
		this.t.food_resource = "mushrooms";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Mushroom
		});
		TopTileLibrary.mushroom_high = this.clone("mushroom_high", "grass_high");
		this.t.color_hex = "#556338";
		this.t.setBiome("biome_mushroom");
		this.t.setDrawLayer(TileZIndexes.mushroom_high, null);
		this.t.fire_chance = 0.03f;
		this.t.burn_rate = 4;
		this.t.food_resource = "mushrooms";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Mushroom
		});
		TopTileLibrary.birch_low = this.clone("birch_low", "grass_low");
		this.t.color_hex = "#93CF3A";
		this.t.setBiome("biome_birch");
		this.t.setDrawLayer(TileZIndexes.birch_low, null);
		this.t.fire_chance = 0.03f;
		this.t.burn_rate = 4;
		this.t.food_resource = "herbs";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Birch
		});
		TopTileLibrary.birch_high = this.clone("birch_high", "grass_high");
		this.t.color_hex = "#76AC2B";
		this.t.setBiome("biome_birch");
		this.t.setDrawLayer(TileZIndexes.birch_high, null);
		this.t.fire_chance = 0.03f;
		this.t.burn_rate = 4;
		this.t.food_resource = "herbs";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Birch
		});
		TopTileLibrary.maple_low = this.clone("maple_low", "grass_low");
		this.t.color_hex = "#B57F22";
		this.t.setBiome("biome_maple");
		this.t.setDrawLayer(TileZIndexes.maple_low, null);
		this.t.fire_chance = 0.03f;
		this.t.burn_rate = 4;
		this.t.food_resource = "herbs";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Maple
		});
		TopTileLibrary.maple_high = this.clone("maple_high", "grass_high");
		this.t.color_hex = "#A75924";
		this.t.setBiome("biome_maple");
		this.t.setDrawLayer(TileZIndexes.maple_high, null);
		this.t.fire_chance = 0.03f;
		this.t.burn_rate = 4;
		this.t.food_resource = "herbs";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Maple
		});
		TopTileLibrary.rocklands_low = this.clone("rocklands_low", "grass_low");
		this.t.color_hex = "#8D8D8D";
		this.t.setBiome("biome_rocklands");
		this.t.setDrawLayer(TileZIndexes.rocklands_low, null);
		this.t.fire_chance = 0.03f;
		this.t.burn_rate = 3;
		this.t.food_resource = "herbs";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Rocklands
		});
		TopTileLibrary.rocklands_high = this.clone("rocklands_high", "grass_high");
		this.t.color_hex = "#7A7A7A";
		this.t.setBiome("biome_rocklands");
		this.t.setDrawLayer(TileZIndexes.rocklands_high, null);
		this.t.fire_chance = 0.03f;
		this.t.burn_rate = 3;
		this.t.food_resource = "herbs";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Rocklands
		});
		TopTileLibrary.garlic_low = this.clone("garlic_low", "grass_low");
		this.t.color_hex = "#ACA979";
		this.t.setBiome("biome_garlic");
		this.t.setDrawLayer(TileZIndexes.garlic_low, null);
		this.t.fire_chance = 0.03f;
		this.t.burn_rate = 5;
		this.t.food_resource = "herbs";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Garlic
		});
		TopTileLibrary.garlic_high = this.clone("garlic_high", "grass_high");
		this.t.color_hex = "#8C8E65";
		this.t.setBiome("biome_garlic");
		this.t.setDrawLayer(TileZIndexes.garlic_high, null);
		this.t.fire_chance = 0.03f;
		this.t.burn_rate = 5;
		this.t.food_resource = "herbs";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Garlic
		});
		TopTileLibrary.flower_low = this.clone("flower_low", "grass_low");
		this.t.color_hex = "#54CC3A";
		this.t.setBiome("biome_flower");
		this.t.setDrawLayer(TileZIndexes.flower_low, null);
		this.t.fire_chance = 0.03f;
		this.t.burn_rate = 5;
		this.t.food_resource = "herbs";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Flower
		});
		TopTileLibrary.flower_high = this.clone("flower_high", "grass_high");
		this.t.color_hex = "#39A334";
		this.t.setBiome("biome_flower");
		this.t.setDrawLayer(TileZIndexes.flower_high, null);
		this.t.fire_chance = 0.03f;
		this.t.burn_rate = 4;
		this.t.food_resource = "herbs";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Flower
		});
		TopTileLibrary.celestial_low = this.clone("celestial_low", "grass_low");
		this.t.color_hex = "#B573DA";
		this.t.setBiome("biome_celestial");
		this.t.setDrawLayer(TileZIndexes.celestial_low, null);
		this.t.fire_chance = 0.03f;
		this.t.burn_rate = 2;
		this.t.food_resource = "herbs";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Celestial
		});
		TopTileLibrary.celestial_high = this.clone("celestial_high", "grass_high");
		this.t.color_hex = "#7F5DA2";
		this.t.setBiome("biome_celestial");
		this.t.setDrawLayer(TileZIndexes.celestial_high, null);
		this.t.fire_chance = 0.03f;
		this.t.burn_rate = 2;
		this.t.food_resource = "herbs";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Celestial
		});
		TopTileLibrary.singularity_low = this.clone("singularity_low", "grass_low");
		this.t.used_in_generator = false;
		this.t.color_hex = "#623079";
		this.t.setBiome("biome_singularity");
		this.t.setDrawLayer(TileZIndexes.singularity_low, null);
		this.t.fire_chance = 0.03f;
		this.t.burn_rate = 1;
		this.t.food_resource = "herbs";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Singularity
		});
		this.t.burnable = false;
		this.t.step_action_chance = 0.1f;
		this.t.step_action = new TileStepAction(ActionLibrary.singularityTeleportation);
		TopTileLibrary.singularity_high = this.clone("singularity_high", "grass_high");
		this.t.used_in_generator = false;
		this.t.color_hex = "#502263";
		this.t.setBiome("biome_singularity");
		this.t.setDrawLayer(TileZIndexes.singularity_high, null);
		this.t.fire_chance = 0.03f;
		this.t.burn_rate = 1;
		this.t.food_resource = "herbs";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Singularity
		});
		this.t.burnable = false;
		this.t.step_action_chance = 0.1f;
		this.t.step_action = new TileStepAction(ActionLibrary.singularityTeleportation);
		TopTileLibrary.clover_low = this.clone("clover_low", "grass_low");
		this.t.color_hex = "#49AB87";
		this.t.setBiome("biome_clover");
		this.t.setDrawLayer(TileZIndexes.clover_low, null);
		this.t.fire_chance = 0.03f;
		this.t.burn_rate = 4;
		this.t.food_resource = "herbs";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Clover
		});
		TopTileLibrary.clover_high = this.clone("clover_high", "grass_high");
		this.t.color_hex = "#3E8C7B";
		this.t.setBiome("biome_clover");
		this.t.setDrawLayer(TileZIndexes.clover_high, null);
		this.t.fire_chance = 0.03f;
		this.t.burn_rate = 4;
		this.t.food_resource = "herbs";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Clover
		});
		TopTileLibrary.paradox_low = this.clone("paradox_low", "grass_low");
		this.t.used_in_generator = false;
		this.t.color_hex = "#896955";
		this.t.setBiome("biome_paradox");
		this.t.setDrawLayer(TileZIndexes.paradox_low, null);
		this.t.fire_chance = 0.03f;
		this.t.burn_rate = 2;
		this.t.food_resource = "herbs";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Paradox
		});
		this.t.step_action = new TileStepAction(ActionLibrary.timeParadox);
		this.t.step_action_chance = 0.1f;
		TopTileLibrary.paradox_high = this.clone("paradox_high", "grass_high");
		this.t.used_in_generator = false;
		this.t.color_hex = "#745844";
		this.t.setBiome("biome_paradox");
		this.t.setDrawLayer(TileZIndexes.paradox_high, null);
		this.t.fire_chance = 0.03f;
		this.t.burn_rate = 2;
		this.t.food_resource = "herbs";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Paradox
		});
		this.t.step_action = new TileStepAction(ActionLibrary.timeParadox);
		this.t.step_action_chance = 0.1f;
		TopTileLibrary.corruption_low = this.clone("corrupted_low", "grass_low");
		this.t.color_hex = "#6F556C";
		this.t.setBiome("biome_corrupted");
		this.t.setDrawLayer(TileZIndexes.corruption_low, null);
		this.t.unit_death_action = new WorldAction(ActionLibrary.spawnGhost);
		this.t.step_action = new TileStepAction(ActionLibrary.giveCursed);
		this.t.step_action_chance = 0.05f;
		this.t.fire_chance = 0.02f;
		this.t.burn_rate = 1;
		this.t.food_resource = "evil_beets";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Corrupted
		});
		this.t.only_allowed_to_build_with_tag = "can_build_in_biome_corruption";
		TopTileLibrary.corruption_high = this.clone("corrupted_high", "grass_high");
		this.t.color_hex = "#533F51";
		this.t.setBiome("biome_corrupted");
		this.t.setDrawLayer(TileZIndexes.corruption_high, null);
		this.t.unit_death_action = new WorldAction(ActionLibrary.spawnGhost);
		this.t.step_action = new TileStepAction(ActionLibrary.giveCursed);
		this.t.step_action_chance = 0.05f;
		this.t.fire_chance = 0.02f;
		this.t.burn_rate = 1;
		this.t.food_resource = "evil_beets";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Corrupted
		});
		this.t.only_allowed_to_build_with_tag = "can_build_in_biome_corruption";
		TopTileLibrary.infernal_low = this.clone("infernal_low", "grass_low");
		this.t.can_be_set_on_fire_by_burning_feet = false;
		this.t.color_hex = "#9C3626";
		this.t.setBiome("biome_infernal");
		this.t.burnable = false;
		this.t.setDrawLayer(TileZIndexes.infernal_low, null);
		this.t.food_resource = "peppers";
		this.t.hold_lava = true;
		this.t.can_be_frozen = false;
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Infernal
		});
		this.t.only_allowed_to_build_with_tag = "can_build_in_biome_infernal";
		TopTileLibrary.infernal_high = this.clone("infernal_high", "grass_high");
		this.t.can_be_set_on_fire_by_burning_feet = false;
		this.t.color_hex = "#68372D";
		this.t.setBiome("biome_infernal");
		this.t.burnable = false;
		this.t.setDrawLayer(TileZIndexes.infernal_high, null);
		this.t.food_resource = "peppers";
		this.t.hold_lava = true;
		this.t.can_be_frozen = false;
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Infernal
		});
		this.t.only_allowed_to_build_with_tag = "can_build_in_biome_infernal";
		TopTileLibrary.jungle_low = this.clone("jungle_low", "grass_low");
		this.t.color_hex = "#46A052";
		this.t.setBiome("biome_jungle");
		this.t.setDrawLayer(TileZIndexes.jungle_low, null);
		this.t.fire_chance = 0.04f;
		this.t.burn_rate = 5;
		this.t.food_resource = "bananas";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Jungle
		});
		TopTileLibrary.jungle_high = this.clone("jungle_high", "grass_high");
		this.t.color_hex = "#1F7020";
		this.t.setBiome("biome_jungle");
		this.t.setDrawLayer(TileZIndexes.jungle_high, null);
		this.t.fire_chance = 0.05f;
		this.t.burn_rate = 5;
		this.t.food_resource = "bananas";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Jungle
		});
		TopTileLibrary.swamp_low = this.clone("swamp_low", "grass_low");
		this.t.color_hex = "#4D483E";
		this.t.setBiome("biome_swamp");
		this.t.setDrawLayer(TileZIndexes.swamp_low, null);
		this.t.walk_multiplier = 0.6f;
		this.t.food_resource = "herbs";
		this.t.burnable = false;
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Swamp
		});
		this.t.ignore_walk_multiplier_if_tag = "walk_adaptation_swamp";
		this.t.only_allowed_to_build_with_tag = "can_build_in_biome_swamp";
		TopTileLibrary.swamp_high = this.clone("swamp_high", "grass_high");
		this.t.color_hex = "#453E34";
		this.t.setBiome("biome_swamp");
		this.t.setDrawLayer(TileZIndexes.swamp_high, null);
		this.t.walk_multiplier = 0.6f;
		this.t.food_resource = "herbs";
		this.t.burnable = false;
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Swamp
		});
		this.t.ignore_walk_multiplier_if_tag = "walk_adaptation_swamp";
		this.t.only_allowed_to_build_with_tag = "can_build_in_biome_swamp";
		TopTileLibrary.wasteland_low = this.clone("wasteland_low", "grass_low");
		this.t.color_hex = "#849371";
		this.t.setBiome("biome_wasteland");
		this.t.grass = false;
		this.t.wasteland = true;
		this.t.burnable = false;
		this.t.can_be_biome = true;
		this.t.setDrawLayer(TileZIndexes.wasteland_low, null);
		this.t.food_resource = "herbs";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Wasteland
		});
		this.t.only_allowed_to_build_with_tag = "can_build_in_biome_wasteland";
		TopTileLibrary.wasteland_high = this.clone("wasteland_high", "grass_high");
		this.t.color_hex = "#6C7759";
		this.t.setBiome("biome_wasteland");
		this.t.grass = false;
		this.t.wasteland = true;
		this.t.burnable = false;
		this.t.can_be_biome = true;
		this.t.setDrawLayer(TileZIndexes.wasteland_high, null);
		this.t.food_resource = "herbs";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Wasteland
		});
		this.t.only_allowed_to_build_with_tag = "can_build_in_biome_wasteland";
		TopTileLibrary.desert_low = this.clone("desert_low", "grass_low");
		this.t.color_hex = "#E8C76E";
		this.t.setBiome("biome_desert");
		this.t.grass = false;
		this.t.burnable = false;
		this.t.setDrawLayer(TileZIndexes.desert_low, null);
		this.t.walk_multiplier = 0.7f;
		this.t.food_resource = "desert_berries";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Desert
		});
		this.t.ignore_walk_multiplier_if_tag = "walk_adaptation_sand";
		this.t.only_allowed_to_build_with_tag = "can_build_in_biome_desert";
		this.t.step_action = new TileStepAction(ActionLibrary.restoreMana);
		TopTileLibrary.desert_high = this.clone("desert_high", "grass_high");
		this.t.color_hex = "#E1BA5A";
		this.t.setBiome("biome_desert");
		this.t.grass = false;
		this.t.burnable = false;
		this.t.setDrawLayer(TileZIndexes.desert_high, null);
		this.t.walk_multiplier = 0.7f;
		this.t.food_resource = "desert_berries";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Desert
		});
		this.t.ignore_walk_multiplier_if_tag = "walk_adaptation_sand";
		this.t.only_allowed_to_build_with_tag = "can_build_in_biome_desert";
		this.t.step_action = new TileStepAction(ActionLibrary.restoreMana);
		TopTileLibrary.crystal_low = this.clone("crystal_low", "grass_low");
		this.t.color_hex = "#68EADE";
		this.t.setBiome("biome_crystal");
		this.t.grass = false;
		this.t.burnable = false;
		this.t.setDrawLayer(TileZIndexes.crystal_low, null);
		this.t.food_resource = "crystal_salt";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Crystal
		});
		TopTileLibrary.crystal_high = this.clone("crystal_high", "grass_high");
		this.t.color_hex = "#5FD6CB";
		this.t.setBiome("biome_crystal");
		this.t.grass = false;
		this.t.burnable = false;
		this.t.setDrawLayer(TileZIndexes.crystal_high, null);
		this.t.food_resource = "crystal_salt";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Crystal
		});
		TopTileLibrary.candy_low = this.clone("candy_low", "grass_low");
		this.t.color_hex = "#FF96B0";
		this.t.setBiome("biome_candy");
		this.t.setDrawLayer(TileZIndexes.candy_low, null);
		this.t.food_resource = "candy";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Candy
		});
		this.t.burn_rate = 4;
		TopTileLibrary.candy_high = this.clone("candy_high", "grass_high");
		this.t.color_hex = "#FB87A4";
		this.t.setBiome("biome_candy");
		this.t.setDrawLayer(TileZIndexes.candy_high, null);
		this.t.food_resource = "candy";
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Candy
		});
		this.t.burn_rate = 4;
		TopTileLibrary.lemon_low = this.clone("lemon_low", "grass_low");
		this.t.color_hex = "#D1E771";
		this.t.setBiome("biome_lemon");
		this.t.setDrawLayer(TileZIndexes.lemon_low, null);
		this.t.food_resource = "lemons";
		TileTypeBase t = this.t;
		BiomeTag[] array = new BiomeTag[2];
		array[0] = BiomeTag.Lemon;
		t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(array);
		this.t.burn_rate = 2;
		this.t.step_action = new TileStepAction(ActionLibrary.restoreStamina);
		TopTileLibrary.lemon_high = this.clone("lemon_high", "grass_high");
		this.t.color_hex = "#8ACF55";
		this.t.setBiome("biome_lemon");
		this.t.setDrawLayer(TileZIndexes.lemon_high, null);
		this.t.food_resource = "lemons";
		TileTypeBase t2 = this.t;
		BiomeTag[] array2 = new BiomeTag[2];
		array2[0] = BiomeTag.Lemon;
		t2.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(array2);
		this.t.burn_rate = 2;
		this.t.step_action = new TileStepAction(ActionLibrary.restoreStamina);
		TopTileLibrary.water_bomb = this.add(new TopTileType
		{
			cost = 10,
			drawPixel = true,
			id = "water_bomb",
			color_hex = "#6D00CD",
			burnable = false,
			explodable = true,
			explodable_delayed = true,
			explode_range = 9,
			explodable_by_ocean = true,
			ignore_ocean_edge_rendering = true,
			ground = true,
			can_be_filled_with_ocean = true,
			strength = 0,
			can_build_on = true
		});
		this.t.can_be_frozen = false;
		this.t.setDrawLayer(TileZIndexes.water_bomb, null);
		this.t.can_be_removed_with_demolish = true;
		TopTileLibrary.tumor_low = this.add(new TopTileType
		{
			drawPixel = true,
			id = "tumor_low",
			creep = true,
			color_hex = "#EE5183",
			height_min = 108,
			ground = true,
			walk_multiplier = 0.8f,
			burnable = true,
			life = true,
			fire_chance = 1f,
			strength = 0,
			can_build_on = false,
			remove_on_freeze = true
		});
		this.t.can_be_frozen = false;
		this.t.step_action = new TileStepAction(TileActionLibrary.giveTumorTrait);
		this.t.setDrawLayer(TileZIndexes.tumor_low, null);
		this.t.setBiome("biome_tumor");
		TopTileLibrary.tumor_high = this.clone("tumor_high", "tumor_low");
		this.t.can_be_frozen = false;
		this.t.color_hex = "#FE1864";
		this.t.setDrawLayer(TileZIndexes.tumor_high, null);
		this.t.setBiome("biome_tumor");
		TopTileLibrary.biomass_low = this.clone("biomass_low", "tumor_low");
		this.t.can_be_frozen = false;
		this.t.color_hex = "#45C842";
		this.t.step_action = new TileStepAction(TileActionLibrary.giveSlownessStatus);
		TopTileType t3 = this.t;
		t3.step_action = (TileStepAction)Delegate.Combine(t3.step_action, new TileStepAction(TileActionLibrary.giveMadnessTrait));
		this.t.setDrawLayer(TileZIndexes.biomass_low, null);
		this.t.setBiome("biome_biomass");
		this.t.fire_chance = 0.06f;
		TopTileLibrary.biomass_high = this.clone("biomass_high", "tumor_high");
		this.t.can_be_frozen = false;
		this.t.color_hex = "#41A840";
		this.t.step_action = new TileStepAction(TileActionLibrary.giveSlownessStatus);
		TopTileType t4 = this.t;
		t4.step_action = (TileStepAction)Delegate.Combine(t4.step_action, new TileStepAction(TileActionLibrary.giveMadnessTrait));
		this.t.setDrawLayer(TileZIndexes.biomass_high, null);
		this.t.setBiome("biome_biomass");
		this.t.fire_chance = 0.06f;
		TopTileLibrary.pumpkin_low = this.clone("pumpkin_low", "tumor_low");
		this.t.can_be_frozen = false;
		this.t.color_hex = "#8F9339";
		this.t.step_action = new TileStepAction(TileActionLibrary.giveSlownessStatus);
		this.t.step_action_chance = 0.2f;
		this.t.setDrawLayer(TileZIndexes.pumpkin_low, null);
		this.t.setBiome("biome_pumpkin");
		this.t.fire_chance = 0.06f;
		TopTileLibrary.pumpkin_high = this.clone("pumpkin_high", "tumor_high");
		this.t.can_be_frozen = false;
		this.t.color_hex = "#696C02";
		this.t.step_action = new TileStepAction(TileActionLibrary.giveSlownessStatus);
		this.t.step_action_chance = 0.2f;
		this.t.setDrawLayer(TileZIndexes.pumpkin_high, null);
		this.t.setBiome("biome_pumpkin");
		this.t.fire_chance = 0.06f;
		TopTileLibrary.cybertile_low = this.clone("cybertile_low", "tumor_low");
		this.t.can_be_frozen = false;
		this.t.life = false;
		this.t.color_hex = "#9EA6A3";
		this.t.setDrawLayer(TileZIndexes.cybertile_low, null);
		this.t.setBiome("biome_cybertile");
		this.t.burnable = false;
		this.t.can_be_removed_with_demolish = true;
		this.t.step_action = null;
		TopTileLibrary.cybertile_high = this.clone("cybertile_high", "tumor_high");
		this.t.can_be_frozen = false;
		this.t.life = false;
		this.t.color_hex = "#858886";
		this.t.setDrawLayer(TileZIndexes.cybertile_high, null);
		this.t.setBiome("biome_cybertile");
		this.t.burnable = false;
		this.t.can_be_removed_with_demolish = true;
		this.t.step_action = null;
		TopTileLibrary.road = this.add(new TopTileType
		{
			cost = 116,
			drawPixel = true,
			id = "road",
			color_hex = "#C1997C",
			road = true,
			ground = true,
			walk_multiplier = 1.5f,
			can_be_set_on_fire = true,
			can_build_on = true,
			can_be_frozen = false,
			strength = 0,
			check_edge = false
		});
		this.t.setDrawLayer(TileZIndexes.road, null);
		this.t.can_be_removed_with_demolish = true;
		TopTileLibrary.fuse = this.add(new TopTileType
		{
			cost = 10,
			drawPixel = true,
			id = "fuse",
			color_hex = "#834C4C",
			burnable = true,
			burn_rate = 4,
			terraform_after_fire = true,
			ground = true,
			can_build_on = true,
			check_edge = false,
			strength = 0
		});
		this.t.can_be_frozen = false;
		this.t.setDrawLayer(TileZIndexes.fuse, null);
		this.t.can_be_removed_with_demolish = true;
		TopTileLibrary.field = this.add(new TopTileType
		{
			cost = 115,
			drawPixel = true,
			id = "field",
			color_hex = "#A8663A",
			height_min = 108,
			ground = true,
			farm_field = true,
			can_be_frozen = false,
			can_be_removed_with_demolish = true,
			can_be_removed_with_spade = true,
			can_build_on = false,
			can_be_set_on_fire = true,
			burnable = true,
			burn_rate = 4,
			strength = 0,
			fire_chance = 0.4f,
			remove_on_freeze = true,
			check_edge = false,
			remove_on_heat = true
		});
		this.t.setDrawLayer(TileZIndexes.field, null);
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Field
		});
		TopTileLibrary.tnt = this.add(new TopTileType
		{
			cost = 10,
			drawPixel = true,
			id = "tnt",
			color_hex = "#A30000",
			burnable = true,
			explodable = true,
			explodable_delayed = true,
			explode_range = 10,
			ground = true,
			can_build_on = true,
			strength = 0,
			can_errode_to_sand = false
		});
		this.t.can_be_frozen = false;
		this.t.setDrawLayer(TileZIndexes.tnt, null);
		this.t.can_be_removed_with_demolish = true;
		TopTileLibrary.fireworks = this.add(new TopTileType
		{
			cost = 10,
			drawPixel = true,
			id = "fireworks",
			color_hex = "#B43DCC",
			burnable = true,
			burn_rate = 5,
			terraform_after_fire = true,
			ground = true,
			can_build_on = true,
			strength = 0,
			can_errode_to_sand = false
		});
		this.t.can_be_frozen = false;
		this.t.setDrawLayer(TileZIndexes.fireworks, null);
		this.t.can_be_removed_with_demolish = true;
		TopTileLibrary.tnt_timed = this.add(new TopTileType
		{
			cost = 10,
			drawPixel = true,
			id = "tnt_timed",
			color_hex = "#7F0000",
			burnable = true,
			explodable = true,
			explodable_timed = true,
			explodeTimer = 5,
			explode_range = 8,
			ground = true,
			can_build_on = true,
			strength = 0,
			can_errode_to_sand = false
		});
		this.t.can_be_frozen = false;
		this.t.setDrawLayer(TileZIndexes.tnt_timed, null);
		this.t.can_be_removed_with_demolish = true;
		TopTileLibrary.landmine = this.add(new TopTileType
		{
			cost = 10,
			drawPixel = true,
			id = "landmine",
			color_hex = "#990000",
			burnable = true,
			explodable = true,
			explode_range = 3,
			ground = true,
			strength = 0,
			can_errode_to_sand = false
		});
		this.t.can_be_frozen = false;
		this.t.step_action = new TileStepAction(TileActionLibrary.landmine);
		this.t.step_action_chance = 0.9f;
		this.t.setDrawLayer(TileZIndexes.landmine, null);
		this.t.can_be_removed_with_demolish = true;
		TopTileLibrary.frozen_low = this.add(new TopTileType
		{
			id = "frozen_low",
			cost = 10,
			drawPixel = true,
			color_hex = "#BAD5D3",
			edge_color_hex = "#F5F7F6",
			ground = true,
			strength = 0,
			burnable = false,
			can_be_removed_with_spade = true,
			can_be_autotested = false
		});
		this.t.walk_multiplier = 0.8f;
		this.t.can_be_farm = true;
		this.t.can_build_on = true;
		this.t.ignore_walk_multiplier_if_tag = "walk_adaptation_snow";
		this.t.setDrawLayer(TileZIndexes.permafrost_low, null);
		this.setSnow();
		TopTileLibrary.frozen_high = this.clone("frozen_high", "frozen_low");
		this.t.walk_multiplier = 0.8f;
		this.t.can_build_on = true;
		this.t.color_hex = "#D3E4E3";
		this.t.edge_color_hex = "#F5F7F6";
		this.t.can_be_autotested = false;
		this.t.ignore_walk_multiplier_if_tag = "walk_adaptation_snow";
		this.t.setDrawLayer(TileZIndexes.permafrost_high, null);
		this.setSnow();
		TopTileLibrary.permafrost_low = this.clone("permafrost_low", "grass_low");
		this.t.walk_multiplier = 0.8f;
		this.t.setBiome("biome_permafrost");
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Permafrost
		});
		this.t.color_hex = "#99BCDB";
		this.t.setDrawLayer(TileZIndexes.permafrost_low, null);
		this.t.food_resource = string.Empty;
		this.t.can_be_frozen = false;
		this.t.forever_frozen = true;
		this.t.freeze_to_id = string.Empty;
		this.t.burnable = false;
		this.t.ignore_walk_multiplier_if_tag = "walk_adaptation_snow";
		this.t.only_allowed_to_build_with_tag = "can_build_in_biome_permafrost";
		TopTileLibrary.permafrost_high = this.clone("permafrost_high", "grass_high");
		this.t.walk_multiplier = 0.8f;
		this.t.setBiome("biome_permafrost");
		this.t.biome_tags = AssetLibrary<TopTileType>.h<BiomeTag>(new BiomeTag[]
		{
			BiomeTag.Permafrost
		});
		this.t.food_resource = "pine_cones";
		this.t.color_hex = "#B4CFE5";
		this.t.edge_color_hex = "#F5F7F6";
		this.t.forever_frozen = true;
		this.t.can_be_frozen = false;
		this.t.setDrawLayer(TileZIndexes.permafrost_high, null);
		this.t.freeze_to_id = string.Empty;
		this.t.burnable = false;
		this.t.ignore_walk_multiplier_if_tag = "walk_adaptation_snow";
		this.t.only_allowed_to_build_with_tag = "can_build_in_biome_permafrost";
		TopTileLibrary.snow_sand = this.clone("snow_sand", "permafrost_low");
		this.t.walk_multiplier = 0.6f;
		this.t.can_build_on = true;
		this.t.setDrawLayer(TileZIndexes.snow_sand, null);
		this.t.color_hex = "#AFF5F1";
		this.t.ground = true;
		this.t.can_be_autotested = false;
		this.t.ignore_walk_multiplier_if_tag = "walk_adaptation_snow";
		this.setSnow();
		TopTileLibrary.ice = this.clone("ice", "permafrost_low");
		this.t.setDrawLayer(TileZIndexes.ice, null);
		this.t.color_hex = "#A7D6F4";
		this.t.can_build_on = false;
		this.t.can_errode_to_sand = false;
		this.t.damaged_when_walked = true;
		this.t.layer_type = TileLayerType.Ocean;
		this.t.ground = false;
		this.t.liquid = true;
		this.t.ocean = true;
		this.t.can_be_autotested = false;
		this.setSnow();
		TopTileLibrary.snow_hills = this.clone("snow_hills", "permafrost_low");
		this.t.walk_multiplier = 0.6f;
		this.t.color_hex = "#E2EDEC";
		this.t.setDrawLayer(TileZIndexes.snow_hills, null);
		this.t.can_build_on = false;
		this.t.can_be_farm = false;
		this.t.can_be_autotested = false;
		this.t.ignore_walk_multiplier_if_tag = "walk_adaptation_snow";
		this.setSnow();
		TopTileLibrary.snow_block = this.clone("snow_block", "permafrost_low");
		this.t.walk_multiplier = 0.6f;
		this.t.layer_type = TileLayerType.Block;
		this.t.color_hex = "#FCFDFD";
		this.t.block = true;
		this.t.block_height = 3f;
		this.t.rocks = true;
		this.t.edge_mountains = true;
		this.t.mountains = true;
		this.t.ground = false;
		this.t.can_build_on = false;
		this.t.can_be_autotested = false;
		this.t.setDrawLayer(TileZIndexes.snow_block, null);
		this.t.ignore_walk_multiplier_if_tag = "walk_adaptation_snow";
		this.setSnow();
		TopTileLibrary.snow_summit = this.clone("snow_summit", "permafrost_low");
		this.t.layer_type = TileLayerType.Block;
		this.t.color_hex = "#FCFDFD";
		this.t.block = true;
		this.t.block_height = 4f;
		this.t.rocks = true;
		this.t.edge_mountains = true;
		this.t.mountains = true;
		this.t.summit = true;
		this.t.ground = false;
		this.t.can_build_on = false;
		this.t.can_be_autotested = false;
		this.t.setDrawLayer(TileZIndexes.snow_summit, null);
		this.setSnow();
		this.addWalls();
		this.loadTileSprites();
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x00031FE4 File Offset: 0x000301E4
	private void addWalls()
	{
		this.add(new TopTileType
		{
			id = "$wall$",
			layer_type = TileLayerType.Block,
			cost = 10,
			drawPixel = true,
			color_hex = "#ffffff",
			edge_color_hex = "#5A1C32",
			wall = true,
			block = true,
			block_height = 6f,
			can_be_set_on_fire = false,
			strength = 0,
			burnable = false,
			can_be_removed_with_demolish = true,
			allowed_to_be_finger_copied = false,
			can_be_frozen = false
		});
		TopTileLibrary.wall_evil = this.clone("wall_evil", "$wall$");
		this.t.strength = 3;
		this.t.color_hex = "#9B3C4D";
		this.t.setDrawLayer(TileZIndexes.wall_evil, null);
		this.t.life = true;
		TopTileLibrary.wall_order = this.clone("wall_order", "$wall$");
		this.t.strength = 3;
		this.t.setDrawLayer(TileZIndexes.wall_order, null);
		this.t.color_hex = "#A1B1A2";
		this.t.edge_color_hex = "#787F87";
		this.t.can_be_removed_with_pickaxe = true;
		TopTileLibrary.wall_ancient = this.clone("wall_ancient", "$wall$");
		this.t.strength = 4;
		this.t.setDrawLayer(TileZIndexes.wall_ancient, null);
		this.t.color_hex = "#425B78";
		this.t.edge_color_hex = "#3A485E";
		TopTileLibrary.wall_wild = this.clone("wall_wild", "$wall$");
		this.t.strength = 1;
		this.t.setDrawLayer(TileZIndexes.wall_wild, null);
		this.t.color_hex = "#C67D49";
		this.t.edge_color_hex = "#4B2E1B";
		this.t.can_be_removed_with_axe = true;
		this.t.burnable = true;
		this.t.burn_rate = 5;
		TopTileLibrary.wall_iron = this.clone("wall_iron", "$wall$");
		this.t.strength = 2;
		this.t.setDrawLayer(TileZIndexes.wall_iron, null);
		this.t.color_hex = "#597D94";
		this.t.edge_color_hex = "#2E363E";
		TopTileLibrary.wall_green = this.clone("wall_green", "$wall$");
		this.t.strength = 1;
		this.t.setDrawLayer(TileZIndexes.wall_green, null);
		this.t.color_hex = "#65A13F";
		this.t.edge_color_hex = "#3F6B3F";
		this.t.can_be_set_on_fire = true;
		this.t.burnable = true;
		this.t.burn_rate = 5;
		this.t.can_be_removed_with_axe = true;
		this.t.can_be_removed_with_sickle = true;
		TopTileLibrary.wall_light = this.clone("wall_light", "$wall$");
		this.t.strength = 10;
		this.t.setDrawLayer(TileZIndexes.wall_light, null);
		this.t.color_hex = "#FFF349";
		this.t.edge_color_hex = "#E7DC42";
		this.t.animated_wall = true;
		this.t.block_height = 20f;
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x00032325 File Offset: 0x00030525
	private void setSnow()
	{
		this.t.can_be_biome = false;
		this.t.setBiome(null);
		this.t.forever_frozen = false;
		this.t.can_be_frozen = false;
		this.t.can_be_unfrozen = true;
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x00032364 File Offset: 0x00030564
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (TileTypeBase tTileType in this.list)
		{
			if (!string.IsNullOrEmpty(tTileType.biome_id))
			{
				tTileType.biome_asset = AssetManager.biome_library.get(tTileType.biome_id);
			}
		}
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x000323DC File Offset: 0x000305DC
	public override TopTileType add(TopTileType pAsset)
	{
		if (!pAsset.isTemplateAsset())
		{
			pAsset.index_id = TileTypeBase.last_index_id++;
			TileLibrary.array_tiles[pAsset.index_id] = pAsset;
		}
		return base.add(pAsset);
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x00032410 File Offset: 0x00030610
	private void loadTileSprites()
	{
		foreach (TopTileType tType in this.list)
		{
			this.loadSpritesForTile(tType);
		}
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x00032464 File Offset: 0x00030664
	private void loadSpritesForTile(TopTileType pType)
	{
		Sprite[] tSpritesArr = SpriteTextureLoader.getSpriteList("tiles/" + pType.id, false);
		if (tSpritesArr == null || tSpritesArr.Length == 0)
		{
			return;
		}
		pType.sprites = new TileSprites();
		foreach (Sprite tSprite in tSpritesArr)
		{
			pType.sprites.addVariation(tSprite, pType.id);
		}
	}

	// Token: 0x04000495 RID: 1173
	public static TopTileType snow_sand;

	// Token: 0x04000496 RID: 1174
	public static TopTileType permafrost_low;

	// Token: 0x04000497 RID: 1175
	public static TopTileType permafrost_high;

	// Token: 0x04000498 RID: 1176
	public static TopTileType frozen_low;

	// Token: 0x04000499 RID: 1177
	public static TopTileType frozen_high;

	// Token: 0x0400049A RID: 1178
	public static TopTileType snow_hills;

	// Token: 0x0400049B RID: 1179
	public static TopTileType snow_block;

	// Token: 0x0400049C RID: 1180
	public static TopTileType snow_summit;

	// Token: 0x0400049D RID: 1181
	public static TopTileType ice;

	// Token: 0x0400049E RID: 1182
	public static TopTileType landmine;

	// Token: 0x0400049F RID: 1183
	public static TopTileType water_bomb;

	// Token: 0x040004A0 RID: 1184
	public static TopTileType tnt_timed;

	// Token: 0x040004A1 RID: 1185
	public static TopTileType tnt;

	// Token: 0x040004A2 RID: 1186
	public static TopTileType fireworks;

	// Token: 0x040004A3 RID: 1187
	public static TopTileType road;

	// Token: 0x040004A4 RID: 1188
	public static TopTileType field;

	// Token: 0x040004A5 RID: 1189
	public static TopTileType fuse;

	// Token: 0x040004A6 RID: 1190
	public static TopTileType wall_evil;

	// Token: 0x040004A7 RID: 1191
	public static TopTileType wall_order;

	// Token: 0x040004A8 RID: 1192
	public static TopTileType wall_ancient;

	// Token: 0x040004A9 RID: 1193
	public static TopTileType wall_wild;

	// Token: 0x040004AA RID: 1194
	public static TopTileType wall_green;

	// Token: 0x040004AB RID: 1195
	public static TopTileType wall_iron;

	// Token: 0x040004AC RID: 1196
	public static TopTileType wall_light;

	// Token: 0x040004AD RID: 1197
	public static TopTileType tumor_low;

	// Token: 0x040004AE RID: 1198
	public static TopTileType tumor_high;

	// Token: 0x040004AF RID: 1199
	public static TopTileType biomass_low;

	// Token: 0x040004B0 RID: 1200
	public static TopTileType biomass_high;

	// Token: 0x040004B1 RID: 1201
	public static TopTileType pumpkin_low;

	// Token: 0x040004B2 RID: 1202
	public static TopTileType pumpkin_high;

	// Token: 0x040004B3 RID: 1203
	public static TopTileType cybertile_low;

	// Token: 0x040004B4 RID: 1204
	public static TopTileType cybertile_high;

	// Token: 0x040004B5 RID: 1205
	public static TopTileType grass_low;

	// Token: 0x040004B6 RID: 1206
	public static TopTileType grass_high;

	// Token: 0x040004B7 RID: 1207
	public static TopTileType corruption_low;

	// Token: 0x040004B8 RID: 1208
	public static TopTileType corruption_high;

	// Token: 0x040004B9 RID: 1209
	public static TopTileType enchanted_low;

	// Token: 0x040004BA RID: 1210
	public static TopTileType enchanted_high;

	// Token: 0x040004BB RID: 1211
	public static TopTileType mushroom_low;

	// Token: 0x040004BC RID: 1212
	public static TopTileType mushroom_high;

	// Token: 0x040004BD RID: 1213
	public static TopTileType savanna_low;

	// Token: 0x040004BE RID: 1214
	public static TopTileType savanna_high;

	// Token: 0x040004BF RID: 1215
	public static TopTileType jungle_low;

	// Token: 0x040004C0 RID: 1216
	public static TopTileType jungle_high;

	// Token: 0x040004C1 RID: 1217
	public static TopTileType infernal_low;

	// Token: 0x040004C2 RID: 1218
	public static TopTileType infernal_high;

	// Token: 0x040004C3 RID: 1219
	public static TopTileType swamp_low;

	// Token: 0x040004C4 RID: 1220
	public static TopTileType swamp_high;

	// Token: 0x040004C5 RID: 1221
	public static TopTileType wasteland_low;

	// Token: 0x040004C6 RID: 1222
	public static TopTileType wasteland_high;

	// Token: 0x040004C7 RID: 1223
	public static TopTileType desert_low;

	// Token: 0x040004C8 RID: 1224
	public static TopTileType desert_high;

	// Token: 0x040004C9 RID: 1225
	public static TopTileType lemon_low;

	// Token: 0x040004CA RID: 1226
	public static TopTileType lemon_high;

	// Token: 0x040004CB RID: 1227
	public static TopTileType candy_low;

	// Token: 0x040004CC RID: 1228
	public static TopTileType candy_high;

	// Token: 0x040004CD RID: 1229
	public static TopTileType crystal_low;

	// Token: 0x040004CE RID: 1230
	public static TopTileType crystal_high;

	// Token: 0x040004CF RID: 1231
	public static TopTileType birch_low;

	// Token: 0x040004D0 RID: 1232
	public static TopTileType birch_high;

	// Token: 0x040004D1 RID: 1233
	public static TopTileType maple_low;

	// Token: 0x040004D2 RID: 1234
	public static TopTileType maple_high;

	// Token: 0x040004D3 RID: 1235
	public static TopTileType rocklands_low;

	// Token: 0x040004D4 RID: 1236
	public static TopTileType rocklands_high;

	// Token: 0x040004D5 RID: 1237
	public static TopTileType garlic_low;

	// Token: 0x040004D6 RID: 1238
	public static TopTileType garlic_high;

	// Token: 0x040004D7 RID: 1239
	public static TopTileType flower_low;

	// Token: 0x040004D8 RID: 1240
	public static TopTileType flower_high;

	// Token: 0x040004D9 RID: 1241
	public static TopTileType celestial_low;

	// Token: 0x040004DA RID: 1242
	public static TopTileType celestial_high;

	// Token: 0x040004DB RID: 1243
	public static TopTileType singularity_low;

	// Token: 0x040004DC RID: 1244
	public static TopTileType singularity_high;

	// Token: 0x040004DD RID: 1245
	public static TopTileType clover_low;

	// Token: 0x040004DE RID: 1246
	public static TopTileType clover_high;

	// Token: 0x040004DF RID: 1247
	public static TopTileType paradox_low;

	// Token: 0x040004E0 RID: 1248
	public static TopTileType paradox_high;

	// Token: 0x040004E1 RID: 1249
	private const string TEMPLATE_WALL = "$wall$";
}
