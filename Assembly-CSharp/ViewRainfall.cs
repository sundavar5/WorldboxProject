using System;
using UnityEngine;

// Token: 0x02000824 RID: 2084
public class ViewRainfall : MapLayer
{
	// Token: 0x0600411D RID: 16669 RVA: 0x001BC388 File Offset: 0x001BA588
	internal override void create()
	{
		this.colorValues = new Color(0f, 0f, 1f);
		this.colors_amount = 10;
		base.create();
		this.sprRnd.color = new Color(1f, 1f, 1f, 0.6f);
	}

	// Token: 0x0600411E RID: 16670 RVA: 0x001BC3E1 File Offset: 0x001BA5E1
	public void setTileDirty(WorldTile pTile)
	{
	}

	// Token: 0x0600411F RID: 16671 RVA: 0x001BC3E3 File Offset: 0x001BA5E3
	protected override void UpdateDirty(float pElapsed)
	{
	}
}
