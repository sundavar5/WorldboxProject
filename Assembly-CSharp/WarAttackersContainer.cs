using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020007C3 RID: 1987
public class WarAttackersContainer : WarBannersContainer
{
	// Token: 0x06003EC4 RID: 16068 RVA: 0x001B3586 File Offset: 0x001B1786
	protected override IEnumerator showContent()
	{
		bool tHasWon = false;
		bool tHasLost = false;
		WarWinner winner = base.war.data.winner;
		if (winner != WarWinner.Attackers)
		{
			if (winner == WarWinner.Defenders)
			{
				tHasLost = true;
			}
		}
		else
		{
			tHasWon = true;
		}
		foreach (Kingdom tKingdom in base.war.getAttackers())
		{
			yield return base.showBanner(tKingdom, false, tHasWon, tHasLost);
		}
		IEnumerator<Kingdom> enumerator = null;
		foreach (Kingdom tKingdom2 in base.war.getDiedAttackers())
		{
			yield return base.showBanner(tKingdom2, false, false, false);
		}
		enumerator = null;
		foreach (Kingdom tKingdom3 in base.war.getPastAttackers())
		{
			yield return base.showBanner(tKingdom3, true, false, false);
		}
		enumerator = null;
		yield break;
		yield break;
	}
}
