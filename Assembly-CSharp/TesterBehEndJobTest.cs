using System;
using ai.behaviours;

// Token: 0x020004C0 RID: 1216
public class TesterBehEndJobTest : BehaviourActionTester
{
	// Token: 0x060029D3 RID: 10707 RVA: 0x00149C07 File Offset: 0x00147E07
	public override BehResult execute(AutoTesterBot pObject)
	{
		pObject.ai.reset();
		pObject.stopAutoTester();
		return BehResult.Continue;
	}
}
