using System;

namespace ai.behaviours
{
	// Token: 0x020008AC RID: 2220
	public class BehCityActorGetResourceFromStorage : BehCityActor
	{
		// Token: 0x060044AD RID: 17581 RVA: 0x001CEBD6 File Offset: 0x001CCDD6
		public BehCityActorGetResourceFromStorage(string pResourceId, int pAmount)
		{
			this._resource_id = pResourceId;
			this._amount = pAmount;
		}

		// Token: 0x060044AE RID: 17582 RVA: 0x001CEBEC File Offset: 0x001CCDEC
		public override BehResult execute(Actor pActor)
		{
			City tCity = pActor.city;
			if (!tCity.hasStorages())
			{
				return BehResult.Stop;
			}
			if (tCity.getResourcesAmount(this._resource_id) < this._amount)
			{
				return BehResult.Stop;
			}
			tCity.takeResource(this._resource_id, this._amount);
			pActor.addToInventory(this._resource_id, this._amount);
			return BehResult.Continue;
		}

		// Token: 0x0400316F RID: 12655
		private string _resource_id;

		// Token: 0x04003170 RID: 12656
		private int _amount;
	}
}
