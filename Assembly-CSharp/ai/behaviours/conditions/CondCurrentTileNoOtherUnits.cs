using System;

namespace ai.behaviours.conditions
{
	// Token: 0x0200097B RID: 2427
	public class CondCurrentTileNoOtherUnits : BehaviourActorCondition
	{
		// Token: 0x060046EE RID: 18158 RVA: 0x001E1E2F File Offset: 0x001E002F
		public override bool check(Actor pActor)
		{
			return pActor.current_tile.countUnits() <= 1;
		}
	}
}
