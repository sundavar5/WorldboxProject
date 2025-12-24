using System;
using System.Collections.Generic;

// Token: 0x02000197 RID: 407
public class CultureTraitLibrary : BaseTraitLibrary<CultureTrait>
{
	// Token: 0x06000C01 RID: 3073 RVA: 0x000ACD42 File Offset: 0x000AAF42
	protected override List<string> getDefaultTraitsForMeta(ActorAsset pAsset)
	{
		return pAsset.default_culture_traits;
	}

	// Token: 0x06000C02 RID: 3074 RVA: 0x000ACD4C File Offset: 0x000AAF4C
	public override void init()
	{
		base.init();
		this.addWeaponRelatedTraits();
		this.addTownPlansZones();
		this.addTownPlansTilePlacements();
		this.add(new CultureTrait
		{
			id = "roads",
			group_id = "architecture"
		});
		this.add(new CultureTrait
		{
			id = "expansionists",
			group_id = "kingdom",
			priority = 100
		});
		this.add(new CultureTrait
		{
			id = "tiny_legends",
			group_id = "kingdom",
			priority = 99
		});
		this.t.addOpposite("youth_reverence");
		this.add(new CultureTrait
		{
			id = "animal_whisperers",
			group_id = "miscellaneous"
		});
		this.add(new CultureTrait
		{
			id = "ancestors_knowledge",
			group_id = "knowledge"
		});
		this.t.setUnlockedWithAchievement("achievementTraitExplorerCulture");
		this.add(new CultureTrait
		{
			id = "statue_lovers",
			value = 4f,
			group_id = "buildings"
		});
		this.add(new CultureTrait
		{
			id = "tower_lovers",
			value = 4f,
			group_id = "buildings"
		});
		this.add(new CultureTrait
		{
			id = "training_potential",
			group_id = "knowledge"
		});
		this.add(new CultureTrait
		{
			id = "elder_reverence",
			value = 2f,
			group_id = "happiness"
		});
		this.t.addOpposite("youth_reverence");
		this.add(new CultureTrait
		{
			id = "youth_reverence",
			group_id = "happiness"
		});
		this.t.addOpposite("elder_reverence");
		this.t.addOpposite("tiny_legends");
		this.add(new CultureTrait
		{
			id = "conscription_female_only",
			group_id = "kingdom",
			priority = 90
		});
		this.t.addOpposite("conscription_male_only");
		this.add(new CultureTrait
		{
			id = "conscription_male_only",
			group_id = "kingdom",
			priority = 89
		});
		this.t.addOpposite("conscription_female_only");
		this.add(new CultureTrait
		{
			id = "fast_learners",
			group_id = "knowledge",
			value = 2f
		});
		this.add(new CultureTrait
		{
			id = "pep_talks",
			group_id = "happiness"
		});
		this.add(new CultureTrait
		{
			id = "expertise_exchange",
			value = 10f,
			group_id = "knowledge"
		});
		this.add(new CultureTrait
		{
			id = "happiness_from_war",
			group_id = "happiness"
		});
		this.add(new CultureTrait
		{
			id = "dense_dwellings",
			value = 2f,
			group_id = "harmony"
		});
		this.t.addOpposite("solitude_seekers");
		this.t.addOpposite("hive_society");
		this.add(new CultureTrait
		{
			id = "solitude_seekers",
			value = 0.5f,
			group_id = "harmony"
		});
		this.t.addOpposite("dense_dwellings");
		this.t.addOpposite("hive_society");
		this.add(new CultureTrait
		{
			id = "hive_society",
			value = 10f,
			group_id = "harmony"
		});
		this.t.setUnlockedWithAchievement("achievementAntWorld");
		this.t.addOpposite("dense_dwellings");
		this.t.addOpposite("solitude_seekers");
		this.add(new CultureTrait
		{
			id = "gossip_lovers",
			group_id = "happiness"
		});
		this.add(new CultureTrait
		{
			id = "reading_lovers",
			group_id = "knowledge"
		});
		this.add(new CultureTrait
		{
			id = "attentive_readers",
			value = 2f,
			group_id = "knowledge"
		});
		CultureTrait cultureTrait = new CultureTrait();
		cultureTrait.id = "join_or_die";
		cultureTrait.group_id = "worldview";
		cultureTrait.priority = 100;
		cultureTrait.action_attack_target = delegate(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
		{
			if (!pSelf.a.isKingdomCiv())
			{
				return false;
			}
			if (!pTarget.isActor())
			{
				return false;
			}
			if (!pTarget.isAlive())
			{
				return false;
			}
			if (pTarget.a.kingdom == pSelf.kingdom)
			{
				return false;
			}
			if (!pTarget.isKingdomCiv() && !pTarget.kingdom.isNomads())
			{
				return false;
			}
			if (!pTarget.a.isSapient())
			{
				return false;
			}
			if (pTarget.a.hasTag("strong_mind"))
			{
				return false;
			}
			if (pTarget.a.getHealthRatio() > 0.3f)
			{
				return false;
			}
			if (!Randy.randomChance(0.3f))
			{
				return false;
			}
			pTarget.a.removeFromPreviousFaction();
			pTarget.a.joinKingdom(pSelf.kingdom);
			pSelf.a.clearAttackTarget();
			pTarget.a.clearAttackTarget();
			pTarget.a.applyRandomForce(1.5f, 2f);
			return true;
		};
		this.add(cultureTrait);
		this.add(new CultureTrait
		{
			id = "true_roots",
			group_id = "miscellaneous"
		});
		this.add(new CultureTrait
		{
			id = "legacy_keepers",
			group_id = "miscellaneous"
		});
		this.add(new CultureTrait
		{
			id = "serenity_now",
			group_id = "worldview"
		});
		this.t.base_stats.addTag("love_peace");
		this.t.addOpposite("xenophobic");
		this.add(new CultureTrait
		{
			id = "weaponsmith_mastery",
			value = 5f,
			group_id = "warfare"
		});
		this.add(new CultureTrait
		{
			id = "armorsmith_mastery",
			value = 5f,
			group_id = "warfare"
		});
		this.add(new CultureTrait
		{
			id = "patriarchy",
			group_id = "succession",
			has_description_2 = false,
			priority = 97
		});
		this.t.addOpposite("matriarchy");
		this.add(new CultureTrait
		{
			id = "matriarchy",
			group_id = "succession",
			has_description_2 = false,
			priority = 96
		});
		this.t.addOpposite("patriarchy");
		this.add(new CultureTrait
		{
			id = "ultimogeniture",
			group_id = "succession",
			priority = 96
		});
		this.add(new CultureTrait
		{
			id = "diplomatic_ascension",
			group_id = "succession",
			priority = 100
		});
		this.t.base_stats.addTag("love_peace");
		this.t.addOpposite("warriors_ascension");
		this.t.addOpposite("fames_crown");
		this.t.addOpposite("golden_rule");
		this.add(new CultureTrait
		{
			id = "fames_crown",
			group_id = "succession",
			priority = 100
		});
		this.t.addOpposite("warriors_ascension");
		this.t.addOpposite("diplomatic_ascension");
		this.t.addOpposite("golden_rule");
		this.add(new CultureTrait
		{
			id = "golden_rule",
			group_id = "succession",
			priority = 100
		});
		this.t.addOpposite("warriors_ascension");
		this.t.addOpposite("fames_crown");
		this.t.addOpposite("diplomatic_ascension");
		this.add(new CultureTrait
		{
			id = "shattered_crown",
			group_id = "succession",
			priority = 60
		});
		this.add(new CultureTrait
		{
			id = "unbroken_chain",
			group_id = "succession",
			priority = 59
		});
		this.t.setUnlockedWithAchievement("achievementSuccession");
		this.add(new CultureTrait
		{
			id = "warriors_ascension",
			group_id = "succession",
			priority = 99
		});
		this.t.addOpposite("diplomatic_ascension");
		this.t.addOpposite("fames_crown");
		this.t.addOpposite("golden_rule");
		this.add(new CultureTrait
		{
			id = "xenophobic",
			group_id = "worldview"
		});
		this.t.addOpposite("xenophiles");
		this.t.addOpposite("serenity_now");
		this.add(new CultureTrait
		{
			id = "ethnocentric_guard",
			group_id = "worldview"
		});
		this.add(new CultureTrait
		{
			id = "xenophiles",
			group_id = "worldview"
		});
		this.t.base_stats.addTag("love_peace");
		this.t.addOpposite("xenophobic");
		this.add(new CultureTrait
		{
			id = "ethno_sculpted",
			group_id = "special",
			can_be_given = false,
			can_be_removed = false,
			can_be_in_book = false,
			spawn_random_trait_allowed = false
		});
		this.add(new CultureTrait
		{
			id = "grin_mark",
			group_id = "fate",
			spawn_random_trait_allowed = false,
			priority = -100
		});
		this.t.setTraitInfoToGrinMark();
		this.t.setUnlockedWithAchievement("achievementCreaturesExplorer");
	}

	// Token: 0x06000C03 RID: 3075 RVA: 0x000AD6B8 File Offset: 0x000AB8B8
	private void addTownPlansZones()
	{
		this.add(new CultureTrait
		{
			id = "city_layout_architects_eye",
			group_id = "town_plan"
		});
		this.t.setTownLayoutPlan(new PassableZoneChecker(TownPlans.isInPassableRingMap));
		this.add(new CultureTrait
		{
			id = "city_layout_madman_labyrinth",
			group_id = "town_plan"
		});
		this.t.setTownLayoutPlan(new PassableZoneChecker(TownPlans.isPassableMadmanLabyrinth));
		this.add(new CultureTrait
		{
			id = "city_layout_honeycomb",
			group_id = "town_plan"
		});
		this.t.setUnlockedWithAchievement("achievementSwarm");
		this.t.setTownLayoutPlan(new PassableZoneChecker(TownPlans.isPassableHoneycomb));
		this.add(new CultureTrait
		{
			id = "city_layout_bricks",
			group_id = "town_plan"
		});
		this.t.setTownLayoutPlan(new PassableZoneChecker(TownPlans.isPassableBrickHorizontal));
		this.add(new CultureTrait
		{
			id = "city_layout_raindrops",
			group_id = "town_plan"
		});
		this.t.setTownLayoutPlan(new PassableZoneChecker(TownPlans.isPassableBrickVertical));
		this.add(new CultureTrait
		{
			id = "city_layout_cross",
			group_id = "town_plan"
		});
		this.t.setTownLayoutPlan(new PassableZoneChecker(TownPlans.isPassableCross));
		this.add(new CultureTrait
		{
			id = "city_layout_claws",
			group_id = "town_plan"
		});
		this.t.setTownLayoutPlan(new PassableZoneChecker(TownPlans.isPassableDiagonal));
		this.add(new CultureTrait
		{
			id = "city_layout_pebbles",
			group_id = "town_plan",
			priority = 50
		});
		this.t.setTownLayoutPlan(new PassableZoneChecker(TownPlans.isPassableClustersSmall));
		this.add(new CultureTrait
		{
			id = "city_layout_stone_garden",
			group_id = "town_plan",
			priority = 49
		});
		this.t.setTownLayoutPlan(new PassableZoneChecker(TownPlans.isPassableClustersMedium));
		this.add(new CultureTrait
		{
			id = "city_layout_titan_footprints",
			group_id = "town_plan",
			priority = 48
		});
		this.t.setTownLayoutPlan(new PassableZoneChecker(TownPlans.isPassableClustersBig));
		this.add(new CultureTrait
		{
			id = "city_layout_silk_web",
			group_id = "town_plan",
			priority = 47
		});
		this.t.setTownLayoutPlan(new PassableZoneChecker(TownPlans.isPassableLatticeSmall));
		this.add(new CultureTrait
		{
			id = "city_layout_iron_weave",
			group_id = "town_plan",
			priority = 46
		});
		this.t.setTownLayoutPlan(new PassableZoneChecker(TownPlans.isPassableLatticeMedium));
		this.add(new CultureTrait
		{
			id = "city_layout_monolith_mesh",
			group_id = "town_plan",
			priority = 45
		});
		this.t.setTownLayoutPlan(new PassableZoneChecker(TownPlans.isPassableLatticeBig));
		this.add(new CultureTrait
		{
			id = "city_layout_royal_checkers",
			group_id = "town_plan",
			priority = 44
		});
		this.t.setTownLayoutPlan(new PassableZoneChecker(TownPlans.isPassableDiamondCluster));
		this.add(new CultureTrait
		{
			id = "city_layout_diamond",
			group_id = "town_plan",
			priority = 43
		});
		this.t.setTownLayoutPlan(new PassableZoneChecker(TownPlans.isPassableDiamond));
		this.add(new CultureTrait
		{
			id = "city_layout_parallels",
			group_id = "town_plan",
			priority = 42
		});
		this.t.setTownLayoutPlan(new PassableZoneChecker(TownPlans.isPassableLineHorizontal));
		this.add(new CultureTrait
		{
			id = "city_layout_pillars",
			group_id = "town_plan",
			priority = 41
		});
		this.t.setTownLayoutPlan(new PassableZoneChecker(TownPlans.isPassableLineVertical));
		this.add(new CultureTrait
		{
			id = "city_layout_rings",
			group_id = "town_plan"
		});
		this.t.setTownLayoutPlan(new PassableZoneChecker(TownPlans.isInPassableRing));
		this.addTownLayoutOpposites();
	}

	// Token: 0x06000C04 RID: 3076 RVA: 0x000ADB30 File Offset: 0x000ABD30
	private void addTownPlansTilePlacements()
	{
		this.add(new CultureTrait
		{
			id = "buildings_spread",
			group_id = "town_plan",
			priority = 100
		});
		this.add(new CultureTrait
		{
			id = "city_layout_tile_wobbly_pattern",
			group_id = "town_plan",
			priority = 98
		});
		this.t.addOpposite("city_layout_the_grand_arrangement");
		this.t.addOpposite("city_layout_tile_moonsteps");
		this.add(new CultureTrait
		{
			id = "city_layout_the_grand_arrangement",
			group_id = "town_plan",
			priority = 99
		});
		this.t.addOpposite("city_layout_tile_wobbly_pattern");
		this.t.addOpposite("city_layout_tile_moonsteps");
		this.add(new CultureTrait
		{
			id = "city_layout_tile_moonsteps",
			group_id = "town_plan",
			priority = 97
		});
		this.t.addOpposite("city_layout_tile_wobbly_pattern");
		this.t.addOpposite("city_layout_the_grand_arrangement");
	}

	// Token: 0x06000C05 RID: 3077 RVA: 0x000ADC48 File Offset: 0x000ABE48
	private void addTownLayoutOpposites()
	{
		using (ListPool<string> tAllFitting = new ListPool<string>())
		{
			foreach (CultureTrait tAsset in this.list)
			{
				if (tAsset.town_layout_plan)
				{
					tAllFitting.Add(tAsset.id);
				}
			}
			foreach (CultureTrait tAsset2 in this.list)
			{
				if (tAsset2.town_layout_plan)
				{
					tAsset2.addOpposites(tAllFitting);
					tAsset2.removeOpposite(tAsset2.id);
				}
			}
		}
	}

	// Token: 0x06000C06 RID: 3078 RVA: 0x000ADD1C File Offset: 0x000ABF1C
	private void addWeaponRelatedTraits()
	{
		this.add(new CultureTrait
		{
			id = "axe_lovers",
			value = 10f,
			group_id = "weapons",
			is_weapon_trait = true
		});
		this.t.addWeaponSubtype("axe");
		this.add(new CultureTrait
		{
			id = "sword_lovers",
			value = 10f,
			group_id = "weapons",
			is_weapon_trait = true
		});
		this.t.addWeaponSubtype("sword");
		this.add(new CultureTrait
		{
			id = "bow_lovers",
			value = 6f,
			group_id = "weapons",
			is_weapon_trait = true
		});
		this.t.addWeaponSubtype("bow");
		this.add(new CultureTrait
		{
			id = "hammer_lovers",
			value = 10f,
			group_id = "weapons",
			is_weapon_trait = true
		});
		this.t.addWeaponSubtype("hammer");
		this.add(new CultureTrait
		{
			id = "spear_lovers",
			value = 10f,
			group_id = "weapons",
			is_weapon_trait = true
		});
		this.t.addWeaponSubtype("spear");
		this.add(new CultureTrait
		{
			id = "craft_flame_weapon",
			value = 10f,
			group_id = "craft",
			is_weapon_trait = true
		});
		this.t.addWeaponSpecial("flame_sword");
		this.t.addWeaponSpecial("flame_hammer");
		this.add(new CultureTrait
		{
			id = "craft_ice_weapon",
			value = 10f,
			group_id = "craft",
			is_weapon_trait = true
		});
		this.t.addWeaponSpecial("ice_hammer");
		this.add(new CultureTrait
		{
			id = "craft_evil_staff",
			value = 10f,
			group_id = "craft",
			is_weapon_trait = true
		});
		this.t.addWeaponSpecial("evil_staff");
		this.add(new CultureTrait
		{
			id = "craft_white_staff",
			value = 10f,
			group_id = "craft",
			is_weapon_trait = true
		});
		this.t.addWeaponSpecial("white_staff");
		this.add(new CultureTrait
		{
			id = "craft_necro_staff",
			value = 10f,
			group_id = "craft",
			is_weapon_trait = true
		});
		this.t.addWeaponSpecial("necromancer_staff");
		this.add(new CultureTrait
		{
			id = "craft_druid_staff",
			value = 10f,
			group_id = "craft",
			is_weapon_trait = true
		});
		this.t.addWeaponSpecial("druid_staff");
		this.add(new CultureTrait
		{
			id = "craft_doctor_staff",
			value = 10f,
			group_id = "craft",
			is_weapon_trait = true
		});
		this.t.addWeaponSpecial("plague_doctor_staff");
		this.add(new CultureTrait
		{
			id = "craft_shotgun",
			value = 10f,
			group_id = "craft",
			is_weapon_trait = true,
			priority = -1,
			spawn_random_trait_allowed = false
		});
		this.t.setUnlockedWithAchievement("achievementSwordWithShotgun");
		this.t.addWeaponSpecial("shotgun");
		this.add(new CultureTrait
		{
			id = "craft_blaster",
			value = 10f,
			group_id = "craft",
			is_weapon_trait = true,
			priority = -2,
			spawn_random_trait_allowed = false
		});
		this.t.addWeaponSpecial("alien_blaster");
	}

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x06000C07 RID: 3079 RVA: 0x000AE11E File Offset: 0x000AC31E
	protected override string icon_path
	{
		get
		{
			return "ui/Icons/culture_traits/";
		}
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x000AE125 File Offset: 0x000AC325
	public static float getValueFloat(string pID)
	{
		return AssetManager.culture_traits.get(pID).value;
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x000AE137 File Offset: 0x000AC337
	public static int getValue(string pID)
	{
		return (int)AssetManager.culture_traits.get(pID).value;
	}
}
