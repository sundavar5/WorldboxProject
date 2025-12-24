using System;

namespace ai.behaviours
{
	// Token: 0x020008A8 RID: 2216
	public class BehCityActorFindUngrownCrop : BehCityActor
	{
		// Token: 0x060044A3 RID: 17571 RVA: 0x001CE81C File Offset: 0x001CCA1C
		public override BehResult execute(Actor pActor)
		{
			City tCity = pActor.city;
			BehResult result;
			using (ListPool<Building> tPotCrops = new ListPool<Building>())
			{
				foreach (WorldTile worldTile in tCity.calculated_crops)
				{
					Building tBuilding = worldTile.building;
					if (!tBuilding.isRekt() && tBuilding.asset.wheat && !tBuilding.component_wheat.isMaxLevel())
					{
						tPotCrops.Add(tBuilding);
					}
				}
				if (tPotCrops.Count == 0)
				{
					result = BehResult.Stop;
				}
				else
				{
					pActor.beh_building_target = tPotCrops.GetRandom<Building>();
					result = BehResult.Continue;
				}
			}
			return result;
		}
	}
}
