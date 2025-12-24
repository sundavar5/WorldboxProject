using System;
using ai.behaviours;

// Token: 0x020003DC RID: 988
public class BehSocializeStartCheck : BehaviourActionActor
{
	// Token: 0x060022A0 RID: 8864 RVA: 0x001225C0 File Offset: 0x001207C0
	public override BehResult execute(Actor pActor)
	{
		base.execute(pActor);
		if (pActor.hasTelepathicLink())
		{
			return base.forceTask(pActor, "socialize_try_to_start_immediate", false, false);
		}
		if (pActor.hasCity() && pActor.city.hasBuildingType("type_bonfire", true, pActor.current_island))
		{
			return base.forceTask(pActor, "socialize_try_to_start_near_bonfire", false, false);
		}
		return base.forceTask(pActor, "socialize_try_to_start_immediate", false, false);
	}
}
