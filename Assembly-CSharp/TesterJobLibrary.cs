using System;
using System.Collections.Generic;

// Token: 0x020004FD RID: 1277
public class TesterJobLibrary : AssetLibrary<JobTesterAsset>
{
	// Token: 0x06002A57 RID: 10839 RVA: 0x0014D788 File Offset: 0x0014B988
	public override void init()
	{
		base.init();
		this.add(new JobTesterAsset
		{
			id = "test_spawn_units"
		});
		for (int i = 0; i < 50; i++)
		{
			this.t.addTask("spawn_random_unit");
			this.t.addTask("wait_1");
		}
		this.t.addTask("super_damage_units");
		this.t.addTask("end_test");
		this.add(new JobTesterAsset
		{
			id = "test_spawn_buildings"
		});
		for (int j = 0; j < 50; j++)
		{
			this.t.addTask("spawn_random_building");
			this.t.addTask("wait_1");
		}
		this.t.addTask("end_test");
		this.add(new JobTesterAsset
		{
			id = "destroy_sim_objects"
		});
		this.t.addTask("destroy_sim_objects");
		this.t.addTask("end_test");
		this.add(new JobTesterAsset
		{
			id = "test_random_power"
		});
		for (int k = 0; k < 15; k++)
		{
			this.t.addTask("spawn_random_power");
			this.t.addTask("wait_1");
		}
		this.t.addTask("end_test");
		this.add(new JobTesterAsset
		{
			id = "test_world_change"
		});
		this.t.addTask("fill_world_water");
		this.t.addTask("wait_1");
		this.t.addTask("fill_world_randomly");
		this.t.addTask("wait_1");
		this.t.addTask("fill_world_soil");
		this.t.addTask("wait_1");
		this.t.addTask("fill_world_randomly");
		this.t.addTask("wait_1");
		this.t.addTask("end_test");
		this.add(new JobTesterAsset
		{
			id = "test_world_pit"
		});
		this.t.addTask("fill_world_pit");
		this.t.addTask("wait_10");
		this.t.addTask("end_test");
		this.add(new JobTesterAsset
		{
			id = "test_world_lava"
		});
		for (int l = 0; l < 10; l++)
		{
			this.t.addTask("fill_world_lava");
		}
		this.t.addTask("wait_5");
		this.t.addTask("end_test");
		this.add(new JobTesterAsset
		{
			id = "test_windows"
		});
		this.t.addTask("end_test");
		this.t.addTask("open_random_window");
		this.t.addTask("open_random_window");
		this.t.addTask("open_random_window");
		this.t.addTask("open_random_window");
		this.t.addTask("open_random_window");
		this.t.addTask("open_random_window");
		this.t.addTask("open_random_window");
		this.t.addTask("open_random_window");
		this.t.addTask("open_random_window");
		this.t.addTask("open_random_window");
		this.t.addTask("open_random_window");
		this.t.addTask("open_random_window");
		this.t.addTask("open_random_window");
		this.t.addTask("open_random_window");
		this.t.addTask("close_all_windows");
		this.t.addTask("end_test");
		this.add(new JobTesterAsset
		{
			id = "test_gene_editor",
			manual_test = true
		});
		this.t.addTask("unpause");
		this.t.addTask("setup_laws");
		this.t.addTask("wait_1");
		this.t.addTask("pick_random_meta_objects");
		this.t.addTask("pause");
		this.t.addTask("open_gene_editor");
		this.t.addTask("end_test");
		this.add(new JobTesterAsset
		{
			id = "test_chart_windows",
			manual_test = true
		});
		this.t.addTask("unpause");
		this.t.addTask("setup_laws");
		for (int m = 0; m < 6; m++)
		{
			this.t.addTask("try_new_city");
		}
		this.t.addTask("wait_for_cities");
		this.t.addTask("wait_years_10");
		for (int n = 0; n < 3; n++)
		{
			this.t.addTask("wait_years_5");
			this.t.addTask("pick_random_meta_objects");
			this.t.addTask("open_city_chart_window");
			this.t.addTask("pick_random_meta_objects");
			this.t.addTask("open_city_chart_window");
			this.t.addTask("wait_years_5");
			this.t.addTask("pick_random_meta_objects");
			this.t.addTask("open_kingdom_chart_window");
			this.t.addTask("pick_random_meta_objects");
			this.t.addTask("open_kingdom_chart_window");
		}
		this.t.addTask("pick_random_meta_objects_graph");
		this.t.addTask("show_compare_window");
		this.t.addTask("pause");
		this.add(new JobTesterAsset
		{
			id = "test_meta_windows",
			manual_test = true
		});
		this.t.addTask("unpause");
		this.t.addTask("setup_laws");
		for (int i2 = 0; i2 < 6; i2++)
		{
			this.t.addTask("try_new_city");
		}
		this.t.addTask("wait_for_cities");
		this.t.addTask("wait_5");
		for (int i3 = 0; i3 < 50; i3++)
		{
			for (int j2 = 0; j2 < 5; j2++)
			{
				this.t.addTask("pick_random_meta_objects");
			}
			this.t.addTask("open_random_meta_window");
			for (int j3 = 0; j3 < 20; j3++)
			{
				this.t.addTask("random_window_tab");
				this.t.addTask("random_meta_switch");
			}
			this.t.addTask("close_all_windows");
		}
		this.t.addTask("pick_random_meta_objects_graph");
		this.t.addTask("show_compare_window");
		this.t.addTask("pause");
		this.add(new JobTesterAsset
		{
			id = "test_unit_selection",
			manual_test = true
		});
		this.t.addTask("setup_laws");
		for (int j4 = 0; j4 < 10; j4++)
		{
			this.t.addTask("spawn_random_unit_5");
			for (int k2 = 0; k2 < 15; k2++)
			{
				this.t.addTask("pick_random_meta_objects");
			}
			this.t.addTask("wait_1");
		}
		this.t.addTask("cull_mobs");
		this.t.addTask("cull_units");
		this.t.addTask("cull_godfinger");
		this.t.addTask("fix_water");
		this.t.addTask("close_all_windows");
		for (int j5 = 0; j5 < 10; j5++)
		{
			this.t.addTask("spawn_random_unit_5");
			for (int k3 = 0; k3 < 15; k3++)
			{
				this.t.addTask("pick_random_meta_objects");
			}
			this.t.addTask("wait_1");
		}
		this.t.addTask("randomize_subspecies_traits");
		this.t.addTask("randomize_subspecies_genes");
		this.t.addTask("randomize_actor_traits");
		this.t.addTask("randomize_actor_status");
		this.t.addTask("restart_test");
		this.add(new JobTesterAsset
		{
			id = "test_multi_chart",
			manual_test = true
		});
		this.t.addTask("unpause");
		this.t.addTask("setup_laws");
		for (int i4 = 0; i4 < 6; i4++)
		{
			this.t.addTask("try_new_city");
		}
		this.t.addTask("wait_for_cities");
		for (int i5 = 0; i5 < 10; i5++)
		{
			this.t.addTask("pick_random_meta_objects_graph");
			this.t.addTask("show_compare_window");
			this.t.addTask("wait_years_5");
		}
		this.t.addTask("pause");
		this.add(new JobTesterAsset
		{
			id = "auto_play",
			manual_test = true
		});
		this.t.addTask("unpause");
		this.t.addTask("setup_laws");
		this.t.addTask("fix_water");
		for (int i6 = 0; i6 < 6; i6++)
		{
			this.t.addTask("try_new_city");
		}
		this.t.addTask("wait_for_cities");
		this.t.addTask("random_bombs_and_kingdoms");
		this.t.addTask("cull_godfinger");
		this.t.addTask("restart_test");
		this.add(new JobTesterAsset
		{
			id = "test_windows_tooltips",
			manual_test = true
		});
		this.t.addTask("unpause");
		this.t.addTask("setup_laws");
		this.t.addTask("fix_water");
		for (int i7 = 0; i7 < 6; i7++)
		{
			this.t.addTask("try_new_city");
		}
		this.t.addTask("wait_for_cities");
		for (int j6 = 0; j6 < 5; j6++)
		{
			this.t.addTask("spawn_random_unit_5");
		}
		this.t.addTask("cull_mobs");
		this.t.addTask("cull_units");
		this.t.addTask("cull_godfinger");
		this.t.addTask("fix_water");
		for (int j7 = 0; j7 < 5; j7++)
		{
			this.t.addTask("spawn_random_unit_5");
		}
		this.t.addTask("randomize_subspecies_traits");
		this.t.addTask("randomize_subspecies_genes");
		this.t.addTask("randomize_actor_traits");
		this.t.addTask("randomize_actor_status");
		this.t.addTask("reset_tweens");
		this.t.addTask("close_all_windows");
		this.t.addTask("prepare_window_testdata");
		this.t.addTask("open_next_window");
		this.t.addTask("test_window_and_tooltips");
		this.t.addTask("clear_window_testdata");
		this.t.addTask("close_all_windows");
		this.t.addTask("restart_test");
	}

	// Token: 0x06002A58 RID: 10840 RVA: 0x0014E306 File Offset: 0x0014C506
	public override void linkAssets()
	{
		base.linkAssets();
	}

	// Token: 0x06002A59 RID: 10841 RVA: 0x0014E310 File Offset: 0x0014C510
	public string getNextJob()
	{
		if (this.list.Count == 0)
		{
			return string.Empty;
		}
		if (this._last_job > this.list.Count - 1)
		{
			this._last_job = 0;
			this.list.Shuffle<JobTesterAsset>();
		}
		List<JobTesterAsset> list = this.list;
		int last_job = this._last_job;
		this._last_job = last_job + 1;
		JobTesterAsset tJob = list[last_job];
		if (tJob.manual_test)
		{
			return this.getNextJob();
		}
		return tJob.id;
	}

	// Token: 0x04001F83 RID: 8067
	private int _last_job;
}
