using System;

namespace ai.behaviours
{
	// Token: 0x0200088E RID: 2190
	public class BehBuildingTargetLoverHome : BehCityActor
	{
		// Token: 0x06004462 RID: 17506 RVA: 0x001CDC9C File Offset: 0x001CBE9C
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.hasLover())
			{
				return BehResult.Stop;
			}
			Building tHomeBuilding = this.getLoverHomeBuilding(pActor, pActor.lover);
			if (tHomeBuilding == null)
			{
				return BehResult.Stop;
			}
			pActor.beh_building_target = tHomeBuilding;
			return BehResult.Continue;
		}

		// Token: 0x06004463 RID: 17507 RVA: 0x001CDCD0 File Offset: 0x001CBED0
		private Building getLoverHomeBuilding(Actor pActor1, Actor pActor2)
		{
			if (pActor1.hasHouse() && pActor2.hasHouse())
			{
				if (pActor1.isSexMale())
				{
					return pActor1.getHomeBuilding();
				}
				return pActor2.getHomeBuilding();
			}
			else
			{
				if (pActor1.hasHouse())
				{
					return pActor1.getHomeBuilding();
				}
				if (pActor2.hasHouse())
				{
					return pActor2.getHomeBuilding();
				}
				return null;
			}
		}
	}
}
