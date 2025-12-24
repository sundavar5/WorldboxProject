using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ai.behaviours;
using UnityEngine;

// Token: 0x02000382 RID: 898
public abstract class AiSystem<TSimObject, TJob, TTask, TAction, TCondition> where TJob : JobAsset<TCondition, TSimObject> where TTask : BehaviourTaskBase<TAction> where TAction : BehaviourActionBase<TSimObject> where TCondition : BehaviourBaseCondition<TSimObject>
{
	// Token: 0x06002163 RID: 8547 RVA: 0x0011C1A5 File Offset: 0x0011A3A5
	public AiSystem(TSimObject pObject)
	{
		this.ai_object = pObject;
		this.next_job_delegate = new GetNextJobID(AiSystem<TSimObject, TJob, TTask, TAction, TCondition>.nextJobDefault);
	}

	// Token: 0x06002164 RID: 8548 RVA: 0x0011C1D2 File Offset: 0x0011A3D2
	public void scheduleTask(string pTaskID)
	{
		this._scheduled_task_id = pTaskID;
	}

	// Token: 0x06002165 RID: 8549 RVA: 0x0011C1DC File Offset: 0x0011A3DC
	public void addSingleTask(string pID)
	{
		TTask pTask = this.task_library.get(pID);
		if (this._single_actions == null)
		{
			this._single_actions = new List<SingleAction<TTask, TAction>>();
		}
		SingleAction<TTask, TAction> tNewSingleAction = new SingleAction<TTask, TAction>(pTask);
		this._single_actions.Add(tNewSingleAction);
		tNewSingleAction.reset();
	}

	// Token: 0x06002166 RID: 8550 RVA: 0x0011C220 File Offset: 0x0011A420
	private void updateNewBehJob()
	{
		if (this._scheduled_task_id != null)
		{
			this.setTask(this._scheduled_task_id, true, false, false);
			this._scheduled_task_id = null;
			return;
		}
		if (this.job == null)
		{
			string tNewJobID = this.next_job_delegate();
			this.setJob(tNewJobID);
		}
		if (this.task_index >= this.job.tasks.Count)
		{
			this.task_index = 0;
		}
		TaskContainer<TCondition, TSimObject> tTaskContainer = this.getNextTask(this.job);
		if (!tTaskContainer.has_conditions)
		{
			this.setTask(tTaskContainer.id, true, false, false);
			return;
		}
		if (this.checkConditionsForTask(tTaskContainer))
		{
			this.setTask(tTaskContainer.id, true, false, false);
			return;
		}
		this.setTask("nothing", true, false, false);
	}

	// Token: 0x06002167 RID: 8551 RVA: 0x0011C2DC File Offset: 0x0011A4DC
	private TaskContainer<TCondition, TSimObject> getNextTask(TJob pJob)
	{
		List<TaskContainer<TCondition, TSimObject>> tTasks = pJob.tasks;
		int num;
		if (pJob.random)
		{
			if (this.task_index == 0 && this._random_tasks.Length != tTasks.Count)
			{
				this._random_tasks = new int[tTasks.Count];
				for (int i = 0; i < this._random_tasks.Length; i++)
				{
					this._random_tasks[i] = i;
				}
				this._random_tasks.Shuffle<int>();
			}
			List<TaskContainer<TCondition, TSimObject>> list = tTasks;
			int[] random_tasks = this._random_tasks;
			num = this.task_index;
			this.task_index = num + 1;
			return list[random_tasks[num]];
		}
		List<TaskContainer<TCondition, TSimObject>> list2 = tTasks;
		num = this.task_index;
		this.task_index = num + 1;
		return list2[num];
	}

	// Token: 0x06002168 RID: 8552 RVA: 0x0011C388 File Offset: 0x0011A588
	private bool checkConditionsForTask(TaskContainer<TCondition, TSimObject> pTaskContainer)
	{
		if (pTaskContainer.conditions.Count == 0)
		{
			Debug.LogError("TOO MANY COOKS");
		}
		foreach (KeyValuePair<TCondition, bool> keyValuePair in pTaskContainer.conditions)
		{
			TCondition tcondition;
			bool flag;
			keyValuePair.Deconstruct(out tcondition, out flag);
			TCondition tcondition2 = tcondition;
			bool tExpectedResult = flag;
			if (tcondition2.check(this.ai_object) != tExpectedResult)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06002169 RID: 8553 RVA: 0x0011C418 File Offset: 0x0011A618
	public void subscribeToTaskSwitch(TaskSwitchAction pAction)
	{
		this._task_switch_action = (TaskSwitchAction)Delegate.Combine(this._task_switch_action, pAction);
	}

	// Token: 0x0600216A RID: 8554 RVA: 0x0011C434 File Offset: 0x0011A634
	public virtual void setTask(string pTaskId, bool pClean = true, bool pCleanJob = false, bool pForceAction = false)
	{
		if (pClean)
		{
			this.clearBeh();
		}
		if (pCleanJob)
		{
			this.job = default(TJob);
			this.task_index = 0;
			this.clearAction();
		}
		this.task = this.task_library.get(pTaskId);
		this.action_index = 0;
		this.restarts = 0;
		this._timestamp_task_start = World.world.getCurWorldTime();
		if (pForceAction)
		{
			this.setAction(this.task.get(this.action_index));
		}
		TaskSwitchAction task_switch_action = this._task_switch_action;
		if (task_switch_action == null)
		{
			return;
		}
		task_switch_action();
	}

	// Token: 0x0600216B RID: 8555 RVA: 0x0011C4C6 File Offset: 0x0011A6C6
	protected virtual void setAction(TAction pAction)
	{
		this.action = pAction;
	}

	// Token: 0x0600216C RID: 8556 RVA: 0x0011C4CF File Offset: 0x0011A6CF
	private void clearAction()
	{
		this.action = default(TAction);
	}

	// Token: 0x0600216D RID: 8557 RVA: 0x0011C4DD File Offset: 0x0011A6DD
	public void restartJob()
	{
		this.action_index = 0;
		this.task_index = 0;
		this.clearAction();
	}

	// Token: 0x0600216E RID: 8558 RVA: 0x0011C4F3 File Offset: 0x0011A6F3
	internal void clearBeh()
	{
		if (this.clear_action_delegate != null)
		{
			this.clear_action_delegate();
		}
	}

	// Token: 0x0600216F RID: 8559 RVA: 0x0011C508 File Offset: 0x0011A708
	public void clearJob()
	{
		this.job = default(TJob);
		this.task_index = 0;
	}

	// Token: 0x06002170 RID: 8560 RVA: 0x0011C51D File Offset: 0x0011A71D
	public virtual void setJob(string pJobID)
	{
		this.job = this.jobs_library.get(pJobID);
		this.task_index = 0;
	}

	// Token: 0x06002171 RID: 8561 RVA: 0x0011C538 File Offset: 0x0011A738
	public void updateSingleTasks(float pElapsed)
	{
		if (this._single_actions == null)
		{
			return;
		}
		for (int i = 0; i < this._single_actions.Count; i++)
		{
			SingleAction<TTask, TAction> tSingleAction = this._single_actions[i];
			tSingleAction.timer -= pElapsed;
			if (tSingleAction.timer <= 0f)
			{
				tSingleAction.task.list[0].startExecute(this.ai_object);
				tSingleAction.reset();
			}
		}
	}

	// Token: 0x06002172 RID: 8562 RVA: 0x0011C5BC File Offset: 0x0011A7BC
	internal void update()
	{
		if (Bench.bench_ai_enabled)
		{
			if (this.task != null)
			{
				string id = this.task.id;
			}
			double tTimeStart = Time.realtimeSinceStartupAsDouble;
			this.run();
			double tTimeEnd = Time.realtimeSinceStartupAsDouble - tTimeStart;
			if (this.task != null)
			{
				this.task.rate_counter_calls.registerEvent();
				this.task.rate_counter_performance.registerEvent(tTimeEnd);
				return;
			}
		}
		else
		{
			this.run();
		}
	}

	// Token: 0x06002173 RID: 8563 RVA: 0x0011C641 File Offset: 0x0011A841
	public void decisionRun()
	{
		this.run();
	}

	// Token: 0x06002174 RID: 8564 RVA: 0x0011C64C File Offset: 0x0011A84C
	private void run()
	{
		if (this.task == null)
		{
			this.updateNewBehJob();
			if (this.task == null)
			{
				return;
			}
		}
		if (this.action_index >= this.task.list.Count)
		{
			this.setTaskBehFinished();
			return;
		}
		this.setAction(this.task.get(this.action_index));
		BehResult tResult;
		if (Bench.bench_ai_enabled)
		{
			string id = this.action.id;
			double tTimeStart = Time.realtimeSinceStartupAsDouble;
			tResult = this.action.startExecute(this.ai_object);
			double tTimeEnd = Time.realtimeSinceStartupAsDouble - tTimeStart;
			if (this.action != null)
			{
				this.action.rate_counter_calls.registerEvent();
				this.action.rate_counter_performance.registerEvent(tTimeEnd);
			}
		}
		else
		{
			tResult = this.action.startExecute(this.ai_object);
		}
		if (this.task == null)
		{
			return;
		}
		switch (tResult)
		{
		case BehResult.Continue:
			this.action_index++;
			return;
		case BehResult.Stop:
			this.setTaskBehFinished();
			return;
		case BehResult.RepeatStep:
		case BehResult.Skip:
		case BehResult.ActiveTaskReturn:
			break;
		case BehResult.StepBack:
			this.action_index--;
			if (this.action_index < 0)
			{
				this.action_index = 0;
				return;
			}
			break;
		case BehResult.RestartTask:
			this.action_index = 0;
			this.restarts++;
			return;
		case BehResult.ImmediateRun:
			this.run();
			break;
		default:
			return;
		}
	}

	// Token: 0x06002175 RID: 8565 RVA: 0x0011C7CE File Offset: 0x0011A9CE
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasTask()
	{
		return this.task != null;
	}

	// Token: 0x06002176 RID: 8566 RVA: 0x0011C7DE File Offset: 0x0011A9DE
	public void setTaskBehFinished()
	{
		this.task = default(TTask);
		this.action_index = -1;
		this.clearAction();
	}

	// Token: 0x06002177 RID: 8567 RVA: 0x0011C7F9 File Offset: 0x0011A9F9
	protected virtual void debugLogAction()
	{
	}

	// Token: 0x06002178 RID: 8568 RVA: 0x0011C7FB File Offset: 0x0011A9FB
	protected virtual void debugLogActionResult(BehResult pResult)
	{
	}

	// Token: 0x06002179 RID: 8569 RVA: 0x0011C800 File Offset: 0x0011AA00
	protected string getActionID(TAction pAction)
	{
		TAction taction = pAction;
		string tActionID = (taction != null) ? taction.GetType().ToString() : null;
		if (tActionID != null)
		{
			tActionID = tActionID.Replace("ai.behaviours.", "");
		}
		return tActionID;
	}

	// Token: 0x0600217A RID: 8570 RVA: 0x0011C83C File Offset: 0x0011AA3C
	public void debug(DebugTool pTool)
	{
		string tActionID = this.getActionID(this.action);
		if (tActionID != null)
		{
			tActionID = tActionID.Replace("ai.behaviours.", "");
		}
		pTool.setText("job:", (this.job == null) ? "-" : this.job.id, 0f, false, 0L, false, false, "");
		int num = this.task_index + 1;
		TJob tjob = this.job;
		int? num2 = (tjob != null) ? new int?(tjob.tasks.Count) : null;
		string nextTask;
		if (num < num2.GetValueOrDefault() & num2 != null)
		{
			if (this.job.random)
			{
				TJob tjob2 = this.job;
				nextTask = ((tjob2 != null) ? tjob2.tasks[this._random_tasks[this.task_index + 1]].id : null);
				nextTask += " (R)";
			}
			else
			{
				TJob tjob3 = this.job;
				nextTask = ((tjob3 != null) ? tjob3.tasks[this.task_index + 1].id : null);
				nextTask += " (S)";
			}
		}
		else
		{
			nextTask = "-";
		}
		pTool.setText("next task:", nextTask, 0f, false, 0L, false, false, "");
		pTool.setSeparator();
		string pT = ": task:";
		TTask ttask = this.task;
		pTool.setText(pT, (ttask != null) ? ttask.id : null, 0f, false, 0L, false, false, "");
		string pT2 = ": task index:";
		string str = this.task_index.ToString();
		string str2 = "/";
		TJob tjob4 = this.job;
		pTool.setText(pT2, str + str2 + ((tjob4 != null) ? new int?(tjob4.tasks.Count) : null).ToString(), 0f, false, 0L, false, false, "");
		pTool.setSeparator();
		pTool.setText(":: action:", tActionID, 0f, false, 0L, false, false, "");
		string pT3 = ":: action index:";
		string str3 = this.action_index.ToString();
		string str4 = "/";
		TTask ttask2 = this.task;
		pTool.setText(pT3, str3 + str4 + ((ttask2 != null) ? new int?(ttask2.list.Count) : null).ToString(), 0f, false, 0L, false, false, "");
		pTool.setSeparator();
	}

	// Token: 0x0600217B RID: 8571 RVA: 0x0011CAC4 File Offset: 0x0011ACC4
	public static string nextJobDefault()
	{
		return null;
	}

	// Token: 0x0600217C RID: 8572 RVA: 0x0011CAC8 File Offset: 0x0011ACC8
	internal virtual void reset()
	{
		this.jobs_library = null;
		this.task_library = null;
		this._single_actions = null;
		this.action_index = 0;
		this.task_index = 0;
		this.restarts = 0;
		this.job = default(TJob);
		this.task = default(TTask);
		this.action = default(TAction);
		this.next_job_delegate = null;
		this.clear_action_delegate = null;
		this._task_switch_action = null;
	}

	// Token: 0x0600217D RID: 8573 RVA: 0x0011CB38 File Offset: 0x0011AD38
	public string getTaskTime()
	{
		return Date.formatSeconds(World.world.getWorldTimeElapsedSince(this._timestamp_task_start));
	}

	// Token: 0x040018B7 RID: 6327
	public AssetLibrary<TJob> jobs_library;

	// Token: 0x040018B8 RID: 6328
	public AssetLibrary<TTask> task_library;

	// Token: 0x040018B9 RID: 6329
	private List<SingleAction<TTask, TAction>> _single_actions;

	// Token: 0x040018BA RID: 6330
	internal int action_index;

	// Token: 0x040018BB RID: 6331
	internal int restarts;

	// Token: 0x040018BC RID: 6332
	internal int task_index;

	// Token: 0x040018BD RID: 6333
	private int[] _random_tasks = new int[0];

	// Token: 0x040018BE RID: 6334
	public TJob job;

	// Token: 0x040018BF RID: 6335
	internal TTask task;

	// Token: 0x040018C0 RID: 6336
	internal TAction action;

	// Token: 0x040018C1 RID: 6337
	private double _timestamp_task_start;

	// Token: 0x040018C2 RID: 6338
	protected readonly TSimObject ai_object;

	// Token: 0x040018C3 RID: 6339
	public GetNextJobID next_job_delegate;

	// Token: 0x040018C4 RID: 6340
	public JobAction clear_action_delegate;

	// Token: 0x040018C5 RID: 6341
	private TaskSwitchAction _task_switch_action;

	// Token: 0x040018C6 RID: 6342
	private string _scheduled_task_id;
}
