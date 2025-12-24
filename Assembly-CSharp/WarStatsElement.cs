using System;
using System.Collections;
using UnityEngine;

// Token: 0x020007CA RID: 1994
public class WarStatsElement : WarElement, IStatsElement, IRefreshElement
{
	// Token: 0x06003EEF RID: 16111 RVA: 0x001B409B File Offset: 0x001B229B
	public void setIconValue(string pName, float pMainVal, float? pMax = null, string pColor = "", bool pFloat = false, string pEnding = "", char pSeparator = '/')
	{
		this._stats_icons.setIconValue(pName, pMainVal, pMax, pColor, pFloat, pEnding, pSeparator);
	}

	// Token: 0x06003EF0 RID: 16112 RVA: 0x001B40B3 File Offset: 0x001B22B3
	protected override void Awake()
	{
		this._stats_icons = base.gameObject.AddOrGetComponent<StatsIconContainer>();
		base.Awake();
	}

	// Token: 0x06003EF1 RID: 16113 RVA: 0x001B40CC File Offset: 0x001B22CC
	protected override IEnumerator showContent()
	{
		if (base.war == null)
		{
			yield break;
		}
		if (!base.war.isAlive())
		{
			yield break;
		}
		this.setIconValue("i_age", (float)base.war.getAge(), null, "", false, "", '/');
		this.setIconValue("i_population", (float)base.war.countTotalPopulation(), null, "", false, "", '/');
		this.setIconValue("i_total_army", (float)base.war.countTotalArmy(), null, "", false, "", '/');
		this.setIconValue("i_kingdoms", (float)base.war.countKingdoms(), null, "", false, "", '/');
		this.setIconValue("i_cities", (float)base.war.countCities(), null, "", false, "", '/');
		this.setIconValue("i_deaths", (float)base.war.getTotalDeaths(), null, "", false, "", '/');
		bool tAttackerPopAdvantage = base.war.countAttackersPopulation() > base.war.countDefendersPopulation();
		bool tAttackerDeadAdvantage = base.war.getDeadDefenders() > base.war.getDeadAttackers();
		bool tAttackerWarriorAdvantage = base.war.countAttackersWarriors() > base.war.countDefendersWarriors();
		bool tAttackerCityAdvantage = base.war.countAttackersCities() > base.war.countDefendersCities();
		string pName = "i_attackers_population";
		float pMainVal = (float)base.war.countAttackersPopulation();
		string pColor = tAttackerPopAdvantage ? "#43FF43" : "#FB2C21";
		this.setIconValue(pName, pMainVal, null, pColor, false, "", '/');
		string pName2 = "i_attackers_army";
		float pMainVal2 = (float)base.war.countAttackersWarriors();
		pColor = (tAttackerWarriorAdvantage ? "#43FF43" : "#FB2C21");
		this.setIconValue(pName2, pMainVal2, null, pColor, false, "", '/');
		string pName3 = "i_attackers_dead";
		float pMainVal3 = (float)base.war.getDeadAttackers();
		pColor = (tAttackerDeadAdvantage ? "#43FF43" : "#FB2C21");
		this.setIconValue(pName3, pMainVal3, null, pColor, false, "", '/');
		string pName4 = "i_attackers_cities";
		float pMainVal4 = (float)base.war.countAttackersCities();
		pColor = (tAttackerCityAdvantage ? "#43FF43" : "#FB2C21");
		this.setIconValue(pName4, pMainVal4, null, pColor, false, "", '/');
		string pName5 = "i_defenders_population";
		float pMainVal5 = (float)base.war.countDefendersPopulation();
		pColor = (tAttackerPopAdvantage ? "#FB2C21" : "#43FF43");
		this.setIconValue(pName5, pMainVal5, null, pColor, false, "", '/');
		string pName6 = "i_defenders_army";
		float pMainVal6 = (float)base.war.countDefendersWarriors();
		pColor = (tAttackerWarriorAdvantage ? "#FB2C21" : "#43FF43");
		this.setIconValue(pName6, pMainVal6, null, pColor, false, "", '/');
		string pName7 = "i_defenders_dead";
		float pMainVal7 = (float)base.war.getDeadDefenders();
		pColor = (tAttackerDeadAdvantage ? "#FB2C21" : "#43FF43");
		this.setIconValue(pName7, pMainVal7, null, pColor, false, "", '/');
		string pName8 = "i_defenders_cities";
		float pMainVal8 = (float)base.war.countDefendersCities();
		pColor = (tAttackerCityAdvantage ? "#FB2C21" : "#43FF43");
		this.setIconValue(pName8, pMainVal8, null, pColor, false, "", '/');
		yield break;
	}

	// Token: 0x06003EF3 RID: 16115 RVA: 0x001B40E3 File Offset: 0x001B22E3
	GameObject IStatsElement.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x04002DD4 RID: 11732
	private StatsIconContainer _stats_icons;
}
