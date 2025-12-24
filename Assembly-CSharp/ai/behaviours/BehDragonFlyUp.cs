using System;

namespace ai.behaviours
{
	// Token: 0x0200094D RID: 2381
	public class BehDragonFlyUp : BehaviourActionActor
	{
		// Token: 0x0600464C RID: 17996 RVA: 0x001DCD40 File Offset: 0x001DAF40
		public override BehResult execute(Actor pActor)
		{
			pActor.data.set("landAttacks", 0);
			return BehResult.Continue;
		}
	}
}
