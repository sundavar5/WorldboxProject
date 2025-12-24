using System;

namespace ai.behaviours
{
	// Token: 0x02000962 RID: 2402
	public class BehUFOCheckAttackCity : BehaviourActionActor
	{
		// Token: 0x0600467E RID: 18046 RVA: 0x001DE6AC File Offset: 0x001DC8AC
		public override BehResult execute(Actor pActor)
		{
			long cityToAttack;
			pActor.data.get("cityToAttack", out cityToAttack, -1L);
			if (cityToAttack.hasValue() && pActor.current_tile.hasBuilding() && !WorldLawLibrary.world_law_peaceful_monsters.isEnabled() && pActor.current_tile.building.isUsable())
			{
				return base.forceTask(pActor, "ufo_attack", true, false);
			}
			int attacksForCity;
			pActor.data.get("attacksForCity", out attacksForCity, 0);
			if (attacksForCity > 0)
			{
				return BehResult.RestartTask;
			}
			return BehResult.Continue;
		}
	}
}
