using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007CB RID: 1995
public class WarTooltipBannersContainer : MonoBehaviour
{
	// Token: 0x06003EF4 RID: 16116 RVA: 0x001B40EC File Offset: 0x001B22EC
	public void load(War pWar)
	{
		this._banner_right.gameObject.SetActive(false);
		this._banner_left.gameObject.SetActive(false);
		this._total_war.gameObject.SetActive(false);
		Kingdom tMainAttacker = pWar.main_attacker;
		if (!tMainAttacker.isRekt())
		{
			this._banner_left.gameObject.SetActive(true);
			this._banner_left.load(tMainAttacker);
		}
		if (pWar.isTotalWar())
		{
			this._total_war.gameObject.SetActive(true);
			return;
		}
		Kingdom tMainDefender = pWar.getMainDefender();
		if (!tMainDefender.isRekt())
		{
			this._banner_right.gameObject.SetActive(true);
			this._banner_right.load(tMainDefender);
		}
	}

	// Token: 0x04002DD5 RID: 11733
	[SerializeField]
	private KingdomBanner _banner_left;

	// Token: 0x04002DD6 RID: 11734
	[SerializeField]
	private KingdomBanner _banner_right;

	// Token: 0x04002DD7 RID: 11735
	[SerializeField]
	private Image _total_war;
}
