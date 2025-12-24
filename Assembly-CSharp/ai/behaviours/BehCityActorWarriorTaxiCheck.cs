using System;
using life.taxi;

namespace ai.behaviours
{
	// Token: 0x020008AE RID: 2222
	public class BehCityActorWarriorTaxiCheck : BehCityActor
	{
		// Token: 0x060044B3 RID: 17587 RVA: 0x001CED08 File Offset: 0x001CCF08
		public override BehResult execute(Actor pActor)
		{
			if (pActor.current_tile.hasCity() && pActor.current_tile.zone_city.kingdom.isEnemy(pActor.kingdom))
			{
				return BehResult.Stop;
			}
			if (pActor.city.hasAttackZoneOrder() && !pActor.city.target_attack_zone.centerTile.isSameIsland(pActor.current_tile))
			{
				TaxiManager.newRequest(pActor, pActor.city.target_attack_zone.centerTile);
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
