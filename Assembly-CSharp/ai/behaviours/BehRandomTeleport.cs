using System;

namespace ai.behaviours
{
	// Token: 0x020008F0 RID: 2288
	public class BehRandomTeleport : BehaviourActionActor
	{
		// Token: 0x06004558 RID: 17752 RVA: 0x001D1FA0 File Offset: 0x001D01A0
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.hasMaxHealth())
			{
				return BehResult.Stop;
			}
			if (!Randy.randomChance(0.3f))
			{
				return BehResult.Stop;
			}
			SpellAsset tSpellAsset = AssetManager.spells.get("teleport");
			bool tTeleported = false;
			if (tSpellAsset.action != null)
			{
				tTeleported = tSpellAsset.action.RunAnyTrue(pActor, pActor, pActor.current_tile);
			}
			if (tTeleported)
			{
				pActor.doCastAnimation();
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
