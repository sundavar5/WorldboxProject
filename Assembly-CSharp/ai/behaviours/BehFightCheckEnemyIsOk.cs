using System;

namespace ai.behaviours
{
	// Token: 0x020008BE RID: 2238
	public class BehFightCheckEnemyIsOk : BehaviourActionActor
	{
		// Token: 0x060044E1 RID: 17633 RVA: 0x001CF908 File Offset: 0x001CDB08
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.has_attack_target)
			{
				return BehResult.Stop;
			}
			if (!pActor.isEnemyTargetAlive())
			{
				return BehResult.Stop;
			}
			if (!pActor.shouldContinueToAttackTarget())
			{
				pActor.clearAttackTarget();
				return BehResult.Stop;
			}
			if (!pActor.canAttackTarget(pActor.attack_target, true, true))
			{
				pActor.ignoreTarget(pActor.attack_target);
				pActor.clearAttackTarget();
				return BehResult.Stop;
			}
			if (!pActor.isInAttackRange(pActor.attack_target))
			{
				if (pActor.isWaterCreature())
				{
					if ((!pActor.attack_target.isInLiquid() && !pActor.asset.force_land_creature) || pActor.attack_target.isFlying())
					{
						pActor.ignoreTarget(pActor.attack_target);
						pActor.clearAttackTarget();
						return BehResult.Stop;
					}
				}
				else if ((pActor.attack_target.isInLiquid() && !pActor.isWaterCreature()) || pActor.attack_target.isFlying())
				{
					pActor.ignoreTarget(pActor.attack_target);
					pActor.clearAttackTarget();
					return BehResult.Stop;
				}
			}
			int x = pActor.chunk.x;
			int tCurY = pActor.chunk.y;
			int tTargetX = pActor.attack_target.chunk.x;
			int tTargetY = pActor.attack_target.chunk.y;
			float tBonusDist = 1f;
			if (Toolbox.Dist(x, tCurY, tTargetX, tTargetY) >= (float)SimGlobals.m.unit_chunk_sight_range + tBonusDist)
			{
				pActor.clearAttackTarget();
				return BehResult.Stop;
			}
			pActor.beh_actor_target = pActor.attack_target;
			return BehResult.Continue;
		}
	}
}
