using System;

namespace ai.behaviours
{
	// Token: 0x0200089C RID: 2204
	public class BehCheckSameCityActorLover : BehCitizenActionCity
	{
		// Token: 0x06004483 RID: 17539 RVA: 0x001CE204 File Offset: 0x001CC404
		public override bool errorsFound(Actor pObject)
		{
			return base.errorsFound(pObject) || !pObject.hasLover() || pObject.lover.isRekt() || pObject.lover.city.isRekt();
		}

		// Token: 0x06004484 RID: 17540 RVA: 0x001CE240 File Offset: 0x001CC440
		public override BehResult execute(Actor pActor)
		{
			City tCity = pActor.lover.city;
			pActor.clearHomeBuilding();
			pActor.stopBeingWarrior();
			pActor.joinCity(tCity);
			return BehResult.Continue;
		}
	}
}
