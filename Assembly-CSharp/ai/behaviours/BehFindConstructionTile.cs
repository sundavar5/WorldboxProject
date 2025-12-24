using System;

namespace ai.behaviours
{
	// Token: 0x020008C1 RID: 2241
	public class BehFindConstructionTile : BehActorBuildingTarget
	{
		// Token: 0x060044E8 RID: 17640 RVA: 0x001CFB90 File Offset: 0x001CDD90
		public override BehResult execute(Actor pActor)
		{
			pActor.beh_tile_target = pActor.beh_building_target.getConstructionTile();
			return BehResult.Continue;
		}
	}
}
