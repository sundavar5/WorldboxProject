using System;
using ai.behaviours;

// Token: 0x020004BE RID: 1214
public class TesterBehDestroySimObjects : BehaviourActionTester
{
	// Token: 0x060029CF RID: 10703 RVA: 0x00149B50 File Offset: 0x00147D50
	public override BehResult execute(AutoTesterBot pObject)
	{
		foreach (Actor actor in BehaviourActionBase<AutoTesterBot>.world.units)
		{
			actor.getHitFullHealth(AttackType.Divine);
		}
		foreach (Building building in BehaviourActionBase<AutoTesterBot>.world.buildings)
		{
			building.getHitFullHealth(AttackType.Divine);
		}
		return base.execute(pObject);
	}
}
