using System;

namespace ai.behaviours
{
	// Token: 0x020008E2 RID: 2274
	public class BehGoToTileTarget : BehaviourActionActor
	{
		// Token: 0x06004536 RID: 17718 RVA: 0x001D1659 File Offset: 0x001CF859
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_tile_target = true;
			this.walk_on_water = false;
		}

		// Token: 0x06004537 RID: 17719 RVA: 0x001D166F File Offset: 0x001CF86F
		public override BehResult execute(Actor pActor)
		{
			if (pActor.goTo(pActor.beh_tile_target, this.walk_on_water, this.walk_on_blocks, false, this.limit_pathfinding_regions) == ExecuteEvent.False)
			{
				return BehResult.Stop;
			}
			return BehResult.Continue;
		}

		// Token: 0x04003199 RID: 12697
		public bool walk_on_water;

		// Token: 0x0400319A RID: 12698
		public bool walk_on_blocks;

		// Token: 0x0400319B RID: 12699
		public int limit_pathfinding_regions;
	}
}
