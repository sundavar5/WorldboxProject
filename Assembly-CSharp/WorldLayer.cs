using System;

// Token: 0x020001EE RID: 494
public class WorldLayer : MapLayer
{
	// Token: 0x06000E50 RID: 3664 RVA: 0x000C1784 File Offset: 0x000BF984
	public override void update(float pElapsed)
	{
	}

	// Token: 0x06000E51 RID: 3665 RVA: 0x000C1786 File Offset: 0x000BF986
	public override void draw(float pElapsed)
	{
		this.UpdateDirty(pElapsed);
	}
}
