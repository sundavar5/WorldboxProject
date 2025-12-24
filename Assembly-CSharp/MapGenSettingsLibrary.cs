using System;

// Token: 0x02000056 RID: 86
public class MapGenSettingsLibrary : AssetLibrary<MapGenSettingsAsset>
{
	// Token: 0x06000329 RID: 809 RVA: 0x0001DAB4 File Offset: 0x0001BCB4
	public override void init()
	{
		base.init();
		MapGenSettingsAsset mapGenSettingsAsset = new MapGenSettingsAsset();
		mapGenSettingsAsset.id = "gen_perlin_scale_stage_1";
		mapGenSettingsAsset.allowed_check = ((MapGenTemplate pAsset) => pAsset.allow_edit_perlin_scale_stage_1);
		mapGenSettingsAsset.max_value = 30;
		mapGenSettingsAsset.action_get = (() => this.gen_values.perlin_scale_stage_1);
		mapGenSettingsAsset.action_set = delegate(int pValue)
		{
			this.gen_values.perlin_scale_stage_1 = pValue;
		};
		this.add(mapGenSettingsAsset);
		MapGenSettingsAsset mapGenSettingsAsset2 = new MapGenSettingsAsset();
		mapGenSettingsAsset2.id = "gen_perlin_scale_stage_2";
		mapGenSettingsAsset2.allowed_check = ((MapGenTemplate pAsset) => pAsset.allow_edit_perlin_scale_stage_1);
		mapGenSettingsAsset2.max_value = 30;
		mapGenSettingsAsset2.action_get = (() => this.gen_values.perlin_scale_stage_2);
		mapGenSettingsAsset2.action_set = delegate(int pValue)
		{
			this.gen_values.perlin_scale_stage_2 = pValue;
		};
		this.add(mapGenSettingsAsset2);
		MapGenSettingsAsset mapGenSettingsAsset3 = new MapGenSettingsAsset();
		mapGenSettingsAsset3.id = "gen_perlin_scale_stage_3";
		mapGenSettingsAsset3.allowed_check = ((MapGenTemplate pAsset) => pAsset.allow_edit_perlin_scale_stage_1);
		mapGenSettingsAsset3.max_value = 30;
		mapGenSettingsAsset3.action_get = (() => this.gen_values.perlin_scale_stage_3);
		mapGenSettingsAsset3.action_set = delegate(int pValue)
		{
			this.gen_values.perlin_scale_stage_3 = pValue;
		};
		this.add(mapGenSettingsAsset3);
		MapGenSettingsAsset mapGenSettingsAsset4 = new MapGenSettingsAsset();
		mapGenSettingsAsset4.id = "gen_random_shapes";
		mapGenSettingsAsset4.allowed_check = ((MapGenTemplate pAsset) => pAsset.allow_edit_random_shapes);
		mapGenSettingsAsset4.max_value = 40;
		mapGenSettingsAsset4.action_get = (() => this.gen_values.random_shapes_amount);
		mapGenSettingsAsset4.action_set = delegate(int pValue)
		{
			this.gen_values.random_shapes_amount = pValue;
		};
		this.add(mapGenSettingsAsset4);
		MapGenSettingsAsset mapGenSettingsAsset5 = new MapGenSettingsAsset();
		mapGenSettingsAsset5.id = "gen_cubicles_sizes";
		mapGenSettingsAsset5.allowed_check = ((MapGenTemplate pAsset) => pAsset.allow_edit_cubicles);
		mapGenSettingsAsset5.min_value = 2;
		mapGenSettingsAsset5.max_value = 15;
		mapGenSettingsAsset5.action_get = (() => this.gen_values.cubicle_size);
		mapGenSettingsAsset5.action_set = delegate(int pValue)
		{
			this.gen_values.cubicle_size = pValue;
		};
		this.add(mapGenSettingsAsset5);
		MapGenSettingsAsset mapGenSettingsAsset6 = new MapGenSettingsAsset();
		mapGenSettingsAsset6.id = "gen_random_biomes";
		mapGenSettingsAsset6.allowed_check = ((MapGenTemplate pAsset) => pAsset.allow_edit_random_biomes);
		mapGenSettingsAsset6.is_switch = true;
		mapGenSettingsAsset6.action_get = delegate()
		{
			if (!this.gen_values.random_biomes)
			{
				return 0;
			}
			return 1;
		};
		mapGenSettingsAsset6.action_set = delegate(int pValue)
		{
			this.gen_values.random_biomes = (pValue == 1);
		};
		this.add(mapGenSettingsAsset6);
		MapGenSettingsAsset mapGenSettingsAsset7 = new MapGenSettingsAsset();
		mapGenSettingsAsset7.id = "gen_mountain_edges";
		mapGenSettingsAsset7.allowed_check = ((MapGenTemplate pAsset) => pAsset.allow_edit_mountain_edges);
		mapGenSettingsAsset7.is_switch = true;
		mapGenSettingsAsset7.action_get = delegate()
		{
			if (!this.gen_values.add_mountain_edges)
			{
				return 0;
			}
			return 1;
		};
		mapGenSettingsAsset7.action_set = delegate(int pValue)
		{
			this.gen_values.add_mountain_edges = (pValue == 1);
		};
		this.add(mapGenSettingsAsset7);
		MapGenSettingsAsset mapGenSettingsAsset8 = new MapGenSettingsAsset();
		mapGenSettingsAsset8.id = "gen_add_vegetation";
		mapGenSettingsAsset8.allowed_check = ((MapGenTemplate pAsset) => pAsset.allow_edit_random_vegetation);
		mapGenSettingsAsset8.is_switch = true;
		mapGenSettingsAsset8.action_get = delegate()
		{
			if (!this.gen_values.add_vegetation)
			{
				return 0;
			}
			return 1;
		};
		mapGenSettingsAsset8.action_set = delegate(int pValue)
		{
			this.gen_values.add_vegetation = (pValue == 1);
		};
		this.add(mapGenSettingsAsset8);
		MapGenSettingsAsset mapGenSettingsAsset9 = new MapGenSettingsAsset();
		mapGenSettingsAsset9.id = "gen_add_resources";
		mapGenSettingsAsset9.allowed_check = ((MapGenTemplate pAsset) => pAsset.allow_edit_random_resources);
		mapGenSettingsAsset9.is_switch = true;
		mapGenSettingsAsset9.action_get = delegate()
		{
			if (!this.gen_values.add_resources)
			{
				return 0;
			}
			return 1;
		};
		mapGenSettingsAsset9.action_set = delegate(int pValue)
		{
			this.gen_values.add_resources = (pValue == 1);
		};
		this.add(mapGenSettingsAsset9);
		MapGenSettingsAsset mapGenSettingsAsset10 = new MapGenSettingsAsset();
		mapGenSettingsAsset10.id = "gen_add_center_lake";
		mapGenSettingsAsset10.allowed_check = ((MapGenTemplate pAsset) => pAsset.allow_edit_center_lake);
		mapGenSettingsAsset10.is_switch = true;
		mapGenSettingsAsset10.action_get = delegate()
		{
			if (!this.gen_values.add_center_lake)
			{
				return 0;
			}
			return 1;
		};
		mapGenSettingsAsset10.action_set = delegate(int pValue)
		{
			this.gen_values.add_center_lake = (pValue == 1);
		};
		this.add(mapGenSettingsAsset10);
		MapGenSettingsAsset mapGenSettingsAsset11 = new MapGenSettingsAsset();
		mapGenSettingsAsset11.id = "gen_add_center_land";
		mapGenSettingsAsset11.allowed_check = ((MapGenTemplate pAsset) => pAsset.allow_edit_center_land);
		mapGenSettingsAsset11.is_switch = true;
		mapGenSettingsAsset11.action_get = delegate()
		{
			if (!this.gen_values.add_center_gradient_land)
			{
				return 0;
			}
			return 1;
		};
		mapGenSettingsAsset11.action_set = delegate(int pValue)
		{
			this.gen_values.add_center_gradient_land = (pValue == 1);
		};
		this.add(mapGenSettingsAsset11);
		MapGenSettingsAsset mapGenSettingsAsset12 = new MapGenSettingsAsset();
		mapGenSettingsAsset12.id = "gen_round_edges";
		mapGenSettingsAsset12.allowed_check = ((MapGenTemplate pAsset) => pAsset.allow_edit_round_edges);
		mapGenSettingsAsset12.is_switch = true;
		mapGenSettingsAsset12.action_get = delegate()
		{
			if (!this.gen_values.gradient_round_edges)
			{
				return 0;
			}
			return 1;
		};
		mapGenSettingsAsset12.action_set = delegate(int pValue)
		{
			this.gen_values.gradient_round_edges = (pValue == 1);
		};
		this.add(mapGenSettingsAsset12);
		MapGenSettingsAsset mapGenSettingsAsset13 = new MapGenSettingsAsset();
		mapGenSettingsAsset13.id = "gen_square_edges";
		mapGenSettingsAsset13.allowed_check = ((MapGenTemplate pAsset) => pAsset.allow_edit_square_edges);
		mapGenSettingsAsset13.is_switch = true;
		mapGenSettingsAsset13.action_get = delegate()
		{
			if (!this.gen_values.square_edges)
			{
				return 0;
			}
			return 1;
		};
		mapGenSettingsAsset13.action_set = delegate(int pValue)
		{
			this.gen_values.square_edges = (pValue == 1);
		};
		this.add(mapGenSettingsAsset13);
		MapGenSettingsAsset mapGenSettingsAsset14 = new MapGenSettingsAsset();
		mapGenSettingsAsset14.id = "gen_ring_effect";
		mapGenSettingsAsset14.allowed_check = ((MapGenTemplate pAsset) => pAsset.allow_edit_ring_effect);
		mapGenSettingsAsset14.is_switch = true;
		mapGenSettingsAsset14.action_get = delegate()
		{
			if (!this.gen_values.ring_effect)
			{
				return 0;
			}
			return 1;
		};
		mapGenSettingsAsset14.action_set = delegate(int pValue)
		{
			this.gen_values.ring_effect = (pValue == 1);
		};
		this.add(mapGenSettingsAsset14);
		MapGenSettingsAsset mapGenSettingsAsset15 = new MapGenSettingsAsset();
		mapGenSettingsAsset15.id = "gen_low_ground";
		mapGenSettingsAsset15.allowed_check = ((MapGenTemplate pAsset) => pAsset.allow_edit_low_ground);
		mapGenSettingsAsset15.is_switch = true;
		mapGenSettingsAsset15.action_set = delegate(int pValue)
		{
			this.gen_values.low_ground = (pValue == 1);
		};
		mapGenSettingsAsset15.action_get = delegate()
		{
			if (!this.gen_values.low_ground)
			{
				return 0;
			}
			return 1;
		};
		this.add(mapGenSettingsAsset15);
		MapGenSettingsAsset mapGenSettingsAsset16 = new MapGenSettingsAsset();
		mapGenSettingsAsset16.id = "gen_high_ground";
		mapGenSettingsAsset16.allowed_check = ((MapGenTemplate pAsset) => pAsset.allow_edit_high_ground);
		mapGenSettingsAsset16.is_switch = true;
		mapGenSettingsAsset16.action_set = delegate(int pValue)
		{
			this.gen_values.high_ground = (pValue == 1);
		};
		mapGenSettingsAsset16.action_get = delegate()
		{
			if (!this.gen_values.high_ground)
			{
				return 0;
			}
			return 1;
		};
		this.add(mapGenSettingsAsset16);
		MapGenSettingsAsset mapGenSettingsAsset17 = new MapGenSettingsAsset();
		mapGenSettingsAsset17.id = "gen_remove_mountains";
		mapGenSettingsAsset17.allowed_check = ((MapGenTemplate pAsset) => pAsset.allow_edit_remove_mountains);
		mapGenSettingsAsset17.is_switch = true;
		mapGenSettingsAsset17.action_set = delegate(int pValue)
		{
			this.gen_values.remove_mountains = (pValue == 1);
		};
		mapGenSettingsAsset17.action_get = delegate()
		{
			if (!this.gen_values.remove_mountains)
			{
				return 0;
			}
			return 1;
		};
		this.add(mapGenSettingsAsset17);
		MapGenSettingsAsset mapGenSettingsAsset18 = new MapGenSettingsAsset();
		mapGenSettingsAsset18.id = "gen_forbidden_knowledge";
		mapGenSettingsAsset18.allowed_check = ((MapGenTemplate _) => Config.hasPremium && AchievementLibrary.cursed_world.isUnlocked());
		mapGenSettingsAsset18.is_switch = true;
		mapGenSettingsAsset18.action_get = delegate()
		{
			if (!this.gen_values.forbidden_knowledge_start)
			{
				return 0;
			}
			return 1;
		};
		mapGenSettingsAsset18.action_set = delegate(int pValue)
		{
			this.gen_values.forbidden_knowledge_start = (pValue == 1);
		};
		this.add(mapGenSettingsAsset18);
	}

	// Token: 0x0600032A RID: 810 RVA: 0x0001E214 File Offset: 0x0001C414
	public override MapGenSettingsAsset add(MapGenSettingsAsset pAsset)
	{
		pAsset2.increase = delegate(MapGenSettingsAsset pAsset)
		{
			int tTemp = pAsset.action_get();
			int tVal = 1;
			if (HotkeyLibrary.many_mod.isHolding())
			{
				tVal = 5;
			}
			tTemp += tVal;
			if (tTemp > pAsset.max_value)
			{
				tTemp = pAsset.min_value;
			}
			pAsset.action_set(tTemp);
		};
		pAsset2.decrease = delegate(MapGenSettingsAsset pAsset)
		{
			int tTemp = pAsset.action_get();
			int tVal = 1;
			if (HotkeyLibrary.many_mod.isHolding())
			{
				tVal = 5;
			}
			tTemp -= tVal;
			if (tTemp < pAsset.min_value)
			{
				tTemp = pAsset.max_value;
			}
			pAsset.action_set(tTemp);
		};
		pAsset2.action_switch = delegate(MapGenSettingsAsset pAsset)
		{
			int tVal;
			if (pAsset.action_get() == 0)
			{
				tVal = 1;
			}
			else
			{
				tVal = 0;
			}
			pAsset.action_set(tVal);
		};
		return base.add(pAsset2);
	}

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x0600032B RID: 811 RVA: 0x0001E297 File Offset: 0x0001C497
	private MapGenValues gen_values
	{
		get
		{
			return AssetManager.map_gen_templates.get(Config.current_map_template).values;
		}
	}

	// Token: 0x0600032C RID: 812 RVA: 0x0001E2B0 File Offset: 0x0001C4B0
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (MapGenSettingsAsset tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
		}
	}
}
