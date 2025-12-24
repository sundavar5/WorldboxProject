using System;
using System.ComponentModel;
using Newtonsoft.Json;

// Token: 0x020000F8 RID: 248
[Serializable]
public class EffectAsset : Asset
{
	// Token: 0x17000027 RID: 39
	// (get) Token: 0x06000740 RID: 1856 RVA: 0x0006A810 File Offset: 0x00068A10
	[JsonIgnore]
	public bool has_sound_launch
	{
		get
		{
			return this.sound_launch != null;
		}
	}

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x06000741 RID: 1857 RVA: 0x0006A81E File Offset: 0x00068A1E
	[JsonIgnore]
	public bool has_sound_loop_idle
	{
		get
		{
			return this.sound_loop_idle != null;
		}
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x0006A82C File Offset: 0x00068A2C
	public bool checkIsUnderCooldown()
	{
		double tCurTime = World.world.getCurSessionTime();
		if (tCurTime - this._cooldown < this.cooldown_interval)
		{
			return true;
		}
		this._cooldown = tCurTime;
		return false;
	}

	// Token: 0x040007E7 RID: 2023
	public string prefab_id;

	// Token: 0x040007E8 RID: 2024
	public bool use_basic_prefab;

	// Token: 0x040007E9 RID: 2025
	public bool load_texture;

	// Token: 0x040007EA RID: 2026
	public string sprite_path;

	// Token: 0x040007EB RID: 2027
	public string sorting_layer_id;

	// Token: 0x040007EC RID: 2028
	[DefaultValue(0.1f)]
	public float time_between_frames = 0.1f;

	// Token: 0x040007ED RID: 2029
	public int limit;

	// Token: 0x040007EE RID: 2030
	public bool limit_unload;

	// Token: 0x040007EF RID: 2031
	public string sound_launch;

	// Token: 0x040007F0 RID: 2032
	public string sound_loop_idle;

	// Token: 0x040007F1 RID: 2033
	public bool show_on_mini_map;

	// Token: 0x040007F2 RID: 2034
	public EffectAction spawn_action;

	// Token: 0x040007F3 RID: 2035
	public double cooldown_interval;

	// Token: 0x040007F4 RID: 2036
	private double _cooldown;

	// Token: 0x040007F5 RID: 2037
	public bool draw_light_area;

	// Token: 0x040007F6 RID: 2038
	public float draw_light_area_offset_x;

	// Token: 0x040007F7 RID: 2039
	public float draw_light_area_offset_y;

	// Token: 0x040007F8 RID: 2040
	[DefaultValue(0.5f)]
	public float draw_light_size = 0.5f;
}
