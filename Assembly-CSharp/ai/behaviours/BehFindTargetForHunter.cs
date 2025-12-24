using System;

namespace ai.behaviours
{
	// Token: 0x020008D0 RID: 2256
	public class BehFindTargetForHunter : BehCityActor
	{
		// Token: 0x06004506 RID: 17670 RVA: 0x001D04A8 File Offset: 0x001CE6A8
		public override BehResult execute(Actor pActor)
		{
			if (pActor.beh_actor_target != null && pActor.isTargetOkToAttack(pActor.beh_actor_target.a))
			{
				return BehResult.Continue;
			}
			pActor.beh_actor_target = this.getClosestMeatActor(pActor, 3, false);
			if (pActor.beh_actor_target != null)
			{
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}

		// Token: 0x06004507 RID: 17671 RVA: 0x001D04E4 File Offset: 0x001CE6E4
		private Actor getClosestMeatActor(Actor pActor, int pMinAge = 0, bool pCheckSame = false)
		{
			BehaviourActionActor.temp_actors.Clear();
			foreach (Actor tTarget in Finder.getUnitsFromChunk(pActor.current_tile, 3, 0f, false))
			{
				if (!tTarget.isSameKingdom(pActor) && pActor.isTargetOkToAttack(tTarget) && tTarget.asset.source_meat && (pMinAge <= 0 || tTarget.getAge() >= pMinAge))
				{
					BehaviourActionActor.temp_actors.Add(tTarget);
				}
			}
			return Toolbox.getClosestActor(BehaviourActionActor.temp_actors, pActor.current_tile);
		}
	}
}
