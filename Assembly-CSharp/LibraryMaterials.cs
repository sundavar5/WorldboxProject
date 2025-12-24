using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001D4 RID: 468
public class LibraryMaterials : MonoBehaviour
{
	// Token: 0x06000DBD RID: 3517 RVA: 0x000BDD04 File Offset: 0x000BBF04
	private void Awake()
	{
		LibraryMaterials.instance = this;
		this.mat_damaged = this.loadMaterial("materials/damaged", true);
		this.mat_highlighted = this.loadMaterial("materials/highlighted", true);
		this.mat_buildings = this.loadMaterial("materials/building", true);
		this.mat_tree = this.loadMaterial("materials/tree", true);
		this.mat_socialize = this.loadMaterial("materials/socialize", false);
		this.mat_minis = this.loadMaterial("materials/minis", false);
		this.mat_tree_celestial = this.loadMaterial("materials/tree_celestial", true);
		this.mat_jelly = this.loadMaterial("materials/jelly", true);
		this.mat_overlapped_shadows = this.loadMaterial("materials/OverlappedShadows", true);
		this.mat_buildings_light = this.loadMaterial("materials/MatBuildingsLight", false);
		this.mat_world_object = this.loadMaterial("materials/mat_world_object", false);
		this.mat_world_object_lit = this.loadMaterial("materials/mat_world_object_lit", false);
		this.mat_lava_glow = this.loadMaterial("materials/lava_glow", true);
		this._night_affected_colors.Add(this.mat_buildings);
		this._night_affected_colors.Add(this.mat_tree);
		this._night_affected_colors.Add(this.mat_jelly);
		this._night_affected_colors.Add(this.mat_world_object);
		this._shadows_color = this.mat_overlapped_shadows.GetColor("_Color");
		AssetManager.status.linkMaterials();
		Shader.SetGlobalFloat("GlobalTime", 1f);
	}

	// Token: 0x06000DBE RID: 3518 RVA: 0x000BDE74 File Offset: 0x000BC074
	private Material loadMaterial(string pPath, bool pCopy = false)
	{
		Material tMat = Resources.Load<Material>(pPath);
		if (pCopy)
		{
			tMat = Object.Instantiate<Material>(tMat);
		}
		string tNameId = tMat.name;
		tNameId = tNameId.Replace("(Clone)", "");
		this.dict.Add(tNameId, tMat);
		return tMat;
	}

	// Token: 0x06000DBF RID: 3519 RVA: 0x000BDEB8 File Offset: 0x000BC0B8
	internal void updateMat()
	{
		if (!World.world.isPaused())
		{
			this._time += World.world.elapsed;
		}
		this.updateNight();
		Shader.SetGlobalFloat("GlobalTime", this._time);
	}

	// Token: 0x06000DC0 RID: 3520 RVA: 0x000BDEF4 File Offset: 0x000BC0F4
	private void updateNight()
	{
		float tNightMod = World.world.era_manager.getNightMod();
		Color tColor = Toolbox.blendColor(Toolbox.color_night, Toolbox.color_white, tNightMod);
		foreach (Material material in this._night_affected_colors)
		{
			material.color = tColor;
		}
		Color tColorOcean = Toolbox.blendColor(Toolbox.color_night, Toolbox.color_ocean, tNightMod);
		if (tNightMod > 0f)
		{
			tColorOcean.r -= 0.007843138f;
			tColorOcean.b -= 0.019607844f;
		}
		World.world.camera.backgroundColor = tColorOcean;
	}

	// Token: 0x06000DC1 RID: 3521 RVA: 0x000BDFC0 File Offset: 0x000BC1C0
	public void updateZoomoutValue(float pValue)
	{
		float tShaderVar = 4f - pValue * 3f;
		if (!DebugConfig.isOn(DebugOption.ScaleEffectEnabled))
		{
			tShaderVar = 1f;
		}
		this._shadow_alpha = tShaderVar * this._shadow_alpha_target;
		this._shadows_color.a = this._shadow_alpha;
		this.mat_overlapped_shadows.SetColor("_Color", this._shadows_color);
	}

	// Token: 0x04000E05 RID: 3589
	public static LibraryMaterials instance;

	// Token: 0x04000E06 RID: 3590
	public const string mat_id_world_object = "mat_world_object";

	// Token: 0x04000E07 RID: 3591
	public const string mat_id_world_object_lit_always = "mat_world_object_lit";

	// Token: 0x04000E08 RID: 3592
	public Dictionary<string, Material> dict = new Dictionary<string, Material>();

	// Token: 0x04000E09 RID: 3593
	public Material mat_damaged;

	// Token: 0x04000E0A RID: 3594
	public Material mat_highlighted;

	// Token: 0x04000E0B RID: 3595
	public Material mat_world_object;

	// Token: 0x04000E0C RID: 3596
	public Material mat_world_object_lit;

	// Token: 0x04000E0D RID: 3597
	public Material mat_buildings;

	// Token: 0x04000E0E RID: 3598
	public Material mat_socialize;

	// Token: 0x04000E0F RID: 3599
	public Material mat_minis;

	// Token: 0x04000E10 RID: 3600
	public Material mat_tree;

	// Token: 0x04000E11 RID: 3601
	public Material mat_tree_celestial;

	// Token: 0x04000E12 RID: 3602
	public Material mat_jelly;

	// Token: 0x04000E13 RID: 3603
	public Material mat_buildings_light;

	// Token: 0x04000E14 RID: 3604
	public Material mat_lava_glow;

	// Token: 0x04000E15 RID: 3605
	public Material mat_overlapped_shadows;

	// Token: 0x04000E16 RID: 3606
	private float _shadow_alpha_target = 0.40392157f;

	// Token: 0x04000E17 RID: 3607
	private float _shadow_alpha = 0.40392157f;

	// Token: 0x04000E18 RID: 3608
	private Color _shadows_color;

	// Token: 0x04000E19 RID: 3609
	private List<Material> _night_affected_colors = new List<Material>();

	// Token: 0x04000E1A RID: 3610
	private float _time;
}
