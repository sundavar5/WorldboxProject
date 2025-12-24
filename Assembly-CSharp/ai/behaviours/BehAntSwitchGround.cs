using System;

namespace ai.behaviours
{
	// Token: 0x02000912 RID: 2322
	public class BehAntSwitchGround : BehaviourActionActor
	{
		// Token: 0x060045AE RID: 17838 RVA: 0x001D339C File Offset: 0x001D159C
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_tile_target = true;
		}

		// Token: 0x060045AF RID: 17839 RVA: 0x001D33AC File Offset: 0x001D15AC
		public override BehResult execute(Actor pActor)
		{
			int antSteps;
			pActor.data.get("ant_steps", out antSteps, 0);
			int direction_index;
			pActor.data.get("direction", out direction_index, 0);
			string tTileType;
			pActor.data.get("tile_type1", out tTileType, null);
			string tTileType2;
			pActor.data.get("tile_type2", out tTileType2, null);
			if (pActor.beh_tile_target.Type.IsType(tTileType2))
			{
				Ant.antUseOnTile(pActor.beh_tile_target, tTileType);
				if (antSteps++ > 3)
				{
					direction_index++;
					if (direction_index > Toolbox.directions.Length - 1)
					{
						direction_index = 0;
					}
					antSteps = 0;
				}
			}
			else
			{
				Ant.antUseOnTile(pActor.beh_tile_target, tTileType2);
				if (antSteps++ > 3)
				{
					direction_index--;
					if (direction_index < 0)
					{
						direction_index = Toolbox.directions.Length - 1;
					}
					antSteps = 0;
				}
			}
			pActor.data.set("ant_steps", antSteps);
			pActor.data.set("direction", direction_index);
			return BehResult.Continue;
		}
	}
}
