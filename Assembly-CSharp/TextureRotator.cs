using System;
using UnityEngine;

// Token: 0x02000492 RID: 1170
public class TextureRotator
{
	// Token: 0x060027F3 RID: 10227 RVA: 0x00141E00 File Offset: 0x00140000
	public static Texture2D Rotate(Texture2D originTexture, int angle, Color32 pDefaultColor)
	{
		Texture2D result = new Texture2D(originTexture.width, originTexture.height);
		result.name = "rotated_" + originTexture.name;
		Color32[] pix = result.GetPixels32();
		Color32[] pixels = originTexture.GetPixels32();
		int W = originTexture.width;
		int H = originTexture.height;
		int x = 0;
		int y = 0;
		Color32[] pix2 = TextureRotator.rotateSquare(pixels, 0.017453292519943295 * (double)angle, originTexture, pDefaultColor);
		for (int i = 0; i < H; i++)
		{
			for (int j = 0; j < W; j++)
			{
				pix[result.width / 2 - W / 2 + x + j + result.width * (result.height / 2 - H / 2 + i + y)] = pix2[j + i * W];
			}
		}
		result.SetPixels32(pix);
		result.Apply();
		return result;
	}

	// Token: 0x060027F4 RID: 10228 RVA: 0x00141EDC File Offset: 0x001400DC
	private static Color32[] rotateSquare(Color32[] arr, double phi, Texture2D originTexture, Color32 pDefaultColor)
	{
		double sn = Math.Sin(phi);
		double cs = Math.Cos(phi);
		Color32[] arr2 = originTexture.GetPixels32();
		int W = originTexture.width;
		int H = originTexture.height;
		int xc = W / 2;
		int yc = H / 2;
		for (int i = 0; i < H; i++)
		{
			for (int j = 0; j < W; j++)
			{
				arr2[i * W + j] = pDefaultColor;
				int x = (int)(cs * (double)(j - xc) + sn * (double)(i - yc) + (double)xc);
				int y = (int)(-sn * (double)(j - xc) + cs * (double)(i - yc) + (double)yc);
				if (x > -1 && x < W && y > -1 && y < H)
				{
					arr2[i * W + j] = arr[y * W + x];
				}
			}
		}
		return arr2;
	}
}
