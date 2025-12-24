using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x020008D5 RID: 2261
	public class BehFindTileForEating : BehaviourActionActor
	{
		// Token: 0x06004510 RID: 17680 RVA: 0x001D0838 File Offset: 0x001CEA38
		public override BehResult execute(Actor pActor)
		{
			WorldTile tTargetTile = this.findTileAround(pActor.current_tile.neighboursAll);
			if (tTargetTile == null)
			{
				tTargetTile = this.findTileAround(pActor.current_tile.region.tiles);
			}
			if (tTargetTile == null)
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = tTargetTile;
			return BehResult.Continue;
		}

		// Token: 0x06004511 RID: 17681 RVA: 0x001D0880 File Offset: 0x001CEA80
		private WorldTile findTileAround(IEnumerable<WorldTile> pList)
		{
			WorldTile tTargetTile = null;
			foreach (WorldTile tTile in pList)
			{
				if (tTile.Type.canBeEatenByGeophag())
				{
					if (tTargetTile == null)
					{
						tTargetTile = tTile;
					}
					else if (Randy.randomBool())
					{
						tTargetTile = tTile;
						break;
					}
				}
			}
			return tTargetTile;
		}
	}
}
