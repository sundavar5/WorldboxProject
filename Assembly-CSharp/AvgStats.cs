using System;

// Token: 0x020006AF RID: 1711
public readonly struct AvgStats
{
	// Token: 0x060036C6 RID: 14022 RVA: 0x0018CE4F File Offset: 0x0018B04F
	public AvgStats(double pAvg, int pCount, string pName)
	{
		this.avg = pAvg;
		this.count = pCount;
		this.name = pName;
	}

	// Token: 0x060036C7 RID: 14023 RVA: 0x0018CE68 File Offset: 0x0018B068
	public AvgStats add(double pValue)
	{
		double pAvg = (this.avg * (double)this.count + pValue) / (double)(this.count + 1);
		int tCount = this.count + 1;
		return new AvgStats(pAvg, tCount, this.name);
	}

	// Token: 0x0400288C RID: 10380
	public readonly double avg;

	// Token: 0x0400288D RID: 10381
	public readonly int count;

	// Token: 0x0400288E RID: 10382
	public readonly string name;
}
