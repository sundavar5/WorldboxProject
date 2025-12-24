using System;

namespace ai.behaviours
{
	// Token: 0x020008BB RID: 2235
	public class BehFamilyGroupJoin : BehaviourActionActor
	{
		// Token: 0x060044DA RID: 17626 RVA: 0x001CF7B4 File Offset: 0x001CD9B4
		public override BehResult execute(Actor pActor)
		{
			if (pActor.hasFamily())
			{
				return BehResult.Stop;
			}
			Family tFamily = BehaviourActionBase<Actor>.world.families.getNearbyFamily(pActor.asset, pActor.current_tile);
			if (tFamily != null)
			{
				pActor.setFamily(tFamily);
			}
			return BehResult.Continue;
		}
	}
}
