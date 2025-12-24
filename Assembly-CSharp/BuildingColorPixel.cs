using System;
using UnityEngine;

// Token: 0x02000238 RID: 568
public readonly struct BuildingColorPixel
{
	// Token: 0x060015D9 RID: 5593 RVA: 0x000E07C4 File Offset: 0x000DE9C4
	public BuildingColorPixel(Color32 pColor, Color32 pColorAbandoned, Color32 pColorRuin)
	{
		this.color = pColor;
		this.color_abandoned = pColorAbandoned;
		this.color_ruin = pColorRuin;
	}

	// Token: 0x0400123A RID: 4666
	public readonly Color32 color;

	// Token: 0x0400123B RID: 4667
	public readonly Color32 color_abandoned;

	// Token: 0x0400123C RID: 4668
	public readonly Color32 color_ruin;
}
