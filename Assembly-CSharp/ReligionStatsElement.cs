using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000740 RID: 1856
public class ReligionStatsElement : ReligionElement, IStatsElement, IRefreshElement
{
	// Token: 0x06003AEC RID: 15084 RVA: 0x0019F916 File Offset: 0x0019DB16
	public void setIconValue(string pName, float pMainVal, float? pMax = null, string pColor = "", bool pFloat = false, string pEnding = "", char pSeparator = '/')
	{
		this._stats_icons.setIconValue(pName, pMainVal, pMax, pColor, pFloat, pEnding, pSeparator);
	}

	// Token: 0x06003AED RID: 15085 RVA: 0x0019F92E File Offset: 0x0019DB2E
	protected override void Awake()
	{
		this._stats_icons = base.gameObject.AddOrGetComponent<StatsIconContainer>();
		base.Awake();
	}

	// Token: 0x06003AEE RID: 15086 RVA: 0x0019F947 File Offset: 0x0019DB47
	protected override IEnumerator showContent()
	{
		if (base.religion == null)
		{
			yield break;
		}
		if (!base.religion.isAlive())
		{
			yield break;
		}
		this._stats_icons.showGeneralIcons<Religion, ReligionData>(base.religion);
		this.setIconValue("i_kingdoms", (float)base.religion.countKingdoms(), null, "", false, "", '/');
		this.setIconValue("i_cities", (float)base.religion.countCities(), null, "", false, "", '/');
		this.setIconValue("i_books", (float)this.meta_object.books.count(), null, "", false, "", '/');
		yield break;
	}

	// Token: 0x06003AF0 RID: 15088 RVA: 0x0019F95E File Offset: 0x0019DB5E
	GameObject IStatsElement.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x04002B89 RID: 11145
	private StatsIconContainer _stats_icons;
}
