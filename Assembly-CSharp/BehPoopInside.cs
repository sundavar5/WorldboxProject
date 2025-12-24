using System;
using ai.behaviours;

// Token: 0x020003CC RID: 972
public class BehPoopInside : BehaviourActionActor
{
	// Token: 0x06002261 RID: 8801 RVA: 0x00120DE0 File Offset: 0x0011EFE0
	public override BehResult execute(Actor pActor)
	{
		pActor.donePooping();
		string tBuildingID;
		if (pActor.hasSubspecies())
		{
			tBuildingID = pActor.subspecies.getRandomBioProduct();
		}
		else
		{
			tBuildingID = "poop";
		}
		if (tBuildingID != "poop")
		{
			BuildingHelper.tryToBuildNear(pActor.current_tile, tBuildingID);
		}
		return BehResult.Continue;
	}
}
