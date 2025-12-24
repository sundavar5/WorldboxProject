using System;

namespace ai.behaviours
{
	// Token: 0x020008AB RID: 2219
	public class BehCityActorGetRandomZoneTile : BehCityActor
	{
		// Token: 0x060044AB RID: 17579 RVA: 0x001CEB48 File Offset: 0x001CCD48
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.city.hasZones())
			{
				return BehResult.Stop;
			}
			WorldTile tTile;
			if (pActor.current_tile.zone.city != pActor.city || Randy.randomChance(0.2f))
			{
				tTile = pActor.city.zones.GetRandom<TileZone>().getRandomTile();
				if (!tTile.isSameIsland(pActor.current_tile))
				{
					return BehResult.Stop;
				}
			}
			else
			{
				tTile = pActor.current_tile.region.tiles.GetRandom<WorldTile>();
			}
			pActor.beh_tile_target = tTile;
			return BehResult.Continue;
		}
	}
}
