using System;
using ai.behaviours;

// Token: 0x020004BC RID: 1212
public class TesterBehCullMobs : BehaviourActionTester
{
	// Token: 0x060029CB RID: 10699 RVA: 0x00149A30 File Offset: 0x00147C30
	public override BehResult execute(AutoTesterBot pActor)
	{
		foreach (Actor tUnit in BehaviourActionBase<AutoTesterBot>.world.units)
		{
			if (!tUnit.isRekt() && tUnit.isKingdomMob() && !Randy.randomChance(0.1f))
			{
				tUnit.getHit(10000f, false, AttackType.Divine, null, true, false, true);
			}
		}
		return base.execute(pActor);
	}
}
