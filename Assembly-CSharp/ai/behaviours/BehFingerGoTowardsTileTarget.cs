using System;

namespace ai.behaviours
{
	// Token: 0x0200095E RID: 2398
	public class BehFingerGoTowardsTileTarget : BehFinger
	{
		// Token: 0x06004675 RID: 18037 RVA: 0x001DE40C File Offset: 0x001DC60C
		public BehFingerGoTowardsTileTarget(int pRadiusTileRange = 25)
		{
			this._tile_range = pRadiusTileRange;
		}

		// Token: 0x06004676 RID: 18038 RVA: 0x001DE41B File Offset: 0x001DC61B
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_tile_target = true;
		}

		// Token: 0x06004677 RID: 18039 RVA: 0x001DE42C File Offset: 0x001DC62C
		public override BehResult execute(Actor pActor)
		{
			WorldTile tWayPoint = Toolbox.getRandomTileWithinDistance(pActor.current_tile, this._tile_range);
			WorldTile tWayPoint2 = Toolbox.getRandomTileWithinDistance(tWayPoint, this._tile_range);
			WorldTile tWayPoint3 = Toolbox.getRandomTileWithinDistance(pActor.beh_tile_target, this._tile_range);
			WorldTile tWayPoint4 = Toolbox.getRandomTileWithinDistance(tWayPoint3, this._tile_range);
			if (ActorMove.goToCurved(pActor, new WorldTile[]
			{
				pActor.current_tile,
				tWayPoint,
				tWayPoint2,
				tWayPoint4,
				tWayPoint3
			}) == ExecuteEvent.False)
			{
				return BehResult.Stop;
			}
			return BehResult.Continue;
		}

		// Token: 0x040031F1 RID: 12785
		private int _tile_range;
	}
}
