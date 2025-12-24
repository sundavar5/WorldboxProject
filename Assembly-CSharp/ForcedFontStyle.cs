using System;
using UnityEngine;

// Token: 0x0200012F RID: 303
[Serializable]
public class ForcedFontStyle
{
	// Token: 0x06000925 RID: 2341 RVA: 0x00082505 File Offset: 0x00080705
	public ForcedFontStyle(FontStyle pStyle, bool pShadow = false)
	{
		this.style = pStyle;
		this.shadow = pShadow;
	}

	// Token: 0x0400094E RID: 2382
	public FontStyle style;

	// Token: 0x0400094F RID: 2383
	public bool shadow;
}
