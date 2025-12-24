using System;

namespace ai.behaviours
{
	// Token: 0x02000931 RID: 2353
	public class BehBoatMakeTrade : BehaviourActionActor
	{
		// Token: 0x060045FF RID: 17919 RVA: 0x001D438F File Offset: 0x001D258F
		public override BehResult execute(Actor pActor)
		{
			pActor.addToInventory("gold", 5);
			return BehResult.Continue;
		}
	}
}
