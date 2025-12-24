using System;
using ai.behaviours;

// Token: 0x020003C9 RID: 969
public class BehFindLover : BehaviourActionActor
{
	// Token: 0x06002257 RID: 8791 RVA: 0x00120BCE File Offset: 0x0011EDCE
	public override bool shouldRetry(Actor pActor)
	{
		return (pActor.hasCity() && BehaviourActionBase<Actor>.world.cities.isLocked()) || base.shouldRetry(pActor);
	}

	// Token: 0x06002258 RID: 8792 RVA: 0x00120BF4 File Offset: 0x0011EDF4
	public override BehResult execute(Actor pActor)
	{
		if (pActor.hasLover())
		{
			return BehResult.Stop;
		}
		Actor tResultTarget = this.findLoverAround(pActor);
		if (tResultTarget == null)
		{
			tResultTarget = this.checkCityLovers(pActor);
		}
		if (tResultTarget != null)
		{
			pActor.becomeLoversWith(tResultTarget);
		}
		return BehResult.Continue;
	}

	// Token: 0x06002259 RID: 8793 RVA: 0x00120C2C File Offset: 0x0011EE2C
	private Actor findLoverAround(Actor pActor)
	{
		Actor tResultTarget = null;
		foreach (Actor tPotentialLover in Finder.getUnitsFromChunk(pActor.current_tile, 1, 0f, false))
		{
			if (this.checkIfPossibleLover(pActor, tPotentialLover))
			{
				tResultTarget = tPotentialLover;
				break;
			}
		}
		return tResultTarget;
	}

	// Token: 0x0600225A RID: 8794 RVA: 0x00120C90 File Offset: 0x0011EE90
	private bool checkIfPossibleLover(Actor pActor, Actor pTarget)
	{
		return pTarget != pActor && pTarget.hasSubspecies() && pTarget.isAlive() && pTarget.canFallInLoveWith(pActor);
	}

	// Token: 0x0600225B RID: 8795 RVA: 0x00120CB8 File Offset: 0x0011EEB8
	private Actor checkCityLovers(Actor pActor)
	{
		if (!pActor.hasCity())
		{
			return null;
		}
		Actor tResultTarget = null;
		foreach (Actor tPotentialLover in pActor.city.getUnits().LoopRandom<Actor>())
		{
			if (this.checkIfPossibleLover(pActor, tPotentialLover) && tPotentialLover.inOwnCityBorders())
			{
				tResultTarget = tPotentialLover;
				break;
			}
		}
		return tResultTarget;
	}
}
