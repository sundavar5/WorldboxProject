using System;

// Token: 0x020001B5 RID: 437
public class WorldLawLibrary : BaseLibraryWithUnlockables<WorldLawAsset>
{
	// Token: 0x06000CAF RID: 3247 RVA: 0x000B8300 File Offset: 0x000B6500
	public override void init()
	{
		base.init();
		this.addFlora();
		WorldLawLibrary.world_law_gene_spaghetti = this.add(new WorldLawAsset
		{
			id = "world_law_gene_spaghetti",
			group_id = "units",
			icon_path = "ui/Icons/worldrules/icon_gene_spaghetti",
			default_state = false
		});
		WorldLawLibrary.world_law_mutant_box = this.add(new WorldLawAsset
		{
			id = "world_law_mutant_box",
			group_id = "units",
			icon_path = "ui/Icons/worldrules/icon_mutant_box",
			default_state = false
		});
		WorldLawLibrary.world_law_glitched_noosphere = this.add(new WorldLawAsset
		{
			id = "world_law_glitched_noosphere",
			group_id = "civilizations",
			icon_path = "ui/Icons/worldrules/icon_glitched_noosphere",
			default_state = false
		});
		WorldLawLibrary.world_law_drop_of_thoughts = this.add(new WorldLawAsset
		{
			id = "world_law_drop_of_thoughts",
			group_id = "spawn",
			icon_path = "ui/Icons/worldrules/icon_drop_of_thoughts",
			default_state = false
		});
		WorldLawLibrary.world_law_diplomacy = this.add(new WorldLawAsset
		{
			id = "world_law_diplomacy",
			group_id = "diplomacy",
			icon_path = "ui/Icons/worldrules/icon_diplomacy",
			default_state = true,
			on_state_change = new PlayerOptionAction(this.checkDiplomacy)
		});
		WorldLawLibrary.world_law_rites = this.add(new WorldLawAsset
		{
			id = "world_law_rites",
			group_id = "diplomacy",
			icon_path = "ui/Icons/worldrules/icon_rites",
			default_state = true
		});
		WorldLawLibrary.world_law_peaceful_monsters = this.add(new WorldLawAsset
		{
			id = "world_law_peaceful_monsters",
			group_id = "mobs",
			icon_path = "ui/Icons/worldrules/icon_peacefulanimals",
			default_state = false,
			on_state_change = new PlayerOptionAction(this.checkPeacefulMonsters)
		});
		WorldLawLibrary.world_law_hunger = this.add(new WorldLawAsset
		{
			id = "world_law_hunger",
			group_id = "units",
			icon_path = "ui/Icons/worldrules/icon_hunger",
			default_state = true
		});
		WorldLawLibrary.world_law_vegetation_random_seeds = this.add(new WorldLawAsset
		{
			id = "world_law_vegetation_random_seeds",
			group_id = "nature",
			icon_path = "ui/Icons/worldrules/icon_random_seeds",
			default_state = true
		});
		WorldLawLibrary.world_law_roots_without_borders = this.add(new WorldLawAsset
		{
			id = "world_law_roots_without_borders",
			group_id = "nature",
			icon_path = "ui/Icons/worldrules/icon_roots_without_borders",
			default_state = false
		});
		WorldLawLibrary.world_law_grow_minerals = this.add(new WorldLawAsset
		{
			id = "world_law_grow_minerals",
			group_id = "nature",
			icon_path = "ui/Icons/iconStone",
			default_state = true
		});
		WorldLawLibrary.world_law_grow_grass = this.add(new WorldLawAsset
		{
			id = "world_law_grow_grass",
			group_id = "biomes",
			icon_path = "ui/Icons/worldrules/icon_growgrass",
			default_state = true
		});
		WorldLawLibrary.world_law_biome_overgrowth = this.add(new WorldLawAsset
		{
			id = "world_law_biome_overgrowth",
			group_id = "biomes",
			icon_path = "ui/Icons/worldrules/icon_overgrowth",
			default_state = true
		});
		WorldLawLibrary.world_law_terramorphing = this.add(new WorldLawAsset
		{
			id = "world_law_terramorphing",
			group_id = "civilizations",
			icon_path = "ui/Icons/worldrules/icon_terramorphing",
			default_state = true
		});
		WorldLawLibrary.world_law_kingdom_expansion = this.add(new WorldLawAsset
		{
			id = "world_law_kingdom_expansion",
			group_id = "civilizations",
			icon_path = "ui/Icons/worldrules/icon_kingdomexpansion",
			default_state = true
		});
		WorldLawLibrary.world_law_old_age = this.add(new WorldLawAsset
		{
			id = "world_law_old_age",
			group_id = "units",
			icon_path = "ui/Icons/worldrules/icon_oldage",
			default_state = true
		});
		WorldLawLibrary.world_law_animals_spawn = this.add(new WorldLawAsset
		{
			id = "world_law_animals_spawn",
			group_id = "spawn",
			icon_path = "ui/Icons/worldrules/icon_animalspawn",
			default_state = true
		});
		WorldLawLibrary.world_law_animals_babies = this.add(new WorldLawAsset
		{
			id = "world_law_animals_babies",
			group_id = "mobs",
			icon_path = "ui/Icons/iconChicken",
			default_state = true
		});
		WorldLawLibrary.world_law_rebellions = this.add(new WorldLawAsset
		{
			id = "world_law_rebellions",
			group_id = "diplomacy",
			icon_path = "ui/Icons/worldrules/icon_rebellion",
			default_state = true
		});
		WorldLawLibrary.world_law_border_stealing = this.add(new WorldLawAsset
		{
			id = "world_law_border_stealing",
			group_id = "diplomacy",
			icon_path = "ui/Icons/worldrules/icon_borderstealing",
			default_state = true
		});
		WorldLawLibrary.world_law_erosion = this.add(new WorldLawAsset
		{
			id = "world_law_erosion",
			group_id = "nature",
			icon_path = "ui/Icons/worldrules/icon_erosion",
			default_state = true
		});
		WorldLawLibrary.world_law_forever_lava = this.add(new WorldLawAsset
		{
			id = "world_law_forever_lava",
			group_id = "weather",
			icon_path = "ui/Icons/worldrules/icon_foreverlava",
			default_state = false
		});
		WorldLawLibrary.world_law_forever_cold = this.add(new WorldLawAsset
		{
			id = "world_law_forever_cold",
			group_id = "weather",
			icon_path = "ui/Icons/iconSnow",
			default_state = false
		});
		WorldLawLibrary.world_law_disasters_nature = this.add(new WorldLawAsset
		{
			id = "world_law_disasters_nature",
			group_id = "disasters",
			icon_path = "ui/Icons/worldrules/icon_disasters",
			default_state = true
		});
		WorldLawLibrary.world_law_clouds = this.add(new WorldLawAsset
		{
			id = "world_law_clouds",
			group_id = "spawn",
			icon_path = "ui/Icons/iconCloud",
			default_state = true
		});
		WorldLawLibrary.world_law_evolution_events = this.add(new WorldLawAsset
		{
			id = "world_law_evolution_events",
			group_id = "other",
			icon_path = "ui/Icons/iconMonolith",
			default_state = true
		});
		WorldLawLibrary.world_law_disasters_other = this.add(new WorldLawAsset
		{
			id = "world_law_disasters_other",
			group_id = "disasters",
			icon_path = "ui/Icons/iconEvilMage",
			default_state = true
		});
		WorldLawLibrary.world_law_rat_plague = this.add(new WorldLawAsset
		{
			id = "world_law_rat_plague",
			group_id = "disasters",
			icon_path = "ui/Icons/iconRatKing",
			default_state = false
		});
		WorldLawLibrary.world_law_angry_civilians = this.add(new WorldLawAsset
		{
			id = "world_law_angry_civilians",
			group_id = "civilizations",
			icon_path = "ui/Icons/worldrules/icon_angryvillagers",
			default_state = false
		});
		WorldLawLibrary.world_law_civ_babies = this.add(new WorldLawAsset
		{
			id = "world_law_civ_babies",
			group_id = "civilizations",
			icon_path = "ui/Icons/worldrules/icon_lastofus",
			default_state = true
		});
		WorldLawLibrary.world_law_civ_migrants = this.add(new WorldLawAsset
		{
			id = "world_law_civ_migrants",
			group_id = "civilizations",
			icon_path = "ui/Icons/worldrules/icon_migrants",
			default_state = true
		});
		WorldLawLibrary.world_law_forever_tumor_creep = this.add(new WorldLawAsset
		{
			id = "world_law_forever_tumor_creep",
			group_id = "mobs",
			icon_path = "ui/Icons/iconTumor",
			default_state = false
		});
		WorldLawLibrary.world_law_civ_army = this.add(new WorldLawAsset
		{
			id = "world_law_civ_army",
			group_id = "civilizations",
			icon_path = "ui/Icons/iconArmyList",
			default_state = true
		});
		WorldLawLibrary.world_law_civ_limit_population_100 = this.add(new WorldLawAsset
		{
			id = "world_law_civ_limit_population_100",
			group_id = "harmony",
			icon_path = "ui/Icons/iconPopulation100",
			default_state = false
		});
		WorldLawLibrary.world_law_gaias_covenant = this.add(new WorldLawAsset
		{
			id = "world_law_gaias_covenant",
			group_id = "harmony",
			icon_path = "ui/Icons/worldrules/icon_gaias_covenant",
			default_state = false
		});
		WorldLawLibrary.world_law_cursed_world = this.add(new WorldLawAsset
		{
			id = "world_law_cursed_world",
			icon_path = "ui/Icons/worldrules/icon_cursed_world",
			default_state = false,
			can_turn_off = false,
			requires_premium = true,
			on_state_change = new PlayerOptionAction(this.checkCursedWorld),
			on_state_enabled = new PlayerOptionAction(this.turnOnCursedWorld),
			on_world_load = new OnWorldLoadAction(this.checkAlreadyCursed)
		});
	}

	// Token: 0x06000CB0 RID: 3248 RVA: 0x000B8B58 File Offset: 0x000B6D58
	private void addFlora()
	{
		WorldLawLibrary.world_law_spread_trees = this.add(new WorldLawAsset
		{
			id = "world_law_spread_trees",
			group_id = "trees",
			icon_path = "ui/Icons/worldrules/icon_grow_trees",
			default_state = true
		});
		WorldLawLibrary.world_law_spread_fungi = this.add(new WorldLawAsset
		{
			id = "world_law_spread_fungi",
			group_id = "fungi",
			icon_path = "ui/Icons/worldrules/icon_grow_fungi",
			default_state = true
		});
		WorldLawLibrary.world_law_spread_plants = this.add(new WorldLawAsset
		{
			id = "world_law_spread_plants",
			group_id = "plants",
			icon_path = "ui/Icons/worldrules/icon_grow_plants",
			default_state = true
		});
		WorldLawLibrary.world_law_spread_fast_trees = this.add(new WorldLawAsset
		{
			id = "world_law_spread_fast_trees",
			group_id = "trees",
			icon_path = "ui/Icons/worldrules/icon_grow_trees_fast",
			default_state = false
		});
		WorldLawLibrary.world_law_spread_fast_fungi = this.add(new WorldLawAsset
		{
			id = "world_law_spread_fast_fungi",
			group_id = "fungi",
			icon_path = "ui/Icons/worldrules/icon_grow_fungi_fast",
			default_state = false
		});
		WorldLawLibrary.world_law_spread_fast_plants = this.add(new WorldLawAsset
		{
			id = "world_law_spread_fast_plants",
			group_id = "plants",
			icon_path = "ui/Icons/worldrules/icon_grow_plants_fast",
			default_state = false
		});
		WorldLawLibrary.world_law_spread_density_high = this.add(new WorldLawAsset
		{
			id = "world_law_spread_density_high",
			group_id = "nature",
			icon_path = "ui/Icons/worldrules/icon_flora_density_high",
			default_state = false
		});
		WorldLawAsset worldLawAsset = new WorldLawAsset();
		worldLawAsset.id = "world_law_exploding_mushrooms";
		worldLawAsset.group_id = "fungi";
		worldLawAsset.icon_path = "ui/Icons/worldrules/icon_exploding_mushrooms";
		worldLawAsset.default_state = false;
		worldLawAsset.on_state_change = delegate(PlayerOptionData pOption)
		{
			if (!pOption.boolVal)
			{
				return;
			}
			World.world.map_stats.exploding_mushrooms_enabled_at = World.world.getCurWorldTime();
		};
		WorldLawLibrary.world_law_exploding_mushrooms = this.add(worldLawAsset);
		WorldLawLibrary.world_law_entanglewood = this.add(new WorldLawAsset
		{
			id = "world_law_entanglewood",
			group_id = "trees",
			icon_path = "ui/Icons/worldrules/icon_entanglewood",
			default_state = true
		});
		WorldLawLibrary.world_law_bark_bites_back = this.add(new WorldLawAsset
		{
			id = "world_law_bark_bites_back",
			group_id = "trees",
			icon_path = "ui/Icons/worldrules/icon_bark_bites_back",
			default_state = false
		});
		WorldLawLibrary.world_law_plants_tickles = this.add(new WorldLawAsset
		{
			id = "world_law_plants_tickles",
			group_id = "plants",
			icon_path = "ui/Icons/worldrules/icon_plants_tickles",
			default_state = false
		});
		WorldLawLibrary.world_law_root_pranks = this.add(new WorldLawAsset
		{
			id = "world_law_root_pranks",
			group_id = "plants",
			icon_path = "ui/Icons/worldrules/icon_root_pranks",
			default_state = false
		});
		WorldLawLibrary.world_law_nectar_nap = this.add(new WorldLawAsset
		{
			id = "world_law_nectar_nap",
			group_id = "plants",
			icon_path = "ui/Icons/worldrules/icon_nectar_nap",
			default_state = false
		});
	}

	// Token: 0x06000CB1 RID: 3249 RVA: 0x000B8E64 File Offset: 0x000B7064
	public override void editorDiagnostic()
	{
		base.editorDiagnostic();
		foreach (WorldLawAsset tAsset in this.list)
		{
			base.checkSpriteExists("icon_path", tAsset.icon_path, tAsset);
		}
	}

	// Token: 0x06000CB2 RID: 3250 RVA: 0x000B8ECC File Offset: 0x000B70CC
	public override void editorDiagnosticLocales()
	{
		foreach (WorldLawAsset tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
			this.checkLocale(tAsset, tAsset.getDescriptionID());
			this.checkLocale(tAsset, tAsset.getDescriptionID2());
		}
		base.editorDiagnosticLocales();
	}

	// Token: 0x06000CB3 RID: 3251 RVA: 0x000B8F48 File Offset: 0x000B7148
	private void checkDiplomacy(PlayerOptionData pOption)
	{
		if (WorldLawLibrary.world_law_diplomacy.isEnabled())
		{
			return;
		}
		World.world.stopAttacksFor(false);
		World.world.wars.stopAllWars();
	}

	// Token: 0x06000CB4 RID: 3252 RVA: 0x000B8F71 File Offset: 0x000B7171
	private void checkPeacefulMonsters(PlayerOptionData pOption)
	{
		if (!WorldLawLibrary.world_law_peaceful_monsters.isEnabled())
		{
			return;
		}
		World.world.stopAttacksFor(true);
	}

	// Token: 0x06000CB5 RID: 3253 RVA: 0x000B8F8B File Offset: 0x000B718B
	private void turnOnCursedWorld(PlayerOptionData pOption)
	{
		CursedSacrifice.justCursedWorld();
	}

	// Token: 0x06000CB6 RID: 3254 RVA: 0x000B8F92 File Offset: 0x000B7192
	private void checkCursedWorld(PlayerOptionData pOption)
	{
		PowerButton.checkActorSpawnButtons();
	}

	// Token: 0x06000CB7 RID: 3255 RVA: 0x000B8F99 File Offset: 0x000B7199
	private void checkAlreadyCursed()
	{
		if (!WorldLawLibrary.world_law_cursed_world.isEnabled())
		{
			return;
		}
		CursedSacrifice.loadAlreadyCursedState();
	}

	// Token: 0x06000CB8 RID: 3256 RVA: 0x000B8FAD File Offset: 0x000B71AD
	public static float getIntervalSpreadTrees()
	{
		if (WorldLawLibrary.world_law_spread_fast_trees.isEnabled())
		{
			return 10f;
		}
		return 50f;
	}

	// Token: 0x06000CB9 RID: 3257 RVA: 0x000B8FC6 File Offset: 0x000B71C6
	public static float getIntervalSpreadPlants()
	{
		if (WorldLawLibrary.world_law_spread_fast_plants.isEnabled())
		{
			return 10f;
		}
		return 40f;
	}

	// Token: 0x06000CBA RID: 3258 RVA: 0x000B8FDF File Offset: 0x000B71DF
	public static float getIntervalSpreadFungi()
	{
		if (WorldLawLibrary.world_law_spread_fast_fungi.isEnabled())
		{
			return 10f;
		}
		return 30f;
	}

	// Token: 0x06000CBB RID: 3259 RVA: 0x000B8FF8 File Offset: 0x000B71F8
	public string addToGameplayReport(string pWhatFor)
	{
		string tResult = string.Empty;
		tResult = tResult + pWhatFor + "\n";
		foreach (WorldLawAsset worldLawAsset in this.list)
		{
			string tName = worldLawAsset.getTranslatedName();
			string tDescription = worldLawAsset.getTranslatedDescription();
			string tDescription2 = worldLawAsset.getTranslatedDescription2();
			string tLineInfo = "\n" + tName;
			tLineInfo += "\n";
			if (!string.IsNullOrEmpty(tDescription))
			{
				tLineInfo = tLineInfo + "1: " + tDescription;
			}
			if (!string.IsNullOrEmpty(tDescription2))
			{
				tLineInfo = tLineInfo + "\n2: " + tDescription2;
			}
			tResult += tLineInfo;
		}
		tResult += "\n\n";
		return tResult;
	}

	// Token: 0x04000C28 RID: 3112
	public static WorldLawAsset world_law_diplomacy;

	// Token: 0x04000C29 RID: 3113
	public static WorldLawAsset world_law_rites;

	// Token: 0x04000C2A RID: 3114
	public static WorldLawAsset world_law_peaceful_monsters;

	// Token: 0x04000C2B RID: 3115
	public static WorldLawAsset world_law_hunger;

	// Token: 0x04000C2C RID: 3116
	public static WorldLawAsset world_law_vegetation_random_seeds;

	// Token: 0x04000C2D RID: 3117
	public static WorldLawAsset world_law_roots_without_borders;

	// Token: 0x04000C2E RID: 3118
	public static WorldLawAsset world_law_spread_trees;

	// Token: 0x04000C2F RID: 3119
	public static WorldLawAsset world_law_spread_fungi;

	// Token: 0x04000C30 RID: 3120
	public static WorldLawAsset world_law_spread_plants;

	// Token: 0x04000C31 RID: 3121
	public static WorldLawAsset world_law_spread_fast_trees;

	// Token: 0x04000C32 RID: 3122
	public static WorldLawAsset world_law_spread_fast_fungi;

	// Token: 0x04000C33 RID: 3123
	public static WorldLawAsset world_law_spread_fast_plants;

	// Token: 0x04000C34 RID: 3124
	public static WorldLawAsset world_law_exploding_mushrooms;

	// Token: 0x04000C35 RID: 3125
	public static WorldLawAsset world_law_entanglewood;

	// Token: 0x04000C36 RID: 3126
	public static WorldLawAsset world_law_bark_bites_back;

	// Token: 0x04000C37 RID: 3127
	public static WorldLawAsset world_law_plants_tickles;

	// Token: 0x04000C38 RID: 3128
	public static WorldLawAsset world_law_root_pranks;

	// Token: 0x04000C39 RID: 3129
	public static WorldLawAsset world_law_nectar_nap;

	// Token: 0x04000C3A RID: 3130
	public static WorldLawAsset world_law_spread_density_high;

	// Token: 0x04000C3B RID: 3131
	public static WorldLawAsset world_law_grow_minerals;

	// Token: 0x04000C3C RID: 3132
	public static WorldLawAsset world_law_grow_grass;

	// Token: 0x04000C3D RID: 3133
	public static WorldLawAsset world_law_biome_overgrowth;

	// Token: 0x04000C3E RID: 3134
	public static WorldLawAsset world_law_kingdom_expansion;

	// Token: 0x04000C3F RID: 3135
	public static WorldLawAsset world_law_old_age;

	// Token: 0x04000C40 RID: 3136
	public static WorldLawAsset world_law_animals_spawn;

	// Token: 0x04000C41 RID: 3137
	public static WorldLawAsset world_law_animals_babies;

	// Token: 0x04000C42 RID: 3138
	public static WorldLawAsset world_law_rebellions;

	// Token: 0x04000C43 RID: 3139
	public static WorldLawAsset world_law_border_stealing;

	// Token: 0x04000C44 RID: 3140
	public static WorldLawAsset world_law_erosion;

	// Token: 0x04000C45 RID: 3141
	public static WorldLawAsset world_law_forever_lava;

	// Token: 0x04000C46 RID: 3142
	public static WorldLawAsset world_law_forever_cold;

	// Token: 0x04000C47 RID: 3143
	public static WorldLawAsset world_law_disasters_nature;

	// Token: 0x04000C48 RID: 3144
	public static WorldLawAsset world_law_disasters_other;

	// Token: 0x04000C49 RID: 3145
	public static WorldLawAsset world_law_rat_plague;

	// Token: 0x04000C4A RID: 3146
	public static WorldLawAsset world_law_angry_civilians;

	// Token: 0x04000C4B RID: 3147
	public static WorldLawAsset world_law_civ_babies;

	// Token: 0x04000C4C RID: 3148
	public static WorldLawAsset world_law_civ_migrants;

	// Token: 0x04000C4D RID: 3149
	public static WorldLawAsset world_law_forever_tumor_creep;

	// Token: 0x04000C4E RID: 3150
	public static WorldLawAsset world_law_civ_army;

	// Token: 0x04000C4F RID: 3151
	public static WorldLawAsset world_law_civ_limit_population_100;

	// Token: 0x04000C50 RID: 3152
	public static WorldLawAsset world_law_gaias_covenant;

	// Token: 0x04000C51 RID: 3153
	public static WorldLawAsset world_law_clouds;

	// Token: 0x04000C52 RID: 3154
	public static WorldLawAsset world_law_evolution_events;

	// Token: 0x04000C53 RID: 3155
	public static WorldLawAsset world_law_terramorphing;

	// Token: 0x04000C54 RID: 3156
	public static WorldLawAsset world_law_gene_spaghetti;

	// Token: 0x04000C55 RID: 3157
	public static WorldLawAsset world_law_mutant_box;

	// Token: 0x04000C56 RID: 3158
	public static WorldLawAsset world_law_glitched_noosphere;

	// Token: 0x04000C57 RID: 3159
	public static WorldLawAsset world_law_drop_of_thoughts;

	// Token: 0x04000C58 RID: 3160
	public static WorldLawAsset world_law_cursed_world;
}
