using System;

namespace ai.behaviours
{
	// Token: 0x020008BA RID: 2234
	public class BehFamilyFollowAlpha : BehaviourActionActor
	{
		// Token: 0x060044D8 RID: 17624 RVA: 0x001CF728 File Offset: 0x001CD928
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.hasFamily())
			{
				return BehResult.Stop;
			}
			if (pActor.family.isAlpha(pActor))
			{
				return BehResult.Stop;
			}
			pActor.family.checkAlpha();
			Actor tTarget = pActor.family.getAlpha();
			if (pActor.family.isAlpha(pActor))
			{
				return BehResult.Stop;
			}
			if (tTarget != null && !tTarget.current_tile.isSameIsland(pActor.current_tile))
			{
				tTarget = null;
			}
			if (tTarget == null)
			{
				return BehResult.Stop;
			}
			if (pActor.distanceToObjectTarget(tTarget) > 400f)
			{
				return BehResult.Stop;
			}
			pActor.beh_actor_target = tTarget;
			return BehResult.Continue;
		}
	}
}
