using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020007C6 RID: 1990
public class WarDefendersContainer : WarBannersContainer
{
	// Token: 0x06003ED1 RID: 16081 RVA: 0x001B384B File Offset: 0x001B1A4B
	protected override IEnumerator showContent()
	{
		bool tHasWon = false;
		bool tHasLost = false;
		WarWinner winner = base.war.data.winner;
		if (winner != WarWinner.Attackers)
		{
			if (winner == WarWinner.Defenders)
			{
				tHasWon = true;
			}
		}
		else
		{
			tHasLost = true;
		}
		foreach (Kingdom tKingdom in base.war.getDefenders())
		{
			yield return base.showBanner(tKingdom, false, tHasWon, tHasLost);
		}
		IEnumerator<Kingdom> enumerator = null;
		foreach (Kingdom tKingdom2 in base.war.getDiedDefenders())
		{
			yield return base.showBanner(tKingdom2, false, false, false);
		}
		enumerator = null;
		foreach (Kingdom tKingdom3 in base.war.getPastDefenders())
		{
			yield return base.showBanner(tKingdom3, true, false, false);
		}
		enumerator = null;
		yield break;
		yield break;
	}
}
