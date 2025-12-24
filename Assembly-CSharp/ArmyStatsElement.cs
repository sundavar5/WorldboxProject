using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000636 RID: 1590
public class ArmyStatsElement : ArmyElement, IStatsElement, IRefreshElement
{
	// Token: 0x060033CF RID: 13263 RVA: 0x0018408A File Offset: 0x0018228A
	public void setIconValue(string pName, float pMainVal, float? pMax = null, string pColor = "", bool pFloat = false, string pEnding = "", char pSeparator = '/')
	{
		this._stats_icons.setIconValue(pName, pMainVal, pMax, pColor, pFloat, pEnding, pSeparator);
	}

	// Token: 0x060033D0 RID: 13264 RVA: 0x001840A2 File Offset: 0x001822A2
	protected override void Awake()
	{
		this._stats_icons = base.gameObject.AddOrGetComponent<StatsIconContainer>();
		base.Awake();
	}

	// Token: 0x060033D1 RID: 13265 RVA: 0x001840BB File Offset: 0x001822BB
	protected override IEnumerator showContent()
	{
		if (base.army == null)
		{
			yield break;
		}
		if (!base.army.isAlive())
		{
			yield break;
		}
		this._stats_icons.showGeneralIcons<Army, ArmyData>(base.army);
		this.setIconValue("i_army_size", (float)base.army.countUnits(), null, "", false, "", '/');
		this.setIconValue("i_kills", (float)base.army.getTotalKills(), null, "", false, "", '/');
		this.setIconValue("i_melee", (float)base.army.countMelee(), null, "", false, "", '/');
		this.setIconValue("i_range", (float)base.army.countRange(), null, "", false, "", '/');
		yield break;
	}

	// Token: 0x060033D3 RID: 13267 RVA: 0x001840D2 File Offset: 0x001822D2
	GameObject IStatsElement.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x0400272D RID: 10029
	private StatsIconContainer _stats_icons;
}
