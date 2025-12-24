using System;

namespace ai.behaviours
{
	// Token: 0x020008FC RID: 2300
	public class BehShortRandomMove : BehaviourActionActor
	{
		// Token: 0x06004570 RID: 17776 RVA: 0x001D22D4 File Offset: 0x001D04D4
		public override BehResult execute(Actor pActor)
		{
			foreach (WorldTile tTile in pActor.current_tile.neighboursAll.LoopRandom<WorldTile>())
			{
				if (tTile.Type.layer_type == pActor.current_tile.Type.layer_type)
				{
					pActor.beh_tile_target = tTile;
					return BehResult.Continue;
				}
			}
			return BehResult.Stop;
		}
	}
}
