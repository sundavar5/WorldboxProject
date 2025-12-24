using System;
using UnityEngine;

// Token: 0x0200065F RID: 1631
public class SelectedCity : SelectedMetaWithUnit<City, CityData>
{
	// Token: 0x170002D9 RID: 729
	// (get) Token: 0x060034DC RID: 13532 RVA: 0x001872CB File Offset: 0x001854CB
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.City;
		}
	}

	// Token: 0x170002DA RID: 730
	// (get) Token: 0x060034DD RID: 13533 RVA: 0x001872CE File Offset: 0x001854CE
	public override string unit_title_locale_key
	{
		get
		{
			return "titled_leader";
		}
	}

	// Token: 0x060034DE RID: 13534 RVA: 0x001872D5 File Offset: 0x001854D5
	public override bool hasUnit()
	{
		return this.nano_object.hasLeader();
	}

	// Token: 0x060034DF RID: 13535 RVA: 0x001872E2 File Offset: 0x001854E2
	public override Actor getUnit()
	{
		return this.nano_object.leader;
	}

	// Token: 0x060034E0 RID: 13536 RVA: 0x001872EF File Offset: 0x001854EF
	protected override string getPowerTabAssetID()
	{
		return "selected_city";
	}

	// Token: 0x060034E1 RID: 13537 RVA: 0x001872F8 File Offset: 0x001854F8
	protected override void showStatsGeneral(City pCity)
	{
		base.showStatsGeneral(pCity);
		int tPopulationPeople = pCity.getPopulationPeople();
		pCity.countFoodTotal();
		if (tPopulationPeople > pCity.getPopulationMaximum())
		{
			base.setIconValue("i_population", (float)tPopulationPeople, new float?((float)pCity.getPopulationMaximum()), "#FB2C21", false, "", '/');
		}
		else
		{
			base.setIconValue("i_population", (float)tPopulationPeople, new float?((float)pCity.getPopulationMaximum()), "", false, "", '/');
		}
		base.setIconValue("i_territory", (float)pCity.countZones(), null, "", false, "", '/');
		base.setIconValue("i_boats", (float)pCity.countBoats(), null, "", false, "", '/');
		base.setIconValue("i_books", (float)pCity.countBooks(), null, "", false, "", '/');
		int tLoyalty = pCity.getLoyalty(true);
		if (tLoyalty > 0)
		{
			base.setIconValue("i_loyalty", (float)tLoyalty, null, "#43FF43", false, "", '/');
		}
		else
		{
			base.setIconValue("i_loyalty", (float)tLoyalty, null, "#FB2C21", false, "", '/');
		}
		this._loyalty_element.setCity(pCity);
		if (WorldLawLibrary.world_law_civ_army.isEnabled())
		{
			base.setIconValue("i_army", (float)pCity.countWarriors(), new float?((float)pCity.getMaxWarriors()), "", false, "", '/');
		}
		else
		{
			base.setIconValue("i_army", (float)pCity.countWarriors(), null, "", false, "", '/');
		}
		base.setIconValue("i_houses", (float)pCity.getHouseCurrent(), new float?((float)pCity.getHouseLimit()), "", false, "", '/');
	}

	// Token: 0x060034E2 RID: 13538 RVA: 0x001874D5 File Offset: 0x001856D5
	protected override void updateElements(City pNano)
	{
		if (pNano.isRekt())
		{
			return;
		}
		base.updateElements(pNano);
		this._last_storage_version = pNano.getStorageVersion();
	}

	// Token: 0x060034E3 RID: 13539 RVA: 0x001874F3 File Offset: 0x001856F3
	protected override void updateElementsAlways(City pNano)
	{
		base.updateElementsAlways(pNano);
		if (this.storageChanged(pNano))
		{
			this._resources.update(pNano);
			this._food.update(pNano);
		}
	}

	// Token: 0x060034E4 RID: 13540 RVA: 0x0018751D File Offset: 0x0018571D
	protected override void checkAchievements(City pCity)
	{
		AchievementLibrary.checkCityAchievements(pCity);
	}

	// Token: 0x060034E5 RID: 13541 RVA: 0x00187525 File Offset: 0x00185725
	public void openInventoryTab()
	{
		ScrollWindow.showWindow(base.window_id);
		ScrollWindow.getCurrentWindow().tabs.showTab("Inventory");
	}

	// Token: 0x060034E6 RID: 13542 RVA: 0x00187546 File Offset: 0x00185746
	public void openBooksTab()
	{
		ScrollWindow.showWindow(base.window_id);
		ScrollWindow.getCurrentWindow().tabs.showTab("Books");
	}

	// Token: 0x060034E7 RID: 13543 RVA: 0x00187567 File Offset: 0x00185767
	public void openFamilyTab()
	{
		ScrollWindow.showWindow(base.window_id);
		ScrollWindow.getCurrentWindow().tabs.showTab("Family");
	}

	// Token: 0x060034E8 RID: 13544 RVA: 0x00187588 File Offset: 0x00185788
	private bool storageChanged(City pCity)
	{
		return pCity.getStorageVersion() != this._last_storage_version || base.isNanoChanged(pCity);
	}

	// Token: 0x040027BC RID: 10172
	[SerializeField]
	private CityLoyaltyElement _loyalty_element;

	// Token: 0x040027BD RID: 10173
	[SerializeField]
	private CitySelectedResources _resources;

	// Token: 0x040027BE RID: 10174
	[SerializeField]
	private CitySelectedResources _food;

	// Token: 0x040027BF RID: 10175
	private int _last_storage_version;
}
