using System;
using UnityEngine;

namespace ai.behaviours
{
	// Token: 0x0200088B RID: 2187
	public class BehAttackActorHuntingTarget : BehaviourActionActor
	{
		// Token: 0x0600445B RID: 17499 RVA: 0x001CDB8C File Offset: 0x001CBD8C
		public override BehResult execute(Actor pActor)
		{
			BaseSimObject tTarget = pActor.beh_actor_target;
			if (pActor.isInWaterAndCantAttack())
			{
				return BehResult.Stop;
			}
			if (tTarget == null || !tTarget.isAlive())
			{
				pActor.makeWait(0.5f);
				return BehResult.Continue;
			}
			if (!pActor.isInAttackRange(tTarget))
			{
				return BehResult.StepBack;
			}
			bool flag = pActor.tryToAttack(tTarget, true, null, default(Vector3), null, null, 0f);
			if (flag && pActor.hasRangeAttack())
			{
				pActor.makeWait(0.5f);
			}
			if (flag && !tTarget.isAlive())
			{
				return BehResult.Continue;
			}
			if (tTarget.isAlive())
			{
				return BehResult.RepeatStep;
			}
			return BehResult.Continue;
		}
	}
}
