using System;

namespace ai.behaviours
{
	// Token: 0x020008DC RID: 2268
	public class BehGetTargetBuildingMainTile : BehActorBuildingTarget
	{
		// Token: 0x06004525 RID: 17701 RVA: 0x001D0EE0 File Offset: 0x001CF0E0
		public override BehResult execute(Actor pActor)
		{
			pActor.beh_tile_target = pActor.beh_building_target.current_tile;
			return BehResult.Continue;
		}
	}
}
