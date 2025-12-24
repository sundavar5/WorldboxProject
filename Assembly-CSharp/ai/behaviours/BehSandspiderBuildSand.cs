using System;

namespace ai.behaviours
{
	// Token: 0x02000917 RID: 2327
	public class BehSandspiderBuildSand : BehGoToTileTarget
	{
		// Token: 0x060045BF RID: 17855 RVA: 0x001D3878 File Offset: 0x001D1A78
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_tile_target = true;
		}

		// Token: 0x060045C0 RID: 17856 RVA: 0x001D3888 File Offset: 0x001D1A88
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.beh_tile_target.Type.IsType("sand"))
			{
				int antSteps;
				pActor.data.get("ant_steps", out antSteps, 0);
				Ant.antUseOnTile(pActor.beh_tile_target, "sand");
				pActor.data.set("ant_steps", ++antSteps);
				pActor.data.removeBool("changed_direction");
			}
			else if (Randy.randomChance(0.1f))
			{
				int antSteps2;
				pActor.data.get("ant_steps", out antSteps2, 0);
				pActor.data.set("ant_steps", ++antSteps2);
				pActor.data.removeBool("changed_direction");
			}
			return BehResult.Continue;
		}
	}
}
