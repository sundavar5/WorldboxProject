using System;

// Token: 0x020006C1 RID: 1729
public readonly struct MinMax
{
	// Token: 0x0600376D RID: 14189 RVA: 0x00190763 File Offset: 0x0018E963
	public MinMax(long pMin, long pMax)
	{
		this.min = pMin;
		this.max = pMax;
	}

	// Token: 0x04002912 RID: 10514
	public readonly long min;

	// Token: 0x04002913 RID: 10515
	public readonly long max;
}
