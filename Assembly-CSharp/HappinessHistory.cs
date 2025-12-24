using System;

// Token: 0x0200036B RID: 875
public struct HappinessHistory
{
	// Token: 0x170001ED RID: 493
	// (get) Token: 0x06002126 RID: 8486 RVA: 0x0011B11F File Offset: 0x0011931F
	public HappinessAsset asset
	{
		get
		{
			return AssetManager.happiness_library.list[this.index];
		}
	}

	// Token: 0x06002127 RID: 8487 RVA: 0x0011B136 File Offset: 0x00119336
	public string getAgoString()
	{
		return Date.getAgoString(this.timestamp);
	}

	// Token: 0x06002128 RID: 8488 RVA: 0x0011B143 File Offset: 0x00119343
	public double elapsedSince()
	{
		return (double)World.world.getWorldTimeElapsedSince(this.timestamp);
	}

	// Token: 0x0400188F RID: 6287
	public int index;

	// Token: 0x04001890 RID: 6288
	public double timestamp;

	// Token: 0x04001891 RID: 6289
	public int bonus;
}
