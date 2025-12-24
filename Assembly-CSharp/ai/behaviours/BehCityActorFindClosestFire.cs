using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x020008A3 RID: 2211
	public class BehCityActorFindClosestFire : BehCityActor
	{
		// Token: 0x06004494 RID: 17556 RVA: 0x001CE3D0 File Offset: 0x001CC5D0
		public override BehResult execute(Actor pActor)
		{
			WorldTile tActorTile = pActor.current_tile;
			WorldTile tFireTile = BehCityActorFindClosestFire.getClosestTileOnFireFromZones(pActor);
			if (tFireTile == null)
			{
				return BehResult.Stop;
			}
			if (pActor.is_visible)
			{
				pActor.spawnSlashYell(tFireTile.pos);
			}
			WorldTile tGoToTile = BehCityActorFindClosestFire.raycastTileForUnitToFightFire(tActorTile, tFireTile);
			if (tGoToTile == null)
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = tGoToTile;
			return BehResult.Continue;
		}

		// Token: 0x06004495 RID: 17557 RVA: 0x001CE420 File Offset: 0x001CC620
		private static WorldTile getClosestTileOnFireFromZones(Actor pActor)
		{
			WorldTile tActorTile = pActor.current_tile;
			TileZone tCurrentZone = pActor.current_zone;
			WorldTile tFireTile = BehCityActorFindClosestFire.getClosestTileOnFire(tCurrentZone.tiles, tActorTile);
			if (tFireTile == null)
			{
				foreach (TileZone tZone in tCurrentZone.neighbours_all.LoopRandom<TileZone>())
				{
					if (tZone.isZoneOnFire())
					{
						tFireTile = BehCityActorFindClosestFire.getClosestTileOnFire(tZone.tiles, tActorTile);
						if (tFireTile != null)
						{
							return tFireTile;
						}
					}
				}
				return tFireTile;
			}
			return tFireTile;
		}

		// Token: 0x06004496 RID: 17558 RVA: 0x001CE4B0 File Offset: 0x001CC6B0
		private static WorldTile getClosestTileOnFire(WorldTile[] pArray, WorldTile pTarget)
		{
			WorldTile tBest = null;
			int tCount = pArray.Length;
			int tBestDist = int.MaxValue;
			for (int i = 0; i < tCount; i++)
			{
				WorldTile tTile = pArray[i];
				int tDist = Toolbox.SquaredDist(pTarget.x, pTarget.y, tTile.x, tTile.y);
				if (tDist < tBestDist && tTile.isOnFire())
				{
					tBestDist = tDist;
					tBest = tTile;
				}
			}
			return tBest;
		}

		// Token: 0x06004497 RID: 17559 RVA: 0x001CE518 File Offset: 0x001CC718
		public static WorldTile raycastTileForUnitToFightFire(WorldTile pActorTile, WorldTile pTargetFire)
		{
			if (pActorTile == pTargetFire)
			{
				return pActorTile;
			}
			List<WorldTile> tRaycastResult = PathfinderTools.raycast(pTargetFire, pActorTile, 0.99f);
			WorldTile tResult = null;
			float tBestDist = float.MaxValue;
			for (int i = 0; i < tRaycastResult.Count; i++)
			{
				WorldTile tTile = tRaycastResult[i];
				float tDist = (float)Toolbox.SquaredDist(tTile.x, tTile.y, pTargetFire.x, pTargetFire.y);
				if (tDist >= 4f && tDist < tBestDist)
				{
					tBestDist = tDist;
					tResult = tTile;
				}
			}
			tRaycastResult.Clear();
			return tResult;
		}
	}
}
