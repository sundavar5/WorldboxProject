using System;

namespace ai.behaviours
{
	// Token: 0x02000949 RID: 2377
	public class BehDragonCheckOverTargetActor : BehDragon
	{
		// Token: 0x06004644 RID: 17988 RVA: 0x001DCA84 File Offset: 0x001DAC84
		public override BehResult execute(Actor pActor)
		{
			if (WorldLawLibrary.world_law_peaceful_monsters.isEnabled())
			{
				return BehResult.Continue;
			}
			if (this.dragon.aggroTargets.Count == 0)
			{
				return BehResult.Continue;
			}
			if (!Dragon.canLand(pActor, null))
			{
				return BehResult.Continue;
			}
			if (this.dragon.targetsWithinLandAttackRange())
			{
				return base.forceTask(pActor, "dragon_land_attack", true, false);
			}
			return BehResult.Continue;
		}
	}
}
