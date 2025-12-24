using System;

namespace ai.behaviours.conditions
{
	// Token: 0x02000979 RID: 2425
	public class CondActorFlying : BehaviourActorCondition
	{
		// Token: 0x060046EA RID: 18154 RVA: 0x001E1DD3 File Offset: 0x001DFFD3
		public override bool check(Actor pActor)
		{
			return pActor.isFlying();
		}
	}
}
