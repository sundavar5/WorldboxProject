using System;

// Token: 0x020004F7 RID: 1271
public class AiSystemTester : AiSystem<AutoTesterBot, JobTesterAsset, BehaviourTaskTester, BehaviourActionTester, BehaviourTesterCondition>
{
	// Token: 0x06002A4C RID: 10828 RVA: 0x0014C2C7 File Offset: 0x0014A4C7
	public AiSystemTester(AutoTesterBot pObject) : base(pObject)
	{
		this.jobs_library = AssetManager.tester_jobs;
		this.task_library = AssetManager.tester_tasks;
	}
}
