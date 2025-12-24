using System;

// Token: 0x02000381 RID: 897
public class SingleAction<TTask, TAction> where TTask : BehaviourTaskBase<TAction> where TAction : BehaviourElementAI
{
	// Token: 0x06002161 RID: 8545 RVA: 0x0011C134 File Offset: 0x0011A334
	public SingleAction(TTask pTask)
	{
		this.task = pTask;
		this.interval = pTask.single_interval;
		this.interval_random = pTask.single_interval_random;
	}

	// Token: 0x06002162 RID: 8546 RVA: 0x0011C186 File Offset: 0x0011A386
	public void reset()
	{
		this.timer = this.interval + Randy.randomFloat(0f, this.interval_random);
	}

	// Token: 0x040018B3 RID: 6323
	public float timer;

	// Token: 0x040018B4 RID: 6324
	public float interval = 1f;

	// Token: 0x040018B5 RID: 6325
	public float interval_random = 1f;

	// Token: 0x040018B6 RID: 6326
	public TTask task;
}
