using System;
using UnityEngine;

// Token: 0x020002FF RID: 767
public class DebugHighlightContainer
{
	// Token: 0x06001D1C RID: 7452 RVA: 0x00104FFA File Offset: 0x001031FA
	public void setTimer(float pVal)
	{
		this.interval = pVal;
		this.timer = pVal;
	}

	// Token: 0x040015F5 RID: 5621
	public Color color;

	// Token: 0x040015F6 RID: 5622
	public float timer = 0.2f;

	// Token: 0x040015F7 RID: 5623
	public float interval = 0.2f;

	// Token: 0x040015F8 RID: 5624
	public TileZone zone;

	// Token: 0x040015F9 RID: 5625
	public MapChunk chunk;

	// Token: 0x040015FA RID: 5626
	public WorldTile tile;
}
