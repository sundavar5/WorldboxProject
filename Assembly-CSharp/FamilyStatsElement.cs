using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000695 RID: 1685
public class FamilyStatsElement : FamilyElement, IStatsElement, IRefreshElement
{
	// Token: 0x060035E9 RID: 13801 RVA: 0x00189EEF File Offset: 0x001880EF
	public void setIconValue(string pName, float pMainVal, float? pMax = null, string pColor = "", bool pFloat = false, string pEnding = "", char pSeparator = '/')
	{
		this._stats_icons.setIconValue(pName, pMainVal, pMax, pColor, pFloat, pEnding, pSeparator);
	}

	// Token: 0x060035EA RID: 13802 RVA: 0x00189F07 File Offset: 0x00188107
	protected override void Awake()
	{
		this._stats_icons = base.gameObject.AddOrGetComponent<StatsIconContainer>();
		base.Awake();
	}

	// Token: 0x060035EB RID: 13803 RVA: 0x00189F20 File Offset: 0x00188120
	protected override IEnumerator showContent()
	{
		if (base.family == null)
		{
			yield break;
		}
		if (!base.family.isAlive())
		{
			yield break;
		}
		this._stats_icons.showGeneralIcons<Family, FamilyData>(base.family);
		yield break;
	}

	// Token: 0x060035ED RID: 13805 RVA: 0x00189F37 File Offset: 0x00188137
	GameObject IStatsElement.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x0400280D RID: 10253
	private StatsIconContainer _stats_icons;
}
