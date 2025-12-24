using System;

namespace ai.behaviours
{
	// Token: 0x020008E4 RID: 2276
	public class BehJoinCity : BehaviourActionActor
	{
		// Token: 0x0600453B RID: 17723 RVA: 0x001D16E4 File Offset: 0x001CF8E4
		public override BehResult execute(Actor pActor)
		{
			City tZoneCity = pActor.current_zone.city;
			if (tZoneCity == null)
			{
				return BehResult.Stop;
			}
			if (!tZoneCity.isPossibleToJoin(pActor))
			{
				return BehResult.Stop;
			}
			if (tZoneCity.isNeutral())
			{
				if (pActor.kingdom.isCiv())
				{
					tZoneCity.setKingdom(pActor.kingdom, false);
				}
				else
				{
					Kingdom tNewKingdom = BehaviourActionBase<Actor>.world.kingdoms.makeNewCivKingdom(pActor, null, true);
					pActor.createDefaultCultureAndLanguageAndClan(null);
					tZoneCity.setKingdom(tNewKingdom, false);
					tZoneCity.setUnitMetas(pActor);
					tNewKingdom.setUnitMetas(pActor);
				}
			}
			if (tZoneCity.kingdom != pActor.kingdom)
			{
				pActor.removeFromPreviousFaction();
			}
			pActor.joinCity(tZoneCity);
			pActor.setMetasFromCity(tZoneCity);
			return BehResult.Continue;
		}
	}
}
