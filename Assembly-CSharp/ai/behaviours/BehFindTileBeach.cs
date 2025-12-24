using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x020008D3 RID: 2259
	public class BehFindTileBeach : BehaviourActionActor
	{
		// Token: 0x0600450B RID: 17675 RVA: 0x001D062C File Offset: 0x001CE82C
		public override BehResult execute(Actor pActor)
		{
			BehaviourActionActor.possible_moves.Clear();
			this.findEdgesInRegion(pActor.current_tile.region);
			if (BehaviourActionActor.possible_moves.Count == 0)
			{
				for (int i = 0; i < pActor.current_tile.region.neighbours.Count; i++)
				{
					MapRegion tReg = pActor.current_tile.region.neighbours[i];
					this.findEdgesInRegion(tReg);
				}
			}
			if (BehaviourActionActor.possible_moves.Count == 0)
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = BehaviourActionActor.possible_moves.GetRandom<WorldTile>();
			BehaviourActionActor.possible_moves.Clear();
			return BehResult.Continue;
		}

		// Token: 0x0600450C RID: 17676 RVA: 0x001D06C8 File Offset: 0x001CE8C8
		private void findEdgesInRegion(MapRegion pRegion)
		{
			List<WorldTile> list = pRegion.getEdgeTiles();
			int tLength = list.Count;
			int tRandomStart = Randy.randomInt(0, tLength);
			for (int i = 0; i < tLength; i++)
			{
				int j = (i + tRandomStart) % tLength;
				WorldTile tRegionTile = list[j];
				if (tRegionTile.Type.ocean)
				{
					BehaviourActionActor.possible_moves.Add(tRegionTile);
					return;
				}
			}
		}
	}
}
