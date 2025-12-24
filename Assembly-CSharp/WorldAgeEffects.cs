using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001EC RID: 492
public class WorldAgeEffects : MonoBehaviour
{
	// Token: 0x06000E2A RID: 3626 RVA: 0x000C0DF0 File Offset: 0x000BEFF0
	public void Awake()
	{
		WorldAgeEffects.instance = this;
		for (int i = 0; i < base.transform.childCount; i++)
		{
			SpriteRenderer tRenderer = base.transform.GetChild(i).GetComponent<SpriteRenderer>();
			Color tColor = tRenderer.color;
			tColor.a = 0f;
			tRenderer.color = tColor;
			this.dict_effects.Add(tRenderer.name, tRenderer);
		}
	}

	// Token: 0x06000E2B RID: 3627 RVA: 0x000C0E57 File Offset: 0x000BF057
	public void update(float pElapsed)
	{
		this.fitTheCamera();
		this.updateEffects(pElapsed);
	}

	// Token: 0x06000E2C RID: 3628 RVA: 0x000C0E68 File Offset: 0x000BF068
	private void updateEffects(float pElapsed)
	{
		this.updateLayer(World.world_era.overlay_chaos, "chaos", pElapsed);
		this.updateLayer(World.world_era.overlay_moon, "moon", pElapsed);
		this.updateLayer(World.world_era.overlay_magic, "magic", pElapsed);
		this.updateLayer(World.world_era.overlay_sun, "sun", pElapsed);
		this.updateLayer(World.world_era.overlay_rain_darkness, "rain_darkness", pElapsed);
		this.updateLayer(World.world_era.overlay_winter, "winter", pElapsed);
		this.updateLayer(World.world_era.overlay_ash, "ash", pElapsed);
		this.updateLayer(World.world_era.overlay_night, "night", pElapsed);
		this.updateLayer(World.world_era.overlay_rain, "rain", pElapsed);
	}

	// Token: 0x06000E2D RID: 3629 RVA: 0x000C0F3C File Offset: 0x000BF13C
	private void updateLayer(bool pEnabled, string pID, float pElapsed)
	{
		SpriteRenderer tEffect = null;
		if (!this.dict_effects.TryGetValue(pID, out tEffect))
		{
			Debug.LogError("NO ERA EFFECT " + pID);
			return;
		}
		Color tColor = tEffect.color;
		if (this.override_night)
		{
			tColor.a = this.night_value_top;
			tEffect.color = tColor;
			return;
		}
		if (pEnabled)
		{
			float tOptionsMod = (float)PlayerConfig.getIntValue("age_overlay_effect");
			float tMax = World.world_era.era_effect_overlay_alpha * (tOptionsMod / 100f);
			tEffect.enabled = (tOptionsMod > 0f);
			if (tColor.a < tMax)
			{
				tColor.a += pElapsed * 0.2f;
				if (tColor.a > tMax)
				{
					tColor.a = tMax;
				}
				tEffect.color = tColor;
			}
			if (tColor.a > tMax)
			{
				tColor.a -= pElapsed * 0.7f;
				if (tColor.a < tMax)
				{
					tColor.a = tMax;
				}
				tEffect.color = tColor;
			}
		}
		if (!pEnabled && tEffect.enabled && tColor.a > 0f)
		{
			tColor.a -= pElapsed * 0.2f;
			if (tColor.a <= 0f)
			{
				tEffect.enabled = false;
			}
			tEffect.color = tColor;
		}
	}

	// Token: 0x06000E2E RID: 3630 RVA: 0x000C1070 File Offset: 0x000BF270
	private void fitTheCamera()
	{
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		float tWorldScreenHeight = World.world.camera.orthographicSize * 2f;
		float tWorldScreenWidth = tWorldScreenHeight / (float)Screen.height * (float)Screen.width;
		base.transform.localScale = new Vector3(tWorldScreenWidth, tWorldScreenHeight);
	}

	// Token: 0x04000EB3 RID: 3763
	internal Dictionary<string, SpriteRenderer> dict_effects = new Dictionary<string, SpriteRenderer>();

	// Token: 0x04000EB4 RID: 3764
	public static WorldAgeEffects instance;

	// Token: 0x04000EB5 RID: 3765
	public bool override_night;

	// Token: 0x04000EB6 RID: 3766
	[Range(0f, 1f)]
	public float night_value_top;

	// Token: 0x04000EB7 RID: 3767
	[Range(0f, 1f)]
	public float night_value_mat;
}
