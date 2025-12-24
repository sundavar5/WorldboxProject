using System;
using ai.behaviours;

// Token: 0x020004F0 RID: 1264
public class TesterBehWaitForYear : BehaviourActionTester
{
	// Token: 0x06002A36 RID: 10806 RVA: 0x0014BCA8 File Offset: 0x00149EA8
	public override BehResult execute(AutoTesterBot pObject)
	{
		float num = (float)pObject.beh_year_target;
		float tCurYear = (float)Date.getCurrentYear();
		if (num - tCurYear <= 0f)
		{
			return BehResult.Continue;
		}
		pObject.wait = 1f;
		return BehResult.RepeatStep;
	}
}
