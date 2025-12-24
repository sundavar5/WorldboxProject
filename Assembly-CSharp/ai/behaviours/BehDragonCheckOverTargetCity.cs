using System;

namespace ai.behaviours
{
	// Token: 0x0200094A RID: 2378
	public class BehDragonCheckOverTargetCity : BehDragon
	{
		// Token: 0x06004646 RID: 17990 RVA: 0x001DCAE4 File Offset: 0x001DACE4
		public override BehResult execute(Actor pActor)
		{
			if (WorldLawLibrary.world_law_peaceful_monsters.isEnabled())
			{
				return BehResult.Continue;
			}
			int attacksForCity;
			pActor.data.get("attacksForCity", out attacksForCity, 0);
			if (attacksForCity == 0)
			{
				return BehResult.Continue;
			}
			long cityToAttack;
			pActor.data.get("cityToAttack", out cityToAttack, -1L);
			if ((cityToAttack.hasValue() ? BehaviourActionBase<Actor>.world.cities.get(cityToAttack) : null) == null)
			{
				return BehResult.Continue;
			}
			if (Randy.randomChance(0.8f))
			{
				return BehResult.Continue;
			}
			if (pActor.isFlying() && !Dragon.canLand(pActor, null) && this.dragon.hasTargetsForSlide() && Randy.randomBool())
			{
				pActor.data.set("attacksForCity", --attacksForCity);
				return base.forceTask(pActor, "dragon_slide", true, false);
			}
			if (!pActor.isFlying() && Dragon.canLand(pActor, null))
			{
				pActor.data.set("attacksForCity", --attacksForCity);
				return base.forceTask(pActor, "dragon_land_attack", true, false);
			}
			return BehResult.Continue;
		}
	}
}
