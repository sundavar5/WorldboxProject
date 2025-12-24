using System;

namespace ai.behaviours
{
	// Token: 0x02000947 RID: 2375
	public class BehDragonCheckAttackTargetAlive : BehDragon
	{
		// Token: 0x06004640 RID: 17984 RVA: 0x001DC8F8 File Offset: 0x001DAAF8
		public override BehResult execute(Actor pActor)
		{
			if (this.dragon.aggroTargets.Count == 0)
			{
				return BehResult.Continue;
			}
			this.dragon.aggroTargets.RemoveWhere((Actor tAttacker) => tAttacker == null || !tAttacker.isAlive());
			return BehResult.Continue;
		}
	}
}
