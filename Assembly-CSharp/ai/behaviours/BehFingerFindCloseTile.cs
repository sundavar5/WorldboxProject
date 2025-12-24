using System;

namespace ai.behaviours
{
	// Token: 0x0200095A RID: 2394
	public class BehFingerFindCloseTile : BehFinger
	{
		// Token: 0x0600466A RID: 18026 RVA: 0x001DDA04 File Offset: 0x001DBC04
		public override BehResult execute(Actor pActor)
		{
			pActor.findCurrentTile(false);
			if (this.finger.target_tiles.Count == 0)
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = this.finger.target_tiles.GetRandom<WorldTile>();
			if (this.finger.target_tiles.Contains(pActor.current_tile))
			{
				while (pActor.beh_tile_target.region != pActor.current_tile.region)
				{
					pActor.beh_tile_target = this.finger.target_tiles.GetRandom<WorldTile>();
				}
			}
			return BehResult.Continue;
		}
	}
}
