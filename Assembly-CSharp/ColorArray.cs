using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000336 RID: 822
public class ColorArray
{
	// Token: 0x06001FE8 RID: 8168 RVA: 0x00112BBC File Offset: 0x00110DBC
	public ColorArray(float pR, float pG, float pB, float pA, float pAmount, float pMod = 1f)
	{
		this.colors = new List<Color32>();
		int i = 0;
		while ((float)i < pAmount)
		{
			float tVal;
			if (i > 0)
			{
				tVal = 1f / pAmount * (float)i;
			}
			else
			{
				tVal = 0f;
			}
			Color tColor = new Color(pR, pG, pB, tVal * 1f * pMod);
			this.colors.Add(tColor);
			i++;
		}
	}

	// Token: 0x06001FE9 RID: 8169 RVA: 0x00112C26 File Offset: 0x00110E26
	public ColorArray(Color32 pColor, int pAmount) : this((float)pColor.r, (float)pColor.g, (float)pColor.b, (float)pColor.a, (float)pAmount, 1f)
	{
	}

	// Token: 0x0400173E RID: 5950
	public List<Color32> colors;
}
