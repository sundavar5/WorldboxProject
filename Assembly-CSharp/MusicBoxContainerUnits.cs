using System;

// Token: 0x02000433 RID: 1075
public class MusicBoxContainerUnits
{
	// Token: 0x06002581 RID: 9601 RVA: 0x00135860 File Offset: 0x00133A60
	public void clear()
	{
		this.units = 0;
		this.enabled = false;
	}

	// Token: 0x04001C7B RID: 7291
	public MusicAsset asset;

	// Token: 0x04001C7C RID: 7292
	public int units;

	// Token: 0x04001C7D RID: 7293
	public bool enabled;
}
