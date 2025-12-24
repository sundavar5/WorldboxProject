using System;
using ai.behaviours;

// Token: 0x020003C2 RID: 962
public class BehFindTargetToStealFrom : BehaviourActionActor
{
	// Token: 0x06002244 RID: 8772 RVA: 0x00120430 File Offset: 0x0011E630
	public override BehResult execute(Actor pActor)
	{
		Actor tTarget = this.getClosestActorWithMoneys(pActor);
		if (tTarget == null)
		{
			return BehResult.Stop;
		}
		pActor.beh_actor_target = tTarget;
		return BehResult.Continue;
	}

	// Token: 0x06002245 RID: 8773 RVA: 0x00120454 File Offset: 0x0011E654
	private Actor getClosestActorWithMoneys(Actor pActor)
	{
		Actor closestActor;
		using (ListPool<Actor> tTempActors = new ListPool<Actor>(4))
		{
			bool tRandomShuffle = Randy.randomBool();
			int tChunkRange = Randy.randomInt(1, 4);
			int tMaxUnits = Randy.randomInt(1, 4);
			foreach (Actor tTarget in Finder.getUnitsFromChunk(pActor.current_tile, tChunkRange, 0f, tRandomShuffle))
			{
				if (tTarget != pActor && pActor.isSameIslandAs(tTarget) && tTarget.hasAnyCash())
				{
					tTempActors.Add(tTarget);
					if (tTempActors.Count >= tMaxUnits)
					{
						break;
					}
				}
			}
			closestActor = Toolbox.getClosestActor(tTempActors, pActor.current_tile);
		}
		return closestActor;
	}
}
