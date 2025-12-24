using System;
using life.taxi;

namespace ai.behaviours
{
	// Token: 0x02000902 RID: 2306
	public class BehTaxiCheck : BehCitizenActionCity
	{
		// Token: 0x06004580 RID: 17792 RVA: 0x001D26E0 File Offset: 0x001D08E0
		public override BehResult execute(Actor pActor)
		{
			WorldTile tCityTile = pActor.city.getTile(false);
			if (tCityTile == null)
			{
				return BehResult.Stop;
			}
			bool needToReturnHome = false;
			if (pActor.isCitizenJob("attacker"))
			{
				if (!pActor.current_tile.isSameIsland(tCityTile) && (!pActor.city.hasAttackZoneOrder() || !pActor.city.target_attack_zone.centerTile.isSameIsland(pActor.current_tile)))
				{
					needToReturnHome = true;
				}
			}
			else if (!pActor.current_tile.isSameIsland(tCityTile))
			{
				needToReturnHome = true;
			}
			if (!needToReturnHome)
			{
				return BehResult.Stop;
			}
			TaxiManager.newRequest(pActor, tCityTile);
			return BehResult.Continue;
		}
	}
}
