using System;
using System.Collections.Generic;

// Token: 0x020001AC RID: 428
public class ResourceLibrary : AssetLibrary<ResourceAsset>
{
	// Token: 0x06000C6F RID: 3183 RVA: 0x000B46FC File Offset: 0x000B28FC
	public override void init()
	{
		base.init();
		this.initTemplates();
		this.initOther();
		this.initStrategic();
		this.initFoodIngredients();
		this.initFood();
		this.initFoodRecipes();
	}

	// Token: 0x06000C70 RID: 3184 RVA: 0x000B4728 File Offset: 0x000B2928
	private void initTemplates()
	{
		this.add(new ResourceAsset
		{
			id = "$TEMPLATE_FOOD$",
			type = ResType.Food,
			path_gameplay_sprite = "bag_food",
			tooltip = "city_resource_food",
			food = true
		});
		this.add(new ResourceAsset
		{
			id = "$TEMPLATE_STRATEGIC_MINERAL$",
			type = ResType.Strategic,
			mineral = true
		});
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_minerals"
		});
	}

	// Token: 0x06000C71 RID: 3185 RVA: 0x000B47B4 File Offset: 0x000B29B4
	private void initFoodIngredients()
	{
		ResourceLibrary.wheat = this.add(new ResourceAsset
		{
			id = "wheat",
			type = ResType.Ingredient_Food,
			path_gameplay_sprite = "wheat",
			path_icon = "iconResWheat",
			supply_bound_give = 100,
			supply_bound_take = 20,
			restore_nutrition = 50,
			restore_health = 0.05f,
			restore_mana = 5,
			restore_stamina = 5,
			favorite_food_chance = 0.1f,
			tastiness = 0.1f,
			restore_happiness = 5
		});
		this.t.food = true;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_crops"
		});
	}

	// Token: 0x06000C72 RID: 3186 RVA: 0x000B4874 File Offset: 0x000B2A74
	private void initFoodRecipes()
	{
		ResourceLibrary.bread = this.clone("bread", "$TEMPLATE_FOOD$");
		this.t.path_icon = "iconResBread";
		this.t.path_gameplay_sprite = "bread";
		this.t.ingredients_amount = 1;
		this.t.restore_nutrition = 100;
		this.t.restore_health = 0.1f;
		this.t.restore_mana = 10;
		this.t.restore_stamina = 10;
		this.t.restore_happiness = 10;
		this.t.give_experience = 10;
		this.t.ingredients = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"wheat"
		});
		this.t.trade_bound = 50;
		this.t.produce_min = 50;
		this.t.maximum = 999;
		this.t.trade_give = 5;
		this.t.tastiness = 0.5f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_crops"
		});
		ResourceLibrary.sushi = this.clone("sushi", "$TEMPLATE_FOOD$");
		this.t.path_icon = "iconResSushi";
		this.t.restore_nutrition = 70;
		this.t.restore_health = 0.2f;
		this.t.restore_mana = 20;
		this.t.restore_stamina = 20;
		this.t.restore_happiness = 20;
		this.t.give_experience = 5;
		this.t.ingredients_amount = 1;
		this.t.ingredients = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"fish",
			"herbs"
		});
		this.t.trade_bound = 50;
		this.t.trade_give = 5;
		this.t.give_trait_id = Toolbox.splitStringIntoArray(new string[]
		{
			"fast#5"
		});
		this.t.give_chance = 0.1f;
		this.t.tastiness = 0.6f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_fish",
			"diet_meat"
		});
		ResourceLibrary.jam = this.clone("jam", "$TEMPLATE_FOOD$");
		this.t.path_icon = "iconResJam";
		this.t.restore_nutrition = 100;
		this.t.restore_health = 0.4f;
		this.t.restore_mana = 40;
		this.t.restore_stamina = 40;
		this.t.restore_happiness = 25;
		this.t.ingredients_amount = 2;
		this.t.give_experience = 15;
		this.t.ingredients = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"berries",
			"herbs"
		});
		this.t.trade_bound = 50;
		this.t.trade_give = 5;
		this.t.give_trait_id = Toolbox.splitStringIntoArray(new string[]
		{
			"regeneration#3"
		});
		this.t.give_chance = 0.1f;
		this.t.tastiness = 0.9f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_fruits",
			"diet_vegetation"
		});
		ResourceLibrary.cider = this.clone("cider", "$TEMPLATE_FOOD$");
		this.t.path_icon = "iconResCider";
		this.t.restore_nutrition = 55;
		this.t.restore_health = 0.25f;
		this.t.restore_mana = 25;
		this.t.restore_stamina = 25;
		this.t.restore_happiness = 20;
		this.t.ingredients_amount = 3;
		this.t.give_experience = 10;
		this.t.ingredients = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"berries"
		});
		this.t.trade_bound = 50;
		this.t.trade_give = 5;
		this.t.tastiness = 0.7f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_fruits"
		});
		ResourceLibrary.ale = this.clone("ale", "$TEMPLATE_FOOD$");
		this.t.path_icon = "iconResAle";
		this.t.restore_nutrition = 45;
		this.t.restore_health = 0.2f;
		this.t.restore_mana = 20;
		this.t.restore_stamina = 20;
		this.t.restore_happiness = 20;
		this.t.ingredients_amount = 3;
		this.t.give_experience = 10;
		this.t.ingredients = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"wheat"
		});
		this.t.trade_bound = 50;
		this.t.trade_give = 5;
		this.t.give_status_id = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"slowness"
		});
		this.t.give_trait_id = Toolbox.splitStringIntoArray(new string[]
		{
			"poison_immune#1"
		});
		this.t.give_chance = 0.4f;
		this.t.tastiness = 0.6f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_crops"
		});
		ResourceLibrary.burger = this.clone("burger", "$TEMPLATE_FOOD$");
		this.t.path_icon = "iconResBurger";
		this.t.restore_nutrition = 100;
		this.t.restore_health = 0.4f;
		this.t.restore_mana = 25;
		this.t.restore_stamina = 35;
		this.t.restore_happiness = 25;
		this.t.ingredients_amount = 1;
		this.t.give_experience = 9;
		this.t.ingredients = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"meat",
			"bread",
			"herbs"
		});
		this.t.trade_bound = 50;
		this.t.trade_give = 5;
		this.t.give_status_id = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"slowness"
		});
		this.t.give_trait_id = Toolbox.splitStringIntoArray(new string[]
		{
			"fat#1"
		});
		this.t.give_chance = 0.05f;
		this.t.tastiness = 0.85f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_meat",
			"diet_crops",
			"diet_vegetation"
		});
		ResourceLibrary.pie = this.clone("pie", "$TEMPLATE_FOOD$");
		this.t.path_icon = "iconResPie";
		this.t.restore_nutrition = 100;
		this.t.restore_health = 0.4f;
		this.t.restore_mana = 35;
		this.t.restore_stamina = 25;
		this.t.restore_happiness = 25;
		this.t.ingredients_amount = 1;
		this.t.give_experience = 10;
		this.t.ingredients = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"meat",
			"wheat"
		});
		this.t.trade_bound = 50;
		this.t.trade_give = 5;
		this.t.give_status_id = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"slowness"
		});
		this.t.give_trait_id = Toolbox.splitStringIntoArray(new string[]
		{
			"fat#1"
		});
		this.t.give_chance = 0.05f;
		this.t.tastiness = 0.9f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_meat",
			"diet_crops"
		});
		ResourceLibrary.tea = this.clone("tea", "$TEMPLATE_FOOD$");
		this.t.restore_nutrition = 20;
		this.t.restore_health = 0.25f;
		this.t.restore_mana = 40;
		this.t.restore_stamina = 10;
		this.t.restore_happiness = 10;
		this.t.give_experience = 10;
		this.t.path_icon = "iconResTea";
		this.t.path_gameplay_sprite = "box_tea";
		this.t.trade_bound = 50;
		this.t.trade_give = 5;
		this.t.ingredients = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"herbs"
		});
		this.t.give_status_id = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"caffeinated"
		});
		this.t.give_chance = 0.1f;
		this.t.tastiness = 0.3f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_vegetation"
		});
	}

	// Token: 0x06000C73 RID: 3187 RVA: 0x000B51A8 File Offset: 0x000B33A8
	private void initFood()
	{
		ResourceLibrary.berries = this.clone("berries", "$TEMPLATE_FOOD$");
		this.t.path_gameplay_sprite = "berries";
		this.t.path_icon = "iconResBerries";
		this.t.restore_nutrition = 30;
		this.t.restore_health = 0.35f;
		this.t.restore_mana = 15;
		this.t.restore_stamina = 10;
		this.t.restore_happiness = 12;
		this.t.give_experience = 5;
		this.t.trade_bound = 50;
		this.t.trade_give = 5;
		this.t.storage_max = 100;
		this.t.tastiness = 0.55f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_fruits"
		});
		ResourceLibrary.bananas = this.clone("bananas", "$TEMPLATE_FOOD$");
		this.t.path_gameplay_sprite = "bananas";
		this.t.path_icon = "iconResBanana";
		this.t.restore_nutrition = 25;
		this.t.restore_health = 0.4f;
		this.t.restore_mana = 15;
		this.t.restore_stamina = 10;
		this.t.restore_happiness = 10;
		this.t.give_experience = 5;
		this.t.trade_bound = 50;
		this.t.trade_give = 5;
		this.t.storage_max = 100;
		this.t.give_trait_id = Toolbox.splitStringIntoArray(new string[]
		{
			"unlucky"
		});
		this.t.give_chance = 0.05f;
		this.t.tastiness = 0.55f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_fruits"
		});
		ResourceLibrary.coconut = this.clone("coconut", "$TEMPLATE_FOOD$");
		this.t.path_icon = "iconResCoconut";
		this.t.path_gameplay_sprite = "coconut";
		this.t.restore_nutrition = 30;
		this.t.restore_health = 0.45f;
		this.t.restore_mana = 15;
		this.t.restore_stamina = 10;
		this.t.restore_happiness = 15;
		this.t.give_experience = 10;
		this.t.trade_bound = 50;
		this.t.trade_give = 5;
		this.t.storage_max = 100;
		this.t.give_status_id = Toolbox.splitStringIntoArray(new string[]
		{
			"shield#1"
		});
		this.t.give_chance = 0.1f;
		this.t.tastiness = 0.55f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_fruits"
		});
		ResourceLibrary.crystal_salt = this.clone("crystal_salt", "$TEMPLATE_FOOD$");
		this.t.drop_per_mass = 150;
		this.t.path_icon = "iconResCrystalSalt";
		this.t.path_gameplay_sprite = "crystal_salt";
		this.t.restore_nutrition = 20;
		this.t.restore_health = 0.2f;
		this.t.restore_mana = 20;
		this.t.restore_stamina = 10;
		this.t.restore_happiness = 10;
		this.t.give_experience = 5;
		this.t.trade_bound = 50;
		this.t.trade_give = 5;
		this.t.storage_max = 100;
		this.t.give_status_id = Toolbox.splitStringIntoArray(new string[]
		{
			"caffeinated#3",
			"slowness#6"
		});
		this.t.give_trait_id = Toolbox.splitStringIntoArray(new string[]
		{
			"madness#10",
			"strong_minded#1"
		});
		this.t.give_chance = 0.1f;
		this.t.tastiness = 0.2f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_minerals"
		});
		ResourceLibrary.desert_berries = this.clone("desert_berries", "$TEMPLATE_FOOD$");
		this.t.path_icon = "iconResDesertBerry";
		this.t.path_gameplay_sprite = "desert_berries";
		this.t.restore_nutrition = 25;
		this.t.restore_health = 0.35f;
		this.t.restore_mana = 15;
		this.t.restore_stamina = 10;
		this.t.restore_happiness = 12;
		this.t.give_experience = 5;
		this.t.trade_bound = 50;
		this.t.trade_give = 5;
		this.t.storage_max = 100;
		this.t.give_status_id = Toolbox.splitStringIntoArray(new string[]
		{
			"poisoned#2",
			"slowness#2"
		});
		this.t.give_trait_id = Toolbox.splitStringIntoArray(new string[]
		{
			"poison_immune#1"
		});
		this.t.give_chance = 0.1f;
		this.t.tastiness = 0.55f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_fruits"
		});
		ResourceLibrary.evil_beets = this.clone("evil_beets", "$TEMPLATE_FOOD$");
		this.t.path_icon = "iconResEvilBeets";
		this.t.path_gameplay_sprite = "evil_beets";
		this.t.restore_nutrition = 25;
		this.t.restore_health = 0.4f;
		this.t.restore_mana = 15;
		this.t.restore_stamina = 25;
		this.t.restore_happiness = 15;
		this.t.give_experience = 5;
		this.t.trade_bound = 50;
		this.t.trade_give = 5;
		this.t.storage_max = 100;
		this.t.give_trait_id = Toolbox.splitStringIntoArray(new string[]
		{
			"pyromaniac#1",
			"evil#3"
		});
		this.t.give_chance = 0.1f;
		this.t.tastiness = 0.1f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_vegetation"
		});
		ResourceLibrary.mushrooms = this.clone("mushrooms", "$TEMPLATE_FOOD$");
		this.t.drop_per_mass = 40;
		this.t.path_gameplay_sprite = "mushrooms";
		this.t.path_icon = "iconResMushrooms";
		this.t.restore_nutrition = 20;
		this.t.restore_health = 0.4f;
		this.t.restore_mana = 15;
		this.t.restore_stamina = 15;
		this.t.restore_happiness = 15;
		this.t.give_experience = 5;
		this.t.trade_bound = 50;
		this.t.trade_give = 5;
		this.t.storage_max = 100;
		this.t.give_status_id = Toolbox.splitStringIntoArray(new string[]
		{
			"powerup#10",
			"slowness#3"
		});
		this.t.give_trait_id = Toolbox.splitStringIntoArray(new string[]
		{
			"madness#1",
			"strong_minded#1",
			"paranoid#5",
			"content#10"
		});
		this.t.give_chance = 0.1f;
		this.t.tastiness = 0.2f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_vegetation"
		});
		ResourceLibrary.peppers = this.clone("peppers", "$TEMPLATE_FOOD$");
		this.t.drop_per_mass = 40;
		this.t.path_icon = "iconResPeppers";
		this.t.path_gameplay_sprite = "peppers";
		this.t.restore_nutrition = 30;
		this.t.restore_health = 0.4f;
		this.t.restore_mana = 25;
		this.t.restore_stamina = 15;
		this.t.restore_happiness = 20;
		this.t.give_experience = 5;
		this.t.trade_bound = 50;
		this.t.trade_give = 5;
		this.t.storage_max = 100;
		this.t.give_status_id = Toolbox.splitStringIntoArray(new string[]
		{
			"burning"
		});
		this.t.give_trait_id = Toolbox.splitStringIntoArray(new string[]
		{
			"fire_proof#5",
			"fire_blood#5"
		});
		this.t.give_chance = 0.1f;
		this.t.tastiness = 0.3f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_vegetation"
		});
		ResourceLibrary.herbs = this.clone("herbs", "$TEMPLATE_FOOD$");
		this.t.path_icon = "iconResHerbs";
		this.t.path_gameplay_sprite = "herbs";
		this.t.restore_nutrition = 25;
		this.t.restore_health = 0.5f;
		this.t.restore_mana = 25;
		this.t.restore_stamina = 10;
		this.t.restore_happiness = 5;
		this.t.give_experience = 5;
		this.t.trade_bound = 30;
		this.t.trade_give = 10;
		this.t.storage_max = 100;
		this.t.tastiness = 0.1f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_vegetation"
		});
		ResourceLibrary.fish = this.clone("fish", "$TEMPLATE_FOOD$");
		this.t.path_gameplay_sprite = "fish";
		this.t.restore_nutrition = 50;
		this.t.restore_health = 0.3f;
		this.t.restore_mana = 25;
		this.t.restore_stamina = 10;
		this.t.restore_happiness = 15;
		this.t.path_icon = "iconResFish";
		this.t.give_experience = 5;
		this.t.trade_bound = 50;
		this.t.trade_give = 5;
		this.t.give_trait_id = Toolbox.splitStringIntoArray(new string[]
		{
			"tough#5",
			"strong#5"
		});
		this.t.give_chance = 0.1f;
		this.t.tastiness = 0.25f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_fish",
			"diet_meat"
		});
		ResourceLibrary.candy = this.clone("candy", "$TEMPLATE_FOOD$");
		this.t.drop_per_mass = 50;
		this.t.path_icon = "iconResCandy";
		this.t.path_gameplay_sprite = "candy";
		this.t.restore_nutrition = 40;
		this.t.restore_health = 0.4f;
		this.t.restore_mana = 25;
		this.t.restore_stamina = 10;
		this.t.restore_happiness = 40;
		this.t.give_experience = 5;
		this.t.trade_bound = 50;
		this.t.trade_give = 5;
		this.t.give_trait_id = Toolbox.splitStringIntoArray(new string[]
		{
			"fat#10",
			"tiny#5",
			"giant#5",
			"bloodlust#1"
		});
		this.t.give_chance = 0.1f;
		this.t.tastiness = 1f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_fruits"
		});
		ResourceLibrary.worms = this.clone("worms", "$TEMPLATE_FOOD$");
		this.t.path_icon = "iconResWorms";
		this.t.path_gameplay_sprite = "worms";
		this.t.restore_nutrition = 10;
		this.t.restore_health = 0.2f;
		this.t.restore_mana = 10;
		this.t.restore_stamina = 10;
		this.t.restore_happiness = -5;
		this.t.give_experience = 5;
		this.t.trade_bound = 50;
		this.t.trade_give = 5;
		this.t.tastiness = 0.1f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_meat"
		});
		ResourceLibrary.snow_cucumbers = this.clone("snow_cucumbers", "$TEMPLATE_FOOD$");
		this.t.path_icon = "iconResSnowCucumbers";
		this.t.path_gameplay_sprite = "snow_cucumbers";
		this.t.restore_nutrition = 30;
		this.t.restore_health = 0.35f;
		this.t.restore_mana = 25;
		this.t.restore_stamina = 10;
		this.t.restore_happiness = 10;
		this.t.give_experience = 5;
		this.t.trade_bound = 50;
		this.t.trade_give = 5;
		this.t.give_status_id = Toolbox.splitStringIntoArray(new string[]
		{
			"frozen"
		});
		this.t.give_trait_id = Toolbox.splitStringIntoArray(new string[]
		{
			"freeze_proof#1"
		});
		this.t.give_chance = 0.1f;
		this.t.tastiness = 0.2f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_vegetation"
		});
		ResourceLibrary.celestial_avocado = this.clone("celestial_avocado", "$TEMPLATE_FOOD$");
		this.t.path_icon = "iconResCelestialAvocado";
		this.t.path_gameplay_sprite = "celestial_avocado";
		this.t.restore_nutrition = 100;
		this.t.restore_health = 0.55f;
		this.t.restore_mana = 50;
		this.t.restore_stamina = 50;
		this.t.restore_happiness = 50;
		this.t.give_experience = 10;
		this.t.trade_bound = 50;
		this.t.trade_give = 5;
		this.t.give_trait_id = Toolbox.splitStringIntoArray(new string[]
		{
			"sunblessed#1"
		});
		this.t.give_chance = 0.1f;
		this.t.tastiness = 1.1f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_fruits"
		});
		ResourceLibrary.pine_cones = this.clone("pine_cones", "$TEMPLATE_FOOD$");
		this.t.path_icon = "iconResPineCones";
		this.t.path_gameplay_sprite = "pine_cones";
		this.t.restore_nutrition = 15;
		this.t.restore_health = 0.15f;
		this.t.restore_mana = 10;
		this.t.restore_stamina = 10;
		this.t.restore_happiness = 10;
		this.t.give_experience = 5;
		this.t.trade_bound = 50;
		this.t.trade_give = 5;
		this.t.give_status_id = Toolbox.splitStringIntoArray(new string[]
		{
			"frozen"
		});
		this.t.give_trait_id = Toolbox.splitStringIntoArray(new string[]
		{
			"freeze_proof#10",
			"tough#1",
			"strong#1",
			"regeneration#1"
		});
		this.t.give_chance = 0.1f;
		this.t.tastiness = 0.1f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_crops"
		});
		ResourceLibrary.lemons = this.clone("lemons", "$TEMPLATE_FOOD$");
		this.t.path_icon = "iconResLemons";
		this.t.path_gameplay_sprite = "lemons";
		this.t.restore_nutrition = 20;
		this.t.restore_health = 0.3f;
		this.t.restore_mana = 10;
		this.t.restore_stamina = 10;
		this.t.restore_happiness = 10;
		this.t.give_experience = 5;
		this.t.trade_bound = 50;
		this.t.trade_give = 5;
		this.t.give_trait_id = Toolbox.splitStringIntoArray(new string[]
		{
			"eagle_eyed#5",
			"regeneration#3"
		});
		this.t.give_chance = 0.1f;
		this.t.tastiness = 0.2f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_fruits"
		});
		ResourceLibrary.meat = this.clone("meat", "$TEMPLATE_FOOD$");
		this.t.drop_per_mass = 50;
		this.t.restore_nutrition = 60;
		this.t.restore_health = 0.5f;
		this.t.restore_mana = 10;
		this.t.restore_stamina = 25;
		this.t.restore_happiness = 20;
		this.t.path_icon = "iconResMeat";
		this.t.path_gameplay_sprite = "meat";
		this.t.give_experience = 5;
		this.t.trade_bound = 50;
		this.t.trade_give = 5;
		this.t.give_trait_id = Toolbox.splitStringIntoArray(new string[]
		{
			"tough#5",
			"strong#5"
		});
		this.t.give_chance = 0.1f;
		this.t.tastiness = 0.5f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_meat"
		});
		ResourceLibrary.honey = this.clone("honey", "$TEMPLATE_FOOD$");
		this.t.path_icon = "iconResHoney";
		this.t.path_gameplay_sprite = "honey";
		this.t.restore_nutrition = 15;
		this.t.restore_health = 0.45f;
		this.t.restore_mana = 10;
		this.t.restore_stamina = 25;
		this.t.restore_happiness = 35;
		this.t.give_experience = 10;
		this.t.trade_bound = 50;
		this.t.trade_give = 10;
		this.t.storage_max = 100;
		this.t.tastiness = 0.5f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_fruits",
			"diet_flowers"
		});
	}

	// Token: 0x06000C74 RID: 3188 RVA: 0x000B64B0 File Offset: 0x000B46B0
	private void initStrategic()
	{
		ResourceLibrary.wood = this.add(new ResourceAsset
		{
			id = "wood",
			path_icon = "iconResWood",
			path_gameplay_sprite = "wood",
			type = ResType.Strategic,
			wood = true
		});
		this.t.restore_nutrition = 50;
		this.t.restore_health = 0.45f;
		this.t.restore_mana = 15;
		this.t.restore_stamina = 15;
		this.t.restore_happiness = 10;
		this.t.tastiness = 0.2f;
		this.t.diet = AssetLibrary<ResourceAsset>.a<string>(new string[]
		{
			"diet_wood"
		});
		ResourceLibrary.stone = this.clone("stone", "$TEMPLATE_STRATEGIC_MINERAL$");
		this.t.drop_per_mass = 150;
		this.t.path_icon = "iconResStone";
		this.t.path_gameplay_sprite = "stone";
		this.t.mine_rate = 25;
		this.t.restore_nutrition = 20;
		this.t.restore_health = 0.2f;
		this.t.restore_mana = 10;
		this.t.restore_stamina = 10;
		this.t.restore_happiness = 5;
		this.t.tastiness = 0.1f;
		this.clone("silver", "$TEMPLATE_STRATEGIC_MINERAL$");
		this.t.path_icon = "iconResSilver";
		this.t.path_gameplay_sprite = "silver";
		this.t.restore_nutrition = 35;
		this.t.restore_health = 0.3f;
		this.t.restore_mana = 25;
		this.t.restore_stamina = 15;
		this.t.restore_happiness = 20;
		this.t.tastiness = 0.4f;
		this.clone("mythril", "$TEMPLATE_STRATEGIC_MINERAL$");
		this.t.path_icon = "iconResMythril";
		this.t.path_gameplay_sprite = "mythril";
		this.t.restore_nutrition = 45;
		this.t.restore_health = 0.4f;
		this.t.restore_mana = 30;
		this.t.restore_stamina = 25;
		this.t.restore_happiness = 25;
		this.t.tastiness = 0.6f;
		this.clone("adamantine", "$TEMPLATE_STRATEGIC_MINERAL$");
		this.t.path_icon = "iconResAdamantine";
		this.t.path_gameplay_sprite = "adamantine";
		this.t.restore_nutrition = 55;
		this.t.restore_health = 0.5f;
		this.t.restore_mana = 45;
		this.t.restore_stamina = 40;
		this.t.restore_happiness = 35;
		this.t.tastiness = 0.8f;
		this.add(new ResourceAsset
		{
			id = "dragon_scales",
			path_icon = "iconResDragonScales",
			path_gameplay_sprite = "dragon_scales",
			type = ResType.Strategic
		});
		ResourceLibrary.common_metals = this.clone("common_metals", "$TEMPLATE_STRATEGIC_MINERAL$");
		this.t.path_icon = "iconResCommonMetals";
		this.t.path_gameplay_sprite = "common_metals";
		this.t.storage_max = 100;
		this.t.restore_nutrition = 35;
		this.t.restore_health = 0.3f;
		this.t.restore_mana = 15;
		this.t.restore_stamina = 15;
		this.t.restore_happiness = 10;
		this.t.tastiness = 0.2f;
		this.clone("bones", "$TEMPLATE_STRATEGIC_MINERAL$");
		this.t.drop_per_mass = 100;
		this.t.path_icon = "iconResBones";
		this.t.path_gameplay_sprite = "bones";
		this.t.tastiness = 0.3f;
		this.add(new ResourceAsset
		{
			id = "leather",
			path_icon = "iconResLeather",
			path_gameplay_sprite = "leather",
			type = ResType.Strategic,
			drop_per_mass = 100
		});
		this.clone("gems", "$TEMPLATE_STRATEGIC_MINERAL$");
		this.t.drop_per_mass = 200;
		this.t.path_icon = "iconResGems";
		this.t.path_gameplay_sprite = "gems";
		this.t.mine_rate = 1;
		this.t.restore_nutrition = 35;
		this.t.restore_health = 0.3f;
		this.t.restore_mana = 15;
		this.t.restore_stamina = 25;
		this.t.restore_happiness = 15;
		this.t.tastiness = 1f;
		ResourceLibrary.fertilizer = this.add(new ResourceAsset
		{
			id = "fertilizer",
			path_icon = "iconFertilizer",
			path_gameplay_sprite = "fertilizer",
			type = ResType.Strategic
		});
	}

	// Token: 0x06000C75 RID: 3189 RVA: 0x000B69D0 File Offset: 0x000B4BD0
	private void initOther()
	{
		ResourceLibrary.gold = this.add(new ResourceAsset
		{
			id = "gold",
			path_icon = "iconResGold",
			path_gameplay_sprite = "gold",
			maximum = 999,
			supply_bound_give = 600,
			supply_bound_take = 10,
			supply_give = 100,
			type = ResType.Currency
		});
	}

	// Token: 0x06000C76 RID: 3190 RVA: 0x000B6A3C File Offset: 0x000B4C3C
	public override void post_init()
	{
		base.post_init();
		int i = 0;
		foreach (ResourceAsset tAsset in this.list)
		{
			tAsset.order = i++;
			tAsset.full_sprite_path = "items/resources/" + tAsset.path_gameplay_sprite;
		}
	}

	// Token: 0x06000C77 RID: 3191 RVA: 0x000B6AB4 File Offset: 0x000B4CB4
	public void loadSprites()
	{
		foreach (ResourceAsset resourceAsset in this.list)
		{
			resourceAsset.gameplay_sprites = SpriteTextureLoader.getSpriteList(resourceAsset.full_sprite_path, false);
		}
	}

	// Token: 0x06000C78 RID: 3192 RVA: 0x000B6B10 File Offset: 0x000B4D10
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (ResourceAsset tAsset in this.list)
		{
			if (tAsset.type == ResType.Strategic)
			{
				this.strategic_resource_assets.Add(tAsset);
			}
			if (tAsset.give_trait_id != null)
			{
				tAsset.give_trait = new ActorTrait[tAsset.give_trait_id.Length];
				for (int i = 0; i < tAsset.give_trait_id.Length; i++)
				{
					string tTraitID = tAsset.give_trait_id[i];
					tAsset.give_trait[i] = AssetManager.traits.get(tTraitID);
				}
			}
			if (tAsset.give_status_id != null)
			{
				tAsset.give_status = new StatusAsset[tAsset.give_status_id.Length];
				for (int j = 0; j < tAsset.give_status_id.Length; j++)
				{
					string tTraitID2 = tAsset.give_status_id[j];
					tAsset.give_status[j] = AssetManager.status.get(tTraitID2);
				}
			}
			if (tAsset.diet != null)
			{
				foreach (SubspeciesTrait tTrait in AssetManager.subspecies_traits.list)
				{
					if (tTrait.is_diet_related)
					{
						string tTraitId = tTrait.id;
						foreach (string tDietTag in tAsset.diet)
						{
							if (tTrait.base_stats_meta.hasTag(tDietTag))
							{
								List<string> tFoods;
								if (!this.diet_food_pools.TryGetValue(tTraitId, out tFoods))
								{
									tFoods = new List<string>();
									this.diet_food_pools.Add(tTraitId, tFoods);
								}
								tFoods.Add(tAsset.id);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000C79 RID: 3193 RVA: 0x000B6CFC File Offset: 0x000B4EFC
	public override void editorDiagnostic()
	{
		foreach (ResourceAsset tAsset in this.list)
		{
			if (!tAsset.isTemplateAsset())
			{
				base.checkSpriteExists("path_gameplay_sprite", tAsset.full_sprite_path, tAsset);
			}
		}
		base.editorDiagnostic();
	}

	// Token: 0x06000C7A RID: 3194 RVA: 0x000B6D6C File Offset: 0x000B4F6C
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (ResourceAsset tAsset in this.list)
		{
			if (!tAsset.isTemplateAsset())
			{
				this.checkLocale(tAsset, tAsset.getLocaleID());
			}
		}
	}

	// Token: 0x04000BC0 RID: 3008
	private const string TEMPLATE_FOOD = "$TEMPLATE_FOOD$";

	// Token: 0x04000BC1 RID: 3009
	private const string TEMPLATE_STRATEGIC_MINERAL = "$TEMPLATE_STRATEGIC_MINERAL$";

	// Token: 0x04000BC2 RID: 3010
	public static ResourceAsset herbs;

	// Token: 0x04000BC3 RID: 3011
	public static ResourceAsset mushrooms;

	// Token: 0x04000BC4 RID: 3012
	public static ResourceAsset desert_berries;

	// Token: 0x04000BC5 RID: 3013
	public static ResourceAsset berries;

	// Token: 0x04000BC6 RID: 3014
	public static ResourceAsset peppers;

	// Token: 0x04000BC7 RID: 3015
	public static ResourceAsset bananas;

	// Token: 0x04000BC8 RID: 3016
	public static ResourceAsset crystal_salt;

	// Token: 0x04000BC9 RID: 3017
	public static ResourceAsset coconut;

	// Token: 0x04000BCA RID: 3018
	public static ResourceAsset evil_beets;

	// Token: 0x04000BCB RID: 3019
	public static ResourceAsset lemons;

	// Token: 0x04000BCC RID: 3020
	public static ResourceAsset meat;

	// Token: 0x04000BCD RID: 3021
	public static ResourceAsset sushi;

	// Token: 0x04000BCE RID: 3022
	public static ResourceAsset jam;

	// Token: 0x04000BCF RID: 3023
	public static ResourceAsset burger;

	// Token: 0x04000BD0 RID: 3024
	public static ResourceAsset cider;

	// Token: 0x04000BD1 RID: 3025
	public static ResourceAsset ale;

	// Token: 0x04000BD2 RID: 3026
	public static ResourceAsset honey;

	// Token: 0x04000BD3 RID: 3027
	public static ResourceAsset tea;

	// Token: 0x04000BD4 RID: 3028
	public static ResourceAsset pie;

	// Token: 0x04000BD5 RID: 3029
	public static ResourceAsset wheat;

	// Token: 0x04000BD6 RID: 3030
	public static ResourceAsset bread;

	// Token: 0x04000BD7 RID: 3031
	public static ResourceAsset fish;

	// Token: 0x04000BD8 RID: 3032
	public static ResourceAsset candy;

	// Token: 0x04000BD9 RID: 3033
	public static ResourceAsset worms;

	// Token: 0x04000BDA RID: 3034
	public static ResourceAsset pine_cones;

	// Token: 0x04000BDB RID: 3035
	public static ResourceAsset snow_cucumbers;

	// Token: 0x04000BDC RID: 3036
	public static ResourceAsset celestial_avocado;

	// Token: 0x04000BDD RID: 3037
	public static ResourceAsset wood;

	// Token: 0x04000BDE RID: 3038
	public static ResourceAsset stone;

	// Token: 0x04000BDF RID: 3039
	public static ResourceAsset common_metals;

	// Token: 0x04000BE0 RID: 3040
	public static ResourceAsset gold;

	// Token: 0x04000BE1 RID: 3041
	public static ResourceAsset fertilizer;

	// Token: 0x04000BE2 RID: 3042
	[NonSerialized]
	public List<ResourceAsset> strategic_resource_assets = new List<ResourceAsset>();

	// Token: 0x04000BE3 RID: 3043
	[NonSerialized]
	public Dictionary<string, List<string>> diet_food_pools = new Dictionary<string, List<string>>();
}
