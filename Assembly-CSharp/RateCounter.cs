using System;
using System.Collections.Generic;

// Token: 0x020002E8 RID: 744
public class RateCounter
{
	// Token: 0x06001C14 RID: 7188 RVA: 0x0010034E File Offset: 0x000FE54E
	public RateCounter(string pID, int pTicks = 60)
	{
		this._id = pID;
		this._ticks = (double)pTicks;
	}

	// Token: 0x06001C15 RID: 7189 RVA: 0x00100370 File Offset: 0x000FE570
	public void reset()
	{
		this._timestamps.Clear();
		this._total = 0;
	}

	// Token: 0x06001C16 RID: 7190 RVA: 0x00100384 File Offset: 0x000FE584
	public void registerEvent()
	{
	}

	// Token: 0x06001C17 RID: 7191 RVA: 0x00100386 File Offset: 0x000FE586
	public void registerEvent(double pValue)
	{
	}

	// Token: 0x06001C18 RID: 7192 RVA: 0x00100388 File Offset: 0x000FE588
	private double getTime()
	{
		return World.world.getCurWorldTime();
	}

	// Token: 0x06001C19 RID: 7193 RVA: 0x00100394 File Offset: 0x000FE594
	public double getValuesAll()
	{
		double tResult = 0.0;
		foreach (RateCounterData tData in this._timestamps)
		{
			tResult += tData.value;
		}
		return tResult;
	}

	// Token: 0x06001C1A RID: 7194 RVA: 0x001003F4 File Offset: 0x000FE5F4
	public int getEventsPerTick()
	{
		double tNow = this.getTime();
		this.cleanupOldEvents(tNow);
		return this._timestamps.Count;
	}

	// Token: 0x06001C1B RID: 7195 RVA: 0x0010041C File Offset: 0x000FE61C
	private void cleanupOldEvents(double tNow)
	{
		if (this._timestamps.Count == 0)
		{
			return;
		}
		this._timestamps.RemoveAll((RateCounterData t) => tNow - t.timestamp > this._ticks);
	}

	// Token: 0x06001C1C RID: 7196 RVA: 0x00100463 File Offset: 0x000FE663
	public string getInfo()
	{
		return string.Format("{0} | tot: {1}", this.getEventsPerTick(), this._total);
	}

	// Token: 0x06001C1D RID: 7197 RVA: 0x00100485 File Offset: 0x000FE685
	public int getTotal()
	{
		return this._total;
	}

	// Token: 0x170001BC RID: 444
	// (get) Token: 0x06001C1E RID: 7198 RVA: 0x0010048D File Offset: 0x000FE68D
	public string id
	{
		get
		{
			return this._id;
		}
	}

	// Token: 0x06001C1F RID: 7199 RVA: 0x00100495 File Offset: 0x000FE695
	public int getEventsPerMinute()
	{
		return this.getEventsPerTick();
	}

	// Token: 0x04001576 RID: 5494
	private readonly List<RateCounterData> _timestamps = new List<RateCounterData>();

	// Token: 0x04001577 RID: 5495
	private string _id;

	// Token: 0x04001578 RID: 5496
	private double _ticks;

	// Token: 0x04001579 RID: 5497
	private int _total;
}
