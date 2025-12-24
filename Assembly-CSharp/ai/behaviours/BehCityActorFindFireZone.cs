using System;
using UnityEngine;

namespace ai.behaviours
{
	// Token: 0x020008A4 RID: 2212
	public class BehCityActorFindFireZone : BehCityActor
	{
		// Token: 0x06004499 RID: 17561 RVA: 0x001CE5A0 File Offset: 0x001CC7A0
		public override BehResult execute(Actor pActor)
		{
			if (pActor.hasStatus("burning"))
			{
				return BehResult.Stop;
			}
			TileZone tBestZone = BehCityActorFindFireZone.getCityZoneNearFire(pActor);
			if (tBestZone == null)
			{
				return BehResult.Stop;
			}
			WorldTile tResultTile = BehCityActorFindFireZone.getClosestTileNotOnFire(tBestZone.tiles, pActor.current_tile);
			if (tResultTile == null)
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = tResultTile;
			return BehResult.Continue;
		}

		// Token: 0x0600449A RID: 17562 RVA: 0x001CE5E8 File Offset: 0x001CC7E8
		private static TileZone getCityZoneNearFire(Actor pActor)
		{
			TileZone result;
			using (ListPool<TileZone> tZonesToCheck = new ListPool<TileZone>(pActor.city.zones))
			{
				int tBestDist = int.MaxValue;
				TileZone tBestZone = null;
				foreach (TileZone tZone in pActor.city.neighbour_zones)
				{
					if (!tZone.hasCity())
					{
						tZonesToCheck.Add(tZone);
					}
				}
				Vector2Int tActorPos = pActor.current_tile.pos;
				for (int i = 0; i < tZonesToCheck.Count; i++)
				{
					TileZone tZone2 = tZonesToCheck[i];
					if (tZone2.isZoneOnFire())
					{
						Vector2Int tCenterZoneTilePos = tZone2.centerTile.pos;
						int tDist = Toolbox.SquaredDist(tActorPos.x, tActorPos.y, tCenterZoneTilePos.x, tCenterZoneTilePos.y);
						if (tDist < tBestDist)
						{
							tBestZone = tZone2;
							tBestDist = tDist;
						}
					}
				}
				tZonesToCheck.Clear();
				result = tBestZone;
			}
			return result;
		}

		// Token: 0x0600449B RID: 17563 RVA: 0x001CE6F8 File Offset: 0x001CC8F8
		public static WorldTile getClosestTileNotOnFire(WorldTile[] pArray, WorldTile pTarget)
		{
			WorldTile tBest = null;
			int tCount = pArray.Length;
			int tBestDist = int.MaxValue;
			for (int i = 0; i < tCount; i++)
			{
				WorldTile tTile = pArray[i];
				int tDist = Toolbox.SquaredDist(pTarget.x, pTarget.y, tTile.x, tTile.y);
				if (tDist < tBestDist && !tTile.hasBuilding() && !tTile.isOnFire())
				{
					tBestDist = tDist;
					tBest = tTile;
				}
			}
			return tBest;
		}
	}
}
