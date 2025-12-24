using System;

// Token: 0x0200016A RID: 362
[Serializable]
public class SimGlobalAsset : Asset
{
	// Token: 0x04000A79 RID: 2681
	public int people_before_meta_divide = 1000;

	// Token: 0x04000A7A RID: 2682
	public int years_before_meta_divide = 500;

	// Token: 0x04000A7B RID: 2683
	public float baby_mass_multiplier = 0.4f;

	// Token: 0x04000A7C RID: 2684
	public float MANA_PER_INTELLIGENCE = 10f;

	// Token: 0x04000A7D RID: 2685
	public float base_tax_rate_local = 0.5f;

	// Token: 0x04000A7E RID: 2686
	public float base_tax_rate_tribute = 0.5f;

	// Token: 0x04000A7F RID: 2687
	public int festive_fireworks_cost = 10;

	// Token: 0x04000A80 RID: 2688
	public float empty_city_borders_shrink_time = 3f;

	// Token: 0x04000A81 RID: 2689
	public float water_damage_multiplier = 0.1f;

	// Token: 0x04000A82 RID: 2690
	public float starvation_damage_multiplier = 0.05f;

	// Token: 0x04000A83 RID: 2691
	public int base_metabolic_rate = 1;

	// Token: 0x04000A84 RID: 2692
	public int unit_chunk_sight_range = 1;

	// Token: 0x04000A85 RID: 2693
	public int child_work_age = 8;

	// Token: 0x04000A86 RID: 2694
	public int diplomacy_years_war_timeout = 5;

	// Token: 0x04000A87 RID: 2695
	public int diplomacy_years_war_min_warriors = 10;

	// Token: 0x04000A88 RID: 2696
	public int rebellions_min_age = 15;

	// Token: 0x04000A89 RID: 2697
	public int rebellions_min_warriors = 10;

	// Token: 0x04000A8A RID: 2698
	public float rebellions_unhappy_multiplier = 0.1f;

	// Token: 0x04000A8B RID: 2699
	public int alliance_months_per_level = 30;

	// Token: 0x04000A8C RID: 2700
	public int alliance_timeout = 3;

	// Token: 0x04000A8D RID: 2701
	public float level_mod_bonus_health = 0.05f;

	// Token: 0x04000A8E RID: 2702
	public float level_mod_bonus_stamina = 0.02f;

	// Token: 0x04000A8F RID: 2703
	public float level_mod_bonus_mana = 0.02f;

	// Token: 0x04000A90 RID: 2704
	public float unexplored_sprite_animation_speed = 22f;

	// Token: 0x04000A91 RID: 2705
	public int minimum_kingdom_age_for_attack = 5;

	// Token: 0x04000A92 RID: 2706
	public int minimum_age_before_war_stop = 10;

	// Token: 0x04000A93 RID: 2707
	public int minimum_years_between_wars = 5;

	// Token: 0x04000A94 RID: 2708
	public int biomes_growth_speed = 5;

	// Token: 0x04000A95 RID: 2709
	public float fire_spread_time = 2f;

	// Token: 0x04000A96 RID: 2710
	public float fire_stop_time = 5f;

	// Token: 0x04000A97 RID: 2711
	public float fire_time = 30f;

	// Token: 0x04000A98 RID: 2712
	public bool allow_different_species_buildings = true;

	// Token: 0x04000A99 RID: 2713
	public float interval_nutrition_decay = 15f;

	// Token: 0x04000A9A RID: 2714
	public float interval_happiness = 2f;

	// Token: 0x04000A9B RID: 2715
	public float interval_stamina = 2f;

	// Token: 0x04000A9C RID: 2716
	public float interval_mana = 10f;

	// Token: 0x04000A9D RID: 2717
	public int stamina_change = 1;

	// Token: 0x04000A9E RID: 2718
	public int mana_change = 1;

	// Token: 0x04000A9F RID: 2719
	public float unit_speed_multiplier = 0.5f;

	// Token: 0x04000AA0 RID: 2720
	public float unit_force_multiplier = 1f;

	// Token: 0x04000AA1 RID: 2721
	public float gravity = 9.8f;

	// Token: 0x04000AA2 RID: 2722
	public float min_people_for_civilization = 2f;

	// Token: 0x04000AA3 RID: 2723
	public float new_civilization_range = 20f;

	// Token: 0x04000AA4 RID: 2724
	public float nomad_check_far_city_range = 20f;

	// Token: 0x04000AA5 RID: 2725
	public float forgotten_plot_time = 20f;

	// Token: 0x04000AA6 RID: 2726
	public int nutrition_cost_new_baby = 50;

	// Token: 0x04000AA7 RID: 2727
	public float nutrition_level_hungry = 0.5f;

	// Token: 0x04000AA8 RID: 2728
	public int nutrition_start_level_baby = 25;

	// Token: 0x04000AA9 RID: 2729
	public int nutrition_level_on_spawn = 100;

	// Token: 0x04000AAA RID: 2730
	public int min_coins_before_city_food = 5;

	// Token: 0x04000AAB RID: 2731
	public int months_till_pool_turns_into_flora = 30;

	// Token: 0x04000AAC RID: 2732
	public float item_repair_cost_multiplier = 0.5f;

	// Token: 0x04000AAD RID: 2733
	public int coins_for_road = 1;

	// Token: 0x04000AAE RID: 2734
	public int coins_for_zone = 5;

	// Token: 0x04000AAF RID: 2735
	public int coins_for_field = 1;

	// Token: 0x04000AB0 RID: 2736
	public int coins_for_planting = 1;

	// Token: 0x04000AB1 RID: 2737
	public int coins_for_fertilize = 1;

	// Token: 0x04000AB2 RID: 2738
	public int coins_for_mine = 1;

	// Token: 0x04000AB3 RID: 2739
	public int coins_for_cleaning = 1;

	// Token: 0x04000AB4 RID: 2740
	public int coins_for_building = 6;
}
