using System;

namespace ai.behaviours
{
	// Token: 0x0200090A RID: 2314
	public class BehUnloadResources : BehCityActor
	{
		// Token: 0x06004592 RID: 17810 RVA: 0x001D2BF8 File Offset: 0x001D0DF8
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.isCarryingResources())
			{
				return BehResult.Continue;
			}
			pActor.giveInventoryResourcesToCity();
			return BehResult.Continue;
		}
	}
}
