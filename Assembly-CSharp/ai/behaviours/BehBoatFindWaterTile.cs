using System;

namespace ai.behaviours
{
	// Token: 0x0200092F RID: 2351
	public class BehBoatFindWaterTile : BehaviourActionActor
	{
		// Token: 0x060045FA RID: 17914 RVA: 0x001D42BC File Offset: 0x001D24BC
		public override BehResult execute(Actor pActor)
		{
			WorldTile tTile = ActorTool.getRandomTileForBoat(pActor);
			pActor.beh_tile_target = tTile;
			return BehResult.Continue;
		}
	}
}
