using System;
using System.ComponentModel;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x0200017B RID: 379
[Serializable]
public class StatusAsset : Asset, IDescriptionAsset, ILocalizedAsset
{
	// Token: 0x17000048 RID: 72
	// (get) Token: 0x06000B4D RID: 2893 RVA: 0x000A219D File Offset: 0x000A039D
	[JsonIgnore]
	public bool has_sound_idle
	{
		get
		{
			return this.sound_idle != null;
		}
	}

	// Token: 0x06000B4E RID: 2894 RVA: 0x000A21AB File Offset: 0x000A03AB
	public Sprite getSprite()
	{
		if (this.cached_sprite == null)
		{
			this.cached_sprite = SpriteTextureLoader.getSprite(this.path_icon);
		}
		return this.cached_sprite;
	}

	// Token: 0x06000B4F RID: 2895 RVA: 0x000A21D2 File Offset: 0x000A03D2
	private static bool defaultRenderCheck(ActorAsset pAsset)
	{
		return true;
	}

	// Token: 0x06000B50 RID: 2896 RVA: 0x000A21D5 File Offset: 0x000A03D5
	public Material getMaterial()
	{
		return LibraryMaterials.instance.dict[this.material_id];
	}

	// Token: 0x06000B51 RID: 2897 RVA: 0x000A21EC File Offset: 0x000A03EC
	public DecisionAsset getDecisionAsset()
	{
		return AssetManager.decisions_library.get(this.decision_id);
	}

	// Token: 0x06000B52 RID: 2898 RVA: 0x000A21FE File Offset: 0x000A03FE
	public string getLocaleID()
	{
		return this.locale_id;
	}

	// Token: 0x06000B53 RID: 2899 RVA: 0x000A2206 File Offset: 0x000A0406
	public string getDescriptionID()
	{
		return this.locale_description;
	}

	// Token: 0x04000ADC RID: 2780
	public WorldAction action_finish;

	// Token: 0x04000ADD RID: 2781
	public WorldAction action_death;

	// Token: 0x04000ADE RID: 2782
	public WorldAction action;

	// Token: 0x04000ADF RID: 2783
	public GetHitAction action_get_hit;

	// Token: 0x04000AE0 RID: 2784
	public WorldAction action_on_receive;

	// Token: 0x04000AE1 RID: 2785
	public float action_interval;

	// Token: 0x04000AE2 RID: 2786
	[DefaultValue(StatusTier.Basic)]
	public StatusTier tier = StatusTier.Basic;

	// Token: 0x04000AE3 RID: 2787
	public bool can_be_cured;

	// Token: 0x04000AE4 RID: 2788
	[DefaultValue(10f)]
	public float duration = 10f;

	// Token: 0x04000AE5 RID: 2789
	[DefaultValue(true)]
	public bool allow_timer_reset = true;

	// Token: 0x04000AE6 RID: 2790
	public string texture;

	// Token: 0x04000AE7 RID: 2791
	public bool random_frame;

	// Token: 0x04000AE8 RID: 2792
	[DefaultValue(true)]
	public bool can_be_flipped = true;

	// Token: 0x04000AE9 RID: 2793
	public bool animated;

	// Token: 0x04000AEA RID: 2794
	public bool is_animated_in_pause;

	// Token: 0x04000AEB RID: 2795
	[DefaultValue(true)]
	public bool loop = true;

	// Token: 0x04000AEC RID: 2796
	[DefaultValue(0.1f)]
	public float animation_speed = 0.1f;

	// Token: 0x04000AED RID: 2797
	public float animation_speed_random;

	// Token: 0x04000AEE RID: 2798
	[DefaultValue(1f)]
	public float scale = 1f;

	// Token: 0x04000AEF RID: 2799
	public float offset_x;

	// Token: 0x04000AF0 RID: 2800
	public float offset_x_ui;

	// Token: 0x04000AF1 RID: 2801
	public float offset_y;

	// Token: 0x04000AF2 RID: 2802
	public float offset_y_ui;

	// Token: 0x04000AF3 RID: 2803
	public float rotation_z;

	// Token: 0x04000AF4 RID: 2804
	[DefaultValue(true)]
	public bool use_parent_rotation = true;

	// Token: 0x04000AF5 RID: 2805
	public bool removed_on_damage;

	// Token: 0x04000AF6 RID: 2806
	[DefaultValue(0.01f)]
	public float position_z = 0.01f;

	// Token: 0x04000AF7 RID: 2807
	public bool random_flip;

	// Token: 0x04000AF8 RID: 2808
	public bool cancel_actor_job;

	// Token: 0x04000AF9 RID: 2809
	public bool affects_mind;

	// Token: 0x04000AFA RID: 2810
	[DefaultValue("mat_world_object")]
	public string material_id = "mat_world_object";

	// Token: 0x04000AFB RID: 2811
	[NonSerialized]
	public Material material;

	// Token: 0x04000AFC RID: 2812
	public bool draw_light_area;

	// Token: 0x04000AFD RID: 2813
	[DefaultValue(0.2f)]
	public float draw_light_size = 0.2f;

	// Token: 0x04000AFE RID: 2814
	public BaseStats base_stats = new BaseStats();

	// Token: 0x04000AFF RID: 2815
	public string[] opposite_traits;

	// Token: 0x04000B00 RID: 2816
	public string[] opposite_tags;

	// Token: 0x04000B01 RID: 2817
	public string[] opposite_status;

	// Token: 0x04000B02 RID: 2818
	public string[] remove_status;

	// Token: 0x04000B03 RID: 2819
	[NonSerialized]
	public Sprite[] sprite_list;

	// Token: 0x04000B04 RID: 2820
	public string path_icon;

	// Token: 0x04000B05 RID: 2821
	public Sprite cached_sprite;

	// Token: 0x04000B06 RID: 2822
	public int render_priority;

	// Token: 0x04000B07 RID: 2823
	public string sound_idle;

	// Token: 0x04000B08 RID: 2824
	public string locale_id;

	// Token: 0x04000B09 RID: 2825
	public string locale_description;

	// Token: 0x04000B0A RID: 2826
	public GetEffectSprite get_override_sprite;

	// Token: 0x04000B0B RID: 2827
	public GetEffectSpriteUI get_override_sprite_ui;

	// Token: 0x04000B0C RID: 2828
	[NonSerialized]
	public bool has_override_sprite;

	// Token: 0x04000B0D RID: 2829
	public GetEffectSpritePosition get_override_sprite_position;

	// Token: 0x04000B0E RID: 2830
	public GetEffectSpritePositionUI get_override_sprite_position_ui;

	// Token: 0x04000B0F RID: 2831
	[NonSerialized]
	public bool has_override_sprite_position;

	// Token: 0x04000B10 RID: 2832
	public GetEffectSpriteRotationZ get_override_sprite_rotation_z;

	// Token: 0x04000B11 RID: 2833
	public GetEffectSpriteRotationZUI get_override_sprite_rotation_z_ui;

	// Token: 0x04000B12 RID: 2834
	[NonSerialized]
	public bool has_override_sprite_rotation_z;

	// Token: 0x04000B13 RID: 2835
	[NonSerialized]
	public GetEffectSpriteCount get_sprites_count = delegate(BaseSimObject _, StatusAsset pEffect)
	{
		if (pEffect == null)
		{
			return 0;
		}
		return pEffect.sprite_list.Length;
	};

	// Token: 0x04000B14 RID: 2836
	public RenderEffectCheck render_check = new RenderEffectCheck(StatusAsset.defaultRenderCheck);

	// Token: 0x04000B15 RID: 2837
	public string decision_id;

	// Token: 0x04000B16 RID: 2838
	[NonSerialized]
	public bool need_visual_render;
}
