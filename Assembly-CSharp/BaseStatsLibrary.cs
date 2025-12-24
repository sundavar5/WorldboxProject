using System;

// Token: 0x02000219 RID: 537
public class BaseStatsLibrary : AssetLibrary<BaseStatAsset>
{
	// Token: 0x0600134A RID: 4938 RVA: 0x000D76C4 File Offset: 0x000D58C4
	public override void init()
	{
		base.init();
		this.add(new BaseStatAsset
		{
			id = "personality_aggression",
			hidden = true,
			normalize = true,
			normalize_min = 0f,
			normalize_max = 1f,
			used_only_for_civs = true
		});
		this.add(new BaseStatAsset
		{
			id = "personality_administration",
			hidden = true,
			normalize = true,
			normalize_min = 0f,
			normalize_max = 1f,
			used_only_for_civs = true
		});
		this.add(new BaseStatAsset
		{
			id = "personality_diplomatic",
			hidden = true,
			normalize = true,
			normalize_min = 0f,
			normalize_max = 1f,
			used_only_for_civs = true
		});
		this.add(new BaseStatAsset
		{
			id = "personality_rationality",
			hidden = true,
			normalize = true,
			normalize_min = 0f,
			normalize_max = 1f,
			used_only_for_civs = true
		});
		this.add(new BaseStatAsset
		{
			id = "diplomacy",
			actor_data_attribute = true,
			normalize = true,
			normalize_max = 999f,
			used_only_for_civs = true,
			sort_rank = 900
		});
		this.add(new BaseStatAsset
		{
			id = "warfare",
			actor_data_attribute = true,
			normalize = true,
			normalize_max = 999f,
			used_only_for_civs = true,
			sort_rank = 900
		});
		this.add(new BaseStatAsset
		{
			id = "stewardship",
			actor_data_attribute = true,
			normalize = true,
			normalize_max = 999f,
			used_only_for_civs = true,
			sort_rank = 900
		});
		this.add(new BaseStatAsset
		{
			id = "intelligence",
			actor_data_attribute = true,
			normalize = true,
			normalize_max = 999f,
			used_only_for_civs = true,
			sort_rank = 900
		});
		this.add(new BaseStatAsset
		{
			id = "lifespan",
			sort_rank = 997,
			normalize = true,
			normalize_min = 1f
		});
		this.add(new BaseStatAsset
		{
			id = "mutation",
			sort_rank = 996
		});
		this.add(new BaseStatAsset
		{
			id = "offspring",
			normalize = true,
			normalize_min = 0f,
			normalize_max = 1000f
		});
		this.add(new BaseStatAsset
		{
			id = "multiplier_offspring",
			show_as_percents = true,
			multiplier = true,
			main_stat_to_multiply = "offspring",
			tooltip_multiply_for_visual_number = 100f,
			translation_key = "offspring"
		});
		this.add(new BaseStatAsset
		{
			id = "army",
			normalize = true,
			normalize_min = 0.1f,
			used_only_for_civs = true
		});
		this.add(new BaseStatAsset
		{
			id = "cities",
			used_only_for_civs = true
		});
		this.add(new BaseStatAsset
		{
			id = "range"
		});
		this.add(new BaseStatAsset
		{
			id = "bonus_towers",
			normalize = true,
			normalize_max = 2f,
			used_only_for_civs = true
		});
		this.add(new BaseStatAsset
		{
			id = "damage",
			normalize = true,
			normalize_min = 1f,
			sort_rank = 999
		});
		this.add(new BaseStatAsset
		{
			id = "speed",
			normalize = true,
			normalize_min = 1f,
			sort_rank = 998
		});
		this.add(new BaseStatAsset
		{
			id = "health",
			normalize = true,
			normalize_min = 1f,
			sort_rank = 1000
		});
		this.add(new BaseStatAsset
		{
			id = "armor",
			normalize = true,
			normalize_min = 0f,
			normalize_max = 99f
		});
		this.add(new BaseStatAsset
		{
			id = "stamina",
			normalize = true,
			normalize_min = 1f,
			sort_rank = 1000
		});
		this.add(new BaseStatAsset
		{
			id = "mana",
			normalize = true,
			normalize_min = 0f,
			sort_rank = 1000
		});
		this.add(new BaseStatAsset
		{
			id = "accuracy",
			normalize_min = 1f,
			normalize_max = 10f,
			normalize = true
		});
		this.add(new BaseStatAsset
		{
			id = "targets",
			hidden = true
		});
		this.add(new BaseStatAsset
		{
			id = "projectiles",
			normalize = true,
			normalize_min = 1f,
			hidden = true
		});
		this.add(new BaseStatAsset
		{
			id = "experience"
		});
		this.add(new BaseStatAsset
		{
			id = "happiness",
			normalize = true,
			normalize_min = 0f
		});
		this.add(new BaseStatAsset
		{
			id = "critical_chance",
			normalize = true,
			normalize_min = 0f,
			show_as_percents = true,
			tooltip_multiply_for_visual_number = 100f
		});
		this.add(new BaseStatAsset
		{
			id = "critical_damage_multiplier",
			show_as_percents = true,
			tooltip_multiply_for_visual_number = 100f
		});
		this.add(new BaseStatAsset
		{
			id = "size",
			hidden = true
		});
		this.add(new BaseStatAsset
		{
			id = "area_of_effect",
			hidden = true
		});
		this.add(new BaseStatAsset
		{
			id = "attack_speed",
			normalize = true,
			normalize_min = 0.5f,
			normalize_max = 10f
		});
		this.add(new BaseStatAsset
		{
			id = "throwing_range",
			normalize = true,
			normalize_min = 1f,
			normalize_max = 100f
		});
		this.add(new BaseStatAsset
		{
			id = "construction_speed",
			normalize = true,
			normalize_min = 1f,
			normalize_max = 100f
		});
		this.add(new BaseStatAsset
		{
			id = "loyalty_traits",
			translation_key = "loyalty",
			used_only_for_civs = true
		});
		this.add(new BaseStatAsset
		{
			id = "birth_rate",
			translation_key = "birth_rate"
		});
		this.add(new BaseStatAsset
		{
			id = "maturation",
			translation_key = "maturation"
		});
		this.add(new BaseStatAsset
		{
			id = "age_adult",
			translation_key = "age_adult",
			hidden = true
		});
		this.add(new BaseStatAsset
		{
			id = "age_breeding",
			translation_key = "age_breeding",
			hidden = true
		});
		this.add(new BaseStatAsset
		{
			id = "max_nutrition",
			translation_key = "max_nutrition"
		});
		this.add(new BaseStatAsset
		{
			id = "metabolic_rate",
			translation_key = "metabolic_rate",
			normalize_min = 1f,
			hidden = true
		});
		this.add(new BaseStatAsset
		{
			id = "loyalty_mood",
			translation_key = "loyalty",
			used_only_for_civs = true
		});
		this.add(new BaseStatAsset
		{
			id = "opinion",
			used_only_for_civs = true
		});
		this.add(new BaseStatAsset
		{
			id = "skill_combat",
			show_as_percents = true,
			tooltip_multiply_for_visual_number = 100f
		});
		this.add(new BaseStatAsset
		{
			id = "skill_spell",
			show_as_percents = true,
			tooltip_multiply_for_visual_number = 100f
		});
		this.add(new BaseStatAsset
		{
			id = "knockback"
		});
		this.add(new BaseStatAsset
		{
			id = "recoil"
		});
		this.add(new BaseStatAsset
		{
			id = "mass"
		});
		this.add(new BaseStatAsset
		{
			id = "mass_2"
		});
		this.add(new BaseStatAsset
		{
			id = "multiplier_mass",
			show_as_percents = true,
			multiplier = true,
			main_stat_to_multiply = "mass_2",
			hidden = true
		});
		this.add(new BaseStatAsset
		{
			id = "limit_population"
		});
		this.add(new BaseStatAsset
		{
			id = "multiplier_health",
			show_as_percents = true,
			multiplier = true,
			main_stat_to_multiply = "health",
			tooltip_multiply_for_visual_number = 100f,
			translation_key = "health"
		});
		this.add(new BaseStatAsset
		{
			id = "multiplier_lifespan",
			show_as_percents = true,
			multiplier = true,
			main_stat_to_multiply = "lifespan",
			tooltip_multiply_for_visual_number = 100f,
			translation_key = "lifespan"
		});
		this.add(new BaseStatAsset
		{
			id = "multiplier_stamina",
			show_as_percents = true,
			multiplier = true,
			main_stat_to_multiply = "stamina",
			tooltip_multiply_for_visual_number = 100f,
			translation_key = "stamina"
		});
		this.add(new BaseStatAsset
		{
			id = "multiplier_mana",
			show_as_percents = true,
			multiplier = true,
			main_stat_to_multiply = "mana",
			tooltip_multiply_for_visual_number = 100f,
			translation_key = "mana"
		});
		this.add(new BaseStatAsset
		{
			id = "multiplier_damage",
			show_as_percents = true,
			multiplier = true,
			main_stat_to_multiply = "damage",
			tooltip_multiply_for_visual_number = 100f,
			translation_key = "damage"
		});
		this.add(new BaseStatAsset
		{
			id = "multiplier_crit",
			show_as_percents = true,
			multiplier = true,
			main_stat_to_multiply = "critical_chance",
			tooltip_multiply_for_visual_number = 100f,
			translation_key = "critical_chance_multiplier"
		});
		this.add(new BaseStatAsset
		{
			id = "multiplier_diplomacy",
			show_as_percents = true,
			multiplier = true,
			main_stat_to_multiply = "diplomacy",
			tooltip_multiply_for_visual_number = 100f,
			translation_key = "diplomacy",
			used_only_for_civs = true
		});
		this.add(new BaseStatAsset
		{
			id = "multiplier_speed",
			show_as_percents = true,
			multiplier = true,
			main_stat_to_multiply = "speed",
			tooltip_multiply_for_visual_number = 100f,
			translation_key = "speed"
		});
		this.add(new BaseStatAsset
		{
			id = "multiplier_attack_speed",
			show_as_percents = true,
			multiplier = true,
			main_stat_to_multiply = "attack_speed",
			tooltip_multiply_for_visual_number = 100f,
			translation_key = "attack_speed"
		});
		this.add(new BaseStatAsset
		{
			id = "scale",
			show_as_percents = true,
			tooltip_multiply_for_visual_number = 1000f,
			translation_key = "size"
		});
		this.add(new BaseStatAsset
		{
			id = "multiplier_supply_timer",
			hidden = true,
			used_only_for_civs = true
		});
		this.add(new BaseStatAsset
		{
			id = "limit_clan_members",
			used_only_for_civs = true
		});
		this.add(new BaseStatAsset
		{
			id = "status_chance"
		});
		this.add(new BaseStatAsset
		{
			id = "damage_range",
			hidden = true,
			normalize = true,
			normalize_min = 0.1f
		});
	}

	// Token: 0x0600134B RID: 4939 RVA: 0x000D8318 File Offset: 0x000D6518
	public override void editorDiagnosticLocales()
	{
		foreach (BaseStatAsset tAsset in this.list)
		{
			if (!tAsset.hidden)
			{
				this.checkLocale(tAsset, tAsset.getLocaleID());
			}
		}
		base.editorDiagnosticLocales();
	}
}
