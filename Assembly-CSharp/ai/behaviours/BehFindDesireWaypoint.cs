using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x020008C2 RID: 2242
	public class BehFindDesireWaypoint : BehaviourActionActor
	{
		// Token: 0x060044EA RID: 17642 RVA: 0x001CFBAC File Offset: 0x001CDDAC
		public override BehResult execute(Actor pActor)
		{
			string tBuildingAttractorID = pActor.kingdom.asset.building_attractor_id;
			if (string.IsNullOrEmpty(tBuildingAttractorID))
			{
				return BehResult.Stop;
			}
			BuildingAsset tBuildingAsset = AssetManager.buildings.get(tBuildingAttractorID);
			if (tBuildingAsset == null)
			{
				return BehResult.Stop;
			}
			HashSet<Building> tBuildingList = tBuildingAsset.buildings;
			if (tBuildingList.Count == 0)
			{
				return BehResult.Stop;
			}
			Building tTarget = Finder.getClosestBuildingFrom(pActor, tBuildingList);
			if (tTarget == null)
			{
				return BehResult.Stop;
			}
			if (Toolbox.DistTile(pActor.current_tile, tTarget.current_tile) < 10f)
			{
				return BehResult.Stop;
			}
			pActor.beh_building_target = tTarget;
			return BehResult.Continue;
		}
	}
}
