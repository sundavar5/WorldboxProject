using System;

namespace ai.behaviours
{
	// Token: 0x02000889 RID: 2185
	public class BehAnimalCheckHungry : BehaviourActionActor
	{
		// Token: 0x06004457 RID: 17495 RVA: 0x001CDAAA File Offset: 0x001CBCAA
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.isHungry())
			{
				return BehResult.Stop;
			}
			return BehResult.Continue;
		}
	}
}
