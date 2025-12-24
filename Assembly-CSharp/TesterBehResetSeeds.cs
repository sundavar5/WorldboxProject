using System;
using ai.behaviours;

// Token: 0x020004D1 RID: 1233
public class TesterBehResetSeeds : BehaviourActionTester
{
	// Token: 0x060029F4 RID: 10740 RVA: 0x0014A4DC File Offset: 0x001486DC
	public TesterBehResetSeeds(int pValue)
	{
		this.value = pValue;
	}

	// Token: 0x060029F5 RID: 10741 RVA: 0x0014A4EB File Offset: 0x001486EB
	public override BehResult execute(AutoTesterBot pObject)
	{
		Randy.resetSeed(this.value);
		return BehResult.Continue;
	}

	// Token: 0x04001F48 RID: 8008
	private int value;
}
