using System;
using ai.behaviours;

// Token: 0x020004F1 RID: 1265
public class TesterBehWaitYears : BehaviourActionTester
{
	// Token: 0x06002A38 RID: 10808 RVA: 0x0014BCE2 File Offset: 0x00149EE2
	public TesterBehWaitYears(int pWaitYears)
	{
		this.wait_years = pWaitYears;
	}

	// Token: 0x06002A39 RID: 10809 RVA: 0x0014BCF1 File Offset: 0x00149EF1
	public override BehResult execute(AutoTesterBot pObject)
	{
		pObject.beh_year_target = Date.getCurrentYear() + this.wait_years;
		return BehResult.Continue;
	}

	// Token: 0x04001F77 RID: 8055
	private int wait_years;
}
