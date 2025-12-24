using System;

namespace ai.behaviours
{
	// Token: 0x020008B1 RID: 2225
	public class BehConsumeGrass : BehaviourActionActor
	{
		// Token: 0x060044BD RID: 17597 RVA: 0x001CF135 File Offset: 0x001CD335
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_tile_target = true;
		}

		// Token: 0x060044BE RID: 17598 RVA: 0x001CF144 File Offset: 0x001CD344
		public override BehResult execute(Actor pActor)
		{
			WorldTile tTargetTile = pActor.beh_tile_target;
			if (!tTargetTile.Type.grass)
			{
				return BehResult.Stop;
			}
			pActor.punchTargetAnimation(tTargetTile.posV3, false, false, 40f);
			pActor.consumeTopTile(tTargetTile);
			return BehResult.Continue;
		}
	}
}
