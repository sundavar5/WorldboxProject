using System;
using System.Collections.Generic;
using Beebyte.Obfuscator;

// Token: 0x0200018B RID: 395
[ObfuscateLiterals]
[Serializable]
public class ActorTraitLibrary : BaseTraitLibrary<ActorTrait>
{
	// Token: 0x06000BAB RID: 2987 RVA: 0x000A6326 File Offset: 0x000A4526
	protected override List<string> getDefaultTraitsForMeta(ActorAsset pAsset)
	{
		return pAsset.traits;
	}

	// Token: 0x06000BAC RID: 2988 RVA: 0x000A632E File Offset: 0x000A452E
	public override void init()
	{
		base.init();
		this.addTraitsSpecial();
		this.addTraitsBody();
		this.addTraitsMind();
		this.addTraitsSpirit();
		this.addTraitsAcquired();
		this.addTraitsFun();
		this.addTraitsMisc();
	}

	// Token: 0x06000BAD RID: 2989 RVA: 0x000A6360 File Offset: 0x000A4560
	private void addTraitsBody()
	{
		this.add(new ActorTrait
		{
			id = "dash",
			path_icon = "ui/Icons/skills/iconSkillDash",
			group_id = "skills",
			in_training_dummy_combat_pot = true
		});
		this.t.addCombatAction("combat_dash");
		this.add(new ActorTrait
		{
			id = "block",
			path_icon = "ui/Icons/skills/iconSkillBlock",
			group_id = "skills",
			in_training_dummy_combat_pot = true
		});
		this.t.addCombatAction("combat_block");
		this.add(new ActorTrait
		{
			id = "dodge",
			path_icon = "ui/Icons/skills/iconSkillDodge",
			group_id = "skills",
			in_training_dummy_combat_pot = true
		});
		this.t.addCombatAction("combat_dodge");
		this.add(new ActorTrait
		{
			id = "backstep",
			path_icon = "ui/Icons/skills/iconSkillBackstep",
			group_id = "skills",
			in_training_dummy_combat_pot = true
		});
		this.t.addCombatAction("combat_backstep");
		this.add(new ActorTrait
		{
			id = "deflect_projectile",
			path_icon = "ui/Icons/skills/iconSkillDeflectProjectile",
			group_id = "skills",
			in_training_dummy_combat_pot = true
		});
		this.t.addCombatAction("combat_deflect_projectile");
		this.add(new ActorTrait
		{
			id = "mute",
			path_icon = "ui/Icons/actor_traits/iconMute",
			group_id = "body",
			rate_birth = 1,
			rate_inherit = 5,
			likeability = -0.1f
		});
		this.add(new ActorTrait
		{
			id = "sunblessed",
			path_icon = "ui/Icons/actor_traits/iconSunblessed",
			group_id = "body",
			rate_birth = 2,
			rate_inherit = 5
		});
		this.t.special_effect_interval = 5f;
		ActorTrait t = this.t;
		t.action_special_effect = (WorldAction)Delegate.Combine(t.action_special_effect, new WorldAction(ActionLibrary.sunblessedEffect));
		this.add(new ActorTrait
		{
			id = "clumsy",
			rarity = Rarity.R0_Normal,
			path_icon = "ui/Icons/actor_traits/iconClumsy",
			group_id = "physique",
			rate_birth = 5,
			rate_inherit = 5
		});
		this.t.addOpposite("long_liver");
		this.t.base_stats["multiplier_lifespan"] = -0.5f;
		this.add(new ActorTrait
		{
			id = "fragile_health",
			rarity = Rarity.R0_Normal,
			path_icon = "ui/Icons/actor_traits/iconFrail",
			group_id = "health",
			rate_birth = 5,
			rate_inherit = 5
		});
		this.t.addOpposite("boosted_vitality");
		this.t.base_stats["multiplier_health"] = -0.5f;
		this.add(new ActorTrait
		{
			id = "boosted_vitality",
			path_icon = "ui/Icons/actor_traits/iconBoostedVitality",
			group_id = "health",
			rate_birth = 5,
			rate_inherit = 5
		});
		this.t.addOpposite("fragile_health");
		this.t.base_stats["multiplier_health"] = 0.5f;
		this.add(new ActorTrait
		{
			id = "hard_skin",
			rarity = Rarity.R0_Normal,
			path_icon = "ui/Icons/actor_traits/iconHardSkin",
			group_id = "physique",
			rate_birth = 5,
			rate_inherit = 5
		});
		this.t.addOpposite("soft_skin");
		this.t.base_stats["armor"] = 5f;
		this.add(new ActorTrait
		{
			id = "soft_skin",
			rarity = Rarity.R0_Normal,
			path_icon = "ui/Icons/actor_traits/iconSoftSkin",
			group_id = "physique",
			rate_birth = 5,
			rate_inherit = 5
		});
		this.t.addOpposite("hard_skin");
		this.t.base_stats["armor"] = -5f;
		this.add(new ActorTrait
		{
			id = "long_liver",
			rarity = Rarity.R0_Normal,
			path_icon = "ui/Icons/actor_traits/iconLongLiver",
			group_id = "health",
			rate_birth = 2,
			rate_inherit = 5
		});
		this.t.addOpposite("clumsy");
		this.t.base_stats["multiplier_lifespan"] = 0.5f;
		this.add(new ActorTrait
		{
			id = "acid_touch",
			path_icon = "ui/Icons/actor_traits/iconAcidTouch",
			unlocked_with_achievement = true,
			achievement_id = "achievementLetsNot",
			group_id = "body",
			likeability = -0.1f
		});
		ActorTrait t2 = this.t;
		t2.action_special_effect = (WorldAction)Delegate.Combine(t2.action_special_effect, new WorldAction(ActionLibrary.acidTouchEffect));
		this.add(new ActorTrait
		{
			id = "acid_blood",
			path_icon = "ui/Icons/actor_traits/iconAcidBlood",
			unlocked_with_achievement = true,
			achievement_id = "achievementLetsNot",
			group_id = "body",
			rate_inherit = 5,
			likeability = -0.1f
		});
		ActorTrait t3 = this.t;
		t3.action_death = (WorldAction)Delegate.Combine(t3.action_death, new WorldAction(ActionLibrary.acidBloodEffect));
		this.add(new ActorTrait
		{
			id = "acid_proof",
			path_icon = "ui/Icons/actor_traits/iconAcidProof",
			unlocked_with_achievement = true,
			achievement_id = "achievementLetsNot",
			group_id = "protection",
			rate_inherit = 5
		});
		this.add(new ActorTrait
		{
			id = "fire_blood",
			path_icon = "ui/Icons/actor_traits/iconFireBlood",
			group_id = "body",
			rate_inherit = 5
		});
		ActorTrait t4 = this.t;
		t4.action_death = (WorldAction)Delegate.Combine(t4.action_death, new WorldAction(ActionLibrary.fireDropsSpawn));
		this.add(new ActorTrait
		{
			id = "fire_proof",
			path_icon = "ui/Icons/actor_traits/iconFireProof",
			group_id = "protection",
			rate_inherit = 5
		});
		this.t.base_stats.addTag("immunity_fire");
		this.add(new ActorTrait
		{
			id = "freeze_proof",
			path_icon = "ui/Icons/actor_traits/iconFreezeProof",
			group_id = "protection",
			rate_inherit = 5
		});
		this.t.base_stats.addTag("immunity_cold");
		this.add(new ActorTrait
		{
			id = "regeneration",
			path_icon = "ui/Icons/actor_traits/iconRegeneration",
			rate_birth = 1,
			rate_inherit = 5,
			group_id = "health",
			type = TraitType.Positive,
			special_effect_interval = 3f
		});
		ActorTrait t5 = this.t;
		t5.action_special_effect = (WorldAction)Delegate.Combine(t5.action_special_effect, new WorldAction(ActionLibrary.regenerationEffect));
		this.add(new ActorTrait
		{
			id = "heliophobia",
			path_icon = "ui/Icons/actor_traits/iconHeliophobia",
			rate_inherit = 10,
			group_id = "body",
			type = TraitType.Negative,
			special_effect_interval = 10f
		});
		ActorTrait t6 = this.t;
		t6.action_special_effect = (WorldAction)Delegate.Combine(t6.action_special_effect, new WorldAction(ActionLibrary.heliophobiaEffect));
		this.add(new ActorTrait
		{
			id = "ugly",
			rarity = Rarity.R0_Normal,
			path_icon = "ui/Icons/actor_traits/iconUgly",
			rate_birth = 7,
			same_trait_mod = 5,
			opposite_trait_mod = -15,
			likeability = -0.1f,
			group_id = "appearance",
			type = TraitType.Negative
		});
		this.t.base_stats["multiplier_offspring"] = -0.3f;
		this.t.addOpposite("attractive");
		this.add(new ActorTrait
		{
			id = "fat",
			rarity = Rarity.R0_Normal,
			path_icon = "ui/Icons/actor_traits/iconFat",
			rate_birth = 7,
			rate_inherit = 5,
			opposite_trait_mod = -10,
			same_trait_mod = 10,
			likeability = -0.1f,
			group_id = "physique",
			type = TraitType.Negative
		});
		this.t.addOpposite("agile");
		this.t.addOpposite("weightless");
		this.t.base_stats["multiplier_mass"] = 0.3f;
		this.t.base_stats["scale"] = 0.02f;
		this.t.base_stats["multiplier_stamina"] = -0.5f;
		this.t.base_stats["multiplier_damage"] = 0.1f;
		this.add(new ActorTrait
		{
			id = "attractive",
			path_icon = "ui/Icons/actor_traits/iconAttractive",
			rate_birth = 3,
			rate_inherit = 5,
			same_trait_mod = 10,
			likeability = 0.1f,
			group_id = "appearance",
			type = TraitType.Positive
		});
		this.t.addOpposite("ugly");
		this.t.base_stats["diplomacy"] = 2f;
		this.t.base_stats["stewardship"] = 1f;
		this.t.base_stats["critical_chance"] = 0.1f;
		this.t.base_stats["multiplier_offspring"] = 0.6f;
		this.add(new ActorTrait
		{
			id = "fast",
			rarity = Rarity.R0_Normal,
			path_icon = "ui/Icons/actor_traits/iconFast",
			rate_birth = 4,
			rate_inherit = 5,
			remove_for_zombie_actor_asset = true,
			group_id = "physique",
			type = TraitType.Positive
		});
		this.t.addOpposite("slow");
		this.t.base_stats["multiplier_speed"] = 0.3f;
		this.t.base_stats["attack_speed"] = 5f;
		this.add(new ActorTrait
		{
			id = "slow",
			rarity = Rarity.R0_Normal,
			path_icon = "ui/Icons/actor_traits/iconSlow",
			rate_birth = 6,
			rate_inherit = 5,
			group_id = "physique",
			type = TraitType.Negative
		});
		this.t.addOpposite("fast");
		this.t.addOpposite("agile");
		this.t.base_stats["multiplier_speed"] = -0.5f;
		this.t.base_stats["attack_speed"] = -5f;
		this.add(new ActorTrait
		{
			id = "gluttonous",
			rarity = Rarity.R0_Normal,
			path_icon = "ui/Icons/actor_traits/iconGluttonous",
			rate_birth = 4,
			rate_inherit = 5,
			same_trait_mod = 5,
			group_id = "mind",
			type = TraitType.Negative
		});
		this.add(new ActorTrait
		{
			id = "giant",
			path_icon = "ui/Icons/actor_traits/iconGiant",
			group_id = "physique",
			type = TraitType.Positive,
			rate_birth = 2,
			rate_inherit = 7,
			unlocked_with_achievement = true,
			achievement_id = "achievementTORNADO"
		});
		this.t.addOpposite("tiny");
		this.t.base_stats["scale"] = 0.05f;
		this.t.base_stats["multiplier_health"] = 0.5f;
		this.t.base_stats["multiplier_speed"] = -0.25f;
		this.add(new ActorTrait
		{
			id = "tiny",
			path_icon = "ui/Icons/actor_traits/iconTiny",
			group_id = "physique",
			type = TraitType.Negative,
			rate_birth = 4,
			rate_inherit = 7,
			unlocked_with_achievement = true,
			achievement_id = "achievementBabyTornado"
		});
		this.t.addOpposite("giant");
		this.t.base_stats["diplomacy"] = -1f;
		this.t.base_stats["scale"] = -0.02f;
		this.t.base_stats["multiplier_health"] = -0.25f;
		this.t.base_stats["multiplier_speed"] = 0.25f;
		this.add(new ActorTrait
		{
			id = "eagle_eyed",
			rarity = Rarity.R0_Normal,
			path_icon = "ui/Icons/actor_traits/iconEagleEye",
			rate_birth = 3,
			rate_inherit = 5,
			group_id = "cognitive",
			type = TraitType.Positive
		});
		this.t.addOpposite("short_sighted");
		this.t.base_stats["accuracy"] = 5f;
		this.t.base_stats["critical_chance"] = 0.15f;
		this.add(new ActorTrait
		{
			id = "short_sighted",
			rarity = Rarity.R0_Normal,
			path_icon = "ui/Icons/actor_traits/iconShortsighted",
			rate_birth = 5,
			rate_inherit = 5,
			group_id = "cognitive",
			type = TraitType.Negative
		});
		this.t.addOpposite("eagle_eyed");
		this.t.base_stats["accuracy"] = -5f;
		this.t.base_stats["critical_chance"] = -0.05f;
		this.add(new ActorTrait
		{
			id = "infertile",
			path_icon = "ui/Icons/actor_traits/iconInfertile",
			rate_birth = 1,
			rate_inherit = 5,
			group_id = "health",
			type = TraitType.Negative
		});
		this.t.addOpposite("fertile");
		this.t.base_stats["offspring"] = -99999f;
		this.add(new ActorTrait
		{
			id = "fertile",
			rarity = Rarity.R0_Normal,
			path_icon = "ui/Icons/actor_traits/iconFertile",
			rate_birth = 3,
			rate_inherit = 7,
			group_id = "health",
			type = TraitType.Positive,
			likeability = 0.1f
		});
		this.t.addOpposite("infertile");
		this.t.base_stats["multiplier_offspring"] = 0.8f;
		this.t.base_stats["birth_rate"] = 4f;
		this.add(new ActorTrait
		{
			id = "thorns",
			path_icon = "ui/Icons/actor_traits/iconThorns",
			group_id = "protection",
			rate_inherit = 5
		});
		ActorTrait t7 = this.t;
		t7.action_get_hit = (GetHitAction)Delegate.Combine(t7.action_get_hit, new GetHitAction(ActionLibrary.thornsDefense));
		this.add(new ActorTrait
		{
			id = "bubble_defense",
			path_icon = "ui/Icons/actor_traits/iconBubbleDefense",
			group_id = "protection",
			rate_inherit = 3
		});
		ActorTrait t8 = this.t;
		t8.action_get_hit = (GetHitAction)Delegate.Combine(t8.action_get_hit, new GetHitAction(ActionLibrary.bubbleDefense));
		this.add(new ActorTrait
		{
			id = "immune",
			path_icon = "ui/Icons/actor_traits/iconImmune",
			rate_birth = 1,
			rate_inherit = 10,
			group_id = "health",
			type = TraitType.Positive
		});
		this.t.addOpposite("plague");
		this.t.addOpposite("tumor_infection");
		this.t.addOpposite("mush_spores");
		this.t.addOpposite("infected");
		this.add(new ActorTrait
		{
			id = "agile",
			path_icon = "ui/Icons/actor_traits/iconAgile",
			rate_birth = 3,
			rate_inherit = 5,
			same_trait_mod = 5,
			remove_for_zombie_actor_asset = true,
			group_id = "physique",
			type = TraitType.Positive
		});
		this.t.addOpposite("fat");
		this.t.addOpposite("slow");
		this.t.base_stats["lifespan"] = 3f;
		this.t.base_stats["scale"] = -0.01f;
		this.t.base_stats["stamina"] = 20f;
		this.t.base_stats["skill_combat"] = 0.2f;
		this.add(new ActorTrait
		{
			id = "weightless",
			path_icon = "ui/Icons/actor_traits/iconWeightless",
			rate_birth = 1,
			rate_inherit = 5,
			group_id = "physique"
		});
		this.t.addOpposite("fat");
		this.add(new ActorTrait
		{
			id = "poisonous",
			path_icon = "ui/Icons/actor_traits/iconPoisonous",
			group_id = "body",
			rate_inherit = 5
		});
		this.add(new ActorTrait
		{
			id = "venomous",
			path_icon = "ui/Icons/actor_traits/iconVenomous",
			group_id = "body",
			rate_inherit = 5
		});
		this.t.action_attack_target = new AttackAction(ActionLibrary.addPoisonedEffectOnTarget);
		this.add(new ActorTrait
		{
			id = "poison_immune",
			path_icon = "ui/Icons/actor_traits/iconPoisonImmune",
			group_id = "protection",
			rate_inherit = 5
		});
		this.add(new ActorTrait
		{
			id = "tough",
			path_icon = "ui/Icons/actor_traits/iconTough",
			rate_birth = 2,
			group_id = "physique",
			type = TraitType.Positive,
			same_trait_mod = -5,
			unlocked_with_achievement = true,
			achievement_id = "achievementDestroyWorldBox"
		});
		this.t.base_stats["armor"] = 10f;
		this.t.base_stats["warfare"] = 1f;
		this.t.base_stats["lifespan"] = 4f;
		this.add(new ActorTrait
		{
			id = "strong",
			rarity = Rarity.R0_Normal,
			path_icon = "ui/Icons/actor_traits/iconStrong",
			rate_birth = 4,
			opposite_trait_mod = -10,
			same_trait_mod = 5,
			group_id = "physique",
			type = TraitType.Positive
		});
		this.t.addOpposite("weak");
		this.t.base_stats["multiplier_damage"] = 0.5f;
		this.t.base_stats["warfare"] = 2f;
		this.t.base_stats["lifespan"] = 3f;
		this.add(new ActorTrait
		{
			id = "weak",
			rarity = Rarity.R0_Normal,
			path_icon = "ui/Icons/actor_traits/iconWeak",
			rate_birth = 5,
			opposite_trait_mod = -10,
			group_id = "physique",
			type = TraitType.Negative
		});
		this.t.addOpposite("strong");
		this.t.base_stats["multiplier_damage"] = -0.5f;
		this.t.base_stats["warfare"] = -2f;
		this.t.base_stats["diplomacy"] = -2f;
		this.t.base_stats["lifespan"] = -6f;
	}

	// Token: 0x06000BAE RID: 2990 RVA: 0x000A7778 File Offset: 0x000A5978
	private void addTraitsMind()
	{
		this.add(new ActorTrait
		{
			id = "lustful",
			path_icon = "ui/Icons/actor_traits/iconLustful",
			group_id = "mind",
			rate_acquire_grow_up = 5,
			rate_birth = 1,
			likeability = 0.1f
		});
		this.t.base_stats["diplomacy"] = -2f;
		this.t.base_stats["multiplier_offspring"] = 0.2f;
		this.add(new ActorTrait
		{
			id = "miner",
			path_icon = "ui/Icons/actor_traits/iconMiner",
			group_id = "miscellaneous",
			type = TraitType.Positive,
			rate_acquire_grow_up = 5
		});
		this.add(new ActorTrait
		{
			id = "psychopath",
			path_icon = "ui/Icons/actor_traits/iconPsychopath",
			group_id = "mind",
			rate_birth = 1,
			type = TraitType.Negative
		});
		this.add(new ActorTrait
		{
			id = "strong_minded",
			path_icon = "ui/Icons/actor_traits/iconStrongMinded",
			group_id = "mind",
			type = TraitType.Positive,
			remove_for_zombie_actor_asset = true
		});
		this.t.base_stats.addTag("strong_mind");
		this.t.addOpposite("madness");
		this.t.addOpposite("desire_golden_egg");
		this.t.addOpposite("desire_harp");
		this.add(new ActorTrait
		{
			id = "peaceful",
			rarity = Rarity.R0_Normal,
			path_icon = "ui/Icons/actor_traits/iconPeaceful",
			remove_for_zombie_actor_asset = true,
			group_id = "mind",
			type = TraitType.Positive
		});
		this.t.base_stats.addTag("love_peace");
		this.add(new ActorTrait
		{
			id = "evil",
			path_icon = "ui/Icons/actor_traits/iconEvil",
			group_id = "mind",
			likeability = -0.2f
		});
		this.t.addOpposite("blessed");
		this.t.base_stats["cities"] = -2f;
		this.t.base_stats["warfare"] = 10f;
		this.add(new ActorTrait
		{
			id = "hotheaded",
			path_icon = "ui/Icons/actor_traits/iconHotheaded",
			rate_birth = 1,
			same_trait_mod = -10,
			group_id = "mind",
			type = TraitType.Negative
		});
		this.add(new ActorTrait
		{
			id = "thief",
			path_icon = "ui/Icons/actor_traits/iconThief",
			rate_birth = 1,
			same_trait_mod = 10,
			group_id = "cognitive",
			type = TraitType.Negative
		});
		this.t.setUnlockedWithAchievement("achievementNotOnMyWatch");
		this.t.addOpposite("honest");
		this.t.addOpposite("content");
		this.t.base_stats.addTag("steal_items");
		this.t.addDecision("try_to_steal_money");
		this.add(new ActorTrait
		{
			id = "stupid",
			rarity = Rarity.R0_Normal,
			path_icon = "ui/Icons/actor_traits/iconStupid",
			rate_birth = 3,
			same_trait_mod = 30,
			group_id = "cognitive",
			type = TraitType.Negative
		});
		this.t.addOpposite("genius");
		this.t.addOpposite("wise");
		this.t.base_stats["damage"] = 5f;
		this.t.base_stats["cities"] = -3f;
		this.t.base_stats["intelligence"] = -5f;
		this.t.base_stats["diplomacy"] = -2f;
		this.t.base_stats["warfare"] = -2f;
		this.t.base_stats["stewardship"] = -5f;
		this.t.base_stats["loyalty_traits"] = -15f;
		this.t.base_stats["personality_rationality"] = -0.5f;
		this.add(new ActorTrait
		{
			id = "genius",
			path_icon = "ui/Icons/actor_traits/iconGenius",
			rate_birth = 1,
			remove_for_zombie_actor_asset = true,
			same_trait_mod = 20,
			opposite_trait_mod = -20,
			unlocked_with_achievement = true,
			achievement_id = "achievementTraitsExplorer60",
			group_id = "cognitive",
			type = TraitType.Positive
		});
		this.t.base_stats.addTag("can_read_any_book");
		this.t.addOpposite("stupid");
		this.t.base_stats["intelligence"] = 10f;
		this.t.base_stats["diplomacy"] = 5f;
		this.t.base_stats["warfare"] = 5f;
		this.t.base_stats["stewardship"] = 7f;
		this.t.base_stats["loyalty_traits"] = -10f;
		this.t.base_stats["cities"] = 3f;
		this.add(new ActorTrait
		{
			id = "deceitful",
			rarity = Rarity.R0_Normal,
			path_icon = "ui/Icons/actor_traits/iconDeceitful",
			rate_acquire_grow_up = 5,
			same_trait_mod = -15,
			opposite_trait_mod = -5,
			likeability = 0.1f,
			group_id = "mind",
			type = TraitType.Negative
		});
		this.t.addOpposite("honest");
		this.t.base_stats["diplomacy"] = 1f;
		this.t.base_stats["stewardship"] = 4f;
		this.t.base_stats["loyalty_traits"] = -20f;
		this.add(new ActorTrait
		{
			id = "ambitious",
			path_icon = "ui/Icons/actor_traits/iconAmbitious",
			rate_acquire_grow_up = 5,
			rate_birth = 1,
			same_trait_mod = -10,
			group_id = "mind",
			achievement_id = "achievement4RaceCities",
			unlocked_with_achievement = true
		});
		this.t.addOpposite("content");
		this.t.base_stats["diplomacy"] = 2f;
		this.t.base_stats["warfare"] = 4f;
		this.t.base_stats["stewardship"] = 1f;
		this.t.base_stats["loyalty_traits"] = -15f;
		this.t.base_stats["cities"] = 5f;
		this.add(new ActorTrait
		{
			id = "content",
			rarity = Rarity.R0_Normal,
			path_icon = "ui/Icons/actor_traits/iconContent",
			rate_acquire_grow_up = 5,
			rate_birth = 2,
			same_trait_mod = 15,
			likeability = 0.1f,
			group_id = "mind",
			type = TraitType.Positive
		});
		this.t.addOpposite("ambitious");
		this.t.addOpposite("greedy");
		this.t.addOpposite("thief");
		this.t.base_stats["multiplier_supply_timer"] = -0.3f;
		this.t.base_stats["loyalty_traits"] = 10f;
		this.t.base_stats["diplomacy"] = 2f;
		this.t.base_stats["stewardship"] = 2f;
		this.t.base_stats["warfare"] = -2f;
		this.add(new ActorTrait
		{
			id = "honest",
			rarity = Rarity.R0_Normal,
			path_icon = "ui/Icons/actor_traits/iconHonest",
			rate_acquire_grow_up = 5,
			rate_birth = 2,
			same_trait_mod = 10,
			opposite_trait_mod = -10,
			likeability = -0.1f,
			group_id = "mind",
			type = TraitType.Positive
		});
		this.t.addOpposite("deceitful");
		this.t.addOpposite("thief");
		this.t.base_stats["stewardship"] = 3f;
		this.t.base_stats["diplomacy"] = 2f;
		this.t.base_stats["warfare"] = -2f;
		this.t.base_stats["loyalty_traits"] = 5f;
		this.add(new ActorTrait
		{
			id = "paranoid",
			path_icon = "ui/Icons/actor_traits/iconParanoid",
			rate_acquire_grow_up = 5,
			rate_birth = 1,
			group_id = "mind",
			type = TraitType.Negative
		});
		this.t.base_stats["diplomacy"] = -2f;
		this.t.base_stats["warfare"] = 4f;
		this.t.base_stats["multiplier_supply_timer"] = 0.5f;
		this.t.base_stats["loyalty_traits"] = -15f;
		this.t.base_stats["cities"] = -1f;
		this.add(new ActorTrait
		{
			id = "greedy",
			path_icon = "ui/Icons/actor_traits/iconGreedy",
			rate_acquire_grow_up = 5,
			rate_birth = 1,
			likeability = -0.1f,
			group_id = "mind",
			type = TraitType.Negative
		});
		this.t.addOpposite("content");
		this.t.base_stats["diplomacy"] = -2f;
		this.t.base_stats["stewardship"] = -3f;
		this.t.base_stats["warfare"] = 4f;
		this.t.base_stats["multiplier_supply_timer"] = 4f;
		this.t.base_stats["loyalty_traits"] = -5f;
		this.t.base_stats["cities"] = 2f;
	}

	// Token: 0x06000BAF RID: 2991 RVA: 0x000A825C File Offset: 0x000A645C
	private void addTraitsSpirit()
	{
		this.add(new ActorTrait
		{
			id = "chosen_one",
			path_icon = "ui/Icons/actor_traits/iconChosenOne",
			likeability = 0.25f,
			group_id = "fate",
			achievement_id = "achievementLavaStrike",
			unlocked_with_achievement = true
		});
		this.t.is_mutation_box_allowed = false;
		this.t.base_stats["stamina"] = 1000f;
		this.t.base_stats["mana"] = 1000f;
		this.t.addCombatAction("combat_backstep");
		this.t.addCombatAction("combat_block");
		this.t.addCombatAction("combat_dash");
		this.t.addCombatAction("combat_deflect_projectile");
		this.t.addCombatAction("combat_dodge");
		this.t.addSpell("cast_fire");
		this.t.addSpell("summon_lightning");
		this.t.addSpell("summon_tornado");
		this.t.addSpell("cast_blood_rain");
		this.t.addSpell("cast_blood_rain");
		this.t.addSpell("cast_cure");
		this.t.addSpell("cast_shield");
		this.t.addSpell("cast_grass_seeds");
		this.t.addSpell("spawn_vegetation");
		this.t.addSpell("cast_curse");
		ActorTrait t = this.t;
		t.action_death = (WorldAction)Delegate.Combine(t.action_death, new WorldAction(ActionLibrary.mageSlayerCheck));
		this.add(new ActorTrait
		{
			id = "moonchild",
			path_icon = "ui/Icons/actor_traits/iconMoonchild",
			only_active_on_era_flag = true,
			era_active_moon = true,
			group_id = "spirit",
			rate_inherit = 5,
			rate_birth = 1
		});
		this.t.base_stats["multiplier_damage"] = 0.5f;
		this.t.base_stats["multiplier_speed"] = 0.1f;
		this.t.base_stats["armor"] = 1f;
		this.t.base_stats["intelligence"] = 3f;
		this.add(new ActorTrait
		{
			id = "nightchild",
			path_icon = "ui/Icons/actor_traits/iconNightchild",
			only_active_on_era_flag = true,
			era_active_night = true,
			group_id = "spirit",
			rate_inherit = 5,
			rate_birth = 1
		});
		this.t.base_stats["multiplier_damage"] = 0.5f;
		this.t.base_stats["multiplier_speed"] = 0.1f;
		this.t.base_stats["critical_chance"] = 0.03f;
		this.t.base_stats["warfare"] = 3f;
		this.add(new ActorTrait
		{
			id = "flesh_eater",
			path_icon = "ui/Icons/actor_traits/iconFleshEater",
			group_id = "spirit",
			rate_inherit = 5,
			rate_birth = 1
		});
		this.t.action_attack_target = new AttackAction(ActionLibrary.restoreHealthOnHit);
		this.add(new ActorTrait
		{
			id = "titan_lungs",
			path_icon = "ui/Icons/actor_traits/iconTitanLungs",
			group_id = "body",
			rate_inherit = 5
		});
		this.t.setUnlockedWithAchievement("achievementNinjaTurtle");
		this.t.base_stats["multiplier_stamina"] = 10f;
		this.add(new ActorTrait
		{
			id = "heart_of_wizard",
			path_icon = "ui/Icons/actor_traits/iconHeartWizard",
			group_id = "spirit",
			rate_inherit = 5
		});
		this.t.base_stats["multiplier_mana"] = 10f;
		this.add(new ActorTrait
		{
			id = "battle_reflexes",
			path_icon = "ui/Icons/actor_traits/iconBattleReflexes",
			group_id = "mind",
			rate_inherit = 5
		});
		this.t.base_stats["skill_combat"] = 0.5f;
		this.add(new ActorTrait
		{
			id = "arcane_reflexes",
			path_icon = "ui/Icons/actor_traits/iconArcaneReflexes",
			group_id = "mind",
			rate_inherit = 5
		});
		this.t.base_stats["skill_spell"] = 0.5f;
		this.add(new ActorTrait
		{
			id = "healing_aura",
			path_icon = "ui/Icons/actor_traits/iconHealingAura",
			group_id = "spirit",
			rate_inherit = 5,
			special_effect_interval = 2f,
			likeability = 0.1f
		});
		ActorTrait t2 = this.t;
		t2.action_special_effect = (WorldAction)Delegate.Combine(t2.action_special_effect, new WorldAction(ActionLibrary.healingAuraEffect));
		this.add(new ActorTrait
		{
			id = "savage",
			path_icon = "ui/Icons/actor_traits/iconSavage",
			group_id = "spirit",
			type = TraitType.Positive,
			same_trait_mod = 5,
			rate_acquire_grow_up = 2,
			rate_inherit = 5
		});
		this.add(new ActorTrait
		{
			id = "immortal",
			path_icon = "ui/Icons/actor_traits/iconImmortal",
			same_trait_mod = -20,
			type = TraitType.Positive,
			unlocked_with_achievement = true,
			achievement_id = "achievementTheKing",
			group_id = "health"
		});
		this.t.addOpposite("plague");
		this.t.addOpposite("boat");
		this.t.base_stats["loyalty_traits"] = -20f;
		this.add(new ActorTrait
		{
			id = "burning_feet",
			path_icon = "ui/Icons/actor_traits/iconBurningFeet",
			unlocked_with_achievement = true,
			achievement_id = "achievementTheHell",
			group_id = "spirit",
			rate_inherit = 3
		});
		ActorTrait t3 = this.t;
		t3.action_special_effect = (WorldAction)Delegate.Combine(t3.action_special_effect, new WorldAction(ActionLibrary.burningFeetEffect));
		this.add(new ActorTrait
		{
			id = "cold_aura",
			path_icon = "ui/Icons/actor_traits/iconColdAura",
			group_id = "spirit",
			rate_inherit = 3
		});
		ActorTrait t4 = this.t;
		t4.action_special_effect = (WorldAction)Delegate.Combine(t4.action_special_effect, new WorldAction(ActionLibrary.coldAuraEffect));
		this.add(new ActorTrait
		{
			id = "lucky",
			path_icon = "ui/Icons/actor_traits/iconLucky",
			rate_birth = 2,
			rate_inherit = 5,
			likeability = 0.1f,
			group_id = "spirit",
			type = TraitType.Positive
		});
		this.t.addOpposite("unlucky");
		this.t.base_stats["lifespan"] = 7f;
		this.t.base_stats["accuracy"] = 4f;
		this.t.base_stats["critical_chance"] = 0.3f;
		this.t.base_stats["birth_rate"] = 5f;
		this.add(new ActorTrait
		{
			id = "unlucky",
			path_icon = "ui/Icons/actor_traits/iconUnlucky",
			rate_birth = 3,
			rate_inherit = 5,
			likeability = -0.1f,
			special_effect_interval = 20f,
			group_id = "spirit",
			type = TraitType.Negative
		});
		this.t.addOpposite("lucky");
		this.t.base_stats["lifespan"] = -13f;
		ActorTrait t5 = this.t;
		t5.action_special_effect = (WorldAction)Delegate.Combine(t5.action_special_effect, new WorldAction(ActionLibrary.unluckyFall));
		this.t.base_stats["accuracy"] = -4f;
		this.t.base_stats["critical_chance"] = -0.3f;
		this.add(new ActorTrait
		{
			id = "bloodlust",
			path_icon = "ui/Icons/actor_traits/iconBloodlust",
			rate_acquire_grow_up = 4,
			rate_birth = 1,
			rate_inherit = 5,
			group_id = "spirit",
			type = TraitType.Negative,
			unlocked_with_achievement = true,
			achievement_id = "achievementTheDemon"
		});
		this.t.addOpposite("pacifist");
		this.t.base_stats["multiplier_supply_timer"] = 1.5f;
		this.t.base_stats["loyalty_traits"] = -20f;
		this.t.base_stats["warfare"] = 5f;
		this.t.base_stats["diplomacy"] = -2f;
		this.t.base_stats["cities"] = 3f;
		this.add(new ActorTrait
		{
			id = "pacifist",
			path_icon = "ui/Icons/actor_traits/iconPacifist",
			rate_acquire_grow_up = 3,
			rate_inherit = 5,
			likeability = 0.1f,
			group_id = "spirit",
			type = TraitType.Positive
		});
		this.t.addOpposite("bloodlust");
		this.t.base_stats["multiplier_supply_timer"] = -0.1f;
		this.t.base_stats["loyalty_traits"] = 50f;
		this.t.base_stats["diplomacy"] = 10f;
		this.t.base_stats["warfare"] = -4f;
	}

	// Token: 0x06000BB0 RID: 2992 RVA: 0x000A8C64 File Offset: 0x000A6E64
	private void addTraitsAcquired()
	{
		this.add(new ActorTrait
		{
			id = "veteran",
			path_icon = "ui/Icons/actor_traits/iconVeteran",
			group_id = "merits",
			type = TraitType.Positive,
			same_trait_mod = 5,
			is_mutation_box_allowed = false
		});
		this.t.base_stats["skill_combat"] = 0.1f;
		this.t.base_stats["multiplier_damage"] = 0.1f;
		this.t.base_stats["multiplier_health"] = 0.1f;
		this.add(new ActorTrait
		{
			id = "wise",
			path_icon = "ui/Icons/actor_traits/iconWise",
			group_id = "cognitive",
			type = TraitType.Positive,
			is_mutation_box_allowed = false
		});
		this.t.addOpposite("stupid");
		this.t.addOpposite("boat");
		this.t.base_stats["diplomacy"] = 1f;
		this.t.base_stats["stewardship"] = 1f;
		this.t.base_stats["warfare"] = 1f;
		this.t.base_stats["intelligence"] = 1f;
		this.add(new ActorTrait
		{
			id = "infected",
			path_icon = "ui/Icons/actor_traits/iconInfected",
			rate_inherit = 20,
			group_id = "acquired",
			can_be_removed_by_divine_light = true,
			can_be_removed_by_accelerated_healing = true,
			is_mutation_box_allowed = false
		});
		this.t.addOpposite("immune");
		this.t.addOpposite("boat");
		this.t.can_be_cured = true;
		ActorTrait t = this.t;
		t.action_special_effect = (WorldAction)Delegate.Combine(t.action_special_effect, new WorldAction(ActionLibrary.infectedEffect));
		this.t.special_effect_interval = 1.5f;
		ActorTrait t2 = this.t;
		t2.action_death = (WorldAction)Delegate.Combine(t2.action_death, new WorldAction(ActionLibrary.turnIntoZombie));
		this.t.base_stats["multiplier_speed"] = 0.1f;
		this.t.base_stats["loyalty_traits"] = -15f;
		this.add(new ActorTrait
		{
			id = "mush_spores",
			path_icon = "ui/Icons/actor_traits/iconMushSpores",
			rate_inherit = 30,
			can_be_removed_by_divine_light = true,
			can_be_removed_by_accelerated_healing = true,
			group_id = "acquired",
			is_mutation_box_allowed = false
		});
		this.t.addOpposite("immune");
		this.t.addOpposite("boat");
		this.t.can_be_cured = true;
		ActorTrait t3 = this.t;
		t3.action_death = (WorldAction)Delegate.Combine(t3.action_death, new WorldAction(ActionLibrary.mushSporesEffect));
		ActorTrait t4 = this.t;
		t4.action_death = (WorldAction)Delegate.Combine(t4.action_death, new WorldAction(ActionLibrary.turnIntoMush));
		this.t.base_stats["multiplier_speed"] = 0.3f;
		this.t.base_stats["loyalty_traits"] = -15f;
		this.add(new ActorTrait
		{
			id = "tumor_infection",
			path_icon = "ui/Icons/actor_traits/iconTumorInfection",
			rate_inherit = 30,
			can_be_removed_by_divine_light = true,
			can_be_removed_by_accelerated_healing = true,
			group_id = "acquired",
			is_mutation_box_allowed = false
		});
		this.t.addOpposite("immune");
		this.t.addOpposite("boat");
		this.t.can_be_cured = true;
		ActorTrait t5 = this.t;
		t5.action_special_effect = (WorldAction)Delegate.Combine(t5.action_special_effect, new WorldAction(ActionLibrary.tumorEffect));
		ActorTrait t6 = this.t;
		t6.action_death = (WorldAction)Delegate.Combine(t6.action_death, new WorldAction(ActionLibrary.turnIntoTumorMonster));
		this.t.base_stats["multiplier_speed"] = 0.3f;
		this.t.base_stats["loyalty_traits"] = -15f;
		this.add(new ActorTrait
		{
			id = "plague",
			path_icon = "ui/Icons/actor_traits/iconPlague",
			rate_inherit = 30,
			unlocked_with_achievement = true,
			achievement_id = "achievementGreatPlague",
			can_be_removed_by_divine_light = true,
			can_be_removed_by_accelerated_healing = true,
			group_id = "acquired",
			is_mutation_box_allowed = false
		});
		this.t.addOpposite("immune");
		this.t.addOpposite("immortal");
		this.t.addOpposite("contagious");
		this.t.addOpposite("boat");
		this.t.can_be_cured = true;
		ActorTrait t7 = this.t;
		t7.action_special_effect = (WorldAction)Delegate.Combine(t7.action_special_effect, new WorldAction(ActionLibrary.plagueEffect));
		this.t.base_stats["multiplier_speed"] = -0.3f;
		this.t.base_stats["multiplier_damage"] = -0.5f;
		this.t.base_stats["stamina"] = -10f;
		this.t.base_stats["armor"] = -2f;
		this.t.base_stats["loyalty_traits"] = -15f;
		this.t.base_stats["lifespan"] = -30f;
		this.add(new ActorTrait
		{
			id = "blessed",
			likeability = 0.1f,
			path_icon = "ui/Icons/actor_traits/iconBlessing",
			group_id = "acquired",
			is_mutation_box_allowed = false
		});
		this.t.addOpposite("evil");
		this.t.base_stats["multiplier_damage"] = 0.5f;
		this.t.base_stats["multiplier_health"] = 0.5f;
		this.t.base_stats["multiplier_speed"] = 0.5f;
		this.t.base_stats["multiplier_diplomacy"] = 0.2f;
		this.t.base_stats["multiplier_crit"] = 0.1f;
		this.t.base_stats["lifespan"] = 5f;
		this.add(new ActorTrait
		{
			id = "kingslayer",
			path_icon = "ui/Icons/actor_traits/iconKingslayer",
			group_id = "merits",
			is_mutation_box_allowed = false
		});
		this.t.base_stats["multiplier_supply_timer"] = 2f;
		this.t.base_stats["loyalty_traits"] = -25f;
		this.t.base_stats["diplomacy"] = -5f;
		this.t.base_stats["warfare"] = 5f;
		this.add(new ActorTrait
		{
			id = "mageslayer",
			group_id = "merits",
			path_icon = "ui/Icons/actor_traits/iconMageslayer",
			is_mutation_box_allowed = false
		});
		this.t.base_stats["loyalty_traits"] = -10f;
		this.t.base_stats["warfare"] = 5f;
		this.t.base_stats["critical_chance"] = 0.03f;
		this.add(new ActorTrait
		{
			id = "dragonslayer",
			group_id = "merits",
			path_icon = "ui/Icons/actor_traits/iconDragonslayer",
			is_mutation_box_allowed = false
		});
		this.t.base_stats["warfare"] = 5f;
		this.t.base_stats["critical_chance"] = 0.04f;
		this.t.base_stats["multiplier_diplomacy"] = 0.1f;
		this.add(new ActorTrait
		{
			id = "crippled",
			path_icon = "ui/Icons/actor_traits/iconCrippled",
			same_trait_mod = 10,
			can_be_removed_by_divine_light = true,
			can_be_removed_by_accelerated_healing = true,
			type = TraitType.Negative,
			group_id = "acquired",
			is_mutation_box_allowed = false
		});
		this.t.base_stats["multiplier_speed"] = -0.5f;
		this.t.base_stats["diplomacy"] = -3f;
		this.t.base_stats["multiplier_offspring"] = -0.5f;
		this.add(new ActorTrait
		{
			id = "golden_tooth",
			path_icon = "ui/Icons/actor_traits/iconGoldenTooth",
			same_trait_mod = 5,
			type = TraitType.Positive,
			group_id = "appearance",
			is_mutation_box_allowed = false
		});
		this.t.base_stats["diplomacy"] = 2f;
		this.add(new ActorTrait
		{
			id = "eyepatch",
			path_icon = "ui/Icons/actor_traits/iconEyePatch",
			same_trait_mod = 20,
			can_be_removed_by_divine_light = true,
			can_be_removed_by_accelerated_healing = true,
			type = TraitType.Negative,
			group_id = "appearance",
			is_mutation_box_allowed = false
		});
		this.t.base_stats["accuracy"] = -5f;
		this.t.base_stats["diplomacy"] = 1f;
		this.t.base_stats["warfare"] = -1f;
		this.t.base_stats["critical_chance"] = -0.15f;
		this.add(new ActorTrait
		{
			id = "skin_burns",
			path_icon = "ui/Icons/actor_traits/iconSkinBurns",
			same_trait_mod = 40,
			can_be_removed_by_divine_light = true,
			can_be_removed_by_accelerated_healing = true,
			type = TraitType.Negative,
			group_id = "appearance",
			is_mutation_box_allowed = false
		});
		this.t.base_stats["diplomacy"] = -2f;
		this.t.base_stats["warfare"] = 2f;
		this.t.base_stats["multiplier_speed"] = -0.25f;
		this.t.base_stats["lifespan"] = -5f;
	}

	// Token: 0x06000BB1 RID: 2993 RVA: 0x000A9720 File Offset: 0x000A7920
	private void addTraitsFun()
	{
		this.add(new ActorTrait
		{
			id = "super_health",
			path_icon = "ui/Icons/actor_traits/iconSuperHealth",
			unlocked_with_achievement = true,
			achievement_id = "achievementTraitsExplorer90",
			group_id = "health",
			rate_inherit = 3
		});
		this.t.base_stats["lifespan"] = 100f;
		this.t.base_stats["multiplier_health"] = 100f;
		this.add(new ActorTrait
		{
			id = "death_nuke",
			path_icon = "ui/Icons/actor_traits/iconDeathNuke",
			unlocked_with_achievement = true,
			achievement_id = "achievementFinalResolution",
			group_id = "fun",
			rate_inherit = 1,
			is_mutation_box_allowed = false
		});
		this.t.addOpposite("death_bomb");
		ActorTrait t = this.t;
		t.action_death = (WorldAction)Delegate.Combine(t.action_death, new WorldAction(ActionLibrary.deathNuke));
		this.add(new ActorTrait
		{
			id = "death_bomb",
			path_icon = "ui/Icons/actor_traits/iconDeathBomb",
			unlocked_with_achievement = true,
			achievement_id = "achievementManyBombs",
			group_id = "fun",
			rate_inherit = 1
		});
		this.t.addOpposite("death_nuke");
		ActorTrait t2 = this.t;
		t2.action_death = (WorldAction)Delegate.Combine(t2.action_death, new WorldAction(ActionLibrary.deathBomb));
		this.add(new ActorTrait
		{
			id = "death_mark",
			path_icon = "ui/Icons/actor_traits/iconDeathMark",
			unlocked_with_achievement = true,
			achievement_id = "achievementTraitsExplorer40",
			group_id = "fate",
			is_mutation_box_allowed = false
		});
		ActorTrait t3 = this.t;
		t3.action_special_effect = (WorldAction)Delegate.Combine(t3.action_special_effect, new WorldAction(ActionLibrary.deathMark));
		this.add(new ActorTrait
		{
			id = "energized",
			path_icon = "ui/Icons/actor_traits/iconLightning",
			group_id = "fun",
			spawn_random_trait_allowed = false
		});
		this.t.addOpposite("boat");
		this.t.base_stats["multiplier_speed"] = 2f;
		this.t.base_stats["lifespan"] = 7f;
		ActorTrait t4 = this.t;
		t4.action_death = (WorldAction)Delegate.Combine(t4.action_death, new WorldAction(ActionLibrary.energizedLightning));
		this.add(new ActorTrait
		{
			id = "mega_heartbeat",
			path_icon = "ui/Icons/actor_traits/iconMegaHeartbeat",
			group_id = "fun",
			rate_inherit = 4,
			unlocked_with_achievement = true,
			achievement_id = "achievementPrintHeart",
			special_effect_interval = 5f,
			likeability = 0.1f,
			spawn_random_trait_allowed = false,
			is_mutation_box_allowed = false
		});
		this.t.addOpposite("whirlwind");
		ActorTrait t5 = this.t;
		t5.action_special_effect = (WorldAction)Delegate.Combine(t5.action_special_effect, new WorldAction(ActionLibrary.megaHeartbeat));
		this.add(new ActorTrait
		{
			id = "bomberman",
			path_icon = "ui/Icons/actor_traits/iconGrenade",
			group_id = "fun"
		});
		this.t.addCombatAction("combat_throw_bomb");
		this.add(new ActorTrait
		{
			id = "pyromaniac",
			path_icon = "ui/Icons/actor_traits/iconPyromaniac",
			rate_acquire_grow_up = 1,
			achievement_id = "achievementWorldWar",
			unlocked_with_achievement = true,
			group_id = "mind",
			acquire_grow_up_sapient_only = true,
			rate_inherit = 1
		});
		this.t.addCombatAction("combat_throw_torch");
		this.add(new ActorTrait
		{
			id = "whirlwind",
			path_icon = "ui/Icons/iconTornado",
			group_id = "fun",
			action_special_effect = new WorldAction(ActionLibrary.whirlwind),
			special_effect_interval = 0.1f,
			unlocked_with_achievement = true,
			spawn_random_trait_allowed = false,
			is_mutation_box_allowed = false,
			achievement_id = "achievementRainTornado"
		});
		this.t.addOpposite("mega_heartbeat");
	}

	// Token: 0x06000BB2 RID: 2994 RVA: 0x000A9B70 File Offset: 0x000A7D70
	private void addTraitsMisc()
	{
		this.add(new ActorTrait
		{
			id = "light_lamp",
			path_icon = "ui/Icons/actor_traits/iconLightLamp",
			group_id = "miscellaneous"
		});
		this.t.base_stats.addTag("generate_light");
		this.add(new ActorTrait
		{
			id = "shiny",
			path_icon = "ui/Icons/actor_traits/iconShiny",
			group_id = "miscellaneous",
			rate_inherit = 10
		});
		this.t.base_stats["diplomacy"] = 5f;
		this.t.action_special_effect = new WorldAction(ActionLibrary.shiny);
		this.add(new ActorTrait
		{
			id = "flower_prints",
			path_icon = "ui/Icons/actor_traits/iconFlowerPrints",
			unlocked_with_achievement = true,
			achievement_id = "achievementTouchTheGrass",
			group_id = "miscellaneous",
			rate_inherit = 10
		});
		ActorTrait t = this.t;
		t.action_special_effect = (WorldAction)Delegate.Combine(t.action_special_effect, new WorldAction(ActionLibrary.flowerPrintsEffect));
	}

	// Token: 0x06000BB3 RID: 2995 RVA: 0x000A9C94 File Offset: 0x000A7E94
	private void addTraitsSpecial()
	{
		this.add(new ActorTrait
		{
			id = "metamorphed",
			rarity = Rarity.R0_Normal,
			path_icon = "ui/Icons/actor_traits/iconMetamorphed",
			can_be_given = false,
			group_id = "special",
			is_mutation_box_allowed = false
		});
		this.add(new ActorTrait
		{
			id = "clone",
			rarity = Rarity.R0_Normal,
			path_icon = "ui/Icons/actor_traits/iconClone",
			can_be_given = false,
			group_id = "special",
			is_mutation_box_allowed = false
		});
		this.add(new ActorTrait
		{
			id = "boat",
			path_icon = "ui/Icons/iconBoat",
			can_be_given = false,
			group_id = "special",
			is_mutation_box_allowed = false
		});
		this.t.addOpposite("infected");
		this.t.addOpposite("tumor_infection");
		this.t.addOpposite("mush_spores");
		this.t.addOpposite("plague");
		this.t.addOpposite("immortal");
		this.t.addOpposite("energized");
		this.t.addOpposite("wise");
		this.add(new ActorTrait
		{
			id = "scar_of_divinity",
			path_icon = "ui/Icons/actor_traits/iconDivineScar",
			can_be_removed = false,
			can_be_given = false,
			rate_inherit = 0,
			group_id = "special",
			is_mutation_box_allowed = false
		});
		this.add(new ActorTrait
		{
			id = "miracle_born",
			path_icon = "ui/Icons/actor_traits/iconMiracleBorn",
			group_id = "special",
			can_be_given = false,
			can_be_removed = false,
			is_mutation_box_allowed = false
		});
		this.t.base_stats["lifespan"] = 20f;
		this.t.base_stats["multiplier_offspring"] = 2f;
		this.t.base_stats["birth_rate"] = 2f;
		this.add(new ActorTrait
		{
			id = "miracle_bearer",
			path_icon = "ui/Icons/actor_traits/iconMiracleBearer",
			group_id = "special",
			can_be_given = false,
			can_be_removed = false,
			is_mutation_box_allowed = false
		});
		this.add(new ActorTrait
		{
			id = "contagious",
			path_icon = "ui/Icons/iconRat",
			group_id = "miscellaneous",
			is_mutation_box_allowed = false
		});
		this.t.addOpposite("plague");
		ActorTrait t = this.t;
		t.action_special_effect = (WorldAction)Delegate.Combine(t.action_special_effect, new WorldAction(ActionLibrary.contagiousEffect));
		this.add(new ActorTrait
		{
			id = "zombie",
			path_icon = "ui/Icons/iconZombie",
			can_be_given = false,
			group_id = "special",
			is_mutation_box_allowed = false
		});
		this.t.action_special_effect = new WorldAction(ActionLibrary.zombieEffect);
		this.t.action_attack_target = new AttackAction(ActionLibrary.zombieInfectAttack);
		this.add(new ActorTrait
		{
			id = "madness",
			path_icon = "ui/Icons/actor_traits/iconMadness",
			group_id = "special",
			can_be_removed_by_divine_light = true,
			can_be_given = false,
			can_be_removed = false,
			is_kingdom_affected = true,
			affects_mind = true,
			forced_kingdom = "mad",
			likeability = -1f,
			is_mutation_box_allowed = false
		});
		ActorTrait t2 = this.t;
		t2.action_on_augmentation_add = (WorldActionTrait)Delegate.Combine(t2.action_on_augmentation_add, new WorldActionTrait(ActionLibrary.forcedKingdomAdd));
		ActorTrait t3 = this.t;
		t3.action_on_augmentation_remove = (WorldActionTrait)Delegate.Combine(t3.action_on_augmentation_remove, new WorldActionTrait(ActionLibrary.forcedKingdomEffectRemove));
		ActorTrait t4 = this.t;
		t4.action_on_augmentation_load = (WorldActionTrait)Delegate.Combine(t4.action_on_augmentation_load, new WorldActionTrait(ActionLibrary.madnessEffectLoad));
		this.t.traits_to_remove_ids = new string[]
		{
			"desire_alien_mold",
			"desire_computer",
			"desire_golden_egg",
			"desire_harp"
		};
		this.t.addOpposite("strong_minded");
		this.t.addOpposite("desire_alien_mold");
		this.t.addOpposite("desire_computer");
		this.t.addOpposite("desire_golden_egg");
		this.t.addOpposite("desire_harp");
		this.t.addDecision("madness_random_emotion");
		this.t.base_stats["multiplier_speed"] = 0.1f;
		this.t.base_stats["diplomacy"] = -100f;
		this.t.base_stats["loyalty_traits"] = -100f;
		this.add(new ActorTrait
		{
			id = "desire_alien_mold",
			path_icon = "ui/Icons/actor_traits/iconWaypointAlienMoldDrop",
			group_id = "special",
			can_be_removed_by_divine_light = true,
			can_be_given = false,
			can_be_removed = false,
			is_kingdom_affected = true,
			affects_mind = true,
			forced_kingdom = "alien_mold",
			is_mutation_box_allowed = false
		});
		ActorTrait t5 = this.t;
		t5.action_on_augmentation_add = (WorldActionTrait)Delegate.Combine(t5.action_on_augmentation_add, new WorldActionTrait(ActionLibrary.forcedKingdomAdd));
		ActorTrait t6 = this.t;
		t6.action_on_augmentation_remove = (WorldActionTrait)Delegate.Combine(t6.action_on_augmentation_remove, new WorldActionTrait(ActionLibrary.forcedKingdomEffectRemove));
		ActorTrait t7 = this.t;
		t7.action_on_augmentation_load = (WorldActionTrait)Delegate.Combine(t7.action_on_augmentation_load, new WorldActionTrait(ActionLibrary.madnessEffectLoad));
		this.t.addDecision("follow_desire_target");
		this.t.traits_to_remove_ids = new string[]
		{
			"desire_computer",
			"desire_golden_egg",
			"desire_harp",
			"madness"
		};
		this.t.addOpposite("madness");
		this.t.addOpposite("desire_computer");
		this.t.addOpposite("desire_golden_egg");
		this.t.addOpposite("desire_harp");
		this.t.base_stats["targets"] = 1f;
		this.t.base_stats["multiplier_crit"] = 0.3f;
		this.t.base_stats["damage_range"] = 0.3f;
		this.t.base_stats["armor"] = 10f;
		this.add(new ActorTrait
		{
			id = "desire_computer",
			path_icon = "ui/Icons/actor_traits/iconWaypointComputerDrop",
			group_id = "special",
			can_be_removed_by_divine_light = true,
			can_be_given = false,
			can_be_removed = false,
			is_kingdom_affected = true,
			affects_mind = true,
			forced_kingdom = "computer",
			is_mutation_box_allowed = false
		});
		ActorTrait t8 = this.t;
		t8.action_on_augmentation_add = (WorldActionTrait)Delegate.Combine(t8.action_on_augmentation_add, new WorldActionTrait(ActionLibrary.forcedKingdomAdd));
		ActorTrait t9 = this.t;
		t9.action_on_augmentation_remove = (WorldActionTrait)Delegate.Combine(t9.action_on_augmentation_remove, new WorldActionTrait(ActionLibrary.forcedKingdomEffectRemove));
		ActorTrait t10 = this.t;
		t10.action_on_augmentation_load = (WorldActionTrait)Delegate.Combine(t10.action_on_augmentation_load, new WorldActionTrait(ActionLibrary.madnessEffectLoad));
		this.t.addDecision("follow_desire_target");
		this.t.traits_to_remove_ids = new string[]
		{
			"desire_alien_mold",
			"desire_golden_egg",
			"desire_harp",
			"madness"
		};
		this.t.addOpposite("madness");
		this.t.addOpposite("desire_alien_mold");
		this.t.addOpposite("desire_golden_egg");
		this.t.addOpposite("desire_harp");
		this.t.base_stats["multiplier_health"] = 0.3f;
		this.t.base_stats["multiplier_lifespan"] = 0.5f;
		this.add(new ActorTrait
		{
			id = "desire_golden_egg",
			path_icon = "ui/Icons/actor_traits/iconWaypointGoldenEggDrop",
			group_id = "special",
			can_be_removed_by_divine_light = true,
			can_be_given = false,
			can_be_removed = false,
			is_kingdom_affected = true,
			affects_mind = true,
			forced_kingdom = "golden_egg",
			is_mutation_box_allowed = false
		});
		ActorTrait t11 = this.t;
		t11.action_on_augmentation_add = (WorldActionTrait)Delegate.Combine(t11.action_on_augmentation_add, new WorldActionTrait(ActionLibrary.forcedKingdomAdd));
		ActorTrait t12 = this.t;
		t12.action_on_augmentation_remove = (WorldActionTrait)Delegate.Combine(t12.action_on_augmentation_remove, new WorldActionTrait(ActionLibrary.forcedKingdomEffectRemove));
		ActorTrait t13 = this.t;
		t13.action_on_augmentation_load = (WorldActionTrait)Delegate.Combine(t13.action_on_augmentation_load, new WorldActionTrait(ActionLibrary.madnessEffectLoad));
		this.t.addDecision("follow_desire_target");
		this.t.traits_to_remove_ids = new string[]
		{
			"desire_alien_mold",
			"desire_computer",
			"desire_harp",
			"madness"
		};
		this.t.addOpposite("strong_minded");
		this.t.addOpposite("madness");
		this.t.addOpposite("desire_alien_mold");
		this.t.addOpposite("desire_computer");
		this.t.addOpposite("desire_harp");
		this.t.base_stats["multiplier_damage"] = 0.5f;
		this.add(new ActorTrait
		{
			id = "desire_harp",
			path_icon = "ui/Icons/actor_traits/iconWaypointHarpDrop",
			group_id = "special",
			can_be_removed_by_divine_light = true,
			can_be_given = false,
			can_be_removed = false,
			is_kingdom_affected = true,
			affects_mind = true,
			forced_kingdom = "harp",
			is_mutation_box_allowed = false
		});
		ActorTrait t14 = this.t;
		t14.action_on_augmentation_add = (WorldActionTrait)Delegate.Combine(t14.action_on_augmentation_add, new WorldActionTrait(ActionLibrary.forcedKingdomAdd));
		ActorTrait t15 = this.t;
		t15.action_on_augmentation_remove = (WorldActionTrait)Delegate.Combine(t15.action_on_augmentation_remove, new WorldActionTrait(ActionLibrary.forcedKingdomEffectRemove));
		ActorTrait t16 = this.t;
		t16.action_on_augmentation_load = (WorldActionTrait)Delegate.Combine(t16.action_on_augmentation_load, new WorldActionTrait(ActionLibrary.madnessEffectLoad));
		this.t.addDecision("follow_desire_target");
		this.t.traits_to_remove_ids = new string[]
		{
			"desire_alien_mold",
			"desire_computer",
			"desire_golden_egg",
			"madness"
		};
		this.t.addOpposite("strong_minded");
		this.t.addOpposite("madness");
		this.t.addOpposite("desire_alien_mold");
		this.t.addOpposite("desire_computer");
		this.t.addOpposite("desire_golden_egg");
		this.t.base_stats["multiplier_speed"] = 0.3f;
		this.t.base_stats["multiplier_attack_speed"] = 0.3f;
	}

	// Token: 0x06000BB4 RID: 2996 RVA: 0x000AA7EC File Offset: 0x000A89EC
	public override void post_init()
	{
		base.post_init();
		foreach (ActorTrait tTrait in this.list)
		{
			if (tTrait.base_stats["health"] > 0f || tTrait.base_stats["mana"] > 0f || tTrait.base_stats["stamina"] > 0f || tTrait.base_stats["multiplier_health"] > 0f || tTrait.base_stats["multiplier_mana"] > 0f || tTrait.base_stats["multiplier_stamina"] > 0f)
			{
				ActorTrait actorTrait = tTrait;
				actorTrait.action_on_augmentation_add = (WorldActionTrait)Delegate.Combine(actorTrait.action_on_augmentation_add, new WorldActionTrait(ActionLibrary.restoreFullStats));
			}
			if (tTrait.base_stats["health"] < 0f || tTrait.base_stats["mana"] < 0f || tTrait.base_stats["stamina"] < 0f || tTrait.base_stats["multiplier_health"] < 0f || tTrait.base_stats["multiplier_mana"] < 0f || tTrait.base_stats["multiplier_stamina"] < 0f)
			{
				ActorTrait actorTrait2 = tTrait;
				actorTrait2.action_on_augmentation_remove = (WorldActionTrait)Delegate.Combine(actorTrait2.action_on_augmentation_remove, new WorldActionTrait(ActionLibrary.restoreFullStats));
			}
		}
	}

	// Token: 0x06000BB5 RID: 2997 RVA: 0x000AA9B4 File Offset: 0x000A8BB4
	public override ActorTrait add(ActorTrait pAsset)
	{
		base.add(pAsset);
		this.checkDefault(pAsset);
		return pAsset;
	}

	// Token: 0x06000BB6 RID: 2998 RVA: 0x000AA9C8 File Offset: 0x000A8BC8
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (ActorTrait tTrait in this.list)
		{
			if (tTrait.is_mutation_box_allowed)
			{
				this.pot_traits_mutation_box.Add(tTrait);
			}
		}
		foreach (ActorTrait tTrait2 in this.list)
		{
			if (tTrait2.rate_birth != 0)
			{
				for (int i = 0; i < tTrait2.rate_birth; i++)
				{
					this.pot_traits_birth.Add(tTrait2);
				}
			}
		}
		foreach (ActorTrait tTrait3 in this.list)
		{
			if (tTrait3.rate_acquire_grow_up != 0)
			{
				for (int j = 0; j < tTrait3.rate_acquire_grow_up; j++)
				{
					this.pot_traits_growup.Add(tTrait3);
				}
			}
		}
		foreach (ActorTrait tTrait4 in this.list)
		{
			if (tTrait4.in_training_dummy_combat_pot)
			{
				this.pot_traits_combat.Add(tTrait4);
			}
		}
	}

	// Token: 0x06000BB7 RID: 2999 RVA: 0x000AAB48 File Offset: 0x000A8D48
	private void checkDefault(ActorTrait pAsset)
	{
		if (pAsset.rate_inherit == 0)
		{
			pAsset.rate_inherit = pAsset.rate_birth * 10;
		}
	}

	// Token: 0x06000BB8 RID: 3000 RVA: 0x000AAB64 File Offset: 0x000A8D64
	public int checkTraitsMod(Actor pMain, Actor pTarget)
	{
		int tResult = 0;
		foreach (ActorTrait tTrait in pMain.getTraits())
		{
			if (tTrait.same_trait_mod != 0 && pTarget.hasTrait(tTrait))
			{
				tResult += tTrait.same_trait_mod;
			}
			if (tTrait.opposite_trait_mod != 0)
			{
				foreach (ActorTrait tOppositeTrait in tTrait.opposite_traits)
				{
					if (pTarget.hasTrait(tOppositeTrait))
					{
						tResult += tTrait.opposite_trait_mod;
					}
				}
			}
		}
		return tResult;
	}

	// Token: 0x04000B3D RID: 2877
	public const int COMBAT_SKILLS_AMOUNT = 5;

	// Token: 0x04000B3E RID: 2878
	[NonSerialized]
	public List<ActorTrait> pot_traits_mutation_box = new List<ActorTrait>();

	// Token: 0x04000B3F RID: 2879
	[NonSerialized]
	public List<ActorTrait> pot_traits_birth = new List<ActorTrait>();

	// Token: 0x04000B40 RID: 2880
	[NonSerialized]
	public List<ActorTrait> pot_traits_growup = new List<ActorTrait>();

	// Token: 0x04000B41 RID: 2881
	[NonSerialized]
	public List<ActorTrait> pot_traits_combat = new List<ActorTrait>();
}
