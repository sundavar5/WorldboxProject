using System;

// Token: 0x020003EA RID: 1002
[Serializable]
public class BehaviourElementAI : Asset
{
	// Token: 0x060022DF RID: 8927 RVA: 0x001233E2 File Offset: 0x001215E2
	public override void create()
	{
		base.create();
		this.rate_counter_calls = new RateCounter("calls", 1);
		this.rate_counter_performance = new RateCounter("performance", 1);
	}

	// Token: 0x040018FF RID: 6399
	[NonSerialized]
	internal RateCounter rate_counter_calls;

	// Token: 0x04001900 RID: 6400
	[NonSerialized]
	internal RateCounter rate_counter_performance;
}
