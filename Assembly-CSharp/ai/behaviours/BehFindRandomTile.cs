using System;

namespace ai.behaviours
{
	// Token: 0x020008CD RID: 2253
	public class BehFindRandomTile : BehaviourActionActor
	{
		// Token: 0x06004500 RID: 17664 RVA: 0x001D0264 File Offset: 0x001CE464
		public override BehResult execute(Actor pActor)
		{
			MapRegion tRegion = pActor.current_tile.region;
			if (Randy.randomChance(0.65f) && tRegion.tiles.Count > 0)
			{
				pActor.beh_tile_target = tRegion.tiles.GetRandom<WorldTile>();
				return BehResult.Continue;
			}
			if (tRegion.neighbours.Count > 0 && Randy.randomBool())
			{
				tRegion = tRegion.neighbours.GetRandom<MapRegion>();
			}
			if (tRegion.tiles.Count > 0)
			{
				pActor.beh_tile_target = tRegion.tiles.GetRandom<WorldTile>();
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
