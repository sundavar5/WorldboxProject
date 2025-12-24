using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020002F4 RID: 756
public class Batch<T>
{
	// Token: 0x06001C72 RID: 7282 RVA: 0x001022E2 File Offset: 0x001004E2
	public Batch()
	{
		this.createJobs();
		this.createHelpers();
	}

	// Token: 0x06001C73 RID: 7283 RVA: 0x00102324 File Offset: 0x00100524
	public void updateJobsPre(float pElapsed)
	{
		this._elapsed = pElapsed;
		List<Job<T>> tJobs = this.jobs_pre;
		int tCount = tJobs.Count;
		for (int i = 0; i < tCount; i++)
		{
			Job<T> tObj = tJobs[i];
			this._cur_container = tObj.container;
			if (tObj.current_skips > 0)
			{
				tObj.current_skips--;
			}
			else
			{
				this.runUpdater(tObj);
			}
		}
	}

	// Token: 0x06001C74 RID: 7284 RVA: 0x00102388 File Offset: 0x00100588
	public void updateJobsParallel(float pElapsed)
	{
		List<Job<T>> tJobs = this.jobs_parallel;
		int tCount = tJobs.Count;
		for (int i = 0; i < tCount; i++)
		{
			Job<T> tObj = tJobs[i];
			this._cur_container = tObj.container;
			tObj.job_updater();
		}
	}

	// Token: 0x06001C75 RID: 7285 RVA: 0x001023D0 File Offset: 0x001005D0
	public void updateJobsPost(float pElapsed)
	{
		this._elapsed = pElapsed;
		List<Job<T>> tJobs = this.jobs_post;
		int tCount = tJobs.Count;
		for (int i = 0; i < tCount; i++)
		{
			Job<T> tObj = tJobs[i];
			this._cur_container = tObj.container;
			if (tObj.current_skips > 0)
			{
				tObj.current_skips--;
			}
			else
			{
				this.runUpdater(tObj);
			}
		}
	}

	// Token: 0x06001C76 RID: 7286 RVA: 0x00102434 File Offset: 0x00100634
	private void runUpdater(Job<T> pObj)
	{
		double tTimeStart = Time.realtimeSinceStartupAsDouble;
		pObj.job_updater();
		if (pObj.random_tick_skips > 0)
		{
			pObj.current_skips = Randy.randomInt(0, pObj.random_tick_skips);
		}
		double tTimeEnd = Time.realtimeSinceStartupAsDouble - tTimeStart;
		pObj.time_benchmark += tTimeEnd;
		pObj.counter += this._cur_container.Count;
	}

	// Token: 0x06001C77 RID: 7287 RVA: 0x0010249C File Offset: 0x0010069C
	internal virtual void prepare()
	{
		for (int i = 0; i < this.containers.Count; i++)
		{
			this.containers[i].doChecks();
		}
	}

	// Token: 0x06001C78 RID: 7288 RVA: 0x001024D0 File Offset: 0x001006D0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected void createJob(out ObjectContainer<T> pContainer, JobUpdater pJobUpdater, JobType pType, string pID, int pRandomTickSkips = 0)
	{
		pContainer = new ObjectContainer<T>();
		pContainer.prepareArray(JobConst.MAX_ELEMENTS);
		this.containers.Add(pContainer);
		if (pJobUpdater != null)
		{
			this.addJob(pContainer, pJobUpdater, pType, pID, pRandomTickSkips);
		}
	}

	// Token: 0x06001C79 RID: 7289 RVA: 0x00102504 File Offset: 0x00100704
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected void addJob(ObjectContainer<T> pContainer, JobUpdater pJobUpdater, JobType pType, string pID, int pRandomTickSkips = 0)
	{
		switch (pType)
		{
		case JobType.Pre:
			this.putJob(pContainer, pJobUpdater, this.jobs_pre, pID, pRandomTickSkips);
			return;
		case JobType.Post:
			this.putJob(pContainer, pJobUpdater, this.jobs_post, pID, pRandomTickSkips);
			return;
		case JobType.Parallel:
			this.putJob(pContainer, pJobUpdater, this.jobs_parallel, pID, pRandomTickSkips);
			return;
		default:
			return;
		}
	}

	// Token: 0x06001C7A RID: 7290 RVA: 0x0010255C File Offset: 0x0010075C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void putJob(ObjectContainer<T> pContainer, JobUpdater pJobUpdater, List<Job<T>> pListJobs, string pID, int pRandomTickSkips = 0)
	{
		pListJobs.Add(new Job<T>
		{
			container = pContainer,
			job_updater = pJobUpdater,
			id = pID,
			random_tick_skips = pRandomTickSkips
		});
	}

	// Token: 0x06001C7B RID: 7291 RVA: 0x00102588 File Offset: 0x00100788
	internal virtual void clear()
	{
		for (int i = 0; i < this.containers.Count; i++)
		{
			this.containers[i].Clear();
		}
		if (this._array == null)
		{
			return;
		}
		Array.Clear(this._array, 0, this._array.Length);
	}

	// Token: 0x06001C7C RID: 7292 RVA: 0x001025DC File Offset: 0x001007DC
	internal virtual void remove(T pObject)
	{
		for (int i = 0; i < this.containers.Count; i++)
		{
			this.containers[i].Remove(pObject);
		}
	}

	// Token: 0x06001C7D RID: 7293 RVA: 0x00102611 File Offset: 0x00100811
	internal virtual void add(T pObject)
	{
		this.main.Add(pObject);
	}

	// Token: 0x06001C7E RID: 7294 RVA: 0x0010261F File Offset: 0x0010081F
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected bool check(ObjectContainer<T> pContainer)
	{
		if (pContainer.Count > 0 || pContainer.isDirtyContainer())
		{
			pContainer.checkAddRemove();
			this._array = pContainer.getFastSimpleArray();
			this._count = pContainer.Count;
			return true;
		}
		return false;
	}

	// Token: 0x06001C7F RID: 7295 RVA: 0x00102653 File Offset: 0x00100853
	protected virtual void createJobs()
	{
	}

	// Token: 0x06001C80 RID: 7296 RVA: 0x00102655 File Offset: 0x00100855
	protected virtual void createHelpers()
	{
	}

	// Token: 0x06001C81 RID: 7297 RVA: 0x00102657 File Offset: 0x00100857
	public virtual void clearHelperLists()
	{
	}

	// Token: 0x06001C82 RID: 7298 RVA: 0x0010265C File Offset: 0x0010085C
	public void debug(DebugTool pTool)
	{
		pTool.setText("total", this.main.Count, 0f, false, 0L, false, false, "");
	}

	// Token: 0x040015B8 RID: 5560
	internal ObjectContainer<T> main;

	// Token: 0x040015B9 RID: 5561
	internal bool free_slots;

	// Token: 0x040015BA RID: 5562
	private List<ObjectContainer<T>> containers = new List<ObjectContainer<T>>();

	// Token: 0x040015BB RID: 5563
	internal List<Job<T>> jobs_pre = new List<Job<T>>();

	// Token: 0x040015BC RID: 5564
	internal List<Job<T>> jobs_post = new List<Job<T>>();

	// Token: 0x040015BD RID: 5565
	internal List<Job<T>> jobs_parallel = new List<Job<T>>();

	// Token: 0x040015BE RID: 5566
	protected List<T> _list;

	// Token: 0x040015BF RID: 5567
	protected T[] _array;

	// Token: 0x040015C0 RID: 5568
	protected int _count;

	// Token: 0x040015C1 RID: 5569
	protected float _elapsed;

	// Token: 0x040015C2 RID: 5570
	protected ObjectContainer<T> _cur_container;

	// Token: 0x040015C3 RID: 5571
	internal JobUpdater clearParallelResults;

	// Token: 0x040015C4 RID: 5572
	internal JobUpdater applyParallelResults;

	// Token: 0x040015C5 RID: 5573
	public int batch_id;
}
