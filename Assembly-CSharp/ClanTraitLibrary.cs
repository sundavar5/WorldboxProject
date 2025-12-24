using System;
using System.Collections.Generic;

// Token: 0x02000193 RID: 403
public class ClanTraitLibrary : BaseTraitLibrary<ClanTrait>
{
	// Token: 0x06000BF2 RID: 3058 RVA: 0x000ABFAE File Offset: 0x000AA1AE
	protected override List<string> getDefaultTraitsForMeta(ActorAsset pAsset)
	{
		return pAsset.default_clan_traits;
	}

	// Token: 0x06000BF3 RID: 3059 RVA: 0x000ABFB8 File Offset: 0x000AA1B8
	public override void init()
	{
		base.init();
		this.add(new ClanTrait
		{
			id = "mark_of_becoming",
			group_id = "special",
			can_be_given = false,
			can_be_removed = false,
			spawn_random_trait_allowed = false
		});
		this.add(new ClanTrait
		{
			id = "blood_pact",
			group_id = "spirit"
		});
		this.t.base_stats["warfare"] = 1f;
		this.t.addDecision("banish_unruly_clan_members");
		this.t.addOpposite("deathbound");
		this.add(new ClanTrait
		{
			id = "deathbound",
			group_id = "spirit"
		});
		this.t.base_stats["warfare"] = 5f;
		this.t.addDecision("kill_unruly_clan_members");
		this.t.addOpposite("blood_pact");
		this.add(new ClanTrait
		{
			id = "bonebreakers",
			group_id = "body"
		});
		this.t.setUnlockedWithAchievement("achievementSegregator");
		this.t.base_stats["damage"] = 5f;
		ClanTrait t = this.t;
		t.action_attack_target = (AttackAction)Delegate.Combine(t.action_attack_target, new AttackAction(ActionLibrary.breakBones));
		this.add(new ClanTrait
		{
			id = "stonefists",
			group_id = "body"
		});
		this.t.base_stats["damage"] = 30f;
		this.add(new ClanTrait
		{
			id = "blood_of_sea",
			group_id = "body"
		});
		this.t.base_stats["stamina"] = 20f;
		this.t.base_stats.addTag("fast_swimming");
		this.add(new ClanTrait
		{
			id = "gaia_shield",
			group_id = "body"
		});
		this.t.base_stats["armor"] = 10f;
		this.t.base_stats["multiplier_health"] = 0.1f;
		this.t.base_stats.addTag("immunity_fire");
		this.t.base_stats.addTag("immunity_cold");
		this.add(new ClanTrait
		{
			id = "iron_will",
			group_id = "mind"
		});
		this.t.base_stats["intelligence"] = 5f;
		this.t.base_stats.addTag("strong_mind");
		this.add(new ClanTrait
		{
			id = "flesh_weavers",
			group_id = "body",
			special_effect_interval = 2f
		});
		this.t.base_stats["multiplier_health"] = 0.2f;
		ClanTrait t2 = this.t;
		t2.action_special_effect = (WorldAction)Delegate.Combine(t2.action_special_effect, new WorldAction(ActionLibrary.regenerationEffectClan));
		this.add(new ClanTrait
		{
			id = "endurance_of_titans",
			group_id = "body"
		});
		this.t.base_stats["multiplier_stamina"] = 3f;
		this.add(new ClanTrait
		{
			id = "combat_instincts",
			group_id = "mind"
		});
		this.t.setUnlockedWithAchievement("achievementMasterOfCombat");
		this.t.base_stats["warfare"] = 10f;
		this.t.addCombatAction("combat_dash");
		this.t.addCombatAction("combat_block");
		this.t.addCombatAction("combat_dodge");
		this.t.addCombatAction("combat_backstep");
		this.t.addCombatAction("combat_deflect_projectile");
		this.add(new ClanTrait
		{
			id = "void_ban",
			group_id = "chaos",
			spawn_random_trait_allowed = false
		});
		this.t.base_stats["multiplier_mana"] = -10f;
		this.add(new ClanTrait
		{
			id = "warlocks_vein",
			group_id = "spirit"
		});
		this.t.base_stats_male["multiplier_mana"] = 2f;
		this.add(new ClanTrait
		{
			id = "witchs_vein",
			group_id = "spirit"
		});
		this.t.base_stats_female["multiplier_mana"] = 2f;
		this.add(new ClanTrait
		{
			id = "magic_blood",
			group_id = "spirit"
		});
		this.t.setUnlockedWithAchievement("achievementTheAccomplished");
		this.t.base_stats["multiplier_mana"] = 3f;
		this.add(new ClanTrait
		{
			id = "blood_of_eons",
			group_id = "body",
			spawn_random_trait_allowed = false
		});
		this.t.addOpposite("cursed_blood");
		this.t.base_stats["lifespan"] = 1E+09f;
		this.add(new ClanTrait
		{
			id = "blood_of_giants",
			group_id = "body"
		});
		this.t.base_stats["scale"] = 0.05f;
		this.add(new ClanTrait
		{
			id = "silver_tongues",
			group_id = "mind"
		});
		this.t.base_stats["opinion"] = 20f;
		this.t.base_stats["diplomacy"] = 5f;
		this.add(new ClanTrait
		{
			id = "masters_of_propaganda",
			group_id = "mind"
		});
		this.t.base_stats["loyalty_traits"] = 20f;
		this.add(new ClanTrait
		{
			id = "gods_chosen",
			group_id = "spirit"
		});
		this.t.base_stats["stewardship"] = 10f;
		this.t.base_stats["diplomacy"] = 5f;
		this.t.base_stats["armor"] = 20f;
		this.add(new ClanTrait
		{
			id = "cursed_blood",
			group_id = "chaos",
			spawn_random_trait_allowed = false
		});
		this.t.setUnlockedWithAchievement("achievementTheBroken");
		this.t.base_stats["lifespan"] = -666f;
		this.t.addOpposite("blood_of_eons");
		this.add(new ClanTrait
		{
			id = "divine_dozen",
			group_id = "harmony"
		});
		this.t.addOpposite("we_are_legion");
		this.t.addOpposite("best_five");
		this.t.base_stats_meta["limit_clan_members"] = 12f;
		this.add(new ClanTrait
		{
			id = "best_five",
			group_id = "harmony"
		});
		this.t.addOpposite("we_are_legion");
		this.t.addOpposite("divine_dozen");
		this.t.base_stats_meta["limit_clan_members"] = 5f;
		this.add(new ClanTrait
		{
			id = "we_are_legion",
			group_id = "harmony"
		});
		this.t.setUnlockedWithAchievement("achievementMegapolis");
		this.t.addOpposite("best_five");
		this.t.addOpposite("divine_dozen");
		this.t.base_stats_meta["limit_clan_members"] = 1000f;
		ClanTrait clanTrait = new ClanTrait();
		clanTrait.id = "nitroglycerin_blood";
		clanTrait.group_id = "chaos";
		clanTrait.action_death = delegate(BaseSimObject _, WorldTile pTile)
		{
			DropsLibrary.action_grenade(pTile, null);
			return true;
		};
		this.add(clanTrait);
		this.t.setUnlockedWithAchievement("achievementMinefield");
		this.t.base_stats["health"] = -1f;
		ClanTrait clanTrait2 = new ClanTrait();
		clanTrait2.id = "antimatter_blood";
		clanTrait2.group_id = "chaos";
		clanTrait2.spawn_random_trait_allowed = false;
		clanTrait2.action_death = delegate(BaseSimObject _, WorldTile pTile)
		{
			DropsLibrary.action_antimatter_bomb(pTile, null);
			return true;
		};
		this.add(clanTrait2);
		this.t.setUnlockedWithAchievement("achievementTraitExplorerClan");
		this.t.base_stats["damage"] = 1f;
		ClanTrait clanTrait3 = new ClanTrait();
		clanTrait3.id = "gaia_blood";
		clanTrait3.group_id = "spirit";
		clanTrait3.action_death = delegate(BaseSimObject _, WorldTile pTile)
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
		this.add(clanTrait3);
		this.t.setUnlockedWithAchievement("achievementThePrincess");
		this.t.base_stats["multiplier_health"] = 0.05f;
		this.add(new ClanTrait
		{
			id = "grin_mark",
			group_id = "fate",
			spawn_random_trait_allowed = false,
			priority = -100
		});
		this.t.setTraitInfoToGrinMark();
		this.t.setUnlockedWithAchievement("achievementCreaturesExplorer");
		this.add(new ClanTrait
		{
			id = "geb",
			group_id = "special",
			can_be_given = false,
			can_be_removed = false,
			spawn_random_trait_allowed = false
		});
	}

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x000AC9CD File Offset: 0x000AABCD
	protected override string icon_path
	{
		get
		{
			return "ui/Icons/clan_traits/";
		}
	}
}
