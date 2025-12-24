using System;
using System.Collections.Generic;

// Token: 0x020001B0 RID: 432
public class WorldAgeLibrary : AssetLibrary<WorldAgeAsset>
{
	// Token: 0x06000C8A RID: 3210 RVA: 0x000B6F30 File Offset: 0x000B5130
	public override void init()
	{
		base.init();
		WorldAgeLibrary.hope = this.add(new WorldAgeAsset
		{
			id = "age_hope",
			path_icon = "ui/Icons/ages/iconAgeHope",
			rate = 10,
			flag_light_age = true,
			global_unfreeze_world = true,
			cloud_interval = 25f,
			temperature_damage_bonus = 7,
			bonus_loyalty = 15,
			bonus_opinion = 15,
			title_color = Toolbox.makeColor("#86BC4E"),
			flag_light_damage = true
		});
		this.t.biomes = AssetLibrary<WorldAgeAsset>.h<string>(new string[]
		{
			"biome_grass"
		});
		this.t.clouds = AssetLibrary<WorldAgeAsset>.l<string>(new string[]
		{
			"cloud_normal"
		});
		this.t.default_slots = AssetLibrary<WorldAgeAsset>.l<int>(new int[]
		{
			1,
			3,
			5,
			6
		});
		this.add(new WorldAgeAsset
		{
			id = "age_sun",
			path_icon = "ui/Icons/ages/iconAgeSun",
			rate = 5,
			overlay_sun = true,
			era_disaster_fire_elemental_spawn_on_fire = true,
			fire_elemental_spawn_chance = 0.01f,
			era_effect_overlay_alpha = 0.1f,
			temperature_damage_bonus = 15,
			global_unfreeze_world = true,
			global_unfreeze_world_mountains = true,
			particles_sun = true,
			title_color = Toolbox.makeColor("#F7A42A"),
			bonus_loyalty = 5,
			bonus_opinion = 5,
			bonus_biomes_growth = 3,
			grow_vegetation = false,
			fire_spread_rate_bonus = 2f,
			flag_light_damage = true,
			special_effect_interval = 7f
		});
		this.t.special_effect_action = new WorldAgeAction(this.droughtAction);
		this.t.biomes = AssetLibrary<WorldAgeAsset>.h<string>(new string[]
		{
			"biome_desert",
			"biome_savanna"
		});
		this.t.clouds = AssetLibrary<WorldAgeAsset>.l<string>(new string[]
		{
			"cloud_normal"
		});
		this.add(new WorldAgeAsset
		{
			id = "age_dark",
			overlay_darkness = true,
			path_icon = "ui/Icons/ages/iconAgeDark",
			era_effect_overlay_alpha = 0.3f,
			overlay_night = true,
			conditions = AssetLibrary<WorldAgeAsset>.a<string>(new string[]
			{
				"age_hope",
				"age_sun",
				"age_wonders",
				"age_tears"
			}),
			rate = 3,
			global_unfreeze_world = true,
			temperature_damage_bonus = -4,
			flag_crops_grow = false,
			flag_night = true,
			title_color = Toolbox.makeColor("#A4A9B2"),
			range_weapons_multiplier = 0.5f,
			bonus_loyalty = -5,
			bonus_opinion = -5,
			bonus_biomes_growth = -2
		});
		this.t.biomes = AssetLibrary<WorldAgeAsset>.h<string>(new string[]
		{
			"biome_corrupted"
		});
		this.t.clouds = AssetLibrary<WorldAgeAsset>.l<string>(new string[]
		{
			"cloud_normal"
		});
		this.t.default_slots = AssetLibrary<WorldAgeAsset>.l<int>(new int[]
		{
			2,
			7
		});
		this.t.link_default_slots = true;
		this.add(new WorldAgeAsset
		{
			id = "age_tears",
			particles_rain = true,
			path_icon = "ui/Icons/ages/iconAgeTears",
			conditions = AssetLibrary<WorldAgeAsset>.a<string>(new string[]
			{
				"age_hope",
				"age_sun",
				"age_wonders"
			}),
			rate = 3,
			global_unfreeze_world = true,
			overlay_rain = true,
			era_effect_overlay_alpha = 0.3f,
			cloud_interval = 20f,
			special_effect_interval = 5f,
			title_color = Toolbox.makeColor("#6C97D8"),
			bonus_biomes_growth = 5
		});
		this.t.special_effect_action = new WorldAgeAction(this.globalRainAction);
		WorldAgeAsset t = this.t;
		t.special_effect_action = (WorldAgeAction)Delegate.Combine(t.special_effect_action, new WorldAgeAction(this.trySpawnThunder));
		WorldAgeAsset t2 = this.t;
		t2.special_effect_action = (WorldAgeAction)Delegate.Combine(t2.special_effect_action, new WorldAgeAction(delegate()
		{
			this.damageHydrophobicUnits(false);
		}));
		this.t.biomes = AssetLibrary<WorldAgeAsset>.h<string>(new string[]
		{
			"biome_swamp",
			"biome_mushroom",
			"biome_jungle"
		});
		this.t.clouds = Toolbox.splitStringIntoList(new string[]
		{
			"cloud_rain#5",
			"cloud_lightning#1",
			"cloud_normal#2"
		});
		this.t.default_slots = AssetLibrary<WorldAgeAsset>.l<int>(new int[]
		{
			2,
			7
		});
		this.t.link_default_slots = true;
		this.add(new WorldAgeAsset
		{
			id = "age_moon",
			path_icon = "ui/Icons/ages/iconAgeMoon",
			overlay_darkness = true,
			flag_moon = true,
			overlay_moon = true,
			global_unfreeze_world = true,
			temperature_damage_bonus = -3,
			conditions = AssetLibrary<WorldAgeAsset>.a<string>(new string[]
			{
				"age_hope",
				"age_sun"
			}),
			rate = 3,
			light_color = Toolbox.makeColor("#8DFFF3"),
			title_color = Toolbox.makeColor("#B5FAFF")
		});
		this.t.default_slots = AssetLibrary<WorldAgeAsset>.l<int>(new int[]
		{
			2,
			7
		});
		this.t.biomes = AssetLibrary<WorldAgeAsset>.h<string>(new string[]
		{
			"biome_crystal"
		});
		this.t.clouds = Toolbox.splitStringIntoList(new string[]
		{
			"cloud_rain#5",
			"cloud_normal#5"
		});
		this.add(new WorldAgeAsset
		{
			id = "age_chaos",
			overlay_chaos = true,
			flag_chaos = true,
			era_disaster_rage_brings_demons = true,
			path_icon = "ui/Icons/ages/iconAgeChaos",
			conditions = AssetLibrary<WorldAgeAsset>.a<string>(new string[]
			{
				"age_hope",
				"age_sun",
				"age_wonders"
			}),
			era_effect_overlay_alpha = 0.35f,
			rate = 2,
			global_unfreeze_world = true,
			global_unfreeze_world_mountains = true,
			title_color = Toolbox.makeColor("#E6503A"),
			bonus_loyalty = -55,
			bonus_opinion = -35,
			flag_light_damage = true
		});
		this.t.biomes = AssetLibrary<WorldAgeAsset>.h<string>(new string[]
		{
			"biome_infernal"
		});
		this.t.clouds = AssetLibrary<WorldAgeAsset>.l<string>(new string[]
		{
			"cloud_rage"
		});
		this.add(new WorldAgeAsset
		{
			id = "age_wonders",
			path_icon = "ui/Icons/ages/iconAgeWonders",
			rate = 2,
			flag_light_age = true,
			particles_magic = true,
			overlay_magic = true,
			era_effect_overlay_alpha = 0.15f,
			global_unfreeze_world = true,
			global_unfreeze_world_mountains = true,
			title_color = Toolbox.makeColor("#D6559C")
		});
		this.t.biomes = AssetLibrary<WorldAgeAsset>.h<string>(new string[]
		{
			"biome_enchanted",
			"biome_candy"
		});
		this.t.clouds = Toolbox.splitStringIntoList(new string[]
		{
			"cloud_magic#5",
			"cloud_normal#1"
		});
		this.t.default_slots = AssetLibrary<WorldAgeAsset>.l<int>(new int[]
		{
			2,
			7
		});
		this.t.link_default_slots = true;
		this.add(new WorldAgeAsset
		{
			id = "age_ice",
			particles_snow = true,
			path_icon = "ui/Icons/ages/iconAgeIce",
			conditions = AssetLibrary<WorldAgeAsset>.a<string>(new string[]
			{
				"age_hope",
				"age_sun",
				"age_wonders"
			}),
			rate = 2,
			global_freeze_world = true,
			overlay_winter = true,
			flag_winter = true,
			era_effect_overlay_alpha = 0.2f,
			temperature_damage_bonus = 5,
			flag_crops_grow = false,
			cloud_interval = 20f,
			title_color = Toolbox.makeColor("#C1FAFF"),
			bonus_biomes_growth = -20,
			years_min = 30,
			years_max = 40
		});
		this.t.special_effect_action = delegate()
		{
			this.damageHydrophobicUnits(true);
		};
		WorldAgeAsset t3 = this.t;
		t3.special_effect_action = (WorldAgeAction)Delegate.Combine(t3.special_effect_action, new WorldAgeAction(this.frostingAction));
		this.t.biomes = AssetLibrary<WorldAgeAsset>.h<string>(new string[]
		{
			"biome_permafrost"
		});
		this.t.clouds = AssetLibrary<WorldAgeAsset>.l<string>(new string[]
		{
			"cloud_snow"
		});
		this.add(new WorldAgeAsset
		{
			id = "age_ash",
			path_icon = "ui/Icons/ages/iconAgeAsh",
			conditions = AssetLibrary<WorldAgeAsset>.a<string>(new string[]
			{
				"age_hope",
				"age_sun",
				"age_wonders"
			}),
			rate = 2,
			overlay_ash = true,
			particles_ash = true,
			era_effect_overlay_alpha = 0.4f,
			global_unfreeze_world = true,
			cloud_interval = 15f,
			title_color = Toolbox.makeColor("#DDC49D"),
			bonus_loyalty = -25,
			bonus_opinion = -10,
			bonus_biomes_growth = -4
		});
		this.t.clouds = AssetLibrary<WorldAgeAsset>.l<string>(new string[]
		{
			"cloud_ash"
		});
		this.add(new WorldAgeAsset
		{
			id = "age_despair",
			overlay_darkness = true,
			particles_snow = true,
			path_icon = "ui/Icons/ages/iconAgeDespair",
			conditions = AssetLibrary<WorldAgeAsset>.a<string>(new string[]
			{
				"age_dark",
				"age_ice"
			}),
			force_next = "age_hope",
			rate = 4,
			overlay_winter = true,
			flag_winter = true,
			overlay_night = true,
			era_effect_overlay_alpha = 0.25f,
			global_freeze_world = true,
			temperature_damage_bonus = 3,
			flag_crops_grow = false,
			flag_night = true,
			cloud_interval = 16f,
			title_color = Toolbox.makeColor("#728599"),
			era_disaster_snow_turns_babies_into_ice_ones = true,
			range_weapons_multiplier = 0.5f,
			bonus_loyalty = -10,
			bonus_opinion = -10,
			bonus_biomes_growth = -2,
			years_min = 30,
			years_max = 40
		});
		WorldAgeAsset t4 = this.t;
		t4.special_effect_action = (WorldAgeAction)Delegate.Combine(t4.special_effect_action, new WorldAgeAction(delegate()
		{
			this.damageHydrophobicUnits(true);
		}));
		WorldAgeAsset t5 = this.t;
		t5.special_effect_action = (WorldAgeAction)Delegate.Combine(t5.special_effect_action, new WorldAgeAction(this.frostingAction));
		this.t.biomes = AssetLibrary<WorldAgeAsset>.h<string>(new string[]
		{
			"biome_permafrost",
			"biome_corrupted"
		});
		this.t.clouds = AssetLibrary<WorldAgeAsset>.l<string>(new string[]
		{
			"cloud_snow"
		});
		this.add(new WorldAgeAsset
		{
			id = "age_unknown",
			path_icon = "ui/Icons/ages/iconAgeUnknown",
			title_color = Toolbox.makeColor("#AAAAAA")
		});
		this.t.default_slots = AssetLibrary<WorldAgeAsset>.l<int>(new int[]
		{
			4,
			8
		});
	}

	// Token: 0x06000C8B RID: 3211 RVA: 0x000B7A64 File Offset: 0x000B5C64
	public override void post_init()
	{
		base.post_init();
		foreach (WorldAgeAsset tAsset in this.list)
		{
			tAsset.path_background = "ui/AgeWheel/backgrounds/" + tAsset.id + "_background";
		}
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x000B7AD4 File Offset: 0x000B5CD4
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (WorldAgeAsset tAsset in this.list)
		{
			foreach (int tSlot in tAsset.default_slots)
			{
				if (!this.pool_by_slots.ContainsKey(tSlot))
				{
					this.pool_by_slots.Add(tSlot, new List<WorldAgeAsset>());
				}
				this.pool_by_slots[tSlot].Add(tAsset);
			}
		}
		this.list_only_normal = this.list.FindAll((WorldAgeAsset pAsset) => pAsset.id != "age_unknown");
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x000B7BC4 File Offset: 0x000B5DC4
	public void damageHydrophobicUnits(bool pFrost = false)
	{
		foreach (Subspecies tSubspecies in World.world.subspecies.list)
		{
			if (tSubspecies.is_damaged_by_water)
			{
				foreach (Actor tActor in tSubspecies.units)
				{
					if (tActor.isAlive() && !tActor.isInsideSomething() && (!pFrost || !tActor.has_tag_immunity_cold) && Randy.randomChance(0.5f))
					{
						tActor.getHit((float)tActor.getWaterDamage(), true, AttackType.Water, null, true, false, true);
					}
				}
			}
		}
	}

	// Token: 0x06000C8E RID: 3214 RVA: 0x000B7C9C File Offset: 0x000B5E9C
	public void droughtAction()
	{
		this.extremeEnvironmentAction(new EnvironmentAction(this.droughtCheck));
	}

	// Token: 0x06000C8F RID: 3215 RVA: 0x000B7CB0 File Offset: 0x000B5EB0
	private bool droughtCheck(BuildingAsset pAsset)
	{
		return pAsset.affected_by_drought && !(pAsset.type != "type_tree");
	}

	// Token: 0x06000C90 RID: 3216 RVA: 0x000B7CD1 File Offset: 0x000B5ED1
	public void frostingAction()
	{
		this.extremeEnvironmentAction(new EnvironmentAction(this.frostCheck));
	}

	// Token: 0x06000C91 RID: 3217 RVA: 0x000B7CE5 File Offset: 0x000B5EE5
	private bool frostCheck(BuildingAsset pAsset)
	{
		return pAsset.affected_by_cold_temperature && (!(pAsset.type != "type_tree") || !(pAsset.type != "type_vegetation"));
	}

	// Token: 0x06000C92 RID: 3218 RVA: 0x000B7D18 File Offset: 0x000B5F18
	private void extremeEnvironmentAction(EnvironmentAction pCheckAsset)
	{
		foreach (Building tBuilding in World.world.kingdoms_wild.get("nature").buildings.LoopRandom(5))
		{
			if (tBuilding.isAlive() && !tBuilding.isRuin() && pCheckAsset(tBuilding.asset))
			{
				tBuilding.startMakingRuins();
			}
		}
	}

	// Token: 0x06000C93 RID: 3219 RVA: 0x000B7D9C File Offset: 0x000B5F9C
	public void globalRainAction()
	{
		if (WorldBehaviourActionFire.hasFires())
		{
			return;
		}
		List<Actor> tList = World.world.units.getSimpleList();
		for (int i = 0; i < tList.Count; i++)
		{
			Actor tActor = tList[i];
			if (tActor.isAlive() && tActor.hasStatus("burning") && Randy.randomChance(0.9f))
			{
				tActor.finishStatusEffect("burning");
			}
		}
		List<Building> tBuildingList = World.world.buildings.getSimpleList();
		for (int j = 0; j < tBuildingList.Count; j++)
		{
			Building tBuilding = tBuildingList[j];
			if (tBuilding.isAlive() && Randy.randomChance(0.9f))
			{
				tBuilding.stopFire();
			}
		}
	}

	// Token: 0x06000C94 RID: 3220 RVA: 0x000B7E54 File Offset: 0x000B6054
	public void trySpawnThunder()
	{
		if (!MapBox.isRenderGameplay())
		{
			return;
		}
		if (!Randy.randomChance(0.1f))
		{
			return;
		}
		float worldTimeElapsedSince = World.world.getWorldTimeElapsedSince(StackEffects.last_thunder_timestamp);
		float tElapsedMax = 100f * Config.time_scale_asset.multiplier;
		if (tElapsedMax > 600f)
		{
			tElapsedMax = 600f;
		}
		if (worldTimeElapsedSince > tElapsedMax || StackEffects.last_thunder_timestamp == 0.0)
		{
			EffectsLibrary.spawn("fx_thunder_flash", null, null, null, 0f, -1f, -1f, null);
			StackEffects.last_thunder_timestamp = World.world.getCurWorldTime();
		}
	}

	// Token: 0x06000C95 RID: 3221 RVA: 0x000B7EE4 File Offset: 0x000B60E4
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (WorldAgeAsset tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
			this.checkLocale(tAsset, tAsset.getDescriptionID());
		}
	}

	// Token: 0x04000C1D RID: 3101
	[NonSerialized]
	public List<WorldAgeAsset> list_only_normal;

	// Token: 0x04000C1E RID: 3102
	[NonSerialized]
	public Dictionary<int, List<WorldAgeAsset>> pool_by_slots = new Dictionary<int, List<WorldAgeAsset>>();

	// Token: 0x04000C1F RID: 3103
	public static WorldAgeAsset hope;
}
