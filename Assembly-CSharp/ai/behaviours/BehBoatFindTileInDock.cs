using System;

namespace ai.behaviours
{
	// Token: 0x0200092E RID: 2350
	public class BehBoatFindTileInDock : BehBoat
	{
		// Token: 0x060045F7 RID: 17911 RVA: 0x001D425E File Offset: 0x001D245E
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.check_building_target_non_usable = true;
			this.null_check_building_target = true;
		}

		// Token: 0x060045F8 RID: 17912 RVA: 0x001D4274 File Offset: 0x001D2474
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.beh_building_target.isCiv())
			{
				return BehResult.Stop;
			}
			WorldTile tWaterTileFromDock = pActor.beh_building_target.component_docks.getOceanTileInSameOcean(pActor.current_tile);
			if (tWaterTileFromDock == null)
			{
				return BehResult.Stop;
			}
			pActor.beh_tile_target = tWaterTileFromDock;
			return BehResult.Continue;
		}
	}
}
