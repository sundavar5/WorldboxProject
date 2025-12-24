using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// Token: 0x020002F7 RID: 759
public class JobManagerBase<TBatch, T> where TBatch : Batch<T>, new()
{
	// Token: 0x06001C85 RID: 7301 RVA: 0x001026A7 File Offset: 0x001008A7
	public JobManagerBase(string pID)
	{
		this.id = pID;
	}

	// Token: 0x06001C86 RID: 7302 RVA: 0x001026E2 File Offset: 0x001008E2
	public void updateBase(float pElapsed)
	{
		this.clearJobBenchmarks();
		this.updateBaseJobsPre(pElapsed);
		this.updateBaseJobsParallel(pElapsed);
		this.updateBaseJobsPost(pElapsed);
		this.saveJobBenchmarks();
	}

	// Token: 0x06001C87 RID: 7303 RVA: 0x00102708 File Offset: 0x00100908
	private void clearJobBenchmarks()
	{
		if (!Bench.bench_enabled)
		{
			return;
		}
		for (int i = 0; i < this._batches_active.Count; i++)
		{
			TBatch tBatch = this._batches_active[i];
			for (int j = 0; j < tBatch.jobs_pre.Count; j++)
			{
				Job<T> job = tBatch.jobs_pre[j];
				job.time_benchmark = 0.0;
				job.counter = 0;
			}
			for (int k = 0; k < tBatch.jobs_post.Count; k++)
			{
				Job<T> job2 = tBatch.jobs_post[k];
				job2.time_benchmark = 0.0;
				job2.counter = 0;
			}
		}
	}

	// Token: 0x06001C88 RID: 7304 RVA: 0x001027C8 File Offset: 0x001009C8
	private void saveJobBenchmarks()
	{
		if (!Bench.bench_enabled)
		{
			return;
		}
		this._dict_benchmark_time.Clear();
		this._dict_benchmark_counter.Clear();
		for (int i = 0; i < this._batches_active.Count; i++)
		{
			TBatch tBatch = this._batches_active[i];
			this.checkListForBenchmark(tBatch.jobs_pre);
			this.checkListForBenchmark(tBatch.jobs_post);
		}
		foreach (KeyValuePair<string, double> keyValuePair in this._dict_benchmark_time)
		{
			string text;
			double num;
			keyValuePair.Deconstruct(out text, out num);
			string tID = text;
			double tTime = num;
			int tCounter = this._dict_benchmark_counter[tID];
			Bench.benchSave(tID, tTime, tCounter, this.benchmark_id);
			Bench.saveAverageCounter(tID, this.benchmark_id);
		}
	}

	// Token: 0x06001C89 RID: 7305 RVA: 0x001028B8 File Offset: 0x00100AB8
	private void checkListForBenchmark(List<Job<T>> pList)
	{
		for (int i = 0; i < pList.Count; i++)
		{
			Job<T> tJob = pList[i];
			if (!this._dict_benchmark_time.ContainsKey(tJob.id))
			{
				this._dict_benchmark_time.Add(tJob.id, 0.0);
				this._dict_benchmark_counter.Add(tJob.id, 0);
			}
			Dictionary<string, double> dict_benchmark_time = this._dict_benchmark_time;
			string key = tJob.id;
			dict_benchmark_time[key] += tJob.time_benchmark;
			Dictionary<string, int> dict_benchmark_counter = this._dict_benchmark_counter;
			key = tJob.id;
			dict_benchmark_counter[key] += tJob.counter;
		}
	}

	// Token: 0x06001C8A RID: 7306 RVA: 0x0010296C File Offset: 0x00100B6C
	internal void updateBaseJobsPre(float pElapsed)
	{
		for (int i = 0; i < this._batches_active.Count; i++)
		{
			this._batches_active[i].updateJobsPre(pElapsed);
		}
	}

	// Token: 0x06001C8B RID: 7307 RVA: 0x001029A8 File Offset: 0x00100BA8
	internal void updateBaseJobsPost(float pElapsed)
	{
		for (int i = 0; i < this._batches_active.Count; i++)
		{
			this._batches_active[i].updateJobsPost(pElapsed);
		}
	}

	// Token: 0x06001C8C RID: 7308 RVA: 0x001029E4 File Offset: 0x00100BE4
	internal void updateBaseJobsParallel(float pElapsed)
	{
		this.clearParallelResults();
		Bench.bench("update_jobs_parallel", this.benchmark_id, false);
		if (Config.parallel_jobs_updater)
		{
			Parallel.ForEach<TBatch>(this._batches_active, World.world.parallel_options, delegate(TBatch pBatch)
			{
				pBatch.updateJobsParallel(pElapsed);
			});
		}
		else
		{
			List<TBatch> tBatches = this._batches_active;
			int tCount = tBatches.Count;
			for (int i = 0; i < tCount; i++)
			{
				tBatches[i].updateJobsParallel(pElapsed);
			}
		}
		Bench.benchEnd("update_jobs_parallel", this.benchmark_id, false, 0L, false);
		this.applyParallelResults();
	}

	// Token: 0x06001C8D RID: 7309 RVA: 0x00102A90 File Offset: 0x00100C90
	internal void clearParallelResults()
	{
		Bench.bench("clear_parallel_results", this.benchmark_id, false);
		for (int i = 0; i < this._batches_active.Count; i++)
		{
			JobUpdater clearParallelResults = this._batches_active[i].clearParallelResults;
			if (clearParallelResults != null)
			{
				clearParallelResults();
			}
		}
		Bench.benchEnd("clear_parallel_results", this.benchmark_id, false, 0L, false);
	}

	// Token: 0x06001C8E RID: 7310 RVA: 0x00102AFC File Offset: 0x00100CFC
	internal void applyParallelResults()
	{
		Bench.bench("apply_parallel_results", this.benchmark_id, false);
		for (int i = 0; i < this._batches_active.Count; i++)
		{
			JobUpdater applyParallelResults = this._batches_active[i].applyParallelResults;
			if (applyParallelResults != null)
			{
				applyParallelResults();
			}
		}
		Bench.benchEnd("apply_parallel_results", this.benchmark_id, false, 0L, false);
	}

	// Token: 0x06001C8F RID: 7311 RVA: 0x00102B67 File Offset: 0x00100D67
	internal void removeObject(T pObject, TBatch pBatch)
	{
		pBatch.remove(pObject);
		this.checkFree(pBatch);
	}

	// Token: 0x06001C90 RID: 7312 RVA: 0x00102B7C File Offset: 0x00100D7C
	protected TBatch newBatch()
	{
		TBatch tBatch = Activator.CreateInstance<TBatch>();
		this._batches_active.Add(tBatch);
		return tBatch;
	}

	// Token: 0x06001C91 RID: 7313 RVA: 0x00102B9C File Offset: 0x00100D9C
	internal virtual void addNewObject(T pObject)
	{
		TBatch tBatch = this.getBatch();
		tBatch.add(pObject);
		tBatch.main.checkAddRemove();
		if (tBatch.main.Count >= JobConst.MAX_ELEMENTS)
		{
			tBatch.free_slots = false;
			this._batches_free.Pop();
		}
	}

	// Token: 0x06001C92 RID: 7314 RVA: 0x00102BFC File Offset: 0x00100DFC
	internal TBatch getBatch()
	{
		if (this._batches_free.Count == 0)
		{
			TBatch tNewBatch = this.newBatch();
			tNewBatch.batch_id = this._batches_active.Count;
			this.makeFree(tNewBatch);
			return tNewBatch;
		}
		TBatch tBatch = this._batches_free.Peek();
		if (tBatch.main.Count == 0)
		{
			this._batches_active.Add(tBatch);
		}
		return tBatch;
	}

	// Token: 0x06001C93 RID: 7315 RVA: 0x00102C68 File Offset: 0x00100E68
	protected void checkFree(TBatch pBatch)
	{
		pBatch.main.checkAddRemove();
		if (pBatch.main.Count < JobConst.MAX_ELEMENTS)
		{
			this.makeFree(pBatch);
		}
		if (pBatch.main.Count == 0)
		{
			this._batches_active.Remove(pBatch);
		}
	}

	// Token: 0x06001C94 RID: 7316 RVA: 0x00102CC2 File Offset: 0x00100EC2
	protected virtual void makeFree(TBatch pBatch)
	{
		if (pBatch.free_slots)
		{
			return;
		}
		pBatch.free_slots = true;
		this._batches_free.Push(pBatch);
	}

	// Token: 0x06001C95 RID: 7317 RVA: 0x00102CEC File Offset: 0x00100EEC
	internal void clear()
	{
		this._batches_free.Clear();
		for (int i = 0; i < this._batches_active.Count; i++)
		{
			TBatch tBatch = this._batches_active[i];
			tBatch.clear();
			tBatch.free_slots = false;
			this.makeFree(tBatch);
		}
	}

	// Token: 0x06001C96 RID: 7318 RVA: 0x00102D48 File Offset: 0x00100F48
	internal void clearHelperLists()
	{
		for (int i = 0; i < this._batches_active.Count; i++)
		{
			this._batches_active[i].clearHelperLists();
		}
	}

	// Token: 0x06001C97 RID: 7319 RVA: 0x00102D84 File Offset: 0x00100F84
	public void debug(DebugTool pTool)
	{
		int tObjects = 0;
		for (int i = 0; i < this._batches_active.Count; i++)
		{
			TBatch tBatch = this._batches_active[i];
			tObjects += tBatch.main.Count;
		}
		pTool.setText("batches all", this._batches_active.Count, 0f, false, 0L, false, false, "");
		pTool.setText("objects", tObjects, 0f, false, 0L, false, false, "");
		pTool.setSeparator();
		pTool.setText("parallel_jobs_updater_on", Config.parallel_jobs_updater, 0f, false, 0L, false, false, "");
	}

	// Token: 0x06001C98 RID: 7320 RVA: 0x00102E40 File Offset: 0x00101040
	public string debugBatchCount()
	{
		return this._batches_active.Count.ToString() + " / " + this._batches_free.Count.ToString();
	}

	// Token: 0x06001C99 RID: 7321 RVA: 0x00102E80 File Offset: 0x00101080
	public string debugJobCount()
	{
		int tCount = 0;
		foreach (TBatch tBatch in this._batches_active)
		{
			tCount += tBatch.jobs_post.Count;
			tCount += tBatch.jobs_pre.Count;
			tCount += tBatch.jobs_parallel.Count;
		}
		return tCount.ToString();
	}

	// Token: 0x040015CE RID: 5582
	protected readonly List<TBatch> _batches_active = new List<TBatch>();

	// Token: 0x040015CF RID: 5583
	private readonly Stack<TBatch> _batches_free = new Stack<TBatch>();

	// Token: 0x040015D0 RID: 5584
	public string id;

	// Token: 0x040015D1 RID: 5585
	public string benchmark_id;

	// Token: 0x040015D2 RID: 5586
	private Dictionary<string, double> _dict_benchmark_time = new Dictionary<string, double>();

	// Token: 0x040015D3 RID: 5587
	private Dictionary<string, int> _dict_benchmark_counter = new Dictionary<string, int>();
}
