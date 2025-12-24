using System;

namespace ai.behaviours
{
	// Token: 0x02000966 RID: 2406
	public class BehUFOSelectTarget : BehaviourActionActor
	{
		// Token: 0x06004686 RID: 18054 RVA: 0x001DEA3C File Offset: 0x001DCC3C
		public override BehResult execute(Actor pActor)
		{
			UFO tUFO = pActor.getActorComponent<UFO>();
			if (tUFO.aggroTargets.Count > 0)
			{
				BehaviourActionActor.temp_actors.Clear();
				foreach (Actor tActor in tUFO.aggroTargets)
				{
					if (tActor != null && tActor.isAlive())
					{
						BehaviourActionActor.temp_actors.Add(tActor);
					}
				}
				tUFO.aggroTargets.Clear();
				Actor tTarget = Toolbox.getClosestActor(BehaviourActionActor.temp_actors, pActor.current_tile);
				if (tTarget != null)
				{
					if (tTarget.city != null)
					{
						long cityToAttack;
						pActor.data.get("cityToAttack", out cityToAttack, -1L);
						if (!cityToAttack.hasValue())
						{
							pActor.data.set("cityToAttack", tTarget.city.data.id);
							pActor.data.set("attacksForCity", Randy.randomInt(3, 10));
						}
					}
					pActor.beh_tile_target = tTarget.current_tile;
					return base.forceTask(pActor, "ufo_chase", false, false);
				}
			}
			return BehResult.Continue;
		}
	}
}
