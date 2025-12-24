using System;
using System.Collections;
using UnityEngine;

// Token: 0x020006F9 RID: 1785
public class KingdomStatsElement : KingdomElement, IStatsElement, IRefreshElement
{
	// Token: 0x0600393D RID: 14653 RVA: 0x00198328 File Offset: 0x00196528
	public void setIconValue(string pName, float pMainVal, float? pMax = null, string pColor = "", bool pFloat = false, string pEnding = "", char pSeparator = '/')
	{
		this._stats_icons.setIconValue(pName, pMainVal, pMax, pColor, pFloat, pEnding, pSeparator);
	}

	// Token: 0x0600393E RID: 14654 RVA: 0x00198340 File Offset: 0x00196540
	protected override void Awake()
	{
		this._stats_icons = base.gameObject.AddOrGetComponent<StatsIconContainer>();
		base.Awake();
	}

	// Token: 0x0600393F RID: 14655 RVA: 0x00198359 File Offset: 0x00196559
	protected override IEnumerator showContent()
	{
		if (base.kingdom == null)
		{
			yield break;
		}
		if (!base.kingdom.isAlive())
		{
			yield break;
		}
		this._stats_icons.showGeneralIcons<Kingdom, KingdomData>(base.kingdom);
		this.setIconValue("i_population", (float)base.kingdom.getPopulationPeople(), new float?((float)base.kingdom.getPopulationTotalPossible()), "", false, "", '/');
		this.setIconValue("i_army", (float)base.kingdom.countTotalWarriors(), new float?((float)base.kingdom.countWarriorsMax()), "", false, "", '/');
		if (base.kingdom.countCities() > base.kingdom.getMaxCities())
		{
			this.setIconValue("i_cities", (float)base.kingdom.countCities(), new float?((float)base.kingdom.getMaxCities()), "#FB2C21", false, "", '/');
		}
		else
		{
			this.setIconValue("i_cities", (float)base.kingdom.countCities(), new float?((float)base.kingdom.getMaxCities()), "", false, "", '/');
		}
		this.setIconValue("i_territory", (float)base.kingdom.countZones(), null, "", false, "", '/');
		this.setIconValue("i_buildings", (float)base.kingdom.countBuildings(), null, "", false, "", '/');
		this.setIconValue("i_food", (float)base.kingdom.countTotalFood(), null, "", false, "", '/');
		yield break;
	}

	// Token: 0x06003941 RID: 14657 RVA: 0x00198370 File Offset: 0x00196570
	GameObject IStatsElement.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x04002A48 RID: 10824
	private StatsIconContainer _stats_icons;
}
