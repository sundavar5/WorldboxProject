using System;

// Token: 0x020002F3 RID: 755
public class JobManagerBuildings : JobManagerBase<BatchBuildings, Building>
{
	// Token: 0x06001C71 RID: 7281 RVA: 0x001022CE File Offset: 0x001004CE
	public JobManagerBuildings(string pID) : base(pID)
	{
		this.benchmark_id = "buildings";
	}
}
