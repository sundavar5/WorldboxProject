using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
public class EffectsCamera : MonoBehaviour
{
	// Token: 0x06000014 RID: 20 RVA: 0x0000338C File Offset: 0x0000158C
	private void Awake()
	{
		this._effectsCamera = base.GetComponent<Camera>();
	}

	// Token: 0x06000015 RID: 21 RVA: 0x0000339A File Offset: 0x0000159A
	private void Start()
	{
		this._mainCamera = World.world.camera;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000033AC File Offset: 0x000015AC
	private void LateUpdate()
	{
		this._effectsCamera.orthographicSize = this._mainCamera.orthographicSize;
		int tWidth = Screen.width / 3;
		int tHeight = Screen.height / 3;
		if (this.renderTexture == null || this.renderTexture.width != tWidth || this.renderTexture.height != tHeight)
		{
			this.renderTexture = new RenderTexture(tWidth, tHeight, 0);
			this.renderTexture.filterMode = FilterMode.Point;
			this.renderTexture.wrapMode = TextureWrapMode.Clamp;
			this._effectsCamera.targetTexture = this.renderTexture;
		}
	}

	// Token: 0x0400000E RID: 14
	private Camera _mainCamera;

	// Token: 0x0400000F RID: 15
	private Camera _effectsCamera;

	// Token: 0x04000010 RID: 16
	internal RenderTexture renderTexture;
}
