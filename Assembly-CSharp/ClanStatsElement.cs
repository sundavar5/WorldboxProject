using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200066A RID: 1642
public class ClanStatsElement : ClanElement, IStatsElement, IRefreshElement
{
	// Token: 0x0600351C RID: 13596 RVA: 0x00187E80 File Offset: 0x00186080
	public void setIconValue(string pName, float pMainVal, float? pMax = null, string pColor = "", bool pFloat = false, string pEnding = "", char pSeparator = '/')
	{
		this._stats_icons.setIconValue(pName, pMainVal, pMax, pColor, pFloat, pEnding, pSeparator);
	}

	// Token: 0x0600351D RID: 13597 RVA: 0x00187E98 File Offset: 0x00186098
	protected override void Awake()
	{
		this._stats_icons = base.gameObject.AddOrGetComponent<StatsIconContainer>();
		base.Awake();
	}

	// Token: 0x0600351E RID: 13598 RVA: 0x00187EB1 File Offset: 0x001860B1
	protected override IEnumerator showContent()
	{
		if (base.clan == null)
		{
			yield break;
		}
		if (!base.clan.isAlive())
		{
			yield break;
		}
		this._stats_icons.showGeneralIcons<Clan, ClanData>(base.clan);
		this.setIconValue("i_books_written", (float)base.clan.data.books_written, null, "", false, "", '/');
		yield break;
	}

	// Token: 0x06003520 RID: 13600 RVA: 0x00187EC8 File Offset: 0x001860C8
	GameObject IStatsElement.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x040027D5 RID: 10197
	private StatsIconContainer _stats_icons;
}
