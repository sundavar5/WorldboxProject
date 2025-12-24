using System;

namespace ai.behaviours
{
	// Token: 0x020008E8 RID: 2280
	public class BehMakeFarm : BehCityActor
	{
		// Token: 0x06004543 RID: 17731 RVA: 0x001D1898 File Offset: 0x001CFA98
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_tile_target = true;
		}

		// Token: 0x06004544 RID: 17732 RVA: 0x001D18A8 File Offset: 0x001CFAA8
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.beh_tile_target.Type.can_be_farm)
			{
				return BehResult.Stop;
			}
			if (pActor.beh_tile_target.hasBuilding() && !pActor.beh_tile_target.building.canRemoveForFarms())
			{
				return BehResult.Stop;
			}
			MapAction.terraformTop(pActor.beh_tile_target, TopTileLibrary.field, false);
			MusicBox.playSound("event:/SFX/CIVILIZATIONS/MakeFarmField", pActor.beh_tile_target, true, true);
			pActor.addLoot(SimGlobals.m.coins_for_field);
			return BehResult.Continue;
		}
	}
}
