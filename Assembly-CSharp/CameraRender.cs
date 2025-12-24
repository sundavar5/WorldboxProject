using System;
using UnityEngine;

// Token: 0x0200044A RID: 1098
public class CameraRender : MonoBehaviour
{
	// Token: 0x06002609 RID: 9737 RVA: 0x00137EE4 File Offset: 0x001360E4
	private void Start()
	{
		this.mainRenderTexture = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32);
		this.mainRenderTexture.Create();
		this.BackgroundCamera.targetTexture = this.mainRenderTexture;
		this.MainCamera.targetTexture = this.mainRenderTexture;
	}

	// Token: 0x0600260A RID: 9738 RVA: 0x00137F37 File Offset: 0x00136137
	private void Update()
	{
	}

	// Token: 0x0600260B RID: 9739 RVA: 0x00137F39 File Offset: 0x00136139
	private void OnPostRender()
	{
		Graphics.Blit(this.mainRenderTexture, this.PostProcessMaterial);
	}

	// Token: 0x04001CCE RID: 7374
	public Material PostProcessMaterial;

	// Token: 0x04001CCF RID: 7375
	public Camera BackgroundCamera;

	// Token: 0x04001CD0 RID: 7376
	public Camera MainCamera;

	// Token: 0x04001CD1 RID: 7377
	private RenderTexture mainRenderTexture;
}
