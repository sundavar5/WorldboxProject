using System;
using ai.behaviours;

// Token: 0x020004BA RID: 1210
public class TesterBehCloseWindows : BehaviourActionTester
{
	// Token: 0x060029C7 RID: 10695 RVA: 0x0014994A File Offset: 0x00147B4A
	public override BehResult execute(AutoTesterBot pObject)
	{
		if (ScrollWindow.isAnimationActive())
		{
			pObject.wait = 0.1f;
			return BehResult.RepeatStep;
		}
		if (!ScrollWindow.isWindowActive())
		{
			return BehResult.Continue;
		}
		ScrollWindow.hideAllEvent(false);
		pObject.wait = 0.25f;
		return BehResult.RepeatStep;
	}
}
