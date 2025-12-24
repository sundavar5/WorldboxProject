using System;
using UnityEngine;

// Token: 0x020001BF RID: 447
public class BaseMapObject : BaseWorldObject
{
	// Token: 0x06000CEE RID: 3310 RVA: 0x000BA2D4 File Offset: 0x000B84D4
	public override void Dispose()
	{
		this.current_tile = null;
		base.Dispose();
	}

	// Token: 0x04000C8E RID: 3214
	public WorldTile current_tile;

	// Token: 0x04000C8F RID: 3215
	public float position_height;

	// Token: 0x04000C90 RID: 3216
	public Vector2 current_position;
}
