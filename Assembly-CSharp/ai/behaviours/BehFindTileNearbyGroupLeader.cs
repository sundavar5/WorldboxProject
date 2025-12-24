using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x020008D7 RID: 2263
	public class BehFindTileNearbyGroupLeader : BehaviourActionActor
	{
		// Token: 0x06004515 RID: 17685 RVA: 0x001D0A50 File Offset: 0x001CEC50
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.hasArmy())
			{
				return BehResult.Stop;
			}
			if (!pActor.army.hasCaptain())
			{
				return BehResult.Stop;
			}
			Actor tCaptain = pActor.army.getCaptain();
			List<WorldTile> tCaptainPath = tCaptain.current_path;
			WorldTile tTileTarget;
			if (tCaptainPath != null && tCaptainPath.Count > 0)
			{
				tTileTarget = tCaptainPath[tCaptainPath.Count - 1].region.tiles.GetRandom<WorldTile>();
			}
			else
			{
				MapRegion tRegion = tCaptain.current_tile.region;
				if (tRegion.tiles.Count < 20 && tRegion.neighbours.Count > 0)
				{
					tRegion = tRegion.neighbours.GetRandom<MapRegion>();
				}
				tTileTarget = tRegion.tiles.GetRandom<WorldTile>();
			}
			pActor.beh_tile_target = tTileTarget;
			return BehResult.Continue;
		}
	}
}
