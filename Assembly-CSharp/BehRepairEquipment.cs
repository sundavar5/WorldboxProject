using System;
using ai.behaviours;

// Token: 0x020003B6 RID: 950
public class BehRepairEquipment : BehCityActor
{
	// Token: 0x06002226 RID: 8742 RVA: 0x0011F7C4 File Offset: 0x0011D9C4
	public override BehResult execute(Actor pActor)
	{
		bool tAnythingRepaired = false;
		foreach (ActorEquipmentSlot tSlot in pActor.equipment)
		{
			if (!tSlot.isEmpty())
			{
				Item tItem = tSlot.getItem();
				if (tItem.needRepair())
				{
					int tRepairCost = (int)((float)tSlot.getItem().getAsset().cost_gold * SimGlobals.m.item_repair_cost_multiplier);
					if (pActor.hasEnoughMoney(tRepairCost))
					{
						pActor.spendMoney(tRepairCost);
						tItem.fullRepair();
						tAnythingRepaired = true;
					}
				}
			}
		}
		if (tAnythingRepaired)
		{
			pActor.setStatsDirty();
		}
		return BehResult.Continue;
	}
}
