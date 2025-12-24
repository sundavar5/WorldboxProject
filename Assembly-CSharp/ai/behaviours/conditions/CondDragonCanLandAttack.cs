using System;

namespace ai.behaviours.conditions
{
	// Token: 0x0200097E RID: 2430
	public class CondDragonCanLandAttack : BehaviourActorCondition
	{
		// Token: 0x060046F4 RID: 18164 RVA: 0x001E1EC4 File Offset: 0x001E00C4
		public override bool check(Actor pActor)
		{
			int landAttacks;
			pActor.data.get("landAttacks", out landAttacks, 0);
			if (landAttacks >= 2)
			{
				return false;
			}
			int attacksForCity;
			pActor.data.get("attacksForCity", out attacksForCity, 0);
			if (attacksForCity > 0 && pActor.current_tile.zone.city != null)
			{
				long cityToAttack;
				pActor.data.get("cityToAttack", out cityToAttack, -1L);
				if (cityToAttack == pActor.current_tile.zone.city.data.id)
				{
					return true;
				}
			}
			Dragon dragon = pActor.getActorComponent<Dragon>();
			if (dragon.targetsWithinLandAttackRange())
			{
				return true;
			}
			if (pActor.hasTrait("zombie") && World.world.kingdoms_wild.get("golden_brain").hasBuildings())
			{
				foreach (Building tB in World.world.kingdoms_wild.get("golden_brain").buildings)
				{
					if (dragon.landAttackRange(tB.current_tile))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}
	}
}
