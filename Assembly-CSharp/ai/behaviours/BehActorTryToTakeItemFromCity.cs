using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x02000909 RID: 2313
	public class BehActorTryToTakeItemFromCity : BehCityActor
	{
		// Token: 0x06004590 RID: 17808 RVA: 0x001D2B8C File Offset: 0x001D0D8C
		public override BehResult execute(Actor pActor)
		{
			City tCity = pActor.city;
			foreach (List<long> tListEquipmentTypes in tCity.data.equipment.getAllEquipmentLists())
			{
				City.giveItem(pActor, tListEquipmentTypes, tCity);
			}
			return BehResult.Continue;
		}
	}
}
