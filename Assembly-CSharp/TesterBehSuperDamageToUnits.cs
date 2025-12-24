using System;
using ai.behaviours;

// Token: 0x020004ED RID: 1261
public class TesterBehSuperDamageToUnits : BehaviourActionTester
{
	// Token: 0x06002A30 RID: 10800 RVA: 0x0014BB5C File Offset: 0x00149D5C
	public override BehResult execute(AutoTesterBot pObject)
	{
		foreach (Actor tActor in BehaviourActionBase<AutoTesterBot>.world.units)
		{
			if (tActor.asset.can_be_killed_by_stuff)
			{
				tActor.getHit(1E+17f, true, AttackType.Divine, null, true, false, true);
			}
		}
		return base.execute(pObject);
	}
}
