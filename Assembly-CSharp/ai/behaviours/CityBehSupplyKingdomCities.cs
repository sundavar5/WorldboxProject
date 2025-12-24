using System;

namespace ai.behaviours
{
	// Token: 0x02000972 RID: 2418
	public class CityBehSupplyKingdomCities : BehaviourActionCity
	{
		// Token: 0x060046D1 RID: 18129 RVA: 0x001E0F00 File Offset: 0x001DF100
		public override BehResult execute(City pCity)
		{
			if (!pCity.hasCulture())
			{
				return BehResult.Stop;
			}
			if (pCity.kingdom.countCities() == 1)
			{
				return BehResult.Stop;
			}
			if (pCity.data.timer_supply > 0f)
			{
				return BehResult.Stop;
			}
			if (!pCity.hasStockpiles())
			{
				return BehResult.Stop;
			}
			Building tStockpile = pCity.getRandomStockpile();
			if (tStockpile == null)
			{
				return BehResult.Stop;
			}
			BehResult result;
			using (ListPool<CityStorageSlot> tResources = new ListPool<CityStorageSlot>())
			{
				foreach (CityStorageSlot tSlot in tStockpile.resources.getSlots())
				{
					if (tSlot.amount > tSlot.asset.supply_bound_give)
					{
						tResources.Add(tSlot);
					}
				}
				if (tResources.Count == 0)
				{
					result = BehResult.Stop;
				}
				else
				{
					foreach (City tKCity in pCity.kingdom.getCities().LoopRandom<City>())
					{
						if (tKCity != pCity)
						{
							foreach (CityStorageSlot iSlot in tResources.LoopRandom<CityStorageSlot>())
							{
								if (tKCity.getResourcesAmount(iSlot.id) < iSlot.asset.supply_bound_take)
								{
									this.shareResource(pCity, tKCity, iSlot);
									this.updateSupplyTimer(pCity);
									return BehResult.Continue;
								}
							}
						}
					}
					result = BehResult.Continue;
				}
			}
			return result;
		}

		// Token: 0x060046D2 RID: 18130 RVA: 0x001E1090 File Offset: 0x001DF290
		private void updateSupplyTimer(City pCity)
		{
			float tTimer = 30f;
			if (pCity.hasLeader())
			{
				tTimer *= pCity.leader.stats["multiplier_supply_timer"];
			}
			if (tTimer < 10f)
			{
				tTimer = 10f;
			}
			pCity.data.timer_supply = tTimer;
		}

		// Token: 0x060046D3 RID: 18131 RVA: 0x001E10DD File Offset: 0x001DF2DD
		private void shareResource(City pCity, City pTargetCity, CityStorageSlot pSlot)
		{
			pCity.takeResource(pSlot.id, pSlot.asset.supply_give);
			pTargetCity.addResourcesToRandomStockpile(pSlot.id, pSlot.asset.supply_give);
		}
	}
}
