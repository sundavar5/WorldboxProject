using System;

namespace ai.behaviours
{
	// Token: 0x020008E9 RID: 2281
	public class BehMakeItem : BehCityActor
	{
		// Token: 0x06004546 RID: 17734 RVA: 0x001D1928 File Offset: 0x001CFB28
		public override BehResult execute(Actor pActor)
		{
			ItemCrafting.tryToCraftRandomWeapon(pActor, pActor.city);
			int i = 0;
			while (i < 5 && ItemCrafting.tryToCraftRandomEquipment(pActor, pActor.city))
			{
				i++;
			}
			return BehResult.Continue;
		}
	}
}
