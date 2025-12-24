using System;

namespace ai.behaviours
{
	// Token: 0x020008CA RID: 2250
	public class BehFindRandomCivBuildingTile : BehaviourActionActor
	{
		// Token: 0x060044FA RID: 17658 RVA: 0x001D00AC File Offset: 0x001CE2AC
		public override BehResult execute(Actor pActor)
		{
			MapRegion tRegion = pActor.current_tile.region;
			if (Randy.randomChance(0.65f) && tRegion.tiles.Count > 0)
			{
				pActor.beh_tile_target = tRegion.getRandomTile();
				return BehResult.Continue;
			}
			Building tTarget = null;
			foreach (Building tBuilding in Finder.getBuildingsFromChunk(pActor.current_tile, 1, 0, true))
			{
				if (tBuilding.asset.city_building && tBuilding.current_tile.isSameIsland(pActor.current_tile) && tBuilding.isCiv())
				{
					tTarget = tBuilding;
					break;
				}
			}
			if (tTarget != null)
			{
				pActor.beh_tile_target = tTarget.current_tile.region.getRandomTile();
				return BehResult.Continue;
			}
			if (tRegion.tiles.Count > 0)
			{
				pActor.beh_tile_target = tRegion.getRandomTile();
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
