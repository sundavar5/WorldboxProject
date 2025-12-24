using System;

namespace ai.behaviours
{
	// Token: 0x0200091D RID: 2333
	public class BehBeeCheckHome : BehaviourActionActor
	{
		// Token: 0x060045D1 RID: 17873 RVA: 0x001D3CE2 File Offset: 0x001D1EE2
		public override BehResult execute(Actor pActor)
		{
			if (pActor.asset.id != "bee")
			{
				return BehResult.Continue;
			}
			if (pActor.getHomeBuilding() != null)
			{
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
