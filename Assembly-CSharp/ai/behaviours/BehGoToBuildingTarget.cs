using System;

namespace ai.behaviours
{
	// Token: 0x020008E0 RID: 2272
	public class BehGoToBuildingTarget : BehActorBuildingTarget
	{
		// Token: 0x0600452D RID: 17709 RVA: 0x001D10E7 File Offset: 0x001CF2E7
		public BehGoToBuildingTarget(bool pPathOnWater = false)
		{
			this._path_on_water = pPathOnWater;
		}

		// Token: 0x0600452E RID: 17710 RVA: 0x001D10F6 File Offset: 0x001CF2F6
		public override BehResult execute(Actor pActor)
		{
			this.goToBuilding(pActor);
			return BehResult.Continue;
		}

		// Token: 0x0600452F RID: 17711 RVA: 0x001D1100 File Offset: 0x001CF300
		internal void goToBuilding(Actor pActor)
		{
			WorldTile tMoveTile = pActor.beh_building_target.current_tile;
			pActor.goTo(tMoveTile, this._path_on_water, false, false, 0);
		}

		// Token: 0x04003193 RID: 12691
		private bool _path_on_water;
	}
}
