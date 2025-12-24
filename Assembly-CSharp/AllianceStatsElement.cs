using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200062A RID: 1578
public class AllianceStatsElement : AllianceElement, IStatsElement, IRefreshElement
{
	// Token: 0x06003387 RID: 13191 RVA: 0x001836D8 File Offset: 0x001818D8
	public void setIconValue(string pName, float pMainVal, float? pMax = null, string pColor = "", bool pFloat = false, string pEnding = "", char pSeparator = '/')
	{
		this._stats_icons.setIconValue(pName, pMainVal, pMax, pColor, pFloat, pEnding, pSeparator);
	}

	// Token: 0x06003388 RID: 13192 RVA: 0x001836F0 File Offset: 0x001818F0
	protected override void Awake()
	{
		this._stats_icons = base.gameObject.AddOrGetComponent<StatsIconContainer>();
		base.Awake();
	}

	// Token: 0x06003389 RID: 13193 RVA: 0x00183709 File Offset: 0x00181909
	protected override IEnumerator showContent()
	{
		if (base.alliance == null)
		{
			yield break;
		}
		if (!base.alliance.isAlive())
		{
			yield break;
		}
		this._stats_icons.showGeneralIcons<Alliance, AllianceData>(base.alliance);
		this.setIconValue("i_population", (float)base.alliance.countPopulation(), null, "", false, "", '/');
		this.setIconValue("i_army", (float)base.alliance.countWarriors(), null, "", false, "", '/');
		this.setIconValue("i_kingdoms", (float)base.alliance.countKingdoms(), null, "", false, "", '/');
		this.setIconValue("i_cities", (float)base.alliance.countCities(), null, "", false, "", '/');
		this.setIconValue("i_buildings", (float)base.alliance.countBuildings(), null, "", false, "", '/');
		this.setIconValue("i_zones", (float)base.alliance.countZones(), null, "", false, "", '/');
		yield break;
	}

	// Token: 0x0600338B RID: 13195 RVA: 0x00183720 File Offset: 0x00181920
	GameObject IStatsElement.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x04002710 RID: 10000
	private StatsIconContainer _stats_icons;
}
