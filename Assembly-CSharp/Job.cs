using System;

// Token: 0x020002F5 RID: 757
public class Job<T>
{
	// Token: 0x040015C6 RID: 5574
	public JobUpdater job_updater;

	// Token: 0x040015C7 RID: 5575
	public ObjectContainer<T> container;

	// Token: 0x040015C8 RID: 5576
	public string id;

	// Token: 0x040015C9 RID: 5577
	public double time_benchmark;

	// Token: 0x040015CA RID: 5578
	public int counter;

	// Token: 0x040015CB RID: 5579
	public int random_tick_skips;

	// Token: 0x040015CC RID: 5580
	public int current_skips;
}
