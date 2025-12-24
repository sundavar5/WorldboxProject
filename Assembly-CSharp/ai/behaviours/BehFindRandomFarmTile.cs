using System;

namespace ai.behaviours
{
	// Token: 0x020008CB RID: 2251
	public class BehFindRandomFarmTile : BehaviourActionActor
	{
		// Token: 0x060044FC RID: 17660 RVA: 0x001D019C File Offset: 0x001CE39C
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
