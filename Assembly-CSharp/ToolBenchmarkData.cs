using System;
using System.Collections.Generic;

// Token: 0x0200050E RID: 1294
public class ToolBenchmarkData
{
	// Token: 0x06002AA8 RID: 10920 RVA: 0x0015408B File Offset: 0x0015228B
	public void newValue(int pValue)
	{
		this.debug_value = (long)pValue;
	}

	// Token: 0x06002AA9 RID: 10921 RVA: 0x00154095 File Offset: 0x00152295
	public void newCount(long pValue)
	{
		this.call_count += pValue;
	}

	// Token: 0x06002AAA RID: 10922 RVA: 0x001540A5 File Offset: 0x001522A5
	public void saveLastMaxValue(double pValue)
	{
		if (pValue > this.last_max_value || this.max_value_ticks <= 0)
		{
			this.last_max_value = pValue;
			this.max_value_ticks = 200;
		}
		if (this.max_value_ticks > 0)
		{
			this.max_value_ticks--;
		}
	}

	// Token: 0x06002AAB RID: 10923 RVA: 0x001540E4 File Offset: 0x001522E4
	public long getAverageCount()
	{
		if (this.results_counters.Count == 0)
		{
			return 0L;
		}
		long tResult = 0L;
		foreach (long tVal in this.results_counters)
		{
			tResult += tVal;
		}
		return tResult / (long)this.results_counters.Count;
	}

	// Token: 0x06002AAC RID: 10924 RVA: 0x00154158 File Offset: 0x00152358
	public long getLastCount()
	{
		if (this.results_counters.Count == 0)
		{
			return 0L;
		}
		return this.results_counters.Peek();
	}

	// Token: 0x06002AAD RID: 10925 RVA: 0x00154175 File Offset: 0x00152375
	public void saveAverageCounter()
	{
		if (this.results_counters.Count > 64)
		{
			this.results_counters.Dequeue();
		}
		this.results_counters.Enqueue(this.call_count);
		this.call_count = 0L;
	}

	// Token: 0x06002AAE RID: 10926 RVA: 0x001541AB File Offset: 0x001523AB
	public void start(double pTime)
	{
		this.latest_time = pTime;
	}

	// Token: 0x06002AAF RID: 10927 RVA: 0x001541B4 File Offset: 0x001523B4
	public void end(double pTime)
	{
		this.latest_result = pTime;
		if (this.results.Count > 64)
		{
			this.results.Dequeue();
		}
		this.results.Enqueue(pTime);
	}

	// Token: 0x06002AB0 RID: 10928 RVA: 0x001541E4 File Offset: 0x001523E4
	public double getAverage()
	{
		double tResult = 0.0;
		foreach (double tVal in this.results)
		{
			tResult += tVal;
		}
		return tResult / (double)this.results.Count;
	}

	// Token: 0x06002AB1 RID: 10929 RVA: 0x0015424C File Offset: 0x0015244C
	public void setResult(double pTime)
	{
		this.latest_time = pTime;
		this.latest_result = pTime;
	}

	// Token: 0x04001FFA RID: 8186
	public string id;

	// Token: 0x04001FFB RID: 8187
	private const int MAXIMUM_VALUES = 64;

	// Token: 0x04001FFC RID: 8188
	private Queue<double> results = new Queue<double>(64);

	// Token: 0x04001FFD RID: 8189
	private Queue<long> results_counters = new Queue<long>(64);

	// Token: 0x04001FFE RID: 8190
	public double latest_time;

	// Token: 0x04001FFF RID: 8191
	public double latest_result;

	// Token: 0x04002000 RID: 8192
	public double calculated_percentage;

	// Token: 0x04002001 RID: 8193
	public long call_count;

	// Token: 0x04002002 RID: 8194
	public long debug_value;

	// Token: 0x04002003 RID: 8195
	public double last_max_value;

	// Token: 0x04002004 RID: 8196
	public int max_value_ticks;
}
