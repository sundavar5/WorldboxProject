using System;
using UnityEngine;

// Token: 0x02000493 RID: 1171
public class TextureScale
{
	// Token: 0x060027F6 RID: 10230 RVA: 0x00141FA9 File Offset: 0x001401A9
	public static void Point(Texture2D tex, int newWidth, int newHeight)
	{
		TextureScale.ThreadedScale(tex, newWidth, newHeight, false);
	}

	// Token: 0x060027F7 RID: 10231 RVA: 0x00141FB4 File Offset: 0x001401B4
	public static void Bilinear(Texture2D tex, int newWidth, int newHeight)
	{
		TextureScale.ThreadedScale(tex, newWidth, newHeight, true);
	}

	// Token: 0x060027F8 RID: 10232 RVA: 0x00141FC0 File Offset: 0x001401C0
	private static void ThreadedScale(Texture2D tex, int newWidth, int newHeight, bool useBilinear)
	{
		TextureScale.texColors = tex.GetPixels();
		TextureScale.newColors = new Color[newWidth * newHeight];
		if (useBilinear)
		{
			TextureScale.ratioX = 1f / ((float)newWidth / (float)(tex.width - 1));
			TextureScale.ratioY = 1f / ((float)newHeight / (float)(tex.height - 1));
		}
		else
		{
			TextureScale.ratioX = (float)tex.width / (float)newWidth;
			TextureScale.ratioY = (float)tex.height / (float)newHeight;
		}
		TextureScale.w = tex.width;
		TextureScale.w2 = newWidth;
		int cores = Mathf.Min(SystemInfo.processorCount, newHeight);
		int num = newHeight / cores;
		TextureScale.finishCount = 0;
		TextureScale.ThreadData threadData = new TextureScale.ThreadData(0, newHeight);
		if (useBilinear)
		{
			TextureScale.BilinearScale(threadData);
		}
		else
		{
			TextureScale.PointScale(threadData);
		}
		tex.Reinitialize(newWidth, newHeight);
		tex.SetPixels(TextureScale.newColors);
		tex.Apply();
		TextureScale.texColors = null;
		TextureScale.newColors = null;
	}

	// Token: 0x060027F9 RID: 10233 RVA: 0x0014209C File Offset: 0x0014029C
	public static void BilinearScale(object obj)
	{
		TextureScale.ThreadData threadData = (TextureScale.ThreadData)obj;
		for (int y = threadData.start; y < threadData.end; y++)
		{
			int yFloor = (int)Mathf.Floor((float)y * TextureScale.ratioY);
			int y2 = yFloor * TextureScale.w;
			int y3 = (yFloor + 1) * TextureScale.w;
			int yw = y * TextureScale.w2;
			for (int x = 0; x < TextureScale.w2; x++)
			{
				int xFloor = (int)Mathf.Floor((float)x * TextureScale.ratioX);
				float xLerp = (float)x * TextureScale.ratioX - (float)xFloor;
				TextureScale.newColors[yw + x] = TextureScale.ColorLerpUnclamped(TextureScale.ColorLerpUnclamped(TextureScale.texColors[y2 + xFloor], TextureScale.texColors[y2 + xFloor + 1], xLerp), TextureScale.ColorLerpUnclamped(TextureScale.texColors[y3 + xFloor], TextureScale.texColors[y3 + xFloor + 1], xLerp), (float)y * TextureScale.ratioY - (float)yFloor);
			}
		}
		TextureScale.finishCount++;
	}

	// Token: 0x060027FA RID: 10234 RVA: 0x001421A8 File Offset: 0x001403A8
	public static void PointScale(object obj)
	{
		TextureScale.ThreadData threadData = (TextureScale.ThreadData)obj;
		for (int y = threadData.start; y < threadData.end; y++)
		{
			int thisY = (int)(TextureScale.ratioY * (float)y) * TextureScale.w;
			int yw = y * TextureScale.w2;
			for (int x = 0; x < TextureScale.w2; x++)
			{
				TextureScale.newColors[yw + x] = TextureScale.texColors[(int)((float)thisY + TextureScale.ratioX * (float)x)];
			}
		}
		TextureScale.finishCount++;
	}

	// Token: 0x060027FB RID: 10235 RVA: 0x00142230 File Offset: 0x00140430
	private static Color ColorLerpUnclamped(Color c1, Color c2, float value)
	{
		return new Color(c1.r + (c2.r - c1.r) * value, c1.g + (c2.g - c1.g) * value, c1.b + (c2.b - c1.b) * value, c1.a + (c2.a - c1.a) * value);
	}

	// Token: 0x04001E18 RID: 7704
	private static Color[] texColors;

	// Token: 0x04001E19 RID: 7705
	private static Color[] newColors;

	// Token: 0x04001E1A RID: 7706
	private static int w;

	// Token: 0x04001E1B RID: 7707
	private static float ratioX;

	// Token: 0x04001E1C RID: 7708
	private static float ratioY;

	// Token: 0x04001E1D RID: 7709
	private static int w2;

	// Token: 0x04001E1E RID: 7710
	private static int finishCount;

	// Token: 0x02000A3D RID: 2621
	public class ThreadData
	{
		// Token: 0x06004EBA RID: 20154 RVA: 0x001FCE5C File Offset: 0x001FB05C
		public ThreadData(int s, int e)
		{
			this.start = s;
			this.end = e;
		}

		// Token: 0x0400389F RID: 14495
		public int start;

		// Token: 0x040038A0 RID: 14496
		public int end;
	}
}
