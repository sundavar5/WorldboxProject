using System;
using UnityEngine;

// Token: 0x0200047F RID: 1151
public static class RectExtensions
{
	// Token: 0x0600278B RID: 10123 RVA: 0x001400C0 File Offset: 0x0013E2C0
	public static Rect Resize(this Rect pRect, float pMultiplier)
	{
		float tNewWidth = pRect.width * pMultiplier;
		float tNewHeight = pRect.height * pMultiplier;
		float tOffsetX = (pRect.width - tNewWidth) / 2f;
		float tOffsetY = (pRect.height - tNewHeight) / 2f;
		pRect.width = tNewWidth;
		pRect.height = tNewHeight;
		pRect.x += tOffsetX;
		pRect.y += tOffsetY;
		return pRect;
	}
}
