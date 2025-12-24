using System;

// Token: 0x0200057F RID: 1407
public interface IRichTracker
{
	// Token: 0x06002EAC RID: 11948
	void trackViewing(string pText);

	// Token: 0x06002EAD RID: 11949
	void trackWatching();

	// Token: 0x06002EAE RID: 11950
	void trackUsing(string pPower);

	// Token: 0x06002EAF RID: 11951
	void updateUsing(int pAmount, string pPower);

	// Token: 0x06002EB0 RID: 11952
	void inspectKingdom(string pKingdom);

	// Token: 0x06002EB1 RID: 11953
	void inspectVillage(string pVillage);

	// Token: 0x06002EB2 RID: 11954
	void inspectUnit(string pUnit);

	// Token: 0x06002EB3 RID: 11955
	void spectatingUnit(string pUnit);

	// Token: 0x06002EB4 RID: 11956
	void trackActivity(string pText);
}
