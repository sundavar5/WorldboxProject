using System;
using ai.behaviours;

// Token: 0x020004D3 RID: 1235
public class TesterBehRestartJobTest : BehaviourActionTester
{
	// Token: 0x060029F8 RID: 10744 RVA: 0x0014A562 File Offset: 0x00148762
	public override BehResult execute(AutoTesterBot pObject)
	{
		pObject.ai.restartJob();
		return BehResult.Continue;
	}
}
