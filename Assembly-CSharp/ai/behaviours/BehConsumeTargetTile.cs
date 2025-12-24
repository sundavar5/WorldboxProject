using System;

namespace ai.behaviours
{
	// Token: 0x020008B3 RID: 2227
	public class BehConsumeTargetTile : BehaviourActionActor
	{
		// Token: 0x060044C2 RID: 17602 RVA: 0x001CF24C File Offset: 0x001CD44C
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_tile_target = true;
		}

		// Token: 0x060044C3 RID: 17603 RVA: 0x001CF25C File Offset: 0x001CD45C
		public override BehResult execute(Actor pActor)
		{
			WorldTile tTargetTile = pActor.beh_tile_target;
			if (!tTargetTile.Type.canBeEatenByGeophag())
			{
				return BehResult.Stop;
			}
			pActor.punchTargetAnimation(tTargetTile.posV3, false, false, 40f);
			pActor.consumeTopTile(tTargetTile);
			MapAction.terraformMain(tTargetTile, TileLibrary.pit_deep_ocean, TerraformLibrary.destroy_no_flash, false);
			return BehResult.Continue;
		}
	}
}
