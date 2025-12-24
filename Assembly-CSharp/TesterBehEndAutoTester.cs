using System;
using ai.behaviours;

// Token: 0x020004BF RID: 1215
public class TesterBehEndAutoTester : BehaviourActionTester
{
	// Token: 0x060029D1 RID: 10705 RVA: 0x00149BEC File Offset: 0x00147DEC
	public override BehResult execute(AutoTesterBot pObject)
	{
		BehaviourActionBase<AutoTesterBot>.world.auto_tester.active = false;
		return BehResult.RepeatStep;
	}
}
