using System;

// Token: 0x02000431 RID: 1073
public class MusicBoxContainerCivs
{
	// Token: 0x0600257B RID: 9595 RVA: 0x001356F5 File Offset: 0x001338F5
	public void clear()
	{
		this.buildings = 0;
		this.kingdom_exists = false;
		this.active = false;
	}

	// Token: 0x04001C70 RID: 7280
	public MusicAsset asset;

	// Token: 0x04001C71 RID: 7281
	public int buildings;

	// Token: 0x04001C72 RID: 7282
	public bool kingdom_exists;

	// Token: 0x04001C73 RID: 7283
	public bool active;
}
