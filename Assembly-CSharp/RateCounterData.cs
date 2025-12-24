using System;

// Token: 0x020002E7 RID: 743
internal struct RateCounterData
{
	// Token: 0x06001C13 RID: 7187 RVA: 0x0010033E File Offset: 0x000FE53E
	public RateCounterData(double pTimestamp, double pValue = 0.0)
	{
		this.timestamp = pTimestamp;
		this.value = pValue;
	}

	// Token: 0x04001574 RID: 5492
	public double timestamp;

	// Token: 0x04001575 RID: 5493
	public double value;
}
