using System;

namespace ai.behaviours
{
	// Token: 0x02000970 RID: 2416
	public class CityBehProduceResources : BehaviourActionCity
	{
		// Token: 0x060046CA RID: 18122 RVA: 0x001E0D7A File Offset: 0x001DEF7A
		public override bool shouldRetry(City pCity)
		{
			return false;
		}

		// Token: 0x060046CB RID: 18123 RVA: 0x001E0D80 File Offset: 0x001DEF80
		public override BehResult execute(City pCity)
		{
			ActorAsset tActorAsset = pCity.getActorAsset();
			if (tActorAsset.production == null)
			{
				return BehResult.Stop;
			}
			foreach (string tResourceID in tActorAsset.production.LoopRandom<string>())
			{
				ResourceAsset tAsset = AssetManager.resources.get(tResourceID);
				int tCurrentStorage = pCity.getResourcesAmount(tResourceID);
				if (tCurrentStorage <= tAsset.maximum)
				{
					int pAmount = pCity.status.population / 10 + 1;
					if (tCurrentStorage < tAsset.produce_min)
					{
						pAmount = tAsset.produce_min;
					}
					this.tryToProduce(tAsset, pCity, pAmount);
				}
			}
			return BehResult.Continue;
		}

		// Token: 0x060046CC RID: 18124 RVA: 0x001E0E2C File Offset: 0x001DF02C
		private bool tryToProduce(ResourceAsset pAsset, City pCity, int pAmount = 1)
		{
			for (int i = 0; i < pAmount; i++)
			{
				if (pCity.getResourcesAmount(pAsset.id) == pAsset.maximum)
				{
					return false;
				}
				foreach (string tRequired in pAsset.ingredients)
				{
					if (pCity.getResourcesAmount(tRequired) < pAsset.ingredients_amount)
					{
						return false;
					}
				}
				foreach (string tRequired2 in pAsset.ingredients)
				{
					pCity.takeResource(tRequired2, pAsset.ingredients_amount);
				}
				if (pCity.addResourcesToRandomStockpile(pAsset.id, 1) <= 0)
				{
					return false;
				}
			}
			return true;
		}
	}
}
