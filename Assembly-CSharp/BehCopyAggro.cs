using System;
using ai.behaviours;

// Token: 0x020003D8 RID: 984
public class BehCopyAggro : BehaviourActionActor
{
	// Token: 0x06002288 RID: 8840 RVA: 0x00121A98 File Offset: 0x0011FC98
	public override BehResult execute(Actor pActor)
	{
		BaseSimObject beh_actor_target = pActor.beh_actor_target;
		Actor tTarget = (beh_actor_target != null) ? beh_actor_target.a : null;
		if (tTarget == null)
		{
			return BehResult.Continue;
		}
		pActor.copyAggroFrom(tTarget);
		this.copyEnemiesOf(pActor, tTarget);
		return BehResult.Continue;
	}

	// Token: 0x06002289 RID: 8841 RVA: 0x00121AD0 File Offset: 0x0011FCD0
	private void copyEnemiesOf(Actor pCopyTo, Actor pTarget)
	{
		foreach (Actor tPossibleEnemy in Finder.getUnitsFromChunk(pTarget.current_tile, 1, 0f, true))
		{
			if (tPossibleEnemy != pCopyTo && tPossibleEnemy.isInAggroList(pTarget) && pCopyTo.isSameIslandAs(tPossibleEnemy))
			{
				pCopyTo.addAggro(tPossibleEnemy);
			}
		}
	}
}
