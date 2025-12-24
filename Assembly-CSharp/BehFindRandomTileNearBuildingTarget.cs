using System;
using ai.behaviours;

// Token: 0x0200039B RID: 923
public class BehFindRandomTileNearBuildingTarget : BehaviourActionActor
{
	// Token: 0x060021C2 RID: 8642 RVA: 0x0011D7D4 File Offset: 0x0011B9D4
	public override BehResult execute(Actor pActor)
	{
		if (pActor.beh_building_target == null)
		{
			return BehResult.Stop;
		}
		if (!pActor.beh_building_target.current_tile.isSameIsland(pActor.current_tile))
		{
			return BehResult.Stop;
		}
		MapRegion tRegion = pActor.beh_building_target.current_tile.region;
		if (Randy.randomChance(0.2f) && tRegion.neighbours.Count > 0)
		{
			tRegion = tRegion.neighbours.GetRandom<MapRegion>();
		}
		WorldTile tResultTile = tRegion.tiles.GetRandom<WorldTile>();
		pActor.beh_tile_target = tResultTile;
		return BehResult.Continue;
	}
}
