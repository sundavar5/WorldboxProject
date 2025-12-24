using System;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x02000160 RID: 352
[Serializable]
public class QuantumSpriteAsset : Asset
{
	// Token: 0x1700003B RID: 59
	// (get) Token: 0x06000A6B RID: 2667 RVA: 0x000966B9 File Offset: 0x000948B9
	[JsonIgnore]
	public bool has_sound_idle
	{
		get
		{
			return this.sound_idle != null;
		}
	}

	// Token: 0x04000A21 RID: 2593
	public string id_prefab;

	// Token: 0x04000A22 RID: 2594
	public bool turn_off_renderer;

	// Token: 0x04000A23 RID: 2595
	public float base_scale = 0.2f;

	// Token: 0x04000A24 RID: 2596
	public bool flag_battle;

	// Token: 0x04000A25 RID: 2597
	public bool flag_kingdom_color;

	// Token: 0x04000A26 RID: 2598
	public bool render_arrow_end;

	// Token: 0x04000A27 RID: 2599
	public bool render_arrow_start;

	// Token: 0x04000A28 RID: 2600
	public bool arrow_animation;

	// Token: 0x04000A29 RID: 2601
	public int line_height = 3;

	// Token: 0x04000A2A RID: 2602
	public int line_width = 3;

	// Token: 0x04000A2B RID: 2603
	public bool render_gameplay;

	// Token: 0x04000A2C RID: 2604
	public bool render_map;

	// Token: 0x04000A2D RID: 2605
	public bool render_space;

	// Token: 0x04000A2E RID: 2606
	public bool add_camera_zoom_multiplier = true;

	// Token: 0x04000A2F RID: 2607
	public int add_camera_zoom_multiplier_min = 1;

	// Token: 0x04000A30 RID: 2608
	public int add_camera_zoom_multiplier_max = 8;

	// Token: 0x04000A31 RID: 2609
	public string sound_idle;

	// Token: 0x04000A32 RID: 2610
	public bool selected_city_scale;

	// Token: 0x04000A33 RID: 2611
	public string path_icon;

	// Token: 0x04000A34 RID: 2612
	public DebugOption debug_option;

	// Token: 0x04000A35 RID: 2613
	public int default_amount;

	// Token: 0x04000A36 RID: 2614
	public Color color;

	// Token: 0x04000A37 RID: 2615
	public Color color_2;

	// Token: 0x04000A38 RID: 2616
	[NonSerialized]
	public QuantumSpriteGroupSystem group_system;

	// Token: 0x04000A39 RID: 2617
	public QuantumSpriteUpdater draw_call;

	// Token: 0x04000A3A RID: 2618
	public QuantumSpriteCreate create_object;
}
