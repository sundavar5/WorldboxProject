using System;

namespace ai.behaviours
{
	// Token: 0x020008CE RID: 2254
	public class BehFindRandomTile4Directions : BehaviourActionActor
	{
		// Token: 0x06004502 RID: 17666 RVA: 0x001D02F8 File Offset: 0x001CE4F8
		public override BehResult execute(Actor pActor)
		{
			int direction_index;
			pActor.data.get("direction", out direction_index, -1);
			ActorDirection currentDirection;
			if (direction_index == -1)
			{
				currentDirection = Randy.getRandom<ActorDirection>(Toolbox.directions);
				direction_index = Toolbox.directions.IndexOf(currentDirection);
				pActor.data.set("direction", direction_index);
			}
			else
			{
				currentDirection = Toolbox.directions[direction_index];
			}
			pActor.beh_tile_target = Ant.getNextTile(pActor.current_tile, currentDirection);
			if (pActor.beh_tile_target == null)
			{
				pActor.beh_tile_target = Ant.randomNeighbour(pActor.current_tile);
			}
			return BehResult.Continue;
		}
	}
}
