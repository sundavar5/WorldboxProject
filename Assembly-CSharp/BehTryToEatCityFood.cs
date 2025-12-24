using System;
using ai.behaviours;

// Token: 0x020003A5 RID: 933
public class BehTryToEatCityFood : BehCityActor
{
	// Token: 0x060021EA RID: 8682 RVA: 0x0011E1F8 File Offset: 0x0011C3F8
	public override BehResult execute(Actor pActor)
	{
		City tCity = pActor.city;
		if (!tCity.hasSuitableFood(pActor.subspecies))
		{
			return BehResult.Stop;
		}
		ResourceAsset tFoodItem = tCity.getFoodItem(pActor.subspecies, pActor.data.favorite_food);
		bool tNeedToPay = !pActor.isFoodFreeForThisPerson();
		if (tFoodItem != null)
		{
			if (tNeedToPay && !pActor.hasEnoughMoney(tFoodItem.money_cost))
			{
				return BehResult.Stop;
			}
			this.eatFood(pActor, tCity, tFoodItem, tNeedToPay);
			if (pActor.hasTrait("gluttonous"))
			{
				tFoodItem = tCity.getFoodItem(pActor.subspecies, pActor.data.favorite_food);
				if (tFoodItem != null && tNeedToPay && pActor.hasEnoughMoney(tFoodItem.money_cost))
				{
					this.eatFood(pActor, tCity, tFoodItem, true);
				}
			}
		}
		return BehResult.Continue;
	}

	// Token: 0x060021EB RID: 8683 RVA: 0x0011E2A2 File Offset: 0x0011C4A2
	private void eatFood(Actor pActor, City pCity, ResourceAsset pFoodItem, bool pNeedToPay)
	{
		if (pNeedToPay)
		{
			pActor.spendMoney(pFoodItem.money_cost);
		}
		pCity.eatFoodItem(pFoodItem.id);
		pActor.consumeFoodResource(pFoodItem);
	}
}
