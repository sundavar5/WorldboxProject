using System;

namespace ai.behaviours
{
	// Token: 0x020008F4 RID: 2292
	public class BehReplenishEnergy : BehaviourActionActor
	{
		// Token: 0x06004560 RID: 17760 RVA: 0x001D20FF File Offset: 0x001D02FF
		public override BehResult execute(Actor pActor)
		{
			pActor.setMaxMana();
			pActor.setMaxStamina();
			return BehResult.Continue;
		}
	}
}
