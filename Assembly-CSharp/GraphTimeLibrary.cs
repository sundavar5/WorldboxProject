using System;
using db;

// Token: 0x02000048 RID: 72
public class GraphTimeLibrary : AssetLibrary<GraphTimeAsset>
{
	// Token: 0x060002F7 RID: 759 RVA: 0x0001CC08 File Offset: 0x0001AE08
	public override void init()
	{
		base.init();
		this.add(new GraphTimeAsset
		{
			scale_id = GraphTimeScale.year_10,
			max_time_frame = 10,
			interval = HistoryInterval.Yearly1
		});
		this.add(new GraphTimeAsset
		{
			scale_id = GraphTimeScale.year_25,
			max_time_frame = 25,
			interval = HistoryInterval.Yearly5
		});
		this.add(new GraphTimeAsset
		{
			scale_id = GraphTimeScale.year_100,
			max_time_frame = 100,
			interval = HistoryInterval.Yearly10
		});
		this.add(new GraphTimeAsset
		{
			scale_id = GraphTimeScale.year_250,
			max_time_frame = 250,
			interval = HistoryInterval.Yearly50
		});
		this.add(new GraphTimeAsset
		{
			scale_id = GraphTimeScale.year_500,
			max_time_frame = 500,
			interval = HistoryInterval.Yearly50
		});
		this.add(new GraphTimeAsset
		{
			scale_id = GraphTimeScale.year_1000,
			max_time_frame = 1000,
			interval = HistoryInterval.Yearly100
		});
		this.add(new GraphTimeAsset
		{
			scale_id = GraphTimeScale.year_2500,
			max_time_frame = 2500,
			interval = HistoryInterval.Yearly500
		});
		this.add(new GraphTimeAsset
		{
			scale_id = GraphTimeScale.year_5000,
			max_time_frame = 5000,
			interval = HistoryInterval.Yearly500
		});
		this.add(new GraphTimeAsset
		{
			scale_id = GraphTimeScale.year_10000,
			max_time_frame = 10000,
			interval = HistoryInterval.Yearly1000
		});
		this.add(new GraphTimeAsset
		{
			scale_id = GraphTimeScale.year_50000,
			max_time_frame = 50000,
			interval = HistoryInterval.Yearly5000
		});
		this.add(new GraphTimeAsset
		{
			scale_id = GraphTimeScale.year_100000,
			max_time_frame = 100000,
			interval = HistoryInterval.Yearly10000
		});
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x0001CDAC File Offset: 0x0001AFAC
	public static long getMinTime(GraphTimeAsset pAsset)
	{
		return (long)Date.getYear((double)((float)Date.getYearsSince(0.0) * 60f - 60f * (float)pAsset.max_time_frame));
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x0001CDD8 File Offset: 0x0001AFD8
	public static long getMaxTime(GraphTimeAsset pAsset)
	{
		return (long)Date.getYear((double)((float)Date.getYearsSince(0.0) * 60f));
	}

	// Token: 0x060002FA RID: 762 RVA: 0x0001CDF6 File Offset: 0x0001AFF6
	public override GraphTimeAsset add(GraphTimeAsset pAsset)
	{
		pAsset.id = pAsset.scale_id.ToString();
		return base.add(pAsset);
	}
}
