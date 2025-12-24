using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200005B RID: 91
public class MapGenTemplateLibrary : AssetLibrary<MapGenTemplate>
{
	// Token: 0x06000358 RID: 856 RVA: 0x0001E6CC File Offset: 0x0001C8CC
	public override void init()
	{
		base.init();
		this.add(new MapGenTemplate
		{
			id = "continent",
			freeze_mountains = true,
			path_icon = "ui/new_world_templates_icons/template_continent"
		});
		this.t.values.main_perlin_noise_stage = true;
		this.t.values.perlin_noise_stage_2 = true;
		this.t.values.perlin_noise_stage_3 = true;
		this.t.values.gradient_round_edges = true;
		this.t.values.add_center_gradient_land = true;
		this.t.values.ring_effect = true;
		this.t.values.random_shapes_amount = 5;
		this.newReplaceContainer(15);
		this.addReplaceOption(170, "soil_high", "soil_low");
		this.add(new MapGenTemplate
		{
			id = "box_world",
			freeze_mountains = true,
			path_icon = "ui/new_world_templates_icons/template_box_world"
		});
		this.t.values.main_perlin_noise_stage = true;
		this.t.values.perlin_noise_stage_2 = true;
		this.t.values.perlin_noise_stage_3 = true;
		this.t.values.add_mountain_edges = true;
		this.t.values.add_center_gradient_land = true;
		this.t.values.ring_effect = true;
		this.t.values.random_shapes_amount = 5;
		this.newReplaceContainer(15);
		this.addReplaceOption(170, "soil_high", "soil_low");
		this.add(new MapGenTemplate
		{
			id = "islands",
			freeze_mountains = true,
			path_icon = "ui/new_world_templates_icons/template_islands"
		});
		this.t.values.main_perlin_noise_stage = true;
		this.t.values.perlin_noise_stage_2 = true;
		this.t.values.perlin_noise_stage_3 = true;
		this.t.values.gradient_round_edges = true;
		this.t.values.ring_effect = true;
		this.t.values.random_shapes_amount = 5;
		this.newReplaceContainer(15);
		this.addReplaceOption(170, "soil_high", "soil_low");
		this.add(new MapGenTemplate
		{
			id = "toast",
			path_icon = "ui/new_world_templates_icons/template_toast"
		});
		this.t.values.square_edges = true;
		this.t.values.add_center_gradient_land = true;
		this.t.values.main_perlin_noise_stage = true;
		this.t.values.perlin_noise_stage_2 = true;
		this.t.values.perlin_noise_stage_3 = true;
		this.t.values.remove_mountains = true;
		this.t.values.random_shapes_amount = 5;
		this.newReplaceContainer(15);
		this.addReplaceOption(170, "soil_high", "soil_low");
		this.add(new MapGenTemplate
		{
			id = "pancake",
			path_icon = "ui/new_world_templates_icons/template_pancake"
		});
		this.t.values.gradient_round_edges = true;
		this.t.values.add_center_gradient_land = true;
		this.t.values.main_perlin_noise_stage = true;
		this.t.values.perlin_noise_stage_2 = true;
		this.t.values.perlin_noise_stage_3 = true;
		this.t.values.remove_mountains = true;
		this.newReplaceContainer(15);
		this.addReplaceOption(170, "soil_high", "soil_low");
		this.add(new MapGenTemplate
		{
			id = "boring_plains",
			path_icon = "ui/new_world_templates_icons/template_boring_plains"
		});
		this.t.values.add_center_gradient_land = true;
		this.t.values.main_perlin_noise_stage = true;
		this.t.values.perlin_noise_stage_2 = true;
		this.t.values.perlin_noise_stage_3 = true;
		this.t.values.add_mountain_edges = true;
		this.t.values.remove_mountains = true;
		this.t.allow_edit_low_ground = false;
		this.t.allow_edit_high_ground = false;
		this.newReplaceContainer(15);
		this.addReplaceOption(170, "soil_high", "soil_low");
		this.addReplaceOption(0, "shallow_waters", "soil_low");
		this.addReplaceOption(0, "close_ocean", "soil_low");
		this.addReplaceOption(0, "deep_ocean", "soil_high");
		this.add(new MapGenTemplate
		{
			id = "checkerboard",
			special_checkerboard = true,
			path_icon = "ui/new_world_templates_icons/template_checkerboard"
		});
		this.t.values.random_biomes = true;
		this.t.values.add_mountain_edges = true;
		this.t.values.remove_mountains = true;
		this.disableNormalSettings(this.t);
		this.t.allow_edit_random_biomes = true;
		this.t.allow_edit_random_resources = true;
		this.t.allow_edit_random_vegetation = true;
		this.add(new MapGenTemplate
		{
			id = "cubicles",
			special_cubicles = true,
			allow_edit_cubicles = true,
			path_icon = "ui/new_world_templates_icons/template_cubicles"
		});
		this.t.values.random_biomes = true;
		this.t.values.add_mountain_edges = true;
		this.t.values.remove_mountains = true;
		this.t.values.cubicle_size = 2;
		this.disableNormalSettings(this.t);
		this.t.allow_edit_cubicles = true;
		this.t.allow_edit_random_biomes = true;
		this.t.allow_edit_random_resources = true;
		this.t.allow_edit_random_vegetation = true;
		this.add(new MapGenTemplate
		{
			id = "dormant_volcano",
			force_height_to = 125,
			path_icon = "ui/new_world_templates_icons/template_dormant_volcano"
		});
		this.t.values.perlin_noise_stage_2 = true;
		this.t.values.random_biomes = false;
		this.t.values.gradient_round_edges = true;
		this.t.values.add_center_gradient_land = true;
		this.newReplaceContainer(15);
		this.addReplaceOption(170, "soil_high", "soil_low");
		this.add(new MapGenTemplate
		{
			id = "cheese",
			force_height_to = 120,
			path_icon = "ui/new_world_templates_icons/template_cheese"
		});
		this.t.values.square_edges = true;
		this.t.values.perlin_noise_stage_2 = true;
		this.t.values.remove_mountains = true;
		this.t.values.add_center_gradient_land = true;
		this.newReplaceContainer(15);
		this.addReplaceOption(170, "soil_high", "shallow_waters");
		this.addReplaceOption(170, "sand", "shallow_waters");
		this.addReplaceOption(150, "soil_high", "sand");
		this.addReplaceOption(130, "soil_low", "sand");
		this.addReplaceOption(80, "soil_high", "soil_low");
		this.add(new MapGenTemplate
		{
			id = "bad_apple",
			path_icon = "ui/new_world_templates_icons/template_bad_apple"
		});
		this.t.values.gradient_round_edges = true;
		this.t.values.add_center_gradient_land = true;
		this.t.values.center_gradient_mountains = true;
		this.t.values.main_perlin_noise_stage = true;
		this.newReplaceContainer(15);
		this.addReplaceOption(170, "soil_high", "soil_low");
		this.add(new MapGenTemplate
		{
			id = "donut",
			force_height_to = 125,
			path_icon = "ui/new_world_templates_icons/template_donut"
		});
		this.t.values.gradient_round_edges = true;
		this.t.values.add_center_gradient_land = true;
		this.t.values.add_center_lake = true;
		this.t.values.perlin_noise_stage_2 = true;
		this.newReplaceContainer(15);
		this.addReplaceOption(170, "soil_high", "soil_low");
		this.add(new MapGenTemplate
		{
			id = "lasagna",
			path_icon = "ui/new_world_templates_icons/template_lasagna"
		});
		this.t.values.square_edges = true;
		this.t.values.add_center_gradient_land = true;
		this.t.values.main_perlin_noise_stage = true;
		this.t.values.random_shapes_amount = 5;
		this.t.values.low_ground = true;
		this.newReplaceContainer(15);
		this.addReplaceOption(170, "soil_high", "soil_low");
		this.add(new MapGenTemplate
		{
			id = "chaos_pearl",
			path_icon = "ui/new_world_templates_icons/template_chaos_pearl"
		});
		this.t.values.gradient_round_edges = true;
		this.t.values.add_center_gradient_land = true;
		this.t.values.main_perlin_noise_stage = true;
		this.t.values.low_ground = true;
		this.newReplaceContainer(15);
		this.addReplaceOption(170, "soil_high", "soil_low");
		this.add(new MapGenTemplate
		{
			id = "anthill",
			special_anthill = true,
			path_icon = "ui/new_world_templates_icons/template_anthill"
		});
		this.disableNormalSettings(this.t);
		this.t.allow_edit_random_biomes = true;
		this.t.allow_edit_random_resources = true;
		this.t.allow_edit_random_vegetation = true;
		this.newReplaceContainer(15);
		this.addReplaceOption(170, "soil_high", "soil_low");
		this.add(new MapGenTemplate
		{
			id = "empty",
			show_reset_button = false,
			path_icon = "ui/new_world_templates_icons/template_empty"
		});
		this.disableNormalSettings(this.t);
		this.createDefaultBackupValues();
	}

	// Token: 0x06000359 RID: 857 RVA: 0x0001F0CC File Offset: 0x0001D2CC
	public void createDefaultBackupValues()
	{
		foreach (MapGenTemplate tTemplate in this.list)
		{
			MapGenValues tValuesBackup = JsonUtility.FromJson<MapGenValues>(JsonUtility.ToJson(tTemplate.values));
			this.default_values.Add(tTemplate.id, tValuesBackup);
		}
	}

	// Token: 0x0600035A RID: 858 RVA: 0x0001F13C File Offset: 0x0001D33C
	public void resetTemplateValues(MapGenTemplate pAsset)
	{
		MapGenValues tFreshValues = JsonUtility.FromJson<MapGenValues>(JsonUtility.ToJson(this.default_values[pAsset.id]));
		pAsset.values = tFreshValues;
	}

	// Token: 0x0600035B RID: 859 RVA: 0x0001F16C File Offset: 0x0001D36C
	public void disableNormalSettings(MapGenTemplate pAsset)
	{
		pAsset.allow_edit_size = false;
		pAsset.allow_edit_random_shapes = false;
		pAsset.allow_edit_random_biomes = false;
		pAsset.allow_edit_perlin_scale_stage_1 = false;
		pAsset.allow_edit_perlin_scale_stage_2 = false;
		pAsset.allow_edit_perlin_scale_stage_3 = false;
		pAsset.allow_edit_mountain_edges = false;
		pAsset.allow_edit_random_vegetation = false;
		pAsset.allow_edit_random_resources = false;
		pAsset.allow_edit_center_lake = false;
		pAsset.allow_edit_round_edges = false;
		pAsset.allow_edit_square_edges = false;
		pAsset.allow_edit_ring_effect = false;
		pAsset.allow_edit_center_land = false;
		pAsset.allow_edit_low_ground = false;
		pAsset.allow_edit_high_ground = false;
		pAsset.allow_edit_remove_mountains = false;
	}

	// Token: 0x0600035C RID: 860 RVA: 0x0001F1F0 File Offset: 0x0001D3F0
	private void newReplaceContainer(int pScale = 1)
	{
		this.t.perlin_replace.Add(new PerlinReplaceContainer
		{
			scale = pScale
		});
	}

	// Token: 0x0600035D RID: 861 RVA: 0x0001F210 File Offset: 0x0001D410
	private void addReplaceOption(int pHeight, string pFrom, string pTo)
	{
		this.t.perlin_replace[this.t.perlin_replace.Count - 1].options.Add(new PerlinReplaceOption
		{
			replace_height_value = pHeight,
			from = pFrom,
			to = pTo
		});
	}

	// Token: 0x0600035E RID: 862 RVA: 0x0001F264 File Offset: 0x0001D464
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (MapGenTemplate tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
			this.checkLocale(tAsset, tAsset.getDescriptionID());
		}
	}

	// Token: 0x040002EE RID: 750
	private Dictionary<string, MapGenValues> default_values = new Dictionary<string, MapGenValues>();
}
