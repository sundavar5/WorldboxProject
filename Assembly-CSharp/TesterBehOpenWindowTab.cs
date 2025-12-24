using System;
using ai.behaviours;

// Token: 0x020004C8 RID: 1224
public class TesterBehOpenWindowTab : BehaviourActionTester
{
	// Token: 0x060029E4 RID: 10724 RVA: 0x0014A0B0 File Offset: 0x001482B0
	public TesterBehOpenWindowTab(string pTab = null)
	{
		this._tab = pTab;
	}

	// Token: 0x060029E5 RID: 10725 RVA: 0x0014A0C0 File Offset: 0x001482C0
	public override BehResult execute(AutoTesterBot pObject)
	{
		pObject.wait = 0.5f;
		if (ScrollWindow.isAnimationActive())
		{
			return BehResult.RepeatStep;
		}
		if (!ScrollWindow.isWindowActive())
		{
			return BehResult.Stop;
		}
		ScrollWindow tCurrentWindow = ScrollWindow.getCurrentWindow();
		if (tCurrentWindow == null)
		{
			return BehResult.Stop;
		}
		tCurrentWindow.tabs.showTab(this._tab);
		return BehResult.Continue;
	}

	// Token: 0x04001F39 RID: 7993
	private string _tab;
}
