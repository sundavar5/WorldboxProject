using System;

namespace ai.behaviours
{
	// Token: 0x020008EC RID: 2284
	public class BehPlantCrops : BehCityActor
	{
		// Token: 0x0600454E RID: 17742 RVA: 0x001D1C14 File Offset: 0x001CFE14
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_tile_target = true;
		}

		// Token: 0x0600454F RID: 17743 RVA: 0x001D1C24 File Offset: 0x001CFE24
		public override BehResult execute(Actor pActor)
		{
			if (pActor.beh_tile_target.Type == TopTileLibrary.field && !pActor.beh_tile_target.hasBuilding())
			{
				BehaviourActionBase<Actor>.world.buildings.addBuilding("wheat", pActor.beh_tile_target, false, false, BuildPlacingType.New);
				pActor.addLoot(SimGlobals.m.coins_for_planting);
				MusicBox.playSound("event:/SFX/CIVILIZATIONS/PlantCrops", pActor.beh_tile_target, true, false);
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
