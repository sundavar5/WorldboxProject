using System;
using System.Collections.Generic;

// Token: 0x020001A0 RID: 416
public class ReligionTraitLibrary : BaseTraitLibrary<ReligionTrait>
{
	// Token: 0x06000C25 RID: 3109 RVA: 0x000AED15 File Offset: 0x000ACF15
	protected override List<string> getDefaultTraitsForMeta(ActorAsset pAsset)
	{
		return pAsset.default_religion_traits;
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x000AED20 File Offset: 0x000ACF20
	public override void init()
	{
		base.init();
		this.addMagicSpells();
		this.addTransformations();
		this.addHarmony();
		this.addRites();
		this.addSpecial();
		this.add(new ReligionTrait
		{
			id = "grin_mark",
			group_id = "fate",
			spawn_random_trait_allowed = false,
			priority = -100
		});
		this.t.setTraitInfoToGrinMark();
		this.t.setUnlockedWithAchievement("achievementCreaturesExplorer");
	}

	// Token: 0x06000C27 RID: 3111 RVA: 0x000AEDA0 File Offset: 0x000ACFA0
	private void addTransformations()
	{
		this.add(new ReligionTrait
		{
			id = "$transformation$",
			group_id = "transformation",
			spawn_random_trait_allowed = false
		});
		this.clone("sands_of_ruin", "$transformation$");
		this.t.transformation_biome_id = "biome_desert";
		this.clone("shadowroot", "$transformation$");
		this.t.transformation_biome_id = "biome_corrupted";
		this.clone("echo_of_the_void", "$transformation$");
		this.t.transformation_biome_id = "biome_singularity";
		this.clone("infernal_rot", "$transformation$");
		this.t.transformation_biome_id = "biome_infernal";
		this.clone("cosmic_radiation", "$transformation$");
		this.t.transformation_biome_id = "biome_wasteland";
	}

	// Token: 0x06000C28 RID: 3112 RVA: 0x000AEE7C File Offset: 0x000AD07C
	private void addSpecial()
	{
		this.add(new ReligionTrait
		{
			id = "divine_insight",
			group_id = "special",
			can_be_given = false,
			can_be_removed = false,
			can_be_in_book = false,
			spawn_random_trait_allowed = false
		});
		this.add(new ReligionTrait
		{
			id = "bloodline_bond",
			group_id = "the_void"
		});
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x000AEEEC File Offset: 0x000AD0EC
	private void addRites()
	{
		this.add(new ReligionTrait
		{
			id = "rite_of_change",
			group_id = "the_void",
			plot_id = "clan_ascension",
			priority = -1
		});
		ReligionTrait t = this.t;
		t.action_death = (WorldAction)Delegate.Combine(t.action_death, new WorldAction(ActionLibrary.mageSlayerCheck));
		this.add(new ReligionTrait
		{
			id = "rite_of_shattered_earth",
			group_id = "destruction",
			plot_id = "summon_earthquake",
			priority = -1
		});
		this.t.setUnlockedWithAchievement("achievementWatchYourMouth");
		ReligionTrait t2 = this.t;
		t2.action_death = (WorldAction)Delegate.Combine(t2.action_death, new WorldAction(ActionLibrary.mageSlayerCheck));
		this.add(new ReligionTrait
		{
			id = "rite_of_falling_stars",
			group_id = "destruction",
			plot_id = "summon_meteor_rain",
			priority = -1
		});
		this.t.setUnlockedWithAchievement("achievementMayday");
		ReligionTrait t3 = this.t;
		t3.action_death = (WorldAction)Delegate.Combine(t3.action_death, new WorldAction(ActionLibrary.mageSlayerCheck));
		this.add(new ReligionTrait
		{
			id = "rite_of_roaring_skies",
			group_id = "destruction",
			plot_id = "summon_thunderstorm",
			priority = -1
		});
		this.t.setUnlockedWithAchievement("achievementMayIInterrupt");
		ReligionTrait t4 = this.t;
		t4.action_death = (WorldAction)Delegate.Combine(t4.action_death, new WorldAction(ActionLibrary.mageSlayerCheck));
		this.add(new ReligionTrait
		{
			id = "rite_of_tempest_call",
			group_id = "destruction",
			plot_id = "summon_stormfront",
			priority = -1
		});
		ReligionTrait t5 = this.t;
		t5.action_death = (WorldAction)Delegate.Combine(t5.action_death, new WorldAction(ActionLibrary.mageSlayerCheck));
		this.add(new ReligionTrait
		{
			id = "rite_of_infernal_wrath",
			group_id = "destruction",
			plot_id = "summon_hellstorm",
			priority = -1
		});
		ReligionTrait t6 = this.t;
		t6.action_death = (WorldAction)Delegate.Combine(t6.action_death, new WorldAction(ActionLibrary.mageSlayerCheck));
		this.add(new ReligionTrait
		{
			id = "rite_of_the_abyss",
			group_id = "destruction",
			plot_id = "summon_demons",
			priority = -1
		});
		ReligionTrait t7 = this.t;
		t7.action_death = (WorldAction)Delegate.Combine(t7.action_death, new WorldAction(ActionLibrary.mageSlayerCheck));
		this.add(new ReligionTrait
		{
			id = "rite_of_infinite_edges",
			group_id = "protection",
			plot_id = "summon_angles",
			priority = -1
		});
		ReligionTrait t8 = this.t;
		t8.action_death = (WorldAction)Delegate.Combine(t8.action_death, new WorldAction(ActionLibrary.mageSlayerCheck));
		this.add(new ReligionTrait
		{
			id = "rite_of_restless_dead",
			group_id = "necromancy",
			plot_id = "summon_skeletons",
			priority = -1
		});
		ReligionTrait t9 = this.t;
		t9.action_death = (WorldAction)Delegate.Combine(t9.action_death, new WorldAction(ActionLibrary.mageSlayerCheck));
		this.add(new ReligionTrait
		{
			id = "rite_of_fractured_minds",
			group_id = "the_void",
			plot_id = "big_cast_madness",
			priority = -1
		});
		this.t.setUnlockedWithAchievement("achievementGreg");
		ReligionTrait t10 = this.t;
		t10.action_death = (WorldAction)Delegate.Combine(t10.action_death, new WorldAction(ActionLibrary.mageSlayerCheck));
		this.add(new ReligionTrait
		{
			id = "rite_of_dissent",
			group_id = "the_void",
			plot_id = "cause_rebellion",
			priority = -1
		});
		this.t.setUnlockedWithAchievement("achievementNotJustACult");
		ReligionTrait t11 = this.t;
		t11.action_death = (WorldAction)Delegate.Combine(t11.action_death, new WorldAction(ActionLibrary.mageSlayerCheck));
		this.add(new ReligionTrait
		{
			id = "rite_of_unbroken_shield",
			group_id = "protection",
			plot_id = "big_cast_bubble_shield",
			priority = -1
		});
		ReligionTrait t12 = this.t;
		t12.action_death = (WorldAction)Delegate.Combine(t12.action_death, new WorldAction(ActionLibrary.mageSlayerCheck));
		this.add(new ReligionTrait
		{
			id = "rite_of_eternal_brew",
			group_id = "necromancy",
			plot_id = "big_cast_coffee",
			priority = -1
		});
		this.t.setUnlockedWithAchievement("achievementGodFingerLightning");
		ReligionTrait t13 = this.t;
		t13.action_death = (WorldAction)Delegate.Combine(t13.action_death, new WorldAction(ActionLibrary.mageSlayerCheck));
		this.add(new ReligionTrait
		{
			id = "rite_of_entanglement",
			group_id = "creation",
			plot_id = "big_cast_slowness",
			priority = -1
		});
		ReligionTrait t14 = this.t;
		t14.action_death = (WorldAction)Delegate.Combine(t14.action_death, new WorldAction(ActionLibrary.mageSlayerCheck));
		this.add(new ReligionTrait
		{
			id = "rite_of_living_harvest",
			group_id = "creation",
			plot_id = "summon_living_plants",
			priority = -1
		});
		ReligionTrait t15 = this.t;
		t15.action_death = (WorldAction)Delegate.Combine(t15.action_death, new WorldAction(ActionLibrary.mageSlayerCheck));
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x000AF4B0 File Offset: 0x000AD6B0
	private void addHarmony()
	{
		this.add(new ReligionTrait
		{
			id = "minds_awakening",
			group_id = "harmony"
		});
		this.t.base_stats["intelligence"] = 10f;
		this.add(new ReligionTrait
		{
			id = "zeal_of_conquest",
			group_id = "harmony"
		});
		this.t.base_stats["warfare"] = 10f;
		this.add(new ReligionTrait
		{
			id = "path_of_unity",
			group_id = "harmony"
		});
		this.t.base_stats["diplomacy"] = 10f;
		this.add(new ReligionTrait
		{
			id = "hand_of_order",
			group_id = "harmony"
		});
		this.t.base_stats["stewardship"] = 10f;
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x000AF5B0 File Offset: 0x000AD7B0
	private void addMagicSpells()
	{
		this.add(new ReligionTrait
		{
			id = "$magic_spell$",
			group_id = "creation"
		});
		ReligionTrait t = this.t;
		t.action_death = (WorldAction)Delegate.Combine(t.action_death, new WorldAction(ActionLibrary.mageSlayerCheck));
		this.clone("teleport", "$magic_spell$");
		this.t.setUnlockedWithAchievement("achievementTraitExplorerReligion");
		this.t.group_id = "the_void";
		this.t.addSpell("teleport");
		this.clone("cast_silence", "$magic_spell$");
		this.t.group_id = "the_void";
		this.t.addSpell("cast_silence");
		this.clone("summon_lightning", "$magic_spell$");
		this.t.group_id = "destruction";
		this.t.addSpell("summon_lightning");
		this.clone("summon_tornado", "$magic_spell$");
		this.t.group_id = "destruction";
		this.t.addSpell("summon_tornado");
		this.clone("cast_curse", "$magic_spell$");
		this.t.addSpell("cast_curse");
		this.clone("cast_fire", "$magic_spell$");
		this.t.group_id = "destruction";
		this.t.addSpell("cast_fire");
		this.clone("cast_blood_rain", "$magic_spell$");
		this.t.group_id = "restoration";
		this.t.addSpell("cast_blood_rain");
		this.clone("cast_grass_seeds", "$magic_spell$");
		this.t.group_id = "creation";
		this.t.addSpell("cast_grass_seeds");
		this.clone("spawn_vegetation", "$magic_spell$");
		this.t.group_id = "creation";
		this.t.addSpell("spawn_vegetation");
		this.clone("spawn_skeleton", "$magic_spell$");
		this.t.setUnlockedWithAchievement("achievementTheCorruptedTrees");
		this.t.group_id = "necromancy";
		this.t.addSpell("spawn_skeleton");
		this.clone("cast_shield", "$magic_spell$");
		this.t.group_id = "protection";
		this.t.addSpell("cast_shield");
		this.clone("cast_cure", "$magic_spell$");
		this.t.group_id = "restoration";
		this.t.addSpell("cast_cure");
		this.add(new ReligionTrait
		{
			id = "blessed_by_ashes",
			group_id = "protection"
		});
		this.t.setUnlockedWithAchievement("achievementSacrifice");
		this.t.base_stats_meta.addTag("building_immunity_fire");
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x06000C2C RID: 3116 RVA: 0x000AF8A9 File Offset: 0x000ADAA9
	protected override string icon_path
	{
		get
		{
			return "ui/Icons/religion_traits/";
		}
	}

	// Token: 0x04000B62 RID: 2914
	private const string TEMPLATE_MAGIC_SPELL = "$magic_spell$";

	// Token: 0x04000B63 RID: 2915
	private const string TEMPLATE_TRANSFORMATION = "$transformation$";
}
