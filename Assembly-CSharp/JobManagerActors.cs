using System;
using System.Collections.Generic;

// Token: 0x020002F2 RID: 754
public class JobManagerActors : JobManagerBase<BatchActors, Actor>
{
	// Token: 0x06001C6F RID: 7279 RVA: 0x001022B2 File Offset: 0x001004B2
	public JobManagerActors(string pID) : base(pID)
	{
		this.benchmark_id = "actors";
	}

	// Token: 0x170001BD RID: 445
	// (get) Token: 0x06001C70 RID: 7280 RVA: 0x001022C6 File Offset: 0x001004C6
	public List<BatchActors> active_batches
	{
		get
		{
			return this._batches_active;
		}
	}
}
