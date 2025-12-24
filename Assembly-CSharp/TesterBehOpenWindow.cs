using System;
using ai.behaviours;

// Token: 0x020004C7 RID: 1223
public class TesterBehOpenWindow : BehaviourActionTester
{
	// Token: 0x060029E2 RID: 10722 RVA: 0x0014A046 File Offset: 0x00148246
	public TesterBehOpenWindow(string pType)
	{
		this._type = pType;
	}

	// Token: 0x060029E3 RID: 10723 RVA: 0x0014A058 File Offset: 0x00148258
	public override BehResult execute(AutoTesterBot pObject)
	{
		pObject.wait = 0.5f;
		if (ScrollWindow.isAnimationActive())
		{
			return BehResult.RepeatStep;
		}
		string tWindow = this._type;
		if (this._type == "random")
		{
			tWindow = AssetManager.window_library.getTestableWindows().GetRandom<WindowAsset>().id;
		}
		ScrollWindow.showWindow(tWindow, true, false);
		return BehResult.Continue;
	}

	// Token: 0x04001F38 RID: 7992
	private string _type;
}
