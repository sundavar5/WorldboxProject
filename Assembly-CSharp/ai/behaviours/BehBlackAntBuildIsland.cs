using System;

namespace ai.behaviours
{
	// Token: 0x02000913 RID: 2323
	public class BehBlackAntBuildIsland : BehaviourActionActor
	{
		// Token: 0x060045B1 RID: 17841 RVA: 0x001D3497 File Offset: 0x001D1697
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_tile_target = true;
		}

		// Token: 0x060045B2 RID: 17842 RVA: 0x001D34A8 File Offset: 0x001D16A8
		public override BehResult execute(Actor pActor)
		{
			int antSteps;
			pActor.data.get("ant_steps", out antSteps, 0);
			int direction_index;
			pActor.data.get("direction", out direction_index, 0);
			if (pActor.beh_tile_target.Type.liquid)
			{
				antSteps = 20;
			}
			if (antSteps > 0)
			{
				string tTileType;
				if (!pActor.beh_tile_target.Type.IsType("mountains"))
				{
					tTileType = "mountains";
					direction_index++;
					if (direction_index > Toolbox.directions.Length - 1)
					{
						direction_index = 0;
					}
				}
				else
				{
					tTileType = "hills";
					direction_index--;
					if (direction_index < 0)
					{
						direction_index = Toolbox.directions.Length - 1;
					}
				}
				Ant.antUseOnTile(pActor.beh_tile_target, tTileType);
				antSteps--;
			}
			if (antSteps == 0)
			{
				pActor.data.set("ant_steps", 40);
				pActor.data.set("direction", BehBlackAntBuildIsland.getRandomDirection());
				pActor.setTask("ant_black_sand", true, false, false);
				return BehResult.Stop;
			}
			pActor.data.set("ant_steps", antSteps);
			pActor.data.set("direction", direction_index);
			return BehResult.Continue;
		}

		// Token: 0x060045B3 RID: 17843 RVA: 0x001D35B0 File Offset: 0x001D17B0
		private static int getRandomDirection()
		{
			ActorDirection currentDirection = Randy.getRandom<ActorDirection>(Toolbox.directions);
			return Toolbox.directions.IndexOf(currentDirection);
		}
	}
}
