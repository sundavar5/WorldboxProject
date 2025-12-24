using System;
using System.Collections.Generic;
using UnityEngine;

namespace ai.behaviours
{
	// Token: 0x020008D4 RID: 2260
	public class BehFindTileForCity : BehaviourActionActor
	{
		// Token: 0x0600450E RID: 17678 RVA: 0x001D072C File Offset: 0x001CE92C
		public override BehResult execute(Actor pActor)
		{
			if (!BehaviourActionBase<Actor>.world.city_zone_helper.city_place_finder.hasPossibleZones())
			{
				return BehResult.Stop;
			}
			TileZone tBestZone = null;
			float tBestDist = float.MaxValue;
			List<TileZone> tZones = BehaviourActionBase<Actor>.world.city_zone_helper.city_place_finder.zones;
			Vector3 tCurrentPosition = pActor.current_tile.posV3;
			TileZone tRandomZone = null;
			for (int i = 0; i < tZones.Count; i++)
			{
				TileZone tZone = tZones[i];
				float tDist = Toolbox.SquaredDistVec3(tCurrentPosition, tZone.centerTile.posV3);
				if (tBestDist > tDist && tZone.tiles[0].isSameIsland(pActor.current_tile) && tZone.isGoodForNewCity())
				{
					tBestDist = tDist;
					tBestZone = tZone;
				}
			}
			if (tBestZone != null)
			{
				pActor.beh_tile_target = tBestZone.tiles.GetRandom<WorldTile>();
				return BehResult.Continue;
			}
			if (tRandomZone != null)
			{
				pActor.beh_tile_target = tRandomZone.tiles.GetRandom<WorldTile>();
				return BehResult.Continue;
			}
			TileIsland tIsland = BehaviourActionBase<Actor>.world.islands_calculator.getRandomIslandGround(true);
			if (tIsland == null)
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = tIsland.getRandomTile();
			return BehResult.Continue;
		}
	}
}
