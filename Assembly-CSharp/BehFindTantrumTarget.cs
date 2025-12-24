using System;
using ai.behaviours;

// Token: 0x020003C5 RID: 965
public class BehFindTantrumTarget : BehaviourActionActor
{
	// Token: 0x0600224C RID: 8780 RVA: 0x0012063C File Offset: 0x0011E83C
	public override BehResult execute(Actor pActor)
	{
		if (pActor.beh_actor_target != null && pActor.isTargetOkToAttack(pActor.beh_actor_target.a))
		{
			return BehResult.Continue;
		}
		Actor tTarget = this.getClosestActor(pActor);
		if (tTarget == null)
		{
			return base.forceTask(pActor, "random_move", true, false);
		}
		pActor.beh_actor_target = tTarget;
		return BehResult.Continue;
	}

	// Token: 0x0600224D RID: 8781 RVA: 0x00120688 File Offset: 0x0011E888
	private Actor getClosestActor(Actor pActor)
	{
		bool tRandomShuffle = Randy.randomBool();
		WorldTile tTile = pActor.current_tile;
		float tBestDist = 2.1474836E+09f;
		Actor tBestActor = null;
		foreach (Actor tTargetToCheck in Finder.getUnitsFromChunk(tTile, 1, 0f, tRandomShuffle))
		{
			float tDist = (float)Toolbox.SquaredDistTile(tTargetToCheck.current_tile, tTile);
			if (tDist < tBestDist && pActor.isTargetOkToAttack(tTargetToCheck) && (!tTargetToCheck.hasStatusStunned() || pActor.areFoes(tTargetToCheck)))
			{
				tBestDist = tDist;
				tBestActor = tTargetToCheck;
				if (Randy.randomBool())
				{
					break;
				}
			}
		}
		return tBestActor;
	}
}
