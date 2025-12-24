using System;
using System.Runtime.CompilerServices;

namespace db
{
	// Token: 0x0200085C RID: 2140
	public static class HistoryIntervalExtensions
	{
		// Token: 0x060042F6 RID: 17142 RVA: 0x001C6BF8 File Offset: 0x001C4DF8
		public static int getIntervalStep(this HistoryInterval pInterval)
		{
			switch (pInterval)
			{
			case HistoryInterval.Yearly1:
				return 1;
			case HistoryInterval.Yearly5:
				return 5;
			case HistoryInterval.Yearly10:
				return 10;
			case HistoryInterval.Yearly50:
				return 50;
			case HistoryInterval.Yearly100:
				return 100;
			case HistoryInterval.Yearly500:
				return 500;
			case HistoryInterval.Yearly1000:
				return 1000;
			case HistoryInterval.Yearly5000:
				return 5000;
			case HistoryInterval.Yearly10000:
				return 10000;
			default:
				throw new NotImplementedException("interval step not defined");
			}
		}

		// Token: 0x060042F7 RID: 17143 RVA: 0x001C6C64 File Offset: 0x001C4E64
		public static int getMaxTimeFrame(this HistoryInterval pInterval)
		{
			int tMaxTimeFrame = 0;
			foreach (GraphTimeAsset tAsset in AssetManager.graph_time_library.list)
			{
				if (tAsset.interval == pInterval && tMaxTimeFrame < tAsset.max_time_frame)
				{
					tMaxTimeFrame = tAsset.max_time_frame;
				}
			}
			return tMaxTimeFrame;
		}

		// Token: 0x060042F8 RID: 17144 RVA: 0x001C6CD0 File Offset: 0x001C4ED0
		[return: TupleElementNames(new string[]
		{
			"tEveryYears",
			"tFromInterval"
		})]
		public static ValueTuple<int, HistoryInterval> fillFrom(this HistoryInterval pInterval)
		{
			switch (pInterval)
			{
			case HistoryInterval.Yearly1:
				return new ValueTuple<int, HistoryInterval>(0, HistoryInterval.None);
			case HistoryInterval.Yearly5:
				return new ValueTuple<int, HistoryInterval>(5, HistoryInterval.Yearly1);
			case HistoryInterval.Yearly10:
				return new ValueTuple<int, HistoryInterval>(10, HistoryInterval.Yearly1);
			case HistoryInterval.Yearly50:
				return new ValueTuple<int, HistoryInterval>(50, HistoryInterval.Yearly10);
			case HistoryInterval.Yearly100:
				return new ValueTuple<int, HistoryInterval>(100, HistoryInterval.Yearly10);
			case HistoryInterval.Yearly500:
				return new ValueTuple<int, HistoryInterval>(500, HistoryInterval.Yearly50);
			case HistoryInterval.Yearly1000:
				return new ValueTuple<int, HistoryInterval>(1000, HistoryInterval.Yearly100);
			case HistoryInterval.Yearly5000:
				return new ValueTuple<int, HistoryInterval>(5000, HistoryInterval.Yearly500);
			case HistoryInterval.Yearly10000:
				return new ValueTuple<int, HistoryInterval>(10000, HistoryInterval.Yearly1000);
			default:
				throw new NotImplementedException("interval step not defined");
			}
		}
	}
}
