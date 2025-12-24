using System;

namespace ai.behaviours.conditions
{
	// Token: 0x02000981 RID: 2433
	public class CondDragonHasTargets : BehaviourActorCondition
	{
		// Token: 0x060046FA RID: 18170 RVA: 0x001E2087 File Offset: 0x001E0287
		public override bool check(Actor pActor)
		{
			return pActor.getActorComponent<Dragon>().aggroTargets.Count > 0;
		}
	}
}
