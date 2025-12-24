using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200000B RID: 11
public class LightRenderer : MonoBehaviour
{
	// Token: 0x06000018 RID: 24 RVA: 0x00003448 File Offset: 0x00001648
	private void Awake()
	{
		LightRenderer.instance = this;
		this._rawImage = base.GetComponent<RawImage>();
	}

	// Token: 0x06000019 RID: 25 RVA: 0x0000345C File Offset: 0x0000165C
	public void update(float pElapsed)
	{
		this._rawImage.texture = this.effectsCamera.renderTexture;
		Color tColor = World.world_era.light_color;
		tColor.a = World.world.era_manager.getNightMod() * 0.6f;
		this._rawImage.color = tColor;
	}

	// Token: 0x04000011 RID: 17
	public static LightRenderer instance;

	// Token: 0x04000012 RID: 18
	public Camera camera;

	// Token: 0x04000013 RID: 19
	public EffectsCamera effectsCamera;

	// Token: 0x04000014 RID: 20
	private RawImage _rawImage;
}
