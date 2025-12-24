using System;

namespace ai.behaviours.conditions
{
	// Token: 0x0200097C RID: 2428
	public class CondNoPeace : BehaviourActorCondition
	{
		// Token: 0x060046F0 RID: 18160 RVA: 0x001E1E4A File Offset: 0x001E004A
		public override bool check(Actor pActor)
		{
			return !WorldLawLibrary.world_law_peaceful_monsters.isEnabled();
		}
	}
}
