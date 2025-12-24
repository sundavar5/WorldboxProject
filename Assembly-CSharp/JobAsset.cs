using System;
using System.Collections.Generic;

// Token: 0x02000387 RID: 903
[Serializable]
public abstract class JobAsset<TCondition, TSimObject> : Asset where TCondition : BehaviourBaseCondition<TSimObject>
{
	// Token: 0x06002195 RID: 8597 RVA: 0x0011CE6D File Offset: 0x0011B06D
	public void addCondition(TCondition pCondition, bool pExpectedResult = true)
	{
		this.tasks[this.tasks.Count - 1].addCondition(pCondition, pExpectedResult);
	}

	// Token: 0x06002196 RID: 8598 RVA: 0x0011CE90 File Offset: 0x0011B090
	public void addTask(string pTask)
	{
		TaskContainer<TCondition, TSimObject> tNewTask = new TaskContainer<TCondition, TSimObject>();
		tNewTask.id = pTask;
		this.tasks.Add(tNewTask);
	}

	// Token: 0x040018DB RID: 6363
	public bool random;

	// Token: 0x040018DC RID: 6364
	public List<TaskContainer<TCondition, TSimObject>> tasks = new List<TaskContainer<TCondition, TSimObject>>();
}
