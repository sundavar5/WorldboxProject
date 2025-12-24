using System;
using ai.behaviours;

// Token: 0x020003B3 RID: 947
public class BehActorGiveTax : BehCitizenActionCity
{
	// Token: 0x0600221E RID: 8734 RVA: 0x0011F5DC File Offset: 0x0011D7DC
	public override BehResult execute(Actor pActor)
	{
		if (pActor.isKing())
		{
			pActor.takeAllOwnLoot();
			return BehResult.Continue;
		}
		if (!pActor.city.hasLeader())
		{
			pActor.takeAllOwnLoot();
			return BehResult.Continue;
		}
		Actor tLeader = pActor.city.leader;
		if (pActor.isCityLeader())
		{
			if (!pActor.kingdom.hasKing())
			{
				pActor.takeAllOwnLoot();
			}
			else
			{
				this.payTributeToKing(pActor, pActor.kingdom.king, pActor.kingdom.getTaxRateTribute());
			}
		}
		else
		{
			this.payTaxToLeader(pActor, tLeader, pActor.kingdom.getTaxRateLocal());
		}
		return BehResult.Continue;
	}

	// Token: 0x0600221F RID: 8735 RVA: 0x0011F66C File Offset: 0x0011D86C
	private void payTributeToKing(Actor pActor, Actor pKing, float pTaxRate)
	{
		if (pActor.loot > 0)
		{
			int loot = pActor.loot;
			int tToKing = (int)((float)loot * pTaxRate);
			int tToMe = loot - tToKing;
			int tToCity = (int)((float)tToMe * 0.5f);
			tToMe -= tToCity;
			pActor.city.addResourcesToRandomStockpile("gold", tToCity);
			pActor.addMoney(tToMe);
			pKing.addLoot(tToKing);
			pActor.paidTax(pTaxRate, "fx_money_paid_tribute");
		}
	}

	// Token: 0x06002220 RID: 8736 RVA: 0x0011F6CC File Offset: 0x0011D8CC
	private void payTaxToLeader(Actor pActor, Actor pTarget, float pTaxRate)
	{
		if (pActor.loot > 0)
		{
			int loot = pActor.loot;
			int pLeader = (int)((float)loot * pTaxRate);
			int tToMe = loot - pLeader;
			pActor.addMoney(tToMe);
			pTarget.addLoot(pLeader);
			pActor.paidTax(pTaxRate, "fx_money_paid_tax");
		}
	}
}
