using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000678 RID: 1656
public class CultureStatsElement : CultureElement, IStatsElement, IRefreshElement
{
	// Token: 0x0600355C RID: 13660 RVA: 0x001887E1 File Offset: 0x001869E1
	public void setIconValue(string pName, float pMainVal, float? pMax = null, string pColor = "", bool pFloat = false, string pEnding = "", char pSeparator = '/')
	{
		this._stats_icons.setIconValue(pName, pMainVal, pMax, pColor, pFloat, pEnding, pSeparator);
	}

	// Token: 0x0600355D RID: 13661 RVA: 0x001887F9 File Offset: 0x001869F9
	protected override void Awake()
	{
		this._stats_icons = base.gameObject.AddOrGetComponent<StatsIconContainer>();
		base.Awake();
	}

	// Token: 0x0600355E RID: 13662 RVA: 0x00188812 File Offset: 0x00186A12
	protected override IEnumerator showContent()
	{
		if (base.culture == null)
		{
			yield break;
		}
		if (!base.culture.isAlive())
		{
			yield break;
		}
		this._stats_icons.showGeneralIcons<Culture, CultureData>(base.culture);
		this.setIconValue("i_cities", (float)base.culture.countCities(), null, "", false, "", '/');
		this.setIconValue("i_kingdoms", (float)base.culture.countKingdoms(), null, "", false, "", '/');
		this.setIconValue("i_books", (float)base.culture.books.count(), null, "", false, "", '/');
		yield break;
	}

	// Token: 0x06003560 RID: 13664 RVA: 0x00188829 File Offset: 0x00186A29
	GameObject IStatsElement.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x040027E2 RID: 10210
	private StatsIconContainer _stats_icons;
}
