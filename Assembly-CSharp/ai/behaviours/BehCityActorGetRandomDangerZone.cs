using System;

namespace ai.behaviours
{
	// Token: 0x020008A9 RID: 2217
	public class BehCityActorGetRandomDangerZone : BehCityActor
	{
		// Token: 0x060044A5 RID: 17573 RVA: 0x001CE8DC File Offset: 0x001CCADC
		public override BehResult execute(Actor pActor)
		{
			City tCity = pActor.city;
			if (!tCity.hasZones())
			{
				return BehResult.Stop;
			}
			if (!tCity.isInDanger())
			{
				return BehResult.Stop;
			}
			if (Randy.randomChance(0.2f))
			{
				foreach (TileZone tileZone in tCity.danger_zones)
				{
					WorldTile tTile = tileZone.tiles.GetRandom<WorldTile>();
					if (tTile.isSameIsland(pActor.current_tile))
					{
						pActor.beh_tile_target = tTile;
						return BehResult.Continue;
					}
				}
			}
			int tMinDist = int.MaxValue;
			WorldTile tBestTile = null;
			foreach (TileZone tileZone2 in tCity.danger_zones)
			{
				WorldTile tZoneTile = tileZone2.centerTile;
				int tDist = Toolbox.SquaredDistTile(pActor.current_tile, tZoneTile);
				if (tDist <= tMinDist && tZoneTile.isSameIsland(pActor.current_tile) && (tDist != tMinDist || !Randy.randomBool()))
				{
					tMinDist = tDist;
					tBestTile = tZoneTile;
				}
			}
			if (tBestTile != null)
			{
				pActor.beh_tile_target = tBestTile;
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
