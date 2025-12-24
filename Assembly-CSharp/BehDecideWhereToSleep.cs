using System;
using ai.behaviours;

// Token: 0x020003D6 RID: 982
public class BehDecideWhereToSleep : BehaviourActionActor
{
	// Token: 0x06002283 RID: 8835 RVA: 0x00121920 File Offset: 0x0011FB20
	public override BehResult execute(Actor pObject)
	{
		if (pObject.hasHouseCityInBordersAndSameIsland())
		{
			return base.forceTask(pObject, "sleep_inside", false, true);
		}
		return base.forceTask(pObject, "sleep_outside", false, true);
	}
}
