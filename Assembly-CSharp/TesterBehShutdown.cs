using System;
using ai.behaviours;

// Token: 0x020004E0 RID: 1248
public class TesterBehShutdown : BehaviourActionTester
{
	// Token: 0x06002A16 RID: 10774 RVA: 0x0014B35B File Offset: 0x0014955B
	public override BehResult execute(AutoTesterBot pObject)
	{
		return BehResult.Stop;
	}
}
