using System;
using ai.behaviours;

// Token: 0x020003DE RID: 990
public class BehTryToSocialize : BehaviourActionActor
{
	// Token: 0x060022A3 RID: 8867 RVA: 0x00122644 File Offset: 0x00120844
	public override BehResult execute(Actor pActor)
	{
		pActor.resetSocialize();
		Actor tTarget = this.getRandomActorAround(pActor);
		if (tTarget == null)
		{
			return BehResult.Stop;
		}
		pActor.beh_actor_target = tTarget;
		if (pActor.canFallInLoveWith(tTarget))
		{
			pActor.becomeLoversWith(tTarget);
		}
		pActor.resetSocialize();
		tTarget.resetSocialize();
		if (pActor.hasTelepathicLink() && tTarget.hasTelepathicLink())
		{
			return base.forceTask(pActor, "socialize_do_talk", false, false);
		}
		return base.forceTask(pActor, "socialize_go_to_target", false, false);
	}

	// Token: 0x060022A4 RID: 8868 RVA: 0x001226B4 File Offset: 0x001208B4
	private Actor getRandomActorAround(Actor pActor)
	{
		Actor result;
		using (ListPool<Actor> tBestTargets = new ListPool<Actor>(4))
		{
			using (ListPool<Actor> tAllTargets = new ListPool<Actor>(4))
			{
				bool tNeedOppositeSex = pActor.subspecies.needOppositeSexTypeForReproduction();
				bool tHasAnimalWhispererTrait = pActor.hasCulture() && pActor.culture.hasTrait("animal_whisperers");
				bool flag = pActor.hasTelepathicLink();
				if (flag)
				{
					this.fillUnitsViaTelepathicLink(pActor, tBestTargets, tAllTargets);
				}
				int tChunkRange = 1;
				if (flag)
				{
					tChunkRange = 2;
				}
				foreach (Actor tSocializeTarget in Finder.getUnitsFromChunk(pActor.current_tile, tChunkRange, 0f, true))
				{
					if (pActor.canTalkWith(tSocializeTarget))
					{
						if (pActor.isKingdomCiv())
						{
							if (tSocializeTarget.isKingdomMob())
							{
								if (!tHasAnimalWhispererTrait)
								{
									continue;
								}
							}
							else if (tSocializeTarget.isKingdomCiv())
							{
							}
						}
						else if (!pActor.isSameSpecies(tSocializeTarget))
						{
							continue;
						}
						if (tNeedOppositeSex && pActor.canFallInLoveWith(tSocializeTarget))
						{
							tBestTargets.Add(tSocializeTarget);
							break;
						}
						tAllTargets.Add(tSocializeTarget);
						if (tAllTargets.Count > 3)
						{
							break;
						}
					}
				}
				if (tBestTargets.Count > 0)
				{
					result = tBestTargets.GetRandom<Actor>();
				}
				else if (tAllTargets.Count > 0)
				{
					result = tAllTargets.GetRandom<Actor>();
				}
				else
				{
					result = null;
				}
			}
		}
		return result;
	}

	// Token: 0x060022A5 RID: 8869 RVA: 0x0012283C File Offset: 0x00120A3C
	private void fillUnitsViaTelepathicLink(Actor pActor, ListPool<Actor> pBestTargets, ListPool<Actor> pNormalTargets)
	{
		if (pActor.hasFamily())
		{
			foreach (Actor tActor in pActor.family.units)
			{
				if (pActor.canTalkWith(tActor))
				{
					pNormalTargets.Add(tActor);
				}
			}
		}
		foreach (Actor tParent in pActor.getParents())
		{
			if (pActor.canTalkWith(tParent))
			{
				pBestTargets.Add(tParent);
			}
		}
	}
}
