using System;
using ai.behaviours;

// Token: 0x020004B7 RID: 1207
public class TesterBehClearFavorites : BehaviourActionTester
{
	// Token: 0x060029C1 RID: 10689 RVA: 0x00149830 File Offset: 0x00147A30
	public override BehResult execute(AutoTesterBot pActor)
	{
		foreach (Actor tUnit in BehaviourActionBase<AutoTesterBot>.world.units)
		{
			if (!tUnit.isRekt())
			{
				tUnit.data.favorite = false;
			}
		}
		return base.execute(pActor);
	}
}
