using System;

namespace ai.behaviours
{
	// Token: 0x020008AA RID: 2218
	public class BehCityActorGetRandomIdleTile : BehCityActor
	{
		// Token: 0x060044A7 RID: 17575 RVA: 0x001CEA10 File Offset: 0x001CCC10
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.city.hasZones())
			{
				return BehResult.Stop;
			}
			WorldTile tResultTile = this.tryToGetBonfireTile(pActor);
			if (tResultTile == null)
			{
				tResultTile = this.getRandomCityZoneTile(pActor);
			}
			if (tResultTile == null)
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = tResultTile;
			return BehResult.Continue;
		}

		// Token: 0x060044A8 RID: 17576 RVA: 0x001CEA4C File Offset: 0x001CCC4C
		private WorldTile getRandomCityZoneTile(Actor pActor)
		{
			WorldTile tTile;
			if (!pActor.current_tile.isSameCityHere(pActor.city) || Randy.randomChance(0.2f))
			{
				tTile = pActor.city.zones.GetRandom<TileZone>().tiles.GetRandom<WorldTile>();
				if (!tTile.isSameIsland(pActor.current_tile))
				{
					return null;
				}
			}
			else
			{
				tTile = pActor.current_tile.region.tiles.GetRandom<WorldTile>();
			}
			return tTile;
		}

		// Token: 0x060044A9 RID: 17577 RVA: 0x001CEABC File Offset: 0x001CCCBC
		private WorldTile tryToGetBonfireTile(Actor pActor)
		{
			Building tBuilding = pActor.city.getBuildingOfType("type_bonfire", true, true, false, pActor.current_island);
			if (tBuilding == null)
			{
				return null;
			}
			WorldTile tResultTile = tBuilding.tiles.GetRandom<WorldTile>();
			if (Randy.randomChance(0.3f))
			{
				MapRegion tRegion = tBuilding.current_tile.region;
				if (tRegion.neighbours.Count > 0 && Randy.randomChance(0.3f))
				{
					tRegion = tRegion.neighbours.GetRandom<MapRegion>();
				}
				tResultTile = tRegion.tiles.GetRandom<WorldTile>();
			}
			return tResultTile;
		}
	}
}
