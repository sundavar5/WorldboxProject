using System;

// Token: 0x02000316 RID: 790
public struct ZoneMetaData
{
	// Token: 0x06001D92 RID: 7570 RVA: 0x00107C06 File Offset: 0x00105E06
	public float getDiffTime()
	{
		return this.getDiffTime(World.world.getCurWorldTime());
	}

	// Token: 0x06001D93 RID: 7571 RVA: 0x00107C18 File Offset: 0x00105E18
	public float getDiffTime(double pWorldTime)
	{
		return (float)(pWorldTime - this.timestamp);
	}

	// Token: 0x0400161F RID: 5663
	public double timestamp;

	// Token: 0x04001620 RID: 5664
	public double timestamp_new;

	// Token: 0x04001621 RID: 5665
	public IMetaObject meta_object;

	// Token: 0x04001622 RID: 5666
	public int previous_priority_amount;

	// Token: 0x04001623 RID: 5667
	public TileZone zone;
}
