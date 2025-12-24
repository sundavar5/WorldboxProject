using System;
using UnityEngine;

// Token: 0x020000F1 RID: 241
public struct Pixel
{
	// Token: 0x06000727 RID: 1831 RVA: 0x00069DFB File Offset: 0x00067FFB
	public Pixel(int pX, int pY, Color32 pColor)
	{
		this.x = pX;
		this.y = pY;
		this.color = pColor;
	}

	// Token: 0x040007AB RID: 1963
	public readonly int x;

	// Token: 0x040007AC RID: 1964
	public readonly int y;

	// Token: 0x040007AD RID: 1965
	public readonly Color32 color;
}
