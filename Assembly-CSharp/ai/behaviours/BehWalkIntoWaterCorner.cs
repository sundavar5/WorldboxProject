using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x0200090D RID: 2317
	public class BehWalkIntoWaterCorner : BehaviourActionActor
	{
		// Token: 0x06004598 RID: 17816 RVA: 0x001D2CA0 File Offset: 0x001D0EA0
		public override BehResult execute(Actor pActor)
		{
			TileIsland tCurIsland = pActor.current_tile.region.island;
			if (tCurIsland.isGoodIslandForActor(pActor))
			{
				return BehResult.Stop;
			}
			WorldTile tBest = null;
			int tBestDist = int.MaxValue;
			foreach (MapRegion mapRegion in tCurIsland.insideRegionEdges)
			{
				List<WorldTile> tCornerList = mapRegion.getEdgeTiles();
				for (int i = 0; i < tCornerList.Count; i++)
				{
					WorldTile tTile = tCornerList[i];
					if (tTile.Type.ocean)
					{
						int tDist = Toolbox.SquaredDistTile(pActor.current_tile, tTile);
						if (tDist < tBestDist)
						{
							tBest = tTile;
							tBestDist = tDist;
						}
					}
				}
			}
			if (tBest == null)
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = tBest;
			return BehResult.Continue;
		}
	}
}
