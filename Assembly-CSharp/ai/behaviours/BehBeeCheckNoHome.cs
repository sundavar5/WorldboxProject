using System;

namespace ai.behaviours
{
	// Token: 0x0200091E RID: 2334
	public class BehBeeCheckNoHome : BehaviourActionActor
	{
		// Token: 0x060045D3 RID: 17875 RVA: 0x001D3D10 File Offset: 0x001D1F10
		public override BehResult execute(Actor pActor)
		{
			if (pActor.getHomeBuilding() == null)
			{
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
