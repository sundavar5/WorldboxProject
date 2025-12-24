using System;

namespace ai.behaviours
{
	// Token: 0x02000915 RID: 2325
	public class BehBlueAntSwitchGround : BehaviourActionActor
	{
		// Token: 0x060045B9 RID: 17849 RVA: 0x001D36CF File Offset: 0x001D18CF
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_tile_target = true;
		}

		// Token: 0x060045BA RID: 17850 RVA: 0x001D36E0 File Offset: 0x001D18E0
		public override BehResult execute(Actor pActor)
		{
			int direction_index;
			pActor.data.get("direction", out direction_index, 0);
			if (pActor.beh_tile_target.Type.liquid)
			{
				direction_index++;
				if (direction_index > Toolbox.directions.Length - 1)
				{
					direction_index = 0;
				}
				Ant.antUseOnTile(pActor.beh_tile_target, "sand");
			}
			else
			{
				direction_index--;
				if (direction_index < 0)
				{
					direction_index = Toolbox.directions.Length - 1;
				}
				Ant.antUseOnTile(pActor.beh_tile_target, "shallow_waters");
			}
			pActor.data.set("direction", direction_index);
			return BehResult.Continue;
		}

		// Token: 0x040031BE RID: 12734
		private const string tileType1 = "sand";

		// Token: 0x040031BF RID: 12735
		private const string tileType2 = "shallow_waters";
	}
}
