using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityPools;

// Token: 0x0200011C RID: 284
public class KingdomLibrary : AssetLibrary<KingdomAsset>
{
	// Token: 0x060008BF RID: 2239 RVA: 0x0007DA30 File Offset: 0x0007BC30
	public override void init()
	{
		base.init();
		this._shared_default_color = ColorAsset.tryMakeNewColorAsset("#888888");
		this._shared_default_color.id = "SHARED_COLOR";
		this.addTemplates();
		this.addNeutral();
		this.addNomads();
		this.addNewCivs();
		this.addCivs();
		this.addAnimals();
		this.addUnique();
		this.addMobs();
		this.addAnimalMinicivs();
		this.addCoolMinicivs();
		this.addCreeps();
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x0007DAA8 File Offset: 0x0007BCA8
	private void addTemplates()
	{
		this.add(new KingdomAsset
		{
			id = "$TEMPLATE_MOB$",
			mobs = true
		});
		this.clone("$TEMPLATE_MOB_GOOD$", "$TEMPLATE_MOB$");
		this.t.addTag("good");
		this.t.addFriendlyTag("good");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("civ");
		this.t.addEnemyTag("orc");
		this.t.addEnemyTag("bandit");
		this.clone("$TEMPLATE_MOB_VERY_GOOD$", "$TEMPLATE_MOB_GOOD$");
		this.t.addFriendlyTag("nature_creature");
		this.t.addFriendlyTag("living_houses");
		this.t.addFriendlyTag("living_plants");
		this.t.addFriendlyTag("snowman");
		this.t.addEnemyTag("wolf");
		this.t.addEnemyTag("bear");
		this.clone("$TEMPLATE_ANIMAL$", "$TEMPLATE_MOB$");
		this.t.addTag("nature_creature");
		this.t.addFriendlyTag("nature_creature");
		this.t.addFriendlyTag("neutral_animals");
		this.clone("$TEMPLATE_ANIMAL_NEUTRAL$", "$TEMPLATE_MOB$");
		this.t.count_as_danger = false;
		this.t.addTag("neutral_animals");
		this.t.addTag("neutral");
		this.clone("$TEMPLATE_ANIMAL_PEACEFUL$", "$TEMPLATE_ANIMAL_NEUTRAL$");
		this.t.addFriendlyTag("good");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("nature_creature");
		this.t.addFriendlyTag("civ");
		this.add(new KingdomAsset
		{
			id = "$TEMPLATE_CIV$",
			civ = true
		});
		this.t.addTag("civ");
		this.t.addEnemyTag("bandit");
		this.clone("$TEMPLATE_CIV_GOOD$", "$TEMPLATE_CIV$");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("good");
		this.clone("$TEMPLATE_NOMAD$", "$TEMPLATE_CIV_GOOD$");
		this.t.addFriendlyTag("neutral");
		this.t.civ = false;
		this.t.mobs = true;
		this.t.nomads = true;
		this.clone("$TEMPLATE_CIV_NEW$", "$TEMPLATE_CIV_GOOD$");
		this.t.addFriendlyTag("neutral");
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x0007DD5C File Offset: 0x0007BF5C
	private void addNomads()
	{
		this.clone("nomads_human", "$TEMPLATE_NOMAD$");
		this.t.group_main = true;
		this.t.default_kingdom_color = ColorAsset.tryMakeNewColorAsset("#BACADD");
		this.t.setIcon("ui/Icons/iconHumans");
		this.t.addTag("human");
		this.t.addTag("sliceable");
		this.t.addFriendlyTag("human");
		this.clone("nomads_elf", "$TEMPLATE_NOMAD$");
		this.t.group_main = true;
		this.t.default_kingdom_color = ColorAsset.tryMakeNewColorAsset("#98DB8C");
		this.t.setIcon("ui/Icons/iconElves");
		this.t.addTag("elf");
		this.t.addTag("nature_creature");
		this.t.addTag("sliceable");
		this.t.addFriendlyTag("elf");
		this.t.addFriendlyTag("nature_creature");
		this.clone("nomads_orc", "$TEMPLATE_NOMAD$");
		this.t.group_main = true;
		this.t.default_kingdom_color = ColorAsset.tryMakeNewColorAsset("#FFCD70");
		this.t.setIcon("ui/Icons/iconOrcs");
		this.t.civ = false;
		this.t.mobs = true;
		this.t.addTag("orc");
		this.t.addTag("sliceable");
		this.t.addFriendlyTag("orc");
		this.t.addFriendlyTag("golden_brain");
		this.t.addFriendlyTag("wolf");
		this.t.addFriendlyTag("hyena");
		this.clone("nomads_dwarf", "$TEMPLATE_NOMAD$");
		this.t.group_main = true;
		this.t.default_kingdom_color = ColorAsset.tryMakeNewColorAsset("#B1A0FF");
		this.t.setIcon("ui/Icons/iconDwarf");
		this.t.addTag("dwarf");
		this.t.addFriendlyTag("dwarf");
		this.t.addFriendlyTag("civ_crystal_golem");
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x0007DF9C File Offset: 0x0007C19C
	private void addCivs()
	{
		this.clone("human", "nomads_human");
		this.t.group_main = true;
		this.t.clearKingdomColor();
		this.t.setIcon("ui/Icons/iconHumans");
		this.t.civ = true;
		this.t.mobs = false;
		this.clone("elf", "nomads_elf");
		this.t.group_main = true;
		this.t.clearKingdomColor();
		this.t.setIcon("ui/Icons/iconElves");
		this.t.civ = true;
		this.t.mobs = false;
		this.clone("dwarf", "nomads_dwarf");
		this.t.group_main = true;
		this.t.clearKingdomColor();
		this.t.setIcon("ui/Icons/iconDwarf");
		this.t.civ = true;
		this.t.mobs = false;
		this.clone("orc", "nomads_orc");
		this.t.group_main = true;
		this.t.clearKingdomColor();
		this.t.setIcon("ui/Icons/iconOrcs");
		this.t.civ = true;
		this.t.mobs = false;
	}

	// Token: 0x060008C3 RID: 2243 RVA: 0x0007E0EC File Offset: 0x0007C2EC
	private void addNewCivs()
	{
		this.clone("civ_cat", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_cat");
		this.t.addTag("sliceable");
		this.clone("civ_dog", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_dog");
		this.t.addTag("sliceable");
		this.clone("civ_chicken", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_chicken");
		this.t.addTag("sliceable");
		this.clone("civ_rabbit", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_rabbit");
		this.t.addTag("sliceable");
		this.clone("civ_monkey", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_monkey");
		this.t.addTag("sliceable");
		this.clone("civ_fox", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_fox");
		this.t.addTag("sliceable");
		this.clone("civ_sheep", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_sheep");
		this.t.addTag("sliceable");
		this.clone("civ_cow", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_cow");
		this.t.addTag("sliceable");
		this.clone("civ_armadillo", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_armadillo");
		this.clone("civ_wolf", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_wolf");
		this.t.addTag("sliceable");
		this.clone("civ_bear", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_bear");
		this.t.addTag("sliceable");
		this.clone("civ_rhino", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_rhino");
		this.clone("civ_buffalo", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_buffalo");
		this.t.addTag("sliceable");
		this.clone("civ_hyena", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_hyena");
		this.t.addTag("sliceable");
		this.clone("civ_rat", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_rat");
		this.t.addTag("sliceable");
		this.clone("civ_alpaca", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_alpaca");
		this.t.addTag("sliceable");
		this.clone("civ_capybara", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_capybara");
		this.t.friendship_for_everyone = true;
		this.t.addFriendlyTag("everyone");
		this.clone("civ_goat", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_goat");
		this.t.addTag("sliceable");
		this.clone("civ_crab", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_crab");
		this.t.addFriendlyTag("crab");
		this.clone("civ_scorpion", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_scorpion");
		this.clone("civ_penguin", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_penguin");
		this.t.addTag("sliceable");
		this.clone("civ_turtle", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_turtle");
		this.clone("civ_crocodile", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_crocodile");
		this.t.addTag("sliceable");
		this.clone("civ_snake", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_snake");
		this.t.addTag("sliceable");
		this.t.addFriendlyTag("snake");
		this.t.addFriendlyTag("miniciv_snake");
		this.clone("civ_frog", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_frog");
		this.t.addTag("sliceable");
		this.clone("civ_piranha", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_piranha");
		this.clone("civ_liliar", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_liliar");
		this.t.addTag("sliceable");
		this.clone("civ_garlic_man", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_garlic_man");
		this.t.addTag("garlic");
		this.clone("civ_lemon_man", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_lemon_man");
		this.t.addTag("sliceable");
		this.clone("civ_acid_gentleman", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_acid_gentleman");
		this.clone("civ_crystal_golem", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_crystal_golem");
		this.t.addFriendlyTag("dwarf");
		this.clone("civ_candy_man", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_candy_man");
		this.clone("civ_beetle", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_beetle");
		this.clone("civ_seal", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_seal");
		this.clone("civ_unicorn", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_unicorn");
		this.clone("civ_ghost", "$TEMPLATE_CIV_NEW$");
		this.t.setIcon("ui/Icons/civs/civ_ghost");
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x0007E75C File Offset: 0x0007C95C
	private void addMobs()
	{
		this.clone("bandit", "$TEMPLATE_MOB$");
		this.t.setIcon("ui/Icons/iconBandit");
		this.t.default_kingdom_color = ColorAsset.tryMakeNewColorAsset("#E3362F");
		this.t.addTag("neutral");
		this.t.addTag("sliceable");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("miniciv_bandit");
		this.t.addEnemyTag("civ");
		this.clone("snowman", "$TEMPLATE_MOB$");
		this.t.setIcon("ui/Icons/iconSnowMan");
		this.t.addTag("snow");
		this.t.addFriendlyTag("good");
		this.t.addFriendlyTag("snow");
		this.clone("evil_mage", "$TEMPLATE_MOB$");
		this.t.setIcon("ui/Icons/iconEvilMage");
		this.t.default_kingdom_color = ColorAsset.tryMakeNewColorAsset("#E3362F");
		this.t.addTag("evil");
		this.t.addFriendlyTag("demon");
		this.clone("white_mage", "$TEMPLATE_MOB_VERY_GOOD$");
		this.t.setIcon("ui/Icons/iconWhiteMage");
		this.t.default_kingdom_color = ColorAsset.tryMakeNewColorAsset("#91E1D6");
		this.clone("necromancer", "$TEMPLATE_MOB$");
		this.t.setIcon("ui/Icons/iconNecromancer");
		this.t.default_kingdom_color = ColorAsset.tryMakeNewColorAsset("#81208B");
		this.t.addTag("evil");
		this.t.addFriendlyTag("undead");
		this.t.addFriendlyTag("miniciv_necromancer");
		this.t.addFriendlyTag("fly");
		this.t.addEnemyTag("garlic");
		this.clone("druid", "$TEMPLATE_MOB_GOOD$");
		this.t.setIcon("ui/Icons/iconDruid");
		this.t.default_kingdom_color = ColorAsset.tryMakeNewColorAsset("#85C32E");
		this.t.addTag("nature_creature");
		this.t.addFriendlyTag("nature_creature");
		this.t.addFriendlyTag("super_pumpkin");
		this.clone("plague_doctor", "$TEMPLATE_MOB_VERY_GOOD$");
		this.t.setIcon("ui/Icons/iconPlagueDoctor");
		this.clone("undead", "$TEMPLATE_MOB$");
		this.t.setIcon("ui/Icons/iconZombie");
		this.t.default_kingdom_color = ColorAsset.tryMakeNewColorAsset("#D5D5D5");
		this.t.addFriendlyTag("necromancer");
		this.t.addEnemyTag("garlic");
		this.clone("cold_one", "$TEMPLATE_MOB$");
		this.t.setIcon("ui/Icons/iconWalker");
		this.t.addTag("snow");
		this.t.addFriendlyTag("snow");
		this.clone("demon", "$TEMPLATE_MOB$");
		this.t.setIcon("ui/Icons/iconDemon");
		this.t.default_kingdom_color = ColorAsset.tryMakeNewColorAsset("#A30000");
		this.t.addFriendlyTag("fire_elemental");
		this.clone("angle", "$TEMPLATE_MOB$");
		this.t.setIcon("ui/Icons/iconAngle");
		this.t.addTag("good");
		this.t.addTag("nature_creature");
		this.t.addFriendlyTag("good");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("civ");
		this.t.addFriendlyTag("nature_creature");
		this.t.addFriendlyTag("super_pumpkin");
		this.t.addFriendlyTag("snowman");
		this.clone("aliens", "$TEMPLATE_MOB$");
		this.t.setIcon("ui/Icons/iconAlien");
		this.t.addTag("sliceable");
		this.t.addFriendlyTag("assimilators");
		this.clone("mush", "$TEMPLATE_MOB$");
		this.t.setIcon("ui/Icons/actor_traits/iconMushSpores");
		this.t.addTag("sliceable");
		this.t.addFriendlyTag("living_plants");
		this.clone("greg", "$TEMPLATE_MOB$");
		this.t.setIcon("ui/Icons/iconGreg");
		this.t.addTag("sliceable");
		this.clone("fire_elemental", "$TEMPLATE_MOB$");
		this.t.setIcon("ui/Icons/iconFireElemental");
		this.t.addFriendlyTag("demon");
		this.t.addFriendlyTag("dragons");
		this.t.addFriendlyTag("fire_skull");
		this.clone("dragons", "$TEMPLATE_MOB$");
		this.t.setIcon("ui/Icons/iconDragon");
		this.t.addTag("sliceable");
		this.t.addFriendlyTag("fire_elemental");
		this.clone("living_plants", "$TEMPLATE_MOB$");
		this.t.setIcon("ui/Icons/iconLivingPlants");
		this.t.addTag("nature_creature");
		this.t.addFriendlyTag("nature_creature");
		this.t.addFriendlyTag("good");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("mush");
		this.clone("living_houses", "$TEMPLATE_MOB$");
		this.t.default_kingdom_color = ColorAsset.tryMakeNewColorAsset("#E53B3B");
		this.t.setIcon("ui/Icons/iconLivingHouse");
		this.t.addFriendlyTag("living_houses");
		this.clone("fire_skull", "$TEMPLATE_MOB$");
		this.t.addTag("undead");
		this.t.addTag("demon");
		this.t.setIcon("ui/Icons/iconFireSkull");
		this.t.addFriendlyTag("demon");
		this.t.addFriendlyTag("dragons");
		this.t.addFriendlyTag("undead");
		this.t.addFriendlyTag("fire_elemental");
		this.clone("jumpy_skull", "$TEMPLATE_MOB$");
		this.t.addTag("undead");
		this.t.setIcon("ui/Icons/iconJumpySkull");
		this.t.addFriendlyTag("undead");
		this.t.addFriendlyTag("fire_skull");
		this.t.addFriendlyTag("necromancer");
		this.clone("fairy", "good");
		this.t.setIcon("ui/Icons/iconFairy");
		this.t.addTag("good");
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x0007EE68 File Offset: 0x0007D068
	private void addAnimals()
	{
		this.clone("cat", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconCat");
		this.t.addTag("small");
		this.t.addTag("sliceable");
		this.t.addFriendlyTag("living_houses");
		this.t.addFriendlyTag("snowman");
		this.t.addEnemyTag("snake");
		this.clone("dog", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconDog");
		this.t.addTag("sliceable");
		this.t.addFriendlyTag("wolf");
		this.t.addFriendlyTag("human");
		this.t.addEnemyTag("cat");
		this.clone("chicken", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconChicken");
		this.t.addTag("small");
		this.t.addTag("sliceable");
		this.clone("rabbit", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconRabbit");
		this.t.addTag("small");
		this.t.addTag("sliceable");
		this.clone("monkey", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconMonkey");
		this.t.addTag("sliceable");
		this.t.addFriendlyTag("living_houses");
		this.t.addFriendlyTag("snowman");
		this.t.addEnemyTag("snake");
		this.clone("fox", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconFox");
		this.t.addTag("sliceable");
		this.t.addFriendlyTag("wolf");
		this.t.addFriendlyTag("bear");
		this.clone("sheep", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconSheep");
		this.t.addTag("sliceable");
		this.clone("cow", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconCow");
		this.t.addTag("sliceable");
		this.clone("armadillo", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconArmadillo");
		this.clone("raccoon", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconRaccoon");
		this.t.addTag("sliceable");
		this.t.addTag("small");
		this.t.addFriendlyTag("bandit");
		this.clone("wolf", "$TEMPLATE_ANIMAL$");
		this.t.setIcon("ui/Icons/iconWolf");
		this.t.addTag("sliceable");
		this.t.addFriendlyTag("orc");
		this.t.addFriendlyTag("dog");
		this.t.addFriendlyTag("living_houses");
		this.clone("bear", "$TEMPLATE_ANIMAL$");
		this.t.setIcon("ui/Icons/iconBear");
		this.t.addTag("sliceable");
		this.t.addFriendlyTag("living_houses");
		this.t.addEnemyTag("rhino");
		this.t.addEnemyTag("crocodile");
		this.clone("rhino", "$TEMPLATE_ANIMAL$");
		this.t.setIcon("ui/Icons/iconRhino");
		this.t.addEnemyTag("hyena");
		this.t.addEnemyTag("snake");
		this.t.addEnemyTag("bear");
		this.t.addEnemyTag("wolf");
		this.t.addEnemyTag("rat");
		this.clone("buffalo", "$TEMPLATE_ANIMAL$");
		this.t.setIcon("ui/Icons/iconBuffalo");
		this.t.addTag("sliceable");
		this.t.addFriendlyTag("rhino");
		this.t.addEnemyTag("hyena");
		this.t.addEnemyTag("bear");
		this.t.addEnemyTag("wolf");
		this.t.addEnemyTag("crocodile");
		this.clone("hyena", "$TEMPLATE_ANIMAL$");
		this.t.setIcon("ui/Icons/iconHyena");
		this.t.addTag("sliceable");
		this.t.addFriendlyTag("orc");
		this.t.addFriendlyTag("living_houses");
		this.t.addEnemyTag("monkey");
		this.clone("rat", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconRat");
		this.t.addTag("sliceable");
		this.t.addTag("small");
		this.t.addFriendlyTag("civ_acid_gentleman");
		this.t.addFriendlyTag("miniciv_acid_blob");
		this.t.addFriendlyTag("acid_blob");
		this.t.addEnemyTag("cat");
		this.clone("alpaca", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconAlpaca");
		this.t.addTag("sliceable");
		this.clone("capybara", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconCapybara");
		this.t.friendship_for_everyone = true;
		this.t.addFriendlyTag("everyone");
		this.clone("goat", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconGoat");
		this.t.addFriendlyTag("nomads_dwarf");
		this.t.addFriendlyTag("dwarf");
		this.t.addFriendlyTag("civ_crystal_golem");
		this.t.addFriendlyTag("crystal_sword");
		this.clone("penguin", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconPenguin");
		this.t.addTag("sliceable");
		this.t.addFriendlyTag("bandit");
		this.t.addFriendlyTag("super_pumpkin");
		this.clone("ostrich", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconOstrich");
		this.t.addTag("sliceable");
		this.clone("crab", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconCrab");
		this.t.addTag("small");
		this.t.addFriendlyTag("living_houses");
		this.t.addFriendlyTag("snowman");
		this.t.addFriendlyTag("crabzilla");
		this.clone("scorpion", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconScorpion");
		this.clone("turtle", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconTurtle");
		this.clone("crocodile", "$TEMPLATE_ANIMAL$");
		this.t.setIcon("ui/Icons/iconCrocodile");
		this.t.addTag("sliceable");
		this.t.addEnemyTag("chicken");
		this.t.addEnemyTag("monkey");
		this.clone("snake", "$TEMPLATE_ANIMAL_NEUTRAL$");
		this.t.setIcon("ui/Icons/iconSnake");
		this.t.addTag("small");
		this.t.addTag("nature_creature");
		this.t.addFriendlyTag("civ_snake");
		this.t.addFriendlyTag("elf");
		this.t.addFriendlyTag("nature_creature");
		this.clone("frog", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconFrog");
		this.t.addTag("sliceable");
		this.clone("piranha", "$TEMPLATE_ANIMAL$");
		this.t.setIcon("ui/Icons/iconPiranha");
		this.t.addTag("sliceable");
		this.t.addTag("small");
		this.clone("seal", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconSeal");
		this.t.addTag("sliceable");
		this.clone("flower_bud", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconFlowerBud");
		this.t.addTag("sliceable");
		this.clone("crystal_sword", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconCrystalSword");
		this.t.addEnemyTag("sliceable");
		this.clone("lemon_snail", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconLemonSnail");
		this.t.addTag("sliceable");
		this.t.addTag("small");
		this.clone("garl", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconGarl");
		this.t.addTag("garlic");
		this.clone("smore", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconSmore");
		this.t.addTag("sliceable");
		this.t.addTag("small");
		this.clone("acid_blob", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconAcidBlob");
		this.t.addEnemyTag("small");
		this.clone("unicorn", "$TEMPLATE_ANIMAL_PEACEFUL$");
		this.t.setIcon("ui/Icons/iconUnicorn");
		this.t.addEnemyTag("sliceable");
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x0007F8B8 File Offset: 0x0007DAB8
	private void addCoolMinicivs()
	{
		this.cloneAsMiniciv("civ_aliens", "aliens", true);
		this.t.group_minicivs_cool = true;
		this.cloneAsMiniciv("civ_druid", "druid", false);
		this.t.group_minicivs_cool = true;
		this.t.default_kingdom_color = ColorAsset.tryMakeNewColorAsset("#85C32E");
		this.t.addTag("sliceable");
		this.cloneAsMiniciv("miniciv_angle", "angle", true);
		this.t.group_minicivs_cool = true;
		this.cloneAsMiniciv("miniciv_bandit", "bandit", true);
		this.t.group_minicivs_cool = true;
		this.cloneAsMiniciv("miniciv_cold_one", "cold_one", true);
		this.t.group_minicivs_cool = true;
		this.cloneAsMiniciv("miniciv_demon", "demon", true);
		this.t.group_minicivs_cool = true;
		this.cloneAsMiniciv("miniciv_evil_mage", "evil_mage", true);
		this.t.group_minicivs_cool = true;
		this.cloneAsMiniciv("miniciv_fire_skull", "fire_skull", true);
		this.t.group_minicivs_cool = true;
		this.cloneAsMiniciv("miniciv_jumpy_skull", "jumpy_skull", true);
		this.t.group_minicivs_cool = true;
		this.cloneAsMiniciv("miniciv_necromancer", "necromancer", true);
		this.t.group_minicivs_cool = true;
		this.t.addFriendlyTag("necromancer");
		this.t.addFriendlyTag("undead");
		this.cloneAsMiniciv("miniciv_plague_doctor", "plague_doctor", true);
		this.t.group_minicivs_cool = true;
		this.cloneAsMiniciv("miniciv_white_mage", "white_mage", true);
		this.t.group_minicivs_cool = true;
		this.cloneAsMiniciv("miniciv_greg", "greg", false);
		this.t.group_minicivs_cool = true;
		this.cloneAsMiniciv("miniciv_fairy", "fairy", true);
		this.t.group_minicivs_cool = true;
		this.cloneAsMiniciv("miniciv_snowman", "snowman", true);
		this.t.group_minicivs_cool = true;
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x0007FAC0 File Offset: 0x0007DCC0
	private void addAnimalMinicivs()
	{
		this.cloneAsMiniciv("miniciv_cat", "cat", false);
		this.cloneAsMiniciv("miniciv_dog", "dog", false);
		this.cloneAsMiniciv("miniciv_chicken", "chicken", false);
		this.cloneAsMiniciv("miniciv_rabbit", "rabbit", false);
		this.cloneAsMiniciv("miniciv_monkey", "monkey", false);
		this.cloneAsMiniciv("miniciv_fox", "fox", false);
		this.cloneAsMiniciv("miniciv_sheep", "sheep", false);
		this.cloneAsMiniciv("miniciv_cow", "cow", false);
		this.cloneAsMiniciv("miniciv_armadillo", "armadillo", false);
		this.cloneAsMiniciv("miniciv_raccoon", "raccoon", false);
		this.cloneAsMiniciv("miniciv_wolf", "wolf", false);
		this.cloneAsMiniciv("miniciv_bear", "bear", false);
		this.cloneAsMiniciv("miniciv_rhino", "rhino", false);
		this.cloneAsMiniciv("miniciv_buffalo", "buffalo", false);
		this.cloneAsMiniciv("miniciv_hyena", "hyena", false);
		this.cloneAsMiniciv("miniciv_rat", "rat", false);
		this.cloneAsMiniciv("miniciv_alpaca", "alpaca", false);
		this.cloneAsMiniciv("miniciv_capybara", "capybara", false);
		this.t.addFriendlyTag("everyone");
		this.cloneAsMiniciv("miniciv_goat", "goat", false);
		this.cloneAsMiniciv("miniciv_penguin", "penguin", false);
		this.cloneAsMiniciv("miniciv_ostrich", "ostrich", false);
		this.cloneAsMiniciv("miniciv_crab", "crab", false);
		this.t.addFriendlyTag("crabzilla");
		this.cloneAsMiniciv("miniciv_scorpion", "scorpion", false);
		this.cloneAsMiniciv("miniciv_turtle", "turtle", false);
		this.cloneAsMiniciv("miniciv_crocodile", "crocodile", false);
		this.cloneAsMiniciv("miniciv_snake", "snake", false);
		this.cloneAsMiniciv("miniciv_frog", "frog", false);
		this.cloneAsMiniciv("miniciv_piranha", "piranha", false);
		this.cloneAsMiniciv("miniciv_seal", "seal", false);
		this.cloneAsMiniciv("miniciv_flower_bud", "flower_bud", false);
		this.cloneAsMiniciv("miniciv_crystal_sword", "crystal_sword", false);
		this.cloneAsMiniciv("miniciv_lemon_snail", "lemon_snail", false);
		this.cloneAsMiniciv("miniciv_garl", "garl", false);
		this.cloneAsMiniciv("miniciv_smore", "smore", false);
		this.cloneAsMiniciv("miniciv_acid_blob", "acid_blob", false);
		this.cloneAsMiniciv("miniciv_insect", "insect", false);
		this.cloneAsMiniciv("miniciv_unicorn", "unicorn", false);
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x0007FD64 File Offset: 0x0007DF64
	private void addCreeps()
	{
		this.clone("super_pumpkin", "$TEMPLATE_MOB$");
		this.t.setIcon("ui/Icons/iconSuperPumpkin");
		this.t.addTag("sliceable");
		this.t.group_creeps = true;
		this.t.addFriendlyTag("druid");
		this.clone("tumor", "$TEMPLATE_MOB$");
		this.t.group_creeps = true;
		this.t.setIcon("ui/Icons/iconTumor");
		this.clone("biomass", "$TEMPLATE_MOB$");
		this.t.group_creeps = true;
		this.t.setIcon("ui/Icons/iconBiomass");
		this.clone("assimilators", "$TEMPLATE_MOB$");
		this.t.group_creeps = true;
		this.t.setIcon("ui/Icons/iconAssimilator");
		this.t.addFriendlyTag("aliens");
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x0007FE58 File Offset: 0x0007E058
	private void addUnique()
	{
		this.add(new KingdomAsset
		{
			id = "godfinger",
			nature = true,
			count_as_danger = false
		});
		this.t.setIcon("ui/Icons/iconGodFinger");
		this.add(new KingdomAsset
		{
			id = "good",
			mobs = true,
			concept = true,
			count_as_danger = false
		});
		this.t.setIcon("ui/Icons/actor_traits/iconBlessing");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("civ");
		this.t.addFriendlyTag("nature_creature");
		this.t.addFriendlyTag("living_houses");
		this.t.addFriendlyTag("snowman");
		this.t.addEnemyTag("wolf");
		this.t.addEnemyTag("bear");
		this.t.addEnemyTag("orc");
		this.t.addEnemyTag("bandit");
		this.add(new KingdomAsset
		{
			id = "mad",
			always_attack_each_other = true,
			force_look_all_chunks = true,
			mobs = true,
			units_always_looking_for_enemies = true,
			is_forced_by_trait = true,
			forced_by_trait_kingdom_id = "madness"
		});
		this.t.setIcon("ui/Icons/actor_traits/iconMadness");
		this.t.default_kingdom_color = ColorAsset.tryMakeNewColorAsset("#E53B3B");
		this.add(new KingdomAsset
		{
			id = "alien_mold",
			force_look_all_chunks = true,
			mobs = true,
			units_always_looking_for_enemies = true,
			is_forced_by_trait = true,
			forced_by_trait_kingdom_id = "desire_alien_mold",
			building_attractor_id = "waypoint_alien_mold"
		});
		this.t.setIcon("ui/Icons/iconWaypointAlienMold");
		this.t.default_kingdom_color = ColorAsset.tryMakeNewColorAsset("#C342FF");
		this.t.addFriendlyTag("aliens");
		this.t.addFriendlyTag("civ_aliens");
		this.add(new KingdomAsset
		{
			id = "computer",
			force_look_all_chunks = true,
			mobs = true,
			units_always_looking_for_enemies = true,
			is_forced_by_trait = true,
			forced_by_trait_kingdom_id = "desire_computer",
			building_attractor_id = "waypoint_computer"
		});
		this.t.setIcon("ui/Icons/iconWaypointComputer");
		this.t.default_kingdom_color = ColorAsset.tryMakeNewColorAsset("#5DCE2D");
		this.t.addFriendlyTag("assimilators");
		this.add(new KingdomAsset
		{
			id = "golden_egg",
			force_look_all_chunks = true,
			mobs = true,
			units_always_looking_for_enemies = true,
			is_forced_by_trait = true,
			forced_by_trait_kingdom_id = "desire_golden_egg",
			building_attractor_id = "waypoint_golden_egg"
		});
		this.t.setIcon("ui/Icons/iconWaypointGoldenEgg");
		this.t.default_kingdom_color = ColorAsset.tryMakeNewColorAsset("#FFEC77");
		this.t.addFriendlyTag("chicken");
		this.t.addFriendlyTag("civ_chicken");
		this.t.addFriendlyTag("miniciv_chicken");
		this.t.addFriendlyTag("sheep");
		this.t.addFriendlyTag("civ_sheep");
		this.t.addFriendlyTag("miniciv_sheep");
		this.add(new KingdomAsset
		{
			id = "harp",
			force_look_all_chunks = true,
			mobs = true,
			units_always_looking_for_enemies = true,
			is_forced_by_trait = true,
			forced_by_trait_kingdom_id = "desire_harp",
			building_attractor_id = "waypoint_harp"
		});
		this.t.setIcon("ui/Icons/iconWaypointHarp");
		this.t.default_kingdom_color = ColorAsset.tryMakeNewColorAsset("#FF60E9");
		this.t.addFriendlyTag("crystal_sword");
		this.t.addFriendlyTag("civ_crystal_golem");
		this.t.addFriendlyTag("miniciv_crystal_sword");
		this.add(new KingdomAsset
		{
			id = "possessed",
			force_look_all_chunks = true
		});
		this.t.setIcon("ui/Icons/iconPossessed2");
		this.t.addEnemyTag("nature");
		this.t.addEnemyTag("ruins");
		this.t.addEnemyTag("abandoned");
		this.add(new KingdomAsset
		{
			id = "crabzilla",
			mobs = true
		});
		this.t.setIcon("ui/Icons/iconCrabzilla");
		this.t.addTag("crab");
		this.t.addFriendlyTag("crab");
		this.t.addFriendlyTag("civ_crab");
		this.t.addFriendlyTag("miniciv_crab");
		this.add(new KingdomAsset
		{
			id = "ants",
			mobs = true
		});
		this.t.setIcon("ui/Icons/iconAntRed");
		this.t.addTag("nature_creature");
		this.t.addFriendlyTag("good");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("nature_creature");
		this.t.addFriendlyTag("living_houses");
		this.add(new KingdomAsset
		{
			id = "golden_brain",
			mobs = true,
			brain = true,
			count_as_danger = false
		});
		this.t.setIcon("ui/Icons/iconGoldBrain");
		this.t.addTag("neutral");
		this.t.addFriendlyTag("orc");
		this.t.addFriendlyTag("bandit");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("civ");
		this.t.addFriendlyTag("nature_creature");
		this.t.addFriendlyTag("living_houses");
		this.t.addFriendlyTag("snowman");
		this.add(new KingdomAsset
		{
			id = "corrupted_brain",
			mobs = true,
			brain = true
		});
		this.t.setIcon("ui/Icons/iconCorruptedBrain");
		this.t.addTag("mad");
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x00080494 File Offset: 0x0007E694
	private void addNeutral()
	{
		this.add(new KingdomAsset
		{
			id = "neutral",
			civ = true,
			neutral = true,
			default_civ_color_index = 83,
			count_as_danger = false,
			concept = true
		});
		this.t.setIcon("ui/Icons/worldrules/icon_random_seeds");
		this.t.default_kingdom_color = ColorAsset.tryMakeNewColorAsset("#AAAAAA");
		this.t.addTag("nature_creature");
		this.t.addTag("neutral");
		this.t.addFriendlyTag("good");
		this.t.addFriendlyTag("nature_creature");
		this.t.addFriendlyTag("neutral");
		this.add(new KingdomAsset
		{
			id = "neutral_animals",
			mobs = true,
			count_as_danger = false,
			concept = true
		});
		this.t.setIcon("ui/Icons/worldrules/icon_animalspawn");
		this.t.addTag("neutral");
		this.t.addTag("nature_creature");
		this.t.addFriendlyTag("good");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("nature_creature");
		this.t.addFriendlyTag("living_houses");
		this.t.addFriendlyTag("snowman");
		this.t.addFriendlyTag("civ");
		this.clone("insect", "neutral_animals");
		this.t.setIcon("ui/Icons/iconBeetle");
		this.t.concept = true;
		this.clone("fly", "insect");
		this.t.setIcon("ui/Icons/iconFly");
		this.add(new KingdomAsset
		{
			id = "nature",
			nature = true,
			mobs = true,
			count_as_danger = false,
			concept = true
		});
		this.t.default_kingdom_color = ColorAsset.tryMakeNewColorAsset("#888888");
		this.t.setIcon("ui/Icons/world generation/icon_randomBiomes");
		this.add(new KingdomAsset
		{
			id = "ruins",
			nature = true,
			mobs = true,
			count_as_danger = false,
			concept = true
		});
		this.t.default_kingdom_color = ColorAsset.tryMakeNewColorAsset("#444444");
		this.t.setIcon("ui/Icons/iconCityDestroyed");
		this.t.color_building = Toolbox.color_white;
		this.add(new KingdomAsset
		{
			id = "abandoned",
			nature = true,
			mobs = true,
			abandoned = true,
			count_as_danger = false,
			concept = true
		});
		this.t.default_kingdom_color = ColorAsset.tryMakeNewColorAsset("#888888");
		this.t.setIcon("ui/Icons/iconKingdomDestroyed");
		this.t.color_building = Toolbox.color_abandoned_building;
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x0008078C File Offset: 0x0007E98C
	public override void post_init()
	{
		base.post_init();
		using (ListPool<string> tFriendshipEveryone = new ListPool<string>())
		{
			foreach (KingdomAsset tAsset in this.list)
			{
				if (tAsset.friendship_for_everyone && !tAsset.brain)
				{
					tFriendshipEveryone.Add(tAsset.id);
				}
			}
			foreach (KingdomAsset tAsset2 in this.list)
			{
				tAsset2.addTag("everyone");
				foreach (string ptr in tFriendshipEveryone)
				{
					string tTag = ptr;
					tAsset2.addFriendlyTag(tTag);
				}
				if (tAsset2.default_kingdom_color != null)
				{
					if (string.Equals(tAsset2.default_kingdom_color.id, "ASSET_ID"))
					{
						tAsset2.default_kingdom_color.id = "kingdom_library_color_" + tAsset2.id;
					}
				}
				else
				{
					tAsset2.default_kingdom_color = this._shared_default_color;
				}
			}
		}
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x0008091C File Offset: 0x0007EB1C
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (KingdomAsset tAsset in this.list)
		{
			this.finish(tAsset);
		}
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x00080978 File Offset: 0x0007EB78
	private void finish(KingdomAsset pAsset)
	{
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x0008097C File Offset: 0x0007EB7C
	public override void editorDiagnostic()
	{
		base.editorDiagnostic();
		this.generateDebugReportFile();
		foreach (KingdomAsset tKingdom in this.list)
		{
			if ((tKingdom.civ || tKingdom.mobs) && !tKingdom.concept && !tKingdom.nomads && !tKingdom.nature && !tKingdom.neutral && !tKingdom.brain && !tKingdom.is_forced_by_trait)
			{
				bool tFound = false;
				foreach (ActorAsset tActorAsset in AssetManager.actor_library.list)
				{
					if (tKingdom.civ && tActorAsset.kingdom_id_civilization == tKingdom.id)
					{
						tFound = true;
						break;
					}
					if (tKingdom.mobs && tActorAsset.kingdom_id_wild == tKingdom.id)
					{
						tFound = true;
						break;
					}
				}
				if (!tFound)
				{
					if (tKingdom.civ)
					{
						BaseAssetLibrary.logAssetError("<b>KingdomLibrary</b>: <e>Civ Kingdom</e> is not used by any <e>kingdom_id_civilization</e>", tKingdom.id);
					}
					else
					{
						BaseAssetLibrary.logAssetError("<b>KingdomLibrary</b>: <e>Mob Kingdom</e> is not used by any <e>kingdom_id_wild</e>", tKingdom.id);
					}
				}
			}
		}
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x00080AF8 File Offset: 0x0007ECF8
	public void checkForMissingTags()
	{
		for (int i = 0; i < this.list.Count - 1; i++)
		{
			KingdomAsset tKingdom = this.list[i];
			for (int j = i + 1; j < this.list.Count; j++)
			{
				KingdomAsset tKingdom2 = this.list[j];
				if (tKingdom != tKingdom2 && tKingdom.isFoe(tKingdom2) != tKingdom2.isFoe(tKingdom))
				{
					KingdomAsset kingdomAsset = tKingdom;
					if (kingdomAsset.assets_discrepancies == null)
					{
						kingdomAsset.assets_discrepancies = new HashSet<string>();
					}
					kingdomAsset = tKingdom2;
					if (kingdomAsset.assets_discrepancies == null)
					{
						kingdomAsset.assets_discrepancies = new HashSet<string>();
					}
					tKingdom.assets_discrepancies.Add(tKingdom2.id);
					tKingdom2.assets_discrepancies.Add(tKingdom.id);
					if (tKingdom2.id.Contains(tKingdom.id) || tKingdom.id.Contains(tKingdom2.id))
					{
						kingdomAsset = tKingdom;
						if (kingdomAsset.assets_discrepancies_bad == null)
						{
							kingdomAsset.assets_discrepancies_bad = new HashSet<string>();
						}
						kingdomAsset = tKingdom2;
						if (kingdomAsset.assets_discrepancies_bad == null)
						{
							kingdomAsset.assets_discrepancies_bad = new HashSet<string>();
						}
						tKingdom.assets_discrepancies_bad.Add(tKingdom2.id);
						tKingdom2.assets_discrepancies_bad.Add(tKingdom.id);
					}
				}
			}
		}
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x00080C44 File Offset: 0x0007EE44
	public unsafe void generateDebugReportFile()
	{
		if (!DebugConfig.isOn(DebugOption.GenerateGameplayReport))
		{
			return;
		}
		string tPath = "GenAssets/wbdiag/kingdom_library.log";
		using (StringBuilderPool tResult = new StringBuilderPool())
		{
			tResult.AppendLine("# RELATIONS");
			using (ListPool<string> friendly = new ListPool<string>())
			{
				using (ListPool<string> foes = new ListPool<string>())
				{
					Span<KingdomAsset> tList = this.list.AsSpan<KingdomAsset>();
					Span<KingdomAsset> span = tList;
					for (int i = 0; i < span.Length; i++)
					{
						KingdomAsset tK = *span[i];
						friendly.Clear();
						foes.Clear();
						tResult.AppendLine("###" + tK.id.ToUpper() + ":");
						Span<KingdomAsset> span2 = tList;
						for (int j = 0; j < span2.Length; j++)
						{
							KingdomAsset tK2 = *span2[j];
							if (tK.isFoe(tK2))
							{
								foes.Add(tK2.id);
							}
							else
							{
								friendly.Add(tK2.id);
							}
						}
						tResult.AppendLine("- FRIENDLY:");
						foreach (string ptr in friendly)
						{
							string tID = ptr;
							tResult.AppendLine("   " + tID);
						}
						tResult.AppendLine("- FOES:");
						foreach (string ptr2 in foes)
						{
							string tID2 = ptr2;
							tResult.AppendLine("   " + tID2);
						}
						tResult.AppendLine();
					}
					tResult.AppendLine();
					tResult.AppendLine();
					tResult.AppendLine("# TAGS");
					Dictionary<string, HashSet<string>> tTags = UnsafeCollectionPool<Dictionary<string, HashSet<string>>, KeyValuePair<string, HashSet<string>>>.Get();
					span = tList;
					for (int i = 0; i < span.Length; i++)
					{
						foreach (string tTag in span[i]->list_tags)
						{
							tTags.TryAdd(tTag, UnsafeCollectionPool<HashSet<string>, string>.Get());
							Span<KingdomAsset> span2 = tList;
							for (int j = 0; j < span2.Length; j++)
							{
								KingdomAsset tAssetInner = *span2[j];
								if (tAssetInner.list_tags.Contains(tTag))
								{
									tTags[tTag].Add(tAssetInner.id);
								}
							}
						}
					}
					foreach (KeyValuePair<string, HashSet<string>> tPair in tTags)
					{
						tResult.AppendLine("-" + tPair.Key.ToUpper() + ":");
						foreach (string tTag2 in tPair.Value)
						{
							tResult.AppendLine("   " + tTag2);
						}
						UnsafeCollectionPool<HashSet<string>, string>.Release(tPair.Value);
					}
					tTags.Clear();
					UnsafeCollectionPool<Dictionary<string, HashSet<string>>, KeyValuePair<string, HashSet<string>>>.Release(tTags);
					File.WriteAllTextAsync(tPath, tResult.ToString(), default(CancellationToken));
				}
			}
		}
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x00081054 File Offset: 0x0007F254
	public void cloneAsMiniciv(string pNew, string pFrom, bool pMakeLoveToNeutrals = false)
	{
		this.clone(pNew, pFrom);
		this.t.group_miniciv = true;
		this.t.mobs = false;
		this.t.civ = true;
		this.t.addTag(pFrom);
		this.t.addFriendlyTag(pFrom);
		this.get(pFrom).addFriendlyTag(pNew);
		if (pMakeLoveToNeutrals)
		{
			this.t.addTag("civ");
			this.t.addFriendlyTag("neutral");
			this.t.addFriendlyTag("nature_creature");
		}
	}

	// Token: 0x060008D2 RID: 2258 RVA: 0x000810E6 File Offset: 0x0007F2E6
	public override KingdomAsset clone(string pNew, string pFrom)
	{
		base.clone(pNew, pFrom);
		this.t.concept = false;
		return this.t;
	}

	// Token: 0x0400091E RID: 2334
	private const string TEMPLATE_MOB = "$TEMPLATE_MOB$";

	// Token: 0x0400091F RID: 2335
	private const string TEMPLATE_MOB_GOOD = "$TEMPLATE_MOB_GOOD$";

	// Token: 0x04000920 RID: 2336
	private const string TEMPLATE_MOB_VERY_GOOD = "$TEMPLATE_MOB_VERY_GOOD$";

	// Token: 0x04000921 RID: 2337
	private const string TEMPLATE_ANIMAL = "$TEMPLATE_ANIMAL$";

	// Token: 0x04000922 RID: 2338
	private const string TEMPLATE_ANIMAL_NEUTRAL = "$TEMPLATE_ANIMAL_NEUTRAL$";

	// Token: 0x04000923 RID: 2339
	private const string TEMPLATE_ANIMAL_PEACEFUL = "$TEMPLATE_ANIMAL_PEACEFUL$";

	// Token: 0x04000924 RID: 2340
	private const string TEMPLATE_CIV = "$TEMPLATE_CIV$";

	// Token: 0x04000925 RID: 2341
	private const string TEMPLATE_CIV_GOOD = "$TEMPLATE_CIV_GOOD$";

	// Token: 0x04000926 RID: 2342
	private const string TEMPLATE_NOMAD = "$TEMPLATE_NOMAD$";

	// Token: 0x04000927 RID: 2343
	private const string TEMPLATE_CIV_NEW = "$TEMPLATE_CIV_NEW$";

	// Token: 0x04000928 RID: 2344
	private ColorAsset _shared_default_color;
}
