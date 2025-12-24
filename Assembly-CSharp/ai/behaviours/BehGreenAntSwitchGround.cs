using System;

namespace ai.behaviours
{
	// Token: 0x02000916 RID: 2326
	public class BehGreenAntSwitchGround : BehaviourActionActor
	{
		// Token: 0x060045BC RID: 17852 RVA: 0x001D3773 File Offset: 0x001D1973
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_tile_target = true;
		}

		// Token: 0x060045BD RID: 17853 RVA: 0x001D3784 File Offset: 0x001D1984
		public override BehResult execute(Actor pActor)
		{
			int direction_index;
			pActor.data.get("direction", out direction_index, 0);
			string tTileType;
			if (pActor.beh_tile_target.Type.liquid)
			{
				tTileType = "sand";
				direction_index--;
			}
			else if (pActor.beh_tile_target.Type.IsType("sand"))
			{
				tTileType = "soil_low";
				direction_index++;
			}
			else if (pActor.beh_tile_target.Type.IsType("soil_low"))
			{
				tTileType = "soil_high";
				direction_index--;
			}
			else if (pActor.beh_tile_target.Type.IsType("soil_high"))
			{
				tTileType = "soil_low";
				direction_index++;
			}
			else
			{
				tTileType = "sand";
				direction_index--;
			}
			if (direction_index > Toolbox.directions.Length - 1)
			{
				direction_index = 0;
			}
			if (direction_index < 0)
			{
				direction_index = Toolbox.directions.Length - 1;
			}
			Ant.antUseOnTile(pActor.beh_tile_target, tTileType);
			pActor.data.set("direction", direction_index);
			return BehResult.Continue;
		}
	}
}
