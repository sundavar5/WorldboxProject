using System;

namespace ai.behaviours
{
	// Token: 0x020008CC RID: 2252
	public class BehFindRandomNeighbourTile : BehaviourActionActor
	{
		// Token: 0x060044FE RID: 17662 RVA: 0x001D0230 File Offset: 0x001CE430
		public override BehResult execute(Actor pActor)
		{
			WorldTile tTile = pActor.current_tile.getWalkableTileAround(pActor.current_tile);
			pActor.beh_tile_target = tTile;
			if (tTile == null)
			{
				return BehResult.Stop;
			}
			return BehResult.Continue;
		}
	}
}
