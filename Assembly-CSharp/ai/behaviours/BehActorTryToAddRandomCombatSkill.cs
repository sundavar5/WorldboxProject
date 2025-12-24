using System;

namespace ai.behaviours
{
	// Token: 0x02000886 RID: 2182
	public class BehActorTryToAddRandomCombatSkill : BehaviourActionActor
	{
		// Token: 0x0600444F RID: 17487 RVA: 0x001CD880 File Offset: 0x001CBA80
		public override BehResult execute(Actor pActor)
		{
			if (!Randy.randomChance(0.15f))
			{
				return BehResult.Stop;
			}
			ActorTrait tTrait = AssetManager.traits.pot_traits_combat.GetRandom<ActorTrait>();
			pActor.addTrait(tTrait, false);
			return BehResult.Continue;
		}
	}
}
