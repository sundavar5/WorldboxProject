using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200065D RID: 1629
public class CityStatsElement : CityElement, IStatsElement, IRefreshElement
{
	// Token: 0x060034C9 RID: 13513 RVA: 0x00186AD9 File Offset: 0x00184CD9
	public void setIconValue(string pName, float pMainVal, float? pMax = null, string pColor = "", bool pFloat = false, string pEnding = "", char pSeparator = '/')
	{
		this._stats_icons.setIconValue(pName, pMainVal, pMax, pColor, pFloat, pEnding, pSeparator);
	}

	// Token: 0x060034CA RID: 13514 RVA: 0x00186AF1 File Offset: 0x00184CF1
	protected override void Awake()
	{
		this._stats_icons = base.gameObject.AddOrGetComponent<StatsIconContainer>();
		base.Awake();
	}

	// Token: 0x060034CB RID: 13515 RVA: 0x00186B0A File Offset: 0x00184D0A
	protected override IEnumerator showContent()
	{
		if (base.city == null)
		{
			yield break;
		}
		if (!base.city.isAlive())
		{
			yield break;
		}
		int tPopulationPeople = base.city.getPopulationPeople();
		int tTotalFood = base.city.countFoodTotal();
		this._stats_icons.showGeneralIcons<City, CityData>(base.city);
		if (tPopulationPeople > base.city.getPopulationMaximum())
		{
			this.setIconValue("i_population", (float)tPopulationPeople, new float?((float)base.city.getPopulationMaximum()), "#FB2C21", false, "", '/');
		}
		else
		{
			this.setIconValue("i_population", (float)tPopulationPeople, new float?((float)base.city.getPopulationMaximum()), "", false, "", '/');
		}
		this.setIconValue("i_territory", (float)base.city.countZones(), null, "", false, "", '/');
		this.setIconValue("i_boats", (float)base.city.countBoats(), null, "", false, "", '/');
		string pName = "i_food";
		float pMainVal = (float)tTotalFood;
		string pColor = (tTotalFood > tPopulationPeople * 4) ? "#43FF43" : "#FB2C21";
		this.setIconValue(pName, pMainVal, null, pColor, false, "", '/');
		this.setIconValue("i_farmers", (float)base.city.jobs.countOccupied(CitizenJobLibrary.farmer), null, "", false, "", '/');
		this.setIconValue("i_books", (float)base.city.countBooks(), null, "", false, "", '/');
		int tLoyalty = base.city.getLoyalty(true);
		if (tLoyalty > 0)
		{
			this.setIconValue("i_loyalty", (float)tLoyalty, null, "#43FF43", false, "", '/');
		}
		else
		{
			this.setIconValue("i_loyalty", (float)tLoyalty, null, "#FB2C21", false, "", '/');
		}
		this._loyalty_element.setCity(base.city);
		if (WorldLawLibrary.world_law_civ_army.isEnabled())
		{
			this.setIconValue("i_army", (float)base.city.countWarriors(), new float?((float)base.city.getMaxWarriors()), "", false, "", '/');
		}
		else
		{
			this.setIconValue("i_army", (float)base.city.countWarriors(), null, "", false, "", '/');
		}
		this.setIconValue("i_houses", (float)base.city.getHouseCurrent(), new float?((float)base.city.getHouseLimit()), "", false, "", '/');
		yield break;
	}

	// Token: 0x060034CD RID: 13517 RVA: 0x00186B21 File Offset: 0x00184D21
	GameObject IStatsElement.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x040027B7 RID: 10167
	[SerializeField]
	private CityLoyaltyElement _loyalty_element;

	// Token: 0x040027B8 RID: 10168
	private StatsIconContainer _stats_icons;
}
