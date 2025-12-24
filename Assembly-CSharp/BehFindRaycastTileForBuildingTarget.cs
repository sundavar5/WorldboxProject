using System;
using System.Collections.Generic;
using ai.behaviours;

// Token: 0x0200039C RID: 924
public class BehFindRaycastTileForBuildingTarget : BehaviourActionActor
{
	// Token: 0x060021C4 RID: 8644 RVA: 0x0011D858 File Offset: 0x0011BA58
	public override BehResult execute(Actor pActor)
	{
		Building tBuildingTarget = pActor.beh_building_target;
		if (tBuildingTarget == null)
		{
			return BehResult.Stop;
		}
		WorldTile tBuildingTile = tBuildingTarget.current_tile;
		WorldTile tActorTile = pActor.current_tile;
		if (!tBuildingTile.isSameIsland(tActorTile))
		{
			return BehResult.Stop;
		}
		List<WorldTile> tRaycastResult = PathfinderTools.raycast(tActorTile, tBuildingTile, 0.99f);
		WorldTile tResultTile = null;
		float tThrowDistance = pActor.getResourceThrowDistance();
		for (int i = 0; i < tRaycastResult.Count; i++)
		{
			WorldTile tRaycastTile = tRaycastResult[i];
			if (tRaycastTile.isSameIsland(tActorTile) && Toolbox.DistTile(tRaycastTile, tBuildingTile) < tThrowDistance)
			{
				tResultTile = tRaycastTile;
				break;
			}
		}
		if (tResultTile == null)
		{
			return BehResult.Stop;
		}
		pActor.beh_tile_target = tResultTile;
		return BehResult.Continue;
	}
}
