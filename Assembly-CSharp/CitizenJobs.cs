using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x02000318 RID: 792
[Serializable]
public class CitizenJobs
{
	// Token: 0x06001DA0 RID: 7584 RVA: 0x0010804C File Offset: 0x0010624C
	public void clear()
	{
		this.jobs.Clear();
		this.occupied.Clear();
	}

	// Token: 0x06001DA1 RID: 7585 RVA: 0x00108064 File Offset: 0x00106264
	public int getTotalTasks()
	{
		return this._total_tasks;
	}

	// Token: 0x06001DA2 RID: 7586 RVA: 0x0010806C File Offset: 0x0010626C
	public bool hasAnyTask()
	{
		return this._total_tasks > 0;
	}

	// Token: 0x06001DA3 RID: 7587 RVA: 0x00108077 File Offset: 0x00106277
	public void clearJobs()
	{
		this._total_tasks = 0;
		this.jobs.Clear();
	}

	// Token: 0x06001DA4 RID: 7588 RVA: 0x0010808C File Offset: 0x0010628C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void addToJob(CitizenJobAsset pJobAsset, int pValue)
	{
		this._total_tasks += pValue;
		int tValue;
		if (this.jobs.TryGetValue(pJobAsset, out tValue))
		{
			this.jobs[pJobAsset] = tValue + pValue;
			return;
		}
		this.jobs.Add(pJobAsset, pValue);
	}

	// Token: 0x06001DA5 RID: 7589 RVA: 0x001080D4 File Offset: 0x001062D4
	public bool continueJob(CitizenJobAsset pJobAsset)
	{
		return this.jobs.ContainsKey(pJobAsset);
	}

	// Token: 0x06001DA6 RID: 7590 RVA: 0x001080E4 File Offset: 0x001062E4
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int countOccupied(CitizenJobAsset pJobAsset)
	{
		int tValue;
		if (this.occupied.TryGetValue(pJobAsset, out tValue))
		{
			return tValue;
		}
		return 0;
	}

	// Token: 0x06001DA7 RID: 7591 RVA: 0x00108104 File Offset: 0x00106304
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int countCurrentJobs(CitizenJobAsset pJobAsset)
	{
		int tValue;
		if (this.jobs.TryGetValue(pJobAsset, out tValue))
		{
			return tValue;
		}
		return 0;
	}

	// Token: 0x06001DA8 RID: 7592 RVA: 0x00108124 File Offset: 0x00106324
	public bool hasJob(CitizenJobAsset pJobAsset)
	{
		int tValue;
		return this.jobs.TryGetValue(pJobAsset, out tValue) && tValue != 0 && (!this.occupied.ContainsKey(pJobAsset) || this.occupied[pJobAsset] < tValue);
	}

	// Token: 0x06001DA9 RID: 7593 RVA: 0x00108168 File Offset: 0x00106368
	public void takeJob(CitizenJobAsset pJobAsset)
	{
		if (!this.occupied.ContainsKey(pJobAsset))
		{
			this.occupied.Add(pJobAsset, 1);
			return;
		}
		Dictionary<CitizenJobAsset, int> dictionary = this.occupied;
		dictionary[pJobAsset]++;
	}

	// Token: 0x06001DAA RID: 7594 RVA: 0x001081AC File Offset: 0x001063AC
	public void freeJob(CitizenJobAsset pJobAsset)
	{
		int tValue;
		if (this.occupied.TryGetValue(pJobAsset, out tValue))
		{
			if (tValue > 0)
			{
				Dictionary<CitizenJobAsset, int> dictionary = this.occupied;
				dictionary[pJobAsset]--;
				return;
			}
		}
		else
		{
			this.occupied.Add(pJobAsset, 0);
		}
	}

	// Token: 0x04001628 RID: 5672
	public Dictionary<CitizenJobAsset, int> jobs = new Dictionary<CitizenJobAsset, int>();

	// Token: 0x04001629 RID: 5673
	public Dictionary<CitizenJobAsset, int> occupied = new Dictionary<CitizenJobAsset, int>();

	// Token: 0x0400162A RID: 5674
	private int _total_tasks;
}
