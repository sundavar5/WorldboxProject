using System;
using ai.behaviours;

// Token: 0x020003D5 RID: 981
public class BehAffectDreams : BehaviourActionActor
{
	// Token: 0x06002280 RID: 8832 RVA: 0x00121838 File Offset: 0x0011FA38
	public override BehResult execute(Actor pActor)
	{
		Actor tActorTarget = this.getRandomDreamingActor(pActor);
		if (tActorTarget == null)
		{
			return BehResult.Stop;
		}
		tActorTarget.tryToConvertActorToMetaFromActor(pActor, true);
		return BehResult.Continue;
	}

	// Token: 0x06002281 RID: 8833 RVA: 0x0012185C File Offset: 0x0011FA5C
	private Actor getRandomDreamingActor(Actor pActor)
	{
		BehaviourActionBase<Actor>.world.units.checkSleepingUnits();
		if (BehaviourActionBase<Actor>.world.units.cached_sleeping_units.Count == 0)
		{
			return null;
		}
		foreach (Actor tActor in BehaviourActionBase<Actor>.world.units.cached_sleeping_units.LoopRandom<Actor>())
		{
			if (tActor.isAlive() && tActor.hasSubspecies() && tActor.hasStatus("sleeping") && (tActor.subspecies.has_advanced_memory || tActor.subspecies.has_advanced_communication))
			{
				return tActor;
			}
		}
		return null;
	}
}
