using System;
using ai.behaviours;

// Token: 0x0200039A RID: 922
public class BehFindRandomTileAroundHouse : BehaviourActionActor
{
	// Token: 0x060021C0 RID: 8640 RVA: 0x0011D758 File Offset: 0x0011B958
	public override BehResult execute(Actor pActor)
	{
		Building tHomeBuilding = pActor.getHomeBuilding();
		if (tHomeBuilding == null)
		{
			return BehResult.Stop;
		}
		if (!tHomeBuilding.current_tile.isSameIsland(pActor.current_tile))
		{
			return BehResult.Stop;
		}
		MapRegion tRegion = tHomeBuilding.current_tile.region;
		if (Randy.randomChance(0.2f) && tRegion.neighbours.Count > 0)
		{
			tRegion = tRegion.neighbours.GetRandom<MapRegion>();
		}
		WorldTile tResultTile = tRegion.tiles.GetRandom<WorldTile>();
		pActor.beh_tile_target = tResultTile;
		return BehResult.Continue;
	}
}
