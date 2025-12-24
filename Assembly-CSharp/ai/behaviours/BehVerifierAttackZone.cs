using System;

namespace ai.behaviours
{
	// Token: 0x0200090B RID: 2315
	public class BehVerifierAttackZone : BehCitizenActionCity
	{
		// Token: 0x06004594 RID: 17812 RVA: 0x001D2C14 File Offset: 0x001D0E14
		public override BehResult execute(Actor pActor)
		{
			if (pActor.city == null)
			{
				return BehResult.Stop;
			}
			TileZone tAttackZone = pActor.city.target_attack_zone;
			if (!pActor.city.hasAttackZoneOrder())
			{
				return BehResult.Stop;
			}
			City tAttackedCity = pActor.city.target_attack_zone.city;
			if (tAttackedCity == null)
			{
				return BehResult.Stop;
			}
			if (tAttackZone == null)
			{
				return BehResult.Stop;
			}
			if (pActor.kingdom.isEnemy(tAttackedCity.kingdom))
			{
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}
	}
}
