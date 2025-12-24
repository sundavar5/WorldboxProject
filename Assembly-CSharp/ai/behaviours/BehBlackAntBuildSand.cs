using System;

namespace ai.behaviours
{
	// Token: 0x02000914 RID: 2324
	public class BehBlackAntBuildSand : BehaviourActionActor
	{
		// Token: 0x060045B5 RID: 17845 RVA: 0x001D35DB File Offset: 0x001D17DB
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_tile_target = true;
		}

		// Token: 0x060045B6 RID: 17846 RVA: 0x001D35EC File Offset: 0x001D17EC
		public override BehResult execute(Actor pActor)
		{
			int antSteps;
			pActor.data.get("ant_steps", out antSteps, 0);
			int direction_index;
			pActor.data.get("direction", out direction_index, 0);
			if (antSteps > 0)
			{
				antSteps--;
				if (!pActor.beh_tile_target.Type.IsType("mountains") && !pActor.beh_tile_target.Type.IsType("hills"))
				{
					Ant.antUseOnTile(pActor.beh_tile_target, "sand");
				}
				direction_index = BehBlackAntBuildSand.getRandomDirection();
			}
			pActor.data.set("ant_steps", antSteps);
			pActor.data.set("direction", direction_index);
			if (antSteps == 0)
			{
				pActor.setTask("ant_black_island", true, false, false);
				return BehResult.Stop;
			}
			return BehResult.Continue;
		}

		// Token: 0x060045B7 RID: 17847 RVA: 0x001D36A4 File Offset: 0x001D18A4
		private static int getRandomDirection()
		{
			ActorDirection currentDirection = Randy.getRandom<ActorDirection>(Toolbox.directions);
			return Toolbox.directions.IndexOf(currentDirection);
		}
	}
}
