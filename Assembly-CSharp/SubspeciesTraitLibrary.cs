using System;
using System.Collections.Generic;
using strings;

// Token: 0x020001A5 RID: 421
public class SubspeciesTraitLibrary : BaseTraitLibrary<SubspeciesTrait>
{
	// Token: 0x1700005F RID: 95
	// (get) Token: 0x06000C41 RID: 3137 RVA: 0x000AFDF8 File Offset: 0x000ADFF8
	protected override string icon_path
	{
		get
		{
			return "ui/Icons/subspecies_traits/";
		}
	}

	// Token: 0x06000C42 RID: 3138 RVA: 0x000AFDFF File Offset: 0x000ADFFF
	protected override List<string> getDefaultTraitsForMeta(ActorAsset pAsset)
	{
		return pAsset.default_subspecies_traits;
	}

	// Token: 0x06000C43 RID: 3139 RVA: 0x000AFE08 File Offset: 0x000AE008
	public override void init()
	{
		base.init();
		this.addMetamorphosis();
		this.addSpawnSomething();
		this.addLimits();
		this.addMaturation();
		this.addStats();
		this.addGenetic();
		this.addDiet();
		this.addReproduction();
		this.addReproductionModes();
		this.addOther();
		this.addSleepCycles();
		this.addMagic();
		this.addChaos();
		this.addPhenotypes();
		this.addAdaptations();
		this.addMutations();
		this.addEggs();
	}

	// Token: 0x06000C44 RID: 3140 RVA: 0x000AFE84 File Offset: 0x000AE084
	private void addMagic()
	{
		this.add(new SubspeciesTrait
		{
			id = "$magic_blood$",
			group_id = "talents"
		});
		this.t.base_stats_meta.addTag("magic");
		SubspeciesTrait t = this.t;
		t.action_death = (WorldAction)Delegate.Combine(t.action_death, new WorldAction(ActionLibrary.mageSlayerCheck));
		this.clone("gift_of_fire", "$magic_blood$");
		this.t.rarity = Rarity.R2_Epic;
		this.t.addSpell("cast_fire");
		this.clone("gift_of_thunder", "$magic_blood$");
		this.t.addSpell("summon_lightning");
		this.clone("gift_of_void", "$magic_blood$");
		this.t.addSpell("teleport");
		this.clone("gift_of_air", "$magic_blood$");
		this.t.addSpell("summon_tornado");
		this.clone("gift_of_blood", "$magic_blood$");
		this.t.rarity = Rarity.R0_Normal;
		this.t.addSpell("cast_blood_rain");
		this.clone("gift_of_harmony", "$magic_blood$");
		this.t.addSpell("cast_blood_rain");
		this.t.addSpell("cast_cure");
		this.clone("gift_of_water", "$magic_blood$");
		this.t.rarity = Rarity.R1_Rare;
		this.t.addSpell("cast_shield");
		this.clone("gift_of_life", "$magic_blood$");
		this.t.rarity = Rarity.R1_Rare;
		this.t.addSpell("cast_grass_seeds");
		this.t.addSpell("spawn_vegetation");
		this.clone("gift_of_death", "$magic_blood$");
		this.t.addSpell("spawn_skeleton");
		this.t.addSpell("cast_curse");
	}

	// Token: 0x06000C45 RID: 3141 RVA: 0x000B0078 File Offset: 0x000AE278
	private void addChaos()
	{
		this.add(new SubspeciesTrait
		{
			id = "grin_mark",
			group_id = "fate",
			spawn_random_trait_allowed = false,
			priority = -100
		});
		this.t.setTraitInfoToGrinMark();
		this.t.show_for_unlockables_ui = true;
		this.t.setUnlockedWithAchievement("achievementCreaturesExplorer");
		SubspeciesTrait subspeciesTrait = new SubspeciesTrait();
		subspeciesTrait.id = "annoying_fireworks";
		subspeciesTrait.group_id = "chaos";
		subspeciesTrait.rarity = Rarity.R0_Normal;
		subspeciesTrait.action_death = delegate(BaseSimObject _, WorldTile pTile)
		{
			EffectsLibrary.spawn("fx_fireworks", pTile, null, null, 0f, -1f, -1f, null);
			return true;
		};
		this.add(subspeciesTrait);
		this.add(new SubspeciesTrait
		{
			id = "spicy_kids",
			group_id = "chaos",
			action_birth = new WorldAction(ActionLibrary.fireDropsSpawn)
		});
		SubspeciesTrait subspeciesTrait2 = new SubspeciesTrait();
		subspeciesTrait2.id = "nimble";
		subspeciesTrait2.group_id = "chaos";
		subspeciesTrait2.in_mutation_pot_add = true;
		subspeciesTrait2.in_mutation_pot_remove = true;
		subspeciesTrait2.action_attack_target = ((BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile) => pTarget.isActor() && pSelf.a.tryToStealItems(pTarget.a, false));
		this.add(subspeciesTrait2);
		this.t.setUnlockedWithAchievement("achievementNotOnMyWatch");
		this.t.base_stats_meta.addTag("steal_items");
		SubspeciesTrait subspeciesTrait3 = new SubspeciesTrait();
		subspeciesTrait3.id = "antimatter_essence";
		subspeciesTrait3.group_id = "chaos";
		subspeciesTrait3.spawn_random_trait_allowed = false;
		subspeciesTrait3.action_death = delegate(BaseSimObject _, WorldTile pTile)
		{
			DropsLibrary.action_antimatter_bomb(pTile, null);
			return true;
		};
		this.add(subspeciesTrait3);
		this.t.setUnlockedWithAchievement("achievementTntAndHeat");
		SubspeciesTrait subspeciesTrait4 = new SubspeciesTrait();
		subspeciesTrait4.id = "gaia_roots";
		subspeciesTrait4.group_id = "growth";
		subspeciesTrait4.rarity = Rarity.R0_Normal;
		subspeciesTrait4.action_death = delegate(BaseSimObject _, WorldTile pTile)
		{
			if (!WorldLawLibrary.world_law_clouds.isEnabled())
			{
				return false;
			}
			if (Randy.randomChance(0.3f))
			{
				EffectsLibrary.spawn("fx_cloud", pTile, "cloud_normal", null, 0f, -1f, -1f, null);
			}
			return true;
		};
		subspeciesTrait4.in_mutation_pot_add = true;
		subspeciesTrait4.in_mutation_pot_remove = true;
		this.add(subspeciesTrait4);
		this.t.setUnlockedWithAchievement("achievementZoo");
		this.add(new SubspeciesTrait
		{
			id = "parental_care",
			group_id = "growth"
		});
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x000B02C8 File Offset: 0x000AE4C8
	private void addMetamorphosis()
	{
		SubspeciesTrait subspeciesTrait = new SubspeciesTrait();
		subspeciesTrait.id = "fire_elemental_form";
		subspeciesTrait.group_id = "chaos";
		subspeciesTrait.rarity = Rarity.R2_Epic;
		subspeciesTrait.action_death = delegate(BaseSimObject pSimObject, WorldTile _)
		{
			Actor tActor = pSimObject.a;
			if (!tActor.isPrettyOld())
			{
				return false;
			}
			ActionLibrary.metamorphInto(tActor, SA.fire_elementals.GetRandom<string>(), false, false);
			return true;
		};
		subspeciesTrait.in_mutation_pot_add = true;
		subspeciesTrait.in_mutation_pot_remove = true;
		this.add(subspeciesTrait);
		this.t.setUnlockedWithAchievement("achievementEternalChaos");
		this.t.addOpposite("fenix_born");
		this.t.addOpposite("metamorphosis_butterfly");
		this.t.addOpposite("metamorphosis_chicken");
		this.t.addOpposite("metamorphosis_crab");
		this.t.addOpposite("metamorphosis_sword");
		this.t.addOpposite("metamorphosis_wolf");
		SubspeciesTrait subspeciesTrait2 = new SubspeciesTrait();
		subspeciesTrait2.id = "fenix_born";
		subspeciesTrait2.group_id = "rebirth";
		subspeciesTrait2.action_death = delegate(BaseSimObject pSimObject, WorldTile _)
		{
			Actor tActor = pSimObject.a;
			if (!tActor.isPrettyOld())
			{
				return false;
			}
			ActionLibrary.metamorphInto(tActor, tActor.asset.id, true, true);
			return true;
		};
		subspeciesTrait2.in_mutation_pot_add = true;
		subspeciesTrait2.in_mutation_pot_remove = true;
		this.add(subspeciesTrait2);
		this.t.setUnlockedWithAchievement("achievementLongLiving");
		this.t.addOpposite("fire_elemental_form");
		this.t.addOpposite("metamorphosis_butterfly");
		this.t.addOpposite("metamorphosis_chicken");
		this.t.addOpposite("metamorphosis_crab");
		this.t.addOpposite("metamorphosis_sword");
		this.t.addOpposite("metamorphosis_wolf");
		SubspeciesTrait subspeciesTrait3 = new SubspeciesTrait();
		subspeciesTrait3.id = "metamorphosis_crab";
		subspeciesTrait3.group_id = "rebirth";
		subspeciesTrait3.rarity = Rarity.R1_Rare;
		subspeciesTrait3.action_death = delegate(BaseSimObject pSimObject, WorldTile _)
		{
			Actor tActor = pSimObject.a;
			if (!tActor.isPrettyOld())
			{
				return false;
			}
			ActionLibrary.metamorphInto(tActor, "crab", false, false);
			return true;
		};
		subspeciesTrait3.in_mutation_pot_add = true;
		subspeciesTrait3.in_mutation_pot_remove = true;
		this.add(subspeciesTrait3);
		this.t.setUnlockedWithAchievement("achievementEngineeredEvolution");
		this.t.addOpposite("fire_elemental_form");
		this.t.addOpposite("fenix_born");
		this.t.addOpposite("metamorphosis_butterfly");
		this.t.addOpposite("metamorphosis_chicken");
		this.t.addOpposite("metamorphosis_sword");
		this.t.addOpposite("metamorphosis_wolf");
		SubspeciesTrait subspeciesTrait4 = new SubspeciesTrait();
		subspeciesTrait4.id = "metamorphosis_chicken";
		subspeciesTrait4.group_id = "rebirth";
		subspeciesTrait4.rarity = Rarity.R0_Normal;
		subspeciesTrait4.action_death = delegate(BaseSimObject pSimObject, WorldTile _)
		{
			Actor tActor = pSimObject.a;
			if (!tActor.isPrettyOld())
			{
				return false;
			}
			ActionLibrary.metamorphInto(tActor, "chicken", false, false);
			return true;
		};
		subspeciesTrait4.in_mutation_pot_add = true;
		subspeciesTrait4.in_mutation_pot_remove = true;
		this.add(subspeciesTrait4);
		this.t.addOpposite("fire_elemental_form");
		this.t.addOpposite("fenix_born");
		this.t.addOpposite("metamorphosis_butterfly");
		this.t.addOpposite("metamorphosis_crab");
		this.t.addOpposite("metamorphosis_sword");
		this.t.addOpposite("metamorphosis_wolf");
		SubspeciesTrait subspeciesTrait5 = new SubspeciesTrait();
		subspeciesTrait5.id = "metamorphosis_wolf";
		subspeciesTrait5.group_id = "rebirth";
		subspeciesTrait5.rarity = Rarity.R0_Normal;
		subspeciesTrait5.action_death = delegate(BaseSimObject pSimObject, WorldTile _)
		{
			Actor tActor = pSimObject.a;
			if (!tActor.isPrettyOld())
			{
				return false;
			}
			ActionLibrary.metamorphInto(tActor, "wolf", false, false);
			return true;
		};
		subspeciesTrait5.in_mutation_pot_add = true;
		subspeciesTrait5.in_mutation_pot_remove = true;
		this.add(subspeciesTrait5);
		this.t.addOpposite("fire_elemental_form");
		this.t.addOpposite("fenix_born");
		this.t.addOpposite("metamorphosis_butterfly");
		this.t.addOpposite("metamorphosis_chicken");
		this.t.addOpposite("metamorphosis_crab");
		this.t.addOpposite("metamorphosis_sword");
		SubspeciesTrait subspeciesTrait6 = new SubspeciesTrait();
		subspeciesTrait6.id = "metamorphosis_butterfly";
		subspeciesTrait6.group_id = "rebirth";
		subspeciesTrait6.rarity = Rarity.R0_Normal;
		subspeciesTrait6.action_death = delegate(BaseSimObject pSimObject, WorldTile _)
		{
			Actor tActor = pSimObject.a;
			if (!tActor.isPrettyOld())
			{
				return false;
			}
			ActionLibrary.metamorphInto(tActor, "butterfly", false, false);
			return true;
		};
		subspeciesTrait6.in_mutation_pot_add = true;
		subspeciesTrait6.in_mutation_pot_remove = true;
		this.add(subspeciesTrait6);
		this.t.setUnlockedWithAchievement("achievementMasterWeaver");
		this.t.addOpposite("fire_elemental_form");
		this.t.addOpposite("fenix_born");
		this.t.addOpposite("metamorphosis_chicken");
		this.t.addOpposite("metamorphosis_crab");
		this.t.addOpposite("metamorphosis_sword");
		this.t.addOpposite("metamorphosis_wolf");
		SubspeciesTrait subspeciesTrait7 = new SubspeciesTrait();
		subspeciesTrait7.id = "metamorphosis_sword";
		subspeciesTrait7.group_id = "rebirth";
		subspeciesTrait7.rarity = Rarity.R1_Rare;
		subspeciesTrait7.action_death = delegate(BaseSimObject pSimObject, WorldTile _)
		{
			Actor tActor = pSimObject.a;
			if (!tActor.isPrettyOld())
			{
				return false;
			}
			ActionLibrary.metamorphInto(tActor, "crystal_sword", false, false);
			return true;
		};
		subspeciesTrait7.in_mutation_pot_add = true;
		subspeciesTrait7.in_mutation_pot_remove = true;
		this.add(subspeciesTrait7);
		this.t.addOpposite("fire_elemental_form");
		this.t.addOpposite("fenix_born");
		this.t.addOpposite("metamorphosis_butterfly");
		this.t.addOpposite("metamorphosis_chicken");
		this.t.addOpposite("metamorphosis_crab");
		this.t.addOpposite("metamorphosis_wolf");
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x000B0834 File Offset: 0x000AEA34
	private void addSpawnSomething()
	{
		this.add(new SubspeciesTrait
		{
			id = "bioproduct_gold",
			group_id = "bioproducts",
			rarity = Rarity.R0_Normal,
			priority = 100,
			is_diet_related = true,
			in_mutation_pot_add = true,
			in_mutation_pot_remove = true
		});
		this.t.setUnlockedWithAchievement("achievementSmellyCity");
		this.add(new SubspeciesTrait
		{
			id = "bioproduct_gems",
			group_id = "bioproducts",
			rarity = Rarity.R0_Normal,
			priority = 100,
			is_diet_related = true,
			in_mutation_pot_add = true,
			in_mutation_pot_remove = true
		});
		this.add(new SubspeciesTrait
		{
			id = "bioproduct_stone",
			group_id = "bioproducts",
			rarity = Rarity.R0_Normal,
			priority = 99,
			is_diet_related = true,
			in_mutation_pot_add = true,
			in_mutation_pot_remove = true
		});
		this.add(new SubspeciesTrait
		{
			id = "bioproduct_mushrooms",
			group_id = "bioproducts",
			rarity = Rarity.R0_Normal,
			priority = 98,
			is_diet_related = true,
			in_mutation_pot_add = true,
			in_mutation_pot_remove = true
		});
		SubspeciesTrait subspeciesTrait = new SubspeciesTrait();
		subspeciesTrait.id = "death_grow_mythril";
		subspeciesTrait.group_id = "growth";
		subspeciesTrait.rarity = Rarity.R1_Rare;
		subspeciesTrait.priority = 97;
		subspeciesTrait.action_death = delegate(BaseSimObject pSimObject, WorldTile pTile)
		{
			if (pSimObject.a.isAdult())
			{
				World.world.buildings.addBuilding("mineral_mythril", pTile, true, false, BuildPlacingType.New);
			}
			return true;
		};
		subspeciesTrait.in_mutation_pot_add = true;
		subspeciesTrait.in_mutation_pot_remove = true;
		this.add(subspeciesTrait);
		this.t.setUnlockedWithAchievement("achievementGen5Worlds");
		this.t.addOpposite("death_grow_tree");
		this.t.addOpposite("death_grow_plant");
		this.add(new SubspeciesTrait
		{
			id = "death_grow_tree",
			group_id = "growth",
			rarity = Rarity.R0_Normal,
			priority = 95,
			action_death = new WorldAction(ActionLibrary.tryToGrowTree),
			in_mutation_pot_add = true,
			in_mutation_pot_remove = true
		});
		this.t.setUnlockedWithAchievement("achievementGen50Worlds");
		this.t.addOpposite("death_grow_plant");
		this.t.addOpposite("death_grow_mythril");
		this.add(new SubspeciesTrait
		{
			id = "death_grow_plant",
			group_id = "growth",
			rarity = Rarity.R0_Normal,
			priority = 96,
			action_death = new WorldAction(ActionLibrary.tryToCreatePlants),
			in_mutation_pot_add = true,
			in_mutation_pot_remove = true
		});
		this.t.setUnlockedWithAchievement("achievementGen100Worlds");
		this.t.addOpposite("death_grow_tree");
		this.t.addOpposite("death_grow_mythril");
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x000B0B00 File Offset: 0x000AED00
	private void addSleepCycles()
	{
		this.add(new SubspeciesTrait
		{
			id = "energy_preserver",
			group_id = "sleep_cycles",
			rarity = Rarity.R1_Rare,
			priority = 100
		});
		this.add(new SubspeciesTrait
		{
			id = "polyphasic_sleep",
			group_id = "sleep_cycles",
			rarity = Rarity.R1_Rare,
			priority = 99
		});
		this.t.addDecision("polyphasic_sleep");
		this.t.addOpposite("monophasic_sleep");
		this.add(new SubspeciesTrait
		{
			id = "monophasic_sleep",
			group_id = "sleep_cycles",
			rarity = Rarity.R1_Rare,
			priority = 98
		});
		this.t.addDecision("monophasic_sleep");
		this.t.addOpposite("polyphasic_sleep");
		this.add(new SubspeciesTrait
		{
			id = "prolonged_rest",
			group_id = "sleep_cycles",
			rarity = Rarity.R1_Rare,
			priority = 97
		});
		this.add(new SubspeciesTrait
		{
			id = "nocturnal_dormancy",
			group_id = "hibernation",
			rarity = Rarity.R2_Epic,
			priority = 100
		});
		this.t.addDecision("sleep_at_dark_age");
		this.t.addOpposite("chaos_driven");
		this.add(new SubspeciesTrait
		{
			id = "circadian_drift",
			group_id = "hibernation",
			priority = 99
		});
		this.t.addDecision("sleep_at_light_age");
		this.t.addOpposite("chaos_driven");
		this.add(new SubspeciesTrait
		{
			id = "winter_slumberers",
			group_id = "hibernation",
			rarity = Rarity.R2_Epic,
			priority = 98
		});
		this.t.addDecision("sleep_at_winter_age");
		this.t.addOpposite("chaos_driven");
		this.add(new SubspeciesTrait
		{
			id = "chaos_driven",
			group_id = "hibernation",
			priority = 97
		});
		this.t.addDecision("sleep_when_not_chaos_age");
		this.t.addOpposite("nocturnal_dormancy");
		this.t.addOpposite("winter_slumberers");
		this.t.addOpposite("circadian_drift");
	}

	// Token: 0x06000C49 RID: 3145 RVA: 0x000B0D68 File Offset: 0x000AEF68
	private void addOther()
	{
		this.add(new SubspeciesTrait
		{
			id = "shiny_love",
			group_id = "chaos"
		});
		this.t.setUnlockedWithAchievement("achievementPlanetOfApes");
		this.t.addDecision("try_to_steal_money");
		this.add(new SubspeciesTrait
		{
			id = "aggressive",
			group_id = "chaos"
		});
		this.add(new SubspeciesTrait
		{
			id = "genetic_mirror",
			group_id = "chaos"
		});
		this.t.setUnlockedWithAchievement("achievementTraitExplorerSubspecies");
		this.add(new SubspeciesTrait
		{
			id = "unstable_genome",
			group_id = "chaos"
		});
		this.t.setUnlockedWithAchievement("achievementGenesExplorer");
		this.add(new SubspeciesTrait
		{
			id = "pure",
			group_id = "mind",
			rarity = Rarity.R2_Epic,
			remove_for_zombies = true
		});
		this.t.setUnlockedWithAchievement("achievementCantBeTooMuch");
		this.add(new SubspeciesTrait
		{
			id = "super_positivity",
			group_id = "mind",
			rarity = Rarity.R0_Normal,
			in_mutation_pot_add = true,
			in_mutation_pot_remove = true,
			remove_for_zombies = true
		});
		this.add(new SubspeciesTrait
		{
			id = "dreamweavers",
			group_id = "mind",
			in_mutation_pot_add = true,
			in_mutation_pot_remove = true,
			remove_for_zombies = true
		});
		this.t.setUnlockedWithAchievement("achievementMindlessHusk");
		this.t.addDecision("try_affect_dreams");
		this.add(new SubspeciesTrait
		{
			id = "telepathic_link",
			group_id = "mind",
			in_mutation_pot_add = true,
			in_mutation_pot_remove = true,
			remove_for_zombies = true
		});
		this.add(new SubspeciesTrait
		{
			id = "inquisitive_nature",
			group_id = "mind",
			rarity = Rarity.R0_Normal,
			in_mutation_pot_add = true,
			in_mutation_pot_remove = true
		});
		this.add(new SubspeciesTrait
		{
			id = "cautious_instincts",
			group_id = "mind",
			rarity = Rarity.R0_Normal,
			in_mutation_pot_add = true,
			in_mutation_pot_remove = true
		});
		this.add(new SubspeciesTrait
		{
			id = "aquatic",
			group_id = "body",
			rarity = Rarity.R0_Normal,
			in_mutation_pot_add = false,
			in_mutation_pot_remove = false
		});
		this.t.base_stats.addTag("water_creature");
		this.t.setUnlockedWithAchievement("achievementBoatsDisposal");
		this.t.addDecision("random_swim");
		this.add(new SubspeciesTrait
		{
			id = "hovering",
			group_id = "body",
			rarity = Rarity.R0_Normal,
			in_mutation_pot_add = true,
			in_mutation_pot_remove = true
		});
		this.add(new SubspeciesTrait
		{
			id = "pollinating",
			group_id = "body",
			rarity = Rarity.R0_Normal,
			in_mutation_pot_add = false,
			in_mutation_pot_remove = false
		});
		this.t.addDecision("pollinate");
		this.add(new SubspeciesTrait
		{
			id = "hydrophobia",
			group_id = "body",
			rarity = Rarity.R0_Normal
		});
		this.t.base_stats_meta.addTag("damaged_by_water");
	}

	// Token: 0x06000C4A RID: 3146 RVA: 0x000B10E0 File Offset: 0x000AF2E0
	private void addReproductionModes()
	{
		this.add(new SubspeciesTrait
		{
			id = "reproduction_strategy_oviparity",
			group_id = "reproduction_strategy",
			rarity = Rarity.R0_Normal,
			priority = 100,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.action_on_augmentation_remove = delegate(NanoObject pNanoObject, BaseAugmentationAsset _)
		{
			if (pNanoObject.isRekt())
			{
				return false;
			}
			Subspecies tSubspecies = (Subspecies)pNanoObject;
			bool result;
			using (ListPool<string> tToRemove = new ListPool<string>())
			{
				foreach (SubspeciesTrait tTrait in tSubspecies.getTraits())
				{
					if (tTrait.phenotype_egg)
					{
						tToRemove.Add(tTrait.id);
					}
				}
				if (tToRemove.Count > 0)
				{
					tSubspecies.removeTraits(tToRemove);
				}
				foreach (Actor tActor in tSubspecies.getUnits())
				{
					if (tActor.isEgg())
					{
						tActor.finishStatusEffect("egg");
					}
				}
				result = true;
			}
			return result;
		};
		this.t.base_stats_meta["maturation"] = 1f;
		this.t.addOpposite("reproduction_strategy_viviparity");
		this.t.addOpposite("reproduction_budding");
		this.t.addOpposite("reproduction_vegetative");
		this.t.base_stats_meta.addTag("oviparity");
		this.add(new SubspeciesTrait
		{
			id = "reproduction_strategy_viviparity",
			group_id = "reproduction_strategy",
			rarity = Rarity.R0_Normal,
			priority = 99,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.base_stats_meta["maturation"] = 1f;
		this.t.addOpposite("reproduction_fission");
		this.t.addOpposite("reproduction_spores");
		this.t.addOpposite("reproduction_strategy_oviparity");
		this.t.addOpposite("reproduction_budding");
		this.t.addOpposite("reproduction_vegetative");
		this.t.base_stats_meta.addTag("viviparity");
	}

	// Token: 0x06000C4B RID: 3147 RVA: 0x000B1274 File Offset: 0x000AF474
	private void addReproduction()
	{
		this.add(new SubspeciesTrait
		{
			id = "reproduction_sexual",
			group_id = "reproductive_methods",
			rarity = Rarity.R0_Normal,
			priority = 100,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.base_stats["birth_rate"] = 3f;
		this.t.addDecision("sexual_reproduction_try");
		this.t.addDecision("find_lover");
		this.t.base_stats_meta.addTag("reproduction_sexual");
		this.t.base_stats_meta.addTag("needs_mate");
		this.t.addOpposite("reproduction_hermaphroditic");
		this.t.addOpposite("reproduction_fission");
		this.t.addOpposite("reproduction_spores");
		this.t.addOpposite("reproduction_vegetative");
		this.add(new SubspeciesTrait
		{
			id = "reproduction_spores",
			group_id = "reproductive_methods",
			priority = 99,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addDecision("asexual_reproduction_spores");
		this.t.base_stats_meta.addTag("reproduction_asexual");
		this.t.addOpposite("reproduction_strategy_viviparity");
		this.t.addOpposite("reproduction_sexual");
		this.t.addOpposite("reproduction_hermaphroditic");
		this.t.addOpposite("reproduction_parthenogenesis");
		this.t.addOpposite("reproduction_fission");
		this.t.addOpposite("reproduction_vegetative");
		this.t.addOpposite("reproduction_divine");
		this.t.addOpposite("reproduction_budding");
		this.add(new SubspeciesTrait
		{
			id = "reproduction_fission",
			group_id = "reproductive_methods",
			rarity = Rarity.R2_Epic,
			priority = 98,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addDecision("asexual_reproduction_fission");
		this.t.base_stats_meta.addTag("reproduction_asexual");
		this.t.addOpposite("reproduction_strategy_viviparity");
		this.t.addOpposite("reproduction_sexual");
		this.t.addOpposite("reproduction_hermaphroditic");
		this.t.addOpposite("reproduction_parthenogenesis");
		this.t.addOpposite("reproduction_spores");
		this.t.addOpposite("reproduction_vegetative");
		this.t.addOpposite("reproduction_divine");
		this.t.addOpposite("reproduction_budding");
		this.add(new SubspeciesTrait
		{
			id = "reproduction_budding",
			group_id = "reproductive_methods",
			rarity = Rarity.R2_Epic,
			priority = 98,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addDecision("asexual_reproduction_budding");
		this.t.base_stats_meta.addTag("reproduction_asexual");
		this.t.addOpposite("reproduction_strategy_viviparity");
		this.t.addOpposite("reproduction_strategy_oviparity");
		this.t.addOpposite("reproduction_parthenogenesis");
		this.t.addOpposite("reproduction_spores");
		this.t.addOpposite("reproduction_vegetative");
		this.t.addOpposite("reproduction_divine");
		this.t.addOpposite("reproduction_fission");
		this.add(new SubspeciesTrait
		{
			id = "reproduction_hermaphroditic",
			group_id = "reproductive_methods",
			rarity = Rarity.R0_Normal,
			priority = 97,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addDecision("sexual_reproduction_try");
		this.t.addDecision("find_lover");
		this.t.base_stats_meta.addTag("reproduction_sexual");
		this.t.base_stats_meta.addTag("needs_mate");
		this.t.addOpposite("reproduction_sexual");
		this.t.addOpposite("reproduction_parthenogenesis");
		this.t.addOpposite("reproduction_fission");
		this.t.addOpposite("reproduction_spores");
		this.t.addOpposite("reproduction_vegetative");
		this.add(new SubspeciesTrait
		{
			id = "reproduction_parthenogenesis",
			group_id = "reproductive_methods",
			rarity = Rarity.R1_Rare,
			priority = 96,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addDecision("asexual_reproduction_parthenogenesis");
		this.t.base_stats_meta.addTag("reproduction_asexual");
		this.t.addOpposite("reproduction_hermaphroditic");
		this.t.addOpposite("reproduction_fission");
		this.t.addOpposite("reproduction_spores");
		this.t.addOpposite("reproduction_vegetative");
		this.t.addOpposite("reproduction_divine");
		this.t.addOpposite("reproduction_budding");
		this.add(new SubspeciesTrait
		{
			id = "reproduction_vegetative",
			group_id = "reproductive_methods",
			rarity = Rarity.R0_Normal,
			priority = 95,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.base_stats_meta["maturation"] = 12f;
		this.t.addDecision("asexual_reproduction_vegetative");
		this.t.base_stats_meta.addTag("reproduction_asexual");
		this.t.addOpposite("reproduction_strategy_oviparity");
		this.t.addOpposite("reproduction_strategy_viviparity");
		this.t.addOpposite("reproduction_sexual");
		this.t.addOpposite("reproduction_hermaphroditic");
		this.t.addOpposite("reproduction_parthenogenesis");
		this.t.addOpposite("reproduction_fission");
		this.t.addOpposite("reproduction_spores");
		this.t.addOpposite("reproduction_divine");
		this.t.addOpposite("reproduction_budding");
		this.add(new SubspeciesTrait
		{
			id = "reproduction_divine",
			group_id = "reproductive_methods",
			rarity = Rarity.R2_Epic,
			priority = 94,
			remove_for_zombies = true
		});
		this.t.addDecision("asexual_reproduction_divine");
		this.t.addOpposite("reproduction_parthenogenesis");
		this.t.addOpposite("reproduction_fission");
		this.t.addOpposite("reproduction_spores");
		this.t.addOpposite("reproduction_vegetative");
		this.t.addOpposite("reproduction_budding");
		SubspeciesTrait subspeciesTrait = new SubspeciesTrait();
		subspeciesTrait.id = "reproduction_soulborne";
		subspeciesTrait.group_id = "reproductive_methods";
		subspeciesTrait.priority = 93;
		subspeciesTrait.remove_for_zombies = true;
		subspeciesTrait.action_attack_target = delegate(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
		{
			if (!pTarget.isActor())
			{
				return false;
			}
			if (!pTarget.a.asset.has_soul)
			{
				return false;
			}
			pSelf.addStatusEffect("soul_harvested", 0f, true);
			return true;
		};
		this.add(subspeciesTrait);
		SubspeciesTrait subspeciesTrait2 = new SubspeciesTrait();
		subspeciesTrait2.id = "reproduction_metamorph";
		subspeciesTrait2.group_id = "reproductive_methods";
		subspeciesTrait2.priority = 92;
		subspeciesTrait2.remove_for_zombies = true;
		subspeciesTrait2.action_attack_target = delegate(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
		{
			if (!pTarget.isActor())
			{
				return false;
			}
			if (!pTarget.a.canTurnIntoColdOne())
			{
				return false;
			}
			if (pTarget.a.subspecies == pSelf.a.subspecies)
			{
				return false;
			}
			Actor tBaby = ActionLibrary.turnIntoMetamorph(pTarget.a, pSelf.a.asset.id);
			if (tBaby != null)
			{
				tBaby.setParent1(pSelf.a, true);
				BabyHelper.applyParentsMeta(pSelf.a, null, tBaby);
			}
			return true;
		};
		this.add(subspeciesTrait2);
	}

	// Token: 0x06000C4C RID: 3148 RVA: 0x000B19E8 File Offset: 0x000AFBE8
	private void addDiet()
	{
		this.add(new SubspeciesTrait
		{
			id = "stomach",
			group_id = "body",
			rarity = Rarity.R0_Normal,
			priority = 100,
			remove_for_zombies = true
		});
		this.t.addDecision("try_to_eat_city_food");
		this.t.action_on_augmentation_add = delegate(NanoObject pNanoObject, BaseAugmentationAsset _)
		{
			((Subspecies)pNanoObject).addTrait("diet_omnivore", false);
			return true;
		};
		this.t.action_on_augmentation_remove = delegate(NanoObject pNanoObject, BaseAugmentationAsset _)
		{
			if (pNanoObject.isRekt())
			{
				return false;
			}
			Subspecies tSubspecies = (Subspecies)pNanoObject;
			bool result;
			using (ListPool<string> tToRemove = new ListPool<string>())
			{
				foreach (SubspeciesTrait tTrait in tSubspecies.getTraits())
				{
					if (tTrait.is_diet_related)
					{
						tToRemove.Add(tTrait.id);
					}
				}
				if (tToRemove.Count > 0)
				{
					tSubspecies.removeTraits(tToRemove);
				}
				result = true;
			}
			return result;
		};
		this.t.base_stats_meta.addTag("needs_food");
		this.add(new SubspeciesTrait
		{
			id = "big_stomach",
			group_id = "body",
			rarity = Rarity.R1_Rare,
			priority = 99,
			is_diet_related = true,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.base_stats_meta["max_nutrition"] = 100f;
		this.add(new SubspeciesTrait
		{
			id = "voracious",
			group_id = "body",
			rarity = Rarity.R0_Normal,
			priority = 98
		});
		this.t.base_stats_meta["metabolic_rate"] = 10f;
		this.add(new SubspeciesTrait
		{
			id = "diet_frugivore",
			group_id = "diet",
			rarity = Rarity.R0_Normal,
			is_diet_related = true,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addDecision("diet_fruits");
		this.t.addOpposite("diet_herbivore");
		this.t.addOpposite("diet_omnivore");
		this.t.base_stats_meta.addTag("diet_fruits");
		this.add(new SubspeciesTrait
		{
			id = "diet_granivore",
			group_id = "diet",
			rarity = Rarity.R0_Normal,
			is_diet_related = true,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addDecision("diet_crops");
		this.t.addOpposite("diet_herbivore");
		this.t.addOpposite("diet_omnivore");
		this.t.base_stats_meta.addTag("diet_crops");
		this.add(new SubspeciesTrait
		{
			id = "diet_florivore",
			group_id = "diet",
			rarity = Rarity.R0_Normal,
			is_diet_related = true,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addDecision("diet_flowers");
		this.t.addOpposite("diet_herbivore");
		this.t.addOpposite("diet_omnivore");
		this.t.base_stats_meta.addTag("diet_flowers");
		this.add(new SubspeciesTrait
		{
			id = "diet_graminivore",
			group_id = "diet",
			rarity = Rarity.R1_Rare,
			is_diet_related = true,
			in_mutation_pot_add = false,
			remove_for_zombies = true
		});
		this.t.addDecision("diet_grass");
		this.t.base_stats_meta.addTag("diet_grass");
		this.add(new SubspeciesTrait
		{
			id = "diet_xylophagy",
			group_id = "diet",
			rarity = Rarity.R2_Epic,
			is_diet_related = true,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addDecision("diet_wood");
		this.t.base_stats_meta.addTag("diet_wood");
		this.add(new SubspeciesTrait
		{
			id = "diet_geophagy",
			group_id = "diet",
			rarity = Rarity.R2_Epic,
			is_diet_related = true,
			remove_for_zombies = true,
			spawn_random_trait_allowed = false
		});
		this.t.addDecision("diet_tiles");
		this.t.base_stats_meta.addTag("diet_tiles");
		this.add(new SubspeciesTrait
		{
			id = "diet_folivore",
			group_id = "diet",
			rarity = Rarity.R0_Normal,
			is_diet_related = true,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addDecision("diet_vegetation");
		this.t.addOpposite("diet_herbivore");
		this.t.addOpposite("diet_omnivore");
		this.t.base_stats_meta.addTag("diet_vegetation");
		this.add(new SubspeciesTrait
		{
			id = "diet_carnivore",
			group_id = "diet",
			rarity = Rarity.R0_Normal,
			priority = 98,
			is_diet_related = true,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addDecision("diet_meat");
		this.t.addOpposite("diet_omnivore");
		this.t.base_stats_meta.addTag("diet_meat");
		this.add(new SubspeciesTrait
		{
			id = "diet_piscivore",
			group_id = "diet",
			rarity = Rarity.R0_Normal,
			is_diet_related = true,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addDecision("diet_fish");
		this.t.addOpposite("diet_omnivore");
		this.t.addOpposite("diet_herbivore");
		this.t.base_stats_meta.addTag("diet_fish");
		this.add(new SubspeciesTrait
		{
			id = "diet_lithotroph",
			group_id = "diet",
			rarity = Rarity.R1_Rare,
			is_diet_related = true,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addDecision("diet_minerals");
		this.t.base_stats_meta.addTag("diet_minerals");
		this.add(new SubspeciesTrait
		{
			id = "diet_insectivore",
			group_id = "diet",
			rarity = Rarity.R0_Normal,
			is_diet_related = true,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addDecision("diet_meat_insect");
		this.t.base_stats_meta.addTag("diet_meat_insect");
		this.add(new SubspeciesTrait
		{
			id = "diet_algivore",
			group_id = "diet",
			rarity = Rarity.R0_Normal,
			is_diet_related = true,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addDecision("diet_algae");
		this.t.base_stats_meta.addTag("diet_algae");
		this.add(new SubspeciesTrait
		{
			id = "diet_cannibalism",
			group_id = "diet",
			priority = 1,
			is_diet_related = true,
			rarity = Rarity.R1_Rare,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.setUnlockedWithAchievement("achievementClannibals");
		this.t.addDecision("diet_same_species");
		this.t.base_stats_meta.addTag("diet_same_species");
		this.add(new SubspeciesTrait
		{
			id = "diet_nectarivore",
			group_id = "diet",
			is_diet_related = true,
			rarity = Rarity.R0_Normal,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addDecision("diet_nectar");
		this.t.base_stats_meta.addTag("diet_nectar");
		this.add(new SubspeciesTrait
		{
			id = "diet_hematophagy",
			group_id = "diet",
			is_diet_related = true,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addDecision("diet_blood");
		this.t.base_stats_meta.addTag("diet_blood");
		this.add(new SubspeciesTrait
		{
			id = "diet_herbivore",
			group_id = "diet",
			rarity = Rarity.R1_Rare,
			priority = 99,
			is_diet_related = true,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addDecision("diet_fruits");
		this.t.addDecision("diet_vegetation");
		this.t.addDecision("diet_flowers");
		this.t.addDecision("diet_grass");
		this.t.addDecision("diet_crops");
		this.t.addOpposite("diet_frugivore");
		this.t.addOpposite("diet_granivore");
		this.t.addOpposite("diet_florivore");
		this.t.addOpposite("diet_folivore");
		this.t.addOpposite("diet_piscivore");
		this.t.addOpposite("diet_omnivore");
		this.t.base_stats_meta.addTag("diet_flowers");
		this.t.base_stats_meta.addTag("diet_fruits");
		this.t.base_stats_meta.addTag("diet_crops");
		this.t.base_stats_meta.addTag("diet_vegetation");
		this.t.base_stats_meta.addTag("diet_grass");
		this.add(new SubspeciesTrait
		{
			id = "diet_omnivore",
			group_id = "diet",
			rarity = Rarity.R1_Rare,
			priority = 100,
			is_diet_related = true,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addDecision("diet_fruits");
		this.t.addDecision("diet_vegetation");
		this.t.addDecision("diet_meat");
		this.t.addOpposite("diet_frugivore");
		this.t.addOpposite("diet_granivore");
		this.t.addOpposite("diet_florivore");
		this.t.addOpposite("diet_folivore");
		this.t.addOpposite("diet_carnivore");
		this.t.addOpposite("diet_piscivore");
		this.t.addOpposite("diet_herbivore");
		this.t.base_stats_meta.addTag("diet_flowers");
		this.t.base_stats_meta.addTag("diet_fruits");
		this.t.base_stats_meta.addTag("diet_crops");
		this.t.base_stats_meta.addTag("diet_vegetation");
		this.t.base_stats_meta.addTag("diet_meat");
		this.t.base_stats_meta.addTag("diet_fish");
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x000B24AC File Offset: 0x000B06AC
	private void addGenetic()
	{
		this.add(new SubspeciesTrait
		{
			id = "advanced_hippocampus",
			group_id = "advanced_brain",
			rarity = Rarity.R1_Rare,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addDecision("try_to_read");
		this.t.base_stats_meta.addTag("has_advanced_memory");
		this.add(new SubspeciesTrait
		{
			id = "wernicke_area",
			group_id = "advanced_brain",
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addDecision("socialize_initial_check");
		this.t.base_stats_meta.addTag("has_advanced_communication");
		this.add(new SubspeciesTrait
		{
			id = "amygdala",
			group_id = "advanced_brain",
			rarity = Rarity.R2_Epic,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addDecision("run_away_from_carnivore");
		this.t.addDecision("run_away");
		this.t.addDecision("reflection");
		this.t.base_stats_meta.addTag("has_emotions");
		this.add(new SubspeciesTrait
		{
			id = "prefrontal_cortex",
			group_id = "advanced_brain",
			in_mutation_pot_add = true,
			remove_for_zombies = true,
			priority = 100
		});
		this.t.addDecision("check_lover_city");
		this.t.addDecision("find_city_job");
		this.t.addDecision("build_civ_city_here");
		this.t.addDecision("try_to_return_to_home_city");
		this.t.addDecision("try_to_start_new_civilization");
		this.t.addDecision("check_join_city");
		this.t.addDecision("check_join_empty_nearby_city");
		this.t.base_stats_meta.addTag("has_sapience");
		SubspeciesTrait subspeciesTrait = new SubspeciesTrait();
		subspeciesTrait.id = "bad_genes";
		subspeciesTrait.group_id = "body";
		subspeciesTrait.rarity = Rarity.R0_Normal;
		subspeciesTrait.in_mutation_pot_add = true;
		subspeciesTrait.in_mutation_pot_remove = true;
		subspeciesTrait.remove_for_zombies = true;
		subspeciesTrait.action_growth = delegate(BaseSimObject pSimObject, WorldTile _)
		{
			if (Randy.randomChance(0.01f))
			{
				string tTraitID = SubspeciesTraitLibrary._bad_genes.GetRandom<string>();
				pSimObject.a.addTrait(tTraitID, false);
			}
			return true;
		};
		this.add(subspeciesTrait);
		this.t.setUnlockedWithAchievement("achievementFastLiving");
		SubspeciesTrait subspeciesTrait2 = new SubspeciesTrait();
		subspeciesTrait2.id = "photosynthetic_skin";
		subspeciesTrait2.group_id = "diet";
		subspeciesTrait2.rarity = Rarity.R2_Epic;
		subspeciesTrait2.in_mutation_pot_add = true;
		subspeciesTrait2.remove_for_zombies = true;
		subspeciesTrait2.special_effect_interval = 10f;
		subspeciesTrait2.action_special_effect = delegate(BaseSimObject pSimObject, WorldTile _)
		{
			if (World.world.era_manager.getCurrentAge().flag_night)
			{
				return false;
			}
			int tRandomNutrition = Randy.randomInt(2, 10);
			pSimObject.a.addNutritionFromEating(tRandomNutrition, false, false);
			return true;
		};
		this.add(subspeciesTrait2);
		SubspeciesTrait subspeciesTrait3 = new SubspeciesTrait();
		subspeciesTrait3.id = "genetic_psychosis";
		subspeciesTrait3.group_id = "mind";
		subspeciesTrait3.rarity = Rarity.R2_Epic;
		subspeciesTrait3.in_mutation_pot_add = true;
		subspeciesTrait3.in_mutation_pot_remove = true;
		subspeciesTrait3.remove_for_zombies = true;
		subspeciesTrait3.action_growth = delegate(BaseSimObject pSimObject, WorldTile _)
		{
			if (pSimObject.a.isPrettyOld() && Randy.randomChance(0.01f))
			{
				pSimObject.a.addTrait("madness", false);
			}
			return true;
		};
		this.add(subspeciesTrait3);
		this.add(new SubspeciesTrait
		{
			id = "bioluminescence",
			group_id = "body",
			in_mutation_pot_add = true,
			in_mutation_pot_remove = true
		});
		this.t.base_stats.addTag("generate_light");
		SubspeciesTrait subspeciesTrait4 = new SubspeciesTrait();
		subspeciesTrait4.id = "accelerated_healing";
		subspeciesTrait4.group_id = "body";
		subspeciesTrait4.rarity = Rarity.R1_Rare;
		subspeciesTrait4.in_mutation_pot_add = true;
		subspeciesTrait4.in_mutation_pot_remove = true;
		subspeciesTrait4.remove_for_zombies = true;
		subspeciesTrait4.action_growth = delegate(BaseSimObject pSimObject, WorldTile _)
		{
			Actor tActor = pSimObject.a;
			IReadOnlyCollection<ActorTrait> tTraitsSet = pSimObject.a.getTraits();
			bool result;
			using (ListPool<ActorTrait> tTraitsToRemove = new ListPool<ActorTrait>())
			{
				foreach (ActorTrait tTrait in tTraitsSet)
				{
					if (tTrait.can_be_removed_by_accelerated_healing)
					{
						tTraitsToRemove.Add(tTrait);
					}
				}
				if (tTraitsToRemove.Count > 0)
				{
					tActor.removeTraits(tTraitsToRemove);
					tActor.setStatsDirty();
				}
				result = true;
			}
			return result;
		};
		this.add(subspeciesTrait4);
		SubspeciesTrait subspeciesTrait5 = new SubspeciesTrait();
		subspeciesTrait5.id = "rapid_aging";
		subspeciesTrait5.group_id = "growth";
		subspeciesTrait5.rarity = Rarity.R1_Rare;
		subspeciesTrait5.in_mutation_pot_add = true;
		subspeciesTrait5.in_mutation_pot_remove = true;
		subspeciesTrait5.remove_for_zombies = true;
		subspeciesTrait5.action_growth = delegate(BaseSimObject pSimObject, WorldTile _)
		{
			if (Randy.randomChance(0.5f))
			{
				ActorData data = pSimObject.a.data;
				int age_overgrowth = data.age_overgrowth;
				data.age_overgrowth = age_overgrowth + 1;
			}
			if (Randy.randomChance(0.5f))
			{
				ActorData data2 = pSimObject.a.data;
				int age_overgrowth = data2.age_overgrowth;
				data2.age_overgrowth = age_overgrowth + 1;
			}
			return true;
		};
		this.add(subspeciesTrait5);
		this.add(new SubspeciesTrait
		{
			id = "good_throwers",
			group_id = "body",
			rarity = Rarity.R1_Rare,
			in_mutation_pot_add = true,
			in_mutation_pot_remove = true
		});
		this.t.setUnlockedWithAchievement("achievementBallToBall");
		this.t.base_stats["throwing_range"] = 6f;
		this.add(new SubspeciesTrait
		{
			id = "fast_builders",
			group_id = "mind",
			rarity = Rarity.R1_Rare,
			in_mutation_pot_add = true,
			in_mutation_pot_remove = true
		});
		this.t.setUnlockedWithAchievement("achievementCustomWorld");
		this.t.addOpposite("slow_builders");
		this.t.base_stats_meta["construction_speed"] = 2f;
		this.add(new SubspeciesTrait
		{
			id = "slow_builders",
			group_id = "mind",
			rarity = Rarity.R1_Rare,
			in_mutation_pot_add = true,
			in_mutation_pot_remove = true
		});
		this.t.addOpposite("fast_builders");
		this.t.base_stats_meta["construction_speed"] = -1f;
		this.add(new SubspeciesTrait
		{
			id = "fins",
			group_id = "body",
			rarity = Rarity.R1_Rare,
			in_mutation_pot_add = true,
			in_mutation_pot_remove = true
		});
		this.t.setUnlockedWithAchievement("achievementPiranhaLand");
		this.t.base_stats.addTag("fast_swimming");
		this.add(new SubspeciesTrait
		{
			id = "heat_resistance",
			group_id = "body",
			rarity = Rarity.R1_Rare,
			in_mutation_pot_add = true,
			in_mutation_pot_remove = true
		});
		this.t.setUnlockedWithAchievement("achievementFlickIt");
		this.t.base_stats.addTag("immunity_fire");
		this.t.base_stats_meta.addTag("can_build_in_biome_infernal");
		this.add(new SubspeciesTrait
		{
			id = "cold_resistance",
			group_id = "body",
			rarity = Rarity.R1_Rare,
			in_mutation_pot_add = true,
			in_mutation_pot_remove = true
		});
		this.t.base_stats.addTag("immunity_cold");
		this.t.base_stats_meta.addTag("can_build_in_biome_permafrost");
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x000B2B4C File Offset: 0x000B0D4C
	private void addStats()
	{
		this.add(new SubspeciesTrait
		{
			id = "exoskeleton",
			group_id = "body",
			rarity = Rarity.R0_Normal,
			in_mutation_pot_add = true,
			in_mutation_pot_remove = true
		});
		this.t.base_stats["armor"] = 10f;
		this.add(new SubspeciesTrait
		{
			id = "long_lifespan",
			group_id = "body",
			rarity = Rarity.R0_Normal,
			in_mutation_pot_add = true,
			in_mutation_pot_remove = true,
			remove_for_zombies = true
		});
		this.t.base_stats["lifespan"] = 100f;
		this.add(new SubspeciesTrait
		{
			id = "hyper_intelligence",
			group_id = "mind",
			rarity = Rarity.R0_Normal,
			in_mutation_pot_add = true,
			in_mutation_pot_remove = true,
			remove_for_zombies = true
		});
		this.t.base_stats["intelligence"] = 30f;
		this.add(new SubspeciesTrait
		{
			id = "enhanced_strength",
			group_id = "body",
			rarity = Rarity.R0_Normal,
			in_mutation_pot_add = true,
			in_mutation_pot_remove = true
		});
		this.t.setUnlockedWithAchievement("achievementSuperMushroom");
		this.t.base_stats["damage"] = 50f;
		this.add(new SubspeciesTrait
		{
			id = "high_fecundity",
			group_id = "body",
			rarity = Rarity.R0_Normal,
			in_mutation_pot_add = true,
			in_mutation_pot_remove = true,
			remove_for_zombies = true
		});
		this.t.setUnlockedWithAchievement("achievement10000Creatures");
		this.t.base_stats["birth_rate"] = 5f;
		this.add(new SubspeciesTrait
		{
			id = "unmoving",
			group_id = "body",
			rarity = Rarity.R0_Normal,
			in_mutation_pot_add = false,
			remove_for_zombies = false,
			spawn_random_trait_allowed = false
		});
		this.t.setUnlockedWithAchievement("achievementSimpleStupidGenetics");
		this.t.base_stats.addTag("immovable");
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x000B2D88 File Offset: 0x000B0F88
	private void addLimits()
	{
		this.add(new SubspeciesTrait
		{
			id = "population_minimal",
			group_id = "harmony",
			rarity = Rarity.R0_Normal,
			in_mutation_pot_remove = false,
			in_mutation_pot_add = false,
			spawn_random_trait_allowed = false,
			priority = 100
		});
		this.t.addOpposite("population_small");
		this.t.addOpposite("population_moderate");
		this.t.addOpposite("population_large");
		this.t.addOpposite("population_expansive");
		this.t.base_stats_meta["limit_population"] = 50f;
		this.add(new SubspeciesTrait
		{
			id = "population_small",
			group_id = "harmony",
			rarity = Rarity.R0_Normal,
			in_mutation_pot_remove = false,
			in_mutation_pot_add = false,
			spawn_random_trait_allowed = false,
			priority = 99
		});
		this.t.addOpposite("population_minimal");
		this.t.addOpposite("population_moderate");
		this.t.addOpposite("population_large");
		this.t.addOpposite("population_expansive");
		this.t.base_stats_meta["limit_population"] = 100f;
		this.add(new SubspeciesTrait
		{
			id = "population_moderate",
			group_id = "harmony",
			rarity = Rarity.R0_Normal,
			in_mutation_pot_remove = false,
			in_mutation_pot_add = false,
			spawn_random_trait_allowed = false,
			priority = 98
		});
		this.t.addOpposite("population_small");
		this.t.addOpposite("population_minimal");
		this.t.addOpposite("population_large");
		this.t.addOpposite("population_expansive");
		this.t.base_stats_meta["limit_population"] = 500f;
		this.add(new SubspeciesTrait
		{
			id = "population_large",
			group_id = "harmony",
			rarity = Rarity.R0_Normal,
			in_mutation_pot_remove = false,
			in_mutation_pot_add = false,
			spawn_random_trait_allowed = false,
			priority = 97
		});
		this.t.addOpposite("population_small");
		this.t.addOpposite("population_minimal");
		this.t.addOpposite("population_moderate");
		this.t.addOpposite("population_expansive");
		this.t.base_stats_meta["limit_population"] = 1000f;
		this.add(new SubspeciesTrait
		{
			id = "population_expansive",
			group_id = "harmony",
			rarity = Rarity.R0_Normal,
			in_mutation_pot_remove = false,
			in_mutation_pot_add = false,
			spawn_random_trait_allowed = false,
			priority = 96
		});
		this.t.addOpposite("population_small");
		this.t.addOpposite("population_minimal");
		this.t.addOpposite("population_moderate");
		this.t.addOpposite("population_large");
		this.t.base_stats_meta["limit_population"] = 3000f;
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x000B30B8 File Offset: 0x000B12B8
	private void addMaturation()
	{
		this.add(new SubspeciesTrait
		{
			id = "gestation_short",
			group_id = "gestation",
			rarity = Rarity.R0_Normal,
			priority = 100,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addOpposite("gestation_moderate");
		this.t.addOpposite("gestation_long");
		this.t.addOpposite("gestation_very_long");
		this.t.addOpposite("gestation_extremely_long");
		this.t.base_stats_meta["maturation"] = 2f;
		this.add(new SubspeciesTrait
		{
			id = "gestation_moderate",
			group_id = "gestation",
			rarity = Rarity.R0_Normal,
			priority = 98,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addOpposite("gestation_short");
		this.t.addOpposite("gestation_long");
		this.t.addOpposite("gestation_very_long");
		this.t.addOpposite("gestation_extremely_long");
		this.t.base_stats_meta["maturation"] = 4f;
		this.add(new SubspeciesTrait
		{
			id = "gestation_long",
			group_id = "gestation",
			rarity = Rarity.R0_Normal,
			priority = 97,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addOpposite("gestation_short");
		this.t.addOpposite("gestation_moderate");
		this.t.addOpposite("gestation_very_long");
		this.t.addOpposite("gestation_extremely_long");
		this.t.base_stats_meta["maturation"] = 9f;
		this.add(new SubspeciesTrait
		{
			id = "gestation_very_long",
			group_id = "gestation",
			rarity = Rarity.R0_Normal,
			priority = 96,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addOpposite("gestation_short");
		this.t.addOpposite("gestation_moderate");
		this.t.addOpposite("gestation_long");
		this.t.addOpposite("gestation_extremely_long");
		this.t.base_stats_meta["maturation"] = 20f;
		this.add(new SubspeciesTrait
		{
			id = "gestation_extremely_long",
			group_id = "gestation",
			rarity = Rarity.R1_Rare,
			priority = 95,
			in_mutation_pot_add = true,
			remove_for_zombies = true
		});
		this.t.addOpposite("gestation_short");
		this.t.addOpposite("gestation_moderate");
		this.t.addOpposite("gestation_long");
		this.t.addOpposite("gestation_very_long");
		this.t.base_stats_meta["maturation"] = 50f;
		this.add(new SubspeciesTrait
		{
			id = "gmo",
			group_id = "special",
			priority = 94,
			can_be_removed = false,
			can_be_given = false,
			spawn_random_trait_allowed = false
		});
		this.add(new SubspeciesTrait
		{
			id = "uplifted",
			group_id = "special",
			priority = 93,
			can_be_removed = false,
			can_be_given = false,
			spawn_random_trait_allowed = false
		});
	}

	// Token: 0x06000C51 RID: 3153 RVA: 0x000B3440 File Offset: 0x000B1640
	private void addAdaptations()
	{
		this.add(new SubspeciesTrait
		{
			id = "$adaptation$",
			group_id = "adaptations",
			remove_for_zombies = true
		});
		this.clone("adaptation_desert", "$adaptation$");
		this.t.rarity = Rarity.R0_Normal;
		this.t.base_stats_meta.addTag("can_build_in_biome_desert");
		this.t.base_stats.addTag("walk_adaptation_sand");
		this.clone("adaptation_swamp", "$adaptation$");
		this.t.rarity = Rarity.R0_Normal;
		this.t.base_stats_meta.addTag("can_build_in_biome_swamp");
		this.t.base_stats.addTag("walk_adaptation_swamp");
		this.clone("adaptation_wasteland", "$adaptation$");
		this.t.rarity = Rarity.R1_Rare;
		this.t.base_stats_meta.addTag("can_build_in_biome_wasteland");
		this.clone("adaptation_corruption", "$adaptation$");
		this.t.rarity = Rarity.R2_Epic;
		this.t.base_stats_meta.addTag("can_build_in_biome_corruption");
		this.clone("adaptation_permafrost", "$adaptation$");
		this.t.rarity = Rarity.R1_Rare;
		this.t.base_stats_meta.addTag("can_build_in_biome_permafrost");
		this.t.base_stats.addTag("walk_adaptation_snow");
		this.clone("adaptation_infernal", "$adaptation$");
		this.t.rarity = Rarity.R2_Epic;
		this.t.base_stats_meta.addTag("can_build_in_biome_infernal");
	}

	// Token: 0x06000C52 RID: 3154 RVA: 0x000B35E4 File Offset: 0x000B17E4
	private void addMutations()
	{
		this.add(new SubspeciesTrait
		{
			id = "$skin_mutation$",
			group_id = "mutations",
			remove_for_zombies = true,
			is_mutation_skin = true,
			animation_walk = ActorAnimationSequences.walk_0_3,
			animation_swim = ActorAnimationSequences.swim_0_3,
			skin_citizen_male = AssetLibrary<SubspeciesTrait>.l<string>(new string[]
			{
				"male_1"
			}),
			skin_citizen_female = AssetLibrary<SubspeciesTrait>.l<string>(new string[]
			{
				"female_1"
			}),
			skin_warrior = AssetLibrary<SubspeciesTrait>.l<string>(new string[]
			{
				"warrior_1"
			}),
			render_heads_for_children = true
		});
		this.clone("mutation_skin_burger", "$skin_mutation$");
		this.t.setUnlockedWithAchievement("achievementBurger");
		this.t.priority = 92;
		this.t.sprite_path = "actors/species/mutations/mutation_skin_burger";
		this.t.render_heads_for_children = false;
		this.loadSpritesPaths(this.t);
		this.clone("mutation_skin_light_orb", "$skin_mutation$");
		this.t.rarity = Rarity.R1_Rare;
		this.t.priority = 93;
		this.t.animation_idle = ActorAnimationSequences.walk_0_3;
		this.t.sprite_path = "actors/species/mutations/mutation_skin_light_orb";
		this.t.prevent_unconscious_rotation = true;
		this.t.base_stats_meta.addTag("always_idle_animation");
		this.t.shadow = false;
		this.loadSpritesPaths(this.t);
		this.clone("mutation_skin_living_rock", "$skin_mutation$");
		this.t.rarity = Rarity.R0_Normal;
		this.t.priority = 92;
		this.t.sprite_path = "actors/species/mutations/mutation_skin_living_rock";
		this.loadSpritesPaths(this.t);
		this.clone("mutation_skin_tentacle_horror", "$skin_mutation$");
		this.t.rarity = Rarity.R2_Epic;
		this.t.priority = 92;
		this.t.animation_idle = ActorAnimationSequences.walk_0_3;
		this.t.sprite_path = "actors/species/mutations/mutation_skin_tentacle_horror";
		this.t.prevent_unconscious_rotation = true;
		this.loadSpritesPaths(this.t);
		this.clone("mutation_skin_abomination", "$skin_mutation$");
		this.t.rarity = Rarity.R1_Rare;
		this.t.priority = 92;
		this.t.sprite_path = "actors/species/mutations/mutation_skin_abomination";
		this.loadSpritesPaths(this.t);
		this.clone("mutation_skin_fractal", "$skin_mutation$");
		this.t.priority = 92;
		this.t.animation_walk = ActorAnimationSequences.walk_0_5;
		this.t.animation_idle = ActorAnimationSequences.walk_0_5;
		this.t.animation_swim = ActorAnimationSequences.swim_0_5;
		this.t.sprite_path = "actors/species/mutations/mutation_skin_fractal";
		this.loadSpritesPaths(this.t);
		this.clone("mutation_skin_void", "$skin_mutation$");
		this.t.priority = 92;
		this.t.sprite_path = "actors/species/mutations/mutation_skin_void";
		this.loadSpritesPaths(this.t);
		this.clone("mutation_skin_metalic_orb", "$skin_mutation$");
		this.t.setUnlockedWithAchievement("achievementBackToBetaTesting");
		this.t.rarity = Rarity.R2_Epic;
		this.t.priority = 92;
		this.t.animation_idle = ActorAnimationSequences.walk_0_3;
		this.t.sprite_path = "actors/species/mutations/mutation_skin_metalic_orb";
		this.t.prevent_unconscious_rotation = true;
		this.loadSpritesPaths(this.t);
		this.clone("mutation_skin_blood_vortex", "$skin_mutation$");
		this.t.priority = 92;
		this.t.sprite_path = "actors/species/mutations/mutation_skin_blood_vortex";
		this.t.shadow_texture = "unitShadow_6";
		this.t.shadow_texture_baby = "unitShadow_5";
		this.loadSpritesPaths(this.t);
		this.clone("mutation_skin_energy", "$skin_mutation$");
		this.t.rarity = Rarity.R0_Normal;
		this.t.priority = 92;
		this.t.animation_idle = ActorAnimationSequences.walk_0_3;
		this.t.sprite_path = "actors/species/mutations/mutation_skin_energy";
		this.t.prevent_unconscious_rotation = true;
		this.t.base_stats_meta.addTag("always_idle_animation");
		this.t.shadow = false;
		this.loadSpritesPaths(this.t);
		this.addMutationOpposites();
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x000B3A4C File Offset: 0x000B1C4C
	private void addEggs()
	{
		this.add(new SubspeciesTrait
		{
			id = "$egg$",
			group_id = "eggs",
			phenotype_egg = true
		});
		this.t.action_on_augmentation_add = delegate(NanoObject pNanoObject, BaseAugmentationAsset _)
		{
			Subspecies tSubspecies = (Subspecies)pNanoObject;
			if (!tSubspecies.hasTrait("reproduction_strategy_oviparity"))
			{
				tSubspecies.addTrait("reproduction_strategy_oviparity", true);
			}
			return true;
		};
		this.clone("egg_shell_plain", "$egg$");
		this.t.rarity = Rarity.R0_Normal;
		this.t.base_stats_meta["maturation"] = 3f;
		this.clone("egg_shell_spotted", "$egg$");
		this.t.rarity = Rarity.R0_Normal;
		this.t.base_stats_meta["maturation"] = 3f;
		this.clone("egg_colored", "$egg$");
		this.t.rarity = Rarity.R0_Normal;
		this.t.base_stats_meta["maturation"] = 3f;
		this.clone("egg_roe", "$egg$");
		this.t.rarity = Rarity.R0_Normal;
		this.t.base_stats_meta["maturation"] = 3f;
		this.clone("egg_face", "$egg$");
		this.t.base_stats_meta["maturation"] = 5f;
		this.clone("egg_orb", "$egg$");
		this.t.rarity = Rarity.R2_Epic;
		this.t.base_stats_meta["maturation"] = 6f;
		this.clone("egg_eyeball", "$egg$");
		this.t.setUnlockedWithAchievement("achievementGodMode");
		this.t.rarity = Rarity.R1_Rare;
		this.t.animation_idle = ActorAnimationSequences.walk_0_3;
		this.t.base_stats_meta["maturation"] = 4f;
		this.clone("egg_alien", "$egg$");
		this.t.rarity = Rarity.R1_Rare;
		this.t.base_stats_meta["maturation"] = 7f;
		this.clone("egg_cocoon", "$egg$");
		this.t.rarity = Rarity.R0_Normal;
		this.t.base_stats_meta["maturation"] = 6f;
		this.clone("egg_metal_box", "$egg$");
		this.t.rarity = Rarity.R2_Epic;
		this.t.base_stats_meta["maturation"] = 15f;
		this.clone("egg_crystal", "$egg$");
		this.t.rarity = Rarity.R1_Rare;
		this.t.base_stats_meta["maturation"] = 10f;
		this.clone("egg_ice", "$egg$");
		this.t.rarity = Rarity.R1_Rare;
		this.t.after_hatch_from_egg_action = delegate(Actor pActor)
		{
			ActionLibrary.snowDropsSpawn(pActor, null);
		};
		this.t.base_stats_meta["maturation"] = 8f;
		this.clone("egg_blob", "$egg$");
		this.t.rarity = Rarity.R1_Rare;
		this.t.base_stats_meta["maturation"] = 2f;
		this.clone("egg_candy", "$egg$");
		this.t.rarity = Rarity.R1_Rare;
		this.t.base_stats_meta["maturation"] = 3f;
		this.clone("egg_bubble", "$egg$");
		this.t.rarity = Rarity.R1_Rare;
		this.t.base_stats_meta["maturation"] = 1f;
		this.clone("egg_rainbow", "$egg$");
		this.t.rarity = Rarity.R2_Epic;
		this.t.base_stats_meta["maturation"] = 3f;
		this.clone("egg_pumpkin", "$egg$");
		this.t.setUnlockedWithAchievement("achievementSocialNetwork");
		this.t.base_stats_meta["maturation"] = 5f;
		this.clone("egg_flames", "$egg$");
		this.t.rarity = Rarity.R2_Epic;
		this.t.after_hatch_from_egg_action = delegate(Actor pActor)
		{
			ActionLibrary.fireDropsSpawn(pActor, null);
		};
		this.t.base_stats_meta["maturation"] = 6f;
		this.addEggOpposites();
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x000B3EFC File Offset: 0x000B20FC
	public override void post_init()
	{
		base.post_init();
		foreach (SubspeciesTrait tTrait in this.list)
		{
			if (tTrait.phenotype_egg)
			{
				if (string.IsNullOrEmpty(tTrait.id_egg))
				{
					tTrait.id_egg = tTrait.id;
				}
				tTrait.sprite_path = "eggs/" + tTrait.id_egg;
			}
			if (tTrait.shadow && tTrait.is_mutation_skin)
			{
				tTrait.texture_asset.loadShadow();
			}
		}
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x000B3FA0 File Offset: 0x000B21A0
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (SubspeciesTrait tTrait in this.list)
		{
			if (tTrait.spawn_random_trait_allowed)
			{
				this._pot_allowed_to_be_given_randomly.Add(tTrait);
			}
			if (tTrait.in_mutation_pot_add)
			{
				int tRate = tTrait.rarity.GetRate();
				this._pot_mutation_traits_add.AddTimes(tRate, tTrait);
			}
			if (tTrait.in_mutation_pot_remove)
			{
				int tRate2 = tTrait.rarity.GetRate();
				this._pot_mutation_traits_remove.AddTimes(tRate2, tTrait);
			}
			if (tTrait.phenotype_egg && tTrait.after_hatch_from_egg_action != null)
			{
				tTrait.has_after_hatch_from_egg_action = true;
			}
		}
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x000B4060 File Offset: 0x000B2260
	public override void editorDiagnostic()
	{
		base.editorDiagnostic();
		foreach (SubspeciesTrait tAsset in this.list)
		{
			base.checkSpriteExists("sprite_path", tAsset.sprite_path, tAsset);
		}
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x000B40C8 File Offset: 0x000B22C8
	public SubspeciesTrait getRandomMutationTraitToAdd()
	{
		return this._pot_mutation_traits_add.GetRandom<SubspeciesTrait>();
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x000B40D5 File Offset: 0x000B22D5
	public SubspeciesTrait getRandomMutationTraitToRemove()
	{
		return this._pot_mutation_traits_remove.GetRandom<SubspeciesTrait>();
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x000B40E4 File Offset: 0x000B22E4
	private void addPhenotypes()
	{
		string tPhenotypeSkin = "phenotype_skin";
		for (int i = 0; i < AssetManager.phenotype_library.list.Count; i++)
		{
			PhenotypeAsset tPhenotypeAsset = AssetManager.phenotype_library.list[i];
			string tID = tPhenotypeSkin + "_" + tPhenotypeAsset.id;
			tPhenotypeAsset.subspecies_trait_id = tID;
		}
		foreach (PhenotypeAsset tPhenotypeAsset2 in AssetManager.phenotype_library.list)
		{
			this.add(new SubspeciesTrait
			{
				id = tPhenotypeSkin + "_" + tPhenotypeAsset2.id,
				group_id = "phenotypes",
				id_phenotype = tPhenotypeAsset2.id,
				phenotype_skin = true,
				priority = tPhenotypeAsset2.priority,
				special_icon_logic = true,
				special_locale_id = "subspecies_trait_phenotype",
				special_locale_description = "subspecies_trait_phenotype_info",
				has_description_2 = false,
				path_icon = "ui/Icons/iconPhenotype",
				spawn_random_trait_allowed = false
			});
		}
	}

	// Token: 0x06000C5A RID: 3162 RVA: 0x000B420C File Offset: 0x000B240C
	private void addMutationOpposites()
	{
		using (ListPool<string> tAllRelatedIds = new ListPool<string>())
		{
			foreach (SubspeciesTrait tAsset in this.list)
			{
				if (tAsset.is_mutation_skin)
				{
					tAllRelatedIds.Add(tAsset.id);
				}
			}
			foreach (SubspeciesTrait tAsset2 in this.list)
			{
				if (tAsset2.is_mutation_skin)
				{
					tAsset2.addOpposites(tAllRelatedIds);
					tAsset2.removeOpposite(tAsset2.id);
				}
			}
		}
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x000B42E0 File Offset: 0x000B24E0
	private void addEggOpposites()
	{
		using (ListPool<string> tAllRelatedIds = new ListPool<string>())
		{
			foreach (SubspeciesTrait tAsset in this.list)
			{
				if (tAsset.phenotype_egg)
				{
					tAllRelatedIds.Add(tAsset.id);
				}
			}
			foreach (SubspeciesTrait tAsset2 in this.list)
			{
				if (tAsset2.phenotype_egg)
				{
					tAsset2.addOpposites(tAllRelatedIds);
					tAsset2.removeOpposite(tAsset2.id);
				}
			}
		}
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x000B43B4 File Offset: 0x000B25B4
	private void loadSpritesPaths(SubspeciesTrait pAsset)
	{
		if (!pAsset.is_mutation_skin)
		{
			return;
		}
		string tPath = pAsset.sprite_path + "/";
		pAsset.texture_asset = new ActorTextureSubAsset(tPath, true);
		pAsset.texture_asset.prevent_unconscious_rotation = pAsset.prevent_unconscious_rotation;
		pAsset.texture_asset.render_heads_for_children = pAsset.render_heads_for_children;
		pAsset.texture_asset.shadow = pAsset.shadow;
		pAsset.texture_asset.shadow_texture = pAsset.shadow_texture;
		pAsset.texture_asset.shadow_texture_egg = pAsset.shadow_texture_egg;
		pAsset.texture_asset.shadow_texture_baby = pAsset.shadow_texture_baby;
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x000B4450 File Offset: 0x000B2650
	public void preloadMainUnitSprites()
	{
		foreach (SubspeciesTrait pAsset in this.list)
		{
			if (pAsset.is_mutation_skin)
			{
				pAsset.texture_asset.preloadSprites(true, true, pAsset);
			}
		}
	}

	// Token: 0x04000B80 RID: 2944
	private const string TEMPLATE_EGG = "$egg$";

	// Token: 0x04000B81 RID: 2945
	private const string TEMPLATE_MAGIC_BLOOD = "$magic_blood$";

	// Token: 0x04000B82 RID: 2946
	private const string TEMPLATE_SKIN_MUTATION = "$skin_mutation$";

	// Token: 0x04000B83 RID: 2947
	private const string TEMPLATE_ADAPTATION = "$adaptation$";

	// Token: 0x04000B84 RID: 2948
	private List<SubspeciesTrait> _pot_mutation_traits_add = new List<SubspeciesTrait>();

	// Token: 0x04000B85 RID: 2949
	private List<SubspeciesTrait> _pot_mutation_traits_remove = new List<SubspeciesTrait>();

	// Token: 0x04000B86 RID: 2950
	private static List<string> _bad_genes = AssetLibrary<SubspeciesTrait>.l<string>(new string[]
	{
		"fragile_health",
		"weak",
		"slow",
		"fat",
		"ugly"
	});
}
