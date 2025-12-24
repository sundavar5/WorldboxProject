using System;
using db;

// Token: 0x02000047 RID: 71
[Serializable]
public class GraphTimeAsset : Asset
{
	// Token: 0x040002A7 RID: 679
	public GraphTimeScale scale_id;

	// Token: 0x040002A8 RID: 680
	public HistoryInterval interval;

	// Token: 0x040002A9 RID: 681
	public int max_time_frame;
}
