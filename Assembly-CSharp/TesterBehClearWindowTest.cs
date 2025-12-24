using System;
using ai.behaviours;

// Token: 0x020004B8 RID: 1208
public class TesterBehClearWindowTest : BehaviourActionTester
{
	// Token: 0x060029C3 RID: 10691 RVA: 0x001498A0 File Offset: 0x00147AA0
	public override BehResult execute(AutoTesterBot pObject)
	{
		ScrollWindow.clear();
		Config.selected_trait_editor = null;
		SaveManager.currentWorkshopMapData = null;
		return BehResult.Continue;
	}
}
