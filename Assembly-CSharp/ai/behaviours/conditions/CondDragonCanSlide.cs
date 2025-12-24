using System;

namespace ai.behaviours.conditions
{
	// Token: 0x0200097F RID: 2431
	public class CondDragonCanSlide : BehaviourActorCondition
	{
		// Token: 0x060046F6 RID: 18166 RVA: 0x001E1FF4 File Offset: 0x001E01F4
		public override bool check(Actor pActor)
		{
			if (!pActor.getActorComponent<Dragon>().hasTargetsForSlide())
			{
				return false;
			}
			bool tJustSlid;
			pActor.data.get("justSlid", out tJustSlid, false);
			pActor.data.removeBool("justSlid");
			return !tJustSlid;
		}
	}
}
