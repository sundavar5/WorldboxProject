using System;

namespace ai.behaviours.conditions
{
	// Token: 0x0200097D RID: 2429
	public class CondDragonCanLand : BehaviourActorCondition
	{
		// Token: 0x060046F2 RID: 18162 RVA: 0x001E1E64 File Offset: 0x001E0064
		public override bool check(Actor pActor)
		{
			if (!Dragon.canLand(pActor, null))
			{
				return false;
			}
			if (pActor.getActorComponent<Dragon>().lastLanded == pActor.current_tile)
			{
				return false;
			}
			bool tJustUp;
			pActor.data.get("justUp", out tJustUp, false);
			if (tJustUp)
			{
				pActor.data.removeBool("justUp");
				return false;
			}
			return true;
		}
	}
}
