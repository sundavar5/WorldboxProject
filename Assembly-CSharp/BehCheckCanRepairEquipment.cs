using System;
using ai.behaviours;

// Token: 0x020003B4 RID: 948
public class BehCheckCanRepairEquipment : BehCityActor
{
	// Token: 0x06002222 RID: 8738 RVA: 0x0011F714 File Offset: 0x0011D914
	public override BehResult execute(Actor pActor)
	{
		if (!pActor.hasEquipment())
		{
			return BehResult.Stop;
		}
		bool tCanRepairAny = false;
		foreach (ActorEquipmentSlot tSlot in pActor.equipment)
		{
			if (tSlot.getItem().needRepair())
			{
				int tRepairCost = (int)((float)tSlot.getItem().getAsset().cost_gold * SimGlobals.m.item_repair_cost_multiplier);
				if (pActor.money >= tRepairCost)
				{
					tCanRepairAny = true;
				}
			}
		}
		if (!tCanRepairAny)
		{
			return BehResult.Stop;
		}
		return BehResult.Continue;
	}
}
