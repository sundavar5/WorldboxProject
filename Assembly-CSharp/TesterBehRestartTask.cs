using System;
using ai.behaviours;

// Token: 0x020004D4 RID: 1236
public class TesterBehRestartTask : BehaviourActionTester
{
	// Token: 0x060029FA RID: 10746 RVA: 0x0014A578 File Offset: 0x00148778
	public override BehResult execute(AutoTesterBot pActor)
	{
		return BehResult.RestartTask;
	}
}
