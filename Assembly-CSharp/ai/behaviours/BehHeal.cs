using System;

namespace ai.behaviours
{
	// Token: 0x020008E3 RID: 2275
	public class BehHeal : BehaviourActionActor
	{
		// Token: 0x06004539 RID: 17721 RVA: 0x001D169E File Offset: 0x001CF89E
		public override BehResult execute(Actor pActor)
		{
			if (pActor.hasMaxHealth())
			{
				return BehResult.Stop;
			}
			AttackAction action = AssetManager.spells.get("cast_blood_rain").action;
			if (action != null)
			{
				action(pActor, pActor, pActor.current_tile);
			}
			pActor.doCastAnimation();
			return BehResult.Continue;
		}
	}
}
