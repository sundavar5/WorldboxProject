using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

// Token: 0x020001AF RID: 431
[Serializable]
public class WorldAgeAsset : Asset, IDescriptionAsset, ILocalizedAsset
{
	// Token: 0x17000062 RID: 98
	// (get) Token: 0x06000C84 RID: 3204 RVA: 0x000B6DF2 File Offset: 0x000B4FF2
	internal Color pie_selection_color
	{
		get
		{
			return this.title_color;
		}
	}

	// Token: 0x06000C85 RID: 3205 RVA: 0x000B6DFA File Offset: 0x000B4FFA
	public Sprite getSprite()
	{
		if (this._cached_sprite == null)
		{
			this._cached_sprite = SpriteTextureLoader.getSprite(this.path_icon);
		}
		return this._cached_sprite;
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x000B6E21 File Offset: 0x000B5021
	public Sprite getBackground()
	{
		if (this._cached_background == null)
		{
			this._cached_background = SpriteTextureLoader.getSprite(this.path_background);
		}
		return this._cached_background;
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x000B6E48 File Offset: 0x000B5048
	public string getLocaleID()
	{
		return this.id + "_title";
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x000B6E5A File Offset: 0x000B505A
	public string getDescriptionID()
	{
		return this.id + "_description";
	}

	// Token: 0x04000BE4 RID: 3044
	[DefaultValue(35)]
	public int years_min = 35;

	// Token: 0x04000BE5 RID: 3045
	[DefaultValue(55)]
	public int years_max = 55;

	// Token: 0x04000BE6 RID: 3046
	[DefaultValue(0.2f)]
	public float era_effect_overlay_alpha = 0.2f;

	// Token: 0x04000BE7 RID: 3047
	[DefaultValue(1f)]
	public float era_effect_light_alpha_game = 1f;

	// Token: 0x04000BE8 RID: 3048
	[DefaultValue(1f)]
	public float era_effect_light_alpha_minimap = 1f;

	// Token: 0x04000BE9 RID: 3049
	public bool overlay_darkness;

	// Token: 0x04000BEA RID: 3050
	public bool particles_snow;

	// Token: 0x04000BEB RID: 3051
	public bool particles_rain;

	// Token: 0x04000BEC RID: 3052
	public bool particles_magic;

	// Token: 0x04000BED RID: 3053
	public bool particles_ash;

	// Token: 0x04000BEE RID: 3054
	public bool particles_sun;

	// Token: 0x04000BEF RID: 3055
	public bool global_freeze_world;

	// Token: 0x04000BF0 RID: 3056
	public bool global_unfreeze_world;

	// Token: 0x04000BF1 RID: 3057
	public bool global_unfreeze_world_mountains;

	// Token: 0x04000BF2 RID: 3058
	public bool overlay_magic;

	// Token: 0x04000BF3 RID: 3059
	public bool overlay_rain_darkness;

	// Token: 0x04000BF4 RID: 3060
	public bool overlay_winter;

	// Token: 0x04000BF5 RID: 3061
	public bool overlay_chaos;

	// Token: 0x04000BF6 RID: 3062
	public bool overlay_moon;

	// Token: 0x04000BF7 RID: 3063
	public bool overlay_sun;

	// Token: 0x04000BF8 RID: 3064
	public bool overlay_ash;

	// Token: 0x04000BF9 RID: 3065
	public bool overlay_night;

	// Token: 0x04000BFA RID: 3066
	public bool overlay_rain;

	// Token: 0x04000BFB RID: 3067
	public List<string> clouds;

	// Token: 0x04000BFC RID: 3068
	public HashSet<string> biomes;

	// Token: 0x04000BFD RID: 3069
	[DefaultValue(15f)]
	public float cloud_interval = 15f;

	// Token: 0x04000BFE RID: 3070
	[DefaultValue(1f)]
	public float range_weapons_multiplier = 1f;

	// Token: 0x04000BFF RID: 3071
	[DefaultValue(1)]
	public int temperature_damage_bonus = 1;

	// Token: 0x04000C00 RID: 3072
	public string[] conditions;

	// Token: 0x04000C01 RID: 3073
	[DefaultValue("")]
	public string force_next = string.Empty;

	// Token: 0x04000C02 RID: 3074
	[DefaultValue(true)]
	public bool flag_crops_grow = true;

	// Token: 0x04000C03 RID: 3075
	public bool era_disaster_snow_turns_babies_into_ice_ones;

	// Token: 0x04000C04 RID: 3076
	public bool era_disaster_fire_elemental_spawn_on_fire;

	// Token: 0x04000C05 RID: 3077
	public float fire_elemental_spawn_chance;

	// Token: 0x04000C06 RID: 3078
	public bool era_disaster_rage_brings_demons;

	// Token: 0x04000C07 RID: 3079
	public bool flag_light_age;

	// Token: 0x04000C08 RID: 3080
	public bool flag_chaos;

	// Token: 0x04000C09 RID: 3081
	public bool flag_winter;

	// Token: 0x04000C0A RID: 3082
	public bool flag_moon;

	// Token: 0x04000C0B RID: 3083
	public bool flag_night;

	// Token: 0x04000C0C RID: 3084
	public bool flag_light_damage;

	// Token: 0x04000C0D RID: 3085
	public string path_icon;

	// Token: 0x04000C0E RID: 3086
	public string path_background;

	// Token: 0x04000C0F RID: 3087
	[NonSerialized]
	private Sprite _cached_sprite;

	// Token: 0x04000C10 RID: 3088
	[NonSerialized]
	private Sprite _cached_background;

	// Token: 0x04000C11 RID: 3089
	public Color light_color = Toolbox.makeColor("#FFCE61");

	// Token: 0x04000C12 RID: 3090
	public Color title_color = Toolbox.makeColor("#FFFFFF");

	// Token: 0x04000C13 RID: 3091
	public int bonus_loyalty;

	// Token: 0x04000C14 RID: 3092
	public int bonus_opinion;

	// Token: 0x04000C15 RID: 3093
	public int bonus_biomes_growth;

	// Token: 0x04000C16 RID: 3094
	[DefaultValue(true)]
	public bool grow_vegetation = true;

	// Token: 0x04000C17 RID: 3095
	[DefaultValue(1f)]
	public float fire_spread_rate_bonus = 1f;

	// Token: 0x04000C18 RID: 3096
	[DefaultValue(1)]
	public int rate = 1;

	// Token: 0x04000C19 RID: 3097
	[DefaultValue(10f)]
	public float special_effect_interval = 10f;

	// Token: 0x04000C1A RID: 3098
	public List<int> default_slots = new List<int>();

	// Token: 0x04000C1B RID: 3099
	public bool link_default_slots;

	// Token: 0x04000C1C RID: 3100
	public WorldAgeAction special_effect_action;
}
