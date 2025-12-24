using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000602 RID: 1538
public class StatsIconContainer : MonoBehaviour
{
	// Token: 0x06003284 RID: 12932 RVA: 0x0017F46C File Offset: 0x0017D66C
	protected void Awake()
	{
		foreach (StatsIcon tStatsIcon in base.GetComponentsInChildren<StatsIcon>(true))
		{
			if (!this._stats_icons.TryAdd(tStatsIcon.name, tStatsIcon))
			{
				Debug.LogError("Duplicate icon name! " + tStatsIcon.name);
			}
		}
		this.clear();
	}

	// Token: 0x06003285 RID: 12933 RVA: 0x0017F4C2 File Offset: 0x0017D6C2
	protected void OnDisable()
	{
		this.clear();
	}

	// Token: 0x06003286 RID: 12934 RVA: 0x0017F4CA File Offset: 0x0017D6CA
	public bool TryGetValue(string pName, out StatsIcon pIcon)
	{
		return this._stats_icons.TryGetValue(pName, out pIcon);
	}

	// Token: 0x06003287 RID: 12935 RVA: 0x0017F4DC File Offset: 0x0017D6DC
	public void setIconValue(string pName, float pMainVal, float? pMax = null, string pColor = "", bool pFloat = false, string pEnding = "", char pSeparator = '/')
	{
		StatsIcon tIcon = this.getIconViaId(pName);
		if (tIcon == null)
		{
			return;
		}
		if (tIcon.areValuesTooClose(pMainVal))
		{
			return;
		}
		tIcon.setValue(pMainVal, pMax, pColor, pFloat, pEnding, pSeparator, false);
		tIcon.textScaleAnimation();
	}

	// Token: 0x06003288 RID: 12936 RVA: 0x0017F51C File Offset: 0x0017D71C
	public void setText(string pName, string pText, string pColor)
	{
		StatsIcon tIcon = this.getIconViaId(pName);
		if (tIcon == null)
		{
			return;
		}
		tIcon.enable_animation = false;
		tIcon.checkDestroyTween();
		tIcon.text.text = pText;
		tIcon.text.color = Toolbox.makeColor(pColor);
	}

	// Token: 0x06003289 RID: 12937 RVA: 0x0017F568 File Offset: 0x0017D768
	public StatsIcon getIconViaId(string pName)
	{
		StatsIcon tIcon;
		this._stats_icons.TryGetValue(pName, out tIcon);
		if (tIcon == null)
		{
			return null;
		}
		tIcon.gameObject.SetActive(true);
		return tIcon;
	}

	// Token: 0x0600328A RID: 12938 RVA: 0x0017F59C File Offset: 0x0017D79C
	protected void clear()
	{
		foreach (StatsIcon statsIcon in this._stats_icons.Values)
		{
			statsIcon.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600328B RID: 12939 RVA: 0x0017F5F8 File Offset: 0x0017D7F8
	public void showGeneralIcons<TMetaObject, TData>(TMetaObject pMetaObject) where TMetaObject : MetaObject<TData>, IMetaObject where TData : MetaObjectData
	{
		int tHoused = pMetaObject.countHoused();
		int tHomeless = pMetaObject.countHomeless();
		this.setIconValue("i_renown", (float)pMetaObject.getRenown(), null, "", false, "", '/');
		this.setIconValue("i_age", (float)pMetaObject.getAge(), null, "", false, "", '/');
		this.setIconValue("i_births", (float)pMetaObject.getTotalBirths(), null, "", false, "", '/');
		this.setIconValue("i_kings", (float)pMetaObject.countKings(), null, "", false, "", '/');
		this.setIconValue("i_leaders", (float)pMetaObject.countLeaders(), null, "", false, "", '/');
		this.setIconValue("i_population", (float)pMetaObject.countUnits(), null, "", false, "", '/');
		this.setIconValue("i_members", (float)pMetaObject.countUnits(), null, "", false, "", '/');
		this.setIconValue("i_total_money", (float)pMetaObject.countTotalMoney(), null, "", false, "", '/');
		this.setIconValue("i_adults", (float)pMetaObject.countAdults(), null, "", false, "", '/');
		string pName = "i_children";
		float pMainVal = (float)pMetaObject.countChildren();
		string pColor = (pMetaObject.getRatioChildren() > 0.5f) ? "#43FF43" : string.Empty;
		this.setIconValue(pName, pMainVal, null, pColor, false, "", '/');
		string pName2 = "i_males";
		float pMainVal2 = (float)pMetaObject.countMales();
		pColor = ((pMetaObject.getRatioMales() > 0.7f) ? "#43FF43" : string.Empty);
		this.setIconValue(pName2, pMainVal2, null, pColor, false, "", '/');
		string pName3 = "i_females";
		float pMainVal3 = (float)pMetaObject.countFemales();
		pColor = ((pMetaObject.getRatioFemales() > 0.7f) ? "#43FF43" : string.Empty);
		this.setIconValue(pName3, pMainVal3, null, pColor, false, "", '/');
		this.setIconValue("i_single_females", (float)pMetaObject.countSingleFemales(), null, "", false, "", '/');
		this.setIconValue("i_single_males", (float)pMetaObject.countSingleMales(), null, "", false, "", '/');
		this.setIconValue("i_families", (float)pMetaObject.countFamilies(), null, "", false, "", '/');
		this.setIconValue("i_couples", (float)pMetaObject.countCouples(), null, "", false, "", '/');
		this.setIconValue("i_deaths", (float)pMetaObject.getTotalDeaths(), null, "", false, "", '/');
		string pName4 = "i_happy_units";
		float pMainVal4 = (float)pMetaObject.countHappyUnits();
		pColor = ((pMetaObject.getRatioHappy() > 0.7f) ? "#43FF43" : string.Empty);
		this.setIconValue(pName4, pMainVal4, null, pColor, false, "", '/');
		string pName5 = "i_unhappy_units";
		float pMainVal5 = (float)pMetaObject.countUnhappyUnits();
		pColor = ((pMetaObject.getRatioUnhappy() > 0.4f) ? "#FB2C21" : string.Empty);
		this.setIconValue(pName5, pMainVal5, null, pColor, false, "", '/');
		string pName6 = "i_sick";
		float pMainVal6 = (float)pMetaObject.countSick();
		pColor = ((pMetaObject.getRatioSick() > 0.3f) ? "#FB2C21" : string.Empty);
		this.setIconValue(pName6, pMainVal6, null, pColor, false, "", '/');
		string pName7 = "i_hungry";
		float pMainVal7 = (float)pMetaObject.countHungry();
		pColor = ((pMetaObject.getRatioHungry() > 0.5f) ? "#FB2C21" : string.Empty);
		this.setIconValue(pName7, pMainVal7, null, pColor, false, "", '/');
		string pName8 = "i_starving";
		float pMainVal8 = (float)pMetaObject.countStarving();
		pColor = ((pMetaObject.getRatioStarving() > 0.3f) ? "#FB2C21" : string.Empty);
		this.setIconValue(pName8, pMainVal8, null, pColor, false, "", '/');
		string pName9 = "i_housed";
		float pMainVal9 = (float)tHoused;
		pColor = ((pMetaObject.getRatioHoused() > 0.8f) ? "#43FF43" : string.Empty);
		this.setIconValue(pName9, pMainVal9, null, pColor, false, "", '/');
		string pName10 = "i_homeless";
		float pMainVal10 = (float)tHomeless;
		pColor = ((pMetaObject.getRatioHomeless() > 0.3f) ? "#FB2C21" : string.Empty);
		this.setIconValue(pName10, pMainVal10, null, pColor, false, "", '/');
		this.setIconValue("i_kills", (float)pMetaObject.getTotalKills(), null, "", false, "", '/');
	}

	// Token: 0x04002631 RID: 9777
	private Dictionary<string, StatsIcon> _stats_icons = new Dictionary<string, StatsIcon>();
}
