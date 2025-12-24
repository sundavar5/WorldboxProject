using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000350 RID: 848
public class StackEffects : BaseMapObject
{
	// Token: 0x06002079 RID: 8313 RVA: 0x001164E4 File Offset: 0x001146E4
	internal override void create()
	{
		base.create();
		this.dictionary = new Dictionary<string, BaseEffectController>();
		this.list = new List<BaseEffectController>();
		this.checkInit();
		this.controller_slash_effects = this.get("fx_slash");
		this.controller_tile_effects = this.get("fx_tile_effect");
	}

	// Token: 0x0600207A RID: 8314 RVA: 0x00116538 File Offset: 0x00114738
	private void checkInit()
	{
		foreach (EffectAsset tAsset in AssetManager.effects_library.list)
		{
			if (!this.dictionary.ContainsKey(tAsset.id))
			{
				this.add(tAsset);
			}
		}
	}

	// Token: 0x0600207B RID: 8315 RVA: 0x001165A4 File Offset: 0x001147A4
	internal int countActive()
	{
		int tResult = 0;
		for (int i = 0; i < this.list.Count; i++)
		{
			BaseEffectController tController = this.list[i];
			tResult += tController.getActiveIndex();
		}
		return tResult;
	}

	// Token: 0x0600207C RID: 8316 RVA: 0x001165E0 File Offset: 0x001147E0
	internal bool isLocked()
	{
		return this.get("fx_spawn_big").getActiveIndex() > 0;
	}

	// Token: 0x0600207D RID: 8317 RVA: 0x001165F8 File Offset: 0x001147F8
	internal BaseEffectController get(string pID)
	{
		return this.dictionary[pID];
	}

	// Token: 0x0600207E RID: 8318 RVA: 0x00116608 File Offset: 0x00114808
	private BaseEffectController add(EffectAsset pAsset)
	{
		string tPrefabPath;
		if (pAsset.use_basic_prefab)
		{
			tPrefabPath = "effects/prefabs/PrefabEffectBasic";
		}
		else
		{
			tPrefabPath = pAsset.prefab_id;
		}
		GameObject tPrefab = Object.Instantiate<GameObject>((GameObject)Resources.Load(tPrefabPath, typeof(GameObject)), base.transform);
		tPrefab.transform.name = "[base] " + pAsset.id;
		tPrefab.gameObject.SetActive(false);
		if (pAsset.use_basic_prefab || pAsset.load_texture)
		{
			SpriteAnimation tAnimation = tPrefab.GetComponent<SpriteAnimation>();
			tAnimation.timeBetweenFrames = pAsset.time_between_frames;
			tAnimation.frames = SpriteTextureLoader.getSpriteList(pAsset.sprite_path, false);
			if (tAnimation.frames == null || tAnimation.frames.Length == 0)
			{
				Debug.LogError("NO SPRITES FOR EFFECT " + pAsset.id + " " + pAsset.sprite_path);
			}
			tAnimation.spriteRenderer.sortingLayerName = pAsset.sorting_layer_id;
		}
		BaseEffectController tController = Object.Instantiate<BaseEffectController>(this.prefabController, base.transform, true);
		tController.create();
		tController.asset = pAsset;
		tController.transform.name = "[controller] " + pAsset.id;
		tController.prefab = tPrefab.transform;
		tController.setLimits(pAsset.limit, pAsset.limit_unload);
		this.dictionary.Add(pAsset.id, tController);
		this.list.Add(tController);
		tController.addNewObject(tPrefab.GetComponent<BaseEffect>());
		return tController;
	}

	// Token: 0x0600207F RID: 8319 RVA: 0x00116774 File Offset: 0x00114974
	internal void clear()
	{
		for (int i = 0; i < this.list.Count; i++)
		{
			this.list[i].clear();
		}
		StackEffects.last_thunder_timestamp = 0.0;
		this.light_blobs.Clear();
		this.plot_removals.Clear();
		this.actor_effect_hit.Clear();
		this.actor_effect_highlight.Clear();
	}

	// Token: 0x06002080 RID: 8320 RVA: 0x001167E4 File Offset: 0x001149E4
	public override void update(float pElapsed)
	{
		if (AssetManager.effects_library.list.Count > this.list.Count)
		{
			this.checkInit();
		}
		Bench.bench("stack_effects", "game_total", false);
		for (int i = 0; i < this.list.Count; i++)
		{
			this.list[i].update(pElapsed);
		}
		Bench.benchEnd("stack_effects", "game_total", false, 0L, false);
	}

	// Token: 0x040017A6 RID: 6054
	public static double last_thunder_timestamp;

	// Token: 0x040017A7 RID: 6055
	public BaseEffectController prefabController;

	// Token: 0x040017A8 RID: 6056
	private Dictionary<string, BaseEffectController> dictionary;

	// Token: 0x040017A9 RID: 6057
	internal List<BaseEffectController> list;

	// Token: 0x040017AA RID: 6058
	public List<LightBlobData> light_blobs = new List<LightBlobData>();

	// Token: 0x040017AB RID: 6059
	public List<PlotIconData> plot_removals = new List<PlotIconData>();

	// Token: 0x040017AC RID: 6060
	public List<ActorDamageEffectData> actor_effect_hit = new List<ActorDamageEffectData>();

	// Token: 0x040017AD RID: 6061
	public List<ActorHighlightEffectData> actor_effect_highlight = new List<ActorHighlightEffectData>();

	// Token: 0x040017AE RID: 6062
	public BaseEffectController controller_slash_effects;

	// Token: 0x040017AF RID: 6063
	public BaseEffectController controller_tile_effects;
}
