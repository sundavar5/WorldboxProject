using System;
using UnityEngine;

// Token: 0x02000491 RID: 1169
public static class TextureExtensions
{
	// Token: 0x060027F2 RID: 10226 RVA: 0x00141D78 File Offset: 0x0013FF78
	public static Texture2D getAsReadable(this Texture2D pSourceTexture)
	{
		RenderTexture tPrevious = RenderTexture.active;
		RenderTexture tRenderTexture = RenderTexture.GetTemporary(pSourceTexture.width, pSourceTexture.height, 0, RenderTextureFormat.Default, pSourceTexture.isDataSRGB ? RenderTextureReadWrite.sRGB : RenderTextureReadWrite.Linear);
		Graphics.Blit(pSourceTexture, tRenderTexture);
		RenderTexture.active = tRenderTexture;
		Texture2D texture2D = new Texture2D(pSourceTexture.width, pSourceTexture.height);
		texture2D.ReadPixels(new Rect(0f, 0f, (float)tRenderTexture.width, (float)tRenderTexture.height), 0, 0);
		texture2D.Apply();
		RenderTexture.active = tPrevious;
		RenderTexture.ReleaseTemporary(tRenderTexture);
		return texture2D;
	}
}
