using System;

namespace ai.behaviours
{
	// Token: 0x02000897 RID: 2199
	public class BehCheckIfInLiquid : BehaviourActionActor
	{
		// Token: 0x06004478 RID: 17528 RVA: 0x001CE110 File Offset: 0x001CC310
		public override BehResult execute(Actor pActor)
		{
			if (pActor.isInLiquid())
			{
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
