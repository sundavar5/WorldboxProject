using System;
using ai.behaviours;

// Token: 0x020004C3 RID: 1219
public class TesterBehGenerateMap : BehaviourActionTester
{
	// Token: 0x060029DA RID: 10714 RVA: 0x00149E49 File Offset: 0x00148049
	public override BehResult execute(AutoTesterBot pObject)
	{
		Config.customZoneX = 7;
		Config.customZoneY = 7;
		BehaviourActionBase<AutoTesterBot>.world.generateNewMap();
		return base.execute(pObject);
	}
}
