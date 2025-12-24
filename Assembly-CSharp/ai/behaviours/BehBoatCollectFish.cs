using System;

namespace ai.behaviours
{
	// Token: 0x02000928 RID: 2344
	public class BehBoatCollectFish : BehaviourActionActor
	{
		// Token: 0x060045EB RID: 17899 RVA: 0x001D403F File Offset: 0x001D223F
		public override BehResult execute(Actor pActor)
		{
			pActor.addToInventory("fish", 1);
			return BehResult.Continue;
		}
	}
}
