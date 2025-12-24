using System;

namespace ai.behaviours
{
	// Token: 0x020008CF RID: 2255
	public class BehFindRandomTile8Directions : BehaviourActionActor
	{
		// Token: 0x06004504 RID: 17668 RVA: 0x001D0384 File Offset: 0x001CE584
		public override BehResult execute(Actor pActor)
		{
			int tRandomSteps;
			pActor.data.get("random_steps", out tRandomSteps, 0);
			int tDirectionIndex;
			pActor.data.get("direction", out tDirectionIndex, -1);
			ActorDirection tCurrentDirection;
			if (tRandomSteps > 0)
			{
				if (pActor.beh_tile_target != null && pActor.current_tile != pActor.beh_tile_target)
				{
					return BehResult.Continue;
				}
				tCurrentDirection = Toolbox.directions_all[tDirectionIndex];
			}
			else
			{
				int pMinInclusive = Randy.randomInt(1, 6);
				int tStepsMax = Randy.randomInt(10, 60);
				tRandomSteps = Randy.randomInt(pMinInclusive, tStepsMax);
				if (tDirectionIndex < 0)
				{
					tCurrentDirection = Randy.getRandom<ActorDirection>(Toolbox.directions_all);
					tDirectionIndex = Toolbox.directions_all.IndexOf(tCurrentDirection);
				}
				else
				{
					tCurrentDirection = Toolbox.directions_all[tDirectionIndex];
					tCurrentDirection = Randy.getRandom<ActorDirection>(Toolbox.directions_all_turns[tCurrentDirection]);
					tDirectionIndex = Toolbox.directions_all.IndexOf(tCurrentDirection);
				}
				pActor.data.set("direction", tDirectionIndex);
			}
			tRandomSteps--;
			pActor.beh_tile_target = Ant.getNextTile(pActor.current_tile, tCurrentDirection);
			if (pActor.beh_tile_target == null)
			{
				pActor.data.set("random_steps", 0);
				pActor.data.set("direction", -1);
				return BehResult.RepeatStep;
			}
			pActor.data.set("random_steps", tRandomSteps);
			return BehResult.Continue;
		}
	}
}
