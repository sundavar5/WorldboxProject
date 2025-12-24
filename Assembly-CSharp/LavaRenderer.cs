using System;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000344 RID: 836
public class LavaRenderer : MonoBehaviour
{
	// Token: 0x0600203F RID: 8255 RVA: 0x00114CF4 File Offset: 0x00112EF4
	private void Start()
	{
		this.renderTexture = new RenderTexture(Screen.width, Screen.height, 8, RenderTextureFormat.ARGB32);
		this.renderTexture.dimension = TextureDimension.Tex2D;
		this.renderTexture.antiAliasing = 1;
		this.renderTexture.anisoLevel = 0;
		this.renderTexture.filterMode = FilterMode.Point;
		this.renderTexture.Create();
		this.curCamera.targetTexture = this.renderTexture;
	}

	// Token: 0x06002040 RID: 8256 RVA: 0x00114D65 File Offset: 0x00112F65
	private void OnPreRender()
	{
		this.targetCamera.targetTexture = this.renderTexture;
	}

	// Token: 0x06002041 RID: 8257 RVA: 0x00114D78 File Offset: 0x00112F78
	private void OnPostRender()
	{
		this.targetCamera.targetTexture = null;
		Graphics.DrawTexture(new Rect(0f, 0f, (float)(Screen.width / 2), (float)(Screen.height / 2)), this.renderTexture, null);
	}

	// Token: 0x0400176F RID: 5999
	public Camera curCamera;

	// Token: 0x04001770 RID: 6000
	public Camera targetCamera;

	// Token: 0x04001771 RID: 6001
	private RenderTexture renderTexture;
}
