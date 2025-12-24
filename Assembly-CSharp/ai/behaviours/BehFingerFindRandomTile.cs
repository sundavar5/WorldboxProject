using System;

namespace ai.behaviours
{
	// Token: 0x0200095B RID: 2395
	public class BehFingerFindRandomTile : BehFinger
	{
		// Token: 0x0600466C RID: 18028 RVA: 0x001DDA93 File Offset: 0x001DBC93
		public BehFingerFindRandomTile(int pRange = 75)
		{
			this._range = pRange;
		}

		// Token: 0x0600466D RID: 18029 RVA: 0x001DDAA2 File Offset: 0x001DBCA2
		public override BehResult execute(Actor pActor)
		{
			pActor.findCurrentTile(false);
			pActor.beh_tile_target = Toolbox.getRandomTileWithinDistance(pActor.current_tile, this._range);
			return BehResult.Continue;
		}

		// Token: 0x040031E9 RID: 12777
		private int _range;
	}
}
