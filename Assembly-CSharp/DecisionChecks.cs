using System;

// Token: 0x020000E3 RID: 227
public readonly ref struct DecisionChecks
{
	// Token: 0x060006B3 RID: 1715 RVA: 0x00063E82 File Offset: 0x00062082
	public DecisionChecks(bool pIsHungry, bool pIsFighting, bool pIsHerd, bool pIsAdult, bool pIsCiv, bool pIsSapient, bool pCityIsInDanger, bool pCanCaptureCity)
	{
		this.is_hungry = pIsHungry;
		this.is_fighting = pIsFighting;
		this.is_herd = pIsHerd;
		this.is_adult = pIsAdult;
		this.is_civ = pIsCiv;
		this.is_sapient = pIsSapient;
		this.city_is_in_danger = pCityIsInDanger;
		this.can_capture_city = pCanCaptureCity;
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x00063EC4 File Offset: 0x000620C4
	public DecisionChecks(Actor pActor)
	{
		this.is_hungry = pActor.isHungry();
		this.is_fighting = pActor.isFighting();
		this.is_herd = pActor.asset.follow_herd;
		this.is_adult = pActor.isAdult();
		this.is_sapient = pActor.isSapient();
		this.is_civ = pActor.isKingdomCiv();
		this.city_is_in_danger = (pActor.inOwnCityBorders() && pActor.city.isInDanger());
		ProfessionAsset profession_asset = pActor.profession_asset;
		this.can_capture_city = (profession_asset != null && profession_asset.can_capture);
	}

	// Token: 0x04000769 RID: 1897
	public readonly bool is_hungry;

	// Token: 0x0400076A RID: 1898
	public readonly bool is_fighting;

	// Token: 0x0400076B RID: 1899
	public readonly bool is_herd;

	// Token: 0x0400076C RID: 1900
	public readonly bool is_adult;

	// Token: 0x0400076D RID: 1901
	public readonly bool is_civ;

	// Token: 0x0400076E RID: 1902
	public readonly bool is_sapient;

	// Token: 0x0400076F RID: 1903
	public readonly bool city_is_in_danger;

	// Token: 0x04000770 RID: 1904
	public readonly bool can_capture_city;
}
