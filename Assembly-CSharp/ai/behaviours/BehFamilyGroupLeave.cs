using System;

namespace ai.behaviours
{
	// Token: 0x020008BC RID: 2236
	public class BehFamilyGroupLeave : BehaviourActionActor
	{
		// Token: 0x060044DC RID: 17628 RVA: 0x001CF7FA File Offset: 0x001CD9FA
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.hasFamily())
			{
				return BehResult.Stop;
			}
			if (!pActor.family.isFull())
			{
				return BehResult.Stop;
			}
			pActor.setFamily(null);
			return BehResult.Continue;
		}
	}
}
