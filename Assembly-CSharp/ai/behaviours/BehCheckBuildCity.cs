using System;

namespace ai.behaviours
{
	// Token: 0x02000890 RID: 2192
	public class BehCheckBuildCity : BehaviourActionActor
	{
		// Token: 0x06004469 RID: 17513 RVA: 0x001CDE48 File Offset: 0x001CC048
		public override BehResult execute(Actor pActor)
		{
			if (pActor.current_tile.zone.hasCity())
			{
				return BehResult.Stop;
			}
			if (!WorldLawLibrary.world_law_kingdom_expansion.isEnabled())
			{
				return BehResult.Stop;
			}
			if (!pActor.current_tile.zone.isGoodForNewCity(pActor))
			{
				return BehResult.Stop;
			}
			City tNewCity = BehaviourActionBase<Actor>.world.cities.buildNewCity(pActor, pActor.current_zone);
			pActor.joinCity(tNewCity);
			return BehResult.Continue;
		}
	}
}
